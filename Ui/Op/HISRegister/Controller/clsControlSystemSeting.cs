using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS

{
	/// <summary>
	/// clsControlSystemSeting ��ժҪ˵����
	/// </summary>
	public class clsControlSystemSeting :com.digitalwave.GUI_Base.clsController_Base
	{
		clsDcl_StstemSeting clsDomain;
		DataTable m_dtRpt;
		DataTable m_dtInfo;
		public clsControlSystemSeting()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain = new clsDcl_StstemSeting();
		}

		#region ���ô������	�Ź���	 2005-2-23
		com.digitalwave.iCare.gui.HIS.frmSystemSeting m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmSystemSeting)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ȡ�����б�  �Ź���	 2005-2-23
		public void m_GetSettings()
		{
			this.m_objViewer.m_dtgGroup.m_mthDeleteAllRow();
			m_dtRpt = new DataTable();
			long lngRes=clsDomain.m_lngStstemSeting(out m_dtRpt);
//			if(lngRes>0 && m_dtRpt.Rows.Count>0)
//			{
//				for(int i=0;i<m_dtRpt.Rows.Count;i++)
//				{
//					this.m_objViewer.m_dtgGroup.m_mthAppendRow(new object[] {m_dtRpt.Rows[i][0],m_dtRpt.Rows[i][1],m_dtRpt.Rows[i][2],m_dtRpt.Rows[i][3],m_dtRpt.Rows[i][4]}); 
//				}
//			}
		}
		#endregion
		#region ���ģ����Ϣ xigui.peng 2005-12-3
		public void m_GetLeftInfo()
		{
		  m_dtInfo=new DataTable();
			long lngRegs=clsDomain.m_lngGetLeftInfo(out m_dtInfo);
			if(lngRegs>0&& m_dtInfo.Rows.Count>0)
			{
				ListViewItem lsv=null;
				for(int i1=0;i1<m_dtInfo.Rows.Count;++i1)
				{
					lsv=new ListViewItem(m_dtInfo.Rows[i1]["MODULEID_CHR"].ToString());
					lsv.SubItems.Add(m_dtInfo.Rows[i1]["MODULENAME_CHR"].ToString());
					this.m_objViewer.lsvConfig.Items.Add(lsv);
				}
			 
			}
		}
		#endregion
		public void UpdataCurrentCell()
		{
			for(int j=0;j<m_objViewer.m_dtgGroup.RowCount;++j)
			for(int i=0;i<m_dtRpt.Rows.Count;i++)
			{
				if(this.m_objViewer.m_dtgGroup[j,0].ToString().Trim()==m_dtRpt.Rows[i][0].ToString().Trim())
			  m_dtRpt.Rows[i][3]=this.m_objViewer.m_dtgGroup[j,3].ToString();
			}
		}
		public void m_SaveSettings()
		{   
			for(int i=0;i<this.m_objViewer.m_dtgGroup.RowCount;i++)
			{  
				if(this.m_objViewer.m_dtgGroup[i,0].ToString().Trim()=="")
					continue;
				long lngRes=clsDomain.m_lngModifyStstemSeting(this.m_objViewer.m_dtgGroup[i,0].ToString().Trim(),this.m_objViewer.m_dtgGroup[i,3].ToString().Trim(),this.m_objViewer.m_dtgGroup[i,4].ToString().Trim());
				
				if(lngRes > 0)
				{
					continue;
				}
				else 
				{
					MessageBox.Show("����ʧ�ܣ���","ICare",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
				
				}
			}
			
			
			MessageBox.Show("����ɹ�����","ICare",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
				
			
		}
		public void m_ShowSelectInfo()
		{
			this.m_objViewer.m_dtgGroup.m_mthDeleteAllRow();
            if (this.m_objViewer.lsvConfig.Items.Count > 0)
            {
                for (int i1 = 0; i1 < this.m_objViewer.lsvConfig.SelectedIndices.Count; i1++)
                {
                    for (int i2 = 0; i2 < this.m_dtRpt.Rows.Count; i2++)
                    {
                        if (this.m_objViewer.lsvConfig.SelectedItems[i1].SubItems[0].Text == this.m_dtRpt.Rows[i2][4].ToString())
                            this.m_objViewer.m_dtgGroup.m_mthAppendRow(new object[] { m_dtRpt.Rows[i2][0], m_dtRpt.Rows[i2][1], m_dtRpt.Rows[i2][2], m_dtRpt.Rows[i2][3], m_dtRpt.Rows[i2][4] });
                    }
                }
            }
		}
	}
}
