using System;
using weCare.Core.Entity;
using System.Collections;

namespace iCare
{
    /// <summary>
    /// Summary description for clsTemplateDomain.
    /// Domain����룬��ɼ򵥵��жϺ���ҵ�߼�,��ӱԴ,2003-5-7 16:28:56
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

        #region ����ģ��,��ӱԴ,2003-5-7 19:24:10
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

        #region ����Ƿ��Ѵ��ڵ�ǰ�����µ�ǰ������ģ��
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

        #region ������װģ��,�������޸�
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


        #region �õ���װģ��ID,��ӱԴ,2003-5-9 11:05:50
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

        #region ����Ƿ��Ѵ��ڵ�ǰ�����µ�ģ��
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


        #region �õ����е�Form,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region �õ����е�Control,��ӱԴ,2003-5-7 20:09:20
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region �õ����е�ICD10_IllnessID,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region �õ����е�ICD10_IllnessSubID,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region �õ����е�ICD10_IllnessDetailID,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region �õ����е�Bio_System,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region �õ����е�Bio_System_Detail,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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


        #region �õ����е�TemplateFormControl,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region ������е�Ԫģ��Ĺؼ���,Domain��
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region ������е�Ԫģ��Ĺؼ����µ�ģ������
        //�ڴ˱�дͬ�๦�ܺ�����
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


        #region ������е�Ԫģ���ICD-10ģ��,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region ������е�Ԫģ���ICD-10ģ��,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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


        #region �õ����е�TemplatesetKeyword,Domain��
        //�ڴ˱�дͬ�๦�ܺ�����
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
        /// �鿴��ǰ������Ƿ���ģ��
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

        #region �õ����е�TemplatesetKeywordName,Domain��
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region �õ����е�TemplatesetICD-10,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region �õ����е�TemplatesetICD-10,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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

        #region �õ����е���װģ������,Domain��,��ӱԴ,2003-5-7 19:24:10
        //�ڴ˱�дͬ�๦�ܺ�����
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


        #region �õ���Ԫģ�����Ϣ,��ӱԴ,2003-5-15 9:09:29
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

        #region �Ƿ���ڴ�ģ��,��ӱԴ,2003-5-15 11:11:41
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

        #region �Ƿ���ڴ���װģ��,��ӱԴ,2003-5-15 11:11:41
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

        #region �õ����е�Template_Detail,Domain��,��ӱԴ,2003-5-7 19:24:10
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

        #region �����װģ����Ϣ,��ӱԴ,2003-5-15 15:25:48
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

        #region ,��ӱԴ,2003-5-15 16:58:17
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

        #region �µ�ģ���߼�
        /// <summary>
        /// ����ĳλԱ����¼��ĳ�ż�¼���µ�����ģ��
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
        /// ����ĳλԱ���������ҵ�ĳ�ż�¼���µ�����ģ��(��Ա��Ϊ����ҽʦ��ʿ��)
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
        /// ����ĳλԱ����¼��ĳ�ż�¼���µ�����ģ��ؼ���
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
        /// �õ�ģ���������
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
        /// �޸�ģ���������
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
        /// �޸�ģ��ɼ�����
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
        /// ͣ��ģ��
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
        /// �޸�ģ�������Ϣ
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
        /// ���ұ��µĹؼ���
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

        #region ģ�崮��
        /// <summary>
        /// ����װģ�����������ֶ�
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
        /// ��ȡ���в����Ĳ��˵���װģ��ID,������ֻȡһ����
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
        /// ��ȡ��װģ������
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
        /// ��ȡ��װģ������,�ؼ���ģ������
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

        #region icd10��ز���
        /// <summary>
        /// ����ģ���뼲���Ĺ�ϵ
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
        /// ����ģ��ID����
        /// </summary>
        /// <param name="p_strTemplateID">ģ��ID</param>
        /// <param name="strEmpID">�����߹���</param>
        /// <param name="strVisibilityLevel">���Ӽ���(0�������߿��ӣ�1�����ã�2�����ҿ���)</param>
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
        /// ����ģ��ID���ҿ��ӵĿ���ID
        /// </summary>
        /// <param name="p_strTemplateID">ģ��ID</param>
        /// <param name="p_arrTemplateID">���ӵĿ���ID</param>
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
        /// ɾ����Template_Dept_Visibility����ؼ�¼
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
        /// �����TEMPLATE_DEPT_VISIBILITY
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
