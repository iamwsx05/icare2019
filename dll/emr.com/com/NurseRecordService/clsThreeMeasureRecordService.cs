using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.ThreeMeasureRecordService
{
	/// <summary>
	/// 的中间件�?

	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsThreeMeasureRecordService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strResultXml">返回的结�?/param>
		/// <param name="p_intResultRows">记录的数�?/param>
		/// <returns>
		/// 操作结果
		/// 0：失败�?

		/// 1：成功�?

		/// </returns>
		[AutoComplete]
		public long m_lngGetPatientRecord(
			string p_strInPatientID,string p_strInPatientDate,ref string p_strResultXml,ref int p_intResultRows)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsThreeMeasureRecordService","m_lngGetPatientRecord");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

            string strSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createid,
       a.specialdatexml,
       a.eventxml,
       a.breathxml,
       a.inputxml,
       a.dejectaxml,
       a.peexml,
       a.outstreamxml,
       a.pressurexml,
       a.weightxml,
       a.skintestxml,
       a.otherxml,
       a.pulsexml,
       a.temperaturexml,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxmlstring,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.pressure2xml,
       a.othername,
       a.stayoutxml,
       b.modifydate
  from threemeasurerecord a, threemeasurerecordcontent b
 where a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
   and b.modifydate in (select max(c.modifydate)
                          from threemeasurerecordcontent c
                         where c.inpatientid = ?
                           and c.inpatientdate = ?
                         group by c.opendate)
   and a.status = 0
   and a.inpatientid = ?
   and a.inpatientdate = ?
 order by a.createdate";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(4, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[2].Value = p_strInPatientID;
            objDPArr[3].DbType = DbType.DateTime;
            objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);

            return objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);
		}		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strMainXml"></param>
		/// <returns>
		/// 操作结果�?

		/// 0：失败�?

		/// 1：成功�?

		/// </returns>
		[AutoComplete]
		public long m_lngAddNew(
			string p_strMainXml,string p_strContentXml,string [] p_strContentAccessXmlArr,string [] p_strContentEventXmlArr,bool p_blnIsAddNew)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsThreeMeasureRecordService","m_lngAddNew");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;


            
			
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
 		    if(p_blnIsAddNew)
			{
                lngRes = objHRPServ.add_new_record("ThreeMeasureRecord", p_strMainXml);			
			}
			else
			{
                lngRes = objHRPServ.modify_record("ThreeMeasureRecord", p_strMainXml, "INPATIENTID", "INPATIENTDATE", "OPENDATE");
			}

			if(lngRes > 0)
			{
                lngRes = objHRPServ.add_new_record("ThreeMeasureRecordContent", p_strContentXml);
			}

			int intIndex = 0;
			while(lngRes > 0 && intIndex < p_strContentAccessXmlArr.Length)
			{
                lngRes = objHRPServ.add_new_record("ThreeMeasureRecContAccess", p_strContentAccessXmlArr[intIndex]);

				intIndex++;
			}

			intIndex = 0;
			while(lngRes > 0 && intIndex < p_strContentEventXmlArr.Length)
			{
                lngRes = objHRPServ.add_new_record("ThreeMeasureRecordContentEvent", p_strContentEventXmlArr[intIndex]);

				intIndex++;
			} 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_blnCheckResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckNewCreateDate(string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out bool p_blnIsAddNew)
		{

			int intCount;

			p_blnIsAddNew = false;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsThreeMeasureRecordService","m_lngCheckNewCreateDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			long lngRes = m_lngGetCreateDateCount( p_strInPatientID,p_strInPatientDate,p_strCreateDate,out intCount);

			if(lngRes <= 0)
			{
				p_blnIsAddNew = false;
				return lngRes;
			}
			else
			{
				if(intCount > 0)
					p_blnIsAddNew = false;
				else
					p_blnIsAddNew = true;

				return lngRes;
			}
		}
