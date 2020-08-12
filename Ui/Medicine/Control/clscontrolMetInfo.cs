using System;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clscontrolMetInfo ��ժҪ˵����
	/// </summary>
	public class clscontrolMetInfo:com.digitalwave.GUI_Base.clsController_Base
	{
		public clscontrolMetInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmMetInfo m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMetInfo)frmMDI_Child_Base_in;
		}
		#endregion

		#region �򿪴���
		/// <summary>
		/// ��ʾҩƷ��Ϣ
		/// </summary>
		/// <param name="objArr">ҩƷ��ϢVO</param>
		/// <param name="p_intFlag">ָʾҩ����־ 0-ҩ�� 1-����ҩ��</param>
		public void OpenWindow(clsMedicines_VO objArr,int p_intFlag)
		{
			this.m_objViewer.m_txtNo.Text=objArr.m_strASSISTCODE_CHR;
			this.m_objViewer.m_txtName.Text=objArr.m_strMEDICINENAME_VCHR;
			this.m_objViewer.m_txtEnName.Text=objArr.m_strMEDICINEENGNAME_VCHR;
			this.m_objViewer.m_txtSpec.Text=objArr.m_strMEDSPEC_VCHR;
			this.m_objViewer.m_txtMedType.Text=objArr.m_strMEDICINETYPENAME_CHR;
			this.m_objViewer.m_txtPreType.Text=objArr.m_strMEDICINEPREPTYPENAME_CHR;
			this.m_objViewer.m_txtDosage.Text=objArr.m_dblDOSAGE_DEC.ToString();
			this.m_objViewer.DosageUnit.Text=objArr.m_strDOSAGEUNITNAME_CHR;
			this.m_objViewer.Unit.Text=objArr.m_strOPUNITNAME_CHR;
			this.m_objViewer.IpUnit.Text=objArr.m_strIPUNITNAME_CHR;
			this.m_objViewer.m_txtPackQty.Text=objArr.m_dblPACKQTY_DEC.ToString();
			this.m_objViewer.Product.Text=objArr.m_strPRODUCTORNAME_CHR;
			if(objArr.m_strISANAESTHESIA_CHR=="��")
			{
				this.m_objViewer.m_chkIsAnaesthesia.Checked=true;
			}
			if(objArr.m_strISCHLORPROMAZINE_CHR=="��")
			{
				this.m_objViewer.m_chkIsChlorpromazine.Checked=true;
			}
			if(objArr.m_strISCOSTLY_CHR=="��")
			{
				this.m_objViewer.m_chkIsCostly.Checked=true;
			}
			if(objArr.m_strISSELF_CHR=="��")
			{
				this.m_objViewer.m_chkIsSelf.Checked=true;
			}
			if(objArr.m_strISIMPORT_CHR=="��")
			{
				this.m_objViewer.m_chkIsImport.Checked=true;
			}
			if(objArr.m_strISSELFPAY_CHR=="��")
			{
				this.m_objViewer.m_chkIsSelfPay.Checked=true;
			}
			if(p_intFlag == 0)
			{
				if(objArr.m_intNOQTYFLAG_INT==1)
				{
					this.m_objViewer.checkBox1.Checked=true;
				}
			}
			else
			{
				if(objArr.m_intIPNOQTYFLAG_INT==1)
				{
					this.m_objViewer.checkBox1.Checked=true;
				}
			}
			this.m_objViewer.ShowDialog();

		}
		#endregion
	}
}
