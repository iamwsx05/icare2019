using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// �Һ����
	/// �Ź���
	/// </summary>
	public class clsControlPatientPayType:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlPatientPayType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		
			clsDomain=new clsDomainControl_RegDefine();
		}
		clsDomainControl_RegDefine clsDomain;
		#region ���ô������	�Ź���	 2004-8-9
		com.digitalwave.iCare.gui.HIS.frmPatientPayType m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmPatientPayType)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ȡ�Һ�����б�	�Ź���		2004-8-9
		public void m_GetItemPatientPayType()
		{

			m_mthGetINSPLANataArr();
			m_objViewer.m_lvw.Items.Clear();
			clstPatientPaytype_VO[] objResult;
			long lngRes=clsDomain.m_lngFindPatientPayTypeList(out objResult);
			if(lngRes>0 && objResult.Length>0)
			{
				ListViewItem lvw;
				for(int i1=0;i1<objResult.Length;i1++)
				{
					lvw=new ListViewItem();
					lvw.SubItems.Add(objResult[i1].m_strPAYTYPENO_VCHR);
					lvw.SubItems.Add(objResult[i1].m_strPAYTYPENAME_VCHR);
					lvw.SubItems.Add(objResult[i1].m_strMEMO_VCHR);
					lvw.SubItems.Add(objResult[i1].m_strPAYLIMIT_MNY.ToString());
					if(objResult[i1].m_strPAYFLAG_DEC.Trim() == "0") 
						lvw.SubItems.Add("����");
					else if(objResult[i1].m_strPAYFLAG_DEC.Trim() == "1") 
						lvw.SubItems.Add("����");
					else if(objResult[i1].m_strPAYFLAG_DEC.Trim() == "2") 
						lvw.SubItems.Add("סԺ");
					else lvw.SubItems.Add("����");
					lvw.SubItems.Add(objResult[i1].m_strPAYPERCENT_DEC);
					lvw.SubItems.Add(objResult[i1].m_strISUSING_NUM);
					lvw.SubItems.Add(objResult[i1].m_strCHARGEPERCENT_DEC);
					lvw.SubItems.Add(objResult[i1].m_strCOPAYID_CHR);
					switch(objResult[i1].m_intINTERNALFLAG_INT)
					{
						case 0:
							lvw.SubItems.Add("��ͨ");
							break;
						case 1:
							lvw.SubItems.Add("����");
							break;
						case 2:
							lvw.SubItems.Add("ҽ��");
							break;
						case 3:
							lvw.SubItems.Add("����");
							break;
						case 4:
							lvw.SubItems.Add("����");
							break;
						case 5:
							lvw.SubItems.Add("��Ժ");
							break;
					}
					lvw.SubItems.Add(objResult[i1].m_strCOALITIONRECIPEFLAG_INT);
                    lvw.SubItems.Add(objResult[i1].m_strBIHLIMITRATE_DEC);
					lvw.Tag=objResult[i1].m_strPAYTYPEID_CHR;
					m_objViewer.m_lvw.Items.Add(lvw);
				}
			}

			if(m_objViewer.m_lvw.Items.Count>0)
				m_objViewer.m_lvw.Items[0].Selected=true;
		}
		#endregion

		#region ��ȡ���ռƻ��б�	�Ź���		2004-9-24
		/// <summary>
		/// ���ռƻ�
		/// </summary>
		public void m_mthGetINSPLANataArr()
		{
			clsInsPlan_VO[] objResult;
			long lngRes=clsDomain.m_lngGetINSPLANDataArr(out objResult);
			if(lngRes>0 && objResult.Length>0)
			{
				
				for(int i1=0;i1<objResult.Length;i1++)
				{
					m_objViewer.m_cboCOPAYID_CHR.m_intAddItem(objResult[i1].m_strPLANID_CHR,objResult[i1].m_strPLANNAME_CHR);
				}
			}
			
		}
		#endregion


		#region ����Һ����	�Ź���	 2004-8-9
		public void m_lngSavePatientPayType()
		{
            double db = Math.Ceiling(double.Parse(m_objViewer.textBoxTypedNumeric1.Text.Trim()));
            if (db.ToString().Length>8)
            {
                MessageBox.Show("סԺ�������������ֵ���󣡣�", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
			if(m_objViewer.m_txtPayMilite.Text.Trim()=="")
			{
				m_objViewer.m_txtPayMilite.Text ="0";
			}
		
			if(m_objViewer.m_txtPAYPERCENT_DEC.Text.Trim()=="")
			{
				m_objViewer.m_txtPAYPERCENT_DEC.Text ="0";
			}
			if(m_objViewer.m_txtCHARGEPERCENT_DEC.Text.Trim()=="" || m_objViewer.m_txtCHARGEPERCENT_DEC.Text.Trim()==null)
			{
				m_objViewer.m_txtCHARGEPERCENT_DEC.Text ="0";
			}
			if(Convert.ToDouble(m_objViewer.m_txtPayMilite.Text.Trim())>=1000000)
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtPayMilite);
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
				m_objViewer.m_txtPayMilite.Focus();
				m_objViewer.m_txtPayMilite.SelectAll();
				return;
			}
			if(Convert.ToDouble(m_objViewer.m_txtPAYPERCENT_DEC.Text.Trim())>100)
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtPAYPERCENT_DEC);
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
				m_objViewer.m_txtPAYPERCENT_DEC.Focus();
				m_objViewer.m_txtPAYPERCENT_DEC.SelectAll();
				return;
			}
			if(Convert.ToDouble(m_objViewer.m_txtCHARGEPERCENT_DEC.Text.Trim())>100)
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtCHARGEPERCENT_DEC);
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
				m_objViewer.m_txtCHARGEPERCENT_DEC.Focus();
				m_objViewer.m_txtCHARGEPERCENT_DEC.SelectAll();
				return;
			}
						
			long lngRes=0;
			string strID="";		
			clstPatientPaytype_VO objResult=new clstPatientPaytype_VO();
			objResult.m_strPAYTYPENAME_VCHR=m_objViewer.m_txtName.Text.Trim();  
			objResult.m_strMEMO_VCHR=m_objViewer.m_txtMemo.Text.Trim(); 
			if(m_objViewer.checkBox1.Checked)
			{
				objResult.m_strCOALITIONRECIPEFLAG_INT ="1";
			}
			else
			{
				objResult.m_strCOALITIONRECIPEFLAG_INT ="0";
			}
			objResult.m_strPAYLIMIT_MNY=m_objViewer.m_txtPayMilite.Text.Trim(); 
			if(m_objViewer.m_cboPAYFLAG_DEC.Text.Trim() == "����") 
			objResult.m_strPAYFLAG_DEC="0";
			else if(m_objViewer.m_cboPAYFLAG_DEC.Text.Trim() == "����") 
				objResult.m_strPAYFLAG_DEC="1";
			else if(m_objViewer.m_cboPAYFLAG_DEC.Text.Trim() == "סԺ") 
				objResult.m_strPAYFLAG_DEC="2";
			else objResult.m_strPAYFLAG_DEC="0";
			switch(this.m_objViewer.m_cobType.Text.Trim())
			{
				case "��ͨ":
				objResult.m_intINTERNALFLAG_INT=0;
					break;
				case "����":
					objResult.m_intINTERNALFLAG_INT=1;
					break;
				case "ҽ��":
					objResult.m_intINTERNALFLAG_INT=2;
					break;
				case "����":
					objResult.m_intINTERNALFLAG_INT=3;
					break;
				case "����":
					objResult.m_intINTERNALFLAG_INT=4;
					break;
				case "��Ժ":
					objResult.m_intINTERNALFLAG_INT=5;
					break;
			}
			objResult.m_strPAYPERCENT_DEC=m_objViewer.m_txtPAYPERCENT_DEC.Text.Trim(); 
			objResult.m_strPAYTYPENO_VCHR=m_objViewer.m_txtPAYTYPENO_VCHR.Text.Trim(); 
			objResult.m_strCHARGEPERCENT_DEC=m_objViewer.m_txtCHARGEPERCENT_DEC.Text.Trim(); 
			objResult.m_strCOPAYID_CHR=m_objViewer.m_cboCOPAYID_CHR.m_strGetID(m_objViewer.m_cboCOPAYID_CHR.SelectedIndex);
            objResult.m_strBIHLIMITRATE_DEC = m_objViewer.textBoxTypedNumeric1.Text.Trim(); 
			if(m_objViewer.m_txtName.Tag==null) //����
			{
				for(int i=0;i<m_objViewer.m_lvw.Items.Count;i++)
				{

					if(m_objViewer.m_lvw.Items[i].SubItems[2].Text.Trim()==m_objViewer.m_txtName.Text.Trim())
					{
						MessageBox.Show("����������Ѵ��ڣ�","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();
						m_objViewer.m_txtName.Focus();
						m_objViewer.m_txtName.SelectAll();
						return;
					}	

					if(m_objViewer.m_lvw.Items[i].SubItems[1].Text.Trim()==m_objViewer.m_txtPAYTYPENO_VCHR.Text.Trim())
					{
						MessageBox.Show("����ݱ���Ѵ��ڣ�","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtPAYTYPENO_VCHR);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();
						m_objViewer.m_txtPAYTYPENO_VCHR.Focus();
						m_objViewer.m_txtPAYTYPENO_VCHR.SelectAll();
						return;
					}	
				
				}

				lngRes=clsDomain.m_lngAddPatientPayType(objResult,out strID);
				int index=m_objViewer.m_lvw.Items.Count;
				if(lngRes>0)
				{
					//MessageBox.Show("����ɹ���","��ʾ");
					ListViewItem lvw=new ListViewItem();
					lvw.SubItems.Add(objResult.m_strPAYTYPENO_VCHR);
					lvw.SubItems.Add(objResult.m_strPAYTYPENAME_VCHR);
					lvw.SubItems.Add(objResult.m_strMEMO_VCHR);
					lvw.SubItems.Add(objResult.m_strPAYLIMIT_MNY);
					lvw.SubItems.Add(m_objViewer.m_cboPAYFLAG_DEC.Text);
					lvw.SubItems.Add(objResult.m_strPAYPERCENT_DEC);
					lvw.SubItems.Add("1");
					lvw.SubItems.Add(objResult.m_strCHARGEPERCENT_DEC);
					lvw.SubItems.Add(m_objViewer.m_cboCOPAYID_CHR.Text.Trim());
					lvw.SubItems.Add(this.m_objViewer.m_cobType.Text.Trim());
					if(this.m_objViewer.checkBox1.Checked)
					{
					lvw.SubItems.Add("1");
					}
					else
					{
					lvw.SubItems.Add("0");
					}
                    lvw.SubItems.Add(objResult.m_strBIHLIMITRATE_DEC);
					lvw.Tag=strID;
					m_objViewer.m_lvw.Items.Add(lvw);
					m_objViewer.m_lvw.Items[index].Selected=true;
					
				}
				else
					MessageBox.Show("����ʧ�ܣ�","��ʾ");
			}
			else //�޸�
			{
				if(m_objViewer.m_lvw.SelectedItems.Count<=0)
				{
					return;
				}
				for(int i=0;i<m_objViewer.m_lvw.Items.Count;i++)
				{
					if (i==m_objViewer.m_lvw.SelectedItems[0].Index) continue;
					if(m_objViewer.m_lvw.Items[i].SubItems[1].Text.Trim()==m_objViewer.m_txtPAYTYPENO_VCHR.Text.Trim())
					{
						MessageBox.Show("����ݱ���Ѵ��ڣ�","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtPAYTYPENO_VCHR);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();
		
						m_objViewer.m_txtPAYTYPENO_VCHR.Focus();
						m_objViewer.m_txtPAYTYPENO_VCHR.SelectAll();
								
						return;
					}	

					if(m_objViewer.m_lvw.Items[i].SubItems[2].Text.Trim()==m_objViewer.m_txtName.Text.Trim())
					{
						MessageBox.Show("����������Ѵ��ڣ�","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();
		
						m_objViewer.m_txtName.Focus();
						m_objViewer.m_txtName.SelectAll();
								
						return;
					}	
						
				}

				objResult.m_strPAYTYPEID_CHR=m_objViewer.m_txtName.Tag.ToString();
				lngRes=clsDomain.m_lngDoUpdPatientPayTypeByID(objResult);
				if(lngRes>0)
				{

					MessageBox.Show("�޸ĳɹ���","��ʾ");
					m_objViewer.m_lvw.SelectedItems[0].SubItems[1].Text=objResult.m_strPAYTYPENO_VCHR;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[2].Text=objResult.m_strPAYTYPENAME_VCHR;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[3].Text=objResult.m_strMEMO_VCHR;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[4].Text=objResult.m_strPAYLIMIT_MNY;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[5].Text=m_objViewer.m_cboPAYFLAG_DEC.Text;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[6].Text=objResult.m_strPAYPERCENT_DEC;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[8].Text=objResult.m_strCHARGEPERCENT_DEC;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[9].Text=m_objViewer.m_cboCOPAYID_CHR.Text.Trim();
					m_objViewer.m_lvw.SelectedItems[0].SubItems[10].Text=this.m_objViewer.m_cobType.Text.Trim();
					if(this.m_objViewer.checkBox1.Checked)
					{
						m_objViewer.m_lvw.SelectedItems[0].SubItems[11].Text="1";
					}
					else
					{
						m_objViewer.m_lvw.SelectedItems[0].SubItems[11].Text="0";
					}
                    m_objViewer.m_lvw.SelectedItems[0].SubItems[12].Text = objResult.m_strBIHLIMITRATE_DEC;
				}
				else
					MessageBox.Show("�޸�ʧ�ܣ�","��ʾ");
			}
			m_objViewer.m_txtName.Text="";
			m_objViewer.m_txtMemo.Text="";
			m_objViewer.m_cboPAYFLAG_DEC.Text = "�Է�";
			m_objViewer.m_txtPAYPERCENT_DEC.Text="";
			m_objViewer.m_txtPAYTYPENO_VCHR.Text="";
			m_objViewer.m_txtCHARGEPERCENT_DEC.Text = "";
            m_objViewer.textBoxTypedNumeric1.Text = "";
			m_objViewer.m_cobType.Text = "";
			m_objViewer.m_txtPayMilite.Text =null;
			m_objViewer.m_txtName.Tag=null;
			m_objViewer.m_txtName.Focus();
		}
		#endregion

		#region �Ƿ�ͣ��   �Ź���	 2004-9-22
		public void m_mthIsUseing()
		{
			
			long lngRes = clsDomain.m_lngIsUseingPAYTYPE(m_objViewer.m_lvw.SelectedItems[0].Tag.ToString(),m_objViewer.m_btnStopUse.Tag.ToString());
			if (lngRes > 0 )
			{
				if(m_objViewer.m_btnStopUse.Tag.ToString() =="0")
				{
					MessageBox.Show("ͣ�óɹ���");
					m_objViewer.m_lblIsStopUse.Text="��ͣ��";
					m_objViewer.m_btnStopUse.Text ="�ָ�";
					m_objViewer.m_lvw.SelectedItems[0].SubItems[7].Text = m_objViewer.m_btnStopUse.Tag.ToString();
					m_objViewer.m_btnStopUse.Tag = "1";
				}
				else if (m_objViewer.m_btnStopUse.Tag.ToString() =="1")
				{
					MessageBox.Show("�ָ��ɹ���");
					m_objViewer.m_lblIsStopUse.Text="����";
					m_objViewer.m_btnStopUse.Text ="ͣ��";
					m_objViewer.m_lvw.SelectedItems[0].SubItems[7].Text = m_objViewer.m_btnStopUse.Tag.ToString();
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

		#region ɾ���Һ����	�Ź���	 2004-8-9
		public void m_DelPatientPayType()
		{
			if(m_objViewer.m_lvw.Items.Count==0 || m_objViewer.m_lvw.SelectedItems==null)
				return;
			if(m_objViewer.m_lvw.SelectedItems.Count<=0)
			{
				return;
			}
			if(m_objViewer.m_lvw.SelectedItems[0].Tag==null)
				return;

			if(clsGetIsUsing.m_blGetIsUsingChargeType("PAYTYPEID_CHR",m_objViewer.m_lvw.SelectedItems[0].Tag.ToString())==true)
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

			if(MessageBox.Show("ȷ��ɾ��������","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2)==DialogResult.No)
				return;
			long lngRes=clsDomain.m_lngDelTPatientPayTypeByID(m_objViewer.m_lvw.SelectedItems[0].Tag.ToString());
			int index=m_objViewer.m_lvw.SelectedIndices[0];
			if(lngRes>0)
			{
				clsGetIsUsing.m_blDeleteDetail("PAYTYPEID_CHR",m_objViewer.m_lvw.SelectedItems[0].Tag.ToString());	
				m_objViewer.m_lvw.Items.Remove(m_objViewer.m_lvw.SelectedItems[0]);
			}
			if(m_objViewer.m_lvw.Items.Count>0)
			{
				if(index>0)
					m_objViewer.m_lvw.Items[index-1].Selected=true;
				else
					m_objViewer.m_lvw.Items[index].Selected=true;
			}
		}
		#endregion
	}
}
