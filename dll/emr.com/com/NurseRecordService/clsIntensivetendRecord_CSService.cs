using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService;

namespace com.digitalwave.clsIntensivetendRecord_CSService
{
    /// <summary>
    /// 新生儿科危重患者护理记录(茶山)
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsIntensivetendRecord_CSService: clsDiseaseTrackService
    {
        public clsIntensivetendRecord_CSService()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
     #region SQL语句
        /// <summary>
        /// 从INTENSIVETENDRECORD_CS获取指定病人的所有没有删除记录的时间。
        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
        /// </summary>
        private const string c_strGetTimeListSQL = @"select createDate, opendate
														from intensivetendrecord_cs
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

        /// <summary>
        /// 从INTENSIVETENDRECORD_CS中获取指定时间的表单,获取已经存在记录的主要信息
        /// InPatientID ,InPatientDate ,CreateDate,Status = 0
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select createuserid, opendate
															from intensivetendrecord_cs
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

        /// <summary>
        /// 从INTENSIVETENDRECORD_CS获取删除表单的主要信息。
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from intensivetendrecord_cs
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

        /// <summary>
        /// 添加记录到INTENSIVETENDRECORD_CS
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into  intensivetendrecord_cs
						                                      (inpatientid,
                                                               inpatientdate,
                                                               opendate,
                                                               createdate,
                                                               createuserid,
                                                               ifconfirm,
                                                               confirmreason,
                                                               confirmreasonxml,
                                                               status,
                                                               boxtemperature,
                                                               boxtemperaturexml,
                                                               temperature,
                                                               temperaturexml,
                                                               heartrate,
                                                               heartratexml,
                                                               respiration,
                                                               respirationxml,
                                                               bloodpress,
                                                               bloodpressxml,
                                                               spo2,
                                                               spo2xml,
                                                               mind,
                                                               pupil_sizeleft,
                                                               pupil_sizeleftxml,
                                                               pupil_sizeright,
                                                               pupil_sizerightxml,
                                                               pupil_reflectleft,
                                                               pupil_reflectright,
                                                               fontanel,
                                                               facecolor,
                                                               skincolor,
                                                               skinedema,
                                                               skinelasticity,
                                                               skinpattern,
                                                               skinedemaposition,
                                                               skinedemaproperty,
                                                               sequence_int,
                                                               recorddate) 
					                                    values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
							                                   ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?
                                                               )";

