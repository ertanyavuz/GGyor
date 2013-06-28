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

namespace ElektrostilXmlEditor
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
            loading = true;
            cDataSets = _service.getConvertedDataSets();

            lvDataSetList.Items.AddRange(cDataSets.Select(x => new ListViewItem(new string[] { x.ID.ToString(), x.Name, x.SourceXmlPath })).ToArray());
            loading = false;
        }

        private void lvDataSetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loading)
                return;

            lvTransforms.Items.Clear();
            if (lvDataSetList.SelectedItems.Count == 0)
                return;

            var cDataSet = this.SelectedDataSet;

            if (cDataSet.Transforms == null || cDataSet.Transforms.Count == 0)
            {
                cDataSet.Transforms = _service.getTransforms(cDataSet.ID);
            }
            else
            {
                //
            }


        }

        private ConvertedDataSetModel SelectedDataSet
        {
            get
            {
                if (lvDataSetList.SelectedIndices.Count == 0)
                    return null;
                if (cDataSets == null)
                    return null;

                return cDataSets[lvDataSetList.SelectedIndices[0]];
            }
        }
        private TransformModel SelectedTransform
        {
            get
            {
                var cds = SelectedDataSet;
                if (cds == null)
                    return null;
                if (lvTransforms.SelectedIndices.Count == 0)
                    return null;
                if (cds.Transforms == null || cds.Transforms.Count == 0)
                    return null;

                return cds.Transforms[lvTransforms.SelectedIndices[0]];

            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            var f = new frmTransform();
            f.Transform = SelectedTransform;
            if (f.Transform == null)
                return;

            if (f.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

    }
}
