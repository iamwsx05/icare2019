using System;
using System.Drawing;
using weCare.Core.Entity;
using System.Drawing.Printing;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsPrintSingleWorkLoad ��ժҪ˵����
	/// </summary>
	public class clsPrintSingleWorkLoad:IDisposable
	{
		public void Dispose()
		{
			objFontTitle1.Dispose();
			objFontTitle1 =null;
			objFontTitle2.Dispose();
			objFontTitle2 =null;
			objFontNormal.Dispose();
			objFontNormal =null;
			GC.SuppressFinalize(this);

		}
		~clsPrintSingleWorkLoad()
		{
			this.Dispose();
		}
		/// <summary>
		/// ��ͼ����
		/// </summary>
		private System.Drawing.Printing.PrintPageEventArgs objDraw;
		private clsSingleWorkLoad_VO obj_VO;
		/// <summary>
		/// ��������1
		/// </summary>
		Font objFontTitle1;
		/// <summary>
		/// ��������2
		/// </summary>
		Font objFontTitle2;
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
		private  float  fltRowHeight=10;
		/// <summary>
		///�п�
		/// </summary>
		private  float  fltRowWidth=0.29f;

		public string type="";
		/// <summary>
		/// ������
		/// </summary>
		private float  Y;
		public clsPrintSingleWorkLoad(System.Drawing.Printing.PrintPageEventArgs p_obj,clsSingleWorkLoad_VO VO)
		{
			objDraw=p_obj;
			objFontTitle1=new Font("SimSun",12,FontStyle.Bold);
			objFontTitle2=new Font("SimSun",16,FontStyle.Bold);
			objFontNormal=new Font("SimSun",10);
			obj_VO=VO;
		}
		#region ��ӡ����
		private void m_mthPrintTitle()
		{

			//����
			//ҽԺ����
			
			if(type.Trim() != "ȫ��" && type !="")
				
				type=  "("+type+")";
			else
				type="";
			SizeF objFontSize =objDraw.Graphics.MeasureString(obj_VO.m_strHospitalName,this.objFontTitle1);
			float X=(this.objDraw.PageBounds.Width-objFontSize.Width)/2;
			Y=this.objDraw.PageBounds.Height*0.047f-(objFontSize.Height/2);
			objDraw.Graphics.DrawString(obj_VO.m_strHospitalName,objFontTitle1,Brushes.Black,X,Y);
			Y+=objFontSize.Height+5;
			//��������
			objFontSize =objDraw.Graphics.MeasureString(obj_VO.m_strTitle + type ,this.objFontTitle2);
			X=(this.objDraw.PageBounds.Width-objFontSize.Width)/2;
			objDraw.Graphics.DrawString(obj_VO.m_strTitle + type,objFontTitle2,Brushes.Black,X,Y);
		//
			Y+=50;
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			objFontSize =objDraw.Graphics.MeasureString(obj_VO.m_strOwnerName,this.objFontNormal);
			objDraw.Graphics.DrawString(obj_VO.m_strOwnerName,objFontNormal,Brushes.Black,X,Y);
			//����
			X+=objFontSize.Width+20;
			objDraw.Graphics.DrawString("����:"+obj_VO.m_strBeginDate+" �� "+obj_VO.m_strEndDate,objFontNormal,Brushes.Black,X,Y);
			//����
			Y+=objFontSize.Height+2;
			X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			float RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			objDraw.Graphics.DrawLine(new Pen(Color.Black,2),X,Y,RX,Y);

					
		}
		#endregion
		#region ��ӡ����
		private void m_mthPrintText()
		{
			Y+=this.fltRowHeight;
			SizeF objFontSize =objDraw.Graphics.MeasureString("����:",this.objFontNormal);
			float X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			int flag =0;
			if(this.obj_VO.objSubItmeArr ==null)
			{
			return;
			}
			for(int i=0;i<this.obj_VO.objSubItmeArr.Length;i++)
			{
				if(flag==0)
				{
					objDraw.Graphics.DrawString(this.obj_VO.objSubItmeArr[i].m_strCatName+": "+this.obj_VO.objSubItmeArr[i].m_strCatMoney,objFontNormal,Brushes.Black,X,Y);
					flag =1;
					continue;
				}
				if(flag ==1)
				{
					X=this.objDraw.PageBounds.Width*fltLeftIndentProp+this.objDraw.PageBounds.Width*fltRowWidth;
					objDraw.Graphics.DrawString(this.obj_VO.objSubItmeArr[i].m_strCatName+": "+this.obj_VO.objSubItmeArr[i].m_strCatMoney,objFontNormal,Brushes.Black,X,Y);
					flag =2;
					continue;
				}
				if(flag ==2)
				{
					X=this.objDraw.PageBounds.Width*fltLeftIndentProp+this.objDraw.PageBounds.Width*fltRowWidth*2;
					objDraw.Graphics.DrawString(this.obj_VO.objSubItmeArr[i].m_strCatName+": "+this.obj_VO.objSubItmeArr[i].m_strCatMoney,objFontNormal,Brushes.Black,X,Y);
					flag =0;
					Y+=this.fltRowHeight+objFontSize.Height;
					X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
					continue;
				}
				
			}

		}
		#endregion
		#region ��ӡҳ��
		private void m_mthPrintEnd()
		{
			//�ϼ�
			Y=this.objDraw.PageBounds.Height*0.80f;
			float X=this.objDraw.PageBounds.Width*(1-fltLeftIndentProp*4);
			objDraw.Graphics.DrawString("�ϼ�"+": "+this.obj_VO.strSumMoney,objFontTitle1,Brushes.Black,X,Y);
			//����
			Y=this.objDraw.PageBounds.Height*0.82f;
			 X=this.objDraw.PageBounds.Width*fltLeftIndentProp;
			float RX=this.objDraw.PageBounds.Width*(1-fltRightIndentProp);
			objDraw.Graphics.DrawLine(new Pen(Color.Black,2),X,Y,RX,Y);

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
