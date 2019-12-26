using System;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// Summary description for clsCheckReportWithAnnotation.
    /// </summary>
    public class clsCheckReportWithAnnotation : infPrintRecord
    {
        private long m_lngWidthPage;//��ӡҳ�Ŀ��
        private long m_lngY;//��ǰY��������

        private float m_fltLeftIndentProp;//����������
        private float m_fltRightIndentProp;//����������

        private Font m_fntTitle;
        private Font m_fntSmallBold;
        private Font m_fntSmallNotBold;
        private Font m_fntSmall2NotBold;
        private Font m_fntHeadNotBold;

        public float m_fltItemSpace;//������Ŀ�ļ��



        public System.Data.DataTable m_dtbSample = null;//������Ϣ������
        public System.Data.DataTable m_dtbResult;//������
        public System.Data.DataTable m_dtbDeviceID_Name;//�����豸ID����Ŀ���ֵ�ӳ��
        public bool m_bolFinishPrint = false;

        public string strComment = "";//����
        public string strComment_XML = "";//����XML

        public string m_strTitle;//���浥����

        private int m_intPageCount;//��ӡ����ҳ��
        private int m_intCurrentPage;//��ӡ�ĵ�ǰҳ
        private int m_intEachSideCount;//ÿ�д�ӡ��Ŀ�ĸ���
        private int m_intEachPageSideCount;//ÿ�е�ҳ��ӡ����
        private int m_intEachSideItemHeight;//ÿ�д�ӡ��Ŀ���ܸ߶�
        private int m_intEachPageSideItemHeight;//ÿ��ҳ��ӡ��Ŀ���ܸ߶�

        public bool m_blnDocked = true;//�жϱ��浥�ײ���Ϣλ���Ƿ�̶�
        public long m_lngEndPosition;//���浥�ײ���ӡ��λ��
        public bool m_blnHasTwoPart = false;//�жϱ��浥�����ӡ�Ƿ�������
        public bool m_blnPrintSummary = true;//�ж��Ƿ��ӡʵ������ʾ
        private bool m_blnHasPrintGroup = false;//�ж��Ƿ��ӡ����Ŀ

        public float m_fltItemAndResultSpace;//��Ŀ���ƺͽ��֮��ľ���
        public float m_fltResultAndRefSpace;//����Ͳο���Χ֮��ľ���

        /// �߿򻭱�
        /// </summary>
        private Pen m_GridPen;

        #region ��ӡ������ʼ����
        /// <summary>
        /// ��ӡ������ʼ����
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        private void m_mthPrintStart(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {
            //			if(m_dtbSample != null)
            //			{
            //				TransactdtbSampleToSampleVO();
            //			}

            m_lngWidthPage = p_objPrintArg.PageBounds.Width;

            m_strTitle = m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim();//"��ɽ�еڶ�����ҽԺ���鱨�浥";
            SizeF szTitle = p_objPrintArg.Graphics.MeasureString(m_strTitle, m_fntTitle);
            float fltCurrentX = m_lngWidthPage / 2 - (long)szTitle.Width / 2;//�����ı����Ͻǵ�X������
            m_lngY = 10;//�����ı����Ͻ�Y������
            p_objPrintArg.Graphics.DrawString(m_strTitle, m_fntTitle, Brushes.Black, fltCurrentX, m_lngY);

            m_lngY = 20 + (int)szTitle.Height;
            SizeF szTmp = p_objPrintArg.Graphics.MeasureString("����:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * m_fltLeftIndentProp;
            p_objPrintArg.Graphics.DrawString("����:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["patient_name_vchr"].ToString().Trim(), m_fntHeadNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

            szTmp = p_objPrintArg.Graphics.MeasureString("סԺ��:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 0.85f / 4);
            p_objPrintArg.Graphics.DrawString("סԺ��:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["PATIENT_INHOSPITALNO_CHR"].ToString().Trim(), m_fntHeadNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

            szTmp = p_objPrintArg.Graphics.MeasureString("��������:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 0.95f / 2);
            p_objPrintArg.Graphics.DrawString("��������:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["SAMPLE_TYPE_DESC_VCHR"].ToString().Trim(), m_fntHeadNotBold, Brushes.Black,
                fltCurrentX, m_lngY);


            szTmp = p_objPrintArg.Graphics.MeasureString("������:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 2.9f / 4);
            p_objPrintArg.Graphics.DrawString("������:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["sample_id_chr"].ToString().Trim(), m_fntHeadNotBold, Brushes.Black,
                fltCurrentX, m_lngY);


            m_lngY = 25 + (int)szTitle.Height + (int)m_fntSmallBold.Height;
            szTmp = p_objPrintArg.Graphics.MeasureString("�Ա�:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * m_fltLeftIndentProp;
            p_objPrintArg.Graphics.DrawString("�Ա�:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["sex_chr"].ToString().Trim(), m_fntHeadNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

            szTmp = p_objPrintArg.Graphics.MeasureString("��  ��:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 0.85f / 4);
            p_objPrintArg.Graphics.DrawString("��  ��:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["deptname_vchr"].ToString().Trim(), m_fntHeadNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

            szTmp = p_objPrintArg.Graphics.MeasureString("�ͼ�ҽ��:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 0.95f / 2);
            p_objPrintArg.Graphics.DrawString("�ͼ�ҽ��:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["applyer"].ToString().Trim(), m_fntHeadNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

            szTmp = p_objPrintArg.Graphics.MeasureString("������:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 2.9f / 4);
            p_objPrintArg.Graphics.DrawString("������:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["check_no_chr"].ToString().Trim(), m_fntHeadNotBold, Brushes.Black,
                fltCurrentX, m_lngY);


            m_lngY = 30 + (int)szTitle.Height + (int)m_fntSmallBold.Height * 2;
            szTmp = p_objPrintArg.Graphics.MeasureString("����:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * m_fltLeftIndentProp;
            p_objPrintArg.Graphics.DrawString("����:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["age_chr"].ToString().Trim(), m_fntHeadNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

            szTmp = p_objPrintArg.Graphics.MeasureString("��  ��:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 0.85f / 4);
            p_objPrintArg.Graphics.DrawString("��  ��:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["bedno_chr"].ToString().Trim(), m_fntHeadNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

            szTmp = p_objPrintArg.Graphics.MeasureString("�ٴ����:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 0.95f / 2);
            p_objPrintArg.Graphics.DrawString("�ٴ����:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["diagnose_vchr"].ToString().Trim(), m_fntHeadNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

            szTmp = p_objPrintArg.Graphics.MeasureString("�ͼ�����:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 2.9f / 4);
            p_objPrintArg.Graphics.DrawString("�ͼ�����:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            string strAcceptDate = "";
            if (Microsoft.VisualBasic.Information.IsDate(m_dtbSample.Rows[0]["accept_dat"].ToString().Trim()))
            {
                strAcceptDate = DateTime.Parse(m_dtbSample.Rows[0]["accept_dat"].ToString().Trim()).ToString("yyyy-MM-dd");
            }
            else
            {
                strAcceptDate = "";
            }
            p_objPrintArg.Graphics.DrawString(strAcceptDate,
                m_fntHeadNotBold, Brushes.Black, fltCurrentX, m_lngY);

        }

        private void m_mthPrintLine(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {
            m_lngY += 2 + m_fntSmallBold.Height;
            long intYStart = m_lngY;

            //������
            p_objPrintArg.Graphics.DrawLine(m_GridPen, m_lngWidthPage * 0.08f, intYStart, m_lngWidthPage * 0.92f, intYStart);
            //			p_objPrintArg.Graphics.DrawString(intYStart.ToString(),m_fntSmallNotBold,Brushes.Black,m_lngWidthPage*0.92f,intYStart);
            //			long intYMiddle=intYStart+8+m_fntSmallBold.Height;
            //			p_objPrintArg.Graphics.DrawLine(m_GridPen,m_lngWidthPage*0.08f,intYMiddle,m_lngWidthPage*0.92f,intYMiddle);

        }
        #endregion


        #region ��ӡ����
        /// <summary>
        /// ��ӡ���浥�м䲿��
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        private void m_mthPrintMiddle(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {
            #region old
            //			m_lngY+=6;
            //			float[] fltColumnXArr=new float[4];
            //			fltColumnXArr[0]=m_lngWidthPage*m_fltLeftIndentProp;
            //			p_objPrintArg.Graphics.DrawString("��Ŀ����",m_fntSmallBold,Brushes.Black,fltColumnXArr[0],m_lngY);
            //
            //			fltColumnXArr[1]=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/4+0.07f);
            //			p_objPrintArg.Graphics.DrawString(" ���",m_fntSmallBold,Brushes.Black,fltColumnXArr[1],m_lngY);
            //
            //			fltColumnXArr[2]=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2+0.04f);
            //			p_objPrintArg.Graphics.DrawString("�ο���Χ",m_fntSmallBold,Brushes.Black,fltColumnXArr[2],m_lngY);
            //
            //			fltColumnXArr[3]=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*3/4+0.07f);
            //			p_objPrintArg.Graphics.DrawString("��λ",m_fntSmallBold,Brushes.Black,fltColumnXArr[3],m_lngY);
            //
            //			//��λY������
            //			float fltCurrentY = m_lngY;
            //			for(int i=0;i<m_dtbResult.Rows.Count;i++)
            //			{
            //				string strResult=m_dtbResult.Rows[i]["result_vchr"].ToString().Trim();
            //				string strAbnormal = m_dtbResult.Rows[i]["ABNORMAL_FLAG_CHR"].ToString().Trim();
            //				string strRefRange=m_dtbResult.Rows[i]["refrange_vchr"].ToString();
            //				string strMinVal = m_dtbResult.Rows[i]["MIN_VAL_DEC"].ToString().Trim();
            //				string strMaxVal = m_dtbResult.Rows[i]["MAX_VAL_DEC"].ToString().Trim();
            //				string strCheckItemName = m_dtbResult.Rows[i]["CHECK_ITEM_NAME_VCHR"].ToString().Trim();
            //				string strDeviceCheckItemName = m_dtbResult.Rows[i]["DEVICE_CHECK_ITEM_NAME_VCHR"].ToString().Trim();
            //				string strUnit = m_dtbResult.Rows[i]["UNIT_VCHR"].ToString().Trim();
            //				double doubleResult = -1; //�����Ϊ������ʱ����ֵĬ��Ϊ��1
            //				double doubleMinVal = -1;
            //				double doubleMaxVal = -1;
            //
            //				fltCurrentY += 8+m_fntSmallBold.Height;
            //				p_objPrintArg.Graphics.DrawString(strCheckItemName+"("+strDeviceCheckItemName+")",m_fntSmallNotBold,Brushes.Black,fltColumnXArr[0],fltCurrentY);
            //				//1.�����쳣��־�ж�,�˴���Ϊ�쳣��־ֻ��"H"(��)��"L"(��)�������
            //				if(strAbnormal !=null)
            //				{
            //					if(strAbnormal == "H")
            //					{
            //						p_objPrintArg.Graphics.DrawString("��"+strResult,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[1],fltCurrentY);
            //					}
            //					else if(strAbnormal == "L")
            //					{
            //						p_objPrintArg.Graphics.DrawString("��"+strResult,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[1],fltCurrentY);
            //					}
            //					else
            //					{
            //						p_objPrintArg.Graphics.DrawString("  "+strResult,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[1],fltCurrentY);
            //					}
            //				}
            //				else
            //				{
            //					//2.�������ֵ��Сֵ�ж�
            //					try
            //					{
            //						doubleResult = double.Parse(strResult);
            //						doubleMinVal = double.Parse(strMinVal);
            //						doubleMaxVal = double.Parse(strMaxVal);
            //					}
            //					catch(Exception objEx)
            //					{
            //						//throw objEx;
            //					}
            //					finally
            //					{
            //						if(doubleResult != -1)
            //						{
            //							if(doubleResult < doubleMinVal)
            //							{
            //								p_objPrintArg.Graphics.DrawString("��"+strResult,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[1],fltCurrentY);
            //							}
            //							else if(doubleResult > doubleMaxVal)
            //							{
            //								p_objPrintArg.Graphics.DrawString("��"+strResult,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[1],fltCurrentY);
            //							}
            //							else
            //							{
            //								p_objPrintArg.Graphics.DrawString("  "+strResult,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[1],fltCurrentY);
            //							}
            //						}
            //						else
            //						{
            //							p_objPrintArg.Graphics.DrawString("  "+strResult,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[1],fltCurrentY);
            //						}
            //					}
            //				}
            //				p_objPrintArg.Graphics.DrawString(strRefRange,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[2],fltCurrentY);
            //				p_objPrintArg.Graphics.DrawString(strUnit,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[3],fltCurrentY);
            //			}
            //			m_lngY = (long)fltCurrentY;
            #endregion

            //			if(m_dtbDeviceID_Name == null) 
            //				return;

            //			m_lngY+=10+m_fntSmallBold.Height;

            try
            {
                ArrayList arlTempLeft = new ArrayList();//��Ŵ�ӡ��������Group
                ArrayList arlTempRight = new ArrayList();//��Ŵ�ӡ��������Group

                //�Ա걾������
                DataView dtvResult = new DataView(m_dtbResult);
                dtvResult.Sort = "REPORT_PRINT_SEQ_INT ASC,SAMPLE_PRINT_SEQ_INT ASC";

                #region �����豸ӳ���ӽ�������еõ����� Group ����ʱ���� arlTempLeft ��
                //				for(int i=0;i<m_dtbDeviceID_Name.Rows.Count;i++)
                //				{
                //					DataView dtvGroupData = new DataView(this.m_dtbResult);
                //
                //					dtvGroupData.RowFilter = "DEVICEID_CHR = '" + m_dtbDeviceID_Name.Rows[i][0].ToString().Trim() + "'";
                //					if(dtvGroupData.Count != 0)
                //					{
                //						clsGroup objGroup = new clsGroup();
                //						objGroup.m_dtvGroupData = dtvGroupData;
                //						objGroup.m_strGroupName = m_dtbDeviceID_Name.Rows[i][1].ToString().Trim();
                //						arlTempLeft.Add(objGroup);
                //					}
                //				}
                #endregion

                #region ����GROUPID_CHR�ӽ�������еõ�����Group����ʱ���� arlTempLeft ��
                //��ȡ���еı��������
                //				ArrayList arlGroupID = new ArrayList();
                //				for(int i=0;i<dtvResult.Count;i++)
                //				{
                //					bool blnHasSameGroup = false;
                //					for(int j=0;j<arlGroupID.Count;j++)
                //					{
                //						if(dtvResult[i]["GROUPID_CHR"].ToString().Trim() == arlGroupID[j].ToString().Trim())
                //						{
                //							blnHasSameGroup = true;
                //							break;
                //						}
                //					}
                //					if(!blnHasSameGroup)
                //					{
                //						string strGroupID = dtvResult[i]["GROUPID_CHR"].ToString().Trim();
                //						arlGroupID.Add(strGroupID);
                //					}
                //				}
                //				//��Ÿ�������
                //				for(int i=0;i<arlGroupID.Count;i++)
                //				{
                //					DataView dtvGroupData = new DataView(this.m_dtbResult);
                //
                //					dtvGroupData.RowFilter = "GROUPID_CHR = '"+arlGroupID[i].ToString().Trim()+"'";
                //					if(dtvGroupData.Count > 0)
                //					{
                //						clsGroup objGroup = new clsGroup();
                //						objGroup.m_dtvGroupData = dtvGroupData;
                //						objGroup.m_dtvGroupData.Sort = "SAMPLE_PRINT_SEQ_INT ASC";
                //						objGroup.m_strGroupName = arlGroupID[i].ToString().Trim();
                //						arlTempLeft.Add(objGroup);
                //					}
                //				}
                #endregion

                //				this.m_mthAdmeasureGroup(ref arlTempLeft,ref arlTempRight);

                //				this.m_mthDistributeGroup(ref arlTempLeft,ref arlTempRight);
                ArrayList arlPage;
                //				m_mthPrintDistribute(dtvResult,out arlPage);
                m_mthAutoDistribute(dtvResult, out arlPage);

                float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp - 10f;//������ӡ�� X ��ʼ��
                float fltRightX = m_lngWidthPage * 0.5f;//������ӡ�� X ��ʼ��
                float fltLeftY = m_lngY;//������ӡ�� Y ��ʼ��
                float fltRightY = m_lngY;//������ӡ�� Y ��ʼ��
                float fltWidth = m_lngWidthPage * (1 - m_fltLeftIndentProp * 2) / 2 + 10f;// �����Ŀ�� 
                string strGroupName;

                #region ��ӡGroup
                bool blnIsTop = true;
                for (int j = 0; j < arlPage.Count; j++)
                {
                    if (j == m_intCurrentPage - 1)
                    {
                        ArrayList[] arlResult = (ArrayList[])arlPage[j];
                        if (arlResult[0] != null)
                        {
                            if (m_intCurrentPage == arlPage.Count - 1)
                            {
                                m_blnHasPrintGroup = true;
                            }
                            for (int i = 0; i < arlResult[0].Count; i++)
                            {
                                if (i > 0)
                                {
                                    blnIsTop = false;
                                }
                                else
                                {
                                    blnIsTop = true;
                                }
                                clsGroup objGroup = (clsGroup)arlResult[0][i];//arlTempLeft[i];
                                float fltEndY;
                                strGroupName = objGroup.m_dtvGroupData[0].Row["PRINT_TITLE_VCHR"].ToString().Trim();//objGroup.m_strGroupName + "������Ŀ";
                                this.m_mthPrintGroup(p_objPrintArg, objGroup.m_dtvGroupData, strGroupName, fltLeftX, fltLeftY, fltWidth, out fltEndY, blnIsTop);
                                fltLeftY = fltEndY;
                                //					p_objPrintArg.Graphics.DrawString((fltEndY-4 - m_fntSmallBold.Height+m_fltItemSpace).ToString().Trim(),m_fntSmallBold,Brushes.Black,m_lngWidthPage*0.8f,fltEndY-4 - m_fntSmallBold.Height+m_fltItemSpace);
                            }
                        }
                        if (arlResult[1] != null)
                        {
                            for (int i = 0; i < arlResult[1].Count; i++)
                            {
                                if (i > 0)
                                {
                                    blnIsTop = false;
                                }
                                else
                                {
                                    blnIsTop = true;
                                }
                                clsGroup objGroup = (clsGroup)arlResult[1][i];//arlTempRight[i];
                                float fltEndY;
                                strGroupName = objGroup.m_dtvGroupData[0].Row["PRINT_TITLE_VCHR"].ToString().Trim();//objGroup.m_strGroupName + "������Ŀ";
                                this.m_mthPrintGroup(p_objPrintArg, objGroup.m_dtvGroupData, strGroupName, fltRightX, fltRightY, fltWidth, out fltEndY, blnIsTop);
                                fltRightY = fltEndY;
                            }
                        }
                    }
                }
                #endregion

                float fltBodyEndY = 0;
                if (fltLeftY >= fltRightY)
                {
                    fltBodyEndY = fltLeftY;
                }
                else
                {
                    fltBodyEndY = fltRightY;
                }
                //				if(arlTempRight.Count != 0)//���������ӡһ����
                //					p_objPrintArg.Graphics.DrawLine(m_GridPen,m_lngWidthPage*0.5f,m_lngY,m_lngWidthPage*0.5f,fltBodyEndY);

                this.m_lngY = (long)fltBodyEndY;//- m_fntSmallNotBold.Height;
            }
            catch { }
        }

        #region unuse
        /// <summary>
        /// �Խ���ƽ������㷨��������������Ҫ��ӡ�� Group
        /// </summary>
        /// <param name="p_arlLeft"></param>
        /// <param name="p_arlRight"></param>
        private void m_mthAdmeasureGroup(ref ArrayList p_arlLeft, ref ArrayList p_arlRight)
        {
            try
            {
                #region �ϲ��� p_arlLeft,�ÿ� p_arlRight

                p_arlLeft.AddRange(p_arlRight);
                p_arlRight.Clear();

                #endregion

                if (p_arlLeft.Count == 0)
                    return;

                #region ���� p_arlRight,�ÿ� p_arlLeft

                for (int i1 = 0; i1 < p_arlLeft.Count; i1++)
                {
                    for (int j2 = 0; j2 <= p_arlRight.Count; j2++)
                    {
                        if (j2 == p_arlRight.Count)
                        {
                            p_arlRight.Add(p_arlLeft[i1]);
                            break;
                        }
                        if (((clsGroup)p_arlLeft[i1]).m_dtvGroupData.Count > ((clsGroup)p_arlRight[j2]).m_dtvGroupData.Count)
                        {
                            p_arlRight.Insert(j2, p_arlLeft[i1]);
                            break;
                        }
                    }
                }
                p_arlLeft.Clear();
                #endregion

                #region ����ƽ������㷨

                int intLeft = 0;
                int intRight = 0;

                ArrayList arlRightTemp = new ArrayList();
                for (int i = 0; i < p_arlRight.Count; i++)
                {
                    if (intLeft <= intRight)
                    {
                        p_arlLeft.Add(p_arlRight[i]);
                        intLeft += ((clsGroup)p_arlRight[i]).m_dtvGroupData.Count;
                    }
                    else
                    {
                        arlRightTemp.Add(p_arlRight[i]);
                        intRight += ((clsGroup)p_arlRight[i]).m_dtvGroupData.Count;
                    }
                }

                p_arlRight = arlRightTemp;

                #endregion
            }
            catch { }
        }

        /// <summary>
        /// ��ʱֻ���ǵ�һ������������
        /// </summary>
        /// <param name="p_arlLeft"></param>
        /// <param name="p_arlRight"></param>
        private void m_mthDistributeGroup(ref ArrayList p_arlLeft, ref ArrayList p_arlRight)
        {
            try
            {
                #region �ϲ��� p_arlLeft,�ÿ� p_arlRight

                p_arlLeft.AddRange(p_arlRight);
                p_arlRight.Clear();

                #endregion

                if (p_arlLeft.Count == 0)
                    return;

                ArrayList arlLeftTemp = new ArrayList();

                for (int i = 0; i < p_arlLeft.Count; i++)
                {
                    clsGroup objRightGroup = new clsGroup();
                    clsGroup objLeftGroup = new clsGroup();

                    DataTable dtbLeft = ((clsGroup)p_arlLeft[i]).m_dtvGroupData.Table.Clone();
                    DataTable dtbRight = ((clsGroup)p_arlLeft[i]).m_dtvGroupData.Table.Clone();

                    if (((clsGroup)p_arlLeft[0]).m_dtvGroupData.Count > (m_intCurrentPage - 1) * m_intEachSideCount * 2 + m_intEachSideCount)
                    {
                        m_blnHasTwoPart = true;
                        //��ߵ���
                        for (int j = (m_intCurrentPage - 1) * m_intEachSideCount * 2; j < (m_intCurrentPage - 1) * m_intEachSideCount * 2 + m_intEachSideCount; j++)
                        {
                            DataRow dr = dtbLeft.NewRow();
                            dr.ItemArray = ((clsGroup)p_arlLeft[i]).m_dtvGroupData[j].Row.ItemArray;
                            dtbLeft.Rows.Add(dr);
                        }

                        //�ұߵ���
                        int intEnd;
                        if ((m_intCurrentPage - 1) * m_intEachSideCount * 2 + 2 * m_intEachSideCount < ((clsGroup)p_arlLeft[i]).m_dtvGroupData.Count)
                        {
                            intEnd = (m_intCurrentPage - 1) * m_intEachSideCount * 2 + 2 * m_intEachSideCount;
                        }
                        else
                        {
                            intEnd = ((clsGroup)p_arlLeft[i]).m_dtvGroupData.Count;
                        }
                        for (int j = (m_intCurrentPage - 1) * m_intEachSideCount * 2 + m_intEachSideCount; j < intEnd; j++)
                        {
                            DataRow dr = dtbRight.NewRow();
                            dr.ItemArray = ((clsGroup)p_arlLeft[i]).m_dtvGroupData[j].Row.ItemArray;
                            dtbRight.Rows.Add(dr);
                        }

                        DataView dtvLeftGroup = new DataView(dtbLeft);
                        objLeftGroup.m_dtvGroupData = dtvLeftGroup;
                        objLeftGroup.m_strGroupName = ((clsGroup)p_arlLeft[i]).m_strGroupName;
                        arlLeftTemp.Add(objLeftGroup);

                        DataView dtvRightGroup = new DataView(dtbRight);
                        objRightGroup.m_dtvGroupData = dtvRightGroup;
                        objRightGroup.m_strGroupName = ((clsGroup)p_arlLeft[i]).m_strGroupName;
                        p_arlRight.Add(objRightGroup);

                    }
                    else
                    {
                        for (int j = (m_intCurrentPage - 1) * m_intEachSideCount * 2; j < ((clsGroup)p_arlLeft[i]).m_dtvGroupData.Count; j++)
                        {
                            DataRow dr = dtbLeft.NewRow();
                            dr.ItemArray = ((clsGroup)p_arlLeft[i]).m_dtvGroupData[j].Row.ItemArray;
                            dtbLeft.Rows.Add(dr);
                        }

                        DataView dtvLeftGroup = new DataView(dtbLeft);
                        objLeftGroup.m_dtvGroupData = dtvLeftGroup;
                        objLeftGroup.m_strGroupName = ((clsGroup)p_arlLeft[i]).m_strGroupName;
                        arlLeftTemp.Add(objLeftGroup);
                        if (p_arlLeft.Count > 1)
                        {
                            m_blnHasTwoPart = true;
                            p_arlRight.Add(p_arlLeft[i + 1]);
                            i++;
                        }
                    }
                }
                p_arlLeft = arlLeftTemp;
                //				for(int i=1;i<p_arlLeft.Count;i++)
                //				{
                //					p_arlRight.Add(p_arlLeft[i]);
                //				}
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }

        public void m_mthPrintDistribute(DataView p_dtvResult, out ArrayList p_arlPage)
        {
            p_arlPage = new ArrayList(m_intPageCount);
            p_dtvResult.Sort = "REPORT_PRINT_SEQ_INT ASC,groupid_chr ASC,SAMPLE_PRINT_SEQ_INT ASC";
            int intLeftEnd = 0;
            int intRightEnd = 0;

            for (int i = 0; i < m_intPageCount; i++)
            {
                ArrayList p_arlLefGroup = new ArrayList();
                ArrayList p_arlRightGroup = new ArrayList();

                DataTable dtbLeft = p_dtvResult.Table.Clone();
                DataTable dtbRight = p_dtvResult.Table.Clone();

                intLeftEnd = intRightEnd + m_intEachSideCount;
                //����״̬������ӡ����Ŀ����
                int intLeftIdearLength = intRightEnd + m_intEachSideCount;

                if (intLeftEnd < p_dtvResult.Count)
                {
                    intLeftEnd = intRightEnd + m_intEachSideCount;
                }
                else
                {
                    intLeftEnd = p_dtvResult.Count;
                }

                ArrayList arlGroupID = new ArrayList();
                int intLeftDropRowCount = 0;
                for (int j = intRightEnd; j < intLeftEnd; j++)
                {
                    bool blnLeftEndAndChange = false;
                    if (j > intRightEnd)
                    {
                        if (p_dtvResult[j]["groupid_chr"].ToString().Trim() != p_dtvResult[j - 1]["groupid_chr"].ToString().Trim())
                        {
                            //							arlGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                            if (j < intLeftEnd - 2)
                            {
                                arlGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                                //								intLeftEnd = intLeftEnd-2;
                                //								if(intLeftIdearLength-2<p_dtvResult.Count)
                                //								{
                                //									intLeftEnd = intLeftEnd-1;
                                //								}
                                //								intLeftIdearLength = intLeftIdearLength-1;
                            }
                            else
                            {
                                blnLeftEndAndChange = true;
                                intLeftDropRowCount = intLeftEnd - j;
                            }
                        }
                    }
                    else
                    {
                        arlGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                    }
                    if (!blnLeftEndAndChange)
                    {
                        DataRow dr = dtbLeft.NewRow();
                        dr.ItemArray = p_dtvResult[j].Row.ItemArray;
                        dtbLeft.Rows.Add(dr);
                    }
                    //					}
                }

                intLeftEnd = intLeftEnd - intLeftDropRowCount;

                for (int j = 0; j < arlGroupID.Count; j++)
                {
                    clsGroup objLeftGroup = new clsGroup();
                    DataView dtvLeft = new DataView(dtbLeft);
                    dtvLeft.RowFilter = "groupid_chr = '" + arlGroupID[j].ToString().Trim() + "'";
                    objLeftGroup.m_dtvGroupData = dtvLeft;
                    objLeftGroup.m_strGroupName = dtbLeft.Rows[0]["print_title_vchr"].ToString().Trim();
                    p_arlLefGroup.Add(objLeftGroup);
                }

                if (intLeftEnd < p_dtvResult.Count)
                {
                    m_blnHasTwoPart = true;
                    //����״̬���Ҳ��ӡ����Ŀ����
                    int intRightIdearLength = intLeftEnd + m_intEachSideCount;
                    if (intLeftEnd + m_intEachSideCount < p_dtvResult.Count)
                    {
                        intRightEnd = intLeftEnd + m_intEachSideCount;
                    }
                    else
                    {
                        intRightEnd = p_dtvResult.Count;
                    }

                    ArrayList arlRightGroupID = new ArrayList();
                    int intRightDropRowCount = 0;
                    for (int j = intLeftEnd; j < intRightEnd; j++)
                    {
                        bool blnRightEndAndChange = false;
                        if (j > intLeftEnd)
                        {
                            if (p_dtvResult[j]["groupid_chr"].ToString().Trim() != p_dtvResult[j - 1]["groupid_chr"].ToString().Trim())
                            {
                                if (j < intRightEnd - 2)
                                {
                                    arlRightGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                                    //									intRightIdearLength = intRightIdearLength-2;
                                    //									if(intRightIdearLength<intRightEnd)
                                    //									{
                                    //										intRightEnd = intRightEnd-2;
                                    //									}
                                }
                                else
                                {
                                    blnRightEndAndChange = true;
                                    intRightDropRowCount += intRightEnd - j;
                                }
                            }
                        }
                        else
                        {
                            arlRightGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                        }

                        if (!blnRightEndAndChange)
                        {
                            DataRow dr = dtbRight.NewRow();
                            dr.ItemArray = p_dtvResult[j].Row.ItemArray;
                            dtbRight.Rows.Add(dr);
                        }
                    }

                    intRightEnd = intRightEnd - intRightDropRowCount;

                    for (int j = 0; j < arlRightGroupID.Count; j++)
                    {
                        clsGroup objRightGroup = new clsGroup();
                        DataView dtvRightGroup = new DataView(dtbRight);
                        dtvRightGroup.RowFilter = "groupid_chr = '" + arlRightGroupID[j].ToString().Trim() + "'";
                        objRightGroup.m_dtvGroupData = dtvRightGroup;
                        objRightGroup.m_strGroupName = dtbRight.Rows[0]["print_title_vchr"].ToString().Trim();
                        p_arlRightGroup.Add(objRightGroup);
                    }
                    if (intRightEnd < p_dtvResult.Count && m_intPageCount <= i + 1)
                    {
                        m_intPageCount++;
                    }
                }
                ArrayList[] arlTemp = new ArrayList[2];
                arlTemp[0] = p_arlLefGroup;
                arlTemp[1] = p_arlRightGroup;
                p_arlPage.Add(arlTemp);
            }
        }
        #endregion


        //1.�жϴ�ӡ�ļ�¼�Ƿ��ܰ���׼��ʽ��һҳ����
        //1.1 Y ��ӡ
        //1.1.1 ��ӡ��һ��group
        //1.1.1.1 �ж����µļ�¼�ܷ�����һ�ߴ���,���ҵ�ǰ�Ѿ���ӡ�ļ�¼����������ڻ���ڵ��д�ӡ������1/2
        //1.1.1.1.1 Y ת����һ�ߴ�ӡ
        //1.1.1.1.2 N �жϸ��л����Դ�ӡ�ļ�¼�����Ƿ�>3
        //1.1.1.1.2.1 Y ������ӡ
        //1.1.1.1.2.2.N ת����һ�ߴ�ӡ
        //1.2 N GoTo 2
        //2.�жϴ�ӡ�ļ�¼�Ƿ��ܰ����޸�ʽ��һҳ����
        //2.1 Y ��ӡ
        //2.1.1 ��ӡ��һ��group�жϣ����µļ�¼�ܷ�����һ�ߴ���,���ҵ�ǰ�Ѿ���ӡ�ļ�¼����������ڻ���ڵ��д�ӡ������1/2
        //2.1.1.1 Y ת����һ�ߴ�ӡ
        //2.1.1.2 N ������ӡ
        //2.2 N ��ӡ ��ҳ

        public void m_mthAutoDistribute(DataView p_dtvResult, out ArrayList p_arlPage)
        {
            p_arlPage = new ArrayList(m_intPageCount);
            p_dtvResult.Sort = "REPORT_PRINT_SEQ_INT ASC,groupid_chr ASC,SAMPLE_PRINT_SEQ_INT ASC";
            int intLeftEnd = 0;
            int intRightEnd = 0;

            for (int i = 0; i < m_intPageCount; i++)
            {
                //1
                if (p_dtvResult.Count <= intRightEnd + 2 * m_intEachSideCount)
                {
                    //1.1
                    #region ��ӡ���
                    ArrayList arlLeftGroupID = new ArrayList();
                    ArrayList arlLeftGroup = new ArrayList();

                    DataTable dtbLeft = p_dtvResult.Table.Clone();

                    if (p_dtvResult.Count <= (intRightEnd + m_intEachSideCount))
                    {
                        intLeftEnd = p_dtvResult.Count;
                    }
                    else
                    {
                        intLeftEnd = intRightEnd + m_intEachSideCount;
                    }
                    for (int j = intRightEnd; j < intLeftEnd; j++)
                    {
                        if (j > intRightEnd)
                        {
                            if (p_dtvResult[j]["groupid_chr"].ToString().Trim() != p_dtvResult[j - 1]["groupid_chr"].ToString().Trim())
                            {
                                if (j >= (intRightEnd + m_intEachSideCount / 2 - 1) && (p_dtvResult.Count - j + 1) < m_intEachSideCount)
                                {
                                    intLeftEnd = j;
                                    break;
                                }
                                else
                                {
                                    if (intLeftEnd - j > 3)
                                    {
                                        arlLeftGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                                    }
                                    else
                                    {
                                        intLeftEnd = j;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            arlLeftGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                        }

                        DataRow dr = dtbLeft.NewRow();
                        dr.ItemArray = p_dtvResult[j].Row.ItemArray;
                        dtbLeft.Rows.Add(dr);
                    }

                    //�����ӡ��Group
                    for (int j = 0; j < arlLeftGroupID.Count; j++)
                    {
                        clsGroup objLeftGroup = new clsGroup();
                        DataView dtvLeft = new DataView(dtbLeft);
                        dtvLeft.RowFilter = "groupid_chr = '" + arlLeftGroupID[j].ToString().Trim() + "'";
                        objLeftGroup.m_dtvGroupData = dtvLeft;
                        objLeftGroup.m_strGroupName = dtbLeft.Rows[0]["print_title_vchr"].ToString().Trim();
                        arlLeftGroup.Add(objLeftGroup);
                    }
                    #endregion
                    #region ��ӡ�ұ�
                    if (intLeftEnd < p_dtvResult.Count)
                    {
                        //�ֱߴ�ӡ
                        m_blnHasTwoPart = true;

                        ArrayList arlRightGroupID = new ArrayList();
                        ArrayList arlRightGroup = new ArrayList();

                        DataTable dtbRight = p_dtvResult.Table.Clone();

                        intRightEnd = intLeftEnd + m_intEachSideCount;
                        if (intRightEnd > p_dtvResult.Count)
                        {
                            intRightEnd = p_dtvResult.Count;
                        }
                        for (int j = intLeftEnd; j < intRightEnd; j++)
                        {
                            if (j > intLeftEnd)
                            {
                                if (p_dtvResult[j]["groupid_chr"].ToString().Trim() != p_dtvResult[j - 1]["groupid_chr"].ToString().Trim())
                                {
                                    arlRightGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                                }
                            }
                            else
                            {
                                arlRightGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                            }

                            DataRow dr = dtbRight.NewRow();
                            dr.ItemArray = p_dtvResult[j].Row.ItemArray;
                            dtbRight.Rows.Add(dr);
                        }

                        //�����ӡ��RightGroup
                        for (int j = 0; j < arlRightGroupID.Count; j++)
                        {
                            clsGroup objRightGroup = new clsGroup();
                            DataView dtvRight = new DataView(dtbRight);
                            dtvRight.RowFilter = "groupid_chr = '" + arlRightGroupID[j].ToString().Trim() + "'";
                            objRightGroup.m_dtvGroupData = dtvRight;
                            objRightGroup.m_strGroupName = dtbRight.Rows[0]["print_title_vchr"].ToString().Trim();
                            arlRightGroup.Add(objRightGroup);
                        }
                        ArrayList[] arlTemp = new ArrayList[2];
                        arlTemp[0] = arlLeftGroup;
                        arlTemp[1] = arlRightGroup;
                        p_arlPage.Add(arlTemp);
                    }
                    else
                    {
                        intRightEnd = intLeftEnd;
                        ArrayList[] arlTemp = new ArrayList[2];
                        arlTemp[0] = arlLeftGroup;
                        arlTemp[1] = null;
                        p_arlPage.Add(arlTemp);
                    }
                    #endregion
                    if (intRightEnd >= p_dtvResult.Count)
                    {
                        m_intPageCount = i;
                    }
                }
                else if (p_dtvResult.Count <= intRightEnd + 2 * m_intEachPageSideCount)
                {
                    //2.1
                    #region ��ӡ���
                    ArrayList arlLeftGroupID = new ArrayList();
                    ArrayList arlLeftGroup = new ArrayList();

                    DataTable dtbLeft = p_dtvResult.Table.Clone();
                    //					int intLeftEnd = 0;

                    if (p_dtvResult.Count <= intRightEnd + m_intEachPageSideCount)
                    {
                        intLeftEnd = p_dtvResult.Count;
                    }
                    else
                    {
                        intLeftEnd = intRightEnd + m_intEachPageSideCount;
                    }
                    for (int j = intRightEnd; j < intLeftEnd; j++)
                    {
                        if (j > intRightEnd)
                        {
                            if (p_dtvResult[j]["groupid_chr"].ToString().Trim() != p_dtvResult[j - 1]["groupid_chr"].ToString().Trim())
                            {
                                if (j >= (intRightEnd + m_intEachPageSideCount / 2 - 1) && (p_dtvResult.Count - j + 1) < m_intEachPageSideCount)
                                {
                                    intLeftEnd = j;
                                    break;
                                }
                                else
                                {
                                    if (intLeftEnd - j > 3)
                                    {
                                        arlLeftGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                                        intLeftEnd--;
                                    }
                                    else
                                    {
                                        intLeftEnd = j;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            arlLeftGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                        }

                        DataRow dr = dtbLeft.NewRow();
                        dr.ItemArray = p_dtvResult[j].Row.ItemArray;
                        dtbLeft.Rows.Add(dr);
                    }

                    //�����ӡ��Group
                    for (int j = 0; j < arlLeftGroupID.Count; j++)
                    {
                        clsGroup objLeftGroup = new clsGroup();
                        DataView dtvLeft = new DataView(dtbLeft);
                        dtvLeft.RowFilter = "groupid_chr = '" + arlLeftGroupID[j].ToString().Trim() + "'";
                        objLeftGroup.m_dtvGroupData = dtvLeft;
                        objLeftGroup.m_strGroupName = dtbLeft.Rows[0]["print_title_vchr"].ToString().Trim();
                        arlLeftGroup.Add(objLeftGroup);
                    }
                    #endregion
                    #region ��ӡ�ұ�
                    if (intLeftEnd < p_dtvResult.Count)
                    {
                        //�ֱߴ�ӡ
                        m_blnHasTwoPart = true;

                        ArrayList arlRightGroupID = new ArrayList();
                        ArrayList arlRightGroup = new ArrayList();

                        DataTable dtbRight = p_dtvResult.Table.Clone();

                        intRightEnd = intLeftEnd + m_intEachPageSideCount;
                        if (intRightEnd > p_dtvResult.Count)
                        {
                            intRightEnd = p_dtvResult.Count;
                        }
                        for (int j = intLeftEnd; j < intRightEnd; j++)
                        {
                            if (j > intLeftEnd)
                            {
                                if (p_dtvResult[j]["groupid_chr"].ToString().Trim() != p_dtvResult[j - 1]["groupid_chr"].ToString().Trim())
                                {
                                    if (intRightEnd - j > 3)
                                    {
                                        arlRightGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                                        intRightEnd--;
                                    }
                                    else
                                    {
                                        intRightEnd = j;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                arlRightGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                            }

                            DataRow dr = dtbRight.NewRow();
                            dr.ItemArray = p_dtvResult[j].Row.ItemArray;
                            dtbRight.Rows.Add(dr);
                        }

                        //�����ӡ��RightGroup
                        for (int j = 0; j < arlRightGroupID.Count; j++)
                        {
                            clsGroup objRightGroup = new clsGroup();
                            DataView dtvRight = new DataView(dtbRight);
                            dtvRight.RowFilter = "groupid_chr = '" + arlRightGroupID[j].ToString().Trim() + "'";
                            objRightGroup.m_dtvGroupData = dtvRight;
                            objRightGroup.m_strGroupName = dtbRight.Rows[0]["print_title_vchr"].ToString().Trim();
                            arlRightGroup.Add(objRightGroup);
                        }
                        ArrayList[] arlTemp = new ArrayList[2];
                        arlTemp[0] = arlLeftGroup;
                        arlTemp[1] = arlRightGroup;
                        p_arlPage.Add(arlTemp);
                    }
                    else
                    {
                        intRightEnd = intLeftEnd;
                        ArrayList[] arlTemp = new ArrayList[2];
                        arlTemp[0] = arlLeftGroup;
                        arlTemp[1] = null;
                        p_arlPage.Add(arlTemp);
                    }
                    #endregion
                    if (i == m_intPageCount - 1 && intRightEnd < p_dtvResult.Count)
                    {
                        m_intPageCount++;
                    }
                    if (intRightEnd >= p_dtvResult.Count)
                    {
                        m_intPageCount = i;
                    }
                }
                else if (p_dtvResult.Count > intRightEnd)
                {
                    //2.2
                    #region ��ӡ���
                    ArrayList arlLeftGroupID = new ArrayList();
                    ArrayList arlLeftGroup = new ArrayList();

                    DataTable dtbLeft = p_dtvResult.Table.Clone();
                    //					int intLeftEnd = 0;

                    if (p_dtvResult.Count <= intRightEnd + m_intEachPageSideCount)
                    {
                        intLeftEnd = p_dtvResult.Count;
                    }
                    else
                    {
                        intLeftEnd = intRightEnd + m_intEachPageSideCount;
                    }
                    for (int j = intRightEnd; j < intLeftEnd; j++)
                    {
                        if (j > intRightEnd)
                        {
                            if (p_dtvResult[j]["groupid_chr"].ToString().Trim() != p_dtvResult[j - 1]["groupid_chr"].ToString().Trim())
                            {
                                if (intLeftEnd - j > 3)
                                {
                                    arlLeftGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                                    intLeftEnd--;
                                }
                                else
                                {
                                    intLeftEnd = j;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            arlLeftGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                        }

                        DataRow dr = dtbLeft.NewRow();
                        dr.ItemArray = p_dtvResult[j].Row.ItemArray;
                        dtbLeft.Rows.Add(dr);
                    }

                    //�����ӡ��Group
                    for (int j = 0; j < arlLeftGroupID.Count; j++)
                    {
                        clsGroup objLeftGroup = new clsGroup();
                        DataView dtvLeft = new DataView(dtbLeft);
                        dtvLeft.RowFilter = "groupid_chr = '" + arlLeftGroupID[j].ToString().Trim() + "'";
                        objLeftGroup.m_dtvGroupData = dtvLeft;
                        objLeftGroup.m_strGroupName = dtbLeft.Rows[0]["print_title_vchr"].ToString().Trim();
                        arlLeftGroup.Add(objLeftGroup);
                    }
                    #endregion
                    #region ��ӡ�ұ�
                    if (intLeftEnd < p_dtvResult.Count)
                    {
                        //�ֱߴ�ӡ
                        m_blnHasTwoPart = true;

                        ArrayList arlRightGroupID = new ArrayList();
                        ArrayList arlRightGroup = new ArrayList();

                        DataTable dtbRight = p_dtvResult.Table.Clone();

                        intRightEnd = intLeftEnd + m_intEachPageSideCount;
                        if (intRightEnd > p_dtvResult.Count)
                        {
                            intRightEnd = p_dtvResult.Count;
                        }
                        for (int j = intLeftEnd; j < intRightEnd; j++)
                        {
                            if (j > intLeftEnd)
                            {
                                if (p_dtvResult[j]["groupid_chr"].ToString().Trim() != p_dtvResult[j - 1]["groupid_chr"].ToString().Trim())
                                {
                                    if (intRightEnd - j > 3)
                                    {
                                        arlRightGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                                        intRightEnd--;
                                    }
                                    else
                                    {
                                        intRightEnd = j;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                arlRightGroupID.Add(p_dtvResult[j]["groupid_chr"].ToString().Trim());
                            }

                            DataRow dr = dtbRight.NewRow();
                            dr.ItemArray = p_dtvResult[j].Row.ItemArray;
                            dtbRight.Rows.Add(dr);
                        }

                        //�����ӡ��RightGroup
                        for (int j = 0; j < arlRightGroupID.Count; j++)
                        {
                            clsGroup objRightGroup = new clsGroup();
                            DataView dtvRight = new DataView(dtbRight);
                            dtvRight.RowFilter = "groupid_chr = '" + arlRightGroupID[j].ToString().Trim() + "'";
                            objRightGroup.m_dtvGroupData = dtvRight;
                            objRightGroup.m_strGroupName = dtbRight.Rows[0]["print_title_vchr"].ToString().Trim();
                            arlRightGroup.Add(objRightGroup);
                        }
                        ArrayList[] arlTemp = new ArrayList[2];
                        arlTemp[0] = arlLeftGroup;
                        arlTemp[1] = arlRightGroup;
                        p_arlPage.Add(arlTemp);
                    }
                    else
                    {
                        intRightEnd = intLeftEnd;
                        ArrayList[] arlTemp = new ArrayList[2];
                        arlTemp[0] = arlLeftGroup;
                        arlTemp[1] = null;
                        p_arlPage.Add(arlTemp);
                    }
                    #endregion
                    if (i == m_intPageCount - 1 && intRightEnd < p_dtvResult.Count)
                    {
                        m_intPageCount++;
                    }
                    if (intRightEnd >= p_dtvResult.Count)
                    {
                        m_intPageCount = i;
                    }
                }
            }
        }

        /// <summary>
        /// ��ӡ Group
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        /// <param name="p_dtvGroupData"></param>
        /// <param name="p_strGroupName"></param>
        /// <param name="p_fltX"></param>
        /// <param name="p_fltY"></param>
        /// <param name="p_fltWidth"></param>
        /// <param name="p_fltEndY"></param>
        private void m_mthPrintGroup(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg, System.Data.DataView p_dtvGroupData,
            string p_strGroupName, float p_fltX, float p_fltY, float p_fltWidth, out float p_fltEndY, bool p_blnIsTop)
        {
            p_fltEndY = 0;
            try
            {
                //				p_objPrintArg.Graphics.DrawLine(m_GridPen,p_fltX,p_fltY,p_fltX+p_fltWidth,p_fltY);

                if (p_blnIsTop)
                {
                    p_fltY += 6;
                }
                float[] fltColumnXArr = new float[3];

                if (m_blnHasTwoPart)
                {
                    fltColumnXArr[0] = p_fltX + 8f;
                    fltColumnXArr[1] = p_fltX + p_fltWidth * 0.36f + 8f;
                    fltColumnXArr[2] = p_fltX + p_fltWidth * 0.60f;// + 15f;
                }
                else
                {
                    fltColumnXArr[0] = p_fltX + 8f;
                    fltColumnXArr[1] = p_fltX + fltColumnXArr[0] + m_fltItemAndResultSpace;
                    fltColumnXArr[2] = p_fltX + fltColumnXArr[1] + m_fltResultAndRefSpace;// + 15f;
                }

                p_objPrintArg.Graphics.DrawString(p_strGroupName, m_fntSmallBold, Brushes.Black, fltColumnXArr[0], p_fltY);
                p_objPrintArg.Graphics.DrawString(" ���", m_fntSmallBold, Brushes.Black, fltColumnXArr[1], p_fltY);
                p_objPrintArg.Graphics.DrawString("�ο���Χ", m_fntSmallBold, Brushes.Black, fltColumnXArr[2], p_fltY);

                //				fltColumnXArr[3]=p_fltX + p_fltWidth*0.85f;
                //				p_objPrintArg.Graphics.DrawString("��λ",m_fntSmallBold,Brushes.Black,fltColumnXArr[3],p_fltY);


                //				p_objPrintArg.Graphics.DrawLine(m_GridPen,p_fltX,p_fltY+2+m_fntSmallBold.Height,p_fltX+p_fltWidth,p_fltY+2+m_fntSmallBold.Height);

                //��ӡ�걾��Ŀ���� 2004.06.03
                p_dtvGroupData.Sort = "REPORT_PRINT_SEQ_INT ASC,SAMPLE_PRINT_SEQ_INT ";

                //��ӡ����Ŀ��
                SizeF ResultPrintWidthSF = p_objPrintArg.Graphics.MeasureString(" ��� 1", m_fntSmallBold);
                float fltResultPrintWidth = ResultPrintWidthSF.Width;
                //fltColumnXArr[2] - fltColumnXArr[1] + 12;

                //��λY������
                float fltCurrentY = p_fltY + 2;
                #region ������ӡ��¼
                for (int i = 0; i < p_dtvGroupData.Count; i++)
                {
                    string strResult = p_dtvGroupData[i].Row["result_vchr"].ToString().Trim();
                    string strAbnormal = p_dtvGroupData[i].Row["ABNORMAL_FLAG_CHR"].ToString().Trim();
                    string strUnit = p_dtvGroupData[i].Row["UNIT_VCHR"].ToString().Trim();
                    string strRefRange = p_dtvGroupData[i].Row["refrange_vchr"].ToString() + " " + strUnit;
                    string strMinVal = p_dtvGroupData[i].Row["MIN_VAL_DEC"].ToString().Trim();
                    string strMaxVal = p_dtvGroupData[i].Row["MAX_VAL_DEC"].ToString().Trim();
                    string strCheckItemName = p_dtvGroupData[i].Row["RPTNO_CHR"].ToString().Trim();//CHECK_ITEM_NAME_VCHR"].ToString().Trim();
                    //					string strDeviceCheckItemName = p_dtvGroupData[i].Row["DEVICE_CHECK_ITEM_NAME_VCHR"].ToString().Trim();
                    //					double doubleResult = -1; //�����Ϊ������ʱ����ֵĬ��Ϊ��1
                    //					double doubleMinVal = -1;
                    //					double doubleMaxVal = -1;

                    fltCurrentY += m_fltItemSpace;

                    //					if(i==0)
                    //						p_objPrintArg.Graphics.DrawString((fltCurrentY).ToString().Trim(),m_fntSmallBold,Brushes.Black,m_lngWidthPage*0.9f,fltCurrentY);

                    p_objPrintArg.Graphics.DrawString(strCheckItemName, m_fntSmall2NotBold, Brushes.Black, fltColumnXArr[0], fltCurrentY);
                    //��ӡ���X���λ��
                    //					fltColumnXArr[1]=p_fltX + p_fltWidth*0.36f-10f;
                    #region ��ӡ ָʾ��ͷ
                    //1.�����쳣��־�ж�,�˴���Ϊ�쳣��־ֻ��"H"(��)��"L"(��)�������
                    if (strAbnormal != null)
                    {
                        System.Drawing.Font objBoldFont = new Font("SimSun", 9, FontStyle.Bold);
                        string strPR;

                        strPR = strResult + " " + "��";
                        SizeF objBoldSF = p_objPrintArg.Graphics.MeasureString(strPR, objBoldFont);
                        SizeF objNotBoldSF = p_objPrintArg.Graphics.MeasureString(strPR, objBoldFont);

                        if (strAbnormal == "H")
                        {
                            strPR = strResult + " " + "��";
                            objBoldSF = p_objPrintArg.Graphics.MeasureString(strPR, objBoldFont);
                            float fltStartPos = fltColumnXArr[1] + fltResultPrintWidth - objBoldSF.Width;
                            //							strPR = strPR.PadLeft(10);
                            p_objPrintArg.Graphics.DrawString(strPR, objBoldFont, Brushes.Black, fltStartPos, fltCurrentY);
                        }
                        else if (strAbnormal == "L")
                        {
                            if (strResult.Contains(">") || strResult.Contains("<"))
                                strPR = strResult + " " + "��";
                            else
                                strPR = strResult + " " + "��";
                            float fltStartPos = fltColumnXArr[1] + fltResultPrintWidth - objBoldSF.Width;
                            //							strPR = strPR.PadLeft(10);
                            p_objPrintArg.Graphics.DrawString(strPR, objBoldFont, Brushes.Black, fltStartPos, fltCurrentY);
                        }
                        else
                        {
                            strPR = strResult + " " + " ";
                            float fltStartPos = fltColumnXArr[1] + fltResultPrintWidth - objNotBoldSF.Width;
                            //							strPR = strPR.PadLeft(10);

                            p_objPrintArg.Graphics.DrawString(strPR, m_fntSmall2NotBold, Brushes.Black, fltStartPos, fltCurrentY);
                        }
                    }
                    else
                    {
                        #region old ���� 2004.4.21 14:10
                        //						//2.�������ֵ��Сֵ�ж�
                        //						try
                        //						{
                        //							doubleResult = double.Parse(strResult);
                        //							doubleMinVal = double.Parse(strMinVal);
                        //							doubleMaxVal = double.Parse(strMaxVal);
                        //						}
                        //						catch(Exception objEx)
                        //						{
                        //							//throw objEx;
                        //						}
                        //						finally
                        //						{
                        //							if(doubleResult != -1)
                        //							{
                        //								if(doubleResult < doubleMinVal)
                        //								{
                        //									p_objPrintArg.Graphics.DrawString("��"+strResult,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[1],fltCurrentY);
                        //								}
                        //								else if(doubleResult > doubleMaxVal)
                        //								{
                        //									p_objPrintArg.Graphics.DrawString("��"+strResult,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[1],fltCurrentY);
                        //								}
                        //								else
                        //								{
                        //									p_objPrintArg.Graphics.DrawString("  "+strResult,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[1],fltCurrentY);
                        //								}
                        //							}
                        //							else
                        //							{
                        //								p_objPrintArg.Graphics.DrawString("  "+strResult,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[1],fltCurrentY);
                        //							}
                        //						}
                        #endregion
                    }
                    #endregion

                    p_objPrintArg.Graphics.DrawString(strRefRange, m_fntSmallNotBold, Brushes.Black, fltColumnXArr[2], fltCurrentY);
                    //					p_objPrintArg.Graphics.DrawString(strUnit,m_fntSmallNotBold,Brushes.Black,fltColumnXArr[3],fltCurrentY);
                }
                #endregion
                fltCurrentY += m_fntSmallNotBold.Height;
                p_fltEndY = fltCurrentY;
            }
            catch { }
        }

        #endregion

        #region ���ﲿ��
        //���ﲿ��
        private void m_mthPrintSummary(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {
            //			if(m_lngY < 366)
            //			{
            //				m_lngY = 366;
            //			}
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            SizeF sfSummary = p_objPrintArg.Graphics.MeasureString(strSummary, m_fntSmallNotBold);
            long lngTemp = m_lngY + (long)sfSummary.Height + (long)m_fntSmallBold.Height + 4;
            if (lngTemp > m_lngEndPosition)
            {
                if (m_blnHasPrintGroup)
                {
                    m_intPageCount++;
                    m_blnPrintSummary = false;
                }
                else
                {
                    m_blnPrintSummary = false;
                }
            }
            else
            {
                m_blnPrintSummary = true;
            }

            if (!m_blnPrintSummary)
                return;

            //			int count = strSummary.Length;
            SizeF szPerWord = p_objPrintArg.Graphics.MeasureString("��", m_fntSmallNotBold);//��ȡһ���ַ��Ŀ��
            //			int intWordCount = strSummary.Length;//(int)(szSummary.Width/szPerWord.Width) + 1;
            //			int intWordCountPerLine = (int)((m_lngWidthPage - (m_lngWidthPage*(this.m_fltLeftIndentProp + this.m_fltRightIndentProp) + szPerWord.Width + 4 + 4 + 4))/szPerWord.Width);//ÿһ�е��ַ�����
            //			int intRowCount = intWordCount/intWordCountPerLine;


            p_objPrintArg.Graphics.DrawString("ʵ������ʾ��", m_fntSmallBold, Brushes.Black, m_lngWidthPage * 0.08f, m_lngY + 6);
            //			p_objPrintArg.Graphics.DrawString(m_lngY.ToString().Trim(),m_fntSmallBold,Brushes.Black,m_lngWidthPage*0.8f,m_lngY+4);
            m_lngY = m_lngY + m_fntSmallBold.Height + 4;

            long CurrentY = m_lngY;
            if (strSummary != null)
            {
                float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp + szPerWord.Width + 8;
                float fltRightX = m_lngWidthPage * 0.92f - fltLeftX;
                CurrentY += 4;
                long lngEndY = CurrentY + m_fntSmallNotBold.Height * 2 + 3;
                Rectangle rectSummary = new Rectangle((int)fltLeftX, (int)CurrentY, (int)fltRightX, (int)lngEndY);
                new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fntSmallNotBold).m_mthPrintText(m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim(),
                    m_dtbSample.Rows[0]["XML_SUMMARY_VCHR"].ToString().Trim(), m_fntSmallNotBold, Color.Black, rectSummary, p_objPrintArg.Graphics);
                //				p_objPrintArg.Graphics.DrawString(strSummary,m_fntSmallNotBold,Brushes.Black,new RectangleF(fltLeftX,CurrentY,fltRightX,lngEndY));
                CurrentY = lngEndY;
            }
            m_lngY += m_fntSmallNotBold.Height * 2 + 4;
        }
        #endregion

        #region ��ӡ���浥��β����
        /// <summary>
        /// ��ӡ���浥��β����
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {
            if (m_blnDocked)
            {
                if (m_lngY < m_lngEndPosition)
                {
                    m_lngY = m_lngEndPosition;
                }
            }
            // ����ҽ��
            string strCheckEmp = m_dtbSample.Rows[0]["reportor"].ToString().Trim();//m_dtbResult.Rows[0]["checkPerson"].ToString().Trim();
            // ���ҽ�� ����ʱ�Ǽ���ҽ����
            string strConfirmEmp = m_dtbSample.Rows[0]["reportor"].ToString().Trim();//m_dtbResult.Rows[0]["confirmPerson"].ToString().Trim();

            //			p_objPrintArg.Graphics.DrawString(m_lngY.ToString(),m_fntSmallNotBold,Brushes.Black,m_lngWidthPage*0.8f,m_lngY);
            m_lngY += 10;

            float fltCurrent = m_lngWidthPage * m_fltLeftIndentProp;
            SizeF sf = p_objPrintArg.Graphics.MeasureString("(�����������ٴ����Ʋο���ֻ�Ըü��ı걾����!)��ע:", m_fntSmallNotBold);
            p_objPrintArg.Graphics.DrawString("(�����������ٴ����Ʋο���ֻ�Ըü��ı걾����!)��ע:", m_fntSmallNotBold, Brushes.Black, fltCurrent, m_lngY);
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["ANNOTATION_VCHR"].ToString().Trim(), m_fntSmallNotBold, Brushes.Black,
                fltCurrent + sf.Width, m_lngY);//
            //			p_objPrintArg.Graphics.DrawString(m_lngY.ToString(),m_fntSmallNotBold,Brushes.Black,m_lngWidthPage*0.8f,m_lngY);
            m_lngY += m_fntSmallNotBold.Height + 6;
            //����
            p_objPrintArg.Graphics.DrawLine(m_GridPen, m_lngWidthPage * 0.08f, m_lngY, m_lngWidthPage * 0.92f, m_lngY);

            m_lngY += 6;
            p_objPrintArg.Graphics.DrawString("��������:", m_fntSmallBold, Brushes.Black, fltCurrent, m_lngY);
            string strReportDate = "";
            if (Microsoft.VisualBasic.Information.IsDate(m_dtbSample.Rows[0]["CONFIRM_DAT"]))
            {
                strReportDate = ((DateTime)m_dtbSample.Rows[0]["CONFIRM_DAT"]).ToString().Trim();//DateTime.Now.Date.ToString("yyyy-MM-dd");
            }
            SizeF szTmp = p_objPrintArg.Graphics.MeasureString("��������:", m_fntSmallBold);
            fltCurrent += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(strReportDate, m_fntSmallBold, Brushes.Black, fltCurrent, m_lngY);


            p_objPrintArg.Graphics.DrawString("����ҽ��:", m_fntSmallBold, Brushes.Black, m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 1.3f / 3), m_lngY);

            fltCurrent = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 1.3f / 3) + szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(strCheckEmp, m_fntSmallBold, Brushes.Black, fltCurrent, m_lngY);

            fltCurrent = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 2 / 3) + szTmp.Width * 3 / 4 + 4;
            p_objPrintArg.Graphics.DrawString("�����:", m_fntSmallBold, Brushes.Black, fltCurrent, m_lngY);
            szTmp = p_objPrintArg.Graphics.MeasureString("�����:", m_fntSmallBold);
            fltCurrent = fltCurrent + szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(strCheckEmp, m_fntSmallBold, Brushes.Black, fltCurrent, m_lngY);
            m_lngY += m_fntSmallBold.Height + 6;
            //			p_objPrintArg.Graphics.DrawString(m_lngY.ToString(),m_fntSmallBold,Brushes.Black,fltCurrent,m_lngY);
            //			p_objPrintArg.Graphics.DrawString(((int)(m_lngY+m_fntSmallBold.Height)).ToString(),m_fntSmallBold,Brushes.Black,fltCurrent,m_lngY+m_fntSmallBold.Height);


        }
        #endregion


        private void m_mthPrintPageSub(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {
            m_mthPrintStart(p_objPrintArg);
            m_mthPrintLine(p_objPrintArg);
            m_mthPrintMiddle(p_objPrintArg);
            if (m_intCurrentPage >= m_intPageCount)
            {
                m_mthPrintSummary(p_objPrintArg);
            }
            m_mthPrintEnd(p_objPrintArg);
            m_bolFinishPrint = true;
        }


        #region �̳д�ӡ�ӿ�
        /// <summary>
        /// ��ʼ����ӡ����
        /// </summary>
        /// <param name="p_objArg">�ⲿ��Ҫ��ʼ���ı��������ݲ�ͬ��ʵ��ʹ��</param>
        public void m_mthInitPrintTool(object p_objArg)
        {
            m_fntTitle = new Font("SimSun", 18, FontStyle.Bold);
            m_fntSmallBold = new Font("SimSun", 11, FontStyle.Bold);
            m_fntSmallNotBold = new Font("SimSun", 10f, FontStyle.Regular);
            m_fntSmall2NotBold = new Font("SimSun", 9f, FontStyle.Regular);
            m_fntHeadNotBold = new Font("SimSun", 11f, FontStyle.Regular);

            m_GridPen = new Pen(Color.Black, 1);

            m_fltItemSpace = m_fntSmall2NotBold.Height / 4 + m_fntSmall2NotBold.Height;

            m_fltLeftIndentProp = 0.1f;
            m_fltRightIndentProp = 0.1f;

            #region ��ӡ����
            try
            {
                //				System.Configuration.AppSettingsReader objAppSettingReader = new System.Configuration.AppSettingsReader();
                //				string strWidth = objAppSettingReader.GetValue("LISReportPrintPaperWidth",typeof(double)).ToString().Trim();
                //				string strHeight = objAppSettingReader.GetValue("LISReportPrintPaperHeight",typeof(double)).ToString().Trim();
                //				if(	Microsoft.VisualBasic.Information.IsNumeric(strWidth) 
                //					&& Microsoft.VisualBasic.Information.IsNumeric(strHeight))
                //				{
                //					double dblWidth_cm = double.Parse(strWidth);//double.Parse(this.m_txtPaperWidth.Text.Trim());
                //					double dblHeight_cm = double.Parse(strHeight);//double.Parse(this.m_txtPaperHeight.Text.Trim());
                //					int intWidth_01mm =  (int)(dblWidth_cm * 100);
                //					int intHeight_01mm = (int)(dblHeight_cm * 100);
                //					int intWidth_001inc = System.Drawing.Printing.PrinterUnitConvert.Convert(intWidth_01mm,PrinterUnit.TenthsOfAMillimeter,PrinterUnit.Display);
                //					int intHeight_001inc = System.Drawing.Printing.PrinterUnitConvert.Convert(intHeight_01mm,PrinterUnit.TenthsOfAMillimeter,PrinterUnit.Display);
                //					ps.PaperSize = new PaperSize("LIS",intWidth_001inc,intHeight_001inc);
                PaperSize ps = null;
                //				foreach(PaperSize objPs in ((System.Drawing.Printing.PrintDocument)p_objArg).PrinterSettings.PaperSizes)
                //				{
                //					if(objPs.PaperName == "LIS_Report")
                //					{
                //						ps = objPs;
                //						break;
                //					}
                //				}
                //				if(ps != null)
                //				{
                //					((System.Drawing.Printing.PrintDocument)p_objArg).DefaultPageSettings.PaperSize = ps;
                //				}
                ps = ((System.Drawing.Printing.PrintDocument)p_objArg).PrinterSettings.DefaultPageSettings.PaperSize;
                m_intEachSideItemHeight = ps.Height - 311;
                m_lngEndPosition = ps.Height - 123;
                m_intEachPageSideItemHeight = ps.Height - 123 - 114;
                //				}
            }
            catch
            {
                MessageBox.Show("��ӡ�����ϣ�", "iCare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //				MessageBox.Show(ex.Message);

            }
            #endregion

        }

        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���
        /// </summary>
        public void m_mthInitPrintContent()
        {

        }

        /// <summary>
        /// �ͷŴ�ӡ����
        /// </summary>
        /// <param name="p_objArg">�ⲿʹ�õ��ı��������ݲ�ͬ��ʵ��ʹ��</param>
        public void m_mthDisposePrintTools(object p_objArg)
        {
        }

        /// <summary>
        /// ��ӡ��ʼ
        /// </summary>
        /// <param name="p_objPrintArg">��ӡ�ĵ�</param>
        public void m_mthBeginPrint(object p_objPrintArg)
        {
            #region ��ʼ����ӡ����
            m_dtbSample = ((clsPrintValuePara)p_objPrintArg).m_dtbBaseInfo;
            m_dtbResult = ((clsPrintValuePara)p_objPrintArg).m_dtbResult;
            m_strTitle = ((clsPrintValuePara)p_objPrintArg).m_strTitle;
            //			m_lngEndPosition = 448;
            m_fltItemAndResultSpace = 80;//��Ŀ���ƺͽ��֮��ľ���
            m_fltResultAndRefSpace = 30;//����Ͳο���Χ֮��ľ���

            m_intEachSideCount = (int)(m_intEachSideItemHeight / m_fltItemSpace);
            m_intEachPageSideCount = (int)(m_intEachPageSideItemHeight / m_fltItemSpace);

            m_intCurrentPage = 1;

            if ((m_dtbResult.Rows.Count) % (2 * m_intEachSideCount) > 0)
            {
                m_intPageCount = (m_dtbResult.Rows.Count) / (2 * m_intEachSideCount) + 1;
            }
            else
            {
                m_intPageCount = (m_dtbResult.Rows.Count) / (2 * m_intEachSideCount);
            }
            #endregion
        }


        /// <summary>
        /// ��ӡ��
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
            if (m_intCurrentPage < m_intPageCount)
            {
                ((PrintPageEventArgs)p_objPrintArg).HasMorePages = true;
            }
            else
            {
                ((PrintPageEventArgs)p_objPrintArg).HasMorePages = false;
            }
            m_intCurrentPage++;
        }

        /// <summary>
        /// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthEndPrint(object p_objPrintArg)
        {

        }
        #endregion

    }

}

