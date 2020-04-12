using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections;
using System.Collections.Generic;

namespace com.digitalwave.MedicalExamService
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsMedicalExamService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        [AutoComplete]
        public string strGetMedicalExam_ID()
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMedicalExamService","strGetMedicalExam_ID");
            //if(lngCheckRes <= 0)
            return null;

            string strOutput = "";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.lngGenerateID(20, "MedicalExam_ID", "MedicalExamInHospital_Target", out strOutput);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return (strOutput);
        }

        [AutoComplete]
        public long lngSaveMedicalExam(clsMedicalExamInHospital_TargetValue p_objMedicalExamInHospital_TargetValue, List<clsMedicalExamMainRecordValue> p_objMedicalExamMainRecord, List<clsMedicalExamDetailRecordValue> p_objMedicalDetalRecord, ref long lngEff)
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMedicalExamService","lngSaveMedicalExam");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                #region 保存表 MedicalExamInHospital_Target,刘颖源,2003-6-3 10:28:49
                //注意逗号,刘颖源,2003-6-3 10:28:49
                if (p_objMedicalExamInHospital_TargetValue != null)
                {
                    string strSQL = @"insert into MedicalExamInHospital_Target(InPatientID,InPatientDate,OpenDate,ItemID,ModifyDate,MedicalExam_ID) 
values(?,?,?,?,?,?)";


                    IDataParameter[] objDPArr = null;// new IDataParameter[6];
                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = p_objMedicalExamInHospital_TargetValue.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_objMedicalExamInHospital_TargetValue.m_strInPatientDate);
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_objMedicalExamInHospital_TargetValue.m_strOpenDate);
                    objDPArr[3].Value = p_objMedicalExamInHospital_TargetValue.m_strItemID;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = DateTime.Parse(p_objMedicalExamInHospital_TargetValue.m_strModifyDate);
                    objDPArr[5].Value = p_objMedicalExamInHospital_TargetValue.m_strMedicalExam_ID;


                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
                #endregion

                #region 保存表 MedicalExamMainRecord,刘颖源,2003-5-22 13:11:49
                if (p_objMedicalExamMainRecord != null && p_objMedicalExamMainRecord.Count > 0)
                {
                    for (int i = 0; i < p_objMedicalExamMainRecord.Count; i++)
                    {

                        //注意逗号,刘颖源,2003-5-22 13:11:49
                        clsMedicalExamMainRecordValue objMedicalExamMainRecordValue =  p_objMedicalExamMainRecord[i];
                        if (objMedicalExamMainRecordValue != null)
                        {
                            string strSQL = @"insert into medicalexammainrecord
  (medicalexam_id,
   option_id,
   activity_date,
   element_id,
   category_id,
   option_type,
   selected_option_index,
   selected_optionvalue_text,
   selected_option_text)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?)";

                            IDataParameter[] objDPArr = null;//new IDataParameter[9];
                            objHRPServ.CreateDatabaseParameter(9, out objDPArr);
                            objDPArr[0].Value = objMedicalExamMainRecordValue.m_strMedicalExam_ID;
                            objDPArr[1].Value = objMedicalExamMainRecordValue.m_strOption_ID;
                            objDPArr[2].DbType = DbType.DateTime;
                            objDPArr[2].Value = DateTime.Parse(objMedicalExamMainRecordValue.m_strActivity_Date);
                            objDPArr[3].Value = objMedicalExamMainRecordValue.m_strElement_ID;
                            objDPArr[4].Value = objMedicalExamMainRecordValue.m_strCategory_ID;
                            objDPArr[5].Value = objMedicalExamMainRecordValue.m_strOption_Type;
                            objDPArr[6].Value = objMedicalExamMainRecordValue.m_strSelected_Option_Index;
                            objDPArr[7].Value = objMedicalExamMainRecordValue.m_strSelected_OptionValue_Text;
                            objDPArr[8].Value = objMedicalExamMainRecordValue.m_strSelected_Option_Text;


                            lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                        }
                    }

                }
                #endregion

                #region 保存表 MedicalExamDetailRecord,刘颖源,2003-5-22 13:23:24
                if (p_objMedicalDetalRecord != null && p_objMedicalDetalRecord.Count > 0)
                {
                    for (int i = 0; i < p_objMedicalDetalRecord.Count; i++)
                    {
                        //注意逗号,刘颖源,2003-5-22 13:23:24
                        clsMedicalExamDetailRecordValue objMedicalExamDetailRecordValue = p_objMedicalDetalRecord[i];
                        if (objMedicalExamDetailRecordValue != null)
                        {
                            string strSQL = @"insert into medicalexamdetailrecord
  (detailitem_id,
   medicalexam_id,
   option_id,
   activity_date,
   element_id,
   category_id,
   selected_option_indexes,
   selected_optionvalue_text)
values
  (?, ?, ?, ?, ?, ?, ?, ?)";

                            IDataParameter[] objDPArr = null;//new IDataParameter[8];
                            objHRPServ.CreateDatabaseParameter(8, out objDPArr);
                            objDPArr[0].Value = objMedicalExamDetailRecordValue.m_strDetailItem_ID;
                            objDPArr[1].Value = objMedicalExamDetailRecordValue.m_strMedicalExam_ID;
                            objDPArr[2].Value = objMedicalExamDetailRecordValue.m_strOption_ID;
                            objDPArr[3].DbType = DbType.DateTime;
                            objDPArr[3].Value = DateTime.Parse(objMedicalExamDetailRecordValue.m_strActivity_Date);
                            objDPArr[4].Value = objMedicalExamDetailRecordValue.m_strElement_ID;
                            objDPArr[5].Value = objMedicalExamDetailRecordValue.m_strCategory_ID;
                            objDPArr[6].Value = objMedicalExamDetailRecordValue.m_strSelected_Option_Indexes;
                            objDPArr[7].Value = objMedicalExamDetailRecordValue.m_strSelected_OptionValue_Text;


                            lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                        }

                    }
                }
                #endregion
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
            return (1);
        }

        [AutoComplete]
        public long lngLoadMedicalExamOptions(string p_strMedicalExam_ID, out clsMedicalExamMainRecordValue[] p_objMedicalExamMainRecordValue, out clsMedicalExamDetailRecordValue[] p_objMedicalExamDetailRecordValue)
        {
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            p_objMedicalExamMainRecordValue = null;
            p_objMedicalExamDetailRecordValue = null;

            if (string.IsNullOrEmpty(p_strMedicalExam_ID))
                return -1;
            try
            {

                #region 获得所有表MedicalExamMainRecord,Service层,刘颖源,2003-5-23 16:34:44
                string strSQL = @"select medicalexam_id,
       option_id,
       activity_date,
       element_id,
       category_id,
       option_type,
       selected_option_index,
       selected_optionvalue_text,
       selected_option_text
  from medicalexammainrecord
 where medicalexam_id = ''
   and activity_date = (select max(activity_date)
                          from medicalexammainrecord
                         where medicalexam_id = '')";

                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMedicalExamService","lngLoadMedicalExamOptions");
                //if(lngCheckRes <= 0)
                //return lngCheckRes;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);


                objDPArr[0].Value = p_strMedicalExam_ID == null ? "" : p_strMedicalExam_ID;
                objDPArr[1].Value = p_strMedicalExam_ID == null ? "" : p_strMedicalExam_ID;

                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objMedicalExamMainRecordValue = new clsMedicalExamMainRecordValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objMedicalExamMainRecordValue.Length; i++)
                    {
                        p_objMedicalExamMainRecordValue[i] = new clsMedicalExamMainRecordValue();
                        p_objMedicalExamMainRecordValue[i].m_strMedicalExam_ID = objDataTableResult.Rows[i]["MEDICALEXAM_ID"].ToString();
                        p_objMedicalExamMainRecordValue[i].m_strOption_ID = objDataTableResult.Rows[i]["OPTION_ID"].ToString();
                        p_objMedicalExamMainRecordValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objMedicalExamMainRecordValue[i].m_strElement_ID = objDataTableResult.Rows[i]["ELEMENT_ID"].ToString();
                        p_objMedicalExamMainRecordValue[i].m_strCategory_ID = objDataTableResult.Rows[i]["CATEGORY_ID"].ToString();
                        p_objMedicalExamMainRecordValue[i].m_strOption_Type = objDataTableResult.Rows[i]["OPTION_TYPE"].ToString();
                        p_objMedicalExamMainRecordValue[i].m_strSelected_Option_Index = objDataTableResult.Rows[i]["SELECTED_OPTION_INDEX"].ToString();
                        p_objMedicalExamMainRecordValue[i].m_strSelected_OptionValue_Text = objDataTableResult.Rows[i]["SELECTED_OPTIONVALUE_TEXT"].ToString();
                        p_objMedicalExamMainRecordValue[i].m_strSelected_Option_Text = objDataTableResult.Rows[i]["SELECTED_OPTION_TEXT"].ToString();

                    }
                }
                else
                {
                    return (lngRes);
                }
                #endregion

                #region 获得所有表MedicalExamDetailRecord,Service层,刘颖源,2003-5-23 16:46:50
                strSQL = @"select detailitem_id,
       medicalexam_id,
       option_id,
       activity_date,
       element_id,
       category_id,
       selected_option_indexes,
       selected_optionvalue_text
  from medicalexamdetailrecord
 where medicalexam_id = ?
   and activity_date = (select max(activity_date)
                          from medicalexamdetailrecord
                         where medicalexam_id = ?)";
                objDataTableResult = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strMedicalExam_ID;
                objDPArr[1].Value = p_strMedicalExam_ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objMedicalExamDetailRecordValue = new clsMedicalExamDetailRecordValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objMedicalExamDetailRecordValue.Length; i++)
                    {
                        p_objMedicalExamDetailRecordValue[i] = new clsMedicalExamDetailRecordValue();
                        p_objMedicalExamDetailRecordValue[i].m_strDetailItem_ID = objDataTableResult.Rows[i]["DETAILITEM_ID"].ToString();
                        p_objMedicalExamDetailRecordValue[i].m_strMedicalExam_ID = objDataTableResult.Rows[i]["MEDICALEXAM_ID"].ToString();
                        p_objMedicalExamDetailRecordValue[i].m_strOption_ID = objDataTableResult.Rows[i]["OPTION_ID"].ToString();
                        p_objMedicalExamDetailRecordValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objMedicalExamDetailRecordValue[i].m_strElement_ID = objDataTableResult.Rows[i]["ELEMENT_ID"].ToString();
                        p_objMedicalExamDetailRecordValue[i].m_strCategory_ID = objDataTableResult.Rows[i]["CATEGORY_ID"].ToString();
                        p_objMedicalExamDetailRecordValue[i].m_strSelected_Option_Indexes = objDataTableResult.Rows[i]["SELECTED_OPTION_INDEXES"].ToString();
                        p_objMedicalExamDetailRecordValue[i].m_strSelected_OptionValue_Text = objDataTableResult.Rows[i]["SELECTED_OPTIONVALUE_TEXT"].ToString();

                    }
                }
                #endregion

            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return (lngRes);
        }

        [AutoComplete]
        public long lngGetMedicalExamUnitString(string p_strMedicalExamID, out clsMedicalExamUnitStringValue[] p_objMedicalExamUnitStringValue)
        {
            p_objMedicalExamUnitStringValue = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMedicalExamService","lngGetMedicalExamUnitString");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            #region 获得所有表MedicalExamUnitString,Service层,刘颖源,2003-5-26 14:28:26
            string strSQL = @"select a.element_id,
       a.option_id,
       a.option_type,
       a.selected_option_index,
       b.option_name,
       a.selected_optionvalue_text,
       selected_option_text,
       c.elementname,
       d.selected_optionvalue_text as selected_optionvalue_detailtext
  from medicalexammainrecord a
  left join medicalexamdetailrecord d on (a.medicalexam_id =
                                         d.medicalexam_id and
                                         a.option_id = d.option_id and
                                         a.activity_date = d.activity_date and
                                         a.element_id = d.element_id and
                                         a.category_id = d.category_id)
 inner join medicalexamoptions b on (a.option_id = b.option_id)
 inner join medicalexamelement c on (a.element_id = c.element_id)
 where a.medicalexam_id = ?
   and a.activity_date = (select max(activity_date)
                            from medicalexammainrecord
                           where medicalexam_id = ?)
 order by a.element_id,
          a.option_id,
          a.selected_option_index,
          d.selected_option_indexes";
            p_objMedicalExamUnitStringValue = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strMedicalExamID;
                objDPArr[1].Value = p_strMedicalExamID;

                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objMedicalExamUnitStringValue = new clsMedicalExamUnitStringValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objMedicalExamUnitStringValue.Length; i++)
                    {
                        p_objMedicalExamUnitStringValue[i] = new clsMedicalExamUnitStringValue();
                        p_objMedicalExamUnitStringValue[i].m_strElement_ID = objDataTableResult.Rows[i]["ELEMENT_ID"].ToString();
                        p_objMedicalExamUnitStringValue[i].m_strOption_ID = objDataTableResult.Rows[i]["OPTION_ID"].ToString();
                        p_objMedicalExamUnitStringValue[i].m_strOption_Type = objDataTableResult.Rows[i]["OPTION_TYPE"].ToString();
                        p_objMedicalExamUnitStringValue[i].m_strSelected_Option_Index = objDataTableResult.Rows[i]["SELECTED_OPTION_INDEX"].ToString();
                        p_objMedicalExamUnitStringValue[i].m_strOption_Name = objDataTableResult.Rows[i]["OPTION_NAME"].ToString();
                        p_objMedicalExamUnitStringValue[i].m_strSelected_OptionValue_Text = objDataTableResult.Rows[i]["SELECTED_OPTIONVALUE_TEXT"].ToString();
                        p_objMedicalExamUnitStringValue[i].m_strSelected_Option_Text = objDataTableResult.Rows[i]["SELECTED_OPTION_TEXT"].ToString();
                        p_objMedicalExamUnitStringValue[i].m_strElementName = objDataTableResult.Rows[i]["ELEMENTNAME"].ToString();
                        p_objMedicalExamUnitStringValue[i].m_strSelected_OptionValue_DetailText = objDataTableResult.Rows[i]["SELECTED_OPTIONVALUE_DETAILTEXT"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            #endregion

            return (lngRes);
        }
        [AutoComplete]
        public string strGetInPatientCaseMedicalExam_ID(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate)
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMedicalExamService","strGetInPatientCaseMedicalExam_ID");
            //if(lngCheckRes <= 0)
            return null;

            #region 获得所有表MedicalExamInHospital_Target,Service层,刘颖源,2003-6-3 10:32:26
            if (p_strInPatientID == null || p_strInPatientID == "" ||
                p_strInPatientDate == null || p_strInPatientDate == "" ||
                p_strOpenDate == null || p_strOpenDate == "")
                return ("");

            string strSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       itemid,
       modifydate,
       medicalexam_id
  from medicalexaminhospital_target
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and modifydate = (select max(modifydate)
                       from medicalexaminhospital_target
                      where inpatientid = ?
                        and inpatientdate = ?)";
            string strReturn = "";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);

                DataTable objDataTableResult = new DataTable();
                long lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    strReturn = objDataTableResult.Rows[0]["MEDICALEXAM_ID"].ToString();
                }
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            #endregion
            return (strReturn);
        }


        [AutoComplete]
        public long lngGetMedicalElementAndOptions(out clsMedicalExamElementValue[] p_objMedicalExamElementValue, out clsMedicalExamOptionsValue[] p_objMedicalExamOptionsValue)
        {
            p_objMedicalExamElementValue = null;
            p_objMedicalExamOptionsValue = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMedicalExamService","lngGetMedicalElementAndOptions");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {

                #region 获得所有表MedicalExamElement,Service层,刘颖源,2003-6-3 14:21:57
                string strSQL = "select element_id, elementname from medicalexamelement ";
                p_objMedicalExamElementValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objMedicalExamElementValue = new clsMedicalExamElementValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objMedicalExamElementValue.Length; i++)
                    {
                        p_objMedicalExamElementValue[i] = new clsMedicalExamElementValue();
                        p_objMedicalExamElementValue[i].m_strElement_ID = objDataTableResult.Rows[i]["ELEMENT_ID"].ToString();
                        p_objMedicalExamElementValue[i].m_strElementName = objDataTableResult.Rows[i]["ELEMENTNAME"].ToString();

                    }
                }
                #endregion

                #region 获得所有表MedicalExamOptions,Service层,刘颖源,2003-6-3 14:21:00
                strSQL = "select option_id, option_name, option_type from medicalexamoptions ";
                p_objMedicalExamOptionsValue = null;
                objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objMedicalExamOptionsValue = new clsMedicalExamOptionsValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objMedicalExamOptionsValue.Length; i++)
                    {
                        p_objMedicalExamOptionsValue[i] = new clsMedicalExamOptionsValue();
                        p_objMedicalExamOptionsValue[i].m_strOption_ID = objDataTableResult.Rows[i]["OPTION_ID"].ToString();
                        p_objMedicalExamOptionsValue[i].m_strOption_Name = objDataTableResult.Rows[i]["OPTION_NAME"].ToString();
                        p_objMedicalExamOptionsValue[i].m_strOption_Type = objDataTableResult.Rows[i]["OPTION_TYPE"].ToString();

                    }
                }
                #endregion
            }
            finally
            {
                //objHRPServ.Dispose();
            }

            return (lngRes);
        }
        [AutoComplete]
        public clsMedicalExamElementOptionUnionValue[] lngGetAllExamElementOptionUnion()
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMedicalExamService","lngGetAllExamElementOptionUnion");
            //if(lngCheckRes <= 0)
            return null;

            #region 获得所有表MedicalExamElementOptionUnion,Service层,刘颖源,2003-6-3 16:58:33
            string strSQL = @"select element_id as oe_id,
       elementname as oe_name,
       -1 as oe_type,
       '0' as oe_type2
  from medicalexamelement
