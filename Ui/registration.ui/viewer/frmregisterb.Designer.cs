namespace Registration.Ui
{
    partial class frmRegisterB
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegisterB));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gcPlan = new DevExpress.XtraGrid.GridControl();
            this.gvPlan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.doctImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.picDoct = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.doctName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.doctIntroduce = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.regDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.usedNums = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.surplusNums = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.booking = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnBooking = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.regDid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.regCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.doctCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sortNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deptCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.lueDept = new Common.Controls.LookUpEdit();
            this.lblDept = new DevExpress.XtraEditors.LabelControl();
            this.lblDate1 = new DevExpress.XtraEditors.LabelControl();
            this.dteStart = new DevExpress.XtraEditors.DateEdit();
            this.dteEnd = new DevExpress.XtraEditors.DateEdit();
            this.lblDate2 = new DevExpress.XtraEditors.LabelControl();
            this.btnNormal = new DevExpress.XtraEditors.SimpleButton();
            this.btnExpert = new DevExpress.XtraEditors.SimpleButton();
            this.btnSpec = new DevExpress.XtraEditors.SimpleButton();
            this.btnDept = new DevExpress.XtraEditors.SimpleButton();
            this.lblRegTypeName = new DevExpress.XtraEditors.LabelControl();
            this.lueDoct = new Common.Controls.LookUpEdit();
            this.lblDoct = new DevExpress.XtraEditors.LabelControl();
            this.lblSort = new DevExpress.XtraEditors.LabelControl();
            this.cboSort = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDoct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDoct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSort.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.cboSort);
            this.pcBackGround.Controls.Add(this.lblSort);
            this.pcBackGround.Controls.Add(this.lblDoct);
            this.pcBackGround.Controls.Add(this.lueDoct);
            this.pcBackGround.Controls.Add(this.lblRegTypeName);
            this.pcBackGround.Controls.Add(this.btnDept);
            this.pcBackGround.Controls.Add(this.btnSpec);
            this.pcBackGround.Controls.Add(this.btnExpert);
            this.pcBackGround.Controls.Add(this.btnNormal);
            this.pcBackGround.Controls.Add(this.lblDate2);
            this.pcBackGround.Controls.Add(this.dteEnd);
            this.pcBackGround.Controls.Add(this.dteStart);
            this.pcBackGround.Controls.Add(this.lblDate1);
            this.pcBackGround.Controls.Add(this.lueDept);
            this.pcBackGround.Controls.Add(this.lblDept);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcBackGround.Location = new System.Drawing.Point(0, 0);
            this.pcBackGround.Size = new System.Drawing.Size(1008, 40);
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
            // gcPlan
            // 
            this.gcPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPlan.Location = new System.Drawing.Point(0, 40);
            this.gcPlan.MainView = this.gvPlan;
            this.gcPlan.MenuManager = this.barManager;
            this.gcPlan.Name = "gcPlan";
            this.gcPlan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnBooking,
            this.picDoct,
            this.repositoryItemTextEdit1,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2,
            this.repositoryItemMemoEdit3,
            this.repositoryItemMemoEdit4});
            this.gcPlan.Size = new System.Drawing.Size(1008, 653);
            this.gcPlan.TabIndex = 12;
            this.gcPlan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPlan});
            // 
            // gvPlan
            // 
            this.gvPlan.Appearance.EvenRow.BackColor = System.Drawing.Color.Red;
            this.gvPlan.Appearance.EvenRow.BackColor2 = System.Drawing.Color.Red;
            this.gvPlan.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvPlan.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.gvPlan.Appearance.OddRow.Options.UseBackColor = true;
            this.gvPlan.Appearance.Row.Font = new System.Drawing.Font("宋体", 9F);
            this.gvPlan.Appearance.Row.Options.UseFont = true;
            this.gvPlan.Appearance.ViewCaption.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
            this.gvPlan.Appearance.ViewCaption.Options.UseFont = true;
            this.gvPlan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.doctImage,
            this.doctName,
            this.doctIntroduce,
            this.regDate,
            this.usedNums,
            this.surplusNums,
            this.booking,
            this.regDid,
            this.regCode,
            this.doctCode,
            this.sortNo,
            this.deptCode});
            this.gvPlan.GridControl = this.gcPlan;
            this.gvPlan.GroupCount = 1;
            this.gvPlan.Images = this.imageList;
            this.gvPlan.Name = "gvPlan";
            this.gvPlan.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPlan.OptionsView.ColumnAutoWidth = false;
            this.gvPlan.OptionsView.RowAutoHeight = true;
            this.gvPlan.OptionsView.ShowColumnHeaders = false;
            this.gvPlan.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvPlan.OptionsView.ShowGroupPanel = false;
            this.gvPlan.OptionsView.ShowIndicator = false;
            this.gvPlan.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvPlan.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.regDate, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // doctImage
            // 
            this.doctImage.Caption = "doctImage";
            this.doctImage.ColumnEdit = this.picDoct;
            this.doctImage.FieldName = "doctImage";
            this.doctImage.Name = "doctImage";
            this.doctImage.OptionsColumn.AllowEdit = false;
            this.doctImage.OptionsColumn.AllowFocus = false;
            this.doctImage.OptionsFilter.AllowAutoFilter = false;
            this.doctImage.OptionsFilter.AllowFilter = false;
            this.doctImage.Visible = true;
            this.doctImage.VisibleIndex = 0;
            this.doctImage.Width = 100;
            // 
            // picDoct
            // 
            this.picDoct.Name = "picDoct";
            this.picDoct.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            // 
            // doctName
            // 
            this.doctName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.doctName.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.doctName.AppearanceCell.Options.UseFont = true;
            this.doctName.AppearanceCell.Options.UseForeColor = true;
            this.doctName.AppearanceCell.Options.UseTextOptions = true;
            this.doctName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.doctName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.doctName.Caption = "doctName";
            this.doctName.ColumnEdit = this.repositoryItemMemoEdit2;
            this.doctName.FieldName = "doctName";
            this.doctName.Image = ((System.Drawing.Image)(resources.GetObject("doctName.Image")));
            this.doctName.Name = "doctName";
            this.doctName.OptionsColumn.AllowEdit = false;
            this.doctName.OptionsColumn.AllowFocus = false;
            this.doctName.OptionsFilter.AllowAutoFilter = false;
            this.doctName.OptionsFilter.AllowFilter = false;
            this.doctName.Visible = true;
            this.doctName.VisibleIndex = 1;
            this.doctName.Width = 150;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.repositoryItemMemoEdit2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.repositoryItemMemoEdit2.Appearance.Options.UseFont = true;
            this.repositoryItemMemoEdit2.Appearance.Options.UseForeColor = true;
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // doctIntroduce
            // 
            this.doctIntroduce.AppearanceCell.Font = new System.Drawing.Font("宋体", 9.5F);
            this.doctIntroduce.AppearanceCell.Options.UseFont = true;
            this.doctIntroduce.AppearanceCell.Options.UseTextOptions = true;
            this.doctIntroduce.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.doctIntroduce.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.doctIntroduce.Caption = "doctIntroduce";
            this.doctIntroduce.ColumnEdit = this.repositoryItemMemoEdit1;
            this.doctIntroduce.FieldName = "doctIntroduce";
            this.doctIntroduce.Name = "doctIntroduce";
            this.doctIntroduce.OptionsColumn.AllowEdit = false;
            this.doctIntroduce.OptionsColumn.AllowFocus = false;
            this.doctIntroduce.OptionsFilter.AllowAutoFilter = false;
            this.doctIntroduce.OptionsFilter.AllowFilter = false;
            this.doctIntroduce.Visible = true;
            this.doctIntroduce.VisibleIndex = 2;
            this.doctIntroduce.Width = 600;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.repositoryItemMemoEdit1.Appearance.Options.UseFont = true;
            this.repositoryItemMemoEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemMemoEdit1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // regDate
            // 
            this.regDate.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regDate.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("regDate.AppearanceCell.Image")));
            this.regDate.AppearanceCell.Options.UseFont = true;
            this.regDate.AppearanceCell.Options.UseImage = true;
            this.regDate.AppearanceCell.Options.UseTextOptions = true;
            this.regDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.regDate.Caption = "出诊日期";
            this.regDate.FieldName = "regDate";
            this.regDate.ImageIndex = 1;
            this.regDate.Name = "regDate";
            this.regDate.OptionsColumn.AllowEdit = false;
            this.regDate.OptionsColumn.AllowFocus = false;
            this.regDate.OptionsFilter.AllowAutoFilter = false;
            this.regDate.OptionsFilter.AllowFilter = false;
            this.regDate.Visible = true;
            this.regDate.VisibleIndex = 3;
            this.regDate.Width = 30;
            // 
            // usedNums
            // 
            this.usedNums.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usedNums.AppearanceCell.ForeColor = System.Drawing.Color.LimeGreen;
            this.usedNums.AppearanceCell.Options.UseFont = true;
            this.usedNums.AppearanceCell.Options.UseForeColor = true;
            this.usedNums.AppearanceCell.Options.UseTextOptions = true;
            this.usedNums.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.usedNums.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.usedNums.Caption = "usedNums";
            this.usedNums.ColumnEdit = this.repositoryItemMemoEdit3;
            this.usedNums.FieldName = "usedNums";
            this.usedNums.Name = "usedNums";
            this.usedNums.OptionsColumn.AllowEdit = false;
            this.usedNums.OptionsColumn.AllowFocus = false;
            this.usedNums.OptionsFilter.AllowAutoFilter = false;
            this.usedNums.OptionsFilter.AllowFilter = false;
            this.usedNums.Visible = true;
            this.usedNums.VisibleIndex = 3;
            this.usedNums.Width = 110;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.repositoryItemMemoEdit3.Appearance.Options.UseFont = true;
            this.repositoryItemMemoEdit3.Appearance.Options.UseTextOptions = true;
            this.repositoryItemMemoEdit3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // surplusNums
            // 
            this.surplusNums.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.surplusNums.AppearanceCell.ForeColor = System.Drawing.Color.RoyalBlue;
            this.surplusNums.AppearanceCell.Options.UseFont = true;
            this.surplusNums.AppearanceCell.Options.UseForeColor = true;
            this.surplusNums.AppearanceCell.Options.UseTextOptions = true;
            this.surplusNums.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.surplusNums.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.surplusNums.Caption = "surplusNums";
            this.surplusNums.ColumnEdit = this.repositoryItemMemoEdit4;
            this.surplusNums.FieldName = "surplusNums";
            this.surplusNums.Name = "surplusNums";
            this.surplusNums.OptionsColumn.AllowEdit = false;
            this.surplusNums.OptionsColumn.AllowFocus = false;
            this.surplusNums.OptionsFilter.AllowAutoFilter = false;
            this.surplusNums.OptionsFilter.AllowFilter = false;
            this.surplusNums.Visible = true;
            this.surplusNums.VisibleIndex = 4;
            this.surplusNums.Width = 110;
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.repositoryItemMemoEdit4.Appearance.Options.UseFont = true;
            this.repositoryItemMemoEdit4.Appearance.Options.UseTextOptions = true;
            this.repositoryItemMemoEdit4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // booking
            // 
            this.booking.Caption = "booking";
            this.booking.ColumnEdit = this.btnBooking;
            this.booking.FieldName = "booking";
            this.booking.Name = "booking";
            this.booking.OptionsFilter.AllowAutoFilter = false;
            this.booking.OptionsFilter.AllowFilter = false;
            this.booking.Visible = true;
            this.booking.VisibleIndex = 5;
            // 
            // btnBooking
            // 
            this.btnBooking.AutoHeight = false;
            serializableAppearanceObject1.Options.UseTextOptions = true;
            serializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject1.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnBooking.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "【预约】", 10, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.btnBooking.Name = "btnBooking";
            this.btnBooking.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnBooking.Click += new System.EventHandler(this.btnBooking_Click);
            // 
            // regDid
            // 
            this.regDid.Caption = "regDid";
            this.regDid.FieldName = "regDid";
            this.regDid.Name = "regDid";
            // 
            // regCode
            // 
            this.regCode.Caption = "regCode";
            this.regCode.FieldName = "regCode";
            this.regCode.Name = "regCode";
            // 
            // doctCode
            // 
            this.doctCode.Caption = "doctCode";
            this.doctCode.FieldName = "doctCode";
            this.doctCode.Name = "doctCode";
            // 
            // sortNo
            // 
            this.sortNo.Caption = "sortNo";
            this.sortNo.FieldName = "sortNo";
            this.sortNo.Name = "sortNo";
            // 
            // deptCode
            // 
            this.deptCode.Caption = "deptCode";
            this.deptCode.FieldName = "deptCode";
            this.deptCode.Name = "deptCode";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList.Images.SetKeyName(0, "Status_16x16.png");
            this.imageList.Images.SetKeyName(1, "Today_16x16.png");
            this.imageList.Images.SetKeyName(2, "Info_16x16.png");
            this.imageList.Images.SetKeyName(3, "Solution_16x16.png");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            this.imageList.Images.SetKeyName(9, "");
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // lueDept
            // 
            this.lueDept.CellValueChanged = true;
            this.lueDept.EditValue = "";
            this.lueDept.IsButtonFind = false;
            this.lueDept.Location = new System.Drawing.Point(49, 11);
            this.lueDept.MenuManager = this.barManager;
            this.lueDept.Name = "lueDept";
            this.lueDept.ParentBandedGridView = null;
            this.lueDept.ParentBindingSource = null;
            this.lueDept.ParentGridView = null;
            this.lueDept.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.lueDept.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.lueDept.Properties.Appearance.Options.UseFont = true;
            this.lueDept.Properties.Appearance.Options.UseForeColor = true;
            this.lueDept.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lueDept.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDept.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lueDept.Properties.DataSource = null;
            this.lueDept.Properties.DBRow = null;
            this.lueDept.Properties.DBValue = "";
            this.lueDept.Properties.DescCode = null;
            this.lueDept.Properties.DisplayColumn = null;
            this.lueDept.Properties.DisplayValue = "";
            this.lueDept.Properties.Essential = false;
            this.lueDept.Properties.FieldName = null;
            this.lueDept.Properties.FilterColumn = null;
            this.lueDept.Properties.ForbidPoput = false;
            this.lueDept.Properties.HideColumn = null;
            this.lueDept.Properties.IsAutoPopup = false;
            this.lueDept.Properties.IsCheckValid = true;
            this.lueDept.Properties.IsDescField = false;
            this.lueDept.Properties.IsFreeInput = false;
            this.lueDept.Properties.IsHideValueColumn = false;
            this.lueDept.Properties.IsSelectedMoveNextControl = false;
            this.lueDept.Properties.IsShowColumnHeaders = false;
            this.lueDept.Properties.IsShowDescInfo = false;
            this.lueDept.Properties.IsShowRowNo = false;
            this.lueDept.Properties.IsTab = true;
            this.lueDept.Properties.IsUseShowColumn = false;
            this.lueDept.Properties.ParentBandedGridView = null;
            this.lueDept.Properties.ParentBindingSource = null;
            this.lueDept.Properties.ParentGridView = null;
            this.lueDept.Properties.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.lueDept.Properties.PopupFormSize = new System.Drawing.Size(10, 10);
            this.lueDept.Properties.PopupHeight = 0;
            this.lueDept.Properties.PopupSizeable = false;
            this.lueDept.Properties.PopupWidth = 0;
            this.lueDept.Properties.PresentationMode = 0;
            this.lueDept.Properties.ShowColumn = null;
            this.lueDept.Properties.ShowPopupCloseButton = false;
            this.lueDept.Properties.ShowPopupShadow = false;
            this.lueDept.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueDept.Properties.ValueColumn = null;
            this.lueDept.Size = new System.Drawing.Size(100, 20);
            this.lueDept.TabIndex = 24;
            this.lueDept.HandleDBValueChanged += new Common.Controls._HandleDBValueChanged(this.lueDept_HandleDBValueChanged);
            this.lueDept.HandleResetDBValue += new Common.Controls._HandleResetDBValue(this.lueDept_HandleResetDBValue);
            this.lueDept.EditValueChanged += new System.EventHandler(this.lueDept_EditValueChanged);
            // 
            // lblDept
            // 
            this.lblDept.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDept.Location = new System.Drawing.Point(16, 15);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(30, 12);
            this.lblDept.TabIndex = 23;
            this.lblDept.Text = "科室:";
            // 
            // lblDate1
            // 
            this.lblDate1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate1.Location = new System.Drawing.Point(317, 15);
            this.lblDate1.Name = "lblDate1";
            this.lblDate1.Size = new System.Drawing.Size(54, 12);
            this.lblDate1.TabIndex = 25;
            this.lblDate1.Text = "就诊日期:";
            // 
            // dteStart
            // 
            this.dteStart.EditValue = null;
            this.dteStart.Location = new System.Drawing.Point(373, 9);
            this.dteStart.MenuManager = this.barManager;
            this.dteStart.Name = "dteStart";
            this.dteStart.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.dteStart.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.dteStart.Properties.Appearance.Options.UseFont = true;
            this.dteStart.Properties.Appearance.Options.UseForeColor = true;
            this.dteStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStart.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dteStart.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteStart.Size = new System.Drawing.Size(100, 22);
            this.dteStart.TabIndex = 26;
            this.dteStart.EditValueChanged += new System.EventHandler(this.dteStart_EditValueChanged);
            // 
            // dteEnd
            // 
            this.dteEnd.EditValue = null;
            this.dteEnd.Location = new System.Drawing.Point(501, 9);
            this.dteEnd.MenuManager = this.barManager;
            this.dteEnd.Name = "dteEnd";
            this.dteEnd.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.dteEnd.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.dteEnd.Properties.Appearance.Options.UseFont = true;
            this.dteEnd.Properties.Appearance.Options.UseForeColor = true;
            this.dteEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEnd.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dteEnd.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteEnd.Size = new System.Drawing.Size(100, 22);
            this.dteEnd.TabIndex = 27;
            this.dteEnd.EditValueChanged += new System.EventHandler(this.dteEnd_EditValueChanged);
            // 
            // lblDate2
            // 
            this.lblDate2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDate2.Location = new System.Drawing.Point(484, 18);
            this.lblDate2.Name = "lblDate2";
            this.lblDate2.Size = new System.Drawing.Size(7, 12);
            this.lblDate2.TabIndex = 28;
            this.lblDate2.Text = "~";
            // 
            // btnNormal
            // 
            this.btnNormal.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNormal.Appearance.Options.UseFont = true;
            this.btnNormal.Location = new System.Drawing.Point(932, 1);
            this.btnNormal.Name = "btnNormal";
            this.btnNormal.Size = new System.Drawing.Size(72, 38);
            this.btnNormal.TabIndex = 29;
            this.btnNormal.Text = "普通门诊";
            this.btnNormal.Visible = false;
            this.btnNormal.Click += new System.EventHandler(this.btnNormal_Click);
            // 
            // btnExpert
            // 
            this.btnExpert.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpert.Appearance.Options.UseFont = true;
            this.btnExpert.Location = new System.Drawing.Point(1004, 1);
            this.btnExpert.Name = "btnExpert";
            this.btnExpert.Size = new System.Drawing.Size(72, 38);
            this.btnExpert.TabIndex = 30;
            this.btnExpert.Text = "专家门诊";
            this.btnExpert.Visible = false;
            this.btnExpert.Click += new System.EventHandler(this.btnExpert_Click);
            // 
            // btnSpec
            // 
            this.btnSpec.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpec.Appearance.Options.UseFont = true;
            this.btnSpec.Location = new System.Drawing.Point(1148, 1);
            this.btnSpec.Name = "btnSpec";
            this.btnSpec.Size = new System.Drawing.Size(72, 38);
            this.btnSpec.TabIndex = 31;
            this.btnSpec.Text = "特需门诊";
            this.btnSpec.Visible = false;
            this.btnSpec.Click += new System.EventHandler(this.btnSpec_Click);
            // 
            // btnDept
            // 
            this.btnDept.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDept.Appearance.Options.UseFont = true;
            this.btnDept.Location = new System.Drawing.Point(1076, 1);
            this.btnDept.Name = "btnDept";
            this.btnDept.Size = new System.Drawing.Size(72, 38);
            this.btnDept.TabIndex = 32;
            this.btnDept.Text = "专科门诊";
            this.btnDept.Visible = false;
            this.btnDept.Click += new System.EventHandler(this.btnDept_Click);
            // 
            // lblRegTypeName
            // 
            this.lblRegTypeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegTypeName.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.lblRegTypeName.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRegTypeName.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblRegTypeName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblRegTypeName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblRegTypeName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRegTypeName.Location = new System.Drawing.Point(910, 1);
            this.lblRegTypeName.Name = "lblRegTypeName";
            this.lblRegTypeName.Size = new System.Drawing.Size(96, 38);
            this.lblRegTypeName.TabIndex = 70;
            this.lblRegTypeName.Text = "诊间预约";
            // 
            // lueDoct
            // 
            this.lueDoct.CellValueChanged = true;
            this.lueDoct.EditValue = "";
            this.lueDoct.IsButtonFind = false;
            this.lueDoct.Location = new System.Drawing.Point(199, 11);
            this.lueDoct.MenuManager = this.barManager;
            this.lueDoct.Name = "lueDoct";
            this.lueDoct.ParentBandedGridView = null;
            this.lueDoct.ParentBindingSource = null;
            this.lueDoct.ParentGridView = null;
            this.lueDoct.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.lueDoct.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.lueDoct.Properties.Appearance.Options.UseFont = true;
            this.lueDoct.Properties.Appearance.Options.UseForeColor = true;
            this.lueDoct.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lueDoct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDoct.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lueDoct.Properties.DataSource = null;
            this.lueDoct.Properties.DBRow = null;
            this.lueDoct.Properties.DBValue = "";
            this.lueDoct.Properties.DescCode = null;
            this.lueDoct.Properties.DisplayColumn = null;
            this.lueDoct.Properties.DisplayValue = "";
            this.lueDoct.Properties.Essential = false;
            this.lueDoct.Properties.FieldName = null;
            this.lueDoct.Properties.FilterColumn = null;
            this.lueDoct.Properties.ForbidPoput = false;
            this.lueDoct.Properties.HideColumn = null;
            this.lueDoct.Properties.IsAutoPopup = false;
            this.lueDoct.Properties.IsCheckValid = true;
            this.lueDoct.Properties.IsDescField = false;
            this.lueDoct.Properties.IsFreeInput = false;
            this.lueDoct.Properties.IsHideValueColumn = false;
            this.lueDoct.Properties.IsSelectedMoveNextControl = false;
            this.lueDoct.Properties.IsShowColumnHeaders = false;
            this.lueDoct.Properties.IsShowDescInfo = false;
            this.lueDoct.Properties.IsShowRowNo = false;
            this.lueDoct.Properties.IsTab = true;
            this.lueDoct.Properties.IsUseShowColumn = false;
            this.lueDoct.Properties.ParentBandedGridView = null;
            this.lueDoct.Properties.ParentBindingSource = null;
            this.lueDoct.Properties.ParentGridView = null;
            this.lueDoct.Properties.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.lueDoct.Properties.PopupFormSize = new System.Drawing.Size(10, 10);
            this.lueDoct.Properties.PopupHeight = 0;
            this.lueDoct.Properties.PopupSizeable = false;
            this.lueDoct.Properties.PopupWidth = 0;
            this.lueDoct.Properties.PresentationMode = 0;
            this.lueDoct.Properties.ShowColumn = null;
            this.lueDoct.Properties.ShowPopupCloseButton = false;
            this.lueDoct.Properties.ShowPopupShadow = false;
            this.lueDoct.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueDoct.Properties.ValueColumn = null;
            this.lueDoct.Size = new System.Drawing.Size(100, 20);
            this.lueDoct.TabIndex = 110;
            this.lueDoct.HandleDBValueChanged += new Common.Controls._HandleDBValueChanged(this.lueDoct_HandleDBValueChanged);
            this.lueDoct.HandleResetDBValue += new Common.Controls._HandleResetDBValue(this.lueDoct_HandleResetDBValue);
            this.lueDoct.EditValueChanged += new System.EventHandler(this.lueDoct_EditValueChanged);
            // 
            // lblDoct
            // 
            this.lblDoct.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoct.Location = new System.Drawing.Point(167, 15);
            this.lblDoct.Name = "lblDoct";
            this.lblDoct.Size = new System.Drawing.Size(30, 12);
            this.lblDoct.TabIndex = 111;
            this.lblDoct.Text = "医师:";
            // 
            // lblSort
            // 
            this.lblSort.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSort.Location = new System.Drawing.Point(616, 15);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(30, 12);
            this.lblSort.TabIndex = 112;
            this.lblSort.Text = "排序:";
            // 
            // cboSort
            // 
            this.cboSort.EditValue = "升序";
            this.cboSort.Location = new System.Drawing.Point(648, 9);
            this.cboSort.MinimumSize = new System.Drawing.Size(0, 22);
            this.cboSort.Name = "cboSort";
            this.cboSort.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSort.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.cboSort.Properties.Appearance.Options.UseFont = true;
            this.cboSort.Properties.Appearance.Options.UseForeColor = true;
            this.cboSort.Properties.Appearance.Options.UseTextOptions = true;
            this.cboSort.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cboSort.Properties.AppearanceDisabled.Font = new System.Drawing.Font("宋体", 9.5F);
            this.cboSort.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Crimson;
            this.cboSort.Properties.AppearanceDisabled.Options.UseFont = true;
            this.cboSort.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.cboSort.Properties.AppearanceDropDown.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSort.Properties.AppearanceDropDown.ForeColor = System.Drawing.Color.Crimson;
            this.cboSort.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboSort.Properties.AppearanceDropDown.Options.UseForeColor = true;
            this.cboSort.Properties.AppearanceFocused.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSort.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Crimson;
            this.cboSort.Properties.AppearanceFocused.Options.UseFont = true;
            this.cboSort.Properties.AppearanceFocused.Options.UseForeColor = true;
            this.cboSort.Properties.AutoHeight = false;
            this.cboSort.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSort.Properties.DropDownItemHeight = 22;
            this.cboSort.Properties.Items.AddRange(new object[] {
            "升序",
            "降序"});
            this.cboSort.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboSort.Size = new System.Drawing.Size(64, 22);
            this.cboSort.TabIndex = 113;
            this.cboSort.SelectedIndexChanged += new System.EventHandler(this.cboSort_SelectedIndexChanged);
            // 
            // frmRegisterB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 693);
            this.Controls.Add(this.gcPlan);
            this.Name = "frmRegisterB";
            this.Text = "预约挂号";
            this.Load += new System.EventHandler(this.frmRegisterB_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRegisterB_KeyDown);
            this.Controls.SetChildIndex(this.marqueeProgressBarControl, 0);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.gcPlan, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDoct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDoct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSort.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl gcPlan;
        internal Common.Controls.LookUpEdit lueDept;
        internal DevExpress.XtraEditors.DateEdit dteEnd;
        internal DevExpress.XtraEditors.DateEdit dteStart;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvPlan;
        private DevExpress.XtraGrid.Columns.GridColumn doctImage;
        private DevExpress.XtraGrid.Columns.GridColumn doctName;
        private DevExpress.XtraGrid.Columns.GridColumn doctIntroduce;
        private DevExpress.XtraGrid.Columns.GridColumn regDate;
        private DevExpress.XtraGrid.Columns.GridColumn usedNums;
        private DevExpress.XtraGrid.Columns.GridColumn surplusNums;
        private DevExpress.XtraGrid.Columns.GridColumn booking;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnBooking;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit picDoct;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private System.Windows.Forms.ImageList imageList;
        internal DevExpress.XtraEditors.SimpleButton btnSpec;
        internal DevExpress.XtraEditors.SimpleButton btnExpert;
        internal DevExpress.XtraEditors.SimpleButton btnNormal;
        internal DevExpress.XtraEditors.SimpleButton btnDept;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn regDid;
        private DevExpress.XtraGrid.Columns.GridColumn regCode;
        private DevExpress.XtraGrid.Columns.GridColumn doctCode;
        private DevExpress.XtraGrid.Columns.GridColumn sortNo;
        internal DevExpress.XtraEditors.LabelControl lblRegTypeName;
        internal Common.Controls.LookUpEdit lueDoct;
        internal DevExpress.XtraEditors.LabelControl lblDoct;
        internal DevExpress.XtraEditors.LabelControl lblDept;
        internal DevExpress.XtraEditors.LabelControl lblDate2;
        internal DevExpress.XtraEditors.LabelControl lblDate1;
        internal DevExpress.XtraEditors.LabelControl lblSort;
        internal DevExpress.XtraEditors.ComboBoxEdit cboSort;
        private DevExpress.XtraGrid.Columns.GridColumn deptCode;
    }
}