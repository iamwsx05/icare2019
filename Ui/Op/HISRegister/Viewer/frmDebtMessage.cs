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
        //�����ǿ��õĳ���,���ղ��ϵĶ����������������Ҫ��
        private const int AW_HOR_POSITIVE = 0x0001;//����������ʾ����,�ñ�ǿ�����Ǩ��ת�䶯���ͻ���������Ӧ�á�Ӧ��AW_CENTER���ʱ���Ӹñ��
        private const int AW_HOR_NEGATIVE = 0x0002;//����������ʾ����,�ñ�ǿ�����Ǩ��ת�䶯���ͻ���������Ӧ�á�Ӧ��AW_CENTER���ʱ���Ӹñ��
        private const int AW_VER_POSITIVE = 0x0004;//�Զ�������ʾ����,�ñ�ǿ�����Ǩ��ת�䶯���ͻ���������Ӧ�á�Ӧ��AW_CENTER���ʱ���Ӹñ��
        private const int AW_VER_NEGATIVE = 0x0008;//����������ʾ����,�ñ�ǿ�����Ǩ��ת�䶯���ͻ���������Ӧ�á�Ӧ��AW_CENTER���ʱ���Ӹñ�Ǹñ��
        private const int AW_CENTER = 0x0010;//��Ӧ����AW_HIDE���,��ʹ���������ص�;��Ȼ��������
        private const int AW_HIDE = 0x10000;//���δ���
        private const int AW_ACTIVE = 0x20000;//�����,��Ӧ����AW_HIDE��Ǻ�ҪӦ��������
        private const int AW_SLIDE = 0x40000;//Ӧ�û������Ͷ������,Ĭ��ΪǨ��ת�䶯������,��Ӧ��AW_CENTER���ʱ,�����Ǿͱ�����
        private const int AW_BLEND = 0x80000;//Ӧ�õ��뵭�����

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
            this.lblName.Text = "���� " + this.dtDebt.Rows[0]["patientname_chr"].ToString().Trim() + " ����Ƿ�Ѵ�������" + dtDebt.Rows.Count.ToString() + "�Ρ�";
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