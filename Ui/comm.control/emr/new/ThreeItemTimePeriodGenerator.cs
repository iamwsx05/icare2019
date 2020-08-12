using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Entity;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    public class ThreeItemTimePeriodGenerator
    {
        /// <summary>
        /// 根据传入的时间计算所在事件区间,并返回实际绘画点时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PointDate"></param>
        /// <returns></returns>
        public static eumnThreeItemsTimePeriod GetTimePeriod(DateTime dt, ref DateTime PointDate)
        {
            EntityThreeItemConstValue vo = new EntityThreeItemConstValue();
            DateTime date = dt.Date;

            DateTime dt_4_From = date.AddSeconds(vo.TimeSpan_4_From);
            DateTime dt_4_To = date.AddSeconds(vo.TimeSpan_4_To);

            DateTime dt_8_From = date.AddSeconds(vo.TimeSpan_8_From);
            DateTime dt_8_To = date.AddSeconds(vo.TimeSpan_8_To);

            DateTime dt_12_From = date.AddSeconds(vo.TimeSpan_12_From);
            DateTime dt_12_To = date.AddSeconds(vo.TimeSpan_12_To);

            DateTime dt_16_From = date.AddSeconds(vo.TimeSpan_16_From);
            DateTime dt_16_To = date.AddSeconds(vo.TimeSpan_16_To);

            DateTime dt_20_From = date.AddSeconds(vo.TimeSpan_20_From);
            DateTime dt_20_To = date.AddSeconds(vo.TimeSpan_20_To);

            DateTime dt_24_From = date.AddSeconds(vo.TimeSpan_24_From);
            DateTime dt_24_To = date.AddSeconds(vo.TimeSpan_24_To);

            if (dt >= dt_4_From && dt <= dt_4_To)
            {
                PointDate = dt;
                return eumnThreeItemsTimePeriod.TimeSpan_4;
            }
            else if (dt >= dt_8_From && dt <= dt_8_To)
            {
                PointDate = dt;
                return eumnThreeItemsTimePeriod.TimeSpan_8;
            }
            else if (dt >= dt_12_From && dt <= dt_12_To)
            {
                PointDate = dt;
                return eumnThreeItemsTimePeriod.TimeSpan_12;
            }
            else if (dt >= dt_16_From && dt <= dt_16_To)
            {
                PointDate = dt;
                return eumnThreeItemsTimePeriod.TimeSpan_16;
            }
            else if (dt >= dt_20_From && dt <= dt_20_To)
            {
                PointDate = dt;
                return eumnThreeItemsTimePeriod.TimeSpan_20;
            }

            //跨天
            if (vo.TimeSpan_24_From > vo.TimeSpan_24_To)
            {
                if (dt >= dt_24_From && dt < date.AddDays(1))
                {
                    PointDate = dt;
                    return eumnThreeItemsTimePeriod.TimeSpan_24;
                }

                if (dt >= date && dt <= dt_24_To.AddDays(1))
                {
                    PointDate = dt.AddDays(-1).Date;
                    return eumnThreeItemsTimePeriod.TimeSpan_24;
                }

                PointDate = dt;
                return eumnThreeItemsTimePeriod.TimeSpan_24;
            }
            else
            {
                PointDate = dt;
                return eumnThreeItemsTimePeriod.TimeSpan_24;
            }
        }

        //public static void CalTimePeriodSpan(DateTime dtInput, ref DateTime outputDtFrom, ref DateTime outputDtTo)
        //{
        //    DateTime dtTemp = dtInput;
        //    DateTime dtDate = dtInput.Date;

        //    eumnThreeItemsTimePeriod timePeriod = GetTimePeriod(dtInput, ref dtTemp);

        //    if (timePeriod == eumnThreeItemsTimePeriod.TimeSpan_4)
        //    {
        //        outputDtFrom = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_4_From);
        //        outputDtTo = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_4_To);
        //    }
        //    else if (timePeriod == eumnThreeItemsTimePeriod.TimeSpan_8)
        //    {
        //        outputDtFrom = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_8_From);
        //        outputDtTo = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_8_To);
        //    }
        //    else if (timePeriod == eumnThreeItemsTimePeriod.TimeSpan_12)
        //    {
        //        outputDtFrom = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_12_From);
        //        outputDtTo = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_12_To);
        //    }
        //    else if (timePeriod == eumnThreeItemsTimePeriod.TimeSpan_16)
        //    {
        //        outputDtFrom = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_16_From);
        //        outputDtTo = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_16_To);
        //    }
        //    else if (timePeriod == eumnThreeItemsTimePeriod.TimeSpan_20)
        //    {
        //        outputDtFrom = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_20_From);
        //        outputDtTo = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_20_To);
        //    }
        //    else if (timePeriod == eumnThreeItemsTimePeriod.TimeSpan_24)
        //    {
        //        if (dtInput.TimeOfDay.TotalSeconds < ThreeItemConstValue.TimeSpan_4_From)
        //        {
        //            outputDtFrom = dtDate.AddDays(-1).AddSeconds(ThreeItemConstValue.TimeSpan_24_From);
        //            outputDtTo = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_24_To);
        //        }
        //        else
        //        {
        //            outputDtFrom = dtDate.AddSeconds(ThreeItemConstValue.TimeSpan_24_From);
        //            outputDtTo = dtDate.AddDays(1).AddSeconds(ThreeItemConstValue.TimeSpan_24_To);
        //        }
        //    }
        //}
    }
}
