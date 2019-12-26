using System;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsDomainController_SampleGroupManage ��ժҪ˵����
    /// ���� 2004.05.26
    /// </summary>
    public class clsDomainController_SampleGroupManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDomainController_SampleGroupManage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region ����걾��TreeView
        public void m_mthGetSampleGroupTreeNodes(out TreeNode[] p_objTreeNodeArr)
        {
            long lngRes = 0;
            p_objTreeNodeArr = null;
            clsCheckCategory_VO[] objCheckCategoryArr = null;
            clsDomainController_CheckItemManage objCheckItemManage = new clsDomainController_CheckItemManage();
            lngRes = objCheckItemManage.m_lngGetCheckCategory(out objCheckCategoryArr);
            if (lngRes > 0 && objCheckCategoryArr != null)
            {
                p_objTreeNodeArr = new TreeNode[objCheckCategoryArr.Length];
                for (int i = 0; i < objCheckCategoryArr.Length; i++)
                {
                    p_objTreeNodeArr[i] = new TreeNode();
                    p_objTreeNodeArr[i].Text = objCheckCategoryArr[i].m_strCheck_Category_Name;
                    p_objTreeNodeArr[i].Tag = objCheckCategoryArr[i];
                }
                clsSampleGroup_VO[] objSampleGroupArr = null;
                lngRes = m_lngGetAllSampleGroup(out objSampleGroupArr);
                if (lngRes > 0 && objSampleGroupArr != null)
                {
                    for (int i = 0; i < objSampleGroupArr.Length; i++)
                    {
                        TreeNode objChildTreeNode = new TreeNode();
                        objChildTreeNode.Text = objSampleGroupArr[i].strSampleGroupName;
                        objChildTreeNode.Tag = objSampleGroupArr[i];
                        for (int j = 0; j < p_objTreeNodeArr.Length; j++)
                        {
                            if (((clsCheckCategory_VO)p_objTreeNodeArr[j].Tag).m_strCheck_Category_ID ==
                                objSampleGroupArr[i].strCheckCategoryID)
                            {
                                p_objTreeNodeArr[j].Nodes.Add(objChildTreeNode);
                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region �������뵥ԪID��ѯ�걾�������뵥Ԫ�Ĺ�ϵ 
        public long m_lngGetSampleGroupUnitByApplUnitID(string p_strApplUnitID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleGroupUnitByApplUnitID(p_strApplUnitID, out p_dtbResult);
            //			objSampleGroupSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����������ID��ѯ���������°��������뵥Ԫ 
        public long m_lngGetApplUnitBySampleGroupID(string p_strSampleGroupID, out clsLisSampleGroupUnit_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetApplUnitBySampleGroupID(p_strSampleGroupID, out p_objResultArr);
            //			objSampleGroupSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ���ݱ걾��ID��ȡ����ı걾����
        public long m_lngGetGroupSampleTypeBySampleGroupID(string p_strSampleGroupID, out clsLisGroupSampleType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetGroupSampleTypeBySampleGroupID(p_strSampleGroupID, out p_objResultArr);
            //			objSampleGroupSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region �����޸ı걾��ı걾�����б� ͯ�� 2004.09.06
        public long m_lngModifyGroupSampleTypeArr(List<clsLisGroupSampleType_VO> p_arlAdd, List<clsLisGroupSampleType_VO> p_arlRemove)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngModifyGroupSampleTypeArr(p_arlAdd, p_arlRemove);
            //			objSampleGroupSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����������ID �õ��������Ӧ�������ͺ��б� 
        /// <summary>
        /// ����������ID �õ��������Ӧ�������ͺ��б�
        /// </summary>
        /// <param name="p_strSampleGroupID"></param>
        /// <param name="p_strSampleGroupModelArr"></param>
        /// <returns></returns>
        public long m_lngGetSampleGroupModelArr(string p_strSampleGroupID, out string[] p_strSampleGroupModelArr)
        {
            long lngRes = 0;
            p_strSampleGroupModelArr = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleGroupModelArr(p_strSampleGroupID, out p_strSampleGroupModelArr);
            //			objSampleGroupSvc.Dispose();

            return lngRes;
        }
        #endregion


        #region �����޸ı걾��������ͺ��б� ͯ�� 2004.08.18
        public long m_lngSetSampleGroupModelArr(List<clsLisSampleGroupModel_VO> p_arlAdd, List<clsLisSampleGroupModel_VO> p_arlRemove)
        {
            long lngRes = 0;

            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngSetSampleGroupModelArr(p_arlAdd, p_arlRemove);
            //			objSampleGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݱ걾��ID��ȡ��Ӧ�������ͺ��б� ͯ�� 2004.08.18
        public long m_lngGetDeviceModelArrBySampleGroupID(string p_strSampleGroupID, out clsLisSampleGroupModel_VO[] p_objResultArr)
        {
            long lngRes = 0;

            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetDeviceModelArrBySampleGroupID(p_strSampleGroupID, out p_objResultArr);
            //			objSampleGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �õ���������б� ���� 2004.07.27
        /// <summary>
        /// �õ���������б�
        /// </summary>
        /// <param name="p_strCategory"></param>
        /// <param name="p_strSampleType"></param>
        /// <param name="p_dtpResult"></param>
        /// <returns></returns>
        public long m_lngGetSampleGroupList(
            string p_strCategory, string p_strSampleType, out DataTable p_dtpResult)
        {
            long lngRes = 0;
            p_dtpResult = null;

            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleGroupList(p_strCategory, p_strSampleType, out p_dtpResult);
            //			objSampleGroupSvc.Dispose();
            return lngRes;
        }
        #endregion


        #region ���ݱ걾��ID��ѯ�걾��VO ͯ�� 2004.06.21
        public long m_lngGetSampleGroupVOBySampleGroupID(string p_strSampleGroupID, out clsSampleGroup_VO p_objSampleGroupVO)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleGroupVOBySampleGroupID(p_strSampleGroupID, out p_objSampleGroupVO);
            //			objSampleGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������뵥ԪID�õ������ڱ걾���VO ���� 2004.06.1
        /// <summary>
        /// ���ݼ�����ĿID�õ������ڱ걾���VO,����ӡ˳�� ���� 2004.06.1
        /// </summary>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_intPrintSeq"></param>
        /// <param name="p_objSampleGroupVO"></param>
        /// <returns></returns>
        public long m_lngGetSampleGoupVOByApplyUnitID(string p_strApplyUnitID, out clsSampleGroup_VO p_objSampleGroupVO)
        {
            long lngRes = 0;
            p_objSampleGroupVO = null;

            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleGoupVOByApplyUnitID(p_strApplyUnitID, out p_objSampleGroupVO);
            //			objSampleGroupSvc.Dispose();
            return lngRes;
        }
        #endregion


        #region ��ȡ���еı걾����ϸ�б� 
        public long m_lngGetAllSampleGroupDetail(out clsSampleGroupDetail_VO[] objSampleGroupDetailVOList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllSampleGroupDetail(out objSampleGroupDetailVOList);
            //			objSampleGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ����������ϸ ͯ�� 2004.05.25
        public long m_lngDelSampleGroupDetail(string strSampleGroupID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelSampleGroupDetail(strSampleGroupID);
            //			objSampleGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ���걾�鼰����ϸ ͯ�� 2004.05.25
        public long m_lngDelSampleGroupAndDetail(string strSampleGroupID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelSampleGroupAndDetail(strSampleGroupID);
            //			objSampleGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����걾�鼰����ϸ ͯ�� 2004.05.25
        public long m_lngAddSampleGroup(ref clsSampleGroup_VO p_objSampleGroupVO, ref clsLisSampleGroupUnit_VO[] p_objSampleGroupUnitList,
            clsApplUnitDetail_VO[] p_objApplUnitArr, List<clsLisSampleGroupModel_VO> p_arlAdd, List<clsLisSampleGroupModel_VO> p_arlRemove,
            List<clsLisGroupSampleType_VO> p_arlAddSampleType, List<clsLisGroupSampleType_VO> p_arlRemoveSampleType)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngAddSampleGroupAndDetail(ref p_objSampleGroupVO, ref p_objSampleGroupUnitList, p_objApplUnitArr
               , p_arlAdd, p_arlRemove, p_arlAddSampleType, p_arlRemoveSampleType); 
            //			objSampleGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡĳһ�걾���µ����м�����Ŀ��Ϣ ͯ�� 2004.05.25
        public long m_lngGetCheckItemBySampleGroupID(string strSampleGroupID, out DataTable dtbCheckItem)
        {
            long lngRes = 0;
            dtbCheckItem = null;
            lngRes = proxy.Service.m_lngGetCheckItemBySampleGroupID(strSampleGroupID, out dtbCheckItem);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���еı걾���б� ͯ�� 2004.05.24
        public long m_lngGetAllSampleGroup(out clsSampleGroup_VO[] objSampleGroupVOList)
        {
            long lngRes = 0;
            objSampleGroupVOList = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAllSampleGroup(ref objSampleGroupVOList);
            //			objSampleGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region	�������뵥ȡ�ö�Ӧ�����Ĳ���˵�� 
        public long m_lngGetSampleRemark(string p_ApplicationID, out clsSampleGroup_VO[] p_objSampleGroupVOArr)
        {
            long lngRes = 0;
            p_objSampleGroupVOArr = null;
            lngRes = proxy.Service.m_lngGetSampleRemark(p_ApplicationID, out p_objSampleGroupVOArr);
            return lngRes;
        }
        #endregion

        #region ��ȡ�Ƿ���ʾδ�շ�״̬���뵥��������Ϣ	xing.chen 
        public long m_lngGetCollocate(out bool p_blnConfig, string p_strSetID)
        {
            p_blnConfig = false;
            long lngRes = proxy.Service.m_lngGetCollocate(out p_blnConfig, p_strSetID);
            return lngRes;
        }
        #endregion
    }
}
