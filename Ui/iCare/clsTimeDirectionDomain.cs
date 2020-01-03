using System;
using weCare.Core.Entity;
namespace iCare
{
    /// <summary>
    /// Summary description for clsTimeDirectionDomain.
    /// </summary>
    public class clsTimeDirectionDomain
    {
        public clsTimeDirectionDomain()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        #region �õ����е�TimeDirectionTable,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
        public clsTimeDirectionTableValue[] lngGetAllTimeDirectionTable()
        {
            clsTimeDirectionTableValue[] objTimeDirectionTableArr = null;

            //clsTimeDirectionService m_objServ =
            //    (clsTimeDirectionService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTimeDirectionService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllTimeDirectionTable(out objTimeDirectionTableArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTimeDirectionTableArr);
        }
        #endregion

        #region �õ����е�TimeDirectionField,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
        public clsTimeDirectionFieldValue[] lngGetAllTimeDirectionField(string p_strTableID)
        {
            clsTimeDirectionFieldValue[] objTimeDirectionFieldArr = null;

            //clsTimeDirectionService m_objServ =
            //    (clsTimeDirectionService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTimeDirectionService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllTimeDirectionField(p_strTableID, out objTimeDirectionFieldArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTimeDirectionFieldArr);
        }
        #endregion

