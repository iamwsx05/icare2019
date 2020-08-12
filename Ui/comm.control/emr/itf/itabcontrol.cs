using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    public interface ITabControl
    {
        DevExpress.XtraTab.XtraTabPageCollection TabPages { get; }
        DevExpress.XtraTab.TabHeaderLocation HeaderLocation { get; set; }
    }
}
