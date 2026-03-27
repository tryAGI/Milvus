#nullable enable

namespace Milvus
{
    public partial interface IAliasOperationsV2Client
    {
        /// <summary>
        /// Describe Alias<br/>
        /// This operation describes the details of a specific alias.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="authorization"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Milvus.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.CreateVectordbAliasesDescribeResponse> CreateVectordbAliasesDescribeAsync(

            global::Milvus.CreateVectordbAliasesDescribeRequest request,
            int? requestTimeout = default,
            string? authorization = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Describe Alias<br/>
        /// This operation describes the details of a specific alias.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="authorization"></param>
        /// <param name="dbName">
        /// The name of the database to which the collection belongs.
        /// </param>
        /// <param name="aliasName">
        /// The name of the alias whose details are to be listed.
        /// </param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.CreateVectordbAliasesDescribeResponse> CreateVectordbAliasesDescribeAsync(
            string dbName,
            string aliasName,
            int? requestTimeout = default,
            string? authorization = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}