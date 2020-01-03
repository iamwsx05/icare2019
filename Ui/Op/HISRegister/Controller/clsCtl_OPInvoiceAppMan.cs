using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// ���﷢Ʊ����
	/// ���ߣ� ����
	/// ʱ�䣺 Aug 23, 2004
	/// </summary>
	public class clsCtl_OPInvoiceAppMan: com.digitalwave.GUI_Base.clsController_Base
	{
		#region ����
		clsDcl_InvoiceManage m_objManage = null;
		public string m_strReportID;
		public string m_strOperatorID;
        /// <summary>
        /// ��Ʊ���� 0-��ͨ��Ʊ(Ĭ��) 1-����Ʊ��
        /// </summary>
        internal int intInvType = 0;
		#endregion 

		#region ���캯��
		public clsCtl_OPInvoiceAppMan()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDcl_InvoiceManage();
			m_strReportID = null;
			m_strOperatorID = "0000001";
		}
		#endregion 

		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmOPInvoiceAppMan m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmOPInvoiceAppMan)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ʼ�� ListView
		/// <summary>
		/// ��ʼ�� ListView [Ĭ����ʾ��������ķ�Ʊ]
		/// </summary>
		public void m_FillListView()
		{
			clsT_opr_opinvoiceman_VO[] objResultArr = null;
			//��ʼ��Ĭ����ʾ�������췢Ʊ�����м�¼
			//m_objManage.m_lngGetApplyInvoice(System.DateTime.Now.ToShortDateString(),System.DateTime.Now.ToShortDateString(),"",out objResultArr);
            m_objManage.m_lngGetApplyInvoice("", "", "", intInvType, out objResultArr);
			if(objResultArr == null || objResultArr.Length == 0)
				return;
			
			ListViewItem lviTemp;
			int iTem = 0;
			m_objViewer.m_lstApplyInvoiceMan.Items.Clear();			
			for(int i1= 0 ;i1<objResultArr.Length;i1++)
			{				
				//��Ʊ������ˮ��
				lviTemp = new ListViewItem(objResultArr[i1].m_strAPPUSERNAME_CHR);
				//�����־
				//lviTemp.SubItems.Add("");
				//lviTemp.SubItems.Add(objResultArr[i1].m_strAPPUSERID_CHR);
				//��ʼ��Ʊ��
				lviTemp.SubItems.Add(objResultArr[i1].m_strINVOICENOFROM_VCHR);
				//������Ʊ��
				lviTemp.SubItems.Add(objResultArr[i1].m_strINVOICENOTO_VCHR);
				//��Ʊ����
				try
				{
					string intMin="";
					string intMax="";
					long lngRes =this.GetNumber(objResultArr[i1].m_strINVOICENOFROM_VCHR,objResultArr[i1].m_strINVOICENOTO_VCHR, out intMin,out intMax);
					int countint=Convert.ToInt32(intMax)-Convert.ToInt32(intMin);
					lviTemp.SubItems.Add(countint.ToString());
				}
				catch{}
				//����������
				//lviTemp.SubItems.Add(objResultArr[i1].m_strOPERATORNAME_CHR);
				//��������
				lviTemp.SubItems.Add(objResultArr[i1].m_strAPPLY_DAT);
				//����������
				lviTemp.SubItems.Add(objResultArr[i1].m_strCANCELUSERNAME_CHR);
				//����ʱ��
				lviTemp.SubItems.Add(objResultArr[i1].m_strCANCEL_DAT);
                if (objResultArr[i1].intInvoiceTypeFlag == 1)
                {
                    lviTemp.SubItems.Add("������λ����Ʊ��");
                }
                else if (objResultArr[i1].intInvoiceTypeFlag == 2)
                {
                    lviTemp.SubItems.Add("�����շ�ͳһƱ��");
                }
                else
                {
                    lviTemp.SubItems.Add("��ͨ��Ʊ");
                }
				lviTemp.Tag =objResultArr[i1].m_strAPPID_CHR;

				m_objViewer.m_lstApplyInvoiceMan.Items.Add(lviTemp);
			}
		}
		#endregion

		#region ���ݹ��Ż��ְ������
		/// <summary>
		/// ����ְ���Ĺ������ְ��������
		/// </summary>
		public void m_GetEmployeeName()
		{
			//�������TextBox
			m_objViewer.m_txtAPPUSERNAME_CHR.Text ="";

			if(m_objViewer.m_txtAPPUSERID_CHR.Text==null || m_objViewer.m_txtAPPUSERID_CHR.Text.Trim()=="")
				return;

			string strApplyName = "";
			m_objManage.m_lngGetEmployeeNameByNO(m_objViewer.m_txtAPPUSERID_CHR.Text.Trim(),out strApplyName);
			m_objViewer.m_txtAPPUSERNAME_CHR.Text =strApplyName;
		}
		#endregion 

		#region ��Ʊ��Ŀ
		/// <summary>
		/// ��Ʊ��Ŀ
		/// </summary>
		public void m_GetInvoiceNumber(string strMinNO,string strMaxNO)
		{
			if(strMinNO==""||strMaxNO=="")
				return;

			//��շ�Ʊ����TextBox
			m_objViewer.m_txtINVOICENUMBET_INT.Text ="";
			this.m_objViewer.label10.Text="";
			if(strMinNO.Length!=strMaxNO.Length)
			{
                this.m_objViewer.label10.Text="���뷢Ʊ�ĳ��Ȳ���ȣ�";
				m_objViewer.m_txtINVOICENOTO_VCHR.Focus();
				return;
			}
			string  intMax = "0";
			string  intMin = "0";
			int intNumber=0;
			try
			{
				long lngRes =this.GetNumber(strMinNO,strMaxNO, out intMin,out intMax);
				if(lngRes!=-1&&intMin.Length==intMax.Length)
					intNumber =Convert.ToInt32(intMax) -Convert.ToInt32(intMin);
				else
				{
					this.m_objViewer.label10.Text="����ķ�Ʊ�Ų���ͬһ�������ϣ�\r\n��ʼ��Ʊ�š�������Ʊ�ű������\r\n������ʽ���磺WD100��WD500";
					m_objViewer.m_txtINVOICENOTO_VCHR.Focus();
					return;
				}

			}
			catch
			{				
				return;
			}

			m_objViewer.m_txtINVOICENUMBET_INT.Text =intNumber.ToString();
		}
		#endregion 
		/// <summary>
		/// �ֽⷢƱ��
		/// </summary>
		/// <param name="text1"></param>
		/// <param name="text2"></param>
		/// <param name="number1"></param>
		/// <param name="number1"></param>
		/// <returns>����-1��ʾ����ķ�Ʊ�Ų���ͬһ����Ʊ������</returns>
		private long GetNumber(string text1,string text2,out string number1,out string number2)
		{
			number1="";
			number2="";
			char[] chArr1 = text1.ToCharArray();
			char[] chArr2 = text2.ToCharArray();
			int val1 = 0;
			for(int i = chArr1.Length-1 ;i>=0;i--)
			{
				if(chArr1[i]>47&&chArr1[i]<58)
				{
					continue;
				}
				val1 = i;
				break;
			}
			int val2 = 0;
			for(int i1 = chArr2.Length-1 ;i1>=0;i1--)
			{
				if(chArr2[i1]>47&&chArr2[i1]<58)
				{
					continue;
				}
				val2 = i1;
				break;
			}
			if(val2!=val1)
			{
				return -1;//��Ʊ�Ų���ͬһ����Ʊ��������
			}
			else if(text1.Substring(0,val1)!=text2.Substring(0,val2))
			{
				return -1;//��Ʊ�Ų���ͬһ����Ʊ��������
			}
			if(val1>=text1.Length)
				number1="0";
			else if(val1 ==0)
				number1=text1.Substring(val1,text1.Length);
			else
				number1=text1.Substring(val1+1,text1.Length-val1-1);

			if(val2>=text2.Length)
				number2="0";
			else if(val2 ==0)
				number2=text2.Substring(val2,text2.Length);
			else
				number2=text2.Substring(val1+1,text2.Length-val2-1);
			return 1;
		}

		#region ������췢Ʊ
		#region У������ֵ
		/// <summary>
		/// У������ֵ
		/// </summary>
		/// <returns>������֤���</returns>
		private bool m_bolCheckValuePass()
		{
			bool bolReturn = true;
			string strReturn ="";
			if(m_objViewer.m_txtAPPUSERID_CHR.Text.Trim() == "")
			{
				strReturn += "���Ų����٣�\n";
				bolReturn = false;
			}
			else
			{
				if(m_objViewer.m_txtAPPUSERNAME_CHR.Text.Trim()=="")
				{
					strReturn += "�����������\n";
					bolReturn = false;
				}
			}
			if(m_objViewer.m_txtINVOICENOFROM_VCHR.Text.Trim() == "")
			{
				strReturn += "��ʼ��Ʊ�Ų�����\n";
				bolReturn = false;
			}
			if(m_objViewer.m_txtINVOICENOTO_VCHR.Text.Trim() == "")
			{
				strReturn += "��ֹ��Ʊ�Ų����٣�\n";
				bolReturn = false;
			}
			if(m_objViewer.m_dtpAPPLY_DAT.Text.Trim() == "")
			{
				strReturn += "�������ڲ����٣�\n";
				bolReturn = false;
			}
			if(m_objViewer.m_txtINVOICENUMBET_INT.Text.Trim()=="" || Int32.Parse(m_objViewer.m_txtINVOICENUMBET_INT.Text.Trim())<=0)
			{
				strReturn += "��Ʊ�����������0�ţ�";
				bolReturn = false;
			}
			if(!bolReturn)
			{
				MessageBox.Show(m_objViewer,strReturn,"�� ��",MessageBoxButtons.OK,MessageBoxIcon.Warning);
			}
			return bolReturn;
		}
		#endregion
		public void m_lngDoAddNewT_opr_opinvoiceman()
		{
			//�����֤
			if(!m_bolCheckValuePass())
				return;			

			//��ȡclsT_opr_opinvoiceman_VO
			clsT_opr_opinvoiceman_VO objResult = new clsT_opr_opinvoiceman_VO();
			string number1="";
			string number2="";
			long lngRes = this.GetNumber(m_objViewer.m_txtINVOICENOFROM_VCHR.Text.Trim(),m_objViewer.m_txtINVOICENOTO_VCHR.Text.Trim(),out number1,out number2);
            // kenny add
            string strH1 = System.Text.RegularExpressions.Regex.Replace(m_objViewer.m_txtINVOICENOFROM_VCHR.Text.Trim(), @"[^A-Za-z]*", "");
            string strH2 = System.Text.RegularExpressions.Regex.Replace(m_objViewer.m_txtINVOICENOTO_VCHR.Text.Trim(), @"[^A-Za-z]*", "");
            // --
            if ((lngRes == -1 || number1.Length != number2.Length) || strH1 != strH2)
			{
				MessageBox.Show("����ķ�Ʊ�Ų���ͬһ�������ϣ�\n��ʼ��Ʊ�š�������Ʊ�ű������������ʽ��\n�磺WD100��WD500","Icare",MessageBoxButtons.OK,MessageBoxIcon.Stop);
				return;
			}
				objResult.m_strINVOICENOFROM_VCHR=m_objViewer.m_txtINVOICENOFROM_VCHR.Text;
			objResult.m_strINVOICENOTO_VCHR = m_objViewer.m_txtINVOICENOTO_VCHR.Text;
			objResult.m_strAPPLY_DAT = m_objViewer.m_dtpAPPLY_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			//��ȡְ����ˮ��
			string  strID ="";
			lngRes =m_objManage.m_lngGetEmployeeIDByNO(m_objViewer.m_txtAPPUSERID_CHR.Text,out strID);
			objResult.m_strAPPUSERID_CHR = strID;
			objResult.m_strOPERATORID_CHR = m_strOperatorID;
            objResult.intInvoiceTypeFlag = this.intInvType;

			//��֤�Ƿ�Ʊ���䱻��ȡ��
			bool blnIsUsed =true;
            long iResult = m_objManage.m_lngCheckInvoiceNOIsUsed(objResult.m_strINVOICENOFROM_VCHR, objResult.m_strINVOICENOTO_VCHR, objResult.intInvoiceTypeFlag, out blnIsUsed);
			if(iResult<=0)
			{	
				MessageBox.Show(m_objViewer,"����ʧ��!","������ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			if(blnIsUsed )
			{
				MessageBox.Show(m_objViewer,"�˷�Ʊ�ڼ����в��ַ�Ʊ�Ѿ����������ˣ�����ʧ�ܣ�","�� ��",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}

			//���
			string strAppid_chr = "";
			iResult =m_objManage.m_lngDoAddNewT_opr_opinvoiceman(objResult,out strAppid_chr);
			if(iResult<=0)
			{
				MessageBox.Show(m_objViewer,"����ʧ��!","������ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			else
			{
				MessageBox.Show(m_objViewer,"����ɹ�!","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			
			//��ListView�����һ��
			string strTem = "";
			int iTem =0;			
			//����������
			m_objManage.m_lngGetEmployeeNameByID(objResult.m_strAPPUSERID_CHR.Trim(),out strTem);
			ListViewItem lviTemp = new ListViewItem(strTem);
			//�����־
			//lviTemp.SubItems.Add("");
			//lviTemp.SubItems.Add(objResultArr[i1].m_strAPPUSERID_CHR);
			//��ʼ��Ʊ��
			lviTemp.SubItems.Add(objResult.m_strINVOICENOFROM_VCHR);
			//������Ʊ��
			lviTemp.SubItems.Add(objResult.m_strINVOICENOTO_VCHR);
			//��Ʊ����
			iTem = System.Convert.ToInt32(this.m_objViewer.m_txtINVOICENUMBET_INT.Text);
			lviTemp.SubItems.Add(iTem.ToString()) ;
			//����������
			//m_objManage.m_lngGetEmployeeNameByID(objResult.m_strOPERATORID_CHR.Trim(),out strTem);
			//lviTemp.SubItems.Add(strTem);
			//��������
			lviTemp.SubItems.Add(objResult.m_strAPPLY_DAT);
			m_objViewer.m_lstApplyInvoiceMan.Items.Add(lviTemp);	
			//����������
			lviTemp.SubItems.Add("");
			//����ʱ��
			lviTemp.SubItems.Add("");
            if (objResult.intInvoiceTypeFlag == 1)
            {
                lviTemp.SubItems.Add("������λ����Ʊ��");
            }
            else if (objResult.intInvoiceTypeFlag == 2)
            {
                lviTemp.SubItems.Add("�����շ�ͳһƱ��");
            }
            else
            {
                lviTemp.SubItems.Add("��ͨ��Ʊ");
            }
			lviTemp.Tag =strAppid_chr;
			//���
			m_EmptyInput();
		}
		#endregion

		#region ���ϵ�ǰ��¼
		public void m_lngModifyT_opr_opinvoiceman()
		{
			//û��ѡ�У��򷵻أ�
			int iTem =m_objViewer.m_lstApplyInvoiceMan.SelectedItems.Count;
			if( iTem == 0)
				return;
			clsT_opr_opinvoiceman_VO objResult = new clsT_opr_opinvoiceman_VO();
			
			//��ʾ�û�ȷ�����ϲ������ۣ����ֻѡ�ж��У���ô��������ʾ�ݣ�
			if(iTem>1)
			{
				DialogResult result;
				result = MessageBox.Show(m_objViewer, "ȷ��Ҫ����ѡ���еķ�Ʊ��", "��ʾ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if(result == DialogResult.No)
				{
					return;
				}
			}

			bool blnIsUsed;
			for(int i1=0;i1< iTem;i1++)
			{
				//���ϵķ�Ʊ������ˮ��
				objResult.m_strAPPID_CHR =m_objViewer.m_lstApplyInvoiceMan.SelectedItems[i1].Tag.ToString();
				//������ID
				objResult.m_strCANCELUSERID_CHR =m_strOperatorID;

				//����Ƿ��Ѿ���������    [�Ѿ����ϵķ�Ʊ�����������ϲ���]
				blnIsUsed =true;
				m_objManage.m_lngCheckInvoiceNOIsCancel(objResult.m_strAPPID_CHR,out blnIsUsed);
				if(!blnIsUsed)
				{
					//��ʾ�û�ȷ�����ϲ����������ֻѡ��һ�У���ô��������ʾ��
					if(iTem==1)
					{
						DialogResult result;
						result = MessageBox.Show(m_objViewer, "ȷ��Ҫ����ѡ���еķ�Ʊ��", "��ʾ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
						if(result == DialogResult.No)
						{
							return;
						}
					}

					long iReturn = m_objManage.m_lngModifyT_opr_opinvoiceman(objResult);
					if(iReturn<=0)
					{
						MessageBox.Show(m_objViewer,"����ʧ��!","������ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
						return;
					}
			
					//�޸ĵ�ǰListViewѡ����
					//�޸�������
					string strTem="";
					m_objManage.m_lngGetEmployeeNameByID(m_strOperatorID.Trim(),out strTem);
					m_objViewer.m_lstApplyInvoiceMan.SelectedItems[i1].SubItems[5].Text =strTem;
					//�޸�����ʱ��
					m_objViewer.m_lstApplyInvoiceMan.SelectedItems[i1].SubItems[6].Text =System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				}
			}
		}
		#endregion

		#region ��ѯ���췢Ʊ
		/// <summary>
		/// ��ѯ���췢Ʊ
		/// </summary>
		public void m_lngGetApplyInvoice()
		{
			//��� ListView
			m_objViewer.m_lstApplyInvoiceMan.Items.Clear();	

			clsT_opr_opinvoiceman_VO[] objResultArr = null;
			//��ʼ��Ĭ����ʾ�������췢Ʊ�����м�¼
			string strStartAPPLY_DAT = m_objViewer.m_dtpStartAPPLY_DAT.Value.ToString("yyyy-MM-dd");// HH:mm:ss
			string strEndAPPLY_DAT = m_objViewer.m_dtpEndAPPLY_DAT.Value.ToString("yyyy-MM-dd");
			string  strID ="";
			long lngRes =m_objManage.m_lngGetEmployeeIDByNO(m_objViewer.m_txtAPPUSERID_CHR2.Text,out strID);
			if(m_objViewer.m_txtAPPUSERID_CHR2.Text!=string.Empty && strID==string.Empty)
			{
				strID ="~!!&^$)(_@";
			}
            m_objManage.m_lngGetApplyInvoice(strStartAPPLY_DAT, strEndAPPLY_DAT, strID, intInvType, out objResultArr);
			if(objResultArr == null || objResultArr.Length == 0)
				return;
			
			ListViewItem lviTemp;
			int iTem = 0;					
			for(int i1= 0 ;i1<objResultArr.Length;i1++)
			{	
				//����������
				lviTemp = new ListViewItem(objResultArr[i1].m_strAPPUSERNAME_CHR);
				//�����־
				//lviTemp.SubItems.Add("");
				//lviTemp.SubItems.Add(objResultArr[i1].m_strAPPUSERID_CHR);
				//��ʼ��Ʊ��
				lviTemp.SubItems.Add(objResultArr[i1].m_strINVOICENOFROM_VCHR);
				//������Ʊ��
				lviTemp.SubItems.Add(objResultArr[i1].m_strINVOICENOTO_VCHR);
				//��Ʊ����
				try
				{
                    iTem = System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(objResultArr[i1].m_strINVOICENOTO_VCHR, @"^[A-Za-z]*", "")) - System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(objResultArr[i1].m_strINVOICENOFROM_VCHR, @"^[A-Za-z]*", ""));
				}
				catch{}
				lviTemp.SubItems.Add(iTem.ToString()) ;
				//����������
				//lviTemp.SubItems.Add(objResultArr[i1].m_strOPERATORNAME_CHR);
				//��������
				lviTemp.SubItems.Add(objResultArr[i1].m_strAPPLY_DAT);
				//����������
				lviTemp.SubItems.Add(objResultArr[i1].m_strCANCELUSERNAME_CHR);
				//����ʱ��
				lviTemp.SubItems.Add(objResultArr[i1].m_strCANCEL_DAT);
                if (objResultArr[i1].intInvoiceTypeFlag == 1)
                {
                    lviTemp.SubItems.Add("������λ����Ʊ��");
                }
                else if (objResultArr[i1].intInvoiceTypeFlag == 2)
                {
                    lviTemp.SubItems.Add("�����շ�ͳһƱ��");
                }
                else
                {
                    lviTemp.SubItems.Add("��ͨ��Ʊ");
                }
				lviTemp.Tag =objResultArr[i1].m_strAPPID_CHR;

				m_objViewer.m_lstApplyInvoiceMan.Items.Add(lviTemp);
			}
		}
		#endregion

		#region ���
		public void m_EmptyInput()
		{
			m_objViewer.m_txtAPPUSERID_CHR.Text ="";
			m_objViewer.m_txtAPPUSERNAME_CHR.Text ="";
			m_objViewer.m_dtpAPPLY_DAT.Value =System.DateTime.Now;
			m_objViewer.m_txtINVOICENOFROM_VCHR.Text ="";
			m_objViewer.m_txtINVOICENOTO_VCHR.Text ="";
			m_objViewer.m_txtINVOICENUMBET_INT.Text ="";
		}	
		#endregion 
	}
}
