using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace StorMan.UI
{
    public partial class CategoryTreeView : UserControl
    {
        public CategoryTreeView()
        {
            InitializeComponent();
        }

        private void CategoryTreeView_Load(object sender, EventArgs e)
        {

        }

        public void SetDataSource(List<Tuple<string, string, string, string, long?>> treeTable)
        {
            treeList1.BeginUnboundLoad();
            treeList1.Nodes.Clear();

            treeTable = treeTable.OrderBy(x => x.Item1)
                                .ThenBy(x => x.Item2)
                                .ThenBy(x => x.Item3)
                                .ToList();

            TreeListNode lastLevel1Node = null, lastLevel2Node = null;
            foreach (var tuple in treeTable)
            {
                var level = !String.IsNullOrWhiteSpace(tuple.Item3)
                                ? 3
                                : (!String.IsNullOrWhiteSpace(tuple.Item2) ? 2 : 1);
                TreeListNode thisNode = null;
                
                if (lastLevel1Node == null || tuple.Item1 != (string)lastLevel1Node.Tag)
                {
                    // Create new node
                    lastLevel1Node = treeList1.AppendNode(new[] {tuple.Item1, ""}, null);
                    lastLevel1Node.Tag = tuple.Item1;

                    thisNode = lastLevel1Node;
                }

                if (!String.IsNullOrEmpty(tuple.Item2))
                {

                    if (lastLevel2Node == null || tuple.Item2 != (string) lastLevel2Node.Tag)
                    {
                        // Create new node
                        lastLevel2Node = treeList1.AppendNode(new[] {tuple.Item2, ""}, lastLevel1Node);
                        lastLevel2Node.Tag = tuple.Item2;
                        thisNode = lastLevel2Node;
                    }

                    if (!String.IsNullOrEmpty(tuple.Item3))
                    {
                        thisNode = treeList1.AppendNode(new[] {tuple.Item3, ""}, lastLevel2Node);
                    }
                }

                if (!String.IsNullOrEmpty(tuple.Item4))
                {
                    if (thisNode != null)
                        thisNode[1] = tuple.Item4;
                }
            }

            treeList1.ExpandAll();
            treeList1.EndUnboundLoad();
        }

        //public DataTable DataSource
        //{
        //    get
        //    {
        //        if (treeList1 != null)
        //            return (DataTable) treeList1.DataSource;
        //        return null;
        //    }
        //    set
        //    {
        //        if (treeList1 != null)
        //        {
        //            treeList1.DataSource = value;
        //        }
        //    }
        //}

        //public string ParentFieldName
        //{
        //    get { return treeList1 != null ? treeList1.ParentFieldName : null; }
        //    set
        //    {
        //        if (treeList1 != null)
        //            treeList1.ParentFieldName = value;
        //    }
        //}
        //public string ChildFieldName
        //{
        //    get { return treeList1 != null ? treeList1.ParentFieldName : null; }
        //    set
        //    {
        //        if (treeList1 != null)
        //            treeList1.ParentFieldName = value;
        //    }
        //}
    }

    

}
