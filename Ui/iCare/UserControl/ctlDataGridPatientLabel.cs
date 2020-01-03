using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

namespace iCare
{
	/// <summary>
	/// Summary description for ctlDataGridPatientLabel.
	/// </summary>
	public class ctlDataGridPatientLabel : ctlDataGridBaseTool
	{
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private System.Data.DataColumn dataColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dgcName;
		private System.Windows.Forms.DataGridTextBoxColumn dgcAge;
		private System.Windows.Forms.DataGridTextBoxColumn dgcSex;
		private System.Windows.Forms.DataGridTextBoxColumn dgcBed;
		private System.Windows.Forms.DataGridTextBoxColumn dgcInPatientID;
		private System.Windows.Forms.DataGridTextBoxColumn dgcDiagnose;
		private System.Windows.Forms.DataGridTextBoxColumn dgcQuantity;
		private System.Windows.Forms.DataGridTextBoxColumn dgcArea;
		private System.Data.DataColumn dataColumn4;
		private System.Data.DataColumn dataColumn5;
		private System.Data.DataColumn dataColumn6;
		private System.Data.DataColumn dataColumn7;
		private System.Data.DataColumn dataColumn8;
		/// <summary>
		/// Required designer variable.
		/// </summary>
//		private System.ComponentModel.Container components = null;

