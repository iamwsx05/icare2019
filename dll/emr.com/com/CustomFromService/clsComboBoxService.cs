using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using System.Collections;
using System.Data;
using weCare.Core.Entity;

namespace iCare.CustomFromService
{
    /// <summary>
    /// 下拉框项目服务层
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsComboBoxService2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        #region  SQL 定义
        private const string c_strAddItemSQL = @"INSERT INTO ComboBox_Value (DeptID, TypeID, ItemID, ItemContent) VALUES (?, ?, ?, ?)";

        private const string c_strModifyItemSQL = @"UPDATE ComboBox_Value SET ItemContent = ? WHERE (DeptID = ?) AND (TypeID = ?) AND (ItemID = ?) AND (ItemContent = ?)";

        //====================修改SQL语句 出去部门病区的限制
        //private const string c_strGetAllItem = @"select Distinct * from ComboBox_Value where trim(DeptID) = ? and TypeID = ? and ItemID = ? order by ItemContent";
        private const string c_strGetAllItem = @"select Distinct * from ComboBox_Value where TypeID = ? and ItemID = ? order by ItemContent";
        //====================修改SQL语句 出去部门病区的限制

        private const string c_strDeleteItemSQL = @"DELETE FROM ComboBox_Value WHERE (DeptID = ?) AND (TypeID = ?) AND (ItemID = ?) AND (ItemContent = ?)";
        #endregion
        
        /// <summary>
        /// 获取下拉框所有项目内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTypeID">表单ID</param>
        /// <param name="p_strItemID">项目ID</param>
        /// <param name="p_objValueArr">返回的项目数组</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllItem(string p_strTypeID, string p_strItemID, out clsComboBoxValue[] p_objValueArr)
        {
            p_objValueArr = null;
            if (p_strItemID == null || p_strTypeID == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strTypeID;
                objDPArr[1].Value = p_strItemID;

                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetAllItem, ref dtbValue, objDPArr);
                if (lngRes <= 0)
                    return lngRes;
                p_objValueArr = new clsComboBoxValue[dtbValue.Rows.Count];
                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    p_objValueArr[i] = new clsComboBoxValue();
                    p_objValueArr[i].m_strDeptID = dtbValue.Rows[i]["DEPTID"].ToString();
                    p_objValueArr[i].m_strTypeID = dtbValue.Rows[i]["TYPEID"].ToString();
                    p_objValueArr[i].m_strItemID = dtbValue.Rows[i]["ITEMID"].ToString();
                    p_objValueArr[i].m_strItemContent = dtbValue.Rows[i]["ITEMCONTENT"].ToString();
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
        /// 添加下拉框的一个项目到数据库
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddItemToDB(clsComboBoxValue p_objValue)
        {
            if (p_objValue == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
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
        /// 从数据库删除下拉框的一项内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteItem(clsComboBoxValue p_objValue)
        {
            if (p_objValue == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
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
        /// 修改下拉框的一个项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOldValue">旧的项目</param>
        /// <param name="p_objNewValue">新的项目</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyItem(clsComboBoxValue p_objOldValue, string p_strNewItemContent)
        {
            if (p_objOldValue == null || p_strNewItemContent == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
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

        #region 申请单、报告单ComBox
        /// <summary>
        /// 得到申请单、报告单的拉框所有项目内容
        /// </summary>
        /// <param name="p_strTypeID">窗体名称</param>
        /// <param name="p_strItemID">控件名称</param>
        /// <param name="p_objValueArr">返回值</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetApplyReprotItem(string p_strTypeID, string p_strItemID, out clsComboBoxValue[] p_objValueArr)
        {
            p_objValueArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string SQL = "";
                DataTable dtbValue = new DataTable();

                SQL = "select TYPEID,ITEMID,ITEMCONTENT from AR_COMBOBOX_VALUE where TYPEID='" + p_strTypeID.Replace("'", "''") + "' and ITEMID='" + p_strItemID + "'";

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(SQL, ref dtbValue);

                if (lngRes < 0)
                    return lngRes;
                if (dtbValue != null)
                {
                    p_objValueArr = new clsComboBoxValue[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_objValueArr[i] = new clsComboBoxValue();
                        p_objValueArr[i].m_strDeptID = "";
                        p_objValueArr[i].m_strTypeID = dtbValue.Rows[i]["TYPEID"].ToString();
                        p_objValueArr[i].m_strItemID = dtbValue.Rows[i]["ITEMID"].ToString();
                        p_objValueArr[i].m_strItemContent = dtbValue.Rows[i]["ITEMCONTENT"].ToString();
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
            return lngRes;
        }
        /// <summary>
        /// 添加下拉框的一个项目到数据库
        /// </summary>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddApplyReprotItemToDB(clsComboBoxValue p_objValue)
        {
            if (p_objValue == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                string SQL = "";

                SQL = "delete from AR_COMBOBOX_VALUE where TYPEID='" + p_objValue.m_strTypeID.Replace("'", "''") + "' and ITEMID='" + p_objValue.m_strItemID.Replace("'", "''") + "' and ITEMCONTENT='" + p_objValue.m_strItemContent.Replace("'", "''") + "'";
                lngRes = objHRPServ.DoExcute(SQL);

                if (lngRes < 0)
                    return lngRes;
                SQL = "insert into AR_COMBOBOX_VALUE (TYPEID,ITEMID,ITEMCONTENT) values ('" + p_objValue.m_strTypeID.Replace("'", "''") + "','" + p_objValue.m_strItemID.Replace("'", "''") + "','" + p_objValue.m_strItemContent.Replace("'", "''") + "')";

                lngRes = objHRPServ.DoExcute(SQL);
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
        /// 从数据库删除下拉框的一项内容
        /// </summary>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteApplyReportItem(clsComboBoxValue p_objValue)
        {
            if (p_objValue == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string SQL = "";

                SQL = "delete from AR_COMBOBOX_VALUE where TYPEID='" + p_objValue.m_strTypeID.Replace("'", "''") + "' and ITEMID='" + p_objValue.m_strItemID.Replace("'", "''") + "' and ITEMCONTENT='" + p_objValue.m_strItemContent.Replace("'", "''") + "'";
                lngRes = objHRPServ.DoExcute(SQL);
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
        /// 修改下拉框的一个项目
        /// </summary>
        /// <param name="p_objOldValue">旧的项目</param>
        /// <param name="p_objNewValue">新的项目</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyApplyReprotItem(clsComboBoxValue p_objOldValue, string p_strNewItemContent)
        {
            if (p_objOldValue == null || p_strNewItemContent == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string SQL = "";
                SQL = "update AR_COMBOBOX_VALUE set ITEMCONTENT='" + p_strNewItemContent.Replace("'", "''") + "' where TYPEID='" + p_objOldValue.m_strTypeID.Replace("'", "''") + "' and ITEMID='" + p_objOldValue.m_strItemID.Replace("'", "''") + "' and ITEMCONTENT='" + p_objOldValue.m_strItemContent.Replace("'", "''") + "'";

                lngRes = objHRPServ.DoExcute(SQL);
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
        #endregion 申请单、报告单ComBox
    }
}
