#!/usr/bin/env bash
set -euo pipefail

dotnet tool update --global autosdk.cli --prerelease || dotnet tool install --global autosdk.cli --prerelease
rm -rf Generated
curl -o openapi.json https://raw.githubusercontent.com/milvus-io/web-content/master/API_Reference/milvus-restful/v2.4.x/Restful%20API%20v2.openapi.json

# Fix 1: Inject servers section and info title (spec lacks both)
jq '
  .servers = [{"url": "http://localhost:19530"}] |
  .info.title = "Milvus REST API"
' openapi.json > openapi.fixed.json
mv openapi.fixed.json openapi.json

# Auth: --security-scheme overrides the spec's missing auth with standard HTTP bearer.
autosdk generate openapi.json \
  --namespace Milvus \
  --clientClassName MilvusClient \
  --targetFramework net10.0 \
  --output Generated \
  --exclude-deprecated-operations \
  --security-scheme Http:Header:Bearer
