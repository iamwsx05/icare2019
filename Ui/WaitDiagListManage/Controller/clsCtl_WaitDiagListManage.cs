using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsCtl_WaitDiagListManage ��ժҪ˵����
	/// </summary>
	public class clsCtl_WaitDiagListManage: com.digitalwave.GUI_Base.clsController_Base
	{
		/// <summary>
		/// Ȩ�ޱ��־
		/// </summary>
		public bool flag=false;
		private clsDcl_WaitDiagListManage objSvc=null;
		/// <summary>
		/// ȫ�ֱ�����¼����ID
		/// </summary>
		ArrayList objArr ;
		public clsCtl_WaitDiagListManage()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			objSvc=new clsDcl_WaitDiagListManage();
			objArr=new ArrayList();
		}
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmWaitDiagListManage m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmWaitDiagListManage)frmMDI_Child_Base_in;
		}
		#endregion
		#region �����ʼ��
		public void m_mthFormLoad()
		{
			this.m_mthLoadDepByID();
		}
		#endregion
		#region ���ز���
		public void m_mthLoadDepByID()
		{
			string strID="0000001";
			if(this.m_objViewer.LoginInfo!=null)
			{
				strID=this.m_objViewer.LoginInfo.m_strEmpID;
			}
			if(flag)
			{
			strID="";
			}
			DataTable dt;
			long l =objSvc.m_mthGetDepartmentByID(strID,out dt);
			if(l>0&&dt.Rows.Count>0)
			{
				
				for(int i=0;i<dt.Rows.Count;i++)
				{
				this.m_objViewer.cmbDep.Items.Add(dt.Rows[i]["DEPTNAME_VCHR"].ToString().Trim());
				objArr.Add(dt.Rows[i]["DEPTID_CHR"].ToString().Trim());
				}
				this.m_objViewer.cmbDep.SelectedIndex=0;
			}
		}
		#endregion
		#region ���ݲ���ID���ҵ����Ű�ҽ��,
		public void m_mthGetDocByDepID()
		{
			if(this.m_objViewer.cmbDep.SelectedIndex<0)
			{
			return;
			}
			string strID=this.objArr[this.m_objViewer.cmbDep.SelectedIndex].ToString();	
			if(strID=="")
			{
			return;
			}
			DataTable dt;
			long l =objSvc.m_mthGetDocByDepID(strID,out dt);
			this.m_objViewer.listView1.Items.Clear();
			if(l>0&&dt.Rows.Count>0)
			{
				ListViewItem lv;
				for(int i=0;i<dt.Rows.Count;i++)
				{
					lv=new ListViewItem(dt.Rows[i]["EMPNO_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["LASTNAME_VCHR"].ToString().Trim());
					if(dt.Rows[i]["ISEXPERT_CHR"].ToString().Trim()=="1")
					{
						lv.SubItems.Add("ר��");
					}
					else
					{
						lv.SubItems.Add("��ͨ");
					}
					lv.SubItems.Add(dt.Rows[i]["EMPID_CHR"].ToString().Trim());
					this.m_objViewer.listView1.Items.Add(lv);
					
				}
						

					this.m_objViewer.listView1.Items[0].Selected=true;
					this.m_objViewer.listView1.Focus();
				}
			ListViewItem lv2=new ListViewItem("****");
			lv2.SubItems.Add("����");
			lv2.SubItems.Add("****");
			lv2.SubItems.Add("****");
			lv2.ForeColor=Color.FromArgb(105, 130, 238);
			this.m_objViewer.listView1.Items.Add(lv2);
		}
		#endregion
		#region ���Һ��ﲡ��
		public void m_mthGetWaitListByID()
		{
			if(this.m_objViewer.cmbDep.SelectedIndex<0)
			{
				return;
			}
			string strID=this.objArr[this.m_objViewer.cmbDep.SelectedIndex].ToString();	
			if(strID=="")
			{
				return;
			}
			DataTable dt;
			long l =objSvc.m_mthGetWaitListByID(this.m_objViewer.listView1.Tag.ToString(),strID,out dt,m_objViewer.dateTimePicker1.Value,m_objViewer.dateTimePicker2.Value);
			this.m_objViewer.listView2.Items.Clear();
			if(l>0&&dt.Rows.Count>0)
			{
				ListViewItem lv;
				for(int i=0;i<dt.Rows.Count;i++)
				{
					lv=new ListViewItem(dt.Rows[i]["WAITDIAGLISTID_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["PATIENTCARDID_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["LASTNAME_VCHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["SEX_CHR"].ToString().Trim());
					string strage =com.digitalwave.controls.clsArithmetic.CalcAge(DateTime.Parse(dt.Rows[i]["BIRTH_DAT"].ToString().Trim()));
					lv.SubItems.Add(strage);
					lv.SubItems.Add(dt.Rows[i]["ORDER_INT"].ToString().Trim());
					this.m_objViewer.listView2.Items.Add(lv);
					
				}
				
			}
		}
		#endregion
		#region ���
		public void m_mthPrecedence()
		{
			if(this.m_objViewer.cmbDep.SelectedIndex<0)
			{
				return;
			}
			string strDepID=this.objArr[this.m_objViewer.cmbDep.SelectedIndex].ToString();	//����ID
			if(strDepID=="")
			{
				return;
			}
			string strDocID=this.m_objViewer.listView1.Tag.ToString();//ҽ��ID
			int RowNo=m_mthConvertToInt(this.m_objViewer.listView2.SelectedItems[0].SubItems[4].Text);//�����
			string strListID=this.m_objViewer.listView2.SelectedItems[0].SubItems[0].Text;//����ID
			long l=this.objSvc.m_mthPrecedence(strDocID,strDepID,RowNo,strListID);
			if(l>0)
			{
			this.m_mthGetWaitListByID();
			}

		}
		#endregion
		#region תΪ����
		private int m_mthConvertToInt(string str)
		{
			int ret=0;
			try
			{
			ret=int.Parse(str);
			}
			catch
			{
			
			}
			return ret;
		
		}
		#endregion
		#region ��ҽ��
		public void m_mthChangeDoc()
		{
			if(this.m_objViewer.cmbDep.SelectedIndex<0)
			{
				return;
			}
			string strID=this.objArr[this.m_objViewer.cmbDep.SelectedIndex].ToString();	
			if(strID=="")
			{
				return;
			}
			DataTable dt;
			long l =objSvc.m_mthGetDocByDepID(strID,out dt);
//			this.m_objViewer.listView1.Items.Clear();
			frmDocList objfrm =new frmDocList();
			objfrm.DepID=strID;
			objfrm.ListID=this.m_objViewer.listView2.SelectedItems[0].SubItems[0].Text;
			if(l>0&&dt.Rows.Count>0)
			{
				ListViewItem lv;
				for(int i=0;i<dt.Rows.Count;i++)
				{
					lv=new ListViewItem(dt.Rows[i]["EMPNO_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["LASTNAME_VCHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["EMPID_CHR"].ToString().Trim());
					objfrm.listView1.Items.Add(lv);
				}
			}
			else
			{
			return ;
			}
			if(objfrm.ShowDialog()==DialogResult.OK)
			{
			this.m_mthGetWaitListByID();
			}
		}
		#endregion
	}
}
