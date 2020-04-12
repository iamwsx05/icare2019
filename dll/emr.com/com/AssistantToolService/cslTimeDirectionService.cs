using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.TimeDirectionService
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsTimeDirectionService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获得所有表TimeDirectionTable,Service层,刘颖源,2003-6-4 17:13:36
        [AutoComplete]
        public long lngGetAllTimeDirectionTable(out clsTimeDirectionTableValue[] p_objTimeDirectionTableValue)
        {

            p_objTimeDirectionTableValue = null;
            string strSQL = @"select t_id,
       t_tabledesc,
       t_tablename,
       t_datetimefiledname,
       t_specialcondition,
       t_specialorder,
       t_availableparametersql,
       t_displayformat,
       t_tabletype,
       t_targetidentityfield
  from TimeDirectionTable ";
            DataTable objDataTableResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            long lngRes = objTabService.DoGetDataTable(strSQL, ref objDataTableResult);

            if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
            {
                p_objTimeDirectionTableValue = new clsTimeDirectionTableValue[objDataTableResult.Rows.Count];
                for (int i = 0; i < p_objTimeDirectionTableValue.Length; i++)
                {
                    p_objTimeDirectionTableValue[i] = new clsTimeDirectionTableValue();
                    p_objTimeDirectionTableValue[i].m_strT_ID = objDataTableResult.Rows[i]["T_ID"].ToString();
                    p_objTimeDirectionTableValue[i].m_strT_TableDesc = objDataTableResult.Rows[i]["T_TABLEDESC"].ToString();
                    p_objTimeDirectionTableValue[i].m_strT_TableName = objDataTableResult.Rows[i]["T_TABLENAME"].ToString();
                    p_objTimeDirectionTableValue[i].m_strT_DateTimeFiledName = objDataTableResult.Rows[i]["T_DATETIMEFILEDNAME"].ToString();
                    p_objTimeDirectionTableValue[i].m_strT_SpecialCondition = objDataTableResult.Rows[i]["T_SPECIALCONDITION"].ToString();
                    p_objTimeDirectionTableValue[i].m_strT_SpecialOrder = objDataTableResult.Rows[i]["T_SPECIALORDER"].ToString();
                    p_objTimeDirectionTableValue[i].m_strT_AvailableParameterSQL = objDataTableResult.Rows[i]["T_AVAILABLEPARAMETERSQL"].ToString();
                    p_objTimeDirectionTableValue[i].m_strT_DisplayFormat = objDataTableResult.Rows[i]["T_DISPLAYFORMAT"].ToString();
                    p_objTimeDirectionTableValue[i].m_strT_TableType = objDataTableResult.Rows[i]["T_TABLETYPE"].ToString();
                    p_objTimeDirectionTableValue[i].m_strT_TargetIdentityField = objDataTableResult.Rows[i]["T_TARGETIDENTITYFIELD"].ToString();
                }
            }
            //objTabService.Dispose();
            return lngRes;
        }

        #endregion

        #region 获得所有表TimeDirectionField,Service层,刘颖源,2003-6-4 17:26:43
        [AutoComplete]
        public long lngGetAllTimeDirectionField(string p_strID, out clsTimeDirectionFieldValue[] p_objTimeDirectionFieldValue)
        {
            string strSQL = @"select t_id,
       f_id,
       f_fielddesc,
       f_datafieldname,
       f_fieldcondiction,
       f_fieldtype
  from TimeDirectionField
 Where T_ID = ?";
            p_objTimeDirectionFieldValue = null;
            DataTable objDataTableResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objTabService.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strID;

            long lngRes = objTabService.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

            if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
            {
                p_objTimeDirectionFieldValue = new clsTimeDirectionFieldValue[objDataTableResult.Rows.Count];
                for (int i = 0; i < p_objTimeDirectionFieldValue.Length; i++)
                {
                    p_objTimeDirectionFieldValue[i] = new clsTimeDirectionFieldValue();
                    p_objTimeDirectionFieldValue[i].m_strT_ID = objDataTableResult.Rows[i]["T_ID"].ToString();
                    p_objTimeDirectionFieldValue[i].m_strF_ID = objDataTableResult.Rows[i]["F_ID"].ToString();
                    p_objTimeDirectionFieldValue[i].m_strF_FieldDesc = objDataTableResult.Rows[i]["F_FIELDDESC"].ToString();
                    p_objTimeDirectionFieldValue[i].m_strF_DataFieldName = objDataTableResult.Rows[i]["F_DATAFIELDNAME"].ToString();
                    p_objTimeDirectionFieldValue[i].m_strF_FieldCondiction = objDataTableResult.Rows[i]["F_FIELDCONDICTION"].ToString();
                    p_objTimeDirectionFieldValue[i].m_strF_FieldType = objDataTableResult.Rows[i]["F_FIELDTYPE"].ToString();
                }
            }
            //objTabService.Dispose();
            return lngRes;
        }

        #endregion

        #region 获得所有可选参数,刘颖源,2003-6-6 11:08:11
        [AutoComplete]
        public long lngGetAllAvailableParamenters(string p_strSqlStatement, out string[] p_strFieldNames, out DataTable dtResult)
        {
            p_strFieldNames = null;
            dtResult = null;
            if (p_strSqlStatement == null || p_strSqlStatement == "") return (-1);

            //DataTable objDataTableResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            long lngRes = objTabService.DoGetDataTable(p_strSqlStatement, ref dtResult);

            if (lngRes <= 0 || dtResult.Columns.Count <= 0) return (-1);
            //p_strFieldNames = new string[objDataTableResult.Columns.Count];
            //for (int i = 0; i < p_strFieldNames.Length; i++)
            //    p_strFieldNames[i] = objDataTableResult.Columns[i].ColumnName;
            //if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
            //{

            //    p_strRowValues = new string[objDataTableResult.Rows.Count, objDataTableResult.Columns.Count];

            //    for (int i = 0; i < p_strRowValues.Length / objDataTableResult.Columns.Count; i++)
            //    {
            //        for (int j = 0; j < objDataTableResult.Columns.Count; j++)
            //        {
            //            p_strRowValues[i, j] = objDataTableResult.Rows[i][j].ToString();
            //        }

            //    }
            //}
            //objTabService.Dispose();
            return lngRes;
        }
        #endregion

        #region 获得所有表TimeDirectionMaxMinValue,Service层,刘颖源,2003-6-10 10:47:18
        [AutoComplete]
        public long lngGetFieldsMaxMinValue(int intFieldType, string strTableName, string strFieldName, out clsTimeDirectionMaxMinValueValue[] p_objTimeDirectionMaxMinValueValue)
        {

            string strSQL = "";
            if (intFieldType == 0)
                strSQL = "select  max(" + strFieldName + " ) as maxvalue,Min(" + strFieldName + ") as minvalue from  " + strTableName;
            else
                strSQL = "select  max(" + clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName(strFieldName, "float") + " ) as maxvalue,min(" + clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName(strFieldName, "float") + ") as minvalue from  " + strTableName;
            p_objTimeDirectionMaxMinValueValue = null;
            DataTable objDataTableResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            long lngRes = objTabService.DoGetDataTable(strSQL, ref objDataTableResult);
            if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
            {
                p_objTimeDirectionMaxMinValueValue = new clsTimeDirectionMaxMinValueValue[objDataTableResult.Rows.Count];
                for (int i = 0; i < p_objTimeDirectionMaxMinValueValue.Length; i++)
                {
                    p_objTimeDirectionMaxMinValueValue[i] = new clsTimeDirectionMaxMinValueValue();
                    p_objTimeDirectionMaxMinValueValue[i].m_strMaxValue = objDataTableResult.Rows[i]["MAXVALUE"].ToString();
                    p_objTimeDirectionMaxMinValueValue[i].m_strMinValue = objDataTableResult.Rows[i]["MINVALUE"].ToString();

                }
            }
            //objTabService.Dispose();
            return (lngRes);
        }
        #endregion


    }
}
