#nullable enable

namespace Milvus
{
    public partial interface IRoleOperationsV2Client
    {
        /// <summary>
        /// Describe Role<br/>
        /// This operation describes the details of a specified role.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="authorization"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Milvus.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.Privileges> CreateVectordbRolesDescribeAsync(

            global::Milvus.RoleName request,
            int? requestTimeout = default,
            string? authorization = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Describe Role<br/>
        /// This operation describes the details of a specified role.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="authorization"></param>
        /// <param name="roleName1">
        /// The name of the role.
        /// </param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.Privileges> CreateVectordbRolesDescribeAsync(
            string roleName1,
            int? requestTimeout = default,
            string? authorization = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}