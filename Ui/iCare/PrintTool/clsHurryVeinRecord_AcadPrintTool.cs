
using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Windows.Forms;
namespace iCare
{
	/// <summary>
	///�߲�������ι۲���ӡ������ 
	/// </summary>
	public class clsHurryVeinRecord_AcadPrintTool: infPrintRecord
	{
		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;		
		private clsPrintInfo_InPatientCaseHistory m_objPrintInfo;
		private clsBaseCaseHistoryDomain m_objRecordsDomain;


		private clsHurryVeinRecord[] m_objResultArr = null;
		private	clsPatient m_objPatient = null;
		DateTime m_dtInHos ;

		#region �в�,���ܵȴ�ֵ�ȱ��� ��
		public string	p_strlaycount_chr = "";
		public string	p_strPregnantweek_chr = "";
		public string	p_strScorecount_chr = "";
		public string	p_strRdbneckexpand_chr = "";
		public string	p_strRdbneckshink_chr = "";
		public string	p_strRdbhighlow_chr = "";
		public string	p_strRdbneckhard_chr = "";
		public string	p_strDroppingcase_chr = "";
		public string	p_strIndicate_chr = "";
		public string	p_strUsecount_chr = "";
		public string	p_strLayway_chr = "";
		public string	p_strRdbnecklocation_chr = "";
		#endregion 

		public clsHurryVeinRecord_AcadPrintTool()
		{
            m_strTitle = "�߲��ؾ�����ι۲��";
		
		}

		///<summary>
		///���������ƴ�ӡ��¼�ˣ�ֻ�ܴ�ӡһ�Ρ�
		///</summary>
		bool  m_blnOnlyPrintOnceHadPrintedPerson ; 
		///<summary>
		///���������ƴ�ӡ��ע��ֻ�ܴ�ӡһ�Ρ�
		///</summary>
		bool  m_blnOnlyPrintOnceHadPrinted ; 

		///<summary>
		///�����������¼�����¼�
		///</summary>
		int m_intRecordIndex =0; 

		#region ���ô�ӡ�п���ÿ�еĺ��������

		///<summary>
		///��������1�п��
		///</summary>
		float m_fltFirstCol ; //��1�п��
		///<summary>
		///��������2�п��
		///</summary>
		float m_fltSeconCol ; //��2�п��
		///<summary>
		///��������3�п��
		///</summary>
		float m_fltthCol ; //��3�п��
		///<summary>
		///��������4�п��
		///</summary>
		float m_fltthirCol ; //��4�п��
		///<summary>
		///��������5�п��
		///</summary>
		float m_fltFiveCol ; //��5�п��
		///<summary>
		///��������6�п��
		///</summary>
		float m_fltSixCol ; //��6�п��
		///<summary>
		///��������7�п��
		///</summary>
		float m_fltSenCol; //��7�п��
		///<summary>
		///��������8�п��
		///</summary>
		float m_fltNigCol; //��8�п��
		///<summary>
		///��������9�п��
		///</summary>
		float m_fltNiNeCol ; 
		///<summary>
		///��������10�п��
		///</summary>
		float m_fltCol10 ; 
		///<summary>
		///��������11�п��
		///</summary>
		float m_fltCol11 ;
		///<summary>
		///��������12�п��
		///</summary>
		float m_fltCol12 ;
		///<summary>
		///��������13�п��
		///</summary>
		float m_fltCol13 ;
		///<summary>
		///��������14�п��
		///</summary>
		float m_fltCol14 ; 
		///<summary>
		///��������15�п��
		///</summary>
		float m_fltCol15 ; 

		///<summary>
		///��������1��Left����
		///</summary>
		float m_fltFirstColLeft  ; //��1��Left����
		///<summary>
		///��������2��Left����
		///</summary>
		float m_fltSeconColLeft; //��2��Left����
		///<summary>
		///��������3��Left����
		///</summary>
		float m_fltthColLeft ; //��3��Left����
		///<summary>
		///��������4��Left����
		///</summary>
		float m_fltthirColLeft; //��4��Left����
		///<summary>
		///��������5��Left����
		///</summary>
		float m_fltFiveColLeft ; //��5��Left����
		///<summary>
		///��������6��Left����
		///</summary>
		float m_fltSixColLeft ; //��6��Left����
		///<summary>
		///��������7��Left����
		///</summary>
		float m_fltSenColLeft ; //��7��Left����
		///<summary>
		///��������8��Left����
		///</summary>
		float m_fltNigColLeft ; //��8��Left����
		///<summary>
		///��������9��Left����
		///</summary>
		float m_fltNiNeColLeft ; //��9��Left����
		///<summary>
		///��������10��Left����
		///</summary>
		float m_fltColLeft10 ; 
		///<summary>
		///��������11��Left����
		///</summary>
		float m_fltColLeft11 ; 
		///<summary>
		///��������12��Left����
		///</summary>
		float m_fltColLeft12 ; 
		///<summary>
		///��������13��Left����
		///</summary>
		float m_fltColLeft13 ; 
		///<summary>
		///��������14��Left����
		///</summary>
		float m_fltColLeft14 ; 

