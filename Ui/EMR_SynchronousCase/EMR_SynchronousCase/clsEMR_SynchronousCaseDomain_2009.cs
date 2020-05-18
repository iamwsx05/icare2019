using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// 病案同步中间件工具类
    /// </summary>
    public class clsEMR_SynchronousCaseDomain_2009 : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        /// <summary>
        /// 获取所有住院科室
        /// </summary>
        /// <param name="p_dtbDept">住院科室数据</param>
        /// <returns></returns>
        public long m_lngGetAllInDept(out DataTable p_dtbDept)
        {
            //clsHospitalManagerService objServ =
            //       (clsHospitalManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHospitalManagerService));

            long lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetAllInDept(  out p_dtbDept);
            return lngRes;
        }

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="p_intDeptType">iCare病案科室同步配置,=0专业组,=1科室</param>
        /// <param name="p_dtmStartDate">查询开始时间</param>
        /// <param name="p_dtmEndDate">查询结束时间</param>
        /// <param name="p_dtbResult">结果</param>
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

        #region 获取数据(根据病人入院登记号)
        /// <summary>
        /// 获取数据(根据病人入院登记号)
        /// </summary>
        /// <param name="p_intDeptType">iCare病案科室同步配置,=0专业组,=1科室</param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">结果</param>
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
        /// 获取同步病案记录
        /// </summary>
        /// <param name="p_dtmOutBegin">出院开始时间</param>
        /// <param name="p_dtmOutEnd">出院结束时间</param>
        /// <param name="p_dtbRecord">同步病案记录</param>
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
        /// 获取广东病案系统字典数据
        /// </summary>
        /// <param name="p_dtbDict">字典数据</param>
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
        /// 获取诊断信息
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strType">诊断类型</param>
        /// <param name="p_dtbResult">诊断内容</param>
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
        /// 获取病案首页病人入院信息
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">入院信息</param>
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
        /// 获取病案首页病人诊断信息
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">诊断信息</param>
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
        /// 获取病人手术信息
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">手术信息</param>
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
        /// 获取肿瘤专科病人治疗记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">肿瘤专科病人治疗记录</param>
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
        /// 获取肿瘤专科病人药物治疗记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">肿瘤专科病人药物治疗记录</param>
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
        /// 产科分娩婴儿记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">产科分娩婴儿记录</param>
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
        /// 获取病案首页其他信息
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">病案首页其他信息</param>
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
        /// 获取HIS_BA1表的结构
        /// </summary>
        /// <param name="p_dtbResult">病案首页其他信息</param>
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
        /// 获取简要住院周转记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">简要住院周转记录</param>
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

        #region 界面字典查询公共方法
        /// <summary>
        /// 赋值单签名,根据ID查询员工
        /// </summary>
        /// <param name="p_txtSignArr">放置签名的Text控件</param>
        /// <param name="p_strEmpArr">签名者ID数组</param>
        /// <param name="p_blnIsEnable">赋值后是否置控件的Enable属性</param>
        public void m_mthAddSignToTextBoxByEmpID(System.Windows.Forms.TextBoxBase[] p_txtSignArr, string[] p_strEmpArr, bool[] p_blnIsEnable)
        {
            if (p_txtSignArr == null || p_strEmpArr == null || p_txtSignArr.Length != p_strEmpArr.Length)
                return;
            //根据工号获取签名信息
            //出于兼容考虑，过渡使用 tfzhang 2006-03-12
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
        /// 赋值单签名,根据工号查询员工
        /// </summary>
        /// <param name="p_txtSignArr">放置签名的Text控件</param>
        /// <param name="p_strEmpArr">签名者ID数组</param>
        /// <param name="p_blnIsEnable">赋值后是否置控件的Enable属性</param>
        public void m_mthAddSignToTextBoxByEmpNO(System.Windows.Forms.TextBoxBase[] p_txtSignArr, string[] p_strEmpArr, bool[] p_blnIsEnable)
        {
            if (p_txtSignArr == null || p_strEmpArr == null || p_txtSignArr.Length != p_strEmpArr.Length)
                return;
            //根据工号获取签名信息
            //出于兼容考虑，过渡使用 tfzhang 2006-03-12
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

        #region 将ICD10诊断添加进DataGridView
        /// <summary>
        /// 将ICD10诊断添加进DataGridView
        /// </summary>
        /// <param name="p_dgICD">显示ICD诊断的DataGridView，第一列为显示名称，第二列显示ICD10码</param>
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

        #region 将ICD10诊断添加进文本框
        /// <summary>
        /// 将ICD10诊断添加进文本框
        /// </summary>
        /// <param name="p_txtName">显示诊断名称的文本框</param>
        /// <param name="p_txtCode">显示ICD10编码的文本框</param>
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

        #region 将麻醉方式添加进DataGridView
        /// <summary>
        /// 将麻醉方式添加进DataGridView
        /// </summary>
        /// <param name="p_dgAna">显示麻醉方式DataGridView，第7列为显示名称，第10列显示麻醉编码</param>
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

        #region 将手术添加进DataGridView
        /// <summary>
        /// 将手术添加进DataGridView
        /// </summary>
        /// <param name="p_dgICD">显示手术DataGridView，第3列为显示名称，第1列显示手术编码</param>
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
        /// 修改病案同步记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strContentXML">内容</param>
        /// <param name="intType">类型0未同步1已同步</param>
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
        /// 新增病案同步记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strContentXML">内容</param>
        /// <param name="intType">类型0未同步1已同步</param>
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
        /// 新增病案同步记录
        /// </summary>
        /// <param name="p_objVOArr">病案同步记录内容</param>
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
        /// 修改病案同步记录
        /// </summary>
        /// <param name="p_objVOArr">病案同步记录内容</param>
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
        /// 获取病案同步记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_objVO">病案同步记录</param>
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

        #region 同步病案系统
        /// <summary>
        /// 同步内容到HIS_BA1
        /// </summary>
        /// <param name="p_dtbBA1">HIS_BA1内容</param>
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
        /// 同步内容到HIS_BA2
        /// </summary>
        /// <param name="p_dtbBA2">HIS_BA2内容</param>
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
        /// 同步内容到HIS_BA3
        /// </summary>
        /// <param name="p_dtbBA3">HIS_BA3内容</param>
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
        /// 同步内容到HIS_BA4
        /// </summary>
        /// <param name="p_dtbBA4">HIS_BA4内容</param>
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
        /// 同步内容到HIS_BA5
        /// </summary>
        /// <param name="p_dtbBA5">HIS_BA5内容</param>
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
        /// 同步内容到HIS_BA6
        /// </summary>
        /// <param name="p_dtbBA6">HIS_BA6内容</param>
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
        /// 同步内容到HIS_BA7
        /// </summary>
        /// <param name="p_dtbBA7">HIS_BA7内容</param>
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
