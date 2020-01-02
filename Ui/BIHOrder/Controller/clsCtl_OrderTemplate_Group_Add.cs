using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 
using com.digitalwave.Utility;
using System.Collections;

namespace com.digitalwave.iCare.BIHOrder
{
	/// <summary>
	/// clsCtl_OrderTemplate_Group_Add  
	/// </summary>
	public class clsCtl_OrderTemplate_Group_Add: com.digitalwave.GUI_Base.clsController_Base
	{
		clsDcl_OrderTemplate_Group_Add m_objDoctorAdvice =null;
        clsDcl_InputOrder m_objInputOrder = null;
		public string m_strOperatorID ="";
		public string m_strOperatorName="";

		/// <summary>
		/// ҽ��������ˮ��
		/// </summary>
		private string m_strGroupID ="";
		/// <summary>
		/// ҽ����������
		/// </summary>
		private string m_strGroupName="";
		/// <summary>
		/// ҽ�����ױ�
		/// </summary>
		private DataTable m_dtOrderGroup =new DataTable();
		/// <summary>
		/// ҽ�����׳�Ա��
		/// </summary>
		public  DataTable m_dtOrderGroupDetail=new DataTable();
		public clsCtl_OrderTemplate_Group_Add()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objDoctorAdvice =new clsDcl_OrderTemplate_Group_Add();
            m_objInputOrder = new clsDcl_InputOrder();
		}

