using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.BedCardManageServ
{
	/// <summary>
	/// clsBedCardManageServ 的摘要说明。
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsBedCardManageServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{

        //private com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHrp;

		public clsBedCardManageServ()
		{
            //objHrp = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
		}

		/// <summary>
		/// 获取床头卡信息
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetBedCardValue(ref clsBedCardValue p_objValue)
		{
			long lngRes = 0;
            clsHRPTableService objHrp = new clsHRPTableService();
            try
            {
                if (p_objValue == null)
                    return (long)enmOperationResult.Parameter_Error;
                if (p_objValue.m_strInPatientID == null || p_objValue.m_strInPatientID == "" || p_objValue.m_strInPatientDate == null || p_objValue.m_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                //string strGetValueSql = @"select * from(SELECT * FROM INPAT_BEDINFO WHERE TRIM(INPATIENTID) = '"+p_objValue.m_strInPatientID+"' AND INPATIENTDATE = "+clsHRPTableService.s_strOracleDateTime(p_objValue.m_strInPatientDate)+@" order by OPENDATE desc)where rownum = 1";

                string strGetValueSql = "";

                if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strGetValueSql = @"select inpatientid,
       inpatientdate,
       doc_incharge,
       doc_managebed,
       state,
       opendate
  from (select inpatientid,
               inpatientdate,
               doc_incharge,
               doc_managebed,
               state,
               opendate
          from inpat_bedinfo
         where inpatientid = ?
           and inpatientdate = ?
         order by opendate desc)
 where rownum = 1";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strGetValueSql = @"select top 1 inpatientid,
       inpatientdate,
       doc_incharge,
       doc_managebed,
       state,
       opendate
  from inpat_bedinfo
 where inpatientid = ?
   and inpatientdate = ?
 order by opendate desc";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strGetValueSql = @"select top 1 inpatientid,
       inpatientdate,
       doc_incharge,
       doc_managebed,
       state,
       opendate
  from inpat_bedinfo
 where inpatientid = ?
   and inpatientdate = ?
 order by opendate desc fetch first 1 row only";
                }
                IDataParameter[] objDPArr = null;
                objHrp.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objValue.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = Convert.ToDateTime(p_objValue.m_strInPatientDate);

                DataTable dtbValue = new DataTable();
                lngRes = objHrp.lngGetDataTableWithParameters(strGetValueSql, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objValue = new clsBedCardValue();
                    p_objValue.m_strInPatientID = dtbValue.Rows[0]["INPATIENTID"].ToString();
                    p_objValue.m_strInPatientDate = dtbValue.Rows[0]["INPATIENTDATE"].ToString();
                    p_objValue.m_strOpenDate = dtbValue.Rows[0]["OPENDATE"].ToString();
                    p_objValue.m_strDoc_InCharge = dtbValue.Rows[0]["DOC_INCHARGE"].ToString().Trim();
                    p_objValue.m_strDoc_ManageBed = dtbValue.Rows[0]["DOC_MANAGEBED"].ToString().Trim();
                    try
                    {
                        p_objValue.m_intState = int.Parse(dtbValue.Rows[0]["STATE"].ToString().Trim());
                    }
                    catch { p_objValue.m_intState = -1; }

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
                //objHRP.Dispose();
            }

			return lngRes;

		}
		/// <summary>
		/// 保存床头卡信息
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSaveBedCardValue(clsBedCardValue p_objValue)
		{
            long lngRes = 0;
            clsHRPTableService objHrp = new clsHRPTableService();
			try
			{
				if(p_objValue == null)
					return (long)enmOperationResult.Parameter_Error;
				if(p_objValue.m_strInPatientID == null || p_objValue.m_strInPatientID == ""|| p_objValue.m_strInPatientDate == null || p_objValue.m_strInPatientDate == ""
					|| p_objValue.m_strOpenDate == null || p_objValue.m_strOpenDate == "")
					return (long)enmOperationResult.Parameter_Error;
				string strValueSql=null;
                strValueSql = @"select inpatientid,
       inpatientdate,
       doc_incharge,
       doc_managebed,
       state,
       opendate
  from inpat_bedinfo
 where inpatientid = ?
   and inpatientdate = ?
   and doc_incharge = ?
   and doc_managebed = ?";

                IDataParameter[] objDPArr = null;
                objHrp.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_objValue.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = Convert.ToDateTime(p_objValue.m_strInPatientDate);
                objDPArr[2].Value = p_objValue.m_strDoc_InCharge;
                objDPArr[3].Value = p_objValue.m_strDoc_ManageBed;

				DataTable dtbValue = new DataTable();
                lngRes = objHrp.lngGetDataTableWithParameters(strValueSql, ref dtbValue, objDPArr);

				//如果病人归已被管床与主管医生分配则修改记录.否则新增
				if(lngRes > 0 && dtbValue.Rows.Count > 1)
				{
					lngRes = 1;
				}
				else
				{
					strValueSql = @"insert into inpat_bedinfo (inpatientid,inpatientdate,opendate,doc_incharge,doc_managebed,state) 
                        values(?,?,?,?,?,?)";

                    IDataParameter[] objLisAddItemRefArr = null;
                    objHrp.CreateDatabaseParameter(6, out objLisAddItemRefArr);
		            //Please change the datetime and reocrdid 
                    objLisAddItemRefArr[0].Value = p_objValue.m_strInPatientID;
                    objLisAddItemRefArr[1].DbType = DbType.DateTime;
                    objLisAddItemRefArr[1].Value = DateTime.Parse(p_objValue.m_strInPatientDate);
                    objLisAddItemRefArr[2].DbType = DbType.DateTime;
		            objLisAddItemRefArr[2].Value = DateTime.Parse(p_objValue.m_strOpenDate);
                    objLisAddItemRefArr[3].Value = p_objValue.m_strDoc_InCharge;
                    objLisAddItemRefArr[4].Value = p_objValue.m_strDoc_ManageBed;
                    objLisAddItemRefArr[5].Value = p_objValue.m_intState;
		            long lngRecEff = -1;
                    lngRes = objHrp.lngExecuteParameterSQL(strValueSql, ref lngRecEff, objLisAddItemRefArr);
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
            finally
            {
                //objHRP.Dispose();
            }

			return lngRes;
		}

		/// <summary>
		/// 根据床位ID查找管床医师
		/// </summary>
		/// <param name="p_strBedID"></param>
		/// <param name="p_strDoctor"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetManageDocWithBedID(string p_strBedID,out string p_strDoctorID)
		{
				p_strDoctorID = null;
			long lngRes = 0;
            clsHRPTableService objHrp = new clsHRPTableService();
			try
			{
				if(p_strBedID == null)
					return (long)enmOperationResult.Parameter_Error;

                string strValueSql = @"select bed_id, doc_managebed, area_id
  from inpat_bed_doctor
 where bed_id = ?";

                IDataParameter[] objDPArr = null;
                objHrp.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strBedID;

				DataTable dtbValue = new DataTable();
                lngRes = objHrp.lngGetDataTableWithParameters(strValueSql, ref dtbValue, objDPArr);
				if(lngRes > 0 && dtbValue.Rows.Count == 1)
				{
					p_strDoctorID = dtbValue.Rows[0]["DOC_MANAGEBED"].ToString().Trim();
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
            
            finally
            {
                //objHRP.Dispose();
            }
			return lngRes;
		}

		/// <summary>
		/// 获取床位信息(包含床位ID，床位名称，病人ID，病人名称)
		/// </summary>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_objBedInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetBedInfoByAreaID(string p_strAreaID,out clsBed_PatientInfo[] p_objBedInfoArr)
		{
				p_objBedInfoArr = null;
			long lngRes = 0;
            clsHRPTableService objHrp = new clsHRPTableService();
			try
			{
				if(p_strAreaID == null || p_strAreaID == "")
					return (long)enmOperationResult.Parameter_Error;

                string strGetBedSql = @"select inpatient_bed_desc.bed_id,
       inpatient_bed_desc.bed_name,
       ww.inpatientid,
       ww.firstname
  from inpatient_room_area
 inner join inpatient_bed_room on inpatient_room_area.room_id =
                                  inpatient_bed_room.room_id
 inner join inpatient_bed on inpatient_bed_room.bed_id =
                             inpatient_bed.bed_id
 inner join inpatient_bed_desc on inpatient_bed.bed_id =
                                  inpatient_bed_desc.bed_id
  left outer join (select indeptinfo.bed_id,
                          patientbaseinfo.inpatientid,
                          patientbaseinfo.firstname
                     from indeptinfo
                    inner join patientbaseinfo on indeptinfo.inpatientid =
                                                  patientbaseinfo.inpatientid
                    where indeptinfo.area_id = ?
                      and indeptinfo.inbedenddate = ?) ww on ww.bed_id =
                                                             inpatient_bed_desc.bed_id
 where (inpatient_room_area.area_id = ?)
   and (inpatient_bed.end_date_bed = ?)
 order by inpatient_bed_desc.bed_id";

                IDataParameter[] objDPArr = null;
                objHrp.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strAreaID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900,1,1);
                objDPArr[2].Value = p_strAreaID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = new DateTime(1900, 1, 1);

				DataTable dtbValue = new DataTable();
                lngRes = objHrp.lngGetDataTableWithParameters(strGetBedSql, ref dtbValue, objDPArr);
				if(lngRes > 0 && dtbValue.Rows.Count > 0)
				{
					p_objBedInfoArr = new clsBed_PatientInfo[dtbValue.Rows.Count];
					for(int i=0;i<p_objBedInfoArr.Length;i++)
					{
						p_objBedInfoArr[i] = new clsBed_PatientInfo();
						p_objBedInfoArr[i].m_strBedID = dtbValue.Rows[i]["BED_ID"].ToString().Trim();
						p_objBedInfoArr[i].m_strBedName = dtbValue.Rows[i]["BED_NAME"].ToString().Trim();
						p_objBedInfoArr[i].m_strInPatientID = dtbValue.Rows[i]["INPATIENTID"].ToString().Trim();
						p_objBedInfoArr[i].m_strInPatientName = dtbValue.Rows[i]["FIRSTNAME"].ToString().Trim();
					}
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
            finally
            {
                //objHRP.Dispose();
            }
			return lngRes;

		}

		/// <summary>
		/// 根据床位ID获取病人ID和姓名
		/// </summary>
		/// <param name="p_strBedID"></param>
		/// <param name="p_objBedInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientByBedID(string p_strAreaID,string p_strBedID,ref clsBed_PatientInfo p_objBedInfo)
		{
			long lngRes2 = 0;
            clsHRPTableService objHrp = new clsHRPTableService();
			try
			{
				if(p_strBedID == null || p_strBedID == "" || p_objBedInfo == null)
					return (long)enmOperationResult.Parameter_Error;
				string strSql = @"select patientbaseinfo.inpatientid, patientbaseinfo.firstname, 
      indeptinfo.inbedenddate
from indeptinfo inner join
      patientbaseinfo on 
      indeptinfo.inpatientid = patientbaseinfo.inpatientid
where indeptinfo.area_id = ? and indeptinfo.bed_id = ? and 
      indeptinfo.inbedenddate = ?";

                IDataParameter[] objDPArr = null;
                objHrp.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strAreaID;
                objDPArr[1].Value = p_strBedID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = new DateTime(1900, 1, 1);
			
				DataTable dtbValue2 = new DataTable();
                lngRes2 = objHrp.lngGetDataTableWithParameters(strSql, ref dtbValue2, objDPArr);
				if(lngRes2 > 0 && dtbValue2.Rows.Count == 1)
				{
					p_objBedInfo.m_strInPatientID = dtbValue2.Rows[0]["INPATIENTID"].ToString().Trim();
					p_objBedInfo.m_strInPatientName = dtbValue2.Rows[0]["FIRSTNAME"].ToString().Trim();
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
            finally
            {
                //objHRP.Dispose();
            }
			return lngRes2;
		}

		/// <summary>
		/// 添加床位－医生对应信息
		/// </summary>
		/// <param name="p_strBedID"></param>
		/// <param name="p_strManageDoc"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddBed_ManageDoc(string p_strBedID,string p_strManageDoc,string p_strAreaID)
		{
			long lngRes = 0;
            clsHRPTableService objHrp = new clsHRPTableService();
			try
			{
				if(p_strBedID == null || p_strBedID == "")
					return (long)enmOperationResult.Parameter_Error;
				string strSql = @"insert into inpat_bed_doctor (bed_id,doc_managebed,area_id) values(?,?,?)";

                IDataParameter[] objDPArr = null;
                objHrp.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strBedID;
                objDPArr[1].Value = p_strManageDoc;
                objDPArr[2].Value = p_strAreaID;

                long lngEff = -1;
                lngRes = objHrp.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
            finally
            {
                //objHRP.Dispose();
            }
			return lngRes;
		}

		/// <summary>
		/// 获取床位－医生对应信息
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetBed_ManageDoc(string p_strAreaID,out clsBed_ManageDocValue[] p_objValueArr)
		{
				p_objValueArr = null;
			long lngRes = 0;
            clsHRPTableService objHrp = new clsHRPTableService();
			try
			{
				if(p_strAreaID == null || p_strAreaID == "")
					return (long)enmOperationResult.Parameter_Error;

				string strGetDocSql = @"select distinct doc_managebed from inpat_bed_doctor where area_id = '"+p_strAreaID+"'";
				string strGetBedIDSql = @"select bed_id from inpat_bed_doctor where area_id = ? and doc_managebed = ?";

                IDataParameter[] objDPArr = null;
                objHrp.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strAreaID;

				DataTable dtbValue = new DataTable();
                lngRes = objHrp.lngGetDataTableWithParameters(strGetDocSql, ref dtbValue, objDPArr);
				if(lngRes > 0 && dtbValue.Rows.Count > 0)
				{
					p_objValueArr = new clsBed_ManageDocValue[dtbValue.Rows.Count];
					for(int i=0;i<p_objValueArr.Length;i++)
					{
						p_objValueArr[i] = new clsBed_ManageDocValue();
						p_objValueArr[i].m_strManageDocID = dtbValue.Rows[i]["DOC_MANAGEBED"].ToString().Trim();

                        objHrp.CreateDatabaseParameter(2, out objDPArr);
                        objDPArr[0].Value = p_strAreaID;
                        objDPArr[1].Value = p_objValueArr[i].m_strManageDocID;

						DataTable dtbValue2 = new DataTable();
                        long lngRes2 = objHrp.lngGetDataTableWithParameters(strGetBedIDSql, ref dtbValue2, objDPArr);
						if(lngRes2 > 0 && dtbValue2.Rows.Count > 0)
						{
							p_objValueArr[i].m_strBedIDArr = new string[dtbValue2.Rows.Count];
							for(int j2=0;j2<dtbValue2.Rows.Count;j2++)
							{
								p_objValueArr[i].m_strBedIDArr[j2] = dtbValue2.Rows[j2]["BED_ID"].ToString().Trim();
							}
						}
					}
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
            finally
            {
                //objHRP.Dispose();
            }
			return lngRes;
		}
		/// <summary>
		///根据病区ID删除床位－医生对应信息
		/// </summary>
		/// <param name="p_strAreaID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteBed_ManageDo(string p_strAreaID)
		{
			long lngRes = 0;
            clsHRPTableService objHrp = new clsHRPTableService();
			try
			{
				if(p_strAreaID == null || p_strAreaID == "")
					return (long)enmOperationResult.Parameter_Error;
				string strDeleteSql = @"delete from inpat_bed_doctor where area_id = ?";

                IDataParameter[] objDPArr = null;
                objHrp.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strAreaID;

                long lngEff = -1;
                lngRes = objHrp.lngExecuteParameterSQL(strDeleteSql, ref lngEff, objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
            finally
            {
                //objHRP.Dispose();
            }
			return lngRes;
		}
	}
}
