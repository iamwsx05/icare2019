using System;
using System.Data;
using weCare.Core.Entity;
using System.Collections;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility.SQLConvert;

namespace iCare.CustomFromService
{
    /// <summary>
    /// 最小元素集中间件 creat by gphuang 2004-11-25
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsMinElementColServ2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        #region 保存值
        /// <summary>
        /// 保存模板填写的数据
        /// </summary>
        /// <param name="p_objElementValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveTemplateData(clsMinElementValues p_objElementValue)
        {
            if (p_objElementValue == null) return -1;
            if (p_objElementValue.m_objElementValueArr == null || p_objElementValue.m_objElementValueArr.Length <= 0)
                return -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;

            try
            {
                //Delete Old
                string strSql = @"delete min_elementcol_valuemain
 where registerid_chr= ?
 and CREATEDDATE_DAT = ?
 and formid_vchr = ?
 and SOURCECONTROLID_VCHR = ?
 and TEMPLATEID_CHR = ?";//5
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_objElementValue.m_strRegisterId;
                objDPArr[1].DbType = DbType.Date;
                objDPArr[1].Value = p_objElementValue.m_dtmCreatedDate;
                objDPArr[2].Value = p_objElementValue.m_strFormClsName;
                objDPArr[3].Value = p_objElementValue.m_strSourceControlName;
                objDPArr[4].Value = p_objElementValue.m_strTemplateId;
                long lngRef = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRef, objDPArr);
                string strId = "-1";
                lngRes = m_lngGetNextValueId("seq_emr_minelement",out strId);
                if (lngRes <= 0 || strId == "-1") return - 1;
                strSql = @"insert into min_elementcol_valuemain
  (valueid_int,
   registerid_chr,
   CREATEDDATE_DAT,
   formid_vchr,
   SOURCECONTROLID_VCHR,
   TEMPLATEID_CHR)
values
  (?, ?, ?, ?, ?, ?)";//6
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = strId;
                objDPArr[1].Value = p_objElementValue.m_strRegisterId;
                objDPArr[2].DbType = DbType.Date;
                objDPArr[2].Value = p_objElementValue.m_dtmCreatedDate;
                objDPArr[3].Value = p_objElementValue.m_strFormClsName;
                objDPArr[4].Value = p_objElementValue.m_strSourceControlName;
                objDPArr[5].Value = p_objElementValue.m_strTemplateId;
                lngRef = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRef, objDPArr);
                if (lngRes <= 0) return lngRes;

