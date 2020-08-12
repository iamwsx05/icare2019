using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    public class DrawingDataColumn : ColumnBase
    {
        public DrawingDataColumn(DrawingGrid parent, ThreeItemsColumnData val)
            : base(parent)
        {
            string regId = parent.PatInfo.RegID;
            this.m_mthSetVisible(regId, parent.PageIndex, true);
            this.TempuPoint = new List<ValuePoint>();
            this.PulsePoints = new List<ValuePoint>();
            this.HeartRatePoints = new List<ValuePoint>();
            this.BreathPoint = new List<ValuePoint>();
            this.BloodPressurePoint = new List<ValuePoint>();
            this.TotalLiqPoint = null;
            this.ExcrementPoint = null;
            this.UrinePoint = null;
            this.Other1Point = new List<ValuePoint>();
            this.PeauTestPoint = new List<ValuePoint>();
            this.Other2Point = new List<ValuePoint>();
            this.EventPoints = new List<EventPoint>();

            this.Value = val;

            this.OperationCount = 0;
            this.HasOperation = false;

            if (val != null)
            {
                this.Date = this.Value.RecDate;
            }
        }

        public List<ValuePoint> TempuPoint { get; set; }
        public List<ValuePoint> PulsePoints { get; set; }
        public List<ValuePoint> HeartRatePoints { get; set; }
        public List<ValuePoint> BreathPoint { get; set; }
        public List<ValuePoint> BloodPressurePoint { get; set; }
        public ValuePoint TotalLiqPoint { get; set; }
        public ValuePoint ExcrementPoint { get; set; }
        public ValuePoint UrinePoint { get; set; }
        public List<ValuePoint> Other1Point { get; set; }
        public ValuePoint WeightPoint { get; set; }
        /// <summary>
        /// 上列最后一个呼吸点画的位置(True-上面,Flase-下面)
        /// </summary>
        public bool BlnLastUp
        {
            get;
            set;
        }

        public List<ValuePoint> PeauTestPoint { get; set; }

        public List<ValuePoint> Other2Point { get; set; }

        public List<EventPoint> EventPoints { get; set; }

        public int OperationCount { get; set; }
        public bool HasOperation { get; set; }

        private void InitColumnData()
        {
            try
            {
                if (Value != null)
                {
                    this.TempuPoint.Clear();
                    foreach (EntityEmrTemperatureMonitorData itemData in Value.TempuValues)
                    {
                        //体温
                        if (itemData.measureType == 1 || itemData.measureType == 2
                            || itemData.measureType == 3 || itemData.measureType == 16)
                        {
                            ValuePoint tempu = new ValuePoint(this, itemData);
                            this.TempuPoint.Add(tempu);
                            tempu.Init();

                            //找出降温点
                            var query = from i in Value.TempuValues
                                        where i.measureType == 4 && i.timePeriod == itemData.timePeriod
                                        select i;

                            foreach (EntityEmrTemperatureMonitorData subItem in query)
                            {
                                ValuePoint subPoint = new ValuePoint(this, subItem);
                                tempu.SubValuePoints.Add(subPoint);
                                subPoint.Init();
                            }
                        }

                    }

                    //脉搏
                    this.PulsePoints.Clear();
                    foreach (var itemData in Value.PulseValues)
                    {
                        ValuePoint pulsePpint = new ValuePoint(this, itemData);
                        this.PulsePoints.Add(pulsePpint);
                        pulsePpint.Init();


                        //找出位置重合点
                        PointF p1 = pulsePpint.GetCenterPosition();

                        var query2 = from i in this.TempuPoint
                                     where i.TimePeriod == pulsePpint.TimePeriod && (i.PointValueType == 1 || i.PointValueType == 2 || i.PointValueType == 3)
                                     select i;

                        if (query2.Count() > 0)
                        {
                            ValuePoint tempu = query2.First();

                            PointF p2 = tempu.GetCenterPosition();

                            if (p2.X < p1.X + 0.5 && p2.X > p1.X - 0.5
                                && p2.Y < p1.Y + 0.5 && p2.Y > p1.Y - 0.5
                                )
                            {
                                if (tempu.PointValueType == 1)
                                {
                                    pulsePpint.OverlapType = PointOverlapType.Type3;
                                    tempu.OverlapType = PointOverlapType.Type3;
                                }
                                else if (tempu.PointValueType == 2)
                                {
                                    pulsePpint.OverlapType = PointOverlapType.Type1;
                                    tempu.OverlapType = PointOverlapType.Type3;
                                }
                                else if (tempu.PointValueType == 3)
                                {
                                    pulsePpint.OverlapType = PointOverlapType.Type2;
                                    tempu.OverlapType = PointOverlapType.Type3;
                                }
                            }
                        }
                    }

                    //心率
                    this.HeartRatePoints.Clear();
                    foreach (var itemData in this.Value.HeartRateValues)
                    {
                        ValuePoint hrp = new ValuePoint(this, itemData);
                        this.HeartRatePoints.Add(hrp);
                        hrp.Init();
                    }


                    //添加呼吸点
                    this.BreathPoint.Clear();
                    foreach (var item in Value.BreathValues)
                    {
                        ValuePoint bp = new ValuePoint(this, item);
                        this.BreathPoint.Add(bp);
                        bp.Init();
                    }

                    //添加血压点
                    this.BloodPressurePoint.Clear();
                    //int bpvIndex = 0;
                    foreach (var item in Value.BloodPressureValues)
                    {
                        if (this.BloodPressurePoint.Count < 4)
                        {
                            ValuePoint vp = new ValuePoint(this, item);
                            vp.Index = this.BloodPressurePoint.Count;
                            vp.TypeCount = Value.BloodPressureValues.Count;
                            this.BloodPressurePoint.Add(vp);
                            vp.Init();
                            //bpvIndex++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    //总入液量
                    if (Value.TotalLiqValue != null)
                    {
                        this.TotalLiqPoint = new ValuePoint(this, Value.TotalLiqValue);
                        TotalLiqPoint.Init();
                    }

                    //大便
                    if (Value.ExcrementValue != null)
                    {
                        this.ExcrementPoint = new ValuePoint(this, Value.ExcrementValue);
                        this.ExcrementPoint.Init();
                    }

                    //小便
                    if (Value.UrineValue != null)
                    {
                        this.UrinePoint = new ValuePoint(this, Value.UrineValue);
                        this.UrinePoint.Init();
                    }

                    //排出量，其他
                    this.Other1Point.Clear();
                    foreach (var item in Value.Other1Values)
                    {
                        if (this.Other1Point.Count < 2)
                        {
                            ValuePoint op = new ValuePoint(this, item);
                            op.Index = this.Other1Point.Count;
                            op.TypeCount = Value.Other1Values.Count;
                            this.Other1Point.Add(op);
                            op.Init();
                        }
                        else
                        {
                            break;
                        }
                    }

                    //体重
                    if (Value.WeightValue != null)
                    {
                        this.WeightPoint = new ValuePoint(this, Value.WeightValue);
                        this.WeightPoint.Init();
                    }

                    //皮试
                    this.PeauTestPoint.Clear();
                    foreach (var item in Value.PeauTestValues)
                    {
                        ValuePoint op = new ValuePoint(this, item);
                        op.Index = this.PeauTestPoint.Count;
                        op.TypeCount = Value.PeauTestValues.Count;
                        this.PeauTestPoint.Add(op);
                        op.Init();
                    }


                    //其他
                    this.Other2Point.Clear();
                    foreach (var item in Value.Other2Values)
                    {
                        //if (this.Other2Point.Count < 2)
                        //{
                        ValuePoint op = new ValuePoint(this, item);
                        op.Index = this.Other2Point.Count;
                        op.TypeCount = Value.Other2Values.Count;
                        this.Other2Point.Add(op);
                        op.Init();
                        //}*
                        //else
                        //{
                        //    break;
                        //}
                    }

                    //事件
                    this.EventPoints.Clear();
                    foreach (var item in this.Value.EventValues)
                    {
                        EventPoint ep = new EventPoint(this, item);
                        ep.Init();

                        if (item.SpecialValue != null)
                        {
                            ValuePoint vp = new ValuePoint(this, item.SpecialValue);
                            ep.OriginValuePoint = vp;
                        }

                        this.EventPoints.Add(ep);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public override void InitCells(string registerId, int pageIndex)
        {
            base.InitCells(registerId, pageIndex);

            InitColumnData();

            if (this.Value != null)
            {
                //计算手术次数
                if (this.EventPoints.Any(i => i.Value.isOperation == 1))//查找当前列中是否有手术事件
                {
                    this.HasOperation = true;

                    if (this.PrevColumn != null)//当前列不为第一列
                    {
                        this.OperationCount = this.PrevColumn.OperationCount + 1;//有则手术次数+1
                    }
                    else
                    {
                        this.OperationCount = 1;
                    }
                }
                else
                {
                    if (this.PrevVisibleColumn != null)
                    {
                        this.OperationCount = PrevColumn.OperationCount;
                    }
                }

                #region 计算手术后日期
                //计算手术后日期



                if (this.HasOperation)//当前列中是否有手术事件
                {
                    string daysAfOp = string.Empty;
                    if (this.OperationCount == 1)
                    {
                        daysAfOp = "0";
                    }
                    else if (this.OperationCount > 1)
                    {
                        daysAfOp = MakeRomanNum(this.OperationCount) + "-0";
                    }
                    rowDaysAfOperation.TextProperty.Text = daysAfOp;
                }
                else
                {
                    //往前查找上一个手术事件
                    DrawingDataColumn prevCol = this.PrevColumn;//前一列
                    DateTime? dtPrevOpDate = null;//前一个手术时间

                    //递归遍历前一列
                    while (prevCol != null)
                    {
                        if (prevCol.HasOperation)//当有手术事件时记录下手术时间
                        {
                            dtPrevOpDate = prevCol.Date;
                            break;
                        }
                        prevCol = prevCol.PrevColumn;
                    }


                    if (dtPrevOpDate != null)
                    {
                        //当前时间减去上一次手术时间
                        int daysAfOp = (this.Date.Value.Date - dtPrevOpDate.Value.Date).Days;
                        if (daysAfOp <= 10)//如果手术后天数大于10天则不描绘
                        {
                            rowDaysAfOperation.TextProperty.Text = daysAfOp.ToString();
                        }
                    }
                }

                #endregion


                //计算入院天数
                if (this.ParentGrid.PatInfo != null && this.Date != null)
                {
                    //当前时间减去入院日期,天数加1
                    rowInDays.TextProperty.Text = ((this.Date.Value.Date - this.ParentGrid.PatInfo.InDate.Date).Days + 1).ToString();
                }
            }
            //foreach (var item in this.TempuPoint)
            //{
            //    item.Init();
            //}



            //foreach (var hrp in this.HeartRatePulsePoint)
            //{
            //    hrp.Init();

            ////找出位置重合点
            //PointF p1 = hrp.GetCenterPosition();

            //var query2 = from i in this.TempuPoint
            //             where i.TimePeriod == hrp.TimePeriod && (i.PointValueType == 1 || i.PointValueType == 2 || i.PointValueType == 3)
            //             select i;

            //if (query2.Count() > 0)
            //{
            //    ValuePoint tempu = query2.First();

            //    PointF p2 = tempu.GetCenterPosition();

            //    if (p2.X < p1.X + 0.5 && p2.X > p1.X - 0.5
            //        && p2.Y < p1.Y + 0.5 && p2.Y > p1.Y - 0.5
            //        )
            //    {
            //        if (tempu.PointValueType == 1)
            //        {
            //            hrp.OverlapType = PointOverlapType.Type3;
            //            tempu.OverlapType = PointOverlapType.Type3;
            //        }
            //        else if (tempu.PointValueType == 2)
            //        {
            //            hrp.OverlapType = PointOverlapType.Type1;
            //            tempu.OverlapType = PointOverlapType.Type3;
            //        }
            //        else if (tempu.PointValueType == 3)
            //        {
            //            hrp.OverlapType = PointOverlapType.Type2;
            //            tempu.OverlapType = PointOverlapType.Type3;
            //        }
            //    }
            //}
            //}

            //foreach (var item in this.BreathPoint)
            //{
            //    item.Init();
            //}

            //foreach (var item in this.BloodPressurePoint)
            //{
            //    item.Init();
            //}
        }

        private ThreeItemsColumnData _value;
        public ThreeItemsColumnData Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                //InitColumn();
            }
        }


        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? Date { get; set; }


        ///// <summary>
        ///// 住院日数
        ///// </summary>
        //public string InDays { get; set; }

        ///// <summary>
        ///// 手术或产后日数
        ///// </summary>
        //public string DaysAfOperation { get; set; }

        public DrawingDataColumn PrevVisibleColumn
        {
            get
            {
                if (this.IsFirstVisibleColumn)
                {
                    return null;
                }
                else
                {
                    if (this.Index == 0)
                    {
                        return null;
                    }
                    else
                    {
                        for (int i = this.Index - 1; i >= 0; i--)
                        {
                            if (this.ParentGrid.Columns[i].Visible == true)
                            {
                                return this.ParentGrid.Columns[i];
                            }
                        }
                        return null;
                    }
                }
            }
        }

        public DrawingDataColumn PrevColumn
        {
            get
            {
                if (this.IsFirstColumn)
                {
                    return null;
                }
                else
                {
                    return this.ParentGrid.Columns[this.Index - 1];
                }
            }
        }

        DrawingGridColumnCell rowInDays = null;
        DrawingGridColumnCell rowDaysAfOperation = null;
        public override float InitHeader()
        {
            float height = 0;

            float fontScale = 0.5f;
            DrawingGridColumnCell rowDate = new DrawingGridColumnCell(this);
            rowDate.HeightUnit = ThreeItemConstValue.HeaderHeight_Date;
            rowDate.WidthUnit = this.WidthUnit;

            string dateText = string.Empty;

            if (this.Date != null)
            {
                if (this.IsFirstVisibleColumn)
                {
                    dateText = this.Date.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    if (PrevVisibleColumn.Date.Value.Year != this.Date.Value.Year)
                    {
                        dateText = this.Date.Value.ToString("yyyy-MM-dd");
                    }
                    else if (PrevVisibleColumn.Date.Value.Month != this.Date.Value.Month)
                    {
                        dateText = this.Date.Value.ToString("MM-dd");
                    }
                    else
                    {
                        dateText = this.Date.Value.ToString("dd");
                    }
                }
            }

            if (this.Value != null)
            {
                rowDate.TextProperty.Text = dateText;
            }

            rowDate.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, rowDate.HeightUnit * this.SizePerUnit * fontScale);

            rowDate.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowDate.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowDate.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            if (!this.IsFirstVisibleColumn)
            {
                rowDate.Border.LeftBorderColor = Color.Red;
            }

            if (!this.IsLastVisibleColumn)
            {
                rowDate.Border.RightBorderColor = Color.Red;
            }
            Cells.Add(rowDate);
            height += rowDate.HeightUnit;

            //住院天数
            rowInDays = new DrawingGridColumnCell(this);
            rowInDays.HeightUnit = ThreeItemConstValue.HeaderHeight_Date;
            rowInDays.WidthUnit = this.WidthUnit;
            rowInDays.TopUnit = Cells[Cells.Count - 1].HeightUnit + Cells[Cells.Count - 1].TopUnit;
            //rowInDays.TextProperty.Text = this.InDays;
            rowInDays.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, rowInDays.HeightUnit * this.SizePerUnit * fontScale);

            rowInDays.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowInDays.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            if (!this.IsFirstVisibleColumn)
            {
                rowInDays.Border.LeftBorderColor = Color.Red;
            }

            if (!this.IsLastVisibleColumn)
            {
                rowInDays.Border.RightBorderColor = Color.Red;
            }

            Cells.Add(rowInDays);

            height += rowInDays.HeightUnit;

            //手术或产后日数
            rowDaysAfOperation = new DrawingGridColumnCell(this);
            rowDaysAfOperation.HeightUnit = ThreeItemConstValue.HeaderHeight_Date;
            rowDaysAfOperation.WidthUnit = this.WidthUnit;
            rowDaysAfOperation.TopUnit = Cells[Cells.Count - 1].HeightUnit + Cells[Cells.Count - 1].TopUnit;
            //rowDaysAfOperation.TextProperty.Text = this.DaysAfOperation;
            rowDaysAfOperation.TextProperty.Font = new Font("Times New Roman", rowDaysAfOperation.HeightUnit * this.SizePerUnit * fontScale);
            rowDaysAfOperation.TextProperty.ForeColor = Color.Red;
            rowDaysAfOperation.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowDaysAfOperation.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            if (!this.IsFirstVisibleColumn)
            {
                rowDaysAfOperation.Border.LeftBorderColor = Color.Red;
            }

            if (!this.IsLastVisibleColumn)
            {
                rowDaysAfOperation.Border.RightBorderColor = Color.Red;
            }

            Cells.Add(rowDaysAfOperation);

            height += rowDaysAfOperation.HeightUnit;

            //上午
            DrawingGridColumnCell rowMoning = new DrawingGridColumnCell(this);
            rowMoning.HeightUnit = 1;// TreeItemConstValue.HeaderHeight_Date / 2;
            rowMoning.WidthUnit = this.WidthUnit / 2;
            rowMoning.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, rowMoning.HeightUnit * this.SizePerUnit * 0.7f);
            rowMoning.TextProperty.Text = "上午";
            rowMoning.TopUnit = Cells[Cells.Count - 1].HeightUnit + Cells[Cells.Count - 1].TopUnit;

            rowMoning.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            if (!this.IsFirstVisibleColumn)
            {
                rowMoning.Border.LeftBorderColor = Color.Red;
            }

            Cells.Add(rowMoning);

            height += rowMoning.HeightUnit;

            ////下午
            DrawingGridColumnCell rowAFTN = new DrawingGridColumnCell(this);
            rowAFTN.HeightUnit = 1;// TreeItemConstValue.HeaderHeight_Date / 2;
            rowAFTN.WidthUnit = this.WidthUnit / 2;
            rowAFTN.LeftUnit = this.WidthUnit / 2;
            rowAFTN.TopUnit = rowMoning.TopUnit;
            rowAFTN.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, rowAFTN.HeightUnit * this.SizePerUnit * 0.7f);
            rowAFTN.TextProperty.Text = "下午";

            rowAFTN.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            if (!this.IsLastVisibleColumn)
            {
                rowAFTN.Border.RightBorderColor = Color.Red;
            }

            Cells.Add(rowAFTN);

            #region 时间刻度
            Font fontDigit = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.SizePerUnit * 0.6f);
            string strTime1 = string.Empty;
            string strTime2 = string.Empty;
            string strTime3 = string.Empty;
            if (Common.Entity.GlobalParm.dicSysParameter[33] == "1")// || clsGlobalHospitalCode.Code == "0002" || clsGlobalHospitalCode.Code == "0003")
            {
                strTime1 = "2";
                strTime2 = "6";
                strTime3 = "10";
            }
            else
            {
                strTime1 = "4";
                strTime2 = "8";
                strTime3 = "12";
            }

            //4    2：00----5：59
            DrawingGridColumnCell rowTime4 = new DrawingGridColumnCell(this);
            rowTime4.TopUnit = rowMoning.TopUnit + rowMoning.HeightUnit;
            rowTime4.TextProperty.Text = strTime1;
            rowTime4.TextProperty.Font = fontDigit;
            rowTime4.TextProperty.ForeColor = Color.Red;

            rowTime4.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            if (!this.IsFirstVisibleColumn)
            {
                rowTime4.Border.LeftBorderColor = Color.Red;
            }

            rowTime4.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;

            Cells.Add(rowTime4);

            //8    6：00—9：59
            DrawingGridColumnCell rowTime8 = new DrawingGridColumnCell(this);
            rowTime8.TopUnit = rowMoning.TopUnit + rowMoning.HeightUnit;
            rowTime8.LeftUnit = rowTime4.LeftUnit + rowTime4.WidthUnit;
            rowTime8.TextProperty.Text = strTime2;
            rowTime8.TextProperty.Font = fontDigit;

            rowTime8.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;

            Cells.Add(rowTime8);

            //12   10：00—13：58
            DrawingGridColumnCell rowTime12 = new DrawingGridColumnCell(this);
            rowTime12.TopUnit = rowMoning.TopUnit + rowMoning.HeightUnit;
            rowTime12.LeftUnit = rowTime8.LeftUnit + rowTime8.WidthUnit;
            rowTime12.TextProperty.Text = strTime3;
            rowTime12.TextProperty.Font = fontDigit;

            rowTime12.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;

            Cells.Add(rowTime12);

            //4    14：00—17：59
            DrawingGridColumnCell rowTime16 = new DrawingGridColumnCell(this);
            rowTime16.TopUnit = rowMoning.TopUnit + rowMoning.HeightUnit;
            rowTime16.LeftUnit = rowTime12.LeftUnit + rowTime12.WidthUnit;
            rowTime16.TextProperty.Text = strTime1;
            rowTime16.TextProperty.Font = fontDigit;

            rowTime16.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;

            Cells.Add(rowTime16);

            //8    18：00----21：59
            DrawingGridColumnCell rowTime20 = new DrawingGridColumnCell(this);
            rowTime20.TopUnit = rowMoning.TopUnit + rowMoning.HeightUnit;
            rowTime20.LeftUnit = rowTime16.LeftUnit + rowTime16.WidthUnit;
            rowTime20.TextProperty.Text = strTime2;
            rowTime20.TextProperty.Font = fontDigit;
            rowTime20.TextProperty.ForeColor = Color.Red;

            rowTime20.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;

            Cells.Add(rowTime20);

            //12   22：00—1：29 
            DrawingGridColumnCell rowTime24 = new DrawingGridColumnCell(this);
            rowTime24.TopUnit = rowMoning.TopUnit + rowMoning.HeightUnit;
            rowTime24.LeftUnit = rowTime20.LeftUnit + rowTime20.WidthUnit;
            rowTime24.TextProperty.Text = strTime3;
            rowTime24.TextProperty.Font = fontDigit;
            rowTime24.TextProperty.ForeColor = Color.Red;

            rowTime24.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            if (!this.IsLastVisibleColumn)
            {
                rowTime24.Border.RightBorderColor = Color.Red;
            }

            rowTime24.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;

            Cells.Add(rowTime24);
            height += rowTime4.HeightUnit;

            #endregion

            return height;
        }

        public override float InitBody()
        {
            float height = 0;

            height = (ThreeItemConstValue.MaxTemp - ThreeItemConstValue.MinTemp) * ThreeItemConstValue.RowsPerTempUnit;

            return height;
        }

        public override float InitFooter(string registerId, int pageIndex)
        {
            float height = 0;

            //呼吸
            for (int i = 0; i < 6; i++)
            {
                DrawingGridColumnCell cellBreath = new DrawingGridColumnCell(this);
                cellBreath.LeftUnit = i;
                cellBreath.TopUnit = this.HeaderHeightUnit + this.BodyHeightUnit;
                cellBreath.HeightUnit = ThreeItemConstValue.FooterHeight_Breath;

                cellBreath.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
                cellBreath.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;

                if (i == 5)
                {
                    cellBreath.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
                }
                else if (i == 0)
                {
                    cellBreath.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
                }

                this.Cells.Add(cellBreath);
            }

            height += ThreeItemConstValue.FooterHeight_Breath;

            //血压
            for (int i = 0; i < 2; i++)
            {
                DrawingGridColumnCell cellPressure = new DrawingGridColumnCell(this);
                cellPressure.LeftUnit = 0;
                cellPressure.WidthUnit = 6;
                cellPressure.HeightUnit = ThreeItemConstValue.FooterHeight_Blood;
                cellPressure.TopUnit = this.HeaderHeightUnit + this.BodyHeightUnit + height;

                cellPressure.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;

                //if (i == 1)
                //{
                //    cellPressure.Border.RightBorderWidth = TreeItemConstValue.OuterBorderWidth;
                //}
                //else if (i == 0)
                //{
                //    cellPressure.Border.LeftBorderWidth = TreeItemConstValue.OuterBorderWidth;
                //}

                this.Cells.Add(cellPressure);
            }
            height += ThreeItemConstValue.FooterHeight_Blood;

            //总入液量
            DrawingGridColumnCell cellLiq = new DrawingGridColumnCell(this);
            cellLiq.WidthUnit = this.WidthUnit;
            cellLiq.TopUnit = this.HeaderHeightUnit + this.BodyHeightUnit + height;
            cellLiq.HeightUnit = ThreeItemConstValue.FooterHeight_Liq;

            cellLiq.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellLiq.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellLiq.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellLiq.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            height += cellLiq.HeightUnit;

            this.Cells.Add(cellLiq);

            //大便
            DrawingGridColumnCell cellEx1 = new DrawingGridColumnCell(this);
            cellEx1.WidthUnit = this.WidthUnit;
            cellEx1.TopUnit = this.HeaderHeightUnit + this.BodyHeightUnit + height;
            cellEx1.HeightUnit = ThreeItemConstValue.FooterHeight_DaBian;

            cellEx1.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellEx1.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellEx1.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellEx1.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            height += cellEx1.HeightUnit;
            this.Cells.Add(cellEx1);

            //尿量
            DrawingGridColumnCell cellEx2 = new DrawingGridColumnCell(this);
            cellEx2.WidthUnit = this.WidthUnit;
            cellEx2.TopUnit = this.HeaderHeightUnit + this.BodyHeightUnit + height;
            cellEx2.HeightUnit = ThreeItemConstValue.FooterHeight_NiaoLiang;

            cellEx2.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellEx2.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellEx2.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellEx2.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            height += cellEx2.HeightUnit;

            this.Cells.Add(cellEx2);

            //其他1
            DrawingGridColumnCell cellEx3 = new DrawingGridColumnCell(this);
            cellEx3.WidthUnit = this.WidthUnit;
            cellEx3.TopUnit = this.HeaderHeightUnit + this.BodyHeightUnit + height;
            cellEx3.HeightUnit = ThreeItemConstValue.FooterHeight_Other1;
            cellEx3.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellEx3.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellEx3.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellEx3.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            height += cellEx3.HeightUnit;
            this.Cells.Add(cellEx3);

            //体重
            DrawingGridColumnCell cellWeight = new DrawingGridColumnCell(this);
            cellWeight.WidthUnit = this.WidthUnit;
            cellWeight.TopUnit = this.HeaderHeightUnit + this.BodyHeightUnit + height;
            cellWeight.HeightUnit = ThreeItemConstValue.FooterHeight_Weight;
            cellWeight.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellWeight.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellWeight.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellWeight.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            height += cellWeight.HeightUnit;
            this.Cells.Add(cellWeight);

            //皮试
            DrawingGridColumnCell cellPeau = new DrawingGridColumnCell(this);
            cellPeau.WidthUnit = this.WidthUnit;
            cellPeau.TopUnit = this.HeaderHeightUnit + this.BodyHeightUnit + height;
            cellPeau.HeightUnit = this.ParentGrid.FooterHeight_PiShi;
            cellPeau.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellPeau.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellPeau.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellPeau.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            height += cellPeau.HeightUnit;
            this.Cells.Add(cellPeau);

            //其他2
            DrawingGridColumnCell cellOther = new DrawingGridColumnCell(this);
            cellOther.WidthUnit = this.WidthUnit;
            cellOther.TopUnit = this.HeaderHeightUnit + this.BodyHeightUnit + height;
            cellOther.HeightUnit = this.ParentGrid.m_fltOtherFooterHeigth;
            cellOther.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellOther.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellOther.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            cellOther.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            height += cellOther.HeightUnit;
            this.Cells.Add(cellOther);
            return height;
        }

        public override float WidthUnit
        {
            get { return 6; }
        }

        public override void Paint()
        {
            try
            {
                if (this.Visible == true)
                {

                    float lPos = this.LeftOffset + this.ParentGrid.LeftOffset;
                    float tPos = (this.TopOffsetUnit + this.HeaderHeightUnit) * this.SizePerUnit + this.ParentGrid.TopOffset;
                    float wSize = this.WidthUnit * this.SizePerUnit;
                    float bPos = tPos + this.BodyHeightUnit * this.SizePerUnit;


                    Pen p = new Pen(Color.Black);

                    for (int i = 0; i <= ThreeItemConstValue.RowsPerTempUnit * (ThreeItemConstValue.MaxTemp - ThreeItemConstValue.MinTemp); i++)
                    {
                        if (i == 0)
                        {
                            p.Color = Color.Black;
                            this.graphics.DrawLine(p, new PointF(lPos, tPos + i * this.SizePerUnit), new PointF(lPos + wSize, tPos + i * this.SizePerUnit));
                        }
                        else if (i == ThreeItemConstValue.RowsPerTempUnit * (ThreeItemConstValue.MaxTemp - ThreeItemConstValue.MinTemp))
                        {
                            p.Color = Color.Black;
                            this.graphics.DrawLine(p, new PointF(lPos, tPos + i * this.SizePerUnit), new PointF(lPos + wSize, tPos + i * this.SizePerUnit));
                        }
                        else if (i >= ThreeItemConstValue.RowsPerTempUnit && i % ThreeItemConstValue.RowsPerTempUnit == 0)
                        {
                            p.Color = ThreeItemConstValue.TempuCellColor;
                            p.Width = 2;
                            this.graphics.DrawLine(p, new PointF(lPos, tPos + i * this.SizePerUnit), new PointF(lPos + wSize, tPos + i * this.SizePerUnit));
                        }
                        else
                        {
                            p.Color = ThreeItemConstValue.TempuCellColor;
                            this.graphics.DrawLine(p, new PointF(lPos, tPos + i * this.SizePerUnit), new PointF(lPos + wSize, tPos + i * this.SizePerUnit));
                        }


                        p.Width = 1;
                    }


                    for (int i = 0; i <= 6; i++)
                    {
                        if (i != 0 && i != 6)
                        {
                            p.Color = ThreeItemConstValue.TempuCellColor;
                            this.graphics.DrawLine(p, new PointF(lPos + i * this.SizePerUnit, tPos), new PointF(lPos + i * this.SizePerUnit, bPos));
                        }
                        else if (i == 0)
                        {
                            if (this.IsFirstVisibleColumn)
                            {
                                p.Color = Color.Black;
                                p.Width = ThreeItemConstValue.OuterBorderWidth;
                                this.graphics.DrawLine(p, new PointF(lPos + i * this.SizePerUnit, tPos), new PointF(lPos + i * this.SizePerUnit, bPos));
                            }
                            else
                            {
                                p.Color = ThreeItemConstValue.TempuCellColor;
                                this.graphics.DrawLine(new Pen(Color.Red), new PointF(lPos + i * this.SizePerUnit, tPos), new PointF(lPos + i * this.SizePerUnit, bPos));
                            }
                        }
                        else if (i == 6)
                        {
                            if (this.IsLastVisibleColumn)
                            {
                                p.Color = Color.Black;
                                p.Width = ThreeItemConstValue.OuterBorderWidth;
                                this.graphics.DrawLine(p, new PointF(lPos + i * this.SizePerUnit, tPos), new PointF(lPos + i * this.SizePerUnit, bPos));
                            }
                            else
                            {
                                p.Color = ThreeItemConstValue.TempuCellColor;
                                this.graphics.DrawLine(new Pen(Color.Red), new PointF(lPos + i * this.SizePerUnit, tPos), new PointF(lPos + i * this.SizePerUnit, bPos));
                            }
                        }
                    }

                    #region 画基准体温线

                    if (ThreeItemConstValue.TempuNormal <= ThreeItemConstValue.MaxTemp && ThreeItemConstValue.TempuNormal >= ThreeItemConstValue.MinTemp)
                    {
                        float n = (float)(ThreeItemConstValue.MaxTemp - ThreeItemConstValue.TempuNormal) * this.BodyHeightUnit / (ThreeItemConstValue.MaxTemp - ThreeItemConstValue.MinTemp);

                        p.Color = Color.Red;
                        p.Width = ThreeItemConstValue.TempuNormalLineWidth;
                        PointF pStart = new PointF(this.LeftOffset + this.ParentGrid.LeftOffset, (n + this.HeaderHeightUnit + this.TopOffsetUnit) * this.SizePerUnit + this.ParentGrid.TopOffset);
                        PointF pEnd = new PointF(this.LeftOffset + this.ParentGrid.LeftOffset + this.WidthUnit * this.SizePerUnit, (n + this.HeaderHeightUnit + this.TopOffsetUnit) * this.SizePerUnit + this.ParentGrid.TopOffset);
                        this.graphics.DrawLine(p, pStart, pEnd);
                    }
                    #endregion

                    #region 画点
                    //画体温
                    foreach (ValuePoint item in this.TempuPoint)
                    {
                        item.Draw();
                    }

                    //画脉搏
                    foreach (ValuePoint item in this.PulsePoints)
                    {
                        item.Draw();
                    }

                    foreach (ValuePoint item in this.HeartRatePoints)
                    {
                        item.Draw();
                    }

                    //呼吸数据先按时间区间排序
                    this.BreathPoint.Sort(s_intCompare);
                    bool blnDrawUp = BlnLastUp;
                    //画呼吸
                    foreach (ValuePoint item in this.BreathPoint)
                    {
                        blnDrawUp = !blnDrawUp;
                        item.BlnDrawUp = blnDrawUp;
                        item.Draw();
                    }
                    BlnLastUp = blnDrawUp;

                    //画血压
                    foreach (ValuePoint item in this.BloodPressurePoint)
                    {
                        item.Draw();
                    }

                    //画总入液量
                    if (this.TotalLiqPoint != null)
                    {
                        this.TotalLiqPoint.Draw();
                    }

                    //大便
                    if (this.ExcrementPoint != null)
                    {
                        this.ExcrementPoint.Draw();
                    }

                    //小便
                    if (this.UrinePoint != null)
                    {
                        this.UrinePoint.Draw();
                    }

                    //其他1
                    foreach (ValuePoint item in this.Other1Point)
                    {
                        //暂时只画一个
                        item.Draw();
                        //break;
                    }

                    //体重
                    if (this.WeightPoint != null)
                    {
                        this.WeightPoint.Draw();
                    }

                    //皮试
                    foreach (ValuePoint item in this.PeauTestPoint)
                    {
                        item.Draw();
                    }

                    int intTemp = 0;
                    PointF ptTemp = PointF.Empty;
                    //其他2
                    foreach (ValuePoint item in this.Other2Point)
                    {
                        intTemp++;
                        if (intTemp > 1)
                        {
                            item.PointCenterPos = new PointF(ptTemp.X, ptTemp.Y + 1.6f * this.SizePerUnit);
                        }
                        item.Draw();
                        ptTemp = item.PointCenterPos;
                    }

                    foreach (EventPoint ep in this.EventPoints)
                    {
                        ep.Draw();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }

            base.Paint();
        }
        /// <summary>
        /// 比较两个点的时间区间
        /// </summary>
        /// <param name="p_objValue1"></param>
        /// <param name="p_objValue2"></param>
        /// <returns></returns>
        private static int s_intCompare(ValuePoint p_objValue1,ValuePoint p_objValue2)
        {
            int intResult = 0;
            if (p_objValue1.TimePeriod < p_objValue2.TimePeriod)
            {
                intResult = -1;
            }
            else if (p_objValue1.TimePeriod > p_objValue2.TimePeriod)
            {
                intResult = 1;
            }
            return intResult;
        }

        string MakeRomanNum(int Num)
        {

            int[] Arabic = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

            string[] Roman = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int i = 0;
            string RomanNum = string.Empty;

            if (Num < 0 || Num > 1000)
                return string.Empty;
            while (Num > 0)
            {
                while (Num >= Arabic[i])
                {
                    RomanNum += Roman[i];
                    Num -= Arabic[i];
                }
                i += 1;
            }
            return RomanNum;
        }
    }

}
