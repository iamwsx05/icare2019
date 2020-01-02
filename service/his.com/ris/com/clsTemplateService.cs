using System;
using System.Collections;
using System.Collections.Generic;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.iCare.middletier.RIS
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsTemplateService : clsMiddleTierBase
    {
        #region Fields
        // Fields
        private const string c_strAddTemplate = @"insert into Template_Item
  (Template_ID,
   Content,
   ContentXml,
   TemplateSet_ID,
   Form_ID,
   Control_ID,
   Order_No)
values
  (?, ?, ?, ?, ?, ?, ?)";

        private const string c_strCreateTemplateSet = @"insert into TemplateSet
  (Set_ID,
   Start_Date,
   End_Date,
   Set_Name,
   Visiblity_Level,
   Employee_ID,
   Keyword,
   Keyword_Type,
   Keyword_PY,
   Order_No,
   Form_ID)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
        private const string c_strDeletePathSQL = "delete Template_Path where Path_ID = ?";
        private const string c_strDeleteTemplate = "delete Template_Item where TemplateSet_ID = ?";
        private const string c_strDeleteTemplateSet = "delete TemplateSet where Set_ID = ?";
        private const string c_strGetControlList = @"select A.Form_ID, A.Control_ID, A.Control_Desc, A.Order_No
  from GUI_CONTROL A
 where Form_ID = ?
 order by Order_No";

        private const string c_strGetDeptListSQL = @"SELECT DEPTID_CHR, deptname_vchr, pycode_chr
  FROM T_BSE_DEPTDESC
 WHERE STATUS_INT = 1
 ORDER BY DEPTID_CHR DESC";
        private const string c_strGetTemplateBySetID = @"select A.Template_ID,
       A.Content,
       A.ContentXml,
       A.TemplateSet_ID,
       A.Form_ID,
       A.Control_ID,
       A.Order_No,
       B.Control_Desc
  from Template_Item A
  left join GUI_CONTROL B
    on A.Form_ID = B.Form_ID
   and A.Control_ID = B.Control_ID
 where TemplateSet_ID = ?
 order by Order_No, Template_ID";
        private const string c_strGetTemplatePathByFormIDEmpIDDeptID2 = @"select distinct B.Path_ID, B.Parent_ID, B.FullPath, B.PathName, B.Order_No
  from (select TS.KEYWORD
          from TemplateSet TS
         inner join (select Distinct TemplateSet_ID
                      from Template_Item
                     where Form_ID = ?) B
            on TS.Set_ID = B.TemplateSet_ID
         where FORM_ID = ?
           and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?) or
               (TS.Visiblity_Level = 1))
        union
        select TS.KEYWORD
          from TemplateSet TS
         inner join (select Distinct TemplateSet_ID
                      from Template_Item
                     where Form_ID = ?) B
            on TS.Set_ID = B.TemplateSet_ID
          join TemplateSet_Dept TSD
            on TS.set_id = TSD.Set_ID
         where TS.Form_ID = ?
           and TS.visiblity_level = 2
           and TSD.Department_ID = ?) A
  join Template_Path B
    on A.KEYWORD || '>>' like B.FULLPATH || '>>%'
 order by B.Order_No, B.PATH_ID";

        private const string c_strGetTemplatePathByFormIDEmpIDDeptID3 = @"select distinct B.Path_ID, B.Parent_ID, B.FullPath, B.PathName, B.Order_No
  from (select TS.KEYWORD
          from TemplateSet TS
         inner join (select Distinct TemplateSet_ID
                      from Template_Item
                     where Form_ID = ?) B
            on TS.Set_ID = B.TemplateSet_ID
         where FORM_ID = ?
           and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?))) A
  join Template_Path B
    on A.KEYWORD || '>>' like B.FULLPATH || '>>%'
 order by B.Order_No, B.PATH_ID";
        private const string c_strInsertTemplatePathSQL = @"insert into Template_Path
  (Path_ID, Parent_ID, PathName, FullPath, Order_No)
