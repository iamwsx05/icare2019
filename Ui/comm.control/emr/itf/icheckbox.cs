using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    public interface ICheckBox
    {
        bool Checked { get; set; }

        decimal CheckedWeightValue { get; set; }

        string GroupName { get; set; }

        string SumName { get; set; }

        event EventHandler CheckedChanged;

        /// <summary>
        /// 同组必填控件组号(1,2,3... a,b,c...)
        /// </summary>
        string EssentialGroupNo { set; get; }

        event HandleItemClick ItemClick;
    }
}
