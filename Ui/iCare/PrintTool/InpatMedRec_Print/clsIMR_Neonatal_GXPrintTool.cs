using System;
using System.IO;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Windows.Forms;
using System.Resources ; 

namespace iCare 
{
	/// <summary>
	/// ���¹淶
	/// �������ƿ�סԺ������ӡ������.
	/// </summary>
	public class clsIMR_Neonatal_GXPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_Neonatal_GXPrintTool(string p_strTypeID) :base(p_strTypeID)
		{
			//
			// TODO: Add constructor logic here
			//
		}
		private clsPrintInPatMedRecItem[] m_objPrintOneItemArr;
		private clsPrintInPatMedRecItem[] m_objPrintMultiItemArr;
		private clsPrintInPatMedRecSign[] m_objPrintSignArr;



		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{			            
			Font m_fotTitleFont;
			/// <summary>
			/// �����ݵ�����(11)
			/// </summary>
			Font m_fotSmallFont;
				/// <summary>
			/// ˢ��
			/// </summary>
			SolidBrush m_slbBrush;

			m_fotTitleFont = new Font("SimSun", 16,FontStyle.Bold);
			m_fotSmallFont = new Font("SimSun",12);
			m_slbBrush = new SolidBrush(Color.Black);
			

			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,310,40);		
			e.Graphics.DrawString("�� �� �� ס Ժ �� ¼",m_fotTitleFont,m_slbBrush,280,70);
		     

			e.Graphics.DrawString("���ң�",m_fotSmallFont,m_slbBrush,50,110);
			e.Graphics.DrawString( this.m_objPrintInfo.m_strDeptName,m_fotSmallFont,m_slbBrush,100,110);
	
			e.Graphics.DrawString("���ţ�",m_fotSmallFont,m_slbBrush,350,110);
			e.Graphics.DrawString(m_objPrintInfo.m_strBedName,m_fotSmallFont,m_slbBrush,400,110);

