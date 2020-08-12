namespace Common.Controls
{
    partial class frmPrintTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintTemplate));
            this.txtFind = new DevExpress.XtraEditors.TextEdit();
            this.lblFind = new DevExpress.XtraEditors.LabelControl();
            this.tvTemplate = new DevExpress.XtraTreeList.TreeList();
            this.imageList = new System.Windows.Forms.ImageList();
            this.plTop = new DevExpress.XtraEditors.PanelControl();
            this.dteUseEndDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtTemplateDesc = new DevExpress.XtraEditors.TextEdit();
            this.chkTemplateStyle3 = new DevExpress.XtraEditors.CheckEdit();
            this.txtTableCols = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtTableRows = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtVersion = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.chkTemplateStyle2 = new DevExpress.XtraEditors.CheckEdit();
            this.chkTemplateStyle1 = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtTemplateName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtTemplateCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.linkLabelDataCols = new System.Windows.Forms.LinkLabel();
            this.ucPrintControl = new Common.Controls.ucPrintControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lstDataCols = new DevExpress.XtraEditors.MemoEdit();
            this.btnRead = new DevExpress.XtraEditors.SimpleButton();
            this.plMiddle = new System.Windows.Forms.Panel();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plTop)).BeginInit();
            this.plTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteUseEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteUseEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTemplateStyle3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableCols.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableRows.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTemplateStyle2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTemplateStyle1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataCols.Properties)).BeginInit();
            this.plMiddle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.tvTemplate);
            this.pcBackGround.Controls.Add(this.lblFind);
            this.pcBackGround.Controls.Add(this.txtFind);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcBackGround.Location = new System.Drawing.Point(0, 0);
            this.pcBackGround.Size = new System.Drawing.Size(250, 730);
            this.pcBackGround.Visible = true;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // marqueeProgressBarControl
            // 
            this.marqueeProgressBarControl.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtFind
            // 
            this.txtFind.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFind.Location = new System.Drawing.Point(2, 2);
            this.txtFind.Name = "txtFind";
            this.txtFind.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtFind.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.txtFind.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtFind.Properties.Appearance.Options.UseBackColor = true;
            this.txtFind.Properties.Appearance.Options.UseFont = true;
            this.txtFind.Properties.Appearance.Options.UseForeColor = true;
            this.txtFind.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFind.Size = new System.Drawing.Size(246, 20);
            this.txtFind.TabIndex = 95;
            this.txtFind.Enter += new System.EventHandler(this.txtFind_Enter);
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            this.txtFind.Leave += new System.EventHandler(this.txtFind_Leave);
            // 
            // lblFind
            // 
            this.lblFind.Appearance.BackColor = System.Drawing.Color.White;
            this.lblFind.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.lblFind.Appearance.ForeColor = System.Drawing.Color.Silver;
            this.lblFind.Location = new System.Drawing.Point(76, 5);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(84, 12);
            this.lblFind.TabIndex = 96;
            this.lblFind.Text = "请输入查找条件";
            // 
            // tvTemplate
            // 
            this.tvTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTemplate.ImageIndexFieldName = "imageIndex";
            this.tvTemplate.Location = new System.Drawing.Point(2, 22);
            this.tvTemplate.Margin = new System.Windows.Forms.Padding(0);
            this.tvTemplate.Name = "tvTemplate";
            this.tvTemplate.OptionsBehavior.AllowExpandOnDblClick = false;
            this.tvTemplate.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tvTemplate.OptionsSelection.UseIndicatorForSelection = true;
            this.tvTemplate.OptionsView.ShowColumns = false;
            this.tvTemplate.OptionsView.ShowHorzLines = false;
            this.tvTemplate.OptionsView.ShowIndicator = false;
            this.tvTemplate.OptionsView.ShowVertLines = false;
            this.tvTemplate.RowHeight = 22;
            this.tvTemplate.SelectImageList = this.imageList;
            this.tvTemplate.Size = new System.Drawing.Size(246, 706);
            this.tvTemplate.TabIndex = 97;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "r2.png");
            this.imageList.Images.SetKeyName(1, "PageMargins_16x16.png");
            this.imageList.Images.SetKeyName(2, "r3.png");
            this.imageList.Images.SetKeyName(3, "Grid_16x16.png");
            this.imageList.Images.SetKeyName(4, "r1.png");
            this.imageList.Images.SetKeyName(5, "orange.png");
            this.imageList.Images.SetKeyName(6, "green.png");
            this.imageList.Images.SetKeyName(7, "Report_16x16.png");
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.dteUseEndDate);
            this.plTop.Controls.Add(this.labelControl9);
            this.plTop.Controls.Add(this.labelControl8);
            this.plTop.Controls.Add(this.txtTemplateDesc);
            this.plTop.Controls.Add(this.chkTemplateStyle3);
            this.plTop.Controls.Add(this.txtTableCols);
            this.plTop.Controls.Add(this.labelControl7);
            this.plTop.Controls.Add(this.txtTableRows);
            this.plTop.Controls.Add(this.labelControl4);
            this.plTop.Controls.Add(this.labelControl3);
            this.plTop.Controls.Add(this.txtVersion);
            this.plTop.Controls.Add(this.labelControl11);
            this.plTop.Controls.Add(this.chkTemplateStyle2);
            this.plTop.Controls.Add(this.chkTemplateStyle1);
            this.plTop.Controls.Add(this.labelControl6);
            this.plTop.Controls.Add(this.txtTemplateName);
            this.plTop.Controls.Add(this.labelControl26);
            this.plTop.Controls.Add(this.labelControl1);
            this.plTop.Controls.Add(this.txtTemplateCode);
            this.plTop.Controls.Add(this.labelControl2);
            this.plTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTop.Location = new System.Drawing.Point(0, 0);
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(569, 103);
            this.plTop.TabIndex = 12;
            // 
            // dteUseEndDate
            // 
            this.dteUseEndDate.EditValue = null;
            this.dteUseEndDate.EnterMoveNextControl = true;
            this.dteUseEndDate.Location = new System.Drawing.Point(428, 108);
            this.dteUseEndDate.Name = "dteUseEndDate";
            this.dteUseEndDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteUseEndDate.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.dteUseEndDate.Properties.Appearance.Options.UseFont = true;
            this.dteUseEndDate.Properties.Appearance.Options.UseForeColor = true;
            this.dteUseEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteUseEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteUseEndDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dteUseEndDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteUseEndDate.Size = new System.Drawing.Size(109, 22);
            this.dteUseEndDate.TabIndex = 8;
            this.dteUseEndDate.Visible = false;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Location = new System.Drawing.Point(373, 114);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(54, 12);
            this.labelControl9.TabIndex = 128;
            this.labelControl9.Text = "截止日期:";
            this.labelControl9.Visible = false;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Location = new System.Drawing.Point(20, 45);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(54, 12);
            this.labelControl8.TabIndex = 127;
            this.labelControl8.Text = "模板描述:";
            // 
            // txtTemplateDesc
            // 
            this.txtTemplateDesc.EditValue = "";
            this.txtTemplateDesc.EnterMoveNextControl = true;
            this.txtTemplateDesc.Location = new System.Drawing.Point(77, 40);
            this.txtTemplateDesc.Name = "txtTemplateDesc";
            this.txtTemplateDesc.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F);
            this.txtTemplateDesc.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.txtTemplateDesc.Properties.Appearance.Options.UseFont = true;
            this.txtTemplateDesc.Properties.Appearance.Options.UseForeColor = true;
            this.txtTemplateDesc.Properties.MaxLength = 10;
            this.txtTemplateDesc.Size = new System.Drawing.Size(487, 20);
            this.txtTemplateDesc.TabIndex = 4;
            // 
            // chkTemplateStyle3
            // 
            this.chkTemplateStyle3.EnterMoveNextControl = true;
            this.chkTemplateStyle3.Location = new System.Drawing.Point(205, 74);
            this.chkTemplateStyle3.MenuManager = this.barManager;
            this.chkTemplateStyle3.Name = "chkTemplateStyle3";
            this.chkTemplateStyle3.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.chkTemplateStyle3.Properties.Appearance.Options.UseFont = true;
            this.chkTemplateStyle3.Properties.Caption = "交叉 )";
            this.chkTemplateStyle3.Size = new System.Drawing.Size(57, 19);
            this.chkTemplateStyle3.TabIndex = 7;
            this.chkTemplateStyle3.CheckedChanged += new System.EventHandler(this.chkTemplateStyle3_CheckedChanged);
            // 
            // txtTableCols
            // 
            this.txtTableCols.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTableCols.EnterMoveNextControl = true;
            this.txtTableCols.Location = new System.Drawing.Point(351, 72);
            this.txtTableCols.MenuManager = this.barManager;
            this.txtTableCols.Name = "txtTableCols";
            this.txtTableCols.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtTableCols.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.txtTableCols.Properties.Appearance.Options.UseFont = true;
            this.txtTableCols.Properties.Appearance.Options.UseForeColor = true;
            this.txtTableCols.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTableCols.Properties.Mask.EditMask = "d";
            this.txtTableCols.Size = new System.Drawing.Size(72, 22);
            this.txtTableCols.TabIndex = 9;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Location = new System.Drawing.Point(435, 78);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(54, 12);
            this.labelControl7.TabIndex = 122;
            this.labelControl7.Text = "竖表行数:";
            // 
            // txtTableRows
            // 
            this.txtTableRows.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTableRows.EnterMoveNextControl = true;
            this.txtTableRows.Location = new System.Drawing.Point(491, 72);
            this.txtTableRows.MenuManager = this.barManager;
            this.txtTableRows.Name = "txtTableRows";
            this.txtTableRows.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtTableRows.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.txtTableRows.Properties.Appearance.Options.UseFont = true;
            this.txtTableRows.Properties.Appearance.Options.UseForeColor = true;
            this.txtTableRows.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTableRows.Properties.Mask.EditMask = "d";
            this.txtTableRows.Size = new System.Drawing.Size(72, 22);
            this.txtTableRows.TabIndex = 10;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(295, 78);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(54, 12);
            this.labelControl4.TabIndex = 120;
            this.labelControl4.Text = "表格列数:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(20, 13);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(54, 12);
            this.labelControl3.TabIndex = 118;
            this.labelControl3.Text = "模板编码:";
            // 
            // txtVersion
            // 
            this.txtVersion.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtVersion.EnterMoveNextControl = true;
            this.txtVersion.Location = new System.Drawing.Point(491, 8);
            this.txtVersion.MenuManager = this.barManager;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtVersion.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.txtVersion.Properties.Appearance.Options.UseFont = true;
            this.txtVersion.Properties.Appearance.Options.UseForeColor = true;
            this.txtVersion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtVersion.Properties.Mask.EditMask = "d";
            this.txtVersion.Size = new System.Drawing.Size(72, 22);
            this.txtVersion.TabIndex = 11;
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl11.Location = new System.Drawing.Point(447, 13);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(42, 12);
            this.labelControl11.TabIndex = 117;
            this.labelControl11.Text = "版本号:";
            // 
            // chkTemplateStyle2
            // 
            this.chkTemplateStyle2.EnterMoveNextControl = true;
            this.chkTemplateStyle2.Location = new System.Drawing.Point(147, 74);
            this.chkTemplateStyle2.MenuManager = this.barManager;
            this.chkTemplateStyle2.Name = "chkTemplateStyle2";
            this.chkTemplateStyle2.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.chkTemplateStyle2.Properties.Appearance.Options.UseFont = true;
            this.chkTemplateStyle2.Properties.Caption = "横向";
            this.chkTemplateStyle2.Size = new System.Drawing.Size(48, 19);
            this.chkTemplateStyle2.TabIndex = 6;
            this.chkTemplateStyle2.CheckedChanged += new System.EventHandler(this.chkTemplateStyle2_CheckedChanged);
            // 
            // chkTemplateStyle1
            // 
            this.chkTemplateStyle1.EnterMoveNextControl = true;
            this.chkTemplateStyle1.Location = new System.Drawing.Point(89, 74);
            this.chkTemplateStyle1.MenuManager = this.barManager;
            this.chkTemplateStyle1.Name = "chkTemplateStyle1";
            this.chkTemplateStyle1.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.chkTemplateStyle1.Properties.Appearance.Options.UseFont = true;
            this.chkTemplateStyle1.Properties.Caption = "竖向";
            this.chkTemplateStyle1.Size = new System.Drawing.Size(48, 19);
            this.chkTemplateStyle1.TabIndex = 5;
            this.chkTemplateStyle1.CheckedChanged += new System.EventHandler(this.chkTemplateStyle1_CheckedChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(20, 78);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 12);
            this.labelControl6.TabIndex = 113;
            this.labelControl6.Text = "模板类型:(";
            // 
            // txtTemplateName
            // 
            this.txtTemplateName.EditValue = "";
            this.txtTemplateName.EnterMoveNextControl = true;
            this.txtTemplateName.Location = new System.Drawing.Point(256, 8);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F);
            this.txtTemplateName.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.txtTemplateName.Properties.Appearance.Options.UseFont = true;
            this.txtTemplateName.Properties.Appearance.Options.UseForeColor = true;
            this.txtTemplateName.Size = new System.Drawing.Size(180, 20);
            this.txtTemplateName.TabIndex = 3;
            // 
            // labelControl26
            // 
            this.labelControl26.Appearance.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl26.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl26.Location = new System.Drawing.Point(188, 11);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(10, 23);
            this.labelControl26.TabIndex = 112;
            this.labelControl26.Text = "*";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(201, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 12);
            this.labelControl1.TabIndex = 111;
            this.labelControl1.Text = "模板名称:";
            // 
            // txtTemplateCode
            // 
            this.txtTemplateCode.EditValue = "";
            this.txtTemplateCode.EnterMoveNextControl = true;
            this.txtTemplateCode.Location = new System.Drawing.Point(77, 8);
            this.txtTemplateCode.Name = "txtTemplateCode";
            this.txtTemplateCode.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemplateCode.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.txtTemplateCode.Properties.Appearance.Options.UseFont = true;
            this.txtTemplateCode.Properties.Appearance.Options.UseForeColor = true;
            this.txtTemplateCode.Properties.MaxLength = 20;
            this.txtTemplateCode.Size = new System.Drawing.Size(103, 22);
            this.txtTemplateCode.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Location = new System.Drawing.Point(7, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(10, 23);
            this.labelControl2.TabIndex = 109;
            this.labelControl2.Text = "*";
            // 
            // linkLabelDataCols
            // 
            this.linkLabelDataCols.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelDataCols.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkLabelDataCols.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabelDataCols.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.linkLabelDataCols.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkLabelDataCols.Location = new System.Drawing.Point(2, 2);
            this.linkLabelDataCols.Name = "linkLabelDataCols";
            this.linkLabelDataCols.Size = new System.Drawing.Size(181, 23);
            this.linkLabelDataCols.TabIndex = 12;
            this.linkLabelDataCols.Text = "数据列：";
            this.linkLabelDataCols.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucPrintControl
            // 
            this.ucPrintControl.Caption = null;
            this.ucPrintControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPrintControl.IsDockFill = true;
            this.ucPrintControl.IsReloadDictionary = false;
            this.ucPrintControl.IsSave = false;
            this.ucPrintControl.Location = new System.Drawing.Point(0, 103);
            this.ucPrintControl.Name = "ucPrintControl";
            this.ucPrintControl.PrintingSystem = null;
            this.ucPrintControl.ShowStatusBar = false;
            this.ucPrintControl.ShowToolBar = false;
            this.ucPrintControl.Size = new System.Drawing.Size(569, 627);
            this.ucPrintControl.TabIndex = 13;
            this.ucPrintControl.ValueChanged = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lstDataCols);
            this.panelControl1.Controls.Add(this.btnRead);
            this.panelControl1.Controls.Add(this.linkLabelDataCols);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(823, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(185, 730);
            this.panelControl1.TabIndex = 131;
            // 
            // lstDataCols
            // 
            this.lstDataCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDataCols.Location = new System.Drawing.Point(2, 25);
            this.lstDataCols.Name = "lstDataCols";
            this.lstDataCols.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lstDataCols.Properties.Appearance.Options.UseFont = true;
            this.lstDataCols.Size = new System.Drawing.Size(181, 703);
            this.lstDataCols.TabIndex = 132;
            this.lstDataCols.UseOptimizedRendering = true;
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRead.Appearance.Options.UseFont = true;
            this.btnRead.Image = ((System.Drawing.Image)(resources.GetObject("btnRead.Image")));
            this.btnRead.Location = new System.Drawing.Point(62, 1);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(118, 24);
            this.btnRead.TabIndex = 131;
            this.btnRead.Text = "读取 &R   ";
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // plMiddle
            // 
            this.plMiddle.Controls.Add(this.ucPrintControl);
            this.plMiddle.Controls.Add(this.plTop);
            this.plMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMiddle.Location = new System.Drawing.Point(250, 0);
            this.plMiddle.Name = "plMiddle";
            this.plMiddle.Size = new System.Drawing.Size(569, 730);
            this.plMiddle.TabIndex = 132;
            // 
            // splitterControl2
            // 
            this.splitterControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl2.Location = new System.Drawing.Point(819, 0);
            this.splitterControl2.MaximumSize = new System.Drawing.Size(4, 0);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(4, 730);
            this.splitterControl2.TabIndex = 133;
            this.splitterControl2.TabStop = false;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(250, 0);
            this.splitterControl1.MaximumSize = new System.Drawing.Size(4, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(4, 730);
            this.splitterControl1.TabIndex = 134;
            this.splitterControl1.TabStop = false;
            // 
            // frmPrintTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.plMiddle);
            this.Controls.Add(this.splitterControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmPrintTemplate";
            this.Text = "打印模板管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrintTemplate_FormClosing);
            this.Load += new System.EventHandler(this.frmPrintTemplate_Load);
            this.Controls.SetChildIndex(this.marqueeProgressBarControl, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.splitterControl2, 0);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.plMiddle, 0);
            this.Controls.SetChildIndex(this.splitterControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plTop)).EndInit();
            this.plTop.ResumeLayout(false);
            this.plTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteUseEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteUseEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTemplateStyle3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableCols.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableRows.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTemplateStyle2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTemplateStyle1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstDataCols.Properties)).EndInit();
            this.plMiddle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.TextEdit txtFind;
        internal DevExpress.XtraEditors.LabelControl lblFind;
        internal DevExpress.XtraTreeList.TreeList tvTemplate;
        internal ucPrintControl ucPrintControl;
        internal DevExpress.XtraEditors.SpinEdit txtVersion;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        internal DevExpress.XtraEditors.CheckEdit chkTemplateStyle2;
        internal DevExpress.XtraEditors.CheckEdit chkTemplateStyle1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        internal DevExpress.XtraEditors.TextEdit txtTemplateName;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.TextEdit txtTemplateCode;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        internal DevExpress.XtraEditors.TextEdit txtTemplateDesc;
        internal DevExpress.XtraEditors.CheckEdit chkTemplateStyle3;
        internal DevExpress.XtraEditors.SpinEdit txtTableCols;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        internal DevExpress.XtraEditors.SpinEdit txtTableRows;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.DateEdit dteUseEndDate;
        private System.Windows.Forms.LinkLabel linkLabelDataCols;
        internal DevExpress.XtraEditors.PanelControl plTop;
        internal System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton btnRead;
        private System.Windows.Forms.Panel plMiddle;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        internal DevExpress.XtraEditors.MemoEdit lstDataCols;
    }
}