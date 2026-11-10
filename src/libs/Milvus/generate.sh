#!/usr/bin/env bash
set -euo pipefail

install_autosdk_cli() {
  dotnet tool update --global autosdk.cli --prerelease >/dev/null 2>&1 || \
    dotnet tool install --global autosdk.cli --prerelease
}

fetch_spec() {
  curl "$@" \
    --fail --silent --show-error --location \
    --retry 5 --retry-delay 10 --retry-all-errors \
    --connect-timeout 30 --max-time 300
}

# OpenAPI spec: https://raw.githubusercontent.com/milvus-io/web-content/master/API_Reference/milvus-restful/v2.4.x/openapi.json
install_autosdk_cli
rm -rf Generated
fetch_spec --fail --silent --show-error -L -o openapi.json https://raw.githubusercontent.com/milvus-io/web-content/master/API_Reference/milvus-restful/v2.4.x/Restful%20API%20v2.openapi.json

# Fix 1: Inject servers section, info title, and fix autoID issues
# - autoID type: spec has string, API expects boolean
# - autoID casing: spec has "autoID", API returns "autoId" in responses
# - Remove autoID from required arrays (has default value)
jq '
  .servers = [{"url": "http://localhost:19530"}] |
  .info.title = "Milvus REST API" |
  walk(
    if type == "object" and has("properties") and .properties.autoID then
      .properties.autoId = (.properties.autoID | .type = "boolean" | .default = false | del(.enum)) |
      del(.properties.autoID) |
      if has("required") then .required |= map(select(. != "autoID")) else . end
    else . end
  ) |

  # `POST /v2/vectordb/roles/describe` answers with camelCase - objectType, objectName, dbName -
  # while the spec declares the snake_case spellings as required, so the response could not
  # deserialize at all. Found against the Testcontainers instance.
  (.components.schemas["Privilege-Entity"] |
     .properties.objectType = .properties.object_type |
     .properties.objectName = .properties.object_name |
     .properties.dbName = .properties.db_name |
     del(.properties.object_type, .properties.object_name, .properties.db_name) |
     .required = (.required
       | map(if . == "object_type" then "objectType"
             elif . == "object_name" then "objectName"
             elif . == "db_name" then "dbName"
             else . end))) as $entity |
  .components.schemas["Privilege-Entity"] = $entity
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
