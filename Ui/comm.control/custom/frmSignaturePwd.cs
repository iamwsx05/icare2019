using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Entity;
using weCare.Core.Utils;
using weCare.Core.Entity;

namespace Common.Controls
{
    public partial class frmSignaturePwd : frmBasePopup
    {
        private EntityCodeOperator EmpVO { get; set; }

        public frmSignaturePwd(EntityCodeOperator _EmpVO)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.EmpVO = _EmpVO; 
            }
        }

        public frmSignaturePwd(EntityCodeOperator _EmpVO, Point p)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.EmpVO = _EmpVO;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(p.X, p.Y);
            }
        }

        public frmSignaturePwd(EntityCodeOperator _EmpVO, frmBase _frmOwner)
        {
            if (!DesignMode && _frmOwner != null)
            {
                _frmOwner.StartRedraw();
            }
            InitializeComponent();
            if (!DesignMode)
            {
                this.EmpVO = _EmpVO;
            }
        }

        private void Confirm()
        {
            string oriPwd = this.EmpVO.pwd;
            if (Common.Entity.GlobalHospital.Current == Entity.EnumHospitalCode.东莞茶山)
            {
                oriPwd = (new clsSymmetricAlgorithm()).Decrypt(oriPwd, clsSymmetricAlgorithm.enmSymmetricAlgorithmType.DES);
            }
            if (oriPwd == this.txtPwd.Text)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                DialogBox.Msg("密码输入错误，请重新输入。", MessageBoxIcon.Exclamation);
                this.txtPwd.Focus();
            }
        }

        private void frmSignaturePwd_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            this.txtEmpNo.Text = this.EmpVO.operCode;
            this.txtEmpName.Text = this.EmpVO.operName;
            this.txtTechName.Text = this.EmpVO.TechnicalLevelName;
        }

        private void frmSignaturePwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Confirm();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
