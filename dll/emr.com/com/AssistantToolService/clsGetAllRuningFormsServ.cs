using System;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Data;
//using Microsoft.Data.Odbc;
using System.EnterpriseServices;

namespace com.digitalwave.GetAllRuningFormsServ
{
	/// <summary>
	/// 模板关联与控件描述
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsGetAllRuningFormsServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsGetAllRuningFormsServ()
		{}
		#region SQL

		private const string strInsertGUI_Info_SQL = @"insert into gui_info(form_id,form_desc) values(?,?)";

        private const string strInsertGUI_Info_Detail_SQL = @"insert into gui_info_detail(control_id,form_id,control_desc,control_tabindex) values(?,?,?,?)";

        private const string strInsertIMR_Type_Item_SQL = @"insert into inpatmedrec_type_item(typeid, itemid, itemname, itemtype,itemtabindex) values (?, ?, ?, ?, ?)";


		#endregion

		[AutoComplete]
		public long m_lngSaveGUI_Info(string p_strFormID,string p_strFormDesc)
		{
			if(p_strFormID==null || p_strFormID=="")
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {
                lngRes = m_lngDelGUI_Info(p_strFormID);

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strFormID;
                objDPArr[1].Value = (p_strFormDesc == null ? "" : p_strFormDesc);

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strInsertGUI_Info_SQL, ref lngEff, objDPArr);
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
			return lngRes;
		}
		[AutoComplete]
		public long m_lngSaveGUI_Info_Detail(string p_strFormID,string p_strControlDesc,string p_strControlID,string p_strControlIndex)
		{
            if (string.IsNullOrEmpty(p_strFormID) || string.IsNullOrEmpty(p_strControlID) 
                || string.IsNullOrEmpty(p_strControlDesc) || string.IsNullOrEmpty(p_strControlIndex))
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strControlID.Trim();
                objDPArr[1].Value = p_strFormID.Trim();
                objDPArr[2].Value = p_strControlDesc.Trim();
                objDPArr[3].Value = p_strControlIndex.Trim();

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strInsertGUI_Info_Detail_SQL, ref lngEff, objDPArr);
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
			return lngRes;
		}
		[AutoComplete]
		public long m_lngDelGUI_Info(string p_strFormID)
		{
			if(p_strFormID==null ||p_strFormID=="")
				return (long)enmOperationResult.Parameter_Error;
			string strDeleteGUI_Info = @"delete from gui_info where form_id = ?";
			clsHRPTableService objHRPServ =new clsHRPTableService();

            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strFormID;

			//执行SQL
            long lngEff = -1;
			long lngRes= objHRPServ.lngExecuteParameterSQL(strDeleteGUI_Info,ref lngEff,objDPArr);
            //objHRPServ.Dispose();
			return lngRes;
		}

		[AutoComplete]
		public long m_lngDelGUI_Info_Detail(string p_strFormID)
		{
			if(p_strFormID==null ||p_strFormID=="")
				return (long)enmOperationResult.Parameter_Error;
			string strDeleteGUI_Info_Detail_SQL = @"delete from gui_info_detail where form_id = ?";
			clsHRPTableService objHRPServ =new clsHRPTableService();
			//执行SQL
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strFormID;

            long lngEff = -1;
			long lngRes= objHRPServ.lngExecuteParameterSQL(strDeleteGUI_Info_Detail_SQL,ref lngEff,objDPArr);
            //objHRPServ.Dispose();
			return lngRes;
		}
		[AutoComplete]
		public long m_lngDel_IMR_Type_Item(string p_strFormID)
		{
			if(p_strFormID==null ||p_strFormID=="")
				return (long)enmOperationResult.Parameter_Error;
			string strDeleteIMR_Type_Item_SQL = @"delete from inpatmedrec_type_item where typeid = ?";
			clsHRPTableService objHRPServ =new clsHRPTableService();
			//执行SQL
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strFormID;

            long lngEff = -1;
			long lngRes= objHRPServ.lngExecuteParameterSQL(strDeleteIMR_Type_Item_SQL,ref lngEff,objDPArr);
            //objHRPServ.Dispose();
			return lngRes;
		}
		[AutoComplete]
		public long m_lngSaveIMR_Type_Item(string p_strTypeID,string p_strItemID,string p_strItemName,string p_strItemType,string p_strItemTabIndex)
		{
            if (p_strTypeID == null || p_strItemID == null 
                || p_strItemName == null || p_strItemType == null || p_strItemTabIndex == null)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;// new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strTypeID.Trim();
                objDPArr[1].Value = p_strItemID.Trim();
                objDPArr[2].Value = p_strItemName.Trim();
                objDPArr[3].Value = p_strItemType.Trim();
                objDPArr[4].Value = p_strItemTabIndex.Trim();

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strInsertIMR_Type_Item_SQL, ref lngEff, objDPArr);
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
			return lngRes;
		}
        [AutoComplete]
        public long m_lngGetAllInpatMedRec(out clsInpatMedRec_Type[] p_objTypes)
        {
            p_objTypes = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select typeid, typename from inpatmedrec_type";
                //执行SQL
                DataTable dt = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSql, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                { 
                    p_objTypes = new clsInpatMedRec_Type[dt.Rows.Count];
                    for (int i = 0 ; i < dt.Rows.Count ; i++)
                    {
                        p_objTypes[i] = new clsInpatMedRec_Type();
                        p_objTypes[i].m_strTypeID = dt.Rows[i]["TYPEID"].ToString();
                        p_objTypes[i].m_strTypeName = dt.Rows[i]["TYPENAME"].ToString();
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
            return lngRes;
        }
        [AutoComplete]
        public long m_lngAddInpatMedRecType(clsInpatMedRec_Type p_objType)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"insert into inpatmedrec_type(typeid,typename) values(?,?)";
                //执行SQL
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;// new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objType.m_strTypeID;
                objDPArr[1].Value = p_objType.m_strTypeName;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
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
        public long m_lngModifyInpatMedRecType(string p_strOldTypeId,clsInpatMedRec_Type p_objType)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"update inpatmedrec_type set typeid=?,typename=? where typeid=?";
                //执行SQL
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;// new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objType.m_strTypeID;
                objDPArr[1].Value = p_objType.m_strTypeName;
                objDPArr[2].Value = p_strOldTypeId;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
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
        public long m_lngDeleteInpatMedRecType(clsInpatMedRec_Type p_objType)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"delete from inpatmedrec_type  where typeid=?";
                //执行SQL
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;// new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objType.m_strTypeID;
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
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
        public long m_lngAddInpatMedRecDept(string p_strTypeID,string[] p_strDeptArr,string[] p_strAreaArr)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                m_lngDeleteInpatMedRecDept(p_strTypeID);
                for (int i = 0 ; i < p_strDeptArr.Length ; i++)
                {
                    string strSql = @"insert into inpatmedrec_type_dept(deptid,area_id,typeid)values(?,?,?)";
                    //执行SQL
                    //获取IDataParameter数组
                    IDataParameter[] objDPArr = null;// new Oracle.DataAccess.Client.OracleParameter[4];
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_strDeptArr[i];
                    objDPArr[1].Value = p_strAreaArr[i];
                    objDPArr[2].Value = p_strTypeID;
                    //执行SQL
                    long lngEff = 0;
                    try
                    {
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                    }
                    catch { continue; }
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
        public long m_lngDeleteInpatMedRecDept(string p_strTypeID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"delete from inpatmedrec_type_dept where typeid=? ";
                //执行SQL
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;// new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strTypeID;
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
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
        public long m_lngGetInpatMedRecDept(string p_strTypeID, out string[] p_strDepts, out string[] p_strAreas)
        {
            p_strDepts = null;
            p_strAreas = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select t.deptid,t.area_id from inpatmedrec_type_dept t where t.typeid=?";

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;// new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strTypeID;

                //执行SQL
                DataTable dt = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dt,objDPArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    p_strDepts = new string[dt.Rows.Count];
                    p_strAreas = new string[dt.Rows.Count];
                    for (int i = 0 ; i < dt.Rows.Count ; i++)
                    {
                        p_strDepts[i] = dt.Rows[i]["DEPTID"].ToString();
                        p_strAreas[i] = dt.Rows[i]["AREA_ID"].ToString();
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
            return lngRes;
        }

        
	}
}
