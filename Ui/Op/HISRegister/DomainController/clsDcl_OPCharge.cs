using System;
using weCare.Core.Entity;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_OPCharge ��ժҪ˵����
    /// </summary>
    public class clsDcl_OPCharge : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_OPCharge()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_mthSetPara();
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP01 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP01();
            }
        }
        #endregion 

        private string m_strNeedCheck = string.Empty;
        private void m_mthSetPara()
        {
            try
            {
                m_strNeedCheck = m_strGetSysparm("1200");
                string[] TmpArr = m_strNeedCheck.Split(';');
                for (int intI = 0; intI < TmpArr.Length; intI++)
                {
                    if (TmpArr[intI] != null)
                    {
                        if (intI == 0)
                        {
                            m_strNeedCheck = "'" + TmpArr[intI] + "'";
                        }
                        else
                        {
                            m_strNeedCheck += "," + "'" + TmpArr[intI] + "'";
                        }
                    }
                }
            }
            catch (Exception objex)
            {
                m_strNeedCheck = "'1','2'";
                com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                objLog.LogError("��ȡ����1200ʧ�ܣ�" + objex.Message);
            }
        }
        /// <summary>
        /// ��ȡ��ϵͳ����
        /// </summary>
        /// <param name="parmcode">��������</param>
        /// <returns>����ֵ</returns>
        private string m_strGetSysparm(string parmcode)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                           (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            string ret = (new weCare.Proxy.ProxyOP()).Service.m_strGetSysparm(parmcode);
            //objSvc.Dispose();

            return ret;
        }
        /// <summary>
        /// ����ҩƷ
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindMedicineByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_mthFindMedicineByID(strType, ID.ToUpper(), strPatientTypeID, out dt, strEmployID, this.m_strNeedCheck, isChildPrice);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ����ҩƷ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindMedicineByID(string ID, out DataTable dt, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_mthFindMedicineByID(ID, out dt, isChildPrice);
            //objSvc.Dispose();
            return lngRes;
        }

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
            blMedicine = proxy.Service.blMedicine9003(p_strMedID);
            //objSvc.Dispose();
            return blMedicine;
        }

        #region ��ȡ������ˮ��
        /// <summary>
        /// ��ȡ������ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strSerNo"></param>
        /// <returns></returns>
        public long m_mthGetSerNO(out string m_strSerNo)
        {
            m_strSerNo = string.Empty;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_mthGetSerNO(out m_strSerNo);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ����ҩƷ���ͱ�
        /// <summary>
        /// ����ҩƷ���ͱ�
        /// </summary>
        /// <param name="objVOMainArr"></param>
        /// <param name="objWMedicineSend"></param>
        /// <param name="objCMedicineSend"></param>
        /// <returns></returns>
        public long m_mthSaveMedicineSend(ref List<clsOutPatientRecipe_VO> objVOMainArr, ref List<clsMedrecipesend_VO> objWMedicineSend, ref List<clsMedrecipesend_VO> objCMedicineSend)
        {

            long lngRes = 0;
            try
            {
                //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
                //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));
                lngRes = proxy.Service.m_mthSaveMedicineSend(ref objVOMainArr, ref objWMedicineSend, ref objCMedicineSend);
                //objSvc.Dispose();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return lngRes;

        }
        #endregion
        #region ��������
        public long m_mthSaveAllData(List<clsOutPatientRecipe_VO> objVOMainArr, out string strRecipeNo,
            clsRecipeDetail_VO[] objRD_VO, decimal times, clsInvoice_VO[] objInvoice_VOArr, List<clsInvoiceTypeDetail_VO>[] objArr1, List<clsInvoiceTypeDetail_VO>[] objArr2, List<clsMedrecipesend_VO> objMedicineSend, string strOpChargeDeptId, bool blnFlag)
        {
            long lngRes = 0;
            strRecipeNo = "";
            try
            {
                //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
                //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));
                lngRes = proxy.Service.m_mthSaveAllData(objVOMainArr, out strRecipeNo, objRD_VO, times, objInvoice_VOArr, objArr1, objArr2, objMedicineSend, strOpChargeDeptId, blnFlag);
                //objSvc.Dispose();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return lngRes;
        }
        #endregion
        public long m_mthAddRecipeMain(clsOutPatientRecipe_VO clsVO, out string p_strID)
        {
            p_strID = "";
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));
            long lngRes = proxy.Service.m_mthAddRecipeMain(clsVO, out p_strID);
            //objSvc.Dispose();
            return lngRes;
        }
        public void m_mthSaveRecipeDetial(string strRecipeNo, clsRecipeDetail_VO[] objRD_VO, decimal times)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));
            proxy.Service.m_mthSaveRecipeDetial(strRecipeNo, objRD_VO, times);
            //objSvc.Dispose();
        }
        public void m_mthSaveRecipeChargeItemDetial(string strRecipeNo, clsRecipeDetail_VO[] objRD_VO)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));
            proxy.Service.m_mthSaveRecipeChargeItemDetial(strRecipeNo, objRD_VO);
            //objSvc.Dispose();
        }
        public void m_mthSaveRecipeSend(clsMedrecipesend_VO objMRS_VO)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));
            proxy.Service.m_mthSaveRecipeSend(objMRS_VO);
            //objSvc.Dispose();

        }
        public long m_mthSaveInvoicInfo(clsInvoice_VO obj)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));
            long lngRes = proxy.Service.m_mthSaveInvoicInfo(obj);
            //objSvc.Dispose();
            return lngRes;
        }
        public void m_mthSaveInvoiceDetail(List<clsInvoiceTypeDetail_VO> objArr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));
            proxy.Service.m_mthSaveInvoiceDetail(objArr);
            //objSvc.Dispose();
        }
        public void m_mthSaveInvoiceDetail2(List<clsInvoiceTypeDetail_VO> objArr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));
            proxy.Service.m_mthSaveInvoiceDetail2(objArr);
            //objSvc.Dispose();
        }
        //		public long m_mthFindRecipeNoByPatientID(string ID,out clsRecipeInfo_VO[] objRI_VO)
        //		{
        //			objRI_VO=null;
        //			com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc = 
        //				(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
        //		long lngRes=	objSvc.m_mthFindRecipeNoByPatientID(ID, out objRI_VO,"");
        //			//objSvc.Dispose();
        //			return lngRes;
        //			
        //		}
        public long m_mthFindMaxRecipeNoByPatientID(string ID, out string strRecipe, out string strSeqid, out string strstatus, out int RecipeCount, out DataTable dt, out string strISgreen, bool isChildPrice)
        {
            strRecipe = "";
            RecipeCount = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_mthFindMaxRecipeNoByPatientID(ID, out strRecipe, out strSeqid, out strstatus, out RecipeCount, out dt, out strISgreen, isChildPrice);
            //objSvc.Dispose();
            return lngRes;

        }

        public long m_mthFindTreatRecipeNoByPatientID(string ID, out string strRecipe, out string strSeqid, out string strstatus, out int RecipeCount, out DataTable dt, bool isChildPrice)
        {
            strRecipe = "";
            strSeqid = "";
            RecipeCount = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_mthFindTreatRecipeNoByPatientID(ID, out strRecipe, out strSeqid, out strstatus, out RecipeCount, out dt, isChildPrice);
            //objSvc.Dispose();
            return lngRes;

        }

        public long m_mthFindRecipeByID(string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_mthFindRecipeByID(ID, out dt, flag, isChildPrice);
            //objSvc.Dispose();
            return lngRes;
        }
        #region ��鷢Ʊ���Ƿ�����
        public bool m_mthCheckInvoice(string strInvoiceNo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            bool lngRes = proxy.Service.m_mthCheckInvoice(strInvoiceNo);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ���Ҷ�Ӧ����Ϣ
        public long m_mthRelationInfo(out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = (new weCare.Proxy.ProxyOP()).Service.m_mthRelationInfo(out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ���Ҵ���ҽ��
        public long m_mthFindRecipeDoctor(string strPatientID, string strRecipeNO, out clsRecipeInfo_VO[] objRI_VO)
        {
            objRI_VO = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            return proxy.Service.m_mthFindRecipeNoByPatientID(strPatientID, out objRI_VO, strRecipeNO, 1);
        }
        #endregion
        public long m_mthGetDefaultItem(string strPatientTypeID, string strRegister, string strRecipeflag, string strExpert, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_mthGetDefaultItem(strPatientTypeID, strRegister, strRecipeflag, strExpert, out dt);
            //objSvc.Dispose();
            return lngRes;
        }

        #region ����Ĭ����Ŀ
        /// <summary>
        /// ����Ĭ����Ŀ
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strPatientTypeID"></param>
        /// <param name="strRecipeflag"></param>
        /// <param name="strDuty"></param>
        /// <param name="strRecipeID"></param>
        /// <returns></returns>     
        public long m_mthGetDefaultItem(out DataTable dt, string strPatientTypeID, string strRecipeflag, string strDuty, string strRecipeID, string strRegID, string strDeptID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_mthGetDefaultItem(out dt, strPatientTypeID, strRecipeflag, strDuty, strRecipeID, strRegID, strDeptID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        public void m_mthGetChooseHospitalInfo(out clsChargeHospitalInfoVO[] objCHInfoVOArr)
        {
            objCHInfoVOArr = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            proxy.Service.m_mthGetChooseHospitalInfo(out objCHInfoVOArr);
            //objSvc.Dispose();
        }
        public int m_mthIsCanDo(string p_flag)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            return proxy.Service.m_mthIsCanDo(p_flag);

        }
        #region �����÷�
        public long m_mthFindUsage(string ID, out DataTable dt)
        {

            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = (new weCare.Proxy.ProxyOP()).Service.m_mthFindUsage2(ID, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        public long m_mthFindFreq(string ID, out DataTable dt)
        {

            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            long lngRes = (new weCare.Proxy.ProxyOP()).Service.m_mthFindFrequency2(ID, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ȡ�ÿ��ұ��
        /// <summary>
        /// ȡ�ÿ��ұ��
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <returns></returns>
        public string m_strGetDeptNO(string p_strDeptID)
        {
            System.Data.DataTable dtRecord = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            proxy.Service.m_mthGetDeptNO(p_strDeptID, out dtRecord);
            if (dtRecord != null)
            {
                if (dtRecord.Rows.Count == 1)
                {
                    return dtRecord.Rows[0][0].ToString().Trim();
                }
            }
            return "";
        }
        #endregion

        #region �ж��Ƿ����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strChrgItem"></param>
        /// <returns></returns>
        public bool m_blnCheckMaterial(string strChrgItem)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            bool lngRes = proxy.Service.m_blnCheckMaterial(strChrgItem);
            //objSvc.Dispose();
            return lngRes;
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
            string strMedStoretype = proxy.Service.m_strGetOutSendMedStoretype(strChrgItem);
            ////objSvc.Dispose();
            return strMedStoretype;
        }
        #endregion

        #region �ж��շ���Ŀ�Ƿ�������
        /// <summary>
        /// �ж��շ���Ŀ�Ƿ�������
        /// </summary>
        /// <param name="p_strChargeItem"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>		
        public bool m_blnCheckSubChargeItem(string p_strPatientTypeID, string p_strChargeItem, out DataTable dtRecord, bool isChildPrice)
        {
            long lngRes = 0;
            bool blnRet = false;
            dtRecord = new DataTable();

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            try
            {
                lngRes = proxy.Service.m_lngGetSubChargeItem(p_strPatientTypeID, p_strChargeItem, out dtRecord, isChildPrice);
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

        #region �ж�ҽ���Ƿ�Ϊר��
        /// <summary>
        /// �ж�ҽ���Ƿ�Ϊר��
        /// </summary>
        /// <param name="strEmpID"></param>
        /// <returns></returns>
        public bool m_blnCheckExpert(string strEmpID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            bool lngRes = proxy.Service.m_blnCheckExpert(strEmpID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �ж��Ƿ������Һ�
        /// <summary>
        /// �ж��Ƿ������Һ�
        /// </summary>
        /// <param name="strEmpID"></param>
        /// <returns></returns>
        public bool m_blnCheckNormalReg(string strRegID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            bool lngRes = proxy.Service.m_blnCheckNormalReg(strRegID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����Ĭ����Ŀ
        /// <summary>
        /// ����Ĭ����Ŀ
        /// </summary>
        /// <param name="strItemID"></param>
        /// <param name="strPatType"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindChrgItemByID(string strItemID, string strPatType, out DataTable dt, bool isChildPrice)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_mthGetDefaultItem(strItemID, strPatType, out dt, isChildPrice);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ʾ֧���������б�
        /// <summary>
        /// ��ʾ֧���������б�
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetPaycardtype(out DataTable dtRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_lngGetPaycardtype(out dtRecord);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���߽��㿨�����б�
        /// <summary>
        /// ��ȡ���߽��㿨�����б�
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public long m_lngGetPaycardno(out DataTable dtRecord, string pid)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_lngGetPaycardno(out dtRecord, pid);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҩ��ID��ȡ���Ŵ��ں�
        /// <summary>
        /// ����ҩ��ID��ȡ���Ŵ��ں�
        /// </summary>
        /// <param name="storageID"></param>
        /// <param name="windowsID"></param>
        /// <returns></returns>
        public long lngGetWindowIDByStorage(string storageID, out string windowsID, out int sortno, bool CheckScope)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol));

            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.lngGetWindowIDByStorage(storageID, out windowsID, out sortno, CheckScope);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡҩ���Ĵ���
        public long m_mthGetMedWindows(string p_strMedstoreID, out DataTable dtbResult)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol));

            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthGetMedWindows(p_strMedstoreID, out dtbResult);
            //objSvc.Dispose();
            return lngRes;
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
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ɻ�����ݶ�Ӧ�ű�
        /// <summary>
        /// ���ɻ�����ݶ�Ӧ�ű�
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="paytypeid"></param>
        /// <param name="idno"></param>
        /// <returns></returns>
        public long m_lngGenpatientidentityno(string pid, string paytypeid, string idno)
        {
            if (paytypeid.Trim() == "")
            {
                return 0;
            }

            if (idno.Trim() == "")
            {
                idno = " ";
            }

            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //       (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            long lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGenpatientidentityno(pid, paytypeid, idno);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݻ���ID�������ID��ȡ�������Ӧ��
        /// <summary>
        /// ���ݻ���ID�������ID��ȡ�������Ӧ��
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="paytypeid"></param>
        /// <returns></returns>
        public string m_strGetpatientidentityno(string pid, string paytypeid)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                   (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            string idno = (new weCare.Proxy.ProxyOP()).Service.m_strGetpatientidentityno(pid, paytypeid);
            //objSvc.Dispose();
            return idno;
        }
        #endregion

        #region ���ݻ���IDͳ�Ƶ���������δ�շѴ�����Ϣ
        /// <summary>
        /// ���ݻ���IDͳ�Ƶ���������δ�շѴ�����Ϣ
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="recsum"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetAllrecinfoBypid(string pid, out int recsum, out DataTable dtRecord, bool isChildPrice)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            long ret = proxy.Service.m_lngGetAllrecinfoBypid(pid, out recsum, out dtRecord, isChildPrice);
            //objSvc.Dispose();
            return ret;
        }
        #endregion

        #region ���ݻ���IDͳ�Ƹò������������ƺ�����δ�շѴ�����Ϣ
        /// <summary>
        /// ���ݻ���IDͳ�Ƹò������������ƺ�����δ�շѴ�����Ϣ
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="recsum"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetTreatRecinfoBypid(string pid, out int recsum, out DataTable dtRecord, bool isChildPrice)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            long ret = proxy.Service.m_lngGetTreatRecinfoBypid(pid, out recsum, out dtRecord, isChildPrice);
            //objSvc.Dispose();
            return ret;
        }
        #endregion

        #region ����PID��ȡ���ߵ��췢ҩ��Ϣ
        /// <summary>
        /// ����PID��ȡ���ߵ��췢ҩ��Ϣ
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetsendmedinfoBypid(string pid, string medid, out DataTable dtRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            long ret = proxy.Service.m_lngGetsendmedinfoBypid(pid, medid, out dtRecord);
            //objSvc.Dispose();
            return ret;
        }
        #endregion

        #region �жϴ���Ĵ����Ƿ��ڹ���״̬
        /// <summary>
        /// �жϴ���Ĵ����Ƿ��ڹ���״̬
        /// </summary>
        /// <param name="winid"></param>
        /// <returns></returns>
        public bool m_blnGetmedwinstatus(string winid)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWindowsCortrol));

            bool b = false;
            long ret = (new weCare.Proxy.ProxyOP02()).Service.m_lngWindowsWork(winid, out b);

            return b;
        }
        #endregion

        #region ���ݴ������ж�һ�Ŵ����Ƿ���ҽ������վ��������Ϊ���շѴ���
        /// <summary>
        /// ���ݴ������ж�һ�Ŵ����Ƿ���ҽ������վ��������Ϊ���շѴ���
        /// </summary>
        /// <param name="recno"></param>
        /// <returns></returns>
        public bool m_blnCheckRecipeProperty(string recno)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            bool lngRes = proxy.Service.m_blnCheckRecipeProperty(recno);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݴ������ж�һ�Ŵ����Ƿ����շ�
        /// <summary>
        /// ���ݴ������ж�һ�Ŵ����Ƿ����շ�
        /// </summary>
        /// <param name="recno"></param>
        /// <returns></returns>
        public bool CheckRecipeIsCharge(string recno)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            bool lngRes = proxy.Service.CheckRecipeIsCharge(recno);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݽ�����ҡ�ҩ����ȡר�ô�����Ϣ
        /// <summary>
        /// ���ݽ�����ҡ�ҩ����ȡר�ô�����Ϣ
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="medin"></param>
        /// <param name="winid"></param>
        /// <returns></returns>
        public long m_lngGetespecialwin(string deptid, string medid, out string winid, out int waitno)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            long ret = proxy.Service.m_lngGetespecialwin(deptid, medid, out winid, out waitno);
            //objSvc.Dispose();
            return ret;
        }
        #endregion

        #region �������ƿ��Ż�����ﲡ�˻����Һ���Ϣ
        /// <summary>
        /// �������ƿ��Ż�����ﲡ�˻����Һ���Ϣ
        /// </summary>
        /// <param name="CardID">���ƿ���</param>
        /// <param name="dt">�����Ϣ</param>
        /// <returns></returns>        
        public long m_lngGetPatientInfoByCard(string CardID, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsHisBase objSvc =
            //                                            (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));

            long l = (new weCare.Proxy.ProxyHisBase()).Service.s_lngGetPatientInfoByCard(CardID, out dt, "PATIENTCARDID_CHR");

            //objSvc.Dispose();

            return l;
        }
        #endregion

        #region �������洦��
        /// <summary>
        /// �������洦��
        /// </summary>
        /// <param name="objVOMainArr"></param>
        /// <param name="objRD_VO"></param>
        /// <param name="times"></param>        
        /// <returns></returns>        
        public long m_lngSaveRecipe(List<clsOutPatientRecipe_VO> objVOMainArr, clsRecipeDetail_VO[] objRD_VO, decimal times)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));

            long ret = proxy.Service.m_lngSaveRecipe(objVOMainArr, objRD_VO, times);
            //objSvc.Dispose();
            return ret;
        }
        #endregion


        #region (ҽ��)���ݴ����Ż�ȡ���ʵ���
        /// <summary>
        /// (ҽ��)���ݴ����Ż�ȡ���ʵ���
        /// </summary>
        /// <param name="Recno"></param>
        /// <param name="Billno"></param>
        /// <returns></returns>
        public long m_lngGetybbillno(string Recno, ref string Billno)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                            (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            long lngRes = proxy.Service.m_lngGetybbillno(Recno, ref Billno);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region (ҽ��)���������շ����ݵ�ҽ��ǰ�û�
        /// <summary>
        /// (ҽ��)���������շ����ݵ�ҽ��ǰ�û�
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="objRecipeArr"></param>
        /// <param name="objYBArr"></param>
        /// <param name="BillNO"></param>
        /// <returns></returns>
        public long m_lngSendybdata(string DSN, List<string> objRecipeArr, ArrayList objYBArr, ref string BillNO)
        {
            long lngRes = 0;

            lngRes = this.m_lngSendybdata(DSN, objYBArr, ref BillNO);
            if (lngRes == 1)
            {
                //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
                //                            (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));
                lngRes = proxy.Service.m_lngSendybdata(objRecipeArr, BillNO);
                if (lngRes == 0)
                {
                    lngRes = this.m_lngDelybdata(DSN, BillNO);
                }
            }

            return lngRes;
        }
        #endregion

        #region (ҽ��)���������շ����ݵ�ҽ��ǰ�û�
        /// <summary>
        /// (ҽ��)���������շ����ݵ�ҽ��ǰ�û�
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>
        public long m_lngSendybdata(string DSN, ArrayList objYBArr, ref string BillNO)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYB objSvc =
            //                            (com.digitalwave.iCare.middletier.HIS.clsYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYB));
            //long lngRes = (new weCare.Proxy.ProxyYB()).Service.m_lngSendybdata(DSN, objYBArr, ref BillNO);
            //return lngRes;
            return 0;
        }
        #endregion

        #region (ҽ��)����ʱHIS����ʧ�ܣ��ֹ�ɾ����������
        /// <summary>
        /// (ҽ��)����ʱHIS����ʧ�ܣ��ֹ�ɾ����������
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="billno"></param>
        /// <returns></returns>
        public long m_lngDelybdata(string DSN, string billno)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYB objSvc =
            //                            (com.digitalwave.iCare.middletier.HIS.clsYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYB));
            //long lngRes = (new weCare.Proxy.ProxyYB()).Service.m_lngDelybdata(DSN, billno);
            //return lngRes;
            return 0;
        }
        #endregion

        #region (ҽ��)��ȡҽ��������ϸ
        /// <summary>
        /// (ҽ��)��ȡҽ��������ϸ
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="Hospcode"></param>
        /// <param name="Billno"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetybjsmx(string DSN, string Hospcode, string Billno, out DataTable dtRecord)
        {
            dtRecord = null;
            //com.digitalwave.iCare.middletier.HIS.clsYB objSvc =
            //                            (com.digitalwave.iCare.middletier.HIS.clsYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYB));
            //long lngRes = (new weCare.Proxy.ProxyYB()).Service.m_lngGetybjsmx(DSN, Hospcode, Billno, out dtRecord);
            //return lngRes;
            return 0;
        }
        #endregion

        #region (ҽ��)�������Է���Ŀ���Ƿ����ָ����BILLNO
        /// <summary>
        /// (ҽ��)�������Է���Ŀ���Ƿ����ָ����BILLNO
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="Hospcode"></param>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public bool m_blnCheckBillNo(string DSN, string Hospcode, string BillNo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYB objSvc =
            //                            (com.digitalwave.iCare.middletier.HIS.clsYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYB));
            //bool b = (new weCare.Proxy.ProxyYB()).Service.m_blnCheckBillNo(DSN, Hospcode, BillNo);
            //return b;
            return false;
        }
        #endregion

        #region (ҽ��)��ѯ�����շ���Ŀ�Ƿ�ɹ�
        /// <summary>
        /// (ҽ��)��ѯ�����շ���Ŀ�Ƿ�ɹ�
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="Hospcode"></param>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public bool m_blnCheckSendRes(string DSN, string Hospcode, string BillNo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYB objSvc =
            //                                        (com.digitalwave.iCare.middletier.HIS.clsYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYB));
            //bool b = (new weCare.Proxy.ProxyYB()).Service.m_blnCheckSendRes(DSN, Hospcode, BillNo);
            //return b;
            return false;
        }
        #endregion

        #region (ҽ��)�ֹ����ļ��ʵ���
        /// <summary>
        /// (ҽ��)�ֹ����ļ��ʵ���
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="OldBillNo"></param>
        /// <param name="NewBillNo"></param>
        /// <returns></returns>
        public long m_lngModifyBillNo(string DSN, string OldBillNo, string NewBillNo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYB objSvc =
            //                            (com.digitalwave.iCare.middletier.HIS.clsYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYB));
            //long lngRes = (new weCare.Proxy.ProxyYB()).Service.m_lngModifyBillNo(DSN, OldBillNo, NewBillNo);
            //return lngRes;
            return 0;
        }
        #endregion

        #region (ҽ��)�����µļ��ʵ���
        /// <summary>
        /// (ҽ��)�����µļ��ʵ���
        /// </summary>
        /// <param name="BillNo"></param>
        public void m_mthGenBillNo(out string BillNo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            proxy.Service.m_mthGenBillNo(out BillNo);
            //objSvc.Dispose();
        }
        #endregion

        #region (ҽ��)�ֹ����¼��ʵ���
        /// <summary>
        /// (ҽ��)�ֹ����¼��ʵ���
        /// </summary>
        /// <param name="OldBillNo"></param>
        /// <param name="NewBillNo"></param>
        /// <returns></returns>
        public long m_lngModifyBillNo(string OldBillNo, string NewBillNo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));

            long l = proxy.Service.m_lngModifyBillNo(OldBillNo, NewBillNo);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region (˳���ض�ҽ��)
        /// <summary>
        /// (˳���ض�ҽ��)
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="InsuranceID"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>
        public long m_lngSDTDMZYB(string DSN, string RecipeID, string DeptID, string EmpID, string InsuranceID, ArrayList objYBArr, out decimal YBMoney, out string OutMsg)
        {
            YBMoney = 0;
            OutMsg = "";
            //com.digitalwave.iCare.middletier.HIS.clsLunJiao objSvc =
            //                                                (com.digitalwave.iCare.middletier.HIS.clsLunJiao)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsLunJiao));

            //long l = proxy.Service.m_lngSDTDMZYB(DSN, RecipeID, DeptID, EmpID, InsuranceID, objYBArr, out YBMoney, out OutMsg);
            //////objSvc.Dispose();
            //return l;
            return 0;
        }
        #endregion

        #region ��ȡ��������Ŀ
        /// <summary>
        /// ��ȡ��������Ŀ
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetRoundingItem(string ItemID, out DataTable dt, bool isChildPrice)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            long l = proxy.Service.m_lngGetRoundingItem(ItemID, out dt, isChildPrice);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region ����ҽ��ID��ȡ��ְ��
        /// <summary>
        /// ����ҽ��ID��ȡ��ְ��
        /// </summary>
        /// <param name="DoctID"></param>
        /// <returns></returns>        
        public string m_strGetTechnicalRank(string DoctID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            string s = proxy.Service.m_strGetTechnicalRank(DoctID);
            //objSvc.Dispose();
            return s;
        }
        #endregion

        #region (���)��ȡ������շ���Ŀ
        /// <summary>
        /// (���)��ȡ������շ���Ŀ
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPEChargeItem(string CardNo, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            long l = proxy.Service.m_lngGetPEChargeItem(CardNo, out dt);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region (���)��������շ���ĿID��ȡ����շ���Ϣ
        /// <summary>
        /// (���)��������շ���ĿID��ȡ����շ���Ϣ  
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="PatType"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPEChargeItemInfo(string ItemID, string PatType, out DataTable dt, bool isChildPrice)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            long l = proxy.Service.m_lngGetPEChargeItemInfo(ItemID, PatType, out dt, isChildPrice);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region (���)��ȡ����˼������뵥Ԫ
        /// <summary>
        /// (���)��ȡ����˼������뵥Ԫ
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPELisItem(string CardNo, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            long l = proxy.Service.m_lngGetPELisItem(CardNo, out dt);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region (���)��������շѱ�־
        /// <summary>
        /// (���)��������շѱ�־
        /// </summary>
        /// <param name="RegNoArr"></param>
        /// <returns></returns>        
        public long m_lngUpdatePEChargeFlag(List<string> RegNoArr, List<clsATTACHRELATION_VO> objAttach, System.Collections.Generic.List<clsPERegGroup_VO> glstRegGroup)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));

            long l = proxy.Service.m_lngUpdatePEChargeFlag(RegNoArr, objAttach, glstRegGroup);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region (���)��ȡ����ײ���Ϣ
        /// <summary>
        /// (���)��ȡ����ײ���Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable GetPeCluster()
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            DataTable dt = proxy.Service.GetPeCluster();
            //objSvc.Dispose();
            return dt;
        }
        #endregion

        #region (���)��ȡ��������Ϣ
        /// <summary>
        /// (���)��ȡ��������Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable GetPeComb()
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            DataTable dt = proxy.Service.GetPeComb();
            //objSvc.Dispose();
            return dt;
        }
        #endregion

        #region m_lngGetPEDoctor
        public long m_lngGetPEDoctor(string CardNo, out clsEmployeeVO objDoctor)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            long l = proxy.Service.m_lngGetPEDoctor(CardNo, out objDoctor);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region (��ɽҽ��)����DBF�ļ�
        /// <summary>
        /// (��ɽҽ��)����DBF�ļ�
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="DbfName"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>  
        public long m_lngCreateDbf_OutPatient(string DSN, string DbfName, ArrayList objYBArr)
        {
            com.digitalwave.iCare.middletier.HIS.clsChaShan objSvc = new middletier.HIS.clsChaShan();
            //(com.digitalwave.iCare.middletier.HIS.clsChaShan)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChaShan));

            long l = objSvc.m_lngCreateDbf_OutPatient(DSN, DbfName, objYBArr);

            return l;
        }
        #endregion

        #region (ҽ��)��ȡ���
        /// <summary>
        /// (ҽ��)��ȡ���
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="DbfName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetResult_OutPatient(string DSN, string DbfName, out DataTable dtRecord)
        {
            com.digitalwave.iCare.middletier.HIS.clsChaShan objSvc = new middletier.HIS.clsChaShan();
            // (com.digitalwave.iCare.middletier.HIS.clsChaShan)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChaShan));

            long l = objSvc.m_lngGetResult_OutPatient(DSN, DbfName, out dtRecord);

            return l;
        }
        #endregion

        #region ��ȡ��ĿסԺ��Ʊ����
        /// <summary>
        /// ��ȡ��ĿסԺ��Ʊ����
        /// </summary>
        /// <param name="p_strItemID"></param>
        /// <param name="p_hasCat"></param>
        /// <returns></returns>
        public long m_lngGetIPInvoCat(string p_strItemID, out Dictionary<string, string> p_hasCat)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            long l = proxy.Service.m_lngGetIPInvoCat(p_strItemID, out p_hasCat);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region ����ҽ�������
        /// <summary>
        /// ����ҽ�������
        /// </summary>
        /// <param name="p_strRecipeID"></param>
        /// <param name="p_strChargeNo"></param>
        /// <returns></returns>
        public long m_lngUpdateYBChargeNo(string p_strRecipeID, string p_strChargeNo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));

            long l = proxy.Service.m_lngUpdateYBChargeNo(p_strRecipeID, p_strChargeNo);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region ��֤ĳһҽ�������Ƿ��ĳһ���˿�������
        /// <summary>
        /// ��֤ĳһҽ�������Ƿ��ĳһ���˿�������
        /// </summary>
        /// <param name="p_strPatientId"></param>
        /// <param name="p_strDoctorId"></param>
        /// <param name="p_blnResult"></param>
        /// <returns></returns>
        public bool m_blnValidatePatientRecipeByDoctor(string p_strPatientId, string p_strDoctorId)
        {
            bool blnResult = false;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            blnResult = proxy.Service.m_blnValidatePatientRecipeByDoctor(p_strPatientId, p_strDoctorId);
            //objSvc.Dispose();
            //objSvc = null;
            return blnResult;
        }
        #endregion

        #region ��֤��Ʊ�Ƿ��ǵ�ǰҽ������
        /// <summary>
        /// ��֤��Ʊ�Ƿ��ǵ�ǰҽ������
        /// </summary>
        /// <param name="p_strEmpId"></param>
        /// <param name="p_strInvoiceNo"></param>
        /// <returns></returns>
        public bool m_blnCheckInvoice(string p_strEmpId, string p_strInvoiceNo)
        {
            bool blnResult = false;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            blnResult = proxy.Service.m_blnCheckInvoice(p_strEmpId, p_strInvoiceNo);
            //objSvc.Dispose();
            //objSvc = null;
            return blnResult;
        }
        #endregion

        #region ����ѡ���Խ���
        /// <summary>
        /// ����ѡ���Խ���
        /// </summary>
        /// <param name="p_dtbChargeItem"></param>
        /// <param name="p_oprVO"></param>
        /// <param name="p_strOriginalRepiceId"></param>
        /// <returns></returns>
        public long m_lngSelectFeeDispose(Dictionary<string, List<string>> p_dicItemID, clsOutPatientRecipe_VO p_oprVO, string p_strOriginalRepiceId)
        {
            long lngRes = -1;

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));

            lngRes = proxy.Service.m_lngSelectFeeDispose(p_dicItemID, p_oprVO, p_strOriginalRepiceId);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="medid"></param>
        /// <param name="blnCmRecipe"></param>
        /// <param name="m_objMedStoreVo"></param>
        /// <returns></returns>
        public long m_lngGetsendmedinfoBypid(string pid, string medid, out clsMedStoreWindowsVo m_objMedStoreVo)
        {
            long lngRes = -1;
            //com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc));

            lngRes = proxy.Service.m_lngGetsendmedinfoBypid(pid, medid, false, out m_objMedStoreVo);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ����PID��ȡ���ߵ��췢ҩ��Ϣ(ֻҪ��ͨ�����ϲ�ҩ��֮���õ�)
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="medid"></param>
        /// <param name="blnCmRecipe"></param>
        /// <param name="m_objMedStoreVo"></param>
        /// <returns></returns>
        public long m_lngGetsendgeneralmedinfoBypid(string pid, string medid, out clsMedStoreWindowsVo m_objMedStoreVo)
        {
            long lngRes = -1;
            //com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc));

            lngRes = proxy.Service.m_lngGetsendgeneralmedinfoBypid(pid, medid, false, out m_objMedStoreVo);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        public long m_lngGetespecialwinNew(string deptid, string medid, out clsMedStoreWindowsVo objMedStoreVo)
        {
            long lngRes = -1;
            //com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc));

            lngRes = proxy.Service.m_lngGetespecialwin(deptid, medid, out objMedStoreVo);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }


        /// <summary>
        /// ��ȡ��ǰ����ҩ���ںͷ�ҩ����(Ҳ���Ǽ��ҩ����ҩ��������ҩ������С�Ĵ��ڣ�
        /// </summary> 
        /// <param name="storageID">ҩ��id</param>
        /// <param name="m_objWindowsVo">����Ҳ������ʵ���ҩ���ںͷ�ҩ���ڣ�����null</param>
        /// <param name="CheckScope">ҩ��ר�ô����Ƿ���Խ������п��Ҵ��� true ���� false ��ֹ ������0057</param>
        /// <param name="m_blnWindowType">�Ƿ��ҩ���ڱ�־��false-��true-��</param>
        /// <param name="m_blnWindowRelation">�䡢��ҩ�����Ƿ������ϵ</param>
        /// <returns></returns>
        public long lngGetWindowIDByStorage(string storageID, out clsMedStoreWindowsVo m_objWindowsVo)
        {
            long lngRes = -1;
            //com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc));

            lngRes = proxy.Service.lngGetWindowIDByStorage(storageID, out m_objWindowsVo, false, false, false);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_mthGetWindowName(out DataTable dtbResult)
        {
            long lngRes = -1;
            //com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc));

            lngRes = proxy.Service.m_mthGetWindowName(out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }


        public long lngOnlyGetSendWindowInfo(string m_strMedStoreid, out clsMedStoreWindowsVo m_objWindowsVo, bool m_blnWindowType)
        {
            long lngRes = -1;
            //com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPMedStore_Supported_Svc));

            lngRes = proxy.Service.lngOnlyGetSendWindowInfo(m_strMedStoreid, out m_objWindowsVo, false);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #region ��ѯ�ڷ�����ҩ�÷�ID
        /// <summary>
        /// ��ѯ�ڷ�����ҩ�÷�ID
        /// </summary>
        /// <param name="p_strParmCode"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_lngGetMedUsageID(string p_strParmCode, out DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = null;
            #region �м������
            //clsOPChargeQuerySvc objServ = null;
            try
            {
                //objServ = (clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOPChargeQuerySvc));
                lngRes = proxy.Service.m_lngGetMedUsageID(p_strParmCode, out dtResult);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
                //if (objServ != null)
                //{
                //    objServ.Dispose();
                //    objServ = null;
                //}
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region �����ƺ�������������ʱ�������ǰ�ɷѵ���ˮ��
        /// <summary>
        /// �����ƺ�������������ʱ�������ǰ�ɷѵ���ˮ��
        /// </summary>
        /// <param name="p_Recipe"></param>
        /// <param name="strseqid"></param>
        /// <returns></returns>
        public long m_lngGetRecipeByRecipeNo(string p_Recipe, out string strseqid)
        {
            long lngRes = -1;
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            lngRes = proxy.Service.m_lngGetRecipeByRecipeNo(p_Recipe, out strseqid);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ���ݲ���ID��ѯ�����Ƿ���������ƺ����
        /// <summary>
        /// ���ݲ���ID��ѯ�����Ƿ���������ƺ����
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        internal long m_lngGetIfVipByPatientID(string p_strPatientID, out DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = null;
            #region �м������
            //clsOPChargeQuerySvc objServ = null;
            try
            {
                //objServ = (clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOPChargeQuerySvc));
                lngRes = proxy.Service.m_lngGetIfVipByPatientID(p_strPatientID, out dtResult);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
                //if (objServ != null)
                //{
                //    objServ.Dispose();
                //    objServ = null;
                //}
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region �籣���߲��ҵ��վ�����
        /// <summary>
        /// �籣���߲��ҵ��վ�����
        /// </summary>
        /// <param name="regDate"></param>
        /// <param name="deptId"></param>
        /// <param name="doctId"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable GetOpRegNo(string regDate, string deptId, string doctId, string pid)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));

            DataTable dt = proxy.Service.GetOpRegNo(regDate, deptId, doctId, pid);
            //objSvc.Dispose();
            return dt;
        }
        #endregion

        #region �籣����д�뵱�վ�����
        /// <summary>
        /// �籣����д�뵱�վ�����
        /// </summary>
        /// <param name="regNo"></param>
        /// <param name="regDate"></param>
        /// <param name="pid"></param>
        /// <param name="deptId"></param>
        /// <param name="doctId"></param>
        /// <param name="diagCode"></param>
        /// <returns></returns>
        public long SaveOpRegNo(string regNo, string regDate, string pid, string deptId, string doctId, string diagCode)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));

            long ret = proxy.Service.SaveOpRegNo(regNo, regDate, pid, deptId, doctId, diagCode);
            //objSvc.Dispose();
            return ret;
        }
        #endregion

        #region ��ȡ�Զ��˶Թ���
        /// <summary>
        /// ��ȡ�Զ��˶Թ���
        /// </summary>
        /// <returns></returns>
        public DataTable GetAutoCheckRule()
        {
            //using (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc svc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)))
            //{
            return proxy.Service.GetAutoCheckRule();
            //}
        }
        #endregion

    }
}
