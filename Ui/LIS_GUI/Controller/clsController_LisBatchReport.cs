using System;
using System.Data;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using weCare.Core.Entity;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// Summary description for clsController_LisBatchReport.
	/// </summary>
	public class clsController_LisBatchReport :com.digitalwave.GUI_Base.clsController_Base
	{
		com.digitalwave.iCare.gui.LIS.clsDomainController_ReportManage m_objManage;
		public clsController_LisBatchReport()
		{
			//
			// TODO: Add constructor logic here
			//
			m_objManage = new clsDomainController_ReportManage();
		}

		#region ���ô������
		com.digitalwave.iCare.gui.LIS.frmLisBatchReport m_objViewer;
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmLisBatchReport)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ʼ��������ӡ����
		public long m_lngInitialfrmLisBatchReport(frmLisBatchReport infrmLisBatchReport)
		{
			long lngRes = 0;
			m_mthGetAllReportGroup();
			return lngRes;
		}

		#endregion

		#region ��ȡ���еı������� ͯ�� 2004.07.21
		public void m_mthGetAllReportGroup()
		{
			long lngRes = 0;
			clsReportGroup_VO[] objResultArr = null;
			lngRes = m_objManage.m_lngGetAllReportGroup(out objResultArr);
			if(lngRes > 0 && objResultArr != null && objResultArr.Length > 0)
			{
				m_objViewer.m_cboReport.Items.Add("ȫ��");
				for(int i=0;i<objResultArr.Length;i++)
				{
					m_objViewer.m_cboReport.Items.Add(objResultArr[i].strReportGroupName);
				}
				m_objViewer.m_cboReport.Tag = objResultArr;
				m_objViewer.m_cboReport.SelectedIndex = 0;
			}
		}
		#endregion

		#region ����������ѯ���浥��Ϣ ͯ�� 2004.07.22
		public void m_mthGetReportByCondition()
		{
			m_objViewer.m_lsvBaseCheckItem.Items.Clear();
			string strConfirmDatFrom = "";
			string strConfirmDatTo = "";
			string strSampleIDFrom = "";
			string strSampleIDTo = "";
			string strReportGroupID = "";
			
			#region ��ȡ��������
			strConfirmDatFrom = m_objViewer.m_dtpFromDate.Value.ToShortDateString().Trim()+" 00:00:00";
			strConfirmDatTo = m_objViewer.m_dtpToDate.Value.ToShortDateString().Trim()+" 23:59:59";
			strSampleIDFrom = m_objViewer.m_txtSampleNoFrom.Text.ToString().Trim();
			strSampleIDTo = m_objViewer.m_txtSampleNoTo.Text.ToString().Trim();
			if(m_objViewer.m_cboReport.SelectedIndex > 0)
			{
				strReportGroupID = ((clsReportGroup_VO[])m_objViewer.m_cboReport.Tag)[m_objViewer.m_cboReport.SelectedIndex-1].strReportGroupID;
			}
			#endregion
            string strPatientType = m_objViewer.m_cboPatientType.SelectedIndex.ToString();

			long lngRes = 0;
			clsLisBatchReportList_VO[] objResultArr = null;
			clsDomainController_CheckResultManage objDomain = new clsDomainController_CheckResultManage();
			lngRes = objDomain.m_lngGetLisBatchReportListByCondition(strSampleIDFrom,strSampleIDTo,strConfirmDatFrom,strConfirmDatTo,strReportGroupID,strPatientType,out objResultArr);
			if(lngRes > 0 && objResultArr != null && objResultArr.Length > 0)
			{
				for(int i=0;i<objResultArr.Length;i++)
				{
					ListViewItem objlsvItem = new ListViewItem(objResultArr[i].strCheckNO.Trim());
					objlsvItem.SubItems.Add(objResultArr[i].strReportGroupName.ToString().Trim());
					objlsvItem.SubItems.Add(objResultArr[i].strPatientName.ToString().Trim());
					objlsvItem.SubItems.Add(objResultArr[i].strSex.ToString().Trim());
					objlsvItem.SubItems.Add(objResultArr[i].strApplyDept.ToString().Trim());
					if(objResultArr[i].strConfirmDat.ToString().Trim() != "")
					{
						objlsvItem.SubItems.Add(DateTime.Parse(objResultArr[i].strConfirmDat.ToString().Trim()).ToShortDateString().Trim());
					}
					else
					{
						objlsvItem.SubItems.Add("");
					}
					objlsvItem.Tag = objResultArr[i];
					m_objViewer.m_lsvBaseCheckItem.Items.Add(objlsvItem);
				}
			}
			else
			{
				MessageBox.Show("�޷��������ļ�¼��");
			}
		}
		#endregion

		#region ��ȡѡ���ı��浥����ϸ��Ϣ
		public void m_mthGetReportInfoArr(clsLisBatchReportList_VO[] p_objReportList,out clsLisBatchReportDetail_VO[] p_objResultArr)
		{
			long lngRes = 0;
			clsDomainController_CheckResultManage objDomain = new clsDomainController_CheckResultManage();
			lngRes = objDomain.m_lngGetLisBatchReportDetailByCondition(p_objReportList,out p_objResultArr);
		}
		#endregion

		#region ���
		public void m_mthClear()
		{
			m_objViewer.m_dtpFromDate.Value = System.DateTime.Now;
			m_objViewer.m_dtpToDate.Value = System.DateTime.Now;
			m_objViewer.m_lsvBaseCheckItem.Items.Clear();
			if(m_objViewer.m_cboReport.Items.Count > 0)
			{
				m_objViewer.m_cboReport.SelectedIndex = 0;
			}
			m_objViewer.m_txtSampleNoFrom.Clear();
			m_objViewer.m_txtSampleNoTo.Clear();
		}
		#endregion

		#region ȫѡ
		public void m_mthSelectAll()
		{
			if(m_objViewer.m_lsvBaseCheckItem.Items.Count <= 0)
				return;
			for(int i=0;i<m_objViewer.m_lsvBaseCheckItem.Items.Count;i++)
			{
				m_objViewer.m_lsvBaseCheckItem.Items[i].Checked = true;
			}
		}
		#endregion

		#region ȫ��
		public void m_mthNotSelectAll()
		{
			if(m_objViewer.m_lsvBaseCheckItem.Items.Count <= 0)
				return;
			for(int i=0;i<m_objViewer.m_lsvBaseCheckItem.Items.Count;i++)
			{
				m_objViewer.m_lsvBaseCheckItem.Items[i].Checked = false;
			}
		}
		#endregion

        #region ��ȡϵͳ������ֵ
        /// <summary>
        /// ��ȡϵͳ������ֵ
        /// </summary>
        /// <param name="p_strParm"></param>
        /// <param name="p_strValue"></param>
        /// <returns></returns>
        public long m_lngGetSysParmValue(string p_strParm, out string p_strValue)
        {
            long lngRes = m_objManage.m_lngGetSysParmValue(p_strParm, out p_strValue);
            return lngRes;
        }
        #endregion

    }
}
