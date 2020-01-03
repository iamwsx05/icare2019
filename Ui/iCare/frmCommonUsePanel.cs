using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using com.digitalwave.emr.DigitalSign;//����ǩ��
namespace iCare
{
	public class frmCommonUsePanel : iCare.frmPrimaryForm
		//	public class frmCommonUsePanel :System.Windows.Forms
	{
		private System.Windows.Forms.ColumnHeader m_clmCommonUseValue;
		private System.Windows.Forms.ListView m_lsvItemList;
		private System.Windows.Forms.ColumnHeader mclmCommonUseValueID;
		private System.ComponentModel.IContainer components = null;

		public frmCommonUsePanel()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
            //m_objBorderTool.m_mthChangedControlBorder(m_lsvItemList);

			if(MDIParent.s_bolIAnaSystem)
			{
				m_lsvItemList.ForeColor = Color.Black;
			}
		}
		/// <summary>
		/// ����ֵ����
		/// </summary>
		private int m_intType;
		/// <summary>
		/// ������
		/// </summary>
		private Form m_objParentForm;
		/// <summary>
		/// Ҫ����Ŀؼ�
		/// </summary>
		private Control m_objSelectedControl;
		

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
				m_objParentForm = null;
				m_objSelectedControl = null;
			}
			base.Dispose( disposing );
			
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_lsvItemList = new System.Windows.Forms.ListView();
			this.mclmCommonUseValueID = new System.Windows.Forms.ColumnHeader();
			this.m_clmCommonUseValue = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// m_lsvItemList
			// 
			this.m_lsvItemList.BackColor = System.Drawing.Color.White;
			this.m_lsvItemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.mclmCommonUseValueID,
																							this.m_clmCommonUseValue});
			this.m_lsvItemList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lsvItemList.ForeColor = System.Drawing.Color.Black;
			this.m_lsvItemList.FullRowSelect = true;
			this.m_lsvItemList.GridLines = true;
			this.m_lsvItemList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.m_lsvItemList.HideSelection = false;
			this.m_lsvItemList.Location = new System.Drawing.Point(0, 0);
			this.m_lsvItemList.MultiSelect = false;
			this.m_lsvItemList.Name = "m_lsvItemList";
			this.m_lsvItemList.Size = new System.Drawing.Size(304, 221);
			this.m_lsvItemList.TabIndex = 413;
			this.m_lsvItemList.View = System.Windows.Forms.View.Details;
			this.m_lsvItemList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvItemList_KeyDown);
			this.m_lsvItemList.DoubleClick += new System.EventHandler(this.m_lsvItemList_DoubleClick);
			// 
			// mclmCommonUseValueID
			// 
			this.mclmCommonUseValueID.Width = 0;
			// 
			// m_clmCommonUseValue
			// 
			this.m_clmCommonUseValue.Text = "";
			this.m_clmCommonUseValue.Width = 290;
			// 
			// frmCommonUsePanel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(304, 221);
			this.Controls.Add(this.m_lsvItemList);
			this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmCommonUsePanel";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "����ֵ";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCommonUsePanel_KeyDown);
			this.Load += new System.EventHandler(this.frmCommonUsePanel_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// �ж��Ƿ�������ϵͳ���ô���
		/// </summary>
		private bool m_blnIsAnaSystem = false;

		public bool M_blnIsAnaSystem
		{
			set{m_blnIsAnaSystem = value;}
		}

		/// <summary>
		/// ����CommonUserType
		/// </summary>
		/// <param name="p_intType">����ֵ����</param>
		public void m_mthSetCommonUserType(int p_intType)
		{
			m_intType = p_intType;
		}
		/// <summary>
		/// ����ID�ֶ�
		/// </summary>
		private string m_strDeptID;
		/// <summary>
		/// ����ID����
		/// </summary>
		public string m_StrDeptID
		{
			set 
			{
				m_strDeptID = value;
			}
		}

		/// <summary>
		/// �Ƿ���Ҫ��֤�ֶ�
		/// </summary>
		private bool m_blnNeedVerify = true;
		/// <summary>
		/// �Ƿ���Ҫ������֤����
		/// </summary>
		public bool m_BlnNeedVerify
		{
			get
			{
				return m_blnNeedVerify;
			}
			set
			{
				m_blnNeedVerify = value;
			}
		}

		/// <summary>
		/// ���õ��ô���
		/// </summary>
		/// <param name="p_objParentForm"></param>
		public void m_mthSetParentForm(Form p_objParentForm,Control p_objSelectedControl)
		{
			m_objParentForm = p_objParentForm;
			m_objSelectedControl = p_objSelectedControl;
		}

		/// <summary>
		/// ���Ա�����б�
		/// </summary>
		private void m_mthAddEmployeesToList(clsDocAndNur[] p_objEmployeeArr)
		{
			if(p_objEmployeeArr != null && p_objEmployeeArr.Length > 0)
			{
				ListViewItem lviNew;
				for(int i = 0;i < p_objEmployeeArr.Length;i++)
				{
					lviNew = m_lsvItemList.Items.Add(p_objEmployeeArr[i].m_strEmployeeID);
					lviNew.SubItems.Add(p_objEmployeeArr[i].m_strEmployeeName.TrimEnd());
				}
				m_lsvItemList.Focus();
				m_lsvItemList.Items[0].Selected=true;
				m_lsvItemList.Items[0].Focused=true;
			}
		}
		/// <summary>
		/// ���������¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmCommonUsePanel_Load(object sender, System.EventArgs e)
		{
			try
			{
				switch(m_intType)
				{
					case -1 ://ҽ��ǩ������ֵ
						m_mthAddEmployeesToList(new clsManageDocAndNurDomain().m_objGetSpecialEmployeeInDept(1));
						break;
					case -2 ://��ʿǩ������ֵ
						m_mthAddEmployeesToList(new clsManageDocAndNurDomain().m_objGetSpecialEmployeeInDept(2));
						break;
					case -3 ://�ض�����ҽ��ǩ������ֵ
						m_mthAddEmployeesToList(new clsManageDocAndNurDomain().m_objGetSpecialEmployeeInDept(1,m_strDeptID));
						//					m_mthAddEmployeesToList(new clsManageDocAndNurDomain().m_objGetSpecialEmployeeInDept(1));
						break;
					case -4 ://�ض�����������Ա��
						//					m_mthAddEmployeesToList(new clsManageDocAndNurDomain().m_objGetSpecialEmployeeInDept(0));
						m_mthAddEmployeesToList(new clsManageDocAndNurDomain().m_objGetSpecialEmployeeInDept(0,m_strDeptID));
						break;
					default ://һ�㳣��ֵ
						clsCommonUseValue[] objclsCommonUseValue=null;			
						//new clsCommonUseDomain().m_lngGetAllCommonUseValue(m_intType.ToString(),out objclsCommonUseValue);
						if (m_blnIsAnaSystem)
							new clsCommonUseDomain().m_lngGetAllCommonUseValue(m_intType.ToString(),out objclsCommonUseValue,m_blnIsAnaSystem);
						else
							new clsCommonUseDomain().m_lngGetAllCommonUseValue(m_intType.ToString(),out objclsCommonUseValue);

						if(objclsCommonUseValue!=null)
						{
							ListViewItem lviNew;
							for(int i=0;i<objclsCommonUseValue.Length;i++)
							{
								lviNew = m_lsvItemList.Items.Add(objclsCommonUseValue[i].m_strValueIndex);
								lviNew.SubItems.Add(objclsCommonUseValue[i].m_strCommonUseValue.TrimEnd());
							}
						}
						if( m_lsvItemList.Items.Count>0)
						{
							m_lsvItemList.Focus();
							m_lsvItemList.Items[0].Selected=true;
							m_lsvItemList.Items[0].Focused=true;
						}
						break;
				}		
			}
			catch (Exception ex)
			{
				
			}
			
		}
		/// <summary>
		/// ѡ��˫���¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_lsvItemList_DoubleClick(object sender, System.EventArgs e)
		{
			if(m_lsvItemList.Items.Count>0 && m_lsvItemList.SelectedItems.Count > 0)
			{
//				if(m_BlnNeedVerify)
//				{
//					//					if(!m_blnCheckEmployeeSign(m_lsvItemList.SelectedItems[0].SubItems[0].Text,m_lsvItemList.SelectedItems[0].SubItems[1].Text))
//					//						return;
//				}
				
				#region �����֤���� modify by tfzhang at 2005-12-6 13:11
				if(m_BlnNeedVerify)
				{
                    string strReturnSetting = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_intGetEmrSettingValue("3002").ToString();
					if (strReturnSetting!=null)
					{
						//������֤
						if (strReturnSetting=="0")
						{
							//continue;
						}
							//������֤
						else if (strReturnSetting=="1")
						{
							if(!m_blnCheckEmployeeSign(m_lsvItemList.SelectedItems[0].SubItems[0].Text,m_lsvItemList.SelectedItems[0].SubItems[1].Text))
								return;
						}
							//key����֤
						else if (strReturnSetting=="2")
						{
							if (!m_blnCheckEmployeeSignByKey(m_lsvItemList.SelectedItems[0].SubItems[0].Text,m_lsvItemList.SelectedItems[0].SubItems[1].Text))
								return;

						}
					
					}
				}
				#endregion

				switch(m_objSelectedControl.GetType().FullName)
				{
					case "com.digitalwave.Utility.Controls.ctlRichTextBox" :
						ctlRichTextBox txtFocusTextBox = (ctlRichTextBox)m_objSelectedControl;
						//						txtFocusTextBox.m_mthInsertText(m_lsvItemList.SelectedItems[0].SubItems[1].Text,txtFocusTextBox.Text.Length);
						if(m_intType==(int)enmCommonUseValue.Anaesthesia_Plane || txtFocusTextBox.Name == "m_txtAttendPeople")
						{
							try
							{
								if(txtFocusTextBox.Name == "m_txtAttendPeople")
								{
									string strPanle=m_lsvItemList.SelectedItems[0].SubItems[1].Text;
									if(txtFocusTextBox.Text.Trim()!="") strPanle=" " + strPanle;
									txtFocusTextBox.m_mthInsertText(strPanle,txtFocusTextBox.Text.Length);
								}
								else
								{
									string strPanle=m_lsvItemList.SelectedItems[0].SubItems[1].Text;
									if(txtFocusTextBox.Text.Trim()!="") strPanle="��" + strPanle;
									txtFocusTextBox.m_mthInsertText(strPanle,txtFocusTextBox.Text.Length);
							
								}
							}
							catch(Exception)
							{}
						}
						else
						{
							txtFocusTextBox.m_mthClearText();
							txtFocusTextBox.m_mthInsertText(m_lsvItemList.SelectedItems[0].SubItems[1].Text,0);
						}
						break;
					case "com.digitalwave.controls.ctlRichTextBox":
						com.digitalwave.controls.ctlRichTextBox txtFocusTextBox1 = (com.digitalwave.controls.ctlRichTextBox)m_objSelectedControl;
						
						if(m_intType==(int)enmCommonUseValue.Anaesthesia_Plane || txtFocusTextBox1.Name == "m_txtAttendPeople")
						{
							try
							{
								if(txtFocusTextBox1.Name == "m_txtAttendPeople")
								{
									string strPanle=m_lsvItemList.SelectedItems[0].SubItems[1].Text;
									if(txtFocusTextBox1.Text.Trim()!="") strPanle=" " + strPanle;
									txtFocusTextBox1.m_mthInsertText(strPanle,txtFocusTextBox1.Text.Length);
								}
								else
								{
									string strPanle=m_lsvItemList.SelectedItems[0].SubItems[1].Text;
									if(txtFocusTextBox1.Text.Trim()!="") strPanle="��" + strPanle;
									txtFocusTextBox1.m_mthInsertText(strPanle,txtFocusTextBox1.Text.Length);
							
								}
							}
							catch(Exception)
							{}
						}
						else
						{
							txtFocusTextBox1.m_mthClearText();
							txtFocusTextBox1.m_mthInsertText(m_lsvItemList.SelectedItems[0].SubItems[1].Text,0);
						}
						break;
					case "System.Windows.Forms.TextBox" :
						TextBox txt = (TextBox)m_objSelectedControl;
						txt.Text = m_lsvItemList.SelectedItems[0].SubItems[1].Text;
						break;
					case "com.digitalwave.Utility.Controls.ctlBorderTextBox" :
						ctlBorderTextBox txt2 = (ctlBorderTextBox)m_objSelectedControl;
						txt2.Text = m_lsvItemList.SelectedItems[0].SubItems[1].Text;
						break;
					case "System.Windows.Forms.ListView" :
						ListView lsv = (ListView)m_objSelectedControl;
						for(int i = 0;i < lsv.Items.Count;i++)
						{
							if(m_lsvItemList.SelectedItems[0].SubItems[1].Text==lsv.Items[i].SubItems[0].Text)//���Ա������ظ���Ϊ�Ա����ֶ�����ID����Ϊ������¼���Ļ�ʿ����û��ID
							{
								clsPublicFunction.ShowInformationMessageBox("�Բ���Ա�������ظ���������ѡ��");
								return;
							}
						}
						ListViewItem lviNewItem=lsv.Items.Add(m_lsvItemList.SelectedItems[0].SubItems[1].Text);
						lviNewItem.SubItems.Add(m_lsvItemList.SelectedItems[0].SubItems[0].Text);
						break;
				}
				if(m_intType < 0 && m_objSelectedControl.GetType().Name != "ListView")
				{
					string strFormName = m_objParentForm.Name;
					if(strFormName != "frmInHospitalMainRecord" && strFormName != "frmConsultation" && 
						strFormName != "frmCaseDiscuss" && strFormName != "frmDeadCaseDiscuss" &&
						strFormName != "frmSaveRecord" && strFormName != "frmOutHospital" &&
						strFormName != "frmBeforeOperationDiscuss" && strFormName != "frmCheckRoom" &&
						strFormName != "frmConvey"&& strFormName!="frmDeathRecord")
						
						m_objSelectedControl.Tag = new clsEmployee(m_lsvItemList.SelectedItems[0].SubItems[0].Text);
					else
						m_objSelectedControl.Tag = m_lsvItemList.SelectedItems[0].SubItems[0].Text;
				}

				this.Close();
			}
		}
		/// <summary>
		/// �������¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_lsvItemList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode.Equals(Keys.Enter))
				m_lsvItemList_DoubleClick(null,null);
		}

		/// <summary>
		/// ��֤Ա��ǩ��
		/// </summary>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_strEmployeeName"></param>
		/// <returns></returns>
		private bool m_blnCheckEmployeeSign(string p_strEmployeeID,string p_strEmployeeName)
		{
			frmCheckSign objCheck = new frmCheckSign(p_strEmployeeID,p_strEmployeeName);

			objCheck.ShowDialog(this);

			if(objCheck.m_LngRes > 0 && objCheck.m_BlnIsPass)
			{
				return true;
			}
			else if(objCheck.m_LngRes > 0 && !objCheck.m_BlnIsPass)
			{
				clsPublicFunction.ShowInformationMessageBox("��֤ʧ�ܣ���Ա������ǩ����");
				return false;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// key����֤ǩ����
		/// ������ѡ��ǩ����ʱ��ʹ��
		/// </summary>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_strEmployeeName"></param>
		/// <returns></returns>
		private bool m_blnCheckEmployeeSignByKey(string p_strEmployeeID,string p_strEmployeeName)
		{
			try
			{
				//��ȡ֤��
				clsDigitalSign objsign=new clsDigitalSign();
				
				//����ǩ��ʹ�������봰��
				string strContentTemp=null;
				strContentTemp=objsign.sign("1",0);
				if (strContentTemp==null)
				{
					MessageBox.Show("key��У��ʧ�ܣ���ȷ���Ѳ���key��!","iCare Message", MessageBoxButtons.OK,MessageBoxIcon.Information);
					return false;

				}
				//ʹ��key��¼
				clsEmrEmployeeBase_VO objEmp =null;
				com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmpDomain=new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
				long ret=0;
				ret=objEmpDomain.m_lngGetMinEmpByKey(objsign.currentCerts.m_strSerialNumber,out objEmp);
				if((ret>0) && (objEmp!=null))
				{
					if (objEmp.m_strEMPNO_CHR.Trim()==p_strEmployeeID.Trim())
					{
						return true;
					}
					else
					{
						MessageBox.Show("��⵽key��֤�����ƺ�ָ����ǩ���߲�һ�£�����ͨ����֤","iCare Message", MessageBoxButtons.OK,MessageBoxIcon.Information);
						return false;
					}
				}
				else
				{
					MessageBox.Show("�û������ڻ���δ����key�̵�¼,����!","iCare Message", MessageBoxButtons.OK,MessageBoxIcon.Information);
					return false;
				}
			}
			catch(Exception exp)
			{
				MessageBox.Show("δ�ܼ�⵽key��,ȷ���Ƿ����key��","iCare Message", MessageBoxButtons.OK,MessageBoxIcon.Information);
				return false;
			}
		}
	 
 		/// <summary>
		/// esc�رմ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmCommonUsePanel_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Escape:
					this.Close();
					break;
			}
		}
	}
}

