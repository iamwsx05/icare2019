using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlReturnTicket ��ժҪ˵����
	/// </summary>
	public class clsControlReturnTicket:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlReturnTicket()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ���ô������
		/// <summary>
		/// ���洰����Ϣ
		/// </summary>
		clsOPMedStoreWin_VO[] objMedStoreWinArr = new clsOPMedStoreWin_VO[0];

		/// <summary>
		///��¼����ID
		/// </summary>
		private string strRegID;
		/// <summary>
		///��¼��Ʊ��
		/// </summary>
		private string Invoiceno;
		/// <summary>
		///�����û�ID
		/// </summary>
		string strOperID;
		/// <summary>
		///�������е�����
		/// </summary>
		private DataTable Fidtable=new DataTable();
		/// <summary>
		///���浱ǰ���ڼ���״̬�Ĵ���ID
		/// </summary>
		private string strWinID;

		clsDomainControlReturnTicket m_objManage=new clsDomainControlReturnTicket();
		clsPublicParm PublicClass=new clsPublicParm();
		com.digitalwave.iCare.gui.HIS.frmReturnTicket m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmReturnTicket)frmMDI_Child_Base_in;
		}
		#endregion

		#region ����
		/// <summary>
		/// ����ҩ����Ϣ
		/// </summary>
		clsMedStore_VO[] objMedStorearr = new clsMedStore_VO[0];


		#endregion 

		#region ��ʼ������
		public void m_lngLoadFrm()
		{
			this.m_objViewer.comboBox1.SelectedIndex=0;
			m_mthGetMedStore();
		}
		#endregion

		#region ����ҩ����ʾ����
		public void m_lngShowWin()
		{
			string MedID=objMedStorearr[this.m_objViewer.cobMedStore.SelectedIndex].m_strMedStoreID;
			if(objMedStorearr[this.m_objViewer.cobMedStore.SelectedIndex].m_intMedicneType==1)
			   this.m_objViewer.cobMedStore.Tag="0001";
			else
				this.m_objViewer.cobMedStore.Tag="0002";
			m_mthGetMedStoreWin(MedID);
			
		}
		#endregion

		#region �ж���ǰ������Ĵ���
		public void m_lngWindows()
		{
			if(this.m_objViewer.tab.SelectedIndex==1)
			this.m_objViewer.bntok.Enabled=false;
			else
			this.m_objViewer.bntok.Enabled=true;
			this.m_objViewer.m_lsvMedicineDetail.Items.Clear();
			this.m_objViewer.m_lsvOpRecDetail.Items.Clear();
		}


		#endregion

		#region ���ݴ��ڻ�ü���ҩ������ҩ����
		/// <summary>
		/// ���ݴ��ڻ�ü���ҩ������ҩ����
		/// </summary>
		public  void m_ingGetOutDataByWindowID(string windowsID)
		{
			long lngRes;
			clsOutPatienTrecipEinv_VO[] objMedStorearr = new clsOutPatienTrecipEinv_VO[0];
			clsDomainControlReturnTicket objManage = new clsDomainControlReturnTicket();
			lngRes = objManage.m_ingGetOutDataByWindowID(windowsID,out objMedStorearr,this.m_objViewer.dateTimePicker1.Value);
			this.m_objViewer.m_lsvPatientDetial.Items.Clear();
			this.m_objViewer.lvsreture.Items.Clear();
			m_mthFillListView(objMedStorearr);
			this.m_objViewer.m_lsvOpRecDetail.Items.Clear();
			this.m_objViewer.m_lsvMedicineDetail.Items.Clear();
		}
		#endregion

		#region �������
		/// <summary>
		/// �������
		/// </summary>
		/// <param name="objMedStorearr"></param>
		private void m_mthFillListView(clsOutPatienTrecipEinv_VO[] objMedStorearr)
		{
			ListViewItem lisTemp=null;
			for(int i1=0;i1<objMedStorearr.Length;i1++)
			{
				lisTemp=new ListViewItem(objMedStorearr[i1].m_strINVOICENO_VCHR);
				lisTemp.SubItems.Add(objMedStorearr[i1].m_strOPREMPNAME_CHR);
				lisTemp.SubItems.Add(objMedStorearr[i1].m_strSEQID_CHR);
				lisTemp.SubItems.Add(objMedStorearr[i1].m_strOUTPATRECIPEID_CHR);
				lisTemp.SubItems.Add(objMedStorearr[i1].m_strINVDATE_DAT);
				lisTemp.SubItems.Add(objMedStorearr[i1].m_strPATIENTCARDID_CHR);
				lisTemp.Tag=objMedStorearr[i1];
				if(objMedStorearr[i1].m_intSTATUS_INT==2)
				{
					this.m_objViewer.lvsreture.Items.Add(lisTemp);
				}
				else
				{
					this.m_objViewer.m_lsvPatientDetial.Items.Add(lisTemp);
				}
				
			}
		}
		#endregion

		#region ѡ�з�Ʊ����ʾ����
		/// <summary>
		/// ѡ�з�Ʊ����ʾ����
		/// </summary>
		public void m_mthSelPatient(int m_lngComm)
		{
			clsOutPatienTrecipEinv_VO m_lngSelItem=new clsOutPatienTrecipEinv_VO();

				if(m_lngComm==1)
				{
					if(this.m_objViewer.m_lsvPatientDetial.SelectedItems.Count==0)
					{
						return;
					}
					m_lngSelItem=(clsOutPatienTrecipEinv_VO)this.m_objViewer.m_lsvPatientDetial.SelectedItems[0].Tag;
				}
				else
				{
					if(this.m_objViewer.lvsreture.SelectedItems.Count==0)
					{
						return;
					}
					m_lngSelItem=(clsOutPatienTrecipEinv_VO)this.m_objViewer.lvsreture.SelectedItems[0].Tag;
				}
			strRegID=m_lngSelItem.m_strOUTPATRECIPEID_CHR;
			Invoiceno=m_lngSelItem.m_strINVOICENO_VCHR;

			long lngRes = 0;
			clsOutpatientRecipe_VO[] objItems = new clsOutpatientRecipe_VO[0];
			lngRes = m_objManage.m_lngGetMainRecipe(strRegID,out objItems);
			m_objViewer.m_lsvOpRecDetail.Items.Clear();
			m_objViewer.m_lsvMedicineDetail.Items.Clear();
			if(lngRes >0 && objItems.Length >0)
			{
				for(int i=0;i<objItems.Length;i++)
				{
					objItems[i].m_strOutpatRecipeID=strRegID;
					ListViewItem lsvItem=null;
					lsvItem = new ListViewItem(objItems[i].m_strOutpatRecipeID.Trim());
					lsvItem.SubItems.Add(objItems[i].m_objDiagDr.strLastName.Trim());
					lsvItem.SubItems.Add(objItems[i].m_objDiagDept.strDeptName.Trim());
					lsvItem.SubItems.Add(objItems[i].m_objRecordEmp.strLastName.Trim());
					lsvItem.SubItems.Add(objItems[i].m_objPatient.strName);
					lsvItem.SubItems.Add(objItems[i].m_strOutpatRecipeID);
					lsvItem.Tag = objItems[i];
					m_objViewer.m_lsvOpRecDetail.Items.Add(lsvItem);
				}
				this.m_objViewer.m_lsvOpRecDetail.Items[0].Selected = true;
				m_mthSelRecipe();
			}
		}
		#endregion

		#region ѡ�д�����ʾ��Ŀ
		/// <summary>
		/// ѡ�д�����ʾ��Ŀ
		/// </summary>
		public void m_mthSelRecipe()
		{
			clsOutpatientRecipe_VO m_lngSelItem=new clsOutpatientRecipe_VO();
			m_lngSelItem=(clsOutpatientRecipe_VO)this.m_objViewer.m_lsvOpRecDetail.SelectedItems[0].Tag;
			string p_strOPRecID=m_lngSelItem.m_strOutpatRecipeID;
			clsOprecipeItemDe[] objItems;
			long lngRes = 0;
			string typeID=(string)this.m_objViewer.cobMedStore.Tag;
			objItems = new clsOprecipeItemDe[0];
			lngRes=m_objManage.m_lngGetOPRecipeListByWinAndOpRecAndType(p_strOPRecID,typeID,out objItems);
			m_objViewer.m_lsvMedicineDetail.Items.Clear();
			Decimal tolMeney=0;
			if(lngRes > 0 && objItems.Length > 0)
			{
				int j2=1;
				for(int i=0;i<objItems.Length;i++)
				{
					ListViewItem lsvItem = new ListViewItem(j2.ToString("000"));
					lsvItem.SubItems.Add(objItems[i].m_objItem.m_strItemCode.Trim());
					lsvItem.SubItems.Add(objItems[i].m_objItem.m_strItemName.Trim());
					lsvItem.SubItems.Add(objItems[i].m_objUnit.m_strUnitID.Trim());
					lsvItem.SubItems.Add(objItems[i].m_decQty.ToString("0.00"));
					lsvItem.SubItems.Add(objItems[i].m_decPrice.ToString("0.00"));
					lsvItem.SubItems.Add(objItems[i].m_decTolPrice.ToString("0.00"));
					lsvItem.SubItems.Add(objItems[i].m_intRecipeType.ToString());
					lsvItem.SubItems.Add(objItems[i].m_objUnit.m_strUnitID.ToString());
					tolMeney+=objItems[i].m_decTolPrice;
					lsvItem.Tag = objItems[i];
					m_objViewer.m_lsvMedicineDetail.Items.Add(lsvItem);
					j2++;
				}
				ListViewItem lsvItemEnd = new ListViewItem("");
				lsvItemEnd.SubItems.Add("");
				lsvItemEnd.SubItems.Add("");
				lsvItemEnd.SubItems.Add("");
				lsvItemEnd.SubItems.Add("");
				lsvItemEnd.SubItems.Add("�ܼƣ�");
				lsvItemEnd.SubItems.Add(tolMeney.ToString("#######.00"));
				m_objViewer.m_lsvMedicineDetail.Items.Add(lsvItemEnd);

			}
		}
		#endregion

		#region ����ҩ����ı�ʱ�������¼�
		public void m_lngCobChang()
		{
			strWinID=objMedStoreWinArr[this.m_objViewer.cbxMedStoreWin.SelectedIndex].m_strWindowID;
			m_ingGetOutDataByWindowID(strWinID);
		}
		#endregion

		#region ���ҩ��
		/// <summary>
		/// ���ҩ��
		/// </summary>
		private void m_mthGetMedStore()
		{
			long lngRes;
			clsDomainControlReturnTicket objManage = new clsDomainControlReturnTicket();
			lngRes = objManage.m_lngGetMedStoreList(out objMedStorearr);
			
			if(objMedStorearr.Length > 0)
			{
				for(int i1=0;i1<objMedStorearr.Length;i1++)
				{
					this.m_objViewer.cobMedStore.Items.Add(objMedStorearr[i1].m_strMedStoreName);
				}
			}
			else
			{
				this.m_objViewer.cobMedStore.Text = "��ÿⷿ����";
				this.m_objViewer.cobMedStore.Tag = null;
			}
		}
		#endregion

		#region ��ô���
		/// <summary>
		/// ��ô���
		/// </summary>
		private void m_mthGetMedStoreWin(string medID)
		{
			long lngRes;
			clsDomainControlReturnTicket objManage = new clsDomainControlReturnTicket();
			lngRes = objManage.m_lngGetMedStoreWinList(medID,out objMedStoreWinArr);
			m_objViewer.cbxMedStoreWin.Items.Clear();
			if(objMedStoreWinArr.Length>0)
			{
				for(int i1=0;i1<objMedStoreWinArr.Length;i1++)
				{
					m_objViewer.cbxMedStoreWin.Items.Add(objMedStoreWinArr[i1].m_strWindowName);

				}
				m_objViewer.cbxMedStoreWin.Text = objMedStoreWinArr[0].m_strWindowName.Trim();
				m_objViewer.cbxMedStoreWin.Tag = objMedStoreWinArr[0];
			}
			else
			{
				m_objViewer.cbxMedStoreWin.Text = "��ô��ڳ���";
				m_objViewer.cbxMedStoreWin.Tag = null;
			}
		}
		#endregion

		#region ��ò���Ա
		/// <summary>
		/// ��ò���Ա
		/// </summary>
		private void m_mthGetOperator()
		{
			strOperID = "0000001";
			string strOperName = clsPublicParm.s_strGetEmpInfo("lastname_vchr","empid_chr",strOperID);
		}
		#endregion

		#region  ��ҩ�����¼�
		public void m_lngRetureOperator()
		{
			if(this.m_objViewer.m_lsvPatientDetial.Items.Count>0)
			{
				if(this.m_objViewer.m_lsvMedicineDetail.Items.Count==0)
				{
					MessageBox.Show("��ѡ��Ҫ�˵�ҩ","ϵͳ��ʾ");
					return;
				}
			}
			else
			{
				MessageBox.Show("��ǰû�п����˵�ҩ","ϵͳ��ʾ");
				return;
			}
			long lngRes=m_objManage.m_lngReturn(strRegID);
			if(lngRes<1)
				return;
			for(int i1=0;i1<this.m_objViewer.m_lsvMedicineDetail.Items.Count-1;i1++)
			{
				switch(Convert.ToInt32(this.m_objViewer.m_lsvMedicineDetail.Items[i1].SubItems[7].Text))
				{
					case 1:
					{

						clsOutPaticntPwmreciPeDe_VO p_objRecord=new clsOutPaticntPwmreciPeDe_VO();
						m_lngFillPWMVo(i1,out p_objRecord);
						lngRes=this.m_objManage.m_lngAddNewPwmreciPeDe(p_objRecord);
						break;
					}
					case 2:
					{
						clsOutPaticntCmreciPeDe_VO p_objRecord =new clsOutPaticntCmreciPeDe_VO();
						m_lngFillCMVo(i1,out p_objRecord);
						lngRes=this.m_objManage.m_lngAddNewCmreciPeDe(p_objRecord);
						break;
					}
				}
			}
			if(lngRes>0)
			{
				MessageBox.Show("��ҩ�ɹ�","ϵͳ��ʾ");
				clsOutPatienTrecipEinv_VO p_objRecord=new clsOutPatienTrecipEinv_VO();
				p_objRecord=(clsOutPatienTrecipEinv_VO)this.m_objViewer.m_lsvPatientDetial.SelectedItems[0].Tag;
				ListViewItem lisTemp=null;
				lisTemp=new ListViewItem(p_objRecord.m_strINVOICENO_VCHR);
				lisTemp.SubItems.Add(p_objRecord.m_strOPREMPNAME_CHR);
				lisTemp.SubItems.Add(p_objRecord.m_strSEQID_CHR);
				lisTemp.SubItems.Add(p_objRecord.m_strOUTPATRECIPEID_CHR);
				lisTemp.SubItems.Add(p_objRecord.m_strINVDATE_DAT);
				lisTemp.Tag=p_objRecord;
				this.m_objViewer.lvsreture.Items.Add(lisTemp);
				this.m_objViewer.m_lsvMedicineDetail.Items.Clear();
				this.m_objViewer.m_lsvOpRecDetail.Items.Remove(m_objViewer.m_lsvOpRecDetail.SelectedItems[0]);
				this.m_objViewer.m_lsvPatientDetial.Items.Remove(m_objViewer.m_lsvPatientDetial.SelectedItems[0]);
			}
			else
			{
				MessageBox.Show("��ҩʧ��","ϵͳ��ʾ");
			}

		}
		#endregion

		#region ����ҩ��ϸ�󶨵���ҩ��ϸVO
		private void m_lngFillPWMVo(int rowNumber,out clsOutPaticntPwmreciPeDe_VO p_objRecord)
		{
			p_objRecord=new clsOutPaticntPwmreciPeDe_VO();
			p_objRecord.m_dblTOLPRICE_MNY=Convert.ToDouble(this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[6].Text.Trim());
			p_objRecord.m_dblUNITPRICE_MNY=Convert.ToDouble(this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[5].Text.Trim());
			p_objRecord.m_strITEMID_CHR=this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[1].Text.Trim();
			p_objRecord.m_strOUTPATRECIPEID_CHR=strRegID;
			p_objRecord.m_strRETURN_DAT=DateTime.Now.ToString("yyyy-MM-dd");
			p_objRecord.m_strRETURNEMP_CHR=strOperID;
			p_objRecord.m_dblTOLQTY_DEC=Convert.ToDouble(this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[4].Text.Trim());
			p_objRecord.m_strROWNO_CHR=this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[0].Text.Trim();
			string newID;
            PublicClass.m_GetNewId("T_OPR_OUTPATIENTPWMRECIPEDE","OUTPATRECIPEDEID_CHR",out newID,20);
			p_objRecord.m_strOUTPATRECIPEDEID_CHR=newID;
			p_objRecord.m_strTREATEMP_CHR=strOperID;
			p_objRecord.m_strUNITID_CHR=this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[8].Text.Trim();
		}
		#endregion

		#region ����ҩ��ϸ�󶨵���ҩ��ϸVO
		private void m_lngFillCMVo(int rowNumber,out clsOutPaticntCmreciPeDe_VO p_objRecord)
		{
			p_objRecord=new clsOutPaticntCmreciPeDe_VO();
			p_objRecord.m_dblTOLPRICE_MNY=Convert.ToDouble(this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[6].Text.Trim());
			p_objRecord.m_dblUNITPRICE_MNY=Convert.ToDouble(this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[5].Text.Trim());
			p_objRecord.m_strITEMID_CHR=this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[1].Text.Trim();
			p_objRecord.m_strOUTPATRECIPEID_CHR=strRegID;
			p_objRecord.m_strRETURN_DAT=DateTime.Now.ToString("yyyy-MM-dd");
			p_objRecord.m_strRETURNEMP_CHR=strOperID;
			p_objRecord.m_dblQTY_DEC=Convert.ToDouble(this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[4].Text.Trim());
			p_objRecord.m_strROWNO_CHR=this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[0].Text.Trim();
			string newID;
			PublicClass.m_GetNewId("T_OPR_OUTPATIENTCMRECIPEDE","OUTPATRECIPEDEID",out newID,20);
			p_objRecord.m_strOUTPATRECIPEDEID_CHR=newID;
			p_objRecord.m_strTREATEMP_CHR=strOperID;
			p_objRecord.m_strUNITID_CHR=this.m_objViewer.m_lsvMedicineDetail.Items[rowNumber].SubItems[8].Text.Trim();

		}
		#endregion

		#region ����û�����
		public bool m_ingCheckValues()
		{
//			if(this.m_objViewer.INVOICENO.Text=="")
//			{
//				if(this.m_objViewer.OUTPATRECIPEID.Text=="")
//				{
//					if(this.m_objViewer.txtName.Text=="")
//					{
//						if(this.m_objViewer.SEQID.Text=="")
//						{
//							if(this.m_objViewer.m_dteRegisterDate.Value.ToString("yyyy-MM-dd")=="2004-01-01")
//							{
//								return false;
//							}
//		
//						}
//					}
//				}
//			}
			return true;
		}
		#endregion

		#region �����������
		public void m_ingClearFrm()
		{
//			this.m_objViewer.INVOICENO.Text="";
//			this.m_objViewer.OUTPATRECIPEID.Text="";
//			this.m_objViewer.txtName.Text="";
//			this.m_objViewer.SEQID.Text="";
		}
		#endregion

		#region ����ģ��
		public void m_ingFid()
		{
			
			if(this.m_objViewer.OUTPATRECIPEID.Text!="")
			{
				int cou=0;
				switch(this.m_objViewer.comboBox1.SelectedIndex)
				{
					case 0:
						cou=5;
						break;
					case 1:
						cou=0;
						break;
					case 2:
						cou=2;
						break;
				}
				if(this.m_objViewer.tab.SelectedIndex==0)
				{
					if(this.m_objViewer.m_lsvPatientDetial.Items.Count>0)
					{
						if(m_mthFindDataFromList(this.m_objViewer.m_lsvPatientDetial,this.m_objViewer.OUTPATRECIPEID.Text.Trim(),cou)==0)
						{
							this.m_objViewer.m_lsvOpRecDetail.Items.Clear();
							this.m_objViewer.m_lsvMedicineDetail.Items.Clear();
							PublicClass.m_mthShowWarning(this.m_objViewer.m_lsvOpRecDetail,"û���ҵ������������ݣ�");
						}
					}
					else
					{
						PublicClass.m_mthShowWarning(this.m_objViewer.m_lsvOpRecDetail,"��ǰʱ���û���κ����ݣ�");
					}

				}
				else
				{
					if(this.m_objViewer.lvsreture.Items.Count>0)
					{
						if(m_mthFindDataFromList(this.m_objViewer.lvsreture,this.m_objViewer.OUTPATRECIPEID.Text.Trim(),cou)==0)
						{
							this.m_objViewer.m_lsvOpRecDetail.Items.Clear();
							this.m_objViewer.m_lsvMedicineDetail.Items.Clear();
							PublicClass.m_mthShowWarning(this.m_objViewer.m_lsvOpRecDetail,"û���ҵ������������ݣ�");
						}
					}
					else
					{
						PublicClass.m_mthShowWarning(this.m_objViewer.m_lsvOpRecDetail,"��ǰʱ���û���κ����ݣ�");
					}
				}
			}
			else
			{
				PublicClass.m_mthShowWarning(this.m_objViewer.OUTPATRECIPEID,"�������ѯ������");
			}

		   }
		private long  m_mthFindDataFromList(ListView objList,string strFind,int couint)
		{
			long lngRes=0;
			for(int i1=0;i1<objList.Items.Count;i1++)
			{
				if(objList.Items[i1].SubItems[couint].Text.ToUpper()==strFind.Trim().ToUpper())
				{
					objList.Items[i1].Selected=true;
					objList.Items[i1].Checked=true;
					objList.Items[i1].Focused=true;
					objList.Items[i1].EnsureVisible();
					lngRes=1;
					return lngRes;
				}
			}
			return lngRes;
		}
		public void m_lngFillTolvs(int rowNumber)
		{
				clsOutPatienTrecipEinv_VO p_objResultArr=new clsOutPatienTrecipEinv_VO();
				p_objResultArr.m_strINVOICENO_VCHR=Fidtable.Rows[rowNumber][0].ToString().Trim();
				p_objResultArr.m_strOPREMPNAME_CHR=Fidtable.Rows[rowNumber][10].ToString().Trim();
				p_objResultArr.m_strSEQID_CHR=Fidtable.Rows[rowNumber][9].ToString().Trim();
				p_objResultArr.m_strOUTPATRECIPEID_CHR=Fidtable.Rows[rowNumber][1].ToString().Trim();
				p_objResultArr.m_strINVDATE_DAT=Fidtable.Rows[rowNumber][2].ToString().Trim();
				if(Fidtable.Rows[rowNumber][8].ToString()=="2")
				{
					
					ListViewItem lisTemp=null;
					lisTemp=new ListViewItem(p_objResultArr.m_strINVOICENO_VCHR.Trim());
					lisTemp.SubItems.Add(p_objResultArr.m_strOPREMPNAME_CHR.Trim());
					lisTemp.SubItems.Add(p_objResultArr.m_strSEQID_CHR.Trim());
					lisTemp.SubItems.Add(p_objResultArr.m_strOUTPATRECIPEID_CHR.Trim());
					lisTemp.SubItems.Add(p_objResultArr.m_strINVDATE_DAT);
					lisTemp.Tag=p_objResultArr;
					this.m_objViewer.lvsreture.Items.Add(lisTemp);
				}
				if(Fidtable.Rows[rowNumber][8].ToString()=="1")
				{
					ListViewItem lisTemp=null;
					lisTemp=new ListViewItem(p_objResultArr.m_strINVOICENO_VCHR.Trim());
					lisTemp.SubItems.Add(p_objResultArr.m_strOPREMPNAME_CHR.Trim());
					lisTemp.SubItems.Add(p_objResultArr.m_strSEQID_CHR.Trim());
					lisTemp.SubItems.Add(p_objResultArr.m_strOUTPATRECIPEID_CHR.Trim());
					lisTemp.SubItems.Add(p_objResultArr.m_strINVDATE_DAT);
					lisTemp.Tag=p_objResultArr;
					this.m_objViewer.m_lsvPatientDetial.Items.Add(lisTemp);
				}
		}

		#endregion
	}
}
