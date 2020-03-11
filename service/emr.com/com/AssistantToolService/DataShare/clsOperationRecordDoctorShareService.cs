using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.DataShareService
{
	/// <summary>
	/// 手术记录单数据共享
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsOperationRecordDoctorShareService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 获取手术数据，只为住院病案首页提供共享
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetBaseOperationValue(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = @"select b.operationbegindate,b.operationname,b.anaesthesiacategorydosage,b.anaesther,
								b.operationdoctorname,b.firstassistantname,b.secondassistantname,b.operationdoctorid,b.firstassistantid,b.secondassistantid
								from operationrecorddoctor a,
								operationrecordcontendoctor b,
								(select max(lastmodifydate) as lastmodifydate,inpatientid,inpatientdate,opendate
								from operationrecordcontendoctor
								group by inpatientid,inpatientdate,opendate
								)base
								where a.inpatientid=?
								and a.inpatientdate=?
								and a.status=0 
								and a.inpatientid=b.inpatientid
								and a.inpatientdate=b.inpatientdate
								and a.opendate = b.opendate
								and base.inpatientid=b.inpatientid
								and base.inpatientdate=b.inpatientdate
								and base.opendate = b.opendate
								and base.lastmodifydate = b.lastmodifydate
								order by a.createdate
								";
			#endregion SQL

			p_dtbResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr); 
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
		
		}
        /// <summary>
        /// 获取所有的病程记录日期作为树的内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPaitentID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllDiseaseRecord(
            string p_strInPaitentID, string p_strInPatientDate, out DataTable p_dtbResult)
        {
            #region SQL
            //string strSQL = "select createdate from(select  distinct a.createdate	from GeneralDiseaseRecord a,  GeneralDiseaseRecordContent b where  InPatientID ='" + p_strInPaitentID + @"'  and a.InPatientDate = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + @"					    and  a.Status=0 and a.InPatientID=b.InPatientID and a.InPatientDate=b.InPatientDate and a.OpenDate = b.OpenDate)  order by createdate";
            //string strSQL = "select distinct a.createdate,a.recordtitle,b.recordcontent_right  from GeneralDiseaseRecord a,GeneralDiseaseRecordContent b  where   a.InPatientID ='" + p_strInPaitentID + @"'  and a.InPatientDate = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + @" and  a.Status=0 AND b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and	b.ModifyDate=( select Max(ModifyDate) from GeneralDiseaseRecordContent  Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
//            string strSQL = @"select distinct a.createdate, a.recordtitle, b.recordcontent_right
//                              from GeneralDiseaseRecord a, GeneralDiseaseRecordContent b
//                             where a.InPatientID = ?
//                               AND a.inpatientid = b.inpatientid
//                               and a.InPatientDate =?
//                               and a.Status = 0
//                               AND b.InPatientDate = a.InPatientDate
//                               and b.OpenDate = a.OpenDate
//                               and b.ModifyDate = (select Max(ModifyDate)
//                                                     from GeneralDiseaseRecordContent
//                                                    Where InPatientID = a.InPatientID
//                                                      and InPatientDate = a.InPatientDate
//                                                      and OpenDate = a.OpenDate)";
            string strSQL = @"select a.createdate, a.recordtitle, b.recordcontent_right
  from generaldiseaserecord a, generaldiseaserecordcontent b
 where a.inpatientid = ?
   and a.inpatientid = b.inpatientid
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from generaldiseaserecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)
 order by a.createdate desc";
            #endregion SQL

            p_dtbResult = new DataTable();



            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;		    

        }

		/// <summary>
		/// 获取最新的病程记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestDiseaseRecord(
			string p_strInPaitentID,string p_strInPatientDate, string p_strCreateDate,out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" a.recordtitle,b.recordcontent_right
							from generaldiseaserecord a,generaldiseaserecordcontent b
							where a.inpatientid = ?
							and a.inpatientdate = ?
                            and a.createdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc" +clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);


                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
		}
		/// <summary>
		/// 获取最新的出院记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetOutHospitalShareValue(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" b.heartid_right,b.xrayid_right,
							b.inhospitaldiagnose_right,b.outhospitaldiagnose_right,
							b.inhospitalcase_right,b.inhospitalby_right,b.outhospitalcase_right,b.outhospitaladvice_right
							from outhospitalrecord a,outhospitalrecordcontent b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
		}
		/// <summary>
		/// 获取最新的交班记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestHandOverRecord(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" b.originaldiagnose_right,b.currentdiagnose_right,b.casehistory_right,b.referral_right
							from handoverrecord a,handoverrecordcontent b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
		}
		/// <summary>
		/// 获取最新的接班记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestTakeOverRecord(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" b.originaldiagnose_right,b.currentdiagnose_right,b.casehistory_right,b.referral_right
							from takeoverrecord a,takeoverrecordcontent b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
		}
		/// <summary>
		/// 获取最新的转出
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestConveyRecord(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" b.originaldiagnose_right,b.conveydiagnose_right,b.casehistory_right,b.consultation_right,b.conveyreason_right
                            ,b.notice_right  from  conveyrecord a, conveyrecordcontent b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
		}
		/// <summary>
		/// 获取最新的转入
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestTurnInRecord(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@"  b.casebeforeturnin_right,b.turninreason_right,b.turnindiagnose_right,
                                b.caseafterturnin_right,b.referral_right  from  turninrecord a, turninrecordcontent b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
		}
		/// <summary>
		/// 获取最新的阶段小结
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestDiseaseSummaryRecord(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@"   b.diagnoseby_right,b.inhospitalcase_right,
                            b.inhospitaldiagnose_right,b.currentcase_right,
                            b.currentdiagnose_right,b.referral_right  from  diseasesummaryrecord  a, diseasesummaryrecordcontent  b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
		}
		/// <summary>
		/// 获取最新的查房记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
        public long m_lngGetAllCheckRoomRecord(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
//            string strSQL = clsDatabaseSQLConvert.s_StrTop1 + @"   b.Patientstate_Right,b.Differentiatediagnose_Right,
//                             b.Diagnose_Right,b.Currentcure_Right,b.Nextcure_Right,a.CreateDate
//                             from  CheckRoomRecord   a, CheckRoomRecordContent  b
//							where a.InPatientID = ?
//							and a.InPatientDate = ?
//							and a.Status=0 
//							and a.InPatientID=b.InPatientID
//							and a.InPatientDate=b.InPatientDate
//							and a.OpenDate = b.OpenDate
//							order by a.CreateDate desc,b.ModifyDate desc" + clsDatabaseSQLConvert.s_StrRownum;
            string strSQL = @"select   b.patientstate_right,b.differentiatediagnose_right,
                             b.diagnose_right,b.currentcure_right,b.nextcure_right,a.createdate
                             from  checkroomrecord   a, checkroomrecordcontent  b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
                            and b.modifydate = (select max(modifydate)
                                                     from checkroomrecordcontent
                                                    where inpatientid = a.inpatientid
                                                      and inpatientdate = a.inpatientdate
                                                      and opendate = a.opendate)
							order by a.createdate desc,b.modifydate desc";
			#endregion SQL

			p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;			
		}
		/// <summary>
		/// 获取最新的病案讨论
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestCaseDiscussRecord(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@"   b.address_right,b.discusscontent_right
                             from  casediscussrecord   a, casediscussrecordcontent   b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;			
		}
		/// <summary>
		/// 获取最新的手术前讨论
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestBeforeOperationDiscussRecord(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@"   b.address_right,b.discusscontent_right
                            from  beforeoperationdiscussrecord a, bfoprdiscussreccont b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;			
		}
		/// <summary>
		/// 获取最新的死亡讨论
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestDeadCaseDiscussRecord(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@"  b.address_right,b.discusscontent_right,b.deaddiagnose_right,b.deadreason_right,b.useforreference_right 
                             from  deadcasediscussrecord    a, deadcasediscussrecordcontent    b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;			
		}
		/// <summary>
		/// 获取最新的死亡记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestDeadRecord(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@"   b.inhospitalcase_right,b.originaldiagnose_right,b.diagnoseby_right,b.deaddiagnose_right,b.deadreason_right,
                             b.experience_right
                             from  deadrecord    a, deadrecordcontent     b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;			
		}

        /// <summary>
        /// 获取最新的会诊记录回复
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPaitentID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllConsultation(
            string p_strInPaitentID, string p_strInPatientDate, out DataTable p_dtbResult)
        {
            #region SQL
            string strSQL = @"select a.createdate,
       b.casehistory_right,
       b.consultationorder_right,
       b.consultationidea_right
  from consultationrecord a, consultationrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
   and b.modifydate = (select max(modifydate)
                         from consultationrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)
 order by a.createdate desc";
            #endregion SQL

            p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

		/// <summary>
		/// 获取最新的抢救记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestSaveRecord(string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+ @"   b.diseasename_right,b.diseasechangecase_right,
                             b.savedeal_right,b.saveresult_right,b.attendpeople_right
                             from  saverecord   a, saverecordcontent      b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;			
		}
		/// <summary>
		/// 获取最近的手术数据，只为手术后病程记录提供共享
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strRecordDate">记录日期，获取次日期之前最近的信息</param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestOperationInfo(
			string p_strInPaitentID,string p_strInPatientDate,string p_strRecordDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" b.operationbegindate,b.anaesthesiacategorydosage,b.operationname,b.diagnoseafteroperation,b.operationprocess
