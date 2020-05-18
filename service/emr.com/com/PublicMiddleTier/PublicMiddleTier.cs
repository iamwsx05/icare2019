using System;
using System.EnterpriseServices;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using weCare.Core.Entity;
using System.Collections.Generic;

namespace com.digitalwave.PublicMiddleTier
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPublicMiddleTier : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        //[AutoComplete]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCheckReptXML"></param>
        /// <returns></returns>
        [AutoComplete]
        public long add_new_record(string strCheckReptXML)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (strCheckReptXML == null || strCheckReptXML == "")
                    return -1;
                lngRes = objHRPServ.add_new_record("PatientCheckedRept", strCheckReptXML);
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
        /// 
        /// </summary>
        /// <param name="strBedID"></param>
        /// <param name="strXML"></param>
        /// <param name="intRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetICUEquipmentByBedID(
            string strBedID, ref string strXML, ref int intRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPublicMiddleTier", "lngGetICUEquipmentByBedID");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                if (strBedID == null || strBedID == "")
                    return -1;

                string strSQL = @"select type,make,model from icuequipment 
                                    where eid in 
                                        (select distinct eid from equipmentbed 
                                                where bedid= ? and todate is null 
                                                and fromdate <=?)";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = strBedID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                //"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"
                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref strXML, ref intRows, objDPArr);
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
        /// 
        /// </summary>
        /// <param name="strBedID"></param>
        /// <param name="strXML"></param>
        /// <param name="intRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetPatientBaseInfoByBedID(
            string strBedID, ref string strXML, ref int intRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPublicMiddleTier", "lngGetPatientBaseInfoByBedID");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                if (strBedID == null || strBedID == "")
                    return -1;
                //			strXML = "<root><PatientInfo PatientID='0000001' InHospitalNo='0000001' PatientName='Hahaha' Sex='男' Married='已婚' Birth='1979-7-29' InHospitalDate='2002-8-1' LinkManName='hhh' LinkManPhone='13692111' /></root>";
                //			intRows = 1;
                //			return 1;

                string strSQL = @"select patientid,pbi.inhospitalno,firstname,sex,married,birth,inhospitaldate,linkmanfirstname,linkmanphone 
                                    from departmentsickroom dsr,patientbaseinfo pbi 
                                        where dsr.inhospitalno = pbi.inhospitalno 
                                        and dsr.sickbedno=? 
                                        and dsr.outhospitaldate=?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = strBedID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900, 1, 1);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref strXML, ref intRows, objDPArr);
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

        //		//Ben 2002-8-8
        //		/// <summary>
        //		/// 获取用户信息
        //		/// </summary>
        //		/// <param name="p_strDomain">用户所在的域</param>
        //		/// <param name="p_strDomainUser">用户名</param>
        //		/// <param name="p_strXML">返回结果的XML</param>
        //		/// <param name="p_intRows">返回结果的条数</param>
        //		/// <returns>
        //		/// 操作结果:
        //		/// 0,失败；
        //		/// 1，成功。
        //		/// </returns>
        //		public long lngGetUserInfo(string p_strDomain,string p_strDomainUser,ref string p_strXML,ref int p_intRows)
        //		{
        //			//liyi 2003-1-3 修改为从新的DepartmentInfo读取
        //			string strSQL = @"SELECT DomainUserInfo.EmployeeID, EmployeeBaseInfo.FirstName, 
        //									DepartmentInfo1.DepartmentID, DepartmentInfo1.DeptFlag
        //								FROM DomainUserInfo INNER JOIN
        //									EmployeeBaseInfo ON 
        //									DomainUserInfo.EmployeeID = EmployeeBaseInfo.EmployeeID INNER JOIN
        //									DeptEmployee ON 
        //									EmployeeBaseInfo.EmployeeID = DeptEmployee.EmployeeID INNER JOIN
        //									DepartmentInfo1 ON 
        //									DeptEmployee.DepartmentID = DepartmentInfo1.DepartmentID
        //								WHERE (DomainUserInfo.DomainName = '"+p_strDomain+@"') AND 
        //									(DomainUserInfo.DomainUserName = '"+p_strDomainUser+@"') AND 
        //									(DomainUserInfo.End_Date_UserName = CONVERT(DATETIME, '1900-01-01 00:00:00', 
        //									102)) AND (EmployeeBaseInfo.Status = 0) AND 
        //									(DeptEmployee.End_Date_Dept_Emp = CONVERT(DATETIME, '1900-01-01 00:00:00', 
        //									102)) AND (DepartmentInfo1.Status = 0)";
        //
        //			return m_objHRPServ.lngGetXMLTable(strSQL,ref p_strXML,ref p_intRows);
        //
        //		}

        //liyi 2003-1-3
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="p_strDomain">管理员所在的域</param>
        /// <param name="p_strXML">返回结果的XML</param>
        /// <param name="p_intRows">返回结果的条数</param>
        /// <returns>
        /// 操作结果:
        /// 0,失败；
        /// 1，成功。
        /// </returns>
        [AutoComplete]
        public long m_lngGetAdminUserInfo(
            string p_strDomain, ref string p_strXML, ref int p_intRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPublicMiddleTier", "m_lngGetAdminUserInfo");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                string strSQL = @"select domainuserinfo.employeeid, employeebaseinfo.firstname
									from domainuserinfo inner join
										employeebaseinfo on 
										domainuserinfo.employeeid = employeebaseinfo.employeeid
									where (domainuserinfo.domainname = ?) and 
										(domainuserinfo.domainusername = 'administrator')";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDomain;

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strXML, ref p_intRows, objDPArr);

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

        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		/// <param name="strID"></param>
        //		/// <param name="strXML"></param>
        //		/// <param name="intRows"></param>
        //		/// <returns></returns>
        //		public long lngGetPatientInHospitalBaseInfo(string strID,ref string strXML,ref int intRows)
        //		{
        //			string strSQL="SELECT DepartmentSickRoom.*, DepartmentInfo.DepartmentName AS DepartmentName, "+
        //				" InPatient_Bed_Desc.Bed_Name AS Bed_Name, "+
        //				" InPatient_Area_Desc.Area_Name AS Area_Name, "+
        //				" PatientBaseInfo.FirstName AS PatientName, "+
        //				" InPatient_Room_Desc.Room_Name AS RoomName "+
        //				" FROM DepartmentSickRoom LEFT OUTER JOIN "+
        //				" DepartmentInfo ON "+
        //				" DepartmentSickRoom.DepartmentID = DepartmentInfo.DepartmentID LEFT OUTER JOIN "+
        //				" InPatient_Area_Desc ON "+
        //				" DepartmentSickRoom.AreaID = InPatient_Area_Desc.Area_ID LEFT OUTER JOIN "+
        //				" InPatient_Bed_Desc ON "+
        //				" DepartmentSickRoom.SickBedNO = InPatient_Bed_Desc.Bed_ID LEFT OUTER JOIN" +
        //				" PatientBaseInfo ON "+
        //				" DepartmentSickRoom.InHospitalNO = PatientBaseInfo.InHospitalNo LEFT OUTER JOIN "+
        //				" InPatient_Room_Desc ON "+
        //				" DepartmentSickRoom.SickRoomNO = InPatient_Room_Desc.Room_ID "+
        //				" WHERE (DepartmentSickRoom.OutHospitalDate = "+clsHRPTableService.c_strInvalidDate+@") AND "+
        //				" (DepartmentSickRoom.InHospitalNO = '"+strID+"')";
        //			return m_objHRPServ.lngGetXMLTable(strSQL,ref strXML,ref intRows);
        //
        //		}

        //		public long m_lngDeActiveReqRecord(string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,string p_strModifyDate,string p_strDeActivedOperatorID,string p_strTableName)
        //		{
        //			string strUpdateSQL = @"UPDATE "+p_strTableName+@"
        //										SET DeActivedDate = "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+@", 
        //											DeActivedOperatorID = '"+p_strDeActivedOperatorID+@"', Status = 1
        //										WHERE (InPatientID = '"+p_strInPatientID+"') AND (InPatientDate = "+p_strInPatientDate+") AND (CreateDate = "+p_strCreateDate+@") AND 
        //											(ModifyDate = "+p_strModifyDate+")";
        //
        //			return new clsHRPTableService().DoExcute(strUpdateSQL);
        //		}


        /// <summary>
        /// 查找该表在该条件下是否有重复的记录
        /// </summary>
        /// <param name="p_strTableName"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <param name="p_intRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCreateDateCount(
            string p_strTableName, string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out int p_intRows)
        {
            p_intRows = 0;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPublicMiddleTier", "m_lngGetCreateDateCount");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                if (p_strTableName == null || p_strTableName == "")
                    return -1;
                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strCreateDate == null || p_strCreateDate == "")
                    return -1;

                DataTable m_dtbResult = new DataTable();
                string m_strCommand = "select count(inpatientid) recordcount from " + p_strTableName + @" where 
                            inpatientid = ? and 
                            inpatientdate = ? and 
                            createdate = ? and status = 0";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref m_dtbResult, objDPArr);
                if (lngRes > 0)
                {
                    p_intRows = int.Parse(m_dtbResult.Rows[0][0].ToString());
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
            //返回
            return lngRes;
        }

        /// <summary>
        /// 删除记录，适合status＝1时为删除状态的表
        /// </summary>
        /// <param name="p_strTableName"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strOperatorID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteRecord(
            string p_strTableName, string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, string p_strOperatorID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPublicMiddleTier", "m_lngDeleteRecord");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                if (p_strTableName == null || p_strTableName == "")
                    return -1;
                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;

                string m_strCommand = "update " + p_strTableName + @" set status =1, 
                                    deactiveddate = ?, 
                                    deactivedoperatorid = ? 
                                    where inpatientid = ? 
                                    and inpatientdate = ? 
                                    and opendate= ? ";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strOperatorID;
                objDPArr[2].Value = p_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strOpenDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strCommand, ref lngEff, objDPArr);
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
        /// 删除记录，适合status＝0时为删除状态的表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTableName"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strOperatorID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteRecord2(
            string p_strTableName, string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, string p_strOperatorID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPublicMiddleTier", "m_lngDeleteRecord");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                if (p_strTableName == null || p_strTableName == "")
                    return -1;
                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;

                string m_strCommand = "update " + p_strTableName + @" set status =0, 
                                    deactiveddate = ?, 
                                    deactivedoperatorid = ? 
                                    where inpatientid = ? 
                                    and inpatientdate = ? 
                                    and opendate= ? ";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strOperatorID;
                objDPArr[2].Value = p_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strOpenDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strCommand, ref lngEff, objDPArr);
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
        /// 获得Middle Tier 端的时间
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetServerTime()
        {
            string strRes = null;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPublicMiddleTier","m_strGetServerTime");
                //if(lngCheckRes <= 0)
                //    return null;

                strRes = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return strRes;

        }

        /// <summary>
        /// 获取数据库当前时间
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetDBServerTime()
        {
            string strRes = null;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPublicMiddleTier","m_strGetServerTime");
                //if(lngCheckRes <= 0)
                //    return null;

                string strSQL = @"select " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + "from dual";
                DataTable dtbResult = new DataTable();
                long lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (dtbResult != null && dtbResult.Rows.Count == 1)
                {
                    DateTime dtNow = DateTime.MinValue;
                    if (DateTime.TryParse(dtbResult.Rows[0][0].ToString(), out dtNow))
                    {
                        strRes = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return strRes;

        }

        /// <summary>
        /// 设置该条记录的第一次打印时间
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strTableName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetFirstPrintDate(
            string[] p_strInPatientID, string[] p_strInPatientDate, string[] p_strOpenDate, string p_strTableName)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPublicMiddleTier", "m_lngSetFirstPrintDate");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID.Length <= 0)
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate.Length <= 0)
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate.Length <= 0)
                    return -1;
                if (p_strTableName == null || p_strTableName == "")
                    return -1;

                string m_strCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string m_strCommand = null;
                for (int i1 = 0; i1 < p_strInPatientID.Length; i1++)
                {
                    m_strCommand = "update " + p_strTableName +
                        @" set firstprintdate = ? 
                         where  inpatientid = ? 
                         and inpatientdate = ? 
                         and opendate = ? ";

                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = DateTime.Parse(m_strCurrentDateTime);
                    objDPArr[1].Value = p_strInPatientID[i1].Trim();
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strInPatientDate[i1].Trim());
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = DateTime.Parse(p_strOpenDate[i1].Trim());

                    long lngEff = -1;
                    lngRes = objHRPServ.lngExecuteParameterSQL(m_strCommand, ref lngEff, objDPArr);
                    if (lngRes <= 0)
                        lngRes = -1;
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
            //返回
            return lngRes;

        }

        /// <summary>
        /// 获得最后修改时间
        /// 如果返回空，表示该记录已被删除
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strContentTable">子表名称</param>
        /// <param name="p_strLastModifyDate"></param>
        /// <param name="p_strMasterTable">主表名称</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLastModifyDate(
            string p_strMasterTable, string p_strContentTable, string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strLastModifyDate)
        {
            p_strLastModifyDate = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPublicMiddleTier", "m_lngGetLastModifyDate");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                if (p_strMasterTable == null || p_strMasterTable == "")
                    return -1;
                if (p_strContentTable == null || p_strContentTable == "")
                    return -1;
                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;

                DataTable m_dtbResult = new DataTable();
                string strCommand = "select max(t2.lastmodifydate) as maxlastmodifydate " +
                    "from " + p_strMasterTable + " t1," + p_strContentTable + " t2 " +
                    @"where t1.status = 0 and t2.status = 0 and 
                    t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate 
                    and t1.opendate = t2.opendate  
                    and t1.inpatientid = ? and t1.inpatientdate = ? 
                    and t1.opendate= ? ";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref m_dtbResult, objDPArr);
                if (lngRes > 0)
                {
                    if (m_dtbResult.Rows[0][0] == null || m_dtbResult.Rows[0][0].ToString() == "")
                        p_strLastModifyDate = null;
                    else
                        p_strLastModifyDate = m_dtbResult.Rows[0][0].ToString();
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
            //返回
            return lngRes;

        }


        /// <summary>
        /// 获得最后修改时间,以及修改人
        /// 如果返回空，表示该记录已被删除
        /// 适合status＝0时为正常状态的表
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strContentTable">子表名称</param>
        /// <param name="p_strLastModifyDate"></param>
        /// <param name="p_strMasterTable">主表名称</param>
        /// <param name="p_strLastModifyUserID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLastModifyDateAndUser(string p_strMasterTable, string p_strContentTable, string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strLastModifyDate, out string p_strLastModifyUserID)
        {
            p_strLastModifyDate = null;
            p_strLastModifyUserID = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPublicMiddleTier", "m_lngGetLastModifyDateAndUser");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                if (string.IsNullOrEmpty(p_strMasterTable) || string.IsNullOrEmpty(p_strContentTable) ||
                    string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate))
                    return -1;


                DataTable m_dtbResult = new DataTable();
                string strCommand = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strCommand = "select top 1 lastmodifydate,lastmodifyuserid from " + p_strMasterTable + " " +
                        "t1," + p_strContentTable + @" t2 where  
                        t1.status = 0 and t2.status = 0 and t1.inpatientid = t2.inpatientid  
                        and t1.inpatientdate = t2.inpatientdate  
                        and t1.opendate = t2.opendate  and t1.inpatientid = ?  
                        and t1.inpatientdate = ? and 
                        t1.opendate= ? 
                        order by lastmodifydate desc  ";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strCommand = "select lastmodifydate,lastmodifyuserid from(select lastmodifydate,lastmodifyuserid from " + p_strMasterTable + " " +
                        "t1," + p_strContentTable + " t2 where  " +
                        @"t1.status = 0 and t2.status = 0 and t1.inpatientid = t2.inpatientid  
                        and t1.inpatientdate = t2.inpatientdate  
                        and t1.opendate = t2.opendate  and t1.inpatientid = ?  
                        and t1.inpatientdate = ? and 
                        t1.opendate= ?  
                        order by lastmodifydate desc)where rownum = 1  ";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strCommand = " select lastmodifydate,lastmodifyuserid from " + p_strMasterTable + " " +
                        "t1," + p_strContentTable + @" t2 where  
                        t1.status = 0 and t2.status = 0 and t1.inpatientid = t2.inpatientid  
                        and t1.inpatientdate = t2.inpatientdate  
                        and t1.opendate = t2.opendate  and t1.inpatientid = ?  
                        and t1.inpatientdate = ? and 
                        t1.opendate= ?  
                        order by lastmodifydate desc  fetch first 1 row only ";
                }

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref m_dtbResult, objDPArr);
                if (lngRes > 0)
                {
                    if (m_dtbResult.Rows.Count <= 0)
                    {
                        p_strLastModifyDate = null;
                        p_strLastModifyUserID = null;
                    }
                    else
                    {
                        if (m_dtbResult.Rows[0][0] == null || m_dtbResult.Rows[0][0].ToString() == "")
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
        /// <summary>
        /// 获得最后修改时间,以及修改人
        /// 如果返回空，表示该记录已被删除
        /// 适合status＝1时为正常状态的表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMasterTable"></param>
        /// <param name="p_strContentTable"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strLastModifyDate"></param>
        /// <param name="p_strLastModifyUserID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLastModifyDateAndUser2(string p_strMasterTable, string p_strContentTable, string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strLastModifyDate, out string p_strLastModifyUserID)
        {
            p_strLastModifyDate = null;
            p_strLastModifyUserID = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPublicMiddleTier", "m_lngGetLastModifyDateAndUser");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                if (string.IsNullOrEmpty(p_strMasterTable) || string.IsNullOrEmpty(p_strContentTable) ||
                    string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate))
                    return -1;

                DataTable m_dtbResult = new DataTable();
                string strCommand = null;

                strCommand = "select lastmodifydate,lastmodifyuserid from " + p_strMasterTable + " " +
                    "t1," + p_strContentTable + @" t2 where  
                    t1.status = 1 and t2.status = 1 and t1.inpatientid = t2.inpatientid  
                    and t1.inpatientdate = t2.inpatientdate  
                    and t1.opendate = t2.opendate  and t1.inpatientid = ?  
                    and t1.inpatientdate = ? and 
                    t1.opendate= ?  ";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref m_dtbResult, objDPArr);
                if (lngRes > 0)
                {
                    if (m_dtbResult.Rows.Count <= 0)
                    {
                        p_strLastModifyDate = null;
                        p_strLastModifyUserID = null;
                    }
                    else
                    {
                        if (m_dtbResult.Rows[0][0] == null || m_dtbResult.Rows[0][0].ToString() == "")
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

        /// <summary>
        /// 获得最后删除时间,以及删除人
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <param name="p_strDeactivedDate"></param>
        /// <param name="p_strMasterTable">主表名称</param>
        /// <param name="p_strDeactivedUserID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeactivedDateAndUser(string p_strMasterTable, string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out string p_strDeactivedDate, out string p_strDeactivedUserID)
        {
            p_strDeactivedDate = null;
            p_strDeactivedUserID = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPublicMiddleTier", "m_lngGetDeactivedDateAndUser");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                if (p_strMasterTable == null || p_strMasterTable == "")
                    return -1;
                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strCreateDate == null || p_strCreateDate == "")
                    return -1;

                DataTable m_dtbResult = new DataTable();
                string strCommand = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strCommand = "select top 1 deactiveddate,deactivedoperatorid " +
                        "from " + p_strMasterTable + @" where inpatientid = ? 
                        and inpatientdate = ? 
                        and createdate = ? and status =1  
                        order by deactiveddate desc";
                }
                else
                {
                    strCommand = "select deactiveddate,deactivedoperatorid from(select deactiveddate,deactivedoperatorid " +
                        "from " + p_strMasterTable + @" where inpatientid = ? 
                        and inpatientdate = ? 
                        and createdate = ? and status =1  
                        order by deactiveddate desc)where rownum = 1";
                }

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.DoGetDataTable(strCommand, ref m_dtbResult);
                if (lngRes > 0)
                {
                    if (m_dtbResult.Rows.Count <= 0)
                    {
                        p_strDeactivedDate = null;
                        p_strDeactivedUserID = null;
                    }
                    else
                    {
                        if (m_dtbResult.Rows[0][0] == null || m_dtbResult.Rows[0][0].ToString() == "")
                        {
                            p_strDeactivedDate = null;
                            p_strDeactivedUserID = null;
                        }
                        else
                        {
                            p_strDeactivedDate = DateTime.Parse(m_dtbResult.Rows[0][0].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            p_strDeactivedUserID = m_dtbResult.Rows[0][1].ToString();
                        }
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

            }
            //返回
            return lngRes;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_dtRecords"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetData(string p_strSQL, out DataTable p_dtRecords)
        {
            p_dtRecords = null;
            long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (p_strSQL.Trim().Length <= 0)
                    return lngRes;

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(p_strSQL, ref p_dtRecords);
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
        /// <summary>
        /// 获取数据(参数化方式)
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_objValues">参数数组</param>
        /// <param name="p_dtRecords"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDataWithParam(string p_strSQL, object[] p_objValues, ref DataTable p_dtRecords)
        {
            p_dtRecords = new DataTable();
            if (p_strSQL == "")
                return -1;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServer = new clsHRPTableService();
                IDataParameter[] objParam = null;
                objHRPServer.CreateDatabaseParameter(p_objValues.Length, out objParam);
                for (int i = 0; i < p_objValues.Length; i++)
                {
                    if (p_objValues[i] is DateTime)
                        objParam[i].DbType = DbType.DateTime;
                    objParam[i].Value = p_objValues[i];
                }
                lngRes = objHRPServer.lngGetDataTableWithParameters(p_strSQL, ref p_dtRecords, objParam);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            //返回
            return lngRes;

        }
        /// <summary>
        /// 获取ctlRichTextBox修改产生痕迹时限
        /// </summary>
        /// <param name="p_strCanModifyTime"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRTBChangeTime(string p_strSetID, out string p_strCanModifyTime)
        {
            long lngRes = -1;
            p_strCanModifyTime = "6";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select setstatus_int from t_sys_setting where setid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSetID.Trim();

                DataTable dtRecord = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtRecord, objDPArr);

                if (lngRes > 0 && dtRecord.Rows.Count == 1)
                {
                    p_strCanModifyTime = dtRecord.Rows[0][0].ToString().Trim();
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
            //返回
            return lngRes;
        }

        /// <summary>
        /// 设置ctlRichTextBox修改产生痕迹时限
        /// </summary>
        /// <param name="p_strCanModifyTime"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetRTBChangeTime(string p_strCanModifyTime, string p_strSetID)
        {
            long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"update t_sys_setting set setdesc_vchr= ?
                      where setid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strCanModifyTime;
                objDPArr[1].Value = p_strSetID.Trim();

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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
        #region 根据settingID获取配置值
        /// <summary>
        /// 根据settingID获取配置值
        /// 公用获取配置值
        /// modify by tfzhang at 2005-12-6 12:12
        /// </summary>
        /// <param name="p_strSetID">配置ID</param>
        /// <param name="strReturn">返回，若为null则表示未取到配置值</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetConfigBySettingID(string p_strSetID, out string strReturn)
        {
            long lngRes = -1;
            strReturn = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select setstatus_int from t_sys_setting where setid_chr =?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSetID.Trim();
                //生成DataTable
                DataTable dtRecord = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtRecord, objDPArr);

                if (lngRes > 0 && dtRecord.Rows.Count == 1)
                {
                    strReturn = dtRecord.Rows[0][0].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                strReturn = null;
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
        /// 工具模块返回所有这个模块的配置，
        /// </summary>
        /// <param name="p_strModuleId">模块ID，如果为空默认取电子病历设置（0006）</param>
        /// <param name="p_dtbReturn"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllSettingsBymoduleid(string p_strModuleId, out DataTable p_dtbReturn)
        {
            long lngRes = -1;
            p_dtbReturn = new DataTable();
            if (string.IsNullOrEmpty(p_strModuleId)) p_strModuleId = "0006";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select setid_chr,setstatus_int from t_sys_setting where moduleid_chr =?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strModuleId.Trim();
                //生成DataTable
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbReturn, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            //返回
            return lngRes;
        }

        #endregion

        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <param name="p_strName">序列名称</param>
        /// <param name="strReturn">返回值</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSequenceValue(string p_strName, out long strReturn)
        {
            long lngRes = -1;
            strReturn = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = "";

                if (clsHRPTableService.bytDatabase_Selector == 2)
                    strSQL = @"select " + p_strName + ".nextval from dual";
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                    strSQL = @"select nextval for " + p_strName + " from  sysibm.sysdummy1";


                DataTable dtRecord = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtRecord);

                if (lngRes > 0 && dtRecord.Rows.Count == 1)
                {
                    strReturn = long.Parse(dtRecord.Rows[0][0].ToString());
                }
            }
            catch (Exception objEx)
            {
                strReturn = -1;
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

        #region 获取多过序列
        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <param name="p_strName">序列名称</param>
        /// <param name="strReturn">返回值</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecallids(string p_strName, int p_intNum, out long strReturn)
        {
            long lngRes = -1;
            strReturn = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dt = null;
                string Sql = string.Format("select {0}.nextval from dual", p_strName);
                clsHRPTableService svc = new clsHRPTableService();
                for (int i = p_intNum - 1; i >= 0; i--)
                {
                    svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                    strReturn = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                return 1;


                //string strSQL = "";

                //if (clsHRPTableService.bytDatabase_Selector == 2)
                //    strSQL = @"select getseq('" + p_strName + "'," + p_intNum + ") from dual";
                //else if (clsHRPTableService.bytDatabase_Selector == 4)
                //    //strSQL = @"select nextval for " + p_strName + " from  sysibm.sysdummy1";
                //    strSQL = @"";


                //DataTable dtRecord = new DataTable();

                //lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtRecord);

                //if (lngRes > 0 && dtRecord.Rows.Count == 1)
                //{
                //    strReturn = long.Parse(dtRecord.Rows[0][0].ToString());
                //}
            }
            catch (Exception objEx)
            {
                strReturn = -1;
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

        #endregion 

        #region 保存签名集合
        /// <summary>
        /// 保存签名集合
        /// </summary>
        /// <param name="p_objSigns">签名集合</param>
        /// <param name="p_lngSequence">流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddSign(clsEmrSigns_VO[] p_objSigns, long p_lngSequence)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objSigns == null || p_lngSequence == 0)
                    return -1;
                string strSql = @"insert into t_emr_signcollection (sign_int, seq_int, empid_vchr, cagetory_vchr, formname_vchr, registerid_vchr,technicalrank_vchr,technicallevel_vchr,modifydate_dat) 
                                    values (?,?, ?, ?, ?, ?, ?,?,?)";

                long lngEff = 0;
                bool blnHasSave = false;
                for (int i = 0; i < p_objSigns.Length; i++)
                {
                    if (p_objSigns[i] == null)
                    {
                        continue;
                    }
                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(9, out objDPArr);

                    objDPArr[0].Value = p_lngSequence;
                    objDPArr[1].Value = i + 1;
                    objDPArr[2].Value = p_objSigns[i].objEmployee.m_strEMPID_CHR;
                    objDPArr[3].Value = p_objSigns[i].controlName;
                    objDPArr[4].Value = p_objSigns[i].m_strFORMID_VCHR;
                    objDPArr[5].Value = p_objSigns[i].m_strREGISTERID_CHR;
                    objDPArr[6].Value = p_objSigns[i].objEmployee.m_strTechnicalRank.TrimEnd();
                    objDPArr[7].Value = p_objSigns[i].objEmployee.m_StrHistroyLevel;
                    objDPArr[8].DbType = DbType.DateTime;
                    if (p_objSigns[i].m_dtmModiftDate == DateTime.MinValue)
                    {
                        objDPArr[8].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        objDPArr[8].Value = p_objSigns[i].m_dtmModiftDate;
                    }
                    //执行SQL
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                    blnHasSave = true;
                }
                if (!blnHasSave)
                {
                    lngRes = 1;//没有签名需要保存
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
            //返回
            return lngRes;
        }

        #endregion

        #region 根据流水号获取签名集合
        /// <summary>
        /// 根据流水号获取签名集合
        /// </summary>
        /// <param name="p_lngSequence">流水号</param>
        /// <param name="p_objSignsArr">签名集合</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSign(long p_lngSequence, out clsEmrSigns_VO[] p_objSignsArr)
        {
            long lngRes = 0;
            p_objSignsArr = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_lngSequence == 0)
                    return -1;

                string strSeqSQL = string.Empty;
                string strReturnValue = string.Empty;
                bool blnIsShowLevel = true;
                lngRes = m_lngGetConfigBySettingID("3014", out strReturnValue);

                if (strReturnValue == "1")
                {
                    strSeqSQL = "order by t.technicallevel_vchr desc,t.seq_int";
                }
                else
                {
                    strSeqSQL = "order by t.modifydate_dat,t.seq_int";
                }

                lngRes = m_lngGetConfigBySettingID("3015", out strReturnValue);
                if (strReturnValue == "1")
                {
                    blnIsShowLevel = true;
                }
                else
                {
                    blnIsShowLevel = false;
                }

                string strSql = @"select t.empid_vchr,
									d.lastname_vchr,
                                    d.technicalrank_chr emptechnicalrank,
									t.technicalrank_vchr,
									d.empno_chr,
									d.psw_chr,
									d.digitalsign_dta,
									d.pycode_chr,
									d.status_int,
									d.technicallevel_chr,
                                    t.technicallevel_vchr histroylevel,
									t.cagetory_vchr,
									t.formname_vchr,
									t.registerid_vchr,t.modifydate_dat
								from t_emr_signcollection t
								left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
								where t.sign_int = ? " + strSeqSQL;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngSequence;

                //DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
                int intSignCount = dtbValue.Rows.Count;
                DataRow objRow = null;
                clsEmrSigns_VO objEmrSigns_VO = null;
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && intSignCount > 0)
                {
                    p_objSignsArr = new clsEmrSigns_VO[intSignCount];
                    for (int i = 0; i < intSignCount; i++)
                    {
                        objRow = dtbValue.Rows[i];
                        objEmrSigns_VO = new clsEmrSigns_VO();
                        objEmrSigns_VO.objEmployee = new clsEmrEmployeeBase_VO();
                        objEmrSigns_VO.objEmployee.m_strEMPID_CHR = objRow["empid_vchr"].ToString();
                        objEmrSigns_VO.objEmployee.m_strLASTNAME_VCHR = objRow["lastname_vchr"].ToString();
                        objEmrSigns_VO.objEmployee.m_strEMPNO_CHR = objRow["empno_chr"].ToString();
                        objEmrSigns_VO.objEmployee.m_strTECHNICALRANK_CHR = objRow["Emptechnicalrank"].ToString().Trim();
                        objEmrSigns_VO.objEmployee.m_strLEVEL_CHR = objRow["technicallevel_chr"].ToString();
                        objEmrSigns_VO.objEmployee.m_strEMPPWD_VCHR = objRow["psw_chr"].ToString();
                        objEmrSigns_VO.objEmployee.m_strEMPKEY_VCHR = objRow["digitalsign_dta"].ToString();
                        objEmrSigns_VO.objEmployee.m_strPYCODE_VCHR = objRow["pycode_chr"].ToString();
                        int intTemp = 1;
                        int.TryParse(objRow["status_int"].ToString(), out intTemp);
                        objEmrSigns_VO.objEmployee.m_intSTATUS_INT = intTemp;
                        objEmrSigns_VO.controlName = objRow["CAGETORY_VCHR"].ToString();
                        objEmrSigns_VO.m_strFORMID_VCHR = objRow["FORMNAME_VCHR"].ToString();
                        objEmrSigns_VO.m_strREGISTERID_CHR = objRow["REGISTERID_VCHR"].ToString();
                        if (blnIsShowLevel)
                        {
                            objEmrSigns_VO.objEmployee.m_strTechnicalRank = objRow["TECHNICALRANK_VCHR"].ToString();
                        }
                        else
                        {
                            objEmrSigns_VO.objEmployee.m_strTechnicalRank = string.Empty;
                        }
                        objEmrSigns_VO.objEmployee.m_StrHistroyLevel = objRow["HistroyLevel"].ToString();
                        objEmrSigns_VO.m_dtmModiftDate = Convert.ToDateTime(objRow["MODIFYDATE_DAT"]);
                        //显示职称
                        //objEmrSigns_VO.objEmployee.m_strLASTNAME_VCHR = objEmrSigns_VO.objEmployee.m_strTechnicalRank + " " + objEmrSigns_VO.objEmployee.m_strLASTNAME_VCHR;
                        p_objSignsArr[i] = objEmrSigns_VO;
                    }
                }
                ////释放
                //objHRPServ = null;

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
        #endregion



        #region 获取ICD10字典
        /// <summary>
        /// 获取ICD10字典
        /// </summary>
        /// <param name="p_dtbICD10Dict">ICD10字典</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetICD10Dict(out DataTable p_dtbICD10Dict)
        {
            p_dtbICD10Dict = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.icdcode_chr code,
       t.icdname_vchr name,
       t.jxcode_chr jx,
       t.wbcode_chr wb,
       t.pycode_chr py,
       t.icdcat_chr
  from t_aid_icd10 t
 order by t.icdcode_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbICD10Dict);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取手术字典
        /// <summary>
        /// 获取手术字典
        /// </summary>
        /// <param name="p_dtbOperationDict">手术字典</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOperationDict(out DataTable p_dtbOperationDict)
        {
            p_dtbOperationDict = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select icdcode_chr code,
       icdname_vchr name,
       jxcode_chr jx,
       wbcode_chr wb,
       pycode_chr py,
       lvlid_chr
  from t_aid_oprticd
 order by icdcode_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbOperationDict);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion


        #region 获取麻醉方法字典
        /// <summary>
        /// 获取麻醉方法字典
        /// </summary>
        /// <param name="p_dtbAnaesthesiaDict">麻醉方法字典</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAnaesthesiaDict(out DataTable p_dtbAnaesthesiaDict)
        {
            p_dtbAnaesthesiaDict = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.anaesthesia_code code,
       t.anaesthesia_name name,
       t.py_code py
  from t_aid_anaesthesia t
 order by t.anaesthesia_code";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbAnaesthesiaDict);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取切口愈合等级字典
        /// <summary>
        /// 获取切口愈合等级字典
        /// </summary>
        /// <param name="p_dtbWoundHealDict">切口愈合等级字典</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetWoundHealDict(out DataTable p_dtbWoundHealDict)
        {
            p_dtbWoundHealDict = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.woundhealcode_vchr code,
       t.woundhealname_vchr name,
       t.pycode_vchr py
  from t_aid_woundheallevel t";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbWoundHealDict);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取手术切口等级字典
        /// <summary>
        /// 获取手术切口等级字典
        /// </summary>
        /// <param name="p_dtbWoundGradeDict">手术切口等级字典</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetWoundGradeDict(out DataTable p_dtbWoundGradeDict)
        {
            p_dtbWoundGradeDict = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.woundgradecode_int code,
       t.woundgradename_vchr name,
       t.pycode_vchr py
  from t_aid_woundgrade t
 order by t.woundgradecode_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbWoundGradeDict);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取愈合情况字典
        /// <summary>
        /// 获取愈合情况字典
        /// </summary>
        /// <param name="p_dtbHealDict">愈合情况字典</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetHealDict(out DataTable p_dtbHealDict)
        {
            p_dtbHealDict = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.healcode_chr code,
       t.healname_vchr name,
       t.pycode_vchr py
  from t_aid_heal t
 order by t.healcode_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbHealDict);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据员工ID获取其所享有的权限
        /// <summary>
        /// 根据员工ID获取其所享有的权限
        /// </summary>
        /// <param name="p_strEmpID">员工ID</param>
        /// <param name="p_strRoleIDArr">权限ID数组</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoleIDByEmpID(string p_strEmpID, out string[] p_strRoleIDArr)
        {
            long lngRes = 0;
            p_strRoleIDArr = null;
            if (p_strEmpID == null || p_strEmpID == "")
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select t.roleid_chr from t_sys_emprolemap t where t.empid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmpID.Trim();
                //DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果                 
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    List<string> strRoleID = new List<string>();
                    int rowCount = dtbValue.Rows.Count;
                    for (int i = 0; i < rowCount; i++)
                    {
                        if (dtbValue.Rows[i]["RoleID_Chr"] != DBNull.Value)
                        {
                            strRoleID.Add(dtbValue.Rows[i]["RoleID_Chr"].ToString());
                        }
                    }
                    p_strRoleIDArr = strRoleID.ToArray();
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
            //返回
            return lngRes;
        }

        /// <summary>
        /// 根据员工ID获取其所享有的权限
        /// </summary>
        /// <param name="p_strEmpID">员工ID</param>
        /// <param name="p_strRoleIDArr">角色ID数组</param>
        /// <param name="p_strRoleNameArr">角色名称数组,其ID与p_strRoleIDArr一一对应</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoleByEmpID(string p_strEmpID, out string[] p_strRoleIDArr, out string[] p_strRoleNameArr)
        {
            long lngRes = 0;
            p_strRoleIDArr = null;
            p_strRoleNameArr = null;
            if (string.IsNullOrEmpty(p_strEmpID))
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select t.roleid_chr, r.name_vchr
  from t_sys_emprolemap t, t_sys_role r
 where t.roleid_chr = r.roleid_chr
   and t.empid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmpID.Trim();
                //DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果                 
                if (lngRes > 0)
                {
                    int rowCount = dtbValue.Rows.Count;
                    if (rowCount <= 0)
                    {
                        return 0;
                    }
                    List<string> strRoleID = new List<string>();
                    List<string> strRoleName = new List<string>();
                    DataRow drCurrent = null;
                    for (int i = 0; i < rowCount; i++)
                    {
                        drCurrent = dtbValue.Rows[i];
                        if (drCurrent["RoleID_Chr"] != DBNull.Value)
                        {
                            strRoleID.Add(drCurrent["RoleID_Chr"].ToString());
                            strRoleName.Add(drCurrent["name_vchr"].ToString());
                        }
                    }
                    p_strRoleIDArr = strRoleID.ToArray();
                    p_strRoleNameArr = strRoleName.ToArray();
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
            //返回
            return lngRes;
        }
        #endregion

        #region 根据权限ID获取权限名称
        /// <summary>
        /// 根据权限ID获取权限名称
        /// </summary>
        /// <param name="RoleID">权限ID</param>
        /// <param name="p_strRoleName">权限名称</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoleNameByRoleID(string RoleID, out string p_strRoleName)
        {
            long lngRes = 0;
            p_strRoleName = null;
            if (RoleID == null || RoleID == "")
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select t.name_vchr from t_sys_role t where t.roleid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = RoleID.Trim();
                //DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strRoleName = dtbValue.Rows[0]["Name_VChr"].ToString();
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
            //返回
            return lngRes;
        }
        #endregion

        #region 根据权限名称获取权限ID
        /// <summary>
        /// 根据权限名称获取权限ID
        /// </summary>		
        /// <param name="p_strRoleName">权限名称</param>
        /// <param name="RoleID">权限ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoleIDByRoleName(string p_strRoleName, out string p_strRoleID)
        {
            long lngRes = 0;
            p_strRoleID = null;
            if (p_strRoleName == null || p_strRoleName == "")
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select t.roleid_chr from t_sys_role t where t.name_vchr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRoleName.Trim();
                //DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strRoleID = dtbValue.Rows[0]["RoleID_Chr"].ToString();
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
            //返回
            return lngRes;
        }
        #endregion

        #region 判断该员工是否享有该权限
        /// <summary>
        /// 判断该员工是否享有该权限,如果返回的权限ID不为空，则表明享有该权限
        /// </summary>	
        /// <param name="p_strEmpID">员工ID</param>	
        /// <param name="p_strRoleName">权限名称</param>
        /// <param name="RoleID">权限ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckRoleByEmpIDAndRoleName(string p_strEmpID, string p_strRoleName, out string p_strRoleID)
        {
            long lngRes = 0;
            p_strRoleID = null;
            if (p_strRoleName == null || p_strRoleName == "" || p_strEmpID == null || p_strEmpID == "")
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select t.roleid_chr
								from t_sys_role t, t_sys_emprolemap t1
								where t1.empid_chr = ?
								and t.name_vchr = ?
								and t1.roleid_chr = t.roleid_chr";



                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strEmpID.Trim();
                objDPArr[1].Value = p_strRoleName.Trim();

                //DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strRoleID = dtbValue.Rows[0]["RoleID_Chr"].ToString();
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
            //返回
            return lngRes;
        }

        /// <summary>
        /// 判断该员工是否享有该科室下的指定权限,如果返回的权限ID不为空，则表明享有该权限
        /// </summary>	
        /// <param name="p_strEmpID">员工ID</param>	
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_strRoleName">权限名称</param>
        /// <param name="RoleID">权限ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckRoleByEmpIDAndRoleName(string p_strEmpID, string p_strDeptID, string p_strRoleName, out string p_strRoleID)
        {
            long lngRes = 0;
            p_strRoleID = null;
            if (string.IsNullOrEmpty(p_strRoleName) || string.IsNullOrEmpty(p_strEmpID) || string.IsNullOrEmpty(p_strDeptID))
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select t.roleid_chr
  from t_sys_role t, t_sys_emprolemap t1
 where t1.empid_chr = ?
   and t.name_vchr = ?
   and t.deptid_chr = ?
   and t1.roleid_chr = t.roleid_chr";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strEmpID.Trim();
                objDPArr[1].Value = p_strRoleName.Trim();
                objDPArr[2].Value = p_strDeptID.Trim();

                //DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strRoleID = dtbValue.Rows[0]["RoleID_Chr"].ToString();
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

        #region 根据入院登记号和入院时间获取入院登记号
        /// <summary>
        /// 根据入院登记号和入院时间获取入院登记号
        /// 该方法使原来的接口不做改变，多做一次数据库查询转换
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="strReturn"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRegisterIDByInpatientID(string p_strInPatientID,
            string p_strInPatientDate, out string strReturn)
        {
            long lngRes = -1;
            strReturn = "";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select t.registerid_chr
                          from t_opr_bih_register t
                         where t.inpatientid_chr =?
                           and t.inpatient_dat = ?  and t.status_int = 1";

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    strReturn = dtbValue.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                strReturn = "";
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
        #endregion

    }
}
