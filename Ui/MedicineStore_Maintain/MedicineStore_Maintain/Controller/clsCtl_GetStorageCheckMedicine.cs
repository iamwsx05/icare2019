﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// 获取盘点药品
    /// </summary>
    public class clsCtl_GetStorageCheckMedicine : com.digitalwave.GUI_Base.clsController_Base
    {
        #region 全局变量
        /// <summary>
        /// 窗体
        /// </summary>
        com.digitalwave.iCare.gui.MedicineStore_Maintain.frmGetStorageCheckMedicine m_objViewer;
        /// <summary>
        /// 模块控制类

        /// </summary>
        private clsDcl_GetStorageCheckMedicine m_objDomain = null;
        #endregion

        #region 构造函数

        /// <summary>
        /// 获取盘点药品
        /// </summary>
        public clsCtl_GetStorageCheckMedicine()
        {
            m_objDomain = new clsDcl_GetStorageCheckMedicine();
        } 
        #endregion

        #region 设置窗体对象
        /// <summary>
        /// 设置窗体对象
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmGetStorageCheckMedicine)frmMDI_Child_Base_in;
        }
        #endregion

        #region 获取盘点药品
        /// <summary>
        /// 获取盘点药品
        /// </summary>
        /// <param name="p_dtbMedicine"></param>
        /// <returns></returns>
        internal long m_dtbGetMedicine(out DataTable p_dtbMedicine)
        {
            long lngRes = 0;
            p_dtbMedicine = null;

            Control ctlCurrent = null;//当前正在查询的控件

            if (m_objViewer.m_rdbCheckSortNum.Checked)
            {
                if (string.IsNullOrEmpty(m_objViewer.m_txtCheckSortNum1.Text) || string.IsNullOrEmpty(m_objViewer.m_txtCheckSortNum2.Text))
                {
                    MessageBox.Show("请先输入完整查询条件", "药品盘点", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_objViewer.m_txtCheckSortNum1.Focus();
                    return -1;
                }
                ctlCurrent = m_objViewer.m_txtCheckSortNum1;
                lngRes = m_objDomain.m_lngGetMedicineBySortNum(m_objViewer.m_txtCheckSortNum1.Text, m_objViewer.m_txtCheckSortNum2.Text, m_objViewer.m_strStorageID, out p_dtbMedicine);
            }
            else if (m_objViewer.m_rdbMedicineCode.Checked)
            {
                if (string.IsNullOrEmpty(m_objViewer.m_txtMedicineCode1.Text) || string.IsNullOrEmpty(m_objViewer.m_txtMedicineCode2.Text))
                {
                    MessageBox.Show("请先输入完整查询条件", "药品盘点", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_objViewer.m_txtMedicineCode1.Focus();
                    return -1;
                }
                ctlCurrent = m_objViewer.m_txtMedicineCode1;
                lngRes = m_objDomain.m_lngGetMedicineByMedicineCode(m_objViewer.m_txtMedicineCode1.Text, m_objViewer.m_txtMedicineCode2.Text, m_objViewer.m_strStorageID, out p_dtbMedicine);
            }
            else if (m_objViewer.m_rdbMedicinePreptype.Checked)
            {
                if (m_objViewer.m_cboMediciePreptype.SelectedIndex == -1 || m_objViewer.m_cboMediciePreptype.SelectedItem == null)
                {
                    MessageBox.Show("请先选择药品剂型", "药品盘点", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_objViewer.m_cboMediciePreptype.Focus();
                    return -1;
                }

                ctlCurrent = m_objViewer.m_cboMediciePreptype;
                 clsMEDICINEPREPTYPE_VO objTypeVO = m_objViewer.m_cboMediciePreptype.SelectedItem as  clsMEDICINEPREPTYPE_VO;
                lngRes = m_objDomain.m_lngGetMedicineByMedicinePreptype(objTypeVO.m_strMEDICINEPREPTYPE_CHR, m_objViewer.m_strStorageID, out p_dtbMedicine);
            }
            else if (m_objViewer.m_rdbRackNum.Checked)
            {
                if (string.IsNullOrEmpty(m_objViewer.m_txtRackNum1.Text) || string.IsNullOrEmpty(m_objViewer.m_txtRackNum2.Text))
                {
                    MessageBox.Show("请先输入完整查询条件", "药品盘点", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_objViewer.m_txtRackNum1.Focus();
                    return -1;
                }
                ctlCurrent = m_objViewer.m_txtRackNum1;
                lngRes = m_objDomain.m_lngGetMedicineByMedicineRackNO(m_objViewer.m_txtRackNum1.Text, m_objViewer.m_txtRackNum2.Text, m_objViewer.m_strStorageID, out p_dtbMedicine);
            }
            else if (m_objViewer.m_rdbAll.Checked)
            {
                lngRes = m_objDomain.m_lngGetAllMedicine(m_objViewer.m_strStorageID, out p_dtbMedicine);
            }
            else
            {
                MessageBox.Show("请先选择筛选条件", "药品盘点", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            if (p_dtbMedicine == null || p_dtbMedicine.Rows.Count == 0)
            {
                DialogResult drResult = MessageBox.Show("未找到符合条件的药品信息，是否更改筛选条件继续查找？", "药品盘点", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drResult == DialogResult.Yes)
                {
                    if (ctlCurrent != null)
                    {
                        ctlCurrent.Focus();
                    }
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            return 1;
        } 
        #endregion

        #region 获取查询条件
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="p_strCondition">查询条件</param>
        /// <returns></returns>
        internal long m_lngGetSearchCondition(out string p_strCondition)
        {
            p_strCondition = string.Empty;
            if (m_objViewer.m_rdbCheckSortNum.Checked)
            {
                if (string.IsNullOrEmpty(m_objViewer.m_txtCheckSortNum1.Text) || string.IsNullOrEmpty(m_objViewer.m_txtCheckSortNum2.Text))
                {
                    MessageBox.Show("请先输入完整筛选条件", "药品盘点", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_objViewer.m_txtCheckSortNum1.Focus();
                    return -1;
                }
                else
                {
                    p_strCondition = "checkmedicineorder_chr >= '" + m_objViewer.m_txtCheckSortNum1.Text + "' and checkmedicineorder_chr <= '" + m_objViewer.m_txtCheckSortNum2.Text + "'";
                }
            }
            else if (m_objViewer.m_rdbMedicineCode.Checked)
            {
                if (string.IsNullOrEmpty(m_objViewer.m_txtMedicineCode1.Text) || string.IsNullOrEmpty(m_objViewer.m_txtMedicineCode2.Text))
                {
                    MessageBox.Show("请先输入完整筛选条件", "药品盘点", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_objViewer.m_txtMedicineCode1.Focus();
                    return -1;
                }
                else
                {
                    p_strCondition = "assistcode_chr >= '" + m_objViewer.m_txtMedicineCode1.Text + "' and assistcode_chr <= '" + m_objViewer.m_txtMedicineCode2.Text + "'";
                }
            }
            else if (m_objViewer.m_rdbMedicinePreptype.Checked)
            {
                if (m_objViewer.m_cboMediciePreptype.SelectedIndex == -1 || m_objViewer.m_cboMediciePreptype.SelectedItem == null)
                {
                    MessageBox.Show("请先选择药品剂型", "药品盘点", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_objViewer.m_cboMediciePreptype.Focus();
                    return -1;
                }
                else
                {
                     clsMEDICINEPREPTYPE_VO objTypeVO = m_objViewer.m_cboMediciePreptype.SelectedItem as  clsMEDICINEPREPTYPE_VO;
                    if (objTypeVO != null && !string.IsNullOrEmpty(objTypeVO.m_strMEDICINEPREPTYPE_CHR))
                    {
                        p_strCondition = "medicinepreptype_chr = " + objTypeVO.m_strMEDICINEPREPTYPE_CHR;
                    }
                }
            }
            else if (m_objViewer.m_rdbRackNum.Checked)
            {
                if (string.IsNullOrEmpty(m_objViewer.m_txtRackNum1.Text) || string.IsNullOrEmpty(m_objViewer.m_txtRackNum2.Text))
                {
                    MessageBox.Show("请先输入完整查询条件", "药品盘点", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_objViewer.m_txtRackNum1.Focus();
                    return -1;
                }
                p_strCondition = "storagerackcode_vchr >= '" + m_objViewer.m_txtRackNum1.Text + "' and storagerackcode_vchr <= '" + m_objViewer.m_txtRackNum2.Text + "'";
            }
            else if (m_objViewer.m_rdbAll.Checked)
            {
                p_strCondition = string.Empty;
            }
            else
            {
                MessageBox.Show("请先选择筛选条件", "药品盘点", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            return 1;
        } 
        #endregion


        #region 获取药品制剂类型
        /// <summary>
        /// 获取药品制剂类型
        /// </summary>
        internal void m_mthGetMedicinePreptype()
        {
             clsMEDICINEPREPTYPE_VO[] objMPVO = null;
            
            long lngRes = m_objDomain.m_lngGetMedicinePreptype(out objMPVO);
       

            if (objMPVO != null && objMPVO.Length > 0)
            {
                m_objViewer.m_cboMediciePreptype.Items.Clear();
                 clsMEDICINEPREPTYPE_VO objAll = new  clsMEDICINEPREPTYPE_VO();
                objAll.m_intFLAGA_INT = 0;
                objAll.m_strMEDICINEPREPTYPE_CHR = string.Empty;
                objAll.m_strMEDICINEPREPTYPENAME_VCHR = "全部";
                m_objViewer.m_cboMediciePreptype.Items.Add(objAll);
                m_objViewer.m_cboMediciePreptype.Items.AddRange(objMPVO);
            }
        }
        #endregion
        
    }
}
