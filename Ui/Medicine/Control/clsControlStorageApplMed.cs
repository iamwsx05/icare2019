using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;
using System.Drawing;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlStorageApplMed ��ժҪ˵����
	/// </summary>
	public class clsControlStorageApplMed : com.digitalwave.GUI_Base.clsController_Base	//gui_base.dll
	{
		public clsControlStorageApplMed()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ���ô������
		frmStorageApplMed m_objViewer;
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objViewer = (frmStorageApplMed)frmMDI_Child_Base_in;
		}

		#endregion

		#region ����
		clsDomainConrol_Medicne  objSVC=new clsDomainConrol_Medicne();
		/// <summary>
		/// �������뵥��
		/// </summary>
		clsStoreMedAppl_VO[] p_objResultArr=new clsStoreMedAppl_VO[0];
		/// <summary>
		/// �����������
		/// </summary>
		clsStoreMedAppl_VO[] findResultArr=null;
		/// <summary>
		/// ���浱ǰ���༭�����뵥����
		/// </summary>
		clsStoreMedAppl_VO p_objSeleItemArr=new clsStoreMedAppl_VO();
		/// <summary>
		/// ���浱ǰ���뵥�ݵ���ϸ����
		/// </summary>
		DataTable dtbResult=new DataTable();
		/// <summary>
		/// ��������ϸ����
		/// </summary>
		DataTable dtbResultArr=new DataTable();
		/// <summary>
		/// ����ֿ�ID
		/// </summary>
		private string StorageID;
		/// <summary>
		/// ���浱ǰ�����ڵ�ID
		/// </summary>
		string  strSelPeriod=null;
		#endregion

		#region ��ʼ������
		/// <summary>
		/// ��ʼ������
		/// </summary>
		public void m_lngResetForm()
		{
			long lngRes=this.objSVC.m_lngGetMedAppl(out p_objResultArr,(string)this.m_objViewer.ApplstorageMed.Tag,this.m_objViewer.dateTimePicker1.Value.ToShortDateString());
			if(lngRes>0&&p_objResultArr.Length>0)
			{
				for(int i1=0;i1<p_objResultArr.Length;i1++)
				{
                    if (p_objResultArr[i1].m_intPSTATUS_INT == 3)
                        m_lngDataFillTolsvAppl(p_objResultArr[i1]);
                    if (p_objResultArr[i1].m_intPSTATUS_INT == 2)
                        m_lngDataFillTolsvApplEnd(p_objResultArr[i1]);
				}
			}
			m_mthGetPeriodList();
		}
		#endregion

		#region ����
		public void m_lngReturnForm()
		{
			this.m_objViewer.DglApplDe.m_mthDeleteAllRow();
			this.m_objViewer.lsvAppl.Items.Clear();
			this.m_objViewer.lsvApplEnd.Items.Clear();  
			this.objSVC.m_lngGetMedAppl(out p_objResultArr,(string)this.m_objViewer.ApplstorageMed.Tag,this.m_objViewer.dateTimePicker1.Value.ToShortDateString());
			if(p_objResultArr.Length>0)
			{
				for(int i1=0;i1<p_objResultArr.Length;i1++)
				{
					if(p_objResultArr[i1].m_intPSTATUS_INT==3)
						m_lngDataFillTolsvAppl(p_objResultArr[i1]);
                    if (p_objResultArr[i1].m_intPSTATUS_INT == 2)
						m_lngDataFillTolsvApplEnd(p_objResultArr[i1]);
				}
			}
		}
		#endregion

		#region ����������б�
		/// <summary>
		/// ����������б�
		/// </summary>
		private void m_mthGetPeriodList()
		{
			clsPeriod_VO[] objPriodItems = new clsPeriod_VO[0];
			objPriodItems = clsPublicParm.s_GetPeriodList();
			string nowdate=clsPublicParm.s_datGetServerDate().Date.ToString();
			if(objPriodItems.Length >0)
			{
				for(int i1=0;i1<objPriodItems.Length;i1++)
				{
					if(Convert.ToDateTime(nowdate)>=Convert.ToDateTime(objPriodItems[i1].m_strStartDate)&&Convert.ToDateTime(nowdate)<=Convert.ToDateTime(objPriodItems[i1].m_strEndDate))
					{
						strSelPeriod = objPriodItems[i1].m_strPeriodID;
						this.m_objViewer.m_txtMedSpec.Text=objPriodItems[i1].m_strStartDate+"  ��  "+objPriodItems[i1].m_strEndDate;
						break;
					}
				}
			}
		}
		#endregion

		#region ����û�����ġ�ʵ���������Ƿ���ȷ
		public void m_lngCheckValues()
		{
			int intNumber;
			if(dtbResult.Rows[this.m_objViewer.DglApplDe.CurrentCell.RowNumber]["ʵ������"].ToString()!="")
			{
				try
				{
					intNumber=Convert.ToInt32(dtbResult.Rows[this.m_objViewer.DglApplDe.CurrentCell.RowNumber]["ʵ������"]);
				}
				catch
				{
					MessageBox.Show("��������","Icare",MessageBoxButtons.OK,MessageBoxIcon.Stop);
					this.m_objViewer.DglApplDe.CurrentCell=new DataGridCell(this.m_objViewer.DglApplDe.CurrentCell.RowNumber,7);
					return;
				}
			}
			else
			{
				MessageBox.Show("������ʵ������","Icare",MessageBoxButtons.OK,MessageBoxIcon.Stop);
				this.m_objViewer.DglApplDe.CurrentCell=new DataGridCell(this.m_objViewer.DglApplDe.CurrentCell.RowNumber,7);
				return;
			}
			if(intNumber>Convert.ToDouble(dtbResult.Rows[this.m_objViewer.DglApplDe.CurrentCell.RowNumber]["QTY_DEC"].ToString()))
			{
				MessageBox.Show("ʵ�����������Դ�����������","Icare",MessageBoxButtons.OK,MessageBoxIcon.Stop);
				this.m_objViewer.DglApplDe.CurrentCell=new DataGridCell(this.m_objViewer.DglApplDe.CurrentCell.RowNumber,7);
				return;
			}
			if(this.m_objViewer.DglApplDe.CurrentCell.RowNumber+1<this.m_objViewer.DglApplDe.RowCount)
			    this.m_objViewer.DglApplDe.CurrentCell=new DataGridCell(this.m_objViewer.DglApplDe.CurrentCell.RowNumber+1,3);
			else
				this.m_objViewer.btnSave.Focus();
		}
		#endregion

		#region ��������䵽lsvAppl
		private void m_lngDataFillTolsvAppl(clsStoreMedAppl_VO p_objResultArr)
		{
			ListViewItem lisTemp=null;
			lisTemp=new ListViewItem(p_objResultArr.m_strAPPLMEDSTORENAME_CHR);
			lisTemp.SubItems.Add(p_objResultArr.m_strMEDAPPLID_CHR);
			lisTemp.SubItems.Add(p_objResultArr.m_strAPPLDATE_DAT);
			lisTemp.SubItems.Add(p_objResultArr.m_strCREATORNAME_CHR);
			lisTemp.Tag=p_objResultArr;
			this.m_objViewer.lsvAppl.Items.Add(lisTemp);
		}

		#endregion

		#region ��������䵽lsvAppl
		private void m_lngDataFillTolsvApplEnd(clsStoreMedAppl_VO p_objResultArr)
		{
			ListViewItem lisTemp=null;
			lisTemp=new ListViewItem(p_objResultArr.m_strAPPLMEDSTORENAME_CHR);
			lisTemp.SubItems.Add(p_objResultArr.m_strMEDAPPLID_CHR);
			lisTemp.SubItems.Add(p_objResultArr.m_strAPPLDATE_DAT);
			lisTemp.SubItems.Add(p_objResultArr.m_strCREATORNAME_CHR);
			lisTemp.Tag=p_objResultArr;
			this.m_objViewer.lsvApplEnd.Items.Add(lisTemp);
		}

		#endregion

		#region LSVAPPL���¼�
		/// <summary>
		/// LSVAPPL���¼�
		/// </summary>
		public void m_lngLsvApplEdit()
		{
			this.m_objViewer.btnSave.Enabled=true;
			this.m_objViewer.DglApplDe.Enabled=true;
			p_objSeleItemArr=(clsStoreMedAppl_VO)this.m_objViewer.lsvAppl.SelectedItems[0].Tag;
			bool flat;
			this.objSVC.m_lngGetMedApplDeByID(p_objSeleItemArr.m_strMEDAPPLID_CHR,out dtbResult,out flat);
			if(!flat)
			{
				if(MessageBox.Show("��ҩϵͳ��⵽��ҩ�����뵥����ĳЩҩƷ��û�����á���װ���������Իᶪʧ���ݣ��Ƿ�Ҫ������","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
				{
					for(int i1=0;i1<dtbResult.Rows.Count;i1++)
					{
						if(dtbResult.Rows[i1]["PACKQTY_DEC"].ToString()=="0")
						{
							dtbResult.Rows.RemoveAt(i1);
						}
					}
				}
				else
				{
					this.m_objViewer.btnSave.Enabled=false;
					this.m_objViewer.DglApplDe.Enabled=false;
				}
			}
			dtbResult.Columns.Add("��������");
			dtbResult.Columns.Add("��ǰ���ŵ�����");
			dtbResult.Columns.Add("ʵ������");
			dtbResult.Columns.Add("��������");
			dtbResult.Columns.Add("��������ID");
			dtbResult.Columns.Add("���ⵥ��");
			dtbResult.Columns.Add("��Ч����");
			this.m_objViewer.DglApplDe.m_mthSetDataTable(dtbResult);
			this.m_objViewer.DglApplDe.Select();
			this.m_objViewer.DglApplDe.Focus();
			this.m_objViewer.DglApplDe.CurrentCell=new DataGridCell(0,3);
			SendKeys.SendWait("{Right}");
			SendKeys.SendWait("{Left}");
		}
		#endregion

		#region ��ȡѡ�����ݵ���Ϣ
		public void m_GetseleData(clsMedStoreOut_VO objVO)
		{
			dtbResult.Rows[this.m_objViewer.DglApplDe.CurrentCell.RowNumber]["��������"]=objVO.strSYSLOTNO_CHR;
			dtbResult.Rows[this.m_objViewer.DglApplDe.CurrentCell.RowNumber]["��ǰ���ŵ�����"]=objVO.strCURQTY_DEC;
			dtbResult.Rows[this.m_objViewer.DglApplDe.CurrentCell.RowNumber]["��������"]=objVO.strPRODUCTORNAME_CHR;
			dtbResult.Rows[this.m_objViewer.DglApplDe.CurrentCell.RowNumber]["��������ID"]=objVO.strPRODUCTORID_CHR;
			dtbResult.Rows[this.m_objViewer.DglApplDe.CurrentCell.RowNumber]["���ⵥ��"]=objVO.dlSALEUNITPRICE_MNY;
			dtbResult.Rows[this.m_objViewer.DglApplDe.CurrentCell.RowNumber]["��Ч����"]=objVO.USEFULLIFE_DAT;
			StorageID=(string)this.m_objViewer.ApplstorageMed.Tag;
		}

		#endregion

		#region lsvApplEnd���¼�
		/// <summary>
		/// lsvApplEnd���¼�
		/// </summary>
		public void m_lnglsvApplEnd()
		{
			p_objSeleItemArr=(clsStoreMedAppl_VO)this.m_objViewer.lsvApplEnd.SelectedItems[0].Tag;
			bool flat;
			this.objSVC.m_lngGetMedApplDeByID(p_objSeleItemArr.m_strMEDAPPLID_CHR,out dtbResult,out flat);
			dtbResult.Columns.Add("��������");
			dtbResult.Columns.Add("��ǰ���ŵ�����");
			dtbResult.Columns.Add("��������");
			dtbResult.Columns.Add("ʵ������");
			dtbResult.Columns.Add("��������ID");
			dtbResult.Columns.Add("���ⵥ��");
			dtbResult.Columns.Add("��Ч����");
			this.m_objViewer.DglApplDe.m_mthSetDataTable(dtbResult);
			
		}
		#endregion

		#region ��ʾ���ϳ��������Ĳ�Ʒ
		public void m_lngShowMedData(com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
		{
			this.m_objViewer.ctlApplMedOut.strSTORAGEID=(string)this.m_objViewer.ApplstorageMed.Tag;
			this.m_objViewer.ctlApplMedOut.isApplMebMod=dtbResult.Rows[this.m_objViewer.DglApplDe.CurrentCell.RowNumber]["MEDICINEID_CHR"].ToString();
			Point p=e.m_ptPositionInScreen;
			p=this.m_objViewer.FindForm().PointToClient(p);
			p.Y+=23;
			this.m_objViewer.ctlApplMedOut.Location=p;
			this.m_objViewer.ctlApplMedOut.Visible=true;
			this.m_objViewer.ctlApplMedOut.Focus();
		}
		#endregion

		#region �������ɳ��ⵥ
		public void m_lngSave()
		{
			if(strSelPeriod==null)
			{
				MessageBox.Show("ϵͳ�Ҳ������ʵĲ����ڣ�����ʧ�ܣ�","Icare",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
            if (this.m_objViewer.ordTypeVo.m_strStorageOrdTypeID=="")
            {
                MessageBox.Show("���ܱ���,���ȵ�[������������]ģ����������Ӧ�ĵ������ͣ�", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
			clsMedStorageOrd_VO  StorageOrdVo=new clsMedStorageOrd_VO();
			StorageOrdVo.m_strCREATORNAME_CHR=this.m_objViewer.LoginInfo.m_strEmpID;
			StorageOrdVo.m_intDEPTTYPE_INT=1;
			StorageOrdVo.m_strDOCID_VCHR=this.m_objViewer.txtMedID.Text.Trim();
            //StorageOrdVo.m_strSTORAGEORDID_CHR = ;
			StorageOrdVo.m_strSTORAGEID_CHR=StorageID;
			StorageOrdVo.m_strCREATORID_CHR=this.m_objViewer.LoginInfo.m_strEmpID;
			StorageOrdVo.m_strINORD_DAT=clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss");
			StorageOrdVo.m_strMEMO_VCHR=p_objSeleItemArr.m_strMEMO_VCHR;
			StorageOrdVo.m_strCREATEDATE_DAT=p_objSeleItemArr.m_strAPPLDATE_DAT;
			StorageOrdVo.m_strPERIODID_CHR=strSelPeriod;
			StorageOrdVo.m_intPSTATUS_INT=1;
            StorageOrdVo.m_strDEPTID_CHR = p_objSeleItemArr.m_strAPPLMEDSTOREID_CHR;
            StorageOrdVo.m_strDEPTNAME_CHR = p_objSeleItemArr.m_strAPPLMEDSTORENAME_CHR;
            StorageOrdVo.m_strSTORAGEORDTYPEID_CHR=this.m_objViewer.ordTypeVo.m_strStorageOrdTypeID;
			int tolRow=dtbResult.Rows.Count;
			for(int i1=0;i1<tolRow;i1++)
			{
				if(dtbResult.Rows[i1]["��ǰ���ŵ�����"].ToString()=="")
				{
					dtbResult.Rows.RemoveAt(i1);
					tolRow=tolRow-1;
					i1=i1-1;
				}
			}
		    clsMedStorageOrdDe_VO[] StorageOrdDeVo=new clsMedStorageOrdDe_VO[dtbResult.Rows.Count];
			if(dtbResult.Rows.Count!=0)
			{
				for(int i1=0;i1<dtbResult.Rows.Count;i1++)
				{
					StorageOrdDeVo[i1]=new clsMedStorageOrdDe_VO();
					try
					{
						StorageOrdDeVo[i1].m_dblSALEUNITPRICE_MNY=Convert.ToDouble(dtbResult.Rows[i1]["���ⵥ��"].ToString().Trim());
						StorageOrdDeVo[i1].m_dblQTY_DEC=Convert.ToDouble(dtbResult.Rows[i1]["ʵ������"].ToString().Trim());
						StorageOrdDeVo[i1].m_dblBUYTOLPRICE_MNY=Math.Round(StorageOrdDeVo[i1].m_dblQTY_DEC*StorageOrdDeVo[i1].m_dblSALEUNITPRICE_MNY,4);
					}
					catch
					{
					}
					StorageOrdDeVo[i1].m_strMEDICINEID_CHR=dtbResult.Rows[i1]["MEDICINEID_CHR"].ToString().Trim();
					StorageOrdDeVo[i1].m_strSYSLOTNO_CHR=dtbResult.Rows[i1]["��������"].ToString().Trim();
					StorageOrdDeVo[i1].m_strROWNO_CHR=dtbResult.Rows[i1]["ROWNO_CHR"].ToString().Trim();
					StorageOrdDeVo[i1].m_strUNITID_CHR=dtbResult.Rows[i1]["UNITID_CHR"].ToString().Trim();
					StorageOrdDeVo[i1].m_strPRODCUTORID_CHR=dtbResult.Rows[i1]["��������ID"].ToString().Trim();
					StorageOrdDeVo[i1].m_strUSEFULLIFE_DAT=dtbResult.Rows[i1]["��Ч����"].ToString().Trim();
					StorageOrdDeVo[i1].m_strORD_DAT=clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss");
				}
			}
			else
			{
				MessageBox.Show("û����Ч������","Icare",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
			string strMedApplId=p_objSeleItemArr.m_strMEDAPPLID_CHR;
			long lngRes=this.objSVC.m_lngChangAndSave(strMedApplId,StorageOrdVo,StorageOrdDeVo);
			if(lngRes>0)
			{
				MessageBox.Show("����ɹ�","Icare",MessageBoxButtons.OK,MessageBoxIcon.Information);
				this.m_objViewer.txtMedID.Text=clsPublicParm.m_mthGetNewDocument(this.m_objViewer.txtMedID.Text,"1",0);
				m_lngDataFillTolsvApplEnd(p_objSeleItemArr);
				this.m_objViewer.lsvAppl.SelectedItems[0].Remove();
				this.m_objViewer.DglApplDe.m_mthDeleteAllRow();
			}

		}
		#endregion

		#region  ���ҹ���
		public void m_lngFind()
		{
			string txtsele=this.m_objViewer.cobSele.Text.Trim();
			string txtFind=this.m_objViewer.txtfind.Text.Trim();
			if(txtsele==""||txtFind=="")
			{
				MessageBox.Show("�������ѯ����","Icare",MessageBoxButtons.OK,MessageBoxIcon.Stop);
				return;
			}
			int intNumber=0;
			findResultArr=new clsStoreMedAppl_VO[p_objResultArr.Length];
			int intlength=0;
			if(p_objResultArr.Length>0)
			{
				for(int i1=0;i1<p_objResultArr.Length;i1++)
				{
					switch(txtsele)
					{
						case "����ҩ��":
							intNumber=p_objResultArr[i1].m_strAPPLMEDSTORENAME_CHR.IndexOf(txtFind,0);
							break;
						case "���뵥��":
							intNumber=p_objResultArr[i1].m_strMEDAPPLID_CHR.IndexOf(txtFind,0);
							break;
						case "������":
							intNumber=p_objResultArr[i1].m_strCREATORNAME_CHR.IndexOf(txtFind,0);
							break;
					}
					
					if(intNumber==0)
					{
						findResultArr[intlength]=p_objResultArr[i1];
						intlength++;
					}
				}

			}
			this.m_objViewer.DglApplDe.m_mthDeleteAllRow();
			this.m_objViewer.lsvAppl.Items.Clear();
			this.m_objViewer.lsvApplEnd.Items.Clear();
			if(intlength!=0)
			{
				for(int i1=0;i1<intlength;i1++)
				{
					if(findResultArr[i1].m_intPSTATUS_INT==1)
						m_lngDataFillTolsvAppl(findResultArr[i1]);
					else
						m_lngDataFillTolsvApplEnd(findResultArr[i1]);

				}
			}
			else
				MessageBox.Show("û�з��������ļ�¼","Icare",MessageBoxButtons.OK,MessageBoxIcon.Stop);
			m_lngClear();
		}
		#endregion

		#region ����
		public void m_lngLocked()
		{
			this.m_objViewer.DglApplDe.Enabled=false;
			this.m_objViewer.btnSave.Enabled=false;
			
		}
		#endregion

		#region ����
		public void m_lngUnLocked()
		{
			this.m_objViewer.DglApplDe.Enabled=true;
			this.m_objViewer.btnSave.Enabled=true;
			
		}
		#endregion

		#region ��ղ�������
		private void m_lngClear()
		{
			this.m_objViewer.cobSele.Text="";
			this.m_objViewer.txtfind.Text="";
		}
		#endregion

	}
}