		///<summary>
		///��������14��rigth����,��15�е�������
		///</summary>
		float m_fltColLeft15 ; 
		#endregion

		#region ��ӡ���ñ���
		/// <summary>
		/// ��ӡ���������
		/// </summary>	
		private System.Drawing.Font m_fontTitle = new System.Drawing.Font("����",18,FontStyle.Bold);
		/// <summary>
		/// ��ӡ�ı���Ŀ
		/// </summary>	
		public  string m_strTitle;
		/// <summary>
		/// Pen����
		/// </summary>
		private Pen m_objPen = new Pen(Color.Black);
		/// <summary>
		/// brush
		/// </summary>	
		private System.Drawing.Brush m_objBrush = System.Drawing.Brushes.Black;
		/// <summary>
		/// ��ӡ���ĵ�����
		/// </summary>	
		private System.Drawing.Font m_fontBody = new System.Drawing.Font("����",10);
		/// <summary>
		/// ��¼��ӡ�ĸ߶����λ
		/// </summary>	
		public  float m_fltLocationY =0;
		///<summary>
		///�����������߼��λ�ø� �����
		///</summary>
		private float m_fltZijiHeight = 10; //�����߼��λ�ø� �����
		///<summary>
		///��������ӡ�ĵ�ǰҳ��
		///</summary>
		private int m_intCurrentPageIndex = 1;
		///<summary>
		///��������������ĸ߶�
		///</summary>
		private SizeF m_objsize ;
		///<summary>
		///�������ָ�
		///</summary>
		private float m_fltZiHeight;
		///<summary>
		///�������ֿ�
		///</summary>
		private float m_fltZiWidth;
		///<summary>
		///�����������������룺���
		///</summary>
		private float m_fltZiJiWide = 0 ;// �����������룺���
		///<summary>
		///�������и�
		///</summary>
		private float m_ftlRowHeight;
		///<summary>
		///�������п�
		///</summary>
		private float m_fltAvgCol;

		#endregion 

		#region ����:��ʼ��ÿһ�е�λ��
		/// <summary>
		/// ����:��ʼ��ÿһ�е�λ��
		/// </summary>		
		private void mthInitColLocation(PrintPageEventArgs e)
		{
			#region ���ô�ӡ�п���ÿ�еĺ�����

			//			double kk=(e.PageBounds.Width - 30)/15;
			double kk=(Convert.ToDouble(e.PageBounds.Width - 50))/11.00;
			m_fltAvgCol  = float.Parse(kk.ToString("0.00"));

			float fltCol =m_fltAvgCol;
			m_fltFirstCol = fltCol; //��1�п��

			m_fltSeconCol = fltCol; //��2�п��

			m_fltthCol = fltCol; //��3�п��

			m_fltthirCol = fltCol; //��4�п��

			m_fltFiveCol = fltCol; //��5�п��

			m_fltSixCol = fltCol; //��6�п��

			m_fltSenCol = fltCol; //��7�п��

			m_fltNigCol = fltCol; //��8�п��

			m_fltNiNeCol = fltCol; //��9�п��

			this.m_fltCol10 = fltCol;
			this.m_fltCol11 = fltCol;
			this.m_fltCol12 = fltCol;
			this.m_fltCol13 = fltCol;
			this.m_fltCol14 = fltCol;
			this.m_fltCol15 = fltCol;


			m_fltFirstColLeft = e.PageBounds.Left + 5 ; //��1��Left����
			//			m_fltFirstColLeft = e.MarginBounds.Left - 90  ; //��1��Left����
			m_fltSeconColLeft = m_fltFirstCol + m_fltFirstColLeft; //��2��Left����
			m_fltthColLeft = m_fltSeconColLeft + m_fltSeconCol; //��3��Left����
			m_fltthirColLeft = m_fltthColLeft + m_fltthCol; //��4��Left����
			m_fltFiveColLeft = m_fltthirColLeft + m_fltthirCol; //��5��Left����
			m_fltSixColLeft = m_fltFiveColLeft + m_fltFiveCol; //��6��Left����
			m_fltSenColLeft = m_fltSixColLeft + m_fltSixCol; //��7��Left����
			m_fltNigColLeft = m_fltSenColLeft + m_fltSenCol; //��8��Left����
			m_fltNiNeColLeft = m_fltNigColLeft + m_fltNiNeCol; //��9��Left����
			this.m_fltColLeft10 = m_fltNiNeColLeft + m_fltNiNeCol;
			this.m_fltColLeft11 = m_fltColLeft10 + m_fltCol10;
			this.m_fltColLeft12 = m_fltColLeft11 + m_fltCol11;
			this.m_fltColLeft13 = m_fltColLeft12 + m_fltCol12;
			this.m_fltColLeft14 = m_fltColLeft13 + m_fltCol13;
			this.m_fltColLeft15 = m_fltColLeft14 + m_fltCol14 ;

			#endregion

			m_objsize = e.Graphics.MeasureString("����",this.m_fontBody);
			m_fltZiHeight = m_objsize.Height ;// �ָ�
			m_ftlRowHeight =  m_fltZijiHeight + m_fltZiHeight;//�и�

		}
		#endregion

