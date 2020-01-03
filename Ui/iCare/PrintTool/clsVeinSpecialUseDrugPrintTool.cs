using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace iCare
{
    /// <summary>
    ///  �������⻯����ҩ�۲��¼��
    /// </summary>
    public class clsVeinSpecialUseDrugPrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        private clsBaseCaseHistoryDomain m_objRecordsDomain;
        //private com.digitalwave.clsRecordsService.clsVeinSpecialUseDrug_MainService m_objMainSvc;
        private clsVeinSpecialUseDrugValue[] m_objResultArr = null;
        private clsPatient m_objPatient = null;
        DateTime m_dtInHos;
        //�Ƿ��ⲿ��ӡ
        public bool isOutPrint = false;
        public string m_strDept = "";//����
        public string m_strBegNo = "";//����
        public string m_strName = "";//����
        public string m_strAge = "";//Age
        public string m_strInHosNo = "";//סԺ��
        public string m_strBeginTime = "";//��Һ��ʼʱ��
        public string m_strEndTime = "";//��Һendʱ��
        public string m_strID_CHR = "";//ID_CHR
        public clsVeinSpecialUseDrugPrintTool()
        {
            //m_objMainSvc = new com.digitalwave.clsRecordsService.clsVeinSpecialUseDrug_MainService();
        }
        ///<summary>
        ///�����������¼�����¼�
        ///</summary>
        int m_intRecordIndex = 0;

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
            if (isOutPrint == true)
            {
                //�������л�ȡָ��CheckDate�ļ�¼
                //com.digitalwave.clsRecordsService.clsVeinSpecialUseDrug_MainService m_objMainSvc =
                //    (com.digitalwave.clsRecordsService.clsVeinSpecialUseDrug_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsVeinSpecialUseDrug_MainService));

                (new weCare.Proxy.ProxyEmr()).Service.m_lngGetCheckDateRecord(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), p_dtmOpenDate, out m_objResultArr);
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
        public void m_mthSetPrintContent(object p_objPrintRecord)
        {

            m_mthInitDataSource((clsVeinSpecialUseDrugValue[])p_objPrintRecord);
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
                    clsPublicFunction.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
                    return null;
                }

                if (m_blnWantInit)
                    m_mthInitPrintContent();
            }

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
            m_intCurrentPageIndex = 1;
            if (this.Source != null)
                m_strTitle = Source.TableName;//Ĭ�ϱ���Ϊ"����"

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
            m_mthInitClientSize(e);
            mthInitColLocation(e, this.m_objRectangle);
            mthPrintTitle(e, this.m_objRectangle);
            mthPrintFormTitle(e);
            m_mthPrintAllPage(e, ref this.m_fltLocationY, this.m_dtSource);
        }

        #endregion

        #region ��ӡÿҳ

        private void m_mthPrintAllPage(System.Drawing.Printing.PrintPageEventArgs e, ref float p_objLocationY)
        {



        }

        #endregion

        /// <summary>
        /// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
        public void m_mthEndPrint(object p_objPrintArg)
        {
            if (m_strID_CHR != "")
                if (!((PrintEventArgs)p_objPrintArg).Cancel)
                {
                    //clsVeinSpecialUseDrug_MainService objServ =
                    //    (clsVeinSpecialUseDrug_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsVeinSpecialUseDrug_MainService));

                    (new weCare.Proxy.ProxyEmr()).Service.m_lngUpdateFirstPrintDateNew(this.m_strID_CHR, DateTime.Now);
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
        private void m_mthSetPrintContent(clsNewBabyInRoomRecord p_objContent, clsNewBabyCircsRecord[] p_objCircsContentArr, DateTime p_dtmFirstPrintDate)
        {

        }

        private clsNewBabyInRoomRecord m_objChangePrintTextColor(clsNewBabyInRoomRecord p_objclsInPatientCase)
        {
            if (p_objclsInPatientCase == null)
                return null;

            return p_objclsInPatientCase;
        }

        // ��ӡ����ʱ�Ĳ���
        private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
            //�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 

            m_mthResetWhenEndPrint();
        }

        /// <summary>
        /// ÿ�δ�ӡ����֮��ĸ�λ,�����Ǵ�ӡ��ǰҳ���ߴ�ӡȫ��.
        /// </summary>
        private void m_mthResetWhenEndPrint()
        {

        }


        #region user print define

        #region ����
        /// <summary>
        ///  ��ȡ������һ��ֵ����ֵָʾ�Ǻ����������ӡ��ҳ�� 
        /// </summary>
        public bool Landscape
        {
            get
            {
                return this.printDocument1.DefaultPageSettings.Landscape;
            }
            set
            {
                this.printDocument1.DefaultPageSettings.Landscape = value;
            }
        }
        /// <summary>
        /// ����Դ
        /// </summary>
        public DataTable Source
        {
            get
            {
                return m_dtSource;
            }
            set
            {
                m_dtSource = value;

            }
        }
        /// <summary>
        /// ��ӡ�ı���Ŀ
        /// </summary>
        public string Title
        {
            get
            {
                return m_strTitle;
            }
            set
            {
                m_strTitle = value;
            }
        }
        /// <summary>
        /// ��ӡ������(string)
        /// </summary>
        public string PrintDate
        {
            get
            {
                return m_strPrintDate;
            }
            set
            {
                m_strPrintDate = value;
            }
        }
        /// <summary>
        /// ��¼��ӡ�ĸ߶����λ
        /// </summary>
        public float LocationY
        {
            get
            {
                return m_fltLocationY;
            }
            set
            {
                m_fltLocationY = value;
            }
        }
        /// <summary>
        /// �ݴ�ÿ�е���ʾ���%(�ٷֱ�)
        /// </summary>
        public float[] ColumnPercentArr
        {
            get
            {
                return m_fltColumnPercentArr;
            }
            set
            {
                m_fltColumnPercentArr = value;
                m_blnUseColumnPercent = false;
            }
        }
        /// <summary>
        /// ��ʾ�ܿ��
        /// </summary>
        public float ColumnTotalWidth
        {
            get
            {
                return m_fltColumnTotalWidth;
            }
            set
            {
                m_fltColumnTotalWidth = value;
                m_blnUseDefaultTotalWidth = false;
            }
        }

        /// <summary>
        /// ��ӡ����Ĭ��Ϊ(x,y,w,h:20,100,787 or 1109,1049 or 707)
        /// </summary>
        public Rectangle RectangleSize
        {
            get
            {
                return m_objRectangle;
            }
            set
            {
                m_objRectangle = value;
                m_blnUseDefaultjRectangle = false;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����ȫ����ʾԤ��
        /// </summary>
        private Form frm = new Form();
        /// <summary>
        /// �Ƿ�ʹ��Ĭ�ϴ�ӡ����Rectangle
        /// </summary>
        private bool m_blnUseDefaultjRectangle = true;
        /// <summary>
        /// �����ӡ����
        /// </summary>
        private Rectangle m_objRectangle;
        /// <summary>
        /// ����ÿ�е���Ӧ���
        /// </summary>
        private float[] m_fltEachColWidth;
        /// <summary>
        /// ����ÿ�е���ӦLeft����
        /// </summary>
        private float[] m_fltEachColLeft;
        /// <summary>
        /// ƽ�����
        /// </summary>
        private float m_fltAvgColWidth;
        /// <summary>
        /// ��ʾ�ܿ��
        /// </summary>
        private float m_fltColumnTotalWidth;
        /// <summary>
        /// �Ƿ�ʹ��Ĭ���ܿ��
        /// </summary>
        private bool m_blnUseDefaultTotalWidth = true;
        /// <summary>
        /// �ݴ�ÿ�е���ʾ���%(�ٷֱ�)
        /// </summary>
        private float[] m_fltColumnPercentArr;
        /// <summary>
        /// �Ƿ�ʹ��Ĭ�ϵ�����ÿ�е���ʾ���%(�ٷֱ�)
        /// </summary>
        private bool m_blnUseColumnPercent = true;
        /// <summary>
        /// ����Դ
        /// </summary>	
        private DataTable m_dtSource;

        /// <summary>
        /// ��ӡԤ������
        /// </summary>	
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;

        /// <summary>
        /// ��ӡ�ĵ�����
        /// </summary>	
        private System.Drawing.Printing.PrintDocument printDocument1;

        #region ��ӡ������صı���

        /// <summary>
        /// ��ӡ�ı���Ŀ
        /// </summary>	
        private string m_strTitle;

        /// <summary>
        /// ��ӡ������
        /// </summary>	
        private string m_strPrintDate;

        /// <summary>
        /// ��¼��ӡ�ĸ߶����λ
        /// </summary>	
        private float m_fltLocationY;

        ///<summary>
        ///��������ӡ�ĵ�ǰҳ��
        ///</summary>
        private int m_intCurrentPageIndex = 0;

        ///<summary>
        ///��������ӡ����ҳ��
        ///</summary>
        private int m_intPageTotal = 0;

        ///<summary>
        ///������Ҫ��ӡ����
        ///</summary>
        private string m_strPrint = "";//Ҫ��ӡ����
        ///<summary>
        ///�����������߼��λ�ø� �����
        ///</summary>
        private float m_fltZijiHeight = 3; //�����߼��λ�ø� �����
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
        private float m_fltZiJiWide = 1;// �����������룺���
        ///<summary>
        ///�������и�
        ///</summary>
        private float m_ftlRowHeight;
        ///<summary>
        ///�������ַ����ĸ�ʽ��
        ///</summary>
        private StringFormat m_sf = new StringFormat();

        #region ���ô�ӡ�п���ÿ�еĺ��������

        ///<summary>
        ///��������1�п��
        ///</summary>
        float m_fltFirstColLeft; //��1��LEFT

        #endregion

        /// <summary>
        /// Pen����
        /// </summary>
        private Pen m_objPen = new Pen(Color.Black);
        /// <summary>
        /// brush
        /// </summary>	
        private System.Drawing.Brush m_objBrush = System.Drawing.Brushes.Black;

        /// <summary>
        /// ��ӡ���������
        /// </summary>	
        private System.Drawing.Font m_fontTitle = new System.Drawing.Font("simsun", 20, FontStyle.Bold);

        /// <summary>
        /// ��ӡ���ĵ�����
        /// </summary>	
        private System.Drawing.Font m_fontBody = new System.Drawing.Font("simsun", 12);

        #endregion

        #endregion

        #region ����:��ӡ����
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="e">PrintPageEventArgs</param>
        /// <param name="p_objRectangle">��ӡ����</param>
        private void mthPrintTitle(PrintPageEventArgs e, Rectangle p_objRectangle)
        {
            System.Drawing.Graphics g = e.Graphics;
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, new Font("SimSun", 12), m_objBrush, 320, 30);
            e.Graphics.DrawString("�������⻯����ҩ�۲��¼��", new Font("SimSun", 18, FontStyle.Bold), m_objBrush, 225, 70);
            string strPrint = "";
            //com.digitalwave.iCare.common.clsCommmonInfo com = new com.digitalwave.iCare.common.clsCommmonInfo();
            //string HospitalTitle = "";
            //HospitalTitle = com.m_strGetHospitalTitle();

            //strPrint = HospitalTitle + this.m_strTitle;
            SizeF objSize = g.MeasureString(strPrint, this.m_fontTitle);
            //g.DrawString(strPrint, this.m_fontTitle, m_objBrush, p_objRectangle.Left + (p_objRectangle.Width - objSize.Width) / 2, p_objRectangle.Top);
            this.m_fltLocationY = p_objRectangle.Top + objSize.Height + 30;


            int col = e.MarginBounds.Width / 4;
            float fltCol = float.Parse(col.ToString());//��ӡ�������е��п�		

            strPrint = "����:" + m_strDept.Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left - 30, m_fltLocationY);

            strPrint = "����:" + m_objPatient.m_strBedCode.Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 1 + 40, m_fltLocationY);

            strPrint = "����:" + m_objPatient.m_StrName.Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 2 + 30, m_fltLocationY);
            strPrint = "����:" + m_objPatient.m_ObjPeopleInfo.m_StrAgeLong.Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 3 + 25, m_fltLocationY);
            this.m_fltLocationY += this.m_ftlRowHeight;

            strPrint = "סԺ��:" + m_objPatient.m_StrHISInPatientID.Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left - 30, m_fltLocationY);

            strPrint = "��Һ��ʼʱ��:" + m_strBeginTime.Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 1 - 55, m_fltLocationY);

            strPrint = "��Һ����ʱ��:" + m_strEndTime.Trim();
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, e.MarginBounds.Left + fltCol * 2 + 70, m_fltLocationY);

            this.m_fltLocationY += this.m_ftlRowHeight;
        }
        #endregion

        #region ����:��ӡҳ��
        /// <summary>
        /// ����:��ӡҳ��
        /// </summary>		
        private void mthPrintFoot(PrintPageEventArgs e)
        {
            m_strPrint = string.Format("��{0}ҳ/��{1}ҳ", this.m_intCurrentPageIndex, this.m_intPageTotal);
            float fltWith = e.Graphics.MeasureString(m_strPrint, this.m_fontBody).Width;
            fltWith = float.Parse(this.m_objRectangle.Width.ToString()) - fltWith;
            e.Graphics.DrawString(m_strPrint, this.m_fontBody, this.m_objBrush, fltWith / 2, this.m_fltLocationY + 5);

        }
        #endregion

        #region ��ʼ��ÿһ����ʾ�Ŀ�Ȱٷֱ�ֵ
        /// <summary>
        ///��ʼ��ÿһ����ʾ�Ŀ�Ȱٷֱ�ֵ(Ĭ��ƽ����)
        /// </summary>
        private void m_mthInitEachColumnAvgWithPercent(int p_intColumnCount)
        {
            m_fltColumnPercentArr = new float[p_intColumnCount];
        }
        #endregion

        #region ����:��ʼ��ÿһ�е�λ��
        /// <summary>
        /// ����:��ʼ��ÿһ�е�λ��(p_objRectangle��ӡ����)
        /// </summary>		
        private void mthInitColLocation(PrintPageEventArgs e, Rectangle p_objRectangle)
        {
            #region ���ô�ӡ�п���ÿ�еĺ�����
            if (ColumnPercentArr == null)
            {
                float[] fltArrColumnPercent = new float[9];

                fltArrColumnPercent[0] = 0.055555556F; // -0.055555556F
                fltArrColumnPercent[1] = 0.111111112F;
                fltArrColumnPercent[2] = 0.055555556F;// -0.055555556F
                fltArrColumnPercent[3] = 0.111111112F;//
                fltArrColumnPercent[4] = 0.111111112F;
                fltArrColumnPercent[5] = 0.111111112F;
                fltArrColumnPercent[6] = 0.111111112F;//
                fltArrColumnPercent[7] = 0.111111112F + 0.055555556F + 0.055555556F + 0.022222226F;//+0.111111112F
                fltArrColumnPercent[8] = 0.111111112F - 0.022222226F;
                ColumnPercentArr = fltArrColumnPercent;
            }
            float dblColumnCount = float.Parse(m_fltColumnPercentArr.Length.ToString());
            float dblAvg = 0;
            //			if(m_blnUseDefaultTotalWidth)
            //			{
            m_fltColumnTotalWidth = float.Parse(Convert.ToString(p_objRectangle.Width));

            //			}						
            dblAvg = m_fltColumnTotalWidth / dblColumnCount;
            float dblPercent = dblAvg / m_fltColumnTotalWidth;
            if (m_blnUseColumnPercent)
            {
                for (int i = 0; i < m_fltColumnPercentArr.Length; ++i)
                {
                    m_fltColumnPercentArr[i] = dblPercent;
                }
            }

            m_fltEachColWidth = new float[m_fltColumnPercentArr.Length];
            m_fltEachColLeft = new float[m_fltColumnPercentArr.Length];
            m_fltFirstColLeft = p_objRectangle.Left; //��1��Left����
            for (int j = 0; j < m_fltColumnPercentArr.Length; ++j)
            {
                m_fltEachColWidth[j] = m_fltColumnPercentArr[j] * m_fltColumnTotalWidth;
                if (j == 0)
                    m_fltEachColLeft[j] = m_fltFirstColLeft;
                else
                    m_fltEachColLeft[j] = m_fltEachColLeft[j - 1] + m_fltEachColWidth[j - 1];
            }
            #endregion

            m_objsize = e.Graphics.MeasureString("����", this.m_fontBody);
            m_fltZiHeight = m_objsize.Height;// �ָ�
            //			m_ftlRowHeight =  m_fltZiHeight+10 ;//�и�
            m_ftlRowHeight = m_fltZijiHeight * 2 + m_fltZiHeight;//�и�

        }
        #endregion

        #region ����
        /// <summary>
        /// ��һ����
        /// </summary>
        /// <param name="p_intColumnCount">������</param>
        private void m_mthDrawLines(PrintPageEventArgs e, int p_intColumnCount)
        {
            Graphics g = e.Graphics;
            for (int i1 = 0; i1 < p_intColumnCount; i1++)
            {
                g.DrawLine(this.m_objPen, m_fltEachColLeft[i1], m_fltLocationY, m_fltEachColLeft[i1], m_fltLocationY + this.m_ftlRowHeight);
            }
            g.DrawLine(this.m_objPen, m_fltEachColLeft[p_intColumnCount - 1] + m_fltEachColWidth[p_intColumnCount - 1], m_fltLocationY, m_fltEachColLeft[p_intColumnCount - 1] + m_fltEachColWidth[p_intColumnCount - 1], m_fltLocationY + this.m_ftlRowHeight);
            g.DrawLine(this.m_objPen, m_fltEachColLeft[0], this.m_fltLocationY + this.m_ftlRowHeight, m_fltEachColLeft[p_intColumnCount - 1] + m_fltEachColWidth[p_intColumnCount - 1], this.m_fltLocationY + this.m_ftlRowHeight);

        }
        #endregion

        #region ������X����������.��"�ַ�"
        /// <summary>
        /// ������X����������.��"�ַ�"
        /// </summary>
        /// <param name="col1">x1</param>
        /// <param name="col2">x2</param>
        /// <param name="strPrint">�ַ�</param>
        /// <param name="LocationY">y</param>
        /// <param name="e"></param>
        private void m_mthDrawStrAtRectangle(float col1, float col2, string strPrint, float LocationY, System.Drawing.Printing.PrintPageEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            System.Drawing.Font objfont = new System.Drawing.Font("����", 10);
            SizeF s = g.MeasureString(strPrint, objfont);
            if (s.Width >= col2 - col1)
            {
                objfont = new System.Drawing.Font("����", 8);
                s = g.MeasureString(strPrint, objfont);

            }
            float ji = col2 - col1;
            float X = col1 + ji / 2 - s.Width / 2;
            //			float Y = LocationY + this.m_fltZijiHeight;	
            float Y = LocationY + m_ftlRowHeight / 2 - s.Height / 2;
            if (s.Width > ji)
            {
                objfont = new System.Drawing.Font("����", 8);
                float flt = ji / g.MeasureString("��", objfont).Width;
                int intcharCount = Convert.ToInt32(flt) + 1;
                float y1 = LocationY + m_ftlRowHeight / 4 - s.Height / 2 + 2;
                float y2 = LocationY + 3 * m_ftlRowHeight / 4 - s.Height / 2;
                string strPrint1 = strPrint.Substring(0, intcharCount);
                float x1 = col1 + ji / 2 - g.MeasureString(strPrint1, objfont).Width / 2;
                g.DrawString(strPrint1, objfont, this.m_objBrush, x1, y1);

                string strPrint2 = "";
                if (strPrint.Length - intcharCount <= intcharCount)
                    strPrint2 = strPrint.Substring(intcharCount);
                else
                    strPrint2 = strPrint.Substring(intcharCount, intcharCount);
                //				x1 =  col1 + ji/2 - g.MeasureString(strPrint2,objfont).Width/2;
                x1 = col1 + 3;
                g.DrawString(strPrint2, objfont, this.m_objBrush, x1, y2);

            }
            else
            {
                g.DrawString(strPrint, objfont, this.m_objBrush, X, Y);
            }

        }
        #endregion

        #region ����:��ӡ��ͷ
        /// <summary>
        /// ����:��ӡ��ͷ
        /// </summary>		
        private void mthPrintFormTitle(PrintPageEventArgs e)
        {
            string objStr = "";
            int intColumnCount = this.m_dtSource.Columns.Count;
            System.Drawing.Graphics g = e.Graphics;
            g.DrawLine(this.m_objPen, m_fltEachColLeft[0], m_fltLocationY, m_fltEachColLeft[intColumnCount - 1] + m_fltEachColWidth[intColumnCount - 1], m_fltLocationY);
            for (int i1 = 0; i1 < intColumnCount; i1++)
            {
                objStr = this.m_dtSource.Columns[i1].ColumnName;
                if (i1 == intColumnCount - 1)
                {
                    m_mthDrawStrAtRectangle(m_fltEachColLeft[i1], m_fltEachColLeft[i1] + m_fltEachColWidth[i1], objStr, m_fltLocationY, e);
                }
                else
                {
                    m_mthDrawStrAtRectangle(m_fltEachColLeft[i1], m_fltEachColLeft[i1 + 1], objStr, m_fltLocationY, e);
                }
            }
            m_mthDrawLines(e, intColumnCount);
            m_fltLocationY += this.m_ftlRowHeight;

        }
        #endregion

        #region ��ʼ���ɴ�ӡ����x,y,width,height
        /// <summary>
        /// ��ʼ���ɴ�ӡ����x,y,width,height
        /// </summary>
        /// <param name="e"></param>
        private void m_mthInitClientSize(PrintPageEventArgs e)
        {
            if (m_blnUseDefaultjRectangle)
            {
                //				e.PageSettings.Margins.Left = 5;
                //				e.PageSettings.Margins.Right = 5;
                int d = e.MarginBounds.Bottom;
                int d2 = e.MarginBounds.Right;
                int d3 = e.MarginBounds.Width;
                int d4 = e.MarginBounds.Left;
                this.m_objRectangle.X = e.PageBounds.Left + 20; //��1��Left����20
                this.m_objRectangle.Y = e.MarginBounds.Top;  //100
                this.m_objRectangle.Width = e.PageBounds.Width - 60;//787 or 1109
                this.m_objRectangle.Height = e.MarginBounds.Height + 80;//1049 or 707
            }
        }
        #endregion

        #region ��ӡÿҳ
        private int i = 0;
        /// <summary>
        /// ��ӡÿҳ
        /// </summary>
        /// <param name="e">PrintPageEventArgs</param>
        /// <param name="p_objLocationY">Y</param>
        /// <param name="p_dt">����Դ</param>
        private void m_mthPrintAllPage(System.Drawing.Printing.PrintPageEventArgs e, ref float p_objLocationY, DataTable p_dt)
        {
            int p_intRowsCount = p_dt.Rows.Count;

            string strPrint = "";
            //�����һҳ���ܴ�ӡ��������
            int intRowCount = Convert.ToInt32((float.Parse(this.m_objRectangle.Height.ToString()) - p_objLocationY) / m_ftlRowHeight);
            #region ��û������,���ӡ�ձ��
            if (p_intRowsCount == 0)
            {
                for (int k = 0; k < intRowCount; ++k)
                {
                    m_mthDrawLines(e, p_dt.Columns.Count);
                    p_objLocationY += this.m_ftlRowHeight;
                }
                return;

            };
            #endregion
            if (p_intRowsCount % intRowCount > 0)
                m_intPageTotal = p_intRowsCount / intRowCount + 1;
            else
                m_intPageTotal = p_intRowsCount / intRowCount;

            if (m_intCurrentPageIndex == 1)//��һҳ
            {
                for (i = 0; i < p_intRowsCount && i < intRowCount; i++)
                {
                    #region draw one row
                    for (int j = 0; j < p_dt.Columns.Count; ++j)
                    {
                        strPrint = p_dt.Rows[i][j].ToString();
                        if (j == p_dt.Columns.Count - 1)
                        {
                            m_mthDrawStrAtRectangle(m_fltEachColLeft[j], m_fltEachColLeft[j] + m_fltEachColWidth[j], strPrint, p_objLocationY, e);
                        }
                        else
                        {
                            m_mthDrawStrAtRectangle(m_fltEachColLeft[j], m_fltEachColLeft[j + 1], strPrint, p_objLocationY, e);
                        }
                    }
                    #endregion
                    m_mthDrawLines(e, p_dt.Columns.Count);
                    p_objLocationY += this.m_ftlRowHeight;
                }
                mthPrintFoot(e);
                //�ж��Ƿ��ҳ
                if (i < p_intRowsCount)
                {
                    m_intCurrentPageIndex++;
                    e.HasMorePages = true;
                    return;
                }
            }
            else
            {
                int temp = i;
                #region draw one row
                for (; i < p_intRowsCount && i < intRowCount + temp; i++)
                {
                    #region draw one row
                    for (int j = 0; j < p_dt.Columns.Count; ++j)
                    {
                        strPrint = p_dt.Rows[i][j].ToString();
                        if (j == p_dt.Columns.Count - 1)
                        {
                            m_mthDrawStrAtRectangle(m_fltEachColLeft[j], m_fltEachColLeft[j] + m_fltEachColWidth[j], strPrint, p_objLocationY, e);
                        }
                        else
                        {
                            m_mthDrawStrAtRectangle(m_fltEachColLeft[j], m_fltEachColLeft[j + 1], strPrint, p_objLocationY, e);
                        }
                    }
                    #endregion
                    m_mthDrawLines(e, p_dt.Columns.Count);
                    p_objLocationY += this.m_ftlRowHeight;
                }
                #endregion
                mthPrintFoot(e);
                //�ж��Ƿ��ҳ
                if (i < p_intRowsCount)
                {
                    m_intCurrentPageIndex++;
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    m_intCurrentPageIndex = 1;
                }

            }
        }
        #endregion

        #region ��ʼ������Դ
        /// <summary>
        /// ��ʼ������Դ
        /// </summary>			
        /// <param name="p_dt">��ӡ������Դ</param>
        public void m_mthSetDataSource(DataTable p_dt)
        {
            Source = p_dt;
            m_strTitle = p_dt.TableName;//Ĭ�ϱ���Ϊ"����"
            m_fltColumnPercentArr = new float[p_dt.Columns.Count];

        }

        /// <summary>
        /// ��ʼ������Դ
        /// </summary>
        /// <param name="p_dt">��ӡ������Դ</param>
        /// <param name="p_fltColumnPercentArr">ÿ����ʾ��Ȱٷֱ�</param>
        public void m_mthSetDataSource(DataTable p_dt, float[] p_fltColumnPercentArr)
        {
            Source = p_dt;
            m_strTitle = p_dt.TableName; //Ĭ�ϱ���Ϊ"����"
            ColumnPercentArr = p_fltColumnPercentArr;

        }
        #endregion

        #region ���ɵ�������Ҫ�õ���DataTable
        /// <summary>
        /// ���������ɵ�������Ҫ�õ���DataTable
        /// </summary>
        private DataTable m_mthCreateTable()
        {
            DataTable dt = new DataTable("�������⻯����ҩ�۲��¼��");
            System.Data.DataColumn dc = new DataColumn("����");
            dt.Columns.Add(dc);
            dc = new DataColumn("ҩ��");
            dt.Columns.Add(dc);
            dc = new DataColumn("��/��");
            dt.Columns.Add(dc);
            dc = new DataColumn("Ѳ��ʱ��");
            dt.Columns.Add(dc);
            dc = new DataColumn("���̵�����");
            dt.Columns.Add(dc);
            dc = new DataColumn("���̵��쳣");
            dt.Columns.Add(dc);
            dc = new DataColumn("�Ƿ���");
            dt.Columns.Add(dc);
            dc = new DataColumn("��ע");
            dt.Columns.Add(dc);
            dc = new DataColumn("ǩ��");
            dt.Columns.Add(dc);
            return dt;
        }
        #endregion

        #region ��ʼ������Դ���ⲿ������
        /// <summary>
        /// ��ʼ������Դ���ⲿ������
        /// </summary>
        /// <param name="p_objPatient"></param>
        protected void m_mthInitDataSource(clsVeinSpecialUseDrugValue[] p_objPrintRecordArr)
        {
            //isOutPrint = true;//�ⲿ��ӡ
            DataTable dtSouce = m_mthCreateTable();
            //������Դ�������
            m_mthSetDataSource(dtSouce, p_objPrintRecordArr);

            float[] fltArrColumnPercent = new float[9];
            fltArrColumnPercent[0] = 0.055555556F; // -0.055555556F
            fltArrColumnPercent[1] = 0.111111112F;
            fltArrColumnPercent[2] = 0.055555556F;// -0.055555556F
            fltArrColumnPercent[3] = 0.111111112F;//
            fltArrColumnPercent[4] = 0.111111112F;
            fltArrColumnPercent[5] = 0.111111112F;
            fltArrColumnPercent[6] = 0.111111112F;//
            fltArrColumnPercent[7] = 0.111111112F + 0.055555556F + 0.055555556F + 0.022222226F;//+0.111111112F
            fltArrColumnPercent[8] = 0.111111112F - 0.022222226F;
            this.Source = dtSouce;
            this.ColumnPercentArr = fltArrColumnPercent;
            if (m_objPatient != null)
            {
                m_strDept = m_objPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptName;//����
                m_strBegNo = m_objPatient.m_strBedCode;//����
                m_strName = m_objPatient.m_StrName;//����
                m_strAge = m_objPatient.m_ObjPeopleInfo.m_StrAge;//Age
                m_strInHosNo = m_objPatient.m_StrHISInPatientID;//סԺ��
            }
            else
            {
                m_strDept = string.Empty;//����
                m_strBegNo = string.Empty;//����
                m_strName = string.Empty;//����
                m_strAge = string.Empty;//Age
                m_strInHosNo = string.Empty;//סԺ��
            }

            m_strBeginTime = p_objPrintRecordArr[0].m_dtmFluidBEGINTIME_DATE.ToString("yyyy-MM-dd HH:mm:ss");//��Һ��ʼʱ��
            m_strEndTime = p_objPrintRecordArr[0].m_dtmfluidEndTIME_DATE.ToString("yyyy-MM-dd HH:mm:ss");//��Һ����ʱ��
        }
        #endregion

        #region ������Դ�������
        private void m_mthSetDataSource(DataTable p_dt, clsVeinSpecialUseDrugValue[] p_objPrintRecordArr)
        {
            if (m_objPatient == null) return;

            try
            {
                if (p_objPrintRecordArr == null)
                {
                    return;
                }
                string strConvert = "";
                for (int i1 = 0; i1 < p_objPrintRecordArr.Length; i1++)
                {
                    clsVeinSpecialUseDrugValue m_objRecord = p_objPrintRecordArr[i1];
                    if (m_objRecord.m_strBEGINTIME_DATE != null)
                    {
                        object[] objDataArr = new object[9];
                        //���������¼������������һ������ʾ
                        if (strConvert != Convert.ToDateTime(m_objRecord.m_strBEGINTIME_DATE).Day.ToString() + "/" + Convert.ToDateTime(m_objRecord.m_strBEGINTIME_DATE).Month.ToString())
                        {
                            strConvert = Convert.ToDateTime(m_objRecord.m_strBEGINTIME_DATE).Day.ToString() + "/" + Convert.ToDateTime(m_objRecord.m_strBEGINTIME_DATE).Month.ToString();
                            objDataArr[0] = strConvert;
                        }
                        else
                        {
                            objDataArr[0] = null;//����
                        }
                        objDataArr[1] = m_objRecord.m_strMEDICINENAME_CHR;//ҩ��
                        objDataArr[2] = m_objRecord.m_strDROP_CHR;//��/��
                        objDataArr[3] = Convert.ToDateTime(m_objRecord.m_strBEGINTIME_DATE).ToString("HH:mm");//Ѳ��ʱ��
                        objDataArr[4] = m_objRecord.m_strINGEAR_CHR;//���̵�����
                        objDataArr[5] = m_objRecord.m_strABNORMITY_CHR;//���̵��쳣
                        objDataArr[6] = m_objRecord.m_strSOLVE_CHR;//�Ƿ���
                        objDataArr[7] = m_objRecord.m_strREMARK_CHR;//��ע
                        objDataArr[8] = m_objRecord.m_strUNDERWRITE_CHR;//ǩ��
                        //��ע��ÿ����ʾ12���ַ�
                        if (m_objRecord.m_strREMARK_CHR.Length > 12)
                        {
                            objDataArr[7] = m_objRecord.m_strREMARK_CHR.Substring(0, 12);
                            p_dt.BeginLoadData();
                            p_dt.LoadDataRow(objDataArr, true);
                            p_dt.EndLoadData();

                            objDataArr[0] = null;
                            objDataArr[1] = null;
                            objDataArr[2] = null;
                            objDataArr[3] = null;
                            objDataArr[4] = null;
                            objDataArr[5] = null;
                            objDataArr[6] = null;
                            //objDataArr[7] = null;
                            objDataArr[8] = null;

                            m_objRecord.m_strREMARK_CHR = m_objRecord.m_strREMARK_CHR.Substring(12);
                            while (m_objRecord.m_strREMARK_CHR.Length > 12)
                            {
                                string strTem = m_objRecord.m_strREMARK_CHR.Substring(0, 12);
                                objDataArr[7] = strTem;
                                p_dt.BeginLoadData();
                                p_dt.LoadDataRow(objDataArr, true);
                                p_dt.EndLoadData();
                                m_objRecord.m_strREMARK_CHR = m_objRecord.m_strREMARK_CHR.Substring(12);
                            }
                            objDataArr[7] = m_objRecord.m_strREMARK_CHR;
                            p_dt.BeginLoadData();
                            p_dt.LoadDataRow(objDataArr, true);
                            p_dt.EndLoadData();
                        }
                        else
                        {
                            p_dt.BeginLoadData();
                            p_dt.LoadDataRow(objDataArr, true);
                            p_dt.EndLoadData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }

        }
        #endregion
        #endregion

    }
}

