using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using N11Lib;
using N11Lib.ProductService;
using StorMan.Business;
using StorMan.Model;
using ProductModel = EntegrasyonServiceBase.ProductModel;

namespace N11Entegrator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private N11Service service;
        private List<CategoryModel> categoryList;
        private List<ProductModel> sourceList;
        private List<ProductBasic> n11List;
        private Dictionary<long, CategoryModel> categoryTable;

        private const int N11_STORE_ID = 2;

        private Dictionary<string, List<KeyValuePair<string, string>>> rowAttributeTable;

        private void Form1_Load(object sender, EventArgs e)
        {
            service = new N11Service();
            rowAttributeTable = new Dictionary<string, List<KeyValuePair<string, string>>>();
        }

        #region " Category "
        
        private void btnGetCategories_Click(object sender, EventArgs e)
        {
            //categoryList = service.GetCategories();
            //categoryList = getCategoryListFromTextFile();
            //categoryList = getCategoryListFromService();
            //categoryList = getAllCategoryListFromDb();
            categoryList = getCategoryListFromDb();

            categoryTable = new Dictionary<long, CategoryModel>();

            foreach (CategoryModel cat in categoryList)
            {
                tree.Nodes.Add(categoryToTreeNode(cat, false));
            }

            lblStatus.Text = String.Format("Kategoriler çekildi, {0} kategori bulundu.", categoryList.Count);
        }
        private void tree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "dummy" && e.Node.Tag is CategoryModel)
            {
                e.Node.Nodes.Clear();

                var cat = e.Node.Tag as CategoryModel;

                var dataService = new N11DataService();
                var list = dataService.GetSubCategories(N11_STORE_ID, (int?) cat.ID);
                foreach (var categoryModel in list)
                {
                    e.Node.Nodes.Add(categoryToTreeNode(categoryModel, false));
                }
            }
        }

        private List<CategoryModel> getCategoryListFromTextFile()
        {
            var catList = new List<CategoryModel>();
            var sr = new System.IO.StreamReader(Path.Combine(Application.StartupPath, "catList.txt"));

            var line = "";
            var catTable = new Dictionary<long, CategoryModel>();
            while (!String.IsNullOrEmpty(line = sr.ReadLine()))
            {
                if (line.StartsWith("Kategori bulunamad"))
                    continue;
                var s = line.Split('\t');
                var cat = new CategoryModel
                {
                    ID = long.Parse(s[1]),
                    Name = s[3],
                    Parent = s[2] == "0" ? null : catTable[long.Parse(s[2])],
                    Children = new List<CategoryModel>()
                };
                catTable.Add(cat.ID, cat);

                if (cat.Parent != null)
                    cat.Parent.Children.Add(cat);
                else
                    catList.Add(cat);
            }
            sr.Close();

            return catList;
        }

        private List<CategoryModel> getCategoryListFromService()
        {
            var list = service.GetCategories();

            // Veritabanına kaydet.
            var dataService = new StorMan.Business.N11DataService();
            dataService.ClearCtategories(N11_STORE_ID);

            foreach (var categoryModel in list)
            {
                //catService.InsertCategory(categoryModel, N11_STORE_ID);
                insertCategory(dataService, categoryModel);
            }
            
            return list;
        }

        private List<CategoryModel> getAllCategoryListFromDb()
        {
            var dataService = new StorMan.Business.N11DataService();
            var list = dataService.GetAllCategories(N11_STORE_ID);
            return list;
        }

        private List<CategoryModel> getCategoryListFromDb()
        {
            var dataService = new StorMan.Business.N11DataService();
            var list = dataService.GetSubCategories(N11_STORE_ID, null);
            return list;
        }


        private void insertCategory(N11DataService dataService, CategoryModel category)
        {
            Debug.WriteLine("Inserting " + category.ToString());
            dataService.InsertCategory(category, N11_STORE_ID);
            foreach (var subCategory in category.Children)
            {
                insertCategory(dataService, subCategory);
            }
        }

        private TreeNode categoryToTreeNode(CategoryModel cat, bool getSubCategories)
        {
            var node = new TreeNode(String.Format("{0} - {1}", cat.ID, cat.Name));
            node.Tag = cat;
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

        #region " Fetch Old and New Data "
        
        private void btnGetSource_Click(object sender, EventArgs e)
        {
            sourceList = service.GetSourceProductsXml();
            lblStatus.Text = String.Format("Kaynak XML çekildi, {0} ürün bulundu.", sourceList.Count);
        }
        private void btnGetDestination_Click(object sender, EventArgs e)
        {
            n11List = service.GetProductsJson();
            lblStatus.Text = String.Format("N11 ürünleri çekildi, {0} ürün bulundu.", sourceList.Count);
        }
        
        #endregion

        private void grid1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var hi = grid1.HitTest(e.X, e.Y);
                if (hi.Type == DataGridViewHitTestType.RowHeader)
                {
                    var rowList = new List<DataGridViewRow>();
                    foreach (DataGridViewRow row in grid1.SelectedRows)
                    {
                        rowList.Add(row);
                    }
                    grid1.DoDragDrop(rowList, DragDropEffects.Link);
                }
            }
        }
        private void tree_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;

        }
        private void tree_DragDrop(object sender, DragEventArgs e)
        {
            //e.Effect = DragDropEffects.Link;

            var drList = e.Data.GetData(typeof(List<DataGridViewRow>)) as List<DataGridViewRow>;
            if (drList == null)
                return;
            var pt = tree.PointToClient(new Point(e.X, e.Y));
            var node = tree.GetNodeAt(pt);
            if (node != null)
            {
                var cat = node.Tag as CategoryModel;
                if (cat != null)
                {
                    foreach (var dataRowView in drList)
                    {
                        var dataRow = (dataRowView.DataBoundItem as DataRowView).Row;
                        dataRow["n11Category"] = String.Format("{0}-{1}", cat.Code, cat.ToString());
                    }
                }
            }
        }

        private DataGridViewRow selectedRow;

        private List<DataGridViewRow> SelectedRows
        {
            get
            {
                if (grid1.SelectedRows.Count > 0)
                {
                    
                }

                return null;
            }
        }
        private void grid1_SelectionChanged(object sender, EventArgs e)
        {
            if (grid1.SelectedRows.Count == 1)
            {
                if (grid1.SelectedRows[0] == selectedRow)
                    return;

                selectedRow = grid1.SelectedRows[0];
                flowLayoutPanel1.Controls.Clear();

                var catStr = selectedRow.Cells["n11Category"].Value.ToString();
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

                var l = new Label {Text = cat.ToString(), AutoSize = true};
                flowLayoutPanel1.Controls.Add(l);

                // Create attribute controls. Set attribute values, if any
                var stockCode = selectedRow.Cells["stockCode"].Value.ToString();
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
                                                    ? valuePairs.Select(x => x.Value).Aggregate((a,b) => a + "|" + "b")
                                                    : valuePairs[0].Value;

                        // Create the control.
                        var c = AttributeControlBase.Create(att, valueStr);
                        flowLayoutPanel1.Controls.Add(c);
                    }
                }

            }
            else
            {
                flowLayoutPanel1.Controls.Clear();
            }
        }


        private DataTable newProductsTable;
        private DataTable updateProductsTable;
        private DataTable oldProductsTable;

        private void btnCompareLists_Click(object sender, EventArgs e)
        {
            newProductsTable = new DataTable();
            newProductsTable.Columns.Add("stockCode");
            newProductsTable.Columns.Add("label");
            newProductsTable.Columns.Add("brand");
            newProductsTable.Columns.Add("category");
            newProductsTable.Columns.Add("n11Category");
            newProductsTable.Columns.Add("price", typeof (decimal));
            newProductsTable.Columns.Add("stockAmount", typeof (int));
            newProductsTable.Columns.Add("picture1Path");
            newProductsTable.Columns.Add("details");

            updateProductsTable = new DataTable();
            updateProductsTable.Columns.Add("stockCode");
            updateProductsTable.Columns.Add("label");
            updateProductsTable.Columns.Add("oldPrice", typeof (decimal));
            updateProductsTable.Columns.Add("newPrice", typeof (decimal));
            updateProductsTable.Columns.Add("diff", typeof (decimal));
            updateProductsTable.Columns.Add("oldStock", typeof (int));
            updateProductsTable.Columns.Add("newStock", typeof (int));

            oldProductsTable = newProductsTable.Copy();

            //newProductsTable.Rows.Add("asdf", "asdf", "asdf", "asdf", "", 123.45, 12, "asdf", "asdf");
            //newProductsTable.Rows.Add("asdf", "asdf", "asdf", "asdf", "", 123.45, 12, "asdf", "asdf");
            //newProductsTable.Rows.Add("asdf", "asdf", "asdf", "asdf", "", 123.45, 12, "asdf", "asdf");
            //newProductsTable.Rows.Add("asdf", "asdf", "asdf", "asdf", "", 123.45, 12, "asdf", "asdf");
            //newProductsTable.Rows.Add("asdf", "asdf", "asdf", "asdf", "", 123.45, 12, "asdf", "asdf");
            //newProductsTable.Rows.Add("asdf", "asdf", "asdf", "asdf", "", 123.45, 12, "asdf", "asdf");
            //newProductsTable.Rows.Add("asdf", "asdf", "asdf", "asdf", "", 123.45, 12, "asdf", "asdf");
            //newProductsTable.Rows.Add("asdf", "asdf", "asdf", "asdf", "", 123.45, 12, "asdf", "asdf");
            //newProductsTable.Rows.Add("asdf", "asdf", "asdf", "asdf", "", 123.45, 12, "asdf", "asdf");
            //grid1.DataSource = newProductsTable;

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
                        var destAmount = service.GetProductStockJson(destProd.id);

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

            //var diffList = n11List.Where(x => !sourceList.Any(y => x.productSellerCode == N11Service.StockCodeToSellerCode(y.stockCode))).ToList();
            var diffList = n11List.Where(x => !sourceList.Any(y => x.productSellerCode.Contains("_" + y.stockCode + "_"))).ToList();

            //var sc = "ME434TU-A";
            ////sourceList.FirstOrDefault(x => x.stockCode.Contains(sc));
            //foreach (var destProd in diffList2)
            //{
            //    foreach (var productModel in sourceList)
            //    {
            //        if (destProd.productSellerCode.Contains(productModel.stockCode))
            //        {
            //            break;
            //        }
            //    }
            //}

            //diffList.GetEnumerator();

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

        private void btnSaveAttribute_Click(object sender, EventArgs e)
        {
            if (selectedRow == null)
                return;

            var list = new List<KeyValuePair<string, string>>();
            foreach (var control in flowLayoutPanel1.Controls)
            {
                var attControl = control as AttributeControlBase;
                if (attControl != null)
                {
                    if (!String.IsNullOrWhiteSpace(attControl.AttributeValue))
                        //list.Add(attControl.AttributeModel.name, attControl.AttributeValue);
                        list.Add(new KeyValuePair<string, string>(attControl.AttributeModel.name, attControl.AttributeValue));
                }
            }

            var stockCode = (string) selectedRow.Cells["stockCode"].Value;

            rowAttributeTable[stockCode] = list;

        }

        private void btnRunUpdate_Click(object sender, EventArgs e)
        {
            if (!bgw1.IsBusy)
            {
                btnRunUpdate.Enabled = false;
                btnStop.Enabled = true;
                bgw1.RunWorkerAsync();
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (bgw1.IsBusy)
            {
                bgw1.CancelAsync();
            }
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            var i = 0;
            bgw1.ReportProgress(0, "Yeni ürünler kaydediliyor.");
            foreach (DataRow dr in newProductsTable.Rows)
            {
                i++;
                var percent = (int)Math.Round((double)(i * 100 / newProductsTable.Rows.Count));
                if (dr["n11Category"] == System.DBNull.Value || String.IsNullOrWhiteSpace(dr["n11Category"].ToString()))
                {
                    bgw1.ReportProgress(percent, String.Format("{0} için kategori seçilmedi.", dr["stockCode"]));
                    continue;
                }
                if (!rowAttributeTable.ContainsKey((string) dr["stockCode"]))
                {
                    bgw1.ReportProgress(percent, String.Format("{0} için özellikler seçilmedi.", dr["stockCode"]));
                    continue;
                }
                var prod = new ProductModel
                {
                    stockCode = (string) dr["stockCode"],
                    label = (string) dr["label"],
                    brand = (string) dr["brand"],
                    title = (string)dr["label"],
                    //dr["category"] = String.Format("{0} / {1} / {2}", sourceProd.mainCategory, sourceProd.category, sourceProd.subCategory);
                    displayPrice = (decimal) dr["price"],
                    stockAmount = (int) dr["stockAmount"],
                    picture1Path = (string) dr["picture1Path"],
                    details = (string) dr["details"]
                };
                //var catId = long.Parse(((string)dr["n11Category"]).Split('-')[0]);
                var catIdStr = ((string) dr["n11Category"]).Split('-')[0];
                //var cat = categoryTable[catId];
                var cat = categoryTable.Values.First(x => x.Code == catIdStr);
                var attList = rowAttributeTable[(string) dr["stockCode"]];

                var result = service.CreateProduct(prod, long.Parse(cat.Code), attList);
                if (result == false)
                {
                    result.GetType();
                }
                bgw1.ReportProgress(percent, String.Format("{0} / {1} ({2} %)", i, newProductsTable.Rows.Count, percent));
                if (bgw1.CancellationPending)
                    return;
            }

            i = 0;
            bgw1.ReportProgress(0, "Ürün fiyat ve stok miktarları güncelleniyor.");
            foreach (DataRow dr in updateProductsTable.Rows)
            {
                i++;
                var percent = (int)Math.Round((double)(i * 100 / updateProductsTable.Rows.Count));

                var stockCode = (string)dr["stockCode"];
                var oldPrice = (decimal) dr["oldPrice"];
                var newPrice = (decimal)dr["newPrice"];
                var oldStock = (int)dr["oldStock"];
                var newStock = (int)dr["newStock"];
                var diff = (decimal)dr["diff"];

                if (oldPrice != newPrice)
                {
                    // update price
                    if (diff > 10)
                        diff.GetType();
                    if (!service.UpdateProduct(stockCode, newPrice))
                        oldPrice.GetType();
                }

                if (oldStock != newStock)
                {
                    // update stock
                    service.UpdateProductStock(stockCode, newStock);
                }


                bgw1.ReportProgress(percent, String.Format("{0} / {1} ({2} %)", i, updateProductsTable.Rows.Count, percent));
            }

            bgw1.ReportProgress(100, "Bitti");
        }
        private void bgw1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (e.UserState != null)
            {
                var str = String.Format("{0}: {1}", DateTime.Now.ToShortTimeString(), e.UserState);
                lblStatus.Text = str;
                lbLog.Items.Insert(0, str);
            }
        }
        private void bgw1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnRunUpdate.Enabled = true;
            btnStop.Enabled = false;
        }

        private void bgw2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bgw2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

    }
}
