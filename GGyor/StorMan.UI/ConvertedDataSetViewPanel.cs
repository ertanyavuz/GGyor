using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StorMan.Business;
using StorMan.Model;

namespace StorMan.UI
{
    public partial class ConvertedDataSetViewPanel : UserControl
    {
        public ConvertedDataSetViewPanel()
        {
            InitializeComponent();
        }

        private ConvertedDataSetModel _convertedDataSet;
        public ConvertedDataSetModel ConvertedDataSet
        {
            get
            {
                return convertedDataSetControl1 != null ? convertedDataSetControl1.ConvertedDataSet : _convertedDataSet;
            }
            set
            {
                _convertedDataSet = value;
                if (convertedDataSetControl1 != null)
                    convertedDataSetControl1.ConvertedDataSet = _convertedDataSet;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            
        }

        private void ConvertedDataSetViewPanel_Load(object sender, EventArgs e)
        {
            if (_convertedDataSet != null)
                convertedDataSetControl1.ConvertedDataSet = _convertedDataSet;
        }

        private void btnSaveXml_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var service = new XmlDataTableService();
                var dt = service.ApplyConvertedDataSet(this.ConvertedDataSet);
                service.SaveAsXml(dt, saveFileDialog1.FileName);
            }
        }

        private void btnYeniTransform_Click(object sender, EventArgs e)
        {
            if (this._convertedDataSet != null)
            {
                var service = new ConvertedDataSetService();
                //this._convertedDataSet.Transforms.Add();
                var tranName = "Transform ";
                var i = 1;
                while (this._convertedDataSet.Transforms.Any(x => x.Name == tranName + i.ToString()))
                    i++;
                tranName += i.ToString();
                var tran = new TransformModel
                    {
                        Name = tranName,
                        Operations = new List<OperationModel>(),
                        Filters = new List<FilterModel>()
                    };
                service.createTransform(this._convertedDataSet.ID, tran);

                reloadTree();

            }
        }

        private void reloadTree()
        {
            var parent = this.ParentForm as frmMain;

            if (parent != null)
            {
                parent.ReloadTree();
            }
        }

    }
}
