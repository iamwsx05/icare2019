using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Controls.Emr
{
    public interface IPanel
    {
        int Columns { get; set; }
        int Rows { get; set; }
        BorderStyle BorderStyle { get; set; }
    }
}
