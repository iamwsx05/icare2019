using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService ;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;



namespace com.digitalwave.iCare.Public.MenuExtend.Service
{
	/// <summary>
	/// ��������м��
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsSpecialSymbolServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		private clsHRPTableService m_objHRPServ =new clsHRPTableService();

		#region SQL����	

		// ��CommonUseValue�л�ȡָ���ı���
		private const string c_strCheckNameSQL=@"select SpecialSymbolValue from SpecialSymbol where rtrim(DeptID) = ? and SpecialSymbolValue=? ";
		
		// ����CommonUseValue��ɾ����¼����Ϣ
		private const string c_strDeleteRecordSQL="DELETE FROM SpecialSymbol where rtrim(DeptID) = ? and SpecialSymbolValue=?";		
		
		
		#endregion SQL����
		
		public clsSpecialSymbolServ()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// ��ȡ����ʹ�õ��������
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllSpecialSymbolValue(string p_strDeptID,out DataTable p_dtbResult)
		{			
			p_dtbResult = null;

//			long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSpecialSymbolServ","m_lngGetAllSpecialSymbolValue");
//			if(lngCheckRes <= 0)
//				return lngCheckRes;

			string strSQL = "select SpecialSymbolValue from SpecialSymbol where rtrim(DeptID) = '"+p_strDeptID+"' ORDER BY ValueIndex";
			return m_objHRPServ.DoGetDataTable(strSQL ,ref p_dtbResult);
		}

		[AutoComplete]
		private long m_lngCheckSame(string p_strCheckDeptID,string p_strCheckName,string p_strCheckSQL)
		{
			//������
			if(p_strCheckDeptID == null || p_strCheckName == null)
				return (long)enmOperationResult.Parameter_Error;
		
			//��ȡIDataParameter����
			IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];
			//��˳���IDataParameter��ֵ
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
			m_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
			objDPArr[0].Value=p_strCheckDeptID;
			objDPArr[1].Value=p_strCheckName;
			
			//����DataTable
			DataTable dtbValue = new DataTable();
			//ִ�в�ѯ���������DataTable
			long lngRes = m_objHRPServ.lngGetDataTableWithParameters(p_strCheckSQL,ref dtbValue,objDPArr);
					
			//�鿴DataTable.Rows.Count
			//�������1����ʾ�Ѿ�����ͬ��p_strCheckString������ֵʹ��Record_Already_Exist
			if(lngRes > 0 && dtbValue.Rows.Count ==1)
			{
				return (long)enmOperationResult.Record_Already_Exist;//����
			}
			//����	
			return lngRes;
		}		
		
		[AutoComplete]
		private long m_lngCheckName(string p_strDeptID,string p_strSpecialSymbol)
		{			
			return m_lngCheckSame(p_strDeptID,p_strSpecialSymbol,c_strCheckNameSQL);
		}

		/// <summary>
		/// �����¼�����ݿ⡣
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAddNewRecord2DB(clsSpecialSymbolValue p_objRecordContent)
		{
			//������                              
			if(p_objRecordContent==null || p_objRecordContent.m_strDeptID == null || p_objRecordContent.m_strSpecialSymbolValue==null)
				return (long)enmOperationResult.Parameter_Error;			
		
			long lngRes=m_lngCheckName(p_objRecordContent.m_strDeptID,p_objRecordContent.m_strSpecialSymbolValue);
			if(lngRes<=0)return lngRes;
		
			// ��Ӽ�¼��SpecialSymbolValue
//			string strAddNewRecordSQL= 	@"begin
//declare intIndex number;
//begin
//select Max(ValueIndex) into intIndex from SpecialSymbol where DeptID = '"+p_objRecordContent.m_strDeptID+@"';
//if intIndex is null then
//intIndex := 0;
//end if;
//intIndex := intIndex+1;
//insert into SpecialSymbol(DeptID,ValueIndex,SpecialSymbolValue) values('"+p_objRecordContent.m_strDeptID+@"',intIndex,'"+p_objRecordContent.m_strSpecialSymbolValue+@"');
//end;
//end;";
			string strSql = "select "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(Max(ValueIndex),0) + 1 from SpecialSymbol where rtrim(DeptID) = '"+p_objRecordContent.m_strDeptID+"'";
			DataTable dtResult = new DataTable();
			lngRes = m_objHRPServ.DoGetDataTable(strSql,ref dtResult);
			if(lngRes <= 0 || dtResult.Rows.Count == 0)
				return lngRes;
			string strIndex = dtResult.Rows[0][0].ToString();
			strSql = "insert into SpecialSymbol(DeptID,ValueIndex,SpecialSymbolValue) values('"+p_objRecordContent.m_strDeptID+"','"+strIndex+"','"+p_objRecordContent.m_strSpecialSymbolValue+"')";
//			strSql = "execute P_INSERTSPECIALSYMBOL('"+p_objRecordContent.m_strDeptID+"','"+p_objRecordContent.m_strSpecialSymbolValue+"');";

			//ִ��SQL			
			lngRes =  m_objHRPServ.DoExcute(strSql);
			return lngRes;
			
		}		
		
		/// <summary>
		/// �޸ļ�¼
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objOldRecordContent"></param>
		/// <param name="p_objNewRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngModifyRecord2DB(clsSpecialSymbolValue p_objOldRecordContent,clsSpecialSymbolValue p_objNewRecordContent)
		{
			//������
			if(p_objOldRecordContent==null || p_objOldRecordContent.m_strSpecialSymbolValue==null ||
				p_objNewRecordContent==null || p_objNewRecordContent.m_strDeptID==null || p_objNewRecordContent.m_strSpecialSymbolValue==null)
				return (long)enmOperationResult.Parameter_Error;
				
			long lngRes=m_lngDeleteRecord2DB(p_objOldRecordContent);
			if(lngRes<=0)return lngRes;

			lngRes=m_lngAddNewRecord2DB(p_objNewRecordContent);			
			return lngRes;
		}		
		
		/// <summary>
		/// ɾ����¼
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngDeleteRecord2DB(clsSpecialSymbolValue p_objRecordContent)
		{
			//������                              
			if(p_objRecordContent==null || p_objRecordContent.m_strDeptID==null || p_objRecordContent.m_strSpecialSymbolValue==null)
				return (long)enmOperationResult.Parameter_Error;
			
			//��ȡIDataParameter����
			IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];
			//��˳���IDataParameter��ֵ
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
			m_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
			objDPArr[0].Value=p_objRecordContent.m_strDeptID;
			objDPArr[1].Value=p_objRecordContent.m_strSpecialSymbolValue;
			
			//ִ��SQL
			long lngEff=0;
			long lngRes= m_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL,ref lngEff,objDPArr);
			return lngRes;
		}	
	}
}
