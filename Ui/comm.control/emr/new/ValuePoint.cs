using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 体温脉搏重叠方式
    /// </summary>
    public enum PointOverlapType
    {
        /// <summary>
        /// 不重叠
        /// </summary>
        None,

        /// <summary>
        /// 脉搏腋表
        /// </summary>
        Type1,

        /// <summary>
        /// 脉搏肛表
        /// </summary>
        Type2,

        /// <summary>
        /// 脉搏口表
        /// </summary>
        Type3,
    }

    public class ValuePoint
    {
        float pointSizeScale = 0.7f;
        float pointWidthScale = 0.14f;//调整此参数可调整圆圈\交叉的线条粗度
        float lineWidthScale = 0.14f;
        
        public PointOverlapType OverlapType = PointOverlapType.None;
        DrawingDataColumn ParentColumn;
        public ValuePoint(DrawingDataColumn parent, EntityEmrTemperatureMonitorData value)
        {
            this.ParentColumn = parent;
            //this.Connect = false;
            this.Value = value;
            this.Index = 0;
            this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, 10f);

            this.BorderWidth = this.ParentColumn.SizePerUnit * pointWidthScale;
            this.LineWidth = this.ParentColumn.SizePerUnit * lineWidthScale;
            this.SubValuePoints = new List<ValuePoint>();
        }

        /// <summary>
        /// 数据、显示坐标出示化
        /// </summary>
        public void Init()
        {
            if (this.Value.measureType == 7)//呼吸
            {
                this.Diameter = this.ParentColumn.SizePerUnit;
            }
            else
            {
                this.Diameter = pointSizeScale * this.ParentColumn.SizePerUnit;
            }


            //当前点的中心位置距离顶端距离
            float centerTop = 0;

            //当前点的中心位置距离左端距离
            float centerLeft = 0;


            float innerOffsetUnit = (float)this.Value.timePeriod;

            if (this.Value.measureType == 7)//呼吸
            {
                #region 呼吸
                centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset + (this.ParentColumn.HeaderHeightUnit + this.ParentColumn.BodyHeightUnit) * this.ParentColumn.SizePerUnit + ThreeItemConstValue.FooterHeight_Breath * this.ParentColumn.SizePerUnit / 2f;
                centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + innerOffsetUnit * this.ParentColumn.SizePerUnit + this.ParentColumn.SizePerUnit / 2f;
                #endregion
            }
            else if (this.Value.measureType == 8)//血压
            {
                #region 血压
                //只有一个数据是，画在中间
                //2个画在左右
                //3个或4个，画在4个角落
                if (this.TypeCount == 1)
                {
                    if (this.Value.recordDate.Hour > 12)
                    {
                        centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
+ (this.ParentColumn.HeaderHeightUnit
+ this.ParentColumn.BodyHeightUnit
+ ThreeItemConstValue.FooterHeight_Breath
+ (ThreeItemConstValue.FooterHeight_Blood / 4f) * 3f) * this.ParentColumn.SizePerUnit;

                        centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                    }
                    else
                    {
                        centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
    + (this.ParentColumn.HeaderHeightUnit + this.ParentColumn.BodyHeightUnit + ThreeItemConstValue.FooterHeight_Breath + ThreeItemConstValue.FooterHeight_Blood / 4f) * this.ParentColumn.SizePerUnit;

                        centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                    }
                }
                else if (this.TypeCount == 2)
                {
                    if (this.Index == 0)
                    {
                        centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                            + (this.ParentColumn.HeaderHeightUnit + this.ParentColumn.BodyHeightUnit + ThreeItemConstValue.FooterHeight_Breath + ThreeItemConstValue.FooterHeight_Blood / 4f) * this.ParentColumn.SizePerUnit;

                        centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                    }
                    else
                    {
                        centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
    + (this.ParentColumn.HeaderHeightUnit
       + this.ParentColumn.BodyHeightUnit
       + ThreeItemConstValue.FooterHeight_Breath
       + (ThreeItemConstValue.FooterHeight_Blood / 4f) * 3f) * this.ParentColumn.SizePerUnit;

                        centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                    }
                }
                else if (this.TypeCount > 2)
                {
                    if (this.Index == 0)
                    {
                        centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                            + (this.ParentColumn.HeaderHeightUnit + this.ParentColumn.BodyHeightUnit + ThreeItemConstValue.FooterHeight_Breath + (ThreeItemConstValue.FooterHeight_Blood / 4f) * 1f) * this.ParentColumn.SizePerUnit;

                        centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 1.5f * this.ParentColumn.SizePerUnit;
                    }
                    else if (this.Index == 1)
                    {
                        centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                            + (this.ParentColumn.HeaderHeightUnit + this.ParentColumn.BodyHeightUnit + ThreeItemConstValue.FooterHeight_Breath + (ThreeItemConstValue.FooterHeight_Blood / 4f) * 1f) * this.ParentColumn.SizePerUnit;

                        centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 4.5f * this.ParentColumn.SizePerUnit;

                    }
                    else if (this.Index == 2)
                    {

                        centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                                + (this.ParentColumn.HeaderHeightUnit + this.ParentColumn.BodyHeightUnit + ThreeItemConstValue.FooterHeight_Breath + (ThreeItemConstValue.FooterHeight_Blood / 4f) * 3f) * this.ParentColumn.SizePerUnit;

                        centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 1.5f * this.ParentColumn.SizePerUnit;
                    }
                    else if (this.Index == 3)
                    {
                        centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                                + (this.ParentColumn.HeaderHeightUnit + this.ParentColumn.BodyHeightUnit + ThreeItemConstValue.FooterHeight_Breath + (ThreeItemConstValue.FooterHeight_Blood / 4f) * 3f) * this.ParentColumn.SizePerUnit;

                        centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 4.5f * this.ParentColumn.SizePerUnit;
                    }
                }
                #endregion
            }
            else if (this.Value.measureType == 9)//总入液量
            {
                #region 总入液量
                centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                            + (this.ParentColumn.HeaderHeightUnit
                                + this.ParentColumn.BodyHeightUnit
                                + ThreeItemConstValue.FooterHeight_Breath
                                + ThreeItemConstValue.FooterHeight_Blood
                                + ThreeItemConstValue.FooterHeight_Liq / 2f
                                ) * this.ParentColumn.SizePerUnit;

                centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                #endregion
            }
            else if (this.Value.measureType == 10)//大便
            {
                #region 大便
                centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                            + (this.ParentColumn.HeaderHeightUnit
                                + this.ParentColumn.BodyHeightUnit
                                + ThreeItemConstValue.FooterHeight_Breath
                                + ThreeItemConstValue.FooterHeight_Blood
                                + ThreeItemConstValue.FooterHeight_Liq
                                + ThreeItemConstValue.FooterHeight_DaBian / 2f
                                ) * this.ParentColumn.SizePerUnit;

                centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                #endregion
            }
            else if (this.Value.measureType == 11)//小便
            {
                #region 小便
                centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                            + (this.ParentColumn.HeaderHeightUnit
                                + this.ParentColumn.BodyHeightUnit
                                + ThreeItemConstValue.FooterHeight_Breath
                                + ThreeItemConstValue.FooterHeight_Blood
                                + ThreeItemConstValue.FooterHeight_Liq
                                + ThreeItemConstValue.FooterHeight_DaBian
                                + ThreeItemConstValue.FooterHeight_NiaoLiang / 2f
                                ) * this.ParentColumn.SizePerUnit;

                centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                #endregion
            }
            else if (this.Value.measureType == 12)//排出量-其他
            {
                #region 排出量-其他
                //只有一个数据是，画在中间
                //2个画在上下
                //2个以上只画2个
                if (this.TypeCount == 1)
                {
                    centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                        + (this.ParentColumn.HeaderHeightUnit
                           + this.ParentColumn.BodyHeightUnit
                           + ThreeItemConstValue.FooterHeight_Breath
                           + ThreeItemConstValue.FooterHeight_Blood
                           + ThreeItemConstValue.FooterHeight_Liq
                           + ThreeItemConstValue.FooterHeight_DaBian
                           + ThreeItemConstValue.FooterHeight_NiaoLiang
                           + ThreeItemConstValue.FooterHeight_Other1 / 2f) * this.ParentColumn.SizePerUnit;

                    centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                }
                else if (this.TypeCount >= 2)
                {
                    if (this.Index == 0)
                    {
                        centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                            + (this.ParentColumn.HeaderHeightUnit
                               + this.ParentColumn.BodyHeightUnit
                               + ThreeItemConstValue.FooterHeight_Breath
                               + ThreeItemConstValue.FooterHeight_Blood
                               + ThreeItemConstValue.FooterHeight_Liq
                               + ThreeItemConstValue.FooterHeight_DaBian
                               + ThreeItemConstValue.FooterHeight_NiaoLiang
                               + ThreeItemConstValue.FooterHeight_Other1 / 4f) * this.ParentColumn.SizePerUnit;

                        centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                    }
                    else
                    {
                        centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                        + (this.ParentColumn.HeaderHeightUnit
                           + this.ParentColumn.BodyHeightUnit
                           + ThreeItemConstValue.FooterHeight_Breath
                           + ThreeItemConstValue.FooterHeight_Blood
                           + ThreeItemConstValue.FooterHeight_Liq
                           + ThreeItemConstValue.FooterHeight_DaBian
                           + ThreeItemConstValue.FooterHeight_NiaoLiang
                           + (ThreeItemConstValue.FooterHeight_Other1 / 4f) * 3f) * this.ParentColumn.SizePerUnit;

                        centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                    }
                }
                //centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                //                    + (this.ParentColumn.HeaderHeightUnit
                //                        + this.ParentColumn.BodyHeightUnit
                //                        + ThreeItemConstValue.FooterHeight_Breath
                //                        + ThreeItemConstValue.FooterHeight_Blood
                //                        + ThreeItemConstValue.FooterHeight_Liq
                //                        + ThreeItemConstValue.FooterHeight_DaBian
                //                        + ThreeItemConstValue.FooterHeight_NiaoLiang
                //                        + ThreeItemConstValue.FooterHeight_Other1 / 2f
                //                        ) * this.ParentColumn.SizePerUnit;

                //centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit; 
                #endregion
            }
            else if (this.Value.measureType == 13)//体重
            {
                #region 体重
                centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                            + (this.ParentColumn.HeaderHeightUnit
                                + this.ParentColumn.BodyHeightUnit
                                + ThreeItemConstValue.FooterHeight_Breath
                                + ThreeItemConstValue.FooterHeight_Blood
                                + ThreeItemConstValue.FooterHeight_Liq
                                + ThreeItemConstValue.FooterHeight_DaBian
                                + ThreeItemConstValue.FooterHeight_NiaoLiang
                                + ThreeItemConstValue.FooterHeight_Other1
                                + ThreeItemConstValue.FooterHeight_Weight / 2f
                                ) * this.ParentColumn.SizePerUnit;

                centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                #endregion
            }
            else if (this.Value.measureType == 14)//皮试
            {
                #region 皮试
                if (this.TypeCount == 1)
                {
                    centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                            + (this.ParentColumn.HeaderHeightUnit
                               + this.ParentColumn.BodyHeightUnit
                               + ThreeItemConstValue.FooterHeight_Breath
                               + ThreeItemConstValue.FooterHeight_Blood
                               + ThreeItemConstValue.FooterHeight_Liq
                               + ThreeItemConstValue.FooterHeight_DaBian
                               + ThreeItemConstValue.FooterHeight_NiaoLiang
                               + ThreeItemConstValue.FooterHeight_Other1
                               + ThreeItemConstValue.FooterHeight_Weight
                               + this.ParentColumn.ParentGrid.FooterHeight_PiShi / 2f) * this.ParentColumn.SizePerUnit;

                    centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                }
                else
                {
                    centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                            + (this.ParentColumn.HeaderHeightUnit
                               + this.ParentColumn.BodyHeightUnit
                               + ThreeItemConstValue.FooterHeight_Breath
                               + ThreeItemConstValue.FooterHeight_Blood
                               + ThreeItemConstValue.FooterHeight_Liq
                               + ThreeItemConstValue.FooterHeight_DaBian
                               + ThreeItemConstValue.FooterHeight_NiaoLiang
                               + ThreeItemConstValue.FooterHeight_Other1
                               + ThreeItemConstValue.FooterHeight_Weight
                               + (this.Index + 1) * 1.2f
                               ) * this.ParentColumn.SizePerUnit;

                    centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                }
                #endregion
            }
            else if (this.Value.measureType == 15)//其他
            {
                #region 其他
                //只有一个数据时，画在中间
                //2个画在上下
                //2个以上只画2个
                if (this.TypeCount == 1)
                {
                    centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                        + (this.ParentColumn.HeaderHeightUnit
                           + this.ParentColumn.BodyHeightUnit
                           + ThreeItemConstValue.FooterHeight_Breath
                           + ThreeItemConstValue.FooterHeight_Blood
                           + ThreeItemConstValue.FooterHeight_Liq
                           + ThreeItemConstValue.FooterHeight_DaBian
                           + ThreeItemConstValue.FooterHeight_NiaoLiang
                           + ThreeItemConstValue.FooterHeight_Other1
                           + ThreeItemConstValue.FooterHeight_Weight
                           + this.ParentColumn.ParentGrid.FooterHeight_PiShi
                           + this.ParentColumn.ParentGrid.m_fltOtherFooterHeigth / 2f
                           ) * this.ParentColumn.SizePerUnit;

                    centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                }
                else if (this.TypeCount >= 2)
                {
                    //if (this.Index == 0)
                    //{
                    centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                        + (this.ParentColumn.HeaderHeightUnit
                           + this.ParentColumn.BodyHeightUnit
                       + ThreeItemConstValue.FooterHeight_Breath
                       + ThreeItemConstValue.FooterHeight_Blood
                       + ThreeItemConstValue.FooterHeight_Liq
                       + ThreeItemConstValue.FooterHeight_DaBian
                       + ThreeItemConstValue.FooterHeight_NiaoLiang
                       + ThreeItemConstValue.FooterHeight_Other1
                       + ThreeItemConstValue.FooterHeight_Weight
                       + this.ParentColumn.ParentGrid.FooterHeight_PiShi
                           + (this.Index + 1) * 1.2f) * this.ParentColumn.SizePerUnit;

                    centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                    //}
                    //else
                    //{
                    //    centerTop = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset
                    //    + (this.ParentColumn.HeaderHeightUnit
                    //       + this.ParentColumn.BodyHeightUnit
                    //       + ThreeItemConstValue.FooterHeight_Breath
                    //       + ThreeItemConstValue.FooterHeight_Blood
                    //       + ThreeItemConstValue.FooterHeight_Liq
                    //       + ThreeItemConstValue.FooterHeight_DaBian
                    //       + ThreeItemConstValue.FooterHeight_NiaoLiang
                    //       + ThreeItemConstValue.FooterHeight_Other1
                    //       + ThreeItemConstValue.FooterHeight_Weight
                    //       + this.ParentColumn.ParentGrid.FooterHeight_PiShi
                    //       + (ThreeItemConstValue.FooterHeight_Other2 / 4f) * 3f) * this.ParentColumn.SizePerUnit;

                    //    centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + 3f * this.ParentColumn.SizePerUnit;
                    //}
                }
                #endregion
            }
            else
            {
                float pointSize = this.ParentColumn.SizePerUnit * 0.8f;

                //当前点的中心位置距离左端距离
                centerLeft = this.ParentColumn.ParentGrid.LeftOffset + this.ParentColumn.LeftOffset + innerOffsetUnit * this.ParentColumn.SizePerUnit + this.ParentColumn.SizePerUnit / 2f;

                float t = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset + this.ParentColumn.HeaderHeightUnit * this.ParentColumn.SizePerUnit;
                float b = this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset + (this.ParentColumn.HeaderHeightUnit + this.ParentColumn.BodyHeightUnit) * this.ParentColumn.SizePerUnit;


                float val = (float)Convert.ToDecimal(this.Value.measureValue);

                //体温
                if (this.Value.measureType == 1 || this.Value.measureType == 2
                    || this.Value.measureType == 3 || this.Value.measureType == 4
                    || this.Value.measureType == 16)
                {
                    centerTop = b - (val - ThreeItemConstValue.MinTemp) * (b - t) / (ThreeItemConstValue.MaxTemp - ThreeItemConstValue.MinTemp);
                }
                else if (this.Value.measureType == 5 || this.Value.measureType == 6)//心率脉搏
                {
                    //if (val > 200 && clsGlobalHospitalCode.Code == "0003")//还需判断医院
                    //{
                    //    val = 200;
                    //}
                    centerTop = b - (val - ThreeItemConstValue.MinPulse) * (b - t) / (ThreeItemConstValue.MaxPulse - ThreeItemConstValue.MinPulse);
                }
            }
            this.PointCenterPos = new PointF(centerLeft, centerTop);

        }

        public List<ValuePoint> SubValuePoints { get; set; }

        /// <summary>
        /// 外框宽度
        /// </summary>
        public float BorderWidth { get; private set; }

        public float LineWidth { get; set; }

        /// <summary>
        /// 直径
        /// </summary>
        public float Diameter { get; private set; }

        /// <summary>
        /// 中心点位置
        /// </summary>
        public PointF PointCenterPos { get; set; }

        /// <summary>
        /// 画点所在日期
        /// </summary>
        public DateTime PointDate { get; private set; }

        /// <summary>
        /// 时间区间标识
        /// </summary>
        public eumnThreeItemsTimePeriod TimePeriod
        {
            get
            {
                if (this.Value != null)
                {
                    return this.Value.timePeriod;
                }
                else
                {
                    return eumnThreeItemsTimePeriod.TimeSpan_4;
                }
            }
        }

        /// <summary>
        /// 呼吸点画的位置(True-上面,Flase-下面)
        /// </summary>
        public bool BlnDrawUp
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public int PointValueType
        {
            get
            {
                if (this.Value != null)
                {
                    return (int)Value.measureType;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 对应的数据点值
        /// </summary>
        public EntityEmrTemperatureMonitorData Value { get; set; }

        ///// <summary>
        ///// 是否跟前一个点连接
        ///// </summary>
        //public bool Connect { get; set; }

        /// <summary>
        /// 连接线文字
        /// </summary>
        public string LineText { get; set; }

        /// <summary>
        /// 顺序号，根据顺序号画在不同的地方
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 同类型的数量
        /// </summary>
        public int TypeCount { get; set; }

        Font TextFont;

        #region 坐标相关

        /// <summary>
        /// 获取中心点坐标
        /// </summary>
        /// <returns></returns>
        public PointF GetCenterPosition()
        {
            return this.PointCenterPos;
        }

        /// <summary>
        /// 左坐标（中心点减半径）
        /// </summary>
        public float X1
        {
            get
            {
                if (this.PointValueType == 7)//呼吸
                {
                    return this.GetCenterPosition().X - this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 8)//血压
                {
                    if (this.TypeCount == 1)
                    {
                        return this.GetCenterPosition().X - 3 * this.ParentColumn.SizePerUnit;
                    }
                    else if (this.TypeCount == 2)
                    {
                        return this.GetCenterPosition().X - 3 * this.ParentColumn.SizePerUnit;
                    }
                    else
                    {
                        return this.GetCenterPosition().X - 6 * this.ParentColumn.SizePerUnit / 4f;
                    }
                }
                else if (this.PointValueType == 9 || this.PointValueType == 10 || this.PointValueType == 11 || this.PointValueType == 12 || this.PointValueType == 13 || this.PointValueType == 14 || this.PointValueType == 15)
                {
                    return this.GetCenterPosition().X - 3 * this.ParentColumn.SizePerUnit;
                }
                else
                {
                    return this.GetCenterPosition().X - this.Diameter / 2f;
                }
            }
        }

        /// <summary>
        /// 右侧坐标 （中心点加半径）
        /// </summary>
        public float X2
        {
            get
            {
                if (this.PointValueType == 7)//呼吸
                {
                    return this.GetCenterPosition().X + this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 8)//血压
                {
                    if (this.TypeCount == 1)
                    {
                        return this.GetCenterPosition().X + 3 * this.ParentColumn.SizePerUnit;
                    }
                    else if (this.TypeCount == 2)
                    {
                        return this.GetCenterPosition().X + 3 * this.ParentColumn.SizePerUnit;
                    }
                    else
                    {
                        return this.GetCenterPosition().X + 6 * this.ParentColumn.SizePerUnit / 4f;
                    }
                }
                else if (this.PointValueType == 9 || this.PointValueType == 10 || this.PointValueType == 11 || this.PointValueType == 12 || this.PointValueType == 13 || this.PointValueType == 14 || this.PointValueType == 15)
                {
                    return this.GetCenterPosition().X + 3 * this.ParentColumn.SizePerUnit;
                }
                else
                {
                    return this.GetCenterPosition().X + this.Diameter / 2f;
                }
            }
        }

        public float Y1
        {
            get
            {
                if (this.PointValueType == 7)//呼吸
                {
                    return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Breath * this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 8)//血压
                {
                    if (this.TypeCount == 1)
                    {
                        return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 4f;
                    }
                    else if (this.TypeCount == 2)
                    {
                        return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 4f;
                    }
                    else
                    {
                        return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 4f;
                    }
                }
                else if (this.PointValueType == 9)//总入液量
                {
                    return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Liq * this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 10)//大便
                {
                    return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_DaBian * this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 11)//大便
                {
                    return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_NiaoLiang * this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 12)//排出量-其他1
                {
                    //return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 2f;
                    if (this.TypeCount == 1)
                    {
                        return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 2f;
                    }
                    else// if (this.TypeCount == 2)
                    {
                        return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 4f;
                    }
                }
                else if (this.PointValueType == 13)//体重
                {
                    return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Weight * this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 14)//皮试
                {
                    if (this.TypeCount == 1)
                    {
                        return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 2f;
                    }
                    else
                    {
                        return this.GetCenterPosition().Y - (1.2f / 2f) * this.ParentColumn.SizePerUnit;
                    }
                }
                else if (this.PointValueType == 15)//其他
                {
                    //return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 2f;
                    if (this.TypeCount == 1)
                    {
                        return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Other2 * this.ParentColumn.SizePerUnit / 2f;
                    }
                    else// if (this.TypeCount == 2)
                    {
                        return this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Other2 * this.ParentColumn.SizePerUnit / 4f;
                    }
                }
                else
                {
                    return this.GetCenterPosition().Y - this.Diameter / 2f;
                }
            }
        }

        public float Y2
        {
            get
            {
                if (this.PointValueType == 7)//呼吸
                {
                    return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Breath * this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 8)//血压
                {
                    if (this.TypeCount == 1)
                    {
                        return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 4f;
                    }
                    else if (this.TypeCount == 2)
                    {
                        return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 4f;
                    }
                    else
                    {
                        return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 4f;
                    }
                }
                else if (this.PointValueType == 9)//总入液量
                {
                    return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Liq * this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 10)//大便
                {
                    return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_DaBian * this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 11)//小便
                {
                    return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_NiaoLiang * this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 12)//其他1
                {
                    //return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 2f;
                    if (this.TypeCount == 1)
                    {
                        return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 2f;
                    }
                    else// if (this.TypeCount == 2)
                    {
                        return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 4f;
                    }
                }
                else if (this.PointValueType == 13)//体重
                {
                    return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Weight * this.ParentColumn.SizePerUnit / 2f;
                }
                else if (this.PointValueType == 14)//皮试
                {
                    //return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 2f;
                    if (this.TypeCount == 1)
                    {
                        return this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 2f;
                    }
                    else// if (this.TypeCount == 2)
                    {
                        return this.GetCenterPosition().Y + (1.2f / 2f) * this.ParentColumn.SizePerUnit;
                    }
                }
                else
                {
                    return this.GetCenterPosition().Y + this.Diameter / 2f;
                }
            }
        }

        #endregion

        /// <summary>
        /// 描绘图像
        /// </summary>
        public void Draw()
        {
            try
            {
                if (PointValueType == -1)
                {
                    return;
                }
                else if (PointValueType == 1)//口表
                {
                    if (this.OverlapType == PointOverlapType.None)
                    {
                        DrawPoint(ThreeItemConstValue.ColorTempu_KouBiao);
                    }
                }
                else if (PointValueType == 2)//腋表
                {
                    if (this.OverlapType == PointOverlapType.None)
                    {
                        DrawCrox(ThreeItemConstValue.ColorTempu_YeBiao);
                    }
                }
                else if (PointValueType == 3)//肛表
                {
                    if (this.OverlapType == PointOverlapType.None)
                    {
                        DrawCircle(ThreeItemConstValue.ColorTempu_GangBiao);
                    }
                }
                else if (PointValueType == 4)//降温
                {
                    DrawCircle(ThreeItemConstValue.ColorTempu_DowmTemp);
                }
                else if (PointValueType == 5)//脉搏
                {
                    if (this.OverlapType == PointOverlapType.None)
                    {
                        DrawPoint(ThreeItemConstValue.ColorTempu_MaiBo);
                    }
                    else if (this.OverlapType == PointOverlapType.Type1)
                    {
                        DrawCrox(ThreeItemConstValue.ColorTempu_YeBiao);
                        DrawCircle(ThreeItemConstValue.ColorTempu_MaiBo);
                    }
                    else if (this.OverlapType == PointOverlapType.Type2)
                    {
                        DrawPoint(ThreeItemConstValue.ColorTempu_MaiBo);
                        DrawCircle(ThreeItemConstValue.ColorTempu_KouBiao);
                    }
                    else if (this.OverlapType == PointOverlapType.Type3)
                    {
                        DrawPoint(ThreeItemConstValue.ColorTempu_KouBiao);
                        DrawCircle(ThreeItemConstValue.ColorTempu_MaiBo);
                    }
                }
                else if (PointValueType == 6)//心率
                {
                    DrawCircle(ThreeItemConstValue.ColorTempu_XinLv);
                }
                else if (PointValueType == 7)//呼吸
                {
                    PointF textLocation;

                    float fontSize = this.ParentColumn.SizePerUnit * 0.6f;

                    if (this.BlnDrawUp)//写在上方
                    {
                        textLocation = new PointF(this.GetCenterPosition().X - this.ParentColumn.SizePerUnit / 2f, this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Breath * this.ParentColumn.SizePerUnit / 2.5f);
                    }
                    else//写在下方
                    {
                        textLocation = new PointF(this.GetCenterPosition().X - this.ParentColumn.SizePerUnit / 2f, this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Breath * this.ParentColumn.SizePerUnit / 2f - fontSize * 1.5f);
                    }

                    if (!string.IsNullOrEmpty(Value.measureValue2))
                    {
                        this.DrawText(textLocation, Value.measureValue2, new Font(ThreeItemConstValue.DefaultFontFamilyName, fontSize), Color.Blue, false);
                    }
                }
                else if (PointValueType == 8)//血压
                {
                    #region 血压

                    string text = this.Value.measureValue + "/" + this.Value.measureValue2;

                    PointF textLocation = new PointF(0, 0);
                    if (this.TypeCount == 1)
                    {
                        this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit / 1.3f);
                        SizeF textSize = this.ParentColumn.graphics.MeasureString(text, this.TextFont);

                        if (this.Value.recordDate.Hour > 12)
                        {
                            textLocation = new PointF(this.GetCenterPosition().X + this.ParentColumn.SizePerUnit * 3f - textSize.Width, this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 4 - textSize.Height * 1.1f);
                        }
                        else
                        {
                            textLocation = new PointF(this.GetCenterPosition().X - this.ParentColumn.SizePerUnit * 3f, this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 6f);
                        }
                    }
                    else if (this.TypeCount == 2)
                    {
                        this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit / 1.3f);
                        SizeF textSize = this.ParentColumn.graphics.MeasureString(text, this.TextFont);

                        if (this.Index == 0)
                        {
                            textLocation = new PointF(this.GetCenterPosition().X - this.ParentColumn.SizePerUnit * 3f, this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 6f);
                        }
                        else
                        {
                            textLocation = new PointF(this.GetCenterPosition().X + this.ParentColumn.SizePerUnit * 3f - textSize.Width, this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 4 - textSize.Height * 1.1f);
                        }
                    }
                    else if (this.TypeCount > 2)
                    {
                        this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit / 1.78f);

                        SizeF textSize = this.ParentColumn.graphics.MeasureString(text, this.TextFont);

                        if (this.Index == 0)
                        {
                            textLocation = new PointF(this.GetCenterPosition().X - this.ParentColumn.SizePerUnit * 1.5f, this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 6f);
                        }
                        else if (this.Index == 1)
                        {
                            textLocation = new PointF(this.GetCenterPosition().X + this.ParentColumn.SizePerUnit * 1.5f - textSize.Width, this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 6f);
                        }
                        else if (this.Index == 2)
                        {
                            textLocation = new PointF(this.GetCenterPosition().X - this.ParentColumn.SizePerUnit * 1.5f, this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 4f - textSize.Height * 1.1f);
                        }
                        else if (this.Index == 3)
                        {
                            textLocation = new PointF(this.GetCenterPosition().X + this.ParentColumn.SizePerUnit * 1.5f - textSize.Width, this.GetCenterPosition().Y + ThreeItemConstValue.FooterHeight_Blood * this.ParentColumn.SizePerUnit / 4f - textSize.Height * 1.1f);
                        }
                    }

                    this.DrawText(textLocation, text, this.TextFont, Color.Blue, false);
                    #endregion
                }
                else if (PointValueType == 9 || PointValueType == 10 || PointValueType == 11 || PointValueType == 13)//总入液量
                {
                    #region MyRegion
                    this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit);

                    string text = string.Empty;

                    if (PointValueType == 13)//体重
                    {
                        decimal decVal = -1m;

                        //如果是数字
                        if (decimal.TryParse(this.Value.measureValue2, out decVal))
                        {
                            text = string.Format("{0} {1}", this.Value.measureValue, this.Value.measureValue2);
                        }
                        else
                        {
                            text = string.Format("{0}", this.Value.measureValue);
                        }
                    }
                    //else if ((PointValueType == 9 || PointValueType == 11) && com.HopeBridge.common.utility.clsGlobalHospitalCode.Code == "0002")
                    //{
                    //    text = string.Format("{0}", this.Value.Measurevalue_vchr);
                    //}
                    else
                    {
                        text = string.Format("{0} {1}", this.Value.measureValue, this.Value.measureValue2);
                    }

                    SizeF textSize = this.ParentColumn.graphics.MeasureString(text, this.TextFont);

                    PointF textLocation = new PointF(this.GetCenterPosition().X - textSize.Width / 2f, this.GetCenterPosition().Y - textSize.Height / 2f);
                    this.DrawText(textLocation, text, new Font(ThreeItemConstValue.DefaultFontFamilyName, 12f), Color.Blue, false);
                    #endregion
                }
                else if (PointValueType == 12)//排出量-其他
                {
                    string text = this.Value.measureValue + " " + this.Value.measureValue2;

                    PointF textLocation = new PointF(0, 0);
                    if (this.TypeCount == 1)
                    {
                        this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit * 0.8f);

                        SizeF textSize = this.ParentColumn.graphics.MeasureString(text, this.TextFont);
                        textLocation = new PointF(this.GetCenterPosition().X - textSize.Width / 2f, this.GetCenterPosition().Y - textSize.Height / 2f);

                        this.DrawText(textLocation, text, this.TextFont, Color.Blue, false);
                    }
                    else if (this.TypeCount >= 2)
                    {
                        this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit / 1.5f);
                        SizeF textSize = this.ParentColumn.graphics.MeasureString(text, this.TextFont);

                        if (this.Index == 0)
                        {
                            textLocation = new PointF(this.GetCenterPosition().X - this.ParentColumn.SizePerUnit * 3f, this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 5f);
                        }
                        else
                        {
                            textLocation = new PointF(this.GetCenterPosition().X - this.ParentColumn.SizePerUnit * 3f, this.GetCenterPosition().Y - ThreeItemConstValue.FooterHeight_Other1 * this.ParentColumn.SizePerUnit / 5f);
                        }

                        this.DrawText(textLocation, text, this.TextFont, Color.Blue, false);
                    }
                }
                else if (PointValueType == 14)//皮试
                {
                    string text = string.Format("{0}({1})", this.Value.measureValue, this.Value.measureValue2);
                    PointF textLocation = new PointF(0, 0);
                    Color fntForeColor = Color.Blue;
                    if (text.Contains("(+"))
                    {
                        fntForeColor = Color.Red;
                    }

                    if (this.TypeCount == 1)
                    {
                        this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit * 0.8f);
                    }
                    else
                    {
                        this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit * 0.65f);
                    }
                    if (System.Text.Encoding.Default.GetBytes(text).Length > 14)
                        this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit * 0.65f);
                    List<string> lstValue = new List<string>();
                    if (System.Text.Encoding.Default.GetBytes(text).Length > 14)
                    {
                        int pos = 0;
                        for (int k = 1; k <= text.Length; k++)
                        {
                            if (System.Text.Encoding.Default.GetBytes(text.Substring(pos, k - pos)).Length == 14)
                            {
                                lstValue.Add(text.Substring(pos, k - pos));
                                pos = k;
                                if (k < text.Length && System.Text.Encoding.Default.GetBytes(text.Substring(pos)).Length < 14)
                                {
                                    lstValue.Add(text.Substring(k));
                                    pos = text.Length;
                                }
                                if (pos == text.Length) break;
                            }
                        }
                    }
                    if (lstValue.Count == 0)
                        lstValue.Add(text);

                    SizeF textSize = this.ParentColumn.graphics.MeasureString(text, this.TextFont);
                    float y = this.GetCenterPosition().Y - textSize.Height / 2f;
                    foreach (string strVale in lstValue)
                    {
                        textLocation = new PointF(this.GetCenterPosition().X - 3 * this.ParentColumn.SizePerUnit, y);
                        this.DrawText(textLocation, strVale, this.TextFont, fntForeColor, false);
                        y += textSize.Height;
                    }
                }
                else if (PointValueType == 15)//其他
                {
                    string text = this.Value.measureValue + " " + this.Value.measureValue2;

                    PointF textLocation = new PointF(0, 0);
                    if (this.TypeCount == 1)
                    {
                        this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit * 0.8f);

                        if (System.Text.Encoding.Default.GetBytes(text).Length > 14)
                            this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit * 0.65f);
                        List<string> lstValue = new List<string>();
                        if (System.Text.Encoding.Default.GetBytes(text).Length > 14)
                        {
                            int pos = 0;
                            for (int k = 1; k <= text.Length; k++)
                            {
                                if (System.Text.Encoding.Default.GetBytes(text.Substring(pos, k - pos)).Length == 14)
                                {
                                    lstValue.Add(text.Substring(pos, k - pos));
                                    pos = k;
                                    if (k < text.Length && System.Text.Encoding.Default.GetBytes(text.Substring(pos)).Length < 14)
                                    {
                                        lstValue.Add(text.Substring(k));
                                        pos = text.Length;
                                    }
                                    if (pos == text.Length) break;
                                }
                            }
                        }
                        if (lstValue.Count == 0)
                            lstValue.Add(text);

                        SizeF textSize = this.ParentColumn.graphics.MeasureString(text, this.TextFont);
                        float y = this.GetCenterPosition().Y - textSize.Height / 2f;
                        foreach (string strVale in lstValue)
                        {
                            textLocation = new PointF(this.GetCenterPosition().X - 3 * this.ParentColumn.SizePerUnit, y);
                            this.DrawText(textLocation, strVale, this.TextFont, Color.Blue, false);
                            y += textSize.Height;
                        }
                    }
                    else
                    {
                        this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit * 0.65f);
                        if (System.Text.Encoding.Default.GetBytes(text).Length > 14)
                            this.TextFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.ParentColumn.SizePerUnit * 0.65f);
                        List<string> lstValue = new List<string>();
                        if (System.Text.Encoding.Default.GetBytes(text).Length > 14)
                        {
                            int pos = 0;
                            for (int k = 1; k <= text.Length; k++)
                            {
                                if (System.Text.Encoding.Default.GetBytes(text.Substring(pos, k - pos)).Length == 14)
                                {
                                    lstValue.Add(text.Substring(pos, k - pos));
                                    pos = k;
                                    if (k < text.Length && System.Text.Encoding.Default.GetBytes(text.Substring(pos)).Length < 14)
                                    {
                                        lstValue.Add(text.Substring(k));
                                        pos = text.Length;
                                    }
                                    if (pos == text.Length) break;
                                }
                            }
                        }
                        if (lstValue.Count == 0)
                            lstValue.Add(text);

                        SizeF textSize = this.ParentColumn.graphics.MeasureString(text, this.TextFont);
                        float y = this.GetCenterPosition().Y - textSize.Height / 2f;
                        foreach (string strVale in lstValue)
                        {
                            textLocation = new PointF(this.GetCenterPosition().X - 3 * this.ParentColumn.SizePerUnit, y);
                            this.DrawText(textLocation, strVale, this.TextFont, Color.Blue, false);
                            y += textSize.Height;
                        }
                        this.PointCenterPos = new PointF(this.PointCenterPos.X, textLocation.Y);
                    }
                }
                else if (PointValueType == 16)//亚低温治疗（跟肛表一个画法）
                {
                    if (this.OverlapType == PointOverlapType.None)
                    {
                        DrawCircle(ThreeItemConstValue.ColorTempu_GangBiao);
                    }
                }
                else
                {
                    return;
                }

                Pen pen = new Pen(Color.Black);
                pen.Width = this.LineWidth;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                //连降温点
                foreach (ValuePoint subPoint in this.SubValuePoints)
                {
                    subPoint.Draw();


                    decimal subPointValue = Convert.ToDecimal(subPoint.Value.measureValue);
                    decimal currentPointValue = Convert.ToDecimal(this.Value.measureValue);

                    if (subPoint.PointValueType == 4)
                    {
                        pen.Color = Color.Red;

                        if (subPointValue < currentPointValue)
                        {
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        }
                        else if (subPointValue > currentPointValue)
                        {
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        }
                        else
                        {
                            continue;
                        }
                        this.ParentColumn.graphics.DrawLine(pen, this.PointCenterPos, subPoint.PointCenterPos);
                    }
                    else
                    {
                        continue;
                    }

                    //this.ParentColumn.graphics.DrawLine(pen, this.PointCenterPos, subPoint.PointCenterPos);
                }

                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                //连体温点(先连当天当前时间之前的点，再连当前之前时间的点)
                if (PointValueType == 1 || PointValueType == 2 || PointValueType == 3 || PointValueType == 16)
                {
                    //连本列点,当前点的前一时刻点
                    bool found = false;
                    ValuePoint prevValuePoint = null;

                    for (int i = (int)this.Value.timePeriod - 1; i >= (int)eumnThreeItemsTimePeriod.TimeSpan_4; i--)
                    {
                        if (this.ParentColumn.TempuPoint.Any(item => item.Value != null && (int)item.Value.timePeriod == i))
                        {
                            found = true;

                            prevValuePoint = this.ParentColumn.TempuPoint.Find(item => item.Value != null && (int)item.Value.timePeriod == i);

                            break;
                        }

                        if (found)
                        {
                            break;
                        }
                    }

                    //当天找不到则找前一天（循环）
                    if (found == false)
                    {
                        DrawingDataColumn prevColumn = this.ParentColumn.PrevVisibleColumn;

                        while (prevColumn != null && found == false)
                        {
                            for (int i = (int)eumnThreeItemsTimePeriod.TimeSpan_24; i >= (int)eumnThreeItemsTimePeriod.TimeSpan_4; i--)
                            {
                                if (prevColumn.TempuPoint.Any(item => item.Value != null && (int)item.Value.timePeriod == i))
                                {
                                    found = true;

                                    prevValuePoint = prevColumn.TempuPoint.Find(item => item.Value != null && (int)item.Value.timePeriod == i);

                                    //pen.Color = ThreeItemConstValue.ColorTempu_KouBiao;
                                    //this.ParentColumn.graphics.DrawLine(pen, this.PointCenterPos, prevPoint.PointCenterPos);
                                    //prevPos = prevPoint.PointCenterPos;
                                    break;
                                }
                            }

                            prevColumn = prevColumn.PrevVisibleColumn;
                        }
                    }

                    if (found && prevValuePoint != null && prevValuePoint.Value != null)//找到上一个连线点
                    {
                        DateTime prevDate = prevValuePoint.Value.recordDate;
                        DateTime currDate = this.Value.recordDate;

                        //this.ParentColumn.ParentGrid.ColumnsData.Any(i=>i.EventValues.Any(n=>n.Recorddate_dat >
                        bool isbreaking = false;
                        foreach (var item in this.ParentColumn.ParentGrid.ColumnsData)
                        {
                            if (item.EventValues.Any(i => i.recordDate > prevDate && i.recordDate < currDate && ((i.EventType != null && i.EventType.isBreakingPoint == 1) || i.isBreakingPoint == 1)))
                            {
                                isbreaking = true;
                                break;
                            }
                        }

                        if (!isbreaking)
                        {
                            pen.Color = ThreeItemConstValue.ColorTempu_KouBiao;
                            this.ParentColumn.graphics.DrawLine(pen, this.PointCenterPos, prevValuePoint.PointCenterPos);
                        }
                    }
                }

                //连脉搏点
                if (PointValueType == 5)
                {
                    //连本列点,当前点的前一时刻点
                    bool found = false;
                    ValuePoint prevValuePoint = null;

                    for (int i = (int)this.Value.timePeriod - 1; i >= (int)eumnThreeItemsTimePeriod.TimeSpan_4; i--)
                    {
                        if (this.ParentColumn.PulsePoints.Any(item => item.Value != null && (int)item.Value.timePeriod == i))
                        {
                            found = true;

                            prevValuePoint = this.ParentColumn.PulsePoints.Find(item => item.Value != null && (int)item.Value.timePeriod == i);

                            //pen.Color = ThreeItemConstValue.ColorTempu_MaiBo;
                            //this.ParentColumn.graphics.DrawLine(pen, this.PointCenterPos, prevPoint.PointCenterPos);

                            break;
                        }

                        if (found)
                        {
                            break;
                        }
                    }

                    //找不到本列前一列
                    if (found == false)
                    {
                        DrawingDataColumn prevColumn = this.ParentColumn.PrevVisibleColumn;

                        while (prevColumn != null && found == false)
                        {
                            for (int i = (int)eumnThreeItemsTimePeriod.TimeSpan_24; i >= (int)eumnThreeItemsTimePeriod.TimeSpan_4; i--)
                            {
                                if (prevColumn.PulsePoints.Any(item => item.Value != null && (int)item.Value.timePeriod == i))
                                {
                                    found = true;

                                    prevValuePoint = prevColumn.PulsePoints.Find(item => item.Value != null && (int)item.Value.timePeriod == i);

                                    //pen.Color = ThreeItemConstValue.ColorTempu_MaiBo;
                                    //this.ParentColumn.graphics.DrawLine(pen, this.PointCenterPos, prevPoint.PointCenterPos);

                                    break;
                                }
                            }

                            prevColumn = prevColumn.PrevVisibleColumn;
                        }
                    }


                    if (found && prevValuePoint != null && prevValuePoint.Value != null)//找到上一个连线点
                    {
                        DateTime prevDate = prevValuePoint.Value.recordDate;
                        DateTime currDate = this.Value.recordDate;

                        //this.ParentColumn.ParentGrid.ColumnsData.Any(i=>i.EventValues.Any(n=>n.Recorddate_dat >
                        bool isbreaking = false;

                        foreach (var item in this.ParentColumn.ParentGrid.ColumnsData)
                        {
                            if (item.EventValues.Any(i => i.recordDate > prevDate && i.recordDate < currDate && i.EventType != null && i.EventType.isBreakingPoint == 1))
                            {
                                isbreaking = true;
                                break;
                            }
                        }

                        //东华医院特殊处理，时间超过24小时(并且中间有心率数据) (该单没有心率值)
                        bool blnNoBreaking = true;
                        //if (clsGlobalHospitalCode.Code == "0002")
                        //{
                        //    //1,是否有心率值
                        //    List<ValuePoint> lstHearRatePoint = new List<ValuePoint>();
                        //    foreach (var objCols in this.ParentColumn.ParentGrid.VisibleColumns)
                        //    {
                        //        lstHearRatePoint.AddRange(objCols.HeartRatePoints);
                        //    }
                        //    if (lstHearRatePoint.Count > 0)
                        //    {
                        //        //2,判断时间（超过24小时）
                        //        TimeSpan tsTime = currDate - prevDate;
                        //        if (tsTime.TotalHours > 24)
                        //        {
                        //            //3,该时间内有心率值
                        //            ValuePoint heartRatePoint = lstHearRatePoint.Find(item => item.PointCenterPos.X != prevValuePoint.PointCenterPos.X && item.PointCenterPos.X != this.PointCenterPos.X 
                        //                && item.Value.Recorddate_dat > prevDate && item.Value.Recorddate_dat < currDate);
                        //            if (heartRatePoint != null)
                        //            {
                        //                blnNoBreaking = false;
                        //            }
                        //        }
                        //    }

                        //}


                        if (!isbreaking && blnNoBreaking)
                        {
                            pen.Color = ThreeItemConstValue.ColorTempu_MaiBo;
                            this.ParentColumn.graphics.DrawLine(pen, this.PointCenterPos, prevValuePoint.PointCenterPos);
                        }
                    }
                }

                //连心率点
                if (PointValueType == 6)
                {

                    //连本列点,当前点的前一时刻点
                    bool found = false;
                    ValuePoint prevValuePoint = null;

                    for (int i = (int)this.Value.timePeriod - 1; i >= (int)eumnThreeItemsTimePeriod.TimeSpan_4; i--)
                    {
                        if (this.ParentColumn.HeartRatePoints.Any(item => item.Value != null && (int)item.Value.timePeriod == i))
                        {
                            found = true;

                            prevValuePoint = this.ParentColumn.HeartRatePoints.Find(item => item.Value != null && (int)item.Value.timePeriod == i);

                            //pen.Color = ThreeItemConstValue.ColorTempu_XinLv;
                            //this.ParentColumn.graphics.DrawLine(pen, this.PointCenterPos, prevPoint.PointCenterPos);

                            break;
                        }

                        if (found)
                        {
                            break;
                        }
                    }

                    //找不到本列前一列
                    if (found == false)
                    {
                        DrawingDataColumn prevColumn = this.ParentColumn.PrevVisibleColumn;

                        while (prevColumn != null && found == false)
                        {
                            for (int i = (int)eumnThreeItemsTimePeriod.TimeSpan_24; i >= (int)eumnThreeItemsTimePeriod.TimeSpan_4; i--)
                            {
                                if (prevColumn.HeartRatePoints.Any(item => item.Value != null && (int)item.Value.timePeriod == i))
                                {
                                    found = true;

                                    prevValuePoint = prevColumn.HeartRatePoints.Find(item => item.Value != null && (int)item.Value.timePeriod == i);

                                    //pen.Color = ThreeItemConstValue.ColorTempu_XinLv;
                                    //this.ParentColumn.graphics.DrawLine(pen, this.PointCenterPos, prevPoint.PointCenterPos);

                                    break;
                                }
                            }

                            prevColumn = prevColumn.PrevVisibleColumn;
                        }
                    }

                    if (found && prevValuePoint != null && prevValuePoint.Value != null)//找到上一个连线点
                    {
                        DateTime prevDate = prevValuePoint.Value.recordDate;
                        DateTime currDate = this.Value.recordDate;

                        bool isbreaking = false;
                        foreach (ThreeItemsColumnData item in this.ParentColumn.ParentGrid.ColumnsData)
                        {
                            if (item.EventValues.Any(i => i.recordDate > prevDate && i.recordDate < currDate && i.EventType != null && i.EventType.isBreakingPoint == 1))
                            {
                                isbreaking = true;
                                break;
                            }                            
                        }

                        //东华医院特殊处理，时间超过24小时，断开连接
                        bool blnNoBreaking = true;
                        //if (clsGlobalHospitalCode.Code == "0002")
                        //{
                        //    //1,是否有脉搏值
                        //    List<ValuePoint> lstPulsePoint = new List<ValuePoint>();
                        //    foreach (var objCols in this.ParentColumn.ParentGrid.VisibleColumns)
                        //    {
                        //        lstPulsePoint.AddRange(objCols.PulsePoints);
                        //    }
                        //    if (lstPulsePoint.Count > 0)
                        //    {
                        //        //2,判断时间（超过24小时）
                        //        TimeSpan tsTime = currDate - prevDate;
                        //        if (tsTime.TotalHours > 24)
                        //        {
                        //            //3,该时间内有心率值
                        //            ValuePoint pulsePoint = lstPulsePoint.Find(item => item.PointCenterPos.X != prevValuePoint.PointCenterPos.X && item.PointCenterPos.X != this.PointCenterPos.X &&
                        //                item.Value.Recorddate_dat > prevDate && item.Value.Recorddate_dat < currDate);
                        //            if (pulsePoint != null)
                        //            {
                        //                blnNoBreaking = false;
                        //            }
                        //        }
                        //    }
                        //}

                        if (!isbreaking && blnNoBreaking)
                        {
                            pen.Color = ThreeItemConstValue.ColorTempu_XinLv;
                            this.ParentColumn.graphics.DrawLine(pen, this.PointCenterPos, prevValuePoint.PointCenterPos);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 画圈
        /// </summary>
        /// <param name="pointCenterPos"></param>
        /// <param name="color"></param>
        private void DrawCircle(Color color)
        {
            try
            {
                this.ParentColumn.DrawCircle(this.PointCenterPos, color, this.Diameter, this.BorderWidth);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// 画点
        /// </summary>
        /// <param name="pointCenterPos"></param>
        /// <param name="color"></param>
        private void DrawPoint(Color color)
        {
            this.ParentColumn.DrawPoint(this.PointCenterPos, color, this.Diameter, this.BorderWidth);
        }

        /// <summary>
        /// 画叉
        /// </summary>
        /// <param name="pointCenterPos"></param>
        /// <param name="color"></param>
        private void DrawCrox(Color color)
        {
            this.ParentColumn.DrawCrox(this.PointCenterPos, color, this.Diameter * 0.8f, this.BorderWidth);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="foreColor"></param>
        /// <param name="isVeri"></param>
        private void DrawText(PointF location, string text, Font font, Color foreColor, bool isVeri)
        {
            this.ParentColumn.DrawText(location, text, font, foreColor, isVeri);
        }

        public override string ToString()
        {
            if (this.Value == null)
            {
                return string.Empty;
            }
            else
            {
                string typename = string.Empty;
                string valuename = string.Empty;

                string value1 = this.Value.measureValue;
                string value2 = this.Value.measureValue2;

                if (this.PointValueType == 1)
                {
                    typename = "口表";
                }
                else if (this.PointValueType == 2)
                {
                    typename = "腋表";
                }
                else if (this.PointValueType == 3)
                {
                    typename = "肛表";
                }
                else if (this.PointValueType == 4)
                {
                    typename = "降温";
                }
                else if (this.PointValueType == 5)
                {
                    typename = "脉搏";
                }
                else if (this.PointValueType == 6)
                {
                    typename = "心率";
                }
                else if (this.PointValueType == 7)
                {
                    typename = "呼吸";
                }
                else if (this.PointValueType == 8)
                {
                    typename = "血压";
                    value1 += "/";
                    value2 += "mmHg";
                }
                else if (this.PointValueType == 9)
                {
                    typename = "总入液量";
                }
                else if (this.PointValueType == 10)
                {
                    typename = "大便";
                }
                else if (this.PointValueType == 11)
                {
                    typename = "尿量";
                }
                else if (this.PointValueType == 12)
                {
                    typename = "排出量-其他";
                }
                else if (this.PointValueType == 13)
                {
                    typename = "体重";

                    decimal decVal = -1m;

                    if (!decimal.TryParse(value2, out decVal))
                    {
                        value1 = string.Empty;
                    }
                    else
                    {
                        value2 = value2 + "Kg";
                    }
                }
                else if (this.PointValueType == 14)
                {
                    typename = "皮试";
                }
                else if (this.PointValueType == 15)
                {
                    typename = "其他";
                }
                else if (this.PointValueType == 16)
                {
                    typename = "亚低温治疗";
                }

                string text = string.Format(@"
时  间：{0}
类  型：{1}
当前值：{2} {3}

", this.Value.recordDate.ToString("yyyy年MM月dd日 HH时mm分"), typename, value1, value2);


                return text;
            }

        }
    }
}
