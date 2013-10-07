using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StorMan.Business;
using StorMan.Model;

namespace StorMan.UI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private bool running = false;

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                toolStrip1.ImageList = imageList1;
                toolStripButton1.ImageIndex = 0;

                ReloadTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ReloadTree()
        {
            running = true;

            var service = new StorMan.Business.ConvertedDataSetService();
            var cdsList = service.getConvertedDataSets(true);

            tree.Nodes.Clear();

            foreach (var cds in cdsList)
            {
                var root = tree.Nodes.Add(cds.Name);
                root.Tag = cds;
                foreach (var tran in cds.Transforms)
                {
                    var tranNode = root.Nodes.Add(tran.Name);
                    tranNode.Tag = tran;
                    //foreach (var filter in tran.Filters)
                    //{
                    //    var node = tranNode.Nodes.Add(filter.ToString());
                    //    node.Tag = filter;
                    //}
                    var node = tranNode.Nodes.Add("Filtre");
                    node.Tag = tran.Filters.Select(x => x).ToList();

                    foreach (var op in tran.Operations)
                    {
                        node = tranNode.Nodes.Add(op.ToString());
                        node.Tag = op;
                    }
                }
            }
            tree.ExpandAll();
            running = false;
        }

        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                this.bodyPanel.Controls.Clear();
                Control control = null;
                if (e.Node.Tag is ConvertedDataSetModel)
                {
                    
                }
                else if (e.Node.Tag is TransformModel)
                {
                    var panel = new TransformViewPanel();
                    
                    control = panel;
                }
                else if (e.Node.Tag is List<FilterModel>)
                {
                    var filterList = e.Node.Tag as List<FilterModel>;
                    var panel = new FilterViewPanel();
                    panel.FilterList = filterList;
                    panel.DataTable = loadedDataTable.Copy();

                    control = panel;

                }
                else if (e.Node.Tag is OperationModel)
                {
                    //var op = e.Node.Tag as OperationModel;
                    //var panel = new OperationViewPanel();
                    //panel.Operation = op;

                    //control = panel;
                }

                if (control != null)
                {
                    control.Dock = DockStyle.Fill;
                    this.bodyPanel.Controls.Add(control);
                }
            }
        }

        private DataTable loadedDataTable;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tree.Nodes.Count > 0)
            {
                var cds = tree.Nodes[0].Tag as ConvertedDataSetModel;
                if (cds == null)
                    return;

                var products = new ProductsXmlCollection(cds.SourceXmlPath);
                loadedDataTable = products.DataTable.Copy();

                MessageBox.Show("XML Yüklendi.");
            }
        }
    }
}
