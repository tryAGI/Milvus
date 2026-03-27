#nullable enable

namespace Milvus
{
    public partial interface IRoleOperationsV2Client
    {
        /// <summary>
        /// Drop Role<br/>
        /// This operation drops an existing role. The operation will succeed if the specified role exists. Otherwise, this operation will fail.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="authorization"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Milvus.ApiException"></exception>
        global::System.Threading.Tasks.Task CreateVectordbRolesDropAsync(

            global::Milvus.RoleName request,
            int? requestTimeout = default,
            string? authorization = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Drop Role<br/>
        /// This operation drops an existing role. The operation will succeed if the specified role exists. Otherwise, this operation will fail.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="authorization"></param>
        /// <param name="roleName1">
        /// The name of the role.
        /// </param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        global::System.Threading.Tasks.Task CreateVectordbRolesDropAsync(
            string roleName1,
            int? requestTimeout = default,
            string? authorization = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}