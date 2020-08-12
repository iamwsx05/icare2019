using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmCheckCondition 的摘要说明。
	/// </summary>
	public class frmCheckCondition :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
		internal PinkieControls.ButtonXP buttonXP1;
		internal PinkieControls.ButtonXP buttonXP3;
		private System.Windows.Forms.TreeView treeView2;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.RadioButton radioButton4;
        private CheckBox chShowZero;
        private CheckBox ckIsStop;
        private Panel panel2;
        private RadioButton radioButton5;
        private RadioButton radioButton6;
        private GroupBox groupBox3;
        internal PinkieControls.ButtonXP buttonXP4;
        private TreeView treeView3;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmCheckCondition()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.buttonXP3 = new PinkieControls.ButtonXP();
            this.chShowZero = new System.Windows.Forms.CheckBox();
            this.ckIsStop = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonXP4 = new PinkieControls.ButtonXP();
            this.treeView3 = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 389);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Location = new System.Drawing.Point(3, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 372);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "药理分类";
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(96, 342);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(56, 24);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "反选";
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(16, 342);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(56, 24);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "全选";
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Window;
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView1.Location = new System.Drawing.Point(3, 19);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(194, 317);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.treeView2);
            this.groupBox2.Location = new System.Drawing.Point(220, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 372);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "剂型分类";
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(117, 341);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(56, 24);
            this.radioButton3.TabIndex = 7;
            this.radioButton3.Text = "反选";
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(37, 342);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(56, 24);
            this.radioButton4.TabIndex = 6;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "全选";
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // treeView2
            // 
            this.treeView2.BackColor = System.Drawing.SystemColors.Window;
            this.treeView2.CheckBoxes = true;
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView2.Location = new System.Drawing.Point(3, 19);
            this.treeView2.Name = "treeView2";
            this.treeView2.ShowLines = false;
            this.treeView2.ShowPlusMinus = false;
            this.treeView2.Size = new System.Drawing.Size(194, 317);
            this.treeView2.TabIndex = 5;
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(429, 293);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(122, 41);
            this.buttonXP1.TabIndex = 65;
            this.buttonXP1.Text = "药理与剂型组合";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // buttonXP3
            // 
            this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP3.DefaultScheme = true;
            this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP3.Hint = "";
            this.buttonXP3.Location = new System.Drawing.Point(429, 340);
            this.buttonXP3.Name = "buttonXP3";
            this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP3.Size = new System.Drawing.Size(122, 41);
            this.buttonXP3.TabIndex = 66;
            this.buttonXP3.Text = "取消(&E)";
            this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click);
            // 
            // chShowZero
            // 
            this.chShowZero.AutoSize = true;
            this.chShowZero.Location = new System.Drawing.Point(439, 217);
            this.chShowZero.Name = "chShowZero";
            this.chShowZero.Size = new System.Drawing.Size(117, 18);
            this.chShowZero.TabIndex = 67;
            this.chShowZero.Text = "显示“0”库存";
            this.chShowZero.UseVisualStyleBackColor = true;
            // 
            // ckIsStop
            // 
            this.ckIsStop.AutoSize = true;
            this.ckIsStop.Location = new System.Drawing.Point(439, 188);
            this.ckIsStop.Name = "ckIsStop";
            this.ckIsStop.Size = new System.Drawing.Size(82, 18);
            this.ckIsStop.TabIndex = 68;
            this.ckIsStop.Text = "显示停用";
            this.ckIsStop.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.treeView3);
            this.panel2.Controls.Add(this.radioButton5);
            this.panel2.Controls.Add(this.radioButton6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(122, 160);
            this.panel2.TabIndex = 69;
            // 
            // radioButton5
            // 
            this.radioButton5.Location = new System.Drawing.Point(59, 131);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(56, 24);
            this.radioButton5.TabIndex = 73;
            this.radioButton5.Text = "反选";
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton6
            // 
            this.radioButton6.Checked = true;
            this.radioButton6.Location = new System.Drawing.Point(2, 132);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(56, 24);
            this.radioButton6.TabIndex = 72;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "全选";
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Location = new System.Drawing.Point(426, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(128, 182);
            this.groupBox3.TabIndex = 70;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "药品类型组合";
            // 
            // buttonXP4
            // 
            this.buttonXP4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP4.DefaultScheme = true;
            this.buttonXP4.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP4.Hint = "";
            this.buttonXP4.Location = new System.Drawing.Point(429, 245);
            this.buttonXP4.Name = "buttonXP4";
            this.buttonXP4.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP4.Size = new System.Drawing.Size(122, 41);
            this.buttonXP4.TabIndex = 71;
            this.buttonXP4.Text = "药品类型组合";
            this.buttonXP4.Click += new System.EventHandler(this.buttonXP4_Click);
            // 
            // treeView3
            // 
            this.treeView3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.treeView3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView3.CheckBoxes = true;
            this.treeView3.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView3.Location = new System.Drawing.Point(0, 0);
            this.treeView3.Name = "treeView3";
            this.treeView3.ShowLines = false;
            this.treeView3.ShowPlusMinus = false;
            this.treeView3.Size = new System.Drawing.Size(120, 126);
            this.treeView3.TabIndex = 74;
            // 
            // frmCheckCondition
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(555, 389);
            this.ControlBox = false;
            this.Controls.Add(this.buttonXP4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ckIsStop);
            this.Controls.Add(this.chShowZero);
            this.Controls.Add(this.buttonXP3);
            this.Controls.Add(this.buttonXP1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCheckCondition";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "盘点条件[通过药理分类与剂型组合来盘点]";
            this.Load += new System.EventHandler(this.frmCheckCondition_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		private void frmCheckCondition_Load(object sender, System.EventArgs e)
		{
			m_mthInitCboMedicinePrepType();
			m_mthSelectAll(treeView1,true);
			m_mthSelectAll(treeView2,true);
            m_mthSelectAll(treeView3, true);
			treeView1.CollapseAll();
		}

		#region 初始化药品剂型和药理数据
		/// <summary>
		/// 初始化药品剂型和药理数据
		/// </summary>
		private void m_mthInitCboMedicinePrepType()
		{
			clsDomainControlMedicineStorageCheck objSVC=new clsDomainControlMedicineStorageCheck();
			System.Data.DataTable dt = new DataTable();
			DataTable dtPharmatype=new DataTable();
            DataTable dtmedtype = new DataTable();
            long lngRes = objSVC.m_lngGetMedinicePrepType(out dt, out dtPharmatype, out dtmedtype);
			if(lngRes>0&&dt.Rows.Count>0)
			{
				for(int i1=0;i1<dt.Rows.Count;i1++)
				{
					TreeNode parNode=new TreeNode(dt.Rows[i1]["MEDICINEPREPTYPENAME_VCHR"].ToString());
					parNode.Tag=dt.Rows[i1]["MEDICINEPREPTYPE_CHR"].ToString();
					treeView2.Nodes.Add(parNode);
				}
			}
            if (dtmedtype.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtmedtype.Rows.Count; i1++)
                {
                    TreeNode parNode = new TreeNode(dtmedtype.Rows[i1]["MEDICINETYPENAME_VCHR"].ToString());
                    parNode.Tag = dtmedtype.Rows[i1]["MEDICINETYPEID_CHR"].ToString();
                    treeView3.Nodes.Add(parNode);
                }
            }
			if(dtPharmatype.Rows.Count>0)
			{
				DataRow[] objRows=dtPharmatype.Select("PARENTID_CHR is null");
				if(objRows.Length>0)
				{
					TreeNode parNode=null;
					for(int i1=0;i1<objRows.Length;i1++)
					{
						parNode=new TreeNode();
						parNode.Text=objRows[i1]["PHARMANAME_VCHR"].ToString();
						treeView1.Nodes.Add(parNode);
						DataRow[] objSunRows=dtPharmatype.Select("PARENTID_CHR='"+objRows[i1]["PHARMAID_CHR"].ToString()+"'");
						if(objSunRows.Length>0)
						{
							TreeNode parNode1=null;
							for(int f2=0;f2<objSunRows.Length;f2++)
							{
								parNode1=new TreeNode();
								parNode1.Text=objSunRows[f2]["PHARMANAME_VCHR"].ToString();
								parNode1.Tag=objSunRows[f2]["PHARMAID_CHR"].ToString();
								treeView1.Nodes[i1].Nodes.Add(parNode1);
							}
						}
					}
				}
			}

		}
		#endregion 初始化药品剂型

		#region 全选/反选
		/// <summary>
		/// 全选/反选
		/// </summary>
		/// <param name="treeView"></param>
		/// <param name="blSele"></param>
		private void m_mthSelectAll(System.Windows.Forms.TreeView treeView,bool blSele)
		{
			if(treeView.Nodes.Count>0)
			{
				for(int i1=0;i1<treeView.Nodes.Count;i1++)
				{
					treeView.Nodes[i1].Checked=blSele;
				}
			}
		}
		#endregion

		#region 返回选中的药理信息
		/// <summary>
		/// 返回药理信息
		/// </summary>
		/// <returns></returns>
		public clsHISMedType_VO[] m_GetMedType()
		{
			clsHISMedType_VO[] reMedType=null;
			DataTable objTb=new DataTable();
			objTb.Columns.Add("PHARMAID_CHR");
			objTb.Columns.Add("PHARMANAME_VCHR");
			if(treeView1.Nodes.Count>0)
			{
				for(int i1=0;i1<treeView1.Nodes.Count;i1++)
				{
					if(treeView1.Nodes[i1].Nodes.Count>0)
					{
						for(int f2=0;f2<treeView1.Nodes[i1].Nodes.Count;f2++)
						{
							if(treeView1.Nodes[i1].Nodes[f2].Checked==true)
							{
								DataRow Row=objTb.NewRow();
								Row["PHARMAID_CHR"]=(string)treeView1.Nodes[i1].Nodes[f2].Tag;
								Row["PHARMANAME_VCHR"]=treeView1.Nodes[i1].Nodes[f2].Text;
								objTb.Rows.Add(Row);
							}
						}
					}
				}
				if(objTb.Rows.Count>0)
				{
					reMedType=new clsHISMedType_VO[objTb.Rows.Count];
					for(int j1=0;j1<objTb.Rows.Count;j1++)
					{
						reMedType[j1]=new clsHISMedType_VO();
						reMedType[j1].m_strPHARMAID_CHR=objTb.Rows[j1]["PHARMAID_CHR"].ToString();
						reMedType[j1].m_strPHARMANAME_VCHR=objTb.Rows[j1]["PHARMANAME_VCHR"].ToString();
					}
				}
			}
			return reMedType;
		}
		#endregion

		#region 返回选中的剂型
		/// <summary>
		/// 返回选中的剂型
		/// </summary>
		/// <returns></returns>
		public clsMedicinePrepType_VO[] m_GetMedPrepType()
		{
			DataTable objTb=new DataTable();
			objTb.Columns.Add("MedicinePrepTypeID");
			objTb.Columns.Add("MedicinePrepTypeName");
			clsMedicinePrepType_VO[] reMedType=null;
			if(treeView2.Nodes.Count>0)
			{
				for(int i1=0;i1<treeView2.Nodes.Count;i1++)
				{
					if(treeView2.Nodes[i1].Checked==true)
					{
						DataRow Row=objTb.NewRow();
						Row["MedicinePrepTypeID"]=(string)treeView2.Nodes[i1].Tag;
						Row["MedicinePrepTypeName"]=treeView2.Nodes[i1].Text;
						objTb.Rows.Add(Row);
					}
				}
				if(objTb.Rows.Count>0)
				{
					reMedType=new clsMedicinePrepType_VO[objTb.Rows.Count];
					for(int j1=0;j1<objTb.Rows.Count;j1++)
					{
						reMedType[j1]=new clsMedicinePrepType_VO();
						reMedType[j1].m_strMedicinePrepTypeID=objTb.Rows[j1]["MedicinePrepTypeID"].ToString();
						reMedType[j1].m_strMedicinePrepTypeName=objTb.Rows[j1]["MedicinePrepTypeName"].ToString();
					}
				}
			}
			return reMedType;
		}
		#endregion

        #region 返回药品类型
        /// <summary>
        /// 返回药品类型
        /// </summary>
        /// <returns></returns>
        public clsMedicineType_VO[] m_MedType()
        {
            DataTable objTb = new DataTable();
            objTb.Columns.Add("MEDICINETYPEID_CHR");
            objTb.Columns.Add("MEDICINETYPENAME_VCHR");
            clsMedicineType_VO[] reMedType = null;
            if (treeView3.Nodes.Count > 0)
            {
                for (int i1 = 0; i1 < treeView3.Nodes.Count; i1++)
                {
                    if (treeView3.Nodes[i1].Checked == true)
                    {
                        DataRow Row = objTb.NewRow();
                        Row["MEDICINETYPEID_CHR"] = (string)treeView3.Nodes[i1].Tag;
                        Row["MEDICINETYPENAME_VCHR"] = treeView3.Nodes[i1].Text;
                        objTb.Rows.Add(Row);
                    }
                }
                if (objTb.Rows.Count > 0)
                {
                    reMedType = new clsMedicineType_VO[objTb.Rows.Count];
                    for (int j1 = 0; j1 < objTb.Rows.Count; j1++)
                    {
                        reMedType[j1] = new clsMedicineType_VO();
                        reMedType[j1].m_strMedicineTypeID = objTb.Rows[j1]["MEDICINETYPEID_CHR"].ToString();
                        reMedType[j1].m_strMedicineTypeName = objTb.Rows[j1]["MEDICINETYPENAME_VCHR"].ToString();
                    }
                }
            }
            return reMedType;
        }
        #endregion

        public bool isShowZero
        {
            get
            {
                return chShowZero.Checked;
            }
        }
        public bool isStop
        {
            get
            {
                return ckIsStop.Checked;
            }
        }
		private void treeView1_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			int SeleIndex=e.Node.Index;
			if(e.Node.Tag==null)
			{
				if(e.Node.Checked==true)
				{
					if(e.Node.Nodes.Count>0)
					{
						this.treeView1.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
						for(int i1=0;i1<e.Node.Nodes.Count;i1++)
						{
							e.Node.Nodes[i1].Checked=true;
						}
						this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
					}
				}
				else
				{
					this.treeView1.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
					for(int i1=0;i1<e.Node.Nodes.Count;i1++)
					{
						e.Node.Nodes[i1].Checked=false;
					}
					this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
				}
				e.Node.Expand();
			}
			else
			{
				if(e.Node.Parent!=null)
				{
					
					this.treeView1.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
					e.Node.Parent.Checked=false;
					this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
				}
			}
		}

		private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radioButton1.Checked==true)
				m_mthSelectAll(treeView1,true);
		}

		private void radioButton2_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radioButton2.Checked==true)
				m_mthSelectAll(treeView1,false);
		}

		private void radioButton4_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radioButton4.Checked==true)
				m_mthSelectAll(treeView2,true);
		}

		private void radioButton3_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radioButton3.Checked==true)
				m_mthSelectAll(treeView2,false);
		}

		private void buttonXP2_Click(object sender, System.EventArgs e)
		{
			
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
            this.DialogResult = DialogResult.OK;
            this.Close();
			
		}

		private void buttonXP3_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.No;
			this.Close();
		}

        private void buttonXP4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
                m_mthSelectAll(treeView3, false);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
                m_mthSelectAll(treeView3, true);
        }
	}
}
