# CLAUDE.md — Milvus SDK

## Overview

Auto-generated C# SDK for [Milvus](https://milvus.io/) — open-source vector database REST API (v2.4.x) for similarity search, collections, indexes, partitions, aliases, users, and roles.
OpenAPI spec from the official [milvus-io/web-content](https://github.com/milvus-io/web-content) repo.

## Build & Test

```bash
dotnet build Milvus.slnx
dotnet test src/tests/IntegrationTests/
```

## Auth

Bearer token auth (optional for self-hosted Milvus, required for Zilliz Cloud):

```csharp
var client = new MilvusClient(apiKey); // MILVUS_API_KEY env var
```

Default base URL: `http://localhost:19530` (local Milvus instance).

## Key Files

- `src/libs/Milvus/openapi.json` — OpenAPI spec (downloaded from milvus-io/web-content)
- `src/libs/Milvus/generate.sh` — Downloads spec, injects securitySchemes + servers + security + title via `jq`, runs autosdk
- `src/libs/Milvus/Generated/` — **Never edit** — auto-generated code
- `src/libs/Milvus/Extensions/MilvusClient.Tools.cs` — MEAI `AIFunction` tools for vector operations
- `src/tests/IntegrationTests/Tests.cs` — Test helper with bearer auth
- `src/tests/IntegrationTests/Examples/` — Example tests (also generate docs)

## Spec Notes

- Original spec has **no** `securitySchemes`, `servers`, or top-level `security` — all injected by `generate.sh`
- `jq` adds `BearerAuth` (http/bearer) security scheme, `localhost:19530` server, and `info.title`
- Uses `--exclude-deprecated-operations` flag
- All REST endpoints use POST method (Milvus v2 RESTful API convention)

## Sub-client Pattern

Milvus API has tagged operations generating sub-clients:
- `client.CollectionOperationsV2.*` — Create, describe, drop, list, load, release, rename collections; get load state and stats
- `client.VectorOperationsV2.*` — Insert, upsert, delete, get, query, search vectors
- `client.IndexOperationsV2.*` — Create, describe, drop, list indexes
- `client.PartitionOperationsV2.*` — Create, drop, list, load, release partitions; get stats; check existence
- `client.AliasOperationsV2.*` — Create, alter, describe, drop, list aliases
- `client.UserOperationsV2.*` — Create, describe, drop, list users; grant/revoke roles; update passwords
- `client.RoleOperationsV2.*` — Create, describe, drop, list roles; grant/revoke privileges

## MEAI Integration

AIFunction tools for use with any `IChatClient`:
- `AsSearchVectorsTool(defaultLimit)` — Search vectors by similarity (most important operation)
- `AsInsertVectorsTool()` — Insert entities/vectors into a collection
- `AsCreateCollectionTool(requestTimeout)` — Create a new collection (quick-setup mode)
- `AsListCollectionsTool(dbName)` — List all collections in a database
- `AsDescribeCollectionTool(dbName)` — Describe a collection's schema, fields, indexes, and status
- `AsQueryVectorsTool()` — Query entities by scalar filter expression
- `AsDeleteVectorsTool()` — Delete entities by filter expression

No MEAI interface (`IChatClient`, `IEmbeddingGenerator`, `ISpeechToTextClient`) is implemented — Milvus is a vector database with no matching MEAI interface.
