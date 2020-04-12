using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections.Generic;
using System.Collections;

namespace com.digitalwave.TemplateService
{
    /// <summary>
    /// Summary description for clsTemplateService.
    /// Service层代码，完成商业逻辑和数据库操作,刘颖源,2003-5-7 16:15:49
    /// 尽量使用exists而不是in。大大提高性能 modify by tfzhang 2005-11-17 16:42
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsTemplateService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获得模板,刘颖源,2003-5-15 13:12:57
        [AutoComplete]
        public long lngGetTemplate(string p_strID, out clsTemplateValue p_objValue)
        {
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetTemplate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strSQL = @"select template_id, start_date, end_date from template  where template_id = ?";

            DataTable dtbValue = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objValue = new clsTemplateValue();
                    p_objValue.m_strTemplate_ID = dtbValue.Rows[0]["TEMPLATE_ID"].ToString(); ;
                    p_objValue.m_strStart_Date = dtbValue.Rows[0]["START_DATE"].ToString(); ;
                    p_objValue.m_strEnd_Date = dtbValue.Rows[0]["END_DATE"].ToString(); ;
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }


        #endregion

        #region 获得套装模板,刘颖源,2003-5-15 13:13:24
        [AutoComplete]
        public long lngGetTemplateSet(string p_strID, out clsTempate_SetValue p_objValue)
        {
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetTemplateSet");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strSQL = @"select set_id, start_date, end_date from tempate_set  where set_id = ?";

            DataTable dtbValue = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objValue = new clsTempate_SetValue();
                    p_objValue.m_strSet_ID = dtbValue.Rows[0]["SET_ID"].ToString(); ;
                    p_objValue.m_strStart_Date = dtbValue.Rows[0]["START_DATE"].ToString(); ;
                    p_objValue.m_strEnd_Date = dtbValue.Rows[0]["END_DATE"].ToString(); ;
                }

            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        }
        #endregion

        #region 获得模板ID,刘颖源,2003-5-8 10:39:56
        //在此编写同类功能函数体
        [AutoComplete]
        public string strGetTemplateID()
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","strGetTemplateID");
            //if(lngCheckRes <= 0)
            //return null;

            string strOutResult;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                objHRPServ.lngGenerateID(10, "Template_ID", "Template", out strOutResult);
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return (strOutResult);
        }
        #endregion

        #region 获得套装模板ID,刘颖源,2003-5-9 11:05:50
        //在此编写同类功能函数体
        [AutoComplete]
        public string strGetTemplateSetID()
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","strGetTemplateSetID");
            //if(lngCheckRes <= 0)
            //return null;

            string strOutResult;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                objHRPServ.lngGenerateID(10, "Set_ID", "Tempate_Set", out strOutResult);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return (strOutResult);
        }
        #endregion

        #region 保存模板,刘颖源,2003-5-9 11:05:50
        [AutoComplete]
        public long lngSaveTemplate(bool p_blnIsNewTemplate,
            clsTemplateValue p_objValue, clsTemplate_DetailValue p_objTemplate_Detail,
            clsTemplate_KeywordValue[] p_objTemplate_Keyword, clsTemplate_TargetValue[] p_objTemplate_Target,
            clsTemplate_Dept_VisibilityValue[] p_objTemplate_Dept_Visiblity, ref long lngEff, out string p_strTemplateID)
        {
            p_strTemplateID = "";
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngSaveTemplate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                //注意逗号,刘颖源,2003-5-7 16:15:49
                #region  保存表Template,刘颖源,2003-5-8 10:00:55
                if (p_objValue != null)
                {
                    string strSQL = "";
                    if (p_blnIsNewTemplate)
                    {
                        p_strTemplateID = strGetTemplateID();
                        if (p_strTemplateID == null || p_strTemplateID == "")
                            p_strTemplateID = "00001";

                        strSQL = @"insert into template(template_id, start_date, end_date) 
values(?, ?, ? )";


                        IDataParameter[] objDPArr = new IDataParameter[3];
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].Value = p_strTemplateID.Trim();
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objValue.m_strStart_Date);
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_objValue.m_strEnd_Date);


                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                    else
                    {
                        strSQL = "update template set " +
                            "start_date=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objValue.m_strStart_Date) + "," +
                            "end_date=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objValue.m_strEnd_Date) + " where template_id='" + p_objValue.m_strTemplate_ID + "'";
                        p_strTemplateID = p_objValue.m_strTemplate_ID;
                        lngRes = objHRPServ.DoExcute(strSQL);
                    }
                }
                #endregion

                #region 保存表 Template_Detail,刘颖源,2003-5-8 10:08:00
                //注意逗号,刘颖源,2003-5-8 10:08:00
                if (p_objTemplate_Detail != null)
                {
                    string strSQL = @"insert into template_detail(template_id, activity_date, template_name, content, employeeid, visibility_level )
                    values(?, ?, ?, ?, ?, ? )";

                    IDataParameter[] objDPArr = null;//new IDataParameter[6];
                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = p_strTemplateID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_objTemplate_Detail.m_strActivity_Date);
                    objDPArr[2].Value = p_objTemplate_Detail.m_strTemplate_Name;
                    objDPArr[3].Value = p_objTemplate_Detail.m_strContent;
                    objDPArr[4].Value = p_objTemplate_Detail.m_strEmployeeID;
                    objDPArr[5].Value = p_objTemplate_Detail.m_strVisibility_Level;


                    objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
                #endregion

                #region 保存表 Template_Keyword,刘颖源,2003-5-8 10:26:54
                //注意逗号,刘颖源,2003-5-8 10:26:54
                if (p_objTemplate_Keyword != null)
                {
                    for (int i = 0; i < p_objTemplate_Keyword.Length; i++)
                    {
                        string strSQL = @"insert into template_keyword(template_id, activity_date, keyword, keyword_py, keyword_type )
values(?, ?, ?, ?, ?)";

                        IDataParameter[] objDPArr = null;//new IDataParameter[5];
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                        objDPArr[0].Value = p_strTemplateID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objTemplate_Keyword[i].m_strActivity_Date);
                        objDPArr[2].Value = p_objTemplate_Keyword[i].m_strKeyword.Replace("'", "き");
                        objDPArr[3].Value = p_objTemplate_Keyword[i].m_strKeyword_PY;
                        objDPArr[4].Value = p_objTemplate_Keyword[i].m_strKeyword_Type;


                        objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                #endregion

                #region 保存表 Template_Target,刘颖源,2003-5-8 10:30:02
                //注意逗号,刘颖源,2003-5-8 10:30:02
                if (p_objTemplate_Target != null)
                {
                    for (int i = 0; i < p_objTemplate_Target.Length; i++)
                    {
                        string strSQL = @"insert into template_target( control_id, form_id, template_id, activity_date, control_tabindex)
values( ?, ?, ?, ?, ?)";

                        IDataParameter[] objDPArr = null;//new IDataParameter[4];
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                        objDPArr[0].Value = p_objTemplate_Target[i].m_strControl_ID;
                        objDPArr[1].Value = p_objTemplate_Target[i].m_strForm_ID;
                        objDPArr[2].Value = p_strTemplateID;
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(p_objTemplate_Target[i].m_strActivity_Date);

                        if (p_objTemplate_Target[i].m_strControl_TabIndex == null || p_objTemplate_Target[i].m_strControl_TabIndex == string.Empty)
                        {
                            objDPArr[4].Value = 0;

                        }
                        else
                        {
                            try
                            {
                                objDPArr[4].Value = Int32.Parse(p_objTemplate_Target[i].m_strControl_TabIndex);

                            }
                            catch (Exception)
                            {
                                objDPArr[4].Value = 0;
                            }
                        }


                        objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                #endregion

                #region 保存表 Template_Dept_Visibility,刘颖源,2003-5-8 10:36:29
                //注意逗号,刘颖源,2003-5-8 10:36:29
                if (p_objTemplate_Dept_Visiblity != null)
                {
                    for (int i = 0; i < p_objTemplate_Dept_Visiblity.Length; i++)
                    {
                        string strSQL = @"insert into template_dept_visibility(template_id, departmentid, activity_date ) 
                        values(?, ?, ?)";

                        IDataParameter[] objDPArr = null;//new IDataParameter[3];
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].Value = p_strTemplateID;
                        objDPArr[1].Value = p_objTemplate_Dept_Visiblity[i].m_strDepartmentID;
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_objTemplate_Dept_Visiblity[i].m_strActivity_Date);


                        objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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
        #endregion

        #region 检查是否已存在当前分类下当前命名的模板
        /// <summary>
        /// 检查是否已存在当前分类下当前命名的模板
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strKeyWord"></param>
        /// <param name="p_strSetKeyWord"></param>
        /// <param name="p_strTemplateName"></param>
        /// <param name="p_strTemplateID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckTemplate(string p_strKeyWord,
            string p_strSetKeyWord,
            string p_strTemplateName,
            out string p_strTemplateID)
        {
            p_strTemplateID = "";
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","m_lngCheckTemplate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string strSQL = @"select distinct (a.template_id)
                                  from template_keyword a
                                 inner join template_set_detail_02 tsd on tsd.template_id = a.template_id
                                 inner join template_set_keyword b on b.set_id = tsd.set_id
                                 inner join template_detail c on c.template_id = a.template_id
                                 where a.keyword = ?
                                   and b.keyword = ?
                                   and c.template_name = ?
                                   and c.activity_date =
                                       (select max(activity_date)
                                          from template_detail
                                         where template_id = c.template_id)";

                IDataParameter[] objDPArr = null;//new IDataParameter[3];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strKeyWord;
                objDPArr[1].Value = p_strSetKeyWord;
                objDPArr[2].Value = p_strTemplateName;

                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count > 0)
                {
                    p_strTemplateID = objDataTableResult.Rows[0]["template_id"].ToString();
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
        #endregion

        #region 检查是否已存在当前分类下的模板
        /// <summary>
        /// 检查是否已存在当前分类下的模板
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strKeyWord"></param>
        /// <param name="p_strSetKeyWord"></param>
        /// <param name="p_strFormID"></param>
        /// <param name="p_strSetID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckTemplateSet(string p_strKeyWord,
            string p_strSetKeyWord,
            string p_strFormID,
            out string p_strSetID)
        {
            p_strSetID = "";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string strSQL = @"select distinct (b.set_id)
                                  from template_keyword a, template_set_keyword b, template_set_detail_02 c,tempate_set d
                                 where a.keyword = ?
                                   and b.keyword = ?
                                   and c.set_id = b.set_id
                                   and c.template_id = a.template_id
                                   and d.set_id = b.set_id
                                   and c.form_id = ?
                                   and d.end_date > ?
                                   order by b.set_id desc";

                IDataParameter[] objDPArr = null;//new IDataParameter[3];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strKeyWord;
                objDPArr[1].Value = p_strSetKeyWord;
                objDPArr[2].Value = p_strFormID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count > 0)
                {
                    p_strSetID = objDataTableResult.Rows[0]["set_id"].ToString();
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
        #endregion

        #region 保存套装模板,刘颖源,2003-5-9 10:50:09
        [AutoComplete]
        public long lngSaveTemplateSet(bool p_blnIsNewTemplate,
            clsTempate_SetValue p_objTempate_SetValue, clsTemplate_Set_Detail_01Value[] p_objTemplate_Set_Detail_01Value,
            clsTemplate_Set_Detail_02Value[] p_objTemplate_Set_Detail_02Value, clsTemplate_Set_KeywordValue[] p_objTemplate_Set_KeywordValue, ref long lngEff, out string p_strSetID)
        {
            p_strSetID = "";
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngSaveTemplateSet");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            long lngRes = 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                #region 保存表 Tempate_Set,刘颖源,2003-5-9 10:52:45
                //注意逗号,刘颖源,2003-5-9 10:52:45
                if (p_objTempate_SetValue != null)
                {
                    string strSQL = "";
                    if (p_blnIsNewTemplate)
                    {
                        p_strSetID = strGetTemplateSetID();
                        if (p_strSetID == null || p_strSetID == "")
                            p_strSetID = "00001";

                        strSQL = @"insert into tempate_set(set_id,start_date,end_date)
values(?,?,?)";
                        IDataParameter[] objDPArr = null;//new IDataParameter[3];
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].Value = p_strSetID.Trim();
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objTempate_SetValue.m_strStart_Date);
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_objTempate_SetValue.m_strEnd_Date);


                        objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                    else
                    {
                        strSQL = "update tempate_set set " +
                            "start_date=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objTempate_SetValue.m_strStart_Date) + "," +
                            "end_date=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objTempate_SetValue.m_strEnd_Date) + " where set_id='" + p_objTempate_SetValue.m_strSet_ID + "'";
                        p_strSetID = p_objTempate_SetValue.m_strSet_ID;
                        objHRPServ.DoExcute(strSQL);
                    }
                }
                #endregion

                #region 保存表 Template_Set_Detail_01,刘颖源,2003-5-9 10:56:05
                //注意逗号,刘颖源,2003-5-9 10:56:05
                if (p_objTemplate_Set_Detail_01Value != null)
                {
                    for (int i = 0; i < p_objTemplate_Set_Detail_01Value.Length; i++)
                    {
                        string strSQL = @"insert into template_set_detail_01(set_id, activity_date, set_name )
                        values( ?, ?, ? )";
                        IDataParameter[] objDPArr = null;//new IDataParameter[3];
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].Value = p_strSetID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objTemplate_Set_Detail_01Value[i].m_strActivity_Date);
                        objDPArr[2].Value = p_objTemplate_Set_Detail_01Value[i].m_strSet_Name;


                        objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                #endregion

                #region 保存表 Template_Set_Detail_02,刘颖源,2003-5-9 10:56:36
                //注意逗号,刘颖源,2003-5-9 10:56:36
                if (p_objTemplate_Set_Detail_02Value != null)
                {
                    for (int i = 0; i < p_objTemplate_Set_Detail_02Value.Length; i++)
                    {
                        string strSQL = @"insert into template_set_detail_02(set_id, activity_date, template_id, control_id, form_id, control_tabindex)
                        values( ?, ?, ?, ?, ?, ?)";

                        IDataParameter[] objDPArr = null;//new IDataParameter[5];
                        objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                        objDPArr[0].Value = p_strSetID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objTemplate_Set_Detail_02Value[i].m_strActivity_Date);
                        objDPArr[2].Value = p_objTemplate_Set_Detail_02Value[i].m_strTemplate_ID;
                        objDPArr[3].Value = p_objTemplate_Set_Detail_02Value[i].m_strControl_ID;
                        objDPArr[4].Value = p_objTemplate_Set_Detail_02Value[i].m_strForm_ID;

                        if (p_objTemplate_Set_Detail_02Value[i].m_strControl_TabIndex == null || p_objTemplate_Set_Detail_02Value[i].m_strControl_TabIndex == string.Empty)
                        {
                            objDPArr[5].Value = 0;

                        }
                        else
                        {
                            try
                            {
                                objDPArr[5].Value = Int32.Parse(p_objTemplate_Set_Detail_02Value[i].m_strControl_TabIndex);

                            }
                            catch (Exception)
                            {
                                objDPArr[5].Value = 0;
                            }
                        }

                        objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                #endregion

                #region 保存表 Template_Set_Keyword,刘颖源,2003-5-9 10:56:59
                //注意逗号,刘颖源,2003-5-9 10:56:59
                if (p_objTemplate_Set_KeywordValue != null)
                {
                    for (int i = 0; i < p_objTemplate_Set_KeywordValue.Length; i++)
                    {
                        string strSQL = @"insert into template_set_keyword( set_id, activity_date, keyword, keyword_type, keyword_py ) 
                        values( ?, ?, ?, ?, ?)";

                        IDataParameter[] objDPArr = null;//new IDataParameter[5];
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                        objDPArr[0].Value = p_strSetID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objTemplate_Set_KeywordValue[i].m_strActivity_Date);
                        objDPArr[2].Value = p_objTemplate_Set_KeywordValue[i].m_strKeyword.Replace("'", "き");
                        objDPArr[3].Value = p_objTemplate_Set_KeywordValue[i].m_strKeyword_Type;
                        objDPArr[4].Value = p_objTemplate_Set_KeywordValue[i].m_strKeyword_PY;


                        objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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
            return lngRes;
        }
        #endregion

        #region 获得所有窗体,刘颖源,2003-5-7 20:06:31
        [AutoComplete]
        public long lngGetAllForms(string p_strID, out clsGUI_InfoValue[] p_objValue)
        {
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetAllForms");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strSQL = "select form_id, form_desc from gui_info  order by form_desc";
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsGUI_InfoValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsGUI_InfoValue();
                        p_objValue[i].m_strForm_ID = objDataTableResult.Rows[i]["FORM_ID"].ToString(); ;
                        p_objValue[i].m_strForm_Desc = objDataTableResult.Rows[i]["FORM_DESC"].ToString(); ;
                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有的窗体下的控件,刘颖源,2003-5-7 19:52:38
        [AutoComplete]
        public long lngGetAllControls(string p_strFormID, out clsGUI_Info_DetailValue[] p_objValue)
        {
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetAllControls");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strSQL = "select control_id, form_id, control_desc, control_tabindex from gui_info_detail where form_id=? order by control_tabindex";
            p_objValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strFormID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsGUI_Info_DetailValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsGUI_Info_DetailValue();
                        p_objValue[i].m_strControl_ID = objDataTableResult.Rows[i]["CONTROL_ID"].ToString(); ;
                        p_objValue[i].m_strForm_ID = objDataTableResult.Rows[i]["FORM_ID"].ToString(); ;
                        p_objValue[i].m_strControl_Desc = objDataTableResult.Rows[i]["CONTROL_DESC"].ToString();
                        p_objValue[i].m_strControl_TabIndex = objDataTableResult.Rows[i]["CONTROL_TABINDEX"].ToString(); ;
                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有表ICD10_IllnessID,刘颖源,2003-5-8 16:24:59
        [AutoComplete]
        public long lngGetAllICD10_IllnessID(string p_strID, out clsICD10_IllnessIDValue[] p_objValue)
        {
            string strSQL = @"select illnessid,
       illnessname,
       illnesscategoryid,
       pycode,
       statcode,
       lessercode,
       status,
       deactiveddate,
       operaterid
  from icd10_illnessid";
            p_objValue = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetAllICD10_IllnessID");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsICD10_IllnessIDValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsICD10_IllnessIDValue();
                        p_objValue[i].m_strIllnessID = objDataTableResult.Rows[i]["ILLNESSID"].ToString();
                        p_objValue[i].m_strIllnessName = objDataTableResult.Rows[i]["ILLNESSNAME"].ToString();
                        p_objValue[i].m_strIllnessCategoryID = objDataTableResult.Rows[i]["ILLNESSCATEGORYID"].ToString();
                        p_objValue[i].m_strPYCode = objDataTableResult.Rows[i]["PYCODE"].ToString();
                        p_objValue[i].m_strStatCode = objDataTableResult.Rows[i]["STATCODE"].ToString();
                        p_objValue[i].m_strLesserCode = objDataTableResult.Rows[i]["LESSERCODE"].ToString();
                        p_objValue[i].m_strStatus = objDataTableResult.Rows[i]["STATUS"].ToString();
                        p_objValue[i].m_strDeActivedDate = objDataTableResult.Rows[i]["DEACTIVEDDATE"].ToString();
                        p_objValue[i].m_strOperaterID = objDataTableResult.Rows[i]["OPERATERID"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有表ICD10_IllnessSubID,Service层,刘颖源,2003-5-8 16:35:45
        [AutoComplete]
        public long lngGetAllICD10_IllnessSubID(string p_strID, out clsICD10_IllnessSubIDValue[] p_objValue)
        {

            string strSQL = @"select illnesssubid,
       illnessid,
       illnesssubname,
       discription,
       pycode,
       statcode,
       lessercode,
       status,
       deactiveddate,
       operaterid
  from icd10_illnesssubid
 where illnessid = ?";
            p_objValue = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetAllICD10_IllnessSubID");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strID;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsICD10_IllnessSubIDValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsICD10_IllnessSubIDValue();
                        p_objValue[i].m_strIllnessSubID = objDataTableResult.Rows[i]["ILLNESSSUBID"].ToString();
                        p_objValue[i].m_strIllnessID = objDataTableResult.Rows[i]["ILLNESSID"].ToString();
                        p_objValue[i].m_strIllnessSubName = objDataTableResult.Rows[i]["ILLNESSSUBNAME"].ToString();
                        p_objValue[i].m_strDiscription = objDataTableResult.Rows[i]["DISCRIPTION"].ToString();
                        p_objValue[i].m_strPYCode = objDataTableResult.Rows[i]["PYCODE"].ToString();
                        p_objValue[i].m_strStatCode = objDataTableResult.Rows[i]["STATCODE"].ToString();
                        p_objValue[i].m_strLesserCode = objDataTableResult.Rows[i]["LESSERCODE"].ToString();
                        p_objValue[i].m_strStatus = objDataTableResult.Rows[i]["STATUS"].ToString();
                        p_objValue[i].m_strDeActivedDate = objDataTableResult.Rows[i]["DEACTIVEDDATE"].ToString();
                        p_objValue[i].m_strOperaterID = objDataTableResult.Rows[i]["OPERATERID"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有表ICD10_IllnessDetailID,Service层,刘颖源,2003-5-8 16:50:24
        [AutoComplete]
        public long lngGetAllICD10_IllnessDetailID(string p_strID, out clsICD10_IllnessDetailIDValue[] p_objValue)
        {
            string strSQL = @"select illnessdetailid,
       illnesssubid,
       illnessdetailname,
       pycode,
       statcode,
       lessercode,
       status,
       deactiveddate,
       operaterid
  from icd10_illnessdetailid
 where illnesssubid = ?";
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetAllICD10_IllnessDetailID");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsICD10_IllnessDetailIDValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsICD10_IllnessDetailIDValue();
                        p_objValue[i].m_strIllnessDetailID = objDataTableResult.Rows[i]["ILLNESSDETAILID"].ToString();
                        p_objValue[i].m_strIllnessSubID = objDataTableResult.Rows[i]["ILLNESSSUBID"].ToString();
                        p_objValue[i].m_strIllnessDetailName = objDataTableResult.Rows[i]["ILLNESSDETAILNAME"].ToString();
                        p_objValue[i].m_strPYCode = objDataTableResult.Rows[i]["PYCODE"].ToString();
                        p_objValue[i].m_strStatCode = objDataTableResult.Rows[i]["STATCODE"].ToString();
                        p_objValue[i].m_strLesserCode = objDataTableResult.Rows[i]["LESSERCODE"].ToString();
                        p_objValue[i].m_strStatus = objDataTableResult.Rows[i]["STATUS"].ToString();
                        p_objValue[i].m_strDeActivedDate = objDataTableResult.Rows[i]["DEACTIVEDDATE"].ToString();
                        p_objValue[i].m_strOperaterID = objDataTableResult.Rows[i]["OPERATERID"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有表Bio_System,Service层,刘颖源,2003-5-8 17:28:01
        [AutoComplete]
        public long lngGetAllBio_System(string p_strID, out clsBio_SystemValue[] p_objValue)
        {
            string strSQL = @"select system_id, system_name from bio_system ";
            p_objValue = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetAllBio_System");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsBio_SystemValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsBio_SystemValue();
                        p_objValue[i].m_strSystem_ID = objDataTableResult.Rows[i]["SYSTEM_ID"].ToString();
                        p_objValue[i].m_strSystem_Name = objDataTableResult.Rows[i]["SYSTEM_NAME"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有表Bio_System_Detail,Service层,刘颖源,2003-5-8 17:29:24
        [AutoComplete]
        public long lngGetAllBio_System_Detail(string p_strID, out clsBio_System_DetailValue[] p_objValue)
        {
            string strSQL = "select system_id, componen_id, componen_name from bio_system_detail where system_id = ?";
            p_objValue = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetAllBio_System_Detail");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsBio_System_DetailValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsBio_System_DetailValue();
                        p_objValue[i].m_strSystem_ID = objDataTableResult.Rows[i]["SYSTEM_ID"].ToString();
                        p_objValue[i].m_strComponen_ID = objDataTableResult.Rows[i]["COMPONEN_ID"].ToString();
                        p_objValue[i].m_strComponen_Name = objDataTableResult.Rows[i]["COMPONEN_NAME"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有关联表TemplateFormControl,Service层,刘颖源,2003-5-9 10:26:53
        [AutoComplete]
        public long lngGetAllTemplateFormControl(string p_strFormID, string p_strControlID, string p_strEmployeeID, out clsTemplateFormControlValue[] p_objValue)
        {
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetAllTemplateFormControl");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strSQL = "select a.Template_ID,'" + p_strControlID + "' as Control_ID,'" + p_strFormID + "' as Form_ID,a.Template_Name from Template_Detail a " +
                @" inner join template_target b on (a.template_id=b.template_id and a.employeeid=?) 
				 where a.activity_date in  
				 (  
				 	select max(activity_date) from template_detail   
				 	where employeeid=?  
				 	group by template_id  
				 )  
				 and b.activity_date in  
				 (  
				 	select max(activity_date) from template_detail   
				 	where employeeid=? 
				 	group by template_id  
				 )and  b.form_id=? and b.control_id=?  
				 order by a.activity_date desc";


            p_objValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID;
                objDPArr[1].Value = p_strEmployeeID;
                objDPArr[2].Value = p_strEmployeeID;
                objDPArr[3].Value = p_strFormID;
                objDPArr[4].Value = p_strControlID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplateFormControlValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplateFormControlValue();
                        p_objValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objValue[i].m_strControl_ID = objDataTableResult.Rows[i]["CONTROL_ID"].ToString();
                        p_objValue[i].m_strForm_ID = objDataTableResult.Rows[i]["FORM_ID"].ToString();
                        p_objValue[i].m_strTemplate_Name = objDataTableResult.Rows[i]["TEMPLATE_NAME"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有单元模板的关键字,Service层
        [AutoComplete]
        public long lngGetSingleKeyword(int p_intKeywordType, string p_strFormID, string p_strControlID, string p_strEmployeeID, string p_strDepartmentID, out clsTemplate_KeywordValue[] p_objValue)
        {
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetSingleKeywordTemplates");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strCurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
            string strBegin = strCurrentDate + " 00:00:01";
            string strEnd = strCurrentDate + " 23:59:59";

            string strSQL = @"select subselect.template_id, subselect.activity_date, subselect.keyword
  from (select b.template_id, b.activity_date, b.keyword
          from template a
          left join template_keyword b on (a.template_id = b.template_id)
          left join template_detail c on (a.template_id = c.template_id)
          left join template_target d on (a.template_id = d.template_id)
          left join template_dept_visibility e on (a.template_id =
                                                  e.template_id)
         where b.keyword_type = ?
           and ? <= a.end_date
           and ? >= a.start_date
           and d.control_id = ?
           and d.form_id = ?
           and (c.visibility_level = 1 or
               (c.visibility_level = 0 and c.employeeid = ?) or
               (c.visibility_level = 2 and e.departmentid = ? and
               (select count(ss.template_id)
                    from (select template_id, departmentid, activity_date
                            from template_dept_visibility
                           where activity_date in
                                 (select max(activity_date)
                                    from template_dept_visibility
                                   group by template_id)) ss
                   where ss.departmentid = ?
                     and c.template_id = ss.template_id) > 0))
         group by b.keyword, b.template_id, b.activity_date) subselect
 where subselect.activity_date in
       (select max(activity_date) from template_detail group by template_id)
 order by keyword";


            p_objValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(8, out objDPArr);
                objDPArr[0].Value = p_intKeywordType;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strBegin);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(strEnd);
                objDPArr[3].Value = p_strControlID;
                objDPArr[4].Value = p_strFormID;
                objDPArr[5].Value = p_strEmployeeID;
                objDPArr[6].Value = p_strDepartmentID;
                objDPArr[7].Value = p_strDepartmentID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplate_KeywordValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplate_KeywordValue();
                        p_objValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString().Trim().Replace("き", "'");
                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有单元模板的关键字下的模板名称,Service层,刘颖源,2003-5-9 20:06:38
        [AutoComplete]
        public long lngGetSingleKeywordTemplates(int p_intKeywordType, string p_strFormID, string p_strControlID, string p_strKeyWord, string p_strEmployeeID, string p_strDepartmentID, out clsTemplate_DetailValue[] p_objValue)
        {
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetSingleKeywordTemplates");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strCurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
            string strBegin = strCurrentDate + " 00:00:01";
            string strEnd = strCurrentDate + " 23:59:59";
            #region 修改科室使用	remark
            /*修改科室使用			
			 * string strSQL = "Select * from (" + 
							"select c.Template_ID,c.Activity_Date, c.Template_Name,c.Content,c.EmployeeID,c.Visibility_Level from Template a " + 
							" left join Template_keyword b on (a.Template_ID=b.Template_ID) " + 
							" left join Template_Detail c on(a.Template_ID=c.Template_ID) " + 
							" left join Template_Target d on(a.Template_ID=d.Template_ID) " + 
							" left join Template_Dept_Visibility  e on(a.Template_ID=e.Template_ID) " + 
							"  " + 
							" where b.Keyword_Type=1 and '" + strBegin + "'<=a.End_Date and '" + strEnd +"'>=a.Start_Date  " + 
							" and d.Control_ID='" + p_strControlID + "' and d.Form_ID='" + p_strFormID + "'  " + 
							" and (c.Visibility_Level=1 or (c.Visibility_Level=0 and c.EmployeeID='" + p_strEmployeeID + "') " + 
							" or (c.Visibility_Level=2 and " + 
							" (select count(*) from Template_Dept_Visibility where DepartmentID='" + p_strDepartmentID +"')>0))" +
							" group by c.Template_ID,c.Activity_Date, c.Template_Name,c.Content,c.EmployeeID,c.Visibility_Level " + 
							"  )" +
							" SubSelect " +
							" where SubSelect.Activity_Date in" +
							"(select max(Activity_Date) from Template_Detail group by Template_ID)";

						string strSQL = "Select * from (" + 
							"select c.Template_ID,c.Activity_Date, c.Template_Name,c.Content,c.EmployeeID,c.Visibility_Level from Template a " + 
							" left join Template_keyword b on (a.Template_ID=b.Template_ID) " + 
							" left join Template_Detail c on(a.Template_ID=c.Template_ID) " + 
							" left join Template_Target d on(a.Template_ID=d.Template_ID) " + 
							" left join Template_Dept_Visibility  e on(a.Template_ID=e.Template_ID) " + 
							"  " + 
							" where b.Keyword_Type=1 and '" + strBegin + "'<=a.End_Date and '" + strEnd +"'>=a.Start_Date  " + 
							" and d.Control_ID='" + p_strControlID + "' and d.Form_ID='" + p_strFormID + "'  " + 
							" and (c.Visibility_Level=1 or (c.Visibility_Level=0 and c.EmployeeID='" + p_strEmployeeID + "') " + 
							" or (c.Visibility_Level=2 and " + 
							" ( " +
							" select count(*) from " +
							" (select DepartmentID from Template_Dept_Visibility " +
							" where Activity_Date in (Select Max(Activity_Date) from Template_Dept_Visibility Group by Template_ID) " +
							" ) DeptSelect " +
							" where DeptSelect.DepartmentID='" + p_strDepartmentID + "')>0))" +
							" group by c.Template_ID,c.Activity_Date, c.Template_Name,c.Content,c.EmployeeID,c.Visibility_Level " + 
							"  )" +
							" SubSelect " +
							" where SubSelect.Activity_Date in" +
							"(select max(Activity_Date) from Template_Detail group by Template_ID)";
			*/
            #endregion
            string strSQL = @"select subselect.template_id,
       subselect.activity_date,
       subselect.template_name,
       subselect.content,
       subselect.employeeid,
       subselect.visibility_level,
       subselect.departmentid
  from (select c.template_id,
               c.activity_date,
               c.template_name,
               c.content,
               c.employeeid,
               c.visibility_level,
               e.departmentid
          from template a
          left join template_keyword b on (a.template_id = b.template_id)
          left join template_detail c on (a.template_id = c.template_id)
          left join template_target d on (a.template_id = d.template_id)
          left join template_dept_visibility e on (a.template_id =
                                                  e.template_id)
         where b.keyword_type = ?
           and b.keyword = ?
           and ? <= a.end_date
           and ? >= a.start_date
           and d.control_id = ?
           and d.form_id = ?
           and (c.visibility_level = 1 or
               (c.visibility_level = 0 and c.employeeid = ?) or
               (c.visibility_level = 2 and e.departmentid = ? and
               (select count(ss.template_id)
                    from (select template_id, departmentid, activity_date
                            from template_dept_visibility
                           where activity_date in
                                 (select max(activity_date)
                                    from template_dept_visibility
                                   group by template_id)) ss
                   where ss.departmentid = ?
                     and c.template_id = ss.template_id) > 0))
         group by c.template_name,
                  c.template_id,
                  c.activity_date,
                  c.content,
                  c.employeeid,
                  c.visibility_level,
                  e.departmentid) subselect
 where subselect.activity_date in
       (select max(activity_date) from template_detail group by template_id)";
            p_objValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(9, out objDPArr);
                objDPArr[0].Value = p_intKeywordType;
                objDPArr[1].Value = p_strKeyWord.Replace("'", "き");
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(strBegin);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(strEnd);
                objDPArr[4].Value = p_strControlID;
                objDPArr[5].Value = p_strFormID;
                objDPArr[6].Value = p_strEmployeeID;
                objDPArr[7].Value = p_strDepartmentID;
                objDPArr[8].Value = p_strDepartmentID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplate_DetailValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplate_DetailValue();
                        p_objValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objValue[i].m_strTemplate_Name = objDataTableResult.Rows[i]["TEMPLATE_NAME"].ToString();
                        p_objValue[i].m_strContent = objDataTableResult.Rows[i]["CONTENT"].ToString();
                        p_objValue[i].m_strEmployeeID = objDataTableResult.Rows[i]["EMPLOYEEID"].ToString();
                        p_objValue[i].m_strVisibility_Level = objDataTableResult.Rows[i]["VISIBILITY_LEVEL"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有单元模板的ICD-10模板,Service层,刘颖源,2003-5-12 9:34:19
        [AutoComplete]
        public long lngGetSingleICD_10Templates(string p_strFormID, string p_strControlID, string p_strEmployeeID, string p_strDepartmentID, out clsTemplate_DetailValue[] p_objValue)
        {
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetSingleICD_10Templates");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strCurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
            string strBegin = strCurrentDate + " 00:00:01";
            string strEnd = strCurrentDate + " 23:59:59";
            string strSQL = @"select subselect.template_id,
       subselect.activity_date,
       subselect.template_name,
       subselect.content,
       subselect.employeeid,
       subselect.visibility_level
  from (select c.template_id,
               c.activity_date,
               c.template_name,
               c.content,
               c.employeeid,
               c.visibility_level
          from template a
          left join template_keyword b on (a.template_id = b.template_id)
          left join template_detail c on (a.template_id = c.template_id)
          left join template_target d on (a.template_id = d.template_id)
          left join template_dept_visibility e on (a.template_id =
                                                  e.template_id)
         inner join (select illnessid   as unionicd10id,
                           illnessname as unionicd10name
                      from icd10_illnessid
                     where status = '0'
                    union
                    select illnesssubid   as unionicd10id,
                           illnesssubname as unionicd10name
                      from icd10_illnesssubid
                     where status = 0
                    union
                    select illnessdetailid   as unionicd10id,
                           illnessdetailname as unionicd10name
                      from icd10_illnessdetailid
                     where status = 0
                    union
                    select oncologyid   as unionicd10id,
                           oncologyname as unionicd10name
                      from icdo_id
                     where status = 0) f on (f.unionicd10id = b.keyword)
        
         where b.keyword_type = 0
           and ? <= a.end_date
           and ? >= a.start_date
           and d.control_id = ?
           and d.form_id = ?
           and (c.visibility_level = 1 or
               (c.visibility_level = 0 and c.employeeid = ?) or
               (c.visibility_level = 2 and
               (select count(template_id)
                    from template_dept_visibility
                   where departmentid = ?) > 0))
         group by c.template_id,
                  c.activity_date,
                  c.template_name,
                  c.content,
                  c.employeeid,
                  c.visibility_level) subselect
 where subselect.activity_date in
       (select max(activity_date) from template_detail group by template_id)";
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(strBegin);
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strEnd);
                objDPArr[2].Value = p_strControlID;
                objDPArr[3].Value = p_strFormID;
                objDPArr[4].Value = p_strEmployeeID;
                objDPArr[5].Value = p_strDepartmentID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplate_DetailValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplate_DetailValue();
                        p_objValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objValue[i].m_strTemplate_Name = objDataTableResult.Rows[i]["TEMPLATE_NAME"].ToString();
                        p_objValue[i].m_strContent = objDataTableResult.Rows[i]["CONTENT"].ToString();
                        p_objValue[i].m_strEmployeeID = objDataTableResult.Rows[i]["EMPLOYEEID"].ToString();
                        p_objValue[i].m_strVisibility_Level = objDataTableResult.Rows[i]["VISIBILITY_LEVEL"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得单元模板八大系统模板,刘颖源,2003-5-12 10:44:21
        [AutoComplete]
        public long lngGetSingleBio_SystemTemplates(string p_strFormID, string p_strControlID, string p_strEmployeeID, string p_strDepartmentID, out clsTemplate_DetailValue[] p_objValue)
        {
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetSingleBio_SystemTemplates");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strCurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
            string strBegin = strCurrentDate + " 00:00:01";
            string strEnd = strCurrentDate + " 23:59:59";
            string strSQL = @"select subselect.template_id,
       subselect.activity_date,
       subselect.template_name,
       subselect.content,
       subselect.employeeid,
       subselect.visibility_level
  from (select c.template_id,
               c.activity_date,
               c.template_name,
               c.content,
               c.employeeid,
               c.visibility_level
          from template a
          left join template_keyword b on (a.template_id = b.template_id)
          left join template_detail c on (a.template_id = c.template_id)
          left join template_target d on (a.template_id = d.template_id)
          left join template_dept_visibility e on (a.template_id =
                                                  e.template_id)
         inner join (select system_id as s_id, system_name as s_name
                      from bio_system
                    union
                    select componen_id as s_id, componen_name as s_name
                      from bio_system_detail) f on (f.s_id = b.keyword)
        
         where b.keyword_type = 2
           and ? <= a.end_date
           and ? >= a.start_date
           and d.control_id = ?
           and d.form_id = ?
           and (c.visibility_level = 1 or
               (c.visibility_level = 0 and c.employeeid = ?) or
               (c.visibility_level = 2 and
               (select count(template_id)
                    from template_dept_visibility
                   where departmentid = ?) > 0))
         group by c.template_id,
                  c.activity_date,
                  c.template_name,
                  c.content,
                  c.employeeid,
                  c.visibility_level) subselect
 where subselect.activity_date in
       (select max(activity_date) from template_detail group by template_id)";


            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(strBegin);
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strEnd);
                objDPArr[2].Value = p_strControlID;
                objDPArr[3].Value = p_strFormID;
                objDPArr[4].Value = p_strEmployeeID;
                objDPArr[5].Value = p_strDepartmentID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplate_DetailValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplate_DetailValue();
                        p_objValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objValue[i].m_strTemplate_Name = objDataTableResult.Rows[i]["TEMPLATE_NAME"].ToString();
                        p_objValue[i].m_strContent = objDataTableResult.Rows[i]["CONTENT"].ToString();
                        p_objValue[i].m_strEmployeeID = objDataTableResult.Rows[i]["EMPLOYEEID"].ToString();
                        p_objValue[i].m_strVisibility_Level = objDataTableResult.Rows[i]["VISIBILITY_LEVEL"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }


        #endregion

        #region 获得所有套装模板中Keyword,Service层
        [AutoComplete]
        public long lngGetSetTemplateKeyword(string p_strFormID, string p_strControl_ID, string p_strEmployeeID, string p_strDepartmentID, out clsTemplate_Set_KeywordValue[] p_objValue)
        {
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetSetTemplateKeyword");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strCurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
            string strBegin = strCurrentDate + " 00:00:01";
            string strEnd = strCurrentDate + " 23:59:59";

            //			string strSQL = @"select base.Set_ID,base.Keyword,max(base.Activity_Date) as Activity_Date from
            //				(
            //				select  b.Set_ID,b.Activity_Date,b.Keyword From Tempate_Set a " + 
            //				" left join Template_Set_Keyword b on(a.Set_ID=b.Set_ID) " + 
            //				" left join Template_Set_Detail_01 c on(a.Set_ID=c.Set_ID) " + 
            //				" left join Template_Set_Detail_02 d on(a.Set_ID=d.Set_ID) " + 
            //				" left join Template_Target e on(d.Template_ID=e.Template_ID) " + 
            //				" left join Template_Detail f on(e.Template_ID=f.Template_ID) " + 
            //				" left join Template_Dept_Visibility  g on(e.Template_ID=g.Template_ID) " + 
            //				" where b.Activity_Date in " +
            //				" (select max(Activity_Date) as Activity_Date from Template_Set_Keyword group by set_id)" +
            //				" and b.Keyword_Type=1 and a.End_Date > "+clsDatabaseSQLConvert.s_StrGetServDateFuncName+@"  " + 
            //				" and d.Form_ID='" + p_strFormID + "' and d.Control_ID = '"+p_strControl_ID+"'" + 
            //				" and (f.Visibility_Level=1 or (f.Visibility_Level=0 and f.EmployeeID='" + p_strEmployeeID + "') " + 
            //				" or (f.Visibility_Level=2 and g.DepartmentID='"+p_strDepartmentID+"' and" + 
            //				" (select count(*) from Template_Dept_Visibility where DepartmentID='" + p_strDepartmentID + "')>0)) " + 				
            //				@"group by b.Keyword,b.Set_ID,b.Activity_Date) base
            //					group by base.Set_ID,base.Keyword
            //					order by base.Keyword";

            //使用exists性能大大高于ni
            //modify by tfzhang in 2005年11月17日
            //            string strSQL = @"select base.Set_ID, base.Keyword, max(base.Activity_Date) as Activity_Date,base.EmployeeID
            //									from (select b.Set_ID, b.Activity_Date, b.Keyword,f.EmployeeID
            //											From Tempate_Set a
            //											left join Template_Set_Keyword b on (a.Set_ID = b.Set_ID)
            //											left join Template_Set_Detail_01 c on (a.Set_ID = c.Set_ID)
            //											left join Template_Set_Detail_02 d on (a.Set_ID = d.Set_ID)
            //											left join Template_Target e on (d.Template_ID = e.Template_ID)
            //											left join Template_Detail f on (e.Template_ID = f.Template_ID)
            //											left join Template_Dept_Visibility g on (e.Template_ID =g.Template_ID)
            //											where b.Keyword_Type = 1
            //											and exists
            //											(select max(Activity_Date) as Activity_Date
            //													from Template_Set_Keyword
            //													group by set_id)
            //											and a.End_Date > "+clsDatabaseSQLConvert.s_StrGetServDateFuncName+@" 
            //											and d.Form_ID ='" + p_strFormID + @"'
            //											and d.Control_ID ='"+p_strControl_ID+@"'
            //											and (f.Visibility_Level = 1 or
            //												(f.Visibility_Level = 0 and f.EmployeeID ='" + p_strEmployeeID + @"') or
            //												(f.Visibility_Level = 2 and g.DepartmentID ='"+p_strDepartmentID+@"' and
            //												exists (select 1
            //															from Template_Dept_Visibility
            //															where DepartmentID = '"+p_strDepartmentID+@"')))
            //											group by b.Keyword, b.Set_ID, b.Activity_Date,f.EmployeeID) base
            //									group by base.Set_ID, base.Keyword,base.EmployeeID
            //									order by base.Keyword";
            //适用于Oracle
            string strSQL = "";
            if (clsHRPTableService.bytDatabase_Selector == 0)//SQL用的版本要修改才能使用
            {
                strSQL = @"select kw.set_id, kw.keyword, kw.activity_date, td.employeeid
  from template_set_keyword kw
 inner join template_set_detail_02 tsd2 on tsd2.set_id = kw.set_id
 inner join tempate_set ts on kw.set_id = ts.set_id
 inner join (select template_id, employeeid, activity_date, visibility_level
               from (select template_id,
                            employeeid,
                            activity_date,
                            visibility_level,
                            row_number() over(partition by template_id order by activity_date desc) num
                       from template_detail)
              where num = 1) td on td.template_id = tsd2.template_id
  left outer join template_dept_visibility tdv on tdv.template_id =
                                                  tsd2.template_id
 where ts.end_date > ?
   and kw.keyword_type = 1
   and tsd2.form_id = ?
   and tsd2.control_id = ?
   and (td.visibility_level = 1 or
       (td.visibility_level = 0 and td.employeeid = ?) or
       (td.visibility_level = 2 and tdv.departmentid = ?))
   and exists (select max(activity_date) activity_date
          from template_set_keyword
         where set_id = kw.set_id)
 group by kw.set_id, kw.keyword, kw.activity_date, td.employeeid
 order by kw.keyword";
            }
            else if (clsHRPTableService.bytDatabase_Selector == 2)
            {
                strSQL = @"select kw.set_id, kw.keyword, kw.activity_date, td.employeeid
  from template_set_keyword kw
 inner join template_set_detail_02 tsd2 on tsd2.set_id = kw.set_id
 inner join tempate_set ts on kw.set_id = ts.set_id
 inner join (select td.template_id,
                    td.employeeid,
                    td.activity_date,
                    td.visibility_level
               from (select template_id,
                            employeeid,
                            activity_date,
                            visibility_level,
                            row_number() over(partition by template_id order by activity_date desc) num
                       from template_detail) td
               left outer join template_dept_visibility tdv on tdv.template_id =
                                                               td.template_id
              where num = 1
                and (td.visibility_level = 1 or
                    (td.visibility_level = 0 and td.employeeid = ?) or
                    (td.visibility_level = 2 and tdv.departmentid = ?))) td on td.template_id =
                                                                               tsd2.template_id
  left outer join template_dept_visibility tdv on tdv.template_id =
                                                  tsd2.template_id
 where ts.end_date > ?
   and kw.keyword_type = 1
   and tsd2.form_id = ?
   and tsd2.control_id = ?
   and (td.visibility_level = 1 or
       (td.visibility_level = 0 and td.employeeid = ?) or
       (td.visibility_level = 2 and tdv.departmentid = ?))
   and exists (select max(activity_date) activity_date
          from template_set_keyword
         where set_id = kw.set_id)
 group by kw.set_id, kw.keyword, kw.activity_date, td.employeeid
 order by kw.keyword";//7
            }
            else if (clsHRPTableService.bytDatabase_Selector == 4)//DB2用的版本要修改才能使用
            {
                strSQL = @"select kw.set_id, kw.keyword, kw.activity_date, td.employeeid
                              from template_set_keyword kw
                             inner join template_set_detail_02 tsd2 on tsd2.set_id = kw.set_id
                             inner join tempate_set ts on kw.set_id = ts.set_id
                             inner join (select h.template_id, h.employeeid, h.activity_date, h.visibility_level
                                              from template_detail h
                                             where h.activity_date in (select max(w.activity_date) activity_date
                                                      from template_detail w
                                                     where w.template_id = h.template_id)) td on td.template_id = tsd2.template_id
                             left outer join template_dept_visibility tdv on tdv.template_id =
                                                                        tsd2.template_id
                             where ts.end_date > ?
                               and kw.keyword_type = 1
                               and tsd2.form_id = ?
                               and tsd2.control_id = ?
                               and (td.visibility_level = 1 or
                                   (td.visibility_level = 0 and td.employeeid = ?) or
                                   (td.visibility_level = 2 and tdv.departmentid = ?))
                               and exists (select max(activity_date)  activity_date
                                      from template_set_keyword
                                     where set_id = kw.set_id)
                             group by kw.set_id, kw.keyword, kw.activity_date, td.employeeid
                             order by kw.keyword";
            }



            p_objValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(7, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID;
                objDPArr[1].Value = p_strDepartmentID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[3].Value = p_strFormID;
                objDPArr[4].Value = p_strControl_ID;
                objDPArr[5].Value = p_strEmployeeID;
                objDPArr[6].Value = p_strDepartmentID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplate_Set_KeywordValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplate_Set_KeywordValue();
                        p_objValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString().TrimEnd().Replace("き", "'");
                        p_objValue[i].m_strEmpNO = objDataTableResult.Rows[i]["EmployeeID"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        /// <summary>
        /// 当前输入框是否有模板
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFormID"></param>
        /// <param name="p_strControl_ID"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strDepartmentID"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnHaveTempateSet(string p_strFormID, string p_strControl_ID, string p_strEmployeeID, string p_strDepartmentID)
        {
            string strSql = @"select count(ts.set_id)
  from tempate_set ts
 inner join template_set_detail_02 tsd2 on (ts.set_id = tsd2.set_id)
 inner join template_detail td on (tsd2.template_id = td.template_id)
  left join template_dept_visibility tdv on (tsd2.template_id =
                                            tdv.template_id)
 where ts.end_date > ?
   and tsd2.form_id = ?
   and tsd2.control_id = ?
   and (td.visibility_level = 1 or
       (td.visibility_level = 0 and td.employeeid = ?) or
       (td.visibility_level = 2 and tdv.departmentid = ? and
       (select count(template_id)
            from template_dept_visibility
           where departmentid = ?) > 0))";

            DataTable dtResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strFormID;
                objDPArr[2].Value = p_strControl_ID;
                objDPArr[3].Value = p_strEmployeeID;
                objDPArr[4].Value = p_strDepartmentID;
                objDPArr[5].Value = p_strDepartmentID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            if (lngRes > 0 && dtResult.Rows.Count > 0 && Convert.ToInt32(dtResult.Rows[0][0]) > 0)
                return true;
            return false;
        }

        #endregion

        #region 获得所有套装模板中Keyword下的模板名称,Service层
        [AutoComplete]
        public long lngGetSetTemplateKeywordName(string p_strFormID, string p_strControl_ID, string p_strKeyword, string p_strEmployeeID, string p_strDepartmentID, out clsTemplateSet_ID_NameValue[] p_objValue)
        {
            p_objValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetSetTemplateKeyword");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strCurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
            string strBegin = strCurrentDate + " 00:00:01";
            string strEnd = strCurrentDate + " 23:59:59";

            //string strSQL ="select  a.Set_ID,c.Set_Name from Tempate_Set a " + 
            //    " left join Template_Set_Keyword b on(a.Set_ID=b.Set_ID) " + 
            //    " left join Template_Set_Detail_01 c on(a.Set_ID=c.Set_ID) " + 
            //    " left join Template_Set_Detail_02 d on(a.Set_ID=d.Set_ID) " + 
            //    " left join Template_Target e on(d.Template_ID=e.Template_ID) " + 
            //    " left join Template_Detail f on(e.Template_ID=f.Template_ID) " + 
            //    " left join Template_Dept_Visibility  g on(e.Template_ID=g.Template_ID) " + 
            //    " where b.Keyword_Type=1 and b.Keyword='" + p_strKeyword.Replace("'","き") + "' and a.End_Date > "+clsDatabaseSQLConvert.s_StrGetServDateFuncName+ 
            //    " and d.Form_ID='" + p_strFormID + "' and d.Control_ID = '"+p_strControl_ID+"'" + 
            //    " and (f.Visibility_Level=1 or (f.Visibility_Level=0 and f.EmployeeID='" + p_strEmployeeID + "') " + 
            //    " or (f.Visibility_Level=2 and g.DepartmentID='"+p_strDepartmentID+"' and " + 
            //    " (select count(*) from Template_Dept_Visibility where DepartmentID='" + p_strDepartmentID + "')>0)) " + 
            //    " group by c.Set_Name,a.Set_ID" +
            //    " order by c.Set_Name";
            //适用于Oracle
            string strSQL = "";
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"select kw.set_id, tsd1.set_name
                              from template_set_keyword kw
                             inner join template_set_detail_02 tsd2 on tsd2.set_id = kw.set_id
                             inner join tempate_set ts on ts.set_id = kw.set_id
                             inner join (select template_id, employeeid, activity_date, visibility_level
                                           from (select template_id,
                                                        employeeid,
                                                        activity_date,
                                                        visibility_level,
                                                        row_number() over(partition by template_id order by activity_date desc) num
                                                   from template_detail)
                                          where num = 1) td on td.template_id = tsd2.template_id
                             inner join template_set_detail_01 tsd1 on tsd1.set_id = kw.set_id
                             left outer join template_dept_visibility tdv on tdv.template_id =
                                                                        tsd2.template_id
                             where ts.end_date > ?
                               and kw.keyword_type = 1
                               and tsd2.form_id = ?
                               and tsd2.control_id = ?
                               and kw.keyword = ?
                               and (td.visibility_level = 1 or
                                   (td.visibility_level = 0 and td.employeeid = ?) or
                                   (td.visibility_level = 2 and tdv.departmentid = ?))
                               and exists (select max(activity_date)  activity_date
                                      from template_set_keyword
                                     where set_id = kw.set_id)
                             group by tsd1.set_name, kw.set_id
                             order by tsd1.set_name";
            }
            else if (clsHRPTableService.bytDatabase_Selector == 2)
            {
                strSQL = @"select kw.set_id, tsd1.set_name
                              from template_set_keyword kw
                             inner join template_set_detail_02 tsd2 on tsd2.set_id = kw.set_id
                             inner join tempate_set ts on ts.set_id = kw.set_id
                             inner join (select template_id, employeeid, activity_date, visibility_level
                                           from (select template_id,
                                                        employeeid,
                                                        activity_date,
                                                        visibility_level,
                                                        row_number() over(partition by template_id order by activity_date desc) num
                                                   from template_detail)
                                          where num = 1) td on td.template_id = tsd2.template_id
                             inner join template_set_detail_01 tsd1 on tsd1.set_id = kw.set_id
                             left outer join template_dept_visibility tdv on tdv.template_id =
                                                                        tsd2.template_id
                             where ts.end_date > ?
                               and kw.keyword_type = 1
                               and tsd2.form_id = ?
                               and tsd2.control_id = ?
                               and kw.keyword = ?
                               and (td.visibility_level = 1 or
                                   (td.visibility_level = 0 and td.employeeid = ?) or
                                   (td.visibility_level = 2 and tdv.departmentid = ?))
                               and exists (select max(activity_date)  activity_date
                                      from template_set_keyword
                                     where set_id = kw.set_id)
                             group by tsd1.set_name, kw.set_id
                             order by tsd1.set_name";

            }
            else if (clsHRPTableService.bytDatabase_Selector == 4)
            {
                strSQL = @"select kw.set_id, tsd1.set_name
                              from template_set_keyword kw
                             inner join template_set_detail_02 tsd2 on tsd2.set_id = kw.set_id
                             inner join tempate_set ts on ts.set_id = kw.set_id
                             inner join (select h.template_id, h.employeeid, h.activity_date, h.visibility_level
                                          from template_detail h
                                         where h.activity_date in (select max(w.activity_date) activity_date
                                                  from template_detail w
                                                 where w.template_id = h.template_id)) td on td.template_id = tsd2.template_id
                             inner join template_set_detail_01 tsd1 on tsd1.set_id = kw.set_id
                             left outer join template_dept_visibility tdv on tdv.template_id =
                                                                        tsd2.template_id
                             where ts.end_date > ?
                               and kw.keyword_type = 1
                               and tsd2.form_id = ?
                               and tsd2.control_id = ?
                               and kw.keyword = ?
                               and (td.visibility_level = 1 or
                                   (td.visibility_level = 0 and td.employeeid = ?) or
                                   (td.visibility_level = 2 and tdv.departmentid = ?))
                               and exists (select max(activity_date)  activity_date
                                      from template_set_keyword
                                     where set_id = kw.set_id)
                             group by tsd1.set_name, kw.set_id
                             order by tsd1.set_name";
            }



            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strFormID;
                objDPArr[2].Value = p_strControl_ID;
                objDPArr[3].Value = p_strKeyword.Replace("'", "き");
                objDPArr[4].Value = p_strEmployeeID;
                objDPArr[5].Value = p_strDepartmentID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplateSet_ID_NameValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplateSet_ID_NameValue();
                        p_objValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objValue[i].m_strSet_Name = objDataTableResult.Rows[i]["SET_NAME"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有套装模板中ICD-10,Service层,刘颖源,2003-5-12 14:42:40
        [AutoComplete]
        public long lngGetSetTemplateICD_10(string p_strFormID, string p_strEmployeeID, string p_strDepartmentID, out clsTemplateSet_ID_NameValue[] p_objValue)
        {
            p_objValue = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetSetTemplateICD_10");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strCurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
            string strBegin = strCurrentDate + " 00:00:01";
            string strEnd = strCurrentDate + " 23:59:59";

            string strSQL = @"select a.set_id, c.set_name
  from tempate_set a
  left join template_set_keyword b on (a.set_id = b.set_id)
  left join template_set_detail_01 c on (a.set_id = c.set_id)
  left join template_set_detail_02 d on (a.set_id = d.set_id)
  left join template_target e on (d.template_id = e.template_id)
  left join template_detail f on (e.template_id = f.template_id)
  left join template_dept_visibility g on (e.template_id = g.template_id)
 where b.keyword_type = 0
   and ? <= a.end_date
   and ? >= a.start_date
   and d.form_id = ?
   and (f.visibility_level = 1 or
       (f.visibility_level = 0 and f.employeeid = ?) or
       (f.visibility_level = 2 and
       (select count(template_id)
            from template_dept_visibility
           where departmentid = ?) > 0))
 group by a.set_id, c.set_name
 order by c.set_name";

            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(strBegin);
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strEnd);
                objDPArr[2].Value = p_strFormID;
                objDPArr[3].Value = p_strEmployeeID;
                objDPArr[4].Value = p_strDepartmentID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplateSet_ID_NameValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplateSet_ID_NameValue();
                        p_objValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objValue[i].m_strSet_Name = objDataTableResult.Rows[i]["SET_NAME"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有套装模板中八大系统,Service层,刘颖源,2003-5-12 14:42:40
        [AutoComplete]
        public long lngGetSetTemplateBio_System(string p_strFormID, string p_strEmployeeID, string p_strDepartmentID, out clsTemplateSet_ID_NameValue[] p_objValue)
        {
            p_objValue = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetSetTemplateBio_System");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;
            string strCurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
            string strBegin = strCurrentDate + " 00:00:01";
            string strEnd = strCurrentDate + " 23:59:59";

            string strSQL = @"select a.set_id, c.set_name
  from tempate_set a
  left join template_set_keyword b on (a.set_id = b.set_id)
  left join template_set_detail_01 c on (a.set_id = c.set_id)
  left join template_set_detail_02 d on (a.set_id = d.set_id)
  left join template_target e on (d.template_id = e.template_id)
  left join template_detail f on (e.template_id = f.template_id)
  left join template_dept_visibility g on (e.template_id = g.template_id)
 where b.keyword_type = 2
   and ? <= a.end_date
   and ? >= a.start_date
   and d.form_id = ?
   and (f.visibility_level = 1 or
       (f.visibility_level = 0 and f.employeeid = ?) or
       (f.visibility_level = 2 and
       (select count(template_id)
            from template_dept_visibility
           where departmentid = ?) > 0))
 group by a.set_id, c.set_name
 order by c.set_name";

            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(strBegin);
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strEnd);
                objDPArr[2].Value = p_strFormID;
                objDPArr[3].Value = p_strEmployeeID;
                objDPArr[4].Value = p_strDepartmentID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplateSet_ID_NameValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplateSet_ID_NameValue();
                        p_objValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objValue[i].m_strSet_Name = objDataTableResult.Rows[i]["SET_NAME"].ToString();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #endregion

        #region 获得所有关联表TemplatesetContent,Service层,刘颖源,2003-5-12 15:02:20
        [AutoComplete]
        public long lngGetAllTemplatesetContent(string p_strSetID, out clsTemplatesetContentValue[] p_objValue)
        {

            //			string strSQL = "select a.control_ID,b.Template_Name,b.Content from Template_Set_Detail_02 a " + 
            //				" left join Template_Detail b on(a.Template_ID=b.Template_ID) " + 
            //				" where a.Set_ID='" + p_strSetID + "' and a.Activity_Date = (select max(Activity_Date) from Template_Set_Detail_02 where Set_ID = '"+p_strSetID+@"') order by a.control_ID";
            string strSQL = @"select a.control_id, b.template_name, b.content, c.control_desc
  from template_set_detail_02 a
  left join template_detail b on (a.template_id = b.template_id)
 inner join gui_info_detail c on (a.form_id = c.form_id)
                             and a.control_id = c.control_id
 where a.set_id = ?
   and b.activity_date =
       (select max(activity_date)
          from template_detail
         where template_id = b.template_id)
 order by a.control_tabindex";
            p_objValue = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetAllTemplatesetContent");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSetID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplatesetContentValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplatesetContentValue();
                        p_objValue[i].m_strControl_ID = objDataTableResult.Rows[i]["CONTROL_ID"].ToString();
                        p_objValue[i].m_strTemplate_Name = objDataTableResult.Rows[i]["TEMPLATE_NAME"].ToString();
                        p_objValue[i].m_strContent = objDataTableResult.Rows[i]["CONTENT"].ToString();
                        p_objValue[i].m_strControl_Desc = objDataTableResult.Rows[i]["Control_Desc"].ToString();
                    }
                }

            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        }

        #endregion

        #region 得到单元模板的信息,刘颖源,2003-5-15 9:09:29
        [AutoComplete]
        public long lngGetTemplateUnit(string p_strTemplateID, out clsTemplateValue[] p_objTemplateValue, out clsTemplate_KeywordValue[] p_objTemplate_KeywordValue,
            out clsTemplate_DetailValue[] p_objTemplate_DetailValue, out clsTemplate_Target_Gui_InfoValue[] p_objTemplate_Target_Gui_InfoValue, out clsTemplate_Dept_VisibilityValue[] p_objTemplate_Dept_VisibilityValue)
        {
            p_objTemplate_DetailValue = null;
            p_objTemplate_Target_Gui_InfoValue = null;
            p_objTemplate_Dept_VisibilityValue = null;
            p_objTemplate_KeywordValue = null;
            p_objTemplateValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetTemplateUnit");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            try
            {

                #region 获得所有表Template,Service层,刘颖源,2003-5-15 9:14:43
                string strSQL = "select template_id, start_date, end_date from template where template_id=?";
                p_objTemplateValue = null;

                DataTable objDataTableResult = new DataTable();
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strTemplateID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objTemplateValue = new clsTemplateValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objTemplateValue.Length; i++)
                    {
                        p_objTemplateValue[i] = new clsTemplateValue();
                        p_objTemplateValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objTemplateValue[i].m_strStart_Date = objDataTableResult.Rows[i]["START_DATE"].ToString();
                        p_objTemplateValue[i].m_strEnd_Date = objDataTableResult.Rows[i]["END_DATE"].ToString();

                    }
                }
                #endregion

                #region 获得所有表Template_Keyword,Service层,刘颖源,2003-5-15 9:17:05
                strSQL = @"select template_id, activity_date, keyword, keyword_py, keyword_type
  from template_keyword
 where template_id = ?
   and activity_date = (select max(activity_date)
                          from template_keyword
                         where template_id = ?)";
                p_objTemplate_KeywordValue = null;
                objDataTableResult = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strTemplateID;
                objDPArr[1].Value = p_strTemplateID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objTemplate_KeywordValue = new clsTemplate_KeywordValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objTemplate_KeywordValue.Length; i++)
                    {
                        p_objTemplate_KeywordValue[i] = new clsTemplate_KeywordValue();
                        p_objTemplate_KeywordValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objTemplate_KeywordValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objTemplate_KeywordValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString();
                        p_objTemplate_KeywordValue[i].m_strKeyword_PY = objDataTableResult.Rows[i]["KEYWORD_PY"].ToString();
                        p_objTemplate_KeywordValue[i].m_strKeyword_Type = objDataTableResult.Rows[i]["KEYWORD_TYPE"].ToString();

                    }
                }
                #endregion

                #region 获得所有表Template_Detail,Service层,刘颖源,2003-5-15 9:18:07
                strSQL = @"select template_id,
       activity_date,
       template_name,
       content,
       employeeid,
       visibility_level,
       contentxml
  from template_detail
 where template_id = ?
   and activity_date = (select max(activity_date)
                          from template_keyword
                         where template_id = ?)";
                p_objTemplate_DetailValue = null;
                objDataTableResult = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strTemplateID;
                objDPArr[1].Value = p_strTemplateID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objTemplate_DetailValue = new clsTemplate_DetailValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objTemplate_DetailValue.Length; i++)
                    {
                        p_objTemplate_DetailValue[i] = new clsTemplate_DetailValue();
                        p_objTemplate_DetailValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objTemplate_DetailValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objTemplate_DetailValue[i].m_strTemplate_Name = objDataTableResult.Rows[i]["TEMPLATE_NAME"].ToString();
                        p_objTemplate_DetailValue[i].m_strContent = objDataTableResult.Rows[i]["CONTENT"].ToString();
                        p_objTemplate_DetailValue[i].m_strEmployeeID = objDataTableResult.Rows[i]["EMPLOYEEID"].ToString();
                        p_objTemplate_DetailValue[i].m_strVisibility_Level = objDataTableResult.Rows[i]["VISIBILITY_LEVEL"].ToString();

                    }
                }
                #endregion

                #region 获得所有表Template_Target_Gui_Info,使用于,Service层,刘颖源,2003-5-15 9:24:37
                strSQL = @"select a.control_id, a.form_id, a.template_id, b.form_desc, c.control_desc
  from template_target a
 inner join gui_info b on (a.form_id = b.form_id)
 inner join gui_info_detail c on (b.form_id = c.form_id and
                                 a.form_id = c.form_id and
                                 a.control_id = c.control_id)
 where a.template_id = ?
   and a.activity_date = (select max(activity_date)
                            from template_keyword
                           where template_id = ?)
 order by a.form_id, a.control_id";
                p_objTemplate_Target_Gui_InfoValue = null;
                objDataTableResult = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strTemplateID;
                objDPArr[1].Value = p_strTemplateID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objTemplate_Target_Gui_InfoValue = new clsTemplate_Target_Gui_InfoValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objTemplate_Target_Gui_InfoValue.Length; i++)
                    {
                        p_objTemplate_Target_Gui_InfoValue[i] = new clsTemplate_Target_Gui_InfoValue();
                        p_objTemplate_Target_Gui_InfoValue[i].m_strControl_ID = objDataTableResult.Rows[i]["CONTROL_ID"].ToString();
                        p_objTemplate_Target_Gui_InfoValue[i].m_strForm_ID = objDataTableResult.Rows[i]["FORM_ID"].ToString();
                        p_objTemplate_Target_Gui_InfoValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objTemplate_Target_Gui_InfoValue[i].m_strForm_Desc = objDataTableResult.Rows[i]["FORM_DESC"].ToString();
                        p_objTemplate_Target_Gui_InfoValue[i].m_strControl_Desc = objDataTableResult.Rows[i]["CONTROL_DESC"].ToString();

                    }
                }
                #endregion

                #region 获得所有表Template_Dept_Visibility,Service层,刘颖源,2003-5-15 10:18:36
                strSQL = @"select template_id, departmentid, activity_date
  from template_dept_visibility
 where template_id = ?
   and activity_date = (select max(activity_date)
                          from template_keyword
                         where template_id = ?)";
                p_objTemplate_Dept_VisibilityValue = null;
                objDataTableResult = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strTemplateID;
                objDPArr[1].Value = p_strTemplateID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objTemplate_Dept_VisibilityValue = new clsTemplate_Dept_VisibilityValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objTemplate_Dept_VisibilityValue.Length; i++)
                    {
                        p_objTemplate_Dept_VisibilityValue[i] = new clsTemplate_Dept_VisibilityValue();
                        p_objTemplate_Dept_VisibilityValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objTemplate_Dept_VisibilityValue[i].m_strDepartmentID = objDataTableResult.Rows[i]["DEPARTMENTID"].ToString();
                        p_objTemplate_Dept_VisibilityValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();

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
            return (lngRes);
        }
        #endregion

        #region 得到该员工所有模板,刘颖源,2003-5-15 10:59:35
        [AutoComplete]
        public long lngGetEmployeeTemplateIDs(string p_strEmployeeID, out clsTemplate_DetailValue[] p_objValue)
        {
            p_objValue = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetEmployeeTemplateIDs");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            #region no keyword
            //			string strSQL = "select * from Template_Detail" + 
            //				" where Activity_Date in " + 
            //				" ( " + 
            //				" 	select Max(Activity_Date) from Template_Detail  " + 
            //				" 	where EmployeeID='" + p_strEmployeeID  + "' " + 
            //				" 	group by Template_ID " + 
            //				" ) " + 
            //				" order by Template_ID desc " + 
            //				" ";			
            #endregion

            string strSQL = @"select a.template_id,
       a.activity_date,
       a.template_name,
       a.content,
       a.employeeid,
       a.visibility_level,
       a.contentxml,
       b.keyword
  from template_detail a
 inner join template_keyword b on a.template_id = b.template_id
 where b.activity_date in
       (select max(activity_date) from template_keyword group by template_id)
   and a.activity_date in (select max(activity_date)
                             from template_detail
                            where employeeid = ?
                            group by template_id)
 order by b.keyword desc";

            p_objValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplate_DetailValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplate_DetailValue();
                        p_objValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objValue[i].m_strTemplate_Name = objDataTableResult.Rows[i]["TEMPLATE_NAME"].ToString();
                        p_objValue[i].m_strContent = objDataTableResult.Rows[i]["CONTENT"].ToString();
                        p_objValue[i].m_strEmployeeID = objDataTableResult.Rows[i]["EMPLOYEEID"].ToString();
                        p_objValue[i].m_strVisibility_Level = objDataTableResult.Rows[i]["VISIBILITY_LEVEL"].ToString();
                        p_objValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString();
                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }
        #endregion

        [AutoComplete]
        public long lngGetEmployeeTemplateIDsByForm(string p_strEmployeeID, string p_strFormName, out clsTemplate_DetailValue[] p_objValue)
        {
            p_objValue = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetEmployeeTemplateIDs");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strSQL = @"select base.template_id,
       base.activity_date,
       base.template_name,
       base.content,
       base.employeeid,
       base.visibility_level,
       base.contentxml,
       base.keyword
  from template_set_detail_02 tsd
 inner join (select a.template_id,
                    a.activity_date,
                    a.template_name,
                    a.content,
                    a.employeeid,
                    a.visibility_level,
                    a.contentxml,
                    b.keyword
               from template_detail a
              inner join template_keyword b on a.template_id = b.template_id
              where b.activity_date in
                    (select max(activity_date)
                       from template_keyword
                      group by template_id)
                and a.activity_date in
                    (select max(activity_date)
                       from template_detail
                      where employeeid = ?
                      group by template_id)) base on tsd.template_id =
                                                     base.template_id
 where tsd.form_id = ?
   and tsd.activity_date in (select max(activity_date)
                               from template_set_detail_02
                              group by template_id)
 order by base.keyword";

            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID;
                objDPArr[1].Value = p_strFormName;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplate_DetailValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplate_DetailValue();
                        p_objValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objValue[i].m_strTemplate_Name = objDataTableResult.Rows[i]["TEMPLATE_NAME"].ToString().Trim();
                        p_objValue[i].m_strContent = objDataTableResult.Rows[i]["CONTENT"].ToString();
                        p_objValue[i].m_strEmployeeID = objDataTableResult.Rows[i]["EMPLOYEEID"].ToString();
                        p_objValue[i].m_strVisibility_Level = objDataTableResult.Rows[i]["VISIBILITY_LEVEL"].ToString();
                        p_objValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString().Trim().Replace("き", "'");
                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            return lngRes;
        }

        #region 获得套装模板信息,刘颖源,2003-5-15 14:07:07
        [AutoComplete]
        public long lngGetTemplateSetInfo(string p_strSet_ID, out clsTempate_SetValue[] p_objTempate_SetValue, out clsTemplate_Set_KeywordValue[] p_objTemplate_Set_KeywordValue, out clsTemplateSet_Gui_TargetValue[] p_objTemplateSet_Gui_TargetValue, out clsTemplate_Set_Detail_01Value[] p_objTemplate_Set_Detail_01Value)
        {

            p_objTempate_SetValue = null;
            p_objTemplate_Set_KeywordValue = null;
            p_objTemplateSet_Gui_TargetValue = null;
            p_objTemplate_Set_Detail_01Value = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetTemplateSetInfo");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {

                IDataParameter[] objDPArr = null;
                #region 获得所有表Tempate_Set,Service层,刘颖源,2003-5-15 14:07:55
                string strSQL = "select set_id, start_date, end_date from tempate_set where set_id = ?";
                p_objTempate_SetValue = null;

                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSet_ID;

                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objTempate_SetValue = new clsTempate_SetValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objTempate_SetValue.Length; i++)
                    {
                        p_objTempate_SetValue[i] = new clsTempate_SetValue();
                        p_objTempate_SetValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objTempate_SetValue[i].m_strStart_Date = objDataTableResult.Rows[i]["START_DATE"].ToString();
                        p_objTempate_SetValue[i].m_strEnd_Date = objDataTableResult.Rows[i]["END_DATE"].ToString();

                    }
                }
                #endregion

                #region 获得所有表Template_Set_Keyword,Service层,刘颖源,2003-5-15 14:16:08
                strSQL = @"select set_id, activity_date, keyword, keyword_type, keyword_py
  from template_set_keyword
 where set_id = ?
   and activity_date =
       (select max(activity_date) from template_set_keyword where set_id = ?)";
                p_objTemplate_Set_KeywordValue = null;
                objDataTableResult = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strSet_ID;
                objDPArr[1].Value = p_strSet_ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objTemplate_Set_KeywordValue = new clsTemplate_Set_KeywordValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objTemplate_Set_KeywordValue.Length; i++)
                    {
                        p_objTemplate_Set_KeywordValue[i] = new clsTemplate_Set_KeywordValue();
                        p_objTemplate_Set_KeywordValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objTemplate_Set_KeywordValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objTemplate_Set_KeywordValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString().Trim().Replace("き", "'");
                        p_objTemplate_Set_KeywordValue[i].m_strKeyword_Type = objDataTableResult.Rows[i]["KEYWORD_TYPE"].ToString();
                        p_objTemplate_Set_KeywordValue[i].m_strKeyword_PY = objDataTableResult.Rows[i]["KEYWORD_PY"].ToString();

                    }
                }
                #endregion

                #region 获得所有表Template_Set_Detail_01,Service层,刘颖源,2003-5-15 16:08:05
                strSQL = @"select set_id, activity_date, set_name
  from template_set_detail_01
 where set_id = ?
   and activity_date = (select max(activity_date)
                          from template_set_detail_01
                         where set_id = ?)";
                p_objTemplate_Set_Detail_01Value = null;
                objDataTableResult = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strSet_ID;
                objDPArr[1].Value = p_strSet_ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objTemplate_Set_Detail_01Value = new clsTemplate_Set_Detail_01Value[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objTemplate_Set_Detail_01Value.Length; i++)
                    {
                        p_objTemplate_Set_Detail_01Value[i] = new clsTemplate_Set_Detail_01Value();
                        p_objTemplate_Set_Detail_01Value[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objTemplate_Set_Detail_01Value[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objTemplate_Set_Detail_01Value[i].m_strSet_Name = objDataTableResult.Rows[i]["SET_NAME"].ToString();

                    }
                }
                #endregion

                #region 获得所有表TemplateSet_Gui_Target,Service层,刘颖源,2003-5-15 15:20:00
                strSQL = @"select g.set_id,
       g.control_id,
       g.form_id,
       g.template_id,
       g.control_tabindex,
       g.form_desc,
       g.control_desc,
       g.template_name,
       g.activity_date
  from (select a.set_id,
               a.control_id,
               a.form_id,
               a.template_id,
               a.control_tabindex,
               b.form_desc,
               c.control_desc,
               d.template_name,
               d.activity_date
          from template_set_detail_02 a
         inner join gui_info b on (a.form_id = b.form_id)
         inner join gui_info_detail c on (b.form_id = c.form_id and
                                         a.form_id = c.form_id and
                                         a.control_id = c.control_id)
         inner join template_detail d on (a.template_id = d.template_id)
         where a.set_id = ?
           and a.activity_date = (select max(activity_date)
                                    from template_set_detail_02
                                   where set_id = ?)) g
 where g.activity_date in (select max(activity_date)
                             from template_detail
                            where template_id = g.template_id
                            group by template_id)
 order by g.form_id, g.control_tabindex, g.template_name";

                p_objTemplateSet_Gui_TargetValue = null;
                objDataTableResult = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strSet_ID;
                objDPArr[1].Value = p_strSet_ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objTemplateSet_Gui_TargetValue = new clsTemplateSet_Gui_TargetValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objTemplateSet_Gui_TargetValue.Length; i++)
                    {
                        p_objTemplateSet_Gui_TargetValue[i] = new clsTemplateSet_Gui_TargetValue();
                        p_objTemplateSet_Gui_TargetValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objTemplateSet_Gui_TargetValue[i].m_strControl_ID = objDataTableResult.Rows[i]["CONTROL_ID"].ToString();
                        p_objTemplateSet_Gui_TargetValue[i].m_strForm_ID = objDataTableResult.Rows[i]["FORM_ID"].ToString();
                        p_objTemplateSet_Gui_TargetValue[i].m_strTemplate_ID = objDataTableResult.Rows[i]["TEMPLATE_ID"].ToString();
                        p_objTemplateSet_Gui_TargetValue[i].m_strForm_Desc = objDataTableResult.Rows[i]["FORM_DESC"].ToString();
                        p_objTemplateSet_Gui_TargetValue[i].m_strControl_Desc = objDataTableResult.Rows[i]["CONTROL_DESC"].ToString();
                        p_objTemplateSet_Gui_TargetValue[i].m_strTemplate_Name = objDataTableResult.Rows[i]["TEMPLATE_NAME"].ToString();
                        p_objTemplateSet_Gui_TargetValue[i].m_strActivity_Date = objDataTableResult.Rows[i]["ACTIVITY_DATE"].ToString();
                        p_objTemplateSet_Gui_TargetValue[i].m_strControl_TabIndex = objDataTableResult.Rows[i]["control_tabindex"].ToString();
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
            return (lngRes);
        }
        #endregion

        #region 获得所有表EmployeeTemplateSet,Service层,刘颖源,2003-5-15 16:53:07
        [AutoComplete]
        public long lngGetAllEmployeeTemplateSet(string p_strEmployeeID, out clsEmployeeTemplateSetValue[] p_objEmployeeTemplateSetValue)
        {
            p_objEmployeeTemplateSetValue = null;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","lngGetAllEmployeeTemplateSet");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            #region no keyword
            //			string strSQL = "Select TSD1.Set_Name,Base.Set_ID  " + 
            //				" from Template_Set_Detail_01 as TSD1 inner join " + 
            //				" (Select distinct Set_ID " + 
            //				" from Template_Set_Detail_02 as TSD2 inner join  " + 
            //				" (Select Template_ID " + 
            //				" from Template_Detail " + 
            //				" where Activity_Date in (Select Max(Activity_Date) from Template_Detail group by Template_ID) " + 
            //				" and EmployeeID = '" + p_strEmployeeID + "' " + 
            //				" )as Base " + 
            //				" on TSD2.Template_ID = Base.Template_ID " + 
            //				" where Activity_Date in (Select Max(Activity_Date) from Template_Set_Detail_02 group by Set_ID) " + 
            //				" )as Base " + 
            //				" on TSD1.Set_ID = Base.Set_ID " + 
            //				" where Activity_Date in (Select Max(Activity_Date) from Template_Set_Detail_01 group by Set_ID)" + 
            //				" order by TSD1.Set_ID";
            #endregion

            string strSQL = @"select tsk.keyword, base.set_name, base.set_id
  from template_set_keyword tsk
 inner join (select tsd1.set_name, base.set_id
               from template_set_detail_01 tsd1
              inner join (select distinct set_id
                           from template_set_detail_02 tsd2
                          inner join (select template_id
                                       from template_detail
                                      where activity_date in
                                            (select max(activity_date)
                                               from template_detail
                                              group by template_id)
                                        and employeeid = ?) base on tsd2.template_id =
                                                                    base.template_id
                          where activity_date in
                                (select max(activity_date)
                                   from template_set_detail_02
                                  group by set_id)) base on tsd1.set_id =
                                                            base.set_id
              where activity_date in (select max(activity_date)
                                        from template_set_detail_01
                                       group by set_id)) base on tsk.set_id =
                                                                 base.set_id
 where activity_date in
       (select max(activity_date) from template_set_keyword group by set_id)
 order by tsk.keyword, base.set_name";
            //查每张表时，如果里面有个Activity_Date,都必须查找最大修改时间的那个记录
            p_objEmployeeTemplateSetValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objEmployeeTemplateSetValue = new clsEmployeeTemplateSetValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objEmployeeTemplateSetValue.Length; i++)
                    {
                        p_objEmployeeTemplateSetValue[i] = new clsEmployeeTemplateSetValue();
                        p_objEmployeeTemplateSetValue[i].m_strSet_Name = objDataTableResult.Rows[i]["SET_NAME"].ToString();
                        p_objEmployeeTemplateSetValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objEmployeeTemplateSetValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString().Trim().Replace("き", "'");

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return (lngRes);
        }
        #endregion

        #region 新的模板逻辑
        /// <summary>
        /// 查找某位员工记录的某张记录单下的所有模板
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_strKeyword"></param>
        /// <param name="p_objEmployeeTemplateSetValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetAllEmployeeTemplateSetByForm(string p_strDeptID, string p_strEmployeeID, string p_strFormName,
            out clsEmployeeTemplateSetValue[] p_objEmployeeTemplateSetValue)
        {
            p_objEmployeeTemplateSetValue = null;

            #region SQL
            //			string strSQL = @"Select distinct TSK.Keyword,Base.Set_Name,Base.Set_ID
            //from Template_Set_Keyword as TSK inner join
            //(Select TSD1.Set_Name,Base.Set_ID   
            //from Tempate_Set as TS inner join
            //Template_Set_Detail_01 as TSD1 on TS.Set_ID = TSD1.Set_ID and TS.End_Date > "+clsDatabaseSQLConvert.s_StrGetServDateFuncName+@"
            //inner join
            //(Select distinct Set_ID  from Template_Set_Detail_02 as TSD2 inner join
            //(Select Template_ID  from Template_Detail  where Activity_Date in (Select Max(Activity_Date) 
            //from Template_Detail group by Template_ID)
            //and (Visibility_Level = 0 and Employeeid = '"+p_strEmployeeID+@"') or (Visibility_Level <> '0' and EmployeeID in (select EmployeeID from dept_employee where deptid='"+p_strDeptID+@"')))as Base 
            //on TSD2.Template_ID = Base.Template_ID
            //and TSD2.Form_ID = '"+p_strFormName+@"'
            //where Activity_Date in (Select Max(Activity_Date) 
            //from Template_Set_Detail_02 group by Set_ID)  )as Base  
            //on TSD1.Set_ID = Base.Set_ID  where Activity_Date in (Select Max(Activity_Date) 
            //from Template_Set_Detail_01 group by Set_ID)  ) as Base
            //on TSK.Set_ID = Base.Set_ID
            //where Activity_Date in (Select Max(Activity_Date) From Template_Set_Keyword Group by Set_ID)
            //order by TSK.Keyword,Base.Set_Name";

            string strSQL = @"select distinct tsk.keyword, base.set_name, base.set_id
  from template_set_keyword tsk
 inner join (select tsd1.set_name, base.set_id
               from tempate_set ts
              inner join template_set_detail_01 tsd1 on ts.set_id =
                                                        tsd1.set_id
                                                    and ts.end_date > ?
              inner join (select distinct set_id
                           from template_set_detail_02 tsd2
                          inner join (select template_id
                                       from template_detail
                                      where activity_date in
                                            (select max(activity_date)
                                               from template_detail
                                              group by template_id)
                                        and employeeid = ?) base on tsd2.template_id =
                                                                    base.template_id
                                                                and tsd2.form_id = ?
                          where activity_date in
                                (select max(activity_date)
                                   from template_set_detail_02
                                  group by set_id)) base on tsd1.set_id =
                                                            base.set_id
              where activity_date in (select max(activity_date)
                                        from template_set_detail_01
                                       group by set_id)) base on tsk.set_id =
                                                                 base.set_id
 where activity_date in
       (select max(activity_date) from template_set_keyword group by set_id)
 order by tsk.keyword, base.set_name";
            #endregion

            //查每张表时，如果里面有个Activity_Date,都必须查找最大修改时间的那个记录
            p_objEmployeeTemplateSetValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strEmployeeID;
                objDPArr[2].Value = p_strFormName;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objEmployeeTemplateSetValue = new clsEmployeeTemplateSetValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objEmployeeTemplateSetValue.Length; i++)
                    {
                        p_objEmployeeTemplateSetValue[i] = new clsEmployeeTemplateSetValue();
                        p_objEmployeeTemplateSetValue[i].m_strSet_Name = objDataTableResult.Rows[i]["SET_NAME"].ToString().Trim();
                        p_objEmployeeTemplateSetValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objEmployeeTemplateSetValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString().Trim().Replace("き", "'");

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return (lngRes);
        }

        /// <summary>
        /// 获取所有模板(最高权限)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_objEmployeeTemplateSetValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetAllTemplate(string p_strFormName, out clsEmployeeTemplateSetValue[] p_objEmployeeTemplateSetValue)
        {
            p_objEmployeeTemplateSetValue = null;

            #region SQL	
            string strSQL = @"select distinct tsk.keyword, base.set_name, base.set_id
								from template_set_keyword tsk
								inner join (select tsd1.set_name, base.set_id
											from tempate_set ts
											inner join template_set_detail_01 tsd1 on ts.set_id =
																						tsd1.set_id
																					and ts.end_date >?
											inner join (select distinct set_id
														from template_set_detail_02 tsd2
														inner join (select template_id
																	from template_detail
																	where activity_date in
																			(select max(activity_date)
																			from template_detail
																			group by template_id)) base on tsd2.template_id =
																											base.template_id
																										and tsd2.form_id =?
														where activity_date in
																(select max(activity_date)
																from template_set_detail_02
																group by set_id)) base on tsd1.set_id =
																							base.set_id
											where activity_date in (select max(activity_date)
																		from template_set_detail_01
																	group by set_id)) base on tsk.set_id =
																								base.set_id
								where activity_date in
									(select max(activity_date) from template_set_keyword group by set_id)
								order by tsk.keyword, base.set_name";
            #endregion

            //查每张表时，如果里面有个Activity_Date,都必须查找最大修改时间的那个记录
            p_objEmployeeTemplateSetValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strFormName;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objEmployeeTemplateSetValue = new clsEmployeeTemplateSetValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objEmployeeTemplateSetValue.Length; i++)
                    {
                        p_objEmployeeTemplateSetValue[i] = new clsEmployeeTemplateSetValue();
                        p_objEmployeeTemplateSetValue[i].m_strSet_Name = objDataTableResult.Rows[i]["SET_NAME"].ToString().Trim();
                        p_objEmployeeTemplateSetValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objEmployeeTemplateSetValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString().Trim().Replace("き", "'");

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return (lngRes);
        }
        /// <summary>
        /// 查找某位员工所见科室的某张记录单下的所有模板(该员工为主任医师或护士长)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strArrDeptID"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_objEmployeeTemplateSetValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetAllEmployeeDeptTemplateSetByForm(string[] p_strArrDeptID, string p_strEmployeeID, string p_strFormName,
            out clsEmployeeTemplateSetValue[] p_objEmployeeTemplateSetValue)
        {
            p_objEmployeeTemplateSetValue = null;
            string strDeptSql = "";

            if (p_strArrDeptID != null && p_strArrDeptID.Length > 0)
            {
                strDeptSql += "( ";
                for (int i = 0; i < p_strArrDeptID.Length; i++)
                {
                    if (i == 0)
                    {
                        strDeptSql += " departmentid = '" + p_strArrDeptID[i] + "' ";
                    }
                    else
                    {
                        strDeptSql += "or departmentid = '" + p_strArrDeptID[i] + "' ";
                    }
                }
                strDeptSql += " )";
            }
            else
                return -1;

            #region SQL	
            //            string strSQL = @"Select distinct TSK.Keyword, Base.Set_Name, Base.Set_ID
            //								from Template_Set_Keyword TSK
            //								inner join (Select TSD1.Set_Name, Base.Set_ID
            //											from Tempate_Set TS
            //											inner join Template_Set_Detail_01 TSD1 on TS.Set_ID =
            //																						TSD1.Set_ID
            //																					and TS.End_Date >
            //																						"+clsDatabaseSQLConvert.s_StrGetServDateFuncName+@"
            //											inner join (Select distinct Set_ID
            //														from Template_Set_Detail_02 TSD2
            //														inner join (Select Template_ID
            //																	from Template_Detail
            //																	where Activity_Date in
            //																			(Select Max(Activity_Date)
            //																			from Template_Detail
            //																			group by Template_ID)
            //																		and EmployeeID =
            //																			'"+p_strEmployeeID+@"' 
            //																		union
            //																		select b.Template_ID
            //																		from TEMPLATE_DEPT_VISIBILITY b
            //																		where "+strDeptSql+@"
            //																			and b.Activity_Date in
            //																				(Select Max(Activity_Date)
            //																				from Template_Detail
            //																				group by Template_ID)) Base on TSD2.Template_ID =
            //																											Base.Template_ID
            //																										and TSD2.Form_ID =
            //																											'"+p_strFormName+@"'
            //														where Activity_Date in
            //																(Select Max(Activity_Date)
            //																from Template_Set_Detail_02
            //																group by Set_ID)) Base on TSD1.Set_ID =
            //																							Base.Set_ID
            //											where Activity_Date in (Select Max(Activity_Date)
            //																		from Template_Set_Detail_01
            //																	group by Set_ID)) Base on TSK.Set_ID =
            //																								Base.Set_ID
            //								where Activity_Date in
            //									(Select Max(Activity_Date) From Template_Set_Keyword Group by Set_ID)
            //								order by TSK.Keyword, Base.Set_Name";
            //适用于Oracle
            string strSQL = @"select distinct tsk.keyword, base.set_name, base.set_id
                              from template_set_keyword tsk
                             inner join (select tsd1.set_name, base.set_id
                                           from tempate_set ts
                                          inner join template_set_detail_01 tsd1 on ts.set_id =
                                                                                    tsd1.set_id
                                                                                and ts.end_date >
                                                                                    " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @"
                                          inner join (select distinct set_id
                                                       from template_set_detail_02 tsd2
                                                      inner join (select template_id
                                                                   from (select template_id,
                                                                                employeeid,
                                                                                row_number() over(partition by template_id order by activity_date desc) num
                                                                           from template_detail)
                                                                  where num = 1
                                                                    and employeeid = '" + p_strEmployeeID + @"'
                                                                 union
                                                                 select b.template_id
                                                                   from template_detail          a,
                                                                        template_dept_visibility b
                                                                  where " + strDeptSql + @"
                                                                    and a.template_id = b.template_id
                                                                    and a.activity_date =
                                                                        b.activity_date
                                                                    and a.visibility_level = 2
                                                                    and b.activity_date =
                                                                        (select max(activity_date)
                                                                           from template_detail
                                                                          where template_id =
                                                                                b.template_id)) base on tsd2.template_id =
                                                                                                        base.template_id
                                                                                                    and tsd2.form_id =
                                                                                                        '" + p_strFormName + @"'
                                                      where activity_date in
                                                            (select max(activity_date)
                                                               from template_set_detail_02
                                                              group by set_id)) base on tsd1.set_id =
                                                                                        base.set_id
                                          where activity_date in (select max(activity_date)
                                                                    from template_set_detail_01
                                                                   group by set_id)) base on tsk.set_id =
                                                                                             base.set_id
                             where activity_date in
                                   (select max(activity_date) from template_set_keyword group by set_id)
                             order by tsk.keyword, base.set_name";
            #endregion

            //查每张表时，如果里面有个Activity_Date,都必须查找最大修改时间的那个记录
            p_objEmployeeTemplateSetValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objEmployeeTemplateSetValue = new clsEmployeeTemplateSetValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objEmployeeTemplateSetValue.Length; i++)
                    {
                        p_objEmployeeTemplateSetValue[i] = new clsEmployeeTemplateSetValue();
                        p_objEmployeeTemplateSetValue[i].m_strSet_Name = objDataTableResult.Rows[i]["SET_NAME"].ToString().Trim();
                        p_objEmployeeTemplateSetValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objEmployeeTemplateSetValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString().Trim().Replace("き", "'");

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return (lngRes);
        }

        /// <summary>
        /// 查找某位员工记录的某张记录单下的所有模板关键字
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_dtKeyword"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetAllEmployeeTemplateKeywordByForm(string p_strEmployeeID, string p_strFormName, out DataTable p_dtKeyword)
        {
            #region SQL
            string strSQL = @"select distinct tsk.keyword
  from template_set_keyword tsk
 inner join (select tsd1.set_name, base.set_id
               from tempate_set as ts
              inner join template_set_detail_01 as tsd1 on ts.set_id =
                                                           tsd1.set_id
                                                       and ts.end_date > ?
              inner join (select distinct set_id
                           from template_set_detail_02 tsd2
                          inner join (select template_id
                                       from template_detail
                                      where activity_date in
                                            (select max(activity_date)
                                               from template_detail
                                              group by template_id)
                                        and employeeid = ?) base on tsd2.template_id =
                                                                    base.template_id
                                                                and tsd2.form_id = ?
                          where activity_date in
                                (select max(activity_date)
                                   from template_set_detail_02
                                  group by set_id)) base on tsd1.set_id =
                                                            base.set_id
              where activity_date in (select max(activity_date)
                                        from template_set_detail_01
                                       group by set_id)) base on tsk.set_id =
                                                                 base.set_id
 where activity_date in
       (select max(activity_date) from template_set_keyword group by set_id)
 order by tsk.keyword";
            #endregion

            p_dtKeyword = null;

            //查每张表时，如果里面有个Activity_Date,都必须查找最大修改时间的那个记录

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strEmployeeID;
                objDPArr[2].Value = p_strFormName;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtKeyword, objDPArr);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;

        }

        /// <summary>
        /// 查找某位员工记录的某张记录单下的某个关键字下的所有套装模板
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_strKeyword"></param>
        /// <param name="p_objEmployeeTemplateSetValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetAllEmployeeTemplateSetByFormAndKeyword(string p_strDeptID, string p_strEmployeeID, string p_strFormName, string p_strKeyword,
            out clsEmployeeTemplateSetValue[] p_objEmployeeTemplateSetValue)
        {
            p_objEmployeeTemplateSetValue = null;

            #region SQL
            string strSQL = @"select distinct tsk.keyword, base.set_name, base.set_id
  from template_set_keyword tsk
 inner join (select tsd1.set_name, base.set_id
               from tempate_set ts
              inner join template_set_detail_01 tsd1 on ts.set_id =
                                                        tsd1.set_id
                                                    and ts.end_date > ?
              inner join (select distinct set_id
                           from template_set_detail_02 tsd2
                          inner join (select template_id
                                       from template_detail
                                      where activity_date in
                                            (select max(activity_date)
                                               from template_detail
                                              group by template_id)
                                        and (visibility_level = 0 and
                                            employeeid = ?)
                                         or (visibility_level <> '0' and
                                            employeeid in
                                            (select employeeid
                                                from dept_employee
                                               where deptid = ?))) base on tsd2.template_id =
                                                                           base.template_id
                                                                       and tsd2.form_id = ?
                          where activity_date in
                                (select max(activity_date)
                                   from template_set_detail_02
                                  group by set_id)) base on tsd1.set_id =
                                                            base.set_id
              where activity_date in (select max(activity_date)
                                        from template_set_detail_01
                                       group by set_id)) base on tsk.set_id =
                                                                 base.set_id
 where activity_date in
       (select max(activity_date) from template_set_keyword group by set_id)
   and tsk.keyword = ?
 order by tsk.keyword, base.set_name";
            #endregion

            //查每张表时，如果里面有个Activity_Date,都必须查找最大修改时间的那个记录
            p_objEmployeeTemplateSetValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strEmployeeID;
                objDPArr[2].Value = p_strDeptID;
                objDPArr[3].Value = p_strFormName;
                objDPArr[4].Value = p_strKeyword;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objEmployeeTemplateSetValue = new clsEmployeeTemplateSetValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objEmployeeTemplateSetValue.Length; i++)
                    {
                        p_objEmployeeTemplateSetValue[i] = new clsEmployeeTemplateSetValue();
                        p_objEmployeeTemplateSetValue[i].m_strSet_Name = objDataTableResult.Rows[i]["SET_NAME"].ToString().Trim();
                        p_objEmployeeTemplateSetValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objEmployeeTemplateSetValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString().Trim().Replace("き", "'");

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return (lngRes);
        }

        /// <summary>
        /// 得到模板具体内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTemplateID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_strGetTemplateContent(string p_strTemplateID, out string p_strTemplateContent, out string p_strCreateID, out string p_strCreateName)
        {
            p_strTemplateContent = "";
            p_strCreateID = "";
            p_strCreateName = "";
            string strSQL = @"select content, employeeid, f_getempnamebyno(employeeid) as empname
  from template_detail
 where template_id = ?
   and activity_date =
       (select max(activity_date) from template_detail where template_id = ?)";

            DataTable dtResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strTemplateID;
                objDPArr[1].Value = p_strTemplateID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_strTemplateContent = dtResult.Rows[0][0].ToString();
                    p_strCreateID = dtResult.Rows[0][1].ToString();
                    p_strCreateName = dtResult.Rows[0][2].ToString();
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        }

        /// <summary>
        /// 修改模板具体内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTemplateID"></param>
        /// <param name="p_strContent"></param>
        /// <param name="p_strVisibility_Level">可见级别</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyTemplateContent(string p_strTemplateID, string p_strContent, string p_strVisibility_Level)
        {
            //注意：不能简单地拼字符串，可能Content里面有 ' 等危险字符
            string strSQL = @"update template_detail
set content = ?,visibility_level = ?
where template_id = ?
and activity_date = (select max(activity_date) from template_detail where template_id=?)";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                //				for(int i = 0; i < objDPArr.Length; i++)
                //					objDPArr[i] = new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strContent;
                objDPArr[1].Value = p_strVisibility_Level;
                objDPArr[2].Value = p_strTemplateID;
                objDPArr[3].Value = p_strTemplateID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

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
        /// 修改模板可见级别
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTemplateID"></param>
        /// <param name="p_strVisibility_Level">可见级别</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyTemplateVisibitity(string p_strTemplateID, string p_strVisibility_Level)
        {
            string strSQL = @"update template_detail a
set a.visibility_level = ?
where a.template_id = ?
and a.activity_date = (select max(activity_date) from template_detail where template_id=a.template_id)";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strVisibility_Level;
                objDPArr[1].Value = p_strTemplateID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

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
        /// 停用模板
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSetID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteTemplate(string p_strSetID)
        {
            string strSql = @"update tempate_set
set end_date = ?
where set_id = ?";


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strSetID;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;

        }

        /// <summary>
        /// 修改模板基本信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSetID"></param>
        /// <param name="p_strKeyword"></param>
        /// <param name="p_strDisease"></param>
        /// <param name="p_strOperation"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyTemplateBaseInfo(string p_strSetID, string p_strSetName, string p_strKeyword)
        {

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {


                #region 修改模板名
                string strSql = @"update template_set_detail_01
set set_name = ?
where (set_id = ?)";

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //			objDPArr[0] = new Oracle.DataAccess.Client.OracleParameter();
                //			objDPArr[1] = new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strSetName;
                objDPArr[1].Value = p_strSetID;

                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;
                #endregion

                #region 修改关键字
                strSql = @"update template_set_keyword
set keyword = ?
where (set_id = ?)";

                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[2];
                objHRPServ.CreateDatabaseParameter(2, out objDPArr2);
                //			objDPArr2[0] = new Oracle.DataAccess.Client.OracleParameter();
                //			objDPArr2[1] = new Oracle.DataAccess.Client.OracleParameter();
                objDPArr2[0].Value = p_strKeyword;
                objDPArr2[1].Value = p_strSetID;

                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr2);

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
            return lngRes;
        }

        /// <summary>
        /// 查找表单下的关键字
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strForm"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetKeywordsByForm(string p_strForm, string p_strEmployeeID, string p_strDeptID, out DataTable p_dtResult)
        {
            p_dtResult = null;

            #region SQL
            string strSql = @"select distinct tsk.keyword
  from template_set_keyword tsk
 inner join tempate_set ts on tsk.set_id = ts.set_id
 inner join template_set_detail_02 tsd2 on tsk.set_id = tsd2.set_id
 inner join template_detail td on (tsd2.template_id = td.template_id)
  left join template_dept_visibility tdv on (tdv.template_id =
                                            td.template_id)
 where (ts.end_date > ?)
   and (tsd2.form_id = ?)
   and (td.visibility_level = 1 or
       (td.visibility_level = 0 and td.employeeid = ?) or
       (td.visibility_level = 2 and tdv.departmentid = ? and
       (select count(template_id)
            from template_dept_visibility
           where departmentid = ?) > 0))";
            #endregion


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strForm;
                objDPArr[2].Value = p_strEmployeeID;
                objDPArr[3].Value = p_strDeptID;
                objDPArr[4].Value = p_strDeptID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtResult, objDPArr);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;

        }
        #endregion

        #region 模板串联
        private const string m_strAddNewAssociate = @"insert into templateset_associate
							(associateid,formname, templatesetid, associatename,type,deptid)
							values (?,?,?,?,?,?)";
        private const string m_strCheckAssociateExist = @"select associateid, deptid, formname, templatesetid, associatename, type
  from templateset_associate
 where templatesetid = ?
   and type = ?
   and deptid = ?";

        /// <summary>
        /// 保存套装模板所关联的字段
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngSaveTemplateSet_Associate(clsTemplateSet_Associate p_objContent)
        {
            if (p_objContent == null) return 0;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                if (p_objContent.strAssociateID == "")
                    objHRPServ.lngGenerateID(10, "AssociateID", "TemplateSet_Associate", out p_objContent.strAssociateID);

                if (m_blnAssociateExist(p_objContent.strTemplateSetID, p_objContent.intType, p_objContent.strDeptID))
                {
                    string strDel = @"delete templateset_associate where templatesetid=? and type=? and deptid = ?";
                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_objContent.strTemplateSetID;
                    objDPArr[1].Value = p_objContent.intType;
                    objDPArr[2].Value = p_objContent.strDeptID;

                    long lngEff = -1;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strDel, ref lngEff, objDPArr);
                    if (lngRes <= 0) return lngRes;
                }

                if (p_objContent.strAssociateName.Trim() != "")
                {
                    lngRes = m_lngAddNewTemplateSet_Associate(p_objContent);
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


        /// <summary>
        /// 套装模板是否已有此类别的关联
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        private bool m_blnAssociateExist(string p_strSetID, int p_intType, string p_strDeptID)
        {

            long lngRes = 0;
            if (p_strSetID == null || p_strSetID == "" || p_strDeptID == null || p_strDeptID == "")
                return false;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr1 = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr1);
                objDPArr1[0].Value = p_strSetID;
                objDPArr1[1].Value = p_intType;
                objDPArr1[2].Value = p_strDeptID;

                DataTable dtResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCheckAssociateExist, ref dtResult, objDPArr1);
                if (lngRes > 0 && dtResult.Rows.Count == 1)
                    return true;

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
            return false;
        }

        /// <summary>
        /// 新添套装模板所关联的字段
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewTemplateSet_Associate(clsTemplateSet_Associate p_objContent)
        {
            clsHRPTableService objHRPServ = new clsHRPTableService();

            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(6, out objDPArr);
            objDPArr[0].Value = p_objContent.strAssociateID;
            objDPArr[1].Value = p_objContent.strFormName;
            objDPArr[2].Value = p_objContent.strTemplateSetID;
            objDPArr[3].Value = p_objContent.strAssociateName;
            objDPArr[4].Value = p_objContent.intType;
            objDPArr[5].Value = p_objContent.strDeptID;

            long lngEff = 0;

            long lngRes = 0;
            try
            {
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strAddNewAssociate, ref lngEff, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        /// <summary>
        /// 获取所有的关联字段
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllAssociate(string p_strDeptID, int p_intType, out clsTemplateSet_Associate[] p_objArr)
        {
            p_objArr = null;

            string strSql = "select distinct associateid,associatename from templateset_associate where deptid = ? and type = ? order by associatename";

            DataTable dtResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strDeptID;
                objDPArr[1].Value = p_intType;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    ArrayList arlTemp = new ArrayList();
                    clsTemplateSet_Associate[] m_objArr = new clsTemplateSet_Associate[dtResult.Rows.Count];
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        clsTemplateSet_Associate m_objContent = new clsTemplateSet_Associate();
                        m_objContent.strAssociateID = dtResult.Rows[i]["ASSOCIATEID"].ToString();
                        m_objContent.strAssociateName = dtResult.Rows[i]["ASSOCIATENAME"].ToString();
                        arlTemp.Add(m_objContent);
                    }
                    m_objArr = (clsTemplateSet_Associate[])arlTemp.ToArray(typeof(clsTemplateSet_Associate));
                    p_objArr = m_objArr;
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;

        }

        /// <summary>
        /// 根据套装模板取相应类别（病名或手术名称）的关联字段
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSetID"></param>
        /// <param name="p_intType"></param>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAssociateBySetID(string p_strDeptID, string p_strSetID, int p_intType, out clsTemplateSet_Associate p_objValue)
        {
            p_objValue = null;

            string strSql = clsDatabaseSQLConvert.s_StrTop1 + @" associateid,associatename from templateset_associate 
where deptid = ? and templatesetid = ? and type = ? " + clsDatabaseSQLConvert.s_StrRownum;

            DataTable dtResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strDeptID;
                objDPArr[1].Value = p_strSetID;
                objDPArr[2].Value = p_intType;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count == 1)
                {
                    p_objValue = new clsTemplateSet_Associate();
                    p_objValue.strAssociateID = dtResult.Rows[0]["ASSOCIATEID"].ToString();
                    p_objValue.strAssociateName = dtResult.Rows[0]["ASSOCIATENAME"].ToString();
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;

        }

        private const string c_strGetAssociateIDBySetID = @"select associateid from templateset_associate
												where templatesetid = ? and type = ? and deptid = ?";

        [AutoComplete]
        public string m_strGetAssociateIDBySetID(string p_strSetID, int p_intType, string p_strDeptID)
        {
            clsHRPTableService objHRPServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
            new clsHRPTableService().CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strSetID;
            objDPArr[1].Value = p_intType;
            objDPArr[2].Value = p_strDeptID;

            DataTable dtResutl = new DataTable();

            long lngRes = 0;
            try
            {
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetAssociateIDBySetID, ref dtResutl, objDPArr);
                if (lngRes > 0 && dtResutl.Rows.Count == 1)
                {
                    return dtResutl.Rows[0][0].ToString().Trim();
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return "";
        }

        private const string c_strGetSpecialPatientTemplateSet = @"select a.control_id,b.template_name,b.content 
																from template_set_detail_02 a  
																left join template_detail b on(a.template_id=b.template_id)  
																where a.set_id=?
																and a.activity_date = (select max(activity_date) 
																from template_set_detail_02 where template_id=b.template_id) 
																order by a.control_id";


        private const string c_strGetSpecialPatientTemplateSetID = @"select templatesetid 
from templateset_associate a inner join
patient_associate b on a.associateid = b.associateid
where b.inpatientid=? and b.inpatientdate=? and a.formname = ? and a.type = ? and a.deptid = ?";


        /// <summary>
        /// 获取指定病人指定关联的套装模板
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_objArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSpecialPatientTemplateSet(string p_strInPatientID, string p_strInPatientDate, string p_strFormName, int p_intType, string p_strDeptID,
            out clsTemplatesetContentValue[][] p_objArr)
        {
            p_objArr = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strFormName;
                objDPArr[3].Value = p_intType;
                objDPArr[4].Value = p_strDeptID;

                DataTable dtResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetSpecialPatientTemplateSetID, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_objArr = new clsTemplatesetContentValue[dtResult.Rows.Count][];
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        IDataParameter[] objDPArr1 = null;//new Oracle.DataAccess.Client.OracleParameter[1];
                        objHRPServ.CreateDatabaseParameter(1, out objDPArr1);
                        //					objDPArr1[0] = new Oracle.DataAccess.Client.OracleParameter();
                        objDPArr1[0].Value = dtResult.Rows[i][0].ToString().Trim();
                        DataTable dtResult1 = new DataTable();
                        long lngRes1 = objHRPServ.lngGetDataTableWithParameters(c_strGetSpecialPatientTemplateSet, ref dtResult1, objDPArr1);
                        if (lngRes1 > 0 && dtResult1.Rows.Count > 0)
                        {
                            p_objArr[i] = new clsTemplatesetContentValue[dtResult1.Rows.Count];
                            for (int j = 0; j < p_objArr[i].Length; j++)
                            {
                                p_objArr[i][j] = new clsTemplatesetContentValue();
                                p_objArr[i][j].m_strControl_ID = dtResult1.Rows[j]["CONTROL_ID"].ToString().Trim();
                                p_objArr[i][j].m_strTemplate_Name = dtResult1.Rows[j]["TEMPLATE_NAME"].ToString().Trim();
                                p_objArr[i][j].m_strContent = dtResult1.Rows[j]["CONTENT"].ToString().TrimEnd();
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

            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;

        }

        [AutoComplete]
        public string m_strGetAssociateIDByAssociateName(string p_strAssociateName, int p_intType, string p_strDeptID)
        {
            string strSql = clsDatabaseSQLConvert.s_StrTop1 + @" associateid
                                                from templateset_associate
                                                where (associatename = ?) and (type = ?) and (deptid = ?)" + clsDatabaseSQLConvert.s_StrRownum;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            new clsHRPTableService().CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strAssociateName;
            objDPArr[1].Value = p_intType;
            objDPArr[2].Value = p_strDeptID;

            DataTable dtResult = new DataTable();

            long lngRes = 0;

            try
            {
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            if (lngRes > 0 && dtResult.Rows.Count == 1)
                return dtResult.Rows[0][0].ToString().Trim();
            return "";
        }

        /// <summary>
        /// 获取已有病名的病人的套装模板ID,在这里只取一个。
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetPatientHaveDisease_TemplateSetID(string p_strInPatientID, string p_strInPatientDate, string p_strFormName, int p_intType, string p_strDeptID)
        {
            clsHRPTableService objHRPServ = new clsHRPTableService();

            IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
            objHRPServ.CreateDatabaseParameter(5, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[2].Value = p_strFormName;
            objDPArr[3].Value = p_intType;
            objDPArr[4].Value = p_strDeptID;

            DataTable dtResult = new DataTable();

            string strSql = clsDatabaseSQLConvert.s_StrTop1 + @" templatesetid 
                                    from templateset_associate a inner join
                                    patient_associate b on a.associateid = b.associateid
                                    where b.inpatientid=? and b.inpatientdate=? and a.formname = ? and a.type = ? and a.deptid = ? order by a.associateid desc" + clsDatabaseSQLConvert.s_StrRownum;
            long lngRes = 0;
            try
            {
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
            }
            finally
            {
                //objHRPServ.Dispose();

            }


            if (lngRes > 0 && dtResult.Rows.Count == 1)
                return dtResult.Rows[0][0].ToString();
            return "";
        }

        private const string c_strAddNewPatient_Associate = @"insert into patient_associate
      (inpatientid, inpatientdate,associateid)
values (?,?,?)";

        [AutoComplete]
        private long m_lngAddPatient_Disease(clsPatient_Associate p_objContent)
        {

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objContent.strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_objContent.strInPatientDate);
                objDPArr[2].Value = p_objContent.strAssociateID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewPatient_Associate, ref lngEff, objDPArr);

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
        public long m_lngSavePatient_Associate(clsPatient_Associate p_objContent, int p_intType, string p_strDeptID)
        {

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;

                //查看某种类别的关联是否已有
                string strSql = @"select a.inpatientid, a.inpatientdate, a.associateid
  from patient_associate a
 inner join templateset_associate b on a.associateid = b.associateid
 where inpatientid = ?
   and inpatientdate = ?
   and b.type = ?
   and b.deptid = ?";

                DataTable dtExist = new DataTable();
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_objContent.strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_objContent.strInPatientDate);
                objDPArr[2].Value = p_intType;
                objDPArr[3].Value = p_strDeptID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtExist, objDPArr);

                if (lngRes <= 0) return lngRes;

                if (dtExist.Rows.Count > 0)
                {
                    //删除某种类别的关联
                    strSql = @"delete patient_associate
                                where 
                                inpatientid = ? 
                                and inpatientdate = ?
                                and associateid in
                                (select b.associateid from patient_associate a
                                inner join templateset_associate b on a.associateid = b.associateid
                                where inpatientid = ? 
                                and inpatientdate = ?
                                and b.type = ? and b.deptid = ?)";

                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = p_objContent.strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_objContent.strInPatientDate);
                    objDPArr[2].Value = p_objContent.strInPatientID;
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = DateTime.Parse(p_objContent.strInPatientDate);
                    objDPArr[4].Value = p_intType;
                    objDPArr[5].Value = p_strDeptID;

                    long lngEff = -1;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                }
                if (p_objContent.strAssociateID.Trim() != "")
                    lngRes = m_lngAddPatient_Disease(p_objContent);

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

        #endregion

        /// <summary>
        /// 获取套装模板内容
        /// </summary>
        [AutoComplete]
        public long m_lngGetTemplateSetValue(string p_strFormID, string p_strControl_ID, string p_strEmployeeID, string p_strDepartmentID, out clsTemplateSetValue[] p_objValue)
        {
            p_objValue = null;

            #region Sql
            string strSQL = @"select base.set_id,
       base.keyword,
       base.set_name,
       max(base.activity_date) activity_date
  from (select b.set_id, b.activity_date, b.keyword, c.set_name
          from tempate_set a
         inner join template_set_keyword b on (a.set_id = b.set_id)
         inner join template_set_detail_01 c on (a.set_id = c.set_id)
         inner join template_set_detail_02 d on (a.set_id = d.set_id)
         inner join template_detail f on (d.template_id = f.template_id)
          left join template_dept_visibility g on (d.template_id =
                                                  g.template_id)
         where b.activity_date in (select max(activity_date) activity_date
                                     from template_set_keyword
                                    group by set_id)
           and b.keyword_type = 1
           and a.end_date > ?
           and d.form_id = ?
           and d.control_id = ?
           and (f.visibility_level = 1 or
               (f.visibility_level = 0 and f.employeeid = ?) or
               (f.visibility_level = 2 and g.departmentid = ? and
               (select count(template_id)
                    from template_dept_visibility
                   where departmentid = ?) > 0))
         group by b.keyword, b.set_id, b.activity_date, c.set_name) base
 group by base.set_id, base.keyword, base.set_name
 order by base.keyword";
            #endregion
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strFormID;
                objDPArr[2].Value = p_strControl_ID;
                objDPArr[3].Value = p_strEmployeeID;
                objDPArr[4].Value = p_strDepartmentID;
                objDPArr[5].Value = p_strDepartmentID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplateSetValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplateSetValue();
                        p_objValue[i].m_strSet_ID = objDataTableResult.Rows[i]["Set_ID"].ToString();
                        p_objValue[i].m_strKeyword = objDataTableResult.Rows[i]["Keyword"].ToString().TrimEnd().Replace("き", "'");
                        p_objValue[i].m_strSet_Name = objDataTableResult.Rows[i]["Set_Name"].ToString().TrimEnd();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        }

        /// <summary>
        /// 获取套装模板内容,关键字模糊查找
        /// </summary>
        [AutoComplete]
        public long m_lngGetTemplateSetValue(string p_strFormID, string p_strControl_ID, string p_strLikeKeyword, string p_strEmployeeID, string p_strDepartmentID, out clsTemplateSetValue[] p_objValue)
        {
            p_objValue = null;
            #region Sql
            //            string strSQL = @"select base.Set_ID,base.Keyword,base.Set_Name,max(base.Activity_Date)  Activity_Date from
            //				(
            //				select  b.Set_ID,b.Activity_Date,b.Keyword,c.Set_Name From Tempate_Set a " + 
            //                " inner join Template_Set_Keyword b on(a.Set_ID=b.Set_ID) " + 
            //                " inner join Template_Set_Detail_01 c on(a.Set_ID=c.Set_ID) " + 
            //                " inner join Template_Set_Detail_02 d on(a.Set_ID=d.Set_ID) " + 
            //                " inner join Template_Detail f on(d.Template_ID=f.Template_ID) " + 
            //                " left join Template_Dept_Visibility  g on(d.Template_ID=g.Template_ID) " + 
            //                " where b.Activity_Date in " +
            //                " (select max(Activity_Date)  Activity_Date from Template_Set_Keyword group by set_id)" +
            //                " and b.Keyword_Type=1 and a.End_Date > "+clsDatabaseSQLConvert.s_StrGetServDateFuncName+ 
            //                " and d.Form_ID='" + p_strFormID + "' and d.Control_ID = '"+p_strControl_ID+"' and b.Keyword like '"+p_strLikeKeyword+"'" + 
            //                " and (f.Visibility_Level=1 or (f.Visibility_Level=0 and f.EmployeeID='" + p_strEmployeeID + "') " + 
            //                " or (f.Visibility_Level=2 and g.DepartmentID='"+p_strDepartmentID+"' and" + 
            //                " (select count(*) from Template_Dept_Visibility where DepartmentID='" + p_strDepartmentID + "')>0)) " + 				
            //                @"group by b.Keyword,b.Set_ID,b.Activity_Date,c.Set_Name) base
            //				group by base.Set_ID,base.Keyword,base.Set_Name
            //				order by base.Keyword";

            string strSQL = @"select base.set_id,
       base.keyword,
       base.set_name,
       max(base.activity_date) activity_date
  from (select b.set_id, b.activity_date, b.keyword, c.set_name
          from tempate_set a
         inner join template_set_keyword b on (a.set_id = b.set_id)
         inner join template_set_detail_01 c on (a.set_id = c.set_id)
         inner join template_set_detail_02 d on (a.set_id = d.set_id)
         inner join template_detail f on (d.template_id = f.template_id)
          left join template_dept_visibility g on (d.template_id =
                                                  g.template_id)
         where (b.set_id, b.activity_date, b.keyword) in
               (select set_id, max(activity_date) activity_date, keyword
                  from template_set_keyword
                 where set_id = a.set_id
                   and keyword like ?
                   and keyword_type = 1
                 group by set_id, keyword)
           and b.keyword_type = 1
           and a.end_date > " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @"
               and d.form_id = ?
           and d.control_id = ?
           and b.keyword like ?
           and (f.visibility_level = 1 or
               (f.visibility_level = 0 and f.employeeid = ?) or
               (f.visibility_level = 2 and g.departmentid = ? and
               (select count(template_id)
                    from template_dept_visibility
                   where departmentid = ?) > 0))
         group by b.keyword, b.set_id, b.activity_date, c.set_name) base
 group by base.set_id, base.keyword, base.set_name
 order by base.keyword";
            #endregion
            clsHRPTableService objHRPServ = new clsHRPTableService();
            DataTable objDataTableResult = new DataTable();

            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(7, out objDPArr);
            objDPArr[0].Value = p_strLikeKeyword;
            objDPArr[1].Value = p_strFormID;
            objDPArr[2].Value = p_strControl_ID;
            objDPArr[3].Value = p_strLikeKeyword;
            objDPArr[4].Value = p_strEmployeeID;
            objDPArr[5].Value = p_strDepartmentID;
            objDPArr[6].Value = p_strDepartmentID;

            long lngRes = 0;
            try
            {
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objValue = new clsTemplateSetValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objValue.Length; i++)
                    {
                        p_objValue[i] = new clsTemplateSetValue();
                        p_objValue[i].m_strSet_ID = objDataTableResult.Rows[i]["Set_ID"].ToString();
                        p_objValue[i].m_strKeyword = objDataTableResult.Rows[i]["Keyword"].ToString().TrimEnd().Replace("き", "'");
                        p_objValue[i].m_strSet_Name = objDataTableResult.Rows[i]["Set_Name"].ToString().TrimEnd();

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        }

        #region ICD10与模板
        /// <summary>
        /// 保存模板与疾病的关系
        /// </summary>
        /// <param name="p_strICD_ID"></param>
        /// <param name="p_strTempateSet_ID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveICD10_TemplateSet(string p_strICD_ID, string p_strTempateSet_ID)
        {

            string strSql = "";

            if (p_strTempateSet_ID == null || p_strTempateSet_ID.Trim().Length == 0)
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            long lngEff = -1;
            try
            {
                if (p_strICD_ID == null || p_strICD_ID.Trim().Length == 0)
                {
                    strSql = "delete from icd10_templateset where tempateset_id=?";
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = p_strTempateSet_ID;

                    return objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                }

                strSql = "delete from icd10_templateset where icd_id=? and tempateset_id=?";
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = long.Parse(p_strICD_ID);
                objDPArr[1].Value = p_strTempateSet_ID;

                objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

                strSql = "insert into icd10_templateset (icd_id,tempateset_id) values (?,?)";
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = long.Parse(p_strICD_ID);
                objDPArr[1].Value = p_strTempateSet_ID;

                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;

        }

        [AutoComplete]
        public long m_lngGetICD10_TemplateSet(string p_strTempateSet_ID, out System.Data.DataTable dtRecords)
        {
            dtRecords = new DataTable();
            string strSql = "";
            if (p_strTempateSet_ID == null || p_strTempateSet_ID.Trim().Length == 0)
                return -1;

            strSql = "select id,icd_name from ticd10 where id in(select icd_id from icd10_templateset where tempateset_id=?)";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strTempateSet_ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtRecords, objDPArr);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;

        }

        [AutoComplete]
        public long m_lngGetTemplateSet_ForICD10ID(string p_strICD_ID, string p_Form_ID, out System.Data.DataTable dtRecords)
        {
            dtRecords = new DataTable();
            string strSql = "";
            if (p_strICD_ID == null || p_strICD_ID.Trim().Length == 0)
                return -1;
            strSql = "select set_id,set_name from template_set_detail_01 where set_id in (select tempateset_id from icd10_templateset,template_set_detail_02 where tempateset_id=set_id and icd_id=? and form_id=?)";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strICD_ID;
                objDPArr[1].Value = p_Form_ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtRecords, objDPArr);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }
        #endregion


        #region 模板维护权限设置
        /// <summary>
        /// 根据员工流水号获取权限角色ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID">员工流水号</param>
        /// <param name="p_arrRoleID">权限角色ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoleIDByEmpID(string p_strEmpID, out List<string> p_arrRoleID)
        {
            long lngRes = -1;
            p_arrRoleID = new List<string>();

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","m_lngGetRoleIDByEmpID");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strSQL = @"select roleid_chr from t_sys_emprolemap where empid_chr=?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmpID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_arrRoleID.Add(dtbValue.Rows[i]["ROLEID_CHR"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string strEx = ex.Message;
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        /// <summary>
        /// 根据窗体名获取模块ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFormName">窗体名</param>
        /// <param name="strSearchType">查找类型(1:模糊查询,0:准确查询)</param>
        /// <param name="p_strModuleID">模块ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetModuleIDByFormName(string p_strFormName, string strSearchType, out string p_strModuleID)
        {
            p_strModuleID = "";
            long lngRes = -1;
            string strSQL = "";

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","m_lngGetModuleIDByFormName");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            switch (strSearchType.Trim())
            {
                case "0":
                    strSQL = @"select moduleid_chr from t_sys_module where name_vchr='" + p_strFormName + "'";
                    break;
                case "1":
                    strSQL = @"select moduleid_chr from t_sys_module where name_vchr like '" + p_strFormName + "%'";
                    break;
            }
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtbValue);
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_strModuleID = dtbValue.Rows[0]["MODULEID_CHR"].ToString();
                }
            }
            catch (Exception ex)
            {
                string strEx = ex.Message;
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        /// <summary>
        /// 根据模块ID获取权限角色ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModuleID">模块ID</param>
        /// <param name="p_arrRoleID">权限角色ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoleIDByModuleID(string p_strModuleID, out List<string> p_arrRoleID)
        {
            long lngRes = -1;
            p_arrRoleID = new List<string>();

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","m_lngGetRoleIDByModuleID");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strSQL = @"select roleid_chr from t_sys_rolemodulemap where moduleid_chr=?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strModuleID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_arrRoleID.Add(dtbValue.Rows[i]["ROLEID_CHR"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string strEx = ex.Message;
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        /// <summary>
        /// 根据模板ID查找
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTemplateID">模板ID</param>
        /// <param name="strEmpID">创建者ID</param>
        /// <param name="strVisibilityLevel">可视级别(0：创建者可视，1：公用，2：科室可视)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTemplateVLandEmpID(string p_strTemplateID, out string strEmpID, out string strVisibilityLevel, out string p_strActivityDate)
        {
            strEmpID = "";
            strVisibilityLevel = "";
            p_strActivityDate = "";
            string strSQL = @"select template_id,
       activity_date,
       template_name,
       content,
       employeeid,
       visibility_level,
       contentxml
  from template_detail
 where template_id = ?
   and activity_date =
       (select max(activity_date) from template_detail where template_id = ?)";

            DataTable dtResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strTemplateID;
                objDPArr[1].Value = p_strTemplateID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    strEmpID = dtResult.Rows[0]["EMPLOYEEID"].ToString();
                    strVisibilityLevel = dtResult.Rows[0]["VISIBILITY_LEVEL"].ToString();
                    p_strActivityDate = dtResult.Rows[0]["Activity_Date"].ToString();
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        }

        /// <summary>
        /// 根据模板ID查找可视的科室ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTemplateID">模板ID</param>
        /// <param name="p_arrTemplateID">可视的科室ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptIDByTemplateID(string p_strTemplateID, out List<string[]> p_arrDept)
        {
            long lngRes = -1;
            p_arrDept = new List<string[]>();
            if (p_strTemplateID == null || p_strTemplateID == "")
                return -1;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","m_lngGetDeptIDByTemplateID");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strSQL = @"select a.departmentid,b.deptname_vchr from template_dept_visibility a 
									left outer join t_bse_deptdesc b on a.departmentid = b.deptid_chr
									 where a.template_id=?";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strTemplateID.Trim();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    string[] objDept = null;
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        objDept = new string[2];
                        objDept[0] = dtbValue.Rows[i]["DEPARTMENTID"].ToString();
                        objDept[1] = dtbValue.Rows[i]["DEPTNAME_VCHR"].ToString();
                        p_arrDept.Add(objDept);
                    }
                }
            }
            catch (Exception ex)
            {
                string strEx = ex.Message;
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        /// <summary>
        /// 查找员工所在科室
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID">员工ID</param>
        /// <param name="p_arrTemplateID">可视的科室ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptIDByEmpID(string p_strEmpID, out List<string> p_arrDeptID)
        {
            long lngRes = -1;
            p_arrDeptID = new List<string>();

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTemplateService","m_lngGetDeptIDByEmpID");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;

            string strSQL = @"select deptid_chr from t_bse_deptemp where empid_chr=?";


            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                DataTable dtbValue = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmpID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_arrDeptID.Add(dtbValue.Rows[i]["DEPTID_CHR"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string strEx = ex.Message;
            }

            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        }

        /// <summary>
        /// 保存表TEMPLATE_DEPT_VISIBILITY
        /// </summary>
        /// <param name="p_objTemplate_Dept_Visiblity"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveTemplate_Dept_Visibility(clsTemplate_Dept_VisibilityValue[] p_objTemplate_Dept_Visiblity)
        {
            long lngRes = 0;
            if (p_objTemplate_Dept_Visiblity == null)
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                for (int i = 0; i < p_objTemplate_Dept_Visiblity.Length; i++)
                {
                    string strSQL = @"insert into template_dept_visibility(template_id,departmentid,activity_date)
						values(?,?,?)";

                    IDataParameter[] objDPArr = null;//new IDataParameter[3];
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_objTemplate_Dept_Visiblity[i].m_strTemplate_ID;
                    objDPArr[1].Value = p_objTemplate_Dept_Visiblity[i].m_strDepartmentID;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_objTemplate_Dept_Visiblity[i].m_strActivity_Date);

                    long lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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
        /// 删除表Template_Dept_Visibility的相关记录
        /// </summary>
        /// <param name="p_strTemplateID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteTemplate_Dept_Visibility(string p_strTemplateID)
        {
            long lngRes = 0;
            if (p_strTemplateID == "" || p_strTemplateID == null)
                return -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"delete from template_dept_visibility where template_id=?";
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strTemplateID.Trim();

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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
        /// 查找某张记录单下的所有模板
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_strKeyword"></param>
        /// <param name="p_objEmployeeTemplateSetValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetAllTemplateSetByForm(
            string p_strFormName, out clsEmployeeTemplateSetValue[] p_objEmployeeTemplateSetValue)
        {
            p_objEmployeeTemplateSetValue = null;

            #region SQL

            string strSQL = @"select distinct tsk.keyword, base.set_name, base.set_id
  from template_set_keyword tsk
 inner join (select tsd1.set_name, base.set_id
               from tempate_set ts
              inner join template_set_detail_01 tsd1 on ts.set_id =
                                                        tsd1.set_id
                                                    and ts.end_date > ?
              inner join (select distinct set_id
                           from template_set_detail_02 tsd2
                          inner join (select template_id
                                       from template_detail
                                      where activity_date in
                                            (select max(activity_date)
                                               from template_detail
                                              group by template_id)) base on tsd2.template_id =
                                                                             base.template_id
                                                                         and tsd2.form_id = ?
                          where activity_date in
                                (select max(activity_date)
                                   from template_set_detail_02
                                  group by set_id)) base on tsd1.set_id =
                                                            base.set_id
              where activity_date in (select max(activity_date)
                                        from template_set_detail_01
                                       group by set_id)) base on tsk.set_id =
                                                                 base.set_id
 where activity_date in
       (select max(activity_date) from template_set_keyword group by set_id)
 order by tsk.keyword, base.set_name";
            #endregion

            //查每张表时，如果里面有个Activity_Date,都必须查找最大修改时间的那个记录
            p_objEmployeeTemplateSetValue = null;
            DataTable objDataTableResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strFormName;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objEmployeeTemplateSetValue = new clsEmployeeTemplateSetValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objEmployeeTemplateSetValue.Length; i++)
                    {
                        p_objEmployeeTemplateSetValue[i] = new clsEmployeeTemplateSetValue();
                        p_objEmployeeTemplateSetValue[i].m_strSet_Name = objDataTableResult.Rows[i]["SET_NAME"].ToString().Trim();
                        p_objEmployeeTemplateSetValue[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objEmployeeTemplateSetValue[i].m_strKeyword = objDataTableResult.Rows[i]["KEYWORD"].ToString().Trim().Replace("き", "'");

                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return (lngRes);
        }
        #endregion

    }
    /// <summary>
    /// 统计疾病中间件
    /// </summary>

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsIllnessReportSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsIllnessReportSvc()
        { }

        [AutoComplete]
        public int m_mthFindIllnessByDateTime(string strBeginDate, string strEndDate, string strIllnessName)
        {
            DataTable dt = new DataTable();
            string strSQL = @"select inpatientid   
  from inhospitalmainrecord_content
 where opendate between ? and ?
   and status = 0
   and icd_10ofmain = ?";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(strBeginDate);
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strEndDate);
                objDPArr[2].Value = strIllnessName;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr);
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            if (lngRes > 0)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }

    }
}



