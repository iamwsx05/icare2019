using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ctlFlowLayoutPanel : System.Windows.Forms.FlowLayoutPanel
    {
        public ctlFlowLayoutPanel()
        {
            AutoScroll = false;
        }

        public int MaxHeight
        {
            get
            {
                int intMaxHeight = 0;
                foreach (Control ctl in this.Controls)
                {
                    intMaxHeight = Math.Max(ctl.Location.Y + ctl.Height, intMaxHeight);
                }
                return intMaxHeight + 5;
            }
        }

        public new void Resize()
        {
            this.Height = MaxHeight;
        }
    }
}
