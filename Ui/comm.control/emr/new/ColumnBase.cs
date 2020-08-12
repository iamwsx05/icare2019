using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Common.Controls.Emr
{
    public abstract class ColumnBase
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="parent"></param>
        public ColumnBase(DrawingGrid parent)
        {
            this.ParentGrid = parent;
            Cells = new List<DrawingGridColumnCell>();
            this._visible = true;
            this.TopOffsetUnit = this.ParentGrid.Caption.HeightUnit;
        }

        /// <summary>
        /// 每单位大小
        /// </summary>
        public float SizePerUnit
        {
            get
            {
                return this.ParentGrid.SizePerUnit;
            }
        }

        /// <summary>
        /// 父网格
        /// </summary>
        public DrawingGrid ParentGrid { get; private set; }

        /// <summary>
        /// 列索引
        /// </summary>
        public int Index { get; set; }


        private bool _visible;
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visible
        {
            get
            {
                return this._visible;
            }
        }

        public void m_mthSetVisible(string registerID, int pageIndex,bool p_blnVisible)
        {
            if (this._visible != p_blnVisible)
            {
                this._visible = p_blnVisible;
                this.InitCells(registerID, pageIndex);
            }
        }

        #region LeftOffset
        public float LeftOffset
        {
            get
            {
                return this.LeftOffsetUnit * this.SizePerUnit;
            }
        }

        public float LeftOffsetUnit
        {
            get
            {
                if (this is DrawingDataColumn)
                {
                    int visibleindex = GetVisibleIndex();
                    return visibleindex * this.WidthUnit + this.ParentGrid.RowHeader.WidthUnit;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region TopOffset

        public float TopOffset
        {
            get
            {
                return TopOffsetUnit * this.SizePerUnit;
            }
        }

        //float _topOffsetUnit;
        public float TopOffsetUnit { get; set; }
        //{
        //    get
        //    {
        //        return _topOffsetUnit;
        //    }
        //    set
        //    {
        //        _topOffsetUnit = value;
        //        //TopOffset = value * this.SizePerUnit;
        //    }
        //}

        #endregion

        /// <summary>
        /// 单元格
        /// </summary>
        public List<DrawingGridColumnCell> Cells { get; private set; }

        public virtual void InitCells(string registerID, int pageIndex)
        {
            this.Cells.Clear();

            if (this.Visible)
            {
                HeaderHeightUnit = InitHeader();
                BodyHeightUnit = InitBody();
                FooterHeightUnit = InitFooter(registerID, pageIndex);
            }
        }

        public float HeaderHeightUnit { get; private set; }
        public float BodyHeightUnit { get; private set; }
        public float FooterHeightUnit { get; private set; }

        public abstract float InitHeader();
        public abstract float InitBody();
        public abstract float InitFooter(string registerID, int pageIndex);
        public abstract float WidthUnit { get; }

        /// <summary>
        /// GDI作图对象
        /// </summary>
        public Graphics graphics
        {
            get
            {
                return ParentGrid.graphics;
            }
        }

        public virtual void Paint()
        {
            if (this.Visible)
            {
                //InitCells();

                foreach (DrawingGridColumnCell cell in this.Cells)
                {
                    cell.Paint();
                }
            }
        }

        public bool IsFirstVisibleColumn
        {
            get
            {
                return (GetVisibleIndex() == 0);
            }
        }

        public bool IsLastVisibleColumn
        {
            get
            {
                return ((GetVisibleIndex() + 1) % this.ParentGrid.ColumnsPerPage == 0);
            }
        }

        public int GetVisibleIndex()
        {
            int visibleindex = 0;
            for (int i = 0; i < this.ParentGrid.Columns.Count; i++)
            {
                if (this.ParentGrid.Columns[i].Visible == true)
                {
                    if (this.ParentGrid.Columns[i] != this)
                    {
                        visibleindex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return visibleindex;
        }

        public int GetVisbleCount()
        {
            int count = 0;
            for (int i = 0; i < this.ParentGrid.Columns.Count; i++)
            {
                if (this.ParentGrid.Columns[i].Visible == true)
                {
                    count++;
                }
            }
            return count;
        }

        public bool IsFirstColumn
        {
            get
            {
                return this.Index == 0;
            }
        }

        /// <summary>
        /// 画圈
        /// </summary>
        /// <param name="pointCenterPos"></param>
        /// <param name="color"></param>
        public void DrawCircle(PointF centerPos, Color color, float diameter, float borderWidth)
        {
            Pen pen = new Pen(color);
            pen.Width = borderWidth;

            PointF location = new PointF(centerPos.X - diameter / 2f, centerPos.Y - diameter / 2f);

            this.graphics.DrawArc(pen, new RectangleF(location, new SizeF(diameter, diameter)), 0, 360);
        }

        /// <summary>
        /// 画点
        /// </summary>
        /// <param name="pointCenterPos"></param>
        /// <param name="color"></param>
        public void DrawPoint(PointF centerPos, Color color, float diameter, float borderWidth)
        {
            Pen pen = new Pen(color);
            pen.Width = borderWidth;


            float dia2 = diameter * 1.2f;

            PointF location = new PointF(centerPos.X - dia2 / 2f, centerPos.Y - dia2 / 2f);

            this.graphics.FillEllipse(pen.Brush, new RectangleF(location, new SizeF(dia2, dia2)));
        }

        /// <summary>
        /// 画叉
        /// </summary>
        /// <param name="pointCenterPos"></param>
        /// <param name="color"></param>
        public void DrawCrox(PointF centerPos, Color color, float diameter, float borderWidth)
        {
            Pen pen = new Pen(color);
            pen.Width = borderWidth;

            PointF topLeft = new PointF(centerPos.X - diameter / 2f, centerPos.Y - diameter / 2f);
            PointF topRight = new PointF(centerPos.X + diameter / 2f, centerPos.Y - diameter / 2f);
            PointF bottomLeft = new PointF(centerPos.X - diameter / 2f, centerPos.Y + diameter / 2f);
            PointF bottomRight = new PointF(centerPos.X + diameter / 2f, centerPos.Y + diameter / 2f);

            this.graphics.DrawLine(pen, topLeft, bottomRight);
            this.graphics.DrawLine(pen, topRight, bottomLeft);
        }

        /// <summary>
        /// 画文字
        /// </summary>
        /// <param name="location"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="foreColor"></param>
        /// <param name="isVeri"></param>
        public void DrawText(PointF location, string text, Font font, Color foreColor, bool isVeri)
        {
            if (isVeri)
            {
                StringFormat sf = new StringFormat();
                sf.FormatFlags = StringFormatFlags.DirectionVertical;
                this.graphics.DrawString(text, font, new Pen(foreColor).Brush, location, sf);
            }
            else
            {
                this.graphics.DrawString(text, font, new Pen(foreColor).Brush, location);
            }
        }

        public float X1
        {
            get
            {
                if (this.Visible)
                {
                    return this.LeftOffset + this.ParentGrid.LeftOffset;
                }
                else
                {
                    return 0;
                }
            }
        }

        public float X2
        {
            get
            {
                if (this.Visible)
                {
                    return this.LeftOffset + this.ParentGrid.LeftOffset + this.WidthUnit * this.SizePerUnit;
                }
                else
                {
                    return 0;
                }
            }
        }

        public float Y1
        {
            get
            {
                if (this.Visible)
                {
                    return this.TopOffset + this.ParentGrid.TopOffset;
                }
                else
                {
                    return 0;
                }
            }
        }

        public float Y2
        {
            get
            {
                if (this.Visible)
                {
                    return this.TopOffset + this.ParentGrid.TopOffset + (this.HeaderHeightUnit + this.BodyHeightUnit + this.FooterHeightUnit) * this.SizePerUnit;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
