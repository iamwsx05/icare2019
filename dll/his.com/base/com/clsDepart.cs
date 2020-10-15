using System;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility;
using System.Collections;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsDepart ��ժҪ˵����
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsDepart : com.digitalwave.iCare.middletier.clsMiddleTierBase, IDisposable
    {
        public clsDepart()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ȡ�����еĿ��һ�����Ϣ
        /// <summary>
        /// ȡ�����еĿ��һ�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDepID"></param>
        /// <param name="objDep"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptList(string strDepID, out clsDepartmentVO[] objDep)
        {
            objDep = new clsDepartmentVO[0];
            long lngRes = 0;
            string strSQL = "SELECT * FROM T_BSE_DEPTDESC Where STATUS_INT=1  ";
            if (strDepID != null)
                strSQL = strSQL + " And deptid_chr='" + strDepID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    objDep = new clsDepartmentVO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < objDep.Length; i1++)
                    {
                        objDep[i1] = new clsDepartmentVO();
                        objDep[i1].strDeptID = dtbResult.Rows[i1]["DEPTID_CHR"].ToString().Trim();
                        objDep[i1].strDeptName = dtbResult.Rows[i1]["DEPTNAME_VCHR"].ToString().Trim();
                        objDep[i1].strCreateDate = dtbResult.Rows[i1]["CREATEDATE_DAT"].ToString().Trim();
                        if (dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim() != "")
                            objDep[i1].intStatus = int.Parse(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ȡ��Ա���Ļ�����Ϣ
        /// <summary>
        /// ��������ȡ��Ա���Ļ�����Ϣ
        /// strEmpID��strDepID��Ϊ��ʱ����ȫ����
        /// strDepID��Ϊ�ղ���strEmpIDΪ�շ���Ҫ����ҵ�Ա��
        /// �������strEmpID���Ҽ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strEmpID"></param>
        /// <param name="strDepID"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeList(string strEmpID, string strDepID, ref DataTable dtbResult)
        {
            long lngRes = 0;
            string strSQL = "";
            if (strDepID == null && strEmpID == null)
                strSQL = "SELECT * FROM T_BSE_Employee ";
            if (strEmpID != null) //ȡ��һԱ��
                strSQL = "SELECT * FROM T_BSE_Employee Where EMPID_CHR='" + strEmpID + "'";
            else
            {
                if (strDepID != null) //ȡ��һԱ��
                    strSQL = "SELECT a.* FROM T_BSE_Employee a,t_bse_deptemp b  " +
                        " Where a.empid_chr=b.empid_chr And b.deptid_chr='" + strDepID + "' ";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����һ�����ҵ���Ϣ
        /// <summary>
        /// ���ݼ��𷵻ؿ��ң�intLevel=1ʱΪһ�����ң�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="intLevel"></param>
        /// <param name="objDep"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lng_getParentDep(int intLevel, out clsDepartmentVO[] objDep)
        {
            objDep = new clsDepartmentVO[0];
            long lngRes = 0;
            string strSQL = "Select Distinct a.parentdeptid_chr,b.deptname_vchr From T_BSE_DEPTANDDEPT a,T_BSE_DEPTDESC b " +
                " Where a.parentdeptid_chr=b.deptid_chr(+) And b.status_int=1 And " +
                " (a.levels_int=" + intLevel + " or a.deptid_chr=a.parentdeptid_chr)  ";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    objDep = new clsDepartmentVO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < objDep.Length; i1++)
                    {
                        objDep[i1] = new clsDepartmentVO();
                        objDep[i1].strDeptID = dtbResult.Rows[i1]["parentdeptid_chr"].ToString().Trim();
                        objDep[i1].strDeptName = dtbResult.Rows[i1]["DEPTNAME_VCHR"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ���ݸ�����ȡ���ӿ��ҵ���Ϣ
        /// <summary>
        /// ���ݸ����ҵ�IDȡ�����ӿ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDepID"></param>
        /// <param name="objDep"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lng_getChildrenDep(string strDepID, out clsDepartmentVO[] objDep)
        {
            objDep = new clsDepartmentVO[0];
            long lngRes = 0;
            string strSQL = "Select b.deptid_chr,a.Deptname_VCHR,b.levels_int from T_BSE_DEPTDESC a,T_BSE_DEPTANDDEPT b " +
                " Where b.parentdeptid_chr='" + strDepID + "' and b.deptid_chr=a.deptid_chr(+) ";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    objDep = new clsDepartmentVO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < objDep.Length; i1++)
                    {
                        objDep[i1] = new clsDepartmentVO();
                        objDep[i1].strDeptID = dtbResult.Rows[i1]["deptid_chr"].ToString().Trim();
                        objDep[i1].strDeptName = dtbResult.Rows[i1]["Deptname_VCHR"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ���ݿ���ID���ظ��������ҵ�ID
        /// <summary>
        /// ���ݿ���ID���ظ��������ҵ�ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDepID"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lng_getLevParentDep(string strDepID, out System.Collections.Generic.List<string> strID)
        {
            strID = new System.Collections.Generic.List<string>(); 
            long lngRes = 0;
            string strSQL = "";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                string strParID = strDepID;
                do
                {
                    strSQL = "Select parentdeptid_chr from T_BSE_DEPTANDDEPT  " +
                           " Where deptid_chr='" + strDepID + "' ";
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                    if (lngRes > 0 && dtbResult.Rows.Count > 0)
                    {
                        strParID = dtbResult.Rows[0][0].ToString().Trim();
                        strDepID = strParID;
                        strID.Add(strParID);
                    }
                    else
                        return lngRes;
                }
                while (strParID == strDepID);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ���ݿ������Ʋ�ѯ����ID
        /// <summary>
        /// ���ݿ������Ʋ�ѯ����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strName"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDepIDByName(string strName, out string strID)
        {
            strID = "";
            long lngRes = 0;
            string strSQL = "select deptid_chr from T_BSE_DEPTDESC  where Deptname_vchr like '" + strName + "%' and Rownum=1 ";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    strID = dtbResult.Rows[0]["deptid_chr"].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ����Ա����ѯ����ID
        /// <summary>
        /// ����Ա����ѯ����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <param name="strNo"></param>
        /// <param name="strName"></param>
        /// <param name="strDepID"></param>
        /// <param name="strDocID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDepByDoc(string strID, string strNo, string strName, out string strDepID, out string strDocID)
        {
            strDepID = "";
            strDocID = "";
            long lngRes = 0;
            string strSQL = @"SELECT a.empid_chr, a.deptid_chr, a.begin_dat, a.end_dat,
                            a.operatorid_chr, a.empkind_chr
                            FROM t_bse_deptemp a,t_bse_employee b
                            where a.empid_chr=b.empid_chr  ";
            if (strID != null)
                strSQL = strSQL + " And a.empid_chr='" + strID + "' ";
            else if (strNo != null)
                strSQL = strSQL + " And b.EMPNO_CHR = '" + strNo + "' ";
            else
                strSQL = strSQL + " And b.LASTNAME_VCHR like '" + strName + "%' ";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    strDepID = dtbResult.Rows[0]["deptid_chr"].ToString().Trim();
                    strDocID = dtbResult.Rows[0]["empid_chr"].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
