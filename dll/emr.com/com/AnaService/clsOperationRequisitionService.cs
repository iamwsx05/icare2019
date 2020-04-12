using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.DiseaseTrackService;

namespace com.digitalwave.iCare.middletier.Anaesthesia
{
    /// <summary>
    /// Summary description for clsOperationRequisitionService.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsOperationRequisitionService : clsDiseaseTrackService
    {

        public clsOperationRequisitionService()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 获取指定病人的所有没有删除记录的时间。
        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
        /// </summary>
        //private const string c_strGetTimeListSQL= "select createdate,opendate from operationrequisition where inpatientid = ? and inpatientdate= ? and status=0";

        /// <summary>
        /// 更新FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = "update operationrequisition set firstprintdate= ? where trim(inpatientid)= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

        /// <summary>
        /// 从获取指定病人的所有指定删除者删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = "select createdate,opendate from operationrequisition where trim(inpatientid) = ? and inpatientdate= ? and deactivedoperatorid= ? and status=1";

        /// <summary>
        /// 从获取指定病人的所有已经删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = "select createdate,opendate from operationrequisition where trim(inpatientid) = ? and inpatientdate= ? and status=1";
        /// <summary>
        /// 获得最新纪录
        /// </summary>
        //		private const string c_strGetRecordContentSQL = @"SELECT TOP 1 T1.*,T2.* FROM OperationRequisition AS T1,
        //			OperationRequisition_Content AS T2 
        //			WHERE	T1.InPatientID = ? AND T1.InPatientDate =? AND T1.OpenDate = ?
        //			AND	T1.InPatientID =  T2.InPatientID AND T1.InPatientDate = T2.InPatientDate AND T1.OpenDate = T2.OpenDate
        //			AND T1.Status = '0'	ORDER BY T2.ModifyDate DESC";
        private const string c_strGetRecordContentSQL = @"select v1.*,v2.priority,v2.operationroomname from 
				(select top 1 t1.*,t2.modifydate,t2.modifyuserid,t2.diagnose,t2.diseasename,
				t2.anamode,t2.operationdate,t2.chiefoperator,t2.assistant1,t2.assistant2,t2.assistant3,
				t2.inhospitaldoctor,t2.supervisorydoctor,t2.chiefdoctor,t2.remark,t2.emergency,
				t2.specialcase,t2.weight,t2.operationname from operationrequisition as t1,
				operationrequisition_content as t2 
				where trim(t1.inpatientid) = ? and t1.inpatientdate = ? and t1.opendate = ?
				and	t1.inpatientid =  t2.inpatientid and t1.inpatientdate = t2.inpatientdate and t1.opendate = t2.opendate
				and t1.status = '0'	order by t2.modifydate desc) as v1
				left join (select m1.*,m2.room_id as operationroomid,m2.roomname as operationroomname from relation_operationroompatient as m1,
				operationroom_desc as m2 where m1.room_id = m2.room_id and m1.status = '0' and m2.status = '0') as v2
				on v1.inpatientid = v2.inpatientid and v1.inpatientdate = v2.inpatientdate and v1.opendate = v2.opendate and v2.status = '0'";

        /// <summary>
        /// 获得已删除的最新纪录
        /// </summary>
        private const string c_strGetDeleteRecordContentSQL = @"select top 1 * from operationrequisition as t1,
			operationrequisition_content as t2 where	t1.inpatientid = ? and t1.inpatientdate =? and t1.opendate = ?
			and	t1.inpatientid =  t2.inpatientid and t1.inpatientdate = t2.inpatientdate and t1.opendate = t2.opendate
			and t1.status = '1'	order by t2.modifydate desc";

        /// <summary>
        /// 查找CreateDate
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select createuserid,opendate 
			from operationrequisition where trim(inpatientid) = ? and inpatientdate= ? and createdate= ? and status=0";


        /// <summary>
        /// 添加新记录主表
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into operationrequisition
			(inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,
			confirmreason,confirmreasonxml,status,diagnose_all,diagnosexml,diseasename_all,diseasename_xml,operationname_all,
			operationnamexml,anamode_all,anamodexml,remark_all,remarkxml,
			specialcase_all,specialcasexml,weight_all,weightxml) values(
			?,?,?,?,?,?,
			?,?,?,?,?,?,?,?,
			?,?,?,?,?,
			?,?,?,?)";


        /// <summary>
        /// 添加新记录子表
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into operationrequisition_content(
			inpatientid,inpatientdate,opendate,modifydate,modifyuserid,diagnose,
			diseasename,operationname,anamode,operationdate,chiefoperator,assistant1,assistant2,
			assistant3,inhospitaldoctor,supervisorydoctor,chiefdoctor,remark,emergency,specialcase,weight,assistant4,operating_no,
			operation_scale,isolation_indicator,blood_tran_doctor,sequence,first_operation_nurse,second_operation_nurse,first_supply_nurse,
			second_supply_nurse,ack_indicator,schedule_id,visit_id
			) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

        private const string c_strCheckLastModifyRecordSQL = @"select top 1 t2.modifydate,
			t2.modifyuserid from operationrequisition as t1,operationrequisition_content as t2
			where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status = '0'
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc";

        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate,
			b.modifydate from operationrequisition a,anaesthesia_plancontent b 
			where trim(a.inpatientid) = ? and a.inpatientdate= ? and a.opendate= ? 
			and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate 
			and b.opendate=a.opendate and
			b.modifydate=(select max(modifydate) from operationrequisition_content 
			where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

        private const string c_strGetDeleteRecordSQL = @"select deactiveddate,deactivedoperatorid from operationrequisition 
			where trim(inpatientid) = ? and inpatientdate= ? and opendate= ? and status = 1 ";

        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

        private const string c_strModifyRecordSQL = @"update operationrequisition set diagnose_all=?,
			diagnosexml=?,diseasename_all=?,diseasename_xml=?,  operationname_all=?,operationnamexml=?,anamode_all=?,anamodexml=?,
			remark_all=?,remarkxml=?,specialcase_all=?,specialcasexml=?,weight_all=?,weightxml=? 
			where  trim(inpatientid)=? and inpatientdate=? and 
			opendate=? and status=?";


        private const string c_strDeleteRecordSQL = @"update operationrequisition set status=1,deactiveddate=?,deactivedoperatorid=? where trim(inpatientid)=? and inpatientdate=? and opendate=? and status=0";

        /// <summary>
        /// 获取病人的该记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            string c_strGetTimeListSQL = "select createdate,opendate from operationrequisition where inpatientid = ? and inpatientdate= ? and status=0";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strCreateDateArr = new string[intRowCount];
                    p_strOpenDateArr = new string[intRowCount];
                    for (int i = 0; i < intRowCount; i++)
                    {
                        //设置结果
                        System.Data.DataRow objDataRow = dtbValue.Rows[i];
                        p_strCreateDateArr[i] = DateTime.Parse(objDataRow["createdate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(objDataRow["opendate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                //返回
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }


        /// <summary>
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">返回的记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                string c_strGetRecordContentSQL = "";
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    c_strGetRecordContentSQL = @"select v1.*,v2.priority,v2.operationroomname from 
							(select top 1 t1.*,t2.modifydate,t2.modifyuserid,t2.diagnose,t2.diseasename,
							t2.anamode,t2.operationdate,t2.chiefoperator,t2.assistant1,t2.assistant2,t2.assistant3,
							t2.inhospitaldoctor,t2.supervisorydoctor,t2.chiefdoctor,t2.remark,t2.emergency,
							t2.specialcase,t2.weight,t2.operationname from operationrequisition as t1,
							operationrequisition_content as t2 
							where	t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ?
							and	t1.inpatientid =  t2.inpatientid and t1.inpatientdate = t2.inpatientdate and t1.opendate = t2.opendate
							and t1.status = '0'	order by t2.modifydate desc) as v1
							left join (select m1.inpatientid,
                                               m1.inpatientdate,
                                               m1.opendate,
                                               m1.room_id,
                                               m1.modifydate,
                                               m1.priority,
                                               m1.status,
                                               m1.operatorid,
                                               m1.deactiveddate,m2.room_id as operationroomid,m2.roomname as operationroomname from relation_operationroompatient as m1,
							operationroom_desc as m2 where m1.room_id = m2.room_id and m1.status = '0' and m2.status = '0') as v2
							on v1.inpatientid = v2.inpatientid and v1.inpatientdate = v2.inpatientdate and v1.opendate = v2.opendate and v2.status = '0'";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    #region old
                    //                    c_strGetRecordContentSQL = @"select v1.*,v2.priority,v2.operationroomname from 
                    //							(select * from (select t1.*,t2.modifydate,t2.modifyuserid,t2.diagnose,t2.diseasename,
                    //							t2.anamode,t2.operationdate,t2.chiefoperator,t2.assistant1,t2.assistant2,t2.assistant3,
                    //							t2.inhospitaldoctor,t2.supervisorydoctor,t2.chiefdoctor,t2.remark,t2.emergency,
                    //							t2.specialcase,t2.weight,t2.operationname,t2.assistant4,t2.operating_no,t2.operation_scale,
                    //							decode(t2.isolation_indicator,'1','正常','2','隔离','3','放射') isolation_indicator,
                    //							t2.blood_tran_doctor,t2.sequence,t2.first_operation_nurse,t2.second_operation_nurse,
                    //							t2.first_supply_nurse,t2.second_supply_nurse,t2.ack_indicator,t2.schedule_id,t2.visit_id from operationrequisition t1,
                    //							operationrequisition_content t2 
                    //							where trim(t1.inpatientid) = ? and t1.inpatientdate = ? and t1.opendate = ?
                    //							and	t1.inpatientid =  t2.inpatientid and t1.inpatientdate = t2.inpatientdate and t1.opendate = t2.opendate
                    //							and t1.status = '0'	order by t2.modifydate desc) where rownum = 1) v1
                    //							left join (select m1.inpatientid,
                    //                                               m1.inpatientdate,
                    //                                               m1.opendate,
                    //                                               m1.room_id,
                    //                                               m1.modifydate,
                    //                                               m1.priority,
                    //                                               m1.status,
                    //                                               m1.operatorid,
                    //                                               m1.deactiveddate,m2.room_id operationroomid,m2.roomname operationroomname from relation_operationroompatient m1,
                    //							operationroom_desc m2 where m1.room_id = m2.room_id and m1.status = '0' and m2.status = '0') v2
                    //							on v1.inpatientid = v2.inpatientid and v1.inpatientdate = v2.inpatientdate and v1.opendate = v2.opendate and v2.status = '0'";
                    #endregion
                    c_strGetRecordContentSQL = @"select v1.inpatientid,
                v1.inpatientdate,
                v1.opendate,
                v1.createdate,
                v1.createuserid,
                v1.ifconfirm,
                v1.confirmreason,
                v1.confirmreasonxml,
                v1.firstprintdate,
                v1.deactiveddate,
                v1.deactivedoperatorid,
                v1.status,
                v1.diagnose_all,
                v1.diagnosexml,
                v1.diseasename_all,
                v1.diseasename_xml,
                v1.operationname_all,
                v1.operationnamexml,
                v1.anamode_all,
                v1.anamodexml,
                v1.remark_all,
                v1.remarkxml,
                v1.specialcase_all,
                v1.specialcasexml,
                v1.weight_all,
                v1.weightxml,
                v1.orderid_chr,
                v1.modifydate,
                v1.modifyuserid,
                v1.diagnose,
                v1.diseasename,
                v1.anamode,
                v1.operationdate,
                v1.chiefoperator,
                v1.assistant1,
                v1.assistant2,
                v1.assistant3,
                v1.inhospitaldoctor,
                v1.supervisorydoctor,
                v1.chiefdoctor,
                v1.remark,
                v1.emergency,
                v1.specialcase,
                v1.weight,
                v1.operationname,
                v1.assistant4,
                v1.operating_no,
                v1.operation_scale,
                decode(v1.isolation_indicator,
                       '1',
                       '正常',
                       '2',
                       '隔离',
                       '3',
                       '放射')  isolation_indicator,
                v1.blood_tran_doctor,
                v1.sequence,
                v1.first_operation_nurse,
                v1.second_operation_nurse,
                v1.first_supply_nurse,
                v1.second_supply_nurse,
                v1.ack_indicator,
                v1.schedule_id,
                v1.visit_id
,v2.priority,v2.operationroomname from 
							(select v3.inpatientid,
                v3.inpatientdate,
                v3.opendate,
                v3.createdate,
                v3.createuserid,
                v3.ifconfirm,
                v3.confirmreason,
                v3.confirmreasonxml,
                v3.firstprintdate,
                v3.deactiveddate,
                v3.deactivedoperatorid,
                v3.status,
                v3.diagnose_all,
                v3.diagnosexml,
                v3.diseasename_all,
                v3.diseasename_xml,
                v3.operationname_all,
                v3.operationnamexml,
                v3.anamode_all,
                v3.anamodexml,
                v3.remark_all,
                v3.remarkxml,
                v3.specialcase_all,
                v3.specialcasexml,
                v3.weight_all,
                v3.weightxml,
                v3.orderid_chr,
                v3.modifydate,
                v3.modifyuserid,
                v3.diagnose,
                v3.diseasename,
                v3.anamode,
                v3.operationdate,
                v3.chiefoperator,
                v3.assistant1,
                v3.assistant2,
                v3.assistant3,
                v3.inhospitaldoctor,
                v3.supervisorydoctor,
                v3.chiefdoctor,
                v3.remark,
                v3.emergency,
                v3.specialcase,
                v3.weight,
                v3.operationname,
                v3.assistant4,
                v3.operating_no,
                v3.operation_scale,
                decode(v3.isolation_indicator,
                       '1',
                       '正常',
                       '2',
                       '隔离',
                       '3',
                       '放射') isolation_indicator,
                v3.blood_tran_doctor,
                v3.sequence,
                v3.first_operation_nurse,
                v3.second_operation_nurse,
                v3.first_supply_nurse,
                v3.second_supply_nurse,
                v3.ack_indicator,
                v3.schedule_id,
                v3.visit_id
 from (select t1.inpatientid,
                t1.inpatientdate,
                t1.opendate,
                t1.createdate,
                t1.createuserid,
                t1.ifconfirm,
                t1.confirmreason,
                t1.confirmreasonxml,
                t1.firstprintdate,
                t1.deactiveddate,
                t1.deactivedoperatorid,
                t1.status,
                t1.diagnose_all,
                t1.diagnosexml,
                t1.diseasename_all,
                t1.diseasename_xml,
                t1.operationname_all,
                t1.operationnamexml,
                t1.anamode_all,
                t1.anamodexml,
                t1.remark_all,
                t1.remarkxml,
                t1.specialcase_all,
                t1.specialcasexml,
                t1.weight_all,
                t1.weightxml,
                t1.orderid_chr,t2.modifydate,t2.modifyuserid,t2.diagnose,t2.diseasename,
							t2.anamode,t2.operationdate,t2.chiefoperator,t2.assistant1,t2.assistant2,t2.assistant3,
							t2.inhospitaldoctor,t2.supervisorydoctor,t2.chiefdoctor,t2.remark,t2.emergency,
							t2.specialcase,t2.weight,t2.operationname,t2.assistant4,t2.operating_no,t2.operation_scale,
							decode(t2.isolation_indicator,'1',
                       '正常',
                       '2',
                       '隔离',
                       '3',
                       '放射') isolation_indicator,
							t2.blood_tran_doctor,t2.sequence,t2.first_operation_nurse,t2.second_operation_nurse,
							t2.first_supply_nurse,t2.second_supply_nurse,t2.ack_indicator,t2.schedule_id,t2.visit_id from operationrequisition t1,
							operationrequisition_content t2 
							where trim(t1.inpatientid) = ? and t1.inpatientdate = ? and t1.opendate = ?
							and	t1.inpatientid =  t2.inpatientid and t1.inpatientdate = t2.inpatientdate and t1.opendate = t2.opendate
							and t1.status = '0'	order by t2.modifydate desc) v3 where rownum = 1) v1
							left join (select m1.inpatientid,
                                               m1.inpatientdate,
                                               m1.opendate,
                                               m1.room_id,
                                               m1.modifydate,
                                               m1.priority,
                                               m1.status,
                                               m1.operatorid,
                                               m1.deactiveddate,m2.room_id operationroomid,m2.roomname operationroomname from relation_operationroompatient m1,
							operationroom_desc m2 where m1.room_id = m2.room_id and m1.status = '0' and m2.status = '0') v2
							on v1.inpatientid = v2.inpatientid and v1.inpatientdate = v2.inpatientdate and v1.opendate = v2.opendate and v2.status = '0'";
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount == 1)
                {

                    //设置结果
                    System.Data.DataRow objDataRow = dtbValue.Rows[0];
                    clsOperationRequisitionValue objRecordContent = new clsOperationRequisitionValue();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(objDataRow["createdate"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(objDataRow["modifydate"].ToString());

                    if (objDataRow["firstprintdate"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(objDataRow["firstprintdate"].ToString());
                    objRecordContent.m_strCreateUserID = objDataRow["createuserid"].ToString();
                    objRecordContent.m_strModifyUserID = objDataRow["modifyuserid"].ToString();
                    if (objDataRow["ifconfirm"].ToString() == "")
                        objRecordContent.m_bytIfConfirm = 0;
                    else objRecordContent.m_bytIfConfirm = Byte.Parse(objDataRow["ifconfirm"].ToString());
                    if (objDataRow["status"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(objDataRow["status"].ToString());

                    objRecordContent.m_strConfirmReason = objDataRow["confirmreason"].ToString();
                    objRecordContent.m_strConfirmReasonXML = objDataRow["confirmreasonxml"].ToString();

                    objRecordContent.m_strDiagnose_All = objDataRow["diagnose_all"].ToString();
                    objRecordContent.m_strDiagnoseXML = objDataRow["diagnosexml"].ToString();
                    objRecordContent.m_strOperationName_All = objDataRow["operationname_all"].ToString();
                    objRecordContent.m_strOperationNameXML = objDataRow["operationnamexml"].ToString();
                    objRecordContent.m_strAnaMode_All = objDataRow["anamode_all"].ToString();
                    objRecordContent.m_strAnaModeXML = objDataRow["anamodexml"].ToString();

                    objRecordContent.m_strOperationDate = objDataRow["operationdate"].ToString();
                    objRecordContent.m_strAssistant1 = objDataRow["assistant1"].ToString();
                    objRecordContent.m_strAssistant2 = objDataRow["assistant2"].ToString();
                    objRecordContent.m_strAssistant3 = objDataRow["assistant3"].ToString();
                    objRecordContent.m_strChiefDoctor = objDataRow["chiefdoctor"].ToString();
                    objRecordContent.m_strChiefOperator = objDataRow["chiefoperator"].ToString();
                    objRecordContent.m_strDeActivedOperatorID = objDataRow["deactivedoperatorid"].ToString();
                    objRecordContent.m_strDiseaseName = objDataRow["diseasename"].ToString();
                    objRecordContent.m_strDiseaseName_All = objDataRow["diseasename_all"].ToString();
                    objRecordContent.m_strDiseaseName_XML = objDataRow["diseasename_xml"].ToString();
                    objRecordContent.m_strRemark = objDataRow["remark"].ToString();
                    objRecordContent.m_strRemark_All = objDataRow["remark_all"].ToString();
                    objRecordContent.m_strRemarkXML = objDataRow["remarkxml"].ToString();
                    objRecordContent.m_strSpecialCase_All = objDataRow["specialcase_all"].ToString();  //
                    objRecordContent.m_strSpecialCaseXML = objDataRow["specialcasexml"].ToString();  //
                    objRecordContent.m_strSpecialCase = objDataRow["specialcase"].ToString();  //
                    objRecordContent.m_strWeight_All = objDataRow["weight_all"].ToString();  //
                    objRecordContent.m_strWeightXML = objDataRow["weightxml"].ToString();  //
                    objRecordContent.m_strWeight = objDataRow["weight"].ToString();  //
                    objRecordContent.m_strOperationRoom = objDataRow["operationroomname"].ToString().Trim();
                    objRecordContent.m_strPriority = objDataRow["priority"].ToString().Trim();

                    objRecordContent.m_strInHospitalDoctor = objDataRow["inhospitaldoctor"].ToString();
                    objRecordContent.m_strSupervisoryDoctor = objDataRow["supervisorydoctor"].ToString();
                    objRecordContent.m_blnEmergency = Convert.ToBoolean(objDataRow["emergency"]);

                    objRecordContent.m_strDiagnose = objDataRow["diagnose"].ToString();
                    objRecordContent.m_strOperationName = objDataRow["operationname"].ToString();
                    objRecordContent.m_strAnaMode = objDataRow["anamode"].ToString();

                    objRecordContent.m_strAssistant4 = objDataRow["assistant4"].ToString();
                    objRecordContent.m_strOPERATING_NO = objDataRow["operating_no"].ToString();
                    objRecordContent.m_strOPERATION_SCALE = objDataRow["operation_scale"].ToString();
                    objRecordContent.m_strISOLATION_INDICATOR = objDataRow["isolation_indicator"].ToString();
                    objRecordContent.m_strBLOOD_TRAN_DOCTOR = objDataRow["blood_tran_doctor"].ToString();
                    objRecordContent.m_strSEQUENCE = objDataRow["sequence"].ToString();
                    objRecordContent.m_strfirstoperationnurse = objDataRow["first_operation_nurse"].ToString();
                    objRecordContent.m_strsecondoperationnurse = objDataRow["second_operation_nurse"].ToString();
                    objRecordContent.m_strfirstsupplynurs = objDataRow["first_supply_nurse"].ToString();
                    objRecordContent.m_strsecondsupplynurs = objDataRow["second_supply_nurse"].ToString();
                    objRecordContent.m_strACK_INDICATOR = objDataRow["ack_indicator"].ToString();
                    objRecordContent.m_strSCHEDULE_ID = objDataRow["schedule_id"].ToString();
                    objRecordContent.m_strVISIT_ID = objDataRow["visit_id"].ToString();
                    m_lngGetAnaDoctor(p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_objHRPServ, ref objRecordContent.m_objAnasthetistArr);
                    p_objRecordContent = objRecordContent;
                }
                //返回
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }

        /// <summary>
        /// 查看是否有相同的记录时间
        /// </summary>
        /// <param name="p_objRecordContent">当前记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objPreModifyInfo">若有相同记录,返回该相同记录的操作信息,否则为空</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //查看DataTable.Rows.Count
                //如果等于1，表示已经有该CreateDate，并且不是删除的记录。
                //获取该记录的信息，赋值到p_objModifyInfo中。返回值使用Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    System.Data.DataRow objDataRow = dtbValue.Rows[0];
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = objDataRow["CreateUserID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(objDataRow["OpenDate"].ToString());
                    return (long)enmOperationResult.Record_Already_Exist;
                }
                //返回	
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }

        [AutoComplete]
        public long m_lngAddAnaDoctor(clsOperationRequisitionValue objContent, clsHRPTableService p_objHRPServ)
        {
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

            objDPArr[0].Value = objContent.m_strInPatientID;
            objDPArr[1].Value = objContent.m_dtmInPatientDate;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[2].Value = objContent.m_dtmOpenDate;
            objDPArr[2].DbType = DbType.DateTime;
            try
            {
                string strSql = @"delete from relation_operationanaesthetist
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?";
                long lngAfft = 0;
                long lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngAfft, objDPArr);
                if (objContent.m_objAnasthetistArr != null)
                {
                    for (int i = 0; i < objContent.m_objAnasthetistArr.Length; i++)
                    {
                        objDPArr = null;
                        objHRPSvc.CreateDatabaseParameter(8, out objDPArr);
                        objDPArr[0].Value = objContent.m_strInPatientID;
                        objDPArr[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr[2].Value = objContent.m_dtmOpenDate;
                        objDPArr[3].Value = objContent.m_objAnasthetistArr[i].m_strAnaesthetistID;
                        objDPArr[4].Value = objContent.m_dtmModifyDate;
                        objDPArr[5].Value = objContent.m_bytStatus;
                        objDPArr[6].Value = objContent.m_objAnasthetistArr[i].m_strChiefFlag;
                        objDPArr[7].Value = objContent.m_strModifyUserID;

                        strSql = @"insert into relation_operationanaesthetist
  (inpatientid,
   inpatientdate,
   opendate,
   anaesthetistid,
   modifydate,
   status,
   ifchief,
   operatorid)
values
  (?, ?, ?, ?, ?, ?, ?, ?)";
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngAfft, objDPArr);
                    }
                }
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }

        /// <summary>
        /// 保存记录到数据库。
        /// </summary>
        /// <param name="p_objRecordContent">当前记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值
                clsOperationRequisitionValue objContent = (clsOperationRequisitionValue)p_objRecordContent;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(23, out objDPArr);

                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].Value = objContent.m_dtmCreateDate;
                objDPArr[4].Value = objContent.m_strCreateUserID;
                objDPArr[5].Value = objContent.m_bytIfConfirm;
                if (objContent.m_strConfirmReason == null)
                    objDPArr[6].Value = DBNull.Value;
                else
                    objDPArr[6].Value = objContent.m_strConfirmReason;
                if (objContent.m_strConfirmReasonXML == null)
                    objDPArr[7].Value = DBNull.Value;
                else
                    objDPArr[7].Value = objContent.m_strConfirmReasonXML;
                objDPArr[8].Value = 0;//status

                objDPArr[9].Value = objContent.m_strDiagnose_All;
                objDPArr[10].Value = objContent.m_strDiagnoseXML;
                objDPArr[11].Value = objContent.m_strDiseaseName_All;
                objDPArr[12].Value = objContent.m_strDiseaseName_XML;
                objDPArr[13].Value = objContent.m_strOperationName_All;
                objDPArr[14].Value = objContent.m_strOperationNameXML;

                objDPArr[15].Value = objContent.m_strAnaMode_All;
                objDPArr[16].Value = objContent.m_strAnaModeXML;
                objDPArr[17].Value = objContent.m_strRemark_All;
                objDPArr[18].Value = objContent.m_strRemarkXML;
                objDPArr[19].Value = objContent.m_strSpecialCase_All;
                objDPArr[20].Value = objContent.m_strSpecialCaseXML;
                objDPArr[21].Value = objContent.m_strWeight_All;
                objDPArr[22].Value = objContent.m_strWeightXML;

                for (int i1 = 0; i1 < objDPArr.Length; i1++)
                {
                    if (objDPArr[i1].Value == null)
                    {
                        return (long)enmOperationResult.Parameter_Error;
                    }
                }
                IDataParameter[] objDPArr2 = null;
                objHRPSvc.CreateDatabaseParameter(34, out objDPArr2);


                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;

                objDPArr2[5].Value = objContent.m_strDiagnose;
                objDPArr2[6].Value = objContent.m_strDiseaseName;
                objDPArr2[7].Value = objContent.m_strOperationName;
                objDPArr2[8].Value = objContent.m_strAnaMode;
                objDPArr2[9].Value = DateTime.Parse(objContent.m_strOperationDate);

                if (objContent.m_strChiefOperator != null)
                {
                    objDPArr2[10].Value = objContent.m_strChiefOperator;
                }
                else
                {
                    objDPArr2[10].Value = "";
                }

                if (objContent.m_strAssistant1 != null)
                {
                    objDPArr2[11].Value = objContent.m_strAssistant1;
                }
                else
                {
                    objDPArr2[11].Value = "";
                }

                if (objContent.m_strAssistant2 != null)
                {
                    objDPArr2[12].Value = objContent.m_strAssistant2;
                }
                else
                {
                    objDPArr2[12].Value = "";
                }

                if (objContent.m_strAssistant3 != null)
                {
                    objDPArr2[13].Value = objContent.m_strAssistant3;
                }
                else
                {
                    objDPArr2[13].Value = "";
                }

                if (objContent.m_strInHospitalDoctor != null)
                {
                    objDPArr2[14].Value = objContent.m_strInHospitalDoctor;
                }
                else
                {
                    objDPArr2[14].Value = "";
                }

                if (objContent.m_strSupervisoryDoctor != null)
                {
                    objDPArr2[15].Value = objContent.m_strSupervisoryDoctor;
                }
                else
                {
                    objDPArr2[15].Value = "";
                }

                if (objContent.m_strChiefDoctor != null)
                {
                    objDPArr2[16].Value = objContent.m_strChiefDoctor;
                }
                else
                {
                    objDPArr2[16].Value = "";
                }

                objDPArr2[17].Value = objContent.m_strRemark;
                objDPArr2[18].Value = (objContent.m_blnEmergency ? 1 : 0);
                objDPArr2[19].Value = objContent.m_strSpecialCase;
                objDPArr2[20].Value = objContent.m_strWeight;

                if (objContent.m_strAssistant4 != null)
                {
                    objDPArr2[21].Value = objContent.m_strAssistant4;
                }
                else
                {
                    objDPArr2[21].Value = "";
                }
                objDPArr2[22].Value = objContent.m_strOPERATING_NO;
                objDPArr2[23].Value = objContent.m_strOPERATION_SCALE;
                objDPArr2[24].Value = objContent.m_strISOLATION_INDICATOR;
                objDPArr2[25].Value = objContent.m_strBLOOD_TRAN_DOCTOR;
                objDPArr2[26].Value = objContent.m_strSEQUENCE;
                objDPArr2[27].Value = objContent.m_strfirstoperationnurse;
                objDPArr2[28].Value = objContent.m_strsecondoperationnurse;
                objDPArr2[29].Value = objContent.m_strfirstsupplynurs;
                objDPArr2[30].Value = objContent.m_strsecondsupplynurs;
                objDPArr2[31].Value = objContent.m_strACK_INDICATOR;

                objDPArr2[32].Value = objContent.m_strSCHEDULE_ID;
                objDPArr2[33].Value = objContent.m_strVISIT_ID;

                for (int i1 = 0; i1 < objDPArr2.Length; i1++)
                {
                    if (objDPArr2[i1].Value == null)
                    {
                        return (long)enmOperationResult.Parameter_Error;
                    }
                }
                //执行SQL
                long lngEff = 0;
                long lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //执行SQL
                lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                m_lngAddAnaDoctor(objContent, p_objHRPServ);

                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }

        /// <summary>
        /// 查看当前记录是否最新的记录。
        /// </summary>
        /// <param name="p_objRecordContent">当前记录内容</param>
        /// <param name="p_objHRPServ"></param>		
        /// <param name="p_objModifyInfo">若当前记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {

                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                //			IDataParameter[] objDPArr = new OdbcParameter[3];
                //			//按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new OdbcParameter();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //按顺序给IDataParameter赋值

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                string c_strCheckLastModifyRecordSQL = "";
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    c_strCheckLastModifyRecordSQL = @"select top 1 t2.modifydate,
						t2.modifyuserid from operationrequisition as t1,operationrequisition_content as t2
						where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
						and t1.opendate = t2.opendate and t1.status = '0'
						and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    c_strCheckLastModifyRecordSQL = @"select modifydate,modifyuserid
  from (select t2.modifydate, t2.modifyuserid
          from operationrequisition t1, operationrequisition_content t2
         where t1.inpatientid = t2.inpatientid
           and t1.inpatientdate = t2.inpatientdate
           and t1.opendate = t2.opendate
           and t1.status = '0'
           and trim(t1.inpatientid) = ?
           and t1.inpatientdate = ?
           and t1.opendate = ?
         order by t2.modifydate desc)
 where rownum = 1";
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                             
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from WatchItemRecord where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
                    dtbValue = null;
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);
                    intRowCount = dtbValue.Rows.Count;
                    if (lngRes > 0 && intRowCount == 1)
                    {
                        System.Data.DataRow objDataRow = dtbValue.Rows[0];
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = objDataRow["deactivedoperatorid"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(objDataRow["deactiveddate"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && intRowCount == 1)
                {
                    //如果相同，返回DB_Succees
                    System.Data.DataRow objDataRow = dtbValue.Rows[0];
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(objDataRow["modifydate"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = objDataRow["modifyuserid"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(objDataRow["modifydate"].ToString());
                    return (long)enmOperationResult.Record_Already_Modify;
                }
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }

        /// <summary>
        /// 把新修改的内容保存到数据库。
        /// </summary>
        /// <param name="p_objRecordContent">当前记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值
                clsOperationRequisitionValue objContent = (clsOperationRequisitionValue)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(18, out objDPArr);


                objDPArr[0].Value = objContent.m_strDiagnose_All;
                objDPArr[1].Value = objContent.m_strDiagnoseXML;
                objDPArr[2].Value = objContent.m_strDiseaseName_All;
                objDPArr[3].Value = objContent.m_strDiseaseName_XML;
                objDPArr[4].Value = objContent.m_strOperationName_All;
                objDPArr[5].Value = objContent.m_strOperationNameXML;
                objDPArr[6].Value = objContent.m_strAnaMode_All;
                objDPArr[7].Value = objContent.m_strAnaModeXML;
                //			objDPArr[6].Value=objContent.m_strAnnounceSelection_All;
                objDPArr[8].Value = objContent.m_strRemark_All;
                objDPArr[9].Value = objContent.m_strRemarkXML;
                objDPArr[10].Value = objContent.m_strSpecialCase_All;
                objDPArr[11].Value = objContent.m_strSpecialCaseXML;
                objDPArr[12].Value = objContent.m_strWeight_All;
                objDPArr[13].Value = objContent.m_strWeightXML;

                objDPArr[14].Value = objContent.m_strInPatientID;
                objDPArr[15].Value = objContent.m_dtmInPatientDate;
                objDPArr[16].Value = objContent.m_dtmOpenDate;
                objDPArr[17].Value = 0;

                IDataParameter[] objDPArr2 = null;
                objHRPSvc.CreateDatabaseParameter(34, out objDPArr2);


                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;

                objDPArr2[5].Value = objContent.m_strDiagnose;
                objDPArr2[6].Value = objContent.m_strDiseaseName;
                objDPArr2[7].Value = objContent.m_strOperationName;
                objDPArr2[8].Value = objContent.m_strAnaMode;
                objDPArr2[9].Value = DateTime.Parse(objContent.m_strOperationDate);

                if (objContent.m_strChiefOperator != null)
                {
                    objDPArr2[10].Value = objContent.m_strChiefOperator;
                }
                else
                {
                    objDPArr2[10].Value = "";
                }

                if (objContent.m_strAssistant1 != null)
                {
                    objDPArr2[11].Value = objContent.m_strAssistant1;
                }
                else
                {
                    objDPArr2[11].Value = "";
                }

                if (objContent.m_strAssistant2 != null)
                {
                    objDPArr2[12].Value = objContent.m_strAssistant2;
                }
                else
                {
                    objDPArr2[12].Value = "";
                }

                if (objContent.m_strAssistant3 != null)
                {
                    objDPArr2[13].Value = objContent.m_strAssistant3;
                }
                else
                {
                    objDPArr2[13].Value = "";
                }

                if (objContent.m_strInHospitalDoctor != null)
                {
                    objDPArr2[14].Value = objContent.m_strInHospitalDoctor;
                }
                else
                {
                    objDPArr2[14].Value = "";
                }

                if (objContent.m_strSupervisoryDoctor != null)
                {
                    objDPArr2[15].Value = objContent.m_strSupervisoryDoctor;
                }
                else
                {
                    objDPArr2[15].Value = "";
                }

                if (objContent.m_strChiefDoctor != null)
                {
                    objDPArr2[16].Value = objContent.m_strChiefDoctor;
                }
                else
                {
                    objDPArr2[16].Value = "";
                }

                objDPArr2[17].Value = objContent.m_strRemark;
                objDPArr2[18].Value = (objContent.m_blnEmergency ? 1 : 0);
                objDPArr2[19].Value = objContent.m_strSpecialCase;
                objDPArr2[20].Value = objContent.m_strWeight;

                if (objContent.m_strAssistant4 != null)
                {
                    objDPArr2[21].Value = objContent.m_strAssistant4;
                }
                else
                {
                    objDPArr2[21].Value = "";
                }
                objDPArr2[22].Value = objContent.m_strOPERATING_NO;
                objDPArr2[23].Value = objContent.m_strOPERATION_SCALE;
                objDPArr2[24].Value = objContent.m_strISOLATION_INDICATOR;
                objDPArr2[25].Value = objContent.m_strBLOOD_TRAN_DOCTOR;
                objDPArr2[26].Value = objContent.m_strSEQUENCE;
                objDPArr2[27].Value = objContent.m_strfirstoperationnurse;
                objDPArr2[28].Value = objContent.m_strsecondoperationnurse;
                objDPArr2[29].Value = objContent.m_strfirstsupplynurs;
                objDPArr2[30].Value = objContent.m_strsecondsupplynurs;
                objDPArr2[31].Value = objContent.m_strACK_INDICATOR;
                objDPArr2[32].Value = objContent.m_strSCHEDULE_ID;
                objDPArr2[33].Value = objContent.m_strVISIT_ID;


                //执行SQL
                long lngEff = 0;
                long lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;
                m_lngAddAnaDoctor(objContent, p_objHRPServ);
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }

        /// <summary>
        /// 把记录从数据中“删除”。
        /// </summary>
        /// <param name="p_objRecordContent">当前记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].Value = p_objRecordContent.m_dtmOpenDate;

                //执行SQL
                long lngEff = 0;
                return p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
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
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    System.Data.DataRow objDataRow = dtbValue.Rows[0];
                    p_strFirstPrintDate = objDataRow["firstprintdate"].ToString();
                    p_dtmModifyDate = DateTime.Parse(objDataRow["modifydate"].ToString());
                }
                //返回DB_Succees
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
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
        public override long m_lngUpdateFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objDPArr);

                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //执行SQL
                long lngEff = 0;
                return objHRPSvc.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }

        /// <summary>
        /// 获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strDeleteUserID">删除操作者ID</param>
        /// <param name="p_strCreateRecordTimeArr">用户填写的记录时间数组</param>
        /// <param name="p_strOpenRecordTimeArr">系统生成的记录时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strCreateDateArr = new string[intRowCount];
                    p_strOpenDateArr = new string[intRowCount];
                    for (int i = 0; i < intRowCount; i++)
                    {
                        //设置结果
                        System.Data.DataRow objDataRow = dtbValue.Rows[i];
                        p_strCreateDateArr[i] = DateTime.Parse(objDataRow["createdate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(objDataRow["opendate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                //返回DB_Succees
                return (long)enmOperationResult.DB_Succeed;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return (long)enmOperationResult.DB_Fail;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }


        /// <summary>
        /// 获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateRecordTimeArr">入院日期</param>
        /// <param name="p_strOpenRecordTimeArr">系统生成的记录时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeListAll(string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strCreateDateArr = new string[intRowCount];
                    p_strOpenDateArr = new string[intRowCount];
                    for (int i = 0; i < intRowCount; i++)
                    {
                        //设置结果
                        System.Data.DataRow objDataRow = dtbValue.Rows[i];
                        p_strCreateDateArr[i] = DateTime.Parse(objDataRow["createdate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(objDataRow["opendate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                //返回DB_Succees
                return (long)enmOperationResult.DB_Succeed;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return (long)enmOperationResult.DB_Fail;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }



        /// <summary>
        /// 获取指定已经被删除记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenRecordTime">记录时间</param>
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
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                string c_strGetDeleteRecordContentSQL = "";
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    c_strGetDeleteRecordContentSQL = @"select top 1 * from operationrequisition as t1,
							operationrequisition_content as t2 where	t1.inpatientid = ? and t1.inpatientdate =? and t1.opendate = ?
							and	t1.inpatientid =  t2.inpatientid and t1.inpatientdate = t2.inpatientdate and t1.opendate = t2.opendate
							and t1.status = '1'	order by t2.modifydate desc";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    c_strGetDeleteRecordContentSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       firstprintdate,
       deactiveddate,
       deactivedoperatorid,
       status,
       diagnose_all,
       diagnosexml,
       diseasename_all,
       diseasename_xml,
       operationname_all,
       operationnamexml,
       anamode_all,
       anamodexml,
       remark_all,
       remarkxml,
       specialcase_all,
       specialcasexml,
       weight_all,
       weightxml,
       orderid_chr
  from (select t1.inpatientid,
                t1.inpatientdate,
                t1.opendate,
                t1.createdate,
                t1.createuserid,
                t1.ifconfirm,
                t1.confirmreason,
                t1.confirmreasonxml,
                t1.firstprintdate,
                t1.deactiveddate,
                t1.deactivedoperatorid,
                t1.status,
                t1.diagnose_all,
                t1.diagnosexml,
                t1.diseasename_all,
                t1.diseasename_xml,
                t1.operationname_all,
                t1.operationnamexml,
                t1.anamode_all,
                t1.anamodexml,
                t1.remark_all,
                t1.remarkxml,
                t1.specialcase_all,
                t1.specialcasexml,
                t1.weight_all,
                t1.weightxml,
                t1.orderid_chr
           from operationrequisition t1, operationrequisition_content t2
          where trim(t1.inpatientid) = ?
                    and t1.inpatientdate = ?
                    and t1.opendate = ?
                    and
          t1.inpatientid = t2.inpatientid
       and t1.inpatientdate = t2.inpatientdate
       and t1.opendate = t2.opendate
       and t1.status = '1'
          order by t2.modifydate desc)
 where rownum = 1";

                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount == 1)
                {
                    //设置结果
                    System.Data.DataRow objDataRow = dtbValue.Rows[0];
                    clsOperationRequisitionValue objRecordContent = new clsOperationRequisitionValue();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(objDataRow["createdate"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(objDataRow["modifydate"].ToString());

                    if (objDataRow["firstprintdate"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(objDataRow["firstprintdate"].ToString());
                    objRecordContent.m_strCreateUserID = objDataRow["createuserid"].ToString();
                    objRecordContent.m_strModifyUserID = objDataRow["modifyuserid"].ToString();
                    if (objDataRow["ifconfirm"].ToString() == "")
                        objRecordContent.m_bytIfConfirm = 0;
                    else objRecordContent.m_bytIfConfirm = Byte.Parse(objDataRow["ifconfirm"].ToString());
                    if (objDataRow["status"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(objDataRow["status"].ToString());

                    objRecordContent.m_strConfirmReason = objDataRow["confirmreason"].ToString();
                    objRecordContent.m_strConfirmReasonXML = objDataRow["confirmreasonxml"].ToString();

                    objRecordContent.m_strDiagnose_All = objDataRow["diagnose_all"].ToString();
                    objRecordContent.m_strDiagnoseXML = objDataRow["diagnosexml"].ToString();
                    objRecordContent.m_strOperationName_All = objDataRow["operationname_all"].ToString();
                    objRecordContent.m_strOperationNameXML = objDataRow["operationnamexml"].ToString();
                    objRecordContent.m_strAnaMode_All = objDataRow["anamode_all"].ToString();
                    objRecordContent.m_strAnaModeXML = objDataRow["anamodexml"].ToString();

                    objRecordContent.m_strAssistant1 = objDataRow["m_strassistant1"].ToString();
                    objRecordContent.m_strAssistant2 = objDataRow["m_strassistant2"].ToString();
                    objRecordContent.m_strAssistant3 = objDataRow["m_strassistant3"].ToString();
                    objRecordContent.m_strChiefDoctor = objDataRow["m_strchiefdoctor"].ToString();
                    objRecordContent.m_strChiefOperator = objDataRow["m_strchiefoperator"].ToString();
                    objRecordContent.m_strDeActivedOperatorID = objDataRow["m_strdeactivedoperatorid"].ToString();
                    objRecordContent.m_strDiseaseName = objDataRow["m_strdiseasename"].ToString();
                    objRecordContent.m_strDiseaseName_All = objDataRow["m_strdiseasename_all"].ToString();
                    objRecordContent.m_strDiseaseName_XML = objDataRow["m_strdiseasename_xml"].ToString();
                    objRecordContent.m_strRemark = objDataRow["m_strremark"].ToString();
                    objRecordContent.m_strRemark_All = objDataRow["m_strremark_all"].ToString();
                    objRecordContent.m_strRemarkXML = objDataRow["m_strremarkxml"].ToString();
                    objRecordContent.m_strSupervisoryDoctor = objDataRow["m_strsupervisorydoctor"].ToString();
                    objRecordContent.m_strOperationRoom = objDataRow["operationroomname"].ToString().Trim();
                    objRecordContent.m_strPriority = objDataRow["priority"].ToString().Trim();

                    objRecordContent.m_strDiagnose = objDataRow["diagnose"].ToString();
                    objRecordContent.m_strOperationName = objDataRow["operationname"].ToString();
                    objRecordContent.m_strAnaMode = objDataRow["anamode"].ToString();

                    p_objRecordContent = objRecordContent;
                }
                //返回
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }

        [AutoComplete]
        public long m_lngGetAnaDoctor(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            ref clsAnaesthesia_PlanAnasthetistValue[] p_obj)
        {
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                long lngRes = 0;
                string strSql = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.anaesthetistid,
       a.modifydate,
       a.status,
       a.ifchief,
       a.operatorid,
       a.deactiveddate,
       b.lastname_vchr
  from relation_operationanaesthetist a, t_bse_employee b where inpatientid=? and inpatientdate=? and opendate=? and trim(a.anaesthetistid)=trim(b.empid_chr) and a.status = 0";

                DataTable dtbValue = null;

                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);//(c_strGetRecordContentSQL,ref dtbValue,objDPArr);
                if (dtbValue != null)
                {
                    int intRowCount = dtbValue.Rows.Count;
                    if (dtbValue.Rows.Count > 0)
                    {
                        p_obj = new clsAnaesthesia_PlanAnasthetistValue[dtbValue.Rows.Count];
                        for (int i = 0; i < dtbValue.Rows.Count; i++)
                        {
                            System.Data.DataRow objDataRow = dtbValue.Rows[i];
                            p_obj[i] = new clsAnaesthesia_PlanAnasthetistValue();
                            p_obj[i].m_strInPatientID = objDataRow["inpatientid"].ToString();
                            p_obj[i].m_dtmInPatientDate = DateTime.Parse(objDataRow["inpatientdate"].ToString());
                            p_obj[i].m_dtmModifyDate = DateTime.Parse(objDataRow["modifydate"].ToString());
                            p_obj[i].m_dtmOpenDate = DateTime.Parse(objDataRow["opendate"].ToString());
                            p_obj[i].m_strAnaesthetistID = objDataRow["anaesthetistid"].ToString();
                            p_obj[i].m_strAnaesthetistName = objDataRow["lastname_vchr"].ToString();
                            p_obj[i].m_strChiefFlag = objDataRow["ifchief"].ToString();
                        }
                    }
                }
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }

        [AutoComplete]
        public long m_lngGetPACClass(string p_strPatientID, string p_strInPatientDate, ref DataTable p_dt)
        {
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (p_strPatientID == null)
                    return -1;
                if (p_strPatientID.Trim().Length == 0)
                    return -1;
                if (p_strInPatientDate == null)
                    return -1;
                if (p_strInPatientDate.Trim().Length == 0)
                    return -1;
                //此表暂时未找到
                string strSql = @"select * from pacinfo where trim(patient_id) = ? and inpatient_date = ? order by exam_date";
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strPatientID.Trim();
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                p_dt = null;
                objHRPSvc.lngGetDataTableWithParameters(strSql, ref p_dt, objDPArr);
                return 1;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }

        /// <summary>
        /// 建立急诊病人基本信息并返回建立的急诊病人的ID
        /// </summary>
        /// <param name="p_Name">病人姓名</param>
        /// <param name="p_strSex">病人性别</param>
        /// <param name="PatientID">急诊病人的ID</param>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_strAreaID">病区ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCreatePatientBaseINFO_BY_Emergency(string p_Name, string p_strSex, ref string PatientID, ref string p_strDeptID, ref string p_strAreaID)
        {
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_Name;
                objDPArr[1].Value = p_strSex;
                objDPArr[2].Value = "                       ";
                objDPArr[2].Direction = ParameterDirection.InputOutput;
                objDPArr[3].Value = "                       ";
                objDPArr[3].Direction = ParameterDirection.InputOutput;
                objDPArr[4].Value = "                       ";
                objDPArr[4].Direction = ParameterDirection.InputOutput;
                long lngRes = objHRPSvc.lngExecuteProc("P_ANAEmergencyPatientBASEINFO", objDPArr);
                PatientID = objDPArr[2].Value.ToString();
                p_strDeptID = objDPArr[3].Value.ToString();
                p_strAreaID = objDPArr[4].Value.ToString();
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }

        /// <summary>
        /// 根据EMRID和住院日期获取HISID
        /// </summary>
        /// <param name="p_strEMRID"></param>
        /// <param name="p_strHISID">病人HISID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetHISIDByEMRID(string p_strEMRID, DateTime p_dtEMRInPatientDate, out string p_strHISID)
        {
            p_strHISID = null;
            if (string.IsNullOrEmpty(p_strEMRID))
                return -1;
            long lngRes = 0;
            string strSQl = @"select t.hisinpatientid_chr
							from t_bse_hisemr_relation t
							where t.emrinpatientid = ?
                            and emrinpatientdate = ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strEMRID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtEMRInPatientDate;

                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQl, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_strHISID = dtResult.Rows[0]["hisinpatientid_chr"].ToString();
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
                objHRPSvc.Dispose();
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngAuditing(string p_strInPatientID,
            DateTime p_dtmInPatientDate,
            DateTime p_dtmOpenDate)
        {
            long lngRes = 0;
            string strSQl = @"update operationrequisition t
   set t.ifconfirm = 1
 where t.inpatientid = ?
   and t.inpatientdate = ?
   and t.opendate = ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = p_dtmInPatientDate;
                objDPArr[2].Value = p_dtmOpenDate;
                long lngAfft = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQl, ref lngAfft, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
    }
}
