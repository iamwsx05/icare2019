using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedAddStorageMene ��ժҪ˵����
	/// </summary>
	public class clsControlMedAddStorageMana:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedAddStorageMana()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objDoMain2=new clsDomainConrol_Medicne();
		}
		clsDomainConrol_Medicne m_objDoMain2=null;

		#region ����
		clsDomainConrol_Medicne m_objDoMain=new clsDomainConrol_Medicne();
		/// <summary>
		/// ����ҩƷ����
		/// </summary>
		private DataTable medTypebt=new DataTable();
		/// <summary>
		/// ����ҩƷ����
		/// </summary>
		DataTable btMed=null;
		/// <summary>
		/// �������ҩƷ����
		/// </summary>
		DataTable btMedFind=null;
		/// <summary>
		/// ����ֿ���Ϣ
		/// </summary>
		DataTable btStorage=null;
		/// <summary>
		/// ����ֿ�ҩƷ
		/// </summary>
		DataTable tbStorage=new DataTable();

		#endregion

		#region ���ô������
		/// <summary>
		/// �������
		/// </summary>
		frmMedAndStorageMana m_objViewer;
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			m_objViewer = (frmMedAndStorageMana)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ʼ������
		public void m_lngFrmLoad()
		{
			m_objDoMain2.m_lngGetMedType(out medTypebt);
			this.m_objViewer.m_cmbMedType.Items.Clear();
			for(int i1=0;i1<medTypebt.Rows.Count;i1++)
			{
				this.m_objViewer.m_cmbMedType.Items.Add(medTypebt.Rows[i1]["MEDICINETYPENAME_VCHR"].ToString().Trim());
			}
			this.m_objViewer.m_cmbMedType.Items.Insert(0,"����");
			this.m_objViewer.m_cmbMedType.SelectedIndex = 0;

			long lngRes=m_objDoMain.m_lngGetAllMed(out btMed,out btStorage);
			if(lngRes==1)
			{
				this.m_objViewer.ctlDgMed.m_mthSetDataTable(btMed);
				this.m_objViewer.ctlDgMed.Tag="btMed";
				this.m_objViewer.m_cboStorage.Items.Clear();
				for(int i1=0;i1<btStorage.Rows.Count;i1++)
				{
					this.m_objViewer.m_cboStorage.Items.Add(btStorage.Rows[i1]["STORAGENAME_VCHR"].ToString ());
				}
				this.m_objViewer.m_cboStorage.SelectedIndex=0;
			}
			else
			{
			   MessageBox.Show("��ȡ��ʼ�����ݳ�����رմ��������ԣ�","ϵͳ������ʾ");
				return;
			}
		}
		#endregion

		#region ����ʾҩ���Ѿ����ڵ�ҩƷ

		public void m_mthNotShowAll()
		{
			if((string)this.m_objViewer.ctlDgMed.Tag=="btMed"&&tbStorage.Rows.Count>0&&btMed.Rows.Count>0)
			{
				for(int i1=0;i1<tbStorage.Rows.Count;i1++)
				{
					for(int f2=0;f2<btMed.Rows.Count;f2++)
					{
						if(tbStorage.Rows[i1][1].ToString().Trim()==btMed.Rows[f2][0].ToString().Trim())
						{
							btMed.Rows[f2].Delete();
							btMed.AcceptChanges();
							f2--;
							if(i1==tbStorage.Rows.Count-1)
							{
								return;
							}
						}
					}					
				}
				
			}
			if((string)this.m_objViewer.ctlDgMed.Tag=="btMedFind"&&tbStorage.Rows.Count>0&&btMedFind.Rows.Count>0)
			{
				for(int i1=0;i1<tbStorage.Rows.Count;i1++)
				{
					for(int f2=0;f2<btMedFind.Rows.Count;f2++)
					{
						if(tbStorage.Rows[i1][1].ToString().Trim()==btMedFind.Rows[f2][0].ToString().Trim())
						{
							btMedFind.Rows[f2].Delete();
							btMedFind.AcceptChanges();
							f2--;
							if(i1==tbStorage.Rows.Count-1)
							{
								return;
							}
						}
					}					
				}
				
			}

		}
		#endregion


		#region ��ʾҩ���Ѿ����ڵ�ҩƷ

		public void m_mthShowAll()
		{
			if((string)this.m_objViewer.ctlDgMed.Tag=="btMed"&&tbStorage.Rows.Count>0)
			{
				for(int i1=tbStorage.Rows.Count-1;i1>=0;i1--)
				{
					DataRow  newRow=btMed.NewRow();
					newRow["MEDICINEID_CHR"]=	tbStorage.Rows[i1]["MEDICINEID_CHR"];
					newRow["ASSISTCODE_CHR"]=	tbStorage.Rows[i1]["ASSISTCODE_CHR"];
					newRow["MEDICINENAME_VCHR"]=	tbStorage.Rows[i1]["MEDICINENAME_VCHR"];
					newRow["MEDICINETYPENAME_VCHR"]=	tbStorage.Rows[i1]["MEDICINETYPENAME_VCHR"];
					btMed.Rows .InsertAt(newRow,0);
				}
				btMed.AcceptChanges();
				
			}
			if((string)this.m_objViewer.ctlDgMed.Tag=="btMedFind"&&tbStorage.Rows.Count>0&&btMedFind.Rows.Count>0)
			{
				for(int i1=tbStorage.Rows.Count-1;i1>=0;i1--)
				{

						DataRow  newRow=btMedFind.NewRow();
						newRow["MEDICINEID_CHR"]=	tbStorage.Rows[i1]["MEDICINEID_CHR"];
						newRow["ASSISTCODE_CHR"]=	tbStorage.Rows[i1]["ASSISTCODE_CHR"];
						newRow["MEDICINENAME_VCHR"]=	tbStorage.Rows[i1]["MEDICINENAME_VCHR"];
						newRow["MEDICINETYPENAME_VCHR"]=	tbStorage.Rows[i1]["MEDICINETYPENAME_VCHR"];
						btMedFind.Rows .InsertAt(newRow,0);
					
				}
				btMedFind.AcceptChanges();
				
			}


		}
		#endregion

		#region ��������
		/// <summary>
		/// ��������
		/// </summary>
		public void m_lngFindData()
		{
			string medType=this.m_objViewer.m_cmbMedType.Text.Trim();
			string medcode=this.m_objViewer.txtMedCode.Text.Trim();
			string medMane=this.m_objViewer.txtMedName.Text.Trim();
			try
			{
				btMedFind.Clear();
			}
			catch
			{
			}			
			if(medType != ""||medcode!=""||medMane!="")
			{
				try
				{
					btMedFind=btMed.Clone();
				}
				catch
				{
				}
				if(medType!="����")
				{
					for(int i1=0;i1<btMed.Rows.Count;i1++)
					{
						if(btMed.Rows[i1]["MEDICINETYPENAME_VCHR"].ToString().IndexOf(medType,0)==0)
						{
							DataRow newRow=btMedFind.NewRow();
							newRow["MEDICINEID_CHR"]=btMed.Rows[i1]["MEDICINEID_CHR"];
							newRow["ASSISTCODE_CHR"]=btMed.Rows[i1]["ASSISTCODE_CHR"];
							newRow["MEDICINENAME_VCHR"]=btMed.Rows[i1]["MEDICINENAME_VCHR"];
							newRow["MEDICINETYPENAME_VCHR"]=btMed.Rows[i1]["MEDICINETYPENAME_VCHR"];
							btMedFind.Rows.Add(newRow);
						}
					}
				}
				else
				{
					for(int i1=0;i1<btMed.Rows.Count;i1++)
					{						
						DataRow newRow=btMedFind.NewRow();
						newRow["MEDICINEID_CHR"]=btMed.Rows[i1]["MEDICINEID_CHR"];
						newRow["ASSISTCODE_CHR"]=btMed.Rows[i1]["ASSISTCODE_CHR"];
						newRow["MEDICINENAME_VCHR"]=btMed.Rows[i1]["MEDICINENAME_VCHR"];
						newRow["MEDICINETYPENAME_VCHR"]=btMed.Rows[i1]["MEDICINETYPENAME_VCHR"];
						btMedFind.Rows.Add(newRow);
					}
				}
				if(medcode!="")
				{
					for(int i1=0;i1<btMed.Rows.Count;i1++)
					{
						if(btMed.Rows[i1]["ASSISTCODE_CHR"].ToString().IndexOf(medcode,0)==0)
						{
							DataRow newRow=btMedFind.NewRow();
							newRow["MEDICINEID_CHR"]=btMed.Rows[i1]["MEDICINEID_CHR"];
							newRow["ASSISTCODE_CHR"]=btMed.Rows[i1]["ASSISTCODE_CHR"];
							newRow["MEDICINENAME_VCHR"]=btMed.Rows[i1]["MEDICINENAME_VCHR"];
							newRow["MEDICINETYPENAME_VCHR"]=btMed.Rows[i1]["MEDICINETYPENAME_VCHR"];
							btMedFind.Rows.Add(newRow);
						}
					}
				}
				if(medMane!="")
				{
					for(int i1=0;i1<btMed.Rows.Count;i1++)
					{
						if(btMed.Rows[i1]["MEDICINENAME_VCHR"].ToString().IndexOf(medMane,0)==0)
						{
							DataRow newRow=btMedFind.NewRow();
							newRow["MEDICINEID_CHR"]=btMed.Rows[i1]["MEDICINEID_CHR"];
							newRow["ASSISTCODE_CHR"]=btMed.Rows[i1]["ASSISTCODE_CHR"];
							newRow["MEDICINENAME_VCHR"]=btMed.Rows[i1]["MEDICINENAME_VCHR"];
							newRow["MEDICINETYPENAME_VCHR"]=btMed.Rows[i1]["MEDICINETYPENAME_VCHR"];
							btMedFind.Rows.Add(newRow);
						}
					}
				}
				this.m_objViewer.ctlDgMed.m_mthSetDataTable(btMedFind);
				this.m_objViewer.ctlDgMed.Tag="btMedFind";
		
			}
			else
			{
				MessageBox.Show("�������ѯ������","ϵͳ��ʾ");
			}
			m_lngClear();
		}
		#endregion

		#region ѡ��ֿ��¼�
		public void m_lngChangItem()
		{
			string storageID=btStorage.Rows[this.m_objViewer.m_cboStorage.SelectedIndex]["STORAGEID_CHR"].ToString();
			long lngRes=m_objDoMain.m_lngGetMedByStorageID(storageID,out tbStorage);
			if(lngRes==1)
			{
				this.m_objViewer.ctlDgStorageMed.m_mthSetDataTable(tbStorage);
			}
		}
		#endregion

		#region ���ҩƷ���ֿ⣨ȫ����
		public void m_lngAddNewAll()
		{
			string strID=btStorage.Rows[this.m_objViewer.m_cboStorage.SelectedIndex]["STORAGEID_CHR"].ToString().Trim();
			if((string)this.m_objViewer.ctlDgMed.Tag=="btMed")
			{
				if(btMed.Rows.Count==0)
					return;
				if(btMed.Rows.Count!=0)
				{
					long lngRes=m_objDoMain.m_lngAddMedToStorage(btMed,tbStorage,strID);
					if(lngRes==1)
					{
						for(int i1=0;i1<btMed.Rows.Count;i1++)
						{
							DataRow AddRow=tbStorage.NewRow();
							AddRow["MEDICINEID_CHR"]=btMed.Rows[i1]["MEDICINEID_CHR"];
							AddRow["MEDICINENAME_VCHR"]=btMed.Rows[i1]["MEDICINENAME_VCHR"];
							AddRow["ASSISTCODE_CHR"]=btMed.Rows[i1]["ASSISTCODE_CHR"];
							AddRow["MEDICINETYPENAME_VCHR"]=btMed.Rows[i1]["MEDICINETYPENAME_VCHR"];
							tbStorage.Rows.Add(AddRow);
						}
					}
					else
					{
						MessageBox.Show("�������ʧ�ܣ�������ĳЩ�����ظ���","ϵͳ��ʾ");
					}
				}

			}
			else
			{
				if(btMedFind.Rows.Count!=0)
				{
					btMedFind.AcceptChanges();
					if(btMedFind.Rows.Count==0)
						return;
					long lngRes=m_objDoMain.m_lngAddMedToStorage(btMedFind,tbStorage,strID);
					if(lngRes==1)
					{
						MessageBox.Show("������ݳɹ���","��ʾ");
						for(int i1=0;i1<btMedFind.Rows.Count;i1++)
						{
							DataRow AddRow=tbStorage.NewRow();
							AddRow["MEDICINEID_CHR"]=btMedFind.Rows[i1]["MEDICINEID_CHR"];
							AddRow["MEDICINENAME_VCHR"]=btMedFind.Rows[i1]["MEDICINENAME_VCHR"];
							AddRow["ASSISTCODE_CHR"]=btMedFind.Rows[i1]["ASSISTCODE_CHR"];
							AddRow["MEDICINETYPENAME_VCHR"]=btMedFind.Rows[i1]["MEDICINETYPENAME_VCHR"];
							tbStorage.Rows.Add(AddRow);
						}
					}
					else
					{
						MessageBox.Show("�������ʧ�ܣ�������ĳЩ�����ظ���","ϵͳ��ʾ");
					}
				}

			}

		}
		#endregion

		#region ɾ��ĳһ��ҩƷ��¼�Ӳֿ�
		public void m_lngDeleOneFromStorage()
		{
			if(this.m_objViewer.ctlDgStorageMed.CurrentCell.RowNumber>=0)
			{
				int nSelectCount = this.m_objViewer.ctlDgStorageMed.m_arrSelectRows().Length;					
				System.Collections.ArrayList arl = new System.Collections.ArrayList();
				for(int i1= 0;i1 < nSelectCount;i1++)
				{
					int nIndex = this.m_objViewer.ctlDgStorageMed.m_arrSelectRows()[i1];				
					string strID=btStorage.Rows[this.m_objViewer.m_cboStorage.SelectedIndex]["STORAGEID_CHR"].ToString().Trim();
					string strMedID=tbStorage.Rows[nIndex]["MEDICINEID_CHR"].ToString().Trim();
					long lngRes=m_objDoMain.m_lngDeleMedToStorage(strID,strMedID);
					if(lngRes==1)
					{
						//	this.m_objViewer.ctlDgStorageMed.m_mthDeleteRow(nIndex);
						arl.Add(strMedID);
						//	break;
					}					
				}	
				for(int j1 = 0; j1<arl.Count;j1++)
				{
					System.Data.DataRow[] drs = tbStorage.Select("MEDICINEID_CHR ='"+arl[j1].ToString()+"'");
					for(int j2 = 0;j2 < drs.Length;j2++)
					{
						drs[j2].Delete();
					}
				}
				tbStorage.AcceptChanges();
				
			}
			else
			{
				MessageBox.Show("��ѡ��ҩƷ��","ϵͳ��ʾ");
			}

		}
		#endregion

		#region ɾ��ҩƷ�Ӳֿ�(ȫ��)
		public void m_lngDelData()
		{
			string strID=btStorage.Rows[this.m_objViewer.m_cboStorage.SelectedIndex]["STORAGEID_CHR"].ToString().Trim();
			long lngRes=m_objDoMain.m_lngDeleMedToStorage(strID,null);
			if(lngRes==1)
			{
				this.m_objViewer.ctlDgStorageMed.m_mthDeleteAllRow();
			}
		}
		#endregion

		#region ���һ��ҩƷ����
		public void m_lngAddNewNeoData()
		{
			if(this.m_objViewer.ctlDgMed.CurrentCell.RowNumber>=0)
			{
				for(int j1 = 0; j1 < this.m_objViewer.ctlDgMed.m_arrSelectRows().Length;j1++)
				{
					int nIndex = this.m_objViewer.ctlDgMed.m_arrSelectRows()[j1];
					
					string strID=btStorage.Rows[this.m_objViewer.m_cboStorage.SelectedIndex]["STORAGEID_CHR"].ToString().Trim();
					string strMedId="";
					if((string)this.m_objViewer.ctlDgMed.Tag=="btMed")
					{
						strMedId=btMed.Rows[nIndex]["MEDICINEID_CHR"].ToString().Trim();
						int i1=0;
						if(tbStorage.Rows.Count>0)
						{
							for(i1 = 0;i1<tbStorage.Rows.Count;i1++)
							{
								if(tbStorage.Rows[i1]["MEDICINEID_CHR"].ToString().Trim()==strMedId)
								{
									MessageBox.Show("ҩƷ��"+tbStorage.Rows[i1]["MEDICINENAME_VCHR"].ToString()+"�Ѿ����ڸ�ҩ���У�","ϵͳ��ʾ");					
									break;
								}
							}
							if(i1 < tbStorage.Rows.Count)
							{
								continue;
							}
						}
						//string strMedID=btMed.Rows[nIndex]["MEDICINEID_CHR"].ToString().Trim();

						long lngRes=m_objDoMain.m_lngAddNoeMedToStorage(strID,strMedId);
						if(lngRes==1)
						{
							DataRow AddRow=tbStorage.NewRow();
							AddRow["MEDICINEID_CHR"]=btMed.Rows[nIndex]["MEDICINEID_CHR"];
							AddRow["MEDICINENAME_VCHR"]=btMed.Rows[nIndex]["MEDICINENAME_VCHR"];
							AddRow["ASSISTCODE_CHR"]=btMed.Rows[nIndex]["ASSISTCODE_CHR"];
							AddRow["MEDICINETYPENAME_VCHR"]=btMed.Rows[nIndex]["MEDICINETYPENAME_VCHR"];
							tbStorage.Rows.Add(AddRow);
						}
						
					}
					else
					{
						strMedId=btMedFind.Rows[nIndex]["MEDICINEID_CHR"].ToString().Trim();
						int i1=0;
						if(tbStorage.Rows.Count>0)
						{
							for(i1 = 0;i1<tbStorage.Rows.Count;i1++)
							{
								if(tbStorage.Rows[i1]["MEDICINEID_CHR"].ToString().Trim()==strMedId)
								{
									MessageBox.Show("ҩƷ��"+tbStorage.Rows[i1]["MEDICINENAME_VCHR"].ToString()+"�Ѿ����ڸ�ҩ���У�","ϵͳ��ʾ");					
									break;
								}
							}
							if(i1 < tbStorage.Rows.Count)
							{
								continue;
							}
						}
						long lngRes=m_objDoMain.m_lngAddNoeMedToStorage(strID,strMedId);
						if(lngRes==1)
						{
							DataRow AddRow=tbStorage.NewRow();
							AddRow["MEDICINEID_CHR"]=btMedFind.Rows[nIndex]["MEDICINEID_CHR"];
							AddRow["MEDICINENAME_VCHR"]=btMedFind.Rows[nIndex]["MEDICINENAME_VCHR"];
							AddRow["ASSISTCODE_CHR"]=btMedFind.Rows[nIndex]["ASSISTCODE_CHR"];
							AddRow["MEDICINETYPENAME_VCHR"]=btMedFind.Rows[nIndex]["MEDICINETYPENAME_VCHR"];
							tbStorage.Rows.Add(AddRow);
						}

					}
				}
				if(this.m_objViewer.checkBox1.Checked==true)
				{
					int[] RowNumber=this.m_objViewer.ctlDgMed.m_arrSelectRows();
					for(int i1=0;i1<RowNumber.Length;i1++)
					{
						if((string)this.m_objViewer.ctlDgMed.Tag=="btMed")
						{
							btMed.Rows[RowNumber[i1]-i1].Delete();
							btMed.AcceptChanges();
						}
						else
						{

							btMedFind.Rows[RowNumber[i1]-i1].Delete();
							btMedFind.AcceptChanges();
						}
					}
//					if((string)this.m_objViewer.ctlDgMed.Tag=="btMed")
//					{
//						btMed.AcceptChanges();
//					}
//					else
//						btMedFind.AcceptChanges();
				}

			}
			else
			{
				MessageBox.Show("��ѡ��һ��ҩƷ��","ϵͳ��ʾ");
			}

		}
		#endregion

		#region �����¼�
		public void m_lngReturnClick()
		{
			this.m_objViewer.ctlDgMed.m_mthSetDataTable(btMed);
			this.m_objViewer.ctlDgMed.Tag="btMed";
		}
		#endregion

		#region ����û�����
		private void m_lngClear()
		{
			this.m_objViewer.m_cmbMedType.Text="";
			this.m_objViewer.txtMedCode.Text="";
			this.m_objViewer.txtMedName.Text="";

		}
		#endregion

	}
}
