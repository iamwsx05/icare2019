using System;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity;
using System.Security.Cryptography;
using com.digitalwave.Utility.SQLConvert; 
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility;
using System.Text; 
using System.Security.Principal;

namespace com.digitalwave.PrivilegeSystemService
{
	/// <summary>
	/// ����Ա����¼���������MidtierȨ�޵��м��.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsPrivilegeHandleService2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// ���ݵ�¼����ȡԱ����
		/// </summary>		
        private const string c_strGetEmployeeIDByLoginName = @"select psw.employeeid,
       psw.opendate,
       psw.loginname,
       psw.psw,
       psw.status,
       psw.deactiveddate,
       psw.operatorid
  from employeepsw psw, employeebaseinfo ebi
 where psw.employeeid = ebi.employeeid
   and psw.loginname = ?
   and psw.status = 0
   and ebi.status = 0";																

		/// <summary>
		/// �ڼ���¼�������µ�¼Ա������ʱ�ܳ���Ϣ��
		/// </summary>
		private const string c_strInsertEmployeeCryptInfoInCheckName = @"insert into employeecryptkey
																			(employeeid, opendate, loginname, psw, encryptkey_temp, encryptiv_temp)
																		values (?,?,?,?,?,?)";
		
		/// <summary>
		/// ��ȡԱ����������Ϣ
		/// </summary>
        private const string c_strGetTempCryptInfo = @"select employeeid,
       opendate,
       loginname,
       psw,
       encryptidinfo,
       encryptkey_temp,
       encryptiv_temp
  from employeecryptkey
 where (rtrim(employeeid) = ?)
   and (opendate = ?)";
		
		/// <summary>
		/// �ڼ�����봦���µ�¼Ա�����ܳ���Ϣ��
		/// </summary>
		private const string c_strUpdateEmployeeCryptInfoInCheckPSW = @"update employeecryptkey
																set encryptidinfo =?
																where (rtrim(employeeid) = ?) and (opendate = ?)";
		
		

		/// <summary>
		/// Logout��ɾ����¼��Ϣ��
		/// </summary>
		private const string c_strLogout = @"delete from employeecryptkey
												where (rtrim(employeeid) = ?) and (opendate = ?) and (encryptidinfo = ?)";

		/// <summary>
		/// DeactiveԱ���ĵ�¼��Ϣ
		/// </summary>
		private const string c_strDeactivePSW = @"update employeepsw
													set status = 1, deactiveddate = ?, 
														operatorid = ?
													where (rtrim(employeeid) = ?) and status = 0";
		
		/// <summary>
		/// ����µ�PSW
		/// </summary>
		private const string c_strAddNewPSW = @"insert into employeepsw
													(employeeid, opendate, loginname, psw, status)
												values (?, ?, ?, ?, 0)";

		/// <summary>
		/// ���µ�ǰ��¼��Ϣ
		/// </summary>
		private const string c_strUpdateCurrentLoginInfo = @"update employeecryptkey
																set loginname =?, psw =?, encryptidinfo =?
																where (opendate = ?) and 
																	(rtrim(employeeid) = ?)";

		/// <summary>
		/// ����û����Ƿ����ڱ�����ʹ�á�
		/// </summary>
		private const string c_strCheckLoginName = @"select loginname
														from employeepsw
														where (status = 0) and (rtrim(employeeid) <> ?) and (loginname = ?)";

		/// <summary>
		/// ����û����Ƿ����ڱ�����ʹ�á�
		/// </summary>
        private const string c_strCheckEmployee = @"select employeeid,
       opendate,
       loginname,
       psw,
       status,
       deactiveddate,
       operatorid
  from employeepsw
 where rtrim(employeeid) = ?
   and psw = ?
   and status = 0";
	    /// <summary>
	    /// ����Ա��ID��ȡ����
	    /// </summary>
		private const string c_strGetPSWbyID = @"select psw from employeepsw
														where rtrim(employeeid) = ?
														and status = 0";

