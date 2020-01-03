using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Drawing;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlChargeCat ��ժҪ˵����
	/// </summary>
	public class clsControlUsage:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlUsage()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain=new clsDomainControl_ChargeItem();
		}
		clsDomainControl_ChargeItem clsDomain;
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmUsage m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmUsage)frmMDI_Child_Base_in;
		}
		#endregion

        /// <summary>
        /// ���ñ���ɫ
        /// </summary>
        private Color gmc = Color.FromArgb(255, 255, 255);

        /// <summary>
        /// ��ҩ����ɫ
        /// </summary>
        private Color wmc = Color.FromArgb(105, 204, 184);

        /// <summary>
        /// ��ҩ����ɫ
        /// </summary>
        private Color cmc = Color.FromArgb(240, 194, 102);

        private Color br = new Color();

		#region ��ȡ�շ���Ŀ����б�
		public void m_GetUsage()
		{
			m_objViewer.m_lvw.Items.Clear();
			clsUsageType_VO[] objResult;
			long lngRes=clsDomain.m_lngGetUsage(out objResult,"");
			if(lngRes>0 && objResult.Length>0)
			{				
                string scope = "";                
				for(int i1=0;i1<objResult.Length;i1++)
				{
                    switch(objResult[i1].m_intScope)
                    {
                        case 0:
                            scope = "������";
                            br = gmc;
                            break;
                        case 1:
                            scope = "��ҩ��";
                            br = wmc;
                            break;
                        case 2:
                            scope = "��ҩ��";
                            br = cmc;
                            break;                        
                    }

                    ListViewItem lvw = new ListViewItem(scope);                    
					lvw.SubItems.Add(objResult[i1].m_strUsageCode);
					lvw.SubItems.Add(objResult[i1].m_strUsageName);
					lvw.SubItems.Add(objResult[i1].m_strUsagePYCODE);
					lvw.SubItems.Add(objResult[i1].m_strUsageWBCODE);
                    if (objResult[i1].m_intPutMed == 0)
                    {
                        lvw.SubItems.Add("ע��");
                    }
                    else
                    {
                        lvw.SubItems.Add("��ע��");
                    }
                    if (objResult[i1].m_intTest == 0)
                    {
                        lvw.SubItems.Add("��Ƥ��");
                    }
                    else
                    {
                        lvw.SubItems.Add("Ƥ��");
                    }
                    lvw.SubItems.Add(objResult[i1].m_strOPUsageDesc);
					lvw.Tag=objResult[i1].m_strUsageID;
                    lvw.BackColor = br;
					m_objViewer.m_lvw.Items.Add(lvw);
				}
			}
			if(m_objViewer.m_lvw.Items.Count>0)
			   m_objViewer.m_lvw.Items[0].Selected=true;
		}
		#endregion

		#region ����
		public void m_lngSave()
		{
			if(m_objViewer.m_txtCode.Text.Trim()=="")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtCode);
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
				m_objViewer.m_txtCode.Focus();
				return;
			}

			if(m_objViewer.m_txtName.Text.Trim()=="")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
				m_objViewer.m_txtName.Focus();
				return;
			}

            if (this.m_objViewer.cboScope.Text.Trim() == "")
            {                
                return;
            }

			long lngRes=0;
			string strID="";
			clsUsageType_VO objResult=new clsUsageType_VO();
			if(m_objViewer.m_txtCode.Text.Trim().Length==1)
			    objResult.m_strUsageCode="0"+m_objViewer.m_txtCode.Text;
			else 
			    objResult.m_strUsageCode=m_objViewer.m_txtCode.Text.Trim();

			objResult.m_strUsageName=m_objViewer.m_txtName.Text.Trim(); 
			objResult.m_strUsagePYCODE=m_objViewer.m_txtPYCODE.Text.Trim();
			objResult.m_strUsageWBCODE=m_objViewer.m_txtWBCODE.Text.Trim();
            objResult.m_intPutMed = m_objViewer.m_cboPutMed_INT.SelectedIndex;
            objResult.m_intScope = m_objViewer.cboScope.SelectedIndex;
            objResult.m_intTest = m_objViewer.m_cboTest.SelectedIndex;
            objResult.m_strOPUsageDesc = this.m_objViewer.m_txtOPUsageDesc.Text;
            string scope = "";
            switch (objResult.m_intScope)
            {
                case 0:
                    scope = "������";
                    br = gmc;
                    break;
                case 1:
                    scope = "��ҩ��";
                    br = wmc;
                    break;
                case 2:
                    scope = "��ҩ��";
                    br = cmc;
                    break;
            }

			if(m_objViewer.m_txtName.Tag==null) //����
			{
				for(int i=0;i<m_objViewer.m_lvw.Items.Count;i++)
				{

					if(int.Parse(m_objViewer.m_lvw.Items[i].SubItems[1].Text)==int.Parse(m_objViewer.m_txtCode.Text))
					{
						MessageBox.Show("���������Ѵ��ڣ�","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtCode);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();

						m_objViewer.m_txtCode.Focus();
						m_objViewer.m_txtCode.SelectAll();
						
						return;
					}	
				
				}
 
				lngRes=clsDomain.m_lngAddUsage(objResult,out strID);
				int index=m_objViewer.m_lvw.Items.Count;
				if(lngRes>0)
				{                    
                    ListViewItem lvw = new ListViewItem(scope);	
				    lvw.SubItems.Add(objResult.m_strUsageCode);
				    lvw.SubItems.Add(objResult.m_strUsageName);
					lvw.SubItems.Add(objResult.m_strUsagePYCODE);
					lvw.SubItems.Add(objResult.m_strUsageWBCODE);
                    lvw.SubItems.Add(objResult.m_intPutMed == 0 ? "ע��" : "��ע��");
                    lvw.SubItems.Add(objResult.m_intTest == 0 ? "��Ƥ��" : "Ƥ��");
                    lvw.SubItems.Add(objResult.m_strOPUsageDesc);
				    lvw.Tag=strID;
                    lvw.BackColor = br;
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
					if(int.Parse(m_objViewer.m_lvw.Items[i].SubItems[1].Text)==int.Parse(m_objViewer.m_txtCode.Text))
					{
						MessageBox.Show("���������Ѵ��ڣ�","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtCode);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();

						m_objViewer.m_txtCode.Focus();
						m_objViewer.m_txtCode.SelectAll();
						
						return;
					}	
				
				}

			    objResult.m_strUsageID=m_objViewer.m_txtName.Tag.ToString();
			    lngRes=clsDomain.m_lngDoUpdUsage(objResult);

				if(lngRes>0)
				{
                    m_objViewer.m_lvw.SelectedItems[0].BackColor = br;
                    m_objViewer.m_lvw.SelectedItems[0].SubItems[0].Text = scope;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[1].Text=objResult.m_strUsageCode;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[2].Text=objResult.m_strUsageName;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[3].Text=objResult.m_strUsagePYCODE;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[4].Text=objResult.m_strUsageWBCODE;
                    m_objViewer.m_lvw.SelectedItems[0].SubItems[5].Text = objResult.m_intPutMed == 0 ? "ע��" : "��ע��";
                    m_objViewer.m_lvw.SelectedItems[0].SubItems[6].Text = objResult.m_intTest == 0 ? "��Ƥ��" : "Ƥ��";
                    m_objViewer.m_lvw.SelectedItems[0].SubItems[7].Text = objResult.m_strOPUsageDesc;
                    MessageBox.Show("�޸ĳɹ���", "��ʾ");
				}
				else
					MessageBox.Show("�޸�ʧ�ܣ�","��ʾ");
			}

			m_objViewer.m_txtName.Text="";
			m_objViewer.m_txtCode.Text="";
			m_objViewer.m_txtPYCODE.Text="";
			m_objViewer.m_txtWBCODE.Text="";
            m_objViewer.m_txtOPUsageDesc.Clear();
            m_objViewer.m_cboTest.SelectedIndex = 0;
            m_objViewer.m_cboPutMed_INT.SelectedIndex = 0;
			m_objViewer.m_txtName.Tag=null;
			m_objViewer.m_txtCode.Focus();
		}
		#endregion
		#region ɾ����Ŀ
		public void m_Del()
		{
			if(m_objViewer.m_lvw.Items.Count==0 || m_objViewer.m_lvw.SelectedItems==null)
				return;
			if(m_objViewer.m_lvw.SelectedItems.Count<=0)
			{
				return;
			}
			if(m_objViewer.m_lvw.SelectedItems[0].Tag==null)
				return;
			if(MessageBox.Show("ȷ��ɾ��������","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2)==DialogResult.No)
				return;
			long lngRes=clsDomain.m_lngDelUsage(m_objViewer.m_lvw.SelectedItems[0].Tag.ToString());
			int index=m_objViewer.m_lvw.SelectedIndices[0];
			if(lngRes>0)
				m_objViewer.m_lvw.Items.Remove(m_objViewer.m_lvw.SelectedItems[0]);
			if(m_objViewer.m_lvw.Items.Count>0)
			{
				if(index>0)
					m_objViewer.m_lvw.Items[index-1].Selected=true;
				else
					m_objViewer.m_lvw.Items[index].Selected=true;
			}

		}
		#endregion

		#region �����÷�Code�����÷�
		public void m_FillTextByCode()
		{
           string strCode=m_objViewer.m_txtCode.Text.Trim();
		   if(strCode=="")
			   return;
			clsUsageType_VO[] objResult;
			long lngRes=clsDomain.m_lngGetUsageByCode(strCode,out objResult);
			if(lngRes<=0 || objResult.Length==0 || objResult[0].m_strUsageID==null)
				return;
            if(m_objViewer.m_txtName.Tag!=null)
			   if(m_objViewer.m_txtName.Tag.ToString()==objResult[0].m_strUsageID)
				  return;
			if(MessageBox.Show("�Ѵ�ñ��룬��ʾ��ϸ��Ϣ��","",MessageBoxButtons.YesNo)==DialogResult.No)
			{
				m_objViewer.m_txtCode.Focus();
				SendKeys.SendWait("{Esc}");
				return;
			}
			int i=clsMain.FindItemByValues(m_objViewer.m_lvw,0,objResult[0].m_strUsageCode);
			if(i>-1)
			{
				m_objViewer.m_lvw.SelectedItems[0].Selected=false;
				m_objViewer.m_lvw.Items[i].Selected=true;
			}
			else
			{
				m_objViewer.m_txtCode.Text=objResult[0].m_strUsageCode;
				m_objViewer.m_txtName.Text=objResult[0].m_strUsageName;
				m_objViewer.m_txtName.Tag=objResult[0].m_strUsageID;
			}
		}
		#endregion
	}
}
