using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Controls;

namespace Common.Controls.Emr
{
    public interface IXtraListBox
    {
        [System.ComponentModel.Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        ListBoxItemCollection Items { get; }
    }
}
