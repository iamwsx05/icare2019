using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace  com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmInput ��ժҪ˵����
	/// </summary>
	public class frmInput  : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		internal PinkieControls.ButtonXP m_cmdSave;
		internal System.Windows.Forms.TextBox txtNo;
		internal System.Windows.Forms.TextBox txtname;
		public System.Windows.Forms.CheckBox chbPrint;
		internal PinkieControls.ButtonXP btnCense;
		private com.digitalwave.iCare.gui.HIS.clsControlOPMedStore ContorlInput;
		/// <summary>
		/// 
		/// </summary>
		public frmInput()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInput));
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.txtname = new System.Windows.Forms.TextBox();
            this.chbPrint = new System.Windows.Forms.CheckBox();
            this.btnCense = new PinkieControls.ButtonXP();
            this.SuspendLayout();
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(162, 18);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(88, 27);
            this.m_cmdSave.TabIndex = 1;
            this.m_cmdSave.Text = "ȷ��(&O)";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(24, 20);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(120, 21);
            this.txtNo.TabIndex = 1;
            this.txtNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNo_KeyPress);
            // 
            // txtname
            // 
            this.txtname.Enabled = false;
            this.txtname.Location = new System.Drawing.Point(24, 53);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(120, 21);
            this.txtname.TabIndex = 2;
            // 
            // chbPrint
            // 
            this.chbPrint.Location = new System.Drawing.Point(24, 80);
            this.chbPrint.Name = "chbPrint";
            this.chbPrint.Size = new System.Drawing.Size(128, 24);
            this.chbPrint.TabIndex = 3;
            this.chbPrint.Text = "��ҩ����ӡ������";
            // 
            // btnCense
            // 
            this.btnCense.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.btnCense.DefaultScheme = true;
            this.btnCense.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCense.Hint = "";
            this.btnCense.Location = new System.Drawing.Point(162, 49);
            this.btnCense.Name = "btnCense";
            this.btnCense.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnCense.Size = new System.Drawing.Size(88, 27);
            this.btnCense.TabIndex = 4;
            this.btnCense.Text = "ȡ��(&C)";
            this.btnCense.Click += new System.EventHandler(this.btnCense_Click);
            // 
            // frmInput
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(264, 101);
            this.Controls.Add(this.btnCense);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.txtNo);
            this.Controls.Add(this.m_cmdSave);
            this.Controls.Add(this.chbPrint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "������ҩԱ����";
            this.Load += new System.EventHandler(this.frmInput_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void frmInput_Load(object sender, System.EventArgs e)
		{
			txtNo.Focus();
		}
		/// <summary>
		/// 
		/// </summary>
		public override void CreateController()
		{
			this.objController = new clsContorlInput();
			this.objController.Set_GUI_Apperance(this);
		}
		/// <summary>
		/// 
		/// </summary>
		public void m_GetcontrolMetStore(clsControlOPMedStore Input)
		{
			this.ContorlInput=Input;
           ((clsContorlInput)this.objController).ShowForm();
		}

		private void m_cmdSave_Click(object sender, System.EventArgs e)
		{
			if(((clsContorlInput)this.objController).m_cmdSaveClick())
			{ 
				string employee=(string)txtNo.Tag;
				string employName=txtname.Text.Trim();
				((clsControlOPMedStore)this.ContorlInput).m_getData(employee,employName,this.chbPrint.Checked,"1");
				this.Close();
			}
		}

		private void txtNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				((clsContorlInput)this.objController).Determinant();
			}
		}

		private void btnCense_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