        /// <summary>
        /// 添加记录到INTENSIVETENDRECORD_CSCONTENT
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into  intensivetendrecord_cscontent
						                                          (inpatientid,
                                                                   inpatientdate,
                                                                   opendate,
                                                                   modifydate,
                                                                   modifyuserid,
                                                                   boxtemperatureright,
                                                                   temperatureright,
                                                                   heartrateright,
                                                                   respirationright,
                                                                   bloodpressright,
                                                                   mindright,
                                                                   pupil_sizeleftright,
                                                                   pupil_sizerightright,
                                                                   pupil_reflectleftright,
                                                                   pupil_reflectrightright,
                                                                   fontanelright,
                                                                   facecolorright,
                                                                   skincolorright,
                                                                   skinedemaright,
                                                                   skinelasticityright,
                                                                   skinpatternright,
                                                                   skindemapositionright,
                                                                   skindemapropertyright,
                                                                   spo2right)
					                                        values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
							                                       ?,?,?,?)";
        /// <summary>
        /// 添加记录到摄入信息表
        /// </summary>
        private const string c_strAddNewInpectInfoSQL = @"insert into nurserecordinceptinfo
                          (inpatientid,
                           inpatientdate,
                           formid,
                           opendate,
                           modifydate,
                           oralseq,
                           modifyuserid,
                           incept_kind,
                           incept_mete,status)
                        values(?,?,?,?,?,?,?,?,?,?)";
        /// <summary>
        /// 更新摄入信息表
        /// </summary>
        private const string c_strUpdateInpectInfoSQL = @"update nurserecordinceptinfo
                                    set status = 0
                                    where inpatientid = ? and inpatientdate = ? and formid =? and opendate =? and status = 1";
        /// <summary>
        /// 更新排出信息表
        /// </summary>
        private const string c_strUpdateEductionInfoSQL = @"update nurserecordeductioninfo
                                    set status = 0
                                    where inpatientid = ? and inpatientdate = ? and formid =?  and opendate =? and status = 1";

        /// <summary>
        /// 添加记录到排出信息表
        /// </summary>
        private const string c_strAddNewEductionInfoSQL = @"insert into nurserecordeductioninfo
                          (inpatientid,
                           inpatientdate,
                           formid,
                           opendate,
                           modifydate,
                           oralseq,
                           modifyuserid,
                           eduction_kind,
                           eduction_mete,status)
                        values(?,?,?,?,?,?,?,?,?,?)";
        /// <summary>
        /// 修改记录到INTENSIVETENDRECORD_CS
        /// </summary>
        private const string c_strModifyRecordSQL = @"update intensivetendrecord_cs set
                                               boxtemperature = ?,
                                               boxtemperaturexml = ?,
                                               temperature = ?,
                                               temperaturexml = ?,
                                               heartrate = ?,
                                               heartratexml = ?,
                                               respiration = ?,
                                               respirationxml = ?,
                                               bloodpress = ?,
                                               bloodpressxml = ?,
                                               spo2 = ?,
                                               spo2xml = ?,
                                               mind = ?,
                                               pupil_sizeleft = ?,
                                               pupil_sizeleftxml = ?,
                                               pupil_sizeright = ?,
                                               pupil_sizerightxml = ?,
                                               pupil_reflectleft = ?,
                                               pupil_reflectright = ?,
                                               fontanel = ?,
                                               facecolor = ?,
                                               skincolor = ?,
                                               skinedema = ?,
                                               skinelasticity = ?,
                                               skinpattern = ?,
                                               skinedemaposition = ?,
                                               skinedemaproperty = ?,
                                               sequence_int = ?
				where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        /// <summary>
        /// 修改记录到INTENSIVETENDRECORD_CSCONTENT
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

        /// <summary>
        /// 设置INTENSIVETENDRECORD_CS中删除记录的信息
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update intensivetendrecord_cs
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";
        /// <summary>
        /// 设置nurserecordeductioninfo中删除记录的信息
        /// </summary>
        private const string c_strDeleteEductionSQL = @"update nurserecordeductioninfo
														set status = 0
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 1";
        /// <summary>
        /// 设置nurserecordinceptinfo中删除记录的信息
        /// </summary>
        private const string c_strDeleteInceptSQL = @"update nurserecordinceptinfo
														set status = 0
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 1";
        /// <summary>
        /// 更新INTENSIVETENDRECORD_CS中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update intensivetendrecord_cs
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

        /// <summary>
        /// 从INTENSIVETENDRECORD_CS获取指定病人的所有指定删除者删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select createdate, opendate
																	from intensivetendrecord_cs
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

        /// <summary>
        /// 从INTENSIVETENDRECORD_CS获取指定病人的所有已经删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select createdate, opendate
																		from intensivetendrecord_cs
																	where inpatientid = ?
																		and inpatientdate = ?
																		and status = 1";
        #endregion

        /// <summary>
        /// 获取病人的该记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
            }
            return lngRes;
        }

        /// <summary>
        /// 更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
            }
            return lngRes;
        }

        /// <summary>
        /// 获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strDeleteUserID">删除者ID</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
            }
            return lngRes;
        }

        /// <summary>
        /// 获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeListAll(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
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
            return lngRes;
        }

        /// <summary>
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            string c_strGetRecordContentSQL = @"select t1.inpatientid,
                                                       t1.inpatientdate,
                                                       t1.opendate,
                                                       t1.createdate,
                                                       t1.createuserid,
                                                       t1.ifconfirm,
                                                       t1.confirmreason,
                                                       t1.confirmreasonxml,
                                                       t1.status,
                                                       t1.deactiveddate,
                                                       t1.deactivedoperatorid,
                                                       t1.firstprintdate,
                                                       t1.boxtemperature,
                                                       t1.boxtemperaturexml,
                                                       t1.temperature,
                                                       t1.temperaturexml,
                                                       t1.heartrate,
                                                       t1.heartratexml,
                                                       t1.respiration,
                                                       t1.respirationxml,
                                                       t1.bloodpress,
                                                       t1.bloodpressxml,
                                                       t1.spo2,
                                                       t1.spo2xml,
                                                       t1.mind,
                                                       t1.pupil_sizeleft,
                                                       t1.pupil_sizeleftxml,
                                                       t1.pupil_sizeright,
                                                       t1.pupil_sizerightxml,
                                                       t1.pupil_reflectleft,
                                                       t1.pupil_reflectright,
                                                       t1.fontanel,
                                                       t1.facecolor,
                                                       t1.skincolor,
                                                       t1.skinedema,
                                                       t1.skinelasticity,
                                                       t1.skinpattern,
                                                       t1.skinedemaposition,
                                                       t1.skinedemaproperty,
                                                       t1.sequence_int,
                                                       t1.recorddate,
                                                       t2.modifydate,
                                                       t2.modifyuserid,
                                                       t2.boxtemperatureright,
                                                       t2.temperatureright,
                                                       t2.heartrateright,
                                                       t2.respirationright,
                                                       t2.bloodpressright,
                                                       t2.mindright,
                                                       t2.pupil_sizeleftright,
                                                       t2.pupil_sizerightright,
                                                       t2.pupil_reflectleftright,
                                                       t2.pupil_reflectrightright,
                                                       t2.fontanelright,
                                                       t2.facecolorright,
                                                       t2.skincolorright,
                                                       t2.skinedemaright,
                                                       t2.skinelasticityright,
                                                       t2.skinpatternright,
                                                       t2.skindemapositionright,
                                                       t2.skindemapropertyright,
                                                       t2.spo2right
                                              from intensivetendrecord_cs t1, intensivetendrecord_cscontent t2
                                              where t1.inpatientid = t2.inpatientid
                                                       and t1.inpatientdate = t2.inpatientdate
                                                       and t1.opendate = t2.opendate
                                                       and t1.status = 0
                                                       and t1.inpatientid = ?
                                                       and t1.inpatientdate = ?
                                                       and t1.opendate = ?
                                                       and t2.modifydate = (select max(modifydate)
                                                                      from intensivetendrecord_cscontent
                                                                      where inpatientid = t2.inpatientid
                                                                               and inpatientdate = t2.inpatientdate
                                                                               and opendate = t2.opendate)";
            string m_strGetInpectInfoSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       modifydate,
       oralseq,
       modifyuserid,
       incept_kind,
       incept_mete
  from nurserecordinceptinfo where inpatientid = ? and inpatientdate =? and opendate =? and formid =? and status = 1";

            string m_strGetEductionInfoSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       modifydate,
       oralseq,
       modifyuserid,
       eduction_kind,
       eduction_mete
  from nurserecordeductioninfo where inpatientid = ? and inpatientdate =? and opendate =? and formid =? and status = 1";
            long lngRes = 0;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                IDataParameter[] objDPArr1 = null;
                p_objHRPServ.CreateDatabaseParameter(4, out objDPArr1);
                objDPArr1[0].Value = p_strInPatientID.Trim();
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr1[2].DbType = DbType.DateTime;
                objDPArr1[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr1[3].Value = "frmIntensivetendRecord_CSRec";
                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(4, out objDPArr2);
                objDPArr2[0].Value = p_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr2[3].Value = "frmIntensivetendRecord_CSRec";
                //生成DataTable
                DataTable dtbValue = new DataTable();
                DataTable dtbInpectValue = new DataTable();
                DataTable dtbEductionValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsIntensivetendRecordContent_CS objRecordContent = new clsIntensivetendRecordContent_CS();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    if (dtbValue.Rows[0]["IFCONFIRM"].ToString() == "")
                        objRecordContent.m_bytIfConfirm = 0;
                    else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    
                    objRecordContent.m_strConfirmReason = dtbValue.Rows[0]["CONFIRMREASON"].ToString();
                    objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();

                    //箱温
                    objRecordContent.m_strBOXTEMPERATURE_RIGHT = dtbValue.Rows[0]["BOXTEMPERATURERIGHT"].ToString();
                    objRecordContent.m_strBOXTEMPERATUREALL = dtbValue.Rows[0]["BOXTEMPERATURE"].ToString();
                    objRecordContent.m_strBOXTEMPERATUREXML = dtbValue.Rows[0]["BOXTEMPERATUREXML"].ToString();
                    //体温
                    objRecordContent.m_strTEMPERATURE_RIGHT = dtbValue.Rows[0]["TEMPERATURERIGHT"].ToString();
                    objRecordContent.m_strTEMPERATUREAll = dtbValue.Rows[0]["TEMPERATURE"].ToString();
                    objRecordContent.m_strTEMPERATUREXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
                    //心率
                    objRecordContent.m_strHEARTRATE_RIGHT = dtbValue.Rows[0]["HEARTRATERIGHT"].ToString();
                    objRecordContent.m_strHEARTRATE = dtbValue.Rows[0]["HEARTRATE"].ToString();
                    objRecordContent.m_strHEARTRATEXML = dtbValue.Rows[0]["HEARTRATEXML"].ToString();
                    //呼吸
                    objRecordContent.m_strRESPIRATION_RIGHT = dtbValue.Rows[0]["RESPIRATIONRIGHT"].ToString();
                    objRecordContent.m_strRESPIRATION = dtbValue.Rows[0]["RESPIRATION"].ToString();
                    objRecordContent.m_strRESPIRATIONXML = dtbValue.Rows[0]["RESPIRATIONXML"].ToString();
                    //血压
                    objRecordContent.m_strBLOODPRESSURES_RIGHT = dtbValue.Rows[0]["BLOODPRESSRIGHT"].ToString();
                    objRecordContent.m_strBLOODPRESSURES = dtbValue.Rows[0]["BLOODPRESS"].ToString();
                    objRecordContent.m_strBLOODPRESSURESXML = dtbValue.Rows[0]["BLOODPRESSXML"].ToString();
                    //spo2
                    objRecordContent.m_strSPO2_RIGHT = dtbValue.Rows[0]["SPO2RIGHT"].ToString();
                    objRecordContent.m_strSPO2 = dtbValue.Rows[0]["SPO2"].ToString();
                    objRecordContent.m_strSPO2XML = dtbValue.Rows[0]["SPO2XML"].ToString();
                    //神志
                    objRecordContent.m_strMind = dtbValue.Rows[0]["Mind"].ToString();
                    //瞳孔大小左
                    objRecordContent.m_strPupilSizeLeft_RIGHT = dtbValue.Rows[0]["PUPIL_SIZELEFTRIGHT"].ToString();
                    objRecordContent.m_strPupilSizeLeft = dtbValue.Rows[0]["PUPIL_SIZELEFT"].ToString();
                    objRecordContent.m_strPupilSizeLeftXML = dtbValue.Rows[0]["PUPIL_SIZELEFTXML"].ToString();
                    //瞳孔大小右
                    objRecordContent.m_strPupilSizeRight_RIGHT = dtbValue.Rows[0]["PUPIL_SIZERIGHTRIGHT"].ToString();
                    objRecordContent.m_strPupilSizeRight = dtbValue.Rows[0]["PUPIL_SIZERIGHT"].ToString();
                    objRecordContent.m_strPupilSizeRightXML = dtbValue.Rows[0]["PUPIL_SIZERIGHTXML"].ToString();
                    //瞳孔反射左
                    objRecordContent.m_strPupilReflectLeft = dtbValue.Rows[0]["PUPIL_REFLECTLEFTRIGHT"].ToString();
                    //瞳孔反射右
                    objRecordContent.m_strPupilReflectRight = dtbValue.Rows[0]["PUPIL_REFLECTRIGHTRIGHT"].ToString();
                    //囟门
                    objRecordContent.m_strFontanel = dtbValue.Rows[0]["FONTANELRIGHT"].ToString();
                    //面色
                    objRecordContent.m_strFaceColor = dtbValue.Rows[0]["FACECOLORRIGHT"].ToString();
                    //皮肤颜色
                    objRecordContent.m_strSkinColor = dtbValue.Rows[0]["SKINCOLORRIGHT"].ToString();
                    //皮肤硬肿
                    objRecordContent.m_strSkinEdema = dtbValue.Rows[0]["SKINEDEMARIGHT"].ToString();
                    //皮肤弹性
                    objRecordContent.m_strSkinLasticity = dtbValue.Rows[0]["SKINELASTICITYRIGHT"].ToString();
                    //皮肤花纹
                    objRecordContent.m_strSkinPattern = dtbValue.Rows[0]["SKINPATTERNRIGHT"].ToString();
                    //皮肤浮肿部位
                    objRecordContent.m_strSkinEdemaPosition = dtbValue.Rows[0]["SKINDEMAPOSITIONRIGHT"].ToString();
                    //皮肤浮肿性质
                    objRecordContent.m_strSkinEdemaProperty = dtbValue.Rows[0]["SKINDEMAPROPERTYRIGHT"].ToString();

                    //记录时间
                    objRecordContent.m_dtmRECORDDATE = DateTime.Parse(dtbValue.Rows[0]["RECORDDATE"].ToString());
                    //获取签名集合
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        //释放
                        objSign = null;
                    }
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(m_strGetInpectInfoSQL, ref dtbInpectValue, objDPArr1);
                    if (lngRes > 0 && dtbInpectValue.Rows.Count > 0)
                    {
                        clsNurseRecordInpectInfo[] objInpectArr = new clsNurseRecordInpectInfo[dtbInpectValue.Rows.Count];
                        for (int i = 0; i < dtbInpectValue.Rows.Count; i++)
                        {
                            objInpectArr[i] = new clsNurseRecordInpectInfo();
                            objInpectArr[i].m_strINPECT_KIND = dtbInpectValue.Rows[i]["incept_kind"].ToString();
                            objInpectArr[i].m_strINPECT_METE = dtbInpectValue.Rows[i]["incept_mete"].ToString();
                        }
                        objRecordContent.m_objInpectArr = objInpectArr;
                    }
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(m_strGetEductionInfoSQL, ref dtbEductionValue, objDPArr2);
                    if (lngRes > 0 && dtbEductionValue.Rows.Count > 0)
                    {
                        clsNurseRecordEductionInfo[] objEductionArr = new clsNurseRecordEductionInfo[dtbEductionValue.Rows.Count];
                        for (int j = 0; j < dtbEductionValue.Rows.Count; j++)
                        {
                            objEductionArr[j] = new clsNurseRecordEductionInfo();
                            objEductionArr[j].m_strEDUCTION_KIND = dtbEductionValue.Rows[j]["eduction_kind"].ToString();
                            objEductionArr[j].m_strEDUCTION_METE = dtbEductionValue.Rows[j]["eduction_mete"].ToString();
                        }
                        objRecordContent.m_objEductionArr = objEductionArr;
                    }
                    p_objRecordContent = objRecordContent;
                    #endregion
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 查看是否有相同的记录时间
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objPreModifyInfo">若有相同记录,返回该相同记录的操作信息,否则为空</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);
                //查看DataTable.Rows.Count
                //如果等于1，表示已经有该CreateDate，并且不是删除的记录。
                //获取该记录的信息，赋值到p_objModifyInfo中。返回值使用Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["OPENDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Exist;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 保存记录到数据库。添加主表,添加子表.
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            //获取签名流水号
            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
            clsIntensivetendRecordContent_CS objRecordContent = (clsIntensivetendRecordContent_CS)p_objRecordContent;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(38, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[4].Value = objRecordContent.m_strCreateUserID;
                objDPArr[5].Value = objRecordContent.m_bytIfConfirm;

                if (objRecordContent.m_strConfirmReason == null)
                    objDPArr[6].Value = DBNull.Value;
                else
                    objDPArr[6].Value = objRecordContent.m_strConfirmReason;
                if (objRecordContent.m_strConfirmReasonXML == null)
                    objDPArr[7].Value = DBNull.Value;
                else
                    objDPArr[7].Value = objRecordContent.m_strConfirmReasonXML;
                objDPArr[8].Value = 0;
                objDPArr[9].Value = objRecordContent.m_strBOXTEMPERATUREALL;
                objDPArr[10].Value = objRecordContent.m_strBOXTEMPERATUREXML;
                objDPArr[11].Value = objRecordContent.m_strTEMPERATUREAll;
                objDPArr[12].Value = objRecordContent.m_strTEMPERATUREXML;
                objDPArr[13].Value = objRecordContent.m_strHEARTRATE;
                objDPArr[14].Value = objRecordContent.m_strHEARTRATEXML;
                objDPArr[15].Value = objRecordContent.m_strRESPIRATION;
                objDPArr[16].Value = objRecordContent.m_strRESPIRATIONXML;
                objDPArr[17].Value = objRecordContent.m_strBLOODPRESSURES;
                objDPArr[18].Value = objRecordContent.m_strBLOODPRESSURESXML;
                objDPArr[19].Value = objRecordContent.m_strSPO2;
                objDPArr[20].Value = objRecordContent.m_strSPO2XML;
                objDPArr[21].Value = objRecordContent.m_strMind;
                objDPArr[22].Value = objRecordContent.m_strPupilSizeLeft;
                objDPArr[23].Value = objRecordContent.m_strPupilSizeLeftXML;
                objDPArr[24].Value = objRecordContent.m_strPupilSizeRight;
                objDPArr[25].Value = objRecordContent.m_strPupilSizeRightXML;
                objDPArr[26].Value = objRecordContent.m_strPupilReflectLeft;
                objDPArr[27].Value = objRecordContent.m_strPupilReflectRight;
                objDPArr[28].Value = objRecordContent.m_strFontanel;
                objDPArr[29].Value = objRecordContent.m_strFaceColor;
                objDPArr[30].Value = objRecordContent.m_strSkinColor;
                objDPArr[31].Value = objRecordContent.m_strSkinEdema;
                objDPArr[32].Value = objRecordContent.m_strSkinLasticity;
                objDPArr[33].Value = objRecordContent.m_strSkinPattern;
                objDPArr[34].Value = objRecordContent.m_strSkinEdemaPosition;
                objDPArr[35].Value = objRecordContent.m_strSkinEdemaProperty;

                objDPArr[36].Value = lngSequence;
                objDPArr[37].DbType = DbType.DateTime;
                objDPArr[37].Value = objRecordContent.m_dtmRECORDDATE;
                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(objRecordContent.objSignerArr, lngSequence);

                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(24, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[5].Value = objRecordContent.m_strBOXTEMPERATURE_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strTEMPERATURE_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strHEARTRATE_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strRESPIRATION_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strBLOODPRESSURES_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strMind;
                objDPArr2[11].Value = objRecordContent.m_strPupilSizeLeft_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strPupilSizeRight_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strPupilReflectLeft;
                objDPArr2[14].Value = objRecordContent.m_strPupilReflectRight;
                objDPArr2[15].Value = objRecordContent.m_strFontanel;
                objDPArr2[16].Value = objRecordContent.m_strFaceColor;
                objDPArr2[17].Value = objRecordContent.m_strSkinColor;
                objDPArr2[18].Value = objRecordContent.m_strSkinEdema;
                objDPArr2[19].Value = objRecordContent.m_strSkinLasticity;
                objDPArr2[20].Value = objRecordContent.m_strSkinPattern;
                objDPArr2[21].Value = objRecordContent.m_strSkinEdemaPosition;
                objDPArr2[22].Value = objRecordContent.m_strSkinEdemaProperty;
                objDPArr2[23].Value = objRecordContent.m_strSPO2_RIGHT;
                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                #region 摄入信息表
                IDataParameter[] objDPArr3 = null;
                if (objRecordContent.m_objInpectArr != null)
                {
                    for (int i = 0; i < objRecordContent.m_objInpectArr.Length; i++)
                    {
                        p_objHRPServ.CreateDatabaseParameter(10, out objDPArr3);
                        objDPArr3[0].Value = objRecordContent.m_strInPatientID;
                        objDPArr3[1].DbType = DbType.DateTime;
                        objDPArr3[1].Value = objRecordContent.m_dtmInPatientDate;
                        objDPArr3[2].Value = "frmIntensivetendRecord_CSRec";
                        objDPArr3[3].DbType = DbType.DateTime;
                        objDPArr3[3].Value = objRecordContent.m_dtmOpenDate;
                        objDPArr3[4].DbType = DbType.DateTime;
                        objDPArr3[4].Value = objRecordContent.m_dtmModifyDate;
                        objDPArr3[5].Value = i.ToString();
                        objDPArr3[6].Value = objRecordContent.m_strModifyUserID;
                        objDPArr3[7].Value = objRecordContent.m_objInpectArr[i].m_strINPECT_KIND;
                        objDPArr3[8].Value = objRecordContent.m_objInpectArr[i].m_strINPECT_METE;
                        objDPArr3[9].Value = 1;
                        //执行SQL			
                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewInpectInfoSQL, ref lngEff, objDPArr3);
                    }
                }
                #endregion

                #region 排出信息表
                if (objRecordContent.m_objEductionArr != null)
                {
                    for (int j = 0; j < objRecordContent.m_objEductionArr.Length; j++)
                    {
                        p_objHRPServ.CreateDatabaseParameter(10, out objDPArr3);
                        objDPArr3[0].Value = objRecordContent.m_strInPatientID;
                        objDPArr3[1].DbType = DbType.DateTime;
                        objDPArr3[1].Value = objRecordContent.m_dtmInPatientDate;
                        objDPArr3[2].Value = "frmIntensivetendRecord_CSRec";
                        objDPArr3[3].DbType = DbType.DateTime;
                        objDPArr3[3].Value = objRecordContent.m_dtmOpenDate;
                        objDPArr3[4].DbType = DbType.DateTime;
                        objDPArr3[4].Value = objRecordContent.m_dtmModifyDate;
                        objDPArr3[5].Value = j.ToString();
                        objDPArr3[6].Value = objRecordContent.m_strModifyUserID;
                        objDPArr3[7].Value = objRecordContent.m_objEductionArr[j].m_strEDUCTION_KIND;
                        objDPArr3[8].Value = objRecordContent.m_objEductionArr[j].m_strEDUCTION_METE;
                        objDPArr3[9].Value = 1;
                        //执行SQL			
                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewEductionInfoSQL, ref lngEff, objDPArr3);
                    }
                }
                #endregion
                //释放
                objSign = null;

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 查看当前记录是否最新的记录。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>		
        /// <param name="p_objModifyInfo">若当前记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            clsIntensivetendRecordContent_CS objRecordContent = (clsIntensivetendRecordContent_CS)p_objRecordContent;
            /// <summary>
            /// 从intensivetendrecord_cs获取指定表单的最后修改时间。
            /// </summary>
            string c_strCheckLastModifyRecordSQL = clsDatabaseSQLConvert.s_StrTop1 + @" t2.modifydate,t2.modifyuserid from intensivetendrecord_cs t1,intensivetendrecord_cscontent t2
			where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status = 0
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;

            long lngRes = 0;
            try
            {
                //获取IDataParameter数组			
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);
                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    return (long)enmOperationResult.DB_Succeed;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 把新修改的内容保存到数据库。更新主表,添加子表.
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            clsIntensivetendRecordContent_CS objRecordContent = (clsIntensivetendRecordContent_CS)p_objRecordContent;
            long lngRes = 0;
            try
            {
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(31, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strBOXTEMPERATUREALL;
                objDPArr[1].Value = objRecordContent.m_strBOXTEMPERATUREXML;
                objDPArr[2].Value = objRecordContent.m_strTEMPERATUREAll;
                objDPArr[3].Value = objRecordContent.m_strTEMPERATUREXML;
                objDPArr[4].Value = objRecordContent.m_strHEARTRATE;
                objDPArr[5].Value = objRecordContent.m_strHEARTRATEXML;
                objDPArr[6].Value = objRecordContent.m_strRESPIRATION;
                objDPArr[7].Value = objRecordContent.m_strRESPIRATIONXML;
                objDPArr[8].Value = objRecordContent.m_strBLOODPRESSURES;
                objDPArr[9].Value = objRecordContent.m_strBLOODPRESSURESXML;
                objDPArr[10].Value = objRecordContent.m_strSPO2;
                objDPArr[11].Value = objRecordContent.m_strSPO2XML;
                objDPArr[12].Value = objRecordContent.m_strMind;
                objDPArr[13].Value = objRecordContent.m_strPupilSizeLeft;
                objDPArr[14].Value = objRecordContent.m_strPupilSizeLeftXML;
                objDPArr[15].Value = objRecordContent.m_strPupilSizeRight;
                objDPArr[16].Value = objRecordContent.m_strPupilSizeRightXML;
                objDPArr[17].Value = objRecordContent.m_strPupilReflectLeft;
                objDPArr[18].Value = objRecordContent.m_strPupilReflectRight;
                objDPArr[19].Value = objRecordContent.m_strFontanel;
                objDPArr[20].Value = objRecordContent.m_strFaceColor;
                objDPArr[21].Value = objRecordContent.m_strSkinColor;
                objDPArr[22].Value = objRecordContent.m_strSkinEdema;
                objDPArr[23].Value = objRecordContent.m_strSkinLasticity;
                objDPArr[24].Value = objRecordContent.m_strSkinPattern;
                objDPArr[25].Value = objRecordContent.m_strSkinEdemaPosition;
                objDPArr[26].Value = objRecordContent.m_strSkinEdemaProperty;
                objDPArr[27].Value = lngSequence;
                objDPArr[28].Value = objRecordContent.m_strInPatientID;
                objDPArr[29].DbType = DbType.DateTime;
                objDPArr[29].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[30].DbType = DbType.DateTime;
                objDPArr[30].Value = objRecordContent.m_dtmOpenDate;
                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(objRecordContent.objSignerArr, lngSequence);
                if (lngRes <= 0) return lngRes;
                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(24, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[5].Value = objRecordContent.m_strBOXTEMPERATURE_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strTEMPERATURE_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strHEARTRATE_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strRESPIRATION_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strBLOODPRESSURES_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strMind;
                objDPArr2[11].Value = objRecordContent.m_strPupilSizeLeft_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strPupilSizeRight_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strPupilReflectLeft;
                objDPArr2[14].Value = objRecordContent.m_strPupilReflectRight;
                objDPArr2[15].Value = objRecordContent.m_strFontanel;
                objDPArr2[16].Value = objRecordContent.m_strFaceColor;
                objDPArr2[17].Value = objRecordContent.m_strSkinColor;
                objDPArr2[18].Value = objRecordContent.m_strSkinEdema;
                objDPArr2[19].Value = objRecordContent.m_strSkinLasticity;
                objDPArr2[20].Value = objRecordContent.m_strSkinPattern;
                objDPArr2[21].Value = objRecordContent.m_strSkinEdemaPosition;
                objDPArr2[22].Value = objRecordContent.m_strSkinEdemaProperty;
                objDPArr2[23].Value = objRecordContent.m_strSPO2_RIGHT;
                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
                IDataParameter[] objDPArr3 = null;
                IDataParameter[] objDPArr4 = null;
                if (objRecordContent.m_objInpectArr != null)
                {
                    //删除
                    p_objHRPServ.CreateDatabaseParameter(4, out objDPArr3);
                    objDPArr3[0].Value = objRecordContent.m_strInPatientID;
                    objDPArr3[1].DbType = DbType.DateTime;
                    objDPArr3[1].Value = objRecordContent.m_dtmInPatientDate;
                    objDPArr3[2].Value = objRecordContent.m_objInpectArr[0].m_strFORMID;
                    objDPArr3[3].DbType = DbType.DateTime;
                    objDPArr3[3].Value = objRecordContent.m_dtmOpenDate;
                    lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strUpdateInpectInfoSQL, ref lngEff, objDPArr3);
                    //插入
                    for (int j = 0; j < objRecordContent.m_objInpectArr.Length; j++)
                    {
                        p_objHRPServ.CreateDatabaseParameter(10, out objDPArr4);
                        objDPArr4[0].Value = objRecordContent.m_strInPatientID;
                        objDPArr4[1].DbType = DbType.DateTime;
                        objDPArr4[1].Value = objRecordContent.m_dtmInPatientDate;
                        objDPArr4[2].Value = objRecordContent.m_objInpectArr[j].m_strFORMID;
                        objDPArr4[3].DbType = DbType.DateTime;
                        objDPArr4[3].Value = objRecordContent.m_dtmOpenDate;
                        objDPArr4[4].DbType = DbType.DateTime;
                        objDPArr4[4].Value = objRecordContent.m_dtmModifyDate;
                        objDPArr4[5].Value = j.ToString();
                        objDPArr4[6].Value = objRecordContent.m_strModifyUserID;
                        objDPArr4[7].Value = objRecordContent.m_objInpectArr[j].m_strINPECT_KIND;
                        objDPArr4[8].Value = objRecordContent.m_objInpectArr[j].m_strINPECT_METE;
                        objDPArr4[9].Value = 1;
                        //执行SQL			
                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewInpectInfoSQL, ref lngEff, objDPArr4);
                    }

                }
                if (objRecordContent.m_objEductionArr != null)
                {
                    //删除
                    p_objHRPServ.CreateDatabaseParameter(4, out objDPArr3);
                    objDPArr3[0].Value = objRecordContent.m_strInPatientID;
                    objDPArr3[1].DbType = DbType.DateTime;
                    objDPArr3[1].Value = objRecordContent.m_dtmInPatientDate;
                    objDPArr3[2].Value = objRecordContent.m_objEductionArr[0].m_strFORMID;
                    objDPArr3[3].DbType = DbType.DateTime;
                    objDPArr3[3].Value = objRecordContent.m_dtmOpenDate;
                    lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strUpdateEductionInfoSQL, ref lngEff, objDPArr3);
                    //插入
                    for (int k = 0; k < objRecordContent.m_objEductionArr.Length; k++)
                    {
                        p_objHRPServ.CreateDatabaseParameter(10, out objDPArr4);
                        objDPArr4[0].Value = objRecordContent.m_strInPatientID;
                        objDPArr4[1].DbType = DbType.DateTime;
                        objDPArr4[1].Value = objRecordContent.m_dtmInPatientDate;
                        objDPArr4[2].Value = objRecordContent.m_objEductionArr[k].m_strFORMID;
                        objDPArr4[3].DbType = DbType.DateTime;
                        objDPArr4[3].Value = objRecordContent.m_dtmOpenDate;
                        objDPArr4[4].DbType = DbType.DateTime;
                        objDPArr4[4].Value = objRecordContent.m_dtmModifyDate;
                        objDPArr4[5].Value = k.ToString();
                        objDPArr4[6].Value = objRecordContent.m_strModifyUserID;
                        objDPArr4[7].Value = objRecordContent.m_objEductionArr[k].m_strEDUCTION_KIND;
                        objDPArr4[8].Value = objRecordContent.m_objEductionArr[k].m_strEDUCTION_METE;
                        objDPArr4[9].Value = 1;
                        //执行SQL			
                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewEductionInfoSQL, ref lngEff, objDPArr4);
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 把记录从数据中“删除”。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            clsIntensivetendRecordContent_CS objRecordContent = (clsIntensivetendRecordContent_CS)p_objRecordContent;
            long lngRes = 0;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = objRecordContent.m_dtmOpenDate;
                IDataParameter[] objDPArr1 = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr1);
                objDPArr1[0].Value = objRecordContent.m_strInPatientID;
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr1[2].DbType = DbType.DateTime;
                objDPArr1[2].Value = objRecordContent.m_dtmOpenDate;
                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
                if (lngRes > 0)
                {
                    lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteEductionSQL, ref lngEff, objDPArr1);
                }
                if (lngRes > 0)
                {
                    lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteInceptSQL, ref lngEff, objDPArr2);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取数据库中最新的修改时间和首次打印时间
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_dtmModifyDate">修改时间</param>
        /// <param name="p_strFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetModifyDateAndFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.Now;
            p_strFirstPrintDate = null;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            /// <summary>
            /// 从intensivetendrecord_cs和intensivetendrecord_cscontent获取LastModifyDate和FirstPrintDate
            /// </summary>
            string c_strGetModifyDateAndFirstPrintDateSQL = clsDatabaseSQLConvert.s_StrTop1 + @" a.firstprintdate,b.modifydate from intensivetendrecord_cs a,
					intensivetendrecord_cscontent b where inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and 
					a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate 
					order by b.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;
            long lngRes = 0;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取指定已经被删除记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            string c_strGetDeleteRecordContentSQL = @"select t1.inpatientid,
                                                       t1.inpatientdate,
                                                       t1.opendate,
                                                       t1.createdate,
                                                       t1.createuserid,
                                                       t1.ifconfirm,
                                                       t1.confirmreason,
                                                       t1.confirmreasonxml,
                                                       t1.status,
                                                       t1.deactiveddate,
                                                       t1.deactivedoperatorid,
                                                       t1.firstprintdate,
                                                       t1.boxtemperature,
                                                       t1.boxtemperaturexml,
                                                       t1.temperature,
                                                       t1.temperaturexml,
                                                       t1.heartrate,
                                                       t1.heartratexml,
                                                       t1.respiration,
                                                       t1.respirationxml,
                                                       t1.bloodpress,
                                                       t1.bloodpressxml,
                                                       t1.spo2,
                                                       t1.spo2xml,
                                                       t1.mind,
                                                       t1.pupil_sizeleft,
                                                       t1.pupil_sizeleftxml,
                                                       t1.pupil_sizeright,
                                                       t1.pupil_sizerightxml,
                                                       t1.pupil_reflectleft,
                                                       t1.pupil_reflectright,
                                                       t1.fontanel,
                                                       t1.facecolor,
                                                       t1.skincolor,
                                                       t1.skinedema,
                                                       t1.skinelasticity,
                                                       t1.skinpattern,
                                                       t1.skinedemaposition,
                                                       t1.skinedemaproperty,
                                                       t1.sequence_int,
                                                       t1.recorddate,
                                                       t2.modifydate,
                                                       t2.modifyuserid,
                                                       t2.boxtemperatureright,
                                                       t2.temperatureright,
                                                       t2.heartrateright,
                                                       t2.respirationright,
                                                       t2.bloodpressright,
                                                       t2.mindright,
                                                       t2.pupil_sizeleftright,
                                                       t2.pupil_sizerightright,
                                                       t2.pupil_reflectleftright,
                                                       t2.pupil_reflectrightright,
                                                       t2.fontanelright,
                                                       t2.facecolorright,
                                                       t2.skincolorright,
                                                       t2.skinedemaright,
                                                       t2.skinelasticityright,
                                                       t2.skinpatternright,
                                                       t2.skindemapositionright,
                                                       t2.skindemapropertyright,
                                                       t2.spo2right
                                              from intensivetendrecord_cs t1, intensivetendrecord_cscontent t2
                                              where t1.inpatientid = t2.inpatientid
                                                       and t1.inpatientdate = t2.inpatientdate
                                                       and t1.opendate = t2.opendate
                                                       and t1.status = 1
                                                       and t1.inpatientid = ?
                                                       and t1.inpatientdate = ?
                                                       and t1.opendate = ?
                                                       and t2.modifydate = (select max(modifydate)
                                                                      from intensivetendrecord_cscontent
                                                                      where inpatientid = t2.inpatientid
                                                                               and inpatientdate = t2.inpatientdate
                                                                               and opendate = t2.opendate)";

            long lngRes = 0;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsIntensivetendRecordContent_CS objRecordContent = new clsIntensivetendRecordContent_CS();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    if (dtbValue.Rows[0]["IFCONFIRM"].ToString() == "")
                        objRecordContent.m_bytIfConfirm = 0;
                    else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());

                    objRecordContent.m_strConfirmReason = dtbValue.Rows[0]["CONFIRMREASON"].ToString();
                    objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();
                    //箱温
                    objRecordContent.m_strBOXTEMPERATURE_RIGHT = dtbValue.Rows[0]["BOXTEMPERATURERIGHT"].ToString();
                    objRecordContent.m_strBOXTEMPERATUREALL = dtbValue.Rows[0]["BOXTEMPERATURE"].ToString();
                    objRecordContent.m_strBOXTEMPERATUREXML = dtbValue.Rows[0]["BOXTEMPERATUREXML"].ToString();
                    //体温
                    objRecordContent.m_strTEMPERATURE_RIGHT = dtbValue.Rows[0]["TEMPERATURERIGHT"].ToString();
                    objRecordContent.m_strTEMPERATUREAll = dtbValue.Rows[0]["TEMPERATURE"].ToString();
                    objRecordContent.m_strTEMPERATUREXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
                    //心率
                    objRecordContent.m_strHEARTRATE_RIGHT = dtbValue.Rows[0]["HEARTRATERIGHT"].ToString();
                    objRecordContent.m_strHEARTRATE = dtbValue.Rows[0]["HEARTRATE"].ToString();
                    objRecordContent.m_strHEARTRATEXML = dtbValue.Rows[0]["HEARTRATEXML"].ToString();
                    //呼吸
                    objRecordContent.m_strRESPIRATION_RIGHT = dtbValue.Rows[0]["RESPIRATIONRIGHT"].ToString();
                    objRecordContent.m_strRESPIRATION = dtbValue.Rows[0]["RESPIRATION"].ToString();
                    objRecordContent.m_strRESPIRATIONXML = dtbValue.Rows[0]["RESPIRATIONXML"].ToString();
                    //血压
                    objRecordContent.m_strBLOODPRESSURES_RIGHT = dtbValue.Rows[0]["BLOODPRESSRIGHT"].ToString();
                    objRecordContent.m_strBLOODPRESSURES = dtbValue.Rows[0]["BLOODPRESS"].ToString();
                    objRecordContent.m_strBLOODPRESSURESXML = dtbValue.Rows[0]["BLOODPRESSXML"].ToString();
                    //spo2
                    objRecordContent.m_strSPO2_RIGHT = dtbValue.Rows[0]["SPO2RIGHT"].ToString();
                    objRecordContent.m_strSPO2 = dtbValue.Rows[0]["SPO2"].ToString();
                    objRecordContent.m_strSPO2XML = dtbValue.Rows[0]["SPO2XML"].ToString();
                    //神志
                    objRecordContent.m_strMind = dtbValue.Rows[0]["Mind"].ToString();
                    //瞳孔大小左
                    objRecordContent.m_strPupilSizeLeft_RIGHT = dtbValue.Rows[0]["PUPIL_SIZELEFTRIGHT"].ToString();
                    objRecordContent.m_strPupilSizeLeft = dtbValue.Rows[0]["PUPIL_SIZELEFT"].ToString();
                    objRecordContent.m_strPupilSizeLeftXML = dtbValue.Rows[0]["PUPIL_SIZELEFTXML"].ToString();
                    //瞳孔大小右
                    objRecordContent.m_strPupilSizeRight_RIGHT = dtbValue.Rows[0]["PUPIL_SIZERIGHTRIGHT"].ToString();
                    objRecordContent.m_strPupilSizeRight = dtbValue.Rows[0]["PUPIL_SIZERIGHT"].ToString();
                    objRecordContent.m_strPupilSizeRightXML = dtbValue.Rows[0]["PUPIL_SIZERIGHTXML"].ToString();
                    //瞳孔反射左
                    objRecordContent.m_strPupilReflectLeft = dtbValue.Rows[0]["PUPIL_REFLECTLEFTRIGHT"].ToString();
                    //瞳孔反射右
                    objRecordContent.m_strPupilReflectRight = dtbValue.Rows[0]["PUPIL_REFLECTRIGHTRIGHT"].ToString();
                    //囟门
                    objRecordContent.m_strFontanel = dtbValue.Rows[0]["FONTANELRIGHT"].ToString();
                    //面色
                    objRecordContent.m_strFaceColor = dtbValue.Rows[0]["FACECOLORRIGHT"].ToString();
                    //皮肤颜色
                    objRecordContent.m_strSkinColor = dtbValue.Rows[0]["SKINCOLORRIGHT"].ToString();
                    //皮肤硬肿
                    objRecordContent.m_strSkinEdema = dtbValue.Rows[0]["SKINEDEMARIGHT"].ToString();
                    //皮肤弹性
                    objRecordContent.m_strSkinLasticity = dtbValue.Rows[0]["SKINELASTICITYRIGHT"].ToString();
                    //皮肤花纹
                    objRecordContent.m_strSkinPattern = dtbValue.Rows[0]["SKINPATTERNRIGHT"].ToString();
                    //皮肤浮肿部位
                    objRecordContent.m_strSkinEdemaPosition = dtbValue.Rows[0]["SKINDEMAPOSITIONRIGHT"].ToString();
                    //皮肤浮肿性质
                    objRecordContent.m_strSkinEdemaProperty = dtbValue.Rows[0]["SKINDEMAPROPERTYRIGHT"].ToString();
                    //记录时间
                    objRecordContent.m_dtmRECORDDATE = DateTime.Parse(dtbValue.Rows[0]["RECORDDATE"].ToString());
                    //获取签名集合
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        //释放
                        objSign = null;
                    }
                    p_objRecordContent = objRecordContent;
                    #endregion
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 更新病情记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateDetail(clsIntensivetendRecordContent_CSDetail p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strINPATIENTID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            try
            {
                string strSQL = @"update intensivetendrecord_csdetail 
								set recordcontent=?,recordcontentxml=? ,recordcontent_right=?,
                                sequence_int=?
								where inpatientid=? and inpatientdate=? and recorddate=? and status = 0";
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(7, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRECORDCONTENTAll;
                objDPArr[1].Value = p_objRecordContent.m_strRECORDCONTENTXML;
                objDPArr[2].Value = p_objRecordContent.m_strRECORDCONTENT_RIGHT;
                objDPArr[3].Value = lngSequence.ToString();
                objDPArr[4].Value = p_objRecordContent.m_strINPATIENTID;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objRecordContent.m_dtmINPATIENTDATE;
                objDPArr[6].DbType = DbType.DateTime;
                objDPArr[6].Value = p_objRecordContent.m_dtmRECORDDATE;
                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                if (lngRes < 0) return lngRes;
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);
                objSign = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取病情记录内容
        /// </summary>
        /// <param name="dtmRecordDate"></param>
        /// <param name="strInPatientID"></param>
        /// <param name="strRecordContent"></param>
        /// <param name="strRecordCotentXML"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDetail(DateTime dtmRecordDate, string strInPatientID, out string strRecordContent, out string strRecordCotentXML)
        {
            strRecordContent = "";
            strRecordCotentXML = "";
            long lngRes = 0;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select t.recordcontent_right, t.recordcontentxml
									from intensivetendrecord_csdetail t
								where recorddate = ?
									and inpatientid = ?
									and status = 0";
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = dtmRecordDate;
                objDPArr[1].Value = strInPatientID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    strRecordContent = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
                    strRecordCotentXML = dtbValue.Rows[0]["recordcontentxml"].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
            }
            return lngRes;
        }

        /// <summary>
        /// 保存病情记录内容
        /// </summary>
        /// <param name="p_objRecordContent">病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewDetail(clsIntensivetendRecordContent_CSDetail p_objRecordContent)
        {
            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strINPATIENTID == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            //获取签名流水号

            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
            try
            {
                string strSQL = @"insert into intensivetendrecord_csdetail (inpatientid,inpatientdate,opendate,
								createdate,createuserid,modifyuserid,recordcontent,recordcontentxml,recorddate,
								status,recordcontent_right,modifydate,sequence_int) 
								values (?,?,?,?,?,?,?,?,?,0,?,?,?)";
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(12, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strINPATIENTID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmINPATIENTDATE;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOPENDATE;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmCREATERECORDDATE;
                objDPArr[4].Value = p_objRecordContent.m_strCREATERECORDUSERID;
                objDPArr[5].Value = p_objRecordContent.m_strMODIFYRECORDUSERID;
                objDPArr[6].Value = p_objRecordContent.m_strRECORDCONTENTAll;
                objDPArr[7].Value = p_objRecordContent.m_strRECORDCONTENTXML;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = p_objRecordContent.m_dtmRECORDDATE;
                objDPArr[9].Value = p_objRecordContent.m_strRECORDCONTENT_RIGHT;
                objDPArr[10].DbType = DbType.DateTime;
                objDPArr[10].Value = p_objRecordContent.m_dtmMODIFYDATE;
                objDPArr[11].Value = lngSequence;
                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 修改病情记录内容
        /// </summary>
        /// <param name="p_objRecordContent">病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyDetail(clsIntensivetendRecordContent_CSDetail p_objRecordContent)
        {
            long lngRes = 0;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strINPATIENTID == null)
                    return (long)enmOperationResult.Parameter_Error;
                string strSQL = @"update intensivetendrecord_csdetail set opendate=?,
							modifyuserid=?,modifydate=?,recordcontent=?,recordcontentxml=?,recordcontent_right=?,
                            sequence_int=?
							where inpatientid=? and inpatientdate=? and recorddate=? and status = 0";
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(10, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmOPENDATE;
                objDPArr[1].Value = p_objRecordContent.m_strMODIFYRECORDUSERID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmMODIFYDATE;
                objDPArr[3].Value = p_objRecordContent.m_strRECORDCONTENTAll;
                objDPArr[4].Value = p_objRecordContent.m_strRECORDCONTENTXML;
                objDPArr[5].Value = p_objRecordContent.m_strRECORDCONTENT_RIGHT;
                objDPArr[6].Value = lngSequence.ToString();
                objDPArr[7].Value = p_objRecordContent.m_strINPATIENTID;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = p_objRecordContent.m_dtmINPATIENTDATE;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = p_objRecordContent.m_dtmRECORDDATE;//注意此处的创建时间为病程记录内容的创建时间
                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                if (lngRes < 0) return lngRes;
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);
                objSign = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
            }
            return lngRes;

        }

        /// <summary>
        ///  获取指定病情记录内容
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">住院日期</param>
        /// <param name="p_strRecordDate">创建日期</param>
        /// <param name="p_objRecordContent">病程记录内容</param>
        /// <returns>返回值</returns>
        [AutoComplete]
        public long m_lngGetRecordContent(string p_strInPatientID, string p_strInPatientDate, string p_strRecordDate,
            out clsIntensivetendRecordContent_CSDetail p_objRecordContent)
        {
            long lngRes = 0;
            p_objRecordContent = null;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select   t.inpatientid,
                                           t.inpatientdate,
                                           t.opendate,
                                           t.createdate,
                                           t.createuserid,
                                           t.modifyuserid,
                                           t.recordcontent,
                                           t.recordcontentxml,
                                           t.status,
                                           t.recordcontent_right,
                                           t.recorddate,
                                           t.modifydate,
                                           t.deactiveddate,
                                           t.deactivedoperatorid,
                                           f_getempnamebyno(t.createuserid) as lastname_vchr,
                                           t.sequence_int
                                  from intensivetendrecord_csdetail t
								  where  inpatientid=? and t.inpatientdate=?  and t.status=0 and t.recorddate=?";

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strRecordDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objRecordContent = new clsIntensivetendRecordContent_CSDetail();
                    p_objRecordContent.m_dtmCREATERECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["CreateDate"]);
                    p_objRecordContent.m_strCREATERECORDUSERID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objRecordContent.m_strDetailCreateUserName = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
                    p_objRecordContent.m_strRECORDCONTENTAll = dtbValue.Rows[0]["RecordContent"].ToString();
                    p_objRecordContent.m_strRECORDCONTENTXML = dtbValue.Rows[0]["RecordContentXML"].ToString();
                    p_objRecordContent.m_strRECORDCONTENT_RIGHT = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
                    p_objRecordContent.m_dtmMODIFYDATE = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                    p_objRecordContent.m_strMODIFYRECORDUSERID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    p_objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                    //获取签名集合
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out p_objRecordContent.objSignerArr);
                        //释放
                        objSign = null;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
            }
            return lngRes;
        }

        /// <summary>
        /// 删除指定病情记录内容
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strRecordDate"></param>
        /// <param name="p_strDelDate"></param>
        /// <param name="p_strDelID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteDetail(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strRecordDate,
            string p_strDelDate,
            string p_strDelID)
        {
            long lngRes = 0;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @" update intensivetendrecord_csdetail set status=1,deactiveddate=?,deactivedoperatorid=?
								where inpatientid=? and  inpatientdate=? and  recorddate=?";
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(p_strDelDate);
                objDPArr[1].Value = p_strDelID;
                objDPArr[2].Value = p_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strRecordDate);
                //执行查询 
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        ///  获取指定病人的病情记录内容
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">住院日期</param>
        /// <param name="p_strRecordContentArr">病程记录内容数组</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecordContentWithInpatient(string p_strInPatientID, string p_strInPatientDate,
            out string[][] p_strRecordContentArr)
        {
            long lngRes = 0;
            p_strRecordContentArr = null;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {

                string strSQL = @"select   t.inpatientid,
                                           t.inpatientdate,
                                           t.opendate,
                                           t.createdate,
                                           t.createuserid,
                                           t.modifyuserid,
                                           t.recordcontent,
                                           t.recordcontentxml,
                                           t.status,
                                           t.recordcontent_right,
                                           t.recorddate,
                                           t.modifydate,
                                           t.deactiveddate,
                                           t.deactivedoperatorid,
                                           f_getempnamebyno(t.createuserid) as lastname_vchr,
                                           t.sequence_int
							    from intensivetendrecord_csdetail t 
								where t.status = 0
									and t.inpatientid = ?
									and t.inpatientdate = ?";
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0)
                {
                    p_strRecordContentArr = new string[dtbValue.Rows.Count][];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_strRecordContentArr[i] = new string[7];
                        p_strRecordContentArr[i][0] = dtbValue.Rows[0]["RecordContent"].ToString();
                        p_strRecordContentArr[i][1] = dtbValue.Rows[0]["RecordContentXML"].ToString();
                        p_strRecordContentArr[i][2] = dtbValue.Rows[0]["RECORDDATE"].ToString();
                        p_strRecordContentArr[i][3] = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                        p_strRecordContentArr[i][4] = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
                        p_strRecordContentArr[i][5] = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        ///  获取指定病人的已删除病情记录内容
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">住院日期</param>
        /// <param name="p_strOpenDate">首次创建日期</param>
        /// <param name="p_objRecordContent">病程记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDelRecordContentWithInpatient(string p_strInPatientID, string p_strInPatientDate,
            string p_strOpenDate,
            out clsIntensivetendRecordContent_CSDetail p_objRecordContent)
        {
            long lngRes = 0;
            p_objRecordContent = null;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select   t.inpatientid,
                                           t.inpatientdate,
                                           t.opendate,
                                           t.createdate,
                                           t.createuserid,
                                           t.modifyuserid,
                                           t.recordcontent,
                                           t.recordcontentxml,
                                           t.status,
                                           t.recordcontent_right,
                                           t.recorddate,
                                           t.modifydate,
                                           t.deactiveddate,
                                           t.deactivedoperatorid,
                                           f_getempnamebyno(t.createuserid) as lastname_vchr,
                                            t.sequence_int
							    from intensivetendrecord_csdetail t 
								where t.status = 1
									and t.inpatientid = ?
									and t.inpatientdate = ?
									and t.opendate=?";
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0)
                {
                    p_objRecordContent = new clsIntensivetendRecordContent_CSDetail();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_objRecordContent.m_strRECORDCONTENT_RIGHT = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
                        p_objRecordContent.m_strRECORDCONTENTXML = dtbValue.Rows[0]["RecordContentXML"].ToString();
                        p_objRecordContent.m_dtmCREATERECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                        p_objRecordContent.m_strCREATERECORDUSERID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                        p_objRecordContent.m_strDetailCreateUserName = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
                        p_objRecordContent.m_strRECORDCONTENTAll = dtbValue.Rows[0]["RECORDCONTENT"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
            return lngRes;

        }
    }
}
