using System;
using System.Drawing;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	public class clsPrintRecipe
	{
		public clsPrintRecipe(System.Drawing.Printing.PrintPageEventArgs p_obj,clsOutpatientPrintRecipe_VO VO)
		{
			objDraw=p_obj;
			objFontTitle=new Font("SimSun",16,FontStyle.Bold);
			objFontNormal=new Font("SimSun",10);
			obj_VO=VO;
		}
		/// <summary>
		/// ��ͼ����
		/// </summary>
		private System.Drawing.Printing.PrintPageEventArgs objDraw;
		private clsOutpatientPrintRecipe_VO obj_VO;
		/// <summary>
		/// ��������
		/// </summary>
		Font objFontTitle;
		/// <summary>
		/// ��������
		/// </summary>
		Font objFontNormal;
		/// <summary>
		/// ��߾�
		/// </summary>
		float fltLeftIndentProp=0.07f;
		/// <summary>
		/// �ұ߾�
		/// </summary>
		float fltRightIndentProp=0.07f;
		/// <summary>
		/// �м��
		/// </summary>
		private  float  fltRowHeight;
		/// <summary>
		/// ������
		/// </summary>
		private float  Y;
		
		#region ��ӡ����
		private void m_mthPrintTitle()
		{
			//����
			SizeF objFontSize =objDraw.Graphics.MeasureString(obj_VO.m_strHospitalName+"������Ϣ",this.objFontTitle);
			float X=(this.objDraw.PageBounds.Width-objFontSize.Width)/2;
			Y=this.objDraw.PageBounds.Height*0.047f-(objFontSize.Height/2);
			objDraw.Graphics.DrawString(obj_VO.m_strHospitalName+"������Ϣ",objFontTitle,Brushes.Black,X,Y);
			//ҳ��
			Y+=objFontSize.Height/2-5;
			objFontSize =objDraw.Graphics.MeasureString("��1ҳ/��1ҳ",this.objFontNormal);
			fltRowHeight=objFontSize.Height;
			X=this.objDraw.PageBounds.Width*(1-fltRightIndentProp)-objFontSize.Width;
			objDraw.Graphics.DrawString("��1ҳ/��1ҳ",objFontNormal,Brushes.Black,X,Y);
			//����
			Y+=14;
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			float RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			objDraw.Graphics.DrawLine(new Pen(Color.Black,2),X,Y,RX,Y);
			//���￨��
			Y+=this.fltRowHeight/2;
			objFontSize =objDraw.Graphics.MeasureString("���￨��:",this.objFontNormal);
			objDraw.Graphics.DrawString("���￨��:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.m_strCardID,objFontNormal,Brushes.Black,X,Y);
			//��ˮ��
			X=this.objDraw.PageBounds.Width*0.34f;
			objFontSize =objDraw.Graphics.MeasureString("��ˮ��:",this.objFontNormal);
			objDraw.Graphics.DrawString("��ˮ��:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width-2;
			objDraw.Graphics.DrawString(obj_VO.m_strRegisterID,objFontNormal,Brushes.Black,X,Y);
			//����ID
			X=this.objDraw.PageBounds.Width*0.60f;
			objFontSize =objDraw.Graphics.MeasureString("�������:",this.objFontNormal);
			objDraw.Graphics.DrawString("�������:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width;
			objDraw.Graphics.DrawString(obj_VO.m_strRecipeID,objFontNormal,Brushes.Black,X,Y);
			//����
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			Y+=this.fltRowHeight;
			objFontSize =objDraw.Graphics.MeasureString("��    ��:",this.objFontNormal);
			objDraw.Graphics.DrawString("��    ��:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.m_strPatientName,objFontNormal,Brushes.Black,X,Y);
			//�Ա�
			X=this.objDraw.PageBounds.Width*0.34f;
			objFontSize =objDraw.Graphics.MeasureString("��  ��:",this.objFontNormal);
			objDraw.Graphics.DrawString("��  ��:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width-2;
			objDraw.Graphics.DrawString(obj_VO.m_strSex,objFontNormal,Brushes.Black,X,Y);
			//����ID
			X=this.objDraw.PageBounds.Width*0.60f;
			objFontSize =objDraw.Graphics.MeasureString("��    ��:",this.objFontNormal);
			objDraw.Graphics.DrawString("��    ��:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.m_strAge,objFontNormal,Brushes.Black,X,Y);
			//�������
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			Y+=this.fltRowHeight;
			objFontSize =objDraw.Graphics.MeasureString("�������:",this.objFontNormal);
			objDraw.Graphics.DrawString("�������:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.m_strDiagDeptID,objFontNormal,Brushes.Black,X,Y);
			//����ҽ��
			X=this.objDraw.PageBounds.Width*0.34f;
			objFontSize =objDraw.Graphics.MeasureString("��ҽ��:",this.objFontNormal);
			objDraw.Graphics.DrawString("��ҽ��:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.m_strDiagDrName,objFontNormal,Brushes.Black,X,Y);
			//����ʱ��
			X=this.objDraw.PageBounds.Width*0.60f;
			objFontSize =objDraw.Graphics.MeasureString("����ʱ��:",this.objFontNormal);
			objDraw.Graphics.DrawString("����ʱ��:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.m_strPrintDate,objFontNormal,Brushes.Black,X,Y);
			//����
			Y+=this.fltRowHeight;
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objDraw.Graphics.DrawLine(new Pen(Color.Black,2),X,Y,RX,Y);
			Y+=this.fltRowHeight/2;
			//��Ŀ��������
			//����
			objDraw.Graphics.DrawString("��Ŀ����",objFontNormal,Brushes.Black,X,Y);
			//��λ
			X=this.objDraw.PageBounds.Width*0.34f;
			objDraw.Graphics.DrawString("��λ",objFontNormal,Brushes.Black,X,Y);
			//������
			X=this.objDraw.PageBounds.Width*0.415f;
			objDraw.Graphics.DrawString("����",objFontNormal,Brushes.Black,X,Y);
			//����
			X=this.objDraw.PageBounds.Width*0.49f;
			objDraw.Graphics.DrawString("����",objFontNormal,Brushes.Black,X,Y);
			//�ܼ�
			X=this.objDraw.PageBounds.Width*0.565f;
			objDraw.Graphics.DrawString("�ܼ�",objFontNormal,Brushes.Black,X,Y);
			//�÷�
			X=this.objDraw.PageBounds.Width*0.76f;
			objDraw.Graphics.DrawString("�÷�         ҩ��",objFontNormal,Brushes.Black,X,Y);
			//����
			Y+=this.fltRowHeight;
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objDraw.Graphics.DrawLine(new Pen(Color.Black,1),X,Y,RX,Y);
		}
		#endregion
		#region ��ӡ����
		private void m_mthPrintText()
		{
			Y+=this.fltRowHeight/2;
			float X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
//			clsOutpatientPrintRecipeDetail_VO[] objArr=this.obj_VO.objPRDArr;
			clsOutpatientPrintRecipeDetail_VO[] objArr=null;
			if(objArr==null)
			{
				return;
			}
			for(int i=0;i<objArr.Length;i++)
			{
				if(objArr[i]==null)
				{
					continue;
				}
				X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
				objDraw.Graphics.DrawString(objArr[i].m_strChargeName,objFontNormal,Brushes.Black,X,Y);
				X=this.objDraw.PageBounds.Width*0.35f;
				objDraw.Graphics.DrawString(objArr[i].m_strUnit,objFontNormal,Brushes.Black,X,Y);
				X=this.objDraw.PageBounds.Width*0.425f;
				objDraw.Graphics.DrawString(objArr[i].m_strCount,objFontNormal,Brushes.Black,X,Y);
				X=this.objDraw.PageBounds.Width*0.48f;
				objDraw.Graphics.DrawString(objArr[i].m_strPrice,objFontNormal,Brushes.Black,X,Y);
				X=this.objDraw.PageBounds.Width*0.565f;
				objDraw.Graphics.DrawString(objArr[i].m_strSumPrice,objFontNormal,Brushes.Black,X,Y);
				X=this.objDraw.PageBounds.Width*0.66f;
				objDraw.Graphics.DrawString(objArr[i].m_strUsage,objFontNormal,Brushes.Black,X,Y);
				Y+=this.fltRowHeight;
			}

		
		}
		#endregion
		#region ��ӡҳ��
		private void m_mthPrintEnd()
		{
			//����
			Y=this.objDraw.PageBounds.Height*0.82f;
			float X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			float RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			objDraw.Graphics.DrawLine(new Pen(Color.Black,2),X,Y,RX,Y);
			//����
			Y+=this.fltRowHeight/2;
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			SizeF objFontSize =objDraw.Graphics.MeasureString("�Ը����:",this.objFontNormal);
			objDraw.Graphics.DrawString("�Ը����:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.m_strSelfPay,objFontNormal,Brushes.Black,X,Y);
			X=this.objDraw.PageBounds.Width*0.34f;
			objFontSize =objDraw.Graphics.MeasureString("���˽��:",this.objFontNormal);
			objDraw.Graphics.DrawString("���˽��:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.m_strChargeUp,objFontNormal,Brushes.Black,X,Y);
			X=this.objDraw.PageBounds.Width*0.65f;
			objFontSize =objDraw.Graphics.MeasureString("�ܶ�:",this.objFontNormal);
			objDraw.Graphics.DrawString("�ܶ�:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.m_strRecipePrice,objFontNormal,Brushes.Black,X,Y);
			//����
			Y+=this.fltRowHeight;
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objDraw.Graphics.DrawLine(new Pen(Color.Black,1),X,Y,RX,Y);
			//����
			Y+=this.fltRowHeight/2;
			X=this.objDraw.PageBounds.Width*0.65f;
			objDraw.Graphics.DrawString(obj_VO.m_strHospitalName+"(����)",objFontNormal,Brushes.Black,X,Y);
			//����
			Y+=this.fltRowHeight;
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objDraw.Graphics.DrawLine(new Pen(Color.Black,1),X,Y,RX,Y);
			Y+=this.fltRowHeight/2;
			objFontSize =objDraw.Graphics.MeasureString("��ӡ����:",this.objFontNormal);
			objDraw.Graphics.DrawString("��ӡ����:",objFontNormal,Brushes.Black,X,Y);
			objDraw.Graphics.DrawString("��",objFontNormal,Brushes.Black,X+objFontSize.Width+15,Y);
			//����Ա����
			X=this.objDraw.PageBounds.Width*0.34f;
			objFontSize =objDraw.Graphics.MeasureString("��ӡԱ����:",this.objFontNormal);
			objDraw.Graphics.DrawString("��ӡԱ����:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.m_strRecordEmpID,objFontNormal,Brushes.Black,X,Y);
			//��ӡʱ��
			X=this.objDraw.PageBounds.Width*0.65f;
			objFontSize =objDraw.Graphics.MeasureString("��ӡʱ��:",this.objFontNormal);
			objDraw.Graphics.DrawString("��ӡʱ��:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(DateTime.Now.ToString("yyyy.MM.dd HH:mm"),objFontNormal,Brushes.Black,X,Y);
		}
		#endregion
		#region ��ʼ��ӡ
		public void m_mthBegionPrint()
		{
			this.m_mthPrintTitle();
			this.m_mthPrintText();
			this.m_mthPrintEnd();
		}
		#endregion
	}
}
