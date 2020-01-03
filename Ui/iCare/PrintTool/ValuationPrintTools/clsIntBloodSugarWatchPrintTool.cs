using System;
using iCareData;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;
using com.digitalwave.clsRecordsService;

namespace iCare
{
    /// <summary>
    /// �ڷ��ڿ�Ѫ�ǹ۲��
    /// </summary>
    public partial class clsIntBloodSugarWatchPrintTool : infPrintRecord
    {
        string m_strRegisterId;
        public clsIntBloodSugarWatchPrintTool(string p_strRegisterId)
		{
            m_strRegisterId = p_strRegisterId;
		}
        private clsRecordsDomain m_objRecordsDomain;  
		private clsPatient m_objPatient;
        private clsPrintInfo_bloodsugarwatch m_objPrintInfo; 
          
        private clsEMR_intbloodsugarwatchValue[] m_objValues;
        //public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        //{
        //    m_objPatient = p_objPatient;
        //}
        public void m_mthSetPrintInfo(clsPatient p_objPatient)
        {
            m_objPatient = p_objPatient;
        }
        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
           // m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ
             m_objPatient = p_objPatient;
            m_objPrintInfo = new clsPrintInfo_bloodsugarwatch();
            m_objPrintInfo.m_strInPatentID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
            m_objPrintInfo.m_strPatientName = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrFirstName : "";
            m_objPrintInfo.m_strSex = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            m_objPrintInfo.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
            m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";
            m_objPrintInfo.m_dtmHISInPatientDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
        }
        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
        /// </summary>
        public void m_mthInitPrintContent()
        {
            if (m_objPatient == null)
            {
                //				clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
                return;
            }
            if (m_objPatient.m_StrInPatientID == "" || m_objPatient.m_DtmSelectedInDate == DateTime.MinValue)
                m_objValues = null;
            else
            {

                m_objRecordsDomain = new clsRecordsDomain(enmRecordsType.EMR_IntBloodSugarWatch);

                long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_strRegisterId,1, out m_objPrintInfo.m_objTransDataArr, out m_objPrintInfo.m_dtmFirstPrintDateArr, out m_objPrintInfo.m_blnIsFirstPrintArr);
                if (lngRes <= 0)
                    return;

                //clsEMR_intbloodsugarwatchMainServ m_objServ = new clsEMR_intbloodsugarwatchMainServ();
                //long lngRes = m_objServ.m_lngGetTransDataInfoArrWithServ(m_objPatient.m_StrInPatientID,1, out m_objValues);
                ////m_objServ.Dispose();
                //if (lngRes <= 0 || m_objValues == null)
                //    return;
                //m_objDomain = new clsMiniBooldSugarChkDomin();
                //long lngRes = m_objDomain.m_lngGetRecoedByInPatient(m_objPatient.m_StrInPatientID, m_objPatient.m_DtmSelectedInDate, out m_objValues);
                //if (lngRes <= 0 || m_objValues == null)
                //    return;
            }
            m_mthSetPrintContent(m_objPrintInfo.m_objTransDataArr, m_objPrintInfo.m_dtmFirstPrintDateArr);
        }
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        { }

        /// <summary>
        /// ��ȡ��ӡ����,(�������ݿ��ȡʱ,���ñ�����֮ǰ�����ȵ���m_mthSetPrintInfo����)
        /// </summary>
        /// <returns>��ӡ����</returns>
        public object m_objGetPrintInfo()
        {
            return m_objPrintInfo;

        }
        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {
            if (m_objPrintInfo.m_objTransDataArr == null)
                m_mthInitPrintContent();
        }
        /// <summary>
        /// ��ʼ����ӡ����,��������ն��󼴿�.
        /// </summary>
        public void m_mthInitPrintTool(object p_objArg)
        {
            #region �йش�ӡ��ʼ��
            m_fotTitleFont = new Font("SimSun", 20, FontStyle.Bold);
            m_fotHeaderFont = new Font("SimSun", 18, FontStyle.Bold);
            m_fotSmallFont = new Font("SimSun", 12);
            m_GridPen = new Pen(Color.Black, 1);
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
            m_fotSmallFont.Dispose();
            m_GridPen.Dispose();
            m_slbBrush.Dispose();
        }
        /// <summary>
        /// ��ӡ��ʼ
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
        public void m_mthBeginPrint(object p_objPrintArg)
        { }

        /// <summary>
        /// ��ӡ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintPageEventArgs���͵Ķ���</param>
        public void m_mthPrintPage()
        {
            frmPrintPreviewDialogPF frmPreview = new frmPrintPreviewDialogPF();
            frmPreview.m_evtBeginPrint += new PrintEventHandler(frmPreview_m_evtBeginPrint);
            frmPreview.m_evtEndPrint += new PrintEventHandler(frmPreview_m_evtEndPrint);
            frmPreview.m_evtPrintContent += new PrintPageEventHandler(frmPreview_m_evtPrintContent);
            frmPreview.m_evtPrintFrame += new PrintPageEventHandler(frmPreview_m_evtPrintFrame);
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
        /// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
        public void m_mthEndPrint(object p_objPrintArg)
        {
            m_intLines = 0;
            m_intPages = 1;
        }
        private void m_mthSetPrintContent(clsTransDataInfo[] p_objTransDataArr,
     DateTime[] p_dtmFirstPrintDate)
        {
            try
            {
                if (p_objTransDataArr == null || p_dtmFirstPrintDate == null || p_objTransDataArr.Length != p_dtmFirstPrintDate.Length)
                {
                   // clsPublicFunction.ShowInformationMessageBox("��ӡ��������!");E:\icar_fo2\Code\EMR\icare\PrintTool\clsICUNurseRecordPrintTool.cs
                    return;
                }

                //���ݲ�ͬ�ı����ͣ���ȡ��Ӧ��clsDiseaseTrackInfo
                clsDiseaseTrackInfo objTrackInfo = null;
                m_objPrintDataArr = new clsEMR_intbloodsugarwatchDataInfo[p_objTransDataArr.Length];
                //				m_objPrintDataArr=(clsIntensiveTendDataInfo[])(p_objTransDataArr.Clone());
                ArrayList arlTemp = new ArrayList();
                arlTemp.AddRange(p_objTransDataArr);
                m_objPrintDataArr = (clsEMR_intbloodsugarwatchDataInfo[])arlTemp.ToArray(typeof(clsEMR_intbloodsugarwatchDataInfo));

                //System.Data.DataTable dtbBlankRecord = null;
                //new clsDiseaseTrackAddBlankDomain().m_lngGetBlankRecordContent(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate,out dtbBlankRecord);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
        private clsEMR_intbloodsugarwatchDataInfo[] m_objPrintDataArr;
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
        private int m_intLines = 0;
        private int m_intLineStep = 58;
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
            RightX = 820 - 20,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 25,

            BottomY = 1024
        }


        #endregion

        #region �������ֲ���
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotSmallFont, m_slbBrush, 320, 30);
            e.Graphics.DrawString("����΢��Ѫ�Ǽ���¼��", m_fotTitleFont, m_slbBrush, 240, 70);

            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX, 120);
            //e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+50,120,(int)enmRectangleInfo.LeftX+120,120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_ObjPeopleInfo.m_StrFirstName), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 45, 120);

            e.Graphics.DrawString("�Ա�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 125, 120);
            //e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+165,120,(int)enmRectangleInfo.LeftX+185,120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_ObjPeopleInfo.m_StrSex), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 165, 120);

            e.Graphics.DrawString("���䣺", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 200, 120);
            //e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+230,120,(int)enmRectangleInfo.LeftX+275,120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_ObjPeopleInfo.m_StrAge), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 240, 120);

            //e.Graphics.DrawString("���ң�",m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+270,100);
            //e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+315,120,(int)enmRectangleInfo.LeftX+385,120);
            //e.Graphics.DrawString((m_objPatient == null?"":m_objPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptName.Trim()) ,m_fotSmallFont,m_slbBrush,(int)enmRectangleInfo.LeftX+315,100);

            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 335, 120);
            //e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+430,120,(int)enmRectangleInfo.LeftX+450,120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(m_objPatient.m_DtmSelectedInDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 375, 120);

            e.Graphics.DrawString("סԺ�ţ�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 605, 120);
            //e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+560,120,(int)enmRectangleInfo.LeftX+640,120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_StrInPatientID.Trim()), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 665, 120);

            e.Graphics.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 525, 120);
            //e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+695,120,(int)enmRectangleInfo.LeftX+740,120);
            string strTemp;
            try
            {
                strTemp = m_objPatient == null ? "" : m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName.ToString().Trim();
            }
            catch
            {
                strTemp = "";
            }
            e.Graphics.DrawString(strTemp, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 565, 120);
        }


        #endregion
     

        private void m_mthPrintRectangleInfo(PrintPageEventArgs e)
        {
            int intYPos = 145;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, intYPos, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, intYPos, (int)enmRectangleInfo.RightX, (int)enmRectangleInfo.BottomY);

            int intXPos = 130;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("     ����", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX, 148);
            e.Graphics.DrawString("�ո�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 148);
            intXPos += m_intLineStep;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("��ͺ�\n2Сʱ", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 148);
            intXPos += m_intLineStep;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("�в�ǰ", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 148);
            intXPos += m_intLineStep;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("�вͺ�\n2Сʱ", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 148);
            intXPos += m_intLineStep;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("���ǰ", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 148);
            intXPos += m_intLineStep;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("��ͺ�\n2Сʱ", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 148);
            intXPos += m_intLineStep;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("˯ǰ", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 148);
            intXPos += m_intLineStep;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("��ע", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 148);
            intXPos += m_intLineStep;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("   ǩ��", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 148);
            for (int i = 0; i < 37; i++)
            {
                if (i == 0)
                {
                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, intYPos, (int)enmRectangleInfo.RightX, intYPos);
                    intYPos += 2 * (int)enmRectangleInfo.RowStep;


                }
                else
                {
                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, intYPos, (int)enmRectangleInfo.RightX, intYPos);
                    intYPos += (int)enmRectangleInfo.RowStep;
                }
            }
            e.Graphics.DrawString("�� " + m_intPages.ToString() + " ҳ", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 600, intYPos + 30);
        }
        private void m_mthAddDataToGrid(PrintPageEventArgs e)
        {
            if (m_objPrintInfo == null)
            {
                e.HasMorePages = false;
                return;
            }
            int intYPos = 172;
            for (; m_intLines < m_objPrintInfo.m_objTransDataArr.Length; )
            {
               // m_objValues;
                clsEMR_intbloodsugarwatchDataInfo p_objdata=new clsEMR_intbloodsugarwatchDataInfo();
               // clsEMR_intbloodsugarwatchValue clsGereralData = new clsEMR_intbloodsugarwatchValue();
                p_objdata = (clsEMR_intbloodsugarwatchDataInfo)m_objPrintInfo.m_objTransDataArr[m_intLines];
                if (p_objdata.m_objRecordArr.Length != 0)
                {
                    m_objValues = p_objdata.m_objRecordArr;
                    for (int i = 0; i <m_objValues.Length; i++)
                    {
                     // m_objPrintInfo.m_objTransDataArr[m_intLines].m_objRecordContent
                       // e.Graphics.DrawString(clsGereralData.m_objRecordContent.m_dtmCreateDate.ToString(m_strDateFormat), new Font("SimSun", 10), m_slbBrush, (int)enmRectangleInfo.LeftX, intYPos);
                        e.Graphics.DrawString(m_objValues[i].m_dtmRecordDate.ToString("yyyy��MM��dd��"), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 10, intYPos + (int)enmRectangleInfo.RowStep);
                        e.Graphics.DrawString(m_objValues[i].m_strNULLABDOMEN_RIGHT, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 135, intYPos + (int)enmRectangleInfo.RowStep);
                        e.Graphics.DrawString(m_objValues[i].m_strTWOBREAKFAST_RIGTH, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 195, intYPos + (int)enmRectangleInfo.RowStep);
                        e.Graphics.DrawString(m_objValues[i].m_strBEFORELUNCH_RIGHT, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 260, intYPos + (int)enmRectangleInfo.RowStep);
                        e.Graphics.DrawString(m_objValues[i].m_strTWOAFTERLUNCH_RIGHT, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 325, intYPos + (int)enmRectangleInfo.RowStep);
                        e.Graphics.DrawString(m_objValues[i].m_strBEFOREDINNER_RIGHT, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 380, intYPos + (int)enmRectangleInfo.RowStep);
                        e.Graphics.DrawString(m_objValues[i].m_strTWOAFTERDINNER_RIGHT, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 430, intYPos + (int)enmRectangleInfo.RowStep);
                        e.Graphics.DrawString(m_objValues[i].m_strBEFORESLEEP_RIGHT, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 495, intYPos + (int)enmRectangleInfo.RowStep);
                        e.Graphics.DrawString(m_objValues[i].m_strBEIZHU_RIGHT, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 555, intYPos + (int)enmRectangleInfo.RowStep);
                        e.Graphics.DrawString(m_objValues[i].objSignerArr[0].objEmployee.ToString(), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 600, intYPos + (int)enmRectangleInfo.RowStep);

                       // e.Graphics.DrawString(m_objValues[i].m_strBEFOREDINNER_VCHR, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 471, intYPos);
                    // e.Graphics.DrawString(clsGereralData.m_objRecordContent.m_strBEFORESLEEP_VCHR, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 606, intYPos);
                    intYPos += (int)enmRectangleInfo.RowStep;
                    m_intLines++;
                    if (m_intLines % 35 == 0)
                    {
                        m_intPages++;
                        e.HasMorePages = true;
                        return;
                    }
                    }
                
                }
            }
            e.HasMorePages = false;
        }

        #endregion

    }
}
