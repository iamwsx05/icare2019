using System;
using System.Collections;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility;
using weCare.Core.Entity;

namespace com.digitalwave.Emr.ConfigService
{
    /// <summary>
    /// 模板关联与控件描述
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsInitFormElementService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 模板关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objType"></param>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveFormElement(clsInpatMedRec_Type p_objType, clsInpatMedRec_Type_Item[] p_objItems)
        {
            if (p_objType == null || p_objItems == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            //string strSql = @"insert into GUI_Info(Form_ID,Form_Desc) values(?,?)";
            string strSql = @"insert into gui_form(ID,Form_ID,PARENT_ID,Form_Desc) values(?,?,?,?)";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = m_lngDeleteFormElement(p_objType.m_strTypeID);
                if (lngRes > 0)
                {
                    DataTable dtbGUITemp = null;
                    lngRes = m_lngGetGUI_FROMData(p_objType.m_strTypeID, out dtbGUITemp);

                    IDataParameter[] objDPArr = null;
                    long lngEff = 0;
                    if (dtbGUITemp == null || dtbGUITemp.Rows.Count <= 0)
                    {
                        int intID = 0;
                        lngRes = m_lngGetMaxIDOfGUI_FORM(out intID);

                        //获取IDataParameter数组
                        objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                        objDPArr[0].Value = intID;
                        objDPArr[1].Value = p_objType.m_strTypeID;
                        objDPArr[2].Value = 0;
                        objDPArr[3].Value = p_objType.m_strTypeName;

                        //执行SQL
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                    }

                    if (lngRes > 0)
                    {
                        //strSql = @"insert into GUI_Info_Detail(Control_ID,Form_ID,Control_Desc,CONTROL_TABINDEX) values(?,?,?,?)";
                        strSql = @"INSERT INTO gui_control (CONTROL_ID,FORM_ID,CONTROL_DESC,ORDER_NO) VALUES (?,?,?,?)";
                        if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                        {
                            for (int i = 0; i < p_objItems.Length; i++)
                            {
                                //获取IDataParameter数组
                                objDPArr = null;
                                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                                objDPArr[0].Value = p_objItems[i].m_strItemID;
                                objDPArr[1].Value = p_objType.m_strTypeID;
                                objDPArr[2].Value = p_objItems[i].m_strItemName;
                                objDPArr[3].Value = p_objItems[i].m_strIndex;
                                lngEff = 0;
                                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                            }
                        }
                        else
                        {
                            //objDPArr = null;
                            //objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                            DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.String, DbType.Int32 };
                            object[][] objValues = new object[4][];
                            ArrayList arlItems = new ArrayList(p_objItems.Length);
                            for (int i1 = 0; i1 < p_objItems.Length; i1++)
                            {
                                if (p_objItems[i1].m_blnCanTemplate)
                                    arlItems.Add(p_objItems[i1]);
                            }
                            if (arlItems.Count > 0)
                            {
                                for (int j = 0; j < objValues.Length; j++)
                                {
                                    objValues[j] = new object[arlItems.Count];
                                }
                                for (int k1 = 0; k1 < arlItems.Count; k1++)
                                {
                                    objValues[0][k1] = ((clsInpatMedRec_Type_Item)arlItems[k1]).m_strItemID;
                                }
                                for (int k2 = 0; k2 < arlItems.Count; k2++)
                                {
                                    objValues[1][k2] = p_objType.m_strTypeID;
                                }
                                for (int k3 = 0; k3 < arlItems.Count; k3++)
                                {
                                    objValues[2][k3] = ((clsInpatMedRec_Type_Item)arlItems[k3]).m_strItemName;
                                }
                                for (int k4 = 0; k4 < arlItems.Count; k4++)
                                {
                                    objValues[3][k4] = ((clsInpatMedRec_Type_Item)arlItems[k4]).m_strIndex;
                                }
                                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSql, objValues, dbTypes);
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //finally
            //{
            //    //objHRPServ.Dispose();
            //}
            return lngRes;
        }
        /// <summary>
        /// 删除模板关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFormID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteFormElement(string p_strFormID)
        {
            if (string.IsNullOrEmpty(p_strFormID))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            //string strSql = @"DELETE FROM GUI_Info_Detail WHERE Form_ID = ?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //IDataParameter[] objDPArr = null;
                //objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //objDPArr[0].Value = p_strFormID;

                ////执行SQL
                long lngEff = 0;
                //lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                //if (lngRes > 0)
                //{
                //strSql = @"DELETE FROM GUI_Info WHERE Form_ID = ?";
                string strSql = @"DELETE FROM gui_control WHERE Form_ID = ?";
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strFormID;
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
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
            return lngRes;
        }
        /// <summary>
        /// 删除专科关联
        /// </summary>
        /// <param name="p_strFormID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDel_IMR_Type_Item(string p_strFormID)
        {
            if (p_strFormID == null || p_strFormID == "")
                return (long)enmOperationResult.Parameter_Error;
            string strSql = @"DELETE FROM InpatMedRec_Type_Item WHERE TypeID = ?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            //执行SQL
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strFormID;

            long lngEff = -1;
            long lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
        }
        /// <summary>
        /// 保存专科关联
        /// </summary>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveIMR_Type_Item(string p_strTypeId, clsInpatMedRec_Type_Item[] p_objItems)
        {
            if (p_objItems == null || string.IsNullOrEmpty(p_strTypeId))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            string strSql = @"INSERT INTO InpatMedRec_Type_Item(TypeID, ItemID, ItemName, ItemType,ITEMTABINDEX) VALUES (?, ?, ?, ?, ?)";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = m_lngDel_IMR_Type_Item(p_strTypeId);
                IDataParameter[] objDPArr = null;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_objItems.Length; i++)
                    {
                        objDPArr = null;
                        //获取IDataParameter数组
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr);

                        objDPArr[0].Value = p_strTypeId;
                        objDPArr[1].Value = p_objItems[i].m_strItemID;
                        objDPArr[2].Value = p_objItems[i].m_strItemName;
                        objDPArr[3].Value = p_objItems[i].m_strItemType;
                        objDPArr[4].Value = p_objItems[i].m_strIndex;

                        //执行SQL
                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    //objDPArr = null;
                    //objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int32 };
                    object[][] objValues = new object[5][];
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_objItems.Length];
                    }

                    for (int k1 = 0; k1 < p_objItems.Length; k1++)
                    {
                        objValues[0][k1] = p_strTypeId;
                    }
                    for (int k2 = 0; k2 < p_objItems.Length; k2++)
                    {
                        objValues[1][k2] = p_objItems[k2].m_strItemID;
                    }
                    for (int k3 = 0; k3 < p_objItems.Length; k3++)
                    {
                        objValues[2][k3] = p_objItems[k3].m_strItemName;
                    }
                    for (int k4 = 0; k4 < p_objItems.Length; k4++)
                    {
                        objValues[3][k4] = p_objItems[k4].m_strItemType;
                    }
                    for (int k5 = 0; k5 < p_objItems.Length; k5++)
                    {
                        objValues[4][k5] = p_objItems[k5].m_strIndex;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSql, objValues, dbTypes);
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
        /// 从GUI_FORM表获取指定窗体的数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFormID">窗体名</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGUI_FROMData(string p_strFormID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strFormID))
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                string strSQL = @"select * from GUI_FORM t where t.form_id = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                //执行SQL
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strFormID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取GUI_FORM表的最大ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intID">最大ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxIDOfGUI_FORM(out int p_intID)
        {
            p_intID = 0;

            long lngRes = 0;
            try
            {
                string strSQL = @"select max(ID) from GUI_FORM";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    if (int.TryParse(dtbResult.Rows[0][0].ToString(), out p_intID))
                    {
                        p_intID++;
                    }
                    else
                    {
                        p_intID = 0;
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
    }
}
