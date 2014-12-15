using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntegrasyonServiceBase;
using N11Lib;
using N11Lib.ProductService;
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
            this.DoubleBuffered = true;
        }

        private N11DataService dataService = new N11DataService();
        private ConvertedDataSetService cdsService = new ConvertedDataSetService();
        private CategoryService catService = new CategoryService();

        // Tab1
        private List<ConvertedDataSetModel> cdsList = null;
        private Dictionary<int, DataTable> loadedTables = new Dictionary<int, DataTable>();
        private bool running = false;

        // Tab2
        private Dictionary<long, CategoryModel> categoryTable;
        private const int N11_STORE_ID = 2;

        // Tab 3
        private N11Service n11Service = new N11Service();
        private DataTable newProductsTable;
        private DataTable updateProductsTable;
        private DataTable oldProductsTable;
        private List<ProductModel> sourceList;
        private List<ProductBasic> n11List;
        private Dictionary<string, List<KeyValuePair<string, string>>> rowAttributeTable;


        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadCdsTree();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                LoadCategoryTree();
            }
        }


    #region " Transforms "

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

        private void LoadCdsTree()
        {
            running = true;
            try
            {
                treeCds.Nodes.Clear();
                bodyPanel.Controls.Clear();

                var list = cdsService.getConvertedDataSets(true);
                list = list.Where(x => x.Name.StartsWith("N11")).ToList();
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

                if (treeCds.SelectedNode == null && treeCds.Nodes.Count > 0)
                    treeCds.SelectedNode = treeCds.Nodes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            running = false;
        }


        private bool loadXml(int cdsID)
        {
            if (!loadedTables.ContainsKey(cdsID))
            {
                loadDataTable(cdsID);
                if (!loadedTables.ContainsKey(cdsID))
                {
                    MessageBox.Show("XML Tabloya yüklenemedi.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }
        private void loadDataTable(int cdsID)
        {
            if (cdsList == null)
                return;

            var cds = cdsList.FirstOrDefault(x => x.ID == cdsID);
            if (cds == null)
                return;

            var service = new XmlDataTableService();
            var loadedDataTable = service.XmlToDataTable(cds.SourceXmlPath);

            if (loadedTables.ContainsKey(cds.ID))
                loadedTables[cds.ID] = loadedDataTable;
            else
                loadedTables.Add(cds.ID, loadedDataTable);

            MessageBox.Show("XML Yüklendi.");
        }


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

    #region " Categories "

        private void btnReloadCategories_Click(object sender, EventArgs e)
        {
            LoadCategoryTree();
        }

        private void LoadCategoryTree()
        {
            try
            {
                var list = dataService.GetSubCategories(N11_STORE_ID, null);
                categoryTable = new Dictionary<long, CategoryModel>();
                treeN11Categories.Nodes.Clear();
                foreach (CategoryModel cat in list)
                {
                    treeN11Categories.Nodes.Add(categoryToTreeNode(cat, false));
                    treeProductCategories.Nodes.Add(categoryToTreeNode(cat, false));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                //loadXml(this.CurrentConvertedDataSet.ID);

                //var xmlColl = new ProductsXmlCollection(this.CurrentConvertedDataSet.SourceXmlPath);

                //xmlColl.GetSubTable("mainCategory", "category", "subCategory");

                var service = new XmlDataTableService();
                //var dt = service.XmlToDataTable(this.CurrentConvertedDataSet.SourceXmlPath);
                loadXml(this.CurrentConvertedDataSet.ID);
                var dt = loadedTables[this.CurrentConvertedDataSet.ID];
                dt = XmlDataTableService.GetSubTable(dt, "mainCategory", "category", "subCategory");

                // Get mappings
                

                //var treeTable = new Dictionary<string, Dictionary<string, List<string>>>();
                var treeTable = new List<Tuple<string, string, string, string, long?>>();
                var mappingTable = catService.GetCategoryMappings(N11_STORE_ID);

                Func<object, string> cellValueToStr = x => x is System.DBNull ? "" : (string) x;

                // DataTable to treeTable
                foreach (DataRow dr in dt.Rows)
                {
                    var mainCategory = cellValueToStr(dr["mainCategory"]);
                    var category = cellValueToStr(dr["category"]);
                    var subCategory = cellValueToStr(dr["subCategory"]);

                    var mappedCategory =
                        mappingTable.FirstOrDefault(x =>
                                                mainCategory == (x.Level1 ?? "") 
                                                    && category == (x.Level2 ?? "") 
                                                    && subCategory == (x.Level3 ?? ""));
                    var mappedCatName = mappedCategory != null ? mappedCategory.CategoryName : "";
                    var mappedCatId = mappedCategory != null ? mappedCategory.CategoryID : (long?) null;

                    treeTable.Add(new Tuple<string, string, string, string, long?>(mainCategory, category, subCategory, mappedCatName, mappedCatId));

                    //if (!String.IsNullOrEmpty(mainCategory))
                    //{
                    //    var level1 = treeTable.ContainsKey(mainCategory) ? treeTable[mainCategory] : null;
                    //    if (level1 == null)
                    //    {
                    //        level1 = new Dictionary<string, List<string>>();
                    //        treeTable.Add(mainCategory, level1);
                    //    }

                    //    if (!String.IsNullOrEmpty(category))
                    //    {
                    //        var level2 = level1.ContainsKey(category) ? level1[category] : null;
                    //        if (level2 == null)
                    //        {
                    //            level2 = new List<string>();
                    //            level1.Add(category, level2);
                    //        }

                    //        if (!String.IsNullOrEmpty(subCategory))
                    //        {
                    //            if (!level2.Contains(subCategory))
                    //                level2.Add(subCategory);
                    //        }
                    //    }
                    //}
                }

                treeTable = treeTable.Distinct().ToList();

                // Add tree nodes
                categoryTreeView1.SetDataSource(treeTable);

                //foreach (var mainCategory in treeTable.Keys)
                //{
                //    var catTable = treeTable[mainCategory];
                //    var mainNode = treeStoreCategories.Nodes.Add(mainCategory);
                //    foreach (var category in catTable.Keys)
                //    {
                //        var subList = catTable[category];
                //        var node = mainNode.Nodes.Add(category);
                //        foreach (var subCategory in subList)
                //        {
                //            node.Nodes.Add(subCategory);
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                //throw ex;
                ex.GetType();
            }


        }
        private void treeN11Categories_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "dummy" && e.Node.Tag is CategoryModel)
            {
                e.Node.Nodes.Clear();

                var cat = e.Node.Tag as CategoryModel;

                var dataService = new N11DataService();
                var list = dataService.GetSubCategories(N11_STORE_ID, (int?)cat.ID);
                foreach (var categoryModel in list)
                {
                    e.Node.Nodes.Add(categoryToTreeNode(categoryModel, false));
                }
            }
        }

        private TreeNode categoryToTreeNode(CategoryModel cat, bool getSubCategories)
        {
            var node = new TreeNode(String.Format("{0} - {1}", cat.ID, cat.Name));
            node.Tag = cat;
            if (categoryTable.ContainsKey(cat.ID))
                categoryTable.Add(cat.ID, cat);
            if (getSubCategories)
            {
                foreach (var subCat in cat.Children)
                {
                    var subNode = categoryToTreeNode(subCat, true);
                    node.Nodes.Add(subNode);
                }
            }
            else
            {
                if (cat.Attributes == null || cat.Attributes.Count == 0)
                    node.Nodes.Add(new TreeNode("dummy"));
            }
            return node;
        }

    #endregion

    #region " Products "

        //private BackgroundWorker bgw1;
        private ProgressBar progressBar1 = new ProgressBar();
        private Label lblStatus = new Label();
        //private ListBox lbLog;
        //private Button btnRunUpdate, btnStop;
        private CheckBox chkDontCheckUpdates = new CheckBox();

        private DataRow SelectedRow
        {
            get
            {
                var grid = tabProductComparison.SelectedIndex == 0 ? grid1 : (tabProductComparison.SelectedIndex == 1 ? grid2 : null);
                if (grid == null)
                    return null;

                if (grid.SelectedRows.Count != 1)
                    return null;

                return ((DataRowView) grid.SelectedRows[0].DataBoundItem).Row;
            }
        }

        private void getSourceProducts()
        {
            sourceList = n11Service.GetSourceProductsXml(N11Service.N11_XML_PATH, N11Service.PRICE_COLUMN);
            lblStatus.Text = String.Format("Kaynak XML çekildi, {0} ürün bulundu.", sourceList.Count);
        }
        private void getDestinationProducts()
        {
            n11List = n11Service.GetProductsJson();
            lblStatus.Text = String.Format("N11 ürünleri çekildi, {0} ürün bulundu.", n11List.Count);
        }
        private void compareProducts()
        {
            newProductsTable = new DataTable();
            newProductsTable.Columns.Add("stockCode");
            newProductsTable.Columns.Add("label");
            newProductsTable.Columns.Add("brand");
            newProductsTable.Columns.Add("category");
            newProductsTable.Columns.Add("n11Category");
            newProductsTable.Columns.Add("price", typeof(decimal));
            newProductsTable.Columns.Add("stockAmount", typeof(int));
            newProductsTable.Columns.Add("picture1Path");
            newProductsTable.Columns.Add("details");

            updateProductsTable = new DataTable();
            updateProductsTable.Columns.Add("stockCode");
            updateProductsTable.Columns.Add("label");
            updateProductsTable.Columns.Add("oldPrice", typeof(decimal));
            updateProductsTable.Columns.Add("newPrice", typeof(decimal));
            updateProductsTable.Columns.Add("diff", typeof(decimal));
            updateProductsTable.Columns.Add("oldStock", typeof(int));
            updateProductsTable.Columns.Add("newStock", typeof(int));

            oldProductsTable = newProductsTable.Copy();

            //return;

            var i = 0;
            var ayniCount = 0;
            foreach (var sourceProd in sourceList)
            {
                i++;
                var destProd = n11List.FirstOrDefault(x => x.productSellerCode.Contains("_" + sourceProd.stockCode + "_")); // == StockCodeToSellerCode(sourceProd.stockCode));
                if (destProd == null)
                {
                    Debug.WriteLine(String.Format("{1}\t{0} hedefte bulunamadı.", sourceProd.stockCode, i));
                    var dr = newProductsTable.NewRow();
                    dr["stockCode"] = sourceProd.stockCode;
                    dr["label"] = sourceProd.label;
                    dr["brand"] = sourceProd.brand;
                    dr["category"] = String.Format("{0} / {1} / {2}", sourceProd.mainCategory, sourceProd.category, sourceProd.subCategory);
                    dr["price"] = sourceProd.displayPrice;
                    dr["stockAmount"] = sourceProd.stockAmount;
                    dr["picture1Path"] = sourceProd.picture1Path;
                    dr["details"] = sourceProd.details;

                    newProductsTable.Rows.Add(dr);
                }
                else
                {
                    if (!chkDontCheckUpdates.Checked)
                    {
                        var sourceAmount = sourceProd.stockAmount;
                        var destAmount = n11Service.GetProductStockJson(destProd.id);

                        if (destProd.displayPrice != sourceProd.displayPrice || sourceAmount != destAmount)
                        {
                            Debug.WriteLine("{6}\t{0}\t{3}\t\t{1}\t{2}\t\t{4}\t{5}", sourceProd.stockCode, sourceProd.displayPrice, destProd.displayPrice, sourceProd.title, sourceAmount, destAmount, i);

                            var dr = updateProductsTable.NewRow();
                            dr["stockCode"] = destProd.productSellerCode;
                            dr["label"] = destProd.title;
                            dr["oldPrice"] = destProd.displayPrice;
                            dr["newPrice"] = sourceProd.displayPrice;
                            dr["oldStock"] = destAmount;
                            dr["newStock"] = sourceAmount;
                            dr["diff"] = Math.Round(Math.Abs(destProd.displayPrice - sourceProd.displayPrice) * 100 / destProd.displayPrice, 2);
                            updateProductsTable.Rows.Add(dr);

                            // Update
                            if (destProd.displayPrice != sourceProd.displayPrice)
                            {
                                // update price
                                Console.WriteLine("price\t{0}\t{1}", destProd.productSellerCode, sourceProd.displayPrice);
                                //service.UpdateProduct(destProd.productSellerCode, sourceProd.displayPrice);                            
                            }
                            if (sourceAmount != destAmount)
                            {
                                // update stock
                                Console.WriteLine("stock\t{0}\t{1}", destProd.productSellerCode, sourceAmount);
                                //service.UpdateProductStock(destProd.productSellerCode, sourceAmount);
                            }
                        }
                        else
                        {
                            ayniCount++;
                            Debug.WriteLine(String.Format("{1}\t{0} aynı.", sourceProd.stockCode, i));
                        }
                    }
                }
            }

            var diffList = n11List.Where(x => !sourceList.Any(y => x.productSellerCode.Contains("_" + y.stockCode + "_"))).ToList();

            foreach (var destProd in diffList)
            {
                i++;
                //Debug.WriteLine("{0}", i);
                var sourceProd = sourceList.FirstOrDefault(x => destProd.productSellerCode == N11Service.StockCodeToSellerCode(x.stockCode));
                if (sourceProd == null)
                {
                    //service.RemoveProduct(destProd.productSellerCode);
                    Debug.WriteLine("{0} sıfırlandı\t{1}\t{2}", i, destProd.productSellerCode, destProd.title);

                    var dr = oldProductsTable.NewRow();
                    dr["stockCode"] = destProd.productSellerCode;
                    dr["label"] = destProd.title;
                    dr["price"] = destProd.displayPrice;
                    oldProductsTable.Rows.Add(dr);
                }
            }

            grid1.DataSource = newProductsTable;
            grid2.DataSource = updateProductsTable;
            grid3.DataSource = oldProductsTable;

            lblStatus.Text = String.Format("Karşılaştırma tamamlandı. {0} yeni ürün, {1} güncelleme ve {2} stok sıfırlama işlemi var. {3} üründe değişiklik yok.",
                                        newProductsTable.Rows.Count, updateProductsTable.Rows.Count, oldProductsTable.Rows.Count, ayniCount);
        }

        private void showAttributes()
        {
            flowLayoutPanel1.Controls.Clear();

            var selectedRow = this.SelectedRow;
            if (selectedRow == null)
            {
                return;
            }

            Func<object, string, string> getDataCellValue = (object x, string y) => (x == null || x is System.DBNull) ? y : x.ToString();

            var stockCode = getDataCellValue(selectedRow["stockCode"], "");
            if ((flowLayoutPanel1.Tag as string) == stockCode)
                return;
            flowLayoutPanel1.Tag = stockCode;

            var catStr = getDataCellValue(selectedRow["n11Category"], "");
            if (String.IsNullOrWhiteSpace(catStr))
                return;

            var s = catStr.Split('-');
            if (s.Length <= 1)
                return;
            //int catId;
            //if (!int.TryParse(s[0], out catId))
            //    return;
            var catCode = s[0];

            var cat = categoryTable.Values.FirstOrDefault(x => x.Code == catCode);
            if (cat == null)
                return;

            var l = new Label { Text = cat.ToString(), AutoSize = true };
            flowLayoutPanel1.Controls.Add(l);

            // Create attribute controls. Set attribute values, if any
            var attValueTable = rowAttributeTable.ContainsKey(stockCode)
                                ? rowAttributeTable[stockCode]
                                : new List<KeyValuePair<string, string>>();
            if (cat.Attributes.Any())
            {
                foreach (var att in cat.Attributes)
                {
                    // Set attribute values, if any
                    var valuePairs = attValueTable.Where(x => x.Key == att.name).ToList();
                    var valueStr = valuePairs.Count == 0
                                        ? null
                                        : att.multipleSelect
                                                ? valuePairs.Select(x => x.Value).Aggregate((a, b) => a + "|" + b)
                                                : valuePairs[0].Value;

                    // Create the control.
                    var c = AttributeControlBase.Create(att, valueStr);
                    flowLayoutPanel1.Controls.Add(c);
                }
            }
        }

        private void btnDownloadProducts_Click(object sender, EventArgs e)3333333333333333333333333333333333,0
        {
            getSourceProducts();
            getDestinationProducts();
            compareProducts();
        }

        private void btnSaveAttributes_Click(object sender, EventArgs e)
        {

        }

        private void btnStartUpdate_Click(object sender, EventArgs e)
        {
            if (!bgw.IsBusy)
            {
                btnStartUpdate.Enabled = false;
                btnStopUpdate.Enabled = true;
                bgw.RunWorkerAsync();
            }
        }

        private void btnStopUpdate_Click(object sender, EventArgs e)
        {

        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            var i = 0;
            bgw.ReportProgress(0, "Yeni ürünler kaydediliyor.");
            foreach (DataRow dr in newProductsTable.Rows)
            {
                i++;
                var percent = (int)Math.Round((double)(i * 100 / newProductsTable.Rows.Count));
                if (dr["n11Category"] == System.DBNull.Value || String.IsNullOrWhiteSpace(dr["n11Category"].ToString()))
                {
                    bgw.ReportProgress(percent, String.Format("{0} için kategori seçilmedi.", dr["stockCode"]));
                    continue;
                }
                if (!rowAttributeTable.ContainsKey((string)dr["stockCode"]))
                {
                    bgw.ReportProgress(percent, String.Format("{0} için özellikler seçilmedi.", dr["stockCode"]));
                    continue;
                }
                var prod = new ProductModel
                {
                    stockCode = (string)dr["stockCode"],
                    label = (string)dr["label"],
                    brand = (string)dr["brand"],
                    title = (string)dr["label"],
                    //dr["category"] = String.Format("{0} / {1} / {2}", sourceProd.mainCategory, sourceProd.category, sourceProd.subCategory);
                    displayPrice = (decimal)dr["price"],
                    stockAmount = (int)dr["stockAmount"],
                    picture1Path = (string)dr["picture1Path"],
                    details = (string)dr["details"]
                };
                //var catId = long.Parse(((string)dr["n11Category"]).Split('-')[0]);
                var catIdStr = ((string)dr["n11Category"]).Split('-')[0];
                //var cat = categoryTable[catId];
                var cat = categoryTable.Values.First(x => x.Code == catIdStr);
                var attList = rowAttributeTable[(string)dr["stockCode"]];

                var result = n11Service.CreateProduct(prod, long.Parse(cat.Code), attList);
                if (result == false)
                {
                    result.GetType();
                }
                bgw.ReportProgress(percent, String.Format("{0} / {1} ({2} %)", i, newProductsTable.Rows.Count, percent));
                if (bgw.CancellationPending)
                    return;
            }

            i = 0;
            bgw.ReportProgress(0, "Ürün fiyat ve stok miktarları güncelleniyor.");
            foreach (DataRow dr in updateProductsTable.Rows)
            {
                i++;
                var percent = (int)Math.Round((double)(i * 100 / updateProductsTable.Rows.Count));

                var stockCode = (string)dr["stockCode"];
                var oldPrice = (decimal)dr["oldPrice"];
                var newPrice = (decimal)dr["newPrice"];
                var oldStock = (int)dr["oldStock"];
                var newStock = (int)dr["newStock"];
                var diff = (decimal)dr["diff"];

                if (oldPrice != newPrice)
                {
                    // update price
                    if (diff > 10)
                        diff.GetType();
                    if (!n11Service.UpdateProduct(stockCode, newPrice))
                        oldPrice.GetType();
                }

                if (oldStock != newStock)
                {
                    // update stock
                    n11Service.UpdateProductStock(stockCode, newStock);
                }


                bgw.ReportProgress(percent, String.Format("{0} / {1} ({2} %)", i, updateProductsTable.Rows.Count, percent));
            }


            i = 0;
            bgw.ReportProgress(0, "N11deki eski ürünlerin stok bilgileri sıfırlanıyor.");
            foreach (DataRow dr in oldProductsTable.Rows)
            {
                i++;
                var percent = (int)Math.Round((double)(i * 100 / oldProductsTable.Rows.Count));

                var productStockCode = (string)dr["stockCode"];
                var productTitle = (string)dr["label"];

                if (productTitle.Contains("Timberland"))
                {
                    bgw.ReportProgress(percent, String.Format("{0} / {1} ({2} %) {3} SIFIRLANMADI.", i, oldProductsTable.Rows.Count, percent, productTitle));
                    if (bgw.CancellationPending)
                        return;
                    continue;
                }

                var sourceProd = sourceList.FirstOrDefault(x => productStockCode.Contains("_" + x.stockCode + "_"));
                if (sourceProd == null)
                {
                    // Remove
                    n11Service.RemoveProduct(productStockCode);
                    bgw.ReportProgress(percent, String.Format("{0} / {1} ({2} %)", i, oldProductsTable.Rows.Count, percent));
                    Debug.WriteLine("{0} sıfırlandı\t{1}\t{2}", i, productStockCode, productTitle);
                }
                else
                {
                    bgw.ReportProgress(percent, String.Format("{0} / {1} ({2} %) Ürün ES'de mevcut, sıfırlanmıyor.", i, oldProductsTable.Rows.Count, percent));
                    sourceProd.GetType();
                }

                if (bgw.CancellationPending)
                    return;
            }

            bgw.ReportProgress(100, "Bitti");
        }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (e.UserState != null)
            {
                var str = String.Format("{0}: {1}", DateTime.Now.ToShortTimeString(), e.UserState);
                lblStatus.Text = str;
                lbLog.Items.Insert(0, str);
            }
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStartUpdate.Enabled = true;
            btnStopUpdate.Enabled = false;
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            showAttributes();
        }


    #endregion


        

    }
}
