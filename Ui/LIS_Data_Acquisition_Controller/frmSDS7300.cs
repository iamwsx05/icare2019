using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS_Data_Acquisition_Controller
{
	/// <summary>
	/// frmSDS7300 ��ժҪ˵����
	/// </summary>
	public class frmSDS7300 : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private string m_strFilePath;
		private string m_strFileData;

		private System.Windows.Forms.GroupBox gpbMain;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.DateTimePicker dtpCreateDate;
		private System.Windows.Forms.ListView lsvFileName;
		private System.Windows.Forms.ColumnHeader clmFileName;
		private System.Windows.Forms.Button btn_Sure;
		private System.Windows.Forms.Button btn_Cancel;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSDS7300(string p_strFilePath)
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
			m_strFilePath = p_strFilePath;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSDS7300));
			this.gpbMain = new System.Windows.Forms.GroupBox();
			this.lsvFileName = new System.Windows.Forms.ListView();
			this.clmFileName = new System.Windows.Forms.ColumnHeader();
			this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btn_Sure = new System.Windows.Forms.Button();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.gpbMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpbMain
			// 
			this.gpbMain.Controls.Add(this.btn_Cancel);
			this.gpbMain.Controls.Add(this.btn_Sure);
			this.gpbMain.Controls.Add(this.lsvFileName);
			this.gpbMain.Controls.Add(this.dtpCreateDate);
			this.gpbMain.Controls.Add(this.lblTitle);
			this.gpbMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.gpbMain.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.gpbMain.Location = new System.Drawing.Point(0, 0);
			this.gpbMain.Name = "gpbMain";
			this.gpbMain.Size = new System.Drawing.Size(376, 376);
			this.gpbMain.TabIndex = 0;
			this.gpbMain.TabStop = false;
			this.gpbMain.Text = "ѡ�������ļ�";
			// 
			// lsvFileName
			// 
			this.lsvFileName.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.clmFileName});
			this.lsvFileName.Location = new System.Drawing.Point(14, 70);
			this.lsvFileName.Name = "lsvFileName";
			this.lsvFileName.Size = new System.Drawing.Size(347, 256);
			this.lsvFileName.TabIndex = 2;
			this.lsvFileName.View = System.Windows.Forms.View.Details;
			this.lsvFileName.DoubleClick += new System.EventHandler(this.lsvFileName_DoubleClick);
			// 
			// clmFileName
			// 
			this.clmFileName.Text = "�ļ���";
			this.clmFileName.Width = 305;
			// 
			// dtpCreateDate
			// 
			this.dtpCreateDate.Location = new System.Drawing.Point(147, 29);
			this.dtpCreateDate.Name = "dtpCreateDate";
			this.dtpCreateDate.Size = new System.Drawing.Size(192, 23);
			this.dtpCreateDate.TabIndex = 1;
			this.dtpCreateDate.CloseUp += new System.EventHandler(this.dtpCreateDate_CloseUp);
			// 
			// lblTitle
			// 
			this.lblTitle.Location = new System.Drawing.Point(16, 32);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(104, 23);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "�ļ�����ʱ��:";
			// 
			// btn_Sure
			// 
			this.btn_Sure.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_Sure.Location = new System.Drawing.Point(192, 336);
			this.btn_Sure.Name = "btn_Sure";
			this.btn_Sure.Size = new System.Drawing.Size(75, 25);
			this.btn_Sure.TabIndex = 3;
			this.btn_Sure.Text = "ȷ ��";
			this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Location = new System.Drawing.Point(280, 336);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 25);
			this.btn_Cancel.TabIndex = 4;
			this.btn_Cancel.Text = "ȡ ��";
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// frmSDS7300
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(376, 381);
			this.Controls.Add(this.gpbMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmSDS7300";
			this.Text = "ѡ��SDS7300�����ļ�";
			this.Load += new System.EventHandler(this.frmSDS7300_Load);
			this.gpbMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region xing.chen 7/21/2005	����ѡ�����ʾ�����������ļ��б�
		/// <summary>
		/// ����ѡ�����ʾ�����������ļ��б�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dtpCreateDate_CloseUp(object sender, System.EventArgs e)
		{
			this.lsvFileName.Items.Clear();

			ArrayList arlFileNames = this.m_mthFindFile(DateTime.Parse(this.dtpCreateDate.Text));

			foreach(string s in arlFileNames)
			{	
				string[] strTemp = s.Split('\\');
				this.lsvFileName.Items.Add(new ListViewItem(strTemp[strTemp.Length-1]));
			}
		}
		#endregion

		#region xing.chen	7/21/2005	��·�����ļ�����ʱ������ļ�����
		/// <summary>
		/// ��·�����ļ�����ʱ������ļ�����
		/// </summary>
		/// <param name="p_CreateDate">�ļ�����ʱ��</param>
		/// <returns>�ļ����Ƽ���</returns>
		public ArrayList m_mthFindFile(DateTime p_CreateDate)
		{
			string path = m_strFilePath;	//�����ļ����·��

			ArrayList arlFileNames = new ArrayList();

			if(!Directory.Exists(path))
			{
				MessageBox.Show("·��������,��ע��!");
			}
			else
			{
				string[] strFileNames = Directory.GetFileSystemEntries(path,"*.csv");	//����·�������к�׺��Ϊcsv���ļ�
				for(int i=0;i<strFileNames.Length;i++)
				{
					FileInfo fi = new FileInfo(strFileNames[i]);
					if(fi.CreationTimeUtc.ToShortDateString() == p_CreateDate.ToShortDateString())	//�ж��ļ��Ĵ���������ѡ��������Ƿ������
					{
						arlFileNames.Add(strFileNames[i]);
					}
				}
			}

			return arlFileNames;
		}
		#endregion

		#region xing.chen 7/21/2005	����load�³�ʼ��listview������
		/// <summary>
		/// ����load�³�ʼ��listview������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmSDS7300_Load(object sender, System.EventArgs e)
		{
			this.lsvFileName.Items.Clear();
			ArrayList arlFileNames = this.m_mthFindFile(DateTime.Now);
			foreach(string s in arlFileNames)
			{	
				string[] strTemp = s.Split('\\');
				this.lsvFileName.Items.Add(new ListViewItem(strTemp[strTemp.Length-1]));
			}
		}
		#endregion

		#region xing.chen 7/22/2005	˫��ѡ���ļ�ʱ�Ĵ���(����ѡ���ļ�����)
		/// <summary>
		/// ˫��ѡ���ļ�ʱ�Ĵ���(����ѡ���ļ�����)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lsvFileName_DoubleClick(object sender, System.EventArgs e)
		{
			try
			{
				string path = this.m_strFilePath + "\\" + this.lsvFileName.SelectedItems[0].Text;
				FileInfo fi = new FileInfo(path);
				StreamReader sr = fi.OpenText();
				m_strFileData = sr.ReadToEnd();
				sr.Close();
				this.DialogResult = DialogResult.OK;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
//			this.Close();
		}

		//����ѡ���ļ�����
		public string m_mthSendData()
		{
			return m_strFileData;
		}
		#endregion

		private void btn_Sure_Click(object sender, System.EventArgs e)
		{
			try
			{
				string path = this.m_strFilePath + "\\" + this.lsvFileName.SelectedItems[0].Text;
				FileInfo fi = new FileInfo(path);
				StreamReader sr = fi.OpenText();
				m_strFileData = sr.ReadToEnd();
				sr.Close();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private void btn_Cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
