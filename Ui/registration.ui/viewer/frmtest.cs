using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Registration.Ui
{
    public partial class frmTest : frmBaseMdi
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            this.txtRequest.Text = "<Request><IdCardNo>440682198904015033</IdCardNo></Request>";
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            string request = this.txtRequest.Text.Trim();
            if (request == string.Empty) return;

            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                this.txtReponse.Text = proxy.Service.SDSBRegister(request); //.WeChatRegister(request);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string request = this.txtRequest.Text.Trim();
            if (request == string.Empty) return;
            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                //this.txtReponse.Text = proxy.Service.GetNextQueueNo2("2016-11-05", "8888", "01").ToString();
                //   this.txtReponse.Text = proxy.Service.WeChatQueryLisReportResult(request); 
            }
            return;
            //using (ProxyMiddle proxy = new ProxyMiddle())
            //{
            //    this.txtReponse.Text = proxy.Service.ProxyQueryLisReport(request); // .ProxyQueryRecipe(request);
            //}
            //return;
            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                this.txtReponse.Text = proxy.Service.GdaLockRegSeq(request);//MessageNotice(request); //WeChatScheduleToday(request);    //.WeChatQueryLisReportList(request); //WeChatQueryRecipe(request);
                //.GdaLockRegSeq(request); //.WeChatQueryPatient(request); //.GdaGetPatMessage(request);
                //.GdaGetPatMessage(request);
                //GetDrRegTimeInfo(request);
                //.GdaInsertReg(request);
                //.GdaLockRegSeq(request);//.GdaGetPatMessage(request);
                //.GdaDocSchedules(request);//.GdaGetDept(request); //proxy.Service.WeChatQuery(request);
            }
        }

        private void btnSchedu_Click(object sender, EventArgs e)
        {
            string request = this.txtRequest.Text.Trim();
            if (request == string.Empty) return;

            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                this.txtReponse.Text = proxy.Service.WeChatSchedule(request);
            }
        }

        private void btnWeChatPay_Click(object sender, EventArgs e)
        {
            string request = this.txtRequest.Text.Trim();
            if (request == string.Empty) return;

            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                this.txtReponse.Text = proxy.Service.WeChatRecipePay(request); //proxy.Service.WeChatRecipePay(request);//WeChatRecipePay(request); //.WeChatQueryRecipeDet(request); //.WeChatRegPay(request);
            }
        }

        private void btnBillCancel_Click(object sender, EventArgs e)
        {
            string request = this.txtBookingId.Text.Trim();
            request = this.txtRequest.Text.Trim();
            if (request == string.Empty) return;

            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                this.txtReponse.Text = proxy.Service.WeChatQueryRecipe(request); //.WeChatCancelOrder(request, "1", "测试");
            }
        }

        private void btnBinding_Click(object sender, EventArgs e)
        {
            string request = this.txtRequest.Text.Trim();
            if (request == string.Empty) return;

            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                this.txtReponse.Text = proxy.Service.WeChatBinding(request);
            }
        }

        private void btnBindingCancel_Click(object sender, EventArgs e)
        {
            string request = this.txtRequest.Text.Trim();
            if (request == string.Empty) return;

            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                this.txtReponse.Text = proxy.Service.WeChatBindingCancel(request);
            }
        }

        private void btnPat_Click(object sender, EventArgs e)
        {
            string request = this.txtRequest.Text.Trim();
            if (request == string.Empty) return;

            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                this.txtReponse.Text = proxy.Service.WeChatNewPatient(request); //.WeChatQueryPatient(request);
            }
        }

        private void btnRefundment_Click(object sender, EventArgs e)
        {

        }

        private void btnQueue_Click(object sender, EventArgs e)
        {
            string request = this.txtRequest.Text.Trim();
            //if (request == string.Empty) return;

            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                // this.txtReponse.Text = proxy.Service //.WeChatDownloadOrderInfo();

                //proxy.Service.WeChatRecipeNotice("6257312", "");

                this.txtReponse.Text = proxy.Service.GdaGetQueue(request); //.WeChatWaitQueue(request);
            }
        }

        private void btnDelPlan_Click(object sender, EventArgs e)
        {
            using (ProxyScheduling proxy = new ProxyScheduling())
            {
                int ret = proxy.Service.DelSchedulingData();
                MessageBox.Show(ret.ToString());
            }
        }

    }
}
