using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Data;

namespace com.digitalwave.Utility.Controls
{
	public class clsDSTRichTextBoxValue
	{
		public string m_strText;

		public string m_strDSTXml;

		public bool m_blnUnderDST = false;

        public Color m_clrTextColor = Color.Black;
	}

	/// <summary>
	/// Summary description for cltDataGridDSTRichTextBox.
	/// </summary>
	public class cltDataGridDSTRichTextBox : DataGridColumnStyle
	{
		private ctlRichTextBox m_rtbBase;

		private CurrencyManager objSource;

		private clsDSTRichTextBoxValue m_objCurrentValue;

		private int m_intCurrentRow;

		private bool m_blnIsInformEditing;

		private bool m_blnCanTextChanged;

		private clsPrintRichTextContext m_objPrint;
		
		//		private bool m_blnReadOnly;

		private DataGrid m_dtgBase;

		public cltDataGridDSTRichTextBox()
		{
			m_rtbBase = new ctlRichTextBox();
			m_RtbBase.BorderStyle = BorderStyle.None;
			m_rtbBase.Visible = false;
			m_rtbBase.TextChanged += new EventHandler(m_mthTextChanged);

			m_blnIsInformEditing = false;
			m_blnCanTextChanged = true;

			m_objPrint = new clsPrintRichTextContext(Color.Black,this.Font);
		}

		/// <summary>
		/// 全局控制，标记双划线是否下双划线
		/// </summary>
		private bool m_blnUnderLineDST = false;

		/// <summary>
		/// 全局控制双划线是否下双划线
		/// </summary>
		public bool m_BlnUnderLineDST
		{
			get
			{
				return m_rtbBase.m_BlnUnderLineDST;
			}
			set
			{
				m_blnUnderLineDST = value;
				m_rtbBase.m_BlnUnderLineDST = value;
				m_objPrint.m_BlnUnderLineDST = value;				
			}
		}

		/// <summary>
		/// 是否使用全局控制，如果为true，双划线的设置以m_BlnUnderLineDST为准，否则根据不同的值作准。
		/// </summary>
		private bool m_blnGobleSet = true;

		[Browsable(false)]
		/// <summary>
		/// 全局控制双划线是否下双划线
		/// </summary>
		public bool m_BlnGobleSet
		{
			get
			{
				return m_blnGobleSet;
			}
			set
			{
				m_blnGobleSet = value;				
			}
		}

		#region Override Method
		protected override void Abort(int rowNum)
		{
		}
        
		protected override bool Commit(System.Windows.Forms.CurrencyManager dataSource, int rowNum)
		{	
			if(rowNum != m_intCurrentRow || !m_rtbBase.Visible || m_dtgBase == null)
				return true;

			clsDSTRichTextBoxValue objValue = m_objGetValue(GetColumnValueAtRow(dataSource,rowNum));

			if(objValue == null)
			{
				objValue = new clsDSTRichTextBoxValue();		
				objValue.m_strDSTXml = "";
				objValue.m_strText = "";
			}

            if (rowNum < ((DataTable)m_dtgBase.DataSource).Rows.Count 
                && rowNum != ((DataTable)m_dtgBase.DataSource).Rows.Count - 1)//DataGrid默认会追加一行，该行为空，应排除
			{
				objValue.m_strText = m_rtbBase.Text;
				objValue.m_strDSTXml = m_strReplaceBlackToWhite(m_rtbBase.m_strGetXmlText());
			}

			SetColumnValueAtRow(dataSource,rowNum,objValue);				

			m_mthHideRichTextBox();
			return true;
		}

