using System;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using System.Drawing;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsController_SampleReceive ��ժҪ˵����
    /// </summary>
    public class clsController_SampleReceive : com.digitalwave.GUI_Base.clsController_Base
    {
        frmSampleReceive m_objViewer;
        clsDomainController_SampleManage m_objManage;
        int intState = 0;

        #region constructMethod
        public clsController_SampleReceive()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_objManage = new clsDomainController_SampleManage();
        }
        #endregion

        #region setGUIApperance
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmSampleReceive)frmMDI_Child_Base_in;
        }
        #endregion

        #region ViewerInital
        public void m_mthViewerInital()
        {
            #region ����ԭ��
            clsDomainController_ApplicationManage doMain = new clsDomainController_ApplicationManage();
            DataTable dt = doMain.GetRejectReason();
            if (dt != null && dt.Rows.Count > 0)
            {
                m_objViewer.m_cboSampleBackReason.DropDownStyle = ComboBoxStyle.DropDownList;
                m_objViewer.m_cboSampleBackReason.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    m_objViewer.m_cboSampleBackReason.Items.Add(dr["dictname_vchr"].ToString());
                }
            }
            #endregion

            //��ȡ���еı걾����
            m_mthGetAllSampleType();
            //��ѯ������յı걾��Ϣ
            //m_mthGetTodayReceivedSample();
            m_objViewer.m_txtReceiveEmp.m_StrEmployeeID = m_objViewer.LoginInfo.m_strEmpID;
            //��ѯ�������
            m_mthGetAllCheckCategory();
        }
        #endregion

        #region ����BarCode��ѯ����VO
        public long m_lngFindSampleVOByBarCode(string p_strBarCode, out clsT_OPR_LIS_SAMPLE_VO[] objSampleVO)
        {
            long lngRes = 0;
            objSampleVO = null;
            lngRes = m_objManage.m_lngGetSampleVOByBarcode(p_strBarCode, out objSampleVO);
            return lngRes;
        }
        #endregion

        #region ��ȡ�����ѽ��յı걾
        public void m_mthGetTodayReceivedSample()
        {
            string strDatFrom = System.DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string strDatTo = System.DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            m_mthGetReceivedSampleByCondition(strDatFrom, strDatTo, "", "", "", "", "", "", "", "");
        }
        #endregion

        #region [U]���ݺ��ձ걾��Ϣ���ÿؼ�
        public void m_mthSetViewerControls(clsSampleReceive_VO objResult)
        {
            m_mthClearAll();
            m_objViewer.m_txtBarCode.Text = objResult.m_strBarCode;
            //����tagΪReceiveVO
            m_objViewer.m_txtBarCode.Tag = objResult;
            m_objViewer.m_txtCheckContent.Text = objResult.m_strCheckContent;
            if (objResult.m_strReceiveEmpID != null && objResult.m_strReceiveEmpID != "")
            {
                m_objViewer.m_txtReceiveEmp.Text = objResult.m_strReceiveEmpDec;
                m_objViewer.m_txtReceiveEmp.m_StrEmployeeID = objResult.m_strReceiveEmpID;
            }
            else if (objResult.m_intStatus == 2)
            {
                if (string.IsNullOrEmpty(m_objViewer.m_strSubmitDoctorId))
                {
                    m_objViewer.m_txtReceiveEmp.m_StrEmployeeID = this.m_strGetOprator();
                }
                else
                {
                    m_objViewer.m_txtReceiveEmp.m_StrEmployeeID = m_objViewer.m_strSubmitDoctorId;
                }
            }
            m_objViewer.m_txtPatientType.Text = objResult.m_strPatientTypeDec;
            m_objViewer.m_txtSampleType.Text = objResult.m_strSampleType;
            if (objResult.m_strReceiveDat != null && objResult.m_strReceiveDat != "")
            {
                m_objViewer.m_dtpReceiveDat.Value = DateTime.Parse(objResult.m_strReceiveDat);
            }
            else
            {
                m_objViewer.m_dtpReceiveDat.Value = DateTime.Now;
            }
            if (objResult.m_strIsSpecial == "1")
            {
                m_objViewer.m_txtFlagSepcial.Text = "���⴦��";
            }
            else
            {
                m_objViewer.m_txtFlagSepcial.Text = "";
            }
            if (objResult.m_strIsEmergency == "1")
            {
                m_objViewer.m_txtFlagEmergency.Text = "����";
            }
            else
            {
                m_objViewer.m_txtFlagEmergency.Text = "";
            }
            this.m_objViewer.m_txtPatientName.Text = objResult.m_strPatientName;


            if (IsPay(objResult.m_strApplicationID))
            {
                this.m_objViewer.m_txtChargeState.Text = "���շ�";
            }
            else
            {

                this.m_objViewer.m_txtChargeState.Text = "";
            }

            if (!string.IsNullOrEmpty(objResult.m_strSendsample_empid_chr))
            {
                this.m_objViewer.m_txtSendPeople.m_StrEmployeeID = objResult.m_strSendsample_empid_chr;
                ctlEmpTextBox emp = new ctlEmpTextBox();
                emp.m_StrEmployeeID = objResult.m_strSendsample_empid_chr;
                this.m_objViewer.m_txtSendPeople.Text = emp.m_StrEmployeeName;
            }
            else
            {
                if (intState == 1)
                {
                    this.m_objViewer.m_txtSendPeople.Clear();
                    this.m_objViewer.m_txtSendPeople.m_StrEmployeeID = null;
                    this.m_objViewer.m_txtSendPeople.m_StrEmployeeName = null;
                }
            }
            //switch(objResult.m_intChargeState)
            //{
            //    case 0:
            //        this.m_objViewer.m_txtChargeState.Text = "";
            //        break;
            //    case 1:
            //        this.m_objViewer.m_txtChargeState.Text = "δ�շ�";
            //        this.m_objViewer.m_txtChargeState.ForeColor = System.Drawing.Color.Red;
            //        break;
            //    case 2:
            //        this.m_objViewer.m_txtChargeState.Text = "���շ�";
            //        this.m_objViewer.m_txtChargeState.ForeColor = System.Drawing.Color.Black;
            //        break;
            //    default:
            //        this.m_objViewer.m_txtChargeState.Text = "";
            //        break;
            //}
        }
        #endregion

        /// <summary>
        /// ��ȡ�շ�״̬
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        private bool IsPay(string applicationId)
        {
            clsChargeStatusVO chargeStatusVO = null;
            clsChargeInfoStatusSmp.s_obj.m_lngFind(applicationId, out chargeStatusVO);
            if (chargeStatusVO == null)
            {
                return false;
            }

            if (chargeStatusVO.m_intChargeStatus == 1)
            {
                return true;
            }

            return false;
        }

        #region ����������ѯ�Ѳɼ�,��δ���յı걾��Ϣ
        /// <summary>
        /// ����������ѯ�Ѳɼ�,��δ���յı걾��Ϣ
        /// </summary>
        /// <param name="p_strDatFrom"></param>
        /// <param name="p_strDatTo"></param>
        /// <param name="p_strSampleType"></param>
        /// <param name="p_strAcceptEmp"></param>
        /// <param name="p_strPatientName"></param>
        /// <param name="p_strPatientCardID"></param>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strCheckCategory"></param>
        /// <param name="p_objResultArr"></param>
        public void m_mthGetUnReceivedSampleByCondition(string p_strDatFrom, string p_strDatTo, string p_strSampleType,
            string p_strReceiveEmp, string p_strPatientName, string p_strPatientCardID, string p_strBarCode, string p_strCheckCategory, string p_strSendPeopleID, string p_strInPatientNum)
        {
            m_objViewer.m_lstUnReceive.Items.Clear();

            long lngRes = 0;
            clsSampleUnReceive_VO[] objResultArr = null;
            lngRes = m_objManage.m_lngGetUnReceivedSampleByCondition(p_strDatFrom, p_strDatTo, p_strSampleType, p_strReceiveEmp, p_strPatientName, p_strPatientCardID, p_strBarCode, p_strCheckCategory, p_strSendPeopleID, p_strInPatientNum,
                out objResultArr);

            m_objViewer.m_lstUnReceive.BeginUpdate();
            m_objViewer.m_lstUnReceive.Items.Clear();
            try
            {
                if (lngRes > 0 && objResultArr != null && objResultArr.Length > 0)
                {
                    clsSampleUnReceive_VO objTemp = null;
                    ListViewItem lviTemp = null;

                    for (int index = 0; index < objResultArr.Length; index++)
                    {
                        objTemp = objResultArr[index];
                        lviTemp = new ListViewItem(new string[] { objTemp.m_strBarCode, objTemp.m_strPatientName, objTemp.m_strSampleType,
                        objTemp.m_strCheckContent, objTemp.m_strPatientTypeDec, objTemp.m_strSamplingDat, objTemp.m_strIsEmergency == "1"? "��":"",
                        objTemp.m_strIsSpecial == "1"? "��":"", objTemp.m_strSamplingeEmpDec});


                        if (objTemp.m_intIsGreen == 1)
                        {
                            lviTemp.BackColor = Color.Orange;
                        }
                        lviTemp.Name = objTemp.m_strBarCode;
                        lviTemp.Tag = objTemp;
                        m_objViewer.m_lstUnReceive.Items.Add(lviTemp);

                    }
                }
            }
            finally
            {
                m_objViewer.m_lstUnReceive.EndUpdate();
            }
        }
        #endregion

        #region ����������ѯ�ѽ��յ�����
        public void m_mthGetReceivedSampleByCondition(string p_strDatFrom, string p_strDatTo, string p_strSampleType, string p_strReceiveEmp, string p_strPatientName, string p_strPatientCardID, string p_strBarCode, string p_strCheckCategory, string p_strSendPeopleID, string p_strInPatientNum)
        {

            m_objViewer.m_lsvReceiveSampleList.Items.Clear();
            long lngRes = 0;
            clsSampleReceive_VO[] objResultArr = null;
            lngRes = m_objManage.m_lngGetReceivedSampleByCondition(p_strDatFrom, p_strDatTo, p_strSampleType, p_strReceiveEmp, p_strPatientName, p_strPatientCardID, p_strBarCode, p_strCheckCategory, p_strSendPeopleID, p_strInPatientNum,
                out objResultArr);

            #region ԭ����(ע�͵�)
            //if (lngRes > 0 && objResultArr != null)
            //{
            //    for (int i = 0; i < objResultArr.Length; i++)
            //    {
            //        ListViewItem objlsvItem = new ListViewItem();
            //        objlsvItem.Text = objResultArr[i].m_strBarCode;
            //        objlsvItem.SubItems.Add(objResultArr[i].m_strPatientName);
            //        objlsvItem.SubItems.Add(objResultArr[i].m_strSampleType);
            //        objlsvItem.SubItems.Add(objResultArr[i].m_strCheckContent);
            //        objlsvItem.SubItems.Add(objResultArr[i].m_strPatientTypeDec);
            //        //					objlsvItem.SubItems.Add(objResultArr[i].m_strReceiveEmpDec);
            //        //					if(objResultArr[i].m_strReceiveDat != null && objResultArr[i].m_strReceiveDat != "")
            //        //					{
            //        //						objlsvItem.SubItems.Add(DateTime.Parse(objResultArr[i].m_strReceiveDat).ToShortDateString());
            //        //					}
            //        //					else
            //        //					{
            //        objlsvItem.SubItems.Add(objResultArr[i].m_strReceiveDat);
            //        //					}
            //        if (objResultArr[i].m_strIsEmergency == "1")
            //        {
            //            objlsvItem.SubItems.Add("��");
            //        }
            //        else
            //        {
            //            objlsvItem.SubItems.Add("");
            //        }
            //        if (objResultArr[i].m_strIsSpecial == "1")
            //        {
            //            objlsvItem.SubItems.Add("��");
            //        }
            //        else
            //        {
            //            objlsvItem.SubItems.Add("");
            //        }
            //        objlsvItem.SubItems.Add(objResultArr[i].m_strReceiveEmpDec);
            //        objlsvItem.Tag = objResultArr[i];
            //        m_objViewer.m_lsvReceiveSampleList.Items.Add(objlsvItem);
            //    }
            //}
            #endregion

            System.Collections.Generic.List<clsSampleReceive_VO> objSampleSingle;
            DataView dtvAppuserGroupDetail;
            lngRes = m_lngGetReceivedSampleNotSingle(p_strDatFrom, p_strDatTo, p_strSampleType, p_strReceiveEmp, p_strPatientName, p_strPatientCardID, p_strBarCode, p_strCheckCategory, p_strSendPeopleID, p_strInPatientNum, out objSampleSingle, out dtvAppuserGroupDetail);   //���벢���Ҹ��ϼ������ݵı걾

            #region �ϲ����������
            int intCountResult = ((objResultArr == null) ? 0 : objResultArr.Length);
            int intCountSampleSingle = ((objSampleSingle == null) ? 0 : objSampleSingle.Count);
            int intCount = intCountResult + intCountSampleSingle;
            clsSampleReceive_VO[] objAllResultArr = new clsSampleReceive_VO[intCount];
            DateTime dtResult;   //objResultArr�ĺ���ʱ��
            DateTime dtSampleSingle;   //objSampleSingle�ĺ���ʱ��

            int i2 = 0;
            int i3 = 0;
            for (int i1 = 0; i1 < intCount && (i2 < intCountSampleSingle || i3 < intCountResult); i1++)   //�ϲ����������
            {
                dtSampleSingle = dtCompare(1, objSampleSingle, i2, intCountSampleSingle);
                dtResult = dtCompare(2, objResultArr, i3, intCountResult);

                if (objSampleSingle != null && (dtSampleSingle.CompareTo(dtResult) < 0 || i3 == intCountResult) && i2 < intCountSampleSingle)
                {
                    //����dtSampleSingle,i2++
                    ListViewItem objlsvItem2 = new ListViewItem();
                    objlsvItem2.Text = objSampleSingle[i2].m_strBarCode;
                    objlsvItem2.SubItems.Add(objSampleSingle[i2].m_strPatientName);
                    objlsvItem2.SubItems.Add(objSampleSingle[i2].m_strSampleType);
                    objlsvItem2.SubItems.Add(objSampleSingle[i2].m_strCheckContent);
                    objlsvItem2.SubItems.Add(objSampleSingle[i2].m_strPatientTypeDec);
                    objlsvItem2.SubItems.Add(objSampleSingle[i2].m_strReceiveDat);

                    if (objSampleSingle[i2].m_strIsEmergency == "1")
                    {
                        objlsvItem2.SubItems.Add("��");
                    }
                    else
                    {
                        objlsvItem2.SubItems.Add("");
                    }
                    if (objSampleSingle[i2].m_strIsSpecial == "1")
                    {
                        objlsvItem2.SubItems.Add("��");
                    }
                    else
                    {
                        objlsvItem2.SubItems.Add("");
                    }
                    objlsvItem2.SubItems.Add(objSampleSingle[i2].m_strReceiveEmpDec);
                    objlsvItem2.SubItems.Add(objSampleSingle[i2].m_strSendName);

                    if (objSampleSingle[i2].m_intIsGreen == 1)
                    {
                        objlsvItem2.BackColor = Color.Orange;
                    }

                    objlsvItem2.Tag = objSampleSingle[i2];
                    m_objViewer.m_lsvReceiveSampleList.Items.Add(objlsvItem2);

                    objAllResultArr[i1] = objSampleSingle[i2];
                    i2++;
                }
                else if (objResultArr != null && i3 < intCountResult)
                {

                    ListViewItem objlsvItem = new ListViewItem();
                    objlsvItem.Text = objResultArr[i3].m_strBarCode;
                    objlsvItem.SubItems.Add(objResultArr[i3].m_strPatientName);
                    objlsvItem.SubItems.Add(objResultArr[i3].m_strSampleType);
                    objlsvItem.SubItems.Add(objResultArr[i3].m_strCheckContent);
                    objlsvItem.SubItems.Add(objResultArr[i3].m_strPatientTypeDec);
                    objlsvItem.SubItems.Add(objResultArr[i3].m_strReceiveDat);

                    if (objResultArr[i3].m_strIsEmergency == "1")
                    {
                        objlsvItem.SubItems.Add("��");
                    }
                    else
                    {
                        objlsvItem.SubItems.Add("");
                    }
                    if (objResultArr[i3].m_strIsSpecial == "1")
                    {
                        objlsvItem.SubItems.Add("��");
                    }
                    else
                    {
                        objlsvItem.SubItems.Add("");
                    }
                    objlsvItem.SubItems.Add(objResultArr[i3].m_strReceiveEmpDec);
                    objlsvItem.SubItems.Add(objResultArr[i3].m_strSendName);

                    if (objResultArr[i3].m_intIsGreen == 1)
                    {
                        objlsvItem.BackColor = Color.Orange;
                    }

                    objlsvItem.Tag = objResultArr[i3];
                    m_objViewer.m_lsvReceiveSampleList.Items.Add(objlsvItem);

                    objAllResultArr[i1] = objResultArr[i3];
                    i3++;
                }
                else
                {
                    if (MessageBox.Show(m_objViewer, "��ʾ������ѯ��Ŀ��ƥ�䣬������ " + i1 + " �������", "ϵͳ��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
            #endregion

            #region ���������(ע�͵�)
            //if (lngRes > 0 && objSampleSingle.Count > 0)   //�����б�
            //{
            //    for (int i = 0; i < objSampleSingle.Count; i++)
            //    {
            //        ListViewItem objlsvItem2 = new ListViewItem();
            //        objlsvItem2.Text = objSampleSingle[i].m_strBarCode;
            //        objlsvItem2.SubItems.Add(objSampleSingle[i].m_strPatientName);
            //        objlsvItem2.SubItems.Add(objSampleSingle[i].m_strSampleType);
            //        objlsvItem2.SubItems.Add(objSampleSingle[i].m_strCheckContent);
            //        objlsvItem2.SubItems.Add(objSampleSingle[i].m_strPatientTypeDec);

            //        objlsvItem2.SubItems.Add(objSampleSingle[i].m_strReceiveDat);

            //        if (objSampleSingle[i].m_strIsEmergency == "1")
            //        {
            //            objlsvItem2.SubItems.Add("��");
            //        }
            //        else
            //        {
            //            objlsvItem2.SubItems.Add("");
            //        }
            //        if (objSampleSingle[i].m_strIsSpecial == "1")
            //        {
            //            objlsvItem2.SubItems.Add("��");
            //        }
            //        else
            //        {
            //            objlsvItem2.SubItems.Add("");
            //        }
            //        objlsvItem2.SubItems.Add(objSampleSingle[i].m_strReceiveEmpDec);
            //        objlsvItem2.Tag = objSampleSingle[i];
            //        m_objViewer.m_lsvReceiveSampleList.Items.Add(objlsvItem2);
            //    }
            //}
            #endregion

            m_objViewer.m_btnQuery.Tag = objAllResultArr;   //�����ѯ���
        }

        #region ���غ���ʱ�亯��
        /// <summary>
        /// ���غ���ʱ�亯��
        /// baojian.mo 2007.09.12 add
        /// </summary>
        /// <param name="intFlag">���������ʶ 1-������������ 2-�����������</param>
        /// <param name="objResult">��������</param>
        /// <param name="intCur">��ǰά��</param>
        /// <param name="intMax">���ά��</param>
        /// <returns></returns>
        private DateTime dtCompare(int intFlag, object objResult, int intCur, int intMax)
        {
            switch (intFlag)
            {
                case 1:
                    if (objResult != null && intMax > 0)
                    {
                        System.Collections.Generic.List<clsSampleReceive_VO> objSampleSingle = (System.Collections.Generic.List<clsSampleReceive_VO>)objResult;
                        intCur = (intCur == intMax ? intMax - 1 : intCur);
                        return Convert.ToDateTime(objSampleSingle[intCur].m_strReceiveDat);
                    }
                    else
                    {
                        return Convert.ToDateTime("1900-01-01 00:00:00");
                    }
                case 2:
                    if (objResult != null)
                    {
                        clsSampleReceive_VO[] objResultArr = (clsSampleReceive_VO[])objResult;
                        intCur = (intCur == intMax ? intMax - 1 : intCur);
                        return Convert.ToDateTime(objResultArr[intCur].m_strReceiveDat);
                    }
                    else
                    {
                        return DateTime.Now;
                    }
                default:
                    return DateTime.Now;
            }
        }
        #endregion

        #region ���벢���Ҹ��ϼ������ݵı걾
        /// <summary>
        /// ���벢���Ҹ��ϼ������ݵı걾
        /// baojian.mo 2007.09.12 add
        /// </summary>
        /// <param name="p_strDatFrom"></param>
        /// <param name="p_strDatTo"></param>
        /// <param name="p_strSampleType"></param>
        /// <param name="p_strReceiveEmp"></param>
        /// <param name="p_strPatientName"></param>
        /// <param name="p_strPatientCardID"></param>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strCheckCategory"></param>
        /// <param name="objSampleSingle">���ط�����ÿ����¼</param>
        /// <returns></returns>
        private long m_lngGetReceivedSampleNotSingle(string p_strDatFrom, string p_strDatTo, string p_strSampleType, string p_strReceiveEmp, string p_strPatientName, string p_strPatientCardID, string p_strBarCode, string p_strCheckCategory, string p_strSendPeopleID, string p_strInPatientNum, out System.Collections.Generic.List<clsSampleReceive_VO> objSampleSingle, out DataView dtvAppuserGroupDetail)
        {
            long lngRes = 0;
            objSampleSingle = null;
            dtvAppuserGroupDetail = null;
            if (p_strCheckCategory != "")
            {
                DataTable dtbDetail = new DataTable();
                System.Collections.Generic.List<string> objSingleAppUnit = new System.Collections.Generic.List<string>();   //��¼���뵥Ԫ������ 
                objSampleSingle = new System.Collections.Generic.List<clsSampleReceive_VO>();      //��������Ľ��
                clsSampleReceive_VO objTemp;
                clsSampleReceive_VO[] objResultArr = null;
                string strUnionCheckContent = "";   //�ϲ����������

                lngRes = m_objManage.m_lngGetAppuserGroupDetail(p_strCheckCategory, out dtbDetail);   //�����Զ������������뵥Ԫ
                if (lngRes > 0 && dtbDetail.Rows.Count > 0)
                {
                    dtvAppuserGroupDetail = new DataView(dtbDetail);
                }

                lngRes = m_objManage.m_lngGetReceivedSampleByCondition(p_strDatFrom, p_strDatTo, p_strSampleType, p_strReceiveEmp, p_strPatientName, p_strPatientCardID, p_strBarCode, "", p_strSendPeopleID, p_strInPatientNum, out objResultArr);
                if (lngRes > 0 && dtvAppuserGroupDetail != null && objResultArr != null)
                {
                    for (int i1 = 0; i1 < objResultArr.Length; i1++)
                    {
                        if (objResultArr[i1].m_strCheckContent.IndexOf(',') > 0)   //�Զ�����Ϊ���ϼ����־
                        {
                            strUnionCheckContent = "";
                            objSingleAppUnit = com.digitalwave.iCare.gui.HIS.clsPublic.m_ArrGettoken(objResultArr[i1].m_strCheckContent, ",");

                            for (int i2 = 0; i2 < objSingleAppUnit.Count; i2++)
                            {
                                dtvAppuserGroupDetail.RowFilter = "apply_unit_name_vchr = '" + objSingleAppUnit[i2] + "'";
                                if (dtvAppuserGroupDetail.Count > 0)
                                {
                                    strUnionCheckContent += objSingleAppUnit[i2].ToString() + ",";
                                }
                            }
                            strUnionCheckContent = (strUnionCheckContent.Length > 0 ? strUnionCheckContent.Substring(0, strUnionCheckContent.Length - 1) : "");
                            if (strUnionCheckContent != "")
                            {
                                objTemp = new clsSampleReceive_VO();
                                objTemp.m_strBarCode = objResultArr[i1].m_strBarCode;
                                objTemp.m_strPatientName = objResultArr[i1].m_strPatientName;
                                objTemp.m_strSampleType = objResultArr[i1].m_strSampleType;
                                objTemp.m_strCheckContent = strUnionCheckContent;   //�ֵ��������
                                objTemp.m_strPatientTypeDec = objResultArr[i1].m_strPatientTypeDec;
                                objTemp.m_strReceiveDat = objResultArr[i1].m_strReceiveDat;
                                objTemp.m_strIsEmergency = objResultArr[i1].m_strIsEmergency;
                                objTemp.m_strIsSpecial = objResultArr[i1].m_strIsSpecial;
                                objTemp.m_strReceiveEmpDec = objResultArr[i1].m_strReceiveEmpDec;
                                objSampleSingle.Add(objTemp);
                            }
                        }
                    }
                }
            }
            return lngRes;
        }
        #endregion
        #endregion

        #region ��ϲ�ѯ�ѽ��յ�������Ϣ
        public void m_mthQryReceivedSample()
        {
            string strDatFrom = m_objViewer.m_dtpDatFrom.Value.ToString("yyyy-MM-dd 00:00:00");
            string strDatTo = m_objViewer.m_dtpDatTo.Value.ToString("yyyy-MM-dd 23:59:59");
            string strSampleType = "";
            string strReceiveEmp = m_objViewer.m_txtQryReceiveEmp.m_StrEmployeeID;
            if (m_objViewer.m_cboQrySampleType.SelectedIndex > 0)
            {
                strSampleType = m_objViewer.m_cboQrySampleType.Text.ToString().Trim();
            }
            string strPatientName = m_objViewer.m_txtPatientNameForSearch.Text;
            string strPatientCardId = m_objViewer.m_txtPatientCardID.Text;
            string strBarCode = m_objViewer.m_txtBarCodeForSearch.Text;
            string strCheckCategory = (m_objViewer.cboCheckCategory.SelectedIndex == 0 ? "" : m_objViewer.cboCheckCategory.Text.Trim());
            string m_strSendPeopleID = m_objViewer.m_txtSendPeopleForSearch.m_StrEmployeeID;
            string m_strInPatientNum = m_objViewer.m_txtInPatientNum.Text;

            if (m_objViewer.m_tabSampleList.SelectedTab == m_objViewer.tabUnReceive)
            {
                m_mthGetUnReceivedSampleByCondition(strDatFrom, strDatTo, strSampleType, strReceiveEmp, strPatientName, strPatientCardId, strBarCode, strCheckCategory, m_strSendPeopleID, m_strInPatientNum);
            }
            else
            {
                m_mthGetReceivedSampleByCondition(strDatFrom, strDatTo, strSampleType, strReceiveEmp, strPatientName, strPatientCardId, strBarCode, strCheckCategory, m_strSendPeopleID, m_strInPatientNum);
            }
            //m_mthGetReceivedSampleByCondition(strDatFrom,strDatTo,strSampleType,strReceiveEmp,strPatientName,strPatientCardId,strBarCode,strCheckCategory);
        }
        #endregion

        #region ��ȡ���е���������
        public void m_mthGetAllSampleType()
        {
            try
            {
                DataTable dtbSampleType = null;
                new clsDomainController_SampleManage().m_lngGetSampleTypeList(out dtbSampleType);
                if (dtbSampleType != null)
                {
                    DataRow dr = dtbSampleType.NewRow();
                    dr["SAMPLE_TYPE_DESC_VCHR"] = "ȫ��";
                    dtbSampleType.Rows.InsertAt(dr, 0);
                    m_objViewer.m_cboQrySampleType.DataSource = dtbSampleType;
                    m_objViewer.m_cboQrySampleType.DisplayMember = "SAMPLE_TYPE_DESC_VCHR";
                    m_objViewer.m_cboQrySampleType.ValueMember = "SAMPLE_TYPE_ID_CHR";
                }
            }
            catch
            {
                m_objViewer.m_cboQrySampleType.DataSource = null;
                m_objViewer.m_cboQrySampleType.DisplayMember = null;
                m_objViewer.m_cboQrySampleType.ValueMember = null;
            }
        }
        #endregion

        #region ��ȡ���еļ������
        public void m_mthGetAllCheckCategory()
        {
            try
            {
                DataTable dtbCheckCategory = null;
                new clsDomainController_SampleManage().m_lngGetCheckCategoryList(out dtbCheckCategory);
                if (dtbCheckCategory != null)
                {
                    DataRow dr = dtbCheckCategory.NewRow();
                    dr["user_group_name_vchr"] = "ȫ��";
                    dtbCheckCategory.Rows.InsertAt(dr, 0);
                    m_objViewer.cboCheckCategory.DataSource = dtbCheckCategory;
                    m_objViewer.cboCheckCategory.DisplayMember = "user_group_name_vchr";
                    m_objViewer.cboCheckCategory.ValueMember = "user_group_name_vchr";
                    m_objViewer.cboCheckCategory.Tag = dtbCheckCategory;
                }
            }
            catch
            {
                m_objViewer.cboCheckCategory.DataSource = null;
                m_objViewer.cboCheckCategory.DisplayMember = null;
                m_objViewer.cboCheckCategory.ValueMember = null;
            }
        }
        #endregion

        #region [U]���ձ걾
        /// <summary>
        /// ǩ������
        /// </summary>
        public void m_mthReceiveSample()
        {
            if (m_objViewer.m_txtBarCode.Tag == null)
            {
                if (m_objViewer.m_txtBarCode.Text.ToString().Trim() != "")
                {
                    m_mthGetUnreceivedSampleInfoByBarCode();
                    if (m_objViewer.m_txtBarCode.Tag == null)
                        return;
                }
                else
                {
                    return;
                }
            }

            clsSampleReceive_VO objSampleInfo = (clsSampleReceive_VO)m_objViewer.m_txtBarCode.Tag;

            if (m_objViewer.intSendPepoleID == 1)
            {
                if (string.IsNullOrEmpty(m_objViewer.m_txtSendPeople.m_StrEmployeeID))
                {
                    MessageBox.Show("�������ͼ���");
                    return;
                }
            }
            //�жϸñ걾��״̬
            if (objSampleInfo.m_intStatus > 2 && objSampleInfo.m_intStatus < 7)
            {
                MessageBox.Show("�ñ걾�ѱ����գ�");
                return;
            }
            else if (objSampleInfo.m_intStatus == 7)
            {
                MessageBox.Show("�ñ걾�ѱ��˻أ�");
                return;
            }
            string strSampleID = objSampleInfo.m_strSampleID;
            string strEmpID = null;

            if (!string.IsNullOrEmpty(m_objViewer.m_strSubmitDoctorId))
            {
                strEmpID = m_objViewer.m_strSubmitDoctorId;

            }
            else
            {
                strEmpID = m_objViewer.LoginInfo.m_strEmpID;

            }
            //m_objViewer.m_txtReceiveEmp.m_blnQuery(strEmpID);
            objSampleInfo.m_strReceiveEmpID = strEmpID;

            string strSendPeopleID = m_objViewer.m_txtSendPeople.m_StrEmployeeID;
            objSampleInfo.m_strSendsample_empid_chr = strSendPeopleID;

            long lngRes = 0;
            lngRes = m_objManage.m_lngReceiveSample(3, strSampleID, m_objViewer.m_dtpReceiveDat.Value.ToString().Trim(),
                strEmpID, strSendPeopleID);
            if (lngRes > 0)
            {

                //				m_mthGetTodayReceivedSample();
                ListViewItem objlsvItem = new ListViewItem();
                objlsvItem.Text = objSampleInfo.m_strBarCode;
                objlsvItem.SubItems.Add(objSampleInfo.m_strPatientName);
                objlsvItem.SubItems.Add(objSampleInfo.m_strSampleType);
                objlsvItem.SubItems.Add(objSampleInfo.m_strCheckContent);
                objlsvItem.SubItems.Add(objSampleInfo.m_strPatientTypeDec);
                objlsvItem.SubItems.Add(m_objViewer.m_dtpReceiveDat.Value.ToString().Trim());
                if (objSampleInfo.m_strIsEmergency == "1")
                {
                    objlsvItem.SubItems.Add("��");
                }
                else
                {
                    objlsvItem.SubItems.Add("");
                }
                if (objSampleInfo.m_strIsSpecial == "1")
                {
                    objlsvItem.SubItems.Add("��");
                }
                else
                {
                    objlsvItem.SubItems.Add("");
                }
                objlsvItem.SubItems.Add(m_objViewer.m_txtReceiveEmp.m_StrEmployeeName);
                objlsvItem.SubItems.Add(m_objViewer.m_txtSendPeople.m_StrEmployeeName);
                objSampleInfo.m_intStatus = 3;
                objlsvItem.Tag = objSampleInfo;
                m_objViewer.m_lsvReceiveSampleList.Items.Add(objlsvItem);

                // ��δ���ձ걾�б���ɾ��
                if (m_objViewer.m_lstUnReceive.Items != null && m_objViewer.m_lstUnReceive.Items.Count > 0)
                {
                    foreach (ListViewItem lvi in m_objViewer.m_lstUnReceive.Items)
                    {
                        if (lvi.Name == objSampleInfo.m_strBarCode)
                        {
                            lvi.Remove();
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("�걾����ʧ�ܣ�");
            }
            m_mthClearAll();
        }
        #endregion

        #region [U]����BarCode��ѯ�걾�����Ϣ
        /// <summary>
        /// ����BarCode��ѯ�걾�����Ϣ
        /// </summary>
        public bool m_mthGetUnreceivedSampleInfoByBarCode()
        {
            bool isFinish = false;
            string strBarCode = m_objViewer.m_txtBarCode.Text.ToString().Trim();
            if (strBarCode == "")
            {
                MessageBox.Show("BarCode����Ϊ�գ�", "iCare- �걾����");
                return isFinish;
            }

            long lngRes = 0;
            clsSampleReceive_VO objResult = null;

            //����BarCode��ѯ�����յ�������Ϣ
            lngRes = m_objManage.m_mthGetUnReceivedSampleByBarCode(strBarCode, out objResult);
            if (lngRes > 0 && objResult != null)
            {
                #region ȡ���շ���Ϣ
                if (objResult.m_strPatientType != "1")
                {
                    long lngRes2 = clsDomainController_ApplicationManage.m_lngGetChargeState(objResult.m_strApplicationID);
                    switch (lngRes2)
                    {
                        case 0:
                            MessageBox.Show(this.m_objViewer, "�������ӵ��շ�ϵͳ���������Ա��ϵ��", "i-Care");
                            objResult.m_intChargeState = 0;
                            break;
                        case 1:
                            objResult.m_intChargeState = 1;
                            break;
                        case 2:
                            objResult.m_intChargeState = 2;
                            break;
                        default:
                            objResult.m_intChargeState = 0;
                            break;
                    }
                }
                #endregion

                m_mthSetViewerControls(objResult);
                isFinish = true;
            }
            else
            {
                MessageBox.Show("�޴�BarCode��Ϣ��", "iCare- �걾����");
                this.m_objViewer.m_txtBarCode.Focus();
                this.m_objViewer.m_txtBarCode.SelectAll();
                isFinish = false;
            }
            return isFinish;
        }
        #endregion

        #region ��ʾѡ��listView�걾��Ϣ
        public void m_mthShowLsvSampleInfo()
        {
            if (m_objViewer.m_lsvReceiveSampleList.SelectedItems.Count <= 0)
                return;
            clsSampleReceive_VO objResult = (clsSampleReceive_VO)m_objViewer.m_lsvReceiveSampleList.SelectedItems[0].Tag;
            #region ȡ���շ���Ϣ
            if (objResult.m_strPatientType != "1")
            {
                long lngRes2 = clsDomainController_ApplicationManage.m_lngGetChargeState(objResult.m_strApplicationID);
                switch (lngRes2)
                {
                    case 0:
                        MessageBox.Show(this.m_objViewer, "�������ӵ��շ�ϵͳ���������Ա��ϵ��", "i-Care");
                        objResult.m_intChargeState = 0;
                        break;
                    case 1:
                        objResult.m_intChargeState = 1;
                        break;
                    case 2:
                        objResult.m_intChargeState = 2;
                        break;
                    default:
                        objResult.m_intChargeState = 0;
                        break;
                }
            }
            #endregion
            intState = 1;
            m_mthSetViewerControls(objResult);
            intState = 0;
        }
        #endregion

        #region Clear
        public void m_mthClearAll()
        {
            m_objViewer.m_txtBarCode.Clear();
            m_objViewer.m_txtBarCode.Tag = null;
            m_objViewer.m_txtCheckContent.Clear();
            m_objViewer.m_txtQryReceiveEmp.Clear();
            m_objViewer.m_txtReceiveEmp.Clear();
            m_objViewer.m_txtReceiveEmp.m_StrEmployeeID = null;
            m_objViewer.m_txtSampleType.Clear();
            m_objViewer.m_txtFlagEmergency.Clear();
            m_objViewer.m_txtFlagSepcial.Clear();
            m_objViewer.m_dtpReceiveDat.Value = System.DateTime.Now;
            m_objViewer.m_txtPatientType.Clear();
            m_objViewer.m_txtChargeState.Clear();
            m_objViewer.m_txtPatientName.Clear();
        }
        #endregion

        public string m_strGetOprator()
        {
            return this.m_objViewer.LoginInfo.m_strEmpID;
        }

        #region Ԥ��/��ӡ��ѯ���
        /// <summary>
        ///  Ԥ��/��ӡ��ѯ���
        /// baojian.mo 2007.09.10 add
        /// </summary>
        public void m_mthReport()
        {
            if (m_objViewer.m_btnQuery.Tag != null)
            {
                clsSampleReceive_VO[] objQueryResult = (clsSampleReceive_VO[])m_objViewer.m_btnQuery.Tag;
                Sybase.DataWindow.DataStore dsReport = new Sybase.DataWindow.DataStore();
                dsReport.LibraryList = Application.StartupPath + "\\PBReport.pbl";
                dsReport.DataWindowObject = "t_lis_samplereceive";

                dsReport.Modify("t_datefrom.text = '" + m_objViewer.m_dtpDatFrom.Value.ToString("yyyy-MM-dd") + "'");
                dsReport.Modify("t_dateto.text = '" + m_objViewer.m_dtpDatTo.Value.ToString("yyyy-MM-dd") + "'");
                dsReport.Modify("t_category.text = '" + m_objViewer.cboCheckCategory.Text + "'");

                int m_intRow = 0;
                DateTime dtTmp;
                for (int i1 = 0; i1 < objQueryResult.Length; i1++)
                {
                    m_intRow = dsReport.InsertRow(0);
                    dsReport.SetItemDouble(m_intRow, "num_int", Convert.ToDouble(i1));
                    dsReport.SetItemString(m_intRow, "barcode_vchr", objQueryResult[i1].m_strBarCode);
                    dsReport.SetItemString(m_intRow, "patientcardid_chr", objQueryResult[i1].m_strPatientCardID);
                    dsReport.SetItemString(m_intRow, "patient_inhospitalno_chr", objQueryResult[i1].m_strInpatientID);
                    dsReport.SetItemString(m_intRow, "patient_name_vchr", objQueryResult[i1].m_strPatientName);
                    dsReport.SetItemString(m_intRow, "sex_chr", objQueryResult[i1].m_strPatientSex);
                    dsReport.SetItemString(m_intRow, "age_chr", objQueryResult[i1].m_strAge);
                    dsReport.SetItemString(m_intRow, "check_content_vchr", objQueryResult[i1].m_strCheckContent);
                    dtTmp = Convert.ToDateTime(objQueryResult[i1].m_strReceiveDat);
                    dsReport.SetItemString(m_intRow, "accept_dat", dtTmp.ToString("MM/dd hh:mm:ss"));
                }
                com.digitalwave.iCare.gui.HIS.clsPublic.PrintDialog(dsReport);
            }
        }
        #endregion

        #region �����걾
        /// <summary>
        /// �����걾
        /// </summary>
        public void m_mthSampleFeedBack()
        {
            clsSampleReceive_VO objSampleInfo = (clsSampleReceive_VO)m_objViewer.m_txtBarCode.Tag;
            if (objSampleInfo == null)
            {
                return;
            }
            long lngRes = 0;
            int intStatus = 0;
            string m_strIsSampleBack = null;
            lngRes = m_objManage.m_lngQuerySampleStatus(objSampleInfo.m_strSampleID, out intState, out m_strIsSampleBack);
            if (lngRes < 0)
            {
                return;
            }
            if (intState > 3)
            {
                MessageBox.Show(m_objViewer, "�ñ걾���ܻ��ˣ��ѳ����", "�걾������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (m_strIsSampleBack == "1")
            {
                MessageBox.Show(m_objViewer, "�ñ걾�ѻ���", "�걾������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            clslissample_feedback objSampleFeedBack = new clslissample_feedback();
            objSampleFeedBack.m_strAppl_Empid_chr = objSampleInfo.m_strDeptID;
            objSampleFeedBack.m_strBack_Empid_chr = m_objViewer.m_strSubmitDoctorId;
            objSampleFeedBack.m_strBedno_chr = objSampleInfo.m_strBedID;
            objSampleFeedBack.m_strPatient_Inhospitalno_vchr = objSampleInfo.m_strInpatientID;
            objSampleFeedBack.m_strPatient_Name_vchr = objSampleInfo.m_strPatientName;
            objSampleFeedBack.m_strSample_Back_Reason_vchr = m_objViewer.m_cboSampleBackReason.Text;
            objSampleFeedBack.m_strSample_id_chr = objSampleInfo.m_strSampleID;
            lngRes = m_objManage.m_lngInsertSampleFeedBack(objSampleFeedBack);
            if (lngRes > 0)
            {
                MessageBox.Show(m_objViewer, "���˱걾�ɹ�", "�걾������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        #endregion
    }
}