using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using DevExpress.XtraReports.UI;
using Common.Entity;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    public partial class ctlThreeItemsControl : UserControl
    {
        /// <summary>
        ///  .ctor
        /// </summary>
        public ctlThreeItemsControl()
        {
            this.EnableHoverToolTip = false;

            this._colsPerPage = 7;
            this.SizePerUnit = 12f; //9.5f;
            InitializeComponent();
            this.PageIndex = 0;

            ColumnsData = new List<ThreeItemsColumnData>();

            //清远医院特殊处理
            //if (clsGlobalHospitalCode.Code == "0003")
            //    ThreeItemConstValue.MaxPulse = 200;
            //else
            ThreeItemConstValue.MaxPulse = 180;
        }

        public int PageIndex { get; set; }

        public int PageCount
        {
            get
            {
                if (this.grid != null)
                {
                    return this.grid.PageCount;
                }
                else
                {
                    return 0;
                }
            }
        }

        public float SizePerUnit { get; set; }

        private int _colsPerPage;
        public int ColumnsPerPage
        {
            get
            {
                return _colsPerPage;
            }
            set
            {
                if (this._colsPerPage != value)
                {
                    this._colsPerPage = value;
                    this.PageIndex = 0;
                    this.Draw(true);
                }
            }
        }

        DrawingGrid grid;

        List<ThreeItemsColumnData> ColumnsData;

        /// <summary>
        /// 加载数据并生成图像
        /// </summary>
        /// <param name="data"></param>
        public void LoadData(EntityEmrTemperaturePatInfo pInfo, List<EntityEmrTemperatureMonitorData> listValues, List<EntityEmrTemperatureChartEvent> listEvent)
        {
            this.ColumnsData = GetColumnsData(listValues, listEvent);

            grid = new DrawingGrid(this.SizePerUnit, this.ColumnsPerPage);
            grid.evtTextChanged += new DrawingGrid.dgtTextChanged(grid_evtTextChanged);
            grid.PageIndex = this.PageIndex;

            grid.LoadData(pInfo, ColumnsData);
            Draw(false);
        }

        public static List<ThreeItemsColumnData> GetColumnsData(List<EntityEmrTemperatureMonitorData> listValues, List<EntityEmrTemperatureChartEvent> listEvent)
        {

            //清远医院特殊处理
            //if (clsGlobalHospitalCode.Code == "0003")
            //    ThreeItemConstValue.MaxPulse = 200;
            //else
            ThreeItemConstValue.MaxPulse = 180;

            //判断是否显示35°下的体温信息
            bool blnFlag = true;
            if (GlobalParm.dicSysParameter.ContainsKey(73))
            {
                string strVal = GlobalParm.dicSysParameter[73];
                int intVal = 0;
                if (int.TryParse(strVal, out intVal))
                {
                    if (intVal == 1) blnFlag = false;
                }
            }

            List<ThreeItemsColumnData> returnData = new List<ThreeItemsColumnData>();
            foreach (EntityEmrTemperatureMonitorData entityPoint in listValues)
            {
                //1口表 2腋表 3肛表 4降温 5脉搏 6心率
                //7呼吸 8血压 9总入液量   10排出量-大便 11排出量-尿量 12排出量-其他1
                //13体重 14皮试 15其他
                entityPoint.timePeriod = ThreeItemTimePeriodGenerator.GetTimePeriod(entityPoint.recordDate, ref entityPoint.drawingPointDate);

                ThreeItemsColumnData columnsData = null;
                if (!returnData.Any(i => i.RecDate.Date == entityPoint.drawingPointDate.Date))
                {
                    columnsData = new ThreeItemsColumnData(entityPoint.drawingPointDate);
                    returnData.Add(columnsData);
                }
                else
                {
                    columnsData = returnData.Single(i => i.RecDate.Date == entityPoint.drawingPointDate.Date);
                }

                //1口表 2腋表 3肛表 4降温 16亚低温治疗
                if (entityPoint.measureType == 1 || entityPoint.measureType == 2
                    || entityPoint.measureType == 3 || entityPoint.measureType == 4
                    || entityPoint.measureType == 16)
                {
                    decimal decVal = -1m;

                    if (entityPoint.measureType != 4 && entityPoint.measureType != 16)
                    {
                        if (decimal.TryParse(entityPoint.measureValue, out decVal))
                        {
                            if (decVal < 35m && blnFlag)//低于35度,转换为事件
                            {
                                EntityEmrTemperatureChartEvent entityEvent = new EntityEmrTemperatureChartEvent();
                                entityEvent.recordDate = entityPoint.recordDate;
                                entityEvent.timePeriod = entityPoint.timePeriod;
                                entityEvent.eventName = "体温不升";
                                entityEvent.drawingPotision = 1;
                                entityEvent.isBreakingPoint = 1;
                                entityEvent.isDrawTime = 0;
                                entityEvent.isOperation = 0;
                                entityEvent.SpecialValue = entityPoint;
                                columnsData.EventValues.Add(entityEvent);
                            }
                            else
                            {
                                columnsData.TempuValues.Add(entityPoint);
                            }
                        }
                    }
                    else
                    {
                        columnsData.TempuValues.Add(entityPoint);
                        if (!string.IsNullOrEmpty(entityPoint.measureValue2))
                        {
                            EntityEmrTemperatureChartEvent entityEvent = new EntityEmrTemperatureChartEvent();
                            entityEvent.recordDate = entityPoint.recordDate;
                            entityEvent.timePeriod = entityPoint.timePeriod;
                            entityEvent.eventName = entityPoint.measureValue2;
                            entityEvent.drawingPotision = 1;
                            entityEvent.isBreakingPoint = 0;
                            entityEvent.isDrawTime = 0;
                            entityEvent.isOperation = 0;
                            entityEvent.SpecialValue = entityPoint;
                            columnsData.EventValues.Add(entityEvent);
                        }
                    }
                }
                else if (entityPoint.measureType == 5)//5脉搏
                {
                    columnsData.PulseValues.Add(entityPoint);
                }
                else if (entityPoint.measureType == 6)//6心率
                {
                    columnsData.HeartRateValues.Add(entityPoint);
                }
                else if (entityPoint.measureType == 7)//7呼吸
                {
                    if (entityPoint.measureValue == "辅助呼吸" || entityPoint.measureValue == "停辅助呼吸")
                    {
                        EntityEmrTemperatureChartEvent entityEvent = new EntityEmrTemperatureChartEvent();
                        entityEvent.recordDate = entityPoint.recordDate;
                        entityEvent.timePeriod = entityPoint.timePeriod;
                        entityEvent.eventName = entityPoint.measureValue;
                        entityEvent.drawingPotision = 1;
                        entityEvent.isBreakingPoint = 0;
                        entityEvent.isDrawTime = 0;
                        entityEvent.isOperation = 0;
                        entityEvent.SpecialValue = entityPoint;
                        columnsData.EventValues.Add(entityEvent);
                        if (!string.IsNullOrEmpty(entityPoint.measureValue2))
                        {
                            columnsData.BreathValues.Add(entityPoint);
                        }
                    }
                    else
                    {
                        columnsData.BreathValues.Add(entityPoint);
                    }
                }
                else if (entityPoint.measureType == 8)//8血压
                {
                    columnsData.BloodPressureValues.Add(entityPoint);
                }
                else if (entityPoint.measureType == 9)//9总入液量
                {
                    columnsData.TotalLiqValue = entityPoint;
                }
                else if (entityPoint.measureType == 10)//10排出量-大便
                {
                    columnsData.ExcrementValue = entityPoint;
                }
                else if (entityPoint.measureType == 11)//11排出量-尿量
                {
                    columnsData.UrineValue = entityPoint;
                }
                else if (entityPoint.measureType == 12)//12排出量-其他
                {
                    columnsData.Other1Values.Add(entityPoint);
                }
                else if (entityPoint.measureType == 13)//13体重
                {
                    columnsData.WeightValue = entityPoint;
                }
                else if (entityPoint.measureType == 14)//14皮试
                {
                    columnsData.PeauTestValues.Add(entityPoint);
                }
                else if (entityPoint.measureType == 15)//15其他
                {
                    columnsData.Other2Values.Add(entityPoint);
                }
            }

            foreach (EntityEmrTemperatureChartEvent entityEvent in listEvent)
            {
                entityEvent.timePeriod = ThreeItemTimePeriodGenerator.GetTimePeriod(entityEvent.recordDate, ref entityEvent.drawingPointDate);

                ThreeItemsColumnData data = null;
                if (!returnData.Any(i => i.RecDate.Date == entityEvent.drawingPointDate.Date))
                {
                    data = new ThreeItemsColumnData(entityEvent.drawingPointDate);
                    returnData.Add(data);
                }
                else
                {
                    data = returnData.Single(i => i.RecDate.Date == entityEvent.drawingPointDate.Date);
                }
                data.EventValues.Add(entityEvent);
            }
            return returnData.OrderBy(i => i.RecDate).ToList();
        }

        private void Draw(bool reDraw)
        {
            try
            {
                uiHelper.BeginLoading(this.FindForm() as frmBase);
                grid.ColumnsPerPage = this._colsPerPage;
                if (reDraw)
                {
                    grid.InitColumns();
                }
                grid.Paint(this.SizePerUnit);

                Bitmap image = grid.GetImage();

                this.picContainer.Image = image;
                this.picContainer.Width = image.Width;
                this.picContainer.Height = image.Height;
            }
            finally
            {
                uiHelper.CloseLoading(this.FindForm() as frmBase);
            }
        }

        /// <summary>
        /// 缩小
        /// </summary>
        public void ZoomOut()
        {
            if (this.SizePerUnit > 6)
            {
                this.SizePerUnit--;
                Draw(false);
            }
        }

        /// <summary>
        /// 原图
        /// </summary>
        public void Zoom()
        {
            this.SizePerUnit = 12;
            Draw(false);
        }

        /// <summary>
        /// 放大
        /// </summary>
        public void ZoomIn()
        {
            this.SizePerUnit++;
            Draw(false);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public void PrevPage()
        {
            if (this.grid != null)
            {
                this.grid.PrevPage();
                PageIndex = this.grid.PageIndex;
                Draw(false);
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPage()
        {
            if (this.grid != null)
            {
                this.grid.NextPage();
                PageIndex = this.grid.PageIndex;
                Draw(false);
            }
        }

        /// <summary>
        /// 最后一页
        /// </summary>
        public void LastPage()
        {
            if (this.grid != null)
            {
                this.grid.LastPage();
                PageIndex = this.grid.PageIndex;
                Draw(false);
            }
        }

        /// <summary>
        /// 第一页
        /// </summary>
        public void FirstPage()
        {
            if (this.grid != null)
            {
                this.grid.FirstPage();
                PageIndex = this.grid.PageIndex;
                Draw(false);
            }
        }

        /// <summary>
        /// 指定页
        /// </summary>
        public bool IndexPage(int pageNo)
        {
            if (this.grid != null)
            {
                if (this.grid.IndexPage(pageNo))
                {
                    PageIndex = this.grid.PageIndex;
                    Draw(false);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// picturebox点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picContainer_MouseClick(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// picturebox双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picContainer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ThreeItemsControlClickedEventArgs args = GetClickObjects(e.X, e.Y);
            if (args.objColumnCell != null)
            {
                string regId = this.grid.PatInfo.RegID;
                //if (clsGlobalHospitalCode.Code == "0002" && intRegID > 0)
                //{
                //    frmModifyText objModifyTextForm = new frmModifyText(this.grid, args.objColumnCell.TextProperty.Text, intRegID,
                //        args.objColumnCell.Name, PageIndex);
                //    if (objModifyTextForm.ShowDialog(this) == DialogResult.OK)
                //    {
                //        this.Draw(true);
                //    }
                //}
            }
            else if (!args.IsEmpty && ItemClicked != null)
            {
                this.ttcHint.HideHint();
                ItemClicked(this, args);
            }
        }

        /// <summary>
        /// 获取鼠标点击数据
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        private ThreeItemsControlClickedEventArgs GetClickObjects(int X, int Y)
        {
            ThreeItemsControlClickedEventArgs args = new ThreeItemsControlClickedEventArgs();

            if (this.grid != null)
            {
                if (this.grid.CellDate != null && this.grid.CellDate.Count > 0)
                {
                    RectangleF recF = RectangleF.Empty;
                    foreach (DrawingGridColumnCell col in this.grid.CellDate)
                    {
                        PointF pfLeftTop = col.GetLeftTopPosition();
                        PointF pfLeftBottom = col.GetLeftBottomPosition();
                        PointF pfRightTop = col.GetRightTopPotision();
                        PointF pfRightBottom = col.GetRightBottomPosition();
                        recF = new RectangleF(pfLeftTop, new SizeF(pfRightTop.X - pfLeftTop.X, pfRightBottom.Y - pfRightTop.Y));
                        if (recF.Contains(X, Y))
                        {
                            args.objColumnCell = col;
                            break;
                        }
                    }
                }

                DrawingDataColumn clickedColumn = null;
                if (this.grid.VisibleColumns != null && this.grid.VisibleColumns.Count > 0)
                {
                    foreach (DrawingDataColumn col in this.grid.VisibleColumns)
                    {
                        if (X > col.X1 && X < col.X2 && Y > col.Y1 && Y < col.Y2)
                        {
                            clickedColumn = col;
                            break;
                        }
                    }

                    //计算坐标获取选中的数据
                    if (clickedColumn != null)
                    {
                        foreach (ValuePoint tPoint in clickedColumn.TempuPoint)
                        {
                            if (X > tPoint.X1 && X < tPoint.X2 && Y > tPoint.Y1 && Y < tPoint.Y2)
                            {
                                args.TempuPoints.Add(tPoint);
                            }

                            foreach (ValuePoint subPoint in tPoint.SubValuePoints)
                            {
                                if (X > subPoint.X1 && X < subPoint.X2 && Y > subPoint.Y1 && Y < subPoint.Y2)
                                {
                                    args.TempuPoints.Add(subPoint);
                                }
                            }
                        }

                        foreach (ValuePoint tPoint in clickedColumn.PulsePoints)
                        {
                            if (X > tPoint.X1 && X < tPoint.X2 && Y > tPoint.Y1 && Y < tPoint.Y2)
                            {
                                args.PulsePoints.Add(tPoint);
                            }

                            //foreach (ValuePoint subPoint in tPoint.SubValuePoints)
                            //{
                            //    if (X > subPoint.X1 && X < subPoint.X2 && Y > subPoint.Y1 && Y < subPoint.Y2)
                            //    {
                            //        args.HeartRatePulsePoints.Add(subPoint);
                            //    }
                            //}
                        }


                        foreach (ValuePoint tPoint in clickedColumn.HeartRatePoints)
                        {
                            if (X > tPoint.X1 && X < tPoint.X2 && Y > tPoint.Y1 && Y < tPoint.Y2)
                            {
                                args.HeartRatePoints.Add(tPoint);
                            }

                            //foreach (ValuePoint subPoint in tPoint.SubValuePoints)
                            //{
                            //    if (X > subPoint.X1 && X < subPoint.X2 && Y > subPoint.Y1 && Y < subPoint.Y2)
                            //    {
                            //        args.HeartRatePulsePoints.Add(subPoint);
                            //    }
                            //}
                        }

                        foreach (ValuePoint tPoint in clickedColumn.BreathPoint)
                        {
                            if (X > tPoint.X1 && X < tPoint.X2 && Y > tPoint.Y1 && Y < tPoint.Y2)
                            {
                                args.BreathPoints.Add(tPoint);
                            }
                        }

                        foreach (ValuePoint tPoint in clickedColumn.BloodPressurePoint)
                        {
                            if (X > tPoint.X1 && X < tPoint.X2 && Y > tPoint.Y1 && Y < tPoint.Y2)
                            {
                                args.BloodPressurePoints.Add(tPoint);
                            }
                        }

                        if (clickedColumn.TotalLiqPoint != null)
                        {
                            if (X > clickedColumn.TotalLiqPoint.X1 && X < clickedColumn.TotalLiqPoint.X2 && Y > clickedColumn.TotalLiqPoint.Y1 && Y < clickedColumn.TotalLiqPoint.Y2)
                            {
                                args.TotalLiqPoint = clickedColumn.TotalLiqPoint;
                            }
                        }

                        if (clickedColumn.ExcrementPoint != null)
                        {
                            if (X > clickedColumn.ExcrementPoint.X1 && X < clickedColumn.ExcrementPoint.X2 && Y > clickedColumn.ExcrementPoint.Y1 && Y < clickedColumn.ExcrementPoint.Y2)
                            {
                                args.ExcrementPoint = clickedColumn.ExcrementPoint;
                            }
                        }

                        if (clickedColumn.UrinePoint != null)
                        {
                            if (X > clickedColumn.UrinePoint.X1 && X < clickedColumn.UrinePoint.X2 && Y > clickedColumn.UrinePoint.Y1 && Y < clickedColumn.UrinePoint.Y2)
                            {
                                args.UrinePoint = clickedColumn.UrinePoint;
                            }
                        }

                        foreach (ValuePoint tPoint in clickedColumn.Other1Point)
                        {
                            if (X > tPoint.X1 && X < tPoint.X2 && Y > tPoint.Y1 && Y < tPoint.Y2)
                            {
                                args.Other1Points.Add(tPoint);
                            }
                        }

                        if (clickedColumn.WeightPoint != null)
                        {
                            if (X > clickedColumn.WeightPoint.X1 && X < clickedColumn.WeightPoint.X2 && Y > clickedColumn.WeightPoint.Y1 && Y < clickedColumn.WeightPoint.Y2)
                            {
                                args.WeightPoint = clickedColumn.WeightPoint;
                            }
                        }

                        foreach (ValuePoint peauTestPoint in clickedColumn.PeauTestPoint)
                        {
                            if (X > peauTestPoint.X1 && X < peauTestPoint.X2 && Y > peauTestPoint.Y1 && Y < peauTestPoint.Y2)
                            {
                                args.PeauTestPoints.Add(peauTestPoint);
                            }
                        }

                        foreach (ValuePoint tPoint in clickedColumn.Other2Point)
                        {
                            if (X > tPoint.X1 && X < tPoint.X2 && Y > tPoint.Y1 && Y < tPoint.Y2)
                            {
                                args.Other2Points.Add(tPoint);
                            }
                        }

                        foreach (EventPoint eventPoint in clickedColumn.EventPoints)
                        {
                            if (X > eventPoint.X1 && X < eventPoint.X2 && Y > eventPoint.Y1 && Y < eventPoint.Y2)
                            {
                                args.EventPoints.Add(eventPoint);
                            }
                        }
                    }
                }
            }

            return args;
        }

        /// <summary>
        /// 点击参数
        /// </summary>
        public class ThreeItemsControlClickedEventArgs : EventArgs
        {
            public List<ValuePoint> TempuPoints { get; private set; }
            public List<ValuePoint> PulsePoints { get; private set; }
            public List<ValuePoint> HeartRatePoints { get; private set; }

            public List<ValuePoint> BreathPoints { get; private set; }
            public List<ValuePoint> BloodPressurePoints { get; private set; }
            public ValuePoint TotalLiqPoint { get; set; }

            public ValuePoint ExcrementPoint { get; set; }
            public ValuePoint UrinePoint { get; set; }
            public List<ValuePoint> Other1Points { get; set; }
            public ValuePoint WeightPoint { get; set; }
            public List<ValuePoint> PeauTestPoints { get; set; }
            public List<ValuePoint> Other2Points { get; set; }

            public List<EventPoint> EventPoints { get; set; }

            public DrawingGridColumnCell objColumnCell { get; set; }

            public ThreeItemsControlClickedEventArgs()
            {
                TempuPoints = new List<ValuePoint>();
                PulsePoints = new List<ValuePoint>();
                HeartRatePoints = new List<ValuePoint>();
                BreathPoints = new List<ValuePoint>();
                BloodPressurePoints = new List<ValuePoint>();
                TotalLiqPoint = null;


                ExcrementPoint = null;
                UrinePoint = null;
                Other1Points = new List<ValuePoint>();
                WeightPoint = null;
                PeauTestPoints = new List<ValuePoint>();
                Other2Points = new List<ValuePoint>();

                EventPoints = new List<EventPoint>();
            }

            public bool IsEmpty
            {
                get
                {
                    return TempuPoints.Count == 0
                        && PulsePoints.Count == 0
                        && HeartRatePoints.Count == 0
                        && BreathPoints.Count == 0
                        && BloodPressurePoints.Count == 0
                        && TotalLiqPoint == null
                        && ExcrementPoint == null
                        && UrinePoint == null
                        && Other1Points.Count == 0
                        && WeightPoint == null
                        && PeauTestPoints.Count == 0
                        && Other2Points.Count == 0
                        && EventPoints.Count == 0
                        && objColumnCell == null
                        ;
                }
            }

            public bool Equal(ThreeItemsControlClickedEventArgs obj)
            {
                try
                {
                    if (this.TempuPoints.Count != obj.TempuPoints.Count)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < this.TempuPoints.Count; i++)
                        {
                            if (this.TempuPoints[i].Value != obj.TempuPoints[i].Value)
                            {
                                return false;
                            }
                        }
                    }

                    if (this.PulsePoints.Count != obj.PulsePoints.Count)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < this.PulsePoints.Count; i++)
                        {
                            if (this.PulsePoints[i].Value != obj.PulsePoints[i].Value)
                            {
                                return false;
                            }
                        }
                    }

                    if (this.HeartRatePoints.Count != obj.HeartRatePoints.Count)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < this.HeartRatePoints.Count; i++)
                        {
                            if (this.HeartRatePoints[i].Value != obj.HeartRatePoints[i].Value)
                            {
                                return false;
                            }
                        }
                    }

                    if (this.BreathPoints.Count != obj.BreathPoints.Count)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < this.BreathPoints.Count; i++)
                        {
                            if (this.BreathPoints[i].Value != obj.BreathPoints[i].Value)
                            {
                                return false;
                            }
                        }
                    }

                    if (this.BloodPressurePoints.Count != obj.BloodPressurePoints.Count)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < this.BloodPressurePoints.Count; i++)
                        {
                            if (this.BloodPressurePoints[i].Value != obj.BloodPressurePoints[i].Value)
                            {
                                return false;
                            }
                        }
                    }

                    if (
                        (this.TotalLiqPoint == null && obj.TotalLiqPoint != null)
                        || (this.TotalLiqPoint != null && obj.TotalLiqPoint == null)
                        ||
                        (
                             this.TotalLiqPoint != null && obj.TotalLiqPoint != null
                              && this.TotalLiqPoint.Value != obj.TotalLiqPoint.Value
                        )
                        )
                    {
                        return false;
                    }

                    if (
                            (this.ExcrementPoint == null && obj.ExcrementPoint != null)
                            || (this.ExcrementPoint != null && obj.ExcrementPoint == null)
                            ||
                            (
                                 this.ExcrementPoint != null && obj.ExcrementPoint != null
                                  && this.ExcrementPoint.Value != obj.ExcrementPoint.Value
                            )
                        )
                    {
                        return false;
                    }

                    if (
                            (this.UrinePoint == null && obj.UrinePoint != null)
                            || (this.UrinePoint != null && obj.UrinePoint == null)
                            ||
                            (
                                 this.UrinePoint != null && obj.UrinePoint != null
                                  && this.UrinePoint.Value != obj.UrinePoint.Value
                            )
                        )
                    {
                        return false;
                    }

                    if (this.Other1Points.Count != obj.Other1Points.Count)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < this.Other1Points.Count; i++)
                        {
                            if (this.Other1Points[i].Value != obj.Other1Points[i].Value)
                            {
                                return false;
                            }
                        }
                    }

                    if (
                            (this.WeightPoint == null && obj.WeightPoint != null)
                            || (this.WeightPoint != null && obj.WeightPoint == null)
                            ||
                            (
                                 this.WeightPoint != null && obj.WeightPoint != null
                                  && this.WeightPoint.Value != obj.WeightPoint.Value
                            )
                        )
                    {
                        return false;
                    }

                    if (this.PeauTestPoints.Count != obj.PeauTestPoints.Count)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < this.PeauTestPoints.Count; i++)
                        {
                            if (this.PeauTestPoints[i].Value != obj.PeauTestPoints[i].Value)
                            {
                                return false;
                            }
                        }
                    }

                    if (this.Other2Points.Count != obj.Other2Points.Count)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < this.Other2Points.Count; i++)
                        {
                            if (this.Other2Points[i].Value != obj.Other2Points[i].Value)
                            {
                                return false;
                            }
                        }
                    }

                    if (this.EventPoints.Count != obj.EventPoints.Count)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < this.EventPoints.Count; i++)
                        {
                            if (this.EventPoints[i].Value != obj.EventPoints[i].Value)
                            {
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    throw;
                }

                return true;
            }
        }

        /// <summary>
        /// 鼠标移动到数据点上是否弹出提示
        /// </summary>
        public bool EnableHoverToolTip { get; set; }

        ThreeItemsControlClickedEventArgs prevArgs = null;
        private void picContainer_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                ThreeItemsControlClickedEventArgs args = GetClickObjects(e.X, e.Y);

                if (prevArgs == null)
                {
                    prevArgs = args;
                }

                string text = string.Empty;

                if (args.objColumnCell != null)
                {
                    Cursor = Cursors.Hand;
                }
                else
                {
                    Cursor = Cursors.Default;
                }

                if (!prevArgs.Equal(args))
                {
                    Debug.WriteLine("Equal");
                    prevArgs = args;

                    if (!args.IsEmpty)
                    {
                        this.ttcHint.HideHint();
                    }

                    if (!args.IsEmpty)
                    {
                        foreach (ValuePoint tPoint in args.TempuPoints)
                        {
                            text += tPoint.ToString();
                        }

                        foreach (ValuePoint tPoint in args.PulsePoints)
                        {
                            text += tPoint.ToString();
                        }

                        foreach (ValuePoint tPoint in args.HeartRatePoints)
                        {
                            text += tPoint.ToString();
                        }

                        foreach (ValuePoint tPoint in args.BreathPoints)
                        {
                            text += tPoint.ToString();
                        }

                        foreach (ValuePoint tPoint in args.BloodPressurePoints)
                        {
                            text += tPoint.ToString();
                        }

                        if (args.TotalLiqPoint != null)
                        {
                            text += args.TotalLiqPoint.ToString();
                        }

                        if (args.ExcrementPoint != null)
                        {
                            text += args.ExcrementPoint.ToString();
                        }

                        if (args.UrinePoint != null)
                        {
                            text += args.UrinePoint.ToString();
                        }

                        foreach (ValuePoint tPoint in args.Other1Points)
                        {
                            text += tPoint.ToString();
                        }

                        if (args.WeightPoint != null)
                        {
                            text += args.WeightPoint.ToString();
                        }

                        foreach (ValuePoint ptPoint in args.PeauTestPoints)
                        {
                            text += ptPoint.ToString();
                        }

                        foreach (ValuePoint tPoint in args.Other2Points)
                        {
                            text += tPoint.ToString();
                        }

                        foreach (EventPoint eventPoint in args.EventPoints)
                        {
                            text += eventPoint.ToString();
                        }
                    }

                    if (text != string.Empty)
                    {
                        Point p = this.PointToScreen(new Point(e.X, e.Y));
                        this.ttcHint.ShowHint(text);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        #region Event点击
        public delegate void ThreeItemsControlClickedEventHandler(object sender, ThreeItemsControlClickedEventArgs args);
        public event ThreeItemsControlClickedEventHandler ItemClicked;
        #endregion

        /// <summary>
        /// 获取当前页图片
        /// </summary>
        /// <returns></returns>
        public Bitmap GetImage()
        {
            return this.grid.GetImage();
        }

        /// <summary>
        /// 导出图像
        /// </summary>
        public void Export()
        {

        }

        #region 文本改变事件
        public delegate void dgtTextChanged(int p_intType, string p_strChangedText);
        public event dgtTextChanged evtTextChanged;
        #endregion

        void grid_evtTextChanged(int p_intType, string p_strChangedText)
        {
            if (this.evtTextChanged != null)
            {
                this.evtTextChanged(p_intType, p_strChangedText);
            }
        }

    }
}
