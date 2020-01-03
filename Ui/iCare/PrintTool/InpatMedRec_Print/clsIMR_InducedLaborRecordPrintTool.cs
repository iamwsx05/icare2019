using System;
using System.IO;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Windows.Forms;

namespace iCare 
{
	/// <summary>
	/// ���������¼����ӡ������.
	/// </summary>
	public class clsIMR_InducedLaborRecordPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_InducedLaborRecordPrintTool(string p_strTypeID) :base(p_strTypeID)
		{}


		private clsPrintInPatMedRecItem[] m_objPrintMultiItemArr;
		private clsPrintInPatMedRecSign[] m_objPrintSignArr;
       
	
		protected override void m_mthSetPrintLineArr()
		{

			m_mthInitPrintLineArr();
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  new clsPrintPatientFixInfo("�� �� �� ¼ ��",320),
                                          m_objPrintMultiItemArr[0],m_objPrintMultiItemArr[1],m_objPrintMultiItemArr[2],
                                          m_objPrintMultiItemArr[3],m_objPrintMultiItemArr[4],
                                          new clsPrintAmniotomyOdinopoeiaRecord(),
                                          m_objPrintMultiItemArr[5],
                                          new clsPrintDrugOdinopoeiaRecord(),
                                          m_objPrintMultiItemArr[6]
									  });
		}
	
		private void m_mthInitPrintLineArr()
		{
			m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[7];
			for(int j2=0;j2<m_objPrintMultiItemArr.Length;j2++)
				m_objPrintMultiItemArr[j2] = new clsPrintInPatMedRecItem();
		}

        protected override void m_mthSetSubPrintInfo()
        {
            #region ����-���
            m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[] { "","����>>ͣ��","����>>ͣ��","����>>������","����>>������","",
                "����>>��������>>��","����>>��������>>��"},
                new string[] { "���ߣ�", "ͣ����$$", "#�£�$$", "�����ڣ�$$", "#�£�$$", "�������У�$$", "#��$$", "#��$$" });
            m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[] { "","�������>>ĩ���¾�","�������>>̥��ʱ��","",
                "�������>>������Ѫ>>��","�������>>������Ѫ>>��"},
                new string[] { "���������$$", "ĩ���¾���$$", "��̥��ʱ�䣺$$", "��������Ѫ��$$", "#��$$", "#��$$" });
            m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[] { "","����ʷ>>��","����ʷ>>��","����ʷ>>��","����ʷ>>��","����ʷ>>��ǰ","����ʷ>>��ǰ","",
                "����ʷ>>��","����ʷ>>��","����ʷ>>Ů","����ʷ>>Ů"},
                new string[] { "����ʷ��", "��$$", "#��$$", "��", "#��$$", "ĩ�β�", "#��ǰ$$", "����", "$$", "#��$$", "$$", "#Ů$$" });
            m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[] { "","��ȥʷ>>���ಡ","��ȥʷ>>����","����ʷ>>��Ѫѹ",
                "��ȥʷ>>����","��ȥʷ>>��Ѫ�Լ���","��ȥʷ>>�����Լ���","��ȥʷ>>����"},
                new string[] { "��ȥʷ��", "���ಡ��$$", "���ף�", "��Ѫѹ��", "���ף�", "��Ѫ�Լ�����", "�����Լ�����", "������" });
            m_objPrintMultiItemArr[4].m_mthSetPrintValue(new string[] { "","���>>Ƥ��>>��","���>>Ƥ��>>��","���>>��",
                "���>>��","���>>��","���>>Ƣ","","���>>�ӹ���","���>>��Χ","���>>��Χ","���>>��Χ","���>>̥λ","���>>̥��̥�������","���>>���"},
                new string[] { "\n��죺Ƥ����", "#��$$", "#��$$", "�ģ�", "�Σ�", "�Σ�","Ƣ��", "�ӹ��� X ��Χ��", "$$", "#X$$","$$", "#����$$", "̥λ��", "̥��+̥����+��⣺", "��ϣ�" }); 
            #endregion
            #region ��ŵ��-��¼
            m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"��Ĥǻ������������¼>>��ŵ��","��Ĥǻ������������¼>>��ŵ��","",
				"��Ĥǻ������������¼>>����Ѫѹ1","��Ĥǻ������������¼>>����Ѫѹ2","��Ĥǻ������������¼>>����Ѫѹ2","","��Ĥǻ������������¼>>��¼"},
                new string[] { "\n0.5%��ŵ��", "#������Ĥǻע��$$", "����Ѫѹ��", "$$", "#/$$", "$$","kPa$$", "��¼��" });
            #endregion
            #region ����Ѫѹ-��ע
            m_objPrintMultiItemArr[6].m_mthSetPrintValue(new string[]{"","�������>>����Ѫѹ1","�������>>����Ѫѹ2","�������>>����Ѫѹ2",
                "","�������>>������ҩ","","�������>>������","","�������>>̥��","�������>>̥��","","�������>>̥��","�������>>̥��",
                "","�������>>�Ա�","��ע","","ǩ��"},
               new string[] { "\n����Ѫѹ��", "$$", "#/$$", "$$", "kPa��������ҩ����ǡ��߲��ء�$$", "$$", "�����ߣ�", "$$", "̥����","$$", "#����$$",
                                "̥�أ�" ,"$$","#kg$$","�Ա�","$$","\n��ע��","\n\n                                                                                                          ǩ����$$","$$"});
            #endregion

        }
	#region Print Class
        /// <summary>
        /// ��ӡ��
        /// </summary>
        /// <param name="e"></param>
        protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, 40, 165, 710, clsPrintPosition.c_intBottomY - 230);
        }
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        protected override  void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            SolidBrush  m_slbBrush = new SolidBrush(Color.Black);
            Font m_fotSmallFont = new Font("SimSun", 12); 
            //ҽԺ����
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle.Trim(), new Font("SimSun", 15), m_slbBrush, 300,50);
            //������
            e.Graphics.DrawString(m_strChildTitleName, new Font("SimSun",18,FontStyle.Bold), Brushes.Black, 300,90);

            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, 80,135);

            e.Graphics.DrawString(m_objPrintInfo.m_strAreaName, m_fotSmallFont, m_slbBrush, 130,135);

            e.Graphics.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, 300,135);

            e.Graphics.DrawString(m_objPrintInfo.m_strBedName, m_fotSmallFont, m_slbBrush, 350,135);

            e.Graphics.DrawString("סԺ�ţ�", m_fotSmallFont, m_slbBrush, 450,135);

            e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID, m_fotSmallFont, m_slbBrush, 515,135);
        }
        /// <summary>
        /// ����������
        /// </summary>
        private class clsPrintPatientFixInfo : clsIMR_PrintLineBase
        {
            public clsPrintPatientFixInfo() { }
            public clsPrintPatientFixInfo(string p_strChildTitleName, int p_intChildTitleNameOffSetX)
            {
                m_strChildTitleName = p_strChildTitleName;
                m_intChildTitleNameOffSetX = p_intChildTitleNameOffSetX;

            }
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                p_intPosY -= 30;
                Font m_fotSmallFont = new Font("SimSun", 10.5f);
                p_objGrp.DrawString("������" + (m_objPrintInfo == null ? "" : m_objPrintInfo.m_strPatientName), m_fotSmallFont, Brushes.Black, m_intPatientInfoX - 20, p_intPosY);
                p_objGrp.DrawString("���䣺" + (m_objPrintInfo == null ? "" : m_objPrintInfo.m_strAge), m_fotSmallFont, Brushes.Black, m_intPatientInfoX + 150, p_intPosY);
                p_objGrp.DrawString("���" + (m_objPrintInfo == null ? "" : m_objPrintInfo.m_strMarried), m_fotSmallFont, Brushes.Black, m_intPatientInfoX + 300, p_intPosY);
                p_intPosY += 20;
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// ��Ĥǻ������������¼
        /// </summary>
        private class  clsPrintAmniotomyOdinopoeiaRecord: clsIMR_PrintLineBase
        {
            public clsPrintAmniotomyOdinopoeiaRecord() { }
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                string m_strFirstPiqureTime = "";//����ʱ��1
                string m_strFirstPiqurePubes = "";//�ܹ���1
                string m_strFirstPiqureSideOpen = "";//�����Կ�1
                string m_strFirstPiqureSucceed = "";//���̶��ٴγɹ�1
                string m_strFirstPiqureOperator = "";//����1
                string m_strSecondPiqureTime = "";//����ʱ��2
                string m_strSecondPiqurePubes = "";//�ܹ���2
                string m_strSecondPiqureSideOpen = "";//�����Կ�2
                string m_strSecondPiqureSucceed = "";//���̶��ٴγɹ�2
                string m_strSecondPiqureOperator = "";//����2
                #region 
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains("��Ĥǻ������������¼>>��һ�δ���ʱ��"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��Ĥǻ������������¼>>��һ�δ���ʱ��"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strFirstPiqureTime = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��Ĥǻ������������¼>>��һ�δ���>>�ܹ���"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��Ĥǻ������������¼>>��һ�δ���>>�ܹ���"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strFirstPiqurePubes = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��Ĥǻ������������¼>>��һ�δ���>>�����Կ�"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��Ĥǻ������������¼>>��һ�δ���>>�����Կ�"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strFirstPiqureSideOpen = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��Ĥǻ������������¼>>��һ�δ���>>���̶��ٴγɹ�"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��Ĥǻ������������¼>>��һ�δ���>>���̶��ٴγɹ�"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strFirstPiqureSucceed = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��Ĥǻ������������¼>>��һ�δ���>>����"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��Ĥǻ������������¼>>��һ�δ���>>����"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strFirstPiqureOperator = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��Ĥǻ������������¼>>�ڶ��δ���ʱ��"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��Ĥǻ������������¼>>�ڶ��δ���ʱ��"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strSecondPiqureTime = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��Ĥǻ������������¼>>�ڶ��δ���>>�ܹ���"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��Ĥǻ������������¼>>�ڶ��δ���>>�ܹ���"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strSecondPiqurePubes = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��Ĥǻ������������¼>>�ڶ��δ���>>�����Կ�"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��Ĥǻ������������¼>>�ڶ��δ���>>�����Կ�"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strSecondPiqureSideOpen = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��Ĥǻ������������¼>>�ڶ��δ���>>���̶��ٴγɹ�"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��Ĥǻ������������¼>>�ڶ��δ���>>���̶��ٴγɹ�"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strSecondPiqureSucceed = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��Ĥǻ������������¼>>�ڶ��δ���>>����"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��Ĥǻ������������¼>>�ڶ��δ���>>����"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strSecondPiqureOperator = objInpatItem.m_strItemContent;
                        }
                    }
                }
