using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility;
using com.digitalwave.iCare.common;
using System.Collections;
using System.Drawing;
using com.digitalwave.iCare.middletier.HI;
using com.digitalwave.iCare.gui.Security;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlRegister ��ժҪ˵����
	/// </summary>
	public class clsControlCancelReg:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlCancelReg()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain=new clsDomainControl_Register();
			clsRegister=new clsPatientRegister_VO();
            m_objChargeCal = new clsCalcPatientCharge(string.Empty, string.Empty, 0, this.m_objComInfo.m_strGetHospitalTitle(), 0, 0);
		//	OperatorID=this.m_objViewer.LoginInfo.m_strEmpID;
		}
//		int intcommand=null;
		clsDomainControl_Register clsDomain;
		clsPatientRegister_VO clsRegister;
        private clsCalcPatientCharge m_objChargeCal;
		private enum enumlvwSel:byte
		{
			PatType=1,
			Dept=2,
			RegType=3,
			Doc=4
		}
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmCancelRegister m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmCancelRegister)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��մ�������
		public void m_Clear()
		{
			clsRegister=new clsPatientRegister_VO();
			clsRegister.m_objDiagDept=new clsDepartmentVO();
			clsRegister.m_objDiagDoctor=new clsEmployeeVO();
			clsRegister.m_strPayType=new clsPatientType_VO();
			clsRegister.m_strRegisterType=new clsRegisterType_VO();
			clsRegister.m_objPatientCard=new clsPatientCard_VO();
			clsRegister.m_objRegisterEmp=new clsEmployeeVO();
			m_objViewer.m_cboSex.SelectedIndex=-1;
			m_objViewer.m_cboSex.Text="";
			this.m_objViewer.m_txtChangeCharge.Text = "0";
			this.m_objViewer.m_txtChangeDisCount.Text = "0";
			m_objViewer.m_dtpBirth.Value=DateTime.Now.Date;
			m_objViewer.m_dtpPreTime.Value=m_ServDate();
			m_objViewer.m_cboSeg.SelectedIndex=this.m_GetSerPerio();
			m_objViewer.m_lbEnd.Text="";
			m_objViewer.m_lbRoom.Text="";
			m_objViewer.m_lbStart.Text="";
			m_objViewer.m_txtAmount.Clear();
			m_objViewer.m_txtCard.Clear();
			m_objViewer.m_txtDept.Clear();
			m_objViewer.m_txtDiagFee.Clear();
			m_objViewer.m_txtDoc.Clear();
			m_objViewer.m_txtName.Clear();
			m_objViewer.m_txtPatType.Clear();
			m_objViewer.m_txtRegFee.Clear();
			m_objViewer.m_txtRegType.Clear();
			m_objViewer.m_txtCard.Focus();
			m_objViewer.m_lvItem.Clear();
			m_objViewer.m_txtRegisterNo.Clear();
			//m_objViewer.m_lsvRegDetail.Clear();
			for(int i1=0;i1<this.m_objViewer.m_lsvRegDetail.Items.Count;i1++)
			{
				this.m_objViewer.m_lsvRegDetail.Items[i1].SubItems[3].Text = "0";
				this.m_objViewer.m_lsvRegDetail.Items[i1].SubItems[4].Text = "0";
			}
			
		}
		//��ǰ����������
		/// <summary>
		/// ��ǰ����������
		/// </summary>
		/// <returns></returns>
		public DateTime m_ServDate()
		{
			DateTime DTP;
            DTP = DateTime.Now;
			return DTP;
		}
		#endregion

		#region ȡ�õ�ǰ������ʱ���
		/// <summary>
		/// ȡ�õ�ǰ������ʱ���
		/// </summary>
		/// <returns></returns>
		public int m_GetSerPerio()
		{
			int intPerio=0;
			int sevTime=DateTime.Now.Hour;
			if(sevTime>=12 && sevTime<18)
				intPerio=1; //����
			else
			{
				if(sevTime>=18)
					intPerio=2; //����
				else
					intPerio=0; //����
			}
			return intPerio;
		}
		#endregion

		#region ����ComboBox
		public void m_FillComboBox()
		{
			//ʱ���
			m_objViewer.m_cboSeg.Items.Clear();
			m_objViewer.m_cboSeg.Items.Add("����");
			m_objViewer.m_cboSeg.Items.Add("����");
			m_objViewer.m_cboSeg.Items.Add("����");
			//�Ա�
			m_objViewer.m_cboSex.Items.Clear();
			m_objViewer.m_cboSex.Items.Add("��");
			m_objViewer.m_cboSex.Items.Add("Ů");
		}
		#endregion

		#region ����Ƿ��е�ǰʱ�ε��Ű�
		public void m_CheckPlan()
		{
			bool isExits=false;
			isExits=clsDomain.m_bnlCheckPlanByDatePerio(m_objViewer.m_dtpPreTime.Value.ToShortDateString(),m_objViewer.m_cboSeg.Text);
			if(!isExits)
				MessageBox.Show(m_objViewer.m_cboSeg.Text + " û���Ű��¼���뵽��Ӧģ��ά����","��ʾ");

		}
		#endregion

		#region ���lvwItem
		public void m_GetlvwItem()
		{
			m_objViewer.Cursor=Cursors.WaitCursor;
			m_objViewer.m_lvItem.Tag=m_objViewer.ActiveControl.Name;
			switch(m_objViewer.ActiveControl.Name)
			{
				case "m_txtPatType":
					this.FillPatType();
					break;
				case "m_txtRegType":
					this.FillRegType();
					break;
				case "m_txtDept":
					this.FillDept();
					break;
				case "m_txtDoc":
					this.FillDoc();
					break;
			}
			if(m_objViewer.m_lvItem.Items.Count>0)
				m_objViewer.m_lvItem.Items[0].Selected=true;
			m_objViewer.Cursor=Cursors.Default;
		}
		private void FillPatType()
		{
			clsPatientType_VO[] objResultArr=null;
			m_objViewer.m_lvItem.Columns.Clear();
			m_objViewer.m_lvItem.Columns.Add("���",60,HorizontalAlignment.Center);
			m_objViewer.m_lvItem.Columns.Add("��������",100,HorizontalAlignment.Center);
			//m_objViewer.m_lvItem.Columns.Add("�Ը�����",100,HorizontalAlignment.Center);
			m_objViewer.m_lvItem.Items.Clear();
			long lngRes = clsDomain.m_lngGetPatType(out objResultArr);

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lvw=null;
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lvw=new ListViewItem(objResultArr[i1].m_strPayTypeID);
						lvw.SubItems.Add(objResultArr[i1].m_strPayTypeName);
						//						decimal decDis=objResultArr[i1].m_decDiscount*100;
						//						lvw.SubItems.Add(decDis.ToString().Trim()+"%");
						lvw.Tag=objResultArr[i1];
						m_objViewer.m_lvItem.Items.Add(lvw);
					}
				}
			}
		}
		private void FillRegType()
		{
			clsRegisterType_VO[] objResultArr=null;
			
			m_objViewer.m_lvItem.Columns.Clear();
			m_objViewer.m_lvItem.Columns.Add("���",60,HorizontalAlignment.Center);
			m_objViewer.m_lvItem.Columns.Add("�Һ�����",100,HorizontalAlignment.Center);
			//			m_objViewer.m_lvItem.Columns.Add("�Һŷ�",60,HorizontalAlignment.Center);
			//			m_objViewer.m_lvItem.Columns.Add("���",60,HorizontalAlignment.Center);
			m_objViewer.m_lvItem.ResumeLayout(false);
			m_objViewer.m_lvItem.Items.Clear();
			long lngRes = clsDomain.m_lngGetRegType(out objResultArr);

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lvw=null;
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lvw=new ListViewItem(objResultArr[i1].m_strRegisterTypeID);
						lvw.SubItems.Add(objResultArr[i1].m_strRegisterTypeName);
						//						lvw.SubItems.Add(objResultArr[i1].m_decRegPay.ToString());
						//						lvw.SubItems.Add(objResultArr[i1].m_decDiagPay.ToString());
						lvw.Tag=objResultArr[i1];
						m_objViewer.m_lvItem.Items.Add(lvw);
					}
				}
			}
		}
		private void FillDept()
		{
			clsDepartmentVO[] objResultArr=null;
			m_objViewer.m_lvItem.Columns.Clear();
			m_objViewer.m_lvItem.Columns.Add("���",60,HorizontalAlignment.Center);
			m_objViewer.m_lvItem.Columns.Add("��������",100,HorizontalAlignment.Center);
			m_objViewer.m_lvItem.ResumeLayout(false);
			m_objViewer.m_lvItem.Items.Clear();
			long lngRes = clsDomain.m_lngGetPlanDepByDate(clsRegister.m_strRegisterDate,m_objViewer.m_cboSeg.Text,out objResultArr);

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lvw=null;
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lvw=new ListViewItem(objResultArr[i1].strDeptID);
						lvw.SubItems.Add(objResultArr[i1].strDeptName);
						lvw.Tag=objResultArr[i1];
						m_objViewer.m_lvItem.Items.Add(lvw);
					}
				}
			}
		}
		private void FillDoc()
		{
			clsEmployeeVO[] objResultArr=null;
			m_objViewer.m_lvItem.Columns.Clear();
			m_objViewer.m_lvItem.Columns.Add("���",60,HorizontalAlignment.Center);
			m_objViewer.m_lvItem.Columns.Add("ҽ������",100,HorizontalAlignment.Center);
			m_objViewer.m_lvItem.Columns.Add("�޺�",60,HorizontalAlignment.Center);
			m_objViewer.m_lvItem.ResumeLayout(false);
			m_objViewer.m_lvItem.Items.Clear();
			string strDate=clsMain.IsNullToString(clsRegister.m_strRegisterDate,null);
			string strDeptID=clsRegister.m_objDiagDept.strDeptID;
			string strRegType=clsRegister.m_strRegisterType.m_strRegisterTypeID;
			long lngRes=0;
			if(strDeptID=="")
				strDeptID=null;
			lngRes = clsDomain.m_lngGetOPDoctorList(strDeptID,out objResultArr);

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lvw=null;
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lvw=new ListViewItem(objResultArr[i1].strEmpID);
						lvw.SubItems.Add(objResultArr[i1].strName);
						lvw.SubItems.Add(objResultArr[i1].intStatus.ToString());
						lvw.Tag=objResultArr[i1];
						m_objViewer.m_lvItem.Items.Add(lvw);
						
					}
				}
			}
		}
		#endregion

		#region ���lvwItem�������¼�
		public void m_lvwItemClick()
		{
			if(m_objViewer.m_lvItem.Items.Count==0 || m_objViewer.m_lvItem.SelectedItems.Count==0)
				return;
			if(m_objViewer.m_lvItem.SelectedItems[0].Tag==null)
				return;
			switch(m_objViewer.m_lvItem.Tag.ToString())
			{
				case "m_txtPatType":
					clsRegister.m_strPayType=(clsPatientType_VO)m_objViewer.m_lvItem.SelectedItems[0].Tag;
					this.m_objViewer.m_txtPatType.Tag = clsRegister.m_strPayType.m_strPayTypeID;
					m_objViewer.m_txtPatType.Text=clsRegister.m_strPayType.m_strPayTypeName;
					if(clsMain.IsNullToString(clsRegister.m_strRegisterType.m_strRegisterTypeID,null)!="")						
						m_GetCurPay();
					m_objViewer.m_txtDept.Focus();
					break;
				case "m_txtRegType":
					clsRegister.m_strRegisterType = (clsRegisterType_VO)m_objViewer.m_lvItem.SelectedItems[0].Tag;
					this.m_objViewer.m_txtRegType.Tag = clsRegister.m_strRegisterType.m_strRegisterTypeID;
					this.m_objViewer.m_txtRegType.Text = clsRegister.m_strRegisterType.m_strRegisterTypeName;
					if(clsMain.IsNullToString(clsRegister.m_strPayType.m_strPayTypeID,null)!="")
					{
						//this.CalFee();
						m_GetCurPay();
						m_objViewer.m_txtDoc.Focus();
					}
					else
						m_objViewer.m_txtDoc.Focus();//m_objViewer.m_txtPatType.Focus();
					break;
				case "m_txtDept":
					clsRegister.m_objDiagDept=(clsDepartmentVO)m_objViewer.m_lvItem.SelectedItems[0].Tag;
					m_objViewer.m_txtDept.Text=clsRegister.m_objDiagDept.strDeptName;
					m_objViewer.m_txtRegType.Focus();
					break;
				case "m_txtDoc":
					if(FilltxtByDoc())
						m_objViewer.m_btnSave.Focus();
					break;
			}
		}
		private void CalFee()
		{
			m_objViewer.m_txtRegType.Text=clsRegister.m_strRegisterType.m_strRegisterTypeName;
			string RegTypeID=clsRegister.m_strRegisterType.m_strRegisterTypeID;
			string PatTypeID=clsRegister.m_strPayType.m_strPayTypeID;
			if(clsMain.IsNullToString(PatTypeID,null)=="" || clsMain.IsNullToString(RegTypeID,null)=="")
			{
				m_objViewer.m_txtRegFee.Clear();              
				m_objViewer.m_txtDiagFee.Clear();
				m_objViewer.m_txtAmount.Clear();
				clsRegister.m_decRegisterPay=0;
				clsRegister.m_decDiagPay=0;
				return;
			}
			clsPatRegFee_VO clsVO;
			long lngRes=clsDomain.m_lngFindPatRegFeeByID(PatTypeID,RegTypeID,out clsVO);
			if(clsVO.m_strRegisterTypeID==null || clsVO.m_strRegisterTypeID=="")
			{
				m_objViewer.m_txtRegFee.Clear();              
				m_objViewer.m_txtDiagFee.Clear();
				m_objViewer.m_txtAmount.Clear();
				clsRegister.m_decRegisterPay=0;
				clsRegister.m_decDiagPay=0;
				return;
			}
			else
			{
				m_objViewer.m_txtRegFee.Text=clsVO.m_decRegFee.ToString();
				m_objViewer.m_txtDiagFee.Text=clsVO.m_decDiagFee.ToString();
				clsRegister.m_decRegisterPay=clsVO.m_decRegFee;
				clsRegister.m_decDiagPay=clsVO.m_decDiagFee;
				m_objViewer.m_txtAmount.Text=Convert.ToString(clsVO.m_decRegFee+clsVO.m_decDiagFee);
			}
		}
		private bool FilltxtByDoc()
		{
			long lngRes=0;
			if(m_objViewer.m_lvItem.SelectedItems[0].Tag==null)
				return false;
			clsOPDoctorPlan_VO objResult=new clsOPDoctorPlan_VO();
			clsRegister.m_objDiagDoctor=(clsEmployeeVO)m_objViewer.m_lvItem.SelectedItems[0].Tag;
			int lngLimitNo=clsRegister.m_objDiagDoctor.intStatus;
			if(lngLimitNo==0)
			{
				if(MessageBox.Show("��ҽ���Ѵﵽ�޺ţ������Һ���","",MessageBoxButtons.YesNo)==DialogResult.No)
					return false;
			}
			else
			{
				int intTakeCount=clsDomain.m_GetDocTakeCout(clsRegister.m_objDiagDoctor.strEmpID,
					clsRegister.m_strRegisterDate);
				if(intTakeCount>=lngLimitNo)
				{
					if(MessageBox.Show("��ҽ���Ѵﵽ�޺ţ������Һ���","",MessageBoxButtons.YesNo)==DialogResult.No)
						return false;
				}
			}
			m_objViewer.m_txtDoc.Text=clsRegister.m_objDiagDoctor.strName;
			lngRes=clsDomain.m_lngGetDocPlan(clsRegister.m_strRegisterDate,m_objViewer.m_cboSeg.Text,
				clsRegister.m_objDiagDoctor.strEmpID,out objResult);

			if(lngRes>0 && clsMain.IsNullToString(objResult.m_strOPDrPlanID,null)!="")
			{
				if(clsMain.IsNullToString(clsRegister.m_objDiagDept.strDeptID,null)=="") //û��������
				{
					clsRegister.m_objDiagDept=objResult.m_objOPDept;
					m_objViewer.m_txtDept.Text=clsRegister.m_objDiagDept.strDeptName;
					clsRegister.m_strRegisterType=objResult.m_objRegisterType;
					m_objViewer.m_txtRegType.Text=clsRegister.m_strRegisterType.m_strRegisterTypeName;
				}
				m_objViewer.m_lbStart.Text=objResult.m_strStartTime;
				m_objViewer.m_lbEnd.Text=objResult.m_strEndTime;
				m_objViewer.m_lbRoom.Text=objResult.m_strOPAddress;
				//this.CalFee();
			}
			else
			{
				clsRegister.m_objDiagDept.strDeptID="";
				m_objViewer.m_txtDept.Clear();
				clsRegister.m_strRegisterType.m_strRegisterTypeID="";
				m_objViewer.m_txtRegType.Clear();
				m_objViewer.m_txtDept.Focus();
				return false;
			}
			return true;
		}
		#endregion

		#region �ı���ı�ʱ�������¼�
		public void m_txtChange()
		{
			if(m_objViewer.ActiveControl==null)
				return;

			if(m_objViewer.ActiveControl.Name=="m_txtRegFee" || m_objViewer.ActiveControl.Name=="m_txtDiagFee")
			{
				decimal decReg=decimal.Parse(clsMain.IsNullToString(m_objViewer.m_txtRegFee.Text,"0"));
				decimal decDiag=decimal.Parse(clsMain.IsNullToString(m_objViewer.m_txtDiagFee.Text,"0"));
				decimal decAmount=decReg+decDiag;
				clsRegister.m_decRegisterPay=decReg;
				clsRegister.m_decDiagPay=decDiag;
				if(decAmount!=0)
					m_objViewer.m_txtAmount.Text=decAmount.ToString();
				else
					m_objViewer.m_txtAmount.Clear();
				return;
			}
            
			switch(m_objViewer.ActiveControl.Name)
			{
				case "m_txtPatType":
					if(m_objViewer.ActiveControl.Text=="")
						clsRegister.m_strPayType.m_strPayTypeID="";
					else
						//m_GetCurPay();
						this.m_FindLvw(m_objViewer.m_txtPatType.Text);
					break;
				case "m_txtRegType":
					if(m_objViewer.ActiveControl.Text=="")
						clsRegister.m_strRegisterType.m_strRegisterTypeID="";
					else
						this.m_FindLvw(m_objViewer.m_txtRegType.Text);
					//m_GetCurPay();
					break;
				case "m_txtDept":
					if(m_objViewer.ActiveControl.Text=="")
						clsRegister.m_objDiagDept.strDeptID="";
					else
						this.m_FindLvw(m_objViewer.m_txtDept.Text);
					break;
				case "m_txtDoc":
					if(m_objViewer.ActiveControl.Text=="")
						clsRegister.m_objDiagDoctor.strEmpID="";
					else
						this.m_FindLvw(m_objViewer.m_txtDoc.Text);
					break;
			}
		}
		#endregion

		#region �ı���õ�����
		public void m_txtFocus()
		{
			switch(m_objViewer.m_lvItem.Tag.ToString())
			{
				case "m_txtPatType":
					m_objViewer.m_txtPatType.Focus();
					break;
				case "m_txtRegType":
					m_objViewer.m_txtRegType.Focus();
					break;
				case "m_txtDept":
					m_objViewer.m_txtDept.Focus();
					break;
				case "m_txtDoc":
					m_objViewer.m_txtDoc.Focus();
					break;
			}
		}
		#endregion

		#region ����Lvw�е�ֵ
		public void m_FindLvw(string strValues)
		{
			if(m_objViewer.m_lvItem.Items.Count==0)
				return;
			int i=0;
			if(clsMain.IsNumber(strValues))
				i=clsMain.FindItemByValues(m_objViewer.m_lvItem,0,strValues);
			else
				i=clsMain.FindItemByValues(m_objViewer.m_lvItem,1,strValues);
			m_objViewer.m_lvItem.SelectedItems[0].Selected=false;
			if(i>0)
				m_objViewer.m_lvItem.Items[i].Selected=true;
			else
				m_objViewer.m_lvItem.Items[0].Selected=true;
		}
		#endregion

		#region ��ȡ��ǰ�������͵ĹҺŷ���
		/// <summary>
		/// ��ȡ��ǰ�������͵ĹҺŷ���
		/// </summary>
		public void m_GetCurPay()
		{
//			decimal mny = 0;
//			this.m_objViewer.m_lsvRegDetail.Items.Clear();
//			for(int i=0;i<this.m_clsRegisterPayVO.Length;i++)
//			{
//				if(this.m_clsRegisterPayVO[i].m_strPAYTYPEID_CHR.Trim() == this.m_objViewer.m_txtPatType.Tag.ToString().Trim()
//					&& this.m_clsRegisterPayVO[i].m_strREGISTERTYPEID_CHR.Trim() == this.m_objViewer.m_txtRegType.Tag.ToString().Trim())
//				{
//					ListViewItem lvi = new ListViewItem();
//					lvi.Text = "0";
//					lvi.SubItems.Add(this.m_clsRegisterPayVO[i].m_strCHARGEID_CHR);
//					lvi.SubItems.Add(this.m_clsRegisterPayVO[i].m_strCHARGENAME_CHR);
//					lvi.SubItems.Add(Convert.ToString(this.m_clsRegisterPayVO[i].m_dblPAYMENT_MNY));
//					lvi.SubItems.Add(Convert.ToString(this.m_clsRegisterPayVO[i].m_fltDISCOUNT_DEC));
//					lvi.SubItems.Add(Convert.ToString(this.m_clsRegisterPayVO[i].m_strMEMO_VCHR));
//					this.m_objViewer.m_lsvRegDetail.Items.Add(lvi);
//					mny += decimal.Parse(this.m_clsRegisterPayVO[i].m_dblPAYMENT_MNY.ToString())*decimal.Parse(this.m_clsRegisterPayVO[i].m_fltDISCOUNT_DEC.ToString());
//				}
//			}
//			if(this.m_objViewer.m_lsvRegDetail.Items.Count>0)
//			{
//				this.m_objViewer.m_lsvRegDetail.Items[0].Selected = true;
//			}
//			
//			m_objViewer.m_txtRegFee.Text = mny.ToString();
			this.m_Calculate();
		}
		/// <summary>
		/// �ܼƽ��
		/// </summary>
		public void m_FigureUpPay()
		{
			decimal mny = 0;
			for(int i=0;i<this.m_objViewer.m_lsvRegDetail.Items.Count;i++)
			{
				try
				{
					mny += decimal.Parse(this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[3].Text)*decimal.Parse(this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[4].Text);
				}
				catch{}
			}
			m_objViewer.m_txtRegFee.Text = mny.ToString();
			m_Calculate();
		}
		/// <summary>
		/// �������
		/// </summary>
		public void m_Calculate()
		{
			if(m_objViewer.m_txtAmount.Text.Trim() == "" || m_objViewer.m_txtRegFee.Text.Trim() == "") return;
			m_objViewer.m_txtDiagFee.Text = Convert.ToString(decimal.Parse(clsMain.IsNullToString(m_objViewer.m_txtAmount.Text,"0")) - decimal.Parse(clsMain.IsNullToString(m_objViewer.m_txtRegFee.Text,"0")));
		}
		#endregion

		#region ���Ҳ���䲡�˵�����
		public void m_FindPat()
		{
			if(m_objViewer.m_txtCard.Text=="")
				return;
			long lngRes=0;
			clsPatient_VO objResult=new clsPatient_VO();
			string DepName=null;
			string doctorName=null;
			string registerDate=null;
			lngRes=clsDomain.m_lngGetPatByCard(m_objViewer.m_txtCard.Text,out objResult,DateTime.Now.ToShortDateString(),out DepName,out doctorName,out registerDate);
			if(lngRes>0 && clsMain.IsNullToString(objResult.strPatientID,null)!="")
			{
				m_objViewer.m_txtName.Text=objResult.strName;
				if(clsMain.IsNullToString(objResult.strBirthDate,null)!="")
					m_objViewer.m_dtpBirth.Value=Convert.ToDateTime(objResult.strBirthDate);
				m_objViewer.m_txtPatType.Text=objResult.objPatType.m_strPayTypeName;
				m_objViewer.m_txtPatType.Tag = (object)objResult.objPatType.m_strPayTypeID;
				clsRegister.m_strPayType=objResult.objPatType;
				clsRegister.m_objPatientCard.m_strCardID=objResult.strPatientCardID;
				clsRegister.strOptimes = objResult.m_strOPTIMES_INT;
				m_objViewer.m_cboSex.Text=objResult.strSex;
				m_objViewer.m_txtDept.Focus();
			}
			else
			{
				if(MessageBox.Show("���޸ÿ��Ų�����Ϣ���Ƿ�������","��ʾ",MessageBoxButtons.YesNo)==DialogResult.Yes)
				{
					m_NewCard();
				}
				else
				{
					m_objViewer.m_txtCard.Focus();
				}

				//m_objViewer.m_txtCard.Text="";
			}
		}
		#endregion

		#region �������� zlc 2004-7-27
		public void m_NewCard()
		{
//			string patientcardid = "";
//			if(new com.digitalwave.iCare.gui.Patient.frmPatient().m_diaGetPatientCardID("1",out patientcardid,this.m_objViewer.m_txtCard.Text.Trim()) == DialogResult.OK)
//			{
//				this.m_objViewer.m_txtCard.Text = patientcardid;
//				this.m_FindPat();
//			}
//			else
//			{
//				m_objViewer.m_txtCard.Focus();
//			}
		}
		#endregion

		#region ��ȡ���ñ� zlc 2004-8-3
		public clsRegisterPay[] m_clsRegisterPayVO;
		public void m_GetPay()
		{
			m_clsRegisterPayVO = new clsRegisterPay[0];
			clsDomain.m_lngGetPay(out m_clsRegisterPayVO);
		}
		#endregion

		#region �޸Ľ����Ż�
		public int m_intModifyPrice()
		{
			for(int i=0;i<this.m_objViewer.m_lsvRegDetail.Items.Count;i++)
			{
				if(this.m_objViewer.m_lsvRegDetail.Items[i].Selected == true)
				{
					if(this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[5].Text.IndexOf("�����޸�")>=0) 
					{					
						return i;
					}
					this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[3].Text =Convert.ToString(decimal.Parse(clsMain.IsNullToString(m_objViewer.m_txtChangeCharge.Text,"0")));
					this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[4].Text = Convert.ToString(decimal.Parse(clsMain.IsNullToString(m_objViewer.m_txtChangeDisCount.Text,"0")));
					m_FigureUpPay();
					return i;
				}
			}
			
			return 0;
		}
		public void m_getCurPrice()
		{
			for(int i=0;i<this.m_objViewer.m_lsvRegDetail.Items.Count;i++)
			{
				if(this.m_objViewer.m_lsvRegDetail.Items[i].Selected == true)
				{
					if(this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[5].Text.IndexOf("�����޸�")>=0) 
					{					
						m_objViewer.m_txtChangeCharge.ReadOnly = true;
						m_objViewer.m_txtChangeDisCount.ReadOnly = true;
					}
					else
					{
						m_objViewer.m_txtChangeCharge.ReadOnly = false;
						m_objViewer.m_txtChangeDisCount.ReadOnly = false;
					}
					m_objViewer.m_txtChangeCharge.Text = this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[3].Text ;
					m_objViewer.m_txtChangeDisCount.Text = this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[4].Text;
					return ;
				}
			}
			
		}
		public void m_UpDown(int index,System.Windows.Forms.KeyEventArgs e)
		{
			if(index == this.m_objViewer.m_lsvRegDetail.Items.Count-1 && e.KeyCode == Keys.Down)
			{
				this.m_objViewer.m_lsvRegDetail.Items[index].Selected = false;
				this.m_objViewer.m_lsvRegDetail.Items[0].Selected = true;
			}
			if(index == 0 && e.KeyCode == Keys.Up)
			{
				this.m_objViewer.m_lsvRegDetail.Items[0].Selected = false;
				this.m_objViewer.m_lsvRegDetail.Items[this.m_objViewer.m_lsvRegDetail.Items.Count-1].Selected = true;
			}
			if(index>0 && index<=this.m_objViewer.m_lsvRegDetail.Items.Count-1 && e.KeyCode == Keys.Up)
			{
				this.m_objViewer.m_lsvRegDetail.Items[index].Selected = false;
				this.m_objViewer.m_lsvRegDetail.Items[index-1].Selected = true;
			}
			if(index>=0 && index<this.m_objViewer.m_lsvRegDetail.Items.Count-1 && e.KeyCode == Keys.Down)
			{
				this.m_objViewer.m_lsvRegDetail.Items[index].Selected = false;
				this.m_objViewer.m_lsvRegDetail.Items[index+1].Selected = true;
			}
		}
		#endregion

		#region ���뷢��ʱ�ķ���
		public void m_MoveCardPay(CheckBox chk)
		{
			//			switch(chk.Name)
			//			{
			//				case "m_chkNeedNotCard":
			//					if(chk.Checked)
			//					{
			//						this.m_objViewer.m_ls
			//					}
			//					break;
			//				case "m_chkNeedNotfalill":
			//					break;
			//			}
		}
		#endregion
		
		#region ��ӡ�Һ�Ʊ 
		private string m_strRegisterID="";
        /// <summary>
        /// ��ӡ�Һ�Ʊ 
        /// </summary>
		public void m_PrintRegister()
		{
            if ((string)this.m_objViewer.m_dtgRegister.Tag == "m_dvRegister")
            {
                if (this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Count == 0) return;
                if (((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["�Һ��˹���"].ToString() == "�ܽ��")
                {
                    MessageBox.Show("������Ч�ĹҺ����ݣ�");
                    return;
                }
            }
            else
            {
                if (this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Count == 0) return;
                if (((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["�Һ��˹���"].ToString() == "�ܽ��")
                {
                    MessageBox.Show("������Ч�ĹҺ����ݣ�");
                    return;
                }
 
            }

            #region new code
            string OldNo = "";

            if ((string)this.m_objViewer.m_dtgRegister.Tag == "m_dvRegisterfind")
            {
                OldNo = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["��Ʊ��"].ToString();
            }
            else
            {
                OldNo = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["��Ʊ��"].ToString();
            }

            frmRegisterRepeatPrt f = new frmRegisterRepeatPrt(OldNo);
            if (f.ShowDialog() == DialogResult.OK)
            {
                DataTable dtbSource = new DataTable("printtemptable");
                dtbSource.Columns.Add("varchar1");
                dtbSource.Columns.Add("patientname");
                dtbSource.Columns.Add("date");
                dtbSource.Columns.Add("orderno");
                dtbSource.Columns.Add("registerno");
                dtbSource.Columns.Add("address");
                dtbSource.Columns.Add("registertype");
                dtbSource.Columns.Add("strDiagdept");
                dtbSource.Columns.Add("strEmpt");
                dtbSource.Columns.Add("doctorName");
                dtbSource.Columns.Add("patienCard");
                dtbSource.Columns.Add("txtNO");
                dtbSource.Columns.Add("decimal1", typeof(decimal));
                dtbSource.Columns.Add("decimal2", typeof(decimal));
                dtbSource.Columns.Add("txtAvailDays");
                dtbSource.Columns.Add("txtRepeatNo");
                dtbSource.Rows.Add(new object[] { });
                dtbSource.Rows[0][12] = 0;
                dtbSource.Rows[0][13] = 0;

                if ((string)this.m_objViewer.m_dtgRegister.Tag == "m_dvRegisterfind")
                {
                    Decimal meney = 0;
                    Decimal meney2 = 0;
                    meney = Convert.ToDecimal(((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)[21].ToString());
                    meney2 = Convert.ToDecimal(((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)[22].ToString());
                    string strDate = "";
                    if (((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["ԤԼ����"].ToString().Trim() != "")
                    {
                        strDate = "ԤԼ" + ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["ԤԼ����"].ToString().Trim();
                    }
                    else
                    {
                        strDate = Convert.ToDateTime(((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["�Һ�����"].ToString()).ToShortDateString();
                    }

                    dtbSource.Rows[0][0] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["��������"].ToString();
                    dtbSource.Rows[0][1] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["��������"].ToString();
                    dtbSource.Rows[0][2] = strDate;
                    dtbSource.Rows[0][3] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["�����"].ToString();
                    dtbSource.Rows[0][4] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["��ˮ��"].ToString();
                    dtbSource.Rows[0][5] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["�����ַ"].ToString();
                    dtbSource.Rows[0][6] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["�Һ�����"].ToString();
                    dtbSource.Rows[0][7] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["��������"].ToString();
                    dtbSource.Rows[0][8] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["�Һ��˹���"].ToString();
                    dtbSource.Rows[0][9] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["ҽ������"].ToString();
                    dtbSource.Rows[0][10] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["���ƿ���"].ToString();

                    dtbSource.Rows[0][12] = meney;
                    dtbSource.Rows[0][13] = meney2;
                    dtbSource.Rows[0][14] = AvailDays;

                    if (f.PrnType == "1")
                    {
                        dtbSource.Rows[0][11] = OldNo;
                        dtbSource.Rows[0][15] = "";
                    }
                    else if (f.PrnType == "2")
                    {
                        dtbSource.Rows[0][11] = f.NewNo;
                        dtbSource.Rows[0][15] = "*REPEAT(" + OldNo + ")*";

                        long l = clsDomain.m_lngSaveinvorepeatprninfo("2", ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind, null].Current)["�Һ�ID"].ToString(), OldNo, f.NewNo, this.m_objViewer.LoginInfo.m_strEmpID);
                        if (l <= 0)
                        {
                            MessageBox.Show("�����ش�Һŷ�Ʊ��Ϣʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                    }
                }
                else
                {
                    Decimal meney = 0;
                    Decimal meney2 = 0;
                    meney = Convert.ToDecimal(((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)[21].ToString());
                    if (((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)[22].ToString().Trim() != string.Empty)
                    {
                        meney2 = Convert.ToDecimal(((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)[22].ToString());
                    }
                    string strDate = "";
                    if (((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["ԤԼ����"].ToString().Trim() != "")
                    {
                        strDate = "ԤԼ" + ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["ԤԼ����"].ToString().Trim();
                    }
                    else
                    {
                        strDate = Convert.ToDateTime(((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["�Һ�����"].ToString()).ToShortDateString();
                    }

                    dtbSource.Rows[0][0] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["��������"].ToString();
                    dtbSource.Rows[0][1] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["��������"].ToString();
                    dtbSource.Rows[0][2] = strDate;
                    dtbSource.Rows[0][3] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["�����"].ToString();
                    dtbSource.Rows[0][4] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["��ˮ��"].ToString();
                    dtbSource.Rows[0][5] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["�����ַ"].ToString();
                    dtbSource.Rows[0][6] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["�Һ�����"].ToString();
                    dtbSource.Rows[0][7] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["��������"].ToString();
                    dtbSource.Rows[0][8] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["�Һ��˹���"].ToString();
                    dtbSource.Rows[0][9] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["ҽ������"].ToString();
                    dtbSource.Rows[0][10] = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["���ƿ���"].ToString();

                    dtbSource.Rows[0][12] = meney;
                    dtbSource.Rows[0][13] = meney2;
                    dtbSource.Rows[0][14] = AvailDays;

                    if (f.PrnType == "1")
                    {
                        dtbSource.Rows[0][11] = OldNo;
                        dtbSource.Rows[0][15] = "";
                    }
                    else if (f.PrnType == "2")
                    {
                        dtbSource.Rows[0][11] = f.NewNo;
                        dtbSource.Rows[0][15] = "*REPEAT(" + OldNo + ")*";

                        long l = clsDomain.m_lngSaveinvorepeatprninfo("2", ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister, null].Current)["�Һ�ID"].ToString(), OldNo, f.NewNo, this.m_objViewer.LoginInfo.m_strEmpID);
                        if (l <= 0)
                        {
                            MessageBox.Show("�����ش�Һŷ�Ʊ��Ϣʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                    }
                }

                new FrmShowPrint().m_PrintRegister11(dtbSource);
            }
            #endregion
        }
		#endregion

        #region ��ȡ�Һ���Ч����
        /// <summary>
        /// ��ȡ�Һ���Ч����
        /// </summary>
        private string AvailDays = "һ";
        /// <summary>
        /// ��ȡ�Һ���Ч����
        /// </summary>
        public void m_mthGetAvailDays()
        {
            clsDcl_DoctorWorkstation objDoctor = new clsDcl_DoctorWorkstation();
            DataTable dt;
            long ret = objDoctor.m_lngGetWSParm("0058", out dt);		//0058 �Һ���Ч����

            if (ret > 0 && dt.Rows.Count > 0)
            {
                string[] bs = new string[10] { "һ", "��", "��", "��", "��", "��", "��", "��", "��", "ʮ" };

                AvailDays = bs[Convert.ToInt32(dt.Rows[0]["SETSTATUS_INT"]) - 1];
            }
        }
        #endregion

        #region ��鵱ǰ�������Ƿ���Ч
        private bool m_bolCheckValuePass()
		{
			bool bolReturn = true;

			//			if(clsMain.IsNullToString(clsRegister.m_objPatientCard.m_strCardID,null)=="")
			//			{
			//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
			//				bolReturn = false;
			//			}
			if(m_objViewer.m_txtName.Text.Trim() == "")
			{
				if(bolReturn)
					m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
				bolReturn = false;
			}

			if(m_objViewer.m_cboSex.SelectedIndex<0)
			{
				m_objViewer.errorProvider1.SetError(m_objViewer.m_cboSex,"��ѡ���Ա�");
				bolReturn = false;
			}
			if(clsMain.IsNullToString(clsRegister.m_strPayType.m_strPayTypeID,null) == "")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtPatType);
				bolReturn = false;
			}
			if(m_objViewer.m_cboSeg.SelectedIndex<0)
			{
				m_objViewer.errorProvider1.SetError(m_objViewer.m_cboSeg,"��ѡ��ʱ���");
				bolReturn = false;
			}
			if(!bnlChekcBirth()) //��������
				bolReturn=false;
			if(clsMain.IsNullToString(clsRegister.m_objDiagDept.strDeptID,null) == "")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtDept);
				bolReturn = false;
			}
			if(clsMain.IsNullToString(clsRegister.m_objDiagDoctor.strEmpID,null) == "" && clsMain.IsNullToString(clsRegister.m_objDiagDept.strDeptID,null) == "0002")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtDoc);
				bolReturn = false;
			}
			if(clsMain.IsNullToString(clsRegister.m_strRegisterType.m_strRegisterTypeID,null) == "")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtRegType);
				bolReturn = false;
			}
			bool bnlChk=this.bnlCheckDate();
			if(!bnlChk)
				bolReturn=false;
			if(!bolReturn)
			{
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
			}
			return bolReturn;
		}
		public bool bnlCheckDate()
		{
			bool bolReturn=true;
			if(m_objViewer.m_chkPre.Checked) //ԤԼ
			{
				DateTime PreTime=Convert.ToDateTime(m_objViewer.m_dtpPreTime.Value.ToShortDateString());
				DateTime sevTime=Convert.ToDateTime(DateTime.Now.ToShortDateString());
				if(PreTime<sevTime) //����ԤԼ��ǰ
				{
					m_objViewer.errorProvider1.SetError(m_objViewer.m_dtpPreTime,"����ԤԼ����֮ǰ�ĺ�");
					bolReturn=false;
				}
				else
				{
					if(sevTime==PreTime)//����ǵ��죬����ԤԼ��������ϵ�
					{
						m_objViewer.errorProvider1.SetError(m_objViewer.m_dtpPreTime,"");
						if(this.m_GetSerPerio()>m_objViewer.m_cboSeg.SelectedIndex) //���ܹ�ǰһ��ʱ��ε�
						{
							m_objViewer.errorProvider1.SetError(m_objViewer.m_cboSeg,"����ԤԼ��ǰʱ���֮ǰ�ĺ�");
							bolReturn=false;
						}
					}
				}
				if(bolReturn)
				{
					this.clsRegister.m_strRegisterDate=PreTime.ToShortDateString();
					m_objViewer.errorProvider1.SetError(m_objViewer.m_dtpPreTime,"");
					m_objViewer.errorProvider1.SetError(m_objViewer.m_cboSeg,"");
				}
			}
			else
			{
				m_objViewer.errorProvider1.SetError(m_objViewer.m_dtpPreTime,"");
				m_objViewer.errorProvider1.SetError(m_objViewer.m_cboSeg,"");
				this.clsRegister.m_strRegisterDate=DateTime.Now.ToShortDateString();
			}
			return bolReturn;
		}
		public bool bnlChekcBirth()
		{
			bool bolReturn=true;
			if(m_objViewer.m_dtpBirth.Value>DateTime.Now)
			{
				m_objViewer.errorProvider1.SetError(m_objViewer.m_dtpBirth,"�������ڲ��ܴ��ڵ�ǰ��ʱ��");
				bolReturn = false;
			}
			else
				m_objViewer.errorProvider1.SetError(m_objViewer.m_dtpBirth,"");
			return bolReturn;   
		}
		#endregion

		#region ��ѯ�Һ�Ʊ zlc 2004-7-30
		public void m_lngQuy()
		{
		}
		#endregion

		#region VO���TEXTBOX zlc
		public void m_FillTextbox(clsPatientRegister_VO objreg)
		{
			this.m_objViewer.m_txtCard.Text    = objreg.m_objPatientCard.m_strCardID;
			this.m_objViewer.m_txtCard.Tag     = objreg.m_objPatientCard.m_strCardID;
			this.m_objViewer.m_txtName.Text    = objreg.m_clsPatientVO.strName;
			this.m_objViewer.m_txtName.Tag     = objreg.m_clsPatientVO.strPatientID;
			this.m_objViewer.m_cboSex.Text     =  objreg.m_clsPatientVO.strSex;
			this.m_objViewer.m_dtpBirth.Value  = Convert.ToDateTime(objreg.m_clsPatientVO.strBirthDate);
			
			this.m_objViewer.m_txtPatType.Text = objreg.m_strPayType.m_strPayTypeName;
			this.m_objViewer.m_txtPatType.Tag  = objreg.m_strPayType.m_strPayTypeID;

			this.m_objViewer.m_txtDept.Text    = objreg.m_objDiagDept.strDeptName;
			this.m_objViewer.m_txtDept.Tag     = objreg.m_objDiagDept.strDeptID;
			this.m_objViewer.m_txtRegType.Text = objreg.m_strRegisterType.m_strRegisterTypeName;
			this.m_objViewer.m_txtRegType.Tag  = objreg.m_strRegisterType.m_strRegisterTypeID;
			this.m_objViewer.m_txtDoc.Text     = objreg.m_objDiagDoctor.strFirstName;
			this.m_objViewer.m_txtDoc.Tag      = objreg.m_objDiagDoctor.strEmpID;
			
			this.m_objViewer.m_lbStart.Text    = objreg.m_clsOPDoctorPlanVO.m_strStartTime;
			this.m_objViewer.m_lbEnd.Text      = objreg.m_clsOPDoctorPlanVO.m_strEndTime;
			this.m_objViewer.m_lbRoom.Text     = objreg.m_clsOPDoctorPlanVO.m_strOPAddress;			
		}
		#endregion

		#region DATAGRID���TEXTBOX
		public void m_DtgFillTXT()
		{
			try
			{
				this.m_objViewer.m_txtRegisterNo.Text    = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["��ˮ��"].ToString() ;
				this.m_objViewer.m_txtCard.Text    = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["���ƿ���"].ToString() ;
				this.m_objViewer.m_txtCard.Tag     = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["���ƿ���"].ToString() ;
				this.m_objViewer.m_txtName.Text    = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["��������"].ToString() ;
				this.m_objViewer.m_txtName.Tag     = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["���˱��ID"].ToString() ;
				this.m_objViewer.m_cboSex.Text     = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�����Ա�"].ToString() ;
				this.m_objViewer.m_dtpBirth.Value  = 	Convert.ToDateTime(((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["��������"].ToString()) ;
				this.m_objViewer.m_txtPatType.Text = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�������"].ToString() ;
				this.m_objViewer.m_txtPatType.Tag  = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�������ID"].ToString() ;
				this.m_objViewer.m_txtDept.Text    = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["��������"].ToString() ;
				this.m_objViewer.m_txtDept.Tag     = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["����ID"].ToString() ;
				this.m_objViewer.m_txtRegType.Text = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�Һ�����"].ToString() ;
				this.m_objViewer.m_txtRegType.Tag  = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�Һ�����ID"].ToString() ;
				this.m_objViewer.m_txtDoc.Text     = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["ҽ������"].ToString() ;
				this.m_objViewer.m_txtDoc.Tag      = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["ҽ��ID"].ToString() ;
			
				this.m_objViewer.m_lbStart.Text    = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["��ʼʱ��"].ToString() ;
				this.m_objViewer.m_lbEnd.Text      = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["��ֹʱ��"].ToString() ;
				this.m_objViewer.m_lbRoom.Text     = 	((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�����ַ"].ToString() ;
				this.m_objViewer.m_cboSeg.Text     =    ((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["����ʱ���"].ToString() ;
			}
			catch{}
		}
		#endregion

		#region �޸�DATAGRID
		public System.Data.DataView m_dvRegister = new System.Data.DataView();
		public void m_ModifyDatagrid()
		{
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["���ƿ���"] = this.m_objViewer.m_txtCard.Text   ;
			//((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)[""] = this.m_objViewer.m_txtCard.Tag    ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["��������"] = this.m_objViewer.m_txtName.Text   ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["���˱��ID"] = this.m_objViewer.m_txtName.Tag    ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�����Ա�"] = this.m_objViewer.m_cboSex.Text    ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["��������"] = this.m_objViewer.m_dtpBirth.Value ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�������"] = this.m_objViewer.m_txtPatType.Text;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�������ID"] = this.m_objViewer.m_txtPatType.Tag ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["��������"] = this.m_objViewer.m_txtDept.Text   ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["����ID"] = this.m_objViewer.m_txtDept.Tag    ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�Һ�����"] = this.m_objViewer.m_txtRegType.Text;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�Һ�����ID"] = this.m_objViewer.m_txtRegType.Tag ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["ҽ������"] = this.m_objViewer.m_txtDoc.Text    ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["ҽ��ID"] = this.m_objViewer.m_txtDoc.Tag     ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["��ʼʱ��"] = this.m_objViewer.m_lbStart.Text;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["��ֹʱ��"] = this.m_objViewer.m_lbEnd.Text  ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�����ַ"] = this.m_objViewer.m_lbRoom.Text ;
			((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["����ʱ���"] = this.m_objViewer.m_cboSeg.Text;
//				((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)[""] = ;
//				((System.Data.DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)[""] = ;
		}
		#endregion

		#region ���VO
		public void m_FillVO(out clsPatientRegister_VO objreg)
		{
			objreg = new clsPatientRegister_VO();
			objreg.m_objDiagDept=new clsDepartmentVO();
			objreg.m_objDiagDoctor=new clsEmployeeVO();
			objreg.m_strPayType=new clsPatientType_VO();
			objreg.m_strRegisterType=new clsRegisterType_VO();
			objreg.m_objPatientCard=new clsPatientCard_VO();
			objreg.m_objRegisterEmp=new clsEmployeeVO();
			objreg.m_clsPatientVO=new clsPatientVO();
			objreg.m_clsOPDoctorPlanVO=new clsOPDoctorPlan_VO();
			if(this.m_dvRegister.Count == 0) return;
			objreg.m_strRegisterID = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�Һ�ID"].ToString();
			objreg.m_objPatientCard.m_strCardID           			= this.m_objViewer.m_txtCard.Text;
			objreg.m_objPatientCard.m_strCardID           			= this.m_objViewer.m_txtCard.Tag.ToString()    ; 
			objreg.m_clsPatientVO.strName                 			= this.m_objViewer.m_txtName.Text   ;
			objreg.m_clsPatientVO.strPatientID            			= this.m_objViewer.m_txtName.Tag.ToString()    ;
			objreg.m_clsPatientVO.strSex                  			= this.m_objViewer.m_cboSex.Text    ; 
			objreg.m_clsPatientVO.strBirthDate                      = this.m_objViewer.m_dtpBirth.Value.ToShortDateString()  ;
		
			objreg.m_strPayType.m_strPayTypeName         			= this.m_objViewer.m_txtPatType.Text ;
			objreg.m_strPayType.m_strPayTypeID           			= this.m_objViewer.m_txtPatType.Tag.ToString()  ;
	
			objreg.m_objDiagDept.strDeptName             			= this.m_objViewer.m_txtDept.Text   ;
			objreg.m_objDiagDept.strDeptID               			= this.m_objViewer.m_txtDept.Tag.ToString()     ;
			objreg.m_strRegisterType.m_strRegisterTypeName          = this.m_objViewer.m_txtRegType.Text ;
			objreg.m_strRegisterType.m_strRegisterTypeID  			= this.m_objViewer.m_txtRegType.Tag.ToString() ;
			objreg.m_objDiagDoctor.strFirstName           			= this.m_objViewer.m_txtDoc.Text     ;
			objreg.m_objDiagDoctor.strEmpID               			= this.m_objViewer.m_txtDoc.Tag.ToString()      ;
		
			objreg.m_clsOPDoctorPlanVO.m_strStartTime     			=	this.m_objViewer.m_lbStart.Text    ;
			objreg.m_clsOPDoctorPlanVO.m_strEndTime       			= this.m_objViewer.m_lbEnd.Text     ;
			objreg.m_clsOPDoctorPlanVO.m_strOPAddress     			=	this.m_objViewer.m_lbRoom.Text    ;
			objreg.m_clsOPDoctorPlanVO.m_strPlanPeriod     			=	this.m_objViewer.m_cboSeg.Text    ;
		}
		#endregion

		#region ���ҹҺű�
		/// <summary>
		/// ���ҹҺű�
		/// </summary>
		System.Data.DataTable dtSource = new System.Data.DataTable();
		public void m_QulReg()
		{
			if(this.m_dvRegister!=null||this.m_dvRegister.Table.Rows.Count==0)
			{
				this.m_objViewer.m_cmbQulType.Text = "ȫ��";
				this.m_objViewer.m_dtgRegister.DataBindings.Clear();
				try
				{
					this.m_dvRegister.Table.Clear();
				}
				catch
                {
                }

                if (this.m_objViewer.Scope == "0")
                {
                    clsDomain.m_lngQulRegByDateNew(this.m_objViewer.m_datFirstdate.Value.ToShortDateString(), this.m_objViewer.m_datLastdate.Value.ToShortDateString(), out dtSource, this.m_objViewer.LoginInfo.m_strEmpID, "0");
                }
                else if (this.m_objViewer.Scope == "1")
                {
                    clsDomain.m_lngQulRegByDateNew(this.m_objViewer.m_datFirstdate.Value.ToShortDateString(), this.m_objViewer.m_datLastdate.Value.ToShortDateString(), out dtSource, this.m_objViewer.LoginInfo.m_strEmpID, "1");
                }
                //if(dtSource.Rows.Count ==0)
                //{
                //    MessageBox.Show("û���ҵ����������ĹҺż�¼��","��ѯ��ʾ");
                //    return;
                //}
				dtSource.Columns[0].ColumnName = "�Һ�ID";
				dtSource.Columns[1].ColumnName = "���ƿ���";
				dtSource.Columns[2].ColumnName = "��Ʊ��";
				dtSource.Columns[3].ColumnName = "��ˮ��";
				dtSource.Columns[4].ColumnName = "�����";
				dtSource.Columns[5].ColumnName = "�Һ�����";
				dtSource.Columns[6].ColumnName = "��������";
				dtSource.Columns[7].ColumnName = "�����Ա�";
				dtSource.Columns[8].ColumnName = "֧������";
				dtSource.Columns[9].ColumnName = "�Һ�����";
                dtSource.Columns[10].ColumnName = "ԤԼ����";
				dtSource.Columns[11].ColumnName = "��������";
				dtSource.Columns[12].ColumnName = "ҽ������";
				dtSource.Columns[13].ColumnName = "����״̬";
				dtSource.Columns[14].ColumnName = "�Һ�״̬";
				dtSource.Columns[15].ColumnName = "�˺��˹���";
				dtSource.Columns[16].ColumnName = "�˺�����";
				dtSource.Columns[17].ColumnName = "��¼����";
				dtSource.Columns[18].ColumnName = "�������";
				dtSource.Columns[19].ColumnName = "�����ַ";
				dtSource.Columns[20].ColumnName = "�Һ��˹���";
				dtSource.Columns[21].ColumnName = "�Һŷ�";
				dtSource.Columns[22].ColumnName = "���Ʒ�";
				dtSource.Columns[25].ColumnName = "������";
				dtSource.Columns[23].ColumnName = "�Һŷ��Ż�ID";
                dtSource.Columns[24].ColumnName = "���Ʒ��Ż�ID";
                dtSource.Columns[26].ColumnName = "�������Ż�ID";
				#region ͳ�ƽ��
				dtSource.Columns.Add("�ϼƽ��");
				for(int j2=0;j2<dtSource.Rows.Count;j2++)
				{
					try 
					{
						double toMoney=0;

							if(dtSource.Rows[j2]["�Һ�״̬"].ToString()=="�˺�")
							{
								if(dtSource.Rows[j2]["�Һŷ�"].ToString()!=""&&dtSource.Rows[j2]["�Һŷ�"].ToString()!="0")
									dtSource.Rows[j2]["�Һŷ�"]="-"+dtSource.Rows[j2]["�Һŷ�"].ToString();
								if(dtSource.Rows[j2]["���Ʒ�"].ToString()!=""&&dtSource.Rows[j2]["���Ʒ�"].ToString()!="0")
									dtSource.Rows[j2]["���Ʒ�"]="-"+dtSource.Rows[j2]["���Ʒ�"].ToString();
								if(dtSource.Rows[j2]["������"].ToString()!=""&&dtSource.Rows[j2]["������"].ToString()!="0")
									dtSource.Rows[j2]["������"]="-"+dtSource.Rows[j2]["������"].ToString();
								toMoney=Convert.ToDouble(dtSource.Rows[j2]["�Һŷ�"])+Convert.ToDouble(dtSource.Rows[j2]["���Ʒ�"])+Convert.ToDouble(dtSource.Rows[j2]["������"]);
							}
							else
							{
								toMoney=Convert.ToDouble(dtSource.Rows[j2]["�Һŷ�"])+Convert.ToDouble(dtSource.Rows[j2]["���Ʒ�"])+Convert.ToDouble(dtSource.Rows[j2]["������"]);
							}
						dtSource.Rows[j2]["�ϼƽ��"]=toMoney;

					}
					catch
					{

					}
				}
                if (dtSource.Rows.Count > 0)
                {
                    #region ����ͳ����
                    DataRow newRow = dtSource.NewRow();
                    newRow["�Һ��˹���"] = "�ܽ��";
                    DataTable tolTb = new DataTable();
                    tolTb.Columns.Add("money");
                    for (int k4 = 0; k4 < 4; k4++)
                    {
                        if (k4 == 3)
                        {
                            k4 += 3;
                        }
                        DataRow newRow1 = tolTb.NewRow();
                        double tolMoney = 0;
                        for (int f6 = 0; f6 < dtSource.Rows.Count; f6++)
                        {
                            if (dtSource.Rows[f6][21 + k4].ToString() != "")
                            {
                                tolMoney += Convert.ToDouble(dtSource.Rows[f6][21 + k4].ToString());
                            }
                        }
                        newRow1["money"] = tolMoney;
                        tolTb.Rows.Add(newRow1);
                        newRow[21 + k4] = tolMoney;
                    }
                    dtSource.Rows.Add(newRow);
                    #endregion
                }

                #endregion
                    this.m_dvRegister = dtSource.DefaultView;
                    //				dtSource.Clear();
                    this.m_objViewer.m_dtgRegister.SetDataBinding(this.m_dvRegister, null);
                    this.m_objViewer.m_dtgRegister.Tag = "m_dvRegister";
                    this.m_objViewer.m_dtgRegister.m_SetDgrStyle();
                    this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["���ƿ���"].Width = 80;
                    this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["��ˮ��"].Width = 100;
                    this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["��Ʊ��"].Width = 90;
                    this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["�����"].Width = 50;
                    this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["�Һ�����"].Width = 80;
                    this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["ԤԼ����"].Width = 110;
                    this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["��¼����"].Width = 130;
                    //				this.m_objViewer.m_OmnipotenceQul.m_SetData(this.m_objViewer.m_dtgRegister,this.m_dvRegister,"��ˮ��");
                
				#region ���Combox
				if(this.m_objViewer.comboBoxField.Items.Count==0)
				{
					int j6=0;
					foreach(DataColumn dc in dtSource.Columns)
					{
						j6++;
						if(dc.ColumnName.IndexOf("ID")>=0)
						{
							continue;
						}
						if(dc.ColumnName=="�����ַ"||dc.ColumnName=="�����Ա�"||dc.ColumnName=="�����"||dc.ColumnName=="֧������")
							continue;
						else if(j6<=21)
							this.m_objViewer.comboBoxField.Items.Add(dc.ColumnName);
						if(this.m_objViewer.comboBoxField.Items.Count>0)
							this.m_objViewer.comboBoxField.SelectedIndex=0;
					}
				}
				#endregion
				m_dtRegister_PositionChanged(null,null);
			}

        }
        //#region �����ֶβ��ҹҺű�
        //public void m_mthQueryReg(string[] m_strArr)
        //{
        //    if (this.m_dvRegister != null || this.m_dvRegister.Table.Rows.Count == 0)
        //    {
              
        //        this.m_objViewer.m_dtgRegister.DataBindings.Clear();
        //        try
        //        {
        //            this.m_dvRegister.Table.Clear();
        //        }
        //        catch
        //        {
        //        }
        //        clsDomain.m_lngQulRegByFieldNew(m_strArr, out dtSource);
        //        dtSource.Columns[0].ColumnName = "�Һ�ID";
        //        dtSource.Columns[1].ColumnName = "���ƿ���";
        //        dtSource.Columns[2].ColumnName = "��Ʊ��";
        //        dtSource.Columns[3].ColumnName = "��ˮ��";
        //        dtSource.Columns[4].ColumnName = "�����";
        //        dtSource.Columns[5].ColumnName = "�Һ�����";
        //        dtSource.Columns[6].ColumnName = "��������";
        //        dtSource.Columns[7].ColumnName = "�����Ա�";
        //        dtSource.Columns[8].ColumnName = "֧������";
        //        dtSource.Columns[9].ColumnName = "�Һ�����";
        //        dtSource.Columns[10].ColumnName = "ԤԼ����";
        //        dtSource.Columns[11].ColumnName = "��������";
        //        dtSource.Columns[12].ColumnName = "ҽ������";
        //        dtSource.Columns[13].ColumnName = "����״̬";
        //        dtSource.Columns[14].ColumnName = "�Һ�״̬";
        //        dtSource.Columns[15].ColumnName = "�˺��˹���";
        //        dtSource.Columns[16].ColumnName = "�˺�����";
        //        dtSource.Columns[17].ColumnName = "��¼����";
        //        dtSource.Columns[18].ColumnName = "�������";
        //        dtSource.Columns[19].ColumnName = "�����ַ";
        //        dtSource.Columns[20].ColumnName = "�Һ��˹���";
        //        dtSource.Columns[21].ColumnName = "�Һŷ�";
        //        dtSource.Columns[22].ColumnName = "���Ʒ�";
        //        dtSource.Columns[25].ColumnName = "������";
        //        dtSource.Columns[23].ColumnName = "�Һŷ��Ż�ID";
        //        dtSource.Columns[24].ColumnName = "���Ʒ��Ż�ID";
        //        dtSource.Columns[26].ColumnName = "�������Ż�ID";
        //        #region ͳ�ƽ��
        //        dtSource.Columns.Add("�ϼƽ��");
        //        for (int j2 = 0; j2 < dtSource.Rows.Count; j2++)
        //        {
        //            try
        //            {
        //                double toMoney = 0;

        //                if (dtSource.Rows[j2]["�Һ�״̬"].ToString() == "�˺�")
        //                {
        //                    if (dtSource.Rows[j2]["�Һŷ�"].ToString() != "" && dtSource.Rows[j2]["�Һŷ�"].ToString() != "0")
        //                        dtSource.Rows[j2]["�Һŷ�"] = "-" + dtSource.Rows[j2]["�Һŷ�"].ToString();
        //                    if (dtSource.Rows[j2]["���Ʒ�"].ToString() != "" && dtSource.Rows[j2]["���Ʒ�"].ToString() != "0")
        //                        dtSource.Rows[j2]["���Ʒ�"] = "-" + dtSource.Rows[j2]["���Ʒ�"].ToString();
        //                    if (dtSource.Rows[j2]["������"].ToString() != "" && dtSource.Rows[j2]["������"].ToString() != "0")
        //                        dtSource.Rows[j2]["������"] = "-" + dtSource.Rows[j2]["������"].ToString();
        //                    toMoney = Convert.ToDouble(dtSource.Rows[j2]["�Һŷ�"]) + Convert.ToDouble(dtSource.Rows[j2]["���Ʒ�"]) + Convert.ToDouble(dtSource.Rows[j2]["������"]);
        //                }
        //                else
        //                {
        //                    toMoney = Convert.ToDouble(dtSource.Rows[j2]["�Һŷ�"]) + Convert.ToDouble(dtSource.Rows[j2]["���Ʒ�"]) + Convert.ToDouble(dtSource.Rows[j2]["������"]);
        //                }
        //                dtSource.Rows[j2]["�ϼƽ��"] = toMoney;

        //            }
        //            catch
        //            {

        //            }
        //        }
        //        if (dtSource.Rows.Count > 0)
        //        {
        //            #region ����ͳ����
        //            DataRow newRow = dtSource.NewRow();
        //            newRow["�Һ��˹���"] = "�ܽ��";
        //            DataTable tolTb = new DataTable();
        //            tolTb.Columns.Add("money");
        //            for (int k4 = 0; k4 < 4; k4++)
        //            {
        //                if (k4 == 3)
        //                {
        //                    k4 += 3;
        //                }
        //                DataRow newRow1 = tolTb.NewRow();
        //                double tolMoney = 0;
        //                for (int f6 = 0; f6 < dtSource.Rows.Count; f6++)
        //                {
        //                    if (dtSource.Rows[f6][21 + k4].ToString() != "")
        //                    {
        //                        tolMoney += Convert.ToDouble(dtSource.Rows[f6][21 + k4].ToString());
        //                    }
        //                }
        //                newRow1["money"] = tolMoney;
        //                tolTb.Rows.Add(newRow1);
        //                newRow[21 + k4] = tolMoney;
        //            }
        //            dtSource.Rows.Add(newRow);
        //            #endregion
        //        }

        //        #endregion
        //        this.m_dvRegister = dtSource.DefaultView;
        //        //				dtSource.Clear();
        //        this.m_objViewer.m_dtgRegister.SetDataBinding(this.m_dvRegister, null);
        //        this.m_objViewer.m_dtgRegister.Tag = "m_dvRegister";
        //        this.m_objViewer.m_dtgRegister.m_SetDgrStyle();
        //        this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["���ƿ���"].Width = 80;
        //        this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["��ˮ��"].Width = 100;
        //        this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["��Ʊ��"].Width = 90;
        //        this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["�����"].Width = 50;
        //        this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["�Һ�����"].Width = 80;
        //        this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["ԤԼ����"].Width = 110;
        //        this.m_objViewer.m_dtgRegister.TableStyles[0].GridColumnStyles["��¼����"].Width = 130;
        //        //				this.m_objViewer.m_OmnipotenceQul.m_SetData(this.m_objViewer.m_dtgRegister,this.m_dvRegister,"��ˮ��");

        //        #region ���Combox
        //        if (this.m_objViewer.comboBoxField.Items.Count == 0)
        //        {
        //            int j6 = 0;
        //            foreach (DataColumn dc in dtSource.Columns)
        //            {
        //                j6++;
        //                if (dc.ColumnName.IndexOf("ID") >= 0)
        //                {
        //                    continue;
        //                }
        //                if (dc.ColumnName == "�����ַ" || dc.ColumnName == "�����Ա�" || dc.ColumnName == "�����" || dc.ColumnName == "֧������"||dc.ColumnName=="�Һ�״̬")
        //                    continue;
        //                else if (j6 <= 21)
        //                    this.m_objViewer.comboBoxField.Items.Add(dc.ColumnName);
        //                if (this.m_objViewer.comboBoxField.Items.Count > 0)
        //                    this.m_objViewer.comboBoxField.SelectedIndex = 0;
        //            }
        //        }
        //        #endregion
        //        m_dtRegister_PositionChanged(null, null);
        //    }

        //}
        //#endregion 
        #region ��������
        DataTable findTB=new DataTable();
		DataView  m_dvRegisterfind=new DataView();
		public void m_lngFindDate()
		{
			if(this.m_objViewer.TextBoxValue.Text=="") return;
			if(this.m_dvRegister.Count>0)
			{
				#region ��ʼ���������ݱ�
				try
				{
					findTB=dtSource.Clone();
				}
				catch
				{
				}
				#endregion
				findTB.Clear();
				for(int i1=0;i1<this.m_dvRegister.Count;i1++)
				{
						if(this.m_dvRegister[i1][this.m_objViewer.comboBoxField.Text.Trim()].ToString().IndexOf(this.m_objViewer.TextBoxValue.Text.Trim(),0)==0)
						{
							DataRow newRow=findTB.NewRow();
							newRow["�Һ�ID"]=this.m_dvRegister[i1]["�Һ�ID"];
							newRow["���ƿ���"]=this.m_dvRegister[i1]["���ƿ���"];
							newRow["��Ʊ��"]=this.m_dvRegister[i1]["��Ʊ��"];
							newRow["��ˮ��"]=this.m_dvRegister[i1]["��ˮ��"];
							newRow["�����"]=this.m_dvRegister[i1]["�����"];
							newRow["�Һ�����"]=this.m_dvRegister[i1]["�Һ�����"];
							newRow["��������"]=this.m_dvRegister[i1]["��������"];
							newRow["�����Ա�"]=this.m_dvRegister[i1]["�����Ա�"];
							newRow["֧������"]=this.m_dvRegister[i1]["֧������"];
							newRow["�Һ�����"]=this.m_dvRegister[i1]["�Һ�����"];
                            newRow["ԤԼ����"] = this.m_dvRegister[i1]["ԤԼ����"];
							newRow["��������"]=this.m_dvRegister[i1]["��������"];
							newRow["ҽ������"]=this.m_dvRegister[i1]["ҽ������"];
							newRow["����״̬"]=this.m_dvRegister[i1]["����״̬"];
							newRow["�Һ�״̬"]=this.m_dvRegister[i1]["�Һ�״̬"];
							newRow["�˺��˹���"]=this.m_dvRegister[i1]["�˺��˹���"];
							newRow["�˺�����"]=this.m_dvRegister[i1]["�˺�����"];
							newRow["��¼����"]=this.m_dvRegister[i1]["��¼����"];
							newRow["�������"]=this.m_dvRegister[i1]["�������"];
							newRow["�����ַ"]=this.m_dvRegister[i1]["�����ַ"];
							newRow["�Һ��˹���"]=this.m_dvRegister[i1]["�Һ��˹���"];
							newRow["�Һŷ�"]=this.m_dvRegister[i1]["�Һŷ�"];
							newRow["���Ʒ�"]=this.m_dvRegister[i1]["���Ʒ�"];
							newRow["������"]=this.m_dvRegister[i1]["������"];
							newRow["�Һŷ��Ż�ID"]=this.m_dvRegister[i1]["�Һŷ��Ż�ID"];
							newRow["���Ʒ��Ż�ID"]=this.m_dvRegister[i1]["���Ʒ��Ż�ID"];
							newRow["�������Ż�ID"]=this.m_dvRegister[i1]["�������Ż�ID"];
							newRow["�ϼƽ��"]=this.m_dvRegister[i1]["�ϼƽ��"];
							findTB.Rows.Add(newRow);
						}
					}
					#region ����ͳ����
					DataRow newRow1=findTB.NewRow();
					newRow1["�Һ��˹���"]="�ܽ��";
				    double AllMoney=0;
					for(int k4=0;k4<4;k4++)
					{
						if(k4==3)
							k4+=3;
						double tolMoney=0;
						for(int f6=0;f6<findTB.Rows.Count;f6++)
						{
							try
							{
								double kk=Convert.ToDouble(findTB.Rows[f6][21+k4].ToString());
								tolMoney+=kk;
							}
							catch
							{
							}
						}
						newRow1[21+k4]=tolMoney;
						AllMoney+=tolMoney;
					}
					findTB.Rows.Add(newRow1);
					#endregion
					m_dvRegisterfind=findTB.DefaultView;
					this.m_objViewer.m_dtgRegister.SetDataBinding(m_dvRegisterfind,null);
					this.m_objViewer.m_dtgRegister.Tag="m_dvRegisterfind";
				}
			this.m_objViewer.TextBoxValue.Clear();
		}
		#endregion
		private void m_dtRegister_PositionChanged(object send,System.EventArgs e)
		{
//			this.m_DtgFillTXT();
//			m_FillDetail();
//			m_GetCurPay();
			
			//m_getCurPrice();
		}
		#endregion

		#region �����ϸ
		/// <summary>
		/// �����ϸ
		/// </summary>
		public void m_FillDetail()
		{
			DataTable dtRegisterDetail = new DataTable();
			string strRegister = "";
			strRegister = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�Һ�ID"].ToString();
			clsDomain.m_lngQulRegDetail(strRegister,out dtRegisterDetail);
			this.m_objViewer.m_lsvRegDetail.Items.Clear();
			decimal mny = 0;
			for(int i1=0;i1<dtRegisterDetail.Rows.Count;i1++)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = dtRegisterDetail.Rows[i1][0].ToString().Trim();
				lvi.SubItems.Add(dtRegisterDetail.Rows[i1][1].ToString().Trim());
				lvi.SubItems.Add(dtRegisterDetail.Rows[i1][2].ToString().Trim());
				lvi.SubItems.Add(Convert.ToString(dtRegisterDetail.Rows[i1][3].ToString().Trim()));
				lvi.SubItems.Add(Convert.ToString(dtRegisterDetail.Rows[i1][4].ToString().Trim()));
				lvi.SubItems.Add(Convert.ToString(dtRegisterDetail.Rows[i1][5].ToString().Trim()));
				this.m_objViewer.m_lsvRegDetail.Items.Add(lvi);
				try
				{
					mny += decimal.Parse(this.m_clsRegisterPayVO[i1].m_dblPAYMENT_MNY.ToString())*decimal.Parse(this.m_clsRegisterPayVO[i1].m_fltDISCOUNT_DEC.ToString());
				}
				catch{}
				m_objViewer.m_txtRegFee.Text = mny.ToString();
//				if(this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[1].Text.Trim() == dtRegisterDetail.Rows[i1][1].ToString().Trim())
//				{
//					this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[3].Text = dtRegisterDetail.Rows[i1][3].ToString().Trim();
//					this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[4].Text = dtRegisterDetail.Rows[i1][4].ToString().Trim();
//					break;
//				}
//				else
//				{
//					this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[3].Text = "0";
//					this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[4].Text = "0";
//
//				}
			}
			
			m_FigureUpPay();
		}
		#endregion

		#region �����ֶβ�ѯ
		public void m_FindRegCol(object sender)
		{
			DataTable dt = new DataTable();
			switch(((TextBox)sender).Name)
			{
				case "m_txtCard":
					clsDomain.m_lngQulRegByCol(out dt,"CARDID",this.m_objViewer.m_txtCard.Text,"1");
					break;
				case "m_txtName":
					clsDomain.m_lngQulRegByCol(out dt,"NAME",this.m_objViewer.m_txtName.Text,"1");
					break;
				case "m_txtRegisterNo":
					clsDomain.m_lngQulRegByCol(out dt,"REGNO",this.m_objViewer.m_txtRegisterNo.Text,"1");
					break;
			}
		}
		#endregion

		#region �ڽ���в���
		public void m_FindRecord(object sender)
		{
			for(int i=0;i<this.m_dvRegister.Count;i++)
			{
				switch(((TextBox)sender).Name)
				{
					case "m_txtCard":
						if(this.m_dvRegister[i]["���ƿ���"].ToString().Trim() == this.m_objViewer.m_txtCard.Text.Trim())
						{
							this.m_objViewer.m_dtgRegister.UnSelect(this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Position);
							this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Position = i;
							this.m_objViewer.m_dtgRegister.Select(i);
							return;
						}
						break;
					case "m_txtName":
						if(this.m_dvRegister[i]["��������"].ToString().Trim() == this.m_objViewer.m_txtName.Text.Trim())
						{
							this.m_objViewer.m_dtgRegister.UnSelect(this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Position);
							this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Position = i;
							this.m_objViewer.m_dtgRegister.Select(i);
							return;
						}
						break;
					case "m_txtRegisterNo":
						if(this.m_dvRegister[i]["��ˮ��"].ToString().Trim() == this.m_objViewer.m_txtRegisterNo.Text.Trim())
						{
							this.m_objViewer.m_dtgRegister.UnSelect(this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Position);
							this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Position = i;
							this.m_objViewer.m_dtgRegister.Select(i);
							return;
						}
						break;
				}
			}
		}
		#endregion

		#region �޸ĹҺ�
		public long m_lngModifyRegister()
		{
			clsPatientRegister_VO objreg = null;
			clsPatientDetail_VO clsPatientDetail = new clsPatientDetail_VO();
			m_FillVO(out objreg);
			
			long lngarg = clsDomain.m_lngModifyRegister(objreg);
			if(lngarg >0)
			{
				m_ModifyDatagrid();
				int DetailCount = m_objViewer.m_lsvRegDetail.Items.Count;
				for(int i=0;i<DetailCount;i++)
				{
//					if(decimal.Parse(this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[3].Text.Trim()) == 0
//						|| decimal.Parse(this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[3].Text.Trim()) == 0) continue;
					
					clsPatientDetail.m_strREGISTERID_CHR = objreg.m_strRegisterID;
					clsPatientDetail.m_strCHARGEID_CHR = this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[1].Text;
					clsPatientDetail.m_dblPAYMENT_MNY = double.Parse(this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[3].Text);
					clsPatientDetail.m_fltDISCOUNT_DEC = float.Parse(this.m_objViewer.m_lsvRegDetail.Items[i].SubItems[4].Text);
					lngarg = clsDomain.m_lngModifyRegisterDetail(clsPatientDetail);
				}
			}
			else
			{
				m_DtgFillTXT();
			}
			return lngarg;
		}
		#endregion

		#region ��ԭ�˺�
		/// <summary>
		/// ��ԭ�˺�
		/// </summary>
		public void m_ResetReg()
		{
			if(intSetStatus==-2)
			{
				clsDomain.m_lngGetSetStatus(out intSetStatus);
			}
			string strRegisterID="";
			DateTime regdate = DateTime.Now.Date;
			try
			{
				if((string)this.m_objViewer.m_dtgRegister.Tag=="m_dvRegister")
					strRegisterID = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�Һ�ID"].ToString();
				else
					strRegisterID = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind,null].Current)["�Һ�ID"].ToString();
			}
			catch
			{
				return;
			}
			if((string)this.m_objViewer.m_dtgRegister.Tag=="m_dvRegister")
			{
//				if(intSetStatus==0)
//				{
//					if(m_dvRegister[this.m_objViewer.m_dtgRegister.CurrentRowIndex]["�Һ��˹���"].ToString().Trim()==this.m_objViewer.LoginInfo.m_strEmpNo||m_dvRegister[this.m_objViewer.m_dtgRegister.CurrentRowIndex]["�˺��˹���"].ToString().Trim()==this.m_objViewer.LoginInfo.m_strEmpNo)
//					{
//						MessageBox.Show("��ֹ��ԭ���˵��˺ţ�","�Ƿ�����");
//						return;
//					}
//				}
				int k1=0;//�˺Ŵ���
				int k2=0;//��ԭ����
				string strNO=m_dvRegister[this.m_objViewer.m_dtgRegister.CurrentRowIndex]["��ˮ��"].ToString();//���浱ǰѡ�йҺ����ݵ���ˮ��
				for(int i1=0;i1<m_dvRegister.Count;i1++)
				{
					if(m_dvRegister[i1]["��ˮ��"].ToString().Trim()==strNO.Trim()&&m_dvRegister[i1]["�Һ�״̬"].ToString().Trim()=="�˺�")
					{
						k1++;
					}
					if(m_dvRegister[i1]["��ˮ��"].ToString().Trim()==strNO.Trim()&&m_dvRegister[i1]["�Һ�״̬"].ToString().Trim()=="��ԭ")
					{
						k2++;
					}

				}
				if(k1==0||k2>=k1)
				{
					MessageBox.Show("�ùҺ��Ѿ�����ԭ�������ٻ�ԭ��","��ʾ");
					return;
				}
			}
			else
			{
//				if(m_dvRegister[this.m_objViewer.m_dtgRegister.CurrentRowIndex]["�Һ��˹���"].ToString().Trim()==this.m_objViewer.LoginInfo.m_strEmpNo||m_dvRegister[this.m_objViewer.m_dtgRegister.CurrentRowIndex]["�˺��˹���"].ToString().Trim()==this.m_objViewer.LoginInfo.m_strEmpNo)
//				{
//					MessageBox.Show("��ֹ��ԭ���˵��˺ţ�","�Ƿ�����");
//					return;
//				}
				int k1=0;//�˺Ŵ���
				int k2=0;//��ԭ����
				string strNO=m_dvRegisterfind[this.m_objViewer.m_dtgRegister.CurrentRowIndex]["��ˮ��"].ToString();//���浱ǰѡ�йҺ����ݵ���ˮ��
				for(int i1=0;i1<m_dvRegisterfind.Count;i1++)
				{
					if(m_dvRegisterfind[i1]["��ˮ��"].ToString().Trim()==strNO.Trim()&&m_dvRegisterfind[i1]["�Һ�״̬"].ToString().Trim()=="�˺�")
					{
						k1++;
					}
					if(m_dvRegisterfind[i1]["��ˮ��"].ToString().Trim()==strNO.Trim()&&m_dvRegisterfind[i1]["�Һ�״̬"].ToString().Trim()=="��ԭ")
					{
						k2++;
					}

				}
				if(k1==0||k2>=k1)
				{
					MessageBox.Show("�ùҺ��Ѿ�����ԭ�������ٻ�ԭ��","��ʾ");
					return;
				}
			}
			string strReSetRegDate = regdate.ToShortDateString();
			clsController_Security clsSe=new clsController_Security();
			string strResetRegEmpno =  this.m_objViewer.LoginInfo.m_strEmpID;
			string newID="";
			int waitNO=0;
			if(clsDomain.m_lngResetReg(strRegisterID,strResetRegEmpno,strReSetRegDate,out newID,out waitNO)==0) return;

			if((string)this.m_objViewer.m_dtgRegister.Tag=="m_dvRegister")
			{
				for(int f2=0;f2<m_dvRegister.Count;f2++)
				{
					if(m_dvRegister[f2]["�Һ��˹���"].ToString().Trim()=="�ܽ��")
					{
						m_dvRegister.Delete(f2);
					}
				}
				DataRowView addRow=m_dvRegister.AddNew();
				DataRowView seleRow=(DataRowView)m_dvRegister[this.m_objViewer.m_dtgRegister.CurrentRowIndex];
				addRow["�Һ�ID"]=newID;
				addRow["���ƿ���"]=seleRow["���ƿ���"];
				addRow["��Ʊ��"]=seleRow["��Ʊ��"];
				addRow["��ˮ��"]=seleRow["��ˮ��"];
				addRow["�����"]=waitNO.ToString();
				addRow["�Һ�����"]=seleRow["�Һ�����"];
				addRow["��������"]=seleRow["��������"];
				addRow["�����Ա�"]=seleRow["�����Ա�"];
				addRow["֧������"]=seleRow["֧������"];
				addRow["�Һ�����"]=seleRow["�Һ�����"];
				addRow["��������"]=seleRow["��������"];
				addRow["ҽ������"]=seleRow["ҽ������"];
				addRow["����״̬"]=seleRow["����״̬"];
				addRow["�Һ�״̬"]="��ԭ";
				addRow["�˺��˹���"]="";
				addRow["��¼����"]=seleRow["��¼����"];
				addRow["�������"]=seleRow["�������"];
				addRow["�����ַ"]=seleRow["�����ַ"];
				addRow["�Һ��˹���"]=this.m_objViewer.LoginInfo.m_strEmpNo;
				int i1;
				try
				{
					for(i1=20;i1<m_dvRegister.Table.Columns.Count;i1++)
					{
						addRow[i1]=Math.Abs(Convert.ToInt32(seleRow[i1].ToString()));
					}
					DataRowView addRowTol=m_dvRegister.AddNew();
					for(int j3=19;j3<i1;j3++)
					{
						double tolMoney=0;
						for(int f2=0;f2<m_dvRegister.Table.Rows.Count;f2++)
						{
							tolMoney+=Convert.ToDouble(m_dvRegister.Table.Rows[f2][j3].ToString());
						}
						addRowTol[j3]=tolMoney;
					}
					addRowTol["�Һ��˹���"]="�ܽ��";
				}
				catch
				{
				}
			}
			else
			{
				for(int f2=0;f2<m_dvRegisterfind.Count;f2++)
				{
					if(m_dvRegisterfind[f2]["�Һ��˹���"].ToString().Trim()=="�ܽ��")
					{
						m_dvRegisterfind.Delete(f2);
					}
				}
				DataRowView addRow=m_dvRegisterfind.AddNew();
				DataRowView seleRow=(DataRowView)m_dvRegisterfind[this.m_objViewer.m_dtgRegister.CurrentRowIndex];
				addRow["�Һ�ID"]=newID;
				addRow["���ƿ���"]=seleRow["���ƿ���"];
				addRow["��Ʊ��"]=seleRow["��Ʊ��"];
				addRow["��ˮ��"]=seleRow["��ˮ��"];
				addRow["�����"]=waitNO.ToString();
				addRow["�Һ�����"]=seleRow["�Һ�����"];
				addRow["��������"]=seleRow["��������"];
				addRow["�����Ա�"]=seleRow["�����Ա�"];
				addRow["֧������"]=seleRow["֧������"];
				addRow["�Һ�����"]=seleRow["�Һ�����"];
				addRow["��������"]=seleRow["��������"];
				addRow["ҽ������"]=seleRow["ҽ������"];
				addRow["����״̬"]=seleRow["����״̬"];
				addRow["�Һ�״̬"]="��ԭ";
				addRow["�˺��˹���"]="";
				addRow["��¼����"]=seleRow["��¼����"];
				addRow["�������"]=seleRow["�������"];
				addRow["�����ַ"]=seleRow["�����ַ"];
				addRow["�Һ��˹���"]=seleRow["�Һ��˹���"];
				int i1;
				for(i1=20;i1<m_dvRegisterfind.Table.Columns.Count;i1++)
				{
					addRow[i1]=Math.Abs(Convert.ToInt32(seleRow[i1].ToString()));
				}
				DataRowView addRowTol=m_dvRegisterfind.AddNew();
				for(int j3=20;j3<i1;j3++)
				{
					double tolMoney=0;
					for(int f2=0;f2<m_dvRegisterfind.Table.Rows.Count;f2++)
					{
						tolMoney+=Convert.ToDouble(m_dvRegisterfind.Table.Rows[f2][j3].ToString());
					}
					addRowTol[j3]=tolMoney;
				}
				addRowTol["�Һ��˹���"]="�ܽ��";

			}
		}
		#endregion

		#region �˺�
		/// <summary>
		/// �����˺ŵ�״̬
		/// </summary>
		int intSetStatus=-2;
		/// <summary>
		/// �˺�
		/// </summary>
		public void m_CancelReg()
		{
			if(intSetStatus==-2)
			{
				clsDomain.m_lngGetSetStatus(out intSetStatus);
			}
			string strRegisterID="";
			try
			{
				if((string)this.m_objViewer.m_dtgRegister.Tag=="m_dvRegister")
				    strRegisterID = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Current)["�Һ�ID"].ToString();
				else
					strRegisterID = ((DataRowView)this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegisterfind,null].Current)["�Һ�ID"].ToString();
			}
			catch
			{
				return;
			}
			if((string)this.m_objViewer.m_dtgRegister.Tag=="m_dvRegister")
			{
				if(intSetStatus==0)
				{
					if(m_dvRegister[this.m_objViewer.m_dtgRegister.CurrentRowIndex]["�Һ��˹���"].ToString().Trim()!=this.m_objViewer.LoginInfo.m_strEmpNo)
					{
						MessageBox.Show("��ֹ�������˵ĹҺţ�","�Ƿ�����");
						return;
					}
				}
                int k1=0;//�˺Ŵ���
				int k2=0;//��ԭ����
				string strNO=m_dvRegister[this.m_objViewer.m_dtgRegister.CurrentRowIndex]["��ˮ��"].ToString();//���浱ǰѡ�йҺ����ݵ���ˮ��
				for(int i1=0;i1<m_dvRegister.Count;i1++)
				{
					if(m_dvRegister[i1]["��ˮ��"].ToString().Trim()==strNO.Trim()&&m_dvRegister[i1]["�Һ�״̬"].ToString().Trim()=="�˺�")
					{
						k1++;
					}
					if(m_dvRegister[i1]["��ˮ��"].ToString().Trim()==strNO.Trim()&&m_dvRegister[i1]["�Һ�״̬"].ToString().Trim()=="��ԭ")
					{
						k2++;
					}

				}
				if(k1>0&&k1>k2)
				{
					MessageBox.Show("�ùҺ��Ѿ��˺ţ������˺ţ�","��ʾ");
					return;
				}
			}
			else
			{
				if(m_dvRegisterfind[this.m_objViewer.m_dtgRegister.CurrentRowIndex]["�Һ��˹���"].ToString().Trim()!=this.m_objViewer.LoginInfo.m_strEmpNo)
				{
					MessageBox.Show("�������˱��˵ĺţ�","�Ƿ�����");
					return;
				}
				int k1=0;//�˺Ŵ���
				int k2=0;//��ԭ����
				string strNO=m_dvRegisterfind[this.m_objViewer.m_dtgRegister.CurrentRowIndex]["��ˮ��"].ToString();//���浱ǰѡ�йҺ����ݵ���ˮ��
				for(int i1=0;i1<m_dvRegisterfind.Count;i1++)
				{
					if(m_dvRegisterfind[i1]["��ˮ��"].ToString().Trim()==strNO.Trim()&&m_dvRegisterfind[i1]["�Һ�״̬"].ToString().Trim()=="�˺�")
					{
						k1++;
					}
					if(m_dvRegisterfind[i1]["��ˮ��"].ToString().Trim()==strNO.Trim()&&m_dvRegisterfind[i1]["�Һ�״̬"].ToString().Trim()=="��ԭ")
					{
						k2++;
					}

				}
				if(k1>0&&k1>k2)
				{
					MessageBox.Show("�ùҺ��Ѿ��˺ţ������˺ţ�","��ʾ");
					return;
				}
			}
			bool isReMoney;
			string outstr="";
			if(clsDomain.m_lngCheckRegister(strRegisterID,out isReMoney,out outstr)==0)
			{
				if(outstr=="0")
				{
					if(MessageBox.Show("ϵͳ��ⲻ���ùҺ��Ƿ��չ��ѣ��Ƿ�Ҫ�����˺ţ�","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
						return;
				}
				else
				{
					if(MessageBox.Show("ϵͳ��ⲻ���ùҺ��Ƿ��չ��ѣ��Ƿ�Ҫ�����˺ţ�","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
						return;
				}
			}
			if(isReMoney)
			 {
				if(outstr=="0")
				{
					MessageBox.Show("�ùҺ��Ѿ������������������˺ţ�","Icare",MessageBoxButtons.OK,MessageBoxIcon.Information);
						return;
				}
				else
				{
					MessageBox.Show("�ùҺ��Ѿ������ѣ��������˺ţ�","Icare",MessageBoxButtons.OK,MessageBoxIcon.Information);
						return;
				}
			 }

            string ConfirmID = "";
            frmOPSConfirm fc = new frmOPSConfirm();
            if (fc.ShowDialog() == DialogResult.OK)
            {
                ConfirmID = fc.Empid;
            }
            else
            {
                return;
            }

			string strReturnDate = DateTime.Now.ToShortDateString();
			string strReturnEmpno = this.m_objViewer.LoginInfo.m_strEmpID;
			string strReturnno= this.m_objViewer.LoginInfo.m_strEmpNo;
			string newID="";
            if (clsDomain.m_lngCancelReg(strRegisterID, strReturnEmpno, strReturnDate, ConfirmID, out newID) == 0)
            {
                return;
            }

			if((string)this.m_objViewer.m_dtgRegister.Tag=="m_dvRegister")
			{
				for(int f2=0;f2<m_dvRegister.Count;f2++)
				{
					if(m_dvRegister[f2]["�Һ��˹���"].ToString().Trim()=="�ܽ��")
					{
						m_dvRegister.Delete(f2);
					}
				}
				DataRowView addRow=m_dvRegister.AddNew();
                try
                {
					DataRowView seleRow=(DataRowView)m_dvRegister[this.m_objViewer.m_dtgRegister.CurrentRowIndex];
					addRow["�Һ�ID"]=newID;
					addRow["���ƿ���"]=seleRow["���ƿ���"];
					addRow["��Ʊ��"]=seleRow["��Ʊ��"];
					addRow["��ˮ��"]=seleRow["��ˮ��"];
					addRow["�Һ�����"]=seleRow["�Һ�����"];
					addRow["��������"]=seleRow["��������"];
					addRow["�����Ա�"]=seleRow["�����Ա�"];
					addRow["֧������"]=seleRow["֧������"];
					addRow["�Һ�����"]=seleRow["�Һ�����"];
					addRow["��������"]=seleRow["��������"];
					addRow["ҽ������"]=seleRow["ҽ������"];
					addRow["����״̬"]=seleRow["����״̬"];
					addRow["�Һ�״̬"]="�˺�";
					addRow["�˺��˹���"]=strReturnno;
					addRow["�˺�����"]=strReturnDate;
					addRow["��¼����"]=seleRow["��¼����"];
					addRow["�������"]=seleRow["�������"];
					addRow["�����ַ"]=seleRow["�����ַ"];
					addRow["�Һ��˹���"]=seleRow["�Һ��˹���"];
					int i1;
					for(i1=21;i1<m_dvRegister.Table.Columns.Count;i1++)
					{
						addRow[i1]="-"+Math.Abs(Convert.ToDouble(seleRow[i1].ToString()));
					}
					DataRowView addRowTol=m_dvRegister.AddNew();
					try
					{
						for(int j3=20;j3<i1;j3++)
						{
							double tolMoney=0;
							for(int f2=0;f2<m_dvRegister.Table.Rows.Count;f2++)
							{
								tolMoney+=Convert.ToDouble(m_dvRegister.Table.Rows[f2][j3].ToString());
							}
							addRowTol[j3]=tolMoney;
						}
					}
					catch
					{
					}
					addRowTol["�Һ��˹���"]="�ܽ��";
                }
                catch
                {
                    MessageBox.Show("�˺�ϵͳ����", "ϵͳ��ʾ");
                    return;
                }

                //���õ�ǰ��
                for (int i = 0; i < m_dvRegister.Count; i++)
                {
                    if (m_dvRegister[i]["�Һ�ID"].ToString() == newID)
                    {
                        this.m_objViewer.m_dtgRegister.CurrentRowIndex = i;
                        break;
                    }
                }
			}
			else
			{
				for(int f2=0;f2<m_dvRegisterfind.Count;f2++)
				{
					if(m_dvRegisterfind[f2]["�Һ��˹���"].ToString().Trim()=="�ܽ��")
					{
						m_dvRegisterfind.Delete(f2);
					}
				}
				DataRowView addRow=m_dvRegisterfind.AddNew();                
				try
				{
					DataRowView seleRow=(DataRowView)m_dvRegisterfind[this.m_objViewer.m_dtgRegister.CurrentRowIndex];
					addRow["�Һ�ID"]=newID;
					addRow["���ƿ���"]=seleRow["���ƿ���"];
					addRow["��Ʊ��"]=seleRow["��Ʊ��"];
					addRow["��ˮ��"]=seleRow["��ˮ��"];
					addRow["�Һ�����"]=seleRow["�Һ�����"];
					addRow["��������"]=seleRow["��������"];
					addRow["�����Ա�"]=seleRow["�����Ա�"];
					addRow["֧������"]=seleRow["֧������"];
					addRow["�Һ�����"]=seleRow["�Һ�����"];
					addRow["��������"]=seleRow["��������"];
					addRow["ҽ������"]=seleRow["ҽ������"];
					addRow["����״̬"]=seleRow["����״̬"];
					addRow["�Һ�״̬"]="�˺�";
					addRow["�˺��˹���"]=strReturnno;
					addRow["�˺�����"]=strReturnDate;
					addRow["��¼����"]=seleRow["��¼����"];
					addRow["�������"]=seleRow["�������"];
					addRow["�����ַ"]=seleRow["�����ַ"];
					addRow["�Һ��˹���"]=seleRow["�Һ��˹���"];
					int i1;
					for(i1=20;i1<m_dvRegisterfind.Table.Columns.Count;i1++)
					{
						addRow[i1]="-"+seleRow[i1].ToString();
					}
					DataRowView addRowTol=m_dvRegisterfind.AddNew();
					for(int j3=20;j3<i1;j3++)
					{
						double tolMoney=0;
						for(int f2=0;f2<m_dvRegisterfind.Table.Rows.Count;f2++)
						{
							tolMoney+=Convert.ToDouble(m_dvRegisterfind.Table.Rows[f2][j3].ToString());
						}
						addRowTol[j3]=tolMoney;
					}
					addRowTol["�Һ��˹���"]="�ܽ��";

                    //���õ�ǰ��
                    for (int i = 0; i < m_dvRegisterfind.Count; i++)
                    {
                        if (m_dvRegisterfind[i]["�Һ�ID"].ToString() == newID)
                        {
                            this.m_objViewer.m_dtgRegister.CurrentRowIndex = i;
                            break;
                        }
                    }
				}
				catch
				{
					MessageBox.Show("�˺ų���","ϵͳ��ʾ");
				}                
			}

            /***����***/
            this.m_PrintRegister();
            /***/
		}
		#endregion

		public void m_Filter()
		{
			switch(this.m_objViewer.m_cmbQulType.SelectedIndex)
			{
				case 0 :
					this.m_dvRegister.RowFilter = "";
					break;
				case 1 :
					this.m_dvRegister.RowFilter = "�Һ�״̬='�˺�'";
					break;
				case 2 :
					this.m_dvRegister.RowFilter = "����״̬='����' and �Һ�״̬<>'�˺�'";
					break;
				case 3 :				
					this.m_dvRegister.RowFilter = "�Һ�״̬='ԤԼ'";
					break;
				case 4 :
					this.m_dvRegister.RowFilter = "����״̬<>'�ѽ���'";
					break;
				case 5 :
					this.m_dvRegister.RowFilter = "����״̬='�ѽ���'";
					break;
				case 6 :
					this.m_dvRegister.RowFilter = "����״̬='ȡ��'";
					break;
			}
			if(this.m_objViewer.m_dtgRegister.BindingContext[this.m_dvRegister,null].Count != 0)
			{
				m_dtRegister_PositionChanged(null,null);
			}
			else
			{
				this.m_Clear();
			}
		}

		
	}
	
	
}
