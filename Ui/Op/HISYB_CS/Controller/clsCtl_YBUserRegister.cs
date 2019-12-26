using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsCtl_YBUserRegister : com.digitalwave.GUI_Base.clsController_Base
    {
        clsDcl_YB objDomain;
        /// <summary>
        /// ��ǰѡ����
        /// </summary>
        private int m_SelRow = 0;
        /// <summary>
        /// ����VO
        /// </summary>
        clsDGYBjbrzc_VO m_objItem;
        /// <summary>
        /// �б�����
        /// </summary>
        List<clsDGYBjbrzc_VO> objAllItem = new List<clsDGYBjbrzc_VO>();

        public clsCtl_YBUserRegister()
        {
            objDomain = new clsDcl_YB();
        }

        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmUserRegister m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmUserRegister)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ʼ������
        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void m_mthInit()
        {
            //ҽԺ������ע����סԺ��¼
            string strUser = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "YYBHZY", "AnyOne");
            string strPwd = clsYBPublic_cs.m_strReadXML("DGCSZYYB", "PASSWORDZY", "AnyOne");
            long lngRes=clsYBPublic_cs.m_lngUserLoin(strUser, strPwd,false);
            if (lngRes < 0)
            {
                MessageBox.Show("��ʼ��ʧ�ܣ������´򿪣�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            m_mthGetDetailList();

        }
        #endregion

        #region ����б�����
        /// <summary>
        /// ����б�����
        /// </summary>
        public void m_mthGetDetailList()
        {
            this.m_objViewer.m_lsvDetail.Items.Clear();
            clsDGYBjbrzc_VO[] objItemArr = new clsDGYBjbrzc_VO[0];
            long lngRes = this.objDomain.m_lngGetUserRegister(out objItemArr);

            if (lngRes > 0 && objItemArr.Length > 0)
            {
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    objAllItem.Add(objItemArr[i1]);
                    m_mthInsertDetail(objItemArr[i1]);
                }
                if (this.m_objViewer.m_lsvDetail.Items.Count != 0)
                    this.m_objViewer.m_lsvDetail.Items[0].Selected = true;
            }
        }
        #endregion

        #region ���б����һ����¼
        /// <summary>
        /// ���б����һ������
        /// </summary>
        /// <param name="objItem"></param>
        private void m_mthInsertDetail(clsDGYBjbrzc_VO objItem)
        {
            if (objItem != null)
            {
                ListViewItem lsvItem = new ListViewItem(objItem.YYBH.ToString());
                lsvItem.SubItems.Add(objItem.JBR);
                //�ֵ��� 1����ͨ������ 2�������շ�Ա 3�������ն�
                string strJbrlx;
                switch (objItem.JBRLX)
                {
                    case "1":
                        strJbrlx = "��ͨ������";
                        break;
                    case "2":
                        strJbrlx = "�����շ�Ա";
                        break;
                    case "3":
                        strJbrlx = "�����ն�";
                        break;
                    default:
                        strJbrlx = "��ͨ������";
                        break;
                }
                lsvItem.SubItems.Add(strJbrlx);
                lsvItem.SubItems.Add(objItem.XM);
                lsvItem.SubItems.Add(objItem.GMSFHM);
                lsvItem.SubItems.Add(objItem.SSKS);
                //�ֵ� 1������ 2���޸�
                lsvItem.SubItems.Add(objItem.BGLX);
                lsvItem.SubItems.Add(objItem.JLSJ);
                lsvItem.Tag = objItem;
                this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);
            }
        }
        #endregion

        #region ��ϸ�б�˫��
        /// <summary>
        /// �б�˫���¼�
        /// </summary>
        public void m_mthDetailSel()
        {
            if (this.m_objViewer.m_lsvDetail.SelectedItems.Count > 0)
            {
                if (this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag != null)
                {
                    clsDGYBjbrzc_VO objItem = (clsDGYBjbrzc_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;
                    this.m_SelRow = this.m_objViewer.m_lsvDetail.SelectedItems[0].Index;
                    m_mthSetViewInfo(objItem);
                }
            }
        }
        #endregion

        #region ���ô�������
        /// <summary>
        /// ���ô�������
        /// </summary>
        /// <param name="objItem">��Һ�кſ���̨����</param>
        public void m_mthSetViewInfo(clsDGYBjbrzc_VO objItem)
        {
            this.m_objItem = objItem;

            if (this.m_objItem == null)
            {
                m_mthClear();
                this.m_objViewer.txtName.Focus();
            }
            else
            {
                m_mthClear();
                this.m_objViewer.txtName.Text = this.m_objItem.XM.Trim();
                this.m_objViewer.txtIdcard.Text = this.m_objItem.GMSFHM.Trim();
                this.m_objViewer.txtDeptName.Text = this.m_objItem.SSKS.Trim();
                this.m_objViewer.txtJbr.Text = this.m_objItem.JBR.Trim();
                string strJbrlx=this.m_objItem.JBRLX.Trim();
                int intUserType=0;
                switch (strJbrlx)
                { 
                    case "1":
                        intUserType=0;
                        break;
                    case "2":
                        intUserType=1;
                        break;
                    case "3":
                        intUserType=2;
                        break;
                    default:
                        intUserType=0;
                        break;

                }
                this.m_objViewer.cmb_UserType.SelectedIndex=intUserType;
                this.m_objViewer.txtName.Focus();
            }
        }
        #endregion

        #region ��ձ༭������
        /// <summary>
        /// ����༭������
        /// </summary>
        private void m_mthClear()
        {
            this.m_objViewer.txtName.Clear();
            this.m_objViewer.txtIdcard.Clear();
            this.m_objViewer.txtDeptName.Clear();
            this.m_objViewer.txtJbr.Clear(); 
            if (this.m_objViewer.cmb_UserType.Items.Count > 0)
            {
                this.m_objViewer.cmb_UserType.SelectedIndex = 0;
            }//Ĭ��
        }
        #endregion


        #region ҽԺ������ע��
        /// <summary>
        /// ҽԺ������ע��
        /// </summary>
        /// <param name="m_intBglx">�������:�ֵ�1������2���޸�</param>
        public void m_mthUserRegister(int m_intBglx)
        {
            if (this.m_objItem == null)
            {
                m_intBglx = 1;

            }
            else
            {
                m_intBglx = 2;
            }
            m_objItem = new clsDGYBjbrzc_VO();
            m_objItem.YYBH = this.m_objViewer.txtHospitalNO.Text.Trim();
            m_objItem.XM = this.m_objViewer.txtName.Text.Trim();
            m_objItem.GMSFHM = this.m_objViewer.txtIdcard.Text.Trim();
            m_objItem.SSKS = this.m_objViewer.txtDeptName.Text.Trim();
            m_objItem.JBR = this.m_objViewer.txtJbr.Text.Trim();
            m_objItem.JBRLX = this.m_objViewer.cmb_UserType.Text.Trim().Split('-')[0].ToString();
            m_objItem.BGLX = m_intBglx.ToString();
            if (string.IsNullOrEmpty(m_objItem.YYBH) || string.IsNullOrEmpty(m_objItem.XM) || string.IsNullOrEmpty(m_objItem.GMSFHM) || string.IsNullOrEmpty(m_objItem.JBR) || string.IsNullOrEmpty(m_objItem.JBRLX))
            {
                MessageBox.Show("����Ϊ�գ����飡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            long lngRes = clsYBPublic_cs.m_lngUserRegister(m_objItem);
            if (lngRes > 0)
            {
                lngRes = this.objDomain.m_lngSaveUserRegister(m_objItem);
                if (m_intBglx == 1)
                {
                    m_mthModifyDetail(m_objItem);
                    objAllItem.Add(m_objItem);
                    MessageBox.Show("ҽԺ������ע��ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    m_mthModifyDetail(m_objItem);
                    MessageBox.Show("ҽԺ�������޸ĳɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            m_mthClear();

        }
        #endregion

        #region �޸��б�����
        /// <summary>
        /// �޸��б�����
        /// </summary>
        /// <param name="objItem">ע������</param>
        private void m_mthModifyDetail(clsDGYBjbrzc_VO objItem)
        {
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[0].Text = m_objItem.YYBH;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[1].Text = m_objItem.JBR;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[2].Text = m_objItem.JBRLX;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[3].Text = m_objItem.XM;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[4].Text = m_objItem.GMSFHM;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[5].Text = m_objItem.SSKS;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[6].Text = m_objItem.BGLX;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[7].Text = m_objItem.JLSJ.Length == 0 ? System.DateTime.Now.ToString() : m_objItem.JLSJ;
        }
        #endregion
    }
}