#endregion
                p_intPosY += 15;
                p_objGrp.DrawString("��Ĥǻ������������¼" , p_fntNormalText, Brushes.Black, m_intPatientInfoX -10, p_intPosY);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX - 20, p_intPosY + 20, 736, p_intPosY + 20);//��1
                p_objGrp.DrawString("���̴���", new Font("SimSun", 10), Brushes.Black, m_intPatientInfoX - 13, p_intPosY + 24);
                p_objGrp.DrawString("ʱ��(�ꡢ�¡��ա�ʱ����)", new Font("SimSun",10), Brushes.Black, m_intPatientInfoX + 65, p_intPosY + 24);
                p_objGrp.DrawString("�ܹ��ϣ�����", new Font("SimSun", 10), Brushes.Black, m_intPatientInfoX + 245, p_intPosY + 24);
                p_objGrp.DrawString("�����Կ�������", new Font("SimSun", 10), Brushes.Black, m_intPatientInfoX + 338, p_intPosY + 24);
                p_objGrp.DrawString("���̶��ٴγɹ�", new Font("SimSun", 10), Brushes.Black, m_intPatientInfoX + 455, p_intPosY + 24);
                p_objGrp.DrawString("��  ��", new Font("SimSun", 10), Brushes.Black, m_intPatientInfoX + 588, p_intPosY + 24);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX - 20, p_intPosY + 44, 736, p_intPosY + 44);//��2
                p_objGrp.DrawString("��һ�δ���", new Font("SimSun",10), Brushes.Black, m_intPatientInfoX -16, p_intPosY + 48);
                p_objGrp.DrawString(m_strFirstPiqureTime, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 65, p_intPosY + 48);
                p_objGrp.DrawString(m_strFirstPiqurePubes, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 250, p_intPosY + 48);
                p_objGrp.DrawString(m_strFirstPiqureSideOpen, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 340, p_intPosY + 48);
                p_objGrp.DrawString(m_strFirstPiqureSucceed, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 455, p_intPosY + 48);
                p_objGrp.DrawString(m_strFirstPiqureOperator, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 565, p_intPosY + 48);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX - 20, p_intPosY + 68, 736, p_intPosY + 68);//��3
                p_objGrp.DrawString("�ڶ��δ���", new Font("SimSun", 10), Brushes.Black, m_intPatientInfoX - 16, p_intPosY + 72);
                p_objGrp.DrawString(m_strSecondPiqureTime, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 65, p_intPosY + 72);
                p_objGrp.DrawString(m_strSecondPiqurePubes, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 250, p_intPosY + 72);
                p_objGrp.DrawString(m_strSecondPiqureSideOpen, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 340, p_intPosY + 72);
                p_objGrp.DrawString(m_strSecondPiqureSucceed, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 455, p_intPosY + 72);
                p_objGrp.DrawString(m_strSecondPiqureOperator, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 565, p_intPosY + 72);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX - 20, p_intPosY + 92, 736, p_intPosY + 92);//��4
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX -20,p_intPosY + 20, m_intPatientInfoX - 20, p_intPosY + 92);//��1
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 57, p_intPosY + 20,m_intPatientInfoX + 57, p_intPosY + 92);//��2
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 242, p_intPosY + 20, m_intPatientInfoX + 242, p_intPosY + 92);//��3
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 336, p_intPosY + 20, m_intPatientInfoX + 336, p_intPosY + 92);//��4
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 450, p_intPosY + 20, m_intPatientInfoX + 450, p_intPosY + 92);//��5
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 558, p_intPosY + 20, m_intPatientInfoX + 558, p_intPosY + 92);//��6
                p_objGrp.DrawLine(Pens.Black, 736, p_intPosY + 20, 736, p_intPosY + 92);//��7
                p_intPosY += 112;
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// ҩ��������¼���������
        /// </summary>
        private class clsPrintDrugOdinopoeiaRecord : clsIMR_PrintLineBase
        {
            public clsPrintDrugOdinopoeiaRecord() { }
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                string m_strOdinopoeiaDrug1 = "";//������ҩ1
                string m_strOdinopoeiaTime1 = "";//ʱ��1
                string m_strAdministrationWay1 = "";//��ҩ;��1
                string m_strOdinopoeiaRecord1 = "";//��¼1
                string m_strOdinopoeiaDrug2 = "";//������ҩ2
                string m_strOdinopoeiaTime2 = "";//ʱ��2
                string m_strAdministrationWay2 = "";//��ҩ;��2
                string m_strOdinopoeiaRecord2 = "";//��¼2

                string m_strAfterOperationDate1 = "";//��������1
                string m_strBloodPressure1 = "";//Ѫѹ1
                string m_strPalace1 = "";//����1
                string m_strQuickening1 = "";//̥��1
                string m_strFetusHeart1 = "";//̥��1
                string m_strVaginaSecretion1 = "";//����������1
                string m_strOther1 = "";//����1
                string m_strOperator1 = "";//��¼��1
                string m_strAfterOperationDate2 = "";//��������2
                string m_strBloodPressure2 = "";//Ѫѹ2
                string m_strPalace2 = "";//����2
                string m_strQuickening2 = "";//̥��2
                string m_strFetusHeart2 = "";//̥��2
                string m_strVaginaSecretion2 = "";//����������2
                string m_strOther2 = "";//����2
                string m_strOperator2 = "";//��¼��2

                string m_strPalaceBeginTime = "";//������ʼʱ��
                string m_strBreakCoatTime = "";//��Ĥʱ��
                string m_strChildBirthTime = "";//̥�����ʱ��
                string m_strMazaBirthTime = "";//̥�����ʱ��
                string m_strRecordTime = "";//��¼ʱ��
     
                #region
                if (m_hasItems != null)
                {
                    #region ҩ��������¼1
                    if (m_hasItems.Contains("ҩ��������¼>>������ҩ1"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["ҩ��������¼>>������ҩ1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strOdinopoeiaDrug1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("ҩ��������¼>>ʱ��1"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["ҩ��������¼>>ʱ��1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strOdinopoeiaTime1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("ҩ��������¼>>��ҩ;��1"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["ҩ��������¼>>��ҩ;��1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strAdministrationWay1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("ҩ��������¼>>��¼1"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["ҩ��������¼>>��¼1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strOdinopoeiaRecord1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("ҩ��������¼>>������ҩ2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["ҩ��������¼>>������ҩ2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strOdinopoeiaDrug2 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("ҩ��������¼>>ʱ��2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["ҩ��������¼>>ʱ��2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strOdinopoeiaTime2 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("ҩ��������¼>>��ҩ;��2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["ҩ��������¼>>��ҩ;��2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strAdministrationWay2 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("ҩ��������¼>>��¼2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["ҩ��������¼>>��¼2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strOdinopoeiaRecord2 = objInpatItem.m_strItemContent;
                        }
                    }
#endregion
                    #region ҩ��������¼2
                    if (m_hasItems.Contains("��������>>1"))
                     {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��������>>1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strAfterOperationDate1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("Ѫѹ>>1"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["Ѫѹ>>1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strBloodPressure1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("����>>1"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["����>>1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strPalace1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("̥��>>1"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["̥��>>1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strQuickening1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("̥��>>1"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["̥��>>1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strFetusHeart1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("����������>>1"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["����������>>1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strVaginaSecretion1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("����>>1"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["����>>1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strOther1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��¼��>>1"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��¼��>>1"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strOperator1 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��������>>2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��������>>2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strAfterOperationDate2 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("Ѫѹ>>2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["Ѫѹ>>2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strBloodPressure2 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("����>>2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["����>>2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strPalace2 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("̥��>>2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["̥��>>2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strQuickening2 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("̥��>>2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["̥��>>2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strFetusHeart2 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("����������>>2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["����������>>2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strVaginaSecretion2 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("����>>2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["����>>2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strOther2 = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("��¼��>>2"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["��¼��>>2"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strOperator2 = objInpatItem.m_strItemContent;
                        }
                    }
#endregion
                    #region �������
                    if (m_hasItems.Contains("�������>>������ʼʱ��"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["�������>>������ʼʱ��"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strPalaceBeginTime = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("�������>>��Ĥʱ��"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["�������>>��Ĥʱ��"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strBreakCoatTime = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("�������>>̥�����ʱ��"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["�������>>̥�����ʱ��"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strChildBirthTime = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("�������>>̥�����ʱ��"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["�������>>̥�����ʱ��"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strMazaBirthTime = objInpatItem.m_strItemContent;
                        }
                    }
                    if (m_hasItems.Contains("�������>>��¼ʱ��"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["�������>>��¼ʱ��"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strRecordTime = objInpatItem.m_strItemContent;
                        }
                    }
                    #endregion
                }
                #endregion
                #region ҩ��������¼1
                p_intPosY += 10;
                p_objGrp.DrawString("ҩ��������¼", p_fntNormalText, Brushes.Black, m_intPatientInfoX -10, p_intPosY);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX -20, p_intPosY + 20, 736, p_intPosY + 20);//��1
                p_objGrp.DrawString("������ҩ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 15, p_intPosY + 24);
                p_objGrp.DrawString("ʱ��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 200, p_intPosY + 24);
                p_objGrp.DrawString("��ҩ;��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 383, p_intPosY + 24);
                p_objGrp.DrawString("�� ¼", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 587, p_intPosY + 24);

                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX -20, p_intPosY + 44, 736, p_intPosY + 44);//��2
                p_objGrp.DrawString(m_strOdinopoeiaDrug1, p_fntNormalText, Brushes.Black, m_intPatientInfoX -10, p_intPosY + 48);
                p_objGrp.DrawString(m_strOdinopoeiaTime1, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 150, p_intPosY + 48);
                p_objGrp.DrawString(m_strAdministrationWay1, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 300, p_intPosY + 48);
                p_objGrp.DrawString(m_strOdinopoeiaRecord1, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 563, p_intPosY + 48);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX -20, p_intPosY + 68, 736, p_intPosY + 68);//��3
                p_objGrp.DrawString(m_strOdinopoeiaDrug2, p_fntNormalText, Brushes.Black, m_intPatientInfoX -10, p_intPosY + 72);
                p_objGrp.DrawString(m_strOdinopoeiaTime2, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 150, p_intPosY + 72);
                p_objGrp.DrawString(m_strAdministrationWay2, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 300, p_intPosY + 72);
                p_objGrp.DrawString(m_strOdinopoeiaRecord2, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 563, p_intPosY + 72);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX -20, p_intPosY + 92, 736, p_intPosY + 92);//��4
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX -20, p_intPosY + 20, m_intPatientInfoX - 20, p_intPosY + 92);//��1
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 150, p_intPosY + 20, m_intPatientInfoX + 150, p_intPosY + 92);//��2
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 300, p_intPosY + 20, m_intPatientInfoX + 300, p_intPosY + 92);//��3
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 550, p_intPosY + 20, m_intPatientInfoX + 550, p_intPosY + 92);//��4
                p_objGrp.DrawLine(Pens.Black,  736, p_intPosY + 20,  736, p_intPosY + 92);//��5
                p_intPosY +=  106;
                #endregion
                #region ҩ��������¼2
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX - 20, p_intPosY + 20, 736, p_intPosY + 20);//��1
                p_objGrp.DrawString("��������", p_fntNormalText, Brushes.Black, m_intPatientInfoX -10, p_intPosY + 24);
                p_objGrp.DrawString("Ѫѹ��kPa", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 85, p_intPosY + 24);
                p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 190, p_intPosY + 24);
                p_objGrp.DrawString("̥��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 255, p_intPosY + 24);
                p_objGrp.DrawString("̥��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 315, p_intPosY + 24);
                p_objGrp.DrawString("����������", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 385, p_intPosY + 24);
                p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 518 , p_intPosY + 24);
                p_objGrp.DrawString("��¼��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 595, p_intPosY + 24);

                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX - 20, p_intPosY + 44, 736, p_intPosY + 44);//��2
                p_objGrp.DrawString(m_strAfterOperationDate1, p_fntNormalText, Brushes.Black, m_intPatientInfoX - 10, p_intPosY + 48);
                p_objGrp.DrawString(m_strBloodPressure1, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 90, p_intPosY + 48);
                p_objGrp.DrawString(m_strPalace1, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 190, p_intPosY + 48);
                p_objGrp.DrawString(m_strQuickening1, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 255, p_intPosY + 48);
                p_objGrp.DrawString(m_strFetusHeart1, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 315, p_intPosY + 48);
                p_objGrp.DrawString(m_strVaginaSecretion1, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 375, p_intPosY + 48);
                p_objGrp.DrawString(m_strOther1, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 505, p_intPosY + 48);
                p_objGrp.DrawString(m_strOperator1, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 595, p_intPosY + 48);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX - 20, p_intPosY + 68, 736, p_intPosY + 68);//��3
                p_objGrp.DrawString(m_strAfterOperationDate2, p_fntNormalText, Brushes.Black, m_intPatientInfoX - 10, p_intPosY + 72);
                p_objGrp.DrawString(m_strBloodPressure2, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 90, p_intPosY + 72);
                p_objGrp.DrawString(m_strPalace2, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 190, p_intPosY + 72);
                p_objGrp.DrawString(m_strQuickening2, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 255, p_intPosY + 72);
                p_objGrp.DrawString(m_strFetusHeart2, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 315, p_intPosY + 72);
                p_objGrp.DrawString(m_strVaginaSecretion2, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 375, p_intPosY + 72);
                p_objGrp.DrawString(m_strOther2, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 505, p_intPosY + 72);
                p_objGrp.DrawString(m_strOperator2, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 595, p_intPosY + 72);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX - 20, p_intPosY + 92, 736, p_intPosY + 92);//��4
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX - 20, p_intPosY + 20, m_intPatientInfoX -20, p_intPosY + 92);//��1
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 80, p_intPosY + 20, m_intPatientInfoX + 80, p_intPosY + 92);//��2
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 180, p_intPosY + 20, m_intPatientInfoX + 180, p_intPosY + 92);//��3
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 243, p_intPosY + 20, m_intPatientInfoX + 243, p_intPosY + 92);//��4
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 305, p_intPosY + 20, m_intPatientInfoX + 305, p_intPosY + 92);//��5
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 365, p_intPosY + 20, m_intPatientInfoX + 365, p_intPosY + 92);//��6
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 495, p_intPosY + 20, m_intPatientInfoX + 495, p_intPosY + 92);//��7
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 585, p_intPosY + 20, m_intPatientInfoX + 585, p_intPosY + 92);//��8
                p_objGrp.DrawLine(Pens.Black, 736, p_intPosY + 20, 736, p_intPosY + 92);//��9

                p_intPosY += 116;
                #endregion
                #region �������
                p_objGrp.DrawString("���������", p_fntNormalText, Brushes.Black, m_intPatientInfoX -10, p_intPosY);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX -20, p_intPosY + 20, 736, p_intPosY + 20);//��1
                p_objGrp.DrawString("��Ŀ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 15, p_intPosY + 24);
                p_objGrp.DrawString("������ʼ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 100, p_intPosY + 24);
                p_objGrp.DrawString("��Ĥ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 193, p_intPosY + 24);
                p_objGrp.DrawString("̥�����", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 250, p_intPosY + 24);
                p_objGrp.DrawString("̥���������Ȼ�����ͽ�ְ���", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 332, p_intPosY + 24);
                p_objGrp.DrawString("�� ¼", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 598, p_intPosY + 24);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX -20, p_intPosY + 44, 736, p_intPosY + 44);//��2
                p_objGrp.DrawString("ʱ�� ������ʱ��", new Font("SimSun", 9), Brushes.Black, m_intPatientInfoX - 15, p_intPosY + 48);
                p_objGrp.DrawString(m_strPalaceBeginTime, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 103, p_intPosY + 48);
                p_objGrp.DrawString(m_strBreakCoatTime, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 188, p_intPosY + 48);
                p_objGrp.DrawString(m_strChildBirthTime, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 255, p_intPosY + 48);
                p_objGrp.DrawString(m_strMazaBirthTime, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 335, p_intPosY + 48);
                p_objGrp.DrawString(m_strRecordTime, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 595, p_intPosY + 48);
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX - 20, p_intPosY + 68, 736, p_intPosY + 68);//��3
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX - 20, p_intPosY + 20, m_intPatientInfoX - 20, p_intPosY + 68);//��1
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 95, p_intPosY + 20, m_intPatientInfoX + 95, p_intPosY + 68);//��2
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 177, p_intPosY + 20, m_intPatientInfoX + 177, p_intPosY + 68);//��3
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 245, p_intPosY + 20, m_intPatientInfoX + 245, p_intPosY + 68);//��4
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 325, p_intPosY + 20, m_intPatientInfoX + 325, p_intPosY + 68);//��5
                p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX + 580, p_intPosY + 20, m_intPatientInfoX + 580, p_intPosY + 68);//��6
                p_objGrp.DrawLine(Pens.Black,  736, p_intPosY + 20,  736, p_intPosY + 68);//��7
                p_intPosY += 80;
                m_blnHaveMoreLine = false;
                #endregion
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
            }
        }
		/// <summary>
		/// ��Ŀ��ӡ
		/// </summary>
		private class clsPrintInPatMedRecItem : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10.5f));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string m_strSpecialTitle = "";
			private string m_strTitle = "";
			private string m_strText = "";
			private string m_strTextXml = "";
			private bool m_blnNoContent = false;
			private bool m_blnNoPrint = true;
			private clsInpatMedRec_Item m_objItemContent = null;

			public clsPrintInPatMedRecItem()
			{}
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnNoContent == true && m_blnNoPrint == true || m_hasItems == null || m_hasItems.Count == 0 || m_objContent == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					if(m_strTitle != "")
					{
						p_objGrp.DrawString(m_strTitle,p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
						p_intPosY += 20;

                        if (m_blnIsPrintMark)
                        {
                            m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent == null ? "" : m_objItemContent.m_strItemContent), (m_objItemContent == null ? "<root />" : m_objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, m_objItemContent != null);
                            m_mthAddSign2(m_strTitle, m_objPrintContext.m_ObjModifyUserArr);
                        }
                        else
                        {
                            m_objPrintContext.m_mthSetContextWithAllCorrect((m_objItemContent == null ? "" : m_objItemContent.m_strItemContent), (m_objItemContent == null ? "<root />" : m_objItemContent.m_strItemContentXml));
                        }

						
					}
					else
					{
						if(m_strSpecialTitle != "")
						{
							p_intPosY += 20;
							p_objGrp.DrawString(m_strSpecialTitle,clsIMR_HerbalismPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+300,p_intPosY);
							p_intPosY += 40;
						}

                        if (m_blnIsPrintMark)
                        {
                            m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strText, m_strTextXml, m_dtmFirstPrintTime, m_blnNoPrint == false);
                            m_mthAddSign2(m_strSpecialTitle, m_objPrintContext.m_ObjModifyUserArr);

                        }
                        else
                        {
                            m_objPrintContext.m_mthSetContextWithAllCorrect(m_strText, m_strTextXml);
                        }

						
					}

					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					if(m_strTitle != "")
						m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth-40,m_intRecBaseX+20,p_intPosY,p_objGrp);
					else
						m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 40,m_intRecBaseX+10,p_intPosY,p_objGrp);
					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
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
			/// <summary>
			/// ���ö����ӡ����
			/// </summary>
			/// <param name="p_strKeyArr">��ӡ���ݵĹ�ϣ������</param>
			/// <param name="p_strTitleArr">С��������(����Ӧ�ڴ���Lable�����洢�����ݿ�����ӡ������)</param>
			public void m_mthSetPrintValue(string[] p_strKeyArr,string[] p_strTitleArr)
			{
				if(p_strKeyArr == null || p_strTitleArr == null || p_strKeyArr.Length != p_strTitleArr.Length)
				{
					m_blnNoContent = true;
					return;
				}
				m_blnNoPrint = false;
				if(m_blnHavePrintInfo(p_strKeyArr) == true)
					m_mthMakeText(p_strTitleArr,p_strKeyArr,ref m_strText,ref m_strTextXml);
			}
			/// <summary>
			/// ���õ����ӡ����
			/// </summary>
			/// <param name="p_strKey">��ϣ��</param>
			/// <param name="p_strTitle">С����</param>
			public void m_mthSetPrintValue(string p_strKey,string p_strTitle)
			{
				if(m_hasItems != null && p_strKey != null)
					if(m_hasItems.Contains(p_strKey))
						m_objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
				m_strTitle = p_strTitle;
			}
			/// <summary>
			/// ���ô�����硰����顱
			/// </summary>
			/// <param name="p_strTitle"></param>
			public void m_mthSetSpecialTitleValue(string p_strTitle)
			{
				m_strSpecialTitle = p_strTitle;
			}

		}

		/// <summary>
		/// ǩ��������
		/// </summary>
		private class clsPrintInPatMedRecSign : clsIMR_PrintLineBase
		{
			private clsInpatMedRec_Item[] objSignContent = null;
			private string[] m_strTitleArr = null;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(objSignContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				p_intPosY += 40;
				for(int i=0; i<objSignContent.Length; i++)
				{
					if(m_strTitleArr[i].IndexOf("����") < 0)
					{
						p_objGrp.DrawString(m_strTitleArr[i]+(objSignContent[i]==null ? "" : objSignContent[i].m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+400,p_intPosY);
						p_intPosY += 20;
					}
					else
					{
						p_objGrp.DrawString(m_strTitleArr[i]+ (objSignContent[i] == null ? "" :DateTime.Parse( objSignContent[i].m_strItemContent).ToString("yyyy��MM��dd��")),p_fntNormalText,Brushes.Black,m_intRecBaseX+400,p_intPosY);
						p_intPosY += 20;
					}
				}
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
			}
			/// <summary>
			/// ����ǩ��������ֵ
			/// </summary>
			/// <param name="p_strkeyArr">ֵ</param>
			/// <param name="p_strTitleArr">����</param>
			public void m_mthSetPrintSignValue(string[] p_strkeyArr,string[] p_strTitleArr)
			{
				if(p_strkeyArr == null || p_strTitleArr == null || p_strkeyArr.Length != p_strTitleArr.Length)
					return;
				objSignContent = m_objGetContentFromItemArr(p_strkeyArr);
				m_strTitleArr = p_strTitleArr;
			}

		}

		#endregion
	}
}
