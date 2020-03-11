using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsCtl_WaitDiagListManage 的摘要说明。
	/// </summary>
	public class clsCtl_WaitDiagListManage2: com.digitalwave.GUI_Base.clsController_Base
	{
		/// <summary>
		/// 权限标标志
		/// </summary>
		public bool flag=false;
		private clsDcl_WaitDiagListManage objSvc=null;
		/// <summary>
		/// 全局变量记录部门ID
		/// </summary>
		ArrayList objArr ;
		public clsCtl_WaitDiagListManage2()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			objSvc=new clsDcl_WaitDiagListManage();
			objArr=new ArrayList();
		}
		#region 设置窗体对象
		com.digitalwave.iCare.gui.HIS.frmWaitDiagListManage2 m_objViewer;
		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmWaitDiagListManage2)frmMDI_Child_Base_in;
		}
		#endregion
		#region 窗体初始化
		public void m_mthFormLoad()
		{
			this.m_mthLoadDepByID();
			this.m_mthGetWaitListInfoByID();
			this.m_mthGetEndListInfoByID();
		}
		#endregion
		#region 加载部门
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
		#region 获取候诊列表
		public void m_mthGetWaitListInfoByID()
		{
			string strID="0000001";
			if(this.m_objViewer.LoginInfo!=null)
			{
				strID=this.m_objViewer.LoginInfo.m_strEmpID;
			}
			DataTable dt;
			long l =objSvc.m_mthGetWaitListInfoByID("","",out dt,this.m_objViewer.dateTimePicker1.Value,m_objViewer.dateTimePicker2.Value,strID,1);
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
					try
					{
						if(dt.Rows[i]["BIRTH_DAT"].ToString().Trim()!="")
						{
                            try
                            {
                                string strage = com.digitalwave.controls.clsArithmetic.CalcAge(DateTime.Parse(dt.Rows[i]["BIRTH_DAT"].ToString().Trim()));
                                lv.SubItems.Add(strage);

                            }
                            catch (Exception)
                            {
                                lv.SubItems.Add("");
                            }
						}
						else
						{
							lv.SubItems.Add("");
						}
					}
					catch
					{
						lv.SubItems.Add("");
					}
//					string strage =com.digitalwave.controls.clsArithmetic.CalcAge(DateTime.Parse(dt.Rows[i]["BIRTH_DAT"].ToString().Trim()));
//					lv.SubItems.Add(strage);
					lv.SubItems.Add(dt.Rows[i]["ORDER_INT"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["WAITDIAGDR_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["EMPNAME"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["WAITDIAGDEPT_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["DEPTNAME_VCHR"].ToString().Trim());
					this.m_objViewer.listView2.Items.Add(lv);
					
				}
				this.m_objViewer.listView2.Items[0].Selected=true;
				this.m_objViewer.listView2.Focus();
			}
			
		}
		#endregion
		#region 获取就诊列表
		public void m_mthGetEndListInfoByID()
		{
			if(this.m_objViewer.cmbDep.SelectedIndex<0)
			{
			return;
			}
			string strDepID =this.objArr[this.m_objViewer.cmbDep.SelectedIndex].ToString();
			string strID="0000001";
			if(this.m_objViewer.LoginInfo!=null)
			{
				strID=this.m_objViewer.LoginInfo.m_strEmpID;
			}
			DataTable dt;
		
			long l =objSvc.m_mthGetWaitListInfoByID(this.m_objViewer.txtDoctor.Text,strDepID,out dt,this.m_objViewer.dateTimePicker1.Value,m_objViewer.dateTimePicker2.Value,strID,2);
			this.m_objViewer.listView1.Items.Clear();
			if(l>0&&dt.Rows.Count>0)
			{
				ListViewItem lv;
				for(int i=0;i<dt.Rows.Count;i++)
				{
					lv=new ListViewItem(dt.Rows[i]["WAITDIAGLISTID_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["PATIENTCARDID_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["LASTNAME_VCHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["SEX_CHR"].ToString().Trim());
                    try
                    {
                        string strage = com.digitalwave.controls.clsArithmetic.CalcAge(DateTime.Parse(dt.Rows[i]["BIRTH_DAT"].ToString().Trim()));
                        lv.SubItems.Add(strage);

                    }
                    catch (Exception)
                    {
                        lv.SubItems.Add(dt.Rows[i]["BIRTH_DAT"].ToString().Trim());
                    }
					lv.SubItems.Add(dt.Rows[i]["ORDER_INT"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["WAITDIAGDR_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["EMPNAME"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["WAITDIAGDEPT_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["DEPTNAME_VCHR"].ToString().Trim());
					this.m_objViewer.listView1.Items.Add(lv);
					
				}
				
			}
			
		}
		#endregion
		#region 插队
		public void m_mthPrecedence()
		{
			if(this.m_objViewer.listView2.SelectedItems.Count==0)
			{
				return;
			}
			if(this.m_objViewer.listView2.SelectedItems[0].SubItems[5].Text.Trim()=="1")
			{
				return;
			}
			string strDocID=this.m_objViewer.listView2.SelectedItems[0].SubItems[6].Text;//医生ID
			string strDepID=this.m_objViewer.listView2.SelectedItems[0].SubItems[8].Text;//科室ID
			int RowNo=m_mthConvertToInt(this.m_objViewer.listView2.SelectedItems[0].SubItems[5].Text);//候诊号
			string strListID=this.m_objViewer.listView2.SelectedItems[0].SubItems[0].Text;//候诊ID
			long l=this.objSvc.m_mthPrecedence(strDocID,strDepID,RowNo,strListID);
			if(l>0)
			{
				this.m_mthGetWaitListInfoByID();
			}

		}
		#endregion
		#region 转为数字
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
		#region 换医生
		public void m_mthChangeDocOrDep()
		{
			if(this.m_objViewer.listView2.SelectedItems.Count==0)
			{
				return;
			}
			frmDocList2 objfrm =new frmDocList2();
			objfrm.objDepArr=this.objArr;
			for(int i =0;i<this.m_objViewer.cmbDep.Items.Count;i++)
			{
			objfrm.cmbDep.Items.Add(this.m_objViewer.cmbDep.Items[i].ToString());
			}
			objfrm.ListID=this.m_objViewer.listView2.SelectedItems[0].SubItems[0].Text;
			string strDocID=this.m_objViewer.listView2.SelectedItems[0].SubItems[6].Text;
			if(objfrm.ShowDialog()==DialogResult.OK)
			{
				this.m_mthGetWaitListInfoByID();
			}
		}
		#endregion
		#region 移动位置
		public long m_mthMoveOrder(string row1,string row2,string ID1,string ID2)
		{
			return objSvc.m_mthMoveOrder(row1,row2,ID1,ID2);
		}
		#endregion
	}
}
