using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;

namespace Common.Controls.Emr
{
    public class RowHeaderColumn : ColumnBase
    {
        public RowHeaderColumn(DrawingGrid parent)
            : base(parent)
        {

        }

        public float PulseDimWidthUnit
        {
            get
            {
                return this.WidthUnit * 0.65f;
            }
        }

        public float TempuDimWidthUnit
        {
            get
            {
                return this.WidthUnit - PulseDimWidthUnit;
            }
        }

        public override float InitHeader()
        {
            float height = 0;

            float fontScale = 0.55f;

            DrawingGridColumnCell rowDate = new DrawingGridColumnCell(this);
            rowDate.HeightUnit = ThreeItemConstValue.HeaderHeight_Date;
            rowDate.WidthUnit = WidthUnit;
            rowDate.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowDate.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowDate.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowDate.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, rowDate.HeightUnit * this.SizePerUnit * fontScale);
            rowDate.TextProperty.Text = "日    期";
            this.Cells.Add(rowDate);

            height += rowDate.HeightUnit;

            DrawingGridColumnCell rowIndays = new DrawingGridColumnCell(this);
            rowIndays.HeightUnit = ThreeItemConstValue.HeaderHeight_InDays;
            rowIndays.WidthUnit = WidthUnit;
            rowIndays.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowIndays.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowIndays.TopUnit = Cells[Cells.Count - 1].HeightUnit + Cells[Cells.Count - 1].TopUnit;
            rowIndays.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, rowIndays.HeightUnit * this.SizePerUnit * fontScale);
            rowIndays.TextProperty.Text = "住院日数";
            Cells.Add(rowIndays);

            height += rowIndays.HeightUnit;

