using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmDebtMessage : Form
    {
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        //下面是可用的常量,按照不合的动画结果声明本身须要的
        private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记该标记
        private const int AW_CENTER = 0x0010;//若应用了AW_HIDE标记,则使窗口向内重叠;不然向外扩大
        private const int AW_HIDE = 0x10000;//隐蔽窗口
        private const int AW_ACTIVE = 0x20000;//激活窗口,在应用了AW_HIDE标记后不要应用这个标记
        private const int AW_SLIDE = 0x40000;//应用滑动类型动画结果,默认为迁移转变动画类型,当应用AW_CENTER标记时,这个标记就被忽视
        private const int AW_BLEND = 0x80000;//应用淡入淡出结果

        public DataTable dtDebt = new DataTable();
        public delegate void recipeDoubleClickEventHandler(object sender, string recipeNum);
        public event recipeDoubleClickEventHandler recipeDoubleClick;

        public frmDebtMessage(DataTable p_dtDebt)
        {
            InitializeComponent();
            this.dtDebt = p_dtDebt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDebtMessage_Load(object sender, EventArgs e)
        {
            this.lblName.Text = "病人 " + this.dtDebt.Rows[0]["patientname_chr"].ToString().Trim() + " 存在欠费处方，共" + dtDebt.Rows.Count.ToString() + "次。";
            for (int i = 0; i < dtDebt.Rows.Count; i++)
            {
                this.lsbRecipe.Items.Add(this.dtDebt.Rows[i]["outpatrecipeid_chr"].ToString().Trim());
            }

            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_VER_NEGATIVE);
        }

        private void frmDebtMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
        }

        private void lsbRecipe_DoubleClick(object sender, EventArgs e)
        {
            string strRecipeNum = this.lsbRecipe.SelectedItem.ToString();
            if (this.recipeDoubleClick != null)
            {
                recipeDoubleClick(this, strRecipeNum);
            }
        }
    }
}