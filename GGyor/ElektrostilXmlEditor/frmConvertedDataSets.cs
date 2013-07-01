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
            try
            {
                loading = true;
                cDataSets = _service.getConvertedDataSets();

                lvDataSetList.Items.AddRange(cDataSets.Select(x => new ListViewItem(new string[] { x.ID.ToString(), x.Name, x.SourceXmlPath })).ToArray());
                loading = false;
                if (lvDataSetList.Items.Count > 0 && lvDataSetList.SelectedIndices.Count == 0)
                    lvDataSetList.SelectedIndices.Add(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.ToString());
            }
        }

        private void lvDataSetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTransforms();
        }
        private void LoadTransforms()
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

                lvTransforms.Items.AddRange(cDataSet.Transforms.Select(x => new ListViewItem(new string[] {x.ID.ToString(), x.Name})).ToArray());

                if (lvTransforms.Items.Count > 0 && lvTransforms.SelectedIndices.Count == 0)
                    lvTransforms.SelectedIndices.Add(0);
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
            var cds = SelectedDataSet;
            if (cds == null)
                return;
            var f = new frmTransform();
            f.SourceXML = cds.SourceXmlPath;
            f.Transform = new TransformModel();

            if (f.ShowDialog() == DialogResult.OK)
            {
                _service.createTransform(SelectedDataSet.ID, f.Transform);
                LoadTransforms();
            }
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            var cds = SelectedDataSet;
            if (cds == null)
                return;
            var f = new frmTransform();
            f.SourceXML = cds.SourceXmlPath;
            f.Transform = SelectedTransform;
            if (f.Transform == null)
                return;

            if (f.ShowDialog() == DialogResult.OK)
            {
                _service.updateTransform(f.Transform);
                LoadTransforms();
            }
        }

        private void btnKaldir_Click(object sender, EventArgs e)
        {
            var t = SelectedTransform;
            if (t == null)
                return;
            _service.deleteTransform(t.ID);

            LoadTransforms();
        }

    }
}
