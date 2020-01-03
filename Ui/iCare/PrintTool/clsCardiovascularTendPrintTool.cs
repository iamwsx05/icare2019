using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// ��Ѫ������ػ���¼��ӡ���ߵ�ժҪ˵����
    /// </summary>
    public class clsCardiovascularTendPrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        private clsRecordsDomain m_objRecordsDomain;
        private clsPrintInfo_CardiovascularTend m_objPrintInfo;
        private clsCardiovascularTend_GXDataInfo[] m_objPrintDataArr;
        //private clsCardiovascularTend_GXService m_objServ;
        private int m_intFlag = -1;
        private int m_intRecordCount = 0;
        private long lngResult;
        private int[] m_intFrontPosArr = new int[46];
        private int[] m_intBackPosArr = new int[30];

        /// <summary>
        /// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
        /// </summary>
        /// <param name="p_objPatient">����</param>
        /// <param name="p_dtmInPatientDate">��Ժ����</param>
        /// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            if (p_objPatient != null)
            {
                m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ
                clsPatient m_objPatient = p_objPatient;
                m_objPrintInfo = new clsPrintInfo_CardiovascularTend();
                m_objPrintInfo.m_strInPatientID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
                m_objPrintInfo.m_strPatientName = m_objPatient != null ? m_objPatient.m_StrName : "";
                m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
                m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
                m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;
                //m_objServ = new clsCardiovascularTend_GXService();
                //clsCardiovascularTend_GXService m_objServ =
                //    (clsCardiovascularTend_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCardiovascularTend_GXService));

                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetBaseInfo(m_objPrintInfo.m_strInPatientID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out m_objPrintInfo.m_objPrintBaseDataArr);
                if (lngResult > 0)
                {
                    if (m_objPrintInfo.m_objPrintBaseDataArr != null)
                        for (int i0 = 0; i0 < m_objPrintInfo.m_objPrintBaseDataArr.Length; i0++)
                        {
                            if (m_objPrintInfo.m_objPrintBaseDataArr[i0].m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"))
                            {
                                m_intFlag = i0;
                                break;
                            }
                        }
                }
                else
                    return;
                if (m_intFlag == -1)
                    return;
                //m_objServ.Dispose();
            }
            else
            {
                clsPublicFunction.ShowInformationMessageBox("��ӡ���˲���Ϊ��,��ѡ��һ������!");
                return;
            }

        }
        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
        /// </summary>
        public void m_mthInitPrintContent()
        {
            if (m_objPrintInfo == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
                return;
            }
            if (m_objPrintInfo.m_strInPatientID == "" || m_intFlag == -1)
            {
                m_blnWantInit = false;
                clsPublicFunction.ShowInformationMessageBox("������д�����Ļ������ϲ�����!");
                return;
            }
            try
            {
                m_objRecordsDomain = new clsRecordsDomain(enmRecordsType.CardiovascularTend_GX);
                long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_objPrintInfo.m_strInPatientID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out m_objPrintInfo.m_objTransDataArr, out m_objPrintInfo.m_dtmFirstPrintDateArr, out m_objPrintInfo.m_blnIsFirstPrintArr);

                if (lngRes <= 0)
                    return;


                //����¼ʱ��(CreateDate)���� 
                m_mthSortTransData(ref m_objPrintInfo.m_objTransDataArr);

                //���ñ����ݵ���ӡ��
                m_mthSetPrintContent(m_objPrintInfo.m_objTransDataArr, m_objPrintInfo.m_dtmFirstPrintDateArr);
                m_objPrintInfo.m_objPrintDataArr = m_objPrintDataArr;

                m_blnWantInit = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// ����¼˳��(CreateDate)�������p_objTansDataInfoArr����
        /// </summary>
        /// <param name="p_objTansDataInfoArr"></param>
        private void m_mthSortTransData(ref clsTransDataInfo[] p_objTansDataInfoArr)
        {
            ArrayList m_arlSort = new ArrayList(p_objTansDataInfoArr);
            m_arlSort.Sort();
            p_objTansDataInfoArr = (clsTransDataInfo[])m_arlSort.ToArray(typeof(clsTransDataInfo));
        }
        /// <summary>
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {
            if (p_objPrintContent.GetType().Name != "clsPrintInfo_CardiovascularTend")
            {
                clsPublicFunction.ShowInformationMessageBox("��������");
            }
            m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            m_objPrintInfo = (clsPrintInfo_CardiovascularTend)p_objPrintContent;
            m_objPrintDataArr = m_objPrintInfo.m_objPrintDataArr;
            m_blnWantInit = false;
        }

        /// <summary>
        /// ��ȡ��ӡ����,(�������ݿ��ȡʱ,���ñ�����֮ǰ�����ȵ���m_mthSetPrintInfo����)
        /// </summary>
        /// <returns>��ӡ����</returns>
        public object m_objGetPrintInfo()
        {
            if (m_blnIsFromDataSource)
            {
                if (m_objPrintInfo == null)
                {
                    clsPublicFunction.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
                    return null;
                }

                if (m_blnWantInit)
                    m_mthInitPrintContent();
            }

            return m_objPrintInfo;
        }

        /// <summary>
        /// ��ʼ����ӡ����,��������ն��󼴿�.
        /// </summary>
        public void m_mthInitPrintTool(object p_objArg)
        {
            m_fotTitleFont = new Font("SimSun", 20, FontStyle.Bold);
            m_fotSmallFont = new Font("SimSun", 12);
            m_fotTinyFont = new Font("SimSun", 10);
            m_GridPen = new Pen(Color.Black, 1);
            m_slbBrush = new SolidBrush(Color.Black);

        }

        /// <summary>
        /// �ͷŴ�ӡ����
        /// </summary>
        public void m_mthDisposePrintTools(object p_objArg)
        {
            m_fotTitleFont.Dispose();
            m_fotSmallFont.Dispose();
            m_fotTinyFont.Dispose();
            m_GridPen.Dispose();
            m_slbBrush.Dispose();

        }
        /// <summary>
        /// ���ô�ӡ���ݡ�
        /// </summary>
        /// <param name="p_objTransDataArr"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        private void m_mthSetPrintContent(clsTransDataInfo[] p_objTransDataArr,
            DateTime[] p_dtmFirstPrintDate)
        {
            if (p_objTransDataArr == null || p_dtmFirstPrintDate == null || p_objTransDataArr.Length != p_dtmFirstPrintDate.Length)
            {
                clsPublicFunction.ShowInformationMessageBox("��ӡ��������!");
                return;
            }
            ArrayList m_arlTemp = new ArrayList();
            for (int i1 = 0; i1 < p_objTransDataArr.Length; i1++)
            {

                m_arlTemp.Add(p_objTransDataArr[i1]);

            }
            m_objPrintDataArr = (clsCardiovascularTend_GXDataInfo[])m_arlTemp.ToArray(typeof(clsCardiovascularTend_GXDataInfo));
            try
            {

                for (int i2 = 0; i2 < m_objPrintDataArr.Length; i2++)
                    if (m_objPrintDataArr[i2].m_objRecordArr != null)
                        for (int j2 = 0; j2 < m_objPrintDataArr[i2].m_objRecordArr.Length; j2++)
                        {
                            if (m_objPrintDataArr[i2].m_objRecordArr[j2].m_dtmRECORDDATE.ToString("yyyy-MM-dd") == m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_dtmRECORDDATE.ToString("yyyy-MM-dd"))
                            {
                                m_intRecordCount++;

                            }
                        }
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.ToString());
            }
        }

        #region �йش�ӡ������
        /// <summary>
        /// ���浱ǰ�е�Y����
        /// </summary>
        private int m_intFrontPosY = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
        /// <summary>
        /// ���浱ǰ�е�Y����
        /// </summary>
        private int m_intBackPosY = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
        /// <summary>
        /// ���������
        /// </summary>
        private Font m_fotTitleFont;
        /// <summary>
        /// �����ݵ�����
        /// </summary>
        private Font m_fotSmallFont;
        /// <summary>
        /// ��С������
        /// </summary>
        private Font m_fotTinyFont;
        /// <summary>
        /// �߿򻭱�
        /// </summary>
        private Pen m_GridPen;
        /// <summary>
        /// ˢ��
        /// </summary>
        private SolidBrush m_slbBrush;
        /// <summary>
        /// ��¼��ӡ���ڼ�ҳ
        /// </summary>
        private int m_intNowPage = 1;
        /// <summary>
        /// ��¼��ӡ��ҳ��
        /// </summary>
        private int m_intTotalPageCount = 2;
        /// <summary>
        /// ��ǰ�����ӡ�ļ�¼�����
        /// </summary>
        private int m_intBackCurrentRecord = 0;
        /// <summary>
        /// ��ǰ�����ӡ�ļ�¼�����
        /// </summary>
        private int m_intFrontCurrentRecord = 0;
        /// <summary>
        /// �ɼ�¼����,׼����ӡһ���¼�¼
        /// </summary>
        bool m_blnBeginPrintNewRecord = true;
        /// <summary>
        /// ����Ҫ������ʷ�ۼ�����ǰ��¼����
        /// </summary>
        private string[][] m_strValueArr;
        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        private enum enmRecordRectangleInfo//����
        {//A3ֽ�ţ����� ��1620*1024 Size
         /// <summary>
         /// ��ѿ��
         /// </summary>
            PerformWith = 40,
            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 175,
            /// <summary>
            /// ��ͷ��һ���ָ���
            /// </summary>
            RowsMark1 = 30,
            /// <summary>
            /// ��ͷ�ڶ����ָ���(������)
            /// </summary>
            RowsMark2 = 70,
            /// <summary>
            /// ��ͷ�������ָ��ߣ������û����ݵ�����ߣ�
            /// </summary>
            RowsMark3 = 180,
            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = 25,
            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = 1620 - 30,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 38,
            /// <summary>
            /// ���ӵ�����
            /// </summary>
            RowLinesNum = 17,
            /// <summary>
            /// �����ڸ�������Ը��Ӷ��˵Ĵ�ֱƫ��
            /// </summary>
            VOffSet = 20,
            /// <summary>
            /// ��һ�������(X),ʵ����1������ߣ�
            /// </summary>			
            ColumnsMark1 = 75,

            /// <summary>
            /// �ڶ��������(X)��ʵ����2������ߣ�
            /// </summary>
            ColumnsMark2 = ColumnsMark1 + PerformWith - 15,//105

            /// <summary>
            /// ��3�������(X)��ʵ����3������ߣ�
            /// </summary>
            ColumnsMark3 = ColumnsMark2 + PerformWith - 15,//135

            /// <summary>
            /// ��4�������(X)��ʵ����4������ߣ�
            /// </summary>
            ColumnsMark4 = ColumnsMark3 + PerformWith - 15,//165

            /// <summary>
            ///  ��5�������(X)��ʵ����5������ߣ�
            /// </summary>
            ColumnsMark5 = ColumnsMark4 + PerformWith - 15,//195

            /// <summary>
            /// ȫѪ������ߣ�
            /// </summary>
            ColumnsMark6 = ColumnsMark5 + PerformWith - 15,//225

            /// <summary>
            ///     ������ߣ�
            /// </summary>
            ColumnsMark7 = ColumnsMark6 + PerformWith,//270

            /// <summary>
            ///ÿʱ������ߣ�
            /// </summary>
            ColumnsMark8 = ColumnsMark7 + PerformWith - 15,//300

            /// <summary>
            ///(����) ����������ߣ�
            /// </summary>
            ColumnsMark9 = ColumnsMark8 + PerformWith,//345

            /// <summary>
            ///(����)���� ������ߣ�
            /// </summary>
            ColumnsMark10 = ColumnsMark9 + PerformWith,//390

            /// <summary>
            /// ����>>ÿʱ������ߣ�
            /// </summary>
            ColumnsMark11 = ColumnsMark10 + PerformWith,//435

            /// <summary>
            /// �ۻ�����������ߣ�
            /// </summary>
            ColumnsMark12 = ColumnsMark11 + PerformWith,//480

            /// <summary>
            /// ����������ߣ�
            /// </summary>
            ColumnsMark13 = ColumnsMark12 + PerformWith,//525

            /// <summary>
            ///��Һ������ߣ�
            /// </summary>
            ColumnsMark14 = ColumnsMark13 + PerformWith,//570

            /// <summary>
            ///������Һ������ߣ�
            /// </summary>
            ColumnsMark15 = ColumnsMark14 + PerformWith,//615

            /// <summary>
            /// θҺ������ߣ�
            /// </summary>
            ColumnsMark16 = ColumnsMark15 + PerformWith,//660

            /// <summary>
            /// ��ѹ����Ѫ��ҩ�����ߣ�
            /// </summary>
            ColumnsMark17 = ColumnsMark16 + PerformWith,//705

            /// <summary>
            ///ǿ����������ߣ�
            /// </summary>
            ColumnsMark18 = ColumnsMark17 + PerformWith * 2,//795

            /// <summary>
            ///����ҩ�����ߣ�
            /// </summary>
            ColumnsMark19 = ColumnsMark18 + PerformWith,//840

            /// <summary>
            /// ��־������ߣ�
            /// </summary>
            ColumnsMark20 = ColumnsMark19 + PerformWith,//885

            /// <summary>
            /// ѭ��>>���£�����ߣ�
            /// </summary>
            ColumnsMark21 = ColumnsMark20 + PerformWith,//930
                                                        /// <summary>
                                                        ///ѭ��>>���ʣ�����ߣ�
                                                        /// </summary>
            ColumnsMark22 = ColumnsMark21 + PerformWith,//975
                                                        /// <summary>
                                                        /// Ѫѹ������ߣ�
                                                        /// </summary>
            ColumnsMark23 = ColumnsMark22 + PerformWith,//1010
                                                        /// <summary>
                                                        /// ѭ��>>cvp������ߣ�
                                                        /// </summary>
            ColumnsMark24 = ColumnsMark23 + PerformWith,//1055
                                                        /// <summary>
                                                        ///�������ͺţ�����ߣ�
                                                        /// </summary>
            ColumnsMark25 = ColumnsMark24 + PerformWith,//1100
                                                        /// <summary>
                                                        /// ����������ߣ�
                                                        /// </summary>
            ColumnsMark26 = ColumnsMark25 + PerformWith * 2,//1190
                                                            /// <summary>
                                                            /// FiO2������ߣ�
                                                            /// </summary>
            ColumnsMark27 = ColumnsMark26 + PerformWith,//1235
                                                        /// <summary>
                                                        /// ����ѹ������ߣ�
                                                        /// </summary>
            ColumnsMark28 = ColumnsMark27 + PerformWith,//1280
                                                        /// <summary>
                                                        ///TV������ߣ�
                                                        /// </summary>
            ColumnsMark29 = ColumnsMark28 + PerformWith * 2,//1370
            ColumnsMark30 = ColumnsMark29 + PerformWith,//1415
                                                        /// <summary>
                                                        /// ������������ߣ�
                                                        /// </summary>
            ColumnsMark31 = ColumnsMark30 + PerformWith,//1460
                                                        /// <summary>
                                                        ///̵ɫ������ߣ�
                                                        /// </summary>
            ColumnsMark32 = ColumnsMark31 + PerformWith * 2,//1550
                                                            /// <summary>
                                                            /// ��λ������ߣ�
                                                            /// </summary>
            ColumnsMark33 = ColumnsMark32 + PerformWith,//1595
                                                        /// <summary>
                                                        /// ��ע������ߣ�
                                                        /// </summary>
            ColumnsMark34 = ColumnsMark33 + PerformWith,//1640

        }
        private enum enmRecordBackRectangleInfo //����
        {//A3ֽ�ţ����� ��1620*1024 Size
            /// <summary>
            /// ��ѿ��
            /// </summary>
            PerformWith = 45,
            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 175,
            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = 25,
            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = 1620 - 30,
            /// <summary>
            /// ��һ�������(X),WBC������ߣ�
            /// </summary>			
            ColumnsMark1 = 75,

            /// <summary>
            /// �ڶ��������(X)��Hb������ߣ�
            /// </summary>
            ColumnsMark2 = ColumnsMark1 + PerformWith,//105

            /// <summary>
            /// ��3�������(X)��RBC������ߣ�
            /// </summary>
            ColumnsMark3 = ColumnsMark2 + PerformWith,//135

            /// <summary>
            /// ��4�������(X)��HCT������ߣ�
            /// </summary>
            ColumnsMark4 = ColumnsMark3 + PerformWith,//165

            /// <summary>
            ///  ��5�������(X)��PLT������ߣ�
            /// </summary>
            ColumnsMark5 = ColumnsMark4 + PerformWith,//195

            /// <summary>
            /// PH������ߣ�
            /// </summary>
            ColumnsMark6 = ColumnsMark5 + PerformWith,//225

            /// <summary>
            ///  PCO3   ������ߣ�
            /// </summary>
            ColumnsMark7 = ColumnsMark6 + PerformWith,//270

            /// <summary>
            ///PaO2������ߣ�
            /// </summary>
            ColumnsMark8 = ColumnsMark7 + PerformWith,//300

            /// <summary>
            ///HCO3������ߣ�
            /// </summary>
            ColumnsMark9 = ColumnsMark8 + PerformWith,//345

            /// <summary>
            ///BE������ߣ�
            /// </summary>
            ColumnsMark10 = ColumnsMark9 + PerformWith,//390

            /// <summary>
            ///K+������ߣ�
            /// </summary>
            ColumnsMark11 = ColumnsMark10 + PerformWith,//435

            /// <summary>
            /// Na+������ߣ�
            /// </summary>
            ColumnsMark12 = ColumnsMark11 + PerformWith,//480

            /// <summary>
            ///CL-������ߣ�
            /// </summary>
            ColumnsMark13 = ColumnsMark12 + PerformWith,//525

            /// <summary>
            ///Ca++������ߣ�
            /// </summary>
            ColumnsMark14 = ColumnsMark13 + PerformWith,//570

            /// <summary>
            ///GLU������ߣ�
            /// </summary>
            ColumnsMark15 = ColumnsMark14 + PerformWith,//615

            /// <summary>
            /// BUN������ߣ�
            /// </summary>
            ColumnsMark16 = ColumnsMark15 + PerformWith,//660

            /// <summary>
            /// UA������ߣ�
            /// </summary>
            ColumnsMark17 = ColumnsMark16 + PerformWith,//705

            /// <summary>
            ///JI������ߣ�
            /// </summary>
            ColumnsMark18 = ColumnsMark17 + PerformWith,//795

            /// <summary>
            ///CO2CP������ߣ�
            /// </summary>
            ColumnsMark19 = ColumnsMark18 + PerformWith,//840

            /// <summary>
            ///PT������ߣ�
            /// </summary>
            ColumnsMark20 = ColumnsMark19 + PerformWith,//885

            /// <summary>
            ///X�߼�飨����ߣ�
            /// </summary>
            ColumnsMark21 = ColumnsMark20 + PerformWith,//930
            /// <summary>
            ///ACT������ߣ�
            /// </summary>
            ColumnsMark22 = ColumnsMark21 + PerformWith * 3,//975
            /// <summary>
            /// ���أ�����ߣ�
            /// </summary>
            ColumnsMark23 = ColumnsMark22 + (int)PerformWith,//1010
            /// <summary>
            /// ���ף�����ߣ�
            /// </summary>
            ColumnsMark24 = ColumnsMark23 + PerformWith,//1055
            /// <summary>
            ///ǱѪ������ߣ�
            /// </summary>
            ColumnsMark25 = ColumnsMark24 + PerformWith,//1100
            /// <summary>
            ///  ������ߣ�
            /// </summary>
            ColumnsMark26 = ColumnsMark25 + PerformWith,//1190
            ///  <summary>
            ///  Ƥ�� ������ߣ�
            /// </summary>
            ColumnsMark27 = ColumnsMark26 + PerformWith,//1235
            /// <summary>
            ///  ������ϴ ������ߣ�
            /// </summary>
            ColumnsMark28 = ColumnsMark27 + PerformWith,//1280
            /// <summary>
            ///��ԡ������ߣ�
            /// </summary>
            ColumnsMark29 = ColumnsMark28 + PerformWith,//1370
            /// <summary>
            ///��ǻ��������ߣ�
            /// </summary>
            ColumnsMark30 = ColumnsMark29 + PerformWith,//1415
            /// <summary>
            ///     ������ߣ�
            /// </summary>
            ColumnsMark31 = ColumnsMark30 + PerformWith//1415


        }
        #endregion
        #region ��ӡʵ��
        /// <summary>
        /// ��ӡ��ʼ
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
        public void m_mthBeginPrint(object p_objPrintArg)
        {
            m_mthBeginPrintSub((PrintEventArgs)p_objPrintArg);
        }
        // ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {

        }
        /// <summary>
        /// ��ӡ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintPageEventArgs���͵Ķ���</param>
        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
        }

        /// <summary>
        /// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
        public void m_mthEndPrint(object p_objPrintArg)
        {
            if (m_objPrintInfo == null || m_blnIsFromDataSource == false || m_objPrintInfo.m_strInPatientID == "") return;
            //�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            if (!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_blnIsFirstPrintArr != null)
            {
                ArrayList arlRecordType = new ArrayList();
                ArrayList arlOpenDate = new ArrayList();
                int intUpdateIndex = -1;//��û���κμ�¼
                for (int i = 0; i < m_objPrintInfo.m_blnIsFirstPrintArr.Length; i++)
                {
                    if (m_objPrintInfo.m_blnIsFirstPrintArr[i])
                    {
                        //���¼�¼��ֻ��ʹ���µ��״δ�ӡʱ����Ϊ��Ч�����������
                        //��ż�¼����
                        arlRecordType.Add(m_objPrintInfo.m_objTransDataArr[i].m_intFlag);
                        //��ż�¼��OpenDate
                        arlOpenDate.Add(m_objPrintInfo.m_objTransDataArr[i].m_objRecordContent.m_dtmOpenDate);
                        intUpdateIndex = i;
                    }
                }

                if (intUpdateIndex >= 0)
                {
                    m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatientID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), (int[])arlRecordType.ToArray(typeof(int)), (DateTime[])arlOpenDate.ToArray(typeof(DateTime)), m_objPrintInfo.m_dtmFirstPrintDateArr[intUpdateIndex]);
                }
                m_objPrintInfo.m_objTransDataArr = null;
                m_objPrintInfo.m_blnIsFirstPrintArr = null;
            }
            m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
        }

        // ��ӡҳ��ÿ��ӡһҳ������һ�Σ��Ǵ�ӡ�������õĺ�����
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {
            try
            {

                if (m_objPrintInfo.m_strInPatientID == "" || m_objPrintDataArr == null || m_objPrintDataArr.Length == 0)
                    return;

                if (m_intNowPage % 2 != 0)//��ӡ����
                {
                    m_mthPrintTitleInfo(p_objPrintPageArg);
                    m_mthPrintFrontRectangleInfo(p_objPrintPageArg);
                    m_mthPrintHeaderInfo(p_objPrintPageArg);

                    if (m_intRecordCount > 0)
                        while (m_intFrontCurrentRecord < m_intRecordCount)
                        {

                            if (m_intFrontCurrentRecord == 0)
                                m_intSetPrintOneValueRows(p_objPrintPageArg);
                            if (m_blnCheckPageChange(ref m_intFrontPosY, p_objPrintPageArg) == true)
                                break;

                            m_blnBeginPrintNewRecord = m_blnPrintOneValue(p_objPrintPageArg, m_intFrontPosY, ref m_intFrontCurrentRecord);

                            if (m_blnBeginPrintNewRecord)
                            {

                                if (m_blnCheckPageChange(ref m_intFrontPosY, p_objPrintPageArg) == true)
                                    break;
                            }

                        }
                }
                else//��ӡ����
                {

                    m_mthPrintBackRectangleInfo(p_objPrintPageArg);
                    m_mthPrintHeaderInfo(p_objPrintPageArg);
                    if (m_intRecordCount > 0)
                        while (m_intBackCurrentRecord < m_intRecordCount)
                        {

                            if (m_intBackCurrentRecord == 0)
                                m_intSetPrintOneValueRows(p_objPrintPageArg);
                            if (m_blnCheckPageChange(ref m_intBackPosY, p_objPrintPageArg) == true)
                                break;

                            m_blnBeginPrintNewRecord = m_blnPrintOneValue(p_objPrintPageArg, m_intBackPosY, ref m_intBackCurrentRecord);

                            if (m_blnBeginPrintNewRecord)
                            {

                                if (m_blnCheckPageChange(ref m_intBackPosY, p_objPrintPageArg) == true)
                                    break;
                            }

                        }

                }

                if (m_intNowPage < m_intTotalPageCount)
                {
                    //if (m_intNowPage % 2 != 0)
                    //  clsPublicFunction.ShowInformationMessageBox("�뷭ת��ǰҳ�������ӡ���棡");
                    m_intNowPage++;
                    p_objPrintPageArg.HasMorePages = true;
                }
                else
                {


                    m_intBackCurrentRecord = 0;
                    m_intNowPage = 1;
                    m_intTotalPageCount = 2;
                    m_intFrontCurrentRecord = 0;
                    m_intFrontPosY = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
                    m_intBackPosY = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
                    p_objPrintPageArg.HasMorePages = false;
                }

            }
            catch (Exception err)
            {
                clsPublicFunction.ShowInformationMessageBox(err.Message + "\r\n" + err.StackTrace);
            }
        }
        /// <summary>
        /// ����Ƿ�ҳ,true:��ҳ��false:����ҳ
        /// </summary>
        /// <param name="p_intYBottom">Ҫ���ĵ���Y����</param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool m_blnCheckPageChange(ref int p_intYBottom, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (p_intYBottom + 100 >= 1015)
            {

                p_intYBottom = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
                this.m_intTotalPageCount += 2;
                return true;
            }
            else
                return false;
        }

        // ��ӡ����ʱ�Ĳ���
        private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
        }
        //�����ݵĲ���
        private void m_mthPrintInBlock(ref int p_intPosY, int p_intLeftX, int p_intRightX, string p_strText, System.Drawing.Printing.PrintPageEventArgs e)
        {

            RectangleF rtg = new RectangleF(p_intLeftX + 1, p_intPosY + 2, p_intRightX - p_intLeftX, 20);
            SizeF szfText = e.Graphics.MeasureString(p_strText, this.m_fotSmallFont, Convert.ToInt32(rtg.Width));
            rtg.Height = szfText.Height;
            rtg.Y = p_intPosY + 4;
            e.Graphics.DrawString(p_strText, this.m_fotSmallFont, Brushes.Black, rtg);
            p_intPosY += Convert.ToInt32(rtg.Height);
        }
        #endregion
        #region �������ֲ���
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string strPrintText = "";
            int m_objPosY = 60;

            e.Graphics.DrawString("�� Ѫ �� �� �� �� �� �� ¼", this.m_fotTitleFont, this.m_slbBrush, clsPrintPosition.c_intLeftX + 150, m_objPosY);
            e.Graphics.DrawString("____________________________", this.m_fotTitleFont, this.m_slbBrush, clsPrintPosition.c_intLeftX + 135, m_objPosY + 4);
            e.Graphics.DrawString("____________________________", this.m_fotTitleFont, this.m_slbBrush, clsPrintPosition.c_intLeftX + 135, m_objPosY + 5);
            m_objPosY += 50;


            strPrintText = "����:" + m_objPrintInfo.m_strPatientName + "  " + "������:" + m_objPrintInfo.m_strInPatientID + "  " + "����:" + m_objPrintInfo.m_strAge + "  " + "����:";
            if (m_intFlag >= 0)
            {
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_dblWEITHT.ToString() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_dblWEITHT + " kg  ������";
                else
                    strPrintText += "_______kg  ������";
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strAFTEROPDAYS.Trim() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strAFTEROPDAYS + "��   �������ƣ�";
                else
                    strPrintText += "______��   �������ƣ�";
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPNAME.Trim() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPNAME;
                else
                    strPrintText += "__________________";
                e.Graphics.DrawString(strPrintText, m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX, m_objPosY + 3);
                strPrintText = "1.";
                m_objPosY += 40;
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPMEDICINE1.Trim() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPMEDICINE1 + " 2.";
                else
                    strPrintText += "_____________ 2.";
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPMEDICINE2.Trim() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPMEDICINE2 + " 3.";
                else
                    strPrintText += "_____________ 3.";
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPMEDICINE3.Trim() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPMEDICINE3 + " 4.";
                else
                    strPrintText += "_____________ 4.";
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPMEDICINE4.Trim() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPMEDICINE1 + " 5.";
                else
                    strPrintText += "_____________ 5.";
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPMEDICINE5.Trim() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOPMEDICINE1;
                else
                    strPrintText += "_____________";
                e.Graphics.DrawString(strPrintText, m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX, m_objPosY);

                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_dtmRECORDDATE.ToString() != "")
                    strPrintText = m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_dtmRECORDDATE.ToString("yyyy��MM��dd��") + "  ��  " + (m_intNowPage / 2 + 1) + "  ҳ";
                else
                    strPrintText = "________��_______��________��    " + "  ��  " + (m_intNowPage / 2 + 1) + "  ҳ";

                e.Graphics.DrawString(strPrintText, m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.RightX - 250, m_objPosY);

                strPrintText = "����ǩ��:";
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strLONGCLASSSIGNID.Trim() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strLONGCLASSSIGNID + "  �칫��ǩ��:";
                else
                    strPrintText += "_____________   �칫��ǩ��:";
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOFFICESIGNID.Trim() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strOFFICESIGNID + "   ҹ��ǩ��(С):";
                else
                    strPrintText += "_____________   ҹ��ǩ��(С):";
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strSMALLNIGHTCLASSSIGNID.Trim() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strSMALLNIGHTCLASSSIGNID + "(��)";
                else
                    strPrintText += "_____________(��)";
                if (m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strBIGNIGHTCLASSSIGNID.Trim() != "")
                    strPrintText += m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_strBIGNIGHTCLASSSIGNID;
                else
                    strPrintText += "_____________";

                e.Graphics.DrawString(strPrintText, m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + 100, 1035);
                e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.RightX - 200, 1035);
            }
            else
            {
                strPrintText += "_____kg ������____��     ��������:_____________________";
                e.Graphics.DrawString(strPrintText, m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX, m_objPosY);
                m_objPosY += 40;
                strPrintText = "1.____________ 2.____________ 3.____________ 4.____________ 5.____________ ";
                e.Graphics.DrawString(strPrintText, m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX, m_objPosY);
                strPrintText = "________��_______��________��    " + "  �� " + (m_intNowPage / 2 + 1) + " ҳ";
                e.Graphics.DrawString(strPrintText, m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.RightX - 250, m_objPosY);
                strPrintText = "����ǩ��___________ �칫��ǩ��___________  ҹ��ǩ��(С)______________________(��)____________________";
                e.Graphics.DrawString(strPrintText, m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + 100, 1035);
                e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.RightX - 200, 1035);
            }

        }
        #endregion
        #region �������ͷ����
        /// <summary>
        ///  �������ͷ����
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintFrontRectangleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {

            #region �����Ӻ���
            int m_intPosY1 = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
            for (int i1 = 0; i1 < 5; i1++)//����
            {
                if (i1 == 0)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX,
                        (int)enmRecordRectangleInfo.TopY,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY);
                else if (i1 == 4)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3);
                else if (i1 == 1)
                {
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.ColumnsMark20,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1,
                        (int)enmRecordRectangleInfo.ColumnsMark34,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1);
                }
                else if (i1 == 2)
                {
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.ColumnsMark1,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1,
                        (int)enmRecordRectangleInfo.ColumnsMark17,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1);
                }
                else if (i1 == 3)
                {
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.ColumnsMark31,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark2,
                        (int)enmRecordRectangleInfo.ColumnsMark32,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark2);
                }
            }
            for (int i2 = 0; i2 < 33; i2++)//33������
            {
                m_intPosY1 += 20;
                e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX, m_intPosY1, (int)enmRecordRectangleInfo.RightX, m_intPosY1);
            }



            #endregion �����Ӻ���
            #region ����������
            int intXPos = (int)enmRecordRectangleInfo.LeftX;
            int intXPos1 = (int)enmRecordRectangleInfo.LeftX;
            int intYTop = (int)enmRecordRectangleInfo.TopY;
            int intYBottom = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
            int intYFirstBottom = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1;
            int intYSecBottom = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark2;

            #region ����б��
            intXPos = (int)enmRecordRectangleInfo.ColumnsMark6;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark7;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark20;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark21;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark21;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark22;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark22;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark23;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark23;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark24;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark24;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark25;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark25;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark26;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark27;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark28;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark28;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark29;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark29;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark30;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark31;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark32;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYSecBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark32;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark33;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark33;
            intXPos1 = (int)enmRecordRectangleInfo.ColumnsMark34;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYBottom, intXPos1, intYFirstBottom);
            #endregion

            intYBottom = 1015;
            //���������
            intXPos = (int)enmRecordRectangleInfo.LeftX;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark1;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark2;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);


            intXPos = (int)enmRecordRectangleInfo.ColumnsMark3;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark4;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark5;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark6;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark7;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark8;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark9;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark10;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark11;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark12;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark13;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark14;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark15;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark16;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark17;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark18;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark19;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark20;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark21;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark22;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark23;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark24;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark25;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark26;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark27;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark28;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark29;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark30;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark31;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark32;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark33;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.ColumnsMark34;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.RightX;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);


            #endregion

        }
        #endregion
        #region �������ͷ����
        /// <summary>
        ///  �������ͷ����
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintBackRectangleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {

            #region �����Ӻ���
            int m_intPosY1 = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
            for (int i1 = 0; i1 < 4; i1++)
            {
                if (i1 == 0)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordBackRectangleInfo.LeftX,
                        (int)enmRecordBackRectangleInfo.TopY,
                        (int)enmRecordBackRectangleInfo.RightX,
                        (int)enmRecordBackRectangleInfo.TopY);
                else if (i1 == 3)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordBackRectangleInfo.LeftX,
                        (int)enmRecordBackRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3,
                        (int)enmRecordBackRectangleInfo.RightX,
                        (int)enmRecordBackRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3);
                else if (i1 == 1)
                {
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordBackRectangleInfo.ColumnsMark1,
                        (int)enmRecordBackRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1,
                        (int)enmRecordBackRectangleInfo.ColumnsMark20,
                        (int)enmRecordBackRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1);
                }
                else if (i1 == 2)
                {
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordBackRectangleInfo.ColumnsMark23,
                        (int)enmRecordBackRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1,
                        (int)enmRecordBackRectangleInfo.ColumnsMark27,
                        (int)enmRecordBackRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1);
                }

            }
            for (int i2 = 0; i2 < 33; i2++)//33������
            {
                m_intPosY1 += 20;
                e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX, m_intPosY1, (int)enmRecordRectangleInfo.RightX, m_intPosY1);
            }


            #endregion �����Ӻ���
            #region ����������
            int intXPos = (int)enmRecordBackRectangleInfo.LeftX;
            int intYTop = (int)enmRecordBackRectangleInfo.TopY;
            int intYBottom = 1015;
            int intYFirstBottom = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1;

            //���������
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark1;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark2;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);


            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark3;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark4;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark5;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark6;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark7;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark8;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark9;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark10;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark11;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark12;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark13;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark14;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark15;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark16;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark17;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark18;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark19;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark20;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark21;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark22;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark23;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark24;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark25;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark26;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYFirstBottom, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark27;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark28;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark29;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark30;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.ColumnsMark31;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordBackRectangleInfo.RightX;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);


            #endregion

        }


        #endregion
        #region ����ͷ���ӵı���		
        /// <summary>
        ///����ͷ���ӵı���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintHeaderInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {

            int[] intYPosFontArr = new int[9]{ (int)enmRecordRectangleInfo.TopY + 10,//0
											   (int)enmRecordRectangleInfo.TopY + 25,//1
											   (int)enmRecordRectangleInfo.TopY + 50,//2
											   (int)enmRecordRectangleInfo.TopY + 70,//3
											   (int)enmRecordRectangleInfo.TopY + 90,//4
											   (int)enmRecordRectangleInfo.TopY + 110,//5
											   (int)enmRecordRectangleInfo.TopY + 130,//6
											   (int)enmRecordRectangleInfo.TopY + 150,//7
											   (int)enmRecordRectangleInfo.TopY + 170//8		   
										   };
            if (m_intNowPage % 2 != 0)//��ӡ�������
            {
                e.Graphics.DrawString("ʱ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + 12, intYPosFontArr[1]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + 12, intYPosFontArr[5] + 15);

                e.Graphics.DrawString("ʵ  ��  ��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark3, intYPosFontArr[0]);
                e.Graphics.DrawString("1", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark1 + 5, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("2", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark2 + 5, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("3", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark3 + 5, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("4", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark4 + 5, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("5", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark5 + 5, intYPosFontArr[5] - 10);

                e.Graphics.DrawString("ȫ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark6, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("Ѫ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark6, intYPosFontArr[1] + 30);

                e.Graphics.DrawString("Ѫ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark7 - 20, intYPosFontArr[7] - 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark7 - 20, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("����", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark9 - 20, intYPosFontArr[0]);
                e.Graphics.DrawString("ÿ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark8 + 10, intYPosFontArr[2]);
                e.Graphics.DrawString("ʱ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark8 + 10, intYPosFontArr[7]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark9 + 10, intYPosFontArr[2]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark9 + 10, intYPosFontArr[7]);

                e.Graphics.DrawString("����", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark11 - 20, intYPosFontArr[0]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark10 + 10, intYPosFontArr[2]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark10 + 10, intYPosFontArr[7]);
                e.Graphics.DrawString("ÿ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark11 + 10, intYPosFontArr[2]);
                e.Graphics.DrawString("ʱ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark11 + 10, intYPosFontArr[7]);

                e.Graphics.DrawString("ʵ  ��  ��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark13, intYPosFontArr[0]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark12 + 10, intYPosFontArr[2]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark12 + 10, intYPosFontArr[3] + 15);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark12 + 10, intYPosFontArr[4] + 25);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark12 + 10, intYPosFontArr[5] + 40);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark13 + 10, intYPosFontArr[2]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark13 + 10, intYPosFontArr[7]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark14 + 10, intYPosFontArr[2]);
                e.Graphics.DrawString("Һ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark14 + 10, intYPosFontArr[7]);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark15 + 10, intYPosFontArr[2]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark15 + 10, intYPosFontArr[3] + 15);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark15 + 10, intYPosFontArr[4] + 25);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark15 + 10, intYPosFontArr[5] + 40);
                e.Graphics.DrawString("θ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark16 + 10, intYPosFontArr[2]);
                e.Graphics.DrawString("Һ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark16 + 10, intYPosFontArr[7]);

                e.Graphics.DrawString("��ѹ����", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark17 + 5, intYPosFontArr[1]);
                e.Graphics.DrawString("Ѫ��ҩ��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark17 + 5, intYPosFontArr[4]);
                e.Graphics.DrawString("ug/kg/min", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark17, intYPosFontArr[7]);

                e.Graphics.DrawString("ǿ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark18 + 10, intYPosFontArr[1]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark18 + 10, intYPosFontArr[3]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark18 + 10, intYPosFontArr[5]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark18 + 10, intYPosFontArr[7]);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark19 + 10, intYPosFontArr[1]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark19 + 10, intYPosFontArr[3]);
                e.Graphics.DrawString("ҩ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark19 + 10, intYPosFontArr[5]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark19 + 10, intYPosFontArr[7]);

                e.Graphics.DrawString("��־", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark20 + 1, intYPosFontArr[0]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark20, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("ʶ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark20, intYPosFontArr[1] + 30);
                e.Graphics.DrawString("ͫ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark21 - 20, intYPosFontArr[7] - 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark21 - 20, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("ѭ          ��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark21 + 20, intYPosFontArr[0]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark21, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark21, intYPosFontArr[1] + 30);
                e.Graphics.DrawString("ĩ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark22 - 20, intYPosFontArr[6] - 10);
                e.Graphics.DrawString("�� ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark22 - 20, intYPosFontArr[7] - 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark22 - 20, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark22, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark22, intYPosFontArr[1] + 30);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark23 - 20, intYPosFontArr[7] - 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark23 - 20, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("Ѫ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark23, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("ѹ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark23, intYPosFontArr[1] + 30);
                e.Graphics.DrawString("ƽ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark24 - 20, intYPosFontArr[6] - 10);
                e.Graphics.DrawString("�� ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark24 - 20, intYPosFontArr[7] - 10);
                e.Graphics.DrawString("ѹ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark24 - 20, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("CVP", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark24, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("LAP", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark25 - 30, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("��                         ��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark26 + 40, intYPosFontArr[0]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark25, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark25, intYPosFontArr[1] + 30);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark25, intYPosFontArr[1] + 50);
                e.Graphics.DrawString("�� ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark25, intYPosFontArr[1] + 70);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark25, intYPosFontArr[1] + 90);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark26 - 20, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("�� ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark26 - 20, intYPosFontArr[6] - 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark26 - 20, intYPosFontArr[7] - 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark26 - 20, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark26 + 10, intYPosFontArr[2]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark26 + 10, intYPosFontArr[3] + 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark26 + 10, intYPosFontArr[4] + 20);
                e.Graphics.DrawString("ʽ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark26 + 10, intYPosFontArr[5] + 30);

                e.Graphics.DrawString("F O", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark27, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("i 2", m_fotTinyFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark27 + 8, intYPosFontArr[1] + 15);
                e.Graphics.DrawString("(%)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark27, intYPosFontArr[1] + 30);
                e.Graphics.DrawString("I:E", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark27 + 10, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark28, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark28, intYPosFontArr[1] + 30);
                e.Graphics.DrawString("ѹ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark28, intYPosFontArr[1] + 50);
                e.Graphics.DrawString("PEEP", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark28 + 40, intYPosFontArr[7] - 10);
                e.Graphics.DrawString("(CmH O)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark28 + 18, intYPosFontArr[8] - 10);
                e.Graphics.DrawString("   2", m_fotTinyFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark28 + 30, intYPosFontArr[8] - 2);

                e.Graphics.DrawString("TV", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark29, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("ml", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark29, intYPosFontArr[1] + 30);
                e.Graphics.DrawString("VF", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark29 + 20, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark30 + 10, intYPosFontArr[2]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark30 + 10, intYPosFontArr[3] + 15);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark30 + 10, intYPosFontArr[4] + 25);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark30 + 10, intYPosFontArr[5] + 35);

                e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark31 + 10, intYPosFontArr[2] - 5);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark31, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark2 + 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark32 - 20, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("̵", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark32, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("ɫ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark32, intYPosFontArr[1] + 30);
                e.Graphics.DrawString("̵", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark33 - 20, intYPosFontArr[7] - 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark33 - 20, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark33, intYPosFontArr[1] + 10);
                e.Graphics.DrawString("λ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark33, intYPosFontArr[1] + 30);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark34 - 20, intYPosFontArr[7] - 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark34 - 20, intYPosFontArr[8] - 10);

                e.Graphics.DrawString("��  ע", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.ColumnsMark34 + 35, intYPosFontArr[4] - 5);
            }
            else//��ӡ����
            {
                e.Graphics.DrawString("ʱ", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + 12, intYPosFontArr[1]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + 12, intYPosFontArr[5] + 15);

                e.Graphics.DrawString("Ѫ  ��  ��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark3 - 5, intYPosFontArr[0]);
                e.Graphics.DrawString("WBC", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark1 + 10, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("Hb", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark2 + 12, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("RBC", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark3 + 10, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("HCT", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark4 + 10, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("PLT", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark5 + 10, intYPosFontArr[5] - 10);

                e.Graphics.DrawString("Ѫ  ��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark8 - 5, intYPosFontArr[0]);
                e.Graphics.DrawString("PH", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark6 + 10, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("PCO", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark7 + 5, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("   2", m_fotTinyFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark7 + 5, intYPosFontArr[5] - 1);
                e.Graphics.DrawString("PaO", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark8 + 5, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("   2", m_fotTinyFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark8 + 5, intYPosFontArr[5] - 1);
                e.Graphics.DrawString("HCO", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark9 + 5, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("   3", m_fotTinyFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark9 + 5, intYPosFontArr[5] - 1);
                e.Graphics.DrawString("BE", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark10 + 10, intYPosFontArr[5] - 10);

                e.Graphics.DrawString("Ѫ �� �� ��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark13 - 10, intYPosFontArr[0]);
                e.Graphics.DrawString("K", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark11 + 12, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("  +", m_fotTinyFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark11 + 7, intYPosFontArr[5] - 12);
                e.Graphics.DrawString("Na", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark12 + 10, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("  +", m_fotTinyFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark12 + 10, intYPosFontArr[5] - 12);
                e.Graphics.DrawString("Cl", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark13 + 7, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("  -", m_fotTinyFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark13 + 7, intYPosFontArr[5] - 12);
                e.Graphics.DrawString("Ca", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark14 + 5, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("  ++", m_fotTinyFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark14 + 5, intYPosFontArr[5] - 12);
                e.Graphics.DrawString("GLU", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark15 + 5, intYPosFontArr[5] - 10);


                e.Graphics.DrawString("Ѫ Һ �� ��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark17 + 5, intYPosFontArr[0]);
                e.Graphics.DrawString("BUN", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark16 + 5, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("UA", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark17 + 10, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark18 + 12, intYPosFontArr[5] - 30);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark18 + 12, intYPosFontArr[5] + 10);
                e.Graphics.DrawString("CO CP", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark19 + 1, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("  2", m_fotTinyFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark19 + 2, intYPosFontArr[5] - 1);

                e.Graphics.DrawString("PT", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark20 + 12, intYPosFontArr[5] - 30);

                e.Graphics.DrawString("X  ��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark21 + 45, intYPosFontArr[5] - 50);
                e.Graphics.DrawString("�� ��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark21 + 45, intYPosFontArr[5] - 10);

                e.Graphics.DrawString("ACT", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark22 + 5, intYPosFontArr[5] - 30);

                e.Graphics.DrawString("�� �� ��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark24 + 5, intYPosFontArr[0]);
                e.Graphics.DrawString("����", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark23 + 5, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("����", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark24 + 5, intYPosFontArr[5] - 10);
                e.Graphics.DrawString("ǱѪ", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark25 + 5, intYPosFontArr[5] - 10);

                e.Graphics.DrawString("Ƥ", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark27 + 12, intYPosFontArr[1]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark27 + 12, intYPosFontArr[5] + 15);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark28 + 12, intYPosFontArr[1]);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark28 + 12, intYPosFontArr[2] + 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark28 + 12, intYPosFontArr[3] + 20);
                e.Graphics.DrawString("ϴ", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark28 + 12, intYPosFontArr[5] + 15);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark29 + 12, intYPosFontArr[1]);
                e.Graphics.DrawString("ԡ", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark29 + 12, intYPosFontArr[5] + 15);

                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark30 + 12, intYPosFontArr[1]);
                e.Graphics.DrawString("ǻ", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark30 + 12, intYPosFontArr[2] + 10);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark30 + 12, intYPosFontArr[3] + 20);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordBackRectangleInfo.ColumnsMark30 + 12, intYPosFontArr[5] + 15);

            }
        }
        #endregion
        #region ��ӡһ����ֵ,���жϵ�ǰ��¼�Ƿ��ӡ��
        /// <summary>
        /// ��ӡһ����ֵ,���жϵ�ǰ��¼�Ƿ��ӡ��
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p_intBottomY"></param>
        /// <returns></returns>
        private bool m_blnPrintOneValue(System.Drawing.Printing.PrintPageEventArgs e, int p_intBottomY, ref int m_intCurrentRecord)
        {

            #region ���޸�˳���ӡ��ǰ��¼��ĳһ��
            bool blnIsRecordFinish = m_blnPrintOneRowValue(m_strValueArr, ref m_intCurrentRecord, e, p_intBottomY);
            if (blnIsRecordFinish)
                return false;
            else
                return true;

            #endregion
        }
        #endregion ��ӡһ����ֵ,���жϵ�ǰ��¼�Ƿ��ӡ��
        #region ��ӡһ����ֵ
        /// <summary>
        /// ��ӡһ����ֵ
        /// </summary>
        /// <param name="p_strValueArr">��ֵ(�ӡ�ʱ�䡱������ǻ����)</param>
        /// <param name="p_intNowRowInOneRecord">�ڼ��εĽ��:�ȼ���NowRowInOneRecord</param>
        /// <param name="e">��ӡ����</param>
        /// <param name="p_intPosY">Y����</param>
        private bool m_blnPrintOneRowValue(string[][] p_strValueArr, ref int p_intNowRowInOneRecord, System.Drawing.Printing.PrintPageEventArgs e, int p_intPosY)
        {
            string strPrintText = "";


            int intMedicine_PupilRows = 0;//�洢ҩ���ͫ������������
            string[] strEXPANDVASMEDICINE = null;//��ѹ����Ѫ��ҩ��
            string[] strCARDIACDIURESIS = null;//ǿ������
            string[] strOTHERMEDICINE = null;//����ҩ��
            string[] strConArr = null;
            if (p_intNowRowInOneRecord < m_intRecordCount)
            {
                if (m_intNowPage % 2 != 0)
                {
                    for (int i0 = p_intNowRowInOneRecord; i0 < m_intRecordCount - 1; i0++)
                    {
                        if (p_strValueArr[p_intNowRowInOneRecord][0].Trim() == p_strValueArr[i0 + 1][0].Trim())
                            p_intNowRowInOneRecord++;

                    }
                    for (int j0 = 0; j0 < m_intFrontPosArr.Length; j0++)
                        m_intFrontPosArr[j0] = p_intPosY;
                    //ʱ��
                    if (p_strValueArr[p_intNowRowInOneRecord][0].Trim() != "")
                    {

                        strPrintText = DateTime.Parse(p_strValueArr[p_intNowRowInOneRecord][0].ToString()).ToString("HH:mm");

                        m_mthPrintInBlock(ref m_intFrontPosArr[0], (int)enmRecordRectangleInfo.LeftX, (int)enmRecordRectangleInfo.ColumnsMark1, strPrintText, e);
                    }
                    //ʵ����1
                    if (p_strValueArr[p_intNowRowInOneRecord][1].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][1];
                        m_mthPrintInBlock(ref m_intFrontPosArr[1], (int)enmRecordRectangleInfo.ColumnsMark1, (int)enmRecordRectangleInfo.ColumnsMark2, strPrintText, e);
                    }
                    //2
                    if (p_strValueArr[p_intNowRowInOneRecord][2].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][2];
                        m_mthPrintInBlock(ref m_intFrontPosArr[2], (int)enmRecordRectangleInfo.ColumnsMark2, (int)enmRecordRectangleInfo.ColumnsMark3, strPrintText, e);
                    }
                    //3
                    if (p_strValueArr[p_intNowRowInOneRecord][3].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][3];
                        m_mthPrintInBlock(ref m_intFrontPosArr[3], (int)enmRecordRectangleInfo.ColumnsMark3, (int)enmRecordRectangleInfo.ColumnsMark4, strPrintText, e);
                    }
                    //4
                    if (p_strValueArr[p_intNowRowInOneRecord][4].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][4];
                        m_mthPrintInBlock(ref m_intFrontPosArr[4], (int)enmRecordRectangleInfo.ColumnsMark4, (int)enmRecordRectangleInfo.ColumnsMark5, strPrintText, e);
                    }
                    //5
                    if (p_strValueArr[p_intNowRowInOneRecord][5].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][5];
                        m_mthPrintInBlock(ref m_intFrontPosArr[5], (int)enmRecordRectangleInfo.ColumnsMark5, (int)enmRecordRectangleInfo.ColumnsMark6, strPrintText, e);
                    }
                    //ȫѪ/Ѫ��
                    if (p_strValueArr[p_intNowRowInOneRecord][6].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][6];
                        m_mthPrintInBlock(ref m_intFrontPosArr[6], (int)enmRecordRectangleInfo.ColumnsMark6, (int)enmRecordRectangleInfo.ColumnsMark7, strPrintText, e);
                    }
                    //����/ÿʱ
                    if (p_strValueArr[p_intNowRowInOneRecord][7].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][7];
                        m_mthPrintInBlock(ref m_intFrontPosArr[7], (int)enmRecordRectangleInfo.ColumnsMark8, (int)enmRecordRectangleInfo.ColumnsMark9, strPrintText, e);
                    }
                    //����/����
                    if (p_strValueArr[p_intNowRowInOneRecord][8].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][8];
                        m_mthPrintInBlock(ref m_intFrontPosArr[8], (int)enmRecordRectangleInfo.ColumnsMark9, (int)enmRecordRectangleInfo.ColumnsMark10, strPrintText, e);
                    }
                    //����/����
                    if (p_strValueArr[p_intNowRowInOneRecord][9].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][9];
                        m_mthPrintInBlock(ref m_intFrontPosArr[9], (int)enmRecordRectangleInfo.ColumnsMark10, (int)enmRecordRectangleInfo.ColumnsMark11, strPrintText, e);
                    }
                    //����/ÿʱ
                    if (p_strValueArr[p_intNowRowInOneRecord][10].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][10];
                        m_mthPrintInBlock(ref m_intFrontPosArr[10], (int)enmRecordRectangleInfo.ColumnsMark11, (int)enmRecordRectangleInfo.ColumnsMark12, strPrintText, e);
                    }
                    //ʵ����>>�ۻ�����
                    if (p_strValueArr[p_intNowRowInOneRecord][11].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][11];
                        m_mthPrintInBlock(ref m_intFrontPosArr[11], (int)enmRecordRectangleInfo.ColumnsMark12, (int)enmRecordRectangleInfo.ColumnsMark13, strPrintText, e);
                    }
                    //����
                    if (p_strValueArr[p_intNowRowInOneRecord][12].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][12];
                        m_mthPrintInBlock(ref m_intFrontPosArr[12], (int)enmRecordRectangleInfo.ColumnsMark13, (int)enmRecordRectangleInfo.ColumnsMark14, strPrintText, e);
                    }
                    //��Һ
                    if (p_strValueArr[p_intNowRowInOneRecord][13].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][13];
                        m_mthPrintInBlock(ref m_intFrontPosArr[13], (int)enmRecordRectangleInfo.ColumnsMark14, (int)enmRecordRectangleInfo.ColumnsMark15, strPrintText, e);
                    }
                    //������Һ
                    if (p_strValueArr[p_intNowRowInOneRecord][14].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][14];
                        m_mthPrintInBlock(ref m_intFrontPosArr[14], (int)enmRecordRectangleInfo.ColumnsMark15, (int)enmRecordRectangleInfo.ColumnsMark16, strPrintText, e);
                    }
                    //θҺ
                    if (p_strValueArr[p_intNowRowInOneRecord][15].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][15];
                        m_mthPrintInBlock(ref m_intFrontPosArr[15], (int)enmRecordRectangleInfo.ColumnsMark16, (int)enmRecordRectangleInfo.ColumnsMark17, strPrintText, e);
                    }
                    //��ѹ����
                    if (p_strValueArr[p_intNowRowInOneRecord][16].Trim() != "")
                    {
                        strEXPANDVASMEDICINE = p_strValueArr[p_intNowRowInOneRecord][16].Split('��');
                        intMedicine_PupilRows = strEXPANDVASMEDICINE.Length;
                        strPrintText = "";
                        for (int i0 = 0; i0 < intMedicine_PupilRows; i0++)
                        {
                            strConArr = strEXPANDVASMEDICINE[i0].Split('��');

                            if (strConArr.Length == 2)
                            {
                                strPrintText += strConArr[0] + " " + strConArr[1] + " ";
                            }
                            else
                            {
                                strPrintText += strConArr[0] + " " + strConArr[1] + " " + strConArr[2] + " ";
                            }
                        }
                        m_mthPrintInBlock(ref m_intFrontPosArr[16], (int)enmRecordRectangleInfo.ColumnsMark17, (int)enmRecordRectangleInfo.ColumnsMark18, strPrintText, e);
                    }
                    //ǿ������
                    if (p_strValueArr[p_intNowRowInOneRecord][17].Trim() != "")
                    {

                        strCARDIACDIURESIS = p_strValueArr[p_intNowRowInOneRecord][17].Split('��');
                        intMedicine_PupilRows = strCARDIACDIURESIS.Length;
                        strPrintText = "";
                        for (int i1 = 0; i1 < intMedicine_PupilRows; i1++)
                        {
                            strConArr = strCARDIACDIURESIS[i1].Split('��');

                            if (strConArr.Length == 2)
                            {
                                strPrintText += strConArr[0] + " " + strConArr[1] + " ";
                            }
                            else
                            {
                                strPrintText += strConArr[0] + " " + strConArr[1] + " " + strConArr[2] + " ";
                            }
                        }
                        m_mthPrintInBlock(ref m_intFrontPosArr[17], (int)enmRecordRectangleInfo.ColumnsMark18, (int)enmRecordRectangleInfo.ColumnsMark19, strPrintText, e);
                    }
                    //����ҩ��
                    if (p_strValueArr[p_intNowRowInOneRecord][18].Trim() != "")
                    {
                        strOTHERMEDICINE = p_strValueArr[p_intNowRowInOneRecord][18].Split('��');

                        intMedicine_PupilRows = strOTHERMEDICINE.Length;
                        strPrintText = "";
                        for (int i2 = 0; i2 < intMedicine_PupilRows; i2++)
                        {
                            strConArr = strOTHERMEDICINE[i2].Split('��');

                            if (strConArr.Length == 2)
                            {
                                strPrintText += strConArr[0] + " " + strConArr[1] + " ";
                            }
                            else
                            {
                                strPrintText += strConArr[0] + " " + strConArr[1] + " " + strConArr[2] + " ";
                            }
                        }

                        m_mthPrintInBlock(ref m_intFrontPosArr[18], (int)enmRecordRectangleInfo.ColumnsMark19, (int)enmRecordRectangleInfo.ColumnsMark20, strPrintText, e);
                    }
                    //��־>>��ʶ/ͫ��
                    if (p_strValueArr[p_intNowRowInOneRecord][19].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][20].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][75].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][76].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][19] + "/" + p_strValueArr[p_intNowRowInOneRecord][20];
                        if (p_strValueArr[p_intNowRowInOneRecord][75].Trim() != "")
                            strPrintText += "��:" + p_strValueArr[p_intNowRowInOneRecord][75];
                        if (p_strValueArr[p_intNowRowInOneRecord][76].Trim() != "")
                            strPrintText += "��:" + p_strValueArr[p_intNowRowInOneRecord][76];
                        m_mthPrintInBlock(ref m_intFrontPosArr[19], (int)enmRecordRectangleInfo.ColumnsMark20, (int)enmRecordRectangleInfo.ColumnsMark21, strPrintText, e);
                    }
                    //ѭ��>>����/ĩ����
                    if (p_strValueArr[p_intNowRowInOneRecord][21].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][22].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][21] + "/" + p_strValueArr[p_intNowRowInOneRecord][22];
                        m_mthPrintInBlock(ref m_intFrontPosArr[21], (int)enmRecordRectangleInfo.ColumnsMark21, (int)enmRecordRectangleInfo.ColumnsMark22, strPrintText, e);
                    }
                    //����/����
                    if (p_strValueArr[p_intNowRowInOneRecord][23].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][24].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][23] + "/" + p_strValueArr[p_intNowRowInOneRecord][24];
                        m_mthPrintInBlock(ref m_intFrontPosArr[23], (int)enmRecordRectangleInfo.ColumnsMark22, (int)enmRecordRectangleInfo.ColumnsMark23, strPrintText, e);
                    }
                    //Ѫѹ/ƽ��ѹ
                    if (p_strValueArr[p_intNowRowInOneRecord][25].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][26].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][25] + "/" + p_strValueArr[p_intNowRowInOneRecord][26];
                        m_mthPrintInBlock(ref m_intFrontPosArr[25], (int)enmRecordRectangleInfo.ColumnsMark23, (int)enmRecordRectangleInfo.ColumnsMark24, strPrintText, e);
                    }
                    //CVP/LAP
                    if (p_strValueArr[p_intNowRowInOneRecord][27].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][28].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][27] + "/" + p_strValueArr[p_intNowRowInOneRecord][28];
                        m_mthPrintInBlock(ref m_intFrontPosArr[27], (int)enmRecordRectangleInfo.ColumnsMark24, (int)enmRecordRectangleInfo.ColumnsMark25, strPrintText, e);
                    }
                    //�������ͺ�/������
                    if (p_strValueArr[p_intNowRowInOneRecord][29].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][30].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][29] + "/" + p_strValueArr[p_intNowRowInOneRecord][30];
                        m_mthPrintInBlock(ref m_intFrontPosArr[29], (int)enmRecordRectangleInfo.ColumnsMark25, (int)enmRecordRectangleInfo.ColumnsMark26, strPrintText, e);
                    }
                    //������ʽ
                    if (p_strValueArr[p_intNowRowInOneRecord][31].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][31];
                        m_mthPrintInBlock(ref m_intFrontPosArr[31], (int)enmRecordRectangleInfo.ColumnsMark26, (int)enmRecordRectangleInfo.ColumnsMark27, strPrintText, e);
                    }
                    //FiO2/I:E
                    if (p_strValueArr[p_intNowRowInOneRecord][32].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][33].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][32] + "/" + p_strValueArr[p_intNowRowInOneRecord][33];
                        m_mthPrintInBlock(ref m_intFrontPosArr[32], (int)enmRecordRectangleInfo.ColumnsMark27, (int)enmRecordRectangleInfo.ColumnsMark28, strPrintText, e);
                    }
                    //����ѹ
                    if (p_strValueArr[p_intNowRowInOneRecord][34].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][35].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][34] + "/" + p_strValueArr[p_intNowRowInOneRecord][35];
                        m_mthPrintInBlock(ref m_intFrontPosArr[34], (int)enmRecordRectangleInfo.ColumnsMark28, (int)enmRecordRectangleInfo.ColumnsMark29, strPrintText, e);
                    }
                    //TV
                    if (p_strValueArr[p_intNowRowInOneRecord][36].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][37].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][36] + "/" + p_strValueArr[p_intNowRowInOneRecord][37];
                        m_mthPrintInBlock(ref m_intFrontPosArr[36], (int)enmRecordRectangleInfo.ColumnsMark29, (int)enmRecordRectangleInfo.ColumnsMark30, strPrintText, e);
                    }
                    //��������
                    if (p_strValueArr[p_intNowRowInOneRecord][38].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][38];
                        m_mthPrintInBlock(ref m_intFrontPosArr[38], (int)enmRecordRectangleInfo.ColumnsMark30, (int)enmRecordRectangleInfo.ColumnsMark31, strPrintText, e);
                    }
                    //������
                    if (p_strValueArr[p_intNowRowInOneRecord][39].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][40].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][39] + "/" + p_strValueArr[p_intNowRowInOneRecord][40];
                        m_mthPrintInBlock(ref m_intFrontPosArr[39], (int)enmRecordRectangleInfo.ColumnsMark31, (int)enmRecordRectangleInfo.ColumnsMark32, strPrintText, e);
                    }
                    //̵ɫ
                    if (p_strValueArr[p_intNowRowInOneRecord][41].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][42].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][41] + "/" + p_strValueArr[p_intNowRowInOneRecord][42];
                        m_mthPrintInBlock(ref m_intFrontPosArr[41], (int)enmRecordRectangleInfo.ColumnsMark32, (int)enmRecordRectangleInfo.ColumnsMark33, strPrintText, e);
                    }
                    //��λ/����
                    if (p_strValueArr[p_intNowRowInOneRecord][43].Trim() != "" || p_strValueArr[p_intNowRowInOneRecord][44].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][43] + "/" + p_strValueArr[p_intNowRowInOneRecord][44];
                        m_mthPrintInBlock(ref m_intFrontPosArr[43], (int)enmRecordRectangleInfo.ColumnsMark33, (int)enmRecordRectangleInfo.ColumnsMark34, strPrintText, e);
                    }
                    //��ע
                    if (p_strValueArr[p_intNowRowInOneRecord][45].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][45];
                        m_mthPrintInBlock(ref m_intFrontPosArr[45], (int)enmRecordRectangleInfo.ColumnsMark34, (int)enmRecordRectangleInfo.RightX, strPrintText, e);
                    }
                    int temp0 = 0;
                    for (int k1 = 0; k1 < m_intFrontPosArr.Length; k1++)
                    {

                        if (m_intFrontPosArr[k1] > temp0)
                            temp0 = m_intFrontPosArr[k1];

                    }
                    m_intFrontPosY = temp0;
                    p_intNowRowInOneRecord++;
                }
                else
                {
                    for (int i1 = p_intNowRowInOneRecord; i1 < m_intRecordCount - 1; i1++)
                    {
                        if (p_strValueArr[p_intNowRowInOneRecord][0].Trim() == p_strValueArr[i1 + 1][0].Trim())
                            p_intNowRowInOneRecord++;

                    }
                    for (int j0 = 0; j0 < m_intBackPosArr.Length; j0++)
                        m_intBackPosArr[j0] = p_intPosY;
                    //ʱ��
                    if (p_strValueArr[p_intNowRowInOneRecord][0].Trim() != "")
                    {
                        strPrintText = DateTime.Parse(p_strValueArr[p_intNowRowInOneRecord][0].ToString()).ToString("HH:mm");

                        m_mthPrintInBlock(ref m_intBackPosArr[0], (int)enmRecordBackRectangleInfo.LeftX, (int)enmRecordBackRectangleInfo.ColumnsMark1, strPrintText, e);
                    }
                    //WBC
                    if (p_strValueArr[p_intNowRowInOneRecord][46].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][46];
                        m_mthPrintInBlock(ref m_intBackPosArr[1], (int)enmRecordBackRectangleInfo.ColumnsMark1, (int)enmRecordBackRectangleInfo.ColumnsMark2, strPrintText, e);
                    }
                    //Hb
                    if (p_strValueArr[p_intNowRowInOneRecord][47].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][47];
                        m_mthPrintInBlock(ref m_intBackPosArr[2], (int)enmRecordBackRectangleInfo.ColumnsMark2, (int)enmRecordBackRectangleInfo.ColumnsMark3, strPrintText, e);
                    }
                    //RBC
                    if (p_strValueArr[p_intNowRowInOneRecord][48].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][48];
                        m_mthPrintInBlock(ref m_intBackPosArr[3], (int)enmRecordBackRectangleInfo.ColumnsMark3, (int)enmRecordBackRectangleInfo.ColumnsMark4, strPrintText, e);
                    }
                    //HCT
                    if (p_strValueArr[p_intNowRowInOneRecord][49].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][49];
                        m_mthPrintInBlock(ref m_intBackPosArr[4], (int)enmRecordBackRectangleInfo.ColumnsMark4, (int)enmRecordBackRectangleInfo.ColumnsMark5, strPrintText, e);
                    }
                    //PLT
                    if (p_strValueArr[p_intNowRowInOneRecord][50].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][50];
                        m_mthPrintInBlock(ref m_intBackPosArr[5], (int)enmRecordBackRectangleInfo.ColumnsMark5, (int)enmRecordBackRectangleInfo.ColumnsMark6, strPrintText, e);
                    }
                    //PH
                    if (p_strValueArr[p_intNowRowInOneRecord][51].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][51];
                        m_mthPrintInBlock(ref m_intBackPosArr[6], (int)enmRecordBackRectangleInfo.ColumnsMark6, (int)enmRecordBackRectangleInfo.ColumnsMark7, strPrintText, e);
                    }
                    //PCO2
                    if (p_strValueArr[p_intNowRowInOneRecord][52].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][52];
                        m_mthPrintInBlock(ref m_intBackPosArr[7], (int)enmRecordBackRectangleInfo.ColumnsMark7, (int)enmRecordBackRectangleInfo.ColumnsMark8, strPrintText, e);
                    }
                    //PaO2
                    if (p_strValueArr[p_intNowRowInOneRecord][53].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][53];
                        m_mthPrintInBlock(ref m_intBackPosArr[8], (int)enmRecordBackRectangleInfo.ColumnsMark8, (int)enmRecordBackRectangleInfo.ColumnsMark9, strPrintText, e);
                    }
                    //HCO3
                    if (p_strValueArr[p_intNowRowInOneRecord][54].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][54];
                        m_mthPrintInBlock(ref m_intBackPosArr[9], (int)enmRecordBackRectangleInfo.ColumnsMark9, (int)enmRecordBackRectangleInfo.ColumnsMark10, strPrintText, e);
                    }
                    //BE
                    if (p_strValueArr[p_intNowRowInOneRecord][55].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][55];
                        m_mthPrintInBlock(ref m_intBackPosArr[10], (int)enmRecordBackRectangleInfo.ColumnsMark10, (int)enmRecordBackRectangleInfo.ColumnsMark11, strPrintText, e);
                    }
                    //K+
                    if (p_strValueArr[p_intNowRowInOneRecord][56].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][56];
                        m_mthPrintInBlock(ref m_intBackPosArr[11], (int)enmRecordBackRectangleInfo.ColumnsMark11, (int)enmRecordBackRectangleInfo.ColumnsMark12, strPrintText, e);
                    }
                    //Na+
                    if (p_strValueArr[p_intNowRowInOneRecord][57].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][57];
                        m_mthPrintInBlock(ref m_intBackPosArr[12], (int)enmRecordBackRectangleInfo.ColumnsMark12, (int)enmRecordBackRectangleInfo.ColumnsMark13, strPrintText, e);
                    }
                    //Cl-
                    if (p_strValueArr[p_intNowRowInOneRecord][58].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][58];
                        m_mthPrintInBlock(ref m_intBackPosArr[13], (int)enmRecordBackRectangleInfo.ColumnsMark13, (int)enmRecordBackRectangleInfo.ColumnsMark14, strPrintText, e);
                    }
                    //CA++
                    if (p_strValueArr[p_intNowRowInOneRecord][59].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][59];
                        m_mthPrintInBlock(ref m_intBackPosArr[14], (int)enmRecordBackRectangleInfo.ColumnsMark14, (int)enmRecordBackRectangleInfo.ColumnsMark15, strPrintText, e);
                    }
                    //GLU
                    if (p_strValueArr[p_intNowRowInOneRecord][60].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][60];
                        m_mthPrintInBlock(ref m_intBackPosArr[15], (int)enmRecordBackRectangleInfo.ColumnsMark15, (int)enmRecordBackRectangleInfo.ColumnsMark16, strPrintText, e);
                    }
                    //BUN
                    if (p_strValueArr[p_intNowRowInOneRecord][61].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][61];
                        m_mthPrintInBlock(ref m_intBackPosArr[16], (int)enmRecordBackRectangleInfo.ColumnsMark16, (int)enmRecordBackRectangleInfo.ColumnsMark17, strPrintText, e);
                    }
                    //UA
                    if (p_strValueArr[p_intNowRowInOneRecord][62].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][62];
                        m_mthPrintInBlock(ref m_intBackPosArr[17], (int)enmRecordBackRectangleInfo.ColumnsMark17, (int)enmRecordBackRectangleInfo.ColumnsMark18, strPrintText, e);
                    }
                    //��
                    if (p_strValueArr[p_intNowRowInOneRecord][63].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][63];
                        m_mthPrintInBlock(ref m_intBackPosArr[18], (int)enmRecordBackRectangleInfo.ColumnsMark18, (int)enmRecordBackRectangleInfo.ColumnsMark19, strPrintText, e);
                    }
                    //CO2CP
                    if (p_strValueArr[p_intNowRowInOneRecord][64].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][64];
                        m_mthPrintInBlock(ref m_intBackPosArr[19], (int)enmRecordBackRectangleInfo.ColumnsMark19, (int)enmRecordBackRectangleInfo.ColumnsMark20, strPrintText, e);
                    }
                    //PT
                    if (p_strValueArr[p_intNowRowInOneRecord][65].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][65];
                        m_mthPrintInBlock(ref m_intBackPosArr[20], (int)enmRecordBackRectangleInfo.ColumnsMark20, (int)enmRecordBackRectangleInfo.ColumnsMark21, strPrintText, e);
                    }
                    //X�߼��
                    if (p_strValueArr[p_intNowRowInOneRecord][66].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][66];
                        m_mthPrintInBlock(ref m_intBackPosArr[21], (int)enmRecordBackRectangleInfo.ColumnsMark21, (int)enmRecordBackRectangleInfo.ColumnsMark22, strPrintText, e);
                    }
                    //ACT
                    if (p_strValueArr[p_intNowRowInOneRecord][67].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][67];
                        m_mthPrintInBlock(ref m_intBackPosArr[22], (int)enmRecordBackRectangleInfo.ColumnsMark22, (int)enmRecordBackRectangleInfo.ColumnsMark23, strPrintText, e);
                    }
                    //�򳣹�>>����
                    if (p_strValueArr[p_intNowRowInOneRecord][68].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][68];
                        m_mthPrintInBlock(ref m_intBackPosArr[23], (int)enmRecordBackRectangleInfo.ColumnsMark23, (int)enmRecordBackRectangleInfo.ColumnsMark24, strPrintText, e);
                    }
                    //����
                    if (p_strValueArr[p_intNowRowInOneRecord][69].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][69];
                        m_mthPrintInBlock(ref m_intBackPosArr[24], (int)enmRecordBackRectangleInfo.ColumnsMark24, (int)enmRecordBackRectangleInfo.ColumnsMark25, strPrintText, e);
                    }
                    //ǱѪ
                    if (p_strValueArr[p_intNowRowInOneRecord][70].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][70];
                        m_mthPrintInBlock(ref m_intBackPosArr[25], (int)enmRecordBackRectangleInfo.ColumnsMark25, (int)enmRecordBackRectangleInfo.ColumnsMark26, strPrintText, e);
                    }
                    //Ƥ��
                    if (p_strValueArr[p_intNowRowInOneRecord][71].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][71];
                        m_mthPrintInBlock(ref m_intBackPosArr[26], (int)enmRecordBackRectangleInfo.ColumnsMark27, (int)enmRecordBackRectangleInfo.ColumnsMark28, strPrintText, e);
                    }
                    //������ϴ
                    if (p_strValueArr[p_intNowRowInOneRecord][72].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][72];
                        m_mthPrintInBlock(ref m_intBackPosArr[27], (int)enmRecordBackRectangleInfo.ColumnsMark28, (int)enmRecordBackRectangleInfo.ColumnsMark29, strPrintText, e);
                    }
                    //��ϴ
                    if (p_strValueArr[p_intNowRowInOneRecord][73].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][73];
                        m_mthPrintInBlock(ref m_intBackPosArr[28], (int)enmRecordBackRectangleInfo.ColumnsMark29, (int)enmRecordBackRectangleInfo.ColumnsMark30, strPrintText, e);
                    }
                    //��ǻ����
                    if (p_strValueArr[p_intNowRowInOneRecord][74].Trim() != "")
                    {
                        strPrintText = p_strValueArr[p_intNowRowInOneRecord][74];
                        m_mthPrintInBlock(ref m_intBackPosArr[29], (int)enmRecordBackRectangleInfo.ColumnsMark30, (int)enmRecordBackRectangleInfo.ColumnsMark31, strPrintText, e);
                    }
                    int temp = 0;
                    for (int k0 = 0; k0 < m_intBackPosArr.Length; k0++)
                    {

                        if (m_intBackPosArr[k0] > temp)
                            temp = m_intBackPosArr[k0];

                    }
                    m_intBackPosY = temp;
                    p_intNowRowInOneRecord++;
                }



            }
            if (p_intNowRowInOneRecord >= m_intRecordCount)
                return true;
            else
                return false;

        }

        #endregion ��ӡһ����ֵ		
        #region ���õ�ǰҪ��ӡ���м�¼����,���ؼ�¼����
        /// <summary>
        /// ���õ�ǰҪ��ӡ���м�¼����,���ؼ�¼����
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p_intBottomY"></param>
        /// <returns></returns>
        private int m_intSetPrintOneValueRows(PrintPageEventArgs e)
        {
            if (m_objPrintDataArr == null || m_intFlag == -1)
                return 0;
            try
            {
                #region ������¼�¼���ж��Ƿ����ۼ�

                if (m_blnBeginPrintNewRecord == true && m_intRecordCount > 0)
                {
                    #region ��ǰ��¼���鸳ֵ
                    m_strValueArr = new string[m_intRecordCount][];
                    int k1 = 0;
                    for (int i2 = 0; i2 < m_objPrintDataArr.Length; i2++)
                        for (int j2 = 0; j2 < m_objPrintDataArr[i2].m_objRecordArr.Length; j2++)
                        {
                            if (m_objPrintDataArr[i2].m_objRecordArr[j2].m_dtmRECORDDATE.ToString("yyyy-MM-dd") == m_objPrintInfo.m_objPrintBaseDataArr[m_intFlag].m_dtmRECORDDATE.ToString("yyyy-MM-dd"))
                            {

                                m_strValueArr[k1] = new string[77];
                                //ǰҳ��¼
                                m_strValueArr[k1][0] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_dtmRECORDDATE.ToString("HH:mm");
                                m_strValueArr[k1][1] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strINFACT1_RIGHT;
                                m_strValueArr[k1][2] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strINFACT2_RIGHT;
                                m_strValueArr[k1][3] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strINFACT3_RIGHT;
                                m_strValueArr[k1][4] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strINFACT4_RIGHT;
                                m_strValueArr[k1][5] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strINFACT5_RIGHT;
                                m_strValueArr[k1][6] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strINBLOOD_RIGHT;
                                m_strValueArr[k1][7] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strINPERHOUR_RIGHT;
                                m_strValueArr[k1][8] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strINSUM_RIGHT;
                                m_strValueArr[k1][9] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strOUTSUM_RIGHT;
                                m_strValueArr[k1][10] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strOUTPERHOUR_RIGHT;
                                m_strValueArr[k1][11] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strOUTFACTPISSSUM_RIGHT;
                                m_strValueArr[k1][12] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strOUTFACTPISS_RIGHT;
                                m_strValueArr[k1][13] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strOUTFACTCHESTJUICE_RIGHT;
                                m_strValueArr[k1][14] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strOUTFACTCHESTJUICESUM_RIGHT;
                                m_strValueArr[k1][15] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strOUTFACTGASTRICJUICE_RIGHT;
                                m_strValueArr[k1][16] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strEXPANDVASMEDICINE_RIGHT;
                                m_strValueArr[k1][17] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strCARDIACDIURESIS_RIGHT;
                                m_strValueArr[k1][18] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strOTHERMEDICINE_RIGHT;
                                m_strValueArr[k1][19] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strCONSCIOUSNESS_RIGHT;
                                m_strValueArr[k1][20] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strPUPIL_RIGHT;
                                m_strValueArr[k1][75] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strLEFTPUPIL_RIGHT;
                                m_strValueArr[k1][76] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strRIGHTPUPIL_RIGHT;
                                m_strValueArr[k1][21] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strTEMPERATURE_RIGHT;
                                m_strValueArr[k1][22] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strTWIGTEMPERATURE_RIGHT;
                                m_strValueArr[k1][23] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strHEARTRATE_RIGHT;
                                m_strValueArr[k1][24] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strHEARTRHYTHM_RIGHT;
                                m_strValueArr[k1][25] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strBPA_RIGHT;
                                m_strValueArr[k1][26] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strAVGBP_RIGHT;
                                m_strValueArr[k1][27] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strCVP_RIGHT;
                                m_strValueArr[k1][28] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strLAP_RIGHT;
                                m_strValueArr[k1][29] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strBREATHMACHINE_RIGHT;
                                m_strValueArr[k1][30] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strINSERTDEPTH_RIGHT;
                                m_strValueArr[k1][31] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strASSISTANT_RIGHT;
                                m_strValueArr[k1][32] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strFIO2_RIGHT;
                                m_strValueArr[k1][33] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strIE_RIGHT;
                                m_strValueArr[k1][34] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strINSPIRATION_RIGHT;
                                m_strValueArr[k1][35] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strPEEP_RIGHT;
                                m_strValueArr[k1][36] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strTV_RIGHT;
                                m_strValueArr[k1][37] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strVF_RIGHT;
                                m_strValueArr[k1][38] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strBREATHTIMES_RIGHT;
                                m_strValueArr[k1][39] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strLEFTBREATHVOICE_RIGHT;
                                m_strValueArr[k1][40] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strRIGHTBREATHVOICE_RIGHT;
                                m_strValueArr[k1][41] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strPHLEGMCOLOR_RIGHT;
                                m_strValueArr[k1][42] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strPHLEGMQUANTITY_RIGHT;
                                m_strValueArr[k1][43] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strGESTICULATION_RIGHT;
                                m_strValueArr[k1][44] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strPHYSICALTHERAPY_RIGHT;
                                m_strValueArr[k1][45] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strREMARK_RIGHT;
                                //�����¼
                                m_strValueArr[k1][46] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strWBC_RIGHT;
                                m_strValueArr[k1][47] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strHB_RIGHT;
                                m_strValueArr[k1][48] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strRBC_RIGHT;
                                m_strValueArr[k1][49] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strHCT_RIGHT;
                                m_strValueArr[k1][50] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strPLT_RIGHT;
                                m_strValueArr[k1][51] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strPH_RIGHT;
                                m_strValueArr[k1][52] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strPCO2_RIGHT;
                                m_strValueArr[k1][53] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strPAO2_RIGHT;
                                m_strValueArr[k1][54] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strHCO3_RIGHT;
                                m_strValueArr[k1][55] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strBE_RIGHT;
                                m_strValueArr[k1][56] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strKPLUS_RIGHT;
                                m_strValueArr[k1][57] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strNAPLUS_RIGHT;
                                m_strValueArr[k1][58] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strCISUB_RIGHT;
                                m_strValueArr[k1][59] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strCAPLUSPLUS_RIGHT;
                                m_strValueArr[k1][60] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strGLU_RIGHT;
                                m_strValueArr[k1][61] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strBUN_RIGHT;
                                m_strValueArr[k1][62] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strUA_RIGHT;
                                m_strValueArr[k1][63] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strANHYDRIDE_RIGHT;
                                m_strValueArr[k1][64] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strCO2CP_RIGHT;
                                m_strValueArr[k1][65] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strPT_RIGHT;
                                m_strValueArr[k1][66] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strXRAYCHECK_RIGHT;
                                m_strValueArr[k1][67] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strACT_RIGHT;
                                m_strValueArr[k1][68] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strPROPORTION_RIGHT;
                                m_strValueArr[k1][69] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strALBUMEN_RIGHT;
                                m_strValueArr[k1][70] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strHIDDENBLOOD_RIGHT;
                                m_strValueArr[k1][71] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strSKIN_RIGHT;
                                m_strValueArr[k1][72] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strWASHPERINEUM_RIGHT;
                                m_strValueArr[k1][73] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strBRUSHBATH_RIGHT;
                                m_strValueArr[k1][74] = m_objPrintDataArr[i2].m_objRecordArr[j2].m_strMOUTHTEND_RIGHT;

                                k1++;

                            }
                        }


                    return m_intRecordCount;

                    #endregion

                }
                else
                    return 0;
                #endregion
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message);
                return 1;
            }
        }
        #endregion
        /// <summary>
        /// ��ӡ��Ϣ.
        /// </summary>
        [Serializable]
        private class clsPrintInfo_CardiovascularTend
        {
            public string m_strInPatientID;
            public string m_strPatientName;
            public string m_strAge;
            public DateTime m_dtmInPatientDate;
            public DateTime m_dtmOpenDate;
            public clsTransDataInfo[] m_objTransDataArr;
            public DateTime[] m_dtmFirstPrintDateArr;//Length��m_dtmFirstPrintDateArr.Length��ͬ.
            public bool[] m_blnIsFirstPrintArr;//Length��m_dtmFirstPrintDateArr.Length��ͬ.
            public clsCardiovascularTend_GXDataInfo[] m_objPrintDataArr;
            public clsCardiovascularBaseInfo_GX[] m_objPrintBaseDataArr;


        }


    }
}
