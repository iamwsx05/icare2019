using System;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;
using Sybase.DataWindow;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 入院通知书
    /// </summary>
    public partial class frmInNotice : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmInNotice(clsPatient_VO _patVo)
        {
            InitializeComponent();
            patVo = _patVo;
        }
        #endregion

        #region 属性.变量

        clsPatient_VO patVo { get; set; }

        DataTable dtPayMode = null;
        DataWindowChild dwcPayMode = null;

        DataTable dtIcd10 = null;
        DataWindowChild dwcIcd10 = null;

        DataTable dtInDept = null;
        DataWindowChild dwcInDept = null;

        int rowNo { get; set; }

        /// <summary>
        /// 住院登记ID
        /// </summary>
        string registerId { get; set; }

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            try
            {
                clsPublic.PlayAvi("正在加载字典...");
                clsPatient_VO patVo1 = null;
                clsT_Opr_Bih_Register_VO bihVo = null;
                clsDcl_DoctorWorkstation svc = new clsDcl_DoctorWorkstation();
                {
                    dtPayMode = svc.GetPatientPayType();
                    dtIcd10 = svc.GetIcd10();
                    dtInDept = svc.GetDeptDesc();
                    this.registerId = svc.GetIpRegisterId(this.patVo.strPatientID);
                    if (!string.IsNullOrEmpty(this.registerId))
                    {
                        svc.GetRegister(this.registerId, out patVo1, out bihVo);
                    }
                }
                this.dwNotice.LibraryList = Application.StartupPath + "\\pbwindow.pbl";
                this.dwNotice.DataWindowObject = "d_bih_notice";
                
                rowNo = this.dwNotice.InsertRow(0);
                this.dwNotice.Modify("p_wxgzh.filename = '" + Application.StartupPath + "\\csyygzh.bmp" + "'");

                dwcPayMode = this.dwNotice.GetChild("fpaymode");
                if (dtPayMode != null && dtPayMode.Rows.Count > 0) dwcPayMode.Retrieve(dtPayMode);

                dwcIcd10 = this.dwNotice.GetChild("ficd10");
                if (dtIcd10 != null && dtIcd10.Rows.Count > 0) dwcIcd10.Retrieve(dtIcd10);

                dwcInDept = this.dwNotice.GetChild("findept");
                if (dtInDept != null && dtInDept.Rows.Count > 0) dwcInDept.Retrieve(dtInDept);

                if (patVo != null && bihVo != null)
                {
                    this.dwNotice.SetItemString(rowNo, "fpatname", this.patVo.strName);
                    this.dwNotice.SetItemString(rowNo, "fsex", this.patVo.strSex);
                    this.dwNotice.SetItemString(rowNo, "fage", new clsBrithdayToAge().m_strGetAge(this.patVo.strBirthDate));
                    this.dwNotice.SetItemString(rowNo, "fcardno", this.patVo.strPatientCardID);
                    this.dwNotice.SetItemString(rowNo, "fdept", this.LoginInfo.m_strdepartmentName);
                    this.dwNotice.SetItemString(rowNo, "fidcardno", patVo1.strID_Card);
                    this.dwNotice.SetItemString(rowNo, "ftelno", patVo1.strHomePhone);
                    this.dwNotice.SetItemString(rowNo, "faddr", patVo1.strHomeAddress);
                    this.dwNotice.SetItemString(rowNo, "fdoct", this.LoginInfo.m_strEmpName);
                    this.dwNotice.SetItemString(rowNo, "findate", bihVo.m_strINPATIENT_DAT);
                    this.dwNotice.SetItemString(rowNo, "fpaymode", bihVo.m_strPAYTYPEID_CHR);

                    this.dwNotice.SetItemString(rowNo, "finstatus", bihVo.m_intSTATE_INT.ToString());
                    this.dwNotice.SetItemString(rowNo, "ficd10", bihVo.m_strICD10DIAGID_VCHR);
                    this.dwNotice.SetItemString(rowNo, "findept", bihVo.m_strAREAID_CHR);
                    this.dwNotice.SetItemString(rowNo, "finmode", bihVo.inMode.ToString());
                    this.dwNotice.SetItemString(rowNo, "fmoney", bihVo.m_strCLINICSAYPREPAY);

                    this.dwNotice.Modify("t_ipno.text = '" + bihVo.m_strINPATIENTID_CHR + "'");
                    this.dwNotice.Modify("t_number.text = '" + svc.GetTodayInNumber(bihVo.m_strINPATIENTID_CHR) + "'");
                    this.tsbiNew.Enabled = false;
                }
                else
                {
                    this.dwNotice.SetItemString(rowNo, "fpatname", this.patVo.strName);
                    this.dwNotice.SetItemString(rowNo, "fsex", this.patVo.strSex);
                    this.dwNotice.SetItemString(rowNo, "fage", new clsBrithdayToAge().m_strGetAge(this.patVo.strBirthDate));
                    this.dwNotice.SetItemString(rowNo, "fcardno", this.patVo.strPatientCardID);
                    this.dwNotice.SetItemString(rowNo, "fdept", this.LoginInfo.m_strdepartmentName);
                    this.dwNotice.SetItemString(rowNo, "fidcardno", this.patVo.strID_Card);
                    this.dwNotice.SetItemString(rowNo, "ftelno", this.patVo.strHomePhone);
                    this.dwNotice.SetItemString(rowNo, "faddr", this.patVo.strHomeAddress);
                    this.dwNotice.SetItemString(rowNo, "fdoct", this.LoginInfo.m_strEmpName);
                    this.dwNotice.SetItemString(rowNo, "findate", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    this.dwNotice.SetItemString(rowNo, "fpaymode", "0001");
                }
            }
            finally
            {
                clsPublic.CloseAvi();
            }
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        void New()
        {
            this.dwNotice.SetItemString(rowNo, "fpatname", this.patVo.strName);
            this.dwNotice.SetItemString(rowNo, "fsex", this.patVo.strSex);
            this.dwNotice.SetItemString(rowNo, "fage", new clsBrithdayToAge().m_strGetAge(this.patVo.strBirthDate));
            this.dwNotice.SetItemString(rowNo, "fcardno", this.patVo.strPatientCardID);
            this.dwNotice.SetItemString(rowNo, "fdept", this.LoginInfo.m_strdepartmentName);
            this.dwNotice.SetItemString(rowNo, "fidcardno", string.Empty);
            this.dwNotice.SetItemString(rowNo, "ftelno", string.Empty);
            this.dwNotice.SetItemString(rowNo, "faddr", string.Empty);
            this.dwNotice.SetItemString(rowNo, "fdoct", this.LoginInfo.m_strEmpName);
            this.dwNotice.SetItemString(rowNo, "findate", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            this.dwNotice.SetItemString(rowNo, "fpaymode", string.Empty);
            this.dwNotice.SetItemString(rowNo, "finstatus", string.Empty);
            this.dwNotice.SetItemString(rowNo, "ficd10", string.Empty);
            this.dwNotice.SetItemString(rowNo, "findept", string.Empty);
            this.dwNotice.SetItemString(rowNo, "finmode", string.Empty);
            this.dwNotice.SetItemString(rowNo, "fmoney", string.Empty);
            this.dwNotice.Modify("t_ipno.text = ''");
            this.dwNotice.Modify("t_number.text = ''");
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        void Save()
        {
            string error = string.Empty;
            this.dwNotice.AcceptText();
            int row = this.dwNotice.CurrentRow;

            this.patVo.strID_Card = this.dwNotice.GetItemString(row, "fidcardno");
            this.patVo.strHomePhone = this.dwNotice.GetItemString(row, "ftelno");
            this.patVo.strHomeAddress = this.dwNotice.GetItemString(row, "faddr");

            if (string.IsNullOrEmpty(this.patVo.strID_Card) || this.patVo.strID_Card.Trim() == string.Empty)
            {
                MessageBox.Show("身份证号码不能为空，请录入。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("fidcardno");
                return;
            }
            if (string.IsNullOrEmpty(this.patVo.strHomePhone) || this.patVo.strHomePhone.Trim() == string.Empty)
            {
                MessageBox.Show("联系电话不能为空，请录入。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("ftelno");
                return;
            }
            if (string.IsNullOrEmpty(this.patVo.strHomeAddress) || this.patVo.strHomeAddress.Trim() == string.Empty)
            {
                MessageBox.Show("现住址不能为空，请录入。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("faddr");
                return;
            }
            if (this.dwNotice.IsItemNull(row, "fpaymode"))
            {
                MessageBox.Show("付款方式不能为空，请选择。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("fpaymode");
                return;
            }
            if (this.dwNotice.IsItemNull(row, "finstatus"))
            {
                MessageBox.Show("入院时情况不能为空，请选择。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("finstatus");
                return;
            }
            if (this.dwNotice.IsItemNull(row, "ficd10"))
            {
                MessageBox.Show("初步诊断不能为空，请选择。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("ficd10");
                return;
            }
            if (this.dwNotice.IsItemNull(row, "findept"))
            {
                MessageBox.Show("入院科室(病区)不能为空，请选择。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("findept");
                return;
            }
            if (this.dwNotice.IsItemNull(row, "finmode"))
            {
                MessageBox.Show("入院方式不能为空，请选择。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("finmode");
                return;
            }
            if (this.dwNotice.IsItemNull(row, "fmoney"))
            {
                MessageBox.Show("建议缴纳押金不能为空，请录入。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("fmoney");
                return;
            }

            clsT_Opr_Bih_Register_VO bihVo = new clsT_Opr_Bih_Register_VO();
            bihVo.m_strREGISTERID_CHR = this.registerId;
            bihVo.m_strAREAID_CHR = this.dwNotice.GetItemString(row, "findept");
            bihVo.m_intSTATE_INT = Convert.ToInt32(this.dwNotice.GetItemString(row, "finstatus"));
            bihVo.m_strOPERATORID_CHR = this.LoginInfo.m_strEmpID;
            bihVo.m_strICD10DIAGID_VCHR = this.dwNotice.GetItemString(row, "ficd10");
            bihVo.m_strICD10DIAGTEXT_VCHR = this.dwNotice.Describe("Evaluate('lookupdisplay(ficd10)',1)");
            bihVo.m_strCLINICSAYPREPAY = this.dwNotice.GetItemString(row, "fmoney");
            bihVo.m_strPAYTYPEID_CHR = this.dwNotice.GetItemString(row, "fpaymode");
            bihVo.m_strCaseDoctorDept = this.LoginInfo.m_strDepartmentID;
            bihVo.inMode = Convert.ToInt32(this.dwNotice.GetItemString(row, "finmode"));

            if (string.IsNullOrEmpty(bihVo.m_strPAYTYPEID_CHR) || bihVo.m_strPAYTYPEID_CHR.Trim() == string.Empty)
            {
                MessageBox.Show("付款方式不能为空，请选择。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("fpaymode");
                return;
            }
            if (bihVo.m_intSTATE_INT <= 0)
            {
                MessageBox.Show("入院时情况不能为空，请选择。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("finstatus");
                return;
            }
            if (string.IsNullOrEmpty(bihVo.m_strICD10DIAGID_VCHR) || bihVo.m_strICD10DIAGID_VCHR.Trim() == string.Empty)
            {
                MessageBox.Show("初步诊断不能为空，请选择。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("ficd10");
                return;
            }
            if (string.IsNullOrEmpty(bihVo.m_strAREAID_CHR) || bihVo.m_strAREAID_CHR.Trim() == string.Empty)
            {
                MessageBox.Show("入院科室(病区)不能为空，请选择。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("findept");
                return;
            }
            if (bihVo.inMode == 0)
            {
                MessageBox.Show("入院方式不能为空，请选择。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("finmode");
                return;
            }
            if (Convert.ToDecimal(bihVo.m_strCLINICSAYPREPAY) <= 0)
            {
                MessageBox.Show("建议缴纳押金不能为空，请录入。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dwNotice.SetColumn("fmoney");
                return;
            }

            string regId = string.Empty;
            string ipNo = string.Empty;
            clsDcl_DoctorWorkstation proxy = new clsDcl_DoctorWorkstation();
            int ret = proxy.InRegister(this.patVo, bihVo, out regId, out ipNo, out error);
            if (ret > 0)
            {
                MessageBox.Show("登记入院资料成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(this.registerId))
                {
                    this.registerId = regId;
                    this.dwNotice.Modify("t_ipno.text = '" + ipNo + "'");
                    this.dwNotice.Modify("t_number.text = '" + proxy.GetTodayInNumber(ipNo) + "'");
                }
            }
            else
            {
                MessageBox.Show(error, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        void Delete()
        {
            if (string.IsNullOrEmpty(this.registerId))
            {
                this.New();
            }
            else
            {
                if (MessageBox.Show("是否确定删除该患者的入院登记资料？？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string error = string.Empty;
                    clsDcl_DoctorWorkstation proxy = new clsDcl_DoctorWorkstation();
                    int ret = proxy.CancelRegister(this.registerId, this.LoginInfo.m_strEmpID, out error);
                    if (ret > 0)
                    {
                        MessageBox.Show("删除该患者的入院登记资料成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.registerId = string.Empty;
                        this.New();
                        this.tsbiNew.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(error, "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #endregion

        #region 事件

        private void frmInNotice_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void dataWindowControl_DataWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dataWindowControl_EditChanged(object sender, EditChangedEventArgs e)
        {
            string val = e.Data.ToUpper();
            if (e.ColumnName == "fpaymode")
            {
                dwcPayMode.SetFilter(string.Format("paytypeid like '{0}%'", val));
                dwcPayMode.Filter();
            }
            else if (e.ColumnName == "ficd10")
            {
                dwcIcd10.SetFilter(string.Format("(icd10code like '{0}%') or (icd10name like '{0}%') or (icd10pycode like '{0}%')", val, val, val));
                dwcIcd10.Filter();
            }
            else if (e.ColumnName == "findept")
            {
                dwcInDept.SetFilter(string.Format("(deptcode like '{0}%') or (deptname like '{0}%') or (deptpycode like '{0}%')", val, val, val));
                dwcInDept.Filter();
            }
        }

        private void tsbiNew_Click(object sender, EventArgs e)
        {
            this.New();
        }

        private void tsbiSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void tsbiDel_Click(object sender, EventArgs e)
        {
            this.Delete();
        }

        private void tsbiPrint_Click(object sender, EventArgs e)
        {
            this.dwNotice.Print();
        }

        private void tsbiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
