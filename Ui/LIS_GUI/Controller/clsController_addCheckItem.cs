using System;
using System.Data;
using com.digitalwave.iCare.common;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// Summary description for clsController_addCheckItem.
    /// </summary>
    public class clsController_addCheckItem : com.digitalwave.GUI_Base.clsController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        com.digitalwave.iCare.gui.LIS.frmAddCheckItem m_objViewer;
        com.digitalwave.iCare.gui.LIS.clsDomainController_CheckItemManage m_objManage;

        #region ���ô������
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmAddCheckItem)frmMDI_Child_Base_in;
        }
        #endregion

        public clsController_addCheckItem()
        {
            //
            // TODO: Add constructor logic here
            //
            m_objManage = new clsDomainController_CheckItemManage();
        }

        #region ˢ��
        public void m_mthrefreshCheckItemList()
        {
            long lngRes = 0;
            string strCheckCategory = "";
            string strSampleType = "";
            string strSampleGroup = "";
            #region ��ȡ��������
            strCheckCategory = m_objViewer.cboCheckCategory.SelectedValue.ToString().Trim();
            strSampleType = m_objViewer.cboSampleType.SelectedValue.ToString().Trim();
            //			if(m_objViewer.m_chkAppointSampleGroup.Checked)
            //			{
            if (m_objViewer.m_cboSampleGroup.Items.Count > 0)
            {
                strSampleGroup = m_objViewer.m_cboSampleGroup.SelectedValue.ToString().Trim();
            }
            //				m_objViewer.m_cboSampleGroup.m_mthShowStateByCategoryAndType(strCheckCategory,strSampleType);
            //			}
            #endregion
            DataTable dtbAllCheckItem = new DataTable();
            m_objViewer.lsvCheckItemDetail.Items.Clear();
            lngRes = m_objManage.m_lngQryCheckItemByCheckCategoryAndSampleType(strCheckCategory, strSampleType, strSampleGroup, out dtbAllCheckItem);
            int count = dtbAllCheckItem.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    ListViewItem objLsvItem = new ListViewItem();
                    objLsvItem.Text = dtbAllCheckItem.Rows[i]["CHECK_ITEM_NAME_VCHR"].ToString().Trim();
                    objLsvItem.SubItems.Add(dtbAllCheckItem.Rows[i]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim());
                    //					objLsvItem.SubItems.Add(dtbAllCheckItem.Rows[i]["SHORTNAME_CHR"].ToString().Trim());
                    //					objLsvItem.SubItems.Add(dtbAllCheckItem.Rows[i]["CHECK_CATEGORY_DESC_VCHR"].ToString().Trim());
                    //					if(dtbAllCheckItem.Rows[i]["IS_NO_FOOD_REQUIRED_CHR"].ToString().Trim() == "1")
                    //					{
                    //						objLsvItem.SubItems.Add("��Ҫ");
                    //					}
                    //					else
                    //					{
                    //						objLsvItem.SubItems.Add("����Ҫ");
                    //					}
                    //					if(dtbAllCheckItem.Rows[i]["IS_PHYSICAL_EXAM_REQUIRED_CHR"].ToString().Trim() == "1")
                    //					{
                    //						objLsvItem.SubItems.Add("��Ҫ");
                    //					}
                    //					else
                    //					{
                    //						objLsvItem.SubItems.Add("����Ҫ");
                    //					}
                    //					if(dtbAllCheckItem.Rows[i]["IS_RESERVATION_REQUIRED_CHR"].ToString().Trim() == "1")
                    //					{
                    //						objLsvItem.SubItems.Add("��Ҫ");
                    //					}
                    //					else
                    //					{
                    //						objLsvItem.SubItems.Add("����Ҫ");
                    //					}
                    //					if(dtbAllCheckItem.Rows[i]["IS_QC_REQUIRED_CHR"].ToString().Trim() == "1")
                    //					{
                    //						objLsvItem.SubItems.Add("��Ҫ");
                    //					}
                    //					else
                    //					{
                    //						objLsvItem.SubItems.Add("����Ҫ");
                    //					}
                    objLsvItem.Tag = dtbAllCheckItem.Rows[i];
                    m_objViewer.lsvCheckItemDetail.Items.Add(objLsvItem);
                }
            }
        }
        #endregion

        #region ���
        public void m_mthReset()
        {
            bool blnTemp = m_objViewer.m_blnInit;
            m_objViewer.m_blnInit = true;
            m_objViewer.txtCheckItemNameCN.Text = "";
            m_objViewer.txtEnglistName.Text = "";
            m_objViewer.txtShortName.Text = "";
            m_objViewer.txtPYAss.Text = "";
            m_objViewer.txtReportNO.Text = "";
            m_objViewer.txtUnit.Text = "";
            m_objViewer.txtWBAss.Text = "";
            m_objViewer.txtFormula.Text = "";
            m_objViewer.txtCheckMethod.Text = "";
            m_objViewer.txtSampleValidateTime.Text = "";
            m_objViewer.txtClinicMeaning.Text = "";
            m_objViewer.txtRef.Text = "";
            m_objViewer.lsvSampleRef.Items.Clear();
            m_objViewer.txtFromAge.Text = "";
            m_objViewer.txtToAge.Text = "";
            m_objViewer.txtFromRef.Text = "";
            m_objViewer.txtToRef.Text = "";
            m_objViewer.txtRef.Text = "";
            m_objViewer.btnSave.Text = "����";
            m_objViewer.intSEQ = 1;

            m_objViewer.cboSex.SelectedIndex = 0;
            m_objViewer.cboResultType.SelectedIndex = 0;

            m_objViewer.m_txtAssistCodeOne.Text = "";
            m_objViewer.m_txtAssistCodeTwo.Text = "";
            m_objViewer.m_txtDefaultRefMax.Text = "";
            m_objViewer.m_txtDefaultRefMin.Text = "";
            m_objViewer.m_txtDefaultRefRange.Text = "";
            m_objViewer.m_cboBeginAgeUnit.SelectedIndex = 0;
            m_objViewer.m_cboEndAgeUnit.SelectedIndex = 0;
            m_objViewer.m_chkSexRelation.Checked = false;
            m_objViewer.m_chkAgeRelation.Checked = false;
            m_objViewer.m_chkMedUsedTimeRelation.Checked = false;
            m_objViewer.m_chkMensesRelation.Checked = false;

            m_objViewer.m_strFormula = "";
            m_objViewer.m_strUserFormula = "";

            m_objViewer.m_txtAlarmVal.Clear();
            m_objViewer.m_txtAlarmValMax.Clear();
            m_objViewer.m_txtAlarmValMin.Clear();

            m_objViewer.txtCrVal1.Clear();
            m_objViewer.txtCrVal2.Clear();
            m_objViewer.lstDepts.Items.Clear();

            m_objViewer.m_blnInit = blnTemp;
        }
        #endregion

        #region ɾ��������Ŀ
        public void m_mthDelCheckItem()
        {
            if (m_objViewer.lsvCheckItemDetail.SelectedItems.Count <= 0)
                return;
            DialogResult dlgRes = MessageBox.Show("ȷ��ɾ���ü�¼��", "ɾ��", MessageBoxButtons.YesNo);
            if (dlgRes == DialogResult.No)
            {
                return;
            }
            long lngRes = 0;
            string strCheckItemID = ((DataRow)m_objViewer.lsvCheckItemDetail.SelectedItems[0].Tag)["CHECK_ITEM_ID_CHR"].ToString().Trim();
            lngRes = m_objManage.m_lngDelCheckItemAndRef(strCheckItemID);
            if (lngRes > 0)
            {
                m_mthrefreshCheckItemList();
                m_mthReset();
            }
        }
        #endregion

        #region �޸Ĳο�ֵ
        public void m_mthSetCheckItemBaseInfo()
        {
            if (m_objViewer.lsvCheckItemDetail.SelectedItems.Count <= 0)
                return;
            long lngRes = 0;
            int intIdx = m_objViewer.lsvCheckItemDetail.SelectedItems[0].Index;

            #region ��ȡ��������
            clsCheckItem_VO objCheckItemVO = new clsCheckItem_VO();
            objCheckItemVO.m_strRPTNO = m_objViewer.txtReportNO.Text.ToString().Trim();
            objCheckItemVO.m_strPycode = m_objViewer.txtPYAss.Text.ToString().Trim();
            objCheckItemVO.m_strUnit = m_objViewer.txtUnit.Text.ToString().Trim();
            objCheckItemVO.m_strCheck_Item_Name = m_objViewer.txtCheckItemNameCN.Text.ToString().Trim();
            objCheckItemVO.m_strCheck_Item_English_Name = m_objViewer.txtEnglistName.Text.ToString().Trim();
            objCheckItemVO.m_strFormula = m_objViewer.m_strFormula;
            objCheckItemVO.m_strTest_Method = m_objViewer.txtCheckMethod.Text.ToString().Trim();
            objCheckItemVO.m_strClinic_meaning = m_objViewer.txtClinicMeaning.Text.ToString().Trim();
            objCheckItemVO.m_strCheck_Item_ID = ((DataRow)m_objViewer.lsvCheckItemDetail.SelectedItems[0].Tag)["CHECK_ITEM_ID_CHR"].ToString().Trim(); ;
            objCheckItemVO.m_strShortName = m_objViewer.txtShortName.Text.ToString().Trim();
            //			objCheckItemVO.m_strIs_Qc_Required = strIsQC;
            objCheckItemVO.m_strResultType = m_objViewer.cboResultType.SelectedIndex.ToString().Trim();
            objCheckItemVO.m_strRef_Value_Range = m_objViewer.m_txtDefaultRefRange.Text.ToString().Trim();
            objCheckItemVO.m_strWbcode = m_objViewer.txtWBAss.Text.ToString().Trim();
            //			objCheckItemVO.m_strIs_No_Food_Required = strIsNoFood;
            //			objCheckItemVO.m_strIs_Physical_Exam_Required = strIsPhysicalExam;
            //			objCheckItemVO.m_strIs_Reservation_Required = strIsReservation;
            //			objCheckItemVO.m_strSample_Valid_Time = m_objViewer;
            //			objCheckItemVO.m_strSample_Valid_Time_Unit = strSampleValidTimeUnit;
            objCheckItemVO.m_strCheck_Category_ID = m_objViewer.cboCheckCategory.SelectedValue.ToString().Trim();
            objCheckItemVO.m_strAssist_Code01 = m_objViewer.m_txtAssistCodeOne.Text.ToString().Trim();
            objCheckItemVO.m_strAssist_Code02 = m_objViewer.m_txtAssistCodeTwo.Text.ToString().Trim();
            objCheckItemVO.m_strRef_Value_Min = m_objViewer.m_txtDefaultRefMin.Text.ToString().Trim();
            objCheckItemVO.m_strRef_Value_Max = m_objViewer.m_txtDefaultRefMax.Text.ToString().Trim();
            objCheckItemVO.m_strSampleTypeID = m_objViewer.cboSampleType.SelectedValue.ToString().Trim();
            objCheckItemVO.m_strALARM_LOW_VAL_VCHR = m_objViewer.m_txtAlarmValMin.Text.ToString().Trim();
            objCheckItemVO.m_strALARM_UP_VAL_VCHR = m_objViewer.m_txtAlarmValMax.Text.ToString().Trim();
            objCheckItemVO.m_strALERT_VALUE_RANGE_VCHR = m_objViewer.m_txtAlarmVal.Text.ToString().Trim();
            if (m_objViewer.m_chkSexRelation.Checked)
            {
                objCheckItemVO.m_strIs_Sex_Related = "1";
            }
            else
            {
                objCheckItemVO.m_strIs_Sex_Related = "0";
            }
            if (m_objViewer.m_chkMensesRelation.Checked)
            {
                objCheckItemVO.m_strIs_Menses_Related = "1";
            }
            else
            {
                objCheckItemVO.m_strIs_Menses_Related = "0";
            }
            if (m_objViewer.m_chkAgeRelation.Checked)
            {
                objCheckItemVO.m_strIs_Age_Related = "1";
            }
            else
            {
                objCheckItemVO.m_strIs_Age_Related = "0";
            }
            if (m_objViewer.chkIsCalculated.Checked)
            {
                objCheckItemVO.m_strIs_Calculated = "1";
            }
            else
            {
                objCheckItemVO.m_strIs_Calculated = "0";
            }
            objCheckItemVO.m_strFormula_User_VCHR = m_objViewer.m_strUserFormula;
            objCheckItemVO.m_strItemprice_mny = m_objViewer.m_txtItemPrice.Text;
            #endregion

            #region ��ȡ�ο�ֵ
            clsCheckItemRef_VO[] objCheckItemRefArr = null;
            if (m_objViewer.lsvSampleRef.Items.Count > 0)
            {
                objCheckItemRefArr = new clsCheckItemRef_VO[m_objViewer.lsvSampleRef.Items.Count];
                for (int i = 0; i < m_objViewer.lsvSampleRef.Items.Count; i++)
                {
                    ListViewItem objLsvItemRef = m_objViewer.lsvSampleRef.Items[i];
                    string strSEQ = objLsvItemRef.SubItems[0].Text.ToString().Trim();
                    string strSex = objLsvItemRef.SubItems[1].Text.ToString().Trim();
                    string strFromAge = objLsvItemRef.SubItems[2].Text.ToString().Trim();
                    string strToAge = objLsvItemRef.SubItems[3].Text.ToString().Trim();
                    string strMenses = objLsvItemRef.Tag.ToString().Trim();
                    string strMedUseTime = objLsvItemRef.SubItems[5].Text.ToString().Trim();
                    string strMinVal = objLsvItemRef.SubItems[6].Text.ToString().Trim();
                    string strMaxVal = objLsvItemRef.SubItems[7].Text.ToString().Trim();
                    string strRefVal = objLsvItemRef.SubItems[8].Text.ToString().Trim();
                    if (m_objViewer.m_chkAgeRelation.Checked)
                    {
                        if (strFromAge == "")
                        {
                            MessageBox.Show("��������������!", "�ο�ֵ", MessageBoxButtons.OK);
                            return;
                        }
                        if (strFromAge == "")
                        {
                            MessageBox.Show("��������������!", "�ο�ֵ", MessageBoxButtons.OK);
                            return;
                        }
                    }
                    if (m_objViewer.m_chkMedUsedTimeRelation.Checked)
                    {
                        if (strMedUseTime == "")
                        {
                            MessageBox.Show("��������ҩʱ��!", "�ο�ֵ", MessageBoxButtons.OK);
                            return;
                        }
                    }
                    if (m_objViewer.m_chkSexRelation.Checked)
                    {
                        if (strSex == "")
                        {
                            MessageBox.Show("�������Ա�!", "�ο�ֵ", MessageBoxButtons.OK); ;
                            return;
                        }
                    }
                    if (m_objViewer.m_chkMensesRelation.Checked)
                    {
                        if (objLsvItemRef.SubItems[4].Text.ToString().Trim() == "")
                        {
                            MessageBox.Show("�������¾���!", "�ο�ֵ", MessageBoxButtons.OK);
                            return;
                        }
                    }

                    objCheckItemRefArr[i] = new clsCheckItemRef_VO();
                    objCheckItemRefArr[i].m_strCheck_Item_ID = objCheckItemVO.m_strCheck_Item_ID;
                    objCheckItemRefArr[i].m_strSeq_Int = i.ToString().Trim();
                    objCheckItemRefArr[i].m_strSex = strSex;
                    objCheckItemRefArr[i].m_strFrom_Age = strFromAge;
                    objCheckItemRefArr[i].m_strTo_Age = strToAge;
                    objCheckItemRefArr[i].m_strMenses_ID = strMenses;
                    objCheckItemRefArr[i].m_strRef_Val = strRefVal;
                    objCheckItemRefArr[i].m_strMax_Val = strMaxVal;
                    objCheckItemRefArr[i].m_strMin_Val = strMinVal;
                    objCheckItemRefArr[i].CrValMin = objLsvItemRef.SubItems[9].Text;
                    objCheckItemRefArr[i].CrValMax = objLsvItemRef.SubItems[10].Text;
                    objCheckItemRefArr[i].DeptNameArr = objLsvItemRef.SubItems[11].Text;
                }
            }
            #endregion

            lngRes = m_objManage.m_lngSetCheckItemAndRef(objCheckItemVO, objCheckItemRefArr);
            if (lngRes > 0)
            {
                m_mthrefreshCheckItemList();
                m_objViewer.lsvCheckItemDetail.Focus();
                m_objViewer.lsvCheckItemDetail.Items[intIdx].Selected = true;
            }
        }
        #endregion

        #region �޸Ĳο�ֵ��Χ ͯ�� 2004.06.08
        public long m_lngSetCheckItemRef(frmAddCheckItem infrmAddCheckItem)
        {
            long lngRes = 0;

            string strCheckItemID = ((DataRow)infrmAddCheckItem.lsvCheckItemDetail.SelectedItems[0].Tag)["CHECK_ITEM_ID_CHR"].ToString().Trim();
            clsDomainController_CheckItemManage objDomainCheckItemManage = new clsDomainController_CheckItemManage();
            lngRes = objDomainCheckItemManage.m_lngDelCheckItemRefByCheckItemID(strCheckItemID);
            if (lngRes > 0)
            {
                lngRes = m_lngAddCheckItemRef(infrmAddCheckItem, strCheckItemID);
            }
            return lngRes;
        }
        #endregion

        #region ���������Ŀ���ο���Χ ͯ�� 2004.06.08
        public long m_lngAddCheckItemAndItemRef(frmAddCheckItem infrmAddCheckItem)
        {
            long lngRes = 0;
            string strCheckItemID = "";
            //����������Ŀ
            lngRes = m_lngAddCheckItem(infrmAddCheckItem, out strCheckItemID);
            if (lngRes > 0)
            {
                if (infrmAddCheckItem.lsvSampleRef.Items.Count > 0)
                {
                    lngRes = m_lngAddCheckItemRef(infrmAddCheckItem, strCheckItemID);
                }
            }

            if (lngRes > 0)
            {
                infrmAddCheckItem.lsvCheckItemDetail.Items.Clear();
                m_mthrefreshCheckItemList();
                infrmAddCheckItem.btnSaveRef.Text = "����";
                for (int i = 0; i < infrmAddCheckItem.lsvCheckItemDetail.Items.Count; i++)
                {
                    if (((DataRow)infrmAddCheckItem.lsvCheckItemDetail.Items[i].Tag)["CHECK_ITEM_ID_CHR"].ToString().Trim() == strCheckItemID)
                    {
                        infrmAddCheckItem.lsvCheckItemDetail.Focus();
                        infrmAddCheckItem.lsvCheckItemDetail.Items[i].Selected = true;
                        break;
                    }
                }
            }
            return lngRes;
        }
        #endregion

        #region ����������Ŀ ͯ�� 2004.06.08
        public long m_lngAddCheckItem(frmAddCheckItem infrmAddCheckItem, out string strCheckItemID)
        {
            long lngRes = 0;
            strCheckItemID = "";
            #region �������
            string strRptNO = infrmAddCheckItem.txtReportNO.Text.ToString().Trim();
            string strPycode = infrmAddCheckItem.txtPYAss.Text.ToString().Trim();
            string strUnit = infrmAddCheckItem.txtUnit.Text.ToString().Trim();
            string strCheckItemName = infrmAddCheckItem.txtCheckItemNameCN.Text.ToString().Trim();
            string strCheckItemEnglishName = infrmAddCheckItem.txtEnglistName.Text.ToString().Trim();
            string strFormula = infrmAddCheckItem.txtFormula.Text.ToString().Trim();
            string strTestMethod = infrmAddCheckItem.txtCheckMethod.Text.ToString().Trim();
            string strClinicMeaning = infrmAddCheckItem.txtClinicMeaning.Text.ToString().Trim();
            string strShortName = infrmAddCheckItem.txtShortName.Text.ToString().Trim();
            string strWBCode = infrmAddCheckItem.txtWBAss.Text.ToString().Trim();
            string strSampleValidTime = infrmAddCheckItem.txtSampleValidateTime.Text.ToString().Trim();
            string strSampleValidTimeUnit = infrmAddCheckItem.cboSampleValidateTimeUnit.Text.ToString().Trim();
            string strCheckCategoryID = infrmAddCheckItem.cboCheckCategory.SelectedValue.ToString().Trim();
            string strResultType = infrmAddCheckItem.cboResultType.SelectedIndex.ToString().Trim();
            string strSampleType = infrmAddCheckItem.cboSampleType.SelectedValue.ToString().Trim();
            string strAssistCode1 = infrmAddCheckItem.m_txtAssistCodeOne.Text.ToString().Trim();
            string strAssistCode2 = infrmAddCheckItem.m_txtAssistCodeTwo.Text.ToString().Trim();
            string strRefValue = infrmAddCheckItem.m_txtDefaultRefRange.Text.ToString().Trim();
            string strDefaultMax = infrmAddCheckItem.m_txtDefaultRefMax.Text.ToString().Trim();
            string strDefaultMin = infrmAddCheckItem.m_txtDefaultRefMin.Text.ToString().Trim();
            string strIsCalculate = "";
            string strIsQC = "";
            string strIsNoFood = "";
            string strIsPhysicalExam = "";
            string strIsReservation = "";
            string strIsAgeRelated = "";
            string strIsSexRelated = "";
            string strIsMensesRelated = "";
            if (infrmAddCheckItem.chkIsCalculated.Checked)
            {
                strIsCalculate = "1";
            }
            else
            {
                strIsCalculate = "0";
            }
            if (infrmAddCheckItem.chkQC.Checked)
            {
                strIsQC = "1";
            }
            else
            {
                strIsQC = "0";
            }
            if (infrmAddCheckItem.chkNoFood.Checked)
            {
                strIsNoFood = "1";
            }
            else
            {
                strIsNoFood = "0";
            }
            if (infrmAddCheckItem.chkPhysicalExam.Checked)
            {
                strIsPhysicalExam = "1";
            }
            else
            {
                strIsPhysicalExam = "0";
            }
            if (infrmAddCheckItem.chkReservation.Checked)
            {
                strIsReservation = "1";
            }
            else
            {
                strIsReservation = "0";
            }
            if (infrmAddCheckItem.m_chkSexRelation.Checked)
            {
                strIsSexRelated = "1";
                if (infrmAddCheckItem.lsvSampleRef.Items.Count == 0)
                {
                    MessageBox.Show("�����������Ŀ�ο�ֵ��Χ", "�ο�ֵ", MessageBoxButtons.OK);
                    return -1;
                }
            }
            else
            {
                strIsSexRelated = "0";
            }
            if (infrmAddCheckItem.m_chkAgeRelation.Checked)
            {
                strIsAgeRelated = "1";
                if (infrmAddCheckItem.lsvSampleRef.Items.Count == 0)
                {
                    MessageBox.Show("�����������Ŀ�ο�ֵ��Χ", "�ο�ֵ", MessageBoxButtons.OK);
                    return -1;
                }
            }
            else
            {
                strIsAgeRelated = "0";
            }
            if (infrmAddCheckItem.m_chkMensesRelation.Checked)
            {
                strIsMensesRelated = "1";
                if (infrmAddCheckItem.lsvSampleRef.Items.Count == 0)
                {
                    MessageBox.Show("�����������Ŀ�ο�ֵ��Χ", "�ο�ֵ", MessageBoxButtons.OK);
                    return -1;
                }
            }
            else
            {
                strIsMensesRelated = "0";
            }
            #endregion

            weCare.Core.Entity.clsCheckItem_VO objCheckItemVO = new weCare.Core.Entity.clsCheckItem_VO();
            objCheckItemVO.m_strRPTNO = strRptNO;
            objCheckItemVO.m_strPycode = strPycode;
            objCheckItemVO.m_strUnit = strUnit;
            objCheckItemVO.m_strCheck_Item_Name = strCheckItemName;
            objCheckItemVO.m_strCheck_Item_English_Name = strCheckItemEnglishName;
            objCheckItemVO.m_strFormula = infrmAddCheckItem.m_strFormula;
            objCheckItemVO.m_strTest_Method = strTestMethod;
            objCheckItemVO.m_strClinic_meaning = strClinicMeaning;
            objCheckItemVO.m_strShortName = strShortName;
            objCheckItemVO.m_strIs_Qc_Required = strIsQC;
            objCheckItemVO.m_strIs_Calculated = strIsCalculate;
            objCheckItemVO.m_strResultType = strResultType;
            objCheckItemVO.m_strRef_Value_Range = strRefValue;
            objCheckItemVO.m_strWbcode = strWBCode;
            objCheckItemVO.m_strIs_No_Food_Required = strIsNoFood;
            objCheckItemVO.m_strIs_Physical_Exam_Required = strIsPhysicalExam;
            objCheckItemVO.m_strIs_Reservation_Required = strIsReservation;
            objCheckItemVO.m_strSample_Valid_Time = strSampleValidTime;
            objCheckItemVO.m_strSample_Valid_Time_Unit = strSampleValidTimeUnit;
            objCheckItemVO.m_strCheck_Category_ID = strCheckCategoryID;
            objCheckItemVO.m_strAssist_Code01 = strAssistCode1;
            objCheckItemVO.m_strAssist_Code02 = strAssistCode2;
            objCheckItemVO.m_strRef_Value_Min = strDefaultMin;
            objCheckItemVO.m_strRef_Value_Max = strDefaultMax;
            objCheckItemVO.m_strSampleTypeID = strSampleType;
            objCheckItemVO.m_strIs_Sex_Related = strIsSexRelated;
            objCheckItemVO.m_strIs_Menses_Related = strIsMensesRelated;
            objCheckItemVO.m_strIs_Age_Related = strIsAgeRelated;
            objCheckItemVO.m_strFormula_User_VCHR = infrmAddCheckItem.txtFormula.Text.ToString().Trim();
            objCheckItemVO.m_strALARM_LOW_VAL_VCHR = infrmAddCheckItem.m_txtAlarmValMin.Text.ToString().Trim();
            objCheckItemVO.m_strALARM_UP_VAL_VCHR = infrmAddCheckItem.m_txtAlarmValMax.Text.ToString().Trim();
            objCheckItemVO.m_strALERT_VALUE_RANGE_VCHR = infrmAddCheckItem.m_txtAlarmVal.Text.ToString().Trim();
            objCheckItemVO.m_strItemprice_mny = infrmAddCheckItem.m_txtItemPrice.Text.ToString().Trim();
            //			objCheckItemVO.m_strOperator_ID = "0000018";

            clsDomainController_CheckItemManage objDomainCheckItemManage = new clsDomainController_CheckItemManage();
            lngRes = objDomainCheckItemManage.m_lngAddNewCheckItem(ref objCheckItemVO);
            strCheckItemID = objCheckItemVO.m_strCheck_Item_ID;

            return lngRes;
        }
        #endregion

        #region �������鷶Χ ͯ�� 2004.06.08
        //�������鷶Χ ͯ�� 2004.06.08
        public long m_lngAddCheckItemRef(frmAddCheckItem infrmAddCheckItem, string strCheckItemID)
        {
            long lngRes = 1;
            clsCheckItemRef_VO[] objCheckItemRefVOList = null;
            int count = infrmAddCheckItem.lsvSampleRef.Items.Count;
            if (count > 0)
            {
                lngRes = 0;
                objCheckItemRefVOList = new clsCheckItemRef_VO[count];
                string strMinVal = null;
                string strFromAge = null;
                string strToAge = null;
                string strMenses = null;
                string strMedUseTime = null;
                string strRefVal = null;
                string strSex = null;
                string strMaxVal = null;
                string strSEQ = null;
                bool blnValidate = true;
                for (int i = 0; i < count; i++)
                {
                    strMinVal = "";
                    strFromAge = "";
                    strToAge = "";
                    strMenses = "";
                    strMedUseTime = "";
                    strRefVal = "";
                    strSex = "";
                    strMaxVal = "";
                    strSEQ = "";
                    ListViewItem objLsvItemRef = infrmAddCheckItem.lsvSampleRef.Items[i];
                    strSEQ = objLsvItemRef.SubItems[0].Text.ToString().Trim();
                    strSex = objLsvItemRef.SubItems[1].Text.ToString().Trim();
                    strFromAge = objLsvItemRef.SubItems[2].Text.ToString().Trim();
                    strToAge = objLsvItemRef.SubItems[3].Text.ToString().Trim();
                    strMenses = objLsvItemRef.Tag.ToString().Trim();
                    strMedUseTime = objLsvItemRef.SubItems[5].Text.ToString().Trim();
                    strMinVal = objLsvItemRef.SubItems[6].Text.ToString().Trim();
                    strMaxVal = objLsvItemRef.SubItems[7].Text.ToString().Trim();
                    strRefVal = objLsvItemRef.SubItems[8].Text.ToString().Trim();
                    if (infrmAddCheckItem.m_chkAgeRelation.Checked)
                    {
                        if (strFromAge == "")
                        {
                            MessageBox.Show("��������������!", "�ο�ֵ", MessageBoxButtons.OK);
                            blnValidate = false;
                            return -1;
                        }
                        if (strFromAge == "")
                        {
                            MessageBox.Show("��������������!", "�ο�ֵ", MessageBoxButtons.OK);
                            blnValidate = false;
                            return -1;
                        }
                    }
                    if (infrmAddCheckItem.m_chkMedUsedTimeRelation.Checked)
                    {
                        if (strMedUseTime == "")
                        {
                            MessageBox.Show("��������ҩʱ��!", "�ο�ֵ", MessageBoxButtons.OK);
                            blnValidate = false;
                            return -1;
                        }
                    }
                    if (infrmAddCheckItem.m_chkSexRelation.Checked)
                    {
                        if (strSex == "")
                        {
                            MessageBox.Show("�������Ա�!", "�ο�ֵ", MessageBoxButtons.OK);
                            blnValidate = false;
                            return -1;
                        }
                    }
                    if (infrmAddCheckItem.m_chkMensesRelation.Checked)
                    {
                        if (objLsvItemRef.SubItems[4].Text.ToString().Trim() == "")
                        {
                            MessageBox.Show("�������¾���!", "�ο�ֵ", MessageBoxButtons.OK);
                            blnValidate = false;
                            return -1;
                        }
                    }

                    objCheckItemRefVOList[i] = new clsCheckItemRef_VO();
                    objCheckItemRefVOList[i].m_strCheck_Item_ID = strCheckItemID;
                    objCheckItemRefVOList[i].m_strSeq_Int = i.ToString().Trim();
                    objCheckItemRefVOList[i].m_strSex = strSex;
                    objCheckItemRefVOList[i].m_strFrom_Age = strFromAge;
                    objCheckItemRefVOList[i].m_strTo_Age = strToAge;
                    objCheckItemRefVOList[i].m_strMenses_ID = strMenses;
                    objCheckItemRefVOList[i].m_strRef_Val = strRefVal;
                    objCheckItemRefVOList[i].m_strMax_Val = strMaxVal;
                    objCheckItemRefVOList[i].m_strMin_Val = strMinVal;
                }
                if (blnValidate)
                {
                    //����ο�ֵ��Χ
                    clsDomainController_CheckItemManage objCheckItemManage = new clsDomainController_CheckItemManage();
                    lngRes = objCheckItemManage.m_lngAddCheckItemRefList(ref objCheckItemRefVOList);
                }
            }
            return lngRes;
        }
        #endregion

        #region ��ʼ���¾��������б� ͯ�� 2004.06.08
        public long m_lngGetAllMenses(frmAddCheckItem infrmAddCheckItem)
        {
            long lngRes = 0;
            DataTable dtbMenses = null;
            clsDomainController_DictManage objDomainDictManage = new clsDomainController_DictManage();
            lngRes = objDomainDictManage.m_lngGetListFor("63", out dtbMenses);
            if (dtbMenses != null && dtbMenses.Rows.Count > 0)
            {
                infrmAddCheckItem.m_cboMenses.DataSource = dtbMenses;
                infrmAddCheckItem.m_cboMenses.DisplayMember = "DICTNAME_VCHR";
                infrmAddCheckItem.m_cboMenses.ValueMember = "DICTID_CHR";
            }
            return lngRes;
        }
        #endregion

        //��ѯ���еļ�����Ŀ
        public long QryAllCheckItemByCheckCategoryAndSampleType(com.digitalwave.iCare.gui.LIS.frmAddCheckItem infrmAddCheckItem, out System.Data.DataTable dtbAllCheckItem)
        {
            long lngRes = 0;
            string strCheckCategory = infrmAddCheckItem.cboCheckCategory.SelectedValue.ToString().Trim();
            string strSampleType = infrmAddCheckItem.cboSampleType.SelectedValue.ToString().Trim();
            lngRes = proxy.Service.m_lngQryCheckItemByCheckCategoryAndSampleType(strCheckCategory, strSampleType, out dtbAllCheckItem);
            //			lngRes = objCheckItemSvc.m_lngGetAllCheckItem(out dtbAllCheckItem);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }

        //��ѯ���еļ������
        public long QryAllCheckCategory(out System.Data.DataTable dtbAllCheckCategory)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllCheckCategory(out dtbAllCheckCategory);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }

        //��ѯ���е���Ʒ���
        public long QryAllSampleType(out System.Data.DataTable dtbAllSampleType)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllSampleType(out dtbAllSampleType);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }

        //����Check_Item_ID��ѯ�������ڸü�����Ŀ�Ĳο�ֵ
        public long QryItemRefByCheckItemID(com.digitalwave.iCare.gui.LIS.frmAddCheckItem infrmAddCheckItem)//string check_item_id,out System.Data.DataTable dtbItemRef)
        {
            long lngRes = 0;
            DataTable dtbItemRef = null;
            string strCheckItemID = ((DataRow)infrmAddCheckItem.lsvCheckItemDetail.SelectedItems[0].Tag)["CHECK_ITEM_ID_CHR"].ToString().Trim();
            lngRes = proxy.Service.m_lngGetItemRefByCheckItemID(strCheckItemID, out dtbItemRef);
            int count = dtbItemRef.Rows.Count;
            infrmAddCheckItem.lsvSampleRef.Items.Clear();
            if (count > 0)
            {
                string deptName = string.Empty;
                DataRow[] drr = null;
                DataTable dtDepts = proxy.Service.GetCriticalValueRefLisDept(strCheckItemID);
                for (int i = 0; i < count; i++)
                {
                    ListViewItem objLsvItem = new ListViewItem();
                    objLsvItem.Text = i.ToString().Trim();
                    objLsvItem.SubItems.Add(dtbItemRef.Rows[i]["SEX_VCHR"].ToString().Trim());
                    objLsvItem.SubItems.Add(dtbItemRef.Rows[i]["FROM_AGE_DEC"].ToString().Trim());
                    objLsvItem.SubItems.Add(dtbItemRef.Rows[i]["TO_AGE_DEC"].ToString().Trim());
                    if (((DataRow)infrmAddCheckItem.lsvCheckItemDetail.SelectedItems[0].Tag)["IS_MENSES_RELATED_CHR"].ToString().Trim() == "1")
                    {
                        objLsvItem.SubItems.Add(dtbItemRef.Rows[i]["menses_name"].ToString().Trim());
                    }
                    else
                    {
                        objLsvItem.SubItems.Add("");
                    }
                    objLsvItem.SubItems.Add("");
                    objLsvItem.SubItems.Add(dtbItemRef.Rows[i]["MIN_VAL_VCHR"].ToString().Trim());
                    objLsvItem.SubItems.Add(dtbItemRef.Rows[i]["MAX_VAL_VCHR"].ToString().Trim());
                    objLsvItem.SubItems.Add(dtbItemRef.Rows[i]["REF_VALUE_RANGE_VCHR"].ToString().Trim());
                    objLsvItem.SubItems.Add(dtbItemRef.Rows[i]["crvalmin"].ToString().Trim());
                    objLsvItem.SubItems.Add(dtbItemRef.Rows[i]["crvalmax"].ToString().Trim());

                    deptName = string.Empty;
                    drr = dtDepts.Select("seq_int = " + dtbItemRef.Rows[i]["SEQ_INT"].ToString());
                    if (drr != null && drr.Length > 0)
                    {
                        foreach (DataRow dr in drr)
                        {
                            deptName += dr["deptname_vchr"].ToString() + ",";
                        }
                        deptName = deptName.TrimEnd(',');
                    }
                    objLsvItem.SubItems.Add(deptName);

                    objLsvItem.Tag = dtbItemRef.Rows[i]["MENSES_ID_CHR"].ToString().Trim();
                    infrmAddCheckItem.lsvSampleRef.Items.Add(objLsvItem);
                }
            }
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }

        //		//����check_item_id���±�t_bse_lis_check_item�����ڸü�����Ŀ����ϸ����
        //		public long SetCheckItemByCheckItemID(string strRptNO,string strPycode,string strUnit,string strCheckItemName,string strCheckItemEnglishName,string strFormula,string strTestMethod,string strClinicMeaning,string strCheckItemID,
        //			string strShortName,string strIsQC,string strResultType,string strRefValue,string strWBCode,string strIsNoFood,string strIsPhysicalExam,string strIsReservation,string strSampleValidTime,string strSampleValidTimeUnit,string strCheckCategoryID,
        //			string p_strAssistCode1, string p_strAssistCode2, string p_strDefaultMin, string p_strDefaultMax,string strSampleType,string strIsAgeRelated,string strIsSexRelated,string strIsMensesRelated,string strIsCalculated)
        //		{
        //			long lngRes = 0;
        //			System.Security.Principal.IPrincipal p_objPrincipal = null;
        //			weCare.Core.Entity.clsCheckItem_VO objCheckItemVO = new  weCare.Core.Entity.clsCheckItem_VO();
        //			objCheckItemVO.m_strRPTNO = strRptNO;
        //			objCheckItemVO.m_strPycode = strPycode;
        //			objCheckItemVO.m_strUnit = strUnit;
        //			objCheckItemVO.m_strCheck_Item_Name = strCheckItemName;
        //			objCheckItemVO.m_strCheck_Item_English_Name = strCheckItemEnglishName;
        //			objCheckItemVO.m_strFormula = m_objViewer.m_strFormula;
        //			objCheckItemVO.m_strTest_Method = strTestMethod;
        //			objCheckItemVO.m_strClinic_meaning = strClinicMeaning;
        //			objCheckItemVO.m_strCheck_Item_ID = strCheckItemID;
        //			objCheckItemVO.m_strShortName = strShortName;
        //			objCheckItemVO.m_strIs_Qc_Required = strIsQC;
        //			objCheckItemVO.m_strResultType = strResultType;
        //			objCheckItemVO.m_strRef_Value_Range = strRefValue;
        //			objCheckItemVO.m_strWbcode = strWBCode;
        //			objCheckItemVO.m_strIs_No_Food_Required = strIsNoFood;
        //			objCheckItemVO.m_strIs_Physical_Exam_Required = strIsPhysicalExam;
        //			objCheckItemVO.m_strIs_Reservation_Required = strIsReservation;
        //			objCheckItemVO.m_strSample_Valid_Time = strSampleValidTime;
        //			objCheckItemVO.m_strSample_Valid_Time_Unit = strSampleValidTimeUnit;
        //			objCheckItemVO.m_strCheck_Category_ID = strCheckCategoryID;
        //			objCheckItemVO.m_strAssist_Code01 = p_strAssistCode1;
        //			objCheckItemVO.m_strAssist_Code02 = p_strAssistCode2;
        //			objCheckItemVO.m_strRef_Value_Min = p_strDefaultMin;
        //			objCheckItemVO.m_strRef_Value_Max = p_strDefaultMax;
        //			objCheckItemVO.m_strSampleTypeID = strSampleType;
        ////			objCheckItemVO.m_strOperator_ID = "0000018";
        //			objCheckItemVO.m_strIs_Sex_Related = strIsSexRelated;
        //			objCheckItemVO.m_strIs_Menses_Related = strIsMensesRelated;
        //			objCheckItemVO.m_strIs_Age_Related = strIsAgeRelated;
        //			objCheckItemVO.m_strIs_Calculated = strIsCalculated;
        //			objCheckItemVO.m_strFormula_User_VCHR = m_objViewer.m_strUserFormula;
        //
        //			com.digitalwave.iCare.middletier.LIS.clsCheckItemSvc objCheckItemSvc =
        //				(com.digitalwave.iCare.middletier.LIS.clsCheckItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.LIS.clsCheckItemSvc));
        //			lngRes = objCheckItemSvc.m_lngSetCheckItemDetailByCheckItemID(ref objCheckItemVO);
        ////			objCheckItemSvc.Dispose();
        //			return lngRes;
        //		}

        //����check_item_id����t_bse_lis_itemref�����ڸü�����Ŀ�Ĳο�ֵ��ϸ����
        public long SetCheckItemRefByCheckItemID(string strCheckItemID, string strMinVal, string strFromAge, string strToAge, string strRefVal, string strSampleType, string strSex, string strMaxVal, string strSEQ)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            weCare.Core.Entity.clsCheckItemRef_VO objCheckItemRefVO = new weCare.Core.Entity.clsCheckItemRef_VO();
            objCheckItemRefVO.m_strCheck_Item_ID = strCheckItemID;
            objCheckItemRefVO.m_strMin_Val = strMinVal;
            objCheckItemRefVO.m_strFrom_Age = strFromAge;
            objCheckItemRefVO.m_strTo_Age = strToAge;
            objCheckItemRefVO.m_strRef_Val = strRefVal;
            objCheckItemRefVO.m_strSampleType = strSampleType;
            objCheckItemRefVO.m_strSex = strSex;
            objCheckItemRefVO.m_strMax_Val = strMaxVal;
            objCheckItemRefVO.m_strSeq_Int = strSEQ;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetCheckItemRefByCheckItemID(ref objCheckItemRefVO);
            //			objCheckItemRefSvc.Dispose();
            return lngRes;
        }

        //���Ѿ����ڵ�CheckItem�Ļ�������������ο�ֵ��Χ
        public long AddItemRefByCheckItemID(string strCheckItemID, string strFromAge, string strMaxVal, string strMinVal, string strItemRef, string strSampleType, string strSex, string strToAge, out string strSEQ)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            weCare.Core.Entity.clsCheckItemRef_VO objCheckItemRefVO = new weCare.Core.Entity.clsCheckItemRef_VO();
            objCheckItemRefVO.m_strCheck_Item_ID = strCheckItemID;
            objCheckItemRefVO.m_strFrom_Age = strFromAge;
            objCheckItemRefVO.m_strMax_Val = strMaxVal;
            objCheckItemRefVO.m_strMin_Val = strMinVal;
            objCheckItemRefVO.m_strRef_Val = strItemRef;
            objCheckItemRefVO.m_strSampleType = strSampleType;
            objCheckItemRefVO.m_strSex = strSex;
            objCheckItemRefVO.m_strTo_Age = strToAge;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddItemRefByCheckItemID(ref objCheckItemRefVO);
            //			objCheckItemRefSvc.Dispose();
            strSEQ = objCheckItemRefVO.m_strSeq_Int;
            return lngRes;
        }

        //����������Ŀ
        public long AddCheckItem(out string strCheckItemID, string strCheckCategoryID, string strEnglishName, string strCheckItemName, string strClinicMeaning, string strFormula, string strNoFood, string strPhysicalExam,
            string strQC, string strReservation, string strPyCode, string strRefVal, string strResultType, string strRPTNO, string strValidTime, string strValidTimeUnit, string strShortName, string strTestMethod, string strUnit, string strWbCode,
            string p_strAssistCode1, string p_strAssistCode2, string p_strDefaultMin, string p_strDefaultMax, string strSampleType)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            weCare.Core.Entity.clsCheckItem_VO objCheckItemVO = new weCare.Core.Entity.clsCheckItem_VO();
            objCheckItemVO.m_strCheck_Category_ID = strCheckCategoryID;
            objCheckItemVO.m_strCheck_Item_English_Name = strEnglishName;
            objCheckItemVO.m_strCheck_Item_Name = strCheckItemName;
            objCheckItemVO.m_strClinic_meaning = strClinicMeaning;
            objCheckItemVO.m_strFormula = strFormula;
            objCheckItemVO.m_strIs_No_Food_Required = strNoFood;
            objCheckItemVO.m_strIs_Physical_Exam_Required = strPhysicalExam;
            objCheckItemVO.m_strIs_Qc_Required = strQC;
            objCheckItemVO.m_strIs_Reservation_Required = strReservation;
            objCheckItemVO.m_strPycode = strPyCode;
            objCheckItemVO.m_strRef_Value_Range = strRefVal;
            objCheckItemVO.m_strResultType = strResultType;
            objCheckItemVO.m_strRPTNO = strRPTNO;
            objCheckItemVO.m_strSample_Valid_Time = strValidTime;
            objCheckItemVO.m_strSample_Valid_Time_Unit = strValidTimeUnit;
            objCheckItemVO.m_strShortName = strShortName;
            objCheckItemVO.m_strTest_Method = strTestMethod;
            objCheckItemVO.m_strUnit = strUnit;
            objCheckItemVO.m_strWbcode = strWbCode;
            //			objCheckItemVO.m_strOperator_ID = "0000018";
            objCheckItemVO.m_strAssist_Code01 = p_strAssistCode1;
            objCheckItemVO.m_strAssist_Code02 = p_strAssistCode2;
            objCheckItemVO.m_strRef_Value_Min = p_strDefaultMin;
            objCheckItemVO.m_strRef_Value_Max = p_strDefaultMax;
            objCheckItemVO.m_strSampleTypeID = strSampleType;
            objCheckItemVO.m_strFormula_User_VCHR = m_objViewer.txtFormula.Text.ToString().Trim();
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddCheckItem(ref objCheckItemVO);
            strCheckItemID = objCheckItemVO.m_strCheck_Item_ID;
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }

        //�����ο�ֵ��Χ
        public long AddItemRef(string strCheckItemID, string strFromAge, string strMaxVal, string strMinVal, string strItemRef, string strSampleType, string strSex, string strToAge, string strSEQ)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            weCare.Core.Entity.clsCheckItemRef_VO objCheckItemRefVO = new weCare.Core.Entity.clsCheckItemRef_VO();
            objCheckItemRefVO.m_strCheck_Item_ID = strCheckItemID;
            objCheckItemRefVO.m_strFrom_Age = strFromAge;
            objCheckItemRefVO.m_strMax_Val = strMaxVal;
            objCheckItemRefVO.m_strMin_Val = strMinVal;
            objCheckItemRefVO.m_strRef_Val = strItemRef;
            objCheckItemRefVO.m_strSampleType = strSampleType;
            objCheckItemRefVO.m_strSex = strSex;
            objCheckItemRefVO.m_strTo_Age = strToAge;
            objCheckItemRefVO.m_strSeq_Int = strSEQ;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewItemRef(ref objCheckItemRefVO, null);
            //			objCheckItemRefSvc.Dispose();
            return lngRes;
        }

        // ɾ����t_bse_lis_check_item������Ŀ
        public long DelCheckItem(com.digitalwave.iCare.gui.LIS.frmAddCheckItem infrmAddCheckItem)
        {
            long lngRes = 0;
            string strCheckItemID = ((DataRow)infrmAddCheckItem.lsvCheckItemDetail.SelectedItems[0].Tag)["CHECK_ITEM_ID_CHR"].ToString().Trim();
            if (infrmAddCheckItem.lsvSampleRef.Items.Count > 0)
            {
                //1.��ɾ���ӱ��е��������ڸü�����Ŀ�ļ���ο���Χ
                lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelCheckItemRef(strCheckItemID);
            }
            else
            {
                lngRes = 1;
            }
            if (lngRes > 0)
            {
                //2.ɾ��������Ŀ
                lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelCheckItem(strCheckItemID);
            }
            return lngRes;
        }

        //ɾ����t_bse_lis_itemref�ж�Ӧĳһ��������Ŀ��ĳһ��ŵĲο�ֵ
        public long DelCheckItemRef(string strCheckItemID, string strSEQ)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelCheckItemRefBySEQ(strCheckItemID, strSEQ);
            return lngRes;
        }

    }
}
