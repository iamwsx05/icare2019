using System;
using weCare.Core.Entity;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_DoctorWorkstation ��ժҪ˵����
    /// </summary>
    public class clsDcl_DoctorWorkstation : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_DoctorWorkstation()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP();
            }
        }
        #endregion

        #region �����շ���Ŀ
        public long m_mthFindWMedicineByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, string p_strRealMedStoreID, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindWMedicineByID(strType, ID.ToUpper(), strPatientTypeID, out dt, strEmployID, p_strRealMedStoreID, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindCMedicineByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, string m_strRealMedStoreID, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindCMedicineByID(strType, ID, strPatientTypeID, out dt, strEmployID, m_strRealMedStoreID, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindTestChargeByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindTestChargeByID(strType, ID, strPatientTypeID, out dt, strEmployID, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindExamineChargeByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindExamineChargeByID(strType, ID, strPatientTypeID, out dt, strEmployID, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindOPSChargeByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindOPSChargeByID(strType, ID, strPatientTypeID, out dt, strEmployID, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindOPSChargeByID(string strType, string ID, string strPatientTypeID, string strEmployID, out DataTable dt, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindOPSChargeByID(strType, ID, strPatientTypeID, strEmployID, out dt, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindOPSChargeByID(string ID, string strPatientTypeID, out DataTable dt, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindOPSChargeByID(ID, strPatientTypeID, out dt, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindOtherChargeByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindOtherChargeByID(strType, ID, strPatientTypeID, out dt, strEmployID, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����Ƶ��
        /// <summary>
        /// ����Ƶ��
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindFrequency(string ID, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindFrequency(ID, out dt);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����÷�
        /// <summary>
        /// �����÷�
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindUsage(string ID, int scope, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindUsage(ID, scope, out dt);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="OPR_VO"></param>
        /// <param name="CH_VO"></param>
        /// <param name="DR_VO"></param>
        /// <param name="PWM_VO"></param>
        /// <param name="CM_VO"></param>
        /// <param name="CHK_VO"></param>
        /// <param name="TR_VO"></param>
        /// <param name="OP_VO"></param>
        /// <param name="Other_VO"></param>
        /// <param name="strRecipeID"></param>
        /// <param name="strCaseHisID"></param>
        /// <param name="IsModify"></param>
        /// <param name="blnCashStatus"></param>
        /// <param name="blnSaveCash"></param>
        /// <returns></returns>
        public long m_mthSaveAllData(clsOutPatientRecipe_VO OPR_VO, clsOutpatientCaseHis_VO CH_VO, clsOutpatientDiagRec_VO DR_VO, clsOutpatientPWMRecipeDe_VO[] PWM_VO, clsOutpatientCMRecipeDe_VO[] CM_VO,
            clsOutpatientCHKRecipeDe_VO[] CHK_VO, clsOutpatientTestRecipeDe_VO[] TR_VO,
            clsOutpatientOPSRecipeDe_VO[] OP_VO, clsOutpatientOtherRecipeDe_VO[] Other_VO, clsOutpatientOrderRecipeDe_VO[] Order_VO,
            out string strRecipeID, out string strCaseHisID, bool IsModify, bool blnCashStatus, bool blnSaveCash)
        {
            strRecipeID = "";
            strCaseHisID = "";
            long lngRes = 0;
            try
            {
                //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
                //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
                lngRes = proxy.Service.m_mthSaveAllData(OPR_VO, CH_VO, DR_VO, PWM_VO, CM_VO, CHK_VO, TR_VO, OP_VO, Other_VO, Order_VO, out strRecipeID, out strCaseHisID, IsModify, blnCashStatus, blnSaveCash);
                // objSvc.Dispose();
            }
            catch
            { }
            return lngRes;
        }
        #endregion

        #region ���没��
        /// <summary>
        /// ���没��
        /// </summary>
        /// <param name="CH_VO"></param>
        /// <param name="strCaseHisID"></param>
        /// <param name="IsModify"></param>
        /// <returns></returns>
        public long m_lngSaveCaseHis(clsOutpatientCaseHis_VO CH_VO, bool IsModify, ref string strCashID)
        {
            long lngRes = 0;
            try
            {
                //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
                //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
                lngRes = proxy.Service.m_mthSaveCaseHistory(CH_VO, IsModify, ref strCashID);
                // objSvc.Dispose();
            }
            catch
            { }
            return lngRes;
        }
        #endregion

        #region ɾ��������ϸ
        /// <summary>
        /// ɾ��������ϸ
        /// </summary>
        /// <param name="ID"></param>
        public void m_mthDeleteRecipeDetail(string ID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            proxy.Service.m_mthDeleteRecipeDetail(ID);
            // objSvc.Dispose();
        }

        #endregion

        #region ��ʾ������
        /// <summary>
        /// ��ʾ������
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dt"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public long m_mthGetRepiceNo(string type, out DataTable dt, string ID)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthGetRepiceNo(type, out dt, ID);
            // objSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region ���ݴ����Ų�����ϸ
        /// <summary>
        /// ���ݴ����Ų�����ϸ
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public long m_mthFindDiagnoses(string ID, out DataTable dt)
        {
            dt = new DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindDiagnoses(ID, out dt);
            // objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public long m_mthFindCaseHistory(string ID, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindCaseHistory(ID, out dt);
            // objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ���첡��
        /// </summary>
        /// <param name="strPatientID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthGetMaxCaseHistory(string strPatientID, string strDoctor, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindMaxCaseHistory(strPatientID, strDoctor, out dt);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindRecipeDetail1(string ID, out DataTable dt, bool flag, bool isChildPrice)//��ҩ
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindRecipeDetail1(ID, out dt, flag, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindRecipeDetail2(string ID, out DataTable dt, bool flag, bool isChildPrice)//��ҩ
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindRecipeDetail2(ID, out dt, flag, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindRecipeDetail3(string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindRecipeDetail3(ID, out dt, flag, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindRecipeDetail3ByOrder(string RecID, string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindRecipeDetail3ByOrder(RecID, ID, out dt, flag, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindRecipeDetail4(string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindRecipeDetail4(ID, out dt, flag, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindRecipeDetail4ByOrder(string RecID, string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindRecipeDetail4ByOrder(RecID, ID, out dt, flag, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindRecipeDetail5(string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindRecipeDetail5(ID, out dt, flag, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindRecipeDetail5ByOrder(string RecID, string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindRecipeDetail5ByOrder(RecID, ID, out dt, flag, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindRecipeDetail6(string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindRecipeDetail6(ID, out dt, flag, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }

        public long m_mthFindRecipeDetailOrder(string ID, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindRecipeDetailOrder(ID, out dt);
            // objSvc.Dispose();
            return lngRes;
        }

        #endregion

        #region �շѸ��ݴ����Ų�ѯ������ϸ
        public long m_lngGetRecipeDetail(string p_strRecipeNo, out DataTable dtResult)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_lngGetRecipeDetail(p_strRecipeNo, out dtResult);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����÷����Ҹ��ӵ��շ���Ŀ
        /// <summary>
        /// �����÷����Ҹ��ӵ��շ���Ŀ
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthGetChargeItemByUsageID(string strID, out DataTable dt, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthGetChargeItemByUsageID(strID, out dt, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }

        /// <summary>
        /// �����÷����Ҹ��ӵ��շ���Ŀ
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthGetChargeItemByUsageID(string strID, string p_strPayTypeID, out DataTable dt, bool isChildPrice)
        {
            dt = null;
            if (p_strPayTypeID == string.Empty)
            {
                p_strPayTypeID = "0001";
            }
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthGetChargeItemByUsageID(strID, p_strPayTypeID, out dt, isChildPrice);
            // objSvc.Dispose();
            return lngRes;
        }

        #endregion

        #region ����ģ���б�
        /// <summary>
        /// ����ģ���б�
        /// </summary>
        /// <param name="ID">��ѯ�ؼ���</param>
        public long m_mthFindAccordRecipe(string ID, string strType, string strCreatID, out DataTable dt, int flag)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindAccordRecipe(ID, strType, strCreatID, out dt, flag);
            // objSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region �������뵥����ĿԴID�����շ���Ŀ
        /// <summary>
        /// �������뵥����ĿԴID�����շ���Ŀ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindChargeItemByApplyBillID(string ID, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindChargeItemByApplyBillID(ID, out dt);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ������ĿID�������ҩƷ
        /// <summary>
        /// ������ĿID�������ҩƷ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindTabuByID(string ID, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthFindTabuByID(ID, out dt);
            // objSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region ��ȡҩƷ��ϸ��Ϣ
        /// <summary>
        /// ��ȡҩƷ��ϸ��Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="strText"></param>
        public void m_mthGetMedicineInfo(string ID, out string strText, out string strRemark)
        {
            strText = "";
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            proxy.Service.m_mthGetMedicineInfo(ID, out strText, out strRemark);
            // objSvc.Dispose();
        }
        #endregion

        #region ��ȡ����Һ�
        /// <summary>
        /// ��ȡ����Һ�
        /// </summary>
        /// <param name="strDoctor"></param>
        /// <param name="strDep"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetWaitReg(string strDoctor, string strDep, out DataTable dt)
        {

            dt = new DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_lngGetGeg(strDoctor, strDep, out dt);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ת�����
        /// <summary>
        /// ת�����
        /// </summary>
        /// <param name="objVo"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public long m_lngAddNewTake(clsTakeDiagrec objVo, out string ID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_lngAddNewTakeDiagrec(out ID, objVo);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ϴ���
        /// <summary>
        /// ���ϴ���
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="p_status"></param>
        /// <returns></returns>
        public long m_mthDelRecipe(string ID, string p_status)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthDelRecipe(ID, p_status);
            // objSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region ��ȡ����Һ�
        /// <summary>
        /// ��ȡ����Һ�
        /// </summary>
        /// <param name="strDoctor"></param>
        /// <param name="strBeginDate"></param>
        /// <param name="strEndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetTakeGeg(string strDoctor, string strBeginDate, string strEndDate, out DataTable dt)
        {
            dt = new DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_lngGetTakeGeg(strDoctor, strBeginDate, strEndDate, out dt);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strRegID"></param>
        /// <param name="strWaitID"></param>
        /// <returns></returns>
        public long m_lngEndTakeReg(string strRegID, string strWaitID)
        {

            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_lngEndTakeReg(strRegID, strWaitID);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strWaitID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public long m_mthContinueTake(string strWaitID, int status)
        {

            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthContinue(strWaitID, status, "TAKEDIAGRECID_CHR");
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��鵱ǰ�Һ�״̬
        /// <summary>
        ///  ��鵱ǰ�Һ�״̬
        /// </summary>
        /// <param name="RegID"></param>
        /// <returns></returns>
        public long m_lngGetCurRegF(string RegID)
        {
            long lngFlag = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            lngFlag = proxy.Service.m_lngGetCurRegF(RegID);
            // objSvc.Dispose();
            return lngFlag;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strRegID"></param>
        /// <param name="strWaitID"></param>
        /// <returns></returns>
        public long m_lngReturnWait(string strRegID, string strWaitID)
        {

            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_lngReturnWait(strRegID, strWaitID);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���˿�������
        /// <summary>
        /// ��ȡ���˿�������
        /// </summary>
        /// <param name="strPatientID"></param>
        /// <returns></returns>
        public int m_mthGetPatientSeeDocTimes(string strPatientID)
        {
            int ret = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            ret = proxy.Service.m_mthGetPatientSeeDocTimes(strPatientID);
            // objSvc.Dispose();
            return ret;
        }
        #endregion

        #region ��ȡע������ID
        /// <summary>
        /// ��ȡע������ID
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthGetinjectInfo(out DataTable dt)
        {
            long ret = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            ret = proxy.Service.m_mthGetinjectInfo(out dt);
            // objSvc.Dispose();
            return ret;
        }
        #endregion

        #region ��ȡϵͳ��������
        /// <summary>
        /// ��ȡϵͳ��������
        /// </summary>
        /// <param name="p_flag"></param>
        /// <returns></returns>
        public bool m_mthIsCanDo(string p_flag)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            return (new weCare.Proxy.ProxyOP01()).Service.m_mthIsCanDo(p_flag) == 1;

        }
        #endregion

        #region ���Ҷ�Ӧ����Ϣ
        /// <summary>
        /// ���Ҷ�Ӧ����Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthRelationInfo(out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthRelationInfo(out dt);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���Ҵ���������Ϣ
        /// <summary>
        /// ���Ҵ���������Ϣ
        /// </summary>
        /// <param name="objRTVO"></param>
        /// <param name="strEx"></param>
        /// <returns></returns>
        public long m_mthGetRecipeTypeInfo(out clsRecipeType_VO[] objRTVO, string strEx)
        {
            objRTVO = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthGetRecipeTypeInfo(out objRTVO, strEx);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ڲ�������ʾ����Ŀ����
        /// <summary>
        /// ����ڲ�������ʾ����Ŀ����
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_mthGetUnDisplayCat(out DataTable dt, string strID)
        {

            long ret = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            ret = proxy.Service.m_mthGetUnDisplayCat(out dt, strID);
            // objSvc.Dispose();
            return ret;
        }
        #endregion

        #region ������ĿID�����������
        /// <summary>
        /// ������ĿID�����������
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthGetApplyTypeByID(string strID, out DataTable dt)
        {

            long ret = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            ret = proxy.Service.m_mthGetApplyTypeByID(strID, out dt);
            // objSvc.Dispose();
            return ret;
        }
        #endregion

        #region ������ĿID�����������
        /// <summary>
        /// ������ĿID�����������
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public string m_mthGetResourceIDByItemID(string strID)
        {

            string ret = "";
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            ret = proxy.Service.m_mthGetResourceIDByItemID(strID);
            // objSvc.Dispose();
            return ret;
        }
        #endregion

        #region �ύ
        /// <summary>
        /// �ύ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="objArr"></param>
        /// <param name="objOPSarr"></param>
        /// <returns></returns>
        public long m_mthPutIn(string ID, List<clsATTACHRELATION_VO> objArr, List<clsOutops_VO> objOPSarr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthPutIn(ID, objArr, objOPSarr);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҩ��ID��ҩƷ��ȡ��ǰ���ۿ�棨��ɽ��ʱ�ò������ۿ�棬��Ϊ���ʵ�ʿ�棩
        /// <summary>
        /// ����ҩ��ID��ҩƷ��ȡ��ǰ���ۿ�棨��ɽ��ʱ�ò������ۿ�棬��Ϊ���ʵ�ʿ�棩
        /// </summary>
        /// <param name="strStoreID">�ֿ�(ҩ��)ID</param>
        /// <param name="strItemID">�շ���ĿID</param>        
        /// <returns></returns>        
        public long m_lngGetTheoryAmountByMedID(string strStoreID, string strItemID, string strDeductType, out DataTable dtInventory)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc));

            long l = proxy.Service.m_lngGetTheoryAmountByMedID(strStoreID, strItemID, strDeductType, out dtInventory);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region �����շ���ĿID��ѯҩƷID
        /// <summary>
        /// �����շ���ĿID��ѯҩƷID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string m_strGetMedicineIDByChargeItemID(string ID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc));

            string s = proxy.Service.m_strGetMedicineIDByChargeItemID(ID);
            // objSvc.Dispose();

            return s;
        }
        #endregion

        #region ��ȡҩ��������Ϣ
        /// <summary>
        /// ��ȡҩ��������Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetMedStore(out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc));

            long l = proxy.Service.m_lngGetMedStore(out dt);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region ��ȡҩ������
        /// <summary>
        /// ��ȡҩ������
        /// </summary>
        /// <param name="strChrgItem"></param>
        /// <returns></returns>
        public string m_strGetOutSendMedStoretype(string strChrgItem)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            string strMedStoretype = (new weCare.Proxy.ProxyOP01()).Service.m_strGetOutSendMedStoretype(strChrgItem);
            //objSvc.Dispose();
            return strMedStoretype;
        }
        #endregion

        #region �ж��°��ҩ�� ��ͨҩ��ת����ҩ��
        /// <summary>
        /// �ж��°��ҩ�� ��ͨҩ��ת����ҩ��
        /// </summary>
        /// <param name="strOldStorageID"></param>
        /// <param name="strNewStorageID"></param>
        /// <returns></returns>
        public long m_lngGetWorkStorage(string strOldStorageID, out string strNewStorageID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol));

            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetWorkStorage(strOldStorageID, out strNewStorageID);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݲ����Ż�ȡ������Ϣ
        /// <summary>
        /// ���ݲ����Ż�ȡ������Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="objICD10"></param>
        /// <returns></returns>
        public long m_mthIllnessInfo(string ID, out clsICD10_VO[] objICD10)
        {
            long ret = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            ret = proxy.Service.m_mthIllnessInfo(ID, out objICD10);
            // objSvc.Dispose();
            return ret;
        }
        #endregion

        #region ������������
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="strType"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetLisSampletyType(string ID, string strType, out DataTable dt)
        {

            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_lngGetLisSampletyType(ID, strType, out dt);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ������������
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="ID">��ĿID</param>
        /// <param name="strex"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthLoadCheckPart(string ID, string strex, out DataTable dt)
        {

            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthLoadCheckPart(out dt, ID, strex);
            // objSvc.Dispose();
            return lngRes;
        }
        public long m_mthLoadCheckPartOrder(string ID, string strex, out DataTable dt)
        {

            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = proxy.Service.m_mthLoadCheckPartOrder(out dt, ID, strex);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����סԺ����
        /// <summary>
        /// ����סԺ����
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="objBIHVO"></param>
        /// <returns></returns>
        public long m_mthAddInHospitalApply(out string strID, clsT_Opr_Bih_Register_VO objBIHVO)
        {
            long lngRes = 0;
            strID = "";
            //com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc objS =
            //   (com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP01()).Service.m_lngAddNewBihRegister(out strID, objBIHVO);
            }
            catch
            {
                return 0;
            }
            //objS.Dispose();
            return lngRes;
        }
        #endregion

        #region ����סԺ����
        /// <summary>
        /// ����סԺ����
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="objBIHVO"></param>
        /// <returns></returns>
        public long m_mthUpdateInHospitalApply(string strID, clsT_Opr_Bih_Register_VO objBIHVO)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc objS =
            //    (com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP01()).Service.m_lngModifyBihRegisterInfoByVo(strID, objBIHVO);
            }
            catch
            {
                return 0;
            }
            //objS.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݲ���ID ���סԺ�źͲ��˲���סԺ����
        /// <summary>
        /// ���ݲ���ID ���סԺ�źͲ��˲���סԺ����
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindInhospitalTimesByID(string strID, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc objS =
            //    (com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc));
            lngRes = (new weCare.Proxy.ProxyIP01()).Service.m_mthFindInhospitalTimesByID(strID, out dt);
            //objS.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݲ���ID������ҩ���
        /// <summary>
        /// ���ݲ���ID������ҩ���
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strID"></param>
        /// <param name="strEx"></param>
        /// <returns></returns>
        public long m_mthGetUsingMedicineByPatientID(out DataTable dt, string strID, string strEx)
        {
            long ret = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            ret = proxy.Service.m_mthGetUsingMedicineByPatientID(out dt, strID, strEx);
            // objSvc.Dispose();
            return ret;

        }
        #endregion

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="dt"></param>
        /// <param name="strEx"></param>
        /// <returns></returns>
        public long m_mthGetChangePriceInfo(string strID, out DataTable dt, string strEx)
        {
            long ret = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            ret = proxy.Service.m_mthGetChangePriceInfo(strID, out dt, strEx);
            // objSvc.Dispose();
            return ret;

        }
        #endregion

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ	
        /// </summary>
        /// <param name="p_strFilter"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngGetAreaInfo(string p_strFilter, out clsT_BSE_DEPTDESC_VO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = new clsT_BSE_DEPTDESC_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP01()).Service.m_lngGetAreaInfo(p_strFilter, out p_objRecordArr);

            }
            catch
            {
                return 0;
            }
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ����ҽʦ��Ϣ
        /// <summary>
        /// ��ȡ����ҽʦ��Ϣ	
        /// </summary>
        /// <param name="p_strFilter"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngGetMainDoctor(string p_strFilter, out clsEmployee_VO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = new clsEmployee_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsPatientQuerySVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsPatientQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsPatientQuerySVC));
            try
            {
                lngRes = (new weCare.Proxy.ProxyHisBase()).Service.m_lngGetMainDoctor(p_strFilter, out p_objRecordArr);

            }
            catch
            {
                return 0;
            }
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡסԺ��
        /// <summary>
        /// ��ȡסԺ��	
        /// </summary>
        /// <param name="p_strFilter"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngGetInpatientID(out string p_strInPatientID)
        {
            long lngRes = 0;
            p_strInPatientID = "";
            //com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP01()).Service.m_lngGetInpatientID(out p_strInPatientID);

            }
            catch
            {
                return 0;
            }
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݲ���ID��ȡ������Ժ��Ϣ,��������д��Ժ�Ǽǿ�ʱ���ж�
        /// <summary>
        /// ���ݲ���ID��ȡ������Ժ��Ϣ,��������д��Ժ�Ǽǿ�ʱ���ж� 
        /// </summary>
        /// <param name="p_strInPatientID">����ID</param>
        /// <param name="p_intFlag">��־:0=��Ժ������ת��Ĳ�����Ϣ,��������Ժ�Ǽǿ�ʹ��;1=��Ժ������ת��Ĳ�����Ϣ,��Ժʱʹ��</param>
        /// <param name="p_dtbResult">�����</param>
        /// <returns></returns>
        public long m_lngGetPatientInHospitalInfo(string p_strInPatientID, int p_intFlag, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP01()).Service.m_lngGetPatientInHospitalInfo(p_strInPatientID, p_intFlag, out p_dtbResult);

            }
            catch
            {
                return 0;
            }
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �ж��շ���Ŀ�Ƿ�������
        /// <summary>
        /// �ж��շ���Ŀ�Ƿ�������
        /// </summary>
        /// <param name="p_strChargeItem"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public bool m_blnCheckSubChargeItem(string p_strChargeItem, out DataTable dtRecord, bool isChildPrice)
        {
            long lngRes = 0;
            bool blnRet = false;
            dtRecord = new DataTable();

            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            try
            {
                lngRes = proxy.Service.m_lngGetSubChargeItem(p_strChargeItem, out dtRecord, isChildPrice);
                if (lngRes > 0 && dtRecord.Rows.Count > 0)
                {
                    blnRet = true;
                }
            }
            catch
            {
                blnRet = false;
            }
            return blnRet;
        }
        #endregion

        #region �ж��շ���Ŀ�Ƿ�������Ŀ
        /// <summary>
        /// �ж��շ���Ŀ�Ƿ�������Ŀ
        /// </summary>		
        public bool m_blnIsSubChrgItem(string strSubChrgItem, string strChrgItem)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            bool lngRes = proxy.Service.m_blnIsSubChrgItem(strSubChrgItem, strChrgItem);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݲ����Ż�ȡ��Ч������
        /// <summary>
        /// ���ݲ����Ż�ȡ��Ч������
        /// </summary>
        /// <param name="strCaseID"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public bool m_blnGetRecipeIDByCaseID(string strCaseID, out DataTable dtRecord)
        {
            long lngRes = 0;
            bool blnRet = false;
            dtRecord = new DataTable();

            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            try
            {
                lngRes = proxy.Service.m_lngGetRecipeIDByCaseID(strCaseID, out dtRecord);
                if (lngRes > 0 && dtRecord.Rows.Count > 0)
                {
                    blnRet = true;
                }
            }
            catch
            {
                blnRet = false;
            }
            // objSvc.Dispose();
            return blnRet;
        }
        #endregion

        #region �ж��Ƿ���
        /// <summary>
        /// �ж��Ƿ���
        /// </summary>
        /// <param name="strRegTypeID"></param>
        /// <returns></returns>
        public bool m_blnCheckRegEmer(string strRegTypeID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            bool lngRes = proxy.Service.m_blnCheckRegiterType(strRegTypeID);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �жϴ����Ƿ����շ�
        /// <summary>
        /// �жϴ����Ƿ����շ�
        /// </summary>
        /// <param name="strRecID"></param>
        /// <returns></returns>
        public bool m_blnCheckRecipeChrg(string strRecID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            bool lngRes = proxy.Service.m_blnCheckRecipeChrg(strRecID);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡҽ������վ��״̬����
        /// <summary>
        /// ��ȡҽ������վ��״̬����
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetWSParm(string strType, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetWSParm(strType, out dt);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ������Ա�б�
        /// <summary>
        /// ������Ա�б�
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public long m_lngGetAllergiclist(out DataTable dtRecord, string DoctorID, string Status)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetAllergiclist(out dtRecord, DoctorID, Status);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ������Ϣ����ȷ��
        /// <summary>
        /// ������Ϣ����ȷ��
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="RecipeID"></param>
        /// <param name="AllergicMed"></param>
        /// <param name="AllergicDesc"></param>
        /// <returns></returns>
        public long m_lngUpdateallergic(string PatientID, string RecipeID, string AllergicMed, string AllergicDesc)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngUpdateallergic(PatientID, RecipeID, AllergicMed, AllergicDesc);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ���ݿ��ҡ�ԤԼʱ���ȡ�������������¼
        /// <summary>
        /// ���ݿ��ҡ�ԤԼʱ���ȡ�������������¼
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="bookingdate"></param>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public long m_lngGetOPSApply(out DataTable dtRecord, string bookingdate, string deptid, int flag, int ischrg)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetOPSApply(out dtRecord, bookingdate, deptid, flag, ischrg);
            // objSvc.Dispose();

            return ret;
        }
        /// <summary>
        /// ���ݿ��ҡ�ԤԼʱ���ȡ�������������¼
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="bookingdate"></param>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public long m_lngGetOPSApply(out DataTable dtRecord, string bookingdate, string deptid, int flag, int ischrg, bool p_blnPstauts_int_2)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetOPSApply(out dtRecord, bookingdate, deptid, flag, ischrg, p_blnPstauts_int_2);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region �������뵥�Ż�ȡ�������������¼
        /// <summary>
        /// �������뵥�Ż�ȡ�������������¼
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="applyid"></param>
        /// <returns></returns>
        public long m_lngGetOPSApply(out DataTable dtRecord, string applyid)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetOPSApply(out dtRecord, applyid);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ȷ�������������뵥
        /// <summary>
        /// ȷ�������������뵥
        /// </summary>
        /// <param name="applyid"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public long m_lngConfrimOPS(string applyid, string empid)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngConfrimOPS(applyid, empid);
            // objSvc.Dispose();

            return ret;
        }
        /// <summary>
        ///  ȷ�������������浥
        /// </summary>
        /// <param name="applyid"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public long m_lngConfrimOPSReport(string applyid, string empid)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngConfrimOPSReport(applyid, empid);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ���ؿ���ID�Ϳ�����������
        /// <summary>
        /// ���ؿ���ID�Ϳ�����������
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strDeptArr"></param>
        /// <returns></returns>
        public long m_lngGetDeptArr(string p_strLike, out DataTable p_strDeptArr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                           (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetDeptAreaByLike(p_strLike, out p_strDeptArr, "");
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ���ݲ���Ա���Ż�ȡID������������
        /// <summary>
        /// ���ݲ���Ա���Ż�ȡID������������
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="empno"></param>
        /// <returns></returns>
        public long m_lngGetempinfo(out DataTable dtRecord, string empno)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                            (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetempinfo(out dtRecord, empno);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ��������������¼��Ϣ
        /// <summary>
        /// ��������������¼��Ϣ
        /// </summary>
        /// <param name="applyid"></param>
        /// <param name="objops"></param>
        /// <returns></returns>
        public long m_lngSaveOPS(string applyid, clsOutops_VO objops)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                            (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngSaveOPS(applyid, objops);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region �������뵥�Ż�ȡ�������浥��Ϣ
        /// <summary>
        /// �������뵥�Ż�ȡ�������浥��Ϣ
        /// </summary>
        /// <param name="applyid"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetopsrecord(string applyid, out DataTable dtRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                            (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetopsrecord(applyid, out dtRecord);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ��ȡ�������δͨ���ļ�¼
        /// <summary>
        /// ��ȡ�������δͨ���ļ�¼
        /// </summary>
        /// <param name="DoctorID"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetrecipeconfirmfall(string DoctorID, out DataTable dtRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                            (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetrecipeconfirmfall(DoctorID, out dtRecord);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ��ȡ�����������浥��Ϣ
        /// <summary>
        /// ��ȡ�����������浥��Ϣ
        /// </summary>
        /// <param name="SQLSelect"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetopsreports(string SQLSelect, out DataTable dtRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetopsreports(SQLSelect, out dtRecord);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ��ȡ����ʽ
        /// <summary>
        /// ��ȡ����ʽ
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetanaesthesiamode(out DataTable dtRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetanaesthesiamode(out dtRecord);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ģ������Ա��������Ա��ID��Ա����������
        /// <summary>
        /// ģ������Ա��������Ա��ID��Ա����������
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strNameArr"></param>
        /// <returns></returns>
        public long m_lngGetEmployeeNameByLike(string p_strLike, out DataTable p_strNameArr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetEmployeeNameByLike(p_strLike, out p_strNameArr);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region �����շ�ID�ж��Ƿ�������������
        /// <summary>
        /// �����շ�ID�ж��Ƿ�������������
        /// </summary>
        /// <param name="chrgitemcode"></param>
        /// <returns></returns>
        public bool m_blnChkopsitem(string chrgitemcode)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                         (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            bool ret = proxy.Service.m_blnChkopsitem(chrgitemcode);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ����������ȡ�������뵥��Ϣ
        /// <summary>
        /// ����������ȡ�������뵥��Ϣ
        /// </summary>
        /// <returns></returns>
        public long m_lngGetApplyOPInfoByOrCondition(string p_strOr, out DataTable dtRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                         (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetApplyOPInfoByOrCondition(p_strOr, out dtRecord);
            // objSvc.Dispose();
            return ret;
        }
        #endregion

        #region �����������뵥��ϢԤԼʱ��
        /// <summary>
        /// �����������뵥��ϢԤԼʱ��
        /// </summary>
        /// <returns></returns>
        public long m_lngGetApplyOPInfoUpdateDate(string p_strAPPLYID_VCHR, string p_strOPSBOOKINGDATE_DAT)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                         (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetApplyOPInfoUpdateDate(p_strAPPLYID_VCHR, p_strOPSBOOKINGDATE_DAT);
            return ret;
        }
        #endregion

        #region ���ݲ���ԱID��ȡ������ҩȨ��
        /// <summary>
        /// ���ݲ���ԱID��ȡ������ҩȨ��
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="neurpur"></param>
        /// <param name="drugpur"></param>
        public void m_mthGetmedpurview(string empid, out string neurpur, out string drugpur, out string recpur)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                        (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            proxy.Service.m_mthGetmedpurview(empid, out neurpur, out drugpur, out recpur);
        }
        #endregion

        #region �����շ���Ŀ�ж��Ƿ�ΪƬ��
        /// <summary>
        /// �����շ���Ŀ�ж��Ƿ�ΪƬ��
        /// </summary>
        /// <param name="chrgitemid"></param>
        /// <returns></returns>
        public bool m_blnCheckmedicament(string chrgitemid)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            bool b = proxy.Service.m_blnCheckmedicament(chrgitemid);
            // objSvc.Dispose();

            return b;
        }
        #endregion

        #region ���ó�ʼ��ģ������
        /// <summary>
        /// ���ó�ʼ��ģ������
        /// </summary>
        /// <returns></returns>
        public long m_lngSetModeByItem(string p_strFormName, string p_strFormDesc, string p_strControlName, string p_strControlDesc)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long lngRes = proxy.Service.m_lngSetModeByItem(p_strFormName, p_strFormDesc, p_strControlName, p_strControlDesc);

            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ���ԡ���������һ����������
        /// <summary>
        /// ��ȡҩƷ���ԡ���������һ����������
        /// </summary>
        /// <param name="chrgitemid"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetmedproperty(string chrgitemid, out DataTable dtRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //               (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetmedproperty(chrgitemid, out dtRecord);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ��ȡ��ϵͳ����
        /// <summary>
        /// ��ȡ��ϵͳ����
        /// </summary>
        /// <param name="parmcode">��������</param>
        /// <returns>����ֵ</returns>
        public string m_strGetSysparm(string parmcode)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                           (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            string ret = proxy.Service.m_strGetSysparm(parmcode);
            // objSvc.Dispose();

            return ret;
        }
        /// <summary>
        /// ������ȡϵͳ����
        /// </summary>
        /// <param name="p_strParamKeyArr"></param>
        /// <param name="p_hasParamValue"></param>
        /// <returns></returns>
        public long m_lngGetSysparm(string[] p_strParamKeyArr, out Dictionary<string, string> p_hasParamValue)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                           (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long lngRes = proxy.Service.m_lngGetSysparm(p_strParamKeyArr, out p_hasParamValue);
            // objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ������ȡϵͳ��������
        /// </summary>
        /// <param name="p_strParamKeyArr"></param>
        /// <param name="p_hasParamValue"></param>
        /// <returns></returns>
        public long m_lngGetSysSetting(string[] p_strParamKeyArr, out Dictionary<string, string> p_hasParamValue)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                           (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long lngRes = proxy.Service.m_lngGetSysSetting(p_strParamKeyArr, out p_hasParamValue);
            // objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region (ҽ��)����ICD10��ȡ����Ӧ�����ֲ�����
        /// <summary>
        /// (ҽ��)����ICD10��ȡ����Ӧ�����ֲ�����
        /// </summary>
        /// <param name="icd10_id"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetYBSpeciaTypeDiseaseByICD10(string icd10_id, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                       (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetYBSpeciaTypeDiseaseByICD10(icd10_id, out dt);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region (ҽ��)�������ֲ������ȡ����Ӧ���շ���Ŀ
        /// <summary>
        /// (ҽ��)�������ֲ������ȡ����Ӧ���շ���Ŀ
        /// </summary>
        /// <param name="deacode"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetYBSpecChargeItemByDeacode(string deacode, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                       (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetYBSpecChargeItemByDeacode(deacode, out dt);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region (ҽ��)��ȡ���ֲ���Ϣ
        /// <summary>
        /// (ҽ��)��ȡ���ֲ���Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetYBSpeciaTypeDisease(out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                       (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetYBSpeciaTypeDisease(out dt);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region (ҽ��)�������ֲ������ȡ����Ӧ��ICD10
        /// <summary>
        /// (ҽ��)�������ֲ������ȡ����Ӧ��ICD10
        /// </summary>
        /// <param name="deacode"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetICD10ByDeacode(string deacode, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                       (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long ret = proxy.Service.m_lngGetICD10ByDeacode(deacode, out dt);
            // objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ��ȡ�����շ���Ŀ�ٴ���˼
        /// <summary>
        /// ��ȡ�����շ���Ŀ�ٴ���˼
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>        
        public long m_lngGetLisItemClinicMeaning(string ItemID, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                       (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngGetLisItemClinicMeaning(ItemID, out dt);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region ���ô���Ȩ
        /// <summary>
        /// ��ȡ���￪��ҽ���б�
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetDoctorList(out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                   (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngGetDoctorList(out dt);
            // objSvc.Dispose();

            return l;
        }

        /// <summary>
        /// ����ҽ��Ȩ�޶����
        /// </summary>
        /// <param name="objArr"></param>
        /// <param name="Flag">1 �¼� 2 ɾ��</param>
        /// <returns></returns>        
        public long m_lngSaveDoctorRecipePurview(List<clsOutRecipePurview_VO> objArr, int Flag)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                               (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngSaveDoctorRecipePurview(objArr, Flag);
            // objSvc.Dispose();

            return l;
        }

        /// <summary>
        /// ��ȡҽ��Ȩ�޶����
        /// </summary>
        /// <param name="objArr"></param>
        /// <returns></returns>        
        public long m_lngGetDoctorRecipePurview(string DoctID, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                               (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngGetDoctorRecipePurview(DoctID, out dt);
            // objSvc.Dispose();

            return l;
        }

        #endregion

        #region ���Ҵ���-������Ŀ
        /// <summary>
        /// ���Ҵ���-������Ŀ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="OrderCat"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngFindRecipeOrderByID(string ID, System.Collections.Generic.List<string> OrderCatArr, out DataTable dt, bool isChildPrice)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngFindRecipeOrderByID(ID, OrderCatArr, out dt, isChildPrice);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region ����������Ŀ��ȡ�շ���Ŀ
        /// <summary>
        /// ����������Ŀ��ȡ�շ���Ŀ
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="PayTypeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetChargeItemByOrderID(string OrderID, string PayTypeID, out DataTable dt, bool isChildPrice)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngGetChargeItemByOrderID(OrderID, PayTypeID, out dt, isChildPrice);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region �ж�������Ŀ(���)�Ƿ��������
        /// <summary>
        /// �ж�������Ŀ(���)�Ƿ��������
        /// </summary>
        /// <param name="OrderID">������ĿID</param>
        /// <param name="InvoCatArr">��Ʊ��������</param>
        /// <param name="SysType">ϵͳ 1 ���� 2 סԺ</param>
        /// <param name="ItemNums">��Ŀ����</param>
        /// <returns></returns>        
        public bool m_blnCheckOrderDiscount(string OrderID, System.Collections.Generic.List<string> InvoCatArr, int SysType, int ItemNums)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            bool b = proxy.Service.m_blnCheckOrderDiscount(OrderID, InvoCatArr, SysType, ItemNums);
            // objSvc.Dispose();

            return b;
        }
        #endregion

        #region ����Э������
        /// <summary>
        /// ����Э������
        /// </summary>
        /// <param name="CreateEmpID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngFindAccordRecipe(string CreateEmpID, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngFindAccordRecipe(CreateEmpID, out dt);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region ��ѯЭ������
        /// <summary>
        /// ��ѯЭ������
        /// </summary>
        /// <param name="RecipeID"></param>
        /// <param name="ClassID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetAccordRecipe(string RecipeID, int ClassID, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngGetAccordRecipe(RecipeID, ClassID, out dt);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region ɾ������
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="RecipeID"></param> 
        public long m_lngDelAccordRecipe(string RecipeID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngDelAccordRecipe(RecipeID);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region ����Э������
        /// <summary>
        /// ����Э������
        /// </summary>
        /// <param name="AccordRecipe_VO"></param>
        /// <param name="objRecEntryArr"></param>
        /// <param name="RecipeID"></param>        
        public long m_lngSaveAccordRecipe(clsAccordRecipe_VO AccordRecipe_VO, List<clsAccordRecipePlus_VO> objRecEntryArr, out string RecipeID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngSaveAccordRecipe(AccordRecipe_VO, objRecEntryArr, out RecipeID);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region �ۺϲ�ѯ�շ���Ŀ
        /// <summary>
        /// �ۺϲ�ѯ�շ���Ŀ
        /// </summary>
        /// <param name="FindStr"></param>
        /// <param name="Type">1 ��ҩ 2 ��ҩ 3 ���� 4 ��� 5 ���� 6 ����</param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngFindChargeItem(string FindStr, int Type, out DataTable dt, bool isChildPrice)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngFindChargeItem(FindStr, Type, out dt, isChildPrice);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region ϵͳ�ڲ����뵥����
        /// <summary>
        /// ϵͳ�ڲ����뵥����
        /// </summary>
        /// <param name="objArr"></param>
        /// <returns></returns>
        public long m_lngGetAPPLY_RLT(out System.Collections.Generic.Dictionary<string, string> objArr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                 (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngGetAPPLY_RLT(out objArr);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region ������ȡ����
        /// <summary>
        /// ������ȡ����
        /// </summary>
        /// <param name="p_strPayTypeID"></param>
        /// <param name="p_strItemIDArr"></param>
        /// <param name="p_hasItemScale"></param>
        /// <returns></returns>
        public long m_lngGetItemScaleByArr(string p_strPayTypeID, string[] p_strItemIDArr, ref Dictionary<string, string> p_hasItemScale)
        {

            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngGetItemScaleByArr(p_strPayTypeID, p_strItemIDArr, ref p_hasItemScale);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region ��ȡ����������Ϣ
        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>
        /// <param name="p_strRecipeID"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngGetRecipeMainInfo(string p_strRecipeID, out DataTable p_dtResult)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngGetRecipeMainInfo(p_strRecipeID, out p_dtResult);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region
        public long m_lngGetRecipeCreateType(ref int p_createtype, string p_strRecipeNO)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                                             (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long l = proxy.Service.m_lngGetRecipeCreateType(ref p_createtype, p_strRecipeNO);
            // objSvc.Dispose();

            return l;
        }
        #endregion

        #region ��ȡָ�����ŵ�ҽ������Ƿ����Ϣ
        /// <summary>
        /// ��ȡָ�����ŵ�ҽ������Ƿ����Ϣ
        /// </summary>
        /// <param name="p_strCard"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngGetVipDebtByCard(string p_strCard, out DataTable p_dtResult)
        {
            //clsDoctorWorkStationSvc objSvc = (clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDoctorWorkStationSvc));
            long l = proxy.Service.m_lngGetVipDebtByCard(p_strCard, out p_dtResult);
            // objSvc.Dispose();
            return l;
        }
        #endregion

        #region �ж���ĳ��ҩƷ��ҩƷ�����Ƿ���9003��
        /// <summary>
        /// �ж���ĳ��ҩƷ��ҩƷ�����Ƿ���9003��
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool blMedicine9003(string p_strMedID)
        {
            bool blMedicine = false;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));
            blMedicine = (new weCare.Proxy.ProxyOP01()).Service.blMedicine9003(p_strMedID);
            // objSvc.Dispose();
            return blMedicine;
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string GetPatientWeight(string pid)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                     (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            return proxy.Service.GetPatientWeight(pid);
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="weight"></param>
        public void UpdatePatientWeight(string pid, string weight)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                     (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            proxy.Service.UpdatePatientWeight(pid, weight);
            //svc = null;
        }
        #endregion

        #region ��ȡ���ƶ�ҩƷ
        /// <summary>
        /// ��ȡ���ƶ�ҩƷ
        /// </summary>
        /// <returns></returns>
        public DataTable GetProduceDrugs()
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc = (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.GetProduceDrugs();
            //}
        }
        #endregion

        #region ΢�ż���Ƿ��
        /// <summary>
        /// ΢�ż���Ƿ��
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public bool IsWechatBanding(string cardNo)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc svc = (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)))
            //{
            return proxy.Service.IsWechatBanding(cardNo);
            //}
        }
        #endregion

        #region ҩƷ����ж�
        /// <summary>
        /// ҩƷ����ж�
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public bool IsHaveDrugStock(string itemId, string storeId)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc svc = (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)))
            //{
            return proxy.Service.IsHaveDrugStock(itemId, storeId);
            //}
        }
        #endregion

        #region ��ݸ��ԤԼƽ̨.�ж��Ƿ���ԤԼ��¼
        /// <summary>
        /// ��ݸ��ԤԼƽ̨.�ж��Ƿ���ԤԼ��¼
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="regDate"></param>
        /// <param name="psOrderNum"></param>
        /// <returns></returns>
        public bool IsWechatPlatformBooking(string cardNo, string regDate, string recipeId, out decimal bookingNo, out string psOrderNum, out string doctCode)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc = (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.IsWechatPlatformBooking(cardNo, regDate, recipeId, out bookingNo, out psOrderNum, out doctCode);
            //}
        }
        #endregion

        #region ���ұ���
        /// <summary>
        /// ���ұ���
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public string GetDeptCode(string deptId)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc svc = (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)))
            //{
            return proxy.Service.GetDeptCode(deptId);
            //}
        }
        #endregion

        #region ���պ�Դ����
        /// <summary>
        /// ���պ�Դ����
        /// </summary>
        /// <param name="doctId"></param>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public bool UpdateTodayPlatformRecipe(string doctId, string recipeId)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc = (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.UpdateTodayPlatformRecipe(doctId, recipeId);
            //}
        }
        #endregion

        #region ��ȡ�ֳ���ʹ�ô�����Ϣ
        /// <summary>
        /// ��ȡ�ֳ���ʹ�ô�����Ϣ
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public DataTable GetTodayRegInfo(string recipeId)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc svc = (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)))
            //{
            return proxy.Service.GetTodayRegInfo(recipeId);
            //}
        }
        #endregion

        #region ���պ�Դ���·���ʱ��
        /// <summary>
        /// ���պ�Դ���·���ʱ��
        /// </summary>
        /// <param name="serNo"></param>
        /// <returns></returns>
        public bool UpdateTodayPlatformSendDate(decimal serNo)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc = (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.UpdateTodayPlatformSendDate(serNo);
            //}
        }
        #endregion

        #region �жϴ������Ƿ�ֻ������Ƭ,�����������
        /// <summary>
        /// �жϴ������Ƿ�ֻ������Ƭ,�����������
        /// </summary>
        /// <param name="lstChargeItemId"></param>
        /// <returns></returns>
        public bool CheckRecipeSlices(List<string> lstChargeItemId)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc svc = (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)))
            //{
            return proxy.Service.CheckRecipeSlices(lstChargeItemId);
            //}
        }
        #endregion

        #region ���ߴ���ҩ�ռ���Ϣ
        /// <summary>
        /// ���ߴ���ҩ�ռ���Ϣ
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public DataTable GetPatientContactInfo(string patientId)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc svc = (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStation_SupportedSvc)))
            //{
            return proxy.Service.GetPatientContactInfo(patientId);
            //}
        }
        #endregion

        #region ��ȡ������Һ���˵��
        /// <summary>
        /// ��ȡ������Һ���˵��
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public string GetRecipeIVDRIInstruction(string recipeId)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                      (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.GetRecipeIVDRIInstruction(recipeId);
            //}
        }
        #endregion

        #region ���¾�����Һ���˵��
        /// <summary>
        /// ���¾�����Һ���˵��
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="ivdriInstr"></param>
        public bool UpdateRecipeIVDRIInstruction(string recipeId, string ivdriInstr)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                      (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.UpdateRecipeIVDRIInstruction(recipeId, ivdriInstr);
            //}
        }
        #endregion

        #region ��Ժ֪ͨ��

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <returns></returns>
        public DataTable GetPatientPayType()
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                      (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.GetPatientPayType();
            //}
        }

        /// <summary>
        /// ��ȡICD10
        /// </summary>
        /// <returns></returns>
        public DataTable GetIcd10()
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                     (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.GetIcd10();
            //}
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeptDesc()
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                     (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.GetDeptDesc();
            //}
        }

        /// <summary>
        /// ��ȡסԺRegisterId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public string GetIpRegisterId(string patientId)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                                 (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.GetIpRegisterId(patientId);
            //}
        }

        /// <summary>
        /// ��Ժ�Ǽ�
        /// </summary>
        /// <param name="patVo"></param>
        /// <param name="bihVo"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int InRegister(clsPatient_VO patVo, clsT_Opr_Bih_Register_VO bihVo, out string regId, out string ipNo, out string error)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                                 (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.InRegister(patVo, bihVo, out regId, out ipNo, out error);
            //}
        }

        /// <summary>
        /// ȡ����Ժ
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="cancelOperId"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int CancelRegister(string registerId, string cancelOperId, out string error)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                                 (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.CancelRegister(registerId, cancelOperId, out error);
            //}
        }

        /// <summary>
        /// ��ȡסԺ�Ǽ���Ϣ
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="patVo"></param>
        /// <param name="bihVo"></param>
        public void GetRegister(string registerId, out clsPatient_VO patVo, out clsT_Opr_Bih_Register_VO bihVo)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                                 (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            proxy.Service.GetRegister(registerId, out patVo, out bihVo);
            //}
        }

        /// <summary>
        /// ��ȡ������Ժ���
        /// </summary>
        /// <param name="ipNo"></param>
        /// <returns></returns>
        public string GetTodayInNumber(string ipNo)
        {
            return proxy.Service.GetTodayInNumber(ipNo);
        }

        #endregion

        #region ���ݴ���ID��ȡ���߳�������
        /// <summary>
        /// ���ݴ���ID��ȡ���߳�������
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public DateTime GetBirthdayByRecipeId(string recipeId)
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc svc =
            //                                                (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)))
            //{
            return proxy.Service.GetBirthdayByRecipeId(recipeId);
            //}
        }
        #endregion

    }
}
