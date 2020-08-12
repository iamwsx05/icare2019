using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ctlLineDU : ctlLineBase
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen p = new Pen(CpObject.ForeColor);
            p.Width = CpObject.LineWidth;

            Point pointStart = new Point(this.Width / 2, this.Height);
            Point pointEnd = new Point(this.Width / 2, 0);

            e.Graphics.DrawLine(p, pointStart, pointEnd);

            Point[] pntArr = { PointA(), PointB(), pointEnd };

            e.Graphics.FillPolygon(CpObject.ForeBursh, pntArr);
        }

        private Point PointA()
        {
            return new Point(this.Width / 2 - 8, 25);
        }

        private Point PointB()
        {
            return new Point(this.Width / 2 + 8, 25);
        }
        
    }
}
