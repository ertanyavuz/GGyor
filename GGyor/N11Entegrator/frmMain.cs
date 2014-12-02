using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StorMan.Business;
using StorMan.Model;
using StorMan.UI;

namespace N11Entegrator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private N11DataService dataService = new N11DataService();
        private ConvertedDataSetService cdsService = new ConvertedDataSetService();

        // Tab1
        private List<ConvertedDataSetModel> cdsList = null;
        private Dictionary<int, DataTable> loadedTables = new Dictionary<int, DataTable>();
        private bool running = false;

        // Tab2
        private Dictionary<long, CategoryModel> categoryTable;
        private const int N11_STORE_ID = 2;


        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadCdsTree();
        }



        private void LoadCdsTree()
        {
            running = true;
            try
            {
                treeCds.Nodes.Clear();
                bodyPanel.Controls.Clear();

                var list = cdsService.getConvertedDataSets(true);
                cdsList = list;

                foreach (var cdsModel in list)
                {
                    var root = treeCds.Nodes.Add(cdsModel.Name);
                    root.Tag = cdsModel;
                    foreach (var tran in cdsModel.Transforms)
                    {
                        var tranNode = root.Nodes.Add(tran.Name);
                        tranNode.Tag = new Tuple<TransformModel>(tran);

                        var node = tranNode.Nodes.Add("Filtreler");
                        node.Tag = tran.Filters.Select(x => x).ToList();

                        node = tranNode.Nodes.Add("Operasyonlar");
                        node.Tag = tran;
                    }

                }

                treeCds.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            running = false;
        }

        private void LoadCategoryTree()
        {
            var list = dataService.GetSubCategories(N11_STORE_ID, null);
            categoryTable = new Dictionary<long, CategoryModel>();
            //tree.Nodes.Clear();
            //foreach (CategoryModel cat in categoryList)
            //{
            //    tree.Nodes.Add(categoryToTreeNode(cat, false));
            //}
        }

        private void treeCds_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null) return;

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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (treeCds.Nodes.Count > 0)
            {
                var cds = treeCds.Nodes[0].Tag as ConvertedDataSetModel;
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


    #region ""

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

        
    
    #endregion

    #region " Get Selecteds"

        private ConvertedDataSetModel CurrentConvertedDataSet
        {
            get
            {
                var node = treeCds.SelectedNode;
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
                var node = treeCds.SelectedNode;
                if (node == null)
                    return null;
                while (node.Parent != null && !(node.Tag is Tuple<TransformModel>))
                    node = node.Parent;
                if (node == null)
                    return null;

                return ((Tuple<TransformModel>)node.Tag).Item1;

            }
        }

    #endregion
    }
}
