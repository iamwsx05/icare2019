using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    public class DrawingGrid //: INotifyPropertyChanged
    {
        #region .ctor
        public DrawingGrid(float fSizePerUnit, int columnsPerPage)
        {
            this.SizePerUnit = fSizePerUnit;
            this.ColumnsPerPage = columnsPerPage;
            this.LeftOffset = 5;
            this.TopOffset = 10;
            InitializeComponent();
        }
        #endregion

        public void InitializeComponent()
        {
            Caption = new GridCaption(this);
            Caption.Init();

            RowHeader = new RowHeaderColumn(this);
            //RowHeader.InitCells();
            Columns = new ColumnCollection();
            VisibleColumns = new ColumnCollection();
        }

        #region Prop&Field
        /// <summary>
        /// 图画对象
        /// </summary>
        private Bitmap image;

        /// <summary>
        /// GDI作图对象
        /// </summary>
        public Graphics graphics { get; private set; }

        /// <summary>
        /// 图像距左端距离
        /// </summary>
        public float LeftOffset { get; set; }

        /// <summary>
        /// 图像距顶端距离
        /// </summary>
        public float TopOffset { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public object DataSource { get; set; }


        //private float _sizeperunit;
        /// <summary>
        /// 每单位大小
        /// </summary>
        public float SizePerUnit { get; set; }
        //{
        //    get
        //    {
        //        return _sizeperunit;
        //    }
        //    set
        //    {
        //        if (_sizeperunit != value)
        //        {
        //            _sizeperunit = value;
        //            //OnSizePerUnitChanged(_sizeperunit);
        //        }
        //    }
        //}

        /// <summary>
        /// 每页列数
        /// </summary>
        public int ColumnsPerPage { get; set; }

        public int PageIndex { get; set; }
        public int PageCount { get; set; }

        public RowHeaderColumn RowHeader { get; private set; }
        public ColumnCollection Columns { get; private set; }
        public ColumnCollection VisibleColumns { get; private set; }
        public GridCaption Caption { get; private set; }

        public List<ThreeItemsColumnData> ColumnsData;

        /// <summary>
        /// 特殊列
        /// </summary>
        public List<DrawingGridColumnCell> CellDate;

        public float FooterHeight_PiShi = 2f;
        public float m_fltOtherFooterHeigth = 2f;
        #endregion

        public EntityEmrTemperaturePatInfo PatInfo = null;

        public void LoadData(EntityEmrTemperaturePatInfo pInfo, List<ThreeItemsColumnData> data)
        {
            ColumnsData = data;
            PatInfo = pInfo;

            try
            {
                InitColumns();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InitColumns()
        {
            try
            {

                //PageIndex = 0;
                //PageCount = 0;

                int emptyColumnsCount = 0;

                this.Columns.Clear();
                if (ColumnsData.Count <= this.ColumnsPerPage)
                {
                    this.PageCount = 1;
                    emptyColumnsCount = this.ColumnsPerPage - ColumnsData.Count;
                }
                else
                {
                    if (ColumnsData.Count % this.ColumnsPerPage == 0)
                    {
                        this.PageCount = ColumnsData.Count / this.ColumnsPerPage;
                    }
                    else
                    {
                        this.PageCount = (ColumnsData.Count / this.ColumnsPerPage) + 1;
                        emptyColumnsCount = this.ColumnsPerPage - ColumnsData.Count % this.ColumnsPerPage;
                    }
                }

                if (this.PageCount < this.PageIndex + 1)
                {
                    this.PageIndex = this.PageCount - 1;
                }

                foreach (ThreeItemsColumnData item in this.ColumnsData)
                {
                    DrawingDataColumn col = new DrawingDataColumn(this, item);
                    this.Columns.Add(col);
                    col.Index = this.Columns.Count - 1;
                    //col.InitCells();
                }

                DateTime lastDate = DateTime.Now;
                if (this.Columns.Count > 0)
                {
                    lastDate = this.Columns[this.Columns.Count - 1].Value.RecDate;
                }

                for (int i = 0; i < emptyColumnsCount; i++)
                {
                    DrawingDataColumn emptyCol = new DrawingDataColumn(this, null);
                    emptyCol.Date = lastDate.AddDays(i + 1);
                    this.Columns.Add(emptyCol);
                    emptyCol.Index = this.Columns.Count - 1;
                    //emptyCol.InitCells();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private float FooterTotalHeight
        {
            get
            {
                return ThreeItemConstValue.FooterHeight_Breath
                    + ThreeItemConstValue.FooterHeight_Blood
                    + ThreeItemConstValue.FooterHeight_Liq
                    + ThreeItemConstValue.FooterHeight_DaBian
                    + ThreeItemConstValue.FooterHeight_NiaoLiang
                    + ThreeItemConstValue.FooterHeight_Other1
                    + ThreeItemConstValue.FooterHeight_Weight
                    + FooterHeight_PiShi
                    + m_fltOtherFooterHeigth;
                //+ ThreeItemConstValue.FooterHeight_Other2;
            }
        }

        public void Paint(float sizePerUnit)
        {
            image = GetImageByPage(sizePerUnit, this.PageIndex);
        }

        private Bitmap GetImageByPage(float sizePerUnit, int pageIndex)
        {
            try
            {
                #region 计算皮试行最大高度
                int maxPeauTestRecCount = 0;
                int colFrom = pageIndex * this.ColumnsPerPage;
                int colTo = (pageIndex + 1) * this.ColumnsPerPage - 1;
                for (int i = colFrom; i <= colTo; i++)
                {
                    if (this.Columns[i].Value != null && this.Columns[i].Value.PeauTestValues.Count > maxPeauTestRecCount)
                    {
                        maxPeauTestRecCount = this.Columns[i].Value.PeauTestValues.Count;
                    }
                }

                if (maxPeauTestRecCount > 1)
                {
                    this.FooterHeight_PiShi = maxPeauTestRecCount * 1.5f;
                }
                else
                {
                    this.FooterHeight_PiShi = 2f;
                }
                #endregion

                #region 计算其它行最大高度
                int maxOtherCount = 0;
                int intOtherCount = 0;
                for (int i = colFrom; i <= colTo; i++)
                {
                    intOtherCount = 0;
                    if (this.Columns[i].Value != null && this.Columns[i].Value.Other2Values.Count > 0)
                    {
                        string strText = string.Empty;
                        List<string> lstValue = null;
                        foreach (EntityEmrTemperatureMonitorData objTemp in this.Columns[i].Value.Other2Values)
                        {
                            lstValue = new List<string>();
                            strText = objTemp.measureValue + " " + objTemp.measureValue2;
                            if (System.Text.Encoding.Default.GetBytes(strText).Length > 14)
                            {
                                int pos = 0;
                                for (int k = 1; k <= strText.Length; k++)
                                {
                                    if (System.Text.Encoding.Default.GetBytes(strText.Substring(pos, k - pos)).Length == 14)
                                    {
                                        lstValue.Add(strText.Substring(pos, k - pos));
                                        pos = k;
                                        if (k < strText.Length && System.Text.Encoding.Default.GetBytes(strText.Substring(pos)).Length < 14)
                                        {
                                            lstValue.Add(strText.Substring(k));
                                            pos = strText.Length;
                                        }
                                        if (pos == strText.Length) break;
                                    }
                                }
                            }
                            else
                            {
                                lstValue.Add(strText);
                            }
                            intOtherCount += lstValue.Count;
                        }

                        if (intOtherCount > maxOtherCount)
                        {
                            maxOtherCount = intOtherCount;
                        }
                    }
                }

                if (maxOtherCount > 1)
                {
                    this.m_fltOtherFooterHeigth = maxOtherCount * 1.5f;
                }
                else
                {
                    this.m_fltOtherFooterHeigth = 2f;
                }
                #endregion

                #region 计算每页宽度,高度

                bool recreate = false;
                if (this.SizePerUnit != sizePerUnit || pageIndex != 0)
                {
                    this.SizePerUnit = sizePerUnit;
                    recreate = true;
                }

                //计算每页宽度,高度
                float colsWidth = 0;
                if (this.ColumnsPerPage > 0)
                {
                    colsWidth = this.ColumnsPerPage * (this.Columns[0].WidthUnit * this.SizePerUnit);
                }

                float imgWidth = this.LeftOffset + this.RowHeader.WidthUnit * this.SizePerUnit + colsWidth;
                float imgHeight = this.TopOffset * 2 + (this.Caption.HeightUnit + ThreeItemConstValue.HeaderTotalHeight + this.FooterTotalHeight + (ThreeItemConstValue.MaxTemp - ThreeItemConstValue.MinTemp) * ThreeItemConstValue.RowsPerTempUnit) * this.SizePerUnit;

                #endregion

                #region 根据当前页计算显示的记录
                string RegID = this.PatInfo.RegID;


                this.VisibleColumns.Clear();
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    this.Columns[i].InitCells(RegID, pageIndex);

                    if (i >= pageIndex * this.ColumnsPerPage && i <= ((pageIndex + 1) * this.ColumnsPerPage) - 1)
                    {
                        this.Columns[i].m_mthSetVisible(RegID, pageIndex, true);
                        this.VisibleColumns.Add(this.Columns[i]);
                    }
                    else
                    {
                        this.Columns[i].m_mthSetVisible(RegID, pageIndex, false);
                    }
                }

                #endregion

                Bitmap img = new Bitmap((int)imgWidth + 1, (int)imgHeight + 1);
                graphics = Graphics.FromImage(img);
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                this.graphics.Clear(Color.White);
                this.SizePerUnit = sizePerUnit;

                if (recreate)
                {
                    Caption.Init();

                }
                Caption.Paint(this.PatInfo, pageIndex, pageIndex + 1 == PageCount);

                RowHeader.InitCells(RegID, pageIndex);
                RowHeader.Paint();

                bool blnTemp = false;
                foreach (DrawingDataColumn col in this.Columns)
                {
                    col.BlnLastUp = blnTemp;
                    if (recreate)
                    {
                        col.InitCells(RegID, pageIndex);
                    }
                    col.Paint();
                    blnTemp = col.BlnLastUp;
                }

                return img;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public Bitmap GetImage()
        {
            return this.image;
        }

        public void NextPage()
        {
            if (this.PageIndex + 1 < this.PageCount)
            {
                this.PageIndex++;
            }
        }

        public void PrevPage()
        {
            if (this.PageIndex > 0)
            {
                this.PageIndex--;
            }
        }

        public void FirstPage()
        {
            this.PageIndex = 0;
        }

        public void LastPage()
        {
            this.PageIndex = this.PageCount - 1;
        }

        public bool IndexPage(int pageNo)
        {
            if (pageNo >= 0 && pageNo < this.PageCount)
            {
                this.PageIndex = pageNo;
                return true;
            }
            return false;
        }

        public Bitmap[] GetImages()
        {
            Bitmap[] images = new Bitmap[this.PageCount];
            for (int i = 0; i < this.PageCount; i++)
            {
                images[i] = GetImageByPage(this.SizePerUnit, i);
            }
            return images;
        }

        #region 文本改变事件
        public delegate void dgtTextChanged(int p_intType, string p_strChangedText);
        public event dgtTextChanged evtTextChanged;
        #endregion

        public void m_mthTextChanged(int p_intType, string p_strChangedTex)
        {
            if (evtTextChanged != null)
            {
                evtTextChanged(p_intType, p_strChangedTex);
            }
        }
    }
}
