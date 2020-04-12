using System;
using System.EnterpriseServices;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;

namespace com.digitalwave.EMR_EmployeeManagerService
{
	/// <summary>
	/// 鍛樺伐绠＄悊鐨勪腑闂翠欢銆?
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsEMR_EmployeeManagerService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		///  鑾峰彇鍛樺伐鐨勫熀鏈俊鎭?
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_strResultXml"></param>
		/// <param name="p_intResultRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetEmployeeBaseInfo( string p_strEmployeeID,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 
                string strSQL = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    //
                    strSQL = @"select top 1 ebi.empid_chr,
       ebi.begindate_dat,
       ebi.firstname_vchr,
       ebi.lastname_vchr,
       ebi.empidcard_chr,
       ebi.pycode_chr,
       ebi.sex_chr,
       ebi.educationallevel_chr,
       ebi.maritalstatus_chr,
       ebi.technicalrank_chr,
       ebi.languageability_vchr,
       ebi.birthdate_dat,
       ebi.officephone_vchr,
       ebi.homephone_vchr,
       ebi.mobile_vchr,
       ebi.officeaddress_vchr,
       ebi.officezip_chr,
       ebi.homeaddress_vchr,
       ebi.homezip_chr,
       ebi.email_vchr,
       ebi.contactname_vchr,
       ebi.contactphone_vchr,
       ebi.remark_vchr,
       ebi.status_int,
       ebi.deactivate_dat,
       ebi.shortname_chr,
       ebi.operatorid_chr,
       ebi.hasprescriptionright_chr,
       ebi.haspsychosisprescriptionright_,
       ebi.hasopiateprescriptionright_chr,
       ebi.isexpert_chr,
       ebi.expertfee_mny,
       ebi.isexternalexpert_chr,
       ebi.ancestoraddr_vchar,
       ebi.experience_vchr,
       ebi.psw_chr,
       ebi.deptcode_chr,
       ebi.technicallevel_chr,
       ebi.digitalsign_dta,
       ebi.extendid_vchr,
       ebi.isemployee_int,
       ebi.empno_chr,
       de.deptid_chr as deptid,
       null as area_id,
       null as area_name
  from t_bse_employee ebi
 inner join t_bse_deptemp de on ebi.empid_chr = de.empid_chr
 where ebi.empno_chr = ?";

