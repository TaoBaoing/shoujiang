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
    public class TaxonomyService : BaseService
    {

        public List<TreeNodeViewModel> GetTaxonomyTree(long parentId, int maxDepth, long openedNodeId, List<long> selectedNodes, TaxonomyTypes taxonomy, MuKeEnabledStatus taxonomyStatus)
        {

            List<TreeNodeViewModel> all = new List<TreeNodeViewModel>();
           
                var whereExpression = Fast.Expression<Taxonomy, bool>(i =>i.Depth <= maxDepth && i.TaxonomyType==taxonomy);

                if (taxonomyStatus != MuKeEnabledStatus.None)
                {
                    whereExpression = whereExpression.And(i => (i.Status == taxonomyStatus));
                }

                var query = SessionContext.Repository.QueryableTaxonomys.
                    Where(whereExpression).
                     Select(j =>
                         new TreeNodeViewModel
                         {
                             id = j.Id,
                             text = j.Name,
                             icon = j.Icon,
                             data = new TreeNodeViewModel.NodeAttr
                             {
                                 id = j.Id,
                                 parent = j.ParentId,
                                 depth = j.Depth,
                                 url = j.LinkUrl,
                                 sort = j.Sort,
                             },
                             state = new TreeNodeViewModel.State { disabled = j.Status == MuKeEnabledStatus.Disabled }

                         });
                all = query.ToList();
         
            var result = TreeDataHandleService.ProcessingTreeData(all, parentId, maxDepth, openedNodeId, selectedNodes);
            return result;
        }
    }
}