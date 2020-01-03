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
    /// ��������������۲��¼  ��ӡ��ժҪ˵����
    /// </summary>
    public class clsPostartumSeeRecordPrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        private clsICUACAD_POSTPARTUMSEERECORDContentDataInfo[] m_objResultArr = null;
        private clsPatient m_objPatient = null;
        DateTime m_dtInHos;


        public clsPostartumSeeRecordPrintTool()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_strTitle = "��������������۲��¼";
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001")
                m_strTitle = "���������������̹۲��¼";

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

            //			double kk=(e.PageBounds.Width - 30)/15;
            //double kk=(Convert.ToDouble(e.PageBounds.Width - 250))/10.00;
            m_fltAvgCol = 60;//ת������λС��

            float fltCol = m_fltAvgCol;
            m_fltFirstCol = fltCol + 20; //��1�п������

            m_fltSeconCol = fltCol - 10; //��2�п��ʱ��

            m_fltthCol = fltCol; //��3�п��Ѫѹ

            m_fltthirCol = fltCol; //��4�п������

            m_fltFiveCol = fltCol; //��5�п������

            m_fltSixCol = fltCol + 20; //��6�п�ȹ���

            m_fltSenCol = fltCol; //��7�п�ȳ�Ѫ

            m_fltNigCol = fltCol; //��8�п����ˮ

            m_fltNiNeCol = fltCol; //��9�п��̥��

            this.m_fltCol10 = fltCol + 20;//���ڴ�С
            this.m_fltCol11 = 130;//ǩ��


            m_fltFirstColLeft = e.PageBounds.Left + 15; //��1��Left����
                                                        //			m_fltFirstColLeft = e.MarginBounds.Left - 90  ; //��1��Left����
            m_fltSeconColLeft = m_fltFirstCol + m_fltFirstColLeft; //��2��Left����
            m_fltthColLeft = m_fltSeconColLeft + m_fltSeconCol; //��3��Left����
            m_fltthirColLeft = m_fltthColLeft + m_fltthCol; //��4��Left����
            m_fltFiveColLeft = m_fltthirColLeft + m_fltthirCol; //��5��Left����
            m_fltSixColLeft = m_fltFiveColLeft + m_fltFiveCol; //��6��Left����
            m_fltSenColLeft = m_fltSixColLeft + m_fltSixCol; //��7��Left����
            m_fltNigColLeft = m_fltSenColLeft + m_fltSenCol; //��8��Left����
            m_fltNiNeColLeft = m_fltNigColLeft + m_fltNiNeCol; //��9��Left����
            this.m_fltColLeft10 = m_fltNiNeColLeft + m_fltNiNeCol;//���ڴ�СLeft����
            this.m_fltColLeft11 = m_fltColLeft10 + m_fltCol10;//ǩ��Left����
            this.m_fltColLeft12 = m_fltColLeft11 + m_fltCol11;

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
            m_dtInHos = p_dtmInPatientDate;

            //com.digitalwave.clsRecordsService.clsPostartumSeeRecordMainService objServ =
            //    (com.digitalwave.clsRecordsService.clsPostartumSeeRecordMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsPostartumSeeRecordMainService));

            //�������л�ȡ����ûɾ��������
            clsTransDataInfo[] objAr = null;
            (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetTransDataInfoArr_factory(enmRecordsType.PostartumSeeRecord, p_objPatient.m_StrRegisterId, out objAr);
            m_mthSetPrintContent(objAr);
        }

        public void m_mthSetRegisterIdAndDate(string p_strRegisterId, DateTime p_dtmCreatedDate)
        {
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

            PrintPageEventArgs e = (PrintPageEventArgs)p_objPrintArg;
            //			e.PageSettings.Landscape = true;
            //			e.PageSettings.Margins.Left = 0;
            //			e.PageSettings.Margins.Right = 0;
            //����
            //	e.Graphics.DrawRectangle(this.m_objPen,m_fltFirstColLeft,e.MarginBounds.Top,15*this.m_fltAvgCol,e.MarginBounds.Height);
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
        private void reset()
        {
            m_intRecordIndex = 0;
            m_blnOnlyPrintOnceHadPrintedPerson = false;
            m_blnOnlyPrintOnceHadPrinted = false;
            m_intCurrentPageIndex = 1;
            this.m_fltLocationY = 0;

        }
        private int i = 0;
        private int intSub = 0;
        //		private int intPageSize =3;

        private void m_mthPrintAllPage(System.Drawing.Printing.PrintPageEventArgs e, ref float p_objLocationY)
        {
            string print = "";
            if (m_objResultArr.Length <= 0)
                return;
            int intPrintCount = 0;

            //�����һҳ���ܴ�ӡ����������¼
            int intRowCount = Convert.ToInt32((float.Parse(e.MarginBounds.Height.ToString()) - p_objLocationY) / m_ftlRowHeight);
            intRowCount--; //��Ϊ����һ��λ������ӡҳ��,��ӡ ��ע ��������ʾ������Ϊ��1
                           //intRowCount--; //��Ϊ����һ��λ������ӡ"�����¼"

            if (m_intCurrentPageIndex == 1)
            {

                for (i = 0; i < m_objResultArr.Length; i++)
                {
                    clsICUACAD_POSTPARTUMSEERECORDContentDataInfo objRecord = m_objResultArr[i];
                    for (intSub = 0; intSub < objRecord.m_objRecordArr.Length; intSub++)
                    {
                        #region draw one row
                        float fltRealHeight = 0f;
                        print = objRecord.m_objRecordArr[intSub].m_dtmRecordDate.ToString("yy-MM-dd");
                        float fltHeight = m_fltDrawStrAtRectangle(false, this.m_fltFirstColLeft, this.m_fltSeconColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_dtmRecordDate.ToString("HH:mm");
                        fltHeight = m_fltDrawStrAtRectangle(false, this.m_fltSeconColLeft, this.m_fltthColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strBLOODPRESSURE_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltthColLeft, this.m_fltthirColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strBODYTEMPARTURE_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltthirColLeft, this.m_fltFiveColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strPULSE_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltFiveColLeft, this.m_fltSixColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strUTERUS_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltSixColLeft, this.m_fltSenColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strBLOODED_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltSenColLeft, this.m_fltNigColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strBREAKWATER_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltNigColLeft, this.m_fltNiNeColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strEMBRYO_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltNiNeColLeft, this.m_fltColLeft10, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strUTERUSSIZE_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft10, this.m_fltColLeft11, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = "";
                        if (objRecord.m_objRecordArr[intSub].objSignerArr != null && objRecord.m_objRecordArr[intSub].objSignerArr.Length > 0)
                        {
                            print = objRecord.m_objRecordArr[intSub].objSignerArr[0].objEmployee.m_strGetTechnicalRankAndName;
                            for (int w1 = 1; w1 < objRecord.m_objRecordArr[intSub].objSignerArr.Length; w1++)
                            {
                                print += ";" + objRecord.m_objRecordArr[intSub].objSignerArr[w1].objEmployee.m_strGetTechnicalRankAndName;
                            }
                        }
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft11, this.m_fltColLeft12, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, m_ftlRowHeight);
                        m_mthPrintHLine(e.Graphics, p_objLocationY, fltRealHeight);
                        m_mthDrawLines(e, fltRealHeight);


                        p_objLocationY += fltRealHeight;
                        intPrintCount++;
                        #endregion
                        //�ж��Ƿ��ҳ
                        if (intPrintCount >= intRowCount)
                        {
                            intPrintCount = 0;
                            m_intCurrentPageIndex++;
                            e.HasMorePages = true;
                            m_mthPrintFoot(e);
                            return;
                        }
                    }

                    intSub = 0;
                }
            }
            else
            {
                int temp = i;

                #region draw one row
                m_mthPrintFormHeader(e, ref p_objLocationY);//����ͷ
                for (; i < m_objResultArr.Length; i++)
                {
                    clsICUACAD_POSTPARTUMSEERECORDContentDataInfo objRecord = m_objResultArr[i];
                    for (; intSub < objRecord.m_objRecordArr.Length; intSub++)
                    {
                        #region draw one row
                        float fltRealHeight = 0f;
                        print = objRecord.m_objRecordArr[intSub].m_dtmCreateDate.ToString("yy-MM-dd");
                        float fltHeight = m_fltDrawStrAtRectangle(false, this.m_fltFirstColLeft, this.m_fltSeconColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_dtmCreateDate.ToString("HH:mm");
                        fltHeight = m_fltDrawStrAtRectangle(false, this.m_fltSeconColLeft, this.m_fltthColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strBLOODPRESSURE_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltthColLeft, this.m_fltthirColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strBODYTEMPARTURE_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltthirColLeft, this.m_fltFiveColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strPULSE_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltFiveColLeft, this.m_fltSixColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strUTERUS_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltSixColLeft, this.m_fltSenColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strBLOODED_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltSenColLeft, this.m_fltNigColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strBREAKWATER_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltNigColLeft, this.m_fltNiNeColLeft, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strEMBRYO_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltNiNeColLeft, this.m_fltColLeft10, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = objRecord.m_objRecordArr[intSub].m_strUTERUSSIZE_CHR;
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft10, this.m_fltColLeft11, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);

                        print = "";
                        if (objRecord.m_objRecordArr[intSub].objSignerArr != null && objRecord.m_objRecordArr[intSub].objSignerArr.Length > 0)
                        {
                            print = objRecord.m_objRecordArr[intSub].objSignerArr[0].objEmployee.m_strGetTechnicalRankAndName;
                            for (int w1 = 1; w1 < objRecord.m_objRecordArr[intSub].objSignerArr.Length; w1++)
                            {
                                print += ";" + objRecord.m_objRecordArr[intSub].objSignerArr[w1].objEmployee.m_strGetTechnicalRankAndName;
                            }
                        }
                        fltHeight = m_fltDrawStrAtRectangle(true, this.m_fltColLeft11, this.m_fltColLeft12, print, p_objLocationY, e);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, fltHeight);
                        fltRealHeight = m_fltGetMaxValue(fltRealHeight, m_ftlRowHeight);
                        #endregion

                        m_mthPrintHLine(e.Graphics, p_objLocationY, fltRealHeight);
                        m_mthDrawLines(e, fltRealHeight);

                        p_objLocationY += fltRealHeight;
                        intPrintCount++;
                        //�ж��Ƿ��ҳ
                        if (intPrintCount >= intRowCount)
                        {
                            intPrintCount = 0;
                            m_intCurrentPageIndex++;
                            e.HasMorePages = true;
                            m_mthPrintFoot(e);
                            return;
                        }
                    }
                    intSub = 0;
                }

                #endregion

            }
            m_mthPrintFoot(e);

        }

        #endregion

        #region �����һҳ�д�ӡ ��ע��
        //		private void m_mthPrintFuZhu(System.Drawing.Printing.PrintPageEventArgs e, float p_objLocationY)
        //		{
        //			string str = "��ע������24Сʱ�ܳ�Ѫ����"+this.m_strTOTALBLOODNUM_CHR+"ml,�����˿ڲ��ߣ����"+m_strSEWPIN_CHR+"�룬���ϼ���"+m_strPERIOD_CHR+"��";
        //			SizeF s = e.Graphics.MeasureString(str,this.m_fontBody);
        //			float with = float.Parse(e.PageBounds.Width.ToString()) - s.Width;
        //			e.Graphics.DrawString(str,this.m_fontBody,this.m_objBrush,this.m_fltFirstColLeft,p_objLocationY + m_fltZijiHeight/2);
        //			p_objLocationY += this.m_ftlRowHeight;
        //			e.Graphics.DrawString("�����¼��",this.m_fontBody,this.m_objBrush,this.m_fltFirstColLeft,p_objLocationY + m_fltZijiHeight/2);
        //
        //		}
        #endregion

        #region ����
        private void m_mthDrawLines(PrintPageEventArgs e, float p_fltHeight)
        {
            e.Graphics.DrawLine(this.m_objPen, this.m_fltFirstColLeft, this.m_fltLocationY + p_fltHeight, this.m_fltColLeft12, this.m_fltLocationY + p_fltHeight);
        }
        #endregion

        //��ӡҳ��
        private void m_mthPrintFoot(PrintPageEventArgs e)
        {
            string str = "�� " + this.m_intCurrentPageIndex.ToString() + " ҳ";
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
                //com.digitalwave.clsRecordsService.clsPostartumSeeRecordService objServ =
                //(com.digitalwave.clsRecordsService.clsPostartumSeeRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsPostartumSeeRecordService));

                (new weCare.Proxy.ProxyEmr()).Service.clsPostartumSeeRecordService_m_lngUpdateFirstPrintDate(m_objPatient.m_StrRegisterId, "1900-1-1", System.DateTime.Now);
            }
        }

        #endregion
        // ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {
            intSub = 0;
            i = 0;
        }

        // ��ӡҳ
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {

        }

        // ���ô�ӡ���ݡ�
        private void m_mthSetPrintContent(clsTransDataInfo[] p_objTransDataArr)
        {
            if (p_objTransDataArr == null)
            {
                clsPublicFunction.ShowInformationMessageBox("��ӡ��������!");
                return;
            }

            ArrayList arlTemp = new ArrayList();
            arlTemp.AddRange(p_objTransDataArr);
            m_objResultArr = (clsICUACAD_POSTPARTUMSEERECORDContentDataInfo[])arlTemp.ToArray(typeof(clsICUACAD_POSTPARTUMSEERECORDContentDataInfo));
        }

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
            g.DrawString(this.m_strTitle, this.m_fontTitle, m_objBrush, e.MarginBounds.Left + (e.MarginBounds.Width - objSize.Width) / 2, e.MarginBounds.Top);
            this.m_fltLocationY = e.MarginBounds.Top + objSize.Height + 15;

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
            int col = e.MarginBounds.Width / 4;
            float fltCol = float.Parse(col.ToString());//��ӡ�������е��п�
            System.Drawing.Graphics g = e.Graphics;

            strPrint = "����:" + p_objPatient.m_StrName.Trim();
            SizeF objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left, p_fltLocationY);

            strPrint = "����:" + p_objPatient.m_ObjPeopleInfo.m_IntAge.ToString().Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 1 - 20, p_fltLocationY);

            strPrint = "סԺ��:" + p_objPatient.m_StrHISInPatientID.Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 2 + 20, p_fltLocationY);
            strPrint = "����:" + p_objPatient.m_strBedCode.Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 3 + 25, p_fltLocationY);

            this.m_fltLocationY = p_fltLocationY + objSize.Height;

        }
        #endregion

        #region ���ͷ
        private void m_mthPrintFormHeader(System.Drawing.Printing.PrintPageEventArgs e, ref float p_objLocationY)
        {

            System.Drawing.Graphics g = e.Graphics;


            g.DrawLine(this.m_objPen, m_fltFirstColLeft, p_objLocationY, m_fltColLeft12, p_objLocationY);

            m_mthPrintHLine(e.Graphics, p_objLocationY, m_ftlRowHeight);
            //for (int i1 = 0 ; i1 < 12 ; i1++)
            //{
            //    //g.DrawLine(this.m_objPen, e.MarginBounds.Left + e.MarginBounds.Width/14 * i1,p_objLocationY, e.MarginBounds.Left + e.MarginBounds.Width/14 * i1, p_objLocationY +m_ftlRowHeight);
            //    //g.DrawLine(this.m_objPen, this.m_fltFirstColLeft + m_fltAvgCol * i1  ,p_objLocationY, m_fltFirstColLeft + m_fltAvgCol * i1 , p_objLocationY +m_ftlRowHeight * 2);
            //    g.DrawLine(this.m_objPen, this.m_fltFirstColLeft + m_fltAvgCol * i1, p_objLocationY, m_fltFirstColLeft + m_fltAvgCol * i1, p_objLocationY + m_ftlRowHeight);

            //}
            //			SizeF s = g.MeasureString("����",this.m_fontBody);
            //			float y = p_objLocationY + m_ftlRowHeight*2 /2 - s.Height/2;
            //			float y1 = p_objLocationY + m_ftlRowHeight /2 - s.Height/2;
            //			float y2 = p_objLocationY + m_ftlRowHeight + m_ftlRowHeight /2 - s.Height/2;

            m_fltDrawStrAtRectangle(false, this.m_fltFirstColLeft, this.m_fltSeconColLeft, "����", p_objLocationY + 2, e);

            m_fltDrawStrAtRectangle(false, this.m_fltSeconColLeft, this.m_fltthColLeft, "ʱ��", p_objLocationY + 2, e);

            m_fltDrawStrAtRectangle(false, this.m_fltthColLeft, this.m_fltthirColLeft, "Ѫѹ", p_objLocationY + 2, e);

            m_fltDrawStrAtRectangle(false, this.m_fltthirColLeft, this.m_fltFiveColLeft, "����", p_objLocationY + 2, e);

            m_fltDrawStrAtRectangle(false, this.m_fltFiveColLeft, this.m_fltSixColLeft, "����", p_objLocationY + 2, e);

            m_fltDrawStrAtRectangle(false, this.m_fltSixColLeft, this.m_fltSenColLeft, "����", p_objLocationY + 2, e);

            m_fltDrawStrAtRectangle(false, this.m_fltSenColLeft, this.m_fltNigColLeft, "��Ѫ", p_objLocationY + 2, e);

            m_fltDrawStrAtRectangle(false, this.m_fltNigColLeft, this.m_fltNiNeColLeft, "��ˮ", p_objLocationY + 2, e);

            m_fltDrawStrAtRectangle(false, this.m_fltNiNeColLeft, this.m_fltColLeft10, "̥��", p_objLocationY + 2, e);

            m_fltDrawStrAtRectangle(false, this.m_fltColLeft10, this.m_fltColLeft11, "���ڴ�С", p_objLocationY + 2, e);

            m_fltDrawStrAtRectangle(false, this.m_fltColLeft11, this.m_fltColLeft12, "ǩ��", p_objLocationY + 2, e);

            p_objLocationY += m_ftlRowHeight;
            g.DrawLine(this.m_objPen, m_fltFirstColLeft, p_objLocationY, m_fltColLeft11 + m_fltCol11, p_objLocationY);


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
            //float ji = col2 - col1;
            //float X =  col1 + ji/2 - s.Width/2;
            //float Y = LocationY + m_ftlRowHeight/2 - s.Height/2;
            e.Graphics.DrawString(strPrint, m_font, this.m_objBrush, rect, sf);
            return rect.Height;
        }

        // ��ӡ����ʱ�Ĳ���
        private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
            intSub = 0;
            i = 0;
        }
        private float m_fltGetMaxValue(float a, float b)
        {
            return a > b ? a : b;
        }

        void m_mthPrintHLine(System.Drawing.Graphics g, float p_objLocationY, float fltRealHeight)
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
        }

    }
}
