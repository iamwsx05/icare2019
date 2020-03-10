using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using weCare.Core.Entity;
using System.IO;
using System.Resources ;
using System.Drawing.Design;

namespace com.digitalwave.Utility.Controls
{
	
	/// <summary>
	/// 画图工具
	/// </summary>
	public class ctlPaintContainer : System.Windows.Forms.UserControl
	{
		#region  Define
		private System.Windows.Forms.ContextMenu ctmNew;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mniHavePic;
		private System.Windows.Forms.MenuItem mniNoPic;
		private System.Windows.Forms.ContextMenu ctmModify;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mniBackPic;
		private System.Windows.Forms.MenuItem mniBackColor;
		private System.ComponentModel.IContainer components;

		
		private bool blnIfPaint = false;
		/// <summary>
		/// 是否画轨迹
		/// </summary>
		private bool m_blnIfPaintPoint = false;
		private int intX1,intX2,intY1,intY2,intXDefault,intYDefault;
		private enum enmPictureBoxPaintType
		{
			
			Pen = 0,
			Line,
			Round,
			SolidRound,
			Rectangle,
			SolidRect,
			Polygon,
			Rubber,
			Text,
			DashPen,
			DashRound,
			DashRect,
			None
		}
		private enum enmContainerPaintType
		{
			None,
			DrawBorder,
		}

		private enum enmFillImageType
		{
			触觉减退  = 12,
			触觉消失,
			触觉过敏或异样,
			痛觉减退,
			痛觉消失,
			感觉过敏或异样,
			震动觉减退或消失,
			位置觉减退或消失,
			浅感觉全部消失,
			深浅感觉全部消失,
			I度
		}


		private enmContainerPaintType m_enmContainerPaintType = enmContainerPaintType.None;
		/// <summary>
		/// 画图类型
		/// </summary>
		private enmPictureBoxPaintType m_enmPicPaintType = enmPictureBoxPaintType.Pen;
		private ArrayList m_arlPic = new ArrayList();
		private Image m_imgDrawRectangle;
		private Image m_imgDrawRound;
		private Image m_imgDrawLine;
		private Image m_imgDrawSolidRound;
		private Image m_imgDrawSolidRectangle;
		private Image m_imgDrawDashPen;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem mniDeleteModify;
		private System.Windows.Forms.MenuItem mniDelete;
		private System.Windows.Forms.MenuItem mniPicSize;
		private System.Windows.Forms.PropertyGrid ppgPicSize;
		private System.Windows.Forms.MenuItem mniPicScale;
		private Pen pn = new Pen(Color.Red);
		public clsImageInfo[] m_objImageInfoArr;
		private System.Windows.Forms.TextBox txtScale;
		private System.Windows.Forms.GroupBox gpbTools;
		public clsPictureBoxValue[] m_objPicInfoArr;
		private System.Windows.Forms.RichTextBox m_txtText;
		private int m_intDefaultWidth;
		private int m_intDefaultHeight;

		private System.Windows.Forms.RadioButton m_rdbPen;
		private System.Windows.Forms.RadioButton m_rdbLine;
		private System.Windows.Forms.RadioButton m_rdbRubber;
		private System.Windows.Forms.RadioButton m_rdbText;
		private System.Windows.Forms.Button m_cmdColor;
		private System.Windows.Forms.ImageList imageIcon;
		private System.Windows.Forms.ContextMenu ctmTools;
		private System.Windows.Forms.ContextMenu ctmFillTool;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox m_gpbEdit;
		private System.Windows.Forms.MenuItem m_mniLine;
		private System.Windows.Forms.MenuItem m_mniRound;
		private System.Windows.Forms.MenuItem m_mniSolidRound;
		private System.Windows.Forms.MenuItem m_mniRectang;
		private System.Windows.Forms.MenuItem m_mniSolidRect;
		private System.Windows.Forms.MenuItem m_mniPolygon;
		private System.Windows.Forms.MenuItem m_mniAnalgesia;
		private System.Windows.Forms.MenuItem m_mniHypopse;
		private System.Windows.Forms.MenuItem m_mniHapticDis;
		private System.Windows.Forms.MenuItem m_mniHyperaphia;
		private System.Windows.Forms.MenuItem m_mniHypalgesia;
		private System.Windows.Forms.MenuItem m_mniAlgesia;
		private System.Windows.Forms.MenuItem m_mniConcussive;
		private System.Windows.Forms.MenuItem m_mniDislocation;
		private System.Windows.Forms.MenuItem m_mniFleetAlgesiaDis;
		private System.Windows.Forms.MenuItem m_mniDeepAlgesiaDis;
		private System.Windows.Forms.RadioButton m_rdbDashed;
		private System.Windows.Forms.RadioButton m_rdbFill;
		private System.Windows.Forms.Button m_cmdSelected;
		private System.Windows.Forms.Panel m_pnlEdit;
		private System.Windows.Forms.ContextMenu ctmSelect;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		
		private Region m_rgnContent;
		private TextureBrush m_txbContent;
		private Image m_imgFillImage;
		private Pen m_penDash;
		private GraphicsPath m_ghpSelectPath;
		/// <summary>
		/// 是否可以改变底图大小
		/// </summary>
//		private bool m_blnCanResizeImage = true;
        private System.Windows.Forms.GroupBox m_gpbMain;
		/// <summary>
		/// 当前选中的图片框
		/// </summary>
		private PictureBox m_picSelected = null;
		private System.Windows.Forms.MenuItem mni8thSysImage;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem mniOtolaryngology;
		private System.Windows.Forms.MenuItem mniNeurosurgery;
		private System.Windows.Forms.MenuItem mniBurnSurgery;
		private System.Windows.Forms.MenuItem mniOphthalmic;
		private System.Windows.Forms.MenuItem mniUrology;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem21;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.Windows.Forms.MenuItem menuItem23;
		private System.Windows.Forms.MenuItem menuItem24;
		private System.Windows.Forms.MenuItem menuItem25;
		private System.Windows.Forms.MenuItem menuItem26;
		private System.Windows.Forms.MenuItem menuItem27;
		private System.Windows.Forms.MenuItem menuItem28;
		private System.Windows.Forms.MenuItem menuItem29;
		private System.Windows.Forms.MenuItem menuItem30;
		private System.Windows.Forms.MenuItem menuItem31;
		private System.Windows.Forms.MenuItem menuItem32;
		private System.Windows.Forms.MenuItem menuItem33;
		private System.Windows.Forms.MenuItem menuItem34;
		private System.Windows.Forms.MenuItem menuItem35;
		private System.Windows.Forms.MenuItem menuItem36;
		private System.Windows.Forms.MenuItem menuItem37;
		private System.Windows.Forms.MenuItem menuItem38;
		private System.Windows.Forms.MenuItem menuItem39;
		private System.Windows.Forms.MenuItem menuItem40;
		private System.Windows.Forms.MenuItem menuItem41;
		private System.Windows.Forms.MenuItem menuItem42;
		private System.Windows.Forms.MenuItem menuItem43;
		private System.Windows.Forms.MenuItem menuItem44;
		private Chris.Beckett.MenuImageLib.MenuImage SetMenuImage;
		private System.Windows.Forms.CheckBox m_chkCanSizePic;
		private System.Windows.Forms.MenuItem mniBreath;
		private System.Windows.Forms.MenuItem menuItem46;
        private MenuItem menuItem45;
        private MenuItem menuItem47;
        private MenuItem menuItem48;
        private MenuItem menuItem49;

		private string m_strPicPath = Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()) + "\\picture\\PaintTool\\";

		#endregion Define

		/// <summary>
		/// 
		/// </summary>
		public ctlPaintContainer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.Load += new EventHandler(ctlPaintContainer_Load);

