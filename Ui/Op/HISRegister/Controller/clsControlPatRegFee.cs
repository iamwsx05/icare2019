using System;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// �Һŷ���ά��ģ��
	/// </summary>
	public class clsControlPatRegFee:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlPatRegFee()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain=new clsDomainControl_Register();
		}
        clsDomainControl_Register clsDomain;
        public bool IsNew=false;
		bool NoTouch=false;
		#region ���ô������
		frmPatRegFee objfrm;
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
            objfrm=(frmPatRegFee)frmMDI_Child_Base_in;
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
		}
		#endregion
		#region �������з���
		public void m_lngFindPatRegFee()
		{
			long lngRes=0;
			DataTable dtResult=new DataTable();
			Clear();
			lngRes=clsDomain.m_lngFindPatRegFee(out dtResult);
			objfrm.Grid.DataSource=dtResult;
		}
		public void Clear()
		{
			objfrm.ctlRegType.SelectedIndex=-1;
			objfrm.ctlPatType.SelectedIndex=-1;
			objfrm.m_txtRegFee.Clear();
			objfrm.m_txtDiagFee.Clear();
			IsNew=true;
		}
		public void GridIndexChange()
		{
			if(NoTouch)
			{
				NoTouch=false;
				return;
			}
			int row=objfrm.Grid.Row;
			if(objfrm.Grid.Rows==0 || row>objfrm.Grid.Rows-1)
			{
				Clear();
				return;
			}
			objfrm.ctlRegType.RegTypeID=objfrm.Grid.Get_TextMatrix(row,0);
            objfrm.ctlPatType.PatTypeID=objfrm.Grid.Get_TextMatrix(row,1);  
			objfrm.m_txtRegFee.Text=objfrm.Grid.Get_TextMatrix(row,4);  
			objfrm.m_txtDiagFee.Text=objfrm.Grid.Get_TextMatrix(row,5);  
			IsNew=false;
		}
		#endregion
		#region ɾ��
		public void m_Del()
		{
			int row=objfrm.Grid.Row;
			if(objfrm.Grid.Rows==0 || row>objfrm.Grid.Rows-1)
			{
			    Clear();
				return;
			}
			if(MessageBox.Show("ȷ��ɾ��������","��ʾ",MessageBoxButtons.YesNo)==DialogResult.No)
				return;
			clsPatRegFee_VO clsVO;
			this.m_SaveToVO(out clsVO);
			long lngRes=clsDomain.m_lngDelPatRegFee(clsVO);
			if(lngRes>0)
			{
				clsMain.Tips("ɾ���ɹ���");
				m_lngFindPatRegFee();
				this.GridIndexChange();
			}
			else
			{
				clsMain.Tips("ɾ��ʧ�ܣ�");
			}
		}
		#endregion
		#region ����
		public void m_SaveFee()
		{
			long lngRes=0;
			if(m_chkVaild())
				return;
			clsPatRegFee_VO clsVO;
			this.m_SaveToVO(out clsVO);
			lngRes=clsDomain.m_lngSavePatRegFee(clsVO,this.IsNew);
			if(lngRes>0)
			{
               clsMain.Tips("����ɹ���"); 
				if(IsNew)
				{
					int row=objfrm.Grid.Rows;
//					object[] newRow=new object[]{clsVO.m_strRegisterTypeID,
//													clsVO.m_strPayTypeID,
//													objfrm.ctlRegType.SelectItemText,
//						                            objfrm.ctlPatType.SelectItemText,
//													Convert.ToDouble(clsVO.m_decRegFee),Convert.ToDouble(clsVO.m_decDiagFee)};
					objfrm.Grid.DataSource.Rows.Add(new object[6]);
					NoTouch=true;
					objfrm.Grid.Set_TextMatrix(row,0,clsVO.m_strRegisterTypeID);
					objfrm.Grid.Set_TextMatrix(row,1,clsVO.m_strPayTypeID);
					objfrm.Grid.Set_TextMatrix(row,2,objfrm.ctlRegType.SelectItemText);
					objfrm.Grid.Set_TextMatrix(row,3,objfrm.ctlPatType.SelectItemText);
					objfrm.Grid.Set_TextMatrix(row,4,clsVO.m_decRegFee.ToString());
					objfrm.Grid.Set_TextMatrix(row,5,clsVO.m_decDiagFee.ToString());
					IsNew=false;
					NoTouch=false;
				}
				else
				{
					int row=objfrm.Grid.Row;
					NoTouch=true;
					objfrm.Grid.Set_TextMatrix(row,4,clsVO.m_decRegFee.ToString());
					objfrm.Grid.Set_TextMatrix(row,5,clsVO.m_decDiagFee.ToString());
					NoTouch=false;
				}
			}
			else
			{
				clsMain.Tips("����ʧ�ܣ�");
			}
		}
		private void m_SaveToVO(out clsPatRegFee_VO clsVO)
		{
           clsVO=new clsPatRegFee_VO();
		   clsVO.m_strPayTypeID=objfrm.ctlPatType.SelectItemValue;
		   clsVO.m_strRegisterTypeID=objfrm.ctlRegType.SelectItemValue;
		   clsVO.m_decRegFee=decimal.Parse(objfrm.m_txtRegFee.Text);
		   clsVO.m_decDiagFee=decimal.Parse(objfrm.m_txtDiagFee.Text);
		}
		private bool m_chkVaild()
		{
			bool IsVaild=false;
			if(objfrm.ctlRegType.SelectedIndex<0) //�Һ�����
			{
				objfrm.errorProvider1.SetError(objfrm.ctlRegType,"��ѡ��Һ����ͣ�");
                IsVaild=true;
				objfrm.ctlRegType.Focus();
			}
			if(objfrm.ctlPatType.SelectedIndex<0)
			{
				objfrm.errorProvider1.SetError(objfrm.ctlPatType,"��ѡ�������ͣ�");
				if(!IsVaild)
					objfrm.ctlPatType.Focus();
				IsVaild=true;
			}
			string tmpText="";
			tmpText=objfrm.m_txtRegFee.Text.Trim();
			if(tmpText==null || tmpText=="")
			{
				objfrm.errorProvider1.SetError(objfrm.m_txtRegFee,"����д�Һŷѣ�");
				if(!IsVaild)
					objfrm.m_txtRegFee.Focus();
				IsVaild=true;
			}
			else if(!clsMain.IsNumberWithPoint(tmpText))
			{
				objfrm.errorProvider1.SetError(objfrm.m_txtRegFee,"����д��ȷ��");
				if(!IsVaild)
					objfrm.m_txtRegFee.Focus();
				IsVaild=true; 
			}
			tmpText=objfrm.m_txtDiagFee.Text.Trim();
			if(tmpText==null || tmpText=="")
			{
				objfrm.errorProvider1.SetError(objfrm.m_txtDiagFee,"����д�Һŷѣ�");
				if(!IsVaild)
					objfrm.m_txtDiagFee.Focus();
				IsVaild=true;
			}
			else if(!clsMain.IsNumberWithPoint(tmpText))
			{
				objfrm.errorProvider1.SetError(objfrm.m_txtDiagFee,"����д��ȷ��");
				if(!IsVaild)
					objfrm.m_txtDiagFee.Focus();
				IsVaild=true; 
			}
			if(IsVaild) return true;
			long lngRes=0;
			string strPatID=objfrm.ctlPatType.SelectItemValue;
			string strRegID=objfrm.ctlRegType.SelectItemValue;
			clsPatRegFee_VO clsVO;
			lngRes=clsDomain.m_lngFindPatRegFeeByID(strPatID,strRegID,out clsVO);
			if(clsVO!=null)
			{
				if(clsVO.m_strPayTypeID!=null && clsVO.m_strPayTypeID!="" && IsNew)
				{
					MessageBox.Show("�Ѿ����ڴ���Һŷ��ã�","");
					int i=objfrm.Grid.FindItemByValues(new int[]{0,1},new string[]{strRegID,strPatID});
					if(i>-1)
					   objfrm.Grid.Row=i;
                    objfrm.Grid.Focus();
					return true;
				}
			}
			else //��ѯ����
			{
				return true;
			}
			return false;
		}
		#endregion
	}
}
