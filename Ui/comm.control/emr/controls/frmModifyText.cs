using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.HopeBridge.common.utility;
using com.HopeBridge.ehr.proxy;
using com.HopeBridge.ehr.control.TemperatureControl;

namespace ThreeItems.TemperatureControl
{
    public partial class frmModifyText : com.HopeBridge.ehr.guibase.frmBaseNormal
    {
        private frmModifyText()
        {
            InitializeComponent();
        }

        private string strPreText { get; set; }
        private int intRegID { get; set; }
        private string strCol { get; set; }
        private int intPageNo { get; set; }
        private DrawingGrid objGrid { get; set; }

        public frmModifyText(DrawingGrid p_objGrid, string p_strTextValue, int p_intRegID, string p_strCol, int p_intPageNo)
            : this()
        {
            objGrid = p_objGrid;
            strPreText = p_strTextValue;
            this.txtValue.Text = p_strTextValue;
            intRegID = p_intRegID;
            strCol = p_strCol;
            intPageNo = p_intPageNo;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strTemp = txtValue.Text.Trim();
            if (string.IsNullOrEmpty(strTemp))
            {
                clsDialog.Msg("文本值不能为空!", MessageBoxIcon.Warning, this);
                return;
            }

            if (strPreText != strTemp)
            {
                clsProxyThreeItems objProxy = new clsProxyThreeItems();
                int intRet = objProxy.Service.m_intAddThreeItemsSelfDefineCol(intRegID, strCol, strTemp, intPageNo + 1);
                if (intRet <= 0)
                {
                    clsDialog.Msg("数据更新错误!", MessageBoxIcon.Warning, this);
                    return;
                }
                if (strCol == "Other1")
                {
                    objGrid.m_mthTextChanged(1, strTemp);
                }
                else if (strCol == "Other2")
                {
                    objGrid.m_mthTextChanged(2, strTemp);
                }

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

   

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(sender, e);
            }
        }
    }
}
