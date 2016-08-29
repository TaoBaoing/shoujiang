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
    public class TreeDataHandleService : BaseService
    {
        public static List<TreeNodeViewModel> ProcessingTreeData(List<TreeNodeViewModel> treeData, long parentId, int maxDepth, long openedNodeId, List<long> selectedNodes)
        {
            if (treeData.Count == 0) return null;

            //筛选出ParentId为parentId的所有子节点以及叶节点
            List<TreeNodeViewModel> requireNodes = SearchRequireNodes(treeData, parentId).ToList();
            //筛选后的最大深度以及最小深度
            var minDepth = requireNodes.Min(i => i.data.depth);
            maxDepth = requireNodes.Max(i => i.data.depth);

            //处理打开状态
            if (openedNodeId > 0)
            {
                var currentNode = requireNodes.Where(i => i.id == openedNodeId).SingleOrDefault();
                if (currentNode != null)
                {
                    currentNode.state.opened = true;
                }
            }

            //从最大深度开始递归填充Children属性,排序+选中
            FillChildrenPropertis(requireNodes, selectedNodes, maxDepth, minDepth);
            //只取出ParentId为parentId的顶级节点，以树的方式返回
            var result = requireNodes.Where(i => i.data.depth == minDepth).OrderByDescending(i => i.data.sort).ToList();
            return result;
        }

        /// <summary>
        /// 筛选ParentId为parentId的所有子节点以及叶节点
        /// </summary>
        /// <param name="all">所有节点</param>
        /// <param name="parentId">父节点ID</param>
        /// <returns></returns>
        private static IEnumerable<TreeNodeViewModel> SearchRequireNodes(List<TreeNodeViewModel> all, long parentId)
        {
            var currentDepthList = all.Where(i => i.data.parent == parentId);
            return currentDepthList.Concat(currentDepthList.SelectMany(t => SearchRequireNodes(all, t.id)));
        }

        /// <summary>
        /// 递归填充Children属性
        /// </summary>
        /// <param name="requireNodes">需要填充属性的节点</param>
        /// <param name="depth">当前深度（从最大深度递归）</param>
        /// <param name="minDepth">最小深度</param>
        private static void FillChildrenPropertis(List<TreeNodeViewModel> requireNodes, List<long> selectedNodes, int depth, int minDepth)
        {
            var currentDepthList = requireNodes.Where(i => i.data.depth == depth).OrderByDescending(i => i.data.sort);
            foreach (var item in currentDepthList)
            {
                //设置选中节点（jsTree的节点复选框选中，只选中子节点，父节点会自动选中。但是如果选中父节点，子节点会被全选。所以这里只选中子节点）
                if (item.children.Count == 0 && selectedNodes != null)
                {
                    if (selectedNodes.Contains(item.id))
                    {
                        item.state.selected = true;
                    }
                }
                //处理节点打开状态，如果打开子节点，也打开父节点
                var parrent = requireNodes.Where(i => i.id == item.data.parent).SingleOrDefault();
                if (parrent != null)
                {
                    if (item.state.opened)
                    {
                        parrent.state.opened = true;
                    }
                    parrent.children.Add(item);
                    parrent.data.is_parent = 1;//有子节点
                }

            }
            if (depth >= minDepth)
            {
                FillChildrenPropertis(requireNodes, selectedNodes, depth - 1, minDepth);
            }
        }
    }
}