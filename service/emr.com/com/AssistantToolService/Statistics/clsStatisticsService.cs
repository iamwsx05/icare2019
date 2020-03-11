using System.EnterpriseServices;
using System;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using weCare.Core.Entity;


namespace com.digitalwave.StatisticsService
{
    /// <summary>
    /// Summary description for clsStatisticsService.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsStatisticsService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获得所有的可选Field,刘颖源,2003-7-9 16:00:22
        [AutoComplete]
        public long m_lngGetAllStatisticSelectedField(string p_strStatisticID, out clsStatisticSelectedFieldValue[] p_objStatisticSelectedFieldValue)
        {

            #region 获得所有表StatisticSelectedField,Service层,刘颖源,2003-7-9 15:55:18
            string strSQL = "select field_id, statistic_id, fieldname, fielddesc from StatisticSelectedField where Statistic_ID=?";
            p_objStatisticSelectedFieldValue = null;
            DataTable objDataTableResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objTabService.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strStatisticID;

            long lngRes = objTabService.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
            if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
            {
                p_objStatisticSelectedFieldValue = new clsStatisticSelectedFieldValue[objDataTableResult.Rows.Count];
                for (int i = 0; i < p_objStatisticSelectedFieldValue.Length; i++)
                {
                    p_objStatisticSelectedFieldValue[i] = new clsStatisticSelectedFieldValue();
                    p_objStatisticSelectedFieldValue[i].m_strField_ID = objDataTableResult.Rows[i]["FIELD_ID"].ToString();
                    p_objStatisticSelectedFieldValue[i].m_strStatistic_ID = objDataTableResult.Rows[i]["STATISTIC_ID"].ToString();
                    p_objStatisticSelectedFieldValue[i].m_strFieldName = objDataTableResult.Rows[i]["FIELDNAME"].ToString();
                    p_objStatisticSelectedFieldValue[i].m_strFieldDesc = objDataTableResult.Rows[i]["FIELDDESC"].ToString();

                }
            }
            #endregion
            //objTabService.Dispose();
            return (lngRes);

        }

        #endregion

        #region 获得所有条件间连接符,刘颖源,2003-7-9 16:03:57
        [AutoComplete]
        public long m_lngGetAllStatisticCCOperator(out clsStatisticCCOperatorValue[] p_objStatisticCCOperatorValue)
        {

            #region 获得所有表StatisticCCOperator,Service层,刘颖源,2003-7-9 16:01:15
            string strSQL = "select operatorid, operatorsymbol, operatordesc from StatisticCCOperator ";
            p_objStatisticCCOperatorValue = null;
            DataTable objDataTableResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            long lngRes = objTabService.DoGetDataTable(strSQL, ref objDataTableResult);
            if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
            {
                p_objStatisticCCOperatorValue = new clsStatisticCCOperatorValue[objDataTableResult.Rows.Count];
                for (int i = 0; i < p_objStatisticCCOperatorValue.Length; i++)
                {
                    p_objStatisticCCOperatorValue[i] = new clsStatisticCCOperatorValue();
                    p_objStatisticCCOperatorValue[i].m_strOperatorID = objDataTableResult.Rows[i]["OPERATORID"].ToString();
                    p_objStatisticCCOperatorValue[i].m_strOperatorSymbol = objDataTableResult.Rows[i]["OPERATORSYMBOL"].ToString();
                    p_objStatisticCCOperatorValue[i].m_strOperatorDesc = objDataTableResult.Rows[i]["OPERATORDESC"].ToString();

                }
            }
            #endregion
            //objTabService.Dispose();
            return (lngRes);
        }


        #endregion

