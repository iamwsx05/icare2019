using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using System.Security;

namespace com.digitalwave.Emr.Signature_srv
{
    /// <summary>
    /// Signature_srv ��ժҪ˵����
    /// ǩ������ֵ�м��
    /// create by tfzhang at 2005-12-26 16:50
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsSignature_srv : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ��ǩ������ֵ
        /// <summary>
        /// ��ȡԱ��ǩ��
        /// ���ò�ָ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intFlag">Ա�����ͣ�0Ϊ����Ա����1Ϊҽ����2Ϊ��ʿ</param>
        /// <param name="p_objdt">���ر�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeSignWithoutDept(int p_intFlag, out DataTable p_objdt)
        {
            long lngRes = 0;
            p_objdt = null;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {
                string strSql = "";

                switch (p_intFlag)
                {
                    case 0:
                        strSql = @" select t.empno_chr,
												t.lastname_vchr,
												t.technicalrank_chr,
												t.pycode_chr,
												t.empid_chr,
												t.psw_chr,
												t.digitalsign_dta,
												t.technicallevel_chr
										from t_bse_employee t
										where t.status_int = 1
										order by t.technicallevel_chr desc";

                        break;
                    case 1:
                        strSql = @"select t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
								from t_bse_employee t
								where t.status_int = 1
								and t.technicallevel_chr like '5%'
								order by t.technicallevel_chr desc";
                        break;
                    case 2:
                        strSql = @"select t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
								from t_bse_employee t
								where t.status_int = 1
								and t.technicallevel_chr like '4%'
								order by t.technicallevel_chr desc";
                        break;
                }
                lngRes = objHRPServer.DoGetDataTable(strSql, ref p_objdt);
            }
            catch (Exception exp)
            {
                lngRes = 0;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(exp);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡȫԺ��ҽ���ͻ�ʿ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objdt">���ر�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeSignDocAndNur(out DataTable p_objdt)
        {
            long lngRes = 0;
            p_objdt = null;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {
                string strSql = "";
                strSql = @"select empno_chr,
       lastname_vchr,
       technicalrank_chr,
       pycode_chr,
       empid_chr,
       psw_chr,
       digitalsign_dta,
       technicallevel_chr
  from (select t.empno_chr,
               t.lastname_vchr,
               t.technicalrank_chr,
               t.pycode_chr,
               t.empid_chr,
               t.psw_chr,
               t.digitalsign_dta,
               t.technicallevel_chr
          from t_bse_employee t
         where t.status_int = 1
           and t.technicallevel_chr like '4%'
        union
        select t.empno_chr,
               t.lastname_vchr,
               t.technicalrank_chr,
               t.pycode_chr,
               t.empid_chr,
               t.psw_chr,
               t.digitalsign_dta,
               t.technicallevel_chr
          from t_bse_employee t
         where t.status_int = 1
           and t.technicallevel_chr like '5%')
 order by technicallevel_chr desc";
              
                lngRes = objHRPServer.DoGetDataTable(strSql, ref p_objdt);
            }
            catch (Exception exp)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(exp);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
            return lngRes;
        }


        /// <summary>
        /// ��ȡԱ��ǩ��
        /// ����ָ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intFlag">Ա�����ͣ�0Ϊ����Ա����1Ϊҽ����2Ϊ��ʿ 10Ϊ�����Σ�20Ϊ��ʿ��</param>
        /// <param name="p_objdt">���ر�</param>
        /// <param name="p_strEmployeeID">����ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeSignWithDept(int p_intFlag, string p_strDeptID, out DataTable p_objdt)
        {
            long lngRes = 0;
            p_objdt = null;
            if (string.IsNullOrEmpty(p_strDeptID))
                return -1;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {
                string strSql = "";

                switch (p_intFlag)
                {
                    case 0:
                        strSql = @"select t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
									and t.empid_chr = d.empid_chr
									and d.deptid_chr = ?
									order by t.technicallevel_chr desc";

                        break;
                    case 1:
                        strSql = @"select t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
									and t.technicallevel_chr like '5%'
									and t.empid_chr = d.empid_chr
									and d.deptid_chr = ?
									order by t.technicallevel_chr desc";
                        break;
                    case 2:
                        strSql = @"select t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
									and t.technicallevel_chr like '4%'
									and t.empid_chr = d.empid_chr
									and d.deptid_chr = ?
									order by t.technicallevel_chr desc";
                        break;
                    case 10:
                        strSql = @" select distinct t.empno_chr,
                                    t.lastname_vchr,
                                    t.technicalrank_chr,
                                    t.pycode_chr,
                                    t.empid_chr,
                                    t.psw_chr,
                                    t.digitalsign_dta,
                                    t.technicallevel_chr
                      from t_bse_employee t
                     where t.status_int = 1
                       and t.empid_chr in
                           (select d.empid_chr
                              from t_sys_emprolemap d
                             inner join (select roleid_chr, name_vchr, desc_vchr, deptid_chr
                                          from t_sys_role r
                                         where r.deptid_chr =?
                                           and r.name_vchr = '������') kk on d.roleid_chr = kk.roleid_chr)";
                        break;

                    case 20:
                        strSql = @" select distinct t.empno_chr,
                                    t.lastname_vchr,
                                    t.technicalrank_chr,
                                    t.pycode_chr,
                                    t.empid_chr,
                                    t.psw_chr,
                                    t.digitalsign_dta,
                                    t.technicallevel_chr
                      from t_bse_employee t
                     where t.status_int = 1
                       and t.empid_chr in
                           (select d.empid_chr
                              from t_sys_emprolemap d
                             inner join (select roleid_chr, name_vchr, desc_vchr, deptid_chr
                                          from t_sys_role r
                                         where r.deptid_chr = ?
                                           and r.name_vchr = '��ʿ��') kk on d.roleid_chr = kk.roleid_chr)";
                        break;
                                                                            
                }

                IDataParameter[] objDPArr = null;
                objHRPServer.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strDeptID.Trim();

                lngRes = objHRPServer.lngGetDataTableWithParameters(strSql, ref p_objdt, objDPArr);
            }
            catch (Exception exp)
            {
                lngRes = 0;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(exp);
            }
            finally
            {
                //objHRPServer.Dispose();
            }

            return lngRes;
        }

        /// <summary>
        /// ��ȡԱ��ǩ��
        /// ����Ա����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intFlag">Ա�����ͣ�0Ϊ����Ա����1Ϊҽ����2Ϊ��ʿ</param>
        /// <param name="p_objdt">���ر�</param>
        /// <param name="p_strEmployeeID">Ա��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeSignWithEmployee(int p_intFlag, string p_strEmployeeID, out DataTable p_objdt)
        {
            long lngRes = 0;
            p_objdt = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = "";

                switch (p_intFlag)
                {
                    case 0:
                        strSql = @"     select distinct t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
									and t.empid_chr = d.empid_chr
									and exists  (select   deptid_chr from t_bse_deptemp de  where   empid_chr=? and de.deptid_chr = d.deptid_chr)
									order by t.technicallevel_chr desc";

                        break;
                    case 1:
                        strSql = @" select distinct t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
                                    and t.technicallevel_chr like '5%'
									and t.empid_chr = d.empid_chr
									and exists  (select   deptid_chr from t_bse_deptemp de  where   empid_chr=? and de.deptid_chr = d.deptid_chr)
									order by t.technicallevel_chr desc";
                        break;
                    case 2:
                        strSql = @"     select  distinct t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
                                    and t.technicallevel_chr like '4%'
									and t.empid_chr = d.empid_chr
									and exists  (select   deptid_chr from t_bse_deptemp de  where   empid_chr=? and de.deptid_chr = d.deptid_chr)
									order by t.technicallevel_chr desc";
                    
                        break;
                }
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strEmployeeID.Trim();

                DataTable dtResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
                if (lngRes > 0) p_objdt = dtResult;
            }
            catch (Exception exp)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(exp);
            }
            finally
            {
                //objHRPServer.Dispose();
            }

            return lngRes;
        }

