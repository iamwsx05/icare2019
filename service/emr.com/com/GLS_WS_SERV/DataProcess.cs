using System;
using System.Collections;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;
 
namespace com.digitalwave.GLS_WS.ApplyReportServer
{
    /// <summary>
    /// 
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class DataProcess : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		
        //private DbService service ;//= new DbService();
        //private clsHRPTableService m_objHRPServ;

		public DataProcess()
		{
            //service = (DbService)this.CreateService();	
            //m_objHRPServ = new clsHRPTableService();
		}

		/// <summary>
		/// 返回检查申请单类型
		/// </summary>
		/// <returns></returns>
        [AutoComplete]
		public DataTable GetApplyList()
		{
			string sql = "select * from AR_APPLY_TYPELIST where Deleted <> 1 order by ORDERSEQ_INT";
//			return service.SqlSelect(sql);
			DataTable dtValue = new DataTable();
            clsHRPTableService m_objHRPServ = new clsHRPTableService();
			m_objHRPServ.DoGetDataTable(sql,ref dtValue);
			return dtValue;
			
		}

		/// <summary>
		/// 取得数下一个数字型ID
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="fieldName"></param>
		/// <returns></returns>
        [AutoComplete]
        public string GetNextID(string tableName,string fieldName)
		{
			string strNextID = "";
			string strMaxID = "";
			DataTable dtValue = new DataTable();
            clsHRPTableService m_objHRPServ = new clsHRPTableService();
			long res=m_objHRPServ.DoGetDataTable("select max(" + fieldName + ") from " + tableName,ref dtValue);
			if(res>0 && dtValue.Rows.Count == 1)
			{
				strMaxID = dtValue.Rows[0][0].ToString();
                if (strMaxID.Trim() == string.Empty)
                {
                    strMaxID = "0";
                }
			}
			strNextID = ((int.Parse(strMaxID))+1).ToString();
			return strNextID;
		}
        
        [AutoComplete]
        public string GetApplyID()
        {
            string m_strMaxID = string.Empty;
            string strSQL = "select SEQ_AR_COMMON_APPLYID.NEXTVAL from dual";
            DataTable m_objTable = new DataTable();
            clsHRPTableService m_objHRPServ = new clsHRPTableService();
            long lngRes = m_objHRPServ.DoGetDataTable(strSQL, ref m_objTable);
            if (lngRes > 0 && m_objTable.Rows.Count > 0)
            {
                m_strMaxID = m_objTable.Rows[0][0].ToString();

            }
            return m_strMaxID;
        }

        [AutoComplete]
        public DataTable SqlSelect(string sql)
		{
			DataTable dtValue = new DataTable();
//			return service.SqlSelect(sql);
            clsHRPTableService m_objHRPServ = new clsHRPTableService();
			m_objHRPServ.DoGetDataTable(sql,ref dtValue);
			return dtValue;
		}

        [AutoComplete]
        public bool Update(string tableName, DataSet ds)
		{
			bool b = false;
            //DbService service = (DbService)this.CreateService();
            //try
            //{
            //    b = service.UpdateData(tableName, ds);
            //}
            //catch(Exception ex)
            //{
            //    //System.Windows.Forms.MessageBox.Show(ex.Message);
            //}
			return b;

		}

        [AutoComplete]
        public bool SqlExecute(string sql)
		{
//			return (service.SqlExecute(sql) >= 0);
            clsHRPTableService m_objHRPServ = new clsHRPTableService();
			return (m_objHRPServ.DoExcute(sql) >= 0);
		}

		/// <summary>
		/// 返回一个检查类型对应的报表
		/// </summary>
		/// <param name="typeID"></param>
		/// <returns></returns>
        [AutoComplete]
        public DataTable GetReportForm(string typeID)
		{
			string sql = @"SELECT B.FORMDESC as FormTitle , A.FORMCLSNAME_C as FormName , A.FORMCLSNAME_P
							FROM
							AR_FORM B
							, AR_APPLYRELATINGREPORT A
							WHERE
								(A.FORMCLSNAME_C = B.FORMCLSNAME) AND ( A.FORMCLSNAME_P = '{0}')";
			DataTable ds = new DataTable();
            clsHRPTableService m_objHRPServ = new clsHRPTableService();
			m_objHRPServ.DoGetDataTable( string.Format(sql,typeID),ref ds );
			return ds;
		}

        [AutoComplete]
        private object CreateService()
		{
            //Type comType = typeof(DigitalWave.DbService);
            //object comObj = com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(comType);
            //return comObj;
            return null;
		}

        [AutoComplete]
        public DataTable ExecuteScalar(string sql)
		{
//			return service.ExecuteScalar(sql);
			DataTable ds = new DataTable();
            clsHRPTableService m_objHRPServ = new clsHRPTableService();
			m_objHRPServ.DoGetDataTable(sql,ref ds);
			return ds;
		}
	}
}