		#region ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.
		/// <summary>
		/// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate)
		{			
			m_objPatient = p_objPatient;
			m_dtInHos = p_dtmInPatientDate;
            //�������л�ȡ����ûɾ��������
            //com.digitalwave.clsRecordsService.clsHurryVeinRecord_ContentService m_objInRoomSvc =
            //    (com.digitalwave.clsRecordsService.clsHurryVeinRecord_ContentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsHurryVeinRecord_ContentService));

            (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetAllMainRecord(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out m_objResultArr);

            (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetOther(
				p_objPatient.m_StrInPatientID, 
				p_dtmInPatientDate.ToString(),
				out p_strlaycount_chr, 
				out p_strPregnantweek_chr, 
				out p_strScorecount_chr,
				out p_strRdbneckexpand_chr, 
				out p_strRdbneckshink_chr,
				out p_strRdbhighlow_chr,
				out p_strRdbneckhard_chr,
				out p_strDroppingcase_chr,
				out p_strIndicate_chr,
				out p_strUsecount_chr,
				out p_strLayway_chr,
				out p_strRdbnecklocation_chr
				);
		}

		#endregion 

		#region �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
		/// </summary>
		public void m_mthInitPrintContent()
		{	
		}
		#endregion 

		#region ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
		}
		#endregion

		#region ��ȡ��ӡ����,(�������ݿ��ȡʱ,���ñ�����֮ǰ�����ȵ���m_mthSetPrintInfo����)
		/// <summary>
		/// ��ȡ��ӡ����,(�������ݿ��ȡʱ,���ñ�����֮ǰ�����ȵ���m_mthSetPrintInfo����)
		/// </summary>
		/// <returns>��ӡ����</returns>
		public object m_objGetPrintInfo()
		{	
			return null;
		}

		#endregion 

		#region ��ʼ����ӡ����,��������ն��󼴿�.

		/// <summary>
		/// ��ʼ����ӡ����,��������ն��󼴿�.
		/// </summary>
		public void m_mthInitPrintTool(object p_objArg)
		{				

		}

		#endregion

		#region �ͷŴ�ӡ����
		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		public void m_mthDisposePrintTools(object p_objArg)
		{

		}
		#endregion
		
		#region ��ӡ

		#region  ��ӡ��ʼ
		/// <summary>
		/// ��ӡ��ʼ
		/// </summary>
		/// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
		public void m_mthBeginPrint(object p_objPrintArg)
		{	
			reset();
			//			m_mthBeginPrintSub((PrintEventArgs)p_objPrintArg);
		}
		#endregion

		#region ��ӡ��
		/// <summary>
		/// ��ӡ��
		/// </summary>
		/// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintPageEventArgs���͵Ķ���</param>
		public void m_mthPrintPage(object p_objPrintArg)
		{
			
			PrintPageEventArgs e =  (PrintPageEventArgs)p_objPrintArg;

			
			m_mthPrintTitleInfo(e);
	
			mthInitColLocation(e);	
			if(m_intCurrentPageIndex == 1)
			{
				m_mthPrintFormTitleInfo(e,this.m_objPatient,ref this.m_fltLocationY);
			
				m_mthPrintPingFenTable(e,ref this.m_fltLocationY);
			}
			
			if(m_intCurrentPageIndex == 1)
			{
				m_mthPrintFormHeader(e,ref this.m_fltLocationY);
			}
			m_mthPrintAllPage(e,ref this.m_fltLocationY);
		}

		#endregion 

		#region ��ӡÿҳ
		private void reset()
		{
			m_intRecordIndex = 0;
			m_blnOnlyPrintOnceHadPrintedPerson= false;
			m_blnOnlyPrintOnceHadPrinted = false;
			m_intCurrentPageIndex = 1;
			this.m_fltLocationY = 0;

		}
		private int i =0;
		//		private int intPageSize =3;

		private void m_mthPrintAllPage(System.Drawing.Printing.PrintPageEventArgs e,ref float p_objLocationY)
		{
			string print = "";
//			if(m_objResultArr.Length <=0)
//				return ;

			//�����һҳ���ܴ�ӡ����������¼
			int intRowCount = Convert.ToInt32((float.Parse(e.MarginBounds.Height.ToString()) - p_objLocationY)/m_ftlRowHeight);
			intRowCount--; //��Ϊ����һ��λ������ӡҳ��,��ӡ ��ע ��������ʾ������Ϊ��1
			intRowCount--; //��Ϊ����һ��λ������ӡ"�����¼"

			if(m_intCurrentPageIndex == 1)
			{
				
				for(  i = 0; i< m_objResultArr.Length && i < intRowCount;i++)
				{
		
					#region draw one row
					print = m_objResultArr[i].m_dtmCreateDate.Date.ToString("yy/mm/dd");
					m_mthDrawStrAtRectangle(this.m_fltFirstColLeft,this.m_fltSeconColLeft, print,p_objLocationY,e);
					
					print = m_objResultArr[i].m_dtmCreateDate.ToShortTimeString();
					m_mthDrawStrAtRectangle(this.m_fltSeconColLeft,this.m_fltthColLeft, print ,p_objLocationY,e);
					
					print = m_objResultArr[i].m_strCHROMA_CHR;
					m_mthDrawStrAtRectangle(this.m_fltthColLeft,this.m_fltthirColLeft, print,p_objLocationY,e);
					
					print = m_objResultArr[i].m_strDROPCOUNT_CHR;
					m_mthDrawStrAtRectangle(this.m_fltthirColLeft,this.m_fltFiveColLeft, print,p_objLocationY,e);
					
					print = m_objResultArr[i].m_strPALACESHRINK_CHR;
					m_mthDrawStrAtRectangle(this.m_fltFiveColLeft,this.m_fltSixColLeft, print,p_objLocationY,e);
									
					print = m_objResultArr[i].m_strEMBRYOHEART_CHR;
					m_mthDrawStrAtRectangle(this.m_fltSixColLeft,this.m_fltSenColLeft, print,p_objLocationY  ,e);
					
					print = m_objResultArr[i].m_strEXPAND_CHR;
					m_mthDrawStrAtRectangle(this.m_fltSenColLeft,this.m_fltNigColLeft, print,p_objLocationY ,e);
									
					print = m_objResultArr[i].m_strPRESENTATION_CHR;
					m_mthDrawStrAtRectangle(this.m_fltNigColLeft,this.m_fltNiNeColLeft, print,p_objLocationY ,e);
					
					print = m_objResultArr[i].m_strBLOODPRESSURE_CHR;
					m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft,this.m_fltColLeft10, print,p_objLocationY,e);
					
					print = m_objResultArr[i].m_strSPECIALRECORD_CHR;				
					m_mthDrawStrAtRectangle(this.m_fltColLeft10,this.m_fltColLeft11, print,p_objLocationY,e);
					
					print = m_objResultArr[i].m_strSIGNATURE_CHR;
					m_mthDrawStrAtRectangle(this.m_fltColLeft11,this.m_fltColLeft12, print,p_objLocationY,e);
												
										
					m_mthDrawLines(e);
					
					p_objLocationY += this.m_ftlRowHeight;
					#endregion 
								
				}
				m_mthPrintFoot(e);
				//�ж��Ƿ��ҳ
				if( i < this.m_objResultArr.Length-1)
				{
					m_intCurrentPageIndex ++;
					e.HasMorePages = true;	
					return;
				}			
			}
			else
			{
				int temp = i;

				#region draw one row

				for(  ; i< m_objResultArr.Length && i < intRowCount + temp;i++)
				{
					#region draw one row 
					print = m_objResultArr[i].m_dtmCreateDate.Date.ToString("yy/mm/dd");
					m_mthDrawStrAtRectangle(this.m_fltFirstColLeft,this.m_fltSeconColLeft, print,p_objLocationY,e);
					
					print = m_objResultArr[i].m_dtmCreateDate.ToShortTimeString();
					m_mthDrawStrAtRectangle(this.m_fltSeconColLeft,this.m_fltthColLeft, print ,p_objLocationY,e);
					
					print = m_objResultArr[i].m_strCHROMA_CHR;
					m_mthDrawStrAtRectangle(this.m_fltthColLeft,this.m_fltthirColLeft, print,p_objLocationY,e);
					
					print = m_objResultArr[i].m_strDROPCOUNT_CHR;
					m_mthDrawStrAtRectangle(this.m_fltthirColLeft,this.m_fltFiveColLeft, print,p_objLocationY,e);
					
					print = m_objResultArr[i].m_strPALACESHRINK_CHR;
					m_mthDrawStrAtRectangle(this.m_fltFiveColLeft,this.m_fltSixColLeft, print,p_objLocationY,e);
									
					print = m_objResultArr[i].m_strEMBRYOHEART_CHR;
					m_mthDrawStrAtRectangle(this.m_fltSixColLeft,this.m_fltSenColLeft, print,p_objLocationY  ,e);
					
					print = m_objResultArr[i].m_strEXPAND_CHR;
					m_mthDrawStrAtRectangle(this.m_fltSenColLeft,this.m_fltNigColLeft, print,p_objLocationY ,e);
									
					print = m_objResultArr[i].m_strPRESENTATION_CHR;
					m_mthDrawStrAtRectangle(this.m_fltNigColLeft,this.m_fltNiNeColLeft, print,p_objLocationY ,e);
					
					print = m_objResultArr[i].m_strBLOODPRESSURE_CHR;
					m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft,this.m_fltColLeft10, print,p_objLocationY,e);
					
					print = m_objResultArr[i].m_strSPECIALRECORD_CHR;				
					m_mthDrawStrAtRectangle(this.m_fltColLeft10,this.m_fltColLeft11, print,p_objLocationY,e);
					
					print = m_objResultArr[i].m_strSIGNATURE_CHR;
					m_mthDrawStrAtRectangle(this.m_fltColLeft11,this.m_fltColLeft12, print,p_objLocationY,e);
												
					
					#endregion
					
					m_mthDrawLines(e);
					
					p_objLocationY += this.m_ftlRowHeight;
								
				}

				#endregion 

				m_mthPrintFoot(e);

				//�ж��Ƿ��ҳ

				if( intRowCount < m_objResultArr.Length-i-1)
				{
					m_intCurrentPageIndex ++;
					e.HasMorePages = true;
					return;
				}
					
			}
			#region ���ձ��
			if(m_objResultArr.Length ==0)
			{
				for(int j=0;j<intRowCount + 4;j++)
				{
					m_mthDrawLines(e);					
					p_objLocationY += this.m_ftlRowHeight;
				}
			}
			#endregion
			#region ��ӡ�������һ��.

