using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using com.digitalwave.Utility;//Utility.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// ���뵥Ԫ���Զ���������ά����clsController_AppUserGroup��
    /// </summary>
    public class clsController_AppUserGroup : com.digitalwave.GUI_Base.clsController_Base
    {

        #region ˽�г�Ա

        //���ڼ�¼�����Ѿ����Ϊ���뵥Ԫ�ļ�����Ŀ
        clsApplUnitDetail_VO[] objApplUnitDetailVOList = null;
        List<clsApplUnitDetail_VO> m_arlAddDetail = new List<clsApplUnitDetail_VO>();
        List<clsApplUnitDetail_VO> m_arlRemoveDetail = new List<clsApplUnitDetail_VO>();

        #endregion

        #region ���캯��
        public clsController_AppUserGroup()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region ����ҳ��ʱ�ĳ�ʼ����Ϣ

        public long GetInitInfo(frmApplUserGroup infrmApplUserGroup)//,out DataTable dtbAllCheckGroup,out DataTable dtbAllCheckCategory)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            //��ʼ���������б�
            refreshAllCheckGroupANDTrvCheckGroup(infrmApplUserGroup);
            //��ʼ����������б�
            DataTable dtbAllCheckCategory = null;
            lngRes = (new weCare.Proxy.ProxyLis()).Service.m_lngGetAllCheckCategory(out dtbAllCheckCategory);
            if (dtbAllCheckCategory.Rows.Count > 0)
            {
                infrmApplUserGroup.cboCheckCategory.DataSource = dtbAllCheckCategory;
                infrmApplUserGroup.cboCheckCategory.DisplayMember = "CHECK_CATEGORY_DESC_VCHR";
                infrmApplUserGroup.cboCheckCategory.ValueMember = "CHECK_CATEGORY_ID_CHR";
            }

            infrmApplUserGroup.rdbApplUnit.Checked = true;

            //��ʼ���������뵥Ԫ������Ŀ��ϸ
            refreshApplUnitDetail();
            return lngRes;
        }

        //ˢ���������뵥Ԫ������Ŀ��ϸ
        private void refreshApplUnitDetail()
        {
            long lngRes = 0;
            clsDomainController_LisCheckGroupManage objDomainControllerCheckGroup = new clsDomainController_LisCheckGroupManage();
            lngRes = objDomainControllerCheckGroup.m_lngGetAllApplUnitDetail(out objApplUnitDetailVOList);
        }

        #endregion

        #region �������뵥Ԫ���û��Զ���������TreeView

        //ˢ�����еļ������б�
        public void refreshAllCheckGroupANDTrvCheckGroup(frmApplUserGroup infrmApplUserGroup)
        {
            long lngRes = 0;
            clsDomainController_AppGroupManage objDomainControllerCheckGroup = new clsDomainController_AppGroupManage();
            clsDomainController_ApplyUnitManage objDomainApplyUnit = new clsDomainController_ApplyUnitManage();
            //��ʼ�����뵥Ԫ�б�
            infrmApplUserGroup.trvCheckGroup.Nodes[0].Nodes.Clear();
            infrmApplUserGroup.trvSubCheckGroup.Nodes[0].Nodes.Clear();
            //���뵥Ԫ����
            clsDomainController_CheckItemManage objCheckCategoryManage = new clsDomainController_CheckItemManage();
            clsCheckCategory_VO[] objCheckCategoryArr = null;
            lngRes = objCheckCategoryManage.m_lngGetCheckCategory(out objCheckCategoryArr);
            if (lngRes > 0 && objCheckCategoryArr != null && objCheckCategoryArr.Length > 0)
            {
                for (int i = 0; i < objCheckCategoryArr.Length; i++)
                {
                    TreeNode objCheckCategoryTreeNode = new TreeNode();
                    objCheckCategoryTreeNode.Text = objCheckCategoryArr[i].m_strCheck_Category_Name;
                    objCheckCategoryTreeNode.Tag = objCheckCategoryArr[i];
                    infrmApplUserGroup.trvCheckGroup.Nodes[0].Nodes.Add(objCheckCategoryTreeNode);
                    TreeNode objSubTreeNode = (TreeNode)objCheckCategoryTreeNode.Clone();
                    infrmApplUserGroup.trvSubCheckGroup.Nodes[0].Nodes.Add(objSubTreeNode);
                }
            }
            else
            {
                return;
            }
            clsApplUnit_VO[] objApplUnitVOList = null;
            clsApplUserGroup_VO[] objUserGroupVOList = null;
            TreeNode objTreeNode = null;
            lngRes = objDomainApplyUnit.m_lngGetAllApplUnit(out objApplUnitVOList);

            if (lngRes > 0 && objApplUnitVOList != null)
            {
                if (objApplUnitVOList.Length > 0)
                {
                    for (int i = 0; i < objApplUnitVOList.Length; i++)
                    {
                        for (int j = 0; j < infrmApplUserGroup.trvCheckGroup.Nodes[0].Nodes.Count; j++)
                        {
                            if (((clsCheckCategory_VO)infrmApplUserGroup.trvCheckGroup.Nodes[0].Nodes[j].Tag).m_strCheck_Category_ID ==
                                objApplUnitVOList[i].strCheckCategoryID)
                            {
                                objTreeNode = infrmApplUserGroup.trvCheckGroup.Nodes[0].Nodes[j].Nodes.Add("(" + objApplUnitVOList[i].strApplUnitID + ")" +
                                    objApplUnitVOList[i].strApplUnitName);
                                objTreeNode.Tag = objApplUnitVOList[i];
                                break;
                            }
                        }
                    }
                }
            }
            //��ʼ���û��Զ�������б�
            clsApplUserGroupRelation_VO[] objApplUserGroupRelationArr = null;
            lngRes = objDomainControllerCheckGroup.m_lngGetAllApplUserGroupRelation(out objApplUserGroupRelationArr);

            infrmApplUserGroup.trvCheckGroup.Nodes[1].Nodes.Clear();
            lngRes = objDomainControllerCheckGroup.m_lngGetAllUserGroup(out objUserGroupVOList);
            if (lngRes > 0 && objUserGroupVOList != null)
            {
                if (objUserGroupVOList.Length > 0)
                {
                    for (int i = 0; i < objUserGroupVOList.Length; i++)
                    {
                        bool blnHasParent = false;
                        if (objApplUserGroupRelationArr != null)
                        {
                            for (int j = 0; j < objApplUserGroupRelationArr.Length; j++)
                            {
                                if (objApplUserGroupRelationArr[j].strChildUserGroupID == objUserGroupVOList[i].strUserGroupID)
                                {
                                    blnHasParent = true;
                                }
                            }
                        }
                        if (!blnHasParent)
                        {
                            objTreeNode = infrmApplUserGroup.trvCheckGroup.Nodes[1].Nodes.Add("(" + objUserGroupVOList[i].strUserGroupID + ")" + objUserGroupVOList[i].strUserGroupName);
                            objTreeNode.Tag = objUserGroupVOList[i];
                            if (int.Parse(objUserGroupVOList[i].strHasChildGroup) > 0)
                            {
                                //������һ����û�������������뵥Ԫ
                                getChildUserGroupAndApplUnit(infrmApplUserGroup, objTreeNode);
                            }
                            else
                            {
                                //���Ҹ����µ����뵥Ԫ
                                getChildApplUnit(infrmApplUserGroup, objTreeNode);
                            }
                        }
                    }
                }
            }
            //trvSubGroup��ʼ��
            //��ʼ�����뵥Ԫ�б�
            if (lngRes > 0 && objApplUnitVOList != null)
            {
                if (objApplUnitVOList.Length > 0)
                {
                    for (int i = 0; i < objApplUnitVOList.Length; i++)
                    {
                        for (int j = 0; j < infrmApplUserGroup.trvSubCheckGroup.Nodes[0].Nodes.Count; j++)
                        {
                            if (((clsCheckCategory_VO)infrmApplUserGroup.trvSubCheckGroup.Nodes[0].Nodes[j].Tag).m_strCheck_Category_ID ==
                                objApplUnitVOList[i].strCheckCategoryID)
                            {
                                objTreeNode = infrmApplUserGroup.trvSubCheckGroup.Nodes[0].Nodes[j].Nodes.Add("(" + objApplUnitVOList[i].strApplUnitID + ")"
                                    + objApplUnitVOList[i].strApplUnitName);
                                objTreeNode.Tag = objApplUnitVOList[i];
                                break;
                            }
                        }
                    }
                }
            }
            //��ʼ���û��Զ�������б�
            infrmApplUserGroup.trvSubCheckGroup.Nodes[1].Nodes.Clear();
            //			lngRes = objDomainControllerCheckGroup.m_lngGetAllUserGroup(out objUserGroupVOList);
            if (lngRes > 0 && objUserGroupVOList != null)
            {
                if (objUserGroupVOList.Length > 0)
                {
                    if (lngRes > 0)
                    {
                        for (int i = 0; i < objUserGroupVOList.Length; i++)
                        {
                            bool blnHasParent = false;
                            if (objApplUserGroupRelationArr != null)
                            {
                                for (int j = 0; j < objApplUserGroupRelationArr.Length; j++)
                                {
                                    if (objApplUserGroupRelationArr[j].strChildUserGroupID == objUserGroupVOList[i].strUserGroupID)
                                    {
                                        blnHasParent = true;
                                    }
                                }
                            }
                            if (!blnHasParent)
                            {
                                objTreeNode = infrmApplUserGroup.trvSubCheckGroup.Nodes[1].Nodes.Add("(" + objUserGroupVOList[i].strUserGroupID + ")" + objUserGroupVOList[i].strUserGroupName);
                                objTreeNode.Tag = objUserGroupVOList[i];
                                if (int.Parse(objUserGroupVOList[i].strHasChildGroup) > 0)
                                {
                                    //������һ����û�������������뵥Ԫ
                                    getChildUserGroupAndApplUnit(infrmApplUserGroup, objTreeNode);
                                }
                                else
                                {
                                    //���Ҹ����µ����뵥Ԫ
                                    getChildApplUnit(infrmApplUserGroup, objTreeNode);
                                }
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        //������һ����û�������������뵥Ԫ
        private void getChildUserGroupAndApplUnit(frmApplUserGroup infrmApplUserGroup, TreeNode objTreeNode)
        {
            long lngRes = 0;
            TreeNode objChildTreeNode = null;
            clsDomainController_LisCheckGroupManage objDomainControllerCheckGroup = new clsDomainController_LisCheckGroupManage();
            string strUserGroupID = ((clsApplUserGroup_VO)objTreeNode.Tag).strUserGroupID;
            clsApplUserGroup_VO[] objApplUserGroupVO = null;
            lngRes = objDomainControllerCheckGroup.m_lngGetSubGroupByUserGroupID(strUserGroupID, out objApplUserGroupVO);
            if (lngRes > 0 && objApplUserGroupVO != null)
            {
                if (objApplUserGroupVO.Length > 0)
                {
                    //���ǵ��û��Զ�������а������뵥Ԫ�����
                    getChildApplUnit(infrmApplUserGroup, objTreeNode);

                    for (int i = 0; i < objApplUserGroupVO.Length; i++)
                    {
                        objChildTreeNode = objTreeNode.Nodes.Add("(" + objApplUserGroupVO[i].strUserGroupID + ")" + objApplUserGroupVO[i].strUserGroupName);
                        objChildTreeNode.Tag = objApplUserGroupVO[i];

                        if (int.Parse(objApplUserGroupVO[i].strHasChildGroup) > 0)
                        {
                            //������һ����û�������������뵥Ԫ
                            getChildUserGroupAndApplUnit(infrmApplUserGroup, objChildTreeNode);
                        }
                        else
                        {
                            getChildApplUnit(infrmApplUserGroup, objChildTreeNode);
                        }
                    }
                }
            }
            else
            {
                getChildApplUnit(infrmApplUserGroup, objTreeNode);
            }
        }

        //���Ҹ����µ����뵥Ԫ
        private void getChildApplUnit(frmApplUserGroup infrmApplUserGroup, TreeNode objTreeNode)
        {
            long lngRes = 0;
            TreeNode objChildTreeNode = null;
            clsDomainController_LisCheckGroupManage objDomainControllerCheckGroup = new clsDomainController_LisCheckGroupManage();
            string strUserGroupID = ((clsApplUserGroup_VO)objTreeNode.Tag).strUserGroupID;
            clsApplUnit_VO[] objApplUnit = null;
            lngRes = objDomainControllerCheckGroup.m_lngGetApplUnitByUserGroupID(strUserGroupID, out objApplUnit);
            if (lngRes > 0 && objApplUnit != null)
            {
                if (objApplUnit.Length > 0)
                {
                    for (int i = 0; i < objApplUnit.Length; i++)
                    {
                        objChildTreeNode = objTreeNode.Nodes.Add("(" + objApplUnit[i].strApplUnitID + ")" + objApplUnit[i].strApplUnitName);
                        objChildTreeNode.Tag = objApplUnit[i];
                    }
                }
            }
        }

        #endregion

        #region ���ݼ�������ȡ���еļ�����Ŀ

        public long getAllCheckItemByCheckCategory(frmApplUserGroup infrmApplUserGroup)//,string strCheckCategoryID)//,out DataTable dtbCheckItemByCategory)
        {
            long lngRes = 0;
            infrmApplUserGroup.lsvCheckItem.Items.Clear();
            string strCheckCategoryID = infrmApplUserGroup.cboCheckCategory.SelectedValue.ToString().Trim();
            DataTable dtbCheckItemByCategory = null;
            lngRes = (new weCare.Proxy.ProxyLis()).Service.m_lngGetAllCheckItemByCheckCategory(strCheckCategoryID, out dtbCheckItemByCategory);
            int count = dtbCheckItemByCategory.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    ListViewItem objlsvItem = new ListViewItem();
                    objlsvItem.Text = dtbCheckItemByCategory.Rows[i]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                    objlsvItem.SubItems.Add(dtbCheckItemByCategory.Rows[i]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim());
                    objlsvItem.SubItems.Add(dtbCheckItemByCategory.Rows[i]["CHECK_ITEM_NAME_VCHR"].ToString().Trim());
                    objlsvItem.Tag = dtbCheckItemByCategory.Rows[i];
                    infrmApplUserGroup.lsvCheckItem.Items.Add(objlsvItem);
                }
            }
            return lngRes;
        }

        #endregion

        #region ѡ�в�ͬ�ڵ�ʱ��ʾ�ýڵ�������Ϣ ͯ��

        public void lsvCheckGroupSelectIndexChanged(frmApplUserGroup infrmApplUserGroup, TreeNode objTreeNode)
        {
            if (objTreeNode.Parent != null)
            {
                long lngRes = 0;
                clsDomainController_ApplyUnitManage objDomainControllerCheckGroup = new clsDomainController_ApplyUnitManage();
                //���뵥Ԫ
                if (objTreeNode.Tag is clsApplUnit_VO)
                {
                    infrmApplUserGroup.rdbApplUnit.Checked = true;
                    infrmApplUserGroup.txtCheckGroupNo.Text = ((clsApplUnit_VO)objTreeNode.Tag).strApplUnitID;
                    infrmApplUserGroup.txtCheckGroupName.Text = ((clsApplUnit_VO)objTreeNode.Tag).strApplUnitName;
                    infrmApplUserGroup.txtPyCode.Text = ((clsApplUnit_VO)objTreeNode.Tag).strPYCode;
                    infrmApplUserGroup.txtWbCode.Text = ((clsApplUnit_VO)objTreeNode.Tag).strWBCode;
                    infrmApplUserGroup.txtAssist01.Text = ((clsApplUnit_VO)objTreeNode.Tag).strAssistCode01;
                    infrmApplUserGroup.txtAssist02.Text = ((clsApplUnit_VO)objTreeNode.Tag).strAssistCode02;
                    infrmApplUserGroup.txtApplUnitOtherName.Text = ((clsApplUnit_VO)objTreeNode.Tag).strOtherName;
                    infrmApplUserGroup.m_txtCost.Text = ((clsApplUnit_VO)objTreeNode.Tag).strCost;
                    infrmApplUserGroup.m_txtPrice.Text = ((clsApplUnit_VO)objTreeNode.Tag).strPrice;

                    infrmApplUserGroup.txtSummary.Text = ((clsApplUnit_VO)objTreeNode.Tag).strSummary;
                    infrmApplUserGroup.chkOutCheckFlag.Checked = (((clsApplUnit_VO)objTreeNode.Tag).strOutCheckFlag == "1" ? true : false);
                    infrmApplUserGroup.txtReportHour.Text = ((clsApplUnit_VO)objTreeNode.Tag).ReportHour.ToString();
                    infrmApplUserGroup.txtSamplingInstr.Text = ((clsApplUnit_VO)objTreeNode.Tag).SamplingInstr;

                    try
                    {
                        infrmApplUserGroup.m_cboSampleType.SelectedValue = ((clsApplUnit_VO)objTreeNode.Tag).strSampleTypeID;
                    }
                    catch
                    {
                        infrmApplUserGroup.m_cboSampleType.SelectedItem = null;
                    }
                    try
                    {
                        infrmApplUserGroup.cboCheckCategory.SelectedValue = ((clsApplUnit_VO)objTreeNode.Tag).strCheckCategoryID;
                    }
                    catch
                    {
                        MessageBox.Show("�����뵥Ԫ�ļ������ɾ�������");
                    }
                    if (((clsApplUnit_VO)objTreeNode.Tag).strIsNoFoodRequired == "1")
                    {
                        infrmApplUserGroup.chkNoFood.Checked = true;
                    }
                    if (((clsApplUnit_VO)objTreeNode.Tag).strIsPhysicsExamRequired == "1")
                    {
                        infrmApplUserGroup.chkBodyCheck.Checked = true;
                    }
                    if (((clsApplUnit_VO)objTreeNode.Tag).strIsReservationRequired == "1")
                    {
                        infrmApplUserGroup.chkReservation.Checked = true;
                    }
                    DataTable dtbCheckItem = null;
                    infrmApplUserGroup.lsvAddCheckItem.Items.Clear();
                    lngRes = objDomainControllerCheckGroup.m_lngGetCheckItemByApplUnitID(((clsApplUnit_VO)objTreeNode.Tag).strApplUnitID, out dtbCheckItem);
                    if (dtbCheckItem != null && dtbCheckItem.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtbCheckItem.Rows.Count; i++)
                        {
                            ListViewItem objlsvItem = new ListViewItem();
                            objlsvItem.Text = dtbCheckItem.Rows[i]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                            objlsvItem.SubItems.Add(dtbCheckItem.Rows[i]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim());
                            objlsvItem.SubItems.Add(dtbCheckItem.Rows[i]["CHECK_ITEM_NAME_VCHR"].ToString().Trim());
                            objlsvItem.Tag = dtbCheckItem.Rows[i];
                            infrmApplUserGroup.lsvAddCheckItem.Items.Add(objlsvItem);
                        }
                        infrmApplUserGroup.lsvAddCheckItem.Tag = dtbCheckItem;
                    }
                }
                //�û��Զ�����
                if (objTreeNode.Tag is clsApplUserGroup_VO)
                {
                    clsDomainController_AppGroupManage objDomainAppGroupManage = new clsDomainController_AppGroupManage();
                    infrmApplUserGroup.rdbSelfDefine.Checked = true;
                    infrmApplUserGroup.txtCheckGroupNo.Text = ((clsApplUserGroup_VO)objTreeNode.Tag).strUserGroupID;
                    infrmApplUserGroup.txtCheckGroupName.Text = ((clsApplUserGroup_VO)objTreeNode.Tag).strUserGroupName;
                    infrmApplUserGroup.txtPyCode.Text = ((clsApplUserGroup_VO)objTreeNode.Tag).strPYCode;
                    infrmApplUserGroup.txtWbCode.Text = ((clsApplUserGroup_VO)objTreeNode.Tag).strWBCode;
                    infrmApplUserGroup.txtAssist01.Text = ((clsApplUserGroup_VO)objTreeNode.Tag).strAssistCode01;
                    infrmApplUserGroup.txtAssist02.Text = ((clsApplUserGroup_VO)objTreeNode.Tag).strAssistCode02;

                    //xing.chen add
                    infrmApplUserGroup.txtSummary.Text = ((clsApplUserGroup_VO)objTreeNode.Tag).strSummary;

                    //������Ϣ
                    TreeNode objChildTreeNode = null;
                    clsApplUserGroup_VO[] objApplUserGroupVO = null;
                    //�û��Զ������
                    lngRes = objDomainAppGroupManage.m_lngGetSubGroupByUserGroupID(((clsApplUserGroup_VO)objTreeNode.Tag).strUserGroupID, out objApplUserGroupVO);
                    if (lngRes > 0 && objApplUserGroupVO != null)
                    {
                        if (objApplUserGroupVO.Length > 0)
                        {
                            infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Clear();
                            for (int i = 0; i < objApplUserGroupVO.Length; i++)
                            {
                                objChildTreeNode = infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Add("(" + objApplUserGroupVO[i].strUserGroupID + ")" + objApplUserGroupVO[i].strUserGroupName);
                                objChildTreeNode.Tag = objApplUserGroupVO[i];
                                if (int.Parse(objApplUserGroupVO[i].strHasChildGroup) > 0)
                                {
                                    //������һ����û�������������뵥Ԫ
                                    getChildUserGroupAndApplUnit(infrmApplUserGroup, objChildTreeNode);
                                }
                                else
                                {
                                    //���Ҹ����µ����뵥Ԫ
                                    getChildApplUnit(infrmApplUserGroup, objChildTreeNode);
                                }
                            }
                        }
                    }
                    //���뵥Ԫ
                    string strUserGroupID = ((clsApplUserGroup_VO)objTreeNode.Tag).strUserGroupID;
                    clsApplUnit_VO[] objApplUnit = null;
                    lngRes = objDomainAppGroupManage.m_lngGetApplUnitByUserGroupID(strUserGroupID, out objApplUnit);
                    infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes.Clear();
                    if (lngRes > 0 && objApplUnit != null)
                    {
                        if (objApplUnit.Length > 0)
                        {
                            for (int i = 0; i < objApplUnit.Length; i++)
                            {
                                objChildTreeNode = infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes.Add("(" + objApplUnit[i].strApplUnitID + ")" + objApplUnit[i].strApplUnitName);
                                objChildTreeNode.Tag = objApplUnit[i];
                            }
                        }
                    }
                }
                if (objTreeNode.Parent == infrmApplUserGroup.trvCheckGroup.Nodes[0])
                {
                    infrmApplUserGroup.cboCheckCategory.SelectedValue = ((clsCheckCategory_VO)objTreeNode.Tag).m_strCheck_Category_ID;
                }
            }

            //������뵥Ԫ�޸Ļ���
            m_arlAddDetail.Clear();
            m_arlRemoveDetail.Clear();
        }

        #endregion

        #region �����ڵ�ѡ��ʱ�������ӽڵ�Ҳȫ��ѡ�� ͯ�� 2004.05.27

        public void checkAllByParentChecked(frmApplUserGroup infrmApplUserGroup, TreeNode objTreeNode)
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

        #region ���뵥Ԫ��ӡ�ɾ��������Ŀ ͯ�� 2004.05.27

        //��Ӽ�����Ŀ
        public void m_mthAddCheckItem(frmApplUserGroup infrmApplUserGroup)
        {
            int count = infrmApplUserGroup.lsvCheckItem.Items.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (infrmApplUserGroup.lsvCheckItem.Items[i].Checked)
                    {
                        int addCount = infrmApplUserGroup.lsvAddCheckItem.Items.Count;
                        //						if(objApplUnitDetailVOList != null && objApplUnitDetailVOList.Length  > 0)
                        //						{
                        //							for(int k=0;k<objApplUnitDetailVOList.Length;k++)
                        //							{
                        //								if(infrmApplUserGroup.lsvCheckItem.Items[i].SubItems[0].Text.ToString().Trim() == objApplUnitDetailVOList[k].strCheckItemID)
                        //								{
                        //									MessageBox.Show("�ü�����Ŀ�ѱ��������뵥Ԫ��ӣ�","������Ŀ",MessageBoxButtons.OK);
                        //									return;
                        //								}
                        //							}
                        //						}
                        if (addCount > 0)
                        {
                            for (int j = 0; j < addCount; j++)
                            {
                                if (infrmApplUserGroup.lsvAddCheckItem.Items[j].SubItems[0].Text.ToString().Trim() == infrmApplUserGroup.lsvCheckItem.Items[i].SubItems[0].Text.ToString().Trim())
                                {
                                    MessageBox.Show("��������ظ��ļ�����Ŀ��", "������Ŀ", MessageBoxButtons.OK);
                                    return;
                                }
                            }
                            if (((DataRow)infrmApplUserGroup.lsvAddCheckItem.Items[0].Tag)["CHECK_CATEGORY_ID_CHR"].ToString().Trim() != ((DataRow)infrmApplUserGroup.lsvCheckItem.Items[i].Tag)["CHECK_CATEGORY_ID_CHR"].ToString().Trim())
                            {
                                MessageBox.Show("������Ӳ�ͬ�������ļ�����Ŀ��", "������Ŀ", MessageBoxButtons.OK);
                                return;
                            }

                            ListViewItem objlsvItem = new ListViewItem();
                            objlsvItem.Text = infrmApplUserGroup.lsvCheckItem.Items[i].SubItems[0].Text.ToString().Trim();
                            objlsvItem.SubItems.Add(infrmApplUserGroup.lsvCheckItem.Items[i].SubItems[1].Text.ToString().Trim());
                            objlsvItem.SubItems.Add(infrmApplUserGroup.lsvCheckItem.Items[i].SubItems[2].Text.ToString().Trim());
                            objlsvItem.Tag = infrmApplUserGroup.lsvCheckItem.Items[i].Tag;
                            infrmApplUserGroup.lsvAddCheckItem.Items.Add(objlsvItem);
                            if (infrmApplUserGroup.btnDelCheckItem.Enabled == false)
                            {
                                infrmApplUserGroup.btnDelCheckItem.Enabled = true;
                            }
                        }
                        else
                        {
                            ListViewItem objlsvItem = new ListViewItem();
                            objlsvItem.Text = infrmApplUserGroup.lsvCheckItem.Items[i].SubItems[0].Text.ToString().Trim();
                            objlsvItem.SubItems.Add(infrmApplUserGroup.lsvCheckItem.Items[i].SubItems[1].Text.ToString().Trim());
                            objlsvItem.SubItems.Add(infrmApplUserGroup.lsvCheckItem.Items[i].SubItems[2].Text.ToString().Trim());
                            objlsvItem.Tag = infrmApplUserGroup.lsvCheckItem.Items[i].Tag;
                            infrmApplUserGroup.lsvAddCheckItem.Items.Add(objlsvItem);
                            if (infrmApplUserGroup.btnDelCheckItem.Enabled == false)
                            {
                                infrmApplUserGroup.btnDelCheckItem.Enabled = true;
                            }
                        }
                        if (infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim() != null)
                        {
                            clsApplUnitDetail_VO objRecord = new clsApplUnitDetail_VO();
                            objRecord.strCheckItemID = infrmApplUserGroup.lsvCheckItem.Items[i].SubItems[0].Text.ToString().Trim();
                            objRecord.strApplUnitID = infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim();
                            m_arlAddDetail.Add(objRecord);
                        }
                    }
                }
            }
        }

        //ɾ��������Ŀ
        public void m_mthDelCheckItem(frmApplUserGroup infrmApplUserGroup)
        {
            DataTable dtbRawItem = (DataTable)infrmApplUserGroup.lsvAddCheckItem.Tag;
            int count = infrmApplUserGroup.lsvAddCheckItem.Items.Count;
            if (count > 0)
            {
                for (int i = 0; i < infrmApplUserGroup.lsvAddCheckItem.Items.Count; i++)
                {
                    if (infrmApplUserGroup.lsvAddCheckItem.Items[i].Checked)
                    {
                        if (infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim() != null)
                        {
                            for (int j = 0; j < dtbRawItem.Rows.Count; j++)
                            {
                                if (dtbRawItem.Rows[j]["CHECK_ITEM_ID_CHR"].ToString().Trim() ==
                                    ((DataRow)(infrmApplUserGroup.lsvAddCheckItem.Items[i].Tag))["CHECK_ITEM_ID_CHR"].ToString().Trim())
                                {
                                    clsApplUnitDetail_VO objRecord = new clsApplUnitDetail_VO();
                                    objRecord.strCheckItemID = dtbRawItem.Rows[j]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                                    objRecord.strApplUnitID = infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim();
                                    m_arlRemoveDetail.Add(objRecord);
                                }
                            }
                        }
                        infrmApplUserGroup.lsvAddCheckItem.Items[i].Remove();
                        i = i - 1;
                    }
                }
            }
        }

        #endregion

        #region �û��Զ���������ӡ�ɾ������ ͯ�� 2004.05.27

        public void m_lngAddSubGroup(frmApplUserGroup infrmApplUserGroup)
        {
            //�������нڵ�
            foreach (TreeNode objTreeNode in infrmApplUserGroup.trvSubCheckGroup.Nodes)
            {
                findNextLevelNode(infrmApplUserGroup, objTreeNode);
            }
        }

        //������һ��Ľڵ�
        private void findNextLevelNode(frmApplUserGroup infrmApplUserGroup, TreeNode objTreeNode)
        {
            for (int i = 0; i < objTreeNode.Nodes.Count; i++)
            {
                if (objTreeNode.Nodes[i].Nodes.Count > 0 && objTreeNode.Nodes[i].Parent != infrmApplUserGroup.trvSubCheckGroup.Nodes[0]
                    && objTreeNode.Nodes[i] != infrmApplUserGroup.trvSubCheckGroup.Nodes[0])
                {
                    //�û��Զ�����
                    if (objTreeNode.Nodes[i].Checked)
                    {
                        //1.�޸�ʱ�������������Ϊ����Ͳ�����Ӹ��ڵ���Ϊ����
                        if (infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim() != "")
                        {
                            if (infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim() == ((clsApplUserGroup_VO)objTreeNode.Nodes[i].Tag).strUserGroupID)
                            {
                                MessageBox.Show("�������������Ϊ���飡", "�Զ�����", MessageBoxButtons.OK);
                                return;
                            }
                            if (findChildNode(infrmApplUserGroup, objTreeNode.Nodes[i], infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim()))
                            {
                                MessageBox.Show("������Ӹ��ڵ���Ϊ���飡", "�Զ�����", MessageBoxButtons.OK);
                                return;
                            }
                        }
                        //2.�ж��Ƿ��Ѿ�����˸��������ýڵ����
                        if (infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Count > 0)
                        {
                            for (int j = 0; j < infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Count; j++)
                            {
                                if (((clsApplUserGroup_VO)objTreeNode.Nodes[i].Tag).strUserGroupID == ((clsApplUserGroup_VO)infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes[j].Tag).strUserGroupID)
                                {
                                    MessageBox.Show("�ýڵ��ѱ���ӣ�", "�Զ�����", MessageBoxButtons.OK);
                                    return;
                                }
                                //�ж���ӵ���������Ƿ����
                                if (findChildNode(infrmApplUserGroup, objTreeNode.Nodes[i], ((clsApplUserGroup_VO)infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes[j].Tag).strUserGroupID))
                                {
                                    MessageBox.Show("�ýڵ���ӽڵ��ѱ���ӣ�", "�Զ�����", MessageBoxButtons.OK);
                                    return;
                                }
                                //�ж��Ѿ���ӵ����Ƿ����Ҫ��ӵ���
                                if (findChildNode(infrmApplUserGroup, infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes[j], ((clsApplUserGroup_VO)objTreeNode.Nodes[i].Tag).strUserGroupID))
                                {
                                    MessageBox.Show("�ýڵ�ĸ��ڵ��ѱ���ӣ�", "�Զ�����", MessageBoxButtons.OK);
                                    return;
                                }
                            }
                        }
                        //						//3.�ж��Ƿ��Ѿ�����˸��������������뵥Ԫ
                        //						if(infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes.Count > 0)
                        //						{
                        //						}
                        //��ӵ�trvSubGroup
                        TreeNode objAddSubTreeNode = null;
                        objAddSubTreeNode = infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Add("(" +
                            ((clsApplUserGroup_VO)objTreeNode.Nodes[i].Tag).strUserGroupID + ")" +
                            ((clsApplUserGroup_VO)objTreeNode.Nodes[i].Tag).strUserGroupName);
                        objAddSubTreeNode.Tag = objTreeNode.Nodes[i].Tag;
                        if (objTreeNode.Nodes[i].Nodes.Count > 0)
                        {
                            getChildUserGroupAndApplUnit(infrmApplUserGroup, objAddSubTreeNode);
                        }
                    }
                    else
                    {
                        findNextLevelNode(infrmApplUserGroup, objTreeNode.Nodes[i]);
                    }
                }
                else if (objTreeNode.Nodes[i].Parent != infrmApplUserGroup.trvSubCheckGroup.Nodes[0]
                    && objTreeNode.Nodes[i] != infrmApplUserGroup.trvSubCheckGroup.Nodes[0])
                {
                    //���뵥Ԫ
                    if (objTreeNode.Nodes[i].Checked)
                    {
                        if (infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes.Count > 0)
                        {
                            //�ж����뵥Ԫ���Ƿ�����ýڵ�
                            for (int k = 0; k < infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes.Count; k++)
                            {
                                if (((clsApplUnit_VO)objTreeNode.Nodes[i].Tag).strApplUnitID == ((clsApplUnit_VO)infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes[k].Tag).strApplUnitID)
                                {
                                    MessageBox.Show("���������ѱ���ӣ�", "�Զ�����", MessageBoxButtons.OK);
                                    return;
                                }
                            }
                        }
                        if (infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Count > 0)
                        {
                            //�жϸ����뵥Ԫ�Ƿ񱻰������Զ�������
                            for (int l = 0; l < infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Count; l++)
                            {
                                if (findApplUnit(infrmApplUserGroup, infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes[l], ((clsApplUnit_VO)objTreeNode.Nodes[i].Tag).strApplUnitID))
                                {
                                    MessageBox.Show("���������ѱ��Զ�������ӣ�", "�Զ�����", MessageBoxButtons.OK);
                                    return;
                                }
                            }
                        }
                        TreeNode objAddSubTreeNode = null;
                        objAddSubTreeNode = infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes.Add("("
                            + ((clsApplUnit_VO)objTreeNode.Nodes[i].Tag).strApplUnitID + ")" + ((clsApplUnit_VO)objTreeNode.Nodes[i].Tag).strApplUnitName);
                        objAddSubTreeNode.Tag = objTreeNode.Nodes[i].Tag;
                    }
                }
                else
                {
                    findNextLevelNode(infrmApplUserGroup, objTreeNode.Nodes[i]);
                }
            }
        }

        /// <summary>
        /// �ж�strUserGroup(�û��Զ�����)�Ƿ������objDestNode��������
        /// </summary>
        /// <param name="infrmApplUserGroup"></param>
        /// <param name="objDestNode"></param>
        /// <param name="strUserGroupID"></param>
        /// <returns></returns>
        public bool findChildNode(frmApplUserGroup infrmApplUserGroup, TreeNode objDestNode, string strUserGroupID)
        {
            bool bolfind = false;
            for (int i = 0; i < objDestNode.Nodes.Count; i++)
            {
                if (objDestNode.Nodes[i].Nodes.Count > 0)
                {
                    if (strUserGroupID == ((clsApplUserGroup_VO)objDestNode.Nodes[i].Tag).strUserGroupID)
                    {
                        bolfind = true;
                        return bolfind;
                    }
                    bolfind = findChildNode(infrmApplUserGroup, objDestNode.Nodes[i], strUserGroupID);
                }
            }
            return bolfind;
        }

        /// <summary>
        /// �ж�һ�����뵥Ԫ�Ƿ������ĳһ�Զ���ڵ���
        /// </summary>
        /// <param name="infrmApplUserGroup"></param>
        /// <param name="objTreeNode">�Զ���ڵ�</param>
        /// <param name="strApplUnitID">���뵥ԪID</param>
        /// <returns></returns>
        public bool findApplUnit(frmApplUserGroup infrmApplUserGroup, TreeNode objTreeNode, string strApplUnitID)
        {
            bool bolfind = false;
            for (int i = 0; i < objTreeNode.Nodes.Count; i++)
            {
                if (objTreeNode.Nodes[i].Nodes.Count == 0)
                {
                    if (((clsApplUnit_VO)objTreeNode.Nodes[i].Tag).strApplUnitID == strApplUnitID)
                    {
                        bolfind = true;
                        return bolfind;
                    }
                }
                if (objTreeNode.Nodes[i].Nodes.Count > 0)
                {
                    bolfind = findApplUnit(infrmApplUserGroup, objTreeNode.Nodes[i], strApplUnitID);
                }
            }
            return bolfind;
        }

        /// <summary>
        /// ɾ����ӵ�����
        /// </summary>
        /// <param name="infrmApplUserGroup"></param>
        public void m_lngDelSubGroup(frmApplUserGroup infrmApplUserGroup)
        {
            int intApplcount = infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes.Count;
            int intUsercount = infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Count;
            //���뵥Ԫ
            if (intApplcount > 0)
            {
                for (int i = 0; i < intApplcount; i++)
                {
                    if (infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes[i].Checked)
                    {
                        infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes[i].Remove();
                        intApplcount--;
                        i--;
                    }
                }
            }
            //�û��Զ�����
            if (intUsercount > 0)
            {
                for (int i = 0; i < intUsercount; i++)
                {
                    if (infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes[i].Checked)
                    {
                        infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes[i].Remove();
                        intUsercount--;
                        i--;
                    }
                }
            }
        }

        #endregion

        #region �������������뵥Ԫ���û��Զ����� ͯ�� 2004.05.27	xing.chen modify

        public long AddNewApplUserGroup(frmApplUserGroup infrmApplUserGroup)
        {
            long lngRes = 0;
            //���뵥Ԫ
            if (infrmApplUserGroup.rdbApplUnit.Checked)
            {
                if (infrmApplUserGroup.lsvAddCheckItem.Items.Count == 0)
                {
                    MessageBox.Show("����Ӽ�����Ŀ��", "���뵥Ԫ", MessageBoxButtons.OK);
                    return -1;
                }
                if (infrmApplUserGroup.txtCheckGroupName.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("���������뵥Ԫ���ƣ�", "���뵥Ԫ", MessageBoxButtons.OK);
                    return -1;
                }
                //����clsApplUnit_VO
                clsApplUnit_VO objApplUnitVO = new clsApplUnit_VO();
                if (infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim() != "")
                {
                    objApplUnitVO.strApplUnitID = infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim();
                }
                objApplUnitVO.strApplUnitName = infrmApplUserGroup.txtCheckGroupName.Text.ToString().Trim();
                objApplUnitVO.strAssistCode01 = infrmApplUserGroup.txtAssist01.Text.ToString().Trim();
                objApplUnitVO.strAssistCode02 = infrmApplUserGroup.txtAssist02.Text.ToString().Trim();
                objApplUnitVO.strPYCode = infrmApplUserGroup.txtPyCode.Text.ToString().Trim();
                objApplUnitVO.strWBCode = infrmApplUserGroup.txtWbCode.Text.ToString().Trim();
                objApplUnitVO.strCheckCategoryID = infrmApplUserGroup.cboCheckCategory.SelectedValue.ToString().Trim();
                objApplUnitVO.strOtherName = infrmApplUserGroup.txtApplUnitOtherName.Text.ToString().Trim();
                objApplUnitVO.strPrice = infrmApplUserGroup.m_txtPrice.Text.ToString().Trim();
                objApplUnitVO.strCost = infrmApplUserGroup.m_txtCost.Text.ToString().Trim();
                objApplUnitVO.strSampleTypeID = infrmApplUserGroup.m_cboSampleType.SelectedValue == null ? null : infrmApplUserGroup.m_cboSampleType.SelectedValue.ToString();
                //xing.chen add
                objApplUnitVO.strSummary = infrmApplUserGroup.txtSummary.Text.ToString().Trim();

                if (infrmApplUserGroup.chkBodyCheck.Checked)
                {
                    objApplUnitVO.strIsPhysicsExamRequired = "1";
                }
                else
                {
                    objApplUnitVO.strIsPhysicsExamRequired = "0";
                }
                if (infrmApplUserGroup.chkNoFood.Checked)
                {
                    objApplUnitVO.strIsNoFoodRequired = "1";
                }
                else
                {
                    objApplUnitVO.strIsNoFoodRequired = "0";
                }
                if (infrmApplUserGroup.chkReservation.Checked)
                {
                    objApplUnitVO.strIsReservationRequired = "1";
                }
                else
                {
                    objApplUnitVO.strIsReservationRequired = "0";
                }

                objApplUnitVO.strOutCheckFlag = (infrmApplUserGroup.chkOutCheckFlag.Checked ? "1" : "0");
                objApplUnitVO.ReportHour = Convert.ToDecimal(infrmApplUserGroup.txtReportHour.Text);
                objApplUnitVO.SamplingInstr = infrmApplUserGroup.txtSamplingInstr.Text;

                //����clsApplUnitDetail_VO
                clsApplUnitDetail_VO[] objApplUnitDetailVO = new clsApplUnitDetail_VO[infrmApplUserGroup.lsvAddCheckItem.Items.Count];
                for (int i = 0; i < infrmApplUserGroup.lsvAddCheckItem.Items.Count; i++)
                {
                    objApplUnitDetailVO[i] = new clsApplUnitDetail_VO();
                    objApplUnitDetailVO[i].strCheckItemID = infrmApplUserGroup.lsvAddCheckItem.Items[i].SubItems[0].Text.ToString().Trim();
                }
                //�����¼
                clsDomainController_ApplyUnitManage objDomainControllerApplyUnit = new clsDomainController_ApplyUnitManage();
                lngRes = objDomainControllerApplyUnit.m_lngAddApplUnitAndDetail(ref objApplUnitVO, ref objApplUnitDetailVO);
                if (lngRes > 0)
                {
                    refreshAllCheckGroupANDTrvCheckGroup(infrmApplUserGroup);
                    m_mthGetApplUnitByID(infrmApplUserGroup, objApplUnitVO.strApplUnitID);
                }
            }
            //�Զ�����
            if (infrmApplUserGroup.rdbSelfDefine.Checked)
            {
                //if(infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes.Count == 0 && infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Count == 0)
                //{
                //    MessageBox.Show("��ѡ��Ҫ��ӵ����뵥Ԫ���Զ�����","�Զ�����",MessageBoxButtons.OK);
                //    return -1;
                //}
                if (infrmApplUserGroup.txtCheckGroupName.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("�������������", "�Զ�����", MessageBoxButtons.OK);
                    return -1;
                }
                //���������Ϣ
                clsApplUserGroup_VO objApplUserVO = new clsApplUserGroup_VO();
                objApplUserVO.strUserGroupName = infrmApplUserGroup.txtCheckGroupName.Text.ToString().Trim();
                objApplUserVO.strAssistCode01 = infrmApplUserGroup.txtAssist01.Text.ToString().Trim();
                objApplUserVO.strAssistCode02 = infrmApplUserGroup.txtAssist02.Text.ToString().Trim();
                objApplUserVO.strPYCode = infrmApplUserGroup.txtPyCode.Text.ToString().Trim();
                objApplUserVO.strWBCode = infrmApplUserGroup.txtWbCode.Text.ToString().Trim();

                //xing.chen add
                objApplUserVO.strSummary = infrmApplUserGroup.txtSummary.Text.ToString().Trim();

                if (infrmApplUserGroup.txtCheckGroupNo.Text.ToString() != "")
                {
                    objApplUserVO.strUserGroupID = infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim();
                }
                //���뵥Ԫ
                clsApplUserGroupDetail_VO[] objApplUserDetailVOList = null;
                if (infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes.Count > 0)
                {
                    //����clsApplUnit_VO
                    objApplUserDetailVOList = new clsApplUserGroupDetail_VO[infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes.Count];
                    for (int i = 0; i < infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes.Count; i++)
                    {
                        objApplUserDetailVOList[i] = new clsApplUserGroupDetail_VO();
                        objApplUserDetailVOList[i].strApplUnitID = ((clsApplUnit_VO)infrmApplUserGroup.trvAddSubGroup.Nodes[0].Nodes[i].Tag).strApplUnitID;
                    }
                }
                //�Զ�����
                clsApplUserGroupRelation_VO[] objApplUserGroupRelationVOList = null;
                if (infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Count > 0)
                {
                    objApplUserVO.strHasChildGroup = "1";
                    //����clsApplUnitRelation_VO
                    objApplUserGroupRelationVOList = new clsApplUserGroupRelation_VO[infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Count];
                    for (int i = 0; i < infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes.Count; i++)
                    {
                        objApplUserGroupRelationVOList[i] = new clsApplUserGroupRelation_VO();
                        objApplUserGroupRelationVOList[i].strChildUserGroupID = ((clsApplUserGroup_VO)infrmApplUserGroup.trvAddSubGroup.Nodes[1].Nodes[i].Tag).strUserGroupID;
                    }
                }
                else
                {
                    objApplUserVO.strHasChildGroup = "0";
                }
                //�жϸýڵ��Ƿ���ĳ���ڵ������
                ArrayList arlIdx = new ArrayList();
                TreeNode objTreeNode = infrmApplUserGroup.trvCheckGroup.SelectedNode;
                clsApplUserGroupRelation_VO objApplUserGroupRelation = null;
                if (infrmApplUserGroup.trvCheckGroup.SelectedNode != infrmApplUserGroup.trvCheckGroup.Nodes[1] &&
                    infrmApplUserGroup.trvCheckGroup.SelectedNode.Nodes.Count > 0 && infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim() == "")
                {
                    //��¼ѡ�нڵ�ĺۼ�
                    while (objTreeNode != null)
                    {
                        arlIdx.Add(objTreeNode.Index);
                        objTreeNode = objTreeNode.Parent;
                    }
                    objApplUserGroupRelation = new clsApplUserGroupRelation_VO();
                    objApplUserGroupRelation.strUserGroupID = ((clsApplUserGroup_VO)infrmApplUserGroup.trvCheckGroup.SelectedNode.Tag).strUserGroupID;
                }
                else
                {
                    objApplUserGroupRelation = null;
                }
                //�����¼
                clsDomainController_AppGroupManage objAppGroup = new clsDomainController_AppGroupManage();
                lngRes = objAppGroup.m_lngAddApplUserGroupAndDetailRelation(ref objApplUserVO, ref objApplUserDetailVOList,
                    ref objApplUserGroupRelationVOList, objApplUserGroupRelation);
                if (lngRes > 0)
                {
                    refreshAllCheckGroupANDTrvCheckGroup(infrmApplUserGroup);
                    refreshApplUnitDetail();
                    if (objApplUserGroupRelation == null)
                    {
                        m_mthGetApplUserGroupByID(infrmApplUserGroup, objApplUserVO.strUserGroupID);
                    }
                    else
                    {
                        try
                        {
                            int intCount = arlIdx.Count - 1;
                            objTreeNode = infrmApplUserGroup.trvCheckGroup.Nodes[(int)arlIdx[intCount]];
                            intCount--;
                            while (intCount >= 0)
                            {
                                objTreeNode = objTreeNode.Nodes[(int)arlIdx[intCount]];
                                intCount--;
                            }
                            for (int i = 0; i < objTreeNode.Nodes.Count; i++)
                            {
                                if (objTreeNode.Nodes[i].Nodes.Count > 0)
                                {
                                    if (((clsApplUserGroup_VO)objTreeNode.Nodes[i].Tag).strUserGroupID == objApplUserVO.strUserGroupID)
                                    {
                                        infrmApplUserGroup.trvCheckGroup.SelectedNode = objTreeNode.Nodes[i];
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }
            }

            //������뵥Ԫ�޸Ļ���
            m_arlAddDetail.Clear();
            m_arlRemoveDetail.Clear();

            return lngRes;
        }

        #endregion

        #region ɾ�����뵥Ԫ���û��Զ��������� ͯ�� 2004.05.27

        public long m_lngDelApplUnitAndUserGroup(frmApplUserGroup infrmApplUserGroup)
        {
            long lngRes = 0;
            clsDomainController_AppGroupManage objDomainAppGroup = new clsDomainController_AppGroupManage();
            //���뵥Ԫ
            if (infrmApplUserGroup.trvCheckGroup.SelectedNode.Tag is clsApplUnit_VO)
            {
                DialogResult objResult = MessageBox.Show("ȷ��Ҫɾ���ü�¼��", "", MessageBoxButtons.OKCancel);
                if (objResult == DialogResult.Cancel)
                {
                    return lngRes;
                }
                //�жϸ����뵥Ԫ�Ƿ��Զ��������
                DataTable dtbApplUnitID = null;
                lngRes = objDomainAppGroup.m_lngGetAllUserGroupApplUnitID(out dtbApplUnitID);
                if (lngRes > 0)
                {
                    if (dtbApplUnitID != null && dtbApplUnitID.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtbApplUnitID.Rows.Count; i++)
                        {
                            if (dtbApplUnitID.Rows[i]["APPLY_UNIT_ID_CHR"].ToString().Trim() == infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim())
                            {
                                MessageBox.Show("���뵥Ԫɾ��ʧ�ܣ�����ɾ�����������뵥Ԫ���Զ����飡", "���뵥Ԫ", MessageBoxButtons.OK);
                                return -1;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("���뵥Ԫɾ��ʧ�ܣ�", "���뵥Ԫ", MessageBoxButtons.OK);
                    return lngRes;
                }

                //�жϸ����뵥Ԫ�Ƿ񱻱걾�����
                DataTable dtbResult = null;
                clsDomainController_SampleGroupManage objManage = new clsDomainController_SampleGroupManage();
                lngRes = objManage.m_lngGetSampleGroupUnitByApplUnitID(infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim(), out dtbResult);
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    MessageBox.Show("����ɾ�����������뵥Ԫ�ı걾�飡", "���뵥Ԫ", MessageBoxButtons.OK);
                    return -1;
                }

                clsDomainController_ApplyUnitManage objDomainControllerApplyUnint = new clsDomainController_ApplyUnitManage();
                lngRes = objDomainControllerApplyUnint.m_lngDelApplUnitAndDetail(infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim());
                if (lngRes > 0)
                {
                    refreshAllCheckGroupANDTrvCheckGroup(infrmApplUserGroup);
                    refreshApplUnitDetail();
                }
                else
                {
                    MessageBox.Show("������ɾ��ʧ�ܣ�", "������ɾ��", MessageBoxButtons.OK);
                }
            }

            //�û��Զ�����
            if (infrmApplUserGroup.trvCheckGroup.SelectedNode.Tag is clsApplUserGroup_VO)
            {
                DialogResult objResult = MessageBox.Show("ȷ��Ҫɾ���ü�¼��", "", MessageBoxButtons.OKCancel);
                if (objResult == DialogResult.Cancel)
                {
                    return lngRes;
                }

                TreeNode objTreeNode = infrmApplUserGroup.trvCheckGroup.SelectedNode;
                string strParentUserGroupID = null;
                if (infrmApplUserGroup.trvCheckGroup.SelectedNode != infrmApplUserGroup.trvCheckGroup.Nodes[1])
                {
                    clsApplUserGroup_VO userGroupVo = objTreeNode.Tag as clsApplUserGroup_VO;
                    clsApplUserGroup_VO userGroupParentVo = objTreeNode.Parent.Tag as clsApplUserGroup_VO;
                    string userGroupId = string.Empty;

                    //�Ǹ�
                    if (userGroupParentVo == null)
                    {
                        if (userGroupVo != null)
                        {
                            clsApplUnit_VO[] arrAppUnits = null;
                            new clsDomainController_AppGroupManage().m_lngGetApplUnitByUserGroupID(userGroupVo.strUserGroupID, out arrAppUnits);
                            if (arrAppUnits != null && arrAppUnits.Length > 0)
                            {
                                MessageBox.Show("�Զ�����������ڵ��µ����뵥Ԫ��Ϊ�գ�", "�Զ���ɾ��");
                                return -1;
                            }
                            if (arrAppUnits != null && arrAppUnits.Length == 0)
                            {
                                userGroupId = userGroupVo.strUserGroupID;
                            }
                        }
                    }
                    else
                    {
                        userGroupId = userGroupParentVo.strUserGroupID;
                    }
                }
                lngRes = objDomainAppGroup.m_lngDelApplUserGroupInfo(infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim(), strParentUserGroupID);
                if (lngRes > 0)
                {
                    refreshAllCheckGroupANDTrvCheckGroup(infrmApplUserGroup);
                    refreshApplUnitDetail();
                }
                else
                {
                    MessageBox.Show("�Զ�����ɾ��ʧ�ܣ�", "�Զ���ɾ��", MessageBoxButtons.OK);
                }
            }
            return lngRes;
        }

        #endregion

        #region �޸����뵥Ԫ���û��Զ��������� ͯ�� 2004.05.28		xing.chen modify

        public long m_lngUpdApplUnitAndApplUserGroup(frmApplUserGroup infrmApplUserGroup)
        {
            long lngRes = 0;
            //��¼ѡ�нڵ�ĺۼ�
            ArrayList arlIdx = new ArrayList();
            TreeNode objTreeNode = infrmApplUserGroup.trvCheckGroup.SelectedNode;
            while (objTreeNode != null)
            {
                arlIdx.Add(objTreeNode.Index);
                objTreeNode = objTreeNode.Parent;
            }
            //���뵥Ԫ
            if (infrmApplUserGroup.trvCheckGroup.SelectedNode.Tag is clsApplUnit_VO)
            {
                if (infrmApplUserGroup.lsvAddCheckItem.Items.Count == 0)
                {
                    MessageBox.Show("������Ӽ�����Ŀ��", "���뵥Ԫ", MessageBoxButtons.OK);
                    return -1;
                }
                clsDomainController_ApplyUnitManage objDomainApplyUnit = new clsDomainController_ApplyUnitManage();
                //�������뵥ԪVO
                clsApplUnit_VO objApplUnitVO = new clsApplUnit_VO();
                objApplUnitVO.strApplUnitID = infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim();
                objApplUnitVO.strApplUnitName = infrmApplUserGroup.txtCheckGroupName.Text.ToString().Trim();
                objApplUnitVO.strAssistCode01 = infrmApplUserGroup.txtAssist01.Text.ToString().Trim();
                objApplUnitVO.strAssistCode02 = infrmApplUserGroup.txtAssist02.Text.ToString().Trim();
                objApplUnitVO.strPYCode = infrmApplUserGroup.txtPyCode.Text.ToString().Trim();
                objApplUnitVO.strWBCode = infrmApplUserGroup.txtWbCode.Text.ToString().Trim();
                objApplUnitVO.strCheckCategoryID = infrmApplUserGroup.cboCheckCategory.SelectedValue.ToString().Trim();
                objApplUnitVO.strOtherName = infrmApplUserGroup.txtApplUnitOtherName.Text.ToString().Trim();
                objApplUnitVO.strPrice = infrmApplUserGroup.m_txtPrice.Text.ToString().Trim();
                objApplUnitVO.strCost = infrmApplUserGroup.m_txtCost.Text.ToString().Trim();
                objApplUnitVO.strSampleTypeID = infrmApplUserGroup.m_cboSampleType.SelectedValue == null ? null : infrmApplUserGroup.m_cboSampleType.SelectedValue.ToString();

                //xing.chen add
                objApplUnitVO.strSummary = infrmApplUserGroup.txtSummary.Text.ToString().Trim();

                if (infrmApplUserGroup.chkBodyCheck.Checked)
                {
                    objApplUnitVO.strIsPhysicsExamRequired = "1";
                }
                else
                {
                    objApplUnitVO.strIsPhysicsExamRequired = "0";
                }
                if (infrmApplUserGroup.chkNoFood.Checked)
                {
                    objApplUnitVO.strIsNoFoodRequired = "1";
                }
                else
                {
                    objApplUnitVO.strIsNoFoodRequired = "0";
                }
                if (infrmApplUserGroup.chkReservation.Checked)
                {
                    objApplUnitVO.strIsReservationRequired = "1";
                }
                else
                {
                    objApplUnitVO.strIsReservationRequired = "0";
                }


                objApplUnitVO.strOutCheckFlag = (infrmApplUserGroup.chkOutCheckFlag.Checked ? "1" : "0");
                objApplUnitVO.ReportHour = Convert.ToDecimal(infrmApplUserGroup.txtReportHour.Text);
                objApplUnitVO.SamplingInstr = infrmApplUserGroup.txtSamplingInstr.Text;

                lngRes = objDomainApplyUnit.m_lngSetApplyUnit(objApplUnitVO, this.m_arlAddDetail, this.m_arlRemoveDetail);
                if (lngRes > 0)
                {
                    m_arlAddDetail.Clear();
                    m_arlRemoveDetail.Clear();
                    try
                    {
                        int intCount = arlIdx.Count - 1;
                        objTreeNode = infrmApplUserGroup.trvCheckGroup.Nodes[(int)arlIdx[intCount]];
                        intCount--;
                        while (intCount >= 0)
                        {
                            objTreeNode = objTreeNode.Nodes[(int)arlIdx[intCount]];
                            intCount--;
                        }
                        infrmApplUserGroup.trvCheckGroup.SelectedNode = objTreeNode;
                        infrmApplUserGroup.trvCheckGroup.SelectedNode.Text = "(" + objApplUnitVO.strApplUnitID + ")" + objApplUnitVO.strApplUnitName;
                        infrmApplUserGroup.trvCheckGroup.SelectedNode.Tag = objApplUnitVO;
                    }
                    catch { }
                    //					refreshAllCheckGroupANDTrvCheckGroup(infrmApplUserGroup);
                }
                //				lngRes = objDomainApplyUnit.m_lngDelApplUnitDetail(infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim());
                //				if(lngRes > 0)
                //				{
                //					lngRes = AddNewApplUserGroup(infrmApplUserGroup);
                //				}
                //				if(lngRes > 0)
                //				{
                //					m_mthGetApplUnitByID(infrmApplUserGroup,infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim());
                //				}
            }
            //�Զ�����
            if (infrmApplUserGroup.trvCheckGroup.SelectedNode.Tag is clsApplUserGroup_VO)
            {



                objTreeNode = infrmApplUserGroup.trvCheckGroup.SelectedNode;
                clsDomainController_AppGroupManage objDomainApplGroup = new clsDomainController_AppGroupManage();
                lngRes = objDomainApplGroup.m_lngDelApplUserGroupDetailAndRelation(infrmApplUserGroup.txtCheckGroupNo.Text.ToString().Trim());
                if (lngRes > 0)
                {
                    lngRes = AddNewApplUserGroup(infrmApplUserGroup);
                }
            }
            if (lngRes > 0)
            {

            }
            return lngRes;
        }

        #endregion

        #region ����ID��ѯ�ڵ�

        /// <summary>
        /// �������뵥ԪID��ȡ���뵥Ԫ�ڵ�
        /// </summary>
        /// <param name="infrmApplUserGroup"></param>
        /// <param name="p_strApplUnitID"></param>
        private void m_mthGetApplUnitByID(frmApplUserGroup infrmApplUserGroup, string p_strApplUnitID)
        {
            for (int i = 0; i < infrmApplUserGroup.trvCheckGroup.Nodes[0].Nodes.Count; i++)
            {
                for (int j = 0; j < infrmApplUserGroup.trvCheckGroup.Nodes[0].Nodes[i].Nodes.Count; j++)
                {
                    if (p_strApplUnitID == ((clsApplUnit_VO)infrmApplUserGroup.trvCheckGroup.Nodes[0].Nodes[i].Nodes[j].Tag).strApplUnitID)
                    {
                        lsvCheckGroupSelectIndexChanged(infrmApplUserGroup, infrmApplUserGroup.trvCheckGroup.Nodes[0].Nodes[i].Nodes[j]);
                        infrmApplUserGroup.trvCheckGroup.Focus();
                        infrmApplUserGroup.trvCheckGroup.SelectedNode = infrmApplUserGroup.trvCheckGroup.Nodes[0].Nodes[i].Nodes[j];
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// �����û��Զ�����ID��ȡ�û��Զ�����ڵ�
        /// </summary>
        /// <param name="infrmApplUserGroup"></param>
        /// <param name="p_strApplUserGroupID"></param>
        private void m_mthGetApplUserGroupByID(frmApplUserGroup infrmApplUserGroup, string p_strApplUserGroupID)
        {
            for (int i = 0; i < infrmApplUserGroup.trvCheckGroup.Nodes[1].Nodes.Count; i++)
            {
                if (p_strApplUserGroupID == ((clsApplUserGroup_VO)infrmApplUserGroup.trvCheckGroup.Nodes[1].Nodes[i].Tag).strUserGroupID)
                {
                    lsvCheckGroupSelectIndexChanged(infrmApplUserGroup, infrmApplUserGroup.trvCheckGroup.Nodes[1].Nodes[i]);
                    infrmApplUserGroup.trvCheckGroup.Focus();
                    infrmApplUserGroup.trvCheckGroup.SelectedNode = infrmApplUserGroup.trvCheckGroup.Nodes[1].Nodes[i];
                    break;
                }
            }
        }

        #endregion

        #region ��trvSubGroup��ѡ�в�ͬ�Ľڵ���ʾ��Ӧ�ļ�����Ŀ��Ϣ ͯ�� 2004.05.28

        public long m_lngGetCheckItemByTreeNode(frmApplUserGroup infrmApplUserGroup, TreeNode objTreeNode)
        {
            long lngRes = 0;
            infrmApplUserGroup.lsvSubGroupCheckItem.Items.Clear();
            //���뵥Ԫ
            if (objTreeNode.Tag is clsApplUnit_VO)
            {
                clsDomainController_ApplyUnitManage objDomainApplUnit = new clsDomainController_ApplyUnitManage();
                DataTable dtbCheckItem = null;
                lngRes = objDomainApplUnit.m_lngGetCheckItemByApplUnitID(((clsApplUnit_VO)objTreeNode.Tag).strApplUnitID, out dtbCheckItem);
                if (dtbCheckItem != null && dtbCheckItem.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbCheckItem.Rows.Count; i++)
                    {
                        ListViewItem objlsvItem = new ListViewItem();
                        objlsvItem.Text = dtbCheckItem.Rows[i]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                        objlsvItem.SubItems.Add(dtbCheckItem.Rows[i]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim());
                        objlsvItem.SubItems.Add(dtbCheckItem.Rows[i]["CHECK_ITEM_NAME_VCHR"].ToString().Trim());
                        objlsvItem.Tag = dtbCheckItem.Rows[i];
                        infrmApplUserGroup.lsvSubGroupCheckItem.Items.Add(objlsvItem);
                    }
                }
            }
            if (objTreeNode.Tag is clsApplUserGroup_VO)
            {
                clsDomainController_AppGroupManage objDomainAppGroup = new clsDomainController_AppGroupManage();
                DataTable dtbCheckItem = null;
                if (((clsApplUserGroup_VO)objTreeNode.Tag).strHasChildGroup == "1")
                {
                    objDomainAppGroup.m_lngGetCheckItemApplGroupRelationByApplUserGroupID(((clsApplUserGroup_VO)objTreeNode.Tag).strUserGroupID, out dtbCheckItem);
                    if (dtbCheckItem != null && dtbCheckItem.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtbCheckItem.Rows.Count; i++)
                        {
                            ListViewItem objlsvItem = new ListViewItem();
                            objlsvItem.Text = dtbCheckItem.Rows[i]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                            objlsvItem.SubItems.Add(dtbCheckItem.Rows[i]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim());
                            objlsvItem.SubItems.Add(dtbCheckItem.Rows[i]["CHECK_ITEM_NAME_VCHR"].ToString().Trim());
                            objlsvItem.Tag = dtbCheckItem.Rows[i];
                            infrmApplUserGroup.lsvSubGroupCheckItem.Items.Add(objlsvItem);
                        }
                    }
                    dtbCheckItem = null;
                }
                objDomainAppGroup.m_lngGetCheckItemApplGroupDetailByApplUserGroupID(((clsApplUserGroup_VO)objTreeNode.Tag).strUserGroupID, out dtbCheckItem);
                if (dtbCheckItem != null && dtbCheckItem.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbCheckItem.Rows.Count; i++)
                    {
                        ListViewItem objlsvItem = new ListViewItem();
                        objlsvItem.Text = dtbCheckItem.Rows[i]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                        objlsvItem.SubItems.Add(dtbCheckItem.Rows[i]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim());
                        objlsvItem.SubItems.Add(dtbCheckItem.Rows[i]["CHECK_ITEM_NAME_VCHR"].ToString().Trim());
                        objlsvItem.Tag = dtbCheckItem.Rows[i];
                        infrmApplUserGroup.lsvSubGroupCheckItem.Items.Add(objlsvItem);
                    }
                }
            }
            return lngRes;
        }

        #endregion

    }
}