		public ctlDataGridPatientLabel(System.ComponentModel.IContainer container)
		{
			/// <summary>
			/// Required for Windows.Forms Class Composition Designer support
			/// </summary>
			container.Add(this);
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public ctlDataGridPatientLabel()
		{
			/// <summary>
			/// Required for Windows.Forms Class Composition Designer support
			/// </summary>
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dataColumn1 = new System.Data.DataColumn();
			this.dataColumn2 = new System.Data.DataColumn();
			this.dataColumn3 = new System.Data.DataColumn();
			this.dgcName = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dgcAge = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dgcSex = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dgcBed = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dgcInPatientID = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dgcDiagnose = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dgcQuantity = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dgcArea = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataColumn4 = new System.Data.DataColumn();
			this.dataColumn5 = new System.Data.DataColumn();
			this.dataColumn6 = new System.Data.DataColumn();
			this.dataColumn7 = new System.Data.DataColumn();
			this.dataColumn8 = new System.Data.DataColumn();
			((System.ComponentModel.ISupportInitialize)(this.dtsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_dtbGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// m_dgsBase
			// 
			this.m_dgsBase.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																										this.dgcName,
																										this.dgcAge,
																										this.dgcSex,
																										this.dgcArea,
																										this.dgcBed,
																										this.dgcInPatientID,
																										this.dgcDiagnose,
																										this.dgcQuantity});
			// 
			// m_dtbGrid
			// 
			this.m_dtbGrid.Columns.AddRange(new System.Data.DataColumn[] {
																			 this.dataColumn1,
																			 this.dataColumn2,
																			 this.dataColumn3,
																			 this.dataColumn4,
																			 this.dataColumn5,
																			 this.dataColumn6,
																			 this.dataColumn7,
																			 this.dataColumn8});
			// 
			// dataColumn1
			// 
			this.dataColumn1.Caption = "Column1";
			this.dataColumn1.ColumnName = "����";
			// 
			// dataColumn2
			// 
			this.dataColumn2.Caption = "Column2";
			this.dataColumn2.ColumnName = "����";
			// 
			// dataColumn3
			// 
			this.dataColumn3.Caption = "Column3";
			this.dataColumn3.ColumnName = "�Ա�";
			// 
			// dgcName
			// 
			this.dgcName.Format = "";
			this.dgcName.FormatInfo = null;
			this.dgcName.HeaderText = "����";
			this.dgcName.MappingName = "����";
			this.dgcName.NullText = "";
			this.dgcName.Width = 75;
			// 
			// dgcAge
			// 
			this.dgcAge.Format = "";
			this.dgcAge.FormatInfo = null;
			this.dgcAge.HeaderText = "����";
			this.dgcAge.MappingName = "����";
			this.dgcAge.NullText = "";
			this.dgcAge.Width = 45;
			// 
			// dgcSex
			// 
			this.dgcSex.Format = "";
			this.dgcSex.FormatInfo = null;
			this.dgcSex.HeaderText = "�Ա�";
			this.dgcSex.MappingName = "�Ա�";
			this.dgcSex.NullText = "";
			this.dgcSex.Width = 45;
			// 
			// dgcBed
			// 
			this.dgcBed.Format = "";
			this.dgcBed.FormatInfo = null;
			this.dgcBed.HeaderText = "����";
			this.dgcBed.MappingName = "����";
			this.dgcBed.NullText = "";
			this.dgcBed.Width = 45;
			// 
			// dgcInPatientID
			// 
			this.dgcInPatientID.Format = "";
			this.dgcInPatientID.FormatInfo = null;
			this.dgcInPatientID.HeaderText = "סԺ��";
			this.dgcInPatientID.MappingName = "סԺ��";
			this.dgcInPatientID.NullText = "";
			this.dgcInPatientID.Width = 75;
			// 
			// dgcDiagnose
			// 
			this.dgcDiagnose.Format = "";
			this.dgcDiagnose.FormatInfo = null;
			this.dgcDiagnose.HeaderText = "���";
			this.dgcDiagnose.MappingName = "���";
			this.dgcDiagnose.NullText = "";
			this.dgcDiagnose.Width = 265;
			// 
			// dgcQuantity
			// 
			this.dgcQuantity.Format = "";
			this.dgcQuantity.FormatInfo = null;
			this.dgcQuantity.HeaderText = "��ӡ����";
			this.dgcQuantity.MappingName = "��ӡ����";
			this.dgcQuantity.NullText = "";
			this.dgcQuantity.Width = 75;
			// 
			// dgcArea
			// 
			this.dgcArea.Format = "";
			this.dgcArea.FormatInfo = null;
			this.dgcArea.HeaderText = "����";
			this.dgcArea.MappingName = "����";
			this.dgcArea.NullText = "";
			this.dgcArea.Width = 130;
			// 
			// dataColumn4
			// 
			this.dataColumn4.ColumnName = "����";
			// 
			// dataColumn5
			// 
			this.dataColumn5.ColumnName = "����";
			// 
			// dataColumn6
			// 
			this.dataColumn6.ColumnName = "סԺ��";
			// 
			// dataColumn7
			// 
			this.dataColumn7.ColumnName = "���";
			// 
			// dataColumn8
			// 
			this.dataColumn8.ColumnName = "��ӡ����";
			((System.ComponentModel.ISupportInitialize)(this.dtsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_dtbGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion

		private enum enmColumn
		{
			Name = 0,
			Bed = 4,
			InPatientID = 5,

		}

		protected override void m_mthInitListViewColumnExt(int p_intColumnIndex, System.Windows.Forms.ListView p_lsvList)
		{
			switch((enmColumn)p_intColumnIndex)
			{
				case enmColumn.Name:
					m_mthAddListViewColumn(p_lsvList);
					break;
				case enmColumn.Bed:
					m_mthAddListViewColumn(p_lsvList);
					break;
				case enmColumn.InPatientID:
					m_mthAddListViewColumn(p_lsvList);
					break;
			}
		}

		protected override void m_mthInitListViewItemExt(int p_intColumnIndex, string p_strText, System.Windows.Forms.ListView p_lsvList)
		{	
			clsPatient [] objPatientArr;
			string strAreaID="";
			foreach(Control ctlSub in this.Parent.Controls)
			{
				if(ctlSub.Name=="m_cboArea")
				{
					strAreaID = ((clsInPatientArea)((com.digitalwave.Utility.Controls.ctlComboBox)ctlSub).SelectedItem).m_StrAreaID;
				}
			}
			switch((enmColumn)p_intColumnIndex)
			{
				case enmColumn.Name:
					this.Cursor = Cursors.WaitCursor;
					//ע��:new frmPatientLabel().m_objGetPatientByBedNO����ִ���乹�캯������Ĵ���
					objPatientArr = clsSystemContext.s_ObjCurrentContext.m_ObjPatientManager.m_objGetPatientByLikePatientName_InArea(strAreaID,p_strText);
					if(objPatientArr == null || objPatientArr.Length==0)
					{
						clsPublicFunction.ShowInformationMessageBox("�Բ���û�д˲��ˣ�");
						return;
					}
					for(int i=0;i<objPatientArr.Length;i++)
					{
						ListViewItem lviPatient = new ListViewItem(
							new string[]{
											objPatientArr[i].m_StrName,
						});
						lviPatient.Tag = objPatientArr[i];

						p_lsvList.Items.Add(lviPatient);
					}
					this.Cursor = Cursors.Default;
					break;
				case enmColumn.Bed:
					this.Cursor = Cursors.WaitCursor;
					objPatientArr = clsSystemContext.s_ObjCurrentContext.m_ObjPatientManager.m_objGetPatientByLikeBedNO_InArea(strAreaID,p_strText);
					if(objPatientArr == null || objPatientArr.Length==0)
					{						
						clsPublicFunction.ShowInformationMessageBox("�Բ���û�д˲��ˣ�");
						return;
					}
					for(int i=0;i<objPatientArr.Length;i++)
					{
						ListViewItem lviPatient = new ListViewItem(
							new string[]{
											objPatientArr[i].m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName
										});
						lviPatient.Tag = objPatientArr[i];

						p_lsvList.Items.Add(lviPatient);
					}	
					this.Cursor = Cursors.Default;
					break;
				case enmColumn.InPatientID:
					this.Cursor = Cursors.WaitCursor;
					objPatientArr = clsSystemContext.s_ObjCurrentContext.m_ObjPatientManager.m_objGetInPatientByAreaIDLike(strAreaID,p_strText);
					if(objPatientArr == null || objPatientArr.Length==0)
					{
						clsPublicFunction.ShowInformationMessageBox("�Բ���û�д˲��ˣ�");
						return;
					}			

					for(int i=0;i<objPatientArr.Length;i++)
					{
						ListViewItem lviPatient = new ListViewItem(
							new string[]{
											objPatientArr[i].m_StrInPatientID,
						});
						lviPatient.Tag = objPatientArr[i];

						p_lsvList.Items.Add(lviPatient);
					}
					this.Cursor = Cursors.Default;
					break;
			}
		}

		protected override object[] m_objMakeDataExt(int p_intColumnIndex, System.Windows.Forms.ListViewItem p_lviSelectedItem)
		{
			object [] objValue = m_objGetRow(this.CurrentCell.RowNumber);
			objValue[0] = ((clsPatient)p_lviSelectedItem.Tag).m_StrName;
			objValue[1] = ((clsPatient)p_lviSelectedItem.Tag).m_ObjPeopleInfo.m_StrAge;
			objValue[2] = ((clsPatient)p_lviSelectedItem.Tag).m_StrSex.Trim() != "" ? ((clsPatient)p_lviSelectedItem.Tag).m_StrSex : "����";
			objValue[3] = ((clsPatient)p_lviSelectedItem.Tag).m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaName;
			objValue[4] = ((clsPatient)p_lviSelectedItem.Tag).m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName;
			objValue[5] = ((clsPatient)p_lviSelectedItem.Tag).m_StrInPatientID;
			objValue[6] = ((clsPatient)p_lviSelectedItem.Tag).m_StrDiagnose;

			return objValue;
		}

		/// <summary>
		/// �����
		/// </summary>
		/// <param name="p_objDataArr">�е�����</param>
		public void m_mthAddRow(object [] p_objDataArr)
		{
			m_dtbGrid.Rows.Add(p_objDataArr);
		}

		/// <summary>
		/// �����
		/// </summary>
		public void m_mthClear()
		{
			m_dtbGrid.Rows.Clear();
		}

		protected override void m_mthAddList()
		{
			m_mthAddColumnWithListViewExt((int)enmColumn.Name);
			m_mthAddColumnWithListViewExt((int)enmColumn.Bed);
			m_mthAddColumnWithListViewExt((int)enmColumn.InPatientID);
		}	

		private ArrayList m_arlTemp = new ArrayList();
		public clsPatientLabel[] m_objGetPatientLabel()
		{
			object [][] objValueArr = m_objGetRowAll();

			int intCount;

			m_arlTemp.Clear();
			for(int i=0;i<objValueArr.Length;i++)
			{				
				if(objValueArr[i][7]!=null && objValueArr[i][7].ToString().Trim()!="")
				{
					try
					{
						intCount = int.Parse(objValueArr[i][7].ToString());
					}
					catch
					{
						clsPublicFunction.ShowInformationMessageBox("��ӡ����ӦΪ����!");
						intCount = 0;
					}
					for(int j=0;j<intCount;j++)
					{
						clsPatientLabel objPatientLabel = new clsPatientLabel();
						objPatientLabel.strName=objValueArr[i][0].ToString().Trim();
						objPatientLabel.strAge=objValueArr[i][1].ToString().Trim();
						objPatientLabel.strSex=objValueArr[i][2].ToString().Trim();
						objPatientLabel.strArea=objValueArr[i][3].ToString().Trim();
						objPatientLabel.strBed=objValueArr[i][4].ToString().Trim();
						objPatientLabel.strInPatientID=objValueArr[i][5].ToString().Trim();
						objPatientLabel.strDiagnose=objValueArr[i][6].ToString().Trim();

						m_arlTemp.Add(objPatientLabel);
					}
				}				
			}

			return (clsPatientLabel[])m_arlTemp.ToArray(typeof(clsPatientLabel));

		}
	}
	public class clsPatientLabel
	{
		public string strName,strAge,strSex,strArea,strBed,strInPatientID,strDiagnose;			
	}
}
