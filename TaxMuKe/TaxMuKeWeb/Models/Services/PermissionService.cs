using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicUPMS.Models;
using BasicFramework.Core;
using BasicFramework.Component;
using System.Linq.Expressions;
using BasicCommon;
using BasicData;
namespace BasicUPMS.Services
{
    public class PermissionService : BaseService
    {
        /// <summary>
        /// 获取Tree格式数据
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="parentId">父节点ID</param>
        /// <param name="maxDepth">要获取的最大深度</param>
        /// <param name="openedNodeId">当前操作节点（用以判断是否打开其父节点）</param>
        /// <param name="permissionType">权限类型</param>
        /// <returns></returns>
        public List<TreeNodeViewModel> GetPermissionResourceTree(long roleId, long parentId, int maxDepth, long openedNodeId, List<long> selectedNodes, PermissionTypes permissionType, MuKeEnabledStatus permissionResourceStatus)
        {

            List<TreeNodeViewModel> all = new List<TreeNodeViewModel>();
            //系统管理员
            if (roleId == UPMSConfig.SystemRole)
            {
                var whereExpression = Fast.Expression<PermissionResource, bool>(i => i.Depth <= maxDepth);

                if (permissionType != PermissionTypes.None)
                {
                    whereExpression = whereExpression.And(i => (i.PermissionType & permissionType) == permissionType);
                }
                if (permissionResourceStatus != MuKeEnabledStatus.None)
                {
                    whereExpression = whereExpression.And(i => (i.Status == permissionResourceStatus));
                }

                var query = SessionContext.Repository.QueryablePermissionResources.
                    Where(whereExpression).
                     Select(j =>
                         new TreeNodeViewModel
                         {
                             id = j.Id,
                             text = j.Name,
                             icon = j.Icon,
                             data = new TreeNodeViewModel.NodeAttr { id = j.Id, parent = j.ParentId, depth = j.Depth, url = j.LinkUrl, sort = j.Sort, permission_type = (int)j.PermissionType },
                             state = new TreeNodeViewModel.State { disabled = j.Status == MuKeEnabledStatus.Disabled }
                         });
                all = query.ToList();

            }
            else
            {
                var whereExpression = Fast.Expression<Permissions, bool>(
                                i =>
                                 i.RoleId == roleId &&
                                 i.PermissionResource.Depth <= maxDepth &&
                                 i.Role.Status == MuKeEnabledStatus.Enabled &&
                                 i.AuthType == AuthTypes.Allow

                                );

                if (permissionType != PermissionTypes.None)
                {
                    whereExpression = whereExpression.And(i => (i.PermissionResource.PermissionType & permissionType) == permissionType);
                }
                if (permissionResourceStatus != MuKeEnabledStatus.None)
                {
                    whereExpression = whereExpression.And(i => (i.PermissionResource.Status == permissionResourceStatus));
                }

                var query = SessionContext.Repository.QueryablePermissions.
                    Where(whereExpression).
                     Select(j =>
                         new TreeNodeViewModel
                         {
                             id = j.PermissionResource.Id,
                             text = j.PermissionResource.Name,
                             icon = j.PermissionResource.Icon,
                             data = new TreeNodeViewModel.NodeAttr
                             {
                                 id = j.PermissionResource.Id,
                                 parent = j.PermissionResource.ParentId,
                                 depth = j.PermissionResource.Depth,
                                 url = j.PermissionResource.LinkUrl,
                                 sort = j.PermissionResource.Sort,
                                 permission_type = (int)j.PermissionResource.PermissionType
                             },
                             state = new TreeNodeViewModel.State { disabled = j.PermissionResource.Status == MuKeEnabledStatus.Disabled }

                         });
                all = query.ToList();
            }
            var result = TreeDataHandleService.ProcessingTreeData(all, parentId, maxDepth, openedNodeId, selectedNodes);
            return result;
        }
    }
}