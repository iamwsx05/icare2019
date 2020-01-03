using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// Summary description for clsIMR_Obstetric_CriterionPrintTool.������Ժ��¼
	/// </summary>
	public class clsIMR_Obstetric_CriterionPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_Obstetric_CriterionPrintTool(string p_strTypeID) :base(p_strTypeID)
		{}


		protected override void m_mthSetPrintLineArr()
		{

			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
                                        new clsPrintPatientFixInfo("������Ժ��¼",320),
										new clsPrinDetail1(), 
                                        new clsPrinDetail2(),
                                        new clsPrintSubInf(),                   
                                        new clsPrinDetail3(1),                                       
										new clsPrinDetail4(),
                                        new clsPrintHomeHistroy(), 
                                        new clsPrint10(), 
                                        new clsPrint11(),
                                        new clsPrintDate(),
                                        new clsPrintHomeHistroy2(),
                                        new clsPrint12(), 
                                        new clsPrint13(),
                                        new clsPrintLastDate(),
                    new clsPrintInPatMedRecDia2(),
                    new clsPrintInPatMedRecDia3()

			});
		}

		/// <summary>
		/// �в�ʷ����ӡ
		/// </summary>
		private class clsPrintSubInf : clsIMR_PrintLineBase
		{
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
//			private bool m_blnIsFirstPrint = true;
			private clsInpatMedRec_Item[][] m_objItemArr = new clsInpatMedRec_Item[3][];
			private int m_intYPos = 10;
			private int m_intXPos = (int)enmRectangleInfo.LeftX - 10;
			private int m_intWidth = (int)enmRectangleInfo.LeftX+(int)enmRectangleInfoInPatientCaseInfo.PrintWidth2+21;
			private string[] m_strItemArr1 = {"�в�ʷ>>1>>�д�","�в�ʷ>>1>>��","�в�ʷ>>1>>��","�в�ʷ>>1>>����>>��Ȼ","�в�ʷ>>1>>����>>�˹�","�в�ʷ>>1>>����","�в�ʷ>>1>>��̥","�в�ʷ>>1>>����","�в�ʷ>>1>>���","�в�ʷ>>1>>���²�","�в�ʷ>>1>>���䷽ʽ>>˳��","�в�ʷ>>1>>���䷽ʽ>>��λ","�в�ʷ>>1>>���䷽ʽ>>������ʽ","�в�ʷ>>1>>���䷽ʽ>>����","�в�ʷ>>1>>�������>>��Ѫ","�в�ʷ>>1>>�������>>������","�в�ʷ>>1>>�ϲ�֢�򲢷�֢","�в�ʷ>>1>>С�����>>�Ա�","�в�ʷ>>1>>С�����>>����","�в�ʷ>>1>>С�����>>�� ��>>ʱ��","�в�ʷ>>1>>С�����>>�� ��>>ԭ��","�в�ʷ>>1>>С�����>>����֢","�в�ʷ>>1>>��������","�в�ʷ>>1>>����"};
			private string[] m_strItemArr2 = {"�в�ʷ>>2>>�д�","�в�ʷ>>2>>��","�в�ʷ>>2>>��","�в�ʷ>>2>>����>>��Ȼ","�в�ʷ>>2>>����>>�˹�","�в�ʷ>>2>>����","�в�ʷ>>2>>��̥","�в�ʷ>>2>>����","�в�ʷ>>2>>���","�в�ʷ>>2>>���²�","�в�ʷ>>2>>���䷽ʽ>>˳��","�в�ʷ>>2>>���䷽ʽ>>��λ","�в�ʷ>>2>>���䷽ʽ>>������ʽ","�в�ʷ>>2>>���䷽ʽ>>����","�в�ʷ>>2>>�������>>��Ѫ","�в�ʷ>>2>>�������>>������","�в�ʷ>>2>>�ϲ�֢�򲢷�֢","�в�ʷ>>2>>С�����>>�Ա�","�в�ʷ>>2>>С�����>>����","�в�ʷ>>2>>С�����>>�� ��>>ʱ��","�в�ʷ>>2>>С�����>>�� ��>>ԭ��","�в�ʷ>>2>>С�����>>����֢","�в�ʷ>>2>>��������","�в�ʷ>>2>>����"};
			private string[] m_strItemArr3 = {"�в�ʷ>>3>>�д�","�в�ʷ>>3>>��","�в�ʷ>>3>>��","�в�ʷ>>3>>����>>��Ȼ","�в�ʷ>>3>>����>>�˹�","�в�ʷ>>3>>����","�в�ʷ>>3>>��̥","�в�ʷ>>3>>����","�в�ʷ>>3>>���","�в�ʷ>>3>>���²�","�в�ʷ>>3>>���䷽ʽ>>˳��","�в�ʷ>>3>>���䷽ʽ>>��λ","�в�ʷ>>3>>���䷽ʽ>>������ʽ","�в�ʷ>>3>>���䷽ʽ>>����","�в�ʷ>>3>>�������>>��Ѫ","�в�ʷ>>3>>�������>>������","�в�ʷ>>3>>�ϲ�֢�򲢷�֢","�в�ʷ>>3>>С�����>>�Ա�","�в�ʷ>>3>>С�����>>����","�в�ʷ>>3>>С�����>>�� ��>>ʱ��","�в�ʷ>>3>>С�����>>�� ��>>ԭ��","�в�ʷ>>3>>С�����>>����֢","�в�ʷ>>3>>��������","�в�ʷ>>3>>����"};

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
//				if(m_blnHavePrintInfo(m_strItemArr1) == false && m_blnHavePrintInfo(m_strItemArr2) == false && m_blnHavePrintInfo(m_strItemArr3) == false  || m_hasItems == null)
//				{
//					m_blnHaveMoreLine = false;
//					return;
//				}
                m_intYPos = p_intPosY + 25;
				m_objItemArr[0] = m_objGetContentFromItemArr(m_strItemArr1);
				m_objItemArr[1] = m_objGetContentFromItemArr(m_strItemArr2);
				m_objItemArr[2] = m_objGetContentFromItemArr(m_strItemArr3);

               
				m_mthDrawTitle(p_intPosY,p_objGrp, p_fntNormalText);
                m_mthPrintContent(p_intPosY, p_objGrp, this.m_objItemArr);

				p_intPosY+= 245;

				m_blnHaveMoreLine = false;
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
//				m_blnIsFirstPrint = true;
			}
			/// <summary>
			/// ��ӡ����������
			/// </summary>
			/// <param name="p_intPosY"></param>
			/// <param name="p_objGrp"></param>
			/// <param name="p_fntNormalText"></param>
			private void m_mthDrawTitle(int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				StringFormat stfTitle = new StringFormat(StringFormatFlags.FitBlackBox);
				int intRowWidth = 30;
				int intRow = 20;
				int intHeight = 200;
                m_intXPos = (int)enmRectangleInfo.LeftX - 10;

				#region Print Titles and Line
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intWidth,m_intYPos);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+intRow,m_intYPos,m_intXPos+intRow,m_intYPos+intHeight);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intHeight,m_intWidth,m_intYPos+intHeight);
				p_objGrp.DrawString(" ��  ��  ʷ",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos,intRow,intHeight),stfTitle);
				m_intXPos += intRow;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+80,m_intWidth+1,m_intYPos+80);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+120,m_intWidth+1,m_intYPos+120);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+160,m_intWidth+1,m_intYPos+160);

				p_objGrp.DrawString(" ��  ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos,intRow,80),stfTitle);
				m_intXPos += intRow;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString("   ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos,intRow,80),stfTitle);
				m_intXPos += 36;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString("   ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos,intRow,80),stfTitle);
				m_intXPos += intRow;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString("����",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos+5,m_intYPos+2,40,intRow),stfTitle);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos+60,m_intYPos+intRow);
				p_objGrp.DrawString(" ��Ȼ",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow,25,60),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos,m_intYPos+intHeight);
				p_objGrp.DrawString(" �˹�",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow,25,60),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString(" ��  ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos,intRow,80),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString(" ��  ̥",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos,intRow,80),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString(" ��  ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos,intRow,80),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString(" ��  ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos,intRow,80),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString(" ���²�",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos,intRow,80),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString("  ���䷽ʽ",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+2,115,intRow),stfTitle);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos+130,m_intYPos+intRow);
				p_objGrp.DrawString(" ˳��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow,25,60),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos,m_intYPos+intHeight);
				p_objGrp.DrawString(" ��λ",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow,25,60),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos,m_intYPos+intHeight);
				p_objGrp.DrawString("������ʽ",new Font("SimSun",8),Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow,40,30),stfTitle);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+50,m_intXPos+40,m_intYPos+50);
				p_objGrp.DrawString("����ǯ����",new Font("SimSun",8),Brushes.Black,new RectangleF(m_intXPos,m_intYPos+50,40,30),stfTitle);
				m_intXPos += 40;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos,m_intYPos+intHeight);
				p_objGrp.DrawString(" ��  ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow,25,60),stfTitle);
				m_intXPos += 35;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString("�������",new Font("SimSun",8),Brushes.Black,new RectangleF(m_intXPos,m_intYPos+2,50,intRow),stfTitle);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos+60,m_intYPos+intRow);
				p_objGrp.DrawString(" ��  Ѫ",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow,25,60),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos,m_intYPos+intHeight);
				p_objGrp.DrawString("������",new Font("SimSun",10),Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow+2,25,60),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);

				p_objGrp.DrawString("�ϲ�֢�򲢷�֢",new Font("SimSun",10),Brushes.Black,new RectangleF(m_intXPos,m_intYPos+2,40,80),stfTitle);
				m_intXPos += 40;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);

				p_objGrp.DrawString("  С�����",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+2,140,intRow),stfTitle);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos+140,m_intYPos+intRow);
				p_objGrp.DrawString(" ��  ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow,25,60),stfTitle);
				m_intXPos += 20;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos,m_intYPos+intHeight);
				p_objGrp.DrawString(" ��  ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow,25,60),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos,m_intYPos+intHeight);
				p_objGrp.DrawString("����",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow+2,50,intRow),stfTitle);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow*2,m_intXPos+60,m_intYPos+intRow*2);
				p_objGrp.DrawString("ʱ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+40,25,40),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow*2,m_intXPos,m_intYPos+intHeight);
				p_objGrp.DrawString("ԭ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+42,25,40),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+intRow,m_intXPos,m_intYPos+intHeight);
				p_objGrp.DrawString(" ����֢",new Font("SimSun",10),Brushes.Black,new RectangleF(m_intXPos,m_intYPos+intRow,25,60),stfTitle);
				m_intXPos += intRowWidth;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString("��������",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos+2,25,80),stfTitle);
				m_intXPos += 25;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+intHeight);
				
				p_objGrp.DrawString("  ��  ��",p_fntNormalText,Brushes.Black,new RectangleF(m_intXPos,m_intYPos,25,80),stfTitle);
				m_intXPos += intRowWidth;
				#endregion
			}
			/// <summary>
			/// 
			/// </summary>
			/// <param name="p_intPosY"></param>
			/// <param name="p_objGrp"></param>
			/// <param name="p_fntNormalText"></param>
            private void m_mthPrintContent(int p_intPosY, System.Drawing.Graphics p_objGrp, clsInpatMedRec_Item[][] m_objItemArr)
			{
				int intYPos = p_intPosY+107;
				int intRowHeight = 40;
				m_intXPos = (int)enmRectangleInfo.LeftX+10;
                if (m_objItemArr[0] != null || m_objItemArr[1] != null || m_objItemArr[2] != null)
                {
                    for (int i = 0; i < m_objItemArr.Length; i++)
                    {
                        int j2 = 0;
                        #region Print Content
                        RectangleF rtgf = new RectangleF(m_intXPos, intYPos, 20, intRowHeight);
                        m_mthPrintSubContent(m_objItemArr[i][j2++], rtgf, p_objGrp);
                        rtgf.X += rtgf.Width;
                        rtgf.Width = 36;
                        m_mthPrintSubContent(m_objItemArr[i][j2++], rtgf, p_objGrp);
                        rtgf.X += rtgf.Width;
                        rtgf.Width = 20;
                        m_mthPrintSubContent(m_objItemArr[i][j2++], rtgf, p_objGrp);
                        for (; j2 < 12; j2++)
                        {
                            rtgf.X += rtgf.Width;
                            rtgf.Width = 30;
                            m_mthPrintSubContent(m_objItemArr[i][j2], rtgf, p_objGrp);
                        }
                        rtgf.X += rtgf.Width;
                        rtgf.Width = 40;
                        m_mthPrintSubContent(m_objItemArr[i][j2++], rtgf, p_objGrp);
                        rtgf.X += rtgf.Width;
                        rtgf.Width = 35;
                        m_mthPrintSubContent(m_objItemArr[i][j2++], rtgf, p_objGrp);
                        rtgf.X += rtgf.Width;
                        rtgf.Width = 30;
                        m_mthPrintSubContent(m_objItemArr[i][j2++], rtgf, p_objGrp);
                        rtgf.X += rtgf.Width;
                        rtgf.Width = 30;
                        m_mthPrintSubContent(m_objItemArr[i][j2++], rtgf, p_objGrp);
                        rtgf.X += rtgf.Width;
                        rtgf.Width = 40;
                        m_mthPrintSubContent(m_objItemArr[i][j2++], rtgf, p_objGrp);
                        rtgf.X += rtgf.Width;
                        rtgf.Width = 20;
                        m_mthPrintSubContent(m_objItemArr[i][j2++], rtgf, p_objGrp);
                        for (; j2 < 24; j2++)
                        {
                            rtgf.X += rtgf.Width;
                            rtgf.Width = 30;
                            m_mthPrintSubContent(m_objItemArr[i][j2], rtgf, p_objGrp);
                        }
                        #endregion
                        intYPos += intRowHeight;
                    }
                }
			}
			private void m_mthPrintSubContent(clsInpatMedRec_Item p_objItem,RectangleF p_rtgContent,System.Drawing.Graphics p_objGrp)
			{
				if(p_objItem == null)
					return ;
				if(p_objItem.m_strItemContent == null || p_objItem.m_strItemContent == "")
					return ;
				Font fnt = new Font("SimSun",9);
				StringFormat stfTitle = new StringFormat(StringFormatFlags.FitBlackBox);
				p_objGrp.DrawString(p_objItem.m_strItemContent,fnt,Brushes.Black,p_rtgContent,stfTitle);
			}
		}

        /// <summary>
        /// ����,�ֲ�ʷ
        /// </summary>
		private class clsPrinDetail1 : clsIMR_PrintLineBase
		{
            private bool m_blnIsFirstPrint = true;
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsInpatMedRec_Item[][] m_objItemArr = new clsInpatMedRec_Item[1][];
			private string[] m_strItemArr = {"����","�ֲ�ʷ>>��","�ֲ�ʷ>>��","�ֲ�ʷ>>ĩ���¾�",
											  "�ֲ�ʷ>>Ԥ����","�ֲ�ʷ>>���з�Ӧ","�ֲ�ʷ>>����ʱ��",
											  "�ֲ�ʷ>>̥������ʱ��","�ֲ�ʷ>>�������","�ֲ�ʷ>>����ص�","�ֲ�ʷ>>�������"};

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                try
                {
                    int PostX = m_intPatientInfoX;
                    int intRowWidth = (int)enmRectangleInfoInPatientCaseInfo.PrintWidth2;
                    int intHeight = 50;
                    Font fnt = new Font("SimSun", 10.5f);
                    Font fnt1 = new Font("SimSun", 12, FontStyle.Bold);

                    m_objItemArr[0] = m_objGetContentFromItemArr(m_strItemArr);

                    if (m_objContent == null || m_objContent.m_objItemContents == null)
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    if (m_blnIsFirstPrint)
                    {
                        #region ����
                        PostX = m_intPatientInfoX - 30;
                        p_objGrp.DrawString("���ߣ�", fnt1, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[0] != null && m_objItemArr[0][0] != null)
                            p_objGrp.DrawString(m_objItemArr[0][0].m_strItemContent, fnt, Brushes.Black, new RectangleF(PostX + 50, p_intPosY + 1, intRowWidth - 30, intHeight - 50));
                        #endregion

                        #region �ֲ�ʷ
                        p_intPosY += intHeight + 10;
                        PostX = m_intPatientInfoX - 30;
                        p_objGrp.DrawString("�ֲ�ʷ�� ", fnt1, Brushes.Black, PostX, p_intPosY);
                        p_objGrp.DrawString("��_______  ��_______  ĩ���¾�___________________  Ԥ����__________________", fnt, Brushes.Black, PostX + 70, p_intPosY);
                        if (m_objItemArr[0] != null && m_objItemArr[0][1] != null)
                            p_objGrp.DrawString(m_objItemArr[0][1].m_strItemContent, fnt, Brushes.Black, PostX + 95, p_intPosY);//��
                        if (m_objItemArr[0] != null && m_objItemArr[0][2] != null)
                            p_objGrp.DrawString(m_objItemArr[0][2].m_strItemContent, fnt, Brushes.Black, PostX + 170, p_intPosY);//��
                        if (m_objItemArr[0] != null && m_objItemArr[0][3] != null)
                            p_objGrp.DrawString(DateTime.Parse(m_objItemArr[0][3].m_strItemContent).ToString("yyyy��MM��dd��"), fnt, Brushes.Black, PostX + 300, p_intPosY);//ĩ���¾�
                        if (m_objItemArr[0] != null && m_objItemArr[0][4] != null)
                            p_objGrp.DrawString(DateTime.Parse(m_objItemArr[0][4].m_strItemContent).ToString("yyyy��MM��dd��"), fnt, Brushes.Black, PostX + 495, p_intPosY);//Ԥ����

                        p_intPosY += 25;
                        PostX = m_intPatientInfoX - 30;
                        p_objGrp.DrawString("���з�Ӧ___________________  ����ʱ��___________________  ̥������ʱ��__________________", fnt, Brushes.Black, PostX + 70, p_intPosY);
                        if (m_objItemArr[0] != null && m_objItemArr[0][5] != null)
                            p_objGrp.DrawString(m_objItemArr[0][5].m_strItemContent, fnt, Brushes.Black, PostX + 135, p_intPosY);//���з�Ӧ
                        if (m_objItemArr[0] != null && m_objItemArr[0][6] != null)
                            p_objGrp.DrawString(m_objItemArr[0][6].m_strItemContent, fnt, Brushes.Black, PostX + 350, p_intPosY);//����ʱ��
                        if (m_objItemArr[0] != null && m_objItemArr[0][7] != null)
                            p_objGrp.DrawString(m_objItemArr[0][7].m_strItemContent, fnt, Brushes.Black, PostX + 590, p_intPosY);//̥������ʱ��

                        p_intPosY += 25;
                        PostX = m_intPatientInfoX - 30;
                        p_objGrp.DrawString("�������________  �ص�_________________________________________________________________", fnt, Brushes.Black, PostX + 70, p_intPosY);
                        if (m_objItemArr[0] != null && m_objItemArr[0][8] != null)
                            p_objGrp.DrawString(m_objItemArr[0][8].m_strItemContent, fnt, Brushes.Black, PostX + 140, p_intPosY);//�������
                        if (m_objItemArr[0] != null && m_objItemArr[0][9] != null)
                            p_objGrp.DrawString(m_objItemArr[0][9].m_strItemContent, fnt, Brushes.Black, PostX + 240, p_intPosY);//�ص�
                        #endregion
                        p_intPosY += 25;
                        PostX = m_intPatientInfoX - 30;
                        p_objGrp.DrawString("���������", fnt, Brushes.Black, PostX + 70, p_intPosY);
                        if (clsIMR_Obstetric_CriterionPrintTool.m_blnIsPrintMark)
                        {
                            m_objPrintContext.m_mthSetContextWithCorrectBefore(((m_objItemArr[0] == null || m_objItemArr[0][10] == null || m_objItemArr[0][10].m_strItemContent == null) ? "" : m_objItemArr[0][10].m_strItemContent), ((m_objItemArr[0] == null || m_objItemArr[0][10] == null || m_objItemArr[0][10].m_strItemContentXml == null) ? "<root />" : m_objItemArr[0][10].m_strItemContentXml), m_dtmFirstPrintTime, (m_objItemArr[0] == null || m_objItemArr[0][10] == null));
                            m_mthAddSign2("���������", m_objPrintContext.m_ObjModifyUserArr);
                        }
                        else
                        {
                            m_objPrintContext.m_mthSetContextWithAllCorrect(((m_objItemArr[0] == null || m_objItemArr[0][10] == null || m_objItemArr[0][10].m_strItemContent == null) ? "" : m_objItemArr[0][10].m_strItemContent), ((m_objItemArr[0] == null || m_objItemArr[0][10] == null || m_objItemArr[0][10].m_strItemContentXml == null) ? "<root />" : m_objItemArr[0][10].m_strItemContentXml));
                        }
                        m_blnIsFirstPrint = false;

                    }

                    if (m_objPrintContext.m_BlnHaveNextLine())
                    {
                        m_objPrintContext.m_mthPrintLine(intRowWidth - 140, 185, p_intPosY, p_objGrp);
                        p_intPosY += 20;
                    }
                    if (m_objPrintContext.m_BlnHaveNextLine())
                    {
                        m_blnHaveMoreLine = true;
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        p_intPosY += 15;
                    }
                }
                catch (Exception ex)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    objLogger.LogError(ex);
                }
                
            }

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
				m_blnIsFirstPrint = true;
			}


		}

        /// <summary>
        /// �¾�ʷ
        /// </summary>
        private class clsPrinDetail2 : clsIMR_PrintLineBase
        {
            private bool m_blnIsFirstPrint = true;
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private clsInpatMedRec_Item[][] m_objItemArr = new clsInpatMedRec_Item[1][];
            private string[] m_strItemArr = {"�¾�ʷ>>����","�¾�ʷ>>����","�¾�ʷ>>��>>��","�¾�ʷ>>��>>��","�¾�ʷ>>��>>��","�¾�ʷ>>ʹ��>>��",
                                                 "�¾�ʷ>>ʹ��>>��","�¾�ʷ>>ʹ��>>��","�¾�ʷ>>ʹ��>>��",
                                                 "�¾�ʷ>>Ѫ��>>��","�¾�ʷ>>Ѫ��>>��"};

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                int PostX = m_intPatientInfoX;
                int intRowWidth = (int)enmRectangleInfoInPatientCaseInfo.PrintWidth2;
                int intHeight = 50;
                Font fnt = new Font("SimSun", 10.5f);
                Font fnt1 = new Font("SimSun", 12, FontStyle.Bold);

                m_objItemArr[0] = m_objGetContentFromItemArr(m_strItemArr);

                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    #region �¾�ʷ
                    PostX = m_intPatientInfoX - 30;
                    p_objGrp.DrawString("�¾�ʷ��", fnt1, Brushes.Black, PostX, p_intPosY);
                    p_objGrp.DrawString("����     �꣬����       �죬������  ��  �ࣩ��ʹ������  ��  ��  �أ���Ѫ�飨��  �ޣ�", fnt, Brushes.Black, PostX  + 70, p_intPosY);

                    if (m_objItemArr[0] != null && m_objItemArr[0][0] != null)
                        p_objGrp.DrawString(m_objItemArr[0][0].m_strItemContent,fnt,Brushes.Black,PostX + 110,p_intPosY);

                    if (m_objItemArr[0] != null && m_objItemArr[0][1] != null)
                        p_objGrp.DrawString(m_objItemArr[0][1].m_strItemContent,fnt,Brushes.Black,PostX+198,p_intPosY);

                    if (m_objItemArr[0] != null && m_objItemArr[0][2] != null && m_objItemArr[0][2].m_strItemContent == "True")
                        p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 305, p_intPosY );

                    if (m_objItemArr[0] != null && m_objItemArr[0][3] != null && m_objItemArr[0][3].m_strItemContent == "True")
                        p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 335, p_intPosY );

                    if (m_objItemArr[0] != null && m_objItemArr[0][4] != null && m_objItemArr[0][4].m_strItemContent == "True")
                        p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 365, p_intPosY );

                    if (m_objItemArr[0] != null && m_objItemArr[0][5] != null && m_objItemArr[0][5].m_strItemContent == "True")
                        p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 450, p_intPosY );

                    if (m_objItemArr[0] != null && m_objItemArr[0][6] != null && m_objItemArr[0][6].m_strItemContent == "True")
                        p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 480, p_intPosY );

                    if (m_objItemArr[0] != null && m_objItemArr[0][7] != null && m_objItemArr[0][7].m_strItemContent == "True")
                        p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 510, p_intPosY );

                    if (m_objItemArr[0] != null && m_objItemArr[0][8] != null && m_objItemArr[0][8].m_strItemContent == "True")
                        p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 540, p_intPosY );

                    if (m_objItemArr[0] != null && m_objItemArr[0][9] != null && m_objItemArr[0][9].m_strItemContent == "True")
                        p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 630, p_intPosY );

                    if (m_objItemArr[0] != null && m_objItemArr[0][10] != null && m_objItemArr[0][10].m_strItemContent == "True")
                        p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 660, p_intPosY );

                    #endregion

                    m_blnIsFirstPrint = false;

                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine(intRowWidth - 140, 185, p_intPosY, p_objGrp);
                    p_intPosY += 35;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_blnHaveMoreLine = true;
                }
                else
                {
                    m_blnHaveMoreLine = false;
                    p_intPosY += 15;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }


        }

        /// <summary>
        /// ��������ȥ�����˼���ʷ    
        /// </summary>
        private class clsPrinDetail3 : clsIMR_PrintLineBase
        {
            private bool m_blnIsFirstPrint = true;
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private clsInpatMedRec_Item[][] m_objItemArr = new clsInpatMedRec_Item[4][];
            private int m_intPart;
            private string[] m_strItemArr1 = {"����ʷ>>�����������ڵ�","����ʷ>>�Ⱥ�>>��","����ʷ>>�Ⱥ�>>��","����ʷ>>�Ⱥ�>>�����Ⱥ�"};
												 
            private string[] m_strItemArr2 = {"��ȥʷ>>����ʷ","��ȥʷ>>��Ѫʷ","��ȥʷ>>����ʷ","��ȥʷ>>���ಡ",
												 "��ȥʷ>>��Ѫѹ","��ȥʷ>>�β�","��ȥʷ>>����","��ȥʷ>>����",
												 "��ȥʷ>>ѪҺ��","��ȥʷ>>��˲�","��ȥʷ>>�ǹؽڲ�","��ȥʷ>>������","��ȥʷ>>(��˵��������)"};

            private string[] m_strItemArr3 =  {"����ʷ>>�������","����ʷ>>��ż����","����ʷ>>��ż����",
                                                 "����ʷ>>��ż����","����ʷ>>��żְҵ","����ʷ>>��ż�Ⱥ�",
                                                 "����ʷ>>�������","����ʷ>>�������"};

            private string[] m_strItemArr4 = { "����ʷ>>��Ѫѹ","����ʷ>>��˲�","����ʷ>>����",
                                                 "����ʷ>>����","����ʷ>>��̥","����ʷ>>�Ŵ���ʷ","����ʷ>>����"};

            private void m_mthPrintData(System.Drawing.Graphics p_objGrp, int p_intX1, int p_intY1, int p_intX2, int p_intY2, System.Drawing.Font p_fnt, int p_intArryPost1, int p_intArryPost2, int p_intType)
            {
                if (m_objItemArr[p_intArryPost1] != null && m_objItemArr[p_intArryPost1][p_intArryPost2] != null)
                {
                    if (p_intType == 0)
                        p_objGrp.DrawString(m_objItemArr[p_intArryPost1][p_intArryPost2].m_strItemContent, p_fnt, Brushes.Black, new RectangleF(p_intX1, p_intY1, p_intX2, p_intY2));
                    else if (p_intType == 1)
                        p_objGrp.DrawString(Convert.ToDateTime(m_objItemArr[p_intArryPost1][p_intArryPost2].m_strItemContent).ToString(), p_fnt, Brushes.Black, new RectangleF(p_intX1, p_intY1, p_intX2, p_intY2));
                    else if (p_intType == 2)
                        p_objGrp.DrawString(Convert.ToDateTime(m_objItemArr[p_intArryPost1][p_intArryPost2].m_strItemContent).ToString("yyyy��MM��dd��"), p_fnt, Brushes.Black, new RectangleF(p_intX1, p_intY1, p_intX2, p_intY2));
                }
            }

            public clsPrinDetail3(int p_intPart)
            {
                m_intPart = p_intPart;
            }
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                int PostX = m_intPatientInfoX - 30;
                int intRowWidth = 770;
                int intHeight;
                int intPostY1;
                int intPostY2;
                int intPostY3;
                int intPostY4;

                Font fnt = new Font("SimSun", 10.5f);
                Font fnt1 = new Font("SimSun", 12, FontStyle.Bold);
                Font fnt2 = new Font("SimSun", 7);

                m_objItemArr[0] = m_objGetContentFromItemArr(m_strItemArr1);
                m_objItemArr[1] = m_objGetContentFromItemArr(m_strItemArr2);
                m_objItemArr[2] = m_objGetContentFromItemArr(m_strItemArr3);
                m_objItemArr[3] = m_objGetContentFromItemArr(m_strItemArr4);


                if (m_blnIsFirstPrint)
                {
                    #region
                    if (m_intPart == 1)
                    {
                        intHeight = 20;
                        #region ��ȥʷ
                        p_objGrp.DrawString("��ȥʷ��", fnt1, Brushes.Black, PostX, p_intPosY);
                        p_objGrp.DrawString("����ʷ����Ѫʷ������ʷ�����ಡ����Ѫѹ���β������������򲡡�ѪҺ������˲����ǹؽڲ�", fnt, Brushes.Black, PostX + 70, p_intPosY);
                        if (m_objItemArr[1] != null)
                        {
                            if (m_objItemArr[1][0] != null)
                                if (m_objItemArr[1][0].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 90, p_intPosY);
                            if (m_objItemArr[1][1] != null)
                                if (m_objItemArr[1][1].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 143, p_intPosY);
                            if (m_objItemArr[1][2] != null)
                                if (m_objItemArr[1][2].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 205, p_intPosY);
                            if (m_objItemArr[1][3] != null)
                                if (m_objItemArr[1][3].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 266, p_intPosY);
                            if (m_objItemArr[1][4] != null)
                                if (m_objItemArr[1][4].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 320, p_intPosY);
                            if (m_objItemArr[1][5] != null)
                                if (m_objItemArr[1][5].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 370, p_intPosY);
                            if (m_objItemArr[1][6] != null)
                                if (m_objItemArr[1][6].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 415, p_intPosY);
                            if (m_objItemArr[1][7] != null)
                                if (m_objItemArr[1][7].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 470, p_intPosY);
                            if (m_objItemArr[1][8] != null)
                                if (m_objItemArr[1][8].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 530, p_intPosY);
                            if (m_objItemArr[1][9] != null)
                                if (m_objItemArr[1][9].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 580, p_intPosY);
                            if (m_objItemArr[1][10] != null)
                                if (m_objItemArr[1][10].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 650, p_intPosY);
                        }
                        //����
                        p_intPosY += intHeight;
                        PostX +=  70;

                        RectangleF m_rtgF = new RectangleF(PostX + 55, p_intPosY, intRowWidth, intHeight);
                        StringFormat m_sf = new StringFormat();
                        m_sf.Alignment = StringAlignment.Near;
                        p_objGrp.DrawString("��������", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1] != null && m_objItemArr[1][11] != null && !string.IsNullOrEmpty(m_objItemArr[1][11].m_strItemContent))
                        {
                            SizeF m_sizeF = p_objGrp.MeasureString(m_objItemArr[1][11].m_strItemContent, fnt, intRowWidth - PostX - 55, m_sf);
                            m_rtgF.X = PostX + 55;
                            m_rtgF.Y = p_intPosY;
                            m_rtgF.Width = intRowWidth - PostX - 55;
                            m_rtgF.Height = m_sizeF.Height;
                            p_objGrp.DrawString(m_objItemArr[1][11].m_strItemContent, fnt, Brushes.Black, m_rtgF, m_sf);
                            p_intPosY += Convert.ToInt32(m_sizeF.Height);
                        }
                        else
                        {

                            //����	
                            p_intPosY += intHeight;
                        }
                        p_objGrp.DrawString("��˵�������⣺", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1] != null && m_objItemArr[1][12] != null && !string.IsNullOrEmpty(m_objItemArr[1][12].m_strItemContent))
                        {
                            SizeF m_sizeF = p_objGrp.MeasureString(m_objItemArr[1][12].m_strItemContent, fnt, intRowWidth - PostX - 100, m_sf);
                            m_rtgF.X = PostX + 100;
                            m_rtgF.Y = p_intPosY;
                            m_rtgF.Width = intRowWidth - PostX - 100;
                            m_rtgF.Height = m_sizeF.Height;
                            p_objGrp.DrawString(m_objItemArr[1][12].m_strItemContent, fnt, Brushes.Black, m_rtgF, m_sf);
                            p_intPosY += Convert.ToInt32(m_sizeF.Height);
                        }
                        else
                        {
                            p_intPosY += intHeight + 5;
                        }
                        #endregion

                        #region ����ʷ
                        //p_intPosY += intHeight + 5;
                        PostX = m_intPatientInfoX - 30;
                        p_objGrp.DrawString("����ʷ��", fnt1, Brushes.Black, PostX, p_intPosY);
                        p_objGrp.DrawString("�����������ڵأ�                                         �Ⱥã� �̡��ƣ�������", fnt, Brushes.Black, PostX + 70, p_intPosY);
                        if (m_objItemArr[0] != null)
                        {
                            if (m_objItemArr[0][0] != null)
                                p_objGrp.DrawString(m_objItemArr[0][0].m_strItemContent, fnt, Brushes.Black, PostX + 185, p_intPosY);
                            if (m_objItemArr[0][1] != null && m_objItemArr[1][1] != null)
                            {
                                if (m_objItemArr[1][1].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 540, p_intPosY);
                            }
                                
                            if (m_objItemArr[0][2] != null)
                                if (m_objItemArr[0][2].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 565, p_intPosY);
                        }
                        if (m_objItemArr[0] != null && m_objItemArr[0][3] != null)
                            p_objGrp.DrawString(m_objItemArr[0][3].m_strItemContent, fnt, Brushes.Black, PostX + 635, p_intPosY);
                        #endregion
                        #region ����ʷ
                        //����
                        p_intPosY += intHeight +5;
                        PostX = m_intPatientInfoX - 30;
                        p_objGrp.DrawString("����ʷ��", fnt1, Brushes.Black, PostX, p_intPosY);
                        p_objGrp.DrawString("������䣺   �꣬��ż������           ���䣺    �꣬���᣺                  ְҵ��", fnt, Brushes.Black, PostX + 70, p_intPosY);
                        if (m_objItemArr[2] != null)
                        {
                            if (m_objItemArr[2][0] != null)
                                p_objGrp.DrawString(m_objItemArr[2][0].m_strItemContent, fnt, Brushes.Black, PostX + 138, p_intPosY);
                            if (m_objItemArr[2][1] != null)
                                p_objGrp.DrawString(m_objItemArr[2][1].m_strItemContent, fnt, Brushes.Black, PostX + 258, p_intPosY);
                            if (m_objItemArr[2][2] != null)
                                p_objGrp.DrawString(m_objItemArr[2][2].m_strItemContent, fnt, Brushes.Black, PostX + 385, p_intPosY);
                            if (m_objItemArr[2][3] != null)
                                p_objGrp.DrawString(m_objItemArr[2][3].m_strItemContent, fnt, Brushes.Black, PostX + 488, p_intPosY);
                            if (m_objItemArr[2][4] != null)
                                p_objGrp.DrawString(m_objItemArr[2][4].m_strItemContent, fnt, Brushes.Black, PostX + 662, p_intPosY);
                        }
                        //����	
                        p_intPosY += intHeight;
                        p_objGrp.DrawString("�Ⱥã�                        ����״����                         ����״����", fnt, Brushes.Black, PostX + 70, p_intPosY);
                        if (m_objItemArr[3] != null)
                        {
                            if (m_objItemArr[2][5] != null)
                                p_objGrp.DrawString(m_objItemArr[2][5].m_strItemContent, fnt, Brushes.Black, PostX + 106, p_intPosY);
                            if (m_objItemArr[2][6] != null)
                                p_objGrp.DrawString(m_objItemArr[2][6].m_strItemContent, fnt, Brushes.Black, PostX + 356, p_intPosY);
                            if (m_objItemArr[2][7] != null)
                                p_objGrp.DrawString(m_objItemArr[2][7].m_strItemContent, fnt, Brushes.Black, PostX + 608, p_intPosY);
                        }
                        //����	
                        p_intPosY += intHeight;
                        p_objGrp.DrawString("������", fnt, Brushes.Black, PostX + 70, p_intPosY);
                        if (m_objItemArr[2] != null && m_objItemArr[2][7] != null)
                            p_objGrp.DrawString(m_objItemArr[2][7].m_strItemContent, fnt, Brushes.Black, PostX + 106, p_intPosY);
                        #endregion
                        #region ����ʷ
                        //����	
                        p_intPosY += intHeight;
                        PostX = m_intPatientInfoX - 30;
                        p_objGrp.DrawString("����ʷ��", fnt1, Brushes.Black, PostX, p_intPosY);
                        p_objGrp.DrawString("��Ѫѹ����˲������򲡡����񲡡���̥���Ŵ���ʷ��������", fnt, Brushes.Black, PostX + 70, p_intPosY);
                        if (m_objItemArr[3] != null)
                        {
                            if (m_objItemArr[3][0] != null)
                                if (m_objItemArr[3][0].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 90, p_intPosY);
                            if (m_objItemArr[3][1] != null)
                                if (m_objItemArr[3][1].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 145, p_intPosY);
                            if (m_objItemArr[3][2] != null)
                                if (m_objItemArr[3][2].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 205, p_intPosY);
                            if (m_objItemArr[3][3] != null)
                                if (m_objItemArr[3][3].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 257, p_intPosY);
                            if (m_objItemArr[3][4] != null)
                                if (m_objItemArr[3][4].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 317, p_intPosY);
                            if (m_objItemArr[3][5] != null)
                                if (m_objItemArr[3][5].m_strItemContent == "True")
                                    p_objGrp.DrawString("��", fnt1, Brushes.Black, PostX + 370, p_intPosY);
                            if (m_objItemArr[3][6] != null)
                                p_objGrp.DrawString(m_objItemArr[3][6].m_strItemContent, fnt, Brushes.Black, PostX +462, p_intPosY);
                        }

                        #endregion
                    }
                    #endregion
                    m_blnIsFirstPrint = false;
                }


                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_blnHaveMoreLine = true;
                }
                else
                {
                    m_blnHaveMoreLine = false;
                    p_intPosY += 2000;//��ҳ
                }
            }
                

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }

        }

        /// <summary>
        /// ��죬���Ƽ�飬����������
        /// </summary>
        private class clsPrinDetail4 : clsIMR_PrintLineBase
		{
            private bool m_blnIsFirstPrint = true;
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsInpatMedRec_Item[][] m_objItemArr = new clsInpatMedRec_Item[3][];
            private string[] m_strItemArr1 = {"���>>����","���>>����","���>>������","���>>��������",
												 "���>>����","���>>����ѹ","���>>����ѹ","���>>���","���>>����","���>>����",
												 "���>>Ӫ��","���>>��־","���>>��������","���>>Ƥ��","���>>��ǳ�ܰͽ�",
												 "���>>ͷ��","���>>���","���>>����","���>>�ز�","���>>�鷿","���>>����",
												 "���>>����","���>>����","���>>Ƣ��","���>>����","���>>����","���>>ϥ����",
												 "���>>��֫>>ˮ��","���>>��֫>>��������","���>>����>>ˮ��","���>>����>>��������",
												 "���>>����>>����","���>>����"};

            private string[] m_strItemArr2 = {"���Ƽ��>>����","���Ƽ��>>��Χ","���Ƽ��>>̥��λ","���Ƽ��>>̥����+","���Ƽ��>>̥��������",
                                                 "���Ƽ��>>̥����������","���Ƽ��>>̥��¶","���Ƽ��>>�ν�>>δ",
											     "���Ƽ��>>�ν�>>����","���Ƽ��>>�ν�>>��","���Ƽ��>>�����",
                                                 "���Ƽ��>>����>>ǿ","���Ƽ��>>����>>��","���Ƽ��>>����>>��","���Ƽ��>>(�ء���)��>>����Bishop����",
												 "���Ƽ��>>(�ء���)��>>���ڿ���","���Ƽ��>>(�ء���)��>>��¶�ߵ�","���Ƽ��>>(�ء���)��>>������ˮ>>��",
                                                 "���Ƽ��>>(�ء���)��>>������ˮ>>��","���Ƽ��>>(�ء���)��>>̥Ĥ����>>��","���Ƽ��>>(�ء���)��>>̥Ĥ����>>δ",
												 "���Ƽ��>>(�ء���)��>>��ˮ��״>>ɫ","���Ƽ��>>(�ء���)��>>��ˮ��״>>PH",
												 "���Ƽ��>>(�ء���)��>>��ˮ��״>>����","���Ƽ��>>̥����������","���Ƽ��>>���������>>��ǰ�ϼ��侶","���Ƽ��>>���������>>���ռ侶",
												 "���Ƽ��>>���������>>�����⾶","���Ƽ��>>���ǽ�ڼ侶" };

            private string[] m_strItemArr3 = {"������>>Ѫɫ��",
												 "������>>��Ѫ��","������>>��Ѫ��","������>>ѪС��","������>>Hct",
												 "������>>Ѫ��","������>>�򵰰�","������>>B��",
                                                 "������>>�ι�","������>>�Ҹ����԰�","������>>����"};//,"���","����ҽʦ","סԺҽʦ","����","������","����������ҽʦ","������סԺҽʦ","����������"};

            private void m_mthPrintData(System.Drawing.Graphics p_objGrp, int p_intX1, int p_intY1, System.Drawing.Font p_fnt, int p_intArryPost1, int p_intArryPost2, int p_intType)
            {
                if (m_objItemArr[p_intArryPost1] != null && m_objItemArr[p_intArryPost1][p_intArryPost2] != null)
                {
                    if (p_intType == 0)
                        p_objGrp.DrawString(m_objItemArr[p_intArryPost1][p_intArryPost2].m_strItemContent, p_fnt, Brushes.Black, new RectangleF(p_intX1, p_intY1, 200, 200));
                    else if (p_intType == 1)
                        p_objGrp.DrawString(Convert.ToDateTime(m_objItemArr[p_intArryPost1][p_intArryPost2].m_strItemContent).ToString(), p_fnt, Brushes.Black, new RectangleF(p_intX1, p_intY1, 200, 200));
                    else if (p_intType == 2)
                        p_objGrp.DrawString(Convert.ToDateTime(m_objItemArr[p_intArryPost1][p_intArryPost2].m_strItemContent).ToString("yyyy��MM��dd��"), p_fnt, Brushes.Black, p_intX1, p_intY1);
                    else if(p_intType == 3)
                        p_objGrp.DrawString(m_objItemArr[p_intArryPost1][p_intArryPost2].m_strItemContent, p_fnt, Brushes.Black, p_intX1, p_intY1);
                }
            }

			public clsPrinDetail4()
			{

			}
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                int PostX = m_intPatientInfoX - 30;
                int intRowWidth = 770;
                int intHeight;
                int intPostY1;
                int intPostY2;
                int intPostY3;
                int intPostY4;

                Font fnt = new Font("SimSun", 10.5f);
                Font fnt1 = new Font("SimSun", 12, FontStyle.Bold);
                Font fnt2 = new Font("SimSun", 7);

                m_objItemArr[0] = m_objGetContentFromItemArr(m_strItemArr1);
                m_objItemArr[1] = m_objGetContentFromItemArr(m_strItemArr2);
                m_objItemArr[2] = m_objGetContentFromItemArr(m_strItemArr3);
                if (m_blnIsFirstPrint)
                {

                    #region ���
                    if (m_objItemArr[0] != null)
                    {
                        #region ���
                        #region ��һ��
                        intHeight = 20;
                        PostX = m_intPatientInfoX - 20;
                        p_objGrp.DrawString("��    ��", fnt1, Brushes.Black, PostX + intRowWidth / 2 - 50, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString("���£�", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 0, 3);
                        PostX += 90;
                        p_objGrp.DrawString("��", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 25;
                        p_objGrp.DrawString("������", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 1, 3);
                        PostX += 60;
                        p_objGrp.DrawString("��/��(", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 45;
                        p_objGrp.DrawString("����", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[0][2] != null)
                            if (m_objItemArr[0][2].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX, p_intPosY + 2, 40, intHeight));
                        PostX += 30;
                        p_objGrp.DrawString("����)", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[0][3] != null)
                            if (m_objItemArr[0][3].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX + 3, p_intPosY + 2, 40, intHeight));
                        PostX += 50;
                        p_objGrp.DrawString("������", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 4, 3);
                        PostX += 60;
                        p_objGrp.DrawString("��/��", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 50;
                        p_objGrp.DrawString("Ѫѹ��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 5, 3);
                        PostX += 60;
                        p_objGrp.DrawString("/", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 10, p_intPosY, fnt, 0, 6, 3);
                        PostX += 40;
                        p_objGrp.DrawString("(mmHg)", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 60;
                        p_objGrp.DrawString("��ߣ�", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 7, 3);
                        PostX += 60;
                        p_objGrp.DrawString("cm", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 30;
                        p_objGrp.DrawString("���أ�", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 8, 3);
                        PostX += 60;
                        p_objGrp.DrawString("kg", fnt, Brushes.Black, PostX, p_intPosY);
                        #endregion
                        #region �ڶ���
                        p_intPosY += intHeight;
                        PostX = m_intPatientInfoX - 20;
                        p_objGrp.DrawString("������", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 9, 3);
                        PostX += 150;
                        p_objGrp.DrawString("Ӫ����", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 10, 3);
                        PostX += 150;
                        p_objGrp.DrawString("��־��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 11, 3);
                        PostX += 150;
                        p_objGrp.DrawString("��������", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 80, p_intPosY, fnt, 0, 12, 3);
                        PostX += 150;
                        p_objGrp.DrawString("Ƥ����", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 13, 3);
                        #endregion
                        #region ������
                        p_intPosY += intHeight;
                        PostX = m_intPatientInfoX - 20;
                        p_objGrp.DrawString("��ǳ�ܰͽ᣺", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 80, p_intPosY, fnt, 0, 14, 3);
                        PostX += 150;
                        p_objGrp.DrawString("ͷ����", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 15, 3);
                        PostX += 150;
                        p_objGrp.DrawString("��٣�", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 16, 3);
                        PostX += 150;
                        p_objGrp.DrawString("������", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 17, 3);
                        PostX += 150;
                        p_objGrp.DrawString("�ز���", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 18, 3);
                        #endregion
                        #region ������
                        p_intPosY += intHeight;
                        PostX = m_intPatientInfoX - 20;
                        p_objGrp.DrawString("�鷿��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 19, 3);
                        PostX += 150;
                        p_objGrp.DrawString("���ࣺ", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 20, 3);
                        PostX += 300;
                        p_objGrp.DrawString("���ࣺ", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 21, 3);
                     
                        #endregion
                        #region ������
                        p_intPosY += intHeight;
                        PostX = m_intPatientInfoX - 20;
                        p_objGrp.DrawString("���ࣺ", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 22, 3);
                        PostX += 300;
                        p_objGrp.DrawString("Ƣ�ࣺ", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 23, 3);

                        #endregion
                        #region ������
                        p_intPosY += intHeight;
                        PostX = m_intPatientInfoX - 20;
                        p_objGrp.DrawString("���ࣺ", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 24, 3);
                        PostX += 300;
                        p_objGrp.DrawString("������", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 0, 25, 3);
                        PostX += 100;
                        p_objGrp.DrawString("ϥ���䣺", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 50, p_intPosY, fnt, 0, 26, 3);
                        PostX += 100;
                        p_objGrp.DrawString("��֫��ˮ�ף�", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 80, p_intPosY, fnt, 0, 27, 3);
                        
                        #endregion
                        #region ������
                        p_intPosY += intHeight;
                        PostX = m_intPatientInfoX - 20;
                        p_objGrp.DrawString("�������ţ�", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 65, p_intPosY, fnt, 0, 28, 3);
                        PostX += 140;
                        p_objGrp.DrawString("������", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 60;
                        p_objGrp.DrawString("ˮ�ס�", fnt, Brushes.Black, PostX, p_intPosY);

                        if (m_objItemArr[0][29] != null)
                            if (m_objItemArr[0][29].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX + 6, p_intPosY + 2, 40, intHeight));

                        PostX += 40;
                        p_objGrp.DrawString("�������š�", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[0][30] != null)
                            if (m_objItemArr[0][30].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX + 25, p_intPosY + 2, 40, intHeight));

                        PostX += 70;
                        p_objGrp.DrawString("����", fnt, Brushes.Black, PostX + 5, p_intPosY);
                        if (m_objItemArr[0][31] != null)
                            if (m_objItemArr[0][31].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX + 10, p_intPosY + 2, 40, intHeight));
                        PostX += 70;
                        p_objGrp.DrawString("������", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 45, p_intPosY, fnt, 0, 32, 3);
                        #endregion
                        #endregion
                    }
                    #endregion

                    #region ���Ƽ��
                    if (m_objItemArr[1] != null)
                    {
                        #region ��һ��
                        intHeight = 20;
                        p_intPosY += 20;
                        PostX = m_intPatientInfoX - 20;
                        p_objGrp.DrawString("���Ƽ��", fnt1, Brushes.Black, PostX + intRowWidth / 2 - 50, p_intPosY);
                        p_intPosY += 26;
                        p_objGrp.DrawString("���ߣ�", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 1, 0, 3);
                        PostX += 60;
                        p_objGrp.DrawString("cm", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 30;
                        p_objGrp.DrawString("��Χ��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 1, 1, 3);
                        PostX += 60;
                        p_objGrp.DrawString("cm", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 30;
                        p_objGrp.DrawString("̥��λ��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 50, p_intPosY, fnt, 1, 2, 3);
                        PostX += 130;
                        p_objGrp.DrawString("̥��������", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 67, p_intPosY, fnt, 1, 3, 3);
                        PostX += 100;
                        p_objGrp.DrawString("��/��", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 60;
                        p_objGrp.DrawString("����", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][4] != null)
                            if (m_objItemArr[1][4].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX + 5, p_intPosY + 2, 40, intHeight));
                        PostX += 40;
                        p_objGrp.DrawString("������", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][5] != null)
                            if (m_objItemArr[1][5].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX + 15, p_intPosY + 2, 40, intHeight));
                        #endregion
                        #region �ڶ���
                        PostX = m_intPatientInfoX - 20;
                        p_intPosY += intHeight;
                        p_objGrp.DrawString("̥��¶��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 50, p_intPosY, fnt, 1, 6, 3);
                        PostX += 90;
                        p_objGrp.DrawString("�νӣ�", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 40;
                        p_objGrp.DrawString("δ��", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][7] != null)
                            if (m_objItemArr[1][7].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX, p_intPosY + 2, 40, intHeight));

                        PostX += 30;
                        p_objGrp.DrawString("���֡�", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][8] != null)
                            if (m_objItemArr[1][8].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX + 3, p_intPosY + 2, 40, intHeight));

                        PostX += 40;
                        p_objGrp.DrawString("��", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][9] != null)
                            if (m_objItemArr[1][9].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX, p_intPosY + 2, 40, intHeight));
                        PostX += 40;
                        p_objGrp.DrawString("�������", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 55, p_intPosY, fnt, 1, 10, 3);
                        PostX += 90;
                        p_objGrp.DrawString("������", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 40;
                        p_objGrp.DrawString("ǿ��", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][11] != null)
                            if (m_objItemArr[1][11].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX, p_intPosY + 2, 40, intHeight));
                        PostX += 20;
                        p_objGrp.DrawString("�С�", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][12] != null)
                            if (m_objItemArr[1][12].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX, p_intPosY + 2, 40, intHeight));
                        PostX += 20;
                        p_objGrp.DrawString("��", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][13] != null)
                            if (m_objItemArr[1][13].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX, p_intPosY + 2, 40, intHeight));
                        PostX += 40;
                        p_objGrp.DrawString("(�ء���)�죺����Bishop���֣�", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 110;
                        m_mthPrintData(p_objGrp, PostX + 90, p_intPosY, fnt, 1, 14, 3);
                        PostX += 125;
                        p_objGrp.DrawString("��", fnt, Brushes.Black, PostX, p_intPosY);
                        #endregion
                        #region ������
                        p_intPosY += intHeight;
                        PostX = m_intPatientInfoX - 20; ;
                        p_objGrp.DrawString("���ڿ��ţ�", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 65, p_intPosY, fnt, 1, 15, 3);
                        PostX += 90;
                        p_objGrp.DrawString("cm", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 40;
                        p_objGrp.DrawString("��¶�ߵͣ�", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 65;
                        p_objGrp.DrawString("S=", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 23, p_intPosY, fnt, 1, 16, 3);
                        PostX += 70;
                        p_objGrp.DrawString("������ˮ��", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 75;
                        p_objGrp.DrawString("��", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][17] != null)
                            if (m_objItemArr[1][17].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX, p_intPosY + 2, 40, intHeight));
                        PostX += 20;
                        p_objGrp.DrawString("��", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][18] != null)
                            if (m_objItemArr[1][18].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX, p_intPosY + 2, 40, intHeight));
                        PostX += 40;
                        p_objGrp.DrawString("̥Ĥ���ƣ�", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 73;
                        p_objGrp.DrawString("��", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][19] != null)
                            if (m_objItemArr[1][19].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX, p_intPosY + 2, 40, intHeight));
                        PostX += 30;
                        p_objGrp.DrawString("δ", fnt, Brushes.Black, PostX, p_intPosY);
                        if (m_objItemArr[1][20] != null)
                            if (m_objItemArr[1][20].m_strItemContent == "True")
                                p_objGrp.DrawString("��", fnt1, Brushes.Black, new RectangleF(PostX, p_intPosY + 2, 40, intHeight));
                        PostX += 40;
                        p_objGrp.DrawString("��ˮ��״��ɫ��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 98, p_intPosY, fnt, 1, 21, 3);
                        #endregion
                        #region ������
                        p_intPosY += intHeight;
                        PostX = m_intPatientInfoX - 20;
                        p_objGrp.DrawString("PH��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 25, p_intPosY, fnt, 1, 22, 3);
                        PostX += 60;
                        p_objGrp.DrawString("���ʣ�", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 1, 23, 3);
                        PostX += 90;
                        p_objGrp.DrawString("̥�����ع��ƣ�", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 95, p_intPosY, fnt, 1, 24, 3);
                        PostX += 130;
                        p_objGrp.DrawString("kg", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 40;
                        p_objGrp.DrawString("������������ǰ�ϼ��侶��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 180, p_intPosY, fnt, 1, 25, 3);
                        PostX += 216;
                        p_objGrp.DrawString("cm", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 40;
                        p_objGrp.DrawString("���ռ侶��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 70, p_intPosY, fnt, 1, 26, 3);
                        PostX += 100;
                        p_objGrp.DrawString("cm", fnt, Brushes.Black, PostX, p_intPosY);
                        
                        #endregion
                        #region ������
                        p_intPosY += intHeight;
                        PostX = m_intPatientInfoX - 20;
                        p_objGrp.DrawString("�����⾶��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 70, p_intPosY, fnt, 1, 27, 3);
                        PostX += 90;
                        p_objGrp.DrawString("cm", fnt, Brushes.Black, PostX, p_intPosY);
                        PostX += 40;
                        p_objGrp.DrawString("���ǽ�ڼ侶��", fnt, Brushes.Black, PostX, p_intPosY);
                        m_mthPrintData(p_objGrp, PostX + 100, p_intPosY, fnt, 1, 28, 3);
                        PostX += 120;
                        p_objGrp.DrawString("cm", fnt, Brushes.Black, PostX, p_intPosY);
                        
                        #endregion

                    #endregion

                        #region �����
                        if (m_objItemArr[2] != null)
                        {
                            #region ��һ��
                            p_intPosY += 35;
                            PostX = m_intPatientInfoX - 20;
                            p_objGrp.DrawString("�����", fnt1, Brushes.Black, PostX + intRowWidth / 2 - 50, p_intPosY);
                            p_intPosY += 25;
                            p_objGrp.DrawString("Ѫɫ�أ�", fnt, Brushes.Black, PostX, p_intPosY);
                            m_mthPrintData(p_objGrp, PostX + 50, p_intPosY, fnt, 2, 0, 3);
                            PostX += 80;
                            p_objGrp.DrawString("g/L", fnt, Brushes.Black, PostX, p_intPosY);
                            PostX += 40;
                            p_objGrp.DrawString("��Ѫ��", fnt, Brushes.Black, PostX, p_intPosY);
                            m_mthPrintData(p_objGrp, PostX + 50, p_intPosY, fnt, 2, 1, 3);
                            PostX += 80;
                            p_objGrp.DrawString("��10", fnt, Brushes.Black, PostX, p_intPosY);
                            PostX += 27;
                            p_objGrp.DrawString("12", fnt2, Brushes.Black, PostX, p_intPosY - 3);
                            PostX += 10;
                            p_objGrp.DrawString("/L", fnt, Brushes.Black, PostX, p_intPosY);
                            PostX += 40;
                            p_objGrp.DrawString("��Ѫ��", fnt, Brushes.Black, PostX, p_intPosY);
                            m_mthPrintData(p_objGrp, PostX + 50, p_intPosY, fnt, 2, 2, 3);
                            PostX += 80;
                            p_objGrp.DrawString("��10", fnt, Brushes.Black, PostX, p_intPosY);
                            PostX += 29;
                            p_objGrp.DrawString("9", fnt2, Brushes.Black, PostX, p_intPosY - 3);
                            PostX += 10;
                            p_objGrp.DrawString("/L", fnt, Brushes.Black, PostX, p_intPosY);
                            PostX += 40;
                            p_objGrp.DrawString("ѪС�壺", fnt, Brushes.Black, PostX, p_intPosY);
                            m_mthPrintData(p_objGrp, PostX + 50, p_intPosY, fnt, 2, 3, 3);
                            PostX += 80;
                            p_objGrp.DrawString("��10", fnt, Brushes.Black, PostX, p_intPosY);
                            PostX += 28;
                            p_objGrp.DrawString("9", fnt2, Brushes.Black, PostX, p_intPosY - 3);
                            PostX += 10;
                            p_objGrp.DrawString("/L   Hct��", fnt, Brushes.Black, PostX, p_intPosY);
                            m_mthPrintData(p_objGrp, PostX + 73, p_intPosY, fnt, 2, 4, 3);
                            p_objGrp.DrawString("%", fnt, Brushes.Black, PostX + 113, p_intPosY);
                            #endregion
                            #region �ڶ���
                            p_intPosY += intHeight;
                            PostX = m_intPatientInfoX - 20;
                            p_objGrp.DrawString("Ѫ�ͣ�", fnt, Brushes.Black, PostX, p_intPosY);
                            m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 2, 5, 3);
                            PostX += 100;
                            p_objGrp.DrawString("�򵰰ף�", fnt, Brushes.Black, PostX, p_intPosY);
                            m_mthPrintData(p_objGrp, PostX + 50, p_intPosY, fnt, 2, 6, 3);
                            PostX += 120;
                            p_objGrp.DrawString("B����", fnt, Brushes.Black, PostX, p_intPosY);
                            m_mthPrintData(p_objGrp, PostX + 40, p_intPosY, fnt, 2, 7, 3);
                            #endregion
                            #region ������
                            p_intPosY += intHeight;
                            PostX = m_intPatientInfoX - 20;
                            p_objGrp.DrawString("", fnt, Brushes.Black, PostX, p_intPosY);
                            m_mthPrintData(p_objGrp, PostX + 56, p_intPosY, fnt, 2, 8, 3);
                          //  PostX += 10;
                            p_objGrp.DrawString("�Ҹ����԰룺", fnt, Brushes.Black, PostX, p_intPosY);
                            m_mthPrintData(p_objGrp, PostX +90, p_intPosY, fnt, 2, 9, 3);
                            PostX += 340;
                            p_objGrp.DrawString("������", fnt, Brushes.Black, PostX, p_intPosY);
                            m_mthPrintData(p_objGrp, PostX + 45, p_intPosY, fnt, 2, 10, 3);
                            p_intPosY += intHeight;
                            #endregion
                        }

                        #endregion

                        #region ���
                        //    p_intPosY += 35;
                        //    PostX = 35;



                        //    p_objGrp.DrawString("��ϣ�", fnt1, Brushes.Black, PostX, p_intPosY);
                        //    m_mthPrintData(p_objGrp, PostX + 55, p_intPosY, fnt, 2, 12, 0);
                        //    p_intPosY += 170;
                        //    p_objGrp.DrawString("סԺҽʦ��", fnt, Brushes.Black, PostX + 250, p_intPosY);
                        //    m_mthPrintData(p_objGrp, PostX + 530 , p_intPosY, fnt, 2, 13, 3);
                        //    p_intPosY += 20;
                        //    p_objGrp.DrawString("����ҽʦ��", fnt, Brushes.Black, PostX + 250, p_intPosY);
                        //    m_mthPrintData(p_objGrp, PostX + 530, p_intPosY, fnt, 2, 14, 3);
                        //    p_intPosY += 20;
                        //    p_objGrp.DrawString("��    �ڣ�", fnt, Brushes.Black, PostX + 250, p_intPosY);
                        //    m_mthPrintData(p_objGrp, PostX + 530, p_intPosY, fnt, 2, 15, 2);
                        //    intPostY2 = p_intPosY;

                        //    PostX = 35;
                        //    p_intPosY = p_intPosY + 10;
                        //    p_objGrp.DrawString("�����ϣ�", fnt1, Brushes.Black, PostX, p_intPosY);
                        //    m_mthPrintData(p_objGrp, PostX + 92, p_intPosY, fnt, 2, 16, 0);
                        //    p_intPosY += 170;
                        //    p_objGrp.DrawString("סԺҽʦ��", fnt, Brushes.Black, PostX + 250, p_intPosY);
                        //    m_mthPrintData(p_objGrp, PostX + 530, p_intPosY, fnt, 2, 17, 3);
                        //    p_intPosY += 20;
                        //    p_objGrp.DrawString("����ҽʦ��", fnt, Brushes.Black, PostX + 250, p_intPosY);
                        //    m_mthPrintData(p_objGrp, PostX + 530, p_intPosY, fnt, 2, 18, 3);
                        //    p_intPosY += 20;
                        //    p_objGrp.DrawString("��    �ڣ�", fnt, Brushes.Black, PostX + 250, p_intPosY);
                        //    m_mthPrintData(p_objGrp, PostX + 530, p_intPosY, fnt, 2, 19, 2);
                        //    intPostY2 = p_intPosY;
                        //}
                        #endregion
                    }
                    if (m_objPrintContext.m_BlnHaveNextLine())
                    {
                        m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
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
            }
			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
			}

		}

        /// <summary>
        /// ���
        /// </summary>
        private class clsPrintHomeHistroy : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strPrintText = "  ��";
            private clsInpatMedRec_Item objItemContent = null;
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_hasItems != null)
                    if (m_hasItems.Contains("���"))
                        objItemContent = m_hasItems["���"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    //if (m_blnCheckBottom(ref p_intPosY, 40, p_intPosY))
                    //{
                    //    m_blnHaveMoreLine = true;
                    //    return;
                    //}
                    p_intPosY += 20;
                    p_objGrp.DrawString("��ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //if (m_hasItemDetail != null && m_hasItemDetail.Contains("��ʷ>>����ʷ"))
                    //{
                    //    strPrintText += m_hasItemDetail["��ʷ>>����ʷ"];
                    p_intPosY += 20;
                    //}
                    //if (strPrintText != "  ��") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
                    //m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
                    //p_fntNormal.Dispose();
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);

                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

                    p_intPosY += 20;

                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
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
        }

        /// <summary>
        /// ����ҽʦ
        /// </summary>
        private class clsPrint10 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    string m_strPrint = string.Empty;
                    p_objGrp.DrawString("����ҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY+2);
                    if (m_hasItems != null)
                        if (m_hasItems.Contains("����ҽʦǩ��"))
                            m_strPrint += (m_hasItems["����ҽʦǩ��"] as clsInpatMedRec_Item).m_strItemContent;
                    //p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    if (!string.IsNullOrEmpty(m_strPrint))
                        m_strPrint = m_strPrint.Replace("������ҽʦ", "").Replace("����ҽʦ", "").Replace("��������ҽʦ", "").Replace("����ҽʦ", "").Replace("��ҽʦ", "").Replace("ҽʦ", "").Trim();
                    //p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    Image imgEmpSig = ImageSignature.GetEmpSigImage(m_strPrint);
                    if (imgEmpSig != null)
                    {
                        p_objGrp.DrawImage(imgEmpSig, m_intRecBaseX + 130, p_intPosY-2, 70, 30);
                    }
                    else
                    {
                        p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 130, p_intPosY);
                    }
                    p_intPosY += 30;
                    //string strOperations = "        ";
                    //string strAllText = "        ";
                    //string strXml = "";
                    //for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    //{
                    //    if (m_objContent.objSignerArr[i].controlName == "m_lsvhelper")
                    //        strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    //}
                    //if (strOperations != "")
                    //{
                    //    p_objGrp.DrawString("����ҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    //    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    //    m_mthMakeText(new string[] { strOperations }, new string[] { "" }, ref strAllText, ref strXml);
                    //}
                
                //else
                //{
                //    m_blnHaveMoreLine = false;
                //    return;
                //}
                //m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                m_blnIsFirstPrint = false;
                m_blnHaveMoreLine = false;
                }

                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 60, p_intPosY, p_objGrp);
                //    p_intPosY += 20;
                //}
                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_blnHaveMoreLine = true;
                //}
                //else
                //{
                //    m_blnHaveMoreLine = false;
                //}
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }
        }

        /// <summary>
        /// סԺҽʦ
        /// </summary>
        private class clsPrint11 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    string m_strPrint = string.Empty ;
                    p_objGrp.DrawString("סԺҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY+2);
                    if (m_hasItems != null)
                        if (m_hasItems.Contains("סԺҽʦǩ��"))
                            m_strPrint += (m_hasItems["סԺҽʦǩ��"] as clsInpatMedRec_Item).m_strItemContent;
                    if (!string.IsNullOrEmpty(m_strPrint))
                        m_strPrint = m_strPrint.Replace("������ҽʦ", "").Replace("����ҽʦ", "").Replace("��������ҽʦ", "").Replace("����ҽʦ", "").Replace("��ҽʦ","").Replace("ҽʦ","").Trim();
                    //p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    Image imgEmpSig = ImageSignature.GetEmpSigImage(m_strPrint);
                    if (imgEmpSig != null)
                    {
                        p_objGrp.DrawImage(imgEmpSig, m_intRecBaseX + 130, p_intPosY-2, 70, 30);
                    }
                    else
                    {
                        p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 130, p_intPosY);
                    }
                    p_intPosY += 30;
                    //string strOperations = "        ";
                    //string strAllText = "        ";
                    //string strXml = "";
                    //for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    //{
                    //    if (m_objContent.objSignerArr[i].controlName == "m_lsvOperation")
                    //        strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    //}
                    //if (strOperations != "")
                    //{
                    //    p_objGrp.DrawString("סԺҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    //    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    //    m_mthMakeText(new string[] { strOperations }, new string[] { "" }, ref strAllText, ref strXml);
                    //}
                    //else
                    //{
                    //    m_blnHaveMoreLine = false;
                    //    return;
                    //}
                    //m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_blnIsFirstPrint = false;
                    m_blnHaveMoreLine = false;
                }

                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 60, p_intPosY, p_objGrp);
                //    p_intPosY += 20;
                //}
                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_blnHaveMoreLine = true;
                //}
                //else
                //{
                //    m_blnHaveMoreLine = false;
                //}
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }
        }

        /// <summary>
        /// ����

        /// </summary>
        private class clsPrintDate : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strPrintText = "  ��";
            private clsInpatMedRec_Item objItemContent = null;
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                string m_strPrint = "���ڣ�";
                if (m_hasItems != null)
                    if (m_hasItems.Contains("����"))
                        m_strPrint += (m_hasItems["����"] as clsInpatMedRec_Item).m_strItemContent;
                p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                p_intPosY += 20;

                m_blnIsFirstPrint = false;
                m_blnHaveMoreLine = false;

                //if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                //{
                //    m_blnHaveMoreLine = false;
                //    return;
                //}
                //if (m_blnIsFirstPrint)
                //{
                //    //if (m_blnCheckBottom(ref p_intPosY, 40, p_intPosY))
                //    //{
                //    //    m_blnHaveMoreLine = true;
                //    //    return;
                //    //}

                //    p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                //    //if (m_hasItemDetail != null && m_hasItemDetail.Contains("��ʷ>>����ʷ"))
                //    //{
                //    //    strPrintText += m_hasItemDetail["��ʷ>>����ʷ"];
                //  //  p_intPosY += 20;
                //    //}
                //    //if (strPrintText != "  ��") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
                //    //m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
                //    //p_fntNormal.Dispose();
                //    m_objPrintContext.m_mthSetContextWithCorrectBefore(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);

                //    m_blnIsFirstPrint = false;
                //}

                //int intLine = 0;
                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

                //    p_intPosY += 20;

                //    intLine++;
                //}

                //if (m_objPrintContext.m_BlnHaveNextLine())
                //    m_blnHaveMoreLine = true;
                //else
                //{
                //    m_blnHaveMoreLine = false;
                //}

            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        }

        /// <summary>
        /// ���

        /// </summary>
        private class clsPrintHomeHistroy2 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strPrintText = "  ��";
            private clsInpatMedRec_Item objItemContent = null;
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    //if (m_blnCheckBottom(ref p_intPosY, 40, p_intPosY))
                    //{
                    //    m_blnHaveMoreLine = true;
                    //    return;
                    //}

                    p_objGrp.DrawString("�����ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //if (m_hasItemDetail != null && m_hasItemDetail.Contains("��ʷ>>����ʷ"))
                    //{
                    //    strPrintText += m_hasItemDetail["��ʷ>>����ʷ"];
                    p_intPosY += 20;
                    //}
                    //if (strPrintText != "  ��") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
                    //m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
                    //p_fntNormal.Dispose();

                    if (m_hasItems != null)
                        if (m_hasItems.Contains("������"))
                            objItemContent = m_hasItems["������"] as clsInpatMedRec_Item;
                    if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                    {
                        p_intPosY += 20;
                        m_blnHaveMoreLine = false;
                        return;
                    }


                    m_objPrintContext.m_mthSetContextWithCorrectBefore(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);

                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

                    p_intPosY += 20;

                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
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
        }


        /// <summary> 
        /// ����ҽʦ
        /// </summary>
        private class clsPrint12 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;

                }
                if (m_blnIsFirstPrint)
                {
                    string m_strPrint = string.Empty;
                    p_objGrp.DrawString("����ҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY+2);
                    if (m_hasItems != null)
                        if (m_hasItems.Contains("�������ҽʦǩ��"))
                            m_strPrint += (m_hasItems["�������ҽʦǩ��"] as clsInpatMedRec_Item).m_strItemContent;
                    //p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    if (!string.IsNullOrEmpty(m_strPrint))
                        m_strPrint = m_strPrint.Replace("������ҽʦ", "").Replace("����ҽʦ", "").Replace("��������ҽʦ", "").Replace("����ҽʦ", "").Replace("��ҽʦ", "").Replace("ҽʦ", "").Trim();
                    Image imgEmpSig = ImageSignature.GetEmpSigImage(m_strPrint);
                    if (imgEmpSig != null)
                    {
                        p_objGrp.DrawImage(imgEmpSig, m_intRecBaseX + 130, p_intPosY-2, 70, 30);
                    }
                    else
                    {
                        p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 130, p_intPosY);
                    }
                    p_intPosY += 30;
                    //string strOperations = "        ";
                    //string strAllText = "        ";
                    //string strXml = "";
                    //for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    //{
                    //    if (m_objContent.objSignerArr[i].controlName == "m_lsvOperations")
                    //        strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    //}
                    //if (strOperations != "")
                    //{
                    //    p_objGrp.DrawString("����ҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    //    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    //    m_mthMakeText(new string[] { strOperations }, new string[] { "" }, ref strAllText, ref strXml);
                    //}
                    //else
                    //{
                    //    m_blnHaveMoreLine = false;
                    //    return;
                    //}
                    //m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    //m_blnIsFirstPrint = false;
                    m_blnIsFirstPrint = false;
                    m_blnHaveMoreLine = false;
                }

                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 60, p_intPosY, p_objGrp);
                //    p_intPosY += 20;
                //}
                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_blnHaveMoreLine = true;
                //}
                //else
                //{
                //    m_blnHaveMoreLine = false;
                //}
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }
        }

        /// <summary>
        /// סԺҽʦ
        /// </summary>
        private class clsPrint13 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    string m_strPrint = string.Empty;
                    p_objGrp.DrawString("סԺҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY+2);
                    if (m_hasItems != null)
                        if (m_hasItems.Contains("���סԺҽʦǩ��"))
                            m_strPrint += (m_hasItems["���סԺҽʦǩ��"] as clsInpatMedRec_Item).m_strItemContent;
                    //p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    if (!string.IsNullOrEmpty(m_strPrint))
                        m_strPrint = m_strPrint.Replace("������ҽʦ", "").Replace("����ҽʦ", "").Replace("��������ҽʦ", "").Replace("����ҽʦ", "").Replace("��ҽʦ", "").Replace("ҽʦ", "").Trim();
                    Image imgEmpSig = ImageSignature.GetEmpSigImage(m_strPrint);
                    if (imgEmpSig != null)
                    {
                        p_objGrp.DrawImage(imgEmpSig, m_intRecBaseX + 130, p_intPosY-2, 70, 30);
                    }
                    else
                    {
                        p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 130, p_intPosY);
                    }
                    p_intPosY += 30;
                    //string strOperations = "        ";
                    //string strAllText = "        ";
                    //string strXml = "";
                    //for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    //{
                    //    if (m_objContent.objSignerArr[i].controlName == "m_lsvhelpers")
                    //        strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    //}
                    //if (strOperations != "")
                    //{
                    //    p_objGrp.DrawString("סԺҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    //    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    //    m_mthMakeText(new string[] { strOperations }, new string[] { "" }, ref strAllText, ref strXml);
                    //}
                    //else
                    //{
                    //    m_blnHaveMoreLine = false;
                    //    return;
                    //}
                    //m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    //m_blnIsFirstPrint = false;
                    m_blnIsFirstPrint = false;
                    m_blnHaveMoreLine = false;
                }

                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 60, p_intPosY, p_objGrp);
                //    p_intPosY += 20;
                //}
                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_blnHaveMoreLine = true;
                //}
                //else
                //{
                //    m_blnHaveMoreLine = false;
                //}
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }
        }

        /// <summary>
        /// ����������

        /// </summary>
        private class clsPrintLastDate : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strPrintText = "  ��";
            private clsInpatMedRec_Item objItemContent = null;
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                string m_strPrint = "���ڣ�";
                if (m_hasItems != null)
                    if (m_hasItems.Contains("����������"))
                        m_strPrint += (m_hasItems["����������"] as clsInpatMedRec_Item).m_strItemContent;
                
                if (m_blnIsFirstPrint)
                {
                    //if (m_blnCheckBottom(ref p_intPosY, 40, p_intPosY))
                    //{
                    //    m_blnHaveMoreLine = true;
                    //    return;
                    //}

                    p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //if (m_hasItemDetail != null && m_hasItemDetail.Contains("��ʷ>>����ʷ"))
                    //{
                    //    strPrintText += m_hasItemDetail["��ʷ>>����ʷ"];
                  //  p_intPosY += 20;
                    //}
                    //if (strPrintText != "  ��") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
                    //m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
                    //p_fntNormal.Dispose();
                    //m_objPrintContext.m_mthSetContextWithCorrectBefore(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);

                    m_blnIsFirstPrint = false;
                    m_blnHaveMoreLine = false;
                }

                //int intLine = 0;
                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

                //    p_intPosY += 20;

                //    intLine++;
                //}

                //if (m_objPrintContext.m_BlnHaveNextLine())
                //    m_blnHaveMoreLine = true;
                //else
                //{
                //    m_blnHaveMoreLine = false;
                //}

            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        }


        internal static Hashtable m_hasItemDetail;
        /// <summary>
        /// �����������Ϊ������Hastable
        /// </summary>
        /// <param name="p_hasItem"></param>
        /// <param name="p_ctlItem"></param>
        /// <param name="p_objItemArr"></param>
        /// <returns></returns>
        protected override Hashtable m_mthSetHashItem(clsInpatMedRec_Item[] p_objItemArr)
        {
            if (p_objItemArr == null)
                return null;
            Hashtable hasItem = new Hashtable(400);
            m_hasItemDetail = new Hashtable(400);
            foreach (clsInpatMedRec_Item objItem in p_objItemArr)
            {
                try
                {
                    if (objItem.m_strItemContent == null || objItem.m_strItemContent == "" || objItem.m_strItemContent == "False")
                    {
                        continue;
                    }
                    else
                    {
                        m_hasItemDetail.Add(objItem.m_strItemName, objItem.m_strItemContent);
                        hasItem.Add(objItem.m_strItemName, objItem);

                    }
                }
                catch { continue; }
            }
            return hasItem;
        }
    

        /// <summary>
        /// �������
        /// </summary>
        private class clsPrintInPatMedRecDia2 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private clsInpatMedRec_Item objItemContent1 = null;
            private clsInpatMedRec_Item objItemContent2 = null;
            private clsInpatMedRec_Item objItemContent3 = null;
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains("�������"))
                        objItemContent1 = m_hasItems["�������"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ��"))
                        objItemContent2 = m_hasItems["�������ҽʦǩ��"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ������"))
                        objItemContent3 = m_hasItems["�������ҽʦǩ������"] as clsInpatMedRec_Item;
                }
                if (objItemContent1 == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent1 == null ? "" : objItemContent1.m_strItemContent), (objItemContent1 == null ? "<root />" : objItemContent1.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent1 != null);
                    m_mthAddSign2("������ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    p_objGrp.DrawString("ҽʦǩ����  " + ((objItemContent2 == null || objItemContent2.m_strItemContent == null) ? "" : objItemContent2.m_strItemContent) +
                        "  ǩ�����ڣ�  " + ((objItemContent3 == null || objItemContent3.m_strItemContent == null) ? "" : objItemContent3.m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);
                    p_intPosY += 20;
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
                objItemContent1 = null;
                objItemContent2 = null;
                objItemContent3 = null;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        private class clsPrintInPatMedRecDia3 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private clsInpatMedRec_Item objItemContent1 = null;
            private clsInpatMedRec_Item objItemContent2 = null;
            private clsInpatMedRec_Item objItemContent3 = null;
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains("�������"))
                        objItemContent1 = m_hasItems["�������"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ��"))
                        objItemContent2 = m_hasItems["�������ҽʦǩ��"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ������"))
                        objItemContent3 = m_hasItems["�������ҽʦǩ������"] as clsInpatMedRec_Item;
                }
                if (objItemContent1 == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent1 == null ? "" : objItemContent1.m_strItemContent), (objItemContent1 == null ? "<root />" : objItemContent1.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent1 != null);
                    m_mthAddSign2("������ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    p_objGrp.DrawString("ҽʦǩ����  " + ((objItemContent2 == null || objItemContent2.m_strItemContent == null) ? "" : objItemContent2.m_strItemContent) +
                        "  ǩ�����ڣ�  " + ((objItemContent3 == null || objItemContent3.m_strItemContent == null) ? "" : objItemContent3.m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);
                    p_intPosY += 20;
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
                objItemContent1 = null;
                objItemContent2 = null;
                objItemContent3 = null;
            }
        }
	}
}