		protected override void Edit(System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{	
//			m_blnReadOnly = readOnly;

//			if(readOnly)
//				return;

			m_objCurrentValue = m_objGetValue(GetColumnValueAtRow(source,rowNum));

			m_blnCanTextChanged = false;
			if(m_objCurrentValue != null)
			{
				m_rtbBase.m_mthSetNewText(m_objCurrentValue.m_strText,m_strReplaceWhiteToBlack(m_objCurrentValue.m_strDSTXml));

				if(!m_blnGobleSet)
				{
					m_rtbBase.m_BlnUnderLineDST = m_objCurrentValue.m_blnUnderDST;
					m_objPrint.m_BlnUnderLineDST = m_objCurrentValue.m_blnUnderDST;
				}
			}
			else
			{
				m_rtbBase.Text = "";
			}
			m_blnCanTextChanged = true;

			if(cellIsVisible)
			{
				objSource = source;
				
				m_mthShowRichTextBox(bounds,readOnly);	
			
				m_blnIsInformEditing = false;
			}
			else
			{
				m_mthHideRichTextBox();
			}		

			m_intCurrentRow = rowNum;
		}

		protected override void Paint(Graphics g,Rectangle Bounds,CurrencyManager Source,int RowNum)
		{
			Paint(g, Bounds, Source, RowNum, false);
		}
		protected override void Paint(Graphics g,Rectangle Bounds,CurrencyManager Source,int RowNum,bool AlignToRight)
		{
			Brush bruBackBrush = new SolidBrush(this.DataGridTableStyle.BackColor);
			Brush bruForeBrush= new SolidBrush(this.DataGridTableStyle.ForeColor);

			m_mthPaintText(g, Bounds, m_objGetValue(GetColumnValueAtRow(Source, RowNum)), bruBackBrush, bruForeBrush, AlignToRight);

            bruBackBrush.Dispose();
			bruForeBrush.Dispose();
		}
		protected override void Paint(Graphics g,Rectangle Bounds,CurrencyManager Source,int RowNum,Brush BackBrush ,Brush ForeBrush ,bool AlignToRight)
        {
			m_mthPaintText(g, Bounds, m_objGetValue(GetColumnValueAtRow(Source, RowNum)), BackBrush, ForeBrush, AlignToRight);
		}			
				
		protected override void SetDataGridInColumn(System.Windows.Forms.DataGrid value)
		{
			if(value !=null)
			{
				value.Controls.Add(m_rtbBase);
				m_rtbBase.ContextMenu = value.ContextMenu;
			}

			m_dtgBase = value;
		}

		protected override void ConcedeFocus()
		{			
			m_mthHideRichTextBox();
		}

		protected override int GetMinimumHeight()
		{
			return m_rtbBase.PreferredHeight;
		}

		protected override int GetPreferredHeight(System.Drawing.Graphics g, object value)
		{
//			return Size.Ceiling(g.MeasureString(m_objGetValue(value).m_strText, this.DataGridTableStyle.DataGrid.Font)).Height;;
			return m_rtbBase.PreferredHeight;
		}

		protected override System.Drawing.Size GetPreferredSize(System.Drawing.Graphics g, object value)
		{
			if(m_objGetValue(value) == null)
				return Size.Empty;
			else
				return Size.Ceiling(g.MeasureString(m_objGetValue(value).m_strText, this.DataGridTableStyle.DataGrid.Font));			
		}
		#endregion		

		#region Private Funtion
		private string m_strReplaceWhiteToBlack(string p_strXML)
		{//-1:白色，-16777216黑色
			if(p_strXML==null)
				return null;
			return p_strXML.Replace("C=\"-1\"","C=\"-16777216\"");
		}
		private string m_strReplaceBlackToWhite(string p_strXML)
		{//-1:白色，-16777216黑色
			if(p_strXML==null)
				return null;
			return p_strXML.Replace("C=\"-16777216\"","C=\"-1\"");
		}

		private void m_mthHideRichTextBox()
		{
			m_rtbBase.Visible = false;
		}

		private void m_mthShowRichTextBox(Rectangle Bounds,bool p_blnReadOnly)
		{
			m_rtbBase.m_BlnReadOnly = p_blnReadOnly;

			if(p_blnReadOnly)
			{
				m_rtbBase.BackColor = Color.LightGray;
			}
			else
			{
				m_rtbBase.BackColor = Color.White;
			}

			m_rtbBase.Bounds = Bounds;
			m_rtbBase.Visible = true;

			m_rtbBase.Focus();
		}

		private clsDSTRichTextBoxValue m_objGetValue(object p_objValue)
		{
			if(p_objValue==System.DBNull.Value)
			{
				return null;
			}

			return (clsDSTRichTextBoxValue)p_objValue;
		}

		private void m_mthPaintText(Graphics p_objGrp , Rectangle p_rtgTextBounds, clsDSTRichTextBoxValue p_objValue, Brush p_bruBackBrush,Brush p_bruForeBrush,bool p_blnAlignToRight)
		{
			try
			{
				StringFormat Format = new StringFormat();
				if(p_blnAlignToRight)
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
//				Format.FormatFlags =Format.FormatFlags;
				Format.FormatFlags =StringFormatFlags.NoWrap;
				p_objGrp.FillRectangle(p_bruBackBrush, p_rtgTextBounds);
//				p_objGrp.DrawImage(p_objValue.m_imgView,p_rtgTextBounds);
								
//				SizeF szfChar = p_objGrp.MeasureString("ab",this.Font);
//				SizeF szfCChar = p_objGrp.MeasureString("测试",this.Font);

//				int intCharPerLine = (int)((float)p_rtgTextBounds.Width/(szfString.Width/2));
				if(p_objValue == null || p_objValue.m_strText == null || p_objValue.m_strText == "")
					return;

				if(!m_blnGobleSet)
				{
					m_objPrint.m_BlnUnderLineDST = p_objValue.m_blnUnderDST;
				}
                m_objPrint.m_mthSetContextWithCorrectBefore(p_objValue.m_strText, m_strReplaceWhiteToBlack(p_objValue.m_strDSTXml), new DateTime(1900, 1, 1), p_objValue.m_clrTextColor);
				m_objPrint.m_FntCharFont = this.Font;

				int intX = p_rtgTextBounds.X+1;
				int intY = p_rtgTextBounds.Y+1;

				int intLineHeight = this.FontHeight;

				while(m_objPrint.m_BlnHaveNextLine())
				{
					m_objPrint.m_mthPrintLine(p_rtgTextBounds.Width,intX,intY,p_objGrp);

					intY += intLineHeight;

					if(intY > p_rtgTextBounds.Y+p_rtgTextBounds.Height)
						break;
				}

				Format.Dispose();
			}
			catch
			{
				
			}
		}
		#endregion

		private void m_mthTextChanged(object p_objSender,EventArgs p_objArg)
		{
			if(m_blnCanTextChanged && !m_blnIsInformEditing)
			{
				string strText = m_rtbBase.Text;
				this.ColumnStartedEditing(m_rtbBase);	
				m_rtbBase.Text = strText;
				m_rtbBase.SelectionStart = strText.Length;
				m_rtbBase.Focus();
				m_blnIsInformEditing = true;
			}
		}

		public System.Drawing.Font Font
		{
			get
			{
				return m_rtbBase.Font;
			}
			set
			{
				m_rtbBase.Font = value;
			}
		}

		public ctlRichTextBox m_RtbBase
		{
			get
			{
				return m_rtbBase;
			}
		}
	}
}
