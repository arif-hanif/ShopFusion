import { FC, ReactNode, useMemo } from "react";
import {
  Environment,
  Network,
  RecordSource,
  Store,
  Observable,
  RequestParameters,
  Variables,
  FetchFunction,
  SubscribeFunction,
} from "relay-runtime";
import { createClient } from "graphql-sse";
//import { useAuth0 } from "@auth0/auth0-react";
import { RelayEnvironmentProvider } from "react-relay";

// Pass in GRAPHQL Endpoint, token

export const RelayProvider: FC<{ children: ReactNode }> = ({ children }) => {
  //const { getAccessTokenSilently } = useAuth0();

  const environment = useMemo(() => {
    const subscriptionsClient = createClient({
      url: import.meta.env.VITE_GRAPHQL_ENDPOINT,
      headers: async () => {
        const token =  {}//await getAccessTokenSilently();
        return {
          Authorization: `Bearer ${token}`,
        };
      },
    });

    const fetchFn: FetchFunction = async (request, variables) => {
      const token = {}//await getAccessTokenSilently();
      const resp = await fetch(import.meta.env.VITE_GRAPHQL_ENDPOINT, {
        method: "POST",
        headers: {
          Accept:
            "application/graphql-response+json; charset=utf-8, application/json; charset=utf-8",
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          query: request.text, // <-- The GraphQL document composed by Relay
          variables,
        }),
      });

      return await resp.json();
    };

    function fetchOrSubscribe(
      operation: RequestParameters,
      variables: Variables
    ) {
      return Observable.create((sink) => {
        if (!operation.text) {
          return sink.error(new Error("Operation text cannot be empty"));
        }
        return subscriptionsClient.subscribe(
          {
            operationName: operation.name,
            query: operation.text,
            variables,
          },
          sink
        );
      });
    }

    return new Environment({
      network: Network.create(fetchFn, fetchOrSubscribe as SubscribeFunction),
      store: new Store(new RecordSource()),
    });
  }, [getAccessTokenSilently]);

  return (
    <RelayEnvironmentProvider environment={environment}>
      {children}
    </RelayEnvironmentProvider>
  );
};