			m_mthInit();
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
			#region Dispose Draw Resouse
			if(m_ghpSelectPath != null)
			{
				m_ghpSelectPath.Dispose();
				m_ghpSelectPath = null;
			}
			if(m_rgnContent != null)
			{
				m_rgnContent.Dispose();
				m_rgnContent = null;
			}
			if(m_txbContent != null)
			{
				m_txbContent.Dispose();
				m_txbContent = null;
			}
			if(m_penDash != null)
				m_penDash.Dispose();
			if(m_imgFillImage != null)
				m_imgFillImage.Dispose();
			if(pn != null)
				pn.Dispose();
			if(m_imgDrawDashPen != null)
			{
				m_imgDrawDashPen.Dispose();
				m_imgDrawDashPen = null;
			}
			if(m_imgDrawLine != null)
			{
				m_imgDrawLine.Dispose();
				m_imgDrawLine = null;
			}
			if(m_imgDrawRectangle != null)
			{
				m_imgDrawRectangle.Dispose();
				m_imgDrawRectangle = null;
			}
			if(m_imgDrawRound != null)
			{
				m_imgDrawRound.Dispose();
				m_imgDrawRound = null;
			}
			if(m_imgDrawSolidRectangle != null)
			{
				m_imgDrawSolidRectangle.Dispose();
				m_imgDrawSolidRectangle = null;
			}
			if(m_imgDrawSolidRound != null)
			{
				m_imgDrawSolidRound.Dispose();
				m_imgDrawSolidRound = null;
			}
			#endregion

			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlPaintContainer));
            this.ctmNew = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mniHavePic = new System.Windows.Forms.MenuItem();
            this.mniNoPic = new System.Windows.Forms.MenuItem();
            this.mni8thSysImage = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem32 = new System.Windows.Forms.MenuItem();
            this.menuItem33 = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuItem35 = new System.Windows.Forms.MenuItem();
            this.menuItem36 = new System.Windows.Forms.MenuItem();
            this.menuItem37 = new System.Windows.Forms.MenuItem();
            this.menuItem38 = new System.Windows.Forms.MenuItem();
            this.menuItem39 = new System.Windows.Forms.MenuItem();
            this.mniOtolaryngology = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.mniNeurosurgery = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.mniBurnSurgery = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.mniOphthalmic = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.mniUrology = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.mniBreath = new System.Windows.Forms.MenuItem();
            this.menuItem46 = new System.Windows.Forms.MenuItem();
            this.menuItem45 = new System.Windows.Forms.MenuItem();
            this.menuItem47 = new System.Windows.Forms.MenuItem();
            this.menuItem48 = new System.Windows.Forms.MenuItem();
            this.ctmModify = new System.Windows.Forms.ContextMenu();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mniBackPic = new System.Windows.Forms.MenuItem();
            this.mniBackColor = new System.Windows.Forms.MenuItem();
            this.mniPicSize = new System.Windows.Forms.MenuItem();
            this.mniPicScale = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mniDeleteModify = new System.Windows.Forms.MenuItem();
            this.mniDelete = new System.Windows.Forms.MenuItem();
            this.gpbTools = new System.Windows.Forms.GroupBox();
            this.ppgPicSize = new System.Windows.Forms.PropertyGrid();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.m_rdbPen = new System.Windows.Forms.RadioButton();
            this.m_rdbLine = new System.Windows.Forms.RadioButton();
            this.m_rdbRubber = new System.Windows.Forms.RadioButton();
            this.m_rdbText = new System.Windows.Forms.RadioButton();
            this.m_cmdColor = new System.Windows.Forms.Button();
            this.m_rdbFill = new System.Windows.Forms.RadioButton();
            this.m_rdbDashed = new System.Windows.Forms.RadioButton();
            this.m_txtText = new System.Windows.Forms.RichTextBox();
            this.imageIcon = new System.Windows.Forms.ImageList(this.components);
            this.m_mniLine = new System.Windows.Forms.MenuItem();
            this.m_mniRound = new System.Windows.Forms.MenuItem();
            this.m_mniSolidRound = new System.Windows.Forms.MenuItem();
            this.m_mniRectang = new System.Windows.Forms.MenuItem();
            this.m_mniSolidRect = new System.Windows.Forms.MenuItem();
            this.m_mniPolygon = new System.Windows.Forms.MenuItem();
            this.m_mniAnalgesia = new System.Windows.Forms.MenuItem();
            this.m_mniHypopse = new System.Windows.Forms.MenuItem();
            this.m_mniHapticDis = new System.Windows.Forms.MenuItem();
            this.m_mniHyperaphia = new System.Windows.Forms.MenuItem();
            this.m_mniHypalgesia = new System.Windows.Forms.MenuItem();
            this.m_mniAlgesia = new System.Windows.Forms.MenuItem();
            this.m_mniConcussive = new System.Windows.Forms.MenuItem();
            this.m_mniDislocation = new System.Windows.Forms.MenuItem();
            this.m_mniFleetAlgesiaDis = new System.Windows.Forms.MenuItem();
            this.m_mniDeepAlgesiaDis = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem40 = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.menuItem42 = new System.Windows.Forms.MenuItem();
            this.menuItem43 = new System.Windows.Forms.MenuItem();
            this.menuItem44 = new System.Windows.Forms.MenuItem();
            this.ctmTools = new System.Windows.Forms.ContextMenu();
            this.ctmFillTool = new System.Windows.Forms.ContextMenu();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_cmdSelected = new System.Windows.Forms.Button();
            this.m_gpbEdit = new System.Windows.Forms.GroupBox();
            this.m_pnlEdit = new System.Windows.Forms.Panel();
            this.ctmSelect = new System.Windows.Forms.ContextMenu();
            this.m_gpbMain = new System.Windows.Forms.GroupBox();
            this.m_chkCanSizePic = new System.Windows.Forms.CheckBox();
            this.SetMenuImage = new Chris.Beckett.MenuImageLib.MenuImage(this.components);
            this.menuItem49 = new System.Windows.Forms.MenuItem();
            this.gpbTools.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.m_gpbEdit.SuspendLayout();
            this.m_gpbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctmNew
            // 
            this.ctmNew.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.SetMenuImage.SetMenuImage(this.menuItem1, null);
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniHavePic,
            this.mniNoPic,
            this.mni8thSysImage,
            this.mniOtolaryngology,
            this.mniNeurosurgery,
            this.mniBurnSurgery,
            this.mniOphthalmic,
            this.mniUrology,
            this.mniBreath,
            this.menuItem45});
            this.menuItem1.OwnerDraw = true;
            this.menuItem1.Text = "新建";
            // 
            // mniHavePic
            // 
            this.mniHavePic.Index = 0;
            this.SetMenuImage.SetMenuImage(this.mniHavePic, null);
            this.mniHavePic.OwnerDraw = true;
            this.mniHavePic.Text = "从文件打开...";
            this.mniHavePic.Click += new System.EventHandler(this.mniHavePic_Click);
            // 
            // mniNoPic
            // 
            this.mniNoPic.Index = 1;
            this.SetMenuImage.SetMenuImage(this.mniNoPic, null);
            this.mniNoPic.OwnerDraw = true;
            this.mniNoPic.Text = "白色底图";
            this.mniNoPic.Click += new System.EventHandler(this.mniNoPic_Click);
            // 
            // mni8thSysImage
            // 
            this.mni8thSysImage.Index = 2;
            this.SetMenuImage.SetMenuImage(this.mni8thSysImage, null);
            this.mni8thSysImage.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.menuItem32,
            this.menuItem33,
            this.menuItem34,
            this.menuItem35,
            this.menuItem36,
            this.menuItem37,
            this.menuItem38,
            this.menuItem39});
            this.mni8thSysImage.OwnerDraw = true;
            this.mni8thSysImage.Text = "八大系统图";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.SetMenuImage.SetMenuImage(this.menuItem7, null);
            this.menuItem7.OwnerDraw = true;
            this.menuItem7.Text = "骨骼系统";
            // 
            // menuItem32
            // 
            this.menuItem32.Index = 1;
            this.SetMenuImage.SetMenuImage(this.menuItem32, null);
            this.menuItem32.OwnerDraw = true;
            this.menuItem32.Text = "呼吸系统";
            // 
            // menuItem33
            // 
            this.menuItem33.Index = 2;
            this.SetMenuImage.SetMenuImage(this.menuItem33, null);
            this.menuItem33.OwnerDraw = true;
            this.menuItem33.Text = "淋巴系统";
            // 
            // menuItem34
            // 
            this.menuItem34.Index = 3;
            this.SetMenuImage.SetMenuImage(this.menuItem34, null);
            this.menuItem34.OwnerDraw = true;
            this.menuItem34.Text = "泌尿系统";
            // 
            // menuItem35
            // 
            this.menuItem35.Index = 4;
            this.SetMenuImage.SetMenuImage(this.menuItem35, null);
            this.menuItem35.OwnerDraw = true;
            this.menuItem35.Text = "内分泌系统";
            // 
            // menuItem36
            // 
            this.menuItem36.Index = 5;
            this.SetMenuImage.SetMenuImage(this.menuItem36, null);
            this.menuItem36.OwnerDraw = true;
            this.menuItem36.Text = "神经系统";
            // 
            // menuItem37
            // 
            this.menuItem37.Index = 6;
            this.SetMenuImage.SetMenuImage(this.menuItem37, null);
            this.menuItem37.OwnerDraw = true;
            this.menuItem37.Text = "生殖系统";
            // 
            // menuItem38
            // 
            this.menuItem38.Index = 7;
            this.SetMenuImage.SetMenuImage(this.menuItem38, null);
            this.menuItem38.OwnerDraw = true;
            this.menuItem38.Text = "消化系统";
            // 
            // menuItem39
            // 
            this.menuItem39.Index = 8;
            this.SetMenuImage.SetMenuImage(this.menuItem39, null);
            this.menuItem39.OwnerDraw = true;
            this.menuItem39.Text = "心血管系统";
            // 
            // mniOtolaryngology
            // 
            this.mniOtolaryngology.Index = 3;
            this.SetMenuImage.SetMenuImage(this.mniOtolaryngology, null);
            this.mniOtolaryngology.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem8,
            this.menuItem9,
            this.menuItem10,
            this.menuItem11,
            this.menuItem12,
            this.menuItem13,
            this.menuItem14,
            this.menuItem15});
            this.mniOtolaryngology.OwnerDraw = true;
            this.mniOtolaryngology.Text = "耳鼻喉科";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 0;
            this.SetMenuImage.SetMenuImage(this.menuItem8, null);
            this.menuItem8.OwnerDraw = true;
            this.menuItem8.Text = "鼻部";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.SetMenuImage.SetMenuImage(this.menuItem9, null);
            this.menuItem9.OwnerDraw = true;
            this.menuItem9.Text = "鼻咽部";
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 2;
            this.SetMenuImage.SetMenuImage(this.menuItem10, null);
            this.menuItem10.OwnerDraw = true;
            this.menuItem10.Text = "喉部";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 3;
            this.SetMenuImage.SetMenuImage(this.menuItem11, null);
            this.menuItem11.OwnerDraw = true;
            this.menuItem11.Text = "口咽部";
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 4;
            this.SetMenuImage.SetMenuImage(this.menuItem12, null);
            this.menuItem12.OwnerDraw = true;
            this.menuItem12.Text = "右耳";
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 5;
            this.SetMenuImage.SetMenuImage(this.menuItem13, null);
            this.menuItem13.OwnerDraw = true;
            this.menuItem13.Text = "右耳廓";
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 6;
            this.SetMenuImage.SetMenuImage(this.menuItem14, null);
            this.menuItem14.OwnerDraw = true;
            this.menuItem14.Text = "左耳";
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 7;
            this.SetMenuImage.SetMenuImage(this.menuItem15, null);
            this.menuItem15.OwnerDraw = true;
            this.menuItem15.Text = "左耳廓";
            // 
            // mniNeurosurgery
            // 
            this.mniNeurosurgery.Index = 4;
            this.SetMenuImage.SetMenuImage(this.mniNeurosurgery, null);
            this.mniNeurosurgery.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem16});
            this.mniNeurosurgery.OwnerDraw = true;
            this.mniNeurosurgery.Text = "神经外科";
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 0;
            this.SetMenuImage.SetMenuImage(this.menuItem16, null);
            this.menuItem16.OwnerDraw = true;
            this.menuItem16.Text = "神经外科感觉检查图片";
            // 
            // mniBurnSurgery
            // 
            this.mniBurnSurgery.Index = 5;
            this.SetMenuImage.SetMenuImage(this.mniBurnSurgery, null);
            this.mniBurnSurgery.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem17,
            this.menuItem18,
            this.menuItem19,
            this.menuItem20,
            this.menuItem21,
            this.menuItem22,
            this.menuItem23,
            this.menuItem24,
            this.menuItem25});
            this.mniBurnSurgery.OwnerDraw = true;
            this.mniBurnSurgery.Text = "烧伤外科";
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 0;
            this.SetMenuImage.SetMenuImage(this.menuItem17, null);
            this.menuItem17.OwnerDraw = true;
            this.menuItem17.Text = "全部烧伤图";
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 1;
            this.SetMenuImage.SetMenuImage(this.menuItem18, null);
            this.menuItem18.OwnerDraw = true;
            this.menuItem18.Text = "人体背面";
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 2;
            this.SetMenuImage.SetMenuImage(this.menuItem19, null);
            this.menuItem19.OwnerDraw = true;
            this.menuItem19.Text = "人体正面";
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 3;
            this.SetMenuImage.SetMenuImage(this.menuItem20, null);
            this.menuItem20.OwnerDraw = true;
            this.menuItem20.Text = "人头右侧";
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 4;
            this.SetMenuImage.SetMenuImage(this.menuItem21, null);
            this.menuItem21.OwnerDraw = true;
            this.menuItem21.Text = "人头左侧";
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 5;
            this.SetMenuImage.SetMenuImage(this.menuItem22, null);
            this.menuItem22.OwnerDraw = true;
            this.menuItem22.Text = "右手背面";
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 6;
            this.SetMenuImage.SetMenuImage(this.menuItem23, null);
            this.menuItem23.OwnerDraw = true;
            this.menuItem23.Text = "右手正面";
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 7;
            this.SetMenuImage.SetMenuImage(this.menuItem24, null);
            this.menuItem24.OwnerDraw = true;
            this.menuItem24.Text = "左上背面";
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 8;
            this.SetMenuImage.SetMenuImage(this.menuItem25, null);
            this.menuItem25.OwnerDraw = true;
            this.menuItem25.Text = "左手正面";
            // 
            // mniOphthalmic
            // 
            this.mniOphthalmic.Index = 6;
            this.SetMenuImage.SetMenuImage(this.mniOphthalmic, null);
            this.mniOphthalmic.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem26,
            this.menuItem27,
            this.menuItem28,
            this.menuItem29,
            this.menuItem49});
            this.mniOphthalmic.OwnerDraw = true;
            this.mniOphthalmic.Text = "眼科";
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 0;
            this.SetMenuImage.SetMenuImage(this.menuItem26, null);
            this.menuItem26.OwnerDraw = true;
            this.menuItem26.Text = "晶状体";
            // 
            // menuItem27
            // 
            this.menuItem27.Index = 1;
            this.SetMenuImage.SetMenuImage(this.menuItem27, null);
            this.menuItem27.OwnerDraw = true;
            this.menuItem27.Text = "瞳孔";
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 2;
            this.SetMenuImage.SetMenuImage(this.menuItem28, null);
            this.menuItem28.OwnerDraw = true;
            this.menuItem28.Text = "右视网膜";
            // 
            // menuItem29
            // 
            this.menuItem29.Index = 3;
            this.SetMenuImage.SetMenuImage(this.menuItem29, null);
            this.menuItem29.OwnerDraw = true;
            this.menuItem29.Text = "左视网膜";
            // 
            // mniUrology
            // 
            this.mniUrology.Index = 7;
            this.SetMenuImage.SetMenuImage(this.mniUrology, null);
            this.mniUrology.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem30,
            this.menuItem31});
            this.mniUrology.OwnerDraw = true;
            this.mniUrology.Text = "泌尿外科";
            // 
            // menuItem30
            // 
            this.menuItem30.Index = 0;
            this.SetMenuImage.SetMenuImage(this.menuItem30, null);
            this.menuItem30.OwnerDraw = true;
            this.menuItem30.Text = "尿路图";
            // 
            // menuItem31
            // 
            this.menuItem31.Index = 1;
            this.SetMenuImage.SetMenuImage(this.menuItem31, null);
            this.menuItem31.OwnerDraw = true;
            this.menuItem31.Text = "性腺图";
            // 
            // mniBreath
            // 
            this.mniBreath.Index = 8;
            this.SetMenuImage.SetMenuImage(this.mniBreath, null);
            this.mniBreath.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem46});
            this.mniBreath.OwnerDraw = true;
            this.mniBreath.Text = "呼吸内科";
            // 
            // menuItem46
            // 
            this.menuItem46.Index = 0;
            this.SetMenuImage.SetMenuImage(this.menuItem46, null);
            this.menuItem46.OwnerDraw = true;
            this.menuItem46.Text = "胸部X线征";
            // 
            // menuItem45
            // 
            this.menuItem45.Index = 9;
            this.SetMenuImage.SetMenuImage(this.menuItem45, null);
            this.menuItem45.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem47,
            this.menuItem48});
            this.menuItem45.OwnerDraw = true;
            this.menuItem45.Text = "口腔科";
            // 
            // menuItem47
            // 
            this.menuItem47.Index = 0;
            this.SetMenuImage.SetMenuImage(this.menuItem47, null);
            this.menuItem47.OwnerDraw = true;
            this.menuItem47.Text = "上颌骨1";
            // 
            // menuItem48
            // 
            this.menuItem48.Index = 1;
            this.SetMenuImage.SetMenuImage(this.menuItem48, null);
            this.menuItem48.OwnerDraw = true;
            this.menuItem48.Text = "上颌骨2";
            // 
            // ctmModify
            // 
            this.ctmModify.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3});
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.SetMenuImage.SetMenuImage(this.menuItem2, null);
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniBackPic,
            this.mniBackColor,
            this.mniPicSize,
            this.mniPicScale});
            this.menuItem2.OwnerDraw = true;
            this.menuItem2.Text = "修改";
            // 
            // mniBackPic
            // 
            this.mniBackPic.Index = 0;
            this.SetMenuImage.SetMenuImage(this.mniBackPic, null);
            this.mniBackPic.OwnerDraw = true;
            this.mniBackPic.Text = "底图";
            this.mniBackPic.Click += new System.EventHandler(this.mniBackPic_Click);
            // 
            // mniBackColor
            // 
            this.mniBackColor.Index = 1;
            this.SetMenuImage.SetMenuImage(this.mniBackColor, null);
            this.mniBackColor.OwnerDraw = true;
            this.mniBackColor.Text = "背景色";
            this.mniBackColor.Click += new System.EventHandler(this.mniBackColor_Click);
            // 
            // mniPicSize
            // 
            this.mniPicSize.Index = 2;
            this.SetMenuImage.SetMenuImage(this.mniPicSize, null);
            this.mniPicSize.OwnerDraw = true;
            this.mniPicSize.Text = "图片大小";
            this.mniPicSize.Visible = false;
            this.mniPicSize.Click += new System.EventHandler(this.mniPicSize_Click);
            // 
            // mniPicScale
            // 
            this.mniPicScale.Index = 3;
            this.SetMenuImage.SetMenuImage(this.mniPicScale, null);
            this.mniPicScale.OwnerDraw = true;
            this.mniPicScale.Text = "图片比例";
            this.mniPicScale.Visible = false;
            this.mniPicScale.Click += new System.EventHandler(this.mniPicScale_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.SetMenuImage.SetMenuImage(this.menuItem3, null);
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniDeleteModify,
            this.mniDelete});
            this.menuItem3.OwnerDraw = true;
            this.menuItem3.Text = "删除";
            // 
            // mniDeleteModify
            // 
            this.mniDeleteModify.Index = 0;
            this.SetMenuImage.SetMenuImage(this.mniDeleteModify, null);
            this.mniDeleteModify.OwnerDraw = true;
            this.mniDeleteModify.Text = "所有修改";
            this.mniDeleteModify.Click += new System.EventHandler(this.mniDeleteModify_Click);
            // 
            // mniDelete
            // 
            this.mniDelete.Index = 1;
            this.SetMenuImage.SetMenuImage(this.mniDelete, null);
            this.mniDelete.OwnerDraw = true;
            this.mniDelete.Text = "整幅图片";
            this.mniDelete.Click += new System.EventHandler(this.mniDelete_Click);
            // 
            // gpbTools
            // 
            this.gpbTools.BackColor = System.Drawing.SystemColors.Control;
            this.gpbTools.Controls.Add(this.ppgPicSize);
            this.gpbTools.Controls.Add(this.txtScale);
            this.gpbTools.Controls.Add(this.m_rdbPen);
            this.gpbTools.Controls.Add(this.m_rdbLine);
            this.gpbTools.Controls.Add(this.m_rdbRubber);
            this.gpbTools.Controls.Add(this.m_rdbText);
            this.gpbTools.Controls.Add(this.m_cmdColor);
            this.gpbTools.Controls.Add(this.m_rdbFill);
            this.gpbTools.Controls.Add(this.m_rdbDashed);
            this.gpbTools.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gpbTools.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gpbTools.Location = new System.Drawing.Point(8, 20);
            this.gpbTools.Name = "gpbTools";
            this.gpbTools.Size = new System.Drawing.Size(185, 46);
            this.gpbTools.TabIndex = 8;
            this.gpbTools.TabStop = false;
            // 
            // ppgPicSize
            // 
            this.ppgPicSize.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.ppgPicSize.CommandsForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ppgPicSize.HelpVisible = false;
            this.ppgPicSize.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.ppgPicSize.Location = new System.Drawing.Point(304, 16);
            this.ppgPicSize.Name = "ppgPicSize";
            this.ppgPicSize.Size = new System.Drawing.Size(8, 22);
            this.ppgPicSize.TabIndex = 9;
            this.ppgPicSize.ToolbarVisible = false;
            this.ppgPicSize.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.ppgPicSize.ViewForeColor = System.Drawing.SystemColors.Window;
            this.ppgPicSize.Visible = false;
            this.ppgPicSize.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.ppgPicSize_PropertyValueChanged);
            // 
            // txtScale
            // 
            this.txtScale.Location = new System.Drawing.Point(312, 16);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(4, 26);
            this.txtScale.TabIndex = 9;
            this.txtScale.Visible = false;
            this.txtScale.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScale_KeyDown);
            // 
            // m_rdbPen
            // 
            this.m_rdbPen.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdbPen.BackColor = System.Drawing.Color.White;
            this.m_rdbPen.Checked = true;
            this.m_rdbPen.Image = ((System.Drawing.Image)(resources.GetObject("m_rdbPen.Image")));
            this.m_rdbPen.Location = new System.Drawing.Point(8, 14);
            this.m_rdbPen.Name = "m_rdbPen";
            this.m_rdbPen.Size = new System.Drawing.Size(24, 24);
            this.m_rdbPen.TabIndex = 0;
            this.m_rdbPen.TabStop = true;
            this.m_rdbPen.UseVisualStyleBackColor = false;
            this.m_rdbPen.Click += new System.EventHandler(this.m_rdbPen_Click);
            // 
            // m_rdbLine
            // 
            this.m_rdbLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdbLine.BackColor = System.Drawing.Color.Silver;
            this.m_rdbLine.Image = ((System.Drawing.Image)(resources.GetObject("m_rdbLine.Image")));
            this.m_rdbLine.Location = new System.Drawing.Point(32, 14);
            this.m_rdbLine.Name = "m_rdbLine";
            this.m_rdbLine.Size = new System.Drawing.Size(24, 24);
            this.m_rdbLine.TabIndex = 0;
            this.m_rdbLine.UseVisualStyleBackColor = false;
            this.m_rdbLine.Click += new System.EventHandler(this.m_rdbLine_Click);
            // 
            // m_rdbRubber
            // 
            this.m_rdbRubber.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdbRubber.BackColor = System.Drawing.Color.Silver;
            this.m_rdbRubber.Image = ((System.Drawing.Image)(resources.GetObject("m_rdbRubber.Image")));
            this.m_rdbRubber.Location = new System.Drawing.Point(80, 14);
            this.m_rdbRubber.Name = "m_rdbRubber";
            this.m_rdbRubber.Size = new System.Drawing.Size(24, 24);
            this.m_rdbRubber.TabIndex = 0;
            this.m_rdbRubber.UseVisualStyleBackColor = false;
            this.m_rdbRubber.Click += new System.EventHandler(this.m_rdbRubber_Click);
            // 
            // m_rdbText
            // 
            this.m_rdbText.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdbText.BackColor = System.Drawing.Color.Silver;
            this.m_rdbText.Image = ((System.Drawing.Image)(resources.GetObject("m_rdbText.Image")));
            this.m_rdbText.Location = new System.Drawing.Point(56, 14);
            this.m_rdbText.Name = "m_rdbText";
            this.m_rdbText.Size = new System.Drawing.Size(24, 24);
            this.m_rdbText.TabIndex = 0;
            this.m_rdbText.UseVisualStyleBackColor = false;
            this.m_rdbText.Click += new System.EventHandler(this.m_rdbText_Click);
            // 
            // m_cmdColor
            // 
            this.m_cmdColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.m_cmdColor.ForeColor = System.Drawing.Color.White;
            this.m_cmdColor.Location = new System.Drawing.Point(152, 14);
            this.m_cmdColor.Name = "m_cmdColor";
            this.m_cmdColor.Size = new System.Drawing.Size(24, 24);
            this.m_cmdColor.TabIndex = 6;
            this.m_cmdColor.UseVisualStyleBackColor = false;
            this.m_cmdColor.Click += new System.EventHandler(this.m_cmdColor_Click);
            // 
            // m_rdbFill
            // 
            this.m_rdbFill.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdbFill.BackColor = System.Drawing.Color.Silver;
            this.m_rdbFill.Image = ((System.Drawing.Image)(resources.GetObject("m_rdbFill.Image")));
            this.m_rdbFill.Location = new System.Drawing.Point(128, 14);
            this.m_rdbFill.Name = "m_rdbFill";
            this.m_rdbFill.Size = new System.Drawing.Size(24, 24);
            this.m_rdbFill.TabIndex = 0;
            this.m_rdbFill.UseVisualStyleBackColor = false;
            this.m_rdbFill.Click += new System.EventHandler(this.m_rdbFill_Click);
            // 
            // m_rdbDashed
            // 
            this.m_rdbDashed.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdbDashed.BackColor = System.Drawing.Color.Silver;
            this.m_rdbDashed.Image = ((System.Drawing.Image)(resources.GetObject("m_rdbDashed.Image")));
            this.m_rdbDashed.Location = new System.Drawing.Point(104, 14);
            this.m_rdbDashed.Name = "m_rdbDashed";
            this.m_rdbDashed.Size = new System.Drawing.Size(24, 24);
            this.m_rdbDashed.TabIndex = 0;
            this.m_rdbDashed.UseVisualStyleBackColor = false;
            this.m_rdbDashed.Click += new System.EventHandler(this.m_rdbDashed_Click);
            // 
            // m_txtText
            // 
            this.m_txtText.BackColor = System.Drawing.Color.White;
            this.m_txtText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtText.Location = new System.Drawing.Point(444, 28);
            this.m_txtText.Name = "m_txtText";
            this.m_txtText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.m_txtText.Size = new System.Drawing.Size(56, 21);
            this.m_txtText.TabIndex = 9;
            this.m_txtText.Text = "";
            this.m_txtText.Visible = false;
            // 
            // imageIcon
            // 
            this.imageIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageIcon.ImageStream")));
            this.imageIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imageIcon.Images.SetKeyName(0, "");
            this.imageIcon.Images.SetKeyName(1, "");
            this.imageIcon.Images.SetKeyName(2, "");
            this.imageIcon.Images.SetKeyName(3, "");
            this.imageIcon.Images.SetKeyName(4, "");
            this.imageIcon.Images.SetKeyName(5, "");
            this.imageIcon.Images.SetKeyName(6, "");
            this.imageIcon.Images.SetKeyName(7, "");
            this.imageIcon.Images.SetKeyName(8, "");
            this.imageIcon.Images.SetKeyName(9, "");
            this.imageIcon.Images.SetKeyName(10, "");
            this.imageIcon.Images.SetKeyName(11, "");
            this.imageIcon.Images.SetKeyName(12, "");
            this.imageIcon.Images.SetKeyName(13, "");
            this.imageIcon.Images.SetKeyName(14, "");
            this.imageIcon.Images.SetKeyName(15, "");
            this.imageIcon.Images.SetKeyName(16, "");
            this.imageIcon.Images.SetKeyName(17, "");
            this.imageIcon.Images.SetKeyName(18, "");
            this.imageIcon.Images.SetKeyName(19, "");
            this.imageIcon.Images.SetKeyName(20, "");
            this.imageIcon.Images.SetKeyName(21, "");
            this.imageIcon.Images.SetKeyName(22, "");
            // 
            // m_mniLine
            // 
            this.m_mniLine.Index = 0;
            this.SetMenuImage.SetMenuImage(this.m_mniLine, "1");
            this.m_mniLine.OwnerDraw = true;
            this.m_mniLine.Text = "直线工具";
            // 
            // m_mniRound
            // 
            this.m_mniRound.Index = 1;
            this.SetMenuImage.SetMenuImage(this.m_mniRound, "2");
            this.m_mniRound.OwnerDraw = true;
            this.m_mniRound.Text = "圆形工具";
            // 
            // m_mniSolidRound
            // 
            this.m_mniSolidRound.Index = 2;
            this.SetMenuImage.SetMenuImage(this.m_mniSolidRound, "3");
            this.m_mniSolidRound.OwnerDraw = true;
            this.m_mniSolidRound.Text = "实心圆工具";
            // 
            // m_mniRectang
            // 
            this.m_mniRectang.Index = 3;
            this.SetMenuImage.SetMenuImage(this.m_mniRectang, "4");
            this.m_mniRectang.OwnerDraw = true;
            this.m_mniRectang.Text = "矩形工具";
            // 
            // m_mniSolidRect
            // 
            this.m_mniSolidRect.Index = 4;
            this.SetMenuImage.SetMenuImage(this.m_mniSolidRect, "5");
            this.m_mniSolidRect.OwnerDraw = true;
            this.m_mniSolidRect.Text = "实心矩形工具";
            // 
            // m_mniPolygon
            // 
            this.m_mniPolygon.Index = 5;
            this.SetMenuImage.SetMenuImage(this.m_mniPolygon, "6");
            this.m_mniPolygon.OwnerDraw = true;
            this.m_mniPolygon.Text = "多边形工具";
            // 
            // m_mniAnalgesia
            // 
            this.m_mniAnalgesia.Index = 4;
            this.SetMenuImage.SetMenuImage(this.m_mniAnalgesia, "16");
            this.m_mniAnalgesia.OwnerDraw = true;
            this.m_mniAnalgesia.Text = "痛觉消失";
            // 
            // m_mniHypopse
            // 
            this.m_mniHypopse.DefaultItem = true;
            this.m_mniHypopse.Index = 0;
            this.SetMenuImage.SetMenuImage(this.m_mniHypopse, "12");
            this.m_mniHypopse.OwnerDraw = true;
            this.m_mniHypopse.Text = "触觉减退";
            // 
            // m_mniHapticDis
            // 
            this.m_mniHapticDis.Index = 1;
            this.SetMenuImage.SetMenuImage(this.m_mniHapticDis, "13");
            this.m_mniHapticDis.OwnerDraw = true;
            this.m_mniHapticDis.RadioCheck = true;
            this.m_mniHapticDis.Text = "触觉消失";
            // 
            // m_mniHyperaphia
            // 
            this.m_mniHyperaphia.Index = 2;
            this.SetMenuImage.SetMenuImage(this.m_mniHyperaphia, "14");
            this.m_mniHyperaphia.OwnerDraw = true;
            this.m_mniHyperaphia.Text = "触觉过敏或异样";
            // 
            // m_mniHypalgesia
            // 
            this.m_mniHypalgesia.Index = 3;
            this.SetMenuImage.SetMenuImage(this.m_mniHypalgesia, "15");
            this.m_mniHypalgesia.OwnerDraw = true;
            this.m_mniHypalgesia.Text = "痛觉减退";
            // 
            // m_mniAlgesia
            // 
            this.m_mniAlgesia.Index = 5;
            this.SetMenuImage.SetMenuImage(this.m_mniAlgesia, "17");
            this.m_mniAlgesia.OwnerDraw = true;
            this.m_mniAlgesia.Text = "感觉过敏或异样";
            // 
            // m_mniConcussive
            // 
            this.m_mniConcussive.Index = 6;
            this.SetMenuImage.SetMenuImage(this.m_mniConcussive, "18");
            this.m_mniConcussive.OwnerDraw = true;
            this.m_mniConcussive.Text = "震动觉减退或消失";
            // 
            // m_mniDislocation
            // 
            this.m_mniDislocation.Index = 7;
            this.SetMenuImage.SetMenuImage(this.m_mniDislocation, "19");
            this.m_mniDislocation.OwnerDraw = true;
            this.m_mniDislocation.Text = "位置觉减退或消失";
            // 
            // m_mniFleetAlgesiaDis
            // 
            this.m_mniFleetAlgesiaDis.Index = 8;
            this.SetMenuImage.SetMenuImage(this.m_mniFleetAlgesiaDis, "20");
            this.m_mniFleetAlgesiaDis.OwnerDraw = true;
            this.m_mniFleetAlgesiaDis.Text = "浅感觉全部消失";
            // 
            // m_mniDeepAlgesiaDis
            // 
            this.m_mniDeepAlgesiaDis.Index = 9;
            this.SetMenuImage.SetMenuImage(this.m_mniDeepAlgesiaDis, "21");
            this.m_mniDeepAlgesiaDis.OwnerDraw = true;
            this.m_mniDeepAlgesiaDis.Text = "深浅感觉全部消失";
            // 
            // menuItem4
            // 
            this.menuItem4.DefaultItem = true;
            this.menuItem4.Index = 0;
            this.SetMenuImage.SetMenuImage(this.menuItem4, "9");
            this.menuItem4.OwnerDraw = true;
            this.menuItem4.Text = "任意形状选择工具";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.SetMenuImage.SetMenuImage(this.menuItem5, "11");
            this.menuItem5.OwnerDraw = true;
            this.menuItem5.Text = "矩形选择工具";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 1;
            this.SetMenuImage.SetMenuImage(this.menuItem6, "10");
            this.menuItem6.OwnerDraw = true;
            this.menuItem6.Text = "圆形选择工具";
            // 
            // menuItem40
            // 
            this.menuItem40.Index = 11;
            this.SetMenuImage.SetMenuImage(this.menuItem40, "22");
            this.menuItem40.OwnerDraw = true;
            this.menuItem40.Text = "Ⅰ度";
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 10;
            this.SetMenuImage.SetMenuImage(this.menuItem41, null);
            this.menuItem41.OwnerDraw = true;
            this.menuItem41.Text = "-";
            // 
            // menuItem42
            // 
            this.menuItem42.Index = 12;
            this.SetMenuImage.SetMenuImage(this.menuItem42, "15");
            this.menuItem42.OwnerDraw = true;
            this.menuItem42.Text = "浅Ⅱ度";
            // 
            // menuItem43
            // 
            this.menuItem43.Index = 13;
            this.SetMenuImage.SetMenuImage(this.menuItem43, "16");
            this.menuItem43.OwnerDraw = true;
            this.menuItem43.Text = "深Ⅱ度";
            // 
            // menuItem44
            // 
            this.menuItem44.Index = 14;
            this.SetMenuImage.SetMenuImage(this.menuItem44, "13");
            this.menuItem44.OwnerDraw = true;
            this.menuItem44.Text = "Ⅲ度";
            // 
            // ctmTools
            // 
            this.ctmTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mniLine,
            this.m_mniRound,
            this.m_mniSolidRound,
            this.m_mniRectang,
            this.m_mniSolidRect,
            this.m_mniPolygon});
            // 
            // ctmFillTool
            // 
            this.ctmFillTool.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mniHypopse,
            this.m_mniHapticDis,
            this.m_mniHyperaphia,
            this.m_mniHypalgesia,
            this.m_mniAnalgesia,
            this.m_mniAlgesia,
            this.m_mniConcussive,
            this.m_mniDislocation,
            this.m_mniFleetAlgesiaDis,
            this.m_mniDeepAlgesiaDis,
            this.menuItem41,
            this.menuItem40,
            this.menuItem42,
            this.menuItem43,
            this.menuItem44});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_cmdSelected);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(208, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(48, 46);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // m_cmdSelected
            // 
            this.m_cmdSelected.BackColor = System.Drawing.Color.White;
            this.m_cmdSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdSelected.ForeColor = System.Drawing.Color.White;
            this.m_cmdSelected.Image = ((System.Drawing.Image)(resources.GetObject("m_cmdSelected.Image")));
            this.m_cmdSelected.Location = new System.Drawing.Point(12, 14);
            this.m_cmdSelected.Name = "m_cmdSelected";
            this.m_cmdSelected.Size = new System.Drawing.Size(24, 24);
            this.m_cmdSelected.TabIndex = 0;
            this.m_cmdSelected.UseVisualStyleBackColor = false;
            // 
            // m_gpbEdit
            // 
            this.m_gpbEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gpbEdit.Controls.Add(this.m_txtText);
            this.m_gpbEdit.Controls.Add(this.m_pnlEdit);
            this.m_gpbEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_gpbEdit.ForeColor = System.Drawing.Color.Black;
            this.m_gpbEdit.Location = new System.Drawing.Point(8, 68);
            this.m_gpbEdit.Name = "m_gpbEdit";
            this.m_gpbEdit.Size = new System.Drawing.Size(428, 188);
            this.m_gpbEdit.TabIndex = 12;
            this.m_gpbEdit.TabStop = false;
            this.m_gpbEdit.Text = "Edit";
            // 
            // m_pnlEdit
            // 
            this.m_pnlEdit.AutoScroll = true;
            this.m_pnlEdit.AutoScrollMargin = new System.Drawing.Size(4, 4);
            this.m_pnlEdit.BackColor = System.Drawing.SystemColors.Control;
            this.m_pnlEdit.ContextMenu = this.ctmNew;
            this.m_pnlEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlEdit.Location = new System.Drawing.Point(3, 22);
            this.m_pnlEdit.Name = "m_pnlEdit";
            this.m_pnlEdit.Size = new System.Drawing.Size(422, 163);
            this.m_pnlEdit.TabIndex = 10;
            this.m_pnlEdit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_pnlEdit_MouseDown);
            this.m_pnlEdit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_pnlEdit_MouseMove);
            this.m_pnlEdit.Paint += new System.Windows.Forms.PaintEventHandler(this.m_pnlEdit_Paint);
            this.m_pnlEdit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_pnlEdit_MouseUp);
            // 
            // ctmSelect
            // 
            this.ctmSelect.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem6,
            this.menuItem5});
            // 
            // m_gpbMain
            // 
            this.m_gpbMain.BackColor = System.Drawing.SystemColors.Control;
            this.m_gpbMain.Controls.Add(this.m_chkCanSizePic);
            this.m_gpbMain.Controls.Add(this.gpbTools);
            this.m_gpbMain.Controls.Add(this.groupBox1);
            this.m_gpbMain.Controls.Add(this.m_gpbEdit);
            this.m_gpbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gpbMain.Font = new System.Drawing.Font("宋体", 12F);
            this.m_gpbMain.ForeColor = System.Drawing.Color.Black;
            this.m_gpbMain.Location = new System.Drawing.Point(0, 0);
            this.m_gpbMain.Name = "m_gpbMain";
            this.m_gpbMain.Size = new System.Drawing.Size(444, 264);
            this.m_gpbMain.TabIndex = 13;
            this.m_gpbMain.TabStop = false;
            this.m_gpbMain.Text = "图片信息";
            // 
            // m_chkCanSizePic
            // 
            this.m_chkCanSizePic.Checked = true;
            this.m_chkCanSizePic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkCanSizePic.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_chkCanSizePic.Location = new System.Drawing.Point(284, 34);
            this.m_chkCanSizePic.Name = "m_chkCanSizePic";
            this.m_chkCanSizePic.Size = new System.Drawing.Size(140, 24);
            this.m_chkCanSizePic.TabIndex = 13;
            this.m_chkCanSizePic.Text = "可以改变图片大小";
            this.m_chkCanSizePic.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // SetMenuImage
            // 
            this.SetMenuImage.ImageList = this.imageIcon;
            // 
            // menuItem49
            // 
            this.menuItem49.Index = 4;
            this.SetMenuImage.SetMenuImage(this.menuItem49, null);
            this.menuItem49.Text = "视网膜眼底";
            // 
            // ctlPaintContainer
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.Controls.Add(this.m_gpbMain);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ctlPaintContainer";
            this.Size = new System.Drawing.Size(444, 264);
            this.gpbTools.ResumeLayout(false);
            this.gpbTools.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.m_gpbEdit.ResumeLayout(false);
            this.m_gpbMain.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region 设置图片
		
		/// <summary>
		/// 123
		/// </summary>
		public enum enmImageNames
		{
			/// <summary>
			/// 
			/// </summary>
			无  = 0,
			/// <summary>
			/// 
			/// </summary>
			神经外科感觉检查图片,
			/// <summary>
			/// 
			/// </summary>
			全部烧伤图
			
		}
		private enmImageNames m_enmImageName = enmImageNames.无;
		/// <summary>
		/// 设置下拉的图片
		/// </summary>
		[Description("设置初始默认的科室图片")]
		public enmImageNames 选择科室图片
		{
			get
			{
				return m_enmImageName;
			}
			set
			{
				m_enmImageName = value;
				m_mthClear();
				m_mthSetFirstImage();
			}
		}
		private bool m_blnCanAddImage = true;
		/// <summary>
		/// 能否添加图片
		/// </summary>
		[Browsable(false),Description("能否添加图片")]
		public bool m_BlnCanAddImage
		{
			get{return m_blnCanAddImage;}
			set
			{
				m_pnlEdit.ContextMenu = (value == true ? ctmNew : null);
				mniDelete.Visible = value;
				m_blnCanAddImage = value;
			}
		}
		/// <summary>
		/// 图片初始宽
		/// </summary>
		[Browsable(true),Description("图片初始宽")]
		public int m_IntDefaultWidth
		{
			get {return m_intDefaultWidth;}
			set {m_intDefaultWidth = value;}
		}

		/// <summary>
		/// 图片初始高
		/// </summary>
		[Description("图片初始高")]
		public int m_IntDefaultHeight
		{
			get {return m_intDefaultHeight;}
			set {m_intDefaultHeight = value;}
		}
		private bool m_blnScaleSize = true;
		/// <summary>
		/// 是否约束初始高和宽的比例
		/// </summary>
		[Description("是否约束初始高和宽的比例")]
		public bool m_BlnScaleSize
		{
			get {return m_blnScaleSize;}
			set {m_blnScaleSize = value;}
        }
        /// <summary>
        /// 标题
        /// </summary>
        [Browsable(true), Description("标题")]
        public new string Text
        {
            get { return m_gpbMain.Text; }
            set { m_gpbMain.Text = value; }
        }

