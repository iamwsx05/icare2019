using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data; 

namespace com.digitalwave.DiseaseTrackService
{
	/// <summary>
	/// 实现特殊记录的中间件。
	/// 一般护理记录
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsGeneralNurseRecordService	: clsDiseaseTrackService
	{

		/// <summary>
		/// 从GeneralNurseRecord获取指定病人的所有没有删除记录的时间。
		/// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		/// </summary>
		private const string c_strGetTimeListSQL= "select createdate,opendate from generalnurserecord where inpatientid = ? and inpatientdate= ? and status=0";
		/// <summary>
		/// 根据指定表单的信息，从GeneralNurseRecord和GeneralNurseRecordContent查找表单的内容。
		/// 用InPatientID ,InPatientDate ,CreateDate,Status = 0等条件，查询该记录的内容，查找Max(ModifyDate)。
		/// 如果返回lngRes = 1 && rows = 0,则证明此记录已被他人删除掉。
		/// </summary>
        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
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
       a.recordcontent,
       a.recordcontentxml,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.recordcontent_right,
       b.pagination,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,generalnurserecord a
                 where e.empno_chr = a.createuserid
                   and e.status_int <> -1
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = a.createuserid
           and rownum = 1) firstname
  from generalnurserecord a, generalnurserecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from generalnurserecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
		/// <summary>
		/// 从GeneralNurseRecord中获取指定时间的表单,获取已经存在记录的主要信息
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= "select createuserid,opendate from generalnurserecord where inpatientid = ? and inpatientdate= ? and createdate= ? and status=0";

		/// <summary>
		/// 从GeneralNurseRecord获取已经存在记录的主要信息,获取修改表单的主要信息
		/// </summary>
//		private const string c_strGetExistInfoSQL= "";

		/// <summary>
		/// 从GeneralNurseRecordContent获取指定表单的最后修改时间。
		/// </summary>
		private const string c_strCheckLastModifyRecordSQL= @"select b.modifydate,b.modifyuserid from generalnurserecord a,generalnurserecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from generalnurserecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";
				
		/// <summary>
		/// 从GeneralNurseRecordContent获取修改表单的主要信息。
		/// </summary>
//		private const string c_strGetModifyRecordSQL= "";

		/// <summary>
		/// 从GeneralNurseRecord获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL= "select deactiveddate,deactivedoperatorid from generalnurserecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1";

