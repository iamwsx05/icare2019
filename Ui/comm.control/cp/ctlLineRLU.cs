using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ctlLineRLU : ctlLineBase
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen p = new Pen(CpObject.ForeColor);
            p.Width = CpObject.LineWidth;

            Point pointStart = new Point(this.Width, this.Height);
            Point pointEnd = new Point(0, 0);

            e.Graphics.DrawLine(p, pointStart, pointEnd);

            Point[] pntArr = { PointA(), PointB(), pointEnd };

            e.Graphics.FillPolygon(CpObject.ForeBursh, pntArr);
        }

        private int LinLen = 30;

        private Point PointA()
        {
            double d1 = 0;
            double x1 = 0;
            double y1 = 0;

            if (this.Width >= this.Height)
            {
                d1 = Math.Sqrt(Math.Pow(this.Width, 2) / (Math.Pow(this.Width, 2) + Math.Pow(this.Height, 2)));
                x1 = LinLen * Math.Sqrt((d1 + 1) / 2);
                y1 = LinLen * Math.Sqrt((1 - d1) / 2);
            }
            else
            {
                d1 = Math.Sqrt(Math.Pow(this.Height, 2) / (Math.Pow(this.Width, 2) + Math.Pow(this.Height, 2)));
                x1 = (LinLen * (2 * d1 + 1) * Math.Sqrt((1 - d1) / 2));
                y1 = LinLen * Math.Sqrt((1 - Math.Pow((2 * d1 + 1) * Math.Sqrt((1 - d1) / 2), 2)));
            }

            return new Point(Convert.ToInt32(x1), Convert.ToInt32(y1));
        }

        private Point PointB()
        {
            double d1 = 0;
            double x1 = 0;
            double y1 = 0;

            if (this.Width >= this.Height)
            {
                d1 = Math.Sqrt(Math.Pow(this.Width, 2) / (Math.Pow(this.Width, 2) + Math.Pow(this.Height, 2)));
                x1 = LinLen * Math.Sqrt((1 - Math.Pow((2 * d1 + 1) * Math.Sqrt((1 - d1) / 2), 2)));
                y1 = (LinLen * (2 * d1 + 1) * Math.Sqrt((1 - d1) / 2));
            }
            else
            {
                d1 = Math.Sqrt(Math.Pow(this.Height, 2) / (Math.Pow(this.Width, 2) + Math.Pow(this.Height, 2)));
                x1 = LinLen * Math.Sqrt((1 - d1) / 2);
                y1 = LinLen * Math.Sqrt((d1 + 1) / 2);
            }

            return new Point(Convert.ToInt32(x1), Convert.ToInt32(y1));
        }
    }
}
