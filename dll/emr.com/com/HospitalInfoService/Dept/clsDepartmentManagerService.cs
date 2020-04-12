using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.DepartmentManagerService
{
    /// <summary>
    /// 
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsDepartmentManagerService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_strInfoResultXml"></param>
        /// <param name="p_intInfoResultRows"></param>
        /// <param name="p_strChildrenResultXml"></param>
        /// <param name="p_intChildrenResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptInfo(string p_strDeptID, ref string p_strInfoResultXml, ref int p_intInfoResultRows, ref string p_strChildrenResultXml, ref int p_intChildrenResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetDeptInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;


                #region RegionName
                //
                //				string strSQL = @"select D_D.*,DAD.ParentDeptID,DAD.ModifyDate as DeptRel,DAD.Levels,DBI.CreateDate
                //									from DeptBaseInfo DBI,DeptAndDept DAD,Dept_Desc D_D
                //									where DBI.DeptID = DAD.DeptID
                //									and DBI.DeptID = D_D.DeptID
                //									and DBI.DeptID = '"+p_strDeptID+@"'
                //									and DBI.Status = '0'
                //									and D_D.ModifyDate = 
                //									(select Max(ModifyDate)
                //									from Dept_Desc
                //									where DeptID = '"+p_strDeptID+"')";
                #endregion
                //使用新表 modified by tfzhang at 2005年10月19日 11:59:41
                string strSQL = @"select t.deptid_chr                as deptid,
									t.modify_dat                as modifydate,
									t.deptname_vchr             as deptname,
									t.category_int              as category,
									t.inpatientoroutpatient_int as inpatientoroutpatient,
									t.address_vchr              as address,
									t.pycode_chr                as pycode,
									t.shortno_chr               as shortno,
									t.parentid                  as parentdeptid,
									t.modify_dat                as deptrel,
									1                           as levels,
									t.createdate_dat            as createdate
								from t_bse_deptdesc t where  t.shortno_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID.Trim();

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strInfoResultXml, ref p_intInfoResultRows, objDPArr);

                if (lngRes <= 0)
                {
                    p_strInfoResultXml = "";
                    p_intInfoResultRows = 0;
                    p_strChildrenResultXml = "";
                    p_intChildrenResultRows = 0;

                    return lngRes;
                }

                #region RegionName
                //				strSQL = @"select DeptID
                //							from DeptAndDept
                //							where ParentDeptID = '"+p_strDeptID+@"'
                //							and DeptID != ParentDeptID";
                //使用新表 modified by tfzhang at 2005年10月19日 12:09:06
                #endregion
                strSQL = @"select t.shortno_chr as deptid
							from t_bse_deptdesc t
							where t.parentid = (select d.deptid_chr
												from t_bse_deptdesc d
												where  d.shortno_chr  = ?)
							and t.shortno_chr != parentid";

                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID.Trim();

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strChildrenResultXml, ref p_intChildrenResultRows, objDPArr);

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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_strInfoResultXml"></param>
        /// <param name="p_intInfoResultRows"></param>
        /// <param name="p_strChildrenResultXml"></param>
        /// <param name="p_intChildrenResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptInfo2(string p_strDeptID_chr, ref string p_strInfoResultXml, ref int p_intInfoResultRows, ref string p_strChildrenResultXml, ref int p_intChildrenResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetDeptInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;


                #region RegionName
                //
                //				string strSQL = @"select D_D.*,DAD.ParentDeptID,DAD.ModifyDate as DeptRel,DAD.Levels,DBI.CreateDate
                //									from DeptBaseInfo DBI,DeptAndDept DAD,Dept_Desc D_D
                //									where DBI.DeptID = DAD.DeptID
                //									and DBI.DeptID = D_D.DeptID
                //									and DBI.DeptID = '"+p_strDeptID+@"'
                //									and DBI.Status = '0'
                //									and D_D.ModifyDate = 
                //									(select Max(ModifyDate)
                //									from Dept_Desc
                //									where DeptID = '"+p_strDeptID+"')";
                #endregion
                //使用新表 modified by tfzhang at 2005年10月19日 11:59:41
                string strSQL = @"select t.deptid_chr                as deptid,
									t.modify_dat                as modifydate,
									t.deptname_vchr             as deptname,
									t.category_int              as category,
									t.inpatientoroutpatient_int as inpatientoroutpatient,
									t.address_vchr              as address,
									t.pycode_chr                as pycode,
									t.shortno_chr               as shortno,
									t.parentid                  as parentdeptid,
									t.modify_dat                as deptrel,
									1                           as levels,
									t.createdate_dat            as createdate
								from t_bse_deptdesc t where  t.deptid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID_chr.Trim();

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strInfoResultXml, ref p_intInfoResultRows, objDPArr);

                if (lngRes <= 0)
                {
                    p_strInfoResultXml = "";
                    p_intInfoResultRows = 0;
                    p_strChildrenResultXml = "";
                    p_intChildrenResultRows = 0;

                    return lngRes;
                }

                #region RegionName
                //				strSQL = @"select DeptID
                //							from DeptAndDept
                //							where ParentDeptID = '"+p_strDeptID+@"'
                //							and DeptID != ParentDeptID";
                //使用新表 modified by tfzhang at 2005年10月19日 12:09:06
                #endregion
                strSQL = @"select t.shortno_chr as deptid
							from t_bse_deptdesc t
							where t.parentid =  ?";

                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID_chr.Trim();

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strChildrenResultXml, ref p_intChildrenResultRows, objDPArr);

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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_strAreaXML"></param>
        /// <param name="p_intAreaRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptForAreaInfo(string p_strDeptID, ref string p_strAreaXML, ref int p_intAreaRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetDeptForAreaInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                #region 旧表
                //				string 	strSQL = @"select distinct IAD2.Area_ID,IAD2.Area_Name
                //							from InPatient_Area_Dept IAD1,InPatient_Area_Desc IAD2
                //							where IAD1.Area_ID = IAD2.Area_ID  and IAD1.DepartmentID='"+p_strDeptID+@"'
                //							and IAD1.End_Date_Dept = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+" and IAD2.End_Date_Area_Naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat();
                #endregion

                //bhuang	20051124
                string strSQL = @"select distinct shortno_chr as area_id,deptname_vchr as area_name, deptid_chr areaid_chr from t_bse_deptdesc 
