using System;
using System.Windows.Forms;
using System.Drawing;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// Summary description for clsController_VerifySampleRelation.
	/// </summary>
	public class clsController_VerifySampleRelation : com.digitalwave.GUI_Base.clsController_Base
	{
		com.digitalwave.iCare.gui.LIS.clsDomainController_CheckResultManage m_objManage;
		com.digitalwave.iCare.gui.LIS.clsDomainController_SampleManage m_objSampleManage;

		#region ���캯��
		public clsController_VerifySampleRelation()
		{
			//
			// TODO: Add constructor logic here
			//
			m_objManage = new clsDomainController_CheckResultManage();
			m_objSampleManage = new clsDomainController_SampleManage();
		}
		#endregion
		
		#region ���ô������
		com.digitalwave.iCare.gui.LIS.frmVerifySampleRelation m_objViewer;
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmVerifySampleRelation)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ʼ���������
		public void m_mthOnViewerLoad()
		{
			m_mthClearDeviceRelation();
		}
		#endregion

		#region ��ѯ����������Ӧ��������� ͯ�� 2004.11.08
		public void m_mthGetDeviceResult()
		{
			m_objViewer.m_lsvDeviceCheckResult.Items.Clear();
			if(m_objViewer.m_lsvDeviceResultImpReq.SelectedItems.Count <= 0)
				return;
			try
			{
				long lngRes = 0;
				clsResultLogVO objVO = (clsResultLogVO)m_objViewer.m_lsvDeviceResultImpReq.SelectedItems[0].Tag;
				int intBeginIdx = int.Parse(objVO.m_strBeginIndex);
				int intEndIdx = int.Parse(objVO.m_strEndIndex);
				string strDeviceID = objVO.m_strDeviceID;
				string strDeviceSampleID = objVO.m_strDeviceSampleID;
				string strCheckDat = objVO.m_strCheckDat;

				clsDeviceReslutVO[] objResultArr = null;

				lngRes = m_objManage.m_lngGetDeviceData(strDeviceID,strDeviceSampleID,strCheckDat,intBeginIdx,intEndIdx,out objResultArr);

				if(lngRes > 0 && objResultArr != null && objResultArr.Length > 0)
				{
					for(int i=0;i<objResultArr.Length;i++)
					{
						ListViewItem objlsvItem = new ListViewItem();
						objlsvItem.Text = objResultArr[i].m_strDeviceCheckItemName;
						objlsvItem.SubItems.Add(objResultArr[i].m_strResult);
						objlsvItem.Tag = objResultArr[i];
						m_objViewer.m_lsvDeviceCheckResult.Items.Add(objlsvItem);
					}
				}
			}
			catch
			{}
		}
		#endregion

		#region ����������ѯ��T_OPR_LIS_RESULT_IMPORT_REQ��Ӧ������ ͯ�� 2004.07.23
		public void m_mthGetResultImportReqByCondition()
		{
			m_objViewer.m_lsvDeviceResultImpReq.Items.Clear();
			if(m_objViewer.m_cboDevice.Items.Count <= 0)
				return;
			long lngRes = 0;
			string strDeviceID = "";
			string strCheckDatFrom = "";
			string strCheckDatTo = "";
			clsResultLogVO[] objResultArr = null;

			#region ��ȡ��������
			strDeviceID = m_objViewer.m_cboDevice.SelectedValue.ToString().Trim();
			strCheckDatFrom = m_objViewer.m_dtpCheckDatFrom.Value.ToShortDateString().Trim() + " 00:00:00";
			strCheckDatTo = m_objViewer.m_dtpCheckDatTo.Value.ToShortDateString().Trim() + " 23:59:59";
			#endregion

			lngRes = m_objManage.m_lngGetDeviceResultLogByCondition(strCheckDatFrom,strCheckDatTo,strDeviceID,out objResultArr);
			if(lngRes > 0 && objResultArr != null && objResultArr.Length > 0)
			{
				for(int i=0;i<objResultArr.Length;i++)
				{
					ListViewItem objlsvItem = new ListViewItem();
					objlsvItem.Text = objResultArr[i].m_strIMPORT_REQ_INT;
					objlsvItem.SubItems.Add(objResultArr[i].m_strDeviceSampleID.Trim());
					objlsvItem.SubItems.Add(objResultArr[i].m_strCheckDat.Trim());
					objlsvItem.Tag = objResultArr[i];
					m_objViewer.m_lsvDeviceResultImpReq.Items.Add(objlsvItem);
				}
			}
			else
			{
				MessageBox.Show("�޷��������ļ�¼��");
			}
		}
		#endregion

		#region ����������ѯ����������֮��Ĺ�ϵ ͯ�� 2004.07.26
		public void m_mthGetDeviceRelationVOArrByCondition()
		{
			m_objViewer.m_lsvDeviceRelation.Items.Clear();
			if(m_objViewer.m_cboCheckDevice.Items.Count <= 0)
				return;
			long lngRes = 0;
			string strDeviceID = "";
			string strReceptDatFrom = "";
			string strReceptDatTo = "";
			clsT_LIS_DeviceRelationVO[] p_objResultArr = null;

			#region ��ȡ��������
			strDeviceID = m_objViewer.m_cboCheckDevice.SelectedValue.ToString().Trim();
			strReceptDatFrom = m_objViewer.m_dtpReceptDatFrom.Value.ToShortDateString().Trim() + " 00:00:00";
			strReceptDatTo = m_objViewer.m_dtpReceptDatTo.Value.ToString().Trim() + " 23:59:59";
			#endregion

			lngRes = m_objSampleManage.m_lngGetDeviceRelationVOArrByCondition(strDeviceID,strReceptDatFrom,strReceptDatTo,out p_objResultArr);

			if(lngRes > 0 && p_objResultArr != null && p_objResultArr.Length > 0)
			{
				for(int i=0;i<p_objResultArr.Length;i++)
				{
					ListViewItem objlsvItem = new ListViewItem(((int)(i+1)).ToString().Trim());
					objlsvItem.SubItems.Add(p_objResultArr[i].m_strCheckNO);
					objlsvItem.SubItems.Add(p_objResultArr[i].m_strDEVICE_SAMPLEID_CHR);
					if(p_objResultArr[i].m_intSTATUS_INT == 1)
					{
						objlsvItem.SubItems.Add("");
					}
					else if(p_objResultArr[i].m_intSTATUS_INT == 2)
					{
						objlsvItem.SubItems.Add("��");
					}
					else
					{
						objlsvItem.SubItems.Add("��");
					}
					if(p_objResultArr[i].m_intSAMPLE_STATUS_INT == 0)
					{
						objlsvItem.SubItems.Add("����");
					}
					else if(p_objResultArr[i].m_intSAMPLE_STATUS_INT == 1)
					{
						objlsvItem.SubItems.Add("��ʼ״̬");
					}
					else if(p_objResultArr[i].m_intSAMPLE_STATUS_INT == 2)
					{
						objlsvItem.SubItems.Add("�ɼ�");
					}
					else if(p_objResultArr[i].m_intSAMPLE_STATUS_INT == 3)
					{
						objlsvItem.SubItems.Add("����");
					}
					else if(p_objResultArr[i].m_intSAMPLE_STATUS_INT == 5)
					{
						objlsvItem.SubItems.Add("����");
					}
					else if(p_objResultArr[i].m_intSAMPLE_STATUS_INT == 6)
					{
						objlsvItem.SubItems.Add("���");
					}
					else if(p_objResultArr[i].m_intSAMPLE_STATUS_INT == 7)
					{
						objlsvItem.SubItems.Add("�˻�");
					}
					else
					{
						objlsvItem.SubItems.Add("");
					}
					objlsvItem.SubItems.Add(p_objResultArr[i].m_strRECEPTION_DAT);
					objlsvItem.SubItems.Add(p_objResultArr[i].m_strCHECK_DAT);
					objlsvItem.Tag = p_objResultArr[i];
					m_objViewer.m_lsvDeviceRelation.Items.Add(objlsvItem);
				}
			}
			else
			{
				MessageBox.Show("�޷��������ļ�¼��");
			}
		}
		#endregion

		#region ȡ������������ϵ  ͯ�� 2004.07.26
		public void m_mthCancelDeviceRelation()
		{
			if(m_objViewer.m_lsvDeviceRelation.SelectedItems.Count <= 0)
				return;
			long lngRes = 0;
			int[] intSelectedIndices = new int[m_objViewer.m_lsvDeviceRelation.SelectedIndices.Count];
			m_objViewer.m_lsvDeviceRelation.SelectedIndices.CopyTo(intSelectedIndices,0);

			for(int i=0;i<m_objViewer.m_lsvDeviceRelation.SelectedItems.Count;i++)
			{
				clsT_LIS_DeviceRelationVO objRecord = (clsT_LIS_DeviceRelationVO)m_objViewer.m_lsvDeviceRelation.SelectedItems[i].Tag;
				if(objRecord.m_intSTATUS_INT == 1)
				{
					return;
				}
				else if(objRecord.m_intSTATUS_INT == 0)
				{
					continue;
				}
				//�ж��Ƿ�������˻����˻�
				if(objRecord.m_intSAMPLE_STATUS_INT >= 6)
				{
					MessageBox.Show("����˻����˻صı걾����ȡ���󶨣�");
					return;
				}
				else if(objRecord.m_intSAMPLE_STATUS_INT == 0)
				{
					continue;
				}
	//			DialogResult dlgRes = MessageBox.Show("�Ƿ�������������󶨵��������ݣ�","������������",MessageBoxButtons.YesNo,MessageBoxIcon.None,MessageBoxDefaultButton.Button2);
	//			if(dlgRes == DialogResult.Yes)
	//			{
	//				//������������
	//				lngRes = m_objManage.m_lngSetResultImportReqStatus(objRecord.m_strDEVICEID_CHR,objRecord.m_strDEVICE_SAMPLEID_CHR,objRecord.m_strCHECK_DAT,
	//					"0");
	//			}
	//			else if(dlgRes == DialogResult.No)
	//			{
				//����������Ϊδ��
				lngRes = m_objManage.m_lngSetResultImportReqStatus(objRecord.m_strDEVICEID_CHR,objRecord.m_strDEVICE_SAMPLEID_CHR,objRecord.m_strCHECK_DAT,
					"1");
				//			}
			
				if(lngRes > 0)
				{
					objRecord.m_intSTATUS_INT = 1;
					objRecord.m_strDEVICE_SAMPLEID_CHR = null;
					objRecord.m_strCHECK_DAT = null;
					objRecord.m_intIMPORT_REQ_INT = -1;
					//�������������ϵΪδ��
					lngRes = m_objSampleManage.m_lngSetLisDeviceRelation(objRecord);
				}
			}
			if(lngRes > 0)
			{
				m_mthGetDeviceRelationVOArrByCondition();
				m_objViewer.m_lsvDeviceRelation.Focus();
				for(int i=0;i<intSelectedIndices.Length;i++)
				{
					m_objViewer.m_lsvDeviceRelation.Items[intSelectedIndices[i]].Selected = true;
					m_objViewer.m_lsvDeviceRelation.Items[intSelectedIndices[i]].EnsureVisible();
				}
			}
		}
		#endregion

		#region ��������������ϵ  ͯ�� 2004.07.26
		public void m_mthInValidatDeviceRelation()
		{
			if(m_objViewer.m_lsvDeviceRelation.SelectedItems.Count <= 0)
				return;
			long lngRes = 0;
			int[] intSelectedIndices = new int[m_objViewer.m_lsvDeviceRelation.SelectedIndices.Count];
			m_objViewer.m_lsvDeviceRelation.SelectedIndices.CopyTo(intSelectedIndices,0);

			for(int i=0;i<m_objViewer.m_lsvDeviceRelation.SelectedItems.Count;i++)
			{
				clsT_LIS_DeviceRelationVO objRecord = (clsT_LIS_DeviceRelationVO)m_objViewer.m_lsvDeviceRelation.SelectedItems[i].Tag;
				if(objRecord.m_intSTATUS_INT == 0)
				{
					continue;
				}
				//�ж��Ƿ�������˻����˻�
				if(objRecord.m_intSAMPLE_STATUS_INT >= 6)
				{
					MessageBox.Show("����˻����˻صı걾�������Ϲ�����");
					return;
				}
				else if(objRecord.m_intSAMPLE_STATUS_INT == 0)
				{
					continue;
				}

				if(objRecord.m_intSTATUS_INT == 2)
				{
	//				DialogResult dlgRes = MessageBox.Show("�Ƿ�������������󶨵��������ݣ�","������������",MessageBoxButtons.YesNo,MessageBoxIcon.None,MessageBoxDefaultButton.Button2);
	//				if(dlgRes == DialogResult.Yes)
	//				{
	//					//������������
	//					lngRes = m_objManage.m_lngSetResultImportReqStatus(objRecord.m_strDEVICEID_CHR,objRecord.m_strDEVICE_SAMPLEID_CHR,objRecord.m_strCHECK_DAT,
	//						"0");
	//				}
	//				else if(dlgRes == DialogResult.No)
	//				{
					//����������Ϊδ��
					lngRes = m_objManage.m_lngSetResultImportReqStatus(objRecord.m_strDEVICEID_CHR,objRecord.m_strDEVICE_SAMPLEID_CHR,objRecord.m_strCHECK_DAT,
						"1");
					//				}
				}
				else if(objRecord.m_intSTATUS_INT == 1)
				{
					lngRes = 1;
				}
				objRecord.m_intSTATUS_INT = 0;
				//��������������ϵ
				if(lngRes > 0)
				{
					//�������������ϵΪδ��
					lngRes = m_objSampleManage.m_lngSetLisDeviceRelation(objRecord);
				}
			}
			if(lngRes > 0)
			{
				m_mthGetDeviceRelationVOArrByCondition();
				m_objViewer.m_lsvDeviceRelation.Focus();
				for(int i=0;i<intSelectedIndices.Length;i++)
				{
					m_objViewer.m_lsvDeviceRelation.Items[intSelectedIndices[i]].Selected = true;
					m_objViewer.m_lsvDeviceRelation.Items[intSelectedIndices[i]].EnsureVisible();
				}
			}
		}
		#endregion

		#region �������������ϵ ͯ�� 2004.07.30
		public void m_mthClearDeviceRelation()
		{
			m_objViewer.m_dtpReceptDatFrom.Value = DateTime.Parse(System.DateTime.Now.ToShortDateString()+" 00:00:00");
		}
		#endregion
	}
}