			print = "�߲���ʹ������:" + p_strUsecount_chr;			
			e.Graphics.DrawString(print,this.m_fontBody,this.m_objBrush,m_fltFirstColLeft,p_objLocationY );
			print  = "���䷽ʽ:"+p_strLayway_chr;	
			SizeF objSize = e.Graphics.MeasureString(print, this.m_fontBody);
			if(p_strLayway_chr == "")
			{
				e.Graphics.DrawString(print, m_fontBody, m_objBrush, m_fltColLeft11 - objSize.Width  , p_objLocationY);
			}
			else
			{
				e.Graphics.DrawString(print, m_fontBody, m_objBrush, m_fltColLeft12 - objSize.Width  , p_objLocationY);
			}
			

			#endregion

		}
		#endregion 

		

		#region ����
		private void m_mthDrawLines(PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			for(int i1 = 0 ;i1 < 12; i1 ++)
			{
				g.DrawLine(this.m_objPen , this.m_fltFirstColLeft + this.m_fltAvgCol * i1,this.m_fltLocationY, this.m_fltFirstColLeft + this.m_fltAvgCol * i1,this.m_fltLocationY + this.m_ftlRowHeight);
			}
			g.DrawLine(this.m_objPen , this.m_fltFirstColLeft ,this.m_fltLocationY + this.m_ftlRowHeight, this.m_fltColLeft12,this.m_fltLocationY + this.m_ftlRowHeight);


		}
		#endregion 

