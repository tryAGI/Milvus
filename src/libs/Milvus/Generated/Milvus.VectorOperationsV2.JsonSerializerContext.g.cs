
#nullable enable

#pragma warning disable CS0618 // Type or member is obsolete
#pragma warning disable CS3016 // Arrays as attribute arguments is not CLS-compliant

namespace Milvus
{
    /// <summary>
    ///
    /// </summary>
    [global::System.Text.Json.Serialization.JsonSourceGenerationOptions(
        DefaultIgnoreCondition = global::System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        Converters = new global::System.Type[]
        {
        })]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.Dictionary<string, string>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.Dictionary<string, object>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.List<object>), TypeInfoPropertyName = "SystemCollectionsGeneric_ObjectList")]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Text.Json.JsonElement?))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(string))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(int))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.IList<string>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.HttpapiGenericRespCustomerDeleteResp))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CustomerDeleteResp))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.IList<global::Milvus.AnyOf<int?, string>>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.AnyOf<int?, string>), TypeInfoPropertyName = "AnyOfInt32String2")]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.SearchParams))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.HttpapiGenericRespCustomerUpsertResp))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.HttpapiGenericRespCustomerUpsertRespData))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(object))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.HttpapiGenericRespCustomerInsertResp))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CustomerInsertResp))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CreateVectordbEntitiesDeleteRequest))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CreateVectordbEntitiesInsertRequest))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.AnyOf<global::Milvus.CreateVectordbEntitiesInsertRequestData, global::System.Collections.Generic.IList<object>>), TypeInfoPropertyName = "AnyOfCreateVectordbEntitiesInsertRequestDataIListObject2")]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CreateVectordbEntitiesInsertRequestData))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.IList<object>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CreateVectordbEntitiesQueryRequest))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CreateVectordbEntitiesUpsertRequest))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.AnyOf<global::Milvus.CreateVectordbEntitiesUpsertRequestData, global::System.Collections.Generic.IList<object>>), TypeInfoPropertyName = "AnyOfCreateVectordbEntitiesUpsertRequestDataIListObject2")]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CreateVectordbEntitiesUpsertRequestData))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CreateVectordbEntitiesGetRequest))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.AnyOf<int?, string, global::System.Collections.Generic.IList<int>, global::System.Collections.Generic.IList<string>>), TypeInfoPropertyName = "AnyOfInt32StringIListInt32IListString2")]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.IList<int>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CreateVectordbEntitiesSearchRequest))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.IList<global::System.Collections.Generic.IList<global::Milvus.AnyOf<int?, string>>>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CreateVectordbEntitiesQueryResponse))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CreateVectordbEntitiesGetResponse))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.CreateVectordbEntitiesSearchResponse))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(int?))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.AnyOf<int?, string>?), TypeInfoPropertyName = "NullableAnyOfInt32String2")]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.AnyOf<global::Milvus.CreateVectordbEntitiesInsertRequestData, global::System.Collections.Generic.IList<object>>?), TypeInfoPropertyName = "NullableAnyOfCreateVectordbEntitiesInsertRequestDataIListObject2")]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.AnyOf<global::Milvus.CreateVectordbEntitiesUpsertRequestData, global::System.Collections.Generic.IList<object>>?), TypeInfoPropertyName = "NullableAnyOfCreateVectordbEntitiesUpsertRequestDataIListObject2")]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.AnyOf<int?, string, global::System.Collections.Generic.IList<int>, global::System.Collections.Generic.IList<string>>?), TypeInfoPropertyName = "NullableAnyOfInt32StringIListInt32IListString2")]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.List<string>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.List<global::Milvus.AnyOf<int?, string>>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.AnyOf<global::Milvus.CreateVectordbEntitiesInsertRequestData, global::System.Collections.Generic.List<object>>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.AnyOf<global::Milvus.CreateVectordbEntitiesUpsertRequestData, global::System.Collections.Generic.List<object>>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::Milvus.AnyOf<int?, string, global::System.Collections.Generic.List<int>, global::System.Collections.Generic.List<string>>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.List<int>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::Milvus.AnyOf<int?, string>>>))]
    internal sealed partial class VectorOperationsV2SourceGenerationContextChunk0 : global::System.Text.Json.Serialization.JsonSerializerContext
    {
    }
    /// <summary>
    ///
    /// </summary>
    public sealed partial class VectorOperationsV2SourceGenerationContext : global::System.Text.Json.Serialization.JsonSerializerContext
    {
        private static readonly global::System.Text.Json.Serialization.Metadata.IJsonTypeInfoResolver Resolver = new LazyChunkResolver();


        private static readonly global::System.Text.Json.JsonSerializerOptions DefaultOptions = CreateDefaultOptions();

        /// <summary>
        ///
        /// </summary>
        public static VectorOperationsV2SourceGenerationContext Default { get; } = new(DefaultOptions);

        private VectorOperationsV2SourceGenerationContext(global::System.Text.Json.JsonSerializerOptions options)
            : base(options)
        {
        }

        /// <inheritdoc />
        protected override global::System.Text.Json.JsonSerializerOptions? GeneratedSerializerOptions => DefaultOptions;

        /// <inheritdoc />
        public override global::System.Text.Json.Serialization.Metadata.JsonTypeInfo? GetTypeInfo(global::System.Type type)
        {
            return Resolver.GetTypeInfo(type, Options);
        }

        /// <summary>
        /// Adds this package's converters to <paramref name="options"/>.
        /// </summary>
        /// <remarks>
        /// A converter has to be on the options a chained resolver builds its JsonTypeInfo against,
        /// and a context resolves types from every package below it. Each package contributes only
        /// what it owns and calls down the chain for the rest, so the family's converter table is
        /// written once rather than copied into all of them.
        /// </remarks>
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static void AddConverters(global::System.Text.Json.JsonSerializerOptions options)
        {
            options.Converters.Add(new global::Milvus.JsonConverters.AnyOfJsonConverter<int?, string>());
            options.Converters.Add(new global::Milvus.JsonConverters.AnyOfJsonConverter<global::Milvus.CreateVectordbEntitiesInsertRequestData, global::System.Collections.Generic.IList<object>>());
            options.Converters.Add(new global::Milvus.JsonConverters.AnyOfJsonConverter<global::Milvus.CreateVectordbEntitiesUpsertRequestData, global::System.Collections.Generic.IList<object>>());
            options.Converters.Add(new global::Milvus.JsonConverters.AnyOfJsonConverter<int?, string, global::System.Collections.Generic.IList<int>, global::System.Collections.Generic.IList<string>>());
            options.Converters.Add(new global::Milvus.JsonConverters.UnixTimestampJsonConverter());
        }

        private static global::System.Text.Json.JsonSerializerOptions CreateDefaultOptions()
        {
            var options = new global::System.Text.Json.JsonSerializerOptions
            {
                DefaultIgnoreCondition = global::System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                TypeInfoResolver = Resolver,
            };
            AddConverters(options);

            return options;
        }

        private sealed class LazyChunkResolver : global::System.Text.Json.Serialization.Metadata.IJsonTypeInfoResolver
        {
            private readonly object _gate = new();
            private readonly global::System.Text.Json.Serialization.Metadata.IJsonTypeInfoResolver?[] _resolvers = new global::System.Text.Json.Serialization.Metadata.IJsonTypeInfoResolver?[1];

            public global::System.Text.Json.Serialization.Metadata.JsonTypeInfo? GetTypeInfo(
                global::System.Type type,
                global::System.Text.Json.JsonSerializerOptions options)
            {
                for (var index = 0; index < _resolvers.Length; index++)
                {
                    var typeInfo = GetResolver(index).GetTypeInfo(type, options);
                    if (typeInfo is not null)
                    {
                        return typeInfo;
                    }
                }

                return null;
            }

            private global::System.Text.Json.Serialization.Metadata.IJsonTypeInfoResolver GetResolver(int index)
            {
                var resolver = global::System.Threading.Volatile.Read(ref _resolvers[index]);
                if (resolver is not null)
                {
                    return resolver;
                }

                lock (_gate)
                {
                    return _resolvers[index] ??= CreateResolver(index);
                }
            }

            private static global::System.Text.Json.Serialization.Metadata.IJsonTypeInfoResolver CreateResolver(int index)
            {
                return index switch
                {
                    0 => new VectorOperationsV2SourceGenerationContextChunk0(new global::System.Text.Json.JsonSerializerOptions()),
                    _ => throw new global::System.ArgumentOutOfRangeException(nameof(index)),
                };
            }
        }
    }
}