/// <summary>
/// 
/// </summary>
/// <param name="p_objPrincipal"></param>
/// <param name="p_strInPatientID"></param>
/// <param name="p_strInPatientDate"></param>
/// <param name="p_strCreateDate"></param>
/// <param name="p_intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetCreateDateCount(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out int p_intRows)
		{
			p_intRows = 0;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsThreeMeasureRecordService","m_lngGetCreateDateCount");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;


			long lngRes = 0;
			PublicMiddleTier.clsPublicMiddleTier objHRPServ = new PublicMiddleTier.clsPublicMiddleTier();
			try
			{
				lngRes=objHRPServ.m_lngGetCreateDateCount("ThreeMeasureRecord", p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_intRows);


		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_blnCheckResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckLastModifyDate(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,string p_strLastModifyDate,out bool p_blnIsLast,out bool p_blnIsDelete,out string p_strChangedUserID,out string p_strChangedDate)
		{
			p_blnIsDelete = false;
			p_blnIsLast = false;
			p_strChangedUserID = null;
			p_strChangedDate = null;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsThreeMeasureRecordService","m_lngCheckLastModifyDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			long lngRes = m_lngGetLastModifyDate( p_strInPatientID,p_strInPatientDate,p_strOpenDate,out p_strChangedDate,out p_strChangedUserID);

			if(lngRes <= 0)
			{
				p_blnIsLast = false;
				p_blnIsDelete = true;
				p_strChangedUserID = null;
				p_strChangedDate = null;
				return lngRes;
			}
			else
			{
				if(p_strChangedDate == null)
				{
					p_blnIsDelete = true;
					p_blnIsLast = false;

					lngRes = m_lngGetDeactiveInfo(p_strInPatientID,p_strInPatientDate,p_strOpenDate,out p_strChangedDate,out p_strChangedUserID);

					if(lngRes <= 0)
					{
						p_blnIsLast = false;
						p_blnIsDelete = true;
						p_strChangedUserID = null;
						p_strChangedDate = null;
						return lngRes;
					}
				}
				else
				{
					p_blnIsDelete = false;

					p_blnIsLast = p_strChangedDate == p_strLastModifyDate;
				}

				return lngRes;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strDeactiveDate"></param>
		/// <param name="p_strDeactiveUserID"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngGetDeactiveInfo(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strDeactiveDate,out string p_strDeactiveUserID)
		{

			p_strDeactiveDate = null;
			p_strDeactiveUserID= null;

			if(p_strInPatientID == null || p_strInPatientID == "")
				return -1;
			if(p_strInPatientDate == null || p_strInPatientDate == "")
				return -1;
			if(p_strOpenDate == null || p_strOpenDate == "")
				return -1;
			
			DataTable m_dtbResult = new DataTable();
			string strCommand=@"select deactiveddate,deactivedoperatorid
									from threemeasurerecord 
									where inpatientid = ?
									and inpatientdate = ?
									and opendate = ?
									and status = 1
									";
           
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
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref m_dtbResult, objDPArr); 
 		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
 			if(lngRes > 0)
			{
				if(m_dtbResult.Rows.Count <=0)
				{
					p_strDeactiveDate = "";
					p_strDeactiveUserID = "";
				}
				else
				{
					if(m_dtbResult.Rows[0][0] == null || m_dtbResult.Rows[0][0].ToString() == "")
					{
						p_strDeactiveDate = "";
						p_strDeactiveUserID = "";
					}
					else
					{
						p_strDeactiveDate = ((DateTime)m_dtbResult.Rows[0][0]).ToString("yyyy-MM-dd HH:mm:ss");
						p_strDeactiveUserID = m_dtbResult.Rows[0][1].ToString();
					}
				}
			}
			return lngRes;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strLastModifyDate"></param>
		/// <param name="p_strLastModifyName"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLastModifyDate(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strLastModifyDate,out string p_strLastModifyUserID)
		{
			p_strLastModifyDate = null;
			p_strLastModifyUserID= null;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsThreeMeasureRecordService","m_lngGetLastModifyDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			if(p_strInPatientID == null || p_strInPatientID == "")
				return -1;
			if(p_strInPatientDate == null || p_strInPatientDate == "")
				return -1;
			if(p_strOpenDate == null || p_strOpenDate == "")
				return -1;
			
			DataTable m_dtbResult = new DataTable();
			string strCommand=clsDatabaseSQLConvert.s_StrTop1+ @" modifydate,modifyuserid from threemeasurerecord 
				t1,threemeasurerecordcontent t2 where  
				t1.status = 0 and t1.inpatientid = t2.inpatientid  
				and t1.inpatientdate = t2.inpatientdate  
				and t1.opendate = t2.opendate  and t1.inpatientid = ? 
				and t1.inpatientdate = ? and 
				t1.opendate=?  
				order by modifydate desc "+clsDatabaseSQLConvert.s_StrRownum;

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
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref m_dtbResult, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
             if(lngRes > 0)
			{
				if(m_dtbResult.Rows.Count <=0)
				{
					p_strLastModifyDate = null;
					p_strLastModifyUserID = null;
				}
				else
				{
					if(m_dtbResult.Rows[0][0] == null || m_dtbResult.Rows[0][0].ToString() == "")
					{
						p_strLastModifyDate = null;
						p_strLastModifyUserID = null;
					}
					else
					{
						p_strLastModifyDate = DateTime.Parse(m_dtbResult.Rows[0][0].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
						p_strLastModifyUserID = m_dtbResult.Rows[0][1].ToString();
					}
				}
			}
			return lngRes;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strOperatorID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteRecord(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,string p_strOperatorID)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsThreeMeasureRecordService","m_lngDeleteRecord");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;


			long lngRes = 0;
			PublicMiddleTier.clsPublicMiddleTier objHRPServ = new PublicMiddleTier.clsPublicMiddleTier();
			try
			{
				lngRes=	objHRPServ.m_lngDeleteRecord( "ThreeMeasureRecord",p_strInPatientID,p_strInPatientDate,p_strOpenDate,p_strOperatorID);


		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strTableName"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSetFirstPrintDate(string[] p_strInPatientIDArr,string[] p_strInPatientDateArr,string[] p_strOpenDateArr)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsThreeMeasureRecordService","m_lngSetFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;


			long lngRes = 0;
			PublicMiddleTier.clsPublicMiddleTier objHRPServ = new PublicMiddleTier.clsPublicMiddleTier();
			try
			{
				lngRes=objHRPServ.m_lngSetFirstPrintDate(p_strInPatientIDArr, p_strInPatientDateArr, p_strOpenDateArr, "ThreeMeasureRecord");


		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDateArr"></param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetInpatientDateList(string p_strInPatientID,out string[] p_strInPatientDateArr)
		{
			long lngRes= 0 ;
			p_strInPatientDateArr=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select inpatientdate  from inpatientdateinfo  where (inpatientid = ?) order by inpatientdate desc";
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege("clsThreeMeasureRecordService", "m_lngSetFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                
                DataTable dtbValue = new DataTable();
                
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                if (lngRes > 0)
                    p_strInPatientDateArr = new string[dtbValue.Rows.Count];
                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    p_strInPatientDateArr[i] = dtbValue.Rows[i]["InPatientDate"].ToString();
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

        [AutoComplete]
        public bool m_blnCheckTValueExistBySelectTime(string p_strInpatientId,string p_strInpatientDate,string p_strCreateTime)
        {
            if (string.IsNullOrEmpty(p_strInpatientId) || string.IsNullOrEmpty(p_strInpatientDate) || string.IsNullOrEmpty(p_strCreateTime))
                return false;
            string strSql = @"select t.temperaturevalue
  from threemeasurereccontaccess t
 inner join (select max(modifydate) as modifydate
               from threemeasurereccontaccess
              where inpatientid = ?
                and inpatientdate = ?
                and createtime = ?) m on t.modifydate = m.modifydate
 where t.inpatientid = ?
   and t.inpatientdate = ?
   and t.createtime = ?";
            
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInpatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateTime);
                objDPArr[3].Value = p_strInpatientId;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInpatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strCreateTime);

                DataTable dtbValue = new DataTable();

                long lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtbValue.Rows[0][0].ToString()))
                        return true;
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
            return false;
        }
	}
}