union
select option_id, option_name, option_type, '1'
  from medicalexamoptions
 order by oe_type2, oe_id";
            clsMedicalExamElementOptionUnionValue[] p_objMedicalExamElementOptionUnionValue = null;
            DataTable objDataTableResult = new DataTable();
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                lngRes = new clsHRPTableService().DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objMedicalExamElementOptionUnionValue = new clsMedicalExamElementOptionUnionValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objMedicalExamElementOptionUnionValue.Length; i++)
                    {
                        p_objMedicalExamElementOptionUnionValue[i] = new clsMedicalExamElementOptionUnionValue();
                        p_objMedicalExamElementOptionUnionValue[i].m_strOE_ID = objDataTableResult.Rows[i]["OE_ID"].ToString();
                        p_objMedicalExamElementOptionUnionValue[i].m_strOE_NAME = objDataTableResult.Rows[i]["OE_NAME"].ToString();
                        p_objMedicalExamElementOptionUnionValue[i].m_strOE_TYPE = objDataTableResult.Rows[i]["OE_TYPE"].ToString();
                        p_objMedicalExamElementOptionUnionValue[i].m_strOE_TYPE2 = objDataTableResult.Rows[i]["OE_TYPE2"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return (p_objMedicalExamElementOptionUnionValue);
            #endregion
        }
    }
}
