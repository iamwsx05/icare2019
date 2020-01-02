using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.gui.MedicineStore;
using System.Data;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.IO;
using com.digitalwave.Utility;
using com.digitalwave.iCare.common;
using Sybase.DataWindow;
using System.Collections;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// ҩ�����ѯ���Ʋ�
    /// </summary>
    public class clsCtl_StorageShelf : com.digitalwave.GUI_Base.clsController_Base
    {
        /// <summary>
        /// ҩ�����ѯ���Ʋ㹹�췽��
        /// </summary>
        public clsCtl_StorageShelf()
        {
            m_objDomain = new clsDcl_StorageShelf();
        }

        #region ȫ�ֱ���
        /// <summary>
        /// ģ�������
        /// </summary>
        private clsDcl_StorageShelf m_objDomain = null;
        /// <summary>
        /// ����
        /// </summary>
        private com.digitalwave.iCare.gui.MedicineStore_Maintain.frmStorageShelf m_objViewer;
        /// <summary>
        /// ҩƷ��ѯ�ؼ�
        /// </summary>
        private ctlQueryMedicintLeastElement m_ctlQueryMedicint = null;
        /// <summary>
        /// ҩ�������Ϣ
        /// </summary>
        private clsValue_StorageBse_VO[] m_objStorageBseArr = null;
        /// <summary>
        /// ҩƷ��������
        /// </summary>
        private clsValue_MedicineType_VO[] m_objMedicineTypeArr = null;
        private clsValue_MedicineType_VO[] objMedicineTypeArr = null;
        /// <summary>
        /// ҩƷ��ϸ���ݱ�
        /// </summary>  
        internal DataTable dtbResult = null;
        /// <summary>
        /// ͳ����Ϣ��ʱ��
        /// </summary>
        private DataTable dtbTem = new DataTable();
        /// <summary>
        /// ȱҩ
        /// </summary>
        private DataTable m_dtbLack = new DataTable();
        /// <summary>
        /// ͣ��
        /// </summary>
        private DataTable m_dtbStop = new DataTable();
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmStorageShelf)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ʾҩƷ�ֵ���СԪ����Ϣ��ѯ����
        /// <summary>
        /// ��ʾҩƷ�ֵ���СԪ����Ϣ��ѯ����
        /// </summary>
        /// <param name="p_strSearchCon">��ѯ����</param>
        internal void m_mthShowQueryMedicineForm(string p_strSearchCon, DataTable m_dtMedicineInfo)
        {
            if (m_ctlQueryMedicint == null)
            {
                m_ctlQueryMedicint = new ctlQueryMedicintLeastElement(m_dtMedicineInfo);
                m_objViewer.Controls.Add(m_ctlQueryMedicint);

                int X = 0;
                int Y = 0;
                X = m_objViewer.m_txtMedicineCode.Location.X + m_objViewer.gradientPanel1.Location.X;
                Y = m_objViewer.m_txtMedicineCode.Location.Y + m_objViewer.m_txtMedicineCode.Size.Height + m_objViewer.gradientPanel1.Location.Y;

                m_ctlQueryMedicint.Location = new System.Drawing.Point(X, Y);

                m_ctlQueryMedicint.ReturnInfo += new ReturnMedicineInfo(frmQueryForm_ReturnInfo);
                m_ctlQueryMedicint.RefreshMedicine += new RefreshMedicineInfo(m_ctlQueryMedicint_RefreshMedicine);
            }

            m_ctlQueryMedicint.Visible = true;
            m_ctlQueryMedicint.BringToFront();
            m_ctlQueryMedicint.Focus();
            m_ctlQueryMedicint.m_mthSetSearchText(p_strSearchCon);
        }

        private void m_ctlQueryMedicint_RefreshMedicine()
        {
            m_objDomain.m_lngGetBaseMedicine(false,"",m_objViewer.m_strStorageID, out m_objViewer.m_dtMedicineInfo);            
            m_ctlQueryMedicint.m_dtbMedicineInfo = m_objViewer.m_dtMedicineInfo;
        }

        internal void frmQueryForm_ReturnInfo( clsMS_MedicintLeastElement_VO MS_VO)
        {
            if (MS_VO == null)
            {
                return;
            }
            m_objViewer.m_txtMedicineCode.Tag = MS_VO.m_strMedicineID;
            m_objViewer.m_txtMedicineCode.Text = MS_VO.m_strMedicineName;
        }
        #endregion

        #region ��ȡҩ�������Ϣ
        /// <summary>
        /// ��ȡҩ�������Ϣ
        /// </summary>
        internal void m_mthShowStorage()
        {
            m_objViewer.m_cboStorage.Item.Add(m_objViewer.m_strStorageName,m_objViewer.m_strStorageID);
            m_objViewer.m_cboStorage.SelectedIndex = 0;
        }
        #endregion

        #region ��ȡҩƷ����
        /// <summary>
        /// ��ȡҩƷ����
        /// </summary>
        internal void m_mthShowMedicineType()
        {
            long lngRes = 0;

            m_objViewer.m_cboMedicineType.Items.Clear();
            if ((m_objViewer.m_cboMedicineType.Items.Count >= 0) && (objMedicineTypeArr == null))
            {
                m_objViewer.m_cboMedicineType.Items.Clear();
                try
                {
                    lngRes = m_objDomain.m_lngGetResultByConditionMedicineType(out objMedicineTypeArr);

                    if (lngRes > 0)
                    {
                        m_objMedicineTypeArr = new clsValue_MedicineType_VO[objMedicineTypeArr.Length + 1];
                        m_objViewer.m_cboMedicineType.Items.Add("��������");
                        int m_index = 0;
                        for (int i1 = 0; i1 < objMedicineTypeArr.Length; i1++)
                        {
                            m_index = m_objViewer.m_cboMedicineType.Items.Add(objMedicineTypeArr[i1].m_strMedicineTypeName);
                            m_objMedicineTypeArr[m_index] = objMedicineTypeArr[i1];
                        }
                        m_objViewer.m_cboMedicineType.SelectedIndex = 0;
                    }
                    else
                    {
                        m_objViewer.m_cboMedicineType.Items.Clear();
                    }
                }
                catch (Exception objEx)
                {
                    MessageBox.Show(objEx.Message, "ҩ��������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            else if ((m_objViewer.m_cboMedicineType.Items.Count >= 0) && (objMedicineTypeArr != null))
            {
                m_objViewer.m_cboMedicineType.Items.Add("��������");
                int m_index = 0;
                for (int i1 = 0; i1 < objMedicineTypeArr.Length; i1++)
                {
                    m_index = m_objViewer.m_cboMedicineType.Items.Add(objMedicineTypeArr[i1].m_strMedicineTypeName);
                    m_objMedicineTypeArr[m_index] = objMedicineTypeArr[i1];
                }
                m_objViewer.m_cboMedicineType.SelectedIndex = 0;

            }
        }
        #endregion

        #region ��ȡҩƷ��ϸ����
        /// <summary>
        /// ��ȡҩƷ��ϸ����
        /// ʵ��ͳ�Ʋ�ѯ����ϸ��ѯ���ܡ�
        /// �ɰ�ҩƷ�������롢ƴ���롢����롢ҩƷ��ID��ҩƷ���ƽ���ģ����ѯ
        /// </summary>
        internal void m_mthQuery()
        {
            string m_strStorageID = string.Empty;
            string m_strMedicineID = string.Empty;
            string m_strAssistCode = string.Empty;
            string m_strMedicineTypeID = string.Empty;

            if (m_objViewer.m_strStorageID.Length == 0)
            {
                MessageBox.Show("����ѡ��ҩ��!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            long lngRes = 0;
            List<string> lstMedicineType = new List<string>();            

            m_objViewer.m_dgvDrugStorage.DataSource = null;

            if (dtbResult != null)
            {
                dtbResult.Clear();
                dtbResult.Dispose();
                dtbResult = null;
            }
            m_strStorageID = m_objViewer.m_strStorageID;
            
            if (m_objViewer.m_txtMedicineCode.Text.Trim().Length > 0)
            {
                if (m_objViewer.m_objMedicineBase.m_strMedicineID.Trim().Length > 0)
                    m_strMedicineID = m_objViewer.m_objMedicineBase.m_strMedicineID.Trim();
                else
                {
                    m_strAssistCode = m_objViewer.m_txtMedicineCode.Text + @"%";
                }
            }
            else
            {
                m_strAssistCode = "";
            }

            //ҩƷ����
            if ((m_objViewer.m_cboMedicineType.Text.Trim().Length > 0) && (m_objViewer.m_cboMedicineType.Text.Trim() != "��������"))
            {
                lstMedicineType.Add(objMedicineTypeArr[m_objViewer.m_cboMedicineType.SelectedIndex - 1].m_strMedicineTypeID);
            }
            else
                m_strMedicineTypeID = "";
            
            dtbResult = new DataTable();//���ݿⷵ�صĽ����

            lngRes = m_objDomain.m_mthGetStorageDetailData(m_strStorageID, m_strMedicineID, m_strAssistCode, m_strMedicineTypeID,
                out dtbResult,lstMedicineType);
            if ((lngRes > 0) && (dtbResult != null))
            {
                m_objViewer.m_dgvDrugStorage.DataSource = dtbResult;               
            }
            m_objViewer.m_dtbModify.Rows.Clear();
            m_objViewer.m_dtbModify = dtbResult.Clone();
            //m_mthFilterShow();
        }
        #endregion

        internal void m_mthFilterShow()
        {            
            if(dtbResult == null) return;
            DataTable m_dtbTemp = dtbResult.Copy();
            
            DataView dvResult = m_dtbTemp.DefaultView;
            if (m_objViewer.m_ckbStop.Checked || m_objViewer.m_ckbNoQuality.Checked)
            {
                if (m_objViewer.m_ckbStop.Checked && m_objViewer.m_ckbNoQuality.Checked == false)
                {
                    dvResult.RowFilter = "NOQTYFLAG_INT <> 1";
                }
                else if (m_objViewer.m_ckbStop.Checked == false && m_objViewer.m_ckbNoQuality.Checked)
                {
                    dvResult.RowFilter = "ifstop_int <> 1";
                }
            }
            else
            {
                dvResult.RowFilter = "ifstop_int = 0 and NOQTYFLAG_INT = 0";                
            }
            m_dtbTemp = dvResult.ToTable();
            m_objViewer.m_dgvDrugStorage.DataSource = m_dtbTemp; 
           
        }

        #region �����񵼳����ݵ�Excel
        /// <summary>
        /// �����񵼳����ݵ�Excel
        /// </summary>
        internal void m_mthExportToExcel()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "����Excel�ļ���";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            string str = "";
            try
            {
                //����б���
                for (int iOr = 0; iOr < m_objViewer.m_dgvDrugStorage.ColumnCount; iOr++)
                {
                    if (m_objViewer.m_dgvDrugStorage.Columns[iOr].Visible == false)
                        continue;
                    if (iOr > 0)
                    {
                        str += "\t";
                    }
                    str += m_objViewer.m_dgvDrugStorage.Columns[iOr].HeaderText.Replace("\n", "");
                }
                sw.WriteLine(str);
                //������ı�
                StringBuilder objStrBuilder = null;
                for (int iOr = 0; iOr < m_objViewer.m_dgvDrugStorage.Rows.Count; iOr++)
                {                    
                    objStrBuilder = new StringBuilder();
                    for (int jOr = 0; jOr < m_objViewer.m_dgvDrugStorage.Columns.Count; jOr++)
                    {
                        if (m_objViewer.m_dgvDrugStorage.Columns[jOr].Visible == false)
                            continue;
                        if (jOr > 0)
                        {
                            objStrBuilder.Append("\t");
                        }
                        if (jOr == 0 || jOr == 1)
                        {
                            objStrBuilder.Append(m_objViewer.m_dgvDrugStorage.Rows[iOr].Cells[jOr].FormattedValue.ToString());
                        }
                        else
                            objStrBuilder.Append(m_objViewer.m_dgvDrugStorage.Rows[iOr].Cells[jOr].Value.ToString());
                    }
                    sw.WriteLine(objStrBuilder);
                }
                MessageBox.Show("�����ɹ���", "��ϲ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sw.Close();
                myStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }
        #endregion

        #region ��ӡ
        /// <summary>
        /// ��ӡ
        /// </summary>
        internal void m_mthPrint()
        {
            //return;//������δ�� 

            //m_mthQuery();
            //DataWindowControl m_dwcDrugQuery = new DataWindowControl();
            //m_dwcDrugQuery.LibraryList = Application.StartupPath + "\\PB_OP.pbl";            
            //m_dwcDrugQuery.DataWindowObject = "d_op_drugquerydetail";

            ////datWindow.Modify("t_tile.text = '" + base.m_objComInfo.m_strGetHospitalTitle() + "(" + p_strStorageName + ")'");
            //m_dwcDrugQuery.SetRedrawOff();            
            //m_dwcDrugQuery.Retrieve(dtbResult);            
            //m_dwcDrugQuery.Refresh();
            //m_dwcDrugQuery.SetRedrawOn();
            //m_dwcDrugQuery.Modify("t_storename.text = 'ҩ�⣺" + m_objViewer.m_cboStorage.Text + "'");
            //m_dwcDrugQuery.Modify("t_drugtypename.text = 'ҩƷ���ͣ�" + m_objViewer.m_cboMedicineType.Text + "'");
            //clsPublic.PrintDialog(m_dwcDrugQuery);
        }
        #endregion

       

        #region �������
        internal long m_lngSaveStorageShelf(DataTable p_dtbModify)
        {
            long lngRes = 0;

            if (p_dtbModify.Rows.Count > 0)
            {
                try
                {
                    lngRes = m_objDomain.m_lngSaveStorageShelf(p_dtbModify);
                }
                catch (Exception objEx)
                {
                    MessageBox.Show(objEx.Message, "�������", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            return lngRes;
        }
        #endregion

        internal long m_lngGetBaseMedicine(string m_strStorageID, out DataTable m_dtbMedicinDict)
        {
            return m_objDomain.m_lngGetBaseMedicine(false, "", this.m_objViewer.m_strStorageID, out m_dtbMedicinDict);
        }

        internal void m_mthGetStorageShelfInfo(out DataTable p_dtbShelfInfo)
        {
            m_objDomain.m_mthGetStorageShelfInfo(m_objViewer.m_strStorageID, out p_dtbShelfInfo);
        }

        #region ����ҩ��ʵ�ʻ��ܰ󶨻���
        /// <summary>
        /// ����ҩ��ʵ�ʻ��ܰ󶨻���
        /// </summary>
        internal void m_mthBindStorageShelf()
        {
            try
            {
                if (m_objViewer.m_dtbShelf != null && m_objViewer.m_dtbShelf.Rows.Count > 0)
                {
                    m_objViewer.colRack.DataSource = m_objViewer.m_dtbShelf;
                    m_objViewer.colRack.ValueMember = "storagerackid_chr";
                    m_objViewer.colRack.DisplayMember = "STORAGERACKNAME_VCHR";
                }

            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message, "���ܼ��س���", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion
    }
}
