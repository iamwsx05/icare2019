using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.Utility.ctlTrendViewer
{
	/// <summary>
	/// Summary description for ctlTrendList.
	/// </summary>
	public class ctlTrendList : System.Windows.Forms.UserControl
	{
		#region Control Defination
		private System.Windows.Forms.Panel pnlTitle;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Panel pnlTrendList;
		private System.Windows.Forms.ListView lsvTrendList;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Constructor
		public ctlTrendList()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.lsvTrendList.Width = m_intListWidth;

			m_mthInitListView();

		}
		#endregion

		#region Member
		/// <summary>
		/// Trend Chart ��ʵ�ʿ��
		/// </summary>
		private int m_intListWidth = 954;

		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		private DateTime m_dtmStartDate = DateTime.Now;

		/// <summary>
		/// Trend Chart �ĸ���,����Ϊ5��
		/// </summary>
		private int m_intGridCount = 5;

		/// <summary>
		/// Trend Chart ÿ��Ŀ�ȣ�Ĭ��Ϊ120����
		/// </summary>
		private int m_intGridWidth = 120;

		/// <summary>
		/// �ֱ���
		/// </summary>
		private enmResolution m_enmResolution = enmResolution.Five_Minute;

		/// <summary>
		/// �ܵ�����ʱ��
		/// </summary>
		private int m_intTotalTime = 25;

		/// <summary>
		/// 
		/// </summary>
		private int m_intParameterWidth = 354;

		/// <summary>
		/// 
		/// </summary>
		private Color m_clrListBackColor = Color.White;

		#endregion

		#region Dispose
		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlTitle = new System.Windows.Forms.Panel();
			this.lblTitle = new System.Windows.Forms.Label();
			this.pnlTrendList = new System.Windows.Forms.Panel();
			this.lsvTrendList = new System.Windows.Forms.ListView();
			this.pnlTitle.SuspendLayout();
			this.pnlTrendList.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTitle
			// 
			this.pnlTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlTitle.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.lblTitle});
			this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlTitle.Name = "pnlTitle";
			this.pnlTitle.Size = new System.Drawing.Size(28, 168);
			this.pnlTitle.TabIndex = 0;
			// 
			// lblTitle
			// 
			this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblTitle.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(26, 166);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "���������б�";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlTrendList
			// 
			this.pnlTrendList.Controls.AddRange(new System.Windows.Forms.Control[] {
																					   this.lsvTrendList});
			this.pnlTrendList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTrendList.Location = new System.Drawing.Point(28, 0);
			this.pnlTrendList.Name = "pnlTrendList";
			this.pnlTrendList.Size = new System.Drawing.Size(648, 168);
			this.pnlTrendList.TabIndex = 1;
			// 
			// lsvTrendList
			// 
			this.lsvTrendList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lsvTrendList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvTrendList.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lsvTrendList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lsvTrendList.LabelWrap = false;
			this.lsvTrendList.MultiSelect = false;
			this.lsvTrendList.Name = "lsvTrendList";
			this.lsvTrendList.Size = new System.Drawing.Size(648, 168);
			this.lsvTrendList.TabIndex = 0;
			this.lsvTrendList.View = System.Windows.Forms.View.Details;
			// 
			// ctlTrendList
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pnlTrendList,
																		  this.pnlTitle});
			this.Name = "ctlTrendList";
			this.Size = new System.Drawing.Size(676, 168);
			this.pnlTitle.ResumeLayout(false);
			this.pnlTrendList.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties
		public Color m_ClrListBackColor 
		{
			get 
			{
				return m_clrListBackColor;
			}
			set
			{
				m_clrListBackColor = value;

				this.lsvTrendList.BackColor = m_clrListBackColor;
			}
		}
		/// <summary>
		///  ��ʼʱ��
		/// </summary>
		public DateTime m_DtmStartDate
		{
			get
			{
				return m_dtmStartDate;
			}
			set
			{
				m_dtmStartDate = value;
//				m_mthInitListView();

				if(m_objGroupSetArr != null)
				{
					m_mthShowParamDesc(m_objGroupSetArr);

					if(m_objTrendValueArr != null)
						m_mthShowParamValue(m_objTrendValueArr);
				}
			}
		}

		/// <summary>
		/// �ֱ���
		/// </summary>
		public enmResolution m_EnmResolution
		{
			get
			{
				return m_enmResolution;
			}
			set
			{
				m_enmResolution = value;

				m_intGridCount = (m_intTotalTime/(int)m_enmResolution) > 5 ? (m_intTotalTime/(int)m_enmResolution) : 5;

				m_intListWidth = m_intGridCount * m_intGridWidth + m_intParameterWidth;

				this.lsvTrendList.Width = m_intListWidth;

//				m_mthInitListView();

				if(m_objGroupSetArr != null)
				{
					m_mthShowParamDesc(m_objGroupSetArr);

					if(m_objTrendValueArr != null)
						m_mthShowParamValue(m_objTrendValueArr);
				}
			}
		}

		/// <summary>
		/// �ܵ�����ʱ��
		/// </summary>
		public int m_IntTotalTime
		{
			get
			{
				return m_intTotalTime;
			}
			set
			{
				m_intTotalTime = value;

				m_intGridCount = (m_intTotalTime/(int)m_enmResolution) > 5 ? (m_intTotalTime/(int)m_enmResolution) : 5;

				m_intListWidth = m_intGridCount * m_intGridWidth + m_intParameterWidth ;

				this.lsvTrendList.Width = m_intListWidth;

//				m_mthInitListView();

				if(m_objGroupSetArr != null)
				{
					m_mthShowParamDesc(m_objGroupSetArr);

					if(m_objTrendValueArr != null)
						m_mthShowParamValue(m_objTrendValueArr);
				}
			}
		}
		#endregion

		#region Private Function

		private DateTime[] m_dtmTimeArr;
		/// <summary>
		/// ��ʼ���б�
		/// </summary>
		private void m_mthInitListView()
		{
			lsvTrendList.Clear();

			ColumnHeader objHeaderParam = new ColumnHeader();
			objHeaderParam.Width = 152;
			objHeaderParam.Text = "�� ��";

			ColumnHeader objHeaderUnit = new ColumnHeader();
			objHeaderUnit.Width = 80;
			objHeaderUnit.Text = "�� λ";

			lsvTrendList.Columns.AddRange(new ColumnHeader[]{objHeaderParam,objHeaderUnit});

			//��ʱ��
			ColumnHeader[] objHeaderTimeArr = new ColumnHeader[m_intGridCount + 1];
			m_dtmTimeArr = new DateTime[m_intGridCount + 1];

			for(int i = -1; i < m_intGridCount; i++)
			{
				objHeaderTimeArr[i + 1] = new ColumnHeader();
				objHeaderTimeArr[i + 1].Width = m_intGridWidth;
				objHeaderTimeArr[i + 1].TextAlign = HorizontalAlignment.Right;

				m_dtmTimeArr[i + 1] = m_dtmStartDate.AddMinutes(((double)m_enmResolution) * (i + 1));

				objHeaderTimeArr[i + 1].Text = m_dtmTimeArr[i + 1].ToString("yy-MM-dd HH:mm");
			}
			lsvTrendList.Columns.AddRange(objHeaderTimeArr);
		}
		#endregion

		#region Public Function

		private clsVitalGroupSet[] m_objGroupSetArr;

		private clsTrendValue[] m_objTrendValueArr;

		/// <summary>
		/// ��ʾ���������ơ���λ��ͼ��
		/// </summary>
		/// <param name="p_objGroupSetArr"></param>
		public void m_mthShowParamDesc(clsVitalGroupSet[] p_objGroupSetArr)
		{
			m_mthInitListView();

			if(p_objGroupSetArr != null)
			{
				m_objGroupSetArr = p_objGroupSetArr;
				
				ListViewItem[] lsvItemArr = new ListViewItem[p_objGroupSetArr.Length];
				for(int i = 0; i < p_objGroupSetArr.Length; i++)
				{
					if(p_objGroupSetArr[i] == null)
						continue;

					string[] strItemTextArr = new String[lsvTrendList.Columns.Count];

					for(int j = 0; j < lsvTrendList.Columns.Count; j++)
					{
						if(j == 0)
							strItemTextArr[j] = p_objGroupSetArr[i].m_strParamLabel;
						else if(j == 1)
							strItemTextArr[j] = p_objGroupSetArr[i].m_strUnitDesc;
						else
							strItemTextArr[j] = "";

					}
					lsvItemArr[i] = new ListViewItem(strItemTextArr);
					lsvItemArr[i].Tag = p_objGroupSetArr[i];

					if(i%2 == 1)
						lsvItemArr[i].BackColor = Color.LightBlue;

					lsvTrendList.Items.Add(lsvItemArr[i]);
				}
			}
			
		}

		/// <summary>
		/// ��ʾ������ֵ
		/// </summary>
		/// <param name="p_objTrendValueArr">������ֵ��ʱ��Ϊ X ������, ֵΪ Y ������</param>
		public void m_mthShowParamValue(clsTrendValue[] p_objTrendValueArr)
		{
			if(m_objGroupSetArr == null)
				return; 

			m_mthShowParamDesc(m_objGroupSetArr);

			if(p_objTrendValueArr != null)
			{
				m_objTrendValueArr = p_objTrendValueArr;

				for(int i = 0; i < lsvTrendList.Items.Count; i++)
				{
					for(int j = 0; j < p_objTrendValueArr.Length; j++)
					{
						if(p_objTrendValueArr[j] != null)
						{
							if(p_objTrendValueArr[j].m_intEMFCID == ((clsVitalGroupSet)lsvTrendList.Items[i].Tag).m_intEMFCID)
							{
								//��ֵ�ӵ���Ӧ��ʱ������
								int intColumn = m_intCalPosition(p_objTrendValueArr[j].m_dtmStoreDate);

								if(intColumn > 0)
								{
									lsvTrendList.Items[i].SubItems[intColumn].Text = p_objTrendValueArr[j].m_fltValue.ToString();
								}
							}
						}
					}//end for j
				}//end for i
			}
		}
		#endregion

		/// <summary>
		/// ������ֵ�Ķ�Ӧ��ʱ����
		/// </summary>
		/// <param name="p_dtmTime"></param>
		/// <returns>���ض�Ӧ��ʱ����</returns>
		private int m_intCalPosition(DateTime p_dtmTime)
		{
			for(int i = 0; i < m_dtmTimeArr.Length; i++)
			{
				TimeSpan tmsDiff = p_dtmTime - m_dtmTimeArr[i];

				if(tmsDiff.TotalMinutes <= 0)
				{
					return i + 2;
				}
			}
			return -1;
		}

		/// <summary>
		/// ��ղ���ֵ
		/// </summary>
		public void m_mthClearParam()
		{
			m_mthShowParamValue(null);
		}
	}
}
