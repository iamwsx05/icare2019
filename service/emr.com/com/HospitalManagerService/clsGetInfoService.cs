using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using System.Security;
using weCare.Core.Entity;

namespace com.digitalwave.emr.HospitalManagerService
{
    /// <summary>
    /// clsGetInfoService ��ժҪ˵����
    /// ���м��������ҡ�������������������صĲ���
    /// ����·�����д�ϱ�Ҫ��xmlע�ͣ�ͬʱ�鵽��Ӧ��region������¹������½� �м� ����
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsHospitalManagerService : com.digitalwave.iCare.middletier.clsMiddleTierBase, IHospitalManagerService
    {


        #region ����
        /// <summary>
        /// ����Ա��ID��ȡ���������б�
        /// �˷���ֻ��ȡ�����ٴ������б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID">Ա��ID</param>
        /// <param name="p_dtbValue">���������б�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptInfo(string p_strEmployeeID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //�������Ա��룺0000002  �ٴ�����category_int=0
                string strSQL = @"select t.empid_chr, d.deptid_chr, d.deptname_vchr, d.address_vchr,d.shortno_chr,d.extendid_vchr,t.default_inpatient_dept_int,d.pycode_chr
								from t_bse_deptemp t, t_bse_deptdesc d
								where t.deptid_chr = d.deptid_chr
								and d.attributeid =? 
								and d.category_int=?
								and t.empid_chr =?
                                order by t.default_inpatient_dept_int desc,t.deptid_chr";

                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //��ֵ
                objDPArr[0].Value = "0000002";
                objDPArr[1].Value = 0;
                objDPArr[2].Value = p_strEmployeeID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡָ��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intDeptType">��������0.����1.סԺ2.����</param>
        /// <param name="p_dtbValue">�����б�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSpecifyTypeDeptInfo(int p_intDeptType, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //�������Ա��룺0000002  �ٴ�����category_int=0
                string strSQL = @"select d.deptid_chr,
       d.modify_dat,
       d.deptname_vchr,
       d.category_int,
       d.inpatientoroutpatient_int,
       d.operatorid_chr,
       d.address_vchr,
       d.attributeid,
       d.pycode_chr,
       d.parentid,
       d.createdate_dat,
       d.deactivate_dat,
       d.status_int,
       d.wbcode_chr,
       d.code_vchr,
       d.extendid_vchr,
       d.shortno_chr,
       d.stdbed_count_int,
       d.putmed_int
  from t_bse_deptdesc d
 where d.attributeid = ?
   and d.category_int = ?
   and d.inpatientoroutpatient_int = ?
 order by d.deptid_chr";

                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //��ֵ
                objDPArr[0].Value = "0000002";
                objDPArr[1].Value = 0;
                objDPArr[2].Value = p_intDeptType;
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ����Ա��ID��ȡ��������/�����б�
        /// �˷���ֻ��ȡ�����ٴ������б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID">Ա��ID</param>
        /// <param name="p_dtbValue">���������б�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptAreaInfo(string p_strEmployeeID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            if (string.IsNullOrEmpty(p_strEmployeeID)) return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //����/�������Ա��룺0000002/0000003  �ٴ�����category_int=0
                string strSQL = @"select t.empid_chr,
       d.deptid_chr,
       d.deptname_vchr,
       d.address_vchr,
       d.shortno_chr,
       d.attributeid,
       t.default_inpatient_dept_int,
       d.parentid
  from t_bse_deptemp t, t_bse_deptdesc d
 where t.deptid_chr = d.deptid_chr
   and d.attributeid <> ?
   and d.category_int = ?
   and t.empid_chr = ?
 order by t.default_inpatient_dept_int desc, t.deptid_chr";

                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //��ֵ
                objDPArr[0].Value = "0000001";
                objDPArr[1].Value = 0;
                objDPArr[2].Value = p_strEmployeeID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// ����Ա��ID��ȡ������������б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID">Ա��ID</param>
        /// <param name="p_objRecordArr">���������б�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutDeptInfo(string p_strEmployeeID, out clsEmrDept_VO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            long m_lngRes = 0;
            if (string.IsNullOrEmpty(p_strEmployeeID))
                return m_lngRes; 
            try
            {
                string m_strSQL = @"select t.empid_chr,
       d.deptid_chr,
       d.deptname_vchr,
       d.address_vchr,
       d.shortno_chr,
       d.attributeid,
       t.default_dept_int,
       d.parentid
  from t_bse_deptemp t, t_bse_deptdesc d
 where t.deptid_chr = d.deptid_chr
   and d.attributeid = '0000001'
   and d.inpatientoroutpatient_int = 0
   and t.empid_chr = ?
 order by t.default_dept_int desc, t.deptid_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID;

                DataTable dtResult = null;
                m_lngRes = objHRPServ.lngGetDataTableWithParameters(m_strSQL, ref dtResult, objDPArr);

                if (m_lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_objRecordArr = new clsEmrDept_VO[dtResult.Rows.Count];
                    clsEmrDept_VO m_objTemp = null;
                    DataRow p_dr = null;

                    for (int i1 = 0; i1 < dtResult.Rows.Count; i1++)
                    {
                        p_dr = dtResult.Rows[i1];
                        m_objTemp = new clsEmrDept_VO();
                        m_objTemp.m_strDEPTID_CHR = p_dr["deptid_chr"].ToString().Trim();
                        m_objTemp.m_strDEPTNAME_VCHR = p_dr["deptname_vchr"].ToString();
                        m_objTemp.m_strADDRESS_VCHR = p_dr["address_vchr"].ToString();
                        m_objTemp.m_strSHORTNO_CHR = p_dr["shortno_chr"].ToString();
                        if (m_objTemp.m_strSHORTNO_CHR != null)
                            m_objTemp.m_strSHORTNO_CHR = m_objTemp.m_strSHORTNO_CHR.Trim();
                        m_objTemp.m_strATTRIBUTEID = p_dr["attributeid"].ToString();
                        m_objTemp.m_intDEFAULT_INPATIENT_DEPT_INT = Convert.ToInt32(p_dr["default_dept_int"]);

                        p_objRecordArr[i1] = m_objTemp;
                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogDetailError(objEx, true);
                objLogger = null;
            }
            finally
            {
                p_strEmployeeID = null;
            }
            return m_lngRes;
        }


        /// <summary>
        /// ��ȡָ��������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">����ID</param>
        /// <param name="p_dtbValue">ָ��������Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSpecialDeptInfo(string p_strDeptID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            if (string.IsNullOrEmpty(p_strDeptID)) return - 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select t.deptid_chr,
       t.modify_dat,
       t.deptname_vchr,
       t.category_int,
       t.inpatientoroutpatient_int,
       t.operatorid_chr,
       t.address_vchr,
       t.attributeid,
       t.pycode_chr,
       t.parentid,
       t.createdate_dat,
       t.deactivate_dat,
       t.status_int,
       t.wbcode_chr,
       t.code_vchr,
       t.extendid_vchr,
       t.shortno_chr,
       t.stdbed_count_int,
       t.putmed_int
  from t_bse_deptdesc t
 where t.deptid_chr = ?";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strDeptID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        /// ��ȡ�������͵Ŀ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbValue">���ұ�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllAttributeDeptInfo(out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select t.deptid_chr,
       t.modify_dat,
       t.deptname_vchr,
       t.category_int,
       t.inpatientoroutpatient_int,
       t.operatorid_chr,
       t.address_vchr,
       t.attributeid,
       t.pycode_chr,
       t.parentid,
       t.createdate_dat,
       t.deactivate_dat,
       t.status_int,
       t.wbcode_chr,
       t.code_vchr,
       t.extendid_vchr,
       t.shortno_chr,
       t.stdbed_count_int,
       t.putmed_int
  from t_bse_deptdesc t
 where t.status_int = ?
 order by t.deptname_vchr";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //��ֵ
                objDPArr[0].Value = 1;
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡ���п��ң�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllDeptInfo(out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select t.deptid_chr,
       t.modify_dat,
       t.deptname_vchr,
       t.category_int,
       t.inpatientoroutpatient_int,
       t.operatorid_chr,
       t.address_vchr,
       t.attributeid,
       t.pycode_chr,
       t.parentid,
       t.createdate_dat,
       t.deactivate_dat,
       t.status_int,
       t.wbcode_chr,
       t.code_vchr,
       t.extendid_vchr,
       t.shortno_chr,
       t.stdbed_count_int,
       t.putmed_int
  from t_bse_deptdesc t
 where t.status_int = ?
   and t.attributeid <> ?
 order by t.deptname_vchr";

                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //��ֵ
                objDPArr[0].Value = 1;
                objDPArr[1].Value = "0000001";
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
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
                //objHrpServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        /// ��ȡ����סԺ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult">��ѯ���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllInDept(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @" select dd.deptid_chr,
       dd.shortno_chr,
       dd.code_vchr,
       dd.deptname_vchr,
       dd.extendid_vchr,
       dd.pycode_chr,
       dd.attributeid
  from t_bse_deptdesc dd
 where dd.attributeid = '0000002'
   and inpatientoroutpatient_int = 1
   and dd.status_int = 1
 order by inpatientoroutpatient_int desc, deptname_vchr";

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        }

        /// <summary>
        /// ��ȡ���п��ң�����,����attributeid
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAttributeid">0000001 = ���ţ�000002 �� ���ң�0000003 �� ����</param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllDeptInfoByAttributeid(string p_strAttributeid,out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select t.deptid_chr,
       t.modify_dat,
       t.deptname_vchr,
       t.category_int,
       t.inpatientoroutpatient_int,
       t.operatorid_chr,
       t.address_vchr,
       t.attributeid,
       t.pycode_chr,
       t.parentid,
       t.createdate_dat,
       t.deactivate_dat,
       t.status_int,
       t.wbcode_chr,
       t.code_vchr,
       t.extendid_vchr,
       t.shortno_chr,
       t.stdbed_count_int,
       t.putmed_int
  from t_bse_deptdesc t
 where t.status_int = ?
   and t.attributeid = ?
 order by t.deptname_vchr";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //��ֵ
                objDPArr[0].Value = 1;
                objDPArr[1].Value = p_strAttributeid;
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;

        }
        /// <summary>
        /// ��ȡ���һ��߲����������סԺ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAttributeid">0000001 = ���ţ�000002 �� ���ң�0000003 �� ����</param>
        /// <param name="p_intInOrOut">0�����1��סԺ��2������</param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllDeptInfo(string p_strAttributeid, int p_intInOrOut, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select t.deptid_chr,
       t.modify_dat,
       t.deptname_vchr,
       t.category_int,
       t.inpatientoroutpatient_int,
       t.operatorid_chr,
       t.address_vchr,
       t.attributeid,
       t.pycode_chr,
       t.parentid,
       t.createdate_dat,
       t.deactivate_dat,
       t.status_int,
       t.wbcode_chr,
       t.code_vchr,
       t.extendid_vchr,
       t.shortno_chr,
       t.stdbed_count_int,
       t.putmed_int
  from t_bse_deptdesc t
 where t.status_int = 1
   and inpatientoroutpatient_int = ?
   and t.attributeid = ?
 order by t.deptname_vchr";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_intInOrOut;
                objDPArr[1].Value = p_strAttributeid;
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;

        }
        /// <summary>
        /// ���ݿ���ID��ȡͬ����չID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptId"></param>
        /// <param name="p_strExtendId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptExtendId(string p_strDeptId, out string p_strExtendId)
        {
            long lngRes = 0;
            p_strExtendId = "";
            if (string.IsNullOrEmpty(p_strDeptId)) return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select extendid_vchr from t_bse_deptdesc where status_int=1 and deptid_chr=?";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strDeptId;
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                    p_strExtendId = dtResult.Rows[0][0].ToString();

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        /// ��ȡԱ�����������п���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID">Ա��ID</param>
        /// <param name="p_blnJustInPatient">ֻ�ǻ�ȡסԺ���ң�0���񣬰����������ȿ��ң�1���ǣ�</param>
        /// <param name="p_objAreas"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptByEmp(string p_strEmpID, bool p_blnJustInPatient, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = new DataTable();
            if (string.IsNullOrEmpty(p_strEmpID)) return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select de.deptid_chr,
       de.deptname_vchr,
       de.category_int,
       de.inpatientoroutpatient_int,
       de.pycode_chr,
       de.parentid,
       de.wbcode_chr,
       de.code_vchr,
       de.extendid_vchr,
       de.shortno_chr,
       de.attributeid,
       em.default_inpatient_dept_int,
       de.address_vchr
  from t_bse_deptdesc de
 inner join t_bse_deptemp em on de.deptid_chr = em.deptid_chr
 where de.status_int = 1 and em.end_dat is null and em.empid_chr = ?
   and de.attributeid = '0000002' {0}
 order by em.default_inpatient_dept_int Desc,de.deptid_chr";
                if (p_blnJustInPatient)
                    strSQL = string.Format(strSQL, "and de.inpatientoroutpatient_int = 1");
                else
                    strSQL = string.Format(strSQL, string.Empty);
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strEmpID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                int intRowCount = dtResult.Rows.Count;
                if (lngRes > 0)
                {
                    p_dtbValue = dtResult;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;

        }
        #endregion

        #region ����
        /// <summary>
        /// ��ȡ����סԺ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult">��ѯ���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllInArea(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @" select dd.deptid_chr,
       dd.shortno_chr,
       dd.deptname_vchr,
       dd.extendid_vchr,
       dd.pycode_chr,
       dd.code_vchr,
       dd.attributeid,
       '' default_inpatient_dept_int
  from t_bse_deptdesc dd
 where dd.attributeid = '0000003'
   and inpatientoroutpatient_int = 1
   and dd.status_int = 1
 order by inpatientoroutpatient_int desc, deptname_vchr";

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        }

        /// <summary>
        /// ���ݡ�����ID����ȡ���������б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">������ID��</param>
        /// <param name="p_dtbValue">���������б�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaInfo(string p_strDeptID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetAreaInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //�������Ա��룺0000003
                string strSQL = @"select t.deptid_chr, t.deptname_vchr, t.address_vchr,t.shortno_chr,t.parentid
								from t_bse_deptdesc t
								where t.attributeid = ?
								and t.category_int=?
								and t.parentid =?";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //��ֵ
                objDPArr[0].Value = "0000003";
                objDPArr[1].Value = 0;
                objDPArr[2].Value = p_strDeptID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
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
                //objHrpServ.Dispose();
            }
            return lngRes;

        }
        /// <summary>
        /// ��ȡ��ָ����������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">������ID��</param>
        /// <param name="p_dtbValue">ָ��������Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSpecialAreaInfo(string p_strAreaID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetAreaInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select t.deptid_chr,
       t.modify_dat,
       t.deptname_vchr,
       t.category_int,
       t.inpatientoroutpatient_int,
       t.operatorid_chr,
       t.address_vchr,
       t.attributeid,
       t.pycode_chr,
       t.parentid,
       t.createdate_dat,
       t.deactivate_dat,
       t.status_int,
       t.wbcode_chr,
       t.code_vchr,
       t.extendid_vchr,
       t.shortno_chr,
       t.stdbed_count_int,
       t.putmed_int
  from t_bse_deptdesc t
 where t.deptid_chr = ?";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strAreaID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
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
                //objHrpServ.Dispose();
            }
            return lngRes;

        }
        /// <summary>
        /// ��ȡԱ�����������в���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID">Ա��ID</param>
        /// <param name="p_blnJustInPatient">ֻ�ǻ�ȡסԺ������0���񣬰����������ȿ��ң�1���ǣ�</param>
        /// <param name="p_objAreas"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaByEmp(string p_strEmpID, bool p_blnJustInPatient, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = new DataTable();
            if (string.IsNullOrEmpty(p_strEmpID)) return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetAreaInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select de.deptid_chr,
       de.deptname_vchr,
       de.category_int,
       de.inpatientoroutpatient_int,
       de.pycode_chr,
       de.parentid,
       de.wbcode_chr,
       de.code_vchr,
       de.extendid_vchr,
       de.shortno_chr,
       de.attributeid,
       em.default_inpatient_dept_int,
       de.address_vchr
  from t_bse_deptdesc de
 inner join t_bse_deptemp em on de.deptid_chr = em.deptid_chr
 where de.status_int = 1 and em.end_dat is null and em.empid_chr = ?
   and de.attributeid = '0000003' {0}
 order by em.default_inpatient_dept_int Desc,de.deptid_chr";
                if (p_blnJustInPatient)
                    strSQL = string.Format(strSQL,"and de.inpatientoroutpatient_int = 1");
                else
                    strSQL = string.Format(strSQL, string.Empty);
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strEmpID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                int intRowCount = dtResult.Rows.Count;
                if (lngRes > 0)
                {
                    p_dtbValue = dtResult;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        /// ��ȡԱ�����������в���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID">Ա��ID</param>
        /// <param name="p_blnJustInPatient">ֻ�ǻ�ȡסԺ������0���񣬰����������ȿ��ң�1���ǣ�</param>
        /// <param name="p_objAreas"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaByEmp(out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = new DataTable();
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetAreaInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select distinct de.deptid_chr,
       de.deptname_vchr,
       de.category_int,
       de.inpatientoroutpatient_int,
       de.pycode_chr,
       de.parentid,
       de.wbcode_chr,
       de.code_vchr,
       de.extendid_vchr,
       de.shortno_chr,
       de.attributeid,
       em.default_inpatient_dept_int,
       de.address_vchr
  from t_bse_deptdesc de
 inner join t_bse_deptemp em on de.deptid_chr = em.deptid_chr
 where de.status_int = 1 and em.end_dat is null and em.default_inpatient_dept_int = ?
   and de.attributeid = '0000003' {0}
 order by em.default_inpatient_dept_int Desc,de.deptid_chr";
                
                strSQL = string.Format(strSQL, string.Empty);
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //��ֵ
                objDPArr[0].Value = 1;
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                int intRowCount = dtResult.Rows.Count;
                if (lngRes > 0)
                {
                    p_dtbValue = dtResult;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;

        }
       
        #endregion

        #region ����
        /// <summary>
        /// ���ݲ���ID��ȡ�����б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_dtbValue">���������б�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoomInfo(string p_strAreaID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetRoomInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //�������Ա��룺0000004
                string strSQL = @"select t.deptid_chr, t.deptname_vchr, t.address_vchr
								from t_bse_deptdesc t
								where t.attributeid = ?
								and t.parentid = ?";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //��ֵ
                objDPArr[0].Value = "0000004";
                objDPArr[1].Value = p_strAreaID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
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
                //objHrpServ.Dispose();
            }
            return lngRes;

        }
        #endregion

        #region ����
        /// <summary>
        /// ���ݿ��Ҽ���Ժʱ�䷶Χ���ز����б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">����ID</param>
        /// <param name="p_dtmOutDateStart">��Ժʱ���ѯ��ʼʱ��</param>
        /// <param name="p_dtmOutDateEnd">��Ժʱ���ѯ����ʱ��</param>
        /// <param name="p_dtbValue">��ѯ���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutPatientByAreaID(string p_strDeptID, DateTime p_dtmOutDateStart, DateTime p_dtmOutDateEnd, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            if (string.IsNullOrEmpty(p_strDeptID))
            {
                return -1;
            }

            try
            {
                string strSQL = @"select c.code_chr,
       c.category_int,
       c.extendid_vchr bedextendid_vchr,
       c.sex_int,
       c.status_int bedstatus_int,
       c.rate_mny,
       c.airrate_mny,
       c.chargeitemid_chr,
       c.bed_no,
       b.registerid_chr,
       c.bedid_chr,
       d.type_int,
       d.diagnose_vchr,
       d.inpatientcount_int,
       d.state_int,
       d.pstatus_int,
       d.casedoctor_chr,
       d.deptid_chr,
       d.areaid_chr,
       d.nursing_class,
       d.patientid_chr,
       e.emrinpatientid,
       e.emrinpatientdate,
       e.hisinpatientid_chr,
       e.hisinpatientdate,
       f_getempnamebyid(d.casedoctor_chr) casedoctorname,
       pa.paytypeid_chr,
       ca.patientcardid_chr,
       b.lastname_vchr,
       b.sex_chr,
       b.birth_dat
  from t_opr_bih_leave a
 inner join t_opr_bih_registerdetail b on a.registerid_chr =
                                          b.registerid_chr
                                      and b.status_int = 1
 inner join t_bse_bed c on a.outbedid_chr = c.bedid_chr
                       and c.status_int = 1
 inner join t_opr_bih_register d on a.registerid_chr = d.registerid_chr
                                and d.status_int = 1
 inner join t_bse_hisemr_relation e on e.registerid_chr = a.registerid_chr
  left outer join t_bse_patient pa on pa.patientid_chr = d.patientid_chr
                                  and pa.status_int = 1
  left outer join t_bse_patientcard ca on ca.patientid_chr =
                                          d.patientid_chr
                                      and (ca.status_int = 1 or ca.status_int = 3)
 where a.status_int = 1
   and a.outareaid_chr = ?
   and a.outhospital_dat between ? and ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strDeptID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmOutDateStart;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmOutDateEnd;
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ���ݡ�����ID����ȡ����(�Ѱ����ڴ�������Ϣ)�б�
        /// �÷����������в����������մ��Լ��в��˴���
        /// �÷�����������û�в����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">������ID��</param>
        /// <param name="p_dtbValue">���������б������ڴ�������Ϣ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoByDeptID(string p_strDeptID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            if (string.IsNullOrEmpty(p_strDeptID)) return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetBedInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //����״̬=5Ϊɾ��״̬ ��Ժ�Ǽ�״̬=1��Ч
                //PSTATUS_INT={0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ;4=���} ֻ��ȡ��Ժ���ϴ��Ĳ�����Ϣ
                string strSQL = @"select dd.code_chr,
       dd.category_int,
       dd.bedextendid_vchr,
       dd.sex_int,
       dd.bedstatus_int,
       dd.rate_mny,
       dd.airrate_mny,
       dd.chargeitemid_chr,
       dd.registerid_chr,
       dd.bedid_chr,
       dd.type_int,
       dd.diagnose_vchr,
       dd.inpatientcount_int,
       dd.state_int,
       dd.pstatus_int,
       dd.casedoctor_chr,
       dd.deptid_chr,
       dd.areaid_chr,
       dd.nursing_class,
       dd.patientid_chr,
       dd.emrinpatientid,
       dd.emrinpatientdate,
       dd.hisinpatientid_chr,
       dd.hisinpatientdate,
       dd.casedoctorname,
       dd.bed_no,
       p.lastname_vchr,
       p.sex_chr,
       p.birth_dat,
       f_getempnamebyid(dd.casedoctor_chr) casedoctorname,
       bed_no sequenceno
  from (select t.code_chr,
               t.category_int,
               t.extendid_vchr bedextendid_vchr,
               t.sex_int,
               t.status_int bedstatus_int,
               t.rate_mny,
               t.airrate_mny,
               t.chargeitemid_chr,
               d.registerid_chr,
               d.bedid_chr,
               d.type_int,
               d.diagnose_vchr,
               d.inpatientcount_int,
               d.state_int,
               d.pstatus_int,
               d.casedoctor_chr,
               d.deptid_chr,
               d.areaid_chr,
               d.nursing_class,
               d.patientid_chr,
               d.emrinpatientid,
               d.emrinpatientdate,
               d.hisinpatientid_chr,
               d.hisinpatientdate,
               d.casedoctorname,
               t.bed_no
          from (select bedid_chr,
                       areaid_chr,
                       code_chr,
                       status_int,
                       rate_mny,
                       sex_int,
                       category_int,
                       airrate_mny,
                       chargeitemid_chr,
                       airchargeflg_int,
                       airchargeitemid_chr,
                       extendid_vchr,
                       bihregisterid_chr,
                       creatorid_chr,
                       creat_dat,
                       cancelerid_chr,
                       cancel_dat,
                       bed_no
                  from t_bse_bed
                 where areaid_chr = ?
                   and status_int <> 5) t
          left outer join (select re.registerid_chr,
                                 re.bedid_chr,
                                 re.type_int,
                                 re.diagnose_vchr,
                                 re.inpatientcount_int,
                                 re.state_int,
                                 re.pstatus_int,
                                 re.casedoctor_chr,
                                 re.deptid_chr,
                                 re.areaid_chr,
                                 re.nursing_class,
                                 re.patientid_chr,
                                 rehis.emrinpatientid,
                                 rehis.emrinpatientdate,
                                 rehis.hisinpatientid_chr,
                                 rehis.hisinpatientdate,
                                 f_getempnamebyid(re.casedoctor_chr) casedoctorname
                            from t_opr_bih_register re
                           inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                     re.registerid_chr
                           where re.areaid_chr = ?
                             and re.status_int = 1
                             and re.pstatus_int not in (0, 3)
                             and not exists
                           (select re2.registerid_chr
                                    from t_opr_bih_register re2
                                   where re2.pstatus_int = 2
                                     and re2.registerid_chr =
                                         re.registerid_chr
                                     and exists
                                   (select registerid_chr
                                            from t_opr_bih_register re3
                                           where re3.bedid_chr = re2.bedid_chr
                                             and re3.pstatus_int not in
                                                 (0, 2, 3)
                                             and re3.status_int = 1))) d on t.areaid_chr =
                                                                            d.deptid_chr
                                                                        and t.bedid_chr =
                                                                            d.bedid_chr) dd
  left join t_opr_bih_registerdetail p on dd.registerid_chr =
                                          p.registerid_chr

 order by bed_no";

                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strDeptID.Trim();
                objDPArr[1].Value = p_strDeptID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;

        }
        /// <summary>
        /// ���ݡ�������������ID����ȡ����(�Ѱ����ڴ�������Ϣ)�б�
        /// �÷����������в����������մ��Լ��в��˴���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">������ID������ID����</param>
        /// <param name="p_blnIsRoom">�Ƿ񲡷�</param>
        /// <param name="p_dtbValue">���������б������ڴ�������Ϣ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfo(string p_strAreaID, bool p_blnIsRoom, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            if (string.IsNullOrEmpty(p_strAreaID)) return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetBedInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //����״̬=5Ϊɾ��״̬ ��Ժ�Ǽ�״̬=1��Ч
                //PSTATUS_INT={0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ;4=���} ֻ��ȡ��Ժ���ϴ��Ĳ�����Ϣ
                string strSQL = @"select dd.*,
       p.lastname_vchr,
       p.sex_chr,
       p.birth_dat,
       f_getempnamebyid(dd.casedoctor_chr) casedoctorname,
       bed_no sequenceno
  from (select t.code_chr,
               t.category_int,
               t.extendid_vchr bedextendid_vchr,
               t.sex_int,
               t.status_int bedstatus_int,
               t.rate_mny,
               t.airrate_mny,
               t.chargeitemid_chr,
               d.registerid_chr,
               d.bedid_chr,
               d.type_int,
               d.diagnose_vchr,
               d.inpatientcount_int,
               d.state_int,
               d.pstatus_int,
               d.casedoctor_chr,
               d.deptid_chr,
               d.areaid_chr,
               d.nursing_class,
               d.patientid_chr,
               d.emrinpatientid,
               d.emrinpatientdate,
               d.hisinpatientid_chr,
               d.hisinpatientdate,
               d.casedoctorname,
               t.bed_no
          from (select bedid_chr,
                       areaid_chr,
                       code_chr,
                       status_int,
                       rate_mny,
                       sex_int,
                       category_int,
                       airrate_mny,
                       chargeitemid_chr,
                       airchargeflg_int,
                       airchargeitemid_chr,
                       extendid_vchr,
                       bihregisterid_chr,
                       creatorid_chr,
                       creat_dat,
                       cancelerid_chr,
                       cancel_dat,
                       bed_no
                  from t_bse_bed
                 where areaid_chr = ?
                   and status_int <> 5) t
          left outer join (select re.registerid_chr,
                                 re.bedid_chr,
                                 re.type_int,
                                 re.diagnose_vchr,
                                 re.inpatientcount_int,
                                 re.state_int,
                                 re.pstatus_int,
                                 re.casedoctor_chr,
                                 re.deptid_chr,
                                 re.areaid_chr,
                                 re.nursing_class,
                                 re.patientid_chr,
                                 rehis.emrinpatientid,
                                 rehis.emrinpatientdate,
                                 rehis.hisinpatientid_chr,
                                 rehis.hisinpatientdate,
                                 f_getempnamebyid(re.casedoctor_chr) casedoctorname
                            from t_opr_bih_register re
                           inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                     re.registerid_chr
                           where re.areaid_chr = ?
                             and re.status_int = 1
                             and re.pstatus_int not in (0, 3)
                             and not exists
                           (select re2.registerid_chr
                                    from t_opr_bih_register re2
                                   where re2.pstatus_int = 2
                                     and re2.registerid_chr =
                                         re.registerid_chr
                                     and exists
                                   (select re3.registerid_chr
                                            from t_opr_bih_register re3
                                           where re3.bedid_chr = re2.bedid_chr
                                             and re3.pstatus_int not in
                                                 (0, 2, 3)
                                             and re3.status_int = 1))) d on t.areaid_chr =
                                                                            d.areaid_chr
                                                                        and t.bedid_chr =
                                                                            d.bedid_chr) dd
  left join t_opr_bih_registerdetail p on dd.registerid_chr =
                                          p.registerid_chr

 order by bed_no";

                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strAreaID.Trim();
                objDPArr[1].Value = p_strAreaID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //finally { //objHrpServ.Dispose(); }
            return lngRes;

        }
        /// <summary>
        /// ���ݡ�������������ID����ȡ�ڴ�������Ϣ�б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_blnIsRoom"></param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInBedInfo(string p_strAreaID, bool p_blnIsRoom, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            if (string.IsNullOrEmpty(p_strAreaID)) return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetBedInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //����״̬=5Ϊɾ��״̬ ��Ժ�Ǽ�״̬=1��Ч
                //PSTATUS_INT={0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ;4=���} ֻ��ȡ��Ժ���ϴ��Ĳ�����Ϣ
                string strSQL = @"select dd.*,
       p.lastname_vchr,
       p.sex_chr,
       p.birth_dat,
       bed_no sequenceno
  from (select t.code_chr,
               t.category_int,
               t.extendid_vchr bedextendid_vchr,
               t.sex_int,
               t.status_int bedstatus_int,
               t.rate_mny,
               t.airrate_mny,
               t.chargeitemid_chr,
               d.registerid_chr,
               d.bedid_chr,
               d.type_int,
               d.diagnose_vchr,
               d.inpatientcount_int,
               d.state_int,
               d.pstatus_int,
               d.casedoctor_chr,
               d.casedoctorname,
               d.deptid_chr,
               d.areaid_chr,
               d.nursing_class,
               d.patientid_chr,
               d.emrinpatientid,
               d.emrinpatientdate,
               d.hisinpatientid_chr,
               d.hisinpatientdate,
               t.bed_no
          from (select bedid_chr,
                       status_int,
                       rate_mny,
                       sex_int,
                       category_int,
                       airrate_mny,
                       chargeitemid_chr,
                       bed_no,
                       extendid_vchr,
                       areaid_chr,
                       code_chr
                  from t_bse_bed
                 where areaid_chr = ?
                   and status_int <> 5) t
         inner join (select re.registerid_chr,
                           re.bedid_chr,
                           re.type_int,
                           re.diagnose_vchr,
                           re.inpatientcount_int,
                           re.state_int,
                           re.pstatus_int,
                           re.casedoctor_chr,
                           (select lastname_vchr
                              from t_bse_employee
                             where empid_chr = re.casedoctor_chr) casedoctorname,
                           re.deptid_chr,
                           re.areaid_chr,
                           re.nursing_class,
                           re.patientid_chr,
                           rehis.emrinpatientid,
                           rehis.emrinpatientdate,
                           rehis.hisinpatientid_chr,
                           rehis.hisinpatientdate
                      from t_opr_bih_register re
                     inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                               re.registerid_chr
                     where re.areaid_chr = ?
                       and re.status_int = 1
                       and re.pstatus_int not in (0, 3)
                       and not exists
                     (select re2.registerid_chr
                              from t_opr_bih_register re2
                             where re2.pstatus_int = 2
                               and re2.registerid_chr = re.registerid_chr
                               and re2.areaid_chr = ?
                               and exists
                             (select re3.registerid_chr
                                      from t_opr_bih_register re3
                                     where re3.bedid_chr = re2.bedid_chr
                                       and re3.areaid_chr = ?
                                       and re3.pstatus_int not in (0, 2, 3)
                                       and re3.status_int = 1))) d on t.areaid_chr =
                                                                      d.areaid_chr
                                                                  and t.bedid_chr =
                                                                      d.bedid_chr) dd
 inner join t_opr_bih_registerdetail p on dd.registerid_chr =
                                          p.registerid_chr

 order by bed_no";

                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                //��ֵ
                objDPArr[0].DbType = DbType.AnsiStringFixedLength;
                objDPArr[0].Value = p_strAreaID;
                objDPArr[1].DbType = DbType.AnsiStringFixedLength;
                objDPArr[1].Value = p_strAreaID;
                objDPArr[2].DbType = DbType.AnsiStringFixedLength;
                objDPArr[2].Value = p_strAreaID;
                objDPArr[3].DbType = DbType.AnsiStringFixedLength;
                objDPArr[3].Value = p_strAreaID;
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            //finally { //objHrpServ.Dispose(); }
            return lngRes;

        }
        /// <summary>
        /// ���ݡ�������������ID����ȡ�ڴ�������Ϣ�б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strHospitalNO"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInBedInfo(string p_strAreaID, string p_strHospitalNO, out clsEmrBed_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            if (string.IsNullOrEmpty(p_strAreaID)) return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetBedInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //����״̬=5Ϊɾ��״̬ ��Ժ�Ǽ�״̬=1��Ч
                //PSTATUS_INT={0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ;4=���} ֻ��ȡ��Ժ���ϴ��Ĳ�����Ϣ
                string strSQL = @"select dd.*,
       p.lastname_vchr,
       p.sex_chr,
       p.birth_dat,
       f_getempnamebyid(dd.casedoctor_chr) casedoctorname,
       bed_no sequenceno
  from (select t.code_chr,
               t.category_int,
               t.extendid_vchr bedextendid_vchr,
               t.sex_int,
               t.status_int bedstatus_int,
               t.rate_mny,
               t.airrate_mny,
               t.chargeitemid_chr,
               d.registerid_chr,
               d.bedid_chr,
               d.type_int,
               d.diagnose_vchr,
               d.inpatientcount_int,
               d.state_int,
               d.pstatus_int,
               d.casedoctor_chr,
               d.deptid_chr,
               d.areaid_chr,
               d.nursing_class,
               d.patientid_chr,
               d.emrinpatientid,
               d.emrinpatientdate,
               d.hisinpatientid_chr,
               d.hisinpatientdate,
               t.bed_no
          from (select bedid_chr,
                       areaid_chr,
                       code_chr,
                       status_int,
                       rate_mny,
                       sex_int,
                       category_int,
                       airrate_mny,
                       chargeitemid_chr,
                       airchargeflg_int,
                       airchargeitemid_chr,
                       extendid_vchr,
                       bihregisterid_chr,
                       creatorid_chr,
                       creat_dat,
                       cancelerid_chr,
                       cancel_dat,
                       bed_no
                  from t_bse_bed
                 where areaid_chr = ?
                   and status_int <> 5) t
         inner join (select re.registerid_chr,
                           re.bedid_chr,
                           re.type_int,
                           re.diagnose_vchr,
                           re.inpatientcount_int,
                           re.state_int,
                           re.pstatus_int,
                           re.casedoctor_chr,
                           re.deptid_chr,
                           re.areaid_chr,
                           re.nursing_class,
                           re.patientid_chr,
                           rehis.emrinpatientid,
                           rehis.emrinpatientdate,
                           rehis.hisinpatientid_chr,
                           rehis.hisinpatientdate
                      from t_opr_bih_register re
                     inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                               re.registerid_chr
                     where re.areaid_chr = ?
                       and re.status_int = 1
                       and re.pstatus_int not in (0, 3)
                       and not exists
                     (select re2.registerid_chr
                              from t_opr_bih_register re2
                             where re2.pstatus_int = 2
                               and re2.registerid_chr = re.registerid_chr
                               and exists
                             (select registerid_chr
                                      from t_opr_bih_register re3
                                     where re3.bedid_chr = re2.bedid_chr
                                       and re3.pstatus_int not in (0, 2, 3)
                                       and re3.status_int = 1))) d on t.areaid_chr =
                                                                      d.areaid_chr
                                                                  and t.bedid_chr =
                                                                      d.bedid_chr) dd
 inner join t_opr_bih_registerdetail p on dd.registerid_chr =
                                          p.registerid_chr

 order by bed_no";

                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strAreaID.Trim();
                objDPArr[1].Value = p_strAreaID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_objResultArr = m_objGetEmrBed_VO(dtResult, p_strHospitalNO);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            //finally { //objHrpServ.Dispose(); }
            return lngRes;

        }
        private clsEmrBed_VO[] m_objGetEmrBed_VO(DataTable p_dtbResult, string p_strHospitalNO)
        {
            int intRowCount = p_dtbResult.Rows.Count;
            clsEmrBed_VO[] objResultArr = new clsEmrBed_VO[intRowCount];
            DataRow objRow = null;
            int intTmp = 0;
            Double dblTmp = 0;
            DateTime dtm1900 = new DateTime(1900, 1, 1);
            DateTime dtmTmp = dtm1900;
            for (int i1 = 0; i1 < intRowCount; i1++)
            {
                objRow = p_dtbResult.Rows[i1];
                objResultArr[i1] = new clsEmrBed_VO();
                objResultArr[i1].m_objInbedPatient = new clsEmrInBedPatient_VO();
                objResultArr[i1].m_strBEDID_CHR = objRow["BEDID_CHR"].ToString();
                objResultArr[i1].m_strAREAID_CHR = objRow["AREAID_CHR"].ToString();
                objResultArr[i1].m_strCODE_CHR = objRow["CODE_CHR"].ToString();
                if (int.TryParse(objRow["BEDSTATUS_INT"].ToString(), out intTmp))
                    objResultArr[i1].m_intSTATUS_INT = intTmp;
                else
                    objResultArr[i1].m_intSTATUS_INT = 1;

                if (Double.TryParse(objRow["RATE_MNY"].ToString(), out dblTmp))
                    objResultArr[i1].m_dblRATE_MNY = dblTmp;
                else
                    objResultArr[i1].m_dblRATE_MNY = 1;
                intTmp = 0;
                if (int.TryParse(objRow["SEX_INT"].ToString(), out intTmp))
                    objResultArr[i1].m_intSEX_INT = intTmp;
                else
                    objResultArr[i1].m_intSEX_INT = 0;
                intTmp = 0;
                if (int.TryParse(objRow["CATEGORY_INT"].ToString(), out intTmp))
                    objResultArr[i1].m_intCATEGORY_INT = intTmp;
                else
                    objResultArr[i1].m_intCATEGORY_INT = 1;
                dblTmp = 0;
                if (Double.TryParse(objRow["AIRRATE_MNY"].ToString(), out dblTmp))
                    objResultArr[i1].m_dblAIRRATE_MNY = dblTmp;
                else
                    objResultArr[i1].m_dblAIRRATE_MNY = 1;
                objResultArr[i1].m_strCHARGEITEMID_CHR = objRow["CHARGEITEMID_CHR"].ToString();
                if (objRow["PATIENTID_CHR"] is System.DBNull)
                {
                    objResultArr[i1].m_objInbedPatient = null;
                }
                else
                {
                    //�ڴ����˻�����Ϣ����С���ϣ�
                    objResultArr[i1].m_objInbedPatient.m_strPATIENTID_CHR = objRow["PATIENTID_CHR"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strINPATIENTID_CHR = objRow["EMRINPATIENTID"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strINPATIENT_DAT = objRow["EMRINPATIENTDATE"].ToString();
                    intTmp = 0;
                    if (int.TryParse(objRow["STATE_INT"].ToString(), out intTmp))
                        objResultArr[i1].m_objInbedPatient.m_intSTATE_INT = intTmp;
                    else
                        objResultArr[i1].m_objInbedPatient.m_intSTATE_INT = 3;
                    intTmp = 0;
                    if (int.TryParse(objRow["NURSING_CLASS"].ToString(), out intTmp))
                        objResultArr[i1].m_objInbedPatient.m_intNurseClass = intTmp;
                    else
                        objResultArr[i1].m_objInbedPatient.m_intNurseClass = -1;
                    objResultArr[i1].m_objInbedPatient.m_strLASTNAME_VCHR = objRow["LASTNAME_VCHR"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strSEX_CHR = objRow["SEX_CHR"].ToString();

                    objResultArr[i1].m_objInbedPatient.m_strBIRTH_DAT = objRow["BIRTH_DAT"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strREGISTERID_CHR = objRow["REGISTERID_CHR"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strDEPTID_CHR = objRow["DEPTID_CHR"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strAREAID_CHR = objRow["AREAID_CHR"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strCODE_CHR = objRow["CODE_CHR"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strBEDID_CHR = objRow["BEDID_CHR"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strEXTENDID_VCHR = objRow["HISINPATIENTID_CHR"].ToString();
                    intTmp = 0;
                    if (int.TryParse(objRow["INPATIENTCOUNT_INT"].ToString(), out intTmp))
                        objResultArr[i1].m_objInbedPatient.m_intINPATIENTCOUNT_INT = intTmp;
                    else
                        objResultArr[i1].m_objInbedPatient.m_intINPATIENTCOUNT_INT = 1;
                    intTmp = 0;
                    if (int.TryParse(objRow["PSTATUS_INT"].ToString(), out intTmp))
                        objResultArr[i1].m_objInbedPatient.m_intPSTATUS_INT = intTmp;
                    else
                        objResultArr[i1].m_objInbedPatient.m_intPSTATUS_INT = 1;
                    objResultArr[i1].m_objInbedPatient.m_strEMRInPatientID = objRow["EMRINPATIENTID"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strHISInPatientID = objRow["HISINPATIENTID_CHR"].ToString();

                    if (DateTime.TryParse(objRow["emrinpatientdate"].ToString(), out dtmTmp))
                        objResultArr[i1].m_objInbedPatient.m_dtmEMRInDate = dtmTmp;
                    else
                        objResultArr[i1].m_objInbedPatient.m_dtmEMRInDate = dtm1900;
                    dtmTmp = dtm1900;
                    if (DateTime.TryParse(objRow["hisinpatientdate"].ToString(), out dtmTmp))
                        objResultArr[i1].m_objInbedPatient.m_dtmHISInDate = dtmTmp;
                    else
                        objResultArr[i1].m_objInbedPatient.m_dtmHISInDate = dtm1900;

                    string strAge = "";
                    objResultArr[i1].m_objInbedPatient.m_strAGESHORT_CHR = m_strCalAge(objResultArr[i1].m_objInbedPatient.m_strBIRTH_DAT, objResultArr[i1].m_objInbedPatient.m_dtmHISInDate, p_strHospitalNO, out strAge);
                    objResultArr[i1].m_objInbedPatient.m_strAGELONG_CHR = strAge;
                    objResultArr[i1].m_objInbedPatient.m_strCaseDoctorId = objRow["casedoctor_chr"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strCaseDoctorName = objRow["casedoctorname"].ToString();
                    objResultArr[i1].m_objInbedPatient.m_strDiagnos = objRow["diagnose_vchr"].ToString();

                }
            }
            return objResultArr;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_strBirthDate"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        private string m_strCalAge(string p_strBirthDate, DateTime p_dtmInpatientDate,string p_strHospitalNO, out string p_strLongAge)
        {
            p_strLongAge = "δ֪";
            string strAge = p_strLongAge;
            if (!string.IsNullOrEmpty(p_strBirthDate))
            {
                DateTime dtmTime = p_dtmInpatientDate;
                if (dtmTime == DateTime.MinValue || dtmTime == new DateTime(1900, 1, 1))
                    dtmTime = DateTime.Now;
                DateTime m_dtmBirth = DateTime.Parse(p_strBirthDate);
                if (p_strHospitalNO != "440104001")
                {
                    if ((int)(DateTime.Now.Year - m_dtmBirth.Year) > 0)
                    {
                        strAge = ((int)(DateTime.Now.Year - m_dtmBirth.Year)).ToString() + "��";
                        if (int.Parse(m_dtmBirth.Month.ToString()) != 12)
                            p_strLongAge = ((int)(DateTime.Now.Year - m_dtmBirth.Year)).ToString() + "��" + m_dtmBirth.Month.ToString() + "��";
                        else
                            p_strLongAge = ((int)(DateTime.Now.Year - m_dtmBirth.Year) + 1).ToString() + "��" + "0��";
                    }
                    else if ((int)(DateTime.Now.Month - m_dtmBirth.Month) > 0)
                    {
                        strAge = ((int)(DateTime.Now.Month - m_dtmBirth.Month)).ToString() + "��";
                        if (int.Parse(m_dtmBirth.Month.ToString()) != 12)
                            p_strLongAge = "0��" + ((int)(DateTime.Now.Month - m_dtmBirth.Month)).ToString() + "��";
                        else
                            p_strLongAge = "1��" + "0��";

                    }
                    else
                    {
                        strAge = ((int)(DateTime.Now.Day - m_dtmBirth.Day)).ToString() + "��";
                        p_strLongAge = ((int)(DateTime.Now.Day - m_dtmBirth.Day)).ToString() + "��";
                    }
                }
                else
                {
                    #region ��һ����
                    //�㷨������
                    //1������סԺʱ�䣭������Ϊ���䣻
                    //2��������1������䣬ֻ�㵽�꣬ȡ����ʣ�಻��һ����·ݶ�����
                    //     ����ʵ������Ϊ21��XX�£�����ʾ����Ϊ21�꼴�ɣ�
                    //3����1�����ڵİ����㣬ֻ�㵽�£�ȡ����ʣ�಻��һ�µ�����������
                    //      ����ʵ������Ϊ10��20�죬����ʾ����Ϊ10�¼��ɣ�
                    //4����1�����ڵİ����㣬����һ����1�죻
                    //5��������200������䲻��ʾ��

                    int intAge = -1;
                    int intMonth = -1;
                    int intDay = -1;
                    if (DateTime.Compare(dtmTime, m_dtmBirth) < 1)
                    {
                        return strAge;
                    }
                    if ((int)(dtmTime.Year - m_dtmBirth.Year) > 0)
                    {
                        intAge = dtmTime.Year - m_dtmBirth.Year;
                        if (dtmTime.Month < m_dtmBirth.Month)
                        {
                            --intAge;
                            if (intAge == 0)
                            {
                                intMonth = dtmTime.Month + 12 - m_dtmBirth.Month;
                            }
                            if (dtmTime.Day < m_dtmBirth.Day)
                            {
                                --intMonth;
                            }
                            if (intMonth == 0)
                            {
                                //���ǿ���
                                System.TimeSpan diff = dtmTime.Date.Subtract(m_dtmBirth.Date);
                                intDay = (int)diff.TotalDays;
                            }
                        }
                        else if (dtmTime.Month == m_dtmBirth.Month)
                        {
                            if (dtmTime.Day < m_dtmBirth.Day)
                            {
                                --intAge;
                            }
                            if (intAge == 0)
                            {
                                intMonth = 11;
                            }
                        }
                        if (intAge > 200)
                            intAge = -1;
                        if (intAge > 0)
                            strAge = intAge.ToString() + "��";
                        else if (intAge == 0)
                        {
                            if (intMonth > 0)
                                strAge = intMonth.ToString() + "��";
                            else if (intDay > 0)
                                strAge = intDay.ToString() + "��";
                        }
                    }
                    else if ((int)(dtmTime.Month - m_dtmBirth.Month) > 0)
                    {
                        intMonth = dtmTime.Month - m_dtmBirth.Month;

                        if (dtmTime.Day < m_dtmBirth.Day)
                        {
                            --intMonth;
                        }
                        if (intMonth == 0)
                            intDay = dtmTime.DayOfYear - m_dtmBirth.DayOfYear;
                        if (intMonth > 0)
                            strAge = intMonth.ToString() + "��";
                        else if (intDay > 0)
                            strAge = intDay.ToString() + "��";
                    }
                    else
                    {
                        intDay = dtmTime.Day - m_dtmBirth.Day;
                        if (intDay == 0)
                            intDay++;
                        strAge = intDay.ToString() + "��";
                    }
                    p_strLongAge = strAge;
                    #endregion ��һ����
                }
            }
            else
            {
                strAge = "δ֪";
                p_strLongAge = "δ֪";
            }
            return strAge;
        }
        /// <summary>
        /// ���ݡ�������������ID��������Ĵ�λ��ģ�����һ�ȡ����(�Ѱ����ڴ�������Ϣ)�б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_blnIsRoom"></param>
        /// <param name="p_strBedCodeLike"></param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoLikeBedCode(string p_strAreaID, bool p_blnIsRoom,string p_strBedCodeLike, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            if (string.IsNullOrEmpty(p_strAreaID) || string.IsNullOrEmpty(p_strBedCodeLike)) return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetBedInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //����״̬=5Ϊɾ��״̬ ��Ժ�Ǽ�״̬=1��Ч
                //PSTATUS_INT={0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ;4=���} ֻ��ȡ��Ժ���ϴ��Ĳ�����Ϣ
                string strSQL = @"select dd.code_chr,
       dd.category_int,
       dd.bedextendid_vchr,
       dd.sex_int,
       dd.bedstatus_int,
       dd.rate_mny,
       dd.airrate_mny,
       dd.chargeitemid_chr,
       dd.registerid_chr,
       dd.bedid_chr,
       dd.type_int,
       dd.diagnose_vchr,
       dd.inpatientcount_int,
       dd.state_int,
       dd.pstatus_int,
       dd.casedoctor_chr,
       dd.deptid_chr,
       dd.areaid_chr,
       dd.nursing_class,
       dd.patientid_chr,
       dd.emrinpatientid,
       dd.emrinpatientdate,
       dd.hisinpatientid_chr,
       dd.hisinpatientdate,
       dd.bed_no,
       p.lastname_vchr,
       p.sex_chr,
       p.birth_dat,
       f_getempnamebyid(dd.casedoctor_chr) casedoctorname,
       bed_no sequenceno
  from (select t.code_chr,
               t.category_int,
               t.extendid_vchr bedextendid_vchr,
               t.sex_int,
               t.status_int bedstatus_int,
               t.rate_mny,
               t.airrate_mny,
               t.chargeitemid_chr,
               d.registerid_chr,
               d.bedid_chr,
               d.type_int,
               d.diagnose_vchr,
               d.inpatientcount_int,
               d.state_int,
               d.pstatus_int,
               d.casedoctor_chr,
               d.deptid_chr,
               d.areaid_chr,
               d.nursing_class,
               d.patientid_chr,
               d.emrinpatientid,
               d.emrinpatientdate,
               d.hisinpatientid_chr,
               d.hisinpatientdate,
               t.bed_no
          from (select bedid_chr,
                       areaid_chr,
                       code_chr,
                       status_int,
                       rate_mny,
                       sex_int,
                       category_int,
                       airrate_mny,
                       chargeitemid_chr,
                       airchargeflg_int,
                       airchargeitemid_chr,
                       extendid_vchr,
                       bihregisterid_chr,
                       creatorid_chr,
                       creat_dat,
                       cancelerid_chr,
                       cancel_dat,
                       bed_no
                  from t_bse_bed
                 where areaid_chr = ?
                   and code_chr like ?
                   and status_int <> 5) t
          left outer join (select re.registerid_chr,
                                 re.bedid_chr,
                                 re.type_int,
                                 re.diagnose_vchr,
                                 re.inpatientcount_int,
                                 re.state_int,
                                 re.pstatus_int,
                                 re.casedoctor_chr,
                                 re.deptid_chr,
                                 re.areaid_chr,
                                 re.nursing_class,
                                 re.patientid_chr,
                                 rehis.emrinpatientid,
                                 rehis.emrinpatientdate,
                                 rehis.hisinpatientid_chr,
                                 rehis.hisinpatientdate
                            from t_opr_bih_register re
                           inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                                                     re.registerid_chr
                           where re.areaid_chr = ?
                             and re.status_int = 1
                             and re.pstatus_int not in (0, 3)
                             and not exists
                           (select re2.registerid_chr
                                    from t_opr_bih_register re2
                                   where re2.pstatus_int = 2
                                     and re2.registerid_chr =
                                         re.registerid_chr
                                     and exists
                                   (select registerid_chr
                                            from t_opr_bih_register re3
                                           where re3.bedid_chr = re2.bedid_chr
                                             and re3.pstatus_int not in
                                                 (0, 2, 3)
                                             and re3.status_int = 1))) d on t.areaid_chr =
                                                                            d.areaid_chr
                                                                        and t.bedid_chr =
                                                                            d.bedid_chr) dd
  left join t_opr_bih_registerdetail p on dd.registerid_chr =
                                          p.registerid_chr

 order by bed_no";

                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strAreaID.Trim();
                objDPArr[1].Value = p_strBedCodeLike.Trim() + "%";
                objDPArr[2].Value = p_strAreaID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //finally { //objHrpServ.Dispose(); }
            return lngRes;

        }
        
        /// <summary>
        /// ��ȡ������λ�Ĳ������ڵĴ�λ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBedID">������λ�Ĵ�λID</param>
        /// <param name="p_strBedCode">���ذ��������ڴ�λ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetWrapBedPatientBedName(string p_strBedID,out string p_strBedCode)
        {
            long lngRes = 0;
            p_strBedCode = "";
            if (string.IsNullOrEmpty(p_strBedID)) return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetBedInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                string strSQL = @"select a.code_chr
  from t_opr_bih_wrapbed t
 inner join t_opr_bih_register c on t.registerid_chr = c.registerid_chr and c.status_int = 1
 inner join t_bse_bed a on c.bedid_chr = a.bedid_chr
 where t.bedid_chr = ?";

                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strBedID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 &&dtResult.Rows.Count > 0 )
                    p_strBedCode = dtResult.Rows[0][0].ToString();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //finally { //objHrpServ.Dispose(); }
            return lngRes;

        }
        /// <summary>
        /// ��ȡ��ָ����������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">������ID��</param>
        /// <param name="p_dtbValue">ָ��������Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnCheckIfCanSetBedDept(string p_strDeptOrAreaID)
        {
            long lngRes = 0;
            if (p_strDeptOrAreaID == null || p_strDeptOrAreaID == "") return false;
            try
            {
                //				long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsHospitalManagerService","m_lngGetAreaInfo");
                //				if(lngCheckRes <= 0)
                //					return lngCheckRes;
                string strSQL = @" select count(t.bedid_chr) BedCount
   from t_bse_bed t
  where t.areaid_chr = ?
    and t.status_int <> 5";
                clsHRPTableService objHrpServ = new clsHRPTableService();
                IDataParameter[] arrParam = null;
                objHrpServ.CreateDatabaseParameter(1, out arrParam);
                arrParam[0].Value = p_strDeptOrAreaID;
                DataTable dtResult = new DataTable();
                lngRes = objHrpServ.lngGetDataTableWithParameters(strSQL, ref dtResult, arrParam);
                if (lngRes > 0)
                {
                    string str = dtResult.Rows[0][0].ToString();
                    return str != "0";
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return false;

        }
        #endregion

        #region ����
        /// <summary>
        /// ���ݲ�����������ID��ȡ�����б�
        /// �÷������ص�ֻ���ڴ����˵��б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">����ID������ID��</param>
        /// <param name="p_blnIsRoom">�Ƿ񲡷�</param>
        /// <param name="p_dtbValue">�����ڴ������б�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfo(string p_strID, bool p_blnIsRoom, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetPatientInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select t.code_chr,
       d.bedid_chr,
       d.patientid_chr,
       a.lastname_vchr,
       d.registerid_chr,
       d.inpatientid_chr,
       d.inpatient_dat,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       d.state_int,
       t.bed_no sequenceno
  from t_bse_bed             t,
       t_opr_bih_register    d,
       t_bse_patient         a,
       t_bse_hisemr_relation rehis
 where t.bedid_chr = d.bedid_chr
   and d.patientid_chr = a.patientid_chr
   and d.registerid_chr = rehis.registerid_chr
   and d.pstatus_int <> ?
   and d.pstatus_int <> ?
   and t.areaid_chr = ?
   and d.status_int = 1
   and not exists
 (select re2.registerid_chr
          from t_opr_bih_register re2
         where re2.pstatus_int = 2
           and re2.registerid_chr = d.registerid_chr
           and exists (select registerid_chr
                  from t_opr_bih_register re3
                 where re3.bedid_chr = re2.bedid_chr
                   and re3.pstatus_int not in (0, 2, 3)
                   and re3.status_int = 1))
 order by t.bed_no";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //��ֵ
                objDPArr[0].Value = 3;
                objDPArr[1].Value = 0;
                objDPArr[2].Value = p_strID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        /// ���ݿ���ID�б��ȡ�����б�
        /// �÷������ص�ֻ���ڴ����˵��б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID"></param>
        /// <param name="p_blnIsRoom"></param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfo(string[] p_strIDArr, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            if (p_strIDArr == null || p_strIDArr.Length == 0)
                return -1;
            clsHRPTableService objServ = new clsHRPTableService();
            try
            {
                //    com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //    long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetPatientInfo");
                //    objPrivilege.Dispose();
                //    if (lngCheckRes <= 0)
                //        return lngCheckRes;
                /* changed by wjqin(06-1-24)*/
                /*-------------------------------------------------
                string strSQL=@"select t.code_chr,
       d.bedid_chr,
       d.patientid_chr,
       a.lastname_vchr,
       a.sex_chr,
       a.birth_dat,
       d.registerid_chr,
       d.inpatientid_chr,
       d.inpatient_dat,
       d.state_int,
       d.deptid_chr,
       c.deptname_vchr,
       round(months_between(sysdate,a.birth_dat)/12,0) age

  from t_bse_bed t
 inner join T_OPR_BIH_REGISTER d on t.bedid_chr = d.bedid_chr
 inner join t_bse_patient a on d.patientid_chr = a.patientid_chr
 inner join t_bse_deptdesc c on d.deptid_chr = c.deptid_chr
 where d.PSTATUS_INT <> 3
   and d.PSTATUS_INT <> 0
                            and [DEPT]";
                            -------------------------------------------------*/
                string strSQL = @"select t.code_chr,
       lpad(rtrim(t.code_chr), 5, '0') as code_chr2,
       d.bedid_chr,
       d.patientid_chr,
       a.lastname_vchr,
       a.sex_chr,
       a.birth_dat,
       d.registerid_chr,
       d.inpatientid_chr,
       d.inpatient_dat,
       d.state_int,
       d.deptid_chr,
       c.deptname_vchr,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       round(months_between(sysdate, a.birth_dat) / 12, 0) age
  from t_bse_bed t
 inner join t_opr_bih_register d on t.bedid_chr = d.bedid_chr
                                and d.status_int = 1
 inner join t_bse_patient a on d.patientid_chr = a.patientid_chr
 inner join t_bse_deptdesc c on d.deptid_chr = c.deptid_chr
 inner join t_bse_hisemr_relation rehis on d.registerid_chr =
                                           rehis.registerid_chr
 where d.pstatus_int <> 3
   and d.pstatus_int <> 0
   and not exists
 (select re2.registerid_chr
          from t_opr_bih_register re2
         where re2.pstatus_int = 2
           and re2.registerid_chr = d.registerid_chr
           and exists (select registerid_chr
                  from t_opr_bih_register re3
                 where re3.bedid_chr = re2.bedid_chr
                   and re3.pstatus_int not in (0, 2, 3)
                   and re3.status_int = 1))
   and [dept]";
                /*<------------------------------------------------------------*/
                string str = "(d.deptid_chr = '" + p_strIDArr[0] + "' ";
                for (int i = 1 ; i < p_strIDArr.Length ; i++)
                    str += "or d.deptid_chr = '" + p_strIDArr[i] + "'";
                str = str + ")";
                DataTable dtResult = new DataTable();
                lngRes = objServ.DoGetDataTable(strSQL.Replace("[dept]", str), ref dtResult);
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
                //objServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        /// ΪPDAȡ���ݣ����ݿ���ID�б��ȡ�����б�
        /// �÷������ص�ֻ���ڴ����˵��б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID"></param>
        /// <param name="p_blnIsRoom"></param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfo(string[] p_strIDArr, out DataTable p_dtbValue, bool pda)
        {
            long lngRes = 0;
            p_dtbValue = null;
            if (p_strIDArr == null || p_strIDArr.Length == 0)
                return -1;
            clsHRPTableService objServ = new clsHRPTableService();
            try
            {

                string strSQL = @"select t.code_chr,
       lpad(rtrim(t.code_chr),5,'0') as code_chr2,
       d.bedid_chr,
       d.patientid_chr,
       a.lastname_vchr,
       a.sex_chr,
       a.birth_dat,
       d.registerid_chr,
       d.inpatientid_chr,
       d.inpatient_dat,
       d.state_int,
       d.deptid_chr,
       c.deptname_vchr,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       (to_char(round(months_between(sysdate,a.birth_dat)/12,0))||'��'||to_char(round(mod((sysdate-a.birth_dat),365),0))||'��') age

  from t_bse_bed t
 inner join t_opr_bih_register d on t.bedid_chr = d.bedid_chr and d.status_int = 1
 inner join t_bse_patient a on d.patientid_chr = a.patientid_chr
 inner join t_bse_deptdesc c on d.deptid_chr = c.deptid_chr
 inner join t_bse_hisemr_relation rehis on d.registerid_chr = rehis.registerid_chr
 where d.pstatus_int <> 3
   and d.pstatus_int <> 0
							and [dept]";

                string str = "(d.deptid_chr = '" + p_strIDArr[0] + "' ";
                for (int i = 1 ; i < p_strIDArr.Length ; i++)
                    str += "or d.deptid_chr = '" + p_strIDArr[i] + "'";
                str = str + ")";
                DataTable dtResult = new DataTable();
                lngRes = objServ.DoGetDataTable(strSQL.Replace("[dept]", str), ref dtResult);
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
                //objServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        /// ���ݲ���סԺ�ǼǺŻ�ȡָ��������ϸ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">����סԺ�ǼǺ�</param>
        /// <param name="p_dtbValue">������ϸ��Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfoDetail(string p_strRegisterId, out clsEmrPatient_VO p_objEmrPatient)
        {
            long lngRes = 0;
            p_objEmrPatient = null;
            if (string.IsNullOrEmpty(p_strRegisterId)) return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetPatientInfoDetail");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                #region SQL
                string strSQL = @"select t.lastname_vchr,
       t.idcard_chr,
       t.married_chr,
       t.birthplace_vchr,
       t.homeaddress_vchr,
       t.sex_chr,
       t.nationality_vchr,
       t.firstname_vchr,
       t.birth_dat,
       t.race_vchr,
       t.nativeplace_vchr,
       t.occupation_vchr,
       t.name_vchr,
       t.homephone_vchr,
       t.officephone_vchr,
       t.insuranceid_vchr,
       t.mobile_chr,
       t.officeaddress_vchr,
       t.employer_vchr,
       t.officepc_vchr,
       t.homepc_chr,
       t.email_vchr,
       t.contactpersonfirstname_vchr,
       t.contactpersonlastname_vchr,
       t.contactpersonaddress_vchr,
       t.contactpersonphone_vchr,
       t.contactpersonpc_chr,
       t.patientrelation_vchr,
       t.firstdate_dat,
       t.isemployee_int,
       t.status_int,
       t.deactivate_dat,
       t.operatorid_chr,
       t.modify_dat,
       t.optimes_int,
       t.govcard_chr,
       t.bloodtype_chr,
       t.ifallergic_int,
       t.allergicdesc_vchr,
       t.difficulty_vchr,
       t.residenceplace_vchr,
       a.modify_dat as currentmodify_dat,
       a.isbooking_int,
       a.inpatient_dat,
       a.deptid_chr,
       a.areaid_chr,
       a.bedid_chr,
       a.type_int,
       a.diagnose_vchr,
       a.limitrate_mny,
       a.inpatientcount_int,
       a.state_int,
       a.status_int as currentstatus_int,
       a.operatorid_chr as currentoperatorid_chr,
       a.pstatus_int,
       a.casedoctor_chr,
       a.inpatientnotype_int,
       a.des_vchr,
       a.mzdoctor_chr,
       a.mzdiagnose_vchr,
       a.diagnoseid_chr,
       a.inareadate_dat,
       a.icd10diagid_vchr,
       a.icd10diagtext_vchr,
       a.registerid_chr,
       a.patientid_chr,
       a.paytypeid_chr,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       pt.paytypename_vchr,
       de.deptname_vchr deptname,
       ar.deptname_vchr areaname,
       be.code_chr bedcode 
       --p.patientsources_vchr
  from t_opr_bih_register a
 inner join t_bse_hisemr_relation rehis on a.registerid_chr =
                                           rehis.registerid_chr
 inner join t_opr_bih_registerdetail t on a.registerid_chr =
                                          t.registerid_chr
 inner join t_bse_deptdesc de on a.deptid_chr = de.deptid_chr
                             and de.status_int = 1
 inner join t_bse_deptdesc ar on a.areaid_chr = ar.deptid_chr
                             and ar.status_int = 1
 inner join t_bse_bed be on be.bedid_chr = a.bedid_chr
                        /*and be.status_int <> 5*/
 inner join t_bse_patient p on p.patientid_chr = a.patientid_chr
                           and p.status_int = 1
  left outer join t_bse_patientpaytype pt on pt.paytypeid_chr =
                                             a.paytypeid_chr
                                         and pt.isusing_num = 1
 where a.status_int = 1
   and t.registerid_chr = ?";

                #endregion SQL
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strRegisterId.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                    p_objEmrPatient = m_objGetEmrPatientDetailVO(dtResult.Rows[0]);
                //�ͷ�
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        private clsEmrPatient_VO m_objGetEmrPatientDetailVO(DataRow p_objRow)
        {
            clsEmrPatient_VO objEmrPatient = new clsEmrPatient_VO();
            DateTime dtmTemp = new DateTime(1900,1,1);
            objEmrPatient.m_strPATIENTID_CHR = p_objRow["PATIENTID_CHR"].ToString();
            objEmrPatient.m_strINPATIENTID_CHR = p_objRow["emrinpatientid"].ToString();
            objEmrPatient.m_strLASTNAME_VCHR = p_objRow["LASTNAME_VCHR"].ToString();
            objEmrPatient.m_strIDCARD_CHR = p_objRow["IDCARD_CHR"].ToString();
            objEmrPatient.m_strMARRIED_CHR = p_objRow["MARRIED_CHR"].ToString();
            objEmrPatient.m_strBIRTHPLACE_VCHR = p_objRow["BIRTHPLACE_VCHR"].ToString();
            objEmrPatient.m_strHOMEADDRESS_VCHR = p_objRow["HOMEADDRESS_VCHR"].ToString();
            objEmrPatient.m_strSEX_CHR = p_objRow["SEX_CHR"].ToString();
            objEmrPatient.m_strNATIONALITY_VCHR = p_objRow["NATIONALITY_VCHR"].ToString();
            objEmrPatient.m_strFIRSTNAME_VCHR = p_objRow["FIRSTNAME_VCHR"].ToString();
            if (DateTime.TryParse(p_objRow["BIRTH_DAT"].ToString(), out dtmTemp))
                objEmrPatient.m_strBIRTH_DAT = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
            objEmrPatient.m_strRACE_VCHR = p_objRow["RACE_VCHR"].ToString();
            objEmrPatient.m_strNATIVEPLACE_VCHR = p_objRow["NATIVEPLACE_VCHR"].ToString();
            objEmrPatient.m_strRESIDENCEPLACE_VCHR = p_objRow["RESIDENCEPLACE_VCHR"].ToString();
            objEmrPatient.m_strOCCUPATION_VCHR = p_objRow["OCCUPATION_VCHR"].ToString();
            objEmrPatient.m_strNAME_VCHR = p_objRow["NAME_VCHR"].ToString();
            objEmrPatient.m_strHOMEPHONE_VCHR = p_objRow["HOMEPHONE_VCHR"].ToString();
            objEmrPatient.m_strOFFICEPHONE_VCHR = p_objRow["OFFICEPHONE_VCHR"].ToString();
            objEmrPatient.m_strINSURANCEID_VCHR = p_objRow["INSURANCEID_VCHR"].ToString();
            objEmrPatient.m_strMOBILE_CHR = p_objRow["MOBILE_CHR"].ToString();
            objEmrPatient.m_strOFFICEADDRESS_VCHR = p_objRow["OFFICEADDRESS_VCHR"].ToString();
            objEmrPatient.m_strEMPLOYER_VCHR = p_objRow["EMPLOYER_VCHR"].ToString();
            objEmrPatient.m_strOFFICEPC_VCHR = p_objRow["OFFICEPC_VCHR"].ToString();
            objEmrPatient.m_strHOMEPC_CHR = p_objRow["HOMEPC_CHR"].ToString();
            objEmrPatient.m_strEMAIL_VCHR = p_objRow["EMAIL_VCHR"].ToString();
            objEmrPatient.m_strCONTACTPERSONFIRSTNAME_VCHR = p_objRow["CONTACTPERSONFIRSTNAME_VCHR"].ToString();
            objEmrPatient.m_strCONTACTPERSONLASTNAME_VCHR = p_objRow["CONTACTPERSONLASTNAME_VCHR"].ToString();
            objEmrPatient.m_strCONTACTPERSONADDRESS_VCHR = p_objRow["CONTACTPERSONADDRESS_VCHR"].ToString();
            objEmrPatient.m_strCONTACTPERSONPHONE_VCHR = p_objRow["CONTACTPERSONPHONE_VCHR"].ToString();
            objEmrPatient.m_strCONTACTPERSONPC_CHR = p_objRow["CONTACTPERSONPC_CHR"].ToString();
            objEmrPatient.m_strPATIENTRELATION_VCHR = p_objRow["PATIENTRELATION_VCHR"].ToString();
            objEmrPatient.m_strFIRSTDATE_DAT = p_objRow["FIRSTDATE_DAT"].ToString();
            int intTemp = 0;
            if(int.TryParse(p_objRow["ISEMPLOYEE_INT"].ToString(),out intTemp))
                objEmrPatient.m_intISEMPLOYEE_INT = intTemp;
            if (int.TryParse(p_objRow["STATUS_INT"].ToString(), out intTemp))
                objEmrPatient.m_intSTATUS_INT = intTemp;
            objEmrPatient.m_strDEACTIVATE_DAT = p_objRow["DEACTIVATE_DAT"].ToString();
            objEmrPatient.m_strOPERATORID_CHR = p_objRow["OPERATORID_CHR"].ToString();
            objEmrPatient.m_strMODIFY_DAT = p_objRow["MODIFY_DAT"].ToString();
            objEmrPatient.m_strPAYTYPEID_CHR = p_objRow["PAYTYPEID_CHR"].ToString();
            objEmrPatient.m_strPayTypeID = p_objRow["PAYTYPEID_CHR"].ToString();
            if (int.TryParse(p_objRow["OPTIMES_INT"].ToString(), out intTemp))
                objEmrPatient.m_intOPTIMES_INT = intTemp;
            objEmrPatient.m_strGOVCARD_CHR = p_objRow["GOVCARD_CHR"].ToString();
            objEmrPatient.m_strBLOODTYPE_CHR = p_objRow["BLOODTYPE_CHR"].ToString();
            if (int.TryParse(p_objRow["IFALLERGIC_INT"].ToString(), out intTemp))
                objEmrPatient.m_intIFALLERGIC_INT = intTemp;
            objEmrPatient.m_strALLERGICDESC_VCHR = p_objRow["ALLERGICDESC_VCHR"].ToString();
            objEmrPatient.m_strDIFFICULTY_VCHR = p_objRow["DIFFICULTY_VCHR"].ToString();
            //סԺ��Ϣ
            objEmrPatient.m_strREGISTERID_CHR = p_objRow["REGISTERID_CHR"].ToString();
            objEmrPatient.m_strCurrentMODIFY_DAT = p_objRow["CurrentMODIFY_DAT"].ToString();
            if (int.TryParse(p_objRow["ISBOOKING_INT"].ToString(), out intTemp))
                objEmrPatient.m_intISBOOKING_INT = intTemp;
            objEmrPatient.m_strINPATIENT_DAT = p_objRow["emrinpatientdate"].ToString();
            objEmrPatient.m_strDEPTID_CHR = p_objRow["DEPTID_CHR"].ToString();
            objEmrPatient.m_strAREAID_CHR = p_objRow["AREAID_CHR"].ToString();
            objEmrPatient.m_strBEDID_CHR = p_objRow["BEDID_CHR"].ToString();
            if (int.TryParse(p_objRow["TYPE_INT"].ToString(), out intTemp))
                objEmrPatient.m_intTYPE_INT = intTemp;
            objEmrPatient.m_strDIAGNOSE_VCHR = p_objRow["DIAGNOSE_VCHR"].ToString();
            if (int.TryParse(p_objRow["LIMITRATE_MNY"].ToString(), out intTemp))
                objEmrPatient.m_dblLIMITRATE_MNY = intTemp;
            if (int.TryParse(p_objRow["INPATIENTCOUNT_INT"].ToString(), out intTemp))
                objEmrPatient.m_intINPATIENTCOUNT_INT = intTemp;
            if (int.TryParse(p_objRow["STATE_INT"].ToString(), out intTemp))
                objEmrPatient.m_intSTATE_INT = intTemp;
            if (int.TryParse(p_objRow["CURRENTSTATUS_INT"].ToString(), out intTemp))
                objEmrPatient.m_intCurrentSTATUS_INT = intTemp;
            objEmrPatient.m_strCurrentOPERATORID_CHR = p_objRow["CurrentOPERATORID_CHR"].ToString();
            if (int.TryParse(p_objRow["PSTATUS_INT"].ToString(), out intTemp))
                objEmrPatient.m_intPSTATUS_INT = intTemp;
            objEmrPatient.m_strCASEDOCTOR_CHR = p_objRow["CASEDOCTOR_CHR"].ToString();
            if (int.TryParse(p_objRow["INPATIENTNOTYPE_INT"].ToString(), out intTemp))
                objEmrPatient.m_intINPATIENTNOTYPE_INT = intTemp;
            objEmrPatient.m_strDES_VCHR = p_objRow["DES_VCHR"].ToString();
            objEmrPatient.m_strMZDOCTOR_CHR = p_objRow["MZDOCTOR_CHR"].ToString();
            objEmrPatient.m_strMZDIAGNOSE_VCHR = p_objRow["MZDIAGNOSE_VCHR"].ToString();
            objEmrPatient.m_strDIAGNOSEID_CHR = p_objRow["DIAGNOSEID_CHR"].ToString();
            objEmrPatient.m_strINAREADATE_DAT = p_objRow["INAREADATE_DAT"].ToString();
            objEmrPatient.m_strICD10DIAGID_VCHR = p_objRow["ICD10DIAGID_VCHR"].ToString();
            objEmrPatient.m_strICD10DIAGTEXT_VCHR = p_objRow["ICD10DIAGTEXT_VCHR"].ToString();
            objEmrPatient.m_strEXTENDID_VCHR = p_objRow["HISINPATIENTID_CHR"].ToString();
            objEmrPatient.m_strEMRInPatientID = p_objRow["EMRINPATIENTID"].ToString();
            objEmrPatient.m_strHISInPatientID = p_objRow["HISINPATIENTID_CHR"].ToString();
            if (DateTime.TryParse(p_objRow["EMRINPATIENTDATE"].ToString(), out dtmTemp))
                objEmrPatient.m_dtmEMRInDate = dtmTemp;
            if (DateTime.TryParse(p_objRow["HISINPATIENTDATE"].ToString(), out dtmTemp))
                objEmrPatient.m_dtmHISInDate = dtmTemp;
            objEmrPatient.m_strPAYTYPEName_CHR = p_objRow["PAYTYPENAME_VCHR"].ToString();
            objEmrPatient.m_strDeptName = p_objRow["deptname"].ToString();
            objEmrPatient.m_strAreaName = p_objRow["areaname"].ToString();
            objEmrPatient.m_strCODE_CHR = p_objRow["bedcode"].ToString();
            string strAge = string.Empty;
            string strHosPitalNO = string.Empty;
            long lngRes = m_lngGetCurrentHospitalNO(out strHosPitalNO);//��ȡ��ǰҽԺ����
            objEmrPatient.m_strAGESHORT_CHR = m_strCalAge(objEmrPatient.m_strBIRTH_DAT, objEmrPatient.m_dtmHISInDate, strHosPitalNO, out strAge);
            objEmrPatient.m_strAGELONG_CHR = strAge;

            return objEmrPatient;
        }

        /// <summary>
        /// ҽԺ����
        /// </summary>
        /// <param name="p_strHospitalNO">ҽԺ����</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetCurrentHospitalNO(out string p_strHospitalNO)
        {
            p_strHospitalNO = string.Empty;
            long lngRes = 0;

            try
            {
                string strSQL = "select t.shortno_chr from t_aid_hospitals t where t.usageflag_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_strHospitalNO = dtResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ���ݲ���ID��ȡ���˵�סԺ��С��Ϣ��
        /// �˷�����������û�в��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">����ID</param>
        /// <param name="p_dtbValue">���˵���С��Ϣ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSpecialMinPatinetInfoByDeptID(string p_strID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetSpecialMinPatinetInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //����״̬=5Ϊɾ��״̬ ��Ժ�Ǽ�״̬=1��Ч
                string strSQL = @"select dd.bedid_chr,
                                       dd.areaid_chr,
                                       dd.code_chr,
                                       dd.status_int,
                                       dd.rate_mny,
                                       dd.sex_int,
                                       dd.category_int,
                                       dd.airrate_mny,
                                       dd.chargeitemid_chr,
                                       dd.airchargeflg_int,
                                       dd.airchargeitemid_chr,
                                       dd.extendid_vchr,
                                       dd.bihregisterid_chr,
                                       dd.creatorid_chr,
                                       dd.creat_dat,
                                       dd.cancelerid_chr,
                                       dd.cancel_dat,
                                       dd.bed_no,
                                       dd.registerid_chr,
                                       dd.patientid_chr,
                                       dd.inpatientid_chr,
                                       dd.deptid_chr,
                                       dd.inbedareaid_chr,
                                       dd.inpatient_dat,
                                       dd.inpatientcount_int,
                                       dd.state_int,
                                       dd.pstatus_int,
                                       dd.emrinpatientid,
                                       dd.emrinpatientdate,
                                       dd.hisinpatientid_chr,
                                       dd.hisinpatientdate,
                                       dd.paytypeid_chr,
                                       dd.patientcardid_chr,
                                       dd.diagnose_vchr,
                                       dd.nursing_class,
                                       dd.doctid,
                                       dd.doctname,
                                       p.lastname_vchr,
                                       p.sex_chr,
                                       p.birth_dat,
                                       p.extendid_vchr as patientextendid
                                  from (select t.bedid_chr,
                                               t.areaid_chr,
                                               t.code_chr,
                                               t.status_int,
                                               t.rate_mny,
                                               t.sex_int,
                                               t.category_int,
                                               t.airrate_mny,
                                               t.chargeitemid_chr,
                                               t.airchargeflg_int,
                                               t.airchargeitemid_chr,
                                               t.extendid_vchr,
                                               t.bihregisterid_chr,
                                               t.creatorid_chr,
                                               t.creat_dat,
                                               t.cancelerid_chr,
                                               t.cancel_dat,
                                               t.bed_no,
                                               d.paytypeid_chr,
                                               d.registerid_chr,
                                               d.patientid_chr,
                                               d.inpatientid_chr,
                                               d.deptid_chr,
                                               d.areaid_chr             as inbedareaid_chr,
                                               d.inpatient_dat,
                                               d.inpatientcount_int,
                                               d.state_int,
                                               d.pstatus_int,
                                               d.diagnose_vchr,
                                               d.nursing_class,
                                               d.casedoctor_chr         as doctid,
                                               e.lastname_vchr          as doctname,
                                               rehis.emrinpatientid,
                                               rehis.emrinpatientdate,
                                               rehis.hisinpatientid_chr,
                                               rehis.hisinpatientdate,
                                               c.patientcardid_chr
                                          from t_opr_bih_register d
                                         inner join t_bse_hisemr_relation rehis
                                            on d.registerid_chr = rehis.registerid_chr
                                          left outer join t_bse_bed t
                                            on t.areaid_chr = d.deptid_chr
                                           and t.bedid_chr = d.bedid_chr
                                           and d.pstatus_int <> 3
                                          left join t_bse_patientcard c
                                            on d.patientid_chr = c.patientid_chr
                                          left join t_bse_employee e
                                            on d.casedoctor_chr = e.empid_chr
                                         where d.status_int = 1
                                           and t.status_int <> 5
                                           and d.patientid_chr = ?) dd
                                  left outer join t_bse_patient p
                                    on dd.patientid_chr = p.patientid_chr
                                 order by dd.bed_no
                                ";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //��ֵ
                objDPArr[0].Value = 3;
                objDPArr[1].Value = 5;
                objDPArr[2].DbType = DbType.StringFixedLength;
                objDPArr[2].Value = p_strID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
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
                //objHrpServ.Dispose();
            }
            return lngRes;

        }
        /// <summary>
        /// ���ݲ���ID��ȡ���˵�סԺ��С��Ϣ��
        /// �˷�����������û�в��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">����ID</param>
        /// <param name="p_dtbValue">���˵���С��Ϣ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMinPatinetInfoByIdAndDate(string p_strInpatientID, DateTime p_dtmInpatientDate, out DataTable p_dtbValue, bool p_blnNoArea)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetSpecialMinPatinetInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //����״̬=5Ϊɾ��״̬ ��Ժ�Ǽ�״̬=1��Ч
                string str = "";
                if (p_blnNoArea)
                    str = "deptid_chr ";
                else
                    str = "areaid_chr ";
                string strSQL = @"select dd.bedid_chr,
       dd.areaid_chr,
       dd.code_chr,
       dd.status_int,
       dd.rate_mny,
       dd.sex_int,
       dd.category_int,
       dd.airrate_mny,
       dd.chargeitemid_chr,
       dd.airchargeflg_int,
       dd.airchargeitemid_chr,
       dd.extendid_vchr,
       dd.bihregisterid_chr,
       dd.creatorid_chr,
       dd.creat_dat,
       dd.cancelerid_chr,
       dd.cancel_dat,
       dd.bed_no,
       dd.registerid_chr,
       dd.patientid_chr,
       dd.inpatientid_chr,
       dd.deptid_chr,
       dd.inbedareaid_chr,
       dd.inpatient_dat,
       dd.inpatientcount_int,
       dd.state_int,
       dd.pstatus_int,
       dd.emrinpatientid,
       dd.emrinpatientdate,
       dd.hisinpatientid_chr,
       dd.hisinpatientdate,
       p.lastname_vchr,
       p.sex_chr,
       p.birth_dat,
       p.extendid_vchr as patientextendid
  from (select t.bedid_chr,
               t.areaid_chr,
               t.code_chr,
               t.status_int,
               t.rate_mny,
               t.sex_int,
               t.category_int,
               t.airrate_mny,
               t.chargeitemid_chr,
               t.airchargeflg_int,
               t.airchargeitemid_chr,
               t.extendid_vchr,
               t.bihregisterid_chr,
               t.creatorid_chr,
               t.creat_dat,
               t.cancelerid_chr,
               t.cancel_dat,
               t.bed_no,
               d.registerid_chr,
               d.patientid_chr,
               d.inpatientid_chr,
               d.deptid_chr,
               d.areaid_chr as inbedareaid_chr,
               d.inpatient_dat,
               d.inpatientcount_int,
               d.state_int,
               d.pstatus_int,
               rehis.emrinpatientid,
               rehis.emrinpatientdate,
               rehis.hisinpatientid_chr,
               rehis.hisinpatientdate
          from t_opr_bih_register d
         inner join t_bse_hisemr_relation rehis on d.registerid_chr =
                                                   rehis.registerid_chr
          left outer join t_bse_bed t on t.areaid_chr = d." + str + @"	and t.bedid_chr = d.bedid_chr  
                                                  and t.status_int <> 5 
										where  rehis.emrinpatientid=? and rehis.emrinpatientdate = ? and d.status_int = 1
										) dd left outer join  t_bse_patient p
										on dd.patientid_chr = p.patientid_chr
										order by dd.bed_no";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strInpatientID.Trim();
                objDPArr[1].DbType = DbType.Date;
                objDPArr[1].Value = p_dtmInpatientDate;
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
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
                //objHrpServ.Dispose();
            }
            return lngRes;

        }


        /// <summary>
        /// ���ݲ���ID��ȡ���˵�סԺ��С��Ϣ��
        /// �˷����������в����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">����ID</param>
        /// <param name="p_dtbValue">���˵���С��Ϣ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSpecialMinPatinetInfo(string p_strID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objserv = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetSpecialMinPatinetInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //����״̬=5Ϊɾ��״̬ ��Ժ�Ǽ�״̬=1��Ч
                string strSQL = @"select dd.bedid_chr,
       dd.areaid_chr,
       dd.code_chr,
       dd.status_int,
       dd.rate_mny,
       dd.sex_int,
       dd.category_int,
       dd.airrate_mny,
       dd.chargeitemid_chr,
       dd.airchargeflg_int,
       dd.airchargeitemid_chr,
       dd.extendid_vchr,
       dd.bihregisterid_chr,
       dd.creatorid_chr,
       dd.creat_dat,
       dd.cancelerid_chr,
       dd.cancel_dat,
       dd.bed_no,
       dd.registerid_chr,
       dd.patientid_chr,
       dd.inpatientid_chr,
       dd.deptid_chr,
       dd.inbedareaid_chr,
       dd.inpatient_dat,
       dd.inpatientcount_int,
       dd.state_int,
       dd.pstatus_int,
       dd.emrinpatientid,
       dd.emrinpatientdate,
       dd.hisinpatientid_chr,
       dd.hisinpatientdate,
       p.lastname_vchr,
       p.sex_chr,
       p.birth_dat,
       p.extendid_vchr as patientextendid
  from (select t.bedid_chr,
               t.areaid_chr,
               t.code_chr,
               t.status_int,
               t.rate_mny,
               t.sex_int,
               t.category_int,
               t.airrate_mny,
               t.chargeitemid_chr,
               t.airchargeflg_int,
               t.airchargeitemid_chr,
               t.extendid_vchr,
               t.bihregisterid_chr,
               t.creatorid_chr,
               t.creat_dat,
               t.cancelerid_chr,
               t.cancel_dat,
               t.bed_no,
               d.registerid_chr,
               d.patientid_chr,
               d.inpatientid_chr,
               d.deptid_chr,
               d.areaid_chr as inbedareaid_chr,
               d.inpatient_dat,
               d.inpatientcount_int,
               d.state_int,
               d.pstatus_int,
               rehis.emrinpatientid,
               rehis.emrinpatientdate,
               rehis.hisinpatientid_chr,
               rehis.hisinpatientdate
          from t_opr_bih_register d
         inner join t_bse_hisemr_relation rehis on d.registerid_chr =
                                                   rehis.registerid_chr
          left outer join t_bse_bed t on t.areaid_chr = d.areaid_chr
                                     and t.bedid_chr = d.bedid_chr
                                     and d.pstatus_int <> 3
         where d.status_int = 1
           and t.status_int <> 5
           and d.patientid_chr = '" + p_strID.Trim() + @"'
										) dd left outer join  t_bse_patient p
										on dd.patientid_chr = p.patientid_chr
										order by dd.bed_no";
                DataTable dtResult = new DataTable();
                lngRes = objserv.DoGetDataTable(strSQL, ref dtResult);
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
                //objserv.Dispose();
            }
            return lngRes;

        }
        /// <summary>
        /// ���ݲ�����չID��ȡ���˵�סԺ��С��Ϣ��
        /// �˷�����������û�в��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strExtendID"></param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMinPatinetInfoByDeptAndExtendID(string p_strExtendID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetSpecialMinPatinetInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //����״̬=5Ϊɾ��״̬ ��Ժ�Ǽ�״̬=1��Ч
                string strSQL = @" select t.bedid_chr,
       t.areaid_chr,
       t.code_chr,
       t.status_int,
       t.rate_mny,
       t.sex_int,
       t.category_int,
       t.airrate_mny,
       t.chargeitemid_chr,
       t.airchargeflg_int,
       t.airchargeitemid_chr,
       t.extendid_vchr,
       t.bihregisterid_chr,
       t.creatorid_chr,
       t.creat_dat,
       t.cancelerid_chr,
       t.cancel_dat,
       t.bed_no,
       d.registerid_chr,
       d.patientid_chr,
       d.inpatientid_chr,
       d.deptid_chr,
       d.areaid_chr as inbedareaid_chr,
       d.inpatient_dat,
       d.inpatientcount_int,
       d.state_int,
       d.pstatus_int,
       p.lastname_vchr,
       p.sex_chr,
       p.birth_dat,
       p.extendid_vchr as patientextendid,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate
  from t_bse_patient p
 inner join t_opr_bih_register d on p.patientid_chr = d.patientid_chr
                                and d.status_int = 1
 inner join t_bse_hisemr_relation rehis on d.registerid_chr =
                                           rehis.registerid_chr
  left outer join t_bse_bed t on t.areaid_chr = d.deptid_chr
                             and t.bedid_chr = d.bedid_chr
 where d.pstatus_int <> ?
   and t.status_int <> ?
   and p.extendid_vchr = ?";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //��ֵ
                objDPArr[0].Value = 3;
                objDPArr[1].Value = 5;
                objDPArr[2].DbType = DbType.StringFixedLength;
                objDPArr[2].Value = p_strExtendID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
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
                //objHrpServ.Dispose();
            }
            return lngRes;

        }
        /// <summary>
        /// ���ݲ���ID���²��˵Ļ�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientID">����ID</param>
        /// <param name="p_dtbValue">������ϸ��Ϣ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUptPatientInfoDetail(clsEmrPatient_VO p_objEmrPatient)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(35, out objDPArr);
                objDPArr[0].Value = p_objEmrPatient.m_strLASTNAME_VCHR;
                objDPArr[1].Value = p_objEmrPatient.m_strIDCARD_CHR;
                objDPArr[2].Value = p_objEmrPatient.m_strBIRTHPLACE_VCHR;
                objDPArr[3].Value = p_objEmrPatient.m_strHOMEADDRESS_VCHR;
                objDPArr[4].Value = p_objEmrPatient.m_strSEX_CHR;
                objDPArr[5].Value = p_objEmrPatient.m_strNATIONALITY_VCHR;
                objDPArr[6].Value = p_objEmrPatient.m_strFIRSTNAME_VCHR;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = Convert.ToDateTime(p_objEmrPatient.m_strBIRTH_DAT);
                objDPArr[8].Value = p_objEmrPatient.m_strRACE_VCHR;
                objDPArr[9].Value = p_objEmrPatient.m_strNATIVEPLACE_VCHR;
                objDPArr[10].Value = p_objEmrPatient.m_strOCCUPATION_VCHR;
                objDPArr[11].Value = p_objEmrPatient.m_strNAME_VCHR;
                objDPArr[12].Value = p_objEmrPatient.m_strHOMEPHONE_VCHR;
                objDPArr[13].Value = p_objEmrPatient.m_strOFFICEPHONE_VCHR;
                objDPArr[14].Value = p_objEmrPatient.m_strINSURANCEID_VCHR;
                objDPArr[15].Value = p_objEmrPatient.m_strMOBILE_CHR;
                objDPArr[16].Value = p_objEmrPatient.m_strOFFICEADDRESS_VCHR;
                objDPArr[17].Value = p_objEmrPatient.m_strEMPLOYER_VCHR;
                objDPArr[18].Value = p_objEmrPatient.m_strOFFICEPC_VCHR;
                objDPArr[19].Value = p_objEmrPatient.m_strHOMEPC_CHR;
                objDPArr[20].Value = p_objEmrPatient.m_strEMAIL_VCHR;
                objDPArr[21].Value = p_objEmrPatient.m_strCONTACTPERSONFIRSTNAME_VCHR;
                objDPArr[22].Value = p_objEmrPatient.m_strCONTACTPERSONLASTNAME_VCHR;
                objDPArr[23].Value = p_objEmrPatient.m_strCONTACTPERSONADDRESS_VCHR;
                objDPArr[24].Value = p_objEmrPatient.m_strCONTACTPERSONPHONE_VCHR;
                objDPArr[25].Value = p_objEmrPatient.m_strCONTACTPERSONPC_CHR;
                objDPArr[26].Value = p_objEmrPatient.m_strPATIENTRELATION_VCHR;
                objDPArr[27].DbType = DbType.DateTime;
                objDPArr[27].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[28].Value = p_objEmrPatient.m_intOPTIMES_INT.ToString();
                objDPArr[29].Value = p_objEmrPatient.m_strBLOODTYPE_CHR;
                objDPArr[30].Value = p_objEmrPatient.m_intIFALLERGIC_INT.ToString();
                objDPArr[31].Value = p_objEmrPatient.m_strALLERGICDESC_VCHR;
                objDPArr[32].Value = p_objEmrPatient.m_strDIFFICULTY_VCHR;
                objDPArr[33].Value = p_objEmrPatient.m_strMARRIED_CHR;
                objDPArr[34].Value = p_objEmrPatient.m_strREGISTERID_CHR.Trim();
                string strUptSQL = @"update t_opr_bih_registerdetail
   set lastname_vchr               = ?,
       idcard_chr                  = ?,
       birthplace_vchr             = ?,
       homeaddress_vchr            = ?,
       sex_chr                     = ?,
       nationality_vchr            = ?,
       firstname_vchr              = ?,
       birth_dat                   = ?,
       race_vchr                   = ?,
       nativeplace_vchr            = ?,
       occupation_vchr             = ?,
       name_vchr                   = ?,
       homephone_vchr              = ?,
       officephone_vchr            = ?,
       insuranceid_vchr            = ?,
       mobile_chr                  = ?,
       officeaddress_vchr          = ?,
       employer_vchr               = ?,
       officepc_vchr               = ?,
       homepc_chr                  = ?,
       email_vchr                  = ?,
       contactpersonfirstname_vchr = ?,
       contactpersonlastname_vchr  = ?,
       contactpersonaddress_vchr   = ?,
       contactpersonphone_vchr     = ?,
       contactpersonpc_chr         = ?,
       patientrelation_vchr        = ?,
       modify_dat                  = ?,
       optimes_int                 = ?,
       bloodtype_chr               = ?,
       ifallergic_int              = ?,
       allergicdesc_vchr           = ?,
       difficulty_vchr             = ?,
       married_chr                 = ?
 where registerid_chr = ?";
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strUptSQL, ref lngEff, objDPArr);
                if (lngRes > 0)
                {
                    strUptSQL = @"update t_bse_patient
   set lastname_vchr               = ?,
       idcard_chr                  = ?,
       birthplace_vchr             = ?,
       homeaddress_vchr            = ?,
       sex_chr                     = ?,
       nationality_vchr            = ?,
       firstname_vchr              = ?,
       birth_dat                   = ?,
       race_vchr                   = ?,
       nativeplace_vchr            = ?,
       occupation_vchr             = ?,
       name_vchr                   = ?,
       homephone_vchr              = ?,
       officephone_vchr            = ?,
       insuranceid_vchr            = ?,
       mobile_chr                  = ?,
       officeaddress_vchr          = ?,
       employer_vchr               = ?,
       officepc_vchr               = ?,
       homepc_chr                  = ?,
       email_vchr                  = ?,
       contactpersonfirstname_vchr = ?,
       contactpersonlastname_vchr  = ?,
       contactpersonaddress_vchr   = ?,
       contactpersonphone_vchr     = ?,
       contactpersonpc_chr         = ?,
       patientrelation_vchr        = ?,
       modify_dat                  = ?,
       optimes_int                 = ?,
       bloodtype_chr               = ?,
       ifallergic_int              = ?,
       allergicdesc_vchr           = ?,
       difficulty_vchr             = ?,
       married_chr                 = ?
 where patientid_chr = (select patientid_chr
                          from t_opr_bih_register
                         where registerid_chr = ?)";
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(35, out objDPArr);
                    objDPArr[0].Value = p_objEmrPatient.m_strLASTNAME_VCHR;
                    objDPArr[1].Value = p_objEmrPatient.m_strIDCARD_CHR;
                    objDPArr[2].Value = p_objEmrPatient.m_strBIRTHPLACE_VCHR;
                    objDPArr[3].Value = p_objEmrPatient.m_strHOMEADDRESS_VCHR;
                    objDPArr[4].Value = p_objEmrPatient.m_strSEX_CHR;
                    objDPArr[5].Value = p_objEmrPatient.m_strNATIONALITY_VCHR;
                    objDPArr[6].Value = p_objEmrPatient.m_strFIRSTNAME_VCHR;
                    objDPArr[7].DbType = DbType.DateTime;
                    objDPArr[7].Value = Convert.ToDateTime(p_objEmrPatient.m_strBIRTH_DAT);
                    objDPArr[8].Value = p_objEmrPatient.m_strRACE_VCHR;
                    objDPArr[9].Value = p_objEmrPatient.m_strNATIVEPLACE_VCHR;
                    objDPArr[10].Value = p_objEmrPatient.m_strOCCUPATION_VCHR;
                    objDPArr[11].Value = p_objEmrPatient.m_strNAME_VCHR;
                    objDPArr[12].Value = p_objEmrPatient.m_strHOMEPHONE_VCHR;
                    objDPArr[13].Value = p_objEmrPatient.m_strOFFICEPHONE_VCHR;
                    objDPArr[14].Value = p_objEmrPatient.m_strINSURANCEID_VCHR;
                    objDPArr[15].Value = p_objEmrPatient.m_strMOBILE_CHR;
                    objDPArr[16].Value = p_objEmrPatient.m_strOFFICEADDRESS_VCHR;
                    objDPArr[17].Value = p_objEmrPatient.m_strEMPLOYER_VCHR;
                    objDPArr[18].Value = p_objEmrPatient.m_strOFFICEPC_VCHR;
                    objDPArr[19].Value = p_objEmrPatient.m_strHOMEPC_CHR;
                    objDPArr[20].Value = p_objEmrPatient.m_strEMAIL_VCHR;
                    objDPArr[21].Value = p_objEmrPatient.m_strCONTACTPERSONFIRSTNAME_VCHR;
                    objDPArr[22].Value = p_objEmrPatient.m_strCONTACTPERSONLASTNAME_VCHR;
                    objDPArr[23].Value = p_objEmrPatient.m_strCONTACTPERSONADDRESS_VCHR;
                    objDPArr[24].Value = p_objEmrPatient.m_strCONTACTPERSONPHONE_VCHR;
                    objDPArr[25].Value = p_objEmrPatient.m_strCONTACTPERSONPC_CHR;
                    objDPArr[26].Value = p_objEmrPatient.m_strPATIENTRELATION_VCHR;
                    objDPArr[27].DbType = DbType.DateTime;
                    objDPArr[27].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[28].Value = p_objEmrPatient.m_intOPTIMES_INT.ToString();
                    objDPArr[29].Value = p_objEmrPatient.m_strBLOODTYPE_CHR;
                    objDPArr[30].Value = p_objEmrPatient.m_intIFALLERGIC_INT.ToString();
                    objDPArr[31].Value = p_objEmrPatient.m_strALLERGICDESC_VCHR;
                    objDPArr[32].Value = p_objEmrPatient.m_strDIFFICULTY_VCHR;
                    objDPArr[33].Value = p_objEmrPatient.m_strMARRIED_CHR;
                    objDPArr[34].Value = p_objEmrPatient.m_strREGISTERID_CHR.Trim();
                    lngRes = objHRPServ.lngExecuteParameterSQL(strUptSQL, ref lngEff, objDPArr);
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// ��鲡���Ƿ���������Ժ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_blnIsOut"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckPatientIsOut(string p_strRegisterID, out  bool p_blnIsOut)
        {
            p_blnIsOut = false;
            if (string.IsNullOrEmpty(p_strRegisterID)) return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @" select t.registerid_chr from t_opr_bih_leave t where t.registerid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //��ֵ
                objDPArr[0].Value = p_strRegisterID;
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                    p_blnIsOut = true;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// ����סԺ�Ż�ȡסԺ��Ϣ,�������е�ס��Ժ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatinetByInPatientID(string p_strInPatientID, out clsEmrInBedPatient_VO p_objValue)
        {
            long lngRes = 0;
            p_objValue = null;
            if (string.IsNullOrEmpty(p_strInPatientID)) return -1;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetPatinetByInPatientID");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //����״̬=5Ϊɾ��״̬ ��Ժ�Ǽ�״̬=1��Ч
                string strSQL = @"select t.bed_no,
       t.code_chr,
       dt.lastname_vchr,
       dt.sex_chr,
       dt.birth_dat,
       re.registerid_chr,
       re.patientid_chr,
       hi.emrinpatientid,
       hi.emrinpatientdate,
       hi.hisinpatientid_chr,
       hi.hisinpatientdate,
       re.deptid_chr,
       re.areaid_chr INBEDAREAID_CHR,
       re.inpatientcount_int,
       re.state_int,
       re.bedid_chr,
       re.pstatus_int,
       le.modify_dat outdate,
       de.deptname_vchr,
       de2.deptname_vchr inbedareaname
  from t_opr_bih_registerdetail dt
 inner join t_bse_hisemr_relation hi on dt.registerid_chr =
                                        hi.registerid_chr
 inner join t_opr_bih_register re on dt.registerid_chr = re.registerid_chr
 inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
 inner join t_bse_deptdesc de2 on re.areaid_chr = de2.deptid_chr
  left outer join t_opr_bih_leave le on re.registerid_chr =
                                        le.registerid_chr
                                    and le.status_int = 1
  left outer join t_bse_bed t on t.areaid_chr = re.areaid_chr
                             and t.bedid_chr = re.bedid_chr
                             and t.status_int <> 5
 where re.status_int = 1
   and re.inpatientid_chr = ?
 order by hi.hisinpatientdate desc";
                clsHRPTableService objHrpServ = new clsHRPTableService();
                IDataParameter[] arrParam = null;
                objHrpServ.CreateDatabaseParameter(1, out arrParam);
                arrParam[0].Value = p_strInPatientID;
                DataTable dtResult = new DataTable();
                lngRes = objHrpServ.lngGetDataTableWithParameters(strSQL, ref dtResult, arrParam);
                int intRowCount = dtResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objValue = m_objGetInPatientInfo(dtResult, null);
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            return lngRes;

        }
        /// <summary>
        /// ����ĳ��סԺ�ǼǺŻ�ȡסԺ��Ϣ,�����ذ������е�ס��Ժ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatinetByRegisterID(string p_strRegisterId, out clsEmrInBedPatient_VO p_objValue)
        {
            long lngRes = 0;
            p_objValue = null;
            if (string.IsNullOrEmpty(p_strRegisterId)) return -1;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetPatinetByInPatientID");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //����״̬=5Ϊɾ��״̬ ��Ժ�Ǽ�״̬=1��Ч
                string strSQL = @"select t.bed_no,
       t.code_chr,
       dt.lastname_vchr,
       dt.sex_chr,
       dt.birth_dat,
       re.registerid_chr,
       re.patientid_chr,
       hi.emrinpatientid,
       hi.emrinpatientdate,
       hi.hisinpatientid_chr,
       hi.hisinpatientdate,
       re.deptid_chr,
       re.areaid_chr INBEDAREAID_CHR,
       re.inpatientcount_int,
       re.extendid_vchr,
       re.state_int,
       re.bedid_chr,
       re.pstatus_int,
       le.modify_dat outdate,
       de.deptname_vchr,
       de2.deptname_vchr inbedareaname
  from t_opr_bih_registerdetail dt
 inner join t_bse_hisemr_relation hi on dt.registerid_chr =
                                        hi.registerid_chr
 inner join t_opr_bih_register re on dt.registerid_chr = re.registerid_chr
 inner join t_bse_deptdesc de on de.deptid_chr = re.deptid_chr
 inner join t_bse_deptdesc de2 on re.areaid_chr = de2.deptid_chr
  left outer join t_opr_bih_leave le on re.registerid_chr =
                                        le.registerid_chr
                                    and le.status_int = 1
  left outer join t_bse_bed t on t.areaid_chr = re.areaid_chr
                             and t.bedid_chr = re.bedid_chr
                             and t.status_int <> 5
 where re.status_int = 1
   and re.inpatientid_chr =
       (select inpatientid_chr
          from t_opr_bih_register
         where registerid_chr = ?)
 order by hi.hisinpatientdate desc";
                clsHRPTableService objHrpServ = new clsHRPTableService();
                IDataParameter[] arrParam = null;
                objHrpServ.CreateDatabaseParameter(1, out arrParam);
                arrParam[0].Value = p_strRegisterId;
                DataTable dtResult = new DataTable();
                lngRes = objHrpServ.lngGetDataTableWithParameters(strSQL, ref dtResult, arrParam);
                int intRowCount = dtResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objValue = m_objGetInPatientInfo(dtResult, p_strRegisterId);
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;

        }
        /// <summary>
        /// �������ݱ���VO
        /// </summary>
        /// <param name="p_dtbInPatient"></param>
        /// <param name="p_strRegisterId">��ǰѡ���סԺ��Ϣ��סԺ�ǼǺ�,���Ϊ��������Ĭ��ȡ���һ��סԺ</param>
        /// <returns></returns>
        private clsEmrInBedPatient_VO m_objGetInPatientInfo(DataTable p_dtbInPatient,string p_strRegisterId)
        {
            DataRow objRowSelected = null;
            clsEmrInBedPatient_VO objValue = new clsEmrInBedPatient_VO();

            objValue.m_arlSessionInfo.Clear();
            DataRow objRow = null;
                DateTime dtmTemp = new DateTime(1900, 1, 1);
            for (int i = 0; i < p_dtbInPatient.Rows.Count; i++)
            {
                objRow = p_dtbInPatient.Rows[i];
                clsEmrPatientSessionInfo_VO obj = new clsEmrPatientSessionInfo_VO();
                obj.m_strEMRInpatientId = objRow["emrinpatientid"].ToString();
                dtmTemp = new DateTime(1900, 1, 1);
                if (DateTime.TryParse(objRow["emrinpatientdate"].ToString(), out dtmTemp))
                    obj.m_dtmEMRInpatientDate = dtmTemp;
                else
                    obj.m_dtmEMRInpatientDate = new DateTime(1900, 1, 1);
                obj.m_strHISInpatientId = objRow["hisinpatientid_chr"].ToString();
                dtmTemp = new DateTime(1900, 1, 1);
                if (DateTime.TryParse(objRow["hisinpatientdate"].ToString(), out dtmTemp))
                    obj.m_dtmHISInpatientDate = dtmTemp;
                else
                    obj.m_dtmHISInpatientDate = new DateTime(1900, 1, 1);
                dtmTemp = new DateTime(1900, 1, 1);
                if (DateTime.TryParse(objRow["outdate"].ToString(), out dtmTemp))
                    obj.m_dtmOutDate = dtmTemp;
                else
                    obj.m_dtmOutDate = new DateTime(1900, 1, 1);
                //���ͬ������ϵͳʱȱʧ��Ժ������ֻ����չID��������(��������)
                if (objRow["inpatientcount_int"].ToString() == "0" && objRow["extendid_vchr"].ToString().Contains("_"))
                {
                    int intSplitIndex = objRow["extendid_vchr"].ToString().IndexOf("_");
                    string strInCount = objRow["extendid_vchr"].ToString().Substring(intSplitIndex + 1);
                    obj.m_strInTimes = strInCount;
                }
                else
                {
                    obj.m_strInTimes = objRow["inpatientcount_int"].ToString();
                }                
                obj.m_strRegisterId = objRow["registerid_chr"].ToString().Trim();
                obj.m_strDeptId = objRow["deptid_chr"].ToString();
                obj.m_strDeptName = objRow["deptname_vchr"].ToString();
                obj.m_strAreaId = objRow["INBEDAREAID_CHR"].ToString();
                obj.m_strAreaName = objRow["inbedareaname"].ToString();
                obj.m_strPatientID = objRow["PATIENTID_CHR"].ToString();
                //ѡ��ָ����סԺ��Ϣ
                if (obj.m_strRegisterId == p_strRegisterId)
                {
                    objRowSelected = objRow;
                    objValue.m_intSelectedSessionIndex = i;
                }
                objValue.m_arlSessionInfo.Add(obj);
            }
            if (objRowSelected == null)
            {
                objRowSelected = p_dtbInPatient.Rows[0];
                objValue.m_intSelectedSessionIndex = 0;
            }

            objValue.m_strPATIENTID_CHR = objRowSelected["PATIENTID_CHR"].ToString();
            objValue.m_strINPATIENTID_CHR = objRowSelected["emrinpatientid"].ToString();
            objValue.m_strINPATIENT_DAT = objRowSelected["emrinpatientdate"].ToString();
            objValue.m_strEMRInPatientID = objValue.m_strINPATIENTID_CHR;
            dtmTemp = new DateTime(1900, 1, 1);
            if (DateTime.TryParse(objValue.m_strINPATIENT_DAT, out dtmTemp))
                objValue.m_dtmEMRInDate = dtmTemp;
            else
                objValue.m_dtmEMRInDate = new DateTime(1900, 1, 1);
            objValue.m_strHISInPatientID = objRowSelected["hisinpatientid_chr"].ToString();
            dtmTemp = new DateTime(1900, 1, 1);
            if (DateTime.TryParse(objRowSelected["hisinpatientdate"].ToString(), out dtmTemp))
                objValue.m_dtmHISInDate = dtmTemp;
            else
                objValue.m_dtmHISInDate = new DateTime(1900, 1, 1);
            int intTemp = 3;
            if (int.TryParse(objRowSelected["STATE_INT"].ToString(), out intTemp))
                objValue.m_intSTATE_INT = intTemp;
            else
                objValue.m_intSTATE_INT = 3;
            objValue.m_strLASTNAME_VCHR = objRowSelected["LASTNAME_VCHR"].ToString();
            objValue.m_strSEX_CHR = objRowSelected["SEX_CHR"].ToString();
            objValue.m_strBIRTH_DAT = objRowSelected["birth_dat"].ToString();
            objValue.m_strREGISTERID_CHR = objRowSelected["REGISTERID_CHR"].ToString();
            objValue.m_strDEPTID_CHR = objRowSelected["DEPTID_CHR"].ToString();
            objValue.m_strAREAID_CHR = objRowSelected["INBEDAREAID_CHR"].ToString();
            objValue.m_strCODE_CHR = objRowSelected["CODE_CHR"].ToString();
            objValue.m_strBEDID_CHR = objRowSelected["BEDID_CHR"].ToString();
            intTemp = 0;
            if (int.TryParse(objRowSelected["INPATIENTCOUNT_INT"].ToString(), out intTemp))
                objValue.m_intINPATIENTCOUNT_INT = intTemp;
            else
                objValue.m_intINPATIENTCOUNT_INT = 0;
            intTemp = 0;
            if (int.TryParse(objRowSelected["PSTATUS_INT"].ToString(), out intTemp))
                objValue.m_intPSTATUS_INT = intTemp;
            else
                objValue.m_intPSTATUS_INT = 0;
            return objValue;
        }
        #endregion

        #region Ա��
        /// <summary>
        /// ��ȡԱ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID"></param>
        /// <param name="p_strPSW"></param>
        /// <param name="p_blnIsShortNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmpPSWByID(string p_strID, out string p_strPSW, bool p_blnIsShortNo)
        {
            p_strPSW = "";
            if (p_strID == null || p_strID.Length == 0) return -1;
            long lngRes = 0;
            string strWhere = "where empid_chr = '" + p_strID.Trim().Replace("'", "''") + "' and status_int = 1";
            if (p_blnIsShortNo)
                strWhere = "where empno_chr = '" + p_strID.Trim().Replace("'", "''") + "' and status_int = 1";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select psw_chr from t_bse_employee " + strWhere;

                DataTable dtResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSql, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                    p_strPSW = dtResult.Rows[0][0].ToString();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// ����key�̷���ָ��Ա����Ϣ
        /// ǰ��������Ա������key����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strEmpKey">key</param>
        /// <param name="p_dtbValue">�������ݼ�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmpByKey(string strEmpKey, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //keyΪ�շ���
                if (strEmpKey == null || strEmpKey.Trim().Length == 0)
                    return lngRes;
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetEmpByKey");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @" select t.empid_chr, t.lastname_vchr, t.technicalrank_chr, t.empno_chr
								from t_bse_employee t
								where t.status_int = ?
								and t.digitalsign_dta =?";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //��ֵ
                objDPArr[0].Value = 1;
                objDPArr[1].Value = strEmpKey.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        #region ����ID����ָ��Ա����Ϣ
        /// <summary>
        /// ����ID����ָ��Ա����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strEmpID">ID</param>
        /// <param name="p_dtbValue">�������ݼ�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmpByID(string strEmpID, out DataTable p_dtbValue)
        {
            //��ʼ��
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                //Ȩ��У��
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "Signature_srv", "m_lngGetEmpByKey");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select t.empno_chr,
       t.lastname_vchr,
       t.technicalrank_chr,
       t.pycode_chr,
       t.empid_chr,
       t.psw_chr,
       t.digitalsign_dta,
       t.technicallevel_chr,
       d.deptid_chr
  from t_bse_employee t
  left outer join T_BSE_DEPTEMP d on d.empid_chr = t.empid_chr
                                 and d.default_inpatient_dept_int = 1
 where t.status_int = ?
   and t.empid_chr = ?";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //��ֵ
                objDPArr[0].Value = 1;
                objDPArr[1].Value = strEmpID.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            //����
            return lngRes;
        }

        #endregion

        #region ���ݹ��ŷ���ָ��Ա����Ϣ
        /// <summary>
        /// ���ݹ��ŷ���ָ��Ա����Ϣ(������ְ)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strEmpNO">����</param>
        /// <param name="p_dtbValue">�������ݼ�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmpByNO(string strEmpNO, out DataTable p_dtbValue)
        {
            //��ʼ��
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                //Ȩ��У��
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "Signature_srv", "m_lngGetEmpByNO");
                //objPrivilege.Dispose();
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
 where t.status_int <> ?
   and t.empno_chr = ?
 order by t.isemployee_int desc, t.empid_chr desc";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //��ֵ
                objDPArr[0].Value = -1;
                objDPArr[1].Value = strEmpNO.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            //����
            return lngRes;
        }


        /// <summary>
        /// ���ݹ��ŷ���ָ��Ա����Ϣ(������Ч����ʷ����)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strEmpNO">����</param>
        /// <param name="p_dtbValue">�������ݼ�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmpByNO_IncludeHistory(string strEmpNO, out DataTable p_dtbValue)
        {
            //��ʼ��
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                //Ȩ��У��
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "Signature_srv", "m_lngGetEmpByNO");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select t.empno_chr,
       t.lastname_vchr,
       t.technicalrank_chr,
       t.pycode_chr,
       t.empid_chr,
       t.psw_chr,
       t.digitalsign_dta,
       t.technicallevel_chr,
       t.status_int
  from t_bse_employee t
 where t.status_int <> 0
   and t.empno_chr = ?";
                //����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //��ֵ
                objDPArr[0].Value = strEmpNO.Trim();
                //table
                DataTable dtResult = new DataTable();
                //ִ��
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //�ͷ�

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            //����
            return lngRes;
        }
        #endregion
        #endregion Ա��

    }
}