//		private Bitmap m_bmpCurImage = null;
//		/// <summary>
//		/// 
//		/// </summary>
//		[Description("设置当前图片"),DefaultValue( null ),DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
//		public Bitmap m_BmpCurImage
//		{
//			get{return m_bmpCurImage;}
//			set
//			{
//				m_bmpCurImage = value;
//				if(value != null)
//					m_mthDisplayImageInPictureBox(value);
//			}
//		}
		#endregion

		#region 控件颜色

		private Color m_clrgpbTools = Color.FromArgb(51, 102, 153);
		[Description("工具箱GroupBox的颜色")]
		public Color m_ClrgpbTools
		{
			get
			{
				return m_clrgpbTools;
			}
			set
			{
				m_clrgpbTools = value;
				this.gpbTools.BackColor = value;
			}
		}

		private Color m_clrrdbPen = Color.Silver;
		[Description("画笔的按钮背景")]
		public Color m_ClrrdbPen
		{
			get
			{
				return m_clrrdbPen;
			}
			set
			{
				m_clrrdbPen = value;
				this.m_rdbPen.BackColor = value;
			}
		}

		private Color m_clrrdbLine = Color.Silver;
		[Description("画直线的按钮背景")]
		public Color m_ClrrdbLine
		{
			get
			{
				return m_clrrdbLine;
			}
			set
			{
				m_clrrdbLine = value;
				this.m_rdbLine.BackColor = value;
			}
		}

		private Color m_clrrdbDash = Color.Silver;
		[Description("画填充形状选择的按钮背景")]
		public Color m_ClrrdbDash
		{
			get
			{
				return m_clrrdbDash;
			}
			set
			{
				m_clrrdbDash = value;
				this.m_rdbDashed.BackColor = value;
			}
		}

		private Color m_clrrdbText = Color.Silver;
		[Description("画文本的按钮背景")]
		public Color m_ClrrdbText
		{
			get
			{
				return m_clrrdbText;
			}
			set
			{
				m_clrrdbText = value;
				this.m_rdbText.BackColor = value;
			}
		}

		private Color m_clrcmdSelected = Color.White;
		[Description("画已选择图案的按钮背景")]
		public Color m_ClrcmdSelected
		{
			get
			{
				return m_clrcmdSelected;
			}
			set
			{
				m_clrcmdSelected = value;
				this.m_cmdSelected.BackColor = value;
			}
		}

		//		private Color m_clrcmdRectangle_Solid = Color.Silver;
		//		[Description("画实心矩形的按钮背景")]
		//		public Color m_ClrcmdRectangle_Solid
		//		{
		//			get
		//			{
		//				return m_clrcmdRectangle_Solid;
		//			}
		//			set
		//			{
		//				m_clrcmdRectangle_Solid = value;
		//				this.m_rdbRectangle_Solid.BackColor = value;
		//			}
		//		}
		//
		//		private Color m_clrcmdPolygon = Color.Silver;
		//		[Description("画多边形的按钮背景")]
		//		public Color m_ClrcmdPolygon
		//		{
		//			get
		//			{
		//				return m_clrcmdPolygon;
		//			}
		//			set
		//			{
		//				m_clrcmdPolygon = value;
		//				this.m_rdbPolygon.BackColor = value;
		//			}
		//		}

		private Color m_clrcmdRubber = Color.Silver;
		[Description("橡皮擦的按钮背景")]
		public Color m_ClrcmdRubber
		{
			get
			{
				return m_clrcmdRubber;
			}
			set
			{
				m_clrcmdRubber = value;
				this.m_rdbRubber.BackColor = value;
			}
		}

		private Color m_clrppgPicSize = Color.FromArgb(51, 102, 153);
		[Description("设置图片大小的背景")]
		public Color m_ClrppgPicSize
		{
			get
			{
				return m_clrppgPicSize;
			}
			set
			{
				m_clrppgPicSize = value;
				this.ppgPicSize.ViewBackColor = value;
			}
		}
		#endregion 控件颜色

		private void m_mthInit()
		{
			m_cmdColor.BackColor = pn.Color;
			m_penDash = new Pen(Color.Black);
			m_penDash.DashPattern = new float[]{3.0f,3.0f};
			m_ghpSelectPath = new GraphicsPath();

			m_mthSetMenuClickEvent(ctmFillTool);
			m_mthSetMenuClickEvent(ctmSelect);
			m_mthSetMenuClickEvent(ctmTools);
			m_mthCheckedChangeEvent();
			m_mthSetNewPicMenuEvent();

			m_imgFillImage = new Bitmap(32,32);
			m_txbContent = new TextureBrush(m_imgFillImage);

		}
		/// <summary>
		/// 设置初始图片
		/// </summary>
		private void m_mthSetFirstImage()
		{
			if(m_enmImageName == enmImageNames.无 || m_pnlEdit.Controls.Count > 0)
				return;
			System.Resources.ResourceManager rm = new ResourceManager("HRPControl.Resources.Image",System.Reflection.Assembly.GetExecutingAssembly());
			Image img = (Bitmap)rm.GetObject(m_enmImageName.ToString());
			m_mthDisplayImageInPictureBox(img);
		}
		/// <summary>
		/// 选择打开图片
		/// </summary>
		/// <returns></returns>
		private string m_strLoadPicture()
		{
			
			OpenFileDialog objDlg= new OpenFileDialog();
			objDlg.Title="请选择要打开的图片";
			objDlg.CheckFileExists=true;
			objDlg.CheckPathExists=true;
			objDlg.AddExtension=true;
			objDlg.DefaultExt="bmp";
			objDlg.Filter="图片文件 (*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif|所有文件 (*.*)|*.*";
			objDlg.RestoreDirectory=true;
			

			if(objDlg.ShowDialog() ==DialogResult.OK)
			{
				return objDlg.FileName;
			}
			return "";
		}
		
		private void mniHavePic_Click(object sender, System.EventArgs e)
		{			
			string strFileName = m_strLoadPicture();
			if(strFileName == "")
				return;
			Image imgOriginal  = new Bitmap(strFileName);
			m_mthDisplayImageInPictureBox(imgOriginal);
		}
		/// <summary>
		/// 加载确定路径的图片文件
		/// </summary>
		/// <param name="p_strPaht"></param>
		public void m_mthLoadPic(string p_strPaht)
		{
			string strFileName = p_strPaht;
			if(strFileName == "")
				return;
			Image imgOriginal  = new Bitmap(strFileName);
			m_mthDisplayImageInPictureBox(imgOriginal);
		}
		/// <summary>
		/// 在编辑区显示图片
		/// </summary>
		/// <param name="p_imgOriginal"></param>
		private void m_mthDisplayImageInPictureBox(Image p_imgOriginal)
		{
			if(p_imgOriginal == null)
				return;
//			m_intDefaultWidth = m_pnlEdit.Height - 12;
//			m_intDefaultHeight = m_intDefaultWidth;
			clsPictureBoxValue objPic = new clsPictureBoxValue();
			objPic.m_imgFront = null;
			Image imgBack = null;
			if(m_blnScaleSize)
			{
				imgBack = new Bitmap(p_imgOriginal,(int)((float)m_intDefaultHeight/(float)p_imgOriginal.Height*p_imgOriginal.Width),m_intDefaultHeight);
			}
			else
				imgBack = new Bitmap(p_imgOriginal,m_intDefaultWidth,m_intDefaultHeight);
			objPic.m_imgBack = imgBack as Bitmap;
			objPic.intWidth = imgBack.Width;
			objPic.intHeight = imgBack.Height;
			objPic.intOriginalHeight = imgBack.Height;
			objPic.intOriginalWidth = imgBack.Width;
			objPic.clrBack = Color.White;

			PictureBox pic1 = new PictureBox();
			pic1.BackColor = Color.White;
			
			m_mthSetPicEvent(pic1,objPic);
			blnIfPaint = true;
			pic1.Invalidate();
		}

		/// <summary>
		/// 绑定图片框事件
		/// </summary>
		/// <param name="p_picSender"></param>
		/// <param name="p_objPic"></param>
		private void m_mthSetPicEvent(PictureBox p_picSender,clsPictureBoxValue p_objPic)
		{
//			m_blnCanResizeImage = true;
			p_picSender.Size = new System.Drawing.Size(p_objPic.intWidth,p_objPic.intHeight);
			p_picSender.Paint += new System.Windows.Forms.PaintEventHandler(pic1_Paint);
			p_picSender.MouseUp += new System.Windows.Forms.MouseEventHandler(pic1_MouseUp);
			p_picSender.MouseMove += new System.Windows.Forms.MouseEventHandler(pic1_MouseMove);
			p_picSender.MouseDown += new System.Windows.Forms.MouseEventHandler(pic1_MouseDown);
			p_picSender.Resize += new EventHandler(p_picSender_Resize);
			p_picSender.ContextMenu = ctmModify;
			p_picSender.Tag = p_objPic;

			m_mthPicLocation(p_picSender);
				
			if(m_blnOverOneLine(p_picSender))
				return;
			m_pnlEdit.Controls.Add(p_picSender);
			m_mthSelectPictureBox(p_picSender);
		}
		
		private void mniNoPic_Click(object sender, System.EventArgs e)
		{
//			m_intDefaultWidth = m_pnlEdit.Height - 12;
//			m_intDefaultHeight = m_intDefaultWidth;
			clsPictureBoxValue objPic = new clsPictureBoxValue();
			objPic.clrBack = Color.White;
			objPic.intWidth = m_intDefaultWidth;
			objPic.intHeight = m_intDefaultWidth;
			objPic.intOriginalHeight = m_intDefaultWidth;
			objPic.intOriginalWidth = m_intDefaultWidth;

			PictureBox pic1 = new PictureBox();
			pic1.BackColor = objPic.clrBack;
			m_mthSetPicEvent(pic1,objPic);
		}

		/// <summary>
		/// 图片框定位
		/// </summary>
		private void m_mthPicLocation(PictureBox p_picSender)
		{
			int intControlCount = m_pnlEdit.Controls.Count;
			if(intControlCount>0)
			{
				p_picSender.Left = m_pnlEdit.Controls[intControlCount - 1].Right + 20;
				p_picSender.Top = 4;
			}
			else
			{
				p_picSender.Left = 10;
				p_picSender.Top = 4;
			}
		}

		/// <summary>
		/// 判断图片是否超过一行
		/// </summary>
		/// <param name="p_ctlSender"></param>
		/// <returns></returns>
		private bool m_blnOverOneLine(Control p_ctlSender)
		{
			if(p_ctlSender.Right > 2000/*m_pnlEdit.Width*/)
			{
				MessageBox.Show("对不起,图片已超过指定宽度.","提示");
//				MessageBox.Show("对不起,图片不能超过一行.","提示");
				return true;
			}
			return false;
		}
		
		private void p_picSender_Resize(object sender, EventArgs e)
		{
			if(/*m_blnCanResizeImage || */m_chkCanSizePic.Checked)
			{
				PictureBox ctl1 = (PictureBox)sender;
				clsPictureBoxValue objPic = (clsPictureBoxValue)(ctl1.Tag);
				objPic.intWidth = ctl1.ClientRectangle.Width;
				objPic.intHeight = ctl1.ClientRectangle.Height;
				ctl1.Invalidate();
			}
		}
		
		private void pic1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(!blnIfPaint)
				return;

			PictureBox ctl1 = (PictureBox)sender;

			clsPictureBoxValue objPic = (clsPictureBoxValue)ctl1.Tag;

			if(objPic.m_imgBack!=null)
			{
				if(/*m_blnCanResizeImage || */m_chkCanSizePic.Checked)
				{
						e.Graphics.DrawImage(objPic.m_imgBack,0,0,ctl1.Width,ctl1.Height);
						objPic.intWidth = ctl1.ClientRectangle.Width;
						objPic.intHeight = ctl1.ClientRectangle.Height;
				}
				else
					e.Graphics.DrawImage(objPic.m_imgBack,0,0,objPic.intWidth,objPic.intHeight);
			}
			
			if(objPic.m_imgFront !=null)
			{
				//				e.Graphics.DrawImage(objPic.m_imgFront,0,0,ctl1.Width+2,ctl1.Height+2);
				if(/*m_blnCanResizeImage || */m_chkCanSizePic.Checked)
				{
					e.Graphics.DrawImage(objPic.m_imgFront,ctl1.ClientRectangle);
				}
				else
					e.Graphics.DrawImage(objPic.m_imgFront,0,0);
			}

			//只是画矩形轨迹
			if(m_imgDrawRectangle !=null)
			{
				e.Graphics.DrawImage(m_imgDrawRectangle,0,0,ctl1.Width,ctl1.Height);
				m_imgDrawRectangle = null;
			}
			//只是画圆轨迹
			if(m_imgDrawRound !=null)
			{
				e.Graphics.DrawImage(m_imgDrawRound,0,0,ctl1.Width,ctl1.Height);					
				m_imgDrawRound = null;
			}
			//只是线轨迹
			if(m_imgDrawLine !=null)
			{
				e.Graphics.DrawImage(m_imgDrawLine,0,0,ctl1.Width,ctl1.Height);					
				m_imgDrawLine = null;
			}
			//只是画实心圆轨迹
			if(m_imgDrawSolidRound !=null)
			{
				e.Graphics.DrawImage(m_imgDrawSolidRound,0,0,ctl1.Width,ctl1.Height);					
				m_imgDrawSolidRound = null;
			}
			//只是画实心矩形轨迹
			if(m_imgDrawSolidRectangle !=null)
			{
				e.Graphics.DrawImage(m_imgDrawSolidRectangle,0,0,ctl1.Width,ctl1.Height);					
				m_imgDrawSolidRectangle = null;
			}
			//只是画任意形状轨迹
			if(m_imgDrawDashPen != null)
			{
				e.Graphics.DrawImage(m_imgDrawDashPen,0,0,ctl1.Width,ctl1.Height);					
				m_imgDrawDashPen = null;
			}
		}

		private void mniBackPic_Click(object sender, System.EventArgs e)
		{
			string strFileName = m_strLoadPicture();
			if(strFileName == "")
				return;

			try
			{
				clsPictureBoxValue objPic = (clsPictureBoxValue)ctmModify.SourceControl.Tag;
				objPic.m_imgBack = new Bitmap(strFileName);
				blnIfPaint = true;
				ctmModify.SourceControl.Invalidate();
			}

			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void mniBackColor_Click(object sender, System.EventArgs e)
		{
			ColorDialog objDialog = new ColorDialog();

			if(objDialog.ShowDialog() == DialogResult.OK)
			{
				clsPictureBoxValue objPic = (clsPictureBoxValue)ctmModify.SourceControl.Tag;
				objPic.m_imgBack = null;
				objPic.clrBack = objDialog.Color;
				ctmModify.SourceControl.BackColor = objDialog.Color;
				blnIfPaint = true;
				ctmModify.SourceControl.Invalidate();
			}
		}

		private void mniDelete_Click(object sender, System.EventArgs e)
		{
			m_pnlEdit.Controls.Remove(ctmModify.SourceControl);
			foreach(Control ctlSub in m_pnlEdit.Controls)
			{
				if(ctlSub.Left > ctmModify.SourceControl.Left)
					ctlSub.Left -= ctmModify.SourceControl.Width + 20;
			}
			m_mthClearBorder();
			m_ghpSelectPath.Reset();
		}

		private void m_rdbLine_Click(object sender, System.EventArgs e)
		{
			ctmTools.Show((RadioButton)sender,new Point(0,((RadioButton)sender).Height));
			m_cmdSelected.Image = ((RadioButton)sender).Image;
		}

		private void m_rdbPen_Click(object sender, System.EventArgs e)
		{
			m_enmPicPaintType = enmPictureBoxPaintType.Pen;
			m_cmdSelected.Image = ((RadioButton)sender).Image;
		}

		private void m_rdbRubber_Click(object sender, System.EventArgs e)
		{
			m_enmPicPaintType = enmPictureBoxPaintType.Rubber;
			m_cmdSelected.Image = ((RadioButton)sender).Image;
		}

		private void pic1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			clsPictureBoxValue objPic = (clsPictureBoxValue)(((PictureBox)sender).Tag);
			m_mthSelectPictureBox((PictureBox)sender);
//			m_blnCanResizeImage = false;

			m_blnIfPaintPoint = true;

			if(m_enmPicPaintType != enmPictureBoxPaintType.Polygon)
			{
				intX1 = e.X;
				intY1 = e.Y;
			}

			//一开始鼠标未按下时
			if(intX1==0 && intY1==0)
			{
				intX1 = e.X;
				intY1 = e.Y;

				intXDefault = intX1;
				intYDefault = intY1;
			}
			m_mthHandleTextInput(sender,e);
		}

		/// <summary>
		/// 处理文字输入
		/// </summary>
		/// <param name="sender"></param>
		private void m_mthHandleTextInput(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(m_enmPicPaintType == enmPictureBoxPaintType.Text)
			{
				PictureBox picContainer = (PictureBox)sender;
				if(m_txtText.Visible)
				{
					if(m_txtText.Text != "")
					{
						clsPictureBoxValue objPic = (clsPictureBoxValue)picContainer.Tag;
						Image imgFront;
						Graphics g;				
						if(objPic.m_imgFront==null)
							imgFront = new Bitmap(objPic.intWidth,objPic.intHeight);
						else
							imgFront = objPic.m_imgFront;					
						g = Graphics.FromImage(imgFront);
						Rectangle rtg = new Rectangle(m_txtText.Location,new Size(m_txtText.Width,m_txtText.Height));						
						g.DrawString(m_txtText.Text,m_txtText.Font,Brushes.Black,rtg);						
						g.Dispose();
						objPic.m_imgFront = imgFront as Bitmap;
                        picContainer.Tag = objPic;
						m_txtText.Text = "";
						picContainer.Invalidate();
					}
					m_txtText.Visible = false;
				}
				else
				{
					picContainer.Controls.Add(m_txtText);
					m_txtText.Location = new Point(e.X,e.Y);
					m_txtText.Width = picContainer.Width - m_txtText.Left;
					m_txtText.Height = picContainer.Height - m_txtText.Top;
					m_txtText.Visible = true;
					m_txtText.Focus();
				}
			}
			else//没有选中文字工具时
			{
				if(m_txtText.Visible)
					m_txtText.Visible = false;
			}
		}

		private void pic1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			blnIfPaint = true;
			m_blnIfPaintPoint = false;
			intX2 = e.X;
			intY2 = e.Y;

			clsPictureBoxValue objPic = (clsPictureBoxValue)((Control)sender).Tag;
			Image imgFront;
			Graphics g;
				
			if(objPic.m_imgFront==null)
				imgFront = new Bitmap(objPic.intWidth,objPic.intHeight);
			else
				imgFront = objPic.m_imgFront;
			g = Graphics.FromImage(imgFront);
			if(objPic.m_imgFront != null)
				g.DrawImage(objPic.m_imgFront,0,0);
			switch(m_enmPicPaintType)
			{
				case enmPictureBoxPaintType.Pen:
				{
					SolidBrush sb = new SolidBrush(pn.Color);
					g.FillRectangle(sb,intX1,intY1,1,1);
				}
					break;
				case enmPictureBoxPaintType.Line:
					g.DrawLine(pn,intX1,intY1,intX2,intY2);
					break;
				case enmPictureBoxPaintType.Round:
					g.DrawEllipse(pn,intX1,intY1,intX2-intX1,intY2-intY1);
					break;
				case enmPictureBoxPaintType.Rectangle:
				{
					Rectangle rtg = m_rtgGetRectangle(intX1,intX2,intY1,intY2);;
					g.DrawRectangle(pn,rtg);
				}
					break;
				case enmPictureBoxPaintType.Rubber:	
				{
					SolidBrush sb = new SolidBrush(Color.White);
					g.FillRectangle(sb,intX1-2,intY1-2,4,4);
				}
					break;
				case enmPictureBoxPaintType.Polygon:
					g.DrawLine(pn,intX1,intY1,intX2,intY2);
					intX1 = intX2;
					intY1 = intY2;

					if(intX2==intXDefault && intY2 ==intYDefault)
					{
						intX1 = 0;
						intY1 = 0;
					}
					break;
				case enmPictureBoxPaintType.SolidRound:
				{
					SolidBrush sb = new SolidBrush(pn.Color);
					g.FillEllipse(sb,intX1,intY1,intX2-intX1,intY2-intY1);
				}
					break;
				case enmPictureBoxPaintType.SolidRect:
				{
					SolidBrush sb = new SolidBrush(pn.Color);
					g.FillRectangle(sb,intX1,intY1,intX2-intX1,intY2-intY1);
				}
					break;
				case enmPictureBoxPaintType.DashPen:
					m_imgDrawDashPen = null;
					m_mthFillImage(g);
					break;
				case enmPictureBoxPaintType.DashRect:
				{
					Rectangle rtg = m_rtgGetRectangle(intX1,intX2,intY1,intY2);;
					m_ghpSelectPath.Reset();
					m_ghpSelectPath.AddRectangle(rtg);
					m_mthFillImage(g);
				}
					break;
				case enmPictureBoxPaintType.DashRound:
					m_ghpSelectPath.Reset();
					m_ghpSelectPath.AddEllipse(intX1,intY1,intX2-intX1,intY2-intY1);
					m_mthFillImage(g);
					break;
			}
			g.Dispose();
			objPic.m_imgFront = imgFront as Bitmap;
            ((Control)sender).Tag = objPic;
			((Control)sender).Invalidate();
		}

		private void m_mthFillImage(Graphics p_gphSender)
		{
			m_ghpSelectPath.CloseAllFigures();
			m_rgnContent = new Region(m_ghpSelectPath);
			p_gphSender.FillRegion(m_txbContent,m_rgnContent);
			m_ghpSelectPath.Reset();
		}

		/// <summary>
		/// 画轨迹
		/// </summary>
		/// <param name="p_intPaintType"></param>
		/// <param name="p_intX1"></param>
		/// <param name="p_intY1"></param>
		/// <param name="p_intX2"></param>
		/// <param name="p_intY2"></param>
		private void m_mthPaintTrack(int p_intPaintType,int p_intX1,int p_intY1,int p_intX2,int p_intY2)
		{
			switch(p_intPaintType)
			{
				case 1:	
					break;

				case 2:
					break;

				case 3:
					break;

				case 4:
					break;

				case 5:
					break;

				case 6:
					break;
			}
		}


		private void pic1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			intX2 = e.X;
			intY2 = e.Y;

			if(m_blnIfPaintPoint)
			{
				PictureBox picContainer = (PictureBox)sender;
				clsPictureBoxValue objPic = (clsPictureBoxValue)picContainer.Tag;
				Image imgFront = new Bitmap(picContainer.Width,picContainer.Height);
				Graphics g = Graphics.FromImage(imgFront);
				SolidBrush sb = new SolidBrush(pn.Color);
				if(objPic.m_imgFront != null)
					g.DrawImage(objPic.m_imgFront,0,0);

				switch(m_enmPicPaintType)
				{
					case enmPictureBoxPaintType.Pen:
						g.DrawLine(pn,intX1,intY1,intX2,intY2);
						intX1 = intX2;
						intY1 = intY2;
						break;
					case enmPictureBoxPaintType.Line:
						pn.DashStyle = DashStyle.Dot;
						m_imgDrawLine = new Bitmap(picContainer.Width,picContainer.Height);	
						g = Graphics.FromImage(m_imgDrawLine);
						g.DrawLine(pn,intX1,intY1,intX2,intY2);	
						pn.DashStyle = DashStyle.Solid;
						break;
					case enmPictureBoxPaintType.Round:
						pn.DashStyle = DashStyle.Dot;
						m_imgDrawRound = new Bitmap(picContainer.Width,picContainer.Height);				
						g = Graphics.FromImage(m_imgDrawRound);
						g.DrawEllipse(pn,intX1,intY1,intX2-intX1,intY2-intY1);
						pn.DashStyle = DashStyle.Solid;
						break;
					case enmPictureBoxPaintType.Rectangle:
					{
						pn.DashStyle = DashStyle.Dot;
						m_imgDrawRectangle = new Bitmap(picContainer.Width,picContainer.Height);				
						g = Graphics.FromImage(m_imgDrawRectangle);
						Rectangle rtg = m_rtgGetRectangle(intX1,intX2,intY1,intY2);
						g.DrawRectangle(pn,rtg);
						pn.DashStyle = DashStyle.Solid;
					}
						break;
					case enmPictureBoxPaintType.Rubber:
						Color clrRubber = System.Drawing.Color.FromArgb(0,100,100,100);//(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
						SolidBrush sbRubber = new SolidBrush(Color.White);
						g.FillRectangle(sbRubber,intX1-2,intY1-2,4,4);
				
						intX1 = intX2;
						intY1 = intY2;
						break;
					case enmPictureBoxPaintType.Polygon:
						pn.DashStyle = DashStyle.Dot;
						m_imgDrawRound = new Bitmap(picContainer.Width,picContainer.Height);				
						g = Graphics.FromImage(m_imgDrawRound);
						g.DrawLine(pn,intX1,intY1,intX2,intY2);	
						pn.DashStyle = DashStyle.Solid;
						break;
					case enmPictureBoxPaintType.SolidRound:						
						m_imgDrawSolidRound = new Bitmap(picContainer.Width,picContainer.Height);				
						g = Graphics.FromImage(m_imgDrawSolidRound);
						g.FillEllipse(sb,intX1,intY1,intX2-intX1,intY2-intY1);
						break;
					case enmPictureBoxPaintType.SolidRect:
						m_imgDrawSolidRectangle = new Bitmap(picContainer.Width,picContainer.Height);				
						g = Graphics.FromImage(m_imgDrawSolidRectangle);
						g.FillRectangle(sb,intX1,intY1,Math.Abs(intX2-intX1),Math.Abs(intY2-intY1));						
						break;
					case enmPictureBoxPaintType.DashPen:
						Pen p = new Pen(Color.Black);
						p.DashStyle = DashStyle.DashDot;
						m_imgDrawDashPen = new Bitmap(picContainer.Width,picContainer.Height);				
						g = Graphics.FromImage(m_imgDrawDashPen);
						m_ghpSelectPath.AddLine(intX1,intY1,intX2,intY2);
						g.DrawPath(p,m_ghpSelectPath);
						intX1 = intX2;
						intY1 = intY2;
						break;
					case enmPictureBoxPaintType.DashRect:
					{
						m_imgDrawRectangle = new Bitmap(picContainer.Width,picContainer.Height);				
						g = Graphics.FromImage(m_imgDrawRectangle);
						Rectangle rtg = m_rtgGetRectangle(intX1,intX2,intY1,intY2);
						g.DrawRectangle(m_penDash,rtg);
					}
						break;
					case enmPictureBoxPaintType.DashRound:
						m_imgDrawRound = new Bitmap(picContainer.Width,picContainer.Height);				
						g = Graphics.FromImage(m_imgDrawRound);
						g.DrawEllipse(m_penDash,intX1,intY1,intX2-intX1,intY2-intY1);
						break;
				}

				g.Dispose();
				objPic.m_imgFront = imgFront as Bitmap;
                ((Control)sender).Tag = objPic;
				
				((Control)sender).Invalidate();
			}
		}

		/// <summary>
		/// 定位矩形框
		/// </summary>
		/// <param name="p_intX1"></param>
		/// <param name="p_intX2"></param>
		/// <param name="p_intY1"></param>
		/// <param name="p_intY2"></param>
		/// <returns></returns>
		private Rectangle m_rtgGetRectangle(int p_intX1, int p_intX2,int p_intY1, int p_intY2)
		{
			if((p_intX2-p_intX1) < 0 && (intY2-p_intY1) < 0)
				return new Rectangle(intX2,intY2,Math.Abs(p_intX2-p_intX1),Math.Abs(p_intY2-p_intY1));
			else if((p_intX2-p_intX1) < 0)
				return new Rectangle(intX2,intY1,Math.Abs(p_intX2-p_intX1),Math.Abs(p_intY2-p_intY1));
			else if((p_intY2-p_intY1) < 0)
				return new Rectangle(intX1,intY2,Math.Abs(p_intX2-p_intX1),Math.Abs(p_intY2-p_intY1));
			else
				return new Rectangle(intX1,intY1,Math.Abs(p_intX2-p_intX1),Math.Abs(p_intY2-p_intY1));
		}

		private void m_cmdColor_Click(object sender, System.EventArgs e)
		{
			ColorDialog objDialog = new ColorDialog();

			if(objDialog.ShowDialog() == DialogResult.OK)
			{
				pn.Color = objDialog.Color;
				m_cmdColor.BackColor = objDialog.Color;
			}
		}

		private void cmdSize_Click(object sender, System.EventArgs e)
		{
			
		}

		private void mniDeleteModify_Click(object sender, System.EventArgs e)
		{
			clsPictureBoxValue objPic = (clsPictureBoxValue)ctmModify.SourceControl.Tag;

			objPic.m_imgFront = null;
			this.Refresh();
		}

		private void mniPicSize_Click(object sender, System.EventArgs e)
		{
			ppgPicSize.SelectedObject = (clsPictureBoxValue)ctmModify.SourceControl.Tag;

		}
		private void m_mthModifyPicSize(object sender,EventArgs e)
		{
		}

		private void ppgPicSize_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{		
			clsPictureBoxValue objPic = (clsPictureBoxValue)ctmModify.SourceControl.Tag;

			PictureBox picSelected = (PictureBox)ctmModify.SourceControl;
			int intDifferentWidth = objPic.intWidth - picSelected.Width;
			picSelected.Width = objPic.intWidth;
			picSelected.Height = objPic.intHeight;
			
			//建一个新的image,把旧的image画上去,再将新的赋给objPic
			Image imgNew = new Bitmap(objPic.intWidth,objPic.intHeight);
			Graphics g = Graphics.FromImage(imgNew);
			g.DrawImage(objPic.m_imgFront,0,0,objPic.intWidth,objPic.intHeight);
			g.Dispose();
			objPic.m_imgFront = imgNew as Bitmap;

			picSelected.Tag = objPic;
			picSelected.Invalidate();

			foreach(Control ctlSub in this.Controls)
			{
				if(ctlSub.TabIndex > ctmModify.SourceControl.TabIndex)
				{
					ctlSub.Left += intDifferentWidth;
				}
			}
		}

		private void mniPicScale_Click(object sender, System.EventArgs e)
		{
			txtScale.Focus();
		}

		private void txtScale_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode.ToString()=="Enter")
			{
				float fltScale =Convert.ToInt32(txtScale.Text) * (float)0.01;
 
				clsPictureBoxValue objPic = (clsPictureBoxValue)ctmModify.SourceControl.Tag;

				objPic.intWidth = Convert.ToInt32(objPic.intOriginalWidth * fltScale);
				objPic.intHeight = Convert.ToInt32(objPic.intOriginalWidth * fltScale);

				PictureBox picSelected = (PictureBox)ctmModify.SourceControl;
				int intDifferentWidth = objPic.intWidth - picSelected.Width;
				picSelected.Width = objPic.intWidth;
				picSelected.Height = objPic.intHeight;
			
				//建一个新的image,把旧的image画上去,再将新的赋给objPic
				Image imgNew = new Bitmap(objPic.intWidth,objPic.intHeight);
				Graphics g = Graphics.FromImage(imgNew);
				g.DrawImage(objPic.m_imgFront,0,0,objPic.intWidth,objPic.intHeight);
				g.Dispose();
				objPic.m_imgFront = imgNew as Bitmap;

				picSelected.Tag = objPic;
				picSelected.Invalidate();

				foreach(Control ctlSub in this.Controls)
				{
					if(ctlSub.TabIndex > ctmModify.SourceControl.TabIndex)
					{
						ctlSub.Left += intDifferentWidth;
					}
				}
			}
			
		}

		#region 保存函数
		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			m_objPicInfoArr = m_objGetPicValue();
		}
		public clsPictureBoxValue[] m_objGetPicValue()
		{
			clsPictureBoxValue[] objPicValueArr = null;

			m_arlPic.Clear();

			foreach(Control ctlSub in m_pnlEdit.Controls)
			{
				if(ctlSub.GetType().Name=="PictureBox")
				{					
					if(ctlSub.Tag!=null)
					{
						clsPictureBoxValue objPic = (clsPictureBoxValue)ctlSub.Tag;
						
						//将底图和面图合成
						Image imgTemp = new Bitmap(ctlSub.Width,ctlSub.Height);
						Graphics g1 = Graphics.FromImage(imgTemp);
						//先画底色
						SolidBrush sb = new SolidBrush(objPic.clrBack);
						g1.FillRectangle(sb,0,0,imgTemp.Width,imgTemp.Height);
						//再画背景图
						if(objPic.m_imgBack!=null)
						{
//							g1.DrawImage(objPic.m_imgBack,0,0,objPic.intOriginalWidth,objPic.intOriginalHeight);
							g1.DrawImage(objPic.m_imgBack,0,0,objPic.intWidth,objPic.intHeight);							
						}
						if(objPic.m_imgFront != null)
							g1.DrawImage(objPic.m_imgFront,0,0,objPic.intWidth,objPic.intHeight);
						objPic.m_imgBack = imgTemp as Bitmap;
						objPic.m_imgFront = null;
						objPic.m_bytImage = m_bytImageToBinary(imgTemp);
						objPic.intHeight = ctlSub.Height;
						objPic.intWidth = ctlSub.Width;
						objPic.intOriginalHeight = ctlSub.Height;
						objPic.intOriginalWidth = ctlSub.Width;
						objPic.clrBack = ctlSub.BackColor;
						m_arlPic.Add(objPic);
						g1.Dispose();
					}
				}
			}
			if(m_arlPic.Count>0)
			{
				objPicValueArr = (clsPictureBoxValue[])m_arlPic.ToArray(typeof(clsPictureBoxValue));
			}
			m_arlPic.Clear();
	
			return objPicValueArr;
		}
		#endregion 保存函数

		#region 读取函数
		private void cmdRead_Click(object sender, System.EventArgs e)
		{
			m_mthSetPicValue(m_objPicInfoArr);
		}

        Bitmap Convert2Bitmap(object p_obj)
        {
            System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])p_obj);
            return new Bitmap(objStream);

        }
        public void m_mthSetPicValue(clsPictureBoxValue[] p_objPicArr)
		{
			if(p_objPicArr!=null && p_objPicArr.Length>0)
			{
				m_mthClear();
//				m_blnCanResizeImage = true;
				for(int i=0;i<p_objPicArr.Length;i++)
				{
					PictureBox pic1 = new PictureBox();
					pic1.Size = new System.Drawing.Size(p_objPicArr[i].intWidth,p_objPicArr[i].intHeight);
					pic1.BackColor = p_objPicArr[i].clrBack;
					
					pic1.Paint += new System.Windows.Forms.PaintEventHandler(pic1_Paint);
					pic1.MouseUp += new System.Windows.Forms.MouseEventHandler(pic1_MouseUp);
					pic1.MouseMove += new System.Windows.Forms.MouseEventHandler(pic1_MouseMove);
					pic1.MouseDown += new System.Windows.Forms.MouseEventHandler(pic1_MouseDown);
					pic1.Resize += new EventHandler(p_picSender_Resize);
					pic1.ContextMenu = ctmModify;

					p_objPicArr[i].m_imgBack = Convert2Bitmap(p_objPicArr[i].m_bytImage);
					p_objPicArr[i].m_imgFront = null;

					p_objPicArr[i].intOriginalHeight = p_objPicArr[i].intHeight;
					p_objPicArr[i].intOriginalWidth = p_objPicArr[i].intWidth;
					p_objPicArr[i].intHeight = p_objPicArr[i].intHeight;
					p_objPicArr[i].intWidth = p_objPicArr[i].intWidth;
					pic1.Tag = p_objPicArr[i];

					m_mthPicLocation(pic1);

					m_pnlEdit.Controls.Add(pic1);

					blnIfPaint = true;
					pic1.Invalidate();
				}				
			}
		}
		#endregion 读取函数

		private byte [] m_bytImageToBinary(Image p_img)
		{
			System.IO.MemoryStream objTempStream = new System.IO.MemoryStream();

			p_img.Save(objTempStream,System.Drawing.Imaging.ImageFormat.Bmp);

			byte[] bytArr =  objTempStream.ToArray();
            objTempStream.Dispose();
            return bytArr;
		}

		private Image m_imgBinaryToImage(object p_obj)
		{
			System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])p_obj);

			Image img = new Bitmap(objStream);
            objStream.Dispose();
			return img;
		}

		/// <summary>
		/// 清除所有图片信息
		/// </summary>
		public void m_mthClear()
		{
			for(int i = 0; i < m_pnlEdit.Controls.Count; i++)
			{
				if(m_pnlEdit.Controls[i].GetType().Name == "PictureBox")
				{
					m_pnlEdit.Controls.RemoveAt(i);
					i--;//当你Remove了一个控件，Count自然就会减1，所以i不能加1
				}
			}
			m_mthClearBorder();
		}
		/// <summary>
		/// 清除所有图片信息后是否设置初始图片
		/// </summary>
		/// <param name="p_blnAddFirstImage">是设置初始图片</param>
		public void m_mthClear(bool p_blnAddFirstImage)
		{
			m_mthClear();
			if(p_blnAddFirstImage)
				m_mthSetFirstImage();
		}

		private void m_rdbText_Click(object sender, System.EventArgs e)
		{
			m_enmPicPaintType = enmPictureBoxPaintType.Text;
			m_cmdSelected.Image = ((RadioButton)sender).Image;
		}

		/// <summary>
		/// 选中图片框，使图片框处于可改变大小状态
		/// </summary>
		private void m_mthSelectPictureBox(PictureBox p_picSender)
		{
			m_enmContainerPaintType = enmContainerPaintType.DrawBorder;
			m_picSelected = p_picSender;
			m_pnlEdit.Invalidate();
		}

		/// <summary>
		/// 选中控件时画边框的宽度
		/// </summary>
		private int m_intDrawBorderWidth = 4;

		private void m_pnlEdit_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			switch(m_enmContainerPaintType)
			{
				case enmContainerPaintType.DrawBorder :
					m_mthDrawSelectedPictureBoxBorder(e.Graphics);
					break;
				default :
					break;
			}
		}

		/// <summary>
		/// 画出选中图片框的边框
		/// </summary>
		/// <param name="g"></param>
		private void m_mthDrawSelectedPictureBoxBorder(Graphics g)
		{
			Pen pn = new Pen(Color.Gray,m_intDrawBorderWidth);
			Brush bs = new SolidBrush(Color.White);
			Rectangle rtgBounds=m_picSelected.Bounds;
			rtgBounds.X -= m_intDrawBorderWidth/2;
			rtgBounds.Y -= m_intDrawBorderWidth/2;
			rtgBounds.Width += m_intDrawBorderWidth;
			rtgBounds.Height += m_intDrawBorderWidth;
			g.DrawRectangle(pn,rtgBounds);

			//最外层的矩形，因为...
			Rectangle rtgOuter = new Rectangle();
			rtgOuter.X = rtgBounds.X - m_intDrawBorderWidth/2;
			rtgOuter.Y = rtgBounds.Y - m_intDrawBorderWidth/2;
			rtgOuter.Width += rtgBounds.Width + m_intDrawBorderWidth;
			rtgOuter.Height += rtgBounds.Height + m_intDrawBorderWidth;

			g.FillRectangle(bs,rtgOuter.Left,rtgOuter.Top,m_intDrawBorderWidth,m_intDrawBorderWidth);
			g.FillRectangle(bs,rtgOuter.Left + rtgOuter.Width/2 - m_intDrawBorderWidth/2,rtgOuter.Top,m_intDrawBorderWidth,m_intDrawBorderWidth);
			g.FillRectangle(bs,rtgOuter.Left + rtgOuter.Width - m_intDrawBorderWidth,rtgOuter.Top,m_intDrawBorderWidth,m_intDrawBorderWidth);
			g.FillRectangle(bs,rtgOuter.Left,rtgOuter.Top + rtgOuter.Height/2 - m_intDrawBorderWidth/2,m_intDrawBorderWidth,m_intDrawBorderWidth);
			g.FillRectangle(bs,rtgOuter.Right - m_intDrawBorderWidth,rtgOuter.Top + rtgOuter.Height/2 - m_intDrawBorderWidth/2,m_intDrawBorderWidth,m_intDrawBorderWidth);
			g.FillRectangle(bs,rtgOuter.Left,rtgOuter.Bottom - m_intDrawBorderWidth,m_intDrawBorderWidth,m_intDrawBorderWidth);
			g.FillRectangle(bs,rtgOuter.Left + rtgOuter.Width/2 - m_intDrawBorderWidth/2,rtgOuter.Bottom - m_intDrawBorderWidth,m_intDrawBorderWidth,m_intDrawBorderWidth);
			g.FillRectangle(bs,rtgOuter.Right - m_intDrawBorderWidth,rtgOuter.Bottom - m_intDrawBorderWidth,m_intDrawBorderWidth,m_intDrawBorderWidth);
		}
		/// <summary>
		/// 清除边框
		/// </summary>
		private void m_mthClearBorder()
		{
			//			m_picSelected = null;
			m_enmContainerPaintType = enmContainerPaintType.None;
			m_pnlEdit.Invalidate();
		}

		private int m_intLastMouseX,m_intLastMouseY;
		//		/// <summary>
		//		/// 容器鼠标是否按下
		//		/// </summary>
		//		private bool m_blnContainerMouseDown = false;

		private void m_pnlEdit_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			m_intLastMouseX = e.X;
			m_intLastMouseY = e.Y;
			//			m_blnContainerMouseDown = true;
		}

		private void m_pnlEdit_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.None)//没按下鼠标的移动
			{
				this.Cursor = Cursors.Default;
				m_blnCanResize = false;
				m_mthChangeCursorWhenMove(e);
			}
			else//按下鼠标的移动
			{
				if(m_blnCanResize)
				{					
					m_mthResizeControls(e);
					m_pnlEdit.Invalidate();
				}
			}
		}

		private bool m_blnCanResize = false;

		/// <summary>
		/// 鼠标移动到某一控件的右中角和右下角时,改变鼠标形状
		/// </summary>
		private void m_mthChangeCursorWhenMove(System.Windows.Forms.MouseEventArgs e)
		{
			if(m_picSelected == null)
				return;

			Rectangle rtgOuter = m_picSelected.Bounds;
			rtgOuter.X -= m_intDrawBorderWidth;
			rtgOuter.Y -= m_intDrawBorderWidth;
			rtgOuter.Width += m_intDrawBorderWidth * 2;
			rtgOuter.Height += m_intDrawBorderWidth *2;

			Rectangle rtgLeftTop = new Rectangle(rtgOuter.Left,rtgOuter.Top,m_intDrawBorderWidth,m_intDrawBorderWidth);
			Rectangle rtgMidTop = new Rectangle(rtgOuter.Left + rtgOuter.Width/2 - m_intDrawBorderWidth/2,rtgOuter.Top,m_intDrawBorderWidth,m_intDrawBorderWidth);
			Rectangle rtgRightTop = new Rectangle(rtgOuter.Right - m_intDrawBorderWidth,rtgOuter.Top,m_intDrawBorderWidth,m_intDrawBorderWidth);
			Rectangle rtgLeftMid = new Rectangle(rtgOuter.Left,rtgOuter.Top + rtgOuter.Height/2 - m_intDrawBorderWidth/2,m_intDrawBorderWidth,m_intDrawBorderWidth);
			Rectangle rtgRightMid = new Rectangle(rtgOuter.Right - m_intDrawBorderWidth,rtgOuter.Top + rtgOuter.Height/2 - m_intDrawBorderWidth/2,m_intDrawBorderWidth,m_intDrawBorderWidth);
			Rectangle rtgLeftBottom = new Rectangle(rtgOuter.Left,rtgOuter.Bottom - m_intDrawBorderWidth,m_intDrawBorderWidth,m_intDrawBorderWidth);
			Rectangle rtgMidBottom = new Rectangle(rtgOuter.Left + rtgOuter.Width/2 - m_intDrawBorderWidth/2,rtgOuter.Bottom - m_intDrawBorderWidth,m_intDrawBorderWidth,m_intDrawBorderWidth);
			Rectangle rtgRightBottom = new Rectangle(rtgOuter.Right - m_intDrawBorderWidth,rtgOuter.Bottom - m_intDrawBorderWidth,m_intDrawBorderWidth,m_intDrawBorderWidth);

			Point ptMouse = new Point(e.X,e.Y);

			if(rtgRightBottom.Contains(ptMouse))
			{
				this.Cursor=Cursors.SizeNWSE;
				m_blnCanResize = true;
				return;
			}
			else if(rtgRightMid.Contains(ptMouse))
			{
				this.Cursor=Cursors.SizeWE;
				m_blnCanResize = true;
				return;
			}
			else if(rtgMidBottom.Contains(ptMouse))
			{
				this.Cursor=Cursors.SizeNS;
				m_blnCanResize = true;
				return;
			}
		}

		/// <summary>
		/// 改变选中控件大小
		/// </summary>
		/// <param name="e"></param>
		private void m_mthResizeControls(System.Windows.Forms.MouseEventArgs e)
		{
			int intHSpace = e.X - m_intLastMouseX;
			int intVSpace = e.Y - m_intLastMouseY;

			if(this.Cursor == Cursors.SizeNS)
				intHSpace = 0;
			else if(this.Cursor == Cursors.SizeWE)
				intVSpace = 0;

			//限制最大宽度和高度(this)
//			if(intHSpace > m_pnlEdit.Width - m_picSelected.Right - m_intDrawBorderWidth)
//				intHSpace = m_pnlEdit.Width - m_picSelected.Right - m_intDrawBorderWidth;
//			if(intVSpace > m_pnlEdit.Height - m_picSelected.Bottom - m_intDrawBorderWidth)
//				intVSpace = m_pnlEdit.Height - m_picSelected.Bottom - m_intDrawBorderWidth;

			//限定最小值
			if(m_picSelected.Width + intHSpace > 50)
				m_picSelected.Width += intHSpace;
			if(m_picSelected.Height + intVSpace > 50)
				m_picSelected.Height += intVSpace;

			m_intLastMouseX = e.X;
			m_intLastMouseY = e.Y;

			m_picSelected.Invalidate();
		}

		/// <summary>
		/// 绑定菜单Click事件
		/// </summary>
		/// <param name="p_ctmSender"></param>
		private void m_mthSetMenuClickEvent(System.Windows.Forms.ContextMenu p_ctmSender)
		{
			if(p_ctmSender == null || p_ctmSender.MenuItems.Count <= 0)
				return;
			foreach(MenuItem item in p_ctmSender.MenuItems)
			{
				item.Click += new System.EventHandler(m_mthMenu_Click);
			}
		}

		private void m_mthMenu_Click(object sender,System.EventArgs e)
		{
			int intIndex = SetMenuImage.m_intGetImageIndex((MenuItem)sender);
			if(m_rdbLine.Checked)
			{
				m_rdbLine.Image = imageIcon.Images[intIndex];
				m_rdbLine.Tag = intIndex;
			}
			else if(m_rdbDashed.Checked)
			{
				m_rdbDashed.Image = imageIcon.Images[intIndex];
				m_rdbDashed.Tag = intIndex;
			}
			else if(m_rdbFill.Checked)
			{
				m_rdbFill.Image = imageIcon.Images[intIndex];
				m_mthSetFillImage(intIndex);
			}
			m_cmdSelected.Image = imageIcon.Images[intIndex];
			if(m_rdbLine.Checked || m_rdbDashed.Checked)
				m_enmPicPaintType = (enmPictureBoxPaintType)intIndex;

			if((enmPictureBoxPaintType)intIndex == enmPictureBoxPaintType.Polygon)
			{
				intX1 = 0;
				intY1 = 0;
			}
		}

		/// <summary>
		/// 设置填充的底图，图片资源文件的Build Action 属性必须设为“Embedded Resource”
		/// </summary>
		/// <param name="p_intIndex"></param>
		private void m_mthSetFillImage(int p_intIndex)
		{
			System.Resources.ResourceManager rm = new ResourceManager("HRPControl.Resources.Image",System.Reflection.Assembly.GetExecutingAssembly());
			switch((enmFillImageType)p_intIndex)
			{
				case enmFillImageType.触觉减退:
				default:
					m_imgFillImage = (Bitmap)rm.GetObject("触觉减退");
					break;
				case enmFillImageType.触觉消失:
					m_imgFillImage = (Bitmap)rm.GetObject("触觉消失");
					break;
				case enmFillImageType.触觉过敏或异样:
					m_imgFillImage = (Bitmap)rm.GetObject("触觉过敏或异样");
					break;
				case enmFillImageType.痛觉减退:
					m_imgFillImage = (Bitmap)rm.GetObject("痛觉减退");
					break;
				case enmFillImageType.痛觉消失:
					m_imgFillImage = (Bitmap)rm.GetObject("痛觉消失");
					break;
				case enmFillImageType.感觉过敏或异样:
					m_imgFillImage = (Bitmap)rm.GetObject("感觉过敏或异样");
					break;
				case enmFillImageType.震动觉减退或消失:
					m_imgFillImage = (Bitmap)rm.GetObject("震动觉减退或消失");
					break;
				case enmFillImageType.位置觉减退或消失:
					m_imgFillImage = (Bitmap)rm.GetObject("位置觉减退或消失");
					break;
				case enmFillImageType.浅感觉全部消失:
					m_imgFillImage = (Bitmap)rm.GetObject("浅感觉全部消失");
					break;
				case enmFillImageType.深浅感觉全部消失:
					m_imgFillImage = (Bitmap)rm.GetObject("深浅感觉全部消失");
					break;
				case enmFillImageType.I度:
					m_imgFillImage = (Bitmap)rm.GetObject("I度");
					break;
			}
			m_txbContent = new TextureBrush(m_imgFillImage);
		}

		private void m_rdbFill_Click(object sender, System.EventArgs e)
		{
			ctmFillTool.Show((RadioButton)sender,new Point(0,((RadioButton)sender).Height));
		}

		private void m_rdbDashed_Click(object sender, System.EventArgs e)
		{
			ctmSelect.Show((RadioButton)sender,new Point(0,((RadioButton)sender).Height));
			m_cmdSelected.Image = ((RadioButton)sender).Image;
		}

		/// <summary>
		/// 绑定RadioButtom的CheckedChange事件（改变控件背景色）
		/// </summary>
		private void m_mthCheckedChangeEvent()
		{
			foreach(Control ctl in gpbTools.Controls)
			{
				RadioButton rdb = ctl as RadioButton;
				if(rdb != null)
				{
					rdb.CheckedChanged += new System.EventHandler(m_mthRdb_CheckedChange);
				}
			}
		}

		private void m_mthRdb_CheckedChange(object sender, System.EventArgs e)
		{
			RadioButton rdb = sender as RadioButton;
			if(rdb.Checked == true)
			{
				rdb.BackColor = Color.White;
				if(rdb.Tag != null)
					m_enmPicPaintType = (enmPictureBoxPaintType)((int)rdb.Tag);
			}
			else
				rdb.BackColor = Color.Silver;

		}


		private void  m_mthSysMenu_Click(object sender,System.EventArgs e)
		{
			System.Resources.ResourceManager rm = new ResourceManager("HRPControl.Resources.Image",System.Reflection.Assembly.GetExecutingAssembly());
			Image img = (Bitmap)rm.GetObject(((MenuItem)sender).Text);

			 m_mthDisplayImageInPictureBox(img);
		}

		private void m_mthSetNewPicMenuEvent()
		{
			for(int i=0;i< menuItem1.MenuItems.Count;i++)
			{
				if(menuItem1.MenuItems[i].IsParent)
				{
					for(int j2=0;j2< menuItem1.MenuItems[i].MenuItems.Count;j2++)
					{
						menuItem1.MenuItems[i].MenuItems[j2].Click += new System.EventHandler(m_mthSysMenu_Click) ;
					}
				}
			}
		}

		private void m_pnlEdit_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
