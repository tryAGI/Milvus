# Microsoft.Extensions.AI Integration

!!! tip "Cross-SDK comparison"
    See the [centralized MEAI documentation](https://tryagi.github.io/docs/meai/) for feature matrices and comparisons across all tryAGI SDKs.

The Milvus SDK provides `AIFunction` tool wrappers compatible with [Microsoft.Extensions.AI](https://learn.microsoft.com/en-us/dotnet/ai/microsoft-extensions-ai). These tools can be used with any `IChatClient` to give AI models access to Milvus vector database operations including search, insert, query, and collection management.

## Installation

```bash
dotnet add package tryAGI.Milvus
```

## Available Tools

| Method | Tool Name | Description |
|--------|-----------|-------------|
| `AsSearchVectorsTool()` | `SearchVectors` | Search for similar vectors using vector similarity |
| `AsInsertVectorsTool()` | `InsertVectors` | Insert entities into a collection |
| `AsCreateCollectionTool()` | `CreateCollection` | Create a new vector collection |
| `AsListCollectionsTool()` | `ListCollections` | List all vector collections |
| `AsDescribeCollectionTool()` | `DescribeCollection` | Describe a collection's schema and status |
| `AsQueryVectorsTool()` | `QueryVectors` | Query entities by scalar filter expression |
| `AsDeleteVectorsTool()` | `DeleteVectors` | Delete entities by filter expression |

## Usage

```csharp
using Microsoft.Extensions.AI;
using Milvus;

var client = new MilvusClient(
    apiKey: Environment.GetEnvironmentVariable("MILVUS_API_KEY")!);

var options = new ChatOptions
{
    Tools = [client.AsSearchVectorsTool()],
};

IChatClient chatClient = /* your chat client */;

var messages = new List<ChatMessage>
{
    new(ChatRole.User, "Search for vectors similar to 'machine learning' in the articles collection"),
};

while (true)
{
    var response = await chatClient.GetResponseAsync(messages, options);
    messages.AddRange(response.ToChatMessages());

    if (response.FinishReason == ChatFinishReason.ToolCalls)
    {
        var results = await response.CallToolsAsync(options);
        messages.AddRange(results);
        continue;
    }

    Console.WriteLine(response.Text);
    break;
}
```
