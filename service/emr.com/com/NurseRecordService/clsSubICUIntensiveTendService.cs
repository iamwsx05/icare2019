using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService;

namespace com.digitalwave.clsSubICUIntensiveTendService
{
	/// <summary>
	/// 危重症监护中心特护记录单
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsSubICUIntensiveTendService:clsDiseaseTrackService
	{
		public clsSubICUIntensiveTendService()
		{}


		private const string c_strCheckCreateDateSQL = @"select createuserid,opendate from icuintensivetend where inpatientid = ? and inpatientdate= ? and createdate= ? and status=0";

		private const string c_strAddNewRecordSQL = @"insert into icuintensivetend(inpatientid,
			inpatientdate,opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,
			machinemode,machinemodexml,breathsoundleft,breathsoundleftxml,breathsoundright,
			breathsoundrightxml,t,t_xml,p,p_xml,r,r_xml,bp,bp_xml,cvp,cvp_xml,bloodsugar,
			bloodsugarxml,consciousness,consciousnessxml,pupilsizeleft,pupilsizeleftxml,
			pupilsizeright,pupilsizerightxml,reflectleft,reflectleftxml,reflectright,
			reflectrightxml,drugname,drugnamexml,drugdosage,drugdosagexml,stomachdirection,
			stomachdirectionxml,stomachproperty,stomachpropertyxml,stomachquantity,
			stomachquantityxml,inoral,inoralxml,peedirection,peedirectionxml,
			peeproperty,peepropertyxml,peequantity,peequantityxml,defecateproperty,
			defecatepropertyxml,defecatequantity,defecatequantityxml,leaddirection,
			leaddirectionxml,leadproperty,leadpropertyxml,leadquantity,leadquantityxml,
			sputumproperty,sputumpropertyxml,sputumquantity,sputumquantityxml,skin,skinxml
			,casehistory,casehistoryxml,hr,hr_xml,bp2,bp2_xml,power,powerxml,stomachpipe,
			stomachpipexml,inoraltype,inoraltypexml,inoralproperty,inoralpropertyxml,
			inoralquantity,inoralquantityxml,transfusiontotal,transfusiontotalxml,
			takefoodtotal,takefoodtotalxml,leadpipe,leadpipexml,defecatetimes,defecatetimesxml
			) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
			?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
			?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
			?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";


		private const string c_strAddNewRecordContentSQL = @"insert into icuintensivetendcontent
				(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,machinemode_last,
				breathsoundleft_last,breathsoundright_last,t_last,p_last,r_last,bp_last,
				cvp_last,bloodsugar_last,consciousness_last,pupilsizeleft_last,
				pupilsizeright_last,reflectleft_last,reflectright_last,stomachdirection_last,
				stomachproperty_last,stomachquantity_last,peedirection_last,peeproperty_last,
				peequantity_last,defecateproperty_last,defecatequantity_last,leaddirection_last,
				leadproperty_last,leadquantity_last,sputumproperty_last,sputumquantity_last,
				skin_last,casehistory_last,hr_last,bp2_last,power_last,stomachpipe_last,
				transfusiontotal_last,takefoodtotal_last,leadpipe_last,defecatetimes_last
				) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		private const string c_strGetModifyDateAndFirstPrintDateSQL=@"";

		private const string c_strGetDeleteRecordSQL = @"select deactiveddate,deactivedoperatorid from icuintensivetend where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strModifyRecordContentSQL=c_strAddNewRecordContentSQL;
		private const string c_strModifyRecordSQL=@"update icuintensivetend set machinemode=?,machinemodexml=?,
				breathsoundleft=?,breathsoundleftxml=?,breathsoundright=?,
				breathsoundrightxml=?,t=?,t_xml=?,p=?,p_xml=?,r=?,r_xml=?,bp=?,bp_xml=?,cvp=?,cvp_xml=?,
				bloodsugar=?,bloodsugarxml=?,consciousness=?,consciousnessxml=?,pupilsizeleft=?,
				pupilsizeleftxml=?,pupilsizeright=?,pupilsizerightxml=?,reflectleft=?,reflectleftxml=?,
				reflectright=?,reflectrightxml=?,drugname=?,drugnamexml=?,drugdosage=?,drugdosagexml=?,
				stomachdirection=?,stomachdirectionxml=?,stomachproperty=?,stomachpropertyxml=?,
				stomachquantity=?,stomachquantityxml=?,inoral=?,inoralxml=?,peedirection=?,
				peedirectionxml=?,peeproperty=?,peepropertyxml=?,peequantity=?,peequantityxml=?,
				defecateproperty=?,defecatepropertyxml=?,defecatequantity=?,defecatequantityxml=?,
				leaddirection=?,leaddirectionxml=?,leadproperty=?,leadpropertyxml=?,leadquantity=?,
				leadquantityxml=?,sputumproperty=?,sputumpropertyxml=?,sputumquantity=?,
				sputumquantityxml=?,skin=?,skinxml=?,casehistory=?,casehistoryxml=?,hr=?,hr_xml=?,bp2=?,bp2_xml=?,
				power=?,powerxml=?,stomachpipe=?,stomachpipexml=?,inoraltype=?,inoraltypexml=?,
				inoralproperty=?,inoralpropertyxml=?,inoralquantity=?,inoralquantityxml=?,transfusiontotal=?,
				transfusiontotalxml=?,takefoodtotal=?,takefoodtotalxml=?,leadpipe=?,leadpipexml=?,
				defecatetimes=?,defecatetimesxml=?
				where inpatientid=? and inpatientdate=? and opendate=? and status=?";

