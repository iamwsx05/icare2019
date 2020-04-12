using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.PACSService
{
	/// <summary>
	/// Summary description for clsPACSService.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsPACSService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		//不允许有实例变量。所有函数的返回值都是long，传出值用out
		private const string m_strGetImageReportSQL=@"select applicationid,inpatientid,applicationtype,reportcheckname,
													requestdatetime,reportdesc,reportdiagnose,reportdatetime,reportorname,reportcheckpart
													 from ps_imageapplication where inpatientid=? order by requestdatetime desc";

		private const string m_strGetImageBookingSQL=@"select applicationid,inpatientid,applicationtype,
													 requestdatetime,bookingresult,applicationtype,replydatedatetime
													  from ps_imageapplication where inpatientid=? order by requestdatetime desc";

		private string m_InsertRecordSQL=@"insert into ps_imageapplication
											(applicationid, inpatientid, patientname, patientsex, patientbirth,
											deptid, deptname,bedname, doctorid, doctorname, diagnose, checkpurpose,
											checkpart,applicationinfo, applicationtype, requestdatetime,ifneedrequire,ifrequired,applicationcomment)
											 values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,0,?)";

		private string m_UpdateRecordSQL=@"update ps_imageapplication set 
			inpatientid=?, patientname=?, patientsex=?, patientbirth=?,
			deptid=?, deptname=?,bedname=?, doctorid=?, doctorname=?, diagnose=?, checkpurpose=?,
			checkpart=?,applicationinfo=?, applicationtype=?, requestdatetime=? where applicationid=?";
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_ImageReport"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetImageReportByPatientID(string p_strInPatientID,out ImageReport[] p_ImageReport)
		{
			p_ImageReport = new ImageReport[0];
			clsHRPTableService objHRPServer=new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			objHRPServer.CreateDatabaseParameter(1,out objDPArr);

 			objDPArr[0].Value=p_strInPatientID;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetImageReportSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_ImageReport = new ImageReport[dtbValue.Rows.Count];
                    for (int i1 = 0; i1 < dtbValue.Rows.Count; i1++)
                    {
                        //设置结果
                        p_ImageReport[i1] = new ImageReport();
                        p_ImageReport[i1].m_strPatientID = dtbValue.Rows[i1]["INPATIENTID"].ToString().Trim();
                        p_ImageReport[i1].m_strCheckType = dtbValue.Rows[i1]["APPLICATIONTYPE"].ToString().Trim();
                        p_ImageReport[i1].m_strCheckName = dtbValue.Rows[i1]["REPORTCHECKNAME"].ToString().Trim();
                        p_ImageReport[i1].m_strReportDiagnose = dtbValue.Rows[i1]["REPORTDIAGNOSE"].ToString().Trim();
                        p_ImageReport[i1].m_strReportDesc = dtbValue.Rows[i1]["REPORTDESC"].ToString().Trim();
                        if (dtbValue.Rows[i1]["REPORTDATETIME"] != DBNull.Value)
                            p_ImageReport[i1].m_dteReportDateTime = (DateTime)dtbValue.Rows[i1]["REPORTDATETIME"];
                        else
                            p_ImageReport[i1].m_dteReportDateTime = DateTime.MinValue;

                        if (dtbValue.Rows[i1]["REQUESTDATETIME"] != DBNull.Value)
                            p_ImageReport[i1].m_dteRequestDateTime = (DateTime)dtbValue.Rows[i1]["REQUESTDATETIME"];
                        else
                            p_ImageReport[i1].m_dteRequestDateTime = DateTime.MinValue;


                        p_ImageReport[i1].m_strReportorName = dtbValue.Rows[i1]["REPORTORNAME"].ToString().Trim();
                        p_ImageReport[i1].m_strApplicationID = dtbValue.Rows[i1]["APPLICATIONID"].ToString().Trim();
                        p_ImageReport[i1].m_strReportCheckPart = dtbValue.Rows[i1]["REPORTCHECKPART"].ToString().Trim();
   lngRes = (long)enmOperationResult.DB_Succeed;
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

            }		//返回
         
            return lngRes;
  
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_ImageBooking"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetImageBookingByPatientID(string p_strInPatientID,out ImageBooking[] p_ImageBooking)
		{
			p_ImageBooking=new ImageBooking[0];

			clsHRPTableService objHRPServer=new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			objHRPServer.CreateDatabaseParameter(1,out objDPArr);
 			objDPArr[0].Value=p_strInPatientID;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetImageBookingSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_ImageBooking = new ImageBooking[dtbValue.Rows.Count];
                    for (int i1 = 0; i1 < dtbValue.Rows.Count; i1++)
                    {
                        //设置结果
                        p_ImageBooking[i1] = new ImageBooking();
                        p_ImageBooking[i1].m_strPatientID = dtbValue.Rows[i1]["INPATIENTID"].ToString().Trim();
                        p_ImageBooking[i1].m_strCheckType = dtbValue.Rows[i1]["APPLICATIONTYPE"].ToString().Trim();
                        p_ImageBooking[i1].m_strBookingInfo = dtbValue.Rows[i1]["BOOKINGRESULT"].ToString().Trim();
                        if (dtbValue.Rows[i1]["REPLYDATEDATETIME"] != System.DBNull.Value)
                            p_ImageBooking[i1].m_dteBookingReplyDate = (DateTime)dtbValue.Rows[i1]["REPLYDATEDATETIME"];
                        else
                            p_ImageBooking[i1].m_dteBookingReplyDate = DateTime.MinValue;

                        if (dtbValue.Rows[i1]["REQUESTDATETIME"] != System.DBNull.Value)
                            p_ImageBooking[i1].m_dteRequestDateTime = (DateTime)dtbValue.Rows[i1]["REQUESTDATETIME"];
                        else
                            p_ImageBooking[i1].m_dteRequestDateTime = DateTime.MinValue;


                        p_ImageBooking[i1].m_strApplicationID = dtbValue.Rows[i1]["APPLICATIONID"].ToString().Trim();
 lngRes = (long)enmOperationResult.DB_Succeed;

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
			//返回
           
            return lngRes;

			
		}

		/// <summary>
		/// 添加一条新记录
		/// </summary>
		/// <param name="p_objApplicationInfo"></param>
		/// <param name="p_strApplicationID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewPACSApplication(ImageRequest p_objApplicationInfo,out string p_strApplicationID)
		{
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                long lngAffectedRows = 0;
                string m_strNextApplicationID;

                  lngRes = m_lngGeneralApplicationID(out m_strNextApplicationID);

                if (lngRes <= 0)
                {
                    p_strApplicationID = "";
                    return lngRes;
                }
                else
                {
                    //最新ID
                    p_objApplicationInfo.m_strApplicationID = m_strNextApplicationID;

                     IDataParameter[] objDPArr = null;
                     objHRPServ.CreateDatabaseParameter(18, out objDPArr);

                    objDPArr[0].Value = p_objApplicationInfo.m_strApplicationID;
                    objDPArr[1].Value = p_objApplicationInfo.m_strInPatientID;
                    objDPArr[2].Value = p_objApplicationInfo.m_strPatientName;
                    objDPArr[3].Value = p_objApplicationInfo.m_strPatientSex;
                    objDPArr[4].DbType = DbType.DateTime;
                    if (p_objApplicationInfo.m_strPatientBirth != "")
                        objDPArr[4].Value = DateTime.Parse(DateTime.Parse(p_objApplicationInfo.m_strPatientBirth).ToString("yyyy-MM-dd HH:mm:ss"));
                    else
                        objDPArr[4].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[5].Value = p_objApplicationInfo.m_strDeptID;
                    objDPArr[6].Value = p_objApplicationInfo.m_strDeptName;
                    objDPArr[7].Value = p_objApplicationInfo.m_strBedName;
                    objDPArr[8].Value = p_objApplicationInfo.m_strDoctorID;
                    objDPArr[9].Value = p_objApplicationInfo.m_strDoctorName;
                    objDPArr[10].Value = p_objApplicationInfo.m_strDiagnose;
                    objDPArr[11].Value = p_objApplicationInfo.m_strCheckPurpose;
                    objDPArr[12].Value = p_objApplicationInfo.m_strCheckPart;
                    objDPArr[13].Value = p_objApplicationInfo.m_strApplicationInfo;
                    objDPArr[14].Value = p_objApplicationInfo.m_strApplicationType;
                    objDPArr[15].DbType = DbType.DateTime;
                    objDPArr[15].Value = DateTime.Parse(DateTime.Parse(p_objApplicationInfo.m_strRequestDateTime).ToString("yyyy-MM-dd HH:mm:ss"));
                    //					objDPArr[16].DbType = DbType.Boolean;
                    objDPArr[16].Value = (p_objApplicationInfo.m_blnIfNeedRequire == true ? 1 : 0);
                    objDPArr[17].Value = p_objApplicationInfo.m_strApplicationComment;

                    DataTable dtbValue = new DataTable();


                    long lngRe = new clsHRPTableService().lngExecuteParameterSQL(m_InsertRecordSQL, ref lngAffectedRows, objDPArr);
                    if (lngRe > 0 && lngAffectedRows > 0)
                    {
                        p_strApplicationID = m_strNextApplicationID;
                        lngRes= 1;
                    }
                    else
                    {
                        p_strApplicationID = "";
                        lngRes= 0;
                    }

                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                p_strApplicationID = "";
                return 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
		}

	
		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="p_objApplicationInfo"></param>
		/// <param name="p_strApplicationID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdatePACSApplication(ImageRequest p_objApplicationInfo,string p_strApplicationID)
		{
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                long lngAffectedRows = 0;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(16, out objDPArr);
                objDPArr[0].Value = p_objApplicationInfo.m_strInPatientID;
                objDPArr[1].Value = p_objApplicationInfo.m_strPatientName;
                objDPArr[2].Value = p_objApplicationInfo.m_strPatientSex;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(DateTime.Parse(p_objApplicationInfo.m_strPatientBirth).ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[4].Value = p_objApplicationInfo.m_strDeptID;
                objDPArr[5].Value = p_objApplicationInfo.m_strDeptName;
                objDPArr[6].Value = p_objApplicationInfo.m_strBedName;
                objDPArr[7].Value = p_objApplicationInfo.m_strDoctorID;
                objDPArr[8].Value = p_objApplicationInfo.m_strDoctorName;
                objDPArr[9].Value = p_objApplicationInfo.m_strDiagnose;
                objDPArr[10].Value = p_objApplicationInfo.m_strCheckPurpose;
                objDPArr[11].Value = p_objApplicationInfo.m_strCheckPart;
                objDPArr[12].Value = p_objApplicationInfo.m_strApplicationInfo;
                objDPArr[13].Value = p_objApplicationInfo.m_strApplicationType;
                objDPArr[14].DbType = DbType.DateTime;
                objDPArr[14].Value = DateTime.Parse(DateTime.Parse(p_objApplicationInfo.m_strRequestDateTime).ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[15].Value = p_strApplicationID;

                DataTable dtbValue = new DataTable();


                 lngRes = objHRPServ.lngExecuteParameterSQL(m_UpdateRecordSQL, ref lngAffectedRows, objDPArr);
                if (lngRes > 0 && lngAffectedRows > 0)
                    return 1;
                else
                    return 0;

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
		}
		
	
		/// <summary>
		/// 计算当前最大申请单号
		/// </summary>
		/// <param name="p_strApplicationID"></param>
		/// <returns></returns>
		[AutoComplete] 
		public long m_lngGeneralApplicationID(out string p_strApplicationID)
		{
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                 string strGetMaxID = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strGetMaxID = @"select max(cast(right(applicationid, len(applicationid) - 1) as bigint)) as maxapplicationid from ps_imageapplication";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    strGetMaxID = "select nvl(max(applicationid),0) as applicationid from ps_imageapplication";
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                    strGetMaxID = "select nvl(max(applicationid),0) as applicationid from ps_imageapplication";



                string mMaxApplicationID = "";
                string mNextApplicationID = "";
                long mMax = 0;

                DataTable dtbValue = new DataTable();

                  lngRes = objHRPServ.DoGetDataTable(strGetMaxID, ref dtbValue);
                if (lngRes > 0)
                {
                    //返回了最大值
                    if (dtbValue.Rows.Count > 0 && dtbValue.Rows[0][0].ToString() != "")
                    {
                        mMaxApplicationID = dtbValue.Rows[0][0].ToString();
                        if (mMaxApplicationID.StartsWith("P"))
                            mMaxApplicationID = mMaxApplicationID.Remove(0, 1);
                        mMax = long.Parse(mMaxApplicationID) + 1;
                    }
                    //没有返回值，但也没有出错。数据表中还没有记录
                    else
                    {
                        mMax = 1;
                    }
                    if (mMax < 100000)
                    {
                        mNextApplicationID = "P" + mMax.ToString("00000");
                    }
                    else
                    {
                        mNextApplicationID = "P" + mMax.ToString();
                    }
                    p_strApplicationID = mNextApplicationID;
                    lngRes= 1;
                }
                else
                {
                    p_strApplicationID = "";
                    lngRes= 0;
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                //				throw new Exception("获取当前最大申请单号失败！");
                p_strApplicationID = "";
                return 0;

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
		}
	}
}
