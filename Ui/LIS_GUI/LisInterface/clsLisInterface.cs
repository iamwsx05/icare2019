using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{

    #region Lis�ӿڷ�����

    /// <summary>
    /// סԺҽ����LIS�ӿ�
    /// </summary>
    public class clsLisInterface
    {

        #region ��  ��

        private string operatorID = string.Empty;
        private clsLIS_App m_currentApplication = null;
        private ArrayList m_arrResults = new ArrayList();
        private string errorMessage = string.Empty; //�����˵����Ϣ 
        private NameValueCollection m_unitOrder = new NameValueCollection();
      
        #endregion

        #region �ӿڷ���

        /// <summary>
        /// ���ؽ��
        /// </summary>
        /// <returns></returns>
        public clsLISAppResults[] m_objGetMutiResults()
        {
            clsLISAppResults[] arrResult = (clsLISAppResults[])m_arrResults.ToArray(typeof(clsLISAppResults));
            return arrResult;
        }

        #endregion

        #region �½��������뵥

        /// <summary>
        /// �½��������뵥
        /// </summary>
        /// <remarks> </remarks>
        /// <param name="p_objPatientInfo">���߻�����Ϣ</param>
        /// <param name="p_objChargeInfoArr">�շ���Ϣ����</param>
        /// <param name="p_blnSend">���뵥�Ƿ���</param>
        /// <returns>�Ƿ񴴽��ɹ�</returns>
        public bool m_mthNewApp(clsLisApplMainVO patientInfo, clsTestApplyItme_VO[] arrChargeInfo, bool isSended)
        {

            clsAppCollection appCollection = InitNewApplication(patientInfo, arrChargeInfo);
            if (appCollection == null)
            {
                return false;
            }

            ArrayList separatedUnits = SeparateApplication(GetUnitId(appCollection));

            foreach (string[] arrUnit in separatedUnits)
            {
                CreateApplicationDB(patientInfo, arrChargeInfo, isSended, arrUnit);
            }

            return true;
        }

        private bool CreateApplicationDB(clsLisApplMainVO patientInfo, clsTestApplyItme_VO[] arrChargeInfo, bool isSended, string[] strUnits)
        {
            clsLIS_App objApp = null;
            ArrayList arrChargeId = null;
            bool isConstruct = ConstructAppliction(patientInfo, arrChargeInfo, strUnits, out objApp, out arrChargeId);
            if (!isConstruct)
            {
                return false;
            }

            this.m_currentApplication = objApp;
            string strMessage = null;

            if (!m_blnSaveApp(out strMessage, m_currentApplication))
            {
                errorMessage = strMessage;
                return false;
            }

            if (isSended)
            {
                if (!m_blnSendApp(out strMessage, m_currentApplication))
                {
                    errorMessage = strMessage;
                    return false;
                }
            }

            m_arrResults.Add(GetApplicationResult(objApp, arrChargeId));
            return true;
        }

        private static ArrayList GetUnitId(clsAppCollection appCollection)
        {
            ArrayList arrUnitIds = new ArrayList();//���UnitID���뵥Ԫ��
            int appCount = appCollection.Count;
            for (int i = 0; i < appCount; i++)
            {
                int unitCount = appCollection[i].m_ObjAppApplyUnits.Count;
                string[] strUnits = new string[unitCount];

                for (int j = 0; j < unitCount; j++)
                {
                    strUnits[j] = appCollection[i].m_ObjAppApplyUnits[j].m_StrApplyUnitID;
                }

                arrUnitIds.Add(strUnits);
            }
            return arrUnitIds;
        }

        private clsAppCollection InitNewApplication(clsLisApplMainVO p_objPatientInfo, clsTestApplyItme_VO[] p_objChargeInfoArr)
        {

            m_arrResults.Clear();

            string[] arrApplyUnitId = GetDifferentUnits(p_objChargeInfoArr);

            bool isUnValid = p_objPatientInfo == null || p_objChargeInfoArr == null || p_objChargeInfoArr.Length == 0
                             || arrApplyUnitId == null || arrApplyUnitId.Length == 0;

            if (isUnValid)
            {
                errorMessage = "������ϢΪ�ջ������뵥ԪIDΪ�գ�";
                return null;
            }

            p_objPatientInfo.m_intPStatus_int = 1; //δ���͵ļ�¼
            p_objPatientInfo.m_intForm_int = 1;

            clsGeneratorCheckContent generatorCheckContent = new clsGeneratorCheckContent(arrApplyUnitId);
            clsAppCollection appCollection = generatorCheckContent.Apps;

            if (appCollection == null)
            {
                return null;
            }
            return appCollection;
        }

        /// <summary>
        /// ��ȡ����Ľ��
        /// </summary>
        /// <param name="objApp">���뵥VO</param>
        /// <param name="arlChargeIDs">����</param>
        /// <returns></returns>
        private clsLISAppResults GetApplicationResult(clsLIS_App application, ArrayList arrChargeId)
        {

            clsLISAppResults objResult = new clsLISAppResults();
            objResult.m_StrApplicationID = application.m_ObjDataVO.m_strAPPLICATION_ID;
            objResult.m_strSampleTypeID = application.m_ObjDataVO.m_strSampleTypeID;
            objResult.m_strSampleTypeName = application.m_ObjDataVO.m_strSampleType;
            objResult.m_StrAppCheckContentDesc = application.m_ObjDataVO.m_strCheckContent;

            int applyUnitCount=application.m_ObjAppApplyUnits.Count;
            objResult.m_ObjApplyUnitIDArr = new string[applyUnitCount];

            List<string> lstOrderId=new List<string>();
            for (int i = 0; i < applyUnitCount; i++)
            {
                objResult.m_ObjApplyUnitIDArr[i] = application.m_ObjAppApplyUnits[i].m_ObjDataVO.m_strAPPLY_UNIT_ID_CHR;
                string[] arrOrder = m_unitOrder.GetValues(application.m_ObjAppApplyUnits[i].m_ObjDataVO.m_strAPPLY_UNIT_ID_CHR);
                lstOrderId.AddRange(arrOrder);
            }

            objResult.m_arrOrderId = lstOrderId.ToArray();
            objResult.m_strChargeIDs = (string[])arrChargeId.ToArray(typeof(string));
            return objResult;
        }

        private bool ConstructAppliction(clsLisApplMainVO p_objPatientInfo, clsTestApplyItme_VO[] p_objChargeInfoArr, string[] strUnits, out clsLIS_App objApp, out ArrayList arrChargeId)
        {
            objApp = null;
            arrChargeId = null;

            clsGeneratorCheckContent checkContent = new clsGeneratorCheckContent(strUnits);
            if (checkContent.Apps == null)
            {
                return false;
            }

            //�Զ���������뵥����
            clsLisApplMainVO objMainVO = new clsLisApplMainVO();
            p_objPatientInfo.m_mthCopyTo(objMainVO);

            objApp = new clsLIS_App(objMainVO);
            objApp.m_ObjAppApplyUnits.AddRange(checkContent.AppApplyUnits);
            objApp.m_ObjAppReports.AddRange(checkContent.AppReports);
            objApp.m_ObjDataVO.m_strSampleTypeID = m_mthGetSampleType(strUnits);
            objApp.m_ObjDataVO.m_strSampleType = new ctlLISSampleTypeComboBox().m_strGetTypeName(objApp.m_ObjDataVO.m_strSampleTypeID);
            objApp.m_ObjDataVO.m_strAppl_Dat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            arrChargeId = new ArrayList();

            foreach (clsTestApplyItme_VO objTestVO in p_objChargeInfoArr)
            {
                foreach (string strUnit in strUnits)
                {
                    arrChargeId.Add(objTestVO.strPartID);
                    m_unitOrder.Add(objTestVO.m_strItemID, objTestVO.m_strOrderID);
                }
            }

            //�����΢����걾�����ȡҽ��ʵ��ѡ��ı걾����!
            bool isGermSample = strUnits != null && strUnits.Length == 1 && p_objChargeInfoArr.Length > 0
                                && !string.IsNullOrEmpty(objApp.m_ObjDataVO.m_strSampleType) 
                                && objApp.m_ObjDataVO.m_strSampleType.Trim() == "΢����걾";
            if (isGermSample)
            {
                foreach (clsTestApplyItme_VO applyItem in p_objChargeInfoArr)
                {
                    if (applyItem.m_strItemID == strUnits[0])
                    {
                        objApp.m_ObjDataVO.m_strSampleTypeID = applyItem.m_strUsageID;
                        objApp.m_ObjDataVO.m_strSampleType = new ctlLISSampleTypeComboBox().m_strGetTypeName(objApp.m_ObjDataVO.m_strSampleTypeID);
                    }
                }
            }

            //�շ���Ϣ
            objApp.m_ObjDataVO.m_strChargeInfo = GetChargeInfo(p_objChargeInfoArr, strUnits);
            //��ȡ��������
            objApp.m_ObjDataVO.m_strCheckContent = GetCheckContent(objApp);
            objApp.m_ObjDataVO.m_strOperator_ID = operatorID;

            return true;
        }

        /// <summary>
        /// �ֵ�����
        /// </summary>
        /// <param name="arrUnitIds"></param>
        /// <returns></returns>
        private ArrayList SeparateApplication(ArrayList arrUnitIds)
        {
            ArrayList arlUnits = new ArrayList();
            foreach (string[] unitIDArr in arrUnitIds)
            {
                //�ֵ�����
                clsSeparateCheckApplication objSep = new clsSeparateCheckApplication();
                clsSeparatedApp[] objSepApps = objSep.m_mthSeparateCheckApplication(unitIDArr);

                foreach (clsSeparatedApp obj in objSepApps)
                {
                    arlUnits.Add(obj.m_strApplyUnits);
                }
            }

            return arlUnits;
        }

        /// <summary>
        /// ��ȡ���뵥ԪId��
        /// </summary>
        /// <param name="p_strApplyUnitIDArr">�շ���Ŀ��Ϣ�����Ƶ�Ԫ��Ϣ</param>
        /// <returns>���뵥ԪID��</returns>
        private string[] GetDifferentUnits(clsTestApplyItme_VO[] p_objChargeInfoArr)
        {
            if (p_objChargeInfoArr == null || p_objChargeInfoArr.Length == 0)
            {
                return new string[0];
            }

            List<string> lstUnitId = new List<string>();
            foreach (clsTestApplyItme_VO applyUnitItem in p_objChargeInfoArr)
            {
                if (!string.IsNullOrEmpty(applyUnitItem.m_strItemID))
                {
                    string unitId = applyUnitItem.m_strItemID;
                    if (!lstUnitId.Contains(unitId))
                    {
                        lstUnitId.Add(unitId);
                    }
                }
            }
            return lstUnitId.ToArray();
        }

        #endregion
       
        #region ��������

        /// <summary>
        /// ��ȡ�걾����
        /// </summary>
        /// <param name="arrAppUnitId">���뵥ԪID</param>
        /// <returns>�걾����</returns>
        private string m_mthGetSampleType(string[] arrAppUnitId)
        {
            string[] arrSampleTypeId = null;
            clsApplyUnitSmp.s_obj.m_lngGetSampleTypeIdList(arrAppUnitId, out arrSampleTypeId);
            if (arrSampleTypeId != null && arrSampleTypeId.Length > 0)
            {
                return arrSampleTypeId[0];
            }
            return null;
        }

        /// <summary>
        /// �������뵥
        /// </summary>
        /// <param name="p_strMessage">��Ϣ</param>
        /// <param name="objApp"></param>
        /// <returns></returns>
        private bool m_blnSaveApp(out string errorMessage, clsLIS_App objApp)
        {
            errorMessage = string.Empty;

            #region ���챣�����

            ArrayList arlRep = new ArrayList();
            ArrayList arlSam = new ArrayList();
            ArrayList arlItem = new ArrayList();
            ArrayList arlUnit = new ArrayList();
            ArrayList arlUnitItem = new ArrayList();

            foreach (clsLIS_AppCheckReport objRep in objApp.m_ObjAppReports)
            {
                arlRep.Add(objRep.m_ObjDataVO);
                foreach (clsLIS_AppSampleGroup objSam in objRep.m_ObjAppSampleGroups)
                {
                    arlSam.Add(objSam.m_ObjDataVO);
                    foreach (clsLIS_AppCheckItem objItem in objSam.m_ObjAppCheckItems)
                    {
                        arlItem.Add(objItem.m_ObjDataVO);
                    }
                }
            }

            foreach (clsLIS_AppApplyUnit objUnit in objApp.m_ObjAppApplyUnits)
            {
                arlUnit.Add(objUnit.m_ObjDataVO);
                arlUnitItem.AddRange(objUnit.m_ObjItemArr);
            }

            clsT_OPR_LIS_APP_REPORT_VO[] objRepArr = (clsT_OPR_LIS_APP_REPORT_VO[])arlRep.ToArray(typeof(clsT_OPR_LIS_APP_REPORT_VO));
            clsT_OPR_LIS_APP_SAMPLE_VO[] objSamArr = (clsT_OPR_LIS_APP_SAMPLE_VO[])arlSam.ToArray(typeof(clsT_OPR_LIS_APP_SAMPLE_VO));
            clsT_OPR_LIS_APP_CHECK_ITEM_VO[] objItemArr = (clsT_OPR_LIS_APP_CHECK_ITEM_VO[])arlItem.ToArray(typeof(clsT_OPR_LIS_APP_CHECK_ITEM_VO));
            clsT_OPR_LIS_APP_APPLY_UNIT_VO[] objUnitArr = (clsT_OPR_LIS_APP_APPLY_UNIT_VO[])arlUnit.ToArray(typeof(clsT_OPR_LIS_APP_APPLY_UNIT_VO));
            clsLisAppUnitItemVO[] objUnitItemArr = (clsLisAppUnitItemVO[])arlUnitItem.ToArray(typeof(clsLisAppUnitItemVO));

            #endregion

            if (objApp.m_StrAppID == null)
            {
                long lngRes = 0;

                //4002����:�Ƿ����������Ĳɼ��ͺ���
                int intConfig=clsLisSetting.IsSkipCollectionReceive();
                switch(intConfig)
                {
                    case 1:
                        objApp.m_ObjDataVO.m_intPStatus_int = 2;
                        lngRes = 0;
                        lngRes = clsApplicationBizSmp.s_obj.m_lngAddApplicationInfo(objApp.m_ObjDataVO, objRepArr, objSamArr, objItemArr, objUnitArr, objUnitItemArr);
                        break;
                    case 2:   //�����ɼ�����������		
                        lngRes = 0;
                        objApp.m_ObjDataVO.m_intPStatus_int = 2;
                        lngRes = clsApplicationBizSmp.s_obj.m_lngAddNewAppInfo(objApp.m_ObjDataVO, objRepArr, objSamArr, objItemArr, objUnitArr, objUnitItemArr);
                        break;
                    default:
                        lngRes = 0;
                        lngRes = clsApplicationBizSmp.s_obj.m_lngAddNewAppInfo(objApp.m_ObjDataVO, objRepArr, objSamArr, objItemArr, objUnitArr, objUnitItemArr);
                        new com.digitalwave.Utility.clsLogText().LogError(errorMessage);
                        break;
                }                

                if (lngRes > 0)
                {
                    objApp.m_mthAcceptChanges();
                    return true;
                }
            }
            else if (objApp.m_ObjAppApplyUnits.Count > 0)
            {
                long lngRes = clsApplicationBizSmp.s_obj.m_lngModifyAppInfo(objApp.m_ObjDataVO, objRepArr, objSamArr, objItemArr, objUnitArr, objUnitItemArr);
                if (lngRes > 0)
                {
                    objApp.m_mthAcceptChanges();
                    return true;
                }
            }
            else
            {
                long lngRes = clsApplicationSmp.s_obj.m_lngAddNewApplication(objApp.m_ObjDataVO);
                if (lngRes > 0)
                {
                    objApp.m_mthAcceptChanges();
                    return true;
                }
            }

            errorMessage = "����ʧ��!";
            return false;
        }

        /// <summary>
        /// �������뵥
        /// </summary>
        /// <param name="p_strMessage">��Ϣ</param>
        /// <param name="objApp"></param>
        /// <returns>��/��ɹ�</returns>
        private bool m_blnSendApp(out string message, clsLIS_App objApp)
        {
            message = "";
            if (objApp.m_StrAppID == null || objApp.m_StrAppID == "")
            {
                message = "���ȱ�������!";
                return false;
            }

            objApp.m_IntPStatus = 2;

            long lngRes = clsApplicationSmp.s_obj.m_lngAddNewApplication(objApp.m_ObjDataVO);
            if (lngRes > 0)
            {
                objApp.m_mthAcceptChanges();
                return true;
            }

            objApp.m_IntPStatus = 1;
            objApp.m_StrOperatorID = objApp.m_ObjOriginalDataVO.m_strOperator_ID;
            message = "����ʧ��!";
            return false;
        }

        /// <summary>
        /// ��ȡ������Ŀ����
        /// </summary>
        /// <param name="objAppInfo"></param>
        /// <returns></returns>
        private string GetCheckContent(clsLIS_App objAppInfo)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (clsLIS_AppApplyUnit obj in objAppInfo.m_ObjAppApplyUnits)
            {

                if (obj.m_ObjApplyUnit.m_ObjDataVO.strApplUnitName != null && obj.m_ObjApplyUnit.m_ObjDataVO.strApplUnitName.Trim() != "")
                {
                    sb.Append(obj.m_ObjApplyUnit.m_ObjDataVO.strApplUnitName + ",");
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        /// <summary>
        /// ��ȡ�շ���Ϣ
        /// </summary>
        /// <param name="p_objChargeInfoArr"></param>
        /// <param name="strUnits"></param>
        /// <returns></returns>
        private string GetChargeInfo(clsTestApplyItme_VO[] p_objChargeInfoArr, string[] strUnits)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (clsTestApplyItme_VO objTestVO in p_objChargeInfoArr)
            {
                foreach (string strUnit in strUnits)
                {
                    if (strUnit == objTestVO.m_strItemID)
                    {
                        sb.Append(objTestVO.m_strItemName == null ? "" : objTestVO.m_strItemName.Trim());
                        sb.Append(">");
                        sb.Append(objTestVO.m_strSpec == null ? "" : objTestVO.m_strSpec.Trim());
                        sb.Append(">");
                        sb.Append(objTestVO.m_decQty.ToString() + "" + objTestVO.m_strUnit);
                        sb.Append(">");
                        sb.Append(objTestVO.m_decTolPrice.ToString());
                        sb.Append(">");
                        sb.Append(objTestVO.m_decDiscount.ToString() + "%");
                        sb.Append("|");
                    }
                }
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }

        #endregion

    } 

    #endregion

    #region �������뵥Ԫ������ص����ݸ�����

    /// <summary>
    /// �������뵥Ԫ������ص�������
    /// </summary>
    internal class clsGeneratorCheckContent
    {

        #region ˽�г�Ա

        private clsAppCollection m_objApps = new clsAppCollection();
        private clsAppCheckReportCollection m_objAppReports = new clsAppCheckReportCollection();
        private clsAppApplyUnitCollection m_objAppApplyUnits = new clsAppApplyUnitCollection();
        private string errorMessage = string.Empty;
        
        #endregion

        #region ���캯��

        public clsGeneratorCheckContent(string[] arrUnitId)
        {
            m_mthGenerateAppContent(arrUnitId);
        }
        
        #endregion

        #region ��  ��

        /// <summary>
        /// �����õļ�����������
        /// </summary>
        public clsAppCollection Apps
        {
            get
            {
                return m_objApps;
            }
        }

        /// <summary>
        /// ������󼯺�
        /// </summary>
        public clsAppCheckReportCollection AppReports
        {
            get { return m_objAppReports; }
        }

        /// <summary>
        /// ���뵥Ԫ���󼯺�
        /// </summary>
        public clsAppApplyUnitCollection AppApplyUnits
        {
            get { return m_objAppApplyUnits; }
        } 

        #endregion

        #region �������뵥�ı�����

        /// <summary>
        /// �������뵥�ı����顾1��
        /// </summary>
        /// <param name="p_strApplyUnitsIDArr"></param>
        /// <returns></returns>
        private void m_mthGenerateAppContent(string[] p_strApplyUnitsIDArr)
        {
            //�γ����뵥�����뵥Ԫ
            m_mthGenerateAppApplyUnits(p_strApplyUnitsIDArr);

            if (m_objAppApplyUnits.Count == 0) 
            {
                return;
            }
                

            //�������뵥�ı�����
            m_mthGenerateAppReports();

            foreach (clsLIS_AppCheckReport objReport in m_objAppReports)
            {
                clsLisApplMainVO objAppVO = new clsLisApplMainVO();
                clsLIS_App objApp = new clsLIS_App(objAppVO, false);

                foreach (clsLIS_AppSampleGroup objSG in objReport.m_ObjAppSampleGroups)
                {
                    for (int i = 0; i < objSG.m_ObjAppUnitArr.Length; i++)
                    {
                        objApp.m_ObjAppApplyUnits.Add(objSG.m_ObjAppUnitArr[i]);
                    }
                }

                m_objApps.Add(objApp);
                objApp.m_ObjAppReports.Add(objReport);
            }
        }

        #endregion

        #region  �γ����뵥�����뵥Ԫ

        /// <summary>
        /// �γ����뵥�����뵥Ԫ��2��
        /// </summary>
        /// <param name="p_strApplyUnitsIDArr"></param>
        private void m_mthGenerateAppApplyUnits(string[] arrApplyUnits)
        {
            if (arrApplyUnits == null || arrApplyUnits.Length == 0)
            {
                return;
            }


            foreach (string unitId in arrApplyUnits)
            {
                clsApplUnit_VO applyUnitVO = null;
                long lngRes = clsApplyUnitSmp.s_obj.m_lngGetApplyUnitVO(unitId, out applyUnitVO);

                if (lngRes == 0 || applyUnitVO == null)
                {
                    m_objAppApplyUnits.Clear();
                    return;
                }

                clsLIS_ApplyUnit objApplyUnit = new clsLIS_ApplyUnit();
                objApplyUnit.m_ObjDataVO = applyUnitVO;

                clsT_OPR_LIS_APP_APPLY_UNIT_VO objAppAppUnitVO = new clsT_OPR_LIS_APP_APPLY_UNIT_VO();
                clsLIS_AppApplyUnit objAppApplyUnit = new clsLIS_AppApplyUnit(objAppAppUnitVO);
                objAppApplyUnit.m_ObjApplyUnit = objApplyUnit;
                objAppApplyUnit.m_StrApplyUnitID = applyUnitVO.strApplUnitID;

                m_objAppApplyUnits.Add(objAppApplyUnit);
            }
        }

        #endregion

        #region �������뵥�ı�����

        /// <summary>
        /// �������뵥�ı����顾3��
        /// </summary>
        private void m_mthGenerateAppReports()
        {
            clsAppSampleGroupCollection objSampleGroups = m_objGenerateAppSampleGroups();

            foreach (clsLIS_AppSampleGroup objAppSampleGroup in objSampleGroups)
            {
                bool blnAppReportGroupExist = false;

                clsReportGroup_VO objReportGroupVO = null;
                long lngRes = clsReportGroupSmp.s_obj.m_lngGetReportGoupVO(objAppSampleGroup.m_ObjDataVO.m_strSAMPLE_GROUP_ID_CHR, out objReportGroupVO);

                if (lngRes > 0 && objReportGroupVO != null)
                {
                    objAppSampleGroup.m_ObjDataVO.m_strREPORT_GROUP_ID_CHR = objReportGroupVO.strReportGroupID;
                    foreach (clsLIS_AppCheckItem objAppCheckItem in objAppSampleGroup.m_ObjAppCheckItems)
                    {
                        objAppCheckItem.m_ObjDataVO.m_strREPORT_GROUP_ID_CHR = objReportGroupVO.strReportGroupID;
                    }

                    foreach (clsLIS_AppCheckReport objAppCheckReport in this.m_objAppReports)
                    {
                        if (objAppCheckReport.m_ObjCheckReport.m_ObjDataVO.strReportGroupID == objReportGroupVO.strReportGroupID)
                        {
                            objAppCheckReport.m_ObjAppSampleGroups.Add(objAppSampleGroup);
                            System.Collections.ArrayList arlSGID = new System.Collections.ArrayList();
                            if (objAppCheckReport.m_ObjDataVO.m_strSampleGroupIDArr != null)
                            {
                                arlSGID.AddRange(objAppCheckReport.m_ObjDataVO.m_strSampleGroupIDArr);
                            }
                            arlSGID.Add(objAppSampleGroup.m_StrSampleGroupID);
                            objAppCheckReport.m_ObjDataVO.m_strSampleGroupIDArr = (string[])arlSGID.ToArray(typeof(string));
                            blnAppReportGroupExist = true;
                            break;
                        }
                    }
                    if (!blnAppReportGroupExist)
                    {
                        clsLIS_CheckReport objCheckReport = new clsLIS_CheckReport();
                        objCheckReport.m_ObjDataVO = objReportGroupVO;
                        clsT_OPR_LIS_APP_REPORT_VO objAppReportVO = new clsT_OPR_LIS_APP_REPORT_VO();
                        clsLIS_AppCheckReport objAppReport = new clsLIS_AppCheckReport(objAppReportVO);
                        objAppReport.m_ObjCheckReport = objCheckReport;
                        objAppReport.m_StrReportGroupID = objCheckReport.m_ObjDataVO.strReportGroupID;
                        objAppReport.m_ObjAppSampleGroups.Add(objAppSampleGroup);
                        objAppReport.m_ObjDataVO.m_strSampleGroupIDArr = new string[] { objAppSampleGroup.m_StrSampleGroupID };
                        objAppReport.m_IntStatus = 1;

                        m_objAppReports.Add(objAppReport);
                    }
                }
            }
        }

        #endregion

        #region �γ����뵥������ı걾��

        /// <summary>
        /// �γ����뵥������ı걾�顾4��
        /// </summary>
        /// <returns></returns>
        private clsAppSampleGroupCollection m_objGenerateAppSampleGroups()
        {

            m_objGenerateAppUnitItems();

            clsAppSampleGroupCollection objSampleGroups = new clsAppSampleGroupCollection();

            foreach (clsLIS_AppApplyUnit objAppUnit in m_objAppApplyUnits)
            {
                // �������Ƿ����
                bool blnAppSampleGroupExist = false;

                clsSampleGroup_VO objSampleGroupVO = null;
                long lngRes = clsSampleGroupSmp.s_obj.m_lngGetSampleGoupVO(objAppUnit.m_StrApplyUnitID, out objSampleGroupVO);

                if (lngRes > 0 && objSampleGroupVO != null)
                {
                    clsLIS_AppSampleGroup objAppSampleGroup = null;
                    foreach (clsLIS_AppSampleGroup obj in objSampleGroups)
                    {
                        if (obj.m_ObjSampleGroup.m_ObjDataVO.strSampleGroupID == objSampleGroupVO.strSampleGroupID)
                        {
                            objAppSampleGroup = obj;
                            blnAppSampleGroupExist = true;
                            break;
                        }
                    }

                    if (!blnAppSampleGroupExist)
                    {
                        clsLIS_SampleGroup objSampleGroup = new clsLIS_SampleGroup();
                        objSampleGroup.m_ObjDataVO = objSampleGroupVO;
                        clsT_OPR_LIS_APP_SAMPLE_VO objAppSampleGroupVO = new clsT_OPR_LIS_APP_SAMPLE_VO();
                        clsLIS_AppSampleGroup objAppSGroup = new clsLIS_AppSampleGroup(objAppSampleGroupVO);
                        objAppSGroup.m_ObjSampleGroup = objSampleGroup;
                        objAppSGroup.m_StrSampleGroupID = objSampleGroup.m_ObjDataVO.strSampleGroupID;
                        objSampleGroups.Add(objAppSGroup);
                        objAppSampleGroup = objAppSGroup;
                    }

                    System.Collections.ArrayList arlSU = new System.Collections.ArrayList();

                    if (objAppSampleGroup.m_ObjAppUnitArr != null)
                    {
                        arlSU.AddRange(objAppSampleGroup.m_ObjAppUnitArr);
                    }

                    arlSU.Add(objAppUnit);
                    objAppSampleGroup.m_ObjAppUnitArr = (clsLIS_AppApplyUnit[])arlSU.ToArray(typeof(clsLIS_AppApplyUnit));

                    for (int i = 0; i < objAppUnit.m_ObjItemArr.Length; i++)
                    {
                        bool blnItemExist = false;
                        foreach (clsLIS_AppCheckItem objAppItem in objAppSampleGroup.m_ObjAppCheckItems)
                        {
                            if (objAppItem.m_StrCheckItemID == objAppUnit.m_ObjItemArr[i].m_strCHECK_ITEM_ID_CHR)
                            {
                                blnItemExist = true;
                                break;
                            }
                        }
                        if (!blnItemExist)
                        {
                            clsT_OPR_LIS_APP_CHECK_ITEM_VO objAppItemVO = new clsT_OPR_LIS_APP_CHECK_ITEM_VO();
                            clsLIS_AppCheckItem objAppCheckItem = new clsLIS_AppCheckItem(objAppItemVO);

                            objAppCheckItem.m_StrCheckItemID = objAppUnit.m_ObjItemArr[i].m_strCHECK_ITEM_ID_CHR;
                            objAppCheckItem.m_StrSampleGroupID = objAppSampleGroup.m_StrSampleGroupID;
                            objAppSampleGroup.m_ObjAppCheckItems.Add(objAppCheckItem);
                        }
                    }

                }
            }
            return objSampleGroups;
        }

        #endregion

        #region �γ����뵥�ı�����ı걾��ļ�����Ŀ

        /// <summary>
        /// �γ����뵥�ı�����ı걾��ļ�����Ŀ
        /// </summary>
        private void m_objGenerateAppUnitItems()
        {
            ArrayList arlUnitItems = new ArrayList();

            foreach (clsLIS_AppApplyUnit objAppApplyUnit in this.m_objAppApplyUnits)
            {
                arlUnitItems.Clear();

                clsCheckItem_VO[] objCheckItemVOArr = null;
                long lngRet = clsCheckItemSmp.s_obj.m_lngGetCheckItem(objAppApplyUnit.m_ObjDataVO.m_strAPPLY_UNIT_ID_CHR, out objCheckItemVOArr);

                if (lngRet > 0 && objCheckItemVOArr != null && objCheckItemVOArr.Length > 0)
                {
                    for (int i = 0; i < objCheckItemVOArr.Length; i++)
                    {
                        clsLisAppUnitItemVO objUnitItem = new clsLisAppUnitItemVO();
                        objUnitItem.m_strAPPLY_UNIT_ID_CHR = objAppApplyUnit.m_StrApplyUnitID;
                        objUnitItem.m_strCHECK_ITEM_ID_CHR = objCheckItemVOArr[i].m_strCheck_Item_ID;
                        arlUnitItems.Add(objUnitItem);
                    }
                    objAppApplyUnit.m_ObjItemArr = (clsLisAppUnitItemVO[])arlUnitItems.ToArray(typeof(clsLisAppUnitItemVO));
                }

            }
        }

        #endregion

    } 

    #endregion

}