            DrawingGridColumnCell rowDaysAfOp = new DrawingGridColumnCell(this);
            rowDaysAfOp.HeightUnit = ThreeItemConstValue.Height_DaysAfOperation;
            rowDaysAfOp.WidthUnit = WidthUnit;
            rowDaysAfOp.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowDaysAfOp.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowDaysAfOp.TopUnit = Cells[Cells.Count - 1].HeightUnit + Cells[Cells.Count - 1].TopUnit;
            rowDaysAfOp.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, rowDaysAfOp.HeightUnit * this.SizePerUnit * fontScale);
            rowDaysAfOp.TextProperty.Text = " 手术或产后日数";
            Cells.Add(rowDaysAfOp);

            height += rowDaysAfOp.HeightUnit;

            DrawingGridColumnCell rowTime = new DrawingGridColumnCell(this);
            rowTime.HeightUnit = ThreeItemConstValue.HeaderHeight_Time;
            rowTime.WidthUnit = WidthUnit;
            rowTime.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowTime.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowTime.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowTime.TopUnit = Cells[Cells.Count - 1].HeightUnit + Cells[Cells.Count - 1].TopUnit;
            rowTime.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, rowDate.HeightUnit * this.SizePerUnit * fontScale); // new Font(TreeItemConstValue.DefaultFontFamilyName, rowTime.HeightUnit * this.SizePerUnit * fontScale);
            rowTime.TextProperty.Text = "时    间";
            Cells.Add(rowTime);

            height += rowTime.HeightUnit;

            return height;
        }

        public override float InitBody()
        {
            float height = 0;

            float heightunit = ((ThreeItemConstValue.MaxTemp - ThreeItemConstValue.MinTemp) / ThreeItemConstValue.TempuUnit) * ThreeItemConstValue.RowsPerTempUnit;

            //脉搏刻度
            DrawingGridColumnCell rowPulse = new DrawingGridColumnCell(this);
            rowPulse.TopUnit = this.HeaderHeightUnit;
            rowPulse.WidthUnit = PulseDimWidthUnit;
            rowPulse.HeightUnit = heightunit;

            rowPulse.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowPulse.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowPulse.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            Cells.Add(rowPulse);

            //温度刻度
            DrawingGridColumnCell rowTempu = new DrawingGridColumnCell(this);
            rowTempu.TopUnit = this.HeaderHeightUnit;
            rowTempu.WidthUnit = TempuDimWidthUnit;
            rowTempu.LeftUnit = PulseDimWidthUnit;
            rowTempu.HeightUnit = heightunit;

            rowTempu.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowTempu.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowTempu.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;

            Cells.Add(rowTempu);

            height = rowTempu.HeightUnit;

            return height;
        }

        public override float InitFooter(string registerId, int pageIndex)
        {
            float height = 0;

            float fontScale = 0.38f;

            //东华自定义其他
            DataTable dtResult = null;
            string strOther1 = "其他";
            string strOther2 = "其   他";
            if (dtResult != null)
            {
                DataRow[] drSelArr = dtResult.Select("colcode_vchr = 'Other1'");
                if (drSelArr != null && drSelArr.Length > 0)
                {
                    strOther1 = drSelArr[0]["coldesc_vchr"].ToString();
                }

                drSelArr = dtResult.Select("colcode_vchr = 'Other2'");
                if (drSelArr != null && drSelArr.Length > 0)
                {
                    strOther2 = drSelArr[0]["coldesc_vchr"].ToString();
                }
            }

            DrawingGridColumnCell rowBreath = new DrawingGridColumnCell(this);
            rowBreath.HeightUnit = ThreeItemConstValue.FooterHeight_Breath;
            rowBreath.TopUnit = this.HeaderHeightUnit + this.BodyHeightUnit;
            rowBreath.WidthUnit = WidthUnit;
            rowBreath.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowBreath.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowBreath.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowBreath.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowBreath.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, ThreeItemConstValue.FooterHeight_Breath * this.SizePerUnit * fontScale * 0.8f);
            rowBreath.TextProperty.Text = "呼 吸(次/分)";
            rowBreath.TextProperty.AlignHort = 1;
            height += rowBreath.HeightUnit;
            this.Cells.Add(rowBreath);


            DrawingGridColumnCell rowPressure = new DrawingGridColumnCell(this);
            rowPressure.HeightUnit = ThreeItemConstValue.FooterHeight_Blood;
            rowPressure.TopUnit = rowBreath.TopUnit + rowBreath.HeightUnit;
            rowPressure.WidthUnit = WidthUnit;
            rowPressure.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowPressure.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowPressure.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowPressure.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowPressure.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, ThreeItemConstValue.FooterHeight_Blood * this.SizePerUnit * fontScale * 0.8f);
            rowPressure.TextProperty.Text = "血  压(mmHg)";
            rowPressure.TextProperty.AlignHort = 1;
            height += rowPressure.HeightUnit;
            this.Cells.Add(rowPressure);

            DrawingGridColumnCell rowLiq = new DrawingGridColumnCell(this);
            rowLiq.HeightUnit = ThreeItemConstValue.FooterHeight_Liq;
            rowLiq.TopUnit = rowPressure.TopUnit + rowPressure.HeightUnit;
            rowLiq.WidthUnit = WidthUnit;
            rowLiq.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowLiq.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowLiq.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowLiq.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowLiq.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, ThreeItemConstValue.FooterHeight_Liq * this.SizePerUnit * fontScale * 0.8f);
            rowLiq.TextProperty.Text = "总入液量(ml)";
            rowLiq.TextProperty.AlignHort = 1;
            height += rowLiq.HeightUnit;
            this.Cells.Add(rowLiq);

            float width1 = this.WidthUnit * 0.33f;
            DrawingGridColumnCell rowEx = new DrawingGridColumnCell(this);
            rowEx.HeightUnit = ThreeItemConstValue.FooterHeight_DaBian * ThreeItemConstValue.FooterHeight_NiaoLiang + ThreeItemConstValue.FooterHeight_Other1;
            rowEx.TopUnit = rowLiq.TopUnit + rowLiq.HeightUnit;
            rowEx.WidthUnit = width1;
            rowEx.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, ThreeItemConstValue.FooterHeight_DaBian * this.SizePerUnit * fontScale);
            rowEx.TextProperty.IsVerticalText = true;
            rowEx.TextProperty.Text = "排出量";
            height += rowEx.HeightUnit;
            this.Cells.Add(rowEx);

            DrawingGridColumnCell rowEx1 = new DrawingGridColumnCell(this);
            rowEx1.HeightUnit = ThreeItemConstValue.FooterHeight_DaBian;
            rowEx1.TopUnit = rowLiq.TopUnit + rowLiq.HeightUnit;
            rowEx1.WidthUnit = this.WidthUnit - width1;
            rowEx1.LeftUnit = width1;
            rowEx1.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx1.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx1.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx1.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx1.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, ThreeItemConstValue.FooterHeight_DaBian * this.SizePerUnit * fontScale);
            rowEx1.TextProperty.Text = "大便(次)";
            height += rowEx1.HeightUnit;
            this.Cells.Add(rowEx1);

            DrawingGridColumnCell rowEx2 = new DrawingGridColumnCell(this);
            rowEx2.HeightUnit = ThreeItemConstValue.FooterHeight_NiaoLiang;
            rowEx2.TopUnit = rowLiq.TopUnit + rowLiq.HeightUnit + rowEx1.HeightUnit;
            rowEx2.WidthUnit = this.WidthUnit - width1;
            rowEx2.LeftUnit = width1;
            rowEx2.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx2.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx2.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx2.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx2.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, ThreeItemConstValue.FooterHeight_NiaoLiang * this.SizePerUnit * fontScale);
            rowEx2.TextProperty.Text = "尿量(ml)";
            height += rowEx2.HeightUnit;
            this.Cells.Add(rowEx2);

            this.ParentGrid.CellDate = new List<DrawingGridColumnCell>();
            DrawingGridColumnCell rowEx3 = new DrawingGridColumnCell(this);
            rowEx3.HeightUnit = ThreeItemConstValue.FooterHeight_Other1;
            rowEx3.TopUnit = rowLiq.TopUnit + rowLiq.HeightUnit + rowEx1.HeightUnit + rowEx2.HeightUnit;
            rowEx3.WidthUnit = this.WidthUnit - width1;
            rowEx3.LeftUnit = width1;
            rowEx3.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx3.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx3.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx3.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowEx3.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, ThreeItemConstValue.FooterHeight_Other1 * this.SizePerUnit * fontScale);
            rowEx3.TextProperty.Text = strOther1;
            rowEx3.Name = "Other1";
            height += rowEx3.HeightUnit;
            this.Cells.Add(rowEx3);
            this.ParentGrid.CellDate.Add(rowEx3);
            if (this.ParentGrid != null)
            {
                this.ParentGrid.m_mthTextChanged(1, strOther1);
            }

            DrawingGridColumnCell rowWeight = new DrawingGridColumnCell(this);
            rowWeight.HeightUnit = ThreeItemConstValue.FooterHeight_Weight;
            rowWeight.TopUnit = rowEx.TopUnit + rowEx.HeightUnit;
            rowWeight.WidthUnit = this.WidthUnit;
            rowWeight.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowWeight.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowWeight.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowWeight.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowWeight.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, ThreeItemConstValue.FooterHeight_Weight * this.SizePerUnit * fontScale);
            rowWeight.TextProperty.Text = "体重(kg)";
            height += rowWeight.HeightUnit;
            this.Cells.Add(rowWeight);


            DrawingGridColumnCell rowPeau = new DrawingGridColumnCell(this);
            rowPeau.HeightUnit = this.ParentGrid.FooterHeight_PiShi;
            rowPeau.TopUnit = rowWeight.TopUnit + rowWeight.HeightUnit;
            rowPeau.WidthUnit = this.WidthUnit;
            rowPeau.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowPeau.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowPeau.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowPeau.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowPeau.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, ThreeItemConstValue.FooterHeight_Weight * this.SizePerUnit * fontScale);
            rowPeau.TextProperty.Text = "皮   试";
            height += rowPeau.HeightUnit;
            this.Cells.Add(rowPeau);

            DrawingGridColumnCell rowOther = new DrawingGridColumnCell(this);
            rowOther.HeightUnit = this.ParentGrid.m_fltOtherFooterHeigth;
            rowOther.TopUnit = rowPeau.TopUnit + rowPeau.HeightUnit;
            rowOther.WidthUnit = this.WidthUnit;
            rowOther.Border.LeftBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowOther.Border.TopBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowOther.Border.RightBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowOther.Border.BottomBorderWidth = ThreeItemConstValue.OuterBorderWidth;
            rowOther.TextProperty.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, ThreeItemConstValue.FooterHeight_Other2 * this.SizePerUnit * fontScale);
            rowOther.TextProperty.Text = strOther2;
            rowOther.Name = "Other2";
            height += rowOther.HeightUnit;
            this.Cells.Add(rowOther);
            this.ParentGrid.CellDate.Add(rowOther);
            if (this.ParentGrid != null)
            {
                this.ParentGrid.m_mthTextChanged(2, strOther2);
            }

            return height;
        }

        public override float WidthUnit
        {
            get { return ThreeItemConstValue.RowHeaderWidth; }
        }

        /// <summary>
        /// 获取左上角坐标
        /// </summary>
        /// <returns></returns>
        public PointF GetLeftTopPosition()
        {
            float top = (this.TopOffsetUnit + this.HeaderHeightUnit) * this.SizePerUnit + this.ParentGrid.TopOffset;
            float left = this.ParentGrid.LeftOffset;

            return new PointF(left, top);
        }

        /// <summary>
        /// 获取左下角坐标
        /// </summary>
        /// <returns></returns>
        public PointF GetLeftBottomPosition()
        {
            PointF pLeftTop = GetLeftTopPosition();
            PointF pLeftBottom = new PointF(pLeftTop.X, pLeftTop.Y + this.BodyHeightUnit * this.SizePerUnit);

            return pLeftBottom;
        }

        /// <summary>
        /// 画刻度
        /// </summary>
        private void DrawDim()
        {
            PointF pLeftTop = GetLeftTopPosition();

            Font textDimFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.SizePerUnit * 1f);

            //画脉搏刻度
            float decHeightPerPUnit = (this.BodyHeightUnit * this.SizePerUnit) / ((ThreeItemConstValue.MaxPulse - ThreeItemConstValue.MinPulse) / ThreeItemConstValue.PulseUnit);

            int pCount = 0;


            for (float i = ThreeItemConstValue.MaxPulse; i >= ThreeItemConstValue.MinPulse; i -= ThreeItemConstValue.PulseUnit)
            {
                if (i != ThreeItemConstValue.MinPulse && i != ThreeItemConstValue.MaxPulse)
                {
                    string text = i.ToString();

                    SizeF textSize = this.graphics.MeasureString(text, textDimFont);

                    float textTopPos = pLeftTop.Y + decHeightPerPUnit * pCount - textSize.Height / 2;

                    float textLeftPos = this.ParentGrid.LeftOffset + this.PulseDimWidthUnit * this.SizePerUnit - textSize.Width;

                    this.graphics.DrawString(text, textDimFont, Brushes.Red, new PointF(textLeftPos, textTopPos));
                }
                pCount++;
            }


            //画温度刻度
            float decHeightPerTempuUnit = (this.BodyHeightUnit * this.SizePerUnit) / ((ThreeItemConstValue.MaxTemp - ThreeItemConstValue.MinTemp) / ThreeItemConstValue.TempuUnit);

            int tCount = 0;
            for (float i = ThreeItemConstValue.MaxTemp; i >= ThreeItemConstValue.MinTemp; i -= ThreeItemConstValue.TempuUnit)
            {
                if (i != ThreeItemConstValue.MinTemp && i != ThreeItemConstValue.MaxTemp)
                {
                    string textpoint = "°";
                    string text = i.ToString() + textpoint;

                    SizeF textSize = this.graphics.MeasureString(text, textDimFont);
                    SizeF pointSize = this.graphics.MeasureString(textpoint, textDimFont);

                    float textTopPos = pLeftTop.Y + decHeightPerTempuUnit * tCount - textSize.Height / 2 - 1;

                    float textLeftPos = this.ParentGrid.LeftOffset + PulseDimWidthUnit * this.SizePerUnit + this.TempuDimWidthUnit * this.SizePerUnit - textSize.Width + pointSize.Width / 2 - 1;

                    this.graphics.DrawString(text, textDimFont, Brushes.Black, new PointF(textLeftPos, textTopPos));
                }
                tCount++;
            }

            //标尺-脉搏
            string textPulse = "脉搏\n(次/分)";
            Font textFont = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.SizePerUnit * 0.8f);

            SizeF textPulseSize = this.graphics.MeasureString(textPulse, textFont);

            float textPulseTopPos = pLeftTop.Y + 2;

            float textPulseLeftPos = this.ParentGrid.LeftOffset + this.PulseDimWidthUnit * this.SizePerUnit - textPulseSize.Width;

            this.graphics.DrawString(textPulse, textFont, Brushes.Red, new PointF(textPulseLeftPos, textPulseTopPos));


            //标尺-体温
            string tempu = "体温\n℃";

            SizeF textTempuSize = this.graphics.MeasureString(tempu, textFont);

            float textTempuLeftPos = this.ParentGrid.LeftOffset + this.WidthUnit * this.SizePerUnit - textTempuSize.Width;

            this.graphics.DrawString(tempu, textFont, Brushes.Red, new PointF(textTempuLeftPos, textPulseTopPos));


            //画图形标识
            PointF pLeftBottom = this.GetLeftBottomPosition();

            Font textFont2 = new Font(ThreeItemConstValue.DefaultFontFamilyName, this.SizePerUnit * 0.8f);
            SizeF textSize2 = graphics.MeasureString("心率", textFont2);
            float pointSizeScale = 0.8f;
            float pointWidthScale = 0.2f;
            float pointBorderWidth = this.SizePerUnit * pointWidthScale;
            float pointDiameter = pointSizeScale * this.SizePerUnit;


            this.graphics.DrawString("心率", textFont2, new Pen(Color.Black).Brush, new PointF(pLeftBottom.X, pLeftBottom.Y - textSize2.Height * 1));
            this.graphics.DrawString("脉搏", textFont2, new Pen(Color.Black).Brush, new PointF(pLeftBottom.X, pLeftBottom.Y - textSize2.Height * 2));
            this.graphics.DrawString("肛表", textFont2, new Pen(Color.Black).Brush, new PointF(pLeftBottom.X, pLeftBottom.Y - textSize2.Height * 3));
            this.graphics.DrawString("腋表", textFont2, new Pen(Color.Black).Brush, new PointF(pLeftBottom.X, pLeftBottom.Y - textSize2.Height * 4));
            this.graphics.DrawString("口表", textFont2, new Pen(Color.Black).Brush, new PointF(pLeftBottom.X, pLeftBottom.Y - textSize2.Height * 5));

            this.DrawCircle(new PointF(pLeftBottom.X + textSize2.Width + this.SizePerUnit * 0.3f, pLeftBottom.Y - textSize2.Height * 1 + textSize2.Height / 2.7f), ThreeItemConstValue.ColorTempu_XinLv, pointDiameter, pointBorderWidth);
            this.DrawPoint(new PointF(pLeftBottom.X + textSize2.Width + this.SizePerUnit * 0.3f, pLeftBottom.Y - textSize2.Height * 2 + textSize2.Height / 2.7f), ThreeItemConstValue.ColorTempu_MaiBo, pointDiameter, pointBorderWidth);
            this.DrawCircle(new PointF(pLeftBottom.X + textSize2.Width + this.SizePerUnit * 0.3f, pLeftBottom.Y - textSize2.Height * 3 + textSize2.Height / 2.7f), ThreeItemConstValue.ColorTempu_GangBiao, pointDiameter, pointBorderWidth);
            this.DrawCrox(new PointF(pLeftBottom.X + textSize2.Width + this.SizePerUnit * 0.3f, pLeftBottom.Y - textSize2.Height * 4 + textSize2.Height / 2.7f), ThreeItemConstValue.ColorTempu_YeBiao, pointDiameter, pointBorderWidth);
            this.DrawPoint(new PointF(pLeftBottom.X + textSize2.Width + this.SizePerUnit * 0.3f, pLeftBottom.Y - textSize2.Height * 5 + textSize2.Height / 2.7f), ThreeItemConstValue.ColorTempu_KouBiao, pointDiameter, pointBorderWidth);

        }

        public override void Paint()
        {
            base.Paint();

            DrawDim();
        }
    }
}
