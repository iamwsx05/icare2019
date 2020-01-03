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
    /// �����������Ʊ��ӡ������ 
    /// </summary>
    public class clsEMR_GestationDiabetesCure_PrintTool_1 : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;


        private clsEMR_GestationDiabetesCureDataInfo m_objResult = null;
        private clsPatient m_objPatient = null;
        private clsTransDataInfo[] objAr = null;
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsEMR_GestationDiabetesCure_PrintTool_1()
        {
            //���ñ���
            m_strTitle = "�����������Ƽ�¼";

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

        #region ���ô�ӡ�п���ÿ�еĺ��������

        ///<summary>
        ///��������1�п��
        ///</summary>
        float m_fltCol1; //��1�п��
        ///<summary>
        ///��������2�п��
        ///</summary>
        float m_fltCol2; //��2�п��
        ///<summary>
        ///��������3�п��
        ///</summary>
        float m_fltCol3; //��3�п��
        ///<summary>
        ///��������4�п��
        ///</summary>
        float m_fltCol4; //��4�п��
        ///<summary>
        ///��������5�п��
        ///</summary>
        float m_fltCol5; //��5�п��
        ///<summary>
        ///��������6�п��
        ///</summary>
        float m_fltCol6; //��6�п��
        ///<summary>
        ///��������7�п��
        ///</summary>
        float m_fltCol7; //��7�п��
        ///<summary>
        ///��������8�п��
        ///</summary>
        float m_fltCol8; //��8�п��
        ///<summary>
        ///��������9�п��
        ///</summary>
        float m_fltCol9;
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
        ///��������16�п��
        ///</summary>
        float m_fltCol16;
        ///<summary>
        ///��������17�п��
        ///</summary>
        float m_fltCol17;

        ///<summary>
        ///��������1��Left����
        ///</summary>
        float m_fltColLeft1; //��1��Left����
        ///<summary>
        ///��������2��Left����
        ///</summary>
        float m_fltColLeft2; //��2��Left����
        ///<summary>
        ///��������3��Left����
        ///</summary>
        float m_fltColLeft3; //��3��Left����
        ///<summary>
        ///��������4��Left����
        ///</summary>
        float m_fltColLeft4; //��4��Left����
        ///<summary>
        ///��������5��Left����
        ///</summary>
        float m_fltColLeft5; //��5��Left����
        ///<summary>
        ///��������6��Left����
        ///</summary>
        float m_fltColLeft6; //��6��Left����
        ///<summary>
        ///��������7��Left����
        ///</summary>
        float m_fltColLeft7; //��7��Left����
        ///<summary>
        ///��������8��Left����
        ///</summary>
        float m_fltColLeft8; //��8��Left����
        ///<summary>
        ///��������9��Left����
        ///</summary>
        float m_fltColLeft9; //��9��Left����
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
        ///��������15��Left����
        ///</summary>
        float m_fltColLeft15;
        ///<summary>
        ///��������16��Left����
        ///</summary>
        float m_fltColLeft16;
        ///<summary>
        ///��������17��Left����
        ///</summary>
        float m_fltColLeft17;
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
        /// ��¼��ӡ�ĸ߶����λ
        /// </summary>	
        public float m_fltLocationY = 0;
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

            m_fltAvgCol = 60;

            float fltCol = 60;
            m_fltCol1 = 70; //��1�п��  ����

            m_fltCol2 = 30; //��2�п��  ����

            m_fltCol3 = 45; //��3�п��  ����

            m_fltCol4 = 45; //��4�п��  ��ʳ��

            m_fltCol5 = 40; //��5�п��  �ȵ�������IU����Ч��

            m_fltCol6 = 40; //��6�п��  �ȵ�������IU����Ч�� ��

            m_fltCol7 = 40; //��7�п��  �ȵ�������IU����Ч�� ��

            m_fltCol8 = 40; //��8�п��  �ȵ�������IU����Ч�� ��

            m_fltCol9 = 40; //��9�п��  Ѫ�Ƕ��� mmol/L���ո���

            m_fltCol10 = 40; //��10�п��  Ѫ�Ƕ��� mmol/L���緹  ǰ��

            m_fltCol11 = 40; //��11�п��  Ѫ�Ƕ��� mmol/L���緹  ��

            m_fltCol12 = 40; //��12�п��  Ѫ�Ƕ��� mmol/L���緹  ǰ��

            m_fltCol13 = 40; //��13�п��  Ѫ�Ƕ��� mmol/L���緹  ��

            m_fltCol14 = 40; //��14�п��  Ѫ�Ƕ��� mmol/L����  ǰ��

            m_fltCol15 = 40; //��15�п��  Ѫ�Ƕ��� mmol/L����  ��

            m_fltCol16 = 40;  //��16�п��  ��ͪ

            m_fltCol17 = 100;  //��17�п��  ǩ��


            //m_fltColLeft1 = e.MarginBounds.Left - 90  ; //��1��Left����
            m_fltColLeft1 = e.PageBounds.Left + 30; //��1��Left����
            m_fltColLeft2 = m_fltColLeft1 + m_fltCol1; //��2��Left����
            m_fltColLeft3 = m_fltColLeft2 + m_fltCol2; //��3��Left����
            m_fltColLeft4 = m_fltColLeft3 + m_fltCol3; //��4��Left����
            m_fltColLeft5 = m_fltColLeft4 + m_fltCol4; //��5��Left����
            m_fltColLeft6 = m_fltColLeft5 + m_fltCol5; //��6��Left����
            m_fltColLeft7 = m_fltColLeft6 + m_fltCol6; //��7��Left����
            m_fltColLeft8 = m_fltColLeft7 + m_fltCol7; //��8��Left����
            m_fltColLeft9 = m_fltColLeft8 + m_fltCol9; //��9��Left����
            m_fltColLeft10 = m_fltColLeft9 + m_fltCol9;  //��10��Left����
            m_fltColLeft11 = m_fltColLeft10 + m_fltCol10; //��11��Left����
            m_fltColLeft12 = m_fltColLeft11 + m_fltCol11; //��12��Left����
            m_fltColLeft13 = m_fltColLeft12 + m_fltCol12;  //��13��Left����
            m_fltColLeft14 = m_fltColLeft13 + m_fltCol13;  //��14��Left����
            m_fltColLeft15 = m_fltColLeft14 + m_fltCol14;  //��15��Left����
            m_fltColLeft16 = m_fltColLeft15 + m_fltCol15;  //��16��Left����
            m_fltColLeft17 = m_fltColLeft16 + m_fltCol16;  //��17��Left����
            #endregion

            m_objsize = e.Graphics.MeasureString("����", this.m_fontBody);
            m_fltZiHeight = m_objsize.Height;// �ָ�
            m_ftlRowHeight = m_fltZijiHeight + m_fltZiHeight;//�и�

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
            //com.digitalwave.clsRecordsService.clsEMR_GestationDiabetesCureMainServ objServ =
            //    (com.digitalwave.clsRecordsService.clsEMR_GestationDiabetesCureMainServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsEMR_GestationDiabetesCureMainServ));

            //�������л�ȡ����ûɾ��������

            (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetTransDataInfoArr_factory(enmRecordsType.EMR_GestationDiabetesCure, p_objPatient.m_StrRegisterId, out objAr);
                if (objAr != null)
                {
                    m_mthSetPrintContent(objAr[0]);
                }
           
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
            m_objResult = p_objPrintContent as clsEMR_GestationDiabetesCureDataInfo;
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
            //			e.PageSettings.Landscape = true;
            //			e.PageSettings.Margins.Left = 0;
            //			e.PageSettings.Margins.Right = 0;
            //����
            //	e.Graphics.DrawRectangle(this.m_objPen,m_fltColLeft1,e.MarginBounds.Top,15*this.m_fltAvgCol,e.MarginBounds.Height);
            //


            m_mthPrintTitleInfo(e);
            m_mthPrintFormTitleInfo(e, this.m_objPatient, ref this.m_fltLocationY);
            mthInitColLocation(e);
            if (m_intCurrentPageIndex == 1)
            {
                m_mthPrintFormHeader(e, ref this.m_fltLocationY);
            }
            m_mthPrintAllPage(e, ref this.m_fltLocationY);
        }

        #endregion

        #region ��ӡÿҳ
        /// <summary>
        /// ����
        /// </summary>
        private void reset()
        {
            m_intRecordIndex = 0;
            m_blnOnlyPrintOnceHadPrintedPerson = false;
            m_blnOnlyPrintOnceHadPrinted = false;
            m_intCurrentPageIndex = 1;
            this.m_fltLocationY = 0;
            intSub = 0;
            m_blnIsPrint1 = false;
            m_blnIsPrint2 = false;

        }
        private int i = 0;
        private int intSub = 0;
        private bool m_blnIsPrint1 = false;
        private bool m_blnIsPrint2 = false;
        /// <summary>
        /// ��ӡȫ��ҳ��
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p_objLocationY"></param>
        private void m_mthPrintAllPage(System.Drawing.Printing.PrintPageEventArgs e, ref float p_objLocationY)
        {
            if (m_objResult == null)
            {
                return;
            }
            
            string print = "";

            if (m_objResult.m_objRecordArr.Length > 0)
            {
                if (m_intCurrentPageIndex == 1)
                {
                    string strTemp = null;
                    for (i = 0; i < m_objResult.m_objRecordArr.Length; i++)
                    {
                        
                            #region �������
                            /////�������
                            float fltRealHeight = 0f;
                            //���� 1
                            if (strTemp == m_objResult.m_objRecordArr[i].m_dtmRecordDate.Date.ToString("yy/MM/dd")) 
                            {
                                print = " ";
                            }
                            else
                            {
                                print = m_objResult.m_objRecordArr[i].m_dtmRecordDate.Date.ToString("yy/MM/dd");
                                strTemp = m_objResult.m_objRecordArr[i].m_dtmRecordDate.Date.ToString("yy/MM/dd");
                            }
                            
                            float fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft1, this.m_fltColLeft2, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //���� 2
                            print = m_objResult.m_objRecordArr[i].m_strGestationWeeks_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft2, this.m_fltColLeft3, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //���� 3
                            print = m_objResult.m_objRecordArr[i].m_strAvoirdupois_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft3, this.m_fltColLeft4, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //��ʳ�� 4
                            print = m_objResult.m_objRecordArr[i].m_strStapleMeasure_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft4, this.m_fltColLeft5, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //�ȵ�������IU����Ч�� 5
                            print = m_objResult.m_objRecordArr[i].m_strInsulinLong_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft5, this.m_fltColLeft6, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //�ȵ�������IU����Ч�� �� 6
                            print = m_objResult.m_objRecordArr[i].m_strInsulinShortMorning_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft6, this.m_fltColLeft7, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //�ȵ�������IU����Ч�� �� 7
                            print = m_objResult.m_objRecordArr[i].m_strInsulinShortNoon_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft7, this.m_fltColLeft8, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //�ȵ�������IU����Ч�� �� 8
                            print = m_objResult.m_objRecordArr[i].m_strInsulinShortNight_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft8, this.m_fltColLeft9, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //Ѫ�Ƕ��� mmol/L���ո��� 9
                            print = m_objResult.m_objRecordArr[i].m_strBloodSugarLimosis_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft9, this.m_fltColLeft10, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //Ѫ�Ƕ��� mmol/L���緹  ǰ�� 10
                            print = m_objResult.m_objRecordArr[i].m_strBloodSugarBe_BF_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft10, this.m_fltColLeft11, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //Ѫ�Ƕ��� mmol/L���緹  ��11
                            print = m_objResult.m_objRecordArr[i].m_strBloodSugarAf_BF_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft11, this.m_fltColLeft12, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //Ѫ�Ƕ��� mmol/L���緹  ǰ�� 12
                            print = m_objResult.m_objRecordArr[i].m_strBloodSugarBe_Lun_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft12, this.m_fltColLeft13, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //Ѫ�Ƕ��� mmol/L���緹  �� 13
                            print = m_objResult.m_objRecordArr[i].m_strBloodSugarAf_Lun_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft13, this.m_fltColLeft14, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //Ѫ�Ƕ��� mmol/L����  ǰ�� 14
                            print = m_objResult.m_objRecordArr[i].m_strBloodSugarBe_Sup_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft14, this.m_fltColLeft15, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //Ѫ�Ƕ��� mmol/L����  �� 15
                            print = m_objResult.m_objRecordArr[i].m_strBloodSugarAf_Sup_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft15, this.m_fltColLeft16, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //��ͪ 16
                            print = m_objResult.m_objRecordArr[i].m_strUreaketone_right;
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft16, this.m_fltColLeft17, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            print = "";
                            //ǩ�� 17
                            for (int w1 = 0; w1 < m_objResult.m_objRecordArr[i].objSignerArr.Length; w1++)
                            {
                                if (m_objResult.m_objRecordArr[i].objSignerArr.Length == 1)
                                {
                                    print += m_objResult.m_objRecordArr[i].objSignerArr[w1].objEmployee.m_strGetTechnicalRankAndName;
                                }
                                else
                                {
                                    print +=";"+ m_objResult.m_objRecordArr[i].objSignerArr[w1].objEmployee.m_strGetTechnicalRankAndName;
                                }
                            }
                            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft17, this.m_fltColLeft17 + m_fltCol17, print, p_objLocationY, e);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            fltRealHeight = m_fltGetMaxValue(fltRealHeight, m_ftlRowHeight);


                            print = "";
                            //if (m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].objSignerArr != null && m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].objSignerArr.Length > 0)
                            //{
                            //    print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].objSignerArr[0].objEmployee.m_strGetTechnicalRankAndName;
                            //    for (int w1 = 1; w1 < m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].objSignerArr.Length; w1++)
                            //    {
                            //        print += ";" + m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].objSignerArr[w1].objEmployee.m_strGetTechnicalRankAndName;
                            //    }
                            //}
                            //fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft15, this.m_fltColLeft15 + m_fltCol15, print, p_objLocationY, e);
                            //fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                            //fltRealHeight = m_fltGetMaxValue(fltRealHeight, m_ftlRowHeight);

                            m_mthDrawLines(e, fltRealHeight);
                            m_mthPrintHLine(e.Graphics, p_objLocationY, fltRealHeight);
                            p_objLocationY += fltRealHeight;
                            #endregion

                            //�ж��Ƿ��ҳ
                            if (Convert.ToInt32(p_objLocationY) >= e.MarginBounds.Bottom)
                            {
                                m_intCurrentPageIndex++;
                                e.HasMorePages = true;
                                m_mthPrintFoot(e);
                                return;
                            }
                        
                        intSub = 0;
                    }
                }
                //else
                //{
                //    int temp = i;

                //    #region draw one row

                //    for (; i < m_objResult.m_objPostPartumValues.Length; i++)
                //    {
                //        for (intSub = 0; intSub < m_objResult.m_objPostPartumValues[i].m_objRecordArr.Length; intSub++)
                //        {
                //            #region draw one row
                //            float fltRealHeight = 0f;
                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_dtmCreateDate.Date.ToString("yy/MM/dd");
                //            float fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft1, this.m_fltColLeft2, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strPOSTPORTUM_NUM_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft2, this.m_fltColLeft3, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strUTERUSBOTTOM_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft3, this.m_fltColLeft4, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strUTERUSPINCH_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft4, this.m_fltColLeft5, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strMILKNUM_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft5, this.m_fltColLeft6, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strBREASTBULGE_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft6, this.m_fltColLeft7, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strNIPPLE_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft7, this.m_fltColLeft8, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strDEWNUM_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft8, this.m_fltColLeft9, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strDEWCOLOR_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft9, this.m_fltColLeft10, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strDEWFUCK_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft10, this.m_fltColLeft11, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strPERINEUM_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft11, this.m_fltColLeft12, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strBP_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft12, this.m_fltColLeft13, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strURINE_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft13, this.m_fltColLeft14, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].m_strANNOTATIONS_CHR_RIGHT;
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft14, this.m_fltColLeft15, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                //            print = "";
                //            if (m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].objSignerArr != null && m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].objSignerArr.Length > 0)
                //            {
                //                print = m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].objSignerArr[0].objEmployee.m_strGetTechnicalRankAndName;
                //                for (int w1 = 1; w1 < m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].objSignerArr.Length; w1++)
                //                {
                //                    print += ";" + m_objResult.m_objPostPartumValues[i].m_objRecordArr[intSub].objSignerArr[w1].objEmployee.m_strGetTechnicalRankAndName;
                //                }
                //            }
                //            fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft15, this.m_fltColLeft15 + m_fltCol15, print, p_objLocationY, e);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                //            fltRealHeight = m_fltGetMaxValue(fltRealHeight, m_ftlRowHeight);
                //            #endregion

                //            m_mthDrawLines(e, fltRealHeight);
                //            m_mthPrintHLine(e.Graphics, p_objLocationY, fltRealHeight);

                //            p_objLocationY += fltRealHeight;
                //            //�ж��Ƿ��ҳ
                //            if (Convert.ToInt32(p_objLocationY) >= e.MarginBounds.Bottom)
                //            {
                //                m_intCurrentPageIndex++;
                //                e.HasMorePages = true;
                //                m_mthPrintFoot(e);
                //                return;
                //            }
                //        }
                //        intSub = 0;
                //    }

                //    #endregion
                //}
            }
            p_objLocationY += 20;
            #region
            //if (m_objResult.m_objMannoValue != null)
            //{
            //    if (!m_blnIsPrint1)
            //    {
            //        //�ж��Ƿ��ҳ
            //        if ((Convert.ToInt32(p_objLocationY) + m_ftlRowHeight * 2) >= e.MarginBounds.Bottom)
            //        {
            //            m_intCurrentPageIndex++;
            //            e.HasMorePages = true;
            //            m_mthPrintFoot(e);
            //            return;
            //        }
            //        string str = "��ע����¼ʱ�䣺"+m_objResult.m_objMannoValue.m_dtmRecordDate.ToString("yyyy��MM��dd�� HH:mm:ss")+Environment.NewLine
            //            + "      ����24Сʱ�ܳ�Ѫ����" + m_objResult.m_objMannoValue.m_strTOTALBLOODNUM_CHR_RIGHT + "ml" + Environment.NewLine + "      �����˿ڲ��ߣ����"
            //            + m_objResult.m_objMannoValue.m_strSEWPIN_CHR_RIGHT + "�룬���ϼ���" + m_objResult.m_objMannoValue.m_strPERIOD_CHR_RIGHT + "��";
            //        e.Graphics.DrawString(str, this.m_fontBody, this.m_objBrush, this.m_fltColLeft1, p_objLocationY + 5);
            //        m_blnIsPrint1 = true;
            //        p_objLocationY += this.m_ftlRowHeight * 3;
            //    }
            //    if (!m_blnIsPrint2)
            //    {
            //        string str = "�����¼��(��¼��Ժʱ�������������ͻ�����)" + Environment.NewLine + "    " + m_objResult.m_objMannoValue.m_strESPECIALRECORD_CHR_RIGHT;
            //        RectangleF rect = new RectangleF(e.PageBounds.Left + 50, p_objLocationY, e.PageBounds.Right - (e.PageBounds.Left + 60), m_ftlRowHeight * 2);
            //        SizeF sz = e.Graphics.MeasureString(str, this.m_fontBody, Convert.ToInt32(rect.Width));
            //        //�ж��Ƿ��ҳ
            //        if ((Convert.ToInt32(p_objLocationY) + Convert.ToInt32(sz.Height)) >= e.MarginBounds.Bottom)
            //        {
            //            m_intCurrentPageIndex++;
            //            e.HasMorePages = true;
            //            m_mthPrintFoot(e);
            //            return;
            //        }
            //        if (sz.Height > rect.Height)
            //            rect.Height = sz.Height;
            //        e.Graphics.DrawString(str, this.m_fontBody, this.m_objBrush, rect);
            //        m_blnIsPrint2 = true;
            //        p_objLocationY += rect.Height + 10;
            //    }

            //    string m_strPrint = "��¼�ˣ�";
            //    e.Graphics.DrawString(m_strPrint, this.m_fontBody, this.m_objBrush, e.PageBounds.Left + 500, p_objLocationY);
            //    if (m_objResult.m_objMannoValue.objSignerArr != null && m_objResult.m_objMannoValue.objSignerArr.Length > 0)
            //    {
            //        clsEmrSigns_VO[] m_objSignArr = m_objResult.m_objMannoValue.objSignerArr;
            //        m_strPrint = "";
            //        for (int i = 0; i < m_objSignArr.Length; i++)
            //        {
            //            if (i < m_objSignArr.Length - 1)
            //            {
            //                m_strPrint += m_objSignArr[i].objEmployee.m_strLASTNAME_VCHR + "��\n";
            //            }
            //            else
            //            {
            //                m_strPrint += m_objSignArr[i].objEmployee.m_strLASTNAME_VCHR;
            //            }
            //        }
            //        e.Graphics.DrawString(m_strPrint, m_fontBody, m_objBrush, e.PageBounds.Left + 560, p_objLocationY);
            //    }

            //    m_mthPrintFoot(e);
            //}
            #endregion

        }

        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p_fltHeight"></param>
        private void m_mthDrawLines(PrintPageEventArgs e, float p_fltHeight)
        {
            e.Graphics.DrawLine(this.m_objPen, this.m_fltColLeft1, this.m_fltLocationY + p_fltHeight, this.m_fltColLeft17 + m_fltCol17, this.m_fltLocationY + p_fltHeight);
        }
        #endregion

        
        /// <summary>
        /// ��ӡҳ��
        /// </summary>
        /// <param name="e"></param>
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

        #endregion
        
        /// <summary>
        /// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {

        }

         
        /// <summary>
        /// ��ӡҳ
        /// </summary>
        /// <param name="p_objPrintPageArg"></param>
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {

        }

        /// <summary>
        /// ���ô�ӡ����
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <param name="p_objCircsContentArr"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        private void m_mthSetPrintContent(clsNewBabyInRoomRecord p_objContent, clsNewBabyCircsRecord[] p_objCircsContentArr, DateTime p_dtmFirstPrintDate)
        {

        }

        /// <summary>
        /// �ı��ӡ�ĵ���ɫ
        /// </summary>
        /// <param name="p_objclsInPatientCase"></param>
        /// <returns></returns>
        private clsNewBabyInRoomRecord m_objChangePrintTextColor(clsNewBabyInRoomRecord p_objclsInPatientCase)
        {
            if (p_objclsInPatientCase == null)
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
            g.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, new Font("����", 15, FontStyle.Bold), m_objBrush, 320,70);
            g.DrawString(this.m_strTitle, this.m_fontTitle, m_objBrush, e.MarginBounds.Left + (e.MarginBounds.Width - objSize.Width) / 2, e.MarginBounds.Top);
            this.m_fltLocationY = e.MarginBounds.Top + objSize.Height;

        }
        #endregion

        #region  ����������ֲ���
        /// <summary>
        /// ����������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintFormTitleInfo(System.Drawing.Printing.PrintPageEventArgs e, clsPatient p_objPatient, ref float p_fltLocationY)
        {
            string strPrint = "";
            int col = e.MarginBounds.Width / 6;
            float fltCol = float.Parse(col.ToString());//��ӡ�������е��п�
            System.Drawing.Graphics g = e.Graphics;
            g.DrawLine(this.m_objPen, m_fltColLeft1, p_fltLocationY, m_fltColLeft15 + m_fltCol15, p_fltLocationY);
            
            p_fltLocationY += m_ftlRowHeight;
            //����
            strPrint = "����:" + p_objPatient.m_StrName.Trim();
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left, p_fltLocationY);
            //�Ԅe
            strPrint = "�Ԅe:"+p_objPatient.m_StrSex.Trim();
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 1-20, p_fltLocationY);
            //����
            strPrint = "����:" + p_objPatient.m_ObjPeopleInfo.m_IntAge.ToString().Trim();
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 2-30, p_fltLocationY);
            //����clsPatientInBedInfo
            strPrint = "������:" + p_objPatient.m_strAreaNewID.ToString().Trim();
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 3-40, p_fltLocationY);
            //����
            strPrint = "����:" + p_objPatient.m_strBedCode.Trim();
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 4-20, p_fltLocationY);
            //סԺ��
            strPrint = "סԺ��:" + p_objPatient.m_StrHISInPatientID.Trim();
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 5-20, p_fltLocationY);

            p_fltLocationY += m_ftlRowHeight;
            //this.m_fltLocationY = p_fltLocationY;// +m_ftlRowHeight;

        }
        #endregion

        #region ���ͷ
        /// <summary>
        /// ���ͷ
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p_objLocationY"></param>
        private void m_mthPrintFormHeader(System.Drawing.Printing.PrintPageEventArgs e, ref float p_objLocationY)
        {
            p_objLocationY += m_ftlRowHeight;
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft1, p_objLocationY, m_fltColLeft17 + m_fltCol17, p_objLocationY);
            //���� 1
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft1, p_objLocationY, m_fltColLeft1, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft1, this.m_fltColLeft2, "��", p_objLocationY + 15, e);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft1, this.m_fltColLeft2, "��", p_objLocationY + 40, e);

            //���� 2
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft2, p_objLocationY, m_fltColLeft2, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft2, this.m_fltColLeft3, "��", p_objLocationY + 15, e);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft2, this.m_fltColLeft3, "��", p_objLocationY + 40, e);
            //���� 3
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft3, p_objLocationY, m_fltColLeft3, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft3, this.m_fltColLeft4, "��", p_objLocationY + 10, e);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft3, this.m_fltColLeft4, "��", p_objLocationY + 30, e);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft3, this.m_fltColLeft4, "kg", p_objLocationY + 50, e);
            //��ʳ�� 4
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft4, p_objLocationY, m_fltColLeft4, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft4, this.m_fltColLeft5, "��", p_objLocationY + 5, e);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft4, this.m_fltColLeft5, "ʳ", p_objLocationY + 20, e);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft4, this.m_fltColLeft5, "��", p_objLocationY + 35, e);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft4, this.m_fltColLeft5, "(��)", p_objLocationY + 50, e);
            //�ȵ�������IU 5 - 8
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft5, p_objLocationY, m_fltColLeft5, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft5, this.m_fltColLeft9, "�ȵ�������IU", p_objLocationY + 4, e);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft5, this.m_fltColLeft6, "��", p_objLocationY + m_ftlRowHeight + 10, e);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft5, this.m_fltColLeft6, "Ч", p_objLocationY + m_ftlRowHeight + 30, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft6, p_objLocationY + m_ftlRowHeight, m_fltColLeft6, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft6, this.m_fltColLeft9, "��  Ч", p_objLocationY + m_ftlRowHeight + 3, e);

            m_fltDrawStrAtRectangle(false, this.m_fltColLeft6, this.m_fltColLeft7, "��", p_objLocationY + m_ftlRowHeight * 2, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft7, p_objLocationY + m_ftlRowHeight*2, m_fltColLeft7, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft7, this.m_fltColLeft8, "��", p_objLocationY + m_ftlRowHeight * 2, e);

            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft8, p_objLocationY + m_ftlRowHeight*2, m_fltColLeft8, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft8, this.m_fltColLeft9, "��", p_objLocationY + m_ftlRowHeight * 2, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft5, p_objLocationY + m_ftlRowHeight, m_fltColLeft9, p_objLocationY + m_ftlRowHeight);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft6, p_objLocationY + m_ftlRowHeight * 2, m_fltColLeft9, p_objLocationY + m_ftlRowHeight*2);

            //Ѫ�Ƕ��� 9 - 15
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft9, p_objLocationY, m_fltColLeft9, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft9, this.m_fltColLeft16, "Ѫ�Ƕ��� mmol/L", p_objLocationY + 4, e);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft9, this.m_fltColLeft10, "��", p_objLocationY + m_ftlRowHeight + 10, e);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft9, this.m_fltColLeft10, "��", p_objLocationY + m_ftlRowHeight + 30, e); 
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft10, p_objLocationY + m_ftlRowHeight, m_fltColLeft10, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft10, this.m_fltColLeft12, "�� ��", p_objLocationY + m_ftlRowHeight + 3, e);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft10, this.m_fltColLeft11, "ǰ", p_objLocationY + m_ftlRowHeight * 2, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft11, p_objLocationY + m_ftlRowHeight * 2, m_fltColLeft11, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft11, this.m_fltColLeft12, "��", p_objLocationY + m_ftlRowHeight * 2, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft12, p_objLocationY + m_ftlRowHeight, m_fltColLeft12, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft12, this.m_fltColLeft14, "�� ��", p_objLocationY + m_ftlRowHeight + 3, e);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft12, this.m_fltColLeft13, "ǰ", p_objLocationY + m_ftlRowHeight * 2, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft13, p_objLocationY + m_ftlRowHeight * 2, m_fltColLeft13, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft13, this.m_fltColLeft14, "��", p_objLocationY + m_ftlRowHeight * 2, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft14, p_objLocationY + m_ftlRowHeight, m_fltColLeft14, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft14, this.m_fltColLeft16, "�� ��", p_objLocationY + m_ftlRowHeight + 3, e);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft14, this.m_fltColLeft15, "ǰ", p_objLocationY + m_ftlRowHeight * 2, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft15, p_objLocationY + m_ftlRowHeight * 2, m_fltColLeft15, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(false, this.m_fltColLeft15, this.m_fltColLeft16, "��", p_objLocationY + m_ftlRowHeight * 2, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft9, p_objLocationY + m_ftlRowHeight, m_fltColLeft16, p_objLocationY + m_ftlRowHeight);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft10, p_objLocationY + m_ftlRowHeight * 2, m_fltColLeft16, p_objLocationY + m_ftlRowHeight * 2);
            


            //��ͪ
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft16, p_objLocationY, m_fltColLeft16, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft16, this.m_fltColLeft17, "��", p_objLocationY + 15, e);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft16, this.m_fltColLeft17, "ͪ", p_objLocationY + 40, e);
            ////ǩ��
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft17, p_objLocationY, m_fltColLeft17, p_objLocationY + m_ftlRowHeight * 3);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft17, this.m_fltColLeft17 + this.m_fltCol17, "ǩ", p_objLocationY + 15, e);
            m_fltDrawStrAtRectangle(true, this.m_fltColLeft17, this.m_fltColLeft17 + this.m_fltCol17, "��", p_objLocationY + 40, e);
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft17 + m_fltCol17, p_objLocationY, m_fltColLeft17 + m_fltCol17, p_objLocationY + m_ftlRowHeight * 3);

           
            p_objLocationY += m_ftlRowHeight * 3;
            e.Graphics.DrawLine(this.m_objPen, m_fltColLeft1, p_objLocationY, m_fltColLeft17 + m_fltCol17, p_objLocationY);


        }
        /// <summary>
        /// ��H��
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p_objLocationY"></param>
        /// <param name="fltRealHeight"></param>
        void m_mthPrintHLine(System.Drawing.Graphics g, float p_objLocationY, float fltRealHeight)
        {
            g.DrawLine(this.m_objPen, m_fltColLeft1, p_objLocationY, m_fltColLeft1, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft2, p_objLocationY, m_fltColLeft2, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft3, p_objLocationY, m_fltColLeft3, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft4, p_objLocationY, m_fltColLeft4, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft5, p_objLocationY, m_fltColLeft5, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft6, p_objLocationY, m_fltColLeft6, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft7, p_objLocationY, m_fltColLeft7, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft8, p_objLocationY, m_fltColLeft8, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft9, p_objLocationY, m_fltColLeft9, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft10, p_objLocationY, m_fltColLeft10, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft11, p_objLocationY, m_fltColLeft11, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft12, p_objLocationY, m_fltColLeft12, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft13, p_objLocationY, m_fltColLeft13, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft14, p_objLocationY, m_fltColLeft14, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft15, p_objLocationY, m_fltColLeft15, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft16, p_objLocationY, m_fltColLeft16, p_objLocationY + fltRealHeight);
            g.DrawLine(this.m_objPen, m_fltColLeft17, p_objLocationY, m_fltColLeft17, p_objLocationY + fltRealHeight);

            g.DrawLine(this.m_objPen, m_fltColLeft17 + m_fltCol17, p_objLocationY, m_fltColLeft17 + m_fltCol17, p_objLocationY + fltRealHeight);
        }
        #endregion

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="p_blnIsMeasureH"></param>
        /// <param name="col1"></param>
        /// <param name="col2"></param>
        /// <param name="strPrint"></param>
        /// <param name="LocationY"></param>
        /// <param name="e"></param>
        /// <returns></returns>
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
            {
                rect.Height = s.Height + 6;
            }
            StringFormat sf = new StringFormat(StringFormatFlags.FitBlackBox);
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(strPrint, m_font, this.m_objBrush, rect, sf);
            return rect.Height;
        }


        /// <summary>
        /// ��ӡ����ʱ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
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
        /// <summary>
        /// ȡ���ֵ
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private float m_fltGetMaxValue(float a, float b)
        {
            return a > b ? a : b;
        }

    }
}