values
  (?, ?, ?, ?, ?)";
        private const string c_strModifyTemplate = @"update Template_Item
   set Content = ?,
       ContentXml = ?,
       TemplateSet_ID = ?,
       Form_ID = ?,
       Control_ID = ?,
       Order_No = ?
 where Template_ID = ? ";
        private const string c_strModifyTemplateSet = @"update TemplateSet
   set Set_Name = ?,
       Visiblity_Level = ?,
       Employee_ID = ?,
       Keyword = ?,
       Keyword_Type = ?,
       Keyword_PY = ?,
       Order_No = ?,
       Form_ID = ?
 where Set_ID = ? ";
        private const string c_strPathIsUsingSQL = @"select count(*) as Num
  from Template_Path A
 inner join TemplateSet B
    on A.fullpath = B.Keyword
 where A.Path_ID = ?";
        private clsHRPTableService m_objDBService;
        private static DateTime s_dtNullDateTime;

        #endregion

        public clsTemplateService()
        {
            this.m_objDBService = new clsHRPTableService();
            s_dtNullDateTime = DateTime.Parse("1900-1-1");
        }

        //[AutoComplete]
        //public string[] m_arrSplitString(string strAll, string strSplit)
        //{
        //    int num = 0;
        //    int num2 = 0;
        //    string[] strArray = null;
        //    List<string> list = null;
        //    if (!string.IsNullOrEmpty(strSplit))
        //    {
        //        list = new List<string>();
        //        num = 0;
        //        num2 = strAll.IndexOf(strSplit, num);
        //        if (num2 > 0)
        //        {
        //            list.Add(strAll.Substring(num, num2 - num));
        //            num = num2 + strSplit.Length;
        //            num2 = strAll.IndexOf(strSplit, num);
        //        }

        //        if (num >= strAll.Length)
        //        {
        //            list.Add(strAll.Substring(num, strAll.Length - num));
        //        }
        //        strArray = list.ToArray();
        //    }
        //    else
        //    {
        //        strArray = new string[] { strAll };
        //    }
        //    return strArray;
        //}

        public string[] m_arrSplitString(string strAll, string strSplit)
        {
            int num = 0;
            int num2 = 0;
            string[] strArray = null;
            List<string> list = null;
            if (!string.IsNullOrEmpty(strSplit))
            {
                list = new List<string>();
                num = 0;
                num2 = strAll.IndexOf(strSplit, num);
                if (num2 < 0)
                {
                    return new string[] { strAll };
                }

            Label_01:
                list.Add(strAll.Substring(num, num2 - num));
                num = num2 + strSplit.Length;
                num2 = strAll.IndexOf(strSplit, num);

            Label_02:
                if (num2 >= 0)
                {
                    goto Label_01;
                }
                if (num >= strAll.Length)
                {
                    list.Add(strAll.Substring(num, strAll.Length - num));
                }
                strArray = list.ToArray();
            }
            else
            {
                strArray = new string[] { strAll };
            }
            return strArray;
        }

        [AutoComplete]
        public bool m_blnPathIsUsing(int intPathID)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            try
            {
                Sql = "select count(*) as Num  from Template_Path A  inner join TemplateSet B on A.fullpath = B.Keyword where A.Path_ID = ?";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = intPathID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (this.ToInt(dt.Rows[0]["Num"]) > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return false;
        }

        [AutoComplete]
        private int m_intCreateTemplatePathID()
        {
            int num = 0;
            string Sql = string.Empty;
            DataTable dt = null;
            try
            {
                Sql = "select SEQ_TEMPLATE.Nextval from dual";
                (new clsHRPTableService()).DoGetDataTable(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return num;
        }

        [AutoComplete]
        public long m_lngAddTemplate(clsTemplate objTemplate)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            try
            {
                string templateId = this.m_strCreateTemplateID();

                if (!string.IsNullOrEmpty(templateId))
                {
                    objTemplate.m_strTemplateID = templateId;

                    Sql = @"insert into Template_Item
                              (Template_ID,
                               Content,
                               ContentXml,
                               TemplateSet_ID,
                               Form_ID,
                               Control_ID,
                               Order_No)
                            values
                              (?, ?, ?, ?, ?, ?, ?)";

                    svc = new clsHRPTableService();
                    IDataParameter[] parm = null;
                    svc.CreateDatabaseParameter(7, out parm);
                    parm[0].Value = objTemplate.m_strTemplateID;
                    parm[1].Value = objTemplate.m_strContent;
                    parm[2].Value = objTemplate.m_strContentXml;
                    parm[3].Value = objTemplate.m_strTemplateSetID;
                    parm[4].Value = objTemplate.m_strFormID;
                    parm[5].Value = objTemplate.m_strControlID;
                    parm[6].Value = (int)objTemplate.m_intOrderNo;

                    long affect = 0;
                    rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngAddTemplate(clsTemplate[] objTemplate)
        {
            long rec = 0;
            foreach (clsTemplate item in objTemplate)
            {
                if (this.m_lngAddTemplate(item) > 0)
                {
                    rec++;
                }
                else
                {
                    throw new Exception("保存模板明细失败");
                }
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngCopyTemplateSet(string strSetID, int intDestinationPathID)
        {
            return 0;
        }

        [AutoComplete]
        public long m_lngCreateTemplatePath(string strFullPath)
        {
            string str;
            string[] strArray;
            bool flag;
            string str2;
            int num;
            int num2;
            clsTemplatePath path;
            clsTemplatePath path2;
            long num3;
            long num4;
            bool flag2;
            str = ">>";

            strArray = this.m_arrSplitString(strFullPath, str);
            if (strArray != null && strArray.Length > 0)
            {
                goto Label_0026;
            }
            num4 = 1L;
            goto Label_0103;
        Label_0026:
            flag = false;
            str2 = "";
            num = 0;
            num2 = 0;
            goto Label_00ED;
        Label_0039:
            if (str2 != "")
            {
                goto Label_0055;
            }
            str2 = strArray[num2];
            goto Label_0061;
        Label_0055:
            str2 = str2 + str + strArray[num2];
        Label_0061:
            path = null;
            this.m_lngGetTemplatePathByPath(str2, out path);
            if (path != null)
            {
                goto Label_00DB;
            }
            path2 = new clsTemplatePath();
            path2.m_intParentID = num;
            path2.m_strFullPath = str2;
            path2.m_strPathName = strArray[num2];
            path2.m_intOrderNo = 0;
            if (this.m_lngInsertTemplatePath(path2) > 0L)
            {
                goto Label_00CD;
            }
            num4 = 0L;
            goto Label_0103;
        Label_00CD:
            num = path2.m_intPathID;
            flag = true;
            goto Label_00E6;
        Label_00DB:
            num = path.m_intPathID;
        Label_00E6:
            num2 += 1;
        Label_00ED:
            if (num2 < ((int)strArray.Length))
            {
                goto Label_0039;
            }
            num4 = 1L;
        Label_0103:
            return num4;
        }

        [AutoComplete]
        public long m_lngCreateTemplateSet(clsTemplateSet objTemplateSet, out string p_strSetID)
        {
            long rec = 0;
            string Sql;
            p_strSetID = "";
            clsHRPTableService svc = null;
            try
            {
                string templateId = this.m_strCreateTemplateSetID();
                if (!string.IsNullOrEmpty(templateId))
                {
                    objTemplateSet.m_strSetID = templateId;

                    Sql = @"insert into TemplateSet
                              (Set_ID,
                               Start_Date,
                               End_Date,
                               Set_Name,
                               Visiblity_Level,
                               Employee_ID,
                               Keyword,
                               Keyword_Type,
                               Keyword_PY,
                               Order_No,
                               Form_ID)
                            values
                              (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    svc = new clsHRPTableService();
                    IDataParameter[] parm = null;
                    svc.CreateDatabaseParameter(11, out parm);
                    parm[0].Value = objTemplateSet.m_strSetID;
                    parm[1].Value = DateTime.Now;
                    parm[2].Value = DateTime.Now.AddYears(10);
                    parm[3].Value = objTemplateSet.m_strSetName;
                    parm[4].Value = objTemplateSet.m_intVisiblityLevel;
                    parm[5].Value = objTemplateSet.m_strEmployeeID;
                    parm[6].Value = objTemplateSet.m_strKeyword;
                    parm[7].Value = objTemplateSet.m_intKeywordType;
                    parm[8].Value = objTemplateSet.m_strKeywordPY;
                    parm[9].Value = objTemplateSet.m_intOrderNo;
                    parm[10].Value = objTemplateSet.m_strFormID;

                    long affect = 0;
                    rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
                }
                p_strSetID = objTemplateSet.m_strSetID;
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngDeletePath(int intPathID)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            try
            {
                if (this.m_blnPathIsUsing(intPathID))
                {
                    Sql = @"delete Template_Path where Path_ID = ?";

                    svc = new clsHRPTableService();
                    IDataParameter[] parm = null;
                    svc.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = intPathID;

                    long affect = 0;
                    rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngDeleteTemplate(string strTemplateSetID)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            try
            {
                Sql = @"delete Template_Item where TemplateSet_ID = ?";

                svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = strTemplateSetID;

                long affect = 0;
                rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngDeleteTemplateSet(string strTemplateSetID)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            try
            {
                Sql = @"delete TemplateSet where Set_ID = ?";

                svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = strTemplateSetID;

                long affect = 0;
                rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetAllInDepartmentlist(out clsDepartment[] arrDept)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            DataTable dt = null;
            arrDept = null;
            try
            {
                Sql = @"select dd.DEPTID_CHR    DeptID,
                               dd.PYCODE_CHR    PYCODE,
                               dd.DEPTNAME_VCHR DeptName
                          from t_bse_deptdesc dd
                         where INPATIENTOROUTPATIENT_INT = 1
                           and dd.ATTRIBUTEID = '0000003'
                           and dd.STATUS_INT = 1
                         order by DeptName desc";

                svc = new clsHRPTableService();
                rec = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrDept = new clsDepartment[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrDept[i] = new clsDepartment();
                        arrDept[i].m_strDeptID = dr["DeptID"].ToString();
                        arrDept[i].m_strDeptName = dr["DeptName"].ToString();
                        arrDept[i].m_strPYCode = dr["PYCODE"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetAllTemplatePath(string strFormID, out clsTemplatePath[] arrTemplatePath)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            DataTable dt = null;
            arrTemplatePath = null;
            try
            {
                Sql = @"select distinct B.Path_ID, B.Parent_ID, B.FullPath, B.PathName, B.Order_No
                          from (select TS.KEYWORD
                                  from TemplateSet TS
                                 inner join (select Distinct TemplateSet_ID
                                              from Template_Item
                                             where Form_ID = ?) B
                                    on TS.Set_ID = B.TemplateSet_ID
                                 where FORM_ID = ?
                                   and TS.End_Date > {0}) A
                          join Template_Path B
                            on A.KEYWORD || '>>' like B.FULLPATH || '>>%'
                         order by B.Order_No, B.PATH_ID";

                Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName);
                svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = strFormID;
                parm[1].Value = strFormID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrTemplatePath = new clsTemplatePath[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrTemplatePath[i] = new clsTemplatePath();
                        arrTemplatePath[i].m_intPathID = this.ToInt(dr["Path_ID"]);
                        arrTemplatePath[i].m_intParentID = this.ToInt(dr["Parent_ID"]);
                        arrTemplatePath[i].m_strFullPath = dr["FullPath"].ToString();
                        arrTemplatePath[i].m_strPathName = dr["PathName"].ToString();
                        arrTemplatePath[i].m_intOrderNo = this.ToInt(dr["Order_No"]);
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetAllTemplateSetList(string strFormID, out clsTemplateSet[] arrTemplateSet)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            DataTable dt = null;
            arrTemplateSet = null;
            try
            {
                Sql = @"select A.Set_ID,
                               A.Start_Date,
                               A.End_Date,
                               A.set_name,
                               A.visiblity_level,
                               A.Employee_ID,
                               A.KeyWord,
                               A.Keyword_Type,
                               A.Keyword_PY,
                               A.Order_No,
                               A.Form_ID,
                               A.IsValid
                          from (select A.*,
                                       (case
                                         when End_Date > {0} then
                                          1
                                         else
                                          0
                                       end) IsValid
                                  from TemplateSet A
                                 inner join (select Distinct TemplateSet_ID
                                              from Template_Item
                                             where Form_ID = ?) B
                                    on A.Set_ID = B.TemplateSet_ID
                                 where FORM_ID = ?) A
                         order by A.order_no, A.Keyword";

                Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName);
                svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = strFormID;
                parm[1].Value = strFormID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrTemplateSet = new clsTemplateSet[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrTemplateSet[i] = new clsTemplateSet();
                        arrTemplateSet[i].m_strSetID = dr["Set_ID"].ToString().Trim();
                        arrTemplateSet[i].m_dtStartDate = this.ToDateTime(dr["Start_Date"]);
                        arrTemplateSet[i].m_dtEndDate = this.ToDateTime(dr["End_Date"]);
                        arrTemplateSet[i].m_strSetName = dr["set_name"].ToString().Trim();
                        arrTemplateSet[i].m_intVisiblityLevel = this.ToInt(dr["visiblity_level"].ToString());
                        arrTemplateSet[i].m_strEmployeeID = dr["Employee_ID"].ToString().Trim();
                        arrTemplateSet[i].m_strKeyword = dr["KeyWord"].ToString().Trim();
                        arrTemplateSet[i].m_intKeywordType = this.ToInt(dr["Keyword_Type"].ToString());
                        arrTemplateSet[i].m_strKeywordPY = dr["Keyword_PY"].ToString().Trim();
                        arrTemplateSet[i].m_intOrderNo = this.ToInt(dr["Order_No"].ToString());
                        arrTemplateSet[i].m_strFormID = dr["Form_ID"].ToString().Trim();
                        arrTemplateSet[i].m_blnIsValid = this.ToInt(dr["IsValid"]) == 0 ? false : true;
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetControlList(string strFormID, out clsControl[] arrControl)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            DataTable dt = null;
            arrControl = null;
            try
            {
                Sql = @"select A.Form_ID, A.Control_ID, A.Control_Desc, A.Order_No
                          from GUI_CONTROL A
                         where Form_ID = ?
                         order by Order_No";

                svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = strFormID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrControl = new clsControl[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrControl[i] = new clsControl();
                        arrControl[i].m_strFormID = strFormID;
                        arrControl[i].m_strControlID = dr["Control_ID"].ToString().Trim();
                        arrControl[i].m_strControlDesc = dr["Control_Desc"].ToString().Trim();
                        arrControl[i].m_intOrderNo = this.ToInt(dr["Order_No"]);
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetDepartmentList(out clsDepartment[] arrDept)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            DataTable dt = null;
            arrDept = null;
            try
            {
                Sql = @"SELECT DEPTID_CHR, deptname_vchr, pycode_chr
                          FROM T_BSE_DEPTDESC
                         WHERE STATUS_INT = 1
                         ORDER BY DEPTID_CHR DESC";

                svc = new clsHRPTableService();
                rec = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrDept = new clsDepartment[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrDept[i] = new clsDepartment();
                        arrDept[i].m_strDeptID = dr["DEPTID_CHR"].ToString();
                        arrDept[i].m_strDeptName = dr["deptname_vchr"].ToString();
                        arrDept[i].m_strPYCode = dr["pycode_chr"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetFormList(out clsForm[] arrForm)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            DataTable dt = null;
            arrForm = null;
            try
            {
                Sql = @"select ID,Parent_ID,Form_ID,Form_Desc from GUI_FORM order by ID";

                svc = new clsHRPTableService();
                rec = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrForm = new clsForm[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrForm[i] = new clsForm();
                        arrForm[i].m_intID = this.ToInt(dr["ID"]);
                        arrForm[i].m_intParentID = this.ToInt(dr["Parent_ID"]);
                        arrForm[i].m_strFormID = dr["Form_ID"].ToString().Trim();
                        arrForm[i].m_strFormDesc = dr["Form_Desc"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetPublic(string strID, out bool isPublic)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            isPublic = false;
            try
            {
                Sql = "select B.* from T_Sys_EmpRoleMap A,T_Sys_Role B where  A.RoleID_Chr=B.RoleID_Chr and A.EmpID_Chr=? and b.NAME_VCHR='编辑公用模板'";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = strID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    isPublic = true;
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetSequenceArr(int p_intNum, out long[] p_lngSEQArr)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            p_lngSEQArr = null;
            try
            {
                Sql = @"select getseq(?, ?) seqarr from dual";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = "seq_template";
                parm[1].Value = p_intNum;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    p_lngSEQArr = new long[p_intNum];
                    for (int i = 0; i < p_intNum - 1; i++)
                    {
                        p_lngSEQArr[i] = Convert.ToInt64(dt.Rows[0][0]);
                    }
                }
                else
                {
                    p_lngSEQArr = new long[] { 1L };
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetTemplateArr(string strTemplateSetID, out clsTemplate[] arrTemplate)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            arrTemplate = null;
            try
            {
                Sql = @"select A.Template_ID,
                               A.Content,
                               A.ContentXml,
                               A.TemplateSet_ID,
                               A.Form_ID,
                               A.Control_ID,
                               A.Order_No,
                               B.Control_Desc
                          from Template_Item A
                          left join GUI_CONTROL B
                            on A.Form_ID = B.Form_ID
                           and A.Control_ID = B.Control_ID
                         where TemplateSet_ID = ?
                         order by Order_No, Template_ID ";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = strTemplateSetID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrTemplate = new clsTemplate[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrTemplate[i] = new clsTemplate();
                        arrTemplate[i].m_strTemplateID = dr["Template_ID"].ToString().Trim();
                        arrTemplate[i].m_strContent = dr["Content"].ToString().Trim();
                        arrTemplate[i].m_strContentXml = dr["ContentXml"].ToString().Trim();
                        arrTemplate[i].m_strTemplateSetID = dr["TemplateSet_ID"].ToString().Trim();
                        arrTemplate[i].m_strFormID = dr["Form_ID"].ToString().Trim();
                        arrTemplate[i].m_strControlID = dr["Control_ID"].ToString().Trim();
                        arrTemplate[i].m_intOrderNo = this.ToInt(dr["Order_No"].ToString());
                        arrTemplate[i].m_strControlDesc = dr["Control_Desc"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetTemplatePath(string strFormID, string strControlID, string strEmployeeID, string strDepartmentID, out clsTemplatePath[] arrTemplatePath)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            arrTemplatePath = null;
            try
            {
                Sql = @"select distinct B.Path_ID, B.Parent_ID, B.FullPath, B.PathName, B.Order_No
                                  from (select TS.KEYWORD
                                          from TemplateSet TS
                                         inner join (select Distinct TemplateSet_ID
                                                      from Template_Item
                                                     where Form_ID = ?
                                                       and Control_ID = ?) B
                                            on TS.Set_ID = B.TemplateSet_ID
                                         where FORM_ID = ?
                                           and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?) or
                                               (TS.Visiblity_Level = 1))
                                           and TS.End_Date > {0}
                                        union
                                        select TS.KEYWORD
                                          from TemplateSet TS
                                         inner join (select Distinct TemplateSet_ID
                                                      from Template_Item
                                                     where Form_ID = ?
                                                       and Control_ID = ?) B
                                            on TS.Set_ID = B.TemplateSet_ID
                                          join TemplateSet_Dept TSD
                                            on TS.set_id = TSD.Set_ID
                                         where TS.Form_ID = ?
                                           and TS.visiblity_level = 2
                                           and TSD.Department_ID = ?
                                           and TS.End_Date > {1}) A
                                  join Template_Path B
                                    on A.KEYWORD || '>>' like B.FullPath || '>>%'
                                 order by B.Order_No, B.Path_ID";

                Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName, clsDatabaseSQLConvert.s_StrGetServDateFuncName);

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(8, out parm);
                parm[0].Value = strFormID;
                parm[1].Value = strControlID;
                parm[2].Value = strFormID;
                parm[3].Value = strEmployeeID;
                parm[4].Value = strFormID;
                parm[5].Value = strControlID;
                parm[6].Value = strFormID;
                parm[7].Value = strDepartmentID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrTemplatePath = new clsTemplatePath[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrTemplatePath[i] = new clsTemplatePath();
                        arrTemplatePath[i].m_intPathID = this.ToInt(dr["Path_ID"]);
                        arrTemplatePath[i].m_intParentID = this.ToInt(dr["Parent_ID"]);
                        arrTemplatePath[i].m_strFullPath = dr["FullPath"].ToString();
                        arrTemplatePath[i].m_strPathName = dr["PathName"].ToString();
                        arrTemplatePath[i].m_intOrderNo = this.ToInt(dr["Order_No"]);
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetTemplatePath(string strFormID, string strEmployeeID, string strDepartmentID, bool blnOnlyShowValid, out clsTemplatePath[] arrTemplatePath, bool isPublic)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            arrTemplatePath = null;
            try
            {
                svc = new clsHRPTableService();

                if (blnOnlyShowValid == false)
                {
                    if (isPublic == false)
                    {
                        Sql = @"select distinct B.Path_ID, B.Parent_ID, B.FullPath, B.PathName, B.Order_No
                                      from (select TS.KEYWORD
                                              from TemplateSet TS
                                             inner join (select Distinct TemplateSet_ID
                                                          from Template_Item
                                                         where Form_ID = ?) B
                                                on TS.Set_ID = B.TemplateSet_ID
                                             where FORM_ID = ?
                                               and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?) or
                                                   (TS.Visiblity_Level = 1))
                                               and TS.End_Date > {0}
                                            union
                                            select TS.KEYWORD
                                              from TemplateSet TS
                                             inner join (select Distinct TemplateSet_ID
                                                          from Template_Item
                                                         where Form_ID = ?) B
                                                on TS.Set_ID = B.TemplateSet_ID
                                              join TemplateSet_Dept TSD
                                                on TS.set_id = TSD.Set_ID
                                             where TS.Form_ID = ?
                                               and TS.visiblity_level = 2
                                               and TSD.Department_ID = ?
                                               and TS.End_Date > {1}) A
                                      join Template_Path B
                                        on A.KEYWORD || '>>' like B.FULLPATH || '>>%'
                                     order by B.Order_No, B.PATH_ID";
                        svc.CreateDatabaseParameter(6, out parm);
                        parm[0].Value = strFormID;
                        parm[1].Value = strFormID;
                        parm[2].Value = strEmployeeID;
                        parm[3].Value = strFormID;
                        parm[4].Value = strFormID;
                        parm[5].Value = strDepartmentID;
                        Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName, clsDatabaseSQLConvert.s_StrGetServDateFuncName);
                    }
                    else
                    {
                        Sql = @"select distinct B.Path_ID, B.Parent_ID, B.FullPath, B.PathName, B.Order_No
                                      from (select TS.KEYWORD
                                              from TemplateSet TS
                                             inner join (select Distinct TemplateSet_ID
                                                          from Template_Item
                                                         where Form_ID = ?) B
                                                on TS.Set_ID = B.TemplateSet_ID
                                             where FORM_ID = ?
                                               and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?))
                                               and TS.End_Date > {0}) A
                                      join Template_Path B
                                        on A.KEYWORD || '>>' like B.FULLPATH || '>>%'
                                     order by B.Order_No, B.PATH_ID";
                        svc.CreateDatabaseParameter(3, out parm);
                        parm[0].Value = strFormID;
                        parm[1].Value = strFormID;
                        parm[2].Value = strEmployeeID;
                        Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName);
                    }
                }
                else
                {
                    if (isPublic == false)
                    {
                        Sql = @"select distinct B.Path_ID, B.Parent_ID, B.FullPath, B.PathName, B.Order_No
                                      from (select TS.KEYWORD
                                              from TemplateSet TS
                                             inner join (select Distinct TemplateSet_ID
                                                          from Template_Item
                                                         where Form_ID = ?) B
                                                on TS.Set_ID = B.TemplateSet_ID
                                             where FORM_ID = ?
                                               and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?) or
                                                   (TS.Visiblity_Level = 1))
                                            union
                                            select TS.KEYWORD
                                              from TemplateSet TS
                                             inner join (select Distinct TemplateSet_ID
                                                          from Template_Item
                                                         where Form_ID = ?) B
                                                on TS.Set_ID = B.TemplateSet_ID
                                              join TemplateSet_Dept TSD
                                                on TS.set_id = TSD.Set_ID
                                             where TS.Form_ID = ?
                                               and TS.visiblity_level = 2
                                               and TSD.Department_ID = ?) A
                                      join Template_Path B
                                        on A.KEYWORD || '>>' like B.FULLPATH || '>>%'
                                     order by B.Order_No, B.PATH_ID";
                        svc.CreateDatabaseParameter(6, out parm);
                        parm[0].Value = strFormID;
                        parm[1].Value = strFormID;
                        parm[2].Value = strEmployeeID;
                        parm[3].Value = strFormID;
                        parm[4].Value = strFormID;
                        parm[5].Value = strDepartmentID;
                    }
                    else
                    {
                        Sql = @"select distinct B.Path_ID, B.Parent_ID, B.FullPath, B.PathName, B.Order_No
                                      from (select TS.KEYWORD
                                              from TemplateSet TS
                                             inner join (select Distinct TemplateSet_ID
                                                          from Template_Item
                                                         where Form_ID = ?) B
                                                on TS.Set_ID = B.TemplateSet_ID
                                             where FORM_ID = ?
                                               and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?))) A
                                      join Template_Path B
                                        on A.KEYWORD || '>>' like B.FULLPATH || '>>%'
                                     order by B.Order_No, B.PATH_ID";
                        svc.CreateDatabaseParameter(3, out parm);
                        parm[0].Value = strFormID;
                        parm[1].Value = strFormID;
                        parm[2].Value = strEmployeeID;
                    }
                }

                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrTemplatePath = new clsTemplatePath[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrTemplatePath[i] = new clsTemplatePath();
                        arrTemplatePath[i].m_intPathID = this.ToInt(dr["Path_ID"]);
                        arrTemplatePath[i].m_intParentID = this.ToInt(dr["Parent_ID"]);
                        arrTemplatePath[i].m_strFullPath = dr["FullPath"].ToString();
                        arrTemplatePath[i].m_strPathName = dr["PathName"].ToString();
                        arrTemplatePath[i].m_intOrderNo = this.ToInt(dr["Order_No"]);
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetTemplatePath_Const(string strFormID, string strControlID, string strEmployeeID, string strDepartmentID, out clsTemplatePath[] arrTemplatePath)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            arrTemplatePath = null;
            try
            {
                Sql = @"select distinct B.Path_ID, B.Parent_ID, B.FullPath, B.PathName, B.Order_No
                              from (select TS.KEYWORD
                                      from TemplateSet TS
                                     inner join (select Distinct TemplateSet_ID
                                                  from Template_Item
                                                 where Form_ID = ?
                                                   and Control_ID = ?) B
                                        on TS.Set_ID = B.TemplateSet_ID
                                     where FORM_ID = ?
                                       and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?) or
                                           (TS.Visiblity_Level = 1))
                                       and TS.End_Date > {0}
                                       and TS.Keyword like '常用值--%'
                                    union
                                    select TS.KEYWORD
                                      from TemplateSet TS
                                     inner join (select Distinct TemplateSet_ID
                                                  from Template_Item
                                                 where Form_ID = ?
                                                   and Control_ID = ?) B
                                        on TS.Set_ID = B.TemplateSet_ID
                                      join TemplateSet_Dept TSD
                                        on TS.set_id = TSD.Set_ID
                                     where TS.Form_ID = ?
                                       and TS.visiblity_level = 2
                                       and TSD.Department_ID = ?
                                       and TS.End_Date > {1}
                                       and TS.Keyword like '常用值--%') A
                              join Template_Path B
                                on A.KEYWORD || '>>' like B.FullPath || '>>%'
                             order by B.Order_No, B.Path_ID";

                Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName, clsDatabaseSQLConvert.s_StrGetServDateFuncName);

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(8, out parm);
                parm[0].Value = strFormID;
                parm[1].Value = strControlID;
                parm[2].Value = strFormID;
                parm[3].Value = strEmployeeID;
                parm[4].Value = strFormID;
                parm[5].Value = strControlID;
                parm[6].Value = strFormID;
                parm[7].Value = strDepartmentID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrTemplatePath = new clsTemplatePath[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrTemplatePath[i] = new clsTemplatePath();
                        arrTemplatePath[i].m_intPathID = this.ToInt(dr["Path_ID"]);
                        arrTemplatePath[i].m_intParentID = this.ToInt(dr["Parent_ID"]);
                        arrTemplatePath[i].m_strFullPath = dr["FullPath"].ToString();
                        arrTemplatePath[i].m_strPathName = dr["PathName"].ToString();
                        arrTemplatePath[i].m_intOrderNo = this.ToInt(dr["Order_No"]);
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetTemplatePathByID(int intPathID, out clsTemplatePath objPath)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            objPath = null;
            try
            {
                Sql = @"select Path_ID,Parent_ID,PathName,FullPath,Order_No from Template_Path where Path_ID= ? ";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = intPathID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    objPath = new clsTemplatePath();
                    objPath.m_intPathID = this.ToInt(dt.Rows[0]["Path_ID"]);
                    objPath.m_intParentID = this.ToInt(dt.Rows[0]["Parent_ID"]);
                    objPath.m_strFullPath = dt.Rows[0]["FullPath"].ToString();
                    objPath.m_strPathName = dt.Rows[0]["PathName"].ToString();
                    objPath.m_intOrderNo = this.ToInt(dt.Rows[0]["Order_No"]);
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetTemplatePathByPath(string strFullPath, out clsTemplatePath objPath)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            objPath = null;
            try
            {
                Sql = @"select Path_ID,Parent_ID,PathName,FullPath,Order_No from Template_Path where FullPath=? ";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = strFullPath;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    objPath = new clsTemplatePath();
                    objPath.m_intPathID = this.ToInt(dt.Rows[0]["Path_ID"]);
                    objPath.m_intParentID = this.ToInt(dt.Rows[0]["Parent_ID"]);
                    objPath.m_strFullPath = dt.Rows[0]["FullPath"].ToString();
                    objPath.m_strPathName = dt.Rows[0]["PathName"].ToString();
                    objPath.m_intOrderNo = this.ToInt(dt.Rows[0]["Order_No"]);
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetTemplateSetDepartment(string strTemplateSetID, out clsDepartment[] arrDept)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            arrDept = null;
            try
            {
                Sql = @"select t.SET_ID, t.Department_ID, d.deptname_vchr
                          from TemplateSet_Dept t, t_bse_deptdesc d
                         where t.Set_ID = ?
                           and t.department_id = d.deptid_chr";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = strTemplateSetID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrDept = new clsDepartment[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrDept[i] = new clsDepartment();
                        arrDept[i].m_strDeptID = dr["Department_ID"].ToString();
                        arrDept[i].m_strDeptName = dr["deptname_vchr"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetTemplateSetList(string strFormID, string strEmployeeID, string strDepartmentID, bool blnOnlyShowValid, out clsTemplateSet[] arrTemplateSet, bool isPublic)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            arrTemplateSet = null;
            try
            {
                svc = new clsHRPTableService();

                if (blnOnlyShowValid == false)
                {
                    if (isPublic == false)
                    {
                        Sql = @"select A.Set_ID,
                                       A.Start_Date,
                                       A.End_Date,
                                       A.set_name,
                                       A.visiblity_level,
                                       A.Employee_ID,
                                       A.KeyWord,
                                       A.Keyword_Type,
                                       A.Keyword_PY,
                                       A.Order_No,
                                       A.Form_ID,
                                       1 as IsValid
                                  from (select TS.*
                                          from TemplateSet TS
                                         inner join (select Distinct TemplateSet_ID
                                                      from Template_Item
                                                     where Form_ID = ?) B
                                            on TS.Set_ID = B.TemplateSet_ID
                                         where FORM_ID = ?
                                           and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?) or
                                               (TS.Visiblity_Level = 1))
                                           and TS.End_Date > {0}
                                        union
                                        select TS.*
                                          from TemplateSet TS
                                         inner join (select Distinct TemplateSet_ID
                                                      from Template_Item
                                                     where Form_ID = ?) B
                                            on TS.Set_ID = B.TemplateSet_ID
                                         inner join TemplateSet_Dept TSD
                                            on TS.set_id = TSD.Set_ID
                                         where TS.Form_ID = ?
                                           and TS.visiblity_level = 2
                                           and TSD.Department_ID = ?
                                           and TS.End_Date > {1}) A
                                 order by A.order_no, A.Keyword";
                        svc.CreateDatabaseParameter(6, out parm);
                        parm[0].Value = strFormID;
                        parm[1].Value = strFormID;
                        parm[2].Value = strEmployeeID;
                        parm[3].Value = strFormID;
                        parm[4].Value = strFormID;
                        parm[5].Value = strDepartmentID;
                        Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName, clsDatabaseSQLConvert.s_StrGetServDateFuncName);
                    }
                    else
                    {
                        Sql = @"select A.Set_ID,
                                       A.Start_Date,
                                       A.End_Date,
                                       A.set_name,
                                       A.visiblity_level,
                                       A.Employee_ID,
                                       A.KeyWord,
                                       A.Keyword_Type,
                                       A.Keyword_PY,
                                       A.Order_No,
                                       A.Form_ID,
                                       1 as IsValid
                                  from (select TS.*
                                          from TemplateSet TS
                                         inner join (select Distinct TemplateSet_ID
                                                      from Template_Item
                                                     where Form_ID = ?) B
                                            on TS.Set_ID = B.TemplateSet_ID
                                         where FORM_ID = ?
                                           and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?))
                                           and TS.End_Date > {0}) A
                                 order by A.order_no, A.Keyword";
                        svc.CreateDatabaseParameter(3, out parm);
                        parm[0].Value = strFormID;
                        parm[1].Value = strFormID;
                        parm[2].Value = strEmployeeID;
                        Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName);
                    }
                }
                else
                {
                    if (isPublic == false)
                    {
                        Sql = @"select A.Set_ID,
                                       A.Start_Date,
                                       A.End_Date,
                                       A.set_name,
                                       A.visiblity_level,
                                       A.Employee_ID,
                                       A.KeyWord,
                                       A.Keyword_Type,
                                       A.Keyword_PY,
                                       A.Order_No,
                                       A.Form_ID,
                                       A.IsValid
                                  from (select A.*,
                                               (case
                                                 when End_Date > {0} then
                                                  1
                                                 else
                                                  0
                                               end) IsValid
                                          from TemplateSet A
                                         inner join (select Distinct TemplateSet_ID
                                                      from Template_Item
                                                     where Form_ID = ?) B
                                            on A.Set_ID = B.TemplateSet_ID
                                         where FORM_ID = ?
                                           and ((Visiblity_Level = 0 and Employee_ID = ?) or
                                               (Visiblity_Level = 1))
                                        union
                                        select TS.*,
                                               (case
                                                 when TS.End_Date > {1} then
                                                  1
                                                 else
                                                  0
                                               end) IsValid
                                          from TemplateSet TS
                                         inner join (select Distinct TemplateSet_ID
                                                      from Template_Item
                                                     where Form_ID = ?) B
                                            on TS.Set_ID = B.TemplateSet_ID
                                         inner join TemplateSet_Dept TSD
                                            on TS.set_id = TSD.Set_ID
                                         where TS.Form_ID = ?
                                           and TS.visiblity_level = 2
                                           and TSD.Department_ID = ?) A
                                 order by A.order_no, A.Keyword
                                ";
                        svc.CreateDatabaseParameter(6, out parm);
                        parm[0].Value = strFormID;
                        parm[1].Value = strFormID;
                        parm[2].Value = strEmployeeID;
                        parm[3].Value = strFormID;
                        parm[4].Value = strFormID;
                        parm[5].Value = strDepartmentID;
                        Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName, clsDatabaseSQLConvert.s_StrGetServDateFuncName);
                    }
                    else
                    {
                        Sql = @"select A.Set_ID,
                                       A.Start_Date,
                                       A.End_Date,
                                       A.set_name,
                                       A.visiblity_level,
                                       A.Employee_ID,
                                       A.KeyWord,
                                       A.Keyword_Type,
                                       A.Keyword_PY,
                                       A.Order_No,
                                       A.Form_ID,
                                       A.IsValid
                                  from (select A.*,
                                               (case
                                                 when End_Date > {0} then
                                                  1
                                                 else
                                                  0
                                               end) IsValid
                                          from TemplateSet A
                                         inner join (select Distinct TemplateSet_ID
                                                      from Template_Item
                                                     where Form_ID = ?) B
                                            on A.Set_ID = B.TemplateSet_ID
                                         where FORM_ID = ?
                                           and ((Visiblity_Level = 0 and Employee_ID = ?))) A
                                 order by A.order_no, A.Keyword";
                        svc.CreateDatabaseParameter(3, out parm);
                        parm[0].Value = strFormID;
                        parm[1].Value = strFormID;
                        parm[2].Value = strEmployeeID;
                        Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName);
                    }
                }

                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrTemplateSet = new clsTemplateSet[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrTemplateSet[i] = new clsTemplateSet();
                        arrTemplateSet[i].m_strSetID = dr["Set_ID"].ToString().Trim();
                        arrTemplateSet[i].m_dtStartDate = this.ToDateTime(dr["Start_Date"]);
                        arrTemplateSet[i].m_dtEndDate = this.ToDateTime(dr["End_Date"]);
                        arrTemplateSet[i].m_strSetName = dr["set_name"].ToString().Trim();
                        arrTemplateSet[i].m_intVisiblityLevel = this.ToInt(dr["visiblity_level"].ToString());
                        arrTemplateSet[i].m_strEmployeeID = dr["Employee_ID"].ToString().Trim();
                        arrTemplateSet[i].m_strKeyword = dr["KeyWord"].ToString().Trim();
                        arrTemplateSet[i].m_intKeywordType = this.ToInt(dr["Keyword_Type"].ToString());
                        arrTemplateSet[i].m_strKeywordPY = dr["Keyword_PY"].ToString().Trim();
                        arrTemplateSet[i].m_intOrderNo = this.ToInt(dr["Order_No"].ToString());
                        arrTemplateSet[i].m_strFormID = dr["Form_ID"].ToString().Trim();
                        arrTemplateSet[i].m_blnIsValid = this.ToInt(dr["IsValid"]) == 0 ? false : true;
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetTemplateSetList(string strFormID, string strControlID, string strEmployeeID, string strDepartmentID, out clsTemplateSet[] arrTemplateSet, bool isPublic)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            arrTemplateSet = null;
            try
            {
                svc = new clsHRPTableService();

                if (isPublic)
                {
                    Sql = @"select A.Set_ID,
                                   A.Start_Date,
                                   A.End_Date,
                                   A.set_name,
                                   A.visiblity_level,
                                   A.Employee_ID,
                                   A.KeyWord,
                                   A.Keyword_Type,
                                   A.Keyword_PY,
                                   A.Order_No,
                                   A.Form_ID
                              from (select TS.*
                                      from TemplateSet TS
                                     inner join (select Distinct TemplateSet_ID
                                                  from Template_Item
                                                 where Form_ID = ?
                                                   and Control_ID = ?) B
                                        on TS.Set_ID = B.TemplateSet_ID
                                     where TS.FORM_ID = ?
                                       and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?))
                                       and TS.End_Date > {0}) A
                             order by A.order_no, A.Keyword";

                    svc.CreateDatabaseParameter(4, out parm);
                    parm[0].Value = strFormID;
                    parm[1].Value = strControlID;
                    parm[2].Value = strFormID;
                    parm[3].Value = strEmployeeID;
                    Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName);
                }
                else
                {
                    Sql = @"select A.Set_ID,
                                   A.Start_Date,
                                   A.End_Date,
                                   A.set_name,
                                   A.visiblity_level,
                                   A.Employee_ID,
                                   A.KeyWord,
                                   A.Keyword_Type,
                                   A.Keyword_PY,
                                   A.Order_No,
                                   A.Form_ID
                              from (select TS.*
                                      from TemplateSet TS
                                     inner join (select Distinct TemplateSet_ID
                                                  from Template_Item
                                                 where Form_ID = ?
                                                   and Control_ID = ?) B
                                        on TS.Set_ID = B.TemplateSet_ID
                                     where TS.FORM_ID = ?
                                       and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?) or
                                           (TS.Visiblity_Level = 1))
                                       and TS.End_Date > {0}
                                    union
                                    select TS.*
                                      from TemplateSet TS
                                     inner join (select Distinct TemplateSet_ID
                                                  from Template_Item
                                                 where Form_ID = ?
                                                   and Control_ID = ?) B
                                        on TS.Set_ID = B.TemplateSet_ID
                                      join TemplateSet_Dept TSD
                                        on TS.set_id = TSD.Set_ID
                                     where TS.Form_ID = ?
                                       and TS.visiblity_level = 2
                                       and TSD.Department_ID = ?
                                       and TS.End_Date > {1}) A
                             order by A.order_no, A.Keyword";

                    svc.CreateDatabaseParameter(8, out parm);
                    parm[0].Value = strFormID;
                    parm[1].Value = strControlID;
                    parm[2].Value = strFormID;
                    parm[3].Value = strEmployeeID;
                    parm[4].Value = strFormID;
                    parm[5].Value = strControlID;
                    parm[6].Value = strFormID;
                    parm[7].Value = strDepartmentID;
                    Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName, clsDatabaseSQLConvert.s_StrGetServDateFuncName);
                }
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    arrTemplateSet = new clsTemplateSet[dt.Rows.Count];
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrTemplateSet[i] = new clsTemplateSet();
                        arrTemplateSet[i].m_strSetID = dr["Set_ID"].ToString().Trim();
                        arrTemplateSet[i].m_dtStartDate = this.ToDateTime(dr["Start_Date"]);
                        arrTemplateSet[i].m_dtEndDate = this.ToDateTime(dr["End_Date"]);
                        arrTemplateSet[i].m_strSetName = dr["set_name"].ToString().Trim();
                        arrTemplateSet[i].m_intVisiblityLevel = this.ToInt(dr["visiblity_level"].ToString());
                        arrTemplateSet[i].m_strEmployeeID = dr["Employee_ID"].ToString().Trim();
                        arrTemplateSet[i].m_strKeyword = dr["KeyWord"].ToString().Trim();
                        arrTemplateSet[i].m_intKeywordType = this.ToInt(dr["Keyword_Type"].ToString());
                        arrTemplateSet[i].m_strKeywordPY = dr["Keyword_PY"].ToString().Trim();
                        arrTemplateSet[i].m_intOrderNo = this.ToInt(dr["Order_No"].ToString());
                        arrTemplateSet[i].m_strFormID = dr["Form_ID"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetTemplateSetList_Const(string strFormID, string strControlID, string strEmployeeID, string strDepartmentID, out clsTemplateSet[] arrTemplateSet)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            arrTemplateSet = null;
            try
            {
                Sql = @"select A.Set_ID,
                               A.Start_Date,
                               A.End_Date,
                               A.set_name,
                               A.visiblity_level,
                               A.Employee_ID,
                               A.KeyWord,
                               A.Keyword_Type,
                               A.Keyword_PY,
                               A.Order_No,
                               A.Form_ID
                          from (select TS.*
                                  from TemplateSet TS
                                 inner join (select Distinct TemplateSet_ID
                                              from Template_Item
                                             where Form_ID = ?
                                               and Control_ID = ?) B
                                    on TS.Set_ID = B.TemplateSet_ID
                                 where TS.FORM_ID = ?
                                   and ((TS.Visiblity_Level = 0 and TS.Employee_ID = ?) or
                                       (TS.Visiblity_Level = 1))
                                   and TS.End_Date > {0}
                                   and TS.Keyword like '常用值--%'
                                union
                                select TS.*
                                  from TemplateSet TS
                                 inner join (select Distinct TemplateSet_ID
                                              from Template_Item
                                             where Form_ID = ?
                                               and Control_ID = ?) B
                                    on TS.Set_ID = B.TemplateSet_ID
                                  join TemplateSet_Dept TSD
                                    on TS.set_id = TSD.Set_ID
                                 where TS.Form_ID = ?
                                   and TS.visiblity_level = 2
                                   and TSD.Department_ID = ?
                                   and TS.End_Date > {1}
                                   and TS.Keyword like '常用值--%') A
                         order by A.Keyword, A.order_no";

                Sql = string.Format(Sql, clsDatabaseSQLConvert.s_StrGetServDateFuncName, clsDatabaseSQLConvert.s_StrGetServDateFuncName);

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(8, out parm);
                parm[0].Value = strFormID;
                parm[1].Value = strControlID;
                parm[2].Value = strFormID;
                parm[3].Value = strEmployeeID;
                parm[4].Value = strFormID;
                parm[5].Value = strControlID;
                parm[6].Value = strFormID;
                parm[7].Value = strDepartmentID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = null;
                    arrTemplateSet = new clsTemplateSet[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        arrTemplateSet[i] = new clsTemplateSet();
                        arrTemplateSet[i].m_strSetID = dr["Set_ID"].ToString().Trim();
                        arrTemplateSet[i].m_dtStartDate = this.ToDateTime(dr["Start_Date"]);
                        arrTemplateSet[i].m_dtEndDate = this.ToDateTime(dr["End_Date"]);
                        arrTemplateSet[i].m_strSetName = dr["set_name"].ToString().Trim();
                        arrTemplateSet[i].m_intVisiblityLevel = this.ToInt(dr["visiblity_level"].ToString());
                        arrTemplateSet[i].m_strEmployeeID = dr["Employee_ID"].ToString().Trim();
                        arrTemplateSet[i].m_strKeyword = dr["KeyWord"].ToString().Trim();
                        arrTemplateSet[i].m_intKeywordType = this.ToInt(dr["Keyword_Type"].ToString());
                        arrTemplateSet[i].m_strKeywordPY = dr["Keyword_PY"].ToString().Trim();
                        arrTemplateSet[i].m_intOrderNo = this.ToInt(dr["Order_No"].ToString());
                        arrTemplateSet[i].m_strFormID = dr["Form_ID"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngHasMaintenanceRight(string p_strEmpID, out bool p_blnIsPublic)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            p_blnIsPublic = false;
            try
            {
                Sql = "select B.RoleID_Chr from T_Sys_EmpRoleMap A,T_Sys_Role B where  A.RoleID_Chr = B.RoleID_Chr and A.EmpID_Chr = ? and b.NAME_VCHR = '模板维护'";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = p_strEmpID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    p_blnIsPublic = true;
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        private long m_lngInsertTemplatePath(clsTemplatePath objPath)
        {
            long rec = 0;
            string Sql = string.Empty;
            clsTemplatePath path = null;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            try
            {
                int pathId = this.m_intCreateTemplatePathID(); ;

                if (pathId > 0)
                {
                    objPath.m_intPathID = pathId;
                    Sql = "insert into Template_Path (Path_ID, Parent_ID, PathName, FullPath, Order_No) values (?, ?, ?, ?, ?)";

                    svc = new clsHRPTableService();
                    svc.CreateDatabaseParameter(5, out parm);
                    parm[0].Value = (int)objPath.m_intPathID;
                    parm[1].Value = (int)objPath.m_intParentID;
                    parm[2].Value = objPath.m_strPathName;
                    parm[3].Value = objPath.m_strFullPath;
                    parm[4].Value = (int)objPath.m_intOrderNo;

                    long affect = 0;
                    rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngModifyTemplate(clsTemplate objTemplate)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            try
            {
                Sql = @"update Template_Item
                           set Content        = ?,
                               ContentXml     = ?,
                               TemplateSet_ID = ?,
                               Form_ID        = ?,
                               Control_ID     = ?,
                               Order_No       = ?
                         where Template_ID = ?";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(7, out parm);
                parm[0].Value = objTemplate.m_strContent;
                parm[1].Value = objTemplate.m_strContentXml;
                parm[2].Value = objTemplate.m_strTemplateSetID;
                parm[3].Value = objTemplate.m_strFormID;
                parm[4].Value = objTemplate.m_strControlID;
                parm[5].Value = (int)objTemplate.m_intOrderNo;
                parm[6].Value = objTemplate.m_strTemplateID;

                long affect = 0;
                rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngModifyTemplateSet(clsTemplateSet objTemplateSet)
        {
            if (objTemplateSet == null) return 0;

            long rec = 0;
            string Sql = string.Empty;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            try
            {
                Sql = @"update TemplateSet
                               set Set_Name = ?,
                                   Visiblity_Level = ?,
                                   Employee_ID = ?,
                                   Keyword = ?,
                                   Keyword_Type = ?,
                                   Keyword_PY = ?,
                                   Order_No = ?,
                                   Form_ID = ?
                             where Set_ID = ? ";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(9, out parm);
                parm[0].Value = objTemplateSet.m_strSetName;
                parm[1].Value = (int)objTemplateSet.m_intVisiblityLevel;
                parm[2].Value = objTemplateSet.m_strEmployeeID;
                parm[3].Value = objTemplateSet.m_strKeyword;
                parm[4].Value = (int)objTemplateSet.m_intKeywordType;
                parm[5].Value = objTemplateSet.m_strKeywordPY;
                parm[6].Value = (int)objTemplateSet.m_intOrderNo;
                parm[7].Value = objTemplateSet.m_strFormID;
                parm[8].Value = objTemplateSet.m_strSetID;

                long affect = 0;
                rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngMoveTemplateSet(string strSetID, int intDestinationPathID)
        {
            long rec = 0;
            string Sql = string.Empty;
            clsTemplatePath path = null;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            try
            {
                this.m_lngGetTemplatePathByID(intDestinationPathID, out path);

                if (path != null)
                {
                    Sql = "update TemplateSet Set Keyword = ? ,Order_No = 0 where Set_ID = ?";

                    svc = new clsHRPTableService();
                    svc.CreateDatabaseParameter(2, out parm);
                    parm[0].Value = path.m_strFullPath;
                    parm[1].Value = strSetID;

                    long affect = 0;
                    rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngRenamePath(int intPathID, string strNewName)
        {
            clsTemplatePath path = null;
            return this.m_lngGetTemplatePathByID(intPathID, out path);
        }

        [AutoComplete]
        public long m_lngSetPathOrderNo(int intPathID, int intOrderNo)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            try
            {
                Sql = "update Template_Path Set Order_No = ? where Path_ID = ?";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = intOrderNo;
                parm[1].Value = intPathID;

                long affect = 0;
                rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngSetTemplateSetDepartment(string strTemplateSetID, clsDepartment[] arrDept)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            try
            {
                Sql = "delete TemplateSet_Dept where Set_ID = ? ";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = strTemplateSetID;

                long affect = 0;
                rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);

                if (arrDept != null)
                {
                    foreach (clsDepartment dept in arrDept)
                    {
                        Sql = @"insert into TemplateSet_Dept(Set_ID, Department_ID) values(?, ?)";
                        svc.CreateDatabaseParameter(2, out parm);
                        parm[0].Value = strTemplateSetID;
                        parm[1].Value = dept.m_strDeptID;
                        rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngSetTemplateSetOrderNo(string strSetID, int intOrderNo)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            try
            {
                Sql = "update TemplateSet Set Order_No = ? where Set_ID = ? ";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = intOrderNo;
                parm[1].Value = strSetID;

                long affect = 0;
                rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngSetTemplateSetValidDate(string strTemplateSetID, int intMonth)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            try
            {
                Sql = "update TemplateSet set END_DATE = ? where Set_ID = ? ";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = DateTime.Now.AddMonths(intMonth);
                parm[1].Value = strTemplateSetID;

                long affect = 0;
                rec = svc.lngExecuteParameterSQL(Sql, ref affect, parm);
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public string m_strCreateTemplateID()
        {
            string Sql;
            clsHRPTableService svc = null;
            DataTable dt = null;
            try
            {
                Sql = "select SEQ_TEMPLATE.Nextval from dual";

                svc = new clsHRPTableService();
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return "1";
        }

        [AutoComplete]
        public string m_strCreateTemplateSetID()
        {
            string Sql;
            clsHRPTableService svc = null;
            DataTable dt = null;
            try
            {
                Sql = "select SEQ_TEMPLATE.Nextval from dual";

                svc = new clsHRPTableService();
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return "1";
        }

        [AutoComplete]
        public string m_strGetDeparementName(string strDeptID)
        {
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            try
            {
                Sql = "SELECT deptname_vchr FROM T_BSE_DEPTDESC WHERE STATUS_INT = 1 and DEPTID_CHR = ?";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = strDeptID;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["deptname_vchr"].ToString();
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return "";
        }

        [AutoComplete]
        public string m_strGetEmployeeName(string strEmpID)
        {
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            try
            {
                Sql = "select EMPID_CHR,LASTNAME_VCHR from T_BSE_EMPLOYEE where STATUS_INT = 1 and EMPID_CHR = ?";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = strEmpID;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["LASTNAME_VCHR"].ToString();
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return "";
        }

        [AutoComplete]
        public string m_strGetTemplateSetIDBySetName(string strTemplateSetName, string strKeyWord)
        {
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            try
            {
                Sql = "select Set_ID from TemplateSet where Set_Name = ? and Keyword = ? ";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = strTemplateSetName;
                parm[1].Value = strKeyWord;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Set_ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return "";
        }

        private string m_strProcessParam(string strParamValue)
        {
            return strParamValue.Replace("'", "''");
        }

        private DateTime ToDateTime(object objValue)
        {
            try
            {
                return Convert.ToDateTime(objValue);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        private int ToInt(object objValue)
        {
            try
            {
                return Convert.ToInt32(objValue);
            }
            catch
            {
                return 0;
            }
        }

        [AutoComplete]
        public long m_lngGetAllCheckCategory(out DataTable dt)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            dt = null;
            try
            {
                Sql = @"select a.category_id_chr, a.category_name_vchr, a.order_no_name_vchr from t_bse_pacs_check_category a";

                svc = new clsHRPTableService();
                rec = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }

        [AutoComplete]
        public long m_lngGetAllCheckPartByCategoryID(string p_strCategoryID, out DataTable dt)
        {
            long rec = 0;
            string Sql;
            clsHRPTableService svc = null;
            IDataParameter[] parm = null;
            dt = null;
            try
            {
                Sql = @"select part_id as 编号, part_name_vchr as 部位名称 from t_bse_pacs_check_part where category_id = ?";

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = p_strCategoryID;
                rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return rec;
        }
    }
}
