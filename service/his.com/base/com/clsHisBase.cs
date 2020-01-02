using System;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll 
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Collections;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 公共方法类
    /// Create by kong 2004-06-10
    /// </summary>
    /// 
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsHisBase : com.digitalwave.iCare.middletier.clsMiddleTierBase, IDisposable	//MiddleTierBase.dll
    {
        //		/// <summary>
        //		/// 构造函数
        //		/// </summary>
        public clsHisBase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 获得系统时间
        /// <summary>
        /// 获取数据库服务器时间
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public System.DateTime s_GetServerDate()
        {
            long lngRes = 0;
            System.DateTime datResult = System.DateTime.Now;
            string strSQL = @"select sysdate from dual";
            System.Data.DataTable dtbResult = new System.Data.DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    datResult = System.DateTime.Parse(dtbResult.Rows[0]["sysdate"].ToString());
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return datResult;
        }
        #endregion

        #region 获取部门信息　欧阳孔伟　2004-05-24
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngGetDept(out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            string strSQL = @"SELECT *
							  FROM T_BSE_DEPTDESC 
							  WHERE status_int = 1";
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region 获得员工列表 欧阳孔伟 2004-05-31
        /// <summary>
        /// 获得员工列表
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngGetEmpList(out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            string strSQL = @"SELECT *
							  FROM t_bse_employee 
							  WHERE status_int = 1";
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                //				System.Windows.Forms.MessageBox.Show(strTmp);
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region 根据病人姓名获得门诊病人基本挂号信息
        /// <summary>
        /// 根据病人姓名获得门诊病人基本挂号信息
        /// </summary>
        /// <param name="strPatientName">病人姓名</param>
        /// <param name="dtbResult">输出信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngGetPatientInfoByName(string strPatientName, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;

            //			string strWhere = "name_vchr like '" + strPatientName.Trim() + "%' AND registerdate_dat BETWEEN TO_DATE ('"+DateTime.Now.ToString("yyyy-MM-dd 00:00:00")+"','yyyy-mm-dd hh24:mi:ss') AND TO_DATE ('"+DateTime.Now.ToString("yyyy-MM-dd 00:00:00")+"','yyyy-mm-dd hh24:mi:ss')";
            //			string strSQL = @"SELECT   *
            //								  FROM v_getpatientbasicinfo
            //							     WHERE registerid_chr IN (SELECT   MAX (registerid_chr)
            //															  FROM v_getpatientbasicinfo
            //															 WHERE " + strWhere;
            //			strSQL += @"
            //														  GROUP BY  patientcardid_chr)
            //							  ORDER BY patientcardid_chr";
            string strSQL = @"SELECT a.*, b.paytypename_vchr, b.paylimit_mny, b.chargepercent_dec, d.name_vchr, d.idcard_chr,
d.HOMEPHONE_VCHR,d.GOVCARD_CHR,d.INSURANCEID_VCHR,d.HOMEADDRESS_VCHR,d.sex_chr,d.birth_dat,d.ALLERGICDESC_VCHR,e.deptname_vchr,f.LASTNAME_VCHR,f.EMPNO_CHR,d.DIFFICULTY_VCHR, f.technicalrank_chr 
  FROM t_opr_patientregister a, t_bse_patientcard g, t_bse_patientpaytype b,T_BSE_PATIENT D, T_BSE_DEPTDESC E,t_bse_employee F,
   (select Max(REGISTERID_CHR) REGISTERID_CHR from t_opr_patientregister  where " + "REGISTERDATE_DAT BETWEEN TO_DATE ('" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') AND TO_DATE ('" + DateTime.Now.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')" + @" group by PATIENTCARDID_CHR ) C
 WHERE a.flag_int <> 3
   AND a.pstatus_int <> 3
   AND a.PAYTYPEID_CHR = b.paytypeid_chr(+)
   AND b.isusing_num = 1
   and a.REGISTERID_CHR(+)=C.REGISTERID_CHR
   and a.PATIENTID_CHR=d.PATIENTID_CHR(+)
  and a.DIAGDEPT_CHR=e.DEPTID_CHR(+)
   and a.DIAGDOCTOR_CHR=f.EMPID_CHR(+)
   and a.patientid_chr =g.patientid_chr
   and g.STATUS_INT >0
	 and d.NAME_VCHR like '" + strPatientName + "%'";

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据诊疗卡号获得门诊病人基本挂号信息
        /// <summary>
        /// 根据诊疗卡号获得门诊病人基本挂号信息
        /// </summary>
        /// <param name="strCardID">诊疗卡号</param>
        /// <param name="dtbResult">输出信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngGetPatientInfoByCard(string strCardID, out DataTable dtbResult, string strType)
        {
            long lngRes = 0;
            dtbResult = null;
            string strSQL = "";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();

                int TimeInterval = 0;
                DataTable dt = new DataTable();
                strSQL = @"select setstatus_int from t_sys_setting where setid_chr = '0067'";
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count == 1)
                {
                    string s = dt.Rows[0][0].ToString().Trim();
                    if (s != "" && Convert.ToInt32(s) > 0)
                    {
                        TimeInterval = Convert.ToInt32(s);
                    }
                }

                strSQL = @"SELECT a.*, b.paytypename_vchr, b.paylimit_mny,b.INTERNALFLAG_INT, b.chargepercent_dec,D.name_vchr,d.sex_chr,d.birth_dat,d.ALLERGICDESC_VCHR, d.IDCARD_CHR, 
                                d.GOVCARD_CHR,d.INSURANCEID_VCHR,d.difficulty_vchr,d.HOMEADDRESS_VCHR,e.deptname_vchr,f.LASTNAME_VCHR,f.EMPNO_CHR,d.HOMEPHONE_VCHR, f.ISEXPERT_CHR, f.ISEXTERNALEXPERT_CHR, f.technicalrank_chr, d.isvip_int
                                  FROM t_opr_patientregister a, t_bse_patientcard g,  t_bse_patientpaytype b,T_BSE_PATIENT D,T_BSE_DEPTDESC E,t_bse_employee F,
                                   (select distinct a.REGISTERID_CHR 
                                      from t_opr_patientregister A,T_BSE_PATIENTCARD B 
                                     where (sysdate between a.recorddate_dat and (a.recorddate_dat + ?/24))
                                       and a.patientcardid_chr = b.patientcardid_chr(+) and b.status_int > 0 
                                       and not exists(
                                                   select 1
                                                     from t_opr_patientregister ta
                                                    where a.invno_chr = ta.invno_chr
                                                      and (sysdate between ta.recorddate_dat and (ta.recorddate_dat + ?/24))
                                                 group by ta.invno_chr having mod(count(ta.invno_chr),2) = 0 )) C
                                 WHERE a.PAYTYPEID_CHR = b.paytypeid_chr(+)
                                   AND b.isusing_num = 1
                                   and C.REGISTERID_CHR = a.REGISTERID_CHR(+)
                                   and a.PATIENTID_CHR = d.PATIENTID_CHR(+)
                                   and a.DIAGDEPT_CHR = e.DEPTID_CHR(+)
                                   and a.DIAGDOCTOR_CHR = f.EMPID_CHR(+)
                                   and a.patientid_chr = g.patientid_chr
                                   and g.STATUS_INT > 0
                                   and a." + strType + " = ? order by a.registerdate_dat desc";

                if (strType.ToUpper() == "REGISTERNO_CHR")
                {
                    strSQL = @"select a.*, b.paytypename_vchr, b.paylimit_mny,b.INTERNALFLAG_INT, b.chargepercent_dec,D.name_vchr,d.sex_chr,d.birth_dat,d.ALLERGICDESC_VCHR, d.IDCARD_CHR, 
							          d.GOVCARD_CHR,d.INSURANCEID_VCHR,d.difficulty_vchr,d.HOMEADDRESS_VCHR,e.deptname_vchr,f.LASTNAME_VCHR,f.EMPNO_CHR,d.HOMEPHONE_VCHR, f.ISEXPERT_CHR, f.ISEXTERNALEXPERT_CHR, f.technicalrank_chr, d.isvip_int
						        from t_opr_patientregister a, t_bse_patientcard g,  t_bse_patientpaytype b,T_BSE_PATIENT D,T_BSE_DEPTDESC E,t_bse_employee F,
						             (select distinct a.REGISTERID_CHR 
                                      from t_opr_patientregister A,T_BSE_PATIENTCARD B 
                                     where (sysdate between a.recorddate_dat and (a.recorddate_dat + ?/24))
                                       and a.patientcardid_chr = b.patientcardid_chr(+) and b.status_int > 0 
                                       and not exists(
                                                   select 1
                                                     from t_opr_patientregister ta
                                                    where a.invno_chr = ta.invno_chr
                                                      and (sysdate between ta.recorddate_dat and (ta.recorddate_dat + ?/24))
                                                 group by ta.invno_chr having mod(count(ta.invno_chr),2) = 0 )) C, 
                                     t_opr_takediagrec h
						        where a.PAYTYPEID_CHR = b.paytypeid_chr(+)
						          and b.isusing_num = 1
						          and C.REGISTERID_CHR = a.REGISTERID_CHR(+)
						          and a.PATIENTID_CHR = d.PATIENTID_CHR(+)
						          and a.DIAGDEPT_CHR = e.DEPTID_CHR(+)
						          and a.DIAGDOCTOR_CHR = f.EMPID_CHR(+)
						          and a.patientid_chr = g.patientid_chr
						          and g.STATUS_INT > 0
						          and a.patientid_chr = h.patientid_chr
						          and h.pstatus_int <> 2
						          and a." + strType + " = ? order by a.registerdate_dat desc";
                }
                else if (strType.ToUpper() == "REGISTERID_CHR")
                {
                    strSQL = @"SELECT a.*, b.paytypename_vchr, b.paylimit_mny,b.INTERNALFLAG_INT, b.chargepercent_dec,D.name_vchr,d.sex_chr,d.birth_dat,d.ALLERGICDESC_VCHR, d.IDCARD_CHR, 
                                d.GOVCARD_CHR,d.INSURANCEID_VCHR,d.difficulty_vchr,d.HOMEADDRESS_VCHR,e.deptname_vchr,f.LASTNAME_VCHR,f.EMPNO_CHR,d.HOMEPHONE_VCHR, f.ISEXPERT_CHR, f.ISEXTERNALEXPERT_CHR, f.technicalrank_chr, d.isvip_int
                                  FROM t_opr_patientregister a, t_bse_patientcard g,  t_bse_patientpaytype b,T_BSE_PATIENT D,T_BSE_DEPTDESC E,t_bse_employee F,
                                   (select distinct a.REGISTERID_CHR 
                                      from t_opr_patientregister A,T_BSE_PATIENTCARD B 
                                     where (sysdate between a.recorddate_dat and (a.recorddate_dat + ?/24))
                                       and a.patientcardid_chr = b.patientcardid_chr(+) and b.status_int > 0 
                                       and not exists(
                                                   select 1
                                                     from t_opr_patientregister ta
                                                    where a.invno_chr = ta.invno_chr
                                                      and (sysdate between ta.recorddate_dat and (ta.recorddate_dat + ?/24))
                                                 group by ta.invno_chr having mod(count(ta.invno_chr),2) = 0 )) C
                                 WHERE a.PAYTYPEID_CHR = b.paytypeid_chr(+)
                                   AND b.isusing_num = 1
                                   and C.REGISTERID_CHR = a.REGISTERID_CHR(+)
                                   and a.PATIENTID_CHR = d.PATIENTID_CHR(+)
                                   and a.DIAGDEPT_CHR = e.DEPTID_CHR(+)
                                   and a.DIAGDOCTOR_CHR = f.EMPID_CHR(+)
                                   and a.patientid_chr = g.patientid_chr
                                   and g.STATUS_INT > 0
                                   and a." + strType + " = ? order by a.registerdate_dat desc";
                }

                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = TimeInterval;
                ParamArr[1].Value = TimeInterval;
                ParamArr[2].Value = strCardID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, ParamArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据卡号查出病人信息
        /// <summary>
        /// 根据卡号查出病人信息
        /// </summary>
        /// <param name="strCardID"></param>
        /// <param name="dtbResult"></param>
        /// <param name="flag">true为病人ID,false为卡号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetPatientInfoByCardID(string strCardID, out DataTable dtbResult, bool flag)
        {
            long lngRes = 0;
            dtbResult = null;
            string str = "and b.patientcardid_chr = ?";
            if (flag)
            {
                str = "and a.patientid_chr = ?";
            }
            string strSQL = @"select   a.patientid_chr,
                                       a.inpatientid_chr,
                                       a.lastname_vchr,
                                       a.idcard_chr,
                                       a.married_chr,
                                       a.birthplace_vchr,
                                       a.homeaddress_vchr,
                                       a.sex_chr,
                                       a.nationality_vchr,
                                       a.firstname_vchr,
                                       a.birth_dat,
                                       a.race_vchr,
                                       a.nativeplace_vchr,
                                       a.occupation_vchr,
                                       a.name_vchr,
                                       a.homephone_vchr,
                                       a.officephone_vchr,
                                       a.insuranceid_vchr,
                                       a.mobile_chr,
                                       a.officeaddress_vchr,
                                       a.employer_vchr,
                                       a.officepc_vchr,
                                       a.homepc_chr,
                                       a.email_vchr,
                                       a.contactpersonfirstname_vchr,
                                       a.contactpersonlastname_vchr,
                                       a.contactpersonaddress_vchr,
                                       a.contactpersonphone_vchr,
                                       a.contactpersonpc_chr,
                                       a.patientrelation_vchr,
                                       a.firstdate_dat,
                                       a.isemployee_int,
                                       a.status_int,
                                       a.deactivate_dat,
                                       a.operatorid_chr,
                                       a.modify_dat,
                                       a.paytypeid_chr,
                                       a.optimes_int,
                                       a.govcard_chr,
                                       a.bloodtype_chr,
                                       a.ifallergic_int,
                                       a.allergicdesc_vchr,
                                       a.difficulty_vchr,
                                       a.extendid_vchr,
                                       a.inpatienttempid_vchr,
                                       a.modifytime_dat,
                                       a.modifyman_vchr,
                                       a.registertime_dat,
                                       a.registerman_vchr,
                                       a.patientsources_vchr,
                                       b.patientcardid_chr,
                                       c.paytypeid_chr,
                                       c.paytypename_vchr,
                                       c.memo_vchr,
                                       c.paylimit_mny,
                                       c.payflag_dec,
                                       c.paypercent_dec,
                                       c.paytypeno_vchr,
                                       c.isusing_num,
                                       c.copayid_chr,
                                       c.chargepercent_dec,
                                       c.internalflag_int,
                                       c.coalitionrecipeflag_int,
                                       c.bihlimitrate_dec, 
                                       '' as isexpert_chr,
                                       '' as isexternalexpert_chr,
                                       a.isvip_int
                                  from t_bse_patient a, t_bse_patientcard b, t_bse_patientpaytype c
                                 where a.patientid_chr = b.patientid_chr(+)
                                   and a.paytypeid_chr = c.paytypeid_chr(+)
                                   and b.status_int > 0
                                   [str]";
            try
            {
                strSQL = strSQL.Replace("[str]", str);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strCardID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, ParamArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据流水号和挂号时间获得门诊病人基本挂号信息
        /// <summary>
        /// 根据流水号和挂号时间获得门诊病人基本挂号信息
        /// </summary>
        /// <param name="strRegisterNo">流水号</param>
        /// <param name="strRegisterDate">挂号时间</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngGetPatientInfoByRegNoAndRegDate(string strRegisterNo, string strRegisterDate, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;

            string strSQL = @"SELECT *
							    FROM v_getpatientbasicinfo
							   WHERE registerno_chr = ?
								 AND registerdate_dat = TO_DATE (?, 'yyyy-mm-dd')";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = strRegisterNo.Trim();
                ParamArr[1].Value = s_GetServerDate().ToShortDateString();

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据SQL语句获得门诊病人基本挂号信息
        /// <summary>
        /// 根据SQL语句获得门诊病人基本挂号信息
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="dtbResult">输出信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngGetPatientInfoByAny(string strSQL, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;

            string strSQLTmp = @"SELECT *
							    FROM v_getpatientbasicinfo
								";
            strSQLTmp += strSQL;
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLTmp, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 查询所有用法
        /// <summary>
        /// 查询所有用法
        /// </summary>
        /// <param name="dtbResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngFindAllUsage(out DataTable dtbResult)
        {
            dtbResult = null;
            long lngRes = 0;

            string strSQL = @"SELECT TRIM (usageid_chr) AS usageid_chr, TRIM (usagename_vchr) AS usagename_vchr,
									 TRIM (usagecode_chr) AS usagecode_chr
							    FROM T_bse_UsageType ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 查询所有用药频率
        /// <summary>
        /// 查询所有用药频率
        /// </summary>
        /// <param name="dtbResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngFindAllRecipeFreq(out DataTable dtbResult)
        {
            dtbResult = null;
            long lngRes = 0;

            string strSQL = @"SELECT *
							    FROM T_AID_RECIPEFREQ ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 查询单位
        /// <summary>
        /// 查询单位
        /// </summary>
        /// <param name="strMedicineID">药品代码</param>
        /// <param name="intUsedFlag">使用标志，1：药库，2：药房，3：门诊，4：住院</param>
        /// <param name="objResults">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngFindUnitByUsedFlag(string strMedicineID, int intUsedFlag, out clsUnit_VO[] objResults)
        {
            objResults = new clsUnit_VO[0];
            long lngRes = 0;

            DataTable dtbResult = null;
            string strSQL = @"SELECT a.bigunit_chr, b.unitname_chr
								FROM t_bse_medunitandunit a, t_aid_unit b
							   WHERE a.bigunit_chr = b.unitid_chr
								 AND a.usedflag_int = ?
								 AND TRIM(a.medicineid_chr) = ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = intUsedFlag.ToString();
                ParamArr[1].Value = strMedicineID.Trim();

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, ParamArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    objResults = new clsUnit_VO[intRow];
                    for (int i = 0; i < intRow; i++)
                    {
                        objResults[i] = new clsUnit_VO();
                        objResults[i].m_strUnitID = dtbResult.Rows[i]["bigunit_chr"].ToString().Trim();
                        objResults[i].m_strUnitName = dtbResult.Rows[i]["unitname_chr"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 查询药品
        /// <summary>
        /// 查询药品
        /// </summary>
        /// <param name="strMedicineID">药品代码</param>
        /// <param name="objResults">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngFindMedicine(string strMedicineID, out clsMedicine_VO[] objResults)
        {
            objResults = new clsMedicine_VO[0];
            long lngRes = 0;

            DataTable dtbResult = null;
            string strSQL = @"SELECT medicineid_chr, medicinename_vchr
								FROM t_bse_medicine
							   WHERE medicineid_chr LIKE ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strMedicineID.Trim() + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, ParamArr);
                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    objResults = new clsMedicine_VO[intRow];
                    for (int i = 0; i < intRow; i++)
                    {
                        objResults[i] = new clsMedicine_VO();
                        objResults[i].m_strMedicineID = dtbResult.Rows[i]["medicineid_chr"].ToString().Trim();
                        objResults[i].m_strMedicineName = dtbResult.Rows[i]["medicinename_vchr"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 按模糊查找库存药品　欧阳孔伟　2004-05-17
        /// <summary>
        /// 按SQL脚本模糊来查询库存药品列表
        /// </summary>
        /// <param name="p_strSQL">SQL脚本</param>
        /// <param name="p_objResultArr">输入值</param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngFindStorageMedicineByAny(string p_strSQL, out clsStorageMedDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedDetail_VO[0];

            string strSQL = @"SELECT *
								FROM v_opr_storagemeddetail
							" + p_strSQL;
            System.Data.DataTable dtbResult = new System.Data.DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsStorageMedDetail_VO[intRow];
                        for (int i1 = 0; i1 < intRow; i1++)
                        {
                            string strQty = "";
                            string strBuyPrice = "";
                            string strSalePrice = "";
                            string strWholeSalePrice = "";
                            string strUsefulStatus = "";

                            p_objResultArr[i1] = new clsStorageMedDetail_VO();
                            p_objResultArr[i1].m_objStorage = new clsStorage_VO();
                            p_objResultArr[i1].m_objStorage.m_strStroageID = dtbResult.Rows[i1]["storageid_chr"].ToString().Trim();
                            p_objResultArr[i1].m_objStorage.m_strStroageName = dtbResult.Rows[i1]["storagename_vchr"].ToString().Trim();
                            p_objResultArr[i1].m_objMedicine = new clsMedicine_VO();
                            p_objResultArr[i1].m_objMedicine.m_strMedicineID = dtbResult.Rows[i1]["medicineid_chr"].ToString().Trim();
                            p_objResultArr[i1].m_objMedicine.m_strMedicineName = dtbResult.Rows[i1]["medicinename_vchr"].ToString().Trim();
                            p_objResultArr[i1].m_objMedicine.m_strMedSpec = dtbResult.Rows[i1]["medspec_vchr"].ToString().Trim();
                            p_objResultArr[i1].m_strSysLotNo = dtbResult.Rows[i1]["syslotno_chr"].ToString().Trim();
                            p_objResultArr[i1].m_strLotNo = dtbResult.Rows[i1]["lotno_vchr"].ToString().Trim();
                            p_objResultArr[i1].m_objProduct = new clsVendor_VO();
                            p_objResultArr[i1].m_objProduct.m_strVendorID = dtbResult.Rows[i1]["productorid_chr"].ToString().Trim();
                            p_objResultArr[i1].m_objProduct.m_strVendorName = dtbResult.Rows[i1]["vendorname_vchr"].ToString().Trim();
                            p_objResultArr[i1].m_objUnit = new clsUnit_VO();
                            p_objResultArr[i1].m_objUnit.m_strUnitID = dtbResult.Rows[i1]["unitid_chr"].ToString().Trim();
                            p_objResultArr[i1].m_objUnit.m_strUnitName = dtbResult.Rows[i1]["unitname_chr"].ToString().Trim();
                            p_objResultArr[i1].m_strUsefulLife = dtbResult.Rows[i1]["usefullife_dat"].ToString().Trim();

                            strQty = dtbResult.Rows[i1]["curqty_dec"].ToString();
                            if (strQty == "")
                            {
                                strQty = "0";
                            }
                            p_objResultArr[i1].m_fltCurQty = float.Parse(strQty);

                            strBuyPrice = dtbResult.Rows[i1]["buyunitprice_mny"].ToString();
                            if (strBuyPrice == "")
                            {
                                strBuyPrice = "0";
                            }
                            p_objResultArr[i1].m_fltBuyUnitPrice = float.Parse(strBuyPrice);

                            strSalePrice = dtbResult.Rows[i1]["saleunitprice_mny"].ToString();
                            if (strSalePrice == "")
                            {
                                strSalePrice = "0";
                            }
                            p_objResultArr[i1].m_fltSaleUnitPrice = float.Parse(strSalePrice);

                            strWholeSalePrice = dtbResult.Rows[i1]["wholesaleunitprice_mny"].ToString();
                            if (strWholeSalePrice == "")
                            {
                                strWholeSalePrice = "0";
                            }
                            p_objResultArr[i1].m_fltWholesaleUnitPrice = float.Parse(strWholeSalePrice);

                            strUsefulStatus = dtbResult.Rows[i1]["usefulstatus_int"].ToString();
                            if (strUsefulStatus == "")
                            {
                                strUsefulStatus = "1";
                            }
                            p_objResultArr[i1].m_intUsefulStatus = int.Parse(strUsefulStatus);
                        }
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

        #region 查询所有煎法 Create By Sam 2004-7-15
        /// <summary>
        /// 查询所有煎法
        /// </summary>
        /// <param name="dtbResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngFindAllCooking(out DataTable dtbResult)
        {
            dtbResult = null;
            long lngRes = 0;

            string strSQL = @"SELECT TRIM (cookingmethodid_chr) AS cookingmethodid_chr, 
                                     TRIM (cookingmethodname_vchr) AS cookingmethodname_vchr,
									 TRIM (mnemonic_chr) AS mnemonic_chr
							    FROM t_aid_cmcookingmethod ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 查询病人类型 Create By Sam 2004-7-15
        /// <summary>
        /// 查询病人类型
        /// </summary>
        /// <param name="dtbResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngFindAllPatType(out DataTable dtbResult)
        {
            dtbResult = null;
            long lngRes = 0;

            string strSQL = @"SELECT paytypeid_chr, paytypename_vchr, discount_dec,
                              recipelimit_dec, bedfeelimit_dec, medlimit_dec 
                              FROM t_bse_patientpaytype ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 查询挂号类型 Create By Sam 2004-7-15
        /// <summary>
        /// 查询挂号类型
        /// </summary>
        /// <param name="dtbResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long s_lngFindAllRegType(out DataTable dtbResult)
        {
            dtbResult = null;
            long lngRes = 0;
            string strSQL = @"SELECT registertypeid_chr, registertypename_vchr, regpay_mny,
                            diagpay_mny 
                            FROM t_bse_registertype ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据条件查询病人的类型
        [AutoComplete]
        public long m_mthGetPatinetTypeByCondition(out DataTable dt, string str)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"SELECT *  FROM t_bse_patientpaytype  where PAYTYPENO_VCHR like '" + str + "%'  order by PAYTYPENO_VCHR";
            //			string strSQL = @"SELECT *  FROM t_bse_patientpaytype  order by PAYTYPEID_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count == 0)
                {
                    strSQL = @"SELECT *  FROM t_bse_patientpaytype  order by PAYTYPENO_VCHR";
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取当天收费次数
        [AutoComplete]
        public long m_mthChargeTimesByPatientID(string strPatientID)
        {

            long lngRes = 0;

            string strSQL = @"select OUTPATRECIPEID_CHR from t_opr_outpatientrecipe where pstauts_int = 2 AND PATIENTID_CHR = ? and  RECORDDATE_DAT between 
                                to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date(?,'yyyy-mm-dd hh24:mi:ss') ";
            try
            {
                DataTable dt = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = strPatientID;
                ParamArr[1].Value = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                ParamArr[2].Value = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                if (lngRes > 0 && dt.Rows.Count == 0)
                {
                    lngRes = dt.Rows.Count;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获得药品基本信息
        /// <summary>
        /// 获得药品基本信息
        /// </summary>
        /// <param name="dtbMedicine">返回药品数据</param>
        /// <param name="strSTORAGEID">仓库ID</param>
        ///  <param name="strMedSTORAGEID">药房ID</param>
        /// <param name="blRepertory">是否显示库存</param>
        /// <param name="blISOutStorage">是否出库模块使用,true-出库模块</param>
        /// <param name="blIsStoreage">是否药库使用,true-药库，fasle-药房使用</param>
        /// <param name="strOutmedID">is not null 为配药模块使用</param>
        /// <param name="OrderByWhere">数据排序的条件</param>
        /// <param name="status">是否合并仓库出库，0-不合并，1-合并</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllMedicine(out DataTable dtbMedicine, string strSTORAGEID, bool blRepertory, bool blISOutStorage, bool blIsStoreage, string strOutmedID, string strMedSTORAGEID, string OrderByWhere, int status)
        {
            dtbMedicine = null;
            long lngRes = 0;
            string strSQL;
            if (blIsStoreage == true)
            {
                if (blISOutStorage == false)
                {
                    #region
                    if (strSTORAGEID == "-1")
                    {
                        #region
                        if (blRepertory == false)
                        {
                            strSQL = @"select b.MEDICINEID_CHR,b.ASSISTCODE_CHR,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,
                                            b.PRODUCTORID_CHR,b.MEDICINEENGNAME_VCHR,b.OPUNIT_CHR,b.IPUNIT_CHR,
                                            b.UNITPRICE_MNY,b.PYCODE_CHR,b.WBCODE_CHR,b.PACKQTY_DEC,b.TRADEPRICE_MNY,
                                            b.PACKQTY_DEC,'' TOLFINANCE_DEC,b.NOQTYFLAG_INT,b.TRADEPRICE_MNY,b.IPNOQTYFLAG_INT 
								     from t_bse_medicine b where b.IFSTOP_INT=0 " + OrderByWhere;
                        }
                        else
                        {
                            strSQL = @"SELECT   medicineid_chr, assistcode_chr,medicinename_vchr,
         medspec_vchr, productorid_chr,medicineengname_vchr,
         unitprice_mny,pycode_chr,
         wbcode_chr, packqty_dec,
         noqtyflag_int, tradeprice_mny, ipnoqtyflag_int,opunit_chr,
         CASE
            WHEN opchargeflg_int = 0
               THEN opunit_chr
            ELSE ipunit_chr
         END AS ipunit_chr,
         CASE
            WHEN opchargeflg_int = 0
               THEN nvl(tolfinance_dec,0) + nvl(tolfinance_dec1,0)
            ELSE   nvl(tolfinance_dec,0) * packqty_dec
                 + nvl(tolfinance_dec1,0)
         END AS tolfinance_dec
    FROM (SELECT c.medicineid_chr, c.assistcode_chr, c.medicinename_vchr,
                 c.ipnoqtyflag_int, c.medspec_vchr,
                 c.productorid_chr, c.medicineengname_vchr, c.opunit_chr,
                 c.ipunit_chr, c.unitprice_mny, c.pycode_chr, c.wbcode_chr,
                 c.tradeprice_mny, c.packqty_dec, c.opchargeflg_int,
                 (SELECT SUM (CURQTY_DEC)
                    FROM t_opr_storagemeddetail
                   WHERE medicineid_chr = c.medicineid_chr
                     AND FLAG_INT = 1) AS tolfinance_dec,
                 (SELECT SUM (CURQTY_DEC)
                    FROM t_opr_storagemeddetail
                   WHERE medicineid_chr = c.medicineid_chr
                     AND FLAG_INT = 0) AS tolfinance_dec1,
                 c.noqtyflag_int
            FROM t_bse_medicine c
           WHERE c.ifstop_int = 0) b   " + OrderByWhere;
                        }
                        #endregion

                    }
                    else if (blRepertory == true)
                    {

                        strSQL = @"select a.MEDICINEID_CHR,b.ASSISTCODE_CHR,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,b.PRODUCTORID_CHR,
										b.MEDICINEENGNAME_VCHR,b.OPUNIT_CHR,b.IPUNIT_CHR,b.UNITPRICE_MNY,b.PYCODE_CHR,b.WBCODE_CHR,
										b.PACKQTY_DEC,b.TRADEPRICE_MNY,b.PACKQTY_DEC,b.NOQTYFLAG_INT,b.TRADEPRICE_MNY,b.IPNOQTYFLAG_INT,
										 TOLFINANCE_DEC
								 from t_bse_storageandmedicine a ,(select MEDICINEID_CHR,sum(CURQTY_DEC) as TOLFINANCE_DEC  from  t_opr_storagemeddetail group by MEDICINEID_CHR) d
										,t_bse_medicine b  where  a.STORAGEID_CHR='" + strSTORAGEID +
                            "' and a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.MEDICINEID_CHR=d.MEDICINEID_CHR(+) and  b.IFSTOP_INT=0 " + OrderByWhere;
                    }
                    else
                    {
                        strSQL = @"select b.MEDICINEID_CHR,b.ASSISTCODE_CHR,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,b.PRODUCTORID_CHR,
										b.MEDICINEENGNAME_VCHR,b.OPUNIT_CHR,b.IPUNIT_CHR,b.UNITPRICE_MNY,b.PYCODE_CHR,
										b.WBCODE_CHR,b.PACKQTY_DEC,b.TRADEPRICE_MNY,b.PACKQTY_DEC,'' TOLFINANCE_DEC,b.NOQTYFLAG_INT,
										b.TRADEPRICE_MNY,b.IPNOQTYFLAG_INT 
								 from t_bse_storageandmedicine a,t_bse_medicine b where a.MEDICINEID_CHR=b.MEDICINEID_CHR and b.IFSTOP_INT=0
										and a.STORAGEID_CHR='" + strSTORAGEID + "' " + OrderByWhere;
                    }
                    #endregion
                }
                else
                {
                    #region
                    string str1 = "";
                    if (strOutmedID != null)
                    {
                        str1 = @" and a.MEDICINEID_CHR='" + strOutmedID + "'";
                    }
                    string strWhere = " ";
                    if (status == 0)
                    {
                        strWhere = " and  a.STORAGEID_CHR='" + strSTORAGEID + "'";
                    }
                    strSQL = @"select a.MEDICINEID_CHR,b.ASSISTCODE_CHR,b.PACKQTY_DEC,b.MEDSPEC_VCHR,b.TRADEPRICE_MNY,
									b.UNITPRICE_MNY,b.MEDSPEC_VCHR,b.MEDICINENAME_VCHR,a.SYSLOTNO_CHR,a.LOTNO_VCHR,
									a.PRODUCTORID_CHR,a.CURQTY_DEC,b.UNITPRICE_MNY SALEUNITPRICE_MNY,a.BUYUNITPRICE_MNY,
									a.WHOLESALEUNITPRICE_MNY,a.UNITID_CHR,a.USEFULLIFE_DAT,b.PYCODE_CHR,b.WBCODE_CHR,
                                    case when c.AIMUNIT_INT=1 then '是' else '否' end as  isAIM,c.AIMUNITPRICE_MNY,c.LIMITUNITPRICE_MNY,a.STORAGEID_CHR 
							from t_opr_storagemeddetail a LEFT JOIN t_opr_storageordde c ON a.medicineid_chr = c.medicineid_chr  
                            AND a.syslotno_chr = c.syslotno_chr  AND c.sign_int =1,t_bse_medicine b where a.MEDICINEID_CHR=b.MEDICINEID_CHR  " + str1 + " and  a.FLAG_INT=0  and b.IFSTOP_INT=0  " + strWhere + OrderByWhere;
                    #endregion
                }
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbMedicine);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            else
            {
                if (blISOutStorage == false)
                {
                    if (strSTORAGEID != "-1" && blRepertory == true)
                    {
                        strSQL = @"select b.MEDICINEID_CHR,b.ASSISTCODE_CHR,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,
										b.PRODUCTORID_CHR,b.MEDICINEENGNAME_VCHR,b.OPUNIT_CHR,case when b.OPCHARGEFLG_INT=0 then b.OPUNIT_CHR when b.OPCHARGEFLG_INT=1 then  b.IPUNIT_CHR end as IPUNIT_CHR,
										b.UNITPRICE_MNY,b.PYCODE_CHR,b.WBCODE_CHR,b.PACKQTY_DEC,b.TRADEPRICE_MNY,
										b.PACKQTY_DEC,b.NOQTYFLAG_INT,b.TRADEPRICE_MNY,b.IPNOQTYFLAG_INT,
										TOLFINANCE_DEC
								from t_bse_storageandmedicine a,t_bse_medicine b,(select MEDICINEID_CHR,sum(CURQTY_DEC) as TOLFINANCE_DEC  from  t_opr_storagemeddetail where STORAGEID_CHR='" + strMedSTORAGEID + @"' and FLAG_INT=1 group by MEDICINEID_CHR) d  where a.STORAGEID_CHR='" + strSTORAGEID + "' and  a.MEDICINEID_CHR=b.MEDICINEID_CHR and b.IFSTOP_INT=0   " + OrderByWhere;
                        try
                        {
                            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbMedicine);

                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }
                    }
                    else
                    {
                        strSQL = @"select b.MEDICINEID_CHR,b.ASSISTCODE_CHR,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,
										b.PRODUCTORID_CHR,b.MEDICINEENGNAME_VCHR,b.OPUNIT_CHR,case when b.OPCHARGEFLG_INT=0 then b.OPUNIT_CHR when b.OPCHARGEFLG_INT=1 then  b.IPUNIT_CHR end as IPUNIT_CHR,
										b.UNITPRICE_MNY,b.PYCODE_CHR,b.WBCODE_CHR,b.PACKQTY_DEC,b.TRADEPRICE_MNY,
										b.PACKQTY_DEC,b.NOQTYFLAG_INT,b.TRADEPRICE_MNY,b.IPNOQTYFLAG_INT,
										'' as TOLFINANCE_DEC
								from t_bse_storageandmedicine a,t_bse_medicine b where a.STORAGEID_CHR='" + strSTORAGEID + "' and  a.MEDICINEID_CHR=b.MEDICINEID_CHR and b.IFSTOP_INT=0  " + OrderByWhere;
                        try
                        {
                            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbMedicine);

                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }

                    }
                }
                else
                {
                    if (strSTORAGEID != "-1")
                    {
                        strSQL = @"select a.STORAGEID_CHR,a.MEDICINEID_CHR,b.ASSISTCODE_CHR,b.PACKQTY_DEC,b.MEDSPEC_VCHR,b.TRADEPRICE_MNY,
									b.UNITPRICE_MNY,b.MEDSPEC_VCHR,b.MEDICINENAME_VCHR,a.SYSLOTNO_CHR,a.LOTNO_VCHR,
									a.PRODUCTORID_CHR,a.CURQTY_DEC,a.SALEUNITPRICE_MNY,a.BUYUNITPRICE_MNY,
									a.WHOLESALEUNITPRICE_MNY,a.UNITID_CHR,a.USEFULLIFE_DAT,b.PYCODE_CHR,b.WBCODE_CHR,case when c.AIMUNIT_INT=1 then '是' else '否' end as  isAIM,c.AIMUNITPRICE_MNY,c.LIMITUNITPRICE_MNY
							from t_opr_storagemeddetail a LEFT JOIN t_opr_storageordde c ON a.medicineid_chr = c.medicineid_chr  AND a.syslotno_chr = c.syslotno_chr  AND c.sign_int =1,t_bse_medicine b where a.STORAGEID_CHR='" + strMedSTORAGEID +
                            "' and a.MEDICINEID_CHR=b.MEDICINEID_CHR and b.IFSTOP_INT=0  and a.FLAG_INT=1 order by ASSISTCODE_CHR,SYSLOTNO_CHR";
                        try
                        {
                            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbMedicine);

                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }
                    }
                }

            }

            return lngRes;
        }
        #endregion


        #region 获取出入库的历史记录数据
        /// <summary>
        /// 获取出入库的历史记录数据
        /// </summary>
        /// <param name="HistoryData">返回历史资料</param>
        /// <param name="p_strMedID">药品ID</param>
        /// <param name="SIGN_INT">出入标志 1－入库2－出库3-退库4-退库</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetHistoryData(out clsHistoryINOUT_VO HistoryData, string p_strMedID, int SIGN_INT)
        {

            long lngRes = 0;
            HistoryData = new clsHistoryINOUT_VO();
            string strSQL = @"select b.AIMUNITPRICE_MNY,b.LIMITUNITPRICE_MNY,b.BUYUNITPRICE_MNY,b.ORDERUNIT_VCHR,b.ORDERUNITPRICE_MNY,b.ORDERPKGQTY_DEC,b.SALEUNITPRICE_MNY,c.UNITPRICE_MNY,b.WHOLESALEUNITPRICE_MNY  from (select Max(ORD_DAT) as MaxORD_DAT from t_opr_storageordde a,T_OPR_STORAGEORD b where a.MEDICINEID_CHR='" + p_strMedID + "' and a.STORAGEORDID_CHR=b.STORAGEORDID_CHR and b.SIGN_INT=" + SIGN_INT.ToString() + ") a,t_opr_storageordde b,t_bse_medicine c where b.ORD_DAT=a.MaxORD_DAT and b.MEDICINEID_CHR=c.MEDICINEID_CHR";
            try
            {
                DataTable dt = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    HistoryData.m_strAIMUNITPRICE = dt.Rows[0]["AIMUNITPRICE_MNY"].ToString();
                    HistoryData.m_strLIMITUNITPRICE = dt.Rows[0]["LIMITUNITPRICE_MNY"].ToString();
                    HistoryData.m_strBUYUNITPRICE = dt.Rows[0]["BUYUNITPRICE_MNY"].ToString();
                    HistoryData.m_strORDERUNIT = dt.Rows[0]["ORDERUNIT_VCHR"].ToString();
                    HistoryData.m_strORDERUNITPRICE = dt.Rows[0]["ORDERUNITPRICE_MNY"].ToString();
                    HistoryData.m_strORDERPKGQTY = dt.Rows[0]["ORDERPKGQTY_DEC"].ToString();
                    HistoryData.m_strSALEUNITPRICE = dt.Rows[0]["SALEUNITPRICE_MNY"].ToString();
                    HistoryData.m_strUNITPRICE_MNY = dt.Rows[0]["UNITPRICE_MNY"].ToString();
                    HistoryData.m_strWHOLESALEUNITPRICE = dt.Rows[0]["WHOLESALEUNITPRICE_MNY"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取某仓库的最大单据号
        /// <summary>
        /// 获取某仓库的最大单据号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMaxDoc"></param>
        /// <param name="strdate"></param>
        /// <param name="SIGN"></param>
        /// <param name="STORAGEID">仓库ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxDoc(out string p_strMaxDoc, string strdate, string SIGN, string STORAGEID)
        {
            p_strMaxDoc = null;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select max(DOCID_VCHR) as MaxDoc from t_opr_storageord where  SIGN_INT=" + SIGN + " and DOCID_VCHR like '" + strdate + "%' and STORAGEID_CHR='" + STORAGEID + "'";
            DataTable dtbResult = new DataTable();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbResult.Rows.Count > 0)
            {
                p_strMaxDoc = dtbResult.Rows[0]["MaxDoc"].ToString();
            }
            return lngRes;
        }
        #endregion


        #region 获取所有药库数据
        /// <summary>
        /// 获取所有药库数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objArrStorage"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllStorage(out clsStorage_VO[] objArrStorage)
        {
            objArrStorage = null;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select * from t_bse_storage";
            DataTable dtbResult = new DataTable();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbResult.Rows.Count > 0)
            {
                objArrStorage = new clsStorage_VO[dtbResult.Rows.Count];
                for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                {
                    objArrStorage[i1] = new clsStorage_VO();
                    objArrStorage[i1].m_strStroageID = dtbResult.Rows[i1]["STORAGEID_CHR"].ToString();
                    objArrStorage[i1].m_strStroageName = dtbResult.Rows[i1]["STORAGENAME_VCHR"].ToString();
                    if (dtbResult.Rows[i1]["STORAGEGROSSPROFIT_DEC"] != System.DBNull.Value)
                    {
                        objArrStorage[i1].m_decStorageGrossProfit = Decimal.Parse(dtbResult.Rows[i1]["STORAGEGROSSPROFIT_DEC"].ToString());
                    }
                    else
                    {
                        objArrStorage[i1].m_decStorageGrossProfit = 0;
                    }
                }
            }
            return lngRes;
        }
        #endregion


        #region 获取所有药房数据
        /// <summary>
        /// 获取所有药房数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtMedStorage"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllMedStorage(out DataTable dtMedStorage)
        {
            dtMedStorage = null;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select MEDSTOREID_CHR,MEDSTORENAME_VCHR from t_bse_medstore where MEDSTORETYPE_INT=1 order by MEDSTOREID_CHR";
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtMedStorage);
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

        #region 根据条件查询病人的类型
        /// <summary>
        /// 根据条件查询病人的类型
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="paytypeno"></param>
        /// <param name="payflag"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetPatinetTypeByCondition(out DataTable dt, string paytypeno, string payflag)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = "";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                bool b = false;
                strSQL = "select setstatus_int from t_sys_setting where setid_chr = '0063'";
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (dt.Rows.Count > 0)
                {
                    b = dt.Rows[0]["setstatus_int"].ToString().Trim() == "1";
                }

                if (b)
                {
                    Hashtable has = new Hashtable();
                    has.Add("monday", "1");
                    has.Add("tuesday", "2");
                    has.Add("wednesday", "3");
                    has.Add("thursday", "4");
                    has.Add("friday", "5");
                    has.Add("saturday", "6");
                    has.Add("sunday", "7");

                    b = false;

                    string NowWeekNo = has[DateTime.Now.DayOfWeek.ToString().ToLower()].ToString();

                    strSQL = "select timespan_vchr from t_opr_ybtimespan where weekno_int = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = NowWeekNo;

                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                    if (dt.Rows.Count == 1)
                    {
                        string TimeSpan = dt.Rows[0]["timespan_vchr"].ToString().Trim();

                        DateTime dte1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + TimeSpan.Substring(0, 5) + ":01");
                        DateTime dte2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + TimeSpan.Substring(6) + ":59");

                        if (DateTime.Now >= dte1 && DateTime.Now <= dte2)
                        {
                            b = true;
                        }
                    }
                }

                if (b)
                {
                    strSQL = @"select * 
                                from t_bse_patientpaytype 
                               where isusing_num = 1 
                                 and paytypename_vchr <> '特定医保' 
                                 and paytypeno_vchr like ?                                         
                                and (payflag_dec = 0 or payflag_dec = ?) 
                          order by paytypeno_vchr";
                }
                else
                {
                    strSQL = @"select * 
                                from t_bse_patientpaytype 
                               where isusing_num = 1 
                                 and paytypeno_vchr like ? 
                                and (payflag_dec = 0 or payflag_dec = ?) 
                          order by paytypeno_vchr";
                }

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = paytypeno + "%";
                ParamArr[1].Value = payflag;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据输入的卡号，关联出诊疗卡
        /// <summary>
        /// 根据输入的卡号，关联出诊疗卡
        /// </summary>
        /// <param name="p_strCardNo"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientCardNo(string p_strCardNo, out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = -1;
            clsHRPTableService objHRPSvc = null;
            string strSQL = @"select a.* ,b.patientcardid_chr
                                      from t_bse_patientcardtype a ,t_bse_patientcard b
                                      where a.patientid_chr = b.patientid_chr
                                      and a.paycardstatus_int = 1
                                      and b.status_int > 0
                                      and a.paycardno_vchr = ?";
            try
            {
                objHRPSvc = new clsHRPTableService();
                IDataParameter[] objIDPar = null;
                objHRPSvc.CreateDatabaseParameter(1, out objIDPar);
                objIDPar[0].Value = p_strCardNo.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objIDPar);
            }
            catch (Exception objEx)
            {
                clsLogText objLoger = new clsLogText();
                objLoger.LogError(objEx);
            }
            finally
            {
                if (objHRPSvc != null)
                {
                    objHRPSvc.Dispose();
                    objHRPSvc = null;
                }
            }
            return lngRes;
        }
        #endregion

        #region 获取病人扩展卡类型
        /// <summary>
        /// 获取病人扩展卡类型
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientExtendCardType(out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = -1;
            clsHRPTableService objHRPSvc = null;
            try
            {
                string strSql = @" select t.paycardtype_int, t.paycarddesc_vchr from t_bse_paycardtype t order by t.paycardtype_int";

                objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSql, ref p_dtbResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                clsLogText objLoger = new clsLogText();
                objLoger.LogError(objEx);
            }
            finally
            {
                if (objHRPSvc != null)
                {
                    objHRPSvc.Dispose();
                    objHRPSvc = null;
                }
            }
            return lngRes;
        }
        #endregion

        #region 通过扩展卡获取诊疗卡
        /// <summary>
        /// 通过扩展卡获取诊疗卡
        /// </summary>
        /// <param name="p_blnIsIdCard">是否身份证</param>
        /// <param name="p_intExtendCardType"></param>
        /// <param name="p_strExtendCardNo"></param>
        /// <param name="p_strPatientCard"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientCardByExtendCard(bool p_blnIsIdCard, int p_intExtendCardType, string p_strExtendCardNo, out string p_strPatientCard)
        {
            p_strPatientCard = "";
            long lngRes = -1;
            DataTable dtbTemp = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                if (p_blnIsIdCard)
                {
                    string strSql = @" select a.patientcardid_chr
  from t_bse_patient t, t_bse_patientcard a
 where t.patientid_chr = a.patientid_chr
   and a.status_int > 0
   and t.idcard_chr = ?
 order by a.issue_date desc ";

                    objHRPSvc = new clsHRPTableService();
                    IDataParameter[] objParamArr = null;
                    objHRPSvc.CreateDatabaseParameter(1, out objParamArr);
                    objParamArr[0].Value = p_strExtendCardNo;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtbTemp, objParamArr);
                    objHRPSvc.Dispose();
                }
                else
                {
                    string strSql = @" select a.patientcardid_chr
  from t_bse_patientcardtype t, t_bse_patientcard a
 where t.patientid_chr = a.patientid_chr
   and a.status_int > 0
   and t.paycardstatus_int = 1
   and t.paycardno_vchr = ?
   and t.paycardtype_int = ?
 order by a.issue_date desc";

                    objHRPSvc = new clsHRPTableService();
                    IDataParameter[] objParamArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out objParamArr);
                    objParamArr[0].Value = p_strExtendCardNo;
                    objParamArr[1].Value = p_intExtendCardType;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtbTemp, objParamArr);
                    objHRPSvc.Dispose();
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLoger = new clsLogText();
                objLoger.LogError(objEx);
            }
            finally
            {
                if (objHRPSvc != null)
                {
                    objHRPSvc.Dispose();
                    objHRPSvc = null;
                }
            }
            if (lngRes > 0 && dtbTemp != null && dtbTemp.Rows.Count > 0)
            {
                p_strPatientCard = dtbTemp.Rows[0]["patientcardid_chr"].ToString().Trim();
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