        #endregion

        /// <summary>
        /// ��ȡ����ҽʦ��ʿ��ǩ��
        /// ����Ա����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intFlag">Ա�����ͣ�0Ϊ����Ա����10Ϊ����ҽʦ��20Ϊ��ʿ��</param>
        /// <param name="p_objdt">���ر�</param>
        /// <param name="p_strEmployeeID">Ա��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDirectorSignWithEmployee(int p_intFlag, string p_strEmployeeID, out DataTable p_objdt)
        {
            long lngRes = 0;
            p_objdt = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = "";

                switch (p_intFlag)
                {

                    case 0:
                        strSql = @" select distinct t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
                                    and (t.technicallevel_chr like '5%'or t.technicallevel_chr like '4%')
									and t.empid_chr = d.empid_chr
									and exists  (select   deptid_chr from t_bse_deptemp de  where   empid_chr=? and de.deptid_chr = d.deptid_chr)
									order by t.technicallevel_chr desc";
                        break;
                    case 10:
                        strSql = @" select distinct t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
                                    and t.technicallevel_chr ='550'
									and t.empid_chr = d.empid_chr
									and exists  (select   deptid_chr from t_bse_deptemp de  where   empid_chr=? and de.deptid_chr = d.deptid_chr)
									order by t.technicallevel_chr desc";
                        break;
                    case 20:
                        strSql = @"     select  distinct t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
                                    and t.technicallevel_chr ='450'
									and t.empid_chr = d.empid_chr
									and exists  (select   deptid_chr from t_bse_deptemp de  where   empid_chr=? and de.deptid_chr = d.deptid_chr)
									order by t.technicallevel_chr desc";
                        break;
                }
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strEmployeeID.Trim();

                DataTable dtResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
                if (lngRes > 0) p_objdt = dtResult;
            }
            catch (Exception exp)
            {
                lngRes = 0;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(exp);
            }
            finally
            {
                //objHRPServer.Dispose();
            }

