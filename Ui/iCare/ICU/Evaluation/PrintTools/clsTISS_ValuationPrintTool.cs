using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;
using com.digitalwave.Utility.Controls;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// TISS-28评分评分打印工具
	/// </summary>
	public class clsTISS_ValuationPrintTool: clsValuationPrintBase
	{
		public clsTISS_ValuationPrintTool()
		{}
		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new clsPrintContext(
				new clsPrintLineBase[]{
										  new clsPrintPatientFixInfo(),
										  new clsPrintValuation()
									  });
		}
		protected override void m_mthPrintSubTitle(System.Drawing.Graphics p_objGrp)
		{
			p_objGrp.DrawString("TISS-28评分",m_fotItemHead,m_slbBrush,320,60);
		}

		#region Print Class
		/// <summary>
		/// 项目打印
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsTISSValuationInfo m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"　","　　","　　　　　　"};
			/// <summary>
			/// 左边距
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;
			private int m_intIndex = 0;
			private bool m_blnPrintNextPage = false;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as clsTISSValuationInfo;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				p_intPosY += 10;
				if(m_intIndex == 0)
				{
					p_objGrp.DrawString("评分结果： " + m_objPrintSubInfo.strResult.TrimEnd() + " 。",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY);
					p_intPosY += 30;
				}
				m_mthPrintBabyInjury(ref p_intPosY,p_objGrp,p_fntNormalText);
				if(m_blnPrintNextPage)
				{
					m_blnHaveMoreLine = true;
					m_blnPrintNextPage = false;
					return;
				}
				p_intPosY += 30;
				p_objGrp.DrawString("评分日期：" + DateTime.Parse(m_objContent.m_strActivityTime).ToString("yyyy年MM月dd日") + m_strSpaceArr[1] + "评估者：" + new clsEmployee(m_objPrintSubInfo.strEvalDoctorID).m_StrFirstName,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+50,p_intPosY);
				
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}

			private void m_mthPrintBabyInjury(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region Set string
				string[] strContentArr = new String[]{m_objPrintSubInfo.blnItem1,m_objPrintSubInfo.blnItem2,m_objPrintSubInfo.blnItem3,m_objPrintSubInfo.blnItem4,m_objPrintSubInfo.blnItem5,m_objPrintSubInfo.blnItem6,m_objPrintSubInfo.blnItem7
													 ,m_objPrintSubInfo.blnItem8,m_objPrintSubInfo.blnItem9,m_objPrintSubInfo.blnItem10,m_objPrintSubInfo.blnItem11,m_objPrintSubInfo.blnItem12,m_objPrintSubInfo.blnItem13,m_objPrintSubInfo.blnItem14
													 ,m_objPrintSubInfo.blnItem15,m_objPrintSubInfo.blnItem16,m_objPrintSubInfo.blnItem17,m_objPrintSubInfo.blnItem18,m_objPrintSubInfo.blnItem19,m_objPrintSubInfo.blnItem20,m_objPrintSubInfo.blnItem21
													 ,m_objPrintSubInfo.blnItem22,m_objPrintSubInfo.blnItem23,m_objPrintSubInfo.blnItem24,m_objPrintSubInfo.blnItem25,m_objPrintSubInfo.blnItem26,m_objPrintSubInfo.blnItem27,m_objPrintSubInfo.blnItem28};
				string[] strTitleArr = {"基础项目：","","","","","","","通气支持：","","","","心血管支持：","","","","","","","肾脏支持：","","","神经系统支持：","代谢支持：","","","特殊干预措施","",""};
				string[] strTextArr = new String[28];
				strTextArr[0] = "1、标准检测：每小时生命特征、液体平衡的常规记录和计算";
				strTextArr[1] = "2、实验室检查：生化和微生物学检查";
				strTextArr[2] = "3、单一药物：静脉、肌肉、皮下注射，和（或）口服（例如经胃管给药）";
				strTextArr[3] = "4、静脉使用多种药物：单次静注或持续输注1种以上药物";
				strTextArr[4] = "5、常规更换敷料：褥疮的护理和预防，每日更换一次敷料";
				strTextArr[5] = "6、频繁更换敷料：频繁更换敷料（每次护理班至少更换1次）和（或）大面积伤口护理";
				strTextArr[6] = "7、引流管的护理：除胃管以外的所有导管的护理";
				strTextArr[7] = "8、机械通气：任何形式的机械通气/辅助通气，无论是否使用PEEP或肌松药；加用PEEP的自主呼吸";
				strTextArr[8] = "9、其他通气支持：经气管插管自主呼吸，不应用PEEP；除了所应用的机械通气模式外， 提供任何形式的氧疗";
				strTextArr[9] = "10、人工气道的护理：气管插管或气管切开的护理";
				strTextArr[10] = "11、改善肺功能：胸部理疗，刺激性肺量计、吸入疗法、气管内吸痰";
				strTextArr[11] = "12、单一血管活性药物：使用任何血管活性药物";
				strTextArr[12] = "13、多种血管活性药物：使用1种以上的血管活性药物，不论种类和剂量";
				strTextArr[13] = "14、静脉补充丢失的大量液体：输液量>3L/(m  * d)，不论液体种类";
				strTextArr[14] = "15、放置外周动脉导管";
				strTextArr[15] = "16、左心房监测：放置肺动脉漂浮导管，不论是否测量心排出量";
				strTextArr[16] = "17、中心静脉置管";
				strTextArr[17] = "18、在过去24小时进行过心跳骤停后心肺复苏（单次心前区叩击除外）";
				strTextArr[18] = "19、血液过滤、血液透析";
				strTextArr[19] = "20、定量测定尿量（例如，经导尿管测量）";
				strTextArr[20] = "21、积极利尿（例如，呋塞米>0.5mg/(kg * d)治疗体液超负荷）";
				strTextArr[21] = "22、颅内压测量";
				strTextArr[22] = "23、复杂性代谢性中毒或碱中毒的治疗";
				strTextArr[23] = "24、静脉营养支持";
				strTextArr[24] = "25、胃肠内营养：经胃管或其他胃肠道途径（例如，空肠造瘘）";
				strTextArr[25] = "26、ICU内单一特殊干预措施：经鼻或经口气管插管、放置起博器、心律转复、内镜检查、过去24h内急诊手术、洗胃。对患者临床情况不产生直接影响的常规干预措施，入X线检查、超声检查、心电图检查、更换敷料、放置静脉或动脉导管等不包括在内";
				strTextArr[26] = "27、ICU内多种特殊干预措施：上述项目种1种以上的干预措施";
				strTextArr[27] = "28、ICU外的特殊干预措施：手术或急诊性操作";
				#endregion
				p_intPosY += 15;
				RectangleF rtg = new RectangleF(m_intXPos+10,p_intPosY,(int)enmRectangleInfo.RightX-20-m_intXPos,20);
				for(int i=m_intIndex;i<strContentArr.Length;i++)
				{
					if(p_intPosY+20 > ((int)enmRectangleInfo.BottomY))
					{
						m_intIndex = i;
						p_intPosY += 500;
						m_blnPrintNextPage = true;
						return;
					}
					if(strTitleArr[i] != "")
					{
						p_objGrp.DrawString(strTitleArr[i],p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
						p_intPosY += 20;
					}
					if(strContentArr[i] == "False")
						continue;
					string strText = "　　"+strTextArr[i];
					SizeF szfText = p_objGrp.MeasureString(strText,p_fntNormalText,Convert.ToInt32(rtg.Width));
					rtg.Height = szfText.Height+5;
					rtg.Y = p_intPosY;
                    if (strContentArr[i] == "1")
                    {
                        m_mthDrawCheckBox(m_intXPos + 30, p_intPosY, p_objGrp, p_fntNormalText, true);
                    }
                    else
                    {
                        m_mthDrawCheckBox(m_intXPos + 30, p_intPosY, p_objGrp, p_fntNormalText, false);
                    }
					p_objGrp.DrawString(strText,p_fntNormalText,Brushes.Black,rtg);
					p_intPosY += Convert.ToInt32(rtg.Height);
				}
			}
		}
		#endregion

	}
}