        #region 获得该统计的可控制条件,刘颖源,2003-7-9 16:15:32
        [AutoComplete]
        public long m_lngGetStatisticCondictionOptionValue(string p_strStatisticID, out clsStatisticCondictionOptionValue[] p_objStatisticCondictionOptionValue)
        {

            #region 获得所有表StatisticCondictionOption,Service层,刘颖源,2003-7-9 16:13:24
            string strSQL = "select optionid, statistic_id, optionfieldname, optiondesc from statisticcondictionoption where statistic_id=?";
            p_objStatisticCondictionOptionValue = null;
            DataTable objDataTableResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objTabService.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strStatisticID;

            long lngRes = objTabService.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
            if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
            {
                p_objStatisticCondictionOptionValue = new clsStatisticCondictionOptionValue[objDataTableResult.Rows.Count];
                for (int i = 0; i < p_objStatisticCondictionOptionValue.Length; i++)
                {
                    p_objStatisticCondictionOptionValue[i] = new clsStatisticCondictionOptionValue();
                    p_objStatisticCondictionOptionValue[i].m_strOptionID = objDataTableResult.Rows[i]["OPTIONID"].ToString();
                    p_objStatisticCondictionOptionValue[i].m_strStatistic_ID = objDataTableResult.Rows[i]["STATISTIC_ID"].ToString();
                    p_objStatisticCondictionOptionValue[i].m_strOptionFieldName = objDataTableResult.Rows[i]["OPTIONFIELDNAME"].ToString();
                    p_objStatisticCondictionOptionValue[i].m_strOptionDesc = objDataTableResult.Rows[i]["OPTIONDESC"].ToString();

                }
            }
            #endregion
            //objTabService.Dispose();
            return (lngRes);

        }

        #endregion

        #region 获得条件符,刘颖源,2003-7-9 16:18:47
        [AutoComplete]
        public long lngGetStatisticConditionOperatorValue(out clsStatisticConditionOperatorValue[] p_objStatisticConditionOperatorValue)
        {

            #region 获得所有表StatisticConditionOperator,Service层,刘颖源,2003-7-9 16:18:37
            string strSQL = "select operatorid, operatorsymbol, operatordesc from statisticconditionoperator ";
            p_objStatisticConditionOperatorValue = null;
            DataTable objDataTableResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            long lngRes = objTabService.DoGetDataTable(strSQL, ref objDataTableResult);
            if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
            {
                p_objStatisticConditionOperatorValue = new clsStatisticConditionOperatorValue[objDataTableResult.Rows.Count];
                for (int i = 0; i < p_objStatisticConditionOperatorValue.Length; i++)
                {
                    p_objStatisticConditionOperatorValue[i] = new clsStatisticConditionOperatorValue();
                    p_objStatisticConditionOperatorValue[i].m_strOperatorID = objDataTableResult.Rows[i]["OPERATORID"].ToString();
                    p_objStatisticConditionOperatorValue[i].m_strOperatorSymbol = objDataTableResult.Rows[i]["OPERATORSYMBOL"].ToString();
                    p_objStatisticConditionOperatorValue[i].m_strOperatorDesc = objDataTableResult.Rows[i]["OPERATORDESC"].ToString();

                }
            }
            #endregion
            //objTabService.Dispose();
            return (lngRes);

        }
        #endregion

        #region 获得所有的查询统计定义,刘颖源,2003-7-10 9:59:12
        [AutoComplete]
        public long m_lngGetAllStatisticDefinition(out clsStatisticDefinitionValue[] p_objStatisticDefinitionValue)
        {
            #region 获得所有表StatisticDefinition,Service层,刘颖源,2003-7-10 9:58:34
            string strSQL = @"select statistic_id,
       statistictype,
       statisticflag,
       statisticsqlcontent,
       statisticdesc,
       statisticdisplayformat,
       openclassname,
       openclassmethod,
       openclassparameters from statisticdefinition ";
            p_objStatisticDefinitionValue = null;
            DataTable objDataTableResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            long lngRes = objTabService.DoGetDataTable(strSQL, ref objDataTableResult);
            if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
            {
                p_objStatisticDefinitionValue = new clsStatisticDefinitionValue[objDataTableResult.Rows.Count];
                for (int i = 0; i < p_objStatisticDefinitionValue.Length; i++)
                {
                    p_objStatisticDefinitionValue[i] = new clsStatisticDefinitionValue();
                    p_objStatisticDefinitionValue[i].m_strStatistic_ID = objDataTableResult.Rows[i]["STATISTIC_ID"].ToString();
                    p_objStatisticDefinitionValue[i].m_strStatisticType = objDataTableResult.Rows[i]["STATISTICTYPE"].ToString();
                    p_objStatisticDefinitionValue[i].m_strStatisticFlag = objDataTableResult.Rows[i]["STATISTICFLAG"].ToString();
                    p_objStatisticDefinitionValue[i].m_strStatisticSQLContent = objDataTableResult.Rows[i]["STATISTICSQLCONTENT"].ToString();
                    p_objStatisticDefinitionValue[i].m_strStatisticDesc = objDataTableResult.Rows[i]["STATISTICDESC"].ToString();
                    p_objStatisticDefinitionValue[i].m_strStatisticDisplayFormat = objDataTableResult.Rows[i]["STATISTICDISPLAYFORMAT"].ToString();
                    p_objStatisticDefinitionValue[i].m_strOpenClassName = objDataTableResult.Rows[i]["OPENCLASSNAME"].ToString();
                    p_objStatisticDefinitionValue[i].m_strOpenClassMethod = objDataTableResult.Rows[i]["OPENCLASSMETHOD"].ToString();
                    p_objStatisticDefinitionValue[i].m_strOpenClassParameters = objDataTableResult.Rows[i]["OPENCLASSPARAMETERS"].ToString();

                }
            }
            #endregion
            //objTabService.Dispose();
            return (lngRes);
        }

