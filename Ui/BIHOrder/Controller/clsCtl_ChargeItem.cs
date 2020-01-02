using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 
using com.digitalwave.iCare.BIHOrder.Control; 
using System.Collections;
using System.Collections.Generic;

namespace com.digitalwave.iCare.BIHOrder
{
    class clsCtl_ChargeItem : com.digitalwave.GUI_Base.clsController_Base
    {
        /// <summary>
        /// �޸�Ŀ�겡����־ 
        /// </summary>
        public bool DeptTag = false;
        /// <summary>
        /// ҽ����Ӧ���ñ�����к� T_OPR_BIH_ORDERCHARGEDEPT
        /// </summary>
        public string p_strSeq_int = "";
        #region ����
        clsDcl_CommitOrder m_objManager = null;

        #endregion

        #region ���캯��
        public clsCtl_ChargeItem()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_objManager = new clsDcl_CommitOrder();

        }
        #endregion

        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmChargeItem m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmChargeItem)frmMDI_Child_Base_in;

        }
        #endregion

        internal void LoadData(string m_strSeq_int, out clsBIHChargeItem objChargeItem, out clsBIHExecOrder order)
        {
            //DeptTag = true;
            long lngRes = 0;
            p_strSeq_int = m_strSeq_int;
            lngRes = m_objManager.m_lngGetORDERCHARGEDEPT(m_strSeq_int, out objChargeItem, out order );
            //if (DeptTag)
            //{
            //    deableContorl();
            //}

        }

        private void deableContorl()
        {
            //m_objViewer.m_txtInputCode.Enabled = false;
            //m_objViewer.m_txtDes.Enabled = false;
            //m_objViewer.m_txtDiscount.Enabled = false;
            //m_objViewer.m_txtGet.Enabled = false;
        }

        internal long m_mthFindChargeItem(string strFindCode, out List<string>[] arrItem)
        {
            long lngRes = 0;
            clsBIHChargeItem objItem;

            objItem = (clsBIHChargeItem)m_objViewer.m_txtInputCode.Tag;
            string m_strItemID = objItem.m_strItemID;
            lngRes = m_objManager.m_lngGetDEPTList(strFindCode, m_strItemID, out arrItem, true);
            /*
            if (p_strSeq_int.Trim().Equals(""))
            {
                string m_strOrdercateid_chr = "";
                if (m_objViewer.m_txtInputCode.Tag != null)
                {
                    m_strOrdercateid_chr = ((clsBIHChargeItem)m_objViewer.m_txtInputCode.Tag).m_strItemID;
                }
                if (!m_strOrdercateid_chr.Trim().Equals(""))
                {
                    lngRes = m_objManager.m_lngGetDEPTList(strFindCode, m_strOrdercateid_chr, out arrItem,true);
                }
                else
                {
                    lngRes = m_objManager.m_lngGetDEPTList(strFindCode,  out arrItem);
                }
           
            }
            else
            {
                lngRes = m_objManager.m_lngGetDEPTList(strFindCode, p_strSeq_int, out arrItem);
            }
             */
            return lngRes;
        }

        internal void SaveTheDeptChange()
        {
            long lngRes = 0;
            string m_strClacarea_chr = "";
            m_strClacarea_chr = ((ArrayList)m_objViewer.ctlCLACAREA_CHR.Tag)[0].ToString();
            lngRes = m_objManager.SaveTheDeptChange(p_strSeq_int, m_strClacarea_chr);

        }

        internal long m_lngGetDEPTDefault(string m_strCREATEAREA_ID, string m_strItemID, out System.Collections.Generic.List<string> arrItem)
        {
            long lngRes = 0;

            lngRes = m_objManager.m_lngGetDEPTDefault(m_strCREATEAREA_ID, m_strItemID, out arrItem);
            return lngRes;
        }
    }
}
