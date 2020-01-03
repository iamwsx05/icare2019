using System;
using weCare.Core.Entity;
using System.Collections;

namespace iCare
{
    /// <summary>
    /// Summary description for clsTemplateDomain.
    /// Domain层代码，完成简单的判断和商业逻辑,刘颖源,2003-5-7 16:28:56
    /// </summary>
    public class clsTemplateDomain
    {
        public clsTemplateDomain()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public long lngGetValue(string p_strID, out clsTemplateValue p_objValue)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetTemplate(p_strID, out p_objValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        public string strGetTemplateID()
        {
            string strTemplateID = "";

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            try
            {
                strTemplateID = (new weCare.Proxy.ProxyEmr02()).Service.strGetTemplateID( );
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (strTemplateID == null || strTemplateID == "") strTemplateID = "000001";
            return (strTemplateID);
        }

        #region 保存模板,刘颖源,2003-5-7 19:24:10
        public long lngSaveTemplate(bool p_blnIsNewTemplate, clsTemplateValue p_objValue, clsTemplate_DetailValue p_objTemplate_Detail, clsTemplate_KeywordValue[] p_objTemplate_Keyword, clsTemplate_TargetValue[] p_objTemplate_Target, clsTemplate_Dept_VisibilityValue[] p_objTemplate_Dept_Visiblity, out string p_strTmeplateID)
        {
            long lngEff = 0;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngSaveTemplate(p_blnIsNewTemplate, p_objValue, p_objTemplate_Detail, p_objTemplate_Keyword, p_objTemplate_Target, p_objTemplate_Dept_Visiblity, ref lngEff, out p_strTmeplateID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (lngRes);
        }

        #endregion

        #region 检查是否已存在当前分类下当前命名的模板
        public long m_lngCheckTemplate(string p_strKeyWord,
            string p_strSetKeyWord,
            string p_strTemplateName,
            out string p_strTemplateID)
        {
            long lngEff = 0;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngCheckTemplate(p_strKeyWord, p_strSetKeyWord, p_strTemplateName, out p_strTemplateID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region 保存套装模板,蔡沐忠修改
        public long lngSaveTemplateSet(bool p_blnIsNewTemplate, clsTempate_SetValue p_objTempate_SetValue, clsTemplate_Set_Detail_01Value[] p_objTemplate_Set_Detail_01Value,
            clsTemplate_Set_Detail_02Value[] p_objTemplate_Set_Detail_02Value, clsTemplate_Set_KeywordValue[] p_objTemplate_Set_KeywordValue, out string p_strSetID)
        {
            long lngEff = 0;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr02()).Service.lngSaveTemplateSet(p_blnIsNewTemplate, p_objTempate_SetValue, p_objTemplate_Set_Detail_01Value, p_objTemplate_Set_Detail_02Value, p_objTemplate_Set_KeywordValue, ref lngEff, out p_strSetID));
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        #endregion


        #region 得到套装模板ID,刘颖源,2003-5-9 11:05:50
        public string strGetTemplateSetID()
        {
            string strID = "";

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            try
            {
                strID = (new weCare.Proxy.ProxyEmr02()).Service.strGetTemplateSetID( );

            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (strID == null || strID == "") strID = "00001";
            return (strID);


        }
        #endregion

        #region 检查是否已存在当前分类下的模板
        public string strCheckTemplateSetID(string p_strKeyWord, string p_strSetKeyWord, string p_strFormID)
        {
            string strID = "";

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            try
            {
                long lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngCheckTemplateSet(
                    p_strKeyWord, p_strSetKeyWord, p_strFormID, out strID);

            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (strID);


        }
        #endregion


        #region 得到所有的Form,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsGUI_InfoValue[] lngGetAllForms()
        {
            clsGUI_InfoValue[] objFormsArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllForms("1234", out objFormsArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objFormsArr);
        }
        #endregion

        #region 得到所有的Control,刘颖源,2003-5-7 20:09:20
        //在此编写同类功能函数体
        public clsGUI_Info_DetailValue[] lngGetAllControls(string p_strFormID)
        {
            clsGUI_Info_DetailValue[] objFormsArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllControls(p_strFormID, out objFormsArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objFormsArr);
        }
        #endregion

        #region 得到所有的ICD10_IllnessID,Domain层,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsICD10_IllnessIDValue[] lngGetAllICD10_IllnessID()
        {
            clsICD10_IllnessIDValue[] objICD10_IllnessIDArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllICD10_IllnessID("", out objICD10_IllnessIDArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objICD10_IllnessIDArr);
        }
        #endregion

        #region 得到所有的ICD10_IllnessSubID,Domain层,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsICD10_IllnessSubIDValue[] lngGetAllICD10_IllnessSubID(string p_strID)
        {
            clsICD10_IllnessSubIDValue[] objICD10_IllnessSubIDArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllICD10_IllnessSubID(p_strID, out objICD10_IllnessSubIDArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objICD10_IllnessSubIDArr);
        }
        #endregion

        #region 得到所有的ICD10_IllnessDetailID,Domain层,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsICD10_IllnessDetailIDValue[] lngGetAllICD10_IllnessDetailID(string p_strID)
        {
            clsICD10_IllnessDetailIDValue[] objICD10_IllnessDetailIDArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllICD10_IllnessDetailID(p_strID, out objICD10_IllnessDetailIDArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objICD10_IllnessDetailIDArr);
        }
        #endregion

        #region 得到所有的Bio_System,Domain层,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsBio_SystemValue[] lngGetAllBio_System()
        {
            clsBio_SystemValue[] objBio_SystemArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllBio_System("", out objBio_SystemArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objBio_SystemArr);
        }
        #endregion

        #region 得到所有的Bio_System_Detail,Domain层,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsBio_System_DetailValue[] lngGetAllBio_System_Detail(string p_strID)
        {
            clsBio_System_DetailValue[] objBio_System_DetailArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllBio_System_Detail(p_strID, out objBio_System_DetailArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objBio_System_DetailArr);
        }
        #endregion


        #region 得到所有的TemplateFormControl,Domain层,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsTemplateFormControlValue[] lngGetAllTemplateFormControl(string p_strFormID, string p_strControlID, string p_strEmployeeID)
        {
            clsTemplateFormControlValue[] objTemplateFormControlArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllTemplateFormControl(p_strFormID, p_strControlID, p_strEmployeeID, out objTemplateFormControlArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplateFormControlArr);
        }
        #endregion

        #region 获得所有单元模板的关键字,Domain层
        //在此编写同类功能函数体
        public clsTemplate_KeywordValue[] lngGetSingleKeyword(int p_intKeywordType, string p_strFormID, string p_strControlID, string p_strEmployeeID, string p_strDepartmentID)
        {
            clsTemplate_KeywordValue[] objTemplate_KeywordArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetSingleKeyword(p_intKeywordType, p_strFormID, p_strControlID, p_strEmployeeID, p_strDepartmentID, out objTemplate_KeywordArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplate_KeywordArr);
        }
        #endregion

        #region 获得所有单元模板的关键字下的模板名称
        //在此编写同类功能函数体
        public clsTemplate_DetailValue[] lngGetSingleKeywordTemplates(int p_intKeywordType, string p_strFormID, string p_strControlID, string p_strKeyWord, string p_strEmployeeID, string p_strDepartmentID)
        {
            clsTemplate_DetailValue[] objTemplate_DetailArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetSingleKeywordTemplates(p_intKeywordType, p_strFormID, p_strControlID, p_strKeyWord, p_strEmployeeID, p_strDepartmentID, out objTemplate_DetailArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplate_DetailArr);
        }
        #endregion


        #region 获得所有单元模板的ICD-10模板,Domain层,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsTemplate_DetailValue[] lngGetSingleICD_10Templates(string p_strFormID, string p_strControlID, string p_strEmployeeID, string p_strDepartmentID)
        {
            clsTemplate_DetailValue[] objTemplate_DetailArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetSingleICD_10Templates(p_strFormID, p_strControlID, p_strEmployeeID, p_strDepartmentID, out objTemplate_DetailArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplate_DetailArr);
        }
        #endregion

        #region 获得所有单元模板的ICD-10模板,Domain层,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsTemplate_DetailValue[] lngGetSingleBio_SystemTemplates(string p_strFormID, string p_strControlID, string p_strEmployeeID, string p_strDepartmentID)
        {
            clsTemplate_DetailValue[] objTemplate_DetailArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetSingleBio_SystemTemplates(p_strFormID, p_strControlID, p_strEmployeeID, p_strDepartmentID, out objTemplate_DetailArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplate_DetailArr);
        }
        #endregion


        #region 得到所有的TemplatesetKeyword,Domain层
        //在此编写同类功能函数体
        public clsTemplate_Set_KeywordValue[] lngGetSetTemplateKeyword(string p_strFormID,
            string p_strControl_ID, string p_strEmployeeID, string p_strDepartmentID)
        {
            clsTemplate_Set_KeywordValue[] objTemplate_Set_KeywordValue = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetSetTemplateKeyword(p_strFormID, p_strControl_ID, p_strEmployeeID, p_strDepartmentID, out objTemplate_Set_KeywordValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplate_Set_KeywordValue);
        }

        /// <summary>
        /// 查看当前输入框是否有模板
        /// </summary>
        /// <param name="p_strFormID"></param>
        /// <param name="p_strControl_ID"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strDepartmentID"></param>
        /// <returns></returns>
        public bool m_blnHaveTempateSet(string p_strFormID, string p_strControl_ID,
            string p_strEmployeeID, string p_strDepartmentID)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            bool blnRes = false;
            try
            {
                blnRes = (new weCare.Proxy.ProxyEmr02()).Service.m_blnHaveTempateSet(
                p_strFormID, p_strControl_ID, p_strEmployeeID, p_strDepartmentID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return blnRes;
        }

        #endregion

        #region 得到所有的TemplatesetKeywordName,Domain层
        //在此编写同类功能函数体
        public clsTemplateSet_ID_NameValue[] lngGetSetTemplateKeywordName(string p_strFormID,
            string p_strControl_ID, string p_strKeyword, string p_strEmployeeID, string p_strDepartmentID)
        {
            clsTemplateSet_ID_NameValue[] objTemplateSet_ID_NameArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetSetTemplateKeywordName(
                p_strFormID, p_strControl_ID, p_strKeyword, p_strEmployeeID, p_strDepartmentID, out objTemplateSet_ID_NameArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplateSet_ID_NameArr);
        }

        #endregion

        #region 得到所有的TemplatesetICD-10,Domain层,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsTemplateSet_ID_NameValue[] lngGetSetTemplateICD_10(string p_strFormID, string p_strEmployeeID, string p_strDepartmentID)
        {
            clsTemplateSet_ID_NameValue[] objTemplateSet_ID_NameArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetSetTemplateICD_10(p_strFormID, p_strEmployeeID, p_strDepartmentID, out objTemplateSet_ID_NameArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplateSet_ID_NameArr);
        }

        #endregion

        #region 得到所有的TemplatesetICD-10,Domain层,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsTemplateSet_ID_NameValue[] lngGetSetTemplateBio_System(string p_strFormID, string p_strEmployeeID, string p_strDepartmentID)
        {
            clsTemplateSet_ID_NameValue[] objTemplateSet_ID_NameArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetSetTemplateBio_System(p_strFormID, p_strEmployeeID, p_strDepartmentID, out objTemplateSet_ID_NameArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplateSet_ID_NameArr);
        }

        #endregion

        #region 得到所有的套装模板内容,Domain层,刘颖源,2003-5-7 19:24:10
        //在此编写同类功能函数体
        public clsTemplatesetContentValue[] lngGetAllTemplatesetContent(string p_strSetID)
        {
            clsTemplatesetContentValue[] objTemplatesetContentArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllTemplatesetContent(p_strSetID, out objTemplatesetContentArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplatesetContentArr);
        }
        #endregion


        #region 得到单元模板的信息,刘颖源,2003-5-15 9:09:29
        public long lngGetTemplateUnit(string p_strTemplateID, out clsTemplateValue[] p_objTemplateValue, out clsTemplate_KeywordValue[] p_objTemplate_KeywordValue,
            out clsTemplate_DetailValue[] p_objTemplate_DetailValue, out clsTemplate_Target_Gui_InfoValue[] p_objTemplate_Target_Gui_InfoValue, out clsTemplate_Dept_VisibilityValue[] p_objTemplate_Dept_VisibilityValue)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr02()).Service.lngGetTemplateUnit(p_strTemplateID, out p_objTemplateValue, out p_objTemplate_KeywordValue, out p_objTemplate_DetailValue, out p_objTemplate_Target_Gui_InfoValue, out p_objTemplate_Dept_VisibilityValue));
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region 是否存在此模板,刘颖源,2003-5-15 11:11:41
        public clsTemplateValue lngGetTemplate(string p_strID)
        {
            clsTemplateValue objTemplateValue = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetTemplate(p_strID, out objTemplateValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplateValue);
        }
        #endregion

        #region 是否存在此套装模板,刘颖源,2003-5-15 11:11:41
        public clsTempate_SetValue lngGetTemplateSet(string p_strID)
        {
            clsTempate_SetValue objTemplateValue = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetTemplateSet(p_strID, out objTemplateValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplateValue);
        }
        #endregion

        #region 得到所有的Template_Detail,Domain层,刘颖源,2003-5-7 19:24:10
        public clsTemplate_DetailValue[] lngGetEmployeeTemplateIDs(string p_strEmployeeID)
        {
            clsTemplate_DetailValue[] objTemplate_DetailArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetEmployeeTemplateIDs(p_strEmployeeID, out objTemplate_DetailArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplate_DetailArr);
        }
        #endregion

        public clsTemplate_DetailValue[] lngGetEmployeeTemplateIDsByForm(string p_strEmployeeID, string p_strFormName)
        {
            clsTemplate_DetailValue[] objTemplate_DetailArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetEmployeeTemplateIDsByForm(p_strEmployeeID, p_strFormName, out objTemplate_DetailArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplate_DetailArr);
        }

        #region 获得套装模板信息,刘颖源,2003-5-15 15:25:48
        public long lngGetTemplateSetInfo(string p_strSet_ID, out clsTempate_SetValue[] p_objTempate_SetValue, out clsTemplate_Set_KeywordValue[] p_objTemplate_Set_KeywordValue, out clsTemplateSet_Gui_TargetValue[] p_objTemplateSet_Gui_TargetValue, out clsTemplate_Set_Detail_01Value[] p_objTemplate_Set_Detail_01Value)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetTemplateSetInfo(p_strSet_ID, out p_objTempate_SetValue, out p_objTemplate_Set_KeywordValue, out p_objTemplateSet_Gui_TargetValue, out p_objTemplate_Set_Detail_01Value);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region ,刘颖源,2003-5-15 16:58:17
        public long lngGetAllEmployeeTemplateSet(string p_strEmployeeID, out clsEmployeeTemplateSetValue[] p_objEmployeeTemplateSetValue)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr02()).Service.lngGetAllEmployeeTemplateSet(p_strEmployeeID, out p_objEmployeeTemplateSetValue));
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region 新的模板逻辑
        /// <summary>
        /// 查找某位员工记录的某张记录单下的所有模板
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_objEmployeeTemplateSetValue"></param>
        /// <returns></returns>
        public long lngGetAllEmployeeTemplateSetByForm(string p_strEmployeeID, string p_strFormName,
            out clsEmployeeTemplateSetValue[] p_objEmployeeTemplateSetValue)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllEmployeeTemplateSetByForm(clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID, p_strEmployeeID, p_strFormName, out p_objEmployeeTemplateSetValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 查找某位员工所见科室的某张记录单下的所有模板(该员工为主任医师或护士长)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_objEmployeeTemplateSetValue"></param>
        /// <returns></returns>
        public long lngGetAllEmployeeDeptTemplateSetByForm(string[] p_strArrDeptID, string p_strEmployeeID, string p_strFormName,
            out clsEmployeeTemplateSetValue[] p_objEmployeeTemplateSetValue)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllEmployeeDeptTemplateSetByForm(p_strArrDeptID, p_strEmployeeID, p_strFormName, out p_objEmployeeTemplateSetValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public long m_lngGetDeptArr_EmployeeCanManage(string p_strEmployeeID, out clsDept_Desc[] p_objDeptArr)
        {
            //clsDepartmentManagerService m_objServ =
            //    (clsDepartmentManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDepartmentManagerService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetDeptArr_EmployeeCanManage(p_strEmployeeID, out p_objDeptArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 查找某位员工记录的某张记录单下的所有模板关键字
        /// </summary>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_dtKeyword"></param>
        /// <returns></returns>
        public long lngGetAllEmployeeTemplateKeywordByForm(string p_strEmployeeID,
            string p_strFormName, out System.Data.DataTable p_dtKeyword)
        {
            p_dtKeyword = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllEmployeeTemplateKeywordByForm(p_strEmployeeID, p_strFormName, out p_dtKeyword);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public long lngGetAllEmployeeTemplateSetByFormAndKeyword(string p_strEmployeeID, string p_strFormName, string p_strKeyword,
            out clsEmployeeTemplateSetValue[] p_objEmployeeTemplateSetValue)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr02()).Service.lngGetAllEmployeeTemplateSetByFormAndKeyword(clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID, p_strEmployeeID, p_strFormName, p_strKeyword, out p_objEmployeeTemplateSetValue));
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 得到模板具体内容
        /// </summary>
        /// <param name="p_strTemplateID"></param>
        /// <returns></returns>
        public long m_lngGetTemplateContent(string p_strTemplateID, out string p_strTemplateContent, out string p_strCreateID, out string p_strCreateName)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_strGetTemplateContent(p_strTemplateID, out p_strTemplateContent, out p_strCreateID, out p_strCreateName);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public long m_lngGetAllTemplate(string p_strFormName, out clsEmployeeTemplateSetValue[] p_objEmployeeTemplateSetValue)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.lngGetAllTemplate(p_strFormName, out p_objEmployeeTemplateSetValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 修改模板具体内容
        /// </summary>
        /// <param name="p_strTemplateID"></param>
        /// <param name="p_strContent"></param>
        /// <returns></returns>
        public long m_lngModifyTemplateContent(string p_strTemplateID, string p_strContent, string p_strVisibility_Level)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngModifyTemplateContent(p_strTemplateID, p_strContent, p_strVisibility_Level);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 修改模板可见级别
        /// </summary>
        /// <param name="p_strTemplateID"></param>
        /// <param name="p_strVisibility_Level"></param>
        /// <returns></returns>
        public long m_lngModifyTemplateVisibitity(string p_strTemplateID, string p_strVisibility_Level)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngModifyTemplateVisibitity(p_strTemplateID, p_strVisibility_Level);
            }
            finally
            {
                //m_objServ = null;
            }
            return lngRes;
        }

        /// <summary>
        /// 停用模板
        /// </summary>
        /// <param name="p_strSetID"></param>
        /// <returns></returns>
        public long m_lngDeleteTemplate(string p_strSetID)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngDeleteTemplate(p_strSetID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 修改模板基本信息
        /// </summary>
        /// <param name="p_strSetID"></param>
        /// <param name="p_strSetName"></param>
        /// <param name="p_strKeyword"></param>
        /// <returns></returns>
        public long m_lngModifyTemplateBaseInfo(string p_strSetID, string p_strSetName, string p_strKeyword)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngModifyTemplateBaseInfo(p_strSetID, p_strSetName, p_strKeyword);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }


        /// <summary>
        /// 查找表单下的关键字
        /// </summary>
        /// <param name="p_strForm"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngGetKeywordsByForm(string p_strForm, out System.Data.DataTable p_dtResult)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetKeywordsByForm(
                p_strForm, MDIParent.OperatorID, clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID,
                out p_dtResult);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        #endregion

        #region 模板串联
        /// <summary>
        /// 存套装模板所关联的字段
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long lngSaveTemplateSet_Associate(clsTemplateSet_Associate p_objContent)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr02()).Service.lngSaveTemplateSet_Associate(p_objContent));
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public long m_lngGetAllAssociate(int p_intType, out clsTemplateSet_Associate[] p_objArr)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr02()).Service.m_lngGetAllAssociate(
                clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID, p_intType, out p_objArr));
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public long m_lngGetAssociateBySetID(string p_strSetID, int p_intType, out clsTemplateSet_Associate p_objValue)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                string strDeptID = "";
                if (clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID != null)
                {
                    strDeptID = clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID;
                }
                lngRes = ((new weCare.Proxy.ProxyEmr02()).Service.m_lngGetAssociateBySetID(strDeptID, p_strSetID, p_intType, out p_objValue));
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public string m_strGetAssociateIDBySetID(string p_strSetID, int p_intType)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            string strRes = "";
            try
            {
                strRes = ((new weCare.Proxy.ProxyEmr02()).Service.m_strGetAssociateIDBySetID(p_strSetID, p_intType, clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID));
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return strRes;
        }

