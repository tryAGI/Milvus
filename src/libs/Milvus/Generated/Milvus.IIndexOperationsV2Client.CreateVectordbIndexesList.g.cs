#nullable enable

namespace Milvus
{
    public partial interface IIndexOperationsV2Client
    {
        /// <summary>
        /// List Indexes<br/>
        /// This operation lists all indexes of a specific collection.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="request"></param>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Milvus.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.CreateVectordbIndexesListResponse> CreateVectordbIndexesListAsync(

            global::Milvus.CreateVectordbIndexesListRequest request,
            int? requestTimeout = default,
            global::Milvus.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// List Indexes<br/>
        /// This operation lists all indexes of a specific collection.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="dbName">
        /// The name of the database to which the collection belongs.
        /// </param>
        /// <param name="collectionName">
        /// The name of an existing collection. Setting this to a non-existing collection leads to an error.
        /// </param>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.CreateVectordbIndexesListResponse> CreateVectordbIndexesListAsync(
            string dbName,
            int? requestTimeout = default,
            string? collectionName = default,
            global::Milvus.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}