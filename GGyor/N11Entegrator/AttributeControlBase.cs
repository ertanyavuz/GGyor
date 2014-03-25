using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StorMan.Model;

namespace N11Entegrator
{
    public class AttributeControlBase : UserControl
    {
        public AttributeModel AttributeModel { get; set; }

        public virtual string AttributeValue { get; set; }

        public static AttributeControlBase Create(AttributeModel attModel, string value)
        {
            AttributeControlBase control;
            if (!attModel.multipleSelect)
            {
                control = new ComboAttributeControl();
            }
            else
            {
                control = new MultiSelectAttributeControl();
            }

            control.AttributeModel = attModel;
            control.AttributeValue = value;

            return control;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AttributeControlBase
            // 
            this.Name = "AttributeControlBase";
            this.Load += new System.EventHandler(this.AttributeControlBase_Load);
            this.ResumeLayout(false);

        }

        private void AttributeControlBase_Load(object sender, EventArgs e)
        {
            if (this.AttributeModel != null)
            {
                var labelArr = this.Controls.Find("lblName", true);
                if (labelArr.Length > 0)
                {
                    labelArr[0].Text = this.AttributeModel.name;
                }
            }
        }

    }
}
