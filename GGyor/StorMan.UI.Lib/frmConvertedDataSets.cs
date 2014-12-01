using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StorMan.Business;
using StorMan.Model;

namespace StomMan.UI.Lib
{
    public partial class frmConvertedDataSets : Form
    {
        public frmConvertedDataSets()
        {
            InitializeComponent();
        }

        private bool loading = true;
        ConvertedDataSetService _service = new ConvertedDataSetService();
        List<ConvertedDataSetModel> cDataSets = new List<ConvertedDataSetModel>();

        private void frmConvertedDataSets_Load(object sender, EventArgs e)
        {

        }
    }
}
