using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using Common.Entity;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 负责画产程进程图的类
    /// </summary>
    public class PartogramDrawer
    {
        #region .ctor
        /// <summary>
        /// 构造函数
        /// </summary>
        public PartogramDrawer()
        {
            this.m_mthInitDefault();
            this.m_mthInitGrid();
            this.m_mthInitImage();
        }
        #endregion

        #region 成员和属性
        /// <summary>
        /// 表格竖线的X坐标
        /// </summary>
        private float[] m_fltXArr = null;
        /// <summary>
        /// 表格横线的Y坐标
        /// </summary>
        private float[] m_fltYArr = null;
        /// <summary>
        /// 13个需要填充信息的位置的坐标
        /// </summary>
        private PointF[] m_pntPosArr = new PointF[13];
        /// <summary>
        /// 当前图像
        /// </summary>
        private Image m_imgCurrentPic = null;
        /// <summary>
        /// 鼠标移动到数据点上是否弹出提示
        /// </summary>
        private bool blnShowToolTip { get; set; }
        /// <summary>
        /// 画图比例
        /// </summary>
        public float fltSizePerUnit { get; set; }
        /// <summary>
        /// 画图工具
        /// </summary>
        public Graphics gpsPainter { get; set; }

        /// <summary>
        /// 图像距左端距离
        /// </summary>
        public float fltLeftOffset { get; set; }

        /// <summary>
        /// 图像距顶端距离
        /// </summary>
        public float fltTopOffset { get; set; }

        /// <summary>
        /// 单元格列数
        /// </summary>
        public int intColNumber { get; set; }

        private float m_fltColWidth = 34f;
        /// <summary>
        /// 列宽度
        /// </summary>
        public float fltColWidth
        {
            get
            {
                return this.m_fltColWidth * this.fltRate;
            }
        }

        private float m_fltRowHeight = 36f;
        /// <summary>
        /// 行高
        /// </summary>
        public float fltRowHeight
        {
            get
            {
                return this.m_fltRowHeight * this.fltRate;
            }
        }

        private float m_fltHeaderHeight = 90f; //110f;
        /// <summary>
        /// 表头高
        /// </summary>
        public float fltHeaderHeight
        {
            get
            {
                return this.m_fltHeaderHeight * this.fltRate;
            }
        }

        private float m_fltFootHeight = 20f;
        /// <summary>
        /// 页脚高
        /// </summary>
        public float fltFootHeight
        {
            get
            {
                return this.m_fltFootHeight * this.fltRate;
            }
        }

        /// <summary>
        /// 画图比率（以10.5为基准）
        /// </summary>
        public float fltRate
        {
            get
            {
                return fltSizePerUnit / 10.5f;
            }
        }

        /// <summary>
        /// 线条大小
        /// </summary>
        public float fltBorderWidth { get; set; }
        /// <summary>
        /// 图高
        /// </summary>
        public float fltImgHeight { get; set; }
        /// <summary>
        /// 图宽
        /// </summary>
        public float fltImgWidth { get; set; }

        private EmrPartogramData _partogramData = null;
        /// <summary>
        /// 画图数据
        /// </summary>
        public EmrPartogramData partogramData
        {
            get
            {
                return this._partogramData;
            }
            set
            {
                this._partogramData = value;
                this.m_mthDraw();
            }
        }

        /// <summary>
        /// 图形数据
        /// </summary>
        public List<clsShape> lstShapeData { get; set; }
        /// <summary>
        /// 宫缩，血压，签名数据
        /// </summary>
        public List<clsSpecialText> lstSpecialText { get; set; }
        /// <summary>
        /// 时间轴上的文本
        /// </summary>
        public List<clsText> lstText { get; set; }
        /// <summary>
        /// 临产时间
        /// </summary>
        public DateTime? dtmLabourtime
        {
            get
            {
                DateTime? dtmResult = null;
                if (partogramData != null && partogramData.partogramMain != null)
                {
                    if (partogramData.partogramMain.labourTime.HasValue)
                    {
                        dtmResult = partogramData.partogramMain.labourTime;
                    }
                }
                return dtmResult;
            }
        }
        /// <summary>
        /// 是否已画过警戒区
        /// </summary>
        private bool blnWarnArea = false;
        #endregion

        #region 供外部调用属性和方法
        /// <summary>
        /// 画图
        /// </summary>
        public void m_mthDraw()
        {
            blnWarnArea = false;
            this.m_mthInitGrid();
            this.m_mthInitImage();
            this.m_mthDrawBackGround();

            this.m_mthInitData();
            if (lstShapeData != null)
            {
                this.m_mthDrawData();
            }
        }

        /// <summary>
        /// 获取当前图像
        /// </summary>
        /// <returns></returns>
        public Image m_imgGetImage()
        {
            if (m_imgCurrentPic != null)
            {
                return m_imgCurrentPic;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取打印用的图像
        /// </summary>
        /// <returns></returns>
        public Image m_imgGetPrintImage()
        {
            //打印之前保存必要变量待打印后恢复
            float fltCurrentSizePerUnit = this.fltSizePerUnit;
            clsShape[] objCurrentShapeArr = new clsShape[this.lstShapeData.Count];
            this.lstShapeData.CopyTo(objCurrentShapeArr);
            Graphics gpsCurrent = this.gpsPainter;
            Image imgCurrent = this.m_imgGetImage();
            float[] fltXArr = this.m_fltXArr;
            float[] fltYArr = this.m_fltYArr;
            bool blnWarnArea = this.blnWarnArea;

            this.fltSizePerUnit = 10.5f;//打印只用未缩放原图
            this.m_mthDraw();

            if (_partogramData != null && _partogramData.lstPartogramData != null &&
                _partogramData.lstPartogramData.Count > 0)
            {
                EntityEmrPartogramData objCurrentContent = _partogramData.lstPartogramData.Find(t => t.measureType == 3 && t.measureValue == "3");
                if (objCurrentContent != null)
                {
                    this.m_mthDrawWarnLine(objCurrentContent.recordDate);
                }
            }
            Image imgResult = this.m_imgGetImage();

            //恢复数据为的是坐标定位数据不会有误
            this.lstShapeData.Clear();
            this.lstShapeData.AddRange(objCurrentShapeArr);
            this.fltSizePerUnit = fltCurrentSizePerUnit;
            this.gpsPainter = gpsCurrent;
            this.m_imgCurrentPic = imgCurrent;
            this.m_fltXArr = fltXArr;
            this.m_fltYArr = fltYArr;
            this.blnWarnArea = blnWarnArea;

            return imgResult;
        }

        /// <summary>
        /// 获取指定点的数据
        /// </summary>
        /// <param name="p_fltX"></param>
        /// <param name="p_fltY"></param>
        /// <returns></returns>
        public clsDoubleClickedEventArgs m_objGetArgs(float p_fltX, float p_fltY)
        {
            clsDoubleClickedEventArgs objArgs = null;
            if (lstShapeData != null && lstShapeData.Count > 0)
            {
                List<clsShape> lstTemp = lstShapeData.FindAll(t => t.rectArea.Contains(p_fltX, p_fltY));
                if (lstTemp != null && lstTemp.Count > 0)
                {
                    lstTemp.RemoveAll(t => t.objCurrentData == null);//移除没有对应数据的图形
                    List<clsShape> lstResult = new List<clsShape>(lstTemp.Count);
                    foreach (clsShape objShape in lstTemp)
                    {
                        if (lstResult.Exists(t => t.objCurrentData == objShape.objCurrentData))
                        {
                            continue;
                        }
                        lstResult.Add(objShape);
                        if (lstResult.Count > 0)
                        {
                            objArgs = new clsDoubleClickedEventArgs(lstResult);
                        }
                    }
                }
            }
            return objArgs;
        }

        /// <summary>
        /// 画警戒区
        /// </summary>
        /// <param name="p_dtmRecordDate">宫口开大3cm时的记录时间</param>
        public void m_mthDrawWarnArea(DateTime p_dtmRecordDate)
        {
            if (blnWarnArea)
            {
                return;
            }

            if (this.dtmLabourtime != null && this.m_fltYArr != null)//临产时间不能为空
            {
                PointF[] pntTempArr = new PointF[4];
                float fltTempX = this.m_fltCalXPos(p_dtmRecordDate);
                float fltTempY = this.m_fltYArr[9];
                pntTempArr[0] = new PointF(fltTempX, fltTempY);
                fltTempX = pntTempArr[0].X + 4f * this.fltColWidth;
                pntTempArr[1] = new PointF(fltTempX, fltTempY);
                fltTempY = this.m_fltYArr[2];
                pntTempArr[3] = new PointF(fltTempX, fltTempY);
                fltTempX = pntTempArr[3].X + 4f * this.fltColWidth;
                pntTempArr[2] = new PointF(fltTempX, fltTempY);

                Pen p = new Pen(Color.FromArgb(80, Color.Green));
                this.gpsPainter.FillPolygon(p.Brush, pntTempArr);
                blnWarnArea = true;
            }
        }

        /// <summary>
        /// 画警戒线
        /// </summary>
        /// <param name="p_dtmRecordDate">宫口开大3cm时的记录时间</param>
        public void m_mthDrawWarnLine(DateTime p_dtmRecordDate)
        {
            if (this.dtmLabourtime != null && this.m_fltYArr != null)//临产时间不能为空
            {
                PointF[] pntTempArr = new PointF[4];
                float fltTempX = this.m_fltCalXPos(p_dtmRecordDate);
                float fltTempY = this.m_fltYArr[9];
                pntTempArr[0] = new PointF(fltTempX, fltTempY);
                fltTempX = pntTempArr[0].X + 4f * this.fltColWidth;
                pntTempArr[1] = new PointF(fltTempX, fltTempY);
                fltTempY = this.m_fltYArr[2];
                pntTempArr[3] = new PointF(fltTempX, fltTempY);
                fltTempX = pntTempArr[3].X + 4f * this.fltColWidth;
                pntTempArr[2] = new PointF(fltTempX, fltTempY);

                Pen p = new Pen(Color.Gray);
                p.Width = 2;

                this.gpsPainter.DrawLine(p, pntTempArr[0], pntTempArr[3]);
                this.gpsPainter.DrawLine(p, pntTempArr[1], pntTempArr[2]);
            }
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 初始化默认值
        /// </summary>
        private void m_mthInitDefault()
        {
            this.fltSizePerUnit = 10.5f;//默认10.5
            this.intColNumber = 25; // 2019-01-09 由27列改为25列（产程时间25,26不再显示）  27;//27列
            this.fltLeftOffset = 5;//左右边距
            this.fltTopOffset = 5;//上下边距
            this.fltBorderWidth = 2;
        }
        /// <summary>
        /// 初始化表格坐标
        /// </summary>
        private void m_mthInitGrid()
        {
            m_fltXArr = new float[this.intColNumber + 4];
            m_fltXArr[0] = this.fltLeftOffset;
            m_fltXArr[1] = m_fltXArr[0] + 1.2f * this.fltColWidth;
            m_fltXArr[2] = m_fltXArr[1] + 1.2f * this.fltColWidth;
            m_fltXArr[3] = m_fltXArr[2] + 1.2f * this.fltColWidth;
            for (int intI = 0; intI < this.intColNumber; intI++)
            {
                m_fltXArr[intI + 4] = m_fltXArr[intI + 3] + this.fltColWidth;
            }

            m_fltYArr = new float[16];
            m_fltYArr[0] = this.fltTopOffset + this.fltHeaderHeight;
            m_fltYArr[1] = m_fltYArr[0] + this.fltRowHeight;
            m_fltYArr[2] = m_fltYArr[1] + 2f * this.fltRowHeight;
            for (int intI = 0; intI < 10; intI++)
            {
                m_fltYArr[intI + 3] = (m_fltYArr[intI + 2] + this.fltRowHeight);
            }

            float fltFontGS = -3f;
            float fltFontXY = -3f;
            float fltFontQM = -2.5f;

            //处理宫缩，血压，签名数据
            if (this.dtmLabourtime.HasValue)//临产时间是其它数据表的基准，不能为空
            {
                if (partogramData.lstPartogramData != null && partogramData.lstPartogramData.Count > 0)
                {
                    DateTime? dtmRecordDate = null;
                    float fltXPos = float.NaN;
                    int intColIndex = 0;
                    PointF pntPos = PointF.Empty;
                    clsSpecialText objSpecialText = null;
                    string strText = string.Empty;
                    lstSpecialText = new List<clsSpecialText>();
                    foreach (EntityEmrPartogramData partData in partogramData.lstPartogramData)
                    {
                        dtmRecordDate = partData.recordDate;
                        if (dtmRecordDate != null)
                        {
                            fltXPos = this.m_fltCalXPos(dtmRecordDate.Value);
                            /* 2019-01-22 不再显示： 宫缩/血压
                            if (partData.measureType == 4)//宫缩
                            {
                                intColIndex = this.m_intCalColIndex(fltXPos);
                                pntPos = new PointF(fltXPos, 0);//位置后面还要计算
                                strText = partData.measureValue + "\"/" + partData.measureValue2 + "'";
                                objSpecialText = new clsSpecialText(this, pntPos, strText, dtmRecordDate.ToString(), "宫缩", Color.Blue,
                                    false, fltFontGS, FontStyle.Regular, intColIndex, 1);
                                objSpecialText.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                lstSpecialText.Add(objSpecialText);
                            }
                            else if (partData.measureType == 5)//血压
                            {
                                intColIndex = this.m_intCalColIndex(fltXPos);
                                pntPos = new PointF(fltXPos, 0);//位置后面还要计算
                                strText = partData.measureValue + "/" + partData.measureValue2;
                                objSpecialText = new clsSpecialText(this, pntPos, strText, dtmRecordDate.ToString(), "血压", Color.Blue,
                                    false, fltFontXY, FontStyle.Regular, intColIndex, 2);
                                objSpecialText.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                lstSpecialText.Add(objSpecialText);
                            }
                            else */
                            if (partData.measureType == 6)//签名
                            {
                                intColIndex = this.m_intCalColIndex(fltXPos);
                                pntPos = new PointF(fltXPos, 0);//位置后面还要计算
                                strText = partData.measureValue;
                                objSpecialText = new clsSpecialText(this, pntPos, strText, dtmRecordDate.ToString(), "签名", Color.Blue,
                                    true, fltFontQM, FontStyle.Regular, intColIndex, 3);
                                objSpecialText.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                lstSpecialText.Add(objSpecialText);
                            }
                        }
                    }
                }
            }

            int intMaxGS = 0;//宫缩一列中数据最大值
            int intMaxXY = 0;//血压最大值
            int intMaxQM = 0;//签名最大值
            if (lstSpecialText != null && lstSpecialText.Count > 0)
            {
                List<clsSpecialText> lstTemp = null;
                for (int intI = 0; intI < this.intColNumber; intI++)
                {
                    //宫缩
                    lstTemp = lstSpecialText.FindAll(t => t.intTextType == 1 && t.intColIndex == intI);
                    if (lstTemp.Count > intMaxGS)
                    {
                        intMaxGS = lstTemp.Count;
                    }
                    //血压
                    lstTemp = lstSpecialText.FindAll(t => t.intTextType == 2 && t.intColIndex == intI);
                    if (lstTemp.Count > intMaxXY)
                    {
                        intMaxXY = lstTemp.Count;
                    }
                    //签名
                    lstTemp = lstSpecialText.FindAll(t => t.intTextType == 3 && t.intColIndex == intI);
                    if (lstTemp.Count > intMaxQM)
                    {
                        intMaxQM = lstTemp.Count;
                    }
                }
            }
            m_fltYArr[13] = m_fltYArr[12];
            m_fltYArr[14] = m_fltYArr[12];
            /* 2019-01-22  不再显示： 宫缩/血压
            float fltGSHeight = this.fltRowHeight / 3f;//宫缩正常情况是一个行高画三个
            m_fltYArr[13] = m_fltYArr[12] + Math.Max(this.fltRowHeight, fltGSHeight * intMaxGS);
            float fltXYHeight = this.fltRowHeight / 3f;//血压正常情况是一个行高画三个
            m_fltYArr[14] = m_fltYArr[13] + Math.Max(this.fltRowHeight, fltXYHeight * intMaxXY); */

            float fltQMHeight = this.fltRowHeight;//签名正常情况是一个行高画一个
            int intTemp1 = 0;
            int intTemp2 = 0;
            intTemp1 = Math.DivRem(intMaxQM, 2, out intTemp2);//签名是一个行高并排画两个
            m_fltYArr[15] = m_fltYArr[14] + Math.Max(2f * this.fltRowHeight, (intTemp1 + intTemp2) * this.fltRowHeight) + 10f;

            //确定了最后三个Y坐标后重新计算宫缩，血压，签名的坐标
            if (lstSpecialText != null && lstSpecialText.Count > 0)
            {
                List<clsSpecialText> lstTemp = null;
                PointF pntNewPos = PointF.Empty;
                float fltNewX = float.NaN;
                SizeF sizTemp = SizeF.Empty;
                int intTemp = 0;
                for (int intI = 0; intI < this.intColNumber; intI++)
                {
                    /* 2019-01-22  不再显示： 宫缩/血压
                    //宫缩
                    lstTemp = lstSpecialText.FindAll(t => t.intTextType == 1 && t.intColIndex == intI);
                    if (lstTemp != null && lstTemp.Count > 0)
                    {
                        lstTemp.Sort(s_intOrderShape);
                        int intTempCount = lstTemp.Count;
                        if (intTempCount == 1)
                        {
                            clsSpecialText objCurrent = lstTemp[0];
                            sizTemp = this.m_sizMeasureString(objCurrent.strText, fltFontGS, FontStyle.Regular);
                            fltNewX = m_fltXArr[intI + 3] + (this.fltColWidth - sizTemp.Width) / 2f;
                            pntNewPos = new PointF(fltNewX, m_fltYArr[12] + 0.4f * this.m_fltRowHeight);
                            objCurrent.pntPos = pntNewPos;
                        }
                        else if (intTempCount == 2)
                        {
                            clsSpecialText objCurrent = lstTemp[0];
                            sizTemp = this.m_sizMeasureString(objCurrent.strText, fltFontGS, FontStyle.Regular);
                            fltNewX = m_fltXArr[intI + 3] + (this.fltColWidth - sizTemp.Width) / 2f;
                            pntNewPos = new PointF(fltNewX, m_fltYArr[12] + 0.2f * this.m_fltRowHeight);
                            objCurrent.pntPos = pntNewPos;
                            objCurrent = lstTemp[1];
                            sizTemp = this.m_sizMeasureString(objCurrent.strText, fltFontGS, FontStyle.Regular);
                            fltNewX = m_fltXArr[intI + 3] + (this.fltColWidth - sizTemp.Width) / 2f;
                            pntNewPos = new PointF(fltNewX, m_fltYArr[12] + 0.3f * this.m_fltRowHeight + fltGSHeight);
                            objCurrent.pntPos = pntNewPos;
                        }
                        else
                        {
                            intTemp = 0;
                            foreach (clsSpecialText objCurrent in lstTemp)
                            {
                                sizTemp = this.m_sizMeasureString(objCurrent.strText, fltFontGS, FontStyle.Regular);
                                fltNewX = m_fltXArr[intI + 3] + (this.fltColWidth - sizTemp.Width) / 2f;
                                pntNewPos = new PointF(fltNewX, m_fltYArr[12] + 0.001f * intTemp * this.m_fltRowHeight + fltGSHeight * intTemp);
                                objCurrent.pntPos = pntNewPos;
                                intTemp++;
                            }
                        }
                    }
                    //血压
                    lstTemp = lstSpecialText.FindAll(t => t.intTextType == 2 && t.intColIndex == intI);
                    if (lstTemp != null && lstTemp.Count > 0)
                    {
                        lstTemp.Sort(s_intOrderShape);
                        int intTempCount = lstTemp.Count;
                        if (intTempCount == 1)
                        {
                            clsSpecialText objCurrent = lstTemp[0];
                            sizTemp = this.m_sizMeasureString(objCurrent.strText, fltFontXY, FontStyle.Regular);
                            fltNewX = m_fltXArr[intI + 3] + (this.fltColWidth - sizTemp.Width) / 2f;
                            pntNewPos = new PointF(fltNewX, m_fltYArr[13] + 0.4f * this.m_fltRowHeight);
                            objCurrent.pntPos = pntNewPos;
                        }
                        else if (intTempCount == 2)
                        {
                            clsSpecialText objCurrent = lstTemp[0];
                            sizTemp = this.m_sizMeasureString(objCurrent.strText, fltFontXY, FontStyle.Regular);
                            fltNewX = m_fltXArr[intI + 3] + (this.fltColWidth - sizTemp.Width) / 2f;
                            pntNewPos = new PointF(fltNewX, m_fltYArr[13] + 0.2f * this.m_fltRowHeight);
                            objCurrent.pntPos = pntNewPos;
                            objCurrent = lstTemp[1];
                            sizTemp = this.m_sizMeasureString(objCurrent.strText, fltFontXY, FontStyle.Regular);
                            fltNewX = m_fltXArr[intI + 3] + (this.fltColWidth - sizTemp.Width) / 2f;
                            pntNewPos = new PointF(fltNewX, m_fltYArr[13] + 0.3f * this.m_fltRowHeight + fltGSHeight);
                            objCurrent.pntPos = pntNewPos;
                        }
                        else
                        {
                            intTemp = 0;
                            foreach (clsSpecialText objCurrent in lstTemp)
                            {
                                sizTemp = this.m_sizMeasureString(objCurrent.strText, fltFontXY, FontStyle.Regular);
                                fltNewX = m_fltXArr[intI + 3] + (this.fltColWidth - sizTemp.Width) / 2f;
                                pntNewPos = new PointF(fltNewX, m_fltYArr[13] + 0.001f * intTemp * this.m_fltRowHeight + fltXYHeight * intTemp);
                                objCurrent.pntPos = pntNewPos;
                                intTemp++;
                            }
                        }
                    } */
                    //签名
                    lstTemp = lstSpecialText.FindAll(t => t.intTextType == 3 && t.intColIndex == intI);
                    if (lstTemp != null && lstTemp.Count > 0)
                    {
                        lstTemp.Sort(s_intOrderShape);
                        int intTempCount = lstTemp.Count;
                        if (intTempCount == 1)//只有一个签名
                        {
                            clsSpecialText objCurrent = lstTemp[0];
                            fltNewX = m_fltXArr[intI + 3] + 0.3f * this.fltColWidth;
                            pntNewPos = new PointF(fltNewX, m_fltYArr[14] + 0.1f * (m_fltYArr[15] - m_fltYArr[14]));
                            objCurrent.pntPos = pntNewPos;
                            objCurrent.fltFont = 0;
                        }
                        else if (intTempCount == 2)//只有两个签名
                        {
                            clsSpecialText objCurrent = lstTemp[0];
                            fltNewX = m_fltXArr[intI + 3] + 0.01f * this.fltColWidth;
                            pntNewPos = new PointF(fltNewX, m_fltYArr[14] + 0.1f * (m_fltYArr[15] - m_fltYArr[14]));
                            objCurrent.pntPos = pntNewPos;
                            objCurrent.fltFont = 0;

                            objCurrent = lstTemp[1];
                            fltNewX = m_fltXArr[intI + 3] + this.fltColWidth / 2f + 0.01f * this.fltColWidth;
                            pntNewPos = new PointF(fltNewX, m_fltYArr[14] + 0.1f * (m_fltYArr[15] - m_fltYArr[14]));
                            objCurrent.pntPos = pntNewPos;
                            objCurrent.fltFont = 0;
                        }
                        else
                        {
                            intTemp = 0;
                            int intRemain = 0;
                            int intDiv = 0;
                            foreach (clsSpecialText objCurrent in lstTemp)
                            {
                                intDiv = Math.DivRem(intTemp, 2, out intRemain);
                                if (intRemain == 0)//奇数次
                                {
                                    fltNewX = m_fltXArr[intI + 3] + 0.01f * this.fltColWidth;
                                }
                                else
                                {
                                    fltNewX = m_fltXArr[intI + 3] + this.fltColWidth / 2f + 0.01f * this.fltColWidth;
                                }
                                pntNewPos = new PointF(fltNewX, m_fltYArr[14] + (intDiv - 0.01f) * fltQMHeight);
                                objCurrent.pntPos = pntNewPos;
                                intTemp++;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void m_mthInitData()
        {
            lstShapeData = new List<clsShape>();//先清空之前画图数据
            if (partogramData != null && partogramData.partogramMain != null)
            {
                clsShape objTemp = null;

                #region 基本信息
                if (!string.IsNullOrEmpty(partogramData.partogramMain.bedNo))
                {
                    objTemp = new clsText(this, m_pntPosArr[0], partogramData.partogramMain.bedNo,
                        string.Empty, "床号", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    lstShapeData.Add(objTemp);
                }
                if (!string.IsNullOrEmpty(partogramData.partogramMain.patName))
                {
                    objTemp = new clsText(this, m_pntPosArr[1], partogramData.partogramMain.patName,
                        string.Empty, "姓名", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    lstShapeData.Add(objTemp);
                }
                if (!string.IsNullOrEmpty(partogramData.partogramMain.patAge))
                {
                    objTemp = new clsText(this, m_pntPosArr[2], partogramData.partogramMain.patAge,
                        string.Empty, "年龄", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    lstShapeData.Add(objTemp);
                }
                if (!string.IsNullOrEmpty(partogramData.partogramMain.pregnantTimes))
                {
                    objTemp = new clsText(this, m_pntPosArr[3], partogramData.partogramMain.pregnantTimes,
                        string.Empty, "孕次", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    objTemp.objCurrentData = partogramData.partogramMain;//双击时要处理，所以要加上对应数据
                    lstShapeData.Add(objTemp);
                }
                if (!string.IsNullOrEmpty(partogramData.partogramMain.labourTimes))
                {
                    objTemp = new clsText(this, m_pntPosArr[4], partogramData.partogramMain.labourTimes,
                        string.Empty, "产次", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    objTemp.objCurrentData = partogramData.partogramMain;//双击时要处理，所以要加上对应数据
                    lstShapeData.Add(objTemp);
                }
                if (!string.IsNullOrEmpty(partogramData.partogramMain.pregnantWeeks))
                {
                    if (!string.IsNullOrEmpty(partogramData.partogramMain.pregnantDays))
                    {
                        objTemp = new clsText(this, m_pntPosArr[5] + new SizeF(-8, 0), partogramData.partogramMain.pregnantWeeks + "周" + partogramData.partogramMain.pregnantDays + "天",
                            string.Empty, "孕周", Color.Black, false, 0f, FontStyle.Regular, partogramData.partogramMain.pregnantWeeks);
                        objTemp.objCurrentData = partogramData.partogramMain;//双击时要处理，所以要加上对应数据
                        lstShapeData.Add(objTemp);

                        objTemp = new clsText(this, m_pntPosArr[5] + new SizeF(7, -5), partogramData.partogramMain.pregnantWeeks + "周" + partogramData.partogramMain.pregnantDays + "天",
                            string.Empty, "孕周", Color.Black, false, 0f, FontStyle.Regular, "+" + partogramData.partogramMain.pregnantDays);
                        objTemp.objCurrentData = partogramData.partogramMain;//双击时要处理，所以要加上对应数据
                        lstShapeData.Add(objTemp);
                    }
                    else
                    {
                        objTemp = new clsText(this, m_pntPosArr[5], partogramData.partogramMain.pregnantWeeks,
                            string.Empty, "孕周", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                        objTemp.objCurrentData = partogramData.partogramMain;//双击时要处理，所以要加上对应数据
                        lstShapeData.Add(objTemp);
                    }
                }
                if (partogramData.partogramMain.predestinateDate != null)
                {
                    objTemp = new clsText(this, m_pntPosArr[6], partogramData.partogramMain.predestinateDate.Value.ToString("yyyy-MM-dd"),
                        string.Empty, "预产期", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    objTemp.objCurrentData = partogramData.partogramMain;//双击时要处理，所以要加上对应数据
                    lstShapeData.Add(objTemp);
                }
                if (!string.IsNullOrEmpty(partogramData.partogramMain.ipNo))
                {
                    objTemp = new clsText(this, m_pntPosArr[7], partogramData.partogramMain.ipNo,
                        string.Empty, "住院号/ID号", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    lstShapeData.Add(objTemp);
                }
                if (!string.IsNullOrEmpty(partogramData.partogramMain.interspinal))
                {
                    objTemp = new clsText(this, m_pntPosArr[8], partogramData.partogramMain.interspinal,
                        string.Empty, "髂棘间径", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    objTemp.objCurrentData = partogramData.partogramMain;//双击时要处理，所以要加上对应数据
                    lstShapeData.Add(objTemp);
                }
                if (!string.IsNullOrEmpty(partogramData.partogramMain.iliacAcrest))
                {
                    objTemp = new clsText(this, m_pntPosArr[9], partogramData.partogramMain.iliacAcrest,
                        string.Empty, "髂脊间径", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    objTemp.objCurrentData = partogramData.partogramMain;//双击时要处理，所以要加上对应数据
                    lstShapeData.Add(objTemp);
                }
                if (!string.IsNullOrEmpty(partogramData.partogramMain.conJugate))
                {
                    objTemp = new clsText(this, m_pntPosArr[10], partogramData.partogramMain.conJugate,
                        string.Empty, "骶耻外径", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    objTemp.objCurrentData = partogramData.partogramMain;//双击时要处理，所以要加上对应数据
                    lstShapeData.Add(objTemp);
                }
                if (!string.IsNullOrEmpty(partogramData.partogramMain.ischium))
                {
                    objTemp = new clsText(this, m_pntPosArr[11], partogramData.partogramMain.ischium,
                        string.Empty, "坐骨结节", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    objTemp.objCurrentData = partogramData.partogramMain;//双击时要处理，所以要加上对应数据
                    lstShapeData.Add(objTemp);
                }
                if (this.dtmLabourtime != null)
                {
                    objTemp = new clsText(this, m_pntPosArr[12], this.dtmLabourtime.ToString(),
                        string.Empty, "临产时间", Color.Black, false, 0f, FontStyle.Regular, string.Empty);
                    objTemp.objCurrentData = partogramData.partogramMain;//双击时要处理，所以要加上对应数据
                    lstShapeData.Add(objTemp);
                }
                #endregion
                #region 数据信息
                /* partData.measureType  1-先露下降,2-胎心率,3-宫口扩张,4-宫缩,5-血压,6-签名
                 * partData.eventType 1-宫口开全,2-胎儿娩出,3-剖宫手术,4-取出胎儿
                 */
                if (this.dtmLabourtime.HasValue)//临产时间是其它数据表的基准，不能为空
                {
                    if (partogramData.lstPartogramData != null && partogramData.lstPartogramData.Count > 0)
                    {
                        DateTime? dtmRecordDate = null;
                        clsCircle objTempCircle = null;//取出胎儿时的Circle
                        clsText objTempText = null;//取出胎儿时的Text
                        clsArrow objTempArrow = null;//取出胎儿时的Arrow

                        foreach (EntityEmrPartogramData partData in partogramData.lstPartogramData)
                        {
                            dtmRecordDate = partData.recordDate;
                            if (partData.measureType == 1)//先露下降
                            {
                                if (dtmRecordDate != null)
                                {
                                    PointF pntPos = new Point();
                                    pntPos.X = this.m_fltCalXPos(dtmRecordDate.Value);
                                    pntPos.Y = this.m_fltCalYPos(partData.measureType, partData.measureValue);
                                    clsCrox objCrox = new clsCrox(this, pntPos, Color.Blue, partData.measureValue,
                                        dtmRecordDate.ToString(), "先露下降");
                                    objCrox.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                    lstShapeData.Add(objCrox);
                                }
                            }
                            else if (partData.measureType == 2)//胎心率
                            {
                                if (dtmRecordDate != null)
                                {
                                    PointF pntPos = new Point();
                                    pntPos.X = this.m_fltCalXPos(dtmRecordDate.Value);
                                    pntPos.Y = this.m_fltCalYPos(partData.measureType, partData.measureValue);
                                    clsPoint objPoint = new clsPoint(this, pntPos, Color.Blue, partData.measureValue,
                                        dtmRecordDate.ToString(), "胎心率");
                                    objPoint.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                    lstShapeData.Add(objPoint);
                                }
                            }
                            else if (partData.measureType == 3)//宫口扩张
                            {
                                if (dtmRecordDate != null)
                                {
                                    if (partData.eventType == 1)//宫口开全
                                    {
                                        PointF pntPos = new Point();
                                        pntPos.X = this.m_fltCalXPos(dtmRecordDate.Value);
                                        pntPos.Y = this.m_fltCalYPos(partData.measureType, partData.measureValue);
                                        clsCircle objCircle = new clsCircle(this, pntPos, partData.measureValue, dtmRecordDate.ToString(), "宫口扩张",
                                            Color.Red, true, -1f, FontStyle.Regular, partData.eventType.Value);
                                        objCircle.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                        lstShapeData.Add(objCircle);

                                        pntPos.X = pntPos.X - 0.5f * this.fltColWidth;
                                        pntPos.Y = pntPos.Y - 0.6f * this.fltRowHeight;
                                        string strTemp = dtmRecordDate.Value.ToString("HH:mm") + " 宫口开全";
                                        clsText objText = new clsText(this, pntPos, strTemp, dtmRecordDate.ToString(), "宫口扩张", Color.Red, false, -1, FontStyle.Regular, string.Empty);
                                        objText.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                        lstShapeData.Add(objText);
                                    }
                                    else if (partData.eventType == 2)//胎儿娩出
                                    {
                                        PointF pntPos = new Point();
                                        pntPos.X = this.m_fltCalXPos(dtmRecordDate.Value);
                                        pntPos.Y = this.m_fltCalYPos(partData.measureType, partData.measureValue);
                                        clsCircle objCircle = new clsCircle(this, pntPos, partData.measureValue, dtmRecordDate.ToString(), "宫口扩张",
                                            Color.Red, true, -1f, FontStyle.Regular, partData.eventType.Value);
                                        objCircle.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                        lstShapeData.Add(objCircle);

                                        PointF pntTemp = pntPos;
                                        pntTemp.X = pntTemp.X + 0.4f * this.fltColWidth;
                                        string strTemp = dtmRecordDate.Value.ToString("HH:mm") + " 胎儿娩出";
                                        clsText objText = new clsText(this, pntTemp, strTemp, dtmRecordDate.ToString(), "宫口扩张", Color.Red, false, -1, FontStyle.Regular, string.Empty);
                                        objText.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                        lstShapeData.Add(objText);

                                        clsArrow objArrow = new clsArrow(this, pntPos, new PointF(pntPos.X, pntPos.Y + 0.6f * this.fltRowHeight), Color.Red);
                                        lstShapeData.Add(objArrow);
                                    }
                                    else if (partData.eventType == 3)//剖宫手术
                                    {
                                        PointF pntPos = new Point();
                                        pntPos.X = this.m_fltCalXPos(dtmRecordDate.Value);
                                        pntPos.Y = this.m_fltCalYPos(partData.measureType, partData.measureValue);
                                        clsCircle objCircle = new clsCircle(this, pntPos, partData.measureValue, dtmRecordDate.ToString(), "宫口扩张",
                                            Color.Red, true, -1f, FontStyle.Regular, partData.eventType.Value);
                                        objCircle.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                        lstShapeData.Add(objCircle);

                                        pntPos.X = pntPos.X - 0.5f * this.fltColWidth;
                                        pntPos.Y = pntPos.Y - 0.6f * this.fltRowHeight;
                                        string strTemp = dtmRecordDate.Value.ToString("HH:mm") + " 剖宫手术";
                                        clsText objText = new clsText(this, pntPos, strTemp, dtmRecordDate.ToString(), "宫口扩张", Color.Red, false, -1, FontStyle.Regular, string.Empty);
                                        objText.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                        lstShapeData.Add(objText);
                                    }
                                    else if (partData.eventType == 4)//取出胎儿
                                    {
                                        PointF pntPos = new Point();
                                        pntPos.X = this.m_fltCalXPos(dtmRecordDate.Value);
                                        pntPos.Y = this.m_fltCalYPos(partData.measureType, partData.measureValue);
                                        clsCircle objCircle = new clsCircle(this, pntPos, partData.measureValue, dtmRecordDate.ToString(), "宫口扩张",
                                            Color.Red, true, -1f, FontStyle.Regular, partData.eventType.Value);
                                        objCircle.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                        lstShapeData.Add(objCircle);
                                        objTempCircle = objCircle;

                                        PointF pntTemp = pntPos;
                                        pntTemp.X = pntTemp.X + 0.4f * this.fltColWidth;
                                        string strTemp = dtmRecordDate.Value.ToString("HH:mm") + " 取出胎儿";
                                        clsText objText = new clsText(this, pntTemp, strTemp, dtmRecordDate.ToString(), "宫口扩张", Color.Red, false, -1, FontStyle.Regular, string.Empty);
                                        objText.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                        lstShapeData.Add(objText);
                                        objTempText = objText;

                                        clsArrow objArrow = new clsArrow(this, pntPos, new PointF(pntPos.X, pntPos.Y + 0.6f * this.fltRowHeight), Color.Red);
                                        lstShapeData.Add(objArrow);
                                        objTempArrow = objArrow;
                                    }
                                    else
                                    {
                                        PointF pntPos = new Point();
                                        pntPos.X = this.m_fltCalXPos(dtmRecordDate.Value);
                                        pntPos.Y = this.m_fltCalYPos(partData.measureType, partData.measureValue);
                                        clsCircle objCircle = new clsCircle(this, pntPos, partData.measureValue, dtmRecordDate.ToString(), "宫口扩张",
                                            Color.Red, true, -1f, FontStyle.Regular, 0);
                                        objCircle.objCurrentData = partData;//双击时要处理，所以要加上对应数据
                                        lstShapeData.Add(objCircle);
                                    }
                                }
                            }
                            //宫缩,血压,签名这三个类型的数据在m_mthInitGrid时已处理，因为要动态计算行高
                        }
                        List<clsShape> lstCircleShape = lstShapeData.FindAll(t => t.strTypeName == "clsCircle");//找出所有的圈连起来
                        if (lstCircleShape != null && lstCircleShape.Count > 0)
                        {
                            lstCircleShape.Sort(s_intOrderShape);
                            int intCount = lstCircleShape.Count - 1;
                            clsLine objTempLine = null;
                            for (int intI = 0; intI < intCount; intI++)
                            {
                                if (((clsCircle)lstCircleShape[intI]).decEventType == 3 || ((clsCircle)lstCircleShape[intI + 1]).decEventType == 4)
                                {
                                    if (objTempCircle != null)
                                    {
                                        objTempCircle.pntPos = new PointF(objTempCircle.pntPos.X, lstCircleShape[intI].pntPos.Y);
                                        if (objTempArrow != null)
                                        {
                                            objTempArrow.pntStart = objTempCircle.pntPos;
                                            objTempArrow.pntEnd = new PointF(objTempCircle.pntPos.X, objTempCircle.pntPos.Y + 0.6f * this.fltRowHeight);
                                        }
                                        if (objTempText != null)
                                        {
                                            objTempText.pntPos = new PointF(objTempCircle.pntPos.X + 0.4f * this.fltColWidth, objTempCircle.pntPos.Y);
                                        }
                                    }
                                    objTempLine = new clsLine(this, lstCircleShape[intI].pntPos, lstCircleShape[intI + 1].pntPos, Color.Red, DashStyle.Dot);
                                }
                                else
                                {
                                    objTempLine = new clsLine(this, lstCircleShape[intI].pntPos, lstCircleShape[intI + 1].pntPos, Color.Red, DashStyle.Solid);
                                }
                                lstShapeData.Add(objTempLine);
                            }
                        }
                        List<clsShape> lstPointShape = lstShapeData.FindAll(t => t.strTypeName == "clsPoint");//找出所有的点连起来
                        if (lstPointShape != null && lstPointShape.Count > 0)
                        {
                            lstPointShape.Sort(s_intOrderShape);
                            int intCount = lstPointShape.Count - 1;
                            clsLine objTempLine = null;
                            for (int intI = 0; intI < intCount; intI++)
                            {
                                objTempLine = new clsLine(this, lstPointShape[intI].pntPos, lstPointShape[intI + 1].pntPos, Color.Blue, DashStyle.Solid);
                                lstShapeData.Add(objTempLine);
                            }
                        }
                        List<clsShape> lstCroxShape = lstShapeData.FindAll(t => t.strTypeName == "clsCrox");//找出所有的叉连起来
                        if (lstCroxShape != null && lstCroxShape.Count > 0)
                        {
                            lstCroxShape.Sort(s_intOrderShape);
                            int intCount = lstCroxShape.Count - 1;
                            clsLine objTempLine = null;
                            for (int intI = 0; intI < intCount; intI++)
                            {
                                objTempLine = new clsLine(this, lstCroxShape[intI].pntPos, lstCroxShape[intI + 1].pntPos, Color.Blue, DashStyle.Solid);
                                lstShapeData.Add(objTempLine);
                            }
                        }

                        //加上宫缩，血压，签名数据
                        if (lstSpecialText != null && lstSpecialText.Count > 0)
                        {
                            lstShapeData.AddRange(lstSpecialText.ToArray());
                        }
                    }
                }
                #endregion
            }
            //加上时间轴数据
            if (lstText != null && lstText.Count > 0)
            {
                lstShapeData.AddRange(lstText.ToArray());
            }
        }

        /// <summary>
        /// 比较两个图形的先后顺序
        /// </summary>
        /// <param name="p_objA"></param>
        /// <param name="p_objB"></param>
        /// <returns></returns>
        private static int s_intOrderShape(clsShape p_objA, clsShape p_objB)
        {
            return p_objA.pntPos.X.CompareTo(p_objB.pntPos.X);
        }
        /// <summary>
        /// 根据X坐标计算位置在第几列(以数字0为起点)
        /// </summary>
        /// <param name="p_fltXPos"></param>
        /// <returns></returns>
        private int m_intCalColIndex(float p_fltXPos)
        {
            int intResult = 0;
            for (int intI = 0; intI < this.intColNumber; intI++)
            {
                if (m_fltXArr[intI + 3] > p_fltXPos)
                {
                    break;
                }
                intResult = intI;
            }
            return intResult;
        }
        /// <summary>
        /// 计算X坐标
        /// </summary>
        /// <returns></returns>
        private float m_fltCalXPos(DateTime p_dtmRecordDate)
        {
            float fltResult = float.NaN;
            if (this.dtmLabourtime != null)
            {
                TimeSpan tspTemp = p_dtmRecordDate - this.dtmLabourtime.Value;
                int intHours = tspTemp.Hours;
                int intMinutes = tspTemp.Minutes;
                float fltStart = float.NaN;
                if (intHours <= 0)
                {
                    fltStart = m_fltXArr[3];
                }
                else if (intHours >= this.intColNumber)
                {
                    fltStart = m_fltXArr[3 + this.intColNumber];
                }
                else
                {
                    fltStart = m_fltXArr[3 + intHours];
                }

                float fltWidth = (this.fltColWidth / 60f) * intMinutes;
                fltResult = fltStart + fltWidth;
            }
            return fltResult;
        }
        /// <summary>
        /// 计算Y坐标
        /// </summary>
        /// <param name="p_intType">类型1-先露下降,2-胎心率,3-宫口扩张,4-宫缩,5-血压,6-签名</param>
        /// <returns></returns>
        private float m_fltCalYPos(decimal p_decType, string strValue)
        {
            float fltResult = float.NaN;
            decimal decValue = decimal.Zero;
            if (decimal.TryParse(strValue, out decValue))
            {
                if (p_decType == 1)
                {
                    if (decValue >= -5 && decValue <= 5)
                    {
                        decimal decTemp = 5 - decValue;
                        int intWhole = (int)decimal.Truncate(decTemp);//整数部分
                        float fltPoint = (float)(decTemp - intWhole);
                        float fltStart = m_fltYArr[2 + intWhole];
                        float fltHeight = this.fltRowHeight * fltPoint;
                        fltResult = fltStart + fltHeight;
                    }
                    else
                    {
                        decimal decTemp = 0;
                        int intWhole = (int)decimal.Truncate(decTemp);//整数部分
                        float fltPoint = (float)(decTemp - intWhole);
                        float fltStart = m_fltYArr[2 + intWhole];
                        float fltHeight = this.fltRowHeight * fltPoint;
                        fltResult = fltStart + fltHeight;
                    }
                }
                else if (p_decType == 2)
                {
                    if (decValue >= 80 && decValue <= 180)
                    {
                        decimal decTemp = (180 - decValue) / 10;
                        int intWhole = (int)decimal.Truncate(decTemp);//整数部分
                        float fltPoint = (float)(decTemp - intWhole);
                        float fltStart = m_fltYArr[2 + intWhole];
                        float fltHeight = this.fltRowHeight * fltPoint;
                        fltResult = fltStart + fltHeight;
                    }
                    else
                    {
                        decimal decTemp = 0;
                        int intWhole = (int)decimal.Truncate(decTemp);//整数部分
                        float fltPoint = (float)(decTemp - intWhole);
                        float fltStart = m_fltYArr[2 + intWhole];
                        float fltHeight = this.fltRowHeight * fltPoint;
                        fltResult = fltStart + fltHeight;
                    }
                }
                else if (p_decType == 3)
                {
                    if (decValue >= 0 && decValue <= 10)
                    {
                        decimal decTemp = 10 - decValue;
                        int intWhole = (int)decimal.Truncate(decTemp);//整数部分
                        float fltPoint = (float)(decTemp - intWhole);
                        float fltStart = m_fltYArr[2 + intWhole];
                        float fltHeight = this.fltRowHeight * fltPoint;
                        fltResult = fltStart + fltHeight;
                    }
                    else
                    {
                        decimal decTemp = 0;
                        int intWhole = (int)decimal.Truncate(decTemp);//整数部分
                        float fltPoint = (float)(decTemp - intWhole);
                        float fltStart = m_fltYArr[2 + intWhole];
                        float fltHeight = this.fltRowHeight * fltPoint;
                        fltResult = fltStart + fltHeight;
                    }
                }
            }
            return fltResult;
        }

        /// <summary>
        /// 画固定内容
        /// </summary>
        private void m_mthDrawBackGround()
        {
            //画标题
            this.m_mthDrawTitle();
            //画表头
            this.m_mthDrawHeader();
            //画表格
            this.m_mthDrawGrid();
            //画页脚
            this.m_mthDrawFoot();
        }
        /// <summary>
        /// 画标题
        /// </summary>
        private void m_mthDrawTitle()
        {
            string strText = string.Empty;
            SizeF sizText = SizeF.Empty;
            if (GlobalHospital.objHospital != null)
            {
                strText = GlobalHospital.objHospital.Hospitalname;
            }
            sizText = this.m_sizMeasureString(strText, 4f, FontStyle.Regular);

            float fltX = m_fltXArr[0] + (m_fltXArr[m_fltXArr.Length - 1] - m_fltXArr[0] - sizText.Width) / 2f;
            float fltY = this.fltTopOffset + 8f * this.fltRate;
            this.m_rectDrawText(new PointF(fltX, fltY), strText, Color.Black, false, 4f, FontStyle.Regular);

            strText = "产 程 进 程 图";
            sizText = this.m_sizMeasureString(strText, 8f, FontStyle.Bold);
            fltX = m_fltXArr[0] + (m_fltXArr[m_fltXArr.Length - 1] - m_fltXArr[0] - sizText.Width) / 2f;
            fltY = this.fltTopOffset + 30f * this.fltRate;
            this.m_rectDrawText(new PointF(fltX, fltY), strText, Color.Black, false, 8f, FontStyle.Bold);
        }
        /// <summary>
        /// 画表头
        /// </summary>
        private void m_mthDrawHeader()
        {
            string strText = string.Empty;
            SizeF sizText = SizeF.Empty;
            SizeF sizTemp = SizeF.Empty;
            float fltPosX = float.NaN;
            float fltPosY = float.NaN;
            float fltLineX = float.NaN;
            float fltLineY = float.NaN;
            float fltLineWidth = 16f * this.fltRate;
            Pen pnTemp = new Pen(Color.Black, 1f);

            strText = "床号：　　　　 姓名：　　　　 年龄：　　　　 孕次：　　 产次：　　 孕周：　　周 预产期：　　　　　　 住院号/ID号：　　　　　";
            sizText = this.m_sizMeasureString(strText);
            float fltX = m_fltXArr[0];
            float fltY = this.fltTopOffset + 65f * this.fltRate;
            this.m_rectDrawText(new PointF(fltX, fltY), strText, Color.Black, false);
            fltPosY = this.fltTopOffset + 63f * this.fltRate;
            sizTemp = this.m_sizMeasureString(strText.Substring(0, 3));
            fltPosX = fltX + sizTemp.Width;
            m_pntPosArr[0] = new PointF(fltPosX, fltPosY);

            fltLineX = fltPosX - 13f * this.fltRate;
            fltLineY = fltPosY + 15f * this.fltRate;
            this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 4f * fltLineWidth, fltLineY), pnTemp);

            sizTemp = this.m_sizMeasureString(strText.Substring(0, 11));
            fltPosX = fltX + sizTemp.Width;
            m_pntPosArr[1] = new PointF(fltPosX, fltPosY);

            fltLineX = fltPosX - 13f * this.fltRate;
            fltLineY = fltPosY + 15f * this.fltRate;
            this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 4f * fltLineWidth, fltLineY), pnTemp);

            sizTemp = this.m_sizMeasureString(strText.Substring(0, 19));
            fltPosX = fltX + sizTemp.Width;
            m_pntPosArr[2] = new PointF(fltPosX, fltPosY);

            fltLineX = fltPosX - 13f * this.fltRate;
            fltLineY = fltPosY + 15f * this.fltRate;
            this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 4f * fltLineWidth, fltLineY), pnTemp);

            sizTemp = this.m_sizMeasureString(strText.Substring(0, 27));
            fltPosX = fltX + sizTemp.Width;
            m_pntPosArr[3] = new PointF(fltPosX, fltPosY);

            fltLineX = fltPosX - 13f * this.fltRate;
            fltLineY = fltPosY + 15f * this.fltRate;
            this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 2f * fltLineWidth, fltLineY), pnTemp);

            sizTemp = this.m_sizMeasureString(strText.Substring(0, 33));
            fltPosX = fltX + sizTemp.Width;
            m_pntPosArr[4] = new PointF(fltPosX, fltPosY);

            fltLineX = fltPosX - 13f * this.fltRate;
            fltLineY = fltPosY + 15f * this.fltRate;
            this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 2f * fltLineWidth, fltLineY), pnTemp);

            sizTemp = this.m_sizMeasureString(strText.Substring(0, 39));
            fltPosX = fltX + sizTemp.Width;
            m_pntPosArr[5] = new PointF(fltPosX, fltPosY);

            fltLineX = fltPosX - 13f * this.fltRate;
            fltLineY = fltPosY + 15f * this.fltRate;
            this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 2f * fltLineWidth, fltLineY), pnTemp);

            sizTemp = this.m_sizMeasureString(strText.Substring(0, 47));
            fltPosX = fltX + sizTemp.Width;
            m_pntPosArr[6] = new PointF(fltPosX, fltPosY);

            fltLineX = fltPosX - 13f * this.fltRate;
            fltLineY = fltPosY + 15f * this.fltRate;
            this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 6f * fltLineWidth, fltLineY), pnTemp);

            sizTemp = this.m_sizMeasureString(strText.Substring(0, 62));
            fltPosX = fltX + sizTemp.Width;
            m_pntPosArr[7] = new PointF(fltPosX, fltPosY);

            fltLineX = fltPosX - 13f * this.fltRate;
            fltLineY = fltPosY + 15f * this.fltRate;
            this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 5f * fltLineWidth, fltLineY), pnTemp);


            //strText = "骨盆外测量：髂棘间径：　　　　cm  髂脊间径：　　　　cm  骶耻外径：　　　　cm  坐骨结节：　　　　cm";
            //fltY = this.fltTopOffset + 90f * this.fltRate;
            //this.m_rectDrawText(new PointF(fltX, fltY), strText, Color.Black, false);
            //fltPosY = this.fltTopOffset + 88f * this.fltRate;
            //sizTemp = this.m_sizMeasureString(strText.Substring(0, 11));
            //fltPosX = fltX + sizTemp.Width;
            //m_pntPosArr[8] = new PointF(fltPosX, fltPosY);

            //fltLineX = fltPosX - 13f * this.fltRate;
            //fltLineY = fltPosY + 15f * this.fltRate;
            //this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 4f * fltLineWidth, fltLineY), pnTemp);

            //sizTemp = this.m_sizMeasureString(strText.Substring(0, 24));
            //fltPosX = fltX + sizTemp.Width;
            //m_pntPosArr[9] = new PointF(fltPosX, fltPosY);

            //fltLineX = fltPosX - 13f * this.fltRate;
            //fltLineY = fltPosY + 15f * this.fltRate;
            //this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 4f * fltLineWidth, fltLineY), pnTemp);

            //sizTemp = this.m_sizMeasureString(strText.Substring(0, 37));
            //fltPosX = fltX + sizTemp.Width;
            //m_pntPosArr[10] = new PointF(fltPosX, fltPosY);

            //fltLineX = fltPosX - 13f * this.fltRate;
            //fltLineY = fltPosY + 15f * this.fltRate;
            //this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 4f * fltLineWidth, fltLineY), pnTemp);

            //sizTemp = this.m_sizMeasureString(strText.Substring(0, 50));
            //fltPosX = fltX + sizTemp.Width;
            //m_pntPosArr[11] = new PointF(fltPosX, fltPosY);

            //fltLineX = fltPosX - 13f * this.fltRate;
            //fltLineY = fltPosY + 15f * this.fltRate;
            //this.m_mthDrawLine(new PointF(fltLineX, fltLineY), new PointF(fltLineX + 4f * fltLineWidth, fltLineY), pnTemp);

        }
        /// <summary>
        /// 画页脚
        /// </summary>
        private void m_mthDrawFoot()
        {
            float fltXEnd = m_fltXArr[m_fltXArr.Length - 1];
            float fltYEnd = m_fltYArr[m_fltYArr.Length - 1];

            this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[0], m_fltYArr[13]), new PointF(fltXEnd, m_fltYArr[13]));
            this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[0], m_fltYArr[14]), new PointF(fltXEnd, m_fltYArr[14]));
            this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[0], m_fltYArr[15]), new PointF(fltXEnd, m_fltYArr[15]));

            this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[0], m_fltYArr[12]), new PointF(m_fltXArr[0], fltYEnd));
            int intJ = 0;
            for (intJ = 0; intJ < this.intColNumber; intJ++)
            {
                this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[intJ + 3], m_fltYArr[12]), new PointF(m_fltXArr[intJ + 3], fltYEnd));
            }
            this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[intJ + 3], m_fltYArr[12]), new PointF(m_fltXArr[intJ + 3], fltYEnd));

            float fltTempY = m_fltYArr[12] + 0.4f * (m_fltYArr[13] - m_fltYArr[12]);
            /* 2019-01-22  不再显示： 宫缩/血压
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.6f * this.fltColWidth, fltTempY), "宫缩（s/min）", Color.Black, false);
            fltTempY = m_fltYArr[13] + 0.4f * (m_fltYArr[14] - m_fltYArr[13]);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.6f * this.fltColWidth, fltTempY), "血压 （mmHg）", Color.Black, false);
            fltTempY = m_fltYArr[14] + 0.4f * (m_fltYArr[15] - m_fltYArr[14]); */
            fltTempY += 30f;
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 1.3f * this.fltColWidth, fltTempY), "签 名", Color.Black, false);

            string strText = "临产时间：";
            this.m_rectDrawText(new PointF(m_fltXArr[0] + this.fltRate * 5f, m_fltYArr[m_fltYArr.Length - 1] + this.fltRate * 3f), strText, Color.Black, false);
            SizeF sizTemp = this.m_sizMeasureString(strText);
            m_pntPosArr[12] = new PointF(m_fltXArr[0] + this.fltRate * 5f + sizTemp.Width, m_fltYArr[m_fltYArr.Length - 1] + this.fltRate * 3f);
        }
        /// <summary>
        /// 画表格
        /// </summary>
        private void m_mthDrawGrid()
        {
            int intXLen = m_fltXArr.Length;
            int intYLen = m_fltYArr.Length - 3;
            float fltXEnd = m_fltXArr[intXLen - 1];
            float fltYEnd = m_fltYArr[intYLen - 1];
            //横线
            for (int intI = 0; intI < intYLen; intI++)
            {
                this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[0], m_fltYArr[intI]), new PointF(fltXEnd, m_fltYArr[intI]));
                //if (intI == 9)
                //{
                //    this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[0], m_fltYArr[intI] + 1), new PointF(fltXEnd, m_fltYArr[intI] + 1));
                //}
            }

            //竖线
            this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[0], m_fltYArr[0]), new PointF(m_fltXArr[0], fltYEnd));
            this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[1], m_fltYArr[1]), new PointF(m_fltXArr[1], fltYEnd));
            this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[2], m_fltYArr[1]), new PointF(m_fltXArr[2], fltYEnd));

            int intJ = 0;
            clsText objTemp = null;
            string strCurrentTime = string.Empty;
            lstText = new List<clsText>(this.intColNumber);
            for (intJ = 0; intJ < this.intColNumber; intJ++)
            {
                this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[intJ + 3], m_fltYArr[1]), new PointF(m_fltXArr[intJ + 3], fltYEnd));
                if (this.dtmLabourtime != null)
                {
                    strCurrentTime = this.dtmLabourtime.Value.AddHours(intJ).ToString();
                }
                else
                {
                    strCurrentTime = string.Empty;
                }
                objTemp = new clsText(this, new PointF(m_fltXArr[intJ + 3] - 5f * this.fltRate, m_fltYArr[1] - 20f * this.fltRate), intJ.ToString(),
                    strCurrentTime, "时间轴", Color.Black, false, 0, FontStyle.Regular, string.Empty);
                lstText.Add(objTemp);
            }
            this.gpsPainter.DrawLine(Pens.Black, new PointF(m_fltXArr[intJ + 3], m_fltYArr[0]), new PointF(m_fltXArr[intJ + 3], fltYEnd));

            //先露下降
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.05f * this.fltColWidth, m_fltYArr[1] + 0.3f * this.fltRowHeight), "先 露", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.05f * this.fltColWidth, m_fltYArr[1] + 0.7f * this.fltRowHeight), "下 降", Color.Black, false);
            this.m_rectDrawCrox(new PointF(m_fltXArr[0] + 0.6f * this.fltColWidth, m_fltYArr[1] + 1.5f * this.fltRowHeight), Color.Blue);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.3f * this.fltColWidth, m_fltYArr[2] + 0.3f * this.fltRowHeight), "+5", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.3f * this.fltColWidth, m_fltYArr[3] + 0.3f * this.fltRowHeight), "+4", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.3f * this.fltColWidth, m_fltYArr[4] + 0.3f * this.fltRowHeight), "+3", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.3f * this.fltColWidth, m_fltYArr[5] + 0.3f * this.fltRowHeight), "+2", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.3f * this.fltColWidth, m_fltYArr[6] + 0.3f * this.fltRowHeight), "+1", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.3f * this.fltColWidth, m_fltYArr[7] + 0.3f * this.fltRowHeight), " 0", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.3f * this.fltColWidth, m_fltYArr[8] + 0.3f * this.fltRowHeight), "-1", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.3f * this.fltColWidth, m_fltYArr[9] + 0.3f * this.fltRowHeight), "-2", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.3f * this.fltColWidth, m_fltYArr[10] + 0.3f * this.fltRowHeight), "-3", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[0] + 0.3f * this.fltColWidth, m_fltYArr[11] + 0.3f * this.fltRowHeight), "-4", Color.Black, false);

            this.m_rectDrawText(new PointF(m_fltXArr[1] - this.fltColWidth * 0.25f, m_fltYArr[0] + this.fltRowHeight * 0.5f), "产程时间：", Color.Black, false);
            //胎心率
            this.m_rectDrawText(new PointF(m_fltXArr[1] + 0.0001f * this.fltColWidth, m_fltYArr[1] + 0.3f * this.fltRowHeight), "胎心率", Color.Black, false, -0.5f, FontStyle.Regular);
            this.m_rectDrawText(new PointF(m_fltXArr[1] + 0.1f * this.fltColWidth, m_fltYArr[1] + 0.7f * this.fltRowHeight), "次/分", Color.Black, false);
            this.m_rectDrawPoint(new PointF(m_fltXArr[1] + 0.6f * this.fltColWidth, m_fltYArr[1] + 1.5f * this.fltRowHeight), Color.Blue);
            this.m_rectDrawText(new PointF(m_fltXArr[1] + 0.3f * this.fltColWidth, m_fltYArr[2] + 0.3f * this.fltRowHeight), "180", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[1] + 0.3f * this.fltColWidth, m_fltYArr[4] + 0.3f * this.fltRowHeight), "160", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[1] + 0.3f * this.fltColWidth, m_fltYArr[6] + 0.3f * this.fltRowHeight), "140", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[1] + 0.3f * this.fltColWidth, m_fltYArr[8] + 0.3f * this.fltRowHeight), "120", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[1] + 0.3f * this.fltColWidth, m_fltYArr[10] + 0.3f * this.fltRowHeight), "100", Color.Black, false);

            //宫口扩张
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.05f * this.fltColWidth, m_fltYArr[1] + 0.3f * this.fltRowHeight), "宫 口", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.05f * this.fltColWidth, m_fltYArr[1] + 0.7f * this.fltRowHeight), "扩 张", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.3f * this.fltColWidth, m_fltYArr[1] + 1.0f * this.fltRowHeight), "cm", Color.Black, false);
            this.m_rectDrawCircle(new PointF(m_fltXArr[2] + 0.6f * this.fltColWidth, m_fltYArr[1] + 1.6f * this.fltRowHeight), Color.Red);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.3f * this.fltColWidth, m_fltYArr[2] + 0.3f * this.fltRowHeight), "10", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.3f * this.fltColWidth, m_fltYArr[3] + 0.3f * this.fltRowHeight), " 9", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.3f * this.fltColWidth, m_fltYArr[4] + 0.3f * this.fltRowHeight), " 8", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.3f * this.fltColWidth, m_fltYArr[5] + 0.3f * this.fltRowHeight), " 7", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.3f * this.fltColWidth, m_fltYArr[6] + 0.3f * this.fltRowHeight), " 6", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.3f * this.fltColWidth, m_fltYArr[7] + 0.3f * this.fltRowHeight), " 5", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.3f * this.fltColWidth, m_fltYArr[8] + 0.3f * this.fltRowHeight), " 4", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.3f * this.fltColWidth, m_fltYArr[9] + 0.3f * this.fltRowHeight), " 3", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.3f * this.fltColWidth, m_fltYArr[10] + 0.3f * this.fltRowHeight), " 2", Color.Black, false);
            this.m_rectDrawText(new PointF(m_fltXArr[2] + 0.3f * this.fltColWidth, m_fltYArr[11] + 0.3f * this.fltRowHeight), " 1", Color.Black, false);
        }

        /// <summary>
        /// 画数据
        /// </summary>
        private void m_mthDrawData()
        {
            foreach (clsShape objCurrentShape in lstShapeData)
            {
                objCurrentShape.m_mthDraw();
            }
        }

        /// <summary>
        /// 初始化图像
        /// </summary>
        private void m_mthInitImage()
        {
            this.fltImgHeight = 2f * this.fltTopOffset + this.fltHeaderHeight + (m_fltYArr[m_fltYArr.Length - 1] - m_fltYArr[0]) + this.fltFootHeight;
            this.fltImgWidth = 2f * this.fltLeftOffset + 3.6f * this.fltColWidth + Convert.ToSingle(this.intColNumber) * this.fltColWidth;
            this.m_imgCurrentPic = new Bitmap((int)this.fltImgWidth + 1, (int)this.fltImgHeight + 1);

            this.gpsPainter = Graphics.FromImage(this.m_imgCurrentPic);
            this.gpsPainter.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.gpsPainter.Clear(Color.White);
        }

        /// <summary>
        /// 画圈
        /// </summary>
        public RectangleF m_rectDrawCircle(PointF p_pntCenterPos, Color p_clrPenColor)
        {
            Pen pnCurrent = new Pen(p_clrPenColor);
            pnCurrent.Width = fltBorderWidth;
            float fltDiameter = 8 * this.fltRate;

            PointF pntLocation = new PointF(p_pntCenterPos.X - fltDiameter / 2f, p_pntCenterPos.Y - fltDiameter / 2f);

            RectangleF rectTemp = new RectangleF(pntLocation, new SizeF(fltDiameter, fltDiameter));
            this.gpsPainter.DrawArc(pnCurrent, rectTemp, 0, 360);
            return rectTemp;
        }

        /// <summary>
        /// 画点
        /// </summary>
        public RectangleF m_rectDrawPoint(PointF p_pntCenterPos, Color p_clrPenColor)
        {
            Pen pnCurrent = new Pen(p_clrPenColor);
            pnCurrent.Width = fltBorderWidth;
            float fltDiameter = 8 * this.fltRate;

            PointF pntLocation = new PointF(p_pntCenterPos.X - fltDiameter / 2f, p_pntCenterPos.Y - fltDiameter / 2f);

            RectangleF rectTemp = new RectangleF(pntLocation, new SizeF(fltDiameter, fltDiameter));
            this.gpsPainter.FillEllipse(pnCurrent.Brush, rectTemp);
            return rectTemp;
        }

        /// <summary>
        /// 画叉
        /// </summary>
        public RectangleF m_rectDrawCrox(PointF p_pntCenterPos, Color p_clrPenColor)
        {
            Pen pnCurrent = new Pen(p_clrPenColor);
            pnCurrent.Width = fltBorderWidth;
            float fltDiameter = 8 * this.fltRate;

            PointF pnTopLeft = new PointF(p_pntCenterPos.X - fltDiameter / 2f, p_pntCenterPos.Y - fltDiameter / 2f);
            PointF pnTopRight = new PointF(p_pntCenterPos.X + fltDiameter / 2f, p_pntCenterPos.Y - fltDiameter / 2f);
            PointF pnBottomLeft = new PointF(p_pntCenterPos.X - fltDiameter / 2f, p_pntCenterPos.Y + fltDiameter / 2f);
            PointF pnBottomRight = new PointF(p_pntCenterPos.X + fltDiameter / 2f, p_pntCenterPos.Y + fltDiameter / 2f);

            this.gpsPainter.DrawLine(pnCurrent, pnTopLeft, pnBottomRight);
            this.gpsPainter.DrawLine(pnCurrent, pnTopRight, pnBottomLeft);
            return new RectangleF(pnTopLeft.X, pnTopLeft.Y, fltDiameter, fltDiameter);
        }

        /// <summary>
        /// 画箭头
        /// </summary>
        public void m_mthDrawArrow(PointF p_pntStart, PointF p_pntEnd, Color p_clrPenColor)
        {
            Pen pnCurrent = new Pen(p_clrPenColor);
            pnCurrent.Width = fltBorderWidth;
            float fltDiameter = 10 * this.fltRate;

            PointF pnTopLeft = new PointF(p_pntEnd.X - fltDiameter / 2f, p_pntEnd.Y - fltDiameter / 2f);
            PointF pnTopRight = new PointF(p_pntEnd.X + fltDiameter / 2f, p_pntEnd.Y - fltDiameter / 2f);

            this.gpsPainter.DrawLine(pnCurrent, p_pntStart, p_pntEnd);
            this.gpsPainter.DrawLine(pnCurrent, pnTopLeft, p_pntEnd);
            this.gpsPainter.DrawLine(pnCurrent, pnTopRight, p_pntEnd);
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="p_pntStart"></param>
        /// <param name="p_pntEnd"></param>
        public void m_mthDrawLine(PointF p_pntStart, PointF p_pntEnd, Color p_clrPenColor, DashStyle p_dsStyle)
        {
            Pen pnCurrent = new Pen(p_clrPenColor);
            pnCurrent.Width = fltBorderWidth;
            pnCurrent.DashStyle = p_dsStyle;
            this.gpsPainter.DrawLine(pnCurrent, p_pntStart, p_pntEnd);
        }
        /// <summary>
        /// 指定pen画线
        /// </summary>
        /// <param name="p_pntStart"></param>
        /// <param name="p_pntEnd"></param>
        /// <param name="p_pnCurrent"></param>
        public void m_mthDrawLine(PointF p_pntStart, PointF p_pntEnd, Pen p_pnCurrent)
        {
            this.gpsPainter.DrawLine(p_pnCurrent, p_pntStart, p_pntEnd);
        }

        /// <summary>
        /// 画文字
        /// </summary>
        public RectangleF m_rectDrawText(PointF p_pnLocation, string p_strText, Color p_clrForeColor, bool p_blnIsVeri)
        {
            if (p_blnIsVeri)
            {
                StringFormat sfFormat = new StringFormat();
                sfFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                this.gpsPainter.DrawString(p_strText, new Font("宋体", this.fltSizePerUnit), new Pen(p_clrForeColor).Brush, p_pnLocation, sfFormat);
            }
            else
            {
                this.gpsPainter.DrawString(p_strText, new Font("宋体", this.fltSizePerUnit), new Pen(p_clrForeColor).Brush, p_pnLocation);
            }
            RectangleF rectArea = new RectangleF(p_pnLocation + new SizeF(2 * this.fltRate, 2 * this.fltRate), this.m_sizMeasureString(p_strText, p_blnIsVeri, p_pnLocation) - new SizeF(2 * this.fltRate, 2 * this.fltRate));
            return rectArea;
        }

        /// <summary>
        /// 画文字
        /// </summary>
        /// <param name="p_fltFont">字体增量</param>
        public RectangleF m_rectDrawText(PointF p_pnLocation, string p_strText, Color p_clrForeColor, bool p_blnIsVeri, float p_fltFont, FontStyle p_fsStyle)
        {
            if (p_blnIsVeri)
            {
                StringFormat sfFormat = new StringFormat();
                sfFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                this.gpsPainter.DrawString(p_strText, new Font("宋体", (this.fltSizePerUnit + p_fltFont), p_fsStyle), new Pen(p_clrForeColor).Brush, p_pnLocation, sfFormat);
            }
            else
            {
                this.gpsPainter.DrawString(p_strText, new Font("宋体", (this.fltSizePerUnit + p_fltFont), p_fsStyle), new Pen(p_clrForeColor).Brush, p_pnLocation);
            }
            RectangleF rectArea = new RectangleF(p_pnLocation + new SizeF(2 * this.fltRate, 2 * this.fltRate), this.m_sizMeasureString(p_strText, p_blnIsVeri, p_fltFont, p_fsStyle, p_pnLocation) - new SizeF(2 * this.fltRate, 2 * this.fltRate));
            return rectArea;
        }

        /// <summary>
        /// 量度文字大小 
        /// </summary>
        public SizeF m_sizMeasureString(string p_strText, bool p_blnIsVeri, PointF p_pntOrigin)
        {
            if (p_blnIsVeri)
            {
                StringFormat sfFormat = new StringFormat();
                sfFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                return this.gpsPainter.MeasureString(p_strText, new Font("宋体", this.fltSizePerUnit), p_pntOrigin, sfFormat);
            }
            else
            {
                return this.gpsPainter.MeasureString(p_strText, new Font("宋体", this.fltSizePerUnit));
            }
        }

        /// <summary>
        /// 量度文字大小 
        /// </summary>
        public SizeF m_sizMeasureString(string p_strText, bool p_blnIsVeri, float p_fltFont, FontStyle p_fsStyle, PointF p_pntOrigin)
        {
            if (p_blnIsVeri)
            {
                StringFormat sfFormat = new StringFormat();
                sfFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                return this.gpsPainter.MeasureString(p_strText, new Font("宋体", (this.fltSizePerUnit + p_fltFont), p_fsStyle), p_pntOrigin, sfFormat);
            }
            else
            {
                return this.gpsPainter.MeasureString(p_strText, new Font("宋体", (this.fltSizePerUnit + p_fltFont)));
            }
        }

        /// <summary>
        /// 量度文字大小 
        /// </summary>
        public SizeF m_sizMeasureString(string p_strText, float p_fltFont, FontStyle p_fsStyle)
        {
            return this.gpsPainter.MeasureString(p_strText, new Font("宋体", (this.fltSizePerUnit + p_fltFont)));
        }

        /// <summary>
        /// 量度文字大小 
        /// </summary>
        public SizeF m_sizMeasureString(string p_strText)
        {
            return this.gpsPainter.MeasureString(p_strText, new Font("宋体", this.fltSizePerUnit));
        }
        #endregion
    }

    #region 基本数据图形
    /// <summary>
    /// 各种需要画的图形基类
    /// </summary>
    public abstract class clsShape
    {
        /// <summary>
        /// 当前图形对应的数据
        /// </summary>
        public object objCurrentData { get; set; }

        /// <summary>
        /// 是否弹出提示
        /// </summary>
        private bool m_blnShowMsg = true;
        public clsShape(PartogramDrawer p_objDrawer)
        {
            objDrawer = p_objDrawer;
            m_blnShowMsg = false;
        }
        public clsShape(PartogramDrawer p_objDrawer, string p_strText,
        string p_strRecordDate, string p_strType)
        {
            objDrawer = p_objDrawer;
            strType = p_strType;
            strRecordDate = p_strRecordDate;
            strText = p_strText;
        }
        /// <summary>
        /// 画图工具
        /// </summary>
        protected PartogramDrawer objDrawer { get; private set; }
        /// <summary>
        /// 画图形
        /// </summary>
        public virtual void m_mthDraw()
        {
        }

        /// <summary>
        /// 记录类型
        /// </summary>
        public string strType { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        private string strRecordDate { get; set; }
        /// <summary>
        /// 当前值
        /// </summary>
        public virtual string strText { get; private set; }
        /// <summary>
        /// 事件名称
        /// </summary>
        public string strEventName { private get; set; }

        /// <summary>
        /// 获取提示框信息
        /// </summary>
        /// <returns></returns>
        public string strMessage
        {
            get
            {
                string strMess = string.Empty;
                if (m_blnShowMsg)
                {
                    if (string.IsNullOrEmpty(strRecordDate))
                    {
                        strMess = "类   型：{0}" + Environment.NewLine + "当前值：{1}";
                        return string.Format(strMess, strType, strText);
                    }
                    else
                    {
                        strRecordDate = weCare.Core.Utils.Function.Datetime(strRecordDate).ToString("yyyy-MM-dd HH:mm");
                        if (string.IsNullOrEmpty(strEventName))
                        {
                            strMess = "时   间：{0}" + Environment.NewLine + "类   型：{1}" + Environment.NewLine + "当前值：{2}";
                            return string.Format(strMess, strRecordDate, strType, strText);
                        }
                        else
                        {
                            strMess = "时   间：{0}" + Environment.NewLine + "类   型：{1}" + Environment.NewLine + "事   件：{2}";
                            return string.Format(strMess, strRecordDate, strType, strEventName);
                        }
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// 获取图形所占用区域
        /// </summary>
        /// <returns></returns>
        public virtual RectangleF rectArea { get; private set; }
        /// <summary>
        /// 位置
        /// </summary>
        public virtual PointF pntPos { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string strTypeName
        {
            get
            {
                return this.GetType().Name;
            }
        }
    }
    /// <summary>
    /// 线条
    /// </summary>
    public class clsLine : clsShape
    {
        /// <summary>
        /// 起点
        /// </summary>
        public PointF pntStart { get; set; }
        /// <summary>
        /// 终点
        /// </summary>
        public PointF pntEnd { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public Color clrForeColor { get; set; }
        /// <summary>
        /// 线条样式
        /// </summary>
        public DashStyle dsStyle { get; set; }

        public clsLine(PartogramDrawer p_objDrawer, PointF p_pntStart,
            PointF p_pntEnd, Color p_clrForeColor, DashStyle p_dsStyle)
            : base(p_objDrawer)
        {
            pntStart = p_pntStart;
            pntEnd = p_pntEnd;
            clrForeColor = p_clrForeColor;
            dsStyle = p_dsStyle;
        }

        public override void m_mthDraw()
        {
            base.objDrawer.m_mthDrawLine(pntStart, pntEnd, clrForeColor, dsStyle);
        }
    }
    /// <summary>
    /// 基本信息文本
    /// </summary>
    public class clsText : clsShape
    {
        /// <summary>
        /// 位置
        /// </summary>
        public override PointF pntPos { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public Color clrForeColor { get; set; }
        /// <summary>
        /// 书写方向
        /// </summary>
        public bool blnIsVeri { get; set; }
        /// <summary>
        /// 字体增量
        /// </summary>
        public float fltFont { get; set; }
        /// <summary>
        /// 字体形式 
        /// </summary>
        public FontStyle fntStyle { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        private RectangleF m_rectArea = RectangleF.Empty;
        /// <summary>
        /// 孕周信息
        /// </summary>
        private string strPregnantText { get; set; }

        public clsText(PartogramDrawer p_objDrawer, PointF p_pntPos, string p_strText, string p_strRecordDate,
            string p_strType, Color p_clrForeColor, bool p_blnIsVeri, float p_fltFont, FontStyle p_fntStyle, string p_strPregnantText)
            : base(p_objDrawer, p_strText, p_strRecordDate, p_strType)
        {
            pntPos = p_pntPos;
            clrForeColor = p_clrForeColor;
            blnIsVeri = p_blnIsVeri;
            fltFont = p_fltFont;
            fntStyle = p_fntStyle;
            strPregnantText = p_strPregnantText;
        }

        public override void m_mthDraw()
        {
            if (base.strType == "孕周" && !string.IsNullOrEmpty(strPregnantText))
            {
                this.m_rectArea = base.objDrawer.m_rectDrawText(pntPos, strPregnantText, clrForeColor, blnIsVeri, fltFont, fntStyle);
            }
            else
            {
                this.m_rectArea = base.objDrawer.m_rectDrawText(pntPos, base.strText, clrForeColor, blnIsVeri, fltFont, fntStyle);
            }
        }

        public override RectangleF rectArea
        {
            get
            {
                return this.m_rectArea;
            }
        }
    }
    /// <summary>
    /// 宫缩，血压，签名文本
    /// </summary>
    public class clsSpecialText : clsText
    {
        /// <summary>
        /// 列序
        /// </summary>
        public int intColIndex { get; set; }
        /// <summary>
        /// 文本类型(1-宫缩，2-血压，3-签名文本)
        /// </summary>
        public int intTextType { get; set; }

        public clsSpecialText(PartogramDrawer p_objDrawer, PointF p_pntPos, string p_strText, string p_strRecordDate,
            string p_strType, Color p_clrForeColor, bool p_blnIsVeri, float p_fltFont, FontStyle p_fntStyle, int p_intColIndex, int p_intTextType)
            : base(p_objDrawer, p_pntPos, p_strText, p_strRecordDate, p_strType, p_clrForeColor, p_blnIsVeri, p_fltFont, p_fntStyle, string.Empty)
        {
            intColIndex = p_intColIndex;
            intTextType = p_intTextType;
        }
    }
    /// <summary>
    /// 圈
    /// </summary>
    public class clsCircle : clsShape
    {
        /// <summary>
        /// 位置
        /// </summary>
        public override PointF pntPos { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public Color clrForeColor { get; set; }

        private decimal m_decEventType = decimal.Zero;
        /// <summary>
        /// 事件类型 1-宫口开全,2-胎儿娩出,3-剖宫手术,4-取出胎儿
        /// </summary>
        public decimal decEventType
        {
            get
            {
                return this.m_decEventType;
            }
            set
            {
                this.m_decEventType = value;
                string strEventName = string.Empty;
                switch ((int)value)
                {
                    case 1:
                        strEventName = "宫口开全";
                        break;
                    case 2:
                        strEventName = "胎儿娩出";
                        break;
                    case 3:
                        strEventName = "剖宫手术";
                        break;
                    case 4:
                        strEventName = "取出胎儿";
                        break;
                    default:
                        strEventName = string.Empty;
                        break;

                }
                base.strEventName = strEventName;
            }
        }
        /// <summary>
        /// 书写方向
        /// </summary>
        public bool blnIsVeri { get; set; }
        /// <summary>
        /// 字体增量
        /// </summary>
        public float fltFont { get; set; }
        /// <summary>
        /// 字体形式 
        /// </summary>
        public FontStyle fntStyle { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        private RectangleF m_rectArea = RectangleF.Empty;

        public clsCircle(PartogramDrawer p_objDrawer, PointF p_pntPos, string p_strText, string p_strRecordDate,
            string p_strType, Color p_clrForeColor, bool p_blnIsVeri, float p_fltFont, FontStyle p_fntStyle, decimal p_decEventType)
            : base(p_objDrawer, p_strText, p_strRecordDate, p_strType)
        {
            pntPos = p_pntPos;
            clrForeColor = p_clrForeColor;
            blnIsVeri = p_blnIsVeri;
            fltFont = p_fltFont;
            fntStyle = p_fntStyle;
            decEventType = p_decEventType;
        }

        public override void m_mthDraw()
        {
            this.m_rectArea = base.objDrawer.m_rectDrawCircle(pntPos, clrForeColor);
        }

        public override RectangleF rectArea
        {
            get
            {
                return this.m_rectArea;
            }
        }
    }
    /// <summary>
    /// 点
    /// </summary>
    public class clsPoint : clsShape
    {
        /// <summary>
        /// 位置
        /// </summary>
        public override PointF pntPos { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public Color clrForeColor { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        private RectangleF m_rectArea = RectangleF.Empty;

        public clsPoint(PartogramDrawer p_objDrawer, PointF p_pntPos, Color p_clrForeColor,
            string p_strText, string p_strRecordDate, string p_strType)
            : base(p_objDrawer, p_strText, p_strRecordDate, p_strType)
        {
            pntPos = p_pntPos;
            clrForeColor = p_clrForeColor;
        }

        public override void m_mthDraw()
        {
            m_rectArea = base.objDrawer.m_rectDrawPoint(pntPos, clrForeColor);
        }

        public override RectangleF rectArea
        {
            get
            {
                return this.m_rectArea;
            }
        }
    }
    /// <summary>
    /// 叉
    /// </summary>
    public class clsCrox : clsShape
    {
        /// <summary>
        /// 位置
        /// </summary>
        public override PointF pntPos { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public Color clrForeColor { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        private RectangleF m_rectArea = RectangleF.Empty;

        public clsCrox(PartogramDrawer p_objDrawer, PointF p_pntPos, Color p_clrForeColor,
            string p_strText, string p_strRecordDate, string p_strType)
            : base(p_objDrawer, p_strText, p_strRecordDate, p_strType)
        {
            pntPos = p_pntPos;
            clrForeColor = p_clrForeColor;
        }

        public override void m_mthDraw()
        {
            m_rectArea = base.objDrawer.m_rectDrawCrox(pntPos, clrForeColor);
        }

        public override RectangleF rectArea
        {
            get
            {
                return this.m_rectArea;
            }
        }
    }
    /// <summary>
    /// 箭头
    /// </summary>
    public class clsArrow : clsShape
    {
        /// <summary>
        /// 起点
        /// </summary>
        public PointF pntStart { get; set; }
        /// <summary>
        /// 终点
        /// </summary>
        public PointF pntEnd { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public Color clrForeColor { get; set; }
        public clsArrow(PartogramDrawer p_objDrawer, PointF p_pntStart, PointF p_pntEnd, Color p_clrForeColor)
            : base(p_objDrawer)
        {
            pntStart = p_pntStart;
            pntEnd = p_pntEnd;
            clrForeColor = p_clrForeColor;
        }

        public override void m_mthDraw()
        {
            base.objDrawer.m_mthDrawArrow(pntStart, pntEnd, clrForeColor);
        }

        public override PointF pntPos
        {
            get
            {
                return this.pntStart;
            }
        }
    }
    #endregion

    #region 点击参数
    /// <summary>
    /// 双击点时的参数
    /// </summary>
    public class clsDoubleClickedEventArgs : EventArgs
    {
        /// <summary>
        /// 点中的数据集合
        /// </summary>
        public List<clsShape> lstCurrentShape { get; set; }

        public clsDoubleClickedEventArgs(List<clsShape> p_lstCurrentShape)
        {
            lstCurrentShape = p_lstCurrentShape;
        }
    }
    #endregion

}
