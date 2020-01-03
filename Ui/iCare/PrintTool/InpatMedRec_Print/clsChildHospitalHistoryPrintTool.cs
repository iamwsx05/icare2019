using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// clsChildHospitalHistoryPrintTool ��ժҪ˵����
	/// </summary>
	public class clsChildHospitalHistoryPrintTool: clsInpatMedRecPrintBase
	{
		public clsChildHospitalHistoryPrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		   new clsPrintPatientFixInfo(),
																		   new clsPrintChildHospitalHistory(),	
					                                                       new clsSignNameAndDate()													  
																		
																	   });			
		}
		#region ��Ժ���---���Ƽƻ�
		/// <summary>
		/// ��Ժ���---���Ƽƻ�
		/// </summary>
		private class clsPrintChildHospitalHistory : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			
			//	private string[] m_strKeysArr1 = {"��������","��������>>��"};
//			private string[] m_strKeysArr2 = {"��","��ʩ��������>>�����黯������ժ����"};
//			private string[] m_strKeysArr02 = {" �����黯������ժ����"};
			#region ����CheckBox�Ĵ�ӡ�ı�
			/// <summary>
			/// ����CheckBox�Ĵ�ӡ�ı�
			/// </summary>
			/// <param name="p_hasItem">��ϣ��</param>
			/// <param name="p_strName">�������飬�����һ��Ϊ��ʶ���ӵڶ��ʼ���Ǽ�</param>
			/// <param name="p_strTextAll">�����ı�</param>
			/// <param name="p_strTextXML">XML�ı�</param>
			protected  void m_mthMakeCheckText(string[] p_strName,ref string p_strTextAll, ref string p_strTextXML)
			{
				if(m_hasItems == null || m_hasItems.Count < 1 || p_strName == null)
					return;
				bool blnPrintFirst = false;
				string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
				string strDH_XML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("��",m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate);
				p_strTextAll += p_strName[0];
				string strXML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[0],m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate);
				if(p_strTextXML != "")
					p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[]{p_strTextXML,strXML});
				else
					p_strTextXML = strXML;
				for(int i =1; i < p_strName.Length; i++)
				{
					if(m_hasItems.Contains(p_strName[i]) == true)
					{
						int index = p_strName[i].LastIndexOf(">");
						string strText = p_strName[i];
						if (index > 0)
							strText = p_strName[i].Substring(index+1);
						p_strTextAll += (blnPrintFirst == true ? "��" : "") + strText;
						p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[]{p_strTextXML,(blnPrintFirst == true ? strDH_XML : "<root />"),ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText,m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate)});
						blnPrintFirst = true;
					}
				}
				//p_strTextAll += "��";
				p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[]{p_strTextXML,ctlRichTextBox.clsXmlTool.s_strMakeTextXml("��",m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate)});
			}	
			#endregion 
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				//				if(m_blnHavePrintInfo(m_strKeysArr1) == false )
				//				{
				//					m_blnHaveMoreLine = false;
				//					return;
				//				}
				if(m_blnIsFirstPrint)
				{
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						#region ��Ժ���--����ʷ��������
						m_mthMakeText(new string[]{"��Ժ��ϣ�"},new string[]{"��Ժ���"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��Ժ��ϣ�"},new string[]{"��Ժ���"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��������"},new string[]{"������"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"\n��Ժ״����","��Ժ״��>>��","��Ժ״��>>��ת","��Ժ״��>>�ޱ仯","��Ժ״��>>��","��Ժ״��>>δ����","��Ժ״��>>�Զ���Ժ","��Ժ״��>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n���ߣ�"},new string[]{"����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n�ֲ�ʷ��"},new string[]{"�ֲ�ʷ"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��ȥʷ��"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"ƽʱ���������","��ȥʷ>>ƽʱ�������>>һ��","��ȥʷ>>ƽʱ�������>>����","��ȥʷ>>ƽʱ�������>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"����","��ȥʷ>>��Ⱦ��ʷ>>��","��ȥʷ>>�޴�Ⱦ��ʷ"},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string []{"��ȥʷ>>��Ⱦ��ʷ>>��"}) != false)
						m_mthMakeText(new string[]{"","��Ⱦ��ʷ$$"},new string[]{"��ȥʷ>>��Ⱦ��ʷ",""},ref strAllText,ref strXml);
						
						m_mthMakeCheckText(new string []{"��","��ȥʷ>>�����ˡ����Ⱦ���ʷ","��ȥʷ>>�����ˡ����Ⱦ���ʷ"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��","��ȥʷ>>������>>��","��ȥʷ>>�޹�����"},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string []{"��ȥʷ>>������>>��"}) != false)						
						m_mthMakeText(new string[]{"","������$$"},new string[]{"��ȥʷ>>������",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��������"},new string[]{"��ȥʷ>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n����ʷ����","̥��$$"},new string[]{"����ʷ>>̥",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��","����$$"},new string[]{"����ʷ>>��",""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"","����ʷ>>����","����ʷ>>���","����ʷ>>�Ѳ�","����ʷ>>�½�����","����ʷ>>�ɽ�����"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"������ʱ","����ʷ>>����Ϣ","����ʷ>>����Ϣ"},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string []{"����ʷ>>����Ϣ"}) != false)						
							m_mthMakeCheckText(new string []{"","����ʷ>>��Ϣ��","����ʷ>>��Ϣ��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��","����ʷ>>�в���","����ʷ>>�޲���"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��","����ʷ>>̥Ĥ����","����ʷ>>��̥Ĥ����"},ref strAllText,ref strXml);
						
						if(m_blnHavePrintInfo(new string []{"����ʷ>>̥Ĥ����"}) != false)
							m_mthMakeText(new string[]{"","Сʱ$$"},new string[]{"����ʷ>>̥Ĥ����>>Сʱ",""},ref strAllText,ref strXml);

						m_mthMakeCheckText(new string []{"��","����ʷ>>��ˮ����","����ʷ>>����ˮ����"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��","����ʷ>>����ƾ�","����ʷ>>������ƾ�"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��","����ʷ>>�л���","����ʷ>>�޻���"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��","����ʷ>>ĸ��","����ʷ>>�˹�ι��"},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"��","�»�����$$"},new string[]{"����ʷ>>�»���",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"","�����·��$$"},new string[]{"����ʷ>>�����·",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"","���˵����$$"},new string[]{"����ʷ>>���˵��",""},ref strAllText,ref strXml);
						
						m_mthMakeCheckText(new string []{"","����ʷ>>�Ⱥ�>>��","����ʷ>>�Ⱥ�>>���Ⱥ�"},ref strAllText,ref strXml);
						
						if(m_blnHavePrintInfo(new string []{"����ʷ>>�Ⱥ�>>��"}) != false)
							m_mthMakeText(new string[]{"","�Ⱥ�$$"},new string[]{"����ʷ>>�Ⱥ�",""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��ѧϰ�ɼ���","����ʷ>>ѧϰ�ɼ�>>��","����ʷ>>ѧϰ�ɼ�>>��","����ʷ>>ѧϰ�ɼ�>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��Ԥ�����������","Ԥ���������>>������","Ԥ���������>>С���������","Ԥ���������>>������","Ԥ���������>>����","Ԥ���������>>�Ҹ�����׸�����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n������"},new string[]{"Ԥ���������>>����"},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"\n����ʷ�����и���ĸ"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"","����ʷ>>���׻���>>�ǽ��׻���","����ʷ>>���׻���>>���ǽ��׻���"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"����"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"","�֡�$$"},new string[]{"����ʷ>>��",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"","�ܡ�$$"},new string[]{"����ʷ>>��",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"","�㡢$$"},new string[]{"����ʷ>>��",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"","�á�$$"},new string[]{"����ʷ>>��",""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"","����ʷ>>�Ŵ�ʷ>>���Ŵ�ʷ","����ʷ>>�Ŵ�ʷ>>���Ŵ�ʷ"},ref strAllText,ref strXml);
						
						if(m_blnHavePrintInfo(new string []{"����ʷ>>�Ŵ�ʷ>>���Ŵ�ʷ"}) != false)
							m_mthMakeText(new string[]{"��"},new string[]{"����ʷ>>�Ŵ�ʷ"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"\n����ʷ��","����ʷ>>������","����ʷ>>������"},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string []{"����ʷ>>������"}) != false)						
						m_mthMakeText(new string[]{"��"},new string[]{"����ʷ>>����"},ref strAllText,ref strXml);
						#endregion

						m_mthMakeText(new string[]{"\n                                                     ��  ��  ��  ��"},new string[]{""},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n���£�","�棻$$","������","��/�֣�$$"},new string[]{"�����>>����","","�����>>����",""},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"������","��/�֣�$$","Ѫѹ��","kpa��$$"},new string[]{"�����>>����","","�����>>Ѫѹ",""},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"���أ�","kg��$$","ͷΧ��","cm��$$","��:","cm��$$"},new string[]{"�����>>����","","�����>>ͷΧ","","�����>>��",""},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\nһ�������������","Ӫ����"},new string[]{"�����>>һ�����>>����","�����>>һ�����>>Ӫ��"},ref strAllText,ref strXml);					
						m_mthMakeCheckText(new string []{"����־��","�����>>��־>>����","�����>>��־>>��˯","�����>>��־>>�к�","�����>>��־>>հ��","�����>>��־>>��˯","�����>>��־>>����"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"������","�����>>����>>��","�����>>����>>����","�����>>����>>����","�����>>����>>��Į"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"�����ݣ�","��λ��"},new string[]{"�����>>����","�����>>��λ"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\nƤ��ճĤ��","\nƤ��֬����"},new string[]{"�����>>Ƥ��ճĤ","�����>>Ƥ��֬��"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n�б��ܰͽ᣺"},new string[]{"�����>>�б��ܰͽ�"},ref strAllText,ref strXml);					
						
						m_mthMakeText(new string[]{"\nͷ������ͷ­��","�Ƿ죺","ǰض��","��ض��"},new string[]{"�����>>ͷ����>>ͷ­","�����>>ͷ����>>�Ƿ�","�����>>ͷ����>>ǰض","�����>>ͷ����>>��ض"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n             �ۿ���","������","���Ĥ��","��Ĥ��","����"},new string[]{"�����>>ͷ����>>�ۿ�","�����>>ͷ����>>����","�����>>ͷ����>>���Ĥ","�����>>ͷ����>>��Ĥ","�����>>ͷ����>>����"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n             ͫ�ף�","�ⷴ�䣺"},new string[]{"�����>>ͷ����>>ͫ��","�����>>ͷ����>>�ⷴ��"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n             ����","��ͻ��","�ǣ�","���ܣ�","����","�ʣ�"},new string[]{"�����>>ͷ����>>��","�����>>ͷ����>>��ͻ","�����>>ͷ����>>��","�����>>ͷ����>>����","�����>>ͷ����>>��","�����>>ͷ����>>��"},ref strAllText,ref strXml);					

						m_mthMakeText(new string[]{"\n             ��ǻճĤ��","����","������","�����壺","�ࣺ"},new string[]{"�����>>ͷ����>>��ǻճĤ","�����>>ͷ����>>��","�����>>ͷ����>>����","�����>>ͷ����>>������","�����>>ͷ����>>��"},ref strAllText,ref strXml);					
						m_mthMakeCheckText(new string []{"\n             ����","�����>>ͷ����>>�еֿ�","�����>>ͷ����>>�޵ֿ�"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"�����ܣ�"},new string[]{"�����>>ͷ����>>����"},ref strAllText,ref strXml);					
						m_mthMakeCheckText(new string []{"����������","�����>>ͷ����>>��ˡ��","�����>>ͷ����>>��ˡ��"},ref strAllText,ref strXml);

						m_mthMakeCheckText(new string []{"���ξ�������������","�����>>ͷ����>>�ξ�����������>>����","�����>>ͷ����>>�ξ�����������>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��"},new string[]{"�����>>ͷ����>>�ξ�����������"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"����״�٣�"},new string[]{"�����>>ͷ����>>��״��"},ref strAllText,ref strXml);					
						
						m_mthMakeText(new string[]{"\n�ز���������"},new string[]{""},ref strAllText,ref strXml);					
						m_mthMakeCheckText(new string []{"","�ز�>>����>>����","�ز�>>����>>����","�ز�>>����>>©����","�ز�>>����>>���Ϲ�","�ز�>>����>>в����","�ز�>>����>>в�ⷭ"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"     ������"},new string[]{"�ز�>>����>>����"},ref strAllText,ref strXml);					
					
						m_mthMakeText(new string[]{"\n���ࣺ��������"},new string[]{"�����>>����>>��"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n             ��������"},new string[]{"�����>>����>>��"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n             ߵ������"},new string[]{"�����>>����>>ߵ"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n             ��������"},new string[]{"�����>>����>>��"},ref strAllText,ref strXml);					

						m_mthMakeText(new string[]{"\n���ࣺ��������"},new string[]{"�����>>����>>��"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n            ��������"},new string[]{"�����>>����>>��"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n            ߵ������"},new string[]{"�����>>����>>ߵ"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n            ��������"},new string[]{"�����>>����>>��"},ref strAllText,ref strXml);					
						
						m_mthMakeText(new string[]{"\n��������������"},new string[]{"�����>>����>>��"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n            ��������"},new string[]{"�����>>����>>��"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n            ߵ������"},new string[]{"�����>>����>>ߵ"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n            ��������"},new string[]{"�����>>����>>��"},ref strAllText,ref strXml);					
						
						m_mthMakeText(new string[]{"\n������֫��"},new string[]{"�����>>������֫"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n���ţ�����ֳ����"},new string[]{"�����>>����ֳ��"},ref strAllText,ref strXml);					

						m_mthMakeText(new string[]{"\n��ϵͳ��"},new string[]{""},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"��ʳ���䣺"},new string[]{"�����>>��ϵͳ>>��ʳ����"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"����˱���䣺"},new string[]{"�����>>��ϵͳ>>��˱����"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"���ճַ��䣺","ӵ�����䣺"},new string[]{"�����>>��ϵͳ>>�ճַ���","�����>>��ϵͳ>>ӵ������"},ref strAllText,ref strXml);					
						
						m_mthMakeText(new string[]{"\n��Ĥ���䣺"},new string[]{"�����>>��ϵͳ>>��Ĥ����"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"�����ڷ��䣺"},new string[]{"�����>>��ϵͳ>>���ڷ���"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"����غ���䣺","ϥ���䣺"},new string[]{"�����>>��ϵͳ>>��غ����","�����>>��ϵͳ>>ϥ����"},ref strAllText,ref strXml);					
					
						m_mthMakeText(new string[]{"\nBabinski's Sign��"},new string[]{""},ref strAllText,ref strXml);					
						m_mthMakeCheckText(new string []{"","�����>>��ϵͳ>>Babinski's Sign>>��","�����>>��ϵͳ>>Babinski's Sign>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��Oppenheim's Sign��","�����>>��ϵͳ>>Oppenheim's Sign>>��","�����>>��ϵͳ>>Oppenheim's Sign>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��Gordon's Sign��","�����>>��ϵͳ>>Gordon's Sign>>��","�����>>��ϵͳ>>Gordon's Sign>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��Chaddock's Sign��","�����>>��ϵͳ>>Chaddock's Sign>>��","�����>>��ϵͳ>>Chaddock's Sign>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��\nKeznig's Sign��","Brudzinskig's Sign��"},new string[]{"�����>>��ϵͳ>>Keznig's Sign","�����>>��ϵͳ>>Brudzinskig's Sign"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"��������"},new string[]{"�����>>��ϵͳ>>����"},ref strAllText,ref strXml);					

						m_mthMakeText(new string[]{"\nʵ���Ҽ�������飺"},new string[]{"�����>>��ϵͳ>>ʵ���Ҽ��������"},ref strAllText,ref strXml);					

						m_mthMakeText(new string[]{"\nժҪ��"},new string[]{"�����>>��ϵͳ>>ժҪ"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n������ݣ�"},new string[]{"�������"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n������ϣ�"},new string[]{"�������"},ref strAllText,ref strXml);					
						m_mthMakeText(new string[]{"\n���Ƽƻ���"},new string[]{"���Ƽƻ�"},ref strAllText,ref strXml);					

					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					//m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);
					p_intPosY += 20;
				}
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
				}
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
		#endregion
		#region ǩ�� �� ����
		/// <summary>
		/// ǩ�� �� ����
		/// </summary>
		private class clsSignNameAndDate : clsIMR_PrintLineBase
		{
			#region ���ô���
//			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
//
//			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
//			/// <summary>
//			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
//			/// </summary>
//			private bool m_blnIsFirstPrint = true;
//			private bool blnNextPage = true;
//			private string[] m_strKeysArr01 = {"ҽʦǩ��"};
//			private string[] m_strKeysArr101 = {"\n                                                                                        ҽʦǩ����"};
//	
//			private string[] m_strKeysArr02 = {"����"};
//			private string[] m_strKeysArr102 = {"                   �� �ڣ�"};		
//			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
//			{
//				if(m_objContent == null|| m_objContent.m_objItemContents == null)
//				{
//					m_blnHaveMoreLine = false;
//					return;
//				}
//				
//				//				if(blnNextPage)
//				//				{
//				//					//����һҳ��ӡ�����ô�ӡʱ���p_intPosY�Ƿ������ױߵ�ֵ���ж���ʵ��
//				//					m_blnHaveMoreLine = true;
//				//					blnNextPage = false;
//				//					p_intPosY += 1500;
//				//					return;
//				//				}
//				if(m_blnIsFirstPrint)
//				{
//					//					p_objGrp.DrawString("��ϵͳ���",m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
//					//					p_intPosY += 20;
//					//					p_objGrp.DrawString("һ�����",m_fontItemMidHead,Brushes.Black,m_intRecBaseX,p_intPosY);
//					//					p_intPosY += 20;
//					string strAllText = "";
//					string strXml = "";
//					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
//					if(m_objContent!=null)
//					{
//						if(m_blnHavePrintInfo(m_strKeysArr01) != false)
//							m_mthMakeText(m_strKeysArr101,m_strKeysArr01,ref strAllText,ref strXml);
//						else
//							m_mthMakeText(m_strKeysArr101,new string[]{""},ref strAllText,ref strXml);
//
//						if(m_blnHavePrintInfo(m_strKeysArr02) != false)
//							m_mthMakeText(m_strKeysArr102,m_strKeysArr02,ref strAllText,ref strXml);
//			
//			
//								
//					}
//					else
//					{
//						m_blnHaveMoreLine = false;
//						return;
//					}
//					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
//					m_mthAddSign2("ҽʦǩ��",m_objPrintContext.m_ObjModifyUserArr);
//					m_blnIsFirstPrint = false;					
//				}
//
//				//int intLine = 0;
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
          # 	endregion


			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private clsInpatMedRec_Item[] objItemContent;
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				objItemContent = m_objGetContentFromItemArr(new string[]{"ҽʦǩ��","����"});
				
//				if(objItemContent == null ||objItemContent[0] == null)
//				{
//					m_blnHaveMoreLine = false;
//					return;
//				}
				if(p_intPosY+20>clsPrintPosition.c_intBottomY-20)
				{
				  m_blnHaveMoreLine = true;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					if(objItemContent!=null)
					{
						p_objGrp.DrawString("ҽʦǩ����"+(objItemContent[0]==null ? "" : objItemContent[0].m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+260,p_intPosY);
						//p_intPosY += 20;
						p_objGrp.DrawString("       ���ڣ�"+ (objItemContent[1] == null ? "" :DateTime.Parse( objItemContent[1].m_strItemContent).ToString("yyyy��MM��dd��HHʱmm��")),p_fntNormalText,Brushes.Black,m_intRecBaseX+420,p_intPosY);
						m_blnIsFirstPrint = false;
					}
				}

			//	int intLine = 0;
//				if(m_objPrintContext.m_BlnHaveNextLine())
//				{
//					m_objPrintContext.m_mthPrintLine(330,m_intRecBaseX+405,p_intPosY,p_objGrp);
//					p_intPosY += 20;
//					intLine++;
//				}			

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
		}
		#endregion
	}
}
