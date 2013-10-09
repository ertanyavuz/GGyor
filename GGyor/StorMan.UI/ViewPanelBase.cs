using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorMan.UI
{
    public class ViewPanelBase : UserControl
    {
        public delegate void OnLogCreateEvent(ViewPanelBase sender);

        public OnLogCreateEvent OnLogCreateEventHandler { get; set; }

    }
}
