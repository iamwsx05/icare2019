using System;
using System.Drawing;
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsPrintCaseHistory ��ժҪ˵����
	/// </summary>
	public class clsPrintApplyInHospital
	{

		/// <summary>
		/// ��ͼ����
		/// </summary>
		private System.Drawing.Printing.PrintPageEventArgs objDraw;
		private clsApplyInHospitalPrint_VO obj_VO;
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
		/// <summary>
		/// ���θ�
		/// </summary>
		private float H;
		/// <summary>
		/// ���ֿ�
		/// </summary>
		private float SingleFontWidth;
		public clsPrintApplyInHospital(clsApplyInHospitalPrint_VO VO)
		{
			objFontTitle=new Font("SimSun",16,FontStyle.Bold);
			objFontNormal=new Font("SimSun",10);
			obj_VO=VO;
		}
		
		#region ��ӡ����
		private void m_mthPrintTitle()
		{
			Pen objPen =new Pen(Color.Black,1);
			//����
			SizeF objFontSize =objDraw.Graphics.MeasureString(obj_VO.HospitalTitle.Trim(),this.objFontTitle);
			float X=(this.objDraw.PageBounds.Width-objFontSize.Width)/2;
			Y=this.objDraw.PageBounds.Height*0.05f;
			objDraw.Graphics.DrawString(obj_VO.HospitalTitle,objFontTitle,Brushes.Black,X,Y);
			Y+=objFontSize.Height+7;
			objFontSize =objDraw.Graphics.MeasureString("��",this.objFontNormal);
			this.fltRowHeight= objFontSize.Height+1;
			this.SingleFontWidth =objFontSize.Width;
			H =this.fltRowHeight-8;
			//ҽ�Ƹ��ʽ:
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objFontSize =objDraw.Graphics.MeasureString("ҽ�Ƹ��ʽ:",this.objFontNormal);
			objDraw.Graphics.DrawString("ҽ�Ƹ��ʽ:   ��",objFontNormal,Brushes.Black,X,Y);
			//ҽ����
			X=this.objDraw.PageBounds.Width*0.25f;
			objDraw.Graphics.DrawString("ҽ����:"+obj_VO.InsuranceNo,objFontNormal,Brushes.Black,X,Y);
			//��N��סԺ
			X=this.objDraw.PageBounds.Width*0.5f;
			objDraw.Graphics.DrawString("�� "+obj_VO.InHospitalTimes+" ��סԺ",objFontNormal,Brushes.Black,X,Y);
			//������
			X=this.objDraw.PageBounds.Width*0.7f;
			objFontSize =objDraw.Graphics.MeasureString("������:",this.objFontNormal);
			objDraw.Graphics.DrawString("������:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.strNo,objFontNormal,Brushes.Black,X,Y);
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,X+50,Y+objFontSize.Height);
			Y+=this.fltRowHeight-2;
			//����
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			Y+=this.fltRowHeight-2;
			float RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			objDraw.Graphics.DrawLine(objPen,X,Y,RX,Y);
			Y+=this.fltRowHeight-2;
			//����
			objFontSize =objDraw.Graphics.MeasureString("����:",this.objFontNormal);
			objDraw.Graphics.DrawString("����:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.PatientName,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*5;
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			objFontSize =objDraw.Graphics.MeasureString("�Ա�: �� ",this.objFontNormal);
			objDraw.Graphics.DrawString("�Ա�: �� 1.�� 2.Ů",objFontNormal,Brushes.Black,X,Y);
			using(Font fontemp =new Font("SimSun",14))
			{
				if(obj_VO.Sex=="��")
				{
					objDraw.Graphics.DrawString("��",fontemp,Brushes.Black,X+objFontSize.Width,Y);
				}
				else
				{
					objDraw.Graphics.DrawString("��",fontemp,Brushes.Black,X+objFontSize.Width*1.5f+2,Y);
				}
			}
			//��������
			X=this.objDraw.PageBounds.Width*0.45f;
			objDraw.Graphics.DrawString("����",objFontNormal,Brushes.Black,X,Y);
			X+=this.SingleFontWidth*2+2;
			objDraw.Graphics.DrawString(obj_VO.BirthDay_Year,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*2;
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			objDraw.Graphics.DrawString("��",objFontNormal,Brushes.Black,X,Y);
			X+=this.SingleFontWidth+2;
			objDraw.Graphics.DrawString(obj_VO.BirthDay_Month,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth+2;
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			objDraw.Graphics.DrawString("��",objFontNormal,Brushes.Black,X,Y);
			X+=this.SingleFontWidth+2;
			objDraw.Graphics.DrawString(obj_VO.BirthDay_Day,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth+2;
			
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX;
			objDraw.Graphics.DrawString("��",objFontNormal,Brushes.Black,X,Y);
			//����
			X=this.objDraw.PageBounds.Width*0.7f;
			objFontSize =objDraw.Graphics.MeasureString("����",this.objFontNormal);
			objDraw.Graphics.DrawString("����",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.Age,objFontNormal,Brushes.Black,X,Y);
			//����
			RX =X+SingleFontWidth*2;
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			objFontSize =objDraw.Graphics.MeasureString("����: �� ",this.objFontNormal);
			objDraw.Graphics.DrawString("����: �� 1.δ 2.��",objFontNormal,Brushes.Black,X,Y);
			using(Font fontemp =new Font("SimSun",14))
			{
				if(obj_VO.Marry=="��")
				{
					objDraw.Graphics.DrawString("��",fontemp,Brushes.Black,X+objFontSize.Width,Y);
				}
				else
				{
					objDraw.Graphics.DrawString("��",fontemp,Brushes.Black,X+objFontSize.Width*1.5f+2,Y);
				}
			}
			this.Y+=this.fltRowHeight+10;

			//ְҵ
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objFontSize =objDraw.Graphics.MeasureString("ְҵ:",this.objFontNormal);
			objDraw.Graphics.DrawString("ְҵ:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.Work,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*5;	
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			//������
			objFontSize =objDraw.Graphics.MeasureString("������",this.objFontNormal);
			objDraw.Graphics.DrawString("������",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.BirthPlace,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*4;	
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			//ʡ(��)
			objFontSize =objDraw.Graphics.MeasureString("ʡ(��)",this.objFontNormal);
			objDraw.Graphics.DrawString("ʡ(��)",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.County,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*2.5f;	
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX;
			float RXTemp =RX;
			objDraw.Graphics.DrawString("��",objFontNormal,Brushes.Black,X,Y);
			X+=SingleFontWidth*2;
			//����
			objFontSize =objDraw.Graphics.MeasureString("����",this.objFontNormal);
			objDraw.Graphics.DrawString("����",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.Nationality,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*2;	
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+10;
			//����
			objFontSize =objDraw.Graphics.MeasureString("����",this.objFontNormal);
			objDraw.Graphics.DrawString("����",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.Country,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*2;	
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+10;
			//���֤��
			objFontSize =objDraw.Graphics.MeasureString("���֤��",this.objFontNormal);
			objDraw.Graphics.DrawString("���֤��",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.PID,objFontNormal,Brushes.Black,X,Y);
			RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			this.Y+=this.fltRowHeight+10;
			//������λ����ַ
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objFontSize =objDraw.Graphics.MeasureString("������λ����ַ:",this.objFontNormal);
			objDraw.Graphics.DrawString("������λ����ַ:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.WorkAddress,objFontNormal,Brushes.Black,X,Y);
			RX =RXTemp;
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			//�绰
			objFontSize =objDraw.Graphics.MeasureString("�绰",this.objFontNormal);
			objDraw.Graphics.DrawString("�绰",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.WorkTel,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*6;
			RXTemp =RX;
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			//��������
			objFontSize =objDraw.Graphics.MeasureString("��������",this.objFontNormal);
			objDraw.Graphics.DrawString("��������",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.WorkPostalcode,objFontNormal,Brushes.Black,X,Y);
			RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			this.Y+=this.fltRowHeight+10;
			//���ڵ�ַ
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objFontSize =objDraw.Graphics.MeasureString("���ڵ�ַ:",this.objFontNormal);
			objDraw.Graphics.DrawString("���ڵ�ַ:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.HomeAddress,objFontNormal,Brushes.Black,X,Y);
			RX =RXTemp;
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			//��������
			objFontSize =objDraw.Graphics.MeasureString("��������",this.objFontNormal);
			objDraw.Graphics.DrawString("��������",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.HomePostalcode,objFontNormal,Brushes.Black,X,Y);
			RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			this.Y+=this.fltRowHeight+10;

			//��ϵ������
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objFontSize =objDraw.Graphics.MeasureString("��ϵ������:",this.objFontNormal);
			objDraw.Graphics.DrawString("��ϵ������:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.LinkMan,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*5;	
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			//��ϵ
			objFontSize =objDraw.Graphics.MeasureString("��ϵ",this.objFontNormal);
			objDraw.Graphics.DrawString("��ϵ",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.Relation,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*3;	
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			//��ַ
			objFontSize =objDraw.Graphics.MeasureString("��ַ",this.objFontNormal);
			objDraw.Graphics.DrawString("��ַ",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.RelationAddress,objFontNormal,Brushes.Black,X,Y);
			RX =RXTemp;	
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX;
			//�绰
			objFontSize =objDraw.Graphics.MeasureString("�绰",this.objFontNormal);
			objDraw.Graphics.DrawString("�绰",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.RelationTel,objFontNormal,Brushes.Black,X,Y);
			RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+10;
			this.Y+=this.fltRowHeight+20;
			//////////////////////
			//��Ժ����
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objFontSize =objDraw.Graphics.MeasureString("��Ժ����:",this.objFontNormal);
			objDraw.Graphics.DrawString("��Ժ����:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;	
			objDraw.Graphics.DrawString(obj_VO.BirthDay_Year,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*2;
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			objDraw.Graphics.DrawString("��",objFontNormal,Brushes.Black,X,Y);
			X+=this.SingleFontWidth+2;
			objDraw.Graphics.DrawString(obj_VO.InhospitalDate_Month,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth+2;
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			objDraw.Graphics.DrawString("��",objFontNormal,Brushes.Black,X,Y);
			X+=this.SingleFontWidth+2;
			objDraw.Graphics.DrawString(obj_VO.InhospitalDate_Day,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth+2;
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			objDraw.Graphics.DrawString("��",objFontNormal,Brushes.Black,X,Y);
			X+=this.SingleFontWidth+2;
			objDraw.Graphics.DrawString(obj_VO.InhospitalDate_Hour,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth+2;
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			objDraw.Graphics.DrawString("ʱ",objFontNormal,Brushes.Black,X,Y);
//			X+=this.SingleFontWidth+2;
//			objDraw.Graphics.DrawString(obj_VO.InhospitalDate_Hour,objFontNormal,Brushes.Black,X,Y);

			//��Ժ�Ʊ�
			X=this.objDraw.PageBounds.Width*0.5f;
			objFontSize =objDraw.Graphics.MeasureString("��Ժ�Ʊ�",this.objFontNormal);
			objDraw.Graphics.DrawString("��Ժ�Ʊ�",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.Department,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*6;
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			//����
			objFontSize =objDraw.Graphics.MeasureString("����",this.objFontNormal);
			objDraw.Graphics.DrawString("����",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.Room,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*6;
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			this.Y+=this.fltRowHeight+10;
			//����
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objFontSize =objDraw.Graphics.MeasureString("����:",this.objFontNormal);
			objDraw.Graphics.DrawString("����:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.Money,objFontNormal,Brushes.Black,X,Y);
			RX =X+SingleFontWidth*5;
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			X=RX+2;
			objDraw.Graphics.DrawString("Ԫ",objFontNormal,Brushes.Black,X,Y);
			//��Ժʱ���
			X=this.objDraw.PageBounds.Width*0.5f;
			objFontSize =objDraw.Graphics.MeasureString("��Ժʱ���:",this.objFontNormal);
			objDraw.Graphics.DrawString("��Ժʱ���:1.Σ 2.�� 3.һ�� ",objFontNormal,Brushes.Black,X,Y);
			using(Font fontemp =new Font("SimSun",14))
			{
				switch(obj_VO.InHospitalCase)
				{
					case "Σ":
						objDraw.Graphics.DrawString("��",fontemp,Brushes.Black,X+objFontSize.Width-SingleFontWidth,Y);
						break;
					case "��":
						objDraw.Graphics.DrawString("��",fontemp,Brushes.Black,X+objFontSize.Width*1.5f-SingleFontWidth,Y);
						break;
					default :
						objDraw.Graphics.DrawString("��",fontemp,Brushes.Black,X+objFontSize.Width*2f-SingleFontWidth,Y);
						break;
				}
			}
			this.Y+=this.fltRowHeight+10;
			/////////////////////////////////////////////
			//��(��)�����
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objFontSize =objDraw.Graphics.MeasureString("��(��)�����:",this.objFontNormal);
			objDraw.Graphics.DrawString("��(��)�����:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
            RX =this.objDraw.PageBounds.Width*0.6f;
			objFontSize =objDraw.Graphics.MeasureString(obj_VO.Diag,this.objFontNormal);
			float y =Y+this.fltRowHeight;
			objDraw.Graphics.DrawLine(objPen,X,y,RX,y);
			 y +=this.fltRowHeight+10;
			objDraw.Graphics.DrawLine(objPen,X,y,RX,y);
			if((RX-X)>objFontSize.Width)
			{
			//һ��
				objDraw.Graphics.DrawString(obj_VO.Diag,objFontNormal,Brushes.Black,X,Y);

			}
			else
			{
				//����
				RXTemp =X;
				y=Y;
				bool flag =false;
				for(int i=0;i<obj_VO.Diag.Length;i++)
				{
					objFontSize =objDraw.Graphics.MeasureString(obj_VO.Diag[i].ToString(),this.objFontNormal);
                    objDraw.Graphics.DrawString(obj_VO.Diag[i].ToString(),objFontNormal,Brushes.Black,RXTemp,y);
					RXTemp+=objFontSize.Width-3;
					if(RXTemp>=RX)
					{
						if(flag)
						{
						break;
						}
						y+=this.fltRowHeight+10;
						RXTemp =X;
						flag =true;
					}
				}
			}
			//��(��)�����ҽ��
			X=this.objDraw.PageBounds.Width*0.65f;
			objFontSize =objDraw.Graphics.MeasureString("��(��)�����ҽ��:",this.objFontNormal);
			objDraw.Graphics.DrawString("��(��)�����ҽ��:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.DoctorName,objFontNormal,Brushes.Black,X,Y);
			RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			this.Y+=this.fltRowHeight+10;

			//ҽ������
			X=this.objDraw.PageBounds.Width*0.65f;
			objFontSize =objDraw.Graphics.MeasureString("ҽ������:",this.objFontNormal);
			objDraw.Graphics.DrawString("ҽ������:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.DoctorNo,objFontNormal,Brushes.Black,X,Y);
			RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
			this.Y+=this.fltRowHeight+10;

			//��ע
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objFontSize =objDraw.Graphics.MeasureString("��ע:",this.objFontNormal);
			objDraw.Graphics.DrawString("��ע:",objFontNormal,Brushes.Black,X,Y);
			X+=objFontSize.Width+2;
			objDraw.Graphics.DrawString(obj_VO.Remark,objFontNormal,Brushes.Black,X,Y);
			RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			//����
			objDraw.Graphics.DrawLine(objPen,X,Y+objFontSize.Height,RX,Y+objFontSize.Height);
		}
		#endregion
		#region ��ӡ����
		
		private void m_mthPrintText()
		{
		
		}
		
		#endregion
		#region ��ӡҳ��
		private void m_mthPrintEnd()
		{
		}
		#endregion
		#region ��ʼ��ӡ
		public void m_mthBegionPrint(System.Drawing.Printing.PrintPageEventArgs p_obj)
		{
			objDraw=p_obj;
			this.m_mthPrintTitle();
			this.m_mthPrintText();
			this.m_mthPrintEnd();
		}
		#endregion
	}
}