		/// <summary>
		/// 添加记录到GeneralNurseRecord
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into  generalnurserecord(inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,recordcontent,recordcontentxml) 
				values(?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 添加记录到GeneralNurseRecordContent
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into  generalnurserecordcontent(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,recordcontent_right) 
				values(?,?,?,?,?,?)";

		
		/// <summary>
		/// 修改记录到GeneralNurseRecordContent
		/// </summary>
		private const string c_strModifyRecordSQL= "update generalnurserecord set recordcontent=?,recordcontentxml=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";//IfConfirm=? ,ConfirmReason=? ,ConfirmReasonXML=?";

		/// <summary>
		/// 修改记录到GeneralNurseRecordContent
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// 设置GeneralNurseRecord中删除记录的信息
		/// </summary>
		private const string c_strDeleteRecordSQL= "update generalnurserecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

		/// <summary>
		/// 从GeneralNurseRecord和GeneralNurseRecordContent获取LastModifyDate和FirstPrintDate
		/// </summary>
		private const string c_strGetModifyDateAndFirstPrintDateSQL=  @"select a.firstprintdate,b.modifydate from generalnurserecord a,generalnurserecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from generalnurserecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";
						
		/// <summary>
		/// 更新GeneralNurseRecord中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= "update  generalnurserecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		/// <summary>
		/// 从GeneralNurseRecord获取指定病人的所有指定删除者删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= "select createdate,opendate from generalnurserecord where inpatientid = ? and inpatientdate= ? and deactivedoperatorid= ? and status=1";

		/// <summary>
		/// 在GeneralNurseRecordContent中获取指定表单的信息。
		/// </summary>
        private const string c_strGetDeleteRecordContentSQL = @"select a.inpatientid,
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
       a.recordcontent,
       a.recordcontentxml,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.recordcontent_right,
       b.pagination
  from generalnurserecord a, generalnurserecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 1
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from generalnurserecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";

		/// <summary>
		/// 从GeneralNurseRecord获取指定病人的所有已经删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= "select createdate,opendate from generalnurserecord where inpatientid = ? and inpatientdate= ? and status=1";

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

			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
                ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsGeneralNurseRecordService","m_lngGetRecordTimeList");
                ////if(lngCheckRes <= 0)
                //    //return lngCheckRes;	

				//检查参数
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
					return (long)enmOperationResult.Parameter_Error;
		
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(2,out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);

				//生成DataTable
				DataTable dtbValue = new DataTable();

				//执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);

				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_strCreateDateArr = new string[dtbValue.Rows.Count];
					p_strOpenDateArr = new string[dtbValue.Rows.Count];
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						//设置结果
						p_strCreateDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
						p_strOpenDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
					}				
				}
				//返回
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    objHRPServ.Dispose();
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
			long lngCheckRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
                //lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsGeneralNurseRecordService","m_lngUpdateFirstPrintDate");
                ////if(lngCheckRes <= 0)
                //    //return lngCheckRes;	

				//检查参数                              
				if(p_strInPatientID==null || p_strInPatientID==""||p_strInPatientDate==null || p_strInPatientDate==""||p_strOpenDate==null || p_strOpenDate=="")
					return (long)enmOperationResult.Parameter_Error;
			
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(4,out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=DateTime.Parse(p_strOpenDate);
			
				//执行SQL
				long lngEff=0;
                lngCheckRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    objHRPServ.Dispose();
                }

                return lngCheckRes;
						
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

			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
                ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsGeneralNurseRecordService","m_lngGetDeleteRecordTimeListAll");
                ////if(lngCheckRes <= 0)
                //    //return lngCheckRes;	

				//检查参数
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
					return (long)enmOperationResult.Parameter_Error;
		
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(3,out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
				objDPArr[2].Value=p_strDeleteUserID;
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_strCreateDateArr = new string[dtbValue.Rows.Count];
					p_strOpenDateArr = new string[dtbValue.Rows.Count];
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						//设置结果
						p_strCreateDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
						p_strOpenDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
					}				
				}
				//返回
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    objHRPServ.Dispose();
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
				p_strCreateDateArr=null;
				p_strOpenDateArr=null;
			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{

                ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsGeneralNurseRecordService","m_lngGetDeleteRecordTimeListAll");
                ////if(lngCheckRes <= 0)
                //    //return lngCheckRes;	

				//检查参数
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
					return (long)enmOperationResult.Parameter_Error;
		
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(2,out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_strCreateDateArr = new string[dtbValue.Rows.Count];
					p_strOpenDateArr = new string[dtbValue.Rows.Count];
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						//设置结果
						p_strCreateDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
						p_strOpenDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
					}				
				}
				//返回
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    objHRPServ.Dispose();
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
				p_objRecordContent=null;
			long lngRes = 0;
			try
			{
			
				//检查参数
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
		
				IDataParameter[] objDPArr = null;
				new clsHRPTableService().CreateDatabaseParameter(3,out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL,ref dtbValue,objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
                    DataRow objRow = dtbValue.Rows[0];
					//设置结果
					clsGeneralNurseRecordContent objRecordContent=new clsGeneralNurseRecordContent();
					objRecordContent.m_strInPatientID=p_strInPatientID;
					objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate=DateTime.Parse(p_strOpenDate);
					objRecordContent.m_dtmCreateDate=DateTime.Parse(objRow["CREATEDATE"].ToString());
					objRecordContent.m_dtmModifyDate=DateTime.Parse(objRow["MODIFYDATE"].ToString());
				
					if(objRow["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(objRow["FIRSTPRINTDATE"].ToString());
					objRecordContent.m_strCreateUserID=objRow["CREATEUSERID"].ToString();
					objRecordContent.m_strModifyUserID=objRow["MODIFYUSERID"].ToString();

					objRecordContent.m_strSignName = objRow["FIRSTNAME"].ToString();

					if(objRow["IFCONFIRM"].ToString()=="")
						objRecordContent.m_bytIfConfirm=0;
					else objRecordContent.m_bytIfConfirm=Byte.Parse(objRow["IFCONFIRM"].ToString());
					if(objRow["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else objRecordContent.m_bytStatus=Byte.Parse(objRow["STATUS"].ToString());
					objRecordContent.m_strConfirmReason=objRow["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=objRow["CONFIRMREASONXML"].ToString();
				
					objRecordContent.m_strRecordContent_Right=objRow["RECORDCONTENT_RIGHT"].ToString();
					objRecordContent.m_strRecordContent=objRow["RECORDCONTENT"].ToString();
					objRecordContent.m_strRecordContentXml=objRow["RECORDCONTENTXML"].ToString();
							
					p_objRecordContent=	objRecordContent;		
				}
				//返回
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
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
				p_objModifyInfo=null;
			long lngRes = 0;
			try
			{

				//检查参数
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
		
				IDataParameter[] objDPArr = null;
				new clsHRPTableService().CreateDatabaseParameter(3,out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=p_objRecordContent.m_dtmCreateDate;
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL,ref dtbValue,objDPArr);
					
				//查看DataTable.Rows.Count
				//如果等于1，表示已经有该CreateDate，并且不是删除的记录。
				//获取该记录的信息，赋值到p_objModifyInfo中。返回值使用Record_Already_Exist
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					p_objModifyInfo=new clsPreModifyInfo();
					p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["CREATEUSERID"].ToString();
					p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["OPENDATE"].ToString());
					return (long)enmOperationResult.Record_Already_Exist;
				}
				//返回	
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
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
			try
			{
				//检查参数                              
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
				//把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
				clsGeneralNurseRecordContent objContent = (clsGeneralNurseRecordContent)p_objRecordContent;
		
				IDataParameter[] objDPArr = null;
				new clsHRPTableService().CreateDatabaseParameter(11,out objDPArr);

                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=objContent.m_dtmCreateDate;
				objDPArr[4].Value=objContent.m_strCreateUserID;
				objDPArr[5].Value=objContent.m_bytIfConfirm;
				if(objContent.m_strConfirmReason==null)
					objDPArr[6].Value=DBNull.Value;
				else
					objDPArr[6].Value=objContent.m_strConfirmReason;
				if(objContent.m_strConfirmReasonXML==null)
					objDPArr[7].Value=DBNull.Value;
				else 
					objDPArr[7].Value=objContent.m_strConfirmReasonXML;
				objDPArr[8].Value=0;
				objDPArr[9].Value=objContent.m_strRecordContent;
				objDPArr[10].Value=objContent.m_strRecordContentXml;
			
				//执行SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				IDataParameter[] objDPArr2 = null;
				p_objHRPServ.CreateDatabaseParameter(6,out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objContent.m_dtmModifyDate;
				objDPArr2[4].Value=objContent.m_strModifyUserID;			
				objDPArr2[5].Value=objContent.m_strRecordContent_Right;
			
				//执行SQL			
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL,ref lngEff,objDPArr2);
				if(lngRes<=0)return lngRes;
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
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
				p_objModifyInfo=null;
			long lngRes = 0;
			try
			{
				//检查参数
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=p_objRecordContent.m_dtmOpenDate;
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL,ref dtbValue,objDPArr);
			        
		
				//如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
				if(lngRes > 0 && dtbValue.Rows.Count ==0)
				{
					lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL,ref dtbValue,objDPArr);
				
					if(lngRes>0 && dtbValue.Rows.Count ==1)
					{
						p_objModifyInfo=new clsPreModifyInfo();
						p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
						p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
					}
					return (long)enmOperationResult.Record_Already_Delete;
				}	
					//从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
				else if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					//如果相同，返回DB_Succees
                    //if(p_objRecordContent.m_dtmModifyDate==DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
						return (long)enmOperationResult.DB_Succeed;

				}	
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
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
			long lngRes = 0;
			try
			{
				//检查参数
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
				//把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
				clsGeneralNurseRecordContent objContent = (clsGeneralNurseRecordContent)p_objRecordContent;
		
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(5,out objDPArr);

				objDPArr[0].Value=objContent.m_strRecordContent;
				objDPArr[1].Value=objContent.m_strRecordContentXml;
                objDPArr[2].Value = objContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value=objContent.m_dtmOpenDate;		
			
			
				//执行SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				IDataParameter[] objDPArr2 = null;
				//按顺序给IDataParameter赋值
				p_objHRPServ.CreateDatabaseParameter(6,out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objContent.m_dtmModifyDate;
				objDPArr2[4].Value=objContent.m_strModifyUserID;			
				objDPArr2[5].Value=objContent.m_strRecordContent_Right;
			
				//执行SQL			
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL,ref lngEff,objDPArr2);
				if(lngRes<=0)return lngRes;
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
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
			long lngRes = 0;
			try
			{
				//检查参数
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_objRecordContent.m_dtmDeActivedDate;
				objDPArr[1].Value=p_objRecordContent.m_strDeActivedOperatorID;			
		
				//执行SQL
				long lngEff=0;
				lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL,ref lngEff,objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
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
				p_dtmModifyDate=DateTime.Now;
				p_strFirstPrintDate=null;
			long lngRes = 0;
			try
			{
				//检查参数
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
		
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL,ref dtbValue,objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					//设置结果
					p_strFirstPrintDate=dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
					p_dtmModifyDate=DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());								
				}
				//返回
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
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
				p_objRecordContent=null;
			long lngRes = 0;
			try
			{
				//检查参数
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
		
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL,ref dtbValue,objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
                    DataRow objRow = dtbValue.Rows[0];
					//设置结果
					clsGeneralNurseRecordContent objRecordContent=new clsGeneralNurseRecordContent();
					objRecordContent.m_strInPatientID=p_strInPatientID;
					objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate=DateTime.Parse(p_strOpenDate);
					objRecordContent.m_dtmCreateDate=DateTime.Parse(objRow["CREATEDATE"].ToString());
					objRecordContent.m_dtmModifyDate=DateTime.Parse(objRow["MODIFYDATE"].ToString());
				
					if(objRow["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(objRow["FIRSTPRINTDATE"].ToString());
					objRecordContent.m_strCreateUserID=objRow["CREATEUSERID"].ToString();
					objRecordContent.m_strModifyUserID=objRow["MODIFYUSERID"].ToString();
					objRecordContent.m_bytIfConfirm=Byte.Parse(objRow["IFCONFIRM"].ToString());
					objRecordContent.m_bytStatus=Byte.Parse(objRow["STATUS"].ToString());
					objRecordContent.m_strConfirmReason=objRow["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=objRow["CONFIRMREASONXML"].ToString();
				
					objRecordContent.m_strRecordContent_Right=objRow["RECORDCONTENT_RIGHT"].ToString();
					objRecordContent.m_strRecordContent=objRow["RECORDCONTENT"].ToString();
					objRecordContent.m_strRecordContentXml=objRow["RECORDCONTENTXML"].ToString();
				
					p_objRecordContent=	objRecordContent;		
				}
				//返回
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
        }
	}// END CLASS DEFINITION clsGeneralNurseRecordService

}
