using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
 
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlReckoningReport ��ժҪ˵����
	/// </summary>
	public class clsControlPublicPayReport:com.digitalwave.GUI_Base.clsController_Base
	{
		//private CrystalDecisions.CrystalReports.Engine.ReportDocument m_rptRpt;
		clsDcl_ReckoningReport clsDomain;
		string str_firstDate,str_toDate;
		int m_intFindType;
		
		public clsControlPublicPayReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			//m_rptRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
			clsDomain = new clsDcl_ReckoningReport();
			
		}
		
		#region ���ô������	�Ź���	 2004-9-9
		com.digitalwave.iCare.gui.HIS.frmPublicPayReport m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmPublicPayReport)frmMDI_Child_Base_in;

			
		}
		#endregion

		#region ��������  �Ź���	 2004-9-14
		public  void m_mthFindByDateReport()
		{
			

			str_firstDate=m_objViewer.m_daFinDate.Value.ToShortDateString();
			str_toDate = m_objViewer.m_toDate.Value.ToShortDateString();
			
			//m_rptRpt.Load("Report\\rptPublicPay.rpt");
			//((TextObject)m_rptRpt.ReportDefinition.ReportObjects["txtReportTitle"]).Text = this.m_objComInfo.m_strGetHospitalTitle()+"���ѷ��ñ���";
			DataTable m_dtRpt = new DataTable();
			DataTable m_dtRptDitial = new DataTable();
			if(m_objViewer.m_chbPatienName.Checked==true&&m_objViewer.m_chbDate.Checked==true)
			m_intFindType=1;
			else if(m_objViewer.m_chbPatienName.Checked==true&&m_objViewer.m_chbDate.Checked==false)
			m_intFindType=2;
			else if(m_objViewer.m_chbPatienName.Checked==false&&m_objViewer.m_chbDate.Checked==true) 
			m_intFindType=3;
			else
				m_intFindType=4;

				long lngRes;
			lngRes = clsDomain.m_lngPublicPayReport(m_intFindType,m_objViewer.m_txtName.Text.Trim(),str_firstDate,str_toDate,out m_dtRpt);
			if(lngRes>=1)
			{
				//m_rptRpt.SetDataSource(m_dtRpt);
				//m_rptRpt.Refresh();
				//m_objViewer.cryReportViewer.ReportSource=m_rptRpt;
			}
			
			
		}
		#endregion

		#region ���Ҳ�����Ϣ
		public void m_mthFindPatientInfo(int intflag,string strValue)
		{
			DataTable dt =null;
			long strRet=clsDomain.m_mthFindPatientInfo(intflag,strValue,out dt);
			if(strRet>0&&dt.Rows.Count>0)
			{
				this.m_objViewer.listView1.Items.Clear();
				for(int i=0;i<dt.Rows.Count;i++)
				{
					ListViewItem lv =new ListViewItem(dt.Rows[i]["PATIENTCARDID_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["FIRSTNAME_VCHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["SEX_CHR"].ToString().Trim());
					string stryear=dt.Rows[i]["BIRTH_DAT"].ToString().Substring(0,4);
					int year=DateTime.Now.Year-int.Parse(stryear);
					lv.SubItems.Add(year.ToString());
					lv.SubItems.Add(dt.Rows[i]["PATIENTID_CHR"].ToString().Trim());
					this.m_objViewer.listView1.Items.Add(lv);
				}
				this.m_objViewer.listView1.Visible=true;
				this.m_objViewer.listView1.BringToFront();
				this.m_objViewer.listView1.Items[0].Selected=true;
				this.m_objViewer.listView1.Select();
			}

		}
		#endregion

		#region ˫���¼�
		public void m_mthListViewDoubleClick()
		{
			if(this.m_objViewer.listView1.SelectedItems.Count==1)
			{
				this.m_objViewer.txtCardID.Text = this.m_objViewer.listView1.SelectedItems[0].SubItems[0].Text.Trim();
				this.m_objViewer.m_txtName.Text=this.m_objViewer.listView1.SelectedItems[0].SubItems[1].Text.Trim();
				this.m_objViewer.listView1.Hide();
				this.m_objViewer.m_txtName.Focus();
			}
		}
		#endregion

	}
}
