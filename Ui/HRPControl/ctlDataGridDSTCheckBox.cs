using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// Summary description for ctlDataGridDSTCheckBox.
	/// </summary>
	public class ctlDataGridDSTCheckBox : DataGridBoolColumn
	{
		private ArrayList m_arlDST = new ArrayList();

		public void m_mthSetDST(int m_intDSTIndex)
		{
			if(m_intDSTIndex >= 0)
				m_arlDST.Add(m_intDSTIndex);
			m_arlDST.Sort();
		}

		public bool m_blnIsDST(int p_intDSTIndex)
		{
			return m_arlDST.Contains(p_intDSTIndex);
		}

		private Color m_clrDST = Color.Red;

		public Color m_ClrDST
		{
			get
			{
				return m_clrDST;
			}
			set
			{
				m_clrDST = value;
			}
		}
		
		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight)
		{
			base.Paint(g,bounds,source,rowNum,backBrush,foreBrush,alignToRight);

			for(int i=0;i<m_arlDST.Count;i++)
			{
				if((int)m_arlDST[i] == rowNum)
				{
					Pen penDST = new Pen(m_clrDST);
					g.DrawLine(penDST,bounds.X,bounds.Y+bounds.Height/2,bounds.X+bounds.Width,bounds.Y+bounds.Height/2);			
					g.DrawLine(penDST,bounds.X,bounds.Y+bounds.Height/2+3,bounds.X+bounds.Width,bounds.Y+bounds.Height/2+3);			
					penDST.Dispose();
					break;
				}
			}
		}

		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, int rowNum, bool alignToRight)
		{
			base.Paint(g,bounds,source,rowNum,alignToRight);

			for(int i=0;i<m_arlDST.Count;i++)
			{
				if((int)m_arlDST[i] == rowNum)
				{
					Pen penDST = new Pen(m_clrDST);
					g.DrawLine(penDST,bounds.X,bounds.Y+bounds.Height/2,bounds.X+bounds.Width,bounds.Y+bounds.Height/2);			
					g.DrawLine(penDST,bounds.X,bounds.Y+bounds.Height/2+3,bounds.X+bounds.Width,bounds.Y+bounds.Height/2+3);			
					penDST.Dispose();
					break;
				}
			}
		}

		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, int rowNum)
		{
			base.Paint(g,bounds,source,rowNum);

			for(int i=0;i<m_arlDST.Count;i++)
			{
				if((int)m_arlDST[i] == rowNum)
				{
					Pen penDST = new Pen(m_clrDST);
					g.DrawLine(penDST,bounds.X,bounds.Y+bounds.Height/2,bounds.X+bounds.Width,bounds.Y+bounds.Height/2);			
					g.DrawLine(penDST,bounds.X,bounds.Y+bounds.Height/2+3,bounds.X+bounds.Width,bounds.Y+bounds.Height/2+3);			
					penDST.Dispose();
					break;
				}
			}		
		}
	}
}
