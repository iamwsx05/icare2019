using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.PatientSvc;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ��Ժ�Ǽǡ�
    /// ���ߣ� ����
    /// ����ʱ�䣺 2004-09-03
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsBihRegisterSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���캯��
        public clsBihRegisterSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        //T_OPR_BIH_REGISTER(סԺ�Ǽ�)
        #region ����סԺ�Ǽ�
        /// <summary>
        /// ����סԺ�Ǽ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ�� [out ����]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewBihRegister(out string p_strRecordID, clsT_Opr_Bih_Register_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.m_lngGenerateNewID("T_Opr_Bih_Register", "registerid_chr", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            //			string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"INSERT INTO t_opr_bih_register
            (registerid_chr, modify_dat, patientid_chr, isbooking_int,
             inpatientid_chr, inpatient_dat, deptid_chr, areaid_chr,
             bedid_chr, type_int, diagnose_vchr, limitrate_mny,
             inpatientcount_int, state_int, status_int, operatorid_chr,
             pstatus_int, casedoctor_chr, des_vchr, inpatientnotype_int,
             mzdoctor_chr, mzdiagnose_vchr, diagnoseid_chr, icd10diagid_vchr,
             icd10diagtext_vchr, clinicsayprepay, paytypeid_chr
            )
     VALUES (?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?
            )";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(27, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = DateTime.Parse(p_objRecord.m_strMODIFY_DAT);
                objLisAddItemRefArr[2].Value = p_objRecord.m_strPATIENTID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_intISBOOKING_INT;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strINPATIENTID_CHR;
                objLisAddItemRefArr[5].Value = DateTime.Parse(p_objRecord.m_strINPATIENT_DAT);
                objLisAddItemRefArr[6].Value = p_objRecord.m_strDEPTID_CHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strAREAID_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strBEDID_CHR;
                objLisAddItemRefArr[9].Value = p_objRecord.m_intTYPE_INT;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strDIAGNOSE_VCHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_dblLIMITRATE_MNY;
                objLisAddItemRefArr[12].Value = p_objRecord.m_intINPATIENTCOUNT_INT;
                objLisAddItemRefArr[13].Value = p_objRecord.m_intSTATE_INT;
                objLisAddItemRefArr[14].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strOPERATORID_CHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_intPSTATUS_INT;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strCASEDOCTOR_CHR;
                objLisAddItemRefArr[18].Value = p_objRecord.DES_VCHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_intINPATIENTNOTYPE_INT;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strMZDOCTOR_CHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strMZDIAGNOSE_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strDIAGNOSEID_CHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strICD10DIAGID_VCHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strICD10DIAGTEXT_VCHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strCLINICSAYPREPAY;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strPAYTYPEID_CHR;

                //�������Ӽ�¼
                objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);

                #region NewEMR.Itf
                DataTable dt = null;
                string Sql = @"select t.parmvalue_vchr from t_bse_sysparm t where t.parmcode_chr = '6666' and t.status_int = 1";
                objHRPSvc.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int val = 0;
                    int.TryParse(dt.Rows[0][0].ToString(), out val);
                    if (val == 1)
                    {
                        Sql = @"select t.* from t_bse_patient t where t.patientid_chr = ?";
                        objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                        objLisAddItemRefArr[0].Value = p_objRecord.m_strPATIENTID_CHR;
                        objHRPSvc.lngGetDataTableWithParameters(Sql, ref dt, objLisAddItemRefArr);

                        clsHRPTableService emrSvc = new clsHRPTableService();
                        emrSvc.m_mthSetDataBase_Selector(1, 19);
                        System.Data.IDataParameter[] parm = null;

                        Sql = @"delete from t_bse_patient where patientid_vchr = ?";
                        emrSvc.CreateDatabaseParameter(1, out parm);
                        parm[0].Value = p_objRecord.m_strPATIENTID_CHR;
                        emrSvc.lngExecuteParameterSQL(Sql, ref lngRes, parm);

                        Sql = @"insert into t_bse_patient
                                  (patientid_vchr,
                                   patientname_vchr,
                                   sex_vchr,
                                   birthday_dat,
                                   nationality_vchr,
                                   nativeplace_vchr,
                                   birthplace_vchr,
                                   idcard_vchr,
                                   occupation_vchr,
                                   homeaddr_vchr,
                                   hometel_vchr,
                                   contactname_vchr,
                                   contacttel_vchr,
                                   contactaddr_vchr,
                                   contactrelation_vchr,
                                   status_int,
                                   operdate_dat,
                                   householdregaddr_vchr)
                                values
                                  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                        DataRow drPat = dt.Rows[0];
                        int n = -1;
                        parm = null;
                        emrSvc.CreateDatabaseParameter(18, out parm);
                        parm[++n].Value = drPat["patientid_chr"].ToString();
                        parm[++n].Value = drPat["lastname_vchr"].ToString();
                        parm[++n].Value = drPat["sex_chr"].ToString();
                        if (drPat["birth_dat"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Convert.ToDateTime(drPat["birth_dat"].ToString());
                        parm[++n].Value = drPat["nationality_vchr"].ToString();
                        parm[++n].Value = drPat["nativeplace_vchr"].ToString();
                        parm[++n].Value = drPat["birthplace_vchr"].ToString();
                        parm[++n].Value = drPat["idcard_chr"].ToString();
                        parm[++n].Value = drPat["occupation_vchr"].ToString();
                        parm[++n].Value = drPat["homeaddress_vchr"].ToString();
                        parm[++n].Value = drPat["homephone_vchr"].ToString();
                        parm[++n].Value = drPat["contactpersonlastname_vchr"].ToString();
                        parm[++n].Value = drPat["contactpersonphone_vchr"].ToString();
                        parm[++n].Value = drPat["contactpersonaddress_vchr"].ToString();
                        parm[++n].Value = drPat["contactpersonpc_chr"].ToString();
                        parm[++n].Value = 1;
                        parm[++n].Value = DateTime.Parse(p_objRecord.m_strMODIFY_DAT);
                        parm[++n].Value = "";
                        emrSvc.lngExecuteParameterSQL(Sql, ref lngRes, parm);

                        Sql = @"insert into t_ip_register
                                      (registerid_int,
                                       registerdate_dat,
                                       indate_dat,
                                       patientid_vchr,
                                       patientipno_vchr,
                                       iptimes_int,
                                       areaid_int,
                                       deptid_int,
                                       doctid_int,
                                       paytype_vchr,
                                       indiagnosis_vchr,
                                       outdiagnosis_vchr,
                                       outtype_int,
                                       outdate_dat,
                                       inoperid_int,
                                       outoperid_int,
                                       status_int)
                                    values
                                      (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                        n = -1;
                        parm = null;
                        emrSvc.CreateDatabaseParameter(17, out parm);
                        parm[++n].Value = Convert.ToDecimal(p_strRecordID);
                        parm[++n].Value = DateTime.Parse(p_objRecord.m_strMODIFY_DAT);
                        parm[++n].Value = null;
                        parm[++n].Value = p_objRecord.m_strPATIENTID_CHR;
                        parm[++n].Value = p_objRecord.m_strINPATIENTID_CHR;
                        parm[++n].Value = p_objRecord.m_intINPATIENTCOUNT_INT;
                        if (string.IsNullOrEmpty(p_objRecord.m_strAREAID_CHR))
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Convert.ToDecimal(p_objRecord.m_strAREAID_CHR);
                        if (string.IsNullOrEmpty(p_objRecord.m_strDEPTID_CHR))
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Convert.ToDecimal(p_objRecord.m_strDEPTID_CHR);
                        if (string.IsNullOrEmpty(p_objRecord.m_strCASEDOCTOR_CHR))
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Convert.ToDecimal(p_objRecord.m_strCASEDOCTOR_CHR);
                        parm[++n].Value = p_objRecord.m_strPAYTYPEID_CHR;
                        parm[++n].Value = p_objRecord.m_strMZDIAGNOSE_VCHR;
                        parm[++n].Value = null;
                        parm[++n].Value = null;
                        parm[++n].Value = null;
                        if (string.IsNullOrEmpty(p_objRecord.m_strOPERATORID_CHR))
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Convert.ToDecimal(p_objRecord.m_strOPERATORID_CHR);
                        parm[++n].Value = null;
                        parm[++n].Value = 1;
                        emrSvc.lngExecuteParameterSQL(Sql, ref lngRes, parm);
                        emrSvc.Dispose();
                    }
                }
                #endregion
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
        #region ��ѯ
        /// <summary>
        /// ��ѯ���е�סԺ�Ǽǣ���Ч�ģ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllBihRegisterInfo(out clsT_Opr_Bih_Register_VO[] p_objResultArr)
        {
            //{status_int ״̬��-1��ʷ��0��Ч��1��Ч}	
            return m_lngGetBihRegisterInfo(" status_int = 1 ", out p_objResultArr);
        }
        /// <summary>
        /// ����סԺ�� ��ѯסԺ�Ǽǡ�[��Ч�ļ�¼]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientid_chr">סԺ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBinRegisterByInpatientID(string p_strInpatientid_chr, out clsT_Opr_Bih_Register_VO[] p_objResultArr)
        {
            string strQueryCondition = " status_int = '1' AND ";
            strQueryCondition += " Inpatientid_chr = '" + p_strInpatientid_chr.Trim() + "'";
            return m_lngGetBihRegisterInfo(strQueryCondition, out p_objResultArr);
        }

        /// <summary>
        /// ����סԺ��ˮ�� ��ѯסԺ�Ǽǡ�[��Ч�ļ�¼]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">סԺ��ˮ�� </param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBinRegisterByRegisterID(string p_strRegisterid_chr, out clsT_Opr_Bih_Register_VO p_objResult)
        {
            p_objResult = new clsT_Opr_Bih_Register_VO();

            clsT_Opr_Bih_Register_VO[] p_objResultArr;
            string strQueryCondition = " status_int = '1' AND ";
            strQueryCondition += " registerid_chr = '" + p_strRegisterid_chr.Trim() + "'";
            long lngReturn = m_lngGetBihRegisterInfo(strQueryCondition, out p_objResultArr);
            if (lngReturn > 0 && p_objResultArr.Length > 0)
                p_objResult = p_objResultArr[0];
            return lngReturn;
        }

        /// <summary>
        /// ��ȡסԺ�ŵ����һ��[��Ч��]סԺ�Ǽ���ˮ�ţ�û�����ȡΪ�մ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientid_chr">סԺ��</param>
        /// <param name="p_strRegisterid_chr">סԺ�Ǽ���ˮ�� [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRegisteridByInpatientID(string p_strInpatientid_chr, out string p_strRegisterid_chr)
        {
            p_strRegisterid_chr = "";
            long lngRes = 0;
            //��ѯ������  ȷ����ѯSQL����ִ�С�	  	 
            //string strSQL = @"SELECT * FROM T_Opr_Bih_Register WHERE status_int = 1 And Inpatientid_chr = '" + p_strInpatientid_chr + "' ORDER BY modify_dat DESC";
            string strSQL = @"SELECT * FROM T_Opr_Bih_Register WHERE status_int = 1 And Inpatientid_chr = '" + p_strInpatientid_chr + "' ORDER BY inpatient_dat DESC";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_strRegisterid_chr = dtbResult.Rows[0]["registerid_chr"].ToString();
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
        /// <summary>
        /// ��ȡסԺ�ŵ����һ��[��Ч��]סԺ�Ǽ���ˮ�ţ�û�����ȡΪ�մ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientid_chr">סԺ��</param>
        /// <param name="p_strRegisterid_chr">סԺ�Ǽ���ˮ�� [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRegisteridByInpatientID(string p_strInpatientid_chr, out string[] p_strRegisteridArr)
        {
            p_strRegisteridArr = new string[0];
            long lngRes = 0;

            //��ѯ������  ȷ����ѯSQL����ִ�С�
            string strCheckCount = @"SELECT Count(*) as c FROM T_Opr_Bih_Register WHERE status_int = 1 And Inpatientid_chr = '" + p_strInpatientid_chr + "' ORDER BY inpatient_dat";
            string strSQL = @"SELECT * FROM T_Opr_Bih_Register WHERE status_int = 1 And Inpatientid_chr = '" + p_strInpatientid_chr + "' ORDER BY inpatient_dat";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strCheckCount, ref dtbResult);
                if (lngRes > 0 && int.Parse(dtbResult.Rows[0]["c"].ToString()) < 1)
                {
                    return lngRes;
                }
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_strRegisteridArr = new string[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_strRegisteridArr[i] = dtbResult.Rows[i]["registerid_chr"].ToString();
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
        /// <summary>
        /// ����������סԺ�Ǽǲ�ѯ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strQueryCondition">��ѯ����</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihRegisterInfo(string p_strQueryCondition, out clsT_Opr_Bih_Register_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Opr_Bih_Register_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT   a.*, (SELECT deptname_vchr
                 FROM t_bse_deptdesc
                WHERE deptid_chr = a.deptid_chr) deptname,
         (SELECT deptname_vchr
            FROM t_bse_deptdesc
           WHERE deptid_chr = a.areaid_chr) areaname,
         (SELECT code_chr
            FROM t_bse_bed
           WHERE bedid_chr = a.bedid_chr) bedno,
         (SELECT lastname_vchr
            FROM t_bse_employee
           WHERE RTRIM (LTRIM (empid_chr)) =
                                RTRIM (LTRIM (a.operatorid_chr)))
                                                                 operatorname,
         (SELECT lastname_vchr
            FROM t_bse_employee
           WHERE RTRIM (LTRIM (empid_chr)) =
                                  RTRIM (LTRIM (a.casedoctor_chr)))
                                                                   doctorname,
         (SELECT lastname_vchr
            FROM t_bse_employee
           WHERE RTRIM (LTRIM (empid_chr)) =
                                 RTRIM (LTRIM (a.mzdoctor_chr)))
                                                                outdoctorname,
         b.flgname_vchr AS typename, c.flgname_vchr AS pstatusname
    FROM t_opr_bih_register a, t_sys_flg_table b, t_sys_flg_table c
   WHERE a.type_int = b.flg_int
     AND a.pstatus_int = c.flg_int
     AND b.tablename_vchr = 't_opr_bih_register'
     AND b.columnseq_int = 9
     AND c.tablename_vchr = 't_opr_bih_register'
     AND c.columnseq_int = 16
     ";

            /* @update by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */

            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT a.*,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.deptid_chr) DeptName 
	,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.areaid_chr) AreaName 
		,(select code_chr from t_bse_bed where bedid_chr=a.bedid_chr) BedNo 
		,(select LastName_vchr FROM T_BSE_EMPLOYEE WHERE Rtrim(LTRIM(empid_chr)) =Rtrim(LTRIM(a.operatorid_chr))) OPERATORNAME 
		,(select LastName_vchr from T_BSE_EMPLOYEE where Rtrim(LTRIM(empid_chr)) = Rtrim(LTRIM(a.CASEDOCTOR_CHR))) doctorname
		,(select LastName_vchr from T_BSE_EMPLOYEE where Rtrim(LTRIM(empid_chr))= Rtrim(LTRIM(a.MZDOCTOR_CHR))) outdoctorname,
		--,decode(type_int,1,'����',2,'����',3,'��Ժת��',4,'����ת��','') TypeName 
		--,decode(pstatus_int,0,'δ�ϴ�',1,'���ϴ�',2,'Ԥ��Ժ',3,'ʵ�ʳ�Ժ','') PstatusName 
               (CASE type_int when 1 then '����' when 2 then '����' when 3 then '��Ժת��' when 4 then '����ת��' else '' end) TypeName,
	       (CASE pstatus_int when 0 then 'δ�ϴ�' when 1 then '���ϴ�' when 2 then 'Ԥ��Ժ' when 3 then 'ʵ�ʳ�Ժ' else '' end) PstatusName 
	     FROM t_opr_bih_register a ";
            }
            /* <<======================================= */


            if (p_strQueryCondition != string.Empty)
            {
                strSQL += " AND  " + p_strQueryCondition;
            }
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Opr_Bih_Register_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Opr_Bih_Register_VO();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ISBOOKING_INT"] != DBNull.Value)
                        {
                            p_objResultArr[i1].m_intISBOOKING_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISBOOKING_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strINPATIENTID_CHR = dtbResult.Rows[i1]["INPATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINPATIENT_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["INPATIENT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strDEPTID_CHR = dtbResult.Rows[i1]["DEPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEDID_CHR = dtbResult.Rows[i1]["BEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["TYPE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strDIAGNOSE_VCHR = dtbResult.Rows[i1]["DIAGNOSE_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["LIMITRATE_MNY"] != DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblLIMITRATE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["LIMITRATE_MNY"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_intINPATIENTCOUNT_INT = Convert.ToInt16(dtbResult.Rows[i1]["INPATIENTCOUNT_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intSTATE_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        // ��Ժ�������� [���ֶ�]
                        p_objResultArr[i1].m_strDeptName = dtbResult.Rows[i1]["DeptName"].ToString().Trim();
                        // ��Ժ�������� [���ֶ�]
                        if (dtbResult.Rows[i1]["AreaName"] != null)
                        {
                            p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        }
                        // ��Ժ���� [���ֶ�]
                        if (dtbResult.Rows[i1]["BedNo"] != null)
                        {
                            p_objResultArr[i1].m_strBedNo = dtbResult.Rows[i1]["BedNo"].ToString().Trim();
                        }
                        // ���������� [���ֶ�]
                        p_objResultArr[i1].m_strOperatorName = dtbResult.Rows[i1]["OperatorName"].ToString().Trim();
                        // ��Ժ��ʽ����	[���ֶ�]	{1=����;2=����;3=��Ժת��}
                        p_objResultArr[i1].m_strTypeName = dtbResult.Rows[i1]["TypeName"].ToString().Trim();
                        // ״̬��־	[���ֶ�] {0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ}
                        p_objResultArr[i1].m_strPstatusName = dtbResult.Rows[i1]["PstatusName"].ToString().Trim();
                        p_objResultArr[i1].m_strCASEDOCTOR_CHR = dtbResult.Rows[i1]["CASEDOCTOR_CHR"].ToString().Trim();
                        //����ҽ��
                        p_objResultArr[i1].m_strMZDOCTOR_CHR = dtbResult.Rows[i1]["MZDOCTOR_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_stroutdoctorname = dtbResult.Rows[i1]["outdoctorname"].ToString().Trim();
                        //�������
                        p_objResultArr[i1].m_strMZDIAGNOSE_VCHR = dtbResult.Rows[i1]["MZDIAGNOSE_VCHR"].ToString().Trim();

                        #region ��ע ��Ժ���(ICD10)	glzhang	2005.08.10
                        p_objResultArr[i1].m_strDIAGNOSEID_CHR = dtbResult.Rows[i1]["DIAGNOSEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].DES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strICD10DIAGID_VCHR = dtbResult.Rows[i1]["ICD10DIAGID_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strICD10DIAGTEXT_VCHR = dtbResult.Rows[i1]["ICD10DIAGTEXT_VCHR"].ToString().Trim();
                        #endregion
                        //���ｨ��Ԥ����
                        p_objResultArr[i1].m_strCLINICSAYPREPAY = dtbResult.Rows[i1]["CLINICSAYPREPAY"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intINPATIENTNOTYPE_INT = int.Parse(dtbResult.Rows[i1]["INPATIENTNOTYPE_INT"].ToString());
                        }
                        catch
                        {
                            p_objResultArr[i1].m_intINPATIENTNOTYPE_INT = -1;
                        }
                        p_objResultArr[i1].m_strdoctname = dtbResult.Rows[i1]["doctorname"].ToString().Trim();

                        p_objResultArr[i1].m_strOutDiagnose = dtbResult.Rows[i1]["OUTDIAGNOSE_VCHR"].ToString().Trim();

                        p_objResultArr[i1].m_intDiseaseType = int.Parse(dtbResult.Rows[i1]["diseasetype_int"].ToString());
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

        #region	��д����"����id"��������Ժ��¼�ķ����ӿ�	glzhang	2005.08.2
        /// <summary>
        /// ��д����"����id"��������Ժ��¼�ķ����ӿ�	glzhang	2005.08.2
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatienID">����id</param>
        /// <param name="p_DtbResult">�����:"���˵ǼǺ�id","��Ժʱ��"</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBeInHospitalInfo(string p_strPatienID, out DataTable p_DtbResult)
        {
            p_DtbResult = null;
            long lngRes = 0;
            string strSQL = @"SELECT REGISTERID_CHR,INPATIENT_DAT
							FROM t_opr_bih_register t1
							WHERE t1.patientid_chr = '" + p_strPatienID + "' " +
                            "order by INPATIENT_DAT";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_DtbResult);
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

        #region ͨ��registerid�õ�������Ϣ
        /// <summary>
        /// ͨ��registerid�õ�������Ϣ
        /// </summary>
        /// <param name="registerid"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfobyRegisterID(string registerid, ref DataTable dtResult)
        {
            long lngRes = 0;
            string strSQL = @"select a.lastname_vchr, a.sex_chr, a.birth_dat, b.deptname_vchr, c.code_chr,
                                   d.inpatientid_chr
                              from t_opr_bih_registerdetail a,
                                   t_bse_deptdesc b,
                                   t_bse_bed c,
                                   t_opr_bih_register d
                             where d.registerid_chr = a.registerid_chr
                               and d.areaid_chr = b.deptid_chr(+)
                               and d.bedid_chr = c.bedid_chr
                               and d.registerid_chr = ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = registerid;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
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
        #endregion
        #region �޸�
        #region  �޸ġ����޸�סԺ�Ǽǵļ�¼״̬��-1��ʷ��0��Ч��1��Ч��
        /// <summary>
        /// �޸�סԺ�Ǽǵļ�¼״̬��-1��ʷ��0��Ч��1��Ч��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">סԺ��</param>
        /// <param name="p_intStatus_int">״̬��-1��ʷ��0��Ч��1��Ч��</param>
        /// <param name="p_strOPERATORID_CHR">������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBihRegisterByRegisterID(string p_strRegisterid_chr, int p_intStatus_int, string p_strOPERATORID_CHR)
        {
            long lngRes = 0;
            string strSQLUpdate = "UPDATE  T_Opr_Bih_Register SET ";
            strSQLUpdate += " Status_int =" + p_intStatus_int.ToString();
            strSQLUpdate += " ,OPERATORID_CHR ='" + p_strOPERATORID_CHR.Trim() + "' ";
            strSQLUpdate += " ,MODIFY_DAT = SYSDATE ";
            strSQLUpdate += " WHERE registerid_chr = '" + p_strRegisterid_chr.Trim() + "'";

            /* @update by wjqin (05-11-28)
                         * ���SQL SERVER��strSQl�汾����
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQLUpdate = "UPDATE  T_Opr_Bih_Register SET ";
                strSQLUpdate += " Status_int =" + p_intStatus_int.ToString();
                strSQLUpdate += " ,OPERATORID_CHR ='" + p_strOPERATORID_CHR.Trim() + "' ";
                strSQLUpdate += " ,MODIFY_DAT = GETDATE() ";
                strSQLUpdate += " WHERE registerid_chr = '" + p_strRegisterid_chr.Trim() + "'";

            }
            /* <<======================================= */

            return m_lngModifyBihRegisterInfo(strSQLUpdate);
        }
        #endregion
        #region  �޸���Ժ״̬{0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ}
        /// <summary>
        /// �޸�סԺ�Ǽǵ�״̬{0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">סԺ��</param>
        /// <param name="p_intPSTATUS_INT">״̬{0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ}</param>
        /// <param name="p_strOPERATORID_CHR">������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBihRegisterPSTATUS_INTByRegisterID(string p_strRegisterid_chr, int p_intPSTATUS_INT, string p_strOPERATORID_CHR, string ModifyDate)
        {
            long lngRes = 0;
            string strSQLUpdate = "UPDATE  T_Opr_Bih_Register SET ";
            strSQLUpdate += " PSTATUS_INT =" + p_intPSTATUS_INT.ToString();
            strSQLUpdate += " ,OPERATORID_CHR ='" + p_strOPERATORID_CHR.Trim() + "' ";
            strSQLUpdate += " WHERE registerid_chr = '" + p_strRegisterid_chr.Trim() + "'";

            return m_lngModifyBihRegisterInfo(strSQLUpdate);
        }
        #endregion
        #region �޸ġ�����Ժ���ҡ���Ժ�����������š���Ժ״̬
        /// <summary>
        /// �޸ġ�����Ժ���ҡ���Ժ�����������š���Ժ״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">��ˮ��</param>
        /// <param name="p_strDEPTID_CHR">��Ժ����</param>
        /// <param name="p_strAREAID_CHR">��Ժ����</param>
        /// <param name="p_strBEDID_CHR">����</param>
        /// <param name="intPSTATUS_INT">{0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ}</param>
        /// <param name="p_strOPERATORID_CHR">������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBedInfoByRegisterID(string p_strRegisterid_chr, string p_strDEPTID_CHR, string p_strAREAID_CHR, string p_strBEDID_CHR, int intPSTATUS_INT, string p_strOPERATORID_CHR, string p_strModifyDate)
        {
            long lngRes = 0;
            string strSQLUpdate = "UPDATE  T_Opr_Bih_Register SET ";
            strSQLUpdate += "  DEPTID_CHR ='" + p_strDEPTID_CHR + "' ";
            strSQLUpdate += " ,AREAID_CHR ='" + p_strAREAID_CHR + "' ";
            strSQLUpdate += " ,BEDID_CHR ='" + p_strBEDID_CHR + "' ";
            strSQLUpdate += " ,PSTATUS_INT =" + intPSTATUS_INT + " ";
            strSQLUpdate += " ,OPERATORID_CHR ='" + p_strOPERATORID_CHR + "' ";
            strSQLUpdate += " ,MODIFY_DAT = TO_DATE('" + p_strModifyDate + "','YYYY-MM-DD HH24:MI:SS') ";
            strSQLUpdate += " WHERE registerid_chr = '" + p_strRegisterid_chr + "'";

            /* @update by wjqin (05-11-28)
                         * ���SQL SERVER��strSQl�汾����
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQLUpdate = "UPDATE  T_Opr_Bih_Register SET ";
                strSQLUpdate += "  DEPTID_CHR ='" + p_strDEPTID_CHR + "' ";
                strSQLUpdate += " ,AREAID_CHR ='" + p_strAREAID_CHR + "' ";
                strSQLUpdate += " ,BEDID_CHR ='" + p_strBEDID_CHR + "' ";
                strSQLUpdate += " ,PSTATUS_INT =" + intPSTATUS_INT + " ";
                strSQLUpdate += " ,OPERATORID_CHR ='" + p_strOPERATORID_CHR + "' ";
                strSQLUpdate += " ,MODIFY_DAT = CONVERT(DATETIME,'" + p_strModifyDate + "') ";
                strSQLUpdate += " WHERE registerid_chr = '" + p_strRegisterid_chr + "'";
            }
            /* <<======================================= */

            return m_lngModifyBihRegisterInfo(strSQLUpdate);
        }
        #endregion
        #region �޸ġ�����Ժ���ҡ���Ժ�����������š���Ժ״̬
        /// <summary>
        /// �޸ġ�����Ժ���ҡ���Ժ�����������š���Ժ״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">��ˮ��</param>
        /// <param name="p_strDEPTID_CHR">��Ժ����</param>
        /// <param name="p_strAREAID_CHR">��Ժ����</param>
        /// <param name="p_strBEDID_CHR">����</param>
        /// <param name="intPSTATUS_INT">{0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ}</param>
        /// <param name="p_strOPERATORID_CHR">������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBedInfoByRegisterID(string p_strRegisterid_chr, string p_strDEPTID_CHR, string p_strAREAID_CHR, string p_strBEDID_CHR, int intPSTATUS_INT, string p_strOPERATORID_CHR, string p_strModifyDate, string p_strCASEDOCTOR_CHR)
        {
            long lngRes = 0;
            string strSQLUpdate = "UPDATE  T_Opr_Bih_Register SET ";
            strSQLUpdate += "  DEPTID_CHR ='" + p_strDEPTID_CHR + "' ";
            strSQLUpdate += " ,AREAID_CHR ='" + p_strAREAID_CHR + "' ";
            strSQLUpdate += " ,BEDID_CHR ='" + p_strBEDID_CHR + "' ";
            strSQLUpdate += " ,PSTATUS_INT =" + intPSTATUS_INT + " ";
            strSQLUpdate += " ,OPERATORID_CHR ='" + p_strOPERATORID_CHR + "' ";
            strSQLUpdate += " ,MODIFY_DAT = TO_DATE('" + p_strModifyDate + "','YYYY-MM-DD HH24:MI:SS') ";
            strSQLUpdate += " ,CASEDOCTOR_CHR ='" + p_strCASEDOCTOR_CHR + "' ";
            strSQLUpdate += " WHERE registerid_chr = '" + p_strRegisterid_chr + "'";

            return m_lngModifyBihRegisterInfo(strSQLUpdate);
        }
        #endregion
        #region �޸�סԺ�Ǽ���Ϣ add by wjqin(06-06-21)
        /// <summary>
        /// �޸�סԺ�Ǽ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">��ˮ��</param>
        /// <param name="objBIHVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBihRegisterInfoByVo(string p_strRegisterid_chr, clsT_Opr_Bih_Register_VO objPatientVO)
        {
            long lngRes = 0;
            string strSQLUpdate = "UPDATE  T_OPR_BIH_REGISTER SET ";
            strSQLUpdate += "   PATIENTID_CHR = '" + objPatientVO.m_strPATIENTID_CHR.Trim() + "' ";
            strSQLUpdate += " , ISBOOKING_INT = " + objPatientVO.m_intISBOOKING_INT.ToString() + " ";
            strSQLUpdate += " , INPATIENTID_CHR = '" + objPatientVO.m_strINPATIENTID_CHR.Trim() + "' ";
            //strSQLUpdate += " , DEPTID_CHR = '" + objPatientVO.m_strDEPTID_CHR.Trim()  + "' ";
            strSQLUpdate += "   ,PAYTYPEID_CHR='" + objPatientVO.m_strPAYTYPEID_CHR + "'";
            strSQLUpdate += " , AREAID_CHR = '" + objPatientVO.m_strAREAID_CHR.Trim() + "' ";
            strSQLUpdate += " , BEDID_CHR = '" + objPatientVO.m_strBEDID_CHR.Trim() + "' ";
            strSQLUpdate += " , CASEDOCTOR_CHR = '" + objPatientVO.m_strCASEDOCTOR_CHR.Trim() + "' ";
            strSQLUpdate += " , TYPE_INT = '" + objPatientVO.m_intTYPE_INT.ToString() + "' ";
            strSQLUpdate += " , DIAGNOSE_VCHR = '" + objPatientVO.m_strDIAGNOSE_VCHR.Trim() + "' ";
            strSQLUpdate += " , LIMITRATE_MNY = " + objPatientVO.m_dblLIMITRATE_MNY.ToString() + " ";
            strSQLUpdate += " , INPATIENTCOUNT_INT = " + objPatientVO.m_intINPATIENTCOUNT_INT.ToString() + " ";
            strSQLUpdate += " , STATE_INT = " + objPatientVO.m_intSTATE_INT.ToString() + " ";
            strSQLUpdate += " , STATUS_INT = " + objPatientVO.m_intSTATUS_INT.ToString() + " ";
            strSQLUpdate += " , PSTATUS_INT = " + objPatientVO.m_intPSTATUS_INT.ToString() + " ";
            strSQLUpdate += " , OPERATORID_CHR = '" + objPatientVO.m_strOPERATORID_CHR.Trim() + "' ";
            strSQLUpdate += " , INPATIENTNOTYPE_INT = " + objPatientVO.m_intINPATIENTNOTYPE_INT + "";
            strSQLUpdate += " , DES_VCHR = '" + objPatientVO.DES_VCHR.Trim() + "' ";
            strSQLUpdate += " , MZDOCTOR_CHR = '" + objPatientVO.m_strMZDOCTOR_CHR.Trim() + "' ";
            strSQLUpdate += " , MZDIAGNOSE_VCHR = '" + objPatientVO.m_strMZDIAGNOSE_VCHR.Trim() + "' ";
            strSQLUpdate += " , DIAGNOSEID_CHR = '" + objPatientVO.m_strDIAGNOSEID_CHR.Trim() + "' ";
            strSQLUpdate += " , ICD10DIAGID_VCHR = '" + objPatientVO.m_strICD10DIAGID_VCHR.Trim() + "' ";
            strSQLUpdate += " , ICD10DIAGTEXT_VCHR = '" + objPatientVO.m_strICD10DIAGTEXT_VCHR.Trim() + "' ";

            if (objPatientVO.m_strCLINICSAYPREPAY != null && objPatientVO.m_strCLINICSAYPREPAY != "")
            {
                strSQLUpdate += " , CLINICSAYPREPAY = '" + objPatientVO.m_strCLINICSAYPREPAY.Trim() + "' ";
            }
            //strSQLUpdate += " , MODIFY_DAT = SYSDATE ";
            strSQLUpdate += " , INPATIENT_DAT = TO_DATE('" + objPatientVO.m_strINPATIENT_DAT.Trim() + "', 'YYYY-MM-DD HH24:MI:SS') ";
            strSQLUpdate += "  WHERE REGISTERID_CHR = '" + p_strRegisterid_chr.Trim() + "'";

            clsRecordMark_VO Markvo = new clsRecordMark_VO();
            //clsRecordMark recordMark = new clsRecordMark();
            Markvo.m_strOPERATORID_CHR = objPatientVO.m_strOPERATORID_CHR;
            Markvo.m_strTABLESEQID_CHR = "1";
            Markvo.m_strRECORDDETAIL_VCHR = strSQLUpdate;
            //recordMark.m_mthAddNewRecord(Markvo);
            return m_lngModifyBihRegisterInfo(strSQLUpdate);
        }
        #endregion
        #region �޸�סԺ�Ǽ���Ϣ
        /// <summary>
        /// �޸�סԺ�Ǽ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">��ˮ��</param>
        /// <param name="objBIHVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBihRegisterInfoByVo(clsT_Opr_Bih_Register_VO objPatientVO)
        {
            long lngRes = 0;
            string strSQLUpdate = "UPDATE  T_OPR_BIH_REGISTER SET ";
            strSQLUpdate += "  DEPTID_CHR = '" + objPatientVO.m_strDEPTID_CHR.Trim() + "' ";
            strSQLUpdate += " , AREAID_CHR = '" + objPatientVO.m_strAREAID_CHR.Trim() + "' ";
            strSQLUpdate += " , BEDID_CHR = '" + objPatientVO.m_strBEDID_CHR.Trim() + "' ";
            strSQLUpdate += " , PSTATUS_INT = " + objPatientVO.m_intPSTATUS_INT.ToString() + " ";
            strSQLUpdate += " , MODIFY_DAT = to_date('" + objPatientVO.m_strMODIFY_DAT + "','YYYY-MM-DD HH24:MI:SS') ";
            strSQLUpdate += "  WHERE REGISTERID_CHR = '" + objPatientVO.m_strREGISTERID_CHR.Trim() + "'";

            return m_lngModifyBihRegisterInfo(strSQLUpdate);
        }
        #endregion

        #region �޸�סԺ��Ϣ(�ڴ�λ�༭ʱ�õ�)	glzhang	2005.08.19
        /// <summary>
        /// �޸�סԺ��Ϣ(�ڴ�λ�༭ʱ�õ�)	glzhang	2005.08.19
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">��ˮ��</param>
        /// <param name="objBIHVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBedInfoByVo(clsT_Opr_Bih_Register_VO objPatientVO)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQLUpdate = "UPDATE  T_OPR_BIH_REGISTER SET ";
            strSQLUpdate += " CASEDOCTOR_CHR = ?";
            strSQLUpdate += " , DIAGNOSEID_CHR = ? ";
            strSQLUpdate += " , DIAGNOSE_VCHR = ? ";
            strSQLUpdate += " , ICD10DIAGID_VCHR = ? ";
            strSQLUpdate += " , ICD10DIAGTEXT_VCHR = ? ";
            strSQLUpdate += " , MODIFY_DAT = ? ";
            strSQLUpdate += "  WHERE REGISTERID_CHR = ?";
            //�޸ĺۼ�����
            clsRecordMark_VO Markvo = new clsRecordMark_VO();
            //clsRecordMark recordMark = new clsRecordMark();

            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(7, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = objPatientVO.m_strCASEDOCTOR_CHR;
                objLisAddItemRefArr[1].Value = objPatientVO.m_strDIAGNOSEID_CHR;
                objLisAddItemRefArr[2].Value = objPatientVO.m_strDIAGNOSE_VCHR;
                objLisAddItemRefArr[3].Value = objPatientVO.m_strICD10DIAGID_VCHR;
                objLisAddItemRefArr[4].Value = objPatientVO.m_strICD10DIAGTEXT_VCHR;
                objLisAddItemRefArr[5].Value = Convert.ToDateTime(objPatientVO.m_strMODIFY_DAT);
                objLisAddItemRefArr[6].Value = objPatientVO.m_strREGISTERID_CHR;
                long lngRecEff = -1;
                //�޸ļ�¼

                #region �޸ĺۼ�����
                string strSQLUpdate_Temp = "UPDATE  T_OPR_BIH_REGISTER SET ";
                strSQLUpdate_Temp += " CASEDOCTOR_CHR = '" + objPatientVO.m_strCASEDOCTOR_CHR + "'";
                strSQLUpdate_Temp += " , DIAGNOSEID_CHR = '" + objPatientVO.m_strDIAGNOSEID_CHR + "' ";
                strSQLUpdate_Temp += " , DIAGNOSE_VCHR = '" + objPatientVO.m_strDIAGNOSE_VCHR + "' ";
                strSQLUpdate_Temp += " , ICD10DIAGID_VCHR = '" + objPatientVO.m_strICD10DIAGID_VCHR + "' ";
                strSQLUpdate_Temp += " , ICD10DIAGTEXT_VCHR = '" + objPatientVO.m_strICD10DIAGTEXT_VCHR + "' ";
                strSQLUpdate_Temp += " , MODIFY_DAT = TO_DATE ('" + objPatientVO.m_strMODIFY_DAT + "', 'yyyy-mm-dd hh24:mi:ss') ";
                strSQLUpdate_Temp += "  WHERE REGISTERID_CHR = '" + objPatientVO.m_strREGISTERID_CHR + "'";
                Markvo.m_strOPERATORID_CHR = objPatientVO.m_strOPERATORID_CHR;
                Markvo.m_strTABLESEQID_CHR = "1";
                Markvo.m_strRECORDDETAIL_VCHR = strSQLUpdate_Temp;
                //recordMark.m_mthAddNewRecord(Markvo);
                #endregion

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQLUpdate, ref lngRecEff, objLisAddItemRefArr);
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

        #region ��SQL�����޸�סԺ�Ǽ���Ϣ
        /// <summary>
        /// ��SQL�����޸�סԺ�Ǽ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSQLUpdate">Update��SQL���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBihRegisterInfo(string p_strSQLUpdate)
        {
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(p_strSQLUpdate);
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
        #endregion
        #region ��֤
        #region ��֤סԺ���Ƿ����
        /// <summary>
        /// ��֤סԺ���Ƿ����[��Ч]�ǼǼ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientid_chr">סԺ��</param>
        /// <param name="IsRegisterd">�Ƿ�Ǽ� [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckIsRegisterdByInpatientID(string p_strInpatientid_chr, out bool IsRegisterd)
        {
            IsRegisterd = false;
            string strRegisterid_chr = "";
            long lngReturn = m_lngGetRegisteridByInpatientID(p_strInpatientid_chr, out strRegisterid_chr);
            if (lngReturn > 0 && strRegisterid_chr != string.Empty)
            {
                IsRegisterd = true;
            }
            return lngReturn;
        }
        #endregion
        #endregion
        #region ����סԺ��
        /// <summary>
        /// ����סԺ�� {ԭ������4λ��+2λ��+2λ��+4λ���� ����: 200409060000}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientid_chr">סԺ�� [out ����]</param>
        /// <returns></returns>
        /// <remarks>
        /// ע��:
        ///		ԭ������2λ��+2λ��+2λ��+4λ���� 
        ///		����:0409060000
        ///		���һ�쳬��10000����Ժ�򷵻ؿ�.
        /// </remarks>
        [AutoComplete]
        public long m_lngGetInpatientID(out string p_strInpatientid_chr)
        {
            p_strInpatientid_chr = "";

            long lngRes = 0;

            //��ѯ���������סԺ��
            string strInpatientid_chr = "";
            string strSQL = @"
					SELECT count(*) as c,max(TO_NUMBER (t_opr_bih_register.inpatientid_chr)) +1 as Inpatientid_chr
					FROM t_opr_bih_register
					WHERE (status_int = 1 OR status_int = 2) and length(trim(inpatientid_chr)) = 8";


            /* @update by wjqin (05-11-24)
             * ���SQL SERVER��strSQl�汾����
             */

            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                clsHRPTableService HRS = new clsHRPTableService();
                int newid = 0;
                long lngRes2 = HRS.m_lngGenerateNewID("t_opr_bih_register", "inpatientid_chr", out newid);
                if (lngRes2 > 0)
                {
                    strSQL = @"SELECT count(*) as c," + newid + @" as Inpatientid_chr
					FROM t_opr_bih_register
					WHERE (status_int = 1 OR status_int = 2) and len(Ltrim(Rtrim(inpatientid_chr))) = 8";
                }
            }
            /* <<======================================= */


            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && Convert.ToInt32(dtbResult.Rows[0]["c"].ToString()) > 0)
                {
                    strInpatientid_chr = dtbResult.Rows[0]["Inpatientid_chr"].ToString().PadLeft(8, '0');
                    strInpatientid_chr = strInpatientid_chr;
                }
                else
                {
                    strInpatientid_chr = "00000001";
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            p_strInpatientid_chr = strInpatientid_chr;
            return lngRes;
        }
        /// <summary>
        /// ����סԺ��ĩβ4λ����,ʧ�ܷ��ؿ�.
        /// </summary>
        /// <param name="strEnd4"></param>
        /// <returns></returns>
        [AutoComplete]
        public string GetEnd4ForInpatientID(string strEnd4)
        {
            int intTem = 0;
            intTem = Int32.Parse(strEnd4) + 1;
            if (intTem >= 10000)
            {
                return "";
            }
            string strTem = intTem.ToString().Trim();
            while (strTem.Length < 4)
            {
                strTem = "0" + strTem;
            }
            return strTem;
        }
        #endregion
        #region ��֤סԺ���Ƿ����
        [AutoComplete]
        public bool IsExistInptientID(string InpatientID)
        {
            bool IsExist = false;
            string strSQL = "select Count(*) as c from t_opr_bih_register where Inpatientid_chr = '" + InpatientID + "'";
            try
            {
                long lngRes = 0;
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && Convert.ToInt32(dtbResult.Rows[0]["c"].ToString()) > 0)
                {
                    IsExist = true;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return IsExist;
        }
        #endregion

        #region	����סԺ�Ż�ȡ�����б�	glzhang	2005.08.5
        /// <summary>
        /// ����סԺ�Ż�ȡ�����б�	glzhang	2005.08.5
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFind">��ѯ�ַ���</param>
        /// <param name="p_dtbResult">�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInHosPatienList(string p_strFind, out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT DISTINCT TRIM (t1.inpatientid_chr) AS inpatientid_chr,
                t2.lastname_vchr,t2.SEX_CHR,t2.BIRTHPLACE_VCHR
           FROM t_opr_bih_register t1, t_bse_patient t2
          WHERE t1.patientid_chr = t2.patientid_chr(+)
            AND t1.status_int = 1
            AND (   t1.inpatientid_chr LIKE '" + p_strFind + @"%'
                 OR t2.lastname_vchr LIKE '" + p_strFind + @"%'
                )
       ORDER BY TRIM (t1.inpatientid_chr), inpatientid_chr";


            /* @update by wjqin (05-11-24)
             * ���SQL SERVER��strSQl�汾���
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT DISTINCT LTRIM(RTRIM (t1.inpatientid_chr)) AS inpatientid_chr,
                t2.lastname_vchr,,t2.SEX_CHR,t2.BIRTHPLACE_VCHR
           FROM t_opr_bih_register t1 LEFT JOIN t_bse_patient t2 ON t1.patientid_chr = t2.patientid_chr 
            WHERE t1.status_int = 1
            AND (   t1.inpatientid_chr LIKE  '" + p_strFind + @"%'
                 OR t2.lastname_vchr LIKE  '" + p_strFind + @"%'
                )
       ORDER BY LTRIM(RTRIM (t1.inpatientid_chr)) ";
            }
            /* <<======================================= */
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
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

        #region ������Ժ	glzhang	2005.08.30
        /// <summary>
        /// ������Ժ	glzhang	2005.08.30 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">סԺ�Ǽ�ID</param>
        /// <param name="p_strPatitenID">����ID</param>
        /// <param name="p_strInPatitenID">סԺ��</param>
        /// <param name="p_strInDate">��Ժ����</param>
        /// <param name="p_strIntStat">סԺ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyCancelPatient(string p_strRegisterID, string p_strPatitenID, string p_strInPatitenID, string p_strInDate, int p_intStatus)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQLdelete = @"DELETE      t_opr_bih_register 
      WHERE registerid_chr = '" + p_strRegisterID + "'";

            string strSQLdelete2 = @"DELETE      t_opr_bih_transfer 
      WHERE REGISTERID_CHR =  '" + p_strRegisterID + "'";

            string strSQLdelete3 = @"DELETE      inpatientdateinfo 
      WHERE inpatientid = '" + p_strInPatitenID + @"'
        AND inpatientdate =
                       TO_DATE ('" + p_strInDate + "', 'yyyy-mm-dd hh24:mi:ss')";

            /* @update by wjqin (05-11-28)
                                     * ���SQL SERVER��strSQl�汾����
                                     */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQLdelete3 = @"DELETE      inpatientdateinfo 
      WHERE inpatientid = '" + p_strInPatitenID + @"'
        AND CONVERT(DATETIME,inpatientdate) = CONVERT(DATETIME,'" + p_strInDate + "')";

            }
            /* <<======================================= */

            string strSQLdelete4 = @"DELETE      PatientBaseInfo 
      WHERE InPatientID = '" + p_strInPatitenID + "'";

            string strSQLUpdate = @"UPDATE t_bse_patient 
   SET inpatientid_chr = ''
 WHERE patientid_chr = '" + p_strPatitenID + "'";
            //�����ж��Ƿ��Ǵ�����ת��
            string strSQL = @"SELECT t1.registerid_chr
  FROM t_opr_bih_register t1
 WHERE t1.isfromclinic = 1 AND t1.registerid_chr = '" + p_strRegisterID + "'";
            //�޸���Ժ�ǼǱ�
            string strUpRegSQL = @"UPDATE t_opr_bih_register t1
   SET t1.status_int = 2,
       t1.modify_dat = SYSDATE
 WHERE t1.registerid_chr = '" + p_strRegisterID + "'";

            /* @create by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strUpRegSQL = @"UPDATE t_opr_bih_register t1
   SET t1.status_int = 2,
       t1.modify_dat = GETDATE()
 WHERE t1.registerid_chr = '" + p_strRegisterID + "'";
            }
            /* <<======================================= */

            try
            {
                DataTable dtbResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count == 1) //����ת�벡��
                {
                    //�޸Ĳ�����Ϣ��סԺ�ſ�
                    if (p_intStatus == 0)
                    {
                        lngRes = objHRPSvc.DoExcute(strSQLUpdate);
                    }
                    //�޸���Ժ�ǼǱ�
                    lngRes = objHRPSvc.DoExcute(strUpRegSQL);
                    //ɾ����ת��
                    lngRes = objHRPSvc.DoExcute(strSQLdelete2);
                }
                else //������ת�벡��
                {
                    //ɾ����¼
                    lngRes = objHRPSvc.DoExcute(strSQLdelete);
                    lngRes = objHRPSvc.DoExcute(strSQLdelete2);
                    lngRes = objHRPSvc.DoExcute(strSQLdelete3);
                    if (p_intStatus == 0)
                    {
                        lngRes = objHRPSvc.DoExcute(strSQLdelete4);
                        //�޸Ĳ�����Ϣ��סԺ�ſ�
                        lngRes = objHRPSvc.DoExcute(strSQLUpdate);
                    }
                }
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

        #region ����סԺ�Ż�ȡ���˻�����Ϣ�������סԺ��Ϣ glzhang	2005.09.22
        /// <summary>
        /// ����סԺ�Ż�ȡ���˻�����Ϣ�������סԺ��Ϣ glzhang	2005.09.22
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientID">����סԺ��</param>
        /// <param name="objPatientVO">���˻�����Ϣ</param>
        /// <param name="p_objResult">����סԺ��Ϣ</param>
        /// <returns></returns>
        public long m_lngFindPatientInfoByInpatientID(string p_strInpatientID, out clsPatient_VO objPatientVO, out clsT_Opr_Bih_Register_VO p_objResult)
        {
            long lngRes = 0;
            string p_strRegisterid_chr = "";
            objPatientVO = new clsPatient_VO();
            clsPatient_VO[] p_objResultArr;
            clsPatientSvc m_patientSvc = new clsPatientSvc();
            lngRes = m_patientSvc.m_lngGetPatientInfoByInpatientID(p_strInpatientID, out p_objResultArr);
            if (lngRes > 0 && p_objResultArr != null && p_objResultArr.Length > 0)
            {
                objPatientVO = p_objResultArr[0];
            }
            lngRes = m_lngGetRegisteridByInpatientID(p_strInpatientID, out p_strRegisterid_chr);
            lngRes = m_lngGetBinRegisterByRegisterID(p_strRegisterid_chr, out p_objResult);
            return lngRes;
        }
        #endregion

        #region ���ݲ���ID��ȡ����12Сʱ�ڵ�������� glzhang	2005.09.29
        /// <summary>
        /// ���ݲ���ID��ȡ����12Сʱ�ڵ�������� glzhang	2005.09.29
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_strDiag">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindDiagByPatientID(string p_strPatientID, out string p_strDiag)
        {
            p_strDiag = "";
            DataTable p_dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT   t1.casehisid_chr, t1.patientid_chr, t1.diag_vchr
    FROM t_opr_outpatientcasehis t1
   WHERE ROUND ((SYSDATE - modifydate_dat) * 24, 1) <= 12
     AND t1.patientid_chr = '" + p_strPatientID + @"'
ORDER BY t1.casehisid_chr DESC";

            /* @create by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT   t1.casehisid_chr, t1.patientid_chr, t1.diag_vchr
    FROM t_opr_outpatientcasehis t1
  WHERE ROUND (datediff(day,modifydate_dat,getdate()) * 24, 1) <= 12
    AND t1.patientid_chr =  '" + p_strPatientID + @"'
ORDER BY t1.casehisid_chr DESC";
            }
            /* <<======================================= */
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                if (p_dtbResult.Rows.Count > 0)
                {
                    p_strDiag = p_dtbResult.Rows[0]["diag_vchr"].ToString();
                }
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

        #region	��ȡ����ת�벡����Ϣ	glzhang	2005.10.10
        /// <summary>
        /// ��ȡ����ת�벡����Ϣ	glzhang	2005.10.10
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult">�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTurnInPatienList(out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT   t2.patientid_chr, t1.registerid_chr, t6.patientcardid_chr,
         TRIM (t1.inpatientid_chr) AS inpatientid_chr, t1.areaid_chr,
         t3.deptname_vchr, t2.lastname_vchr AS patientname, t2.sex_chr,
         DECODE (t1.state_int, 1, 'Σ', 2, '��', 3, '��ͨ', '') state_int,
         t4.paytypename_vchr, t5.lastname_vchr AS doctername, t1.modify_dat
    FROM t_opr_bih_register t1,
         t_bse_patient t2,
         t_bse_deptdesc t3,
         t_bse_patientpaytype t4,
         t_bse_employee t5,
         t_bse_patientcard t6
   WHERE t1.status_int = 2
     AND t1.patientid_chr = t2.patientid_chr(+)
     AND t1.areaid_chr = t3.deptid_chr(+)
     AND t2.paytypeid_chr = t4.paytypeid_chr(+)
     AND t1.mzdoctor_chr = t5.empid_chr(+)
     AND t2.patientid_chr = t6.patientid_chr(+)
ORDER BY t1.areaid_chr, t1.modify_dat DESC";

            /* @create by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT   t2.patientid_chr, t1.registerid_chr, t6.patientcardid_chr,
         LTRIM(RTRIM (t1.inpatientid_chr)) AS inpatientid_chr, t1.areaid_chr,
         t3.deptname_vchr, t2.lastname_vchr AS patientname, t2.sex_chr,
        -- DECODE (t1.state_int, 1, 'Σ', 2, '��', 3, '��ͨ', '') state_int,
         (CASE t1.state_int WHEN 1 THEN 'Σ' WHEN 2 THEN '��' WHEN 3 THEN '��ͨ' ELSE '' END) state_int,
         t4.paytypename_vchr, t5.lastname_vchr AS doctername, t1.modify_dat
    FROM t_opr_bih_register t1 
        left join t_bse_patient t2 on t1.patientid_chr = t2.patientid_chr
        left join  t_bse_deptdesc t3 on t1.areaid_chr = t3.deptid_chr 
        left join  t_bse_patientpaytype t4 on t2.paytypeid_chr = t4.paytypeid_chr 
        left join  t_bse_employee t5 on t1.mzdoctor_chr = t5.empid_chr
        left join t_bse_patientcard t6 on t2.patientid_chr = t6.patientid_chr
   WHERE t1.status_int = 2
ORDER BY t1.areaid_chr, t1.modify_dat DESC";
            }
            /* <<======================================= */
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
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

        #region ����ת�벡����Ժ	glzhang	200.10.10
        /// <summary>
        /// ����ת�벡����Ժ	glzhang	200.10.10
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ�ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngPatientTurnIn(string p_strRegisterID, string p_strArearID, string p_strOperatorID, string p_strInPatientID)
        {
            long lngRes = 0;
            string p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(12, "transferid_chr", "t_opr_bih_transfer", out p_strRecordID);
            if (lngRes < 0)
            {
                return lngRes;
            }
            if (p_strInPatientID == "")
            {
                m_lngGetInpatientID(out p_strInPatientID);
            }
            string strNow = System.DateTime.Now.ToString();
            //�޸���Ժ�ǼǱ�
            string strSQL = @"UPDATE t_opr_bih_register t1
   SET t1.status_int = 1,t1.inpatientid_chr = '" + p_strInPatientID + @"',t1.isfromclinic = 1,
       t1.modify_dat =
                      TO_DATE ('" + strNow + @"', 'yyyy-mm-dd HH24:mi:ss')
 WHERE t1.registerid_chr = '" + p_strRegisterID + "'";

            /* @create by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"UPDATE t_opr_bih_register 
   SET t_opr_bih_register.status_int = 1,t_opr_bih_register.inpatientid_chr = '" + p_strInPatientID + @"',t_opr_bih_register.isfromclinic = 1,
       t_opr_bih_register.modify_dat =
                      --TO_DATE ('" + strNow + @"', 'yyyy-mm-dd HH24:mi:ss')
                        CONVERT(DATETIME,'" + strNow + @"',20)
 WHERE t_opr_bih_register.registerid_chr = '" + p_strRegisterID + "'";
            }
            /* <<======================================= */
            //���˵�ת�����Ӽ�¼
            string strSQL2 = @"INSERT INTO t_opr_bih_transfer
            (transferid_chr, targetareaid_chr, type_int, operatorid_chr,
             registerid_chr, modify_dat
            )
     VALUES ('" + p_strRecordID + @"', '" + p_strArearID + @"', 5, '" + p_strOperatorID + @"',
             '" + p_strRegisterID + @"', TO_DATE ('" + strNow + @"', 'yyyy-mm-dd HH24:mi:ss')
            )";

            /* @create by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL2 = @"INSERT INTO t_opr_bih_transfer
            (transferid_chr, targetareaid_chr, type_int, operatorid_chr,
             registerid_chr, modify_dat
            )
     VALUES ('" + p_strRecordID + @"', '" + p_strArearID + @"', 5, '" + p_strOperatorID + @"',
             '" + p_strRegisterID + @"', CONVERT(DATETIME,'" + strNow + @"',20)
            )";

            }
            /* <<======================================= */
            //�޸Ĳ�����Ϣ��סԺ��
            string strSQL3 = @"UPDATE t_bse_patient
   SET inpatientid_chr = '" + p_strInPatientID + @"'
 WHERE patientid_chr = (SELECT t1.patientid_chr
                          FROM t_opr_bih_register t1
                         WHERE t1.registerid_chr = '" + p_strRegisterID + "')";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
                lngRes = objHRPSvc.DoExcute(strSQL2);
                lngRes = objHRPSvc.DoExcute(strSQL3);
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

        #region	���ݲ���ID��ȡ�����Ƿ�����Ժ��Ϣ,��������Ժ�Ǽǿ�ʹ��	glzhang	2005.10.10
        /// <summary>
        /// ���ݲ���ID��ȡ������Ժ��Ϣ,��������Ժ�Ǽǿ�ʹ��	glzhang	2005.10.10
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_intFlag">��־:0=��Ժ������ת��Ĳ�����Ϣ,��������Ժ�Ǽǿ�ʹ��;1=��Ժ������ת��Ĳ�����Ϣ,��Ժʱʹ��</param>
        /// <param name="p_dtbResult">�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInHospitalInfo(string p_strPatientID, int p_intFlag, out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT t1.registerid_chr
  FROM t_opr_bih_register t1
 WHERE t1.patientid_chr = '" + p_strPatientID + "'";
            if (p_intFlag == 0)
            {
                strSQL += " AND (t1.status_int = 2 OR t1.pstatus_int <> 3)";
            }
            if (p_intFlag == 1)
            {
                strSQL += " AND t1.status_int <> 2 AND t1.pstatus_int <> 3";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
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

        #region �޸Ĳ���סԺ�� glzhag	glzhang 2006.01.20
        /// <summary>
        /// �޸Ĳ���סԺ�� glzhag	glzhang 2006.01.20
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOldInPatientID">��סԺ��</param>
        /// <param name="p_strNewInPatientID">��סԺ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyInPatientID(string p_strOldInPatientID, string p_strNewInPatientID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQLUpdate1 = @"UPDATE t_bse_patient t1
   SET t1.inpatientid_chr = '" + p_strNewInPatientID + @"'
 WHERE t1.inpatientid_chr = '" + p_strOldInPatientID + "'";

            string strSQLUpdate2 = @"UPDATE t_opr_bih_register t1
   SET t1.inpatientid_chr = '" + p_strNewInPatientID + @"'
 WHERE t1.inpatientid_chr = '" + p_strOldInPatientID + "'";

            string strSQLUpdate3 = @"UPDATE indeptinfo t1
   SET t1.inpatientid = '" + p_strNewInPatientID + @"'
 WHERE t1.inpatientid = '" + p_strOldInPatientID + "'";

            string strSQLUpdate4 = @"UPDATE inpatientdateinfo t1
   SET t1.inpatientid = '" + p_strNewInPatientID + @"'
 WHERE t1.inpatientid = '" + p_strOldInPatientID + "'";

            string strSQLUpdate5 = @"UPDATE patientbaseinfo t1
   SET t1.inpatientid = '" + p_strNewInPatientID + @"'
 WHERE t1.inpatientid ='" + p_strOldInPatientID + "'";

            try
            {
                //�޸Ĳ�����Ϣ��סԺ�ſ�
                lngRes = objHRPSvc.DoExcute(strSQLUpdate1);
                lngRes = objHRPSvc.DoExcute(strSQLUpdate2);
                lngRes = objHRPSvc.DoExcute(strSQLUpdate3);
                lngRes = objHRPSvc.DoExcute(strSQLUpdate4);
                lngRes = objHRPSvc.DoExcute(strSQLUpdate5);
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

        #region	��ȡԤ�������	glzhang	2005.03.06
        /// <summary>
        /// ��ȡԤ�������	glzhang	2005.03.06
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegsterID">��Ժ�Ǽ�ID</param>
        /// <param name="p_dtbResult">�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrepayMoney(string p_strRegsterID, out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT SUM (t1.money_dec) sum_money
  FROM t_opr_bih_prepay t1
 WHERE t1.registerid_chr = '" + p_strRegsterID + @"'
   AND t1.status_int = 1
   AND t1.isclear_int = 0";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
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

        //T_Opr_Bih_Leave(��Ժ��¼)
        #region ����
        /// <summary>
        /// ��Ժ---���ӳ�Ժ��¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��[200409010001]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewBihLeave(out string p_strRecordID, clsT_Opr_Bih_Leave_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(12, "leaveid_chr", "T_Opr_Bih_Leave", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_Opr_Bih_Leave (LEAVEID_CHR,REGISTERID_CHR,TYPE_INT,OUTDEPTID_CHR,DES_VCHR,OUTAREAID_CHR,STATUS_INT,MODIFY_DAT,OUTBEDID_CHR,OPERATORID_CHR,PSTATUS_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(11, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strTYPE_INT;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strOUTDEPTID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strDES_VCHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strOUTAREAID_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[7].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[8].Value = p_objRecord.m_strOUTBEDID_CHR;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strOPERATORID_CHR;
                objLisAddItemRefArr[10].Value = p_objRecord.m_intPSTATUS_INT;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        #region ����
        /// <summary>
        /// ��ѯ���еĳ�Ժ��¼����Ч�ģ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllBihLeaveInfo(out clsT_Opr_Bih_Leave_VO[] p_objResultArr)
        {
            //{status_int ״̬��-1��ʷ��0��Ч��1��Ч}	
            return m_lngGetBihLeaveInfo(" status_int = '1' ", out p_objResultArr);
        }
        /// <summary>
        /// ������Ժ�Ǽ���ˮ�Ų�ѯ��Ч�ĳ�Ժ�ǼǼ�¼ {ԭ����ֻ��һ����Ч�ļ�¼}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBinLeaveByRegisterID(string p_strRegisterid_chr, out clsT_Opr_Bih_Leave_VO[] p_objResultArr)
        {
            string strQueryCondition = " status_int = '1' AND ";
            strQueryCondition += " registerid_chr = '" + p_strRegisterid_chr.Trim() + "'";
            return m_lngGetBihLeaveInfo(strQueryCondition, out p_objResultArr);
        }
        /// <summary>
        /// ������������Ժ��¼��ѯ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strQueryCondition">��ѯ����</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihLeaveInfo(string p_strQueryCondition, out clsT_Opr_Bih_Leave_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Opr_Bih_Leave_VO[0];
            long lngRes = 0;

            string strSQL = "";
            strSQL += " SELECT a.*";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.outdeptid_chr) OutDeptName ";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.outareaid_chr) OutAreaName ";
            strSQL += "     ,(select code_chr from t_bse_bed where bedid_chr=a.outbedid_chr) OutBedNo ";
            strSQL += "     ,(select LastName_vchr FROM T_BSE_EMPLOYEE WHERE trim(empid_chr) =trim(a.operatorid_chr)) OPERATORNAME ";
            strSQL += "     ,a.TYPE_INT TypeName ";
            strSQL += "     ,decode(a.PSTATUS_INT,0,'Ԥ��Ժ',1,'ʵ�ʳ�Ժ','') PStatusName ";
            strSQL += " FROM t_opr_bih_leave a";

            /* @create by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT a.*
		     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.outdeptid_chr) OutDeptName 
		   ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.outareaid_chr) OutAreaName 
		,(select code_chr from t_bse_bed where bedid_chr=a.outbedid_chr) OutBedNo 
		  ,(select LastName_vchr FROM T_BSE_EMPLOYEE WHERE LTRIM(RTRIM(empid_chr)) =Ltrim(RTRIM(a.operatorid_chr))) OPERATORNAME 
		 ,a.TYPE_INT TypeName 
		-- ,decode(a.PSTATUS_INT,0,'Ԥ��Ժ',1,'ʵ�ʳ�Ժ','') PStatusName 
                ,(CASE  a.PSTATUS_INT WHEN 0 THEN 'Ԥ��Ժ' WHEN 1 THEN 'ʵ�ʳ�Ժ' ELSE '' END) PStatusName 
		FROM t_opr_bih_leave a";
            }
            /* <<======================================= */
            //���˵�ת�����Ӽ�¼


            if (p_strQueryCondition != string.Empty)
            {
                strSQL += " WHERE " + p_strQueryCondition;
            }
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Opr_Bih_Leave_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Opr_Bih_Leave_VO();
                        p_objResultArr[i1].m_strLEAVEID_CHR = dtbResult.Rows[i1]["LEAVEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTYPE_INT = dtbResult.Rows[i1]["TYPE_INT"].ToString().Trim();
                        p_objResultArr[i1].m_strOUTDEPTID_CHR = dtbResult.Rows[i1]["OUTDEPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOUTAREAID_CHR = dtbResult.Rows[i1]["OUTAREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strOUTBEDID_CHR = dtbResult.Rows[i1]["OUTBEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Int32.Parse(dtbResult.Rows[i1]["STATUS_INT"].ToString());
                        p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        //���ֶ�
                        p_objResultArr[i1].m_strOutDeptName = dtbResult.Rows[i1]["OutDeptName"].ToString().Trim();
                        p_objResultArr[i1].m_strOutAreaName = dtbResult.Rows[i1]["OutAreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strOutBedNo = dtbResult.Rows[i1]["OutBedNo"].ToString().Trim();
                        p_objResultArr[i1].m_strTypeName = dtbResult.Rows[i1]["TypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strOperatorName = dtbResult.Rows[i1]["OperatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strPStatusName = dtbResult.Rows[i1]["PStatusName"].ToString().Trim();

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
        #region �޸� | ɾ��
        #region  �޸ļ�¼״̬��-1��ʷ��0��Ч��1��Ч��	������Ժ�Ǽ���ˮ��
        /// <summary>
        /// �޸ĳ�Ժ�ļ�¼״̬��-1��ʷ��0��Ч��1��Ч��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_intStatus_int">״̬��-1��ʷ��0��Ч��1��Ч��</param>
        /// <param name="p_strOPERATORID_CHR">������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBihLeaveByRegisterID(string p_strRegisterid_chr, int p_intStatus_int, string p_strOPERATORID_CHR)
        {
            long lngRes = 0;
            string strSQLUpdate = "UPDATE  T_Opr_Bih_Leave SET ";
            strSQLUpdate += " Status_int =" + p_intStatus_int.ToString();
            strSQLUpdate += " ,OPERATORID_CHR ='" + p_strOPERATORID_CHR.Trim() + "' ";
            strSQLUpdate += " ,MODIFY_DAT = SYSDATE ";
            strSQLUpdate += " WHERE registerid_chr = '" + p_strRegisterid_chr.Trim() + "'";

            return m_lngModifyBihLeaveInfo(strSQLUpdate);
        }
        #endregion
        #region  �޸ļ�¼״̬��-1��ʷ��0��Ч��1��Ч��	���ݳ�Ժ��ˮ��
        /// <summary>
        /// �޸ĳ�Ժ�ļ�¼״̬��-1��ʷ��0��Ч��1��Ч��	������ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��Ժ��ˮ��</param>
        /// <param name="p_intStatus_int">״̬��-1��ʷ��0��Ч��1��Ч��</param>
        /// <param name="p_strOPERATORID_CHR">������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBihLeaveSTATUS_INTByID(string p_strID, int p_intStatus_int, string p_strOPERATORID_CHR)
        {
            long lngRes = 0;
            string strSQLUpdate = "UPDATE  T_Opr_Bih_Leave SET ";
            strSQLUpdate += " Status_int =" + p_intStatus_int.ToString();
            strSQLUpdate += " ,OPERATORID_CHR ='" + p_strOPERATORID_CHR.Trim() + "' ";
            strSQLUpdate += " ,MODIFY_DAT = SYSDATE ";
            strSQLUpdate += " WHERE LEAVEID_CHR = '" + p_strID.Trim() + "'";
            /* @update by wjqin (05-11-28)
                                     * ���SQL SERVER��strSQl�汾����
                                     */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQLUpdate = "UPDATE  T_Opr_Bih_Leave SET ";
                strSQLUpdate += " Status_int =" + p_intStatus_int.ToString();
                strSQLUpdate += " ,OPERATORID_CHR ='" + p_strOPERATORID_CHR.Trim() + "' ";
                strSQLUpdate += " ,MODIFY_DAT = GETDATE() ";
                strSQLUpdate += " WHERE LEAVEID_CHR = '" + p_strID.Trim() + "'";
            }
            /* <<======================================= */
            return m_lngModifyBihLeaveInfo(strSQLUpdate);
        }
        #endregion
        #region  �޸ĳ�Ժ״̬{0=Ԥ��Ժ;1=ʵ�ʳ�Ժ}	���ݳ�Ժ��ˮ��
        /// <summary>
        /// �޸ĳ�Ժ�ļ�¼״̬��-1��ʷ��0��Ч��1��Ч��	������ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��Ժ��ˮ��</param>
        /// <param name="p_intPSTATUS_INT">״̬��-1��ʷ��0��Ч��1��Ч��</param>
        /// <param name="p_strOPERATORID_CHR">������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBihLeavePSTATUS_INTByID(string p_strID, int p_intPSTATUS_INT, string p_strOPERATORID_CHR)
        {
            long lngRes = 0;
            string strSQLUpdate = "UPDATE  T_Opr_Bih_Leave SET ";
            strSQLUpdate += " PSTATUS_INT =" + p_intPSTATUS_INT.ToString();
            strSQLUpdate += " ,OPERATORID_CHR ='" + p_strOPERATORID_CHR.Trim() + "' ";
            strSQLUpdate += " ,MODIFY_DAT = SYSDATE ";
            strSQLUpdate += " WHERE LEAVEID_CHR = '" + p_strID.Trim() + "'";

            return m_lngModifyBihLeaveInfo(strSQLUpdate);
        }
        #endregion
        #region ��SQL�����޸ĳ�Ժ�Ǽ���Ϣ
        /// <summary>
        /// ��SQL�����޸ĳ�Ժ�Ǽ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSQLUpdate">Update��SQL���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBihLeaveInfo(string p_strSQLUpdate)
        {
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(p_strSQLUpdate);
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
        #endregion
        #region ��֤ĳ��Ժ��ˮ���Ƿ����[��Ч]��Ժ��¼
        /// <summary>
        /// ��֤ĳ��Ժ��ˮ���Ƿ����[��Ч]��Ժ��¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">��Ժ�Ǽ���ˮ��</param>
        /// <param name="IsLeaveHospital">�Ƿ��Ժ [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckIsLeaveHospitalByRegisterID(string p_strRegisterid_chr, out bool IsLeaveHospital)
        {
            IsLeaveHospital = false;
            clsT_Opr_Bih_Leave_VO[] p_objResultArr;
            long lngReturn = m_lngGetBinLeaveByRegisterID(p_strRegisterid_chr, out p_objResultArr);
            if (lngReturn > 0 && p_objResultArr != null && p_objResultArr.Length > 0 && p_objResultArr[0].m_strREGISTERID_CHR != null)
            {
                IsLeaveHospital = true;
            }
            return lngReturn;
        }
        #endregion

        //�ۺ� 
        #region ��ȡסԺ�ŵ����һ��סԺ��״̬ [�״���Ժ����Ժ���ѳ�Ժ]
        /// <summary>
        /// ��ȡסԺ�ŵ����һ��סԺ��״̬ [�״���Ժ����Ժ���ѳ�Ժ] 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientid_chr">סԺ��</param>
        /// <param name="p_intBihState">סԺ״̬ {1-�״���Ժ��2-��Ժ��3-�ѳ�Ժ} [ out ���� ]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihState(string p_strInpatientid_chr, out int p_intBihState)
        {
            p_intBihState = 0;
            string strRegisterid_chr = "";
            long lngReturn = m_lngGetRegisteridByInpatientID(p_strInpatientid_chr, out strRegisterid_chr);
            if (lngReturn <= 0)
            {
                return lngReturn;	//ִ�д���
            }
            if (strRegisterid_chr == "")
            {
                p_intBihState = 1;	//1-�״���Ժ
                return lngReturn;
            }

            lngReturn = m_lngGetBihStateByRegisterID(strRegisterid_chr, out p_intBihState);
            return lngReturn;
        }
        #endregion
        #region ��ȡסԺ��ˮ�ŵ�סԺ״̬ [�״���Ժ����Ժ���ѳ�Ժ]
        /// <summary>
        /// ��ȡסԺ��ˮ�ŵ�סԺ״̬ [�״���Ժ����Ժ���ѳ�Ժ] 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">סԺ��ˮ��</param>
        /// <param name="p_intBihState">סԺ״̬ {1-�״���Ժ��2-��Ժ��3-�ѳ�Ժ} [ out ���� ]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihStateByRegisterID(string p_strRegisterid_chr, out int p_intBihState)
        {
            p_intBihState = 0;
            bool IsLeaveHospital = false;
            if (p_strRegisterid_chr == string.Empty)
            {
                p_intBihState = 1;	//1-�״���Ժ
                return 1;
            }
            long lngReturn = m_lngCheckIsLeaveHospitalByRegisterID(p_strRegisterid_chr, out IsLeaveHospital);
            if (lngReturn <= 0)
            {
                return lngReturn;	//ִ�д���
            }
            if (IsLeaveHospital)
            {
                p_intBihState = 3;	//3-�ѳ�Ժ
                return lngReturn;
            }
            else
            {
                p_intBihState = 2;	//2-��Ժ
                return lngReturn;
            }
        }
        #endregion
        #region ��ȡĳסԺ�ŵ�����[��Ч��]��Ժ��¼
        /// <summary>
        /// ��ȡĳסԺ�ŵ�����[��Ч��]��Ժ��¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientid_chr">סԺ��</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihLeaveByInpatientID(string p_strInpatientid_chr, out clsT_Opr_Bih_Leave_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Opr_Bih_Leave_VO[0];
            long lngRes = 0;

            string strSQL = @"SELECT a.* ";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.outdeptid_chr) OutDeptName ";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.outareaid_chr) OutAreaName ";
            strSQL += "     ,(select code_chr from t_bse_bed where bedid_chr=a.outbedid_chr) OutBedNo ";
            strSQL += "     ,(select LastName_vchr FROM T_BSE_EMPLOYEE WHERE trim(empid_chr) =trim(a.operatorid_chr)) OPERATORNAME ";
            strSQL += "     ,a.TYPE_INT TypeName ";
            strSQL += "     ,decode(a.PSTATUS_INT,0,'Ԥ��Ժ',1,'ʵ�ʳ�Ժ','') PStatusName ";
            strSQL += " FROM t_opr_bih_leave a ,t_opr_bih_register b ";
            strSQL += " WHERE a.STATUS_INT=1 AND a.registerid_chr=b.registerid_chr AND b.inpatientid_chr='" + p_strInpatientid_chr.Trim() + "'";
            strSQL += " Order By a.modify_dat";

            /* @create by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT a.*  ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.outdeptid_chr) OutDeptName 
			 ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.outareaid_chr) OutAreaName 
			  ,(select code_chr from t_bse_bed where bedid_chr=a.outbedid_chr) OutBedNo 
		    --,(select LastName_vchr FROM T_BSE_EMPLOYEE WHERE trim(empid_chr) =trim(a.operatorid_chr)) OPERATORNAME 
                   ,(select LastName_vchr FROM T_BSE_EMPLOYEE WHERE Ltrim(RTRIM(empid_chr)) =Ltrim(RTRIM(a.operatorid_chr))) OPERATORNAME 
		   ,a.TYPE_INT TypeName
		   -- ,decode(a.PSTATUS_INT,0,'Ԥ��Ժ',1,'ʵ�ʳ�Ժ','') PStatusName 
                 ,(CASE a.PSTATUS_INT WHEN 0 THEN 'Ԥ��Ժ'WHEN 1 THEN 'ʵ�ʳ�Ժ' ELSE '' END ) PStatusName 
		 FROM t_opr_bih_leave a ,t_opr_bih_register b 
		 WHERE a.STATUS_INT=1 AND a.registerid_chr=b.registerid_chr AND b.inpatientid_chr='" + p_strInpatientid_chr.Trim() + @"' Order By a.modify_dat";
            }
            /* <<======================================= */
            //���˵�ת�����Ӽ�¼
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Opr_Bih_Leave_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Opr_Bih_Leave_VO();
                        p_objResultArr[i1].m_strLEAVEID_CHR = dtbResult.Rows[i1]["LEAVEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTYPE_INT = dtbResult.Rows[i1]["TYPE_INT"].ToString().Trim();
                        p_objResultArr[i1].m_strOUTDEPTID_CHR = dtbResult.Rows[i1]["OUTDEPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOUTAREAID_CHR = dtbResult.Rows[i1]["OUTAREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strOUTBEDID_CHR = dtbResult.Rows[i1]["OUTBEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        //���ֶ�
                        p_objResultArr[i1].m_strOutDeptName = dtbResult.Rows[i1]["OutDeptName"].ToString().Trim();
                        p_objResultArr[i1].m_strOutAreaName = dtbResult.Rows[i1]["OutAreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strOutBedNo = dtbResult.Rows[i1]["OutBedNo"].ToString().Trim();
                        p_objResultArr[i1].m_strTypeName = dtbResult.Rows[i1]["TypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strOperatorName = dtbResult.Rows[i1]["OperatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strPStatusName = dtbResult.Rows[i1]["PStatusName"].ToString().Trim();
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
        #region ��ȡĳסԺ�ŵ����е�ת��¼
        /// <summary>
        /// ��ȡĳסԺ�ŵ����е�ת��¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientid_chr">סԺ��</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihTransferByInpatientID(string p_strInpatientid_chr, string p_strFilter, out clsT_Opr_Bih_Transfer_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Opr_Bih_Transfer_VO[0];
            long lngRes = 0;

            string strSQL = "";
            strSQL += " SELECT ";
            strSQL += "     a.REGISTERID_CHR ";
            strSQL += "     ,a.TRANSFERID_CHR ";
            strSQL += "     ,a.SOURCEDEPTID_CHR ";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.SOURCEDEPTID_CHR)SOURCEDEPTNAME ";
            strSQL += "     ,a.SOURCEAREAID_CHR ";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.SOURCEAREAID_CHR)SOURCEAREANAME ";
            strSQL += "     ,a.SOURCEBEDID_CHR ";
            strSQL += "     ,(select code_chr from t_bse_bed where bedid_chr=a.sourcebedid_chr) SourceBedNo ";
            strSQL += "     ,a.TARGETDEPTID_CHR ";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.TARGETDEPTID_CHR)TARGETDEPTNAME ";
            strSQL += "     ,TARGETAREAID_CHR ";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.TARGETAREAID_CHR)TARGETAREANAME ";
            strSQL += "     ,a.TARGETBEDID_CHR ";
            strSQL += "     ,(select code_chr from t_bse_bed where bedid_chr=a.targetbedid_chr) TargetBedNo ";
            strSQL += "     ,a.TYPE_INT ";
            strSQL += "     ,DECODE(a.TYPE_INT,1,'ת��',2,'����',3,'ר��+����',4,'��Ժ�ٻ�',5,'��Ժ',6,'��Ժ','') TypeName ";
            strSQL += "     ,a.DES_VCHR ";
            strSQL += "     ,a.OPERATORID_CHR ";
            strSQL += "     ,(select LastName_vchr FROM T_BSE_EMPLOYEE WHERE trim(empid_chr) =trim(a.OPERATORID_CHR))OPERATORNAME ";
            strSQL += "     ,a.MODIFY_DAT ";
            strSQL += " FROM t_opr_bih_transfer a,t_opr_bih_register b  ";
            strSQL += " WHERE a.registerid_chr=b.registerid_chr AND b.inpatientid_chr='" + p_strInpatientid_chr.Trim() + "'" + p_strFilter;
            strSQL += " Order By b.INPATIENTCOUNT_INT,a.MODIFY_DAT";

            /* @create by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT  a.REGISTERID_CHR 
		 ,a.TRANSFERID_CHR 
		  ,a.SOURCEDEPTID_CHR 
		 ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.SOURCEDEPTID_CHR)SOURCEDEPTNAME 
		 ,a.SOURCEAREAID_CHR 
	 ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.SOURCEAREAID_CHR)SOURCEAREANAME 
		 ,a.SOURCEBEDID_CHR 
		  ,(select code_chr from t_bse_bed where bedid_chr=a.sourcebedid_chr) SourceBedNo 
		  ,a.TARGETDEPTID_CHR 
		 ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.TARGETDEPTID_CHR)TARGETDEPTNAME 
		,TARGETAREAID_CHR 
	 ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.TARGETAREAID_CHR)TARGETAREANAME 
		 ,a.TARGETBEDID_CHR 
		 ,(select code_chr from t_bse_bed where bedid_chr=a.targetbedid_chr) TargetBedNo 
	 ,a.TYPE_INT 
	--,DECODE(a.TYPE_INT,1,'ת��',2,'����',3,'ר��+����',4,'��Ժ�ٻ�',5,'��Ժ',6,'��Ժ','') TypeName 
          ,(CASE a.TYPE_INT WHEN 1 THEN 'ת��' WHEN 2 THEN '����' WHEN 3 THEN 'ר��+����' WHEN 4 THEN '��Ժ�ٻ�' WHEN 5 THEN '��Ժ' WHEN 6 THEN '��Ժ' ELSE '' END) TypeName 
	 ,a.DES_VCHR 
	 ,a.OPERATORID_CHR 
	,(select LastName_vchr FROM T_BSE_EMPLOYEE WHERE Ltrim(RTRIM(empid_chr)) =Ltrim(RTRIM(a.OPERATORID_CHR))) OPERATORNAME 
	,a.MODIFY_DAT 
	 FROM t_opr_bih_transfer a,t_opr_bih_register b  
	 WHERE a.registerid_chr=b.registerid_chr AND b.inpatientid_chr='" + p_strInpatientid_chr.Trim() + "'" + p_strFilter + @"
	 Order By b.INPATIENTCOUNT_INT,a.MODIFY_DAT ";
            }
            /* <<======================================= */
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Opr_Bih_Transfer_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Opr_Bih_Transfer_VO();
                        //��ˮ��
                        p_objResultArr[i1].m_strTRANSFERID_CHR = dtbResult.Rows[i1]["TRANSFERID_CHR"].ToString().Trim();
                        //Դ����id
                        p_objResultArr[i1].m_strSOURCEDEPTID_CHR = dtbResult.Rows[i1]["SOURCEDEPTID_CHR"].ToString().Trim();
                        //Դ��������
                        if (dtbResult.Rows[i1]["SOURCEDEPTNAME"] != null)
                        {
                            p_objResultArr[i1].m_strSourceDeptName = dtbResult.Rows[i1]["SOURCEDEPTNAME"].ToString().Trim();
                        }
                        //Դ����id
                        p_objResultArr[i1].m_strSOURCEAREAID_CHR = dtbResult.Rows[i1]["SOURCEAREAID_CHR"].ToString().Trim();
                        //Դ��������
                        if (dtbResult.Rows[i1]["SOURCEAREANAME"] != null)
                        {
                            p_objResultArr[i1].m_strSourceAreaName = dtbResult.Rows[i1]["SOURCEAREANAME"].ToString().Trim();
                        }
                        //Դ����id
                        p_objResultArr[i1].m_strSOURCEBEDID_CHR = dtbResult.Rows[i1]["SOURCEBEDID_CHR"].ToString().Trim();
                        //Դ���� [���ֶ�]
                        if (dtbResult.Rows[i1]["SourceBedNo"] != null)
                        {
                            p_objResultArr[i1].m_strSourceBedNo = dtbResult.Rows[i1]["SourceBedNo"].ToString().Trim();
                        }
                        //Ŀ�����id
                        p_objResultArr[i1].m_strTARGETDEPTID_CHR = dtbResult.Rows[i1]["TARGETDEPTID_CHR"].ToString().Trim();
                        //Ŀ���������
                        if (dtbResult.Rows[i1]["TARGETDEPTNAME"] != null)
                        {
                            p_objResultArr[i1].m_strTargetDeptName = dtbResult.Rows[i1]["TARGETDEPTNAME"].ToString().Trim();
                        }
                        //Ŀ�겡��id
                        p_objResultArr[i1].m_strTARGETAREAID_CHR = dtbResult.Rows[i1]["TARGETAREAID_CHR"].ToString().Trim();
                        //Ŀ�괲�� [���ֶ�]
                        p_objResultArr[i1].m_strTargetBedNo = dtbResult.Rows[i1]["TargetBedNo"].ToString().Trim();
                        //Ŀ�겡������
                        if (dtbResult.Rows[i1]["TARGETAREANAME"] != null)
                        {
                            p_objResultArr[i1].m_strTargetAreaName = dtbResult.Rows[i1]["TARGETAREANAME"].ToString().Trim();
                        }
                        //Ŀ�겡��id
                        p_objResultArr[i1].m_strTARGETBEDID_CHR = dtbResult.Rows[i1]["TARGETBEDID_CHR"].ToString().Trim();
                        //Ŀ�괲�� [���ֶ�]
                        if (dtbResult.Rows[i1]["TargetBedNo"] != null)
                        {
                            p_objResultArr[i1].m_strTargetBedNo = dtbResult.Rows[i1]["TargetBedNo"].ToString().Trim();
                        }
                        //�������Ͳ�������{1=ת��;2=����;3=ת��+����;4=��Ժ����;5=��Ժ;6=��Ժ}
                        p_objResultArr[i1].m_intTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["TYPE_INT"].ToString());
                        //������������
                        if (dtbResult.Rows[i1]["TypeName"] != null)
                        {
                            p_objResultArr[i1].m_strTypeName = dtbResult.Rows[i1]["TypeName"].ToString().Trim();
                        }
                        //��ע
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        //������ID
                        p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        ///����������
                        if (dtbResult.Rows[i1]["OPERATORNAME"] != null)
                        {
                            p_objResultArr[i1].m_strOperatorName = dtbResult.Rows[i1]["OPERATORNAME"].ToString().Trim();
                        }
                        //��Ժ�Ǽ���ˮ��(200409010001)
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        //�޸����ڣ���������
                        p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
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
        #region ͳ��ĳ����ĳ�����Ŀ�ת��Ĳ�����Ϣ
        /// <summary>
        /// ͳ��ĳ����ĳ�����Ŀ�ת��Ĳ�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">���ſ���ID</param>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_dtbResult">[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMayGetInPatientByAreaID(string p_strDeptID, string p_strAreaID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            //{��Ϊת��ʱ��Ժ�ǼǼ�¼ҲҪ�䣬����ֻҪ��ѯ��Ժ�ǼǱ��еļ�¼����}
            //����Ϊ�� ���������ҵĲ���Ϊ�ջ򱾲������޲����ļ�¼
            string strSQL = @"SELECT t1.*, t2.sex_chr
  FROM t_opr_bih_register t1, t_bse_patient t2
 WHERE t1.status_int = 1
   AND t1.patientid_chr = t2.patientid_chr(+)
   AND t1.pstatus_int = 0
   AND t1.areaid_chr = '" + p_strAreaID + "'";


            /* @create by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT t1.*, t2.sex_chr
  FROM t_opr_bih_register t1 left join  t_bse_patient t2 on t1.patientid_chr = t2.patientid_chr
 WHERE t1.status_int = 1
   
   AND t1.pstatus_int = 0
   AND t1.areaid_chr = '" + p_strAreaID + "'";
            }
            /* <<======================================= */
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
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
        #region ������Ժ�Ǽ���ˮ�Ż�ȡסԺ��Ϣ�Ͳ��˻�����Ϣ
        /// <summary>
        /// ������Ժ�Ǽ���ˮ�Ż�ȡסԺ��Ϣ�Ͳ��˻�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInfoByRegisterID(string p_strRegisterID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();
            string strSQL = "";
            strSQL += " select a.*,b.* ";
            strSQL += "		,TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(b.birth_dat,'YYYY') As Age";
            strSQL += "		,(select deptname_vchr from t_bse_deptdesc a1 where a1.deptid_chr=a.areaid_chr) AreaName";
            strSQL += "		,(select code_chr from t_bse_bed a2 where a2.areaid_chr=a.areaid_chr and a2.bedid_chr=a.bedid_chr) BedCode";
            strSQL += "		,(select modify_dat from t_opr_bih_leave a3 where a3.registerid_chr=a.registerid_chr and a3.status_int=1) LeaveHospitalTime";
            strSQL += "		,(select sum(money_dec) from t_opr_bih_prepay a4 where a4.registerid_chr=a.registerid_chr and a4.status_int=1 and a4.isclear_int=0) PrePayMoney";
            strSQL += "		,(SELECT chearact_dat FROM (SELECT chearact_dat FROM t_opr_bih_paymoney WHERE t_opr_bih_paymoney.registerid_chr='" + p_strRegisterID.Trim() + "' ORDER BY chearact_dat desc) WHERE rownum=1) chearact_dat";//��������
            strSQL += " from t_opr_bih_register a,t_bse_patient b";
            strSQL += " where a.patientid_chr=b.patientid_chr";
            strSQL += "     and a.registerid_chr='" + p_strRegisterID.Trim() + "'";

            /* @create by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @" select a.*,b.*,
              CONVERT(VARCHAR(4),DATEDIFF(YEAR,b.birth_dat,GETDATE())) As Age
			,(select top 1 deptname_vchr from t_bse_deptdesc a1 where a1.deptid_chr=a.areaid_chr) AreaName
		,(select top 1 code_chr from t_bse_bed a2 where a2.areaid_chr=a.areaid_chr and a2.bedid_chr=a.bedid_chr) BedCode
		,(select top 1 modify_dat from t_opr_bih_leave a3 where a3.registerid_chr=a.registerid_chr and a3.status_int=1) LeaveHospitalTime
		,(select sum(money_dec) from t_opr_bih_prepay a4 where a4.registerid_chr=a.registerid_chr and a4.status_int=1 and a4.isclear_int=0) PrePayMoney
		,(SELECT top  1 chearact_dat from t_opr_bih_paymoney WHERE t_opr_bih_paymoney.registerid_chr='" + p_strRegisterID.Trim() + @"'  ORDER BY chearact_dat desc) chearact_dat
		
         from t_opr_bih_register a,t_bse_patient b
		 where a.patientid_chr=b.patientid_chr
	    and a.registerid_chr='" + p_strRegisterID.Trim() + "'";
            }
            /* <<======================================= */


            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
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



        //T_Opr_Bih_Transfer(סԺ��ת��¼)
        #region ����
        /// <summary>
        /// ����סԺ��ת��¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">סԺ��ת��ˮ�� [out ����]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewBinTransfer(out string p_strRecordID, clsT_Opr_Bih_Transfer_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(12, "transferid_chr", "t_opr_bih_transfer", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;

            string strDateTime = p_objRecord.m_strMODIFY_DAT;
            //			string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //��Ժ�Ǽ�
            DataTable dtbResult = null;
            lngRes = 0;
            lngRes = new com.digitalwave.iCare.middletier.HIS.clsBedManageSvc().m_lngGetPatientLastestTransferInfo(p_objRecord.m_strREGISTERID_CHR, out dtbResult);
            string DeleteTurnOutRecord = "";
            string TransferID = "";
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                if (dtbResult.Rows[0]["SOURCEAREAID_CHR"] != System.DBNull.Value && dtbResult.Rows[0]["TARGETAREAID_CHR"] != System.DBNull.Value && dtbResult.Rows[0]["TARGETBEDID_CHR"] == System.DBNull.Value)
                {
                    TransferID = dtbResult.Rows[0]["TRANSFERID_CHR"].ToString();
                    DeleteTurnOutRecord = "delete from T_OPR_BIH_TRANSFER where TRANSFERID_CHR = '" + dtbResult.Rows[0]["TRANSFERID_CHR"].ToString() + "'";
                }
            }

            string strSQL = "INSERT INTO t_opr_bih_transfer (TRANSFERID_CHR,SOURCEDEPTID_CHR,SOURCEAREAID_CHR,SOURCEBEDID_CHR,TARGETDEPTID_CHR,TARGETAREAID_CHR,TARGETBEDID_CHR,TYPE_INT,DES_VCHR,OPERATORID_CHR,REGISTERID_CHR,MODIFY_DAT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)";

            //��Ժ���䴲λ
            if (p_objRecord.m_intTYPE_INT == 5 && p_objRecord.m_strTARGETBEDID_CHR != "")
            {
                strSQL = "UPDATE t_opr_bih_transfer SET DES_VCHR = '" + p_objRecord.m_strDES_VCHR + "',TYPE_INT = 5,MODIFY_DAT = TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss'),TARGETBEDID_CHR = '" + p_objRecord.m_strTARGETBEDID_CHR + "' WHERE REGISTERID_CHR = '" + p_objRecord.m_strREGISTERID_CHR + "' AND TYPE_INT = 5 AND TARGETBEDID_CHR IS NULL";
                /* @create by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾���
             */
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQL = @" UPDATE t_opr_bih_transfer SET DES_VCHR = '" + p_objRecord.m_strDES_VCHR + "',OPERATORID_CHR = '" + p_objRecord.m_strOPERATORID_CHR + "',TYPE_INT = 5,MODIFY_DAT = CONVERT(DATETIME,'" + strDateTime + "',20),TARGETBEDID_CHR = '" + p_objRecord.m_strTARGETBEDID_CHR + "' WHERE REGISTERID_CHR = '" + p_objRecord.m_strREGISTERID_CHR + "' AND TYPE_INT = 5 AND TARGETBEDID_CHR IS NULL";
                }
                /* <<======================================= */
                try
                {
                    lngRes = 0;
                    lngRes = objHRPSvc.DoExcute(strSQL);
                    objHRPSvc.Dispose();
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                //				#region ������־ͳ��
                //lngRes=0;
                //				lngRes = new clsBihStatQuerySvc().m_lngUpdateSickRoomLOGSTAT(p_objRecord);
                //				#endregion
            }
            //��Ժ�ٻ�
            else if (p_objRecord.m_intTYPE_INT == 4)
            {
                strSQL = @"UPDATE t_opr_bih_transfer SET DES_VCHR = '" + p_objRecord.m_strDES_VCHR + "',OPERATORID_CHR = '" + p_objRecord.m_strOPERATORID_CHR + "',TYPE_INT = 4,MODIFY_DAT = TO_DATE('" + strDateTime + @"','YYYY-MM-DD hh24:mi:ss'),TARGETBEDID_CHR = '' WHERE TRANSFERID_CHR = (select a.TRANSFERID_CHR from t_opr_bih_transfer a ,(
                           SELECT   MAX (t_opr_bih_transfer.modify_dat) AS modify_dat,
                           t_opr_bih_transfer.registerid_chr
                           FROM t_opr_bih_transfer
                           GROUP BY t_opr_bih_transfer.registerid_chr) b
                           where a.modify_dat = b.modify_dat and a.registerid_chr = b.registerid_chr
                           and b.registerid_chr = '" + p_objRecord.m_strREGISTERID_CHR + "')";
                try
                {
                    lngRes = 0;
                    lngRes = objHRPSvc.DoExcute(strSQL);
                    objHRPSvc.Dispose();
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                //				#region ������־ͳ��
                //lngRes=0;
                //				lngRes = new clsBihStatQuerySvc().m_lngUpdateSickRoomLOGSTAT(p_objRecord);
                //				#endregion
            }
            else
            {
                strSQL = "INSERT INTO t_opr_bih_transfer (TRANSFERID_CHR,SOURCEDEPTID_CHR,SOURCEAREAID_CHR,SOURCEBEDID_CHR,TARGETDEPTID_CHR,TARGETAREAID_CHR,TARGETBEDID_CHR,TYPE_INT,DES_VCHR,OPERATORID_CHR,REGISTERID_CHR,MODIFY_DAT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)";
                try
                {
                    System.Data.IDataParameter[] objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(12, out objLisAddItemRefArr);
                    //Please change the datetime and reocrdid 
                    objLisAddItemRefArr[0].Value = p_strRecordID;
                    objLisAddItemRefArr[1].Value = p_objRecord.m_strSOURCEDEPTID_CHR;
                    objLisAddItemRefArr[2].Value = p_objRecord.m_strSOURCEAREAID_CHR;
                    objLisAddItemRefArr[3].Value = p_objRecord.m_strSOURCEBEDID_CHR;
                    objLisAddItemRefArr[4].Value = p_objRecord.m_strTARGETDEPTID_CHR;
                    objLisAddItemRefArr[5].Value = p_objRecord.m_strTARGETAREAID_CHR;
                    objLisAddItemRefArr[6].Value = p_objRecord.m_strTARGETBEDID_CHR;
                    objLisAddItemRefArr[7].Value = p_objRecord.m_intTYPE_INT;
                    objLisAddItemRefArr[8].Value = p_objRecord.m_strDES_VCHR;
                    objLisAddItemRefArr[9].Value = p_objRecord.m_strOPERATORID_CHR;
                    objLisAddItemRefArr[10].Value = p_objRecord.m_strREGISTERID_CHR;
                    objLisAddItemRefArr[11].Value = DateTime.Parse(strDateTime);
                    long lngRecEff = -1;
                    //�������Ӽ�¼
                    if (DeleteTurnOutRecord != "")
                    {
                        lngRes = 0;
                        lngRes = objHRPSvc.DoExcute(DeleteTurnOutRecord);
                        objLisAddItemRefArr[0].Value = TransferID;
                    }
                    lngRes = 0;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                    objHRPSvc.Dispose();
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                //				#region ������־ͳ��
                //lngRes=0;
                //				lngRes = new clsBihStatQuerySvc().m_lngUpdateSickRoomLOGSTAT(p_objRecord);
                //				#endregion
            }
            return lngRes;
        }
        #endregion
        #region ����
        /// <summary>
        /// ������Ժ�Ǽ���ˮ�Ų�ѯת����¼ {�����ˣɣĵ��Σ����ת��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBinTransferByRegisterID(string p_strRegisterid_chr, string p_strFilter, out clsT_Opr_Bih_Transfer_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Opr_Bih_Transfer_VO[0];
            long lngRes = 0;
            string strSQL = "";
            strSQL += " SELECT ";
            strSQL += "     a.REGISTERID_CHR ";
            strSQL += "     ,a.TRANSFERID_CHR ";
            strSQL += "     ,a.SOURCEDEPTID_CHR ";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.SOURCEDEPTID_CHR)SOURCEDEPTNAME ";
            strSQL += "     ,a.SOURCEAREAID_CHR ";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.SOURCEAREAID_CHR)SOURCEAREANAME ";
            strSQL += "     ,a.SOURCEBEDID_CHR ";
            strSQL += "     ,(select code_chr from t_bse_bed where bedid_chr=a.sourcebedid_chr) SourceBedNo ";
            strSQL += "     ,a.TARGETDEPTID_CHR ";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.TARGETDEPTID_CHR)TARGETDEPTNAME ";
            strSQL += "     ,TARGETAREAID_CHR ";
            strSQL += "     ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.TARGETAREAID_CHR)TARGETAREANAME ";
            strSQL += "     ,a.TARGETBEDID_CHR ";
            strSQL += "     ,(select code_chr from t_bse_bed where bedid_chr=a.targetbedid_chr) TargetBedNo ";
            strSQL += "     ,a.TYPE_INT ";
            strSQL += "     ,DECODE(a.TYPE_INT,1,'ת��',2,'����',3,'ת��',4,'��Ժ�ٻ�',5,'��Ժ',6,'��Ժ','') TypeName ";
            strSQL += "     ,a.DES_VCHR ";
            strSQL += "     ,a.OPERATORID_CHR ";
            strSQL += "     ,(select LastName_vchr FROM T_BSE_EMPLOYEE WHERE trim(empid_chr) =trim(a.OPERATORID_CHR))OPERATORNAME ";
            strSQL += "     ,a.MODIFY_DAT ";
            strSQL += " FROM t_opr_bih_transfer a";
            strSQL += " WHERE a.registerid_chr = '" + p_strRegisterid_chr.Trim() + "' " + p_strFilter;
            strSQL += " Order by a.MODIFY_DAT ";
            /* @create by wjqin (05-11-25)
                         * ���SQL SERVER��strSQl�汾���
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT  a.REGISTERID_CHR  ,a.TRANSFERID_CHR 
 ,a.SOURCEDEPTID_CHR 
  ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.SOURCEDEPTID_CHR)SOURCEDEPTNAME 
 ,a.SOURCEAREAID_CHR 
 ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.SOURCEAREAID_CHR)SOURCEAREANAME 
 ,a.SOURCEBEDID_CHR 
 ,(select code_chr from t_bse_bed where bedid_chr=a.sourcebedid_chr) SourceBedNo 
 ,a.TARGETDEPTID_CHR 
 ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.TARGETDEPTID_CHR)TARGETDEPTNAME 
  ,TARGETAREAID_CHR 
 ,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.TARGETAREAID_CHR)TARGETAREANAME 
  ,a.TARGETBEDID_CHR 
,(select code_chr from t_bse_bed where bedid_chr=a.targetbedid_chr) TargetBedNo 
 ,a.TYPE_INT 
 --,DECODE(a.TYPE_INT,1,'ת��',2,'����',3,'ת��',4,'��Ժ�ٻ�',5,'��Ժ',6,'��Ժ','') TypeName 
,(CASE a.TYPE_INT WHEN 1 THEN 'ת��' WHEN 2 THEN '����' WHEN 3 THEN 'ת��' WHEN 4 THEN '��Ժ�ٻ�' WHEN 5 THEN '��Ժ' WHEN 6 THEN '��Ժ' ELSE '' END ) TypeName 
 ,a.DES_VCHR 
,a.OPERATORID_CHR 
 ,(select LastName_vchr FROM T_BSE_EMPLOYEE WHERE Ltrim(RTRIM(empid_chr)) =Ltrim(RTRIM(a.OPERATORID_CHR)))OPERATORNAME 
 ,a.MODIFY_DAT 
 FROM t_opr_bih_transfer a
 WHERE 
 a.registerid_chr = '" + p_strRegisterid_chr.Trim() + "' " + p_strFilter;
                strSQL += @" Order by a.MODIFY_DAT ";
            }
            /* <<======================================= */
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Opr_Bih_Transfer_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Opr_Bih_Transfer_VO();
                        //��ˮ��
                        p_objResultArr[i1].m_strTRANSFERID_CHR = dtbResult.Rows[i1]["TRANSFERID_CHR"].ToString().Trim();
                        //Դ����id
                        p_objResultArr[i1].m_strSOURCEDEPTID_CHR = dtbResult.Rows[i1]["SOURCEDEPTID_CHR"].ToString().Trim();
                        //Դ��������
                        if (dtbResult.Rows[i1]["SOURCEDEPTNAME"] != null)
                        {
                            p_objResultArr[i1].m_strSourceDeptName = dtbResult.Rows[i1]["SOURCEDEPTNAME"].ToString().Trim();
                        }
                        //Դ����id
                        p_objResultArr[i1].m_strSOURCEAREAID_CHR = dtbResult.Rows[i1]["SOURCEAREAID_CHR"].ToString().Trim();
                        //Դ��������
                        if (dtbResult.Rows[i1]["SOURCEAREANAME"] != null)
                        {
                            p_objResultArr[i1].m_strSourceAreaName = dtbResult.Rows[i1]["SOURCEAREANAME"].ToString().Trim();
                        }
                        //Դ����id
                        p_objResultArr[i1].m_strSOURCEBEDID_CHR = dtbResult.Rows[i1]["SOURCEBEDID_CHR"].ToString().Trim();
                        //Դ���� [���ֶ�]
                        if (dtbResult.Rows[i1]["SourceBedNo"] != null)
                        {
                            p_objResultArr[i1].m_strSourceBedNo = dtbResult.Rows[i1]["SourceBedNo"].ToString().Trim();
                        }
                        //Ŀ�����id
                        p_objResultArr[i1].m_strTARGETDEPTID_CHR = dtbResult.Rows[i1]["TARGETDEPTID_CHR"].ToString().Trim();
                        //Ŀ���������
                        if (dtbResult.Rows[i1]["TARGETDEPTNAME"] != null)
                        {
                            p_objResultArr[i1].m_strTargetDeptName = dtbResult.Rows[i1]["TARGETDEPTNAME"].ToString().Trim();
                        }
                        //Ŀ�겡��id
                        p_objResultArr[i1].m_strTARGETAREAID_CHR = dtbResult.Rows[i1]["TARGETAREAID_CHR"].ToString().Trim();
                        //Ŀ�겡������
                        if (dtbResult.Rows[i1]["TARGETAREANAME"] != null)
                        {
                            p_objResultArr[i1].m_strTargetAreaName = dtbResult.Rows[i1]["TARGETAREANAME"].ToString().Trim();
                        }
                        //Ŀ�겡��id
                        p_objResultArr[i1].m_strTARGETBEDID_CHR = dtbResult.Rows[i1]["TARGETBEDID_CHR"].ToString().Trim();
                        //Ŀ�괲�� [���ֶ�]
                        if (dtbResult.Rows[i1]["TargetBedNo"] != null)
                        {
                            p_objResultArr[i1].m_strTargetBedNo = dtbResult.Rows[i1]["TargetBedNo"].ToString().Trim();
                        }
                        //��������{1=ת��;2=����;3=ת��+����;4=��Ժ����;5=��Ժ;6=��Ժ}
                        p_objResultArr[i1].m_intTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["TYPE_INT"].ToString());
                        //������������
                        if (dtbResult.Rows[i1]["TypeName"] != null)
                        {
                            p_objResultArr[i1].m_strTypeName = dtbResult.Rows[i1]["TypeName"].ToString().Trim();
                        }
                        //��ע
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        //������ID
                        p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        ///����������
                        if (dtbResult.Rows[i1]["OPERATORNAME"] != null)
                        {
                            p_objResultArr[i1].m_strOperatorName = dtbResult.Rows[i1]["OPERATORNAME"].ToString().Trim();
                        }
                        //��Ժ�Ǽ���ˮ��(200409010001)
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        //�޸����ڣ���������
                        p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
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
        /// <summary>
        /// ����ת����ˮ�Ų�ѯ��Ӧ��¼����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTransferID">ת����ˮ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBinTransferByTransferID(string p_strTransferID, out clsT_Opr_Bih_Transfer_VO p_objResult)
        {
            p_objResult = new clsT_Opr_Bih_Transfer_VO();

            clsT_Opr_Bih_Transfer_VO[] p_objResultArr;
            string strQueryCondition = " transferid_chr = '" + p_strTransferID.Trim() + "'";
            long lngReturn = m_lngGetBihTransferInfo(strQueryCondition, out p_objResultArr);
            if (lngReturn > 0 && p_objResultArr.Length > 0)
                p_objResult = p_objResultArr[0];
            return lngReturn;
        }
        /// <summary>
        /// ����������ת����ѯ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strQueryCondition">��ѯ����</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihTransferInfo(string p_strQueryCondition, out clsT_Opr_Bih_Transfer_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Opr_Bih_Transfer_VO[0];
            long lngRes = 0;

            //��ѯ������  ȷ����ѯSQL����ִ�С�	  	 
            if (p_strQueryCondition.Trim() == "") p_strQueryCondition = " 1=1 ";
            string strSQL = @"SELECT * FROM t_opr_bih_transfer WHERE " + p_strQueryCondition;
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Opr_Bih_Transfer_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Opr_Bih_Transfer_VO();
                        p_objResultArr[i1].m_strTRANSFERID_CHR = dtbResult.Rows[i1]["TRANSFERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSOURCEDEPTID_CHR = dtbResult.Rows[i1]["SOURCEDEPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSOURCEAREAID_CHR = dtbResult.Rows[i1]["SOURCEAREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSOURCEBEDID_CHR = dtbResult.Rows[i1]["SOURCEBEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTARGETDEPTID_CHR = dtbResult.Rows[i1]["TARGETDEPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTARGETAREAID_CHR = dtbResult.Rows[i1]["TARGETAREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTARGETBEDID_CHR = dtbResult.Rows[i1]["TARGETBEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["TYPE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
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
        #region ����סԺ�Ǽ�ID���ҵ�ת��¼
        [AutoComplete]
        public long m_lngGetTransferInfoByRegisterID(string p_strRegisterID, out clsT_Opr_Bih_Transfer_VO p_objResult)
        {
            p_objResult = new clsT_Opr_Bih_Transfer_VO();
            long lngRes = 0;
            string strSQL = @"select a.* from t_opr_bih_transfer a ,(
                              SELECT   MAX (t_opr_bih_transfer.modify_dat) AS modify_dat,
                              t_opr_bih_transfer.registerid_chr
                              FROM t_opr_bih_transfer
                              GROUP BY t_opr_bih_transfer.registerid_chr) b
                              where a.modify_dat = b.modify_dat and a.registerid_chr = b.registerid_chr
                              and b.registerid_chr = '" + p_strRegisterID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Opr_Bih_Transfer_VO();
                    p_objResult.m_strTRANSFERID_CHR = dtbResult.Rows[0]["TRANSFERID_CHR"].ToString().Trim();
                    p_objResult.m_strSOURCEDEPTID_CHR = dtbResult.Rows[0]["SOURCEDEPTID_CHR"].ToString().Trim();
                    p_objResult.m_strSOURCEAREAID_CHR = dtbResult.Rows[0]["SOURCEAREAID_CHR"].ToString().Trim();
                    p_objResult.m_strSOURCEBEDID_CHR = dtbResult.Rows[0]["SOURCEBEDID_CHR"].ToString().Trim();
                    p_objResult.m_strTARGETDEPTID_CHR = dtbResult.Rows[0]["TARGETDEPTID_CHR"].ToString().Trim();
                    p_objResult.m_strTARGETAREAID_CHR = dtbResult.Rows[0]["TARGETAREAID_CHR"].ToString().Trim();
                    p_objResult.m_strTARGETBEDID_CHR = dtbResult.Rows[0]["TARGETBEDID_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["TYPE_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intTYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["TYPE_INT"].ToString().Trim());
                    }
                    p_objResult.m_strDES_VCHR = dtbResult.Rows[0]["DES_VCHR"].ToString().Trim();
                    p_objResult.m_strOPERATORID_CHR = dtbResult.Rows[0]["OPERATORID_CHR"].ToString().Trim();
                    p_objResult.m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["MODIFY_DAT"] != System.DBNull.Value)
                    {
                        p_objResult.m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[0]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
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

        #region ����סԺ������¼	glzhang		2005.09.14
        /// <summary>
        /// ����סԺ������¼	glzhang		2005.09.14
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">id</param>
        /// <param name="p_strFilter">��ѯ�ַ���</param>
        /// <param name="p_objResultArr">�����</param>
        /// <param name="p_intFlag">������־:0=����סԺ��¼,����סԺ��¼</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBinTransferByRegisterID(string p_strRegisterid_chr, string p_strFilter, out clsT_Opr_Bih_Transfer_VO[] p_objResultArr, int p_intFlag)
        {
            p_objResultArr = new clsT_Opr_Bih_Transfer_VO[0];
            long lngRes = 0;

            string strSQL = @"SELECT   a.registerid_chr, a.transferid_chr, a.sourcedeptid_chr,
         s_dep.deptname_vchr sourcedeptname, a.sourceareaid_chr,
         s_area.deptname_vchr sourceareaname, a.sourcebedid_chr,
         s_bed.code_chr sourcebedno, a.targetdeptid_chr,
         t_dep.deptname_vchr targetdeptname, a.targetareaid_chr,
         t_area.deptname_vchr targetareaname, a.targetbedid_chr,
         g_bed.code_chr targetbedno, a.type_int, d.flgname_vchr AS typename,
         a.des_vchr, a.operatorid_chr, c.lastname_vchr operatorname,
         CASE
            WHEN a.type_int = 5
               THEN b.inpatient_dat
            ELSE a.modify_dat
         END AS modify_dat,
         b.inpatientcount_int
    FROM t_opr_bih_transfer a,
         t_opr_bih_register b,
         t_bse_employee c,
         t_sys_flg_table d,
         t_bse_deptdesc s_dep,
         t_bse_deptdesc s_area,
         t_bse_deptdesc t_dep,
         t_bse_deptdesc t_area,
         t_bse_bed s_bed,
         t_bse_bed g_bed
   WHERE a.registerid_chr = b.registerid_chr(+)
     AND a.operatorid_chr = c.empid_chr
     AND a.sourcedeptid_chr = s_dep.deptid_chr(+)
     AND a.sourceareaid_chr = s_area.deptid_chr(+)
     AND a.sourcebedid_chr = s_bed.bedid_chr(+)
     AND a.targetdeptid_chr = t_dep.deptid_chr(+)
     AND a.targetareaid_chr = t_area.deptid_chr(+)
     AND a.targetbedid_chr = g_bed.bedid_chr(+)
     AND a.type_int = d.flg_int
     AND d.tablename_vchr = 't_opr_bih_transfer'
     AND d.columnseq_int = 7
     " + p_strFilter;
            if (p_intFlag == 0)
            {
                strSQL += "     AND a.registerid_chr = '" + p_strRegisterid_chr + "'";
            }
            else
            {
                strSQL += "     AND b.inpatientid_chr = '" + p_strRegisterid_chr + "'";
            }
            strSQL += " ORDER BY b.inpatientcount_int, modify_dat";


            /* @create by wjqin (05-11-25)
                         * ���SQL SERVER��strSQl�汾���
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT   a.registerid_chr, a.transferid_chr, a.sourcedeptid_chr,
         s_dep.deptname_vchr sourcedeptname, a.sourceareaid_chr,
         s_area.deptname_vchr sourceareaname, a.sourcebedid_chr,
         s_bed.code_chr sourcebedno, a.targetdeptid_chr,
         t_dep.deptname_vchr targetdeptname, a.targetareaid_chr,
         t_area.deptname_vchr targetareaname, a.targetbedid_chr,
         g_bed.code_chr targetbedno, a.type_int,
        ( CASE a.type_int WHEN 1 THEN 'ת��' WHEN 2 THEN '����' WHEN 3 THEN 'ת��' WHEN 4 THEN  '��Ժ�ٻ�' WHEN 5 THEN  '��Ժ' WHEN 6 THEN  '��Ժ' ELSE  '' END) typename,
         a.des_vchr, a.operatorid_chr, c.lastname_vchr operatorname,
         CASE
            WHEN a.type_int = 5
               THEN b.inpatient_dat
            ELSE a.modify_dat
         END AS modify_dat,
         b.inpatientcount_int
    FROM t_opr_bih_transfer a left join t_opr_bih_register b on a.registerid_chr = b.registerid_chr 
         full join  t_bse_employee c on a.operatorid_chr = c.empid_chr
         left join  t_bse_deptdesc s_dep on a.sourcedeptid_chr = s_dep.deptid_chr
         left join  t_bse_deptdesc s_area on  a.sourceareaid_chr = s_area.deptid_chr
         left join  t_bse_deptdesc t_dep on a.targetdeptid_chr = t_dep.deptid_chr
         left join  t_bse_deptdesc t_area on a.targetareaid_chr = t_area.deptid_chr
         left join  t_bse_bed      s_bed  on a.sourcebedid_chr = s_bed.bedid_chr
         left join  t_bse_bed      g_bed  on a.targetbedid_chr = g_bed.bedid_chr ";
                strSQL += p_strFilter;
                if (p_intFlag == 0)
                {
                    strSQL += "     AND a.registerid_chr = '" + p_strRegisterid_chr + "'";
                }
                else
                {
                    strSQL += "     AND b.inpatientid_chr = '" + p_strRegisterid_chr + "'";
                }
                strSQL += " ORDER BY b.inpatientcount_int, modify_dat";

            }
            /* <<======================================= */
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Opr_Bih_Transfer_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Opr_Bih_Transfer_VO();
                        //��ˮ��
                        p_objResultArr[i1].m_strTRANSFERID_CHR = dtbResult.Rows[i1]["TRANSFERID_CHR"].ToString().Trim();
                        //Դ����id
                        p_objResultArr[i1].m_strSOURCEDEPTID_CHR = dtbResult.Rows[i1]["SOURCEDEPTID_CHR"].ToString().Trim();
                        //Դ��������
                        if (dtbResult.Rows[i1]["SOURCEDEPTNAME"] != null)
                        {
                            p_objResultArr[i1].m_strSourceDeptName = dtbResult.Rows[i1]["SOURCEDEPTNAME"].ToString().Trim();
                        }
                        //Դ����id
                        p_objResultArr[i1].m_strSOURCEAREAID_CHR = dtbResult.Rows[i1]["SOURCEAREAID_CHR"].ToString().Trim();
                        //Դ��������
                        if (dtbResult.Rows[i1]["SOURCEAREANAME"] != null)
                        {
                            p_objResultArr[i1].m_strSourceAreaName = dtbResult.Rows[i1]["SOURCEAREANAME"].ToString().Trim();
                        }
                        //Դ����id
                        p_objResultArr[i1].m_strSOURCEBEDID_CHR = dtbResult.Rows[i1]["SOURCEBEDID_CHR"].ToString().Trim();
                        //Դ���� [���ֶ�]
                        if (dtbResult.Rows[i1]["SourceBedNo"] != null)
                        {
                            p_objResultArr[i1].m_strSourceBedNo = dtbResult.Rows[i1]["SourceBedNo"].ToString().Trim();
                        }
                        //Ŀ�����id
                        p_objResultArr[i1].m_strTARGETDEPTID_CHR = dtbResult.Rows[i1]["TARGETDEPTID_CHR"].ToString().Trim();
                        //Ŀ���������
                        if (dtbResult.Rows[i1]["TARGETDEPTNAME"] != null)
                        {
                            p_objResultArr[i1].m_strTargetDeptName = dtbResult.Rows[i1]["TARGETDEPTNAME"].ToString().Trim();
                        }
                        //Ŀ�겡��id
                        p_objResultArr[i1].m_strTARGETAREAID_CHR = dtbResult.Rows[i1]["TARGETAREAID_CHR"].ToString().Trim();
                        //Ŀ�겡������
                        if (dtbResult.Rows[i1]["TARGETAREANAME"] != null)
                        {
                            p_objResultArr[i1].m_strTargetAreaName = dtbResult.Rows[i1]["TARGETAREANAME"].ToString().Trim();
                        }
                        //Ŀ�겡��id
                        p_objResultArr[i1].m_strTARGETBEDID_CHR = dtbResult.Rows[i1]["TARGETBEDID_CHR"].ToString().Trim();
                        //Ŀ�괲�� [���ֶ�]
                        if (dtbResult.Rows[i1]["TargetBedNo"] != null)
                        {
                            p_objResultArr[i1].m_strTargetBedNo = dtbResult.Rows[i1]["TargetBedNo"].ToString().Trim();
                        }
                        //��������{1=ת��;2=����;3=ת��+����;4=��Ժ����;5=��Ժ;6=��Ժ}
                        p_objResultArr[i1].m_intTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["TYPE_INT"].ToString());
                        //������������
                        if (dtbResult.Rows[i1]["TypeName"] != null)
                        {
                            p_objResultArr[i1].m_strTypeName = dtbResult.Rows[i1]["TypeName"].ToString().Trim();
                        }
                        //��ע
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        //������ID
                        p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        ///����������
                        if (dtbResult.Rows[i1]["OPERATORNAME"] != null)
                        {
                            p_objResultArr[i1].m_strOperatorName = dtbResult.Rows[i1]["OPERATORNAME"].ToString().Trim();
                        }
                        //��Ժ�Ǽ���ˮ��(200409010001)
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        //�޸����ڣ���������
                        p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        //��Ժ����
                        p_objResultArr[i1].m_strINPATIENTCOUNT_INT = dtbResult.Rows[i1]["inpatientcount_int"].ToString().Trim();
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

        //���˻�����Ϣ
        #region ����
        #region ���Ӳ��˻�������
        /// <summary>
        /// ���Ӳ��˻�������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPatient(out string p_strRecordID, clsPatient_VO p_objRecord)
        {
            long lngRes = 0;
            clsPatientSvc objSvc = new clsPatientSvc();
            lngRes = objSvc.m_lngAddNewPatient(out p_strRecordID, p_objRecord);
            return lngRes;
        }
        #endregion

        #region ���Ӳ��˻�������������
        /// <summary>
        /// ���Ӳ��˻�������������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPatientIdx(out string p_strRecordID, clsclsPatientIdxVO p_objRecord)
        {
            long lngRes = 0;
            clsPatientSvc objSvc = new clsPatientSvc();
            lngRes = objSvc.m_lngAddNewPatientIndexInfo(out p_strRecordID, p_objRecord);
            return lngRes;
        }
        #endregion
        #endregion
        #region �޸�
        #region  �޸Ĳ��˻�����Ϣ
        /// <summary>
        /// �޸Ĳ��˻�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyPatient(clsPatient_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"UPDATE t_bse_patient
   SET lastname_vchr = ?,
       idcard_chr = ?,
       married_chr = ?,
       birthplace_vchr = ?,
       homeaddress_vchr = ?,
       sex_chr = ?,
       nationality_vchr = ?,
       firstname_vchr = ?,
       birth_dat = ?,
       race_vchr = ?,
       nativeplace_vchr = ?,
       occupation_vchr = ?,
       name_vchr = ?,
       homephone_vchr = ?,
       officephone_vchr = ?,
       insuranceid_vchr = ?,
       mobile_chr = ?,
       inpatientid_chr = ?,
       officeaddress_vchr = ?,
       employer_vchr = ?,
       officepc_vchr = ?,
       homepc_chr = ?,
       email_vchr = ?,
       contactpersonfirstname_vchr = ?,
       contactpersonlastname_vchr = ?,
       contactpersonaddress_vchr = ?,
       contactpersonphone_vchr = ?,
       contactpersonpc_chr = ?,
       patientrelation_vchr = ?,
       firstdate_dat = ?,
       isemployee_int = ?,
       status_int = ?,
       operatorid_chr = ?,
       modify_dat = ?,
       paytypeid_chr = ?,
       govcard_chr = ?,
       bloodtype_chr = ?,
       ifallergic_int = ?,
       allergicdesc_vchr = ?,
       inpatienttempid_vchr = ?,
       consigneeaddr = ? 
 WHERE patientid_chr = ?";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(42, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strLASTNAME_VCHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strIDCARD_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strMARRIED_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strBIRTHPLACE_VCHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strHOMEADDRESS_VCHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strSEX_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strNATIONALITY_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strFIRSTNAME_VCHR;
                objLisAddItemRefArr[8].Value = Convert.ToDateTime(p_objRecord.m_strBIRTH_DAT);
                objLisAddItemRefArr[9].Value = p_objRecord.m_strRACE_VCHR;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strNATIVEPLACE_VCHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strOCCUPATION_VCHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strNAME_VCHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strHOMEPHONE_VCHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strOFFICEPHONE_VCHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strINSURANCEID_VCHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strMOBILE_CHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strINPATIENTID_CHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strOFFICEADDRESS_VCHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strEMPLOYER_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strOFFICEPC_VCHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strHOMEPC_CHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strEMAIL_VCHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strCONTACTPERSONFIRSTNAME_VCHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strCONTACTPERSONFIRSTNAME_VCHR;// p_objRecord.m_strCONTACTPERSONLASTNAME_VCHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strCONTACTPERSONADDRESS_VCHR;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strCONTACTPERSONPHONE_VCHR;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strCONTACTPERSONPC_CHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strPATIENTRELATION_VCHR;
                objLisAddItemRefArr[29].Value = Convert.ToDateTime(p_objRecord.m_strFIRSTDATE_DAT);
                objLisAddItemRefArr[30].Value = p_objRecord.m_intISEMPLOYEE_INT;
                objLisAddItemRefArr[31].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strOPERATORID_CHR;
                objLisAddItemRefArr[33].Value = Convert.ToDateTime(p_objRecord.m_strMODIFY_DAT);
                objLisAddItemRefArr[34].Value = p_objRecord.m_strPAYTYPEID_CHR;
                objLisAddItemRefArr[35].Value = p_objRecord.m_strGOVCARD_CHR;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strBLOODTYPE_CHR;
                objLisAddItemRefArr[37].Value = p_objRecord.m_intIFALLERGIC_INT;
                objLisAddItemRefArr[38].Value = p_objRecord.m_strALLERGICDESC_VCHR;
                objLisAddItemRefArr[39].Value = p_objRecord.m_strINPATIENTTEMPID_VCHR;
                objLisAddItemRefArr[40].Value = p_objRecord.ConsigneeAddr;
                objLisAddItemRefArr[41].Value = p_objRecord.m_strPATIENTID_CHR;
                objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
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

        #region  �޸Ĳ��˻�������������
        /// <summary>
        /// �޸Ĳ��˻�������������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyPatientIdx(clsclsPatientIdxVO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"UPDATE t_bse_patientidx
   SET inpatientid_chr = ?,
       idcard_chr = ?,
       homeaddress_vchr = ?,
       sex_chr = ?,
       birth_dat = ?,
       name_vchr = ?,
       homephone_vchr = ?,
       insuranceid_vchr = ?
 WHERE patientid_chr = ?";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(9, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strINPATIENTID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strIDCARD_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strHOMEADDRESS_VCHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strSEX_CHR;
                objLisAddItemRefArr[4].Value = Convert.ToDateTime(p_objRecord.m_strBIRTH_DAT);
                objLisAddItemRefArr[5].Value = p_objRecord.m_strNAME_VCHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strHOMEPHONE_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strINSURANCEID_VCHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strPATIENTID_CHR;

                objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
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
        #endregion
        #region ��ȡְԱ����
        /// <summary>
        /// ��ȡְԱ����	������ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_strName">ְ������	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeNameByID(string p_strID, out string p_strName)
        {
            p_strName = "";
            long lngRes = 0;
            string strSQL = "select LastName_vchr FROM T_BSE_EMPLOYEE WHERE trim(empid_chr) ='" + p_strID.Trim() + "'";

            /* @create by wjqin (05-11-25)
                         * ���SQL SERVER��strSQl�汾���
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = "select LastName_vchr FROM T_BSE_EMPLOYEE WHERE Ltrim(Rtrim(empid_chr)) ='" + p_strID.Trim() + "'";

            }
            /* <<======================================= */

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_strName = dtbResult.Rows[0][0].ToString();
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

        //�����������߼�����������ʵ�֡�
        #region ��Ժ�Ǽ�
        /// <summary>
        /// ��Ժ�Ǽ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="intBihState">סԺ״̬[1���״���Ժ����2��Ժ����3���ٴ���Ժ��]</param>
        /// <param name="objPatientVO">clsPatient_VO ����</param>
        /// <param name="objBIHVO">clsT_Opr_Bih_Register_VO ����</param>
        /// <returns></returns>
        /// <remarks>
        /// ע�⣺
        /// 
        ///	������Ϣ�����������˱�Ų�����		�����˱����ϵͳ���ɣ�
        ///	������Ϣ�޸ġ������˱�Ŵ���	
        ///		
        ///	�״���Ժ������Ժ�Ǽ���ˮ ���ǼǱ�û�м�¼				��
        ///	��Ժ	������Ժ�Ǽ���ˮ ���ǼǱ��м�¼	  ��Ժ��û�м�¼��
        ///	�ٴ���Ժ������Ժ�Ǽ���ˮ ���ǼǱ��м�¼	  ��Ժ���м�¼  �� ��Ϊ��ǰ״̬Ϊ��Ժ״̬��
        ///	
        ///	1��������Ϣ (����)		סԺ��Ϣ (�״���Ժ)		��סԺ����ϵͳ���ɣ�	
        ///	2��������Ϣ (�޸�)		סԺ��Ϣ (�״���Ժ)
        ///	3��������Ϣ (�޸�)		סԺ��Ϣ (��Ժ)
        ///	4��������Ϣ (�޸�)		סԺ��Ϣ (�ٴ���Ժ)
        /// </remarks>
        [AutoComplete]
        public long m_lngRegisterHospital(int intBihState, clsPatient_VO objPatientVO, ref clsT_Opr_Bih_Register_VO objBIHVO)
        {

            long lngReg = 0;
            clsclsPatientIdxVO objIndexVo = new clsclsPatientIdxVO();
            clsT_Opr_Bih_Transfer_VO objTransferVO = new clsT_Opr_Bih_Transfer_VO();
            clsBedManageSvc obj = new clsBedManageSvc();
            string strTem = "";
            //ȷ��������Ϣ״̬
            if (intBihState == 1 && (objPatientVO.m_strPATIENTID_CHR == null || objPatientVO.m_strPATIENTID_CHR == string.Empty))
            {
                #region ������Ϣ (����)
                //{1�������µ�סԺ�ţ�2���������Vo[clsclsPatientIdxVO][סԺ�Ÿ�ֵ]��3�����ӻ�����Ϣ������4�����ӻ�����Ϣ[סԺ�š����˱�Ÿ�ֵ]��5������סԺ�Ǽ���Ϣ��6��ռ����7�����ӵ�ת��Ϣ��}
                //1�������µ�סԺ�ţ�				
                lngReg = m_lngGetInpatientID(out strTem);
                if (objPatientVO.m_strINPATIENTID_CHR != "")
                    strTem = objPatientVO.m_strINPATIENTID_CHR;

                //2���������Vo[clsclsPatientIdxVO][סԺ�Ÿ�ֵ]��
                if (lngReg > 0)
                {
                    BaseVoToIndexVo(objPatientVO, out objIndexVo);
                    objIndexVo.m_strINPATIENTID_CHR = strTem;
                }

                //3�����ӻ�����Ϣ������ 
                if (lngReg > 0)
                {
                    strTem = "";
                    lngReg = m_lngAddNewPatientIdx(out strTem, objIndexVo);
                }

                //4�����ӻ�����Ϣ[סԺ�š����˱�Ÿ�ֵ]��
                if (lngReg > 0)
                {
                    objPatientVO.m_strPATIENTID_CHR = strTem;
                    objPatientVO.m_strINPATIENTID_CHR = objIndexVo.m_strINPATIENTID_CHR;
                    lngReg = m_lngAddNewPatient(out strTem, objPatientVO);
                }

                //5������סԺ�Ǽ���Ϣ��
                if (lngReg > 0)
                {
                    objBIHVO.m_strPATIENTID_CHR = objPatientVO.m_strPATIENTID_CHR;
                    objBIHVO.m_strINPATIENTID_CHR = objIndexVo.m_strINPATIENTID_CHR;
                    objBIHVO.m_intINPATIENTCOUNT_INT += 1;
                    lngReg = m_lngAddNewBihRegister(out strTem, objBIHVO);
                    objBIHVO.m_strREGISTERID_CHR = strTem;
                }

                //6��ռ����
                if (lngReg > 0)
                {
                    if (objBIHVO.m_strBEDID_CHR != null && objBIHVO.m_strBEDID_CHR != string.Empty)
                    {
                        lngReg = obj.m_lngModifyBedByBedID(objBIHVO.m_strBEDID_CHR, 2);
                    }
                }

                //7�����ӵ�ת��Ϣ��
                FillTransfer_VO(objBIHVO, out objTransferVO);
                if (lngReg > 0)
                {
                    lngReg = m_lngAddNewBinTransfer(out strTem, objTransferVO);
                }
                #endregion
            }
            else
            {
                #region ������Ϣ (�޸�)
                switch (intBihState)
                {
                    case 1:
                        #region �״���Ժ��
                        //{1�������µ�סԺ�ţ�2���������Vo[clsclsPatientIdxVO][סԺ�Ÿ�ֵ]��3���޸Ļ�����Ϣ������4���޸Ļ�����Ϣ[סԺ�Ÿ�ֵ]��5������סԺ�Ǽ���Ϣ[סԺ�Ÿ�ֵ]��6��ռ����7�����ӵ�ת��Ϣ��}
                        //1�������µ�סԺ�ţ�
                        lngReg = m_lngGetInpatientID(out strTem);

                        //2���������Vo[clsclsPatientIdxVO][סԺ�Ÿ�ֵ]��
                        if (objPatientVO.m_strINPATIENTID_CHR != "")
                            strTem = objPatientVO.m_strINPATIENTID_CHR;
                        if (lngReg > 0)
                        {
                            BaseVoToIndexVo(objPatientVO, out objIndexVo);
                            objIndexVo.m_strINPATIENTID_CHR = strTem;
                        }

                        //3���޸Ļ�����Ϣ������
                        if (lngReg > 0)
                        {
                            lngReg = m_lngModifyPatientIdx(objIndexVo);
                        }

                        //4���޸Ļ�����Ϣ[סԺ�Ÿ�ֵ]��
                        if (lngReg > 0)
                        {
                            objPatientVO.m_strINPATIENTID_CHR = objIndexVo.m_strINPATIENTID_CHR;
                            lngReg = m_lngModifyPatient(objPatientVO);
                        }

                        //5������סԺ�Ǽ���Ϣ[סԺ�Ÿ�ֵ]��
                        if (lngReg > 0)
                        {
                            objBIHVO.m_strPATIENTID_CHR = objPatientVO.m_strPATIENTID_CHR;
                            objBIHVO.m_strINPATIENTID_CHR = objIndexVo.m_strINPATIENTID_CHR;
                            objBIHVO.m_intINPATIENTCOUNT_INT += 1;
                            lngReg = m_lngAddNewBihRegister(out strTem, objBIHVO);
                            objBIHVO.m_strREGISTERID_CHR = strTem;
                        }

                        //6��ռ����
                        if (lngReg > 0)
                        {
                            if (objBIHVO.m_strBEDID_CHR != null && objBIHVO.m_strBEDID_CHR != string.Empty)
                            {
                                lngReg = obj.m_lngModifyBedByBedID(objBIHVO.m_strBEDID_CHR, 2);
                            }
                        }

                        //7�����ӵ�ת��Ϣ��
                        FillTransfer_VO(objBIHVO, out objTransferVO);
                        if (lngReg > 0)
                        {
                            lngReg = m_lngAddNewBinTransfer(out strTem, objTransferVO);
                        }
                        #endregion
                        break;
                    case 2:
                        #region ��Ժ	{�����޸�[��Ժ���ҡ�����������]  ע������û�п��ƣ�ֻ���ڱ�ʾ���ϴ���}
                        //{1���������Vo[clsclsPatientIdxVO]��2���޸Ļ�����Ϣ������3���޸Ļ�����Ϣ��4���޸�סԺ�Ǽ���Ϣ��}
                        //1���������Vo[clsclsPatientIdxVO]��
                        BaseVoToIndexVo(objPatientVO, out objIndexVo);

                        //2���޸Ļ�����Ϣ������
                        lngReg = m_lngModifyPatientIdx(objIndexVo);

                        //3���޸Ļ�����Ϣ��
                        if (lngReg > 0)
                        {
                            lngReg = m_lngModifyPatient(objPatientVO);
                        }

                        //4���޸�סԺ�Ǽ���Ϣ��
                        if (lngReg > 0)
                        {
                            lngReg = m_lngModifyBihRegisterInfoByVo(objBIHVO.m_strREGISTERID_CHR, objBIHVO);
                        }
                        //5 �޸ĵ�ת��Ϣ��
                        if (lngReg > 0)
                        {
                            lngReg = this.m_lngUpdateTransferInfo(objBIHVO.m_strREGISTERID_CHR);
                        }
                        #endregion
                        break;
                    case 3:
                        #region �ٴ���Ժ
                        //{1���������Vo[clsclsPatientIdxVO]��2���޸Ļ�����Ϣ������3���޸Ļ�����Ϣ��4������סԺ�Ǽ���Ϣ��5��ռ����6�����ӵ�ת��Ϣ��}
                        //1���������Vo[clsclsPatientIdxVO]��
                        BaseVoToIndexVo(objPatientVO, out objIndexVo);

                        //2���޸Ļ�����Ϣ������
                        lngReg = m_lngModifyPatientIdx(objIndexVo);

                        //3���޸Ļ�����Ϣ��
                        if (lngReg > 0)
                        {
                            lngReg = m_lngModifyPatient(objPatientVO);
                        }

                        //4������סԺ�Ǽ���Ϣ��
                        if (lngReg > 0)
                        {
                            objBIHVO.m_intINPATIENTCOUNT_INT += 1;
                            lngReg = m_lngAddNewBihRegister(out strTem, objBIHVO);
                            objBIHVO.m_strREGISTERID_CHR = strTem;
                        }

                        //5��ռ����
                        if (lngReg > 0)
                        {
                            if (objBIHVO.m_strBEDID_CHR != null && objBIHVO.m_strBEDID_CHR != string.Empty)
                            {
                                lngReg = obj.m_lngModifyBedByBedID(objBIHVO.m_strBEDID_CHR, 2);
                            }
                        }

                        //6�����ӵ�ת��Ϣ��
                        FillTransfer_VO(objBIHVO, out objTransferVO);
                        if (lngReg > 0)
                        {
                            lngReg = m_lngAddNewBinTransfer(out strTem, objTransferVO);
                        }
                        #endregion
                        break;
                }
                #endregion
            }

            if (lngReg <= 0)
            {
                throw new Exception("����ʧ�ܣ�");
            }
            return lngReg;
        }
        /// <summary>
        /// �������Vo
        /// </summary>
        /// <param name="objPatientVO">[clsPatient_VO]</param>
        /// <param name="objIndexVo">[clsclsPatientIdxVO]</param>
        [AutoComplete]
        public void BaseVoToIndexVo(clsPatient_VO objPatientVO, out clsclsPatientIdxVO objIndexVo)
        {
            objIndexVo = new clsclsPatientIdxVO();
            //���˱��
            objIndexVo.m_strPATIENTID_CHR = objPatientVO.m_strPATIENTID_CHR;
            //סԺ���
            objIndexVo.m_strINPATIENTID_CHR = objPatientVO.m_strINPATIENTID_CHR;
            //���֤��
            objIndexVo.m_strIDCARD_CHR = objPatientVO.m_strIDCARD_CHR;
            //��ͥסַ
            objIndexVo.m_strHOMEADDRESS_VCHR = objPatientVO.m_strHOMEADDRESS_VCHR;
            //�Ա�
            objIndexVo.m_strSEX_CHR = objPatientVO.m_strSEX_CHR;
            //��������
            objIndexVo.m_strBIRTH_DAT = objPatientVO.m_strBIRTH_DAT;
            //��������
            objIndexVo.m_strNAME_VCHR = objPatientVO.m_strNAME_VCHR;
            //��ϵ�绰
            objIndexVo.m_strHOMEPHONE_VCHR = objPatientVO.m_strHOMEPHONE_VCHR;
            //ҽ�����
            objIndexVo.m_strINSURANCEID_VCHR = objPatientVO.m_strINSURANCEID_VCHR;
        }
        [AutoComplete]
        public void FillTransfer_VO(clsT_Opr_Bih_Register_VO objBIHVO, out clsT_Opr_Bih_Transfer_VO objPatientVO)
        {
            objPatientVO = new clsT_Opr_Bih_Transfer_VO();
            //��������{1=ת��;2=����;3=ת��+����;4=��Ժ����;5=��Ժ;6=��Ժ}
            objPatientVO.m_intTYPE_INT = 5;
            objPatientVO.m_strDES_VCHR = null;
            objPatientVO.m_strMODIFY_DAT = objBIHVO.m_strMODIFY_DAT;
            objPatientVO.m_strOPERATORID_CHR = objBIHVO.m_strOPERATORID_CHR;
            objPatientVO.m_strREGISTERID_CHR = objBIHVO.m_strREGISTERID_CHR;
            objPatientVO.m_strSOURCEAREAID_CHR = null;
            objPatientVO.m_strSOURCEBEDID_CHR = null;
            objPatientVO.m_strSOURCEDEPTID_CHR = null;
            objPatientVO.m_strTARGETAREAID_CHR = objBIHVO.m_strAREAID_CHR;
            objPatientVO.m_strTARGETBEDID_CHR = objBIHVO.m_strBEDID_CHR;
            objPatientVO.m_strTARGETDEPTID_CHR = objBIHVO.m_strDEPTID_CHR;
            objPatientVO.m_strTRANSFERID_CHR = null;
        }
        #endregion
        #region ��ת
        /// <summary>
        /// ��ת
        /// {1���ճ�ԭ���Ĵ�λ��2��ռ����ת�Ĵ�λ��3�����ӵ�ת��¼��4���޸���Ժ�ǼǵĲ�����Ϣ��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objPatientVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngTransferInHospital(clsT_Opr_Bih_Transfer_VO objPatientVO)
        {
            long lngReg = 0;
            //1���ճ�ԭ���Ĵ�λ��
            clsBedManageSvc obj = new clsBedManageSvc();
            if (lngReg > 0)
            {
                if (objPatientVO.m_strSOURCEBEDID_CHR != null && objPatientVO.m_strSOURCEBEDID_CHR != string.Empty && objPatientVO.m_intTYPE_INT != 3)
                {
                    lngReg = obj.m_lngModifyBedByBedID(objPatientVO.m_strSOURCEBEDID_CHR, 1);
                    if (objPatientVO.m_intTYPE_INT == 1)
                    {
                        objPatientVO.m_intTYPE_INT = 3;
                    }
                }
            }

            //2��ռ����ת�Ĵ�λ��
            if (lngReg > 0)
            {
                if (objPatientVO.m_strTARGETBEDID_CHR != null && objPatientVO.m_strTARGETBEDID_CHR != string.Empty)
                {
                    //��С���˵İ���
                    if (objPatientVO.m_intTYPE_INT != 2)
                    {
                        lngReg = new clsBedManageSvc().m_lngDelPatientOccupyBedByRegisterID(objPatientVO.m_strREGISTERID_CHR);
                    }
                    if (lngReg > 0)
                    {
                        lngReg = obj.m_lngModifyBedByBedID(objPatientVO.m_strTARGETBEDID_CHR, 2);
                    }
                }
            }

            //3�����ӵ�ת��¼��
            if (lngReg > 0)
            {
                string strRecordID = "";
                lngReg = m_lngAddNewBinTransfer(out strRecordID, objPatientVO);
                //				#region ������־ͳ��
                //				lngReg = new clsBihStatQuerySvc().m_lngUpdateSickRoomLOGSTAT(objPatientVO);
                //				#endregion
            }

            //4���޸���Ժ�ǼǵĲ�����Ϣ��
            if (lngReg > 0)
            {
                //{0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ}
                int intPSTATUS_INT = 0;
                if (objPatientVO.m_strTARGETBEDID_CHR != null && objPatientVO.m_strTARGETBEDID_CHR != string.Empty)
                {
                    intPSTATUS_INT = 1;
                }
                /* change by wjqin(06-3-6) */

                //	lngReg =m_lngModifyBedInfoByRegisterID(p_objPrincipal ,objPatientVO.m_strREGISTERID_CHR ,objPatientVO.m_strTARGETDEPTID_CHR.Trim() ,objPatientVO.m_strTARGETAREAID_CHR.Trim() ,objPatientVO.m_strTARGETBEDID_CHR.Trim() ,intPSTATUS_INT,objPatientVO.m_strOPERATORID_CHR,objPatientVO.m_strMODIFY_DAT);
                /*-------------------------------------------------------------------->*/
                lngReg = m_lngModifyBedInfoByRegisterID(objPatientVO.m_strREGISTERID_CHR, objPatientVO.m_strTARGETDEPTID_CHR, objPatientVO.m_strTARGETAREAID_CHR.Trim(), objPatientVO.m_strTARGETBEDID_CHR.Trim(), intPSTATUS_INT, objPatientVO.m_strOPERATORID_CHR, objPatientVO.m_strMODIFY_DAT);
                /*<---------------------------------------------------------------------*/
            }

            if (lngReg <= 0)
            {
                throw new Exception("��ת����ʧ�ܣ�");
            }
            return lngReg;
        }
        #endregion
        #region ��ת
        /// <summary>
        /// ��ת
        /// {1���ճ�ԭ���Ĵ�λ��2��ռ����ת�Ĵ�λ��3�����ӵ�ת��¼��4���޸���Ժ�ǼǵĲ�����Ϣ��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objPatientVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngTransferInHospital(clsT_Opr_Bih_Transfer_VO objPatientVO, string p_strCASEDOCTOR_CHR)
        {
            long lngReg = 0;
            //1���ճ�ԭ���Ĵ�λ��
            clsBedManageSvc obj = new clsBedManageSvc();
            if (lngReg > 0)
            {
                //1���ճ�ԭ���Ĵ�λ��
                if (objPatientVO.m_strSOURCEBEDID_CHR != null && objPatientVO.m_strSOURCEBEDID_CHR != string.Empty && objPatientVO.m_intTYPE_INT != 3)
                {
                    lngReg = obj.m_lngModifyBedByBedID(objPatientVO.m_strSOURCEBEDID_CHR, 1);
                    if (objPatientVO.m_intTYPE_INT == 1)
                    {
                        objPatientVO.m_intTYPE_INT = 3;
                    }
                }
            }

            //2��ռ����ת�Ĵ�λ��
            if (lngReg > 0)
            {
                if (objPatientVO.m_strTARGETBEDID_CHR != null && objPatientVO.m_strTARGETBEDID_CHR != string.Empty)
                {
                    //��С���˵İ���
                    if (objPatientVO.m_intTYPE_INT != 2)
                    {
                        lngReg = new clsBedManageSvc().m_lngDelPatientOccupyBedByRegisterID(objPatientVO.m_strREGISTERID_CHR);
                    }
                    if (lngReg > 0)
                    {
                        lngReg = obj.m_lngModifyBedByBedID(objPatientVO.m_strTARGETBEDID_CHR, 2);
                    }
                }
            }

            //3�����ӵ�ת��¼��
            if (lngReg > 0)
            {
                string strRecordID = "";
                lngReg = m_lngAddNewBinTransfer(out strRecordID, objPatientVO);
                //				#region ������־ͳ��
                //				lngReg = new clsBihStatQuerySvc().m_lngUpdateSickRoomLOGSTAT(objPatientVO);
                //				#endregion
            }

            //4���޸���Ժ�ǼǵĲ�����Ϣ��
            if (lngReg > 0)
            {
                //{0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ}
                int intPSTATUS_INT = 0;
                if (objPatientVO.m_strTARGETBEDID_CHR != null && objPatientVO.m_strTARGETBEDID_CHR != string.Empty)
                {
                    intPSTATUS_INT = 1;
                }
                lngReg = m_lngModifyBedInfoByRegisterID(objPatientVO.m_strREGISTERID_CHR, objPatientVO.m_strTARGETDEPTID_CHR, objPatientVO.m_strTARGETAREAID_CHR, objPatientVO.m_strTARGETBEDID_CHR, intPSTATUS_INT, objPatientVO.m_strOPERATORID_CHR, objPatientVO.m_strMODIFY_DAT, p_strCASEDOCTOR_CHR);
            }

            if (lngReg <= 0)
            {
                throw new Exception("��ת����ʧ�ܣ�");
            }
            return lngReg;
        }
        #endregion
        #region ��Ժ
        /// <summary>
        /// ��Ժ
        /// {1���ճ���λ��2������һ����Ժ��¼��3�����ӵ�ת��¼��4���޸���Ժ�Ǽ���Ժ״̬��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objPrincipal"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngLeaveHospital(clsT_Opr_Bih_Leave_VO objPatientVO)
        {
            long lngReg = 0;
            //��С���˵İ���
            lngReg = new clsBedManageSvc().m_lngDelPatientOccupyBedByRegisterID(objPatientVO.m_strREGISTERID_CHR);
            if (lngReg > 0)
            {
                //1���ճ���λ��
                if (objPatientVO.m_intPSTATUS_INT == 1)
                {
                    clsBedManageSvc obj = new clsBedManageSvc();
                    if (objPatientVO.m_strOUTBEDID_CHR != null && objPatientVO.m_strOUTBEDID_CHR != string.Empty)
                    {
                        if (objPatientVO.m_strOUTBEDID_CHR != null && objPatientVO.m_strOUTBEDID_CHR != string.Empty)
                        {
                            lngReg = obj.m_lngModifyBedByBedID(objPatientVO.m_strOUTBEDID_CHR, 1);
                        }
                    }
                }
            }

            //2������һ����Ժ��¼��
            if (lngReg > 0 && objPatientVO.m_intPSTATUS_INT == 1)
            {
                string strRecordID = "";
                lngReg = m_lngAddNewBihLeave(out strRecordID, objPatientVO);
            }

            //3�����ӵ�ת��¼��
            string strTem = "";
            clsT_Opr_Bih_Transfer_VO objTransferVO = new clsT_Opr_Bih_Transfer_VO();
            FillTransfer_VO1(objPatientVO, out objTransferVO);
            objTransferVO.m_strMODIFY_DAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (lngReg > 0 && objPatientVO.m_intPSTATUS_INT == 1)
            {
                lngReg = m_lngAddNewBinTransfer(out strTem, objTransferVO);
            }
            if (lngReg > 0 && objPatientVO.m_intPSTATUS_INT == 0)
            {
                objTransferVO.m_intTYPE_INT = 7;
                lngReg = m_lngAddNewBinTransfer(out strTem, objTransferVO);
            }

            //4���޸���Ժ�Ǽ���Ժ״̬
            if (lngReg > 0)
            {
                lngReg = m_lngModifyBihRegisterPSTATUS_INTByRegisterID(objPatientVO.m_strREGISTERID_CHR, objPatientVO.m_intPSTATUS_INT + 2, objPatientVO.m_strOPERATORID_CHR, objPatientVO.m_strMODIFY_DAT);
            }

            if (lngReg <= 0)
            {
                throw new Exception("��Ժ����ʧ�ܣ�");
            }
            return lngReg;
        }
        [AutoComplete]
        public void FillTransfer_VO1(clsT_Opr_Bih_Leave_VO objBIHVO, out clsT_Opr_Bih_Transfer_VO objPatientVO)
        {
            objPatientVO = new clsT_Opr_Bih_Transfer_VO();
            //��������{1=ת��;2=����;3=ת��+����;4=��Ժ����;5=��Ժ;6=��Ժ}
            objPatientVO.m_intTYPE_INT = 6;
            objPatientVO.m_strDES_VCHR = null;
            objPatientVO.m_strMODIFY_DAT = objBIHVO.m_strMODIFY_DAT;
            objPatientVO.m_strOPERATORID_CHR = objBIHVO.m_strOPERATORID_CHR;
            objPatientVO.m_strREGISTERID_CHR = objBIHVO.m_strREGISTERID_CHR;
            objPatientVO.m_strSOURCEAREAID_CHR = objBIHVO.m_strOUTAREAID_CHR;
            objPatientVO.m_strSOURCEBEDID_CHR = objBIHVO.m_strOUTBEDID_CHR;
            objPatientVO.m_strSOURCEDEPTID_CHR = objBIHVO.m_strOUTDEPTID_CHR;
            objPatientVO.m_strTARGETAREAID_CHR = null;
            objPatientVO.m_strTARGETBEDID_CHR = null;
            objPatientVO.m_strTARGETDEPTID_CHR = null;
            objPatientVO.m_strTRANSFERID_CHR = null;
        }
        #endregion
        #region �´�
        /// <summary>
        /// �´�
        /// {1���ճ���λ��2���޸���Ժ�ǼǼ�¼[ɾ�����Ĳ���ID������ID����λID]��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objPrincipal"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDownBed(clsT_Opr_Bih_Leave_VO objPatientVO)
        {
            long lngReg = 0;
            //			clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
            //			lngReg = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal,"com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc","m_lngLeaveHospital");
            //			if(lngReg < 0)
            //			{
            //				return -1;
            //			}
            //
            //			//1���ճ���λ��
            //			clsBedManageSvc obj =new clsBedManageSvc();
            //			if(objPatientVO.m_strOUTBEDID_CHR!=null && objPatientVO.m_strOUTBEDID_CHR!=string.Empty)
            //			{
            //				lngReg = obj.m_lngModifyBedByBedID(p_objPrincipal,objPatientVO.m_strOUTBEDID_CHR,1);
            //			}
            //
            //
            //			//����һ����Ժ��¼��
            //			if(lngReg > 0)
            //			{
            //				string strRecordID="";				 
            //				lngReg =m_lngAddNewBihLeave(p_objPrincipal,out strRecordID,objPatientVO);
            //			}
            //			
            //			if(lngReg <=0)
            //			{
            //				throw new Exception("��Ժ����ʧ�ܣ�");
            //			}
            return lngReg;
        }
        #endregion
        #region ��Ժ�ٻ�
        /// <summary>
        /// ��Ժ�ٻ�
        /// {1��ɾ����Ժ��¼��2��ռ���´�λ��3������סԺ��ת��¼��4���޸���Ժ�ǼǵĲ�����Ϣ��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objPatientVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngRecallHospital(clsT_Opr_Bih_Transfer_VO objPatientVO)
        {
            long lngReg = 0;

            //1��ɾ����Ժ��¼��
            lngReg = m_lngModifyBihLeaveByRegisterID(objPatientVO.m_strREGISTERID_CHR, 0, objPatientVO.m_strOPERATORID_CHR);

            //2��ռ���´�λ��
            if (lngReg > 0)
            {
                if (objPatientVO.m_strTARGETBEDID_CHR != null && objPatientVO.m_strTARGETBEDID_CHR != string.Empty)
                {
                    clsBedManageSvc obj = new clsBedManageSvc();
                    lngReg = obj.m_lngModifyBedByBedID(objPatientVO.m_strTARGETBEDID_CHR, 2);
                }
            }

            //3������סԺ��ת��¼��
            if (lngReg > 0)
            {
                string strRecordID = "";
                lngReg = m_lngAddNewBinTransfer(out strRecordID, objPatientVO);
            }

            //4���޸���Ժ�ǼǵĲ�����Ϣ��
            if (lngReg > 0)
            {
                //{0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ}
                int intPSTATUS_INT = 0;
                if (objPatientVO.m_strTARGETBEDID_CHR != null && objPatientVO.m_strTARGETBEDID_CHR != string.Empty)
                {
                    intPSTATUS_INT = 1;
                }
                lngReg = m_lngModifyBedInfoByRegisterID(objPatientVO.m_strREGISTERID_CHR, objPatientVO.m_strTARGETDEPTID_CHR, objPatientVO.m_strTARGETAREAID_CHR, objPatientVO.m_strTARGETBEDID_CHR, intPSTATUS_INT, objPatientVO.m_strOPERATORID_CHR, objPatientVO.m_strMODIFY_DAT);
            }

            if (lngReg <= 0)
            {
                throw new Exception("��Ժ�ٻز���ʧ�ܣ�");
            }
            return lngReg;
        }
        #endregion
        #region ����������סԺ����
        [AutoComplete]
        public long m_lngGetAreaInfo(string p_strFilter, out clsT_BSE_DEPTDESC_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_BSE_DEPTDESC_VO[0];
            long lngRes = 0;
            string strSQL = @"select deptid_chr, modify_dat, deptname_vchr, category_int,
                                   inpatientoroutpatient_int, operatorid_chr, address_vchr, pycode_chr,
                                   attributeid, parentid, createdate_dat, status_int, deactivate_dat,
                                   wbcode_chr, code_vchr, extendid_vchr, shortno_chr, stdbed_count_int,
                                   putmed_int
                              from t_bse_deptdesc";
            strSQL += p_strFilter + " ORDER BY CODE_VCHR";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_BSE_DEPTDESC_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_BSE_DEPTDESC_VO();
                        p_objResultArr[i1].m_strDEPTID_CHR = dtbResult.Rows[i1]["DEPTID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["MODIFY_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        p_objResultArr[i1].m_strDEPTNAME_VCHR = dtbResult.Rows[i1]["DEPTNAME_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["CATEGORY_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intCATEGORY_INT = Convert.ToInt32(dtbResult.Rows[i1]["CATEGORY_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["INPATIENTOROUTPATIENT_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intINPATIENTOROUTPATIENT_INT = Convert.ToInt32(dtbResult.Rows[i1]["INPATIENTOROUTPATIENT_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strADDRESS_VCHR = dtbResult.Rows[i1]["ADDRESS_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSHORTNO_CHR = dtbResult.Rows[i1]["SHORTNO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPYCODE_CHR = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strATTRIBUTEID = dtbResult.Rows[i1]["ATTRIBUTEID"].ToString().Trim();
                        p_objResultArr[i1].m_strPARENTID = dtbResult.Rows[i1]["PARENTID"].ToString().Trim();
                        if (dtbResult.Rows[i1]["CREATEDATE_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        if (dtbResult.Rows[i1]["STATUS_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["DEACTIVATE_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        p_objResultArr[i1].m_strWBCODE_CHR = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCODE_VCHR = dtbResult.Rows[i1]["CODE_VCHR"].ToString().Trim();
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
        #region ����ҽ�����������ò���
        [AutoComplete]
        public long m_mthGroupDepartment(out DataTable dt, string strID, string strEx)
        {
            long lngRes = 0;
            dt = new DataTable();
            string strSQL = @"select * from T_AID_BIH_ORDERGROUPDEPARTMENT where GROUPID_CHR ='" + strID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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

        //Add by jli in 2005-03-03
        #region ��ȡ��Ժ�ٻ�����
        [AutoComplete]
        public long m_lnggetrecalllimitdays()
        {
            long lngRes = 0;
            string strSQL = @"select leaverange_int from T_BSE_BIH_SPECORDERCATE";

            HRPService.clsHRPTableService objService = new clsHRPTableService();

            DataTable dtbres = new DataTable();

            try
            {
                lngRes = objService.DoGetDataTable(strSQL, ref dtbres);
                return int.Parse(dtbres.Rows[0]["leaverange_int"].ToString().Trim());
            }
            catch (Exception err)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(err);
                return -1;
            }
        }

        #endregion

        #region ��ȡ����Ժ����
        [AutoComplete]
        public long m_lnggetlastleavedate(string strregisterid, out DateTime dtleave)
        {
            long lngRes = 0;
            dtleave = DateTime.Now;
            if (strregisterid == null || strregisterid.Trim() == "")
            {
                return -1;
            }
            string strSQLCount = @"select count(*) as c from t_opr_bih_register where pstatus_int=3 and inpatientid_chr='" + strregisterid.Trim() + "' order by INPATIENTCOUNT_INT desc";

            string strSQL = @"select modify_dat from t_opr_bih_register where pstatus_int=3 and inpatientid_chr='" + strregisterid.Trim() + "' order by INPATIENTCOUNT_INT desc";

            HRPService.clsHRPTableService objService = new clsHRPTableService();

            DataTable dtbres = new DataTable();

            try
            {
                lngRes = objService.DoGetDataTable(strSQLCount, ref dtbres);
                if (int.Parse(dtbres.Rows[0]["c"].ToString().Trim()) == 0)
                {
                    return -1;
                }
                lngRes = objService.DoGetDataTable(strSQL, ref dtbres);
                dtleave = DateTime.Parse(dtbres.Rows[0]["modify_dat"].ToString().Trim());
                return 0;
            }
            catch (Exception err)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(err);
                return -1;
            }
        }
        #endregion
        //Add end
        #region �鿴���˳�Ժʱ�Ƿ���δͣ��ҽ��
        [AutoComplete]
        public long m_lngIshasAdvice(string p_strRegisterID, out int p_iCount)
        {
            p_iCount = 0;
            long ret = 0;
            string strSQL = @"SELECT COUNT (*) as c
  FROM t_opr_bih_order a
 WHERE ((    a.executetype_int = 1
        AND (   a.status_int = 1
             OR a.status_int = 2
             OR a.status_int = 5
            )
       )
    OR (a.executetype_int = 2 AND (a.status_int = 2 OR a.status_int = 5)))
    and a.registerid_chr = '" + p_strRegisterID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                ret = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (ret > 0 && int.Parse(dtbResult.Rows[0]["c"].ToString()) > 0)
                {
                    p_iCount = int.Parse(dtbResult.Rows[0]["c"].ToString());
                }
                else
                {
                    ret = 0;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return ret;
        }
        #endregion
        #region �޸ĵ�ת��Ϣ{����,ʱ��}
        public long m_lngUpdateTransferInfo(string p_strRegisterid)
        {
            long lngRes = 0;
            DataTable dtbResult = null;
            string strTransferID = "";
            lngRes = new clsBedManageSvc().m_lngGetPatientLastestTransferInfo(p_strRegisterid, out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                strTransferID = dtbResult.Rows[0]["TRANSFERID_CHR"].ToString();
            }
            else
            {
                return -1;
            }
            string strSQL = @"UPDATE t_opr_bih_transfer
   SET (targetareaid_chr, modify_dat) = (SELECT areaid_chr, modify_dat
                                           FROM t_opr_bih_register
                                          WHERE registerid_chr = '" + p_strRegisterid + @"')
 WHERE transferid_chr = '" + strTransferID + "'";

            /* @update by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */

            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"UPDATE t_opr_bih_transfer
SET targetareaid_chr = (SELECT  top 1 areaid_chr  FROM t_opr_bih_register  WHERE registerid_chr = '" + p_strRegisterid + @"'),
modify_dat = (SELECT  top 1 modify_dat  FROM t_opr_bih_register  WHERE registerid_chr =  '" + p_strRegisterid + @"')
WHERE transferid_chr  = '" + strTransferID + "'";
            }
            /* <<======================================= */
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPService = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPService.DoExcute(strSQL);
            }
            catch (Exception err)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(err);
                return -1;
            }
            return lngRes;
        }
        #endregion

        #region �жϽ�����Ժ�������Ժ�Ĳ��ˣ��Ƿ�©���մ�λ��|���
        /// <summary>
        /// �ж��Ƿ�Ϊ������Ժ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_blnIsTodayPatient">[out����] �Ƿ�Խ�����Ժ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngJugeTodayPatient(string p_strRegisterID, out bool p_blnIsTodayPatient)
        {
            p_blnIsTodayPatient = false;
            long lngRes = 0;
            string strSQL = @"
						SELECT COUNT (*)
						FROM t_opr_bih_register b
						WHERE TRUNC (b.inpatient_dat) = TRUNC (SYSDATE)
							AND Trim(b.registerid_chr) = '[REGISTERID]'";

            /* @update by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */

            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT COUNT (*)
						FROM t_opr_bih_register b
						WHERE datediff(day,GETDATE(),b.inpatient_dat)= 0
							AND LTrim(RTRIM(b.registerid_chr)) = '[REGISTERID]'";
            }
            /* <<======================================= */

            strSQL = strSQL.Replace("[REGISTERID]", p_strRegisterID.Trim());
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (Int32.Parse(dtbResult.Rows[0][0].ToString()) > 0) p_blnIsTodayPatient = true;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = 0;
            }
            return lngRes;
        }
        /// <summary>
        /// �ж��Ƿ�Խ�����Ժ�������Ժ�Ĳ��ˣ���ȡ�˴�λ��
        /// ע�⣺
        ///		����2ʱ�����ǽ�����Ժ�Ĳ��ˣ�{p_blnExist��Ч}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_blnExist">[out����] �Ƿ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChargeBedForTodayPatient(string p_strRegisterID, out bool p_blnExist)
        {
            p_blnExist = false;
            long lngRes = 0;

            //�ж��Ƿ�Ϊ������Ժ����
            bool blnIsTodayPatient = false;
            lngRes = m_lngJugeTodayPatient(p_strRegisterID, out blnIsTodayPatient);
            if (!blnIsTodayPatient)
            {
                return 2;
            }

            string strSQL = @"
							SELECT COUNT (*)
							FROM t_opr_bih_patientcharge a
							WHERE TRIM (a.registerid_chr) = '[REGISTERID]'
							AND EXISTS (
									SELECT *
										FROM t_opr_bih_register b
									WHERE b.registerid_chr = a.registerid_chr
										AND TRUNC (b.inpatient_dat) = TRUNC (SYSDATE))
							AND EXISTS (SELECT *
											FROM t_bse_bih_specordercate
											WHERE TRIM (bedchargecate) = TRIM (a.invcateid_chr))";

            /* @update by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */

            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT COUNT (*)
							FROM t_opr_bih_patientcharge a
							WHERE LTRIM(RTRIM (a.registerid_chr)) = '[REGISTERID]'
							AND EXISTS (
									SELECT *
										FROM t_opr_bih_register b
									WHERE b.registerid_chr = a.registerid_chr
										AND DATEDIFF(DAY,b.inpatient_dat,GETDATE())=0)
							AND EXISTS (SELECT *
											FROM t_bse_bih_specordercate
											WHERE LTRIM(RTRIM (bedchargecate)) = LTRIM(RTRIM (a.invcateid_chr)))";
            }
            /* <<======================================= */

            strSQL = strSQL.Replace("[REGISTERID]", p_strRegisterID.Trim());
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (Int32.Parse(dtbResult.Rows[0][0].ToString()) > 0) p_blnExist = true;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = 0;
            }
            return lngRes;
        }
        /// <summary>
        /// �ж��Ƿ�Խ�����Ժ�������Ժ�Ĳ��ˣ���ȡ�����
        /// ע�⣺
        ///		����2ʱ�����ǽ�����Ժ�Ĳ��ˣ�{p_blnExist��Ч}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_blnExist">[out����] �Ƿ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChargeDiagnosisForTodayPatient(string p_strRegisterID, out bool p_blnExist)
        {
            p_blnExist = false;
            long lngRes = 0;

            //�ж��Ƿ�Ϊ������Ժ����
            bool blnIsTodayPatient = false;
            lngRes = m_lngJugeTodayPatient(p_strRegisterID, out blnIsTodayPatient);
            if (!blnIsTodayPatient)
            {
                return 2;
            }

            string strSQL = @"
							SELECT COUNT (*)
							FROM t_opr_bih_patientcharge a
							WHERE TRIM (a.registerid_chr) = '[REGISTERID]'
							AND EXISTS (
									SELECT *
										FROM t_opr_bih_register b
									WHERE b.registerid_chr = a.registerid_chr
										AND TRUNC (b.inpatient_dat) = TRUNC (SYSDATE))
							AND EXISTS (SELECT *
											FROM t_bse_bih_specordercate
											WHERE TRIM (autochargeitemtype) = TRIM (a.chargeitemid_chr))";

            /* @update by wjqin (05-11-25)
             * ���SQL SERVER��strSQl�汾����
             */

            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT COUNT (*)
							FROM t_opr_bih_patientcharge a
							WHERE LTRIM(RTRIM (a.registerid_chr)) = '[REGISTERID]'
							AND EXISTS (
									SELECT *
										FROM t_opr_bih_register b
									WHERE b.registerid_chr = a.registerid_chr
										AND DATEDIFF(DAY,b.inpatient_dat,GETDATE())=0)
							AND EXISTS (SELECT *
											FROM t_bse_bih_specordercate
											WHERE LTRIM(RTRIM (autochargeitemtype)) = LTRIM(RTRIM (a.chargeitemid_chr)))";
            }
            /* <<======================================= */

            strSQL = strSQL.Replace("[REGISTERID]", p_strRegisterID.Trim());
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (Int32.Parse(dtbResult.Rows[0][0].ToString()) > 0) p_blnExist = true;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = 0;
            }
            return lngRes;
        }
        #endregion
        #region ��֤ҽ�����Ƿ����غ�
        [AutoComplete]
        public long m_lngCheckInsuranceNum(string p_strInsurceNum, string p_strPatientId, out bool IsExist)
        {
            long lngRes = 0;
            IsExist = false;
            string strSQL = @"SELECT COUNT (*) as c 
  FROM t_bse_patient a
 WHERE a.insuranceid_vchr = '" + p_strInsurceNum + "' AND a.patientid_chr != '" + p_strPatientId + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPService = new clsHRPTableService();
                lngRes = objHRPService.DoGetDataTable(strSQL, ref dtbResult);
                if (lngRes > 0 && int.Parse(dtbResult.Rows[0]["c"].ToString()) > 0)
                {
                    IsExist = true;
                }
            }
            catch (Exception err)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(err);
                return -1;
            }
            return lngRes;
        }
        #endregion
        #region ���ݲ���ID ���סԺ�źͲ��˲���סԺ����
        [AutoComplete]
        public long m_mthFindInhospitalTimesByID(string strID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"SELECT a.*, b.times
  FROM t_bse_patient a,
       (SELECT   COUNT (*) AS times, patientid_chr
            FROM t_opr_bih_register
           WHERE status_int = 1 AND patientid_chr = '" + strID + @"'
        GROUP BY patientid_chr) b
 WHERE a.patientid_chr = b.patientid_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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

        #region �޸�סԺ������Ϣ(��)
        /// <summary>
        /// �޸�סԺ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="intBihState"></param>
        /// <param name="objPatientVO"></param>
        /// <param name="objBIHVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangeRegisterHospital(int intBihState, clsPatient_VO objPatientVO, ref clsT_Opr_Bih_Register_VO objBIHVO)
        {

            long lngReg = 0;
            clsclsPatientIdxVO objIndexVo = new clsclsPatientIdxVO();
            clsT_Opr_Bih_Transfer_VO objTransferVO = new clsT_Opr_Bih_Transfer_VO();
            clsBedManageSvc obj = new clsBedManageSvc();

            //ȷ��������Ϣ״̬
            #region ��Ժ	{�����޸�[��Ժ���ҡ�����������]  ע������û�п��ƣ�ֻ���ڱ�ʾ���ϴ���}
            //{1���������Vo[clsclsPatientIdxVO]��2���޸Ļ�����Ϣ������3���޸Ļ�����Ϣ��4���޸�סԺ�Ǽ���Ϣ��}
            //1���������Vo[clsclsPatientIdxVO]��
            BaseVoToIndexVo(objPatientVO, out objIndexVo);
            //2���޸Ļ�����Ϣ������
            lngReg = m_lngModifyPatientIdx(objIndexVo);

            //3���޸Ļ�����Ϣ��
            if (lngReg > 0)
            {
                lngReg = m_lngModifyPatient(objPatientVO);
            }

            //4���޸�סԺ�Ǽ���Ϣ��
            if (lngReg > 0)
            {
                lngReg = m_lngModifyBihRegisterInfoByVo(objBIHVO.m_strREGISTERID_CHR, objBIHVO);
            }
            //5 �޸ĵ�ת��Ϣ��
            if (lngReg > 0)
            {
                lngReg = this.m_lngUpdateTransferInfo(objBIHVO.m_strREGISTERID_CHR);
            }
            #endregion
            if (lngReg <= 0)
            {
                throw new Exception("����ʧ�ܣ�");
            }
            return lngReg;
        }
        #endregion

        #region �޸�סԺ������Ϣ(��)
        /// <summary>
        /// �޸�סԺ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="intBihState"></param>
        /// <param name="objPatientVO"></param>
        /// <param name="objBIHVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangeRegisterHospital2(int intBihState, clsPatient_VO objPatientVO, ref clsT_Opr_Bih_Register_VO objBIHVO)
        {

            long lngReg = 0;
            clsclsPatientIdxVO objIndexVo = new clsclsPatientIdxVO();
            clsT_Opr_Bih_Transfer_VO objTransferVO = new clsT_Opr_Bih_Transfer_VO();
            clsBedManageSvc obj = new clsBedManageSvc();

            //ȷ��������Ϣ״̬
            #region ��Ժ	{�����޸�[��Ժ���ҡ�����������]  ע������û�п��ƣ�ֻ���ڱ�ʾ���ϴ���}
            //{1���������Vo[clsclsPatientIdxVO]��2���޸Ļ�����Ϣ������3���޸Ļ�����Ϣ��4���޸�סԺ�Ǽ���Ϣ��}
            //1���������Vo[clsclsPatientIdxVO]��
            //BaseVoToIndexVo(objPatientVO, out objIndexVo);
            //2���޸Ļ�����Ϣ������
            // lngReg = m_lngModifyPatientIdx(objIndexVo);

            //3���޸Ļ�����Ϣ��
            if (lngReg > 0)
            {
                lngReg = m_lngModifyPatient(objPatientVO, objBIHVO.m_strREGISTERID_CHR);
            }

            //4���޸�סԺ�Ǽ���Ϣ��
            if (lngReg > 0)
            {
                lngReg = m_lngModifyBihRegisterInfoByVo(objBIHVO.m_strREGISTERID_CHR, objBIHVO);
            }
            //6.�޸�t_bse_hisemr_relation(���Ӳ���ͬ��������)
            if (lngReg > 0)
            {
                lngReg = m_lngModifyHisemrRelation(objBIHVO.m_strREGISTERID_CHR, objBIHVO);

            }
            //5 �޸ĵ�ת��Ϣ��
            if (lngReg > 0)
            {
                lngReg = this.m_lngUpdateTransferInfo(objBIHVO.m_strREGISTERID_CHR);
            }
            #endregion
            if (lngReg <= 0)
            {
                throw new Exception("����ʧ�ܣ�");
            }
            return lngReg;
        }
        #endregion

        #region ��ȡ סԺ���״̬(0--�Զ�,1--�ֹ� 
        /// <summary>
        /// ��ȡ סԺ���״̬(0--�Զ�,1--�ֹ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_intISHOSNUMATUO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetISHOSNUMATUO(out int m_intISHOSNUMATUO)
        {
            m_intISHOSNUMATUO = 0;
            long lngReg = 0;
            DataTable dtbResult = new DataTable();
            string strSQL = @"select a.ishosnumatuo from T_BSE_BIH_SPECORDERCATE a";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngReg = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngReg > 0 && dtbResult.Rows.Count > 0)
                {
                    m_intISHOSNUMATUO = int.Parse(dtbResult.Rows[0]["ishosnumatuo"].ToString());
                }
                else
                {
                    lngReg = 0;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngReg;

        }
        #endregion

        #region ����סԺ�Ǽ���ˮ�Ż�ȡ���˻�����Ϣ 
        /// <summary>
        /// ����סԺ�Ǽ���ˮ�Ż�ȡ���˻�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strREGISTERID_CHR">����ID</param>
        /// <param name="p_objResult">�˻�����ϢVO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfoByREGISTERID_CHR(string p_strREGISTERID_CHR, out clsPatient_VO p_objResult)
        {
            p_objResult = new clsPatient_VO();
            long lngRes = 0;
            string strSQL = @"SELECT 
            a.*,b.*
            FROM t_opr_bih_registerdetail a,t_opr_bih_register b
            where 
            a.registerid_chr=b.registerid_chr
            and a.registerid_chr=?";
            try
            {

                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strREGISTERID_CHR;
                lngRes = 0;
                // ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    // p_objResult.strPatientCardID = dtbResult.Rows[0]["PATIENTCARDID_CHR"].ToString().Trim();
                    p_objResult.m_strPATIENTID_CHR = dtbResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
                    p_objResult.m_strINPATIENTID_CHR = dtbResult.Rows[0]["INPATIENTID_CHR"].ToString().Trim();
                    p_objResult.m_strLASTNAME_VCHR = dtbResult.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
                    p_objResult.m_strIDCARD_CHR = dtbResult.Rows[0]["IDCARD_CHR"].ToString().Trim();
                    p_objResult.m_strMARRIED_CHR = dtbResult.Rows[0]["MARRIED_CHR"].ToString().Trim();
                    p_objResult.m_strBIRTHPLACE_VCHR = dtbResult.Rows[0]["BIRTHPLACE_VCHR"].ToString().Trim();
                    p_objResult.m_strHOMEADDRESS_VCHR = dtbResult.Rows[0]["HOMEADDRESS_VCHR"].ToString().Trim();
                    p_objResult.m_strSEX_CHR = dtbResult.Rows[0]["SEX_CHR"].ToString().Trim();
                    p_objResult.m_strNATIONALITY_VCHR = dtbResult.Rows[0]["NATIONALITY_VCHR"].ToString().Trim();
                    p_objResult.m_strFIRSTNAME_VCHR = dtbResult.Rows[0]["FIRSTNAME_VCHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["BIRTH_DAT"] != Convert.DBNull)
                    {
                        p_objResult.m_strBIRTH_DAT = Convert.ToDateTime(dtbResult.Rows[0]["BIRTH_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    p_objResult.m_strRACE_VCHR = dtbResult.Rows[0]["RACE_VCHR"].ToString().Trim();
                    p_objResult.m_strNATIVEPLACE_VCHR = dtbResult.Rows[0]["NATIVEPLACE_VCHR"].ToString().Trim();
                    p_objResult.m_strOCCUPATION_VCHR = dtbResult.Rows[0]["OCCUPATION_VCHR"].ToString().Trim();
                    p_objResult.m_strNAME_VCHR = dtbResult.Rows[0]["NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strHOMEPHONE_VCHR = dtbResult.Rows[0]["HOMEPHONE_VCHR"].ToString().Trim();
                    p_objResult.m_strOFFICEPHONE_VCHR = dtbResult.Rows[0]["OFFICEPHONE_VCHR"].ToString().Trim();
                    p_objResult.m_strINSURANCEID_VCHR = dtbResult.Rows[0]["INSURANCEID_VCHR"].ToString().Trim();
                    p_objResult.m_strMOBILE_CHR = dtbResult.Rows[0]["MOBILE_CHR"].ToString().Trim();
                    p_objResult.m_strOFFICEADDRESS_VCHR = dtbResult.Rows[0]["OFFICEADDRESS_VCHR"].ToString().Trim();
                    p_objResult.m_strEMPLOYER_VCHR = dtbResult.Rows[0]["EMPLOYER_VCHR"].ToString().Trim();
                    p_objResult.m_strOFFICEPC_VCHR = dtbResult.Rows[0]["OFFICEPC_VCHR"].ToString().Trim();
                    p_objResult.m_strHOMEPC_CHR = dtbResult.Rows[0]["HOMEPC_CHR"].ToString().Trim();
                    p_objResult.m_strEMAIL_VCHR = dtbResult.Rows[0]["EMAIL_VCHR"].ToString().Trim();
                    p_objResult.m_strCONTACTPERSONFIRSTNAME_VCHR = dtbResult.Rows[0]["CONTACTPERSONFIRSTNAME_VCHR"].ToString().Trim();
                    p_objResult.m_strCONTACTPERSONLASTNAME_VCHR = dtbResult.Rows[0]["CONTACTPERSONLASTNAME_VCHR"].ToString().Trim();
                    p_objResult.m_strCONTACTPERSONADDRESS_VCHR = dtbResult.Rows[0]["CONTACTPERSONADDRESS_VCHR"].ToString().Trim();
                    p_objResult.m_strCONTACTPERSONPHONE_VCHR = dtbResult.Rows[0]["CONTACTPERSONPHONE_VCHR"].ToString().Trim();
                    p_objResult.m_strCONTACTPERSONPC_CHR = dtbResult.Rows[0]["CONTACTPERSONPC_CHR"].ToString().Trim();
                    p_objResult.m_strPATIENTRELATION_VCHR = dtbResult.Rows[0]["PATIENTRELATION_VCHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["FIRSTDATE_DAT"] != Convert.DBNull)
                    {
                        p_objResult.m_strFIRSTDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["FIRSTDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    if (dtbResult.Rows[0]["ISEMPLOYEE_INT"] != Convert.DBNull)
                    {
                        p_objResult.m_intISEMPLOYEE_INT = Convert.ToInt32(dtbResult.Rows[0]["ISEMPLOYEE_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["STATUS_INT"] != Convert.DBNull)
                    {
                        p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["DEACTIVATE_DAT"] != Convert.DBNull)
                    {
                        p_objResult.m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    p_objResult.m_strOPERATORID_CHR = dtbResult.Rows[0]["OPERATORID_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["MODIFY_DAT"] != Convert.DBNull)
                    {
                        p_objResult.m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[0]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    p_objResult.m_strGOVCARD_CHR = dtbResult.Rows[0]["GOVCARD_CHR"].ToString();
                    p_objResult.m_strBLOODTYPE_CHR = dtbResult.Rows[0]["BLOODTYPE_CHR"].ToString();
                    if (dtbResult.Rows[0]["IFALLERGIC_INT"] != Convert.DBNull)
                    {
                        p_objResult.m_intIFALLERGIC_INT = Convert.ToInt16(dtbResult.Rows[0]["IFALLERGIC_INT"].ToString());
                    }
                    p_objResult.m_strALLERGICDESC_VCHR = dtbResult.Rows[0]["ALLERGICDESC_VCHR"].ToString();
                    p_objResult.m_strPAYTYPEID_CHR = dtbResult.Rows[0]["PAYTYPEID_CHR"].ToString().Trim();
                    p_objResult.m_strDIFFICULTY_VCHR = dtbResult.Rows[0]["DIFFICULTY_VCHR"].ToString().Trim();
                    p_objResult.strInsuranceID = dtbResult.Rows[0]["INSURANCEID_VCHR"].ToString().Trim();
                    p_objResult.ConsigneeAddr = dtbResult.Rows[0]["consigneeaddr"].ToString().Trim();
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

        #region  �޸Ĳ��˻�����Ϣ 
        /// <summary>
        /// �޸Ĳ��˻�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <param name="m_strREGISTERID_CHR"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyPatient(clsPatient_VO p_objRecord, string m_strREGISTERID_CHR)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"UPDATE t_opr_bih_registerdetail
   SET lastname_vchr = ?,
       idcard_chr = ?,
       married_chr = ?,
       birthplace_vchr = ?,
       homeaddress_vchr = ?,
       sex_chr = ?,
       nationality_vchr = ?,
       firstname_vchr = ?,
       birth_dat = ?,
       race_vchr = ?,
       nativeplace_vchr = ?,
       occupation_vchr = ?,
       name_vchr = ?,
       homephone_vchr = ?,
       officephone_vchr = ?,
       insuranceid_vchr = ?,
       mobile_chr = ?,
       
       officeaddress_vchr = ?,
       employer_vchr = ?,
       officepc_vchr = ?,
       homepc_chr = ?,
       email_vchr = ?,
       contactpersonfirstname_vchr = ?,
       contactpersonlastname_vchr = ?,
       contactpersonaddress_vchr = ?,
       contactpersonphone_vchr = ?,
       contactpersonpc_chr = ?,
       patientrelation_vchr = ?,
       firstdate_dat = ?,
       isemployee_int = ?,
       status_int = ?,
       operatorid_chr = ?,
       modify_dat = ?,
       --paytypeid_chr = ?,
       govcard_chr = ?,
       bloodtype_chr = ?,
       ifallergic_int = ?,
       allergicdesc_vchr = ?,
       consigneeaddr = ? 
 WHERE  registerid_chr = ?";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(39, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                int n = -1;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strLASTNAME_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strIDCARD_CHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strMARRIED_CHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strBIRTHPLACE_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strHOMEADDRESS_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strSEX_CHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strNATIONALITY_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strFIRSTNAME_VCHR;
                n++; objLisAddItemRefArr[n].Value = Convert.ToDateTime(p_objRecord.m_strBIRTH_DAT);
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strRACE_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strNATIVEPLACE_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strOCCUPATION_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strNAME_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strHOMEPHONE_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strOFFICEPHONE_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strINSURANCEID_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strMOBILE_CHR;

                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strOFFICEADDRESS_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strEMPLOYER_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strOFFICEPC_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strHOMEPC_CHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strEMAIL_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strCONTACTPERSONFIRSTNAME_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strCONTACTPERSONLASTNAME_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strCONTACTPERSONADDRESS_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strCONTACTPERSONPHONE_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strCONTACTPERSONPC_CHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strPATIENTRELATION_VCHR;
                n++; objLisAddItemRefArr[n].Value = Convert.ToDateTime(p_objRecord.m_strFIRSTDATE_DAT);
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_intISEMPLOYEE_INT;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_intSTATUS_INT;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strOPERATORID_CHR;
                n++; objLisAddItemRefArr[n].Value = Convert.ToDateTime(p_objRecord.m_strMODIFY_DAT);
                //n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strPAYTYPEID_CHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strGOVCARD_CHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strBLOODTYPE_CHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_intIFALLERGIC_INT;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.m_strALLERGICDESC_VCHR;
                n++; objLisAddItemRefArr[n].Value = p_objRecord.ConsigneeAddr;

                n++; objLisAddItemRefArr[n].Value = m_strREGISTERID_CHR;
                objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
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

        #region �޸�סԺ�Ǽ�--���Ӳ���������
        /// <summary>
        /// �޸�סԺ�Ǽ�--���Ӳ���������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterid_chr">��ˮ��</param>
        /// <param name="objBIHVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyHisemrRelation(string p_strRegisterid_chr, clsT_Opr_Bih_Register_VO objPatientVO)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"UPDATE t_bse_hisemr_relation a
set 
a.hisinpatientdate=?,
a.operatorid_chr=?,
a.operat_dat=sysdate
   
   
 WHERE  a.registerid_chr=?";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = Convert.ToDateTime(objPatientVO.m_strINPATIENT_DAT);
                objLisAddItemRefArr[1].Value = objPatientVO.m_strOPERATORID_CHR;
                objLisAddItemRefArr[2].Value = p_strRegisterid_chr;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
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

        #region ���ӳ�Ժ�ٻؼ�¼
        /// <summary>
        /// ���ӳ�Ժ�ٻؼ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        private long InsertReturnRecord(clsT_Opr_Bih_Transfer_VO objPatientVO)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO T_OPR_BIH_RETURNRECORD (SEQ_INT, REGISTERID_CHR, CREATE_DAT, CREATORID_CHR, RETURN_DAT, LEAVEID_CHR) 
                                                          VALUES (SEQ_RETURNRECORD.nextval, ?, sysdate, ?, sysdate, ?)";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = objPatientVO.m_strREGISTERID_CHR;
                objLisAddItemRefArr[1].Value = objPatientVO.m_strOPERATORID_CHR;
                //objLisAddItemRefArr[2].Value = objPatientVO.m_;

                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
    }
}
