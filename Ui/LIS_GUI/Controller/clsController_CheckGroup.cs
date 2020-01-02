using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using com.digitalwave.Utility;//Utility.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// Summary description for clsController_CheckGroup.
    /// </summary>
    public class clsController_CheckGroup : com.digitalwave.GUI_Base.clsController_Base
    {
        //���ڼ�¼�����Ѿ����Ϊ�걾��ļ�����Ŀ
        weCare.Core.Entity.clsSampleGroupDetail_VO[] objSampleGroupDetailVOList = null;
        //���ڼ�¼�����Ѿ����Ϊ������ı걾��
        weCare.Core.Entity.clsReportGroupDetail_VO[] objReportGroupDetailVOList = null;
        //���ڼ�¼�����Ѿ����Ϊ�걾������뵥Ԫ
        weCare.Core.Entity.clsLisSampleGroupUnit_VO[] m_objSampleGroupUnitArr = null;
        DataTable dtbAllCheckCategory = null;
        com.digitalwave.iCare.gui.LIS.clsDomainController_SampleGroupManage m_objManage;
        internal List<clsLisSampleGroupModel_VO> arlSampleModelRemove = new List<clsLisSampleGroupModel_VO>();
        internal List<clsLisSampleGroupModel_VO> arlSampleModelAdd = new List<clsLisSampleGroupModel_VO>();
        internal List<clsLisGroupSampleType_VO> m_arlGroupSampleAdd = new List<clsLisGroupSampleType_VO>();
        internal List<clsLisGroupSampleType_VO> m_arlGroupSampleRemove = new List<clsLisGroupSampleType_VO>();
        internal List<clsLisSampleGroupModel_VO> m_arlSampleModelRaw = new List<clsLisSampleGroupModel_VO>();
        internal List<clsApplUnitDetail_VO> m_arlSameApplyUnitItem = new List<clsApplUnitDetail_VO>();

        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region ���ô������
        com.digitalwave.iCare.gui.LIS.frmCheckGroup m_objViewer;
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmCheckGroup)frmMDI_Child_Base_in;
        }
        #endregion

        #region ���캯��
        public clsController_CheckGroup()
        {
            //
            // TODO: Add constructor logic here
            //
            m_objManage = new clsDomainController_SampleGroupManage();
        }

        #endregion

        #region ���ݼ�������ȡ���е����뵥Ԫ ͯ�� 2004.09.27
        public void m_mthGetApplUnitByCheckCategory()
        {
            m_objViewer.m_trvApplUnit.Nodes[0].Nodes.Clear();
            if (m_objViewer.cboCheckCategory.Items.Count <= 0)
                return;
            long lngRes = 0;
            clsApplUnit_VO[] objResultArr = null;
            clsDomainController_ApplyUnitManage objManage = new clsDomainController_ApplyUnitManage();
            lngRes = objManage.m_lngGetApplUnitByCheckCategory(m_objViewer.cboCheckCategory.SelectedValue.ToString().Trim(),
                out objResultArr);
            if (lngRes > 0 && objResultArr != null)
            {
                for (int i = 0; i < objResultArr.Length; i++)
                {
                    TreeNode objTreeNode = new TreeNode();
                    objTreeNode.Text = "(" + objResultArr[i].strApplUnitID + ")" + objResultArr[i].strApplUnitName;
                    objTreeNode.Tag = objResultArr[i];
                    m_objViewer.m_trvApplUnit.Nodes[0].Nodes.Add(objTreeNode);
                }
            }
        }
        #endregion

        #region ɾ���걾������뵥Ԫ��� ͯ�� 2004.09.09
        public void m_mthRemoveApplUnit()
        {
            if (m_objViewer.m_trvApplUnit.Nodes.Count <= 0)
                return;
            for (int i = 0; i < m_objViewer.m_trvAddSampleGroup.Nodes.Count; i++)
            {
                if (m_objViewer.m_trvAddSampleGroup.Nodes[i].Checked)
                {
                    for (int j = 0; j < m_objViewer.m_lsvApplUnitItem.Items.Count; j++)
                    {
                        if (((DataRow)m_objViewer.m_lsvApplUnitItem.Items[j].Tag)["apply_unit_id_chr"].ToString().Trim()
                            == ((clsLisSampleGroupUnit_VO)m_objViewer.m_trvAddSampleGroup.Nodes[i].Tag).m_strAPPLY_UNIT_ID_CHR)
                        {
                            m_objViewer.m_lsvApplUnitItem.Items[j].Remove();
                            j--;
                        }
                    }
                    m_objViewer.m_trvAddSampleGroup.Nodes[i].Remove();
                    i--;
                }
            }

            for (int i = 0; i < m_objViewer.m_lsvApplUnitItem.Items.Count; i++)
            {
                m_objViewer.m_lsvApplUnitItem.Items[i].SubItems[3].Text = i.ToString();
            }
        }
        #endregion

        #region ��ӱ걾������뵥Ԫ��� ͯ�� 2004.09.09
        public void m_mthAddApplUnit()
        {
            //�ж����뵥Ԫ�Ƿ��ظ�
            for (int i = 0; i < m_objViewer.m_trvApplUnit.Nodes[0].Nodes.Count; i++)
            {
                if (m_objViewer.m_trvApplUnit.Nodes[0].Nodes[i].Checked)
                {
                    if (m_objSampleGroupUnitArr != null)
                    {
                        //�ж����뵥Ԫ�Ƿ񱻱�ı걾�����
                        for (int k = 0; k < m_objSampleGroupUnitArr.Length; k++)
                        {
                            if (m_objSampleGroupUnitArr[k].m_strAPPLY_UNIT_ID_CHR ==
                                ((clsApplUnit_VO)m_objViewer.m_trvApplUnit.Nodes[0].Nodes[i].Tag).strApplUnitID)
                            {
                                MessageBox.Show("���뵥Ԫ[" + m_objSampleGroupUnitArr[k].m_strAPPLY_UNIT_DESC_VCHR + "]�Ѿ��������걾����ӣ�");
                                return;
                            }
                        }
                    }
                    for (int j = 0; j < m_objViewer.m_trvAddSampleGroup.Nodes.Count; j++)
                    {
                        if (((clsApplUnit_VO)m_objViewer.m_trvApplUnit.Nodes[0].Nodes[i].Tag).strApplUnitID ==
                            ((clsLisSampleGroupUnit_VO)m_objViewer.m_trvAddSampleGroup.Nodes[j].Tag).m_strAPPLY_UNIT_ID_CHR)
                        {
                            MessageBox.Show("���뵥Ԫ[" + ((clsApplUnit_VO)m_objViewer.m_trvApplUnit.Nodes[0].Nodes[i].Tag).strApplUnitName + "]�Ѿ�����ӣ�");
                            return;
                        }
                    }
                }
            }
            clsDomainController_ApplyUnitManage objManage = new clsDomainController_ApplyUnitManage();
            for (int i = 0; i < m_objViewer.m_trvApplUnit.Nodes[0].Nodes.Count; i++)
            {
                if (m_objViewer.m_trvApplUnit.Nodes[0].Nodes[i].Checked)
                {
                    TreeNode objTreeNode = new TreeNode();
                    objTreeNode.Text = "(" + ((clsApplUnit_VO)m_objViewer.m_trvApplUnit.Nodes[0].Nodes[i].Tag).strApplUnitID + ")"
                        + ((clsApplUnit_VO)m_objViewer.m_trvApplUnit.Nodes[0].Nodes[i].Tag).strApplUnitName;
                    clsLisSampleGroupUnit_VO objSampleGroupUnit = new clsLisSampleGroupUnit_VO();
                    objSampleGroupUnit.m_strAPPLY_UNIT_DESC_VCHR = ((clsApplUnit_VO)m_objViewer.m_trvApplUnit.Nodes[0].Nodes[i].Tag).strApplUnitName;
                    objSampleGroupUnit.m_strAPPLY_UNIT_ID_CHR = ((clsApplUnit_VO)m_objViewer.m_trvApplUnit.Nodes[0].Nodes[i].Tag).strApplUnitID;
                    objSampleGroupUnit.m_strSAMPLE_GROUP_ID_CHR = m_objViewer.txtCheckGroupNo.Text.ToString().Trim();
                    objTreeNode.Tag = objSampleGroupUnit;
                    m_objViewer.m_trvAddSampleGroup.Nodes.Add(objTreeNode);

                    DataTable dtbResult = null;
                    long lngRes = objManage.m_lngGetCheckItemByApplUnitID(objSampleGroupUnit.m_strAPPLY_UNIT_ID_CHR, out dtbResult);
                    if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtbResult.Rows.Count; j++)
                        {
                            bool blnSameItem = false;
                            for (int k = 0; k < m_objViewer.m_lsvApplUnitItem.Items.Count; k++)
                            {
                                if (dtbResult.Rows[j]["check_item_id_chr"].ToString().Trim() == ((DataRow)m_objViewer.m_lsvApplUnitItem.Items[k].Tag)
                                    ["check_item_id_chr"].ToString().Trim())
                                {
                                    blnSameItem = true;
                                    clsApplUnitDetail_VO objApplDetail = new clsApplUnitDetail_VO();
                                    objApplDetail.strApplUnitID = dtbResult.Rows[j]["apply_unit_id_chr"].ToString().Trim();
                                    objApplDetail.strCheckItemID = dtbResult.Rows[j]["check_item_id_chr"].ToString().Trim();
                                    m_arlSameApplyUnitItem.Add(objApplDetail);
                                }
                            }
                            if (!blnSameItem)
                            {
                                ListViewItem objlsvItem = new ListViewItem();
                                objlsvItem.Text = dtbResult.Rows[j]["check_item_id_chr"].ToString().Trim();
                                objlsvItem.SubItems.Add(dtbResult.Rows[j]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim());
                                objlsvItem.SubItems.Add(dtbResult.Rows[j]["CHECK_ITEM_NAME_VCHR"].ToString().Trim());
                                int intOrder = m_objViewer.m_lsvApplUnitItem.Items.Count;
                                objlsvItem.SubItems.Add(intOrder.ToString().Trim());
                                objlsvItem.Tag = dtbResult.Rows[j];
                                m_objViewer.m_lsvApplUnitItem.Items.Add(objlsvItem);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region ����������ID��ѯ���������°��������뵥Ԫ ͯ�� 2004.09.08
        public void m_mthGetSampleGroupUnitBySampleGroupID()
        {
            long lngRes = 0;
            m_objViewer.m_trvAddSampleGroup.Nodes.Clear();
            m_objViewer.m_lsvApplUnitItem.Items.Clear();
            ArrayList arlApplID = new ArrayList();
            string strSampleGroupID = m_objViewer.txtCheckGroupNo.Text.ToString().Trim();
            clsLisSampleGroupUnit_VO[] objResultArr = null;
            lngRes = m_objManage.m_lngGetApplUnitBySampleGroupID(strSampleGroupID, out objResultArr);
            if (lngRes > 0 && objResultArr != null && objResultArr.Length > 0)
            {
                for (int i = 0; i < objResultArr.Length; i++)
                {
                    TreeNode objTreeNode = new TreeNode();
                    objTreeNode.Text = "(" + objResultArr[i].m_strAPPLY_UNIT_ID_CHR + ")" + objResultArr[i].m_strAPPLY_UNIT_DESC_VCHR;
                    objTreeNode.Tag = objResultArr[i];
                    m_objViewer.m_trvAddSampleGroup.Nodes.Add(objTreeNode);
                    arlApplID.Add(objResultArr[i].m_strAPPLY_UNIT_ID_CHR);
                }

                m_mthGetApplItemByApplUnitIDArr(arlApplID);
            }
        }
        #endregion

        #region �������뵥ԪID�����ȡ�����뵥Ԫ�µļ�����Ŀ ͯ�� 2004.09.09
        public void m_mthGetApplItemByApplUnitIDArr(ArrayList p_arlApplID)
        {
            long lngRes = 0;
            DataTable dtbAllItem = null;
            DataTable dtbResult = null;
            clsDomainController_ApplyUnitManage objManage = new clsDomainController_ApplyUnitManage();
            for (int i = 0; i < p_arlApplID.Count; i++)
            {
                lngRes = objManage.m_lngGetCheckItemByApplUnitID(p_arlApplID[i].ToString().Trim(), out dtbResult);
                if (lngRes > 0 && dtbResult != null)
                {
                    if (dtbAllItem == null)
                    {
                        dtbAllItem = dtbResult.Copy();
                    }
                    else
                    {
                        for (int j = 0; j < dtbResult.Rows.Count; j++)
                        {
                            DataRow dr = dtbAllItem.NewRow();
                            dr.ItemArray = dtbResult.Rows[j].ItemArray;
                            dtbAllItem.Rows.Add(dr);
                        }
                    }
                }
            }
            DataRow[] drArr = dtbAllItem.Select("1=1", "PRINT_SEQ_INT ASC");
            for (int i = 0; i < drArr.Length; i++)
            {
                bool blnSameItem = false;
                for (int j = 0; j < m_objViewer.m_lsvApplUnitItem.Items.Count; j++)
                {
                    if (drArr[i]["check_item_id_chr"].ToString().Trim() == ((DataRow)m_objViewer.m_lsvApplUnitItem.Items[j].Tag)
                        ["check_item_id_chr"].ToString().Trim())
                    {
                        blnSameItem = true;
                        clsApplUnitDetail_VO objApplDetail = new clsApplUnitDetail_VO();
                        objApplDetail.strApplUnitID = drArr[i]["apply_unit_id_chr"].ToString().Trim();
                        objApplDetail.strCheckItemID = drArr[i]["check_item_id_chr"].ToString().Trim();
                        m_arlSameApplyUnitItem.Add(objApplDetail);
                    }
                }
                if (!blnSameItem)
                {
                    ListViewItem objlsvItem = new ListViewItem();
                    objlsvItem.Text = drArr[i]["check_item_id_chr"].ToString().Trim();
                    objlsvItem.SubItems.Add(drArr[i]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim());
                    objlsvItem.SubItems.Add(drArr[i]["CHECK_ITEM_NAME_VCHR"].ToString().Trim());
                    objlsvItem.SubItems.Add(drArr[i]["PRINT_SEQ_INT"].ToString().Trim());
                    objlsvItem.Tag = drArr[i];
                    m_objViewer.m_lsvApplUnitItem.Items.Add(objlsvItem);
                }
            }
        }
        #endregion

        #region ����ѡ�е����뵥Ԫ�г������еļ�����Ŀ(OVER) ͯ�� 2004.09.08
        public void m_mthGetApplUnitItem(TreeNode p_objTreeNode)
        {
            if (p_objTreeNode == m_objViewer.m_trvApplUnit.Nodes[0])
                return;
            long lngRes = 0;
            m_objViewer.m_lsvApplUnitItem.Items.Clear();
            clsCheckItem_VO[] objResultArr = null;
            clsDomainController_ApplyUnitManage objManage = new clsDomainController_ApplyUnitManage();
            lngRes = objManage.m_lngGetCheckItemByApplUnitID(((clsApplUnit_VO)p_objTreeNode.Tag).strApplUnitID, out objResultArr);
            if (lngRes > 0 && objResultArr != null && objResultArr.Length > 0)
            {
                for (int i = 0; i < objResultArr.Length; i++)
                {
                    ListViewItem objlsvItem = new ListViewItem(objResultArr[i].m_strCheck_Item_ID);
                    objlsvItem.SubItems.Add(objResultArr[i].m_strCheck_Item_English_Name);
                    objlsvItem.SubItems.Add(objResultArr[i].m_strCheck_Item_Name);
                    objlsvItem.Tag = objResultArr[i];
                    m_objViewer.m_lsvApplUnitItem.Items.Add(objlsvItem);
                }
            }
        }
        #endregion

        #region ��ȡ�����Ѿ���ӵ����뵥Ԫ ͯ�� 2004.09.10
        public void m_mthGetUsedApplUnit()
        {
            long lngRes = 0;
            m_objSampleGroupUnitArr = null;
            lngRes = m_objManage.m_lngGetApplUnitBySampleGroupID(null, out m_objSampleGroupUnitArr);
        }
        #endregion

        #region ��ѯ���е����뵥Ԫ�������Ѿ���ӵ����뵥Ԫ ͯ�� 2004.09.08
        public void m_mthGetAllApplUnit()
        {
            //			m_objViewer.m_trvApplUnit.Nodes[0].Nodes.Clear();
            long lngRes = 0;
            //			clsApplUnit_VO[] objResultArr = null;
            //�����Ѿ���ӵ����뵥Ԫ
            m_objSampleGroupUnitArr = null;
            lngRes = m_objManage.m_lngGetApplUnitBySampleGroupID(null, out m_objSampleGroupUnitArr);

            //			clsDomainController_ApplyUnitManage objManage = new clsDomainController_ApplyUnitManage();
            //			lngRes = objManage.m_lngGetAllApplUnit(out objResultArr);
            //			if(lngRes > 0 && objResultArr != null && objResultArr.Length > 0)
            //			{
            //				for(int i=0;i<objResultArr.Length;i++)
            //				{
            //					TreeNode objTreeNode = new TreeNode();
            //					objTreeNode.Text = "("+objResultArr[i].strApplUnitID+")"+objResultArr[i].strApplUnitName;
            //					objTreeNode.Tag = objResultArr[i];
            //					m_objViewer.m_trvApplUnit.Nodes[0].Nodes.Add(objTreeNode);
            //				}
            //			}
        }
        #endregion

        #region ��ȡ�걾���Ӧ�ı걾�����б� ͯ�� 2004.09.06
        public void m_mthGetGroupSampleBySampleGroupID()
        {
            if (m_objViewer.txtCheckGroupNo.Text.ToString().Trim() == "")
                return;
            m_objViewer.m_lsvGroupSampleType.Items.Clear();
            string strCheckGroupNo = m_objViewer.txtCheckGroupNo.Text.ToString().Trim();
            long lngRes = 0;
            clsLisGroupSampleType_VO[] objResultArr = null;
            lngRes = m_objManage.m_lngGetGroupSampleTypeBySampleGroupID(strCheckGroupNo, out objResultArr);
            if (lngRes > 0 && objResultArr != null)
            {
                for (int i = 0; i < objResultArr.Length; i++)
                {
                    ListViewItem objlsvItem = new ListViewItem();
                    objlsvItem.Text = objResultArr[i].m_strSAMPLE_TYPE_DESC_VCHR;
                    objlsvItem.Tag = objResultArr[i];
                    m_objViewer.m_lsvGroupSampleType.Items.Add(objlsvItem);
                }
            }
        }
        #endregion

        #region ��ӱ걾��걾����
        public void m_mthAddGroupSampleType()
        {
            if (m_objViewer.m_cboGroupSampleType.Items.Count <= 0)
                return;

            if (m_objViewer.m_lsvGroupSampleType.Items.Count >= 0)
            {
                for (int i = 0; i < m_objViewer.m_lsvGroupSampleType.Items.Count; i++)
                {
                    clsLisGroupSampleType_VO objTemp = (clsLisGroupSampleType_VO)m_objViewer.m_lsvGroupSampleType.Items[i].Tag;
                    if (objTemp.m_strSAMPLE_TYPE_ID_CHR == m_objViewer.m_cboGroupSampleType.SelectedValue.ToString().Trim())
                    {
                        MessageBox.Show("�ñ걾�����ѱ���ӣ�");
                        return;
                    }
                }
            }

            clsLisGroupSampleType_VO objRecord = new clsLisGroupSampleType_VO();
            objRecord.m_strSAMPLE_TYPE_DESC_VCHR = m_objViewer.m_cboGroupSampleType.Text.ToString().Trim();
            objRecord.m_strSAMPLE_TYPE_ID_CHR = m_objViewer.m_cboGroupSampleType.SelectedValue.ToString().Trim();
            objRecord.m_strSAMPLE_GROUP_ID_CHR = m_objViewer.txtCheckGroupNo.Text.ToString().Trim();

            m_arlGroupSampleAdd.Add(objRecord);

            ListViewItem objlsvItem = new ListViewItem();
            objlsvItem.Text = objRecord.m_strSAMPLE_TYPE_DESC_VCHR;
            objlsvItem.Tag = objRecord;
            m_objViewer.m_lsvGroupSampleType.Items.Add(objlsvItem);
        }
        #endregion

        #region ɾ���걾��걾����
        public void m_mthRemoveGroupSampleType()
        {
            if (m_objViewer.m_cboGroupSampleType.Items.Count <= 0)
                return;

            for (int j = 0; j < m_objViewer.m_lsvGroupSampleType.Items.Count; j++)
            {
                if (m_objViewer.m_lsvGroupSampleType.Items[j].Checked)
                {
                    bool blnRemove = false;
                    clsLisGroupSampleType_VO objGroupSample = (clsLisGroupSampleType_VO)m_objViewer.m_lsvGroupSampleType.Items[j].Tag;
                    for (int i = 0; i < m_arlGroupSampleAdd.Count; i++)
                    {
                        if (((clsLisGroupSampleType_VO)m_arlGroupSampleAdd[i]).m_strSAMPLE_TYPE_ID_CHR.Trim() ==
                            objGroupSample.m_strSAMPLE_TYPE_ID_CHR.Trim())
                        {
                            m_arlGroupSampleAdd.RemoveAt(i);
                            m_objViewer.m_lsvGroupSampleType.Items[j].Remove();
                            j--;
                            blnRemove = true;
                        }
                    }
                    if (!blnRemove)
                    {
                        m_arlGroupSampleRemove.Add(objGroupSample);
                        m_objViewer.m_lsvGroupSampleType.Items[j].Remove();
                    }
                }
            }
        }
        #endregion

        #region ��ȡ�걾���Ӧ�������ͺ��б� 2004.08.18
        public void m_mthGetSampleGroupModel()
        {
            if (m_objViewer.txtCheckGroupNo.Text.ToString().Trim() == "")
                return;
            m_arlSampleModelRaw.Clear();
            m_objViewer.m_lsvDeviceModel.Items.Clear();
            string strCheckGroupNo = m_objViewer.txtCheckGroupNo.Text.ToString().Trim();
            long lngRes = 0;
            clsLisSampleGroupModel_VO[] objResultArr = null;
            lngRes = m_objManage.m_lngGetDeviceModelArrBySampleGroupID(strCheckGroupNo, out objResultArr);
            if (lngRes > 0 && objResultArr != null)
            {
                for (int i = 0; i < objResultArr.Length; i++)
                {
                    ListViewItem objlsvItem = new ListViewItem();
                    objlsvItem.Text = objResultArr[i].m_strDEVICE_MODEL_DESC_VCHR;
                    objlsvItem.Tag = objResultArr[i];
                    m_arlSampleModelRaw.Add(objResultArr[i]);
                    m_objViewer.m_lsvDeviceModel.Items.Add(objlsvItem);
                }
            }
        }
        #endregion

        #region ��������������ͺ�
        public void m_mthAddSampleGroupModel()
        {
            if (m_objViewer.cboDeviceModle.Items.Count <= 0 || m_objViewer.cboDeviceModle.Enabled == false)
                return;
            if (m_objViewer.m_lsvDeviceModel.Items.Count >= 0)
            {
                for (int i = 0; i < m_objViewer.m_lsvDeviceModel.Items.Count; i++)
                {
                    clsLisSampleGroupModel_VO objTemp = (clsLisSampleGroupModel_VO)m_objViewer.m_lsvDeviceModel.Items[i].Tag;
                    if (objTemp.m_strDEVICE_MODEL_ID_CHR == m_objViewer.cboDeviceModle.SelectedValue.ToString().Trim())
                    {
                        MessageBox.Show("�������ͺ��ѱ���ӣ�");
                        return;
                    }
                }
            }

            clsLisSampleGroupModel_VO objSampleGroupModel = new clsLisSampleGroupModel_VO();
            objSampleGroupModel.m_strDEVICE_MODEL_ID_CHR = m_objViewer.cboDeviceModle.SelectedValue.ToString().Trim();
            objSampleGroupModel.m_strSAMPLE_GROUP_ID_CHR = m_objViewer.txtCheckGroupNo.Text.ToString().Trim();
            objSampleGroupModel.m_strDEVICE_MODEL_DESC_VCHR = m_objViewer.cboDeviceModle.Text.ToString().Trim();

            arlSampleModelAdd.Add(objSampleGroupModel);

            ListViewItem objlsvItem = new ListViewItem(objSampleGroupModel.m_strDEVICE_MODEL_DESC_VCHR);
            objlsvItem.Tag = objSampleGroupModel;
            m_objViewer.m_lsvDeviceModel.Items.Add(objlsvItem);
        }
        #endregion

        #region ɾ�������������ͺ�
        public void m_mthDelSampleGroupModel()
        {
            if (m_objViewer.m_lsvDeviceModel.Items.Count <= 0 || m_objViewer.cboDeviceModle.Enabled == false)
                return;
            for (int j = 0; j < m_objViewer.m_lsvDeviceModel.Items.Count; j++)
            {
                if (m_objViewer.m_lsvDeviceModel.Items[j].Checked)
                {
                    bool blnRemove = false;
                    clsLisSampleGroupModel_VO objDeviceModel = (clsLisSampleGroupModel_VO)m_objViewer.m_lsvDeviceModel.Items[j].Tag;
                    for (int i = 0; i < arlSampleModelAdd.Count; i++)
                    {
                        if (((clsLisSampleGroupModel_VO)arlSampleModelAdd[i]).m_strDEVICE_MODEL_ID_CHR.Trim() ==
                            objDeviceModel.m_strDEVICE_MODEL_ID_CHR.Trim())
                        {
                            arlSampleModelAdd.RemoveAt(i);
                            m_objViewer.m_lsvDeviceModel.Items[j].Remove();
                            j--;
                            blnRemove = true;
                        }
                    }
                    if (!blnRemove)
                    {
                        clsLisSampleGroupModel_VO objSampleGroupModel = new clsLisSampleGroupModel_VO();
                        objSampleGroupModel.m_strDEVICE_MODEL_ID_CHR = objDeviceModel.m_strDEVICE_MODEL_ID_CHR;
                        objSampleGroupModel.m_strSAMPLE_GROUP_ID_CHR = m_objViewer.txtCheckGroupNo.Text.ToString().Trim();
                        arlSampleModelRemove.Add(objSampleGroupModel);
                        m_objViewer.m_lsvDeviceModel.Items[j].Remove();
                    }
                }
            }
        }
        #endregion

        #region ��ʼ����Ϣ
        public long GetInitInfo(frmCheckGroup infrmCheckGroup)//,out DataTable dtbAllCheckGroup,out DataTable dtbAllCheckCategory)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            clsDomainController_LisDeviceManage objLisDeviceManage = new clsDomainController_LisDeviceManage();
            DataTable dtbDeviceModel = null;
            //��ʼ���������б�
            refreshAllCheckGroupANDTrvCheckGroup(infrmCheckGroup);
            //��ʼ����������б�
            lngRes = proxy.Service.m_lngGetAllCheckCategory(out dtbAllCheckCategory);
            if (dtbAllCheckCategory.Rows.Count > 0)
            {
                infrmCheckGroup.cboCheckCategory.DataSource = dtbAllCheckCategory;
                infrmCheckGroup.cboCheckCategory.DisplayMember = "CHECK_CATEGORY_DESC_VCHR";
                infrmCheckGroup.cboCheckCategory.ValueMember = "CHECK_CATEGORY_ID_CHR";
            }
            //��ʼ����ӡ����б�
            DataTable dtbPrintCategory = null;
            lngRes = proxy.Service.m_lngGetAllPrintCategory(out dtbPrintCategory);
            infrmCheckGroup.cboPrintCategory.DataSource = dtbPrintCategory;
            infrmCheckGroup.cboPrintCategory.DisplayMember = "PRINT_CATEGORY_DESC_VCHR";
            infrmCheckGroup.cboPrintCategory.ValueMember = "PRINT_CATEGORY_ID_CHR";

            //��ʼ����Ʒ����б�
            DataTable dtbSampleType = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleTypeList(out dtbSampleType);
            infrmCheckGroup.cboSampleType.DataSource = dtbSampleType;
            infrmCheckGroup.cboSampleType.DisplayMember = "SAMPLE_TYPE_DESC_VCHR";
            infrmCheckGroup.cboSampleType.ValueMember = "SAMPLE_TYPE_ID_CHR";
            infrmCheckGroup.rdbBseGroup.Checked = true;
            //��ʼ�������б�
            lngRes = objLisDeviceManage.m_lngGetDeviceModel(out dtbDeviceModel);
            if (dtbDeviceModel.Rows.Count > 0)
            {
                infrmCheckGroup.cboDeviceModle.DataSource = dtbDeviceModel;
                infrmCheckGroup.cboDeviceModle.DisplayMember = "DEVICE_MODEL_DESC_VCHR";
                infrmCheckGroup.cboDeviceModle.ValueMember = "DEVICE_MODEL_ID_CHR";
                infrmCheckGroup.cboDeviceModle.SelectedIndex = 0;
            }
            //��ʼ�����еı걾����ϸ�ͱ�������ϸ
            refreshSampleGroupAndReportGroupDetail();

            //��ʼ�����뵥Ԫ�б�
            m_mthGetApplUnitByCheckCategory();
            //			m_mthGetAllApplUnit();
            m_mthGetUsedApplUnit();

            return lngRes;
        }
        #endregion

        private void refreshSampleGroupAndReportGroupDetail()
        {
            clsDomainController_ReportManage objDomainReportGroup = new clsDomainController_ReportManage();
            clsDomainController_SampleGroupManage objDomainSampleGroup = new clsDomainController_SampleGroupManage();
            objDomainReportGroup.m_lngGetAllReportGroupDetail(out objReportGroupDetailVOList);
            objDomainSampleGroup.m_lngGetAllSampleGroupDetail(out objSampleGroupDetailVOList);
        }

        #region ˢ�����еļ������б�
        //ˢ�����еļ������б�
        public void refreshAllCheckGroupANDTrvCheckGroup(frmCheckGroup infrmCheckGroup)
        {
            //��ʼ���������б�
            clsDomainController_ReportManage objDomainReportGroup = new clsDomainController_ReportManage();
            clsDomainController_SampleGroupManage objDomainSampleGroup = new clsDomainController_SampleGroupManage();
            clsReportGroup_VO[] objReportGroupVOList = null;
            //			clsSampleGroup_VO[] objSampleGroupVOList = null;
            objDomainReportGroup.m_lngGetAllReportGroup(out objReportGroupVOList);
            //			objDomainSampleGroup.m_lngGetAllSampleGroup(out objSampleGroupVOList);
            //��ʼ��trvCheckGroup�б�
            infrmCheckGroup.trvCheckGroup.Nodes[1].Nodes.Clear();
            if (objReportGroupVOList != null)
            {
                if (objReportGroupVOList.Length > 0)
                {
                    TreeNode CurNode = null;
                    TreeNode ChildNode = null;

                    for (int i = 0; i < objReportGroupVOList.Length; i++)
                    {
                        CurNode = infrmCheckGroup.trvCheckGroup.Nodes[1].Nodes.Add("(" + objReportGroupVOList[i].strReportGroupID + ")" + objReportGroupVOList[i].strReportGroupName);
                        CurNode.Tag = objReportGroupVOList[i];
                        if (objReportGroupVOList[i].objSampleGroupVO != null)
                        {
                            if (objReportGroupVOList[i].objSampleGroupVO.Length > 0)
                            {
                                for (int j = 0; j < objReportGroupVOList[i].objSampleGroupVO.Length; j++)
                                {
                                    ChildNode = CurNode.Nodes.Add("(" + objReportGroupVOList[i].objSampleGroupVO[j].strSampleGroupID + ")" + objReportGroupVOList[i].objSampleGroupVO[j].strSampleGroupName);
                                    ChildNode.Tag = objReportGroupVOList[i].objSampleGroupVO[j];
                                }
                            }
                        }
                    }
                }
            }
            TreeNode[] objTreeNodeArr = null;
            m_objManage.m_mthGetSampleGroupTreeNodes(out objTreeNodeArr);
            infrmCheckGroup.trvCheckGroup.Nodes[0].Nodes.Clear();
            if (objTreeNodeArr != null)
            {
                infrmCheckGroup.trvCheckGroup.Nodes[0].Nodes.AddRange(objTreeNodeArr);
            }

            //��ʼ��trvSubCheckGroup�б�
            infrmCheckGroup.trvSubCheckGroup.Nodes.Clear();
            TreeNode[] objSubTreeNodeArr = null;
            m_objManage.m_mthGetSampleGroupTreeNodes(out objSubTreeNodeArr);
            if (objSubTreeNodeArr != null)
            {
                infrmCheckGroup.trvSubCheckGroup.Nodes.AddRange(objSubTreeNodeArr);
            }
        }
        #endregion

        //��ȡtrvCheckGroup���ӽڵ���Ϣ
        public void getChildNode(frmCheckGroup infrmCheckGroup, TreeNode p_treeNode)
        {
            string strFussField = "GROUPID_CHR";
            string strFieldValue = ((clsCheckGroup_VO)p_treeNode.Tag).m_strGROUPID;
            string strOrderField = strFussField;
            try
            {
                weCare.Core.Entity.clsCheckGroup_VO[] objChildGroupVOList = null;
                proxy.Service.m_mthGetGroupInfo(1, strFussField, strFieldValue, strOrderField, false, out objChildGroupVOList);

                if (objChildGroupVOList != null)
                {
                    if (objChildGroupVOList.Length > 0)
                    {
                        //						p_treeNode.Nodes.Remove(p_treeNode.Nodes[0]);

                        TreeNode CurNode = null;
                        for (int i = 0; i < objChildGroupVOList.Length; i++)
                        {
                            CurNode = p_treeNode.Nodes.Add("(" + objChildGroupVOList[i].m_strGROUPID + ")" + objChildGroupVOList[i].m_strGroupName);
                            CurNode.Tag = objChildGroupVOList[i];
                            if (int.Parse(objChildGroupVOList[i].m_strHas_SubGroup) > 0)
                                getChildNode(infrmCheckGroup, CurNode);
                            //								CurNode.Nodes.Add("");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //����trvAddSubGroup����ڵ���Ϣ 2004.05.10(�����ڻ��������Ӽ�����Ŀ��Ϣ)
        public void getAddSubGroupChildNode(frmCheckGroup infrmCheckGroup, TreeNode p_treeNode)
        {
            string strFussField = "GROUPID_CHR";
            string strFieldValue = ((clsCheckGroup_VO)p_treeNode.Tag).m_strGROUPID;
            string strOrderField = strFussField;
            try
            {
                weCare.Core.Entity.clsCheckGroup_VO[] objChildGroupVOList = null;
                proxy.Service.m_mthGetGroupInfo(1, strFussField, strFieldValue, strOrderField, false, out objChildGroupVOList);

                if (objChildGroupVOList != null)
                {
                    if (objChildGroupVOList.Length > 0)
                    {
                        //						p_treeNode.Nodes.Remove(p_treeNode.Nodes[0]);

                        TreeNode CurNode = null;
                        for (int i = 0; i < objChildGroupVOList.Length; i++)
                        {
                            CurNode = p_treeNode.Nodes.Add("(" + objChildGroupVOList[i].m_strGROUPID + ")" + objChildGroupVOList[i].m_strGroupName);
                            CurNode.Tag = objChildGroupVOList[i];
                            if (int.Parse(objChildGroupVOList[i].m_strHas_SubGroup) > 0)
                                getChildNode(infrmCheckGroup, CurNode);
                            if (objChildGroupVOList[i].m_strHas_SubGroup == "0")
                            {
                                CurNode.Nodes.Add("");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #region ��Ӽ�����Ŀ
        public void NewCheckItemBybtnNewCheckClick(frmCheckGroup infrmCheckGroup)
        {
            //�˴�������isAdded���ж��Ѿ�����˵���Ŀ�����ظ����
            int count = infrmCheckGroup.lsvCheckItem.Items.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (infrmCheckGroup.lsvCheckItem.Items[i].Checked)
                    {
                        int addCount = infrmCheckGroup.lsvAddCheckItem.Items.Count;
                        if (objSampleGroupDetailVOList != null && objSampleGroupDetailVOList.Length > 0)
                        {
                            for (int k = 0; k < objSampleGroupDetailVOList.Length; k++)
                            {
                                if (infrmCheckGroup.lsvCheckItem.Items[i].SubItems[0].Text.ToString().Trim() == objSampleGroupDetailVOList[k].strCheckItemID)
                                {
                                    MessageBox.Show("�ü�����Ŀ�ѱ������걾����ӣ�", "������Ŀ", MessageBoxButtons.OK);
                                    return;
                                }
                            }
                        }
                        if (addCount > 0)
                        {
                            for (int j = 0; j < addCount; j++)
                            {
                                if (infrmCheckGroup.lsvAddCheckItem.Items[j].SubItems[0].Text.ToString().Trim() == infrmCheckGroup.lsvCheckItem.Items[i].SubItems[0].Text.ToString().Trim())
                                {
                                    MessageBox.Show("��������ظ��ļ�����Ŀ��", "������Ŀ", MessageBoxButtons.OK);
                                    return;
                                }
                            }
                            if (((DataRow)infrmCheckGroup.lsvAddCheckItem.Items[0].Tag)["CHECK_CATEGORY_ID_CHR"].ToString().Trim() != ((DataRow)infrmCheckGroup.lsvCheckItem.Items[i].Tag)["CHECK_CATEGORY_ID_CHR"].ToString().Trim())
                            {
                                MessageBox.Show("������Ӳ�ͬ�������ļ�����Ŀ��", "������Ŀ", MessageBoxButtons.OK);
                                return;
                            }
                            if (((DataRow)infrmCheckGroup.lsvAddCheckItem.Items[0].Tag)["SAMPLETYPE_VCHR"].ToString().Trim() != ((DataRow)infrmCheckGroup.lsvCheckItem.Items[i].Tag)["SAMPLETYPE_VCHR"].ToString().Trim())
                            {
                                MessageBox.Show("������Ӳ�ͬ�������ļ�����Ŀ��", "������Ŀ", MessageBoxButtons.OK);
                                return;
                            }
                            ListViewItem objlsvItem = new ListViewItem();
                            objlsvItem.Text = infrmCheckGroup.lsvCheckItem.Items[i].SubItems[0].Text.ToString().Trim();
                            objlsvItem.SubItems.Add(infrmCheckGroup.lsvCheckItem.Items[i].SubItems[1].Text.ToString().Trim());
                            objlsvItem.SubItems.Add(infrmCheckGroup.lsvCheckItem.Items[i].SubItems[2].Text.ToString().Trim());
                            objlsvItem.SubItems.Add((infrmCheckGroup.lsvAddCheckItem.Items.Count).ToString().Trim());
                            objlsvItem.Tag = infrmCheckGroup.lsvCheckItem.Items[i].Tag;
                            infrmCheckGroup.lsvAddCheckItem.Items.Add(objlsvItem);
                            if (infrmCheckGroup.btnDelCheckItem.Enabled == false)
                            {
                                infrmCheckGroup.btnDelCheckItem.Enabled = true;
                            }
                        }
                        else
                        {
                            ListViewItem objlsvItem = new ListViewItem();
                            objlsvItem.Text = infrmCheckGroup.lsvCheckItem.Items[i].SubItems[0].Text.ToString().Trim();
                            objlsvItem.SubItems.Add(infrmCheckGroup.lsvCheckItem.Items[i].SubItems[1].Text.ToString().Trim());
                            objlsvItem.SubItems.Add(infrmCheckGroup.lsvCheckItem.Items[i].SubItems[2].Text.ToString().Trim());
                            objlsvItem.SubItems.Add((infrmCheckGroup.lsvAddCheckItem.Items.Count).ToString().Trim());
                            objlsvItem.Tag = infrmCheckGroup.lsvCheckItem.Items[i].Tag;
                            infrmCheckGroup.lsvAddCheckItem.Items.Add(objlsvItem);
                            if (infrmCheckGroup.btnDelCheckItem.Enabled == false)
                            {
                                infrmCheckGroup.btnDelCheckItem.Enabled = true;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        public void DelCheckItemBybtnDelCheckClick(frmCheckGroup infrmCheckGroup)
        {
            int count = infrmCheckGroup.lsvAddCheckItem.Items.Count;
            if (count > 0)
            {
                for (int i = 0; i < infrmCheckGroup.lsvAddCheckItem.Items.Count; i++)
                {
                    if (infrmCheckGroup.lsvAddCheckItem.Items[i].Checked)
                    {
                        infrmCheckGroup.lsvAddCheckItem.Items[i].Remove();
                        i = i - 1;
                    }
                }
            }
        }

        #region ��ʾѡ�е���������Ϣ
        public void m_mthTreeNodeSelectIndexChanged(frmCheckGroup infrmCheckGroup, TreeNode objTreeNode)
        {
            if (objTreeNode.Parent != null)
            {
                infrmCheckGroup.btnSaveCheckGroup.Text = "�޸�";
                clsDomainController_SampleGroupManage objDomainSampleGroup = new clsDomainController_SampleGroupManage();
                //�걾��
                if (objTreeNode.Parent.Parent != null && objTreeNode.Nodes.Count == 0 &&
                    objTreeNode.Parent != infrmCheckGroup.trvCheckGroup.Nodes[0]) //
                {
                    infrmCheckGroup.lsvAddCheckItem.Items.Clear();
                    arlSampleModelRemove.Clear();
                    arlSampleModelAdd.Clear();
                    //��ʼ��������Ϣ
                    infrmCheckGroup.rdbBseGroup.Checked = true;
                    infrmCheckGroup.btnDelCheckItem.Enabled = true;
                    clsSampleGroup_VO objSampleGroupVO = (clsSampleGroup_VO)objTreeNode.Tag;
                    string strCheckGroupID = objSampleGroupVO.strSampleGroupID;//((clsSampleGroup_VO)objTreeNode.Tag).m_strGROUPID;
                    infrmCheckGroup.txtCheckGroupNo.Text = strCheckGroupID;
                    infrmCheckGroup.txtCheckGroupName.Text = objSampleGroupVO.strSampleGroupName;
                    infrmCheckGroup.txtPyCode.Text = objSampleGroupVO.strPYCode;
                    infrmCheckGroup.txtWbCode.Text = objSampleGroupVO.strWBCode;
                    infrmCheckGroup.txtAssist01.Text = objSampleGroupVO.strAssistCode01;
                    infrmCheckGroup.txtAssist02.Text = objSampleGroupVO.strAssistCode02;
                    infrmCheckGroup.txtPrintTitle.Text = objSampleGroupVO.strPRINT_TITLE_VCHR;
                    infrmCheckGroup.cboCheckCategory.SelectedValue = objSampleGroupVO.strCheckCategoryID;
                    if (objSampleGroupVO.strIsHandWork == "0")
                    {
                        infrmCheckGroup.rdbDeviceSample.Checked = true;
                        //						infrmCheckGroup.cboDeviceModle.SelectedValue = objSampleGroupVO.strDeviceModleID;
                    }
                    else
                    {
                        infrmCheckGroup.rdbManualSample.Checked = true;
                    }
                    infrmCheckGroup.txtSampleRemark.Text = objSampleGroupVO.strRemark;
                    //��ȡ�ñ걾���µ������ͺ��б�
                    m_mthGetSampleGroupModel();
                    //��ȡ�ñ걾���µı걾�����б�
                    m_mthGetGroupSampleBySampleGroupID();
                    if (infrmCheckGroup.m_lsvGroupSampleType.Items.Count > 0)
                    {
                        infrmCheckGroup.cboSampleType.SelectedValue =
                            ((clsLisGroupSampleType_VO)infrmCheckGroup.m_lsvGroupSampleType.Items[0].Tag).m_strSAMPLE_TYPE_ID_CHR;
                    }
                    m_mthGetSampleGroupUnitBySampleGroupID();
                }
                else if (objTreeNode.Parent == infrmCheckGroup.trvCheckGroup.Nodes[0])
                {
                    m_objViewer.cboCheckCategory.SelectedValue = ((clsCheckCategory_VO)objTreeNode.Tag).m_strCheck_Category_ID;
                }
                //������
                if (objTreeNode.Parent.Index == 1 && objTreeNode.Parent.Parent == null)
                {
                    TreeNode objReportTreeNode = infrmCheckGroup.trvCheckGroup.SelectedNode;
                    clsReportGroup_VO objReportGroup = (clsReportGroup_VO)objReportTreeNode.Tag;

                    infrmCheckGroup.trvAddSubGroup.Nodes.Clear();
                    infrmCheckGroup.rdbCheckGroup.Checked = true;
                    infrmCheckGroup.txtCheckGroupNo.Text = objReportGroup.strReportGroupID;
                    infrmCheckGroup.txtCheckGroupName.Text = objReportGroup.strReportGroupName;
                    infrmCheckGroup.txtPyCode.Text = objReportGroup.strPYCode;
                    infrmCheckGroup.txtWbCode.Text = objReportGroup.strWBCode;
                    infrmCheckGroup.txtAssist01.Text = objReportGroup.strAssistCode01;
                    infrmCheckGroup.txtAssist02.Text = objReportGroup.strAssistCode02;
                    infrmCheckGroup.txtPrintTitle.Text = objReportGroup.strPrintTitle;

                    if (objReportGroup != null)
                    {
                        if (objReportGroup.objSampleGroupVO != null && objReportGroup.objSampleGroupVO.Length > 0)
                        {
                            TreeNode objAddNode = null;
                            for (int i = 0; i < objReportGroup.objSampleGroupVO.Length; i++)
                            {
                                objAddNode = infrmCheckGroup.trvAddSubGroup.Nodes.Add("(" + objReportGroup.objSampleGroupVO[i].strSampleGroupID + ")" +
                                    objReportGroup.objSampleGroupVO[i].strSampleGroupName);
                                objAddNode.Tag = objReportGroup.objSampleGroupVO[i];
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region ��ȡѡ�е�TreeView�ڵ��������ļ�����Ŀ����䵽lsvSubGroupCheckItem
        //��ȡѡ�е�TreeView�ڵ��������ļ�����Ŀ����䵽lsvSubGroupCheckItem
        //		public void getCheckItemByGroupID(frmCheckGroup infrmCheckGroup,TreeNode objTreeNode)
        //		{
        ////			long lngRes = 0;
        //			infrmCheckGroup.lsvSubGroupCheckItem.Items.Clear();
        //			if(objTreeNode.Parent != null)
        //			{
        //				clsDomainController_SampleGroupManage objDomainSampleGroup = new clsDomainController_SampleGroupManage();
        //				DataTable dtbCheckItem = null;
        //				objDomainSampleGroup.m_lngGetCheckItemBySampleGroupID(((clsSampleGroup_VO)objTreeNode.Tag).strSampleGroupID,out dtbCheckItem);
        //				if(dtbCheckItem != null)
        //				{
        //					if(dtbCheckItem.Rows.Count > 0)
        //					{
        //						for(int i=0;i<dtbCheckItem.Rows.Count;i++)
        //						{
        //							ListViewItem objlsvItem = new ListViewItem();
        //							objlsvItem.Text = dtbCheckItem.Rows[i]["CHECK_ITEM_ID_CHR"].ToString().Trim();
        //							objlsvItem.SubItems.Add(dtbCheckItem.Rows[i]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim());
        //							objlsvItem.SubItems.Add(dtbCheckItem.Rows[i]["CHECK_ITEM_NAME_VCHR"].ToString().Trim());
        //							objlsvItem.Tag = (DataRow)dtbCheckItem.Rows[i];
        //							infrmCheckGroup.lsvSubGroupCheckItem.Items.Add(objlsvItem);
        //						}
        //					}
        //				}
        //			}
        //		}
        #endregion

        #region ��TreeView�ĸ��ڵ�ѡ��ʱ�������е��ӽ��ȫ��ѡ��
        //��TreeView�ĸ��ڵ�ѡ��ʱ�������е��ӽ��ȫ��ѡ��
        public void checkAllByParentChecked(frmCheckGroup infrmCheckGroup, TreeNode objTreeNode)
        {
            if (objTreeNode.Checked)
            {
                for (int i = 0; i < objTreeNode.Nodes.Count; i++)
                {
                    objTreeNode.Nodes[i].Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < objTreeNode.Nodes.Count; i++)
                {
                    objTreeNode.Nodes[i].Checked = false;
                }
            }
        }
        #endregion

        #region ���trvAddSubGroup�����б�
        //���lsvAddSubGroup�����б�
        public void AddAllCheckSubGroup(frmCheckGroup infrmCheckGroup)
        {
            int count = infrmCheckGroup.trvCheckGroup.Nodes.Count;
            string strCheckGroupID = infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim();
            //			TreeNodeCollection nodes = infrmCheckGroup.trvSubCheckGroup.Nodes[0].Nodes;
            for (int i = 0; i < infrmCheckGroup.trvSubCheckGroup.Nodes.Count; i++)
            {
                m_mthfindNextLevel(infrmCheckGroup.trvSubCheckGroup.Nodes[i], infrmCheckGroup);
            }
        }
        #endregion

        public void m_mthfindNextLevel(TreeNode p_objTreeNode, frmCheckGroup infrmCheckGroup)
        {
            for (int i = 0; i < p_objTreeNode.Nodes.Count; i++)
            {
                if (p_objTreeNode.Nodes[i].Checked)
                {
                    if (objReportGroupDetailVOList != null && objReportGroupDetailVOList.Length > 0)
                    {
                        for (int j = 0; j < objReportGroupDetailVOList.Length; j++)
                        {
                            if (((clsSampleGroup_VO)p_objTreeNode.Nodes[i].Tag).strSampleGroupID == objReportGroupDetailVOList[j].strSampleGroupID)
                            {
                                MessageBox.Show("�ñ걾���Ѿ���������������ӣ�", "������", MessageBoxButtons.OK);
                                return;
                            }
                        }
                    }

                    if (infrmCheckGroup.trvAddSubGroup.Nodes.Count > 0)
                    {
                        foreach (TreeNode objAddedTreeNode in infrmCheckGroup.trvAddSubGroup.Nodes)
                        {
                            if (((clsSampleGroup_VO)p_objTreeNode.Nodes[i].Tag).strSampleGroupID == ((clsSampleGroup_VO)objAddedTreeNode.Tag).strSampleGroupID)
                            {
                                MessageBox.Show("�Ѿ�����˸ñ걾��", "������", MessageBoxButtons.OK);
                                return;
                            }
                        }
                        TreeNode objAddSubTreeNode = null;
                        objAddSubTreeNode = infrmCheckGroup.trvAddSubGroup.Nodes.Add("(" + ((clsSampleGroup_VO)p_objTreeNode.Nodes[i].Tag)
                            .strSampleGroupID + ")" + ((clsSampleGroup_VO)p_objTreeNode.Nodes[i].Tag).strSampleGroupName);
                        objAddSubTreeNode.Tag = ((clsSampleGroup_VO)p_objTreeNode.Nodes[i].Tag);
                    }
                    else
                    {
                        TreeNode objAddSubTreeNode = null;
                        objAddSubTreeNode = infrmCheckGroup.trvAddSubGroup.Nodes.Add("(" + ((clsSampleGroup_VO)p_objTreeNode.Nodes[i].Tag)
                            .strSampleGroupID + ")" + ((clsSampleGroup_VO)p_objTreeNode.Nodes[i].Tag).strSampleGroupName);
                        objAddSubTreeNode.Tag = ((clsSampleGroup_VO)p_objTreeNode.Nodes[i].Tag);
                    }
                }
            }
        }

        //		public void XX(com.digitalwave.iCare.gui.LIS.frmCheckGroup infrmCheckGroup,TreeNode objTreeNode)
        //		{
        //			for(int i=0;i<objTreeNode.Nodes.Count;i++)
        //			{
        //				string strHasSubGroup = ((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag).m_strHas_SubGroup;
        //				string strGroupID = ((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag).m_strGROUPID;
        //				string strGroupName = ((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag).m_strGroupName;
        //
        //				if(objTreeNode.Nodes[i].Checked)
        //				{
        //					if(objTreeNode.Checked)
        //					{
        //					}
        //					else
        //					{
        //						//���ڼ��������������������
        //						if(infrmCheckGroup.rdbCheckGroup.Checked)
        //						{
        //							//1.�жϸ����Ƿ��ǻ�����
        //							if(strHasSubGroup != "0")
        //							{
        //								MessageBox.Show("���鵥��ֻ����ӻ�������Ϊ���飡","������",MessageBoxButtons.OK);
        //								return;
        //							}
        //		
        //							//2.�ж��Ƿ��ظ���Ӹ���
        //							foreach(TreeNode objSubTreeNode in infrmCheckGroup.trvAddSubGroup.Nodes)
        //							{
        //								string strSubGroupID = ((clsCheckGroup_VO)objSubTreeNode.Tag).m_strGROUPID;
        //								string strSubGroupName = ((clsCheckGroup_VO)objSubTreeNode.Tag).m_strGroupName;
        //								if(strGroupID == strSubGroupID)
        //								{
        //									MessageBox.Show("������"+strSubGroupName+"("+strSubGroupID+")����ӣ�","������",MessageBoxButtons.OK);
        //									return;
        //								}
        //							}
        //							//3.�ж��Ƿ����������Ϊ����(�޸�)
        //							if(infrmCheckGroup.txtCheckGroupNo.Text != null)
        //							{
        //								if(strGroupID == infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim())
        //								{
        //									MessageBox.Show("�������������Ϊ����","���鵥��",MessageBoxButtons.OK);
        //									return;
        //								}
        //							}
        //							//�ж���ӵ����Ƿ�����ͬһ�����
        //							if(infrmCheckGroup.trvAddSubGroup.Nodes.Count > 0)
        //							{
        //								if(((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag).m_strCheck_Category_ID != ((clsCheckGroup_VO)infrmCheckGroup.trvAddSubGroup.Nodes[0].Tag).m_strCheck_Category_ID)
        //								{
        //									MessageBox.Show("��ӵ������Ŀ������ͬһ���������","���鵥��",MessageBoxButtons.OK);
        //									return;
        //								}
        //							}
        //							//��ӵ�trvSubGroup
        //							TreeNode objAddSubTreeNode = null;
        //							objAddSubTreeNode = infrmCheckGroup.trvAddSubGroup.Nodes.Add("("+strGroupID+")"+strGroupName);
        //							objAddSubTreeNode.Tag = ((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag);
        //							if(int.Parse(((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag).m_strHas_SubGroup) > 0)
        //								getAddSubGroupChildNode(infrmCheckGroup,objAddSubTreeNode);
        //							if(((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag).m_strHas_SubGroup == "0")
        //							{
        //								objAddSubTreeNode.Nodes.Add("");
        //							}
        //						}
        //						if(infrmCheckGroup.rdbSelfDefineGroup.Checked)
        //						{
        //							//1.�жϸ����Ƿ��ǻ�����
        //							if(strHasSubGroup == "0")
        //							{
        //								MessageBox.Show("�Զ����鲻����ӻ�������Ϊ���飡","�Զ�����",MessageBoxButtons.OK);
        //								return;
        //							}
        //							//2.�޸�ʱ�������������Ϊ����Ͳ�����Ӹ��ڵ���Ϊ����
        //							if(infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim() != "")
        //							{
        //								if(infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim() == strGroupID)
        //								{
        //									MessageBox.Show("�������������Ϊ���飡","�Զ�����",MessageBoxButtons.OK);
        //									return;
        //								}
        //								if(findChildNode(infrmCheckGroup,objTreeNode.Nodes[i],infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim()))
        //								{
        //									MessageBox.Show("������Ӹ��ڵ���Ϊ���飡","�Զ�����",MessageBoxButtons.OK);
        //									return;
        //								}
        //							}
        //							//3.�ж��Ƿ��Ѿ�����˸��������ýڵ����
        //							if(infrmCheckGroup.trvAddSubGroup.Nodes != null)
        //							{
        //								for(int k=0;k<infrmCheckGroup.trvAddSubGroup.Nodes.Count;k++)
        //								{
        //									string strSubGroupID = ((clsCheckGroup_VO)infrmCheckGroup.trvAddSubGroup.Nodes[k].Tag).m_strGROUPID;
        //									string strSubGroupName = ((clsCheckGroup_VO)infrmCheckGroup.trvAddSubGroup.Nodes[k].Tag).m_strGroupName;
        //									if(strGroupID == strSubGroupID)
        //									{
        //										MessageBox.Show("������"+strSubGroupName+"("+strSubGroupID+")����ӣ�","�Զ�����",MessageBoxButtons.OK);
        //										return;
        //									}
        //									if(findChildNode(infrmCheckGroup,infrmCheckGroup.trvAddSubGroup.Nodes[k],strGroupID))
        //									{
        //										MessageBox.Show("������Ӹ��ڵ���Ϊ���飡","�Զ�����",MessageBoxButtons.OK);
        //										return;
        //									}
        //									//�жϽ�Ҫ��ӵĽڵ��Ƿ�������Ѿ���ӵĽڵ���Ϊ��
        //									if(findChildNode(infrmCheckGroup,objTreeNode.Nodes[i],strSubGroupID))
        //									{
        //										MessageBox.Show("����������Ѿ�����ӣ�","�Զ�����",MessageBoxButtons.OK);
        //										return;
        //									}
        //									//�ж���ӵĽڵ㼰���ӽڵ��Ƿ��ظ����
        //									if(findChildNode(infrmCheckGroup,infrmCheckGroup.trvAddSubGroup.Nodes[k],objTreeNode.Nodes[i]))
        //									{
        //										MessageBox.Show("����������ظ�����ӣ�","�Զ�����",MessageBoxButtons.OK);
        //										return;
        //									}
        //								}
        //							}
        //							//��ӵ�trvSubGroup
        //							TreeNode objAddSubTreeNode = null;
        //							objAddSubTreeNode = infrmCheckGroup.trvAddSubGroup.Nodes.Add("("+strGroupID+")"+strGroupName);
        //							objAddSubTreeNode.Tag = ((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag);
        //							if(int.Parse(((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag).m_strHas_SubGroup) > 0)
        //								getAddSubGroupChildNode(infrmCheckGroup,objAddSubTreeNode);
        //						}
        //					}
        //				}
        //				if(int.Parse(strHasSubGroup) > 0)
        //				{
        //					XX(infrmCheckGroup,objTreeNode.Nodes[i]);
        //				}
        //			}
        //		}

        //�ж�ĳһ�ڵ��Ƿ�����һ�ڵ������
        public bool findChildNode(com.digitalwave.iCare.gui.LIS.frmCheckGroup infrmCheckGroup, TreeNode objSrcNode, TreeNode objDestNode)
        {
            bool bolfind = false;
            for (int j = 0; j < objDestNode.Nodes.Count; j++)
            {
                string strDesGroupID = ((clsCheckGroup_VO)objDestNode.Nodes[j].Tag).m_strGROUPID;
                string strDesHasSubGroup = ((clsCheckGroup_VO)objDestNode.Nodes[j].Tag).m_strHas_SubGroup;
                for (int i = 0; i < objSrcNode.Nodes.Count; i++)
                {
                    string strGroupID = ((clsCheckGroup_VO)objSrcNode.Nodes[i].Tag).m_strGROUPID;
                    string strSrcHasSubGroup = ((clsCheckGroup_VO)objSrcNode.Nodes[i].Tag).m_strHas_SubGroup;

                    if (strGroupID == strDesGroupID)
                    {
                        bolfind = true;
                        return bolfind;
                    }

                    if (int.Parse(strSrcHasSubGroup) > 0)
                    {
                        bolfind = findChildNode(infrmCheckGroup, objSrcNode.Nodes[i], objDestNode);
                        if (bolfind)
                        {
                            return bolfind;
                        }
                    }
                }
                if (int.Parse(strDesHasSubGroup) > 0)
                {
                    bolfind = findChildNode(infrmCheckGroup, objSrcNode, objDestNode.Nodes[j]);
                    if (bolfind)
                    {
                        return bolfind;
                    }
                }
            }
            return bolfind;
        }

        //�ж��Զ�����ʱ�Ƿ�����˸��ڵ���Ϊ����
        public bool findChildNode(com.digitalwave.iCare.gui.LIS.frmCheckGroup infrmCheckGroup, TreeNode objTreeNode, string strCheckGroupID)
        {
            bool bolfind = false;
            for (int i = 0; i < objTreeNode.Nodes.Count; i++)
            {
                string strHasSubGroup = ((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag).m_strHas_SubGroup;
                string strGroupID = ((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag).m_strGROUPID;
                string strGroupName = ((clsCheckGroup_VO)objTreeNode.Nodes[i].Tag).m_strGroupName;

                if (strGroupID == strCheckGroupID)
                {
                    bolfind = true;
                    return bolfind;
                }

                if (int.Parse(strHasSubGroup) > 0)
                {
                    findChildNode(infrmCheckGroup, objTreeNode.Nodes[i], strCheckGroupID);
                }
            }
            return bolfind;
        }

        #region ɾ��trvAddSubGroup�����б�
        //ɾ��trvAddSubGroup�����б�
        public void DelAllCheckedSubGroup(frmCheckGroup infrmCheckGroup)
        {
            int count = infrmCheckGroup.trvAddSubGroup.Nodes.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (infrmCheckGroup.trvAddSubGroup.Nodes[i].Checked)
                    {
                        infrmCheckGroup.trvAddSubGroup.Nodes[i].Remove();
                        count--;
                        i--;
                    }
                }
            }
        }
        #endregion

        #region ����
        //����������
        public long AddNewCheckGroup(frmCheckGroup infrmCheckGroup)
        {
            long lngRes = 0;
            clsDomainController_ReportManage objDomainReportGroup = new clsDomainController_ReportManage();
            clsDomainController_SampleGroupManage objDomainSampleGroup = new clsDomainController_SampleGroupManage();
            if (infrmCheckGroup.txtCheckGroupName.Text.ToString().Trim() == "")
            {
                MessageBox.Show("�������������", "", MessageBoxButtons.OK);
                return -1;
            }

            #region �걾��
            //�걾��
            if (infrmCheckGroup.rdbBseGroup.Checked)
            {
                if (infrmCheckGroup.m_lsvGroupSampleType.Items.Count <= 0)
                {
                    MessageBox.Show("����ӱ걾����������ͣ�", "", MessageBoxButtons.OK);
                    return -1;
                }
                if (infrmCheckGroup.m_trvAddSampleGroup.Nodes.Count > 0)
                {
                    if (infrmCheckGroup.rdbDeviceSample.Checked)
                    {
                        if (infrmCheckGroup.m_lsvDeviceModel.Items.Count <= 0)
                        {
                            MessageBox.Show("����������ͺ��б�", "", MessageBoxButtons.OK);
                            return -1;
                        }
                    }
                    //1.���浽t_aid_lis_sample_group
                    clsSampleGroup_VO objSampleGroup = new clsSampleGroup_VO();
                    if (infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim() != "")
                    {
                        objSampleGroup.strSampleGroupID = infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim();
                    }
                    objSampleGroup.strSampleGroupName = infrmCheckGroup.txtCheckGroupName.Text.ToString().Trim();
                    objSampleGroup.strCheckCategoryID = infrmCheckGroup.cboCheckCategory.SelectedValue.ToString().Trim();
                    //						((DataRow)infrmCheckGroup.lsvCheckItem.Items[0].Tag)["CHECK_CATEGORY_ID_CHR"].ToString().Trim();
                    //					objSampleGroup.strSampleTypeID = ((DataRow)infrmCheckGroup.lsvCheckItem.Items[0].Tag)["SAMPLETYPE_VCHR"].ToString().Trim();
                    objSampleGroup.strPYCode = infrmCheckGroup.txtPyCode.Text.ToString().Trim();
                    objSampleGroup.strWBCode = infrmCheckGroup.txtWbCode.Text.ToString().Trim();
                    objSampleGroup.strRemark = infrmCheckGroup.txtSampleRemark.Text.ToString().Trim();
                    objSampleGroup.strAssistCode01 = infrmCheckGroup.txtAssist01.Text.ToString().Trim();
                    objSampleGroup.strAssistCode02 = infrmCheckGroup.txtAssist02.Text.ToString().Trim();
                    objSampleGroup.strPRINT_TITLE_VCHR = infrmCheckGroup.txtPrintTitle.Text.ToString().Trim();
                    if (infrmCheckGroup.cboDeviceModle.Items.Count > 0)
                    {
                        if (infrmCheckGroup.rdbDeviceSample.Checked)
                        {
                            //							objSampleGroup.strDeviceModleID = infrmCheckGroup.cboDeviceModle.SelectedValue.ToString().Trim();
                            objSampleGroup.strIsHandWork = "0";
                        }
                        else
                        {
                            objSampleGroup.strIsHandWork = "1";
                        }
                    }
                    //2.���浽t_aid_lis_sample_group_detail
                    //					clsSampleGroupDetail_VO[] objSampleGroupDetailVO = null;
                    //					objSampleGroupDetailVO = new clsSampleGroupDetail_VO[infrmCheckGroup.lsvAddCheckItem.Items.Count];
                    //					for(int i=0;i<infrmCheckGroup.lsvAddCheckItem.Items.Count;i++)
                    //					{
                    //						objSampleGroupDetailVO[i] = new clsSampleGroupDetail_VO();
                    //						objSampleGroupDetailVO[i].strCheckItemID = infrmCheckGroup.lsvAddCheckItem.Items[i].SubItems[0].Text.ToString().Trim();
                    //						objSampleGroupDetailVO[i].strPrintSeq = i.ToString().Trim();
                    //					}
                    //2.���浽t_aid_lis_sample_group_unit
                    clsLisSampleGroupUnit_VO[] objSampleGroupUnitArr = null;
                    objSampleGroupUnitArr = new clsLisSampleGroupUnit_VO[m_objViewer.m_trvAddSampleGroup.Nodes.Count];
                    for (int i = 0; i < m_objViewer.m_trvAddSampleGroup.Nodes.Count; i++)
                    {
                        objSampleGroupUnitArr[i] = new clsLisSampleGroupUnit_VO();
                        objSampleGroupUnitArr[i].m_strAPPLY_UNIT_ID_CHR =
                            ((clsLisSampleGroupUnit_VO)m_objViewer.m_trvAddSampleGroup.Nodes[i].Tag).m_strAPPLY_UNIT_ID_CHR;
                        objSampleGroupUnitArr[i].m_strSAMPLE_GROUP_ID_CHR =
                            ((clsLisSampleGroupUnit_VO)m_objViewer.m_trvAddSampleGroup.Nodes[i].Tag).m_strSAMPLE_GROUP_ID_CHR;
                    }
                    //3.����t_aid_lis_apply_unit_detail�Ĵ�ӡ˳��
                    clsApplUnitDetail_VO[] objApplUnitArr = null;
                    objApplUnitArr = new clsApplUnitDetail_VO[m_objViewer.m_lsvApplUnitItem.Items.Count + m_arlSameApplyUnitItem.Count];
                    for (int i = 0; i < m_objViewer.m_lsvApplUnitItem.Items.Count; i++)
                    {
                        objApplUnitArr[i] = new clsApplUnitDetail_VO();
                        objApplUnitArr[i].strApplUnitID =
                            ((DataRow)m_objViewer.m_lsvApplUnitItem.Items[i].Tag)["APPLY_UNIT_ID_CHR"].ToString().Trim();
                        objApplUnitArr[i].strCheckItemID =
                            ((DataRow)m_objViewer.m_lsvApplUnitItem.Items[i].Tag)["CHECK_ITEM_ID_CHR"].ToString().Trim();
                        objApplUnitArr[i].intPrintSeq = i;
                    }
                    if (m_arlSameApplyUnitItem.Count > 0)
                    {
                        for (int i = 0; i < m_arlSameApplyUnitItem.Count; i++)
                        {
                            for (int j = 0; j < m_objViewer.m_lsvApplUnitItem.Items.Count; j++)
                            {
                                if (objApplUnitArr[j].strCheckItemID == ((clsApplUnitDetail_VO)m_arlSameApplyUnitItem[i]).strCheckItemID)
                                {
                                    objApplUnitArr[i + m_objViewer.m_lsvApplUnitItem.Items.Count] = (clsApplUnitDetail_VO)m_arlSameApplyUnitItem[i];
                                    objApplUnitArr[i + m_objViewer.m_lsvApplUnitItem.Items.Count].intPrintSeq = objApplUnitArr[j].intPrintSeq;
                                }
                            }
                        }
                    }
                    //����
                    lngRes = objDomainSampleGroup.m_lngAddSampleGroup(ref objSampleGroup, ref objSampleGroupUnitArr, objApplUnitArr,
                        arlSampleModelAdd, arlSampleModelRemove, m_arlGroupSampleAdd, m_arlGroupSampleRemove);
                    if (lngRes > 0)
                    {
                        //						MessageBox.Show("�걾����ӳɹ�!","�걾��",MessageBoxButtons.OK);
                        arlSampleModelRemove.Clear();
                        arlSampleModelAdd.Clear();
                        m_arlGroupSampleAdd.Clear();
                        m_arlGroupSampleRemove.Clear();
                        m_arlSampleModelRaw.Clear();
                        m_mthGetUsedApplUnit();
                        refreshAllCheckGroupANDTrvCheckGroup(infrmCheckGroup);
                        //ˢ�����еı걾���뱨������ϸ
                        refreshSampleGroupAndReportGroupDetail();
                        m_mthFindSampleTreeNodeByCheckGroupNO(infrmCheckGroup, objSampleGroup.strSampleGroupID);
                    }
                }
                else
                {
                    MessageBox.Show("���뵥Ԫ����Ϊ��!", "�걾��", MessageBoxButtons.OK);
                    return -1;
                }
            }
            #endregion

            #region ������
            //������
            if (infrmCheckGroup.rdbCheckGroup.Checked)
            {
                if (infrmCheckGroup.trvAddSubGroup.Nodes.Count > 0)
                {
                    //1.���浽t_aid_lis_report_group
                    clsReportGroup_VO objReportGroupVO = new clsReportGroup_VO();
                    if (infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim() != "")
                    {
                        objReportGroupVO.strReportGroupID = infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim();
                    }
                    if (infrmCheckGroup.txtCheckGroupName.Text.ToString().Trim() == "")
                    {
                        MessageBox.Show("�����뱨�������ƣ�", "������", MessageBoxButtons.OK);
                        return -1;
                    }
                    objReportGroupVO.strReportGroupName = infrmCheckGroup.txtCheckGroupName.Text.ToString().Trim();
                    objReportGroupVO.strPYCode = infrmCheckGroup.txtPyCode.Text.ToString().Trim();
                    objReportGroupVO.strWBCode = infrmCheckGroup.txtWbCode.Text.ToString().Trim();
                    objReportGroupVO.strPrintCategoryID = infrmCheckGroup.cboPrintCategory.SelectedValue.ToString().Trim();
                    objReportGroupVO.strAssistCode01 = infrmCheckGroup.txtAssist01.Text.ToString().Trim();
                    objReportGroupVO.strAssistCode02 = infrmCheckGroup.txtAssist02.Text.ToString().Trim();
                    objReportGroupVO.strPrintTitle = infrmCheckGroup.txtPrintTitle.Text.ToString().Trim();
                    //2.���浽t_aid_lis_report_group_detail
                    clsReportGroupDetail_VO[] objReportGroupDetailVO = new clsReportGroupDetail_VO[infrmCheckGroup.trvAddSubGroup.Nodes.Count];
                    for (int i = 0; i < infrmCheckGroup.trvAddSubGroup.Nodes.Count; i++)
                    {
                        objReportGroupDetailVO[i] = new clsReportGroupDetail_VO();
                        objReportGroupDetailVO[i].strSampleGroupID = ((clsSampleGroup_VO)infrmCheckGroup.trvAddSubGroup.Nodes[i].Tag).strSampleGroupID;
                        objReportGroupDetailVO[i].strPrintSeq = i.ToString().Trim();
                    }
                    lngRes = objDomainReportGroup.m_lngAddReportGroupAndDetail(ref objReportGroupVO, ref objReportGroupDetailVO);
                    if (lngRes > 0)
                    {
                        refreshAllCheckGroupANDTrvCheckGroup(infrmCheckGroup);
                        //ˢ�����еı걾���뱨������ϸ
                        refreshSampleGroupAndReportGroupDetail();
                        ResetAll(infrmCheckGroup);
                        m_mthFindReportTreeNodeByCheckGroupNO(infrmCheckGroup, objReportGroupVO.strReportGroupID);
                    }
                }
                else
                {
                    MessageBox.Show("����ӱ걾�飡", "������", MessageBoxButtons.OK);
                    return -1;
                }
            }
            #endregion

            return lngRes;
        }
        #endregion

        #region ɾ��������
        //ɾ��������
        public long DelCheckGroup(frmCheckGroup infrmCheckGroup)
        {
            long lngRes = 0;
            clsDomainController_ReportManage objDomainReportGroup = new clsDomainController_ReportManage();
            clsDomainController_SampleGroupManage objDomainSampleGroup = new clsDomainController_SampleGroupManage();
            //�걾��
            if (infrmCheckGroup.trvCheckGroup.SelectedNode.Parent.Index == 0 || infrmCheckGroup.trvCheckGroup.SelectedNode.Parent.Parent != null) //
            {
                clsReportGroupDetail_VO[] objReportGroupVOList = null;
                lngRes = objDomainReportGroup.m_lngGetAllReportGroupDetail(out objReportGroupVOList);
                if (lngRes > 0 && objReportGroupVOList != null)
                {
                    for (int i = 0; i < objReportGroupVOList.Length; i++)
                    {
                        if (objReportGroupVOList[i].strSampleGroupID == infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim())
                        {
                            MessageBox.Show("����ɾ�������ñ걾��ı����飡", "�걾��ɾ��", MessageBoxButtons.OK);
                            return -1;
                        }
                    }
                }
                lngRes = objDomainSampleGroup.m_lngDelSampleGroupAndDetail(infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim());
                if (lngRes > 0)
                {
                    m_mthGetUsedApplUnit();
                    this.refreshAllCheckGroupANDTrvCheckGroup(infrmCheckGroup);
                    this.refreshSampleGroupAndReportGroupDetail();
                }
            }
            //������
            else if (infrmCheckGroup.trvCheckGroup.SelectedNode.Parent.Index == 1 && infrmCheckGroup.trvCheckGroup.SelectedNode.Parent.Parent == null)
            {
                MessageBox.Show("������ɾ�������飡�������Ա��ϵ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
                //lngRes = objDomainReportGroup.m_lngDelReportGroupAndDetail(infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim());
                //if(lngRes > 0)
                //{
                //    this.refreshAllCheckGroupANDTrvCheckGroup(infrmCheckGroup);
                //    this.refreshSampleGroupAndReportGroupDetail();
                //}
            }
            return lngRes;
        }
        #endregion

        //ɾ�����������صı����Ϣ
        public long DelRelatedCheckGroup(frmCheckGroup infrmCheckGroup)
        {
            long lngRes = 0;
            string strCheckGroupID = infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim();
            if (strCheckGroupID != "")
            {
                lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelCheckGroupRelatedInfo(strCheckGroupID);
            }
            return lngRes;
        }

        //�޸ļ�����
        public long UpdCheckGroup(frmCheckGroup infrmCheckGroup)
        {
            long lngRes = 0;
            clsDomainController_ReportManage objDomainReportGroup = new clsDomainController_ReportManage();
            clsDomainController_SampleGroupManage objDomainSampleGroup = new clsDomainController_SampleGroupManage();
            string strCheckGroupNO = "";
            //�걾��
            if (infrmCheckGroup.trvCheckGroup.SelectedNode.Parent != null && infrmCheckGroup.trvCheckGroup.SelectedNode.Nodes.Count == 0
                && infrmCheckGroup.trvCheckGroup.SelectedNode.Parent != infrmCheckGroup.trvCheckGroup.Nodes[1]) //
            {
                if (infrmCheckGroup.m_trvAddSampleGroup.Nodes.Count == 0)
                {
                    MessageBox.Show("����������뵥Ԫ��", "�걾��", MessageBoxButtons.OK);
                    return -1;
                }
                strCheckGroupNO = infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim();
                //				//1.ɾ��ԭ�еı걾�����ϸ
                //				lngRes = objDomainSampleGroup.m_lngDelSampleGroupDetail(strCheckGroupNO);
                //				if(lngRes > 0)
                //				{
                //					//2.�����µı걾��
                lngRes = this.AddNewCheckGroup(infrmCheckGroup);
                //				}
                if (lngRes > 0)
                {
                    m_mthFindSampleTreeNodeByCheckGroupNO(infrmCheckGroup, strCheckGroupNO);
                }
            }
            //������
            if ((infrmCheckGroup.trvCheckGroup.SelectedNode.Parent != null && infrmCheckGroup.trvCheckGroup.SelectedNode.Nodes.Count > 0)
                || infrmCheckGroup.trvCheckGroup.SelectedNode.Parent == infrmCheckGroup.trvCheckGroup.Nodes[1])
            {
                strCheckGroupNO = infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim();
                //1.ɾ��ԭ�еı��������ϸ
                //				lngRes = objDomainReportGroup.m_lngDelReportGroupDetail(infrmCheckGroup.txtCheckGroupNo.Text.ToString().Trim());
                //				if(lngRes > 0)
                //				{
                //2.�����µı�����
                lngRes = this.AddNewCheckGroup(infrmCheckGroup);
                //				}
                if (lngRes > 0)
                {
                    m_mthFindReportTreeNodeByCheckGroupNO(infrmCheckGroup, strCheckGroupNO);
                }
            }
            return lngRes;
        }

        /// <summary>
        /// ���ݱ걾��ID��ѯ�걾�ڵ�
        /// </summary>
        /// <param name="infrmCheckGroup"></param>
        /// <param name="p_strCheckGroupNO"></param>
        private void m_mthFindSampleTreeNodeByCheckGroupNO(frmCheckGroup infrmCheckGroup, string p_strCheckGroupNO)
        {
            for (int j = 0; j < infrmCheckGroup.trvCheckGroup.Nodes[0].Nodes.Count; j++)
            {
                for (int i = 0; i < infrmCheckGroup.trvCheckGroup.Nodes[0].Nodes[j].Nodes.Count; i++)
                {
                    clsSampleGroup_VO objCheckGroupVO = (clsSampleGroup_VO)infrmCheckGroup.trvCheckGroup.Nodes[0].Nodes[j].Nodes[i].Tag;
                    if (objCheckGroupVO.strSampleGroupID == p_strCheckGroupNO)
                    {
                        infrmCheckGroup.trvCheckGroup.SelectedNode = infrmCheckGroup.trvCheckGroup.Nodes[0].Nodes[j].Nodes[i];
                        m_mthTreeNodeSelectIndexChanged(infrmCheckGroup, infrmCheckGroup.trvCheckGroup.Nodes[0].Nodes[j].Nodes[i]);
                        infrmCheckGroup.trvCheckGroup.Focus();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// ���ݱ�����ID��ѯ������ڵ�
        /// </summary>
        /// <param name="infrmCheckGroup"></param>
        /// <param name="p_strCheckGroupNO"></param>
        private void m_mthFindReportTreeNodeByCheckGroupNO(frmCheckGroup infrmCheckGroup, string p_strCheckGroupNO)
        {
            for (int i = 0; i < infrmCheckGroup.trvCheckGroup.Nodes[1].Nodes.Count; i++)
            {
                clsReportGroup_VO objCheckGroupVO = (clsReportGroup_VO)infrmCheckGroup.trvCheckGroup.Nodes[1].Nodes[i].Tag;
                if (objCheckGroupVO.strReportGroupID == p_strCheckGroupNO)
                {
                    infrmCheckGroup.trvCheckGroup.SelectedNode = infrmCheckGroup.trvCheckGroup.Nodes[1].Nodes[i];
                    m_mthTreeNodeSelectIndexChanged(infrmCheckGroup, infrmCheckGroup.trvCheckGroup.Nodes[1].Nodes[i]);
                    infrmCheckGroup.trvCheckGroup.Focus();
                    break;
                }
            }
        }

        private void ResetAll(frmCheckGroup infrmCheckGroup)
        {
            infrmCheckGroup.txtCheckGroupName.Text = "";
            infrmCheckGroup.txtCheckGroupNo.Text = "";
            infrmCheckGroup.txtPyCode.Text = "";
            infrmCheckGroup.txtWbCode.Text = "";
            //			infrmCheckGroup.chkBodyCheck.Checked = false;
            //			infrmCheckGroup.chkNoFood.Checked = false;
            //			infrmCheckGroup.chkReservation.Checked = false;
            infrmCheckGroup.lsvAddCheckItem.Items.Clear();
            //			infrmCheckGroup.lsvAddSubGroup.Items.Clear();
            //			infrmCheckGroup.lsvAllCheckItemDetail.Items.Clear();
            //			infrmCheckGroup.lsvSampleInfo.Items.Clear();
            if (infrmCheckGroup.lsvCheckItem.Items.Count > 0)
            {
                for (int i = 0; i < infrmCheckGroup.lsvCheckItem.Items.Count; i++)
                {
                    if (infrmCheckGroup.lsvCheckItem.Items[i].Checked)
                    {
                        infrmCheckGroup.lsvCheckItem.Items[i].Checked = false;
                    }
                }
            }
            TreeNodeCollection nodes = infrmCheckGroup.trvSubCheckGroup.Nodes[0].Nodes;
            foreach (TreeNode objTreeNode in nodes)
            {
                if (objTreeNode.Checked)
                {
                    objTreeNode.Checked = false;
                }
            }
        }

        public void getTrvAddSubGroupCheckItem(com.digitalwave.iCare.gui.LIS.frmCheckGroup infrmCheckGroup, TreeNode objTreeNode)
        {
            if (((clsCheckGroup_VO)objTreeNode.Tag).m_strHas_SubGroup == "0")
            {
                try
                {
                    string strGroupID = ((clsCheckGroup_VO)objTreeNode.Tag).m_strGROUPID;
                    weCare.Core.Entity.clsCheckItem_VO[] objCheckItemVOList = null;
                    proxy.Service.m_lngGetCheckItemByNoSubCheckGroupID(strGroupID, out objCheckItemVOList);

                    if (objCheckItemVOList != null)
                    {
                        if (objCheckItemVOList.Length > 0)
                        {
                            objTreeNode.Nodes.Clear();
                            TreeNode CurNode = null;
                            for (int i = 0; i < objCheckItemVOList.Length; i++)
                            {
                                CurNode = objTreeNode.Nodes.Add("(" + objCheckItemVOList[i].m_strCheck_Item_English_Name + ")" + objCheckItemVOList[i].m_strCheck_Item_Name);
                                CurNode.Tag = objCheckItemVOList[i];
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
