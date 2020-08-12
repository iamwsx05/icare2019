using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity; 
using System.Drawing;
//using NUnit.Framework;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedStorageInOrd ��ժҪ˵����
	/// </summary>
	public class clsControlMedStorageInOrd: com.digitalwave.GUI_Base.clsController_Base	//gui_base.dll
	{
		public clsControlMedStorageInOrd()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ���ô������
		/// <summary>
		/// �������
		/// </summary>
		frmMedStorageInOrd m_objViewer;
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objViewer = (frmMedStorageInOrd)frmMDI_Child_Base_in;
		}

		#endregion

		#region ����

		clsDomainControlStorageOrd m_objManage = new clsDomainControlStorageOrd();
		/// <summary>
		/// ����ҩƷ���ϵ����ݱ�
		/// </summary>
		private DataTable objItems=null;
		/// <summary>
		/// �������������
		/// </summary>
		clsPeriod_VO[] objPriodItems = new clsPeriod_VO[0];
		/// <summary>
		/// ���浱ǰ�����ڵ�����
		/// </summary>
		int intSelPeriod=-1;
		/// <summary>
		/// �����ϸ
		/// </summary>
		public clsStorageOrdDe_VO m_objItemDetail;
		/// <summary>
		/// �к�
		/// </summary>
		private int m_RowNo;
		/// <summary>
		/// �Ƿ�Ϊ������ϸ
		/// </summary>
		private bool m_blnIsInsert = false;
		/// <summary>
		/// ��ǰѡ����
		/// </summary>
		private int m_SelRow = 0;
		/// <summary>
		/// ����ҩ������
		/// </summary>
		private clsStorage_VO[] objStorageArr = new clsStorage_VO[0];
		/// <summary>
		///��ǰ��ѡ����ϸ�Ľ��
		/// </summary>
		private double  TolPriceDe=0;
		/// <summary>
		///������ⵥ����
		/// </summary>
		clsMedStorageOrd_VO[] p_objResultArr=null;
		/// <summary>
		///���������ⵥ����
		/// </summary>
		clsMedStorageOrd_VO[] p_objResultFind=null;
		/// <summary>
		///��־�����״̬��0��������1�޸�
		/// </summary>
		public  int intWindowState=0;
		/// <summary>
		///�ж��û����ڲ��������Ǹ��б�0����ⵥ��1�������ϸ��
		/// </summary>
		private int IntEditList=1;
		/// <summary>
		///�ж��û��ڲ��������޸���ϸ����������ϸ��1���޸ģ�0������
		/// </summary>
		public int intNewOrUpdate=0;
		/// <summary>
		///���汻�޸���ϸ���ݵ�ID
		/// </summary>
		private string updataID=null;
		/// <summary>
		/// ������ҵ���ҩƷ����
		/// </summary>
		private DataTable dtbFindMed=new DataTable();
		/// <summary>
		/// ��ʶ�Ƿ����޸Ļ�û�б��������
		/// </summary>
		bool isSave=false;
		clsPublicParm publicClass=new clsPublicParm();
		/// <summary>
		/// ��־λ����־�û��Ƿ���������������1-�У�0��
		/// </summary>
		public int isModidy=0;
		/// <summary>
		/// ���ݺſ�ͷ��ʶ
		/// </summary>
		public string strDocStar="";
        /// <summary>
        /// ���浱ǰ�༭�ĵ��ݺŵ�ID
        /// </summary>
        string strOrderID = "";
		#endregion

		#region �����ʼ��
		/// <summary>
		/// �����ʼ��
		/// </summary>
		DataTable dtType=new DataTable();
		public void m_mthInit()
		{
			m_mthGetPeriodList();
			isSave=false;
			#region ����Ҽ��˵�
			
			m_objManage.m_lngGetOutOrdType(out dtType,"2");
			if(dtType.Rows.Count>0)
			{
				for(int i1=0;i1<dtType.Rows.Count;i1++)
				{
					this.m_objViewer.contextMenu1.MenuItems.Add("����"+dtType.Rows[i1]["STORAGEORDTYPENAME_VCHR"].ToString());
					this.m_objViewer.contextMenu1.MenuItems[i1].Click+=new EventHandler(clsControlMedStorageInOrd_Click);
				}
			}
			#endregion
		}
		#endregion

		#region �Ҽ��˵�ѡ���¼�
		public void m_mthMenuSele()
		{
			
		}
		#endregion

		#region ���������Ƿ񱻸ı�
		/// <summary>
		/// ���������Ƿ񱻸ı�
		/// </summary>
		public void m_mthIsSave()
		{

			isSave=true;
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
				int intWindowState=0;
				for(int i1=0;i1<objPriodItems.Length;i1++)
				{
					this.m_objViewer.m_cboSelPeriod.Items.Insert(i1,objPriodItems[i1].m_strStartDate + " �� " + objPriodItems[i1].m_strEndDate);
					if(Convert.ToDateTime(nowdate)>=Convert.ToDateTime(objPriodItems[i1].m_strStartDate)&&Convert.ToDateTime(nowdate)<=Convert.ToDateTime(objPriodItems[i1].m_strEndDate))
					{
						intSelPeriod = i1;
						this.m_objViewer.m_cboSelPeriod.Tag = objPriodItems[i1].m_strPeriodID;
					}
					intWindowState=i1;
				}
				this.m_objViewer.m_cboSelPeriod.Items.Insert(intWindowState+1,"���в����ڵ�����");
				if(intSelPeriod!=-1)
				{
					m_objViewer.m_cboSelPeriod.SelectedIndex = intSelPeriod;
				}
				else
				{
					MessageBox.Show("��û�г�ʼ��������,�������ò�����!","ϵͳ��ʾ");
				}

			}
			this.m_objViewer.m_ctlInMedOrd.getPeriod=objPriodItems;
		}
		#endregion

		#region ������ѡ���¼�
		/// <summary>
		/// ������ѡ���¼�
		/// </summary>
		public void m_lngPriodchang()
		{
            this.m_objViewer.m_lsvUnAduit.Items.Clear();
            this.m_objViewer.m_lsvEnAduit.Items.Clear();
			if(this.m_objViewer.m_cboSelPeriod.Text!="���в����ڵ�����")
			{
				m_lngFillToLSV(objPriodItems[this.m_objViewer.m_cboSelPeriod.SelectedIndex].m_strPeriodID);
				this.m_objViewer.m_cboSelPeriod.Tag=objPriodItems[this.m_objViewer.m_cboSelPeriod.SelectedIndex].m_strPeriodID;
			}
			else
			{
				m_lngFillToLSV("");
				this.m_objViewer.m_cboSelPeriod.Tag="";
				return;
			}
			if(clsPublicParm.m_EstimatePeriod(objPriodItems[this.m_objViewer.m_cboSelPeriod.SelectedIndex].m_strPeriodID))
			{
				this.m_objViewer.panel2.Enabled=false;
				this.m_objViewer.panel3.Enabled=false;
				this.m_objViewer.m_btnNew.Enabled=false;
				this.m_objViewer.btnSave.Enabled=false;
				this.m_objViewer.btnDelect.Enabled=false;
				this.m_objViewer.dntEmp.Enabled=false;
			}
			else if(this.m_objViewer.m_tabAduit.SelectedIndex==0)
			{
				this.m_objViewer.panel2.Enabled=true;
				this.m_objViewer.panel3.Enabled=true;
				this.m_objViewer.m_btnNew.Enabled=true;
				this.m_objViewer.btnSave.Enabled=true;
				this.m_objViewer.btnDelect.Enabled=true;
				this.m_objViewer.dntEmp.Enabled=true;
			}
            m_lngFrmReset();
		}
		#endregion

		#region ȷ����ť�¼�
		/// <summary>
		/// ȷ����ť
		/// </summary>
		public void m_mthOkButtonClick()
		{
            if (intNewOrUpdate == 0 && intWindowState == 1)
			{
				if(MessageBox.Show("��ȷ��Ҫ�����ⵥ�����ϸ������","��ʾ",System.Windows.Forms.MessageBoxButtons.YesNo,MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.No)
					return;
				clsMedStorageOrdDe_VO objItem=this.m_objViewer.m_ctlInMed.ordDe;
                objItem.m_strSTORAGEORDID_CHR = strOrderID;
				objItem.m_strSTORAGEORDTYPEID_CHR=this.m_objViewer.m_ctlInMedOrd.strGetOrdTypeID;
				double tolMoney;
				long lngRes=m_objManage.m_lngInsertMetStorageOrdDe(objItem,out tolMoney);
                if (lngRes > 0)
                {
                    m_mthInsertDetailList(objItem);
                    if (this.m_objViewer.m_lsvUnAduit.SelectedItems.Count > 0)
                        this.m_objViewer.m_lsvUnAduit.SelectedItems[0].SubItems[6].Text = tolMoney.ToString("");
                    m_lngCountTol();
                    publicClass.m_mthShowWarning(this.m_objViewer.panel2, "������ݳɹ���");
                }
                else
                {
                    publicClass.m_mthShowWarning(this.m_objViewer.panel2, "�������ʧ�ܣ�");
                }
				this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.m_lsvDetail.Items.Count-1].Selected=true;
				this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.m_lsvDetail.Items.Count-1].EnsureVisible();
				this.m_objViewer.m_ctlInMed.m_mthClear();
                this.m_objViewer.m_ctlInMed.Focus();
                intNewOrUpdate = 0;
                return;
			}
            if (intNewOrUpdate == 1 && intWindowState == 0)
			{
				clsMedStorageOrdDe_VO objUpdata=this.m_objViewer.m_ctlInMed.ordDe;
				m_mthModifyGUI(objUpdata);
				this.m_objViewer.m_ctlInMed.m_mthClear();
				this.m_objViewer.m_ctlInMed.Focus();
                intNewOrUpdate = 0;
                return;
			}

            if (intNewOrUpdate == 1 && intWindowState == 1)
			{
				clsMedStorageOrdDe_VO objUpdata=this.m_objViewer.m_ctlInMed.ordDe;
				objUpdata.m_strSTORAGEORDDEID_CHR=updataID;
				string strOrdID=(string)this.m_objViewer.m_ctlInMedOrd.txtDoc.Tag;
				double tolmoney=0;
				long lngRes=this.m_objManage.m_lngDoUpdateOrdAndDe(strOrdID,objUpdata,out tolmoney);
				if(lngRes>0)
				{
					m_mthModifyGUI(objUpdata);
                    this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].SubItems[6].Text = tolmoney.ToString();
                    clsMedStorageOrd_VO modifyEnd = (clsMedStorageOrd_VO)this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].Tag;
					modifyEnd.m_dblTOLMNY_MNY=tolmoney;
                    this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].Tag = modifyEnd;
					publicClass.m_mthShowWarning(this.m_objViewer.panel2,"�޸���ϸ�ɹ���");
				}
				this.m_objViewer.m_ctlInMed.m_mthClear();
				this.m_objViewer.m_ctlInMed.Focus();
                intNewOrUpdate = 0;
                return;
			}

            if (intNewOrUpdate == 0 && intWindowState == 0)
			{
				m_RowNo=this.m_objViewer.m_lsvDetail.Items.Count;
				m_mthAddDetail();
				this.m_objViewer.m_ctlInMed.Focus();
				this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.m_lsvDetail.Items.Count-1].Selected=true;
				this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.m_lsvDetail.Items.Count-1].EnsureVisible();
				this.m_objViewer.m_ctlInMed.m_mthClear();
				this.m_objViewer.m_ctlInMed.Focus();
                intNewOrUpdate = 0;
			}
            
		}
		#endregion

		#region ����к�
		/// <summary>
		/// ����к�
		/// </summary>
		/// <returns></returns>
		private string m_mthGetRowNo()
		{
			string strResult = "";
			++ m_RowNo;
			strResult = m_RowNo.ToString("0000");
			return strResult;
		}
		#endregion

		#region ������ϸ
		/// <summary>
		/// ������ϸ
		/// </summary>
		private void m_mthAddDetail()
		{
			clsMedStorageOrdDe_VO objItem =null;
			objItem=this.m_objViewer.m_ctlInMed.ordDe;
			objItem.m_strROWNO_CHR = m_mthGetRowNo();
			m_mthInsertDetailList(objItem);
			this.m_objViewer.m_ctlInMed.m_mthClear();
			m_lngCountTol();
		}
		#endregion

		#region �����ܽ��
		/// <summary>
		/// �����ܽ��
		/// </summary>
		private void m_lngCountTol()
		{
			//�����ۺϼ�
			double TotailMoney1=0;
			//�����ۺϼ�
			double TotailMoney2=0;
			//���ۼۺϼ�
			double TotailMoney3=0;
			//���ۺϼ�
			double TotailMoney4=0;
			if(this.m_objViewer.m_lsvDetail.Items.Count>0)
			{
				clsMedStorageOrdDe_VO objItem;
				for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;i1++)
				{
					objItem=new clsMedStorageOrdDe_VO();
					objItem=(clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
					TotailMoney1+=double.Parse(objItem.m_strORDERQTY_DEC)*double.Parse(objItem.m_strORDERUNITPRICE_MNY);
					TotailMoney2+=objItem.m_dblWHOLESALEUNITPRICE_MNY*double.Parse(objItem.m_strORDERQTY_DEC);
					TotailMoney3+=objItem.m_dblSALEUNITPRICE_MNY*objItem.m_dblQTY_DEC;
				}
			}
			TotailMoney4=TotailMoney3-TotailMoney1;
			this.m_objViewer.m_ctlInMedOrd.m_LabTotailPACKAGEPRICE.Text=TotailMoney1.ToString("0.0000");
			this.m_objViewer.m_ctlInMedOrd.m_LabTotailTradePrice.Text=TotailMoney2.ToString("0.0000");
			this.m_objViewer.m_ctlInMedOrd.m_LabToltailTretailPrice.Text=TotailMoney3.ToString("0.0000");
			this.m_objViewer.m_ctlInMedOrd.m_LabTotailPriceDifference.Text=TotailMoney4.ToString("0.0000");
		}
		#endregion

		#region ����ϸ�б��м����µ�����
		/// <summary>
		/// ����ϸ�б��м����µ�����
		/// </summary>
		/// <param name="objItem"></param>
		private void m_mthInsertDetailList(clsMedStorageOrdDe_VO objItem)
		{
			if(objItem != null)
			{
				if(m_blnIsInsert)
				{
					int intRowNoCount = m_objViewer.m_lsvDetail.Items.Count;
					m_mthChangeRowNo(this.m_SelRow,intRowNoCount,true);
				}
				System.Windows.Forms.ListViewItem lsvItem = new System.Windows.Forms.ListViewItem(objItem.m_strROWNO_CHR);
				lsvItem.SubItems.Add(objItem.m_strASSISTCODE_CHR);
				lsvItem.SubItems.Add(objItem.m_strMEDICINENAME_CHR);
				lsvItem.SubItems.Add(objItem.m_strspec);
				lsvItem.SubItems.Add(objItem.m_strPRODCUTORNAME_CHR);
				lsvItem.SubItems.Add(objItem.m_strORDERQTY_DEC);
				lsvItem.SubItems.Add(objItem.m_strORDERUNIT_VCHR);
				lsvItem.SubItems.Add(objItem.m_strORDERUNITPRICE_MNY);
				
				double dblbuymony=objItem.m_dblBUYUNITPRICE_MNY;
				lsvItem.SubItems.Add(dblbuymony.ToString());
				double dblSalePrice = objItem.m_dblQTY_DEC;
				lsvItem.SubItems.Add(dblSalePrice.ToString());
				lsvItem.SubItems.Add(objItem.m_strUNITID_CHR);

				lsvItem.SubItems.Add(objItem.m_strAIMUNITPRICE_MNY);
				lsvItem.SubItems.Add(objItem.m_strLIMITUNITPRICE_MNY);
				lsvItem.SubItems.Add(objItem.m_dblWHOLESALEUNITPRICE_MNY.ToString());
				lsvItem.SubItems.Add(objItem.m_dblSALEUNITPRICE_MNY.ToString());
				lsvItem.SubItems.Add(objItem.m_intStorage.ToString());
				lsvItem.SubItems.Add(objItem.m_strORDERPKGQTY_DEC);
				double dblTolBuyPrice = objItem.m_dblBUYTOLPRICE_MNY;
				lsvItem.SubItems.Add(dblTolBuyPrice.ToString());
				if(objItem.m_strUSEFULLIFE_DAT!=null&&objItem.m_strUSEFULLIFE_DAT!="")
				    lsvItem.SubItems.Add(Convert.ToDateTime(objItem.m_strUSEFULLIFE_DAT).ToShortDateString());
				else
					lsvItem.SubItems.Add("");
				lsvItem.SubItems.Add(objItem.m_strLOTNO_VCHR);
				lsvItem.SubItems.Add(objItem.m_strINVOICENO_VCHR);
				lsvItem.SubItems.Add(objItem.m_strPRODCUTORID_CHR);
				lsvItem.Tag = objItem;
				this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);
			}
		}
		#endregion

		#region ����ϸ�б��в����µ�����
		/// <summary>
		/// ����ϸ�б��в����µ�����
		/// </summary>
		/// <param name="objItem"></param>
		private void m_mthInsertDetailList(clsMedStorageOrdDe_VO objItem,int insertIndex)
		{
			if(objItem != null)
			{
				if(m_blnIsInsert)
				{
					int intRowNoCount = m_objViewer.m_lsvDetail.Items.Count;
					m_mthChangeRowNo(this.m_SelRow,intRowNoCount,true);
				}
				System.Windows.Forms.ListViewItem lsvItem = new System.Windows.Forms.ListViewItem(objItem.m_strROWNO_CHR);
				lsvItem.SubItems.Add(objItem.m_strASSISTCODE_CHR);
				lsvItem.SubItems.Add(objItem.m_strMEDICINENAME_CHR);
				lsvItem.SubItems.Add(objItem.m_strspec);
				lsvItem.SubItems.Add(objItem.m_strPRODCUTORNAME_CHR);

				lsvItem.SubItems.Add(objItem.m_strORDERQTY_DEC);
				lsvItem.SubItems.Add(objItem.m_strORDERUNIT_VCHR);
				lsvItem.SubItems.Add(objItem.m_strORDERUNITPRICE_MNY);
				lsvItem.SubItems.Add(objItem.m_strORDERPKGQTY_DEC);


				double dblbuymony=objItem.m_dblBUYUNITPRICE_MNY;
				lsvItem.SubItems.Add(dblbuymony.ToString());
				double dblSalePrice = objItem.m_dblQTY_DEC;
				lsvItem.SubItems.Add(dblSalePrice.ToString());
				lsvItem.SubItems.Add(objItem.m_strUNITID_CHR);
				double dblTolBuyPrice = objItem.m_dblBUYTOLPRICE_MNY;
				lsvItem.SubItems.Add(dblTolBuyPrice.ToString());
				lsvItem.SubItems.Add(objItem.m_intStorage.ToString("0.00"));
				if(objItem.m_strUSEFULLIFE_DAT!=null&&objItem.m_strUSEFULLIFE_DAT!="")
					lsvItem.SubItems.Add(Convert.ToDateTime(objItem.m_strUSEFULLIFE_DAT).ToShortDateString());
				else
					lsvItem.SubItems.Add("");
				lsvItem.SubItems.Add(objItem.m_strLOTNO_VCHR);
				lsvItem.SubItems.Add(objItem.m_strINVOICENO_VCHR);
				lsvItem.SubItems.Add(objItem.m_strPRODCUTORID_CHR);
				lsvItem.Tag = objItem;
				this.m_objViewer.m_lsvDetail.Items.Insert(insertIndex,lsvItem);
			}
		}
		#endregion

		#region ����ĳ���б��е��к�
		/// <summary>
		/// ����ĳ���б��е��к�
		/// </summary>
		/// <param name="intStart">��ʼ��</param>
		/// <param name="intEnd">ĩβ��</param>
		/// <param name="blnAdd">���Ż����</param>
		private void m_mthChangeRowNo(int intStart,int intEnd,bool blnAdd)
		{
			for(int i=intStart;i<intEnd;i++)
			{
				string strRowNo = m_objViewer.m_lsvDetail.Items[i].Text;
				int intRowNo = int.Parse(strRowNo);

				if(blnAdd)
				{
					++ intRowNo;
				}
				else
				{
					-- intRowNo;
				}
				m_objViewer.m_lsvDetail.Items[i].Text = intRowNo.ToString("0000");
			}
		}
		#endregion

		#region ˢ������
		/// <summary>
		/// ˢ������
		/// </summary>
		public void m_mthResetData()
		{
			clsMedStorageOrdDe_VO[] listItem=m_GetDeData();
			this.m_objViewer.m_ctlInMed.intIsReData=1;
			this.m_objViewer.m_ctlInMedOrd.m_mthFillVENDOR();
			if(listItem.Length>0)
			{
				long lngRes=0;
				if(intWindowState==0)
				{
					lngRes=m_objManage.m_lonResetData(ref listItem,null);
				}
				else
				{
					lngRes=m_objManage.m_lonResetData(ref listItem,(string)this.m_objViewer.m_ctlInMedOrd.txtDoc.Tag);
				}
				if(lngRes==1)
				{
					for(int i1=0;i1<listItem.Length;i1++)
					{
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[4].Text=listItem[i1].m_strPRODCUTORID_CHR;
						this.m_objViewer.m_lsvDetail.Items[i1].Tag=listItem[i1];
					}
				}
			}
		}
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public void m_mthSave()
		{
			if(intWindowState==0)
			{
				if(this.m_objViewer.m_lsvDetail.Items.Count >0)
				{
					long lngRes = 0;
					lngRes = m_lngDoAddNewOrd();
					if(lngRes ==1)
					{
						m_lngFrmReset();
						this.m_objViewer.m_lsvDetail.Items.Clear();
						this.m_objViewer.m_lsvUnAduit.Items.Clear();
						this.m_objViewer.m_lsvEnAduit.Items.Clear();
						m_lngFillToLSV(objPriodItems[this.m_objViewer.m_cboSelPeriod.SelectedIndex].m_strPeriodID);
					}
				}
				else
				{
					publicClass.m_mthShowWarning(this.m_objViewer.m_lsvDetail,"��ϸ�б��������ݣ��������棡");
					this.m_objViewer.m_ctlInMed.Focus();
				}
			}
			else
			{
				m_lngUpdata();
				publicClass.m_mthShowWarning(this.m_objViewer.panel2,"�޸ĵ��ݳɹ���");
			}
				isSave=false;
				
		}
		#endregion

		#region ��ȡ���е���ϸ����
		/// <summary>
		/// ��ȡ���е���ϸ����
		/// </summary>
		/// <returns></returns>
		public clsMedStorageOrdDe_VO[] m_GetDeData()
		{
			clsMedStorageOrdDe_VO[] listItem=new clsMedStorageOrdDe_VO[this.m_objViewer.m_lsvDetail.Items.Count];
			if(this.m_objViewer.m_lsvDetail.Items.Count>0)
			{
				for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;i1++)
				{
					listItem[i1]=new clsMedStorageOrdDe_VO();
					listItem[i1]=(clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
					listItem[i1].m_strORD_DAT=clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss");
					listItem[i1].m_strSTORAGEORDTYPEID_CHR=this.m_objViewer.m_ctlInMedOrd.strGetOrdTypeID;
				}
			}
			return listItem;
		}

		#endregion

		#region ��������¼ 
		/// <summary>
		/// ��������¼
		/// </summary>
		/// <returns></returns>
		private long m_lngDoAddNewOrd()
		{
			long lngRes = 0;
			clsMedStorageOrd_VO objItem = new clsMedStorageOrd_VO();
			objItem = this.m_objViewer.m_ctlInMedOrd.m_objGetOrdInfo();
            if (objItem != null)
            {
                clsMedStorageOrdDe_VO[] listItem = m_GetDeData();
                string newID;
                lngRes = this.m_objManage.m_lngInsertMetStorageOrd(objItem, listItem, out newID, true,this.m_objViewer.intDept==1?3:1);
                if (lngRes == -2)
                {
                    publicClass.m_mthShowWarning(this.m_objViewer.m_ctlInMedOrd.txtDocEnd, "���޸ĵ��ݺ�");
                    this.m_objViewer.m_ctlInMedOrd.txtDocEnd.Focus();
                    return -2;
                }
                if (lngRes != 1)
                {
                    publicClass.m_mthShowWarning(this.m_objViewer.panel3, "������ⵥ����ʧ�ܣ�");
                }
            }
			return lngRes;
		}
		#endregion

		#region ����ҩƷ��������
		public double m_mthCountPrice(double UNITPRICE,double QTY)
		{
			return UNITPRICE/QTY;
		}
		#endregion

		#region ����ҩƷ��������
		public double m_mthCountQty(double ORDERQTY,double QTY)
		{
			return ORDERQTY*QTY;
		}
		#endregion

		#region ��ӡ
		public void m_mthPrint()
		{
//			if(this.m_objViewer.m_lsvDetail.Items.Count>0)
//			{
//				DataTable dtPrint=new DataTable();
//				dtPrint.Columns.Add("ASSISTCODE_CHR");
//				dtPrint.Columns.Add("MEDICINENAME_VCHR");
//				dtPrint.Columns.Add("MEDSPEC_VCHR");
//				dtPrint.Columns.Add("LOTNO_VCHR");
//				dtPrint.Columns.Add("QTY_DEC");
//				dtPrint.Columns.Add("UNITID_CHR");
//				dtPrint.Columns.Add("BUYUNITPRICE_MNY");
//				dtPrint.Columns.Add("BUYTOLPRICE_MNY");
//				dtPrint.Columns.Add("PRODUCTORID_CHR");
//				clsMedStorageOrdDe_VO ordDeVO=new clsMedStorageOrdDe_VO();
//				for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;i1++)
//				{
//					ordDeVO=(clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
//					DataRow newRow=dtPrint.NewRow();
//					newRow["ASSISTCODE_CHR"]=ordDeVO.m_strASSISTCODE_CHR;
//					newRow["MEDICINENAME_VCHR"]=ordDeVO.m_strMEDICINENAME_CHR;
//					newRow["MEDSPEC_VCHR"]=ordDeVO.m_strspec;
//					newRow["LOTNO_VCHR"]=ordDeVO.m_strLOTNO_VCHR;
//					newRow["QTY_DEC"]=ordDeVO.m_strORDERQTY_DEC;
//					newRow["UNITID_CHR"]=ordDeVO.m_strORDERUNIT_VCHR;
//					newRow["BUYUNITPRICE_MNY"]=Decimal.Parse(ordDeVO.m_strORDERUNITPRICE_MNY);
//					newRow["BUYTOLPRICE_MNY"]=Decimal.Parse(ordDeVO.m_dblBUYTOLPRICE_MNY.ToString());
//					newRow["PRODUCTORID_CHR"]=ordDeVO.m_strPRODCUTORNAME_CHR;
//					dtPrint.Rows.Add(newRow);

//				}
//				com.digitalwave.iCare.gui.HIS.baotable.OrdInReport Report=new com.digitalwave.iCare.gui.HIS.baotable.OrdInReport();
//				Report.SetDataSource(dtPrint);
//				((TextObject)Report.ReportDefinition.ReportObjects["ReportTitl"]).Text = this.m_objComInfo.m_strGetHospitalTitle()+"ҩ����ⵥ";
//				((TextObject)Report.ReportDefinition.ReportObjects["docNo"]).Text = this.m_objViewer.m_ctlInMedOrd.txtDoc.Text.Trim()+this.m_objViewer.m_ctlInMedOrd.txtDocEnd.Text;
//				((TextObject)Report.ReportDefinition.ReportObjects["VerName"]).Text = this.m_objViewer.m_ctlInMedOrd.textVENDOR.txtValuse.Trim();
////				((TextObject)Report.ReportDefinition.ReportObjects["Text27"]).Text = this.m_objViewer.m_ctlInMedOrd.m_cobStorage.;
//				((TextObject)Report.ReportDefinition.ReportObjects["Text7"]).Text = DateTime.Parse(this.m_objViewer.m_ctlInMedOrd.m_inOrdDate.Text).ToString("yyyy-MM-dd");
//				((TextObject)Report.ReportDefinition.ReportObjects["TotailMoney"]).Text = this.m_objViewer.m_ctlInMedOrd.m_LabTotailPACKAGEPRICE.Text.Trim()+"Ԫ";
//				((TextObject)Report.ReportDefinition.ReportObjects["CreageName"]).Text =this.m_objViewer.m_ctlInMedOrd.strGetLoginName;
//				((TextObject)Report.ReportDefinition.ReportObjects["txtType"]).Text = this.m_objViewer.m_ctlInMedOrd.strGetOrdType;
//                //com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
//				((TextObject)Report.ReportDefinition.ReportObjects["printDateTime"]).Text = (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate().ToString("yyyy-MM-dd");
				
//				((TextObject)Report.ReportDefinition.ReportObjects["ADUITEMPName"]).Text = (string)this.m_objViewer.m_ctlInMedOrd.m_txtMemo.Tag;
//				frmShowReport ShowPrint=new frmShowReport();
//				ShowPrint.crystalReportViewer1.ReportSource=Report;
//				ShowPrint.ShowDialog();
//			}

		}
		#endregion

		#region ����ⵥ��ӵ���ⵥδ����б�
		/// <summary>
		/// ����ⵥ��ӵ���ⵥδ����б�
		/// </summary>
		/// <param name="objResArr"></param>
		private void m_lngFillStorageOrdList(clsMedStorageOrd_VO objResArr)
		{
			ListViewItem lisTemp=null;
			lisTemp=new ListViewItem(objResArr.m_strDOCID_VCHR);
			lisTemp.SubItems.Add(objResArr.m_strSTORAGEORDTYPENAME_CHR);
			lisTemp.SubItems.Add(objResArr.m_strCREATORNAME_CHR);
			lisTemp.SubItems.Add(objResArr.m_strINORD_DAT);
			lisTemp.SubItems.Add(objResArr.m_strADUITEMPNAME_CHR);
			if(this.m_objViewer.intDept==0)
			{
				lisTemp.SubItems.Add(objResArr.m_strVENDORNAME_CHR);
			}
			else
			{
				lisTemp.SubItems.Add(objResArr.m_strDEPTNAME_CHR);
			}
			lisTemp.SubItems.Add(objResArr.m_dblTOLMNY_MNY.ToString());
			lisTemp.Tag=objResArr;
			this.m_objViewer.m_lsvUnAduit.Items.Add(lisTemp);
		}
		#endregion

		#region ����ⵥ��ӵ���ⵥ����б�
		/// <summary>
		/// ����ⵥ��ӵ���ⵥ����б�
		/// </summary>
		/// <param name="objResArr"></param>
		private void m_lngFillStorageOrdOK(clsMedStorageOrd_VO objResArr)
		{
			ListViewItem lisTemp=null;
			lisTemp=new ListViewItem(objResArr.m_strDOCID_VCHR);
			lisTemp.SubItems.Add(objResArr.m_strSTORAGEORDTYPENAME_CHR);
			lisTemp.SubItems.Add(objResArr.m_strCREATORNAME_CHR);
			lisTemp.SubItems.Add(objResArr.m_strINORD_DAT);
			lisTemp.SubItems.Add(objResArr.m_strADUITEMPNAME_CHR);
			lisTemp.SubItems.Add(objResArr.m_strVENDORNAME_CHR);
			lisTemp.SubItems.Add(objResArr.m_dblTOLMNY_MNY.ToString());
            lisTemp.SubItems.Add(objResArr.m_strADUITDATE_DAT);
			lisTemp.Tag=objResArr;
			this.m_objViewer.m_lsvEnAduit.Items.Add(lisTemp);
		}
		#endregion

		#region ������ⵥ���ֱ�󶨵�����ˡ�����δ��ˡ��б�
		/// <summary>
		/// ������ⵥ���ֱ�󶨵�����ˡ�����δ��ˡ��б�
		/// </summary>
		private void m_lngFillToLSV(string priod)
		{
			long lngRes=m_objManage.m_lngGetStorageOrdList(out p_objResultArr,priod,this.m_objViewer.m_ctlInMedOrd.strGetOrdTypeID);
			if(lngRes>0&&p_objResultArr!=null)
			{
				for(int i1=0;i1<p_objResultArr.Length;i1++)
				{
					if(p_objResultArr[i1].m_intPSTATUS_INT==1)
					{
						m_lngFillStorageOrdList(p_objResultArr[i1]);
					}
					else
					{
						m_lngFillStorageOrdOK(p_objResultArr[i1]);
					}
				}
			}
		}
		#endregion

		#region ��ⵥ�б��¼�
		/// <summary>
		/// ��ⵥ�б���¼�
		/// </summary>
		public void m_lngLisvSelect()
		{
			if(isSave==true)
			{
				if(MessageBox.Show("�Ƿ񱣴浥��!","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
				{
					if(intWindowState==1)
					{
						m_mthSave();
					}
					else
					{
						m_mthOkButtonClick();
						m_mthSave();
					}
				}
			}
			m_mthChengRowNO();
            intNewOrUpdate = 0;
			intWindowState=1;
            this.m_objViewer.m_ctlInMed.blIsNewAdd = false;
			IntEditList=0;
			updataID=null;
			clsMedStorageOrdDe_VO[] SeleItemDe=new clsMedStorageOrdDe_VO[0];
			clsMedStorageOrd_VO SeleItem=new clsMedStorageOrd_VO();
			if(this.m_objViewer.m_lsvUnAduit.SelectedItems.Count==0)
				return;
			SeleItem=(clsMedStorageOrd_VO)this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Tag;
            strOrderID = SeleItem.m_strSTORAGEORDID_CHR;
			m_objManage.m_lngGetMedStorageOrdDe(SeleItem.m_strSTORAGEORDID_CHR,out SeleItemDe);
			this.m_objViewer.m_ctlInMedOrd.m_mthFillToText(SeleItem);
			this.m_objViewer.m_lsvDetail.Items.Clear();
			for(int i1=0;i1<SeleItemDe.Length;i1++)
			{
				m_mthInsertDetailList(SeleItemDe[i1]);
			}
			m_lngCountTol();
            this.m_objViewer.btnSave.Text = "�޸�(&S)";
			isSave=false;
			this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Index].BackColor=System.Drawing.Color.PapayaWhip;
			isModidy=0;
			this.m_objViewer.m_ctlInMed.m_mthClear();
		}
		#endregion

		#region ��ⵥ�б��¼�(����˴��ڣ�
		/// <summary>
		/// ��ⵥ�б���¼�
		/// </summary>
		public void m_lngLisvEMP()
		{
			clsMedStorageOrdDe_VO[] SeleItemDe=new clsMedStorageOrdDe_VO[0];
			clsMedStorageOrd_VO SeleItem=new clsMedStorageOrd_VO();
			if(this.m_objViewer.m_lsvEnAduit.SelectedItems.Count>0)
			{
				SeleItem=(clsMedStorageOrd_VO)this.m_objViewer.m_lsvEnAduit.SelectedItems[0].Tag;
				m_objManage.m_lngGetMedStorageOrdDe(SeleItem.m_strSTORAGEORDID_CHR,out SeleItemDe);
				this.m_objViewer.m_ctlInMedOrd.m_mthFillToText(SeleItem);
				this.m_objViewer.m_lsvDetail.Items.Clear();
				for(int i1=0;i1<SeleItemDe.Length;i1++)
				{
					m_mthInsertDetailList(SeleItemDe[i1]);
				}
				m_lngCountTol();
				this.m_objViewer.m_ctlInMed.m_mthClear();
			}

		}
		#endregion

		#region ��ʾ�۸��������ⵥ
		/// <summary>
		/// ��ʾ�۸��������ⵥ
		/// </summary>
		public void m_mthShow()
		{
			this.m_objViewer.listView1.Items.Clear();
			this.m_objViewer.listView1.Visible=false;
			if(this.m_objViewer.m_lsvDetail.Items.Count>0&&this.m_objViewer.m_ctlInMedOrd.m_txtSTORAGEGROSSPROFIT.Text!="")
			{
				clsMedStorageOrdDe_VO objOrdDe=null;
				for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;i1++)
				{
					objOrdDe=(clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
					double kk=double.Parse(objOrdDe.m_strORDERUNITPRICE_MNY)*(1+double.Parse(this.m_objViewer.m_ctlInMedOrd.m_txtSTORAGEGROSSPROFIT.Text)/100);
					if(objOrdDe.m_dblSALEUNITPRICE_MNY.ToString("0.00")!=kk.ToString("0.00"))
					{
						ListViewItem AddItem=new ListViewItem(objOrdDe.m_strMEDICINENAME_CHR);
						AddItem.SubItems.Add(objOrdDe.m_strORDERUNITPRICE_MNY);
						AddItem.SubItems.Add(objOrdDe.m_dblSALEUNITPRICE_MNY.ToString());
						this.m_objViewer.listView1.Items.Add(AddItem);
					}

				}
				if(this.m_objViewer.listView1.Items.Count>0)
					this.m_objViewer.listView1.Visible=true;
			}

		}
		#endregion

		#region ��ⵥ��ϸ�б��¼�
		/// <summary>
		/// ��ⵥ��ϸ�б��¼�
		/// </summary>
		public void m_lngLisvSelectOfDe()
		{
			if(this.m_objViewer.m_lsvDetail.SelectedItems.Count==0)
				return;
			clsMedStorageOrdDe_VO SeleItemDe=new clsMedStorageOrdDe_VO();
			SeleItemDe=(clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;
            this.m_objViewer.m_ctlInMed.blIsNewAdd = false;
			this.m_objViewer.m_ctlInMed.m_fillToFrom(SeleItemDe);
            intNewOrUpdate = 1;
			updataID=SeleItemDe.m_strSTORAGEORDDEID_CHR;
			this.m_objViewer.m_ctlInMed.blIsNewAdd=false;
			TolPriceDe=SeleItemDe.m_dblBUYTOLPRICE_MNY;
			if(intWindowState==1)
			{
				IntEditList=1;
				this.m_objViewer.m_ctlInMed.btnAdd.Text="�޸�(&A)";
				this.m_objViewer.m_ctlInMed.btnClear.Enabled=false;
				isSave=false;
			}
			else
			{
				this.m_objViewer.m_ctlInMed.btnAdd.Text="�޸�(&A)";
				this.m_objViewer.m_ctlInMed.btnClear.Enabled=true;
			}
		}
		#endregion

		#region ɾ�������¼�
		/// <summary>
		/// ɾ�������¼�
		/// </summary>
		public void m_lngDele()
		{
			if(intWindowState==0&&IntEditList==1&&this.m_objViewer.m_lsvDetail.Items.Count>0)
			{
				this.m_objViewer.m_lsvDetail.Items.RemoveAt(this.m_objViewer.m_lsvDetail.SelectedItems[0].Index);
				this.m_objViewer.btnSave.Text="����(&S)";
				m_lngCountTol();
				return;
			}
			if(intWindowState==1&&IntEditList==1)
			{
                if (this.m_objViewer.m_lsvDetail.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("ȷ��ɾ������ϸ��", "��ʾ", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                    clsMedStorageOrdDe_VO SeleItem = new clsMedStorageOrdDe_VO();
                    SeleItem = (clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;
                    double TolMoney;
                    long lngRes = m_objManage.m_lngDeleteOrdDeBy(SeleItem.m_strSTORAGEORDDEID_CHR,out TolMoney);
                    if (lngRes > 0)
                    {
                        if (this.m_objViewer.m_lsvUnAduit.SelectedItems.Count > 0)
                        {
                            this.m_objViewer.m_lsvUnAduit.SelectedItems[0].SubItems[6].Text = TolMoney.ToString("####.00");
                            p_objResultArr[this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Index].m_dblTOLMNY_MNY = TolMoney;
                            this.m_objViewer.m_lsvDetail.Items.RemoveAt(this.m_objViewer.m_lsvDetail.SelectedItems[0].Index);
                        }
                        m_lngCountTol();
                        this.m_objViewer.m_ctlInMed.m_mthClear();
                        intNewOrUpdate = 0;
                    }
                    else
                    {
                        MessageBox.Show("ɾ��ʧ��!", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("��ѡ����Ҫɾ������ϸ����!", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
			}
			if(intWindowState==1&&IntEditList==0)
			{
				if(this.m_objViewer.m_lsvUnAduit.SelectedItems.Count>0)
				{
					if(MessageBox.Show("ȷ��ɾ���õ�����","Icare",System.Windows.Forms.MessageBoxButtons.YesNo,MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.No)
						return;
					clsMedStorageOrd_VO SeleItem=new clsMedStorageOrd_VO();
					SeleItem=(clsMedStorageOrd_VO)this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Tag;
					long lngRes=m_objManage.m_lngDeleStorageOrd(SeleItem.m_strSTORAGEORDID_CHR);
					if(lngRes>0)
					{
                        this.m_objViewer.m_lsvUnAduit.Items.RemoveAt(this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Index);
                        this.m_objViewer.m_lsvDetail.Items.Clear();
						MessageBox.Show("ɾ�����ݳɹ�","Icare",MessageBoxButtons.OK,MessageBoxIcon.Information);
						m_lngFrmReset();
					}
				}
				else
                {
					MessageBox.Show("û�п�ɾ������ⵥ","Icare",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                }
			}
		}
		#endregion

		#region �޸Ľ����ϵ���ϸ����
		/// <summary>
		/// �޸Ľ����ϵ���ϸ����
		/// </summary>
		/// <param name="objUpdata"></param>
		private void m_mthModifyGUI(clsMedStorageOrdDe_VO objUpdata)
		{
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].Tag=null;
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[1].Text=objUpdata.m_strASSISTCODE_CHR;
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[2].Text=objUpdata.m_strMEDICINENAME_CHR;
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[3].Text=objUpdata.m_strspec;
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[4].Text=objUpdata.m_strPRODCUTORID_CHR;

			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[5].Text=objUpdata.m_strORDERQTY_DEC;
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[6].Text=objUpdata.m_strORDERUNIT_VCHR;
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[7].Text=objUpdata.m_strORDERUNITPRICE_MNY;
			double dblSalePrice = objUpdata.m_dblQTY_DEC;
			double dblTolBuyPrice =objUpdata.m_dblBUYTOLPRICE_MNY;
			double dblbuymony=objUpdata.m_dblBUYUNITPRICE_MNY;
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[8].Text=dblbuymony.ToString("0.00");
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[9].Text=dblSalePrice.ToString();
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[10].Text=objUpdata.m_strUNITID_CHR;
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[11].Text=objUpdata.m_strAIMUNITPRICE_MNY.ToString();
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[12].Text=objUpdata.m_strLIMITUNITPRICE_MNY;
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[13].Text=objUpdata.m_dblWHOLESALEUNITPRICE_MNY.ToString();
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[14].Text=objUpdata.m_dblSALEUNITPRICE_MNY.ToString();


			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[15].Text=objUpdata.m_intStorage.ToString();
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[16].Text=objUpdata.m_strORDERPKGQTY_DEC;
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[17].Text=dblTolBuyPrice.ToString("0.00");
			if(objUpdata.m_strUSEFULLIFE_DAT!=null&&objUpdata.m_strUSEFULLIFE_DAT!="")
				this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[18].Text=DateTime.Parse(objUpdata.m_strUSEFULLIFE_DAT).ToString("yyyy-MM-dd");
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[19].Text=objUpdata.m_strLOTNO_VCHR;
			this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].SubItems[20].Text=objUpdata.m_strPRODCUTORID_CHR;
            this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.intSeleteDe].Tag = objUpdata;
		}
		#endregion

		#region �޸Ľ������ⵥ����
		/// <summary>
		/// �޸Ľ������ⵥ����
		/// </summary>
		/// <param name="Updata"></param>
		private void m_mthOrdModifyGUI(clsMedStorageOrd_VO Updata,double tolmoney)
		{
			this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].Tag=null;
			this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].SubItems[0].Text=Updata.m_strDOCID_VCHR;
			this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].SubItems[1].Text=Updata.m_strSTORAGEORDTYPENAME_CHR;
			this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].SubItems[2].Text=Updata.m_strCREATORNAME_CHR;
//			this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].SubItems[3].Text=Updata.m_strCREATEDATE_DAT;
			this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].SubItems[4].Text=Updata.m_strADUITEMPNAME_CHR;
			this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].SubItems[5].Text=Updata.m_strVENDORNAME_CHR;
			if(tolmoney!=0)
				this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].SubItems[6].Text=tolmoney.ToString("0.00");
            this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.intSeleteOrd].Tag = Updata;
		}

		#endregion

		#region  �޸�����
		/// <summary>
		/// �޸�����
		/// </summary>
		private void m_lngUpdata()
		{	
			
			clsMedStorageOrd_VO Updata=this.m_objViewer.m_ctlInMedOrd.m_objGetOrdInfo();
			Updata.m_strSTORAGEORDID_CHR=(string)this.m_objViewer.m_ctlInMedOrd.Tag;
			long lngRes;
			if(updataID!=null&&this.m_objViewer.m_lsvDetail.SelectedItems.Count>0)
			{

			}
			else
			{
				lngRes=this.m_objManage.m_lngDoUpdateOrd(Updata);
				if(lngRes>0)
				{
					clsMedStorageOrd_VO SeleItem1=new clsMedStorageOrd_VO();
					for(int i1=0;i1<this.m_objViewer.m_lsvUnAduit.Items.Count;i1++)
					{
						SeleItem1=(clsMedStorageOrd_VO)this.m_objViewer.m_lsvUnAduit.Items[i1].Tag;
						if(SeleItem1.m_strSTORAGEORDID_CHR==Updata.m_strSTORAGEORDID_CHR)
						{
							m_mthOrdModifyGUI(Updata,0);
							break;
						}
					}
				}
			}
			m_lngCountTol();			
			TolPriceDe=0;
			updataID=null;
			TolPriceDe=0;

			this.m_objViewer.btnSave.Text="����(&S)";
			intNewOrUpdate = 0;
            IntEditList=0;
			intWindowState=1;

		}
		#endregion

		#region �ж��û�����ʱ����������Ǹ��б�
		/// <summary>
		/// 1����ϸ�б�2����ⵥ�б�
		/// </summary>
		/// <param name="Command"></param>
		
		ListViewItem lvt=null;//ѡ�в�Ҫ����������
		int indexItem=-1;//ѡ�в�Ҫ������������
		public void MouseDown(int Command)
		{
			if(Command==1)
			{
				Point pot = Form.MousePosition;
				pot = this.m_objViewer.FindForm().PointToClient(pot);
				pot=this.m_objViewer.m_lsvDetail.Parent.PointToScreen(pot);
				pot = this.m_objViewer.m_lsvDetail.PointToClient(pot);
				lvt = this.m_objViewer.m_lsvDetail.GetItemAt(20,pot.Y);
				indexItem=this.m_objViewer.m_lsvDetail.Items.IndexOf(lvt);
			}
			if(Command==1)
				IntEditList=1;
			if(Command==2)
				IntEditList=0;
		}
		#endregion

		#region �϶�ʱ�ı��к�
		/// <summary>
		/// �϶�ʱ�ı��к�
		/// </summary>
		public void m_mthChengRowNO()
		{
			DataTable dt=new DataTable();
			if(isModidy==1)
			{
				dt.Columns.Add("RowNO");
				dt.Columns.Add("STORAGEORDDEID");
				for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;i1++)
				{
					clsMedStorageOrdDe_VO deVo=(clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
					DataRow newRow=dt.NewRow();
					int k=i1+1;
					newRow["RowNO"]=k.ToString("000");
					newRow["STORAGEORDDEID"]=deVo.m_strSTORAGEORDDEID_CHR;
					dt.Rows.Add(newRow);
				}
				m_objManage.m_lngModifyRowNO(dt);
			}
		}
		#endregion

		#region ��������û�����
		/// <summary>
		/// ��������û�����
		/// </summary>
		public  void m_lngFrmReset()
		{
			intWindowState=0;
			updataID=null;
			TolPriceDe=0;
			intNewOrUpdate = 0;
			IntEditList=1;
			this.m_objViewer.m_lsvDetail.Items.Clear();
			this.m_objViewer.m_ctlInMed.m_mthClear();
            this.m_objViewer.m_ctlInMedOrd.isNewOrd = true;
			this.m_objViewer.m_ctlInMedOrd.m_mthClearOrd();
			this.m_objViewer.btnSave.Text="����(&S)";
			isSave=false;
		}
		#endregion

		#region ��ȡ���еĵ���
		/// <summary>
		/// ��ȡ���еĵ���
		/// </summary>
		public void m_mthGetOrd()
		{
			if(this.m_objViewer.m_lsvUnAduit.Items.Count>0)
			{
				p_objResultArr=new clsMedStorageOrd_VO[this.m_objViewer.m_lsvUnAduit.Items.Count];
				clsMedStorageOrd_VO SeleItem2=new clsMedStorageOrd_VO();
				for(int i1=0;i1<this.m_objViewer.m_lsvUnAduit.Items.Count;i1++)
				{
					SeleItem2=(clsMedStorageOrd_VO)this.m_objViewer.m_lsvUnAduit.Items[i1].Tag;
					p_objResultArr[i1]=new clsMedStorageOrd_VO();
					p_objResultArr[i1].m_dblTOLMNY_MNY=SeleItem2.m_dblTOLMNY_MNY;
					p_objResultArr[i1].m_intDEPTTYPE_INT=SeleItem2.m_intDEPTTYPE_INT;
					p_objResultArr[i1].m_intPSTATUS_INT=SeleItem2.m_intPSTATUS_INT;
					p_objResultArr[i1].m_strACCTDATE_DAT=SeleItem2.m_strACCTDATE_DAT;
					p_objResultArr[i1].m_strACCTEMP_CHR=SeleItem2.m_strACCTEMP_CHR;
					p_objResultArr[i1].m_strACCTEMPNAME_CHR=SeleItem2.m_strACCTEMPNAME_CHR;
					p_objResultArr[i1].m_strADUITDATE_DAT=SeleItem2.m_strADUITDATE_DAT;
					p_objResultArr[i1].m_strADUITEMP_CHR=SeleItem2.m_strADUITEMP_CHR;
					p_objResultArr[i1].m_strADUITEMPNAME_CHR=SeleItem2.m_strADUITEMPNAME_CHR;
					p_objResultArr[i1].m_strCREATEDATE_DAT=SeleItem2.m_strCREATEDATE_DAT;
					p_objResultArr[i1].m_strCREATORID_CHR=SeleItem2.m_strCREATORID_CHR;
					p_objResultArr[i1].m_strCREATORNAME_CHR=SeleItem2.m_strCREATORNAME_CHR;
					p_objResultArr[i1].m_strDEPTID_CHR=SeleItem2.m_strDEPTID_CHR;
					p_objResultArr[i1].m_strDEPTNAME_CHR=SeleItem2.m_strDEPTNAME_CHR;
					p_objResultArr[i1].m_strDOCID_VCHR=SeleItem2.m_strDOCID_VCHR;
					p_objResultArr[i1].m_strINORD_DAT=SeleItem2.m_strINORD_DAT;
					p_objResultArr[i1].m_strMEMO_VCHR=SeleItem2.m_strMEMO_VCHR;
					p_objResultArr[i1].m_strOFFERID_CHR=SeleItem2.m_strOFFERID_CHR;
					p_objResultArr[i1].m_strOFFERIDNAME_CHR=SeleItem2.m_strOFFERIDNAME_CHR;
					p_objResultArr[i1].m_strPERIODID_CHR=SeleItem2.m_strPERIODID_CHR;
					p_objResultArr[i1].m_strSTORAGEID_CHR=SeleItem2.m_strSTORAGEID_CHR;
					p_objResultArr[i1].m_strSTORAGENAME_CHR=SeleItem2.m_strSTORAGENAME_CHR;
					p_objResultArr[i1].m_strSTORAGEORDID_CHR=SeleItem2.m_strSTORAGEORDID_CHR;
					p_objResultArr[i1].m_strSTORAGEORDTYPEID_CHR=SeleItem2.m_strSTORAGEORDTYPEID_CHR;
					p_objResultArr[i1].m_strSTORAGEORDTYPENAME_CHR=SeleItem2.m_strSTORAGEORDTYPENAME_CHR;
					p_objResultArr[i1].m_strVENDORID_CHR=SeleItem2.m_strVENDORID_CHR;
					p_objResultArr[i1].m_strVENDORNAME_CHR=SeleItem2.m_strVENDORNAME_CHR;
				}
			}
		}
		#endregion

		#region ��������
		/// <summary>
		/// ��������
		/// </summary>
		public void m_lngFindData()
		{
			if(this.m_objViewer.comboBox1.Text=="")
			{
				this.m_objViewer.comboBox1.Focus();
				return;
			}
			if(this.m_objViewer.textBox1.Text=="")
			{
				this.m_objViewer.textBox1.Focus();
				return;
			}
            if (p_objResultArr == null)
                return;
			p_objResultFind=new clsMedStorageOrd_VO[p_objResultArr.Length];
			int intNumber=0;
			string strSele=this.m_objViewer.textBox1.Text.Trim();

				switch(this.m_objViewer.comboBox1.Text)
				{
					case "���ݺ�":
						for(int i1=0;i1<p_objResultArr.Length;i1++)
						{
							if(p_objResultArr[i1].m_strDOCID_VCHR.IndexOf(strSele,0)==0)
							{
								p_objResultFind[intNumber]=new clsMedStorageOrd_VO();
								p_objResultFind[intNumber]=p_objResultArr[i1];
								intNumber++;
							}
						}
						break;
					case "������":
						for(int i1=0;i1<p_objResultArr.Length;i1++)
						{
							if(p_objResultArr[i1].m_strCREATORNAME_CHR.IndexOf(strSele,0)==0)
							{
								p_objResultFind[intNumber]=new clsMedStorageOrd_VO();
								p_objResultFind[intNumber]=p_objResultArr[i1];
								intNumber++;
							}
						}
						break;
					case "����ʱ��":
						for(int i1=0;i1<p_objResultArr.Length;i1++)
						{
							if(p_objResultArr[i1].m_strCREATEDATE_DAT.IndexOf(strSele,0)==0)
							{
								p_objResultFind[intNumber]=new clsMedStorageOrd_VO();
								p_objResultFind[intNumber]=p_objResultArr[i1];
								intNumber++;
							}
						}
						break;
					case "��Ӧ��":
						for(int i1=0;i1<p_objResultArr.Length;i1++)
						{
							if(p_objResultArr[i1].m_strVENDORNAME_CHR.IndexOf(strSele,0)==0)
							{
								p_objResultFind[intNumber]=new clsMedStorageOrd_VO();
								p_objResultFind[intNumber]=p_objResultArr[i1];
								intNumber++;
							}
						}
						break;
					case "�ɹ�Ա":
						for(int i1=0;i1<p_objResultArr.Length;i1++)
						{
							if(p_objResultArr[i1].m_strOFFERIDNAME_CHR.IndexOf(strSele,0)==0)
							{
								p_objResultFind[intNumber]=new clsMedStorageOrd_VO();
								p_objResultFind[intNumber]=p_objResultArr[i1];
								intNumber++;
							}
						}
						break;
				}
			this.m_objViewer.m_lsvUnAduit.Items.Clear();
			this.m_objViewer.m_lsvEnAduit.Items.Clear();
			for(int i1=0;i1<intNumber;i1++)
			{
				if(p_objResultFind[i1].m_intPSTATUS_INT==1)
				{
					m_lngFillStorageOrdList(p_objResultFind[i1]);
				}
				else
				{
					m_lngFillStorageOrdOK(p_objResultFind[i1]);

				}
			}
		}
		#endregion

		#region �رղ��ҽ���
		/// <summary>
		/// �رղ��ҽ���
		/// </summary>
		public void m_ColseFind()
		{
            //this.m_objViewer.panel1.Visible=false;
			this.m_objViewer.m_lsvUnAduit.Items.Clear();
			this.m_objViewer.m_lsvEnAduit.Items.Clear();
			if(p_objResultArr!=null)
			{

					for(int i1=0;i1<p_objResultArr.Length;i1++)
					{
						if(p_objResultArr[i1].m_intPSTATUS_INT==1)
						{
							m_lngFillStorageOrdList(p_objResultArr[i1]);
						}
						else
						{
							m_lngFillStorageOrdOK(p_objResultArr[i1]);

						}
					}
			}
		}

		#endregion

		#region ��˹���
		/// <summary>
		/// ��˹���
		/// </summary>
		public void EmpOrd()
		{
			if(this.m_objViewer.m_lsvDetail.Items.Count>0)
			{
				if(this.m_objViewer.m_lsvUnAduit.SelectedItems.Count==0)
				{
					publicClass.m_mthShowWarning(this.m_objViewer.m_lsvUnAduit,"��ǰû��ѡ�е���ⵥ��");
					return;
				}
				long lngRes=0;
				clsMedStorageOrd_VO SelectItem=new clsMedStorageOrd_VO();
				SelectItem=(clsMedStorageOrd_VO)this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Tag;
				SelectItem.m_strADUITEMPNAME_CHR=this.m_objViewer.LoginInfo.m_strEmpName;
				string EmpMan=this.m_objViewer.LoginInfo.m_strEmpID;
				string strdate=clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss");
				clsMedStorageOrdDe_VO[] EathDate=new clsMedStorageOrdDe_VO[this.m_objViewer.m_lsvDetail.Items.Count];
				for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;i1++)
				{
					EathDate[i1]=new clsMedStorageOrdDe_VO();
					EathDate[i1]=(clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
				}
				lngRes=this.m_objManage.m_lngEmpOrd(SelectItem.m_strSTORAGEORDID_CHR,SelectItem.m_strSTORAGEID_CHR,EmpMan,strdate,EathDate,this.m_objViewer.intDept);
				if(lngRes>0)
				{
					this.m_objViewer.m_lsvDetail.Items.Clear();
					this.m_objViewer.m_lsvUnAduit.Items.RemoveAt(this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Index);
					ListViewItem LisTemp=null;
					LisTemp=new ListViewItem(SelectItem.m_strDOCID_VCHR);
					LisTemp.SubItems.Add(SelectItem.m_strSTORAGEORDTYPENAME_CHR);
					LisTemp.SubItems.Add(SelectItem.m_strCREATORNAME_CHR);
					LisTemp.SubItems.Add(SelectItem.m_strINORD_DAT);
					LisTemp.SubItems.Add(SelectItem.m_strADUITEMPNAME_CHR);
					LisTemp.SubItems.Add(SelectItem.m_strVENDORNAME_CHR);
					LisTemp.SubItems.Add(SelectItem.m_dblTOLMNY_MNY.ToString());
					LisTemp.Tag=SelectItem;
					this.m_objViewer.m_lsvEnAduit.Items.Add(LisTemp);
					m_lngFrmReset();
				}
			}
			else
			{
				publicClass.m_mthShowWarning(this.m_objViewer.m_lsvDetail,"�յ���ⵥ��������ˣ�");
				
			}
		}
		#endregion

		#region �ж���������Ǹ�ѡ�п�
		/// <summary>
		/// �ж���������Ǹ�ѡ�п�
		/// </summary>
		public void m_lngActivity()
		{
			if(this.m_objViewer.m_tabAduit.SelectedIndex==1)
			{
				this.m_objViewer.panel2.Enabled=false;
				this.m_objViewer.panel3.Enabled=false;
				this.m_objViewer.m_btnNew.Enabled=false;
				this.m_objViewer.btnSave.Enabled=false;
				this.m_objViewer.btnDelect.Enabled=false;
				this.m_objViewer.dntEmp.Enabled=false;
				this.m_objViewer.btnPrint.Enabled=true;
				if(isSave==true)
				{
					if(MessageBox.Show("�Ƿ񱣴浥��!","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
					{
//						if(intWindowState==1)
//						{
//							m_mthSave();
//						}
//						else
//						{
//							if(this.m_objViewer.m_txtMedName.Text!="")
//							{
//								m_mthOkButtonClick();
//							}
							m_mthSave();
//						}
					}
				}

				this.m_objViewer.m_lsvDetail.Items.Clear();
				m_lngFrmReset();
			}
			else
			{
				if(!clsPublicParm.m_EstimatePeriod(objPriodItems[this.m_objViewer.m_cboSelPeriod.SelectedIndex].m_strPeriodID))
				{
					this.m_objViewer.panel2.Enabled=true;
					this.m_objViewer.panel3.Enabled=true;
					this.m_objViewer.m_btnNew.Enabled=true;
					this.m_objViewer.btnSave.Enabled=true;
					this.m_objViewer.btnDelect.Enabled=true;
					this.m_objViewer.dntEmp.Enabled=true;
				}
				if(this.m_objViewer.m_lsvUnAduit.SelectedItems.Count>0)
				{
					isSave=false;
					m_lngLisvSelect();
				}
				else
				{
					this.m_objViewer.m_lsvDetail.Items.Clear();
					m_lngFrmReset();
				}

			}

		}
		#endregion

		#region �������е��û�����
		/// <summary>
		/// �������е��û�����
		/// </summary>
		public  void m_lngAllUnenable()
		{
			this.m_objViewer.panel3.Enabled=false;
			this.m_objViewer.panel2.Enabled=false;
			this.m_objViewer.m_btnNew.Enabled=false;
			this.m_objViewer.btnSave.Enabled=false;
			this.m_objViewer.dntEmp.Enabled=false;
			this.m_objViewer.btnDelect.Enabled=false;

		}
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public  void m_lngAllenable()
		{
			this.m_objViewer.panel3.Enabled=true;
			this.m_objViewer.panel2.Enabled=true;
			this.m_objViewer.m_btnNew.Enabled=true;
			this.m_objViewer.btnSave.Enabled=true;
			this.m_objViewer.dntEmp.Enabled=true;
			this.m_objViewer.btnDelect.Enabled=true;

		}
		#endregion

		#region ��ȡҩƷ�Ĳο���
		/// <summary>
		/// ��ȡҩƷ�Ĳο���
		/// </summary>
		/// <param name="strMedID"></param>
		/// <returns></returns>
		public string  m_mthGetConsoltPrice(string strMedID)
		{

			string strConsoltPrice="";
			string strConsoltPrice1="";
			string strConsoltPrice2="";
			string strConsoltPrice3="";
			string ConsoltLIMITUNITPRICE;
			string ConsoltAIMUNITPRICE;
			this.m_objManage.m_lngGetConsoltPrice(strMedID,out strConsoltPrice,out strConsoltPrice1,out strConsoltPrice2,out strConsoltPrice3,out ConsoltAIMUNITPRICE,out ConsoltLIMITUNITPRICE);
			return strConsoltPrice;
		}
		#endregion

		#region ���뵼������
		/// <summary>
		/// ���뵼������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void clsControlMedStorageInOrd_Click(object sender, EventArgs e)
		{

			MenuItem objItem=(MenuItem)sender;
			if(this.m_objViewer.m_lsvEnAduit.SelectedItems.Count>0)
			{
				if(MessageBox.Show("�Ƿ�Ҫ�Ѹ�������¼����������棿","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
				{
					DataRow[] objseleRow=dtType.Select("STORAGEORDTYPENAME_VCHR = '"+objItem.Text.Replace("����","")+"'");
					clsMedStorageOrd_VO objSeleItem=(clsMedStorageOrd_VO)this.m_objViewer.m_lsvEnAduit.SelectedItems[0].Tag;
					string p_strMaxDoc=null;
                    clsStorageOrdType_VO ordTypeVo = new clsStorageOrdType_VO();
                    clsDomainControlStorageOrd m_objManage = new clsDomainControlStorageOrd();
                    m_objManage.m_lngFindOrdTypeNameByID(objseleRow[0]["STORAGEORDTYPEID_CHR"].ToString().Trim(), out ordTypeVo, "");
                    m_objManage.m_lngGetMaxDoc(out p_strMaxDoc, ordTypeVo.m_strBEGINSTR_CHR + DateTime.Now.Date.ToString("yyMMdd"), "2", objSeleItem.m_strSTORAGEID_CHR);
                    string maxDocId = clsPublicParm.m_mthGetNewDocument(p_strMaxDoc, objseleRow[0]["STORAGEORDTYPEID_CHR"].ToString().Trim(), int.Parse(objSeleItem.m_strSTORAGEID_CHR), DateTime.Now.Date.ToString("yyMMdd"), ordTypeVo.m_strBEGINSTR_CHR);
					long lngRes=m_objManage.m_lngGuideRope(objSeleItem.m_strSTORAGEORDID_CHR,objseleRow[0]["STORAGEORDTYPEID_CHR"].ToString(),objPriodItems[intSelPeriod].m_strPeriodID,objSeleItem.m_strDOCID_VCHR,maxDocId,this.m_objViewer.LoginInfo.m_strEmpID,"2");
					if(lngRes==1)
					{
						publicClass.m_mthShowWarning(this.m_objViewer.panel2,"�������ݳɹ���");
					}
				}
			}
			else
			{
				publicClass.m_mthShowWarning(this.m_objViewer.panel2,"����ѡ��Ҫ���������ݣ�");
			}

		}
		#endregion
	}

}
