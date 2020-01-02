using System;
using System.Data;
using System.Windows.Forms; 
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// clsController_ScienceStat ��ժҪ˵����
	/// </summary>
	public class clsController_ScienceStat: com.digitalwave.GUI_Base.clsController_Base
	{
		frmScienceStat m_objViewer;
		#region ���캯��
		public clsController_ScienceStat()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion

		#region ���ô������
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmScienceStat)frmMDI_Child_Base_in;
		}
		#endregion

		#region Inital
		public void m_mthInitalViewer()
		{
			m_mthGetCheckCategory();
			if(m_objViewer.m_cboCheckCategory.Items.Count > 0)
				m_objViewer.m_cboCheckCategory.SelectedIndex = 0;
			m_mthGetCheckItemByCheckCategory();
			m_objViewer.m_cboLowCompare.SelectedIndex = 0;
			m_objViewer.m_cboCondition.SelectedIndex = 0;
			m_objViewer.m_cboUpCompare.SelectedIndex = 0;
			m_objViewer.m_cboFromAgeUnit.SelectedIndex = 0;
			m_objViewer.m_cboToAgeUnit.SelectedIndex = 0;
		}
		#endregion

		#region ��ȡ���еļ������
		public void m_mthGetCheckCategory()
		{
			long lngRes = 0;
			DataTable dtbCheckCategory = null;
			clsDomainController_CheckItemManage objManage = new clsDomainController_CheckItemManage();
			lngRes = objManage.m_lngGetCheckCategory(out dtbCheckCategory);
			if(lngRes > 0 && dtbCheckCategory != null)
			{
				m_objViewer.m_cboCheckCategory.DataSource = dtbCheckCategory;
				m_objViewer.m_cboCheckCategory.DisplayMember = "CHECK_CATEGORY_DESC_VCHR";
				m_objViewer.m_cboCheckCategory.ValueMember = "CHECK_CATEGORY_ID_CHR";
			}
		}
		#endregion

		#region ���ݼ�������ȡ��Ӧ�ļ�����Ŀ
		public void m_mthGetCheckItemByCheckCategory()
		{
			if(m_objViewer.m_cboCheckCategory.Items.Count <= 0)
				return;
			long lngRes = 0;
			string strCheckCategory = m_objViewer.m_cboCheckCategory.SelectedValue.ToString().Trim();
			DataTable dtbResult = null;
			clsDomainController_CheckItemManage objManage = new clsDomainController_CheckItemManage();
			lngRes = objManage.m_lngGetCheckItemArrByCheckCategory(strCheckCategory,out dtbResult);
			if(lngRes > 0 && dtbResult != null)
			{
				m_objViewer.m_cboCheckItem.DataSource = dtbResult;
				m_objViewer.m_cboCheckItem.DisplayMember = "CHECK_ITEM_NAME_VCHR";
				m_objViewer.m_cboCheckItem.ValueMember = "CHECK_ITEM_ID_CHR";
			}
		}
		#endregion

		#region ����������ѯѧ��ͳ����Ϣ
		public void m_mthQueryScienceStatInfo()
		{
			m_objViewer.m_lsvItemDetail.Items.Clear();
			#region checkValidate
			if(m_objViewer.m_txtAgeFrom.Text.ToString().Trim() != "")
			{
				if(!Microsoft.VisualBasic.Information.IsNumeric(m_objViewer.m_txtAgeFrom.Text.ToString().Trim()))
				{
					MessageBox.Show("��������ȷ�����ʽ��","ѧ��ͳ��");
					m_objViewer.m_txtAgeFrom.Focus();
					return;
				}
			}
			if(m_objViewer.m_txtAgeTo.Text.ToString().Trim() != "")
			{
				if(!Microsoft.VisualBasic.Information.IsNumeric(m_objViewer.m_txtAgeTo.Text.ToString().Trim()))
				{
					MessageBox.Show("��������ȷ�����ʽ��","ѧ��ͳ��");
					m_objViewer.m_txtAgeTo.Focus();
					return;
				}
			}
			if(m_objViewer.m_lsvItemList.Items.Count <= 0)
			{
				MessageBox.Show("�������ѯ����","ѧ��ͳ��");
				return;
			}
			#endregion
			this.m_objViewer.m_btnQuery.Enabled = false;
			this.m_objViewer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			#region InitalParameter
			string strCheckItemID = m_objViewer.m_cboCheckItem.SelectedValue.ToString().Trim();
			string strCheckDatFrom = m_objViewer.m_dtpDatFrom.Value.ToShortDateString() + " 00:00:00";
			string strCheckDatTo = m_objViewer.m_dtpDatTo.Value.ToShortDateString() + " 23:59:59";
			string strResultFrom = m_objViewer.m_txtResultFrom.Text.ToString().Trim();
			string strResultTo = m_objViewer.m_txtResultTo.Text.ToString().Trim();
			string strLowCompare = m_objViewer.m_cboLowCompare.Text.ToString().Trim();
			if(strLowCompare == "��")
			{
				strLowCompare = "IN";
			}
			else if(strLowCompare == "����")
			{
				strLowCompare = "NOT IN";
			}
			string strUpCompare = m_objViewer.m_cboUpCompare.Text.ToString().Trim();
			string strCondition = m_objViewer.m_cboCondition.Text.ToString().Trim();
			string strAgeFrom = "";
			string strAgeTo = "";
			string strSex = m_objViewer.m_cboSex.Text.ToString().Trim();

			if(m_objViewer.m_txtAgeFrom.Text.ToString().Trim() != null &&  m_objViewer.m_txtAgeFrom.Text.ToString().Trim() != "")
			{
				strAgeFrom = m_objViewer.m_txtAgeFrom.Text.ToString().Trim()+" "+m_objViewer.m_cboFromAgeUnit.Text.ToString().Trim();
			}

			if(m_objViewer.m_txtAgeTo.Text.ToString().Trim() != null && m_objViewer.m_txtAgeTo.Text.ToString().Trim() != "")
			{
				strAgeTo = m_objViewer.m_txtAgeTo.Text.ToString().Trim()+" "+m_objViewer.m_cboToAgeUnit.Text.ToString().Trim();
			}
			#endregion

			clsLisScienceStatItemQueryCondition[] objVOArr = new clsLisScienceStatItemQueryCondition[m_objViewer.m_lsvItemList.Items.Count];
			for(int i=0;i<m_objViewer.m_lsvItemList.Items.Count;i++)
			{
				objVOArr[i] = (clsLisScienceStatItemQueryCondition)m_objViewer.m_lsvItemList.Items[i].Tag;
			}

			clsDomainController_StatManage objManage = new clsDomainController_StatManage();
			DataTable dtbHead = null;
			DataTable dtbDetail = null;
			DataSet ds = new DataSet();
			long lngRes = 0;
			lngRes = objManage.m_lngGetScienceStatByCondition(strCheckDatFrom,strCheckDatTo,strAgeFrom,
				strAgeTo,strSex,objVOArr,out dtbHead,out dtbDetail);
			if(dtbHead != null && dtbDetail != null)
			{
				dtbHead.TableName = "dtbHead";
				dtbDetail.TableName = "dtbDetail";
				ds.Tables.Add(dtbHead);
				ds.Tables.Add(dtbDetail);
			}
//			DataSet ds = m_mthCreateDataSet();

//			com.digitalwave.iCare.gui.LIS.Report.cryScienceStat obj = new com.digitalwave.iCare.gui.LIS.Report.cryScienceStat();

			//CrystalDecisions.CrystalReports.Engine.ReportDocument objReportDoc = new ReportDocument();

			try
			{
				if(lngRes > 0 && ds != null)
				{
                    try
                    {
                        //objReportDoc.Load(Application.StartupPath + "\\lis_reports\\cryScienceStat.rpt");
                        //objReportDoc.SetDataSource(ds);
                    }
                    catch
                    {
                        MessageBox.Show("���ر���ʧ�ܣ�");
                    }
				}
				else
				{
					MessageBox.Show("û�з��������ļ�¼","ѧ��ͳ��");
					ds = null;
					ds.Tables.Add(new DataTable("dtbHead"));
					ds.Tables.Add(new DataTable("dtbDetail"));
				}

				#region ���Ͳ���
				try
				{
                    ////���Ͳ���
                    //ParameterValues paramValues=new ParameterValues();
                    ////�������
                    //ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();//
                    //discreteVal.Value=m_objViewer.m_dtpDatFrom.Value.ToShortDateString().Trim();
                    //paramValues.Add(discreteVal);
                    //ParameterDiscreteValue discreteVal1 = new ParameterDiscreteValue();//
                    //discreteVal1.Value=m_objViewer.m_dtpDatTo.Value.ToShortDateString().Trim();
                    //paramValues.Add(discreteVal1);
		
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
                    //objReportDoc.SetParameterValue(0, strCheckDatFrom.Substring(0, 16));
                    //objReportDoc.SetParameterValue(1, strCheckDatTo.Substring(0, 16));
				}
				catch
				{
				}
				#endregion

				//m_objViewer.m_ReportViewer.ReportSource = objReportDoc;
		//		m_objViewer.m_ReportViewer.RefreshReport();
			}
			catch
			{
			}
			finally
			{
				this.m_objViewer.Cursor = System.Windows.Forms.Cursors.Default;
				this.m_objViewer.m_btnQuery.Enabled = true;
			}
		}
		#endregion

		#region ��Ӽ�����Ŀ��ѯ����
		public void m_mthAddCheckItemCondition()
		{
			#region checkValidate
			if(m_objViewer.m_txtResultFrom.Text.ToString().Trim() == "" && m_objViewer.m_txtResultTo.Text.ToString().Trim() == "")
			{
				MessageBox.Show("��������Ҫͳ�Ƶļ���������!");
				m_objViewer.m_txtResultFrom.Focus();
				return;
			}
			if(m_objViewer.m_cboLowCompare.Text == ">" || m_objViewer.m_cboLowCompare.Text == ">=")
			{
				if(m_objViewer.m_txtResultFrom.Text.ToString().Trim() != "")
				{
					if(!Microsoft.VisualBasic.Information.IsNumeric(m_objViewer.m_txtResultFrom.Text.ToString().Trim()))
					{
						MessageBox.Show("�����������͵ļ�������Χ��");
						m_objViewer.m_txtResultFrom.Focus();
						return;
					}
				}
				if(m_objViewer.m_txtResultTo.Text.ToString().Trim() != "")
				{
					if(!Microsoft.VisualBasic.Information.IsNumeric(m_objViewer.m_txtResultTo.Text.ToString().Trim()))
					{
						MessageBox.Show("��������ֵ�͵ļ�������Χ��");
						m_objViewer.m_txtResultTo.Focus();
						return;
					}
				}
			}

			string strCheckItemID = m_objViewer.m_cboCheckItem.SelectedValue.ToString().Trim();
			for(int i=0;i<m_objViewer.m_lsvItemList.Items.Count;i++)
			{
				if(strCheckItemID == ((clsLisScienceStatItemQueryCondition)m_objViewer.m_lsvItemList.Items[i].Tag).m_strCheckItemID)
				{
					MessageBox.Show("�ü�����Ŀ�Ѿ����ڣ�","ѧ��ͳ��");
					return;
				}
			}
			#endregion

			#region createVO
			clsLisScienceStatItemQueryCondition objVO = new clsLisScienceStatItemQueryCondition();
			objVO.m_strCheckItemID = m_objViewer.m_cboCheckItem.SelectedValue.ToString().Trim();
			objVO.m_strLowCondition = m_objViewer.m_cboLowCompare.Text.ToString().Trim();
			objVO.m_strLowResult = m_objViewer.m_txtResultFrom.Text.ToString().Trim();
			objVO.m_strResultRelation = m_objViewer.m_cboCondition.Text.ToString().Trim();
			objVO.m_strUpCondition = m_objViewer.m_cboUpCompare.Text.ToString().Trim();
			objVO.m_strUpResult = m_objViewer.m_txtResultTo.Text.ToString().Trim();
			#endregion

			ListViewItem lsvItem = new ListViewItem();
			lsvItem.Text = m_objViewer.m_cboCheckItem.Text.ToString().Trim();
			string strCondition = objVO.m_strLowCondition+objVO.m_strLowResult;
			if(objVO.m_strUpResult != null && objVO.m_strUpResult != "")
			{
				strCondition += objVO.m_strResultRelation+objVO.m_strUpCondition+objVO.m_strUpResult;
			}
			lsvItem.SubItems.Add(strCondition);
			lsvItem.Tag = objVO;
			m_objViewer.m_lsvItemList.Items.Add(lsvItem);
		}
		#endregion

		#region �Ƴ�������Ŀ��ѯ����
		public void m_mthRemoveCheckItemCondition()
		{
			for(int i=0;i<m_objViewer.m_lsvItemList.Items.Count;i++)
			{
				if(m_objViewer.m_lsvItemList.Items[i].Checked)
				{
					m_objViewer.m_lsvItemList.Items[i].Remove();
					i--;
				}
			}
		}
		#endregion
	}
}