		#region ���ô������
		com.digitalwave.iCare.BIHOrder.frmOrderTemplate_Group_Add m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmOrderTemplate_Group_Add)frmMDI_Child_Base_in;
		}
		#endregion

		public void m_LoadOrderGroupDetail()
		{
//			if(m_strGroupID==string.Empty) return;
//			m_dtOrderGroupDetail.Columns.Add(
			/*m_dtOrderGroupDetail.Columns.AddRange(new DataColumn[]{
																	 
																	  new DataColumn("detailid_chr"),
																	  new DataColumn("groupid_chr"),
																	  new DataColumn("orderdicid_chr"),
																	  new DataColumn("freqid_chr"),
																	  new DataColumn("dosetype_chr"),
																	  new DataColumn("GroupName"),
																	  new DataColumn("OrderdicName"),
																	  new DataColumn("FreqName"),
																	  new DataColumn("dosage_dec"),
																	  new DataColumn("dosageunit_chr"),
																	  new DataColumn("use_dec"),
																	  new DataColumn("useunit_chr"),
																	  new DataColumn("get_dec"),
																	  new DataColumn("getunit_chr"),
																	  new DataColumn("DosetypeName"),
																	  new DataColumn("entrust_vchr"),
																	  new DataColumn("isrich_int"),
																	  new DataColumn("parentid_chr"),
																	  new DataColumn("ifparentid_int"),
																	  new DataColumn("IfParentName"),
																	  new DataColumn("ParentName"),
																	  new DataColumn("DOSAGEVIEWTYPE"),
																	  new DataColumn("USAGEVIEWTYPE")
																	 
																  });
			*/
			long lngRes =m_objDoctorAdvice.m_lngGetOrderGroupDetailByGroupID(m_strGroupID,out m_dtOrderGroupDetail);
			
		}

		public void m_InitValueOrderGroupDetail(clsBIHOrder objItem,bool ifParent,bool blnSame)
		{
			
			
			long lngRes =m_objDoctorAdvice.m_InitValueOrderGroupDetail(objItem,ref m_dtOrderGroupDetail,ifParent,blnSame);
			
		}


		public void m_Save()
		{
			//������֤
			if(!m_booInputValidate()) return;
			
			//��clsT_bse_bih_orderdic_VO��ֵ
			clsT_aid_bih_ordergroup_VO dtResult =new clsT_aid_bih_ordergroup_VO(); 
			TextBoxToVo(out dtResult);
			string[] strArr=null;
			strArr = new string[0]; 
			//������Ŀ-�շ���Ŀӳ��
			long lngRes =m_objDoctorAdvice.m_lngSaveOrderGroupDetail(m_dtOrderGroupDetail,ref dtResult,strArr);	
			
			//������
			if(lngRes>0)
			{
				
				if(MessageBox.Show(m_objViewer,"����ɹ���","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information)==DialogResult.Yes)
					m_objViewer.Close();
				m_strGroupID =dtResult.m_strGROUPID_CHR;
				m_strGroupName =dtResult.m_strNAME_CHR;
				m_LoadDataInfo();	
//				m_Find();
			}
			else
			{
				MessageBox.Show(m_objViewer,"���ʧ�ܣ�","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);	
			}
		}


        public void m_SaveTheNew()
        {
            //������֤
            if (!m_booInputValidate()) return;

            //��clsT_bse_bih_orderdic_VO��ֵ
            clsT_aid_bih_ordergroup_VO dtResult = new clsT_aid_bih_ordergroup_VO();
            
            ArrayList m_arrGroupVo;
            TextBoxToVo(out dtResult);
            BingTheOrderVO(out m_arrGroupVo);
            SetTheGroupDetailParent(ref m_arrGroupVo);
            clsT_aid_bih_ordergroup_detail_VO[] m_objGroupVoList=null;
            if (m_arrGroupVo.Count > 0)
            {
                m_objGroupVoList = new clsT_aid_bih_ordergroup_detail_VO[m_arrGroupVo.Count];
                for (int i = 0; i < m_arrGroupVo.Count; i++)
                {
                    m_objGroupVoList[i] = (clsT_aid_bih_ordergroup_detail_VO)m_arrGroupVo[i];
                }
            }
            else
            {
                MessageBox.Show(m_objViewer, "��ѡ��������ϸ��¼��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return;
            }
            string[] strArr = null;
            strArr = new string[0];
            //������Ŀ-�շ���Ŀӳ��
           long lngRes = m_objDoctorAdvice.m_lngSaveOrderGroupDetailNew(dtResult,m_objGroupVoList);
            //������
            if (lngRes > 0)
            {

                if (MessageBox.Show(m_objViewer, "����ɹ���", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.Yes)
                    m_objViewer.Close();
                this.m_objViewer.Close();
                //m_strGroupID = dtResult.m_strGROUPID_CHR;
                //m_strGroupName = dtResult.m_strNAME_CHR;
                //m_LoadDataInfo();
                //				m_Find();
            }
            else
            {
                MessageBox.Show(m_objViewer, "���ʧ�ܣ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ͬ��ҽ���ĸ����趨
        /// </summary>
        /// <param name="m_arrGroupVo"></param>
        private void SetTheGroupDetailParent(ref ArrayList m_arrGroupVo)
        {
            int m_intRecipenNo = -1, m = 1;
            bool m_blSame = false;
            for (int i = 0; i < m_arrGroupVo.Count; i++)//��������
            {

                if (i > 0 && ((clsT_aid_bih_ordergroup_detail_VO)m_arrGroupVo[i]).m_intRecipenNo == m_intRecipenNo)
                {
                    m_blSame = true;
                }
                else
                {
                    m_blSame = false;
                }
                if (m_intRecipenNo != ((clsT_aid_bih_ordergroup_detail_VO)m_arrGroupVo[i]).m_intRecipenNo)
                {
                    m_intRecipenNo = ((clsT_aid_bih_ordergroup_detail_VO)m_arrGroupVo[i]).m_intRecipenNo;
                }
                if (m_blSame && i > 0)
                {
                    ((clsT_aid_bih_ordergroup_detail_VO)m_arrGroupVo[i]).m_intRecipenNo = ((clsT_aid_bih_ordergroup_detail_VO)m_arrGroupVo[i - 1]).m_intRecipenNo;
                }
                else
                {
                    ((clsT_aid_bih_ordergroup_detail_VO)m_arrGroupVo[i]).m_intRecipenNo = m;
                    m++;
                }
            }
            int Count = 0;
            for (int i = 1; i < m_arrGroupVo.Count; i++)//�����趨
            {
                if (((clsT_aid_bih_ordergroup_detail_VO)m_arrGroupVo[i]).m_intRecipenNo == ((clsT_aid_bih_ordergroup_detail_VO)m_arrGroupVo[i - 1]).m_intRecipenNo)
                {
                    ((clsT_aid_bih_ordergroup_detail_VO)m_arrGroupVo[i]).m_intIFPARENTID_INT = 0;
                    Count++;
                    if (Count == 1)
                    {
                        ((clsT_aid_bih_ordergroup_detail_VO)m_arrGroupVo[i - 1]).m_intIFPARENTID_INT = 1;
                    }
                }
                else
                {
                    Count = 0;
                }

            }


        }

        private void BingTheOrderVO(out ArrayList m_arrGroupVo)
        {
            m_arrGroupVo = new ArrayList();
            for (int i = 0; i < this.m_objViewer.m_dtvOrderGroup.RowCount; i++)
            {
                if (this.m_objViewer.m_dtvOrderGroup.Rows[i].Cells["dtv_check"].Value.ToString().Trim().Equals("1"))
                {
                    clsT_aid_bih_ordergroup_detail_VO m_objGroupVo = new clsT_aid_bih_ordergroup_detail_VO();
                    clsBIHOrder bihorder = (clsBIHOrder)this.m_objViewer.m_dtvOrderGroup.Rows[i].Tag;

                    #region ���ֵ
                    m_objGroupVo.m_fltDOSAGE_DEC = (float)bihorder.m_dmlDosage;
                    m_objGroupVo.m_fltGET_DEC = (float)bihorder.m_dmlGet;
                    m_objGroupVo.m_fltUSE_DEC = (float)bihorder.m_dmlUse;
                    m_objGroupVo.m_intATTACHTIMES_INT=bihorder.m_intATTACHTIMES_INT;
                    m_objGroupVo.m_intEXECUTETYPE_INT = bihorder.m_intExecuteType;
                    //m_objGroupVo.m_intIFPARENTID_INT=0;
                    m_objGroupVo.m_intISRICH_INT = bihorder.m_intIsRich;
                    m_objGroupVo.m_intOUTGETMEDDAYS_INT = bihorder.m_intOUTGETMEDDAYS_INT;
                    m_objGroupVo.m_strDETAILID_CHR = "";
                    m_objGroupVo.m_strDOSAGEUNIT_CHR = bihorder.m_strDosageUnit;
                    m_objGroupVo.m_strDOSETYPE_CHR = bihorder.m_strDosetypeID;
                    m_objGroupVo.m_strDosetypeName = bihorder.m_strDosetypeName;
                    m_objGroupVo.m_strENTRUST_VCHR = bihorder.m_strEntrust;
                    m_objGroupVo.m_strFREQID_CHR = bihorder.m_strExecFreqID;
                    m_objGroupVo.m_strFreqName = bihorder.m_strExecFreqName;
                    m_objGroupVo.m_strGETUNIT_CHR = bihorder.m_strGetunit;
                    m_objGroupVo.m_strGROUPID_CHR = bihorder.m_strOrderID;//���ñ���ҽ���ţ��������׸����жϣ�
                    m_objGroupVo.m_strGroupName = "";
                    m_objGroupVo.m_strORDERDICID_CHR = bihorder.m_strOrderDicID;
                    m_objGroupVo.m_strOrderdicName = bihorder.m_strName;
                    //m_objGroupVo.m_strPARENTID_CHR = bihorder.m_strParentID;
                    //m_objGroupVo.m_strParentName = "";
                    m_objGroupVo.m_strUSEUNIT_CHR = bihorder.m_strUseunit;
                    m_objGroupVo.m_strSAMPLEID_VCHR = bihorder.m_strSAMPLEID_VCHR;
                    m_objGroupVo.m_strPARTID_VCHR = bihorder.m_strPARTID_VCHR;
                    m_objGroupVo.m_intRecipenNo = bihorder.m_intRecipenNo;
                    m_objGroupVo.RateType = bihorder.RateType;
                    m_objGroupVo.m_intISNEEDFEEL = bihorder.m_intISNEEDFEEL;
                    m_objGroupVo.m_dmlOneUse = bihorder.m_dmlOneUse;
                    #endregion
                    m_arrGroupVo.Add(m_objGroupVo);
                }
            }
        }

        /// <summary>
        /// У������ֵ
        /// </summary>
        /// <returns></returns>
        private bool m_booInputValidate()
		{
			if(m_objViewer.m_txtNAME_CHR.Text.Trim() == "")
			{
				MessageBox.Show(m_objViewer,"ҽ���������Ʋ����٣�","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
                m_objViewer.m_txtNAME_CHR.Focus();
                return false;
			}
			if(m_objViewer.m_txtSHARETYPE_INT.Text.Trim() == "")
			{
				MessageBox.Show(m_objViewer,"�������Ͳ����٣�","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
                m_objViewer.m_txtSHARETYPE_INT.Focus();
                return false;
			}
            if (m_objViewer.m_txtUSERCODE_VCHR.Text.Trim() == "")
			{
                MessageBox.Show(m_objViewer, "�����벻���٣�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.m_txtUSERCODE_VCHR.Focus();
                return false;
			}
            //����Ƿ������ͬ��ҽ��������
            bool m_blSame = false;
            m_lngGetTheOrderGroupName(m_objViewer.m_txtNAME_CHR.Text.Trim(), out m_blSame);
            if (m_blSame == true)
            {
                MessageBox.Show(m_objViewer, "�Ѵ�����ͬ���������ƣ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.m_txtNAME_CHR.Focus();
                m_objViewer.m_txtNAME_CHR.SelectAll();
                return false;
            }
            /*<============================*/
		
			
			return true;
		}

        private void m_lngGetTheOrderGroupName(string m_strNAME_CHR, out bool m_blSame)
        {
            //������Ŀ-�շ���Ŀӳ��
            long lngRes = this.m_objDoctorAdvice.m_lngGetTheOrderGroupName(m_strNAME_CHR, out m_blSame);
        }

	

		/// <summary>
		/// ��TextBox���VO
		/// </summary>
		/// <param name="objItem"></param>
		private void TextBoxToVo(out clsT_aid_bih_ordergroup_VO objItem)
		{
			objItem = new clsT_aid_bih_ordergroup_VO();
			objItem.m_strGROUPID_CHR = m_strGroupID;
			objItem.m_strNAME_CHR = m_objViewer.m_txtNAME_CHR.Text.Trim();
			objItem.m_strDES_VCHR = m_objViewer.m_txtDES_VCHR.Text.Trim();
            objItem.m_strUSERCODE_VCHR = m_objViewer.m_txtUSERCODE_VCHR.Text.Trim();

			int index = m_objViewer.m_txtSHARETYPE_INT.SelectedIndex;
			int shareType=0;
			switch (index) 
			{
				case 0:
                    shareType = 1;
                    break;
				case 1:
					shareType =2;
					break;
				case 2:
					shareType = 3;
					break;
               
			}
			objItem.m_intSHARETYPE_INT = shareType;
			
			objItem.m_strWBCODE_CHR = m_objViewer.m_txtWBCODE_CHR.Text.Trim();
			objItem.m_strPYCODE_CHR = m_objViewer.m_txtPYCODE_CHR.Text.Trim();
			objItem.m_intISSAMERECIPENO_INT =(m_objViewer.m_chkISSAMERECIPENO_INT.Checked)?1:0;
			//�޸�ʱ������Ĵ����ߵ�
			objItem.m_strCREATORID_CHR =m_strOperatorID;
			//objItem.m_strCREATORID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
            string m_strAreaId = "";
            if (objItem.m_intSHARETYPE_INT == 3)
            {
               
                for (int i = 0; i < m_objViewer.m_listDept.Items.Count; i++)
                {
                    m_strAreaId += (string)m_objViewer.m_listDept.Items[i].Tag + ",";
                }
                m_strAreaId = m_strAreaId.TrimEnd(",".ToCharArray());

            }
            objItem.m_strAREAID_VCHR = m_strAreaId;
		}


        /// <summary>
        /// ��Vo���TextBox
        /// </summary>
        /// <param name="objResult"></param>
        private void VoToTextBox(clsT_aid_bih_ordergroup_VO objResult)
		{
			if(objResult == null)
				return;
			m_strGroupID = objResult.m_strGROUPID_CHR;
			m_strGroupName = objResult.m_strNAME_CHR;
			m_objViewer.m_txtNAME_CHR.Text =objResult.m_strNAME_CHR;
			m_objViewer.m_txtDES_VCHR.Text =objResult.m_strDES_VCHR;
			//��������

			/* @update by xzf (05-11-04)
			 * �����������޸�Ϊ{˽��/����}
			 */ 
			/* @remark---------------------------
			switch (objResult.m_intSHARETYPE_INT)
			{
				case 1:
					m_objViewer.m_txtSHARETYPE_INT.Text ="����";
					break;
				case 2:
					m_objViewer.m_txtSHARETYPE_INT.Text ="����";
					break;
				case 3:
					m_objViewer.m_txtSHARETYPE_INT.Text ="��ȫ";
					break;
				default : 
					m_objViewer.m_txtSHARETYPE_INT.Text ="";
					break;
			}
			------------------------------------*/
			int shareType;
			string shareTypeText;
			shareType = objResult.m_intSHARETYPE_INT;
			switch (shareType) 
			{
				case 1:
					goto default;
				case 2:
					shareTypeText = "����";
					break;
				default:
					shareTypeText = "˽��";
					break;
			}
			m_objViewer.m_txtSHARETYPE_INT.Text = shareTypeText;
			/* <<=================================================== */
			m_objViewer.m_txtWBCODE_CHR.Text =objResult.m_strWBCODE_CHR;
			m_objViewer.m_txtPYCODE_CHR.Text =objResult.m_strPYCODE_CHR;
			m_objViewer.m_chkISSAMERECIPENO_INT.Checked =(objResult.m_intISSAMERECIPENO_INT==1)?true:false;
			/* @add by xzf (05-11-3) */
			m_objViewer.txt_creator.Text = objResult.m_strCreatorName.Trim();
			m_objViewer.txt_createDate.Text = objResult.m_strCREATEDATE_DAT.Trim();
			/* <<================================== */
		}
		private void m_LoadDataInfo()
		{
		//	m_Full();
			if(m_strGroupID==string.Empty) return;
			//���������Ϣ
			clsT_aid_bih_ordergroup_VO p_objResult =new clsT_aid_bih_ordergroup_VO();
			long lngRes =m_objDoctorAdvice.m_lngGetOrderGroupByID(m_strGroupID,out p_objResult);
		
			if(lngRes>0)
			{
				VoToTextBox(p_objResult);
			}
			//�������׳�Ա
		//	m_LoadOrderGroupDetail();
		}


        internal void m_lngGetpywb()
        {
            try
            {
                string strAny = this.m_objViewer.m_txtNAME_CHR.Text.Trim();
                clsCreateChinaCode getChinaCode = new clsCreateChinaCode();
                
                this.m_objViewer.m_txtPYCODE_CHR.Text = getChinaCode.m_strCreateChinaCode(strAny, ChinaCode.PY).Trim();
                this.m_objViewer.m_txtWBCODE_CHR.Text = getChinaCode.m_strCreateChinaCode(strAny, ChinaCode.WB).Trim();
            }
            catch
            {
                MessageBox.Show("�������������/ƴ��������벻Ҫ��Ӣ����ĸ", "icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        
        /// <summary>
        /// ���б�����
        /// </summary>
        /// <param name="m_arrObjOrder"></param>
        internal void m_LoadOrderDetail(clsBIHOrder[] m_arrObjOrder)
        {
            this.m_objViewer.m_dtvOrderGroup.Rows.Clear();
            if (m_arrObjOrder == null)
            {
                return;
            }
            for (int i = 0; i < m_arrObjOrder.Length; i++)
            {
                clsBIHOrder objOrder = m_arrObjOrder[i];
                m_objGetDataViewRow(objOrder);
            }
        }


        /// <summary>
        /// ҽ�����DATAGRIDVIEW
        /// </summary>
        /// <param name="objOrder">ҽ������</param>
        /// <param name="m_intRecipenNoUp">��һ��ҽ���ķ���(ͬ���ŵ���ҽ����������ʾ����/�١�����÷���Ƶ�ʡ�״̬������ҽ��)</param>
        public void m_objGetDataViewRow(clsBIHOrder objOrder)
        {
            this.m_objViewer.m_dtvOrderGroup.Rows.Add();

            DataGridViewRow objRow = this.m_objViewer.m_dtvOrderGroup.Rows[this.m_objViewer.m_dtvOrderGroup.RowCount - 1];

            objRow.Height = 20;
            objRow.Cells["dtv_check"].Value = "1";
            //��
            objRow.Cells["dtv_NO"].Value = this.m_objViewer.m_dtvOrderGroup.RowCount;
            //ҽ������
            clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)this.m_objViewer.m_htOrderCate[objOrder.m_strOrderDicCateID];
            if (p_objItem == null)
            {
                if (objOrder.m_strName.ToString().Trim().Equals("����ҽ��"))
                {
                    objRow.Cells["dtv_Name"].Value = objOrder.m_strName.ToString();
                    objRow.Cells["dtv_Name"].Style.Alignment = DataGridViewContentAlignment.BottomCenter;
                    objRow.Tag = objOrder;
                    return;
                }
                else if (objOrder.m_strName.ToString().Trim().Equals("ת��ҽ��"))
                {
                    objRow.Cells["dtv_Name"].Value = objOrder.m_strName.ToString();
                    objRow.Cells["dtv_Name"].Style.Alignment = DataGridViewContentAlignment.BottomCenter;
                    objRow.Tag = objOrder;
                    return;
                }
            }
            if (objOrder.m_intExecuteType == 1)
            {
                //��
                objRow.Cells["dtv_RecipeNo"].Value = " " + objOrder.m_intRecipenNo2.ToString();
            }
            //�۸�
            //objRow["Price"] =objOrder.m_dmlPrice.ToString("0.0000");

            //���������С�������ʾ����ҽ�����������ͣ��ͼ��ҽ���Ĳ�λ��Ϣ
            if (!objOrder.m_strPARTID_VCHR.Trim().Equals(""))
            {
                objRow.Cells["dtv_method"].Value = objOrder.m_strPARTNAME_VCHR;
            }
            else if (!objOrder.m_strSAMPLEID_VCHR.Trim().Equals(""))
            {
                objRow.Cells["dtv_method"].Value = objOrder.m_strSAMPLEName_VCHR;
            }


            //����  N�칲MƬ��N-��ʾ��Ժ��ҩ��������M-��ʾ��Ժ��ҩ�ϼƵ�����
            if (objOrder.m_intExecuteType == 3)
            {
                objRow.Cells["dtv_sum"].Value = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "�칲" + Convert.ToString(objOrder.m_dmlGet * objOrder.m_intOUTGETMEDDAYS_INT) + objOrder.m_strGetunit;
            }
            else
            {
                objRow.Cells["dtv_sum"].Value = "";
            }

            //��ʼִ��ʱ��
            objRow.Cells["dtv_StartDate"].Value = DateTimeToString(objOrder.m_dtExecutedate);
            //ͣ����
            objRow.Cells["dtv_Stoper"].Value = objOrder.m_strStoper;
            //���ֹͣ��
            objRow.Cells["ASSESSORFORSTOP_CHR"].Value = objOrder.m_strASSESSORFORSTOP_CHR;
            //ͣ��ʱ��
            objRow.Cells["dtv_FinishDate"].Value = DateTimeToCutYearDateString(objOrder.m_dtFinishDate);
            //ִ��ʱ��/����
            objRow.Cells["dtv_REMARK"].Value = objOrder.m_strREMARK_VCHR;
            // ҩƷ��Դ: 0 ҩ��(ȫ�Ʒ�,��ҩ); 1 �����Ա�(ֻ�շ��÷�������Ŀ,����ҩ); 2 ���һ���(ȫ�Ʒѣ�����ҩ)
            switch (objOrder.RateType)
            {
                case 0:
                    objRow.Cells["RATETYPE_INT"].Value = "ҩ��";
                    break;
                case 1:
                    objRow.Cells["RATETYPE_INT"].Value = "�Ա�";
                    break;
                case 2:
                    objRow.Cells["RATETYPE_INT"].Value = "����";
                    break;
                default:
                    break;
            }


            //У�Ի�ʿ
            objRow.Cells["ASSESSORFOREXEC_CHR"].Value = objOrder.m_strASSESSORFOREXEC_CHR;
            //¼��ʱ��
            objRow.Cells["CREATEDATE_DAT"].Value = DateTimeToString(objOrder.m_dtCreatedate); 
            //����ʱ��(��ʼʱ�䣩
            objRow.Cells["m_dtPOSTDATE_DAT"].Value = DateTimeToCutYearDateString(objOrder.m_dtPostdate);
            objRow.Cells["m_dtStartDate"].Value = DateTimeToCutYearDateString(objOrder.m_dtStartDate);

            /*<===============================*/
            //Ƥ��
            string m_strFeel = "";
            if (objOrder.m_intISNEEDFEEL == 1)
            {

                switch (objOrder.m_intFEEL_INT)
                {
                    case 0:
                        m_strFeel = " AST( ) ";
                        break;
                    case 1:
                        m_strFeel = " AST(-) ";
                        break;
                    case 2:
                        m_strFeel = " AST(+) ";
                        break;
                }

            }
            /*<==================================*/
            #region ҽ�����Ϳ����б����
            if (p_objItem != null)
            {
                objOrder.m_strOrderDicCateName = p_objItem.m_strVIEWNAME_VCHR;

                if (!objOrder.m_strExecFreqID.Trim().Equals(this.m_objViewer.m_objSpecateVo.m_strCONFREQID_CHR.Trim()))//������ҽ������ʾ����
                {
                    if (p_objItem.m_intDOSAGEVIEWTYPE == 1)
                    {
                        //����
                        if (objOrder.m_dmlDosage > 0)
                        {
                            objRow.Cells["dtv_Dosage"].Value = objOrder.m_dmlDosage.ToString() + "" + objOrder.m_strDosageUnit;
                        }
                        else
                        {
                            objRow.Cells["dtv_Dosage"].Value = "";

                        }
                    }
                    else
                    {
                        objRow.Cells["dtv_Dosage"].Value = "";
                    }
                }
                else
                {
                    objRow.Cells["dtv_Dosage"].Value = "";
                }
                if (p_objItem.m_intUSAGEVIEWTYPE == 1)
                {
                    //�÷�
                    objRow.Cells["dtv_UseType"].Value = objOrder.m_strDosetypeName;
                }
                else
                {
                    //�÷�
                    objRow.Cells["dtv_UseType"].Value = "";
                }
                if (objOrder.m_intExecuteType == 1)//���ٲ���ʾƵ�ʣ���������ʾ
                {
                    if (p_objItem.m_intExecuFrenquenceType == 1)
                    {
                        //Ƶ��
                        objRow.Cells["dtv_Freq"].Value = objOrder.m_strExecFreqName;
                    }
                    else
                    {
                        //������ʾʱ��ҽ�����е�Ϊ�޸ı�־=1ʱҲ��ʾ���� (0-��ͨ״̬,1-Ƶ���޸�)
                        if (objOrder.m_intCHARGE_INT == 1)
                        {
                            objRow.Cells["dtv_Freq"].Value = objOrder.m_strExecFreqName;//Ƶ��
                        }
                        else
                        {
                            objRow.Cells["dtv_Freq"].Value = "";//Ƶ��
                        }
                    }
                }
                else
                {
                    objRow.Cells["dtv_Freq"].Value = "";//Ƶ��
                }

                if (p_objItem.m_intAPPENDVIEWTYPE_INT == 1)
                {
                    //����
                    objRow.Cells["ATTACHTIMES_INT"].Value = objOrder.m_intATTACHTIMES_INT;
                }
                else
                {
                    //����
                    objRow.Cells["ATTACHTIMES_INT"].Value = "";
                }
                //����
                if (p_objItem.m_intQTYVIEWTYPE_INT == 1)
                {
                    if (objOrder.m_dmlGet > 0)
                    {
                        objRow.Cells["dtv_Get"].Value = objOrder.m_dmlGet.ToString() + " " + objOrder.m_strGetunit;

                    }
                    else
                    {
                        objRow.Cells["dtv_Get"].Value = "";

                    }
                }
                else
                {
                    //����
                    objRow.Cells["dtv_Get"].Value = "";
                }
            }
            else
            {
                //����
                objRow.Cells["dtv_Dosage"].Value = "";
                //Ƶ��
                objRow.Cells["dtv_Freq"].Value = "";
                //�÷�
                objRow.Cells["dtv_UseType"].Value = "";
                //����
                objRow.Cells["ATTACHTIMES_INT"].Value = "";
                //����
                objRow.Cells["dtv_Get"].Value = "";

            }
            #endregion
            //����
            objRow.Cells["dtv_Name"].Value = objOrder.m_strName + " " + objRow.Cells["dtv_Dosage"].Value.ToString() + " " + objRow.Cells["dtv_UseType"].Value.ToString() + " " + objRow.Cells["dtv_Freq"].Value.ToString() + objOrder.m_strPARTNAME_VCHR + m_strFeel;
            //���Ƹ�ʽ����
            if (p_objItem != null)
            {
                if (p_objItem.m_strVIEWNAME_VCHR.ToString().Trim() == "����ҽ��")
                {
                    objRow.Cells["dtv_Name"].Value = "   " + objRow.Cells["dtv_Name"].Value.ToString();

                }
            }

            /*<=====================================================================*/
            //ҽ��
            objRow.Cells["MedicareTypeName"].Value = objOrder.m_strMedicareTypeName;
            //ҽ������ 
            objRow.Cells["dtv_DOCTOR_VCHR"].Value = objOrder.m_strDOCTOR_VCHR;
            //�������� 
            objRow.Cells["dtv_CREATEAREA_Name"].Value = objOrder.m_strCREATEAREA_Name;
            //������ 
            objRow.Cells["dtv_DELETERNAME_VCHR"].Value = objOrder.m_strDELETERNAME_VCHR;
            //����ʱ�� 
            objRow.Cells["dtv_DELETE_DAT"].Value = objOrder.m_strDELETE_DAT;
            //�޸�������
            objRow.Cells["dtv_ChangedID"].Value = objOrder.m_strChangedName_CHR;
            //�޸���ʱ��
            objRow.Cells["dtv_ChangedDate"].Value = DateTimeToString(objOrder.m_dtChanged_DAT);

            // ͬ���ŵ���ҽ����������ʾ����/�١�����÷���Ƶ�ʡ�״̬������ҽ��
            //��/��
            if (objOrder.m_intExecuteType == 1)
            {
                objRow.Cells["dtv_ExecuteType"].Value = "����";

            }
            else if (objOrder.m_intExecuteType == 2)
            {
                objRow.Cells["dtv_ExecuteType"].Value = "��ʱ";

            }
            else if (objOrder.m_intExecuteType == 3)
            {
                objRow.Cells["dtv_ExecuteType"].Value = "��ҩ";

            }
            else
            {
                objRow.Cells["dtv_ExecuteType"].Value = "";
            }

            //��Ժ��ҩ����
            if (objOrder.m_intExecuteType == 3)
            {
                objRow.Cells["dtv_OUTGETMEDDAYS_INT"].Value = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��";
            }
            else
            {
                objRow.Cells["dtv_OUTGETMEDDAYS_INT"].Value = "";
            }
            //ҽ����������
            objRow.Cells["viewname_vchr"].Value = objOrder.m_strOrderDicCateName.ToString().Trim();
            //ҽ��״̬ ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ������ύ;6-���ֹͣ;7-�˻�;}
            switch (objOrder.m_intStatus)
            {
                case -1:
                    objRow.Cells["STATUS_INT"].Value = "����";
                    break;
                case 0:
                    objRow.Cells["STATUS_INT"].Value = "�¿�";
                    break;
                case 1:
                    objRow.Cells["STATUS_INT"].Value = "�ύ";
                    break;
                case 2:
                    objRow.Cells["STATUS_INT"].Value = "ִ��";
                    break;
                case 3:
                    objRow.Cells["STATUS_INT"].Value = "ֹͣ";
                    break;
                case 4:
                    objRow.Cells["STATUS_INT"].Value = "����";
                    break;
                case 5:
                    objRow.Cells["STATUS_INT"].Value = "ת��";
                    break;
                case 6:
                    objRow.Cells["STATUS_INT"].Value = "���ֹͣ";
                    break;
                case 7:
                    objRow.Cells["STATUS_INT"].Value = "�˻�";
                    break;
                default:
                    objRow.Cells["STATUS_INT"].Value = "";
                    break;
            }
            //����ҽ��
            objRow.Cells["CREATOR_CHR"].Value = objOrder.m_strCreator;
            //ִ����
            objRow.Cells["dtv_Executor"].Value = objOrder.m_strExecutor;

            /*<==================================================================*/
            objRow.Tag = objOrder;

        }


        public string DateTimeToCutYearDateString(DateTime dtValue)
        {
            //if(dtValue.Date==clsBIHOrder.m_dtNullDate.Date)
            if (dtValue.Date == DateTime.MinValue)
                return "";
            else
                return dtValue.ToString("MM-dd HH:mm");
        }

        /// <summary>
        /// ʱ������ת��
        /// </summary>
        /// <param name="dtValue"></param>
        /// <returns></returns>
        public string DateTimeToString(DateTime dtValue)
        {
            //if(dtValue.Date==clsBIHOrder.m_dtNullDate.Date)
            if (dtValue.Date == DateTime.MinValue)
                return "";
            else
                return dtValue.ToString("yyyy-MM-dd HH:mm");
        }

        internal void m_SelectChange()
        {
            string m_strCheck="0";
            if (this.m_objViewer.m_rdoYes.Checked == true)
            {
                m_strCheck="1";
            }
            if (this.m_objViewer.m_rdoNO.Checked == true)
            {
                m_strCheck="0";
            }
            for (int i = 0; i < this.m_objViewer.m_dtvOrderGroup.RowCount; i++)
            {
                this.m_objViewer.m_dtvOrderGroup.Rows[i].Cells["dtv_check"].Value = m_strCheck;
            }
        }

       

        internal void m_SelectItemexChange(int m_intRow, int m_intColumn)
        {
            if (m_intColumn != 0)
            {
                return;
            }
            int m_intRecipenNo = -1;//����
            string m_strCheck = "0";
            if (m_intRow >= 0 && m_intRow < this.m_objViewer.m_dtvOrderGroup.RowCount)
            {
                clsBIHOrder bihorder = (clsBIHOrder)this.m_objViewer.m_dtvOrderGroup.Rows[m_intRow].Tag;
                m_intRecipenNo = bihorder.m_intRecipenNo;
                  
                if (this.m_objViewer.m_dtvOrderGroup[m_intColumn, m_intRow].Value.ToString().Trim().Equals("0"))
                {
                  
                    m_strCheck = "1";
                }
                else
                {
                    
                    m_strCheck = "0";
                }
               
            }
            //ͬ��ѡ����ҽ��
            for (int i = 0; i < this.m_objViewer.m_dtvOrderGroup.RowCount; i++)
            {
                clsBIHOrder m_objOrder = (clsBIHOrder)this.m_objViewer.m_dtvOrderGroup.Rows[i].Tag;
                if (m_objOrder.m_intRecipenNo == m_intRecipenNo)
                {
                    this.m_objViewer.m_dtvOrderGroup.Rows[i].Cells["dtv_Check"].Value = m_strCheck;

                }
            }
        }

        #region �����¼�
        public void m_txtAreaInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Add("�������", 60, HorizontalAlignment.Left);
            lvwList.Columns.Add("��������", 90, HorizontalAlignment.Left);
            lvwList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            lvwList.Width = 170;
        }
        public void m_txtAreaFindItem(string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            clsBIHArea[] objItemArr;
            long lngRes = m_objInputOrder.m_lngFindArea(strFindCode, out objItemArr);
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                //��ȡ��Ȩ�޷��ʵĲ���ID����
                if (m_objViewer.LoginInfo != null)
                {
                    IList ilUsableAreaID = m_objViewer.LoginInfo.m_ilUsableAreaID;
                    clsDcl_InputOrder objInputOrder = new clsDcl_InputOrder();
                    objItemArr = (clsBIHArea[])(objInputOrder.GetUsableAreaObject(objItemArr, ilUsableAreaID)).ToArray(typeof(clsBIHArea));
                }
                for (int i = 0; i < objItemArr.Length; i++)
                {
                    /** @update by xzf (05-09-20) 
                     * 
                     */
                    //@ListViewItem lvi=lvwList.Items.Add(objItemArr[i].m_strAreaID);
                    ListViewItem lvi = lvwList.Items.Add(objItemArr[i].code);
                    lvi.SubItems.Add(objItemArr[i].m_strAreaName);
                    lvi.Tag = objItemArr[i].m_strAreaID;
                    /* <<======================== */
                }
            }
        }
        public void m_txtAreaSelectItem(System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                //m_objViewer.m_txtArea.Text = lviSelected.SubItems[1].Text;
                //m_objViewer.m_txtArea.Tag = lviSelected.Tag;
                string m_strAreaID = "";
                m_strAreaID = (string)lviSelected.Tag;
                for (int i = 0; i < m_objViewer.m_listDept.Items.Count; i++)
                {
                    if (((string)m_objViewer.m_listDept.Items[i].Tag).Equals(m_strAreaID))
                    {
                        return;
                    }
                }
                m_objViewer.m_listDept.Items.Add(lviSelected.SubItems[1].Text);
                m_objViewer.m_listDept.Items[m_objViewer.m_listDept.Items.Count-1].Tag = lviSelected.Tag;
            }
        }

     
     
      
        #endregion

        /// <summary>
        /// ��������{1=˽��;2=����;3=����}
        /// </summary>
        internal void m_lngSHARETYPEChange()
        {
            if (this.m_objViewer.m_txtSHARETYPE_INT.SelectedIndex != 2)
            {
                this.m_objViewer.m_listDept.Items.Clear();
            }
        }

        internal void GetTheSystemRole()
        {
            bool m_blSystemRole = false;
            this.m_objDoctorAdvice.GetTheSystemRole(this.m_objViewer.LoginInfo.m_strEmpID, out  m_blSystemRole);
            if (m_blSystemRole == true)
            {
                this.m_objViewer.m_txtArea.Enabled = true;
                this.m_objViewer.m_listDept.Enabled = true;

            }
            else
            {
                this.m_objViewer.m_txtSHARETYPE_INT.Items.Remove("����");
                this.m_objViewer.m_txtSHARETYPE_INT.Items.Remove("����");
                this.m_objViewer.m_txtArea.Enabled = false;
                this.m_objViewer.m_listDept.Enabled = false;
            }
        }

        public bool IsGridviewCheck()
        {
            bool m_blCheck = false;
            for (int i = 0; i < this.m_objViewer.m_dtvOrderGroup.RowCount; i++)
            {
                if (this.m_objViewer.m_dtvOrderGroup.Rows[i].Cells["dtv_check"].Value.ToString().Trim().Equals("1"))
                {
                    m_blCheck = true;
                    break;
                }
            }
            return m_blCheck;
        }

        /// <summary>
        /// �жϵ�ǰ���水���Ƿ����
        /// </summary>
        internal void EnableSaveBtn()
        {

            this.m_objViewer.cmdSave.Enabled = IsGridviewCheck();
            
        }
    }
}
