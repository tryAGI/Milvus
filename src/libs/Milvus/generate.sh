dotnet tool install --global autosdk.cli --prerelease
rm -rf Generated
curl -o openapi.json https://raw.githubusercontent.com/milvus-io/web-content/master/API_Reference/milvus-restful/v2.4.x/Restful%20API%20v2.openapi.json

# Fix auth: inject securitySchemes, top-level security, and servers
jq '
  .components.securitySchemes = {
    "BearerAuth": {
      "type": "http",
      "scheme": "bearer"
    }
  }
  | .security = [{"BearerAuth": []}]
  | .servers = [{"url": "http://localhost:19530"}]
  | .info.title = "Milvus REST API"
' openapi.json > openapi.fixed.json
mv openapi.fixed.json openapi.json

autosdk generate openapi.json \
  --namespace Milvus \
  --clientClassName MilvusClient \
  --targetFramework net10.0 \
  --output Generated \
  --exclude-deprecated-operations