                strSql = @"insert into min_elementcol_valuesub
  (valueid_int, controlid_vchr, controlvalue_vchr)
values
  (?, ?, ?)";
                DbType[] dbTypes = new DbType[] {  DbType.String, DbType.String, DbType.String };
                object[][] objValues = new object[3][];
                if (p_objElementValue.m_objElementValueArr.Length > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_objElementValue.m_objElementValueArr.Length];//初始化
                    }

                    for (int k1 = 0; k1 < p_objElementValue.m_objElementValueArr.Length; k1++)
                    {
                        int intIndex = 0;
                        objValues[intIndex++][k1] = strId;
                        objValues[intIndex++][k1] = p_objElementValue.m_objElementValueArr[k1].m_strCONTROL_ID;
                        objValues[intIndex++][k1] = p_objElementValue.m_objElementValueArr[k1].m_strCONTROL_VALUE;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSql, objValues, dbTypes);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 保存主表信息
        /// </summary>
        /// <param name="p_objTextTemplate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveApplyInfo(clsTextTemplate p_objTextTemplate)
        {
            if (p_objTextTemplate == null)
                return 0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;

            try
            {
                string strSql = @"delete from  Min_Element_Apply where GUI_ID=? and Form_ID=? and Control_ID=?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objTextTemplate.m_strGUI_ID;
                objDPArr[1].Value = p_objTextTemplate.m_strFORM_ID;
                objDPArr[2].Value = p_objTextTemplate.m_strCONTROL_ID;

                long lngRef = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRef, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                strSql = @"Insert into Min_Element_Apply(Form_ID,Control_ID,Gui_ID,Status,DOCTOR_ID)
								values(?,?,?,0,?)";
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_objTextTemplate.m_strFORM_ID;
                objDPArr[1].Value = p_objTextTemplate.m_strCONTROL_ID;
                objDPArr[2].Value = p_objTextTemplate.m_strGUI_ID;
                objDPArr[3].Value = p_objTextTemplate.m_strDoctor_ID;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRef, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }

        #endregion

        #region 保存模板信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTIVO"></param>
        /// <param name="p_intVisbleLevel"></param>
        /// <param name="p_strDeptArr"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveTemplate(clsTemplateInfo objTIVO, int p_intVisbleLevel, string[] p_strDeptArr, out string strID)
        {
            long lngRes = 0;
            strID = "-1";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = m_lngGetNextValueId("seq_emr_minelement", out strID);
                if (strID == "-1") return 0;
                string strSQL = "insert into MIN_ELEMENTCOL_GUI(TEMPLATE_ID,TEMPLATE_NAME,TEMPLATE_XML,VISBLELEVEL)values(?,?,?,?) ";
                    IDataParameter[] objDPArr = null;

                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].Value = strID;
                    objDPArr[1].Value = objTIVO.m_strTEMPLATE_NAME;
                    objDPArr[2].Value = System.Text.Encoding.Unicode.GetBytes(objTIVO.m_strTEMPLATE_XML);
                    objDPArr[3].Value = p_intVisbleLevel;
                    long lngAff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAff, objDPArr);
                    if (lngRes > 0 && p_intVisbleLevel == 2 && p_strDeptArr.Length > 0)
                    {
                        strSQL = @"insert into MIN_ELEMENT_DEPT_VISBLE(TEMPLATE_ID,DEPTID) values(?,?)";
                        if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                        {
                            for (int i = 0; i < p_strDeptArr.Length; i++)
                            {
                                objDPArr = null;
                                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                                objDPArr[0].Value = strID;
                                objDPArr[1].Value = p_strDeptArr[i].Trim();
                                try
                                {
                                    objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAff, objDPArr);
                                }
                                catch { continue; }
                            }
                        }
                        else
                        {
                            DbType[] dbTypes = new DbType[] { DbType.String, DbType.String };
                            object[][] objValues = new object[2][];
                            for (int j = 0; j < objValues.Length; j++)
                            {
                                objValues[j] = new object[p_strDeptArr.Length];//初始化
                            }

                            for (int k1 = 0; k1 < p_strDeptArr.Length; k1++)
                            {
                                objValues[0][k1] = strID;
                                objValues[1][k1] = p_strDeptArr[k1].Trim();
                            }
                            lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes); 
                        }
                    }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region 更新模板
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTIVO"></param>
        /// <param name="p_intVisbleLevel"></param>
        /// <param name="p_strDeptArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateTemplate(clsTemplateInfo objTIVO, int p_intVisbleLevel, string[] p_strDeptArr)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            string strSQL = "";
            IDataParameter[] objDPArr = null;
            long lngAff = 0;
            try
            {
                if (p_intVisbleLevel >= 0 && p_intVisbleLevel <= 2)
                {
                    strSQL = "update MIN_ELEMENTCOL_GUI set TEMPLATE_NAME =?,TEMPLATE_XML =?,VISBLELEVEL = ? where TEMPLATE_ID =?";

                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].Value = objTIVO.m_strTEMPLATE_NAME;
                    objDPArr[1].Value = System.Text.Encoding.Unicode.GetBytes(objTIVO.m_strTEMPLATE_XML);

                    objDPArr[2].Value = p_intVisbleLevel;
                    objDPArr[3].Value = objTIVO.m_strTEMPLATE_ID;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAff, objDPArr);
                }
                else
                {
                    strSQL = "update MIN_ELEMENTCOL_GUI set TEMPLATE_NAME =?,TEMPLATE_XML =? where TEMPLATE_ID =?";

                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = objTIVO.m_strTEMPLATE_NAME;
                    objDPArr[1].Value = System.Text.Encoding.Unicode.GetBytes(objTIVO.m_strTEMPLATE_XML);
                    objDPArr[2].Value = objTIVO.m_strTEMPLATE_ID;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAff, objDPArr);
                }
                if (lngRes > 0 && p_intVisbleLevel != -1)
                {
                    strSQL = @"delete from MIN_ELEMENT_DEPT_VISBLE where TEMPLATE_ID = ?";
                    objDPArr = null; 
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = objTIVO.m_strTEMPLATE_ID;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAff, objDPArr);

                    if (lngRes > 0 && p_intVisbleLevel == 2 && p_strDeptArr.Length > 0)
                    {
                        strSQL = @"insert into MIN_ELEMENT_DEPT_VISBLE(TEMPLATE_ID,DEPTID) values(?,?')";
                        if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                        {
                            for (int i = 0; i < p_strDeptArr.Length; i++)
                            {
                                objDPArr = null;
                                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                                objDPArr[0].Value = objTIVO.m_strTEMPLATE_ID;
                                objDPArr[1].Value = p_strDeptArr[i].Trim();
                                try
                                {
                                    objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAff, objDPArr);
                                }
                                catch { continue; }
                            }
                        }
                        else
                        {
                            DbType[] dbTypes = new DbType[] { DbType.String, DbType.String };
                            object[][] objValues = new object[2][];
                            for (int j = 0; j < objValues.Length; j++)
                            {
                                objValues[j] = new object[p_strDeptArr.Length];//初始化
                            }

                            for (int k1 = 0; k1 < p_strDeptArr.Length; k1++)
                            {
                                objValues[0][k1] = objTIVO.m_strTEMPLATE_ID;
                                objValues[1][k1] = p_strDeptArr[k1].Trim();
                            }
                            lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region 停用模板
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strTemplate_ID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngHaltTemplate(string p_strTemplate_ID)
        {
            long lngRes = 0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = "update MIN_ELEMENT_APPLY set STATUS = 1 where GUI_ID = ?";
                IDataParameter[] objDPArr = null;
                long lngAff = 0;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strTemplate_ID;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region 复制模版
        /// <summary>
        /// 复制模版
        /// </summary>
        /// <param name="objTemplate"></param>
        /// <param name="intVisbleLevel"></param>
        /// <param name="strDeptArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCopyTemplate(clsTemplateInfo objTemplate,int intVisbleLevel,string[] strDeptArr)
        {
            if (objTemplate == null)
                return -1;

            string strId = null;
            string sqlQuery = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRef = 0;

            try
            {
                //获取下一个可用的模版编号
                lngRef = m_lngSaveTemplate(objTemplate, intVisbleLevel, strDeptArr, out strId);

                if (lngRef > 0 && !string.IsNullOrEmpty(strId))
                {
                    DataTable dtbApplyItem = new DataTable();
                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = objTemplate.m_strTEMPLATE_ID;//这里必须使用复制源模版编号
                    sqlQuery = "select form_id,control_id,status,doctor_id from min_element_apply where gui_id = ? ";

                    //这里必须使用复制源模版编号
                    lngRef = objHRPServ.lngGetDataTableWithParameters(sqlQuery, ref dtbApplyItem, objDPArr);
                    if (lngRef > 0 && dtbApplyItem.Rows.Count > 0)
                    {
                        DataRow objRow = dtbApplyItem.Rows[0];

                        sqlQuery = "Insert into Min_Element_Apply(Form_ID,Control_ID,Gui_ID,Status,DOCTOR_ID) values(?,?,?,0,?)";
                        objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                        objDPArr[0].Value = objRow["Form_ID"].ToString();
                        objDPArr[1].Value = objRow["Control_ID"].ToString();
                        objDPArr[2].Value = strId;
                        objDPArr[3].Value = objTemplate.m_strDoctor_ID;
                        lngRef = objHRPServ.lngExecuteParameterSQL(sqlQuery, ref lngRef, objDPArr);
                    }
                    else
                        throw new Exception("复制不成功。");
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRef;
        }
        #endregion

        #region 根据窗口ID，控件ID查找模板。
        /// <summary>
        ///  获取指定控件得相关模板
        /// （保留以适应旧的接口）
        /// </summary>
        /// <param name="strFormID"></param>
        /// <param name="strControlID"></param>
        /// <param name="p_strDoctor_ID"></param>
        /// <param name="p_strDeptArr"></param>
        /// <param name="arrTemplate"></param>
        /// <param name="isUseAll"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTemplates(string strFormID, string strControlID, string p_strDoctor_ID, string[] p_strDeptArr, out clsTemplateInfo[] arrTemplate, bool isUseAll)
        {
            arrTemplate = null;

            long lngRes = 0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //			string strSQL=@"SELECT min_elementcol_gui.*,min_element_apply.doctor_id
                //  FROM min_element_apply INNER JOIN min_elementcol_gui ON min_element_apply.gui_id = min_elementcol_gui.template_id
                // WHERE ltrim(rtrim(min_element_apply.form_id)) = '"+strFormID+"' AND ltrim(rtrim(min_element_apply.control_id)) = '"+strControlID+"' and min_element_apply.status = 0";
                string strWhere = "";
                //if (isUseAll)
                //{
                //    strWhere = " and b.visblelevel = '1' and rtrim(a.doctor_id) = '" + p_strDoctor_ID + @"'";
                //}
                //else
                //{
                    strWhere = " and  (b.visblelevel = 0 or (b.visblelevel = 1 and a.doctor_id = '" + p_strDoctor_ID + @"') or (b.visblelevel = 2 and b.template_id in
 (select distinct c.template_id from MIN_ELEMENT_DEPT_VISBLE c where [<DEPTARR>])))";
                //}
                string strSQL = @"SELECT b.*,a.doctor_id  FROM min_element_apply a INNER JOIN min_elementcol_gui b ON a.gui_id = b.template_id
 WHERE a.form_id = '" + strFormID + @"' AND a.control_id = '" + strControlID + @"' and a.status = 0" + strWhere;

                string strDeptSql = "";
                if (p_strDeptArr != null && p_strDeptArr.Length > 0)
                {
                    strDeptSql = " c.deptid = '" + p_strDeptArr[0].Trim() + "' ";
                    for (int i = 1 ; i < p_strDeptArr.Length ; i++)
                    {
                        strDeptSql += " or c.deptid = '" + p_strDeptArr[i].Trim() + "' ";
                    }
                }
                else
                    strDeptSql = " c.deptid = '' ";
                strSQL = strSQL.Replace("[<DEPTARR>]", strDeptSql);

                //排序，使模板列表按科室规类(降序)
                strSQL = strSQL + " order by b.template_name desc";

                DataTable dt = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    arrTemplate = new clsTemplateInfo[dt.Rows.Count];
                    for (int i = 0 ; i < arrTemplate.Length ; i++)
                    {
                        arrTemplate[i] = new clsTemplateInfo();
                        arrTemplate[i].m_strTEMPLATE_ID = dt.Rows[i]["TEMPLATE_ID"].ToString().Trim();
                        arrTemplate[i].m_strTEMPLATE_NAME = dt.Rows[i]["TEMPLATE_NAME"].ToString().Trim();
                        arrTemplate[i].m_strTEMPLATE_XML = System.Text.Encoding.Unicode.GetString((byte[])(dt.Rows[i]["TEMPLATE_XML"]));
                        arrTemplate[i].m_strDoctor_ID = dt.Rows[i]["DOCTOR_ID"].ToString().Trim();
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
        /// 获取指定控件得相关模板
        /// 有权限控制,员工可见
        /// SQL经过优化
        /// </summary>
        /// <param name="strFormID"></param>
        /// <param name="strControlID"></param>
        /// <param name="p_strDoctor_ID"></param>
        /// <param name="arrTemplate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTemplates(string strFormID, string strControlID, string p_strDoctor_ID, out clsTemplateInfo[] arrTemplate)
        {
            arrTemplate = null;
            if (string.IsNullOrEmpty(strFormID) || string.IsNullOrEmpty(strControlID) || string.IsNullOrEmpty(p_strDoctor_ID))
                return -1;
            long lngRes = 0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select *
  from (SELECT b.*, a.doctor_id
          FROM min_element_apply a
         INNER JOIN min_elementcol_gui b ON a.gui_id = b.template_id
         WHERE a.form_id = ?
           AND a.control_id = ?
           and a.status = 0
           and (b.visblelevel = '0' or
               (b.visblelevel = '1' and a.doctor_id = ?))
        union all
        SELECT b.*, a.doctor_id
          FROM min_element_apply a
         INNER JOIN min_elementcol_gui b ON a.gui_id = b.template_id
         inner join (select distinct template_id
                       from MIN_ELEMENT_DEPT_VISBLE v
                      inner join t_bse_deptemp d on d.deptid_chr = v.deptid
                      inner join t_bse_employee e on d.empid_chr =
                                                     e.empid_chr
                      where e.empno_chr = ?) t on t.template_id =
                                                       b.template_id
         WHERE a.form_id = ?
           AND a.control_id = ?
           and a.status = 0
           and b.visblelevel = '2')
 order by template_name desc";//6

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = strFormID;
                objDPArr[1].Value = strControlID;
                objDPArr[2].Value = p_strDoctor_ID.Trim();
                objDPArr[3].Value = p_strDoctor_ID.Trim();
                objDPArr[4].Value = strFormID;
                objDPArr[5].Value = strControlID;
                DataTable dt = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr);
                int intRowCount = dt.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    arrTemplate = new clsTemplateInfo[intRowCount];
                    DataRow objRow = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        clsTemplateInfo objTemplate = new clsTemplateInfo();
                        objRow = dt.Rows[i];
                        objTemplate.m_strTEMPLATE_ID = objRow["TEMPLATE_ID"].ToString().Trim();
                        objTemplate.m_strTEMPLATE_NAME = objRow["TEMPLATE_NAME"].ToString().Trim();
                        objTemplate.m_strTEMPLATE_XML = System.Text.Encoding.Unicode.GetString((byte[])(objRow["TEMPLATE_XML"]));
                        objTemplate.m_strDoctor_ID = objRow["DOCTOR_ID"].ToString().Trim();
                        arrTemplate[i] = objTemplate;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }


        /// <summary>
        ///  获取模板有描述的控件
        /// </summary>
        /// <param name="strTemplateID"></param>
        /// <param name="arrItems"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTemplateControls(string p_strTemplateID, out clsTemplateControlValue[] arrItems)
        {
            arrItems = null;
            if (string.IsNullOrEmpty(p_strTemplateID)) return -1;
            string strSql = @"select Control_ID,Control_Desc  from MIN_ELEMENT_DESC where TEMPLATE_ID=?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strTemplateID;
                DataTable dt = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dt, objDPArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    arrItems = new clsTemplateControlValue[dt.Rows.Count];
                    DataRow objRow = null;
                    for (int i = 0 ; i < arrItems.Length ; i++)
                    {
                        clsTemplateControlValue objItem = new clsTemplateControlValue();
                        objRow = dt.Rows[i];
                        objItem.m_strGUI_ID = p_strTemplateID;
                        objItem.m_strCONTROL_ID = objRow["CONTROL_ID"].ToString();
                        objItem.m_strCONTROL_VALUE = "";
                        objItem.m_strCONTROL_DESC = objRow["CONTROL_DESC"].ToString();
                        arrItems[i] = objItem;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 保存控件描述
        /// </summary>
        /// <param name="p_objTextTemplate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveTemplateDesc(clsTextTemplate p_objTextTemplate)
        {
            if (p_objTextTemplate == null)
                return 0;
            if (string.IsNullOrEmpty(p_objTextTemplate.m_strGUI_ID)|| p_objTextTemplate.m_objTmpCtlValueArr == null)
                return 0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                string strDelete = @"delete from min_element_desc where template_id = ?";
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objTextTemplate.m_strGUI_ID;
                long lngRef = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strDelete, ref lngRef, objDPArr);
                string strSql = @"INSERT INTO min_element_desc (template_id, control_id, control_desc) VALUES (?, ?, ? )";
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_objTextTemplate.m_objTmpCtlValueArr.Length; i++)
                    {
                        objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].Value = p_objTextTemplate.m_strGUI_ID;
                        objDPArr[0].Value = p_objTextTemplate.m_objTmpCtlValueArr[i].m_strCONTROL_ID;
                        objDPArr[0].Value = p_objTextTemplate.m_objTmpCtlValueArr[i].m_strCONTROL_DESC;
                        lngRef = 0;
                        objHRPServ.lngExecuteParameterSQL(strSql, ref lngRef, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.String };
                    object[][] objValues = new object[3][];
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_objTextTemplate.m_objTmpCtlValueArr.Length];//初始化
                    }

                    for (int k1 = 0; k1 < p_objTextTemplate.m_objTmpCtlValueArr.Length; k1++)
                    {
                        objValues[0][k1] = p_objTextTemplate.m_strGUI_ID;
                        objValues[1][k1] = p_objTextTemplate.m_objTmpCtlValueArr[k1].m_strCONTROL_ID;
                        objValues[2][k1] = p_objTextTemplate.m_objTmpCtlValueArr[k1].m_strCONTROL_DESC;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSql, objValues, dbTypes);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return 1;
        }


        #endregion

        #region  temp
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrGuiID"></param>
        /// <param name="arrCtrlName"></param>
        /// <param name="arrCtrlValue"></param>
        /// <param name="arrValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPatientTemplateText(string[] arrGuiID, string[] arrCtrlName, string[] arrCtrlValue, out clsTextTemplate[] arrValue)
        {
            arrValue = new clsTextTemplate[0];
            if (arrGuiID == null || arrGuiID.Length <= 0) return 1;

            string strSql = strGetSql(arrGuiID[0], arrCtrlName[0], arrCtrlValue[0], 0);
            for (int i = 1 ; i < arrGuiID.Length ; i++)
            {
                //				strSql=strSql + " intersect " + strGetSql(arrGuiID[i],arrCtrlName[i],arrCtrlValue[i]);
                string strIndexF = "i" + Convert.ToString(i - 1);
                string strIndexN = "i" + i.ToString();
                strSql = strSql + " and exists ( " + strGetSql(arrGuiID[i], arrCtrlName[i], arrCtrlValue[i], i) + " and " + strIndexF
                    + "templateid_chr = " + strIndexN + ".templateid_chr and " + strIndexF + ".inpatientid = " + strIndexN + ".inpatientid and " + strIndexF + ".inpatientdate = " + strIndexN + ".inpatientdate)";
            }

            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                DataTable objDT = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSql, ref objDT);
                if ((lngRes > 0) && (objDT.Rows.Count > 0))
                {
                    arrValue = new clsTextTemplate[objDT.Rows.Count];
                    for (int i = 0 ; i < arrValue.Length ; i++)
                    {
                        arrValue[i] = new clsTextTemplate();
                        arrValue[i].m_strInPatientID = ToStr(objDT.Rows[i]["INPATIENTID"]);
                        arrValue[i].m_dtInPatientDate = ToDT(objDT.Rows[i]["INPATIENTDATE"]);
                        arrValue[i].m_dtOpenDate = ToDT(objDT.Rows[i]["OPENDATE"]);
                    }
                }
                else
                {
                    arrValue = null;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }

        private string strGetSql(string strGuiID, string strCtrlName, string strCtrlValue, int p_intIndex)
        {
            string strIndex = "i" + p_intIndex.ToString();
            string strSql = @"select "
                + strIndex + @".hisinpatientid_chr inpatientid, "
                + strIndex + @".hisinpatientdate inpatientdate, "
                + strIndex + @"1.templateid_chr, " 
                + strIndex + @"1.valueid_int  from min_elementcol_valuemain " 
                + strIndex + @"1 inner join t_bse_hisemr_relation " 
                + strIndex + @" on " 
                + strIndex + @"1.registerid_chr = "
                + strIndex + @".registerid_chr inner join min_elementcol_valuesub " 
                + strIndex + @"2 on " 
                + strIndex + @"1.valueid_int = " 
                + strIndex + @"2.valueid_int where " 
                + strIndex + "2.controlid_vchr = '[CTRLNAME]' and " 
                + strIndex + "1.templateid_chr = '[GUIID]' and " 
                + strIndex + "2.controlvalue_vchr like '%[CTRLVALUE]%'";
            strSql = strSql.Replace("[GUIID]", strGuiID);
            strSql = strSql.Replace("[CTRLNAME]", strCtrlName);
            strSql = strSql.Replace("[CTRLVALUE]", strCtrlValue);
            return strSql;
        }


        private string ToStr(object objValue)
        {
            try
            {
                if (objValue == null)
                    return "";
                else
                    return Convert.ToString(objValue);
            }
            catch (Exception)
            {
                return "";
            }
        }

        private DateTime ToDT(object objValue)
        {
            try
            {
                return Convert.ToDateTime(objValue);
            }
            catch (Exception)
            {
                return DateTime.Parse("1900-1-1");
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objElementValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetValue(ref clsMinElementValues p_objElementValue)
        {
            if (p_objElementValue == null) return -1;
            if (string.IsNullOrEmpty(p_objElementValue.m_strRegisterId) || string.IsNullOrEmpty(p_objElementValue.m_strFormClsName)
                || string.IsNullOrEmpty(p_objElementValue.m_strSourceControlName) || string.IsNullOrEmpty(p_objElementValue.m_strTemplateId))
                return -1;


            string strSql = @"select b.*
  from min_elementcol_valuemain a
 inner join min_elementcol_valuesub b on a.valueid_int = b.valueid_int
 where a.registerid_chr = ?
 and a.createddate_dat = ?
 and a.formid_vchr = ?
 and a.sourcecontrolid_vchr = ?
 and a.templateid_chr = ?
 and a.status_int = 1";//5

            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_objElementValue.m_strRegisterId;
                objDPArr[1].DbType = DbType.Date;
                objDPArr[1].Value = p_objElementValue.m_dtmCreatedDate;
                objDPArr[2].Value = p_objElementValue.m_strFormClsName;
                objDPArr[3].Value = p_objElementValue.m_strSourceControlName;
                objDPArr[4].Value = p_objElementValue.m_strTemplateId;
                long lngRef = 0;
                DataTable dtValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                int intCount = dtValue.Rows.Count;
                if (lngRes > 0 && intCount > 0)
                {
                    DataRow objRow = null;
                    p_objElementValue.m_objElementValueArr = new clsElementValue[intCount];
                    objRow = dtValue.Rows[0];
                    int intTmp = 0;
                    int.TryParse(objRow["valueid_int"].ToString(), out intTmp);
                    p_objElementValue.m_intValuesId = intTmp;
                    for (int i = 0 ; i < dtValue.Rows.Count ; i++)
                    {
                        clsElementValue obj = new clsElementValue();
                        objRow = dtValue.Rows[i];
                        obj.m_strCONTROL_ID = objRow["controlid_vchr"].ToString();
                        obj.m_strCONTROL_VALUE = objRow["controlvalue_vchr"].ToString();
                        p_objElementValue.m_objElementValueArr[i] = obj;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 获取指定控件的相关模板，不包含模板的XML信息
        /// </summary>
        /// <param name="strFormID"></param>
        /// <param name="strControlID"></param>
        /// <param name="arrTemplate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTemplateName(string strFormID, string strControlID, out clsTemplateInfo[] arrTemplate)
        {
            arrTemplate = null;

            long lngRes = 0;
            string strSQL = @"select mg.template_id,mg.template_name 
from MIN_ELEMENT_APPLY ma inner join MIN_ELEMENTCOL_GUI mg
on ma.gui_id = mg.template_id where ma.FORM_ID = ? 
and ma.CONTROL_ID = ? and ma.status = '0'";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = strFormID;
                objDPArr[1].Value = strControlID;
                long lngRef = 0;
                DataTable dt = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    arrTemplate = new clsTemplateInfo[dt.Rows.Count];
                    for (int i = 0 ; i < arrTemplate.Length ; i++)
                    {
                        arrTemplate[i] = new clsTemplateInfo();
                        arrTemplate[i].m_strTEMPLATE_ID = dt.Rows[i]["TEMPLATE_ID"].ToString().Trim();
                        arrTemplate[i].m_strTEMPLATE_NAME = dt.Rows[i]["TEMPLATE_NAME"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
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
        /// <param name="p_strTemplateID"></param>
        /// <param name="p_intVisble"></param>
        /// <param name="p_strDeptArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptVisble(string p_strTemplateID, ref int p_intVisble, out string[] p_strDeptArr)
        {
            p_strDeptArr = null;
            if (string.IsNullOrEmpty(p_strTemplateID))
                return -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                string strSql = @" select t.visblelevel from min_elementcol_gui t where t.template_id = ?";
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strTemplateID;
                long lngRef = 0;
                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    try
                    {
                        p_intVisble = int.Parse(dtbResult.Rows[0]["VISBLELEVEL"].ToString());
                    }
                    catch { p_intVisble = 0; }
                    if (p_intVisble == 2)
                    {
                        strSql = @"select t.deptid from MIN_ELEMENT_DEPT_VISBLE t where t.template_id = ? ";
                        objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = p_strTemplateID;
                        lngRef = 0;
                        dtbResult = new DataTable();
                        lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr);
                        if (lngRes > 0 && dtbResult.Rows.Count > 0)
                        {
                            p_strDeptArr = new string[dtbResult.Rows.Count];
                            for (int i = 0 ; i < dtbResult.Rows.Count ; i++)
                            {
                                p_strDeptArr[i] = dtbResult.Rows[i]["DEPTID"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 获取序列
        /// </summary>
        /// <param name="p_strSeqName"></param>
        /// <param name="p_strId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetNextValueId(string p_strSeqName,out string p_strId)
        {
            p_strId = "-1";
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strSeqName)) return -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSql = @"select " + p_strSeqName + ".NEXTVAL from dual";
                DataTable dt = new DataTable();
                long lngRef = 0;
                lngRes = objHRPServ.DoGetDataTable(strSql, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    p_strId = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            return lngRes;
        }
        /// <summary>
        /// 更新模板名称
        /// </summary>
        /// <param name="p_strTempId">模板ID</param>
        /// <param name="p_strTemplateName">新的模板名称</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateTemplateName(string p_strTempId, string p_strTemplateName)
        {
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strTempId) || string.IsNullOrEmpty(p_strTemplateName)) return -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSql = @"update min_elementcol_gui set template_name = ? where template_id = ?";
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strTemplateName;
                objDPArr[1].Value = p_strTempId;
                long lngRef = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRef,objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
    }
}