		private const string c_strAddInLiquidSQL = @"insert into icuintensivetendinliquid(inpatientid,inpatientdate,
						opendate,modifydate,modifyuserid,drugseq,drugname,drugdosage) values(?,?,?,?,?,?,?,?)";

		private const string c_strAddInOralSQL = @"insert into icuintensivetendinoral(inpatientid,inpatientdate,
					opendate,modifydate,modifyuserid,oralseq,inoral,inoraltype,inoralproperty,inoralquantity) values(?,?,?,?,?,?,?,?,?,?)";

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
			p_strCreateDateArr=null;
			p_strOpenDateArr=null;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubICUIntensiveTendService","m_lngGetRecordTimeList");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
		
			//按顺序给IDataParameter赋值
		
			//生成DataTable
		
			//执行查询，填充结果到DataTable
		
			//从DataTable.Rows中获取结果
		
			//设置结果
		
			//返回DB_Succees
			return (long)enmOperationResult.DB_Succeed;
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubICUIntensiveTendService","m_lngUpdateFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			//检查参数                              
			if(p_strInPatientID==null || p_strInPatientID==""||p_strInPatientDate==null || p_strInPatientDate==""||p_strOpenDate==null || p_strOpenDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
		
			//按顺序给IDataParameter赋值
		
			//执行SQL
			return (long)enmOperationResult.DB_Succeed;
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
			p_strCreateDateArr=null;
			p_strOpenDateArr=null;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubICUIntensiveTendService","m_lngGetDeleteRecordTimeList");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
		
			//按顺序给IDataParameter赋值
		
			//生成DataTable
		
			//执行查询，填充结果到DataTable
		
			//从DataTable.Rows中获取结果
		
			//设置结果
		
			//返回DB_Succees
			return (long)enmOperationResult.DB_Succeed;
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
			p_strCreateDateArr=null;
			p_strOpenDateArr=null;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubICUIntensiveTendService","m_lngGetDeleteRecordTimeListAll");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
		
			//按顺序给IDataParameter赋值
		
			//生成DataTable
		
			//执行查询，填充结果到DataTable
		
			//从DataTable.Rows中获取结果
		
			//设置结果
		
			//返回DB_Succees
			return (long)enmOperationResult.DB_Succeed;
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
			p_objRecordContent=null;
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

            string c_strGetRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @" a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.machinemode,
       a.machinemodexml,
       a.breathsoundleft,
       a.breathsoundleftxml,
       a.breathsoundright,
       a.breathsoundrightxml,
       a.t,
       a.t_xml,
       a.p,
       a.p_xml,
       a.r,
       a.r_xml,
       a.bp,
       a.bp_xml,
       a.cvp,
       a.cvp_xml,
       a.bloodsugar,
       a.bloodsugarxml,
       a.consciousness,
       a.consciousnessxml,
       a.pupilsizeleft,
       a.pupilsizeleftxml,
       a.pupilsizeright,
       a.pupilsizerightxml,
       a.reflectleft,
       a.reflectleftxml,
       a.reflectright,
       a.reflectrightxml,
       a.drugname,
       a.drugnamexml,
       a.drugdosage,
       a.drugdosagexml,
       a.stomachdirection,
       a.stomachdirectionxml,
       a.stomachproperty,
       a.stomachpropertyxml,
       a.stomachquantity,
       a.stomachquantityxml,
       a.inoral,
       a.inoralxml,
       a.peedirection,
       a.peedirectionxml,
       a.peeproperty,
       a.peepropertyxml,
       a.peequantity,
       a.peequantityxml,
       a.defecateproperty,
       a.defecatepropertyxml,
       a.defecatequantity,
       a.defecatequantityxml,
       a.leaddirection,
       a.leaddirectionxml,
       a.leadproperty,
       a.leadpropertyxml,
       a.leadquantity,
       a.leadquantityxml,
       a.sputumproperty,
       a.sputumpropertyxml,
       a.sputumquantity,
       a.sputumquantityxml,
       a.skin,
       a.skinxml,
       a.casehistory,
       a.casehistoryxml,
       a.hr,
       a.hr_xml,
       a.bp2,
       a.bp2_xml,
       a.power,
       a.powerxml,
       a.stomachpipe,
       a.stomachpipexml,
       a.inoraltype,
       a.inoraltypexml,
       a.inoralproperty,
       a.inoralpropertyxml,
       a.inoralquantity,
       a.inoralquantityxml,
       a.transfusiontotal,
       a.transfusiontotalxml,
       a.takefoodtotal,
       a.takefoodtotalxml,
       a.leadpipe,
       a.leadpipexml,
       a.defecatetimes,
       a.defecatetimesxml,
       b.modifydate,
       b.modifyuserid,
       b.machinemode_last,
       b.breathsoundleft_last,
       b.breathsoundright_last,
       b.t_last,
       b.p_last,
       b.r_last,
       b.bp_last,
       b.cvp_last,
       b.bloodsugar_last,
       b.consciousness_last,
       b.pupilsizeleft_last,
       b.pupilsizeright_last,
       b.reflectleft_last,
       b.reflectright_last,
       b.stomachdirection_last,
       b.stomachproperty_last,
       b.stomachquantity_last,
       b.peedirection_last,
       b.peeproperty_last,
       b.peequantity_last,
       b.defecateproperty_last,
       b.defecatequantity_last,
       b.leaddirection_last,
       b.leadproperty_last,
       b.leadquantity_last,
       b.sputumproperty_last,
       b.sputumquantity_last,
       b.skin_last,
       b.casehistory_last,
       b.hr_last,
       b.bp2_last,
       b.power_last,
       b.stomachpipe_last,
       b.transfusiontotal_last,
       b.takefoodtotal_last,
       b.leadpipe_last,
       b.defecatetimes_last
  from (select t1.inpatientid,
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
               t1.machinemode,
               t1.machinemodexml,
               t1.breathsoundleft,
               t1.breathsoundleftxml,
               t1.breathsoundright,
               t1.breathsoundrightxml,
               t1.t,
               t1.t_xml,
               t1.p,
               t1.p_xml,
               t1.r,
               t1.r_xml,
               t1.bp,
               t1.bp_xml,
               t1.cvp,
               t1.cvp_xml,
               t1.bloodsugar,
               t1.bloodsugarxml,
               t1.consciousness,
               t1.consciousnessxml,
               t1.pupilsizeleft,
               t1.pupilsizeleftxml,
               t1.pupilsizeright,
               t1.pupilsizerightxml,
               t1.reflectleft,
               t1.reflectleftxml,
               t1.reflectright,
               t1.reflectrightxml,
               t1.drugname,
               t1.drugnamexml,
               t1.drugdosage,
               t1.drugdosagexml,
               t1.stomachdirection,
               t1.stomachdirectionxml,
               t1.stomachproperty,
               t1.stomachpropertyxml,
               t1.stomachquantity,
               t1.stomachquantityxml,
               t1.inoral,
               t1.inoralxml,
               t1.peedirection,
               t1.peedirectionxml,
               t1.peeproperty,
               t1.peepropertyxml,
               t1.peequantity,
               t1.peequantityxml,
               t1.defecateproperty,
               t1.defecatepropertyxml,
               t1.defecatequantity,
               t1.defecatequantityxml,
               t1.leaddirection,
               t1.leaddirectionxml,
               t1.leadproperty,
               t1.leadpropertyxml,
               t1.leadquantity,
               t1.leadquantityxml,
               t1.sputumproperty,
               t1.sputumpropertyxml,
               t1.sputumquantity,
               t1.sputumquantityxml,
               t1.skin,
               t1.skinxml,
               t1.casehistory,
               t1.casehistoryxml,
               t1.hr,
               t1.hr_xml,
               t1.bp2,
               t1.bp2_xml,
               t1.power,
               t1.powerxml,
               t1.stomachpipe,
               t1.stomachpipexml,
               t1.inoraltype,
               t1.inoraltypexml,
               t1.inoralproperty,
               t1.inoralpropertyxml,
               t1.inoralquantity,
               t1.inoralquantityxml,
               t1.transfusiontotal,
               t1.transfusiontotalxml,
               t1.takefoodtotal,
               t1.takefoodtotalxml,
               t1.leadpipe,
               t1.leadpipexml,
               t1.defecatetimes,
               t1.defecatetimesxml
          from icuintensivetend t1
         where t1.inpatientid = ?
           and t1.inpatientdate = ?
           and t1.opendate = ?) a
 inner join (select t2.inpatientid,
                    t2.inpatientdate,
                    t2.opendate,
                    t2.modifydate,
                    t2.modifyuserid,
                    t2.machinemode_last,
                    t2.breathsoundleft_last,
                    t2.breathsoundright_last,
                    t2.t_last,
                    t2.p_last,
                    t2.r_last,
                    t2.bp_last,
                    t2.cvp_last,
                    t2.bloodsugar_last,
                    t2.consciousness_last,
                    t2.pupilsizeleft_last,
                    t2.pupilsizeright_last,
                    t2.reflectleft_last,
                    t2.reflectright_last,
                    t2.stomachdirection_last,
                    t2.stomachproperty_last,
                    t2.stomachquantity_last,
                    t2.peedirection_last,
                    t2.peeproperty_last,
                    t2.peequantity_last,
                    t2.defecateproperty_last,
                    t2.defecatequantity_last,
                    t2.leaddirection_last,
                    t2.leadproperty_last,
                    t2.leadquantity_last,
                    t2.sputumproperty_last,
                    t2.sputumquantity_last,
                    t2.skin_last,
                    t2.casehistory_last,
                    t2.hr_last,
                    t2.bp2_last,
                    t2.power_last,
                    t2.stomachpipe_last,
                    t2.transfusiontotal_last,
                    t2.takefoodtotal_last,
                    t2.leadpipe_last,
                    t2.defecatetimes_last
               from icuintensivetendcontent t2
              where t2.inpatientid = ?
                and t2.inpatientdate = ?
                and t2.opendate = ?) b on a.inpatientid = b.inpatientid
                                       and a.inpatientdate =
                                           b.inpatientdate
                                       and a.opendate = b.opendate
 where a.status = 0
 order by b.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[6];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果

                    clsICUIntensiveTendContent objRecordContent = new clsICUIntensiveTendContent();
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

                    objRecordContent.m_strMachineMode = dtbValue.Rows[0]["MACHINEMODE"].ToString();
                    objRecordContent.m_strMachineModeXML = dtbValue.Rows[0]["MACHINEMODEXML"].ToString();
                    objRecordContent.m_strBreathSoundLeft = dtbValue.Rows[0]["BREATHSOUNDLEFT"].ToString();
                    objRecordContent.m_strBreathSoundLeftXML = dtbValue.Rows[0]["BREATHSOUNDLEFTXML"].ToString();
                    objRecordContent.m_strBreathSoundRight = dtbValue.Rows[0]["BREATHSOUNDRIGHT"].ToString();
                    objRecordContent.m_strBreathSoundRightXML = dtbValue.Rows[0]["BREATHSOUNDRIGHTXML"].ToString();
                    objRecordContent.m_strT = dtbValue.Rows[0]["T"].ToString();
                    objRecordContent.m_strT_XML = dtbValue.Rows[0]["T_XML"].ToString();
                    objRecordContent.m_strP = dtbValue.Rows[0]["P"].ToString();
                    objRecordContent.m_strP_XML = dtbValue.Rows[0]["P_XML"].ToString();
                    objRecordContent.m_strR = dtbValue.Rows[0]["R"].ToString();
                    objRecordContent.m_strR_XML = dtbValue.Rows[0]["R_XML"].ToString();
                    objRecordContent.m_strBp = dtbValue.Rows[0]["BP"].ToString();
                    objRecordContent.m_strBp_XML = dtbValue.Rows[0]["BP_XML"].ToString();
                    objRecordContent.m_strCVP = dtbValue.Rows[0]["CVP"].ToString();
                    objRecordContent.m_strCVP_XML = dtbValue.Rows[0]["CVP_XML"].ToString();
                    objRecordContent.m_strBloodSugar = dtbValue.Rows[0]["BLOODSUGAR"].ToString();
                    objRecordContent.m_strBloodSugarXML = dtbValue.Rows[0]["BLOODSUGARXML"].ToString();
                    objRecordContent.m_strConsciousness = dtbValue.Rows[0]["CONSCIOUSNESS"].ToString();
                    objRecordContent.m_strConsciousnessXML = dtbValue.Rows[0]["CONSCIOUSNESSXML"].ToString();
                    objRecordContent.m_strPupilSizeLeft = dtbValue.Rows[0]["PUPILSIZELEFT"].ToString();
                    objRecordContent.m_strPupilSizeLeftXML = dtbValue.Rows[0]["PUPILSIZELEFTXML"].ToString();
                    objRecordContent.m_strPupilSizeRight = dtbValue.Rows[0]["PUPILSIZERIGHT"].ToString();
                    objRecordContent.m_strPupilSizeRightXML = dtbValue.Rows[0]["PUPILSIZERIGHTXML"].ToString();
                    objRecordContent.m_strReflectLeft = dtbValue.Rows[0]["REFLECTLEFT"].ToString();
                    objRecordContent.m_strReflectLeftXML = dtbValue.Rows[0]["REFLECTLEFTXML"].ToString();
                    objRecordContent.m_strReflectRight = dtbValue.Rows[0]["REFLECTRIGHT"].ToString();
                    objRecordContent.m_strReflectRightXML = dtbValue.Rows[0]["REFLECTRIGHTXML"].ToString();
                    objRecordContent.m_strDrugName = dtbValue.Rows[0]["DRUGNAME"].ToString();
                    objRecordContent.m_strDrugNameXML = dtbValue.Rows[0]["DRUGNAMEXML"].ToString();
                    objRecordContent.m_strDrugDosage = dtbValue.Rows[0]["DRUGDOSAGE"].ToString();
                    objRecordContent.m_strDrugDosageXML = dtbValue.Rows[0]["DRUGDOSAGEXML"].ToString();
                    objRecordContent.m_strStomachDirection = dtbValue.Rows[0]["STOMACHDIRECTION"].ToString();
                    objRecordContent.m_strStomachDirectionXML = dtbValue.Rows[0]["STOMACHDIRECTIONXML"].ToString();
                    objRecordContent.m_strStomachProperty = dtbValue.Rows[0]["STOMACHPROPERTY"].ToString();
                    objRecordContent.m_strStomachPropertyXML = dtbValue.Rows[0]["STOMACHPROPERTYXML"].ToString();
                    objRecordContent.m_strStomachQuantity = dtbValue.Rows[0]["STOMACHQUANTITY"].ToString();
                    objRecordContent.m_strStomachQuantityXML = dtbValue.Rows[0]["STOMACHQUANTITYXML"].ToString();
                    objRecordContent.m_strInOral = dtbValue.Rows[0]["INORAL"].ToString();
                    objRecordContent.m_strInOralXML = dtbValue.Rows[0]["INORALXML"].ToString();
                    objRecordContent.m_strPeeDirection = dtbValue.Rows[0]["PEEDIRECTION"].ToString();
                    objRecordContent.m_strPeeDirectionXML = dtbValue.Rows[0]["PEEDIRECTIONXML"].ToString();
                    objRecordContent.m_strPeeProperty = dtbValue.Rows[0]["PEEPROPERTY"].ToString();
                    objRecordContent.m_strPeePropertyXML = dtbValue.Rows[0]["PEEPROPERTYXML"].ToString();
                    objRecordContent.m_strPeeQuantity = dtbValue.Rows[0]["PEEQUANTITY"].ToString();
                    objRecordContent.m_strPeeQuantityXML = dtbValue.Rows[0]["PEEQUANTITYXML"].ToString();
                    objRecordContent.m_strDefecateProperty = dtbValue.Rows[0]["DEFECATEPROPERTY"].ToString();
                    objRecordContent.m_strDefecatePropertyXML = dtbValue.Rows[0]["DEFECATEPROPERTYXML"].ToString();
                    objRecordContent.m_strDefecateQuantity = dtbValue.Rows[0]["DEFECATEQUANTITY"].ToString();
                    objRecordContent.m_strDefecateQuantityXML = dtbValue.Rows[0]["DEFECATEQUANTITYXML"].ToString();
                    objRecordContent.m_strLeadDirection = dtbValue.Rows[0]["LEADDIRECTION"].ToString();
                    objRecordContent.m_strLeadDirectionXML = dtbValue.Rows[0]["LEADDIRECTIONXML"].ToString();
                    objRecordContent.m_strLeadProperty = dtbValue.Rows[0]["LEADPROPERTY"].ToString();
                    objRecordContent.m_strLeadPropertyXML = dtbValue.Rows[0]["LEADPROPERTYXML"].ToString();
                    objRecordContent.m_strLeadQuantity = dtbValue.Rows[0]["LEADQUANTITY"].ToString();
                    objRecordContent.m_strLeadQuantityXML = dtbValue.Rows[0]["LEADQUANTITYXML"].ToString();
                    objRecordContent.m_strSputumProperty = dtbValue.Rows[0]["SPUTUMPROPERTY"].ToString();
                    objRecordContent.m_strSputumPropertyXML = dtbValue.Rows[0]["SPUTUMPROPERTYXML"].ToString();
                    objRecordContent.m_strSputumQuantity = dtbValue.Rows[0]["SPUTUMQUANTITY"].ToString();
                    objRecordContent.m_strSputumQuantityXML = dtbValue.Rows[0]["SPUTUMQUANTITYXML"].ToString();
                    objRecordContent.m_strSkin = dtbValue.Rows[0]["SKIN"].ToString();
                    objRecordContent.m_strSkinXML = dtbValue.Rows[0]["SKINXML"].ToString();
                    objRecordContent.m_strCaseHistory = dtbValue.Rows[0]["CASEHISTORY"].ToString();
                    objRecordContent.m_strCaseHistoryXML = dtbValue.Rows[0]["CASEHISTORYXML"].ToString();

                    objRecordContent.m_strHR = dtbValue.Rows[0]["HR"].ToString();
                    objRecordContent.m_strHR_XML = dtbValue.Rows[0]["HR_XML"].ToString();
                    objRecordContent.m_strBp2 = dtbValue.Rows[0]["BP2"].ToString();
                    objRecordContent.m_strBp2_XML = dtbValue.Rows[0]["BP2_XML"].ToString();
                    objRecordContent.m_strPower = dtbValue.Rows[0]["POWER"].ToString();
                    objRecordContent.m_strPowerXML = dtbValue.Rows[0]["POWERXML"].ToString();
                    objRecordContent.m_strStomachPipe = dtbValue.Rows[0]["STOMACHPIPE"].ToString();
                    objRecordContent.m_strStomachPipeXML = dtbValue.Rows[0]["STOMACHPIPEXML"].ToString();
                    objRecordContent.m_strInOralType = dtbValue.Rows[0]["INORALTYPE"].ToString();
                    objRecordContent.m_strInOralTypeXML = dtbValue.Rows[0]["INORALTYPEXML"].ToString();
                    objRecordContent.m_strInOralProperty = dtbValue.Rows[0]["INORALPROPERTY"].ToString();
                    objRecordContent.m_strInOralPropertyXML = dtbValue.Rows[0]["INORALPROPERTYXML"].ToString();
                    objRecordContent.m_strInOralQuantity = dtbValue.Rows[0]["INORALQUANTITY"].ToString();
                    objRecordContent.m_strInOralQuantityXML = dtbValue.Rows[0]["INORALQUANTITYXML"].ToString();
                    objRecordContent.m_strTransfusionTotal = dtbValue.Rows[0]["TRANSFUSIONTOTAL"].ToString();
                    objRecordContent.m_strTransfusionTotalXML = dtbValue.Rows[0]["TRANSFUSIONTOTALXML"].ToString();
                    objRecordContent.m_strTakeFoodTotal = dtbValue.Rows[0]["TAKEFOODTOTAL"].ToString();
                    objRecordContent.m_strTakeFoodTotalXML = dtbValue.Rows[0]["TAKEFOODTOTALXML"].ToString();
                    objRecordContent.m_strLeadPipe = dtbValue.Rows[0]["LEADPIPE"].ToString();
                    objRecordContent.m_strLeadPipeXML = dtbValue.Rows[0]["LEADPIPEXML"].ToString();
                    objRecordContent.m_strDefecateTimes = dtbValue.Rows[0]["DEFECATETIMES"].ToString();
                    objRecordContent.m_strDefecateTimesXML = dtbValue.Rows[0]["DEFECATETIMESXML"].ToString();

                    objRecordContent.m_strMachineMode_Last = dtbValue.Rows[0]["MACHINEMODE_LAST"].ToString();
                    objRecordContent.m_strBreathSoundLeft_Last = dtbValue.Rows[0]["BREATHSOUNDLEFT_LAST"].ToString();
                    objRecordContent.m_strBreathSoundRight_Last = dtbValue.Rows[0]["BREATHSOUNDRIGHT_LAST"].ToString();
                    objRecordContent.m_strT_Last = dtbValue.Rows[0]["T_LAST"].ToString();
                    objRecordContent.m_strP_Last = dtbValue.Rows[0]["P_LAST"].ToString();
                    objRecordContent.m_strR_Last = dtbValue.Rows[0]["R_LAST"].ToString();
                    objRecordContent.m_strBp_Last = dtbValue.Rows[0]["BP_LAST"].ToString();
                    objRecordContent.m_strCVP_Last = dtbValue.Rows[0]["CVP_LAST"].ToString();
                    objRecordContent.m_strBloodSugar_Last = dtbValue.Rows[0]["BLOODSUGAR_LAST"].ToString();
                    objRecordContent.m_strConsciousness_Last = dtbValue.Rows[0]["CONSCIOUSNESS_LAST"].ToString();
                    objRecordContent.m_strPupilSizeLeft_Last = dtbValue.Rows[0]["PUPILSIZELEFT_LAST"].ToString();
                    objRecordContent.m_strPupilSizeRight_Last = dtbValue.Rows[0]["PUPILSIZERIGHT_LAST"].ToString();
                    objRecordContent.m_strReflectLeft_Last = dtbValue.Rows[0]["REFLECTLEFT_LAST"].ToString();
                    objRecordContent.m_strReflectRight_Last = dtbValue.Rows[0]["REFLECTRIGHT_LAST"].ToString();
                    objRecordContent.m_strStomachDirection_Last = dtbValue.Rows[0]["STOMACHDIRECTION_LAST"].ToString();
                    objRecordContent.m_strStomachProperty_Last = dtbValue.Rows[0]["STOMACHPROPERTY_LAST"].ToString();
                    objRecordContent.m_strStomachQuantity_Last = dtbValue.Rows[0]["STOMACHQUANTITY_LAST"].ToString();
                    objRecordContent.m_strPeeDirection_Last = dtbValue.Rows[0]["PEEDIRECTION_LAST"].ToString();
                    objRecordContent.m_strPeeProperty_Last = dtbValue.Rows[0]["PEEPROPERTY_LAST"].ToString();
                    objRecordContent.m_strPeeQuantity_Last = dtbValue.Rows[0]["PEEQUANTITY_LAST"].ToString();
                    objRecordContent.m_strDefecateProperty_Last = dtbValue.Rows[0]["DEFECATEPROPERTY_LAST"].ToString();
                    objRecordContent.m_strDefecateQuantity_Last = dtbValue.Rows[0]["DEFECATEQUANTITY_LAST"].ToString();
                    objRecordContent.m_strLeadDirection_Last = dtbValue.Rows[0]["LEADDIRECTION_LAST"].ToString();
                    objRecordContent.m_strLeadProperty_Last = dtbValue.Rows[0]["LEADPROPERTY_LAST"].ToString();
                    objRecordContent.m_strLeadQuantity_Last = dtbValue.Rows[0]["LEADQUANTITY_LAST"].ToString();
                    objRecordContent.m_strSputumProperty_Last = dtbValue.Rows[0]["SPUTUMPROPERTY_LAST"].ToString();
                    objRecordContent.m_strSputumQuantity_Last = dtbValue.Rows[0]["SPUTUMQUANTITY_LAST"].ToString();
                    objRecordContent.m_strSkin_Last = dtbValue.Rows[0]["SKIN_LAST"].ToString();
                    objRecordContent.m_strCaseHistory_Last = dtbValue.Rows[0]["CASEHISTORY_LAST"].ToString();

                    objRecordContent.m_strHR_Last = dtbValue.Rows[0]["HR_LAST"].ToString();
                    objRecordContent.m_strBp2_Last = dtbValue.Rows[0]["BP2_LAST"].ToString();
                    objRecordContent.m_strPower_Last = dtbValue.Rows[0]["POWER_LAST"].ToString();
                    objRecordContent.m_strStomachPipe_Last = dtbValue.Rows[0]["STOMACHPIPE_LAST"].ToString();
                    objRecordContent.m_strTransfusionTotal_Last = dtbValue.Rows[0]["TRANSFUSIONTOTAL_LAST"].ToString();
                    objRecordContent.m_strTakeFoodTotal_Last = dtbValue.Rows[0]["TAKEFOODTOTAL_LAST"].ToString();
                    objRecordContent.m_strLeadPipe_Last = dtbValue.Rows[0]["LEADPIPE_LAST"].ToString();
                    objRecordContent.m_strDefecateTimes_Last = dtbValue.Rows[0]["DEFECATETIMES_LAST"].ToString();

                    p_objRecordContent = objRecordContent;
                    #endregion
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

            } return lngRes;
		}

		/// <summary>
		/// 查看是否有相同的记录时间
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo">若有相同记录,返回该相同记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo=null;

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
			//string strSQL = "select CreateUserID,OpenDate from WatchItemRecord Where trim(InPatientID) = ? and InPatientDate= ? and CreateDate= ? and Status=0";

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
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
		/// 保存记录到数据库。添加主表,添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			//把p_objRecordContent造型为需要使用的子类，以使用对应的字段值
			clsICUIntensiveTendContent objContent = (clsICUIntensiveTendContent)p_objRecordContent;
		
			//获取IDataParameter数组

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[95];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(95, out objDPArr);
                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
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
                objDPArr[8].Value = 0;

                objDPArr[9].Value = objContent.m_strMachineMode;
                objDPArr[10].Value = objContent.m_strMachineModeXML;
                objDPArr[11].Value = objContent.m_strBreathSoundLeft;
                objDPArr[12].Value = objContent.m_strBreathSoundLeftXML;
                objDPArr[13].Value = objContent.m_strBreathSoundRight;
                objDPArr[14].Value = objContent.m_strBreathSoundRightXML;
                objDPArr[15].Value = objContent.m_strT;
                objDPArr[16].Value = objContent.m_strT_XML;
                objDPArr[17].Value = objContent.m_strP;
                objDPArr[18].Value = objContent.m_strP_XML;
                objDPArr[19].Value = objContent.m_strR;
                objDPArr[20].Value = objContent.m_strR_XML;
                objDPArr[21].Value = objContent.m_strBp;
                objDPArr[22].Value = objContent.m_strBp_XML;
                objDPArr[23].Value = objContent.m_strCVP;
                objDPArr[24].Value = objContent.m_strCVP_XML;
                objDPArr[25].Value = objContent.m_strBloodSugar;
                objDPArr[26].Value = objContent.m_strBloodSugarXML;
                objDPArr[27].Value = objContent.m_strConsciousness;
                objDPArr[28].Value = objContent.m_strConsciousnessXML;
                objDPArr[29].Value = objContent.m_strPupilSizeLeft;
                objDPArr[30].Value = objContent.m_strPupilSizeLeftXML;
                objDPArr[31].Value = objContent.m_strPupilSizeRight;
                objDPArr[32].Value = objContent.m_strPupilSizeRightXML;
                objDPArr[33].Value = objContent.m_strReflectLeft;
                objDPArr[34].Value = objContent.m_strReflectLeftXML;
                objDPArr[35].Value = objContent.m_strReflectRight;
                objDPArr[36].Value = objContent.m_strReflectRightXML;
                objDPArr[37].Value = objContent.m_strDrugName;
                objDPArr[38].Value = objContent.m_strDrugNameXML;
                objDPArr[39].Value = objContent.m_strDrugDosage;
                objDPArr[40].Value = objContent.m_strDrugDosageXML;
                objDPArr[41].Value = objContent.m_strStomachDirection;
                objDPArr[42].Value = objContent.m_strStomachDirectionXML;
                objDPArr[43].Value = objContent.m_strStomachProperty;
                objDPArr[44].Value = objContent.m_strStomachPropertyXML;
                objDPArr[45].Value = objContent.m_strStomachQuantity;
                objDPArr[46].Value = objContent.m_strStomachQuantityXML;
                objDPArr[47].Value = objContent.m_strInOral;
                objDPArr[48].Value = objContent.m_strInOralXML;
                objDPArr[49].Value = objContent.m_strPeeDirection;
                objDPArr[50].Value = objContent.m_strPeeDirectionXML;
                objDPArr[51].Value = objContent.m_strPeeProperty;
                objDPArr[52].Value = objContent.m_strPeePropertyXML;
                objDPArr[53].Value = objContent.m_strPeeQuantity;
                objDPArr[54].Value = objContent.m_strPeeQuantityXML;
                objDPArr[55].Value = objContent.m_strDefecateProperty;
                objDPArr[56].Value = objContent.m_strDefecatePropertyXML;
                objDPArr[57].Value = objContent.m_strDefecateQuantity;
                objDPArr[58].Value = objContent.m_strDefecateQuantityXML;
                objDPArr[59].Value = objContent.m_strLeadDirection;
                objDPArr[60].Value = objContent.m_strLeadDirectionXML;
                objDPArr[61].Value = objContent.m_strLeadProperty;
                objDPArr[62].Value = objContent.m_strLeadPropertyXML;
                objDPArr[63].Value = objContent.m_strLeadQuantity;
                objDPArr[64].Value = objContent.m_strLeadQuantityXML;
                objDPArr[65].Value = objContent.m_strSputumProperty;
                objDPArr[66].Value = objContent.m_strSputumPropertyXML;
                objDPArr[67].Value = objContent.m_strSputumQuantity;
                objDPArr[68].Value = objContent.m_strSputumQuantityXML;
                objDPArr[69].Value = objContent.m_strSkin;
                objDPArr[70].Value = objContent.m_strSkinXML;
                objDPArr[71].Value = objContent.m_strCaseHistory;
                objDPArr[72].Value = objContent.m_strCaseHistoryXML;

                objDPArr[73].Value = objContent.m_strHR;
                objDPArr[74].Value = objContent.m_strHR_XML;
                objDPArr[75].Value = objContent.m_strBp2;
                objDPArr[76].Value = objContent.m_strBp2_XML;
                objDPArr[77].Value = objContent.m_strPower;
                objDPArr[78].Value = objContent.m_strPowerXML;
                objDPArr[79].Value = objContent.m_strStomachPipe;
                objDPArr[80].Value = objContent.m_strStomachPipeXML;
                objDPArr[81].Value = objContent.m_strInOralType;
                objDPArr[82].Value = objContent.m_strInOralTypeXML;
                objDPArr[83].Value = objContent.m_strInOralProperty;
                objDPArr[84].Value = objContent.m_strInOralPropertyXML;
                objDPArr[85].Value = objContent.m_strInOralQuantity;
                objDPArr[86].Value = objContent.m_strInOralQuantityXML;
                objDPArr[87].Value = objContent.m_strTransfusionTotal;
                objDPArr[88].Value = objContent.m_strTransfusionTotalXML;
                objDPArr[89].Value = objContent.m_strTakeFoodTotal;
                objDPArr[90].Value = objContent.m_strTakeFoodTotalXML;
                objDPArr[91].Value = objContent.m_strLeadPipe;
                objDPArr[92].Value = objContent.m_strLeadPipeXML;
                objDPArr[93].Value = objContent.m_strDefecateTimes;
                objDPArr[94].Value = objContent.m_strDefecateTimesXML;


                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[42];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr2.Length;i++)
                //				objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();
                p_objHRPServ.CreateDatabaseParameter(42, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strMachineMode_Last;
                objDPArr2[6].Value = objContent.m_strBreathSoundLeft_Last;
                objDPArr2[7].Value = objContent.m_strBreathSoundRight_Last;
                objDPArr2[8].Value = objContent.m_strT_Last;
                objDPArr2[9].Value = objContent.m_strP_Last;
                objDPArr2[10].Value = objContent.m_strR_Last;
                objDPArr2[11].Value = objContent.m_strBp_Last;
                objDPArr2[12].Value = objContent.m_strCVP_Last;
                objDPArr2[13].Value = objContent.m_strBloodSugar_Last;
                objDPArr2[14].Value = objContent.m_strConsciousness_Last;
                objDPArr2[15].Value = objContent.m_strPupilSizeLeft_Last;
                objDPArr2[16].Value = objContent.m_strPupilSizeRight_Last;
                objDPArr2[17].Value = objContent.m_strReflectLeft_Last;
                objDPArr2[18].Value = objContent.m_strReflectRight_Last;
                objDPArr2[19].Value = objContent.m_strStomachDirection_Last;
                objDPArr2[20].Value = objContent.m_strStomachProperty_Last;
                objDPArr2[21].Value = objContent.m_strStomachQuantity_Last;
                objDPArr2[22].Value = objContent.m_strPeeDirection_Last;
                objDPArr2[23].Value = objContent.m_strPeeProperty_Last;
                objDPArr2[24].Value = objContent.m_strPeeQuantity_Last;
                objDPArr2[25].Value = objContent.m_strDefecateProperty_Last;
                objDPArr2[26].Value = objContent.m_strDefecateQuantity_Last;
                objDPArr2[27].Value = objContent.m_strLeadDirection_Last;
                objDPArr2[28].Value = objContent.m_strLeadProperty_Last;
                objDPArr2[29].Value = objContent.m_strLeadQuantity_Last;
                objDPArr2[30].Value = objContent.m_strSputumProperty_Last;
                objDPArr2[31].Value = objContent.m_strSputumQuantity_Last;
                objDPArr2[32].Value = objContent.m_strSkin_Last;
                objDPArr2[33].Value = objContent.m_strCaseHistory_Last;

                objDPArr2[34].Value = objContent.m_strHR_Last;
                objDPArr2[35].Value = objContent.m_strBp2_Last;
                objDPArr2[36].Value = objContent.m_strPower_Last;
                objDPArr2[37].Value = objContent.m_strStomachPipe_Last;
                objDPArr2[38].Value = objContent.m_strTransfusionTotal_Last;
                objDPArr2[39].Value = objContent.m_strTakeFoodTotal_Last;
                objDPArr2[40].Value = objContent.m_strLeadPipe_Last;
                objDPArr2[41].Value = objContent.m_strDefecateTimes_Last;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //执行SQL
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                lngRes = m_lngAddNewInLiquid(p_objRecordContent, p_objHRPServ);
                if (lngRes <= 0) return lngRes;

                lngRes = m_lngAddNewInOral(p_objRecordContent, p_objHRPServ);
                if (lngRes <= 0) return lngRes;

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
            return lngRes;
		}


		/// <summary>
		/// 保存记录到数据库。添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngAddNewInLiquid(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{

			clsICUIntensiveTendContent objContent = (clsICUIntensiveTendContent)p_objRecordContent;
			if(objContent.m_objInLiquidArr == null || objContent.m_objInLiquidArr.Length <=0)
				return (long)enmOperationResult.DB_Succeed;

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                long lngEff = 0;
                for (int i1 = 0; i1 < objContent.m_objInLiquidArr.Length; i1++)
                {
                    objHRPServ.CreateDatabaseParameter(8, out objDPArr);
                    objDPArr[0].Value = objContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = objContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = objContent.m_dtmOpenDate;
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = objContent.m_dtmModifyDate;
                    objDPArr[4].Value = objContent.m_strModifyUserID;
                    objDPArr[5].Value = m_strGetMaxSeqID("ICUIntensiveTendInLiquid", objContent, p_objHRPServ);
                    if (objContent.m_objInLiquidArr[i1].m_strDrugName_Last == null)
                        objDPArr[6].Value = "";
                    else
                        objDPArr[6].Value = objContent.m_objInLiquidArr[i1].m_strDrugName_Last;
                    if (objContent.m_objInLiquidArr[i1].m_strDrugDosage_Last == null)
                        objDPArr[7].Value = "";
                    else
                        objDPArr[7].Value = objContent.m_objInLiquidArr[i1].m_strDrugDosage_Last;
                    //执行SQL
                    lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddInLiquidSQL, ref lngEff, objDPArr);
                    if (lngRes <= 0) return lngRes;
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

            } return lngRes;
		}

		/// <summary>
		/// 保存记录到数据库。添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngAddNewInOral(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			clsICUIntensiveTendContent objContent = (clsICUIntensiveTendContent)p_objRecordContent;
			if(objContent.m_objInOralArr == null || objContent.m_objInOralArr.Length <=0)
				return (long)enmOperationResult.DB_Succeed;

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                long lngEff = 0;
                for (int i1 = 0; i1 < objContent.m_objInOralArr.Length; i1++)
                {
                    objHRPServ.CreateDatabaseParameter(10, out objDPArr);
                    objDPArr[0].Value = objContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = objContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = objContent.m_dtmOpenDate;
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = objContent.m_dtmModifyDate;
                    objDPArr[4].Value = objContent.m_strModifyUserID;
                    objDPArr[5].Value = m_strGetMaxSeqID("ICUIntensiveTendInOral", objContent, p_objHRPServ);
                    if (objContent.m_objInOralArr[i1].m_strInOral_Last == null)
                        objDPArr[6].Value = "";
                    else
                        objDPArr[6].Value = objContent.m_objInOralArr[i1].m_strInOral_Last;
                    if (objContent.m_objInOralArr[i1].m_strInOralType_Last == null)
                        objDPArr[7].Value = "";
                    else
                        objDPArr[7].Value = objContent.m_objInOralArr[i1].m_strInOralType_Last;
                    if (objContent.m_objInOralArr[i1].m_strInOralProperty_Last == null)
                        objDPArr[8].Value = "";
                    else
                        objDPArr[8].Value = objContent.m_objInOralArr[i1].m_strInOralProperty_Last;
                    if (objContent.m_objInOralArr[i1].m_strInOralQuantity_Last == null)
                        objDPArr[9].Value = "";
                    else
                        objDPArr[9].Value = objContent.m_objInOralArr[i1].m_strInOralQuantity_Last;

                    //执行SQL
                    lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddInOralSQL, ref lngEff, objDPArr);
                    if (lngRes <= 0) return lngRes;
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

            } return lngRes;
		}

		[AutoComplete]
		private string m_strGetMaxSeqID(string p_strTable,clsTrackRecordContent p_objContent,clsHRPTableService p_objHRPServ)
		{
			string strSQL;
			if(p_strTable == "ICUIntensiveTendInLiquid")
				strSQL = @"select max(drugseq) as maxseq from icuintensivetendinliquid where 
				 inpatientid= ? 
				 and inpatientdate = ?
				 and opendate = ?
				 and modifydate = ?";
			else
				strSQL = @"select max(oralseq) as maxseq from icuintensivetendinoral where 
					inpatientid= ? 
					 and inpatientdate = ?
					 and opendate = ?
					 and modifydate = ?";

			DataTable dtbResult = new DataTable();

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_objContent.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));

                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			string strNewID = null;
			if(dtbResult.Rows.Count == 0)
			{
				return strNewID;
			}
			else
			{
				if(dtbResult.Rows[0][0] == System.DBNull.Value)
				{
					strNewID = "001";
				}
				else
				{
					strNewID = Convert.ToString(Convert.ToInt16(dtbResult.Rows[0][0]) + 1);
					strNewID = strNewID.ToString().PadLeft(3,'0');
				}
			}
			return strNewID;
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
			p_objModifyInfo=null;
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string c_strCheckLastModifyRecordSQL = clsDatabaseSQLConvert.s_StrTop1 + @" t2.modifydate,t2.modifyuserid from icuintensivetend t1,icuintensivetendcontent t2
					where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
					and t1.opendate = t2.opendate and t1.status =0
					and t1.inpatientid= ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //按顺序给IDataParameter赋值

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from WatchItemRecord Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

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

            finally
            {
                //objHRPServ.Dispose();

            } 
            return lngRes;	
		}

	
		/// <summary>
		///  把新修改的内容保存到数据库。
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			//把p_objRecordContent造型为需要使用的子类，以使用对应的字段值
			clsICUIntensiveTendContent objContent = (clsICUIntensiveTendContent)p_objRecordContent;

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[90];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(90, out objDPArr);
                objDPArr[0].Value = objContent.m_strMachineMode;
                objDPArr[1].Value = objContent.m_strMachineModeXML;
                objDPArr[2].Value = objContent.m_strBreathSoundLeft;
                objDPArr[3].Value = objContent.m_strBreathSoundLeftXML;
                objDPArr[4].Value = objContent.m_strBreathSoundRight;
                objDPArr[5].Value = objContent.m_strBreathSoundRightXML;
                objDPArr[6].Value = objContent.m_strT;
                objDPArr[7].Value = objContent.m_strT_XML;
                objDPArr[8].Value = objContent.m_strP;
                objDPArr[9].Value = objContent.m_strP_XML;
                objDPArr[10].Value = objContent.m_strR;
                objDPArr[11].Value = objContent.m_strR_XML;
                objDPArr[12].Value = objContent.m_strBp;
                objDPArr[13].Value = objContent.m_strBp_XML;
                objDPArr[14].Value = objContent.m_strCVP;
                objDPArr[15].Value = objContent.m_strCVP_XML;
                objDPArr[16].Value = objContent.m_strBloodSugar;
                objDPArr[17].Value = objContent.m_strBloodSugarXML;
                objDPArr[18].Value = objContent.m_strConsciousness;
                objDPArr[19].Value = objContent.m_strConsciousnessXML;
                objDPArr[20].Value = objContent.m_strPupilSizeLeft;
                objDPArr[21].Value = objContent.m_strPupilSizeLeftXML;
                objDPArr[22].Value = objContent.m_strPupilSizeRight;
                objDPArr[23].Value = objContent.m_strPupilSizeRightXML;
                objDPArr[24].Value = objContent.m_strReflectLeft;
                objDPArr[25].Value = objContent.m_strReflectLeftXML;
                objDPArr[26].Value = objContent.m_strReflectRight;
                objDPArr[27].Value = objContent.m_strReflectRightXML;
                objDPArr[28].Value = objContent.m_strDrugName;
                objDPArr[29].Value = objContent.m_strDrugNameXML;
                objDPArr[30].Value = objContent.m_strDrugDosage;
                objDPArr[31].Value = objContent.m_strDrugDosageXML;
                objDPArr[32].Value = objContent.m_strStomachDirection;
                objDPArr[33].Value = objContent.m_strStomachDirectionXML;
                objDPArr[34].Value = objContent.m_strStomachProperty;
                objDPArr[35].Value = objContent.m_strStomachPropertyXML;
                objDPArr[36].Value = objContent.m_strStomachQuantity;
                objDPArr[37].Value = objContent.m_strStomachQuantityXML;
                objDPArr[38].Value = objContent.m_strInOral;
                objDPArr[39].Value = objContent.m_strInOralXML;
                objDPArr[40].Value = objContent.m_strPeeDirection;
                objDPArr[41].Value = objContent.m_strPeeDirectionXML;
                objDPArr[42].Value = objContent.m_strPeeProperty;
                objDPArr[43].Value = objContent.m_strPeePropertyXML;
                objDPArr[44].Value = objContent.m_strPeeQuantity;
                objDPArr[45].Value = objContent.m_strPeeQuantityXML;
                objDPArr[46].Value = objContent.m_strDefecateProperty;
                objDPArr[47].Value = objContent.m_strDefecatePropertyXML;
                objDPArr[48].Value = objContent.m_strDefecateQuantity;
                objDPArr[49].Value = objContent.m_strDefecateQuantityXML;
                objDPArr[50].Value = objContent.m_strLeadDirection;
                objDPArr[51].Value = objContent.m_strLeadDirectionXML;
                objDPArr[52].Value = objContent.m_strLeadProperty;
                objDPArr[53].Value = objContent.m_strLeadPropertyXML;
                objDPArr[54].Value = objContent.m_strLeadQuantity;
                objDPArr[55].Value = objContent.m_strLeadQuantityXML;
                objDPArr[56].Value = objContent.m_strSputumProperty;
                objDPArr[57].Value = objContent.m_strSputumPropertyXML;
                objDPArr[58].Value = objContent.m_strSputumQuantity;
                objDPArr[59].Value = objContent.m_strSputumQuantityXML;
                objDPArr[60].Value = objContent.m_strSkin;
                objDPArr[61].Value = objContent.m_strSkinXML;
                objDPArr[62].Value = objContent.m_strCaseHistory;
                objDPArr[63].Value = objContent.m_strCaseHistoryXML;

                objDPArr[64].Value = objContent.m_strHR;
                objDPArr[65].Value = objContent.m_strHR_XML;
                objDPArr[66].Value = objContent.m_strBp2;
                objDPArr[67].Value = objContent.m_strBp2_XML;
                objDPArr[68].Value = objContent.m_strPower;
                objDPArr[69].Value = objContent.m_strPowerXML;
                objDPArr[70].Value = objContent.m_strStomachPipe;
                objDPArr[71].Value = objContent.m_strStomachPipeXML;
                objDPArr[72].Value = objContent.m_strInOralType;
                objDPArr[73].Value = objContent.m_strInOralTypeXML;
                objDPArr[74].Value = objContent.m_strInOralProperty;
                objDPArr[75].Value = objContent.m_strInOralPropertyXML;
                objDPArr[76].Value = objContent.m_strInOralQuantity;
                objDPArr[77].Value = objContent.m_strInOralQuantityXML;
                objDPArr[78].Value = objContent.m_strTransfusionTotal;
                objDPArr[79].Value = objContent.m_strTransfusionTotalXML;
                objDPArr[80].Value = objContent.m_strTakeFoodTotal;
                objDPArr[81].Value = objContent.m_strTakeFoodTotalXML;
                objDPArr[82].Value = objContent.m_strLeadPipe;
                objDPArr[83].Value = objContent.m_strLeadPipeXML;
                objDPArr[84].Value = objContent.m_strDefecateTimes;
                objDPArr[85].Value = objContent.m_strDefecateTimesXML;

                objDPArr[86].Value = objContent.m_strInPatientID;
                objDPArr[87].DbType = DbType.DateTime;
                objDPArr[87].Value = objContent.m_dtmInPatientDate;
                objDPArr[88].DbType = DbType.DateTime;
                objDPArr[88].Value = objContent.m_dtmOpenDate;
                objDPArr[89].Value = 0;


                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[42];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr2.Length;i++)
                //				objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(42, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strMachineMode_Last;
                objDPArr2[6].Value = objContent.m_strBreathSoundLeft_Last;
                objDPArr2[7].Value = objContent.m_strBreathSoundRight_Last;
                objDPArr2[8].Value = objContent.m_strT_Last;
                objDPArr2[9].Value = objContent.m_strP_Last;
                objDPArr2[10].Value = objContent.m_strR_Last;
                objDPArr2[11].Value = objContent.m_strBp_Last;
                objDPArr2[12].Value = objContent.m_strCVP_Last;
                objDPArr2[13].Value = objContent.m_strBloodSugar_Last;
                objDPArr2[14].Value = objContent.m_strConsciousness_Last;
                objDPArr2[15].Value = objContent.m_strPupilSizeLeft_Last;
                objDPArr2[16].Value = objContent.m_strPupilSizeRight_Last;
                objDPArr2[17].Value = objContent.m_strReflectLeft_Last;
                objDPArr2[18].Value = objContent.m_strReflectRight_Last;
                objDPArr2[19].Value = objContent.m_strStomachDirection_Last;
                objDPArr2[20].Value = objContent.m_strStomachProperty_Last;
                objDPArr2[21].Value = objContent.m_strStomachQuantity_Last;
                objDPArr2[22].Value = objContent.m_strPeeDirection_Last;
                objDPArr2[23].Value = objContent.m_strPeeProperty_Last;
                objDPArr2[24].Value = objContent.m_strPeeQuantity_Last;
                objDPArr2[25].Value = objContent.m_strDefecateProperty_Last;
                objDPArr2[26].Value = objContent.m_strDefecateQuantity_Last;
                objDPArr2[27].Value = objContent.m_strLeadDirection_Last;
                objDPArr2[28].Value = objContent.m_strLeadProperty_Last;
                objDPArr2[29].Value = objContent.m_strLeadQuantity_Last;
                objDPArr2[30].Value = objContent.m_strSputumProperty_Last;
                objDPArr2[31].Value = objContent.m_strSputumQuantity_Last;
                objDPArr2[32].Value = objContent.m_strSkin_Last;
                objDPArr2[33].Value = objContent.m_strCaseHistory_Last;

                objDPArr2[34].Value = objContent.m_strHR_Last;
                objDPArr2[35].Value = objContent.m_strBp2_Last;
                objDPArr2[36].Value = objContent.m_strPower_Last;
                objDPArr2[37].Value = objContent.m_strStomachPipe_Last;
                objDPArr2[38].Value = objContent.m_strTransfusionTotal_Last;
                objDPArr2[39].Value = objContent.m_strTakeFoodTotal_Last;
                objDPArr2[40].Value = objContent.m_strLeadPipe_Last;
                objDPArr2[41].Value = objContent.m_strDefecateTimes_Last;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                lngRes = m_lngAddNewInLiquid(p_objRecordContent, p_objHRPServ);
                if (lngRes <= 0) return lngRes;

                lngRes = m_lngAddNewInOral(p_objRecordContent, p_objHRPServ);
                if (lngRes <= 0) return lngRes;

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
            return lngRes;
		}

		/// <summary>
		/// 把记录从数据中“删除”。
		///	在编辑窗体重不用实现本方法
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
		
			//按顺序给IDataParameter赋值
		
			//执行SQL
			return (long)enmOperationResult.DB_Succeed;
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
			p_dtmModifyDate=DateTime.Now;
			p_strFirstPrintDate=null;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
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
            finally
            {
                //objHRPServ.Dispose();

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
			p_objRecordContent=null;
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

            string c_strGetDeleteRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @" a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.machinemode,
       a.machinemodexml,
       a.breathsoundleft,
       a.breathsoundleftxml,
       a.breathsoundright,
       a.breathsoundrightxml,
       a.t,
       a.t_xml,
       a.p,
       a.p_xml,
       a.r,
       a.r_xml,
       a.bp,
       a.bp_xml,
       a.cvp,
       a.cvp_xml,
       a.bloodsugar,
       a.bloodsugarxml,
       a.consciousness,
       a.consciousnessxml,
       a.pupilsizeleft,
       a.pupilsizeleftxml,
       a.pupilsizeright,
       a.pupilsizerightxml,
       a.reflectleft,
       a.reflectleftxml,
       a.reflectright,
       a.reflectrightxml,
       a.drugname,
       a.drugnamexml,
       a.drugdosage,
       a.drugdosagexml,
       a.stomachdirection,
       a.stomachdirectionxml,
       a.stomachproperty,
       a.stomachpropertyxml,
       a.stomachquantity,
       a.stomachquantityxml,
       a.inoral,
       a.inoralxml,
       a.peedirection,
       a.peedirectionxml,
       a.peeproperty,
       a.peepropertyxml,
       a.peequantity,
       a.peequantityxml,
       a.defecateproperty,
       a.defecatepropertyxml,
       a.defecatequantity,
       a.defecatequantityxml,
       a.leaddirection,
       a.leaddirectionxml,
       a.leadproperty,
       a.leadpropertyxml,
       a.leadquantity,
       a.leadquantityxml,
       a.sputumproperty,
       a.sputumpropertyxml,
       a.sputumquantity,
       a.sputumquantityxml,
       a.skin,
       a.skinxml,
       a.casehistory,
       a.casehistoryxml,
       a.hr,
       a.hr_xml,
       a.bp2,
       a.bp2_xml,
       a.power,
       a.powerxml,
       a.stomachpipe,
       a.stomachpipexml,
       a.inoraltype,
       a.inoraltypexml,
       a.inoralproperty,
       a.inoralpropertyxml,
       a.inoralquantity,
       a.inoralquantityxml,
       a.transfusiontotal,
       a.transfusiontotalxml,
       a.takefoodtotal,
       a.takefoodtotalxml,
       a.leadpipe,
       a.leadpipexml,
       a.defecatetimes,
       a.defecatetimesxml,
       b.modifydate,
       b.modifyuserid,
       b.machinemode_last,
       b.breathsoundleft_last,
       b.breathsoundright_last,
       b.t_last,
       b.p_last,
       b.r_last,
       b.bp_last,
       b.cvp_last,
       b.bloodsugar_last,
       b.consciousness_last,
       b.pupilsizeleft_last,
       b.pupilsizeright_last,
       b.reflectleft_last,
       b.reflectright_last,
       b.stomachdirection_last,
       b.stomachproperty_last,
       b.stomachquantity_last,
       b.peedirection_last,
       b.peeproperty_last,
       b.peequantity_last,
       b.defecateproperty_last,
       b.defecatequantity_last,
       b.leaddirection_last,
       b.leadproperty_last,
       b.leadquantity_last,
       b.sputumproperty_last,
       b.sputumquantity_last,
       b.skin_last,
       b.casehistory_last,
       b.hr_last,
       b.bp2_last,
       b.power_last,
       b.stomachpipe_last,
       b.transfusiontotal_last,
       b.takefoodtotal_last,
       b.leadpipe_last,
       b.defecatetimes_last
  from (select t1.inpatientid,
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
               t1.machinemode,
               t1.machinemodexml,
               t1.breathsoundleft,
               t1.breathsoundleftxml,
               t1.breathsoundright,
               t1.breathsoundrightxml,
               t1.t,
               t1.t_xml,
               t1.p,
               t1.p_xml,
               t1.r,
               t1.r_xml,
               t1.bp,
               t1.bp_xml,
               t1.cvp,
               t1.cvp_xml,
               t1.bloodsugar,
               t1.bloodsugarxml,
               t1.consciousness,
               t1.consciousnessxml,
               t1.pupilsizeleft,
               t1.pupilsizeleftxml,
               t1.pupilsizeright,
               t1.pupilsizerightxml,
               t1.reflectleft,
               t1.reflectleftxml,
               t1.reflectright,
               t1.reflectrightxml,
               t1.drugname,
               t1.drugnamexml,
               t1.drugdosage,
               t1.drugdosagexml,
               t1.stomachdirection,
               t1.stomachdirectionxml,
               t1.stomachproperty,
               t1.stomachpropertyxml,
               t1.stomachquantity,
               t1.stomachquantityxml,
               t1.inoral,
               t1.inoralxml,
               t1.peedirection,
               t1.peedirectionxml,
               t1.peeproperty,
               t1.peepropertyxml,
               t1.peequantity,
               t1.peequantityxml,
               t1.defecateproperty,
               t1.defecatepropertyxml,
               t1.defecatequantity,
               t1.defecatequantityxml,
               t1.leaddirection,
               t1.leaddirectionxml,
               t1.leadproperty,
               t1.leadpropertyxml,
               t1.leadquantity,
               t1.leadquantityxml,
               t1.sputumproperty,
               t1.sputumpropertyxml,
               t1.sputumquantity,
               t1.sputumquantityxml,
               t1.skin,
               t1.skinxml,
               t1.casehistory,
               t1.casehistoryxml,
               t1.hr,
               t1.hr_xml,
               t1.bp2,
               t1.bp2_xml,
               t1.power,
               t1.powerxml,
               t1.stomachpipe,
               t1.stomachpipexml,
               t1.inoraltype,
               t1.inoraltypexml,
               t1.inoralproperty,
               t1.inoralpropertyxml,
               t1.inoralquantity,
               t1.inoralquantityxml,
               t1.transfusiontotal,
               t1.transfusiontotalxml,
               t1.takefoodtotal,
               t1.takefoodtotalxml,
               t1.leadpipe,
               t1.leadpipexml,
               t1.defecatetimes,
               t1.defecatetimesxml
          from icuintensivetend t1
         where t1.inpatientid = ?
           and t1.inpatientdate = ?
           and t1.opendate = ?) a
 inner join (select t2.inpatientid,
                    t2.inpatientdate,
                    t2.opendate,
                    t2.modifydate,
                    t2.modifyuserid,
                    t2.machinemode_last,
                    t2.breathsoundleft_last,
                    t2.breathsoundright_last,
                    t2.t_last,
                    t2.p_last,
                    t2.r_last,
                    t2.bp_last,
                    t2.cvp_last,
                    t2.bloodsugar_last,
                    t2.consciousness_last,
                    t2.pupilsizeleft_last,
                    t2.pupilsizeright_last,
                    t2.reflectleft_last,
                    t2.reflectright_last,
                    t2.stomachdirection_last,
                    t2.stomachproperty_last,
                    t2.stomachquantity_last,
                    t2.peedirection_last,
                    t2.peeproperty_last,
                    t2.peequantity_last,
                    t2.defecateproperty_last,
                    t2.defecatequantity_last,
                    t2.leaddirection_last,
                    t2.leadproperty_last,
                    t2.leadquantity_last,
                    t2.sputumproperty_last,
                    t2.sputumquantity_last,
                    t2.skin_last,
                    t2.casehistory_last,
                    t2.hr_last,
                    t2.bp2_last,
                    t2.power_last,
                    t2.stomachpipe_last,
                    t2.transfusiontotal_last,
                    t2.takefoodtotal_last,
                    t2.leadpipe_last,
                    t2.defecatetimes_last
               from icuintensivetendcontent t2
              where t2.inpatientid = ?
                and t2.inpatientdate = ?
                and t2.opendate = ?) b on a.inpatientid = b.inpatientid
                                      and a.inpatientdate = b.inpatientdate
                                      and a.opendate = b.opendate
 where a.status = 1
 order by b.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsICUIntensiveTendContent objRecordContent = new clsICUIntensiveTendContent();
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

                    objRecordContent.m_strMachineMode = dtbValue.Rows[0]["MACHINEMODE"].ToString();
                    objRecordContent.m_strMachineModeXML = dtbValue.Rows[0]["MACHINEMODEXML"].ToString();
                    objRecordContent.m_strBreathSoundLeft = dtbValue.Rows[0]["BREATHSOUNDLEFT"].ToString();
                    objRecordContent.m_strBreathSoundLeftXML = dtbValue.Rows[0]["BREATHSOUNDLEFTXML"].ToString();
                    objRecordContent.m_strBreathSoundRight = dtbValue.Rows[0]["BREATHSOUNDRIGHT"].ToString();
                    objRecordContent.m_strBreathSoundRightXML = dtbValue.Rows[0]["BREATHSOUNDRIGHTXML"].ToString();
                    objRecordContent.m_strT = dtbValue.Rows[0]["T"].ToString();
                    objRecordContent.m_strT_XML = dtbValue.Rows[0]["T_XML"].ToString();
                    objRecordContent.m_strP = dtbValue.Rows[0]["P"].ToString();
                    objRecordContent.m_strP_XML = dtbValue.Rows[0]["P_XML"].ToString();
                    objRecordContent.m_strR = dtbValue.Rows[0]["R"].ToString();
                    objRecordContent.m_strR_XML = dtbValue.Rows[0]["R_XML"].ToString();
                    objRecordContent.m_strBp = dtbValue.Rows[0]["BP"].ToString();
                    objRecordContent.m_strBp_XML = dtbValue.Rows[0]["BP_XML"].ToString();
                    objRecordContent.m_strCVP = dtbValue.Rows[0]["CVP"].ToString();
                    objRecordContent.m_strCVP_XML = dtbValue.Rows[0]["CVP_XML"].ToString();
                    objRecordContent.m_strBloodSugar = dtbValue.Rows[0]["BLOODSUGAR"].ToString();
                    objRecordContent.m_strBloodSugarXML = dtbValue.Rows[0]["BLOODSUGARXML"].ToString();
                    objRecordContent.m_strConsciousness = dtbValue.Rows[0]["CONSCIOUSNESS"].ToString();
                    objRecordContent.m_strConsciousnessXML = dtbValue.Rows[0]["CONSCIOUSNESSXML"].ToString();
                    objRecordContent.m_strPupilSizeLeft = dtbValue.Rows[0]["PUPILSIZELEFT"].ToString();
                    objRecordContent.m_strPupilSizeLeftXML = dtbValue.Rows[0]["PUPILSIZELEFTXML"].ToString();
                    objRecordContent.m_strPupilSizeRight = dtbValue.Rows[0]["PUPILSIZERIGHT"].ToString();
                    objRecordContent.m_strPupilSizeRightXML = dtbValue.Rows[0]["PUPILSIZERIGHTXML"].ToString();
                    objRecordContent.m_strReflectLeft = dtbValue.Rows[0]["REFLECTLEFT"].ToString();
                    objRecordContent.m_strReflectLeftXML = dtbValue.Rows[0]["REFLECTLEFTXML"].ToString();
                    objRecordContent.m_strReflectRight = dtbValue.Rows[0]["REFLECTRIGHT"].ToString();
                    objRecordContent.m_strReflectRightXML = dtbValue.Rows[0]["REFLECTRIGHTXML"].ToString();
                    objRecordContent.m_strDrugName = dtbValue.Rows[0]["DRUGNAME"].ToString();
                    objRecordContent.m_strDrugNameXML = dtbValue.Rows[0]["DRUGNAMEXML"].ToString();
                    objRecordContent.m_strDrugDosage = dtbValue.Rows[0]["DRUGDOSAGE"].ToString();
                    objRecordContent.m_strDrugDosageXML = dtbValue.Rows[0]["DRUGDOSAGEXML"].ToString();
                    objRecordContent.m_strStomachDirection = dtbValue.Rows[0]["STOMACHDIRECTION"].ToString();
                    objRecordContent.m_strStomachDirectionXML = dtbValue.Rows[0]["STOMACHDIRECTIONXML"].ToString();
                    objRecordContent.m_strStomachProperty = dtbValue.Rows[0]["STOMACHPROPERTY"].ToString();
                    objRecordContent.m_strStomachPropertyXML = dtbValue.Rows[0]["STOMACHPROPERTYXML"].ToString();
                    objRecordContent.m_strStomachQuantity = dtbValue.Rows[0]["STOMACHQUANTITY"].ToString();
                    objRecordContent.m_strStomachQuantityXML = dtbValue.Rows[0]["STOMACHQUANTITYXML"].ToString();
                    objRecordContent.m_strInOral = dtbValue.Rows[0]["INORAL"].ToString();
                    objRecordContent.m_strInOralXML = dtbValue.Rows[0]["INORALXML"].ToString();
                    objRecordContent.m_strPeeDirection = dtbValue.Rows[0]["PEEDIRECTION"].ToString();
                    objRecordContent.m_strPeeDirectionXML = dtbValue.Rows[0]["PEEDIRECTIONXML"].ToString();
                    objRecordContent.m_strPeeProperty = dtbValue.Rows[0]["PEEPROPERTY"].ToString();
                    objRecordContent.m_strPeePropertyXML = dtbValue.Rows[0]["PEEPROPERTYXML"].ToString();
                    objRecordContent.m_strPeeQuantity = dtbValue.Rows[0]["PEEQUANTITY"].ToString();
                    objRecordContent.m_strPeeQuantityXML = dtbValue.Rows[0]["PEEQUANTITYXML"].ToString();
                    objRecordContent.m_strDefecateProperty = dtbValue.Rows[0]["DEFECATEPROPERTY"].ToString();
                    objRecordContent.m_strDefecatePropertyXML = dtbValue.Rows[0]["DEFECATEPROPERTYXML"].ToString();
                    objRecordContent.m_strDefecateQuantity = dtbValue.Rows[0]["DEFECATEQUANTITY"].ToString();
                    objRecordContent.m_strDefecateQuantityXML = dtbValue.Rows[0]["DEFECATEQUANTITYXML"].ToString();
                    objRecordContent.m_strLeadDirection = dtbValue.Rows[0]["LEADDIRECTION"].ToString();
                    objRecordContent.m_strLeadDirectionXML = dtbValue.Rows[0]["LEADDIRECTIONXML"].ToString();
                    objRecordContent.m_strLeadProperty = dtbValue.Rows[0]["LEADPROPERTY"].ToString();
                    objRecordContent.m_strLeadPropertyXML = dtbValue.Rows[0]["LEADPROPERTYXML"].ToString();
                    objRecordContent.m_strLeadQuantity = dtbValue.Rows[0]["LEADQUANTITY"].ToString();
                    objRecordContent.m_strLeadQuantityXML = dtbValue.Rows[0]["LEADQUANTITYXML"].ToString();
                    objRecordContent.m_strSputumProperty = dtbValue.Rows[0]["SPUTUMPROPERTY"].ToString();
                    objRecordContent.m_strSputumPropertyXML = dtbValue.Rows[0]["SPUTUMPROPERTYXML"].ToString();
                    objRecordContent.m_strSputumQuantity = dtbValue.Rows[0]["SPUTUMQUANTITY"].ToString();
                    objRecordContent.m_strSputumQuantityXML = dtbValue.Rows[0]["SPUTUMQUANTITYXML"].ToString();
                    objRecordContent.m_strSkin = dtbValue.Rows[0]["SKIN"].ToString();
                    objRecordContent.m_strSkinXML = dtbValue.Rows[0]["SKINXML"].ToString();
                    objRecordContent.m_strCaseHistory = dtbValue.Rows[0]["CASEHISTORY"].ToString();
                    objRecordContent.m_strCaseHistoryXML = dtbValue.Rows[0]["CASEHISTORYXML"].ToString();

                    objRecordContent.m_strHR = dtbValue.Rows[0]["HR"].ToString();
                    objRecordContent.m_strHR_XML = dtbValue.Rows[0]["HR_XML"].ToString();
                    objRecordContent.m_strBp2 = dtbValue.Rows[0]["BP2"].ToString();
                    objRecordContent.m_strBp2_XML = dtbValue.Rows[0]["BP2_XML"].ToString();
                    objRecordContent.m_strPower = dtbValue.Rows[0]["POWER"].ToString();
                    objRecordContent.m_strPowerXML = dtbValue.Rows[0]["POWERXML"].ToString();
                    objRecordContent.m_strStomachPipe = dtbValue.Rows[0]["STOMACHPIPE"].ToString();
                    objRecordContent.m_strStomachPipeXML = dtbValue.Rows[0]["STOMACHPIPEXML"].ToString();
                    objRecordContent.m_strInOralType = dtbValue.Rows[0]["INORALTYPE"].ToString();
                    objRecordContent.m_strInOralTypeXML = dtbValue.Rows[0]["INORALTYPEXML"].ToString();
                    objRecordContent.m_strInOralProperty = dtbValue.Rows[0]["INORALPROPERTY"].ToString();
                    objRecordContent.m_strInOralPropertyXML = dtbValue.Rows[0]["INORALPROPERTYXML"].ToString();
                    objRecordContent.m_strInOralQuantity = dtbValue.Rows[0]["INORALQUANTITY"].ToString();
                    objRecordContent.m_strInOralQuantityXML = dtbValue.Rows[0]["INORALQUANTITYXML"].ToString();
                    objRecordContent.m_strTransfusionTotal = dtbValue.Rows[0]["TRANSFUSIONTOTAL"].ToString();
                    objRecordContent.m_strTransfusionTotalXML = dtbValue.Rows[0]["TRANSFUSIONTOTALXML"].ToString();
                    objRecordContent.m_strTakeFoodTotal = dtbValue.Rows[0]["TAKEFOODTOTAL"].ToString();
                    objRecordContent.m_strTakeFoodTotalXML = dtbValue.Rows[0]["TAKEFOODTOTALXML"].ToString();
                    objRecordContent.m_strLeadPipe = dtbValue.Rows[0]["LEADPIPE"].ToString();
                    objRecordContent.m_strLeadPipeXML = dtbValue.Rows[0]["LEADPIPEXML"].ToString();
                    objRecordContent.m_strDefecateTimes = dtbValue.Rows[0]["DEFECATETIMES"].ToString();
                    objRecordContent.m_strDefecateTimesXML = dtbValue.Rows[0]["DEFECATETIMESXML"].ToString();

                    objRecordContent.m_strMachineMode_Last = dtbValue.Rows[0]["MACHINEMODE_LAST"].ToString();
                    objRecordContent.m_strBreathSoundLeft_Last = dtbValue.Rows[0]["BREATHSOUNDLEFT_LAST"].ToString();
                    objRecordContent.m_strBreathSoundRight_Last = dtbValue.Rows[0]["BREATHSOUNDRIGHT_LAST"].ToString();
                    objRecordContent.m_strT_Last = dtbValue.Rows[0]["T_LAST"].ToString();
                    objRecordContent.m_strP_Last = dtbValue.Rows[0]["P_LAST"].ToString();
                    objRecordContent.m_strR_Last = dtbValue.Rows[0]["R_LAST"].ToString();
                    objRecordContent.m_strBp_Last = dtbValue.Rows[0]["BP_LAST"].ToString();
                    objRecordContent.m_strCVP_Last = dtbValue.Rows[0]["CVP_LAST"].ToString();
                    objRecordContent.m_strBloodSugar_Last = dtbValue.Rows[0]["BLOODSUGAR_LAST"].ToString();
                    objRecordContent.m_strConsciousness_Last = dtbValue.Rows[0]["CONSCIOUSNESS_LAST"].ToString();
                    objRecordContent.m_strPupilSizeLeft_Last = dtbValue.Rows[0]["PUPILSIZELEFT_LAST"].ToString();
                    objRecordContent.m_strPupilSizeRight_Last = dtbValue.Rows[0]["PUPILSIZERIGHT_LAST"].ToString();
                    objRecordContent.m_strReflectLeft_Last = dtbValue.Rows[0]["REFLECTLEFT_LAST"].ToString();
                    objRecordContent.m_strReflectRight_Last = dtbValue.Rows[0]["REFLECTRIGHT_LAST"].ToString();
                    objRecordContent.m_strStomachDirection_Last = dtbValue.Rows[0]["STOMACHDIRECTION_LAST"].ToString();
                    objRecordContent.m_strStomachProperty_Last = dtbValue.Rows[0]["STOMACHPROPERTY_LAST"].ToString();
                    objRecordContent.m_strStomachQuantity_Last = dtbValue.Rows[0]["STOMACHQUANTITY_LAST"].ToString();
                    objRecordContent.m_strPeeDirection_Last = dtbValue.Rows[0]["PEEDIRECTION_LAST"].ToString();
                    objRecordContent.m_strPeeProperty_Last = dtbValue.Rows[0]["PEEPROPERTY_LAST"].ToString();
                    objRecordContent.m_strPeeQuantity_Last = dtbValue.Rows[0]["PEEQUANTITY_LAST"].ToString();
                    objRecordContent.m_strDefecateProperty_Last = dtbValue.Rows[0]["DEFECATEPROPERTY_LAST"].ToString();
                    objRecordContent.m_strDefecateQuantity_Last = dtbValue.Rows[0]["DEFECATEQUANTITY_LAST"].ToString();
                    objRecordContent.m_strLeadDirection_Last = dtbValue.Rows[0]["LEADDIRECTION_LAST"].ToString();
                    objRecordContent.m_strLeadProperty_Last = dtbValue.Rows[0]["LEADPROPERTY_LAST"].ToString();
                    objRecordContent.m_strLeadQuantity_Last = dtbValue.Rows[0]["LEADQUANTITY_LAST"].ToString();
                    objRecordContent.m_strSputumProperty_Last = dtbValue.Rows[0]["SPUTUMPROPERTY_LAST"].ToString();
                    objRecordContent.m_strSputumQuantity_Last = dtbValue.Rows[0]["SPUTUMQUANTITY_LAST"].ToString();
                    objRecordContent.m_strSkin_Last = dtbValue.Rows[0]["SKIN_LAST"].ToString();
                    objRecordContent.m_strCaseHistory_Last = dtbValue.Rows[0]["CASEHISTORY_LAST"].ToString();

                    objRecordContent.m_strHR_Last = dtbValue.Rows[0]["HR_LAST"].ToString();
                    objRecordContent.m_strBp2_Last = dtbValue.Rows[0]["BP2_LAST"].ToString();
                    objRecordContent.m_strPower_Last = dtbValue.Rows[0]["POWER_LAST"].ToString();
                    objRecordContent.m_strStomachPipe_Last = dtbValue.Rows[0]["STOMACHPIPE_LAST"].ToString();
                    objRecordContent.m_strTransfusionTotal_Last = dtbValue.Rows[0]["TRANSFUSIONTOTAL_LAST"].ToString();
                    objRecordContent.m_strTakeFoodTotal_Last = dtbValue.Rows[0]["TAKEFOODTOTAL_LAST"].ToString();
                    objRecordContent.m_strLeadPipe_Last = dtbValue.Rows[0]["LEADPIPE_LAST"].ToString();
                    objRecordContent.m_strDefecateTimes_Last = dtbValue.Rows[0]["DEFECATETIMES_LAST"].ToString();

                    p_objRecordContent = objRecordContent;
                    #endregion
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
			return lngRes;
		}
	}
}