        #region ����������,��ӱԴ,2003-6-6 11:26:42
        public long lngGenerateTimeDirection(string p_strTableName, clsTimeDirectionFieldValue[] p_objFields,
            string p_strDateTimeFiledName, string p_strBeginDate, string p_strEndDate,
            string p_strSpecailCondition, string[] p_strCondictionValues,
            string p_strSecialOrder,
            out clsTDParameterExplainValue[] p_objTDParameterExplain, out clsTDParameterDetailValue[] p_objTDParameterDetail, out DateTime p_objMinStartDate)
        {
            p_objTDParameterExplain = null;
            p_objTDParameterDetail = null;

            //clsTimeDirectionService m_objServ =
            //    (clsTimeDirectionService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTimeDirectionService));

            long lngRes = 0;
            try
            {
                p_objMinStartDate = DateTime.MinValue;
                string strStandardSQL = "Select  <DATETIME_FIELD_NAME>,<DATA_FIELD_NAMES> From <TABLE_NAME> " +
                                    " where (<DATETIME_FIELD_NAME> Between <BEGIN_DATE> And <END_DATE>) <SPECIAL_CONDITION> " +
                                    " Order By <DATETIME_FIELD_NAME><SPECIAL_ORDER>";
                if (p_objFields == null || p_objFields.Length <= 0) return (-1);
                string strFields = "";
                for (int i = 0; i < p_objFields.Length; i++)
                    strFields += (p_objFields[i].m_strF_DataFieldName + ",");
                if (strFields.Length > 0)
                    strFields = strFields.Substring(0, strFields.Length - 1);
                if (p_strBeginDate == null || p_strBeginDate == "")
                    p_strBeginDate = "TO_DATE('1900-01-01 00:00:00','yyyy-mm-dd hh24:mi:ss')";
                else
                    p_strBeginDate = "TO_DATE('" + p_strBeginDate + "','yyyy-mm-dd hh24:mi:ss')";
                if (p_strEndDate == null || p_strEndDate == "")
                    p_strEndDate = "TO_DATE('2099-12-31 23:59:59','yyyy-mm-dd hh24:mi:ss')";
                else
                    p_strEndDate = "TO_DATE('" + p_strEndDate + "','yyyy-mm-dd hh24:mi:ss')";
                string strSpecialCondiction = p_strSpecailCondition;
                if (strSpecialCondiction == null) strSpecialCondiction = "";
                strSpecialCondiction = strSpecialCondiction.Trim();
                strSpecialCondiction = strSpecialCondiction.Replace("<TABLE_NAME>", p_strTableName);
                strSpecialCondiction = strSpecialCondiction.Replace("<DATETIME_FIELD_NAME>", p_strDateTimeFiledName);
                if (strSpecialCondiction.Length > 5 && strSpecialCondiction.Substring(0, 5) != "where")			//������where 
                {
                    if (p_strCondictionValues != null && p_strCondictionValues.Length > 0)
                    {
                        for (int i = 0; i < p_strCondictionValues.Length; i++)
                        {
                            string strDateFormat = null;
                            string strTemp = "CONDICTION" + i.ToString("00");
                            if (p_strCondictionValues[i] != null && p_strCondictionValues[i] != "")
                            {
                                if (m_blnGetDateFormat(p_strCondictionValues[i], out strDateFormat))
                                    strSpecialCondiction = strSpecialCondiction.Replace("'" + strTemp + "'", strDateFormat);
                                else
                                    strSpecialCondiction = strSpecialCondiction.Replace(strTemp, p_strCondictionValues[i]);
                            }
                        }
                    }
                    if (strSpecialCondiction != null && strSpecialCondiction.Trim() != "")
                    {
                        strSpecialCondiction = strSpecialCondiction.Trim();
                        if (strSpecialCondiction.Substring(0, 3).ToLower() != "and")
                            strSpecialCondiction = " and " + strSpecialCondiction;
                    }
                }

                string strSpecialOrder = p_strSecialOrder;
                if (strSpecialOrder != null && strSpecialOrder.Trim() != "")
                {
                    strSpecialOrder = strSpecialOrder.Trim();
                    if (strSpecialOrder.Substring(0, 1) != ",")
                        strSpecialOrder = "," + strSpecialOrder;
                }

                strStandardSQL = strStandardSQL.Replace("<TABLE_NAME>", p_strTableName);
                strStandardSQL = strStandardSQL.Replace("<DATETIME_FIELD_NAME>", p_strDateTimeFiledName);
                strStandardSQL = strStandardSQL.Replace("<DATA_FIELD_NAMES>", strFields);
                strStandardSQL = strStandardSQL.Replace("<BEGIN_DATE>", p_strBeginDate);
                strStandardSQL = strStandardSQL.Replace("<END_DATE>", p_strEndDate);
                strStandardSQL = strStandardSQL.Replace("<SPECIAL_CONDITION>", strSpecialCondiction);
                strStandardSQL = strStandardSQL.Replace("<SPECIAL_ORDER>", strSpecialOrder);
                string[] strFieldNames = null;
                System.Data.DataTable strRowValue = null;
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllAvailableParamenters(strStandardSQL, out strFieldNames, out strRowValue);
                p_objTDParameterExplain = new clsTDParameterExplainValue[p_objFields.Length];
                for (int i = 0; i < p_objFields.Length; i++)
                {
                    p_objTDParameterExplain[i] = new clsTDParameterExplainValue();
                    p_objTDParameterExplain[i].m_strP_Desc = p_objFields[i].m_strF_FieldDesc;
                    p_objTDParameterExplain[i].m_strP_ID = p_objFields[i].m_strT_ID + p_objFields[i].m_strF_ID;
                    //Ѱ�������Сֵ
                    p_objTDParameterExplain[i].m_strP_MaxValue = "0";
                    p_objTDParameterExplain[i].m_strP_MinValue = "1";
                    clsTimeDirectionMaxMinValueValue[] objMaxMinValue = null;
                    lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetFieldsMaxMinValue(int.Parse(p_objFields[i].m_strF_FieldType), p_strTableName, p_objFields[i].m_strF_DataFieldName, out objMaxMinValue);
                    if (objMaxMinValue != null && objMaxMinValue.Length > 0)
                    {
                        p_objTDParameterExplain[i].m_strP_MaxValue = objMaxMinValue[0].m_strMaxValue;
                        p_objTDParameterExplain[i].m_strP_MinValue = objMaxMinValue[0].m_strMinValue;
                    }

                }
                p_objTDParameterDetail = null;
                if (lngRes > 0 && strFieldNames != null && strFieldNames.Length > 0 && strRowValue != null && strRowValue.Rows.Count > 0)
                {
                    int intRowsNumber = strRowValue.Rows.Count / (p_objFields.Length + 1);		//����һ��ActiveDate,�������¼��������
                    if (intRowsNumber <= 0) return (-1);
                    p_objTDParameterDetail = new clsTDParameterDetailValue[intRowsNumber * p_objFields.Length];		//�����пռ�
                    for (int i = 0; i < intRowsNumber * p_objFields.Length; i++)
                    {
                        int intIndexRow = i / (p_objFields.Length);
                        int intIndexColumn = i % (p_objFields.Length);

                        p_objTDParameterDetail[i] = new clsTDParameterDetailValue();

                        p_objTDParameterDetail[i].m_strP_DateTime = strRowValue.Rows[intIndexRow][0].ToString();
                        p_objMinStartDate = (DateTime.Parse(p_objTDParameterDetail[i].m_strP_DateTime) < p_objMinStartDate) ? DateTime.Parse(p_objTDParameterDetail[i].m_strP_DateTime) : p_objMinStartDate;
                        p_objTDParameterDetail[i].m_strP_Value = strRowValue.Rows[intIndexRow][intIndexColumn + 1].ToString().Trim();
                        p_objTDParameterDetail[i].m_strP_ID = p_objFields[intIndexColumn].m_strT_ID + p_objFields[intIndexColumn].m_strF_ID;
                    }


                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (lngRes);

        }

        #endregion

        #region �����������,��ӱԴ,2003-6-9 14:38:28
        public long lngGenerateTimeDirection1(string p_strTableName, string p_strTableID_Field, clsTimeDirectionFieldValue[] p_objFields,
            string p_strDateTimeFiledName, string p_strBeginDate, string p_strEndDate,
            string p_strSpecailCondition, string[] p_strCondictionValues,
            string p_strSecialOrder,
            out clsTDParameterExplainValue[] p_objTDParameterExplain, out clsTDParameterDetailValue[] p_objTDParameterDetail, out DateTime p_objMinStartDate)
        {
            p_objTDParameterExplain = null;
            p_objTDParameterDetail = null;

            //clsTimeDirectionService m_objServ =
            //    (clsTimeDirectionService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTimeDirectionService));

            long lngRes = 0;
            try
            {
                p_objMinStartDate = DateTime.MinValue;
                string strStandardSQL = "Select  <DATETIME_FIELD_NAME>,<DATA_FIELD_NAMES>,<TARGET_ID_FIELD> as 'Target_ID' From <TABLE_NAME> " +
                    " where (<DATETIME_FIELD_NAME> Between <BEGIN_DATE> And <END_DATE>) <SPECIAL_CONDITION> " +
                    " Order By <DATETIME_FIELD_NAME><SPECIAL_ORDER>";
                if (p_objFields == null || p_objFields.Length <= 0 || p_strTableID_Field == null || p_strTableID_Field == "") return (-1);

                string strFields = p_objFields[0].m_strF_DataFieldName; ;
                if (strFields == "") return (-1);

                if (p_strBeginDate == null || p_strBeginDate == "")
                    p_strBeginDate = "TO_DATE('1900-01-01 00:00:00','yyyy-mm-dd hh24:mi:ss')";
                else
                    p_strBeginDate = "TO_DATE('" + p_strBeginDate + "','yyyy-mm-dd hh24:mi:ss')";
                if (p_strEndDate == null || p_strEndDate == "")
                    p_strEndDate = "TO_DATE('2099-12-31 23:59:59','yyyy-mm-dd hh24:mi:ss')";
                else
                    p_strEndDate = "TO_DATE('" + p_strEndDate + "','yyyy-mm-dd hh24:mi:ss')";
                string strSpecialCondiction = p_strSpecailCondition;
                if (strSpecialCondiction == null) strSpecialCondiction = "";
                strSpecialCondiction = strSpecialCondiction.Trim();
                strSpecialCondiction = strSpecialCondiction.Replace("<TABLE_NAME>", p_strTableName);
                strSpecialCondiction = strSpecialCondiction.Replace("<DATETIME_FIELD_NAME>", p_strDateTimeFiledName);
                if (strSpecialCondiction.Length > 5 && strSpecialCondiction.Substring(0, 5) != "where")			//������where 
                {
                    if (p_strCondictionValues != null && p_strCondictionValues.Length > 0)
                    {
                        for (int i = 0; i < p_strCondictionValues.Length; i++)
                        {
                            string strDateFormat = null;
                            string strTemp = "CONDICTION" + i.ToString("00");
                            if (p_strCondictionValues[i] != null && p_strCondictionValues[i] != "")
                            {
                                if (m_blnGetDateFormat(p_strCondictionValues[i], out strDateFormat))
                                    strSpecialCondiction = strSpecialCondiction.Replace("'" + strTemp + "'", strDateFormat);
                                else
                                    strSpecialCondiction = strSpecialCondiction.Replace(strTemp, p_strCondictionValues[i]);
                            }
                        }
                    }
                    if (strSpecialCondiction != null && strSpecialCondiction.Trim() != "")
                    {
                        strSpecialCondiction = strSpecialCondiction.Trim();
                        if (strSpecialCondiction.Substring(0, 3).ToLower() != "and")
                            strSpecialCondiction = " and " + strSpecialCondiction;
                    }
                }
                string strOrCondiction = "";
                for (int i = 0; i < p_objFields.Length; i++)
                    strOrCondiction += (p_objFields[i].m_strF_FieldCondiction + " or ");
                if (strOrCondiction.Length > 4)
                    strOrCondiction = strOrCondiction.Substring(0, strOrCondiction.Length - 4);

                strSpecialCondiction += " and (" + strOrCondiction + ") ";

                string strSpecialOrder = p_strSecialOrder;
                if (strSpecialOrder != null && strSpecialOrder.Trim() != "")
                {
                    strSpecialOrder = strSpecialOrder.Trim();
                    if (strSpecialOrder.Substring(0, 1) != ",")
                        strSpecialOrder = "," + strSpecialOrder;
                }

                strStandardSQL = strStandardSQL.Replace("<TABLE_NAME>", p_strTableName);
                strStandardSQL = strStandardSQL.Replace("<TARGET_ID_FIELD>", p_strTableID_Field);
                strStandardSQL = strStandardSQL.Replace("<DATETIME_FIELD_NAME>", p_strDateTimeFiledName);
                strStandardSQL = strStandardSQL.Replace("<DATA_FIELD_NAMES>", strFields);
                strStandardSQL = strStandardSQL.Replace("<BEGIN_DATE>", p_strBeginDate);
                strStandardSQL = strStandardSQL.Replace("<END_DATE>", p_strEndDate);
                strStandardSQL = strStandardSQL.Replace("<SPECIAL_CONDITION>", strSpecialCondiction);
                strStandardSQL = strStandardSQL.Replace("<SPECIAL_ORDER>", strSpecialOrder);
                string[] strFieldNames = null;
                System.Data.DataTable strRowValue = null;
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllAvailableParamenters(strStandardSQL, out strFieldNames, out strRowValue);
                p_objTDParameterExplain = new clsTDParameterExplainValue[p_objFields.Length];
                for (int i = 0; i < p_objFields.Length; i++)
                {
                    p_objTDParameterExplain[i] = new clsTDParameterExplainValue();
                    p_objTDParameterExplain[i].m_strP_Desc = p_objFields[i].m_strF_FieldDesc;
                    p_objTDParameterExplain[i].m_strP_ID = p_objFields[i].m_strT_ID + p_objFields[i].m_strF_ID;
                    //Ѱ�������Сֵ
                    string strMaxMinSQL = "Select Max(" + strFields + ") as 'MaxValue', Min(" + strFields + ") as 'MinValue' From " + p_strTableName + " Where " + p_objFields[i].m_strF_FieldCondiction;
                    string[] strMaxMinFields = null;
                    System.Data.DataTable strMaxMinRowValue = null;
                    long lngReturn = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllAvailableParamenters(strMaxMinSQL, out strMaxMinFields, out strMaxMinRowValue);
                    if (lngReturn > 0 && strMaxMinRowValue != null && strMaxMinRowValue.Rows.Count > 0)
                    {
                        p_objTDParameterExplain[i].m_strP_MaxValue = strMaxMinRowValue.Rows[0][0].ToString();
                        p_objTDParameterExplain[i].m_strP_MinValue = strMaxMinRowValue.Rows[0][1].ToString();
                    }
                }
                //����ÿһ��Ѱ�������Сֵ
                p_objMinStartDate = DateTime.MaxValue;

                p_objTDParameterDetail = null;
                if (lngRes > 0 && strFieldNames != null && strFieldNames.Length > 0 && strRowValue != null && strRowValue.Rows.Count > 0)
                {
                    int intRowsNumber = strRowValue.Rows.Count / 3;		//ActiveDate,Value,Targer_ID
                    if (intRowsNumber <= 0) return (-1);
                    p_objTDParameterDetail = new clsTDParameterDetailValue[intRowsNumber];		//�����пռ�
                    for (int i = 0; i < intRowsNumber; i++)
                    {
                        int intIndexRow = i;
                        int intIndexColumn = i % (p_objFields.Length);
                        p_objTDParameterDetail[i] = new clsTDParameterDetailValue();
                        p_objTDParameterDetail[i].m_strP_DateTime = strRowValue.Rows[intIndexRow][0].ToString();
                        p_objMinStartDate = (DateTime.Parse(p_objTDParameterDetail[i].m_strP_DateTime) < p_objMinStartDate) ? DateTime.Parse(p_objTDParameterDetail[i].m_strP_DateTime) : p_objMinStartDate;
                        p_objTDParameterDetail[i].m_strP_Value = strRowValue.Rows[intIndexRow][1].ToString().Trim();
                        p_objTDParameterDetail[i].m_strP_ID = p_objFields[intIndexColumn].m_strT_ID + p_objFields[intIndexColumn].m_strF_ID;
                    }
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (lngRes);

        }
        private bool m_blnGetDateFormat(string p_strValue, out string p_strResult)
        {
            p_strResult = p_strValue;
            try
            {
                p_strResult = "to_date('" + DateTime.Parse(p_strValue).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
            }
            catch { return false; }
            return true;
        }

        #endregion

        #region ������п�ѡ��,��ӱԴ,2003-6-6 11:26:42
        public long lngGetAllAvailableParamenters(string p_strSqlStatement, out string[] p_strFieldNames, out System.Data.DataTable p_strRowValues)
        {
            //clsTimeDirectionService m_objServ =
            //    (clsTimeDirectionService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTimeDirectionService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr02()).Service.lngGetAllAvailableParamenters(p_strSqlStatement, out p_strFieldNames, out p_strRowValues));
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        #endregion
    }
}