            return lngRes;
        }

        #region ��ȡ����ֵ

        /// <summary>
        /// ����settingID��ȡ����ֵ
        /// </summary>
        /// <param name="p_strSetID">����ID</param>
        /// <param name="strReturn">���أ���Ϊnull���ʾδȡ������ֵ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetConfigBySettingID(string p_strSetID, out string strReturn)
        {
            strReturn = null;
            if (string.IsNullOrEmpty(p_strSetID)) return -1;
            long lngRes = -1;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {
                string strSQL = @"select setstatus_int from t_sys_setting where setid_chr = ?";

                DataTable dtRecord = new DataTable();

                IDataParameter[] objDPArr = null;
                objHRPServer.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strSetID.Trim();

                lngRes = objHRPServer.lngGetDataTableWithParameters(strSQL, ref dtRecord,objDPArr);

                if (lngRes > 0 && dtRecord.Rows.Count == 1)
                {
                    strReturn = dtRecord.Rows[0][0].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                strReturn = null;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
            //����
            return lngRes;
        }
        #endregion

        #region ����key�̷���ָ��Ա��������Ϣ
        /// <summary>
        /// ����key�̷���ָ��Ա����Ϣ
        /// ǰ��������Ա������key����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strEmpKey">key</param>
        /// <param name="p_dtbValue">�������ݼ�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmpByKey( string strEmpKey, out DataTable p_dtbValue)
        {
            //��ʼ��
            long lngRes = 0;
            p_dtbValue = null;
            if (string.IsNullOrEmpty(strEmpKey)) return -1;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {

                //Ȩ��У��
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "Signature_srv", "m_lngGetEmpByKey");
                ////objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @" select t.empno_chr,
												t.lastname_vchr,
												t.technicalrank_chr,
												t.pycode_chr,
												t.empid_chr,
												t.psw_chr,
												t.digitalsign_dta,
												t.technicallevel_chr
								from t_bse_employee t
								where t.status_int = 1
								and t.digitalsign_dta = ?";
                DataTable dtResult = new DataTable();

                IDataParameter[] objDPArr = null;
                objHRPServer.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strEmpKey.Trim();

                lngRes = objHRPServer.lngGetDataTableWithParameters(strSQL, ref dtResult,objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
            //����
            return lngRes;
        }
        #endregion

        #region ������ˮ�Ż�ȡǩ������
        /// <summary>
        /// ������ˮ�Ż�ȡǩ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSequence">��ˮ��</param>
        /// <param name="p_dtbValue">�����б�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSignBySequence( long p_lngSequence, out DataTable p_dtbValue)
        {
            //��ʼ��
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������                              
                if (p_lngSequence == 0)
                    return -1; 

                string strSql = @"select t.empid_vchr,
									d.lastname_vchr,
									d.technicalrank_chr,
									d.empno_chr,
									d.psw_chr,
									d.digitalsign_dta,
									d.pycode_chr,
									d.status_int,
									d.technicallevel_chr,
									t.cagetory_vchr,
									t.formname_vchr,
									t.registerid_vchr
								from t_emr_signcollection t
								left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
								where t.sign_int =? order by t.seq_int asc";


                //DataTable
                DataTable dtbValue = new DataTable();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngSequence;

                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue,objDPArr);
                int intSignCount = dtbValue.Rows.Count;
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0)
                    p_dtbValue = dtbValue;

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            //����
            return lngRes;
        }
        #endregion
    }
}
