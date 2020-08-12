using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DevExpress.XtraLayout;

namespace Common.Controls.Emr
{
    public interface ILayoutControl
    {
        bool DrawItemBorders { get; set; }
        LayoutControlGroup Root { get; set; }
    }
}
