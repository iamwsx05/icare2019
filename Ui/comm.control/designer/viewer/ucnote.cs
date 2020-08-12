using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controls.Emr;
using Common.Entity;
using Common.Utils;
using weCare.Core.Utils;
using weCare.Core.Entity;

namespace Common.Controls
{
    /// <summary>
    /// 书写文本
    /// </summary>
    public partial class ucNote : UserControl
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public ucNote()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 是否显示发送按钮
        /// </summary>
        bool _IsShowSend = true;
        /// <summary>
        /// 是否显示发送按钮
        /// </summary>
        public bool IsShowSend
        {
            get { return _IsShowSend; }
            set
            {
                this.btnReply.Visible = value;
                _IsShowSend = value;
            }
        }

        /// <summary>
        /// 是否显示原因标签
        /// </summary>
        bool _IsShowReasonLabel = false;
        /// <summary>
        /// 是否显示原因标签
        /// </summary>
        public bool IsShowReasonLabel
        {
            get { return _IsShowReasonLabel; }
            set
            {
                this.lblReason.Visible = value;
                _IsShowReasonLabel = value;
            }
        }

        /// <summary>
        /// 是否显示B.I.U
        /// </summary>
        bool _IsShowBIU = true;
        /// <summary>
        /// 是否显示B.I.U
        /// </summary>
        public bool IsShowBIU
        {
            get { return _IsShowBIU; }
            set
            {
                this.btnB.Visible = value;
                this.btnI.Visible = value;
                this.btnU.Visible = value;
                _IsShowBIU = value;
            }
        }

        #endregion

        #region 方法

        #region 字体其他属性

        /// <summary>
        /// 字体其他属性
        /// </summary>
        /// <param name="flag"></param>
        void SetSelectionFontProperty(int flag)
        {
            float emSize = Convert.ToSingle(fontBig.Text);
            switch (flag)
            {
                case 4:     // 字体
                    this.rtbNote.Font = new System.Drawing.Font(fontEdit.Text, emSize);
                    break;
                case 5:     // 颜色
                    this.rtbNote.ForeColor = colorEdit.Color;
                    break;
                default:
                    break;
            }

            if (!(rtbNote.SelectionFont == null))
            {
                if (rtbNote.SelectionLength == 0) return;
                Font currentFont = rtbNote.SelectionFont;
                FontStyle newFontStyle = rtbNote.SelectionFont.Style;
                using (RichTextBox rtx = new RichTextBox())
                {
                    rtx.Rtf = rtbNote.SelectedRtf;
                    rtx.Select(0, rtx.Text.Length);
                    switch (flag)
                    {
                        case 1:     // 粗体
                            newFontStyle = rtx.SelectionFont.Style ^ FontStyle.Bold;
                            rtx.SelectionFont = new Font(currentFont.FontFamily, emSize, newFontStyle);
                            break;
                        case 2:     // 斜体
                            newFontStyle = rtx.SelectionFont.Style ^ FontStyle.Italic;
                            rtx.SelectionFont = new Font(currentFont.FontFamily, emSize, newFontStyle);
                            break;
                        case 3:     // 下划线
                            newFontStyle = rtx.SelectionFont.Style ^ FontStyle.Underline;
                            rtx.SelectionFont = new Font(currentFont.FontFamily, emSize, newFontStyle);
                            break;
                        //case 4:     // 字体
                        //    rtx.SelectionFont = new System.Drawing.Font(fontEdit.Text, emSize);
                        //    break;
                        //case 5:     // 颜色
                        //    rtx.SelectionColor = colorEdit.Color;
                        //    break;
                        default:
                            break;
                    }
                    rtbNote.SelectedRtf = rtx.SelectedRtf;
                }
            }
        }
        #endregion

        #region GetRtf
        /// <summary>
        /// GetRtf
        /// </summary>
        /// <returns></returns>
        public string GetRtf()
        {
            return this.rtbNote.Rtf;
        }
        #endregion

        #region GetText
        /// <summary>
        /// GetText
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return this.rtbNote.Text.Trim();
        }
        #endregion

        #region Clear
        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            this.rtbNote.Clear();
        }
        #endregion

        #region SetRtf
        /// <summary>
        /// SetRtf
        /// </summary>
        /// <param name="rtf"></param>
        public void SetRtf(string rtf)
        {
            this.rtbNote.Rtf = rtf;
        }
        #endregion

        #region SetTxt
        /// <summary>
        /// SetTxt
        /// </summary>
        /// <param name="txt"></param>
        public void SetTxt(string txt)
        {
            this.rtbNote.Text = txt;
        }
        #endregion

        #endregion

        #region 事件

        private void ucNote_Load(object sender, EventArgs e)
        {
            colorEdit.Color = Color.Black;
            fontBig.Text = "12";
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            SetSelectionFontProperty(1);
        }

        private void btnI_Click(object sender, EventArgs e)
        {
            SetSelectionFontProperty(2);
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            SetSelectionFontProperty(3);
        }

        private void fontEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectionFontProperty(4);
        }

        private void fontBig_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectionFontProperty(4);
        }

        private void colorEdit_EditValueChanged(object sender, EventArgs e)
        {
            SetSelectionFontProperty(5);
        }

        #endregion

    }
}
