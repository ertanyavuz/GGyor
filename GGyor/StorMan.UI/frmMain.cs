using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
                    foreach (var filter in tran.Filters)
                    {
                        var node = tranNode.Nodes.Add(filter.ToString());
                        node.Tag = filter;
                    }
                    foreach (var op in tran.Operations)
                    {
                        var node = tranNode.Nodes.Add(op.ToString());
                        node.Tag = op;
                    }
                }
            }
            running = false;
        }
    }
}
