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
    ///  ICU�����¼��ӡ��
    /// </summary>
    public class clsICUNurseRecord_GX_PrintTool : frmICUNurseRecord_GX,infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        private  clsRecordsDomain m_objRecordsDomain;
        //private com.digitalwave.clsRecordsService.clsICUNurseRecord_GXMainService m_objInRoomSvc;
        clsPatient m_objPatient = null;
        //private com.digitalwave.clsRecordsService.clsVeinSpecialUseDrug_ConService m_objInRoomSvc;
        private clsICUNurseRecordContentGX[] m_objValue;
        DateTime m_dtInHos;
        #region  ���õ������ֶ�
        public string m_strInPatientID = "";
        public string m_strInPatientDate = "";
        public string m_strName = "";
        public string m_strSex = "";
        public string m_strAge = "";
        public string m_strBedCode = "";
        public DateTime m_dtmPrintOpenDate;
        /// <summary>
        /// ������
        /// </summary>
        public string m_strDiseaseID = "";
        /// <summary>
        /// ��ӡ�� ҳ X
        /// </summary>
        public float m_fltPageIndexX;
        /// <summary>
        /// ��ӡ�� ҳ Y
        /// </summary>
        public float m_fltPageIndexY;
        public DateTime m_dtmCreateDate;
        public clsPatient m_objPatientTemp;
        public string m_strCustomColumn1 = "";//�Զ�������1
        public string m_strCustomColumn2 = "";
        public string m_strCustomColumn3 = "";
        public string m_strCustomColumn4 = "";
        public string m_strTempColumnName = "";
        #endregion
        public clsICUNurseRecord_GX_PrintTool()
        {
            //m_objInRoomSvc = new com.digitalwave.clsRecordsService.clsICUNurseRecord_GXMainService();
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
        /// <param 	name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            
            m_objPatient = p_objPatient;
            m_dtInHos = p_dtmInPatientDate;
            //��ȡ����ûɾ��������
            //com.digitalwave.clsRecordsService.clsICUNurseRecord_GXMainService m_objInRoomSvc =
            //    (com.digitalwave.clsRecordsService.clsICUNurseRecord_GXMainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsICUNurseRecord_GXMainService));

            (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetAllMainRecord(m_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out m_objValue);
            m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ
            //clsPatient m_objPatient = p_objPatient;
            //m_objPrintInfo = new clsPrintInfo_ICUNurseRecord();
            //m_objPrintInfo.m_strInPatentID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
            //m_objPrintInfo.m_strPatientName = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrFirstName : "";
            //m_objPrintInfo.m_strSex = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            //m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            //m_objPrintInfo.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            //m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptName : "";
            //m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            //m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";
            //m_objPrintInfo.m_dtmHISInDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
            

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

            return m_objValue;
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
            m_strTitle = "ICU�����¼";//Ĭ�ϱ���Ϊ"����"

        }
        #endregion

        #region ��ӡ��
        /// <summary>
        /// ��ӡ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintPageEventArgs���͵Ķ���</param>
        public void m_mthPrintPage(object p_objPrintArg)
        {
            if (Source==null)
            {
                m_mthInitPrintData(this, m_objPatient);
            }
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

            if (!((PrintEventArgs)p_objPrintArg).Cancel)
            {
                //com.digitalwave.clsICUNurseRecord_GXService.clsICUNurseRecord_GXService service =
                //    (com.digitalwave.clsICUNurseRecord_GXService.clsICUNurseRecord_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsICUNurseRecord_GXService.clsICUNurseRecord_GXService));

                (new weCare.Proxy.ProxyEmr05()).Service.clsICUNurseRecord_GXService_m_lngUpdateFirstPrintDate(m_strInPatientID, m_strInPatientDate, m_dtmPrintOpenDate.ToString(), System.DateTime.Now);
                //new 

                //clsVeinSpecialUseDrug_MainService().m_lngUpdateFirstPrintDateNew(this.m_strID_CHR,DateTime.Now);
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
        private void m_mthSetPrintContent(clsNewBabyInRoomRecord p_objContent, clsNewBabyCircsRecord[]

            p_objCircsContentArr, DateTime p_dtmFirstPrintDate)
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

        #region ��ͷ�������ȡ
        private RectangleF m_mthgetPointRectangle(int p_strFormNameIndex)
        {
            float fltRowHeiht = m_ftlRowHeight;
            fltRowHeiht += 5F;
            float fltY = this.m_fltLocationY;
            fltY += 5F;
            RectangleF rec = new RectangleF(0, 0, 0, 0);
            switch (p_strFormNameIndex)
            {
                case 0: //ʱ
                    rec = new RectangleF(
                                 m_fltEachColLeft[0],
                                fltY,
                                m_fltEachColWidth[0],
                                fltRowHeiht);
                    break;

                case 1://���� (ռ5���ĵ�Ԫ��)
                    rec = new RectangleF(m_fltEachColLeft[1],
                                            fltY + 2F,
                                            m_fltEachColLeft[4] - m_fltEachColLeft[1],
                                            fltRowHeiht);
                    break;
                case 2://��Ŀ(3��)
                    rec = new RectangleF(m_fltEachColLeft[1],
                    fltY + fltRowHeiht,
                    m_fltEachColWidth[1],
                    fltRowHeiht);
                    break;
                case 3://������
                    rec = new RectangleF(m_fltEachColLeft[2],
                        fltY + fltRowHeiht - 6F,
                        m_fltEachColWidth[2],
                        fltRowHeiht);
                    break;
                case 4://ʵ����
                    rec = new RectangleF(m_fltEachColLeft[3],
                        fltY + fltRowHeiht - 6F,
                        m_fltEachColWidth[3],
                        fltRowHeiht);
                    break;
                case 5://����
                    rec = new RectangleF(m_fltEachColLeft[4],
                        fltY + 2F,
                        m_fltEachColLeft[7] - m_fltEachColLeft[4],
                        fltRowHeiht);
                    break;
                case 6://��
                    rec = new RectangleF(m_fltEachColLeft[4],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[4],
                        fltRowHeiht);
                    break;
                case 7://�Զ�����һ
                    rec = new RectangleF(m_fltEachColLeft[5],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[5],
                        fltRowHeiht);
                    break;
                case 8://�Զ�����2
                    rec = new RectangleF(m_fltEachColLeft[6],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[6],
                        fltRowHeiht);
                    break;
                case 9://T
                    rec = new RectangleF(m_fltEachColLeft[7],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[7],
                        fltRowHeiht);
                    break;
                case 10://HR
                    rec = new RectangleF(m_fltEachColLeft[8],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[8],
                        fltRowHeiht);
                    break;
                case 11://R
                    rec = new RectangleF(m_fltEachColLeft[9],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[9],
                        fltRowHeiht);
                    break;
                case 12://BP
                    rec = new RectangleF(m_fltEachColLeft[10],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[10],
                        fltRowHeiht);
                    break;
                case 13://A-
                    rec = new RectangleF(m_fltEachColLeft[11],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[11],
                        fltRowHeiht);
                    break;
                case 14://SPO2
                    rec = new RectangleF(m_fltEachColLeft[12],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[12],
                        fltRowHeiht);
                    break;
                case 15://һ�����
                    rec = new RectangleF(m_fltEachColLeft[13],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[13],
                        fltRowHeiht);
                    break;
                case 16://С��
                    rec = new RectangleF(m_fltEachColLeft[13],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[13],
                        fltRowHeiht);
                    break;
                case 17://�۲첡��
                    rec = new RectangleF(m_fltEachColLeft[7],
                        fltY + 2F,
                        m_fltEachColLeft[13] + m_fltEachColWidth[13] - m_fltEachColLeft[7],
                        fltRowHeiht);
                    break;
                case 18: //��
                    rec = new RectangleF(
                        m_fltEachColLeft[0],
                        fltY + fltRowHeiht,
                        m_fltEachColWidth[0],
                        fltRowHeiht);
                    break;
            }
            return rec;
        }
        #endregion

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
        private float m_fltZijiHeight = 8; //�����߼��λ�ø� �����
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

        #region ���ô�ӡ�п���ÿ�еĺ��������		///<summary>
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
        private System.Drawing.Font m_fontTitle = new System.Drawing.Font("SimSun", 20, FontStyle.Bold);

        /// <summary>
        /// ��ӡ���ĵ�����
        /// </summary>	
        private System.Drawing.Font m_fontBody = new System.Drawing.Font("SimSun", 12);

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

            string strPrint = "";
            //com.digitalwave.iCare.common.clsCommmonInfo com = new com.digitalwave.iCare.common.clsCommmonInfo();
            //string HospitalTitle = "";
            //HospitalTitle = com.m_strGetHospitalTitle();
            System.Drawing.Graphics g = e.Graphics;
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, new Font("simsun", 12), m_objBrush, 320,30);

            e.Graphics.DrawString("ICU   ��   ��   ��   ¼", new Font("simsun", 20, FontStyle.Bold), m_objBrush, 260,70);
            //strPrint = HospitalTitle;
            SizeF objSize = g.MeasureString(strPrint, new System.Drawing.Font("SimSun", 12));

            //g.DrawString(strPrint, new System.Drawing.Font("SimSun", 12), m_objBrush, e.MarginBounds.Left + (e.MarginBounds.Width - objSize.Width) / 2, p_objRectangle.Top - 50);
            this.m_fltLocationY = p_objRectangle.Top + objSize.Height;

            //strPrint = this.m_strTitle;
            objSize = g.MeasureString(strPrint, this.m_fontTitle);
            //g.DrawString(strPrint, this.m_fontTitle, m_objBrush, e.MarginBounds.Left + (e.MarginBounds.Width - objSize.Width) / 2, m_fltLocationY);

            this.m_fltLocationY += objSize.Height+20;

            int col = p_objRectangle.Width / 7;
            float fltCol = float.Parse(col.ToString());//��ӡ�������е��п�		

            strPrint = "����:" + (m_objPatient == null ? m_strName.Trim() : m_objPatient.m_StrName);
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, p_objRectangle.Left, m_fltLocationY);

            strPrint = "�Ա�:" + (m_objPatient == null ?m_strSex.Trim():m_objPatient.m_ObjPeopleInfo.m_StrSex);
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, p_objRectangle.Left + fltCol * 1 + 20, m_fltLocationY);

            strPrint = "����:" + (m_objPatient == null ?m_strAge.Trim():m_objPatient.m_ObjPeopleInfo.m_IntAge.ToString());
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, p_objRectangle.Left + fltCol * 2, m_fltLocationY);
            strPrint = "����:" + (m_objPatient == null ?m_strBedCode.Trim():m_objPatient.m_strBedCode);
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, p_objRectangle.Left + fltCol * 3, m_fltLocationY);

            strPrint = "������:" + (m_objPatient == null ? m_strDiseaseID.Trim() : m_objPatient.m_StrInPatientID);
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, p_objRectangle.Left + fltCol * 4, m_fltLocationY);

            strPrint = "����:" + m_dtmCreateDate.ToString("yyyy��MM��");
            objSize = g.MeasureString(strPrint, this.m_fontBody);
            g.DrawString(strPrint, m_fontBody, m_objBrush, p_objRectangle.Left + fltCol * 5+30, m_fltLocationY);

            m_fltPageIndexY = m_fltLocationY;
            this.m_fltLocationY += this.m_ftlRowHeight - 5F;
        }
        #endregion

        #region ����:��ӡҳ��
        /// <summary>
        /// ����:��ӡҳ��
        /// </summary>		
        private void mthPrintFoot(PrintPageEventArgs e)
        {
            m_strPrint = string.Format("��{0}ҳ", this.m_intCurrentPageIndex);
            float fltWith = e.Graphics.MeasureString(m_strPrint, this.m_fontBody).Width;
            fltWith = float.Parse(this.m_objRectangle.Right.ToString()) - fltWith;
            e.Graphics.DrawString(m_strPrint, this.m_fontBody, this.m_objBrush, fltWith, m_fltPageIndexY);

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
            if (m_fltColumnPercentArr==null)
            {
                float[] fltArrColumnPercent = new float[14];
			    fltArrColumnPercent[0] = 0.04761905F; 
			    fltArrColumnPercent[1] = 0.04761905F * 3;
			    fltArrColumnPercent[2] = 0.04761905F;
			    fltArrColumnPercent[3] = 0.04761905F;
			    fltArrColumnPercent[4] = 0.04761905F;
			    fltArrColumnPercent[5] = 0.04761905F;
			    fltArrColumnPercent[6] = 0.04761905F;
			    fltArrColumnPercent[7] = 0.04761905F;
			    fltArrColumnPercent[8] = 0.04761905F;
			    fltArrColumnPercent[9] = 0.04761905F;
			    fltArrColumnPercent[10] = 0.04761905F;
			    fltArrColumnPercent[11] = 0.04761905F;
			    fltArrColumnPercent[12] = 0.04761905F;
			    fltArrColumnPercent[13] = 0.04761905F * 3+0.04761905F * 3;
                ColumnPercentArr = fltArrColumnPercent;
            }
            float dblColumnCount = float.Parse(m_fltColumnPercentArr.Length.ToString());
            float dblAvg = 0;
            m_fltColumnTotalWidth = float.Parse(Convert.ToString(p_objRectangle.Width));
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
                g.DrawLine(this.m_objPen, m_fltEachColLeft[i1], m_fltLocationY, m_fltEachColLeft[i1]

                    , m_fltLocationY + this.m_ftlRowHeight);
            }
            g.DrawLine(this.m_objPen, m_fltEachColLeft[p_intColumnCount - 1] + m_fltEachColWidth[p_intColumnCount

                - 1], m_fltLocationY, m_fltEachColLeft[p_intColumnCount - 1] + m_fltEachColWidth[p_intColumnCount - 1], m_fltLocationY +

                this.m_ftlRowHeight);
            g.DrawLine(this.m_objPen, m_fltEachColLeft[0], this.m_fltLocationY + this.m_ftlRowHeight,

                m_fltEachColLeft[p_intColumnCount - 1] + m_fltEachColWidth[p_intColumnCount - 1], this.m_fltLocationY + this.m_ftlRowHeight);

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
        private void m_mthDrawStrAtRectangle(float col1, float col2, string strPrint, float

            LocationY, System.Drawing.Printing.PrintPageEventArgs e)
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
            //float X =  col1 + ji/2 - s.Width/2;
            float X = col1 + 3;
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
                //float x1 =  col1 + ji/2 - g.MeasureString(strPrint1,objfont).Width/2;
                float x1 = col1 + 3;
                g.DrawString(strPrint1, objfont, this.m_objBrush, x1, y1);

                string strPrint2 = "";
                if (strPrint.Length - intcharCount <= intcharCount)
                    strPrint2 = strPrint.Substring(intcharCount);
                else
                    strPrint2 = strPrint.Substring(intcharCount, intcharCount);
                //	x1 =  col1 + ji/2 - g.MeasureString(strPrint2,objfont).Width/2;
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
            string strPrint = "";
            RectangleF rec;
            StringFormat m_sfm = new StringFormat(StringFormatFlags.FitBlackBox);
            m_sfm.Alignment = StringAlignment.Center;
            int intColumnCount = m_dtSource==null?m_fltColumnPercentArr.Length:this.m_dtSource.Columns.Count;
            System.Drawing.Graphics g = e.Graphics;
            g.DrawLine(this.m_objPen, m_fltEachColLeft[0], m_fltLocationY, m_fltEachColLeft[intColumnCount - 1] + m_fltEachColWidth[intColumnCount - 1], m_fltLocationY);
            //ʱ��
            rec = m_mthgetPointRectangle(0);
            strPrint = "ʱ";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);
            g.DrawLine(this.m_objPen, rec.X, m_fltLocationY, rec.X, m_fltLocationY + this.m_ftlRowHeight * 2);
            g.DrawLine(this.m_objPen, rec.Right, m_fltLocationY, rec.Right, m_fltLocationY + this.m_ftlRowHeight * 2);
            //��
            rec = m_mthgetPointRectangle(18);
            g.DrawString("��", this.m_fontBody, this.m_objBrush, rec, m_sfm);

            //����
            rec = m_mthgetPointRectangle(1);
            strPrint = "��    ��";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);
            //m_mthDrawStrAtRectangle(rec.Left,rec.Right,strPrint,this.m_fltLocationY,e);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + this.m_ftlRowHeight, rec.Right, m_fltLocationY + this.m_ftlRowHeight);

            //����
            rec = m_mthgetPointRectangle(5);
            strPrint = "��   ��";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + this.m_ftlRowHeight, rec.Right, m_fltLocationY + this.m_ftlRowHeight);
            g.DrawLine(this.m_objPen, rec.X, m_fltLocationY, rec.X, m_fltLocationY + this.m_ftlRowHeight * 2);
            g.DrawLine(this.m_objPen, rec.Right, m_fltLocationY, rec.Right, m_fltLocationY + this.m_ftlRowHeight * 2);

            //�۲첡��
            rec = m_mthgetPointRectangle(17);
            strPrint = "��     ��     ��     ��";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + this.m_ftlRowHeight, rec.Right, m_fltLocationY + this.m_ftlRowHeight);

            //��Ŀ
            rec = m_mthgetPointRectangle(2);
            strPrint = "��   Ŀ";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);

            //������
            rec = m_mthgetPointRectangle(3);
            strPrint = "������";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);
            //m_mthDrawStrAtRectangle(rec.Left,rec.Right,strPrint,this.m_fltLocationY + this.m_ftlRowHeight,e);

            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);

            //������
            rec = m_mthgetPointRectangle(4);
            strPrint = "ʵ����";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);
            //m_mthDrawStrAtRectangle(rec.Left,rec.Right,strPrint,this.m_fltLocationY + this.m_ftlRowHeight,e);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);
            //��
            rec = m_mthgetPointRectangle(6);
            strPrint = "��";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);

            //�Զ�����һ
            rec = m_mthgetPointRectangle(7);
            strPrint = this.m_strCustomColumn1.Replace("\r\n", "");
            System.Drawing.Font m_fontCustom = new System.Drawing.Font("����", 10);
            if (strPrint.Length > 2)
            {
                rec.Y -= 6F;
                if (strPrint.Length > 4)
                {
                    m_fontCustom = new System.Drawing.Font("����", 8);
                }
            }
            g.DrawString(strPrint, m_fontCustom, this.m_objBrush, rec, m_sfm);
            //m_mthDrawStrAtRectangle(rec.Left,rec.Right,strPrint,this.m_fltLocationY,e);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);
            //�Զ�����2
            rec = m_mthgetPointRectangle(8);
            strPrint = this.m_strCustomColumn2.Replace("\r\n", "");
            m_fontCustom = new System.Drawing.Font("����", 10);
            if (strPrint.Length > 2)
            {
                rec.Y -= 6F;
                if (strPrint.Length > 4)
                {
                    m_fontCustom = new System.Drawing.Font("����", 8);
                }
            }
            g.DrawString(strPrint, m_fontCustom, this.m_objBrush, rec, m_sfm);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);
            //T
            rec = m_mthgetPointRectangle(9);
            strPrint = "T";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);
            //HR
            rec = m_mthgetPointRectangle(10);
            strPrint = "HR";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);
            //R
            rec = m_mthgetPointRectangle(11);
            strPrint = "R";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);
            //BP
            rec = m_mthgetPointRectangle(12);
            strPrint = "BP";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);
            //A-
            rec = m_mthgetPointRectangle(13);
            strPrint = this.m_strCustomColumn3.Replace("\r\n", "");
            m_fontCustom = new System.Drawing.Font("����", 10);
            if (strPrint.Length > 2)
            {
                rec.Y -= 6F;
                if (strPrint.Length > 4)
                {
                    m_fontCustom = new System.Drawing.Font("����", 8);
                }
            }
            g.DrawString(strPrint, m_fontCustom, this.m_objBrush, rec, m_sfm);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);
            //spo2
            rec = m_mthgetPointRectangle(14);
            strPrint = m_strCustomColumn4.Replace("\r\n", "");
            m_fontCustom = new System.Drawing.Font("����", 10);
            if (strPrint.Length > 2)
            {
                rec.Y -= 6F;
                if (strPrint.Length > 4)
                {
                    m_fontCustom = new System.Drawing.Font("����", 8);
                }
            }
            g.DrawString(strPrint, m_fontCustom, this.m_objBrush, rec, m_sfm);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);
            //һ�����
            rec = m_mthgetPointRectangle(15);
            strPrint = "һ�����";
            g.DrawString(strPrint, this.m_fontBody, this.m_objBrush, rec, m_sfm);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);
            //С��
            rec = m_mthgetPointRectangle(16);
            //strPrint = "С    ��";
            //g.DrawString(strPrint,this.m_fontBody,this.m_objBrush,rec,m_sfm);
            g.DrawLine(this.m_objPen, rec.Left, m_fltLocationY + m_ftlRowHeight, rec.Left, m_fltLocationY + m_ftlRowHeight * 2);
            g.DrawLine(this.m_objPen, rec.Right, m_fltLocationY, rec.Right, m_fltLocationY + this.m_ftlRowHeight * 2);

            m_fltLocationY += this.m_ftlRowHeight * 2;

            g.DrawLine(this.m_objPen, m_fltEachColLeft[0], m_fltLocationY, m_fltEachColLeft[intColumnCount - 1] + m_fltEachColWidth[intColumnCount - 1], m_fltLocationY);

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
        private void m_mthPrintAllPage(System.Drawing.Printing.PrintPageEventArgs e, ref float

            p_objLocationY, DataTable p_dt)
        {
            int p_intRowsCount = p_dt.Rows.Count;

            string strPrint = "";
            //�����һҳ���ܴ�ӡ��������
            int intRowCount = Convert.ToInt32((float.Parse(this.m_objRectangle.Height.ToString()) -

                p_objLocationY) / m_ftlRowHeight);
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
                            m_mthDrawStrAtRectangle(m_fltEachColLeft[j], m_fltEachColLeft[j] +

                                m_fltEachColWidth[j], strPrint, p_objLocationY, e);
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
                            m_mthDrawStrAtRectangle(m_fltEachColLeft[j], m_fltEachColLeft[j] +

                                m_fltEachColWidth[j], strPrint, p_objLocationY, e);
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
        #endregion

    }
}