where parentid = (select deptid_chr from t_bse_deptdesc where shortno_chr = ?) and attributeid = '0000003'";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                return objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strAreaXML, ref p_intAreaRows, objDPArr);

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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strRoomXML"></param>
        /// <param name="p_intRoomRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaForRoomInfo(string p_strAreaID, ref string p_strRoomXML, ref int p_intRoomRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetAreaForRoomInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select i2.room_id,i2.room_name
							from inpatient_room_area i1,inpatient_room_desc i2
							where i1.room_id = i2.room_id  and i1.area_id=?
							and i1.end_date_room_area = ? and i2.end_date_room_naming = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strAreaID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900, 1, 1);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = new DateTime(1900, 1, 1);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strRoomXML, ref p_intRoomRows, objDPArr);
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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRoomID"></param>
        /// <param name="p_strBedXML"></param>
        /// <param name="p_intBedRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoomForBedInfo(string p_strRoomID, ref string p_strBedXML, ref int p_intBedRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetRoomForBedInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select i2.bed_id,i2.bed_name
							from inpatient_bed_room i1,inpatient_bed_desc i2
							where i1.bed_id = i2.bed_id  and i1.room_id=?
							and i1.end_date_bed_room = ? and i2.end_date_bed_naming = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strRoomID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900, 1, 1);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = new DateTime(1900, 1, 1);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strBedXML, ref p_intBedRows, objDPArr);

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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRoomID"></param>
        /// <param name="p_strBedXML"></param>
        /// <param name="p_intBedRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoomForBedAndPatientInfo(string p_strRoomID, ref string p_strBedXML, ref int p_intBedRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetRoomForBedAndPatientInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select i2.bed_id,
       i2.bed_name,
       ipdi.inpatientid,
       ipdi.inpatientdate,
       ipdi.inpatientenddate,
       p.patientid,
       p.firstname,
       p.lastname,
       p.idcard,
       p.sex,
       p.married,
       p.birth,
       p.chargecategory,
       p.paymentpercent,
       p.homeplace,
       p.nationality,
       p.nation,
       p.nativeplace,
       p.occupation,
       p.officephone,
       p.homephone,
       p.mobile,
       p.officeaddress,
       p.homeaddress,
       p.job,
       p.officepc,
       p.homepc,
       p.email,
       p.linkmanfirstname,
       p.linkmanlastname,
       p.linkmanaddress,
       p.linkmanphone,
       p.linkmanpc,
       p.patientrelation,
       p.firstdate,
       p.isemployee,
       p.status,
       p.deactivated_date,
       p.de_employeeid,
       p.times,
       p.hic_no,
       p.book_no,
       p.vip_code,
       p.office_name,
       p.office_district,
       p.office_street,
       p.linkman_district,
       p.linkman_street,
       p.home_district,
       p.home_street,
       p.temp_district,
       p.temp_street,
       p.temp_tel,
       p.temp_zipcode,
       p.insurance,
       p.admiss_diag_str,
       p.admiss_status,
       p.visit_type
  from inpatient_bed_room i1
 inner join inpatient_bed_desc i2 on i1.bed_id = i2.bed_id
  left outer join indeptinfo idi on i1.bed_id = idi.bed_id
                                and (idi.inbedenddate = ?)
  left outer join inpatientdateinfo ipdi on idi.inpatientid =
                                            ipdi.inpatientid
                                        and idi.inpatientdate =
                                            ipdi.inpatientdate
                                        and (ipdi.inpatientenddate = ?)
  left outer join patientbaseinfo p on p.inpatientid = idi.inpatientid
 where (i1.room_id = ?)
   and (i1.end_date_bed_room = ?)
   and (i2.end_date_bed_naming = ?)";

                DateTime dtFormat = new DateTime(1900, 1, 1);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = dtFormat;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = dtFormat;
                objDPArr[2].Value = p_strRoomID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = dtFormat;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = dtFormat;

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strBedXML, ref p_intBedRows, objDPArr);

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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRoomID"></param>
        /// <param name="p_strBedXML"></param>
        /// <param name="p_intBedRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoomForPatientInfo(string p_strRoomID, ref string p_strBedXML, ref int p_intBedRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetRoomForPatientInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select ipdi.inpatientid as inpid,
       ipdi.inpatientdate,
       ipdi.inpatientenddate,
       p.patientid,
       p.firstname,
       p.lastname,
       p.idcard,
       p.sex,
       p.married,
       p.birth,
       p.chargecategory,
       p.paymentpercent,
       p.homeplace,
       p.nationality,
       p.nation,
       p.nativeplace,
       p.occupation,
       p.officephone,
       p.homephone,
       p.mobile,
       p.officeaddress,
       p.homeaddress,
       p.job,
       p.officepc,
       p.homepc,
       p.email,
       p.linkmanfirstname,
       p.linkmanlastname,
       p.linkmanaddress,
       p.linkmanphone,
       p.linkmanpc,
       p.patientrelation,
       p.firstdate,
       p.isemployee,
       p.status,
       p.deactivated_date,
       p.de_employeeid,
       p.times,
       p.hic_no,
       p.book_no,
       p.vip_code,
       p.office_name,
       p.office_district,
       p.office_street,
       p.linkman_district,
       p.linkman_street,
       p.home_district,
       p.home_street,
       p.temp_district,
       p.temp_street,
       p.temp_tel,
       p.temp_zipcode,
       p.insurance,
       p.admiss_diag_str,
       p.admiss_status,
       p.visit_type
  from indeptinfo idi
 inner join inpatientdateinfo ipdi on idi.inpatientid = ipdi.inpatientid
                                  and idi.inpatientdate =
                                      ipdi.inpatientdate
 inner join patientbaseinfo p on p.inpatientid = idi.inpatientid
 where (idi.room_id = ?)
   and (ipdi.inpatientenddate = ?)
   and (idi.inbedenddate = ?)";
                DateTime dtFormat = new DateTime(1900, 1, 1);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strRoomID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = dtFormat;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = dtFormat;

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strBedXML, ref p_intBedRows, objDPArr);

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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strBedXML"></param>
        /// <param name="p_intBedRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaForBedAndPatientInfo(string p_strAreaID, ref string p_strBedXML, ref int p_intBedRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetAreaForBedAndPatientInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;


                //由于添加了病人床头卡信息为了方便读取此处改为以下形式
                #region old method
                string strSQL = null;
                //				if (clsHRPTableService.bytDatabase_Selector==0)
                //					strSQL = @"SELECT i2.Bed_ID,i2.Bed_Name,IPDI.InPatientID,
                //											IPDI.InPatientDate,IPDI.InPatientEndDate,p.* ,IDI.Bed_Status,case when IBI.STATE is null then'无' when IBI.STATE =0 then '稳定'when IBI.STATE =1 then '慢性'when IBI.STATE =2 then '病重' when IBI.STATE =3 then '病危' end BedClipState
                //										FROM InPatient_Room_Area ra INNER JOIN
                //											InPatient_Bed_Room i1 ON ra.Room_ID = i1.Room_ID INNER JOIN									
                //											InPatient_Bed_Desc i2 ON i1.Bed_ID = i2.Bed_ID LEFT OUTER JOIN
                //											InDeptInfo IDI ON i1.Bed_ID = IDI.Bed_ID  AND 
                //											(IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@") LEFT OUTER JOIN
                //											InPatientDateInfo IPDI ON IDI.InPatientID = IPDI.InPatientID AND 
                //											IDI.InPatientDate = IPDI.InPatientDate AND (IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")
                //											LEFT OUTER JOIN PatientBaseInfo p ON p.InPatientID=IDI.InPatientID
                //											LEFT OUTER JOIN (SELECT * FROM INPAT_BEDINFO where OPENDATE IN (SELECT MAX(OPENDATE) FROM INPAT_BEDINFO group by inpatientid,INPATIENTDATE))IBI ON (IPDI.INPATIENTID=IBI.INPATIENTID AND IPDI.INPATIENTDATE=IBI.INPATIENTDATE)
                //										WHERE (ra.Area_ID = '"+p_strAreaID+@"') AND ra.End_Date_Room_Area = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" AND (i1.End_Date_Bed_Room = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@") AND 
                //											(i2.End_Date_Bed_Naming = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";
                //
                //				else
                //					strSQL = @"SELECT i2.Bed_ID,i2.Bed_Name,IPDI.InPatientID,
                //											IPDI.InPatientDate,IPDI.InPatientEndDate,p.* ,IDI.Bed_Status,decode(IBI.STATE,null,'无',0,'稳定',1,'慢性',2,'病重',3,'病危') BedClipState
                //										FROM InPatient_Room_Area ra INNER JOIN
                //											InPatient_Bed_Room i1 ON ra.Room_ID = i1.Room_ID INNER JOIN									
                //											InPatient_Bed_Desc i2 ON i1.Bed_ID = i2.Bed_ID LEFT OUTER JOIN
                //											InDeptInfo IDI ON i1.Bed_ID = IDI.Bed_ID  AND 
                //											(IDI.InBedEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@") LEFT OUTER JOIN
                //											InPatientDateInfo IPDI ON IDI.InPatientID = IPDI.InPatientID AND 
                //											IDI.InPatientDate = IPDI.InPatientDate AND (IPDI.InPatientEndDate = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")
                //											LEFT OUTER JOIN PatientBaseInfo p ON p.InPatientID=IDI.InPatientID
                //											LEFT OUTER JOIN (SELECT * FROM INPAT_BEDINFO where OPENDATE IN (SELECT MAX(OPENDATE) FROM INPAT_BEDINFO group by inpatientid,INPATIENTDATE))IBI ON (IPDI.INPATIENTID=IBI.INPATIENTID AND IPDI.INPATIENTDATE=IBI.INPATIENTDATE)
                //										WHERE (ra.Area_ID = '"+p_strAreaID+@"') AND ra.End_Date_Room_Area = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" AND (i1.End_Date_Bed_Room = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@") AND 
                //											(i2.End_Date_Bed_Naming = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";
                #endregion
                strSQL = @"select b.lastname_vchr   as lastname_vchr,
       b.birth_dat       as birth_dat,
       b.sex_chr         as sex_chr,
       d.deptname_vchr   as deptname_vchr,
       c.code_chr        as code_chr,
       a.inpatientid_chr as inpatientid_chr,
       a.mzdiagnose_vchr as mzdiagnose_vchr
  from t_opr_bih_register a
 inner join t_opr_bih_registerdetail b on a.registerid_chr =
                                          b.registerid_chr
 inner join t_bse_bed c on a.bedid_chr = c.bedid_chr
 inner join t_bse_deptdesc d on c.areaid_chr = d.deptid_chr
 where a.status_int = 1
   and b.status_int = 1
   and c.status_int <> 5
   and d.status_int = 1
   and c.areaid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strAreaID;

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strBedXML, ref p_intBedRows, objDPArr);

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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strBed_Name"></param>
        /// <param name="p_strBedXML"></param>
        /// <param name="p_intBedRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetNextPatientInfoInArea(string p_strAreaID, string p_strBed_Name, ref string p_strBedXML, ref int p_intBedRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetAreaForBedAndPatientInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select base.bed_id,
       base.bed_name,
       base.inpatientid,
       base.inpatientdate,
       base.inpatientenddate,
       base.patientid,
       base.firstname,
       base.lastname,
       base.idcard,
       base.sex,
       base.married,
       base.birth,
       base.chargecategory,
       base.paymentpercent,
       base.homeplace,
       base.nationality,
       base.nation,
       base.nativeplace,
       base.occupation,
       base.officephone,
       base.homephone,
       base.mobile,
       base.officeaddress,
       base.homeaddress,
       base.job,
       base.officepc,
       base.homepc,
       base.email,
       base.linkmanfirstname,
       base.linkmanlastname,
       base.linkmanaddress,
       base.linkmanphone,
       base.linkmanpc,
       base.patientrelation,
       base.firstdate,
       base.isemployee,
       base.status,
       base.deactivated_date,
       base.de_employeeid,
       base.times,
       base.hic_no,
       base.book_no,
       base.vip_code,
       base.office_name,
       base.office_district,
       base.office_street,
       base.linkman_district,
       base.linkman_street,
       base.home_district,
       base.home_street,
       base.temp_district,
       base.temp_street,
       base.temp_tel,
       base.temp_zipcode,
       base.insurance,
       base.admiss_diag_str,
       base.admiss_status,
       base.visit_type,
       base.bed_status
  from (select bed_id,
               bed_name,
               inpatientid,
               inpatientdate,
               inpatientenddate,
               patientid,
               firstname,
               lastname,
               idcard,
               sex,
               married,
               birth,
               chargecategory,
               paymentpercent,
               homeplace,
               nationality,
               nation,
               nativeplace,
               occupation,
               officephone,
               homephone,
               mobile,
               officeaddress,
               homeaddress,
               job,
               officepc,
               homepc,
               email,
               linkmanfirstname,
               linkmanlastname,
               linkmanaddress,
               linkmanphone,
               linkmanpc,
               patientrelation,
               firstdate,
               isemployee,
               status,
               deactivated_date,
               de_employeeid,
               times,
               hic_no,
               book_no,
               vip_code,
               office_name,
               office_district,
               office_street,
               linkman_district,
               linkman_street,
               home_district,
               home_street,
               temp_district,
               temp_street,
               temp_tel,
               temp_zipcode,
               insurance,
               admiss_diag_str,
               admiss_status,
               visit_type,
               bed_status
          from (select i2.bed_id,
                       i2.bed_name,
                       ipdi.inpatientid,
                       ipdi.inpatientdate,
                       ipdi.inpatientenddate,
                       p.patientid,
                       p.firstname,
                       p.lastname,
                       p.idcard,
                       p.sex,
                       p.married,
                       p.birth,
                       p.chargecategory,
                       p.paymentpercent,
                       p.homeplace,
                       p.nationality,
                       p.nation,
                       p.nativeplace,
                       p.occupation,
                       p.officephone,
                       p.homephone,
                       p.mobile,
                       p.officeaddress,
                       p.homeaddress,
                       p.job,
                       p.officepc,
                       p.homepc,
                       p.email,
                       p.linkmanfirstname,
                       p.linkmanlastname,
                       p.linkmanaddress,
                       p.linkmanphone,
                       p.linkmanpc,
                       p.patientrelation,
                       p.firstdate,
                       p.isemployee,
                       p.status,
                       p.deactivated_date,
                       p.de_employeeid,
                       p.times,
                       p.hic_no,
                       p.book_no,
                       p.vip_code,
                       p.office_name,
                       p.office_district,
                       p.office_street,
                       p.linkman_district,
                       p.linkman_street,
                       p.home_district,
                       p.home_street,
                       p.temp_district,
                       p.temp_street,
                       p.temp_tel,
                       p.temp_zipcode,
                       p.insurance,
                       p.admiss_diag_str,
                       p.admiss_status,
                       p.visit_type,
                       idi.bed_status
                  from inpatient_room_area ra
                 inner join inpatient_bed_room i1 on ra.room_id = i1.room_id
                 inner join inpatient_bed_desc i2 on i1.bed_id = i2.bed_id
                  left outer join indeptinfo idi on i1.bed_id = idi.bed_id
                                                and (idi.inbedenddate = ?)
                  left outer join inpatientdateinfo ipdi on idi.inpatientid =
                                                            ipdi.inpatientid
                                                        and idi.inpatientdate =
                                                            ipdi.inpatientdate
                                                        and (ipdi.inpatientenddate = ?)
                  left outer join patientbaseinfo p on p.inpatientid =
                                                       idi.inpatientid
                 where (ra.area_id = ?)
                   and ra.end_date_room_area = ?
                   and (i1.end_date_bed_room = ?)
                   and (i2.end_date_bed_naming = ?)) v1
         where v1.inpatientid is not null
           and v1.bed_name > ?
         order by v1.bed_id) base
 where rownum = 1";

                DateTime dtFormat = new DateTime(1900, 1, 1);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(7, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = dtFormat;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = dtFormat;
                objDPArr[2].Value = p_strAreaID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = dtFormat;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = dtFormat;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = dtFormat;
                objDPArr[6].Value = p_strBed_Name;

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strBedXML, ref p_intBedRows, objDPArr);

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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strBed_Name"></param>
        /// <param name="p_strBedXML"></param>
        /// <param name="p_intBedRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPreviousPatientInfoInArea(string p_strAreaID, string p_strBed_Name, ref string p_strBedXML, ref int p_intBedRows)
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentManagerService","m_lngGetAreaForBedAndPatientInfo");
            //if (lngCheckRes <= 0)
            //return lngCheckRes;

            clsHRPTableService objHRPServ = new clsHRPTableService();

            string strSQL = @"select base.bed_id,
       base.bed_name,
       base.inpatientid,
       base.inpatientdate,
       base.inpatientenddate,
       base.patientid,
       base.firstname,
       base.lastname,
       base.idcard,
       base.sex,
       base.married,
       base.birth,
       base.chargecategory,
       base.paymentpercent,
       base.homeplace,
       base.nationality,
       base.nation,
       base.nativeplace,
       base.occupation,
       base.officephone,
       base.homephone,
       base.mobile,
       base.officeaddress,
       base.homeaddress,
       base.job,
       base.officepc,
       base.homepc,
       base.email,
       base.linkmanfirstname,
       base.linkmanlastname,
       base.linkmanaddress,
       base.linkmanphone,
       base.linkmanpc,
       base.patientrelation,
       base.firstdate,
       base.isemployee,
       base.status,
       base.deactivated_date,
       base.de_employeeid,
       base.times,
       base.hic_no,
       base.book_no,
       base.vip_code,
       base.office_name,
       base.office_district,
       base.office_street,
       base.linkman_district,
       base.linkman_street,
       base.home_district,
       base.home_street,
       base.temp_district,
       base.temp_street,
       base.temp_tel,
       base.temp_zipcode,
       base.insurance,
       base.admiss_diag_str,
       base.admiss_status,
       base.visit_type,
       base.bed_status
  from (select bed_id,
               bed_name,
               inpatientid,
               inpatientdate,
               inpatientenddate,
               patientid,
               firstname,
               lastname,
               idcard,
               sex,
               married,
               birth,
               chargecategory,
               paymentpercent,
               homeplace,
               nationality,
               nation,
               nativeplace,
               occupation,
               officephone,
               homephone,
               mobile,
               officeaddress,
               homeaddress,
               job,
               officepc,
               homepc,
               email,
               linkmanfirstname,
               linkmanlastname,
               linkmanaddress,
               linkmanphone,
               linkmanpc,
               patientrelation,
               firstdate,
               isemployee,
               status,
               deactivated_date,
               de_employeeid,
               times,
               hic_no,
               book_no,
               vip_code,
               office_name,
               office_district,
               office_street,
               linkman_district,
               linkman_street,
               home_district,
               home_street,
               temp_district,
               temp_street,
               temp_tel,
               temp_zipcode,
               insurance,
               admiss_diag_str,
               admiss_status,
               visit_type,
               bed_status
          from (select i2.bed_id,
                       i2.bed_name,
                       ipdi.inpatientid,
                       ipdi.inpatientdate,
                       ipdi.inpatientenddate,
                       p.patientid,
                       p.firstname,
                       p.lastname,
                       p.idcard,
                       p.sex,
                       p.married,
                       p.birth,
                       p.chargecategory,
                       p.paymentpercent,
                       p.homeplace,
                       p.nationality,
                       p.nation,
                       p.nativeplace,
                       p.occupation,
                       p.officephone,
                       p.homephone,
                       p.mobile,
                       p.officeaddress,
                       p.homeaddress,
                       p.job,
                       p.officepc,
                       p.homepc,
                       p.email,
                       p.linkmanfirstname,
                       p.linkmanlastname,
                       p.linkmanaddress,
                       p.linkmanphone,
                       p.linkmanpc,
                       p.patientrelation,
                       p.firstdate,
                       p.isemployee,
                       p.status,
                       p.deactivated_date,
                       p.de_employeeid,
                       p.times,
                       p.hic_no,
                       p.book_no,
                       p.vip_code,
                       p.office_name,
                       p.office_district,
                       p.office_street,
                       p.linkman_district,
                       p.linkman_street,
                       p.home_district,
                       p.home_street,
                       p.temp_district,
                       p.temp_street,
                       p.temp_tel,
                       p.temp_zipcode,
                       p.insurance,
                       p.admiss_diag_str,
                       p.admiss_status,
                       p.visit_type,
                       idi.bed_status
                  from inpatient_room_area ra
                 inner join inpatient_bed_room i1 on ra.room_id = i1.room_id
                 inner join inpatient_bed_desc i2 on i1.bed_id = i2.bed_id
                  left outer join indeptinfo idi on i1.bed_id = idi.bed_id
                                                and (idi.inbedenddate = ?)
                  left outer join inpatientdateinfo ipdi on idi.inpatientid =
                                                            ipdi.inpatientid
                                                        and idi.inpatientdate =
                                                            ipdi.inpatientdate
                                                        and (ipdi.inpatientenddate = ?)
                  left outer join patientbaseinfo p on p.inpatientid =
                                                       idi.inpatientid
                 where (ra.area_id = ?)
                   and ra.end_date_room_area = ?
                   and (i1.end_date_bed_room = ?)
                   and (i2.end_date_bed_naming = ?)) v1
         where v1.inpatientid is not null
           and v1.bed_name < ?
         order by v1.bed_id desc) base
 where rownum = 1";
            //objHRPServ.Dispose();

            DateTime dtFormat = new DateTime(1900, 1, 1);
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(7, out objDPArr);
            objDPArr[0].DbType = DbType.DateTime;
            objDPArr[0].Value = dtFormat;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = dtFormat;
            objDPArr[2].Value = p_strAreaID;
            objDPArr[3].DbType = DbType.DateTime;
            objDPArr[3].Value = dtFormat;
            objDPArr[4].DbType = DbType.DateTime;
            objDPArr[4].Value = dtFormat;
            objDPArr[5].DbType = DbType.DateTime;
            objDPArr[5].Value = dtFormat;
            objDPArr[6].Value = p_strBed_Name;

            return objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strBedXML, ref p_intBedRows, objDPArr);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllInDept(ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetAllInDept");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;
                //				//暂时使用SHORTNO_CHR，模板插入ID，查询使用的是SHORTNO_CHR  －－－bhuang  20051117
                string strSQL = @"select shortno_chr as deptid, deptname_vchr as deptname, deptid_chr
									from t_bse_deptdesc
									where inpatientoroutpatient_int = 1
									and status_int = 1
									and attributeid = '0000002'";

                lngRes = clsTablService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //clsTablService.Dispose();
            }
            //返回
            return lngRes;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllInDept1(ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetAllInDept");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //				string strSQL = @" select dd.DEPTID_CHR as DeptID, dd.DEPTNAME_VCHR as DeptName
                //										from t_bse_deptdesc dd,
                //										(select Max(MODIFY_DAT) as ModifyDate, DEPTID_CHR
                //										from t_bse_deptdesc
                //										group by DEPTID_CHR) DeptSelect
                //										where dd.DEPTID_CHR = DeptSelect.DEPTID_CHR
                //										and dd.MODIFY_DAT = DeptSelect.ModifyDate
                //										and dd.ATTRIBUTEID = '0000002'
                //										order by DeptName desc";

                string strSQL = @" select dd.deptid_chr as deptid,dd.shortno_chr as shortno, dd.deptname_vchr as deptname
									from t_bse_deptdesc dd
									where inpatientoroutpatient_int = 1
									and dd.attributeid = '0000002'
									and dd.status_int = 1
									order by deptname desc";

                lngRes = clsTablService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //clsTablService.Dispose();
            }
            //返回
            return lngRes;


        }

        /// <summary>
        /// 获取所有有效科室(改为病区过滤后获取所有病区)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllDept(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                string strSQL = @" select dd.deptid_chr as deptid,dd.shortno_chr as shortno, dd.deptname_vchr as deptname,dd.extendid_vchr,dd.code_vchr,dd.pycode_chr
									from t_bse_deptdesc dd
									where dd.attributeid = '0000003'
                                    and (inpatientoroutpatient_int = 0
                                    or inpatientoroutpatient_int = 1)
									and dd.status_int = 1
									order by inpatientoroutpatient_int desc,deptid";

                lngRes = clsTablService.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptLikeString"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptLikeQuery(string p_strDeptLikeString, ref string p_strResultXml, ref int p_intResultRows)
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentManagerService","m_lngGetDeptLikeQuery");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;
            clsHRPTableService clsTablService = new clsHRPTableService();
            string strSQL = @"select deptdesc.deptid, deptdesc.deptname
  from (select dd.deptid,
               dd.modifydate,
               dd.deptname
          from dept_desc dd, deptbaseinfo dbi
         where dd.deptid = dbi.deptid
           and dbi.status = 0
           and dd.inpatientoroutpatient = 1
           and dd.category = 0) deptdesc,
       (select max(modifydate) as modifydate, deptid
          from dept_desc
         group by deptid) deptselect
 where deptdesc.deptid = deptselect.deptid
   and deptdesc.modifydate = deptselect.modifydate";
            try
            {
                long.Parse(p_strDeptLikeString);
                strSQL += "	and DeptDesc.DeptID like '" + p_strDeptLikeString + "%'";
                return clsTablService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);
            }
            catch
            {
                strSQL += "	and DeptDesc.DeptName like '" + p_strDeptLikeString + "%'";
                return clsTablService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strResultXml"></param>
        /// <param name="p_intResultRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllDeptAndAreaInfo(ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentManagerService","m_lngGetAllDeptAndAreaInfo");
                //				if(lngCheckRes <= 0)
                //					//return lngCheckRes;

                //				string strSQL = @"select distinct DeptDesc.DeptID,DeptDesc.DeptName,IAD2.Area_ID,IAD2.Area_Name
                //									from
                //									(select DD.*
                //									from Dept_Desc DD,DeptBaseInfo DBI
                //									where DD.DeptID = DBI.DeptID
                //									and DBI.Status = 0
                //									and DD.InPatientOrOutPatient = 1
                //									and DD.Category=0
                //									)DeptDesc Inner Join 
                //									(select Max(ModifyDate) as ModifyDate,DeptID
                //									from Dept_Desc
                //									group by DeptID
                //									)DeptSelect
                //									On DeptDesc.DeptID = DeptSelect.DeptID
                //									and DeptDesc.ModifyDate = DeptSelect.ModifyDate
                //								Left Outer Join InPatient_Area_Dept IAD1
                //								On IAD1.DepartmentID=DeptSelect.DeptID
                //								and IAD1.End_Date_Dept = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"
                //								Left Outer Join InPatient_Area_Desc IAD2
                //								On IAD1.Area_ID = IAD2.Area_ID
                //								and IAD2.End_Date_Area_Naming = "+ clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() +" order by DeptDesc.deptid";
                string strSQL = @"select c.deptid_chr    as deptid,
										c.deptname_vchr as deptname,
										c.shortno_chr   as deptshortno,
										b.deptid_chr    as area_id,
										b.deptname_vchr as area_name,
										b.shortno_chr   as areashortno
									from t_bse_deptdesc c
									left outer join t_bse_deptdesc b on c.deptid_chr = b.parentid
																	and b.status_int = 1
																	and b.inpatientoroutpatient_int = 1
																	and b.category_int = 0
																	and b.attributeid = '0000003'
									where c.status_int = 1
									and c.inpatientoroutpatient_int = 1
									and c.category_int = 0
									and c.attributeid = '0000002'";


                lngRes = clsTablService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //clsTablService.Dispose();
            }
            //返回
            return lngRes;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientIDArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientArrThatNotHasBed(out string[] p_strPatientIDArr)
        {

            p_strPatientIDArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetPatientArrThatNotHasBed");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select distinct inpatientid from inpatientdateinfo where (inpatientenddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")
									and inpatientid not in	(select inpatientid from indeptinfo where (inbedenddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")	) ";

                //鎵цSQL	
                DataTable dtbResult = null;
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_strPatientIDArr = new string[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                        p_strPatientIDArr[i] = dtbResult.Rows[i]["INPATIENTID"].ToString();
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

        //重载返回没有分配床号的病人ID与姓名2005-10-12
        [AutoComplete]
        public long m_lngGetPatientArrThatNotHasBed(out DataTable dtResult)
        {

            dtResult = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetPatientArrThatNotHasBed");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select distinct inpatientdateinfo.inpatientid,b.firstname from inpatientdateinfo inner join patientbaseinfo b on inpatientdateinfo.inpatientid=b.inpatientid  where (inpatientenddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")
									and inpatientdateinfo.inpatientid not in	(select inpatientid from indeptinfo where (inbedenddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")	) ";

                //鎵цSQL	
                //DataTable dtbResult = null;
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtResult);
                //if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                //{
                //    p_strPatientIDArr = new string[dtbResult.Rows.Count, 2];
                //    for (int i = 0; i < dtbResult.Rows.Count; i++)
                //    {
                //        p_strPatientIDArr[i, 0] = dtbResult.Rows[i]["INPATIENTID"].ToString();
                //        p_strPatientIDArr[i, 1] = dtbResult.Rows[i]["FIRSTNAME"].ToString();
                //    }
                //}
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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_objAreaArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaArr_EmployeeCanManage(string p_strEmployeeID, out clsArea_Desc[] p_objAreaArr)
        {
            p_objAreaArr = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetAreaArr_EmployeeCanManage");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select a.area_id,b.area_name,a.departmentid ,c. deptname from inpatient_area_employee a ,inpatient_area_desc b,dept_desc c where a.employee_id=?
									and a.end_date_employee_area=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @" and b.area_id=a.area_id and b.end_date_area_naming=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @"
									and c.deptid=a.departmentid";

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID;

                //鎵цSQL	
                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objAreaArr = new clsArea_Desc[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objAreaArr[i] = new clsArea_Desc();
                        p_objAreaArr[i].m_strArea_ID = dtbResult.Rows[i]["AREA_ID"].ToString();
                        p_objAreaArr[i].m_strArea_Name = dtbResult.Rows[i]["AREA_NAME"].ToString();
                        p_objAreaArr[i].m_strParentDeptID = dtbResult.Rows[i]["DEPARTMENTID"].ToString();
                        p_objAreaArr[i].m_strParentDeptName = dtbResult.Rows[i]["DEPTNAME"].ToString();

                    }
                }
                return lngRes;

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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_objEmployeeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeArrInArea(string p_strAreaID, out clsEmployee_BaseInfo[] p_objEmployeeArr)
        {
            p_objEmployeeArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetEmployeeArrInArea");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select a.employee_id,b.firstname,b.sex from inpatient_area_employee a ,employeebaseinfo b where a.area_id=?
									and a.end_date_employee_area=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @" and b.employeeid=a.employee_id and b.status=0 ";


                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strAreaID;

                //鎵цSQL	
                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objEmployeeArr = new clsEmployee_BaseInfo[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objEmployeeArr[i] = new clsEmployee_BaseInfo();
                        p_objEmployeeArr[i].m_strEmployeeID = dtbResult.Rows[i]["EMPLOYEE_ID"].ToString();
                        p_objEmployeeArr[i].m_strFirstName = dtbResult.Rows[i]["FIRSTNAME"].ToString();
                        p_objEmployeeArr[i].m_strSex = dtbResult.Rows[i]["SEX"].ToString();
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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_objDeptArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptArr_EmployeeCanManage(string p_strEmployeeID, out clsDept_Desc[] p_objDeptArr)
        {
            p_objDeptArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetDeptArr_EmployeeCanManage");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;
                string strSQL = @"select c.deptid_chr,
       c.modify_dat,
       c.deptname_vchr,
       c.category_int,
       c.inpatientoroutpatient_int,
       c.operatorid_chr,
       c.address_vchr,
       c.pycode_chr,
       c.attributeid,
       c.parentid,
       c.createdate_dat,
       c.status_int,
       c.deactivate_dat,
       c.wbcode_chr,
       c.code_vchr,
       c.extendid_vchr,
       c.shortno_chr,
       c.stdbed_count_int,
       c.putmed_int
  from t_bse_deptemp a, t_bse_employee b, t_bse_deptdesc c
 where a.empid_chr = b.empid_chr
   and b.empno_chr = ?
   and c.status_int = 1
   and (a.end_dat = ? or a.end_dat is null)
   and c.deptid_chr = a.deptid_chr
   and c.inpatientoroutpatient_int = 1
   and c.category_int = 0
   and c.attributeid = '0000002'
   and c.modify_dat = (select max(modify_dat)
                         from t_bse_deptdesc
                        where deptid_chr = a.deptid_chr)
 order by c.deptid_chr";


                //鎵цSQL	
                DataTable dtbResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900, 1, 1);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && dtbResult != null && intRowCount > 0)
                {
                    p_objDeptArr = new clsDept_Desc[intRowCount];
                    DataRow objRow = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        clsDept_Desc objDesc = new clsDept_Desc();
                        objRow = dtbResult.Rows[i];
                        objDesc.m_strDeptID = objRow["SHORTNO_CHR"].ToString();
                        objDesc.m_strDeptName = objRow["DEPTNAME_VCHR"].ToString();
                        objDesc.m_dtmCreateDate = DateTime.Parse(objRow["CREATEDATE_DAT"].ToString());
                        objDesc.m_dtmModifyDate = DateTime.Parse(objRow["MODIFY_DAT"].ToString());
                        objDesc.m_strAddress = objRow["ADDRESS_VCHR"].ToString();
                        objDesc.m_strCategory = objRow["CATEGORY_INT"].ToString();
                        objDesc.m_strInPatientOrOutPatient = objRow["INPATIENTOROUTPATIENT_INT"].ToString();
                        objDesc.m_strPYCode = objRow["PYCODE_CHR"].ToString();
                        objDesc.m_strShortNO = objRow["SHORTNO_CHR"].ToString();
                        objDesc.m_strDeptNewID = objRow["DEPTID_CHR"].ToString();
                        p_objDeptArr[i] = objDesc;
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
        /// 获取与当前员工所属科室有关的所有科室(通过查周转表)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_objDeptArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRelationDeptArr(string p_strEmployeeID, out clsDept_Desc[] p_objDeptArr)
        {
            p_objDeptArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetRelationDeptArr");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                #region SQL
                string strSQL = @"select tdp.deptid_chr,
       tdp.modify_dat,
       tdp.deptname_vchr,
       tdp.category_int,
       tdp.inpatientoroutpatient_int,
       tdp.operatorid_chr,
       tdp.address_vchr,
       tdp.pycode_chr,
       tdp.attributeid,
       tdp.parentid,
       tdp.createdate_dat,
       tdp.status_int,
       tdp.deactivate_dat,
       tdp.wbcode_chr,
       tdp.code_vchr,
       tdp.extendid_vchr,
       tdp.shortno_chr,
       tdp.stdbed_count_int,
       tdp.putmed_int
  from (select distinct a.targetdeptid_chr as id
          from t_opr_bih_transfer a
         inner join (select c.deptid_chr
                      from t_bse_deptemp  a,
                           t_bse_employee b,
                           t_bse_deptdesc c
                     where a.empid_chr = b.empid_chr
                       and b.empno_chr = ?
                       and c.status_int = 1
                       and (a.end_dat = ? or a.end_dat is null)
                       and c.deptid_chr = a.deptid_chr
                       and c.inpatientoroutpatient_int = 1
                       and c.category_int = 0
                       and c.attributeid = '0000002'
                       and c.modify_dat =
                           (select max(modify_dat)
                              from t_bse_deptdesc
                             where deptid_chr = a.deptid_chr)) base on a.sourcedeptid_chr =
                                                                       base.deptid_chr
                                                                   and a.targetdeptid_chr is not null
         where a.type_int <> 1
        union
        
        select distinct a.sourcedeptid_chr as id
          from t_opr_bih_transfer a
         inner join (select c.deptid_chr
                       from t_bse_deptemp  a,
                            t_bse_employee b,
                            t_bse_deptdesc c
                      where a.empid_chr = b.empid_chr
                        and b.empno_chr = ?
                        and c.status_int = 1
                        and (a.end_dat = ? or a.end_dat is null)
                        and c.deptid_chr = a.deptid_chr
                        and c.inpatientoroutpatient_int = 1
                        and c.category_int = 0
                        and c.attributeid = '0000002'
                        and c.modify_dat =
                            (select max(modify_dat)
                               from t_bse_deptdesc
                              where deptid_chr = a.deptid_chr)) base on a.targetdeptid_chr =
                                                                        base.deptid_chr
                                                                    and a.sourcedeptid_chr is not null
         where a.type_int <> 1
        union
        
        select c.deptid_chr as id
          from t_bse_deptemp a, t_bse_employee b, t_bse_deptdesc c
         where a.empid_chr = b.empid_chr
           and b.empno_chr = ?
           and c.status_int = 1
           and (a.end_dat = ? or a.end_dat is null)
           and c.deptid_chr = a.deptid_chr
           and c.inpatientoroutpatient_int = 1
           and c.category_int = 0
           and c.attributeid = '0000002'
           and c.modify_dat =
               (select max(modify_dat)
                  from t_bse_deptdesc
                 where deptid_chr = a.deptid_chr)) tid
 inner join t_bse_deptdesc tdp on tid.id = tdp.deptid_chr";
                #endregion
                DateTime dtFormat = new DateTime(1900, 1, 1);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = dtFormat;
                objDPArr[2].Value = p_strEmployeeID.Trim();
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = dtFormat;
                objDPArr[4].Value = p_strEmployeeID.Trim();
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = dtFormat;

                //鎵цSQL	
                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objDeptArr = new clsDept_Desc[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objDeptArr[i] = new clsDept_Desc();
                        p_objDeptArr[i].m_strDeptID = dtbResult.Rows[i]["SHORTNO_CHR"].ToString();
                        p_objDeptArr[i].m_strDeptName = dtbResult.Rows[i]["DEPTNAME_VCHR"].ToString();
                        p_objDeptArr[i].m_dtmCreateDate = DateTime.Parse(dtbResult.Rows[i]["CREATEDATE_DAT"].ToString());
                        p_objDeptArr[i].m_dtmModifyDate = DateTime.Parse(dtbResult.Rows[i]["MODIFY_DAT"].ToString());
                        p_objDeptArr[i].m_strAddress = dtbResult.Rows[i]["ADDRESS_VCHR"].ToString();
                        p_objDeptArr[i].m_strCategory = dtbResult.Rows[i]["CATEGORY_INT"].ToString();
                        p_objDeptArr[i].m_strInPatientOrOutPatient = dtbResult.Rows[i]["INPATIENTOROUTPATIENT_INT"].ToString();
                        p_objDeptArr[i].m_strPYCode = dtbResult.Rows[i]["PYCODE_CHR"].ToString();
                        p_objDeptArr[i].m_strShortNO = dtbResult.Rows[i]["SHORTNO_CHR"].ToString();
                        p_objDeptArr[i].m_strDeptNewID = dtbResult.Rows[i]["DEPTID_CHR"].ToString();
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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_objEmployeeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeArrInDept(string p_strDeptID, out clsEmployee_BaseInfo[] p_objEmployeeArr)
        {
            p_objEmployeeArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetEmployeeArrInDept");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select a.employeeid,b.firstname,b.sex from dept_employee a ,employeebaseinfo b where  a.deptid =?
									and  b.employeeid = a.employeeid  and b.status=0 and (a.enddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + " or a.enddate is null)";


                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                //鎵цSQL	
                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objEmployeeArr = new clsEmployee_BaseInfo[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objEmployeeArr[i] = new clsEmployee_BaseInfo();
                        p_objEmployeeArr[i].m_strEmployeeID = dtbResult.Rows[i]["EMPLOYEEID"].ToString();
                        p_objEmployeeArr[i].m_strFirstName = dtbResult.Rows[i]["FIRSTNAME"].ToString();
                        p_objEmployeeArr[i].m_strSex = dtbResult.Rows[i]["SEX"].ToString();
                    }
                }
                return lngRes;

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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeLikeString"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_objEmployeeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeLikeArrInDept(string p_strEmployeeLikeString, string p_strDeptID, out clsEmployee_BaseInfo[] p_objEmployeeArr)
        {
            p_objEmployeeArr = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetEmployeeLikeArrInDept");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select a.employeeid,b.firstname,b.sex from dept_employee a ,employeebaseinfo b where a.deptid=? and a.employeeid like ?
									and b.employeeid=a.employeeid and b.status=0 and (a.enddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + " or a.enddate is null)";


                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strDeptID;
                objDPArr[1].Value = p_strEmployeeLikeString + "%";

                //鎵цSQL	
                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objEmployeeArr = new clsEmployee_BaseInfo[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objEmployeeArr[i] = new clsEmployee_BaseInfo();
                        p_objEmployeeArr[i].m_strEmployeeID = dtbResult.Rows[i]["EMPLOYEEID"].ToString();
                        p_objEmployeeArr[i].m_strFirstName = dtbResult.Rows[i]["FIRSTNAME"].ToString();
                        p_objEmployeeArr[i].m_strSex = dtbResult.Rows[i]["SEX"].ToString();
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
        /// 判断员工是否属于科室(true＝是)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_strEmpID"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnCheckEmpInDept(string p_strDeptID, string p_strEmpID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {


                string strSQL = @"select employeeid from dept_employee where (enddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + " or enddate is null) and employeeid = ? and deptid = ?";


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strEmpID;
                objDPArr[1].Value = p_strDeptID;


                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    return true;
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
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCaseType(string p_strDeptID, out clsInpatMedRec_Type_Dept[] p_objContentArr)
        {
            p_objContentArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //妫�鏌ュ弬鏁?
                if (p_strDeptID == null || p_strDeptID == "")
                    return (long)enmOperationResult.Parameter_Error;


                string strGetCaseTypeSQL = @"select distinct a.deptid,b.typeid ,b.typename from inpatmedrec_type_dept a inner join inpatmedrec_type b 
on a.typeid = b.typeid 
where deptid = ?  order by deptid";



                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;
                objDPArr[0].DbType = DbType.String;
                //				objDPArr[1].Value=p_strAreaID;

                //鎵цSQL	
                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetCaseTypeSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objContentArr = new clsInpatMedRec_Type_Dept[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objContentArr[i] = new clsInpatMedRec_Type_Dept();
                        p_objContentArr[i].m_strDeptID = dtbResult.Rows[i]["DEPTID"].ToString();
                        p_objContentArr[i].m_strArea_ID = "";//dtbResult.Rows[i]["AREA_ID"].ToString();
                        p_objContentArr[i].m_strTypeID = dtbResult.Rows[i]["TYPEID"].ToString();
                        p_objContentArr[i].m_strTypeName = dtbResult.Rows[i]["TYPENAME"].ToString();
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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_strBedXML"></param>
        /// <param name="p_intBedRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaForBedAndPatientLeaveInfo(string p_strAreaID, ref string p_strBedXML, ref int p_intBedRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentManagerService", "m_lngGetAreaForBedAndPatientInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                string strSQL = @"select distinct i2.bed_id,
                i2.bed_name,
                ipdi.inpatientid,
                ipdi.inpatientdate,
                ipdi.inpatientenddate,
                p.patientid,
                p.firstname,
                p.lastname,
                p.idcard,
                p.sex,
                p.married,
                p.birth,
                p.chargecategory,
                p.paymentpercent,
                p.homeplace,
                p.nationality,
                p.nation,
                p.nativeplace,
                p.occupation,
                p.officephone,
                p.homephone,
                p.mobile,
                p.officeaddress,
                p.homeaddress,
                p.job,
                p.officepc,
                p.homepc,
                p.email,
                p.linkmanfirstname,
                p.linkmanlastname,
                p.linkmanaddress,
                p.linkmanphone,
                p.linkmanpc,
                p.patientrelation,
                p.firstdate,
                p.isemployee,
                p.status,
                p.deactivated_date,
                p.de_employeeid,
                p.times,
                p.hic_no,
                p.book_no,
                p.vip_code,
                p.office_name,
                p.office_district,
                p.office_street,
                p.linkman_district,
                p.linkman_street,
                p.home_district,
                p.home_street,
                p.temp_district,
                p.temp_street,
                p.temp_tel,
                p.temp_zipcode,
                p.insurance,
                p.admiss_diag_str,
                p.admiss_status,
                p.visit_type,
                idi.bed_status
  from inpatient_room_area ra
 inner join inpatient_bed_room i1 on ra.room_id = i1.room_id
 inner join inpatient_bed_desc i2 on i1.bed_id = i2.bed_id
  left outer join indeptinfo idi on i1.bed_id = idi.bed_id
                                and (idi.inbedenddate <> " + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")
  LEFT OUTER JOIN inpatientdateinfo ipdi on idi.inpatientid =
                                            ipdi.inpatientid
                                        and idi.inpatientdate =
                                            ipdi.inpatientdate
                                        and (ipdi.inpatientenddate =
                                            idi.inbedenddate)
  left outer join patientbaseinfo p on p.inpatientid = idi.inpatientid
 where (ra.area_id = '" + p_strAreaID + @"')
   and ipdi.inpatientenddate in
       (select max(inpatientenddate)
          from inpatientdateinfo
         group by inpatientid)";

                lngRes = objHRPServ.lngGetXMLTable(strSQL, ref p_strBedXML, ref p_intBedRows);

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
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBedID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedStatus(string p_strBedID)
        {

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtResult = new DataTable();
                long lngCheckRes;

                string strSQLBedDelete = null;
                string strSQLBedOccupt = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQLBedDelete = @"select ipbr.bed_id,
       ipbr.room_id,
       ipbr.begin_date_bed_room,
       ipbr.end_date_bed_room
  from inpatient_bed_room ipbr
 where ipbr.bed_id ='" + p_strBedID + "' and  ipbr.end_date_bed_room <> '1900-1-1'";

                    strSQLBedOccupt = @"select idif.inpatientid,
       idif.inpatientdate,
       idif.indeptid,
       idif.area_id,
       idif.room_id,
       idif.bed_id,
       idif.modifydate,
       idif.inbedenddate,
       idif.begin_date_area_dept,
       idif.begin_date_room_area,
       idif.begin_date_bed_room,
       idif.bed_status
  from indeptinfo idif
 where idif.bed_id ='" + p_strBedID + "' and idif.inbedenddate = '1900-1-1'";

                }
                else
                {
                    strSQLBedDelete = @"select ipbr.bed_id,
       ipbr.room_id,
       ipbr.begin_date_bed_room,
       ipbr.end_date_bed_room
  from inpatient_bed_room ipbr
 where ipbr.bed_id ='" + p_strBedID + "' and  ipbr.end_date_bed_room <> to_date('1900-1-1','yyyy-mm-dd')";

                    strSQLBedOccupt = @"select inpatientid,
       inpatientdate,
       indeptid,
       area_id,
       room_id,
       bed_id,
       modifydate,
       inbedenddate,
       begin_date_area_dept,
       begin_date_room_area,
       begin_date_bed_room,
       bed_status
  from indeptinfo idif
 where idif.bed_id ='" + p_strBedID + "' and idif.inbedenddate = to_date('1900-1-1','yyyy-mm-dd')";

                }

                lngCheckRes = objHRPServ.lngGetDataTableWithoutParameters(strSQLBedDelete, ref dtResult);
                if (lngCheckRes > 0 && dtResult.Rows.Count > 0)
                {
                    return 1;
                }
                lngCheckRes = objHRPServ.lngGetDataTableWithoutParameters(strSQLBedOccupt, ref dtResult);
                if (lngCheckRes > 0 && dtResult.Rows.Count > 0)
                {
                    return 2;
                }

                lngRes = 3;

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
        /// 
        /// </summary>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strEndBedDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUnDoLeave(string p_strPatientId, string p_strEndBedDate)
        {

            string strSQLInpatientarchivingDelete = null;
            string strSQLInPatientDateInfoUpDate = null;
            string strSQLInDeptInfoUpdate = null;

            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQLInpatientarchivingDelete = @"delete from inpatientarchiving where inpatientid ='" + p_strPatientId + "' and opendate ='" + p_strEndBedDate + "'";
                strSQLInPatientDateInfoUpDate = @"update inpatientdateinfo set inpatientenddate ='1900-1-1' where inpatientid ='" + p_strPatientId + "' and inpatientenddate ='" + p_strEndBedDate + "'";
                strSQLInDeptInfoUpdate = @"update indeptinfo set inbedenddate='1900-1-1' where inpatientid ='" + p_strPatientId + "' and inbedenddate ='" + p_strEndBedDate + "'";

            }
            else
            {
                strSQLInpatientarchivingDelete = @"delete from inpatientarchiving where inpatientid ='" + p_strPatientId + "' and opendate =to_date('" + p_strEndBedDate + "','yyyy-mm-dd hh24:mi:ss')";
                strSQLInPatientDateInfoUpDate = @"update inpatientdateinfo set inpatientenddate = to_date('1900-1-1','yyyy-mm-dd') where inpatientid ='" + p_strPatientId + "' and inpatientenddate =to_date('" + p_strEndBedDate + "','yyyy-mm-dd hh24:mi:ss')";
                strSQLInDeptInfoUpdate = @"update indeptinfo set inbedenddate=to_date('1900-1-1','yyyy-mm-dd') where inpatientid ='" + p_strPatientId + "' and inbedenddate =to_date('" + p_strEndBedDate + "','yyyy-mm-dd hh24:mi:ss')";

            }
            long lngCheckRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                lngCheckRes = objHRPServ.DoExcute(strSQLInpatientarchivingDelete);
                if (lngCheckRes < 0)
                    //return lngCheckRes;
                    lngCheckRes = objHRPServ.DoExcute(strSQLInPatientDateInfoUpDate);
                if (lngCheckRes < 0)
                    //return lngCheckRes;
                    lngCheckRes = objHRPServ.DoExcute(strSQLInDeptInfoUpdate);

                //return lngCheckRes;

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
            return lngCheckRes;


        }
        /// <summary>
        /// 为出院病人重新分配病床
        /// </summary>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strEndBedDate"></param>
        /// <param name="m_strDeptID"></param>
        /// <param name="m_strAreaID"></param>
        /// <param name="m_strBedID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngRelocate(string p_strPatientId, string p_strEndBedDate, string m_strDeptID, string m_strAreaID, string m_strBedID)
        {
            string strSQLInpatientarchivingDelete = null;
            string strSQLInPatientDateInfoUpDate = null;
            string strSQLInDeptInfoUpdate = null;

            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQLInpatientarchivingDelete = @"delete from inpatientarchiving where inpatientid ='" + p_strPatientId + "' and opendate = '" + p_strEndBedDate + "' ";
                strSQLInPatientDateInfoUpDate = @"update inpatientdateinfo set inpatientenddate = '1900-1-1'  where inpatientid ='" + p_strPatientId + "' and inpatientenddate = '" + p_strEndBedDate + "' ";
                strSQLInDeptInfoUpdate = @"update indeptinfo set inbedenddate= '1900-1-1'  indeptid = '" + m_strDeptID + "' area_id = '" + m_strAreaID + "' bed_id ='" + m_strBedID + "' room_id = (select room_id from inpatient_room_area where area_id = '" + m_strAreaID + "') where inpatientid ='" + p_strPatientId + "' and inbedenddate = '" + p_strEndBedDate + "' ";

            }
            else
            {
                strSQLInpatientarchivingDelete = @"delete from inpatientarchiving where inpatientid ='" + p_strPatientId + "' and opendate =to_date('" + p_strEndBedDate + "','yyyy-mm-dd hh24:mi:ss')";
                strSQLInPatientDateInfoUpDate = @"update inpatientdateinfo set inpatientenddate = to_date('1900-1-1','yyyy-mm-dd') where inpatientid ='" + p_strPatientId + "' and inpatientenddate =to_date('" + p_strEndBedDate + "','yyyy-mm-dd hh24:mi:ss')";
                strSQLInDeptInfoUpdate = @"update indeptinfo set inbedenddate=to_date('1900-1-1','yyyy-mm-dd') indeptid = '" + m_strDeptID + "' area_id = '" + m_strAreaID + "' bed_id ='" + m_strBedID + "' room_id = (select room_id from inpatient_room_area where area_id = '" + m_strAreaID + "') where inpatientid ='" + p_strPatientId + "' and inbedenddate =to_date('" + p_strEndBedDate + "','yyyy-mm-dd hh24:mi:ss')";

            }

            long lngCheckRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                lngCheckRes = objHRPServ.DoExcute(strSQLInpatientarchivingDelete);
                if (lngCheckRes < 0)
                    //return lngCheckRes;
                    lngCheckRes = objHRPServ.DoExcute(strSQLInPatientDateInfoUpDate);
                //			if(lngCheckRes <0)
                //				//return lngCheckRes;
                //			lngCheckRes =objHRPServ.DoExcute(strSQLInDeptInfoUpdate);

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
            return lngCheckRes;

        }
        #region 获取用户所属部门
        /// <summary>
        /// 获取医院名称
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDepartmentByUserID(string strEmpID, out DataTable p_dt)
        {
            p_dt = new DataTable();
            if (strEmpID == null || strEmpID == "") return -1;
            long lngRes = 0;
            //			clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
            //			lngRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal,"com.digitalwave.iCare.middletier.HIS","m_mthGetDepartmentByID");
            //			if(lngRes < 0)
            //			{
            //				return lngRes;
            //			}
            string strSQL = "";
            if (strEmpID.Trim() != "")
            {
                strSQL = @"select a.deptid_chr, b.modify_dat, b.deptname_vchr, b.category_int,
       b.inpatientoroutpatient_int, b.operatorid_chr, b.address_vchr,
       b.shortno_chr, b.pycode_chr, b.attributeid, b.parentid,
       b.createdate_dat, b.status_int, b.deactivate_dat, b.wbcode_chr
  from t_bse_deptemp a left join t_bse_deptdesc b
 on a.deptid_chr = b.deptid_chr where a.end_dat is null and a.empid_chr = ? ";
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strEmpID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dt, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 从表T_BSE_DEPTCONFIG获取指定类型的科室ID
        /// </summary>
        /// <param name="p_intType">类型(1－ICU科室;2－麻醉科室)</param>
        /// <param name="p_strDeptIDArr">科室ID数组</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptIDFromDetpConfig(int p_intType, out string[] p_strDeptIDArr)
        {
            p_strDeptIDArr = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                DataTable dtResult = new DataTable();

                string strSQL = @"select deptid_chr from t_bse_deptconfig where type_int= ?";
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_intType;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_strDeptIDArr = new string[dtResult.Rows.Count];
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        p_strDeptIDArr[i] = dtResult.Rows[i]["DEPTID_CHR"].ToString();
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
        /// 获取所有科室
        /// </summary>
        /// <param name="p_objDeptArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllDept(out clsDept_Desc[] p_objDeptArr)
        {
            p_objDeptArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region SQL
                string strSQL = @"select c.deptid_chr,c.deptname_vchr
								from t_bse_deptdesc c
								where c.status_int = 1
								and c.inpatientoroutpatient_int = 1
								and c.category_int = 0
								and c.attributeid = '0000003'";
                #endregion

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objDeptArr = new clsDept_Desc[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objDeptArr[i] = new clsDept_Desc();
                        p_objDeptArr[i].m_strDeptName = dtbResult.Rows[i]["DEPTNAME_VCHR"].ToString();
                        p_objDeptArr[i].m_strDeptNewID = dtbResult.Rows[i]["DEPTID_CHR"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #region 获取指定科室的扩展ID
        /// <summary>
        /// 获取指定科室的扩展ID
        /// </summary>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_strExtendID">扩展ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetExtendDeptID(string p_strDeptID, out string p_strExtendID)
        {
            p_strExtendID = null;
            if (string.IsNullOrEmpty(p_strDeptID))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region SQL
                string strSQL = @"select extendid_vchr from t_bse_deptdesc where deptid_chr = ?";
                #endregion

                DataTable dtbResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_strExtendID = dtbResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据科室名称获取科室ID
        /// <summary>
        /// 根据科室名称获取科室ID
        /// </summary>
        /// <param name="p_strDeptName">科室名称</param>
        /// <param name="p_strDeptID">科室ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptIDByDeptName(string p_strDeptName, out string p_strDeptID)
        {
            p_strDeptID = string.Empty;
            if (string.IsNullOrEmpty(p_strDeptName))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region SQL
                string strSQL = @"select t.deptid_chr from t_bse_deptdesc t where t.deptname_vchr = ?";
                #endregion

                DataTable dtbResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptName;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_strDeptID = dtbResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取员工所属科室的扩展ID
        /// <summary>
        /// 获取员工所属科室的扩展ID
        /// </summary>
        /// <param name="p_strDeptID">员工ID</param>
        /// <param name="p_strExtendID">扩展ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetExtendDeptIDArr(string p_strEmpID, out string[] p_strExtendIDArr)
        {
            p_strExtendIDArr = null;
            if (string.IsNullOrEmpty(p_strEmpID))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region SQL
                string strSQL = @"select d.extendid_vchr
								from t_bse_deptemp t, t_bse_deptdesc d
								where t.deptid_chr = d.deptid_chr
								and d.attributeid =? 
								and d.category_int=?
								and t.empid_chr =?
                                order by t.default_inpatient_dept_int,t.deptid_chr";
                #endregion

                DataTable dtbResult = new DataTable();
                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值
                objDPArr[0].Value = "0000002";
                objDPArr[1].Value = 0;
                objDPArr[2].Value = p_strEmpID.Trim();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_strExtendIDArr = new string[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_strExtendIDArr[i] = dtbResult.Rows[i][0].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    }
}