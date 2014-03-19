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
using EntegrasyonServiceBase;
using N11Lib;
using N11Lib.ProductService;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            service = new N11Service();
        }

        private void btnGetCategories_Click(object sender, EventArgs e)
        {
            categoryList = service.GetCategories();
            //categoryList = getCategoryListFromTextFile();

            foreach (CategoryModel cat in categoryList)
            {
                tree.Nodes.Add(categoryToTreeNode(cat));
            }

            lblStatus.Text = String.Format("Kategoriler çekildi, {0} kategori bulundu.", categoryList.Count);
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
                    id = long.Parse(s[1]),
                    name = s[3],
                    parent = s[2] == "0" ? null : catTable[long.Parse(s[2])],
                    subCategories = new List<CategoryModel>()
                };
                catTable.Add(cat.id, cat);

                if (cat.parent != null)
                    cat.parent.subCategories.Add(cat);
                else
                    catList.Add(cat);
            }
            sr.Close();

            return catList;
        }
        private TreeNode categoryToTreeNode(CategoryModel cat)
        {
            var node = new TreeNode(String.Format("{0} - {1}", cat.id, cat.name));
            node.Tag = cat;
            foreach (var subCat in cat.subCategories)
            {
                var subNode = categoryToTreeNode(subCat);
                node.Nodes.Add(subNode);
            }
            return node;
        }

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
                        dataRow["n11Category"] = String.Format("{0}-{1}", cat.id, cat.ToString());
                    }
                }
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
                var destProd = n11List.FirstOrDefault(x => x.productSellerCode == N11Service.StockCodeToSellerCode(sourceProd.stockCode));
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
                    //var sourceAmount = sourceProd.stockAmount;
                    //var destAmount = service.GetProductStockJson(destProd.id);

                    //if (destProd.displayPrice != sourceProd.displayPrice || sourceAmount != destAmount)
                    //{
                    //    Debug.WriteLine("{6}\t{0}\t{3}\t\t{1}\t{2}\t\t{4}\t{5}", sourceProd.stockCode, sourceProd.displayPrice, destProd.displayPrice, sourceProd.title, sourceAmount, destAmount, i);

                    //    var dr = updateProductsTable.NewRow();
                    //    dr["stockCode"] = destProd.productSellerCode;
                    //    dr["label"] = destProd.title;
                    //    dr["oldPrice"] = destProd.displayPrice;
                    //    dr["newPrice"] = sourceProd.displayPrice;
                    //    dr["oldStock"] = destAmount;
                    //    dr["newStock"] = sourceAmount;
                    //    dr["diff"] = Math.Round(Math.Abs(destProd.displayPrice - sourceProd.displayPrice)*100/destProd.displayPrice, 2);
                    //    updateProductsTable.Rows.Add(dr);
                        
                    //    // Update
                    //    if (destProd.displayPrice != sourceProd.displayPrice)
                    //    {
                    //        // update price
                    //        Console.WriteLine("price\t{0}\t{1}", destProd.productSellerCode, sourceProd.displayPrice);
                    //        //service.UpdateProduct(destProd.productSellerCode, sourceProd.displayPrice);                            
                    //    }
                    //    if (sourceAmount != destAmount)
                    //    {
                    //        // update stock
                    //        Console.WriteLine("stock\t{0}\t{1}", destProd.productSellerCode, sourceAmount);
                    //        //service.UpdateProductStock(destProd.productSellerCode, sourceAmount);
                    //    }
                    //}
                    //else
                    //{
                    //    ayniCount++;
                    //    Debug.WriteLine(String.Format("{1}\t{0} aynı.", sourceProd.stockCode, i));
                    //}
                }
            }

            var diffList = n11List.Where(x => !sourceList.Any(y => x.productSellerCode == N11Service.StockCodeToSellerCode(y.stockCode))).ToList();
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


        private void btnRunUpdate_Click(object sender, EventArgs e)
        {
            if (!bgw.IsBusy)
            {
                btnRunUpdate.Enabled = false;
                btnStop.Enabled = true;
                bgw.RunWorkerAsync();
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (bgw.IsBusy)
            {
                bgw.CancelAsync();
            }
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            var i = 0;
            bgw.ReportProgress(0, "Yeni ürünler");
            foreach (DataRow dr in newProductsTable.Rows)
            {
                var prod = new ProductModel
                {
                    stockCode = (string) dr["stockCode"],
                    label = (string) dr["label"],
                    brand = (string) dr["brand"],
                    //dr["category"] = String.Format("{0} / {1} / {2}", sourceProd.mainCategory, sourceProd.category, sourceProd.subCategory);
                    displayPrice = (decimal) dr["price"],
                    stockAmount = (int) dr["stockAmount"],
                    picture1Path = (string) dr["picture1Path"],
                    details = (string) dr["details"]
                };
                var catId = long.Parse(((string)dr["n11Category"]).Split('-')[0]);
                service.CreateProduct(prod, catId);
                i++;
                bgw.ReportProgress(i, "Yeni ürünler");
                if (bgw.CancellationPending)
                    return;
            }

            
        }
        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnRunUpdate.Enabled = true;
            btnStop.Enabled = false;
        }

    }
}
