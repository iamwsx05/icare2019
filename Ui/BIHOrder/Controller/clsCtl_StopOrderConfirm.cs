using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using com.digitalwave.iCare.BIHOrder.Control;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using com.digitalwave.iCare.gui.HIS;

namespace com.digitalwave.iCare.BIHOrder
{
    class clsCtl_StopOrderConfirm : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ��������
        clsDcl_ExecuteOrder m_objManage = null;
        clsDcl_InputOrder m_objInputOrder = null;
        DataTable m_dtChargeList;
        #endregion

        #region ���캯��
        public clsCtl_StopOrderConfirm()
        {
            m_objManage = new clsDcl_ExecuteOrder();
            m_objInputOrder = new clsDcl_InputOrder();

        }
        #endregion

        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmStopOrderConfirm m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmStopOrderConfirm)frmMDI_Child_Base_in;

        }
        #endregion

        /// <summary>
        /// ���ݲ��˵Ǽ���ˮ�Ų������ͣ�õ�������Ŀ
        /// </summary>
        /// <param name="m_strRegisterID"></param>
        internal void LoadTheOrders(string m_strRegisterID)
        {
            this.m_objViewer.m_dtvOrder.Rows.Clear();
            //ͣ����Ŀ����ͣҩ��Ŀ�б�
            DataTable m_dtOrderSign = null;
            List<string> m_arrRecipenNo = new List<string>();
            long ret = m_objInputOrder.m_lngGetOrderStopSignByRegisterId(m_strRegisterID, out m_dtOrderSign);
            if (m_dtOrderSign != null && m_dtOrderSign.Rows.Count > 0)
            {
                m_arrRecipenNo = GetTheAllStopOrders(m_dtOrderSign);
                if (m_arrRecipenNo.Count > 0)
                {
                    clsBIHOrder[] arrOrder = null;
                    ret = m_objInputOrder.m_lngGetOrderStopByRECIPENO_INT(m_arrRecipenNo, m_strRegisterID, out arrOrder, out m_dtChargeList);
                    if ((ret > 0) && (arrOrder != null))
                    {
                        //��һ��ҽ���ķ���
                        int m_intNo = 0;
                        for (int i = 0; i < arrOrder.Length; i++)
                        {
                            // ͬ���ŵ���ҽ����������ʾ����/�١�����÷���Ƶ�ʡ�״̬������ҽ��
                            this.m_objViewer.m_dtvOrder.Rows.Add();
                            DataGridViewRow objRow = this.m_objViewer.m_dtvOrder.Rows[this.m_objViewer.m_dtvOrder.RowCount - 1];
                            m_intNo = this.m_objViewer.m_dtvOrder.RowCount;
                            m_objGetDataViewRow(arrOrder[i], objRow, m_intNo);
                            /*<===========================*/
                        }
                        //m_mthRefreshGridColor();
                        //ˢ��ͬ��ҽ���ķ�����ɫ
                        m_mthRefreshSameReqNoColor();
                        //��̴����¼�
                        if (this.m_objViewer.m_dtvOrder.RowCount > 0)
                        {
                            this.m_objViewer.m_dtvOrder.CurrentCell = this.m_objViewer.m_dtvOrder.Rows[0].Cells[4];
                        }
                        else
                        {
                            this.m_objViewer.m_dtvChangeList.Rows.Clear();
                        }
                    }
                }
            }
            this.m_objViewer.m_dtvChangeList.CurrentCell = null;
            /*<=============================*/
            //ѡ�иò���Ҫ�����ҽ������           
        }

        /// <summary>
        /// ˢ��ͬ��ҽ���ķ�����ɫ��������ͬ���ʵ��ֶ�
        /// </summary>
        public void m_mthRefreshSameReqNoColor()
        {
            for (int i = 1; i < this.m_objViewer.m_dtvOrder.Rows.Count; i++)
            {
                clsBIHOrder order = (clsBIHOrder)m_objViewer.m_dtvOrder.Rows[i - 1].Tag;
                DataGridViewRow objRow = m_objViewer.m_dtvOrder.Rows[i];

                if (order.m_intRecipenNo == ((clsBIHOrder)m_objViewer.m_dtvOrder.Rows[i].Tag).m_intRecipenNo)
                {
                    //m_objViewer.m_dtvOrder.Rows[i - 1].Cells["dtv_RecipeNo"].Style.ForeColor = Color.SkyBlue;
                    //objRow.Cells["dtv_RecipeNo"].Style.ForeColor = Color.SkyBlue;
                    //���ص��ֶ�
                    //����dtv_RecipeNo
                    objRow.Cells["dtv_RecipeNo"].Value = "";
                    //����dtv_ExecuteType
                    objRow.Cells["dtv_ExecuteType"].Value = "";
                    //����ʱ��m_dtStartDate
                    //objRow.Cells["m_dtStartDate"].Value = "";
                    objRow.Cells["m_dtPOSTDATE_DAT"].Value = "";
                    //��ҽ����CREATOR_CHR
                    objRow.Cells["CREATOR_CHR"].Value = "";
                    //��ҽ����ASSESSORFOREXEC_CHR
                    objRow.Cells["ASSESSORFOREXEC_CHR"].Value = "";
                    //��ҩ��ͬ��Ҳ��ʾ�÷�
                    if (this.m_objViewer.m_objSpecateVo.m_strMID_MEDICINE_CHR.Trim().Equals(((clsBIHOrder)objRow.Tag).m_strOrderDicCateID))//��ҩ�����߼�
                    {
                        //�÷�dtv_UseType
                        objRow.Cells["dtv_UseType"].Value = ((clsBIHOrder)objRow.Tag).m_strDosetypeName;
                        objRow.Cells["dtv_REMARK"].Value = "";
                    }
                    else
                    {
                        //�÷�dtv_UseType
                        objRow.Cells["dtv_UseType"].Value = "";

                    }


                    // Ƶ��dtv_Freq
                    objRow.Cells["dtv_Freq"].Value = "";
                    //˵��dtv_ENTRUST
                    //objRow.Cells["dtv_ENTRUST"].Value = "";
                    //ͣ��ʱ��dtv_FinishDate
                    objRow.Cells["dtv_FinishDate"].Value = "";
                    //ͣҽ����dtv_Stoper
                    objRow.Cells["dtv_Stoper"].Value = "";
                    //��ҽ���� 
                    //objRow.Cells[""].Value = "";
                    //����ATTACHTIMES_INT
                    objRow.Cells["ATTACHTIMES_INT"].Value = "";
                    //ҽ��״̬STATUS_INT
                    objRow.Cells["STATUS_INT"].Value = "";
                    //ִ��ʱ��dtv_StartDate
                    objRow.Cells["dtv_StartDate"].Value = "";

                    //ִ����dtv_Executor
                    objRow.Cells["dtv_Executor"].Value = "";
                    //����ʱ��dtv_DELETE_DAT
                    objRow.Cells["dtv_DELETE_DAT"].Value = "";
                    //����ʱ��dtv_DELETE_DAT
                    objRow.Cells["dtv_DELETE_DAT"].Value = "";
                    //������dtv_DELETERNAME_VCHR
                    objRow.Cells["dtv_DELETERNAME_VCHR"].Value = "";

                    /*<=================================*/
                    //Ƥ��
                    string m_strFeel = "";
                    if (((clsBIHOrder)objRow.Tag).m_intISNEEDFEEL == 1)
                    {

                        switch (((clsBIHOrder)objRow.Tag).m_intFEEL_INT)
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

                    //����  ͬһ���ŵ�ҽ������ҽ�����÷���Ƶ�ʲ�����ʾ
                    objRow.Cells["dtv_Name"].Value = ((clsBIHOrder)objRow.Tag).m_strName + " " + objRow.Cells["dtv_Dosage"].Value.ToString() + "  " + objRow.Cells["dtv_UseType"].Value.ToString() + " " + m_strFeel;


                }

                //��ͣ������ͣ��ҽ��,��ִ�й��������ú�ɫ��ʾ(����ִ�г�Ժ��ҩ)

                if (order.m_intStatus == 3 || order.m_intStatus == 6 || (order.m_intExecuteType == 2 && order.m_intStatus == 2) || (order.m_intExecuteType == 3 && order.m_intStatus == 2))
                {
                    m_objViewer.m_dtvOrder.Rows[i - 1].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (order.m_intStatus == -1)
                {
                    m_objViewer.m_dtvOrder.Rows[i - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(255)))));

                }

            }
            if (m_objViewer.m_dtvOrder.RowCount > 0)
            {
                //��ͣ������ͣ��ҽ��,��ִ�й��������ú�ɫ��ʾ(���һ���Ĵ���)
                clsBIHOrder order2 = (clsBIHOrder)m_objViewer.m_dtvOrder.Rows[m_objViewer.m_dtvOrder.RowCount - 1].Tag;
                if (order2.m_intStatus == 3 || order2.m_intStatus == 6 || (order2.m_intExecuteType == 2 && order2.m_intStatus == 2) || (order2.m_intExecuteType == 3 && order2.m_intStatus == 2))
                {
                    m_objViewer.m_dtvOrder.Rows[m_objViewer.m_dtvOrder.RowCount - 1].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (order2.m_intStatus == -1)
                {
                    m_objViewer.m_dtvOrder.Rows[m_objViewer.m_dtvOrder.RowCount - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(255)))));

                }
            }

        }

        /// <summary>
        /// ҽ�����DATAGRIDVIEW
        /// </summary>
        /// <param name="objOrder">ҽ������</param>
        /// <param name="m_intRecipenNoUp">��һ��ҽ���ķ���(ͬ���ŵ���ҽ����������ʾ����/�١�����÷���Ƶ�ʡ�״̬������ҽ��)</param>
        public void m_objGetDataViewRow(clsBIHOrder objOrder, DataGridViewRow objRow, int m_intNo)
        {
            objRow.Height = 20;
            decimal m_dmlOneUse = 0;//��һ�ε�����
            //��
            objRow.Cells["dtv_NO"].Value = m_intNo;
            //ҽ������
            clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)this.m_objViewer.m_htOrderCate[objOrder.m_strOrderDicCateID];
            if (p_objItem == null)
            {

                if (objOrder.m_intTYPE_INT > 0)
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



            //��ʼִ��ʱ��
            objRow.Cells["dtv_StartDate"].Value = DateTimeToString(objOrder.m_dtExecutedate);
            //ͣ����
            objRow.Cells["dtv_Stoper"].Value = objOrder.m_strStoper;
            //���ֹͣ��
            objRow.Cells["ASSESSORFORSTOP_CHR"].Value = objOrder.m_strASSESSORFORSTOP_CHR;
            //ͣ��ʱ��
            objRow.Cells["dtv_FinishDate"].Value = DateTimeToCutYearDateString(objOrder.m_dtFinishDate);
            //objRow.Cells["dtv_ParentName"].Value = objOrder.m_strParentName;
            //ִ��ʱ��/����
            objRow.Cells["dtv_REMARK"].Value = objOrder.m_strREMARK_VCHR;
            //�Ա�ҩ (1-ȫ�Ʒ� 1-��)( 2-�÷��շ� 2-��)(3 ���Ʒ� ����)
            switch (objOrder.RateType)
            {
                case 0:
                    objRow.Cells["RATETYPE_INT"].Value = "��";
                    break;
                case 1:
                    objRow.Cells["RATETYPE_INT"].Value = "��";
                    break;
                case 2:
                    objRow.Cells["RATETYPE_INT"].Value = "";
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
                if (!objOrder.m_strExecFreqID.Trim().Equals(this.m_objViewer.m_objSpecateVo.m_strCONFREQID_CHR.Trim()))//������ҽ������ʾ�÷�
                {
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

                if (p_objItem.m_intAPPENDVIEWTYPE_INT == 1)
                {
                    //����
                    objRow.Cells["ATTACHTIMES_INT"].Value = objOrder.m_intATTACHTIMES_INT;
                    m_dmlOneUse = objOrder.m_dmlOneUse * objOrder.m_intATTACHTIMES_INT;
                }
                else
                {
                    //����
                    objRow.Cells["ATTACHTIMES_INT"].Value = "";
                    m_dmlOneUse = 0;
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
            //��Ժ��ҩ����
            string m_strOUTGETMEDDAYS_INT = "";
            //�����ֶεĿ���
            if (objOrder.m_strOrderDicCateID.Equals(this.m_objViewer.m_objSpecateVo.m_strMID_MEDICINE_CHR))//��ҩ�����߼�
            {
                objRow.Cells["dtv_sum"].Value = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "����" + Convert.ToString(objOrder.m_dmlGet + m_dmlOneUse) + objOrder.m_strGetunit;
                m_strOUTGETMEDDAYS_INT = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��"; ;
            }
            else
            {

                if (objOrder.m_intExecuteType == 3)
                {
                    objRow.Cells["dtv_sum"].Value = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "�칲" + Convert.ToString(objOrder.m_dmlGet + m_dmlOneUse) + objOrder.m_strGetunit;
                    m_strOUTGETMEDDAYS_INT = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��";
                    //objRow.Cells["dtv_OUTGETMEDDAYS_INT"].Value = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��";
                }
                else
                {
                    objRow.Cells["dtv_sum"].Value = "��" + Convert.ToString(objOrder.m_dmlGet + m_dmlOneUse) + objOrder.m_strGetunit;
                    m_strOUTGETMEDDAYS_INT = "";
                    //objRow.Cells["dtv_OUTGETMEDDAYS_INT"].Value = "";
                }
                objRow.Cells["dtv_OUTGETMEDDAYS_INT"].Value = m_strOUTGETMEDDAYS_INT;
            }

            //����
            objRow.Cells["dtv_Name"].Value = objOrder.m_strName + " " + objRow.Cells["dtv_Dosage"].Value.ToString() + " " + objRow.Cells["dtv_UseType"].Value.ToString() + " " + objRow.Cells["dtv_Freq"].Value.ToString() + m_strFeel + " " + m_strOUTGETMEDDAYS_INT;
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
            ////ҽ��ǩ��dtv_DOCTOR_SIGN
            //if (this.m_frmInput.m_blDoctorSign)
            //{
            //    if (objOrder.SIGN_GRP != null && objOrder.SIGN_INT == 1)
            //    {
            //        System.IO.MemoryStream ms = new System.IO.MemoryStream(objOrder.SIGN_GRP);
            //        Bitmap m_bpSign = new Bitmap(ms);
            //        objRow.Cells["dtv_DOCTOR_SIGN"].Value = m_bpSign;
            //        ms.Close();
            //    }
            //    else if (objOrder.SIGN_INT == 0)
            //    {

            //        Bitmap m_bpSign = new Bitmap("Picture//unsign.bmp");
            //        objRow.Cells["dtv_DOCTOR_SIGN"].Value = m_bpSign;

            //    }
            //    else
            //    {

            //        objRow.Cells["dtv_DOCTOR_SIGN"].Style.NullValue = null;
            //    }

            //    if (this.m_frmInput.m_blDoctorSign && objOrder.SIGN_INT != 1)
            //    {
            //        objRow.DefaultCellStyle.ForeColor = Color.Red;
            //    }
            //}
            //�˻���
            objRow.Cells["m_dtvSENDBACKER_CHR"].Value = objOrder.m_strSENDBACKER_CHR;



            /*<==================================================================*/
            objRow.Tag = objOrder;

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
                return dtValue.ToString("yy-MM-dd HH:mm");
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
        /// ���ص�ǰ����ͣ�û�ͣҩ��ҽ����ˮ����
        /// </summary>
        /// <param name="m_arrOrderIDs"></param>
        /// <returns></returns>
        private List<string> GetTheAllStopOrders(DataTable m_dtOrderSign)
        {
            List<string> m_arrStopOrderIds = new List<string>();
            ArrayList m_arrRECIPENO_INT = new ArrayList();
            if (m_dtOrderSign != null)
            {
                string m_strRECIPENO_INT = "";
                string STATUS_INT = "";//(������Ŀ״̬ 0-ͣ�� 1-����)
                string IFSTOP_INT = "";//ͣ�ñ�־ 1-ͣ�� 0-����
                string ITEMSRCTYPE_INT = "";//��Ŀ��Դ����1��ҩƷ��
                string IPNOQTYFLAG_INT = "";//����ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
                bool m_blStop = false;
                for (int i = 0; i < m_dtOrderSign.Rows.Count; i++)
                {
                    m_blStop = false;
                    DataRow row = m_dtOrderSign.Rows[i];
                    m_strRECIPENO_INT = row["RECIPENO_INT"].ToString().Trim();
                    STATUS_INT = row["STATUS_INT"].ToString().Trim();//(������Ŀ״̬ 0-ͣ�� 1-����)
                    IFSTOP_INT = row["IFSTOP_INT"].ToString().Trim();//ͣ�ñ�־ 1-ͣ�� 0-����
                    ITEMSRCTYPE_INT = row["ITEMSRCTYPE_INT"].ToString().Trim();//��Ŀ��Դ����1��ҩƷ��
                    IPNOQTYFLAG_INT = row["IPNOQTYFLAG_INT"].ToString().Trim();//����ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
                    if ((STATUS_INT.Equals("0") || IFSTOP_INT.Equals("1")))
                    {
                        if (!this.m_objViewer.m_blStopControl)
                        {
                            m_blStop = true;
                        }
                    }

                    if (!this.m_objViewer.m_blDeableMedControl)
                    {
                        if (ITEMSRCTYPE_INT.Equals("1") && IPNOQTYFLAG_INT.Equals("1"))
                        {
                            m_blStop = true;
                        }
                    }


                    if (m_blStop)
                    {
                        if (!m_arrStopOrderIds.Contains(m_strRECIPENO_INT))
                        {
                            m_arrStopOrderIds.Add(m_strRECIPENO_INT);
                        }
                    }

                }

            }
            return m_arrStopOrderIds;

        }

        #region Ϊ����datagridview��ֵ
        /// <summary>
        /// Ϊ����datagridview��ֵ
        /// </summary>
        /// <param name="order"></param>
        private void filltheChargeList(clsBIHOrder order)
        {
            this.m_objViewer.m_dtvChangeList.Rows.Clear();
            if (m_dtChargeList != null && m_dtChargeList.Rows.Count > 0)
            {

                DataView myDataView = new DataView(m_dtChargeList);
                myDataView.RowFilter = "orderid_chr='" + order.m_strOrderID + "'";
                myDataView.Sort = "FLAG_INT";
                if (myDataView.Count <= 0)
                {
                    return;
                }
                clsChargeForDisplay[] m_arrObjItem;
                m_mthGetChargeListFromDateTable(myDataView, out m_arrObjItem);
                int k = 0;
                for (int i = 0; i < m_arrObjItem.Length; i++)
                {

                    k++;
                    this.m_objViewer.m_dtvChangeList.Rows.Add();
                    DataGridViewRow row1 = this.m_objViewer.m_dtvChangeList.Rows[this.m_objViewer.m_dtvChangeList.RowCount - 1];
                    row1.Cells["seq"].Value = Convert.ToString(k);
                    row1.Cells["chargeName"].Value = m_arrObjItem[i].m_strName;
                    row1.Cells["ITEMSPEC_VCHR"].Value = m_arrObjItem[i].m_strSPEC_VCHR;
                    row1.Cells["ChargeClass"].Value = "";
                    switch (m_arrObjItem[i].m_intType)
                    {
                        case 0:
                            row1.Cells["ChargeClass"].Value = "����Ŀ";
                            break;
                        case 1:
                            row1.Cells["ChargeClass"].Value = "������Ŀ";
                            break;
                        case 2:
                            row1.Cells["ChargeClass"].Value = "�÷�����";
                            break;
                        case 3:
                            row1.Cells["ChargeClass"].Value = "����¼��";
                            break;
                    }

                    row1.Cells["ChargePrice"].Value = m_arrObjItem[i].m_dblPrice.ToString();
                    row1.Cells["get_count"].Value = m_arrObjItem[i].m_dblDrawAmount.ToString() + " " + m_arrObjItem[i].m_strUNIT_VCHR;
                    row1.Cells["countSum"].Value = m_arrObjItem[i].m_dblMoney.ToString();
                    switch (m_arrObjItem[i].m_intCONTINUEUSETYPE_INT)
                    {
                        case 1:
                            row1.Cells["xuClass"].Value = "�״���";
                            break;
                        case 0:
                            row1.Cells["xuClass"].Value = "������";
                            break;
                        default:
                            row1.Cells["xuClass"].Value = " -- ";
                            break;
                    }

                    row1.Cells["excuteDept"].Value = m_arrObjItem[i].m_strClacareaName_chr;
                    row1.Cells["YBClass"].Value = m_arrObjItem[i].m_strYBClass;
                    row1.Cells["IPNOQTYFLAG_INT"].Value = "";
                    if (m_arrObjItem[i].m_intITEMSRCTYPE_INT == 1)
                    {
                        if (m_arrObjItem[i].m_intIPNOQTYFLAG_INT == 1)
                        {
                            row1.Cells["IPNOQTYFLAG_INT"].Value = "ȱҩ";
                            row1.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    if (m_arrObjItem[i].m_intIFSTOP_INT == 1)
                    {
                        row1.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    }
                    row1.Tag = m_arrObjItem[i];

                    //��� seq
                    //��Ŀ���� chargeName
                    //�������� ChargeClass
                    //���� ChargePrice
                    //���� get_count
                    //�ܽ�� countSum
                    //�������� xuClass
                    //ִ�п��� excuteDept
                    //ҽ������ YBClass
                }
                this.m_objViewer.m_dtvChangeList.CurrentCell = null;
            }

        }


        #endregion

        #region ���ñ�ת��Ϊ������ϸ����
        /// <summary>
        /// ���ñ�ת��Ϊ������ϸ����
        /// </summary>
        /// <param name="objRow"></param>
        /// <param name="m_arrObjItem"></param>
        private void m_mthGetChargeListFromDateTable(DataView objRow, out clsChargeForDisplay[] m_arrObjItem)
        {
            m_arrObjItem = new clsChargeForDisplay[objRow.Count];
            for (int i = 0; i < objRow.Count; i++)
            {
                m_arrObjItem[i] = new clsChargeForDisplay();
                m_arrObjItem[i].m_strChargeID = clsConverter.ToString(objRow[i]["CHARGEITEMID_CHR"]).Trim();
                //�շ���Ŀ����
                m_arrObjItem[i].m_strName = clsConverter.ToString(objRow[i]["CHARGEITEMNAME_CHR"]).Trim();
                double dblNum = 0;
                //if (objMedicineItemArr[i1].m_objChargeItem.m_strITEMID_CHR.Trim() == objMedicineItemArr[i1].m_strChiefItemID.Trim())//�Ƿ����շ���Ŀ
                //{
                //    dblNum = p_dblDraw;
                //    //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                //    m_arrObjItem[i].m_intType = 2;
                //}
                //else
                //{
                //    dblNum = System.Convert.ToDouble(m_dmlGetChargeNotMainItem(objRecipeFreq, objMedicineItemArr[i1]));
                //    //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                //    m_arrObjItem[i].m_intType = 1;
                //}
                //����
                if (!objRow[i]["UNITPRICE_DEC"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_dblPrice = double.Parse(clsConverter.ToString(objRow[i]["UNITPRICE_DEC"]).Trim());
                }
                if (!objRow[i]["AMOUNT_DEC"].ToString().Trim().Equals(""))
                {
                    dblNum = double.Parse(clsConverter.ToString(objRow[i]["AMOUNT_DEC"]).Trim());
                }
                /*<---------------------------------*/
                //����
                m_arrObjItem[i].m_dblDrawAmount = dblNum;

                //�ϼƽ��
                m_arrObjItem[i].m_dblMoney = m_arrObjItem[i].m_dblPrice * dblNum;
                //�������� {-1=���÷��շѣ�ҩƷ�շѵȣ�;0=������;1=ȫ������;2-��������}
                if (!objRow[i]["CONTINUEUSETYPE_INT"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intCONTINUEUSETYPE_INT = int.Parse(objRow[i]["CONTINUEUSETYPE_INT"].ToString().Trim());
                }

                //�Ƿ�������ҽ��	{0=��1=��} ������ҽ������ʾҩƷ������Ϣ��
                // m_arrObjItem[i].m_intIsContinueOrder = (blnIsConOrder) ? (1) : (0);
                //�Ƿ�ȱҩ
                // m_arrObjItem[i].m_strNoqtyFLag = objMedicineItemArr[i1].m_strNoqtyFLag;
                // ���Ͽ�������
                m_arrObjItem[i].m_strClacarea_chr = clsConverter.ToString(objRow[i]["CLACAREA_CHR"]).Trim();
                m_arrObjItem[i].m_strClacareaName_chr = clsConverter.ToString(objRow[i]["deptname_vchr"]).Trim();
                //�ݴ�סԺ������Ŀ�շ���Ŀִ�пͻ������ˮ��
                m_arrObjItem[i].m_strSeq_int = clsConverter.ToString(objRow[i]["SEQ_INT"]).Trim(); ;
                m_arrObjItem[i].m_strYBClass = clsConverter.ToString(objRow[i]["INSURACEDESC_VCHR"]).Trim();
                m_arrObjItem[i].m_strUNIT_VCHR = clsConverter.ToString(objRow[i]["UNIT_VCHR"]).Trim();
                //�շ�����Դ�� 0-��������Ŀ��1-������Ŀ,2���������÷���3���Զ����¿�
                if (!objRow[i]["FLAG_INT"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_intType = clsConverter.ToInt(objRow[i]["FLAG_INT"].ToString().Trim());
                // סԺ������Ŀ�շ���Ŀִ�пͻ���VO
                //objItem.m_objORDERCHARGEDEPT_VO = objMedicineItemArr[i1].m_objORDERCHARGEDEPT_VO;
                if (!objRow[i]["ITEMSRCTYPE_INT"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intITEMSRCTYPE_INT = int.Parse(objRow[i]["ITEMSRCTYPE_INT"].ToString().Trim());
                }
                if (!objRow[i]["IPNOQTYFLAG_INT"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intIPNOQTYFLAG_INT = int.Parse(objRow[i]["IPNOQTYFLAG_INT"].ToString().Trim());
                }
                m_arrObjItem[i].m_strSPEC_VCHR = clsConverter.ToString(objRow[i]["ITEMSPEC_VCHR"].ToString().Trim());
                m_arrObjItem[i].m_intIFSTOP_INT = clsConverter.ToInt(objRow[i]["IFSTOP_INT"].ToString().Trim());
            }
        }
        #endregion

        internal void OrderListSelect()
        {
            if (this.m_objViewer.m_dtvOrder.CurrentCell != null && this.m_objViewer.m_dtvOrder.RowCount > 0)
            {

                clsBIHOrder order = (clsBIHOrder)this.m_objViewer.m_dtvOrder.Rows[this.m_objViewer.m_dtvOrder.CurrentCell.RowIndex].Tag;

                if (order != null)
                {
                    filltheChargeList(order);
                    TheSameNoRowSelect(order);
                }
            }
        }

        private void TheSameNoRowSelect(clsBIHOrder order)
        {
            string m_strID = order.m_strRegisterID + "," + order.m_intRecipenNo.ToString() + ";";
            string m_strTemp = "";

            for (int i = 0; i < this.m_objViewer.m_dtvOrder.Rows.Count; i++)
            {
                clsBIHOrder Exeorder = (clsBIHOrder)this.m_objViewer.m_dtvOrder.Rows[i].Tag;
                m_strTemp = Exeorder.m_strRegisterID + "," + Exeorder.m_intRecipenNo.ToString() + ";";
                if (m_strTemp.Equals(m_strID))
                {
                    this.m_objViewer.m_dtvOrder.Rows[i].Selected = true;
                }

            }
            /*<====================================*/

        }

        internal void OrderStop()
        {
            ArrayList m_arrOrder = new ArrayList();
            ArrayList m_arrOrderIdCan = new ArrayList();
            GetTheSelectOrders(ref m_arrOrder);
            #region ˢ�µ�ǰҽ�����ݣ�Ȼ�����ж�

            List<string> m_arrORDERID_CHR = new List<string>();
            string m_strOrderID = "";
            for (int i = 0; i < m_arrOrder.Count; i++)
            {
                m_strOrderID = ((clsBIHOrder)m_arrOrder[i]).m_strOrderID;
                if (!m_arrORDERID_CHR.Contains(m_strOrderID))
                {
                    m_arrORDERID_CHR.Add(m_strOrderID);
                }
            }

            clsBIHOrder[] arrOrder = null;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService m_objService = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            (new weCare.Proxy.ProxyIP()).Service.m_lngGetArrOrderByOrderID(m_arrORDERID_CHR, out arrOrder);
            if (arrOrder != null && arrOrder.Length > 0)
            {
                for (int i = 0; i < arrOrder.Length; i++)
                {
                    clsBIHOrder order = arrOrder[i];
                    DataGridViewRow row = GetTheGridRowByOrder(order.m_strOrderID);
                    this.m_objGetDataViewRow(order, row, row.Index + 1);
                }

            }
            this.m_mthRefreshSameReqNoColor();
            m_arrOrder.Clear();
            GetTheSelectOrders(ref m_arrOrder);
            #endregion

            if (m_arrOrder.Count <= 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {

                bool m_blCan = false;
                for (int i = 0; i < m_arrOrder.Count; i++)
                {
                    clsBIHOrder order = (clsBIHOrder)m_arrOrder[i];
                    if (order.m_intExecuteType == 1 && order.m_intStatus == 2)
                    {
                        m_arrOrderIdCan.Add(order.m_strOrderID);
                        m_blCan = true;
                    }
                }
                if (!m_blCan)
                {
                    MessageBox.Show("��ǰ������û�з��Ͽ�ֹͣ��ִ���еĳ���!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (m_arrOrderIdCan.Count > 0)
                {
                    if (MessageBox.Show("ȷ�Ͻ���ͣ��������?", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        string[] m_strOrderIDs = (string[])(m_arrOrderIdCan.ToArray(typeof(string)));

                        long m_lngRef = this.m_objInputOrder.m_lngStopOrder(m_strOrderIDs, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);

                        MessageBox.Show("�����ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTheOrders(this.m_objViewer.m_strRegisterID);
                    }
                }
            }

        }

        private void GetTheSelectOrders(ref ArrayList m_arrOrder)
        {
            ArrayList m_arrSelectOrders = new ArrayList();
            string temp = "";
            for (int i = 0; i < this.m_objViewer.m_dtvOrder.SelectedRows.Count; i++)
            {
                clsBIHOrder order = (clsBIHOrder)this.m_objViewer.m_dtvOrder.SelectedRows[i].Tag;
                temp = order.m_strRegisterID + "," + order.m_intRecipenNo + ";";
                if (!m_arrSelectOrders.Contains(temp))
                {
                    m_arrSelectOrders.Add(temp);
                }
            }

            for (int i = 0; i < this.m_objViewer.m_dtvOrder.RowCount; i++)
            {
                clsBIHOrder order = (clsBIHOrder)this.m_objViewer.m_dtvOrder.Rows[i].Tag;
                temp = order.m_strRegisterID + "," + order.m_intRecipenNo + ";";
                if (m_arrSelectOrders.Contains(temp))
                {
                    m_arrOrder.Add(order);
                }
            }
        }

        internal void OrderDelete()
        {
            ArrayList m_arrOrder = new ArrayList();
            ArrayList m_arrOrderIdCan = new ArrayList();
            GetTheSelectOrders(ref m_arrOrder);
            if (m_arrOrder.Count <= 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {

                #region ˢ�µ�ǰҽ�����ݣ�Ȼ�����ж�

                List<string> m_arrORDERID_CHR = new List<string>();
                string m_strOrderID = "";
                for (int i = 0; i < m_arrOrder.Count; i++)
                {
                    m_strOrderID = ((clsBIHOrder)m_arrOrder[i]).m_strOrderID;
                    if (!m_arrORDERID_CHR.Contains(m_strOrderID))
                    {
                        m_arrORDERID_CHR.Add(m_strOrderID);
                    }
                }

                clsBIHOrder[] arrOrder = null;
                //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService m_objService = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
                //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                (new weCare.Proxy.ProxyIP()).Service.m_lngGetArrOrderByOrderID(m_arrORDERID_CHR, out arrOrder);
                if (arrOrder != null && arrOrder.Length > 0)
                {
                    for (int i = 0; i < arrOrder.Length; i++)
                    {
                        clsBIHOrder order = arrOrder[i];
                        DataGridViewRow row = GetTheGridRowByOrder(order.m_strOrderID);
                        this.m_objGetDataViewRow(order, row, row.Index + 1);
                    }

                }
                this.m_mthRefreshSameReqNoColor();
                m_arrOrder.Clear();
                GetTheSelectOrders(ref m_arrOrder);
                #endregion

                string m_strMessage = "", m_strMessage2 = "", m_strMessage3 = "";
                for (int i = 0; i < m_arrOrder.Count; i++)
                {
                    clsBIHOrder BihOrder = (clsBIHOrder)m_arrOrder[i];
                    if (BihOrder.m_strCreatorID == this.m_objViewer.LoginInfo.m_strEmpID || BihOrder.m_strDOCTORID_CHR == this.m_objViewer.LoginInfo.m_strEmpID)
                    {

                    }
                    else
                    {
                        m_strMessage2 += "\r\n" + "  { " + BihOrder.m_strName + " }";

                    }
                    if (BihOrder.m_intStatus == 0 || BihOrder.m_intStatus == 1 || BihOrder.m_intStatus == 7)
                    {

                    }
                    else if (BihOrder.m_intTYPE_INT == 3 || BihOrder.m_intTYPE_INT == 4)
                    {
                    }
                    else
                    {
                        m_strMessage3 += "\r\n" + "  { " + BihOrder.m_strName + " }";

                    }
                    if (!m_arrOrderIdCan.Contains(BihOrder.m_strOrderID))
                    {
                        m_arrOrderIdCan.Add(BihOrder.m_strOrderID);
                    }

                }
                if (!m_strMessage2.Trim().Equals(""))
                {
                    m_strMessage2 = "\r\n" + " û���㹻Ȩ��ɾ������ҽ��" + m_strMessage2;
                }
                if (!m_strMessage3.Trim().Equals(""))
                {
                    m_strMessage3 = "\r\n" + " ����ɾ����ǰ״̬��ҽ��" + m_strMessage3;
                }
                m_strMessage = m_strMessage2 + m_strMessage3;
                if (!m_strMessage.Trim().Equals(""))
                {
                    MessageBox.Show(m_strMessage, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //ɾ��ҽ��
                if (m_arrOrderIdCan.Count <= 0)
                {
                    return;
                }
                else
                {
                    if (MessageBox.Show("ȷ�Ͻ���ɾ��������?", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        this.m_objViewer.Cursor = Cursors.WaitCursor;
                        ArrayList m_arrContinue = new ArrayList();//������ҽ��Ҫɾ������
                        for (int i = 0; i < m_arrOrder.Count; i++)
                        {
                            clsBIHOrder order = (clsBIHOrder)m_arrOrder[i];
                            if (this.m_objViewer.m_objSpecateVo.m_strCONFREQID_CHR.Trim().Equals(order.m_strExecFreqID.Trim()))
                            {
                                m_arrContinue.Add(order.m_strOrderID);
                            }
                        }
                        string[] m_strOrderID2 = null;
                        if (m_arrContinue.Count > 0)
                        {
                            m_strOrderID2 = (string[])m_arrContinue.ToArray(typeof(string));
                        }
                        string[] m_strOrderIDs = (string[])(m_arrOrderIdCan.ToArray(typeof(string)));
                        long m_lngRef = m_objInputOrder.m_lngDeleteOrder(m_strOrderIDs, m_strOrderID2);
                        this.m_objViewer.Cursor = Cursors.Default;
                        MessageBox.Show("�����ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTheOrders(this.m_objViewer.m_strRegisterID);
                    }
                }
            }

        }

        /// <summary>
        /// ���ݵ�ǰҽ���Ż�����ڵ���
        /// </summary>
        /// <param name="m_strOrderID"></param>
        /// <returns></returns>
        public DataGridViewRow GetTheGridRowByOrder(string m_strOrderID)
        {
            DataGridViewRow row = null;
            for (int i = 0; i < this.m_objViewer.m_dtvOrder.RowCount; i++)
            {
                if (((clsBIHOrder)this.m_objViewer.m_dtvOrder.Rows[i].Tag).m_strOrderID.Equals(m_strOrderID))
                {
                    row = this.m_objViewer.m_dtvOrder.Rows[i];
                }
            }
            return row;
        }
    }
}
