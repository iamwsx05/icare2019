namespace Registration.Ui
{
    partial class frmTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtRequest = new DevExpress.XtraEditors.MemoEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtReponse = new DevExpress.XtraEditors.MemoEdit();
            this.btnReg = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.btnSchedu = new DevExpress.XtraEditors.SimpleButton();
            this.btnWeChatPay = new DevExpress.XtraEditors.SimpleButton();
            this.btnBillCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtBookingId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnBinding = new DevExpress.XtraEditors.SimpleButton();
            this.btnBindingCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPat = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefundment = new DevExpress.XtraEditors.SimpleButton();
            this.btnQueue = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelPlan = new DevExpress.XtraEditors.SimpleButton();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReponse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookingId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.btnDelPlan);
            this.pcBackGround.Controls.Add(this.btnQueue);
            this.pcBackGround.Controls.Add(this.btnRefundment);
            this.pcBackGround.Controls.Add(this.btnPat);
            this.pcBackGround.Controls.Add(this.btnBindingCancel);
            this.pcBackGround.Controls.Add(this.btnBinding);
            this.pcBackGround.Controls.Add(this.labelControl1);
            this.pcBackGround.Controls.Add(this.txtBookingId);
            this.pcBackGround.Controls.Add(this.btnBillCancel);
            this.pcBackGround.Controls.Add(this.btnWeChatPay);
            this.pcBackGround.Controls.Add(this.btnSchedu);
            this.pcBackGround.Controls.Add(this.btnQuery);
            this.pcBackGround.Controls.Add(this.btnReg);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcBackGround.Location = new System.Drawing.Point(0, 0);
            this.pcBackGround.Size = new System.Drawing.Size(1370, 40);
            this.pcBackGround.Visible = true;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtRequest);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 40);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1370, 410);
            this.groupControl1.TabIndex = 11;
            this.groupControl1.Text = "request";
            // 
            // txtRequest
            // 
            this.txtRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequest.Location = new System.Drawing.Point(2, 22);
            this.txtRequest.MenuManager = this.barManager;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.Size = new System.Drawing.Size(1366, 386);
            this.txtRequest.TabIndex = 0;
            this.txtRequest.UseOptimizedRendering = true;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtReponse);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl2.Location = new System.Drawing.Point(0, 450);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1370, 300);
            this.groupControl2.TabIndex = 12;
            this.groupControl2.Text = "reponse";
            // 
            // txtReponse
            // 
            this.txtReponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReponse.Location = new System.Drawing.Point(2, 22);
            this.txtReponse.MenuManager = this.barManager;
            this.txtReponse.Name = "txtReponse";
            this.txtReponse.Size = new System.Drawing.Size(1366, 276);
            this.txtReponse.TabIndex = 1;
            this.txtReponse.UseOptimizedRendering = true;
            // 
            // btnReg
            // 
            this.btnReg.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReg.Appearance.Options.UseFont = true;
            this.btnReg.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnReg.Image = ((System.Drawing.Image)(resources.GetObject("btnReg.Image")));
            this.btnReg.Location = new System.Drawing.Point(2, 2);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(98, 36);
            this.btnReg.TabIndex = 0;
            this.btnReg.Text = "1.挂号";
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.Appearance.Options.UseFont = true;
            this.btnQuery.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(100, 2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(98, 36);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "2.查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnSchedu
            // 
            this.btnSchedu.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSchedu.Appearance.Options.UseFont = true;
            this.btnSchedu.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSchedu.Image = ((System.Drawing.Image)(resources.GetObject("btnSchedu.Image")));
            this.btnSchedu.Location = new System.Drawing.Point(198, 2);
            this.btnSchedu.Name = "btnSchedu";
            this.btnSchedu.Size = new System.Drawing.Size(114, 36);
            this.btnSchedu.TabIndex = 2;
            this.btnSchedu.Text = "3.排班计划";
            this.btnSchedu.Click += new System.EventHandler(this.btnSchedu_Click);
            // 
            // btnWeChatPay
            // 
            this.btnWeChatPay.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeChatPay.Appearance.Options.UseFont = true;
            this.btnWeChatPay.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWeChatPay.Image = ((System.Drawing.Image)(resources.GetObject("btnWeChatPay.Image")));
            this.btnWeChatPay.Location = new System.Drawing.Point(312, 2);
            this.btnWeChatPay.Name = "btnWeChatPay";
            this.btnWeChatPay.Size = new System.Drawing.Size(114, 36);
            this.btnWeChatPay.TabIndex = 3;
            this.btnWeChatPay.Text = "4.预约支付";
            this.btnWeChatPay.Click += new System.EventHandler(this.btnWeChatPay_Click);
            // 
            // btnBillCancel
            // 
            this.btnBillCancel.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBillCancel.Appearance.Options.UseFont = true;
            this.btnBillCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBillCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnBillCancel.Image")));
            this.btnBillCancel.Location = new System.Drawing.Point(426, 2);
            this.btnBillCancel.Name = "btnBillCancel";
            this.btnBillCancel.Size = new System.Drawing.Size(150, 36);
            this.btnBillCancel.TabIndex = 4;
            this.btnBillCancel.Text = "5.订单取消或退费";
            this.btnBillCancel.Click += new System.EventHandler(this.btnBillCancel_Click);
            // 
            // txtBookingId
            // 
            this.txtBookingId.Location = new System.Drawing.Point(1160, 57);
            this.txtBookingId.MenuManager = this.barManager;
            this.txtBookingId.Name = "txtBookingId";
            this.txtBookingId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookingId.Properties.Appearance.Options.UseFont = true;
            this.txtBookingId.Size = new System.Drawing.Size(156, 26);
            this.txtBookingId.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(1082, 64);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 14);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "bookingID:";
            // 
            // btnBinding
            // 
            this.btnBinding.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBinding.Appearance.Options.UseFont = true;
            this.btnBinding.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBinding.Image = ((System.Drawing.Image)(resources.GetObject("btnBinding.Image")));
            this.btnBinding.Location = new System.Drawing.Point(576, 2);
            this.btnBinding.Name = "btnBinding";
            this.btnBinding.Size = new System.Drawing.Size(96, 36);
            this.btnBinding.TabIndex = 7;
            this.btnBinding.Text = "6.绑定";
            this.btnBinding.Click += new System.EventHandler(this.btnBinding_Click);
            // 
            // btnBindingCancel
            // 
            this.btnBindingCancel.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBindingCancel.Appearance.Options.UseFont = true;
            this.btnBindingCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBindingCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnBindingCancel.Image")));
            this.btnBindingCancel.Location = new System.Drawing.Point(672, 2);
            this.btnBindingCancel.Name = "btnBindingCancel";
            this.btnBindingCancel.Size = new System.Drawing.Size(120, 36);
            this.btnBindingCancel.TabIndex = 8;
            this.btnBindingCancel.Text = "7.取消绑定";
            this.btnBindingCancel.Click += new System.EventHandler(this.btnBindingCancel_Click);
            // 
            // btnPat
            // 
            this.btnPat.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPat.Appearance.Options.UseFont = true;
            this.btnPat.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPat.Image = ((System.Drawing.Image)(resources.GetObject("btnPat.Image")));
            this.btnPat.Location = new System.Drawing.Point(792, 2);
            this.btnPat.Name = "btnPat";
            this.btnPat.Size = new System.Drawing.Size(120, 36);
            this.btnPat.TabIndex = 9;
            this.btnPat.Text = "8.患者查询";
            this.btnPat.Click += new System.EventHandler(this.btnPat_Click);
            // 
            // btnRefundment
            // 
            this.btnRefundment.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefundment.Appearance.Options.UseFont = true;
            this.btnRefundment.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefundment.Image = ((System.Drawing.Image)(resources.GetObject("btnRefundment.Image")));
            this.btnRefundment.Location = new System.Drawing.Point(912, 2);
            this.btnRefundment.Name = "btnRefundment";
            this.btnRefundment.Size = new System.Drawing.Size(120, 36);
            this.btnRefundment.TabIndex = 10;
            this.btnRefundment.Text = "9.退费";
            this.btnRefundment.Click += new System.EventHandler(this.btnRefundment_Click);
            // 
            // btnQueue
            // 
            this.btnQueue.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQueue.Appearance.Options.UseFont = true;
            this.btnQueue.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnQueue.Image = ((System.Drawing.Image)(resources.GetObject("btnQueue.Image")));
            this.btnQueue.Location = new System.Drawing.Point(1032, 2);
            this.btnQueue.Name = "btnQueue";
            this.btnQueue.Size = new System.Drawing.Size(120, 36);
            this.btnQueue.TabIndex = 11;
            this.btnQueue.Text = "10.队列查询";
            this.btnQueue.Click += new System.EventHandler(this.btnQueue_Click);
            // 
            // btnDelPlan
            // 
            this.btnDelPlan.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelPlan.Appearance.Options.UseFont = true;
            this.btnDelPlan.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelPlan.Image = ((System.Drawing.Image)(resources.GetObject("btnDelPlan.Image")));
            this.btnDelPlan.Location = new System.Drawing.Point(1218, 2);
            this.btnDelPlan.Name = "btnDelPlan";
            this.btnDelPlan.Size = new System.Drawing.Size(150, 36);
            this.btnDelPlan.TabIndex = 12;
            this.btnDelPlan.Text = "删除重复排班计划";
            this.btnDelPlan.Click += new System.EventHandler(this.btnDelPlan_Click);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl1.Location = new System.Drawing.Point(0, 444);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(1370, 6);
            this.splitterControl1.TabIndex = 13;
            this.splitterControl1.TabStop = false;
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 750);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Name = "frmTest";
            this.Text = "接口测试";
            this.Load += new System.EventHandler(this.frmTest_Load);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.groupControl2, 0);
            this.Controls.SetChildIndex(this.groupControl1, 0);
            this.Controls.SetChildIndex(this.splitterControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRequest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReponse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookingId.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnReg;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.MemoEdit txtRequest;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.MemoEdit txtReponse;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.SimpleButton btnSchedu;
        private DevExpress.XtraEditors.SimpleButton btnWeChatPay;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtBookingId;
        private DevExpress.XtraEditors.SimpleButton btnBillCancel;
        private DevExpress.XtraEditors.SimpleButton btnBinding;
        private DevExpress.XtraEditors.SimpleButton btnBindingCancel;
        private DevExpress.XtraEditors.SimpleButton btnPat;
        private DevExpress.XtraEditors.SimpleButton btnRefundment;
        private DevExpress.XtraEditors.SimpleButton btnQueue;
        private DevExpress.XtraEditors.SimpleButton btnDelPlan;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
    }
}