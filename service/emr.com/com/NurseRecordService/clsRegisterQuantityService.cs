using System.EnterpriseServices;
using System;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.BaseCaseHistorySevice;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Drawing;
using System.Collections;

namespace com.digitalwave.clsRecordsService
{
	/// <summary>
	/// Summary description for Class1.
	/// 出入量登记表
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]	
	public class clsRegisterQuantityService:clsBaseCaseHistorySevice
	{
		public clsRegisterQuantityService()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private const string c_strGetTimeListSQL="select inpatientdate,createdate,opendate from t_emr_registerquantity where inpatientid = ?  and status=0 order by opendate desc";
		
		/// <summary>
		/// SQL Statement
		/// </summary>
        private const string c_strGetRecordSQL_ByDate = @"select t1.inpatientid,
       t1.inpatientdate,
       t1.createdate,
       t1.createuserid,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.lastprintdate,
       t1.firstprintdate,
       t1.opendate,
       t1.regdate,
       t1.customoutcolumnname,
       t1.customincolumnname,
       t1.outsummary,
       t1.outsummaryxml,
       t1.outsummaryrate,
       t1.outsummaryratexml,
       t1.outurinesummary,
       t1.outurinesummaryxml,
       t1.insummary,
       t1.insummaryxml,
       t1.recordersignid,
       t1.recordersignname,
       t1.regid,
       t1.modifydate,
       t1.modifyuserid
  from t_emr_registerquantity t1
 where t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.regdate = ?
 order by t1.regdate";
        private const string c_strGetAllMainContentSQL = @"select t.inpatientid,
       t.inpatientdate,
       t.createdate,
       t.createuserid,
       t.status,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.lastprintdate,
       t.firstprintdate,
       t.opendate,
       t.regdate,
       t.customoutcolumnname,
       t.customincolumnname,
       t.outsummary,
       t.outsummaryxml,
       t.outsummaryrate,
       t.outsummaryratexml,
       t.outurinesummary,
       t.outurinesummaryxml,
       t.insummary,
       t.insummaryxml,
       t.recordersignid,
       t.recordersignname,
       t.regid,
       t.modifydate,
       t.modifyuserid
  from t_emr_registerquantity t
 where t.status = 0
   and inpatientid = ?
   and t.inpatientdate = ?
 order by t.regdate";
        private const string c_strGetRecordContent_PictureSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       picid,
       backimage,
       frontimage,
       backcolor,
       width,
       height
  from inpatientcasehistory_picture
 where inpatientid = ?
   and inpatientdate = ?";

        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.lastprintdate,
       a.firstprintdate,
       a.opendate,
       a.regdate,
       a.customoutcolumnname,
       a.customincolumnname,
       a.outsummary,
       a.outsummaryxml,
       a.outsummaryrate,
       a.outsummaryratexml,
       a.outurinesummary,
       a.outurinesummaryxml,
       a.insummary,
       a.insummaryxml,
       a.recordersignid,
       a.recordersignname,
       a.regid,
       a.modifydate,
       a.modifyuserid,
       b.lastmodifydate,
       b.lastmodifyuserid,
       b.deactiveddate,
       b.deactivedoperatorid,
       b.status,
       b.maindescription,
       b.currentstatus,
       b.beforetimestatus,
       b.ownhistory,
       b.marriagehistory,
       b.catameniahistory,
       b.familyhistory,
       b.summary,
       b.temperature,
       b.pulse,
       b.breath,
       b.sys,
       b.dia,
       b.bloodpressureunit,
       b.medical,
       b.professionalcheck,
       b.labcheck,
       b.firstcatamenia,
       b.catamenialasttime,
       b.catameniacycle,
       b.lastcatameniatime,
       b.catameniacase,
       b.yjs,
       b.contrahistory,
       b.shys,
       b.lcqk,
       b.cqjc,
       b.pregtimes,
       b.borntimes,
       b.careplan,
       b.chargedoctor,
       b.diretdoctor,
       b.oldmaternitysuffer,
       b.midwife,
       b.modifydiagnose,
       b.adddiagnose,
       b.diagnosenor,
       f_getempnamebyno(a.createuserid) as firstname
  from t_emr_registerquantity a, ipcasehistory_historycontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.lastmodifydate = (select max(lastmodifydate)
                             from ipcasehistory_historycontent
                            where inpatientid = a.inpatientid
                              and inpatientdate = a.inpatientdate
                              and opendate = a.opendate)";
        private const string c_strGetDeletedRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.lastprintdate,
       a.firstprintdate,
       a.opendate,
       a.regdate,
       a.customoutcolumnname,
       a.customincolumnname,
       a.outsummary,
       a.outsummaryxml,
       a.outsummaryrate,
       a.outsummaryratexml,
       a.outurinesummary,
       a.outurinesummaryxml,
       a.insummary,
       a.insummaryxml,
       a.recordersignid,
       a.recordersignname,
       a.regid,
       a.modifydate,
       a.modifyuserid,
       b.lastmodifydate,
       b.lastmodifyuserid,
       b.deactiveddate,
       b.deactivedoperatorid,
       b.status,
       b.maindescription,
       b.currentstatus,
       b.beforetimestatus,
       b.ownhistory,
       b.marriagehistory,
       b.catameniahistory,
       b.familyhistory,
       b.summary,
       b.temperature,
       b.pulse,
       b.breath,
       b.sys,
       b.dia,
       b.bloodpressureunit,
       b.medical,
       b.professionalcheck,
       b.labcheck,
       b.firstcatamenia,
       b.catamenialasttime,
       b.catameniacycle,
       b.lastcatameniatime,
       b.catameniacase,
       b.yjs,
       b.contrahistory,
       b.shys,
       b.lcqk,
       b.cqjc,
       b.pregtimes,
       b.borntimes,
       b.careplan,
       b.chargedoctor,
       b.diretdoctor,
       b.oldmaternitysuffer,
       b.midwife,
       b.modifydiagnose,
       b.adddiagnose,
       b.diagnosenor,
       f_getempnamebyno(a.createuserid) as firstname
  from t_emr_registerquantity a, ipcasehistory_historycontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 1
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.lastmodifydate = (select max(lastmodifydate)
                             from ipcasehistory_historycontent
                            where inpatientid = a.inpatientid
                              and inpatientdate = a.inpatientdate
                              and opendate = a.opendate)";
		private const string c_strCheckRegDateSQL=@"select createdate,createuserid ,modifyuserid,modifydate,modifyusername 
														from t_emr_registerquantity  a
														where inpatientid = ? 
                                                        and a.inpatientdate= ?													
														and a.regdate= ?
                                                        and a.regid<> ?
														and a.status=0";



        private const string c_strGetContentSQL_FromRevisit = @"select a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.lastprintdate,
       a.firstprintdate,
       a.opendate,
       a.regdate,
       a.customoutcolumnname,
       a.customincolumnname,
       a.outsummary,
       a.outsummaryxml,
       a.outsummaryrate,
       a.outsummaryratexml,
       a.outurinesummary,
       a.outurinesummaryxml,
       a.insummary,
       a.insummaryxml,
       a.recordersignid,
       a.recordersignname,
       a.regid,
       a.modifydate,
       a.modifyuserid
  from t_emr_registerquantity a, ipcasehistory_historycontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.lastmodifydate = (select max(lastmodifydate)
                             from ipcasehistory_historycontent
                            where inpatientid = a.inpatientid
                              and inpatientdate = a.inpatientdate
                              and opendate = a.opendate)";

		#region old code
		//		private const string c_strGetRecordContentSQL="select top 1 *  from InPatientCaseHistory_History as ms,IPCaseHistory_HistoryContent as co, t_bse_employee as EBI "+
		//			"where ms.CreateUserID=EBI.EmployeeID and ms.InPatientID= ? and ms.InPatientDate = ? and ms.OpenDate = ? and ms.Status=0 "+
		//			"and ms.InPatientID=co.InPatientID and ms.InPatientDate=co.InPatientDate and ms.OpenDate=co.OpenDate order by co.LastModifyDate desc";
		#endregion

        private const string c_strCheckCreateDateSQL = @"select inpatientid,
       inpatientdate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       lastprintdate,
       firstprintdate,
       opendate,
       regdate,
       customoutcolumnname,
       customincolumnname,
       outsummary,
       outsummaryxml,
       outsummaryrate,
       outsummaryratexml,
       outurinesummary,
       outurinesummaryxml,
       insummary,
       insummaryxml,
       recordersignid,
       recordersignname,
       regid,
       modifydate,
       modifyuserid
  from t_emr_registerquantity
 where inpatientid = ?
   and inpatientdate = ?
   and status = 0";

		//		private const string c_strGetExistInfoSQL="";

		/// <summary>
		/// 更新GeneralDiseaseRecord中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= "update  t_emr_registerquantity set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";


		//		private const string c_strGetModifyRecordSQL="";
		
		private const string c_strGetDeleteRecordSQL="";
		private const string c_strGetFirstPrintDate=@"select firstprintdate from t_emr_registerquantity where inpatientid = ?  and inpatientdate = ? and status = 0 ";
		private const string c_strGetModifyDateAndFirstPrintDateSQL=  @"select a.firstprintdate,b.lastmodifydate from t_emr_registerquantity a,ipcasehistory_historycontent b where inpatientid = ? and a.inpatientdate= ?  and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.lastmodifydate=(select max(lastmodifydate) from ipcasehistory_historycontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strModifyRecord_PictureSQL = "update inpatientcasehistory_picture set picid=?,frontimage=?,backcolor=?,width=?,height=? where inpatientid=? and inpatientdate=? and opendate=?" ;

		private const string c_strDeleteRecord_PictureSQL = "delete inpatientcasehistory_picture where inpatientid=? and inpatientdate=? and opendate=?" ;

		private const string c_strModifyRecordSQL=@"update t_emr_registerquantity 
														set status = 1, modifydate = ?, modifyuserid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and regdate = ?
														and status = 0";

		private const string c_strModifyRecordSQL2=@"update t_emr_registerquantity 
														set status = 1, modifydate = ?, modifyuserid = ?
													where regid = ?
														 and status = 0";


		private const string c_strDeleteRecordSQL="update t_emr_registerquantity set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and regdate=? and status=0";

		private const string c_strAddNewRecord_PictureSQL = @"insert into inpatientcasehistory_picture(inpatientid,inpatientdate,opendate,picid,frontimage,backcolor,width,height) 
		values(?,?,?,?,?,?,?,?)";

		private const string c_strAddNewPatient_Disease = @"insert into patient_associate
      (inpatientid, inpatientdate,associateid)
values (?,?,?)";

        private const string c_strGetRecordContentSQL_ByRegID = @"select t1.regperiod,
       t1.initem1,
       t1.initem1xml,
       t1.initem2,
       t1.initem2xml,
       t1.initem3,
       t1.initem3xml,
       t1.initem4,
       t1.initem4xml,
       t1.initem5,
       t1.initem5xml,
       t1.initem6,
       t1.initem6xml,
       t1.initem7,
       t1.initem7xml,
       t1.initem8,
       t1.initem8xml,
       t1.outitem1,
       t1.outitem1xml,
       t1.outitem2,
       t1.outitem2xml,
       t1.outitem3,
       t1.outitem3xml,
       t1.outitem4,
       t1.outitem4xml,
       t1.outitem5,
       t1.outitem5xml,
       t1.outitem6,
       t1.outitem6xml,
       t1.outitem7,
       t1.outitem7xml,
       t1.outitem8,
       t1.outitem8xml,
       t1.status,
       t1.modifiydate,
       t1.modifiyuserid,
       t1.regid
  from t_emr_registerquantitydetail t1
 where t1.regid = ?
 order by t1.regperiod";

		private const string c_strAddNewRecordSQL= @"insert into t_emr_registerquantity (inpatientid,inpatientdate,
				opendate,createdate,createuserid,status,regid,recordersignid,recordersignname,insummary,insummaryxml,customincolumnname,customoutcolumnname,outsummary,outsummaryxml,
                outurinesummary,outurinesummaryxml,outsummaryrate,outsummaryratexml,regdate,modifyuserid,modifydate,modifyusername) 
				values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		
		private const string c_strAddNewRecordContentSQL=  @"insert into t_emr_registerquantitydetail (regid,regperiod,initem1, initem1xml,initem2, initem2xml, 
        initem3, initem3xml,initem4, initem4xml,initem5, initem5xml,initem6, initem6xml, initem7, initem7xml, initem8,  initem8xml, outitem1,outitem1xml,outitem2, outitem2xml,outitem3, outitem3xml,outitem4, outitem4xml,outitem5,outitem5xml,
         outitem6, outitem6xml,outitem7, outitem7xml,outitem8,outitem8xml,status) 
		values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";//35个参数

		private const string c_strAddNewRecordContent_PrimaryDiagnoseSQL=@"insert into  ipcasehistcont_primarydiagnose(inpatientid,
						inpatientdate,opendate,lastmodifydate,indexid,primarydiagnose)
						values(?,?,?,?,?,?)";

		private const string c_strAddNewRecordContent_FinallyDiagnoseSQL=@"insert into  ipcasehistcont_finallydiagnose(inpatientid,
						inpatientdate,opendate,lastmodifydate,indexid,finallydiagnose)
						values(?,?,?,?,?,?)";

		// 获取病人的该记录时间列表。
		[AutoComplete] 
		public override long m_lngGetRecordTimeList(
			string p_strInPatientID,
			out string[] p_strInPatientDateArr,
			out string[] p_strCreateRecordTimeArr,
			out string[] p_strOpenRecordTimeArr)
		{
			p_strInPatientDateArr = null;
			p_strCreateRecordTimeArr=null;
			p_strOpenRecordTimeArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRegisterQuantityService", "m_lngGetRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strInPatientDateArr = new string[dtbValue.Rows.Count];
                    p_strCreateRecordTimeArr = new string[dtbValue.Rows.Count];
                    p_strOpenRecordTimeArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strInPatientDateArr[i] = dtbValue.Rows[i]["INPATIENTDATE"].ToString();
                        p_strCreateRecordTimeArr[i] = dtbValue.Rows[i]["CREATEDATE"].ToString();
                        p_strOpenRecordTimeArr[i] = dtbValue.Rows[i]["OPENDATE"].ToString();
                    }
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

            }			//返回
			return lngRes;

		
		}
		#region 得到日期改变时的内容
		/// <summary>
		/// 修改日期
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strRecordOpenDate"></param>
		/// <param name="p_objTansDataInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordContent(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strRegDate ,
			out clsRegisterQuantity_VODataInfo p_objTansDataInfo)
		{
			
			//检查参数
			p_objTansDataInfo=new clsRegisterQuantity_VODataInfo();
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
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
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strRegDate);

                //生成DataTable
                DataTable dtbValue = new DataTable();
                clsRegisterQuantity_VO objContentDetaill = new clsRegisterQuantity_VO();
                //执行查询，填充结果到DataTable       
                clsRegisterQuantity_VO objMainRecordInfo = new clsRegisterQuantity_VO();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordSQL_ByDate, ref dtbValue, objDPArr);
                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    //p_objTansDataInfo = new clsRegisterQuantity_VO[dtbValue.Rows.Count];

                    //clsRegisterQuantity_VO objRecordContent= null;
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        //获取当前DataTable记录的OpenDate，记录在dtmOpenDate
                        objMainRecordInfo.m_intRegID = Convert.ToInt32(dtbValue.Rows[j]["RegID"].ToString());
                        objMainRecordInfo.m_strCreateUserID = dtbValue.Rows[j]["CreateUserID"].ToString();
                        objMainRecordInfo.m_strCustomInComumnName = dtbValue.Rows[j]["CustomIncolumnName"].ToString();
                        objMainRecordInfo.m_strCustomOutComumnName = dtbValue.Rows[j]["CustomOutColumnName"].ToString();
                        objMainRecordInfo.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["InPatientDate"].ToString());
                        objMainRecordInfo.m_strInPatientID = dtbValue.Rows[j]["InPatientID"].ToString();
                        objMainRecordInfo.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[j]["CreateDate"].ToString());
                        objMainRecordInfo.m_dtmRegDate = Convert.ToDateTime(dtbValue.Rows[j]["regdate"].ToString());
                        objMainRecordInfo.m_bytStatus = Convert.ToByte(dtbValue.Rows[j]["status"].ToString());
                        objMainRecordInfo.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[j]["Opendate"].ToString());
                        objMainRecordInfo.m_strInSummary = dtbValue.Rows[j]["InSummary"].ToString();
                        objMainRecordInfo.m_strInSummaryXML = dtbValue.Rows[j]["InSummaryXML"].ToString();
                        objMainRecordInfo.m_strOutSummary = dtbValue.Rows[j]["OutSummary"].ToString();
                        objMainRecordInfo.m_strOutSummaryXML = dtbValue.Rows[j]["OutSummaryXML"].ToString();
                        objMainRecordInfo.m_strOutSummaryRate = dtbValue.Rows[j]["OutSummaryRate"].ToString();
                        objMainRecordInfo.m_strOutSummaryRateXML = dtbValue.Rows[j]["OutSummaryRateXML"].ToString();
                        objMainRecordInfo.m_strOutUrineSummary = dtbValue.Rows[j]["OutUrineSummary"].ToString();
                        objMainRecordInfo.m_strOutUrineSummaryXML = dtbValue.Rows[j]["OutUrineSummaryXML"].ToString();

                        objMainRecordInfo.m_strRecordersignID = dtbValue.Rows[j]["recordersignID"].ToString();
                        objMainRecordInfo.m_strRecordersignName = dtbValue.Rows[j]["recordersignName"].ToString();

                    }
                    //获取当前的详细记录
                    p_objTansDataInfo.m_objMainRecord = (clsRegisterQuantity_VO)objMainRecordInfo;
                    ArrayList arlModifyData = new ArrayList();
                    objDPArr = null;

                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = objMainRecordInfo.m_intRegID; ;

                    //执行查询，填充结果到DataTable       
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_ByRegID, ref dtbValue, objDPArr);
                    //循环DataTable
                    if (lngRes > 0 && dtbValue.Rows.Count > 0)
                    {
                        //p_objTansDataInfo = new clsRegisterQuantity_VO[dtbValue.Rows.Count];

                        //clsRegisterQuantity_VO objRecordContent= null;
                        for (int j = 0; j < dtbValue.Rows.Count; j++)
                        {
                            //获取当前DataTable记录的OpenDate，记录在dtmOpenDate

                            objContentDetaill = new clsRegisterQuantity_VO();
                            objContentDetaill.m_intPeriodID = Convert.ToInt32(dtbValue.Rows[j]["regperiod"].ToString());
                            objContentDetaill.m_strInItem1 = dtbValue.Rows[j]["InItem1"].ToString();
                            objContentDetaill.m_strInItem2 = dtbValue.Rows[j]["InItem2"].ToString();
                            objContentDetaill.m_strInItem3 = dtbValue.Rows[j]["InItem3"].ToString();
                            objContentDetaill.m_strInItem4 = dtbValue.Rows[j]["InItem4"].ToString();
                            objContentDetaill.m_strInItem5 = dtbValue.Rows[j]["InItem5"].ToString();
                            objContentDetaill.m_strInItem6 = dtbValue.Rows[j]["InItem6"].ToString();
                            objContentDetaill.m_strInItem7 = dtbValue.Rows[j]["InItem7"].ToString();
                            objContentDetaill.m_strInItem8 = dtbValue.Rows[j]["InItem8"].ToString();

                            objContentDetaill.m_strOutItem1 = dtbValue.Rows[j]["OutItem1"].ToString();
                            objContentDetaill.m_strOutItem2 = dtbValue.Rows[j]["OutItem2"].ToString();
                            objContentDetaill.m_strOutItem3 = dtbValue.Rows[j]["OutItem3"].ToString();
                            objContentDetaill.m_strOutItem4 = dtbValue.Rows[j]["OutItem4"].ToString();
                            objContentDetaill.m_strOutItem5 = dtbValue.Rows[j]["OutItem5"].ToString();
                            objContentDetaill.m_strOutItem6 = dtbValue.Rows[j]["OutItem6"].ToString();
                            objContentDetaill.m_strOutItem7 = dtbValue.Rows[j]["OutItem7"].ToString();
                            objContentDetaill.m_strOutItem8 = dtbValue.Rows[j]["OutItem8"].ToString();

                            objContentDetaill.m_strInItem1XML = dtbValue.Rows[j]["InItem1XML"].ToString();
                            objContentDetaill.m_strInItem2XML = dtbValue.Rows[j]["InItem2XML"].ToString();
                            objContentDetaill.m_strInItem3XML = dtbValue.Rows[j]["InItem3XML"].ToString();
                            objContentDetaill.m_strInItem4XML = dtbValue.Rows[j]["InItem4XML"].ToString();
                            objContentDetaill.m_strInItem5XML = dtbValue.Rows[j]["InItem5XML"].ToString();
                            objContentDetaill.m_strInItem6XML = dtbValue.Rows[j]["InItem6XML"].ToString();
                            objContentDetaill.m_strInItem7XML = dtbValue.Rows[j]["InItem7XML"].ToString();
                            objContentDetaill.m_strInItem8XML = dtbValue.Rows[j]["InItem8XML"].ToString();

                            objContentDetaill.m_strOutItem1XML = dtbValue.Rows[j]["OutItem1XML"].ToString();
                            objContentDetaill.m_strOutItem2XML = dtbValue.Rows[j]["OutItem2XML"].ToString();
                            objContentDetaill.m_strOutItem3XML = dtbValue.Rows[j]["OutItem3XML"].ToString();
                            objContentDetaill.m_strOutItem4XML = dtbValue.Rows[j]["OutItem4XML"].ToString();
                            objContentDetaill.m_strOutItem5XML = dtbValue.Rows[j]["OutItem5XML"].ToString();
                            objContentDetaill.m_strOutItem6XML = dtbValue.Rows[j]["OutItem6XML"].ToString();
                            objContentDetaill.m_strOutItem7XML = dtbValue.Rows[j]["OutItem7XML"].ToString();
                            objContentDetaill.m_strOutItem8XML = dtbValue.Rows[j]["OutItem8XML"].ToString();

                            arlModifyData.Add(objContentDetaill);

                        }
                        p_objTansDataInfo.m_objRecordArr = (clsRegisterQuantity_VO[])arlModifyData.ToArray(typeof(clsRegisterQuantity_VO));
                        arlModifyData.Clear();

                        //获取当前的详细记录

                    }


                }


            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            } return (long)enmOperationResult.DB_Succeed;
		}
		#endregion 
		#region 得到分页记录
		/// <summary>
		/// 得到分页记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strRecordOpenDate"></param>
		/// <param name="p_objTansDataInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetCustomsDataPage(clsRegisterQuantity_VO objContent,	
			out clsRegisterQuantity_VODataInfo[] p_objTansDataInfo,out int p_intReturnPageCount,out int p_intReturnPageIndex)
		{

			
			int iRecordCount=0;
			long lngRes=0;
			string sPageSQL="";
			p_objTansDataInfo=null;
			p_intReturnPageCount=0;
			p_intReturnPageIndex=objContent.m_intCurrentPage;


            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
 
                ArrayList arlModifyData = new ArrayList();
                DataTable dtbValue = new DataTable();
                DataTable dtbValueDetail = new DataTable();

                IDataParameter[] objDPArr = null;
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;

                clsRegisterQuantity_VO objMainRecordInfo;
                clsRegisterQuantity_VODataInfo objTemp = new clsRegisterQuantity_VODataInfo();
                clsRegisterQuantity_VO objContentDetaill = new clsRegisterQuantity_VO();


                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetAllMainContentSQL, ref dtbValue, objDPArr);
                //循环DataTable
                if (lngRes > 0)
                    iRecordCount = dtbValue.Rows.Count;
                else
                    return (long)enmOperationResult.DB_Fail;

                p_intReturnPageIndex = 1;
                p_intReturnPageCount = Convert.ToInt32(iRecordCount);
                switch (objContent.m_strGoPage)
                {
                    case "First":
                        p_intReturnPageIndex = 1;
                        break;
                    case "Next":
                        p_intReturnPageIndex = objContent.m_intCurrentPage + 1;
                        break;
                    case "Previous":
                        p_intReturnPageIndex = objContent.m_intCurrentPage - 1;
                        break;
                    case "Last":
                        p_intReturnPageIndex = p_intReturnPageCount;
                        break;
                    case "Refresh":
                        p_intReturnPageIndex = objContent.m_intCurrentPage;
                        break;
                }

                if (p_intReturnPageIndex > p_intReturnPageCount) p_intReturnPageIndex = p_intReturnPageCount;
                if (p_intReturnPageIndex < 1) p_intReturnPageIndex = 1;
                //没有记录返回
                if (iRecordCount == 0) return (long)enmOperationResult.DB_Succeed;

                if (clsHRPTableService.bytDatabase_Selector==2)
                {
                    if (iRecordCount > 1)//如果只是需要1页显示
                    {
                        //如果页数显示2页的记录数比当前记录数大，则只是取1条
                        if ((p_intReturnPageIndex * 1) > iRecordCount)
                        {
                            sPageSQL = @"select inpatientid,
       inpatientdate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       lastprintdate,
       firstprintdate,
       opendate,
       regdate,
       customoutcolumnname,
       customincolumnname,
       outsummary,
       outsummaryxml,
       outsummaryrate,
       outsummaryratexml,
       outurinesummary,
       outurinesummaryxml,
       insummary,
       insummaryxml,
       recordersignid,
       recordersignname,
       regid,
       modifydate,
       modifyuserid
  from (select inpatientid,
               inpatientdate,
               createdate,
               createuserid,
               status,
               deactiveddate,
               deactivedoperatorid,
               lastprintdate,
               firstprintdate,
               opendate,
               regdate,
               customoutcolumnname,
               customincolumnname,
               outsummary,
               outsummaryxml,
               outsummaryrate,
               outsummaryratexml,
               outurinesummary,
               outurinesummaryxml,
               insummary,
               insummaryxml,
               recordersignid,
               recordersignname,
               regid,
               modifydate,
               modifyuserid
          from (select inpatientid,
                       inpatientdate,
                       createdate,
                       createuserid,
                       status,
                       deactiveddate,
                       deactivedoperatorid,
                       lastprintdate,
                       firstprintdate,
                       opendate,
                       regdate,
                       customoutcolumnname,
                       customincolumnname,
                       outsummary,
                       outsummaryxml,
                       outsummaryrate,
                       outsummaryratexml,
                       outurinesummary,
                       outurinesummaryxml,
                       insummary,
                       insummaryxml,
                       recordersignid,
                       recordersignname,
                       regid,
                       modifydate,
                       modifyuserid
                  from t_emr_registerquantity
                 where inpatientid = ?
                   and inpatientdate = ?
                   and status = 0
                 order by regdate)
         where rownum <= " + Convert.ToString(1 * p_intReturnPageIndex) + " order by  regdate desc) where rownum<=1 order by regdate";
                        }
                        else
                        {
                            sPageSQL = @"select inpatientid,
       inpatientdate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       lastprintdate,
       firstprintdate,
       opendate,
       regdate,
       customoutcolumnname,
       customincolumnname,
       outsummary,
       outsummaryxml,
       outsummaryrate,
       outsummaryratexml,
       outurinesummary,
       outurinesummaryxml,
       insummary,
       insummaryxml,
       recordersignid,
       recordersignname,
       regid,
       modifydate,
       modifyuserid
  from (select inpatientid,
               inpatientdate,
               createdate,
               createuserid,
               status,
               deactiveddate,
               deactivedoperatorid,
               lastprintdate,
               firstprintdate,
               opendate,
               regdate,
               customoutcolumnname,
               customincolumnname,
               outsummary,
               outsummaryxml,
               outsummaryrate,
               outsummaryratexml,
               outurinesummary,
               outurinesummaryxml,
               insummary,
               insummaryxml,
               recordersignid,
               recordersignname,
               regid,
               modifydate,
               modifyuserid
          from (select inpatientid,
                       inpatientdate,
                       createdate,
                       createuserid,
                       status,
                       deactiveddate,
                       deactivedoperatorid,
                       lastprintdate,
                       firstprintdate,
                       opendate,
                       regdate,
                       customoutcolumnname,
                       customincolumnname,
                       outsummary,
                       outsummaryxml,
                       outsummaryrate,
                       outsummaryratexml,
                       outurinesummary,
                       outurinesummaryxml,
                       insummary,
                       insummaryxml,
                       recordersignid,
                       recordersignname,
                       regid,
                       modifydate,
                       modifyuserid
                  from t_emr_registerquantity
                 where inpatientid = ?
                   and inpatientdate = ?
                   and status = 0
                 order by regdate)
         where rownum <=" + Convert.ToString(1 * p_intReturnPageIndex) + " order by  regdate desc) where rownum<=1 order by regdate";
                        }


                    }
                    else
                    {
                        sPageSQL = @"select inpatientid,
       inpatientdate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       lastprintdate,
       firstprintdate,
       opendate,
       regdate,
       customoutcolumnname,
       customincolumnname,
       outsummary,
       outsummaryxml,
       outsummaryrate,
       outsummaryratexml,
       outurinesummary,
       outurinesummaryxml,
       insummary,
       insummaryxml,
       recordersignid,
       recordersignname,
       regid,
       modifydate,
       modifyuserid
  from (select inpatientid,
               inpatientdate,
               createdate,
               createuserid,
               status,
               deactiveddate,
               deactivedoperatorid,
               lastprintdate,
               firstprintdate,
               opendate,
               regdate,
               customoutcolumnname,
               customincolumnname,
               outsummary,
               outsummaryxml,
               outsummaryrate,
               outsummaryratexml,
               outurinesummary,
               outurinesummaryxml,
               insummary,
               insummaryxml,
               recordersignid,
               recordersignname,
               regid,
               modifydate,
               modifyuserid
          from t_emr_registerquantity
         where inpatientid = ?
           and inpatientdate = ?
           and status = 0
         order by regdate desc)
 where rownum <= 1
 order by regdate ";
                    }
                }
                else if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    if (iRecordCount > 2)//如果只是需要1页显示
                    {
                        sPageSQL = @"select top 2 a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.lastprintdate,
       a.firstprintdate,
       a.opendate,
       a.regdate,
       a.customoutcolumnname,
       a.customincolumnname,
       a.outsummary,
       a.outsummaryxml,
       a.outsummaryrate,
       a.outsummaryratexml,
       a.outurinesummary,
       a.outurinesummaryxml,
       a.insummary,
       a.insummaryxml,
       a.recordersignid,
       a.recordersignname,
       a.regid,
       a.modifydate,
       a.modifyuserid
  from (select top " + Convert.ToString((iRecordCount - 2 * p_intReturnPageIndex)) + @" inpatientid,
               inpatientdate,
               createdate,
               createuserid,
               status,
               deactiveddate,
               deactivedoperatorid,
               lastprintdate,
               firstprintdate,
               opendate,
               regdate,
               customoutcolumnname,
               customincolumnname,
               outsummary,
               outsummaryxml,
               outsummaryrate,
               outsummaryratexml,
               outurinesummary,
               outurinesummaryxml,
               insummary,
               insummaryxml,
               recordersignid,
               recordersignname,
               regid,
               modifydate,
               modifyuserid
          from t_emr_registerquantity
         where inpatientid = ?
           and inpatientdate = ?
           and opendate = ?
           and status = 0
         order by regdate asc) a
 order by regdate desc";

                    }
                    else
                    {
                        sPageSQL = @"select top 2 inpatientid,
       inpatientdate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       lastprintdate,
       firstprintdate,
       opendate,
       regdate,
       customoutcolumnname,
       customincolumnname,
       outsummary,
       outsummaryxml,
       outsummaryrate,
       outsummaryratexml,
       outurinesummary,
       outurinesummaryxml,
       insummary,
       insummaryxml,
       recordersignid,
       recordersignname,
       regid,
       modifydate,
       modifyuserid
  from t_emr_registerquantity
 where inpatientid = ?
   and inpatientdate = ?
   and status = 0
 order by regdate desc";
                    }

                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    if (iRecordCount > 1)//如果只是需要1页显示
                    {
                        //如果页数显示2页的记录数比当前记录数大，则只是取1条
                        if ((p_intReturnPageIndex * 1) > iRecordCount)
                        {
                            sPageSQL = @" select inpatientid,
       inpatientdate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       lastprintdate,
       firstprintdate,
       opendate,
       regdate,
       customoutcolumnname,
       customincolumnname,
       outsummary,
       outsummaryxml,
       outsummaryrate,
       outsummaryratexml,
       outurinesummary,
       outurinesummaryxml,
       insummary,
       insummaryxml,
       recordersignid,
       recordersignname,
       regid,
       modifydate,
       modifyuserid
  from (select inpatientid,
               inpatientdate,
               createdate,
               createuserid,
               status,
               deactiveddate,
               deactivedoperatorid,
               lastprintdate,
               firstprintdate,
               opendate,
               regdate,
               customoutcolumnname,
               customincolumnname,
               outsummary,
               outsummaryxml,
               outsummaryrate,
               outsummaryratexml,
               outurinesummary,
               outurinesummaryxml,
               insummary,
               insummaryxml,
               recordersignid,
               recordersignname,
               regid,
               modifydate,
               modifyuserid
          from t_emr_registerquantity
         where inpatientid = ?
           and inpatientdate = ?
           and status = 0
         order by regdate) fetch first " + Convert.ToString(1 * p_intReturnPageIndex) + " row onlyorder by  regdate desc fetch first 1 row only";
                        }
                        else
                        {
                            sPageSQL = @" select inpatientid,
       inpatientdate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       lastprintdate,
       firstprintdate,
       opendate,
       regdate,
       customoutcolumnname,
       customincolumnname,
       outsummary,
       outsummaryxml,
       outsummaryrate,
       outsummaryratexml,
       outurinesummary,
       outurinesummaryxml,
       insummary,
       insummaryxml,
       recordersignid,
       recordersignname,
       regid,
       modifydate,
       modifyuserid
  from (select inpatientid,
               inpatientdate,
               createdate,
               createuserid,
               status,
               deactiveddate,
               deactivedoperatorid,
               lastprintdate,
               firstprintdate,
               opendate,
               regdate,
               customoutcolumnname,
               customincolumnname,
               outsummary,
               outsummaryxml,
               outsummaryrate,
               outsummaryratexml,
               outurinesummary,
               outurinesummaryxml,
               insummary,
               insummaryxml,
               recordersignid,
               recordersignname,
               regid,
               modifydate,
               modifyuserid
          from t_emr_registerquantity
         where inpatientid = ?
           and inpatientdate = ?
           and status = 0
         order by regdate) fetch first " + Convert.ToString(1 * p_intReturnPageIndex) + " row only order by  regdate desc fetch first 1 row only";
                        }


                    }
                    else
                    {
                        sPageSQL = @" select inpatientid,
       inpatientdate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       lastprintdate,
       firstprintdate,
       opendate,
       regdate,
       customoutcolumnname,
       customincolumnname,
       outsummary,
       outsummaryxml,
       outsummaryrate,
       outsummaryratexml,
       outurinesummary,
       outurinesummaryxml,
       insummary,
       insummaryxml,
       recordersignid,
       recordersignname,
       regid,
       modifydate,
       modifyuserid
  from t_emr_registerquantity
 where inpatientid = ?
   and inpatientdate = ?
   and status = 0
 order by regdate desc fetch first 1 row only";
                    }

                }

                p_objTansDataInfo = new clsRegisterQuantity_VODataInfo[iRecordCount];

                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;

                lngRes = objHRPServ.lngGetDataTableWithParameters(sPageSQL, ref dtbValue, objDPArr);
                //循环DataTable,先填充主表中的内容到obj
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {

                        objTemp = new clsRegisterQuantity_VODataInfo();
                        objMainRecordInfo = new clsRegisterQuantity_VO();
                        objMainRecordInfo.m_intRegID = Convert.ToInt32(dtbValue.Rows[j]["RegID"].ToString());
                        objMainRecordInfo.m_strCreateUserID = dtbValue.Rows[j]["CreateUserID"].ToString();
                        objMainRecordInfo.m_strCustomInComumnName = dtbValue.Rows[j]["CustomIncolumnName"].ToString();
                        objMainRecordInfo.m_strCustomOutComumnName = dtbValue.Rows[j]["CustomOutColumnName"].ToString();
                        objMainRecordInfo.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["InPatientDate"].ToString());
                        objMainRecordInfo.m_strInPatientID = dtbValue.Rows[j]["InPatientID"].ToString();
                        objMainRecordInfo.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[j]["CreateDate"].ToString());
                        objMainRecordInfo.m_dtmRegDate = Convert.ToDateTime(dtbValue.Rows[j]["regdate"].ToString());

                        objMainRecordInfo.m_strModifyUserID = dtbValue.Rows[j]["ModifyUserID"].ToString();


                        objMainRecordInfo.m_bytStatus = Convert.ToByte(dtbValue.Rows[j]["status"].ToString());
                        objMainRecordInfo.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[j]["Opendate"].ToString());
                        objMainRecordInfo.m_strInSummary = dtbValue.Rows[j]["InSummary"].ToString();
                        objMainRecordInfo.m_strInSummaryXML = dtbValue.Rows[j]["InSummaryXML"].ToString();
                        objMainRecordInfo.m_strOutSummary = dtbValue.Rows[j]["OutSummary"].ToString();
                        objMainRecordInfo.m_strOutSummaryXML = dtbValue.Rows[j]["OutSummaryXML"].ToString();
                        objMainRecordInfo.m_strOutSummaryRate = dtbValue.Rows[j]["OutSummaryRate"].ToString();
                        objMainRecordInfo.m_strOutSummaryRateXML = dtbValue.Rows[j]["OutSummaryRateXML"].ToString();
                        objMainRecordInfo.m_strOutUrineSummary = dtbValue.Rows[j]["OutUrineSummary"].ToString();
                        objMainRecordInfo.m_strOutUrineSummaryXML = dtbValue.Rows[j]["OutUrineSummaryXML"].ToString();
                        objMainRecordInfo.m_strRecordersignID = dtbValue.Rows[j]["RecordersignID"].ToString();
                        objMainRecordInfo.m_strRecordersignName = dtbValue.Rows[j]["RecordersignName"].ToString();

                        objTemp.m_objMainRecord = objMainRecordInfo;
                        //						p_objTansDataInfo[0].m_objMainRecord =;
                        //同时GET the detail record according to the regid

                        objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = Convert.ToInt32(dtbValue.Rows[j]["RegID"].ToString()); ;

                        lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_ByRegID, ref dtbValueDetail, objDPArr);
                        //循环DataTable
                        if (lngRes > 0 && dtbValueDetail.Rows.Count > 0)
                        {
                            //p_objTansDataInfo = new clsRegisterQuantity_VO[dtbValue.Rows.Count];

                            //clsRegisterQuantity_VO objRecordContent= null;
                            for (int j1 = 0; j1 < dtbValueDetail.Rows.Count; j1++)
                            {
                                //获取当前DataTable记录的OpenDate，记录在dtmOpenDate
                                objContentDetaill = new clsRegisterQuantity_VO();
                                objContentDetaill.m_intPeriodID = Convert.ToInt32(dtbValueDetail.Rows[j1]["regperiod"].ToString());
                                objContentDetaill.m_strInItem1 = dtbValueDetail.Rows[j1]["InItem1"].ToString();
                                objContentDetaill.m_strInItem1XML = dtbValueDetail.Rows[j1]["InItem1XML"].ToString();
                                objContentDetaill.m_strInItem2 = dtbValueDetail.Rows[j1]["InItem2"].ToString();
                                objContentDetaill.m_strInItem2XML = dtbValueDetail.Rows[j1]["InItem2XML"].ToString();
                                objContentDetaill.m_strInItem3 = dtbValueDetail.Rows[j1]["InItem3"].ToString();
                                objContentDetaill.m_strInItem3XML = dtbValueDetail.Rows[j1]["InItem3XML"].ToString();
                                objContentDetaill.m_strInItem4 = dtbValueDetail.Rows[j1]["InItem4"].ToString();
                                objContentDetaill.m_strInItem4XML = dtbValueDetail.Rows[j1]["InItem4XML"].ToString();
                                objContentDetaill.m_strInItem5 = dtbValueDetail.Rows[j1]["InItem5"].ToString();
                                objContentDetaill.m_strInItem5XML = dtbValueDetail.Rows[j1]["InItem5XML"].ToString();
                                objContentDetaill.m_strInItem6 = dtbValueDetail.Rows[j1]["InItem6"].ToString();
                                objContentDetaill.m_strInItem6XML = dtbValueDetail.Rows[j1]["InItem6XML"].ToString();
                                objContentDetaill.m_strInItem7 = dtbValueDetail.Rows[j1]["InItem7"].ToString();
                                objContentDetaill.m_strInItem7XML = dtbValueDetail.Rows[j1]["InItem7XML"].ToString();
                                objContentDetaill.m_strInItem8 = dtbValueDetail.Rows[j1]["InItem8"].ToString();
                                objContentDetaill.m_strInItem8XML = dtbValueDetail.Rows[j1]["InItem8XML"].ToString();

                                objContentDetaill.m_strOutItem1 = dtbValueDetail.Rows[j1]["OutItem1"].ToString();
                                objContentDetaill.m_strOutItem1XML = dtbValueDetail.Rows[j1]["OutItem1XML"].ToString();
                                objContentDetaill.m_strOutItem2 = dtbValueDetail.Rows[j1]["OutItem2"].ToString();
                                objContentDetaill.m_strOutItem2XML = dtbValueDetail.Rows[j1]["OutItem2XML"].ToString();
                                objContentDetaill.m_strOutItem3 = dtbValueDetail.Rows[j1]["OutItem3"].ToString();
                                objContentDetaill.m_strOutItem3XML = dtbValueDetail.Rows[j1]["OutItem3XML"].ToString();
                                objContentDetaill.m_strOutItem4 = dtbValueDetail.Rows[j1]["OutItem4"].ToString();
                                objContentDetaill.m_strOutItem4XML = dtbValueDetail.Rows[j1]["OutItem4XML"].ToString();
                                objContentDetaill.m_strOutItem5 = dtbValueDetail.Rows[j1]["OutItem5"].ToString();
                                objContentDetaill.m_strOutItem5XML = dtbValueDetail.Rows[j1]["OutItem5XML"].ToString();
                                objContentDetaill.m_strOutItem6 = dtbValueDetail.Rows[j1]["OutItem6"].ToString();
                                objContentDetaill.m_strOutItem6XML = dtbValueDetail.Rows[j1]["OutItem6XML"].ToString();
                                objContentDetaill.m_strOutItem7 = dtbValueDetail.Rows[j1]["OutItem7"].ToString();
                                objContentDetaill.m_strOutItem7XML = dtbValueDetail.Rows[j1]["OutItem7XML"].ToString();
                                objContentDetaill.m_strOutItem8 = dtbValueDetail.Rows[j1]["OutItem8"].ToString();
                                objContentDetaill.m_strOutItem8XML = dtbValueDetail.Rows[j1]["OutItem8XML"].ToString();
                                arlModifyData.Add(objContentDetaill);

                            }

                            objTemp.m_objRecordArr = (clsRegisterQuantity_VO[])arlModifyData.ToArray(typeof(clsRegisterQuantity_VO));
                            p_objTansDataInfo[j] = objTemp;
                            objTemp = null;
                            arlModifyData.Clear();
                        }

                    }
                }
                //填充次表的内容到obj,关键值从主表所填充的对象取出
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            } 
            return (long)enmOperationResult.DB_Succeed;

		}
		#endregion 
		#region 得到病人的所有打印记录
		/// <summary>
		/// 得到此病人的所有打印内容
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strRecordOpenDate"></param>
		/// <param name="p_objTansDataInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPrintInfo(clsRegisterQuantity_VO objContent,	
			out clsRegisterQuantity_VODataInfo[] p_objTansDataInfo)
		{
           long lngRes=0;
		   long	iRecordCount=0;
		   string sPageSQL=string.Empty;
			p_objTansDataInfo=null;


            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
 
                ArrayList arlModifyData = new ArrayList();
                DataTable dtbValue = new DataTable();
                DataTable dtbValueDetail = new DataTable();

                IDataParameter[] objDPArr = null;
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;

                clsRegisterQuantity_VO objMainRecordInfo;
                clsRegisterQuantity_VODataInfo objTemp = new clsRegisterQuantity_VODataInfo();
                clsRegisterQuantity_VO objContentDetaill = new clsRegisterQuantity_VO();


                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetAllMainContentSQL, ref dtbValue, objDPArr);
                //循环DataTable
                if (lngRes > 0)
                    iRecordCount = dtbValue.Rows.Count;
                else
                    return (long)enmOperationResult.DB_Fail;
                //没有记录返回
                sPageSQL = @"select inpatientid,
       inpatientdate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       lastprintdate,
       firstprintdate,
       opendate,
       regdate,
       customoutcolumnname,
       customincolumnname,
       outsummary,
       outsummaryxml,
       outsummaryrate,
       outsummaryratexml,
       outurinesummary,
       outurinesummaryxml,
       insummary,
       insummaryxml,
       recordersignid,
       recordersignname,
       regid,
       modifydate,
       modifyuserid
  from t_emr_registerquantity
 where inpatientid = ?
   and inpatientdate = ?
   and status = 0
 order by regdate";

                p_objTansDataInfo = new clsRegisterQuantity_VODataInfo[iRecordCount];

                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;

                lngRes = objHRPServ.lngGetDataTableWithParameters(sPageSQL, ref dtbValue, objDPArr);
                //循环DataTable,先填充主表中的内容到obj
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {

                        objTemp = new clsRegisterQuantity_VODataInfo();
                        objMainRecordInfo = new clsRegisterQuantity_VO();
                        objMainRecordInfo.m_intRegID = Convert.ToInt32(dtbValue.Rows[j]["RegID"].ToString());
                        objMainRecordInfo.m_strCreateUserID = dtbValue.Rows[j]["CreateUserID"].ToString();
                        objMainRecordInfo.m_strCustomInComumnName = dtbValue.Rows[j]["CustomIncolumnName"].ToString();
                        objMainRecordInfo.m_strCustomOutComumnName = dtbValue.Rows[j]["CustomOutColumnName"].ToString();
                        objMainRecordInfo.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[j]["InPatientDate"].ToString());
                        objMainRecordInfo.m_strInPatientID = dtbValue.Rows[j]["InPatientID"].ToString();
                        objMainRecordInfo.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[j]["CreateDate"].ToString());
                        objMainRecordInfo.m_dtmRegDate = Convert.ToDateTime(dtbValue.Rows[j]["regdate"].ToString());
                        objMainRecordInfo.m_bytStatus = Convert.ToByte(dtbValue.Rows[j]["status"].ToString());
                        objMainRecordInfo.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[j]["Opendate"].ToString());
                        objMainRecordInfo.m_strInSummary = dtbValue.Rows[j]["InSummary"].ToString();
                        objMainRecordInfo.m_strInSummaryXML = dtbValue.Rows[j]["InSummaryXML"].ToString();
                        objMainRecordInfo.m_strOutSummary = dtbValue.Rows[j]["OutSummary"].ToString();
                        objMainRecordInfo.m_strOutSummaryXML = dtbValue.Rows[j]["OutSummaryXML"].ToString();
                        objMainRecordInfo.m_strOutSummaryRate = dtbValue.Rows[j]["OutSummaryRate"].ToString();
                        objMainRecordInfo.m_strOutSummaryRateXML = dtbValue.Rows[j]["OutSummaryRateXML"].ToString();
                        objMainRecordInfo.m_strOutUrineSummary = dtbValue.Rows[j]["OutUrineSummary"].ToString();
                        objMainRecordInfo.m_strOutUrineSummaryXML = dtbValue.Rows[j]["OutUrineSummaryXML"].ToString();
                        objMainRecordInfo.m_strRecordersignID = dtbValue.Rows[j]["RecordersignID"].ToString();
                        objMainRecordInfo.m_strRecordersignName = dtbValue.Rows[j]["RecordersignName"].ToString();

                        objTemp.m_objMainRecord = objMainRecordInfo;
                        //						p_objTansDataInfo[0].m_objMainRecord =;
                        //同时GET the detail record according to the regid

                        objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = Convert.ToInt32(dtbValue.Rows[j]["RegID"].ToString()); ;

                        lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_ByRegID, ref dtbValueDetail, objDPArr);
                        //循环DataTable
                        if (lngRes > 0 && dtbValueDetail.Rows.Count > 0)
                        {
                            //p_objTansDataInfo = new clsRegisterQuantity_VO[dtbValue.Rows.Count];

                            //clsRegisterQuantity_VO objRecordContent= null;
                            for (int j1 = 0; j1 < dtbValueDetail.Rows.Count; j1++)
                            {
                                //获取当前DataTable记录的OpenDate，记录在dtmOpenDate
                                objContentDetaill = new clsRegisterQuantity_VO();
                                objContentDetaill.m_intPeriodID = Convert.ToInt32(dtbValueDetail.Rows[j1]["regperiod"].ToString());
                                objContentDetaill.m_strInItem1 = dtbValueDetail.Rows[j1]["InItem1"].ToString();
                                objContentDetaill.m_strInItem1XML = dtbValueDetail.Rows[j1]["InItem1XML"].ToString();
                                objContentDetaill.m_strInItem2 = dtbValueDetail.Rows[j1]["InItem2"].ToString();
                                objContentDetaill.m_strInItem2XML = dtbValueDetail.Rows[j1]["InItem2XML"].ToString();
                                objContentDetaill.m_strInItem3 = dtbValueDetail.Rows[j1]["InItem3"].ToString();
                                objContentDetaill.m_strInItem3XML = dtbValueDetail.Rows[j1]["InItem3XML"].ToString();
                                objContentDetaill.m_strInItem4 = dtbValueDetail.Rows[j1]["InItem4"].ToString();
                                objContentDetaill.m_strInItem4XML = dtbValueDetail.Rows[j1]["InItem4XML"].ToString();
                                objContentDetaill.m_strInItem5 = dtbValueDetail.Rows[j1]["InItem5"].ToString();
                                objContentDetaill.m_strInItem5XML = dtbValueDetail.Rows[j1]["InItem5XML"].ToString();
                                objContentDetaill.m_strInItem6 = dtbValueDetail.Rows[j1]["InItem6"].ToString();
                                objContentDetaill.m_strInItem6XML = dtbValueDetail.Rows[j1]["InItem6XML"].ToString();
                                objContentDetaill.m_strInItem7 = dtbValueDetail.Rows[j1]["InItem7"].ToString();
                                objContentDetaill.m_strInItem7XML = dtbValueDetail.Rows[j1]["InItem7XML"].ToString();
                                objContentDetaill.m_strInItem8 = dtbValueDetail.Rows[j1]["InItem8"].ToString();
                                objContentDetaill.m_strInItem8XML = dtbValueDetail.Rows[j1]["InItem8XML"].ToString();

                                objContentDetaill.m_strOutItem1 = dtbValueDetail.Rows[j1]["OutItem1"].ToString();
                                objContentDetaill.m_strOutItem1XML = dtbValueDetail.Rows[j1]["OutItem1XML"].ToString();
                                objContentDetaill.m_strOutItem2 = dtbValueDetail.Rows[j1]["OutItem2"].ToString();
                                objContentDetaill.m_strOutItem2XML = dtbValueDetail.Rows[j1]["OutItem2XML"].ToString();
                                objContentDetaill.m_strOutItem3 = dtbValueDetail.Rows[j1]["OutItem3"].ToString();
                                objContentDetaill.m_strOutItem3XML = dtbValueDetail.Rows[j1]["OutItem3XML"].ToString();
                                objContentDetaill.m_strOutItem4 = dtbValueDetail.Rows[j1]["OutItem4"].ToString();
                                objContentDetaill.m_strOutItem4XML = dtbValueDetail.Rows[j1]["OutItem4XML"].ToString();
                                objContentDetaill.m_strOutItem5 = dtbValueDetail.Rows[j1]["OutItem5"].ToString();
                                objContentDetaill.m_strOutItem5XML = dtbValueDetail.Rows[j1]["OutItem5XML"].ToString();
                                objContentDetaill.m_strOutItem6 = dtbValueDetail.Rows[j1]["OutItem6"].ToString();
                                objContentDetaill.m_strOutItem6XML = dtbValueDetail.Rows[j1]["OutItem6XML"].ToString();
                                objContentDetaill.m_strOutItem7 = dtbValueDetail.Rows[j1]["OutItem7"].ToString();
                                objContentDetaill.m_strOutItem7XML = dtbValueDetail.Rows[j1]["OutItem7XML"].ToString();
                                objContentDetaill.m_strOutItem8 = dtbValueDetail.Rows[j1]["OutItem8"].ToString();
                                objContentDetaill.m_strOutItem8XML = dtbValueDetail.Rows[j1]["OutItem8XML"].ToString();
                                arlModifyData.Add(objContentDetaill);

                            }

                            objTemp.m_objRecordArr = (clsRegisterQuantity_VO[])arlModifyData.ToArray(typeof(clsRegisterQuantity_VO));
                            p_objTansDataInfo[j] = objTemp;
                            objTemp = null;
                            arlModifyData.Clear();
                        }

                    }
                }
                //填充次表的内容到obj,关键值从主表所填充的对象取出
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return (long)enmOperationResult.DB_Succeed;

		}
		#endregion 
		// 更新数据库中的首次打印时间。
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRegisterQuantityService", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			


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
		// 更新数据库中的首次打印时间。
		[AutoComplete] 
		public  long m_lngUptFirstPrintDate(string p_strInPatientID,string p_strInPatientDate)
		{
			DataTable dtbValue = new DataTable();
			string c_strUptPrintDate;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null)
                    return (long)enmOperationResult.Parameter_Error;


                //执行查询，填充结果到DataTable       



                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetFirstPrintDate, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    if (dtbValue.Rows[0]["FirstPrintDate"].ToString().Length != 0)
                    {
                        objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[1].Value = p_strInPatientID;
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);


                        c_strUptPrintDate = @" update t_emr_registerquantity set lastprintdate=? where inpatientid=? and inpatientdate = ?  and status = 0";
                    }
                    else
                    {
                        objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[2].Value = p_strInPatientID;
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);

                        c_strUptPrintDate = @" update t_emr_registerquantity set firstprintdate=?,lastprintdate=? where inpatientid=? and inpatientdate = ?  and status = 0";
                    }
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strUptPrintDate, ref lngEff, objDPArr);

                }
                objHRPServ = null;

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
            //返回

			return lngRes;

			
		}


		// 获取病人的已经被删除记录时间列表。
		[AutoComplete] 
		public override long m_lngGetDeleteRecordTimeList(
			string p_strInPatientID,
			string p_strInPatientDate,
			string p_strDeleteUserID,
			out string[] p_strCreateRecordTimeArr,
			out string[] p_strOpenRecordTimeArr)
		{
			p_strCreateRecordTimeArr=null;
			p_strOpenRecordTimeArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRegisterQuantityService", "m_lngGetDeleteRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                lngRes = (long)enmOperationResult.DB_Succeed;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}

		// 获取病人的已经被删除记录时间列表。
		[AutoComplete] 
		public override long m_lngGetDeleteRecordTimeListAll(
			string p_strInPatientID,
			string p_strInPatientDate,
			out string[] p_strCreateRecordTimeArr,
			out string[] p_strOpenRecordTimeArr)
		{
			p_strCreateRecordTimeArr=null;
			p_strOpenRecordTimeArr=null;
			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsRegisterQuantityService","m_lngGetDeleteRecordTimeListAll");
				//if(lngCheckRes <= 0)
					//return lngCheckRes;	

				lngRes= (long)enmOperationResult.DB_Succeed ;
				
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;

		}

		// 获取指定记录的内容。
		[AutoComplete] 
		protected override long m_lngGetRecordContentWithServ(
			string p_strInPatientID,
			string p_strInPatientDate,
			/* string p_strOpenRecordTime, */
			clsHRPTableService p_objHRPServ,
			out clsBaseCaseHistoryInfo p_objRecordContent,
			out clsPictureBoxValue[] p_objPicValueArr)
		{
			p_objRecordContent=null;
			p_objPicValueArr = null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //			objDPArr[2].Value=DateTime.Parse(p_strOpenRecordTime);

                //			clsInPatientCaseHistoryContent p_objContent =new clsInPatientCaseHistoryContent() ;	
                //生成DataTable
                DataTable dtbValue = new DataTable();

                string strSql = @"select a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.lastprintdate,
       a.firstprintdate,
       a.opendate,
       a.regdate,
       a.customoutcolumnname,
       a.customincolumnname,
       a.outsummary,
       a.outsummaryxml,
       a.outsummaryrate,
       a.outsummaryratexml,
       a.outurinesummary,
       a.outurinesummaryxml,
       a.insummary,
       a.insummaryxml,
       a.recordersignid,
       a.recordersignname,
       a.regid,
       a.modifydate,
       a.modifyuserid,
       b.lastmodifydate,
       b.lastmodifyuserid,
       b.deactiveddate,
       b.deactivedoperatorid,
       b.status,
       b.maindescription,
       b.currentstatus,
       b.beforetimestatus,
       b.ownhistory,
       b.marriagehistory,
       b.catameniahistory,
       b.familyhistory,
       b.summary,
       b.temperature,
       b.pulse,
       b.breath,
       b.sys,
       b.dia,
       b.bloodpressureunit,
       b.medical,
       b.professionalcheck,
       b.labcheck,
       b.firstcatamenia,
       b.catamenialasttime,
       b.catameniacycle,
       b.lastcatameniatime,
       b.catameniacase,
       b.yjs,
       b.contrahistory,
       b.shys,
       b.lcqk,
       b.cqjc,
       b.pregtimes,
       b.borntimes,
       b.careplan,
       b.chargedoctor,
       b.diretdoctor,
       b.oldmaternitysuffer,
       b.midwife,
       b.modifydiagnose,
       b.adddiagnose,
       b.diagnosenor,
       pbi.lastname_vchr
  from t_emr_registerquantity       a,
       ipcasehistory_historycontent b,
       t_bse_employee               pbi
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and a.createuserid = pbi.empno_chr
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.lastmodifydate = (select max(lastmodifydate)
                             from ipcasehistory_historycontent
                            where inpatientid = a.inpatientid
                              and inpatientdate = a.inpatientdate
                              and opendate = a.opendate)";

                //执行查询，填充结果到DataTable
                //			long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL,ref dtbValue,objDPArr);
                
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsInPatientCaseHistoryContent p_objContent = new clsInPatientCaseHistoryContent();

                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_objContent.m_bytIfConfirm = byte.Parse(dtbValue.Rows[i]["IFCONFIRM"].ToString());
                        p_objContent.m_bytStatus = byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        p_objContent.m_dtmCreateDate = (dtbValue.Rows[i]["CREATEDATE"].ToString() != null ? DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()) : DateTime.MinValue);

                        p_objContent.m_dtmDeActivedDate = (dtbValue.Rows[i]["DEACTIVEDDATE"].ToString() != "" ? DateTime.Parse(dtbValue.Rows[i]["DEACTIVEDDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_dtmFirstPrintDate = (dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString() != "" ? DateTime.Parse(dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_dtmInPatientDate = (dtbValue.Rows[i]["INPATIENTDATE"].ToString() != "" ? DateTime.Parse(dtbValue.Rows[i]["INPATIENTDATE"].ToString()) : DateTime.MinValue);

                        p_objContent.m_dtmModifyDate = (dtbValue.Rows[i]["LASTMODIFYDATE"].ToString() != "" ? DateTime.Parse(dtbValue.Rows[i]["LASTMODIFYDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_dtmOpenDate = (dtbValue.Rows[i]["OPENDATE"].ToString() != "" ? DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_strBeforetimeStatus = dtbValue.Rows[i]["BEFORETIMESTATUS"].ToString();

                        p_objContent.m_strBeforetimeStatusAll = dtbValue.Rows[i]["BEFORETIMESTATUSALL"].ToString();
                        p_objContent.m_strBeforetimeStatusXML = dtbValue.Rows[i]["BEFORETIMESTATUSXML"].ToString();
                        p_objContent.m_strBloodPressureUnit = dtbValue.Rows[i]["BLOODPRESSUREUNIT"].ToString();

                        p_objContent.m_strBloodPressureUnitAll = dtbValue.Rows[i]["BLOODPRESSUREUNITALL"].ToString();
                        p_objContent.m_strBloodPressureUnitXML = dtbValue.Rows[i]["BLOODPRESSUREUNITXML"].ToString();
                        p_objContent.m_strBreath = dtbValue.Rows[i]["BREATH"].ToString();

                        p_objContent.m_strBreathAll = dtbValue.Rows[i]["BREATHALL"].ToString();
                        p_objContent.m_strBreathXML = dtbValue.Rows[i]["BREATHXML"].ToString();
                        p_objContent.m_strConfirmReason = dtbValue.Rows[i]["CONFIRMREASON"].ToString();

                        p_objContent.m_strConfirmReasonXML = dtbValue.Rows[i]["CONFIRMREASONXML"].ToString();
                        p_objContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
                        p_objContent.m_strCreateName = dtbValue.Rows[i]["LASTNAME_VCHR"].ToString();
                        p_objContent.m_strCredibility = dtbValue.Rows[i]["CREDIBILITY"].ToString();

                        p_objContent.m_strCurrentStatus = dtbValue.Rows[i]["CURRENTSTATUS"].ToString();
                        p_objContent.m_strCurrentStatusXAll = dtbValue.Rows[i]["CURRENTSTATUSALL"].ToString();
                        p_objContent.m_strCurrentStatusXML = dtbValue.Rows[i]["CURRENTSTATUSXML"].ToString();

                        p_objContent.m_strCatameniaHistory = dtbValue.Rows[i]["CATAMENIAHISTORY"].ToString();
                        p_objContent.m_strCatameniaHistoryAll = dtbValue.Rows[i]["CATAMENIAHISTORYALL"].ToString();
                        p_objContent.m_strCatameniaHistoryXML = dtbValue.Rows[i]["CATAMENIAHISTORYXML"].ToString();

                        p_objContent.m_strDeActivedOperatorID = dtbValue.Rows[i]["DEACTIVEDOPERATORID"].ToString();

                        p_objContent.m_strDia = dtbValue.Rows[i]["DIA"].ToString();
                        p_objContent.m_strDiaAll = dtbValue.Rows[i]["DIAALL"].ToString();
                        p_objContent.m_strDiaXML = dtbValue.Rows[i]["DIAXML"].ToString();

                        p_objContent.m_strFamilyHistory = dtbValue.Rows[i]["FAMILYHISTORY"].ToString();
                        p_objContent.m_strFamilyHistoryAll = dtbValue.Rows[i]["FAMILYHISTORYALL"].ToString();
                        p_objContent.m_strFamilyHistoryXML = dtbValue.Rows[i]["FAMILYHISTORYXML"].ToString();

                        //p_objContent.m_strFinallyDiagnose=dtbValue.Rows[i]["FINALLYDIAGNOSE"].ToString() ;
                        p_objContent.m_strFinallyDiagnoseAll = dtbValue.Rows[i]["FINALLYDIAGNOSEALL"].ToString();
                        p_objContent.m_strFinallyDiagnoseXML = dtbValue.Rows[i]["FINALLYDIAGNOSEXML"].ToString();

                        p_objContent.m_strFinallyDiagnoseDate = dtbValue.Rows[i]["FINALLYDIAGNOSEDATE"].ToString();
                        p_objContent.m_strFinallyDiagnoseDocID = dtbValue.Rows[i]["FINALLYDIAGNOSEDOCID"].ToString();
                        p_objContent.m_strInPatientID = dtbValue.Rows[i]["INPATIENTID"].ToString();

                        p_objContent.m_strLabCheckAll = dtbValue.Rows[i]["LABCHECKALL"].ToString();
                        p_objContent.m_strLabCheckXML = dtbValue.Rows[i]["LABCHECKXML"].ToString();
                        p_objContent.m_strLabCheck = dtbValue.Rows[i]["LABCHECK"].ToString();

                        p_objContent.m_strMainDescription = dtbValue.Rows[i]["MAINDESCRIPTION"].ToString();
                        p_objContent.m_strMainDescriptionAll = dtbValue.Rows[i]["MAINDESCRIPTIONALL"].ToString();
                        p_objContent.m_strMainDescriptionXML = dtbValue.Rows[i]["MAINDESCRIPTIONXML"].ToString();

                        p_objContent.m_strMarriageHistory = dtbValue.Rows[i]["MARRIAGEHISTORY"].ToString();
                        p_objContent.m_strMarriageHistoryAll = dtbValue.Rows[i]["MARRIAGEHISTORYALL"].ToString();
                        p_objContent.m_strMarriageHistoryXML = dtbValue.Rows[i]["MARRIAGEHISTORYXML"].ToString();

                        p_objContent.m_strMedical = dtbValue.Rows[i]["MEDICAL"].ToString();
                        p_objContent.m_strMedicalAll = dtbValue.Rows[i]["MEDICALALL"].ToString();
                        p_objContent.m_strMedicalXML = dtbValue.Rows[i]["MEDICALXML"].ToString();

                        p_objContent.m_strModifyUserID = dtbValue.Rows[i]["LASTMODIFYUSERID"].ToString();
                        p_objContent.m_strOwnHistory = dtbValue.Rows[i]["OWNHISTORY"].ToString();
                        p_objContent.m_strOwnHistoryAll = dtbValue.Rows[i]["OWNHISTORYALL"].ToString();

                        p_objContent.m_strOwnHistoryXML = dtbValue.Rows[i]["OWNHISTORYXML"].ToString();
                        //p_objContent.m_strPrimaryDiagnose=dtbValue.Rows[i]["PRIMARYDIAGNOSE"].ToString() ;
                        p_objContent.m_strPrimaryDiagnoseAll = dtbValue.Rows[i]["PRIMARYDIAGNOSEALL"].ToString();

                        p_objContent.m_strPrimaryDiagnoseDate = dtbValue.Rows[i]["PRIMARYDIAGNOSEDATE"].ToString();
                        p_objContent.m_strPrimaryDiagnoseDocID = dtbValue.Rows[i]["PRIMARYDIAGNOSEDOCID"].ToString();
                        p_objContent.m_strPrimaryDiagnoseXML = dtbValue.Rows[i]["PRIMARYDIAGNOSEXML"].ToString();

                        p_objContent.m_strProfessionalCheck = dtbValue.Rows[i]["PROFESSIONALCHECK"].ToString();
                        p_objContent.m_strProfessionalCheckAll = dtbValue.Rows[i]["PROFESSIONALCHECKALL"].ToString();
                        p_objContent.m_strProfessionalCheckXML = dtbValue.Rows[i]["PROFESSIONALCHECKXML"].ToString();

                        p_objContent.m_strPulse = dtbValue.Rows[i]["PULSE"].ToString();
                        p_objContent.m_strPulseAll = dtbValue.Rows[i]["PULSEALL"].ToString();
                        p_objContent.m_strPulseXML = dtbValue.Rows[i]["PULSEXML"].ToString();

                        p_objContent.m_strRepresentor = dtbValue.Rows[i]["REPRESENTOR"].ToString();
                        p_objContent.m_strSummary = dtbValue.Rows[i]["SUMMARY"].ToString();
                        p_objContent.m_strSummaryAll = dtbValue.Rows[i]["SUMMARYALL"].ToString();

                        p_objContent.m_strSummaryXML = dtbValue.Rows[i]["SUMMARYXML"].ToString();
                        p_objContent.m_strSys = dtbValue.Rows[i]["SYS"].ToString();
                        p_objContent.m_strSysAll = dtbValue.Rows[i]["SYSALL"].ToString();

                        p_objContent.m_strSysXML = dtbValue.Rows[i]["SYSXML"].ToString();
                        p_objContent.m_strTemperature = dtbValue.Rows[i]["TEMPERATURE"].ToString();
                        p_objContent.m_strTemperatureAll = dtbValue.Rows[i]["TEMPERATUREALL"].ToString();

                        p_objContent.m_strTemperatureXML = dtbValue.Rows[i]["TEMPERATUREXML"].ToString();
                        p_objContent.m_strFirstCatamenia = dtbValue.Rows[i]["FIRSTCATAMENIA"].ToString();
                        p_objContent.m_strCatameniaLastTime = dtbValue.Rows[i]["CATAMENIALASTTIME"].ToString();
                        p_objContent.m_strCatameniaCycle = dtbValue.Rows[i]["CATAMENIACYCLE"].ToString();
                        try
                        {
                            p_objContent.m_dtmLastCatameniaTime = DateTime.Parse(dtbValue.Rows[i]["LASTCATAMENIATIME"].ToString());
                        }
                        catch
                        {
                            p_objContent.m_dtmLastCatameniaTime = DateTime.MinValue;
                        }
                        p_objContent.m_strCatameniaCase = dtbValue.Rows[i]["CATAMENIACASE"].ToString();

                        p_objContent.m_strYJS = dtbValue.Rows[i]["YJS"].ToString();
                        p_objContent.m_strYJSAll = dtbValue.Rows[i]["YJSALL"].ToString();
                        p_objContent.m_strYJSXML = dtbValue.Rows[i]["YJSXML"].ToString();

                        p_objContent.m_strContraHistory = dtbValue.Rows[i]["CONTRAHISTORY"].ToString();
                        p_objContent.m_strContraHistoryAll = dtbValue.Rows[i]["CONTRAHISTORYALL"].ToString();
                        p_objContent.m_strContraHistoryXML = dtbValue.Rows[i]["CONTRAHISTORYXML"].ToString();

                        p_objContent.m_strShYS = dtbValue.Rows[i]["SHYS"].ToString();
                        p_objContent.m_strShYSAll = dtbValue.Rows[i]["SHYSALL"].ToString();
                        p_objContent.m_strShYSXML = dtbValue.Rows[i]["SHYSXML"].ToString();

                        p_objContent.m_strLCQK = dtbValue.Rows[i]["LCQK"].ToString();
                        p_objContent.m_strLCQKAll = dtbValue.Rows[i]["LCQKALL"].ToString();
                        p_objContent.m_strLCQKXML = dtbValue.Rows[i]["LCQKXML"].ToString();

                        p_objContent.m_strCQJC = dtbValue.Rows[i]["CQJC"].ToString();
                        p_objContent.m_strCQJCAll = dtbValue.Rows[i]["CQJCALL"].ToString();
                        p_objContent.m_strCQJCXML = dtbValue.Rows[i]["CQJCXML"].ToString();

                        p_objContent.m_strCarePlan = dtbValue.Rows[i]["CAREPLAN"].ToString();
                        p_objContent.m_strCarePlanAll = dtbValue.Rows[i]["CAREPLANALL"].ToString();
                        p_objContent.m_strCarePlanXML = dtbValue.Rows[i]["CAREPLANXML"].ToString();

                        p_objContent.m_strPregTimes = dtbValue.Rows[i]["PREGTIMES"].ToString();
                        p_objContent.m_strBornTimes = dtbValue.Rows[i]["BORNTIMES"].ToString();
                        p_objContent.m_strChargeDoctor = dtbValue.Rows[i]["CHARGEDOCTOR"].ToString();
                        p_objContent.m_strDiretDoctor = dtbValue.Rows[i]["DIRETDOCTOR"].ToString();
                        p_objContent.m_strMidWife = dtbValue.Rows[i]["MIDWIFE"].ToString();

                        p_objContent.m_strOldMaternitySuffer = dtbValue.Rows[i]["OLDMATERNITYSUFFER"].ToString();
                        p_objContent.m_strOldMaternitySufferAll = dtbValue.Rows[i]["OLDMATERNITYSUFFERALL"].ToString();
                        p_objContent.m_strOldMaternitySufferXML = dtbValue.Rows[i]["OLDMATERNITYSUFFERXML"].ToString();

                        //修正诊断
                        p_objContent.m_strModifyDiagnose = dtbValue.Rows[i]["MODIFYDIAGNOSE"].ToString();
                        p_objContent.m_strModifyDiagnoseAll = dtbValue.Rows[i]["MODIFYDIAGNOSEALL"].ToString();
                        p_objContent.m_strModifyDiagnoseXML = dtbValue.Rows[i]["MODIFYDIAGNOSEXML"].ToString();
                        p_objContent.m_strModifyDiagnoseDoctorID = dtbValue.Rows[i]["MODIFYDIAGNOSEDOCTORID"].ToString();
                        try
                        {
                            p_objContent.m_datModifyDiagnose = DateTime.Parse(dtbValue.Rows[i]["MODIFYDIAGNOSEDATE"].ToString());
                        }
                        catch
                        {
                            p_objContent.m_datModifyDiagnose = DateTime.Now;

                        }

                        //补充诊断
                        p_objContent.m_strAddDiagnose = dtbValue.Rows[i]["ADDDIAGNOSE"].ToString();
                        p_objContent.m_strAddDiagnoseALL = dtbValue.Rows[i]["ADDDIAGNOSEALL"].ToString();
                        p_objContent.m_strAddDiagnoseXML = dtbValue.Rows[i]["ADDDIAGNOSEXML"].ToString();
                        p_objContent.m_strAddDiagnoseDoctorID = dtbValue.Rows[i]["ADDDIAGNOSEDOCTORID"].ToString();
                        try
                        {
                            p_objContent.m_datAddDiagnose = DateTime.Parse(dtbValue.Rows[i]["ADDDIAGNOSEDATE"].ToString());

                        }
                        catch
                        {
                            p_objContent.m_datAddDiagnose = DateTime.Now;

                        }

                    }
                    p_objRecordContent = p_objContent;
                }

                #region 读取画图信息
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr1);
                objDPArr1[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = DateTime.Parse(p_strInPatientDate);

                //生成DataTable
                DataTable dtbValue1 = new DataTable();

                //执行查询，填充结果到DataTable
                long lngRes1 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContent_PictureSQL, ref dtbValue1, objDPArr1);

                ArrayList arlPic = new ArrayList();

                //从DataTable.Rows中获取结果
                if (lngRes1 > 0 && dtbValue1.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbValue1.Rows.Count; i++)
                    {
                        clsPictureBoxValue objPicValue = new clsPictureBoxValue();
                        //					objPicValue.m_imgBack = m_imgBinaryToImage(dtbValue1.Rows[i]["BACKIMAGE"]) ;
                        //					objPicValue.m_imgFront = m_imgBinaryToImage(dtbValue1.Rows[i]["FRONTIMAGE"]);
                        objPicValue.m_bytImage = (byte[])(dtbValue1.Rows[i]["FRONTIMAGE"]);
                        //						objPicValue.clrBack = dtbValue.Rows[i]["BACKCOLOR"];
                        objPicValue.intWidth = Convert.ToInt32(dtbValue1.Rows[i]["WIDTH"]);
                        objPicValue.intHeight = Convert.ToInt32(dtbValue1.Rows[i]["HEIGHT"]);
                        objPicValue.clrBack = Color.FromArgb(Convert.ToInt32(dtbValue1.Rows[i]["BACKCOLOR"]));
                        arlPic.Add(objPicValue);
                    }
                }
                p_objPicValueArr = (clsPictureBoxValue[])arlPic.ToArray(typeof(clsPictureBoxValue));
                arlPic.Clear();

                #endregion


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
			//返回
			return lngRes;

		}

		/// <summary>
		/// 出院病人随访复诊提醒获取出院记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strDiagnose"></param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetRecordFromRevisit(string p_strInPatientID,
			string p_strInPatientDate,out string p_strDiagnose)
		{
			p_strDiagnose = "";
 			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;


			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
 		    			//获取IDataParameter数组
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
			//按顺序给IDataParameter赋值
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
			objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
			//生成DataTable
			DataTable dtbValue = new DataTable();
			//执行查询，填充结果到DataTable
            lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetContentSQL_FromRevisit, ref dtbValue, objDPArr);
			
			if(lngRes > 0 && dtbValue.Rows.Count ==1)
			{
				p_strDiagnose = dtbValue.Rows[0]["PrimaryDiagnoseAll"].ToString();
			}

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;			 
		}
		/// <summary>
		/// 保存主表记录到数据库。添加主表,添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewRecord2DB(clsRegisterQuantity_VODataInfo p_objRecordContent,int p_intRegID)
		{
			
			 
 			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_objMainRecord.m_strInPatientID==null )
				return (long)enmOperationResult.Parameter_Error;
			long m_lngRes = 0;
			long lngEff=0;
			long m_lngEMR_SEQ=0;

			clsRegisterQuantity_VO objRecordContent = (clsRegisterQuantity_VO)p_objRecordContent.m_objMainRecord ;
			clsRegisterQuantity_VO[] objRecordContentDetail =(clsRegisterQuantity_VO[]) p_objRecordContent.m_objRecordArr ;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //当新加表时，要把相同的记录标记为“修改”
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = objRecordContent.m_strModifyUserID;
                objDPArr[2].Value = objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = objRecordContent.m_dtmRegDate;
                m_lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (m_lngRes < 1) return (long)enmOperationResult.DB_Fail;
                //same id
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = objRecordContent.m_strModifyUserID;
                objDPArr[2].Value = p_intRegID;

                m_lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL2, ref lngEff, objDPArr);
                if (m_lngRes < 1) return (long)enmOperationResult.DB_Fail;



                //Get the seq id from Oracle
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                long lngRes = objSign.m_lngGetSequenceValue("seq_emr", out lngSequence);
                m_lngEMR_SEQ = lngSequence;
                //string strGetSeq = @"select seq_emr.nextval from dual";
                //DataTable dtbResult = new DataTable();
                //m_lngRes = objHRPServ.DoGetDataTable(strGetSeq, ref dtbResult);
                //if (m_lngRes > 0 && dtbResult.Rows.Count > 0)
                //    m_lngEMR_SEQ = Convert.ToInt64(dtbResult.Rows[0][0]);

                //Add one record to main table
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(23, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[4].Value = objRecordContent.m_strCreateUserID;
                objDPArr[5].Value = 0;
                objDPArr[6].Value = m_lngEMR_SEQ;
                objDPArr[7].Value = objRecordContent.m_strRecordersignID;
                objDPArr[8].Value = objRecordContent.m_strRecordersignName;
                objDPArr[9].Value = objRecordContent.m_strInSummary;
                objDPArr[10].Value = objRecordContent.m_strInSummaryXML;
                objDPArr[11].Value = objRecordContent.m_strCustomInComumnName;
                objDPArr[12].Value = objRecordContent.m_strCustomOutComumnName;
                objDPArr[13].Value = objRecordContent.m_strOutSummary;
                objDPArr[14].Value = objRecordContent.m_strOutSummaryXML;
                objDPArr[15].Value = objRecordContent.m_strOutUrineSummary;
                objDPArr[16].Value = objRecordContent.m_strOutUrineSummaryXML;
                objDPArr[17].Value = objRecordContent.m_strOutSummaryRate;
                objDPArr[18].Value = objRecordContent.m_strOutSummaryRateXML;
                objDPArr[19].DbType = DbType.DateTime;
                objDPArr[19].Value = objRecordContent.m_dtmRegDate;
                objDPArr[20].Value = objRecordContent.m_strModifyUserID;
                objDPArr[21].DbType = DbType.DateTime;
                objDPArr[21].Value = objRecordContent.m_dtmModifyDate;
                objDPArr[22].Value = objRecordContent.m_strModifyUserName;



                m_lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);

                //Add some records to child table
                IDataParameter[] objDPArr2 = null;

                for (int i1 = 0; i1 < objRecordContentDetail.Length; i1++)
                {
                    objDPArr2 = null;
                    objHRPServ.CreateDatabaseParameter(35, out objDPArr2);
                    objDPArr2[0].Value = m_lngEMR_SEQ;
                    objDPArr2[1].Value = objRecordContentDetail[i1].m_intPeriodID;
                    objDPArr2[2].Value = objRecordContentDetail[i1].m_strInItem1;
                    objDPArr2[3].Value = objRecordContentDetail[i1].m_strInItem1XML;
                    objDPArr2[4].Value = objRecordContentDetail[i1].m_strInItem2;
                    objDPArr2[5].Value = objRecordContentDetail[i1].m_strInItem2XML;
                    objDPArr2[6].Value = objRecordContentDetail[i1].m_strInItem3;
                    objDPArr2[7].Value = objRecordContentDetail[i1].m_strInItem3XML;
                    objDPArr2[8].Value = objRecordContentDetail[i1].m_strInItem4;
                    objDPArr2[9].Value = objRecordContentDetail[i1].m_strInItem4XML;
                    objDPArr2[10].Value = objRecordContentDetail[i1].m_strInItem5;
                    objDPArr2[11].Value = objRecordContentDetail[i1].m_strInItem5XML;
                    objDPArr2[12].Value = objRecordContentDetail[i1].m_strInItem6;
                    objDPArr2[13].Value = objRecordContentDetail[i1].m_strInItem6XML;
                    objDPArr2[14].Value = objRecordContentDetail[i1].m_strInItem7;
                    objDPArr2[15].Value = objRecordContentDetail[i1].m_strInItem7XML;
                    objDPArr2[16].Value = objRecordContentDetail[i1].m_strInItem8;
                    objDPArr2[17].Value = objRecordContentDetail[i1].m_strInItem8XML;
                    objDPArr2[18].Value = objRecordContentDetail[i1].m_strOutItem1;
                    objDPArr2[19].Value = objRecordContentDetail[i1].m_strOutItem1XML;
                    objDPArr2[20].Value = objRecordContentDetail[i1].m_strOutItem2;
                    objDPArr2[21].Value = objRecordContentDetail[i1].m_strOutItem2XML;
                    objDPArr2[22].Value = objRecordContentDetail[i1].m_strOutItem3;
                    objDPArr2[23].Value = objRecordContentDetail[i1].m_strOutItem3XML;
                    objDPArr2[24].Value = objRecordContentDetail[i1].m_strOutItem4;
                    objDPArr2[25].Value = objRecordContentDetail[i1].m_strOutItem4XML;
                    objDPArr2[26].Value = objRecordContentDetail[i1].m_strOutItem5;
                    objDPArr2[27].Value = objRecordContentDetail[i1].m_strOutItem5XML;
                    objDPArr2[28].Value = objRecordContentDetail[i1].m_strOutItem6;
                    objDPArr2[29].Value = objRecordContentDetail[i1].m_strOutItem6XML;
                    objDPArr2[30].Value = objRecordContentDetail[i1].m_strOutItem7;
                    objDPArr2[31].Value = objRecordContentDetail[i1].m_strOutItem7XML;
                    objDPArr2[32].Value = objRecordContentDetail[i1].m_strOutItem8;
                    objDPArr2[33].Value = objRecordContentDetail[i1].m_strOutItem8XML;
                    objDPArr2[34].Value = 0;
                    //执行SQL			
                    m_lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
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

            }
			return (long)enmOperationResult.DB_Succeed;
		}
		
		// Add Record
		[AutoComplete] 		
		protected override long m_lngAddNewRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent, clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, string p_strDeptID, clsHRPTableService p_objHRPServ)
		{
			return 0;
		}

		// 查看是否有相同的记录时间
		[AutoComplete] 		
		protected override long m_lngCheckCreateDate(clsBaseCaseHistoryInfo  p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objPreModifyInfo)
		{
			p_objPreModifyInfo=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = Convert.ToDateTime(p_objRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objPreModifyInfo = new clsPreModifyInfo();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_objPreModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString());
                        p_objPreModifyInfo.m_strActionUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
                    }
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

            }			//返回
			return lngRes;


		}

		// 查看是否有相同的记录时间
		[AutoComplete] 		
		public long m_lngCheckRegisterDate(clsRegisterQuantity_VO p_objRecordContent,int p_intRegID,out clsRegisterQuantity_VO p_objPreModifyInfo)
		{
			p_objPreModifyInfo=null;
			p_objPreModifyInfo=new clsRegisterQuantity_VO();
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmRegDate;
                objDPArr[3].Value = p_intRegID;

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckRegDateSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {

                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_objPreModifyInfo.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[i]["ModifyDate"].ToString());
                        p_objPreModifyInfo.m_strModifyUserID = dtbValue.Rows[i]["ModifyUserID"].ToString();
                        p_objPreModifyInfo.m_strModifyUserName = dtbValue.Rows[i]["MODIFYUSERNAME"].ToString();
                    }
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

            }			//返回
			return lngRes;


		}

		// 查看当前记录是否最新的记录。
		[AutoComplete] 		
		protected override long m_lngCheckLastModifyRecord(clsBaseCaseHistoryInfo p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo=null;
			long lngRes = 0;
			string c_strCheckLastModifyRecordSQL=null;
	
			if (clsHRPTableService.bytDatabase_Selector==0)
			{
				c_strCheckLastModifyRecordSQL= @"select top 1lastmodifydate,lastmodifyuserid from ipcasehistory_historycontent where inpatientid = ? and inpatientdate= ? and opendate= ? and status=0 order by lastmodifydate desc";
			}
            else if (clsHRPTableService.bytDatabase_Selector == 2)
			{
				c_strCheckLastModifyRecordSQL= @"select lastmodifydate, lastmodifyuserid
  from (select lastmodifydate, lastmodifyuserid
          from ipcasehistory_historycontent
         where inpatientid = ?
           and inpatientdate = ?
           and opendate = ?
           and status = 0
         order by lastmodifydate desc)
 where rownum = 1";
			}
            else if (clsHRPTableService.bytDatabase_Selector == 4)
            {
                c_strCheckLastModifyRecordSQL = " select lastmodifydate,lastmodifyuserid from ipcasehistory_historycontent where inpatientid = ? and inpatientdate= ? and opendate= ? and status=0 order by lastmodifydate desc fetch first 1 row only";
            }


            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果,
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[i]["LASTMODIFYDATE"].ToString());
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[i]["LASTMODIFYUSERID"].ToString();
                    }
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

            }			//返回
			return lngRes;
	
	
		}


		[AutoComplete] 
		private byte [] m_bytImageToBinary(Image p_img)
		{
			System.IO.MemoryStream objTempStream = new System.IO.MemoryStream();

			p_img.Save(objTempStream,System.Drawing.Imaging.ImageFormat.Bmp);

			return objTempStream.ToArray();
		}
		[AutoComplete] 
		private Image m_imgBinaryToImage(object p_obj)
		{
			System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])p_obj);

			Image img = new Bitmap(objStream);

			return img;
		}
		
		// 把新修改的内容保存到数据库。
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
            clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, string p_strDeptID,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsInPatientCaseHistoryContent m_objContent = (clsInPatientCaseHistoryContent)p_objRecordContent;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(69, out objDPArr);


                objDPArr[0].Value = (m_objContent.m_strCredibility == null ? "" : m_objContent.m_strCredibility);
                objDPArr[1].Value = (m_objContent.m_strRepresentor == null ? "" : m_objContent.m_strRepresentor);
                objDPArr[2].Value = (m_objContent.m_strPrimaryDiagnoseDocID == null ? "" : m_objContent.m_strPrimaryDiagnoseDocID);

                objDPArr[3].DbType = DbType.DateTime;
                if (m_objContent.m_strPrimaryDiagnoseDate == "")
                    objDPArr[3].Value = DBNull.Value;
                else
                    objDPArr[3].Value = DateTime.Parse(m_objContent.m_strPrimaryDiagnoseDate);

                objDPArr[4].Value = (m_objContent.m_strFinallyDiagnoseDocID == null ? "" : m_objContent.m_strFinallyDiagnoseDocID);

                objDPArr[5].DbType = DbType.DateTime;
                if (m_objContent.m_strFinallyDiagnoseDate == "")
                    objDPArr[5].Value = DBNull.Value;
                else
                {
                    try
                    {
                        objDPArr[5].Value = DateTime.Parse(m_objContent.m_strFinallyDiagnoseDate);
                    }
                    catch
                    {
                        objDPArr[5].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }

                objDPArr[6].Value = (m_objContent.m_strMainDescriptionXML == null ? "" : m_objContent.m_strMainDescriptionXML);
                objDPArr[7].Value = (m_objContent.m_strMainDescriptionAll == null ? "" : m_objContent.m_strMainDescriptionAll);
                objDPArr[8].Value = (m_objContent.m_strCurrentStatusXML == null ? "" : m_objContent.m_strCurrentStatusXML);
                objDPArr[9].Value = (m_objContent.m_strCurrentStatusXAll == null ? "" : m_objContent.m_strCurrentStatusXAll);
                objDPArr[10].Value = (m_objContent.m_strBeforetimeStatusXML == null ? "" : m_objContent.m_strBeforetimeStatusXML);
                objDPArr[11].Value = (m_objContent.m_strBeforetimeStatusAll == null ? "" : m_objContent.m_strBeforetimeStatusAll);
                objDPArr[12].Value = (m_objContent.m_strOwnHistoryXML == null ? "" : m_objContent.m_strOwnHistoryXML);
                objDPArr[13].Value = (m_objContent.m_strOwnHistoryAll == null ? "" : m_objContent.m_strOwnHistoryAll);
                objDPArr[14].Value = (m_objContent.m_strMarriageHistoryXML == null ? "" : m_objContent.m_strMarriageHistoryXML);
                objDPArr[15].Value = (m_objContent.m_strMarriageHistoryAll == null ? "" : m_objContent.m_strMarriageHistoryAll);
                objDPArr[16].Value = (m_objContent.m_strFamilyHistoryXML == null ? "" : m_objContent.m_strFamilyHistoryXML);
                objDPArr[17].Value = (m_objContent.m_strFamilyHistoryAll == null ? "" : m_objContent.m_strFamilyHistoryAll);
                objDPArr[18].Value = (m_objContent.m_strSummaryXML == null ? "" : m_objContent.m_strSummaryXML);
                objDPArr[19].Value = (m_objContent.m_strSummaryAll == null ? "" : m_objContent.m_strSummaryAll);
                objDPArr[20].Value = (m_objContent.m_strPrimaryDiagnoseXML == null ? "" : m_objContent.m_strPrimaryDiagnoseXML);
                objDPArr[21].Value = (m_objContent.m_strPrimaryDiagnoseAll == null ? "" : m_objContent.m_strPrimaryDiagnoseAll);
                objDPArr[22].Value = (m_objContent.m_strFinallyDiagnoseXML == null ? "" : m_objContent.m_strFinallyDiagnoseXML);
                objDPArr[23].Value = (m_objContent.m_strFinallyDiagnoseAll == null ? "" : m_objContent.m_strFinallyDiagnoseAll);
                objDPArr[24].Value = (m_objContent.m_strTemperatureXML == null ? "" : m_objContent.m_strTemperatureXML);
                objDPArr[25].Value = (m_objContent.m_strTemperatureAll == null ? "" : m_objContent.m_strTemperatureAll);
                objDPArr[26].Value = (m_objContent.m_strPulseXML == null ? "" : m_objContent.m_strPulseXML);
                objDPArr[27].Value = (m_objContent.m_strPulseAll == null ? "" : m_objContent.m_strPulseAll);
                objDPArr[28].Value = (m_objContent.m_strBreathXML == null ? "" : m_objContent.m_strBreathXML);
                objDPArr[29].Value = (m_objContent.m_strBreathAll == null ? "" : m_objContent.m_strBreathAll);
                objDPArr[30].Value = (m_objContent.m_strSysXML == null ? "" : m_objContent.m_strSysXML);
                objDPArr[31].Value = (m_objContent.m_strSysAll == null ? "" : m_objContent.m_strSysAll);
                objDPArr[32].Value = (m_objContent.m_strDiaXML == null ? "" : m_objContent.m_strDiaXML);
                objDPArr[33].Value = (m_objContent.m_strDiaAll == null ? "" : m_objContent.m_strDiaAll);
                objDPArr[34].Value = (m_objContent.m_strBloodPressureUnitXML == null ? "" : m_objContent.m_strBloodPressureUnitXML);
                objDPArr[35].Value = (m_objContent.m_strBloodPressureUnitAll == null ? "" : m_objContent.m_strBloodPressureUnitAll);
                objDPArr[36].Value = (m_objContent.m_strMedicalXML == null ? "" : m_objContent.m_strMedicalXML);
                objDPArr[37].Value = (m_objContent.m_strMedicalAll == null ? "" : m_objContent.m_strMedicalAll);
                objDPArr[38].Value = (m_objContent.m_strProfessionalCheckXML == null ? "" : m_objContent.m_strProfessionalCheckXML);
                objDPArr[39].Value = (m_objContent.m_strProfessionalCheckAll == null ? "" : m_objContent.m_strProfessionalCheckAll);
                objDPArr[40].Value = (m_objContent.m_strLabCheckAll == null ? "" : m_objContent.m_strLabCheckAll);
                objDPArr[41].Value = (m_objContent.m_strLabCheckXML == null ? "" : m_objContent.m_strLabCheckXML);
                objDPArr[42].Value = (m_objContent.m_strCatameniaHistoryAll == null ? "" : m_objContent.m_strCatameniaHistoryAll);
                objDPArr[43].Value = (m_objContent.m_strCatameniaHistoryXML == null ? "" : m_objContent.m_strCatameniaHistoryXML);

                objDPArr[44].Value = (m_objContent.m_strYJSAll == null ? "" : m_objContent.m_strYJSAll);
                objDPArr[45].Value = (m_objContent.m_strYJSXML == null ? "" : m_objContent.m_strYJSXML);
                objDPArr[46].Value = (m_objContent.m_strContraHistoryAll == null ? "" : m_objContent.m_strContraHistoryAll);
                objDPArr[47].Value = (m_objContent.m_strContraHistoryXML == null ? "" : m_objContent.m_strContraHistoryXML);
                objDPArr[48].Value = (m_objContent.m_strShYSAll == null ? "" : m_objContent.m_strShYSAll);
                objDPArr[49].Value = (m_objContent.m_strShYSXML == null ? "" : m_objContent.m_strShYSXML);
                objDPArr[50].Value = (m_objContent.m_strLCQKAll == null ? "" : m_objContent.m_strLCQKAll);
                objDPArr[51].Value = (m_objContent.m_strLCQKXML == null ? "" : m_objContent.m_strLCQKXML);
                objDPArr[52].Value = (m_objContent.m_strCQJCAll == null ? "" : m_objContent.m_strCQJCAll);
                objDPArr[53].Value = (m_objContent.m_strCQJCXML == null ? "" : m_objContent.m_strCQJCXML);

                objDPArr[54].Value = (m_objContent.m_strCarePlanAll == null ? "" : m_objContent.m_strCarePlanAll);
                objDPArr[55].Value = (m_objContent.m_strCarePlanXML == null ? "" : m_objContent.m_strCarePlanXML);
                objDPArr[56].Value = (m_objContent.m_strOldMaternitySufferAll == null ? "" : m_objContent.m_strOldMaternitySufferAll);
                objDPArr[57].Value = (m_objContent.m_strOldMaternitySufferXML == null ? "" : m_objContent.m_strOldMaternitySufferXML);

                //修正诊断
                objDPArr[58].Value = (m_objContent.m_strModifyDiagnoseAll == null ? "" : m_objContent.m_strModifyDiagnoseAll);
                objDPArr[59].Value = (m_objContent.m_strModifyDiagnoseXML == null ? "" : m_objContent.m_strModifyDiagnoseXML);
                objDPArr[60].Value = (m_objContent.m_strModifyDiagnoseDoctorID == null ? "" : m_objContent.m_strModifyDiagnoseDoctorID);

                objDPArr[61].DbType = DbType.DateTime;
                try
                {
                    objDPArr[61].Value = m_objContent.m_datModifyDiagnose;
                }
                catch
                {
                    objDPArr[61].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                //补充诊断
                objDPArr[62].Value = (m_objContent.m_strAddDiagnoseALL == null ? "" : m_objContent.m_strAddDiagnoseALL);
                objDPArr[63].Value = (m_objContent.m_strAddDiagnoseXML == null ? "" : m_objContent.m_strAddDiagnoseXML);
                objDPArr[64].Value = (m_objContent.m_strAddDiagnoseDoctorID == null ? "" : m_objContent.m_strAddDiagnoseDoctorID);

                objDPArr[65].DbType = DbType.DateTime;
                try
                {
                    objDPArr[65].Value = m_objContent.m_datAddDiagnose;
                }
                catch
                {
                    objDPArr[65].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                //条件
                objDPArr[66].Value = m_objContent.m_strInPatientID;
                objDPArr[67].DbType = DbType.DateTime;
                objDPArr[67].Value = m_objContent.m_dtmInPatientDate;
                objDPArr[68].DbType = DbType.DateTime;
                objDPArr[68].Value = m_objContent.m_dtmOpenDate;


                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                #region 旧的修改画图
                //			if(p_objPicValueArr!=null && p_objPicValueArr.Length>0)
                //			{
                //				IDataParameter[] objDPArr0 = new Oracle.DataAccess.Client.OracleParameter[8];
                //
                //				for(int j=0;j<p_objPicValueArr.Length;j++)
                //				{
                //					//按顺序给IDataParameter赋值
                //					for(int i=0;i<objDPArr0.Length;i++)
                //						objDPArr0[i]=new Oracle.DataAccess.Client.OracleParameter();
                //
                //					objDPArr0[0].Value=j+1;
                //					//					objDPArr0[4].DbType = System.Data.DbType.Binary;
                //					//					objDPArr0[5].DbType = System.Data.DbType.Binary;
                //
                //					//					if(p_objPicValueArr[j].m_imgBack!=null)
                //					//						objDPArr0[4].Value= m_bytImageToBinary(p_objPicValueArr[j].m_imgBack);	
                //					//					else
                //					//					objDPArr0[4].Value= new System.Array();
                //						
                //					if(p_objPicValueArr[j].m_imgFront!=null)
                //						objDPArr0[1].Value= m_bytImageToBinary(p_objPicValueArr[j].m_imgFront);	
                //					else
                //						objDPArr0[1].Value= System.DBNull.Value;
                //
                //					//					objDPArr0[5].Value=Convert.ToString(p_objPicValueArr[j].clrBack);
                //					objDPArr0[2].Value=p_objPicValueArr[j].clrBack.ToArgb();
                //
                //					//					int intTemp = 32;
                //					//					Color.FromArgb((intTemp)
                //
                //					objDPArr0[3].Value= p_objPicValueArr[j].intWidth;
                //					objDPArr0[4].Value= p_objPicValueArr[j].intHeight;
                //
                //					objDPArr0[5].Value=m_objContent.m_strInPatientID;
                //					objDPArr0[6].Value=m_objContent.m_dtmInPatientDate;
                //					objDPArr0[7].Value=m_objContent.m_dtmOpenDate;
                //
                //					//执行SQL
                //					lngEff=0;
                //					long lngRes0 =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecord_PictureSQL,ref lngEff,objDPArr0);
                //					if(lngRes0<=0)	return lngRes0;
                //				}
                //			}
                #endregion 旧的修改画图

                #region 新的修改画图

                #region 先删除旧的记录,以防数据库过大
                //			if(p_objPicValueArr!=null && p_objPicValueArr.Length>0)
                //			{
                //					IDataParameter[] objDPArr00 = new Oracle.DataAccess.Client.OracleParameter[3];
                //
                //	//				for(int j=0;j<p_objPicValueArr.Length;j++)
                //	//				{
                //						//按顺序给IDataParameter赋值
                //						for(int i=0;i<objDPArr00.Length;i++)
                //							objDPArr00[i]=new Oracle.DataAccess.Client.OracleParameter();
                IDataParameter[] objDPArr00 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr00);

                objDPArr00[0].Value = m_objContent.m_strInPatientID;
                objDPArr00[1].DbType = DbType.DateTime;
                objDPArr00[1].Value = m_objContent.m_dtmInPatientDate;
                objDPArr00[2].DbType = DbType.DateTime;
                objDPArr00[2].Value = m_objContent.m_dtmOpenDate;

                //执行SQL
                lngEff = 0;
                long lngRes00 = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecord_PictureSQL, ref lngEff, objDPArr00);
                if (lngRes00 <= 0) return lngRes00;
                //				}
                //			}
                #endregion 先删除旧的记录,以防数据库过大

                #region 再添加新的记录
                if (p_objPicValueArr != null && p_objPicValueArr.Length > 0)
                {


                    for (int j = 0 ; j < p_objPicValueArr.Length ; j++)
                    {

                        IDataParameter[] objDPArr0 = null;
                        objHRPServ.CreateDatabaseParameter(8, out objDPArr0);

                        objDPArr0[0].Value = m_objContent.m_strInPatientID;
                        objDPArr0[1].DbType = DbType.DateTime;
                        objDPArr0[1].Value = m_objContent.m_dtmInPatientDate;
                        objDPArr0[2].DbType = DbType.DateTime;
                        objDPArr0[2].Value = m_objContent.m_dtmOpenDate;

                        objDPArr0[3].Value = j + 1;
                        //					objDPArr0[4].DbType = System.Data.DbType.Binary;
                        //					objDPArr0[5].DbType = System.Data.DbType.Binary;

                        //					if(p_objPicValueArr[j].m_imgBack!=null)
                        //						objDPArr0[4].Value= m_bytImageToBinary(p_objPicValueArr[j].m_imgBack);	
                        //					else
                        //					objDPArr0[4].Value= new System.Array();
                        objDPArr0[4].DbType = DbType.Binary;
                        if (p_objPicValueArr[j].m_bytImage != null)
                            objDPArr0[4].Value = p_objPicValueArr[j].m_bytImage;//m_bytImageToBinary(p_objPicValueArr[j].m_imgFront);	
                        else
                            objDPArr0[4].Value = System.DBNull.Value;

                        //					objDPArr0[5].Value=Convert.ToString(p_objPicValueArr[j].clrBack);
                        objDPArr0[5].Value = p_objPicValueArr[j].clrBack.ToArgb();

                        //					int intTemp = 32;
                        //					Color.FromArgb((intTemp)

                        objDPArr0[6].Value = p_objPicValueArr[j].intWidth;
                        objDPArr0[7].Value = p_objPicValueArr[j].intHeight;


                        //执行SQL
                        lngEff = 0;
                        long lngRes0 = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecord_PictureSQL, ref lngEff, objDPArr0);
                        if (lngRes0 <= 0) return lngRes0;
                    }
                }
                #endregion 再添加新的记录
                #endregion 新的修改画图

                #region 保存病名
                if (p_strDiseaseID != "")//套装模板与病名挂勾
                {
                    lngRes = m_lngSavePatient_Disease(m_objContent.m_strInPatientID, m_objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strDiseaseID, p_strDeptID, p_objHRPServ);
                    if (lngRes <= 0) return lngRes;
                }
                #endregion


                //******************************************************************
                //				IDataParameter[] objDPArr1 = new Oracle.DataAccess.Client.OracleParameter[42];
                //				//按顺序给IDataParameter赋值
                //				for(int i=0;i<objDPArr1.Length;i++)
                //					objDPArr1[i]=new Oracle.DataAccess.Client.OracleParameter();
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(44, out objDPArr1);

                objDPArr1[0].Value = m_objContent.m_strInPatientID;
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = m_objContent.m_dtmInPatientDate;
                objDPArr1[2].DbType = DbType.DateTime;
                objDPArr1[2].Value = m_objContent.m_dtmOpenDate;
                objDPArr1[3].DbType = DbType.DateTime;
                objDPArr1[3].Value = m_objContent.m_dtmModifyDate;
                objDPArr1[4].Value = (m_objContent.m_strModifyUserID == null ? "" : m_objContent.m_strModifyUserID);

                objDPArr1[5].DbType = DbType.DateTime;
                if (m_objContent.m_dtmDeActivedDate == DateTime.MinValue)
                    objDPArr1[5].Value = DBNull.Value;
                else
                    objDPArr1[5].Value = m_objContent.m_dtmDeActivedDate;

                objDPArr1[6].Value = (m_objContent.m_strDeActivedOperatorID == null ? "" : m_objContent.m_strDeActivedOperatorID);
                objDPArr1[7].Value = m_objContent.m_bytStatus;

                objDPArr1[8].Value = (m_objContent.m_strMainDescription == null ? "" : m_objContent.m_strMainDescription);
                objDPArr1[9].Value = (m_objContent.m_strCurrentStatus == null ? "" : m_objContent.m_strCurrentStatus);
                objDPArr1[10].Value = (m_objContent.m_strBeforetimeStatus == null ? "" : m_objContent.m_strBeforetimeStatus);
                objDPArr1[11].Value = (m_objContent.m_strOwnHistory == null ? "" : m_objContent.m_strOwnHistory);
                objDPArr1[12].Value = (m_objContent.m_strMarriageHistory == null ? "" : m_objContent.m_strMarriageHistory);
                objDPArr1[13].Value = (m_objContent.m_strFamilyHistory == null ? "" : m_objContent.m_strFamilyHistory);
                objDPArr1[14].Value = (m_objContent.m_strSummary == null ? "" : m_objContent.m_strSummary);
                //			objDPArr1[15].Value=(m_objContent.m_strPrimaryDiagnose==null ? "":m_objContent.m_strPrimaryDiagnose);
                //			objDPArr1[16].Value=(m_objContent.m_strFinallyDiagnose==null ? "":m_objContent.m_strFinallyDiagnose);
                objDPArr1[15].Value = (m_objContent.m_strTemperature == null ? "" : m_objContent.m_strTemperature);
                objDPArr1[16].Value = (m_objContent.m_strPulse == null ? "" : m_objContent.m_strPulse);
                objDPArr1[17].Value = (m_objContent.m_strBreath == null ? "" : m_objContent.m_strBreath);
                objDPArr1[18].Value = (m_objContent.m_strSys == null ? "" : m_objContent.m_strSys);
                objDPArr1[19].Value = (m_objContent.m_strDia == null ? "" : m_objContent.m_strDia);
                objDPArr1[20].Value = (m_objContent.m_strBloodPressureUnit == null ? "" : m_objContent.m_strBloodPressureUnit);
                objDPArr1[21].Value = (m_objContent.m_strMedical == null ? "" : m_objContent.m_strMedical);
                objDPArr1[22].Value = (m_objContent.m_strProfessionalCheck == null ? "" : m_objContent.m_strProfessionalCheck);
                objDPArr1[23].Value = (m_objContent.m_strLabCheck == null ? "" : m_objContent.m_strLabCheck);
                objDPArr1[24].Value = (m_objContent.m_strCatameniaHistory == null ? "" : m_objContent.m_strCatameniaHistory);
                objDPArr1[25].Value = (m_objContent.m_strFirstCatamenia == null ? "" : m_objContent.m_strFirstCatamenia);
                objDPArr1[26].Value = (m_objContent.m_strCatameniaLastTime == null ? "" : m_objContent.m_strCatameniaLastTime);
                objDPArr1[27].Value = (m_objContent.m_strCatameniaCycle == null ? "" : m_objContent.m_strCatameniaCycle);

                objDPArr1[28].DbType = DbType.DateTime;
                objDPArr1[28].Value = m_objContent.m_dtmLastCatameniaTime;
                objDPArr1[29].Value = (m_objContent.m_strCatameniaCase == null ? "" : m_objContent.m_strCatameniaCase);
                objDPArr1[30].Value = (m_objContent.m_strYJS == null ? "" : m_objContent.m_strYJS);
                objDPArr1[31].Value = (m_objContent.m_strContraHistory == null ? "" : m_objContent.m_strContraHistory);
                objDPArr1[32].Value = (m_objContent.m_strShYS == null ? "" : m_objContent.m_strShYS);
                objDPArr1[33].Value = (m_objContent.m_strLCQK == null ? "" : m_objContent.m_strLCQK);
                objDPArr1[34].Value = (m_objContent.m_strCQJC == null ? "" : m_objContent.m_strCQJC);
                objDPArr1[35].Value = (m_objContent.m_strPregTimes == null ? "" : m_objContent.m_strPregTimes);
                objDPArr1[36].Value = (m_objContent.m_strBornTimes == null ? "" : m_objContent.m_strBornTimes);
                objDPArr1[37].Value = (m_objContent.m_strCarePlan == null ? "" : m_objContent.m_strCarePlan);
                objDPArr1[38].Value = (m_objContent.m_strChargeDoctor == null ? "" : m_objContent.m_strChargeDoctor);
                objDPArr1[39].Value = (m_objContent.m_strDiretDoctor == null ? "" : m_objContent.m_strDiretDoctor);
                objDPArr1[40].Value = (m_objContent.m_strOldMaternitySuffer == null ? "" : m_objContent.m_strOldMaternitySuffer);
                objDPArr1[41].Value = (m_objContent.m_strMidWife == null ? "" : m_objContent.m_strMidWife);
                //修正诊断
                objDPArr1[42].Value = (m_objContent.m_strModifyDiagnose == null ? "" : m_objContent.m_strModifyDiagnose);
                //补充诊断
                objDPArr1[43].Value = (m_objContent.m_strAddDiagnose == null ? "" : m_objContent.m_strAddDiagnose);

                //执行SQL
                lngEff = 0;
                long lngRes1 = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr1);
                if (lngRes1 <= 0) return lngRes1;

                //******************************************************************
                if (m_objContent.m_strPrimaryDiagnoseArr != null)
                {
                    for (int j1 = 0 ; j1 < m_objContent.m_strPrimaryDiagnoseArr.Length ; j1++)
                    {
                        //						objDPArr1 = new Oracle.DataAccess.Client.OracleParameter[6];
                        //						//按顺序给IDataParameter赋值
                        //						for(int i=0;i<objDPArr1.Length;i++)
                        //							objDPArr1[i]=new Oracle.DataAccess.Client.OracleParameter();
                        //				IDataParameter[] objDPArr = null; 
                        objHRPServ.CreateDatabaseParameter(6, out objDPArr1);

                        objDPArr1[0].Value = m_objContent.m_strInPatientID;
                        objDPArr1[1].DbType = DbType.DateTime;
                        objDPArr1[1].Value = m_objContent.m_dtmInPatientDate;
                        objDPArr1[2].DbType = DbType.DateTime;
                        objDPArr1[2].Value = m_objContent.m_dtmOpenDate;
                        objDPArr1[3].DbType = DbType.DateTime;
                        objDPArr1[3].Value = m_objContent.m_dtmModifyDate;
                        objDPArr1[4].Value = j1;

                        if (m_objContent.m_strPrimaryDiagnoseArr[j1] == null || m_objContent.m_strPrimaryDiagnoseArr[j1].Trim() == "")
                            continue;
                        objDPArr1[5].Value = m_objContent.m_strPrimaryDiagnoseArr[j1];

                        //执行SQL					
                        lngRes1 = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContent_PrimaryDiagnoseSQL, ref lngEff, objDPArr1);
                        if (lngRes1 <= 0) return lngRes1;
                    }
                }

                //******************************************************************
                if (m_objContent.m_strFinallyDiagnoseArr != null)
                {
                    for (int j1 = 0 ; j1 < m_objContent.m_strFinallyDiagnoseArr.Length ; j1++)
                    {
                        //						objDPArr1 = new Oracle.DataAccess.Client.OracleParameter[6];
                        //						//按顺序给IDataParameter赋值
                        //						for(int i=0;i<objDPArr1.Length;i++)
                        //							objDPArr1[i]=new Oracle.DataAccess.Client.OracleParameter();
                        //				IDataParameter[] objDPArr = null; 
                        objHRPServ.CreateDatabaseParameter(6, out objDPArr1);

                        objDPArr1[0].Value = m_objContent.m_strInPatientID;
                        objDPArr1[1].DbType = DbType.DateTime;
                        objDPArr1[1].Value = m_objContent.m_dtmInPatientDate;
                        objDPArr1[2].DbType = DbType.DateTime;
                        objDPArr1[2].Value = m_objContent.m_dtmOpenDate;
                        objDPArr1[3].DbType = DbType.DateTime;
                        objDPArr1[3].Value = m_objContent.m_dtmModifyDate;
                        objDPArr1[4].Value = j1;

                        if (m_objContent.m_strFinallyDiagnoseArr[j1] == null || m_objContent.m_strFinallyDiagnoseArr[j1].Trim() == "")
                            continue;
                        objDPArr1[5].Value = m_objContent.m_strFinallyDiagnoseArr[j1];

                        //执行SQL					
                        lngRes1 = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContent_FinallyDiagnoseSQL, ref lngEff, objDPArr1);
                        if (lngRes1 <= 0) return lngRes1;
                    }
                }

                lngRes = lngRes1;

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
            //返回
            return lngRes;

        }
		#region 把记录从数据中“删除”。
		/// <summary>
		/// 把记录从数据中“删除”。
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteRecord(clsRegisterQuantity_VO p_objRecordContent)			
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;
 			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedUserID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmRegDate;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;
		}
		#endregion
		
		// 把记录从数据中“删除”。
		[AutoComplete] 
		protected override long m_lngDeleteRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			long lngRes = 0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                    return (long)enmOperationResult.Parameter_Error;
                //获取IDataParameter数组

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmOpenDate;

                //执行SQL
                long lngEff = 0;
                p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
                if (lngEff < 0) return -1;

                #region 删除画图信息
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr1);
                objDPArr1[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr1[2].DbType = DbType.DateTime;
                objDPArr1[2].Value = p_objRecordContent.m_dtmOpenDate;

                //执行SQL
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecord_PictureSQL, ref lngEff, objDPArr1);
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;


		}

		
		// 获取数据库中最新的修改时间和首次打印时间
		[AutoComplete] 
		protected override long m_lngGetModifyDateAndFirstPrintDate
			(string p_strInPatientID,
			string p_strInPatientDate,/*string p_strOpenRecordTime,*/clsHRPTableService p_objHRPServ,
			out DateTime p_dtmModifyDate,
			out string p_strFirstPrintDate)
		{
			p_dtmModifyDate=DateTime.MinValue ;
			p_strFirstPrintDate=null;
			
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
                //			objDPArr[2].Value=DateTime.Parse(p_strOpenRecordTime);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["LASTMODIFYDATE"].ToString());
                }
                //返回
                return lngRes;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}

		// 获取指定已经被删除记录的内容。		
		[AutoComplete] 
		protected override long m_lngGetDeleteRecordContentWithServ(
			string p_strInPatientID,
			string p_strInPatientDate,
			string p_strOpenRecordTime,
			clsHRPTableService p_objHRPServ,
			out clsBaseCaseHistoryInfo p_objRecordContent)
		{
			p_objRecordContent=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);


                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenRecordTime);

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeletedRecordContentSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsInPatientCaseHistoryContent p_objContent = new clsInPatientCaseHistoryContent();

                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_objContent.m_bytIfConfirm = byte.Parse(dtbValue.Rows[i]["IFCONFIRM"].ToString());
                        p_objContent.m_bytStatus = byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        p_objContent.m_dtmCreateDate = (dtbValue.Rows[i]["CREATEDATE"].ToString() != null ? DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()) : DateTime.MinValue);

                        p_objContent.m_dtmDeActivedDate = (dtbValue.Rows[i]["DEACTIVEDDATE"].ToString() != "" ? DateTime.Parse(dtbValue.Rows[i]["DEACTIVEDDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_dtmFirstPrintDate = (dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString() != "" ? DateTime.Parse(dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_dtmInPatientDate = (dtbValue.Rows[i]["INPATIENTDATE"].ToString() != "" ? DateTime.Parse(dtbValue.Rows[i]["INPATIENTDATE"].ToString()) : DateTime.MinValue);

                        p_objContent.m_dtmModifyDate = (dtbValue.Rows[i]["LASTMODIFYDATE"].ToString() != "" ? DateTime.Parse(dtbValue.Rows[i]["LASTMODIFYDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_dtmOpenDate = (dtbValue.Rows[i]["OPENDATE"].ToString() != "" ? DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_strBeforetimeStatus = dtbValue.Rows[i]["BEFORETIMESTATUS"].ToString();

                        p_objContent.m_strBeforetimeStatusAll = dtbValue.Rows[i]["BEFORETIMESTATUSALL"].ToString();
                        p_objContent.m_strBeforetimeStatusXML = dtbValue.Rows[i]["BEFORETIMESTATUSXML"].ToString();
                        p_objContent.m_strBloodPressureUnit = dtbValue.Rows[i]["BLOODPRESSUREUNIT"].ToString();

                        p_objContent.m_strBloodPressureUnitAll = dtbValue.Rows[i]["BLOODPRESSUREUNITALL"].ToString();
                        p_objContent.m_strBloodPressureUnitXML = dtbValue.Rows[i]["BLOODPRESSUREUNITXML"].ToString();
                        p_objContent.m_strBreath = dtbValue.Rows[i]["BREATH"].ToString();

                        p_objContent.m_strBreathAll = dtbValue.Rows[i]["BREATHALL"].ToString();
                        p_objContent.m_strBreathXML = dtbValue.Rows[i]["BREATHXML"].ToString();
                        p_objContent.m_strConfirmReason = dtbValue.Rows[i]["CONFIRMREASON"].ToString();

                        p_objContent.m_strConfirmReasonXML = dtbValue.Rows[i]["CONFIRMREASONXML"].ToString();
                        p_objContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
                        p_objContent.m_strCreateName = dtbValue.Rows[i]["FIRSTNAME"].ToString();
                        p_objContent.m_strCredibility = dtbValue.Rows[i]["CREDIBILITY"].ToString();

                        p_objContent.m_strCurrentStatus = dtbValue.Rows[i]["CURRENTSTATUS"].ToString();
                        p_objContent.m_strCurrentStatusXAll = dtbValue.Rows[i]["CURRENTSTATUSALL"].ToString();
                        p_objContent.m_strCurrentStatusXML = dtbValue.Rows[i]["CURRENTSTATUSXML"].ToString();

                        p_objContent.m_strCatameniaHistory = dtbValue.Rows[i]["CATAMENIAHISTORY"].ToString();
                        p_objContent.m_strCatameniaHistoryAll = dtbValue.Rows[i]["CATAMENIAHISTORYALL"].ToString();
                        p_objContent.m_strCatameniaHistoryXML = dtbValue.Rows[i]["CATAMENIAHISTORYXML"].ToString();

                        p_objContent.m_strDeActivedOperatorID = dtbValue.Rows[i]["DEACTIVEDOPERATORID"].ToString();

                        p_objContent.m_strDia = dtbValue.Rows[i]["DIA"].ToString();
                        p_objContent.m_strDiaAll = dtbValue.Rows[i]["DIAALL"].ToString();
                        p_objContent.m_strDiaXML = dtbValue.Rows[i]["DIAXML"].ToString();

                        p_objContent.m_strFamilyHistory = dtbValue.Rows[i]["FAMILYHISTORY"].ToString();
                        p_objContent.m_strFamilyHistoryAll = dtbValue.Rows[i]["FAMILYHISTORYALL"].ToString();
                        p_objContent.m_strFamilyHistoryXML = dtbValue.Rows[i]["FAMILYHISTORYXML"].ToString();

                        //p_objContent.m_strFinallyDiagnose=dtbValue.Rows[i]["FINALLYDIAGNOSE"].ToString() ;
                        p_objContent.m_strFinallyDiagnoseAll = dtbValue.Rows[i]["FINALLYDIAGNOSEALL"].ToString();
                        p_objContent.m_strFinallyDiagnoseXML = dtbValue.Rows[i]["FINALLYDIAGNOSEXML"].ToString();

                        p_objContent.m_strFinallyDiagnoseDate = dtbValue.Rows[i]["FINALLYDIAGNOSEDATE"].ToString();
                        p_objContent.m_strFinallyDiagnoseDocID = dtbValue.Rows[i]["FINALLYDIAGNOSEDOCID"].ToString();
                        p_objContent.m_strInPatientID = dtbValue.Rows[i]["INPATIENTID"].ToString();

                        p_objContent.m_strLabCheckAll = dtbValue.Rows[i]["LABCHECKALL"].ToString();
                        p_objContent.m_strLabCheckXML = dtbValue.Rows[i]["LABCHECKXML"].ToString();
                        p_objContent.m_strLabCheck = dtbValue.Rows[i]["LABCHECK"].ToString();

                        p_objContent.m_strMainDescription = dtbValue.Rows[i]["MAINDESCRIPTION"].ToString();
                        p_objContent.m_strMainDescriptionAll = dtbValue.Rows[i]["MAINDESCRIPTIONALL"].ToString();
                        p_objContent.m_strMainDescriptionXML = dtbValue.Rows[i]["MAINDESCRIPTIONXML"].ToString();

                        p_objContent.m_strMarriageHistory = dtbValue.Rows[i]["MARRIAGEHISTORY"].ToString();
                        p_objContent.m_strMarriageHistoryAll = dtbValue.Rows[i]["MARRIAGEHISTORYALL"].ToString();
                        p_objContent.m_strMarriageHistoryXML = dtbValue.Rows[i]["MARRIAGEHISTORYXML"].ToString();

                        p_objContent.m_strMedical = dtbValue.Rows[i]["MEDICAL"].ToString();
                        p_objContent.m_strMedicalAll = dtbValue.Rows[i]["MEDICALALL"].ToString();
                        p_objContent.m_strMedicalXML = dtbValue.Rows[i]["MEDICALXML"].ToString();

                        p_objContent.m_strModifyUserID = dtbValue.Rows[i]["LASTMODIFYUSERID"].ToString();
                        p_objContent.m_strOwnHistory = dtbValue.Rows[i]["OWNHISTORY"].ToString();
                        p_objContent.m_strOwnHistoryAll = dtbValue.Rows[i]["OWNHISTORYALL"].ToString();

                        p_objContent.m_strOwnHistoryXML = dtbValue.Rows[i]["OWNHISTORYXML"].ToString();
                        //p_objContent.m_strPrimaryDiagnose=dtbValue.Rows[i]["PRIMARYDIAGNOSE"].ToString() ;
                        p_objContent.m_strPrimaryDiagnoseAll = dtbValue.Rows[i]["PRIMARYDIAGNOSEALL"].ToString();

                        p_objContent.m_strPrimaryDiagnoseDate = dtbValue.Rows[i]["PRIMARYDIAGNOSEDATE"].ToString();
                        p_objContent.m_strPrimaryDiagnoseDocID = dtbValue.Rows[i]["PRIMARYDIAGNOSEDOCID"].ToString();
                        p_objContent.m_strPrimaryDiagnoseXML = dtbValue.Rows[i]["PRIMARYDIAGNOSEXML"].ToString();

                        p_objContent.m_strProfessionalCheck = dtbValue.Rows[i]["PROFESSIONALCHECK"].ToString();
                        p_objContent.m_strProfessionalCheckAll = dtbValue.Rows[i]["PROFESSIONALCHECKALL"].ToString();
                        p_objContent.m_strProfessionalCheckXML = dtbValue.Rows[i]["PROFESSIONALCHECKXML"].ToString();

                        p_objContent.m_strPulse = dtbValue.Rows[i]["PULSE"].ToString();
                        p_objContent.m_strPulseAll = dtbValue.Rows[i]["PULSEALL"].ToString();
                        p_objContent.m_strPulseXML = dtbValue.Rows[i]["PULSEXML"].ToString();

                        p_objContent.m_strRepresentor = dtbValue.Rows[i]["REPRESENTOR"].ToString();
                        p_objContent.m_strSummary = dtbValue.Rows[i]["SUMMARY"].ToString();
                        p_objContent.m_strSummaryAll = dtbValue.Rows[i]["SUMMARYALL"].ToString();

                        p_objContent.m_strSummaryXML = dtbValue.Rows[i]["SUMMARYXML"].ToString();
                        p_objContent.m_strSys = dtbValue.Rows[i]["SYS"].ToString();
                        p_objContent.m_strSysAll = dtbValue.Rows[i]["SYSALL"].ToString();

                        p_objContent.m_strSysXML = dtbValue.Rows[i]["SYSXML"].ToString();
                        p_objContent.m_strTemperature = dtbValue.Rows[i]["TEMPERATURE"].ToString();
                        p_objContent.m_strTemperatureAll = dtbValue.Rows[i]["TEMPERATUREALL"].ToString();

                        p_objContent.m_strTemperatureXML = dtbValue.Rows[i]["TEMPERATUREXML"].ToString();

                        p_objContent.m_strYJS = dtbValue.Rows[i]["YJS"].ToString();
                        p_objContent.m_strYJSAll = dtbValue.Rows[i]["YJSALL"].ToString();
                        p_objContent.m_strYJSXML = dtbValue.Rows[i]["YJSXML"].ToString();

                        p_objContent.m_strContraHistory = dtbValue.Rows[i]["CONTRAHISTORY"].ToString();
                        p_objContent.m_strContraHistoryAll = dtbValue.Rows[i]["CONTRAHISTORYALL"].ToString();
                        p_objContent.m_strContraHistoryXML = dtbValue.Rows[i]["CONTRAHISTORYXML"].ToString();

                        p_objContent.m_strShYS = dtbValue.Rows[i]["SHYS"].ToString();
                        p_objContent.m_strShYSAll = dtbValue.Rows[i]["SHYSALL"].ToString();
                        p_objContent.m_strShYSXML = dtbValue.Rows[i]["SHYSXML"].ToString();

                        p_objContent.m_strLCQK = dtbValue.Rows[i]["LCQK"].ToString();
                        p_objContent.m_strLCQKAll = dtbValue.Rows[i]["LCQKALL"].ToString();
                        p_objContent.m_strLCQKXML = dtbValue.Rows[i]["LCQKXML"].ToString();

                        p_objContent.m_strCQJC = dtbValue.Rows[i]["CQJC"].ToString();
                        p_objContent.m_strCQJCAll = dtbValue.Rows[i]["CQJCALL"].ToString();
                        p_objContent.m_strCQJCXML = dtbValue.Rows[i]["CQJCXML"].ToString();

                        p_objContent.m_strCarePlan = dtbValue.Rows[i]["CAREPLAN"].ToString();
                        p_objContent.m_strCarePlanAll = dtbValue.Rows[i]["CAREPLANALL"].ToString();
                        p_objContent.m_strCarePlanXML = dtbValue.Rows[i]["CAREPLANXML"].ToString();

                        p_objContent.m_strPregTimes = dtbValue.Rows[i]["PREGTIMES"].ToString();
                        p_objContent.m_strBornTimes = dtbValue.Rows[i]["BORNTIMES"].ToString();
                        p_objContent.m_strChargeDoctor = dtbValue.Rows[i]["CHARGEDOCTOR"].ToString();
                        p_objContent.m_strDiretDoctor = dtbValue.Rows[i]["DIRETDOCTOR"].ToString();
                        p_objContent.m_strMidWife = dtbValue.Rows[i]["MIDWIFE"].ToString();

                        p_objContent.m_strOldMaternitySuffer = dtbValue.Rows[i]["OLDMATERNITYSUFFER"].ToString();
                        p_objContent.m_strOldMaternitySufferAll = dtbValue.Rows[i]["OLDMATERNITYSUFFERALL"].ToString();
                        p_objContent.m_strOldMaternitySufferXML = dtbValue.Rows[i]["OLDMATERNITYSUFFERXML"].ToString();

                        //修正诊断
                        p_objContent.m_strModifyDiagnose = dtbValue.Rows[i]["MODIFYDIAGNOSE"].ToString();
                        p_objContent.m_strModifyDiagnoseAll = dtbValue.Rows[i]["MODIFYDIAGNOSEALL"].ToString();
                        p_objContent.m_strModifyDiagnoseXML = dtbValue.Rows[i]["MODIFYDIAGNOSEXML"].ToString();
                        p_objContent.m_strModifyDiagnoseDoctorID = dtbValue.Rows[i]["MODIFYDIAGNOSEDOCTORID"].ToString();
                        try
                        {
                            p_objContent.m_datModifyDiagnose = DateTime.Parse(dtbValue.Rows[i]["MODIFYDIAGNOSEDATE"].ToString());
                        }
                        catch
                        {
                            p_objContent.m_datModifyDiagnose = DateTime.Now;

                        }
                        //补充诊断
                        p_objContent.m_strAddDiagnose = dtbValue.Rows[i]["ADDDIAGNOSE"].ToString();
                        p_objContent.m_strAddDiagnoseALL = dtbValue.Rows[i]["ADDDIAGNOSEALL"].ToString();
                        p_objContent.m_strAddDiagnoseXML = dtbValue.Rows[i]["ADDDIAGNOSEXML"].ToString();
                        p_objContent.m_strAddDiagnoseDoctorID = dtbValue.Rows[i]["ADDDIAGNOSEDOCTORID"].ToString();
                        try
                        {
                            p_objContent.m_datAddDiagnose = DateTime.Parse(dtbValue.Rows[i]["ADDDIAGNOSEDATE"].ToString());

                        }
                        catch
                        {
                            p_objContent.m_datAddDiagnose = DateTime.Now;

                        }
                    }

                    p_objRecordContent = p_objContent;
                }

                //返回
                return lngRes;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;


		}
		[AutoComplete] 
		private long m_lngAddPatient_Disease(string p_strPatID,string p_strInPatientDate,string p_strDiseaseID)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //				IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[3];
                //				for(int i=0;i<objDPArr.Length;i++) 
                //					objDPArr[i] = new Oracle.DataAccess.Client.OracleParameter();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strPatID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDiseaseID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewPatient_Disease, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}

		[AutoComplete] 
		private long m_lngSavePatient_Disease(string p_strPatID,string p_strInPatientDate,string p_strDiseaseID,string p_strDeptID,clsHRPTableService p_objHRPServ)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string strSql = @"select a.inpatientid,
       a.inpatientdate,
       a.associateid,
       b.deptid,
       b.formname,
       b.templatesetid,
       b.associatename,
       b.type
  from patient_associate a
 inner join templateset_associate b on a.associateid = b.associateid
 where inpatientid = ?
   and inpatientdate = ?
   and b.type = '0'
   and b.deptid = ?";
                DataTable dtExist = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeptID;

                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSql, ref dtExist, objDPArr);
                if (lngRes <= 0) return lngRes;
                if (dtExist.Rows.Count > 0)
                {

                    strSql = @"delete patient_associate
								where 
								inpatientid = ? 
								and inpatientdate = ?
								and associateid in
								(select b.associateid from patient_associate a
								inner join templateset_associate b on a.associateid = b.associateid
								where inpatientid = ? 
								and inpatientdate = ?
								and b.type = '0' and b.deptid = ?)";

                    objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                    objDPArr[0].Value = p_strPatID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[2].Value = p_strPatID;
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[4].Value = p_strDeptID;

                    long lngEff = -1;
                    lngRes = p_objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                }
                if (p_strDiseaseID.Trim() != "")
                    lngRes = m_lngAddPatient_Disease(p_strPatID, p_strInPatientDate, p_strDiseaseID);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}


	}
}
