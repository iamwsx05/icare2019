using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// �Һ�����
	///  �Ź���	 2004-9-22
	/// </summary>
	public class clsControlRegType:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlRegType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain = new clsDomainControl_RegDefine();
		}
			clsDomainControl_RegDefine clsDomain;
	


		#region ���ô������ �Ź���	 2004-9-22
		com.digitalwave.iCare.gui.HIS.frmRegType m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmRegType)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ȡ�Һ������б�  �Ź���	 2004-9-22
		public void m_GetRegType()
		{
			m_objViewer.m_lvw.Items.Clear();
			clsRegType_VO[] objResult;
			long lngRes = clsDomain.m_lngFindRegType(out objResult);
			if (lngRes > 0 && objResult.Length > 0)
			{
				ListViewItem lvw;
				for (int i1=0; i1 < objResult.Length; i1++)
				{
					lvw = new ListViewItem();
					lvw.SubItems.Add(objResult[i1].m_strRegTypeID);
					lvw.SubItems.Add(objResult[i1].m_strRegTypeName);
					lvw.SubItems.Add(objResult[i1].m_strRegTypeMemo);
					lvw.SubItems.Add(objResult[i1].m_strRegTypeNo);
					lvw.SubItems.Add(objResult[i1].m_strIsUsing);
					lvw.SubItems.Add(objResult[i1].m_strIsDoctor);
					if(objResult[i1].m_strIsUrgency=="1")
                    lvw.SubItems.Add("��");//xigui.peng���
					else
						lvw.SubItems.Add("��");
					lvw.Tag = objResult[i1].m_strRegTypeID;
					m_objViewer.m_lvw.Items.Add(lvw);
				}
			}
			if (m_objViewer.m_lvw.Items.Count > 0)
				m_objViewer.m_lvw.Items[0].Selected = true;
		}
		#endregion

		#region ����  �Ź���	 2004-9-22
		public void m_lngSave()
		{
			if(m_objViewer.m_txtName.Text.Trim()=="")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
				m_objViewer.m_txtName.Focus();
				return;
			}

			if(m_objViewer.m_txtREGISTERTYPENO_VCHR.Text.Trim()=="")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtREGISTERTYPENO_VCHR);
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
				m_objViewer.m_txtREGISTERTYPENO_VCHR.Focus();
				return;
			}
			long lngRes=0;
			string strID="";
			clsRegType_VO objResult = new clsRegType_VO();
			objResult.m_strRegTypeName = m_objViewer.m_txtName.Text;
			objResult.m_strRegTypeMemo = m_objViewer.m_txtMemo.Text;
			objResult.m_strRegTypeNo = m_objViewer.m_txtREGISTERTYPENO_VCHR.Text.Trim();
			objResult.m_strIsDoctor=this.GetFlag();
			objResult.m_strIsUrgency=this.GetFlag1();
			if(m_objViewer.m_txtName.Tag==null) //����
			{
				for(int i=0;i<m_objViewer.m_lvw.Items.Count;i++)
				{

					if(m_objViewer.m_lvw.Items[i].SubItems[2].Text.Trim()==m_objViewer.m_txtName.Text.Trim())
					{
						MessageBox.Show("�ùҺ������Ѵ��ڣ�","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();
						m_objViewer.m_txtName.Focus();
						m_objViewer.m_txtName.SelectAll();
						
						return;
					}	
				
				}
 
				lngRes=clsDomain.m_lngAddRegType(objResult,out strID);
				int index=m_objViewer.m_lvw.Items.Count;
				if(lngRes>0)
				{
				
					ListViewItem lvw=new ListViewItem();	
						lvw.SubItems.Add(strID);
						lvw.SubItems.Add(objResult.m_strRegTypeName);
						lvw.SubItems.Add(objResult.m_strRegTypeMemo);
						lvw.SubItems.Add(objResult.m_strRegTypeNo);
						lvw.SubItems.Add("1");
						lvw.SubItems.Add(objResult.m_strIsDoctor);
					 if(objResult.m_strIsUrgency=="1")
						lvw.SubItems.Add("��");     //xigui.peng���
					else
						lvw.SubItems.Add("��");
					  // lvw.SubItems.Add(objResult.m_strIsUrgency);//xigui.peng���

						lvw.Tag = objResult.m_strRegTypeID;
					m_objViewer.m_lvw.Items.Add(lvw);

					m_objViewer.m_lvw.Items[index].Selected=true;
					
					
				}
				else
					MessageBox.Show("����ʧ�ܣ�","��ʾ");
			}
			else
			{
				if(m_objViewer.m_lvw.SelectedItems.Count<=0)
				{
					return;
				}

						for(int i=0;i<m_objViewer.m_lvw.Items.Count;i++)
						{
							if (i==m_objViewer.m_lvw.SelectedItems[0].Index) continue;
							if(m_objViewer.m_lvw.Items[i].SubItems[2].Text.Trim()==m_objViewer.m_txtName.Text.Trim())
							{
								MessageBox.Show("�ùҺ������Ѵ��ڣ�","��ʾ");
								m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
								m_ephHandler.m_mthShowControlsErrorProvider();
								m_ephHandler.m_mthClearControl();
		
								m_objViewer.m_txtName.Focus();
								m_objViewer.m_txtName.SelectAll();
								
								return;
							}	
						
						}

				objResult.m_strRegTypeID = m_objViewer.m_txtName.Tag.ToString();
				lngRes=clsDomain.m_lngDoUpdRegByID(objResult);

				if(lngRes>0)
				{

					MessageBox.Show("�޸ĳɹ���","��ʾ");
					
							m_objViewer.m_lvw.SelectedItems[0].SubItems[2].Text = objResult.m_strRegTypeName;
							m_objViewer.m_lvw.SelectedItems[0].SubItems[3].Text = objResult.m_strRegTypeMemo;
							m_objViewer.m_lvw.SelectedItems[0].SubItems[4].Text =  objResult.m_strRegTypeNo;					
							m_objViewer.m_lvw.SelectedItems[0].SubItems[6].Text = objResult.m_strIsDoctor;
					
					       // m_objViewer.m_lvw.SelectedItems[0].SubItems[7].Text = objResult.m_strIsUrgency;//xigui.peng ���
					if(objResult.m_strIsUrgency=="1")
					{
						m_objViewer.m_lvw.SelectedItems[0].SubItems[7].Text = "��";//xigui.peng ���
					  
					}
					else

						m_objViewer.m_lvw.SelectedItems[0].SubItems[7].Text = "��";//xigui.peng ���
								
				}
				else
					MessageBox.Show("�޸�ʧ�ܣ�","��ʾ");
			}

		m_objViewer.m_txtName.Text = "";
		m_objViewer.m_txtMemo.Text = "";
		m_objViewer.m_txtREGISTERTYPENO_VCHR.Text = "";
		m_objViewer.m_txtName.Tag = null;
		m_objViewer.ra1.Checked = true;
		//m_objViewer.m_chkEmergency.Checked = false;//xigui.peng ���

		m_objViewer.m_txtName.Focus();
		}
		#endregion


		#region ɾ���Һ�����  �Ź���	 2004-9-22
		public void m_Delete()
		{
			if(m_objViewer.m_lvw.SelectedItems.Count<=0)
			{
				return;
			}
			if (m_objViewer.m_lvw.Items.Count == 0 || m_objViewer.m_lvw.SelectedItems == null)
				return;
			if (m_objViewer.m_lvw.SelectedItems[0].Tag == null)
				return;
			if(clsGetIsUsing.m_blGetIsUsingChargeType("REGISTERTYPEID_CHR",m_objViewer.m_lvw.SelectedItems[0].Tag.ToString())==true)
			{
				if(m_objViewer.m_btnStopUse.Tag.ToString() =="0")
				{
					if(MessageBox.Show("�ùҺ������ѱ����ã�����ɾ�����Ƿ�ͣ�ã�","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2) == DialogResult.No)
						return;
					m_mthIsUseing();
					return;
				}
				else if(m_objViewer.m_btnStopUse.Tag.ToString() =="1")
				{																														
					MessageBox.Show("�ùҺ������ѱ����ã�����ɾ����","��ʾ");
					return;
				}
			}

			if(MessageBox.Show("ȷ��ɾ��������","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2) == DialogResult.No)
				return;
			long lngRes = clsDomain.m_lngDelRegByID(m_objViewer.m_lvw.SelectedItems[0].Tag.ToString());
			int index = m_objViewer.m_lvw.SelectedIndices[0];
			if (lngRes > 0)
			{
				clsGetIsUsing.m_blDeleteDetail("REGISTERTYPEID_CHR",m_objViewer.m_lvw.SelectedItems[0].Tag.ToString());				
				m_objViewer.m_lvw.Items.Remove(m_objViewer.m_lvw.SelectedItems[0]);
			}
			if (m_objViewer.m_lvw.Items.Count > 0)
			{
				if(index > 0)
					m_objViewer.m_lvw.Items[index - 1].Selected = true;
				else
					m_objViewer.m_lvw.Items[index].Selected = true;
			}
		}
		#endregion

		#region �Ƿ�ͣ��  �Ź���	 2004-9-22
		public void m_mthIsUseing()
		{
			
			long lngRes = clsDomain.m_lngIsUseing(m_objViewer.m_lvw.SelectedItems[0].Tag.ToString(),m_objViewer.m_btnStopUse.Tag.ToString());
			if (lngRes > 0 )
			{
				if(m_objViewer.m_btnStopUse.Tag.ToString() =="0")
				{
					MessageBox.Show("ͣ�óɹ���");
					m_objViewer.m_lblIsStopUse.Text="��ͣ��";
					m_objViewer.m_btnStopUse.Text ="�ָ�";
					m_objViewer.m_lvw.SelectedItems[0].SubItems[5].Text = m_objViewer.m_btnStopUse.Tag.ToString();
					m_objViewer.m_btnStopUse.Tag = "1";
				}
				else if (m_objViewer.m_btnStopUse.Tag.ToString() =="1")
				{
					MessageBox.Show("�ָ��ɹ���");
					m_objViewer.m_lblIsStopUse.Text="����";
					m_objViewer.m_btnStopUse.Text ="ͣ��";
					m_objViewer.m_lvw.SelectedItems[0].SubItems[5].Text = m_objViewer.m_btnStopUse.Tag.ToString();
					m_objViewer.m_btnStopUse.Tag = "0";
				}
				

			}
			else
			{
				if(m_objViewer.m_btnStopUse.Tag.ToString() =="0")
					MessageBox.Show("ͣ��ʧ�ܣ�");
				else if (m_objViewer.m_btnStopUse.Tag.ToString() =="1")
					MessageBox.Show("�ָ�ʧ�ܣ�");
			}
			
		}
		#endregion

		public string GetFlag()
		{
			string strFlag="";
			if(m_objViewer.ra1.Checked)
				strFlag="0";
			else if (m_objViewer.ra2.Checked)
				strFlag="1";
			else if(m_objViewer.ra3.Checked)
				strFlag="2";
			return strFlag;
		}
		public string GetFlag1()//xigui.peng ���
		{
			string strFlag1="";
			if(m_objViewer.m_chkEmergency.Checked)
				strFlag1="1";
			else 
				strFlag1="0";
			
			return strFlag1;
		}

	}

}
