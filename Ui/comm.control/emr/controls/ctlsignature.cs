using Common.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 签名控件
    /// </summary>
    public partial class ctlSignature : UserControl, ISignatureControl, IRuntimeDesignControl, IFormCtrl
    {
        #region 事件
        public event AfterValueChangedEventArgs AfterValueChanged;
        public delegate void AfterValueChangedEventArgs(object sender, EventArgs e);
        protected virtual void OnAfterValueChanged()
        {
            if (AfterValueChanged != null)
            {
                AfterValueChanged(this, new EventArgs());

            }
        }
        #endregion

        #region 表格标志
        /// <summary>
        /// 表格标志
        /// </summary>
        private bool _blnTableFalg = false;
        /// <summary>
        /// 表格标志
        /// </summary>
        public bool TableFlag
        {
            get { return _blnTableFalg; }
            set { _blnTableFalg = value; }
        }
        #endregion

        #region 原始高度、宽度
        /// <summary>
        /// 高度
        /// </summary>
        private int m_intOrgHeight = 0;
        /// <summary>
        /// 宽度
        /// </summary>
        private int m_intOrgWidth = 0;
        #endregion

        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public ctlSignature()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.Transparent;
            this.LineStyle = CtlLineStyle.Dash;
            this.BorderStyle = BorderStyle.None;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.m_intOrgHeight = this.Height; //this.lblDoctName.Height;
            this.m_intOrgWidth = this.Width; //this.lblDoctName.Width;
        }
        #endregion

        #region IEfCtrl

        /// <summary>
        /// 项目代码(名称)
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public string ItemType { get; set; }

        /// <summary>
        /// 项目标题(描述)
        /// </summary>
        public string ItemCaption { get; set; }

        /// <summary>
        /// 父节点名
        /// </summary>
        public string ParentNode { get; set; }

        /// <summary>
        /// 计算类型
        /// </summary>
        [Browsable(false)]
        public string CalProperty { get; set; }

        /// <summary>
        /// 行缩进字符个数
        /// </summary>
        [Browsable(false)]
        public int RowShrinkDigit { get; set; }

        #endregion

        #region IRuntimeDesignControl 成员

        public object EditObject
        {
            get
            {
                return GetSignEmpName();//this.DBValue;
            }
            set
            {
                //throw new NotImplementedException();
            }
        }

        public Font TextFont
        {
            get
            {
                return this.lblCaption.Font;
            }
            set
            {
                this.lblCaption.Font = value;
            }
        }

        public bool RunTimeReadOnly
        {
            get
            {
                return true;
            }
            set
            {
                //throw new NotImplementedException();
            }
        }

        bool referencetype = true;
        public bool Referencetype
        {
            get
            {
                return referencetype;
            }
            set
            {
                referencetype = value;
            }
        }

        public string EssentialGroupNo { get; set; }

        bool essential = false;
        public bool Essential
        {
            get
            {
                return essential;
            }
            set
            {
                essential = value;
            }
        }

        private int _presentationMode = 0;
        Color prevBackColor;
        public int PresentationMode
        {
            get
            {
                return _presentationMode;
            }
            set
            {
                if (_presentationMode != value)
                {
                    _presentationMode = value;

                    if (value == 0)
                    {
                        this.BackColor = prevBackColor;
                    }
                    else if (value == 1 || value == 2 || value == 3)
                    {
                        this.prevBackColor = this.BackColor;

                        this.BackColor = Color.Transparent;
                    }
                }
            }
        }

        [Browsable(false)]
        public decimal ZIndex { get; set; }

        private bool visible4design = true;

        [Browsable(false)]
        public bool Visible4Design
        {
            get
            {
                return visible4design;
            }
            set
            {
                visible4design = value;
            }
        }

        bool _showunderline = true;
        /// <summary>
        /// 是否显示下划线
        /// </summary>
        public bool ShowUnderLine
        {
            get
            {
                return _showunderline;
            }
            set
            {
                _showunderline = value;
                this.Invalidate();
            }
        }

        ///// <summary>
        ///// 默认行数
        ///// </summary>
        //int defaultRows = 1;
        //[Category("IRuntimeDesignControl属性")]
        //[Description("默认行数")]
        //public int DefaultRows
        //{
        //    get
        //    {
        //        return defaultRows;
        //    }
        //    set
        //    {
        //        if (value <= 0)
        //        {
        //            defaultRows = 1;
        //        }
        //        else
        //        {
        //            defaultRows = value;
        //        }
        //    }
        //}

        int masktype = 0;
        [Category("IRuntimeDesignControl属性")]
        [Description("掩码类型")]
        public int MaskType
        {
            get { return masktype; }
            set { masktype = value; }
        }
        #endregion

        #region 值变化标志
        /// <summary>
        /// 值变化标志
        /// </summary>
        private bool _blnValueChangedFlag = false;
        /// <summary>
        /// 值变化标志
        /// </summary>
        public bool ValueChangedFlag
        {
            get { return _blnValueChangedFlag; }
            set { _blnValueChangedFlag = value; }
        }
        #endregion

        #region 自动高度
        /// <summary>
        /// 自动高度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDoctName_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region 属性

        /// <summary>
        /// 线类型: Dash 虚线 Solid 实线
        /// </summary>
        private CtlLineStyle _lineStyle;
        /// <summary>
        /// 线类型: Dash 虚线 Solid 实线
        /// </summary>
        [Category("杂项"), Description("线类型: Dash 虚线 Solid 实线")]
        public CtlLineStyle LineStyle
        {
            get
            {
                return _lineStyle;
            }
            set
            {
                _lineStyle = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 签名标题
        /// </summary>
        private string _strCaption = "签名:";
        /// <summary>
        /// 签名标题
        /// </summary>
        [Category("杂项"), Description("签名标题")]
        public string Caption
        {
            get
            {
                if (string.IsNullOrEmpty(_strCaption))
                    return string.Empty;
                else
                    return _strCaption.Trim();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _strCaption = string.Empty;
                else
                    _strCaption = value.Trim();
                this.lblCaption.Text = _strCaption;
            }
        }

        private int _intIsAutoSignature = 1;
        [Category("杂项"), Description("控件是否自动签当前登录人姓名 0 否 1 是 ")]
        public int IsAutoSignature
        {
            get { return _intIsAutoSignature; }
            set { _intIsAutoSignature = value; }
        }
        private int _intIsAllowSignNull = 0;
        [Category("杂项"), Description("控件是否允许不签名 0 否 1 是")]
        public int IsAllowSignNull
        {
            get { return _intIsAllowSignNull; }
            set { _intIsAllowSignNull = value; }
        }

        /// <summary>
        /// 病人住院登记流水ID
        /// </summary>
        public string RegisterID { get; set; }

        private bool blnMulti = false;
        /// <summary>
        /// 签名绑定字段. 为空时默认针对整份表单签名，否则针对指定列签名。多个字段时用逗号(,)隔开。
        /// </summary>
        //[Category("杂项"), Description("签名绑定字段. 为空时默认针对整份表单签名，否则针对指定列签名。多个字段时用逗号(,)隔开。")]
        //public string SignBindingCol
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 签名医师类型 0 共用 1 自身 2 上级医生
        /// </summary>
        //[Category("杂项"), Description("签名医师类型 0 共用 1 自身 2 上级医生.")]
        //public int SignDoctType { get; set; }

        /// <summary>
        /// 签名成员数组
        /// </summary>
        private List<EntitySignature> m_lstSignature = new List<EntitySignature>();

        /// <summary>
        /// 是否已签名
        /// </summary>
        public bool IsSignature
        {
            get
            {
                if (this.m_lstSignature.Count == 0)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 未保存的签名成员
        /// </summary>
        public List<EntitySignature> m_lstNoSaveSignature = new List<EntitySignature>();
        #endregion

        #region OVERRIDE
        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (/*this._showunderline ||*/ _presentationMode == 1)
            {
                Pen p = new Pen(this.ForeColor);
                if (this.LineStyle == CtlLineStyle.Dash)
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                    p.DashPattern = ConstValue.DashPattern;
                }
                else if (this.LineStyle == CtlLineStyle.Solid)
                {

                }
                e.Graphics.DrawLine(p, new Point(this.lblCaption.Width + 2, this.Height - 2), new Point(this.Width - 2, this.Height - 2));
            }
            else if (_presentationMode == 3)
            {
                Pen p = new Pen(this.ForeColor);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                e.Graphics.DrawLine(p, new Point(this.lblCaption.Width + 2, this.Height - 2), new Point(this.Width - 2, this.Height - 2));
            }
        }

        public override string Text
        {
            get
            {
                return this.lblCaption.Text + this.lblDoctName.Text;
            }
            set
            {
                if (this.TableFlag && !string.IsNullOrEmpty(value))
                {
                    this.lblDoctName.Text = value;
                    this.lblDoctName.ForeColor = Color.Black;   // Color.Gray;
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 清空
        /// </summary>
        public void ClearText()
        {
            this.RegisterID = string.Empty;
            this.m_lstSignature = null;
            this.m_lstSignature = new List<EntitySignature>();
            //this.lblCaption.Text = this.Caption;
            this.lblDoctName.Text = string.Empty;

            this.m_lstNoSaveSignature.Clear();
            this.Height = this.m_intOrgHeight;
            //this.Width = this.m_intOrgWidth;
            this.OnAfterValueChanged();
        }
        /// <summary>
        /// 清空非保存的签名人信息
        /// </summary>
        public void CleartNoSaveSignature()
        {
            this.m_lstNoSaveSignature.Clear();
        }
        /// <summary>
        /// 添加签名成员
        /// </summary>
        /// <param name="p_dcSignature"></param>
        public void AddSignEmp(EntitySignature p_dcSignature)
        {
            this.m_lstSignature.Add(p_dcSignature);
            this.ResetCaption();

            this.OnAfterValueChanged();
        }
        /// <summary>
        /// 添加签名成员
        /// </summary>
        /// <param name="p_lstSignature"></param>
        public void AddSignEmp(List<EntitySignature> p_lstSignature)
        {
            this.m_lstSignature.AddRange(p_lstSignature);
            this.ResetCaption();

            this.OnAfterValueChanged();
        }
        private void ResetCaption()
        {
            this.m_lstSignature.Sort();
            string strSignName = string.Empty;
            this.blnMulti = false;
            if (this.m_lstSignature.Count > 1)
            {
                this.blnMulti = true;
            }
            List<string> lstEmpID = new List<string>();
            foreach (EntitySignature objSign in this.m_lstSignature)
            {
                if (lstEmpID.IndexOf(objSign.empId) >= 0)
                {
                    continue;
                }
                else
                {
                    lstEmpID.Add(objSign.empId);
                    if (this.blnMulti)
                        strSignName += objSign.empName + "\n";
                    else
                        strSignName += objSign.empName + "、";
                }
            }
            //this.lblCaption.Text = this.Caption + strSignName.Substring(0, strSignName.Length - 1);
            if (strSignName.Length > 1)
            {
                strSignName = strSignName.Substring(0, strSignName.Length - 1);
                this.lblDoctName.Text = strSignName;
                this.lblDoctName.ForeColor = Color.Blue; //Color.Black;

                if (this.blnMulti)
                {
                    int nums = (this.m_lstSignature.Count > 2 ? 2 : this.m_lstSignature.Count);
                    if (lstEmpID.Count > 1)
                    {
                        this.lblDoctName.Font = new Font("宋体", 9f, FontStyle.Regular);
                        if (TableFlag == false)
                        {
                            this.Height = nums * this.m_intOrgHeight - 5; //- 15;
                        }
                    }
                }
            }
            else
            {
                if (this.TableFlag && this.lblDoctName.Text.Length > 0)
                {
                    this.lblDoctName.ForeColor = Color.Black; //Color.Gray;
                }
                else
                {
                    this.lblDoctName.Text = string.Empty;
                    this.Height = this.m_intOrgHeight;
                    //this.Width = this.m_intOrgWidth;
                }
            }
            this.Invalidate();
        }
        /// <summary>
        /// 获取签名成员
        /// </summary>
        /// <returns></returns>
        public List<EntitySignature> GetSignEmp()
        {
            for (int i = 0; i < this.m_lstSignature.Count; i++)
            {
                if (string.IsNullOrEmpty(this.m_lstSignature[i].signKeyId))
                {
                    this.m_lstSignature[i].signKeyId = GlobalLogin.objLogin.SignKeyID;
                }
                if (this.m_lstSignature[i].recordDate.Value.ToString("yyyy-MM-dd") == "0001-01-01")
                {
                    this.m_lstSignature[i].recordDate = this.m_lstSignature[i].signDate;
                }
            }
            return this.m_lstSignature;
        }
        /// <summary>
        /// 签名成员名 
        /// </summary>
        /// <returns></returns>
        public string GetSignEmpName()
        {
            List<EntitySignature> data = GetSignEmp();
            if (data != null && data.Count > 0 && !string.IsNullOrEmpty(this.lblDoctName.Text.Trim()))
            {
                return this.lblDoctName.Text.Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 重要说明:本方法仅供只提供显示,不再进行任何操作的界面使用,即纯预览情况下通过DBValue赋值,省掉读取签名表,这样在历史轨迹里可以看到不同时期的签名
        /// </summary>
        /// <param name="strDoctName"></param>
        public void SetSignName(string strDoctName)
        {
            this.lblDoctName.Text = strDoctName;
        }

        public void SetSignNameNoSave(string signName)
        {
            this.lblDoctName.Text = signName;
            if (!string.IsNullOrEmpty(signName))
            {
                this.m_lstNoSaveSignature.Add(new EntitySignature() { empId = "**", empName = signName });
            }
        }

        #endregion

        #region 手工签名
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="Register">病人住院登记流水ID</param>
        /// <returns></returns>
        public bool Sign(string regNo)
        {
            bool blnPass = false;
            if (string.IsNullOrEmpty(regNo))
            {
                DialogBox.Msg("签名之前请选择病人。", MessageBoxIcon.Information, uiHelper.frmCurr);
                return blnPass;
            }

            frmSignature frm = new frmSignature();
            frm.StartPosition = FormStartPosition.Manual;

            Point pt1 = new Point(this.lblDoctName.Location.X, this.lblDoctName.Location.Y);
            Point pt2 = this.lblDoctName.PointToScreen(pt1);
            int intScreenW = Screen.PrimaryScreen.Bounds.Width;
            int intScreeH = Screen.PrimaryScreen.Bounds.Height;

            int intX = pt2.X;
            if (pt2.X + frm.Width > intScreenW)
            {
                intX = pt2.X - (pt2.X + frm.Width - intScreenW) - 30;
            }
            else if (intX < 0)
            {
                intX = 30;
            }
            int intY = pt2.Y - 10;
            if (intY < 0)
            {
                intY = 30;
            }
            else
            {
                if (pt2.Y + frm.Height > intScreeH)
                {
                    intY = intScreeH / 2 - 50;
                }
            }
            frm.Location = new Point(intX, intY);
            if (frm.ShowDialog(this.ParentForm) == DialogResult.OK)
            {
                //int intEmpID = int.Parse(frm.drDoct["empid_int"].ToString());
                string strEmpNo = frm.EmpVO.operCode;
                string strEmpName = frm.EmpVO.operName;
                string strTechCode = frm.EmpVO.TechnicalLevelNo;
                string strTechName = frm.EmpVO.TechnicalLevelName;
                string strPassWord = frm.EmpVO.pwd;
                //string strTeacherID = frm.drDoct["teacherid_int"].ToString();

                // 暂时屏蔽
                /*
                foreach (clsDCSignature obj in this.m_lstSignature)
                {
                    if (int.Parse(strTechCode) > int.Parse(obj.strTechLevelCode))
                    {
                        clsDialog.Msg("低职称不能给高职称医师签名。", MessageBoxIcon.Stop);
                        return blnPass;
                    }
                } */

                // 参数67 病历签名时，是否必须由导师权限审签病历。
                // 0 可以实习生本人签名保存；1 必须由导师签名保存。
                //if (GlobalParm.dicSysParameter[67] == "1")
                //{
                //    if (strTeacherID != string.Empty && strTeacherID != intEmpID.ToString())
                //    {
                //        DialogBox.Msg("系统当前设定：\r\n\r\n实习进修人员必须由导师权限审签病历。", MessageBoxIcon.Information, uiHelper.frmCurr);
                //        return false;
                //    }
                //}

                // 进修、实习医师(护士)采用密码签名
                bool blnTraineeFlag = false;
                //if (clsGlobalEmpRoleInfo.dicEmpRole.ContainsKey(intEmpID))
                //{
                //    string strP29 = clsGlobalSysParameter.dicSysParameter[29];
                //    int intP29 = 0;
                //    int.TryParse(strP29, out intP29);
                //    if (clsGlobalEmpRoleInfo.dicEmpRole[intEmpID].IndexOf(intP29) > 0)
                //    {
                //        blnTraineeFlag = true;

                //        if (clsGlobalHospitalCode.Code == "0001")
                //        {
                //            strEmpName += "(实习)";
                //        }
                //    }
                //}

                EntitySignature dcSign = null;
                //签名方式 0 密码; 1 KEY; 2 指纹
                string strSignType = "0";    // GlobalParm.dicSysParameter[3];
                if (strSignType == "0" || blnTraineeFlag)
                {
                    dcSign = new EntitySignature();
                    dcSign.empId = strEmpNo;
                    dcSign.empName = strEmpName;
                    dcSign.signDate = DateTime.Now;
                    dcSign.recordDate = DateTime.Now;
                    dcSign.techLevelCode = strTechCode;
                    dcSign.techLevelName = strTechName;
                    dcSign.registerId = regNo;
                    dcSign.caseCode = GlobalCase.caseInfo.CaseCode;
                    dcSign.objectID = this.ItemName;
                    this.AddSignEmp(dcSign);
                    blnPass = true;

                    this.ValueChangedFlag = true;
                    this.m_lstNoSaveSignature.Add(dcSign);

                    //frmDoctSignByPwd frmPwd = new frmDoctSignByPwd(strEmpNo, strEmpName, strTechName, strPassWord);
                    //frmPwd.StartPosition = FormStartPosition.Manual;
                    //intY = intY + (frm.Height - frmPwd.Height) / 2;
                    //frmPwd.Location = new Point(frm.Location.X, intY);
                    //if (frmPwd.ShowDialog(this.ParentForm) == DialogResult.OK)
                    //{
                    //    dcSign = new clsDCSignature();
                    //    dcSign.intEmpID = int.Parse(frm.drDoct["empid_int"].ToString());
                    //    dcSign.strEmpName = strEmpName;
                    //    dcSign.dtmSignDate = clsMidderTime.s_dtmMidderTime();
                    //    dcSign.dtmRecordDate = dcSign.dtmSignDate.Value;
                    //    dcSign.strTechLevelCode = strTechCode;
                    //    dcSign.strTechLevelName = strTechName;
                    //    dcSign.intRegisterID = regNo;
                    //    dcSign.strCaseCode = clsGlobalCase.objCaseInfo.strCaseCode;
                    //    dcSign.strObjectID = this.DBColName;
                    //    this.AddSignEmp(dcSign);
                    //    blnPass = true;

                    //    this.ValueChangedFlag = true;

                    //    this.m_lstNoSaveSignature.Add(dcSign);
                    //}
                }
                //else if (strSignType == "1")
                //{
                //    string strDBKeyID = frm.drDoct["signdigital_vchr"].ToString();
                //    if (clsCA.s_blnSignVerify(strDBKeyID))
                //    {
                //        dcSign = new clsDCSignature();
                //        dcSign.intEmpID = int.Parse(frm.drDoct["empid_int"].ToString());
                //        dcSign.strEmpName = strEmpName;
                //        dcSign.dtmSignDate = clsMidderTime.s_dtmMidderTime();
                //        dcSign.dtmRecordDate = dcSign.dtmSignDate.Value;
                //        dcSign.strTechLevelCode = strTechCode;
                //        dcSign.strTechLevelName = strTechName;
                //        dcSign.intRegisterID = regNo;
                //        dcSign.strCaseCode = clsGlobalCase.objCaseInfo.strCaseCode;
                //        dcSign.strObjectID = this.DBColName;
                //        dcSign.strSignKeyID = strDBKeyID;
                //        this.m_mthAddSignEmp(dcSign);
                //        blnPass = true;

                //        this.ValueChangedFlag = true;

                //        this.m_lstNoSaveSignature.Add(dcSign);

                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
                else
                {
                    DialogBox.Msg("签名失败。\r\n\r\n系统目前只支持：密码、电子KEY 两种签名模式。", MessageBoxIcon.Information, uiHelper.frmCurr);
                    return false;
                }

                //if (dcSign != null && this.m_lstNoSaveSignature.Count == 1 && clsGlobalCase.objCaseInfo != null &&
                //    !string.IsNullOrEmpty(clsGlobalCase.objCaseInfo.strCaseName) && clsGlobalCase.objCaseInfo.strCaseName.Contains("术前准备"))
                //{
                //    Form frmParent = this.ParentForm;
                //    if (frmParent != null && frmParent.GetType().Name == "DefaultFormTemplate")
                //    {
                //        try
                //        {
                //            List<IDBColProperty> lstDBCol = ((frmBaseMdiCase)frmParent).lstDBCol;
                //            if (lstDBCol != null && lstDBCol.Count > 0)
                //            {
                //                string strPrefix = string.Empty;
                //                if (dcSign.strObjectID.ToLower().StartsWith("executor"))
                //                    strPrefix = dcSign.strObjectID.Substring(0, 8);
                //                else if (dcSign.strObjectID.ToLower().StartsWith("inspector"))
                //                    strPrefix = dcSign.strObjectID.Substring(0, 9);
                //                else
                //                    return blnPass;
                //                int intIndex = 0;
                //                int.TryParse(dcSign.strObjectID.Replace(strPrefix, ""), out intIndex);
                //                if (intIndex <= 3)
                //                    return blnPass;

                //                List<ctlSignature> lstTmpSign = new List<ctlSignature>();
                //                for (int i = intIndex - 1; i >= 1; i--)
                //                {
                //                    if (lstDBCol.Exists(t => t.DBColName == strPrefix + i.ToString()))
                //                    {
                //                        IDBColProperty data = lstDBCol.Single(t => t.DBColName == strPrefix + i.ToString());
                //                        if (data is ctlSignature)
                //                        {
                //                            ctlSignature sign = data as ctlSignature;
                //                            lstTmpSign.Add(sign);
                //                            if (i + 1 < intIndex && sign.m_lstNoSaveSignature.Count == 1 && sign.m_lstNoSaveSignature[0].intEmpID == dcSign.intEmpID)
                //                            {
                //                                if (DialogBox.Msg("是否对第 " + Convert.ToString(i + 1) + " 至 " + Convert.ToString(intIndex - 1) + " 条信息进行批量签名？", MessageBoxIcon.Question, uiHelper.frmCurr) == DialogResult.Yes)
                //                                {
                //                                    foreach (ctlSignature item in lstTmpSign)
                //                                    {
                //                                        if (item.m_lstNoSaveSignature.Count == 0)
                //                                        {
                //                                            clsDCSignature dcS = new clsDCSignature();
                //                                            dcS.intEmpID = dcSign.intEmpID;
                //                                            dcS.strEmpName = dcSign.strEmpName;
                //                                            dcS.dtmSignDate = dcSign.dtmSignDate;
                //                                            dcS.dtmRecordDate = dcSign.dtmRecordDate;
                //                                            dcS.strTechLevelCode = dcSign.strTechLevelCode;
                //                                            dcS.strTechLevelName = dcSign.strTechLevelName;
                //                                            dcS.intRegisterID = dcSign.intRegisterID;
                //                                            dcS.strCaseCode = dcSign.strCaseCode;
                //                                            dcS.strObjectID = item.DBColName;
                //                                            dcS.strSignKeyID = dcSign.strSignKeyID;
                //                                            item.AddSignEmp(dcS);
                //                                            item.m_lstNoSaveSignature.Add(dcS);
                //                                            item.ValueChangedFlag = true;
                //                                            dcS = null;
                //                                        }
                //                                    }
                //                                    return blnPass;
                //                                }
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //        catch { }
                //    }
                //}
            }
            return blnPass;
        }

        /// <summary>
        /// 签名
        /// </summary>
        public bool Sign()
        {
            if (!string.IsNullOrEmpty(this.RegisterID))
            {
                return this.Sign(this.RegisterID);
            }
            else
            {
                if (GlobalPatient.currPatient == null)
                    return false;
                else
                    return this.Sign(GlobalPatient.currPatient.RegisterID);
            }
        }

        #endregion

        #region 事件

        private void blbiSign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(RegisterID))
            {
                if (GlobalPatient.currPatient == null)
                {
                    DialogBox.Msg("请选择病人。");
                    return;
                }
                else
                    RegisterID = GlobalPatient.currPatient.RegisterID;
            }
            this.Sign(RegisterID);
        }

        private void blbiDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.TableFlag)
            {
                if (DialogBox.Msg("是否删除签名信息？\r\n\r\n" + this.lblDoctName.Text, MessageBoxIcon.Question, uiHelper.frmCurr) == DialogResult.Yes)
                {
                    foreach (EntitySignature objSign in this.m_lstNoSaveSignature)
                    {
                        this.m_lstSignature.Remove(objSign);
                    }
                    this.m_lstNoSaveSignature.Clear();
                    this.ResetCaption();
                    if (this.m_lstSignature.Count == 0)
                    {
                        this.lblDoctName.Text = string.Empty;
                        this.Height = this.m_intOrgHeight;
                        //this.Width = this.m_intOrgWidth;
                    }
                    this.ValueChangedFlag = true;
                }
            }
            else
            {
                if (this.m_lstNoSaveSignature.Count > 0)
                {
                    string strDoctInfo = string.Empty;
                    List<string> lstName = new List<string>();
                    foreach (EntitySignature objSign in this.m_lstNoSaveSignature)
                    {
                        if (lstName.IndexOf(objSign.empName) < 0)
                            lstName.Add(objSign.empName);
                        else
                            continue;
                        strDoctInfo += "医师: " + objSign.empName + "\r\n\r\n";
                    }
                    if (DialogBox.Msg("是否删除签名信息？\r\n\r\n" + strDoctInfo, MessageBoxIcon.Question, uiHelper.frmCurr) == DialogResult.Yes)
                    {
                        foreach (EntitySignature objSign in this.m_lstNoSaveSignature)
                        {
                            this.m_lstSignature.Remove(objSign);
                        }
                        this.m_lstNoSaveSignature.Clear();
                        this.ResetCaption();
                        if (this.m_lstSignature.Count == 0)
                        {
                            this.lblDoctName.Text = string.Empty;
                            this.Height = this.m_intOrgHeight;
                            //this.Width = this.m_intOrgWidth;
                        }
                        this.ValueChangedFlag = true;
                    }
                }
                else
                {
                    DialogBox.Msg("签名已提交，不能删除。", MessageBoxIcon.Information, uiHelper.frmCurr);
                }
            }
        }

        private void lblCaption_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popupMenu.ShowPopup(Control.MousePosition);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.lblCaption.Focus();
            }
        }

        private void lblDoctName_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popupMenu.ShowPopup(Control.MousePosition);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.lblCaption.Focus();
            }
        }

        private void lblCaption_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            blbiSign_ItemClick(null, null);
        }

        private void lblDoctName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            blbiSign_ItemClick(null, null);
        }

        private void ctlSignature_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popupMenu.ShowPopup(Control.MousePosition);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.lblCaption.Focus();
            }
        }

        private void ctlSignature_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            blbiSign_ItemClick(null, null);
        }

        private void lblCaption_Enter(object sender, EventArgs e)
        {
            //this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDoctName.BackColor = Color.FromArgb(192, 0, 192);
        }

        private void lblCaption_Leave(object sender, EventArgs e)
        {
            //this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblDoctName.BackColor = Color.Transparent;
        }

        #endregion

    }
}


