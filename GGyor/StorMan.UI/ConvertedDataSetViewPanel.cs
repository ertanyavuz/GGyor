using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorMan.UI
{
    public partial class ConvertedDataSetViewPanel : UserControl
    {
        public ConvertedDataSetViewPanel()
        {
            InitializeComponent();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bu veri kaynağı silinecek, devam etmek istiyor musunuz?", "", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                
            }
        }
    }
}
