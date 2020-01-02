using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 
using com.digitalwave.iCare.BIHOrder.Control; 
using System.Collections;

namespace com.digitalwave.iCare.BIHOrder
{
	/// <summary>
	/// ����ҽ��	�߼����Ʋ�
	/// ���ߣ�		����
	/// ����ʱ�䣺	2005-04-22
	/// </summary>
	public class clsCtl_ReformingOrder: com.digitalwave.GUI_Base.clsController_Base
	{
		#region ����
		clsDcl_InputOrder m_objManage = null;
		public string m_strReportID;
		public string m_strOperatorID ="";
		public string m_strOperatorName ="";
		#endregion 
		#region ���캯��
		public clsCtl_ReformingOrder()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDcl_InputOrder();
			m_strReportID = null;
		}
		#endregion 
		#region ���ô������
		com.digitalwave.iCare.BIHOrder.frmReformingOrder m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmReformingOrder)frmMDI_Child_Base_in;
		}
		#endregion

		#region �����¼�
		/// <summary>
		/// ����
		/// </summary>
		public void LoadData()
		{
			m_objViewer.m_lsvDisplayOrder.Items.Clear();
			if(m_objViewer.m_strRegisterID.Trim() ==string.Empty) return;

			//��ȡ���ҽ��
			clsBIHOrder[] objResultArr =new clsBIHOrder[0];
			long lngRes =0;
			switch(m_objViewer.m_intType)
			{
				case 3:
					lngRes =m_objManage.m_lngGetCanStopOrder(m_objViewer.m_strRegisterID,out objResultArr );
					break;
				case 4:
                    lngRes = m_objManage.m_lngGetCanReformingOrder(m_objViewer.m_strRegisterID, out objResultArr );
					break;
				default:
					MessageBox.Show(m_objViewer,"��ȷ���Ĳ����������´�ҳ�棡","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
					m_objViewer.Close();
					break;
			}
			if(lngRes<=0 || objResultArr==null || objResultArr.Length<=0)	return;

			//��ֵListView
			#region ��ֵ
			ListViewItem lviTemp = null;
			System.Drawing.Color clrBack,clrFore;
			for(int i1= 0 ;i1<objResultArr.Length;i1++)
			{
				//���
				lviTemp = new ListViewItem((i1+1).ToString());
				//����
				lviTemp.SubItems.Add(objResultArr[i1].m_intRecipenNo.ToString());
				//��/��	
				if(objResultArr[i1].m_intExecuteType==1)
				{
					lviTemp.SubItems.Add("��");
				}
				else 
				{
					if(objResultArr[i1].m_intExecuteType==2)
						lviTemp.SubItems.Add("��");
					else
						lviTemp.SubItems.Add("");
				}
				//����
				lviTemp.SubItems.Add(objResultArr[i1].m_strName);
				//�� ��
				lviTemp.SubItems.Add(objResultArr[i1].m_dmlDosageRate.ToString()+objResultArr[i1].m_strDosageUnit);
				//�� ��  
				lviTemp.SubItems.Add(objResultArr[i1].m_dmlGet.ToString()+objResultArr[i1].m_strGetunit);
				//ִ��Ƶ��	  
				lviTemp.SubItems.Add(objResultArr[i1].m_strExecFreqName);
				//�� ��	
				lviTemp.SubItems.Add(objResultArr[i1].m_strDosetypeName);
				//Ƥ		
				if(objResultArr[i1].m_intISNEEDFEEL==1)
					lviTemp.SubItems.Add("��");
				else 
					lviTemp.SubItems.Add("");//��
				//����ҽ��
				lviTemp.SubItems.Add(objResultArr[i1].m_strParentName);

				lviTemp.Tag =objResultArr[i1];
				m_objViewer.m_lsvDisplayOrder.Items.Add(lviTemp);
				clsOrderStatus.s_mthGetColorByStatus(objResultArr[i1].m_intExecuteType,objResultArr[i1].m_intStatus,out clrBack,out clrFore);
				m_objViewer.m_lsvDisplayOrder.Items[m_objViewer.m_lsvDisplayOrder.Items.Count-1].ForeColor =clrFore;
				m_objViewer.m_lsvDisplayOrder.Items[m_objViewer.m_lsvDisplayOrder.Items.Count-1].BackColor =clrBack;
			}
			#endregion
		}
		#endregion

		#region ��ť�¼�
		/// <summary>
		/// ����
		/// </summary>
		public void m_OK()
		{
			//��ȡѡ��ҽ��
			ArrayList alItem =new ArrayList();
			IEnumerator iEn =m_objViewer.m_lsvDisplayOrder.CheckedItems.GetEnumerator();
			while (iEn.MoveNext())
			{
				if(((ListViewItem)iEn.Current).Tag is clsBIHOrder)
				{
					alItem.Add(((ListViewItem)iEn.Current).Tag);
				}
			}
			
			//��ʾ
			if(alItem.Count<=0)
			{
				MessageBox.Show(m_objViewer,"��ѡ��ҽ����","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
			if(MessageBox.Show(m_objViewer,"ȷ��������","��ʾ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No) return;
			
			string strMessage ="";
			long lngRes =0;
			switch(m_objViewer.m_intType)
			{
				case 3:
					#region ֹͣ
					foreach(clsBIHOrder objItem in alItem)
					{
						//�ݹ�ֹͣҽ��
						try
						{
                            lngRes = m_objManage.m_lngStopOrder(objItem, m_strOperatorID, m_strOperatorName, true, m_objViewer.IsChildPrice); ;
						}
						catch//(System.Exception e)
						{
							strMessage +="ҽ����["+ objItem.m_strName + "]ֹͣʧ�ܣ�\r\n";
						}
					}
					#endregion
					break;
				case 4:
					#region ����
					foreach(clsBIHOrder objItem in alItem)
					{
						//�ݹ�����ҽ��
						try
						{
                            lngRes = m_objManage.m_lngRetractOrder(objItem, m_strOperatorID, m_strOperatorName, true, m_objViewer.IsChildPrice);
						}
						catch//(System.Exception e)
						{
							strMessage +="ҽ����["+ objItem.m_strName + "]����ʧ�ܣ�\r\n";
						}
					}
					#endregion
					break;
				default:
					MessageBox.Show(m_objViewer,"��ȷ���Ĳ����������´�ҳ�棡","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
					m_objViewer.Close();
					break;
			}
			
			//������
			if(lngRes>0 && strMessage=="")
			{
				MessageBox.Show(m_objViewer,"�����ɹ���","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);			
			}
			else
			{
				if(strMessage.Trim()=="") strMessage ="����ʧ��!";
				MessageBox.Show(m_objViewer,strMessage,"��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

			//ˢ��
			LoadData();
		}
		#endregion
	}
}