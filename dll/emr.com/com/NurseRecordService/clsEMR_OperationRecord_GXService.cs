using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;

namespace com.digitalwave.DiseaseTrackService
{
    /// <summary>
    /// 手术护理记录
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_OperationRecord_GXService : clsDiseaseTrackService
    {
        #region SQL语句
        #region 获取指定病人的所有没有删除记录的时间
        /// <summary>
        /// 获取指定病人的所有没有删除记录的时间
        /// </summary>
        private const string c_strGetTimeListSQL = @"select createdate, opendate
                                                      from t_emr_oprecord_gx
                                                     where inpatientid = ?
                                                       and inpatientdate = ?
                                                       and status = 0"; 
        #endregion

        #region 根据指定表单的信息，查找表单的内容
        /// <summary>
        /// 根据指定表单的信息，查找表单的内容
        /// </summary>
        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.emr_seq,
       a.opname,
       a.opnamexml,
       a.ananame,
       a.ananamexml,
       a.liftsignafterop,
       a.skinfull,
       a.seepblood,
       a.posture,
       a.otherposture,
       a.otherposturexml,
       a.stanchstrap,
       a.foley,
       a.otherfoley,
       a.otherfoleyxml,
       a.skinantisepsis,
       a.otherskinantisepsis,
       a.otherskinantisepsisxml,
       a.wholeblood,
       a.wholebloodxml,
       a.redcell,
       a.redcellxml,
       a.plasm,
       a.plasmxml,
       a.selfblood,
       a.selfbloodxml,
       a.platelet,
       a.plateletxml,
       a.colddeposit,
       a.colddepositxml,
       a.otherblood,
       a.otherbloodxml,
       a.inliquid,
       a.inliquidxml,
       a.piss,
       a.pissxml,
       a.drain,
       a.skinbeforeop,
       a.skinbeforeop_desc,
       a.skinbeforeop_descxml,
       a.skinafterop,
       a.skinafterop_desc,
       a.skinafterop_descxml,
       a.sample,
       a.othersample,
       a.othersamplexml,
       a.afteropsend,
       a.oprecord,
       a.oprecordxml,
       a.sequence_int,
       a.recorddate,
       a.axenicbag,
       a.axenicbagxml,
       a.embedded,
       a.embeddedxml,
       a.guiding,
       a.healthedu,
       a.skinbeforeop_desc2,
       a.skinbeforeop_desc2xml,
       b.modifydate,
       b.modifyuserid,
       b.opname_right,
       b.ananame_right,
       b.otherposture_right,
       b.otherfoley_right,
       b.otherskinantisepsis_right,
       b.wholeblood_right,
       b.redcell_right,
       b.plasm_right,
       b.selfblood_right,
       b.platelet_right,
       b.colddeposit_right,
       b.otherblood_right,
       b.inliquid_right,
       b.piss_right,
       b.skinbeforeop_desc_right,
       b.skinafterop_desc_right,
       b.othersample_right,
       b.oprecord_right,
       b.axenicbag_right,
       b.embedded_right,
       b.skinbeforeop_desc2_right
  from t_emr_oprecord_gx a, t_emr_oprecordcon_gx b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 0
   and a.emr_seq = b.emr_seq
   and b.status = 0";
        /// <summary>
        /// 获取肢体充、放气及压力情况
        /// </summary>
        private const string c_strGetLimbContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.modifydate,
       a.modifyuserid,
       a.emr_seq,
       a.status,
       a.cubitus,
       a.leg,
       a.left,
       a.right,
       a.chargetime,
       a.deflatetime,
       a.alltime,
       a.press,
       a.orderid
  from t_emr_oprecordlimb_gx a
 where a.status = 0
   and a.emr_seq = ?
 order by orderid";
        #endregion

        #region 获取指定时间的表单
        /// <summary>
        /// 获取指定时间的表单
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select createuserid, opendate
                                                          from t_emr_oprecord_gx
                                                         where inpatientid = ?
                                                           and inpatientdate = ?
                                                           and createdate = ?
                                                           and status = 0"; 
        #endregion

        #region 获取指定表单的最后修改时间
        /// <summary>
        /// 获取指定表单的最后修改时间
        /// </summary>
        private const string c_strCheckLastModifyRecordSQL = @"select b.modifydate, b.modifyuserid
                                                              from t_emr_oprecordcon_gx b
                                                             where emr_seq = ?
                                                               and b.status = 0"; 
        #endregion

        #region 获取删除表单的主要信息
        /// <summary>
        /// 获取删除表单的主要信息
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
                                                          from t_emr_oprecord_gx
                                                         where inpatientid = ?
                                                           and inpatientdate = ?
                                                           and opendate = ?
                                                           and status = 1 ";        
        #endregion

        #region 添加记录到T_EMR_OPRECORD_GX
        /// <summary>
        /// 添加记录到T_EMR_OPRECORD_GX
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_oprecord_gx 
        (inpatientid,inpatientdate,opendate,createdate,createuserid,status,emr_seq,opname,opnamexml,ananame,ananamexml,
        liftsignafterop,skinfull,seepblood,posture,otherposture,otherposturexml,stanchstrap,foley,otherfoley,otherfoleyxml,
        skinantisepsis,otherskinantisepsis,otherskinantisepsisxml,wholeblood,wholebloodxml,redcell,redcellxml,plasm,plasmxml,
        selfblood,selfbloodxml,platelet,plateletxml,colddeposit,colddepositxml,otherblood,otherbloodxml,inliquid,inliquidxml,
        piss,pissxml,drain,skinbeforeop,skinbeforeop_desc,skinbeforeop_descxml,skinafterop,skinafterop_desc,skinafterop_descxml,
        sample,othersample,othersamplexml,afteropsend,oprecord,oprecordxml,sequence_int,recorddate,axenicbag,axenicbagxml,
        embedded,embeddedxml,guiding,healthedu,skinbeforeop_desc2,skinbeforeop_desc2xml) 
        values (?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?)"; 
        #endregion

        #region 添加记录到T_EMR_OPRECORDCON_GX
        /// <summary>
        /// 添加记录到T_EMR_OPRECORDCON_GX
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into t_emr_oprecordcon_gx 
        (inpatientid,inpatientdate,opendate,modifydate,modifyuserid,emr_seq,status,opname_right,ananame_right,otherposture_right,
        otherfoley_right,otherskinantisepsis_right,wholeblood_right,redcell_right,plasm_right,selfblood_right,platelet_right,
        colddeposit_right,otherblood_right,inliquid_right,piss_right,skinbeforeop_desc_right,skinafterop_desc_right,othersample_right,
        oprecord_right,axenicbag_right,embedded_right,skinbeforeop_desc2_right)
        values (?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?)"; 
        #endregion

        #region 添加记录到T_EMR_OPRECORDLIMB_GX
        /// <summary>
        /// 添加记录到T_EMR_OPRECORDLIMB_GX
        /// </summary>
        private const string c_strAddNewLimbContentSQL = @"insert into t_emr_oprecordlimb_gx 
        (inpatientid,inpatientdate,opendate,modifydate,modifyuserid,emr_seq,status,cubitus,leg,left,right,chargetime,deflatetime,alltime,press,orderid) 
        values (?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?)"; 
        #endregion

        #region 修改记录到T_EMR_OPRECORD_GX
        /// <summary>
        /// 修改记录到T_EMR_OPRECORD_GX
        /// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_oprecord_gx set 
        opname=?,opnamexml=?,ananame=?,ananamexml=?,liftsignafterop=?,skinfull=?,seepblood=?,posture=?,otherposture=?,otherposturexml=?,
        stanchstrap=?,foley=?,otherfoley=?,otherfoleyxml=?,skinantisepsis=?,otherskinantisepsis=?,otherskinantisepsisxml=?,wholeblood=?,
        wholebloodxml=?,redcell=?,redcellxml=?,plasm=?,plasmxml=?,selfblood=?,selfbloodxml=?,platelet=?,plateletxml=?,colddeposit=?,
        colddepositxml=?,otherblood=?,otherbloodxml=?,inliquid=?,inliquidxml=?,piss=?,pissxml=?,drain=?,skinbeforeop=?,skinbeforeop_desc=?,
        skinbeforeop_descxml=?,skinafterop=?,skinafterop_desc=?,skinafterop_descxml=?,sample=?,othersample=?,othersamplexml=?,afteropsend=?,
        oprecord=?,oprecordxml=?,sequence_int=? ,recorddate = ?,axenicbag = ?,axenicbagxml = ?,embedded = ?,embeddedxml = ?,guiding = ?,
        healthedu = ?,skinbeforeop_desc2 = ?,skinbeforeop_desc2xml = ?
        where emr_seq = ? and status=0"; 
        #endregion

        #region 修改记录到T_EMR_OPRECORDCON_GX
        /// <summary>
        /// 设置T_EMR_OPRECORDCON_GX旧记录状态为2
        /// </summary>
        private const string c_strSetOldRecordSQL = @"update t_emr_oprecordcon_gx set status = 2 where emr_seq = ? and status = 0";

        /// <summary>
        /// 修改记录到T_EMR_OPRECORDCON_GX
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;
        #endregion

        #region 修改记录到T_EMR_OPRECORDLIMB_GX
        /// <summary>
        /// 设置T_EMR_OPRECORDLIMB_GX旧记录状态为2
        /// </summary>
        private const string c_strSetOldLimbRecordSQL = @"update t_emr_oprecordlimb_gx set status = 2 where emr_seq = ? and status = 0";

        /// <summary>
        /// 修改记录到T_EMR_OPRECORDCON_GX
        /// </summary>
        private const string c_strModifyLimbContentSQL = c_strAddNewLimbContentSQL;
        #endregion

        #region 设置T_EMR_OPRECORD_GX中删除记录的信息
        /// <summary>
        /// 设置T_EMR_OPRECORD_GX中删除记录的信息
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_oprecord_gx
                                                       set status = 1, deactiveddate = ?, deactivedoperatorid = ?
                                                     where emr_seq = ?
                                                       and status = 0"; 
        #endregion

        #region 获取LastModifyDate和FirstPrintDate
        /// <summary>
        /// 获取LastModifyDate和FirstPrintDate
        /// </summary>
        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate, b.modifydate
                                                                      from t_emr_oprecord_gx a, t_emr_oprecordcon_gx b
                                                                     where a.inpatientid = ?
                                                                       and a.inpatientdate = ?
                                                                       and a.opendate = ?
                                                                       and a.status = 0
                                                                       and a.emr_seq = b.emr_seq
                                                                       and b.status = 0"; 
        #endregion

        #region 更新FirstPrintDate
        /// <summary>
        /// 更新FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update  t_emr_oprecord_gx 
															set firstprintdate= ? 
															where inpatientid= ? 
															and inpatientdate= ? 
															and opendate=? 
															and firstprintdate is null 
															and status=0"; 
        #endregion

        #region 获取指定病人的所有指定删除者删除的记录时间
        /// <summary>
        /// 获取指定病人的所有指定删除者删除的记录时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select createdate,opendate 
																from t_emr_oprecord_gx 
																where inpatientid = ? 
																and inpatientdate= ? 
																and deactivedoperatorid= ? 
																and status=1"; 
        #endregion

        #region 获取指定病人的所有已经删除的记录时间
        /// <summary>
        /// 获取指定病人的所有已经删除的记录时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select createdate,opendate 
																from t_emr_oprecord_gx 
																where inpatientid = ? 
																and inpatientdate= ? and status=1"; 
        #endregion 

        #region 获取已删除记录
        /// <summary>
        /// 获取已删除记录
        /// </summary>
        private const string c_strGetDeleteRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.emr_seq,
       a.opname,
       a.opnamexml,
       a.ananame,
       a.ananamexml,
       a.liftsignafterop,
       a.skinfull,
       a.seepblood,
       a.posture,
       a.otherposture,
       a.otherposturexml,
       a.stanchstrap,
       a.foley,
       a.otherfoley,
       a.otherfoleyxml,
       a.skinantisepsis,
       a.otherskinantisepsis,
       a.otherskinantisepsisxml,
       a.wholeblood,
       a.wholebloodxml,
       a.redcell,
       a.redcellxml,
       a.plasm,
       a.plasmxml,
       a.selfblood,
       a.selfbloodxml,
       a.platelet,
       a.plateletxml,
       a.colddeposit,
       a.colddepositxml,
       a.otherblood,
       a.otherbloodxml,
       a.inliquid,
       a.inliquidxml,
       a.piss,
       a.pissxml,
       a.drain,
       a.skinbeforeop,
       a.skinbeforeop_desc,
       a.skinbeforeop_descxml,
       a.skinafterop,
       a.skinafterop_desc,
       a.skinafterop_descxml,
       a.sample,
       a.othersample,
       a.othersamplexml,
       a.afteropsend,
       a.oprecord,
       a.oprecordxml,
       a.sequence_int,
       a.recorddate,
       a.axenicbag,
       a.axenicbagxml,
       a.embedded,
       a.embeddedxml,
       a.guiding,
       a.healthedu,
       a.skinbeforeop_desc2,
       a.skinbeforeop_desc2xml,
       b.modifydate,
       b.modifyuserid,
       b.opname_right,
       b.ananame_right,
       b.otherposture_right,
       b.otherfoley_right,
       b.otherskinantisepsis_right,
       b.wholeblood_right,
       b.redcell_right,
       b.plasm_right,
       b.selfblood_right,
       b.platelet_right,
       b.colddeposit_right,
       b.otherblood_right,
       b.inliquid_right,
       b.piss_right,
       b.skinbeforeop_desc_right,
       b.skinafterop_desc_right,
       b.othersample_right,
       b.oprecord_right,
       b.axenicbag_right,
       b.embedded_right,
       b.skinbeforeop_desc2_right
  from t_emr_oprecord_gx a, t_emr_oprecordcon_gx b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 1
   and a.emr_seq = b.emr_seq
   and b.status = 0";
        #endregion
        #endregion

        #region 获取病人的该记录时间列表
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
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
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }		//返回
            return lngRes;
        } 
        #endregion

        #region 更新数据库中的首次打印时间
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
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID.Trim();
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
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion

        #region 获取病人的已经被删除记录时间列表
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID.Trim();
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
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion

        #region 获取病人的已经被删除记录时间列表
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

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
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
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion

        #region 获取指定记录的内容
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    clsEMR_OperationRecord_GX objRecordContent = new clsEMR_OperationRecord_GX();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE"] == DBNull.Value)
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    if (dtbValue.Rows[0]["STATUS"] == DBNull.Value)
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    if (dtbValue.Rows[0]["EMR_SEQ"] == DBNull.Value)
                        return -1;
                    objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);

                    objRecordContent.m_strOPNAME = dtbValue.Rows[0]["OPNAME"].ToString();
                    objRecordContent.m_strOPNAMEXML = dtbValue.Rows[0]["OPNAMEXML"].ToString();
                    objRecordContent.m_strANANAME = dtbValue.Rows[0]["ANANAME"].ToString();
                    objRecordContent.m_strANANAMEXML = dtbValue.Rows[0]["ANANAMEXML"].ToString();
                    objRecordContent.m_strLIFTSIGNAFTEROP = dtbValue.Rows[0]["LIFTSIGNAFTEROP"].ToString();
                    objRecordContent.m_strSKINFULL = dtbValue.Rows[0]["SKINFULL"].ToString();
                    objRecordContent.m_strSEEPBLOOD = dtbValue.Rows[0]["SEEPBLOOD"].ToString();
                    objRecordContent.m_strPOSTURE = dtbValue.Rows[0]["POSTURE"].ToString();
                    objRecordContent.m_strOTHERPOSTURE = dtbValue.Rows[0]["OTHERPOSTURE"].ToString();
                    objRecordContent.m_strOTHERPOSTUREXML = dtbValue.Rows[0]["OTHERPOSTUREXML"].ToString();
                    objRecordContent.m_strSTANCHSTRAP = dtbValue.Rows[0]["STANCHSTRAP"].ToString();
                    objRecordContent.m_strFOLEY = dtbValue.Rows[0]["FOLEY"].ToString();
                    objRecordContent.m_strOTHERFOLEY = dtbValue.Rows[0]["OTHERFOLEY"].ToString();
                    objRecordContent.m_strOTHERFOLEYXML = dtbValue.Rows[0]["OTHERFOLEYXML"].ToString();
                    objRecordContent.m_strSKINANTISEPSIS = dtbValue.Rows[0]["SKINANTISEPSIS"].ToString();
                    objRecordContent.m_strOTHERSKINANTISEPSIS = dtbValue.Rows[0]["OTHERSKINANTISEPSIS"].ToString();
                    objRecordContent.m_strOTHERSKINANTISEPSISXML = dtbValue.Rows[0]["OTHERSKINANTISEPSISXML"].ToString();
                    objRecordContent.m_strWHOLEBLOOD = dtbValue.Rows[0]["WHOLEBLOOD"].ToString();
                    objRecordContent.m_strWHOLEBLOODXML = dtbValue.Rows[0]["WHOLEBLOODXML"].ToString();
                    objRecordContent.m_strREDCELL = dtbValue.Rows[0]["REDCELL"].ToString();
                    objRecordContent.m_strREDCELLXML = dtbValue.Rows[0]["REDCELLXML"].ToString();
                    objRecordContent.m_strPLASM = dtbValue.Rows[0]["PLASM"].ToString();
                    objRecordContent.m_strPLASMXML = dtbValue.Rows[0]["PLASMXML"].ToString();
                    objRecordContent.m_strSELFBLOOD = dtbValue.Rows[0]["SELFBLOOD"].ToString();
                    objRecordContent.m_strSELFBLOODXML = dtbValue.Rows[0]["SELFBLOODXML"].ToString();
                    objRecordContent.m_strPLATELET = dtbValue.Rows[0]["PLATELET"].ToString();
                    objRecordContent.m_strPLATELETXML = dtbValue.Rows[0]["PLATELETXML"].ToString();
                    objRecordContent.m_strCOLDDEPOSIT = dtbValue.Rows[0]["COLDDEPOSIT"].ToString();
                    objRecordContent.m_strCOLDDEPOSITXML = dtbValue.Rows[0]["COLDDEPOSITXML"].ToString();
                    objRecordContent.m_strOTHERBLOOD = dtbValue.Rows[0]["OTHERBLOOD"].ToString();
                    objRecordContent.m_strOTHERBLOODXML = dtbValue.Rows[0]["OTHERBLOODXML"].ToString();
                    objRecordContent.m_strINLIQUID = dtbValue.Rows[0]["INLIQUID"].ToString();
                    objRecordContent.m_strINLIQUIDXML = dtbValue.Rows[0]["INLIQUIDXML"].ToString();
                    objRecordContent.m_strPISS = dtbValue.Rows[0]["PISS"].ToString();
                    objRecordContent.m_strPISSXML = dtbValue.Rows[0]["PISSXML"].ToString();
                    objRecordContent.m_strDRAIN = dtbValue.Rows[0]["DRAIN"].ToString();
                    objRecordContent.m_strSKINBEFOREOP = dtbValue.Rows[0]["SKINBEFOREOP"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESC = dtbValue.Rows[0]["SKINBEFOREOP_DESC"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESCXML = dtbValue.Rows[0]["SKINBEFOREOP_DESCXML"].ToString();
                    objRecordContent.m_strSKINAFTEROP = dtbValue.Rows[0]["SKINAFTEROP"].ToString();
                    objRecordContent.m_strSKINAFTEROP_DESC = dtbValue.Rows[0]["SKINAFTEROP_DESC"].ToString();
                    objRecordContent.m_strSKINAFTEROP_DESCXML = dtbValue.Rows[0]["SKINAFTEROP_DESCXML"].ToString();
                    objRecordContent.m_strSAMPLE = dtbValue.Rows[0]["SAMPLE"].ToString();
                    objRecordContent.m_strOTHERSAMPLE = dtbValue.Rows[0]["OTHERSAMPLE"].ToString();
                    objRecordContent.m_strOTHERSAMPLEXML = dtbValue.Rows[0]["OTHERSAMPLEXML"].ToString();
                    objRecordContent.m_strAFTEROPSEND = dtbValue.Rows[0]["AFTEROPSEND"].ToString();
                    objRecordContent.m_strOPRECORD = dtbValue.Rows[0]["OPRECORD"].ToString();
                    objRecordContent.m_strOPRECORDXML = dtbValue.Rows[0]["OPRECORDXML"].ToString();
                    objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                    objRecordContent.m_strAXENICBAG = dtbValue.Rows[0]["AXENICBAG"].ToString();
                    objRecordContent.m_strAXENICBAGXML = dtbValue.Rows[0]["AXENICBAGXML"].ToString();
                    objRecordContent.m_strEMBEDDED = dtbValue.Rows[0]["EMBEDDED"].ToString();
                    objRecordContent.m_strEMBEDDEDXML = dtbValue.Rows[0]["EMBEDDEDXML"].ToString();
                    objRecordContent.m_strGUIDING = dtbValue.Rows[0]["GUIDING"].ToString();
                    objRecordContent.m_strHEALTHEDU = dtbValue.Rows[0]["HEALTHEDU"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESC2 = dtbValue.Rows[0]["SKINBEFOREOP_DESC2"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESC2XML = dtbValue.Rows[0]["SKINBEFOREOP_DESC2XML"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESC2_RIGHT = dtbValue.Rows[0]["SKINBEFOREOP_DESC2_RIGHT"].ToString();

                    objRecordContent.m_strOPNAME_RIGHT = dtbValue.Rows[0]["OPNAME_RIGHT"].ToString();
                    objRecordContent.m_strANANAME_RIGHT = dtbValue.Rows[0]["ANANAME_RIGHT"].ToString();
                    objRecordContent.m_strOTHERPOSTURE_RIGHT = dtbValue.Rows[0]["OTHERPOSTURE_RIGHT"].ToString();
                    objRecordContent.m_strOTHERFOLEY_RIGHT = dtbValue.Rows[0]["OTHERFOLEY_RIGHT"].ToString();
                    objRecordContent.m_strOTHERSKINANTISEPSIS_RIGHT = dtbValue.Rows[0]["OTHERSKINANTISEPSIS_RIGHT"].ToString();
                    objRecordContent.m_strWHOLEBLOOD_RIGHT = dtbValue.Rows[0]["WHOLEBLOOD_RIGHT"].ToString();
                    objRecordContent.m_strREDCELL_RIGHT = dtbValue.Rows[0]["REDCELL_RIGHT"].ToString();
                    objRecordContent.m_strPLASM_RIGHT = dtbValue.Rows[0]["PLASM_RIGHT"].ToString();
                    objRecordContent.m_strSELFBLOOD_RIGHT = dtbValue.Rows[0]["SELFBLOOD_RIGHT"].ToString();
                    objRecordContent.m_strPLATELET_RIGHT = dtbValue.Rows[0]["PLATELET_RIGHT"].ToString();
                    objRecordContent.m_strCOLDDEPOSIT_RIGHT = dtbValue.Rows[0]["COLDDEPOSIT_RIGHT"].ToString();
                    objRecordContent.m_strOTHERBLOOD_RIGHT = dtbValue.Rows[0]["OTHERBLOOD_RIGHT"].ToString();
                    objRecordContent.m_strINLIQUID_RIGHT = dtbValue.Rows[0]["INLIQUID_RIGHT"].ToString();
                    objRecordContent.m_strPISS_RIGHT = dtbValue.Rows[0]["PISS_RIGHT"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESC_RIGHT = dtbValue.Rows[0]["SKINBEFOREOP_DESC_RIGHT"].ToString();
                    objRecordContent.m_strSKINAFTEROP_DESC_RIGHT = dtbValue.Rows[0]["SKINAFTEROP_DESC_RIGHT"].ToString();
                    objRecordContent.m_strOTHERSAMPLE_RIGHT = dtbValue.Rows[0]["OTHERSAMPLE_RIGHT"].ToString();
                    objRecordContent.m_strOPRECORD_RIGHT = dtbValue.Rows[0]["OPRECORD_RIGHT"].ToString();
                    objRecordContent.m_strAXENICBAG_RIGHT = dtbValue.Rows[0]["AXENICBAG_RIGHT"].ToString();
                    objRecordContent.m_strEMBEDDED_RIGHT = dtbValue.Rows[0]["EMBEDDED_RIGHT"].ToString();

                    //获取签名集合
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //释放
                        objSign = null;
                    }

                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                    objDPArr[0].Value = objRecordContent.m_lngEMR_SEQ;
                    //生成DataTable
                    DataTable dtbLimb = new DataTable();
                    //执行查询，填充结果到DataTable
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetLimbContentSQL, ref dtbLimb, objDPArr);
                    if (dtbLimb != null && dtbLimb.Rows.Count > 0)
                    {
                        objRecordContent.m_objLimbInfoArr = new clsEMR_OperationRecordLimb_GX[dtbLimb.Rows.Count];
                        for (int i = 0; i < dtbLimb.Rows.Count; i++)
                        {
                            objRecordContent.m_objLimbInfoArr[i] = new clsEMR_OperationRecordLimb_GX();
                            objRecordContent.m_objLimbInfoArr[i].m_lngEMR_SEQ = objRecordContent.m_lngEMR_SEQ;
                            objRecordContent.m_objLimbInfoArr[i].m_dtmInPatientDate = objRecordContent.m_dtmInPatientDate;
                            objRecordContent.m_objLimbInfoArr[i].m_strInPatientID = objRecordContent.m_strInPatientID;
                            objRecordContent.m_objLimbInfoArr[i].m_dtmModifyDate = DateTime.Parse(p_strOpenDate);
                            objRecordContent.m_objLimbInfoArr[i].m_dtmModifyDate = DateTime.Parse(dtbLimb.Rows[i]["MODIFYDATE"].ToString());
                            objRecordContent.m_objLimbInfoArr[i].m_strModifyUserID = dtbLimb.Rows[i]["MODIFYUSERID"].ToString();
                            if (dtbLimb.Rows[i]["STATUS"] == DBNull.Value)
                                objRecordContent.m_objLimbInfoArr[i].m_bytStatus = 0;
                            else objRecordContent.m_objLimbInfoArr[i].m_bytStatus = Byte.Parse(dtbLimb.Rows[i]["STATUS"].ToString());

                            objRecordContent.m_objLimbInfoArr[i].m_strCUBITUS = dtbLimb.Rows[i]["CUBITUS"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strLEG = dtbLimb.Rows[i]["LEG"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strLEFT = dtbLimb.Rows[i]["LEFT"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strRIGHT = dtbLimb.Rows[i]["RIGHT"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strCHARGETIME = dtbLimb.Rows[i]["CHARGETIME"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strDEFLATETIME = dtbLimb.Rows[i]["DEFLATETIME"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strALLTIME = dtbLimb.Rows[i]["ALLTIME"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strPRESS = dtbLimb.Rows[i]["PRESS"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_intOrderID = Convert.ToInt32(dtbLimb.Rows[i]["ORDERID"]);
                        }
                    }

                    p_objRecordContent = objRecordContent;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion

        #region 查看是否有相同的记录时间
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

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
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
                //返回	
                return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion

        #region 保存记录到数据库。添加主表,添加子表.
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
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值	
                //获取签名流水号
                long lngSequence = 0;
                long lngSignSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSequence);
                lngRes = objSign.m_lngGetSequenceValue("seq_emr", out lngSequence);

                clsEMR_OperationRecord_GX objContent = (clsEMR_OperationRecord_GX)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(65, out objDPArr);

                objDPArr[0].Value = objContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmCreateDate;
                objDPArr[4].Value = objContent.m_strCreateUserID.Trim();
                objDPArr[5].Value = 0;
                objDPArr[6].Value = lngSequence;
                objDPArr[7].Value = objContent.m_strOPNAME;
                objDPArr[8].Value = objContent.m_strOPNAMEXML;
                objDPArr[9].Value = objContent.m_strANANAME;
                objDPArr[10].Value = objContent.m_strANANAMEXML;
                objDPArr[11].Value = objContent.m_strLIFTSIGNAFTEROP;
                objDPArr[12].Value = objContent.m_strSKINFULL;
                objDPArr[13].Value = objContent.m_strSEEPBLOOD;
                objDPArr[14].Value = objContent.m_strPOSTURE;
                objDPArr[15].Value = objContent.m_strOTHERPOSTURE;
                objDPArr[16].Value = objContent.m_strOTHERPOSTUREXML;
                objDPArr[17].Value = objContent.m_strSTANCHSTRAP;
                objDPArr[18].Value = objContent.m_strFOLEY;
                objDPArr[19].Value = objContent.m_strOTHERFOLEY;
                objDPArr[20].Value = objContent.m_strOTHERFOLEYXML;
                objDPArr[21].Value = objContent.m_strSKINANTISEPSIS;
                objDPArr[22].Value = objContent.m_strOTHERSKINANTISEPSIS;
                objDPArr[23].Value = objContent.m_strOTHERSKINANTISEPSISXML;
                objDPArr[24].Value = objContent.m_strWHOLEBLOOD;
                objDPArr[25].Value = objContent.m_strWHOLEBLOODXML;
                objDPArr[26].Value = objContent.m_strREDCELL;
                objDPArr[27].Value = objContent.m_strREDCELLXML;
                objDPArr[28].Value = objContent.m_strPLASM;
                objDPArr[29].Value = objContent.m_strPLASMXML;
                objDPArr[30].Value = objContent.m_strSELFBLOOD;
                objDPArr[31].Value = objContent.m_strSELFBLOODXML;
                objDPArr[32].Value = objContent.m_strPLATELET;
                objDPArr[33].Value = objContent.m_strPLATELETXML;
                objDPArr[34].Value = objContent.m_strCOLDDEPOSIT;
                objDPArr[35].Value = objContent.m_strCOLDDEPOSITXML;
                objDPArr[36].Value = objContent.m_strOTHERBLOOD;
                objDPArr[37].Value = objContent.m_strOTHERBLOODXML;
                objDPArr[38].Value = objContent.m_strINLIQUID;
                objDPArr[39].Value = objContent.m_strINLIQUIDXML;
                objDPArr[40].Value = objContent.m_strPISS;
                objDPArr[41].Value = objContent.m_strPISSXML;
                objDPArr[42].Value = objContent.m_strDRAIN;
                objDPArr[43].Value = objContent.m_strSKINBEFOREOP;
                objDPArr[44].Value = objContent.m_strSKINBEFOREOP_DESC;
                objDPArr[45].Value = objContent.m_strSKINBEFOREOP_DESCXML;
                objDPArr[46].Value = objContent.m_strSKINAFTEROP;
                objDPArr[47].Value = objContent.m_strSKINAFTEROP_DESC;
                objDPArr[48].Value = objContent.m_strSKINAFTEROP_DESCXML;
                objDPArr[49].Value = objContent.m_strSAMPLE;
                objDPArr[50].Value = objContent.m_strOTHERSAMPLE;
                objDPArr[51].Value = objContent.m_strOTHERSAMPLEXML;
                objDPArr[52].Value = objContent.m_strAFTEROPSEND;
                objDPArr[53].Value = objContent.m_strOPRECORD;
                objDPArr[54].Value = objContent.m_strOPRECORDXML;
                objDPArr[55].Value = lngSignSequence;
                objDPArr[56].Value = objContent.m_dtmRecordDate;
                objDPArr[57].Value = objContent.m_strAXENICBAG;
                objDPArr[58].Value = objContent.m_strAXENICBAGXML;
                objDPArr[59].Value = objContent.m_strEMBEDDED;
                objDPArr[60].Value = objContent.m_strEMBEDDEDXML;
                objDPArr[61].Value = objContent.m_strGUIDING;
                objDPArr[62].Value = objContent.m_strHEALTHEDU;
                objDPArr[63].Value = objContent.m_strSKINBEFOREOP_DESC2;
                objDPArr[64].Value = objContent.m_strSKINBEFOREOP_DESC2XML;


                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSignSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(28, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = lngSequence;
                objDPArr2[6].Value = 0;
                objDPArr2[7].Value = objContent.m_strOPNAME_RIGHT;
                objDPArr2[8].Value = objContent.m_strANANAME_RIGHT;
                objDPArr2[9].Value = objContent.m_strOTHERPOSTURE_RIGHT;
                objDPArr2[10].Value = objContent.m_strOTHERFOLEY_RIGHT;
                objDPArr2[11].Value = objContent.m_strOTHERSKINANTISEPSIS_RIGHT;
                objDPArr2[12].Value = objContent.m_strWHOLEBLOOD_RIGHT;
                objDPArr2[13].Value = objContent.m_strREDCELL_RIGHT;
                objDPArr2[14].Value = objContent.m_strPLASM_RIGHT;
                objDPArr2[15].Value = objContent.m_strSELFBLOOD_RIGHT;
                objDPArr2[16].Value = objContent.m_strPLATELET_RIGHT;
                objDPArr2[17].Value = objContent.m_strCOLDDEPOSIT_RIGHT;
                objDPArr2[18].Value = objContent.m_strOTHERBLOOD_RIGHT;
                objDPArr2[19].Value = objContent.m_strINLIQUID_RIGHT;
                objDPArr2[20].Value = objContent.m_strPISS_RIGHT;
                objDPArr2[21].Value = objContent.m_strSKINBEFOREOP_DESC_RIGHT;
                objDPArr2[22].Value = objContent.m_strSKINAFTEROP_DESC_RIGHT;
                objDPArr2[23].Value = objContent.m_strOTHERSAMPLE_RIGHT;
                objDPArr2[24].Value = objContent.m_strOPRECORD_RIGHT;
                objDPArr2[25].Value = objContent.m_strAXENICBAG_RIGHT;
                objDPArr2[26].Value = objContent.m_strEMBEDDED_RIGHT;
                objDPArr2[27].Value = objContent.m_strSKINBEFOREOP_DESC2_RIGHT;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);

                if (objContent.m_objLimbInfoArr != null && objContent.m_objLimbInfoArr.Length > 0)
                {
                    for (int l = 0; l < objContent.m_objLimbInfoArr.Length; l++)
                    {
                        objHRPServ.CreateDatabaseParameter(16, out objDPArr2);
                        objDPArr2[0].Value = objContent.m_objLimbInfoArr[l].m_strInPatientID;
                        objDPArr2[1].DbType = DbType.DateTime;
                        objDPArr2[1].Value = objContent.m_objLimbInfoArr[l].m_dtmInPatientDate;
                        objDPArr2[2].DbType = DbType.DateTime;
                        objDPArr2[2].Value = objContent.m_objLimbInfoArr[l].m_dtmOpenDate;
                        objDPArr2[3].DbType = DbType.DateTime;
                        objDPArr2[3].Value = objContent.m_objLimbInfoArr[l].m_dtmModifyDate;
                        objDPArr2[4].Value = objContent.m_objLimbInfoArr[l].m_strModifyUserID;
                        objDPArr2[5].Value = lngSequence;
                        objDPArr2[6].Value = 0;
                        objDPArr2[7].Value = objContent.m_objLimbInfoArr[l].m_strCUBITUS;
                        objDPArr2[8].Value = objContent.m_objLimbInfoArr[l].m_strLEG;
                        objDPArr2[9].Value = objContent.m_objLimbInfoArr[l].m_strLEFT;
                        objDPArr2[10].Value = objContent.m_objLimbInfoArr[l].m_strRIGHT;
                        objDPArr2[11].Value = objContent.m_objLimbInfoArr[l].m_strCHARGETIME;
                        objDPArr2[12].Value = objContent.m_objLimbInfoArr[l].m_strDEFLATETIME;
                        objDPArr2[13].Value = objContent.m_objLimbInfoArr[l].m_strALLTIME;
                        objDPArr2[14].Value = objContent.m_objLimbInfoArr[l].m_strPRESS;
                        objDPArr2[15].Value = objContent.m_objLimbInfoArr[l].m_intOrderID;

                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewLimbContentSQL, ref lngEff, objDPArr2);
                    }
                }
                //释放
                objSign = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion

        #region 查看当前记录是否最新的记录
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsEMR_OperationRecord_GX objContent = p_objRecordContent as clsEMR_OperationRecord_GX;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = objContent.m_lngEMR_SEQ;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = objContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = objContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = objContent.m_dtmOpenDate;

                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Modify;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion

        #region 把新修改的内容保存到数据库
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值	
                //获取签名流水号
                long lngSignSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSequence);

                clsEMR_OperationRecord_GX objContent = (clsEMR_OperationRecord_GX)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(59, out objDPArr);
                objDPArr[0].Value = objContent.m_strOPNAME;
                objDPArr[1].Value = objContent.m_strOPNAMEXML;
                objDPArr[2].Value = objContent.m_strANANAME;
                objDPArr[3].Value = objContent.m_strANANAMEXML;
                objDPArr[4].Value = objContent.m_strLIFTSIGNAFTEROP;
                objDPArr[5].Value = objContent.m_strSKINFULL;
                objDPArr[6].Value = objContent.m_strSEEPBLOOD;
                objDPArr[7].Value = objContent.m_strPOSTURE;
                objDPArr[8].Value = objContent.m_strOTHERPOSTURE;
                objDPArr[9].Value = objContent.m_strOTHERPOSTUREXML;
                objDPArr[10].Value = objContent.m_strSTANCHSTRAP;
                objDPArr[11].Value = objContent.m_strFOLEY;
                objDPArr[12].Value = objContent.m_strOTHERFOLEY;
                objDPArr[13].Value = objContent.m_strOTHERFOLEYXML;
                objDPArr[14].Value = objContent.m_strSKINANTISEPSIS;
                objDPArr[15].Value = objContent.m_strOTHERSKINANTISEPSIS;
                objDPArr[16].Value = objContent.m_strOTHERSKINANTISEPSISXML;
                objDPArr[17].Value = objContent.m_strWHOLEBLOOD;
                objDPArr[18].Value = objContent.m_strWHOLEBLOODXML;
                objDPArr[19].Value = objContent.m_strREDCELL;
                objDPArr[20].Value = objContent.m_strREDCELLXML;
                objDPArr[21].Value = objContent.m_strPLASM;
                objDPArr[22].Value = objContent.m_strPLASMXML;
                objDPArr[23].Value = objContent.m_strSELFBLOOD;
                objDPArr[24].Value = objContent.m_strSELFBLOODXML;
                objDPArr[25].Value = objContent.m_strPLATELET;
                objDPArr[26].Value = objContent.m_strPLATELETXML;
                objDPArr[27].Value = objContent.m_strCOLDDEPOSIT;
                objDPArr[28].Value = objContent.m_strCOLDDEPOSITXML;
                objDPArr[29].Value = objContent.m_strOTHERBLOOD;
                objDPArr[30].Value = objContent.m_strOTHERBLOODXML;
                objDPArr[31].Value = objContent.m_strINLIQUID;
                objDPArr[32].Value = objContent.m_strINLIQUIDXML;
                objDPArr[33].Value = objContent.m_strPISS;
                objDPArr[34].Value = objContent.m_strPISSXML;
                objDPArr[35].Value = objContent.m_strDRAIN;
                objDPArr[36].Value = objContent.m_strSKINBEFOREOP;
                objDPArr[37].Value = objContent.m_strSKINBEFOREOP_DESC;
                objDPArr[38].Value = objContent.m_strSKINBEFOREOP_DESCXML;
                objDPArr[39].Value = objContent.m_strSKINAFTEROP;
                objDPArr[40].Value = objContent.m_strSKINAFTEROP_DESC;
                objDPArr[41].Value = objContent.m_strSKINAFTEROP_DESCXML;
                objDPArr[42].Value = objContent.m_strSAMPLE;
                objDPArr[43].Value = objContent.m_strOTHERSAMPLE;
                objDPArr[44].Value = objContent.m_strOTHERSAMPLEXML;
                objDPArr[45].Value = objContent.m_strAFTEROPSEND;
                objDPArr[46].Value = objContent.m_strOPRECORD;
                objDPArr[47].Value = objContent.m_strOPRECORDXML;
                objDPArr[48].Value = lngSignSequence;
                objDPArr[49].Value = objContent.m_dtmRecordDate;
                objDPArr[50].Value = objContent.m_strAXENICBAG;
                objDPArr[51].Value = objContent.m_strAXENICBAGXML;
                objDPArr[52].Value = objContent.m_strEMBEDDED;
                objDPArr[53].Value = objContent.m_strEMBEDDEDXML;
                objDPArr[54].Value = objContent.m_strGUIDING;
                objDPArr[55].Value = objContent.m_strHEALTHEDU;
                objDPArr[56].Value = objContent.m_strSKINBEFOREOP_DESC2;
                objDPArr[57].Value = objContent.m_strSKINBEFOREOP_DESC2XML;
                objDPArr[58].Value = objContent.m_lngEMR_SEQ;


                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSignSequence);

                //设置旧记录状态为2
                IDataParameter[] objDPArrStatus = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArrStatus);
                objDPArrStatus[0].Value = objContent.m_lngEMR_SEQ;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strSetOldRecordSQL, ref lngEff, objDPArrStatus);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(28, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_lngEMR_SEQ; 
                objDPArr2[6].Value = 0;
                objDPArr2[7].Value = objContent.m_strOPNAME_RIGHT;
                objDPArr2[8].Value = objContent.m_strANANAME_RIGHT;
                objDPArr2[9].Value = objContent.m_strOTHERPOSTURE_RIGHT;
                objDPArr2[10].Value = objContent.m_strOTHERFOLEY_RIGHT;
                objDPArr2[11].Value = objContent.m_strOTHERSKINANTISEPSIS_RIGHT;
                objDPArr2[12].Value = objContent.m_strWHOLEBLOOD_RIGHT;
                objDPArr2[13].Value = objContent.m_strREDCELL_RIGHT;
                objDPArr2[14].Value = objContent.m_strPLASM_RIGHT;
                objDPArr2[15].Value = objContent.m_strSELFBLOOD_RIGHT;
                objDPArr2[16].Value = objContent.m_strPLATELET_RIGHT;
                objDPArr2[17].Value = objContent.m_strCOLDDEPOSIT_RIGHT;
                objDPArr2[18].Value = objContent.m_strOTHERBLOOD_RIGHT;
                objDPArr2[19].Value = objContent.m_strINLIQUID_RIGHT;
                objDPArr2[20].Value = objContent.m_strPISS_RIGHT;
                objDPArr2[21].Value = objContent.m_strSKINBEFOREOP_DESC_RIGHT;
                objDPArr2[22].Value = objContent.m_strSKINAFTEROP_DESC_RIGHT;
                objDPArr2[23].Value = objContent.m_strOTHERSAMPLE_RIGHT;
                objDPArr2[24].Value = objContent.m_strOPRECORD_RIGHT;
                objDPArr2[25].Value = objContent.m_strAXENICBAG_RIGHT;
                objDPArr2[26].Value = objContent.m_strEMBEDDED_RIGHT;
                objDPArr2[27].Value = objContent.m_strSKINBEFOREOP_DESC2_RIGHT;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);

                //设置旧记录状态为2
                objHRPServ.CreateDatabaseParameter(1, out objDPArrStatus);
                objDPArrStatus[0].Value = objContent.m_lngEMR_SEQ;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strSetOldLimbRecordSQL, ref lngEff, objDPArrStatus);
                if (lngRes <= 0) return lngRes;

                if (objContent.m_objLimbInfoArr != null && objContent.m_objLimbInfoArr.Length > 0)
                {                    
                    for (int l = 0; l < objContent.m_objLimbInfoArr.Length; l++)
                    {
                        objHRPServ.CreateDatabaseParameter(16, out objDPArr2);
                        objDPArr2[0].Value = objContent.m_objLimbInfoArr[l].m_strInPatientID;
                        objDPArr2[1].DbType = DbType.DateTime;
                        objDPArr2[1].Value = objContent.m_objLimbInfoArr[l].m_dtmInPatientDate;
                        objDPArr2[2].DbType = DbType.DateTime;
                        objDPArr2[2].Value = objContent.m_objLimbInfoArr[l].m_dtmOpenDate;
                        objDPArr2[3].DbType = DbType.DateTime;
                        objDPArr2[3].Value = objContent.m_objLimbInfoArr[l].m_dtmModifyDate;
                        objDPArr2[4].Value = objContent.m_objLimbInfoArr[l].m_strModifyUserID;
                        objDPArr2[5].Value = objContent.m_lngEMR_SEQ;
                        objDPArr2[6].Value = 0;
                        objDPArr2[7].Value = objContent.m_objLimbInfoArr[l].m_strCUBITUS;
                        objDPArr2[8].Value = objContent.m_objLimbInfoArr[l].m_strLEG;
                        objDPArr2[9].Value = objContent.m_objLimbInfoArr[l].m_strLEFT;
                        objDPArr2[10].Value = objContent.m_objLimbInfoArr[l].m_strRIGHT;
                        objDPArr2[11].Value = objContent.m_objLimbInfoArr[l].m_strCHARGETIME;
                        objDPArr2[12].Value = objContent.m_objLimbInfoArr[l].m_strDEFLATETIME;
                        objDPArr2[13].Value = objContent.m_objLimbInfoArr[l].m_strALLTIME;
                        objDPArr2[14].Value = objContent.m_objLimbInfoArr[l].m_strPRESS;
                        objDPArr2[15].Value = objContent.m_objLimbInfoArr[l].m_intOrderID;

                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewLimbContentSQL, ref lngEff, objDPArr2);
                    }
                }

                //释放
                objSign = null;
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
        #endregion

        #region 把记录从数据中“删除”
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                clsEMR_OperationRecord_GX objContent = p_objRecordContent as clsEMR_OperationRecord_GX;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objContent.m_lngEMR_SEQ;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }			//返回
            return lngRes;
        } 
        #endregion

        #region 获取数据库中最新的修改时间和首次打印时间
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

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
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion

        #region 获取指定已经被删除记录的内容
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
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
                    //设置结果
                    clsEMR_OperationRecord_GX objRecordContent = new clsEMR_OperationRecord_GX();
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
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());

                    if (dtbValue.Rows[0]["EMR_SEQ"] == DBNull.Value)
                        return -1;
                    objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);

                    objRecordContent.m_strOPNAME = dtbValue.Rows[0]["OPNAME"].ToString();
                    objRecordContent.m_strOPNAMEXML = dtbValue.Rows[0]["OPNAMEXML"].ToString();
                    objRecordContent.m_strANANAME = dtbValue.Rows[0]["ANANAME"].ToString();
                    objRecordContent.m_strANANAMEXML = dtbValue.Rows[0]["ANANAMEXML"].ToString();;
                    objRecordContent.m_strLIFTSIGNAFTEROP = dtbValue.Rows[0]["LIFTSIGNAFTEROP"].ToString();
                    objRecordContent.m_strSKINFULL = dtbValue.Rows[0]["SKINFULL"].ToString();
                    objRecordContent.m_strSEEPBLOOD = dtbValue.Rows[0]["SEEPBLOOD"].ToString();
                    objRecordContent.m_strPOSTURE = dtbValue.Rows[0]["POSTURE"].ToString();
                    objRecordContent.m_strOTHERPOSTURE = dtbValue.Rows[0]["OTHERPOSTURE"].ToString();
                    objRecordContent.m_strOTHERPOSTUREXML = dtbValue.Rows[0]["OTHERPOSTUREXML"].ToString();
                    objRecordContent.m_strSTANCHSTRAP = dtbValue.Rows[0]["STANCHSTRAP"].ToString();
                    objRecordContent.m_strFOLEY = dtbValue.Rows[0]["FOLEY"].ToString();
                    objRecordContent.m_strOTHERFOLEY = dtbValue.Rows[0]["OTHERFOLEY"].ToString();
                    objRecordContent.m_strOTHERFOLEYXML = dtbValue.Rows[0]["OTHERFOLEYXML"].ToString();
                    objRecordContent.m_strSKINANTISEPSIS = dtbValue.Rows[0]["SKINANTISEPSIS"].ToString();
                    objRecordContent.m_strOTHERSKINANTISEPSIS = dtbValue.Rows[0]["OTHERSKINANTISEPSIS"].ToString();
                    objRecordContent.m_strOTHERSKINANTISEPSISXML = dtbValue.Rows[0]["OTHERSKINANTISEPSISXML"].ToString();
                    objRecordContent.m_strWHOLEBLOOD = dtbValue.Rows[0]["WHOLEBLOOD"].ToString();
                    objRecordContent.m_strWHOLEBLOODXML = dtbValue.Rows[0]["WHOLEBLOODXML"].ToString();
                    objRecordContent.m_strREDCELL = dtbValue.Rows[0]["REDCELL"].ToString();
                    objRecordContent.m_strREDCELLXML = dtbValue.Rows[0]["REDCELLXML"].ToString();
                    objRecordContent.m_strPLASM = dtbValue.Rows[0]["PLASM"].ToString();
                    objRecordContent.m_strPLASMXML = dtbValue.Rows[0]["PLASMXML"].ToString();
                    objRecordContent.m_strSELFBLOOD = dtbValue.Rows[0]["SELFBLOOD"].ToString();
                    objRecordContent.m_strSELFBLOODXML = dtbValue.Rows[0]["SELFBLOODXML"].ToString();
                    objRecordContent.m_strPLATELET = dtbValue.Rows[0]["PLATELET"].ToString();
                    objRecordContent.m_strPLATELETXML = dtbValue.Rows[0]["PLATELETXML"].ToString();
                    objRecordContent.m_strCOLDDEPOSIT = dtbValue.Rows[0]["COLDDEPOSIT"].ToString();
                    objRecordContent.m_strCOLDDEPOSITXML = dtbValue.Rows[0]["COLDDEPOSITXML"].ToString();
                    objRecordContent.m_strOTHERBLOOD = dtbValue.Rows[0]["OTHERBLOOD"].ToString();
                    objRecordContent.m_strOTHERBLOODXML = dtbValue.Rows[0]["OTHERBLOODXML"].ToString();
                    objRecordContent.m_strINLIQUID = dtbValue.Rows[0]["INLIQUID"].ToString();
                    objRecordContent.m_strINLIQUIDXML = dtbValue.Rows[0]["INLIQUIDXML"].ToString();
                    objRecordContent.m_strPISS = dtbValue.Rows[0]["PISS"].ToString();
                    objRecordContent.m_strPISSXML = dtbValue.Rows[0]["PISSXML"].ToString();
                    objRecordContent.m_strDRAIN = dtbValue.Rows[0]["DRAIN"].ToString();
                    objRecordContent.m_strSKINBEFOREOP = dtbValue.Rows[0]["SKINBEFOREOP"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESC = dtbValue.Rows[0]["SKINBEFOREOP_DESC"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESCXML = dtbValue.Rows[0]["SKINBEFOREOP_DESCXML"].ToString();
                    objRecordContent.m_strSKINAFTEROP = dtbValue.Rows[0]["SKINAFTEROP"].ToString();
                    objRecordContent.m_strSKINAFTEROP_DESC = dtbValue.Rows[0]["SKINAFTEROP_DESC"].ToString();
                    objRecordContent.m_strSKINAFTEROP_DESCXML = dtbValue.Rows[0]["SKINAFTEROP_DESCXML"].ToString();
                    objRecordContent.m_strSAMPLE = dtbValue.Rows[0]["SAMPLE"].ToString();
                    objRecordContent.m_strOTHERSAMPLE = dtbValue.Rows[0]["OTHERSAMPLE"].ToString();
                    objRecordContent.m_strOTHERSAMPLEXML = dtbValue.Rows[0]["OTHERSAMPLEXML"].ToString();
                    objRecordContent.m_strAFTEROPSEND = dtbValue.Rows[0]["AFTEROPSEND"].ToString();
                    objRecordContent.m_strOPRECORD = dtbValue.Rows[0]["OPRECORD"].ToString();
                    objRecordContent.m_strOPRECORDXML = dtbValue.Rows[0]["OPRECORDXML"].ToString();
                    objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                    objRecordContent.m_strAXENICBAG = dtbValue.Rows[0]["AXENICBAG"].ToString();
                    objRecordContent.m_strAXENICBAGXML = dtbValue.Rows[0]["AXENICBAGXML"].ToString();
                    objRecordContent.m_strEMBEDDED = dtbValue.Rows[0]["EMBEDDED"].ToString();
                    objRecordContent.m_strEMBEDDEDXML = dtbValue.Rows[0]["EMBEDDEDXML"].ToString();
                    objRecordContent.m_strGUIDING = dtbValue.Rows[0]["GUIDING"].ToString();
                    objRecordContent.m_strHEALTHEDU = dtbValue.Rows[0]["HEALTHEDU"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESC2 = dtbValue.Rows[0]["SKINBEFOREOP_DESC2"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESC2XML = dtbValue.Rows[0]["SKINBEFOREOP_DESC2XML"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESC2_RIGHT = dtbValue.Rows[0]["SKINBEFOREOP_DESC2_RIGHT"].ToString();

                    objRecordContent.m_strOPNAME_RIGHT = dtbValue.Rows[0]["OPNAME_RIGHT"].ToString();
                    objRecordContent.m_strANANAME_RIGHT = dtbValue.Rows[0]["ANANAME_RIGHT"].ToString();
                    objRecordContent.m_strOTHERPOSTURE_RIGHT = dtbValue.Rows[0]["OTHERPOSTURE_RIGHT"].ToString();
                    objRecordContent.m_strOTHERFOLEY_RIGHT = dtbValue.Rows[0]["OTHERFOLEY_RIGHT"].ToString();
                    objRecordContent.m_strOTHERSKINANTISEPSIS_RIGHT = dtbValue.Rows[0]["OTHERSKINANTISEPSIS_RIGHT"].ToString();
                    objRecordContent.m_strWHOLEBLOOD_RIGHT = dtbValue.Rows[0]["WHOLEBLOOD_RIGHT"].ToString();
                    objRecordContent.m_strREDCELL_RIGHT = dtbValue.Rows[0]["REDCELL_RIGHT"].ToString();
                    objRecordContent.m_strPLASM_RIGHT = dtbValue.Rows[0]["PLASM_RIGHT"].ToString();
                    objRecordContent.m_strSELFBLOOD_RIGHT = dtbValue.Rows[0]["SELFBLOOD_RIGHT"].ToString();
                    objRecordContent.m_strPLATELET_RIGHT = dtbValue.Rows[0]["PLATELET_RIGHT"].ToString();
                    objRecordContent.m_strCOLDDEPOSIT_RIGHT = dtbValue.Rows[0]["COLDDEPOSIT_RIGHT"].ToString();
                    objRecordContent.m_strOTHERBLOOD_RIGHT = dtbValue.Rows[0]["OTHERBLOOD_RIGHT"].ToString();
                    objRecordContent.m_strINLIQUID_RIGHT = dtbValue.Rows[0]["INLIQUID_RIGHT"].ToString();
                    objRecordContent.m_strPISS_RIGHT = dtbValue.Rows[0]["PISS_RIGHT"].ToString();
                    objRecordContent.m_strSKINBEFOREOP_DESC_RIGHT = dtbValue.Rows[0]["SKINBEFOREOP_DESC_RIGHT"].ToString();
                    objRecordContent.m_strSKINAFTEROP_DESC_RIGHT = dtbValue.Rows[0]["SKINAFTEROP_DESC_RIGHT"].ToString();
                    objRecordContent.m_strOTHERSAMPLE_RIGHT = dtbValue.Rows[0]["OTHERSAMPLE_RIGHT"].ToString();
                    objRecordContent.m_strOPRECORD_RIGHT = dtbValue.Rows[0]["OPRECORD_RIGHT"].ToString();
                    objRecordContent.m_strAXENICBAG_RIGHT = dtbValue.Rows[0]["AXENICBAG_RIGHT"].ToString();
                    objRecordContent.m_strEMBEDDED_RIGHT = dtbValue.Rows[0]["EMBEDDED_RIGHT"].ToString();

                    //获取签名集合
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //释放
                        objSign = null;
                    }

                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                    objDPArr[0].Value = objRecordContent.m_lngEMR_SEQ;
                    //生成DataTable
                    DataTable dtbLimb = new DataTable();
                    //执行查询，填充结果到DataTable
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetLimbContentSQL, ref dtbLimb, objDPArr);
                    if (dtbLimb != null && dtbLimb.Rows.Count > 0)
                    {
                        objRecordContent.m_objLimbInfoArr = new clsEMR_OperationRecordLimb_GX[dtbLimb.Rows.Count];
                        for (int i = 0; i < dtbLimb.Rows.Count; i++)
                        {
                            objRecordContent.m_objLimbInfoArr[i] = new clsEMR_OperationRecordLimb_GX();
                            objRecordContent.m_objLimbInfoArr[i].m_lngEMR_SEQ = objRecordContent.m_lngEMR_SEQ;
                            objRecordContent.m_objLimbInfoArr[i].m_dtmInPatientDate = objRecordContent.m_dtmInPatientDate;
                            objRecordContent.m_objLimbInfoArr[i].m_strInPatientID = objRecordContent.m_strInPatientID;
                            objRecordContent.m_objLimbInfoArr[i].m_dtmModifyDate = DateTime.Parse(p_strOpenDate);
                            objRecordContent.m_objLimbInfoArr[i].m_dtmModifyDate = DateTime.Parse(dtbLimb.Rows[i]["MODIFYDATE"].ToString());
                            objRecordContent.m_objLimbInfoArr[i].m_strModifyUserID = dtbLimb.Rows[i]["MODIFYUSERID"].ToString();
                            if (dtbLimb.Rows[i]["STATUS"] == DBNull.Value)
                                objRecordContent.m_objLimbInfoArr[i].m_bytStatus = 0;
                            else objRecordContent.m_objLimbInfoArr[i].m_bytStatus = Byte.Parse(dtbLimb.Rows[i]["STATUS"].ToString());

                            objRecordContent.m_objLimbInfoArr[i].m_strCUBITUS = dtbLimb.Rows[i]["CUBITUS"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strLEG = dtbLimb.Rows[i]["LEG"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strLEFT = dtbLimb.Rows[i]["LEFT"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strRIGHT = dtbLimb.Rows[i]["RIGHT"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strCHARGETIME = dtbLimb.Rows[i]["CHARGETIME"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strDEFLATETIME = dtbLimb.Rows[i]["DEFLATETIME"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strALLTIME = dtbLimb.Rows[i]["ALLTIME"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_strPRESS = dtbLimb.Rows[i]["PRESS"].ToString();
                            objRecordContent.m_objLimbInfoArr[i].m_intOrderID = Convert.ToInt32(dtbLimb.Rows[i]["ORDERID"]);
                        }
                    }

                    p_objRecordContent = objRecordContent;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        } 
        #endregion
    }
}
