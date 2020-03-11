using System;
using System.Collections.Generic;
using System.Text;

namespace com.digitalwave.Utility.Controls
{
    /// <summary>
    /// 定义位置
    /// </summary>
    public static class clsPartogramLocation
    {
        #region   数值

        /// <summary>
        /// 显示的小时数
        /// </summary>
        internal const int c_intHours = 24;
        /// <summary>
        /// 
        /// </summary>
        internal const int c_intTopBankHeight = 10;
        /// <summary>
        /// 血压高度
        /// </summary>
        internal const int c_intPressureHeight = 40;
        /// <summary>
        /// 血压底部坐标
        /// </summary>
        internal const int c_intPressureBottom = c_intPressureHeight + c_intTopBankHeight;
        /// <summary>
        /// 宫缩高度
        /// </summary>
        internal const int c_intUterineContractionHeight = 40;
        /// <summary>
        /// 宫缩底部坐标
        /// </summary>
        internal const int c_intUterineContractionBottom = c_intUterineContractionHeight + c_intPressureBottom;
        /// <summary>
        /// 胎心率高度(40)
        /// </summary>
        internal const int c_intFetalRhythmHeight = 40;
        /// <summary>
        /// 胎心率底部坐标
        /// </summary>
        internal const int c_intFetalRhythmBottom = c_intFetalRhythmHeight + c_intUterineContractionBottom;

        internal const int c_intFlawHeight = 2;
        /// <summary>
        /// 格子宽(22)
        /// </summary>
        internal const int c_intGridWidth = 22;
        /// <summary>
        /// 产程图正方行格子行数(12)
        /// </summary>
        internal const int c_intGridHeightCount = 12;
        /// <summary>
        /// 产程图总高度
        /// </summary>
        internal const int c_intUterineNectGridTotalHeight = c_intGridWidth * c_intGridHeightCount;
        /// <summary>
        /// 产程图底部坐标
        /// </summary>
        internal const int c_intUterineNectBottom = c_intFetalRhythmBottom + c_intFlawHeight + c_intUterineNectGridTotalHeight;
        /// <summary>
        /// 检查时间高度(50)
        /// </summary>
        internal const int c_intCheckDateHeight = 50;
        /// <summary>
        /// 检查时间底部坐标
        /// </summary>
        internal const int c_intCheckDateBottom = c_intUterineNectBottom + c_intCheckDateHeight + c_intFlawHeight;
        /// <summary>
        /// 处理的高度(150)
        /// </summary>
        internal const int c_intProcessHeight = 150;
        /// <summary>
        /// 处理的底部坐标
        /// </summary>
        internal const int c_intProcessBottom = c_intCheckDateBottom + c_intProcessHeight;
        /// <summary>
        /// 签名高度(60)
        /// </summary>
        internal const int c_intSignHeight = 60;
        /// <summary>
        /// 总共的高度
        /// </summary>
        internal static int m_intTotalHeight = c_intProcessBottom + c_intSignHeight + 10;

        /// <summary>
        /// 左侧文本的宽度(50)
        /// </summary>
        internal const int c_intLeftTextWidth = 50;
        /// <summary>
        /// 右侧文本的宽度(50)
        /// </summary>
        internal const int c_intRightTextWidth = 50;
        /// <summary>
        /// 左右边距(5)
        /// </summary>
        internal const int c_intLeftBeginDrawWidth = 5;

        /// <summary>
        /// 总列数(25)
        /// </summary>
        internal const int c_intColumnCount = 25;
        /// <summary>
        /// 总宽度
        /// </summary>
        internal static int m_intTotalWidth = c_intGridWidth * c_intColumnCount + c_intLeftTextWidth + c_intRightTextWidth + c_intLeftBeginDrawWidth * 2;

        //internal const int c_intFirstLineDownPointX = c_intLeftBeginDrawWidth + c_intLeftTextWidth + c_intGridWidth * 2;
        //internal const int c_intFirstLineDownPointY = c_intFetalRhythmBottom + c_intFlawHeight + c_intGridWidth * 8;

        //internal const int c_intFirstLineUpPointX = c_intLeftBeginDrawWidth + c_intLeftTextWidth + c_intGridWidth * 9;
        internal const int c_intFirstLineUpPointY = c_intFetalRhythmBottom + c_intFlawHeight + c_intGridWidth;

        //internal const int c_intSecendLineDownPointX = c_intLeftBeginDrawWidth + c_intLeftTextWidth + c_intGridWidth * 6;
        //internal const int c_intSecendLineDownPointY = c_intFetalRhythmBottom + c_intFlawHeight + c_intGridWidth * 8;

        //internal const int c_intSecendLineUpPointX = c_intLeftBeginDrawWidth + c_intLeftTextWidth + c_intGridWidth * 13;
        //internal const int c_intSecendLineUpPointY = c_intFetalRhythmBottom + c_intFlawHeight + c_intGridWidth;
        /// <summary>
        /// 虚线第一个点的Y位置
        /// </summary>
        internal const int c_intMarkY = c_intUterineNectBottom - c_intFlawHeight - c_intGridWidth * 4;
        #endregion


        #region  //各种字体大小的数值
        internal const float c_flt12PointFontSize = 12f;
        internal const float c_flt10PointFontSize = 10.5f;

        internal const float c_flt8PointFontSize = 8f;
        internal const float c_flt9PointFontSize = 9f;
        internal const float c_flt7PointFontSize = 7.5f;
        internal const float c_flt6PointFontSize = 6.75f;
        internal const float c_flt5PointFontSize = 5.25f;
        internal const float c_flt14PointFontSize = 14.25f;

        #endregion
    }
}
