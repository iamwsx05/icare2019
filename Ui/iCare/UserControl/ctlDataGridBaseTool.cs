using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace iCare
{
	/// <summary>
	/// Summary description for ctlDataGridBaseTool.
	/// </summary>
	public class ctlDataGridBaseTool : System.Windows.Forms.DataGrid
	{
		protected System.Data.DataSet dtsGrid;
		protected System.Windows.Forms.DataGridTableStyle m_dgsBase;
		protected System.Data.DataTable m_dtbGrid;

		#region �ڲ�ʵ��ListView��Column�󶨹���
		/// <summary>
		/// �ڲ�ʵ��ListView��Column�󶨹���
		/// </summary>
		protected class clsGridListViewExt
		{		
			private DataGrid m_dtgBase;
			private DataGridTextBoxColumn m_dtxtcText;
			private DataGridTextBox m_txtText;

			private ListView m_lsvShow;			

			/// <summary>
			/// �ڼ���
			/// </summary>
			private int m_intColumnIndex;

			/// <summary>
			/// ��ǰ��Cell
			/// </summary>
			private DataGridCell m_objCurrentCell;

			public delegate void d_mthBeforeListViewShowHandler(clsGridListViewExt p_objSender,ListView p_lsvList);

			public event d_mthBeforeListViewShowHandler m_evtBeforeListViewShow;

			public delegate void d_mthListViewDoubleClickHandler(clsGridListViewExt p_objSender,ListViewItem p_lsvClickItem);

			public event d_mthListViewDoubleClickHandler m_evtListViewDoubleClick;

			/// <summary>
			/// ���캯��
			/// </summary>
			/// <param name="p_dtxtcText">��ListView�󶨵���</param>
			/// <param name="p_intColumnIndex">�е�����</param>
			/// <param name="p_lsvShow">ListView</param>
			public clsGridListViewExt(DataGridTextBoxColumn p_dtxtcText,int p_intColumnIndex,ListView p_lsvShow)
			{
				m_dtgBase = p_dtxtcText.DataGridTableStyle.DataGrid;
				m_dtxtcText = p_dtxtcText;

				m_lsvShow = p_lsvShow;				

				m_txtText = (DataGridTextBox)p_dtxtcText.TextBox;
				m_txtText.KeyDown += new KeyEventHandler(m_mthTextKeyDown);
				m_txtText.KeyPress += new KeyPressEventHandler(m_mthTextKeyPress);

				m_txtText.LostFocus += new EventHandler(m_mthLostFocus);
				m_lsvShow.LostFocus += new EventHandler(m_mthLostFocus);

				m_lsvShow.KeyDown += new KeyEventHandler(m_mthListViewKeyDown);

				m_lsvShow.DoubleClick += new EventHandler(m_mthListViewDoubleClick);
				
				m_intColumnIndex = p_intColumnIndex;
			}

			#region �¼�����
			private bool m_blnListViewShowed = false;
			/// <summary>
			/// �ж��Ƿ�ǰ��ListView�¼�
			/// </summary>
			/// <returns></returns>
			private bool m_blnIsThisListViewCall()
			{
				//����ListView�ᱻ���ʵ�����ã���Ҫ�жϵ�ǰ�����Ƿ��ڱ�ʵ������
				return m_dtgBase.CurrentCell.ColumnNumber == m_intColumnIndex && (m_blnListViewShowed || m_txtText.Focus());				
			}
			private void m_mthListViewKeyDown(object sender,KeyEventArgs e)
			{
				if(!m_blnIsThisListViewCall())
					return;

				switch(e.KeyCode)
				{
					case Keys.Space:
					case Keys.Enter:						
						m_mthListViewSelected();
						break;
					case Keys.Escape:
						m_mthHideListView();
						break;
				}
			}

			private void m_mthTextKeyPress(object sender,KeyPressEventArgs e)
			{
				if(e.KeyChar == ' ')
					e.Handled = true;
			}
			private void m_mthTextKeyDown(object sender,KeyEventArgs e)
			{
				if(!m_blnIsThisListViewCall())
					return;

				if(e.KeyCode != Keys.Space)
					return;

				if(!m_bolIfResponseSpaceBar)
				{
					if(m_strGetCurrentText() == "")
						return;
				}

				//�û����˿ո���ʾListView����ѡ���һ��
				m_mthBeforeListViewShow();
				m_mthShowListView();
				m_lsvShow.Focus();
				if(m_lsvShow.Items.Count > 0)
				{
					m_lsvShow.Items[0].Selected = true;
				}			
			}
            
			/// <summary>
			/// ����DataGird�е�DatagridTextBox�Ƿ���Ӧ�ո����ListView
			/// (Ĭ����True)
			/// </summary>
			private bool m_bolIfResponseSpaceBar = true;
			/// <summary>
			/// ����DataGird�е�DatagridTextBox�Ƿ���Ӧ�ո����ListView(Ĭ����True)
			/// </summary>
			protected bool m_BolIfResponseSpaceBar
			{
				get
				{
					return m_bolIfResponseSpaceBar;
				}
				set
				{
					m_bolIfResponseSpaceBar = value;
				}
			}
			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void m_mthLostFocus(object sender,EventArgs e)
			{
				if(!m_blnIsThisListViewCall())
					return;

				if(sender.Equals(m_lsvShow) && !m_txtText.Focus())
				{
					m_mthHideListView();
				}
				else if(sender.Equals(m_txtText) && !m_lsvShow.Focused)
				{
					m_mthHideListView();
				}
			}

			/// <summary>
			/// ListView˫��
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void m_mthListViewDoubleClick(object sender,EventArgs e)
			{
				if(!m_blnIsThisListViewCall())
					return;
				
				m_mthListViewSelected();
			}

			/// <summary>
			/// �û�ѡ������Ŀ
			/// </summary>
			private void m_mthListViewSelected()
			{
				if(!m_blnIsThisListViewCall())
					return;

				if(m_evtListViewDoubleClick != null && m_lsvShow.SelectedItems.Count > 0)
					m_evtListViewDoubleClick(this,m_lsvShow.SelectedItems[0]);

				m_mthHideListView();
			}
			#endregion

			#region ListView����ʾ������
			/// <summary>
			/// ListView��ʾ֮ǰ
			/// </summary>
			private void m_mthBeforeListViewShow()
			{
				m_objCurrentCell = m_dtgBase.CurrentCell;

				if(m_evtBeforeListViewShow!=null)
					m_evtBeforeListViewShow(this,m_lsvShow);			
			}

			/// <summary>
			/// ��ʾListView
			/// </summary>
			private void m_mthShowListView()
			{
				int x = m_txtText.Location.X + m_dtgBase.Location.X;
				int y = m_txtText.Location.Y + m_dtgBase.Location.Y + m_txtText.Height;
			
				Control ctlForm = m_dtgBase.Parent;
				while(ctlForm != null && !(ctlForm is System.Windows.Forms.Form))
				{
					x += ctlForm.Left;
					y += ctlForm.Top;
					ctlForm = ctlForm.Parent;
				}

				if(x != m_dtgBase.Location.X)
				{
					Point p = new Point(x,y);
					m_lsvShow.Location = p;
					m_lsvShow.Width = m_txtText.Width ;
					m_lsvShow.BringToFront();
					m_lsvShow.Visible = true;
					m_blnListViewShowed = true;
				}			
			}

			/// <summary>
			/// ����ListView
			/// </summary>
			private void m_mthHideListView()
			{
				/*
				 * ��ListView��TabIndex��DataGrid��һ�£��ſ�����ListView
				 * ���غ�ѡ��DataGrid��
				 */
				int intTempTabIndex = m_lsvShow.TabIndex;
				m_lsvShow.TabIndex = m_dtgBase.TabIndex;
						
				m_lsvShow.Visible = false;

				m_txtText.Focus();
				m_txtText.SelectionStart = m_txtText.Text.Length;
				m_txtText.SelectionLength = 0;			
				m_lsvShow.TabIndex = intTempTabIndex;				
			
				m_blnListViewShowed = false;
			}
			#endregion ListView����ʾ������

			/// <summary>
			/// ��ȡ�û����������
			/// </summary>
			/// <returns></returns>
			public string m_strGetCurrentText()
			{
				return m_txtText.Text;
			}

			/// <summary>
			/// ��ǰ��Cell
			/// </summary>
			/// <returns></returns>
			public DataGridCell m_objGetCurrentCell()
			{
				return m_objCurrentCell;
			}

			/// <summary>
			/// ��ǰ���߰󶨵��е�����
			/// </summary>
			/// <returns></returns>
			public int m_intGetColumnIndex()
			{
				return m_intColumnIndex;
			}
		}
		#endregion

		#region Initalize
		private void InitializeComponent()
		{
			this.dtsGrid = new System.Data.DataSet();
			this.m_dtbGrid = new System.Data.DataTable();
			this.m_dgsBase = new System.Windows.Forms.DataGridTableStyle();
			((System.ComponentModel.ISupportInitialize)(this.dtsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_dtbGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// dtsGrid
			// 
			this.dtsGrid.DataSetName = "TempData";
			this.dtsGrid.Locale = new System.Globalization.CultureInfo("zh-CN");
			this.dtsGrid.Tables.AddRange(new System.Data.DataTable[] {
																		 this.m_dtbGrid});
			// 
			// m_dtbGrid
			// 
			this.m_dtbGrid.TableName = "DataContainer";
			// 
			// m_dgsBase
			// 
			this.m_dgsBase.DataGrid = this;
			this.m_dgsBase.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.m_dgsBase.MappingName = "DataContainer";
			// 
			// ctlDataGridBaseTool
			// 
			this.DataSource = this.m_dtbGrid;
			this.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																						this.m_dgsBase});
			((System.ComponentModel.ISupportInitialize)(this.dtsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_dtbGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion Initalize
	
		public ctlDataGridBaseTool()
		{
			//
			// TODO: Add constructor logic here
			//
			InitializeComponent();

			this.TableStyles.Remove(m_dgsBase);

			m_dtbGrid.RowDeleting += new DataRowChangeEventHandler(m_mthBeforeRowDelete);
		}

		private ArrayList m_arlTempBuffer = new ArrayList();

		#region ʹ��ListView����ģ����ѯ
		protected void m_mthAddListViewColumn(ListView p_lsvList)
		{
			p_lsvList.Columns.Add("",100,HorizontalAlignment.Left);
		}
		private ListView m_lsvGridShow;
		private ArrayList m_arlGridListTools = new ArrayList();

		/// <summary>
		/// DataGrid�������ݸı�ʱ���ֵ�ListView
		/// </summary>
		public ListView m_LsvGridShow
		{
			get
			{
				return m_lsvGridShow;
			}
			set
			{
				if(m_lsvGridShow != null)
				{
					m_lsvGridShow.Visible = true;
				}

				m_lsvGridShow = value;
				if(m_lsvGridShow != null)
				{
					m_lsvGridShow.View = View.Details;					
					m_lsvGridShow.Visible = false;
					m_lsvGridShow.HeaderStyle = ColumnHeaderStyle.None;
					m_lsvGridShow.FullRowSelect = true;
				}
			}
		}	
		
		/// <summary>
		/// ���TextChange����ʾ��ListView
		/// </summary>
		/// <param name="p_intColumnIndex">�е���������0��ʼ</param>
		/// <param name="p_evtBeforeListViewShow">��ʾListView֮ǰ���¼�</param>
		/// <param name="p_evtListViewDoubleClick">ListView˫�����¼�</param>
		protected void m_mthAddColumnWithListView(int p_intColumnIndex,clsGridListViewExt.d_mthBeforeListViewShowHandler p_evtBeforeListViewShow,clsGridListViewExt.d_mthListViewDoubleClickHandler p_evtListViewDoubleClick)
		{
			if(m_lsvGridShow == null || p_evtBeforeListViewShow == null || p_evtListViewDoubleClick == null || p_intColumnIndex <0 || p_intColumnIndex > this.TableStyles[0].GridColumnStyles.Count-1)
				return;

			DataGridTextBoxColumn objColumn = this.TableStyles[0].GridColumnStyles[p_intColumnIndex] as DataGridTextBoxColumn;

			if(objColumn == null)
				return;

			clsGridListViewExt objGridListView = new clsGridListViewExt(objColumn,p_intColumnIndex,m_lsvGridShow);
			objGridListView.m_evtBeforeListViewShow += p_evtBeforeListViewShow;
			objGridListView.m_evtListViewDoubleClick += p_evtListViewDoubleClick;
			m_arlGridListTools.Add(objGridListView);
		}
		#endregion ʹ��ListView����ģ����ѯ

		#region ListViewģ����ѯ��ͳһʵ��
		/// <summary>
		/// ��ʼ��ListView����
		/// </summary>
		/// <param name="p_intColumnIndex">�е���������0��ʼ</param>
		/// <param name="p_lsvList">ListView</param>
		protected virtual void m_mthInitListViewColumnExt(int p_intColumnIndex,ListView p_lsvList)
		{
		}
		/// <summary>
		/// ��ʼ��ListView������
		/// </summary>
		/// <param name="p_intColumnIndex">�е���������0��ʼ</param>
		/// <param name="p_strText">�û����������</param>
		/// <param name="p_lsvList">ListView</param>
		protected virtual void m_mthInitListViewItemExt(int p_intColumnIndex,string p_strText,ListView p_lsvList)
		{
		}
		/// <summary>
		/// ��ListView˫���󣬻�ȡһ������
		/// </summary>
		/// <param name="p_intColumnIndex">�е���������0��ʼ</param>
		/// <param name="p_lviSelectedItem">�û�ѡ���ListView������</param>
		/// <returns>����DataTable������</returns>
		protected virtual object[] m_objMakeDataExt(int p_intColumnIndex,ListViewItem p_lviSelectedItem)
		{
			return null;
		}

		/// <summary>
		/// �������ListView�󶨡�ʹ�ô�ͳһ����������ʵ�ָ���Ext��β�ĺ�����		
		/// </summary>
		/// <param name="p_intColumnIndex">�е���������0��ʼ</param>
		protected void m_mthAddColumnWithListViewExt(int p_intColumnIndex)
		{
			m_mthAddColumnWithListView(p_intColumnIndex,new clsGridListViewExt.d_mthBeforeListViewShowHandler(m_mthBeforeShow),new clsGridListViewExt.d_mthListViewDoubleClickHandler(m_mthDoubleClick));
		}

		/// <summary>
		/// ֮ǰListView���󶨵���
		/// </summary>
		private int m_intPreColumnIndexExt = -1;
		private void m_mthBeforeShow(clsGridListViewExt p_objGridTool,ListView p_lsvList)
		{
			//���ListView����
			p_lsvList.Items.Clear();

			//�������ͬһ�У���ʼ����
			if(p_objGridTool.m_intGetColumnIndex() != m_intPreColumnIndexExt)
			{
				p_lsvList.Columns.Clear();
				m_mthInitListViewColumnExt(p_objGridTool.m_intGetColumnIndex(),p_lsvList);
				m_intPreColumnIndexExt = p_objGridTool.m_intGetColumnIndex();
			}

			//��ʼ���б�����
			m_mthInitListViewItemExt(p_objGridTool.m_intGetColumnIndex(),p_objGridTool.m_strGetCurrentText(),p_lsvList);
		}
		private void m_mthDoubleClick(clsGridListViewExt p_objGridTool,ListViewItem p_lviClickItem)
		{
			//��ȡ����
			object []objvalue = m_objMakeDataExt(p_objGridTool.m_intGetColumnIndex(),p_lviClickItem);
			
			if(objvalue == null)
				return;

			//�������ݣ�������µ����ݣ�������С�
			int intRowNum = p_objGridTool.m_objGetCurrentCell().RowNumber;

			if(intRowNum >= m_dtbGrid.Rows.Count)
			{
				m_mthAddRow(objvalue);
			}
			else
			{
				m_mthUpdateRow(intRowNum,objvalue);
			}

		}
		#endregion ͳһʵ�ֵĻ�������

		/// <summary>
		/// ��ʼ���������ڴ����Load�¼��е���
		/// </summary>
		public void m_mthInitGrid()
		{
			this.TableStyles.Add(m_dgsBase);

			m_mthAddList();			
		}
		/// <summary>
		/// �������ListView�Ĺ���
		/// </summary>
		protected virtual void m_mthAddList()
		{			
		}

		#region �ṩCellChanged����
		private DataGridCell m_dgcPreCell = new DataGridCell(-1,-1);
		protected override void OnCurrentCellChanged(System.EventArgs e)
		{
			if(m_dgcPreCell.RowNumber >= m_dtbGrid.Rows.Count)
				m_dgcPreCell.RowNumber = -1;

			if(m_dgcPreCell.ColumnNumber != -1 && m_dgcPreCell.RowNumber != -1)
			{
				m_mthHandleCellChanged(m_dgcPreCell,this.CurrentCell);
			}
			m_dgcPreCell = this.CurrentCell;
			base.OnCurrentCellChanged(e);			
		}

		protected virtual void m_mthHandleCellChanged(DataGridCell p_dgcOldCell,DataGridCell p_dgcNewCell)
		{
		}
		#endregion �ṩCellChanged����

		#region ɾ������
		private void m_mthBeforeRowDelete(object p_objSender,DataRowChangeEventArgs p_objArg)
		{
			m_mthHandleRowDelete(this.CurrentRowIndex,p_objArg.Row.ItemArray);
		}

		protected virtual void m_mthHandleRowDelete(int p_intRowIndex,object [] p_objRowData)
		{
		}
		#endregion

		#region �����ݵĻ������ܣ���ӡ���ȡ�����¡�ɾ����������м�¼��
		/// <summary>
		/// �����
		/// </summary>
		/// <param name="p_objDataArr">�е�����</param>
		protected void m_mthAddRow(object [] p_objDataArr)
		{
			m_dtbGrid.Rows.Add(p_objDataArr);
		}
		/// <summary>
		/// ��ȡ���ݵ�����
		/// </summary>
		/// <returns></returns>
		protected int m_intRowCount()
		{
			return m_dtbGrid.Rows.Count;
		}
		/// <summary>
		/// ��ȡ�е�����
		/// </summary>
		/// <param name="p_intRowIndex">�е������������������ȷ������null</param>
		/// <returns></returns>
		protected object[] m_objGetRow(int p_intRowIndex)
		{
			if(p_intRowIndex < 0 && p_intRowIndex >= m_dtbGrid.Rows.Count)
				return null;

			return m_dtbGrid.Rows[p_intRowIndex].ItemArray;
		}
		/// <summary>
		/// ��ȡȫ������
		/// </summary>
		/// <returns></returns>
		protected object[][] m_objGetRowAll()
		{
			object [][] objValueArr = new object[m_dtbGrid.Rows.Count][];

			for(int i=0;i<m_dtbGrid.Rows.Count;i++)
			{
				objValueArr[i] = m_dtbGrid.Rows[i].ItemArray;
			}

			return objValueArr;
		}
		/// <summary>
		/// ����������
		/// </summary>
		/// <param name="p_intRowIndex">�е������������������ȷ����������</param>
		/// <param name="p_objDataArr">�е�����</param>
		protected void m_mthUpdateRow(int p_intRowIndex,object [] p_objDataArr)
		{
			if(p_intRowIndex < 0 && p_intRowIndex >= m_dtbGrid.Rows.Count)
				return;

			m_dtbGrid.Rows[p_intRowIndex].ItemArray = p_objDataArr;
		}
		/// <summary>
		/// ɾ����
		/// </summary>
		/// <param name="p_intRowIndex">�е������������������ȷ������ɾ��</param>
		protected void m_mthDeleteRow(int p_intRowIndex)
		{
			if(p_intRowIndex < 0 && p_intRowIndex >= m_dtbGrid.Rows.Count)
				return;

			m_dtbGrid.Rows.RemoveAt(p_intRowIndex);			
		}
		#endregion

		#region ���빦��
		/// <summary>
		/// ���빦��
		/// </summary>
		/// <param name="p_intRowIndex">���뵽�������������ݽ��ڴ�����</param>
		/// <param name="p_objDataArr">���������</param>
		protected void m_mthInsertRow(int p_intRowIndex,object [] p_objDataArr)
		{
			/*
			 * �����㷨����ȥָ���������������ݣ���������ݣ������ԭ��������
			 */

			if(p_intRowIndex < 0 || p_objDataArr == null)
				return;

			m_arlTempBuffer.Clear();

			while(p_intRowIndex<m_dtbGrid.Rows.Count)
			{
				m_arlTempBuffer.Add(m_objGetRow(p_intRowIndex));			
				m_mthDeleteRow(p_intRowIndex);
			}			

			m_mthAddRow(p_objDataArr);

			for(int i=0;i<m_arlTempBuffer.Count;i++)
			{
				m_mthAddRow((object[])m_arlTempBuffer[i]);
			}

			m_arlTempBuffer.Clear();
		}
		#endregion ���빦��

		#region ��չ���
		/// <summary>
		/// �������Row
		/// </summary>
		protected void m_mthClearRow()
		{
			this.CurrentRowIndex = 0;

			m_dtbGrid.Rows.Clear();
		}
		#endregion

		#region �ṩʹĳһ��Cellѡ�еĹ���
		/// <summary>
		/// �ṩʹĳһ��Cellѡ�еĹ���
		/// </summary>
		/// <param name="p_intRowIndex"></param>
		/// <param name="p_intColumnIndex"></param>
		protected void m_mthSetCellSelected(int p_intRowIndex,int p_intColumnIndex)
		{
			this.CurrentCell = new DataGridCell(p_intRowIndex,p_intColumnIndex);
		}

		#endregion 
	}
}