		//��ӡҳ��
		private void m_mthPrintFoot(PrintPageEventArgs e)
		{
			string str = "��"+this.m_intCurrentPageIndex.ToString()+"ҳ";
			SizeF s = e.Graphics.MeasureString(str,this.m_fontBody);
			float with = float.Parse(e.PageBounds.Width.ToString()) - s.Width;

			e.Graphics.DrawString(str,this.m_fontBody,this.m_objBrush,with/2,float.Parse(e.MarginBounds.Bottom.ToString()));
		}
		/// <summary>
		/// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
		/// </summary>
		/// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
		public void m_mthEndPrint(object p_objPrintArg)
		{
			if(this.m_objPatient != null)
            {
                //com.digitalwave.clsRecordsService.clsHurryVeinRecord_ContentService m_objInRoomSvc =
                //    (com.digitalwave.clsRecordsService.clsHurryVeinRecord_ContentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsHurryVeinRecord_ContentService));

                (new weCare.Proxy.ProxyEmr05()).Service.clsHurryVeinRecord_ContentService_m_lngUpdateALLFirstPrintDate(m_objPatient.m_StrInPatientID,m_dtInHos.ToString(),System.DateTime.Now);
			}					
		}

		#endregion
		// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
		private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{
			
		}

		// ��ӡҳ
		private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
		{
			
		}

		// ���ô�ӡ���ݡ�
		private  void m_mthSetPrintContent(clsNewBabyInRoomRecord p_objContent,clsNewBabyCircsRecord[] p_objCircsContentArr, DateTime p_dtmFirstPrintDate)
		{
			
		}

		private clsNewBabyInRoomRecord m_objChangePrintTextColor(clsNewBabyInRoomRecord p_objclsInPatientCase)
		{
			if(p_objclsInPatientCase==null)
				return null;
			
			return p_objclsInPatientCase;
		}

		#region  �������ֲ���
		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{	
			System.Drawing.Graphics g = e.Graphics;
			SizeF objSize = g.MeasureString(this.m_strTitle, this.m_fontTitle);
			g.DrawString(this.m_strTitle,this.m_fontTitle, m_objBrush,e.MarginBounds.Left +( e.MarginBounds.Width - objSize.Width)/2,e.MarginBounds.Top);
			this.m_fltLocationY = e.MarginBounds.Top + objSize.Height;

		}
		#endregion

