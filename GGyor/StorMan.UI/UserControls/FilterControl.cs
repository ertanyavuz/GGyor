using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StorMan.Model;

namespace StorMan.UI.UserControls
{
    public partial class FilterControl : UserControl
    {
        public FilterControl()
        {
            _filter = new FilterModel();
            this.FieldList = new List<string>();
            InitializeComponent();
        }

        private FilterModel _filter;
        private bool filterLoading = false;
        public FilterModel Filter
        {
            get
            {
                setFilter();
                return _filter;
            }
            set
            {
                _filter = value;
                if (this.DesignMode)
                    return;
                if (_filter != null && !String.IsNullOrWhiteSpace(_filter.FieldName) && _loaded)
                {
                    filterLoading = true;
                    cmbFieldName.SelectedItem = _filter.FieldName;
                    cmbFilterType.SelectedItem = _filter.FilterType == FilterTypeEnum.Equals ? "Eşitlik" : "Aralık";
                    txtValue.Text = _filter.Value.ToString();
                    filterLoading = false;
                }
            }
        }

        private List<string> _fieldList;
        public List<string> FieldList
        {
            get
            {
                return _fieldList;
            }
            set
            {
                _fieldList = value;
                if (_fieldList != null && _loaded)
                {
                    cmbFieldName.Items.Clear();
                    cmbFieldName.Items.AddRange(_fieldList.ToArray<object>());
                }
            }
        }

        private bool _loaded = false;
        private void FilterControl_Load(object sender, EventArgs e)
        {
            //if (!String.IsNullOrWhiteSpace(this.Filter.FieldName))
            //    this.FieldList.Add(this.Filter.FieldName);
            if (_fieldList == null)
                _fieldList = new List<string>();

            cmbFieldName.Items.AddRange(_fieldList.ToArray<object>());

            cmbFilterType.Items.Add("Eşitlik");
            //cmbFilterType.Items.Add("Aralık");
            _loaded = true;
        }

        private void cmbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterLoading)
                return;
            txtValue.Visible = (cmbFilterType.SelectedItem ?? "").ToString() == "Eşitlik";
            txtFrom.Visible = txtTo.Visible = !txtValue.Visible;

            //setFilter();
        }

        private void setFilter()
        {
            if (this.DesignMode)
                return;
            _filter = new FilterModel
                {
                    FieldName = cmbFieldName.SelectedItem.ToString(),
                    FilterType = cmbFilterType.SelectedItem.ToString() == "Eşitlik" ? FilterTypeEnum.Equals : FilterTypeEnum.Aralık,
                    Value = txtValue.Text
                };
        }

    }
}
