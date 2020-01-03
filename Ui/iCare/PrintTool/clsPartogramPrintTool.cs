using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Drawing;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;

namespace iCare
{
    /// <summary>
    /// �и����̼�¼�Ĵ�ӡ��
    /// </summary>
    public class clsPartogramPrintTool : infPrintRecord
    {
        private clsPartogramAll_VO m_objPartogramAll;
        private int m_intCurrentPage = 0;
        private string m_strRegisterId = string.Empty;//סԺ��ˮ
        private string m_strPatientName = string.Empty;//����
        private string m_strDeptName = string.Empty;//��������
        private string m_strPatientSex = string.Empty;//�Ա�
        private string m_strPatientAge = string.Empty;//����
        private string m_strInPatientId = string.Empty; //סԺ��
        private string m_strBedNo = string.Empty; //����
        private string m_strOcupy = string.Empty; //ְҵ
        private string m_strNation = string.Empty;  //����
        private string m_strNativePlace = string.Empty;//����
        private com.digitalwave.Utility.Controls.clsPartogramPrintTool m_objPartogramPrinter;
        private const int m_intTopBankHeight = 190;

        Font m_fotTitleFont;
        Font m_fotHeaderFont;
        Font m_fotSmallFont;

        private const int m_intHospitalNameTop = 30;
        private const int m_intTitleTop = 60;
        private const int m_intIDTop = 100;
        private const int m_intRectTop = 130;
        private const int m_intTimeTop = 150;
        private const int c_intPageLeft = 50;
        private const int c_intPageRight = 710;
        private const int c_intUpHeight = 90;

        #region infPrintRecord ��Ա

        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            if (p_objPatient != null)
            {
                m_strPatientName = p_objPatient.m_StrName;
                m_strRegisterId = p_objPatient.m_StrRegisterId;
                m_strDeptName = p_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName;
                m_strBedNo = p_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName;
                m_strPatientAge = p_objPatient.m_ObjPeopleInfo.m_StrAge;
                m_strInPatientId = p_objPatient.m_StrHISInPatientID;
                m_strPatientSex = p_objPatient.m_ObjPeopleInfo.m_StrSex;
                m_strOcupy = p_objPatient.m_ObjPeopleInfo.m_StrOccupation;
                m_strNation = p_objPatient.m_ObjPeopleInfo.m_StrNation;
                m_strNativePlace = p_objPatient.m_ObjPeopleInfo.m_StrNativePlace;
            }
        }

        public void m_mthInitPrintContent()
        {
            if (string.IsNullOrEmpty(m_strRegisterId)) return;
            long lngRes = new clsPartogramDomain().m_lngGetValues(m_strRegisterId, out m_objPartogramAll);
            com.digitalwave.Utility.Controls.clsPartogramManager objPartogramManager = new com.digitalwave.Utility.Controls.clsPartogramManager();
            if (lngRes > 0 && m_objPartogramAll != null)
            {
                if (m_objPartogramAll.m_ObjPartogramArr != null)
                {
                    objPartogramManager.m_strReAddRange(m_objPartogramAll.m_ObjPartogramArr);
                }
            }
            m_objPartogramPrinter = new com.digitalwave.Utility.Controls.clsPartogramPrintTool(m_intTopBankHeight, objPartogramManager);
            objPartogramManager = null;
        }

        public void m_mthSetPrintContent(object p_objPrintContent)
        {
            
        }

        public object m_objGetPrintInfo()
        {
            return m_objPartogramAll;
        }
        public void m_mthInitPrintTool(object p_objArg)
        {
			m_fotTitleFont = new Font("SimSun", 16,FontStyle.Bold);
			m_fotHeaderFont = new Font("SimSun", 18,FontStyle.Bold);
			m_fotSmallFont = new Font("SimSun",11);
        }

        public void m_mthDisposePrintTools(object p_objArg)
        {
            m_objPartogramPrinter.m_mthClear();
            m_objPartogramPrinter = null;
            m_objPartogramAll = null;
            if (m_fotTitleFont != null)
                m_fotTitleFont.Dispose();
            if (m_fotHeaderFont != null)
                m_fotHeaderFont.Dispose();
            if (m_fotSmallFont != null)
                m_fotSmallFont.Dispose();
        }

        public void m_mthBeginPrint(object p_objPrintArg)
        {
            
        }

        public void m_mthPrintPage(object p_objPrintArg)
        {
            frmPrintPreviewDialogPF frmPreview = new frmPrintPreviewDialogPF();
            frmPreview.m_evtBeginPrint += new PrintEventHandler(frmPreview_m_evtBeginPrint);
            frmPreview.m_evtEndPrint += new PrintEventHandler(frmPreview_m_evtEndPrint);
            frmPreview.m_evtPrintContent += new PrintPageEventHandler(frmPreview_m_evtPrintContent);
            frmPreview.m_evtPrintFrame += new PrintPageEventHandler(frmPreview_m_evtPrintFrame);
            frmPreview.ShowDialog();
        }

        public void m_mthEndPrint(object p_objPrintArg)
        {
            m_objPartogramAll = null;
            m_intCurrentPage = 0;
        }

