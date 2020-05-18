using System.EnterpriseServices;
using System;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;
using System.Data;

namespace com.digitalwave.BaseCaseHistorySevice
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public abstract class clsBaseCaseHistorySevice : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsBaseCaseHistorySevice()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // 获取病人的该记录时间列表。
        [AutoComplete]
        public abstract long m_lngGetRecordTimeList(string p_strInPatientID,
                                                     out string[] p_strInPatientDateArr,
                                                     out string[] p_strCreateRecordTimeArr,
                                                     out string[] p_strOpenRecordTimeArr);

        // 获取指定记录的内容。
        [AutoComplete]
        public long m_lngGetRecordContent(string p_strInPatientID,
                                           string p_strInPatientDate,
                                           /* string p_strOpenRecordTime, */
                                           out clsBaseCaseHistoryInfo p_objRecordContent,
                                           out clsPictureBoxValue[] p_objPicValueArr)
        {
            p_objRecordContent = null;
            p_objPicValueArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngGetRecordContent");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" /* || p_strOpenRecordTime==null || p_strOpenRecordTime=="" */)
                    return (long)enmOperationResult.Parameter_Error;


                lngRes = m_lngGetRecordContentWithServ(p_strInPatientID, p_strInPatientDate,/*p_strOpenRecordTime,*/objHRPServ, out p_objRecordContent, out p_objPicValueArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            //返回
            return lngRes;


        }

        // 添加新记录。
        // 1.生成 HRPServ。
        // 2.添加新记录。(m_lngAddNewRecordWithServ)
        [AutoComplete]
        public long m_lngAddNewRecord(clsBaseCaseHistoryInfo p_objRecordContent,
            clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, string p_strDeptID,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngAddNewRecord");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                lngRes = m_lngAddNewRecordWithServ(p_objRecordContent, p_objPicValueArr, p_strDiseaseID, p_strDeptID, objHRPServ, out p_objModifyInfo);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //返回
            return lngRes;


        }

        // 修改记录。
        // 1.生成 HRPServ。
        // 2.修改记录。(m_lngModifyRecordWithServ)
        [AutoComplete]
        public long m_lngModifyRecord(clsBaseCaseHistoryInfo p_objOldRecordContent,
            clsBaseCaseHistoryInfo p_objNewRecordContent, clsPictureBoxValue[] p_objPicValueArr,
            string p_strDiseaseID, string p_strDeptID,
            out clsPreModifyInfo p_objPreModifyInfo)
        {
            p_objPreModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngModifyRecord");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;


                lngRes = m_lngModifyRecordWithServ(p_objOldRecordContent, p_objNewRecordContent, objHRPServ, p_objPicValueArr, p_strDiseaseID, p_strDeptID, out p_objPreModifyInfo);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //返回
            return lngRes;


        }

        // 删除记录。
        // 1.生成 HRPServ。
        // 2.删除记录。(m_lngDeleteRecordWithServ)
        [AutoComplete]
        public long m_lngDeleteRecord(clsBaseCaseHistoryInfo p_objRecordContent,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngDeleteRecord");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                lngRes = m_lngDeleteRecordWithServ(p_objRecordContent, objHRPServ, out p_objModifyInfo);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            //返回
            return lngRes;

        }

        #region 旧的作废重做
        // 作废重做记录。
        // 1.生成 HRPServ。
        // 2.检查是否可以删除记录和添加记录。
        // 3.删除记录。
        // 4.添加记录。
        [AutoComplete]
        public long m_lngReAddNewRecord(clsBaseCaseHistoryInfo p_objDelRecord,
                                     clsBaseCaseHistoryInfo p_objAddNewRecord,
                                     out clsPreModifyInfo p_objPreModifyInfo)
        {
            p_objPreModifyInfo = null;

            long lngRes = 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngReAddNewRecord");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                lngRes = m_lngCheckLastModifyRecord(p_objDelRecord, objHRPServ, out p_objPreModifyInfo);

                if (lngRes <= 0)
                    return lngRes;

                lngRes = m_lngDeleteRecord2DB(p_objDelRecord, objHRPServ);

                if (lngRes <= 0)
                    return lngRes;

                //		return m_lngAddNewRecord2DB(p_objAddNewRecord,objHRP); 
                return 1;

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //返回
            return lngRes;

        }
        #endregion

        // 获取打印信息。
        // 1.获取打印内容：如果输入参数p_dtmModifyDate不是最新的ModifyDate，输出变量 p_objContent
        //   会存放最新的内容；否则，输出变量为null。
        // 2.获取打印时间：输出变量 p_dtmFirstPrintDate 存放首次打印时间。p_blnIsFirstPrint标记
        //   是否首次打印，如果是为true，客户端在打印后需要保存p_dtmFirstPrintDate到数据库。
        //   
        // 
        [AutoComplete]
        public long m_lngGetPrintInfo(string p_strInPatientID,
                string p_strInPatientDate,
                /*string p_strOpenRecordTime,*/DateTime p_dtmModifyDate,
                                   out clsBaseCaseHistoryInfo p_objContent,
                                   out clsPictureBoxValue[] p_objPicValueArr,
                                   out DateTime p_dtmFirstPrintDate,
                                   out bool p_blnIsFirstPrint)
        {
            //参数检查                     

            p_objContent = null;
            p_objPicValueArr = null;
            p_dtmFirstPrintDate = DateTime.MinValue;
            p_blnIsFirstPrint = false;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngGetPrintInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                //获取时间
                DateTime dtmModifyDate;
                string strFirstPrintDate;
                lngRes = m_lngGetModifyDateAndFirstPrintDate(p_strInPatientID, p_strInPatientDate,/*p_strOpenRecordTime,*/objHRPServ, out dtmModifyDate, out strFirstPrintDate);

                if (lngRes <= 0)
                    return lngRes;

                //判断dtmModifyDate和p_dtmModifyDate是否一致
                if (p_dtmModifyDate != dtmModifyDate)
                //如果不一致
                {
                    lngRes = m_lngGetRecordContentWithServ(p_strInPatientID, p_strInPatientDate,/*p_strOpenRecordTime,*/objHRPServ, out p_objContent, out p_objPicValueArr);
                    if (lngRes <= 0)
                        return lngRes;
                }
                //判断strFirstPrintDate是否为null或者不为null但为空值
                if (strFirstPrintDate == null || strFirstPrintDate == "")
                {//如果是
                    p_dtmFirstPrintDate = DateTime.Now;
                    p_blnIsFirstPrint = true;
                }
                else
                {//如果不是
                    p_dtmFirstPrintDate = DateTime.Parse(strFirstPrintDate);
                    p_blnIsFirstPrint = false;
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //返回
            return lngRes;


        }

        // 更新数据库中的首次打印时间。
        [AutoComplete]
        public abstract long m_lngUpdateFirstPrintDate(
                    string p_strInPatientID,
                    string p_strInPatientDate,
                    string p_strOpenDate,
                    DateTime p_dtmFirstPrintDate);

        // 获取病人的已经被删除记录时间列表。
        [AutoComplete]
        public abstract long m_lngGetDeleteRecordTimeList(
                string p_strInPatientID,
                                                           string p_strInPatientDate,
                                                           string p_strDeleteUserID,
                                                           out string[] p_strCreateRecordTimeArr,
                                                           out string[] p_strOpenRecordTimeArr);

        // 获取病人的已经被删除记录时间列表。
        [AutoComplete]
        public abstract long m_lngGetDeleteRecordTimeListAll(
                                                                string p_strInPatientID,
                                                              string p_strInPatientDate,
                                                              out string[] p_strCreateRecordTimeArr,
                                                              out string[] p_strOpenRecordTimeArr);

        // 获取指定已经被删除记录的内容。
        [AutoComplete]
        public long m_lngGetDeleteRecordContent(string p_strInPatientID,
                                                 string p_strInPatientDate,
                                                 string p_strOpenRecordTime,
                                                 out clsBaseCaseHistoryInfo p_objRecordContent)
        {
            p_objRecordContent = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngGetDeleteRecordContent");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;


                lngRes = m_lngGetDeleteRecordContentWithServ(p_strInPatientID, p_strInPatientDate, p_strOpenRecordTime, objHRPServ, out p_objRecordContent);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //返回
            return lngRes;


        }

        // 获取指定记录的内容。
        [AutoComplete]
        protected abstract long m_lngGetRecordContentWithServ(
                                                                string p_strInPatientID,
                                                               string p_strInPatientDate,
                                                               /*string p_strOpenRecordTime,*/
                                                               clsHRPTableService p_objHRPServ,
                                                               out clsBaseCaseHistoryInfo p_objRecordContent,
                                                                out clsPictureBoxValue[] p_objPicValueArr);

        // 添加新记录。
        // 1.先查看是否有相同的记录时间。（m_lngCheckCreateDate）
        // 2.添加记录到数据库。(m_lngAddNewRecord2DB)
        [AutoComplete]
        protected long m_lngAddNewRecordWithServ(clsBaseCaseHistoryInfo p_objRecordContent,
                                                    clsPictureBoxValue[] p_objPicValueArr,
                                                    string p_strDiseaseID, string p_strDeptID,
                                                  clsHRPTableService p_objHRPServ,
                                                  out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            try
            {

                if (p_objRecordContent == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                //m_mthDeleteAlreadyExistRecord(p_objRecordContent,p_objHRPServ);

                lngRes = m_lngAddNewRecord2DB(p_objRecordContent, p_objPicValueArr, p_strDiseaseID, p_strDeptID, p_objHRPServ);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;

        }

        /// <summary>
        /// 删除在同一时间已生成的记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        [AutoComplete]
        private void m_mthDeleteAlreadyExistRecord(clsBaseCaseHistoryInfo p_objRecordContent, clsHRPTableService p_objHRPServ)
        {
            //long lngRes = 0;
            try
            {
                string strSql = @"update inpatientcasehistory_history set status = 1 where 
				inpatientid = ? and inpatientdate = ? and status = 0";

                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;

                long lngEff = -1;
                p_objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }


        }

        // 查看是否有相同的记录时间
        [AutoComplete]
        protected abstract long m_lngCheckCreateDate(clsBaseCaseHistoryInfo p_objRecordContent,
                                                      clsHRPTableService p_objHRPServ,
                                                      out clsPreModifyInfo p_objPreModifyInfo);

        // 保存记录到数据库。
        [AutoComplete]
        protected abstract long m_lngAddNewRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
                                                        clsPictureBoxValue[] p_objPicValueArr,
                                                        string p_strDiseaseID, string p_strDeptID,
                                                      clsHRPTableService p_objHRPServ);

        // 修改记录。
        // 1.查看当前记录是否最新的记录。（m_lngCheckLastModifyRecord）
        // 2.把新修改的内容保存到数据库。(m_lngModifyRecord2DB)
        [AutoComplete]
        protected long m_lngModifyRecordWithServ(clsBaseCaseHistoryInfo p_objOldRecordContent,
                                              clsBaseCaseHistoryInfo p_objNewRecordContent,
                                              clsHRPTableService p_objHRPServ,
                                              clsPictureBoxValue[] p_objPicValueArr,
                                              string p_strDiseaseID, string p_strDeptID,
                                              out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            if (p_objNewRecordContent == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            try
            {
                lngRes = m_lngCheckLastModifyRecord(p_objOldRecordContent, p_objHRPServ, out p_objModifyInfo);

                if (lngRes <= 0)
                    return lngRes;

                lngRes = m_lngModifyRecord2DB(p_objNewRecordContent, p_objPicValueArr, p_strDiseaseID, p_strDeptID, p_objHRPServ);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;

        }

        // 查看当前记录是否最新的记录。
        [AutoComplete]
        protected abstract long m_lngCheckLastModifyRecord(clsBaseCaseHistoryInfo p_objRecordContent,
                    clsHRPTableService p_objHRPServ,
                    out clsPreModifyInfo p_objModifyInfo);


        // 把新修改的内容保存到数据库。
        [AutoComplete]
        protected abstract long m_lngModifyRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
                                                      clsPictureBoxValue[] p_objPicValueArr,
                                                      string p_strDiseaseID, string p_strDeptID,
                                                      clsHRPTableService p_objHRPServ);

        // 删除记录。
        // 1.查看当前记录是否最新的记录。（m_lngCheckLastModifyRecord）
        // 2.把记录从数据中“删除”。(m_lngDeleteRecord2DB)
        [AutoComplete]
        protected long m_lngDeleteRecordWithServ(clsBaseCaseHistoryInfo p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            try
            {
                if (p_objRecordContent == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                lngRes = m_lngCheckLastModifyRecord(p_objRecordContent, p_objHRPServ, out p_objModifyInfo);

                if (lngRes <= 0)
                    return lngRes;

                lngRes = m_lngDeleteRecord2DB(p_objRecordContent, p_objHRPServ);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;


        }

        // 把记录从数据中“删除”。
        [AutoComplete]
        protected abstract long m_lngDeleteRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
                                                  clsHRPTableService p_objHRPServ);

        // 获取数据库中最新的修改时间和首次打印时间
        [AutoComplete]
        protected abstract long m_lngGetModifyDateAndFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,/*string p_strOpenRecordTime,*/clsHRPTableService p_objHRPServ,
                                                             out DateTime p_dtmModifyDate,
                                                             out string p_strFirstPrintDate);

        // 获取指定已经被删除记录的内容。
        [AutoComplete]
        protected abstract long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
                                                                 string p_strInPatientDate,
                                                                 string p_strOpenRecordTime,
                                                                 clsHRPTableService p_objHRPServ,
                                                                 out clsBaseCaseHistoryInfo p_objRecordContent);

    }
}
