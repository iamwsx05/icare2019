using System;
using System.Windows.Forms;
using System.Drawing;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// PocketDataGrid���书��DataGrid
	/// </summary>
	public delegate void _EnterDown(int i);

	public class PocketDataGrid:DataGrid
	{
		public event _EnterDown EnterDown;
		DateTime gridMouseDownTime;
		public PocketDataGrid()
		{
			AddCustomDataTableStyle();
			this.CaptionVisible=false;
			this.Dock=System.Windows.Forms.DockStyle.Fill;
			this.ReadOnly=true;
			this.CurrentCellChanged+=new EventHandler(m_mthCurrentCellChanged);
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
					
		}
		#region ������
		private void AddCustomDataTableStyle()
		{
			DataGridTableStyle stl = new DataGridTableStyle();
			stl.MappingName = "dt";
			// ��������
//			stl.AlternatingBackColor=Color.Gray;
			stl.AllowSorting=false;
			stl.ReadOnly=true;
			stl.AllowSorting=false;
			// ���Textbox����ʽ���Ա����ǲ�׽����¼�
			DataGridTextBoxColumn TextCol = new DataGridTextBoxColumn();
			TextCol.MappingName = "ITEMCODE_VCHR";
			TextCol.HeaderText = "��Ŀ����";
			TextCol.Width = 100;

			//����¼�������
			TextCol.TextBox.MouseDown += new MouseEventHandler(m_mthMouseDown);
			TextCol.TextBox.DoubleClick += new EventHandler(m_mthDoubleClick);
			stl.GridColumnStyles.Add(TextCol);

			TextCol = new DataGridTextBoxColumn();
			TextCol.MappingName = "ITEMNAME_VCHR";
			TextCol.HeaderText = "��Ŀ����";
			TextCol.Width = 150;
			//����¼�������
			TextCol.TextBox.MouseDown += new MouseEventHandler(m_mthMouseDown);
			TextCol.TextBox.DoubleClick += new EventHandler(m_mthDoubleClick);
			stl.GridColumnStyles.Add(TextCol);

			TextCol = new DataGridTextBoxColumn();
			TextCol.MappingName = "ITEMSPEC_VCHR";
			TextCol.HeaderText = "��Ŀ���";
			TextCol.Width = 100;
			//����¼�������
			TextCol.TextBox.MouseDown += new MouseEventHandler(m_mthMouseDown);
			TextCol.TextBox.DoubleClick += new EventHandler(m_mthDoubleClick);
			stl.GridColumnStyles.Add(TextCol);
		

			TextCol = new DataGridTextBoxColumn();
			TextCol.MappingName = "ITEMPRICE_MNY";
			TextCol.HeaderText = "��Ŀ�۸�";
			TextCol.Width = 100;
			//����¼�������
			TextCol.TextBox.MouseDown += new MouseEventHandler(m_mthMouseDown);
			TextCol.TextBox.DoubleClick += new EventHandler(m_mthDoubleClick);
			stl.GridColumnStyles.Add(TextCol);


			TextCol = new DataGridTextBoxColumn();
			TextCol.MappingName = "ITEMUNIT_CHR";
			TextCol.HeaderText = "��Ŀ��λ";
			TextCol.Width = 100;
			//����¼�������
			TextCol.TextBox.MouseDown += new MouseEventHandler(m_mthMouseDown);
			TextCol.TextBox.DoubleClick += new EventHandler(m_mthDoubleClick);
			stl.GridColumnStyles.Add(TextCol);

			TextCol = new DataGridTextBoxColumn();
			TextCol.MappingName = "DOSAGE_DEC";
			TextCol.HeaderText = "��������";
			TextCol.Width = 100;
			//����¼�������
			TextCol.TextBox.MouseDown += new MouseEventHandler(m_mthMouseDown);
			TextCol.TextBox.DoubleClick += new EventHandler(m_mthDoubleClick);
			stl.GridColumnStyles.Add(TextCol);

			TextCol = new DataGridTextBoxColumn();
			TextCol.MappingName = "DOSAGEUNIT_CHR";
			TextCol.HeaderText = "������λ";
			TextCol.Width = 100;
			//����¼�������
			TextCol.TextBox.MouseDown += new MouseEventHandler(m_mthMouseDown);
			TextCol.TextBox.DoubleClick += new EventHandler(m_mthDoubleClick);
			stl.GridColumnStyles.Add(TextCol);


			TextCol = new DataGridTextBoxColumn();
			TextCol.MappingName = "ITEMOPCODE_CHR";
			TextCol.HeaderText = "�����շѱ���";
			TextCol.Width = 100;
			//����¼�������
			TextCol.TextBox.MouseDown += new MouseEventHandler(m_mthMouseDown);
			TextCol.TextBox.DoubleClick += new EventHandler(m_mthDoubleClick);
			stl.GridColumnStyles.Add(TextCol);

			this.TableStyles.Add(stl);
			
		}
	
		#endregion
		protected override void OnMouseDown(MouseEventArgs e)
		{
			gridMouseDownTime = DateTime.Now;
			if(CurrentRowIndex>-1)
			{
				this.Select(this.CurrentRowIndex);
			}
			base.OnMouseDown (e);
		}
	
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			this.Select(this.CurrentRowIndex);
			// TODO:  ��� Class2.ProcessCmdKey ʵ��
			if((msg.Msg==256) && (msg.WParam.ToInt32() == (int) Keys.Enter)) 
			{
				if(EnterDown!=null)
					EnterDown(this.CurrentRowIndex);
				return true;
			}
			else
			{
				return base.ProcessCmdKey (ref msg, keyData);
			}
		}
		private void m_mthCurrentCellChanged(object sender, System.EventArgs e)
		{
			if(this.CurrentRowIndex>-1)
			{
				this.Select(this.CurrentRowIndex);
			}
		}
		private void m_mthMouseDown(object sender,MouseEventArgs e)
		{
			if(DateTime.Now < gridMouseDownTime.AddMilliseconds(SystemInformation.DoubleClickTime))
			{
				if(this.CurrentRowIndex>-1)
				{
					if(EnterDown!=null)
					{
						EnterDown(this.CurrentRowIndex);
					}
				}
			}
		}
		private void m_mthDoubleClick(object sender,System.EventArgs e)
		{
			if(this.CurrentRowIndex>-1)
			{
				if(EnterDown!=null)
				{
					EnterDown(this.CurrentRowIndex);
				}
			}


		}
		
	}
}
