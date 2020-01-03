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
	/// ��ӡ ̥���໤�� ��ժҪ˵����
	/// </summary>
	public class clsQuickeningTutelar_AcadPrintTool : infPrintRecord
	{
		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;		
		private clsPrintInfo_InPatientCaseHistory m_objPrintInfo;
		private clsBaseCaseHistoryDomain m_objRecordsDomain;
		public string m_strLaycount  = ""; //����
		public string m_strBirthDate  = "";// ����

		private clsQuickeningTutelarValue[] m_objResultArr = null;
		private	clsPatient m_objPatient = null;
		DateTime m_dtInHos ;

		#region �������ڴ�ֵ�ȱ��� ��
		public string m_strTOTALBLOODNUM_CHR;  
		public string m_strSEWPIN_CHR;
		public string m_strESPECIALRECORD_CHR;
		public string m_strPERIOD_CHR;
		public string m_strCHILDBIRTHINGDATE;  
		public string m_strRECORDPERSON_CHR;
		#endregion 
		public clsQuickeningTutelar_AcadPrintTool()
		{
            m_strTitle = "̥ �� �� �� ��";
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
		private float m_fltZijiHeight = 6; //�����߼��λ�ø� �����
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
			double kk=(Convert.ToDouble(e.PageBounds.Width - 50))/12.00;
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


			m_fltFirstColLeft = e.PageBounds.Left + 20 ; //��1��Left����
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
            //com.digitalwave.clsRecordsService.clsQuickeningTutelar_AcadService objServ =
            //    (com.digitalwave.clsRecordsService.clsQuickeningTutelar_AcadService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsQuickeningTutelar_AcadService));

            (new weCare.Proxy.ProxyEmr()).Service.m_lngGetAllMainRecord(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out m_objResultArr);
		
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
            if (m_blnIsFromDataSource)
            {
                if (m_objResultArr == null)
                {
                    MDIParent.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
                    return null;
                }
            }

            //û�м�¼����ʱ�����ؿ�
            if (m_objResultArr.Length == 0)
                return null;
            else
                return m_objResultArr;
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
			//			e.PageSettings.Landscape = true;
			//			e.PageSettings.Margins.Left = 0;
			//			e.PageSettings.Margins.Right = 0;
			//����
			//	e.Graphics.DrawRectangle(this.m_objPen,m_fltFirstColLeft,e.MarginBounds.Top,15*this.m_fltAvgCol,e.MarginBounds.Height);
			//

			
			m_mthPrintTitleInfo(e);
			m_mthPrintFormTitleInfo(e,this.m_objPatient,ref this.m_fltLocationY);
			mthInitColLocation(e);
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
			float PosY=p_objLocationY;
			if(m_objResultArr.Length <=0)
				return ;

			//�����һҳ���ܴ�ӡ����������¼
			int intRowCount = Convert.ToInt32((float.Parse(e.MarginBounds.Height.ToString()) - p_objLocationY)/m_ftlRowHeight);
			intRowCount--; //��Ϊ����һ��λ������ӡҳ��,��ӡ ��ע ��������ʾ������Ϊ��1
			intRowCount--; //��Ϊ����һ��λ������ӡ"�����¼"

			if(m_intCurrentPageIndex == 1)
			{
				int temp1;
				
				for(  i = 0; i< m_objResultArr.Length && i < intRowCount-1;i++)
				{
		
					#region draw one row
					print = m_objResultArr[i].m_dtmCreateDate.ToString("yy-MM-dd");
					m_mthDrawStrAtRectangle(this.m_fltFirstColLeft,this.m_fltSeconColLeft, print,p_objLocationY,e);

					print = m_objResultArr[i].m_strPREGNANTTEAM_CHR;
					m_mthDrawStrAtRectangle(this.m_fltSeconColLeft,this.m_fltthColLeft, print ,p_objLocationY,e);

					print = m_objResultArr[i].m_strMORNING_CHR;
					m_mthDrawStrAtRectangle(this.m_fltthColLeft,this.m_fltthirColLeft, print,p_objLocationY,e);

					print = m_objResultArr[i].m_strMIDDAY_CHR;
					m_mthDrawStrAtRectangle(this.m_fltthirColLeft,this.m_fltFiveColLeft, print,p_objLocationY,e);

					print = m_objResultArr[i].m_strEVENING_CHR;
					m_mthDrawStrAtRectangle(this.m_fltFiveColLeft,this.m_fltSixColLeft, print,p_objLocationY,e);
				
					print = m_objResultArr[i].m_strQUICKENINGNUM_CHR;
					m_mthDrawStrAtRectangle(this.m_fltSixColLeft,this.m_fltSenColLeft, print,p_objLocationY  ,e);

					m_mthDrawLines(e);

					p_objLocationY += this.m_ftlRowHeight;
					#endregion 
								
				}
                temp1=i;
				
				for(  ; i< m_objResultArr.Length && i < intRowCount+temp1-1;i++)
				{
					                    print = m_objResultArr[i].m_dtmCreateDate.ToString("yy-MM-dd");
										m_mthDrawStrAtRectangle(this.m_fltSenColLeft,this.m_fltNigColLeft, print,PosY ,e);
									
										print = m_objResultArr[i].m_strPREGNANTTEAM_CHR;
										m_mthDrawStrAtRectangle(this.m_fltNigColLeft,this.m_fltNiNeColLeft, print,PosY ,e);
					
										print = m_objResultArr[i].m_strMORNING_CHR;
										m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft,this.m_fltColLeft10, print,PosY,e);
					
										print = m_objResultArr[i].m_strMIDDAY_CHR;				
										m_mthDrawStrAtRectangle(this.m_fltColLeft10,this.m_fltColLeft11, print,PosY,e);
					
										print = m_objResultArr[i].m_strEVENING_CHR;
										m_mthDrawStrAtRectangle(this.m_fltColLeft11,this.m_fltColLeft12, print,PosY,e);
												
										print = m_objResultArr[i].m_strQUICKENINGNUM_CHR;
										m_mthDrawStrAtRectangle(this.m_fltColLeft12,this.m_fltColLeft13, print,PosY,e);
					
//										print = m_objResultArr[i].m_strURINE_CHR;
//										m_mthDrawStrAtRectangle(this.m_fltColLeft13,this.m_fltColLeft14, print,p_objLocationY,e);
//					
//										print = m_objResultArr[i].m_strANNOTATIONS_CHR;
//										m_mthDrawStrAtRectangle(this.m_fltColLeft14,this.m_fltColLeft15, print,p_objLocationY,e);
//					
//										print = m_objResultArr[i].m_strSCRTATOR_CHR;
//										m_mthDrawStrAtRectangle(this.m_fltColLeft15,this.m_fltColLeft15 + this.m_fltCol15, print,p_objLocationY,e);
                                     PosY+=this.m_ftlRowHeight;

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
			else//���ڶ�������....ҳ
			{
				int temp = i;

				#region draw one row
				// e.Graphics.DrawLine(this.m_objPen, m_fltFirstColLeft,p_objLocationY, m_fltColLeft12 + m_fltCol12, p_objLocationY);
                  m_mthPrintFormHeader( e, ref  p_objLocationY);
				for(  ; i< m_objResultArr.Length && i < intRowCount + temp-1;i++)
				{
					#region draw one row 
					print = m_objResultArr[i].m_dtmCreateDate.Date.ToString("yy-MM-dd");
					m_mthDrawStrAtRectangle(this.m_fltFirstColLeft,this.m_fltSeconColLeft, print,p_objLocationY,e);

					print = m_objResultArr[i].m_strPREGNANTTEAM_CHR;
					m_mthDrawStrAtRectangle(this.m_fltSeconColLeft,this.m_fltthColLeft, print ,p_objLocationY,e);

					print = m_objResultArr[i].m_strMORNING_CHR;
					m_mthDrawStrAtRectangle(this.m_fltthColLeft,this.m_fltthirColLeft, print,p_objLocationY,e);

					print = m_objResultArr[i].m_strMIDDAY_CHR;
					m_mthDrawStrAtRectangle(this.m_fltthirColLeft,this.m_fltFiveColLeft, print,p_objLocationY,e);

					print = m_objResultArr[i].m_strEVENING_CHR;
					m_mthDrawStrAtRectangle(this.m_fltFiveColLeft,this.m_fltSixColLeft, print,p_objLocationY,e);
				
					print = m_objResultArr[i].m_strQUICKENINGNUM_CHR;
					m_mthDrawStrAtRectangle(this.m_fltSixColLeft,this.m_fltSenColLeft, print,p_objLocationY  ,e);

//					print = m_objResultArr[i].m_strNIPPLE_CHR;
//					m_mthDrawStrAtRectangle(this.m_fltSenColLeft,this.m_fltNigColLeft, print,p_objLocationY ,e);
//				
//					print = m_objResultArr[i].m_strDEWNUM_CHR;
//					m_mthDrawStrAtRectangle(this.m_fltNigColLeft,this.m_fltNiNeColLeft, print,p_objLocationY ,e);
//
//					print = m_objResultArr[i].m_strDEWCOLOR_CHR;
//					m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft,this.m_fltColLeft10, print,p_objLocationY,e);
//
//					print = m_objResultArr[i].m_strDEWFUCK_CHR;				
//					m_mthDrawStrAtRectangle(this.m_fltColLeft10,this.m_fltColLeft11, print,p_objLocationY,e);
//
//					print = m_objResultArr[i].m_strPERINEUM_CHR;
//					m_mthDrawStrAtRectangle(this.m_fltColLeft11,this.m_fltColLeft12, print,p_objLocationY,e);
//							
//					print = m_objResultArr[i].m_strBP_CHR;
//					m_mthDrawStrAtRectangle(this.m_fltColLeft12,this.m_fltColLeft13, print,p_objLocationY,e);
//
//					print = m_objResultArr[i].m_strURINE_CHR;
//					m_mthDrawStrAtRectangle(this.m_fltColLeft13,this.m_fltColLeft14, print,p_objLocationY,e);
//
//					print = m_objResultArr[i].m_strANNOTATIONS_CHR;
//					m_mthDrawStrAtRectangle(this.m_fltColLeft14,this.m_fltColLeft15, print,p_objLocationY,e);
//
//					print = m_objResultArr[i].m_strSCRTATOR_CHR;
//					m_mthDrawStrAtRectangle(this.m_fltColLeft15,this.m_fltColLeft15 + this.m_fltCol15, print,p_objLocationY,e);

					#endregion

					m_mthDrawLines(e);



					p_objLocationY += this.m_ftlRowHeight;
								
				}

				#endregion
				temp=i;
				for(  ; i< m_objResultArr.Length && i < intRowCount+temp-1;i++)
				{
					print = m_objResultArr[i].m_dtmCreateDate.ToString("yy-MM-dd");
					m_mthDrawStrAtRectangle(this.m_fltSenColLeft,this.m_fltNigColLeft, print,PosY ,e);
									
					print = m_objResultArr[i].m_strPREGNANTTEAM_CHR;
					m_mthDrawStrAtRectangle(this.m_fltNigColLeft,this.m_fltNiNeColLeft, print,PosY ,e);
					
					print = m_objResultArr[i].m_strMORNING_CHR;
					m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft,this.m_fltColLeft10, print,PosY,e);
					
					print = m_objResultArr[i].m_strMIDDAY_CHR;				
					m_mthDrawStrAtRectangle(this.m_fltColLeft10,this.m_fltColLeft11, print,PosY,e);
					
					print = m_objResultArr[i].m_strEVENING_CHR;
					m_mthDrawStrAtRectangle(this.m_fltColLeft11,this.m_fltColLeft12, print,PosY,e);
												
					print = m_objResultArr[i].m_strQUICKENINGNUM_CHR;
					m_mthDrawStrAtRectangle(this.m_fltColLeft12,this.m_fltColLeft13, print,PosY,e);
					
					//										print = m_objResultArr[i].m_strURINE_CHR;
					//										m_mthDrawStrAtRectangle(this.m_fltColLeft13,this.m_fltColLeft14, print,p_objLocationY,e);
					//					
					//										print = m_objResultArr[i].m_strANNOTATIONS_CHR;
					//										m_mthDrawStrAtRectangle(this.m_fltColLeft14,this.m_fltColLeft15, print,p_objLocationY,e);
					//					
					//										print = m_objResultArr[i].m_strSCRTATOR_CHR;
					//										m_mthDrawStrAtRectangle(this.m_fltColLeft15,this.m_fltColLeft15 + this.m_fltCol15, print,p_objLocationY,e);
					PosY+=this.m_ftlRowHeight;

				}

				m_mthPrintFoot(e);

				//�ж��Ƿ��ҳ

				if( intRowCount < m_objResultArr.Length-i-1)
				{
					m_intCurrentPageIndex ++;
					e.HasMorePages = true;
					return;
				}
					
			}
			//�����һҳ�д�ӡ ��ע��
			if(!m_blnOnlyPrintOnceHadPrinted)
			{
				m_mthPrintFuZhu(e,p_objLocationY);
				m_blnOnlyPrintOnceHadPrinted = true;
				p_objLocationY += this.m_ftlRowHeight;
				p_objLocationY += this.m_ftlRowHeight;
			}

			#region ��ӡ�����¼
//			Char[] ch = m_strESPECIALRECORD_CHR.ToCharArray();
//			string str = "";
//			float tempX = this.m_fltFirstColLeft;
//				
//			for(; m_intRecordIndex < ch.Length ; m_intRecordIndex ++)
//			{
//				if(m_intRecordIndex==0)
//				{
//					str = "     "+ch[m_intRecordIndex].ToString();
//				}
//				else
//				{
//					str = ch[m_intRecordIndex].ToString();
//				}
//				SizeF sf = e.Graphics.MeasureString(str,this.m_fontBody);
//				if( p_objLocationY >= e.MarginBounds.Height )
//				{					
//					m_intCurrentPageIndex ++;
//					e.HasMorePages = true;
//					return;
//				}
//				if(tempX  < this.m_fltColLeft15 + this.m_fltCol15 - this.m_fltFirstColLeft)
//				{
//							
//					e.Graphics.DrawString(str, this.m_fontBody,this.m_objBrush,tempX ,p_objLocationY);
//					tempX  = tempX + sf.Width;
//				}
//				else
//				{
//					tempX = this.m_fltFirstColLeft;
//					p_objLocationY += this.m_ftlRowHeight;
//					e.Graphics.DrawString(str, this.m_fontBody,this.m_objBrush,tempX ,p_objLocationY);
//					tempX  = tempX + sf.Width;
//				}
//			}					
			#endregion

//			#region ��ӡ��¼�� 
//			p_objLocationY += this.m_ftlRowHeight;
//			if( p_objLocationY < e.MarginBounds.Height && !m_blnOnlyPrintOnceHadPrintedPerson)
//			{
//				string strP = "��¼�ˣ�"+ m_strRECORDPERSON_CHR;
//				e.Graphics.DrawString(strP, this.m_fontBody,this.m_objBrush,e.MarginBounds.Right - e.Graphics.MeasureString(strP,this.m_fontBody).Width,p_objLocationY);
//				m_blnOnlyPrintOnceHadPrintedPerson =  true;
//			}
//			else
//			{
//				m_intCurrentPageIndex ++;
//				e.HasMorePages = true;
//				return;
//			}
//			#endregion

		}

		#endregion 

		#region �����һҳ�д�ӡ ��ע������24Сʱ�ܳ�Ѫ����___ml ,�����˿ڲ��ߣ����__�룬���ϼ���__��
		private void m_mthPrintFuZhu(System.Drawing.Printing.PrintPageEventArgs e, float p_objLocationY)
		{
			p_objLocationY += this.m_ftlRowHeight;
			string str = "ע�� 1.�������и��硢�С�������ԡ�����̥��1Сʱ������̥����ӳ���4����Ϊ12Сʱ̥������";
			SizeF s = e.Graphics.MeasureString(str,this.m_fontBody);
			float with = float.Parse(e.PageBounds.Width.ToString()) - s.Width;
			e.Graphics.DrawString(str,this.m_fontBody,this.m_objBrush,this.m_fltFirstColLeft,p_objLocationY + m_fltZijiHeight/2);
			p_objLocationY += this.m_ftlRowHeight;
			string str1 = "       2.12Сʱ̥��>30��Ϊ̥��������ã�����20�α�ʾ̥��ȱ��������10��ΪԤ������Ӧ��ʱ���";

			e.Graphics.DrawString(str1,this.m_fontBody,this.m_objBrush,this.m_fltFirstColLeft,p_objLocationY + m_fltZijiHeight/2);
            p_objLocationY += this.m_ftlRowHeight;
			string str2 = "       3.���򾲼�������ҩ��̥�����������á�";

			e.Graphics.DrawString(str2,this.m_fontBody,this.m_objBrush,this.m_fltFirstColLeft,p_objLocationY + m_fltZijiHeight/2);
			p_objLocationY += this.m_ftlRowHeight;


		}
		#endregion

		#region ����
		private void m_mthDrawLines(PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			for(int i1 = 0 ;i1 < 13; i1 ++)//16��
			{
				g.DrawLine(this.m_objPen , this.m_fltFirstColLeft + this.m_fltAvgCol * i1,this.m_fltLocationY, this.m_fltFirstColLeft + this.m_fltAvgCol * i1,this.m_fltLocationY + this.m_ftlRowHeight);
			}
			g.DrawLine(this.m_objPen , this.m_fltFirstColLeft ,this.m_fltLocationY + this.m_ftlRowHeight, this.m_fltColLeft12 + this.m_fltCol12,this.m_fltLocationY + this.m_ftlRowHeight);


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
                //com.digitalwave.clsRecordsService.clsQuickeningTutelar_AcadService objServ =
                //    (com.digitalwave.clsRecordsService.clsQuickeningTutelar_AcadService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsQuickeningTutelar_AcadService));

                (new weCare.Proxy.ProxyEmr()).Service.clsQuickeningTutelar_AcadService_m_lngUpdateALLFirstPrintDate(m_objPatient.m_StrInPatientID,m_dtInHos.ToString(),System.DateTime.Now);
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
//			string strPrint = "";
//			int col = e.MarginBounds.Width/4;
//			float fltCol = float.Parse(col.ToString());//��ӡ�������е��п�
//			System.Drawing.Graphics g = e.Graphics;			
//
//			strPrint  = "����:"+p_objPatient.m_StrName.Trim();
//			SizeF objSize = g.MeasureString(strPrint, this.m_fontBody);
//			g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left , p_fltLocationY);
//
//			strPrint  = "��������:"+m_strCHILDBIRTHINGDATE;
//			objSize = g.MeasureString(strPrint, this.m_fontBody);
//			g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 1  -20, p_fltLocationY);
//
//			strPrint  = "����:"+p_objPatient.m_strBedCode.Trim();
//			objSize = g.MeasureString(strPrint, this.m_fontBody);
//			g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 2 + 50 , p_fltLocationY);
//
//			strPrint  = "סԺ��:"+p_objPatient.m_StrInPatientID.Trim();
//			objSize = g.MeasureString(strPrint, this.m_fontBody);
//			g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 3 , p_fltLocationY);
//
//			this.m_fltLocationY = p_fltLocationY + objSize.Height;
       
		}
		#endregion

		#region ���ͷ
		private void m_mthPrintFormHeader(System.Drawing.Printing.PrintPageEventArgs e, ref float p_objLocationY)
		{
			
			System.Drawing.Graphics g = e.Graphics;	
			

			g.DrawLine(this.m_objPen, m_fltFirstColLeft,p_objLocationY, m_fltColLeft12 + m_fltCol12, p_objLocationY);

	 
			for(int i1=0;i1<13;i1++)
			{
				//g.DrawLine(this.m_objPen, e.MarginBounds.Left + e.MarginBounds.Width/14 * i1,p_objLocationY, e.MarginBounds.Left + e.MarginBounds.Width/14 * i1, p_objLocationY +m_ftlRowHeight);
				g.DrawLine(this.m_objPen, this.m_fltFirstColLeft + m_fltAvgCol * i1  ,p_objLocationY, m_fltFirstColLeft + m_fltAvgCol * i1 , p_objLocationY +m_ftlRowHeight * 2);

			}
//			SizeF s = g.MeasureString("����",this.m_fontBody);
//			float y = p_objLocationY + m_ftlRowHeight*2 /2 - s.Height/2;
//			float y1 = p_objLocationY + m_ftlRowHeight /2 - s.Height/2;
//			float y2 = p_objLocationY + m_ftlRowHeight + m_ftlRowHeight /2 - s.Height/2;

			m_mthDrawStrAtRectangle(this.m_fltFirstColLeft,this.m_fltSeconColLeft, "����",p_objLocationY+5,e);
			m_mthDrawStrAtRectangle(this.m_fltSeconColLeft,this.m_fltthColLeft, "����",p_objLocationY+5,e);
			m_mthDrawStrAtRectangle(this.m_fltthColLeft,this.m_fltthirColLeft, "��",p_objLocationY+5,e);
			m_mthDrawStrAtRectangle(this.m_fltthirColLeft,this.m_fltFiveColLeft, "��",p_objLocationY+5,e);
			m_mthDrawStrAtRectangle(this.m_fltFiveColLeft,this.m_fltSixColLeft, "��",p_objLocationY+5 ,e);
			//			m_mthDrawStrAtRectangle(this.m_fltthColLeft,this.m_fltthirColLeft, "cm",p_objLocationY + m_ftlRowHeight+ m_ftlRowHeight,e);
			m_mthDrawStrAtRectangle(this.m_fltSixColLeft,this.m_fltSenColLeft, "12Сʱ",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltSixColLeft,this.m_fltSenColLeft, "̥����",p_objLocationY+ m_ftlRowHeight,e);
			//			m_mthDrawStrAtRectangle(this.m_fltthirColLeft,this.m_fltFiveColLeft, "���",p_objLocationY+ m_ftlRowHeight+ m_ftlRowHeight,e);
			
			m_mthDrawStrAtRectangle(this.m_fltSenColLeft,this.m_fltNigColLeft, "����",p_objLocationY+5 ,e);
			m_mthDrawStrAtRectangle(this.m_fltNigColLeft,this.m_fltNiNeColLeft, "����",p_objLocationY+5 ,e);
			m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft,this.m_fltColLeft10, "��",p_objLocationY+5,e);
			m_mthDrawStrAtRectangle(this.m_fltColLeft10,this.m_fltColLeft11, "��",p_objLocationY+5,e);
			
			m_mthDrawStrAtRectangle(this.m_fltColLeft11,this.m_fltColLeft12, "��",p_objLocationY+5,e);
			m_mthDrawStrAtRectangle(this.m_fltColLeft12,this.m_fltColLeft13, "12Сʱ",p_objLocationY,e);
			m_mthDrawStrAtRectangle(this.m_fltColLeft12,this.m_fltColLeft13, "̥����",p_objLocationY+ m_ftlRowHeight,e);
//			m_mthDrawStrAtRectangle(this.m_fltColLeft13,this.m_fltColLeft14, "��",p_objLocationY,e);
//			m_mthDrawStrAtRectangle(this.m_fltColLeft14,this.m_fltColLeft15, "��ע",p_objLocationY,e);
//			m_mthDrawStrAtRectangle(this.m_fltColLeft15,this.m_fltColLeft15 + this.m_fltCol15, "�����",p_objLocationY,e);

			p_objLocationY += m_ftlRowHeight* 2;
			g.DrawLine(this.m_objPen, m_fltFirstColLeft,p_objLocationY, m_fltColLeft12 + m_fltCol12, p_objLocationY);

			
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
