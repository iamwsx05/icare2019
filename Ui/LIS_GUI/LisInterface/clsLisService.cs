using System;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.LIS
{
    /* ====================================
     * clsLisService
     * ��������Ľӿ�(�滻clsLisInterface)
     * סԺ�����Lis�ڲ�����
     * ==================================== */

    #region clsLisService

    internal class clsLisService
    {
        #region �ڲ���Ա

        NameValueCollection m_unitOrder = new NameValueCollection();
        ApplyUnitInfo m_unitInfo;
        Dictionary<string, string> m_orderUnitRelation = new Dictionary<string, string>();
        List<clsLISAppResults> m_arrResults = new List<clsLISAppResults>();
        Dictionary<string, string> dic3to1 = null;

        #endregion

        #region �ӿڷ���

        public bool CreateApply(clsLisApplMainVO patientInfo, clsTestApplyItme_VO[] unitItems, bool isSended, out string message)
        {
            bool result = true;
            message = string.Empty;
            dic3to1 = new Dictionary<string, string>();
            try
            {
                if (patientInfo == null)
                {
                    throw new LisCreateApplyException("������ϢΪ��!(patientInfo==null)");
                }

                if (unitItems == null || unitItems.Length == 0)
                {
                    throw new LisCreateApplyException("�ü�������û�а�����Ч�����뵥Ԫ!");
                }

                #region ��������һ����ϻ���Ŀ�ɶ�Ӧ���������

                List<string> lstTmp = new List<string>();
                List<clsTestApplyItme_VO> lstPlusExecApplyUnit = new List<clsTestApplyItme_VO>();
                DataTable dtUnit = null;
                DataTable dtTmp = null;
                clsDomainController_ApplicationManage dclApp = new clsDomainController_ApplicationManage();
                foreach (clsTestApplyItme_VO item in unitItems)
                {
                    if (patientInfo.m_strPatientType == "1")
                    {
                        if (lstTmp.IndexOf(item.m_strOrderID) < 0)
                        {
                            lstTmp.Add(item.m_strOrderID);
                            dtTmp = dclApp.GetOrderDicLisApplyUnitByOrderId(item.m_strOrderID);
                            if (dtTmp != null && dtTmp.Rows.Count > 0)
                            {
                                if (dtUnit == null) dtUnit = dtTmp.Clone();
                                dtUnit.Merge(dtTmp);
                                dtUnit.AcceptChanges();
                            }
                        }
                    }
                    else if (patientInfo.m_strPatientType == "2")
                    {
                        if (lstTmp.IndexOf(item.m_strOutpatRecipeDeID) < 0)
                        {
                            lstTmp.Add(item.m_strOutpatRecipeDeID);
                            dtTmp = dclApp.GetOrderDicLisApplyUnit(item.m_strOutpatRecipeDeID);
                            if (dtTmp != null && dtTmp.Rows.Count > 0)
                            {
                                if (dtUnit == null) dtUnit = dtTmp.Clone();
                                dtUnit.Merge(dtTmp);
                                dtUnit.AcceptChanges();
                            }
                        }
                    }
                }
                string filterExp = string.Empty;
                List<string> lstCheck1to3 = new List<string>();
                DataRow[] drr = null;
                if (dtUnit != null && dtUnit.Rows.Count > 0)
                {
                    for (int i = 0; i < unitItems.Length; i++)
                    {
                        if (patientInfo.m_strPatientType == "1")
                        {
                            filterExp = string.Format("orderId = '{0}' and mainLisApplyUnitId = '{1}' and isUsed = 0", unitItems[i].m_strOrderID, unitItems[i].m_strItemID);
                        }
                        else if (patientInfo.m_strPatientType == "2")
                        {
                            filterExp = string.Format("orderDicId = '{0}' and mainLisApplyUnitId = '{1}' and isUsed = 0", unitItems[i].m_strOutpatRecipeDeID, unitItems[i].m_strItemID);
                        }
                        drr = dtUnit.Select(filterExp);
                        if (drr != null && drr.Length > 0)
                        {
                            string mainApplyUnitId = unitItems[i].m_strItemID;
                            string plusApplyUnitId = string.Empty;
                            clsTestApplyItme_VO vo = null;
                            string key = string.Empty;
                            foreach (DataRow dr1 in drr)
                            {
                                plusApplyUnitId = dr1["plusLisApplyUnitId"].ToString();
                                if (patientInfo.m_strPatientType == "1")    // ��ֹ�磺��������1�Զ����뵥Ԫ����ɾ��ҽ����������ʱ�ظ����������뵥
                                {
                                    key = unitItems[i].m_strOrderID + "|" + plusApplyUnitId;
                                }
                                else if (patientInfo.m_strPatientType == "2")
                                {
                                    key = unitItems[i].m_strOutpatRecipeID + "|" + plusApplyUnitId;
                                }
                                if (lstCheck1to3.IndexOf(key) < 0)
                                    lstCheck1to3.Add(key);
                                else
                                    continue;

                                if (Convert.ToInt32(dr1["sortNo"]) == 1)
                                {
                                    dr1["isUsed"] = 1;
                                    unitItems[i].m_strItemID = plusApplyUnitId;
                                    unitItems[i].m_strItemName = dr1["applyUnitName"].ToString();
                                }
                                else
                                {
                                    vo = new clsTestApplyItme_VO();
                                    vo.m_decPrice = unitItems[i].m_decPrice;
                                    vo.m_decQty = unitItems[i].m_decQty;
                                    vo.m_decTolPrice = unitItems[i].m_decTolPrice;
                                    vo.m_strItemID = plusApplyUnitId;
                                    vo.m_strUsageID = unitItems[i].m_strUsageID;
                                    vo.m_strItemName = dr1["applyUnitName"].ToString();
                                    vo.m_strSpec = unitItems[i].m_strSpec;
                                    vo.m_strSampleId = unitItems[i].m_strSampleId;
                                    vo.m_strUnit = unitItems[i].m_strUnit;
                                    vo.m_strOutpatRecipeID = unitItems[i].m_strOutpatRecipeID;
                                    vo.m_strRowNo = unitItems[i].m_strRowNo;
                                    vo.m_strOprDeptID = unitItems[i].m_strOprDeptID;
                                    vo.strPartID = unitItems[i].strPartID;
                                    vo.m_strOutpatRecipeDeID = unitItems[i].m_strOutpatRecipeDeID;
                                    vo.m_strOrderID = unitItems[i].m_strOrderID;
                                    vo.m_decDiscount = unitItems[i].m_decDiscount;
                                    lstPlusExecApplyUnit.Add(vo);
                                }
                                if (dic3to1.ContainsKey(plusApplyUnitId) == false)
                                {
                                    dic3to1.Add(plusApplyUnitId, mainApplyUnitId);
                                }
                            }
                        }
                    }
                }
                #endregion

                string[] arrUnitId = GetDifferentUnits(unitItems);
                if (arrUnitId.Length == 0)
                {
                    throw new LisCreateApplyException("�ü�������û�а�����Ч�����뵥Ԫ!");
                }

                m_unitOrder = GetOrderUnitCollection(unitItems);

                m_unitInfo = new ApplyUnitInfo(arrUnitId);

                // ���»�ȡ�걾��Ϣ
                DataTable dtSampleInfo = null;

                // �ϵ��ж�.��ȡ��Ҫ�ϵ������뵥ԪID string[]   *** 2018-04-18 �ܹؼ� ***
                List<string[]> applyApplications = SeparateApplication(m_unitInfo.GetUnitIdList());
                clsTestApplyItme_VO[] items;
                // �ϵ��ж�.ѭ���������뵥Ԫ���γ�һ�����뵥    *** 2018-04-18 �ܹؼ� ***
                foreach (string[] arrUnit in applyApplications)
                {
                    //if (!string.IsNullOrEmpty(arrUnit[0]))
                    //{
                    //    dtSampleInfo = (new clsDomainController_ApplicationManage()).GetSampleInfo(arrUnit[0]);
                    //    if (dtSampleInfo != null && dtSampleInfo.Rows.Count > 0)
                    //    {
                    //        patientInfo.m_strSampleTypeID = dtSampleInfo.Rows[0]["sample_type_id_chr"].ToString();
                    //        patientInfo.m_strSampleType = dtSampleInfo.Rows[0]["sample_type_desc_vchr"].ToString();
                    //    }
                    //}
                    items = GetApplyUnitItems(arrUnit, unitItems);
                    if (CreateApplyApplication(patientInfo, items, arrUnit, isSended) == false)
                    {
                        return false;
                    }
                }

                #region ��������һ����ϻ���Ŀ�ɶ�Ӧ���������

                if (lstPlusExecApplyUnit.Count > 0)
                {
                    foreach (clsTestApplyItme_VO item in lstPlusExecApplyUnit)
                    {
                        unitItems = new clsTestApplyItme_VO[1] { item };

                        arrUnitId = GetDifferentUnits(unitItems);

                        m_unitOrder = GetOrderUnitCollection(unitItems);

                        m_unitInfo = new ApplyUnitInfo(arrUnitId);

                        applyApplications = SeparateApplication(m_unitInfo.GetUnitIdList());
                        foreach (string[] arrUnit in applyApplications)
                        {
                            //if (!string.IsNullOrEmpty(arrUnit[0]))
                            //{
                            //    dtSampleInfo = (new clsDomainController_ApplicationManage()).GetSampleInfo(arrUnit[0]);
                            //    if (dtSampleInfo != null && dtSampleInfo.Rows.Count > 0)
                            //    {
                            //        patientInfo.m_strSampleTypeID = dtSampleInfo.Rows[0]["sample_type_id_chr"].ToString();
                            //        patientInfo.m_strSampleType = dtSampleInfo.Rows[0]["sample_type_desc_vchr"].ToString();
                            //    }
                            //}
                            items = GetApplyUnitItems(arrUnit, unitItems);
                            if (CreateApplyApplication(patientInfo, items, arrUnit, isSended) == false)
                            {
                                return false;
                            }
                        }
                    }
                }
                #endregion

                return true;
            }
            catch (LisCreateApplyException ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex.Message);
                message = ex.Message;
                result = false;
            }

            return result;
        }

        #region ɾ��ҽ��
        /// <summary>
        /// ɾ��ҽ��
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool DeleteOrder(string orderId, out string message)
        {
            message = string.Empty;
            bool result = true;

            try
            {
                List<clsLisApplMainVO> lstAppMain = new List<clsLisApplMainVO>();
                long lngRes = clsLisServiceSmp.s_obj.m_lngGetApplication(orderId, out lstAppMain);

                if (lstAppMain == null || lstAppMain.Count == 0)
                {
                    // ɾ��ҽ����Ϊ��ɾ����ʿ�ɼ������뵥��
                    // �����Ҳ���������뵥��ʱ��
                    // ˵��ҽ���ǿ���ɾ����
                    message = string.Format("���Ҳ���ҽ��Id{0}��ص����뵥!", orderId);
                    return true;
                }

                List<Dictionary<string, string>> lstDic = new List<Dictionary<string, string>>();
                foreach (clsLisApplMainVO app in lstAppMain)
                {
                    if (!string.IsNullOrEmpty(app.m_strSampleID))
                    {
                        CheckSampleStatus(app.m_strSampleID);
                    }
                    Dictionary<string, string> dic = ParseOrderUnitRelation(app.m_strOrderunitrelation);
                    dic.Remove(orderId);

                    clsLisServiceSmp.s_obj.m_lngDeleteApplication(app.m_strAPPLICATION_ID);
                    lstDic.Add(dic);
                }

                int count = 0;
                foreach (Dictionary<string, string> item in lstDic)
                {
                    count += item.Count;
                }
                if (count == 0)
                {
                    return true;
                }

                clsTestApplyItme_VO[] unitItems = new clsTestApplyItme_VO[count];
                int i = 0;
                foreach (Dictionary<string, string> dic in lstDic)
                {
                    foreach (KeyValuePair<string, string> pair in dic)
                    {
                        clsTestApplyItme_VO item = new clsTestApplyItme_VO();
                        item.m_strItemID = pair.Value;
                        item.m_strOrderID = pair.Key;
                        unitItems[i] = item;

                        i++;
                    }
                }
                clsLisApplMainVO patientInfo = lstAppMain[0];
                patientInfo.m_strChargeInfo = string.Empty;
                patientInfo.m_strCheckContent = string.Empty;

                CreateApply(patientInfo, unitItems, true, out message);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex.Message);
                message = ex.Message;
                result = false;
            }
            return result;
        }
        #endregion

        private void CheckSampleStatus(string sampleId)
        {
            clsT_OPR_LIS_SAMPLE_VO sample = GetSample(sampleId);
            if (sample != null)
            {
                int status = sample.m_intSTATUS_INT;
                // 4002����:�Ƿ����������Ĳɼ��ͺ���
                // 0=������, 1=������2=�����ɼ�����������
                int intConfig = clsLisSetting.IsSkipCollectionReceive();

                switch (intConfig)
                {
                    case 0:
                        if (status == 2)
                        {
                            if (!string.IsNullOrEmpty(sample.m_strBARCODE_VCHR))
                            {
                                throw new LisCreateApplyException("������ɾ���Ѱ�����ı걾!");
                            }
                            else
                            {
                                //û������������Ĵ���
                            }
                        }
                        if (status >= 3)
                        {
                            throw new LisCreateApplyException("������ɾ���Ѿ����յı걾!");
                        }
                        break;

                    case 1:
                        if (status == 6)
                        {
                            throw new LisCreateApplyException("������ɾ���Ѿ���˵ļ��鱨���¼��");
                        }
                        break;
                    case 2:
                        if (status >= 3)
                        {
                            throw new LisCreateApplyException("������ɾ���Ѿ����յı걾!");
                        }
                        break;

                }
            }
        }

        /// <summary>
        /// ������ʽ( orderId-unitId | orderId-unitId )
        /// </summary>
        /// <param name="orderUnitRelation"></param>
        /// <returns></returns>
        private Dictionary<string, string> ParseOrderUnitRelation(string orderUnitRelation)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string[] arrUnitRelation = orderUnitRelation.Split('|');
            foreach (string unitRelatin in arrUnitRelation)
            {
                string[] temp = unitRelatin.Split('-');
                Debug.Assert(temp.Length == 2, "���� orderId-unitId ���鳤�ȿ϶�Ϊ2");   // ҽ��ID - ���뵥ԪID
                result.Add(temp[0].Trim(), temp[1].Trim());
            }
            return result;
        }

        public clsLISAppResults[] GetApplyResult()
        {
            clsLISAppResults[] arrResult = m_arrResults.ToArray();
            if (arrResult != null && arrResult.Length > 0 && dic3to1 != null && dic3to1.Keys.Count > 0)
            {
                foreach (clsLISAppResults item in arrResult)
                {
                    for (int i = item.m_ObjApplyUnitIDArr.Length - 1; i >= 0; i--)
                    {
                        if (dic3to1.ContainsKey(item.m_ObjApplyUnitIDArr[i]))
                        {
                            item.m_ObjApplyUnitIDArr[i] = dic3to1[item.m_ObjApplyUnitIDArr[i]];
                        }
                    }
                }
            }
            return arrResult;
        }

        #endregion

        #region ����һ�����뵥

        private bool CreateApplyApplication(clsLisApplMainVO patientInfo, clsTestApplyItme_VO[] unitItems, string[] strUnits, bool isSended)
        {
            InitApplyApplication(unitItems, strUnits, ref patientInfo);

            if (SaveApplyApplication(patientInfo, strUnits, isSended) <= 0)
            {
                return false;
            }

            m_arrResults.Add(GetApplicationResult(patientInfo, strUnits));

            return true;
        }

        private void InitApplyApplication(clsTestApplyItme_VO[] unitItems, string[] unitIds, ref clsLisApplMainVO patientInfo)
        {
            patientInfo.m_strAge = clsAgeConverter.m_strAgeToAge(patientInfo.m_strAge);

            //δ���͵ļ�¼
            patientInfo.m_intPStatus_int = 1;
            //������������
            patientInfo.m_intForm_int = 1;

            if (patientInfo.m_strPatientType == "1")   // ����סԺҽ����������������걾�޸�
            {
                patientInfo.m_strSampleTypeID = unitItems[0].m_strSampleId;
            }
            else
            {
                patientInfo.m_strSampleTypeID = unitItems[0].m_strSampleId;     // GetSampleTypeId(unitIds); 
            }
            patientInfo.m_strSampleType = GetSampleTypeName(patientInfo.m_strSampleTypeID);
            patientInfo.m_strOrderunitrelation = ConstructOrderUnitRelation(unitItems);
            patientInfo.m_strOrderArr = new string[unitItems.Length];
            for (int i = 0; i < patientInfo.m_strOrderArr.Length; i++)
            {
                patientInfo.m_strOrderArr[i] = (patientInfo.m_strPatientType == "2" ? unitItems[i].m_strOutpatRecipeID : unitItems[i].m_strOrderID);
            }

            if (patientInfo.m_strAppl_Dat == null)
            {
                patientInfo.m_strAppl_Dat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            //�շ���Ϣ
            patientInfo.m_strChargeInfo = GetChargeInfo(unitItems, unitIds);
            //��ȡ��������
            patientInfo.m_strCheckContent = m_unitInfo.GetCheckContent(unitIds);


            //�����΢����걾�����ȡҽ��ʵ��ѡ��ı걾����!
            bool isGermSample = unitIds != null && unitIds.Length == 1 && unitItems.Length > 0
                                && !string.IsNullOrEmpty(patientInfo.m_strSampleType)
                                && patientInfo.m_strSampleType.Trim() == "΢����걾";

            if (isGermSample)
            {
                SetGermSampleType(ref patientInfo, unitItems, unitIds);
            }
        }

        private string ConstructOrderUnitRelation(clsTestApplyItme_VO[] unitItems)
        {
            StringBuilder sb = new StringBuilder();
            string temp;
            foreach (clsTestApplyItme_VO item in unitItems)
            {
                temp = string.Format("{0}-{1} |", item.m_strOrderID, item.m_strItemID);
                sb.Append(temp);
            }

            temp = sb.ToString();
            if (temp.Length > 0)
            {
                return temp.Remove(temp.Length - 1, 1);
            }

            return temp;
        }

        private int SaveApplyApplication(clsLisApplMainVO patientInfo, string[] arrUnitIds, bool isSend)
        {
            long lngRes = 0;
            //����һ���µ����뵥
            patientInfo.m_strAPPLICATION_ID = string.Empty;

            clsT_OPR_LIS_APP_APPLY_UNIT_VO[] applyUnits;
            clsT_OPR_LIS_APP_CHECK_ITEM_VO[] checkItems;
            clsT_OPR_LIS_APP_SAMPLE_VO[] samples;
            clsT_OPR_LIS_APP_REPORT_VO[] reports;
            clsLisAppUnitItemVO[] unitItems;

            m_unitInfo.GetApplyUnitInfo(arrUnitIds, out applyUnits, out checkItems, out samples, out reports, out unitItems);

            if (patientInfo.m_strPatientType == "2")    // ����
            {
                patientInfo.m_intPStatus_int = 2;
                lngRes = clsApplicationBizSmp.s_obj.m_lngAddAppInfoWithoutReceive(patientInfo, reports, samples, checkItems, applyUnits, unitItems);   //ȡ������״̬
            }
            else
            {
                //4002����:�Ƿ����������Ĳɼ��ͺ���
                int intConfig = clsLisSetting.IsSkipCollectionReceive();
                switch (intConfig)
                {
                    case 1:
                        patientInfo.m_intPStatus_int = 2;
                        lngRes = 0;
                        lngRes = clsApplicationBizSmp.s_obj.m_lngAddApplicationInfo(patientInfo, reports, samples, checkItems, applyUnits, unitItems);
                        break;
                    case 2:   //�����ɼ�����������		 
                        patientInfo.m_intPStatus_int = 2;
                        lngRes = 0;
                        lngRes = clsApplicationBizSmp.s_obj.m_lngAddAppInfoWithoutReceive(patientInfo, reports, samples, checkItems, applyUnits, unitItems);   //ȡ������״̬
                        break;
                    default:
                        lngRes = 0;
                        // lngRes = clsApplicationBizSmp.s_obj.m_lngAddNewAppInfo(patientInfo, reports, samples, checkItems, applyUnits, unitItems);
                        lngRes = clsLisServiceSmp.s_obj.m_lngAddApplyApplication(patientInfo, isSend, reports, samples, checkItems, applyUnits, unitItems);
                        break;
                }
            }

            if (lngRes <= 0)
            {
                //throw new LisCreateApplyException("���뵥���浽���ݿ�ʧ��!");
                System.Windows.Forms.MessageBox.Show("���뵥���浽���ݿ�ʧ��");
            }
            return (int)lngRes;
        }

        #endregion

        #region �������뵥

        public clsT_OPR_LIS_SAMPLE_VO GetSample(string sampleId)
        {
            clsT_OPR_LIS_SAMPLE_VO sample;
            clsLisServiceSmp.s_obj.m_lngFindSample(sampleId, out sample);

            return sample;
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="orderId">ҽ����</param>
        /// <param name="objSample">����</param>
        public void GetLisSample(string orderId, ref clsT_OPR_LIS_SAMPLE_VO objSample)
        {
            objSample = null;
            clsLisApplMainVO patientInfo = null;
            long lngRes = clsLisServiceSmp.s_obj.m_lngGetApplVO(orderId, out patientInfo);

            if (patientInfo == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(patientInfo.m_strSampleID))
            {
                objSample = GetSample(patientInfo.m_strSampleID);
            }
        }

        #endregion

        #region ��������

        private string GetChargeInfo(clsTestApplyItme_VO[] unitItems, string[] strUnits)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (clsTestApplyItme_VO objTestVO in unitItems)
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

        private string GetSampleTypeName(string sampleTypeId)
        {
            return new ctlLISSampleTypeComboBox().m_strGetTypeName(sampleTypeId);
        }

        private string GetSampleTypeId(string[] arrAppUnitId)
        {
            string[] arrSampleTypeId = null;
            clsApplyUnitSmp.s_obj.m_lngGetSampleTypeIdList(arrAppUnitId, out arrSampleTypeId);
            if (arrSampleTypeId != null && arrSampleTypeId.Length > 0)
            {
                return arrSampleTypeId[0];
            }
            return string.Empty;
        }

        private string[] GetDifferentUnits(clsTestApplyItme_VO[] applyUnitItem)
        {
            List<string> lstUnitId = new List<string>();
            foreach (clsTestApplyItme_VO item in applyUnitItem)
            {
                if (!string.IsNullOrEmpty(item.m_strItemID))
                {
                    string unitId = item.m_strItemID;
                    if (!lstUnitId.Contains(unitId))
                    {
                        lstUnitId.Add(unitId.Trim());
                    }
                }
            }
            return lstUnitId.ToArray();
        }

        private clsTestApplyItme_VO[] GetApplyUnitItems(string[] arrUnitId, clsTestApplyItme_VO[] allItems)
        {
            List<clsTestApplyItme_VO> lstApplyItem = new List<clsTestApplyItme_VO>();
            foreach (clsTestApplyItme_VO item in allItems)
            {
                foreach (string unitId in arrUnitId)
                {
                    if (item.m_strItemID.Trim() == unitId)
                    {
                        lstApplyItem.Add(item);
                    }
                }
            }

            return lstApplyItem.ToArray();
        }

        private List<string[]> SeparateApplication(List<string[]> arrUnitIds)
        {
            List<string[]> arlUnits = new List<string[]>();
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

        private void SetGermSampleType(ref clsLisApplMainVO patientInfo, clsTestApplyItme_VO[] unitItems, string[] strUnits)
        {
            foreach (clsTestApplyItme_VO applyItem in unitItems)
            {
                if (applyItem.m_strItemID == strUnits[0])
                {
                    patientInfo.m_strSampleTypeID = applyItem.m_strUsageID;
                    patientInfo.m_strSampleType = GetSampleTypeName(patientInfo.m_strSampleTypeID);
                }
            }
        }

        private NameValueCollection GetOrderUnitCollection(clsTestApplyItme_VO[] unitItems)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            foreach (clsTestApplyItme_VO objTestVO in unitItems)
            {
                nameValueCollection.Add(objTestVO.m_strItemID, objTestVO.m_strOrderID);
            }

            return nameValueCollection;
        }

        private clsLISAppResults GetApplicationResult(clsLisApplMainVO patientInfo, string[] strUnits)
        {
            clsLISAppResults objResult = new clsLISAppResults();
            objResult.m_StrApplicationID = patientInfo.m_strAPPLICATION_ID;
            objResult.m_strSampleTypeID = patientInfo.m_strSampleTypeID;
            objResult.m_strSampleTypeName = patientInfo.m_strSampleType;
            objResult.m_StrAppCheckContentDesc = patientInfo.m_strCheckContent;

            int applyUnitCount = strUnits.Length;
            objResult.m_ObjApplyUnitIDArr = new string[applyUnitCount];

            List<string> lstOrderId = new List<string>();
            for (int i = 0; i < applyUnitCount; i++)
            {
                objResult.m_ObjApplyUnitIDArr[i] = strUnits[i];
                string[] arrOrder = m_unitOrder.GetValues(strUnits[i]);
                if (arrOrder != null && arrOrder.Length > 0)
                {
                    lstOrderId.AddRange(arrOrder);
                }
            }

            objResult.m_arrOrderId = lstOrderId.ToArray();
            return objResult;
        }

        #endregion
    }

    #endregion

    #region ApplyUnitInfo

    public class ApplyUnitInfo
    {
        #region ˽�г�Ա

        string[] unitIds;
        Dictionary<string, ApplyUnitDetail> m_dicApplyUnitDetail = new Dictionary<string, ApplyUnitDetail>();

        #endregion

        public ApplyUnitInfo(string[] arrUnitId)
        {
            this.unitIds = arrUnitId;

            foreach (string unitId in this.unitIds)
            {
                AddUnitDetail(unitId);
            }
        }

        /// <summary>
        /// ����������ͽ����뵥Ԫ����
        /// </summary>
        /// <remarks>
        /// ��ͬ��ʽ�ı����ܷ�����ͬ���뵥��
        /// </remarks>
        /// <returns></returns>
        public List<string[]> GetUnitIdList()
        {
            List<string[]> lstUnitId = new List<string[]>();

            Dictionary<string, List<string>> lstTemp = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, ApplyUnitDetail> detail in m_dicApplyUnitDetail)
            {
                string reportId = detail.Value.ReportGroup.strReportGroupID;
                if (!lstTemp.ContainsKey(reportId))
                {
                    List<string> temp = new List<string>();
                    temp.Add(detail.Value.UnitId);
                    lstTemp.Add(reportId, temp);
                }
                else
                {
                    lstTemp[reportId].Add(detail.Value.UnitId);
                }
            }

            foreach (KeyValuePair<string, List<string>> pair in lstTemp)
            {
                lstUnitId.Add(pair.Value.ToArray());
            }

            return lstUnitId;
        }

        public string GetCheckContent(string[] arrUnitId)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string unitId in arrUnitId)
            {
                string unitName = m_dicApplyUnitDetail[unitId].ApplyUnit.strApplUnitName;
                if (!string.IsNullOrEmpty(unitName))
                {
                    sb.Append(unitName + ",");
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }



        public void GetApplyUnitInfo(string[] arrUnitId,
                                      out clsT_OPR_LIS_APP_APPLY_UNIT_VO[] arrUnitRelation, out clsT_OPR_LIS_APP_CHECK_ITEM_VO[] arrItems,
                                      out clsT_OPR_LIS_APP_SAMPLE_VO[] arrSamples, out clsT_OPR_LIS_APP_REPORT_VO[] arrReports,
                                      out clsLisAppUnitItemVO[] arrUnitItem)
        {
            List<clsT_OPR_LIS_APP_APPLY_UNIT_VO> lstUnitRelation = new List<clsT_OPR_LIS_APP_APPLY_UNIT_VO>();
            List<clsT_OPR_LIS_APP_CHECK_ITEM_VO> lstItems = new List<clsT_OPR_LIS_APP_CHECK_ITEM_VO>();
            List<clsT_OPR_LIS_APP_SAMPLE_VO> lstSamples = new List<clsT_OPR_LIS_APP_SAMPLE_VO>();
            List<clsT_OPR_LIS_APP_REPORT_VO> lstReports = new List<clsT_OPR_LIS_APP_REPORT_VO>();
            List<clsLisAppUnitItemVO> lstUnitItem = new List<clsLisAppUnitItemVO>();

            foreach (string unitId in arrUnitId)
            {
                AddUnitDetail(unitId);

                AddUnitRelation(ref lstUnitRelation, unitId);
                AddCheckItems(ref lstItems, unitId);
                AddSampleGroup(ref lstSamples, unitId);
                AddReport(ref lstReports, unitId);
                AddUnitItems(ref lstUnitItem, unitId);
            }

            arrUnitRelation = lstUnitRelation.ToArray();
            arrItems = lstItems.ToArray();
            arrSamples = lstSamples.ToArray();
            arrReports = lstReports.ToArray();
            arrUnitItem = lstUnitItem.ToArray();
        }

        private void AddUnitDetail(string unitId)
        {
            if (!m_dicApplyUnitDetail.ContainsKey(unitId))
            {
                m_dicApplyUnitDetail.Add(unitId, ApplyUnitDetail.GetApplyUnitDetail(unitId));
            }
        }

        private void AddUnitRelation(ref List<clsT_OPR_LIS_APP_APPLY_UNIT_VO> arrApplyUnit, string unitId)
        {
            clsT_OPR_LIS_APP_APPLY_UNIT_VO applyUnit = new clsT_OPR_LIS_APP_APPLY_UNIT_VO();
            applyUnit.m_strAPPLY_UNIT_ID_CHR = unitId;
            arrApplyUnit.Add(applyUnit);
        }

        private void AddCheckItems(ref List<clsT_OPR_LIS_APP_CHECK_ITEM_VO> lstItems, string unitId)
        {

            ApplyUnitDetail detail = m_dicApplyUnitDetail[unitId];
            clsCheckItem_VO[] arrCheckItem = detail.Items;
            for (int i = 0; i < arrCheckItem.Length; i++)
            {
                clsT_OPR_LIS_APP_CHECK_ITEM_VO item = new clsT_OPR_LIS_APP_CHECK_ITEM_VO();
                item.m_strCHECK_ITEM_ID_CHR = arrCheckItem[i].m_strCheck_Item_ID;
                item.m_strSAMPLE_GROUP_ID_CHR = detail.SampleGroup.strSampleGroupID;
                item.m_strREPORT_GROUP_ID_CHR = detail.ReportGroup.strReportGroupID;
                item.m_strItemprice_mny = arrCheckItem[i].m_strItemprice_mny;

                lstItems.Add(item);
            }
        }

        private void AddSampleGroup(ref List<clsT_OPR_LIS_APP_SAMPLE_VO> lstSamples, string unitId)
        {
            ApplyUnitDetail detail = m_dicApplyUnitDetail[unitId];
            clsSampleGroup_VO sampleGroup = detail.SampleGroup;
            string sampleGroupId = sampleGroup.strSampleGroupID;

            clsT_OPR_LIS_APP_SAMPLE_VO sampleGroupVO = new clsT_OPR_LIS_APP_SAMPLE_VO();
            sampleGroupVO.m_strSAMPLE_GROUP_ID_CHR = sampleGroupId;

            bool isExist = false;
            foreach (clsT_OPR_LIS_APP_SAMPLE_VO sample in lstSamples)
            {
                if (sample.m_strSAMPLE_GROUP_ID_CHR == sampleGroupId)
                {
                    isExist = true;
                    break;
                }
            }

            if (!isExist)
            {
                sampleGroupVO.m_strREPORT_GROUP_ID_CHR = detail.ReportGroup.strReportGroupID;
                lstSamples.Add(sampleGroupVO);
            }
        }

        private void AddReport(ref List<clsT_OPR_LIS_APP_REPORT_VO> lstReports, string unitId)
        {
            ApplyUnitDetail detail = m_dicApplyUnitDetail[unitId];
            clsReportGroup_VO objReportGroupVO = detail.ReportGroup;

            bool isExist = false;
            string reportGroupId = objReportGroupVO.strReportGroupID;

            foreach (clsT_OPR_LIS_APP_REPORT_VO report in lstReports)
            {
                if (report.m_strREPORT_GROUP_ID_CHR == reportGroupId)
                {
                    isExist = true;
                    break;
                }
            }

            if (!isExist)
            {
                clsT_OPR_LIS_APP_REPORT_VO reportVO = new clsT_OPR_LIS_APP_REPORT_VO();
                reportVO.m_strREPORT_GROUP_ID_CHR = reportGroupId;
                //��ʼ��״̬
                reportVO.m_intSTATUS_INT = 1;
                lstReports.Add(reportVO);
            }
        }

        private void AddUnitItems(ref List<clsLisAppUnitItemVO> lstUnitItem, string unitId)
        {
            ApplyUnitDetail detail = m_dicApplyUnitDetail[unitId];

            clsCheckItem_VO[] arrCheckItem = detail.Items;
            for (int i = 0; i < arrCheckItem.Length; i++)
            {
                clsLisAppUnitItemVO item = new clsLisAppUnitItemVO();
                item.m_strAPPLY_UNIT_ID_CHR = unitId;
                item.m_strCHECK_ITEM_ID_CHR = arrCheckItem[i].m_strCheck_Item_ID;
                lstUnitItem.Add(item);
            }
        }
    }

    #endregion

    #region LisCreateApplyException

    public class LisCreateApplyException : Exception
    {
        string message;

        public LisCreateApplyException()
        {
            message = string.Empty;
        }

        public LisCreateApplyException(string message)
        {
            this.message = message;
        }

        public LisCreateApplyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public LisCreateApplyException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string Message
        {
            get
            {
                return message;
            }
        }
    }

    #endregion

    #region ApplyUnitDetail

    /// <summary>
    /// ���뵥Ԫ����ϸ��Ϣ
    /// </summary>
    public class ApplyUnitDetail
    {
        public ApplyUnitDetail(string unitId, clsApplUnit_VO applyUnit, clsSampleGroup_VO sampleGroup, clsReportGroup_VO reportGroup, clsCheckItem_VO[] items)
        {
            this.unitId = unitId;
            this.applyUnit = applyUnit;
            this.sampleGroup = sampleGroup;
            this.reportGroup = reportGroup;
            this.items = items;
        }



        string unitId;
        clsApplUnit_VO applyUnit;
        clsSampleGroup_VO sampleGroup;
        clsReportGroup_VO reportGroup;
        clsCheckItem_VO[] items;

        public string UnitId
        {
            get { return unitId; }
        }
        public clsApplUnit_VO ApplyUnit
        {
            get { return applyUnit; }
        }
        public clsSampleGroup_VO SampleGroup
        {
            get { return sampleGroup; }
        }
        public clsReportGroup_VO ReportGroup
        {
            get { return reportGroup; }
        }
        public clsCheckItem_VO[] Items
        {
            get { return items; }
        }

        public static ApplyUnitDetail GetApplyUnitDetail(string unitId)
        {
            // new clsLogText().LogError(string.Format("���뵥ԪIDΪ({0})!",unitId));

            long lngRes = 0;
            string message;

            clsApplUnit_VO applyUnitVO = null;
            lngRes = clsApplyUnitSmp.s_obj.m_lngGetApplyUnitVO(unitId, out applyUnitVO);
            if (lngRes == 0 || applyUnitVO == null)
            {
                message = string.Format("���뵥ԪIDΪ({0})�����뵥Ԫ�����ڻ���Ϊ��!", unitId);
                throw new LisCreateApplyException(message);
            }

            string unitName = applyUnitVO.strApplUnitName;

            clsSampleGroup_VO sampleGroup = null;
            lngRes = clsSampleGroupSmp.s_obj.m_lngGetSampleGoupVO(unitId, out sampleGroup);
            if (lngRes == 0 || sampleGroup == null)
            {
                message = string.Format("���뵥ԪIDΪ({0})({1})�µı걾�鲻���ڻ���Ϊ��!", unitId, unitName);
                throw new LisCreateApplyException(message);
            }

            clsCheckItem_VO[] items = null;
            lngRes = clsCheckItemSmp.s_obj.m_lngGetCheckItem(unitId, out items);
            if (lngRes == 0 || items == null)
            {
                message = string.Format("���뵥ԪIDΪ({0})({1})�µļ�����Ŀ�����ڻ���Ϊ��!", unitId, unitName);
                throw new LisCreateApplyException(message);
            }

            clsReportGroup_VO reportGroup = null;
            lngRes = clsReportGroupSmp.s_obj.m_lngGetReportGoupVO(sampleGroup.strSampleGroupID, out reportGroup);
            if (lngRes == 0 || reportGroup == null)
            {
                message = string.Format("���뵥ԪIDΪ({0})({1})�µı����鲻���ڻ���Ϊ��!", unitId, unitName);
                throw new LisCreateApplyException(message);
            }

            return new ApplyUnitDetail(unitId, applyUnitVO, sampleGroup, reportGroup, items);
        }
    }

    #endregion
}