        #endregion

        #region 获得该查询统计定义下所有的查询方式,刘颖源,2003-7-10 10:01:07
        [AutoComplete]
        public long m_lngGetStatisticQueryMode(string p_strStatisticID, out clsStatisticQueryModeValue[] p_objStatisticQueryModeValue)
        {

            #region 获得所有表StatisticQueryMode,Service层,刘颖源,2003-7-10 10:01:00
            string strSQL = "select statistic_id, queryid, queryname, xmlcontent, selectedfieldindexes, modedesc from statisticquerymode  where statistic_id=?";
            p_objStatisticQueryModeValue = null;
            DataTable objDataTableResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objTabService.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strStatisticID;

            long lngRes = objTabService.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
            if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
            {
                p_objStatisticQueryModeValue = new clsStatisticQueryModeValue[objDataTableResult.Rows.Count];
                for (int i = 0; i < p_objStatisticQueryModeValue.Length; i++)
                {
                    p_objStatisticQueryModeValue[i] = new clsStatisticQueryModeValue();
                    p_objStatisticQueryModeValue[i].m_strStatistic_ID = objDataTableResult.Rows[i]["STATISTIC_ID"].ToString();
                    p_objStatisticQueryModeValue[i].m_strQueryID = objDataTableResult.Rows[i]["QUERYID"].ToString();
                    p_objStatisticQueryModeValue[i].m_strQueryName = objDataTableResult.Rows[i]["QUERYNAME"].ToString();
                    p_objStatisticQueryModeValue[i].m_strXMLContent = objDataTableResult.Rows[i]["XMLCONTENT"].ToString();
                    p_objStatisticQueryModeValue[i].m_strSelectedFieldIndexes = objDataTableResult.Rows[i]["SELECTEDFIELDINDEXES"].ToString();
                    p_objStatisticQueryModeValue[i].m_strModeDesc = objDataTableResult.Rows[i]["MODEDESC"].ToString();

                }
            }
            #endregion
            //objTabService.Dispose();
            return (lngRes);
        }
        #endregion

