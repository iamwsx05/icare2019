using System;
using System.Collections.Generic;
using System.Text;

namespace com.digitalwave.Utility.Controls
{
    /// <summary>
    /// ����λ��
    /// </summary>
    public static class clsPartogramLocation
    {
        #region   ��ֵ

        /// <summary>
        /// ��ʾ��Сʱ��
        /// </summary>
        internal const int c_intHours = 24;
        /// <summary>
        /// 
        /// </summary>
        internal const int c_intTopBankHeight = 10;
        /// <summary>
        /// Ѫѹ�߶�
        /// </summary>
        internal const int c_intPressureHeight = 40;
        /// <summary>
        /// Ѫѹ�ײ�����
        /// </summary>
        internal const int c_intPressureBottom = c_intPressureHeight + c_intTopBankHeight;
        /// <summary>
        /// �����߶�
        /// </summary>
        internal const int c_intUterineContractionHeight = 40;
        /// <summary>
        /// �����ײ�����
        /// </summary>
        internal const int c_intUterineContractionBottom = c_intUterineContractionHeight + c_intPressureBottom;
        /// <summary>
        /// ̥���ʸ߶�(40)
        /// </summary>
        internal const int c_intFetalRhythmHeight = 40;
        /// <summary>
        /// ̥���ʵײ�����
        /// </summary>
        internal const int c_intFetalRhythmBottom = c_intFetalRhythmHeight + c_intUterineContractionBottom;

        internal const int c_intFlawHeight = 2;
        /// <summary>
        /// ���ӿ�(22)
        /// </summary>
        internal const int c_intGridWidth = 22;
        /// <summary>
        /// ����ͼ�����и�������(12)
        /// </summary>
        internal const int c_intGridHeightCount = 12;
        /// <summary>
        /// ����ͼ�ܸ߶�
        /// </summary>
        internal const int c_intUterineNectGridTotalHeight = c_intGridWidth * c_intGridHeightCount;
        /// <summary>
        /// ����ͼ�ײ�����
        /// </summary>
        internal const int c_intUterineNectBottom = c_intFetalRhythmBottom + c_intFlawHeight + c_intUterineNectGridTotalHeight;
        /// <summary>
        /// ���ʱ��߶�(50)
        /// </summary>
        internal const int c_intCheckDateHeight = 50;
        /// <summary>
        /// ���ʱ��ײ�����
        /// </summary>
        internal const int c_intCheckDateBottom = c_intUterineNectBottom + c_intCheckDateHeight + c_intFlawHeight;
        /// <summary>
        /// ����ĸ߶�(150)
        /// </summary>
        internal const int c_intProcessHeight = 150;
        /// <summary>
        /// ����ĵײ�����
        /// </summary>
        internal const int c_intProcessBottom = c_intCheckDateBottom + c_intProcessHeight;
        /// <summary>
        /// ǩ���߶�(60)
        /// </summary>
        internal const int c_intSignHeight = 60;
        /// <summary>
        /// �ܹ��ĸ߶�
        /// </summary>
        internal static int m_intTotalHeight = c_intProcessBottom + c_intSignHeight + 10;

        /// <summary>
        /// ����ı��Ŀ��(50)
        /// </summary>
        internal const int c_intLeftTextWidth = 50;
        /// <summary>
        /// �Ҳ��ı��Ŀ��(50)
        /// </summary>
        internal const int c_intRightTextWidth = 50;
        /// <summary>
        /// ���ұ߾�(5)
        /// </summary>
        internal const int c_intLeftBeginDrawWidth = 5;

        /// <summary>
        /// ������(25)
        /// </summary>
        internal const int c_intColumnCount = 25;
        /// <summary>
        /// �ܿ��
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
        /// ���ߵ�һ�����Yλ��
        /// </summary>
        internal const int c_intMarkY = c_intUterineNectBottom - c_intFlawHeight - c_intGridWidth * 4;
        #endregion


        #region  //���������С����ֵ
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
