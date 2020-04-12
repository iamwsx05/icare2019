using System;
using System.EnterpriseServices;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility.SQLConvert;
using System.Data;

namespace com.digitalwave.ICUIntensiveTendRecordServ
{
	/// <summary>
	/// 危重患者护理记录,Jacky-2003-2-14
	/// 当参数中含有具体CreateDate时，说明此CreateDate的状态Status=0，故在SQL条件中若含有CreateDate可以不用考虑Status=0
	/// 否则，要考虑Status=0的情况
	/// 在统计中用到的CreateDate不是具体的时间，因为只需要精确到天
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsICUIntensiveTendRecordServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsICUIntensiveTendRecordServ()
		{}	

		[AutoComplete]
		public long m_lngUpdateFirstPrintDate(
			string p_strInPatientID,string p_strInPatientDate/*,string p_strCreateDate*/)
		{
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUIntensiveTendRecordServ","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			//更新当前病人所有对应记录的首次打印时间				
			string strCommand = "update icuintensivetendrecord set firstprintdate=?  where firstprintdate is null and status=0 and inpatientid=? and inpatientdate=? ";
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].DbType = DbType.DateTime;
            objDPArr[0].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[1].Value = p_strInPatientID;
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_strInPatientDate);

            long lngEff = -1;
            long lngRes = objHRPServ.lngExecuteParameterSQL(strCommand,ref lngEff,objDPArr);	
            //objHRPServ.Dispose();
            return lngRes;		
		}

		/// <summary>
		/// 获得主表中记录的创建时间，p_strInPatientDate的格式必须为"yyyy-MM-dd HH:mm:ss"
		/// </summary>	
		[AutoComplete]	
		public long m_lngGetAllTendRecordCreateDateArr(
			string p_strInPatientID,string p_strInPatientDate,out string p_strXML,out int p_intRows)
		{
			
			//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出Status=0的记录
			p_strXML="";
			p_intRows=0;		

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUIntensiveTendRecordServ","m_lngGetAllTendRecordCreateDateArr");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			string strCommand = "select *  from icuintensivetendrecord where status=0 and inpatientid=? and inpatientdate=? order by inpatientdate";
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		/// <summary>
		/// 查出主表信息，p_strInPatientDate和p_strCreateDate的格式必须为"yyyy-MM-dd HH:mm:ss"
		/// </summary>	
		[AutoComplete]		
		public long m_lngGetAllTendRecord(
			string p_strInPatientID,string p_strInPatientDate,out string p_strXML,out int p_intRows)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出CreateDate为指定时间的记录
			p_strXML="";
			p_intRows=0;		

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUIntensiveTendRecordServ","m_lngGetAllTendRecord");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			string strCommand = @"select a.*,b.* 				
				 from icuintensivetendrecord a,icuintensivetendrecordallcontent b where  a.inpatientid=? and a.inpatientdate=?
				 and a.inpatientid=b.inpatientid and a.inpatientdate=b.inpatientdate and a.createdate=b.createdate and a.status=0";
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);	
            //objHRPServ.Dispose();
            return lngRes;			
		}		

		/// <summary>
		/// 查出时间对应的主表信息，（作用：显示所有界面信息），p_strInPatientDate和p_strCreateDate的格式必须为"yyyy-MM-dd HH:mm:ss"
		/// </summary>	
		[AutoComplete]		
		public long m_lngGetLatestTendRecord(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out string p_strXML,out int p_intRows,out string p_strParamXML,out int p_intParamRows)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出CreateDate为指定时间的记录
			p_strXML="";
			p_intRows=0;	
			p_strParamXML="";
			p_intParamRows=0;

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUIntensiveTendRecordServ","m_lngGetLatestTendRecord");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			string strCommand = "select a.*,b.*  from icuintensivetendrecord a,icuintensivetendrecordallcontent b where  a.inpatientid=? and a.inpatientdate=? and a.createdate=?"+
				" and a.inpatientid=b.inpatientid and a.inpatientdate=b.inpatientdate and a.createdate=b.createdate ";
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_strCreateDate);

            objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);	
            //objHRPServ.Dispose();
	
			return m_lngGetLatestTendRecordParam(p_strInPatientID,p_strInPatientDate,p_strCreateDate, out p_strParamXML,out p_intParamRows);
		}

		/// <summary>
		/// 查出时间对应的参数表信息，（作用：显示界面机械参数信息）
		/// </summary>
		[AutoComplete]		
		private long m_lngGetLatestTendRecordParam(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out string p_strXML,out int p_intRows)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出CreateDate为指定时间的记录中,ModifyDate为最大修改时间的记录
			p_strXML="";
			p_intRows=0;	//无Status字段

			string strCommand = @"select a.* ,b.paramflag,b.englishdesc as paramname ,
				(select firstname from employeebaseinfo where employeeid=a.modifyuserid) as modifyusername 
				 from icuintensivetendrecordparam a ,icustandardparam b 
				 where  a.inpatientid=? and a.inpatientdate=? and a.createdate=?
                 and a.modifydate=(select max(modifydate) from icuintensivetendrecordparam where  inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and createdate=a.createdate) 
				 and a.standardparamid=b.standardparamid";
		
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_strCreateDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}


		/// <summary>
		/// 查出时间对应的从表信息,（作用：在统计时读出相关的变量）,p_strInPatientDate和p_strCreateDate的格式必须为"yyyy-MM-dd HH:mm:ss"
		/// </summary>
		[AutoComplete]		
		public long m_lngGetLatestTendRecordContent(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out string p_strXML,out int p_intRows)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出CreateDate为指定时间的记录中,ModifyDate为最大修改时间的记录
			p_strXML="";
			p_intRows=0;	//无Status字段

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUIntensiveTendRecordServ","m_lngGetLatestTendRecordContent");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			string strCommand = @"select a.*  from icuintensivetendrecordcontent a 
				 where  a.inpatientid=? and a.inpatientdate=? and a.createdate=? 
				 and a.modifydate=(select max(modifydate) from icuintensivetendrecordcontent where  inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and createdate=a.createdate) ";
				
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_strCreateDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		/// <summary>
		/// 查询创建时间p_strCreateDate对应的当天出入量统计信息，p_strInPatientDate的格式必须"yyyy-MM-dd HH:mm:ss"和p_strCreateDate的格式必须为"yyyy-MM-dd HH:mm:ss"或"yyyy-MM-dd"
		/// </summary>
		[AutoComplete]		
		public long m_lngGetStatisticInfo(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out string p_strXML,out int p_intRows)
		{
			p_strXML="";
			p_intRows=0;	//统计要求关联到Status字段，因为此时的p_strCreateDate只有精确到天的时间有效

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUIntensiveTendRecordServ","m_lngGetStatisticInfo");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			string strCommand = 
				" select "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.ind_last),0) as totalind,"+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.ini_last),0) as totalini,"+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.outu_last),0) as totaloutu,"+
				" "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.outs_last),0) as totalouts,"+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.outv_last),0) as totaloutv,"+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.oute_last),0) as totaloute,"+
				" "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.ind_last),0)+"+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.ini_last),0) as totalin,"+
				" "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.outu_last),0)+"+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.outs_last),0)+"+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.outv_last),0)+"+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum(i.oute_last),0) as totalout 
				 from icuintensivetendrecordcontent i,
				( 
				 select max(a.modifydate) as mdate,a.createdate as createdate from icuintensivetendrecordcontent a,icuintensivetendrecord main 
				 where main.inpatientid=a.inpatientid and main.inpatientdate=a.inpatientdate and main.createdate=a.createdate and main.status=0 and a.inpatientid=? and a.inpatientdate=? and a.createdate between ? and ? 
				 group by a.createdate 
				 )as base 
				 where  i.modifydate = base.mdate and i.createdate = base.createdate and 
				 i.inpatientid=? and i.inpatientdate=? ";	
				
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(6, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = DateTime.Parse(Convert.ToDateTime(p_strCreateDate).ToString("yyyy-MM-dd 00:00:00"));
            objDPArr[3].DbType = DbType.DateTime;
            objDPArr[3].Value = DateTime.Parse(Convert.ToDateTime(p_strCreateDate).ToString("yyyy-MM-dd 23:59:59"));
            objDPArr[4].Value = p_strInPatientID;
            objDPArr[5].DbType = DbType.DateTime;
            objDPArr[5].Value = DateTime.Parse(p_strInPatientDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		[AutoComplete]	
		public long m_lngGetAllTendRecordContent(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out string p_strXML,out int p_intRows)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，所有修改时间对应的结果记录（打印专用）
			p_strXML="";
			p_intRows=0;	//无Status字段

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUIntensiveTendRecordServ","m_lngGetAllTendRecordContent");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			string strCommand = @"select a.* ,
				(select firstname from employeebaseinfo where employeeid=a.modifyuserid) as modifyusername 
				from icuintensivetendrecordcontent a 
				 where  inpatientid=? and inpatientdate=? and createdate=? and 				
					( modifydate > (select firstprintdate from icuintensivetendrecord where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate 
							and createdate=a.createdate ) 
					or modifydate = 
						(select max(modifydate) from icuintensivetendrecordcontent 
							where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate 
							and createdate=a.createdate 
							and modifydate <=
										(select "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(firstprintdate,"+clsDatabaseSQLConvert.s_StrGetServDateFuncName+@") 
										from icuintensivetendrecord
										where inpatientid = a.inpatientid and inpatientdate = a.inpatientdate and 
											createdate = a.createdate and status = 0
										)
						)
					)  order by modifydate ";
            
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		[AutoComplete]	
		public long m_lngGetAllTendRecordParam(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out string p_strXML,out int p_intRows)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，所有修改时间对应的结果记录（打印专用）
			p_strXML="";
			p_intRows=0;	//无Status字段		

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUIntensiveTendRecordServ","m_lngGetAllTendRecordParam");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			string strCommand = @"Select a.* ,b.ParamFlag,b.EnglishDesc as ParamName ,
			 (select FirstName from EmployeeBaseInfo where EmployeeID=a.ModifyUserID) as ModifyUserName 
				 from ICUIntensiveTendRecordParam a ,ICUStandardParam b 
				 where  a.InPatientID=? and a.InPatientDate=? and a.CreateDate=? and 
					( ModifyDate > (select FirstPrintDate from ICUIntensiveTendRecord Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate 
							and CreateDate=a.CreateDate ) 
					or ModifyDate = 
						(select Max(ModifyDate) from ICUIntensiveTendRecordParam 
							Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate 
							and CreateDate=a.CreateDate )
					)   "+
				" and a.StandardParamID=b.StandardParamID order by b.ParamFlag,a.ModifyDate,a.StandardParamID ";

			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}


		/// <summary>
		/// 添加信息
		/// </summary>		
		[AutoComplete]	
		public long m_lngAddNew(
			string p_strMainXml,string p_strMainAllContentXml,string[] p_strParamXMLArr,string p_strSubXml)
		{
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUIntensiveTendRecordServ","m_lngAddNew");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			//添加记录时，主从表同时添加一条记录,参数表添加多条记录		
			if(p_strMainXml=="" || p_strSubXml=="")return -1;	
			clsHRPTableService objHRPServ =new clsHRPTableService();		
			long lngRes=objHRPServ.add_new_record("ICUIntensiveTendRecord",p_strMainXml); 	
			if(lngRes==1)
			{
                lngRes = objHRPServ.add_new_record("ICUIntensiveTendRecordAllContent", p_strMainAllContentXml); 
				if(lngRes==1)	
				{
                    lngRes = objHRPServ.add_new_record("ICUIntensiveTendRecordContent", p_strSubXml);
					if(p_strParamXMLArr!=null)
						for(int i=0;i<p_strParamXMLArr.Length && lngRes==1;i++)
						{
                            lngRes = objHRPServ.add_new_record("ICUIntensiveTendRecordParam", p_strParamXMLArr[i]);
						}
						
				}							
			}
            //objHRPServ.Dispose();			
			return lngRes;
		}

		/// <summary>
		/// 修改信息
		/// </summary>	
		[AutoComplete]		
		public long m_lngModify(
			string p_strMainXml,string p_strMainAllContentXml,string[] p_strParamXMLArr,string p_strSubXml)
		{
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUIntensiveTendRecordServ","m_lngModify");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			//修改记录时，主表更新原来记录中的相关XML记录，同时从表添加一条记录,参数表添加多条记录	
			if(p_strMainXml=="" || p_strSubXml=="")return -1;	
			clsHRPTableService objHRPServ =new clsHRPTableService();		
			long lngRes=objHRPServ.modify_record("ICUIntensiveTendRecord",p_strMainXml,"INPATIENTID","INPATIENTDATE","CREATEDATE"); 	
			if(lngRes==1)
			{
				lngRes=objHRPServ.modify_record("ICUIntensiveTendRecordAllContent",p_strMainAllContentXml,"INPATIENTID","INPATIENTDATE","CREATEDATE");
				if(lngRes==1)	
				{
					lngRes=objHRPServ.add_new_record("ICUIntensiveTendRecordContent",p_strSubXml); 	
					if(p_strParamXMLArr!=null)
						for(int i=0;i<p_strParamXMLArr.Length && lngRes==1;i++)
						{
                            lngRes = objHRPServ.add_new_record("ICUIntensiveTendRecordParam", p_strParamXMLArr[i]);
						}
				}
							
			}
            //objHRPServ.Dispose();		
			return lngRes;
		}


	}
}
