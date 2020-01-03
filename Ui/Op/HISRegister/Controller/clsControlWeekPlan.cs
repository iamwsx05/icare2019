using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedicine:ҩƷ��Ϣ�б������ Create by Sam 2004-5-24
	/// </summary>
	public class clsControlWeekPlan:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlWeekPlan()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objDoMain=new clsDomainConrol_Plan();
		}
		clsDomainConrol_Plan m_objDoMain=null;
//		private bool IsSec=true; //�����еݹ�

		#region ���ô������
		public com.digitalwave.iCare.gui.HIS.frmWeekPlan m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmWeekPlan)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��������
		/// <summary>
		/// �õݹ鷽������treeView
		/// </summary>
		public void fillDepartTree() 
		{
			System.Data.DataTable dt;
			m_objDoMain.m_lngGetDep(out dt);
			this.createTree(dt,"",null);
			this.m_objViewer.m_TV.Nodes[0].Expand();
		}
		public void createTree(System.Data.DataTable dt,string pId,TreeNode pNode) 
		{
			string tnId = ""; //�ڵ�id;
			string tnName = ""; //�ڵ�Text;
			string rootId = ""; //���ڵ�id;
			string rootName = ""; //���ڵ�Text;
			TreeNode tnRoot = null;
			DataView dv = new DataView(dt);
			//������ڵ�
			if (pId == "")
			{
				dv.RowFilter = "parentId='0'";
				rootId = dv[0]["DEPTID_CHR"].ToString();
				rootName = dv[0]["DEPTNAME_VCHR"].ToString();
				tnRoot = new TreeNode(rootName);
				tnRoot.Tag = rootId;
				this.m_objViewer.m_TV.Nodes.Add(tnRoot);
				//�ݹ�
				this.createTree(dt,rootId,tnRoot);
			}
				//�ݹ�ѭ��,������ڵ�������ӽڵ�
			else 
			{
				dv.RowFilter = "parentId='" + pId + "'";
				foreach (DataRowView drv in dv) 
				{
					tnId = drv["DEPTID_CHR"].ToString();
					tnName = drv["DEPTNAME_VCHR"].ToString();
					TreeNode tn = new TreeNode(tnName);
					tn.Tag = tnId;
					pNode.Nodes.Add(tn);
					this.createTree(dt,tnId,tn);
				}
			}
		}
		#endregion 

		#region ɾ����Ŀ
		public void m_lngDelPlan()
		{
			if(m_objViewer.m_lvwPlan.Items.Count==0 || m_objViewer.m_lvwPlan.SelectedItems.Count==0)
				return;
			if(MessageBox.Show("ȷ��ɾ��������","��ʾ",MessageBoxButtons.YesNo)==DialogResult.No)
				return;
			long lngRes=0;
			clsOPDoctorWkPlan_VO objResult=new clsOPDoctorWkPlan_VO();
			if(m_objViewer.m_lvwPlan.SelectedItems[0].Tag!=null)
				objResult=(clsOPDoctorWkPlan_VO)m_objViewer.m_lvwPlan.SelectedItems[0].Tag;
			else
				return;
			lngRes=m_objDoMain.m_lngDelWeekPlan(objResult.m_strOPDrWkPlanID);
			if(lngRes>0)
			{
				this.m_lngReMoveList();
			}
		}
		//���б����Ƴ�
		public void m_lngReMoveList()
		{
			m_objViewer.m_lvwPlan.SelectedItems[0].Tag=null;
			m_objViewer.m_lvwPlan.Items.Remove(m_objViewer.m_lvwPlan.SelectedItems[0]);
		}
		#endregion
		#region ȡ�ؿ��ҵ�����Ű�
		public void m_GetPlanByDepID()
		{
			clsOPDoctorWkPlan_VO[] objResultArr=null;
			m_objViewer.m_lvwPlan.Items.Clear();
			if(m_objViewer.m_TV.SelectedNode==null)
				return;
			string strDepID=m_objViewer.m_TV.SelectedNode.Tag.ToString();
			string strWeek=this.m_strGetWeek();
			long lngRes;
			if((string)m_objViewer.m_TV.SelectedNode.Tag=="0000001")
				lngRes = m_objDoMain.m_lngGetPlanByWeekAndDepAll(strWeek,out objResultArr);
			else
			    lngRes = m_objDoMain.m_lngGetWeekPlan(strWeek,strDepID,out objResultArr);

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lvw=null;
					int[] strArr=new int[objResultArr.Length];
					int f2=0;
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						if(objResultArr[i1].m_strPlanPeriod.Trim()=="����")
						{
							strArr[f2]=i1;
							f2++;
						}
						else
						{
							lvw=new ListViewItem(objResultArr[i1].m_objOPDept.strDeptName);
							lvw.SubItems.Add(objResultArr[i1].m_objOPDoctor.strEmpNO);
							lvw.SubItems.Add(objResultArr[i1].m_objOPDoctor.strName);
							lvw.SubItems.Add(objResultArr[i1].m_objRegisterType.m_strRegisterTypeName);
							lvw.SubItems.Add(objResultArr[i1].m_strPlanPeriod);
							lvw.SubItems.Add(objResultArr[i1].m_strStartTime);
							lvw.SubItems.Add(objResultArr[i1].m_strEndTime);
							lvw.SubItems.Add(objResultArr[i1].m_strOPAddress);
							lvw.SubItems.Add(objResultArr[i1].m_intMaxDiagTimes.ToString());	
							lvw.Tag=objResultArr[i1];
							m_objViewer.m_lvwPlan.Items.Add(lvw);
							switch(objResultArr[i1].m_strPlanPeriod.Trim())
							{
								case "����":
									m_objViewer.m_lvwPlan.Items[i1-f2].BackColor=System.Drawing.Color.LightSeaGreen;
									break;
								case "����":
									m_objViewer.m_lvwPlan.Items[i1-f2].BackColor=System.Drawing.Color.Tan;
									break; 
								case "ȫ��":
									m_objViewer.m_lvwPlan.Items[i1-f2].BackColor=System.Drawing.Color.LightSalmon;
									break; 
							}
						}
					}
					if(f2>0)
					{
						for(int h4=0;h4<f2;h4++)
						{
							lvw=new ListViewItem(objResultArr[strArr[h4]].m_objOPDept.strDeptName);
							lvw.SubItems.Add(objResultArr[strArr[h4]].m_objOPDoctor.strEmpNO);
							lvw.SubItems.Add(objResultArr[strArr[h4]].m_objOPDoctor.strName);
							lvw.SubItems.Add(objResultArr[strArr[h4]].m_objRegisterType.m_strRegisterTypeName);
							lvw.SubItems.Add(objResultArr[strArr[h4]].m_strPlanPeriod);
							lvw.SubItems.Add(objResultArr[strArr[h4]].m_strStartTime);
							lvw.SubItems.Add(objResultArr[strArr[h4]].m_strEndTime);
							lvw.SubItems.Add(objResultArr[strArr[h4]].m_strOPAddress);
							lvw.SubItems.Add(objResultArr[strArr[h4]].m_intMaxDiagTimes.ToString());	
							lvw.Tag=objResultArr[strArr[h4]];
							m_objViewer.m_lvwPlan.Items.Add(lvw);
							m_objViewer.m_lvwPlan.Items[objResultArr.Length-f2+h4].BackColor=System.Drawing.Color.Orange;
						}
					}
				}
			}
		}
		#endregion
		public void m_SetItem(bool IsNew)
		{
			m_objViewer.Cursor=Cursors.WaitCursor;
			frmAddPlan frm=new frmAddPlan();
			if(m_objViewer.m_TV.Nodes.Count==0)
				return;
			if(m_objViewer.m_TV.SelectedNode==null)
				return;
			if (!IsNew)
			{
				if(m_objViewer.m_lvwPlan.Items.Count>0 && m_objViewer.m_lvwPlan.SelectedItems.Count>0)
					IsNew=false;
				else
					IsNew=true;
			}
			frm.ShowWeekPlan(IsNew,this);
			m_objViewer.Cursor=Cursors.Default;
		}
		public string m_strGetWeek()
		{
			int i=m_objViewer.m_tab.SelectedIndex+1;
			if(i>6)
				i=0;//������
			string strWeek=i.ToString();;
            return strWeek;
		}
		#region ˢ������
		public void m_RefreshDate(bool IsNew,clsOPDoctorWkPlan_VO objResultArr)
		{
			if(IsNew)
			{

				if(objResultArr.m_strPlanPeriod.Trim()=="����")
				{
					ListViewItem lvw=new ListViewItem(objResultArr.m_objOPDept.strDeptName);
					lvw.SubItems.Add(objResultArr.m_objOPDoctor.strEmpNO);
					lvw.SubItems.Add(objResultArr.m_objOPDoctor.strName);
					lvw.SubItems.Add(objResultArr.m_objRegisterType.m_strRegisterTypeName);
					lvw.SubItems.Add(objResultArr.m_strPlanPeriod);
					lvw.SubItems.Add(objResultArr.m_strStartTime);
					lvw.SubItems.Add(objResultArr.m_strEndTime);
					lvw.SubItems.Add(objResultArr.m_strOPAddress);
					lvw.SubItems.Add(objResultArr.m_intMaxDiagTimes.ToString());	
					lvw.Tag=objResultArr;
					lvw.Selected=true;
					m_objViewer.m_lvwPlan.Items.Add(lvw);
					m_objViewer.m_lvwPlan.Items[m_objViewer.m_lvwPlan.Items.Count-1].BackColor=System.Drawing.Color.Orange;
				}
				else
				{
					for(int i1=0;i1<m_objViewer.m_lvwPlan.Items.Count;i1++)
					{

						if(objResultArr.m_strPlanPeriod.Trim()=="����")
						{
							ListViewItem lvw=new ListViewItem(objResultArr.m_objOPDept.strDeptName);
							lvw.SubItems.Add(objResultArr.m_objOPDoctor.strEmpNO);
							lvw.SubItems.Add(objResultArr.m_objOPDoctor.strName);
							lvw.SubItems.Add(objResultArr.m_objRegisterType.m_strRegisterTypeName);
							lvw.SubItems.Add(objResultArr.m_strPlanPeriod);
							lvw.SubItems.Add(objResultArr.m_strStartTime);
							lvw.SubItems.Add(objResultArr.m_strEndTime);
							lvw.SubItems.Add(objResultArr.m_strOPAddress);
							lvw.SubItems.Add(objResultArr.m_intMaxDiagTimes.ToString());	
							lvw.Tag=objResultArr;
							m_objViewer.m_lvwPlan.Items.Insert(0,lvw);
							m_objViewer.m_lvwPlan.Items[0].BackColor=System.Drawing.Color.LightSeaGreen;
							return;
						}
						else
						{
							if(m_objViewer.m_lvwPlan.Items[i1].SubItems[4].Text.Trim()=="����")
							{
								ListViewItem lvw=new ListViewItem(objResultArr.m_objOPDept.strDeptName);
								lvw.SubItems.Add(objResultArr.m_objOPDoctor.strEmpNO);
								lvw.SubItems.Add(objResultArr.m_objOPDoctor.strName);
								lvw.SubItems.Add(objResultArr.m_objRegisterType.m_strRegisterTypeName);
								lvw.SubItems.Add(objResultArr.m_strPlanPeriod);
								lvw.SubItems.Add(objResultArr.m_strStartTime);
								lvw.SubItems.Add(objResultArr.m_strEndTime);
								lvw.SubItems.Add(objResultArr.m_strOPAddress);
								lvw.SubItems.Add(objResultArr.m_intMaxDiagTimes.ToString());	
								lvw.Tag=objResultArr;
								m_objViewer.m_lvwPlan.Items.Insert(i1,lvw);
								m_objViewer.m_lvwPlan.Items[i1].BackColor=System.Drawing.Color.Tan;
								return;
							}
						}
							

					}
					ListViewItem lvw1=new ListViewItem(objResultArr.m_objOPDept.strDeptName);
					lvw1.SubItems.Add(objResultArr.m_objOPDoctor.strEmpNO);
					lvw1.SubItems.Add(objResultArr.m_objOPDoctor.strName);
					lvw1.SubItems.Add(objResultArr.m_objRegisterType.m_strRegisterTypeName);
					lvw1.SubItems.Add(objResultArr.m_strPlanPeriod);
					lvw1.SubItems.Add(objResultArr.m_strStartTime);
					lvw1.SubItems.Add(objResultArr.m_strEndTime);
					lvw1.SubItems.Add(objResultArr.m_strOPAddress);
					lvw1.SubItems.Add(objResultArr.m_intMaxDiagTimes.ToString());	
					lvw1.Tag=objResultArr;
					m_objViewer.m_lvwPlan.Items.Add(lvw1);
					if(m_objViewer.m_lvwPlan.Items.Count==1)
						m_objViewer.m_lvwPlan.Items[0].BackColor=System.Drawing.Color.Coral;
					else
						m_objViewer.m_lvwPlan.Items[m_objViewer.m_lvwPlan.Items.Count-1].BackColor=System.Drawing.Color.Tan;
				}
			}
			else
			{
				if(m_objViewer.m_lvwPlan.SelectedItems.Count>0)
				{
					m_objViewer.m_lvwPlan.SelectedItems[0].Text=objResultArr.m_objOPDept.strDeptName;
					m_objViewer.m_lvwPlan.SelectedItems[0].SubItems[1].Text=objResultArr.m_objOPDoctor.strEmpID;
					m_objViewer.m_lvwPlan.SelectedItems[0].SubItems[2].Text=objResultArr.m_objOPDoctor.strName;
					m_objViewer.m_lvwPlan.SelectedItems[0].SubItems[3].Text=objResultArr.m_objRegisterType.m_strRegisterTypeName;
					m_objViewer.m_lvwPlan.SelectedItems[0].SubItems[4].Text=objResultArr.m_strPlanPeriod;
					m_objViewer.m_lvwPlan.SelectedItems[0].SubItems[5].Text=objResultArr.m_strStartTime;
					m_objViewer.m_lvwPlan.SelectedItems[0].SubItems[6].Text=objResultArr.m_strEndTime;
					m_objViewer.m_lvwPlan.SelectedItems[0].SubItems[7].Text=objResultArr.m_strOPAddress;
					m_objViewer.m_lvwPlan.SelectedItems[0].SubItems[8].Text=objResultArr.m_intMaxDiagTimes.ToString();	
					m_objViewer.m_lvwPlan.SelectedItems[0].Tag=objResultArr;
				}
			}
			try
			{
				switch(objResultArr.m_strPlanPeriod)
				{
					case "����":
						if(m_objViewer.m_lvwPlan.Items.Count==1)
							m_objViewer.m_lvwPlan.Items[0].BackColor=System.Drawing.Color.LightSeaGreen;
						else
							m_objViewer.m_lvwPlan.Items[m_objViewer.m_lvwPlan.SelectedItems[0].Index].BackColor=System.Drawing.Color.LightSeaGreen;
						break;
					case "����":
						if(m_objViewer.m_lvwPlan.Items.Count==1)
							m_objViewer.m_lvwPlan.Items[0].BackColor=System.Drawing.Color.Orange;
						else
							m_objViewer.m_lvwPlan.Items[m_objViewer.m_lvwPlan.SelectedItems[0].Index].BackColor=System.Drawing.Color.Orange;
						break;
					case "����":
						if(m_objViewer.m_lvwPlan.Items.Count==1)
							m_objViewer.m_lvwPlan.Items[0].BackColor=System.Drawing.Color.Tan;
						else
							m_objViewer.m_lvwPlan.Items[m_objViewer.m_lvwPlan.SelectedItems[0].Index].BackColor=System.Drawing.Color.Tan;
						break;
					case "ȫ��":
						if(m_objViewer.m_lvwPlan.Items.Count==1)
							m_objViewer.m_lvwPlan.Items[0].BackColor=System.Drawing.Color.Goldenrod;
						else
							m_objViewer.m_lvwPlan.Items[m_objViewer.m_lvwPlan.SelectedItems[0].Index].BackColor=System.Drawing.Color.LightSalmon;
						break;
				}
			}
			catch
			{
			}
		}
		#endregion
		#region Ӧ�õ�����
		public long m_lngApp()
		{
			clsOPDoctorWkPlan_VO objResult=new clsOPDoctorWkPlan_VO();
			if(m_objViewer.m_lvwPlan.Items.Count==0 || m_objViewer.m_lvwPlan.SelectedItems.Count==0)
				return -1;
			if(m_objViewer.m_lvwPlan.SelectedItems[0].Tag!=null)
				objResult=(clsOPDoctorWkPlan_VO)m_objViewer.m_lvwPlan.SelectedItems[0].Tag;
			else
				return-1;
			if(MessageBox.Show("Ӧ�õ����콫�Ḳ��ԭ�е��Ű࣬ȷ����","��ʾ",MessageBoxButtons.YesNo)==DialogResult.No)
				return -1;
			long lngRes=0;
			lngRes=m_objDoMain.m_lngAppWeekPlan(objResult);
			if(lngRes>0)
			{
				MessageBox.Show("Ӧ���ܼƻ��ɹ���","��ʾ");
			}
			else
				MessageBox.Show("Ӧ���ܼƻ�ʧ�ܣ�","��ʾ");
			return lngRes;
		}
		#endregion
	}
}
