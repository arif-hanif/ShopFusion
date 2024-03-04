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

# Documentation

## Export Schema

```

dotnet run --project ./src/Products -- schema export --output schema.graphql
dotnet fusion subgraph pack -w ./src/Products
dotnet fusion compose -p ./src/Gateway/gateway.fgp -s ./src/Products/products.fsp

```

# Contributing

# License
