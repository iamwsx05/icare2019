using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    public interface IPluginEventControl
    {
        string ClickEventScript { get; set; }
        event EventHandler Click;
    }
}
