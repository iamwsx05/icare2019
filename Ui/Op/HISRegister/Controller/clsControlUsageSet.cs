using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS

{
	/// <summary>
	/// clsControlUsageSet ��ժҪ˵����
	/// </summary>
	public class clsControlUsageSet:com.digitalwave.GUI_Base.clsController_Base
	{
		clsDomainControl_ChargeItem clsDomain;
		public clsControlUsageSet()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain=new clsDomainControl_ChargeItem();
		}

		#region ���ô������	�Ź���	 2005-2-18
		com.digitalwave.iCare.gui.HIS.frmUsageSet m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmUsageSet)frmMDI_Child_Base_in;
		}
		#endregion

		#region  ��ȡ�÷���Ŀ
		public void m_GetUsageList()
		{
			clsUsageType_VO[] objResult = new clsUsageType_VO[0];
			long lngRes=clsDomain.m_lngGetUsage(out objResult,"");

			if(lngRes>0 && objResult.Length>0)
			{
				ListViewItem lvw;
				for(int i1=0;i1<objResult.Length;i1++)
				{
					lvw=new ListViewItem(objResult[i1].m_strUsageID);
					lvw.SubItems.Add(objResult[i1].m_strUsageCode);
					lvw.SubItems.Add(objResult[i1].m_strUsageName);
					m_objViewer.m_lvw.Items.Add(lvw);
				}
			}

		}
		#endregion

		#region  ��ȡ�÷���Ŀ�����б�
		public void m_GetSetUsageList()
		{
			clsUsageType_VO[] objResult = new clsUsageType_VO[0];
			long lngRes=clsDomain.m_lngGetUsageSet(out objResult);
			if(lngRes>0 && objResult.Length>0)
			{
				ListViewItem lvw;
				for(int i1=0;i1<objResult.Length;i1++)
				{
					lvw=new ListViewItem(objResult[i1].m_strUsageID);
					lvw.SubItems.Add(objResult[i1].m_strUsageCode);
					lvw.SubItems.Add(objResult[i1].m_strUsageName);
					if (objResult[i1].m_strUsageType =="1")
					{
						lvw.SubItems.Add("ע��");
						
					}
					else if(objResult[i1].m_strUsageType =="2")
						lvw.SubItems.Add("����");
					else if(objResult[i1].m_strUsageType =="3")
						lvw.SubItems.Add("����");
                    else if (objResult[i1].m_strUsageType == "4")
						lvw.SubItems.Add("��Ѫ");
                    else
						lvw.SubItems.Add("����");


					m_objViewer.m_lvw2.Items.Add(lvw);
				}
			}
		}

		#endregion

		#region ����ע������
		public void insertType1()
		 {
			for(int i1=0;i1<m_objViewer.m_lvw2.Items.Count;i1++)
			{
				if (this.m_objViewer.m_lvw2.Items[i1].SubItems[0].ToString().Trim() == this.m_objViewer.m_lvw.SelectedItems[0].SubItems[0].ToString().Trim())
				{
					MessageBox.Show("���÷���Ŀ�Ѵ��ڣ�","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
					m_objViewer.m_lvw2.Focus();
					m_objViewer.m_lvw2.Items[i1].Selected=true;
					return;
				}
			}
			long lngRes=0;
			if(m_objViewer.m_cmbType.SelectedIndex==0)
			{
				lngRes=clsDomain.m_lngDoAddNewUsageType(m_objViewer.m_lvw.SelectedItems[0].SubItems[0].Text.Trim(),"1");
			}
			if(m_objViewer.m_cmbType.SelectedIndex==1)
			{
				lngRes=clsDomain.m_lngDoAddNewUsageType(m_objViewer.m_lvw.SelectedItems[0].SubItems[0].Text.Trim(),"2");
			}
			if(m_objViewer.m_cmbType.SelectedIndex==2)
			{
				lngRes=clsDomain.m_lngDoAddNewUsageType(m_objViewer.m_lvw.SelectedItems[0].SubItems[0].Text.Trim(),"3");
			}
			if(m_objViewer.m_cmbType.SelectedIndex==3)
			{
				lngRes=clsDomain.m_lngDoAddNewUsageType(m_objViewer.m_lvw.SelectedItems[0].SubItems[0].Text.Trim(),"4");
			}
            if (m_objViewer.m_cmbType.SelectedIndex == 4)
            {
                lngRes = clsDomain.m_lngDoAddNewUsageType(m_objViewer.m_lvw.SelectedItems[0].SubItems[0].Text.Trim(), "5");
            }
				if(lngRes>0)
				{
				
					ListViewItem lvw;
					lvw=new ListViewItem(m_objViewer.m_lvw.SelectedItems[0].SubItems[0].Text.Trim());
					lvw.SubItems.Add(m_objViewer.m_lvw.SelectedItems[0].SubItems[1].Text.Trim());
					lvw.SubItems.Add(m_objViewer.m_lvw.SelectedItems[0].SubItems[2].Text.Trim());
					lvw.Tag = "";
					if(m_objViewer.m_cmbType.SelectedIndex==0)
					{
						lvw.SubItems.Add("ע��");
					}
					if(m_objViewer.m_cmbType.SelectedIndex==1)
					{
						lvw.SubItems.Add("����");
					}
					if(m_objViewer.m_cmbType.SelectedIndex==2)
					{
						lvw.SubItems.Add("����");
					}
					if(m_objViewer.m_cmbType.SelectedIndex==3)
					{
						lvw.SubItems.Add("��Ѫ");
					}
                    if (m_objViewer.m_cmbType.SelectedIndex == 4)
                    {
                        lvw.SubItems.Add("����");
                    }
					lvw.SubItems.Add("");

					m_objViewer.m_lvw2.Items.Add(lvw);
				}
		}
					
		 
		#endregion

		#region ɾ������Ŀ����
		public void m_delSet()
		{
			int index=m_objViewer.m_lvw2.SelectedIndices[0];
			if(MessageBox.Show("ȷ��ɾ��������","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2)==DialogResult.No)
				return;
			long lngRes=clsDomain.m_lngDelUsageSet(m_objViewer.m_lvw2.SelectedItems[0].SubItems[0].Text.ToString().ToString());
			if(lngRes>0)
			{
				m_objViewer.m_lvw2.Items.Remove(m_objViewer.m_lvw2.SelectedItems[0]);
			
				if(m_objViewer.m_lvw2.Items.Count>0)
				{
					if(index>0)
						m_objViewer.m_lvw2.Items[index-1].Selected=true;
					else
						m_objViewer.m_lvw2.Items[index].Selected=true;
				}
			}
		}
		#endregion

		#region ���͵�������
		/// <summary>
		/// ���͵�������
		/// </summary>
		/// <param name="p_strorderid"></param>
		/// <returns></returns>
		private string m_strJieOrderName(string p_strorderid)
		{
			string strResult = "";
			if(p_strorderid != "")
			{
				string[] str = p_strorderid.Split('|');
				clsDomainControl_ChargeItem objsvc = new clsDomainControl_ChargeItem();
				DataTable dt = new DataTable();
				long res= objsvc.m_lngGetData("SELECT ORDERID_INT,ORDERNAME_VCHR FROM t_bse_nurseorder",out dt);
				if(res>0)
				{
					for(int i = 0 ;i<str.Length; ++i)
					{						
						for(int i2 = 0 ;i2<dt.Rows.Count; ++i2)
						{
							if(str[i].Trim() == dt.Rows[i2]["ORDERID_INT"].ToString().Trim())
							{
								strResult += dt.Rows[i2]["ORDERNAME_VCHR"].ToString().Trim()+",";
								break;
							}
						}
					}
				}
			}
			if(strResult.EndsWith(","))
			{
				strResult= strResult.Substring(0,strResult.Length-1);
			}
			return strResult;
		}
		#endregion
		#region ����������ʾ�ڶ���listview������  xigui.peng 2005-11-29
		public void m_selectByCondition()
		{
			clsUsageType_VO[] objResult1 = new clsUsageType_VO[0];
			m_objViewer.m_lvw2.Items.Clear();
			long lngRes=clsDomain.m_lngGetUsageSet(out objResult1);
			if(lngRes>0 && objResult1.Length>0)
			{
				ListViewItem lvw=null;
				for(int i1=0;i1<objResult1.Length;i1++)
				{
					if(Convert.ToInt32(this.m_objViewer.m_cmbType.SelectedIndex)==0&&Convert.ToInt32(this.m_objViewer.m_cmbType.SelectedIndex)+1==Convert.ToInt32(objResult1[i1].m_strUsageType.ToString()))
					{
						lvw=new ListViewItem(objResult1[i1].m_strUsageID);
						lvw.SubItems.Add(objResult1[i1].m_strUsageCode);
						lvw.SubItems.Add(objResult1[i1].m_strUsageName);
						lvw.SubItems.Add("ע��");
						//lvw.SubItems.Add(m_strJieOrderName(objResult1[i1].m_strorderid));
							lvw.SubItems.Add("");
							lvw.Tag = objResult1[i1];
						m_objViewer.m_lvw2.Items.Add(lvw);
					}
					
					else if(Convert.ToInt32(this.m_objViewer.m_cmbType.SelectedIndex)==1&&Convert.ToInt32(this.m_objViewer.m_cmbType.SelectedIndex)+1==Convert.ToInt32(objResult1[i1].m_strUsageType.ToString()))
					{
						lvw=new ListViewItem(objResult1[i1].m_strUsageID);
						lvw.SubItems.Add(objResult1[i1].m_strUsageCode);
						lvw.SubItems.Add(objResult1[i1].m_strUsageName);
						lvw.SubItems.Add("����");
						lvw.Tag = objResult1[i1];
							lvw.SubItems.Add("");
			
						m_objViewer.m_lvw2.Items.Add(lvw);
					}
					else if(Convert.ToInt32(this.m_objViewer.m_cmbType.SelectedIndex)==2&&Convert.ToInt32(this.m_objViewer.m_cmbType.SelectedIndex)+1==Convert.ToInt32(objResult1[i1].m_strUsageType.ToString()))
					{
						lvw=new ListViewItem(objResult1[i1].m_strUsageID);
						lvw.SubItems.Add(objResult1[i1].m_strUsageCode);
						lvw.SubItems.Add(objResult1[i1].m_strUsageName);
						lvw.SubItems.Add("����");
							lvw.SubItems.Add("");
						lvw.Tag = objResult1[i1];
						m_objViewer.m_lvw2.Items.Add(lvw);
					}
					else if(Convert.ToInt32(this.m_objViewer.m_cmbType.SelectedIndex)==3&&Convert.ToInt32(this.m_objViewer.m_cmbType.SelectedIndex)+1==Convert.ToInt32(objResult1[i1].m_strUsageType.ToString()))
					
					{
						lvw=new ListViewItem(objResult1[i1].m_strUsageID);
						lvw.SubItems.Add(objResult1[i1].m_strUsageCode);
						lvw.SubItems.Add(objResult1[i1].m_strUsageName);
						lvw.SubItems.Add("��Ѫ");
						lvw.SubItems.Add("");
						lvw.Tag = objResult1[i1];
						m_objViewer.m_lvw2.Items.Add(lvw);
					}
                    else if (Convert.ToInt32(this.m_objViewer.m_cmbType.SelectedIndex) == 4 && Convert.ToInt32(this.m_objViewer.m_cmbType.SelectedIndex) + 1 == Convert.ToInt32(objResult1[i1].m_strUsageType.ToString()))
                    {
                        lvw = new ListViewItem(objResult1[i1].m_strUsageID);
                        lvw.SubItems.Add(objResult1[i1].m_strUsageCode);
                        lvw.SubItems.Add(objResult1[i1].m_strUsageName);
                        lvw.SubItems.Add("����");
                        lvw.SubItems.Add("");
                        lvw.Tag = objResult1[i1];
                        m_objViewer.m_lvw2.Items.Add(lvw);
                    }
						//m_objViewer.m_lvw2.Items.Add(lvw);
					
				}
				int intTypeId = this.m_objViewer.m_cmbType.SelectedIndex+1;
				DataTable dt = m_mthGetDataFromTypeId(intTypeId);
				for(int i = 0; i<this.m_objViewer.m_lvw2.Items.Count;i++)
				{
					for(int i2 = 0; i2<dt.Rows.Count;i2++)
					{
						if( dt.Rows[i2]["USAGEID_CHR"].ToString().Trim() == this.m_objViewer.m_lvw2.Items[i].SubItems[0].Text.Trim())
							if(dt.Rows[i2]["ORDERNAME_VCHR"].ToString().Trim() != "")
							this.m_objViewer.m_lvw2.Items[i].SubItems[4].Text += dt.Rows[i2]["ORDERNAME_VCHR"].ToString().Trim()+",";
					}
					string str = this.m_objViewer.m_lvw2.Items[i].SubItems[4].Text;
						if(str.EndsWith(","))
						this.m_objViewer.m_lvw2.Items[i].SubItems[4].Text = str.Substring(0,str.Length-1);
				}

			}
			
		}
		#endregion

		private DataTable m_mthGetDataFromTypeId(int p_intTypeId)
		{
			clsDomainControl_ChargeItem objsvc = new clsDomainControl_ChargeItem();
			DataTable dt = new DataTable();
			long res= objsvc.m_lngGetData("SELECT * FROM t_opr_setusage t1,t_bse_nurseorder t2 where t1.orderid_vchr=t2.orderid_int(+)  and t1.TYPE_INT="+p_intTypeId.ToString(),out dt);
			if(res >0)
				return dt;
			else
				return null;
		}
	}
}
