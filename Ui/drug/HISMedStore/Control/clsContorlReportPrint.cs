using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsContorlReportPrint ��ժҪ˵����
	/// </summary>
	public class clsContorlReportPrint : com.digitalwave.GUI_Base.clsController_Base
	{
		public clsContorlReportPrint()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ����
		int currPage=2;
		float x1;
		float x2;
		float fwidth;
		float width;
		Pen blackPen = new Pen(Color.Black, 1);
		System.Drawing.Font TextFont=new Font("����",14);//����ʹ�õ�����
		System.Drawing.Font DigFont=new Font("����",16,System.Drawing.FontStyle.Bold);
		System.Drawing.Font DigFontText=new Font("����",14,System.Drawing.FontStyle.Bold);
		
		SizeF szPerWord;//ÿ���ֵĿ��
		SizeF szPerStr;//�ַ����Ŀ��
		int RowHight;
		int StartRow;
		#endregion

		#region ��ҩ�ı�ͷ
		private  void m_mthPrintTitleEN(System.Drawing.Printing.PrintPageEventArgs e,clsReportSendMedStart_VO ReportSendMedStart)
		{
			System.Drawing.Font LFont=new Font("����",11);
			x1=50.0F;
			x2 = 800.0F;
			fwidth=50;
			//			height=100;
			RowHight=25;
			StartRow=18;
			width=fwidth;
			int RowHight1=20;
			szPerWord = e.Graphics.MeasureString("��",TextFont);//��ȡһ���ַ��Ŀ��
			e.Graphics.DrawString("��ҩ��"+"��"+ReportSendMedStart.m_strInternalEN+"��",DigFont,Brushes.Black,400,StartRow);
			StartRow+=RowHight1*2;
			
			e.Graphics.DrawString("������",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("������",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strname,LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strname,LFont);
			width+=szPerStr.Width+100;
			e.Graphics.DrawString("���䣺",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("���䣺",LFont);
			width+=szPerStr.Width;
			if(ReportSendMedStart.m_strAge=="")
			{
				e.Graphics.DrawString("����",LFont,Brushes.Black,width,StartRow);
			}
			else
			{
				e.Graphics.DrawString(ReportSendMedStart.m_strAge,LFont,Brushes.Black,width,StartRow);
			}
			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strAge,LFont);
			width+=szPerStr.Width+100;
			e.Graphics.DrawString("�Ա�",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("�Ա�",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strsex,LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strsex,LFont);
			width+=szPerStr.Width+130;
			e.Graphics.DrawString("����ҽ��:",LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString("����ҽ��:",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strDoctorName,LFont,Brushes.Black,width,StartRow);
			StartRow+=RowHight1;
			width=fwidth;
			e.Graphics.DrawString("���￨�ţ�",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("���￨�ţ�",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strPatCardID,LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strPatCardID,LFont);
			width+=szPerStr.Width+110;
			e.Graphics.DrawString("����ʱ�䣺",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("����ʱ�䣺",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strRegisterdate,LFont,Brushes.Black,width,StartRow);
			width+=150;
			e.Graphics.DrawString("��ӡʱ�䣺",LFont,Brushes.Black,width,StartRow);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strPintdate,LFont,Brushes.Black,width,StartRow);

			StartRow+=15;
			e.Graphics.DrawLine(blackPen, x1, StartRow, x2, StartRow);
			StartRow+=15;
				width=fwidth;
				e.Graphics.DrawString("ҩƷ����",TextFont,Brushes.Black,width,StartRow);
				e.Graphics.DrawString("���",TextFont,Brushes.Black,width+300,StartRow);
				e.Graphics.DrawString("����",TextFont,Brushes.Black,width+520,StartRow);
				e.Graphics.DrawString("�÷�",TextFont,Brushes.Black,width+580,StartRow);
				e.Graphics.DrawString("����",TextFont,Brushes.Black,width+700,StartRow);

		}
		#endregion

		#region ��ҩ�ı�ͷ
		private  void m_mthPrintTitleCH(System.Drawing.Printing.PrintPageEventArgs e,clsReportSendMedStart_VO ReportSendMedStart)
		{
			System.Drawing.Font LFont=new Font("����",11);
			x1=50.0F;
			x2 = 800.0F;
			fwidth=50;
			RowHight=25;
			StartRow=18;
			width=fwidth;
			int RowHight1=20;
			szPerWord = e.Graphics.MeasureString("��",TextFont);//��ȡһ���ַ��Ŀ��
			e.Graphics.DrawString("��ҩ��"+"��"+ReportSendMedStart.m_strInternalCH+"��",DigFont,Brushes.Black,400,StartRow);
			StartRow+=RowHight1*2;
			
			e.Graphics.DrawString("������",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("������",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strname,LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strname,LFont);
			width+=szPerStr.Width+100;
			e.Graphics.DrawString("���䣺",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("���䣺",LFont);
			width+=szPerStr.Width;
			if(ReportSendMedStart.m_strAge=="")
			{
				e.Graphics.DrawString("����",LFont,Brushes.Black,width,StartRow);
			}
			else
			{
				e.Graphics.DrawString(ReportSendMedStart.m_strAge,LFont,Brushes.Black,width,StartRow);
			}
			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strAge,LFont);
			width+=szPerStr.Width+100;
			e.Graphics.DrawString("�Ա�",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("�Ա�",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strsex,LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strsex,LFont);
			width+=szPerStr.Width+130;
			e.Graphics.DrawString("����ҽ��:",LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString("����ҽ��:",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strDoctorName,LFont,Brushes.Black,width,StartRow);
			StartRow+=RowHight1;
			width=fwidth;
			e.Graphics.DrawString("���￨�ţ�",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("���￨�ţ�",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strPatCardID,LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strPatCardID,LFont);
			width+=szPerStr.Width+110;
			e.Graphics.DrawString("����ʱ�䣺",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("����ʱ�䣺",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strRegisterdate,LFont,Brushes.Black,width,StartRow);
			width+=150;
			e.Graphics.DrawString("��ӡʱ�䣺",LFont,Brushes.Black,width,StartRow);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strPintdate,LFont,Brushes.Black,width,StartRow);

			StartRow+=15;
			e.Graphics.DrawLine(blackPen, x1, StartRow, x2, StartRow);
		}
		#endregion

		#region ҳβ
		private void m_printEend(System.Drawing.Printing.PrintPageEventArgs e,clsReportSendMedStart_VO ReportSendMedStart)
		{
			float StartRow1;
			float width1;
			int pageHight=e.PageBounds.Height;
			StartRow1=pageHight-50;
			e.Graphics.DrawLine(blackPen, x1, StartRow1, x2, StartRow1);
			StartRow1+=3;
			width1=fwidth;
			e.Graphics.DrawString("��ҩ�ˣ�",TextFont,Brushes.Black,width1,StartRow1);
			szPerStr=e.Graphics.MeasureString("��ҩ�ˣ�",TextFont);
			width1+=250;
			e.Graphics.DrawString("�����ˣ�",TextFont,Brushes.Black,width1,StartRow1);
			width1+=230;
			e.Graphics.DrawString("�ϼƽ�",TextFont,Brushes.Black,width1,StartRow1);
			szPerStr=e.Graphics.MeasureString("�ϼƽ�",TextFont);
			width1+=szPerStr.Width;
			e.Graphics.DrawString("��"+ReportSendMedStart.m_strTotalMoney+"Ԫ",TextFont,Brushes.Black,width1,StartRow1);
		}
		#endregion

		#region ��ӡ��ҩ����
		private void m_printMedEN(System.Drawing.Printing.PrintPageEventArgs e,clsReportSendMed_VO[] sendMedEN,int currRow,int endRow)
		{
			width=fwidth;
			StartRow+=RowHight;
			if(sendMedEN.Length < 14)
			{
				for(int i1=currRow;i1<endRow;i1++)
				{
					if(e.Graphics.MeasureString(sendMedEN[i1].m_strMedName,TextFont).Width>307)
					{
						StartRow+=10;
						string str1=sendMedEN[i1].m_strMedName.Substring(0,15);
						string str2=sendMedEN[i1].m_strMedName.Substring(15);
						e.Graphics.DrawString(str1,TextFont,Brushes.Black,width,StartRow-13);
						e.Graphics.DrawString(str2,TextFont,Brushes.Black,width,StartRow+2);
					}
					else
					{
						e.Graphics.DrawString(sendMedEN[i1].m_strMedName,TextFont,Brushes.Black,width,StartRow);
					}
					e.Graphics.DrawString(sendMedEN[i1].m_strMedSpace,TextFont,Brushes.Black,width+300,StartRow);
					if(sendMedEN[i1].m_strdosage.Trim()!="")
						e.Graphics.DrawString(sendMedEN[i1].m_strdosage.Trim()+sendMedEN[i1].m_strdosageUnit,TextFont,Brushes.Black,width+520,StartRow);
					e.Graphics.DrawString(sendMedEN[i1].m_strUseName.Trim(),TextFont,Brushes.Black,width+580,StartRow);
					e.Graphics.DrawString(sendMedEN[i1].m_strTotal+sendMedEN[i1].m_strMedUnit,TextFont,Brushes.Black,width+700,StartRow);
					StartRow+=RowHight;
				}
			}
			else
			{
				for(int i1=currRow;i1<endRow;i1++)
				{
					if(e.Graphics.MeasureString(sendMedEN[i1].m_strMedName,TextFont).Width>307)
					{
						StartRow+=10;
						string str1=sendMedEN[i1].m_strMedName.Substring(0,15);
						string str2=sendMedEN[i1].m_strMedName.Substring(15);
						e.Graphics.DrawString(str1,TextFont,Brushes.Black,width,StartRow-13);
						e.Graphics.DrawString(str2,TextFont,Brushes.Black,width,StartRow+2);
					}
					else
					{
						e.Graphics.DrawString(sendMedEN[i1].m_strMedName,TextFont,Brushes.Black,width,StartRow);
					}
					e.Graphics.DrawString(sendMedEN[i1].m_strMedSpace,TextFont,Brushes.Black,width+300,StartRow);
					if(sendMedEN[i1].m_strdosage.Trim()!="")
						e.Graphics.DrawString(sendMedEN[i1].m_strdosage.Trim()+sendMedEN[i1].m_strdosageUnit,TextFont,Brushes.Black,width+520,StartRow);
					e.Graphics.DrawString(sendMedEN[i1].m_strUseName.Trim(),TextFont,Brushes.Black,width+580,StartRow);
					e.Graphics.DrawString(sendMedEN[i1].m_strTotal+sendMedEN[i1].m_strMedUnit,TextFont,Brushes.Black,width+700,StartRow);
					StartRow+=RowHight;
				}
			}
//			e.HasMorePages=false;
		}
		#endregion

		#region ��ӡ��ҩ����
		private void m_printMedCH(System.Drawing.Printing.PrintPageEventArgs e,clsReportSendMed_VO[] sendMedCH,clsReportSendMedStart_VO ReportSendMedStart,int currRow,int endRow,string strMedType)
		{
			if(currRow==0&&strMedType=="3")
			{
				StartRow+=15;
				width=fwidth;
				e.Graphics.DrawString("ҩƷ����",TextFont,Brushes.Black,width,StartRow);
				e.Graphics.DrawString("����",TextFont,Brushes.Black,width+150,StartRow);
				e.Graphics.DrawString("�÷�",TextFont,Brushes.Black,width+250,StartRow);
				width+=400;
				e.Graphics.DrawString("ҩƷ����",TextFont,Brushes.Black,width,StartRow);
				e.Graphics.DrawString("����",TextFont,Brushes.Black,width+150,StartRow);
				e.Graphics.DrawString("�÷�",TextFont,Brushes.Black,width+250,StartRow);
				StartRow+=RowHight;
			}
			width=fwidth;


			if(strMedType=="2"&&currRow==0)
			{
				StartRow+=10;
				e.Graphics.DrawString("ҩƷ����",TextFont,Brushes.Black,width,StartRow);
				e.Graphics.DrawString("���",TextFont,Brushes.Black,width+300,StartRow);
				e.Graphics.DrawString("����",TextFont,Brushes.Black,width+520,StartRow);
				e.Graphics.DrawString("�÷�",TextFont,Brushes.Black,width+580,StartRow);
				e.Graphics.DrawString("����",TextFont,Brushes.Black,width+700,StartRow);
			}
			if(strMedType=="3")
			{
				if(sendMedCH.Length<26)
				{
					for(int i1=0;i1<endRow;i1++)
					{
						e.Graphics.DrawString(sendMedCH[i1].m_strMedName,TextFont,Brushes.Black,width,StartRow);
						e.Graphics.DrawString(sendMedCH[i1].m_strdosage+sendMedCH[i1].m_strdosageUnit,TextFont,Brushes.Black,width+150,StartRow);
						e.Graphics.DrawString(sendMedCH[i1].m_strUseName,TextFont,Brushes.Black,width+250,StartRow);
						if(i1+1<endRow)
						{
							float tempWith=width+400;
							e.Graphics.DrawString(sendMedCH[i1+1].m_strMedName,TextFont,Brushes.Black,tempWith,StartRow);
							e.Graphics.DrawString(sendMedCH[i1+1].m_strdosage+sendMedCH[i1+1].m_strdosageUnit,TextFont,Brushes.Black,tempWith+150,StartRow);
							e.Graphics.DrawString(sendMedCH[i1+1].m_strUseName,TextFont,Brushes.Black,tempWith+250,StartRow);
							i1++;
						}
						StartRow+=RowHight;
					}
				}
				else
				{
				
					for(int i1=currRow;i1<endRow;i1++)
					{
						e.Graphics.DrawString(sendMedCH[i1].m_strMedName,TextFont,Brushes.Black,width,StartRow);
						e.Graphics.DrawString(sendMedCH[i1].m_strdosage+sendMedCH[i1].m_strdosageUnit,TextFont,Brushes.Black,width+150,StartRow);
						e.Graphics.DrawString(sendMedCH[i1].m_strUseName,TextFont,Brushes.Black,width+250,StartRow);
						if(i1+1<sendMedCH.Length)
						{
							float tempWith=width+400;
							e.Graphics.DrawString(sendMedCH[i1+1].m_strMedName,TextFont,Brushes.Black,tempWith,StartRow);
							e.Graphics.DrawString(sendMedCH[i1+1].m_strdosage+sendMedCH[i1+1].m_strdosageUnit,TextFont,Brushes.Black,tempWith+150,StartRow);
							e.Graphics.DrawString(sendMedCH[i1+1].m_strUseName,TextFont,Brushes.Black,tempWith+250,StartRow);
							i1++;
						}
						StartRow+=RowHight;
					}
				}

				
				int pgHight=e.PageBounds.Height;
				float StartRow1=pgHight-70;
				float width1=fwidth;
				e.Graphics.DrawString("������",DigFontText,Brushes.Black,width1,StartRow1);
				e.Graphics.DrawString("�����÷���",TextFont,Brushes.Black,width1+200,StartRow1);
				try
				{
					if(sendMedCH.Length > 0)
					{
						szPerStr=e.Graphics.MeasureString("�����÷���",TextFont);
						e.Graphics.DrawString(ReportSendMedStart.m_strUseNameAll,TextFont,Brushes.Black,width+200+szPerStr.Width,StartRow1);
						szPerStr=e.Graphics.MeasureString("������",DigFont);
						width1+=szPerStr.Width;
						e.Graphics.DrawString(ReportSendMedStart.m_strSun.Trim(),DigFontText,Brushes.Black,width1,StartRow1);
					}

				}
				catch
				{
				}

			}
			else
			{
				m_printMedEN(e,sendMedCH,currRow,endRow);
			}
		}
		#endregion

		#region ��ҩ�ı�ͷ
		private  void m_mthPrintTitleCHPage(System.Drawing.Printing.PrintPageEventArgs e,clsReportSendMedStart_VO ReportSendMedStart,string strPage)
		{
			System.Drawing.Font LFont=new Font("����",11);
			x1=50.0F;
			x2 = 800.0F;
			fwidth=50;
			RowHight=25;
			StartRow=18;
			width=fwidth;
			int RowHight1=20;
			szPerWord = e.Graphics.MeasureString("��",TextFont);//��ȡһ���ַ��Ŀ��
			e.Graphics.DrawString("��ҩ��"+"��"+ReportSendMedStart.m_strInternalCH+"��",DigFont,Brushes.Black,350,StartRow);
			e.Graphics.DrawString(strPage+"ҳ",DigFont,Brushes.Black,600,StartRow);
			StartRow+=RowHight1*2;
			
			e.Graphics.DrawString("������",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("������",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strname,LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strname,LFont);
			width+=szPerStr.Width+100;
			e.Graphics.DrawString("���䣺",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("���䣺",LFont);
			width+=szPerStr.Width;
			if(ReportSendMedStart.m_strAge=="")
			{
				e.Graphics.DrawString("����",LFont,Brushes.Black,width,StartRow);
			}
			else
			{
				e.Graphics.DrawString(ReportSendMedStart.m_strAge,LFont,Brushes.Black,width,StartRow);
			}
			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strAge,LFont);
			width+=szPerStr.Width+100;
			e.Graphics.DrawString("�Ա�",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("�Ա�",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strsex,LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strsex,LFont);
			width+=szPerStr.Width+130;
			e.Graphics.DrawString("����ҽ��:",LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString("����ҽ��:",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strDoctorName,LFont,Brushes.Black,width,StartRow);
			StartRow+=RowHight1;
			width=fwidth;
			e.Graphics.DrawString("���￨�ţ�",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("���￨�ţ�",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strPatCardID,LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strPatCardID,LFont);
			width+=szPerStr.Width+110;
			e.Graphics.DrawString("����ʱ�䣺",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("����ʱ�䣺",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strRegisterdate,LFont,Brushes.Black,width,StartRow);
			width+=150;
			e.Graphics.DrawString("��ӡʱ�䣺",LFont,Brushes.Black,width,StartRow);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strPintdate,LFont,Brushes.Black,width,StartRow);

			StartRow+=15;
			e.Graphics.DrawLine(blackPen, x1, StartRow, x2, StartRow);
		}
		#endregion

		#region ��ҩ�ı�ͷ
		private  void m_mthPrintTitleENPage(System.Drawing.Printing.PrintPageEventArgs e,clsReportSendMedStart_VO ReportSendMedStart,string strPage)
		{
			System.Drawing.Font LFont=new Font("����",11);
			x1=50.0F;
			x2 = 800.0F;
			fwidth=50;
			//			height=100;
			RowHight=25;
			StartRow=18;
			width=fwidth;
			int RowHight1=20;
			szPerWord = e.Graphics.MeasureString("��",TextFont);//��ȡһ���ַ��Ŀ��
			e.Graphics.DrawString("��ҩ��"+"��"+ReportSendMedStart.m_strInternalEN+"��",DigFont,Brushes.Black,350,StartRow);

			e.Graphics.DrawString(strPage+"ҳ",DigFont,Brushes.Black,600,StartRow);
			StartRow+=RowHight1*2;
			
			e.Graphics.DrawString("������",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("������",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strname,LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strname,LFont);
			width+=szPerStr.Width+100;
			e.Graphics.DrawString("���䣺",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("���䣺",LFont);
			width+=szPerStr.Width;
			if(ReportSendMedStart.m_strAge=="")
			{
				e.Graphics.DrawString("����",LFont,Brushes.Black,width,StartRow);
			}
			else
			{
				e.Graphics.DrawString(ReportSendMedStart.m_strAge,LFont,Brushes.Black,width,StartRow);
			}
			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strAge,LFont);
			width+=szPerStr.Width+100;
			e.Graphics.DrawString("�Ա�",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("�Ա�",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strsex,LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strsex,LFont);
			width+=szPerStr.Width+130;
			e.Graphics.DrawString("����ҽ��:",LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString("����ҽ��:",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strDoctorName,LFont,Brushes.Black,width,StartRow);
			StartRow+=RowHight1;
			width=fwidth;
			e.Graphics.DrawString("���￨�ţ�",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("���￨�ţ�",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strPatCardID,LFont,Brushes.Black,width,StartRow);

			szPerStr=e.Graphics.MeasureString(ReportSendMedStart.m_strPatCardID,LFont);
			width+=szPerStr.Width+110;
			e.Graphics.DrawString("����ʱ�䣺",LFont,Brushes.Black,width,StartRow);
			szPerStr=e.Graphics.MeasureString("����ʱ�䣺",LFont);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strRegisterdate,LFont,Brushes.Black,width,StartRow);
			width+=150;
			e.Graphics.DrawString("��ӡʱ�䣺",LFont,Brushes.Black,width,StartRow);
			width+=szPerStr.Width;
			e.Graphics.DrawString(ReportSendMedStart.m_strPintdate,LFont,Brushes.Black,width,StartRow);

			StartRow+=15;
			e.Graphics.DrawLine(blackPen, x1, StartRow, x2, StartRow);
			StartRow+=15;
			width=fwidth;
			e.Graphics.DrawString("ҩƷ����",TextFont,Brushes.Black,width,StartRow);
			e.Graphics.DrawString("���",TextFont,Brushes.Black,width+300,StartRow);
			e.Graphics.DrawString("����",TextFont,Brushes.Black,width+520,StartRow);
			e.Graphics.DrawString("�÷�",TextFont,Brushes.Black,width+580,StartRow);
			e.Graphics.DrawString("����",TextFont,Brushes.Black,width+700,StartRow);

		}
		#endregion

		#region ��ʼ��ӡ�¼�
		int currRow=0;
		int endRow=14;
		int pageint=1;
		int totailPage=0;

		int currRowCH=0;
		int endRowCH=26;
		int pageintCH=1;
		int totailPageCH=0;
		int endRowENCH=14;
		int currRowENCH=0;
		int pageChang=0;
		int t1=0;
		int PageCH=0;
		int k2=0;
//		int currRow=0;
		public  void m_lngPrint(System.Drawing.Printing.PrintPageEventArgs e,clsReportSendMedStart_VO ReportSendMedStart,clsReportSendMed_VO[] sendMedEN,clsReportSendMed_VO[] sendMedCH)
		{
			if(sendMedCH!=null&&sendMedCH.Length!=0&&currPage==2)
			{
				clsReportSendMed_VO[] sendMedCHAndEN=new clsReportSendMed_VO[sendMedCH.Length];//�����г�ҩ����
				clsReportSendMed_VO[] sendMedCH1=new clsReportSendMed_VO[sendMedCH.Length];//������ҩ����
				int intCount=0;//�����г�ҩ������
				int intCount1=0;//������ҩ������
				for(int i1=0;i1<sendMedCH.Length;i1++)
				{
					if(sendMedCH[i1].m_strMedType=="2")
					{
						sendMedCHAndEN[intCount]=new clsReportSendMed_VO();
						sendMedCHAndEN[intCount].m_strdosage=sendMedCH[i1].m_strdosage;
						sendMedCHAndEN[intCount].m_strdosageUnit=sendMedCH[i1].m_strdosageUnit;
						sendMedCHAndEN[intCount].m_strMedName=sendMedCH[i1].m_strMedName;
						sendMedCHAndEN[intCount].m_strMedSpace=sendMedCH[i1].m_strMedSpace;
						sendMedCHAndEN[intCount].m_strMedType=sendMedCH[i1].m_strMedType;
						sendMedCHAndEN[intCount].m_strMedUnit=sendMedCH[i1].m_strMedUnit;
						sendMedCHAndEN[intCount].m_strTotal=sendMedCH[i1].m_strTotal;
						sendMedCHAndEN[intCount].m_strUseName=sendMedCH[i1].m_strUseName;
						intCount++;
					}
					else
					{
						sendMedCH1[intCount1]=new clsReportSendMed_VO();
						sendMedCH1[intCount1].m_strdosage=sendMedCH[i1].m_strdosage;
						sendMedCH1[intCount1].m_strdosageUnit=sendMedCH[i1].m_strdosageUnit;
						sendMedCH1[intCount1].m_strMedName=sendMedCH[i1].m_strMedName;
						sendMedCH1[intCount1].m_strMedSpace=sendMedCH[i1].m_strMedSpace;
						sendMedCH1[intCount1].m_strMedType=sendMedCH[i1].m_strMedType;
						sendMedCH1[intCount1].m_strMedUnit=sendMedCH[i1].m_strMedUnit;
						sendMedCH1[intCount1].m_strTotal=sendMedCH[i1].m_strTotal;
						sendMedCH1[intCount1].m_strUseName=sendMedCH[i1].m_strUseName;
						intCount1++;
					}
				}
				for(int i1=0;i1<intCount;i1++)
				{
					sendMedCH1[intCount1+i1]=new clsReportSendMed_VO();
					sendMedCH1[intCount1+i1].m_strdosage=sendMedCHAndEN[i1].m_strdosage;
					sendMedCH1[intCount1+i1].m_strdosageUnit=sendMedCHAndEN[i1].m_strdosageUnit;
					sendMedCH1[intCount1+i1].m_strMedName=sendMedCHAndEN[i1].m_strMedName;
					sendMedCH1[intCount1+i1].m_strMedSpace=sendMedCHAndEN[i1].m_strMedSpace;
					sendMedCH1[intCount1+i1].m_strMedType=sendMedCHAndEN[i1].m_strMedType;
					sendMedCH1[intCount1+i1].m_strMedUnit=sendMedCHAndEN[i1].m_strMedUnit;
					sendMedCH1[intCount1+i1].m_strTotal=sendMedCHAndEN[i1].m_strTotal;
					sendMedCH1[intCount1+i1].m_strUseName=sendMedCHAndEN[i1].m_strUseName;

				}
//				m_mthPrintTitleCHPage(e,ReportSendMedStart,pageint.ToString()+"/"+totailPageCH.ToString());
//				width=fwidth;
//				for(int f2=currRow;f2<sendMedCH1.Length;f2++)
//				{
//					if(sendMedCH1[f2].m_strMedType=="3")
//					{
//						if(f2+1<sendMedCH1.Length&&sendMedCH1[f2+1].m_strMedType=="3")
//						{
//							if(StartRow+RowHight<=e.PageBounds.Height)
//							{
//								m_printNew(e,sendMedCH1[f2],sendMedCH1[f2+1],ReportSendMedStart,width,StartRow);
//								StartRow+=RowHight;
//								f2++;
//							}
//							else
//							{
//								pageint++;
//								currRow=f2;
//								e.HasMorePages=true;
//								return;
//							}
//						}
//
//					}
//				}
//				m_printEend(e,ReportSendMedStart);

						
				if(intCount*2+intCount1<26)
				{
					if(intCount==0)//ֻ����ҩ
					{
						m_mthPrintTitleCH(e,ReportSendMedStart);
						m_printMedCH(e,sendMedCH,ReportSendMedStart,0,sendMedCH.Length,"3");
						currPage=1;
						m_printEend(e,ReportSendMedStart);
					}
					else if(intCount1==0)//ֻ���г�ҩ
					{
						m_mthPrintTitleCH(e,ReportSendMedStart);
						currPage=1;
						m_printMedCH(e,sendMedCHAndEN,ReportSendMedStart,0,intCount+1,"2");
						m_printEend(e,ReportSendMedStart);
					}
					else//���г�ҩ����ҩ
					{
						m_mthPrintTitleCH(e,ReportSendMedStart);
						m_printMedCH(e,sendMedCH1,ReportSendMedStart,0,intCount1,"3");
						m_printMedCH(e,sendMedCHAndEN,ReportSendMedStart,0,intCount,"2");
						currPage=1;
						m_printEend(e,ReportSendMedStart);
					}
				}
				else
				{
					
					double kk=sendMedCH.Length/26;
					totailPageCH=int.Parse(kk.ToString());
					if(sendMedCH.Length%26!=0)
					{
						totailPageCH++;
					}
					for(int k1=pageintCH;k1<=totailPageCH;k1++)
					{
						if(intCount1!=0&&intCount==0)
						{
							m_mthPrintTitleCHPage(e,ReportSendMedStart,k1.ToString()+"/"+totailPageCH.ToString());
							m_printMedCH(e,sendMedCH1,ReportSendMedStart,currRowCH,endRowCH,"3");
							m_printEend(e,ReportSendMedStart);
							currRowCH=26*pageint;
							if(currRowCH+26>sendMedCH.Length)
							{
								endRowCH=sendMedCH.Length;
							}
							else
							{
								endRowCH=currRowCH+26;
							}
							pageintCH++;
							if(pageintCH<=totailPageCH)
							{
								e.HasMorePages=true;
								break;
							}
							currPage=1;
						}
						if(intCount1==0&&intCount!=0)
						{
							m_mthPrintTitleCHPage(e,ReportSendMedStart,k1.ToString()+"/"+totailPageCH.ToString());
							m_printMedCH(e,sendMedCHAndEN,ReportSendMedStart,currRowCH,endRow,"2");
							m_printEend(e,ReportSendMedStart);
							currRowCH=14*pageint;
							if(currRowCH+14>intCount)
							{
								endRow=intCount;
							}
							else
							{
								endRow=currRowCH+14;
							}
							pageintCH++;
							if(pageintCH<=totailPageCH)
							{
								e.HasMorePages=true;
								break;
							}
							currPage=1;
						}
						if(intCount1!=0&&intCount!=0)
						{
							double kk1=(intCount1/2+intCount)/14;
							totailPageCH=int.Parse(kk1.ToString());
							if((intCount1/2+intCount)%14!=0)
							{
								totailPageCH++;
							}

							if(t1!=2)
							{
								if(intCount1<=endRowCH)
									endRowCH=intCount1;
								PageCH=intCount1/26;
								if(intCount1%26!=0)
									PageCH++;
								m_mthPrintTitleCHPage(e,ReportSendMedStart,k1.ToString()+"/"+totailPageCH.ToString());
								m_printMedCH(e,sendMedCH1,ReportSendMedStart,currRowCH,endRowCH,"3");
								m_printEend(e,ReportSendMedStart);
								currRowCH=26*pageint;
								if(currRowCH+26>intCount1)
								{
									t1++;
									endRowCH=intCount1-1;
									endRowENCH=12-intCount1/32;
									pageint++;
								}
								else
								{
									endRowCH=currRowCH+26;
								}
								if(intCount1>26)
								        pageintCH++;
								if(pageintCH<=PageCH||endRowENCH==0)
								{
									if(intCount1<26)
										pageintCH++;
									e.HasMorePages=true;
									break;
								}
								
								
							}
							if(t1==2)
							{
								if(pageChang>0||endRowENCH==0)
								   m_mthPrintTitleCHPage(e,ReportSendMedStart,k1.ToString()+"/"+totailPageCH.ToString());
								
								m_printMedCH(e,sendMedCHAndEN,ReportSendMedStart,currRowENCH,endRowENCH,"2");
								m_printEend(e,ReportSendMedStart);
								currRowENCH=endRowENCH;

								if(currRowENCH+14>intCount)
								{
									endRowENCH=intCount;
								}
								else
								{
									endRowENCH=currRowENCH+14;
								}
								pageintCH++;
								if(pageintCH<=totailPageCH||k2==0)
								{
//									pageChang++;
									e.HasMorePages=true;
									break;
								}
							}
							currPage=1;
						}
					}
				}
			}
			if(sendMedEN!=null&&sendMedEN.Length!=0&&currPage>=1||sendMedCH.Length==0)
			{
				if(currPage==1)
				{
					currPage++;
					e.HasMorePages=true;
				}
				else
				{
					if(sendMedEN.Length<14)
					{
						m_mthPrintTitleEN(e,ReportSendMedStart);
						m_printMedEN(e,sendMedEN,0,sendMedEN.Length);
						m_printEend(e,ReportSendMedStart);
					}
					else
					{
						double kk=sendMedEN.Length/14;
						totailPage=int.Parse(kk.ToString());
						if(sendMedEN.Length%14!=0)
						{
							totailPage++;
						}
						for(int k1=pageint;k1<=totailPage;k1++)
						{
							m_mthPrintTitleENPage(e,ReportSendMedStart,k1.ToString()+"/"+totailPage.ToString());
							m_printMedEN(e,sendMedEN,currRow,endRow);
							m_printEend(e,ReportSendMedStart);
							currRow=14*pageint;
							if(currRow+14>sendMedEN.Length)
							{
								endRow=sendMedEN.Length;
							}
							else
							{
								endRow=currRow+14;
							}
							pageint++;
							if(pageint<=totailPage)
							{
								e.HasMorePages=true;
								break;
							}
						}
					}
				}

			}
			
		}
		#endregion

		#region �µĴ�ӡ����
		int currRow1=0;

		#region ����ҩ����
		private void m_printNew(System.Drawing.Printing.PrintPageEventArgs e,clsReportSendMed_VO sendMedCH,clsReportSendMed_VO sendMedCHNext,clsReportSendMedStart_VO ReportSendMedStart,float width,float StartRow)
		{

						e.Graphics.DrawString(sendMedCH.m_strMedName,TextFont,Brushes.Black,width,StartRow);
						e.Graphics.DrawString(sendMedCH.m_strdosage+sendMedCH.m_strdosageUnit,TextFont,Brushes.Black,width+150,StartRow);
						e.Graphics.DrawString(sendMedCH.m_strUseName,TextFont,Brushes.Black,width+250,StartRow);
						if(sendMedCHNext!=null)
						{
							float tempWith=width+400;
							e.Graphics.DrawString(sendMedCHNext.m_strMedName,TextFont,Brushes.Black,tempWith,StartRow);
							e.Graphics.DrawString(sendMedCHNext.m_strdosage+sendMedCHNext.m_strdosageUnit,TextFont,Brushes.Black,tempWith+150,StartRow);
							e.Graphics.DrawString(sendMedCHNext.m_strUseName,TextFont,Brushes.Black,tempWith+250,StartRow);
						}
		}
     
        #endregion

		#region ����ҩ��ͷ
		private void m_printENCol(System.Drawing.Printing.PrintPageEventArgs e,clsReportSendMed_VO sendMedCH,float width,float StartRow)
		{
			StartRow+=15;
			width=fwidth;
			e.Graphics.DrawString("ҩƷ����",TextFont,Brushes.Black,width,StartRow);
			e.Graphics.DrawString("���",TextFont,Brushes.Black,width+300,StartRow);
			e.Graphics.DrawString("����",TextFont,Brushes.Black,width+520,StartRow);
			e.Graphics.DrawString("�÷�",TextFont,Brushes.Black,width+580,StartRow);
			e.Graphics.DrawString("����",TextFont,Brushes.Black,width+700,StartRow);

		}

		#endregion

		#region ����ҩ����
		private void m_printEN(System.Drawing.Printing.PrintPageEventArgs e,clsReportSendMed_VO sendMedCH,float width,float StartRow)
		{
						if(e.Graphics.MeasureString(sendMedCH.m_strMedName,TextFont).Width>307)
						{
							StartRow+=10;
							string str1=sendMedCH.m_strMedName.Substring(0,15);
							string str2=sendMedCH.m_strMedName.Substring(15);
							e.Graphics.DrawString(str1,TextFont,Brushes.Black,width,StartRow-13);
							e.Graphics.DrawString(str2,TextFont,Brushes.Black,width,StartRow+2);
						}
						else
						{
							e.Graphics.DrawString(sendMedCH.m_strMedName,TextFont,Brushes.Black,width,StartRow);
						}
						e.Graphics.DrawString(sendMedCH.m_strMedSpace,TextFont,Brushes.Black,width+300,StartRow);
						if(sendMedCH.m_strdosage.Trim()!="")
							e.Graphics.DrawString(sendMedCH.m_strdosage.Trim()+sendMedCH.m_strdosageUnit,TextFont,Brushes.Black,width+520,StartRow);
						e.Graphics.DrawString(sendMedCH.m_strUseName.Trim(),TextFont,Brushes.Black,width+580,StartRow);
						e.Graphics.DrawString(sendMedCH.m_strTotal+sendMedCH.m_strMedUnit,TextFont,Brushes.Black,width+700,StartRow);
//HasMorePages=true;

		}

		#endregion

		#endregion

	}
}
