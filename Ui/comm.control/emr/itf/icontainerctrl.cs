using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Controls.Emr
{
    public interface IContainerCtrl
    {
        Control.ControlCollection Controls { get; }
    }
}
