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
        private List<ConvertedDataSetModel> cdsList;
        //private int cdsIndex = -1;
        private ConvertedDataSetModel CurrentConvertedDataSet
        {
            get
            {
                var node = tree.SelectedNode;
                if (node == null)
                    return null;
                while (node.Parent != null && !(node.Tag is ConvertedDataSetModel))
                    node = node.Parent;
                if (node == null)
                    return null;
                
                var cdsIndex = node.Index;
                if (cdsIndex < 0 || cdsList == null || cdsList.Count == 0 || cdsIndex >= cdsList.Count)
                    return null;
                return cdsList[cdsIndex];
            }
        }
        private TransformModel CurrentTransform
        {
            get
            {
                var node = tree.SelectedNode;
                if (node == null)
                    return null;
                while (node.Parent != null && !(node.Tag is Tuple<TransformModel>))
                    node = node.Parent;
                if (node == null)
                    return null;

                return ((Tuple<TransformModel>) node.Tag).Item1;
                
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            loadedTables = new Dictionary<int, DataTable>();

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

        public void ReloadTree()
        {
            running = true;

            List<int> nodeIndexList = null;
            if (tree.SelectedNode != null)
            {
                nodeIndexList = new List<int>();
                var node = tree.SelectedNode;
                while (node != null)
                {
                    nodeIndexList.Add(node.Index);
                    node = node.Parent;
                }
            }
            
            var service = new StorMan.Business.ConvertedDataSetService();
            this.cdsList = service.getConvertedDataSets(true);

            tree.Nodes.Clear();
            bodyPanel.Controls.Clear();

            foreach (var cds in cdsList)
            {
                var root = tree.Nodes.Add(cds.Name);
                root.Tag = cds;
                foreach (var tran in cds.Transforms)
                {
                    var tranNode = root.Nodes.Add(tran.Name);
                    tranNode.Tag = new Tuple<TransformModel>(tran);
                    //foreach (var filter in tran.Filters)
                    //{
                    //    var node = tranNode.Nodes.Add(filter.ToString());
                    //    node.Tag = filter;
                    //}
                    var node = tranNode.Nodes.Add("Filtreler");
                    node.Tag = tran.Filters.Select(x => x).ToList();

                    //foreach (var op in tran.Operations)
                    //{
                    //    node = tranNode.Nodes.Add(op.ToString());
                    //    node.Tag = op;
                    //}
                    node = tranNode.Nodes.Add("Operasyonlar");
                    node.Tag = tran;
                }
            }
            tree.ExpandAll();
            running = false;

            if (nodeIndexList != null && nodeIndexList.Count > 0)
            {
                TreeNode node = null;
                var index = -1;
                for (var i = nodeIndexList.Count - 1; i >= 0; i--)
                {
                    index = nodeIndexList[i];
                    if (index < 0)
                        break;
                    if (node != null && node.Nodes.Count <= index)
                        break;
                    var nodeColl = (node == null ? tree.Nodes : node.Nodes);
                    node = nodeColl.Count > index ? nodeColl[index] : (nodeColl.Count > 0 ? nodeColl[nodeColl.Count - 1] : null);
                }

                if (node != null)
                    tree.SelectedNode = node;
            }

            if (tree.Nodes.Count > 0)
                tree_NodeMouseClick(tree, new TreeNodeMouseClickEventArgs(tree.Nodes[0], MouseButtons.Left, 1, 0, 0));
        }

        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == tree.SelectedNode && this.bodyPanel.Controls.Count > 0)
                return;
            if (e.Node != null && e.Node.Tag != null)
            {
                this.bodyPanel.Controls.Clear();
                Control control = null;

                try
                {
                    if (e.Node.Tag is ConvertedDataSetModel)
                    {
                        var panel = new ConvertedDataSetViewPanel();
                        var cds = e.Node.Tag as ConvertedDataSetModel;
                        panel.ConvertedDataSet = cds;
                        control = panel;
                    }
                    else if (e.Node.Tag is TransformModel)
                    {
                        var panel = new TransformViewPanel();
                        panel.Transform = e.Node.Tag as TransformModel;
                        if (!loadXml(this.CurrentConvertedDataSet.ID))
                            return;
                        panel.DataTable = loadedTables[this.CurrentConvertedDataSet.ID].Copy();
                        control = panel;
                    }
                    else if (e.Node.Tag is List<FilterModel>)
                    {
                        var filterList = e.Node.Tag as List<FilterModel>;
                        var panel = new FilterViewPanel();
                        panel.FilterList = filterList;
                        if (!loadXml(this.CurrentConvertedDataSet.ID))
                            return;
                        panel.DataTable = loadedTables[this.CurrentConvertedDataSet.ID].Copy();

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
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.ToString());
                }
            }
        }

        private Dictionary<int, DataTable> loadedTables;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tree.Nodes.Count > 0)
            {
                var cds = tree.Nodes[0].Tag as ConvertedDataSetModel;
                if (cds == null)
                    return;

                //var products = new ProductsXmlCollection(cds.SourceXmlPath);
                var service = new XmlDataTableService();
                var loadedDataTable = service.XmlToDataTable(cds.SourceXmlPath);

                if (loadedTables.ContainsKey(cds.ID))
                    loadedTables[cds.ID] = loadedDataTable;
                else
                    loadedTables.Add(cds.ID, loadedDataTable);

                MessageBox.Show("XML Yüklendi.");
            }
        }

        private bool loadXml(int cdsID)
        {
            DataTable loadedDataTable = null;
            if (!loadedTables.ContainsKey(cdsID))
            {
                toolStripButton1_Click(null, null);
                if (!loadedTables.ContainsKey(cdsID))
                {
                    MessageBox.Show("XML Tabloya yüklenemedi.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }

        private void btnDbKaydet_Click(object sender, EventArgs e)
        {
            if (bodyPanel.Controls.Count == 0)
                return;

            var service = new ConvertedDataSetService();
            if (bodyPanel.Controls[0] is FilterViewPanel)
            {
                try
                {
                    var panel = bodyPanel.Controls[0] as FilterViewPanel;
                    var transform = this.CurrentTransform;
                    if (transform == null)
                        return;

                    transform.Filters = panel.FilterList;
                    service.updateTransform(transform);

                    ReloadTree();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.ToString());
                }
            }
            else if (bodyPanel.Controls[0] is TransformViewPanel)
            {
                try
                {
                    var panel = bodyPanel.Controls[0] as TransformViewPanel;
                    var transform = panel.Transform;
                    if (transform == null)
                        return;

                    service.updateTransform(transform);

                    ReloadTree();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.ToString());
                }
            }
            else if (bodyPanel.Controls[0] is ConvertedDataSetViewPanel)
            {
                try
                {
                    var panel = bodyPanel.Controls[0] as ConvertedDataSetViewPanel;

                    service.updateConvertedDataSet(panel.ConvertedDataSet.ID, panel.ConvertedDataSet.Name, panel.ConvertedDataSet.SourceXmlPath);

                    ReloadTree();
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }

        private void btnVeriKaynaginiSil_Click(object sender, EventArgs e)
        {
            if (bodyPanel.Controls.Count == 0)
                return;

            var service = new ConvertedDataSetService();
            if (bodyPanel.Controls[0] is ConvertedDataSetViewPanel)
            {
                var panel = bodyPanel.Controls[0] as ConvertedDataSetViewPanel;
                var cds = panel.ConvertedDataSet;
                if (cds == null)
                    return;

                if (MessageBox.Show(String.Format("Bu veri kaynağı ({0})ve tüm bağlı dönüşümler silinecek, devam etmek istiyor musunuz?", cds.Name), "", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        service.deleteConvertedDataSet(cds.ID);

                        ReloadTree();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.ToString());
                    }
                }
            }
            else if (bodyPanel.Controls[0] is TransformViewPanel)
            {
                var panel = bodyPanel.Controls[0] as TransformViewPanel;
                var tran = panel.Transform;
                if (tran == null)
                    return;

                if (MessageBox.Show(String.Format("Bu dönüşüm ({0}) tüm bağlı filtre ve operasyonlar silinecek, devam etmek istiyor musunuz?", tran.Name), "", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        service.deleteTransform(tran.ID);
                        ReloadTree();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.ToString());
                    }
                }
            }
            
        }

        private void btnYeniVeriKaynagi_Click(object sender, EventArgs e)
        {
            var service = new ConvertedDataSetService();
            service.createConvertedDataSet("Yeni Veri Kaynağı", "http://");

            ReloadTree();

        }
    }
}
