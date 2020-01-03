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
    /// ѪҺ������¼���ӡ������ 
    /// </summary>
    public class clsBloodCleanseRecordPrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;

        private clsBloodCleanRecordValueContentDataInfo m_objResult = null;
        private clsPatient m_objPatient = null;
        private clsTransDataInfo[] objAr = null;

        public clsBloodCleanseRecordPrintTool()
        {
            m_strTitle = "ѪҺ������¼��";

        }

        ///<summary>
        ///���������ƴ�ӡ��¼�ˣ�ֻ�ܴ�ӡһ�Ρ�
        ///</summary>
        bool m_blnOnlyPrintOnceHadPrintedPerson;
        ///<summary>
        ///���������ƴ�ӡ��ע��ֻ�ܴ�ӡһ�Ρ�
        ///</summary>
        bool m_blnOnlyPrintOnceHadPrinted;

        ///<summary>
        ///�����������¼�����¼�
        ///</summary>
        int m_intRecordIndex = 0;

        private clsPrint2[] m_objLine2Arr;

        #region ���ô�ӡ�п���ÿ�еĺ��������

        ///<summary>
        ///��������1�п��
        ///</summary>
        float m_fltFirstCol; //��1�п��
        ///<summary>
        ///��������2�п��
        ///</summary>
        float m_fltSeconCol; //��2�п��
        ///<summary>
        ///��������3�п��
        ///</summary>
        float m_fltthCol; //��3�п��
        ///<summary>
        ///��������4�п��
        ///</summary>
        float m_fltthirCol; //��4�п��
        ///<summary>
        ///��������5�п��
        ///</summary>
        float m_fltFiveCol; //��5�п��
        ///<summary>
        ///��������6�п��
        ///</summary>
        float m_fltSixCol; //��6�п��
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
        float m_fltNiNeCol;
        ///<summary>
        ///��������10�п��
        ///</summary>
        float m_fltCol10;
        ///<summary>
        ///��������11�п��
        ///</summary>
        float m_fltCol11;
        ///<summary>
        ///��������12�п��
        ///</summary>
        float m_fltCol12;
        ///<summary>
        ///��������13�п��
        ///</summary>
        float m_fltCol13;
        ///<summary>
        ///��������14�п��
        ///</summary>
        float m_fltCol14;
        ///<summary>
        ///��������15�п��
        ///</summary>
        float m_fltCol15;

        ///<summary>
        ///��������1��Left����
        ///</summary>
        float m_fltFirstColLeft; //��1��Left����
        ///<summary>
        ///��������2��Left����
        ///</summary>
        float m_fltSeconColLeft; //��2��Left����
        ///<summary>
        ///��������3��Left����
        ///</summary>
        float m_fltthColLeft; //��3��Left����
        ///<summary>
        ///��������4��Left����
        ///</summary>
        float m_fltthirColLeft; //��4��Left����
        ///<summary>
        ///��������5��Left����
        ///</summary>
        float m_fltFiveColLeft; //��5��Left����
        ///<summary>
        ///��������6��Left����
        ///</summary>
        float m_fltSixColLeft; //��6��Left����
        ///<summary>
        ///��������7��Left����
        ///</summary>
        float m_fltSenColLeft; //��7��Left����
        ///<summary>
        ///��������8��Left����
        ///</summary>
        float m_fltNigColLeft; //��8��Left����
        ///<summary>
        ///��������9��Left����
        ///</summary>
        float m_fltNiNeColLeft; //��9��Left����
        ///<summary>
        ///��������10��Left����
        ///</summary>
        float m_fltColLeft10;
        ///<summary>
        ///��������11��Left����
        ///</summary>
        float m_fltColLeft11;
        ///<summary>
        ///��������12��Left����
        ///</summary>
        float m_fltColLeft12;
        ///<summary>
        ///��������13��Left����
        ///</summary>
        float m_fltColLeft13;
        ///<summary>
        ///��������14��Left����
        ///</summary>
        float m_fltColLeft14;

        ///<summary>
        ///��������14��rigth����,��15�е�������
        ///</summary>
        float m_fltColLeft15;
        #endregion

        #region ��ӡ���ñ���
        /// <summary>
        /// ��ӡ���������
        /// </summary>	
        private System.Drawing.Font m_fontTitle = new System.Drawing.Font("����", 18, FontStyle.Bold);
        /// <summary>
        /// ��ӡ�ı���Ŀ
        /// </summary>	
        public string m_strTitle;
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
        private System.Drawing.Font m_fontBody = new System.Drawing.Font("����", 10.5f);
        /// <summary>
        /// ��¼��ǰ��ӡ�߶�λ��
        /// </summary>	
        public int m_intLocationY = 0;
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
        private SizeF m_objsize;
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
        private float m_fltZiJiWide = 0;// �����������룺���
        ///<summary>
        ///�������и�
        ///</summary>
        private int m_ftlRowHeight = 20;
        ///<summary>
        ///�������п�
        ///</summary>
        private float m_fltAvgCol;

        #endregion
        /// <summary>
        /// ��ȡ��ӡ�޸ĺۼ�����
        /// </summary>
        private void m_mthGetPrintMarkConfig()
        {
            int intConfig = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_intGetEmrSettingValue("3012");
            if (intConfig == 0)
            {
                m_blnIsPrintMark = false;
            }
            else
            {
                m_blnIsPrintMark = true;
            }
        }

        #region ����:��ʼ��ÿһ�е�λ��
        /// <summary>
        /// ����:��ʼ��ÿһ�е�λ��
        /// </summary>		
        private void mthInitColLocation(PrintPageEventArgs e)
        {
            #region ���ô�ӡ�п���ÿ�еĺ�����

            m_fltAvgCol = 60;

            float fltCol = 60;
            m_fltFirstCol = 70; //��1�п������

            m_fltSeconCol = 30; //��2�п�Ȳ�������

            m_fltthCol = 30; //��3�п�ȹ���

            m_fltthirCol = 60; //��4�п���������

            m_fltFiveCol = 40; //��5�п������

            m_fltSixCol = 40; //��6�п������

            m_fltSenCol = 40; //��7�п����ͷ

            m_fltNigCol = 40; //��8�п����

            m_fltNiNeCol = 40; //��9�п��ɫ

            this.m_fltCol10 = 40;//��ζ
            this.m_fltCol11 = 80;//�������
            this.m_fltCol12 = 40;//BP
            this.m_fltCol13 = 40;//��
            this.m_fltCol14 = 105;//��ע
            this.m_fltCol15 = 65;//�����


            m_fltFirstColLeft = e.PageBounds.Left + 30; //��1��Left����
            //			m_fltFirstColLeft = clsPrintPosition.c_intLeftX - 90  ; //��1��Left����
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
            this.m_fltColLeft15 = m_fltColLeft14 + m_fltCol14;

            #endregion

            m_objsize = e.Graphics.MeasureString("����", this.m_fontBody);
            m_fltZiHeight = m_objsize.Height;// �ָ�


        }
        #endregion

        #region ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.
        /// <summary>
        /// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
        /// </summary>
        /// <param name="p_objPatient">����</param>
        /// <param name="p_dtmInPatientDate">��Ժ����</param>
        /// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            m_objPatient = p_objPatient;
            //m_dtInHos = p_dtmInPatientDate;
            //com.digitalwave.clsRecordsService.clsBloodCleanseRecord_MainService objServ =
            //    (com.digitalwave.clsRecordsService.clsBloodCleanseRecord_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsBloodCleanseRecord_MainService));

            //�������л�ȡ����ûɾ��������

            (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetTransDataInfoArr_factory(enmRecordsType.frmBloodCleanseRecord, p_objPatient.m_StrRegisterId, out objAr);
            if (objAr != null)
                m_mthSetPrintContent(objAr[0]);
            m_mthGetPrintMarkConfig();
        }

        #endregion

        #region �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
        /// </summary>
        public void m_mthInitPrintContent()
        {
            m_mthSetPrintValue();
        }
        #endregion

        #region ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// <summary>
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {
            m_objResult = p_objPrintContent as clsBloodCleanRecordValueContentDataInfo;
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
                if (m_objResult == null)
                {
                    MDIParent.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
                    return null;
                }
                return m_objResult;
            }
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

            PrintPageEventArgs e = (PrintPageEventArgs)p_objPrintArg;
            m_mthPrintTitleInfo(e);
            m_mthPrintFormTitleInfo(e, this.m_objPatient, ref this.m_intLocationY);
            //mthInitColLocation(e);
            m_mthPrintZhenDuan(e);
            if (m_intCurrentPageIndex == 1)
            {
                //m_mthPrintFormHeader(e, ref this.m_intLocationY);
            }
            m_mthPrintAllPage(e, ref this.m_intLocationY);
        }
        #endregion


        private void m_mthPrintZhenDuan(PrintPageEventArgs e)
        {
            e.HasMorePages = false;
            Font fntNormal = new Font("SimSun", 12);
            while (m_objPrintContext.m_BlnHaveMoreLine)
            {
                m_objPrintContext.m_mthPrintNextLine(ref m_intLocationY, e.Graphics, fntNormal);
                if (m_intLocationY >= (int)enmRectangleInfo.BottomY
                    && m_objPrintContext.m_BlnHaveMoreLine)
                {
                    #region ��ҳ����
                    e.HasMorePages = true;
                    //e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, 115, (int)enmRectangleInfo.LeftX, m_intYPos);
                    //e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, 115, (int)enmRectangleInfo.RightX, m_intYPos);
                    //e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, m_intYPos, (int)enmRectangleInfo.RightX, m_intYPos);

                    //m_intPages++;
                    //m_intYPos = (int)enmRectangleInfo.TopY + 20;
                    return;

                    #endregion ��ҳ����
                }

            }

            #region ���һҳ����
            //m_intYPos += 30;
            //e.Graphics.DrawString("ҽʦǩ��:", new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 440, m_intYPos);

            //if (m_objRecordContentOutIn24 != null)
            //{
            //    com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
            //    clsEmrEmployeeBase_VO objEmpVO = null;
            //    objEmployeeSign.m_lngGetEmpByNO(m_objRecordContentOutIn24.m_strDOCTORSIGN, out objEmpVO);
            //    if (objEmpVO != null)
            //        if (!(string.IsNullOrEmpty(objEmpVO.m_strGetTechnicalRankAndName)))
            //            e.Graphics.DrawString(objEmpVO.m_strGetTechnicalRankAndName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 440 + (int)(5f * 17.5f), m_intYPos);
            //}

            //m_intYPos += 30;
            //e.Graphics.DrawString("��¼����:", new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 440, m_intYPos);
            //if (m_objRecordContentOutIn24 != null)
            //    e.Graphics.DrawString(m_objRecordContentOutIn24.m_dtmRECORDDATE.ToString("yyyy��MM��dd��HHʱmm��"), new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 440 + (int)(5f * 17.5f), m_intYPos);

            //m_intYPos += 25;
            //if (m_intYPos < (int)enmRectangleInfo.BottomY)
            //    m_intYPos = (int)enmRectangleInfo.BottomY;
            //e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, 115, (int)enmRectangleInfo.LeftX, m_intYPos);
            //e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, 115, (int)enmRectangleInfo.RightX, m_intYPos);
            //e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, m_intYPos, (int)enmRectangleInfo.RightX, m_intYPos);

            #endregion ���һҳ����

            //m_intYPos += (int)enmRectangleInfo.RowStep + 15;
            //Font fntSign = new Font("", 6);
            //while (m_objPrintContext.m_BlnHaveMoreSign)
            //{
            //    m_objPrintContext.m_mthPrintNextSign((int)enmRectangleInfo.LeftX, m_intYPos, e.Graphics, fntSign);

            //    m_intYPos += (int)enmRectangleInfo.RowStep - 10;
            //}

            ////ȫ������
            //m_objPrintContext.m_mthReset();
            //m_intPages = 1;
            //m_intYPos = (int)enmRectangleInfo.TopY;
        }

        #region ��ӡÿҳ
        private void reset()
        {
            m_intRecordIndex = 0;
            m_blnOnlyPrintOnceHadPrintedPerson = false;
            m_blnOnlyPrintOnceHadPrinted = false;
            m_intCurrentPageIndex = 1;
            this.m_intLocationY = 0;
            intSub = 0;
            m_blnIsPrint1 = false;
            m_blnIsPrint2 = false;

        }
        private int i = 0;
        private int intSub = 0;
        private bool m_blnIsPrint1 = false;
        private bool m_blnIsPrint2 = false;

        private void m_mthPrintAllPage(System.Drawing.Printing.PrintPageEventArgs e, ref int p_objLocationY)
        {
            if (m_objResult == null)
                return;
            if (m_objResult.m_objDialyseRecordValues == null && m_objResult.m_objBloodCleanseBaseRecord == null)
                return;
            string print = "";

            //if (m_objResult.m_objDialyseRecordValues != null && m_objResult.m_objDialyseRecordValues.Length > 0)
            //{
            //    if (m_intCurrentPageIndex == 1)
            //    {

            //        for (i = 0; i < m_objResult.m_objDialyseRecordValues.Length; i++)
            //        {
            //            for (intSub = 0; intSub < m_objResult.m_objDialyseRecordValues[i].m_objRecordArr.Length; intSub++)
            //            {
            //                #region draw one row
            //                float fltRealHeight = 0f;
            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_dtmRecordDate.Date.ToString("yy/MM/dd");
            //                float fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltFirstColLeft, this.m_fltSeconColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strPOSTPORTUM_NUM_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltSeconColLeft, this.m_fltthColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strUTERUSBOTTOM_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltthColLeft, this.m_fltthirColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strUTERUSPINCH_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltthirColLeft, this.m_fltFiveColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strMILKNUM_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltFiveColLeft, this.m_fltSixColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strBREASTBULGE_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltSixColLeft, this.m_fltSenColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strNIPPLE_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltSenColLeft, this.m_fltNigColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strDEWNUM_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltNigColLeft, this.m_fltNiNeColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strDEWCOLOR_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltNiNeColLeft, this.m_fltColLeft10, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strDEWFUCK_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft10, this.m_fltColLeft11, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strPERINEUM_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft11, this.m_fltColLeft12, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strBP_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft12, this.m_fltColLeft13, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strURINE_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft13, this.m_fltColLeft14, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strANNOTATIONS_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft14, this.m_fltColLeft15, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = "";
            //                if (m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].objSignerArr != null && m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].objSignerArr.Length > 0)
            //                {
            //                    print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].objSignerArr[0].objEmployee.m_strGetTechnicalRankAndName;
            //                    for (int w1 = 1; w1 < m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].objSignerArr.Length; w1++)
            //                    {
            //                        print += ";" + m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].objSignerArr[w1].objEmployee.m_strGetTechnicalRankAndName;
            //                    }
            //                }
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft15, this.m_fltColLeft15 + m_fltCol15, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, m_ftlRowHeight);

            //                m_mthDrawLines(e, fltRealHeight);
            //                m_mthPrintHLine(e.Graphics, p_objLocationY, fltRealHeight);
            //                p_objLocationY += fltRealHeight;
            //                #endregion

            //                //�ж��Ƿ��ҳ
            //                if (Convert.ToInt32(p_objLocationY) >= e.MarginBounds.Bottom)
            //                {
            //                    m_intCurrentPageIndex++;
            //                    e.HasMorePages = true;0.
            //                    m_mthPrintFoot(e);
            //                    return;
            //                }
            //            }
            //            intSub = 0;
            //        }
            //    }
            //    else
            //    {
            //        int temp = i;

            //        #region draw one row

            //        for (; i < m_objResult.m_objDialyseRecordValues.Length; i++)
            //        {
            //            for (intSub = 0; intSub < m_objResult.m_objDialyseRecordValues[i].m_objRecordArr.Length; intSub++)
            //            {
            //                #region draw one row
            //                float fltRealHeight = 0f;
            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_dtmCreateDate.Date.ToString("yy/MM/dd");
            //                float fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltFirstColLeft, this.m_fltSeconColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strPOSTPORTUM_NUM_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltSeconColLeft, this.m_fltthColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strUTERUSBOTTOM_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltthColLeft, this.m_fltthirColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strUTERUSPINCH_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltthirColLeft, this.m_fltFiveColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strMILKNUM_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltFiveColLeft, this.m_fltSixColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strBREASTBULGE_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltSixColLeft, this.m_fltSenColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strNIPPLE_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltSenColLeft, this.m_fltNigColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strDEWNUM_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltNigColLeft, this.m_fltNiNeColLeft, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strDEWCOLOR_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltNiNeColLeft, this.m_fltColLeft10, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strDEWFUCK_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft10, this.m_fltColLeft11, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strPERINEUM_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft11, this.m_fltColLeft12, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strBP_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft12, this.m_fltColLeft13, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strURINE_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft13, this.m_fltColLeft14, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].m_strANNOTATIONS_CHR_RIGHT;
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft14, this.m_fltColLeft15, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

            //                print = "";
            //                if (m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].objSignerArr != null && m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].objSignerArr.Length > 0)
            //                {
            //                    print = m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].objSignerArr[0].objEmployee.m_strGetTechnicalRankAndName;
            //                    for (int w1 = 1; w1 < m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].objSignerArr.Length; w1++)
            //                    {
            //                        print += ";" + m_objResult.m_objDialyseRecordValues[i].m_objRecordArr[intSub].objSignerArr[w1].objEmployee.m_strGetTechnicalRankAndName;
            //                    }
            //                }
            //                fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft15, this.m_fltColLeft15 + m_fltCol15, print, p_objLocationY, e);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
            //                fltRealHeight = m_fltGetMaxValue(fltRealHeight, m_ftlRowHeight);
            //                #endregion

            //                m_mthDrawLines(e, fltRealHeight);
            //                m_mthPrintHLine(e.Graphics, p_objLocationY, fltRealHeight);

            //                p_objLocationY += fltRealHeight;
            //                //�ж��Ƿ��ҳ
            //                if (Convert.ToInt32(p_objLocationY) >= e.MarginBounds.Bottom)
            //                {
            //                    m_intCurrentPageIndex++;
            //                    e.HasMorePages = true;
            //                    m_mthPrintFoot(e);
            //                    return;
            //                }
            //            }
            //            intSub = 0;
            //        }

            //        #endregion
            //    }
            //}
            p_objLocationY += 20;

        }

        #endregion

        #region ����
        private void m_mthDrawLines(PrintPageEventArgs e, float p_fltHeight)
        {
            e.Graphics.DrawLine(this.m_objPen, this.m_fltFirstColLeft, this.m_intLocationY + p_fltHeight, this.m_fltColLeft15 + m_fltCol15, this.m_intLocationY + p_fltHeight);
        }
        #endregion

        //��ӡҳ��
        private void m_mthPrintFoot(PrintPageEventArgs e)
        {
            string str = "��" + this.m_intCurrentPageIndex.ToString() + "ҳ";
            SizeF s = e.Graphics.MeasureString(str, this.m_fontBody);
            float with = float.Parse(e.PageBounds.Width.ToString()) - s.Width;

            e.Graphics.DrawString(str, this.m_fontBody, this.m_objBrush, with / 2, float.Parse(e.MarginBounds.Bottom.ToString()));
        }
        /// <summary>
        /// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
        public void m_mthEndPrint(object p_objPrintArg)
        {
            if (this.m_objPatient != null)
            {
                //m_objInRoomSvc.m_lngUpdateALLFirstPrintDate(m_objPatient.m_StrInPatientID,m_dtInHos.ToString(),System.DateTime.Now);
            }
        }

        private com.digitalwave.Utility.Controls.clsPrintContext m_objPrintContext;

        #endregion
        // ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {

        }

        // ��ӡҳ
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {

        }


        #region ����ϣ������¼��ֵ
        private DateTime dtmFirstPrintTime;
        /// <summary>
        /// ����ϣ������¼��ֵ
        /// </summary>
        private void m_mthSetPrintValue()
        {
            #region  ��һ�δ�ӡʱ�丳ֵ
            dtmFirstPrintTime = DateTime.Now;
            if (m_objResult.m_objBloodCleanseBaseRecord != null && m_objResult.m_objBloodCleanseBaseRecord.m_dtmFirstPrintDate != DateTime.MinValue)
                dtmFirstPrintTime = m_objResult.m_objBloodCleanseBaseRecord.m_dtmFirstPrintDate;
            #endregion  ��һ�δ�ӡʱ�丳ֵ

            #region ��ӡ�г�ʼ��
            m_objLine2Arr = new clsPrint2[2];
            for (int i = 0; i < m_objLine2Arr.Length; i++)
                m_objLine2Arr[i] = new clsPrint2(m_objResult.m_objBloodCleanseBaseRecord == null ? "" : m_objResult.m_objBloodCleanseBaseRecord.m_strZHENDUAN_CHR, m_objResult.m_objBloodCleanseBaseRecord == null ? "" : m_objResult.m_objBloodCleanseBaseRecord.m_strZHENDUAN_CHRXML, true);

            m_objPrintContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		   m_objLine2Arr[0]//,m_objLine2Arr[1]                                               
																	   });
            m_objPrintContext.m_ObjPrintSign = new com.digitalwave.Utility.Controls.clsPrintRecordSign();
            #endregion

            #region ��ÿһ�е�Ԫ�ظ�ֵ
            if (m_objResult.m_objBloodCleanseBaseRecord != null)
            {
                Object[] objData1 = new object[4];
                ///////////////1��/////////////////				
                objData1[0] = m_objResult.m_objBloodCleanseBaseRecord.m_strZHENDUAN_CHR;
                objData1[1] = m_objResult.m_objBloodCleanseBaseRecord.m_strZHENDUAN_CHRXML;
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "�����:";
                m_objLine2Arr[0].m_ObjPrintLineInfo = objData1;

                ///////////////2��/////////////////				
                objData1[0] = m_objResult.m_objBloodCleanseBaseRecord.m_strHULIJILU_CHR;
                objData1[1] = m_objResult.m_objBloodCleanseBaseRecord.m_strHULIJILU_CHRXML;
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "�������¼:";
                m_objLine2Arr[1].m_ObjPrintLineInfo = objData1;
            }
            else
            {
                Object[] objData1 = new object[4];
                ///////////////1��/////////////////				
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "�����:";
                m_objLine2Arr[0].m_ObjPrintLineInfo = objData1;

                ///////////////2��/////////////////				
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "�������¼:";
                m_objLine2Arr[1].m_ObjPrintLineInfo = objData1;
            }
            #endregion
        }
        #endregion

        #region  �������ֲ���
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            SizeF objSize = g.MeasureString(this.m_strTitle, this.m_fontTitle);
            g.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, new Font("����", 15, FontStyle.Bold), m_objBrush, 320,70);
            g.DrawString(this.m_strTitle, this.m_fontTitle, m_objBrush, clsPrintPosition.c_intLeftX + (e.MarginBounds.Width - objSize.Width) / 2, e.MarginBounds.Top);
            this.m_intLocationY = e.MarginBounds.Top + 40;

        }
        #endregion

        #region  ����������ֲ���
        /// <summary>
        /// ����������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintFormTitleInfo(System.Drawing.Printing.PrintPageEventArgs e, clsPatient p_objPatient, ref int p_fltLocationY)
        {
            p_fltLocationY += m_ftlRowHeight;

            e.Graphics.DrawString("͸���ţ�" + (m_objResult.m_objBloodCleanseBaseRecord == null ? "" :m_objResult.m_objBloodCleanseBaseRecord.m_strTOUXIHAO), m_fontBody, m_objBrush, clsPrintPosition.c_intLeftX, p_fltLocationY);
            e.Graphics.DrawString("�� " + (m_objResult.m_objBloodCleanseBaseRecord == null ? "" : m_objResult.m_objBloodCleanseBaseRecord.m_strTOTALBLOODNUM_CHR), m_fontBody, m_objBrush, clsPrintPosition.c_intLeftX + 200, p_fltLocationY);
            e.Graphics.DrawString("��͸��", m_fontBody, m_objBrush, clsPrintPosition.c_intLeftX + 250, p_fltLocationY);
            e.Graphics.DrawString("͸�����ڣ�" + (m_objResult.m_objBloodCleanseBaseRecord == null ? "" : m_objResult.m_objBloodCleanseBaseRecord.m_strTOUXIRIQI_CHR), m_fontBody, m_objBrush, clsPrintPosition.c_intLeftX + 350, p_fltLocationY);
            e.Graphics.DrawString("סԺ�ţ�" + p_objPatient.m_StrHISInPatientID, m_fontBody, m_objBrush, clsPrintPosition.c_intLeftX + 570, p_fltLocationY);

            p_fltLocationY += m_ftlRowHeight;
            e.Graphics.DrawRectangle(m_objPen, clsPrintPosition.c_intLeftX, p_fltLocationY, clsPrintPosition.c_intRightX - clsPrintPosition.c_intLeftX, clsPrintPosition.c_intBottomY - p_fltLocationY);

            p_fltLocationY += m_ftlRowHeight;
            e.Graphics.DrawString("������" + p_objPatient.m_StrName, m_fontBody, m_objBrush, clsPrintPosition.c_intLeftX + 20, p_fltLocationY);
            e.Graphics.DrawString("�Ա�" + p_objPatient.m_StrSex, m_fontBody, m_objBrush, clsPrintPosition.c_intLeftX + 200, p_fltLocationY);
            e.Graphics.DrawString("���䣺" + p_objPatient.m_ObjPeopleInfo.m_StrAge, m_fontBody, m_objBrush, clsPrintPosition.c_intLeftX + 400, p_fltLocationY);
           
            p_fltLocationY += m_ftlRowHeight;
            e.Graphics.DrawString("��ϣ�" , m_fontBody, m_objBrush, clsPrintPosition.c_intLeftX + 20, p_fltLocationY);
           
        }
        #endregion

        #region ���ͷ
        private void m_mthPrintFormHeader(System.Drawing.Printing.PrintPageEventArgs e, ref int p_objLocationY)
        {
            p_objLocationY += m_ftlRowHeight;
            e.Graphics.DrawLine(this.m_objPen, m_fltFirstColLeft, p_objLocationY, m_fltColLeft15 + m_fltCol15, p_objLocationY);

            e.Graphics.DrawLine(this.m_objPen, m_fltFirstColLeft, p_objLocationY, m_fltFirstColLeft, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltFirstColLeft, this.m_fltSeconColLeft, "����", p_objLocationY + 15, e);

            e.Graphics.DrawLine(this.m_objPen, m_fltSeconColLeft, p_objLocationY, m_fltSeconColLeft, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(true, this.m_fltSeconColLeft, this.m_fltthColLeft, "����", p_objLocationY, e);
            m_fltDrawStrAtRectangle(true, this.m_fltSeconColLeft, this.m_fltthColLeft, "����", p_objLocationY + m_ftlRowHeight, e);

            e.Graphics.DrawLine(this.m_objPen, m_fltthColLeft, p_objLocationY, m_fltthColLeft, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltthColLeft, this.m_fltFiveColLeft, "�ӹ�", p_objLocationY + 3, e);
            m_fltDrawStrAtRectangle(true, this.m_fltthColLeft, this.m_fltthirColLeft, "����", p_objLocationY + m_ftlRowHeight, e);
            m_fltDrawStrAtRectangle(true, this.m_fltthColLeft, this.m_fltthirColLeft, "cm", p_objLocationY + m_ftlRowHeight  + 7, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltthirColLeft, p_objLocationY + m_ftlRowHeight, m_fltthirColLeft, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(true, this.m_fltthirColLeft, this.m_fltFiveColLeft, "�������", p_objLocationY + m_ftlRowHeight + 2, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltthColLeft, p_objLocationY + m_ftlRowHeight, m_fltFiveColLeft, p_objLocationY + m_ftlRowHeight);

            e.Graphics.DrawLine(this.m_objPen, m_fltFiveColLeft, p_objLocationY, m_fltFiveColLeft, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltFiveColLeft, this.m_fltNigColLeft, "����", p_objLocationY + 3, e);
            m_fltDrawStrAtRectangle(false, this.m_fltFiveColLeft, this.m_fltSixColLeft, "����", p_objLocationY + m_ftlRowHeight + 2, e);

            e.Graphics.DrawLine(this.m_objPen, m_fltSixColLeft, p_objLocationY + m_ftlRowHeight, m_fltSixColLeft, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltSixColLeft, this.m_fltSenColLeft, "����", p_objLocationY + m_ftlRowHeight + 2, e);

            e.Graphics.DrawLine(this.m_objPen, m_fltSenColLeft, p_objLocationY + m_ftlRowHeight, m_fltSenColLeft, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltSenColLeft, this.m_fltNigColLeft, "��ͷ", p_objLocationY + m_ftlRowHeight + 2, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltFiveColLeft, p_objLocationY + m_ftlRowHeight, m_fltNigColLeft, p_objLocationY + m_ftlRowHeight);


            e.Graphics.DrawLine(this.m_objPen, m_fltNigColLeft, p_objLocationY, m_fltNigColLeft, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltNigColLeft, this.m_fltColLeft11, "��¶", p_objLocationY + 3, e);
            m_fltDrawStrAtRectangle(false, this.m_fltNigColLeft, this.m_fltNiNeColLeft, "��", p_objLocationY + m_ftlRowHeight + 2, e);

            e.Graphics.DrawLine(this.m_objPen, m_fltNiNeColLeft, p_objLocationY + m_ftlRowHeight, m_fltNiNeColLeft, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltNiNeColLeft, this.m_fltColLeft10, "ɫ", p_objLocationY + m_ftlRowHeight + 2, e);

            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft10, p_objLocationY + m_ftlRowHeight, m_fltColLeft10, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft10, this.m_fltColLeft11, "��ζ", p_objLocationY + m_ftlRowHeight + 2, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltNigColLeft, p_objLocationY + m_ftlRowHeight, m_fltColLeft11, p_objLocationY + m_ftlRowHeight);

            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft11, p_objLocationY, m_fltColLeft11, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft11, this.m_fltColLeft12, "����", p_objLocationY + 2, e);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft11, this.m_fltColLeft12, "����", p_objLocationY + m_ftlRowHeight, e);

            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft12, p_objLocationY, m_fltColLeft12, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft12, this.m_fltColLeft13, "BP", p_objLocationY + 2, e);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft12, this.m_fltColLeft13, "mmHg", p_objLocationY + m_ftlRowHeight, e);

            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft13, p_objLocationY, m_fltColLeft13, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft13, this.m_fltColLeft14, "��", p_objLocationY + 15, e);

            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft14, p_objLocationY, m_fltColLeft14, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft14, this.m_fltColLeft15, "��ע", p_objLocationY + 15, e);

            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft15, p_objLocationY, m_fltColLeft15, p_objLocationY + m_ftlRowHeight * 2);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft15, this.m_fltColLeft15 + this.m_fltCol15, "�����", p_objLocationY + 15, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft15 + m_fltCol15, p_objLocationY, m_fltColLeft15 + m_fltCol15, p_objLocationY + m_ftlRowHeight * 2);

            p_objLocationY += m_ftlRowHeight * 2;
            e.Graphics.DrawLine(this.m_objPen, m_fltFirstColLeft, p_objLocationY, m_fltColLeft15 + m_fltCol15, p_objLocationY);


        }

        void m_mthPrintHLine(System.Drawing.Graphics g, int p_objLocationY, float fltRealHeight)
        {
            g.DrawLine(this.m_objPen, m_fltFirstColLeft, p_objLocationY, m_fltFirstColLeft, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltSeconColLeft, p_objLocationY, m_fltSeconColLeft, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltthColLeft, p_objLocationY, m_fltthColLeft, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltthirColLeft, p_objLocationY, m_fltthirColLeft, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltFiveColLeft, p_objLocationY, m_fltFiveColLeft, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltSixColLeft, p_objLocationY, m_fltSixColLeft, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltSenColLeft, p_objLocationY, m_fltSenColLeft, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltNigColLeft, p_objLocationY, m_fltNigColLeft, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltNiNeColLeft, p_objLocationY, m_fltNiNeColLeft, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft10, p_objLocationY, m_fltColLeft10, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft11, p_objLocationY, m_fltColLeft11, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft12, p_objLocationY, m_fltColLeft12, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft13, p_objLocationY, m_fltColLeft13, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft14, p_objLocationY, m_fltColLeft14, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft15, p_objLocationY, m_fltColLeft15, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft15 + m_fltCol15, p_objLocationY, m_fltColLeft15 + m_fltCol15, p_objLocationY + fltRealHeight);
        }
        #endregion


        private float m_fltDrawStrAtRectangle(bool p_blnIsMeasureH, float col1, float col2, string strPrint, float LocationY, System.Drawing.Printing.PrintPageEventArgs e)
        {
            RectangleF rect = new RectangleF(col1, LocationY + 2, col2 - col1, m_ftlRowHeight);
            System.Drawing.Font m_font = this.m_fontBody;
            SizeF s = e.Graphics.MeasureString(strPrint, m_font, Convert.ToInt32(rect.Width));//�ֿ�
            if (s.Height > rect.Height && p_blnIsMeasureH)
            {
                m_font = new System.Drawing.Font("����", 9);
                s = e.Graphics.MeasureString(strPrint, m_font, Convert.ToInt32(rect.Width));
                if (s.Height > rect.Height)
                    rect.Height = s.Height + 6;
            }
            else if (s.Height > rect.Height)
                rect.Height = s.Height + 6;
            StringFormat sf = new StringFormat(StringFormatFlags.FitBlackBox);
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(strPrint, m_font, this.m_objBrush, rect, sf);
            return rect.Height;
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
        private float m_fltGetMaxValue(float a, float b)
        {
            return a > b ? a : b;
        }
        private class clsPrint2 : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strAllText = "";
            private string strXml = "";
            public clsPrint2(string p_strText, string p_strXML, bool p_blnIsFirstPrint)
            {
                strAllText = p_strText;
                strXml = p_strXML;
                m_blnIsFirstPrint = p_blnIsFirstPrint;
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (strAllText == "")
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    if (m_blnIsPrintMark)
                    {
                        m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime,true);
                        m_mthAddSign2("���", m_objPrintContext.m_ObjModifyUserArr);
                    }
                    else
                        m_objPrintContext.m_mthSetContextWithAllCorrect(strAllText, strXml);
                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine(680, clsPrintPosition.c_intLeftX + 70, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_blnHaveMoreLine = true;
                }
                else
                {
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }
        }
    }
}
