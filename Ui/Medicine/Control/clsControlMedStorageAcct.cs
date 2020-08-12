using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedStorageAcct ��ժҪ˵����
	/// </summary>
	public class clsControlMedStorageAcct: com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedStorageAcct()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ���ô������
		private frmMedStorageAcct m_objViewer;

		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			// TODO:  ��� clsControlStorageAcct.Set_GUI_Apperance ʵ��
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objViewer = (frmMedStorageAcct)frmMDI_Child_Base_in;
		}
		#endregion

		#region ����

		clsDomainControlStorageFin objSVC=new clsDomainControlStorageFin();
		/// <summary>
		/// ����δ���˵ĳ���ⵥ����
		/// </summary>
		DataTable MedStorageArr=new DataTable();
		/// <summary>
		/// ����δ���˵ĵ��۵�����
		/// </summary>
		DataTable MedStorageChangArr=new DataTable();
		/// <summary>
		/// �������������
		/// </summary>
		clsPeriod_VO[] objPriodItems = new clsPeriod_VO[0];
		/// <summary>
		/// ���浱ǰ�����ڵ�����
		/// </summary>
		int intSelPeriod=-1;
		#endregion

		#region ��ʼ������
		public void m_lngResetFrm()
		{
			m_lngGetAndFill();
			m_mthGetPeriodList();
		}
		#endregion

		#region ����������б�
		/// <summary>
		/// ����������б�
		/// </summary>
		private void m_mthGetPeriodList()
		{
			objPriodItems = clsPublicParm.s_GetPeriodList();
			string nowdate=clsPublicParm.s_datGetServerDate().Date.ToString();
			if(objPriodItems.Length >0)
			{
				int intcommand=0;
				for(int i1=0;i1<objPriodItems.Length;i1++)
				{
					this.m_objViewer.m_cboSelPeriod.Items.Insert(i1,objPriodItems[i1].m_strStartDate + " �� " + objPriodItems[i1].m_strEndDate);
					if(Convert.ToDateTime(nowdate)>=Convert.ToDateTime(objPriodItems[i1].m_strStartDate)&&Convert.ToDateTime(nowdate)<=Convert.ToDateTime(objPriodItems[i1].m_strEndDate))
					{
						intSelPeriod = i1;
						this.m_objViewer.m_cboSelPeriod.Tag = objPriodItems[i1].m_strPeriodID;
					}
					intcommand=i1;
				}
				this.m_objViewer.m_cboSelPeriod.Items.Insert(intcommand+1,"���в����ڵ�����");
				if(intSelPeriod!=-1)
				{
					m_objViewer.m_cboSelPeriod.SelectedIndex = intSelPeriod;
				}
				else
				{
					MessageBox.Show("��û�г�ʼ��������,�������ò�����!","ϵͳ��ʾ");
				}

			}
		}
		#endregion

		#region ������ݲ���䵽���˼�δ�����б�
		/// <summary>
		/// ������ݲ���䵽���˼�δ�����б�
		/// </summary>
		private void m_lngGetAndFill()
		{
			long lngRes=this.objSVC.m_lngGetMedStorageUnAcct(out MedStorageArr,out MedStorageChangArr);
			if(MedStorageArr.Rows.Count>0)
			{
				for(int i1=0;i1<MedStorageArr.Rows.Count;i1++)
				{
					if(MedStorageArr.Rows[i1]["PSTATUS_INT"].ToString()=="3")
						m_lngFillAeect(MedStorageArr.Rows[i1]);
					else
						m_lngFillUnAeect(MedStorageArr.Rows[i1]);
				}
			}
			if(MedStorageChangArr.Rows.Count>0)
			{
				MedStorageChangArr.Columns.Add("flag");
				for(int i1=0;i1<MedStorageChangArr.Rows.Count;i1++)
				{
					MedStorageChangArr.Rows[i1]["flag"]="����";
					if(MedStorageChangArr.Rows[i1]["PSTATUS_INT"].ToString()=="3")
						m_lngFillAeect(MedStorageChangArr.Rows[i1]);
					else
						m_lngFillUnAeect(MedStorageChangArr.Rows[i1]);
				}
			}
		}
		#endregion

		#region ��δ�������ݰ󶨵�δ�����б�
		/// <summary>
		/// ��δ�������ݰ󶨵�δ�����б�
		/// </summary>
		/// <param name="Rows"></param>
		private void m_lngFillUnAeect(DataRow Rows)
		{
			ListViewItem lisTemp=new ListViewItem(Rows["id"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["flag"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["creatName"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["CREATEDATE_DAT"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["aduitempName"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["tolMoney"].ToString().Trim());
			lisTemp.Tag=Rows;
			this.m_objViewer.m_lsvUnAccet.Items.Add(lisTemp);
		}
		#endregion

		#region �Ѽ��������ݰ󶨵��������б�
		/// <summary>
		/// �Ѽ��������ݰ󶨵��������б�
		/// </summary>
		/// <param name="Rows"></param>
		private void m_lngFillAeect(DataRow Rows)
		{
			ListViewItem lisTemp=new ListViewItem(Rows["id"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["flag"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["creatName"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["CREATEDATE_DAT"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["acctempName"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["tolMoney"].ToString().Trim());
			lisTemp.Tag=Rows;
			this.m_objViewer.lsvAccet.Items.Add(lisTemp);
		}
		#endregion

		#region ����ϸ������䵽�б�
		/// <summary>
		/// ����ϸ������䵽�б�
		/// </summary>
		/// <param name="command">1,�����.����������</param>
		/// <param name="GetRow"></param>
		private void m_lngFillLSV(int command,DataRow GetRow)
		{
			ListViewItem lisTemp=new ListViewItem(GetRow["ROWNO_CHR"].ToString().Trim());
			lisTemp.SubItems.Add(GetRow["MEDICINEID_CHR"].ToString().Trim());
			lisTemp.SubItems.Add(GetRow["MEDICINENAME_VCHR"].ToString().Trim());
			lisTemp.SubItems.Add(GetRow["MEDSPEC_VCHR"].ToString().Trim());
			lisTemp.SubItems.Add(GetRow["UNITID_CHR"].ToString().Trim());
			if(command==1)
			{
				lisTemp.SubItems.Add(GetRow["BUYUNITPRICE_MNY"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["SALEUNITPRICE_MNY"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["QTY_DEC"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["BUYTOLPRICE_MNY"].ToString().Trim());
			}
			else
			{
				lisTemp.SubItems.Add(GetRow["CURPRICE_MNY"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["CHANGEPRICE_MNY"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["QTY_DEC"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["ODDSDE_MNY"].ToString().Trim());
			}
			lisTemp.Tag=GetRow;
			this.m_objViewer.m_lsvDetail.Items.Add(lisTemp);
		}

		#endregion

		#region ��ʾ��ϸ����
		/// <summary>
		/// ��ʾ��ϸ����
		/// </summary>
		public void m_lngShowDe()
		{
			DataTable dtDe=new DataTable();
			long lngRes=0;
			string strID=this.m_objViewer.m_lsvUnAccet.SelectedItems[0].SubItems[0].Text.Trim();
			if(this.m_objViewer.m_lsvUnAccet.SelectedItems[0].SubItems[1].Text.Trim()=="����")
			{
				lngRes=this.objSVC.m_lngGetDeById(2,strID,out dtDe);
				if(this.m_objViewer.m_lsvDetail.Columns.Count==5)
				{
					System.Windows.Forms.ColumnHeader ColumnName=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName1=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName2=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName3=new ColumnHeader();
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName);
					this.m_objViewer.m_lsvDetail.Columns[5].Text="ԭ�۸�";
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName1);
					this.m_objViewer.m_lsvDetail.Columns[6].Text="�����۸�";
					this.m_objViewer.m_lsvDetail.Columns[6].Width=80;
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName2);
					this.m_objViewer.m_lsvDetail.Columns[7].Text="����";
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName3);
					this.m_objViewer.m_lsvDetail.Columns[8].Text="���";
				}
				else
				{
					this.m_objViewer.m_lsvDetail.Columns[5].Text="ԭ�۸�";
					this.m_objViewer.m_lsvDetail.Columns[6].Text="�����۸�";
					this.m_objViewer.m_lsvDetail.Columns[7].Text="����";
					this.m_objViewer.m_lsvDetail.Columns[8].Text="���";
				}
				this.m_objViewer.m_lsvDetail.Items.Clear();
				if(lngRes>0&&dtDe.Rows.Count>0)
				{
					for(int i1=0;i1<dtDe.Rows.Count;i1++)
					{
						m_lngFillLSV(2,dtDe.Rows[i1]);
					}
				}
			}
			else
			{
				lngRes=this.objSVC.m_lngGetDeById(1,strID,out dtDe);
				if(this.m_objViewer.m_lsvDetail.Columns.Count==5)
				{
					System.Windows.Forms.ColumnHeader ColumnName=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName1=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName2=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName3=new ColumnHeader();
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName);
					this.m_objViewer.m_lsvDetail.Columns[5].Text="�����";
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName1);
					this.m_objViewer.m_lsvDetail.Columns[6].Text="����";
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName2);
					this.m_objViewer.m_lsvDetail.Columns[7].Text="����";
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName3);
					this.m_objViewer.m_lsvDetail.Columns[8].Text="���";
				}
				else
				{
					this.m_objViewer.m_lsvDetail.Columns[5].Text="�����";
					this.m_objViewer.m_lsvDetail.Columns[6].Text="����";
					this.m_objViewer.m_lsvDetail.Columns[7].Text="����";
					this.m_objViewer.m_lsvDetail.Columns[8].Text="���";
				}
				this.m_objViewer.m_lsvDetail.Items.Clear();
				if(lngRes>0&&dtDe.Rows.Count>0)
				{
					for(int i1=0;i1<dtDe.Rows.Count;i1++)
					{
						m_lngFillLSV(1,dtDe.Rows[i1]);
					}
				}
			}


		}
		#endregion
	}
}
