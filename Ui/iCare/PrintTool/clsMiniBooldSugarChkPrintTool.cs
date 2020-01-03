using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// ����΢��Ѫ�Ǽ���¼���ӡ����
	/// </summary>
	public class clsMiniBooldSugarChkPrintTool : infPrintRecord
	{
		public clsMiniBooldSugarChkPrintTool()
		{
			
		}
		private clsMiniBooldSugarChkDomin m_objDomain;
		private clsPatient m_objPatient;
		private clsMiniBloodSugarChkValue[] m_objValues;
		
		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate)
		{	
			m_objPatient=p_objPatient;
		}
		public void m_mthSetPrintInfo(clsPatient p_objPatient)
		 {	
			 m_objPatient=p_objPatient;
		 }

		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
		/// </summary>
		public void m_mthInitPrintContent()
		{		
			if(m_objPatient==null)
			{
//				clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
				return;
			}
			if(m_objPatient.m_StrInPatientID=="" || m_objPatient.m_DtmSelectedInDate==DateTime.MinValue)
				m_objValues=null;				
			else
			{
				m_objDomain=new clsMiniBooldSugarChkDomin();
				long lngRes=m_objDomain.m_lngGetRecoedByInPatient(m_objPatient.m_StrInPatientID,m_objPatient.m_DtmSelectedInDate,out m_objValues );
				if(lngRes <= 0 || m_objValues == null)
					return ;  
			}
			//���ñ����ݵ���ӡ��			
           // m_mthSetPrintValue();//�����з��ӡ����,��ʹ�ڴ�ӡ�հױ�ʱ,����Ҳ����ִ��.
		}

		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{}

		/// <summary>
		/// ��ȡ��ӡ����,(�������ݿ��ȡʱ,���ñ�����֮ǰ�����ȵ���m_mthSetPrintInfo����)
		/// </summary>
		/// <returns>��ӡ����</returns>
		public object m_objGetPrintInfo()
		{
            return m_objValues;
            
		}		

		/// <summary>
		/// ��ʼ����ӡ����,��������ն��󼴿�.
		/// </summary>
		public void m_mthInitPrintTool(object p_objArg)
		{				
			#region �йش�ӡ��ʼ��
			m_fotTitleFont = new Font("SimSun", 20,FontStyle.Bold);
			m_fotHeaderFont = new Font("SimSun", 18,FontStyle.Bold);
			m_fotSmallFont = new Font("SimSun",12);
			m_GridPen = new Pen(Color.Black,1);
			m_slbBrush = new SolidBrush(Color.Black);

			#endregion
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		public void m_mthDisposePrintTools(object p_objArg)
		{
			m_fotTitleFont.Dispose();
			m_fotHeaderFont.Dispose();
			m_fotSmallFont .Dispose();
			m_GridPen .Dispose();
			m_slbBrush .Dispose();
		}

		/// <summary>
		/// ��ӡ��ʼ
		/// </summary>
		/// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
		public void m_mthBeginPrint(object p_objPrintArg)
		{}

		/// <summary>
		/// ��ӡ��
		/// </summary>
		/// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintPageEventArgs���͵Ķ���</param>
		public void m_mthPrintPage()
		{
			frmPrintPreviewDialogPF frmPreview = new frmPrintPreviewDialogPF();
			frmPreview.m_evtBeginPrint +=new PrintEventHandler(frmPreview_m_evtBeginPrint);
			frmPreview.m_evtEndPrint +=new PrintEventHandler(frmPreview_m_evtEndPrint);
			frmPreview.m_evtPrintContent +=new PrintPageEventHandler(frmPreview_m_evtPrintContent);
			frmPreview.m_evtPrintFrame +=new PrintPageEventHandler(frmPreview_m_evtPrintFrame);
			frmPreview.ShowDialog();
		}
        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
        }
		#region  �����¼�
		private void frmPreview_m_evtBeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			m_mthBeginPrint(e);
		}
		private void frmPreview_m_evtEndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			m_mthEndPrint(e);
		}
		private void frmPreview_m_evtPrintContent(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			m_mthAddDataToGrid(e);
		}
		private void frmPreview_m_evtPrintFrame(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			m_mthPrintTitleInfo(e);	
			m_mthPrintRectangleInfo(e);	
		}
		#endregion

		/// <summary>
		/// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
		/// </summary>
		/// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
		public void m_mthEndPrint(object p_objPrintArg)
		{
			m_intLines = 0;
			m_intPages = 1;
		}

		// ��ӡҳ
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
		{
            try
            {
                p_objPrintPageArg.HasMorePages = false;
                m_mthPrintTitleInfo(p_objPrintPageArg);
                m_mthAddDataToGrid(p_objPrintPageArg);
                m_mthPrintRectangleInfo(p_objPrintPageArg);
                
            }
            catch (Exception err)
            {
                MDIParent.ShowInformationMessageBox(err.Message + "\r\n" + err.StackTrace);
            }
        }

		#region ��ӡ

		#region �йش�ӡ������
			
		/// <summary>
		/// ���������
		/// </summary>
		private Font m_fotTitleFont;
		/// <summary>
		/// ��ͷ������
		/// </summary>
		private Font m_fotHeaderFont;
		/// <summary>
		/// �����ݵ�����
		/// </summary>
		private Font m_fotSmallFont;
		/// <summary>
		/// �߿򻭱�
		/// </summary>
		private Pen m_GridPen;
		/// <summary>
		/// ˢ��
		/// </summary>
		private SolidBrush m_slbBrush;

		private int m_intPages = 1;
		private int m_intLines=0;
		private int m_intLineStep = 135;
		private string m_strDateFormat = "yyyy��MM��dd�� HHʱmm��ss��";

		/// <summary>
		/// ���ӵ���Ϣ
		/// </summary>
		private enum enmRectangleInfo
		{
			/// <summary>
			/// ���ӵĶ���
			/// </summary>
			TopY = 130,
			///<summary>
			/// ���ӵ����
			/// </summary>
			LeftX = 40,
			/// <summary>
			/// ���ӵ��Ҷ�
			/// </summary>
			RightX = 820-40,
			/// <summary>
			/// ����ÿ�еĲ���
			/// </summary>
			RowStep = 25,

			BottomY=1024		
		}
		
		
		#endregion
				
		#region �������ֲ���
		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotSmallFont, m_slbBrush, 320,30);
			e.Graphics.DrawString("����΢��Ѫ�Ǽ���¼��",m_fotTitleFont,m_slbBrush,240,70);
			
			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX,120);
			//e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+50,120,(int)enmRectangleInfo.LeftX+120,120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_ObjPeopleInfo.m_StrFirstName), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 45, 120);

			e.Graphics.DrawString("�Ա�",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+125,120);
			//e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+165,120,(int)enmRectangleInfo.LeftX+185,120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_ObjPeopleInfo.m_StrSex), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 165, 120);
						
			e.Graphics.DrawString("���䣺",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+200,120);
			//e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+230,120,(int)enmRectangleInfo.LeftX+275,120);
			e.Graphics.DrawString((m_objPatient == null?"":m_objPatient.m_ObjPeopleInfo.m_StrAge) ,m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+240,120);
	
            //e.Graphics.DrawString("���ң�",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+270,100);
            //e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+315,120,(int)enmRectangleInfo.LeftX+385,120);
            //e.Graphics.DrawString((m_objPatient == null?"":m_objPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptName.Trim()) ,m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+315,100);

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+335,120);
			//e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+430,120,(int)enmRectangleInfo.LeftX+450,120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(m_objPatient.m_DtmSelectedInDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 375, 120);
						
			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+605,120);
			//e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+560,120,(int)enmRectangleInfo.LeftX+640,120);
			e.Graphics.DrawString((m_objPatient == null?"":m_objPatient.m_StrInPatientID.Trim()) ,m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+665,120);
	
			e.Graphics.DrawString("���ţ�",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+525,120);
			//e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+695,120,(int)enmRectangleInfo.LeftX+740,120);
			string strTemp;
			try
			{
				 strTemp=m_objPatient == null?"":m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName.ToString().Trim();
			}
			catch
			{
			strTemp="";
			}
			e.Graphics.DrawString(strTemp,m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+565,120);
		}
		
	
		#endregion		

		private void m_mthPrintRectangleInfo(PrintPageEventArgs e)
		{
			int intYPos = 145;
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,intYPos,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.BottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.RightX,intYPos,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.BottomY);

			int intXPos = 200;
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+intXPos,intYPos,(int)enmRectangleInfo.LeftX+intXPos,(int)enmRectangleInfo.BottomY);
			e.Graphics.DrawString("     ��    ��",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX,148);
			e.Graphics.DrawString("���ǰ(mmol/L)",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+intXPos+1,148);
			intXPos += m_intLineStep;
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+intXPos,intYPos,(int)enmRectangleInfo.LeftX+intXPos,(int)enmRectangleInfo.BottomY);
			e.Graphics.DrawString("�в�ǰ(mmol/L)",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+intXPos+1,148);
			intXPos += m_intLineStep;
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+intXPos,intYPos,(int)enmRectangleInfo.LeftX+intXPos,(int)enmRectangleInfo.BottomY);
			e.Graphics.DrawString("���ǰ(mmol/L)",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+intXPos+1,148);
			intXPos += m_intLineStep;
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+intXPos,intYPos,(int)enmRectangleInfo.LeftX+intXPos,(int)enmRectangleInfo.BottomY);
			e.Graphics.DrawString("��˯ǰ(mmol/L)",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+intXPos+1,148);
			
			for(int i=0;i<37;i++)
			{
				e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,intYPos,(int)enmRectangleInfo.RightX,intYPos);
				intYPos += (int)enmRectangleInfo.RowStep;
			}
			e.Graphics.DrawString("�� "+m_intPages.ToString()+" ҳ",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+600,intYPos+30);
		}
		private void m_mthAddDataToGrid(PrintPageEventArgs e)
		{
			if(m_objValues == null)
			{
				e.HasMorePages = false;
				return;
			}
			int intYPos = 172;
			for(;m_intLines <m_objValues.Length;)
			{
				e.Graphics.DrawString(m_objValues[m_intLines].m_dtmCreatedDate.ToString(m_strDateFormat),new Font("SimSun",10),m_slbBrush,(int)enmRectangleInfo.LeftX,intYPos);
				e.Graphics.DrawString(m_objValues[m_intLines].m_strBreakfast,m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+201,intYPos);
				e.Graphics.DrawString(m_objValues[m_intLines].m_strLunch,m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+336,intYPos);
				e.Graphics.DrawString(m_objValues[m_intLines].m_strSupper,m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+471,intYPos);
				e.Graphics.DrawString(m_objValues[m_intLines].m_strPreRest,m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+606,intYPos);
				intYPos += (int)enmRectangleInfo.RowStep;
				m_intLines++;
				if(m_intLines%35 == 0)
				{
					m_intPages++;
					e.HasMorePages = true;
					return;
				}
			}
			e.HasMorePages = false;
		}

		#endregion
	}
}