			//			e.Graphics.DrawString("���ң�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title ));
			//			e.Graphics.DrawString(m_objPrintInfo.m_strDeptName ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name ));
			//
			//			e.Graphics.DrawString("���ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title ));
			//			e.Graphics.DrawString(m_objPrintInfo.m_strBedName ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo ));	
		
			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,630,110);
			e.Graphics.DrawString(m_objPrintInfo.m_strInPatientID ,m_fotSmallFont,m_slbBrush,700,110);	
		}
		protected override void m_mthSetPrintLineArr()
		{

			m_mthInitPrintLineArr();
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		   new clsPrintPatientFixInfo(),
																		   m_objPrintMultiItemArr[0],
																		   m_objPrintMultiItemArr[1],
																		   m_objPrintMultiItemArr[2],m_objPrintMultiItemArr[3],m_objPrintMultiItemArr[4],
																		   m_objPrintMultiItemArr[5],m_objPrintMultiItemArr[6],m_objPrintMultiItemArr[7],
																		   m_objPrintMultiItemArr[8],m_objPrintMultiItemArr[9],m_objPrintMultiItemArr[10],
																		   m_objPrintMultiItemArr[11],m_objPrintMultiItemArr[12],m_objPrintMultiItemArr[13],
																		   m_objPrintMultiItemArr[14],m_objPrintMultiItemArr[15],m_objPrintMultiItemArr[16],
																		   m_objPrintMultiItemArr[17],m_objPrintMultiItemArr[18],m_objPrintMultiItemArr[19],
																		   m_objPrintMultiItemArr[20],m_objPrintMultiItemArr[21],m_objPrintMultiItemArr[22],
																		   m_objPrintMultiItemArr[23],m_objPrintMultiItemArr[24],m_objPrintMultiItemArr[25],new clsPrintSubInf(),
																		   m_objPrintMultiItemArr[26],m_objPrintMultiItemArr[27],
																		  m_objPrintSignArr[0],m_objPrintOneItemArr[5], m_objPrintSignArr[1],  m_objPrintOneItemArr[6],  m_objPrintSignArr[2]
																	   });
		}
	
		private void m_mthInitPrintLineArr()
		{
			m_objPrintOneItemArr = new clsPrintInPatMedRecItem[7];
			for(int i1=0;i1<m_objPrintOneItemArr.Length;i1++)
				m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();

			m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[28];
			for(int j2=0;j2<m_objPrintMultiItemArr.Length;j2++)
				m_objPrintMultiItemArr[j2] = new clsPrintInPatMedRecItem();

			m_objPrintSignArr = new clsPrintInPatMedRecSign[3];
			for(int k3=0;k3<m_objPrintSignArr.Length;k3++)
				m_objPrintSignArr[k3] = new clsPrintInPatMedRecSign();
		}
		#region ��ӡ��һҳ�Ĺ̶�����
		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
		internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region
				com.digitalwave.controls.ctlRichTextBox m_txtTemp=new ctlRichTextBox(); 
             	clsInpatMedRec_Item[] m_strMotherName=new clsInpatMedRec_Item[1];
				
				m_strMotherName=m_objGetContentFromItemArr(new string[]{"��ĸ����","��Ժʱ��"});
              
				p_objGrp.DrawString("������"+ m_objPrintInfo.m_strPatientName,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("��¼���ڣ�"+(m_objContent==null ? "": m_objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
						
				p_intPosY += 20;
				p_objGrp.DrawString("���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
				p_objGrp.DrawString("��ʷ�ߺͿɿ��̶ȣ�"+(m_objContent==null ? "": m_objContent.m_strRepresentor+"," +m_objContent.m_strCredibility),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
						
				p_intPosY += 20;
				p_objGrp.DrawString("�Ա�"+ m_objPrintInfo.m_strSex ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);	
				p_objGrp.DrawString("���᣺"+ m_objPrintInfo.m_strNativePlace ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);			
				
				p_intPosY += 20;
				p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("���壺"+ m_objPrintInfo.m_strNationality,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
						
				p_intPosY += 20;
				p_objGrp.DrawString("�������룺"+ m_objPrintInfo.m_StrHomePC,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("��ϵ�绰��"+ m_objPrintInfo.m_strHomePhone ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
				
				p_intPosY += 20;
				if(m_objPrintInfo.m_dtmInPatientDate != System.DateTime.MinValue)
				{
				

					p_objGrp.DrawString("��Ժ���ڣ�"+ m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HHʱ"),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);			
				}
				else
				{
					p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				}				
							
				m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��"+ m_objPrintInfo.m_strHomeAddress ,"<root />");
				int intRealHeight;
				Rectangle rtgBlock = new Rectangle(m_intPatientInfoX+350,p_intPosY,(int)enmRectangleInfo.RightX-(m_intPatientInfoX+350),30);
				m_objPrintContext.m_blnPrintAllBySimSun(11,rtgBlock,p_objGrp,out intRealHeight,false);

				p_intPosY += 20;
				if(m_strMotherName!=null)
				{
					if(m_strMotherName[0]!=null)
					{
						m_txtTemp.m_mthSetNewText(m_strMotherName[0].m_strItemContent,m_strMotherName[0].m_strItemContentXml);
						p_objGrp.DrawString("��/ĸ������"+(m_strMotherName[0]!=null?m_txtTemp.m_strGetRightText().ToString():""),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
					}
					else
					{
						p_objGrp.DrawString("��/ĸ������",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
					}

				}
				else
				{
	                    p_objGrp.DrawString("��/ĸ������",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				}
				//���ó�Ժ����
				DateTime m_dtmOutHospitalDate;
				m_lngGetOutDate(m_objPrintInfo.m_strInPatientID,m_objPrintInfo.m_dtmHISInDate,out m_dtmOutHospitalDate);
				if(m_dtmOutHospitalDate == new DateTime(1900,1,1) || m_dtmOutHospitalDate == DateTime.MinValue)
					   p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
				else
				{
					   p_objGrp.DrawString("��Ժ���ڣ�"+m_dtmOutHospitalDate.ToString("yyyy��MM��dd�� HHʱ"),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);			
				}  	

				m_blnHaveMoreLine = false;
				#endregion
				//				p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotItemHead ,Brushes.Black,340,45);
				//		
				//				p_objGrp.DrawString("ҩ �� �� �� �� ¼ ��",new Font("SimSun", 16,FontStyle.Bold),Brushes.Black,330,75);
				//				p_intPosY =130;
			}
			/// <summary>
			/// ��ȡ���˳�Ժʱ��
			/// </summary>
			/// <returns></returns>
			private  void m_lngGetOutDate(string p_StrPatientID,DateTime m_DtmSelectedInDate,out DateTime p_dtmOutHospitalDate)
			{
				p_dtmOutHospitalDate = new DateTime(1900,1,1);
				string strRegisterID = "";
				long lngRes = 0;
                //clsPatientManagerService objServ =
                //(clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPatientManagerService));

				lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRegisterIDByPatient(p_StrPatientID, m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strRegisterID);
				lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOutHospitalDate(strRegisterID, out p_dtmOutHospitalDate);
				//objServ = null;
				
			}
			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}
		}

		#endregion
	
		/// <summary>
		/// �����������Ϊ������Hastable
		/// </summary>
		/// <param name="p_hasItem"></param>
		/// <param name="p_ctlItem"></param>
		/// <param name="p_objItemArr"></param>
		/// <returns></returns>
		protected override Hashtable m_mthSetHashItem(clsInpatMedRec_Item[] p_objItemArr)
		{
			if(p_objItemArr == null)
				return null;
			Hashtable hasItem = new Hashtable(300);
			foreach(clsInpatMedRec_Item objItem in p_objItemArr)
			{
				if(objItem.m_strItemContent == null || objItem.m_strItemContent == "" || objItem.m_strItemContent == "False")
					continue;
				try
				{
					hasItem.Add(objItem.m_strItemName,objItem);
				}
				catch
				{
					continue;
					//					string strEx = ex.ToString();
					//					hasItem = null;
				}
			}
			return hasItem;
		}
		
		protected override void m_mthSetSubPrintInfo()
		{
			#region ̧ͷ��Ϣ
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"tt","tt"},
				new string[]{"tt","tt"});
		
			#endregion

			#region ������Ŀ
			//m_objPrintOneItemArr[0].m_mthSetPrintValue("��Ժ���","��Ժ��ϣ�");
			//m_objPrintOneItemArr[1].m_mthSetPrintValue("��Ժ���","��Ժ��ϣ�");
			//m_objPrintOneItemArr[2].m_mthSetPrintValue("����","���ߣ�");
			//m_objPrintOneItemArr[3].m_mthSetPrintValue("�ֲ�ʷ","�ֲ�ʷ��");
			
			
		
			#endregion	

			#region ��ʷ��¼
	
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{
																		 "��Ժ���",
																		 "��Ժ���",
                                                                         "����",
																		 "�ֲ�ʷ",
																		 "����ʷ>>̥�������",
																		 "����ʷ>>���󻼲����",
				                                                
																		 },
				new string[]{
								"\n ��Ժ��ϣ�",
								"\n ��Ժ��ϣ�",
								"\n ���ߣ�",
								"\n �ֲ�ʷ��",
								"\n����ʷ��1.̥���������",
								"\n                  2.���󻼲������",
								
							});
			#endregion
			#region ����ʷ
			//m_objPrintMultiItemArr[1].m_mthSetSpecialTitleValue("����ʷ"); 
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{
																		 "",
																		 "",
																		 "",
																		 "����ʷ>>̥",
																		 "",
																		 "",
																		 "����ʷ>>��",
																		 "",
																		 "",
																		 "����ʷ>>̥��",
																		 "",
																		 "",
																		 "����ʷ>>��������",
																		 "",
																		 "����ʷ>>��̥",
																		 "����ʷ>>˫̥",
																		 "����ʷ>>��",
																		 "����ʷ>>С",
																		 "����ʷ>>����ʱ��",
																		 "",
																		 "����ʷ>>���ھ���>>��",
																		 "����ʷ>>���ھ���>>��",
																		 "",
																		 "����ʷ>>̥Ĥ����ʱ��",
																		 ""			 },
				new string[]{
								"����ʷ��",
								"\n1.����ʷ��",
								"��(",
								"",
								"��̥��$$",
								"��",
								"",
								"������$$",
								"̥�䣨",
								"",
								"���ܣ�$$",
								"����ʱ���أ�",
								"",
								"���ˣ�$$",
								"#��̥$$",
								"#˫̥$$",
								"#��$$",
								"#С��$$",
								"\n                  ����ʱ�䣺",
								"",
				                "#���ھ��ȣ���",
				                "#���ھ��ȣ���",
				                "̥Ĥ���ƣ�",
				                "",
				                "��Сʱ��$$"
								});

			m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[]{	"",
																		 "����ʷ>>���䷽ʽ>>˳��",
																		 "����ʷ>>���䷽ʽ>>�ʹ���",
																		 "����ʷ>>���䷽ʽ>>������",
																		 "����ʷ>>���䷽ʽ>>����λ��ǯ��",
																		 "����ʷ>>���䷽ʽ>>��λ��",
																		 "����ʷ>>���䷽ʽ>>����",
																		 "����ʷ>>���䷽ʽ>>��������",
																		 "",
																		 "����ʷ>>��ˮ��",
																		 "",
																		 "����ʷ>>��ˮ>>��>>����",
																		 "����ʷ>>��ˮ>>��>>����",
																		 "����ʷ>>��ˮ>>��>>����",
																		 "����ʷ>>��ˮ>>��>>��",
																		 "����ʷ>>��ˮ>>��>>����",
																		 "����ʷ>>��ˮ>>��>>̥����",
																		 "����ʷ>>��ˮ>>��>>����",
																		 "",
																		 "����ʷ>>̥��>>���쳣",
																		 "�ƻ�",
																		 "����ʷ>>̥��>>���",
																		 "����ʷ>>̥��>>ǰ��",
																		 "",
																		 "����ʷ>>�����",
																		 "",
																		 "",
																		 "����ʷ>>�ƾ�����",
																		 "",
																		 "����ʷ>>̥��>.����"	 },
				new string[]{
								"                  ���䷽ʽ��",//����
								"#˳��",
								"#�ƹ���",
								"#������",
								"#��/��λ��ǯ��",
								"#��λ��",
								"#����",
								"",
								"\n                  ��ˮ������",
								"",
								"��ML��$$",
								"#����",
								"#����",
								"#����",
								"#��",
								"#����",
								"#̥����",
								"������",
								"\n                  ",
								"#̥�̣����쳣",
								"#̥�̣��ƻ�",
								"#̥�̣����",
								"#̥�̣�ǰ��",
								"���������",
				                "",
				                "��cm��$$",
				                "�ƾ���",
				                "",
				                "���ܣ�$$",
				                "������"
                       	});
			m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[]{	"",
																		 "",
																		 "����ʷ>>Apgar����>>1min",
																		 "",
																		 "",
																		 "����ʷ>>Apgar����>>5min",
																		 "",
																		 "",
																		 "����ʷ>>Apgar����>>10min",
																		 "",
																		 "",
																		 "����ʷ>>Apgar����>>15min",
																		 "",
																		 "",
																		 "����ʷ>>Apgar����>>����ʱ��",
																		 "",
																		 "",
																		 "����ʷ>>����ʱ�������>>��",
																		 "����ʷ>>����ʱ�������>>��",
																		 "",
																		 "����ʷ>>����ʱ�������>>���ܲ��>>��",
																		 "����ʷ>>����ʱ�������>>���ܲ��>>��",
																		 "",
																		 "����ʷ>>����ʱ�������>>����ճҺ����",
																		 "",
																		 "����ʷ>>����ʱ�������>>����ճҺ��ɫ",
																		 "",
																		 "����ʷ>>����ʱ�������>>ճҺ����>>Ѫ��",
																		 "����ʷ>>����ʱ�������>>ճҺ����>>ճҺ",
																		 "����ʷ>>����ʱ�������>>ճҺ����>>̥����",
																		 "����ʷ>>����ʱ�������>>������ҩ���",
																		 "����ʷ>>Ŀ�׷���ʱ��ҩ���" },
				new string[]{
								"                  Apgar���֣�",
								"1min��",
								"",
								"���֣�$$",
								"5min��",
								"",
								"���֣�$$",
								"10min��",
								"",
								"���֣�$$",
								"15min��",
								"",
								"���֣�$$",
								"����",
								"",
								"��Сʱ���գ�$$",
								"\n                  ����ʱ���������",
								"#����ʱ�����������",
								"#����ʱ�����������",
								"",
								"#���ܲ�ܣ���",
								"#���ܲ�ܣ���",
								"����ճҺ��",
								"",
								"��ml��$$",
								"��ɫ��",
								"",
								"#���ʣ�Ѫ��",
								"#���ʣ�ճҺ",
								"#���ʣ�̥����",
								"������ҩ�����",
								"\n                 ����ʱĸ����ҩ������򾲼�����ǰ4Сʱ���������(ҩ������"
								
							});
			#endregion
			#region ι��ʷ
			m_objPrintMultiItemArr[4].m_mthSetPrintValue(new string[]{	
																		 "",
																		 "",
																		 "ι��ʷ>>����ʱ��",
																		 "",
																		
																		 "ι��ʷ>>ι����ʽ>>ĸ��",
																		 "ι��ʷ>>ι����ʽ>>�˹�",
																		 "ι��ʷ>>ι����ʽ>>���"
																		  },
				new string[]{
								"2.ι��ʷ��",
								"����ʱ�䣺����",
								"",
								"��Сʱ��$$",
								
								"#ι����ʽ��ĸ��",
								"#ι����ʽ���˹�",
								"#ι����ʽ�����"
								
							});

			#endregion
			#region Ԥ������ʷ
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{	
																		 "",
																		 "Ԥ������ʷ>>������>>����",
																		 "Ԥ������ʷ>>������>>δ��",
																	     "Ԥ������ʷ>>�Ҹ�����>>����",
				                                                         "Ԥ������ʷ>>�Ҹ�����>>δ��"
			},
				new string[]{
								"3.Ԥ������ʷ��",
								"#�����磺����",
								"#�����磺δ��",
                                "#�Ҹ����磺����",
							    "#�Ҹ����磺δ��"
							});

			#endregion

			#region ����ʷ
	
			m_objPrintMultiItemArr[6].m_mthSetPrintValue(new string[]{	
																		 "",
																		 "����ʷ>>����>>����",
																		 "",
																		 "����ʷ>>����>>ְҵ",
																		 "����ʷ>>����>>ƽ�ս���״��",
																		 "����ʷ>>����>>Ѫ��",
																		 "",
																		 "����ʷ>>ĸ��>>����",
																		 "",
																		 "����ʷ>>ĸ��>>ְҵ",
																		 "����ʷ>>ĸ��>>ƽ�ս���״��",
																		 "����ʷ>>ĸ��>>Ѫ��",
																		 "",
																		 "����ʷ>>ĸ�����ڽ������>>���쳣",
																		 "����ʷ>>ĸ�����ڽ������>>���쳣����",
																		 "����ʷ>>ĸ�����ڽ������>>����ˮ��",
																		 "����ʷ>>ĸ�����ڽ������>>��������",
																		 "����ʷ>>ĸ�����ڽ������>>�����Ѫѹ",
																		 "����ʷ>>ĸ�����ڽ������>>������",
																		 "����ʷ>>ĸ�����ڽ������>>����",
																		 "����ʷ>>ĸ�����ڽ������>>����ϸ��������Ⱦ",
																		 "����ʷ>>ĸ�����ڽ������>>���ಡ",
																		 "����ʷ>>ĸ�����ڽ������>>���ಡ",
																		 "����ʷ>>ĸ�����ڽ������>>����",
																		 "����ʷ>>ĸ�����ڽ������>>ѪҺ��",
																		 "����ʷ>>ĸ�����ڽ������>>ƶѪ",
																		 "����ʷ>>ĸ�����ڽ������>>�β�",
																		 "����ʷ>>ĸ�����ڽ������>>���",
																		 "����ʷ>>ĸ�����ڽ������>>����",
																		 "����ʷ>>ĸ�����ڽ������>>��������",
																		 "����ʷ>>������ҩ���",
																		 "",
																		 "",
																		 "����ʷ>>�ֽ����>>�˹���������",
																		 "",
																		 "",
																		 "����ʷ>>�ֽ����>>��Ȼ��������",
																		 "",
																		 "����ʷ>>�ֽ����>>��̥",
																		 "����ʷ>>�ֽ����>>����",
																		 "����ʷ>>�ֽ����>>���",
																		 "",
																		 "����ʷ>>�ֽ����>>��������",
																		 "",
																		 "����ʷ>>�ֽ����>>���н���",
																		 "",
																		 "",
																		 "����ʷ>>�ֽ����>>��Ѫʷ>>��",
																		 "����ʷ>>�ֽ����>>��Ѫʷ>>��",
																		 "",
																		 "����ʷ>>�ֽ����>>��Ѫʷ>>��",
																		 "����ʷ>>�ֽ����>>��Ѫʷ>>��",
																		 "",
																		 "����ʷ>>�ֽ����>>��̥ʷ>>��",
																		 "����ʷ>>�ֽ����>>��̥ʷ>>��",
																		 "",
																		 "����ʷ>>��ĸ���׽��>>��",
																		 "����ʷ>>��ĸ���׽��>>��",
																		 "����ʷ>>�����Ŵ���ʷ"		           
																	 },
				new string[]{
								"����ʷ�����ף����䣨",
								"",
								"���ꣻ$$",
								"ְҵ��",
								"ƽ�ս���״����",
								"Ѫ�ͣ�",
								"\n                  ĸ�ף����䣨",
								"",
								"���ꣻ$$",
								"ְҵ��",
								"ƽ�ս���״����",
								"Ѫ�ͣ�",
								"\n                  ĸ�����ڽ��������",
								"#���쳣",
						    	"#���쳣����",
				                "\n                  ����ˮ��",
				                "#��������",
				"#�����Ѫѹ��",
				"#������",
				"#���ʣ�",
				"#����ϸ��������Ⱦ��",
				"#���ಡ��",
				"#���ಡ��",
				"#���򲡣�",
				"#ѪҺ����",
				"#ƶѪ��",
				"#�β���",
				"#��",
				"#������",
				"����(����):",
				"\n                  ������ҩ�����",
				"\n                  �ֽ������",
				"�˹�������",
				"",
				"���Σ�$$",
								"��Ȼ������",
								"",
								"���Σ�$$",
				"��̥��",
				"������",
				"�����",
				"\n                  ���У�",
				"",
				"���֣���$$",
				"",
                "���㣻$$",
				"",
				"#��Ѫʷ����",
				"#��Ѫʷ����",
								"",
								"#��Ѫʷ����",
								"#��Ѫʷ����",
								"",
								"#��̥ʷ����",
								"#��̥ʷ����",
								"",
								"#\n                  ��ĸ���׽�飺��",
								"#\n                  ��ĸ���׽�飺��",
								"\n                  �����Ŵ���ʷ��"
		        
							});

			#endregion

			#region ������¼
			m_objPrintMultiItemArr[7].m_mthSetSpecialTitleValue("�����"); 
			m_objPrintMultiItemArr[7].m_mthSetPrintValue(new string[]{	
																		 "",
																		 "",
																		 "�����>>������¼>>����",
																		 "",
																		 "",
																		 "�����>>������¼>>����",
																		 "",
																		 "",
																		 "�����>>������¼>>����",
																		 "",
																		 "",
																		 "�����>>������¼>>Ѫѹ",
																		 "",
																		 "",
																		 "�����>>������¼>>����",
																		 "",
																		 "",
																		 "�����>>������¼>>ͷΧ",
																		 "",
																		 "",
																		 "�����>>������¼>>��Χ",
																		 "",
																		 "",
																		 "�����>>������¼>>��Χ",
																		 "",
																		 "",
																		 "�����>>������¼>>��",
																		 ""

																	 },
				new string[]{
								"������¼��",
								"���£�",
								"",
								"���棻$$",
								"������",
								"",
								"����/�֣�$$",

								"������",
								"",
								"����/�֣�$$",

								"Ѫѹ��",
								"",
								"��mmHg��$$",
								"���أ�",
								"",
								"���ˣ�$$",

								"\n                    ͷΧ��",
								"",
								"��cm��$$",

								"��Χ��",
								"",
								"��cm��$$",
								"��Χ��",
								"",
								"��cm��$$",
								"����",
								"",
								"��cm��$$"
							});

			#endregion
			#region �����(һ�����)
			m_objPrintMultiItemArr[8].m_mthSetPrintValue(new string[]{	
                                                                         "",
																		 "�����>>һ�����>>��ò",
																		 "�����>>һ�����>>��ɫ",
																		 "�����>>һ�����>>����",
																		 "�����>>һ�����>>����Ӧ��",
																		 "�����>>һ�����>>��־",
																		 "�����>>һ�����>>����",
																		 "�����>>һ�����>>������",
																		 "",
																		 "�����>>һ�����>>Ӫ��״��>>����",
																		 "�����>>һ�����>>Ӫ��״��>>�е�",
																		 "�����>>һ�����>>Ӫ��״��>>����",
																		 "�����>>һ�����>>Ӫ��״��>>����1",
																		 "�����>>һ�����>>Ӫ��״��>>����2",
																		 "�����>>һ�����>>Ӫ��״��>>����3",
																		 ""

																	 },
				new string[]{
								"һ�������",
								"��ò��",
								"��ɫ��",
							    "������",
								"����Ӧ��",
							    "��־��",
							    "\n                    ������",
							    "��������",
						        "",
								"#Ӫ��״��������",
								"#Ӫ��״�����е�",
								"#Ӫ��״����������",
								"#���)",
								"#���)",
								"#���)",
								""
							});

			#endregion
			#region Ƥ��ճĤ
			m_objPrintMultiItemArr[9].m_mthSetPrintValue(new string[]{	
																		 "",
																		 "�����>>Ƥ��ճĤ>>��ɫ",
																		 "�����>>Ƥ��ճĤ>>Ƥ��",
																		 "�����>>Ƥ��ճĤ>>��Ѫ���ٰ�",
																		 "�����>>Ƥ��ճĤ>>ˮ��",
																		 "�����>>Ƥ��ճĤ>>Ӳ��",
																		 "�����>>Ƥ��ճĤ>>����",
																		 "�����>>Ƥ��ճĤ>>�������",
																	
																		 "�����>>Ƥ��ճĤ>>֫��Ƥ���¶�>>����",
																		 "�����>>Ƥ��ճĤ>>֫��Ƥ���¶�>>��ů",
																		 "�����>>Ƥ��ճĤ>>֫��Ƥ���¶�>>����",
																		 "�����>>Ƥ��ճĤ>>��Ƥ"
																		 

																	 },
				new string[]{
								"Ƥ��ճĤ��",
								"��ɫ��",
								"Ƥ�",
								"��Ѫ���ٰߣ�",
								"ˮ�ף�",
								"\n                    Ӳ�ף�",
								"���ԣ�",
								"���������",
								
								"#\n                  ֫��Ƥ���¶ȣ�����",
								"#\n                  ֫��Ƥ���¶ȣ���ů",
								"#\n                  ֫��Ƥ���¶ȣ�����",
								"��Ƥ��"
							});

			#endregion
			#region �ܰͽ�
			m_objPrintMultiItemArr[10].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "�����>>�ܰͽ�>>��ǳ���ܰͽ��״�",
																		  "�����>>�ܰͽ�>>��ǳ���ܰͽ��״�",
																		  "�����>>�ܰͽ�>>ǳ���ܰͽ��״�λ"
																	 },
				new string[]{ "�ܰͽ᣺",
								"#ǳ���ܰͽ��״���",
								"#ǳ���ܰͽ��״���",
								"��λ��"
								
							});

			#endregion
			#region ͷ��
			m_objPrintMultiItemArr[11].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "�����>>ͷ��>>��̬����",
																		  "�����>>ͷ��>>��̬����",
																		  "",
																		  "�����>>ͷ��>>ǰض",
																		  "",
																		  "�����>>ͷ��>>ǰض¡��",
																		  "�����>>ͷ��>>ǰضƽ̹",
																		  "�����>>ͷ��>>ǰض����",
																		  "",
																		  "�����>>ͷ��>>��ض",
																		  "",
																		  "�����>>ͷ��>>�Ƿ�",
																		  "�����>>ͷ��>>��­����",
																		  "�����>>ͷ��>>��­����",
																		  "�����>>ͷ��>>­������λ",
																		  "",
																		  "�����>>ͷ��>>­������Χ",
																		  "",
																		  "�����>>ͷ��>>ë��",
																		  "�����>>ͷ��>>��ˮ��",
																		  "�����>>ͷ��>>��ˮ��",
																		  "�����>>ͷ��>>ˮ�ײ�λ",
																		  "",
																		  "�����>>ͷ��>>ˮ�׷�Χ",
																		  "",
																		  "�����>>ͷ��>>ˮ������"
								

																	  },
				new string[]{ "ͷ������̬��",
								"#����",
								"#����",
								"ǰض��",
								"",
				                "��cm��$$",
								"#¡��",
								"#ƽ̹",
								"#����",
								"��ض��",
								"",
								"��cm��$$",
				     			"\n             �Ƿ죺",
				                "#­��������",
								"#­��������",
								
				"��λ��",
				"��Χ��",
				"",
				"��cm��$$",
				"ë����",
				"#\n                  ˮ�ף���",
			    "#\n                  ˮ�ף���",
			
								"��λ��",
								"��Χ��",
								"",
								"��cm��$$",
				"������"
								

				
							});

			#endregion
			#region ��
			m_objPrintMultiItemArr[12].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "�����>>��>>��Ĥ����",
																		  "�����>>��>>��Ѫ",
																		  "�����>>��>>��Ѫ",
																		 
																		  "�����>>��>>�з�����",
																		  "�����>>��>>�޷�����",
																		
																		  "�����>>��>>ͫ������",
																		  "�����>>��>>ͫ��ɢ��",
																		  "�����>>��>>ͫ����С",
																		  "�����>>��>>ͫ�ײ��Գ�",
																		 
																		  "�����>>��>>�Թⷴ������",
																		  "�����>>��>>�Թⷴ��ٶ�",
																		  "�����>>��>>�Թⷴ����ʧ",
																		
																		  "�����>>��>>��������",
																		  "�����>>��>>�������",
																		  "�����>>��>>����ͻ��",
																		  "�����>>��>>��������",
																		  
																		  "�����>>��>>��Ĥ����",
																		  "�����>>��>>��Ⱦ",
																		
																		  "�����>>��>>��Ĥ����",
																		  "�����>>��>>��Ĥ����",
																		  "�����>>��>>��Ĥ����",
																		  "�����>>��>>����"

																	  },
				new string[]{ "    �ۣ�",
								"#��Ĥ������",
								"#��Ĥ����Ѫ",
								"#��Ĥ����Ѫ",
								
								"#�������",
								"#�������",
								
								"#ͫ�ף�����",
								"#ͫ�ף�ɢ��",
								"#ͫ�ף���С",
								"#ͫ�ף����Գ�",
								
								"#\n             �Թⷴ�䣺������",
								"#\n             �Թⷴ�䣺�ٶۣ�",
								"#\n             �Թⷴ�䣺��ʧ��",
								
								"#��������",
								"#�������",
								"#����ͻ��",
								"#��������",
								"#\n             ��Ĥ������",
								"#\n             ��Ĥ����Ⱦ",
								"#��Ĥ������",
								"#��Ĥ������",
								"#��Ĥ������",
								"������"
								

				
							});

			#endregion
			#region ��
			m_objPrintMultiItemArr[13].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "�����>>��>>�������",
																		  "�����>>��>>�������",
																		  
																		  "�����>>��>>����������",
																		  "�����>>��>>��������һ��",
																		  "�����>>��>>����������",
																	
																		  "�����>>��>>�ж�������",
																		  "�����>>��>>�޶�������",
																		  "�����>>��>>�Ͷ�λ"

																	  },
				new string[]{
								"\n    ����",
								"#�����������",
								"#��������Σ�",
								
								"#�����������ã�",
								"#����������һ�㣻",
								"#�����������",
								
								"#��������У�",
								"#��������ޣ�",
								"#�Ͷ�λ��"
								
							});

			#endregion
			#region ��
			m_objPrintMultiItemArr[14].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "�����>>��>>�ⲿ����",
																		  "�����>>��>>�ⲿ����",
																		  "",
																		  "�����>>��>>�б���",
																		  "�����>>��>>�ޱ���",
																		  "",
																		  "�����>>��>>�бǷ�����",
																		  "�����>>��>>�ޱǷ�����"
								
																	  },
				new string[]{
								"    �ǣ��ⲿ��",
								"#����",
								"#����",
								"���ȣ�",
								"#��",
								"#��",
								"�Ƿ����",
								"#��",
								"#��"
								
							});

			#endregion
			#region ��
			m_objPrintMultiItemArr[14].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "�����>>��>>��������",
																		  "�����>>��>>���ܷ��",
								
																		  "�����>>��>>�ڴ�����",
																		  "�����>>��>>�ڴ�����",
																		  "�����>>��>>�ڴ��԰ף�",
																		  "�����>>��>>�ڴ�����",
																		  "�����>>��>>ճĤ",
																		  "�����>>��>>����"
																	  },
				new string[]{
								"\n    �ڣ�",
								"#���ܣ�������",
								"#���ܣ���礣�",
								
								"#�ڴ�������",
								"#�ڴ������ϣ�",
								"#�ڴ����԰ף�",
								"#�ڴ������",
								"ճĤ��$$",
								"���񲿣�"
								
							});

			#endregion
			#region ����
			m_objPrintMultiItemArr[14].m_mthSetPrintValue(new string[]{	
																		  "",
								
																		  "�����>>����>>�л���",
																		  "�����>>����>>�޻���",
																		  
																		  "�����>>����>>�еֿ�",
																		  "�����>>����>>�޵ֿ�",
																		  
																		  "�����>>����>>��״���״�>>��",
																		  "�����>>����>>��״���״�>>��",
																		  "�����>>����>>����"								  },
				new string[]{
								"\n������",
								
								"#���Σ���",
								"#���Σ���",
								
								"#�ֿ�����",
								"#�ֿ�����",
								
								"#��״���״���",
								"#��״���״���",
								"����"
								
							});

			#endregion
			#region �ز�
			m_objPrintMultiItemArr[15].m_mthSetPrintValue(new string[]{	
																		  "",
																		  
																		  "�����>>����>>�л���",
																		  "�����>>����>>�޻���",
																		  
																		  "�����>>����>>�еֿ�",
																		  "�����>>����>>�޵ֿ�",
								
																		  "�����>>����>>��״���״�>>��",
																		  "�����>>����>>��״���״�>>��",
                                                                          "�����>>����>>����"
																	  },
				new string[]{
								"�ز���",
								
								"#�������Σ��У�",
								"#�������Σ��ޣ�",
								
								"#˫���������Գƣ�",
								"#˫�����������Գƣ�",
								
								"#���ǹ��ۣ���",
								"#���ǹ��ۣ���",
				                "������"
								
							});

			#endregion
			#region �β�
			m_objPrintMultiItemArr[16].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "",
																	
																		  "�����>>����>>˫�κ����>>�Գ�",
																		  "�����>>����>>˫�κ����>>���Գƣ�",
																		  "",
																		  "�����>>����>>������ʽ>>��ʽ",
																		  "�����>>����>>������ʽ>>��ʽ",
																		  "",
																		  "",
																		  "�����>>����>>˫�δ������>>�Գ�",
																		  "�����>>����>>˫�δ������>>���Գ�",
																		  "",
																		  "�����>>ߵ��>>����",
																		  "�����>>ߵ��>>������",
																		  "�����>>ߵ��>>����",
																		  "�����>>ߵ��>>ʵ��",
																		  "�����>>ߵ��>>ʵ��",
																		  "",
																		  "",
																		  "�����>>����>>˫�κ�����>>�Գ�",
																		  "�����>>����>>���Գ�",
																		  "",
																		  "�����>>����>>ǿ�� >>��ǿ",
																		  "�����>>����>>ǿ�� >>����",
																		  "",
																		  "�����>>����>>ǿ�� >>�І���",
																		  "�����>>����>>ǿ�� >>�ކ���",
																		  "�����>>����>>ǿ�� >>��λ������"
				
																	  },
				new string[]{
								"    �Σ�",
								"���",
								
								"#˫�κ�������Գ�",
								"#˫�κ���������Գƣ�",
								"",
								"#������ʽ����ʽ",
								"#������ʽ����ʽ",
								"\n             ���",
								"˫�δ��������",
								"#�Գ�",
								"#���Գƣ�",
								"\n             ���",
								"#����",
								"#������",
                                "#����",
								"#ʵ��",
								"ʵ��������",
								"\n             ���",
				"",
				"#˫�κ��������Գ�",
				"#˫�κ����������Գ�",
				"",
				"#ǿ�ȣ���ǿ",
				"#ǿ�ȣ�����",
				"",
				"#��������",
				"#��������",
				"\n                         ��λ�����ʣ�"
				                 
							});

			#endregion
			#region ����--����+����
			m_objPrintMultiItemArr[17].m_mthSetPrintValue(new string[]{	
                                                                          "",
																		  "",
																		  "",
																		  "�����>>����>>����>>��ǰ����¡��",
																		  "�����>>����>>����>>��ǰ����¡��",
																		  "",
																		  "",
																		  "�����>>����>>����>>�ļⲫ��λ��",
																		  "",
																		  "�����>>����>>����>>ǿ����ǿ",
																		  "�����>>����>>����>>ǿ�ȼ���",
																		  "",
																		  "",
																		  "�����>>����>>����>>����ǰ�����",
																		  "�����>>����>>����>>����ǰ�����",
																		  "�����>>����>>����>>λ��"
								
																	  },
				new string[]{
								"���ࣺ",
								"���",
								"",
								"#��ǰ����¡��",
								"#��ǰ����¡��",
								"",
								"�ļⲫ����",
								"λ�ã�",
								"",
								"#ǿ�ȣ���ǿ",
								"#ǿ�ȣ�����",
								"\n             ����",
				                "",
			                    "#��ǰ���������",
			                    "#��ǰ���������",
						        "λ�ã�"
						 		});

			#endregion
			#region ����--ߵ��
			m_objPrintMultiItemArr[18].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "",
																		  "�����>>����>>ߵ��>>���Ľ���Զ������߼�",
																		  "",
																		  "�����>>����>>ߵ��>>���Ľ���Զ����������",
																		  "�����>>����>>ߵ��>>���Ľ���Զ����������",
																		  "",
																		  "�����>>����>>ߵ��>>���Ľ���Զ�������Ǿ���",
																		  "",
																		  "",
																		  "�����>>����>>ߵ��>>���Ľ���Զ���Ҳ��߼�",
																		  "",
																		  "�����>>����>>ߵ��>>���Ľ���Զ����������",
																		  "�����>>����>>ߵ��>>���Ľ���Զ����������",
																		  "",
																		  "�����>>����>>ߵ��>>���Ľ���Զ�������Ǿ���",
																		  ""
								
																	  },
				new string[]{
								"             ߵ�",
								"���Ľ���Զ�����ڣ�",
								"",
								"���߼����������ߣ�$$",
								"#��$$",
								"#��$$",
								"����$$",
								"",
								"��cm����$$",
								"\n                          ���Ľ���Զ���Ҳ�ڣ�",
								"",  
								"���߼����������ߣ�$$",
								"#��$$",
								"#��$$",
								"����$$",
								"",
								"��cm����$$"
								
							});

			#endregion
			#region ����--����
			m_objPrintMultiItemArr[19].m_mthSetPrintValue(new string[]{	
																		  "",
								            							  "",
																		  "�����>>����>>����>>����",
																		  "",
																		  "",
																		  "�����>>����>>����>>������",
																		  "�����>>����>>����>>���ɲ���",
																		  "",
																		  "�����>>����>>����>>����ǿ",
																		  "�����>>����>>����>>������",
																		  "",
																		  "�����>>����>>����>>������",
																		  "�����>>����>>����>>������",
																		  "�����>>����>>����>>����",
																	  },
				new string[]{
								"             ���",
								"����(",
								"",
								"����/�֣�$$",
								"",
								"#���ɣ���",
								"#���ɣ�����",
								"",
								"#������ǿ",
								"#��������",
								"",
								"#\n                          ��������",
								"#\n                          ��������",
								"������",
								
							});

			#endregion
			#region Ѫ��
			m_objPrintMultiItemArr[20].m_mthSetPrintValue(new string[]{	
																		  
																		  "",
																		  "",
																		  "�����>>Ѫ��>>�ɶ�������>>ǿ",
																		  "�����>>Ѫ��>>�ɶ�������>>��",
																		  "",
																		  "�����>>Ѫ��>>ëϸѪ�ܳ�ӯʱ��",
																		  ""
			},
				new string[]{
								"Ѫ�ܣ�",
								"",
								"#�ɶ���������ǿ",
								"#�ɶ�����������",
								"ǰ���ڲ�ëϸѪ�ܳ�ӯʱ�䣨",
								"",
								"���룻$$"
								
			});

			#endregion
			#region ����
			m_objPrintMultiItemArr[21].m_mthSetPrintValue(new string[]{	
																		  
																		  "",
																		  "",
																		  "",
																		  "�����>>����>>����>>����",
																		  "",
																		  "�����>>����>>����>>�г���",
																		  "�����>>����>>����>>�޳���",
																		  "",
																		  "�����>>����>>����>>���䶯��",
																		  "�����>>����>>����>>���䶯��",
																		  "��",
																		  "�����>>����>>����>>�������",
																		  "�����>>����>>����>>���δ��",
																		  "",
																		  "�����>>����>>����>>�겿���쳣",
																		  "�����>>����>>����>>���ֺ���",
																		  "",
																		  "�����>>����>>����>>�з�����",
																		  "�����>>����>>����>>�޷�����",
																		  "�����>>����>>����>>����������",
																		  "",
																		  "�����>>����>>����>>������",
																		  "�����>>����>>����>>������",
																		  "",
																		  "",
																		  "�����>>����>>����>>������",
																		  "�����>>����>>����>>���ڽ���",
																		  "",
																		  "�����>>����>>����>>�������¾���",
																		  "",
																		  "",
																		  "�����>>����>>����>>��ͻ�¾���",
																		  "",
																		  "",
																		  "�����>>����>>����>>���ʵ���",
																		  "�����>>����>>����>>���ʵ���",
																		  "�����>>����>>����>>���ʵ�Ӳ",
																		  "",
																		  "�����>>����>>����>>Ƣ�����¾���",
																		  "",
																		  "",
																		  "�����>>����>>����>>Ƣ�ʵ���",
																		  "�����>>����>>����>>Ƣ�ʵ���",
																		  "�����>>����>>����>>Ƣ�ʵ�Ӳ",
																		  "�����>>����>>����>>����",
																		  "",
																		  "",
																		  "�����>>����>>ߵ��>>���ƶ�������",
																		  "�����>>����>>ߵ��>>���ƶ�������",
																		  "",
																		  "",
																		  "�����>>����>>����>>������Ƶ��",
																		  "",
																		  "�����>>����>>ߵ��>>������>>����",
																		  "�����>>����>>ߵ��>>������>>����",
																		  "�����>>����>>ߵ��>>������>>����",
																		  "�����>>����>>ߵ��>>������>>��ʧ",
																	  },
				new string[]{
								"������",
								"���",
								"���Σ�",
								"",
								"",
								"#���Σ����䶯��",
								"#���Σ����䶯��",
								"",
								"",
								"",
								"",
								"#���������",
								"#�����δ��",
								"",
								"#\n                          �겿�����쳣",
								"#\n                          �겿�����ֺ���",
								"",
								"#�������",
								"#�������",
								"���������ʣ�",
								"",
								"#���ޣ���",
								"#���ޣ���",
								"\n              ���",
								"",
								"#���ڣ���",
								"#���ڣ�����",
								"�������£�",
								"",
								"��cm��$$",
								"��ͻ�£�",
								"",
								"��cm��$$",
								"$$",
								"#�ʵأ���",
								"#�ʵأ���",
								"#�ʵأ�Ӳ",
								"Ƣ�����£�",
								"",
								"��cm��$$",
								"",
								"#�ʵأ���",
								"#�ʵأ���",
								"#�ʵأ�Ӳ",
								"���죺",
								"",
								"",
								"#\n             ����ƶ�����������",
								"#\n             ����ƶ�����������",
								"\n             ���",
								"��������",
				                "",
				                "����/�֣�$$",
				                "#����",
				                "#����",
				                "#����",
				                "#��ʧ"
								
							});

			#endregion
			#region ������֫
			m_objPrintMultiItemArr[21].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "",
																		  "�����>>������֫>>�л���",
																		  "�����>>������֫>>�޻���",
																		  "",
																		  "�����>>������֫>>��֫������>>����",
																		  "�����>>������֫>>��֫������>>����",
																		  "�����>>������֫>>��֫������>>����",
																		  "�����>>������֫>>��֫������>>���",
																		  "�",
																		  "�����>>������֫>>�>>����",
																		  "�����>>������֫>>�>>����",
																		  "",
																		  "�����>>������֫>>��֫ĩ���¶�>>ů",
																		  "�����>>������֫>>��֫ĩ���¶�>>��",
																		  "�����>>������֫>>����",
																	  },
				new string[]{
								"\n������֫��",
								"",
								"#���Σ���",
								"#���Σ���",
								"",
								"#��֫������������",
								"#��֫������������",
								"#��֫������������",
								"#��֫�����������",
								"",
								"#�������",
								"#�������",
								"",
								"#\n                   ��֫ĩ���¶�ů",
								"#\n                   ��֫ĩ���¶���",
				                "����",


								
							});

			#endregion
			#region ������ֳ��
			m_objPrintMultiItemArr[23].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "�����>>��������ֳ������>>����",
																		  "�����>>��������ֳ������>>����ֳ��",
																		  "�����>>��������ֳ������>>����"
			},
				new string[]{
								"���š�����ֳ����������",
								"���ţ�",
								"����ֳ����",
								"\n                                             ������"
								
			});

			#endregion
			#region ��ϵͳ
			m_objPrintMultiItemArr[24].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "�����>>��ϵͳ>>ӵ������",
																		  "�����>>��ϵͳ>>��˱����",
																		  "�����>>��ϵͳ>>�ճַ��䣺",
																		  "�����>>��ϵͳ>>Χ����",
																		  "�����>>��ϵͳ>>̤������",
																		  "�����>>��ϵͳ>>�������ȷ���",
																		  "�����>>��ϵͳ>>������",
																		  "�����>>��ϵͳ>>�N����"
																	  },
				new string[]{
								"��ϵͳ��",
								"ӵ�����䣺",
								"��˱���䣺",
								"�ճַ��䣺",
								"Χ������",
								"\n                     ̤�����䣺",
								"�������ȷ��䣺",
								"��������",
								"�N������"
								
							});
			#endregion
		    #region ̥������
			m_objPrintMultiItemArr[25].m_mthSetPrintValue(new string[]{	
																		  "",
																		  "̥������",
																		  "",
																		  "̥�����ֲ���",
																		  "",
																		  "̥������",
																		  ""
																	  },
				new string[]{
								"̥�����֣�����24Сʱ������27+��",
								"",
								"������$$",
								"",
								"���֣�̥������Ϊ��$$",
								"",
								"���֣�$$"
															
								
							});
			#endregion
			#region �������
			m_objPrintMultiItemArr[26].m_mthSetSpecialTitleValue("�������");
			m_objPrintMultiItemArr[26].m_mthSetPrintValue(new string[]{	"�������"},new string[]{"������飺"});
			#endregion
			#region �������
			m_objPrintMultiItemArr[27].m_mthSetPrintValue(new string[]{
																		  "",
																		  "�������1",
																		  "�������2",
																		  "�������3"
																	  },new string[]{
                                          "������ϣ�",
								          "1��", 
										  "\n                      2��",
				                          "\n                      3��"
																					});
			#endregion
			#region ǩ��������
			m_objPrintSignArr[0].m_mthSetPrintSignValue(new string[]{"ҽʦǩ��","��¼����"},new string[]{"ҽʦǩ����","��¼���ڣ�"});
			#endregion
            #region ����/��������Լ�ǩ��
            m_objPrintOneItemArr[5].m_mthSetPrintValue("�������", "������ϣ�");
            m_objPrintOneItemArr[6].m_mthSetPrintValue("�������", "������ϣ�");
            m_objPrintSignArr[1].m_mthSetPrintSignValue(new string[] { "�������ҽʦǩ��", "�������ҽʦǩ������" }, new string[] { "ҽʦǩ����", "ǩ�����ڣ�" });
            m_objPrintSignArr[2].m_mthSetPrintSignValue(new string[] { "�������ҽʦǩ��", "�������ҽʦǩ������" }, new string[] { "ҽʦǩ����", "ǩ�����ڣ�" });
                  #endregion





		}
	
		
		#region Print Class

		/// <summary>
		/// ��Ŀ��ӡ
		/// </summary>
		private class clsPrintInPatMedRecItem : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

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
				if(m_blnNoContent == true && m_blnNoPrint == true)
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
                        //�ж��Ƿ�����/�������
                        if (m_strTitle == "������ϣ�" || m_strTitle == "������ϣ�")
                            m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent == null ? "" : m_objItemContent.m_strItemContent), (m_objItemContent == null ? "<root />" : m_objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, m_objItemContent != null, Color.Red);
                        else
						    m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent == null ? "" : m_objItemContent.m_strItemContent)  ,(m_objItemContent==null ? "<root />" : m_objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,m_objItemContent != null);
						m_mthAddSign2(m_strTitle,m_objPrintContext.m_ObjModifyUserArr);
					}
					else
					{
						if(m_strSpecialTitle != "")
						{
							p_objGrp.DrawString(m_strSpecialTitle,clsIMR_HerbalismPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+300,p_intPosY);
							p_intPosY += 20;
						}
						m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strText ,m_strTextXml,m_dtmFirstPrintTime,m_blnNoPrint == false);
						m_mthAddSign2(m_strSpecialTitle,m_objPrintContext.m_ObjModifyUserArr);
					}

					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					if(m_strTitle != "")
						m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);
					else
						m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+20,p_intPosY,p_objGrp);
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
						p_objGrp.DrawString(m_strTitleArr[i]+(objSignContent[i]==null ? "" : objSignContent[i].m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
						p_intPosY += 20;
					}
					else
					{
						p_objGrp.DrawString(m_strTitleArr[i]+ (objSignContent[i] == null ? "" :DateTime.Parse( objSignContent[i].m_strItemContent).ToString("yyyy��MM��dd��")),p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
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
		#region ����ӡ
		/// <summary>
		/// ����ӡ
		/// </summary>
		private class clsPrintSubInf : clsIMR_PrintLineBase
		{
			#region Define

			private clsPrintRichTextContext m_objDiagnoseR = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objDiagnoseL = new clsPrintRichTextContext(Color.Black,new Font("",10));
			//			private string m_strTitle = "";
			//			private string[] m_strTitleArr = null;
			private string m_strImagePath = Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()) + "\\picture\\Ophthalmology\\";

			/// <summary>
			/// ���Ӹ߶�
			/// </summary>
			private const int c_intHeight = 40;
			/// <summary>
			/// ������X��
			/// </summary>
			private const int c_intShortLeft = 140;
			/// <summary>
			/// ������X��
			/// </summary>
			private const int c_intShortRight = 663;
			/// <summary>
			/// ��ӡ���ݸ��ӿ��
			/// </summary>
			//private const int c_intWidth = 323;
			private const int c_intWidth = 127;
			/// <summary>
			/// ��ӡС������
			/// </summary>
			private const int c_intTitleWidth = 80;
			//			private int m_intLongLineTop = 150;
			/// <summary>
			/// ��ӡ���ߵ�X����
			/// </summary>
			//			private int m_intLeftX = (int)enmRectangleInfo.LeftX -10;

			//			private int m_intIndex = 0;
			//			int m_intPosY;

			//			private bool m_IsPrintCol0=false;
			private bool m_IsPrintCol1=false;
			private bool m_IsPrintCol0=false;
			private bool m_IsPrintCol2=false;
			private bool m_IsPrintCol3=false;
			private bool m_IsPrintCol4=false;
			private bool m_IsTitlePrint=false;
	
			private Pen PrintPenInf =new Pen(Color.Black ,1);

			//			private bool m_IsPrintCol7=false;

			#endregion

			public clsPrintSubInf()
			{
		
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

				int ColHeight;
		
                ColHeight=0;
			
				m_IsPrintCol0=false;
				m_IsPrintCol1=false;
				m_IsPrintCol2=false;
				m_IsPrintCol3=false;
				m_IsPrintCol4=false;
				m_IsTitlePrint=false;



				
				if (m_IsTitlePrint==false)
				{
					p_objGrp.DrawString ("������̥�����ֱ�",p_fntNormalText,Brushes.Black,(float)(enmRectangleInfo.LeftX+250),(float)p_intPosY+20);
					//				p_intPosY+=20;
					
					m_IsTitlePrint=true;
				}
			
				ColHeight=40;
				p_intPosY+=40;
               
				//				#region ���������
				////				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY-10 ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY-10);
				//				#endregion
				System.Drawing.Font p_TsFont=new Font (p_fntNormalText.Name ,p_fntNormalText.Size -3);
				if (m_IsPrintCol0==false)
				{
					if (m_mthIsPage(p_intPosY,ColHeight)) {return;}
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_TsFont,"     ����           ����",ColHeight,-1,-1,"");
					p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.LeftX+80) ,(float)p_intPosY+ColHeight);
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"                  0��   ",ColHeight,0,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"                  1��   ",ColHeight,1,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"                  2��   ",ColHeight,2,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"                  3��   ",ColHeight,3,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"                  4��   ",ColHeight,4,-1,"");
					m_IsPrintCol0=true;
					ColHeight=40;
					p_intPosY+=40;
				}
				

				if (m_IsPrintCol1==false)
				{
					//��ҳ��
					if (m_mthIsPage(p_intPosY,ColHeight)) {p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);return;}
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"Ƥ����֯",ColHeight,-1,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"��������״",ColHeight,0,-1,"������̥��������ֱ�>>Ƥ����֯>>��������״");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"�����⻬",ColHeight,1,-1,"������̥��������ֱ�>>Ƥ����֯>>�����⻬");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"�еȺ�Ƥ��",ColHeight,2,-1,"������̥��������ֱ�>>Ƥ����֯>>�еȺ�Ƥ��");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"�񡢱�Ƥ����",ColHeight,3,-1,"������̥��������ֱ�>>Ƥ����֯>>�񡢱�Ƥ����");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"����Ƥֽ��",ColHeight,4,-1,"������̥��������ֱ�>>Ƥ����֯>>����Ƥֽ��");
					
					m_IsPrintCol1=true;
				}

				ColHeight=40;
				p_intPosY+=40;
				if (m_IsPrintCol2==false)
				{
					if (m_mthIsPage(p_intPosY,ColHeight)) {p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);return;}
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"��ͷ�γ�",ColHeight,-1,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"���ϡ�������",ColHeight,0,-1,"������̥��������ֱ�>>��ͷ�γ�>>���ϡ�������");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"����ƽ����ֱ�� <0.75cm",ColHeight,1,-1,"������̥��������ֱ�>>��ͷ�γ�>>����ƽ����ֱ�� <0.75cm");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"���ε�״��ֱ��<0.75cm",ColHeight,2,-1,"������̥��������ֱ�>>��ͷ�γ�>>���ε�״��ֱ��<0.75cm");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"���ε�״��ֱ��>0.75cm",ColHeight,3,-1,"������̥��������ֱ�>>��ͷ�γ�>>���ε�״��ֱ��>0.75cm");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"",ColHeight,4,-1,"");
					
					m_IsPrintCol2=true;
				}

				ColHeight=40;
				p_intPosY+=40;
				if (m_IsPrintCol3==false)
				{
					if (m_mthIsPage(p_intPosY,ColHeight)) {p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);return;}
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"ָ  ��",ColHeight,-1,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"",ColHeight,0,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"δ��ָ��",ColHeight,1,-1,"������̥��������ֱ�>>ָ��>>δ��ָ��");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"�Ѵ�ָ��",ColHeight,2,-1,"������̥��������ֱ�>>ָ��>>�Ѵ�ָ��");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"����ָ��",ColHeight,3,-1,"������̥��������ֱ�>>ָ��>>����ָ��");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"",ColHeight,4,-1,"");
				
					
					m_IsPrintCol3=true;
				}

				ColHeight=40;
				p_intPosY+=40;
				if (m_IsPrintCol4==false)
				{
					if (m_mthIsPage(p_intPosY,ColHeight)) {p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);return;}
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"�������",ColHeight,-1,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"��",ColHeight,0,-1,"������̥��������ֱ�>>�������>>��");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"ǰ�벿��۲�����",ColHeight,1,-1,"������̥��������ֱ�>>�������>>ǰ�벿��۲�����");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"���>ǰ�벿�޺�<ǰ1/3",ColHeight,2,-1,"������̥��������ֱ�>>�������>>���>ǰ�벿  �޺�<ǰ1/3");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"�޺�>ǰ2/3",ColHeight,3,-1,"������̥��������ֱ�>>�������>>�޺�>ǰ2/3");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"��������޺�>2/3",ColHeight,4,-1,"������̥��������ֱ�>>�������>>���ϡ�������");
					
					m_IsPrintCol4=true; 
				}

				p_intPosY+=40;
				#region ������
				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);
				#endregion
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_objDiagnoseR.m_mthRestartPrint();	
				m_objDiagnoseL.m_mthRestartPrint();	
			}

			private void m_mthPrintDetail(ref int p_intPosY, System.Drawing.Graphics p_objGrp, 
				System.Drawing.Font p_fntNormalText,
				string PrintStr,int CellHeight,int Col,int p_LineX,string p_Key)
			{
				StringFormat p_StrFormat=new StringFormat ();
				p_StrFormat.FormatFlags =StringFormatFlags.FitBlackBox;
				

				Rectangle rtgCellinf = new Rectangle(0,0,0,0);
				//				Pen PrintPenInf =new Pen(Color.Black ,1);
				if (Col==-1) 
				{
					#region ��������˵��л�����
					if (p_LineX==-1)
					{
						//����˵���
						p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);
					}
					else
					{
						p_objGrp.DrawLine (PrintPenInf,p_LineX,(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);
					}
					#endregion
					

					#region ������ߵ�����
					p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)(p_intPosY) ,(float)(enmRectangleInfo.LeftX+15) ,(float)(p_intPosY+CellHeight));
					#endregion
					
				}
				
				switch (Col)
				{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					    #region ��ȥ��һ�и���ÿ�л�����
						p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+80+c_intWidth*Col)+5,(float)(p_intPosY) ,(float)(enmRectangleInfo.LeftX+80+c_intWidth*Col)+5 ,(float)(p_intPosY+CellHeight));
						#endregion
						rtgCellinf=new Rectangle ((int)(enmRectangleInfo.LeftX+80+c_intWidth*Col)+5,p_intPosY,c_intWidth,CellHeight);
						break;
					case -2:
						rtgCellinf=new Rectangle ((int)(enmRectangleInfo.LeftX+20)+5,p_intPosY,20,CellHeight);
						break;
					default:
						rtgCellinf=new Rectangle ((int)(enmRectangleInfo.LeftX+10)+5,p_intPosY,c_intWidth,CellHeight);
						#region �����ұߵ�����
						p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.RightX-20),(float)(p_intPosY) ,(float)(enmRectangleInfo.RightX-20) ,(float)(p_intPosY+CellHeight));
						#endregion 
						break;
			
				}
				if(m_hasItems!=null)
				{
					if(m_hasItems.Contains(p_Key)&&p_Key!="")
					{
						p_objGrp.DrawString (" �� "+PrintStr,p_fntNormalText,Brushes.Black  ,rtgCellinf,p_StrFormat);
					}
					else
					{
						p_objGrp.DrawString (PrintStr,p_fntNormalText,Brushes.Black,rtgCellinf,p_StrFormat);
					}
				}
				else
				{
	                  p_objGrp.DrawString (PrintStr,p_fntNormalText,Brushes.Black,rtgCellinf,p_StrFormat);
				}
				if (Col==4 )
				{
					//p_intPosY=p_intPosY+CellHeight+10;
					//					p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+80+c_intWidth*Col),(float)(p_intPosY-10) ,(float)(enmRectangleInfo.LeftX+80+c_intWidth*Col) ,(float)(p_intPosY+CellHeight+10));
				}
			}

			private bool m_mthIsPage(int p_intPosY,int p_ColHeight)
			{
				if(p_intPosY+40+p_ColHeight > ((int)enmRectangleInfo.BottomY -50))
				{
					m_blnHaveMoreLine = true;
					
					p_intPosY += 500;
					return true;
				}
				else
				{
					return false;
				}

			}

		}

		#endregion ����ӡ

	}
}
