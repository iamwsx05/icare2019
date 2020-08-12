using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    public class EventPoint
    {
        DrawingDataColumn ParentColumn;
        public EntityEmrTemperatureChartEvent Value;

        /// <summary>
        /// 索引
        /// </summary>
        int Index;
        Font TextFont;

        /// <summary>
        /// 描绘位置
        /// </summary>
        PointF Loacation;

        /// <summary>
        /// 字体颜色
        /// </summary>
        Color ForeColor;

        /// <summary>
        /// 字符串大小
        /// </summary>
        SizeF textSize;

        /// <summary>
        /// 描绘内容
        /// </summary>
        string Text;

        /// <summary>
        /// 是否已描绘
        /// </summary>
        bool drawed;

        /// <summary>
        /// 由描绘点转换为事件的原始数据
        /// </summary>
        public ValuePoint OriginValuePoint { get; set; }

        public EventPoint(DrawingDataColumn parent, EntityEmrTemperatureChartEvent value)
        {
            this.ParentColumn = parent;
            this.Value = value;
            this.Index = 0;
            this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit * 0.75f);
            this.drawed = false;
            this.OriginValuePoint = null;

            Text = this.Value.eventName;
            if (this.Value.EventType != null)
            {
                Text = this.Value.EventType.eventName;
            }

            if (this.Value.isDrawTime == 1)//如果描绘时间则加上连接线
            {
                DateTime dt = this.Value.recordDate;
                Text += "—" + DateTimeToCHS(dt);
            }
        }

        #region 坐标相关
        public float X1
        {
            get
            {
                return this.Loacation.X;
            }
        }


        public float X2
        {
            get
            {
                return this.Loacation.X + this.ParentColumn.SizePerUnit;
            }
        }

        public float Y1
        {
            get
            {
                return this.Loacation.Y;
            }
        }

        public float Y2
        {
            get
            {
                return this.Y1 + this.textSize.Width;
            }
        } 
        #endregion

        public void Init()
        {

        }

        public void Draw()
        {
            //当前点距离顶端距离
            float top;

            float t = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset + this.ParentColumn.HeaderHeightUnit * this.ParentColumn.SizePerUnit;

            float innerTopOffset = 0f;
            float innerOffsetUnit = (float)this.Value.timePeriod;

            var query = from item in this.ParentColumn.EventPoints
                        where item.drawed == true && item.Value.timePeriod == this.Value.timePeriod && item.Value.drawingPotision == this.Value.drawingPotision
                        select item;

            //if (com.HopeBridge.common.utility.clsGlobalHospitalCode.Code == "0002"
            //    && this.Value.Drawingpotision_int == 0)//东华42度下
            //{
            //    foreach (var item in query)
            //    {
            //        innerOffsetUnit += 1f;
            //    }
            //}
            //else
            //{
            foreach (var item in query)
            {
                innerTopOffset += item.Y2 - item.Y1;
            }
            //}

            if (this.Value.drawingPotision == 0)//画在42度下
            {
                ForeColor = Color.Red;
                top = t;
            }
            else//画在35度下
            {
                ForeColor = Color.Blue;

                float b = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset + (this.ParentColumn.HeaderHeightUnit + this.ParentColumn.BodyHeightUnit) * this.ParentColumn.SizePerUnit;

                float val = 35;
                top = b - (val - ThreeItemConstValue.MinTemp) * (b - t) / (ThreeItemConstValue.MaxTemp - ThreeItemConstValue.MinTemp);
            }

            //float innerOffsetUnit = (float)this.Value.TimePeriod;

            //当前点的中心位置距离左端距离
            float left = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + innerOffsetUnit * this.ParentColumn.SizePerUnit - this.TextFont.Size * 0.15f;

            Loacation = new PointF(left, top + innerTopOffset);

            this.textSize = this.ParentColumn.graphics.MeasureString(Text, this.TextFont);

            this.ParentColumn.DrawText(this.Loacation, this.Text, this.TextFont, this.ForeColor, true);

            drawed = true;
        }

        #region 时间转为中文
        private string DateTimeToCHS(DateTime dt)
        {
            int hour = dt.Hour;
            int minute = dt.Minute;

            string text = string.Format("{0}点{1}分", DigitalToCHS(hour, 1), DigitalToCHS(minute, 2));

            return text;
        }

        private string DigitalToCHS(int intValue)
        {
            string text = string.Empty;

            if (intValue < 10)
            {
                text = SingleDigitalToChs(intValue);
            }
            else if (intValue >= 10 && intValue < 20)
            {
                int d1 = intValue / 10;
                int d2 = intValue % 10;

                if (d2 == 0)
                {
                    text = "十";
                }
                else
                {
                    text = "十" + SingleDigitalToChs(d2);
                }
            }
            else
            {
                int d1 = intValue / 10;
                int d2 = intValue % 10;

                text = SingleDigitalToChs(d1) + "十" + SingleDigitalToChs(d2);
            }

            return text;
        }

        /// <summary>
        /// 时间转为中文
        /// </summary>
        /// <param name="intValue">当前数值</param>
        /// <param name="p_intType">类型（1-小时，2-分钟）</param>
        /// <returns></returns>
        private string DigitalToCHS(int intValue, int p_intType)
        {
            bool blnFlags = false;
            //if (com.HopeBridge.common.utility.clsGlobalHospitalCode.Code == "0002")//东华采用12小时制
            //{
            //    blnFlags = true;
            //}

            string text = string.Empty;
            if (p_intType == 1)
            {
                if (intValue < 10)
                {
                    text = SingleDigitalToChs(intValue);
                }
                else if (intValue >= 10 && intValue <= 12)
                {
                    int d1 = intValue / 10;
                    int d2 = intValue % 10;

                    if (d2 == 0)
                    {
                        text = "十";
                    }
                    else
                    {
                        text = "十" + SingleDigitalToChs(d2);
                    }
                }
                else if (intValue > 12 && intValue < 22)
                {
                    if (blnFlags)
                    {
                        int d1 = (intValue - 12);

                        text = SingleDigitalToChs(d1);
                    }
                    else
                    {
                        int d1 = intValue / 10;
                        int d2 = intValue % 10;

                        if (d1 == 1)
                        {
                            text = "十" + SingleDigitalToChs(d2);
                        }
                        else
                        {
                            text = SingleDigitalToChs(d1) + "十" + SingleDigitalToChs(d2);
                        }
                    }
                }
                else
                {
                    if (blnFlags)
                    {
                        int d1 = (intValue - 12) / 10;
                        int d2 = (intValue - 12) % 10;

                        if (d2 == 0)
                        {
                            text = "十";
                        }
                        else
                        {
                            text = "十" + SingleDigitalToChs(d2);
                        }
                    }
                    else
                    {
                        int d1 = intValue / 10;
                        int d2 = intValue % 10;
                        text = SingleDigitalToChs(d1) + "十" + SingleDigitalToChs(d2);
                    }
                }
            }
            else
            {
                if (intValue < 10)
                {
                    text = SingleDigitalToChs(0) + SingleDigitalToChs(intValue);
                }
                else if (intValue >= 10 && intValue < 20)
                {
                    int d1 = intValue / 10;
                    int d2 = intValue % 10;

                    if (d2 == 0)
                    {
                        text = "十";
                    }
                    else
                    {
                        text = "十" + SingleDigitalToChs(d2);
                    }
                }
                else
                {
                    int d1 = intValue / 10;
                    int d2 = intValue % 10;

                    if (d1 == 1)
                    {
                        text = "十" + SingleDigitalToChs(d2);
                    }
                    else if (d2 == 0)
                    {
                        text = SingleDigitalToChs(d1) + "十";
                    }
                    else
                    {
                        text = SingleDigitalToChs(d1) + "十" + SingleDigitalToChs(d2);
                    }
                }
            }

            return text;
        }

        private string SingleDigitalToChs(int val)
        {
            if (val == 0)
            {
                return "零";
            }
            else if (val == 1)
            {
                return "一";
            }
            else if (val == 2)
            {
                return "二";
            }
            else if (val == 3)
            {
                return "三";
            }
            else if (val == 4)
            {
                return "四";
            }
            else if (val == 5)
            {
                return "五";
            }
            else if (val == 6)
            {
                return "六";
            }
            else if (val == 7)
            {
                return "七";
            }
            else if (val == 8)
            {
                return "八";
            }
            else if (val == 9)
            {
                return "九";
            }
            else
            {
                return string.Empty;
            }
        } 
        #endregion


        public override string ToString()
        {
            string eventName = this.Value.eventName;
            if (this.Value.EventType != null)
            {
                eventName = this.Value.EventType.eventName;
            }

            string text = string.Format(@"
时  间：{0}
事  件：{1}

", this.Value.recordDate.ToString("yyyy年MM月dd日 HH时mm分"), eventName);

            return text;
        }
    }
}
