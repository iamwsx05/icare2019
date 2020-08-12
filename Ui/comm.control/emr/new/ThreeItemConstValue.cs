using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Common.Controls.Emr
{
    public class ThreeItemConstValue
    {
        public const float OuterBorderWidth = 1f;

        public const float RowHeaderWidth = 8f;

        public const float TempuNormalLineWidth = 2f;

        public const float CaptionHeightUnit = 2f;

        #region Header
        /// <summary>
        /// 列头"时间"高度
        /// </summary>
        public const float HeaderHeight_Date = 1.5f;

        /// <summary>
        /// 列头"住院日数"高度
        /// </summary>
        public const float HeaderHeight_InDays = 1.5f;

        //列头"手术后产后日数"高度
        public const float Height_DaysAfOperation = 1.5f;

        /// <summary>
        /// 列头"时间"高度
        /// </summary>
        public const float HeaderHeight_Time = 2f;

        /// <summary>
        /// 列头总高度
        /// </summary>
        public static float HeaderTotalHeight
        {
            get
            {
                return HeaderHeight_Date + HeaderHeight_InDays + Height_DaysAfOperation + HeaderHeight_Time;
            }
        }
        #endregion

        public const string DateFormat = "yyyy-MM-dd";

        public const float DefaultBorderWidth = 1f;

        /// <summary>
        /// 最大温度刻度
        /// </summary>
        public const int MaxTemp = 42;

        /// <summary>
        /// 最小温度刻度
        /// </summary>
        public const int MinTemp = 33;

        public const float TempuNormal = 37f;

        /// <summary>
        /// 每温度格数
        /// </summary>
        public const int RowsPerTempUnit = 5;

        /// <summary>
        /// 最大脉搏刻度
        /// </summary>
        public static float MaxPulse { get; set; }

        /// <summary>
        /// 最小脉搏刻度
        /// </summary>
        public const float MinPulse = 0;

        /// <summary>
        /// 每个脉搏刻度数
        /// </summary>
        public const float PulseUnit = 20f;

        public const float TempuUnit = 1f;

        public const float DefaultFontSize = 8f;

        public static string DefaultFontFamilyName = "宋体";

        /// <summary>
        /// 数据区域线的颜色
        /// </summary>
        public static Color TempuCellColor = Color.FromArgb(122, 175, 217); //Color.FromArgb(105, 121, 126); // Color.FromArgb(93, 154, 187); //Color.Black;//  Color.CornflowerBlue;

        #region Footer
        /// <summary>
        /// 列脚"呼吸"高度
        /// </summary>
        public const float FooterHeight_Breath = 2.5f;

        /// <summary>
        ///  列脚"血压"高度
        /// </summary>
        public const float FooterHeight_Blood = 2.5f;

        /// <summary>
        /// 列脚"总入液量"高度
        /// </summary>
        public const float FooterHeight_Liq = 2.5f;

        /// <summary>
        /// 列脚"排出量-大便"高度
        /// </summary>
        public const float FooterHeight_DaBian = 2f;

        /// <summary>
        /// 列脚"排出量-尿量"高度
        /// </summary>
        public const float FooterHeight_NiaoLiang = 2f;

        /// <summary>
        /// 列脚"排出量-尿量"高度
        /// </summary>
        public const float FooterHeight_Other1 = 2f;

        /// <summary>
        /// 列脚"体重"高度
        /// </summary>
        public const float FooterHeight_Weight = 2f;

        ///// <summary>
        ///// 列脚"皮试"高度
        ///// </summary>
        //public const float FooterHeight_PiShi = 2f;

        /// <summary>
        /// 列脚"其他"高度
        /// </summary>
        public const float FooterHeight_Other2 = 2f;


        #endregion

        #region 时间区间

        //public const int TimeSpan_4_From = 2 * 60 * 60; //"02:00:00";
        //public const int TimeSpan_4_To = 6 * 60 * 60 - 1;//"05:59:59";

        //public const int TimeSpan_8_From = 6 * 60 * 60;// "06:00:00";
        //public const int TimeSpan_8_To = 10 * 60 * 60 - 1;// "09:59:59";

        //public const int TimeSpan_12_From = 10 * 60 * 60;// "10:00:00";
        //public const int TimeSpan_12_To = 14 * 60 * 60 - 1;// "13:59:59";

        //public const int TimeSpan_16_From = 14 * 60 * 60;//"17:00:00";
        //public const int TimeSpan_16_To = 18 * 60 * 60 - 1;// "17:59:59";


        //public const int TimeSpan_20_From = 18 * 60 * 60;//"18:00:00";
        //public const int TimeSpan_20_To = 22 * 60 * 60 - 1;// "21:59:59";

        //public const int TimeSpan_24_From = 22 * 60 * 60;// "22:00:00";
        //public const int TimeSpan_24_To = 2 * 60 * 60 - 1;// "01:59:59";


        #endregion

        #region Color
        public static Color ColorTempu_KouBiao = Color.Blue;
        public static Color ColorTempu_GangBiao = Color.Blue;
        public static Color ColorTempu_YeBiao = Color.Blue;
        public static Color ColorTempu_MaiBo = Color.Red;
        public static Color ColorTempu_XinLv = Color.Red;
        public static Color ColorTempu_DowmTemp = Color.Red;
        #endregion
    }
}
