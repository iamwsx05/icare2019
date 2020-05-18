using System;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Data;
//using Microsoft.Data.Odbc;
using System.EnterpriseServices;

namespace iCare.CustomFromService
{
    /// <summary>
    /// Summary description for clsGetAllRuningFormsServ.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsGetAllRuningFormsServ2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region SQL

        private const string strInsertGUI_Info_SQL = @"insert into GUI_Info(Form_ID,Form_Desc) values(?,?)";

        private const string strInsertGUI_Info_Detail_SQL = @"insert into GUI_Info_Detail(Control_ID,Form_ID,Control_Desc) values(?,?,?)";

        private const string strInsertIMR_Type_Item_SQL = @"INSERT INTO InpatMedRec_Type_Item(TypeID, ItemID, ItemName, ItemType) VALUES (?, ?, ?, ?)";


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strFormID"></param>
        /// <param name="p_strFormDesc"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveGUI_Info(string p_strFormID, string p_strFormDesc)
        {
            if (p_strFormID == null || p_strFormID == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strFormID"></param>
        /// <param name="p_strControlDesc"></param>
        /// <param name="p_strControlID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveGUI_Info_Detail(string p_strFormID, string p_strControlDesc, string p_strControlID)
        {
            if (p_strFormID == null || p_strControlID == null || p_strFormID == "" || p_strControlID == "" || p_strControlDesc == null || p_strControlDesc == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];

                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strControlID;
                objDPArr[1].Value = p_strFormID;
                objDPArr[2].Value = p_strControlDesc;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strFormID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelGUI_Info(string p_strFormID)
        {
            if (p_strFormID == null || p_strFormID == "")
                return (long)enmOperationResult.Parameter_Error;
            string strDeleteGUI_Info = @"DELETE FROM GUI_Info WHERE Form_ID = '" + p_strFormID + "'";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //执行SQL
                lngRes = objHRPServ.DoExcute(strDeleteGUI_Info);

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
        public long m_lngDelGUI_Info_Detail(string p_strFormID)
        {
            if (p_strFormID == null || p_strFormID == "")
                return (long)enmOperationResult.Parameter_Error;
            string strDeleteGUI_Info_Detail_SQL = @"DELETE FROM GUI_Info_Detail WHERE Form_ID = '" + p_strFormID + "'";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;

            try
            {
                //执行SQL
                lngRes = objHRPServ.DoExcute(strDeleteGUI_Info_Detail_SQL);

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
        public long m_lngDel_IMR_Type_Item(string p_strFormID)
        {
            if (p_strFormID == null || p_strFormID == "")
                return (long)enmOperationResult.Parameter_Error;
            string strDeleteIMR_Type_Item_SQL = @"DELETE FROM InpatMedRec_Type_Item WHERE TypeID = '" + p_strFormID + "'";


            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;

            try
            {
                //执行SQL
                lngRes = objHRPServ.DoExcute(strDeleteIMR_Type_Item_SQL);

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
        public long m_lngSaveIMR_Type_Item(string p_strTypeID, string p_strItemID, string p_strItemName, string p_strItemType)
        {
            if (p_strTypeID == null || p_strItemID == null || p_strItemName == null || p_strItemType == null)
                return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;

            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];

                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strTypeID;
                objDPArr[1].Value = p_strItemID;
                objDPArr[2].Value = p_strItemName;
                objDPArr[3].Value = p_strItemType;

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
    }
}