from operationrecorddoctor a,operationrecordcontendoctor b
where a.inpatientid = ?
and a.inpatientdate = ?
and a.status=0 
and a.inpatientid=b.inpatientid
and a.inpatientdate=b.inpatientdate
and a.opendate = b.opendate
and b.operationbegindate <= ?
order by a.createdate desc,b.lastmodifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strRecordDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;			
		}

        /// <summary>
        /// 获取最新的手术后病程记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPaitentID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLatestAfterOPRecord( string p_strInPaitentID, string p_strInPatientDate, out DataTable p_dtbResult)
        {
            #region SQL
            string strSQL = clsDatabaseSQLConvert.s_StrTop1 + @"   b.anaesthesiamode_right,b.operationname_right,
                             b.operationdiagnose_right,b.inoperationseeing_right,b.afteroperationdeal_right,
                             b.afteroperationnotice_right,b.cuthealupstatus_right
                             from  afteroperationrecord   a, afteroperationrecordcontent      b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
							and a.inpatientid=b.inpatientid
							and a.inpatientdate=b.inpatientdate
							and a.opendate = b.opendate
							order by a.createdate desc,b.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;
            #endregion SQL

            p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        /// <summary>
        /// 获取所有的手术后病程记录日期作为树的内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPaitentID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_dtbResult">内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllAfterOPRecord( string p_strInPaitentID, string p_strInPatientDate, out DataTable p_dtbResult)
        {
            #region SQL
            string strSQL = @"select a.createdate,
       b.anaesthesiamode_right,
       b.operationname_right,
       b.operationdiagnose_right,
       b.inoperationseeing_right,
       b.afteroperationdeal_right,
       b.afteroperationnotice_right,
       b.cuthealupstatus_right
  from afteroperationrecord a, afteroperationrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
   and b.modifydate = (select max(modifydate)
                         from afteroperationrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)
 order by a.createdate desc";
            #endregion SQL

            p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取最新的术前小结
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPaitentID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLatestSummaryBeforeOP( string p_strInPaitentID, string p_strInPatientDate, out DataTable p_dtbResult)
        {
            #region SQL
            string strSQL = @" select  b.diseasesummary_right,b.diagnosisbeforeop_right,
                             b.diagnosisgist_right,b.opindication_right,b.opmode_right,
                             b.anamode_right,b.proceeding_right,b.preparebeforeop_right
                             from  t_emr_summarybeforeop   a, t_emr_summarybeforeopcon      b
							where a.inpatientid = ?
							and a.inpatientdate = ?
							and a.status=0 
                            and b.status = 0
							and a.emr_seq=b.emr_seq";
            #endregion SQL

            p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        /// <summary>
        /// 获取所有术前小结记录日期作为树的内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPaitentID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_dtbResult">内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllSummaryBeforeOP( string p_strInPaitentID, string p_strInPatientDate, out DataTable p_dtbResult)
        {
            #region SQL
            string strSQL = @"select a.recorddate,
       b.diseasesummary_right,
       b.diagnosisbeforeop_right,
       b.diagnosisgist_right,
       b.opindication_right,
       b.opmode_right,
       b.anamode_right,
       b.proceeding_right,
       b.preparebeforeop_right
  from t_emr_summarybeforeop a, t_emr_summarybeforeopcon b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.status = 0
   and a.emr_seq = b.emr_seq
 order by a.recorddate desc";
            #endregion SQL

            p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
    }
}
