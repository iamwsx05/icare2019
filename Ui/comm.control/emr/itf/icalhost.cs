using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 控件关联计算宿主
    /// </summary>
    public interface ICalHost
    {
        /// <summary>
        /// 计算公式
        /// </summary>
        string CalFormula { get; set; }

        string DBValue { get; set; }

        List<ICalMember> CalMembers { get; }
    }

    public interface ICalMember
    {
        /// <summary>
        /// 权值
        /// </summary>
        decimal? WeightValue { get; set; }

        event CalDemandEventHandler CalDemand;

        string DBColName { get; set; }

        List<ICalHost> CalHosts { get; }
    }

    public delegate void CalDemandEventHandler(ICalMember sender, decimal? value);
}
