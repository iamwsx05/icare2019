using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls.Emr
{
    /// <summary>
    /// ctlBasePatientInfoControl
    /// </summary>
    public class ctlBasePatientInfoControl : Label, IPatientControl, IFormCtrl
    {
        /// <summary>
        /// ctlBasePatientInfoControl
        /// </summary>
        public ctlBasePatientInfoControl()
        {
            this.Font = new Font("宋体", 9.5f);
            this.BackColor = Color.Transparent;
            this.AutoSize = false;
            this.Height = 22;
        }

        /// <summary>
        /// 是否文字大小自动伸缩
        /// </summary>
        public override bool AutoSize
        {
            get
            {
                return false; //return base.AutoSize;
            }
            set
            {
                //base.AutoSize = value;
            }
        }

        bool referencetype = false;
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

        //int defaultRows = 1;
        //public int DefaultRows
        //{
        //    get
        //    {
        //        return defaultRows;
        //    }
        //    set
        //    {
        //        defaultRows = value;
        //    }
        //}

        int masktype = 0;
        public int MaskType
        {
            get { return masktype; }
            set { masktype = value; }
        }

        int intCalcAgeType = 0;
        public int CalcAgeType
        {
            get { return intCalcAgeType; }
            set { intCalcAgeType = value; }
        }

        ContentAlignment _TextAlign = ContentAlignment.MiddleLeft;

        /// <summary>
        /// 文字对齐方式
        /// </summary>
        public override ContentAlignment TextAlign
        {
            get
            {
                return _TextAlign;//base.TextAlign;
            }
            set
            {
                _TextAlign = value;
                base.TextAlign = value;
            }
        }

        #region IRuntimeDesignControl 成员
        [Browsable(false)]
        public object EditObject
        {
            get
            {
                return base.Text;
            }
            set
            {
                //throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public Font TextFont
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        //bool bCanChangeText = true;
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (!DesignMode)
                {
                    base.Text = value;
                }
            }
        }

        [Browsable(false)]
        public bool RunTimeReadOnly//始终为只读
        {
            get
            {
                return true;
            }
            set
            {
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
                        this.BorderStyle = BorderStyle.Fixed3D;
                    }
                    else if (value == 2)
                    {
                        this.BorderStyle = BorderStyle.None;
                    }
                    else
                    {
                        this.prevBackColor = this.BackColor;
                        this.BorderStyle = BorderStyle.None;
                        this.BackColor = Color.Transparent;
                    }
                    this.Invalidate();
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

        #endregion

        #region IEfCtrl

        /// <summary>
        /// 项目代码(名称)
        /// </summary>
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public string ItemType { get; set; }

        /// <summary>
        /// 项目标题(描述)
        /// </summary>
        public virtual string ItemCaption { get; set; }

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

        bool bShowUnderLine = true;

        /// <summary>
        /// 是否显示下划线
        /// </summary>
        public bool ShowUnderLine
        {
            get
            {
                return bShowUnderLine;
            }
            set
            {
                bShowUnderLine = value;
                this.Invalidate();
            }
        }


        bool _bShowCaption = false;// true;
        /// <summary>
        /// 是否显示标题
        /// </summary>
        public bool ShowCaption
        {
            get
            {
                return _bShowCaption;
            }
            set
            {
                _bShowCaption = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 页绑定
        /// </summary>
        private bool _blnBandingPage = false;
        /// <summary>
        /// 页绑定
        /// </summary>
        public bool BandingPage
        {
            get { return _blnBandingPage; }
            set { _blnBandingPage = value; }
        }

        bool _ReadPatientInfoFromGolbolEnv = true;

        /// <summary>
        /// 从全局对象获取病人宏元素信息
        /// </summary>
        public bool ReadPatientInfoFromGolbolEnv
        {
            get { return _ReadPatientInfoFromGolbolEnv; }
            set { _ReadPatientInfoFromGolbolEnv = value; }
        }

        /// <summary>
        /// 获取数据的文本值
        /// </summary>
        /// <returns></returns>
        public virtual string GetDataText()
        {
            string strValue = string.Empty;
            if (this.InfoType == EnumPatientInfoType.年龄)
            {
                if (GlobalPatient.currPatient != null)
                {
                    if (CalcAgeType == 1)
                    {
                        strValue = CalcAge.GetAge(GlobalPatient.currPatient.Birthday, GlobalPatient.currPatient.RegisterDate);
                    }
                    else
                    {
                        int intAge = CalcAge.GetAgeInt(GlobalPatient.currPatient.Birthday);
                        if (intAge < 1)
                        {
                            strValue = CalcAge.GetAge(GlobalPatient.currPatient.Birthday, GlobalPatient.currPatient.RegisterDate);
                        }
                        else
                        {
                            strValue = CalcAge.GetAge(GlobalPatient.currPatient.Birthday);
                        }
                    }
                }
            }
            else
            {
                strValue = PatientInfoHelper.GetTypePatientInfo(this.InfoType);
            }

            return strValue;
        }

        EnumPatientInfoType _InfoType = EnumPatientInfoType.姓名;

        /// <summary>
        /// 病人信息类型
        /// </summary>
        [Browsable(false)]
        public virtual EnumPatientInfoType InfoType
        {
            get
            {
                return _InfoType;
            }
            set
            {
                _InfoType = value;
                this.Invalidate();
            }
        }

        SizeF CaptionSize = new SizeF(0, 0);

        bool captionsetted = false;

        string _captiontext = string.Empty;
        /// <summary>
        /// 标题文字
        /// </summary>
        public string CaptionText
        {
            get
            {
                return this._captiontext;
            }
            set
            {
                _captiontext = value;
                captionsetted = true;
                //isFirstShow = true;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshData()
        {
            if (this.ReadPatientInfoFromGolbolEnv == false) return;

            //获取数据
            string strDisplayText = string.Empty;

            if (this.InfoType == EnumPatientInfoType.记录时间)
            {
                //if (GlobalHospital.HospitalCode == "0010")
                //{
                this.Text = this.CaptionText + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                //}
                //else
                //{
                //    this.Text = this.CaptionText + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //}
            }
            else
            {
                strDisplayText = GetDataText();
                //this._dbvalue = strDisplayText;

                if (_bShowCaption)//显示标题
                {
                    if (captionsetted)
                    {
                        //_captiontext = this._captiontext;
                    }
                    else
                    {
                        _captiontext = this.InfoType.ToString() + "：";
                    }
                    strDisplayText = _captiontext + strDisplayText;
                }
                this.Text = strDisplayText;
            }

            this.Invalidate();
        }

        private int m_intNums = 0;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.DesignMode)//设计时
            {
                string displayText = string.Format("[{0}]", InfoType.ToString());

                if (_bShowCaption)//显示标题
                {
                    if (captionsetted)
                    {
                        //_captiontext = this._captiontext;
                    }
                    else
                    {
                        _captiontext = string.Empty;
                    }

                    CaptionSize = e.Graphics.MeasureString(_captiontext, this.Font);
                    displayText = _captiontext + displayText;
                }
                else
                {
                    CaptionSize = new SizeF(0, 0);
                }

                SizeF textSize = e.Graphics.MeasureString(displayText, this.Font);
                PointF location = new PointF(0, ((float)this.Height - textSize.Height) / 2f);
                e.Graphics.DrawString(displayText, this.Font, Brushes.Blue, location);
            }
            else//运行时
            {
                if (m_intNums == 0 && this.ReadPatientInfoFromGolbolEnv)
                {
                    string val = this.GetDataText();
                    if (val != null) this.Text = val;
                    m_intNums++;
                }
                if (_bShowCaption)//显示标题
                {
                    if (captionsetted)
                    {
                        //_captiontext = this._captiontext;
                    }
                    else
                    {
                        _captiontext = this.InfoType.ToString() + "：";
                    }
                    CaptionSize = e.Graphics.MeasureString(_captiontext, this.Font);
                }
                else
                {
                    CaptionSize = new SizeF(0, 0);
                }
            }

            if (_presentationMode == 1 || _presentationMode == 3)//是否画下划线
            {
                Pen p = new Pen(Brushes.Black);
                if (_presentationMode == 3)
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                }
                else
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                    p.DashPattern = ConstValue.DashPattern;
                }
                Point pLineLocation1 = new Point();
                if (CaptionSize.Width > 0)
                {
                    pLineLocation1.X = (int)CaptionSize.Width - 8;
                }
                else
                {
                    pLineLocation1.X = 0;
                }
                pLineLocation1.Y = this.Height - 2;
                e.Graphics.DrawLine(p, pLineLocation1, new Point(this.Width - 2, this.Height - 2));
            }
        }

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

        #region OnMouseDoubleClick
        /// <summary>
        /// OnMouseDoubleClick
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            /*  出生日期, 
                病区, 科室, 病床号, 记录时间, 医院名称   */
            List<EnumPatientInfoType> lstInfoType = new List<EnumPatientInfoType>();
            lstInfoType.Add(EnumPatientInfoType.婚姻);
            lstInfoType.Add(EnumPatientInfoType.民族);
            lstInfoType.Add(EnumPatientInfoType.籍贯);
            lstInfoType.Add(EnumPatientInfoType.国籍);
            lstInfoType.Add(EnumPatientInfoType.家庭地址);
            lstInfoType.Add(EnumPatientInfoType.身份证号);
            lstInfoType.Add(EnumPatientInfoType.出生地);
            lstInfoType.Add(EnumPatientInfoType.职业);
            lstInfoType.Add(EnumPatientInfoType.工作单位);
            lstInfoType.Add(EnumPatientInfoType.联系电话);
            lstInfoType.Add(EnumPatientInfoType.联系人);
            lstInfoType.Add(EnumPatientInfoType.联系人电话);
            lstInfoType.Add(EnumPatientInfoType.与联系人关系);
            lstInfoType.Add(EnumPatientInfoType.身高);
            lstInfoType.Add(EnumPatientInfoType.体重);
            lstInfoType.Add(EnumPatientInfoType.血型);
            lstInfoType.Add(EnumPatientInfoType.费别);
            lstInfoType.Add(EnumPatientInfoType.住院天数);
            lstInfoType.Add(EnumPatientInfoType.户口地址);

            //lstInfoType.Add(EnumPatientInfoType.文化);
            //lstInfoType.Add(EnumPatientInfoType.邮编);
            //lstInfoType.Add(EnumPatientInfoType.监护人);
            //lstInfoType.Add(EnumPatientInfoType.监护人地址);
            //lstInfoType.Add(EnumPatientInfoType.监护人电话);
            //lstInfoType.Add(EnumPatientInfoType.与监护人关系);

            // 以下9个为指定角色的人才能修改
            lstInfoType.Add(EnumPatientInfoType.姓名);
            lstInfoType.Add(EnumPatientInfoType.性别);
            lstInfoType.Add(EnumPatientInfoType.年龄);
            lstInfoType.Add(EnumPatientInfoType.住院号);
            lstInfoType.Add(EnumPatientInfoType.住院次数);
            lstInfoType.Add(EnumPatientInfoType.病区);
            lstInfoType.Add(EnumPatientInfoType.科室);
            lstInfoType.Add(EnumPatientInfoType.病床号);
            lstInfoType.Add(EnumPatientInfoType.入院日期);
            lstInfoType.Add(EnumPatientInfoType.出生日期);
            lstInfoType.Add(EnumPatientInfoType.门诊住院号);

            if (lstInfoType.IndexOf(InfoType) >= 0)
            {
                string strCaption = CaptionText.Replace("：", "").Replace(":", "");
                frmModifyPatBasicInfo frm = new frmModifyPatBasicInfo(strCaption, this.Text, ShowCaption);
                if (frm.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    string info = frm.PatientInfo.Trim();
                    if (ShowCaption)
                        this.Text = CaptionText + info;
                    else
                        this.Text = info;
                    this.Refresh();
                }
            }
            base.OnMouseDoubleClick(e);
        }
        #endregion

    }
}
