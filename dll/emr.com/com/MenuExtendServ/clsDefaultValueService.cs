using System;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Data;

using System.EnterpriseServices;

namespace com.digitalwave.iCare.Public.MenuExtend.Service
{
	/// <summary>
	/// 操作默认值的中间层
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsDefaultValueService2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		private clsHRPTableService m_objServ = new clsHRPTableService();

		private const string c_strAdd = @"INSERT INTO DefaultValue
(DeptID, Area_ID,FormName, ControlName, Content)
VALUES (?,?,?,?,?)";

		private const string c_strDel = @"DELETE FROM DefaultValue
Where rtrim(DeptID) = ? And rtrim(Area_ID) = ? And FormName = ?";

		private const string c_strGet = @"Select * From DefaultValue
Where rtrim(DeptID) = ? And rtrim(Area_ID) = ? And FormName = ?";

		[AutoComplete]
		public long m_lngSaveDefaultValue(clsCustomDefaultValue[] p_objArr)
		{
			if(p_objArr == null || p_objArr.Length ==0)
				return 0;

			long lngRes = 0;
			long lngEff = 0;

			if(m_blnHasExist(p_objArr[0].m_strDeptID,p_objArr[0].m_strAreaID,p_objArr[0].m_strFormName))
			{
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
				m_objServ.CreateDatabaseParameter(3,out objDPArr);
				objDPArr[0].Value = p_objArr[0].m_strDeptID;
				objDPArr[1].Value = p_objArr[0].m_strAreaID;
				objDPArr[2].Value = p_objArr[0].m_strFormName;

				lngRes = m_objServ.lngExecuteParameterSQL(c_strDel,ref lngEff,objDPArr);
				if(lngRes < 0) return lngRes;
			}

			for(int i = 0; i < p_objArr.Length; i++)
			{
				//最好不要拼字符串，因为用户可能会输入"'"delete等可能会影响到数据库的内容
				//					string strSql = @"";
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
				m_objServ.CreateDatabaseParameter(5,out objDPArr);
				objDPArr[0].Value = p_objArr[i].m_strDeptID;
				objDPArr[1].Value = p_objArr[i].m_strAreaID;
				objDPArr[2].Value = p_objArr[i].m_strFormName;
				objDPArr[3].Value = p_objArr[i].m_strControlName;
				objDPArr[4].Value = p_objArr[i].m_strContent;
				
				lngRes = m_objServ.lngExecuteParameterSQL(c_strAdd,ref lngEff,objDPArr);
				if(lngRes < 0) return lngRes;
			}				
			
			return lngRes;

		}

		[AutoComplete]
		private bool m_blnHasExist(string p_strDeptID,string p_strAreaID,string p_strFormName)
		{
			IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
			m_objServ.CreateDatabaseParameter(3,out objDPArr);
			objDPArr[0].Value = p_strDeptID;
			objDPArr[1].Value = p_strAreaID;
			objDPArr[2].Value = p_strFormName;

			DataTable  dtResult = new DataTable();
			long lngRes = m_objServ.lngGetDataTableWithParameters(c_strGet,ref dtResult,objDPArr);
			if(lngRes > 0 && dtResult.Rows.Count > 0)
				return true;
			return false;
		}

		/// <summary>
		/// 获取默认值
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_strFormName"></param>
		/// <param name="p_objArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDefaultValue(string p_strDeptID,string p_strAreaID,string p_strFormName,out clsCustomDefaultValue[] p_objArr)
		{
			p_objArr = null;

			IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
			m_objServ.CreateDatabaseParameter(3,out objDPArr);
			objDPArr[0].Value = p_strDeptID;
			objDPArr[1].Value = p_strAreaID;
			objDPArr[2].Value = p_strFormName;

			DataTable  dtResult = new DataTable();
			long lngRes = m_objServ.lngGetDataTableWithParameters(c_strGet,ref dtResult,objDPArr);
			int intCount = dtResult.Rows.Count;
			if(lngRes > 0 && intCount > 0)
			{
				p_objArr = new clsCustomDefaultValue[intCount];
				for(int i = 0; i < intCount; i++)
				{
					p_objArr[i] = new clsCustomDefaultValue();
					p_objArr[i].m_strDeptID = dtResult.Rows[i]["DEPTID"].ToString().Trim();
					p_objArr[i].m_strAreaID = dtResult.Rows[i]["AREA_ID"].ToString().Trim();
					p_objArr[i].m_strFormName = dtResult.Rows[i]["FORMNAME"].ToString().Trim();
					p_objArr[i].m_strControlName = dtResult.Rows[i]["CONTROLNAME"].ToString().Trim();
					p_objArr[i].m_strContent = dtResult.Rows[i]["CONTENT"].ToString().Trim();
				}
			}

			return lngRes;
		}
	}
}