//			if(m_picSelected != null)
//				m_picSelected.Height -= m_intDrawBorderWidth;
		}

		private void ctlPaintContainer_Load(object sender, EventArgs e)
		{
			int intHeight = m_pnlEdit.Height - 12;
			m_intDefaultHeight = 253;//intHeight;
			m_intDefaultWidth = 320;//m_intDefaultHeight;
		}
	}

	#region 在enumerate中定义画图信息的类
//	/// <summary>
//	/// 住院病历画图控件信息
//	/// </summary>
//	public class clsPictureBoxValue
//	{
//		/// <summary>
//		/// 底图的文件路径
//		/// </summary>
//		//		public string m_strBackFileName;
//		/// <summary>
//		/// 底图
//		/// </summary>
//		public Image m_imgBack;
//		/// <summary>
//		/// 画在底图上的图案
//		/// </summary>
//		public Image m_imgFront;		
//		public byte[] m_bytImage;
//		/// <summary>
//		/// 背景色
//		/// </summary>
//		public Color clrBack;
//		/// <summary>
//		/// 图片宽度
//		/// </summary>
//		public int intWidth;
//		/// <summary>
//		/// 图片高度
//		/// </summary>
//		public int intHeight;
//		/// <summary>
//		/// 图片原始宽度
//		/// </summary>
//		public int intOriginalWidth;
//		/// <summary>
//		/// 图片原始高度
//		/// </summary>
//		public int intOriginalHeight;
//
//		public int 宽
//		{
//			get
//			{
//				return intWidth;
//			}
//			set
//			{
//				if(value<=0)
//				{					
//				}
//				else
//					intWidth = value;
//			}
//		}
//		public int 高
//		{
//			get
//			{
//				return intHeight;
//			}
//			set 
//			{
//				if(value<=0)
//				{					
//				}
//				else
//					intHeight = value;
//			}
//		}
//	}
	
	#endregion

	public class clsImageInfo
	{
		public Image imgBack;

		public Color clrBack;

		public Image imgFront;
	}
}