		#region  ����������ֲ���
		/// <summary>
		/// ����������ֲ���
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintFormTitleInfo(System.Drawing.Printing.PrintPageEventArgs e, clsPatient p_objPatient,ref float p_fltLocationY)
		{	
			string strPrint = "";
			//			int col = e.MarginBounds.Width/6;		
			//			float fltCol = float.Parse(col.ToString());//��ӡ�������е��п�	
			double dblCol = (m_fltColLeft12 - m_fltFirstColLeft)/6.0;//��ӡ�������е��п�
			float fltCol = float.Parse(dblCol.ToString());
			System.Drawing.Graphics g = e.Graphics;			

			strPrint  = "����:"+p_objPatient.m_StrName.Trim();
			SizeF objSize = g.MeasureString(strPrint, this.m_fontBody);
			g.DrawString(strPrint, m_fontBody, m_objBrush, m_fltFirstColLeft , p_fltLocationY);

			strPrint  = "����:"+p_objPatient.m_ObjPeopleInfo.m_IntAge.ToString();
			objSize = g.MeasureString(strPrint, this.m_fontBody);
			g.DrawString(strPrint, m_fontBody, m_objBrush, m_fltFirstColLeft + fltCol * 1  , p_fltLocationY);

			strPrint  = "��/��:"+p_strlaycount_chr;
			objSize = g.MeasureString(strPrint, this.m_fontBody);
			g.DrawString(strPrint, m_fontBody, m_objBrush, m_fltFirstColLeft + fltCol * 2  , p_fltLocationY);

			strPrint  = "����:"+p_strPregnantweek_chr;
			objSize = g.MeasureString(strPrint, this.m_fontBody);
			g.DrawString(strPrint, m_fontBody, m_objBrush, m_fltFirstColLeft + fltCol * 3  , p_fltLocationY);

			strPrint  = "����:"+p_objPatient.m_strBedCode.Trim();
			objSize = g.MeasureString(strPrint, this.m_fontBody);
			g.DrawString(strPrint, m_fontBody, m_objBrush, m_fltFirstColLeft + fltCol * 4 , p_fltLocationY);

			strPrint  = "סԺ��:"+p_objPatient.m_StrHISInPatientID.Trim();
			objSize = g.MeasureString(strPrint, this.m_fontBody);
			g.DrawString(strPrint, m_fontBody, m_objBrush, m_fltFirstColLeft + fltCol * 5 , p_fltLocationY);

			this.m_fltLocationY = p_fltLocationY + objSize.Height;
       
		}
		#endregion

		#region ��ӡ���ֱ�
		private void m_mthPrintPingFenTable(System.Drawing.Printing.PrintPageEventArgs e, ref float p_objLocationY)
		{
			
			System.Drawing.Graphics g = e.Graphics;	
			string strPrint  = "BiShop�������������:";			
			g.DrawString(strPrint,this.m_fontBody,this.m_objBrush,m_fltFirstColLeft,p_objLocationY );
			
			
				strPrint  = "�ۻ����ֹ���:  "+p_strScorecount_chr+"  ��";	
			
			
			SizeF objSize = g.MeasureString(strPrint, this.m_fontBody);
			g.DrawString(strPrint, m_fontBody, m_objBrush, m_fltColLeft12 - objSize.Width  , p_objLocationY);
		
			this.m_fltLocationY = p_objLocationY + objSize.Height;

			double dblCol = (m_fltColLeft12 - m_fltFirstColLeft)/5.0;//��ӡ�������е��п�
			float fltCol = float.Parse(dblCol.ToString());
			double dblsmallCol= (m_fltColLeft12 - m_fltFirstColLeft)/10.0;//��ӡ�������е��п�
			float fltSmallCol = float.Parse(dblsmallCol.ToString());

			g.DrawLine(this.m_objPen, this.m_fltFirstColLeft   ,p_objLocationY, m_fltColLeft12 , p_objLocationY);
			for(int i1=0;i1<6;i1++)
			{
				g.DrawLine(this.m_objPen, this.m_fltFirstColLeft + fltCol* i1  ,p_objLocationY, m_fltFirstColLeft + fltCol* i1 , p_objLocationY + m_ftlRowHeight);

			}
			//��0 1 2 3 
			m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol,this.m_fltFirstColLeft + fltCol *2, "0",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol *2,this.m_fltFirstColLeft + fltCol *3, "1",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol *3,this.m_fltFirstColLeft + fltCol *4, "2",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol *4,this.m_fltFirstColLeft + fltCol *5, "3",p_objLocationY,e);

			p_objLocationY += m_ftlRowHeight;
			g.DrawLine(this.m_objPen, m_fltFirstColLeft,p_objLocationY, m_fltColLeft12 , p_objLocationY);
			//end

			//����һ���������.
			string[] strFirst =  new string[]{"������(cm)","������(%)","��¶�ߵ�(cm)","����Ӳ��","����λ��"};
			string[] strFirst2 =  new string[]{"δ��","0-30","-3","Ӳ","��λ"};
			string[] strFirst3 =  new string[]{"1-2","40-50","-2","�е�","��λ"};
			string[] strFirst4 =  new string[]{"3-4","60-70","-1��0","��","ǰλ"};
			string[] strFirst5 =  new string[]{">=5",">=80","-1��2","",""};

