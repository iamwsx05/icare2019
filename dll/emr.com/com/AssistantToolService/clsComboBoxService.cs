using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService ;
using System.Collections;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.ComboBoxService
{
	/// <summary>
	/// ��������Ŀ�����
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsComboBoxService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{		
		#region  SQL ����
		private const string c_strAddItemSQL=@"insert into combobox_value (deptid, typeid, itemid, itemcontent) values (?, ?, ?, ?)";
		
		private const string c_strModifyItemSQL = @"update combobox_value set itemcontent = ? where deptid = ? and (typeid = ?) and (itemid = ?) and (itemcontent = ?)";

        private const string c_strGetAllItem = @"select distinct deptid, typeid, itemid, itemcontent from combobox_value where deptid = ? and typeid = ? and itemid = ? order by itemcontent";

		private const string c_strDeleteItemSQL = @"delete from combobox_value where ( deptid = ?) and (typeid = ?) and (itemid = ?) and (itemcontent = ?)";
		#endregion
		
		public clsComboBoxService()
		{}

		/// <summary>
		/// ��ȡ������������Ŀ����
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptID">����ID</param>
		/// <param name="p_strTypeID">��ID</param>
		/// <param name="p_strItemID">��ĿID</param>
		/// <param name="p_objValueArr">���ص���Ŀ����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllItem( string p_strDeptID,string p_strTypeID,string p_strItemID,out clsComboBoxValue[] p_objValueArr)
		{
			p_objValueArr = null;
			if(p_strDeptID == null || p_strItemID == null || p_strTypeID == null)
				return (long)enmOperationResult.Parameter_Error;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			
			long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

                objDPArr[0].Value = p_strDeptID;
                objDPArr[1].Value = p_strTypeID;
                objDPArr[2].Value = p_strItemID;

                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetAllItem, ref dtbValue, objDPArr);
                if (lngRes <= 0)
                {
                    return lngRes;
                }
                p_objValueArr = new clsComboBoxValue[dtbValue.Rows.Count];
                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    p_objValueArr[i] = new clsComboBoxValue();
                    p_objValueArr[i].m_strDeptID = dtbValue.Rows[i]["DEPTID"].ToString().Trim();
                    p_objValueArr[i].m_strTypeID = dtbValue.Rows[i]["TYPEID"].ToString().Trim();
                    p_objValueArr[i].m_strItemID = dtbValue.Rows[i]["ITEMID"].ToString().Trim();
                    p_objValueArr[i].m_strItemContent = dtbValue.Rows[i]["ITEMCONTENT"].ToString().Trim();
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
			return lngRes;
		}

		/// <summary>
		/// ����������һ����Ŀ�����ݿ�
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddItemToDB( clsComboBoxValue p_objValue)
		{
			if(p_objValue == null)
				return (long)enmOperationResult.Parameter_Error;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
            try
            {


                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

                objDPArr[0].Value = p_objValue.m_strDeptID;
                objDPArr[1].Value = p_objValue.m_strTypeID;
                objDPArr[2].Value = p_objValue.m_strItemID;
                objDPArr[3].Value = p_objValue.m_strItemContent;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddItemSQL, ref lngEff, objDPArr);

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

		/// <summary>
		/// �����ݿ�ɾ���������һ������
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteItem( clsComboBoxValue p_objValue)
		{
			if(p_objValue == null)
				return (long)enmOperationResult.Parameter_Error;
				clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
            try
            {

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

                objDPArr[0].Value = p_objValue.m_strDeptID;
                objDPArr[1].Value = p_objValue.m_strTypeID;
                objDPArr[2].Value = p_objValue.m_strItemID;
                objDPArr[3].Value = p_objValue.m_strItemContent;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteItemSQL, ref lngEff, objDPArr);

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

		/// <summary>
		/// �޸��������һ����Ŀ
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objOldValue">�ɵ���Ŀ</param>
		/// <param name="p_objNewValue">�µ���Ŀ</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyItem( clsComboBoxValue p_objOldValue,string p_strNewItemContent)
		{
			if(p_objOldValue == null || p_strNewItemContent == null)
				return (long)enmOperationResult.Parameter_Error;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
            try
            {

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

                objDPArr[0].Value = p_strNewItemContent;
                objDPArr[1].Value = p_objOldValue.m_strDeptID;
                objDPArr[2].Value = p_objOldValue.m_strTypeID;
                objDPArr[3].Value = p_objOldValue.m_strItemID;
                objDPArr[4].Value = p_objOldValue.m_strItemContent;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyItemSQL, ref lngEff, objDPArr);

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

		#region ���뵥�����浥ComBox
		/// <summary>
		/// �õ����뵥�����浥������������Ŀ����
		/// </summary>
		/// <param name="p_strTypeID">��������</param>
		/// <param name="p_strItemID">�ؼ�����</param>
		/// <param name="p_objValueArr">����ֵ</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetApplyReprotItem(string p_strTypeID,string p_strItemID,out clsComboBoxValue[] p_objValueArr)
		{
			string SQL="";
			p_objValueArr = null;
			DataTable dtbValue = new DataTable();

			SQL="select typeid,itemid,itemcontent from ar_combobox_value where typeid=? and itemid=?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strTypeID.Replace("'", "''");
            objDPArr[1].Value = p_strItemID;

            long lngRes = objHRPServ.lngGetDataTableWithParameters(SQL, ref dtbValue,objDPArr);
            //objHRPServ.Dispose();
			if (lngRes<0)
				return lngRes;
			if (dtbValue!=null)
			{
				p_objValueArr = new clsComboBoxValue[dtbValue.Rows.Count];
				for (int i=0;i<dtbValue.Rows.Count;i++)
				{
					p_objValueArr[i]=new clsComboBoxValue();
					p_objValueArr[i].m_strDeptID="";
					p_objValueArr[i].m_strTypeID=dtbValue.Rows[i]["TYPEID"].ToString();
					p_objValueArr[i].m_strItemID=dtbValue.Rows[i]["ITEMID"].ToString();
					p_objValueArr[i].m_strItemContent=dtbValue.Rows[i]["ITEMCONTENT"].ToString();
				}
			}
			return lngRes;
		}
		/// <summary>
		/// ����������һ����Ŀ�����ݿ�
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddApplyReprotItemToDB(clsComboBoxValue p_objValue)
		{
			if(p_objValue == null)
				return (long)enmOperationResult.Parameter_Error;

			string SQL="";
			long lngRet=0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
			SQL="delete from ar_combobox_value where typeid=? and itemid=? and itemcontent=?";

            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_objValue.m_strTypeID.Replace("'", "''");
            objDPArr[1].Value = p_objValue.m_strItemID.Replace("'", "''");
            objDPArr[2].Value = p_objValue.m_strItemContent.Replace("'", "''");

            long lngEff = -1;
            lngRet = objHRPServ.lngExecuteParameterSQL(SQL,ref lngEff,objDPArr);

            if (lngRet < 0)
            {
                //objHRPServ.Dispose();
                return lngRet;
            }
			SQL="insert into ar_combobox_value (typeid,itemid,itemcontent) values (?,?,?)"	;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_objValue.m_strTypeID.Replace("'", "''");
            objDPArr[1].Value = p_objValue.m_strItemID.Replace("'", "''");
            objDPArr[2].Value = p_objValue.m_strItemContent.Replace("'", "''");

            lngRet = objHRPServ.lngExecuteParameterSQL(SQL,ref lngEff,objDPArr);
            //objHRPServ.Dispose();
            return lngRet;
		}

		/// <summary>
		/// �����ݿ�ɾ���������һ������
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteApplyReportItem(clsComboBoxValue p_objValue)
		{
			if(p_objValue == null)
				return (long)enmOperationResult.Parameter_Error;

			string SQL="";
            clsHRPTableService objHRPServ = new clsHRPTableService();
			SQL="delete from ar_combobox_value where typeid=? and itemid=? and itemcontent=?";
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_objValue.m_strTypeID.Replace("'", "''");
            objDPArr[1].Value = p_objValue.m_strItemID.Replace("'", "''");
            objDPArr[2].Value = p_objValue.m_strItemContent.Replace("'", "''");

            long lngEff = -1;
            long lngRes = objHRPServ.lngExecuteParameterSQL(SQL,ref lngEff,objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		/// <summary>
		/// �޸��������һ����Ŀ
		/// </summary>
		/// <param name="p_objOldValue">�ɵ���Ŀ</param>
		/// <param name="p_objNewValue">�µ���Ŀ</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyApplyReprotItem(clsComboBoxValue p_objOldValue,string p_strNewItemContent)
		{
			if(p_objOldValue == null || p_strNewItemContent == null)
				return (long)enmOperationResult.Parameter_Error;

			string SQL="";
			SQL="update ar_combobox_value set itemcontent=? where typeid=? and itemid=? and itemcontent=?";
            clsHRPTableService objHRPServ = new clsHRPTableService();

            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(4, out objDPArr);
            objDPArr[0].Value = p_strNewItemContent.Replace("'", "''");
            objDPArr[1].Value = p_objOldValue.m_strTypeID.Replace("'", "''");
            objDPArr[2].Value = p_objOldValue.m_strItemID.Replace("'", "''");
            objDPArr[3].Value = p_objOldValue.m_strItemContent.Replace("'", "''");

            long lngEff = -1;
            long lngRes = objHRPServ.lngExecuteParameterSQL(SQL,ref lngEff,objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}
		#endregion ���뵥�����浥ComBox
	}
}
