using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// ����ͬ���м��������
    /// </summary>
    public class clsEMR_SynchronousCaseDomain_2009 : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        /// <summary>
        /// ��ȡ����סԺ����
        /// </summary>
        /// <param name="p_dtbDept">סԺ��������</param>
        /// <returns></returns>
        public long m_lngGetAllInDept(out DataTable p_dtbDept)
        {
            //clsHospitalManagerService objServ =
            //       (clsHospitalManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHospitalManagerService));

            long lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetAllInDept(  out p_dtbDept);
            return lngRes;
        }

        #region ��ȡ����
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="p_intDeptType">iCare��������ͬ������,=0רҵ��,=1����</param>
        /// <param name="p_dtmStartDate">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEndDate">��ѯ����ʱ��</param>
        /// <param name="p_dtbResult">���</param>
        /// <returns></returns>
        public long m_lngGetSynchronousData(int p_intDeptType, DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));

            if (p_intDeptType == 0)
            {
                //lngRes = objServ.m_lngGetCaseData(objPrincipal, p_dtmStartDate, p_dtmEndDate, out p_dtbResult);
            }
            else
            {
                lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetCaseData_Dept(  p_dtmStartDate, p_dtmEndDate, out p_dtbResult);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ����(���ݲ�����Ժ�ǼǺ�)
        /// <summary>
        /// ��ȡ����(���ݲ�����Ժ�ǼǺ�)
        /// </summary>
        /// <param name="p_intDeptType">iCare��������ͬ������,=0רҵ��,=1����</param>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtbResult">���</param>
        /// <returns></returns>
        public long m_lngGetSynchronousData_reg(int p_intDeptType, string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));

            if (p_intDeptType == 0)
            {
                //lngRes = objServ.m_lngGetCaseData_reg(objPrincipal, p_strRegisterID, out p_dtbResult);
            }
            else
            {
                lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetCaseData_Dept(  p_strRegisterID, out p_dtbResult);
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡͬ��������¼
        /// </summary>
        /// <param name="p_dtmOutBegin">��Ժ��ʼʱ��</param>
        /// <param name="p_dtmOutEnd">��Ժ����ʱ��</param>
        /// <param name="p_dtbRecord">ͬ��������¼</param>
        /// <returns></returns>
        public long m_lngGetSynchronousCaseRecord(DateTime p_dtmOutBegin, DateTime p_dtmOutEnd, out DataTable p_dtbRecord)
        {
            p_dtbRecord = null;
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetSynchronousCaseRecord(p_dtmOutBegin, p_dtmOutEnd, out p_dtbRecord);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// ��ȡ�㶫����ϵͳ�ֵ�����
        /// </summary>
        /// <param name="p_dtbDict">�ֵ�����</param>
        /// <returns></returns>
        public long m_lngGetGDCaseDict(out DataTable p_dtbDict)
        {
            p_dtbDict = null;
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetGDCaseDICT(  out p_dtbDict);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ�����Ϣ
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_strType">�������</param>
        /// <param name="p_dtbResult">�������</param>
        /// <returns></returns>        
        public long m_lngGetDiagnosis(string p_strRegisterID, string p_strType, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetDiagnosis(  p_strRegisterID, p_strType, out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ������ҳ������Ժ��Ϣ
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtbResult">��Ժ��Ϣ</param>
        /// <returns></returns>
        public long m_lngGetPatientInInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetPatientInInfo(  p_strRegisterID, out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ������ҳ���������Ϣ
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtbResult">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngGetPatientDiagnosisInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetPatientDiagnosisInfo(  p_strRegisterID, out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtbResult">������Ϣ</param>
        /// <returns></returns>
        public long m_lngGetOperationInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetOperationInfo(  p_strRegisterID, out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ����ר�Ʋ������Ƽ�¼
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtbResult">����ר�Ʋ������Ƽ�¼</param>
        /// <returns></returns>
        public long m_lngGetChemotherapyInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetChemotherapyInfo(  p_strRegisterID, out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ����ר�Ʋ���ҩ�����Ƽ�¼
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtbResult">����ר�Ʋ���ҩ�����Ƽ�¼</param>
        /// <returns></returns>
        public long m_lngGetChemotherapyMedicine(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetChemotherapyMedicine(  p_strRegisterID, out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// ���Ʒ���Ӥ����¼
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtbResult">���Ʒ���Ӥ����¼</param>
        /// <returns></returns>
        public long m_lngLaborInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngLaborInfo(  p_strRegisterID, out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ������ҳ������Ϣ
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtbResult">������ҳ������Ϣ</param>
        /// <returns></returns>
        public long m_lngGetOthersCaseInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetOthersCaseInfo(  p_strRegisterID, out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// ��ȡHIS_BA1��Ľṹ
        /// </summary>
        /// <param name="p_dtbResult">������ҳ������Ϣ</param>
        /// <returns></returns>
        public long m_lngGetHIS_BA1Schema(out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetHIS_BA1Schema(out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ��ҪסԺ��ת��¼
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtbResult">��ҪסԺ��ת��¼</param>
        /// <returns></returns>
        public long m_lngGetTransferInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetTransferInfo(p_strRegisterID, out p_dtbResult);
            return lngRes;
        }

        #region �����ֵ��ѯ��������
        /// <summary>
        /// ��ֵ��ǩ��,����ID��ѯԱ��
        /// </summary>
        /// <param name="p_txtSignArr">����ǩ����Text�ؼ�</param>
        /// <param name="p_strEmpArr">ǩ����ID����</param>
        /// <param name="p_blnIsEnable">��ֵ���Ƿ��ÿؼ���Enable����</param>
        public void m_mthAddSignToTextBoxByEmpID(System.Windows.Forms.TextBoxBase[] p_txtSignArr, string[] p_strEmpArr, bool[] p_blnIsEnable)
        {
            if (p_txtSignArr == null || p_strEmpArr == null || p_txtSignArr.Length != p_strEmpArr.Length)
                return;
            //���ݹ��Ż�ȡǩ����Ϣ
            //���ڼ��ݿ��ǣ�����ʹ�� tfzhang 2006-03-12
            com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
            for (int i = 0; i < p_strEmpArr.Length; i++)
            {
                if (!string.IsNullOrEmpty(p_strEmpArr[i]))
                {
                     clsEmrEmployeeBase_VO objSign4 = null;
                    objEmployeeSign.m_lngGetEmpByID(p_strEmpArr[i].Trim(), out objSign4);
                    if (objSign4 != null)
                    {
                        objSign4.m_strTechnicalRank = objSign4.m_strTECHNICALRANK_CHR;
                        p_txtSignArr[i].Text = objSign4.m_strGetTechnicalRankAndName;
                        p_txtSignArr[i].Tag = objSign4;
                        p_txtSignArr[i].Enabled = p_blnIsEnable[i];
                    }
                }
            }
        }

        /// <summary>
        /// ��ֵ��ǩ��,���ݹ��Ų�ѯԱ��
        /// </summary>
        /// <param name="p_txtSignArr">����ǩ����Text�ؼ�</param>
        /// <param name="p_strEmpArr">ǩ����ID����</param>
        /// <param name="p_blnIsEnable">��ֵ���Ƿ��ÿؼ���Enable����</param>
        public void m_mthAddSignToTextBoxByEmpNO(System.Windows.Forms.TextBoxBase[] p_txtSignArr, string[] p_strEmpArr, bool[] p_blnIsEnable)
        {
            if (p_txtSignArr == null || p_strEmpArr == null || p_txtSignArr.Length != p_strEmpArr.Length)
                return;
            //���ݹ��Ż�ȡǩ����Ϣ
            //���ڼ��ݿ��ǣ�����ʹ�� tfzhang 2006-03-12
            com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
            for (int i = 0; i < p_strEmpArr.Length; i++)
            {
                if (!string.IsNullOrEmpty(p_strEmpArr[i]))
                {
                     clsEmrEmployeeBase_VO objSign4 = null;
                    objEmployeeSign.m_lngGetEmpByNO(p_strEmpArr[i].Trim(), out objSign4);
                    if (objSign4 != null)
                    {
                        objSign4.m_strTechnicalRank = objSign4.m_strTECHNICALRANK_CHR;
                        p_txtSignArr[i].Text = objSign4.m_strGetTechnicalRankAndName;
                        p_txtSignArr[i].Tag = objSign4;
                        p_txtSignArr[i].Enabled = p_blnIsEnable[i];
                    }
                }
            }
        }

        #region ��ICD10�����ӽ�DataGridView
        /// <summary>
        /// ��ICD10�����ӽ�DataGridView
        /// </summary>
        /// <param name="p_dgICD">��ʾICD��ϵ�DataGridView����һ��Ϊ��ʾ���ƣ��ڶ�����ʾICD10��</param>
        public bool m_blnAddICDToDataGridView(DataGridView p_dgICD)
        {
            bool blnHasGetICD = false;
            if (p_dgICD == null)
            {
                return blnHasGetICD;
            }

            DataTable dtbSource = p_dgICD.DataSource as DataTable;
            if (dtbSource == null)
            {
                return blnHasGetICD;
            }

            com.digitalwave.EMR.PublicTools.clsShowICD10QueryForm objQueryForm = new com.digitalwave.EMR.PublicTools.clsShowICD10QueryForm();
            string strName = string.Empty;
            string strCode = string.Empty;
            blnHasGetICD = objQueryForm.ShowAndReturnCodeAndName(out strName, out strCode);
            if (blnHasGetICD)
            {
                if (p_dgICD.CurrentCell.RowIndex < dtbSource.Rows.Count)
                {
                    DataRowView drvCurrent = p_dgICD.Rows[p_dgICD.CurrentCell.RowIndex].DataBoundItem as DataRowView;
                    drvCurrent["name"] = strName;
                    drvCurrent["code"] = strCode;
                }
                else
                {
                    DataRow drTemp = dtbSource.NewRow();
                    drTemp["name"] = strName;
                    drTemp["code"] = strCode;
                    dtbSource.Rows.Add(drTemp);
                }
            }
            objQueryForm = null;
            dtbSource.AcceptChanges();
            return blnHasGetICD;
        }
        #endregion

        #region ��ICD10�����ӽ��ı���
        /// <summary>
        /// ��ICD10�����ӽ��ı���
        /// </summary>
        /// <param name="p_txtName">��ʾ������Ƶ��ı���</param>
        /// <param name="p_txtCode">��ʾICD10������ı���</param>
        public void m_mthAddICDToTextBox(TextBoxBase p_txtName, TextBoxBase p_txtCode)
        {
            com.digitalwave.EMR.PublicTools.clsShowICD10QueryForm objQueryForm = new com.digitalwave.EMR.PublicTools.clsShowICD10QueryForm();
            string strName = string.Empty;
            string strCode = string.Empty;
            if (objQueryForm.ShowAndReturnCodeAndName(out strName, out strCode))
            {
                p_txtName.Text = strName;
                p_txtCode.Text = strCode;
            }
            objQueryForm = null;
        }
        #endregion

        #region ������ʽ��ӽ�DataGridView
        /// <summary>
        /// ������ʽ��ӽ�DataGridView
        /// </summary>
        /// <param name="p_dgAna">��ʾ����ʽDataGridView����7��Ϊ��ʾ���ƣ���10����ʾ�������</param>
        public bool m_blnAddAnaToDataGridView(DataGridView p_dgAna)
        {
            bool blnHasGetAna = false;
            if (p_dgAna == null)
            {
                return blnHasGetAna;
            }

            DataTable dtbSource = p_dgAna.DataSource as DataTable;
            if (dtbSource == null)
            {
                return blnHasGetAna;
            }

            com.digitalwave.EMR.PublicTools.clsShowAnaesthesiaQueryForm objQueryForm = new com.digitalwave.EMR.PublicTools.clsShowAnaesthesiaQueryForm();
            string strName = string.Empty;
            string strCode = string.Empty;
            blnHasGetAna = objQueryForm.ShowAndReturnCodeAndName(out strName, out strCode);
            if (blnHasGetAna)
            {
                if (p_dgAna.CurrentCell.RowIndex < dtbSource.Rows.Count)
                {
                    DataRowView drvCurrent = p_dgAna.Rows[p_dgAna.CurrentCell.RowIndex].DataBoundItem as DataRowView;
                    drvCurrent["ananame"] = strName;
                    drvCurrent["anacode"] = strCode;
                }
                else
                {
                    DataRow drTemp = dtbSource.NewRow();
                    drTemp["ananame"] = strName;
                    drTemp["anacode"] = strCode;
                    dtbSource.Rows.Add(drTemp);
                }
            }
            objQueryForm = null;
            dtbSource.AcceptChanges();
            return blnHasGetAna;
        }
        #endregion

        #region ��������ӽ�DataGridView
        /// <summary>
        /// ��������ӽ�DataGridView
        /// </summary>
        /// <param name="p_dgICD">��ʾ����DataGridView����3��Ϊ��ʾ���ƣ���1����ʾ��������</param>
        public bool m_blnAddOperationToDataGridView(DataGridView p_dgICD)
        {
            bool blnHasGetOP = false;
            if (p_dgICD == null)
            {
                return blnHasGetOP;
            }

            DataTable dtbSource = p_dgICD.DataSource as DataTable;
            if (dtbSource == null)
            {
                return blnHasGetOP;
            }

            com.digitalwave.EMR.PublicTools.clsShowOperationQureyForm objQueryForm = new com.digitalwave.EMR.PublicTools.clsShowOperationQureyForm();
            string strName = string.Empty;
            string strCode = string.Empty;
            blnHasGetOP = objQueryForm.ShowAndReturnCodeAndName(out strName, out strCode);
            if (blnHasGetOP)
            {
                if (p_dgICD.CurrentCell.RowIndex < dtbSource.Rows.Count)
                {
                    DataRowView drvCurrent = p_dgICD.Rows[p_dgICD.CurrentCell.RowIndex].DataBoundItem as DataRowView;
                    drvCurrent["opname"] = strName;
                    drvCurrent["opcode"] = strCode;
                }
                else
                {
                    DataRow drTemp = dtbSource.NewRow();
                    drTemp["opname"] = strName;
                    drTemp["opcode"] = strCode;
                    dtbSource.Rows.Add(drTemp);
                }
            }
            objQueryForm = null;
            dtbSource.AcceptChanges();
            return blnHasGetOP;
        }
        #endregion 
        #endregion

        /// <summary>
        /// �޸Ĳ���ͬ����¼
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_strContentXML">����</param>
        /// <param name="intType">����0δͬ��1��ͬ��</param>
        /// <returns></returns>
        public long m_lngModifyCaseRecord(string p_strRegisterID, string p_strContentXML, int intType)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009_Modify objServ =
            //       (clsEMR_SynchronousCaseServ_2009_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009_Modify));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngModifyCaseRecord(p_strRegisterID, p_strContentXML, intType);
            return lngRes;
        }

        /// <summary>
        /// ��������ͬ����¼
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_strContentXML">����</param>
        /// <param name="intType">����0δͬ��1��ͬ��</param>
        /// <returns></returns>
        public long m_lngAddNewCaseRecord(string p_strRegisterID, string p_strContentXML, int intType)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009_Modify objServ =
            //       (clsEMR_SynchronousCaseServ_2009_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009_Modify));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngAddNewCaseRecord(p_strRegisterID, p_strContentXML, intType);
            return lngRes;
        }

        /// <summary>
        /// ��������ͬ����¼
        /// </summary>
        /// <param name="p_objVOArr">����ͬ����¼����</param>
        /// <returns></returns>
        public long m_lngAddNewCaseRecordArr(clsEMR_SynchronousCase2009_VO[] p_objVOArr)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009_Modify objServ =
            //       (clsEMR_SynchronousCaseServ_2009_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009_Modify));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngAddNewCaseRecordArr(p_objVOArr);
            return lngRes;
        }

        /// <summary>
        /// �޸Ĳ���ͬ����¼
        /// </summary>
        /// <param name="p_objVOArr">����ͬ����¼����</param>
        /// <returns></returns>
        public long m_lngModifyCaseRecordArr(clsEMR_SynchronousCase2009_VO[] p_objVOArr)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009_Modify objServ =
            //       (clsEMR_SynchronousCaseServ_2009_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009_Modify));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngModifyCaseRecordArr(p_objVOArr);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ����ͬ����¼
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_objVO">����ͬ����¼</param>
        /// <returns></returns>
        public long m_lngGetCaseRecord(string p_strRegisterID, out clsEMR_SynchronousCase2009_VO p_objVO)
        {
            p_objVO = null;
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009 objServ =
            //       (clsEMR_SynchronousCaseServ_2009)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetCaseRecord(p_strRegisterID, out p_objVO);
            return lngRes;
        }

        #region ͬ������ϵͳ
        /// <summary>
        /// ͬ�����ݵ�HIS_BA1
        /// </summary>
        /// <param name="p_dtbBA1">HIS_BA1����</param>
        /// <returns></returns>
        public long m_lngCommitToHIS_BA1(DataTable p_dtbBA1)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009_Modify objServ =
            //       (clsEMR_SynchronousCaseServ_2009_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009_Modify));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngCommitToHIS_BA1(p_dtbBA1);
            return lngRes;
        }

        /// <summary>
        /// ͬ�����ݵ�HIS_BA2
        /// </summary>
        /// <param name="p_dtbBA2">HIS_BA2����</param>
        /// <returns></returns>
        public long m_lngCommitToHIS_BA2(DataTable p_dtbBA2)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009_Modify objServ =
            //       (clsEMR_SynchronousCaseServ_2009_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009_Modify));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngCommitToHIS_BA2(p_dtbBA2);
            return lngRes;
        }

        /// <summary>
        /// ͬ�����ݵ�HIS_BA3
        /// </summary>
        /// <param name="p_dtbBA3">HIS_BA3����</param>
        /// <returns></returns>
        public long m_lngCommitToHIS_BA3(DataTable p_dtbBA3)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009_Modify objServ =
            //       (clsEMR_SynchronousCaseServ_2009_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009_Modify));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngCommitToHIS_BA3(p_dtbBA3);
            return lngRes;
        }

        /// <summary>
        /// ͬ�����ݵ�HIS_BA4
        /// </summary>
        /// <param name="p_dtbBA4">HIS_BA4����</param>
        /// <returns></returns>
        public long m_lngCommitToHIS_BA4(DataTable p_dtbBA4)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009_Modify objServ =
            //       (clsEMR_SynchronousCaseServ_2009_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009_Modify));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngCommitToHIS_BA4(p_dtbBA4);
            return lngRes;
        }

        /// <summary>
        /// ͬ�����ݵ�HIS_BA5
        /// </summary>
        /// <param name="p_dtbBA5">HIS_BA5����</param>
        /// <returns></returns>
        public long m_lngCommitToHIS_BA5(DataTable p_dtbBA5)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009_Modify objServ =
            //       (clsEMR_SynchronousCaseServ_2009_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009_Modify));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngCommitToHIS_BA5(p_dtbBA5);
            return lngRes;
        }

        /// <summary>
        /// ͬ�����ݵ�HIS_BA6
        /// </summary>
        /// <param name="p_dtbBA6">HIS_BA6����</param>
        /// <returns></returns>
        public long m_lngCommitToHIS_BA6(DataTable p_dtbBA6)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009_Modify objServ =
            //       (clsEMR_SynchronousCaseServ_2009_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009_Modify));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngCommitToHIS_BA6(p_dtbBA6);
            return lngRes;
        }

        /// <summary>
        /// ͬ�����ݵ�HIS_BA7
        /// </summary>
        /// <param name="p_dtbBA7">HIS_BA7����</param>
        /// <returns></returns>
        public long m_lngCommitToHIS_BA7(DataTable p_dtbBA7)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ_2009_Modify objServ =
            //       (clsEMR_SynchronousCaseServ_2009_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ_2009_Modify));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngCommitToHIS_BA7(p_dtbBA7);
            return lngRes;
        }
        #endregion
    }
}
