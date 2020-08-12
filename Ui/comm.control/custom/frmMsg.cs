using System;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 消息窗
    /// </summary>
    internal partial class frmMsg : System.Windows.Forms.Form
    {
        private int intTimes = 0;
        private int intDiffHeight = 0;

        /// <summary>
        /// 构造
        /// </summary>
        public frmMsg(int p_intType, string p_strInfo, bool p_blnShowCancel)
        {
            InitializeComponent();
            if (DesignMode) return;
            if (p_intType == 1)
            {
                this.btnOk.Visible = true;
                this.btnYes.Visible = false;
                this.btnNo.Visible = false;
                this.btnCancel.Visible = false;

                if (p_strInfo.Contains("成功") || p_strInfo.Contains("字段标题") || p_strInfo == "没有资料" || p_strInfo.Contains("未找到符合条件的记录") || p_strInfo.Contains("温馨提示") || p_strInfo.Contains("未进入临床路径") ||
                    p_strInfo.Contains("书写受限") || p_strInfo.Contains("已引用") || p_strInfo.Contains("重新登录完成") || p_strInfo.Contains("无病人信息") || p_strInfo.Contains("节点项目不可编辑"))
                {
                    this.lblInfo.Top += 15;
                    this.btnOk.Visible = false;
                    if (p_strInfo.Contains("重新登录完成"))
                        this.timer.Interval = 500;
                    else
                        this.timer.Interval = 600;
                    this.timer.Enabled = true;
                    if (p_strInfo.Length < 10)
                    {
                        this.Width = 400;
                        this.lblInfo.Width = 300;
                        this.Height = 82;
                    }
                }
            }
            else if (p_intType == 99)
            {
                this.btnYes.Visible = true;
                this.btnOk.Visible = true;
                this.btnCancel.Visible = true;
                this.btnNo.Visible = false;

                this.btnYes.Text = "单条(&1)";
                this.btnOk.Text = "全部(&2)";
                this.btnCancel.Text = "取消(&C)";
            }
            else
            {
                if (p_blnShowCancel)
                {
                    this.btnCancel.Visible = true;
                }
                else
                {
                    this.btnCancel.Visible = false;
                    this.btnYes.Location = new Point(this.btnYes.Location.X + 30, this.btnYes.Location.Y);
                    this.btnNo.Location = new Point(this.btnNo.Location.X + 45, this.btnNo.Location.Y);
                }
                this.btnOk.Visible = false;
                this.btnYes.Visible = true;
                this.btnNo.Visible = true;
            }
            char[] chrArr = new char[] { '\r', '\n' };
            string[] strArr = p_strInfo.Split(chrArr);
            foreach (string str in strArr)
            {
                if (string.IsNullOrEmpty(str)) continue;
                this.intTimes += Convert.ToInt32(Convert.ToDouble(Math.Ceiling(str.Length / 16M)));
                this.lblInfo.Text += str + Environment.NewLine;
            }
            this.lblInfo.Text = p_strInfo;
            this.intTimes = (intTimes == 0 ? 1 : intTimes);

            if (p_strInfo.Contains("是否"))
                this.pictureBox1.Image = Properties.Resources._21;
            else
                this.pictureBox1.Image = Properties.Resources._22;
        }

        private void frmMsg_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            this.intDiffHeight = this.panelControl.Height - this.lblInfo.Height;
            this.lblInfo.Height = this.lblInfo.Height * this.intTimes + 15;
            if (this.lblInfo.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.lblInfo.Height = Screen.PrimaryScreen.WorkingArea.Height - 100;
            this.StartPosition = FormStartPosition.Manual;
            int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Location = new Point(x, y);

            //if (HopeBridge.Common.Data.GlobalLoginInfo.LookAndFeelSkinValue == 8)
            //{
            //    this.lblInfo.ForeColor = Color.White;
            //}
            Function.AnimateWindow(this.Handle);            
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.DialogResult = DialogResult.OK;
        }

        private void lblInfo_Resize(object sender, EventArgs e)
        {
            this.Height = this.lblInfo.Height + this.intDiffHeight;   
        }

        private void panelControl_MouseDown(object sender, MouseEventArgs e)
        {
            Function.SendMessage(this.Handle);
        }

        private void lblInfo_MouseDown(object sender, MouseEventArgs e)
        {
            Function.SendMessage(this.Handle);
        }        
    }

    /// <summary>
    /// clsDialog
    /// </summary>
    public class DialogBox
    {
        #region 对话框

        /// <summary>
        /// 对话框
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DialogResult Msg(string info)
        {
            frmMsg objFrmMsg = new frmMsg(1, info, false);
            return objFrmMsg.ShowDialog();
        }
        /// <summary>
        /// 对话框
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ico"></param>
        /// <returns></returns>
        public static DialogResult Msg(string info, MessageBoxIcon ico)
        {
            return Do(info, ico, null, false);

            //MessageBoxButtons objMsgButton = MessageBoxButtons.OK;
            //if (p_objMsgIco == MessageBoxIcon.Question)
            //{
            //    objMsgButton = MessageBoxButtons.YesNo;
            //}
            //return MessageBox.Show(p_strInfo, "HQ.EHR温馨提示", objMsgButton, p_objMsgIco);
        }
        /// <summary>
        /// 对话框
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ico"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DialogResult Msg(string info, MessageBoxIcon ico, int flag)
        {
            if (flag != 99) return DialogResult.Cancel;

            frmMsg objFrmMsg = new frmMsg(flag, info, false);
            return objFrmMsg.ShowDialog();
        }
        /// <summary>
        /// 对话框
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ico"></param>
        /// <param name="isShowCancelKey"></param>
        /// <returns></returns>
        public static DialogResult Msg(string info, MessageBoxIcon ico, bool isShowCancelKey)
        {
            return Do(info, ico, null, isShowCancelKey);
        }
        /// <summary>
        /// 对话框
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ico"></param>
        /// <param name="frmParent"></param>
        /// <returns></returns>
        public static DialogResult Msg(string info, MessageBoxIcon ico, System.Windows.Forms.Form frmParent)
        {
            return Do(info, ico, frmParent, false);
        }
        /// <summary>
        /// 对话框
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ico"></param>
        /// <param name="frmParent"></param>
        /// <param name="isShowCancelKey"></param>
        /// <returns></returns>
        public static DialogResult Msg(string info, MessageBoxIcon ico, System.Windows.Forms.Form frmParent, bool isShowCancelKey)
        {
            return Do(info, ico, frmParent, isShowCancelKey);
        }
        /// <summary>
        /// 对话框
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ico"></param>
        /// <param name="frmParent"></param>
        /// <param name="isShowCancelKey"></param>
        /// <returns></returns>
        private static DialogResult Do(string info, MessageBoxIcon ico, System.Windows.Forms.Form frmParent, bool isShowCancelKey)
        {
            int intType = 1;
            if (ico == MessageBoxIcon.Question)
                intType = 0;
            frmMsg objFrmMsg = new frmMsg(intType, info, isShowCancelKey);
            //if (p_frmParent == null || GlobalSysCaseScope.blnExtCall) return objFrmMsg.ShowDialog();
            return objFrmMsg.ShowDialog(frmParent);
        }
        #endregion
    }
}

