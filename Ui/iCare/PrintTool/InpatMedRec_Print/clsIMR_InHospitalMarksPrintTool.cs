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
	/// clsIMR_InHospitalMarksPrintTool ��ժҪ˵����
	/// </summary>
	public class clsIMR_InHospitalMarksPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_InHospitalMarksPrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			
		}
		private clsPrintInPatMedRecItem[] m_objPrintOneItemArr;
		private clsPrintInPatMedRecItem[] m_objPrintMultiItemArr;
		private clsPrintInPatMedRecSign[] m_objPrintSignArr;
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
																		   m_objPrintMultiItemArr[20],m_objPrintMultiItemArr[21],m_objPrintMultiItemArr[22],m_objPrintMultiItemArr[23],
																		   m_objPrintMultiItemArr[24],m_objPrintMultiItemArr[25],m_objPrintMultiItemArr[26],m_objPrintMultiItemArr[27],
																		   m_objPrintMultiItemArr[28], m_objPrintMultiItemArr[29], m_objPrintMultiItemArr[30],m_objPrintSignArr[0],
                    m_objPrintOneItemArr[6], m_objPrintSignArr[1],  m_objPrintOneItemArr[7],  m_objPrintSignArr[2]
																		  // new clsPrintInHospitalMarks(),	
																		  // new clsSignNameAndDate()													  
																		
																	   });
        }
		private void m_mthInitPrintLineArr()
		{
			m_objPrintOneItemArr = new clsPrintInPatMedRecItem[8];
			for(int i1=0;i1<m_objPrintOneItemArr.Length;i1++)
				m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();

			m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[31];
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
		private class clsPrintPatientFixInfo : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				clsInpatMedRec_Item[] m_strOutHospitalDate=new clsInpatMedRec_Item[3];
				
				m_strOutHospitalDate=m_objGetContentFromItemArr(new string[]{"��Ժʱ��","��Ժ���","�ڼ���סԺ"});
              

								p_objGrp.DrawString("����ҽѧ����סԺ־",m_fotItemHead,Brushes.Black,m_intRecBaseX+285,p_intPosY - 10);
						
								p_intPosY += 20;
								p_objGrp.DrawString("������"+ m_objPrintInfo.m_strPatientName,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
								p_objGrp.DrawString("��¼���ڣ�"+(m_objContent==null ? "": m_objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
						
								p_intPosY += 20;
								p_objGrp.DrawString("���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
								p_objGrp.DrawString("��ʷ�ߺͿɿ��̶ȣ�"+(m_objContent==null ? "": m_objContent.m_strRepresentor+"," +m_objContent.m_strCredibility),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
						
								p_intPosY += 20;
								p_objGrp.DrawString("�Ա�"+ m_objPrintInfo.m_strSex ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);	
								p_objGrp.DrawString("�����أ�"+ m_objPrintInfo.m_strNativePlace ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);			
				
								p_intPosY += 20;
								p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
								p_objGrp.DrawString("סԺ�ţ�"+ m_objPrintInfo.m_strHISInPatientID ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
						
								p_intPosY += 20;
								p_objGrp.DrawString("ְҵ��"+ m_objPrintInfo.m_strOccupation ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
								p_objGrp.DrawString("��ϵ�ˣ�"+m_objPrintInfo.m_strLinkManFirstName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
						
								p_intPosY += 20;
								p_objGrp.DrawString("������"+ m_objPrintInfo.m_strMarried,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
								p_objGrp.DrawString("�绰��"+ m_objPrintInfo.m_strHomePhone ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
				
								p_intPosY += 20;
								p_objGrp.DrawString("���壺"+ m_objPrintInfo.m_strNationality ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
								p_objGrp.DrawString("������λ��"+ m_objPrintInfo.m_strOfficeName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);	
						
								p_intPosY += 20;
								if(m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
								{
									p_objGrp.DrawString("��Ժ���ڣ�"+ m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HHʱmm��"),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);			
								}
								else
								{
									p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
								}	
					

//			com.digitalwave.PatientManagerService.clsPatientManagerService objServ=new com.digitalwave.PatientManagerService.clsPatientManagerService();
//               iCare.clsPatient m_objCurrentPatient=null;
//				DateTime m_dtmOutHospitalDate=new DateTime();
//				string strRegisterID;
//				 objServ.m_lngGetRegisterIDByPatient(m_objCurrentPatient.m_StrPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strRegisterID);
//				objServ.m_lngGetOutHospitalDate(strRegisterID,out m_dtmOutHospitalDate);
//				if(m_dtmOutHospitalDate == new DateTime(1900,1,1) || m_dtmOutHospitalDate == DateTime.MinValue)
//					p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);			
//				else
//				{
//					p_objGrp.DrawString("��Ժ���ڣ�"+ Convert.ToDateTime(m_dtmOutHospitalDate).ToString("yyyy��MM��dd�� HHʱ"),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);			
//					
//				}
				if(m_strOutHospitalDate!=null)
				{
					if(m_strOutHospitalDate[2]!=null)
						p_objGrp.DrawString("��"+m_strOutHospitalDate[2].m_strItemContent+"��סԺ",p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);			
                     p_intPosY += 20;
					if(m_strOutHospitalDate[0]!=null&&m_strOutHospitalDate[1]!=null)
					{
					
						if(Convert.ToDateTime( m_strOutHospitalDate[0].m_strItemContent) != System.DateTime.MinValue&&(m_strOutHospitalDate[1].m_strItemContent)!="false")
						{
							p_objGrp.DrawString("��Ժ���ڣ�"+ Convert.ToDateTime(m_strOutHospitalDate[0].m_strItemContent).ToString("yyyy��MM��dd�� HHʱ"),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);			
					
						}
						else
							p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);			

					}
					else
						p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);			
				    
					
				}
				
//				/// <summary>
//				/// ��ȡ���˳�Ժʱ�䣬��ʱ���ڸ��������ѯ
//				/// </summary>
//				/// <returns></returns>
//				DateTime p_dtmOutHospitalDate;
//					p_dtmOutHospitalDate = new DateTime(1900,1,1);
//					string strRegisterID = "";
//					long lngRes = 0;
//					clsPatientManagerService objServ = new clsPatientManagerService();
//
//					lngRes = objServ.m_lngGetRegisterIDByPatient(m_objCurrentPatient.m_StrPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy��MM��dd�� HHʱmm��"), out strRegisterID);
//			
//					lngRes = objServ.m_lngGetOutHospitalDate(strRegisterID, out p_dtmOutHospitalDate);
//					objServ = null;
//                 
//				if(p_dtmOutHospitalDate != DateTime.MinValue)
//				{
//					p_objGrp.DrawString("��Ժ���ڣ�"+ p_dtmOutHospitalDate.ToString("yyyy��MM��dd�� HHʱmm��"),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);			
//				}
//				else
//				{
//					p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
//				}	

								m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��"+ m_objPrintInfo.m_strHomeAddress ,"<root />");
								int intRealHeight;
								Rectangle rtgBlock = new Rectangle(m_intPatientInfoX+350,p_intPosY,(int)enmRectangleInfo.RightX-(m_intPatientInfoX+350),30);
								m_objPrintContext.m_blnPrintAllBySimSun(11,rtgBlock,p_objGrp,out intRealHeight,false);
												
								p_intPosY += 30;
								m_blnHaveMoreLine = false;
				#endregion
	
			//	p_intPosY =+130;
				m_blnHaveMoreLine = false;
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

			#region �������
	
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{
																		 "�������>>��ʱ���",
																		 "�������>>������",
																		 "�������>>����֢",
																		 "�������>>����",
																		 "�������>>���������",
					},
				new string[]{	"��ʱ��ϣ�",
								"\n�����ϣ�",
								"\n����֢��",
								"\n��Ժʱ���ߣ�",
								"\n��������飺"
							
								
			});
			#endregion
			#region ��ȥ��ʷ
			
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(
			new string[]{"","","��ʷ>>ƽ�ؽ���>>����","��ʷ>>ƽ�ؽ���>>һ��","��ʷ>>ƽ�ؽ���>>�ϲ�"},																
			
			new string[]{"��ȥ��ʷ(���Դ�̣����Դ�0,���Կ��ڿ�������ϸ��д)","\n                ƽ�ؽ�����","#����","#һ��","#�ϲ�"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(
				new string[]{"","��ʷ>>����","��ʷ>>���","��ʷ>>�˺�","��ʷ>>����","��ʷ>>Ѫ���没"},																
			
				new string[]{"��   ��Ⱦ��ʷ��","���� ","��� ","�˺� ","���� ","Ѫ���没 "});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(
				new string[]{"��ʷ>>����ʷ","��ʷ>>ҩ�����","��ʷ>>���Կ���","��ʷ>>��Ѫ","��ʷ>>����","��ʷ>>�ļ�"},																
			
				new string[]{"\n                ����ʷ ","ҩ����� ","���Կ��� ","��Ѫ ","���� ","�ļ� "});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(
				new string[]{"��ʷ>>�Ͷ��󼱴�","��ʷ>>��Ѫѹ","��ʷ>>θʹ","��ʷ>>ŻѪ","��ʷ>>�ڱ�","��ʷ>>����"},																
			
				new string[]{"\n                �Ͷ��󼱴� ","��Ѫѹ ","θʹ ","ŻѪ ","�ڱ� ","���� "});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(
				new string[]{"��ʷ>>��������","��ʷ>>Ѫ��","��ʷ>>�沿����","��ʷ>>����","��ʷ>>Ƥ�³�Ѫ","��ʷ>>����"},																
			
				new string[]{"\n                �������� ","Ѫ�� ","�沿���� ","���� ","Ƥ�³�Ѫ ","���� "});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(
				new string[]{"��ʷ>>��ʺ","��ʷ>>ʳ������","��ʷ>>����"},																
			
				new string[]{"\n                ��ʺ ","ʳ������ ","���� "});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(
				new string[]{"��ʷ>>��ϸ"},																
			
				new string[]{"\n                 "});

			#endregion
			#region ����ʷ
			m_objPrintMultiItemArr[2].m_mthSetPrintValue(
				new string[]{"��ʷ>>����ʷ>>������","��ʷ>>����ʷ>>����������","��ʷ>>����ʷ>>����","��ʷ>>����ʷ>>�Ļ��̶�"},																
			
				new string[]{"����ʷ�������أ� ","���������أ�","���֣�","�Ļ��̶ȣ�"});
			m_objPrintMultiItemArr[2].m_mthSetPrintValue(
				new string[]{"","��ʷ>>����ʷ>>����Ӵ�ʷ>>��","��ʷ>>����ʷ>>����Ӵ�ʷ>>��","","��ʷ>>����ʷ>>����ʷ>>��","��ʷ>>����ʷ>>����ʷ>>��","","��ʷ>>����ʷ>>����ʷ>>��","��ʷ>>����ʷ>>����ʷ>>��"},																
			
				new string[]{"\n                ����Ӵ�ʷ��","#��","#��","����ʷ��","#��","#��","����ʷ��","#��","#��"});

			#endregion

			#region ��ͥʷ
			m_objPrintMultiItemArr[3].m_mthSetPrintValue(
				new string[]{"","��ʷ>>��ͥʷ>>��������","��ʷ>>��ͥʷ>>��������","��ʷ>>��ͥʷ>>�����ѹ�","��ʷ>>��ͥʷ>>��>>����"},																
			
				new string[]{"��ͥʷ��","#��������","#��������","#�����ѹ�","����"});
			m_objPrintMultiItemArr[3].m_mthSetPrintValue(
				new string[]{"","��ʷ>>��ͥʷ>>ĸ������","��ʷ>>��ͥʷ>>ĸ������","��ʷ>>��ͥʷ>>ĸ���ѹ�","��ʷ>>��ͥʷ>>ĸ>>����"},																
			
				new string[]{"�� ","#ĸ������","#ĸ������","#ĸ���ѹ�","����"});

			#endregion

			#region һ����Ŀ
			m_objPrintMultiItemArr[4].m_mthSetSpecialTitleValue("һ����");
			m_objPrintMultiItemArr[4].m_mthSetPrintValue(
				new string[]{"һ�����>>T","","һ�����>>P","","һ�����>>R","","һ�����>>BP",""},																
			
				new string[]{"һ����Ŀ��T��","�棻$$","P��","��/�֣�$$","R��","��/�֣�$$","BP��","/$$"});
			m_objPrintMultiItemArr[4].m_mthSetPrintValue(
				new string[]{"һ�����>>BP>>mmHg",""},																
			
				new string[]{"","mmHg��$$"});

			m_objPrintMultiItemArr[4].m_mthSetPrintValue(
				new string[]{"","һ�����>>����>>����","һ�����>>����>>����","","һ�����>>Ӫ��>>��","һ�����>>Ӫ��>>��","һ�����>>Ӫ��>>��"},																
			
				new string[]{"\n                ������","#����","#����","Ӫ����","#��","#��","#��"});
			m_objPrintMultiItemArr[4].m_mthSetPrintValue(
				new string[]{"","һ�����>>��̬>>���а�����","һ�����>>��̬>>���а�����","һ�����>>��̬>>���а�����","һ�����>>��̬>>�����ݣ���","һ�����>>��̬>>�����ݣ���","һ�����>>��̬>>�����ݣ���"},																
			
				new string[]{"����̬��","#�ߡ�","#�С�","#����","#��","#��","#��"});

			#endregion

			#region Ƥ��ճĤ
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(
				new string[]{"","һ�����>>����","һ�����>>�԰�","һ�����>>��ů","һ�����>>����","һ�����>>Ƥ��","һ�����>>����"},																
			
				new string[]{"Ƥ��ճĤ��","#����","#�԰ס�","#��ů��","#���ʡ�","#Ƥ�","#���ߣ�"});
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(
				new string[]{"һ�����>>Ƥ��ճĤ>>�촯����","һ�����>>Ƥ��ճĤ>>�촯����"},																
			
				new string[]{"# �촯����","# �촯����"});
			#endregion
			#region �ܰͽ�
			m_objPrintMultiItemArr[6].m_mthSetPrintValue(
				new string[]{"","һ�����>>�ܰͽ�>>�״���","һ�����>>�ܰͽ�>>�״�:��","һ�����>>�ܰͽ�>>�״�>>��λ����","һ�����>>�ܰͽ�>>�״�>>��λ��Ҹ��","һ�����>>�ܰͽ�>>�״�>>��λ�����ɹ�"},																
			
				new string[]{"�ܰͽ᣺","#�״��У�","#�״�:�ޣ�","#��λ����","#��λ��Ҹ��","#��λ�����ɹ�"});
			
			#endregion
			#region ͷ��
			m_objPrintMultiItemArr[7].m_mthSetPrintValue(
				new string[]{"","һ�����>>ͷ��>>­��ȱ����","һ�����>>ͷ��>>­��ȱ����","һ�����>>ͷ��>>­��ȱ��>>��λ����","һ�����>>ͷ��>>­��ȱ��>>��λ����","һ�����>>ͷ��>>­��ȱ��>>��λ������"
							,"һ�����>>ͷ��>>­��ȱ��>>��λ����","һ�����>>ͷ��>>­��ȱ��>>��λ����","һ�����>>ͷ��>>­��ȱ��>>��λ���"},																
			
				new string[]{"ͷ����","#­��ȱ����","#­��ȱ����","#����λ����","#��λ����","#����","#��","#��","#�"});
			m_objPrintMultiItemArr[7].m_mthSetPrintValue(
				new string[]{"","һ�����>>ͷ��>>��Ĥ��ŧ>>��","һ�����>>ͷ��>>��Ĥ��ŧ>>��","","һ�����>>ͷ��>>��Ĥ��Ѫ>>��","һ�����>>ͷ��>>��Ĥ��Ѫ>>��","",
								"һ�����>>ͷ��>>�����ѹʹ>>��","һ�����>>ͷ��>>�����ѹʹ>>��"},																
			
				new string[]{"\n            ��Ĥ����","#��","#��","��Ĥ��Ѫ��","#��","#��","�����ѹʹ��","#��","#��"});
			m_objPrintMultiItemArr[7].m_mthSetPrintValue(
				new string[]{"","һ�����>>ͷ��>>������ŧ>>��","һ�����>>ͷ��>>������ŧ>>��","","һ�����>>ͷ��>>��ǻ����>>��","һ�����>>ͷ��>>��ǻ����>>��","",
								"һ�����>>ͷ��>>�ʲ���Ѫ>>��","һ�����>>ͷ��>>�ʲ���Ѫ>>��"},																
			
				new string[]{"��������ŧ��","#��","#��","��ǻ����","#��","#��","�ʲ���Ѫ��","#��","#��"});
			
			#endregion
			#region ����
			m_objPrintMultiItemArr[8].m_mthSetPrintValue(
				new string[]{"","","һ�����>>ͷ��>>����ƫ��>>��","һ�����>>ͷ��>>����ƫ��>>��","","һ�����>>ͷ��>>��״���״�>>��","һ�����>>ͷ��>>��״���״�>>��","","һ�����>>ͷ��>����ŭ��>>��","һ�����>>ͷ��>����ŭ��>>��"},																
				new string[]{"������","����ƫ�ƣ�","#��","#��","��״���״�","#��","#��","����ŭ�ţ�","#��","#��"});

			#endregion
			#region �ز�
			m_objPrintMultiItemArr[9].m_mthSetPrintValue(
				new string[]{"","һ�����>>�ز�>>�ز����Σ���","һ�����>>�ز�>>�ز����Σ���","һ�����>>�ز�>>������������","һ�����>>�ز�>>���������ֲ�","һ�����>>�ز�>>��������������"
								,"һ�����>>�ز�>>��������ʪ����"},																
			
				new string[]{"�ز���","#�ز����Σ��У�","#�ز����Σ��ޣ�","#������������","#���������ֲ�","#��������������","#��������ʪ����"});
			m_objPrintMultiItemArr[9].m_mthSetPrintValue(
				new string[]{"һ�����>>�ز�>>����","","һ�����>>�ز�>>���ɣ���","һ�����>>�ز�>>���ɣ�����","һ�����>>�ز�>>������ǿ","һ�����>>�ز�>>��������","һ�����>>�ز�>>��������"},																
				new string[]{"\n            ���ʣ�","��/�֣�$$","#���ɣ��룻","#���ɣ����룻","#������ǿ","#��������","#��������"});
			m_objPrintMultiItemArr[9].m_mthSetPrintValue(
				new string[]{"һ�����>>�ز�>>��������","һ�����>>�ز�>>��������","һ�����>>�ز�>>����>>��λ������","һ�����>>�ز�>>����>>��λ����������","һ�����>>�ز�>>����>>ʱ�ࣺ������","һ�����>>�ز�>>����>>ʱ�ࣺ������","һ�����>>�ز�>>��ϸ"},																
				new string[]{"#���������У�","#�������ޣ�","#��λ�����ࣻ","#��λ������������","#ʱ�ࣺ������","#ʱ�ࣺ������",""});

			#endregion
            #region ����
			m_objPrintMultiItemArr[10].m_mthSetPrintValue(
				new string[]{"","","һ�����>>����>>����ѹʹ:��","һ�����>>����>>����ѹʹ:��","","һ�����>>����>>����:��","һ�����>>����>>����:��"},	
																							
				new string[]{"������","����ѹʹ��","#��","#��","���飺","#��","#��"});
			m_objPrintMultiItemArr[10].m_mthSetPrintValue(
				new string[]{"","һ�����>>����ѹʹ:��","һ�����>>����ѹʹ:��","","һ�����>>����>>������:��","һ�����>>����>>������:��"},	
																							
				new string[]{"������ѹʹ��","#��","#��","��������","#��","#��"});
			#endregion

			#region ������ֳ��
			m_objPrintMultiItemArr[11].m_mthSetPrintValue(
				new string[]{"","һ�����>>������ֳ��:δ��","һ�����>>������ֳ��:���쳣"},	
																							
				new string[]{"������ֳ����","#δ��","#���쳣"});

			#endregion
			#region ������֫
			m_objPrintMultiItemArr[12].m_mthSetPrintValue(
				new string[]{"","","һ�����>>������֫>>����:��","һ�����>>������֫>>����:��","","һ�����>>������֫>>�ؽں���:��","һ�����>>������֫>>�ؽں���:��","","һ�����>>������֫>>�ؽڻ:����","һ�����>>������֫>>�ؽڻ:����"},	
																							
				new string[]{"������֫��","���Σ�","#��","#��","�ؽں��ף�","#��","#��","�ؽڻ��","#����","#����"});
			m_objPrintMultiItemArr[12].m_mthSetPrintValue(
				new string[]{"","һ�����>>������֫>>��ؽڻ��:��","һ�����>>������֫>>��ؽڻ��:��","һ�����>>������֫>>ǰ��","","һ�����>>������֫>>��չ",""},	
																							
				new string[]{"\n            ��ؽڻ�ȣ�","#��","#��","ǰ����","�ȣ�$$","��չ��","�ȣ�$$"});
			m_objPrintMultiItemArr[12].m_mthSetPrintValue(
				new string[]{"һ�����>>������֫>>����","","һ�����>>������֫>>����","","һ�����>>������֫>>����","","һ�����>>������֫>>����","","һ�����>>������֫>>��ؽڻ��>>��ϸ"},	
																							
				new string[]{"\n            ���գ�","�ȣ�$$","���죺","�ȣ�$$","������","�ȣ�$$","������","�ȣ�$$",""});

			#endregion

			#region Ѫ�����
			m_objPrintMultiItemArr[13].m_mthSetPrintValue(
				new string[]{"","","һ�����>>Ѫ�����>>Ѫ����:��","һ�����>>Ѫ�����>>Ѫ����:��","","һ�����>>Ѫ�����>>����:��","һ�����>>Ѫ�����>>����:��","","һ�����>>Ѫ�����>>ǹ����:��","һ�����>>Ѫ�����>>ǹ����:��"},	
																							
				new string[]{"Ѫ�������","Ѫ���壺","#��","#��","������","#��","#��","ǹ������","#��","#��"});

			#endregion

			#region ��ʶ״̬
            m_objPrintMultiItemArr[14].m_mthSetSpecialTitleValue("��ϵͳ���");
			m_objPrintMultiItemArr[14].m_mthSetPrintValue(
				new string[]{"","��ϵͳ>>­��>>��ʶ״̬:���","��ϵͳ>>­��>>��ʶ״̬:ģ��","��ϵͳ>>­��>>��ʶ״̬:��˯","��ϵͳ>>­��>>��ʶ״̬:����","��ϵͳ>>­��>>��ʶ(����˹��Ƿ�)"},	
																							
				new string[]{"��ʶ״̬��","#���","#ģ��","#��˯","#����","��ʶ:(����˹���ܷ�)��"});

			#endregion
			#region ���Թ���
			m_objPrintMultiItemArr[15].m_mthSetPrintValue(
				new string[]{"","��ϵͳ>>­��>>���Թ���>>�˶���ʧ���","��ϵͳ>>­��>>���Թ���>>�˶���ʧ���","��ϵͳ>>­��>>���Թ���>>�о���ʧ���","��ϵͳ>>­��>>���Թ���>>�о���ʧ���","��ϵͳ>>­��>>���Թ���>>�����ʧ���","��ϵͳ>>­��>>���Թ���>>�����ʧ���"},	
																							
				new string[]{"���Թ��ܣ�","#�˶���ʧ��У�","#�˶���ʧ��ޣ�","#�о���ʧ��У�","#�о���ʧ��ޣ�","#�����ʧ���","�����ʧ���"});

			#endregion

			#region ­��
			m_objPrintMultiItemArr[16].m_mthSetPrintValue(
				new string[]{"","","­��>>���>>��","­��>>���>>��"},	
																							
				new string[]{"­�񾭣�","��.�����","��","�ң�"});
			m_objPrintMultiItemArr[16].m_mthSetPrintValue(
				new string[]{"","����>>����>>��","����>>����>>��","","����>>��Ұ>>��","����>>��Ұ>>��","����>>�۵�"},	
																							
				new string[]{"\n            ��.������","��","�ң�","��Ұ��","��","�ң�","�۵ף�"});
			m_objPrintMultiItemArr[16].m_mthSetPrintValue(
				new string[]{"","����>>���´�","����>>����"},	
																							
				new string[]{"\n            ��.��.��. ���´���","","���ѣ�"});
			m_objPrintMultiItemArr[16].m_mthSetPrintValue(
				new string[]{"","����>>����>>λ��","����>>����>>�˶�","����>>����>>����","����>>����>>���"},	
																							
				new string[]{"\n            ����","λ�ã�","�˶���","���ӣ�","�����"});
			m_objPrintMultiItemArr[16].m_mthSetPrintValue(
				new string[]{"","����>>ͫ��>>��С","����>>ͫ��>>��״","����>>ͫ��>>ֱ�ӹⷴ��","����>>ͫ��>>��ӹⷴ��"},	
																							
				new string[]{"\n            ͫ�ף�","��С��","��״��","ֱ�ӹⷴ�䣺","��ӹⷴ�䣺"});
			m_objPrintMultiItemArr[17].m_mthSetPrintValue(
				new string[]{"","����>>�沿�о�","����>>�沿�о�>>������ʹ"},	
																							
				new string[]{"            ��.","�沿�о���","������ʹ��"});
			m_objPrintMultiItemArr[17].m_mthSetPrintValue(
				new string[]{"����>>�沿�о�>>��Ĥ����","����>>�沿�о�>>��Ĥ����>>ֱ��","����>>�沿�о�>>��Ĥ����>>���"},	
																							
				new string[]{"\n            ��Ĥ���䣺","ֱ�ӣ���","��ӣ�"});
			m_objPrintMultiItemArr[17].m_mthSetPrintValue(
				new string[]{"����>>�沿�о�>>����˶�","����>>�沿�о�>>����˶�>>����","����>>�沿�о�>>����˶�>>򨼡"},	
																							
				new string[]{"            ����˶���","������","򨼡��"});
			m_objPrintMultiItemArr[18].m_mthSetPrintValue(
				new string[]{"","����>>��ü","����>>��Ŀ","����>>ʾ��","����>>����","����>>����"},	
																							
				new string[]{"            ��.","��","��Ŀ��","ʾ�ݣ�","������","���죺"});
			m_objPrintMultiItemArr[18].m_mthSetPrintValue(
				new string[]{"","����>>����������","����>>����������","����>>��ѣ����","����>>��ѣ����","����>>��������","����>>��������"},	
																							
				new string[]{"\n            ��.","#������������","#���������ˣ�","#��ѣ���У�","#��ѣ���ޣ�","#��������","#��������"});
			m_objPrintMultiItemArr[18].m_mthSetPrintValue(
				new string[]{"","����>>����������","����>>�����������ϰ�","����>>���ʣ�����","����>>���ʣ�����","����>>���ʣ�����","����>>�����˶�","����>>�ʷ���"},	
																							
				new string[]{"\n            ��.��.","#������������","#�����������ϰ���","#���ʣ�������","#���ʣ����ԣ�","#���ʣ�����","�����˶���","�ʷ��䣺"});
			m_objPrintMultiItemArr[19].m_mthSetPrintValue(
				new string[]{"","����>>������ͻ","","����>>��ή��:��","����>>��ή��:��","����>>б��������","","����>>б����>>��ή��:��","����>>б����>>��ή��:��"},	
																							
				new string[]{"            ��.","������ͻ��������","��ή����","#��","#��","б����������","��ή����","#��","#��"});
			m_objPrintMultiItemArr[19].m_mthSetPrintValue(
				new string[]{"","����>>б����>>���˶�","","����>>��>>��ή��:��","����>>��>>��ή��:��","","����>>�༡�˲�:��","����>>�༡�˲�:��"},	
																							
				new string[]{"\n            ��.","���˶���","�༡ή����","#��","#��","�༡�˲���","#��","#��"});
			#region �˶�����
			m_objPrintMultiItemArr[20].m_mthSetPrintValue(
				new string[]{"","�˶���>>˫������","","�˶���>>���ϰ�:��","�˶���>>���ϰ�:��","�˶���>>�����˶�","","�˶���>>�����˶�>>��֫:��","�˶���>>�����˶�>>��֫:��","","�˶���>>�����˶�>>��֫:��","�˶���>>�����˶�>>��֫:��"},	
																							
				new string[]{"�˶����ܣ�","˫��������","���ϰ���","#��","#��","�����˶���","��֫��","#��","#��","��֫��","#��","#��"});
			m_objPrintMultiItemArr[20].m_mthSetPrintValue(
				new string[]{"�˶���>>��ͬ�˶�","","�˶���>>��ͬ�˶�>>��֫:��","�˶���>>��ͬ�˶�>>��֫:��","","�˶���>>��ͬ�˶�>>��֫:��","�˶���>>��ͬ�˶�>>��֫:��"},	
																							
				new string[]{"\n            ��ͬ�˶���","��֫��","#��","#��","��֫��","#��","#��"});
			
			m_objPrintMultiItemArr[20].m_mthSetPrintValue(
				new string[]{"�˶���>>Brunnstrom�ּ�","�˶���>>Brunnstrom�ּ�>>�ϱ�","","�˶���>>Brunnstrom�ּ�>>��"},	
																							
				new string[]{"\n            Brunnstrom�ּ���","�ϱۣ�","����$$",""});
			m_objPrintMultiItemArr[20].m_mthSetPrintValue(
				new string[]{"�˶���>>Brunnstrom�ּ�>>��>>��","","�˶���>>Brunnstrom�ּ�>>��֫",""},	
																							
				new string[]{"�֣�","����$$","��֫��","����$$"});
			m_objPrintMultiItemArr[21].m_mthSetPrintValue(
				new string[]{"","�˶���>>��֫��������","","�˶���>>��֫�����켡","","�˶���>>��֫Զ������","","�˶���>>��֫Զ���켡",""},	
																							
				new string[]{"������","��֫����������","����$$","�����켡��","����$$","Զ��������","����$$","Զ���켡��","����$$"});
			m_objPrintMultiItemArr[21].m_mthSetPrintValue(
				new string[]{"�˶���>>��֫��������","","�˶���>>��֫�����켡","","�˶���>>��֫Զ������","","�˶���>>��֫Զ���켡",""},	
																							
				new string[]{"\n            ��֫����������","����$$","�����켡��","����$$","Զ��������","����$$","Զ���켡��","����$$"});
			
			m_objPrintMultiItemArr[22].m_mthSetPrintValue(
				new string[]{"","","�˶���>>������>>����֫:����","�˶���>>������>>����֫:����","�˶���>>������>>����֫:����"},	
																							
				new string[]{"��������","����֫��","#����","#����","#����"});
			m_objPrintMultiItemArr[22].m_mthSetPrintValue(
				new string[]{"","�˶���>>������>>����֫:����","�˶���>>������>>����֫:����","�˶���>>������>>����֫:����"},	
																							
				new string[]{"������֫��","#����","#����","#����"});
			m_objPrintMultiItemArr[22].m_mthSetPrintValue(
				new string[]{"","�˶���>>������>>����֫:����","�˶���>>������>>����֫:����","�˶���>>������>>����֫:����"},	
																							
				new string[]{"������֫��","#����","#����","#����"});
			m_objPrintMultiItemArr[22].m_mthSetPrintValue(
				new string[]{"","�˶���>>������>>����֫:����","�˶���>>������>>����֫:����","�˶���>>������>>����֫:����"},	
																							
				new string[]{"������֫��","#����","#����","#����"});
			m_objPrintMultiItemArr[23].m_mthSetPrintValue(
				new string[]{"","�˶���>>�����˶�����̱����>>��ָ����","�˶���>>�����˶�����̱����>>ֺ��������","�˶���>>�����˶�����̱����>>ƽ������"},	
																							
				new string[]{"�����˶�����̱���飺","\n            ��ָ���飺","ֺ�������飺","ƽ�����飺"});
			m_objPrintMultiItemArr[23].m_mthSetPrintValue(
				new string[]{"�˶���>>�����˶�����̱����>>Barre��I����","�˶���>>�����˶�����̱����>>Barre��II����","�˶���>>�����˶�����̱����>>Mingazini����"},	
																							
				new string[]{"\n            Barre��I���飺","Barre��II���飺","Mingazini���飺"});
			m_objPrintMultiItemArr[23].m_mthSetPrintValue(
				new string[]{"�˶���>>�����˶�����̱����>>ָ������","�˶���>>�����˶�����̱����>>��ϥ������","�˶���>>�����˶�����̱����>>Romberg��֢"},	
																							
				new string[]{"\n            ָ�����飺","��ϥ�����飺","Romberg��֢��"});
			m_objPrintMultiItemArr[24].m_mthSetPrintValue(
				new string[]{"�˶���>>��̬","�˶���>>��̬>>�ۺϹ���","�˶���>>��̬>>ADL Barthelָ��"},	
																							
				new string[]{"��̬��","\n            �ۺϹ��ܣ�","ADL Barthelָ���÷֣�"});
			
			#endregion
			#region �о�����
			m_objPrintMultiItemArr[25].m_mthSetPrintValue(
				new string[]{"","�о�����>>ǳ�о�","�о�����>>��о�","�о�����>>Ƥ���"},	
																							
				new string[]{"�о����ܣ�","\n            ǳ�о���","\n            ��о���","\n            Ƥ�����"});
			m_objPrintMultiItemArr[25].m_mthSetPrintValue(
				new string[]{"�о�����>>�񾭸�ǣ��ʹ","�о�����>>�񾭸�ѹʹ"},	
																							
				new string[]{"\n            �񾭸�ǣ��ʹ��","\n            �񾭸�ѹʹ��"});
			#endregion
			#region ����
			m_objPrintMultiItemArr[26].m_mthSetPrintValue(
				new string[]{"","��ϵͳ>>����>>������>>���ڷ���","��ϵͳ>>����>>������>>���Ĥ����"},	
																							
				new string[]{"���䣺�����䣨��ʧ��-��������+���³���++��������+++������=++++��","\n            ���ڷ��䣺","���Ĥ���䣺"});
			m_objPrintMultiItemArr[26].m_mthSetPrintValue(
				new string[]{"��ϵͳ>>����>>������>>�Ŷ�ͷ��","��ϵͳ>>����>>������>>����ͷ��"},	
																							
				new string[]{"\n            �Ŷ�ͷ�����䣺","����ͷ�����䣺"});
			m_objPrintMultiItemArr[26].m_mthSetPrintValue(
				new string[]{"��ϵͳ>>����>>������>>ϥ����","��ϵͳ>>����>>������>>�׷���"},	
																							
				new string[]{"\n            ϥ���䣺","�׷��䣺"});

			m_objPrintMultiItemArr[26].m_mthSetPrintValue(
				new string[]{"","��ϵͳ>>����>>������>>��˱����","��ϵͳ>>����>>������>>������򢷴��"},	
																							
				new string[]{"\n�����䣺","\n            ��˱���䣺","������򢷴�䣺"});
			m_objPrintMultiItemArr[26].m_mthSetPrintValue(
				new string[]{"��ϵͳ>>����>>������>>Hoffmann","��ϵͳ>>����>>������>>Babinaskin"},	
																							
				new string[]{"\n            Hoffmann������","Babinaskin������"});
		
			#endregion

			#region ֲ����
			m_objPrintMultiItemArr[27].m_mthSetPrintValue(
				new string[]{"","","��ϵͳ>>ֲ����>>�쳣����:��","��ϵͳ>>ֲ����>>�쳣����:��"},	
																							
				new string[]{"ֲ���񾭣�","�쳣������","#�У�","#�ޣ�"});
			m_objPrintMultiItemArr[27].m_mthSetPrintValue(
				new string[]{"","��ϵͳ>>ֲ����>>��㹦��:����","��ϵͳ>>ֲ����>>��㹦��:����","��ϵͳ>>ֲ����>>��㹦��:ʧ��",
							"","��ϵͳ>>ֲ����>>С�㹦��:����","��ϵͳ>>ֲ����>>С�㹦��:����","��ϵͳ>>ֲ����>>С�㹦��:ʧ��"},	
																							
				new string[]{"��㹦�ܣ�","#����","#����","#ʧ��",
                             "С�㹦�ܣ�","#����","#����","#ʧ��"});
		
			#endregion
			#region ��Ĥ�̼�֢
			m_objPrintMultiItemArr[28].m_mthSetPrintValue(
				new string[]{"","","��ϵͳ>>��Ĥ�̼�֢>>��ǿ:��","��ϵͳ>>��Ĥ�̼�֢>>��ǿ:��"},	
																							
				new string[]{"��Ĥ�̼�֢��","��ǿ��","#��","#��"});
			m_objPrintMultiItemArr[28].m_mthSetPrintValue(
				new string[]{"��ϵͳ>>��Ĥ�̼�֢>>��ǿ:�����ع�","��ϵͳ>>��Ĥ�̼�֢>>��ǿ:��ָ"},	
																							
				new string[]{"�������عǣ�","��ָ��"});
			m_objPrintMultiItemArr[28].m_mthSetPrintValue(
				new string[]{"","��ϵͳ>>��Ĥ�̼�֢>>Kerning��֢:����","��ϵͳ>>��Ĥ�̼�֢>>Kerning��֢:����",
			                  "","��ϵͳ>>��Ĥ�̼�֢>>Brubzinski:����","��ϵͳ>>��Ĥ�̼�֢>>Brubzinski:����"},	
																							
				new string[]{"��Kerning��֢��","#����","#����","Brubzinski��","#����","#����"});
			#endregion
			#region С��
			m_objPrintMultiItemArr[29].m_mthSetPrintValue(
				new string[]{"���>>����CT����������","���>>С��"},	
																							
				new string[]{"����CT���������ϣ�","\nС�᣺"});
			#endregion
			
			#region ���
			m_objPrintMultiItemArr[30].m_mthSetSpecialTitleValue("���");
			m_objPrintMultiItemArr[30].m_mthSetPrintValue(
				new string[]{"���>>��λ","���>>����"},	
																							
				new string[]{"\n��λ��","\n���ԣ�"});
			m_objPrintMultiItemArr[30].m_mthSetPrintValue(
				new string[]{"���>>�������","���>>��Ժ���"},	
																							
				new string[]{"\n������ϣ�","\n��Ժ��ϣ�"});
			#endregion

			#region ǩ��������
			m_objPrintSignArr[0].m_mthSetPrintSignValue(new string[]{"ҽʦǩ��"},new string[]{"ҽʦǩ����"});
			#endregion

            #endregion
            #region ����/��������Լ�ǩ��
            m_objPrintOneItemArr[6].m_mthSetPrintValue("�������", "������ϣ�");
            m_objPrintOneItemArr[7].m_mthSetPrintValue("�������", "������ϣ�");
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
						m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent==null ? "" : m_objItemContent.m_strItemContent)  ,(m_objItemContent==null ? "<root />" : m_objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,m_objItemContent != null);
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
		#region ��Ժ����---��Ժ���
		/// <summary>
		/// ��Ժ����---��Ժ���
		/// </summary>
		private class clsPrintInHospitalMarks : clsIMR_PrintLineBase
		{
//			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
//		//	private clsInpatMedRec_Item[] objItemContent1;
//		    bool m_blnIsFirstPrint=true;
//			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
//			{
//				if(m_objContent == null|| m_objContent.m_objItemContents == null)
//				{
//					m_blnHaveMoreLine = false;
//					return;
//				}
//			//	objItemContent1 = m_objGetContentFromItemArr(new string[]{"��Ժʱ��"});
//
//				//				if(m_blnHavePrintInfo(m_strKeysArr1) == false )
//				//				{
//				//					m_blnHaveMoreLine = false;
//				//					return;
//				//				}
//				if(m_blnIsFirstPrint)
//				{
//					string strAllText = "";
//					string strXml = "";
//					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
//				
//					
//						//p_objGrp.DrawString("��Ժ���ڣ�"+ (objItemContent1[0] == null ? "" :DateTime.Parse( objItemContent1[0].m_strItemContent).ToString("yyyy��MM��dd��HHʱmm��")),p_fntNormalText,Brushes.Black,m_intRecBaseX+15,p_intPosY);
//						//p_intPosY+=20;
//					
//					
//					if(m_objContent!=null)
//					{
//						#region ��ʱ���--��ȥ��ʷ
//						m_mthMakeText(new string[]{"\n��","��סԺ$$"},new string[]{"�ڼ���סԺ",""},ref strAllText,ref strXml);
//
//						m_mthMakeText(new string[]{"\n��ʱ��ϣ�"},new string[]{"�������>>��ʱ���"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"\n�����ϣ�"},new string[]{"�������>>������"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"\n����֢��"},new string[]{"�������>>����֢"},ref strAllText,ref strXml);
//						
//						m_mthMakeText(new string[]{"\n��Ժ���ߣ�"},new string[]{"�������>>����"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"\n��������飺"},new string[]{"�������>>���������"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"\n��ȥ��ʷ(���Դ�̣����Դ�o,���Կ��ڿ�������ϸ��д)��"},new string[]{""},ref strAllText,ref strXml);
//						m_mthMakeCheckText(new string []{"\n                   ƽ�ؽ�����","��ʷ>>ƽ�ؽ���>>����","��ʷ>>ƽ�ؽ���>>һ��","��ʷ>>ƽ�ؽ���>>�ϲ�"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"��Ⱦ��ʷ������"},new string[]{"��ʷ>>����"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"���","�˺�","����","Ѫ���没"},new string[]{"��ʷ>>���","��ʷ>>�˺�","��ʷ>>����","��ʷ>>Ѫ���没"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"\n                   ����ʷ��"},new string[]{""},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{""},new string[]{"��ʷ>>����ʷ"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"ҩ�����","���Կ���","��Ѫ","����","�ļ�"},new string[]{"��ʷ>>ҩ�����","��ʷ>>���Կ���","��ʷ>>��Ѫ","��ʷ>>����","��ʷ>>�ļ�"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"\n                   �Ͷ��󼱴�"},new string[]{"��ʷ>>�Ͷ��󼱴�"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"��Ѫѹ","θʹ","ŻѪ","�ڱ�","����"},new string[]{"��ʷ>>��Ѫѹ","��ʷ>>θʹ","��ʷ>>ŻѪ","��ʷ>>�ڱ�","��ʷ>>����"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"\n                   ��������"},new string[]{"��ʷ>>��������"},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"Ѫ��","�沿����","����","Ƥ�³�Ѫ","����"},new string[]{"��ʷ>>Ѫ��","��ʷ>>�沿����","��ʷ>>����","��ʷ>>Ƥ�³�Ѫ","��ʷ>>����"},ref strAllText,ref strXml);
//						
//
//						
//					
//						
//						#endregion
//
//
//					}
//					else
//					{
//						m_blnHaveMoreLine = false;
//						return;
//					}
//					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
//					//m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
//					m_blnIsFirstPrint = false;					
//				}
//
//				if(m_objPrintContext.m_BlnHaveNextLine())
//				{
//					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);
//					p_intPosY += 20;
//				}
//				if(m_objPrintContext.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//				}
//			}
//
//			public override void m_mthReset()
//			{
//				m_objPrintContext.m_mthRestartPrint();
//				m_blnHaveMoreLine = true;
//				m_blnIsFirstPrint = true;
//			}
		}
		#endregion
	}
}
