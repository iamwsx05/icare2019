using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    public class ThreeItemsColumnData
    {
        public DateTime RecDate { get; private set; }

        public List<EntityEmrTemperatureMonitorData> TempuValues { get; private set; }
        public List<EntityEmrTemperatureMonitorData> HeartRateValues { get; private set; }
        public List<EntityEmrTemperatureMonitorData> PulseValues { get; private set; }
        public List<EntityEmrTemperatureMonitorData> BreathValues { get; private set; }
        public List<EntityEmrTemperatureMonitorData> BloodPressureValues { get; private set; }

        /// <summary>
        /// 总入液量
        /// </summary>
        public EntityEmrTemperatureMonitorData TotalLiqValue { get; set; }

        /// <summary>
        /// 大便
        /// </summary>
        public EntityEmrTemperatureMonitorData ExcrementValue { get; set; }

        /// <summary>
        /// 小便
        /// </summary>
        public EntityEmrTemperatureMonitorData UrineValue { get; set; }

        /// <summary>
        /// 排出量，其他
        /// </summary>
        public List<EntityEmrTemperatureMonitorData> Other1Values { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public EntityEmrTemperatureMonitorData WeightValue { get; set; }

        /// <summary>
        /// 皮试
        /// </summary>
        public List<EntityEmrTemperatureMonitorData> PeauTestValues { get; set; }


        /// <summary>
        /// 其他
        /// </summary>
        public List<EntityEmrTemperatureMonitorData> Other2Values { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        public List<EntityEmrTemperatureChartEvent> EventValues { get; private set; }


        public ThreeItemsColumnData(DateTime date)
        {
            this.RecDate = date;
            this.TempuValues = new List<EntityEmrTemperatureMonitorData>();
            this.HeartRateValues = new List<EntityEmrTemperatureMonitorData>();
            this.PulseValues = new List<EntityEmrTemperatureMonitorData>();
            this.BreathValues = new List<EntityEmrTemperatureMonitorData>();
            this.BloodPressureValues = new List<EntityEmrTemperatureMonitorData>();

            this.TotalLiqValue = null;
            this.ExcrementValue = null;
            this.WeightValue = null;

            this.Other1Values = new List<EntityEmrTemperatureMonitorData>();

            this.EventValues = new List<EntityEmrTemperatureChartEvent>();

            this.PeauTestValues = new List<EntityEmrTemperatureMonitorData>();
            this.Other2Values = new List<EntityEmrTemperatureMonitorData>();
        }
    }
}
