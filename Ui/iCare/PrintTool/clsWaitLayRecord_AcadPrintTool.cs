
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
    /// �����¼��ӡ������ 
    /// </summary>
    public class clsWaitLayRecord_AcadPrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        private clsPrintInfo_InPatientCaseHistory m_objPrintInfo;
        private clsBaseCaseHistoryDomain m_objRecordsDomain;
        public string m_strLaycount = ""; //����
        public string m_strBirthDate = "";// ����
        private com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
        //private com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadService m_objInRoomSvc;
        private clsIcuAcad_WaitLayRecord[] m_objResultArr = null;
        private clsPatient m_objPatient = null;
        DateTime m_dtInHos;

        public clsWaitLayRecord_AcadPrintTool()
        {
            //m_objInRoomSvc = new com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadService();
            m_strTitle = "�� �� �� ¼";

        }

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
        ///��������14��rigth����
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
        private System.Drawing.Font m_fontBody = new System.Drawing.Font("����", 10);
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
        /// <summary>
        /// ҽԺ����(���壬�Ӵ֣�С��)
        /// </summary>
        private Font m_fotHospitalFont;

        #endregion

        #region ����:��ʼ��ÿһ�е�λ��
        /// <summary>
        /// ����:��ʼ��ÿһ�е�λ��
        /// </summary>		
        private void mthInitColLocation(PrintPageEventArgs e)
        {
            #region ���ô�ӡ�п���ÿ�еĺ�����

            //			int col  = (e.MarginBounds.Width)/14 ;
            int col = (e.PageBounds.Width - 20) / 15;
            m_fltAvgCol = col;
            //			int col = e.MarginBounds.Width/14;
            float fltCol = float.Parse(col.ToString());
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

            //			m_fltFirstColLeft = e.MarginBounds.Left - 10 ; //��1��Left����
            m_fltFirstColLeft = e.PageBounds.Left + 40; //��1��Left����
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
            //�������л�ȡ����ûɾ��������
            //com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadService m_objInRoomSvc =
            //    (com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadService));

            (new weCare.Proxy.ProxyEmr()).Service.m_lngGetAllMainRecord(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out m_objResultArr);

            //clsRecordsDomain m_objRecordDomain = new clsRecordsDomain(enmRecordsType.WaitLayRecord_Acad);
            //m_objRecordDomain.m_lngGetTransDataInfoArr(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out m_objResultArr);

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
            #region �йش�ӡ��ʼ��
            //				
            //			m_fotTitleFont = new Font("SimSun", 16,FontStyle.Bold);
            //			m_fotHeaderFont = new Font("SimSun", 18,FontStyle.Bold);
            //			m_fotItemHead = new Font("",13,FontStyle.Bold);
            //			m_fotSmallFont = new Font("SimSun",12);
            //			m_GridPen = new Pen(Color.Black,2);
            //			m_slbBrush = new SolidBrush(Color.Black);
            //			m_objPageSetting = new clsPrintPageSettingForRecord();
            m_fotHospitalFont = new Font("����", 15, FontStyle.Bold);
            #endregion �йش�ӡ��ʼ��
        }

        #endregion

        #region �ͷŴ�ӡ����
        /// <summary>
        /// �ͷŴ�ӡ����
        /// </summary>
        public void m_mthDisposePrintTools(object p_objArg)
        {
            //			m_fotTitleFont.Dispose() ;
            //			m_fotHeaderFont.Dispose() ;
            //			m_fotSmallFont.Dispose() ;
            //			m_GridPen.Dispose() ;
            //			m_slbBrush.Dispose() ;
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
            //e.PageSettings.Margins.Left = 0;
            //e.PageSettings.Margins.Right = 0;
            m_mthPrintTitleInfo(e);
            m_mthPrintFormTitleInfo(e, this.m_objPatient, ref this.m_fltLocationY);
            mthInitColLocation(e);
            m_mthPrintFormHeader(e, ref this.m_fltLocationY);
            m_mthPrintAllPage(e, ref this.m_fltLocationY);
        }

        #endregion

        #region ��ӡÿҳ
        private void reset()
        {
            m_intCurrentPageIndex = 1;
            this.m_fltLocationY = 0;

        }
        private int i = 0;
        //		private int intPageSize =3;

        private void m_mthPrintAllPage(System.Drawing.Printing.PrintPageEventArgs e, ref float p_objLocationY)
        {
            string print = "";
            if (m_objResultArr.Length <= 0)
                return;

            //�����һҳ���ܴ�ӡ����������¼
            int intRowCount = Convert.ToInt32((float.Parse(e.MarginBounds.Height.ToString()) - p_objLocationY) / m_ftlRowHeight);
            intRowCount--; //��Ϊ����һ��λ������ӡҳ�� ��������ʾ������Ϊ��1

            if (m_intCurrentPageIndex == 1)
            {

                for (i = 0; i < m_objResultArr.Length && i <= intRowCount; i++)
                {

                    #region draw one row
                    print = m_objResultArr[i].m_dtmRecordDate.Date.ToString("yy/MM/dd");
                    m_mthDrawStrAtRectangle(this.m_fltFirstColLeft, this.m_fltSeconColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_dtmRecordDate.ToShortTimeString();
                    m_mthDrawStrAtRectangle(this.m_fltSeconColLeft, this.m_fltthColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strBloodPressure_chr + "/" + m_objResultArr[i].m_strBloodPressure2_chr;
                    m_mthDrawStrAtRectangle(this.m_fltthColLeft, this.m_fltthirColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strEmbryoLocation_chr;
                    m_mthDrawStrAtRectangle(this.m_fltthirColLeft, this.m_fltFiveColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strEmbryoHeart_chr;
                    m_mthDrawStrAtRectangle(this.m_fltFiveColLeft, this.m_fltSixColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strIntermission_chr;
                    m_mthDrawStrAtRectangle(this.m_fltSixColLeft, this.m_fltSenColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strPersist_chr;
                    m_mthDrawStrAtRectangle(this.m_fltSenColLeft, this.m_fltNigColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strIntensity_chr;
                    m_mthDrawStrAtRectangle(this.m_fltNigColLeft, this.m_fltNiNeColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strPalaceMouth_chr;
                    m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft, this.m_fltColLeft10, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strShow_chr;
                    m_mthDrawStrAtRectangle(this.m_fltColLeft10, this.m_fltColLeft11, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strCaul_chr;
                    m_mthDrawStrAtRectangle(this.m_fltColLeft11, this.m_fltColLeft12, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strAnusCheck_chr;
                    m_mthDrawStrAtRectangle(this.m_fltColLeft12, this.m_fltColLeft13, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strOther_chr;
                    m_mthDrawStrAtRectangle(this.m_fltColLeft13, this.m_fltColLeft14, print, p_objLocationY, e);

                    clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
                    objEmployeeSign.m_lngGetEmpByID(m_objResultArr[i].m_strScrutator_chr, out objEmpVO);
                    if (objEmpVO == null)
                        print = m_objResultArr[i].m_strScrutator_chr;
                    else
                        print = objEmpVO.m_strLASTNAME_VCHR;
                    m_mthDrawStrAtRectangle(this.m_fltColLeft14, this.m_fltColLeft15, print, p_objLocationY, e);

                    m_mthDrawLines(e);

                    p_objLocationY += this.m_ftlRowHeight;
                    #endregion

                }
                m_mthPrintFoot(e);
                //�ж��Ƿ��ҳ
                if (i < this.m_objResultArr.Length - 1)
                {
                    m_intCurrentPageIndex++;
                    e.HasMorePages = true;
                }
            }
            else
            {
                int temp = i;

                #region draw one row

                for (; i < m_objResultArr.Length && i < intRowCount + temp; i++)
                {

                    print = m_objResultArr[i].m_dtmCreateDate.Date.ToString("yy/MM/dd");
                    m_mthDrawStrAtRectangle(this.m_fltFirstColLeft, this.m_fltSeconColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_dtmCreateDate.ToShortTimeString();
                    m_mthDrawStrAtRectangle(this.m_fltSeconColLeft, this.m_fltthColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strBloodPressure_chr + "/" + m_objResultArr[i].m_strBloodPressure2_chr;
                    m_mthDrawStrAtRectangle(this.m_fltthColLeft, this.m_fltthirColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strEmbryoLocation_chr;
                    m_mthDrawStrAtRectangle(this.m_fltthirColLeft, this.m_fltFiveColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strEmbryoHeart_chr;
                    m_mthDrawStrAtRectangle(this.m_fltFiveColLeft, this.m_fltSixColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strIntermission_chr;
                    m_mthDrawStrAtRectangle(this.m_fltSixColLeft, this.m_fltSenColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strPersist_chr;
                    m_mthDrawStrAtRectangle(this.m_fltSenColLeft, this.m_fltNigColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strIntensity_chr;
                    m_mthDrawStrAtRectangle(this.m_fltNigColLeft, this.m_fltNiNeColLeft, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strAnusCheck_chr;
                    m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft, this.m_fltColLeft10, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strShow_chr;
                    m_mthDrawStrAtRectangle(this.m_fltColLeft10, this.m_fltColLeft11, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strCaul_chr;
                    m_mthDrawStrAtRectangle(this.m_fltColLeft11, this.m_fltColLeft12, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strAnusCheck_chr;
                    m_mthDrawStrAtRectangle(this.m_fltColLeft12, this.m_fltColLeft13, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strOther_chr;
                    m_mthDrawStrAtRectangle(this.m_fltColLeft13, this.m_fltColLeft14, print, p_objLocationY, e);

                    print = m_objResultArr[i].m_strScrutator_chr_RIGHT;
                    clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
                    objEmployeeSign.m_lngGetEmpByID(m_objResultArr[i].m_strScrutator_chr, out objEmpVO);
                    if (objEmpVO == null)
                        print = m_objResultArr[i].m_strScrutator_chr;
                    else
                        print = objEmpVO.m_strLASTNAME_VCHR;
                    m_mthDrawStrAtRectangle(this.m_fltColLeft14, this.m_fltColLeft15, print, p_objLocationY, e);

                    m_mthDrawLines(e);

                    p_objLocationY += this.m_ftlRowHeight;

                }

                #endregion

                m_mthPrintFoot(e);

                //�ж��Ƿ��ҳ

                if (intRowCount < m_objResultArr.Length - i - 1)
                {
                    m_intCurrentPageIndex++;
                    e.HasMorePages = true;
                    return;
                }

            }
        }

        #endregion


        #region ����
        private void m_mthDrawLines(PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i1 = 0; i1 < 15; i1++)
            {
                g.DrawLine(this.m_objPen, this.m_fltFirstColLeft + this.m_fltAvgCol * i1, this.m_fltLocationY, this.m_fltFirstColLeft + this.m_fltAvgCol * i1, this.m_fltLocationY + this.m_ftlRowHeight);
            }
            g.DrawLine(this.m_objPen, this.m_fltFirstColLeft, this.m_fltLocationY + this.m_ftlRowHeight, this.m_fltColLeft15, this.m_fltLocationY + this.m_ftlRowHeight);


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
                //com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadService m_objInRoomSvc =
                //    (com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadService));

                (new weCare.Proxy.ProxyEmr()).Service.clsWaitLayRecord_AcadService_m_lngUpdateALLFirstPrintDate(m_objPatient.m_StrInPatientID, m_dtInHos.ToString(), System.DateTime.Now);
            }

            //			m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
            //			if(m_blnIsFromDataSource==false || m_objPrintInfo.m_strInPatentID=="") 
            //				return; 
            //			//�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            //			if(!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_blnIsFirstPrint)
            //			{
            //				m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss")  ,m_objPrintInfo.m_dtmFirstPrintDate);
            //			}
        }

        #region �йش�ӡ������

        /// <summary>
        /// ��ӡһ�е�����
        /// </summary>
        private com.digitalwave.Utility.Controls.clsPrintContext m_objPrintLineContext;

        /// <summary>
        /// ��ӡ�߿����߾�
        /// </summary>
        private const int m_intRecBaseX = clsPrintPosition.c_intLeftX;
        private int m_intCurrentPage = 1;
        /// <summary>
        /// ���������(20 bold)
        /// </summary>
        private Font m_fotTitleFont = new Font("SimSun", 16, FontStyle.Bold);
        /// <summary>
        /// ��ͷ������(14 )
        /// </summary>
        private Font m_fotHeaderFont;
        /// <summary>
        /// ����Ŀ�ı��⣬�������
        /// </summary>
        public static Font m_fotItemHead;
        /// <summary>
        /// �����ݵ�����(11)
        /// </summary>
        private Font m_fotSmallFont;
        /// <summary>
        /// �߿򻭱�
        /// </summary>
        private Pen m_GridPen;
        /// <summary>
        /// ˢ��
        /// </summary>
        private SolidBrush m_slbBrush = new SolidBrush(Color.Black);
        /// <summary>
        /// ��ǰ��ӡλ�ã�Y��
        /// </summary>
        private int m_intYPos = 155;//= (int)enmRectangleInfo.TopY+5;

        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        public enum enmRectangleInfo
        {

            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 150,
            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = clsPrintPosition.c_intLeftX,
            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = clsPrintPosition.c_intRightX,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 25,
            SmallRowStep = 25,
            /// <summary>
            /// ���ӵ�����
            /// </summary>
            RowLinesNum = 32,

            ColumnsMark1 = 35,

            /// <summary>
            /// CheckBoxƫ���ұ��ı��ľ���
            /// </summary>
            CheckShift = 15,

            /// <summary>
            /// �׻���ƫ���ı�����ľ���
            /// </summary>
            BottomLineShift = 15,

            BottomY = 1025

        }

        #endregion
        // ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {

        }

        // ��ӡҳ
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {
            //			m_mthPrintTitleInfo(p_objPrintPageArg);
            //
            //			Font fntNormal = new Font("",10);
            //
            //			while(m_objPrintLineContext.m_BlnHaveMoreLine)
            //			{
            //				//�������ݴ�ӡ
            //				m_objPrintLineContext.m_mthPrintNextLine(ref m_intYPos,p_objPrintPageArg.Graphics,fntNormal);
            //
            //				if(m_intYPos > p_objPrintPageArg.PageBounds.Height-270
            //					&& m_objPrintLineContext.m_BlnHaveMoreLine)
            //				{
            //					//�������ݴ�ӡ������Ҫ��ҳ
            //
            //					m_mthPrintFoot(p_objPrintPageArg);
            //
            //					p_objPrintPageArg.HasMorePages = true;
            //
            //					m_intYPos = 155;
            //
            //					m_intCurrentPage++;
            //
            //					return;
            //				}				
            //			}
            //
            //			m_intYPos += 30;
            //			Font fntSign = new Font("",6);
            //			while(m_objPrintLineContext.m_BlnHaveMoreSign)
            //			{
            //				m_objPrintLineContext.m_mthPrintNextSign(30+10,m_intYPos,p_objPrintPageArg.Graphics,fntSign);
            //
            //				m_intYPos += (int)enmRectangleInfo.RowStep-10;				
            //			}
            //
            //			//ȫ������			
            //
            //			m_mthPrintFoot(p_objPrintPageArg);
        }


        /// <summary>
        /// ��ӡԪ��
        /// </summary>
        private enum enmItemDefination
        {
            //����Ԫ��
            InPatientID_Title,
            InPatientID,
            Name_Title,
            Name,
            BabySex_Title,
            BabySex,
            BirthTime_Title,
            Birth,

            Page_HospitalName,
            Page_Name_Title,
            Page_Title,
            Page_Num,
            Page_Of,
            Page_Count,

            Print_Date_Title,
            Print_Date,
            //�����Ԫ��
            RecordDate,
            RecordTime,
            RecordContent,
            RecordSign1,
            RecordSign2

        }

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        private clsPrintPageSettingForRecord m_objPageSetting;

        #region �����ӡ��Ԫ�ص������
        protected class clsPrintPageSettingForRecord
        {
            public clsPrintPageSettingForRecord() { }

            /// <summary>
            /// ��������
            /// </summary>
            /// <param name="p_intItemName">��Ŀ����</param>
            /// <returns></returns>
            public PointF m_getCoordinatePoint(int p_intItemName)
            {
                float fltOffsetX = 20;//X��ƫ����
                PointF m_fReturnPoint;
                switch (p_intItemName)
                {

                    case (int)enmItemDefination.Page_HospitalName:
                        m_fReturnPoint = new PointF(340f - fltOffsetX, 40f);
                        break;
                    case (int)enmItemDefination.Page_Name_Title:
                        m_fReturnPoint = new PointF(280f - fltOffsetX, 70f);
                        break;
                    case (int)enmItemDefination.Name_Title:
                        m_fReturnPoint = new PointF(50f - fltOffsetX, 110f);
                        break;
                    case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(130f - fltOffsetX, 110f);
                        break;

                    case (int)enmItemDefination.BabySex_Title:
                        m_fReturnPoint = new PointF(250f - fltOffsetX, 110f);
                        break;
                    case (int)enmItemDefination.BabySex:
                        m_fReturnPoint = new PointF(330f - fltOffsetX, 110f);
                        break;

                    case (int)enmItemDefination.BirthTime_Title:
                        m_fReturnPoint = new PointF(400f - fltOffsetX, 110f);
                        break;
                    case (int)enmItemDefination.Birth:
                        m_fReturnPoint = new PointF(500f - fltOffsetX, 110f);
                        break;
                    case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(610f - fltOffsetX, 75f);
                        break;
                    case (int)enmItemDefination.InPatientID:
                        m_fReturnPoint = new PointF(680f - fltOffsetX, 80f);
                        break;

                    default:
                        m_fReturnPoint = new PointF(400f, 400f);
                        break;

                }
                return m_fReturnPoint;
            }
        }

        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        private enum enmRectangleInfoInPatientCaseInfo
        {
            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 140,

            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = 16,

            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = 180 + 17,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 7,
            SmallRowStep = 20,
            /// <summary>
            /// ���ӵ�����
            /// </summary>
            RowLinesNum = 32,

            ColumnsMark1 = 35,

            /// <summary>
            /// CheckBoxƫ���ұ��ı��ľ���
            /// </summary>
            CheckShift = 15,

            /// <summary>
            /// �׻���ƫ���ı�����ľ���
            /// </summary>
            BottomLineShift = 15,

            BottomY = 1024,

            PrintWidth = 630,
            PrintWidth2 = 710,

        }

        #endregion




        #region ��ӡҳ��
        /// <summary>
        /// ��ӡҳ��
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintPageAtFoot(System.Drawing.Printing.PrintPageEventArgs e)
        {
            float fltOffsetX = 20;//X��ƫ����
            Font fntHeader = new Font("SimSun", 12);

            e.Graphics.DrawString("��      ҳ", fntHeader, Brushes.Black, 385 - fltOffsetX, e.PageBounds.Height - 225);
            e.Graphics.DrawString(m_intCurrentPage.ToString(), fntHeader, Brushes.Black, 425 - fltOffsetX, e.PageBounds.Height - 225);
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
            SizeF objFormNameSize = g.MeasureString(this.m_strTitle, this.m_fontTitle);
            SizeF objHospitalTitalSize = g.MeasureString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle.Trim(), this.m_fontTitle);
            g.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle.Trim(), m_fotHospitalFont, m_slbBrush, e.MarginBounds.Left + (e.MarginBounds.Width - objHospitalTitalSize.Width) / 2 + 25, e.MarginBounds.Top - 33);
            g.DrawString(this.m_strTitle, this.m_fontTitle, m_objBrush, e.MarginBounds.Left + (e.MarginBounds.Width - objFormNameSize.Width) / 2, e.MarginBounds.Top);
            this.m_fltLocationY = e.MarginBounds.Top + objFormNameSize.Height;

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

            strPrint = "����:" + p_objPatient.m_StrName.Trim();
            SizeF objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left - 40, p_fltLocationY);

            strPrint = "����:" + p_objPatient.m_ObjPeopleInfo.m_IntAge.ToString().Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol - 40, p_fltLocationY);

            strPrint = "��/����:" + ((m_objResultArr != null && m_objResultArr.Length > 0) ? m_objResultArr[0].m_strLayCount_chr : "");
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 2 - 50, p_fltLocationY);

            strPrint = "Ԥ����:" + ((m_objResultArr != null && m_objResultArr.Length > 0) ? m_objResultArr[0].m_strBeforehand_chr : "");
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 3 - 55, p_fltLocationY);

            strPrint = "����:" + p_objPatient.m_strBedCode.Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 4 + 35, p_fltLocationY);

            strPrint = "סԺ��:" + p_objPatient.m_StrHISInPatientID.Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 5 + 20, p_fltLocationY);

            this.m_fltLocationY = p_fltLocationY + objSize.Height;

            //			if(m_objResultArr.Length <=0)
            //			      return ;
            //			for(int i1 = 0; i1 < m_objResultArr.Length; i1 =i1+1)
            //			{
            //				e.Graphics.DrawString(m_objResultArr[i1].m_strLayCount_chr,m_fotTitleFont,m_slbBrush,300,i1+350);
            //
            //			}
        }
        #endregion

        #region ���ͷ
        private void m_mthPrintFormHeader(System.Drawing.Printing.PrintPageEventArgs e, ref float p_objLocationY)
        {

            System.Drawing.Graphics g = e.Graphics;


            g.DrawLine(this.m_objPen, m_fltFirstColLeft, p_objLocationY, m_fltColLeft14 + m_fltCol14, p_objLocationY);


            for (int i1 = 0; i1 < 15; i1++)
            {
                //g.DrawLine(this.m_objPen, e.MarginBounds.Left + e.MarginBounds.Width/14 * i1,p_objLocationY, e.MarginBounds.Left + e.MarginBounds.Width/14 * i1, p_objLocationY +m_ftlRowHeight);
                g.DrawLine(this.m_objPen, this.m_fltFirstColLeft + m_fltAvgCol * i1, p_objLocationY, m_fltFirstColLeft + m_fltAvgCol * i1, p_objLocationY + m_ftlRowHeight * 2);

            }
            SizeF s = g.MeasureString("����", this.m_fontBody);
            float y = p_objLocationY + m_ftlRowHeight * 2 / 2 - s.Height / 2;
            float y1 = p_objLocationY + m_ftlRowHeight / 2 - s.Height / 2;
            float y2 = p_objLocationY + m_ftlRowHeight + m_ftlRowHeight / 2 - s.Height / 2;

            m_mthDrawStrAtRectangle(this.m_fltFirstColLeft, this.m_fltSeconColLeft, "����", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltSeconColLeft, this.m_fltthColLeft, "ʱ��", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltthColLeft, this.m_fltthirColLeft, "Ѫѹ", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltthColLeft, this.m_fltthirColLeft, "mmHg", p_objLocationY + m_ftlRowHeight, e);
            m_mthDrawStrAtRectangle(this.m_fltthirColLeft, this.m_fltFiveColLeft, "̥λ", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltFiveColLeft, this.m_fltSixColLeft, "̥��", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltFiveColLeft, this.m_fltSixColLeft, "��/��", p_objLocationY + m_ftlRowHeight, e);
            m_mthDrawStrAtRectangle(this.m_fltSixColLeft, this.m_fltSenColLeft, "����", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltSixColLeft, this.m_fltSenColLeft, "��Ъ", p_objLocationY + m_ftlRowHeight, e);
            m_mthDrawStrAtRectangle(this.m_fltSenColLeft, this.m_fltNigColLeft, "����", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltSenColLeft, this.m_fltNigColLeft, "����", p_objLocationY + m_ftlRowHeight, e);
            m_mthDrawStrAtRectangle(this.m_fltNigColLeft, this.m_fltNiNeColLeft, "����", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltNigColLeft, this.m_fltNiNeColLeft, "ǿ��", p_objLocationY + m_ftlRowHeight, e);
            m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft, this.m_fltColLeft10, "����", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltNiNeColLeft, this.m_fltColLeft10, "cm", p_objLocationY + m_ftlRowHeight, e);
            m_mthDrawStrAtRectangle(this.m_fltColLeft10, this.m_fltColLeft11, "��¶", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltColLeft10, this.m_fltColLeft11, "S", p_objLocationY + m_ftlRowHeight, e);
            m_mthDrawStrAtRectangle(this.m_fltColLeft11, this.m_fltColLeft12, "̥Ĥ", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltColLeft12, this.m_fltColLeft13, "��(��)��", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltColLeft13, this.m_fltColLeft14, "����", p_objLocationY, e);
            m_mthDrawStrAtRectangle(this.m_fltColLeft14, this.m_fltColLeft15, "�����", p_objLocationY, e);

            p_objLocationY += m_ftlRowHeight * 2;
            g.DrawLine(this.m_objPen, m_fltFirstColLeft, p_objLocationY, m_fltColLeft14 + m_fltCol14, p_objLocationY);


        }
        #endregion


        private void m_mthDrawStrAtRectangle(float col1, float col2, string strPrint, float LocationY, System.Drawing.Printing.PrintPageEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            System.Drawing.Font m_font = this.m_fontBody;
            SizeF s = g.MeasureString(strPrint, m_font);
            if (s.Width > this.m_fltAvgCol)
            {
                m_font = new System.Drawing.Font("����", 8);
                s = g.MeasureString(strPrint, m_font);
            }

            float ji = col2 - col1;
            float X = col1 + ji / 2 - s.Width / 2;
            float Y = LocationY + m_ftlRowHeight / 2 - s.Height / 2;
            g.DrawString(strPrint, m_font, this.m_objBrush, X, Y);

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
            m_objPrintLineContext.m_mthReset();

            m_intYPos = 155;

            m_intCurrentPage = 1;
        }

        #endregion
    }
}
