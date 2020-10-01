using SnipeSharp.Models;

namespace SnipeSharp.EndPoint
{
    /// <summary>
    /// Provides additional methods for the Component endpoint.
    /// </summary>
    public sealed class ComponentEndPoint : EndPoint<Component>
    {
        /// <param name="api">The Api to grab the RequestManager from.</param>
        /// <exception cref="SnipeSharp.Exceptions.MissingRequiredAttributeException">When the type parameter does not have the <see cref="PathSegmentAttribute">PathSegmentAttribute</see> attribute.</exception>
        internal ComponentEndPoint(SnipeItApi api) : base(api) {}

        /// <summary>
        /// Get the list of component assignees for a component.
        /// </summary>
        /// <param name="component">The component to get the assignee list of.</param>
        /// <returns>A ResponseCollection list of ComponentAssignees.</returns>
        public ResponseCollection<ComponentAsset> GetAssignedAssets(Component component)
            => Api.Client.GetMultiple<ComponentAsset>($"{EndPointInfo.BaseUri}/{component.Id}/assets").RethrowExceptionIfAny().Value;
    }
}
