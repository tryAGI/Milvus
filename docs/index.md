# Milvus

[![Nuget package](https://img.shields.io/nuget/vpre/Milvus)](https://www.nuget.org/packages/Milvus/)
[![dotnet](https://github.com/tryAGI/Milvus/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/tryAGI/Milvus/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/github/license/tryAGI/Milvus)](https://github.com/tryAGI/Milvus/blob/main/LICENSE.txt)
[![Discord](https://img.shields.io/discord/1115206893015662663?label=Discord&logo=discord&logoColor=white&color=d82679)](https://discord.gg/Ca2xhfBf3v)

## Features 🔥
- Fully generated C# SDK based on [official Milvus OpenAPI specification](https://raw.githubusercontent.com/milvus-io/web-content/master/API_Reference/milvus-restful/v2.4.x/Restful%20API%20v2.openapi.json) using [AutoSDK](https://github.com/HavenDV/AutoSDK)
- Same day update to support new features
- Updated and supported automatically if there are no breaking changes
- All modern .NET features - nullability, trimming, NativeAOT, etc.
- Supports .NET 10.0

### Usage
```csharp
using Milvus;

using var client = new MilvusClient(apiKey);
```

<!-- EXAMPLES:START -->
<!-- EXAMPLES:END -->

## Support

Priority place for bugs: https://github.com/tryAGI/Milvus/issues  
Priority place for ideas and general questions: https://github.com/tryAGI/Milvus/discussions  
Discord: https://discord.gg/Ca2xhfBf3v  

## Acknowledgments

![JetBrains logo](https://resources.jetbrains.com/storage/products/company/brand/logos/jetbrains.png)

This project is supported by JetBrains through the [Open Source Support Program](https://jb.gg/OpenSourceSupport).
