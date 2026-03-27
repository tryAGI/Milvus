#nullable enable

namespace Milvus
{
    public partial interface IPartitionOperationsV2Client
    {
        /// <summary>
        /// List Partitions<br/>
        /// This operation lists all partitions in the database used in the current connection.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="authorization"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Milvus.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.CreateVectordbPartitionsListResponse> CreateVectordbPartitionsListAsync(

            global::Milvus.CreateVectordbPartitionsListRequest request,
            int? requestTimeout = default,
            string? authorization = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// List Partitions<br/>
        /// This operation lists all partitions in the database used in the current connection.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="authorization"></param>
        /// <param name="dbName">
        /// The name of the target database.
        /// </param>
        /// <param name="collectionName">
        /// The name of the target collection to which the partition belongs.
        /// </param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.CreateVectordbPartitionsListResponse> CreateVectordbPartitionsListAsync(
            string dbName,
            string collectionName,
            int? requestTimeout = default,
            string? authorization = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}