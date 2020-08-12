using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Common.Controls.Emr
{
    public class DrawingGridColumnCell
    {
        public DrawingGridColumnCell(ColumnBase parent)
        {
            this.ParentColumn = parent;
            this.TextProperty = new TextProperty();
            this.Border = new CellBorderProperty();

            this.WidthUnit = 1f;
            this.HeightUnit = 1f;

            this.Name = string.Empty;
        }

        #region Prop
        /// <summary>
        /// 父列
        /// </summary>
        public ColumnBase ParentColumn { get; private set; }

        /// <summary>
        /// 文本属性
        /// </summary>
        public TextProperty TextProperty { get; set; }

        /// <summary>
        /// 边框属性
        /// </summary>
        public CellBorderProperty Border { get; private set; }

        #region Size
        /// <summary>
        /// 宽度，单位：单元格单位
        /// </summary>
        public float WidthUnit { get; set; }

        /// <summary>
        /// 宽度，单位：像素
        /// </summary>
        public float Width
        {
            get
            {
                return WidthUnit * this.SizePerUnit;
            }
        }

        /// <summary>
        /// 高度，单位：单元格单位
        /// </summary>
        public float HeightUnit { get; set; }

        /// <summary>
        /// 高度，单位：像素
        /// </summary>
        public float Height
        {
            get
            {
                return this.HeightUnit * this.SizePerUnit;
            }
        }


        public int ColumnIndex
        {
            get
            {
                return this.ParentColumn.Index;
            }
        }

        public string Name { get; set; }
        #endregion

        #region Position
        /// <summary>
        /// 距离左侧距离，单位：单元格单位
        /// </summary>
        public float LeftUnit { get; set; }

        /// <summary>
        ///  距离左侧距离，单位：像素
        /// </summary>
        public float Left
        {
            get
            {
                return this.SizePerUnit * LeftUnit;
            }
        }

        /// <summary>
        /// 距离顶端距离，单位：单元格单位
        /// </summary>
        public float TopUnit { get; set; }

        /// <summary>
        /// 距离顶端距离，单位：像素
        /// </summary>
        public float Top
        {
            get
            {
                return this.SizePerUnit * this.TopUnit;
            }
        }
        #endregion

        /// <summary>
        /// 每单位大小
        /// </summary>
        public float SizePerUnit// { get; private set; }
        {
            get
            {
                return ParentColumn.SizePerUnit;
            }
        }


        /// <summary>
        /// GDI作图对象
        /// </summary>
        public Graphics graphics
        {
            get
            {
                return ParentColumn.graphics;
            }
        }
        #endregion

        /// <summary>
        /// 画边框
        /// </summary>
        private void DrawBorder()
        {
            Pen p = new Pen(Brushes.Black);

            if (Border.DrawLeftBorder)
            {
                //画左线
                p.Width = Border.LeftBorderWidth;
                p.Color = Border.LeftBorderColor;
                graphics.DrawLine(p, GetLeftTopPosition(), GetLeftBottomPosition());
            }

            if (Border.DrawTopBorder)
            {
                p.Width = Border.TopBorderWidth;
                p.Color = Border.TopBorderColor;
                graphics.DrawLine(p, GetLeftTopPosition(), GetRightTopPotision());
            }

            if (Border.DrawRightBorder)
            {
                p.Width = Border.RightBorderWidth;
                p.Color = Border.RightBorderColor;
                graphics.DrawLine(p, GetRightTopPotision(), GetRightBottomPosition());
            }

            if (Border.DrawBottomBorder)
            {
                p.Width = Border.BottomBorderWidth;
                p.Color = Border.BottomBorderColor;
                graphics.DrawLine(p, GetLeftBottomPosition(), GetRightBottomPosition());
            }
        }

        /// <summary>
        /// 画文本
        /// </summary>
        private void DrawText()
        {
            if (!string.IsNullOrEmpty(this.TextProperty.Text))
            {
                //Pen pen = new Pen(this.Text.ForeColor);

                SizeF size = graphics.MeasureString(this.TextProperty.Text, this.TextProperty.Font);
                Font textFont = this.TextProperty.Font;
                float fMargin = this.WidthUnit * this.SizePerUnit / 10f;


                StringFormat sf = new StringFormat();

                //文字坐标
                PointF pos;
                float X = 0;
                float Y = 0;

                if (this.TextProperty.IsVerticalText)
                {
                    sf.FormatFlags = StringFormatFlags.DirectionVertical;

                    if (this.TextProperty.AlignHort == 0)
                    {
                        X = GetLeftTopPosition().X;
                    }
                    else if (this.TextProperty.AlignHort == 2)
                    {
                        X = GetLeftTopPosition().X + this.Width - size.Height;
                    }
                    else
                    {
                        X = GetLeftTopPosition().X + this.Width / 2 - size.Height / 2;
                    }

                    if (this.TextProperty.AlignVert == 0)
                    {
                        Y = GetLeftTopPosition().Y;
                    }
                    else if (this.TextProperty.AlignVert == 2)
                    {
                        Y = GetLeftTopPosition().Y + this.Height - size.Width + this.SizePerUnit * 0.2f;
                    }
                    else
                    {
                        Y = GetLeftTopPosition().Y + this.Height / 2 - size.Width / 2;
                    }
                }
                else
                {
                    if (this.TextProperty.AlignHort == 0)
                    {
                        X = GetLeftTopPosition().X;
                    }
                    else if (this.TextProperty.AlignHort == 2)
                    {
                        X = GetLeftTopPosition().X + this.Width - size.Width + this.SizePerUnit * 0.2f;
                    }
                    else
                    {
                        X = (GetLeftTopPosition().X + this.Width / 2 - size.Width / 2);// +this.SizePerUnit * 0.1f;
                    }

                    if (this.TextProperty.AlignVert == 0)
                    {
                        Y = GetLeftTopPosition().Y + this.SizePerUnit * 0.2f;
                    }
                    else if (this.TextProperty.AlignVert == 2)
                    {
                        Y = GetLeftTopPosition().Y + this.Height - size.Height;
                    }
                    else
                    {
                        Y = GetLeftTopPosition().Y + this.Height / 2 - size.Height / 2 + this.SizePerUnit * 0.15f;
                    }
                }
                pos = new PointF(X, Y);

                Pen pp = new Pen(this.TextProperty.ForeColor);
                graphics.DrawString(this.TextProperty.Text, this.TextProperty.Font, pp.Brush, pos, sf);
            }
        }

        #region 获取顶点位置
        /// <summary>
        /// 获取左上顶点位置
        /// </summary>
        /// <returns></returns>
        public PointF GetLeftTopPosition()
        {
            PointF p = new PointF(this.Left + this.ParentColumn.LeftOffset + this.ParentColumn.ParentGrid.LeftOffset,
                                this.Top + this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset);

            return p;
        }

        public PointF GetRightTopPotision()
        {
            PointF p = new PointF(this.Left + this.Width + this.ParentColumn.LeftOffset + this.ParentColumn.ParentGrid.LeftOffset,
                                  this.Top + this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset);

            return p;
        }

        public PointF GetLeftBottomPosition()
        {
            PointF p = new PointF(this.Left + this.ParentColumn.LeftOffset + this.ParentColumn.ParentGrid.LeftOffset,
                                  this.Top + this.Height + this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset);

            return p;
        }

        public PointF GetRightBottomPosition()
        {
            PointF p = new PointF(this.Left + this.Width + this.ParentColumn.LeftOffset + this.ParentColumn.ParentGrid.LeftOffset,
                                  this.Top + this.Height + this.ParentColumn.TopOffset + this.ParentColumn.ParentGrid.TopOffset);

            return p;
        }
        #endregion

        /// <summary>
        /// 作图
        /// </summary>
        public void Paint()
        {
            //this.SizePerUnit = sizePerUnit;
            DrawBorder();
            DrawText();
        }
    }
}
