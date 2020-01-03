using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
namespace iCare
{
	/// <summary>
	/// ��������������֪��ͬ����  ��ӡ ��ժҪ˵����
	/// </summary>
	public class clsIMR_CataractSuffererApprovePrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_CataractSuffererApprovePrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		   new clsPrintPatientFixInfo("��������������֪��ͬ����",250),
																		   new clsPrintInPatCataractSuffererMain(),																	  
																		    new  clsPrintInPatMedDoctorAndDate(),
                                                                            new  clsPrintInPatMedDoctorAndDate1()
																		 //  new clsPrintInPatMedRecDiagnostic()
																	   });			
		}
        //protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        //{}
        //protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //}
		#region ��ӡ��һҳ�Ĺ̶�����
		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
        //internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
        //{
        //    private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
        //    {
        //        p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotItemHead ,Brushes.Black,340,40);
		
        //        p_objGrp.DrawString("��������������֪��ͬ����",new Font("SimSun", 16,FontStyle.Bold),Brushes.Black,280,70);
			
        //        //				p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,350,110);
        //        //				p_objGrp.DrawString("ĸ��סԺ�ţ�",p_fntNormalText,Brushes.Black,550,110);
        //        //p_objGrp.DrawString(m_objPrintInfo.m_strInPatientID ,p_fntNormalText,Brushes.Black,680,110);	
        //        p_intPosY =120;
        //        m_blnHaveMoreLine = false;
        //    }

        //    public override void m_mthReset()
        //    {
        //        m_objPrintContext.m_mthRestartPrint();

        //        m_blnHaveMoreLine = true;
        //    }
        //}

		#endregion
		#region ��ǰ���---���껼��
		/// <summary>
		/// ��ǰ���---���껼��
		/// </summary>
		private class clsPrintInPatCataractSuffererMain : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			
		//	private string[] m_strKeysArr1 = {"��������","��������>>��"};
			private string[] m_strKeysArr2 = {"��","��ʩ��������>>�����黯������ժ����"};
			private string[] m_strKeysArr02 = {" �����黯������ժ����"};

			private string[] m_strKeysArr3 = {"\n                                       ��","��ʩ��������>>�˹�����ֲ����"};
			private string[] m_strKeysArr03 = {"\n                                         �˹�����ֲ����"};
			
			private string[] m_strKeysArr4 = {"\n                                       ��","��ʩ��������>>�ִ����������ժ����"};
			private string[] m_strKeysArr04 = {"\n                                         �ִ����������ժ����"};
			
			private string[] m_strKeysArr5 = {"\n                                       ��","��ʩ��������>>�����ϳ�����"};
			private string[] m_strKeysArr05 = {"\n                                         �����ϳ�����"};
			
			private string[] m_strKeysArr6 = {"\n                                       ��","��ʩ��������>>���ڰ�����ժ����"};
			private string[] m_strKeysArr06 = {"\n                                         ���ڰ�����ժ����"};
			
			private string[] m_strKeysArr7 = {"\n                                       ��","��ʩ��������>>Ĥ�԰������г���"};
			private string[] m_strKeysArr07 = {"\n                                         Ĥ�԰������г���"};

			private string[] m_strKeysArr8 = {"\n    1���� ","���п��ܷ����Ĳ���֢���䴦��>>�������⣬��������ȣ�����ͣ����"};
			private string[] m_strKeysArr08 = {"\n    1��   �������⣬��������ȣ�����ͣ����"};
			private string[] m_strKeysArr9 = {"\n    2���� ","���п��ܷ����Ĳ���֢���䴦��>>��������Ĥ��Ѫ����ֹѪ���Ƴٻ���ͣ����"};
			private string[] m_strKeysArr09 = {"\n    2��   ��������Ĥ��Ѫ����ֹѪ���Ƴٻ���ͣ����"};
			private string[] m_strKeysArr10 = {"\n    3���� ","���п��ܷ����Ĳ���֢���䴦��>>����ֲ���˹�����"};
			private string[] m_strKeysArr010 = {"\n    3��   ����ֲ���˹�����"};
			private string[] m_strKeysArr11 = {"\n    4���� ","���п��ܷ����Ĳ���֢���䴦��>>��������֢"};
			private string[] m_strKeysArr011 = {"\n    4��   ��������֢"};
			private string[] m_strKeysArr12 = {"\n    1���� ","������ܷ����������Ȳ���֢>>���԰�����"};
			private string[] m_strKeysArr012 = {"\n    1��   ���԰�����"};
			private string[] m_strKeysArr13 = {"\n    2���� ","������ܷ����������Ȳ���֢>>�˹�������λ"};
			private string[] m_strKeysArr013 = {"\n    2��   �˹�������λ"};
			private string[] m_strKeysArr14 = {"\n    3���� ","������ܷ����������Ȳ���֢>>����Ĥ����������"};
			private string[] m_strKeysArr014 = {"\n    3��   ����Ĥ����������"};
			private string[] m_strKeysArr15 = {"\n    4���� ","������ܷ����������Ȳ���֢>>�����Ѫ��Ĥ��Ƥʧ����"};
			private string[] m_strKeysArr015 = {"\n    4��   �����Ѫ��Ĥ��Ƥʧ����"};
			private string[] m_strKeysArr16 = {"\n    5���� ","������ܷ����������Ȳ���֢>>�˿��ѿ�"};
			private string[] m_strKeysArr016 = {"\n    5��   �˿��ѿ�"};
			private string[] m_strKeysArr17 = {"\n    6���� ","������ܷ����������Ȳ���֢>>��������֢"};
			private string[] m_strKeysArr017 = {"\n    6��   ��������֢"};
			private string[] m_strKeysArr18 = {"\n    1���� ","�����������֮һ��>>���������Գ�Ѫ"};
			private string[] m_strKeysArr018 = {"\n    1��   ���������Գ�Ѫ"};
			private string[] m_strKeysArr19 = {"\n    2���� ","�����������֮һ��>>���ڻ�ŧ�Ը�Ⱦ"};
			private string[] m_strKeysArr019 = {"\n    2��   ���ڻ�ŧ�Ը�Ⱦ"};
			private string[] m_strKeysArr20 = {"\n    3���� ","�����������֮һ��>>�����Խ�Ĥ��"};
			private string[] m_strKeysArr020 = {"\n    3��   �����Խ�Ĥ��"};
			private string[] m_strKeysArr21 = {"\n    4���� ","�����������֮һ��>>�̷��������"};
			private string[] m_strKeysArr021 = {"\n    4��   �̷��������"};
			private string[] m_strKeysArr22 = {"\n    5���� ","�����������֮һ��>>����Ĥ����"};
			private string[] m_strKeysArr022 = {"\n    5��   ����Ĥ����"};
		

						
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
                        //m_mthMakeText(new string[]{"����������"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strPatientName)+"��","  �Ա�"+ m_objPrintInfo.m_strSex.Trim()+"��" ,"  ���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge)+"��","   ������"+m_objPrintInfo.m_strAreaName+"��"},
                        //    new string [] {"","","",""},ref strAllText,ref strXml);
						
                        ////m_mthMakeText(new string[]{"   ���䣺"},new string []{"(m_objPrintInfo==null ? : m_objPrintInfo.m_strAge)"},ref strAllText,ref strXml);
                        //m_mthMakeText(new string[]{"   ���ţ�"+m_objPrintInfo.m_strBedName+"��"},new string []{""},ref strAllText,ref strXml);
                        //m_mthMakeText(new string[]{"   סԺ�ţ�"+m_objPrintInfo.m_strInPatientID+""},new string []{""},ref strAllText,ref strXml);
						//						m_mthMakeText(new string[]{"������"},new string []{"m_objPrintInfo.m_strPatientName"},ref strAllText,ref strXml);
						//						m_mthMakeText(new string[]{"   ���䣺"},new string []{"(m_objPrintInfo==null ? : m_objPrintInfo.m_strAge)"},ref strAllText,ref strXml);
						//						m_mthMakeText(new string[]{"   ���ţ�"},new string []{"m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName"},ref strAllText,ref strXml);
						//						m_mthMakeText(new string[]{"   סԺ�ţ�"},new string []{"m_objPrintInfo.m_strInPatientID"},ref strAllText,ref strXml);
						//m_mthMakeText(new string[]{"\n��������:","    ��   $$"},m_strKeysArr1,ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"\n        ��ר��ҽ�������ŶԻ��߼���������ľ�����������ؽ����������ơ�ҽ�����й���������ܳ��ֲ���֢��ע����������˵�����£�"},new string[]{""},ref strAllText,ref strXml);
						#region ��ǰ��� 
						m_mthMakeText(new string[]{"\nһ��  ��ǰ��ϣ� ������"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                   ���ͣ�"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","��ǰ���>>������>>����>>������"}) != false)
						m_mthMakeCheckText(new string []{"��","��ǰ���>>������>>����>>������"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"�����ԣ�"},new string []{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","��ǰ���>>������>>����>>������"}) != false)
							m_mthMakeCheckText(new string []{" ��","��ǰ���>>������>>����>>������"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string []{"   �����ԣ�"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","��ǰ���>>������>>����>>������"}) != false)
							m_mthMakeCheckText(new string []{" ��","��ǰ���>>������>>����>>������"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string []{"   �����ԣ�"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","��ǰ���>>������>>����>>������"}) != false)
							m_mthMakeCheckText(new string []{" ��","��ǰ���>>������>>����>>������"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string []{"   �����ԣ�"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","��ǰ���>>������>>����>>����"}) != false)
							m_mthMakeCheckText(new string []{" ��","��ǰ���>>������>>����>>����"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string []{"  ������"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"��ǰ���>>������>>����>>����1"}) != false)
							m_mthMakeText(new string[]{""},new string[]{"��ǰ���>>������>>����>>����1"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"_________"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","��ǰ���>>������>>����>>�޾�����"}) != false)
							m_mthMakeCheckText(new string []{"����","��ǰ���>>������>>����>>�޾�����"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string []{"�� �޾�����"},new string[]{""},ref strAllText,ref strXml);
						#region �۱�
						m_mthMakeText(new string[]{"\n                   �۱�"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","��ǰ���>>������>>�۱�>>����"}) != false)
							m_mthMakeCheckText(new string []{"��","��ǰ���>>������>>�۱�>>����"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string []{"���ۣ�"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","��ǰ���>>������>>�۱�>>����"}) != false)
							m_mthMakeCheckText(new string []{" ��","��ǰ���>>������>>�۱�>>����"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string []{"   ���ۣ�"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","��ǰ���>>������>>�۱�>>˫��(ָ����������)"}) != false)
							m_mthMakeCheckText(new string []{" ��","��ǰ���>>������>>�۱�>>˫��(ָ����������)"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string []{"   ˫��(ָ����������)"},new string[]{""},ref strAllText,ref strXml);
						#endregion
						#endregion ��ǰ���
						
						#region ����ѡ��
						m_mthMakeText(new string[]{"\n����  ����ѡ��"},new string[]{""},ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(new string[]{"","����ѡ��>>����"}) != false)
							m_mthMakeCheckText(new string []{"��","����ѡ��>>����"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string []{" ���飻"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","����ѡ��>>��+����"}) != false)
							m_mthMakeCheckText(new string []{" ��","����ѡ��>>��+����"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string []{"   ��+���飻"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","����ѡ��>>����"}) != false)
							m_mthMakeCheckText(new string []{" ��","����ѡ��>>����"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string []{"   ������"},new string[]{""},ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(new string[]{"","����ѡ��>>����1"}) != false)
							m_mthMakeText(new string[]{""},new string[]{"����ѡ��>>����1"},ref strAllText,ref strXml);
                        else
							m_mthMakeText(new string[]{"_________"},new string[]{""},ref strAllText,ref strXml);

						#endregion
						//						m_mthMakeCheckText(new string []{"\n����  ��ʩ�������ƣ�",m_strKeysArr2,m_strKeysArr3,m_strKeysArr4,m_strKeysArr5,m_strKeysArr6,m_strKeysArr7},ref strAllText,ref strXml);
//						m_mthMakeCheckText(new string []{"\n�ġ�  ���п��ܷ����Ĳ���֢���䴦��",m_strKeysArr8},ref strAllText,ref strXml);
						#region ����  ��ʩ��������
						m_mthMakeText(new string []{"\n����  ��ʩ�������ƣ�"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr2) != false)
						    m_mthMakeCheckText(m_strKeysArr2,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr02,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr3) != false)
						    m_mthMakeCheckText(m_strKeysArr3,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr03,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr4) != false)
							m_mthMakeCheckText(m_strKeysArr4,ref strAllText,ref strXml);
						else
							m_mthMakeText(m_strKeysArr04,new string[]{""},ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr5) != false)
						    m_mthMakeCheckText(m_strKeysArr5,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr05,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr6) != false)
							m_mthMakeCheckText(m_strKeysArr6,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr06,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr7) != false)
							m_mthMakeCheckText(m_strKeysArr7,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr07,ref strAllText,ref strXml);
                        #endregion ����  ��ʩ��������
						#region �ġ�  ���п��ܷ����Ĳ���֢���䴦��
						m_mthMakeText(new string []{"\n�ġ�  ���п��ܷ����Ĳ���֢���䴦��"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr8) != false)
						    m_mthMakeCheckText(m_strKeysArr8,ref strAllText,ref strXml);
                        else
							m_mthMakeCheckText(m_strKeysArr08,ref strAllText,ref strXml);


						if(m_blnHavePrintInfo(m_strKeysArr9) != false)
						    m_mthMakeCheckText(m_strKeysArr9,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr09,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr10) != false)
						    m_mthMakeCheckText(m_strKeysArr10,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr010,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr11) != false)
						   m_mthMakeCheckText(m_strKeysArr11,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr011,ref strAllText,ref strXml);
						#endregion �ġ�  ���п��ܷ����Ĳ���֢���䴦��
						
						m_mthMakeText(new string[]{"\n�塢  ��������Ԥ�󣺾�����������������۵�����̫�����ֲ���֢���������������ָܻ�������"},new string[]{""},ref strAllText,ref strXml);
						#region ����  ������ܷ����������Ȳ���֢
						m_mthMakeText(new string[]{"\n����  ������ܷ����������Ȳ���֢��"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr12) != false)
							m_mthMakeCheckText(m_strKeysArr12,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr012,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr13) != false)
							m_mthMakeCheckText(m_strKeysArr13,ref strAllText,ref strXml);
						else
						    m_mthMakeCheckText(m_strKeysArr013,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr14) != false)
							m_mthMakeCheckText(m_strKeysArr14,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr014,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr15) != false)
							m_mthMakeCheckText(m_strKeysArr15,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr015,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr16) != false)
							m_mthMakeCheckText(m_strKeysArr16,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr016,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr17) != false)
							m_mthMakeCheckText(m_strKeysArr17,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr017,ref strAllText,ref strXml);

						#endregion ����  ������ܷ����������Ȳ���֢
						#region �ߡ�  �����������֮һ�ߣ����ܻ�����ʧ��������������ժ����
						m_mthMakeText(new string[]{"\n�ߡ�  �����������֮һ�ߣ����ܻ�����ʧ��������������ժ������"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr18) != false)
							m_mthMakeCheckText(m_strKeysArr18,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr018,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr19) != false)
							m_mthMakeCheckText(m_strKeysArr19,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr019,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr20) != false)
							m_mthMakeCheckText(m_strKeysArr20,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr020,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr21) != false)
							m_mthMakeCheckText(m_strKeysArr21,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr021,ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr22) != false)
							m_mthMakeCheckText(m_strKeysArr22,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr022,ref strAllText,ref strXml);
						#endregion �ߡ�  �����������֮һ�ߣ����ܻ�����ʧ��������������ժ����
						m_mthMakeText(new string[]{"\n�ˡ�  ���껼�ߣ������շ��ġ��ԡ��Ρ�������˥�ߣ��������⡣"},new string[]{""},ref strAllText,ref strXml);
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
		#region ̸��ҽ��ǩ��
		/// <summary>
		///  ̸��ҽ��ǩ��
		/// </summary>
		private class clsPrintInPatMedDoctorAndDate : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private bool blnNextPage = true;
			private string[] m_strKeysArr01 = {"̸��ҽ��ǩ��"};
			private string[] m_strKeysArr101 = {"                                                                                                         ̸��ҽ��ǩ����"};
	
			private string[] m_strKeysArr02 = {"����"};
			private string[] m_strKeysArr102 = {"\n                                                                                                         �� �ڣ�"};
	


 
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				
				//				if(blnNextPage)
				//				{
				//					//����һҳ��ӡ�����ô�ӡʱ���p_intPosY�Ƿ������ױߵ�ֵ���ж���ʵ��
				//					m_blnHaveMoreLine = true;
				//					blnNextPage = false;
				//					p_intPosY += 1500;
				//					return;
				//				}
				if(m_blnIsFirstPrint)
				{
					//					p_objGrp.DrawString("��ϵͳ���",m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
					//					p_intPosY += 20;
					//					p_objGrp.DrawString("һ�����",m_fontItemMidHead,Brushes.Black,m_intRecBaseX,p_intPosY);
					//					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						if(m_blnHavePrintInfo(m_strKeysArr01) != false)
							m_mthMakeText(m_strKeysArr101,m_strKeysArr01,ref strAllText,ref strXml);
						else
							m_mthMakeText(m_strKeysArr101,new string[]{""},ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr02) != false)
							m_mthMakeText(m_strKeysArr102,m_strKeysArr02,ref strAllText,ref strXml);
			
			
								
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("�����ǩ��",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
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
		#region ����(����)ǩ��
		/// <summary>
		///  ����(����)ǩ��
		/// </summary>
		private class clsPrintInPatMedDoctorAndDate1 : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private bool blnNextPage = true;
            private string[] m_strKeysArr01 = { "����(����)ǩ��" };
			private string[] m_strKeysArr101 = {"\n                                                                                                         ����(����)ǩ����"};

            private string[] m_strKeysArr02 = { "����1" };
			private string[] m_strKeysArr102 = {"\n                                                                                                         �� �ڣ�"};
	


 
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				
				//				if(blnNextPage)
				//				{
				//					//����һҳ��ӡ�����ô�ӡʱ���p_intPosY�Ƿ������ױߵ�ֵ���ж���ʵ��
				//					m_blnHaveMoreLine = true;
				//					blnNextPage = false;
				//					p_intPosY += 1500;
				//					return;
				//				}
				if(m_blnIsFirstPrint)
				{
					//					p_objGrp.DrawString("��ϵͳ���",m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
					//					p_intPosY += 20;
					//					p_objGrp.DrawString("һ�����",m_fontItemMidHead,Brushes.Black,m_intRecBaseX,p_intPosY);
					//					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"     ��ǰ����(���������λ�쵼)�����ǩ�����£���(���������λ�쵼)�����濴�����ϸ�֪���ݣ�ҽ����������ϸ���ͣ�����ȫ��⣬�����ؿ��ǣ���(���������λ�쵼)��������Ը�����������ơ�"},new string[]{""},ref strAllText,ref strXml);


						if(m_blnHavePrintInfo(m_strKeysArr01) != false)
							m_mthMakeText(m_strKeysArr101,m_strKeysArr01,ref strAllText,ref strXml);
						else
							m_mthMakeText(m_strKeysArr101,new string[]{""},ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(m_strKeysArr02) != false)
							m_mthMakeText(m_strKeysArr102,m_strKeysArr02,ref strAllText,ref strXml);
			
			
								
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("�����ǩ��",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
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
	}
}