        #region 通过SQL获得记录集合,刘颖源,2003-7-11 10:29:15
        [AutoComplete]
        public long m_lngPerformSqlQuery(string p_strSqlStatement, out DataTable dtResult)
        {
            dtResult = null;
            if (p_strSqlStatement == null || p_strSqlStatement == "") return -1;

            //DataTable objDataTableResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            long lngRes = objTabService.DoGetDataTable(p_strSqlStatement, ref dtResult);

            if (lngRes <= 0 || dtResult.Columns.Count <= 0) return -1;
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

        #region 新增一种查询模式,刘颖源,2003-7-11 14:12:32
        [AutoComplete]
        public long m_lngAddNewStatisticMode(clsStatisticQueryModeValue p_objStatisticQueryModeValue, out string p_strQueryID)
        {

            #region 保存表 StatisticQueryMode,刘颖源,2003-7-11 14:04:28
            long lngRes = 0;
            p_strQueryID = null;
            clsHRPTableService objTabService = new clsHRPTableService();
            if (p_objStatisticQueryModeValue != null)
            {
                string strSQL = "insert into statisticquerymode(" +
                    "statistic_id," + "queryid," + "xmlcontent," + "selectedfieldindexes," + "modedesc," + "queryname" +
                    ") values(" +
                    "?," + "?," + "?," + "?," + "?" + "?" +
                    ");";
                string strQueryID = "";
                objTabService.lngGenerateID(7, "QueryID", "StatisticQueryMode", out strQueryID);
                if (strQueryID == null || strQueryID == "") strQueryID = "0000001";
                p_strQueryID = strQueryID;
                #region 防止null,刘颖源,2003-7-11 14:07:27
                p_objStatisticQueryModeValue.m_strStatistic_ID = p_objStatisticQueryModeValue.m_strStatistic_ID == null ? "" : p_objStatisticQueryModeValue.m_strStatistic_ID;
                p_objStatisticQueryModeValue.m_strQueryID = strQueryID;
                p_objStatisticQueryModeValue.m_strQueryName = p_objStatisticQueryModeValue.m_strQueryName == null ? "" : p_objStatisticQueryModeValue.m_strQueryName;
                p_objStatisticQueryModeValue.m_strXMLContent = p_objStatisticQueryModeValue.m_strXMLContent == null ? "" : p_objStatisticQueryModeValue.m_strXMLContent;
                p_objStatisticQueryModeValue.m_strSelectedFieldIndexes = p_objStatisticQueryModeValue.m_strSelectedFieldIndexes == null ? "" : p_objStatisticQueryModeValue.m_strSelectedFieldIndexes;
                p_objStatisticQueryModeValue.m_strModeDesc = p_objStatisticQueryModeValue.m_strModeDesc == null ? "" : p_objStatisticQueryModeValue.m_strModeDesc;
                #endregion


                IDataParameter[] objStatisticQueryModeArr = null;
                objTabService.CreateDatabaseParameter(6, out objStatisticQueryModeArr);
                objStatisticQueryModeArr[0].Value = p_objStatisticQueryModeValue.m_strStatistic_ID;
                objStatisticQueryModeArr[1].Value = p_objStatisticQueryModeValue.m_strQueryID;
                objStatisticQueryModeArr[2].Value = p_objStatisticQueryModeValue.m_strXMLContent;
                objStatisticQueryModeArr[3].Value = p_objStatisticQueryModeValue.m_strSelectedFieldIndexes;
                objStatisticQueryModeArr[4].Value = p_objStatisticQueryModeValue.m_strModeDesc;
                objStatisticQueryModeArr[5].Value = p_objStatisticQueryModeValue.m_strQueryName;

                long lngEff = 0;
                lngRes = objTabService.lngExecuteParameterSQL(strSQL, ref lngEff, objStatisticQueryModeArr);
            }
            #endregion
            //objTabService.Dispose();
            return (lngRes);
        }

        #endregion

        #region 修改查询模式,刘颖源,2003-7-11 14:12:32
        [AutoComplete]
        public long m_lngUpdateStatisticMode(string p_strStasticID, string p_strQueryID, string p_strXmlContent, string p_strSelectedFiledsIndexes, string p_strModeDesc)
        {

            #region 保存表 StatisticQueryMode,刘颖源,2003-7-11 14:04:28
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            if (p_strStasticID != null && p_strQueryID != null && p_strQueryID != "" && p_strStasticID != "")
            {
                string strSQL = "update statisticquerymode" +
                    " set xmlcontent=?,selectedfieldindexes=?,modedesc=?" +
                    " where statistic_id=? and queryid=?";


                IDataParameter[] objStatisticQueryModeArr = null;
                objTabService.CreateDatabaseParameter(5, out objStatisticQueryModeArr);
                objStatisticQueryModeArr[0].Value = p_strXmlContent;
                objStatisticQueryModeArr[1].Value = p_strSelectedFiledsIndexes;
                objStatisticQueryModeArr[2].Value = p_strModeDesc;
                objStatisticQueryModeArr[3].Value = p_strStasticID;
                objStatisticQueryModeArr[4].Value = p_strQueryID;

                long lngEff = 0;
                lngRes = objTabService.lngExecuteParameterSQL(strSQL, ref lngEff, objStatisticQueryModeArr);
            }
            #endregion
            //objTabService.Dispose();
            return (lngRes);
        }

        #endregion


        //------------------------------------------------------------------------------
    }
}
