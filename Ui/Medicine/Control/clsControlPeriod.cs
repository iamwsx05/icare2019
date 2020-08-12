using System;
using System.Data;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;
using System.Windows.Forms;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// �����ڽ������
	/// </summary>
	public class clsControlPeriod : com.digitalwave.GUI_Base.clsController_Base //GUI_Base.dll
	{
		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
		public clsControlPeriod()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDomainControlStorageAidInfo();
			intUseMonths = 24;
		}
		#endregion

		clsDomainControlStorageAidInfo m_objManage = null;
		public clsPeriod_VO m_objItem;

		string m_strMaxdate = "";
		int intUseMonths;

		#region ���ô������
		frmPeriod m_objViewer;
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			// TODO:  ��� clsControlStorageOrdType.Set_GUI_Apperance ʵ��
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			m_objViewer = (frmPeriod)frmMDI_Child_Base_in;
		}
		#endregion

		#region ���б��в���һ������  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ���б����һ������
		/// </summary>
		/// <param name="objItem"></param>
		private void m_mthInsertList(clsPeriod_VO objItem)
		{
			System.Windows.Forms.ListViewItem lsvItem = new System.Windows.Forms.ListViewItem(objItem.m_strPeriodID);
			lsvItem.SubItems.Add(objItem.m_strStartDate);
			lsvItem.SubItems.Add(objItem.m_strEndDate);

			lsvItem.Tag = objItem;

			this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);
		}
		#endregion

		#region �޸��б��е�����  ŷ����ΰ  2004-06-06
		/// <summary>
		/// �޸��б��е�����
		/// </summary>
		/// <param name="objItem"></param>
		private void m_mthChangeList(clsPeriod_VO objItem,int intRow)
		{
			m_objViewer.m_lsvDetail.Items[intRow].SubItems[1].Text = objItem.m_strStartDate;
			m_objViewer.m_lsvDetail.Items[intRow].SubItems[2].Text = objItem.m_strEndDate;

			m_objViewer.m_lsvDetail.Items[intRow].Tag = objItem;

		}
		#endregion

		#region ���ô�������  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ���ô�������
		/// </summary>
		/// <param name="p_objItem"></param>
		private void m_mthSetViewInfo(clsPeriod_VO objItem)
		{
//			m_objItem = objItem;
//			
//			if(m_objItem == null)
//			{
//				this.m_objViewer.m_dtpStartDate.Value = System.DateTime.Today;
//				this.m_objViewer.m_dtpEndDate.Value = System.DateTime.Today.AddMonths(1);
//			}
//			else
//			{
//				this.m_objViewer.m_dtpStartDate.Value = System.DateTime.Parse(objItem.m_strStartDate);
//				this.m_objViewer.m_dtpEndDate.Value = System.DateTime.Parse(objItem.m_strEndDate);
//			}
		}
		#endregion

		#region ��õ��������б�  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ��õ��������б�
		/// </summary>
		public void m_mthGetStorageOrdTypeList()
		{
			this.m_objViewer.m_lsvDetail.Items.Clear();

			clsPeriod_VO [] objItemArr = new clsPeriod_VO[0];
			long lngRes = 0;
			
			lngRes = m_objManage.m_lngFindAllPeriod(out objItemArr);

			if(lngRes >0)
			{
				if(objItemArr.Length >0)
				{
					for(int i1=0;i1<objItemArr.Length;i1++)
					{
						m_mthInsertList(objItemArr[i1]);
					}
					m_mthSetViewInfo(objItemArr[0]);
				}
				else
				{
					m_mthSetViewInfo(null);
				}
			}


		}
		#endregion

		#region ��������  ŷ����ΰ  2004-06-06. altered by weiling.huang at 2005-09-29
		/// <summary>
		/// ��������
		/// </summary>
		private void m_mthDoAddNew()
		{
			#region Altered by weiling.huang at 2005-9-29

				int  intYear = Convert.ToInt32(this.m_objViewer.m_cboYear.Text.Trim());
			System.DateTime dteStartDate;				
			System.DateTime dteEndDate;				
			dteStartDate = this.m_objViewer.m_dtpStartDate.Value.Date ;				  	
			string year = Convert.ToString(dteStartDate.Year+intYear);//��
			string md =year+dteStartDate.Date.ToString("yyyy-MM-dd").Substring(4,6);

			dteEndDate = Convert.ToDateTime(md);			  
			clsPeriod_VO objItem ;
//			if(this.m_strMaxdate != "")
//			{
				DateTime dtTemp;
				while(dteStartDate.Date.ToString("yyyy-MM") != dteEndDate.Date.ToString("yyyy-MM") )
				{
					dtTemp = dteStartDate.AddMonths(1);
					objItem = new clsPeriod_VO();
					objItem.m_strPeriodID = this.m_objManage.m_strGetMaxPeriodID();
					objItem.m_strStartDate = dteStartDate.ToString("yyyy-MM-dd");
					objItem.m_strEndDate = dtTemp.ToString("yyyy-MM-dd").Substring(0,8)+(dtTemp.Day-1).ToString("00");
					
					dteStartDate = dtTemp;
					long lngRes = 0;
					lngRes = m_objManage.m_lngDoAddNewPeriod(objItem);
					if(lngRes>0)
					{						
						m_mthInsertList(objItem);
					}
					else
					{
					}
//				}
			}
			
			#endregion

			#region ԭ��
			//				System.DateTime dteStartDate;				
			//				dteStartDate = this.m_objViewer.m_dtpStartDate.Value.Date ;				  	
			//				clsPeriod_VO objItem = new clsPeriod_VO();
			//				objItem.m_strPeriodID = this.m_objManage.m_strGetMaxPeriodID();
			//				objItem.m_strStartDate = dteStartDate.ToString("yyyy-MM-dd");
			//				objItem.m_strEndDate = dteEndDate.ToString("yyyy-MM-dd");
			//				long lngRes = 0;
			//				lngRes = m_objManage.m_lngDoAddNewPeriod(objItem);
			//				if(lngRes>0)
			//				{
			//					MessageBox.Show("���ɳɹ���","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
			//					m_mthInsertList(objItem);
			//				}
			#endregion
		
		}
		#endregion

		#region �����޸�  ŷ����ΰ  2004-06-06
		/// <summary>
		/// �����޸�
		/// </summary>
		private void m_mthDoModify()
		{
//			System.DateTime dteStartDate;
//			System.DateTime dteEndDate;
//
//			dteStartDate = this.m_objViewer.m_dtpStartDate.Value;
//			dteEndDate = this.m_objViewer.m_dtpEndDate.Value;
//            
//			for(int i1=0;i1<intUseMonths;i1++)
//			{
//				if(this.m_objViewer.m_lsvDetail.Items[i1].Tag != null)
//				{
//					m_objItem = (clsPeriod_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
//					m_objItem.m_strStartDate = dteStartDate.AddMonths(i1).ToString("yyyy-MM-dd");
//					m_objItem.m_strEndDate = dteEndDate.AddMonths(i1).ToString("yyyy-MM-dd");
//
//					long lngRes = 0;
//					lngRes = m_objManage.m_lngDoUpdatePeriod(m_objItem);
//
//					if(lngRes>0)
//					{
//						m_mthChangeList(m_objItem,i1);
//					}
//				}
//			}
		}
		#endregion

		#region ɾ������  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ɾ������
		/// </summary>
		private void m_mthDoDelete()
		{
			if(m_objViewer.m_lsvDetail.SelectedItems.Count>0)
			{
				if(m_objViewer.m_lsvDetail.SelectedItems[0].Index>=0)
				{
					clsPeriod_VO objItem;
					objItem = (clsPeriod_VO)m_objViewer.m_lsvDetail.SelectedItems[0].Tag;

					long lngRes = 0;
					lngRes = m_objManage.m_lngDoDeletePeriod(objItem.m_strPeriodID);

					if(lngRes>0)
					{
						m_objViewer.m_lsvDetail.SelectedItems[0].Remove();
					}
					else
					{
						System.Windows.Forms.MessageBox.Show("ɾ������ʱ����","ϵͳ��ʾ");
					}
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("��ѡ����ɾ�������ݣ�","ϵͳ��ʾ");
			}
		}
		#endregion

		#region ɾ����������  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ɾ����������
		/// </summary>
		private void m_mthDoAllDelete()
		{
			m_objManage.m_lngDoDeletePeriod();
		}
		#endregion

		#region ��������  ŷ����ΰ  2004-06-06  . altered by weiling.huang at 2005-09-29
		/// <summary>
		/// ��������
		/// </summary>
		public void m_mthDoSave()
		{

			string strYear = this.m_objViewer.m_cboYear.Text.Trim();
			if(strYear == "")
			{
				MessageBox.Show("����������");
				return ;
			}

			if(!m_mthIsVaild())
			{
				MessageBox.Show("����ʱ����Ч��");
				return;
			}
			if(MessageBox.Show("�Ƿ�Ҫ����������?\n���ɺ󽫲����޸ġ�","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
			{
			return;
			}

			m_mthDoAddNew();
		
			
		}
		#endregion

		#region �б��˫���¼�  ŷ����ΰ  2004-06-06
		/// <summary>
		/// �޸�
		/// </summary>
		public void m_mthModify()
		{
			if(m_objViewer.m_lsvDetail.SelectedItems.Count>0)
			{
				if(m_objViewer.m_lsvDetail.SelectedItems[0].Index == 0)
				{
					clsPeriod_VO objItem;
					objItem = (clsPeriod_VO)m_objViewer.m_lsvDetail.SelectedItems[0].Tag;

					m_mthSetViewInfo(objItem);
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("��ѡ���һ�������ڣ�","ϵͳ��ʾ");
				}
			}
		}
		#endregion

		#region ��ʼʱ����޸��¼�  ŷ����ΰ  2004-06-06
		public void m_mthStartDateChange()
		{
			System.DateTime dteStartDate;

			dteStartDate = this.m_objViewer.m_dtpStartDate.Value;
			//this.m_objViewer.m_dtpEndDate.Value = dteStartDate.AddMonths(1);
		}
		#endregion

		#region �ж�ʱ���Ƿ���Ч
		private bool m_mthIsVaild()
		{
			#region Altered by weiling.huang at 2005-9-29

			bool ret =false;	
			DateTime dt = this.m_objViewer.m_dtpStartDate.Value.Date;
			string strPeriodId = this.m_objManage.m_strGetMaxPeriodID();
			string strMaxdate = "";
			this.m_objManage.m_lngMaxValuePeriod(out strMaxdate);
			DateTime dtMax ;
			if(strMaxdate != "")
			{
				dtMax = Convert.ToDateTime(strMaxdate);
				this.m_strMaxdate = strMaxdate;
				if(dtMax < dt)
				{
					ret = true;
				}
				else
				{
					ret = false;
				}
			}
			else
			{
				ret = true;
			}
			return ret;

			#endregion 

			#region ԭ�汾
			//
//			bool ret =false;

//			if(this.m_objViewer.m_dtpStartDate.Value>=this.m_objViewer.m_dtpEndDate.Value)
//			{
//				MessageBox.Show("��������Ҫ���ڿ�ʼ���ڣ�","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
//				return true;
//			}
//			System.DateTime dteStartDate;
//			System.DateTime dteEndDate;
//
//			dteStartDate = this.m_objViewer.m_dtpStartDate.Value;
//			dteEndDate = this.m_objViewer.m_dtpEndDate.Value;
//			DateTime value1;
//			DateTime value2;
//			for(int i=0;i<this.m_objViewer.m_lsvDetail.Items.Count;i++)
//			{
//			   value1 =DateTime.Parse(this.m_objViewer.m_lsvDetail.Items[i].SubItems[1].Text);
//			   value2 =DateTime.Parse(this.m_objViewer.m_lsvDetail.Items[i].SubItems[2].Text);
//				if(dteStartDate>=value1&&dteStartDate<=value2)
//				{
//					MessageBox.Show("��ʼ�����Ѿ�ʹ�ã�","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
//					return true;
//				}
//				if(dteEndDate>=value1&&dteEndDate<=value2)
//				{
//					MessageBox.Show("���������Ѿ�ʹ�ã�","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
//					return true;
//				}
//			}
			//return ret;
			#endregion		
		
		}
		#endregion

	}
}