                else if (clsHRPTableService.bytDatabase_Selector == 2) //oracle
                    strSQL = @"select empid_chr,
       begindate_dat,
       firstname_vchr,
       lastname_vchr,
       empidcard_chr,
       pycode_chr,
       sex_chr,
       educationallevel_chr,
       maritalstatus_chr,
       technicalrank_chr,
       languageability_vchr,
       birthdate_dat,
       officephone_vchr,
       homephone_vchr,
       mobile_vchr,
       officeaddress_vchr,
       officezip_chr,
       homeaddress_vchr,
       homezip_chr,
       email_vchr,
       contactname_vchr,
       contactphone_vchr,
       remark_vchr,
       status_int,
       deactivate_dat,
       shortname_chr,
       operatorid_chr,
       hasprescriptionright_chr,
       haspsychosisprescriptionright_,
       hasopiateprescriptionright_chr,
       isexpert_chr,
       expertfee_mny,
       isexternalexpert_chr,
       ancestoraddr_vchar,
       experience_vchr,
       psw_chr,
       deptcode_chr,
       technicallevel_chr,
       digitalsign_dta,
       extendid_vchr,
       isemployee_int,
       empno_chr,
       deptid,
       begin_dat,
       end_dat,
       area_id,
       area_name
  from (select ebi.empid_chr,
               ebi.begindate_dat,
               ebi.firstname_vchr,
               ebi.lastname_vchr,
               ebi.empidcard_chr,
               ebi.pycode_chr,
               ebi.sex_chr,
               ebi.educationallevel_chr,
               ebi.maritalstatus_chr,
               ebi.technicalrank_chr,
               ebi.languageability_vchr,
               ebi.birthdate_dat,
               ebi.officephone_vchr,
               ebi.homephone_vchr,
               ebi.mobile_vchr,
               ebi.officeaddress_vchr,
               ebi.officezip_chr,
               ebi.homeaddress_vchr,
               ebi.homezip_chr,
               ebi.email_vchr,
               ebi.contactname_vchr,
               ebi.contactphone_vchr,
               ebi.remark_vchr,
               ebi.status_int,
               ebi.deactivate_dat,
               ebi.shortname_chr,
               ebi.operatorid_chr,
               ebi.hasprescriptionright_chr,
               ebi.haspsychosisprescriptionright_,
               ebi.hasopiateprescriptionright_chr,
               ebi.isexpert_chr,
               ebi.expertfee_mny,
               ebi.isexternalexpert_chr,
               ebi.ancestoraddr_vchar,
               ebi.experience_vchr,
               ebi.psw_chr,
               ebi.deptcode_chr,
               ebi.technicallevel_chr,
               ebi.digitalsign_dta,
               ebi.extendid_vchr,
               ebi.isemployee_int,
               ebi.empno_chr,
               de.deptid_chr as deptid,
               de.begin_dat,
               de.end_dat,
               null as area_id,
               null as area_name
          from t_bse_employee ebi
         inner join t_bse_deptemp de on ebi.empid_chr = de.empid_chr
         where ebi.empno_chr = ?) t1
 where rownum = 1";
                else if (clsHRPTableService.bytDatabase_Selector == 4) //db2
                    strSQL = @"select ebi.empid_chr,
       ebi.begindate_dat,
       ebi.firstname_vchr,
       ebi.lastname_vchr,
       ebi.empidcard_chr,
       ebi.pycode_chr,
       ebi.sex_chr,
       ebi.educationallevel_chr,
       ebi.maritalstatus_chr,
       ebi.technicalrank_chr,
       ebi.languageability_vchr,
       ebi.birthdate_dat,
       ebi.officephone_vchr,
       ebi.homephone_vchr,
       ebi.mobile_vchr,
       ebi.officeaddress_vchr,
       ebi.officezip_chr,
       ebi.homeaddress_vchr,
       ebi.homezip_chr,
       ebi.email_vchr,
       ebi.contactname_vchr,
       ebi.contactphone_vchr,
       ebi.remark_vchr,
       ebi.status_int,
       ebi.deactivate_dat,
       ebi.shortname_chr,
       ebi.operatorid_chr,
       ebi.hasprescriptionright_chr,
       ebi.haspsychosisprescriptionright_,
       ebi.hasopiateprescriptionright_chr,
       ebi.isexpert_chr,
       ebi.expertfee_mny,
       ebi.isexternalexpert_chr,
       ebi.ancestoraddr_vchar,
       ebi.experience_vchr,
       ebi.psw_chr,
       ebi.deptcode_chr,
       ebi.technicallevel_chr,
       ebi.digitalsign_dta,
       ebi.extendid_vchr,
       ebi.isemployee_int,
       ebi.empno_chr,
       de.deptid_chr as deptid,
       de.begin_dat,
       de.end_dat,
       '' as area_id,
       '' as area_name
  from t_bse_employee ebi
 inner join t_bse_deptemp de on ebi.empid_chr = de.empid_chr
 where ebi.empno_chr = ? fetch first 1 row only";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID.Trim();

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			//杩斿洖
			return lngRes;
		}

		/// <summary>
		/// 鑾峰彇褰撳墠鐧婚檰鐨勫憳宸?
		/// </summary>
		/// <param name="p_strDomainName">鐧婚檰鐨勫煙鍚嶇О</param>
		/// <param name="p_strDomainUserName">鎵�鍦ㄥ煙鐨勭敤鎴峰悕</param>
		/// <param name="p_strResultXml">杩斿洖鐨勭粨鏋?/param>
		/// <param name="p_intResultRows">璁板綍鐨勬暟閲?/param>
		/// <returns>
		/// 鎿嶄綔缁撴灉
		/// 0锛氬け璐?
		/// 1锛氭垚鍔熴�?
		/// </returns>
		[AutoComplete]
		public long m_lngGetCurrentLoginEmployee( string p_strDomainName,string p_strDomainUserName,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 
                string strSQL = @"select ebi.employeeid,ebi.firstname,de.deptid as deptid
										from domainuserinfo dui,employeebaseinfo ebi,dept_employee de
										where dui.employeeid = ebi.employeeid
										and ebi.employeeid = de.employeeid
										and domainname = '" + p_strDomainName + @"'
										and domainusername = '" + p_strDomainUserName + @"'
										and end_date_username = " + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @"
										order by de.modifydate desc";

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			//杩斿洖
			return lngRes;
		}

		/// <summary>
		/// 妯＄硦鏌ヨ鑾峰緱鍖荤敓ID鍜屽悕绉帮紝鍙煡璇㈡湁閮ㄩ棬鐨勫尰鐢?
		/// </summary>
		/// <param name="p_strDoctorIDLike">瑕佹煡璇㈢殑鍖荤敓ID鎴栧悕绉板叧閿瓧</param>
		/// <param name="p_strResultXml"></param>
		/// <param name="p_intResultRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetEmployeeIDLikeArr( string p_strDoctorIDLike,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 
                string strSQL = "";//鐢变簬鍛樺伐鍙峰彲浠ュ惈鏈夊瓧姣嶏紝鏁呬笉鑳界敱鏄惁鏁存暟鏉ョ洿鎺ュ垽鏂憳宸ュ彿涓庡悕绉般�?
                //			try
                //			{
                //				long.Parse(p_strDoctorIDLike);
                #region old
                //				strSQL = @"select EmployeeID,FirstName
                //								from EmployeeBaseInfo as EBI
                //								where EBI.Status = 0
                //								and (EmployeeID like '"+p_strDoctorIDLike+@"%'
                //								   or FirstName like '"+p_strDoctorIDLike+@"%')";
                #endregion old
                //钄℃矏蹇?
                //					strSQL = @"select distinct DE.EmployeeID,EBI.FirstName 
                //								from Dept_Employee DE,EmployeeBaseInfo EBI
                //								where DE.EmployeeID=EBI.EmployeeID
                //								and (DE.EmployeeID like '"+p_strDoctorIDLike+@"%'
                //								or FirstName like '"+p_strDoctorIDLike+@"%')
                //								and EBI.Status=0";
                strSQL = @"select distinct de.empid_chr as employeeid,ebi.lastname_vchr as firstname
								from t_bse_deptemp de,t_bse_employee ebi
								where de.empid_chr=ebi.empid_chr
								and (de.empid_chr like '" + p_strDoctorIDLike + @"%'
								or ebi.lastname_vchr like '" + p_strDoctorIDLike + @"%')
								and ebi.status_int=1";
                //			}
                //			catch
                //			{
                //				strSQL = @"select EmployeeID,FirstName
                //								from EmployeeBaseInfo as EBI
                //								where EBI.Status = 0
                //								and FirstName like '"+p_strDoctorIDLike+@"%'";
                //
                //			}

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			//杩斿洖
			return lngRes;
		}

		/// <summary>
		/// 妯＄硦鏌ヨ鍏ㄩ儴鍖荤敓ID鍜屽悕绉?
		/// </summary>
		/// <param name="p_strDoctorIDLike">瑕佹煡璇㈢殑鍖荤敓ID鎴栧悕绉板叧閿瓧</param>
		/// <param name="p_strResultXml"></param>
		/// <param name="p_intResultRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllEmployeeIDLikeArr( string p_strDoctorIDLike,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 
                string strSQL = "";//鐢变簬鍛樺伐鍙峰彲浠ュ惈鏈夊瓧姣嶏紝鏁呬笉鑳界敱鏄惁鏁存暟鏉ョ洿鎺ュ垽鏂憳宸ュ彿涓庡悕绉般�?
                try
                {
                    long.Parse(p_strDoctorIDLike);

                    strSQL = @"select employeeid,firstname
									from employeebaseinfo ebi
									where ebi.status = 0
									and (employeeid like '" + p_strDoctorIDLike + @"%'
										or firstname like '" + p_strDoctorIDLike + @"%')";
                }
                catch
                {
                    strSQL = @"select employeeid,firstname
									from employeebaseinfo ebi
									where ebi.status = 0
									and firstname like '" + p_strDoctorIDLike + @"%'";

                }

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			//杩斿洖
			return lngRes;
		}

		/// <summary>
		/// 鏍规嵁閮ㄩ棬ID鑾峰彇璇ラ儴闂ㄤ笅鐨勬墍鏈夊憳宸ワ紝钄℃矏蹇?2003-7-15
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_strResultXml"></param>
		/// <param name="p_intResultRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetEmployeeByDeptID( string p_strDeptID,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 
                string strSQL = "";

                //				strSQL = @"select distinct DE.EmployeeID,EBI.FirstName 
                //								from Dept_Employee DE,EmployeeBaseInfo EBI
                //								where DE.EmployeeID=EBI.EmployeeID
                //								and DeptID='" + p_strDeptID + "' and EBI.Status=0 and DE.EndDate="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat();							

                strSQL = @"select distinct de.empid_chr as employeeid,
                ebi.lastname_vchr as firstname,
                ebi.pycode_chr,
                ebi.lastname_vchr,
                ebi.empid_chr,
                ebi.empno_chr,
                ebi.sex_chr
  from t_bse_deptemp de, t_bse_employee ebi
 where de.empid_chr = ebi.empid_chr
   and de.deptid_chr = ?
   and ebi.status_int = 1
   and (de.end_dat = ?
    or de.end_dat is null)";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strDeptID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900, 1, 1);

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			//杩斿洖
			return lngRes;
		}

        /// <summary>
        /// 获取指定科室所有员工
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_dtbEmp"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeByDeptID(  string p_strDeptID, out DataTable p_dtbEmp)
        {
            long lngRes = 0;
            p_dtbEmp = null;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 
                string strSQL = @"select distinct de.empid_chr as employeeid,
                ebi.lastname_vchr as firstname,
                ebi.pycode_chr,
                ebi.lastname_vchr,
                ebi.empid_chr,
                ebi.empno_chr,
                ebi.sex_chr
  from t_bse_deptemp de, t_bse_employee ebi
 where de.empid_chr = ebi.empid_chr
   and de.deptid_chr = ?
   and ebi.status_int = 1
   and (de.end_dat = ?
    or de.end_dat is null)";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strDeptID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900, 1, 1);

                lngRes = objTabService.lngGetDataTableWithParameters(strSQL, ref p_dtbEmp, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
            //杩斿洖
            return lngRes;
        }

		/// <summary>
		/// 鏍规嵁閮ㄩ棬ID鑾峰彇璇ラ儴闂ㄧ殑涓讳换鍖诲笀
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_dtRecord"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetChargeDocByDeptID( string p_strDeptID, out DataTable p_dtRecord)
		{
			long lngRes = -1;
            clsHRPTableService objTabService = new clsHRPTableService();
			p_dtRecord = new DataTable();
            try
            { 
                string SQL = @"select employeeid,firstname from employeebaseinfo 
						where employeeid in (select employeeid from role_employee where role_id in 
						(select role_id from role_info where role_name='涓讳换鍖诲笀' and
						category =(select deptname from dept_desc where deptid='" + p_strDeptID + "')) )";

                lngRes = objTabService.DoGetDataTable(SQL, ref p_dtRecord);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }

			return lngRes;
		}
		[AutoComplete]
		public long m_lngGetEmp(string p_strEmpID ,out DataTable p_dt)
		{
			p_dt = new DataTable();
			if(p_strEmpID == null) return -1;
            string strSql = @"select empid_chr,
       begindate_dat,
       firstname_vchr,
       lastname_vchr,
       empidcard_chr,
       pycode_chr,
       sex_chr,
       educationallevel_chr,
       maritalstatus_chr,
       technicalrank_chr,
       languageability_vchr,
       birthdate_dat,
       officephone_vchr,
       homephone_vchr,
       mobile_vchr,
       officeaddress_vchr,
       officezip_chr,
       homeaddress_vchr,
       homezip_chr,
       email_vchr,
       contactname_vchr,
       contactphone_vchr,
       remark_vchr,
       status_int,
       deactivate_dat,
       shortname_chr,
       operatorid_chr,
       hasprescriptionright_chr,
       haspsychosisprescriptionright_,
       hasopiateprescriptionright_chr,
       isexpert_chr,
       expertfee_mny,
       isexternalexpert_chr,
       ancestoraddr_vchar,
       experience_vchr,
       psw_chr,
       deptcode_chr,
       technicallevel_chr,
       digitalsign_dta,
       extendid_vchr,
       isemployee_int,
       empno_chr
  from t_bse_employee t
 where t.empid_chr = ?
    or t.empno_chr = ?";
            clsHRPTableService objTabService = new clsHRPTableService();

            IDataParameter[] objDPArr = null;
            objTabService.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strEmpID;
            objDPArr[1].Value = p_strEmpID;
            return objTabService.lngGetDataTableWithParameters(strSql, ref p_dt, objDPArr);
         
                
        }


        #region 根据输入的字符/字符串模糊查询员工ID及姓名
        /// <summary>
        /// 根据输入的字符/字符串模糊查询员工ID及姓名
		/// </summary>
        /// <param name="p_strSome">输入的字符/字符串</param>
        /// <param name="p_dtbValue">员工信息</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetEmpArrByIDOrNameLike(string p_strSome, out DataTable p_dtbValue)
		{
			long lngRes = 0;
			p_dtbValue = null;
			if(p_strSome == null)
				return -1;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                DataTable m_dtbResult = new DataTable();
                string strSQL = @"select empid_chr, empno_chr, lastname_vchr
  from t_bse_employee
 where (empno_chr like ? or lastname_vchr like ? or pycode_chr like ?)
   and status_int = 1";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strSome.Trim() + "%";
                objDPArr[1].Value = p_strSome.Trim() + "%";
                objDPArr[2].Value = p_strSome.Trim() + "%";

                lngRes = objTabService.lngGetDataTableWithParameters(strSQL, ref m_dtbResult, objDPArr);

                if (lngRes > 0 && m_dtbResult.Rows.Count > 0)
                {
                    p_dtbValue = m_dtbResult;
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
                //objTabService.Dispose();
            }
			//杩斿洖
			return lngRes;
		}

        /// <summary>
        /// 根据输入的字符/字符串模糊查询指定科室员工ID及姓名
        /// </summary>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_strSome">输入的字符/字符串</param>
        /// <param name="p_dtbValue">员工信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmpArrByIDOrNameLikeInDept(string p_strDeptID, string p_strSome, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            if (p_strSome == null || p_strDeptID == null)
                return -1;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                DataTable m_dtbResult = new DataTable();
                string strSQL = @"select a.empid_chr, a.empno_chr, a.lastname_vchr
  from t_bse_employee a, t_bse_deptemp b
 where (a.empno_chr like ? or a.lastname_vchr like ? or a.pycode_chr like ?)
   and a.status_int = 1
   and a.empid_chr = b.empid_chr
   and b.deptid_chr = ?
   and (b.end_dat = ? or b.end_dat is null)";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_strSome.Trim() + "%";
                objDPArr[1].Value = p_strSome.Trim() + "%";
                objDPArr[2].Value = p_strSome.Trim() + "%";
                objDPArr[3].Value = p_strDeptID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = new DateTime(1900,1,1);

                lngRes = objTabService.lngGetDataTableWithParameters(strSQL, ref m_dtbResult, objDPArr);

                if (lngRes > 0 && m_dtbResult.Rows.Count > 0)
                {
                    p_dtbValue = m_dtbResult;
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
                //objTabService.Dispose();
            }
            //杩斿洖
            return lngRes;
        }
		#endregion

        #region 获取所有员工ID及姓名
        /// <summary>
        /// 获取所有员工ID及姓名 
        /// </summary>
        /// <param name="p_dtbValue">员工信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllEmp(out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                DataTable m_dtbResult = new DataTable();
                string strSQL = @"select pycode_chr, lastname_vchr, empid_chr, empno_chr, sex_chr
  from t_bse_employee
 where status_int = 1
 order by empid_chr";

                lngRes = objTabService.DoGetDataTable(strSQL, ref m_dtbResult);

                if (lngRes > 0 && m_dtbResult.Rows.Count > 0)
                {
                    p_dtbValue = m_dtbResult;
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
                //objTabService.Dispose();
            }
            //返回
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 获取员工处方权
        /// </summary>
        /// <param name="p_strEMPID">员工ID</param>
        /// <param name="p_intRight">处方权0,无处方权;1,有处方权</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrescriptionRight(string p_strEMPID,out int p_intRight)
        {
            long lngRes = 0;
            p_intRight = 0;
            if (p_strEMPID == null)
            {
                return -1;
            }
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                DataTable m_dtbResult = new DataTable();
                string strSQL = @"select t.hasprescriptionright_chr
  from t_bse_employee t
 where t.empid_chr = ?";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEMPID;

                lngRes = objTabService.lngGetDataTableWithParameters(strSQL, ref m_dtbResult, objDPArr);

                if (lngRes > 0 && m_dtbResult.Rows.Count > 0)
                {
                    p_intRight = Convert.ToInt32(m_dtbResult.Rows[0][0]);
                }
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
	}
}