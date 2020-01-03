using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using weCare.Core.Entity;
using com.digitalwave.Utility;


namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmMultiunit_drug : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        private bool blnisCmdNewOpen = false;
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmMultiunit_drug()
        {
            InitializeComponent();

        }
        #endregion

        #region �������Ʋ�

        /// <summary>
        /// �������Ʋ�
        /// </summary>
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_Multiunit_drug();
            this.objController.Set_GUI_Apperance(this);
        }
        #endregion

        #region �����ʼ��
        /// <summary>
        /// �����ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMultiunit_drug_Load(object sender, EventArgs e)
        {
            this.cmbMKey.SelectedIndex = 1;
            ((clsCtl_Multiunit_drug)this.objController).m_mthInit();
            m_mthSetBtnStatus();
            this.cmbMKey.Focus();
        }
        #endregion

        #region ��ʾȫ��ҩƷ�¼�
        private void cmdShowAll_Click(object sender, EventArgs e)
        {
            if (blnisCmdNewOpen)
            {
                blnisCmdNewOpen = false;
                //�ر�txtUnitName
                this.txtUnitName.ReadOnly = true;
                ((clsCtl_Multiunit_drug)this.objController).m_mthLoadDvMultiUnit();
                ((clsCtl_Multiunit_drug)this.objController).m_mthDgvMultiUnitDataBind();
                //���°ѵ�λ�б��ѡ����������ʾ��textbox��
                // this.dtgMultiUnitList.Rows[e.RowIndex].Selected = true;
                // ((clsCtl_Multiunit_drug)objController).m_mthTxtLoadData();
                m_mthSetBtnStatus();
            }

            ((clsCtl_Multiunit_drug)this.objController).m_mthShowAllMedicine();

            m_mthSetBtnStatus();
            this.txtMKey.Text = "";
        }
        #endregion

        #region ����ҩƷ��ť�¼�
        /// <summary>
        /// ����ҩƷ��ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdFind_Click(object sender, EventArgs e)
        {
            if (blnisCmdNewOpen)
            {
                blnisCmdNewOpen = false;
                //�ر�txtUnitName
                this.txtUnitName.ReadOnly = true;
                ((clsCtl_Multiunit_drug)this.objController).m_mthDgvMultiUnitDataBind();
                //���°ѵ�λ�б��ѡ����������ʾ��textbox��
                // this.dtgMultiUnitList.Rows[e.RowIndex].Selected = true;
                //((clsCtl_Multiunit_drug)objController).m_mthTxtLoadData();
                m_mthSetBtnStatus();
            }

            if (this.txtMKey.Text.Trim().Equals(""))
            {
                MessageBox.Show("������ҩƷ����������", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtMKey.Focus();
                return;
            }
            ((clsCtl_Multiunit_drug)objController).m_mthQueryMedicine();
            m_mthSetBtnStatus();
        }
        #endregion

        #region ���dtgMedicineList�������¼�
        /// <summary>
        /// ���dtgMedicineList�������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgMedicineList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (blnisCmdNewOpen)
            {
                blnisCmdNewOpen = false;
                //�ر�txtUnitName
                //this.txtUnitName.ReadOnly = true;

            }
            if (e.RowIndex > -1)
            {
                //��ʾ�Ƿ񱣴���������
                SaveNewPerPrompt(sender, e);
                this.dtgMedicineList.Rows[e.RowIndex].Selected = true;
                ((clsCtl_Multiunit_drug)objController).m_mthShowSeledMedName();
                string strMedSeledId = this.dtgMedicineList.SelectedRows[0].Cells["ColumnMedicineId"].Value.ToString();
                ((clsCtl_Multiunit_drug)this.objController).m_mthLoadDvMultiUnit();
                ((clsCtl_Multiunit_drug)this.objController).m_mthDgvMultiUnitDataBind();
                if (this.dtgMultiUnitList.Rows.Count > 0)
                {
                    this.dtgMultiUnitList.Rows[0].Selected = true;
                }
                ((clsCtl_Multiunit_drug)this.objController).m_mthTxtLoadData();
            }
            m_mthSetBtnStatus();
        }
        #endregion

        #region ������"ɾ��"��"����"��ť��״̬
        /// <summary>
        /// ������"ɾ��"��"����"��ť��״̬
        /// </summary>
        private void m_mthSetBtnStatus()
        {
            if (this.dtgMultiUnitList.Rows.Count > 0)
            {
                this.cmdNew.Enabled = true;
                this.cmdDelete.Enabled = true;
                this.cmdSave.Enabled = true;
            }
            else
            {
                this.cmdNew.Enabled = true;
                this.cmdDelete.Enabled = false;
                this.cmdSave.Enabled = false;
            }
        }
        #endregion

        #region ������ť�¼�
        /// <summary>
        /// ������ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNew_Click(object sender, EventArgs e)
        {
            if (this.dtgMedicineList.SelectedRows.Count < 1)
            {
                return;
            }

            if (dtgMultiUnitList.DataSource != null)
            {

                if (!blnisCmdNewOpen)
                {
                    //�������textbox������
                    ((clsCtl_Multiunit_drug)objController).m_mthClearTXT();
                    //������Ϊ��������״̬
                    this.blnisCmdNewOpen = true;
                    //��txtUnitName
                    // this.txtUnitName.ReadOnly = false;
                    //��cmdSave
                    this.cmdSave.Enabled = true;
                    //�ر�cmdNew��cmdDelete
                    this.cmdNew.Enabled = false;
                    this.cmdDelete.Enabled = false;
                    this.txtUnitName.Focus();
                }
                else
                {

                }
            }
        }
        #endregion

        #region �����λ�����¼�
        /// <summary>
        /// �����λ�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgMultiUnitList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (blnisCmdNewOpen)
            {
                blnisCmdNewOpen = false;
                //�ر�txtUnitName
                //this.txtUnitName.ReadOnly = true;
            }
            if (e.RowIndex > -1)
            {
                //��ʾ�Ƿ񱣴�������λ
                SaveNewPerPrompt(sender, e);
                this.dtgMultiUnitList.Rows[e.RowIndex].Selected = true;
                ((clsCtl_Multiunit_drug)objController).m_mthTxtLoadData();
            }
            m_mthSetBtnStatus();
        }
        #endregion

        #region ��ʾ�Ƿ񱣴�������λ ����
        /// <summary>
        /// ��ʾ�Ƿ񱣴�������λ
        /// ������������������е�λ���룬������б��¼�����ʾ�Ƿ񱣴�
        /// </summary>
        private void SaveNewPerPrompt(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (blnIsNew)
            //{
            //    if (this.txtUnitName.Text.Trim() != "")
            //    {
            //        if (MessageBox.Show("�������ĵ�λ���ƻ�û�б��棬�Ƿ񱣴棿", "��Ϣ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        {
            //            blnIsNew = false;
            //            m_mthSetBtnStatus(blnIsNew);
            //        }
            //        else
            //        {
            //            cmdSave_Click(sender, e);
            //        }
            //    }
            //    else
            //    {
            //        blnIsNew = false;
            //        m_mthSetBtnStatus(blnIsNew);
            //    }
            //}
        }
        #endregion

        #region ���������ť�¼�
        /// <summary>
        /// ���������ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCancel_Click(object sender, EventArgs e)
        {

            //�ر�����״̬
            this.blnisCmdNewOpen = false;
            //�ر�txtUnitName
            //this.txtUnitName.ReadOnly = true;
            ((clsCtl_Multiunit_drug)this.objController).m_mthLoadDvMultiUnit();
            //����Ϊ��λ�б������
            ((clsCtl_Multiunit_drug)this.objController).m_mthDgvMultiUnitDataBind();
            //���°ѵ�λ�б��ѡ����������ʾ��textbox��
            //((clsCtl_Multiunit_drug)objController).m_mthTxtLoadData();
            m_mthSetBtnStatus();
            ((clsCtl_Multiunit_drug)this.objController).m_mthClearTXT();
            ((clsCtl_Multiunit_drug)this.objController).m_mthTxtLoadData();

        }
        #endregion

        #region ����˳���ť�¼�
        /// <summary>
        /// ����˳���ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.dtgMedicineList.Dispose();
            this.dtgMultiUnitList.Dispose();
            this.Close();
            //if (blnisCmdNewOpen)
            //{
            //    MessageBox.Show("����Ϊ����״̬��\n�뱣�����뿪��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    this.dtgMedicineList.Dispose();
            //    this.dtgMultiUnitList.Dispose();
            //    this.Close();
            //}
        }
        #endregion

        #region ���ɾ����ť�¼�
        /// <summary>
        /// ���ɾ����ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (this.cmdSave.Tag == null)
            {
                return;
            }
            if (ckbCurruseFlag.Checked == true)
            {
                MessageBox.Show("�õ�λ�ǵ�ǰҩƷ��λ\n�����ҩƷ��ǰ��λ���ٽ���ɾ������", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult drIsDel = MessageBox.Show("ȷʵɾ���õ�λ?", "ɾ��ѯ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (drIsDel == DialogResult.Cancel)
            {
                return;
            }

            if (((clsCtl_Multiunit_drug)objController).m_blnDeleteMultiUnit())
            {
                //����Ϊ��λ�б������
                ((clsCtl_Multiunit_drug)this.objController).m_mthLoadDvMultiUnit();
                ((clsCtl_Multiunit_drug)this.objController).m_mthDgvMultiUnitDataBind();
                //���°ѵ�λ�б��ѡ����������ʾ��textbox��
                //if (this.dtgMultiUnitList.Rows.Count > 0)
                //{
                //    //this.dtgMultiUnitList.Rows[0].Selected = true;
                //    //((clsCtl_Multiunit_drug)objController).m_mthTxtLoadData();
                //}
                ((clsCtl_Multiunit_drug)this.objController).m_mthClearTXT();
                m_mthSetBtnStatus();
                this.cmdSave.Tag = null;
                blnisCmdNewOpen = true;
            }
        }
        #endregion

        #region ������水ť�¼�
        /// <summary>
        /// ������水ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            //���������״̬
            if (this.blnisCmdNewOpen)
            {
                bool blnIsNum = ((clsCtl_Multiunit_drug)this.objController).m_blnIsNum();
                //�ж�textbox�Ƿ�Ϊ��,�Ƿ������������
                if (txtUnitName.Text.ToString().Trim().Equals("") || txtPackage.Text.ToString().Trim().Equals("") || blnIsNum)
                {
                    MessageBox.Show("������Ϣ�������������ݸ�ʽ����!\n����д����", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPackage.Focus();
                }
                else
                {
                    //�ж��Ƿ���Ϊ��ǰҩƷ��λ
                    if (ckbCurruseFlag.Checked == true)
                    {
                        if (((clsCtl_Multiunit_drug)this.objController).m_BlnQueryByIndex() == false)
                        {
                            return;
                        }

                        //��������ҩƷ��Ϊ�ǵ�ǰҩƷ
                        if (((clsCtl_Multiunit_drug)objController).m_lngSetAllCurruseFlag_0ByItemId() > -1)
                        {
                            //��������
                            if (((clsCtl_Multiunit_drug)objController).m_blnAddMultiUnit())
                            {
                                //�ر�����״̬
                                this.blnisCmdNewOpen = false;
                                //�ر�txtUnitName
                                //this.txtUnitName.ReadOnly = true;
                                //����Ϊ��λ�б������
                                ((clsCtl_Multiunit_drug)this.objController).m_mthDgvMultiUnitDataBind();
                                //���°ѵ�λ�б��ѡ����������ʾ��textbox��
                                ((clsCtl_Multiunit_drug)objController).m_mthTxtLoadData();
                                m_mthSetBtnStatus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("������ҩƷ��λ��Ϊ�ǵ�ǰ��λʧ��", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        if (((clsCtl_Multiunit_drug)this.objController).m_BlnQueryByIndex() == false)
                        {
                            return;
                        }

                        //�������Ϊ��1��ҩƷ��λ�����û�ȴû�а�����Ϊ��ǰҩƷ����������λ��ΪĬ�ϵ�ǰҩƷ��λ
                        if (this.dtgMultiUnitList.Rows.Count <= 0)
                        {
                            ckbCurruseFlag.Checked = true;
                        }
                        //��������
                        if (((clsCtl_Multiunit_drug)objController).m_blnAddMultiUnit())
                        {
                            //�ر�����״̬
                            this.blnisCmdNewOpen = false;
                            //�ر�txtUnitName
                            //this.txtUnitName.ReadOnly = true;
                            //����Ϊ��λ�б������
                            ((clsCtl_Multiunit_drug)this.objController).m_mthDgvMultiUnitDataBind();
                            //���°ѵ�λ�б��ѡ����������ʾ��textbox��
                            ((clsCtl_Multiunit_drug)objController).m_mthTxtLoadData();
                            m_mthSetBtnStatus();
                        }
                    }

                }

            }
            //��������
            else
            {
                clsMultiunit_drug_VO objTmp = this.cmdSave.Tag as clsMultiunit_drug_VO;
                bool blnCurr = false;
                if (objTmp.m_intCurruseFlag_Int == 1)
                {
                    blnCurr = true;
                }
                else
                {
                    blnCurr = false;
                }
                if (objTmp.m_strUnit == this.txtUnitName.Text.Trim() && objTmp.m_intPackage.ToString() == this.txtPackage.Text
                    && blnCurr == this.ckbCurruseFlag.Checked && objTmp.m_intStauts == this.cboStatus.SelectedIndex)
                {
                    return;
                }

                //�������ж�
                //�ж��Ƿ���Ϊ��ǰҩƷ��λ
                if (ckbCurruseFlag.Checked == true)
                {
                    //if (dtgMultiUnitList.Rows.Count != 1)
                    //{
                        //����:�����ڸ���ʱ����txtUnitName�����е�ǰҩƷʹ�õ�λ��ͬʱ�����쳣��Υ��ΨһԼ����
                        //// ���ڴ���ing ///////////////////

                        //// ���ڴ���ing ///////////////////
                        //��������ҩƷ��Ϊ�ǵ�ǰҩƷ
                        if (((clsCtl_Multiunit_drug)objController).m_lngSetAllCurruseFlag_0ByItemId() > -1)
                        {
                            if (((clsCtl_Multiunit_drug)objController).m_blnUpdateMultiUnit())
                            {
                                //���³ɹ�
                                MessageBox.Show("���³ɹ���", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //����Ϊ��λ�б������
                                ((clsCtl_Multiunit_drug)this.objController).m_mthLoadDvMultiUnit();
                                ((clsCtl_Multiunit_drug)this.objController).m_mthDgvMultiUnitDataBind();
                                //���°ѵ�λ�б��ѡ����������ʾ��textbox��
                                if (this.dtgMultiUnitList.Rows.Count > 0)
                                {
                                    this.dtgMultiUnitList.Rows[0].Selected = true;
                                    ((clsCtl_Multiunit_drug)objController).m_mthTxtLoadData();
                                }

                                m_mthSetBtnStatus();
                            }
                            else
                            {
                                //����ʧ��
                                MessageBox.Show("����ʧ�ܣ�", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("������ҩƷ��λ��Ϊ�ǵ�ǰ��λʧ��", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}

                    }
                    else
                    {
                        MessageBox.Show("��ǰ��Ψһ��λ\n������Ϊ�ǵ�ǰҩƷ��λ", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    //if (dtgMultiUnitList.Rows.Count != 1)
                    //{
                        //���ø���
                        if (((clsCtl_Multiunit_drug)objController).m_blnUpdateMultiUnit())
                        {
                            //���³ɹ�
                            MessageBox.Show("���³ɹ���", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //����Ϊ��λ�б������
                            ((clsCtl_Multiunit_drug)this.objController).m_mthLoadDvMultiUnit();
                            ((clsCtl_Multiunit_drug)this.objController).m_mthDgvMultiUnitDataBind();
                            //���°ѵ�λ�б��ѡ����������ʾ��textbox��
                            if (this.dtgMultiUnitList.Rows.Count > 0)
                            {
                                this.dtgMultiUnitList.Rows[0].Selected = true;
                                ((clsCtl_Multiunit_drug)objController).m_mthTxtLoadData();
                            }

                            m_mthSetBtnStatus();
                        }
                        else
                        {
                            //����ʧ��
                            MessageBox.Show("����ʧ�ܣ�", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("��ǰ��Ψһ��λ\n������Ϊ�ǵ�ǰҩƷ��λ", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }

            }
        }
        #endregion

        #region ��ҩƷ���ҹؼ��������ı��а�Enter��
        private void txtMKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                cmdFind_Click(sender, e);
            }
        }
        #endregion

        #region ��ҩƷ�б����ƶ��ϡ��¼�
        private void dtgMedicineList_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.dtgMedicineList.SelectedRows.Count <= 0)
            {
                return;
            }
            //�ر�����״̬
            this.blnisCmdNewOpen = false;
            //�ر�txtUnitName
            //this.txtUnitName.ReadOnly = true;
            int index = this.dtgMedicineList.SelectedRows[0].Index;

            if (e.KeyCode == Keys.Up)
            {
                index -= 1;
                if (index < 0)
                    return;

            }
            if (e.KeyCode == Keys.Down)
            {
                index += 1;
                if (index > this.dtgMedicineList.Rows.Count - 1)
                    return;
            }
            this.dtgMedicineList.Rows[index].Selected = true;
            //����Ϊ��λ�б������
            ((clsCtl_Multiunit_drug)this.objController).m_mthLoadDvMultiUnit();
            ((clsCtl_Multiunit_drug)this.objController).m_mthDgvMultiUnitDataBind();

            if (this.dtgMultiUnitList.Rows.Count > 0)
            {
                this.dtgMultiUnitList.Rows[0].Selected = true;
            }
            ((clsCtl_Multiunit_drug)this.objController).m_mthTxtLoadData();
            ((clsCtl_Multiunit_drug)this.objController).m_mthShowSeledMedName();
        }
        #endregion

        #region �ڵ�λ�б����ƶ��ϡ��¼�
        private void dtgMultiUnitList_KeyDown(object sender, KeyEventArgs e)
        {
            //�ر�����״̬
            this.blnisCmdNewOpen = false;
            //�ر�txtUnitName
            //this.txtUnitName.ReadOnly = true;
            int index = this.dtgMultiUnitList.SelectedRows[0].Index;
            if (e.KeyCode == Keys.Up)
            {
                index -= 1;
                if (index < 0)
                    return;
            }
            if (e.KeyCode == Keys.Down)
            {
                index += 1;
                if (index > this.dtgMultiUnitList.Rows.Count - 1)
                    return;
            }
            this.dtgMultiUnitList.Rows[index].Selected = true;
            ((clsCtl_Multiunit_drug)objController).m_mthTxtLoadData();
        }
        #endregion

        private void txtUnitName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPackage.Focus();
            }
        }

        private void txtPackage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                cmdSave.Focus();
            }
        }

        private void dtgMultiUnitList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int iRow = 0; iRow < this.dtgMultiUnitList.Rows.Count; iRow++)
            {
                this.dtgMultiUnitList.Rows[iRow].DefaultCellStyle.ForeColor = Color.Black;
                if (Convert.ToString(dtgMultiUnitList[4, iRow].Value) == "ͣ��")
                {
                    dtgMultiUnitList.Rows[iRow].DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    dtgMultiUnitList.Rows[iRow].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        internal void dtgMultiUnitList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView drvCurrent = null;
            for (int iRow = 0; iRow < dtgMultiUnitList.Rows.Count; iRow++)
            {
                drvCurrent = dtgMultiUnitList.Rows[iRow].DataBoundItem as DataRowView;
                if (drvCurrent != null && drvCurrent["status_int"].ToString() == "ͣ��")
                {
                    dtgMultiUnitList.Rows[iRow].DefaultCellStyle.SelectionForeColor = Color.Red;
                }
                else
                {
                    dtgMultiUnitList.Rows[iRow].DefaultCellStyle.SelectionForeColor = Color.White;
                }
            }
        }
    }
}