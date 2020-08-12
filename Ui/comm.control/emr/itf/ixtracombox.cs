using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    public interface IXtraCombox
    {
        int SelectedIndex { get; set; }
        DevExpress.XtraEditors.Controls.ComboBoxItemCollection Items { get; }
    }
}
