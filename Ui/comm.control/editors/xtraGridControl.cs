using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Common.Controls
{
    public partial class xtraGridControl : DevExpress.XtraGrid.GridControl
    {
         public xtraGridControl()
        {
            InitializeComponent();
        }

         public xtraGridControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

         public void ShowRowNo(DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
         {
             if (e.Info.IsRowIndicator && e.RowHandle >= 0)
             {
                 e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                 e.Appearance.ForeColor = Color.Gray;
                 e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
             }
         }

         public void SetFocusCell(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bgv, int row, DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col)
         {
             bgv.FocusedRowHandle = row;
             bgv.FocusedColumn = col;
         }

         public void SetCellColorWarning(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
         {
             e.Appearance.BackColor = Color.OrangeRed;
             e.Appearance.BackColor2 = Color.White;
             e.Appearance.ForeColor = Color.Black;
         }

         public void SetCellColorEmpty(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
         {
             //e.Appearance.BackColor = Color.White;
             e.Appearance.ForeColor = Color.White;
         }
                
         public void DrawCellBorder(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
         {
             Brush brush = Brushes.Red;
             e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.X - 2, e.Bounds.Y - 2, e.Bounds.Width, 1));
             e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.Right + 2, e.Bounds.Y - 2, 1, e.Bounds.Height));
             e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.X - 2, e.Bounds.Bottom, e.Bounds.Width, 1));
             e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.X - 2, e.Bounds.Y - 2, 1, e.Bounds.Height));
         }

    }
}
