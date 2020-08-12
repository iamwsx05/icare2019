using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ctlLineLR : ctlLineBase
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen p = new Pen(CpObject.ForeColor);
            p.Width = CpObject.LineWidth;

            Point pointStart = new Point(0, this.Height/2);
            Point pointEnd = new Point(this.Width, this.Height / 2);

            e.Graphics.DrawLine(p, pointStart, pointEnd);           

            Point[] pntArr = { PointA(), PointB(), pointEnd };

            e.Graphics.FillPolygon(CpObject.ForeBursh, pntArr);
        }
                
        private Point PointA()
        {
            return new Point(this.Width - 25, this.Height/2 - 6);//8);
        }

        private Point PointB()
        {
            return new Point(this.Width - 25, this.Height / 2 + 6);//8);
        }

    }
}
