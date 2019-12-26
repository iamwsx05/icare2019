using System;
using System.Data;
using System.Windows.Forms; 
using com.digitalwave.iCare.gui.LIS.Report;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// clsController_StatReport ��ժҪ˵����
	/// </summary>
	public class clsController_StatReport: com.digitalwave.GUI_Base.clsController_Base
	{
		com.digitalwave.iCare.gui.LIS.frmStatReport m_objViewer;
		com.digitalwave.iCare.gui.LIS.clsDomainController_StatManage m_objManage;
		#region ���캯��
		public clsController_StatReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDomainController_StatManage();
		}
		#endregion

		#region ���ô������
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmStatReport)frmMDI_Child_Base_in;
		}
		#endregion

		#region �����������ɱ��� ͯ�� 2004.09.22
		public void m_mthCreateReport()
		{
			m_objViewer.m_btnQuery.Enabled = false;
			m_objViewer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			long lngRes = 0;
			string strDatFrom = m_objViewer.m_dtpDatFrom.Value.ToString("yyyy-MM-dd HH:mm:ss");		//xing.chen modify for Ϊͳ�Ʋ�ѯ����ʱ��
            string strDatTo = m_objViewer.m_dtpDatTo.Value.ToString("yyyy-MM-dd HH:mm:ss");			//xing.chen modify for Ϊͳ�Ʋ�ѯ����ʱ��
			string strOprID = m_objViewer.m_txtOprDoct.m_StrEmployeeID;
			DataTable dtbResult = null;

			//CrystalDecisions.CrystalReports.Engine.ReportDocument objReportDoc = new ReportDocument();

			try
			{
				if(m_objViewer.m_cboReport.SelectedIndex == 0)
				{
					#region ����������
					lngRes = m_objManage.m_lngGetStatTotalReport(strDatFrom,strDatTo,strOprID,out dtbResult);
					if(lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
					{
						//					cryStatTotalReport objReport = new cryStatTotalReport();
						//					objReport.SetDataSource(dtbResult);
						try
						{
							//objReportDoc.Load(@"lis_reports\cryStatTotalReport.rpt");
							//objReportDoc.SetDataSource(dtbResult);
						}
						catch
						{
							MessageBox.Show("���ر���ʧ�ܣ�");
						}
					}
					else
					{
						MessageBox.Show("û�з��������ļ�¼��");
						return;
					}
					#endregion
				}
				else if(m_objViewer.m_cboReport.SelectedIndex == 1)
				{
					#region ��������ϸ
					lngRes = m_objManage.m_lngGetStatDetailReport(strDatFrom,strDatTo,strOprID,out dtbResult);
					if(lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
					{
						//					cryStatDetailReport objReport = new cryStatDetailReport();
						//					objReport.SetDataSource(dtbResult);
						try
						{
							//objReportDoc.Load(@"lis_reports\cryStatDetailReport.rpt");

       //                     objReportDoc.SetDataSource(dtbResult);
						}
						catch
						{
							MessageBox.Show("���ر���ʧ�ܣ�");
						}

						//					//���Ͳ���
						//					for(int i=0;i<=paramValues.Count-1;i++)
						//					{	
						//						paramValue.Clear();
						//						paramValue.Add(paramValues[i]);
						//						objReport.DataDefinition.ParameterFields[i].ApplyCurrentValues(paramValue);
						//					}
						//					m_objViewer.m_ReportViewer.ReportSource = objReport;
						//					m_objViewer.m_ReportViewer.RefreshReport();
					}
					else
					{
						MessageBox.Show("û�з��������ļ�¼��");
						return;
					}
					#endregion
				}
				else if(m_objViewer.m_cboReport.SelectedIndex == 2)
				{
					#region ������û���
					lngRes = m_objManage.m_lngGetCheckPriceTotalReport(strDatFrom,strDatTo,strOprID,out dtbResult);
					if(lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
					{
						//					cryCheckPriceTotalReport objReport = new cryCheckPriceTotalReport();
						//					objReport.SetDataSource(dtbResult);
						try
						{
							//objReportDoc.Load(@"lis_reports\cryCheckPriceTotalReport.rpt");
							//objReportDoc.SetDataSource(dtbResult);
						}
						catch
						{
							MessageBox.Show("���ر���ʧ�ܣ�");
						}

						//					//���Ͳ���
						//					for(int i=0;i<=paramValues.Count-1;i++)
						//					{	
						//						paramValue.Clear();
						//						paramValue.Add(paramValues[i]);
						//						objReport.DataDefinition.ParameterFields[i].ApplyCurrentValues(paramValue);
						//					}
						//					m_objViewer.m_ReportViewer.ReportSource = objReport;
						//					m_objViewer.m_ReportViewer.RefreshReport();
					}
					else
					{
						MessageBox.Show("û�з��������ļ�¼��");
						return;
					}
					#endregion
				}
				else if(m_objViewer.m_cboReport.SelectedIndex == 3)
				{
					#region ���������ϸ
					lngRes = m_objManage.m_lngGetCheckPriceDetailReport(strDatFrom,strDatTo,strOprID,out dtbResult);
					if(lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
					{
						//					cryCheckPriceDetailReport objReport = new cryCheckPriceDetailReport();
						//					objReport.SetDataSource(dtbResult);
						try
						{
							//objReportDoc.Load(@"lis_reports\cryCheckPriceDetailReport.rpt");
							//objReportDoc.SetDataSource(dtbResult);
						}
						catch
						{
							MessageBox.Show("���ر���ʧ�ܣ�");
						}

						//					//���Ͳ���
						//					for(int i=0;i<=paramValues.Count-1;i++)
						//					{	
						//						paramValue.Clear();
						//						paramValue.Add(paramValues[i]);
						//						objReport.DataDefinition.ParameterFields[i].ApplyCurrentValues(paramValue);
						//					}
						//					m_objViewer.m_ReportViewer.ReportSource = objReport;
						//					m_objViewer.m_ReportViewer.RefreshReport();
					}
					else
					{
						MessageBox.Show("û�з��������ļ�¼��");
						return;
					}
					#endregion
				}
				#region ���Ͳ���
                ////���Ͳ���
                //ParameterValues paramValues=new ParameterValues();
                ////�������
                //ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();//
                //discreteVal.Value=m_objViewer.m_dtpDatFrom.Value.ToShortDateString();
                //paramValues.Add(discreteVal);
                //discreteVal=new ParameterDiscreteValue();//
                //discreteVal.Value=m_objViewer.m_dtpDatTo.Value.ToShortDateString();
                //paramValues.Add(discreteVal);
		
                ////���������뼯��
                //discreteVal.Kind =DiscreteOrRangeKind.DiscreteValue;
                //ParameterValues paramValue=new ParameterValues();

                ////���Ͳ���
                //for(int i=0;i<=paramValues.Count-1;i++)
                //{	
                //    paramValue.Clear();
                //    paramValue.Add(paramValues[i]);
                //    objReportDoc.DataDefinition.ParameterFields[i].ApplyCurrentValues(paramValue);
                //}

                //objReportDoc.SetParameterValue(0, strDatFrom.Substring(0,16));
                //objReportDoc.SetParameterValue(1, strDatTo.Substring(0, 16));
                #endregion            

				//m_objViewer.m_ReportViewer.ReportSource = objReportDoc;
                //m_objViewer.m_ReportViewer.RefreshReport(); ���ϴ˾����ʹ֮ǰ�����õĲ���ʧЧ��
			}
			catch(Exception objEx)
			{
			}
			finally
			{
				m_objViewer.m_btnQuery.Enabled = true;
				m_objViewer.Cursor = System.Windows.Forms.Cursors.Default;
			}
		}
		#endregion

        #region ����������ͳ�Ʊ���
        /// <summary>
        /// ����������ͳ�Ʊ���
        /// baojian.mo add in 2007.12.01
        /// </summary>
        /// <param name="p_datStart"></param>
        /// <param name="p_datEnd"></param>
        /// <param name="p_dtDevice"></param>
        public void m_mthGetDeviceCheckTotal(DateTime p_datStart, DateTime p_datEnd, out DataTable p_dtDevice)
        {
            m_objManage.m_mthGetDeviceCheckStatis(p_datStart, p_datEnd, out p_dtDevice);
        }        
        #endregion


    }
}