		/// <summary>
		/// ����¼��
		/// </summary>
		/// <param name="p_strLoginName">��¼��</param>
		/// <param name="p_strPublicKey">�����㷨�Ĺ���</param>
		/// <param name="p_bytKey">�����㷨��Key</param>
		/// <param name="p_bytIV">�����㷨��IV</param>
		/// <param name="p_strEmployeeID">��¼����Ӧ��Ա����</param>
		/// <returns>�������</returns>
		[AutoComplete]
		public long m_lngCheckLoginName(string p_strLoginName,string p_strPublicKey,out byte [] p_bytKeyArr,out byte [] p_bytIVArr,out string p_strEmployeeID,out DateTime p_dtmOpenDate)
		{
            string strGetEmployeeIDByLoginName = @"select psw.employeeid,
       psw.opendate,
       psw.loginname,
       psw.psw,
       psw.status,
       psw.deactiveddate,
       psw.operatorid
  from employeepsw psw, employeebaseinfo ebi
 where psw.employeeid = ebi.employeeid
   and psw.loginname = ?
   and psw.status = 0
   and ebi.status = 0";
			p_bytKeyArr = null;
			p_bytIVArr = null;
			p_strEmployeeID = null;

			p_dtmOpenDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

			//������                              
			if(p_strLoginName==null)
				return (long)enmOperationResult.Parameter_Error;
			
			clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr1);
                objDPArr1[0].Value = p_strLoginName;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetEmployeeIDByLoginName, ref dtbValue, objDPArr1);

                if (lngRes > 0)
                {
                    //�������
                    if (dtbValue.Rows.Count == 0)
                        return (long)enmOperationResult.Not_permission;

                    clsCryptoTool objCT = new clsCryptoTool();

                    //����������������µ�¼��Ϣ
                    byte[] bytKeyInfo = objCT.m_bytGetSAKey();
                    byte[] bytIVInfo = objCT.m_bytGetSAIV();

                    //��ȡIDataParameter����
                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = dtbValue.Rows[0]["EMPLOYEEID"];
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_dtmOpenDate;
                    objDPArr[2].Value = p_strLoginName;
                    objDPArr[3].Value = dtbValue.Rows[0]["PSW"];
                    objDPArr[4].Value = bytKeyInfo;
                    objDPArr[5].Value = bytIVInfo;

                    long lngAff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strInsertEmployeeCryptInfoInCheckName, ref lngAff, objDPArr);

                    if (lngRes <= 0)
                        return lngRes;

                    //��������������÷���
                    //				objCT.m_mthEncryptSAInfo(p_strPublicKey,bytKeyInfo,bytIVInfo,out p_bytKeyArr,out p_bytIVArr);
                    p_bytKeyArr = bytKeyInfo;
                    p_bytIVArr = bytIVInfo;

                    objCT.m_mthClear();

                    p_strEmployeeID = (string)dtbValue.Rows[0]["EMPLOYEEID"];
                }
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;			
		}

		/// <summary>
		/// ����¼����
		/// </summary>
		/// <param name="p_strEmployeeID">Ա��ID��</param>
		/// <param name="p_dtmOpenDate">��¼��������ʱ��</param>
		/// <param name="p_bytIDDataArr">��¼�߼��ܺ����Ϣ</param>
		/// <param name="p_objRoleArr">���ص�¼�����ڵ�Role</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckLoginPSW(string p_strEmployeeID,DateTime p_dtmOpenDate,byte [] p_bytIDDataArr,out clsRole[] p_objRoleArr)
		{
			p_objRoleArr = null;

			//������                              
			if(p_strEmployeeID==null || p_bytIDDataArr == null)
				return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;

            try
            {
                string strGetTempCryptInfo = null;
                //			if (clsHRPTableService.bytDatabase_Selector==0)
                strGetTempCryptInfo = @"select employeeid,
       opendate,
       loginname,
       psw,
       encryptidinfo,
       encryptkey_temp,
       encryptiv_temp
  from employeecryptkey
 where (employeeid = ?)
   and opendate = ?" ;
                //			else
                //				strGetTempCryptInfo = "SELECT * FROM EmployeeCryptKey WHERE (EmployeeID = '"+p_strEmployeeID+"') and (OpenDate = TO_DATE ('"+p_dtmOpenDate.ToString()+"', 'yyyy-mm-dd hh24:mi:ss'))";													

                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr1);
                objDPArr1[0].Value = p_strEmployeeID;
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = Convert.ToDateTime(p_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));

                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                //			long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTempCryptInfo,ref dtbValue,objIDDP,objOpenDateDP);
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetTempCryptInfo, ref dtbValue, objDPArr1);

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    if (dtbValue.Rows[0]["ENCRYPTKEY_TEMP"] == DBNull.Value || dtbValue.Rows[0]["ENCRYPTIV_TEMP"] == DBNull.Value)
                        return (long)enmOperationResult.Not_permission;

                    string strLoginName = dtbValue.Rows[0]["LOGINNAME"].ToString();
                    string strPSW = dtbValue.Rows[0]["PSW"].ToString();

                    byte[] bytKey = (byte[])dtbValue.Rows[0]["ENCRYPTKEY_TEMP"];
                    byte[] bytIV = (byte[])dtbValue.Rows[0]["ENCRYPTIV_TEMP"];

                    //��ȡ�ͻ��˴��������
                    clsCryptoTool objCT = new clsCryptoTool();
                    objCT.m_mthSetSAInfo(bytKey, bytIV);
                    byte[] bytData = objCT.m_bytDecrypt(p_bytIDDataArr);
                    objCT.m_mthClear();
                    string strIDInfo = Encoding.Unicode.GetString(bytData);

                    //��������
                    if (strIDInfo != strLoginName + strPSW)
                        return (long)enmOperationResult.Not_permission;

                    //���µ�¼��Ϣ��
                    //��ȡIDataParameter����
                    IDataParameter[] objDPArr;
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_bytIDDataArr;
                    objDPArr[1].Value = p_strEmployeeID;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_dtmOpenDate;

                    long lngAff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateEmployeeCryptInfoInCheckPSW, ref lngAff, objDPArr);

                    if (lngRes <= 0)
                        return lngRes;

                    //��ȡԱ����Role
                    clsPrivilegeSystemService2 objServ = new clsPrivilegeSystemService2();
                    lngRes = objServ.m_lngGetRolesByEmployee(p_strEmployeeID, out p_objRoleArr);
                }
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
		}

		/// <summary>
		/// ������MidTier��Ȩ��
		/// </summary>
		/// <param name="p_objPrincipal">Ȩ����Ϣ</param>
		/// <param name="p_strClassName">MidTier������</param>
		/// <param name="p_strMethodName">MidTier�ķ�����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckCallPrivilege( string p_strClassName,string p_strMethodName)
		{	
			return 1;

//			if(p_objPrincipal == null)
//				return (long)enmOperationResult.Parameter_Error;
///// <summary>
//		/// ����û�Ȩ��
//		/// </summary>
//		string c_strCheckUserCallPrivilege = clsDatabaseSQLConvert.s_StrTop1+@" re.employeeid
//																from role_employee re inner join
//																employeecryptkey eck
//																on re.employeeid = eck.employeeid inner join
//																midtierprivilegeinfo mpi
//																on mpi.role_id = re.role_id
//																where rtrim(re.employeeid) = ?
//																and eck.opendate = ?
//																and eck.encryptidinfo = ?
//																and re.status = '0'
//																and mpi.classname = ?
//																and mpi.methodname = ?"+clsDatabaseSQLConvert.s_StrRownum;
//			clsHRPTableService objHRPServ = new clsHRPTableService();
//            try
//            {
//                if (p_objPrincipal.Identity is clsEncryptIdentity)
//                {
//                    clsEncryptIdentity objID = (clsEncryptIdentity)p_objPrincipal.Identity;



//                    IDataParameter[] objDPArr;
//                    objHRPServ.CreateDatabaseParameter(5, out objDPArr);
//                    objDPArr[0].Value = objID.m_StrEmployeeID.Trim();
//                    objDPArr[1].DbType = DbType.DateTime;
//                    objDPArr[1].Value = objID.m_DtmOpenDate;
//                    objDPArr[2].Value = objID.m_BytEncryptInfo;
//                    objDPArr[3].Value = p_strClassName;
//                    objDPArr[4].Value = p_strMethodName;

//                    DataTable dtbValue = new DataTable();
//                    long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckUserCallPrivilege, ref dtbValue, objDPArr);
//                    //				long lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSql,ref dtbValue);
//                    //���û�м�¼�������û����ڵĽ�ɫû�е��øú�����Ȩ��
//                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
//                        return (long)enmOperationResult.DB_Succeed;
//                    else if (lngRes <= 0)
//                        return (long)enmOperationResult.DB_Fail;
//                    else
//                        return (long)enmOperationResult.Not_permission;
//                }
//                else
//                    return (long)enmOperationResult.Not_permission;
//            }
//            catch
//            {
//            }
//            finally
//            {
//                //objHRPServ.Dispose();
//            }
		}

		/// <summary>
		/// �û��˳�ע����Ϣ
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngLogout( )
		{
            return 1;
           
		}			

		/// <summary>
		/// �����û���¼��Ϣ
		/// </summary>
		/// <param name="p_objPrincipal">�û���¼Ȩ����Ϣ</param>
		/// <param name="p_strLoginName">�µ��û���</param>
		/// <param name="p_bytEncryptPSW">�µļ��ܺ���û�����</param>
		/// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateLoginInfo( string p_strLoginName, byte[] p_bytEncryptPSW)
        {
            return 1;
        }

		/// <summary>
		/// ����û�ǩ��
		/// </summary>
		/// <param name="p_objPrincipal">�û���¼Ȩ����Ϣ</param>
		/// <param name="p_strEmployeeID">Ա����</param>
		/// <param name="p_bytEncryptPSW">���ܺ��Ա������</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckEmployeeSign( string p_strEmployeeID,byte[] p_bytEncryptPSW,out bool p_blnIsPass)
		{
			p_blnIsPass = true;
            return 1;
		}

		/// <summary>
		/// ��ȡԱ������
		/// </summary>
		/// <param name="p_objPrincipal">�û���¼Ȩ����Ϣ</param>
		/// <param name="p_strEmployeeID">Ա����</param>
		/// <param name="p_strPSW">����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPSWbyID( string p_strEmployeeID,out string p_strPSW)
		{
			p_strPSW = null;
            return 1;
			//if(p_objPrincipal == null || p_strEmployeeID == null || p_strEmployeeID == "")
			//	return (long)enmOperationResult.Parameter_Error;

			//if(p_objPrincipal.Identity is clsEncryptIdentity)
			//{
			//	clsEncryptIdentity objID = (clsEncryptIdentity)p_objPrincipal.Identity;
			//	DataTable dtbValue = new DataTable();

			//	clsHRPTableService objHRPServ = new clsHRPTableService();
			//	//��ȡIDataParameter����
			//	IDataParameter[] objDPArr;
			//	objHRPServ.CreateDatabaseParameter(1, out objDPArr);
			//	objDPArr[0].Value=p_strEmployeeID;	

			//	long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetPSWbyID,ref dtbValue,objDPArr);
				
   //             //objHRPServ.Dispose();
			//	if(lngRes > 0)
			//	{
			//		p_strPSW = dtbValue.Rows[0]["PSW"].ToString();
			//		return lngRes;
			//	}
			//}
			//return (long)enmOperationResult.Not_permission;
		}
	}
}