        public string m_strGetAssociateIDByAssociateName(string p_strAssociateName, int p_intType)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            string strRes = "";
            try
            {
                strRes = (new weCare.Proxy.ProxyEmr02()).Service.m_strGetAssociateIDByAssociateName(p_strAssociateName, p_intType, clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return strRes;
        }

        public clsTemplatesetContentValue[][] m_lngGetSpecialPatientTemplateSet(string p_strInPatientID,
            string p_strInPatientDate, string p_strFormName, int p_intType)
        {
            clsTemplatesetContentValue[][] objTemplatesetContentArr = null;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetSpecialPatientTemplateSet(
                p_strInPatientID, p_strInPatientDate, p_strFormName, p_intType, clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID, out objTemplatesetContentArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return (objTemplatesetContentArr);
        }

        /// <summary>
        /// 获取已有病名的病人的套装模板ID,在这里只取一个。
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_intType"></param>
        /// <returns></returns>
        public string m_strGetPatientHaveDisease_TemplateSetID(string p_strInPatientID,
            string p_strInPatientDate, string p_strFormName, int p_intType)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            string strRes = "";
            try
            {
                strRes = (new weCare.Proxy.ProxyEmr02()).Service.m_strGetPatientHaveDisease_TemplateSetID(
                p_strInPatientID, p_strInPatientDate, p_strFormName, p_intType, clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return strRes;
        }

        public long m_lngSavePatient_Associate(clsPatient_Associate p_objContent, int p_intType)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngSavePatient_Associate(p_objContent, p_intType, clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        #endregion

        /// <summary>
        /// 获取套装模板内容
        /// </summary>
        public long m_lngGetTemplateSetValue(string p_strFormID, string p_strControl_ID, string p_strEmployeeID, string p_strDepartmentID, out clsTemplateSetValue[] p_objValue)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetTemplateSetValue(p_strFormID, p_strControl_ID, p_strEmployeeID, p_strDepartmentID, out p_objValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 获取套装模板内容,关键字模糊查找
        /// </summary>
        public long m_lngGetTemplateSetValue(string p_strFormID, string p_strControl_ID, string p_strLikeKeyword, string p_strEmployeeID, string p_strDepartmentID, out clsTemplateSetValue[] p_objValue)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetTemplateSetValue(p_strFormID, p_strControl_ID, p_strLikeKeyword, p_strEmployeeID, p_strDepartmentID, out p_objValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        #region icd10相关操作
        /// <summary>
        /// 保存模板与疾病的关系
        /// </summary>
        /// <param name="p_lsvTemp"></param>
        /// <param name="p_strTempateSet_ID"></param>
        /// <returns></returns>
        public long m_lngSaveICD10_TemplateSet(System.Windows.Forms.ListView p_lsvTemp, string p_strTempateSet_ID)
        {
            long lngRet = 0;

            string strTempateSet_ID = p_strTempateSet_ID.Replace("'", "''");

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            try
            {
                lngRet = (new weCare.Proxy.ProxyEmr02()).Service.m_lngSaveICD10_TemplateSet("", strTempateSet_ID);
                if (p_lsvTemp.Items.Count > 0)
                {
                    for (int i = 0; i <= p_lsvTemp.Items.Count - 1; i++)
                    {
                        string strIcd_ID = p_lsvTemp.Items[i].Tag.ToString();
                        lngRet = (new weCare.Proxy.ProxyEmr02()).Service.m_lngSaveICD10_TemplateSet(strIcd_ID, strTempateSet_ID);
                        if (lngRet == -1)
                            return -1;
                    }
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRet;
        }

        public long m_lngGetICD10_TemplateSet(System.Windows.Forms.ListView lsvTemp, string p_strTempateSet_ID)
        {
            System.Data.DataTable dtRecords = null;
            long lngRet = 0;

            if (p_strTempateSet_ID == null || p_strTempateSet_ID.Trim().Length == 0)
                return -1;

            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            try
            {
                lngRet = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetICD10_TemplateSet(p_strTempateSet_ID, out dtRecords);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (lngRet == -1)
                return -1;
            if (dtRecords == null || dtRecords.Rows.Count <= 0)
                return -1;
            for (int i = 0; i <= dtRecords.Rows.Count - 1; i++)
            {
                System.Windows.Forms.ListViewItem lviTemp = lsvTemp.Items.Add(dtRecords.Rows[i]["icd_name"].ToString());
                lviTemp.Tag = dtRecords.Rows[i]["id"].ToString();
            }
            return lngRet;
        }

        public long m_lngGetICD10IDToTemplateSetName(string p_strICD_ID, string p_strFormID, System.Windows.Forms.ListView lsvTemp)
        {
            System.Data.DataTable dtRecords = null;
            long lngRet = 0;
            if (p_strICD_ID != null || p_strICD_ID.Trim().Length > 0)
            {
                //clsTemplateService m_objServ =
                //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

                try
                {
                    lngRet = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetTemplateSet_ForICD10ID(p_strICD_ID, p_strFormID, out dtRecords);
                }
                finally
                {
                    //m_objServ.Dispose();
                }
                if (dtRecords == null || dtRecords.Rows.Count <= 0)
                    return -1;
                for (int i = 0; i <= dtRecords.Rows.Count - 1; i++)
                {
                    System.Windows.Forms.ListViewItem lviTemp = lsvTemp.Items.Add(dtRecords.Rows[i]["Set_Name"].ToString());
                    lviTemp.Tag = dtRecords.Rows[i]["Set_ID"].ToString();
                }
                return lngRet;
            }
            else
            {
                return -1;
            }

        }
        #endregion

        /// <summary>
        /// 根据模板ID查找
        /// </summary>
        /// <param name="p_strTemplateID">模板ID</param>
        /// <param name="strEmpID">创建者工号</param>
        /// <param name="strVisibilityLevel">可视级别(0：创建者可视，1：公用，2：科室可视)</param>
        /// <returns></returns>
        public long m_lngGetTemplateVLandEmpID(string p_strTemplateID, out string p_strEmpID, out string p_strVisibilityLevel, out string p_strActivityDate)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetTemplateVLandEmpID(p_strTemplateID, out p_strEmpID, out p_strVisibilityLevel, out p_strActivityDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 根据模板ID查找可视的科室ID
        /// </summary>
        /// <param name="p_strTemplateID">模板ID</param>
        /// <param name="p_arrTemplateID">可视的科室ID</param>
        /// <returns></returns>
        public long m_lngGetDeptIDByTemplateID(string p_strTemplateID, out System.Collections.Generic.List<string[]> p_arrDeptID)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetDeptIDByTemplateID(p_strTemplateID, out p_arrDeptID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 删除表Template_Dept_Visibility的相关记录
        /// </summary>
        /// <param name="p_strTemplateID"></param>
        /// <returns></returns>
        public long m_lngDeleteTemplate_Dept_Visibility(string p_strTemplateID)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngDeleteTemplate_Dept_Visibility(p_strTemplateID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 保存表TEMPLATE_DEPT_VISIBILITY
        /// </summary>
        /// <param name="p_objTemplate_Dept_Visiblity"></param>
        /// <returns></returns>
        public long m_lngSaveTemplate_Dept_Visibility(clsTemplate_Dept_VisibilityValue[] p_objTemplate_Dept_Visiblity)
        {
            //clsTemplateService m_objServ =
            //    (clsTemplateService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTemplateService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngSaveTemplate_Dept_Visibility(p_objTemplate_Dept_Visiblity);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
    }
}
