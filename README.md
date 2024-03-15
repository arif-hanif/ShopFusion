# ShopFusion

- Open source, Cloud Native, GraphQL first eCommerce platform that puts developers first.

# Features

- Headless: Build web, mobile, automation, etc...
- GraphQL API
- Dashboard
- Composable
- Scalable
- Cloud

# Installation

```
dotnet tool restore
dotnet workload install aspire

```

# Documentation

## Export Schema

```

dotnet run --project ./src/<PROJECT> -- schema export --output schema.graphql
dotnet fusion subgraph pack -w ./src/<PROJECT>
dotnet fusion compose -p ./src/Gateway/gateway.fgp -s ./src/<PROJECT>/<SUBGRAPH>.fsp

```

# Contributing

# License