        #endregion

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
           //
        }
        private void frmPreview_m_evtPrintFrame(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            m_mthPrintTitleInfo(e);
            m_mthPrintGridAndTitle(e);
        }
        #endregion

        #region �������ֲ���
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            using (SolidBrush slbBrush = new SolidBrush(Color.Black))
            {
                StringFormat sf = new StringFormat(StringFormatFlags.FitBlackBox);
                sf.Alignment = StringAlignment.Center;
                e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotTitleFont , slbBrush, new RectangleF(50, m_intHospitalNameTop,750,30),sf);

                e.Graphics.DrawString("�и����̼�¼", m_fotHeaderFont, slbBrush, new RectangleF(50, m_intTitleTop,750,40),sf);


                e.Graphics.DrawString("������" + m_strPatientName, m_fotSmallFont, slbBrush, 30, m_intIDTop);

                e.Graphics.DrawString("�Ա�" + m_strPatientSex, m_fotSmallFont, slbBrush, 160, m_intIDTop);

                e.Graphics.DrawString("���䣺" + m_strPatientAge, m_fotSmallFont, slbBrush, 240, m_intIDTop);

                e.Graphics.DrawString("���ң�" + m_strDeptName, m_fotSmallFont, slbBrush, 360, m_intIDTop);

                e.Graphics.DrawString("���ţ�" + m_strBedNo, m_fotSmallFont, slbBrush, 550, m_intIDTop);

                e.Graphics.DrawString("סԺ�ţ�" + m_strInPatientId, m_fotSmallFont, slbBrush, 640, m_intIDTop);
            }
        }
        #endregion	
        private void m_mthPrintGridAndTitle(System.Drawing.Printing.PrintPageEventArgs e)
        {
            using (SolidBrush slbBrush = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft, m_intIDTop + 24, c_intPageRight, m_intIDTop + 24);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft, m_intIDTop + 24, c_intPageLeft, m_intIDTop + 24 + c_intUpHeight);
                e.Graphics.DrawLine(Pens.Black, c_intPageRight, m_intIDTop + 24, c_intPageRight, m_intIDTop + 24 + c_intUpHeight);
                e.Graphics.DrawString("����:"+m_strPatientName, m_fotSmallFont, slbBrush, c_intPageLeft + 2, m_intIDTop + 32);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft + 40, m_intIDTop + 48, c_intPageLeft + 120, m_intIDTop + 48);

                e.Graphics.DrawString("����:"+m_strPatientAge, m_fotSmallFont, slbBrush, c_intPageLeft + 120, m_intIDTop + 32);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft + 160, m_intIDTop + 48, c_intPageLeft + 220, m_intIDTop + 48);
                
                e.Graphics.DrawString("ְҵ:"+m_strOcupy, m_fotSmallFont, slbBrush, c_intPageLeft + 220, m_intIDTop + 32);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft + 260, m_intIDTop + 48, c_intPageLeft + 340, m_intIDTop + 48);
                
                e.Graphics.DrawString("����:"+m_strNation, m_fotSmallFont, slbBrush, c_intPageLeft + 340, m_intIDTop + 32);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft + 380, m_intIDTop + 48, c_intPageLeft + 460, m_intIDTop + 48);
                
                e.Graphics.DrawString("����:"+m_strNativePlace, m_fotSmallFont, slbBrush, c_intPageLeft + 460, m_intIDTop + 32);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft + 500, m_intIDTop + 48, c_intPageLeft + 580, m_intIDTop + 48);
                
                e.Graphics.DrawString("��/��:", m_fotSmallFont, slbBrush, c_intPageLeft + 580, m_intIDTop + 32);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft + 620, m_intIDTop + 48, c_intPageRight - 2, m_intIDTop + 48);

                e.Graphics.DrawString("ĩ���¾�: 2006��12��12��", m_fotSmallFont, slbBrush, c_intPageLeft + 2, m_intIDTop + 53);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft + 92, m_intIDTop + 70, c_intPageLeft + 212, m_intIDTop + 70);

                e.Graphics.DrawString("Ԥ����: 2006��12��12��", m_fotSmallFont, slbBrush, c_intPageLeft + 212, m_intIDTop + 53);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft + 282, m_intIDTop + 70, c_intPageLeft + 402, m_intIDTop + 70);

                e.Graphics.DrawString("��ˮʱ��: 2006��12��12�� 12ʱ", m_fotSmallFont, slbBrush, c_intPageLeft + 402, m_intIDTop + 53);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft + 492, m_intIDTop + 70, c_intPageRight - 2, m_intIDTop + 70);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft, m_intIDTop + 72, c_intPageRight, m_intIDTop + 72);

                e.Graphics.DrawString("�ٲ���ʼʱ��:   2006��12��12�� 12ʱ12��", m_fotSmallFont, slbBrush, c_intPageLeft + 2, m_intIDTop + 75);
                e.Graphics.DrawLine(Pens.Black, c_intPageLeft + 102, m_intIDTop + 95, c_intPageLeft + 402, m_intIDTop + 95);
            }
        }
    }
}
