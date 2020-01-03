using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using com.digitalwave.Utility;
using com.digitalwave.Utility.Controls ;
using iCare;
using weCare.Core.Entity;
using com.digitalwave.Utility.ctlTrendViewer;
//using iCare.Common;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// 时间趋势
	/// </summary>
	public class frmTimeDirection : frmViewBase
	{
		#region Variable

        private System.Windows.Forms.Label label1;
        private com.digitalwave.Utility.Controls.ctlComboBox cboCatory;
		private System.Windows.Forms.ListView lvFields;
		private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Desc;
		private System.Windows.Forms.GroupBox grbOptions;
		private com.digitalwave.Utility.ctlTrendViewer.ctlTrendChart ctrTimeDirection;
		private System.Windows.Forms.GroupBox 标记属性;
		private com.digitalwave.Utility.Controls.ctlComboBox cboMarker;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdColor;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		clsTimeDirectionDomain m_objTimeDirectionDomain=new clsTimeDirectionDomain();
        clsTimeDirectionTableValue[] m_objTimeDirectTables = null;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label5;
		private com.digitalwave.Utility.Controls.ctlTimePicker dtpEndTime;
		private System.Windows.Forms.Label lblEndTime;
		private com.digitalwave.Utility.Controls.ctlTimePicker dtpBeginTime;
		private PinkieControls.ButtonXP cmdTimeDirection;
		clsTimeDirectionFieldValue [] m_objTimeDirectFields=null;
		private PinkieControls.ButtonXP m_cmdBack;
		private PinkieControls.ButtonXP m_cmdForward;
		protected com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtHow;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ContextMenu ctmOption;
		private System.Windows.Forms.MenuItem mniDisplay;
		private System.Windows.Forms.MenuItem mniDisplay_Scatter;
		private System.Windows.Forms.MenuItem mniDisplay_Continuous;
		private System.Windows.Forms.MenuItem mniDisplay_Mixed;
		private System.Windows.Forms.MenuItem mniResolution;
		private System.Windows.Forms.MenuItem mniResolution_1Minute;
		private System.Windows.Forms.MenuItem mniResolution_5Minute;
		private System.Windows.Forms.MenuItem mniResolution_30Minute;
		private System.Windows.Forms.MenuItem mniResolution_1Hour;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboTimeSection;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label8;
		#endregion

		public frmTimeDirection()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			
			#region 画白边,在构造函数中执行此段代码,刘颖源,2003-6-6 12:08:01
//			m_objBorderTool = new clsBorderTool(Color.White);
//
//			foreach(Control ctlControl in this.grbParameter.Controls)
//			{
//				string typeName = ctlControl.GetType().Name;
//				if(typeName == "ctlRichTextBox" )
//				{
//					m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
//										{
//											ctlControl ,
//					});
//				}
//				if(typeName == "TextBox" )
//				{
//					m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
//										{
//											ctlControl ,
//					});
//				}
//				if(typeName == "ListView")
//				{
//					m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
//										{
//											ctlControl ,
//					});
//				}
//
//				if(typeName =="TreeView")
//				{
//					m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
//										{
//											ctlControl ,
//					});
//				}
//				if(typeName == "GroupBox")
//				{
//					foreach(Control ctlGrp in ((GroupBox)ctlControl).Controls)
//					{
//						string strSubTypeName = ctlGrp.GetType().Name;
//						if(strSubTypeName == "PictureBox")
//						{
//							m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
//														{
//															ctlGrp ,
//							});
//						}
//					}
//				}
//			}
			#endregion

			#region 画白边,在构造函数中执行此段代码,刘颖源,2003-6-6 12:08:01
//			foreach(Control ctlControl in this.grbOptions.Controls)
//			{
//				string typeName = ctlControl.GetType().Name;
//				if(typeName == "ctlRichTextBox" )
//				{
//					m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
//										{
//											ctlControl ,
//					});
//				}
//				if(typeName == "TextBox" )
//				{
//					m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
//										{
//											ctlControl ,
//					});
//				}
//				if(typeName == "ListView")
//				{
//					m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
//										{
//											ctlControl ,
//					});
//				}
//
//				if(typeName =="TreeView")
//				{
//					m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
//										{
//											ctlControl ,
//					});
//				}
//				if(typeName == "GroupBox")
//				{
//					foreach(Control ctlGrp in ((GroupBox)ctlControl).Controls)
//					{
//						string strSubTypeName = ctlGrp.GetType().Name;
//						if(strSubTypeName == "PictureBox")
//						{
//							m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
//														{
//															ctlGrp ,
//							});
//						}
//					}
//				}
//			}
			#endregion

			cboMarker.AddItem("空心正方形");
			cboMarker.AddItem("实心正方形");
			cboMarker.AddItem("空心圆形");
			cboMarker.AddItem("星号");
			cboMarker.AddItem("加号");
			cboMarker.AddItem("上箭头");
			cboMarker.AddItem("下箭头");
			cboMarker.SelectedIndex =0;

			m_cboTimeSection.AddRangeItems(new string[]{"1","4","8","12","24"});
			m_cboTimeSection.SelectedIndex = 0;

			m_txtHow.KeyPress += new KeyPressEventHandler(clsPublicFunction.s_mthTextBoxOnlyDigit);
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTimeDirection));
            this.标记属性 = new System.Windows.Forms.GroupBox();
            this.cboMarker = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.grbOptions = new System.Windows.Forms.GroupBox();
            this.lvFields = new System.Windows.Forms.ListView();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.Desc = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCatory = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.ctmOption = new System.Windows.Forms.ContextMenu();
            this.mniDisplay = new System.Windows.Forms.MenuItem();
            this.mniDisplay_Scatter = new System.Windows.Forms.MenuItem();
            this.mniDisplay_Continuous = new System.Windows.Forms.MenuItem();
            this.mniDisplay_Mixed = new System.Windows.Forms.MenuItem();
            this.mniResolution = new System.Windows.Forms.MenuItem();
            this.mniResolution_1Minute = new System.Windows.Forms.MenuItem();
            this.mniResolution_5Minute = new System.Windows.Forms.MenuItem();
            this.mniResolution_30Minute = new System.Windows.Forms.MenuItem();
            this.mniResolution_1Hour = new System.Windows.Forms.MenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m_cboTimeSection = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.dtpEndTime = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.dtpBeginTime = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.cmdTimeDirection = new PinkieControls.ButtonXP();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_cmdForward = new PinkieControls.ButtonXP();
            this.label6 = new System.Windows.Forms.Label();
            this.m_txtHow = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_cmdBack = new PinkieControls.ButtonXP();
            this.ctrTimeDirection = new com.digitalwave.Utility.ctlTrendViewer.ctlTrendChart();
            this.m_pnlNewBase.SuspendLayout();
            this.标记属性.SuspendLayout();
            this.grbOptions.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Size = new System.Drawing.Size(991, 66);
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(989, 30);
            // 
            // 标记属性
            // 
            this.标记属性.Controls.Add(this.cboMarker);
            this.标记属性.Controls.Add(this.label2);
            this.标记属性.Controls.Add(this.cmdColor);
            this.标记属性.Controls.Add(this.label3);
            this.标记属性.Font = new System.Drawing.Font("宋体", 10.5F);
            this.标记属性.Location = new System.Drawing.Point(736, 52);
            this.标记属性.Name = "标记属性";
            this.标记属性.Size = new System.Drawing.Size(236, 110);
            this.标记属性.TabIndex = 471;
            this.标记属性.TabStop = false;
            this.标记属性.Text = "标记属性";
            // 
            // cboMarker
            // 
            this.cboMarker.BackColor = System.Drawing.SystemColors.Control;
            this.cboMarker.BorderColor = System.Drawing.Color.Black;
            this.cboMarker.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboMarker.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboMarker.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboMarker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMarker.flatFont = new System.Drawing.Font("宋体", 10.5F);
            this.cboMarker.Font = new System.Drawing.Font("宋体", 10.5F);
            this.cboMarker.ForeColor = System.Drawing.Color.White;
            this.cboMarker.ListBackColor = System.Drawing.Color.White;
            this.cboMarker.ListForeColor = System.Drawing.Color.Black;
            this.cboMarker.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboMarker.ListSelectedForeColor = System.Drawing.Color.White;
            this.cboMarker.Location = new System.Drawing.Point(68, 32);
            this.cboMarker.m_BlnEnableItemEventMenu = false;
            this.cboMarker.MaxLength = 32767;
            this.cboMarker.Name = "cboMarker";
            this.cboMarker.SelectedIndex = -1;
            this.cboMarker.SelectedItem = null;
            this.cboMarker.SelectionStart = 0;
            this.cboMarker.Size = new System.Drawing.Size(164, 23);
            this.cboMarker.TabIndex = 140;
            this.cboMarker.TextBackColor = System.Drawing.Color.White;
            this.cboMarker.TextForeColor = System.Drawing.Color.Black;
            this.cboMarker.SelectedIndexChanged += new System.EventHandler(this.cboMarker_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 420;
            this.label2.Text = "标记:";
            // 
            // cmdColor
            // 
            this.cmdColor.BackColor = System.Drawing.Color.Green;
            this.cmdColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdColor.Location = new System.Drawing.Point(68, 70);
            this.cmdColor.Name = "cmdColor";
            this.cmdColor.Size = new System.Drawing.Size(164, 23);
            this.cmdColor.TabIndex = 150;
            this.cmdColor.UseVisualStyleBackColor = false;
            this.cmdColor.Click += new System.EventHandler(this.cmdColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 421;
            this.label3.Text = "颜色:";
            // 
            // grbOptions
            // 
            this.grbOptions.Controls.Add(this.lvFields);
            this.grbOptions.Location = new System.Drawing.Point(4, 52);
            this.grbOptions.Name = "grbOptions";
            this.grbOptions.Size = new System.Drawing.Size(724, 196);
            this.grbOptions.TabIndex = 470;
            this.grbOptions.TabStop = false;
            this.grbOptions.Text = "参数";
            // 
            // lvFields
            // 
            this.lvFields.BackColor = System.Drawing.Color.White;
            this.lvFields.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvFields.CheckBoxes = true;
            this.lvFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Desc,
            this.columnHeader1,
            this.columnHeader2});
            this.lvFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFields.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lvFields.ForeColor = System.Drawing.Color.Black;
            this.lvFields.FullRowSelect = true;
            this.lvFields.GridLines = true;
            this.lvFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvFields.Location = new System.Drawing.Point(3, 19);
            this.lvFields.MultiSelect = false;
            this.lvFields.Name = "lvFields";
            this.lvFields.Size = new System.Drawing.Size(718, 174);
            this.lvFields.TabIndex = 130;
            this.lvFields.UseCompatibleStateImageBehavior = false;
            this.lvFields.View = System.Windows.Forms.View.Details;
            this.lvFields.SelectedIndexChanged += new System.EventHandler(this.lvFields_SelectedIndexChanged);
            // 
            // ID
            // 
            this.ID.Text = "编号";
            this.ID.Width = 100;
            // 
            // Desc
            // 
            this.Desc.Text = "描述";
            this.Desc.Width = 400;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "标记";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "颜色";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 422;
            this.label1.Text = "种类:";
            // 
            // cboCatory
            // 
            this.cboCatory.BackColor = System.Drawing.SystemColors.Control;
            this.cboCatory.BorderColor = System.Drawing.Color.Black;
            this.cboCatory.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboCatory.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboCatory.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboCatory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCatory.flatFont = new System.Drawing.Font("宋体", 10.5F);
            this.cboCatory.Font = new System.Drawing.Font("宋体", 10.5F);
            this.cboCatory.ForeColor = System.Drawing.Color.Black;
            this.cboCatory.ListBackColor = System.Drawing.Color.White;
            this.cboCatory.ListForeColor = System.Drawing.Color.Black;
            this.cboCatory.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboCatory.ListSelectedForeColor = System.Drawing.Color.White;
            this.cboCatory.Location = new System.Drawing.Point(60, 22);
            this.cboCatory.m_BlnEnableItemEventMenu = false;
            this.cboCatory.MaxLength = 32767;
            this.cboCatory.Name = "cboCatory";
            this.cboCatory.SelectedIndex = -1;
            this.cboCatory.SelectedItem = null;
            this.cboCatory.SelectionStart = 0;
            this.cboCatory.Size = new System.Drawing.Size(208, 23);
            this.cboCatory.TabIndex = 100;
            this.cboCatory.TextBackColor = System.Drawing.Color.White;
            this.cboCatory.TextForeColor = System.Drawing.Color.Black;
            this.cboCatory.SelectedIndexChanged += new System.EventHandler(this.cboCatory_SelectedIndexChanged);
            // 
            // ctmOption
            // 
            this.ctmOption.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniDisplay,
            this.mniResolution});
            // 
            // mniDisplay
            // 
            this.mniDisplay.Index = 0;
            this.mniDisplay.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniDisplay_Scatter,
            this.mniDisplay_Continuous,
            this.mniDisplay_Mixed});
            this.mniDisplay.Text = "显   示";
            // 
            // mniDisplay_Scatter
            // 
            this.mniDisplay_Scatter.Index = 0;
            this.mniDisplay_Scatter.Text = "离  散";
            this.mniDisplay_Scatter.Click += new System.EventHandler(this.m_mthDisplayMode);
            // 
            // mniDisplay_Continuous
            // 
            this.mniDisplay_Continuous.Index = 1;
            this.mniDisplay_Continuous.Text = "连  续";
            this.mniDisplay_Continuous.Click += new System.EventHandler(this.m_mthDisplayMode);
            // 
            // mniDisplay_Mixed
            // 
            this.mniDisplay_Mixed.Checked = true;
            this.mniDisplay_Mixed.Index = 2;
            this.mniDisplay_Mixed.Text = "混  合";
            this.mniDisplay_Mixed.Click += new System.EventHandler(this.m_mthDisplayMode);
            // 
            // mniResolution
            // 
            this.mniResolution.Index = 1;
            this.mniResolution.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniResolution_1Minute,
            this.mniResolution_5Minute,
            this.mniResolution_30Minute,
            this.mniResolution_1Hour});
            this.mniResolution.Text = "分辨率";
            // 
            // mniResolution_1Minute
            // 
            this.mniResolution_1Minute.Index = 0;
            this.mniResolution_1Minute.Text = "1   分钟";
            this.mniResolution_1Minute.Click += new System.EventHandler(this.m_mthResolutionMode);
            // 
            // mniResolution_5Minute
            // 
            this.mniResolution_5Minute.Checked = true;
            this.mniResolution_5Minute.Index = 1;
            this.mniResolution_5Minute.Text = "5   分钟";
            this.mniResolution_5Minute.Click += new System.EventHandler(this.m_mthResolutionMode);
            // 
            // mniResolution_30Minute
            // 
            this.mniResolution_30Minute.Index = 2;
            this.mniResolution_30Minute.Text = "30 分钟";
            this.mniResolution_30Minute.Click += new System.EventHandler(this.m_mthResolutionMode);
            // 
            // mniResolution_1Hour
            // 
            this.mniResolution_1Hour.Index = 3;
            this.mniResolution_1Hour.Text = "1   小时";
            this.mniResolution_1Hour.Click += new System.EventHandler(this.m_mthResolutionMode);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cboCatory);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.grbOptions);
            this.groupBox2.Controls.Add(this.标记属性);
            this.groupBox2.Controls.Add(this.m_cboTimeSection);
            this.groupBox2.Controls.Add(this.dtpEndTime);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lblEndTime);
            this.groupBox2.Controls.Add(this.dtpBeginTime);
            this.groupBox2.Controls.Add(this.cmdTimeDirection);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(8, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(984, 252);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "显示设置";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(452, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 14);
            this.label8.TabIndex = 477;
            this.label8.Text = "小时";
            // 
            // m_cboTimeSection
            // 
            this.m_cboTimeSection.BackColor = System.Drawing.SystemColors.Control;
            this.m_cboTimeSection.BorderColor = System.Drawing.Color.Black;
            this.m_cboTimeSection.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboTimeSection.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboTimeSection.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboTimeSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboTimeSection.flatFont = new System.Drawing.Font("宋体", 10.5F);
            this.m_cboTimeSection.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_cboTimeSection.ForeColor = System.Drawing.Color.Black;
            this.m_cboTimeSection.ListBackColor = System.Drawing.Color.White;
            this.m_cboTimeSection.ListForeColor = System.Drawing.Color.Black;
            this.m_cboTimeSection.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboTimeSection.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboTimeSection.Location = new System.Drawing.Point(372, 22);
            this.m_cboTimeSection.m_BlnEnableItemEventMenu = false;
            this.m_cboTimeSection.MaxLength = 32767;
            this.m_cboTimeSection.Name = "m_cboTimeSection";
            this.m_cboTimeSection.SelectedIndex = -1;
            this.m_cboTimeSection.SelectedItem = null;
            this.m_cboTimeSection.SelectionStart = 0;
            this.m_cboTimeSection.Size = new System.Drawing.Size(64, 23);
            this.m_cboTimeSection.TabIndex = 101;
            this.m_cboTimeSection.TextBackColor = System.Drawing.Color.White;
            this.m_cboTimeSection.TextForeColor = System.Drawing.Color.Black;
            this.m_cboTimeSection.SelectedIndexChanged += new System.EventHandler(this.m_cboTimeSection_SelectedIndexChanged);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.BorderColor = System.Drawing.Color.Black;
            this.dtpEndTime.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtpEndTime.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.dtpEndTime.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.dtpEndTime.DropButtonForeColor = System.Drawing.Color.Black;
            this.dtpEndTime.flatFont = new System.Drawing.Font("宋体", 12F);
            this.dtpEndTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(756, 22);
            this.dtpEndTime.m_BlnOnlyTime = false;
            this.dtpEndTime.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEndTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEndTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ReadOnly = false;
            this.dtpEndTime.Size = new System.Drawing.Size(216, 22);
            this.dtpEndTime.TabIndex = 120;
            this.dtpEndTime.TextBackColor = System.Drawing.Color.White;
            this.dtpEndTime.TextForeColor = System.Drawing.Color.Black;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(284, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 14);
            this.label5.TabIndex = 102;
            this.label5.Text = "常用时间段:";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lblEndTime.ForeColor = System.Drawing.Color.Black;
            this.lblEndTime.Location = new System.Drawing.Point(720, 24);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(28, 14);
            this.lblEndTime.TabIndex = 466;
            this.lblEndTime.Text = "至:";
            // 
            // dtpBeginTime
            // 
            this.dtpBeginTime.BorderColor = System.Drawing.Color.Black;
            this.dtpBeginTime.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtpBeginTime.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.dtpBeginTime.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.dtpBeginTime.DropButtonForeColor = System.Drawing.Color.Black;
            this.dtpBeginTime.flatFont = new System.Drawing.Font("宋体", 12F);
            this.dtpBeginTime.Font = new System.Drawing.Font("宋体", 10.5F);
            this.dtpBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBeginTime.Location = new System.Drawing.Point(496, 22);
            this.dtpBeginTime.m_BlnOnlyTime = false;
            this.dtpBeginTime.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpBeginTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpBeginTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpBeginTime.Name = "dtpBeginTime";
            this.dtpBeginTime.ReadOnly = false;
            this.dtpBeginTime.Size = new System.Drawing.Size(216, 22);
            this.dtpBeginTime.TabIndex = 110;
            this.dtpBeginTime.TextBackColor = System.Drawing.Color.White;
            this.dtpBeginTime.TextForeColor = System.Drawing.Color.Black;
            // 
            // cmdTimeDirection
            // 
            this.cmdTimeDirection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdTimeDirection.DefaultScheme = true;
            this.cmdTimeDirection.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdTimeDirection.Hint = "";
            this.cmdTimeDirection.Location = new System.Drawing.Point(896, 212);
            this.cmdTimeDirection.Name = "cmdTimeDirection";
            this.cmdTimeDirection.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdTimeDirection.Size = new System.Drawing.Size(76, 32);
            this.cmdTimeDirection.TabIndex = 0;
            this.cmdTimeDirection.Text = "查  看";
            this.cmdTimeDirection.Click += new System.EventHandler(this.cmdTimeDirection_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_cmdForward);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.m_txtHow);
            this.groupBox3.Controls.Add(this.m_cmdBack);
            this.groupBox3.Location = new System.Drawing.Point(736, 160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(236, 48);
            this.groupBox3.TabIndex = 478;
            this.groupBox3.TabStop = false;
            // 
            // m_cmdForward
            // 
            this.m_cmdForward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdForward.DefaultScheme = true;
            this.m_cmdForward.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdForward.Hint = "";
            this.m_cmdForward.Location = new System.Drawing.Point(164, 16);
            this.m_cmdForward.Name = "m_cmdForward";
            this.m_cmdForward.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdForward.Size = new System.Drawing.Size(40, 24);
            this.m_cmdForward.TabIndex = 473;
            this.m_cmdForward.Text = ">>";
            this.m_cmdForward.Click += new System.EventHandler(this.m_cmdForward_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(124, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 14);
            this.label6.TabIndex = 475;
            this.label6.Text = "分钟";
            // 
            // m_txtHow
            // 
            this.m_txtHow.AccessibleName = "NoDefault";
            this.m_txtHow.BackColor = System.Drawing.Color.White;
            this.m_txtHow.BorderColor = System.Drawing.Color.Transparent;
            this.m_txtHow.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_txtHow.ForeColor = System.Drawing.Color.Black;
            this.m_txtHow.Location = new System.Drawing.Point(72, 18);
            this.m_txtHow.Name = "m_txtHow";
            this.m_txtHow.Size = new System.Drawing.Size(48, 23);
            this.m_txtHow.TabIndex = 474;
            this.m_txtHow.Text = "5";
            this.m_txtHow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_cmdBack
            // 
            this.m_cmdBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdBack.DefaultScheme = true;
            this.m_cmdBack.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdBack.Hint = "";
            this.m_cmdBack.Location = new System.Drawing.Point(24, 16);
            this.m_cmdBack.Name = "m_cmdBack";
            this.m_cmdBack.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdBack.Size = new System.Drawing.Size(40, 24);
            this.m_cmdBack.TabIndex = 472;
            this.m_cmdBack.Text = "<<";
            this.m_cmdBack.Click += new System.EventHandler(this.m_cmdBack_Click);
            // 
            // ctrTimeDirection
            // 
            this.ctrTimeDirection.Location = new System.Drawing.Point(8, 328);
            this.ctrTimeDirection.m_BlnShowAlarm = true;
            this.ctrTimeDirection.m_ClrChartBackColor = System.Drawing.Color.White;
            this.ctrTimeDirection.m_ClrChartColor = System.Drawing.Color.Black;
            this.ctrTimeDirection.m_ClrDateColor = System.Drawing.Color.Black;
            this.ctrTimeDirection.m_DtmStartDate = new System.DateTime(2004, 12, 13, 11, 0, 0, 0);
            this.ctrTimeDirection.m_EnmDisplay = com.digitalwave.Utility.ctlTrendViewer.enmDisplay.Mixed;
            this.ctrTimeDirection.m_EnmResolution = com.digitalwave.Utility.ctlTrendViewer.enmResolution.Five_Minute;
            this.ctrTimeDirection.m_IntTotalTime = 25;
            this.ctrTimeDirection.Name = "ctrTimeDirection";
            this.ctrTimeDirection.Size = new System.Drawing.Size(984, 304);
            this.ctrTimeDirection.TabIndex = 10000004;
            // 
            // frmTimeDirection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1004, 645);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ctrTimeDirection);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTimeDirection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "时间趋势";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTimeDirection_Load);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.ctrTimeDirection, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.标记属性.ResumeLayout(false);
            this.标记属性.PerformLayout();
            this.grbOptions.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void frmTimeDirection_Load(object sender, System.EventArgs e)
		{
			m_objTimeDirectTables = m_objTimeDirectionDomain.lngGetAllTimeDirectionTable();
			if(m_objTimeDirectTables !=null && m_objTimeDirectTables.Length >0)
			{
				this.cboCatory.ClearItem ();
				for(int i=0;i<m_objTimeDirectTables.Length ;i++)
					this.cboCatory.AddItem (m_objTimeDirectTables[i].m_strT_TableDesc); 
			}
			this.cmdColor.BackColor = Color.Red ;
		}

        //// 从数据库读入指定病人的信息
        //protected override void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
        //{
                 
        //}

		private void cboCatory_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cboCatory.SelectedIndex >=0 && this.cboCatory.SelectedIndex <this.cboCatory.GetItemsCount ())
			{
				string strTableID=m_objTimeDirectTables[this.cboCatory.SelectedIndex ].m_strT_ID ;
				m_objTimeDirectFields=m_objTimeDirectionDomain.lngGetAllTimeDirectionField (strTableID);
				if(m_objTimeDirectFields !=null && m_objTimeDirectFields.Length >0)
				{
					this.lvFields.Items.Clear (); 
					Color[] objColors=new Color[10];
					for(int i=0;i<objColors.Length ;i++)
					{
						objColors[i]=new Color ();
					}
					objColors[0]=Color.Red ;
					objColors[1]=Color.Green ; 
					objColors[2]=Color.Blue  ;
					objColors[3]=Color.Black  ; 
					objColors[4]=Color.Chocolate   ;
					objColors[5]=Color.Yellow  ; 
					objColors[6]=Color.YellowGreen  ;
					objColors[8]=Color.Tomato  ; 
					objColors[9]=Color.Brown  ;

					for(int i=0;i<m_objTimeDirectFields.Length ;i++)
					{
						ListViewItem objListViewItem=new ListViewItem (m_objTimeDirectFields[i].m_strF_ID  );
						objListViewItem.SubItems.Add (m_objTimeDirectFields[i].m_strF_FieldDesc );
						int k=(i % cboMarker.GetItemsCount ());
						objListViewItem.Tag =k.ToString (); 						
						objListViewItem.SubItems.Add (cboMarker.GetItem(k).ToString () );
						objListViewItem.SubItems.Add ("■"); 
						objListViewItem.UseItemStyleForSubItems =false;
						objListViewItem.SubItems[3].ForeColor =objColors[i % objColors.Length ]; 
						
						objListViewItem.Checked =true;
						this.lvFields.Items.Add (objListViewItem);
					}
				}
//				string [] strFieldNames=null;
//				string [,] strRowValue=null;
				string strSqlStatement=m_objTimeDirectTables[this.cboCatory.SelectedIndex ].m_strT_AvailableParameterSQL ;
				string strDisplayFormat=m_objTimeDirectTables[this.cboCatory.SelectedIndex ].m_strT_DisplayFormat  ;
//				m_objTimeDirectionDomain.lngGetAllAvailableParamenters (strSqlStatement,out strFieldNames ,out strRowValue ); 
//				this.lsvParamenters.Clear (); 
//				if(strFieldNames !=null && strFieldNames.Length >0)
//				{
//					string [] strDisplays=strDisplayFormat.Split (',');
//					for(int i=0;i<strFieldNames.Length ;i++)
//					{
//						int intWidth=100;
//						if(strDisplays.Length >i && strDisplays[i]!=null && strDisplays[i].Trim() !="")
//						{
//							if(strDisplays[i].IndexOf ("%")>=0)
//								intWidth = this.lsvParamenters.Width * int.Parse ("0" + strDisplays[i].Replace ("%","").Trim ()) / 100;
//							else
//								intWidth = int.Parse ("0" + strDisplays[i].Trim ()); 
//						}
//						this.lsvParamenters.Columns.Add (strFieldNames[i],intWidth,System.Windows.Forms.HorizontalAlignment.Left ); 
//					}
//					for(int i=0;i<strRowValue.Length / strFieldNames.Length ;i++)
//					{
//						string[] strItems=new string [strFieldNames.Length];
//						for(int j=0;j<strFieldNames.Length ;j++)
//						{
//							strItems[j]=strRowValue[i,j];
//						}
//						ListViewItem objItem=new ListViewItem (strItems);
//						this.lsvParamenters.Items.Add (objItem );
//					}
//				}
			}
		}

		private void cmdTimeDirection_Click(object sender, System.EventArgs e)
		{
			int intIndex=this.cboCatory.SelectedIndex ;
			if(intIndex <0 || intIndex >=this.cboCatory.GetItemsCount ())
			{
				
				MessageBox.Show("请选择时间趋势的种类");
				return;
			}
			if(m_objTimeDirectFields==null && m_objTimeDirectFields.Length <=0)
			{
				MessageBox.Show("无可进行时间趋势的项目");
				return;
			}
//			if(this.lsvParamenters.Items.Count >0)
//				if(this.lsvParamenters.SelectedItems ==null || this.lsvParamenters.SelectedItems.Count <=0)
//				{
//					MessageBox.Show("请选择一个可用的参数");
//					return;
//				}
			if(this.lvFields.CheckedItems.Count <=0)
			{
				
				MessageBox.Show("请至少选择一个时间趋势的项目");
				return;
			}
			string strTableName=m_objTimeDirectTables[intIndex].m_strT_TableName ;
			int intTableType=int.Parse (m_objTimeDirectTables[intIndex].m_strT_TableType) ; 
			string strTableIDField=m_objTimeDirectTables[intIndex].m_strT_TargetIdentityField ;
			clsTimeDirectionFieldValue  [] objFields=new clsTimeDirectionFieldValue[this.lvFields.CheckedItems.Count];
			for(int i=0;i<this.lvFields.CheckedItems.Count ;i++)
			{
				objFields[i]=new clsTimeDirectionFieldValue();
				objFields[i].m_strF_DataFieldName =m_objTimeDirectFields[this.lvFields.CheckedItems[i].Index].m_strF_DataFieldName ;
				objFields[i].m_strF_FieldDesc =m_objTimeDirectFields[this.lvFields.CheckedItems[i].Index].m_strF_FieldDesc ;
				objFields[i].m_strF_ID =m_objTimeDirectFields[this.lvFields.CheckedItems[i].Index].m_strF_ID ;
				objFields[i].m_strT_ID =m_objTimeDirectFields[this.lvFields.CheckedItems[i].Index].m_strT_ID ; 
				objFields[i].m_strF_FieldCondiction =m_objTimeDirectFields[this.lvFields.CheckedItems[i].Index].m_strF_FieldCondiction ;
				objFields[i].m_strF_FieldType =m_objTimeDirectFields[this.lvFields.CheckedItems[i].Index].m_strF_FieldType  ;
			}
			 
			string strDateTimeFieldName=m_objTimeDirectTables[intIndex].m_strT_DateTimeFiledName ;
			string strSpecialCondiction=m_objTimeDirectTables[intIndex].m_strT_SpecialCondition ;
			string[] strSpecialCondications=new string[]{m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss")};

			string strSpecialOrder=m_objTimeDirectTables[intIndex].m_strT_SpecialOrder ;
			clsTDParameterExplainValue[] objTDParameterExplain=null;
			clsTDParameterDetailValue[] objTDParameterDetail=null;
			DateTime dtStartDate=DateTime.MinValue ;
			if(intTableType==0)
				m_objTimeDirectionDomain.lngGenerateTimeDirection (strTableName,objFields,strDateTimeFieldName,
					this.dtpBeginTime.Value.ToString ("yyyy-MM-dd HH:mm:ss"),this.dtpEndTime.Value.ToString ("yyyy-MM-dd HH:mm:ss") ,strSpecialCondiction,strSpecialCondications,strSpecialOrder ,
					out objTDParameterExplain,out objTDParameterDetail,out dtStartDate); 
			else
				m_objTimeDirectionDomain.lngGenerateTimeDirection1 (strTableName, strTableIDField,objFields,strDateTimeFieldName,
					this.dtpBeginTime.Value.ToString ("yyyy-MM-dd HH:mm:ss"),this.dtpEndTime.Value.ToString ("yyyy-MM-dd HH:mm:ss") ,strSpecialCondiction,strSpecialCondications,strSpecialOrder ,
					out objTDParameterExplain,out objTDParameterDetail,out dtStartDate); 


			if(objTDParameterExplain!=null && objTDParameterExplain.Length >0)
			{
				clsVitalGroupSet[] objVitalGroupSet=new clsVitalGroupSet[objTDParameterExplain.Length ];
				for(int i=0;i<objVitalGroupSet.Length ;i++)
				{
					objVitalGroupSet[i]=new clsVitalGroupSet();
					objVitalGroupSet[i].m_intEMFCID =int.Parse (objTDParameterExplain[i].m_strP_ID );
//					objVitalGroupSet[i].m_strParamLabelDesc =objTDParameterExplain[i].m_strP_Desc ;
					objVitalGroupSet[i].m_strParamLabel =objTDParameterExplain[i].m_strP_Desc ;
					if(objTDParameterExplain[i].m_strP_MaxValue==null || objTDParameterExplain[i].m_strP_MaxValue=="")
						objTDParameterExplain[i].m_strP_MaxValue="0";
					if(objTDParameterExplain[i].m_strP_MinValue==null || objTDParameterExplain[i].m_strP_MinValue=="")
						objTDParameterExplain[i].m_strP_MinValue="0";

					objVitalGroupSet[i].m_intMaxScale0 =(int)  float.Parse (objTDParameterExplain[i].m_strP_MaxValue );
					objVitalGroupSet[i].m_intMinScale0 =(int)  float.Parse (objTDParameterExplain[i].m_strP_MinValue );  
					if(objVitalGroupSet[i].m_intMaxScale0 == objVitalGroupSet[i].m_intMinScale0 )
						objVitalGroupSet[i].m_intMaxScale0 =objVitalGroupSet[i].m_intMaxScale0 +1;
					if(this.lvFields.Items.Count >i) 
					{
						objVitalGroupSet[i].m_intMarkerIndex =int.Parse (this.lvFields.Items[i].Tag.ToString ())  ;					
						objVitalGroupSet[i].m_clrColor =this.lvFields.Items[i].SubItems[3].ForeColor;
					}
				}
				this.ctrTimeDirection.m_mthShowParamDesc (objVitalGroupSet); 
			}
			if(objTDParameterDetail !=null && objTDParameterDetail.Length >0)
			{
				clsTrendValue [] objTrend=new clsTrendValue[objTDParameterDetail.Length ];
				for(int i=0;i<objTDParameterDetail.Length ;i++)
				{
					if(i==0) this.ctrTimeDirection.m_DtmStartDate = DateTime.Parse (objTDParameterDetail[i].m_strP_DateTime ); 
					objTrend[i]=new clsTrendValue ();
					objTrend[i].m_intEMFCID =int.Parse (objTDParameterDetail[i].m_strP_ID );
					objTrend[i].m_fltValue =float.Parse ("0" + objTDParameterDetail[i].m_strP_Value );
					objTrend[i].m_dtmStoreDate =DateTime.Parse (objTDParameterDetail[i].m_strP_DateTime ); 
				}
				this.ctrTimeDirection.m_mthShowParamValue (objTrend );
			}
		}

		private void cmdColor_Click(object sender, System.EventArgs e)
		{
			ColorDialog objDialog = new ColorDialog();

			if(objDialog.ShowDialog() == DialogResult.OK)
			{
				this.cmdColor.BackColor = objDialog.Color;
				if(this.lvFields.SelectedItems!=null && this.lvFields.SelectedItems.Count >0 )
				{
					this.lvFields.SelectedItems[0].SubItems[3].ForeColor = objDialog.Color;
				}
			}

		}

		private void cboMarker_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.lvFields.SelectedItems!=null && this.lvFields.SelectedItems.Count >0 )
			{
				this.lvFields.SelectedItems[0].SubItems[2].Text = cboMarker.Text ;
				this.lvFields.SelectedItems[0].Tag =cboMarker.SelectedIndex.ToString ();   
			}
		}

		private void lvFields_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.lvFields.SelectedItems !=null && this.lvFields.SelectedItems.Count >0 )
			{
				this.cmdColor.BackColor = this.lvFields.SelectedItems[0].SubItems[3].ForeColor; 
				this.cboMarker.SelectedIndex =int.Parse (this.lvFields.SelectedItems[0].Tag.ToString () ); 
			}
		}

		private void tabPage2_Click(object sender, System.EventArgs e)
		{
			cboCatory.Focus();
		}

		
		//改变显示模式
		private void m_mthDisplayMode(object sender, System.EventArgs e)
		{
			for(int i=0;i<mniDisplay.MenuItems.Count;i++)
				mniDisplay.MenuItems[i].Checked = false;
			MenuItem mniSender = (MenuItem)sender;
			mniSender.Checked = true;
			switch(mniSender.Text)
			{
				case "离  散":
					ctrTimeDirection.m_EnmDisplay = com.digitalwave.Utility.ctlTrendViewer.enmDisplay.Scatter;
					break;
				case "连  续":
					ctrTimeDirection.m_EnmDisplay = com.digitalwave.Utility.ctlTrendViewer.enmDisplay.Continues;
					break;
				default:
					ctrTimeDirection.m_EnmDisplay = com.digitalwave.Utility.ctlTrendViewer.enmDisplay.Mixed;
					break;
			}
		}

		//改变分辨率模式
		private void m_mthResolutionMode(object sender, System.EventArgs e)
		{
			for(int i=0;i<mniResolution.MenuItems.Count;i++)
				mniResolution.MenuItems[i].Checked = false;			
			MenuItem mniSender = (MenuItem)sender;
			mniSender.Checked = true;
			switch(mniSender.Text)
			{
				case "1   分钟":
					ctrTimeDirection.m_EnmResolution = com.digitalwave.Utility.ctlTrendViewer.enmResolution.One_Minute;
					break;
				case "5   分钟":
					ctrTimeDirection.m_EnmResolution = com.digitalwave.Utility.ctlTrendViewer.enmResolution.Five_Minute;
					break;
				case "30   分钟":
					ctrTimeDirection.m_EnmResolution = com.digitalwave.Utility.ctlTrendViewer.enmResolution.Thirty_Minute;
					break;
				default:
					ctrTimeDirection.m_EnmResolution = com.digitalwave.Utility.ctlTrendViewer.enmResolution.One_Hour;
					break;
			}

			cmdTimeDirection_Click(null,null);
		}

		private void m_cboTimeSection_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			dtpBeginTime.Value = DateTime.Now.AddHours(-(int.Parse(m_cboTimeSection.Text)));
			dtpEndTime.Value = DateTime.Now;
		}

		/// <summary>
		/// 推进多少分钟
		/// </summary>
		/// <param name="p_blnPlus">true为加；false为减</param>
		private void m_mthAddMinutes(bool p_blnPlus)
		{
			try
			{
				int intSpan = int.Parse(m_txtHow.Text);
				if(!p_blnPlus)
					intSpan = -intSpan;
				dtpBeginTime.Value = dtpBeginTime.Value.AddMinutes(intSpan);
				cmdTimeDirection_Click(null,null);
			}
			catch
			{}
		}

		private void m_cmdBack_Click(object sender, System.EventArgs e)
		{
			m_mthAddMinutes(false);
		}

		private void m_cmdForward_Click(object sender, System.EventArgs e)
		{
			m_mthAddMinutes(true);
		}
        ///// <summary>
        ///// 不需要打印前提示保存
        ///// </summary>
        //protected override DialogResult m_dlgHandleSaveBeforePrint()
        //{
        //    return DialogResult.None;
        //}
        ///// <summary>
        ///// 记录设置窗体当前状态，以在窗体关闭时有保存提示。如果不需要保存提示，重载该函数。
        ///// </summary>
        //protected override void m_mthAddFormStatusForClosingSave()
        //{}

	}
}
