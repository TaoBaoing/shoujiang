using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BasicUPMS.Models
{

    public class TreeNodeViewModel
    {
        #region jsTree需要的数据格式
        public TreeNodeViewModel()
        {
            this.children = new List<TreeNodeViewModel>();
            this.li_attr = new LiAttr();
            this.a_attr = new AAttr();
            this.data = new NodeAttr();
            this.state = new State();
        }

        public long id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public List<TreeNodeViewModel> children { get; set; }
        public LiAttr li_attr { get; set; }
        public AAttr a_attr { get; set; }
        public NodeAttr data { get; set; }
        public State state { get; set; }

        public class LiAttr
        {

        }
        public class AAttr
        {

        }
        public class NodeAttr
        {
            public long id { get; set; }
            public long parent { get; set; }

            /// <summary>
            /// 有无子节点1有，0无
            /// </summary>
            public int is_parent { get; set; }

            public int depth { get; set; }

            public string url { get; set; }

            public int sort { get; set; }
            /// <summary>
            /// 权限资源类型
            /// </summary>
            public int permission_type { get; set; }
        }
        public class State
        {
            public bool opened { get; set; }
            public bool disabled { get; set; }
            public bool selected { get; set; }
        }

        #endregion
    }
}
