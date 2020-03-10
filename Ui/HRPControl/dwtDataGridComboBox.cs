using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace com.digitalwave.Utility
{
	/// <summary>
	/// 
	/// </summary>
	public class dwtDataGridComboBox : DataGridColumnStyle
	{
		private ComboBox cboColumn;

		private ArrayList alOldValue,alNewValue;

		private int intCurrentRow;
		private CurrencyManager objSource;
		private bool blnCanSet;

		private bool m_blnReadOnly = false;

		private ArrayList m_arlDST = new ArrayList();

		public void m_mthSetDST(int m_intDSTIndex)
		{
			if(m_intDSTIndex >= 0)
				m_arlDST.Add(m_intDSTIndex);
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

		public dwtDataGridComboBox()
		{
			cboColumn = new ComboBox();
			cboColumn.DropDownStyle = ComboBoxStyle.DropDown;
			cboColumn.Text = "";
			cboColumn.Visible = false;
			cboColumn.SelectedIndexChanged += new EventHandler(ComboSelectedText);
			cboColumn.TextChanged += new EventHandler(ComboTextChanged);

			alOldValue = new ArrayList(10);
			alNewValue = new ArrayList(10);

			intCurrentRow = 0;

			blnCanSet = false;
			
		}

		#region Override Method
		protected override void Abort(int rowNum)
		{
		}
        
		protected override bool Commit(System.Windows.Forms.CurrencyManager dataSource, int rowNum)
		{
			HideComboBox();
			return true;
		}

		protected override void Edit(System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{			
			intCurrentRow = rowNum;
			blnCanSet = false;

			string text = strGetText(GetColumnValueAtRow(source,rowNum));
			
			//liyi 2002-7-23 ¸ÄÉÆComboBoxµÄÏÔÊ¾ 
			if(text == NullText && cboColumn.DropDownStyle == ComboBoxStyle.DropDownList)
				cboColumn.SelectedIndex = -1;
			else
				cboColumn.Text = text;


			blnCanSet = true;

			if(cellIsVisible)
			{
				objSource = source;
				
				ShowComboBox(bounds);				
			}
			else
			{
				HideComboBox();
			}			
		}

		protected override void Paint(Graphics g,Rectangle Bounds,CurrencyManager Source,int RowNum)
		{
			Paint(g, Bounds, Source, RowNum, false);

			for(int i=0;i<m_arlDST.Count;i++)
			{
				if((int)m_arlDST[i] == RowNum)
				{
					Pen penDST = new Pen(m_clrDST);
					g.DrawLine(penDST,Bounds.X,Bounds.Y+Bounds.Height/2,Bounds.X+Bounds.Width,Bounds.Y+Bounds.Height/2);			
					g.DrawLine(penDST,Bounds.X,Bounds.Y+Bounds.Height/2+3,Bounds.X+Bounds.Width,Bounds.Y+Bounds.Height/2+3);			
					penDST.Dispose();
					break;
				}
			}
		}
		protected override void Paint(Graphics g,Rectangle Bounds,CurrencyManager Source,int RowNum,bool AlignToRight)
		{
			string Text = strGetText(GetColumnValueAtRow(Source, RowNum));

			PaintText(g, Bounds, Text, AlignToRight);

			for(int i=0;i<m_arlDST.Count;i++)
			{
				if((int)m_arlDST[i] == RowNum)
				{
					Pen penDST = new Pen(m_clrDST);
					g.DrawLine(penDST,Bounds.X,Bounds.Y+Bounds.Height/2,Bounds.X+Bounds.Width,Bounds.Y+Bounds.Height/2);			
					g.DrawLine(penDST,Bounds.X,Bounds.Y+Bounds.Height/2+3,Bounds.X+Bounds.Width,Bounds.Y+Bounds.Height/2+3);			
					penDST.Dispose();
					break;
				}
			}
		}
		protected override void Paint(Graphics g,Rectangle Bounds,CurrencyManager Source,int RowNum,Brush BackBrush ,Brush ForeBrush ,bool AlignToRight)
		{
			string Text = strGetText(GetColumnValueAtRow(Source, RowNum));

			PaintText(g, Bounds, Text, BackBrush, ForeBrush, AlignToRight);

			for(int i=0;i<m_arlDST.Count;i++)
			{
				if((int)m_arlDST[i] == RowNum)
				{
					Pen penDST = new Pen(m_clrDST);
					g.DrawLine(penDST,Bounds.X,Bounds.Y+this.FontHeight/2,Bounds.X+Bounds.Width,Bounds.Y+this.FontHeight/2);			
					g.DrawLine(penDST,Bounds.X,Bounds.Y+this.FontHeight/2+3,Bounds.X+Bounds.Width,Bounds.Y+this.FontHeight/2+3);			
					penDST.Dispose();
					break;
				}
			}
		}
			
				
		protected override void SetDataGridInColumn(System.Windows.Forms.DataGrid value)
		{
			if(value !=null)
				value.Controls.Add(cboColumn);
		}

		protected override void ConcedeFocus()
		{
			HideComboBox();		
		}

		protected override int GetMinimumHeight()
		{
			return cboColumn.PreferredHeight;
		}

		protected override int GetPreferredHeight(System.Drawing.Graphics g, object value)
		{
			return Size.Ceiling(g.MeasureString(strGetText(value), this.DataGridTableStyle.DataGrid.Font)).Height;
		}

		protected override System.Drawing.Size GetPreferredSize(System.Drawing.Graphics g, object value)
		{
			Size Extents = Size.Ceiling(g.MeasureString(strGetText(value), this.DataGridTableStyle.DataGrid.Font));
			return Extents;
		}
		#endregion		

		#region Private Funtion
		private void ComboSelectedText(object sender,EventArgs e)
		{
			if(SelectedIndexChanged != null)
				SelectedIndexChanged(this,e);

			if(blnCanSet)
			{
				SetColumnValueAtRow(objSource,intCurrentRow,cboColumn.Text);
			}
		}
		private void ComboTextChanged(object sender,EventArgs e)
		{
			if(blnCanSet)
			{
				SetColumnValueAtRow(objSource,intCurrentRow,cboColumn.Text);				
			}
		}
		private void HideComboBox()
		{
			cboColumn.Visible = false;
		}
		private void ShowComboBox(Rectangle Bounds)
		{
			if(ReadOnly)
				cboColumn.Enabled = false;

			cboColumn.Bounds = Bounds;
			cboColumn.Visible = true;
		}
		private string strGetText(object Value)
		{
			if(Value==System.DBNull.Value)
			{
				return NullText;
			}
			if(Value!=null)
			{
				return Value.ToString();
			}
			else
			{
				return string.Empty;
			}
		}
		private void PaintText(Graphics g ,Rectangle Bounds,string Text,bool AlignToRight)
		{
			Brush BackBrush = new SolidBrush(this.DataGridTableStyle.BackColor);
			Brush ForeBrush= new SolidBrush(this.DataGridTableStyle.ForeColor);
			PaintText(g, Bounds, Text, BackBrush, ForeBrush, AlignToRight);
		}
		private void PaintText(Graphics g , Rectangle TextBounds, string Text, Brush BackBrush,Brush ForeBrush,bool AlignToRight)
		{	
			try
			{
				Rectangle Rect = TextBounds;
				RectangleF RectF  = Rect; 
				StringFormat Format = new StringFormat();
				if(AlignToRight)
				{
					Format.FormatFlags = StringFormatFlags.DirectionRightToLeft;
				}
				switch(this.Alignment)
				{
					case HorizontalAlignment.Left:
						Format.Alignment = StringAlignment.Near;
						break;
					case HorizontalAlignment.Right:
						Format.Alignment = StringAlignment.Far;
						break;
					case HorizontalAlignment.Center:
						Format.Alignment = StringAlignment.Center;
						break;
				}
				Format.FormatFlags =Format.FormatFlags;
				Format.FormatFlags =StringFormatFlags.NoWrap;
				g.FillRectangle(BackBrush, Rect);
				g.DrawString(Text, this.DataGridTableStyle.DataGrid.Font, ForeBrush, RectF, Format);
				Format.Dispose();
			}
			catch
			{
				return;
			}
		}
		#endregion

		#region Public Function
		public ComboBoxStyle DropDownStyle
		{
			get
			{
				return cboColumn.DropDownStyle;
			}
			set
			{
				cboColumn.DropDownStyle = value;
			}
		}
		public void AddItem(object objItem)
		{
			cboColumn.Items.Add(objItem);
		}
		public void AddRangeItems(object [] objItems)
		{
			cboColumn.Items.AddRange(objItems);
		}
		public void ClearItem()
		{
			cboColumn.Items.Clear();
		}
		public void RemoveItem(object objItem)
		{
			cboColumn.Items.Remove(objItem);
		}
		public void RemoveItemAt(int index)
		{
			cboColumn.Items.RemoveAt(index);
		}

		public int GetItemsCount()
		{
			return cboColumn.Items.Count; 
		}
		#endregion

		#region Properties
		public int SelectedIndex
		{
			get
			{
				return cboColumn.SelectedIndex;
			}
			set
			{
				cboColumn.SelectedIndex = value;
			}
		}
		public string SelectedText
		{
			get
			{
				return cboColumn.SelectedText;
			}
			set
			{
				cboColumn.SelectedText = value;
			}
		}
		public object SelectedItem
		{
			get
			{
				return cboColumn.SelectedItem;
			}
			set
			{
				cboColumn.SelectedItem = value;
			}
		}
		#endregion

		#region Events
		public event EventHandler SelectedIndexChanged;
		#endregion

		public override bool ReadOnly
		{
			get
			{
				return m_blnReadOnly;
			}
			set
			{
				cboColumn.Enabled = !value;;
				
				m_blnReadOnly = value;
			}
		}
	}
}