			float tempY = p_objLocationY;
			for(int k1=0;k1<5;k1++)
			{
				for(int k2=0;k2<11;k2++)
				{
					if( k2 != 1)
						g.DrawLine(this.m_objPen, this.m_fltFirstColLeft + fltSmallCol* k2  ,p_objLocationY, m_fltFirstColLeft + fltSmallCol* k2 , p_objLocationY + m_ftlRowHeight);

				}
				m_mthDrawStrAtRectangle(this.m_fltFirstColLeft  ,this.m_fltFirstColLeft + fltCol , strFirst[k1],p_objLocationY,e);
				m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol   ,this.m_fltFirstColLeft + fltCol + fltSmallCol    , strFirst2[k1],p_objLocationY,e);
				m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol*2 ,this.m_fltFirstColLeft + fltCol *2 + fltSmallCol  , strFirst3[k1],p_objLocationY,e);
				m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol*3 ,this.m_fltFirstColLeft + fltCol *3 + fltSmallCol  , strFirst4[k1],p_objLocationY,e);
				m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol*4 ,this.m_fltFirstColLeft + fltCol *4 + fltSmallCol  , strFirst5[k1],p_objLocationY,e);
			
				if(p_strRdbneckexpand_chr == k1.ToString())
				{
					m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol + fltSmallCol +  fltCol * k1,this.m_fltFirstColLeft + fltCol + fltSmallCol +fltSmallCol +  fltCol * k1 , "��",tempY,e);
				}
				if(p_strRdbneckshink_chr == k1.ToString())
				{
					m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol + fltSmallCol +  fltCol * k1,this.m_fltFirstColLeft + fltCol + fltSmallCol +fltSmallCol +  fltCol * k1 , "��",tempY + m_ftlRowHeight,e);
				}
				if(p_strRdbhighlow_chr == k1.ToString())
				{
					m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol + fltSmallCol +  fltCol * k1,this.m_fltFirstColLeft + fltCol + fltSmallCol +fltSmallCol +  fltCol * k1 , "��",tempY + m_ftlRowHeight*2,e);
				}
				if(p_strRdbneckhard_chr == k1.ToString())
				{
					m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol + fltSmallCol +  fltCol * k1,this.m_fltFirstColLeft + fltCol + fltSmallCol +fltSmallCol +  fltCol * k1 , "��",tempY + m_ftlRowHeight*3,e);
				}
				if(p_strRdbnecklocation_chr == k1.ToString())
				{
					m_mthDrawStrAtRectangle(this.m_fltFirstColLeft + fltCol + fltSmallCol +  fltCol * k1,this.m_fltFirstColLeft + fltCol + fltSmallCol +fltSmallCol +  fltCol * k1 , "��",tempY + m_ftlRowHeight*4,e);
				}

				
				p_objLocationY += m_ftlRowHeight;
				g.DrawLine(this.m_objPen, m_fltFirstColLeft,p_objLocationY, m_fltColLeft12 , p_objLocationY);

			}
			//end
			strPrint="���������������Ӧ���ڻ�\"��\"�ۻ���������.";
			g.DrawString(strPrint,this.m_fontBody,this.m_objBrush,m_fltFirstColLeft,p_objLocationY );
			p_objLocationY += m_ftlRowHeight;
			strPrint = "�߲��ؾ���������:"+ p_strDroppingcase_chr;
			SizeF sf = g.MeasureString(strPrint,this.m_fontBody);
			g.DrawString(strPrint,this.m_fontBody,this.m_objBrush,m_fltFirstColLeft,p_objLocationY );
			
				strPrint = "���δ߲���ָ��:"+ p_strIndicate_chr;
			sf = g.MeasureString(strPrint,this.m_fontBody);			

			if(p_strIndicate_chr == "")
			{
				g.DrawString(strPrint,this.m_fontBody,this.m_objBrush,m_fltColLeft11 - sf.Width,p_objLocationY);
			}
			else
			{
				g.DrawString(strPrint,this.m_fontBody,this.m_objBrush,m_fltColLeft12 - sf.Width,p_objLocationY);
			}
						
			p_objLocationY += m_ftlRowHeight;

						
		}
		#endregion 

		#region ���ͷ
		private void m_mthPrintFormHeader(System.Drawing.Printing.PrintPageEventArgs e, ref float p_objLocationY)
		{
			
			System.Drawing.Graphics g = e.Graphics;	
			

			g.DrawLine(this.m_objPen, m_fltFirstColLeft,p_objLocationY, m_fltColLeft12, p_objLocationY);

	 
			for(int i1=0;i1<12;i1++)
			{
				//g.DrawLine(this.m_objPen, e.MarginBounds.Left + e.MarginBounds.Width/14 * i1,p_objLocationY, e.MarginBounds.Left + e.MarginBounds.Width/14 * i1, p_objLocationY +m_ftlRowHeight);
				g.DrawLine(this.m_objPen, this.m_fltFirstColLeft + m_fltAvgCol * i1  ,p_objLocationY, m_fltFirstColLeft + m_fltAvgCol * i1 , p_objLocationY +m_ftlRowHeight * 2);

			}
			SizeF s = g.MeasureString("����",this.m_fontBody);
			float y = p_objLocationY + m_ftlRowHeight*2 /2 - s.Height/2;
			float y1 = p_objLocationY + m_ftlRowHeight /2 - s.Height/2;
			float y2 = p_objLocationY + m_ftlRowHeight + m_ftlRowHeight /2 - s.Height/2;

			m_mthDrawStrAtRectangle(this.m_fltFirstColLeft,this.m_fltSeconColLeft, "��",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltFirstColLeft,this.m_fltSeconColLeft, "��",p_objLocationY+ m_ftlRowHeight,e);
			m_mthDrawStrAtRectangle(this.m_fltSeconColLeft,this.m_fltthColLeft, "ʱ",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltSeconColLeft,this.m_fltthColLeft, "��",p_objLocationY+ m_ftlRowHeight,e);
			m_mthDrawStrAtRectangle(this.m_fltthColLeft,this.m_fltthirColLeft, "�߲���Ũ��",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltthColLeft,this.m_fltthirColLeft, "U/500ml",p_objLocationY + m_ftlRowHeight,e);
			m_mthDrawStrAtRectangle(this.m_fltthirColLeft,this.m_fltFiveColLeft, "����",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltthirColLeft,this.m_fltFiveColLeft, "(��/��)",p_objLocationY+ m_ftlRowHeight,e);
			m_mthDrawStrAtRectangle(this.m_fltFiveColLeft,this.m_fltSixColLeft, "��",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltFiveColLeft,this.m_fltSixColLeft, "��",p_objLocationY+ m_ftlRowHeight,e);
			m_mthDrawStrAtRectangle(this.m_fltSixColLeft,this.m_fltSenColLeft, "̥",p_objLocationY ,e);
			m_mthDrawStrAtRectangle(this.m_fltSixColLeft,this.m_fltSenColLeft, "��",p_objLocationY + m_ftlRowHeight,e);
			
			m_mthDrawStrAtRectangle(this.m_fltSenColLeft,this.m_fltNigColLeft, "����",p_objLocationY ,e);
			m_mthDrawStrAtRectangle(this.m_fltSenColLeft,this.m_fltNigColLeft, "����",p_objLocationY  + m_ftlRowHeight,e);
			
			m_mthDrawStrAtRectangle(this.m_fltNigColLeft,this.m_fltNiNeColLeft, "��¶",p_objLocationY ,e);
			m_mthDrawStrAtRectangle(this.m_fltNigColLeft,this.m_fltNiNeColLeft, "�ߵ�",p_objLocationY+ m_ftlRowHeight,e);
			
			m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft,this.m_fltColLeft10, "Ѫ",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft,this.m_fltColLeft10, "ѹ",p_objLocationY+ m_ftlRowHeight,e);
			
			m_mthDrawStrAtRectangle(this.m_fltColLeft10,this.m_fltColLeft11, "�������",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltColLeft10,this.m_fltColLeft11, "������",p_objLocationY+ m_ftlRowHeight,e);
			
			m_mthDrawStrAtRectangle(this.m_fltColLeft11,this.m_fltColLeft12, "ǩ",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltColLeft11,this.m_fltColLeft12, "��",p_objLocationY+ m_ftlRowHeight,e);
			
			
			p_objLocationY += m_ftlRowHeight*2;
			g.DrawLine(this.m_objPen, m_fltFirstColLeft,p_objLocationY,m_fltColLeft12, p_objLocationY);

			
		}
		#endregion 


		private void  m_mthDrawStrAtRectangle(float col1, float col2 ,string strPrint,float LocationY,System.Drawing.Printing.PrintPageEventArgs e)
		{
			System.Drawing.Graphics g = e.Graphics;	
			System.Drawing.Font m_font = this.m_fontBody;
			SizeF s = g.MeasureString(strPrint,m_font);
			if(s.Width >= this.m_fltAvgCol)
			{
				m_font = new System.Drawing.Font("����",8);
				s = g.MeasureString(strPrint,m_font);				
			}			
			
			float ji = col2 - col1;
			float X =  col1 + ji/2 - s.Width/2;
			float Y = LocationY + m_ftlRowHeight/2 - s.Height/2;				
			g.DrawString(strPrint,m_font,this.m_objBrush,X,Y);

		}

		// ��ӡ����ʱ�Ĳ���
		private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{		
			m_mthResetWhenEndPrint();
		}

		/// <summary>
		/// ÿ�δ�ӡ����֮��ĸ�λ,�����Ǵ�ӡ��ǰҳ���ߴ�ӡȫ��.
		/// </summary>
		private void m_mthResetWhenEndPrint()
		{

		}
		
	}
}
