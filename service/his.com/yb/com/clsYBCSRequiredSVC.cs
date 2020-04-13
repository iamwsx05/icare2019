using System;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Collections;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace com.digitalwave.iCare.middletier.HIS
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsYBCSRequiredSVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsYBCSRequiredSVC()
        {

        }

        #region 保存门诊医保结算返回信息SP3_2004
        /// <summary>
        /// 保存医保结算返回信息SP3_2004
        /// </summary>
        /// <param name="objDgmzjsfhVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveYBChargeReturn(string strRegID, clsDGMzjsfh_VO objDgmzjsfhVo)
        {
            if (objDgmzjsfhVo == null)
            {
                return -1;
            }
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = string.Empty;
            #region sql
            strSQL = @"insert into t_ins_chargemz_csyb
                                  (registerid_chr,
                                   jzjlh,
                                   sdywh,
                                   cfh,
                                   ylfyze,
                                   tczf,
                                   grzfze,
                                   jsrq,
                                   xm,
                                   jisfs,
                                   yybh,
                                   gmsfhm,
                                   zyh,
                                   zfyy,
                                   jzlb,
                                   zh,
                                   mzyfbxje,
                                   createtime,
                                   charge_status,BCYLTCZF1,BCYLTCZF2,BCYLTCZF3,BCYLTCZF4,QTZHIFU,YBJZFPJE)
                                values
                                  (?, ?, ?, ?, ?, ?, ?, to_date(?,'yyyy-MM-dd hh24:mi:ss'), ?, ?, ?, ?, ?, ?, ?, ?, ?, sysdate, 1,?,?,?,?,?,?)";
            #endregion
            IDataParameter[] paraArr = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.CreateDatabaseParameter(23, out paraArr);
            #region 赋值
            paraArr[0].Value = strRegID;
            paraArr[1].Value = objDgmzjsfhVo.JZJLH;
            paraArr[2].Value = objDgmzjsfhVo.SDYWH;
            paraArr[3].Value = objDgmzjsfhVo.CFH;
            paraArr[4].Value = objDgmzjsfhVo.YLFYZE;
            paraArr[5].Value = objDgmzjsfhVo.TCZF;
            paraArr[6].Value = objDgmzjsfhVo.GRZFZE;
            paraArr[7].Value = objDgmzjsfhVo.JSRQ;
            paraArr[8].Value = objDgmzjsfhVo.XM;
            paraArr[9].Value = objDgmzjsfhVo.JISFS;
            paraArr[10].Value = objDgmzjsfhVo.YYBH;
            paraArr[11].Value = objDgmzjsfhVo.GMSFHM;
            paraArr[12].Value = objDgmzjsfhVo.ZYH;
            paraArr[13].Value = objDgmzjsfhVo.ZFYY;
            paraArr[14].Value = objDgmzjsfhVo.JZLB;
            paraArr[15].Value = objDgmzjsfhVo.ZH;
            paraArr[16].Value = objDgmzjsfhVo.MZYFBXJE;
            paraArr[17].Value = objDgmzjsfhVo.BCYLTCZF1;
            paraArr[18].Value = objDgmzjsfhVo.BCYLTCZF2;
            paraArr[19].Value = objDgmzjsfhVo.BCYLTCZF3;
            paraArr[20].Value = objDgmzjsfhVo.BCYLTCZF4;
            paraArr[21].Value = objDgmzjsfhVo.QTZHIFU;
            paraArr[22].Value = objDgmzjsfhVo.YBJZFPJE;
            #endregion
            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
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

        #region 保存住院医保登记返回信息
        /// <summary>
        /// 保存住院医保登记返回信息
        /// </summary>
        /// <param name="strRegID"></param>
        /// <param name="strJzjlh"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveYBZYRegInfo(string strRegID, string strJzjlh, string jbr, clsDGZydj_VO objDgmzjsfhVo)
        {
            if (objDgmzjsfhVo == null)
            {
                return -1;
            }
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = string.Empty;
            #region sql
            strSQL = @"insert into t_ins_cszyreg
                              (registerid_vchr,
                               jzjlh_vchr,
                               zylb_vchr,
                               jzlb_vchr,
                               wsbz_vchr,
                               zqqrqk_vchr,
                               zqqrsbh_vchr,
                               operatime_dat,
                               status_chr,
                               cybz_vchr,
                               cyks_vchr,
                               yycyks_vchr,
                               cyrq_dat,
                               ryrq_dat,
                               cbdtcqbm_vchr,
                               sbbz_int,
                               rydyzdby_vchr,
                               icd10_1,
                               icd10_2,
                               icd10_3,
                               inreason,
                               assitype,
                               outstatus,
                               recordoperid,
                               recorddate)
                            values
                              (?, ?, ?, ?, ?, ?, ?, sysdate, 0, ?,
                               ?, ?, to_date(?, 'yyyy-mm-dd'), to_date(?, 'yyyy-mm-dd'), ?, ?, ?, ?, ?, ?,
                               ?, ?, ?, ?, sysdate )";

            #endregion
            IDataParameter[] paraArr = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.CreateDatabaseParameter(22, out paraArr);
            #region 赋值
            int n = -1;
            paraArr[++n].Value = strRegID;
            paraArr[++n].Value = strJzjlh;
            paraArr[++n].Value = objDgmzjsfhVo.ZYLB;
            paraArr[++n].Value = objDgmzjsfhVo.JZLB;
            paraArr[++n].Value = objDgmzjsfhVo.WSBZ;
            paraArr[++n].Value = objDgmzjsfhVo.ZQQRQK;
            paraArr[++n].Value = objDgmzjsfhVo.ZQQRSBH;
            paraArr[++n].Value = objDgmzjsfhVo.CYBZ;
            paraArr[++n].Value = objDgmzjsfhVo.CYKS;
            paraArr[++n].Value = objDgmzjsfhVo.YYCYKS;
            paraArr[++n].Value = objDgmzjsfhVo.CYRQ;
            paraArr[++n].Value = objDgmzjsfhVo.RYRQ;
            paraArr[++n].Value = objDgmzjsfhVo.CBDTCQBM;
            paraArr[++n].Value = objDgmzjsfhVo.SBBZ;
            paraArr[++n].Value = objDgmzjsfhVo.RYDYZDBY;
            paraArr[++n].Value = objDgmzjsfhVo.Icd10_1;
            paraArr[++n].Value = objDgmzjsfhVo.Icd10_2;
            paraArr[++n].Value = objDgmzjsfhVo.Icd10_3;
            paraArr[++n].Value = objDgmzjsfhVo.InReason;
            paraArr[++n].Value = objDgmzjsfhVo.AssiType;
            paraArr[++n].Value = objDgmzjsfhVo.OutStatus;
            paraArr[++n].Value = jbr;
            #endregion
            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
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

        #region 保存医保住院结算返回信息[SP3_3004]
        /// <summary>
        /// 保存医保住院结算返回信息[SP3_3004]
        /// </summary>
        /// <param name="strRegID"></param>
        /// <param name="strInvNo"></param>
        /// <param name="objDgzyjsfhVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveYBChargeReturnZY(string strRegID, string strInvNo, clsDGZyjsfh_VO objDgzyjsfhVo, string strJSDYZDBY)
        {
            if (objDgzyjsfhVo == null)
            {
                return -1;
            }
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = string.Empty;
            #region sql
            strSQL = @"insert into t_ins_chargezy_csyb
                                  (registerid_chr,
                                   invoiceno_vchr,
                                   jzjlh,
                                   sdywh, 
                                   zyfyze,
                                   sbzfje,
                                   jbyltczf,
                                   bcyltczf,
                                   ylbz,
                                   jzje,
                                   dbyljzj,
                                   grzfeije,
                                   jslx,
                                   mzyfbxje,
                                   createtime,
                                   charge_status,BCYLTCZF1,BCYLTCZF2,BCYLTCZF3,BCYLTCZF4,QTZHIFU,YBJZFPJE)
                                values
                                  (?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, sysdate, 1,?,?,?,?,?,?)";
            #endregion
            IDataParameter[] paraArr = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.CreateDatabaseParameter(20, out paraArr);
            #region 赋值
            paraArr[0].Value = strRegID;
            paraArr[1].Value = strInvNo;
            paraArr[2].Value = objDgzyjsfhVo.JZJLH;
            paraArr[3].Value = objDgzyjsfhVo.SDYWH;
            paraArr[4].Value = objDgzyjsfhVo.ZYFYZE;
            paraArr[5].Value = objDgzyjsfhVo.SBZFJE;
            paraArr[6].Value = objDgzyjsfhVo.JBYLTCZF;
            paraArr[7].Value = objDgzyjsfhVo.BCYLTCZF;
            paraArr[8].Value = objDgzyjsfhVo.YLBZ;
            paraArr[9].Value = objDgzyjsfhVo.JZJE;
            paraArr[10].Value = objDgzyjsfhVo.DBYLJZJ;
            paraArr[11].Value = objDgzyjsfhVo.GRZFEIJE;
            paraArr[12].Value = objDgzyjsfhVo.JSLX;
            paraArr[13].Value = objDgzyjsfhVo.MZYFBXJE;
            paraArr[14].Value = objDgzyjsfhVo.BCYLTCZF1;
            paraArr[15].Value = objDgzyjsfhVo.BCYLTCZF2;
            paraArr[16].Value = objDgzyjsfhVo.BCYLTCZF3;
            paraArr[17].Value = objDgzyjsfhVo.BCYLTCZF4;
            paraArr[18].Value = objDgzyjsfhVo.QTZHIFU;
            paraArr[19].Value = objDgzyjsfhVo.YBJZFPJE;
            #endregion
            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
                if (lngRes > 0)
                {
                    m_lngUpdateYBRegisterStatusZY(strRegID, "2", strJSDYZDBY);
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

        #region 更新医保住院结算表状态
        /// <summary>
        /// 更新医保住院结算表状态
        /// </summary>
        /// <param name="strSdywh">结算序号</param>
        /// <param name="strStatusFlag">1结算，-1取消结算</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateYBChargeStatusZY(string strSdywh, string strRegID, string strStatusFlag)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = string.Empty;
            #region sql
            strSQL = @"update t_ins_chargezy_csyb set charge_status=? where sdywh=? and registerid_chr=?";
            #endregion
            IDataParameter[] paraArr = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(3, out paraArr);
                paraArr[0].Value = strStatusFlag;//1结算，-1取消结算
                paraArr[1].Value = strSdywh;
                paraArr[2].Value = strRegID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
                if (lngRes > 0)
                {
                    lngRes = m_lngUpdateYBRegisterStatusZY(strRegID, "3");
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

        #region 更新住院登记表状态
        /// <summary>
        /// 更新住院登记表状态
        /// </summary>
        /// <param name="strRegID"></param>
        /// <param name="strStatusFlag"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateYBRegisterStatusZY(string strRegID, string strStatusFlag)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = string.Empty;
            #region sql
            strSQL = @"update t_ins_cszyreg set status_chr = ?
                           where registerid_vchr = ?";
            #endregion
            IDataParameter[] paraArr = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out paraArr);
                paraArr[0].Value = strStatusFlag;
                paraArr[1].Value = strRegID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
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
        /// 更新住院登记表状态和结算第一诊断病因
        /// </summary>
        /// <param name="strRegID"></param>
        /// <param name="strStatusFlag"></param>
        /// <param name="strJSDYZDBY"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateYBRegisterStatusZY(string strRegID, string strStatusFlag, string strJSDYZDBY)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = string.Empty;
            #region sql
            strSQL = @"update t_ins_cszyreg set status_chr = ?, jsdyzdby_vchr = ?
                           where registerid_vchr = ?";
            #endregion
            IDataParameter[] paraArr = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(3, out paraArr);
                paraArr[0].Value = strStatusFlag;
                paraArr[1].Value = strJSDYZDBY;
                paraArr[2].Value = strRegID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
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

        #region 更新住院传送明细数据
        /// <summary>
        /// 更新住院传送明细数据
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateDgzyxmcs(List<clsDGZyxmcs_VO> lstDgzyxmcsVo)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = string.Empty;
            #region sql
            strSQL = @"update t_opr_bih_patientcharge a set a.sendflag_int=1 where a.pchargeid_chr =?";
            #endregion
            IDataParameter[] paraArr = null;
            clsHRPTableService objHRPSvc = null;
            DbType[] dbTypes = null;
            object[][] objValues = null;
            int n = 0;
            try
            {
                dbTypes = new DbType[] { DbType.String };
                objValues = new object[1][];

                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[lstDgzyxmcsVo.Count];//初始化
                }

                for (int k1 = 0; k1 < lstDgzyxmcsVo.Count; k1++)
                {
                    n = -1;
                    objValues[++n][k1] = lstDgzyxmcsVo[k1].CFXMWYH.ToString().Trim();
                }

                objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region 更新住院传送明细数据
        /// <summary>
        /// 更新住院传送明细数据
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateDgzyxmcs(string strRegID)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = @"update t_opr_bih_patientcharge a set a.sendflag_int=0 where a.registerid_chr =?";
            IDataParameter[] paraArr = null;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = strRegID.Trim();
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
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

        #region 保存医院经办人注册
        /// <summary>
        /// 保存医院经办人注册
        /// </summary>
        /// <param name="m_clsDGYBjbrzcVo">经办人注册VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveUserRegister(clsDGYBjbrzc_VO m_clsDGYBjbrzcVo)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = string.Empty;

            strSQL = @" update t_ins_registeruser a set a.bglx='0' where a.jbr=? ";
            IDataParameter[] paraArr = null;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = m_clsDGYBjbrzcVo.JBR;//经办人:向社保系统注册医院经办人用户ID

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);

                if (lngRes > 0)
                {
                    strSQL = @"insert into t_ins_registeruser
                                  (YYBH, JBR, JBRLX, XM, GMSFHM, SSKS, BGLX, JLSJ)
                                values
                                  (?, ?, ?, ?, ?, ?, ?, sysdate)";
                    paraArr = null;
                    objHRPSvc = new clsHRPTableService();
                    objHRPSvc.CreateDatabaseParameter(7, out paraArr);
                    #region 赋值
                    paraArr[0].Value = m_clsDGYBjbrzcVo.YYBH;//医院编号
                    paraArr[1].Value = m_clsDGYBjbrzcVo.JBR;//经办人:向社保系统注册医院经办人用户ID
                    paraArr[2].Value = m_clsDGYBjbrzcVo.JBRLX;//经办人类型:字典项1、普通经办人 2、门诊收费员 3、自助终端
                    paraArr[3].Value = m_clsDGYBjbrzcVo.XM;//姓名
                    paraArr[4].Value = m_clsDGYBjbrzcVo.GMSFHM;//公民身份号码
                    paraArr[5].Value = m_clsDGYBjbrzcVo.SSKS;//所属科室
                    paraArr[6].Value = m_clsDGYBjbrzcVo.BGLX;//变更类型:字典1、新增2、修改
                    #endregion
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
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

        #region 修改医院登录密码
        /// <summary>
        /// 修改医院登录密码
        /// </summary>
        /// <param name="p_strPwd">新密码</param>
        /// <param name="p_strYYBH">医院编码</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngHospitalUserLogin(string p_strPwd, string p_strYYBH)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = @" update t_ins_hospitaluser a set a.pwd=? where a.yybh=? ";
            IDataParameter[] paraArr = null;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out paraArr);
                paraArr[0].Value = p_strPwd;
                paraArr[1].Value = p_strYYBH;


                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
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

        #region 更新门诊结算退款的相关信息
        /// <summary>
        /// 更新门诊结算退款的相关信息
        /// </summary>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateCSYBChargeCancel(clsDGExtra_VO objDgextraVo)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = @"update t_ins_chargemz_csyb a set a.charge_status=-1 where a.sdywh=?";
            IDataParameter[] paraArr = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = objDgextraVo.SDYWH;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 修改住院登记信息
        /// <summary>
        /// 修改住院登记信息
        /// </summary>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateYBZYRegInfo(string strRegID, clsDGZydj_VO objDgzydjVo, clsDGExtra_VO objDgextraVo)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = @"update t_ins_cszyreg
                                   set zylb_vchr     = ?,
                                       jzlb_vchr     = ?,
                                       wsbz_vchr     = ?,
                                       zqqrqk_vchr   = ?,
                                       zqqrsbh_vchr  = ?,
                                       operatime_dat = sysdate,
                                       cybz_vchr     = ?,
                                       cyks_vchr     = ?,
                                       yycyks_vchr   = ?,
                                       cyrq_dat      = to_date(?, 'YYYY-MM-DD'),
                                       ryrq_dat      = to_date(?, 'YYYY-MM-DD'),
                                       cbdtcqbm_vchr = ?,
                                       rydyzdby_vchr = ?,
                                       icd10_1       = ?,
                                       icd10_2       = ?,
                                       icd10_3       = ?,
                                       inreason      = ?,
                                       assitype      = ?,
                                       outstatus     = ? 
                                 where jzjlh_vchr = ?
                                   and registerid_vchr = ?";
            IDataParameter[] paraArr = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                int n = -1;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(20, out paraArr);
                paraArr[++n].Value = objDgzydjVo.ZYLB;
                paraArr[++n].Value = objDgzydjVo.JZLB;
                paraArr[++n].Value = objDgzydjVo.WSBZ;
                paraArr[++n].Value = objDgzydjVo.ZQQRQK;
                paraArr[++n].Value = objDgzydjVo.ZQQRSBH;
                paraArr[++n].Value = objDgzydjVo.CYBZ;
                paraArr[++n].Value = objDgzydjVo.CYKS;
                paraArr[++n].Value = objDgzydjVo.YYCYKS;
                paraArr[++n].Value = objDgzydjVo.CYRQ;
                paraArr[++n].Value = objDgzydjVo.RYRQ;
                paraArr[++n].Value = objDgzydjVo.CBDTCQBM;
                paraArr[++n].Value = objDgzydjVo.RYDYZDBY;
                paraArr[++n].Value = objDgzydjVo.Icd10_1;
                paraArr[++n].Value = objDgzydjVo.Icd10_2;
                paraArr[++n].Value = objDgzydjVo.Icd10_3;
                paraArr[++n].Value = objDgzydjVo.InReason;
                paraArr[++n].Value = objDgzydjVo.AssiType;
                paraArr[++n].Value = objDgzydjVo.OutStatus;
                paraArr[++n].Value = objDgextraVo.JZJLH;
                paraArr[++n].Value = strRegID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 更新医保结算发票的相关信息
        /// <summary>
        /// 更新医保结算退款的相关信息
        /// </summary>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateCSYBChargeInfo(clsDGExtra_VO objDgextraVo, string strMZorZY)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = string.Empty;
            if (strMZorZY.Equals("1"))
            {
                strSQL = @"update t_ins_chargemz_csyb a set a.invoiceno_vchr=? where a.sdywh=?";
            }
            else
            {
                strSQL = @"update t_ins_chargezy_csyb a set a.invoiceno_vchr=? where a.sdywh=?";
            }
            IDataParameter[] paraArr = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out paraArr);
                paraArr[0].Value = objDgextraVo.FPHM;
                paraArr[1].Value = objDgextraVo.SDYWH;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p_intType"></param>
        /// <param name="p_strHospID"></param>
        /// <param name="p_strHospName"></param>
        /// <param name="p_strYBID"></param>
        /// <param name="p_strYBName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveData(int p_intType, string p_strHospID, string p_strHospName, string p_strYBID, string p_strYBName)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = string.Empty;
            IDataParameter[] paraArr = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                strSQL = @"insert into t_ins_deptrel
                                  (hosdeptid_vchr, hosdeptname_vchr, insdeptcode_vchr, insdeptname_vchr, type_int)
                                values
                                  (?, ?, ?, ?, ?)";
                paraArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(5, out paraArr);
                #region 赋值
                paraArr[0].Value = p_strHospID;
                paraArr[1].Value = p_strHospName;
                paraArr[2].Value = p_strYBID;
                paraArr[3].Value = p_strYBName;
                paraArr[4].Value = p_intType;
                #endregion
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
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

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="p_strHospID"></param>
        /// <param name="p_strYBID"></param>
        /// <param name="p_intType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelData(string p_strHospID, string p_strYBID, int p_intType)
        {
            long lngRes = 0;
            long lngEffct = 0;
            string strSQL = string.Empty;
            IDataParameter[] paraArr = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                strSQL = @"delete t_ins_deptrel t where t.hosdeptid_vchr = ? and t.insdeptcode_vchr =? and t.type_int = ?";
                paraArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(3, out paraArr);
                #region 赋值
                paraArr[0].Value = p_strHospID;
                paraArr[1].Value = p_strYBID;
                paraArr[2].Value = p_intType;
                #endregion
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffct, paraArr);
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
