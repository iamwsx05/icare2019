using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ctlLineRL : ctlLineBase
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen p = new Pen(CpObject.ForeColor);
            p.Width = CpObject.LineWidth;

            Point pointStart = new Point(this.Width, this.Height / 2);
            Point pointEnd = new Point(0, this.Height / 2);

            e.Graphics.DrawLine(p, pointStart, pointEnd);

            Point[] pntArr = { PointA(), PointB(), pointEnd };

            e.Graphics.FillPolygon(CpObject.ForeBursh, pntArr);
        }

        private Point PointA()
        {
            return new Point(25, this.Height / 2 - 8);
        }

        private Point PointB()
        {
            return new Point(25, this.Height / 2 + 8);
        }
    }
}
