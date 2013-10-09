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
                while (node.Parent != null && !(node.Tag is TransformModel))
                    node = node.Parent;
                if (node == null)
                    return null;

                return node.Tag as TransformModel;
                
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

        private void ReloadTree()
        {
            running = true;

            var service = new StorMan.Business.ConvertedDataSetService();
            this.cdsList = service.getConvertedDataSets(true);

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
        }

        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                this.bodyPanel.Controls.Clear();
                Control control = null;

                try
                {
                    if (e.Node.Tag is ConvertedDataSetModel)
                    {

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
                    var transform = this.CurrentTransform;
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
        }
    }
}
