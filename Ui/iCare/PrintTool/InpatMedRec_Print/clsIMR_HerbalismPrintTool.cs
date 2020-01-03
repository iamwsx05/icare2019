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
	/// Summary description for clsIMR_HerbalismPrintTool.
	/// </summary>
	public class clsIMR_HerbalismPrintTool : clsInpatMedRecPrintBase
	{
		public clsIMR_HerbalismPrintTool(string p_strTypeID) :base(p_strTypeID)
		{
			//
			// TODO: Add constructor logic here
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
										  new clsPrintPatientFixInfo("����ҽѧ����Ժ��¼",285)
										  ,m_objPrintOneItemArr[0],m_objPrintOneItemArr[1],m_objPrintOneItemArr[2]
										  ,m_objPrintOneItemArr[3],m_objPrintOneItemArr[4],m_objPrintOneItemArr[5]
										  ,m_objPrintOneItemArr[6],m_objPrintOneItemArr[7]
										  ,m_objPrintMultiItemArr[0],m_objPrintMultiItemArr[1]
										  ,new clsPrintInPatMedRecPic(),m_objPrintMultiItemArr[2]
										  ,m_objPrintOneItemArr[8],m_objPrintSignArr[0]
										  ,m_objPrintMultiItemArr[3],m_objPrintSignArr[1]
									  });
		}

   		private void m_mthInitPrintLineArr()
		{
			m_objPrintOneItemArr = new clsPrintInPatMedRecItem[9];
			for(int i1=0;i1<m_objPrintOneItemArr.Length;i1++)
				m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();

			m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[4];
			for(int j2=0;j2<m_objPrintMultiItemArr.Length;j2++)
				m_objPrintMultiItemArr[j2] = new clsPrintInPatMedRecItem();

			m_objPrintSignArr = new clsPrintInPatMedRecSign[2];
			for(int k3=0;k3<m_objPrintSignArr.Length;k3++)
				m_objPrintSignArr[k3] = new clsPrintInPatMedRecSign();
		}

		protected override void m_mthSetSubPrintInfo()
		{
			#region ������Ŀ
			m_objPrintOneItemArr[0].m_mthSetPrintValue("����","���ߣ�");
			m_objPrintOneItemArr[1].m_mthSetPrintValue("�ֲ�ʷ","�ֲ�ʷ��");
			m_objPrintOneItemArr[2].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[3].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[4].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[5].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[6].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[7].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[8].m_mthSetPrintValue("��Ժ���","��Ժ��ϣ�");
			#endregion

			#region �����
			m_objPrintMultiItemArr[0].m_mthSetSpecialTitleValue("�� �� �� ��");
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"�����>>T","�����>>T","�����>>P","�����>>P","�����>>R","�����>>R","�����>>BP","�����>>BP","�����>>WT","�����>>WT"}
				,new string[]{"T��","#��","P��","#��/��","R��","#��/��","BP��","#mmHg","WT��","#Kg"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�����>>һ�����>>����","�����>>һ�����>>Ӫ��","�����>>һ�����>>����","�����>>һ�����>>��λ","�����>>һ�����>>��ʶ״̬"}
				,new string[]{"\nһ�������","������","Ӫ����","���ͣ�","��λ��","��ʶ״̬(���ר�����)��"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�����>>Ƥ��ճĤ>>��ɫ","�����>>Ƥ��ճĤ>>�¶�","�����>>Ƥ��ճĤ>>ˮ��","�����>>Ƥ��ճĤ>>Ƥ��"
																	 ,"�����>>Ƥ��ճĤ>>����","�����>>Ƥ��ճĤ>>�촯","�����>>Ƥ��ճĤ>>�̺�"}
				,new string[]{"\nƤ��ճĤ��","��ɫ��","�¶ȣ�","ˮ�ף�","Ƥ�","���ߣ�","�촯��","�̺ۣ�"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�����>>�ܰͽ�>>�״�","�����>>�ܰͽ�>>��λ","�����>>�ܰͽ�>>ѹʹ"}
				,new string[]{"\n�ܰͽ᣺","�״�","��λ��","ѹʹ��"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�����>>�ܰͽ�>>­��ȱ��","�����>>ͷ��>>­��ȱ��>>��λ","�����>>ͷ��>>­��ȱ��>>��С","�����>>ͷ��>>��Ĥ��Ѫ","�����>>ͷ��>>�ʲ���Ѫ","�����>>ͷ��>>�������״�","�����>>ͷ��>>��ǻ����"}
				,new string[]{"\nͷ  ����","­��ȱ��","��λ��","��С��","��Ĥ��Ѫ��","�ʲ���Ѫ��","�������״�","��ǻ����"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�����>>����>>��б","�����>>����>>������б","�����>>����>>��״���״�","�����>>����>>����Ŭ��"}
				,new string[]{"\n��  ����","��б��","����ƫб��","��״���״�","����ŭ�ţ�"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�����>>�ز�>>�ز�����","�����>>�ز�>>������","�����>>�ز�>>����","�����>>�ز�>>����","�����>>�ز�>>����","�����>>�ز�>>����","�����>>�ز�>>����","�����>>�ز�>>����"}
				,new string[]{"\n��  ����","�ز����Σ�","��������","������","���ʣ�","#��/��","���ɣ�","������","������"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�����>>����>>����","�����>>����>>ѹʹ","�����>>����>>����ʹ","�����>>����>>����","�����>>����>>����ѹʹ","�����>>����>>Ƣ","�����>>����>>ī������","�����>>����>>�ƶ�������","�����>>����>>����ߵʹ","�����>>����>>����ܵ�ѹʹ","�����>>����>>������"}
				,new string[]{"\n��  ����","���ڣ�","ѹʹ��","����ʹ��","���飺","����ѹʹ��","Ƣ��","ī��������","�ƶ���������","����ߵʹ��","����ܵ�ѹʹ��","��������"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"�����>>����ֱ��","�����>>����ֳ��","","�����>>������>>����","�����>>������>>���õ���","�����>>����","�����>>����","","�����>>��֫>>��","�����>>��֫>>�ؽ�","�����>>��֫>>�˶�"}
				,new string[]{"\n����ֱ����","\n����ֳ����","\n�����ڣ�","����","���õ���","\n������","\n���裺","\n��֫��","�ǣ�","�ؽڣ�","�˶���"});
			#endregion

			#region  ר�Ƽ��(1~8)
			m_objPrintMultiItemArr[1].m_mthSetSpecialTitleValue("ר �� �� ��");
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"ר�Ƽ��>>һ����ʶ״̬","ר�Ƽ��>>һ����ʶ״̬>>����"},new string[]{"\nһ����ʶ��̬��","������"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","ר�Ƽ��>>������֪ ���� ���� ����>>1�����﹦��>>����","ר�Ƽ��>>������֪ ���� ���� ����>>1�����﹦��>>ʧ��","","","ר�Ƽ��>>������֪ ���� ���� ����>>2����֪����>>������>>����","ר�Ƽ��>>������֪ ���� ���� ����>>2����֪����>>������>>Զ��"
				 ,"","ר�Ƽ��>>������֪ ���� ���� ����>>2����֪����>>������>>ʱ��","ר�Ƽ��>>������֪ ���� ���� ����>>2����֪����>>������>>�ص�","ר�Ƽ��>>������֪ ���� ���� ����>>2����֪����>>������>>����","ר�Ƽ��>>������֪ ���� ���� ����>>2����֪����>>ע����"
				,"ר�Ƽ��>>������֪ ���� ���� ����>>2����֪����>>�����","ר�Ƽ��>>������֪ ���� ���� ����>>2����֪����>>�����������","ר�Ƽ��>>������֪ ���� ���� ����>>3������״̬","","ר�Ƽ��>>������֪ ���� ���� ����>>4��������Ϊ>>����","ר�Ƽ��>>������֪ ���� ���� ����>>4��������Ϊ>>�����̶�","ר�Ƽ��>>������֪ ���� ���� ����>>4��������Ϊ>>������"}
				,new string[]{"\n������֪ ���� ���� ������","\n  1�����﹦�ܣ�","������","ʧ�","\n  2����֪���ܣ�","��������","���£�","Զ�£�","\n  ��������","ʱ�䣺","�ص㣺","���","ע������","�������","�������������","\n  3������״̬��","\n  4��������Ϊ��","������","�����̶ȣ�","��������"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","ר�Ƽ��>>����­��>>������>>���"},new string[]{"\n����­�񾭣�","\n  �� ���񾭣�","�����"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>����­��>>������>>����","ר�Ƽ��>>����­��>>������>>��Ұ","","ר�Ƽ��>>����­��>>������>>�۵�>>����ͷ"
				 ,"ר�Ƽ��>>����­��>>������>>�۵�>>����������","ר�Ƽ��>>����­��>>������>>�۵�>>����Ĥ��Ѫ","ר�Ƽ��>>����­��>>������>>�۵�>>���"}
				,new string[]{"\n  �� ���񾭣�","������","��Ұ��","�۵ף�","����ͷ��","������������","����Ĥ��Ѫ��","��飺"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>����­��>>�� �����񾭡��� �����񾭡��� ��չ��>>����λ��","ר�Ƽ��>>����­��>>�� �����񾭡��� �����񾭡��� ��չ��>>�������","ר�Ƽ��>>����­��>>�� �����񾭡��� �����񾭡��� ��չ��>>�����˶�"
				,"","ר�Ƽ��>>����­��>>�� �����񾭡��� �����񾭡��� ��չ��>>ͫ�׼�ͫ�׷���>>��С","ר�Ƽ��>>����­��>>�� �����񾭡��� �����񾭡��� ��չ��>>ͫ�׼�ͫ�׷���>>��С>>��","ר�Ƽ��>>����­��>>�� �����񾭡��� �����񾭡��� ��չ��>>ͫ�׼�ͫ�׷���>>��С>>��","ר�Ƽ��>>����­��>>�� �����񾭡��� �����񾭡��� ��չ��>>ͫ�׼�ͫ�׷���>>��С>>��","ר�Ƽ��>>����­��>>�� �����񾭡��� �����񾭡��� ��չ��>>ͫ�׼�ͫ�׷���>>��С>>��"
				,"ר�Ƽ��>>����­��>>�� �����񾭡��� �����񾭡��� ��չ��>>ͫ�׼�ͫ�׷���>>��״","ר�Ƽ��>>����­��>>�� �����񾭡��� �����񾭡��� ��չ��>>ͫ�׼�ͫ�׷���>>�Թⷴ��","ר�Ƽ��>>����­��>>�� �����񾭡��� �����񾭡��� ��չ��>>ͫ�׼�ͫ�׷���>>�����´�"}
				,new string[]{"\n  �� �����񾭡��� �����񾭡��� ��չ�񾭣�","����λ�ã�","���������","�����˶���","\n  ͫ�׼�ͫ�׷��䣺","��С��","�ң�","#mm","��","#mm","��״��","�Թⷴ�䣺","�����´���"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>����­��>>�� ������>>����о�","ר�Ƽ��>>����­��>>�� ������>>��Ĥ����","","ר�Ƽ��>>����­��>>�� ������>>����˶�>>����","ר�Ƽ��>>����­��>>�� ������>>����˶�>>򨼡"}
				,new string[]{"\n  �� �����񾭣�","����о���","��Ĥ���䣺","����˶���","������","򨼡��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>����­��>>�� ����>>���","ר�Ƽ��>>����­��>>�� ����>>�Ǵ���","ר�Ƽ��>>����­��>>�� ����>>����","ר�Ƽ��>>����­��>>�� ����>>����","ר�Ƽ��>>����­��>>�� ����>>¶��"
				,"ר�Ƽ��>>����­��>>�� ����>>����","ר�Ƽ��>>����­��>>�� ����>>�ڽ�ƫ��","ר�Ƽ��>>����­��>>�� ����>>���ǰ2/3ζ��"}
				,new string[]{"\n  �� ���񾭣�","��","�Ǵ�����","���ۣ�","������","¶�ݣ�","���ڣ�","�ڽ�ƫ�᣺","��ǰ2/3ζ����"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>����­��>>�� ����>>����","ר�Ƽ��>>����­��>>�� ����>>����"},new string[]{"\n  �� ���񾭣�","������","������"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>����­��>>�� �����񾭡��� ������>>���1/3ζ��","ר�Ƽ��>>����­��>>�� �����񾭡��� ������>>����","ר�Ƽ��>>����­��>>�� �����񾭡��� ������>>�ʷ���","ר�Ƽ��>>����­��>>�� �����񾭡��� ������>>����"}
				,new string[]{"\n  �� �����񾭡��� ������:��","���1/3ζ����","������","�ʷ��䣺","���ʣ�"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>����­��>>�� ����>>�ʼ�","ר�Ƽ��>>����­��>>�� ����>>ת��"},new string[]{"\n  �� ���񾭣�","�ʼ磺","ת����"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>����­��>>�� ������>>����","ר�Ƽ��>>����­��>>�� ������>>�༡ή��","ר�Ƽ��>>����­��>>�� ������>>�༡���"},new string[]{"\n  �� �����񾭣�","���ࣺ","�༡ή����","�༡�����"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>�ġ�������>>����֫","ר�Ƽ��>>�ġ�������>>����֫","ר�Ƽ��>>�ġ�������>>����֫","ר�Ƽ��>>�ġ�������>>����֫","ר�Ƽ��>>�ġ�������>>����֫","ר�Ƽ��>>�ġ�������>>����֫","ר�Ƽ��>>�ġ�������>>����֫","ר�Ƽ��>>�ġ�������>>����֫"}
				,new string[]{"\n�ġ���������(Ashworth�ּ�)��","\n  ����֫��","#��","����֫��","#��","����֫��","#��","����֫��","#��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","ר�Ƽ��>>�塢����>>����֫>>����","ר�Ƽ��>>�塢����>>����֫>>����","ר�Ƽ��>>�塢����>>����֫>>Զ��","ר�Ƽ��>>�塢����>>����֫>>Զ��","","ר�Ƽ��>>�塢����>>����֫>>����","ר�Ƽ��>>�塢����>>����֫>>����","ר�Ƽ��>>�塢����>>����֫>>Զ��","ר�Ƽ��>>�塢����>>����֫>>Զ��"
				,"","ר�Ƽ��>>�塢����>>����֫>>����","ר�Ƽ��>>�塢����>>����֫>>����","ר�Ƽ��>>�塢����>>����֫>>Զ��","ר�Ƽ��>>�塢����>>����֫>>Զ��","","ר�Ƽ��>>�塢����>>����֫>>����","ר�Ƽ��>>�塢����>>����֫>>����","ר�Ƽ��>>�塢����>>����֫>>Զ��","ר�Ƽ��>>�塢����>>����֫>>Զ��"}
				,new string[]{"\n�塢����(���������߿ɲ���)","\n  ����֫��","���ˣ�","#��","Զ�ˣ�","#��","\n  ����֫��","����","#��","Զ��","#��","\n  ����֫��","����","#��","Զ��","#��","\n  ����֫��","����","#��","Զ��","#��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>������̱����>>����֫","ר�Ƽ��>>������̱����>>����֫","ר�Ƽ��>>������̱����>>����֫","ר�Ƽ��>>������̱����>>����֫"},new string[]{"\n������̱���飺","\n  ����֫��","����֫��","����֫��","����֫��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","ר�Ƽ��>>�ߡ������˶�>>1.ָ������>>�Ҳ�","ר�Ƽ��>>�ߡ������˶�>>1.ָ������>>���"},new string[]{"\n�ߡ������˶���","\n  1.ָ�����飺","�Ҳࣺ","��ࣺ"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>�ߡ������˶�>>2.�츴���涯��>>�Ҳ�","ר�Ƽ��>>�ߡ������˶�>>2.�츴���涯��>>���"},new string[]{"\n  2.�츴���涯����","�Ҳࣺ","��ࣺ"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>�ߡ������˶�>>3.��ϥ������>>�Ҳ�","ר�Ƽ��>>�ߡ������˶�>>3.��ϥ������>>���"},new string[]{"\n  3.��ϥ�����飺","�Ҳࣺ","��ࣺ"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>�ߡ������˶�>>4.Romberg��s��>>����","ר�Ƽ��>>�ߡ������˶�>>4.Romberg��s��>>����"},new string[]{"\n  4.Romberg��s����","���ۣ�","���ۣ�"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","","ר�Ƽ��>>�ˡ��о�����>>ǳ�о�>>����>>��","ר�Ƽ��>>�ˡ��о�����>>ǳ�о�>>����>>��","","ר�Ƽ��>>�ˡ��о�����>>ǳ�о�>>ʹ��>>��","ר�Ƽ��>>�ˡ��о�����>>ǳ�о�>>ʹ��>>��","","ר�Ƽ��>>�ˡ��о�����>>ǳ�о�>>�¶Ⱦ�>>��","ר�Ƽ��>>�ˡ��о�����>>ǳ�о�>>�¶Ⱦ�>>��"}
				,new string[]{"\n�ˡ��о�����:(������������)(����+++������++������+����ʧ-)","\n  ǳ�о���","������","�ң�","��","ʹ����","�ң�","��","�¶Ⱦ���","�ң�","��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","ר�Ƽ��>>�ˡ��о�����>>��о�>>�˶���>>��","ר�Ƽ��>>�ˡ��о�����>>��о�>>�˶���>>��","","ר�Ƽ��>>�ˡ��о�����>>��о�>>λ�þ�>>��","ר�Ƽ��>>�ˡ��о�����>>��о�>>λ�þ�>>��","","ר�Ƽ��>>�ˡ��о�����>>��о�>>�𶯾�>>��","ר�Ƽ��>>�ˡ��о�����>>��о�>>�𶯾�>>��"}
				,new string[]{"\n  ��о���","�˶�����","�ң�","��","λ�þ���","�ң�","��","�𶯾���","�ң�","��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","ר�Ƽ��>>�ˡ��о�����>>���ϸо�>>�����>>��","ר�Ƽ��>>�ˡ��о�����>>���ϸо�>>�����>>��","","ר�Ƽ��>>�ˡ��о�����>>���ϸо�>>�������>>��","ר�Ƽ��>>�ˡ��о�����>>���ϸо�>>�������>>��"}
				,new string[]{"\n  ���ϸо���","�������","�ң�","��","���������","�ң�","��"});
			#endregion

			#region ר�Ƽ��(9~12)
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","","","","ר�Ƽ��>>�š�����>>1��������>>ǳ����>>���ڷ���>>��>>��","ר�Ƽ��>>�š�����>>1��������>>ǳ����>>���ڷ���>>��>>��","ר�Ƽ��>>�š�����>>1��������>>ǳ����>>���ڷ���>>��>>��","","ר�Ƽ��>>�š�����>>1��������>>ǳ����>>���ڷ���>>��>>��"
				,"ר�Ƽ��>>�š�����>>1��������>>ǳ����>>���ڷ���>>��>>��","ר�Ƽ��>>�š�����>>1��������>>ǳ����>>���ڷ���>>��>>��","","ר�Ƽ��>>�š�����>>1��������>>ǳ����>>��غ����>>��","ר�Ƽ��>>�š�����>>1��������>>ǳ����>>��غ����>>��","","ר�Ƽ��>>�š�����>>1��������>>ǳ����>>���ŷ���>>��","ר�Ƽ��>>�š�����>>1��������>>ǳ����>>���ŷ���>>��"}
				,new string[]{"\n�š�����","\n  1��������:(����++++������+++������++������+����ʧ-)��","\n  ǳ���䣺","���ڷ��䣺","�ң�","�ϣ�","�У�","�£�","��","�ϣ�","�У�","�£�","\n  ��غ���䣺","��","�ң�","���ŷ��䣺","��","�ң�"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","ר�Ƽ��>>�š�����>>1��������>>���>>�Ŷ�ͷ������>>�Ҳ�","ר�Ƽ��>>�š�����>>1��������>>���>>�Ŷ�ͷ������>>���","","ר�Ƽ��>>�š�����>>1��������>>���>>����ͷ������>>�Ҳ�","ר�Ƽ��>>�š�����>>1��������>>���>>����ͷ������>>���","","ר�Ƽ��>>�š�����>>1��������>>���>>�㷴��>>�Ҳ�"
				,"ר�Ƽ��>>�š�����>>1��������>>���>>����>>���","","ר�Ƽ��>>�š�����>>1��������>>���>>ϥ����>>�Ҳ�","ר�Ƽ��>>�š�����>>1��������>>���>>ϥ����>>���","","ר�Ƽ��>>�š�����>>1��������>>���>>�׷���>>�Ҳ�","ר�Ƽ��>>�š�����>>1��������>>���>>����>>���","","ר�Ƽ��>>�š�����>>1��������>>���>>����>>���Ҳ�"
				,"ר�Ƽ��>>�š�����>>1��������>>���>>����>>�����","ר�Ƽ��>>�š�����>>1��������>>���>>����>>���Ҳ�","ר�Ƽ��>>�š�����>>1��������>>���>>����>>�����"}
				,new string[]{"\n  ��䣺","�Ŷ�ͷ�����䣺","�Ҳࣺ","��ࣺ","����ͷ�����䣺","�Ҳࣺ","��ࣺ","�㷴�䣺","�Ҳࣺ","��ࣺ","ϥ���䣺","�Ҳࣺ","��ࣺ","�׷��䣺","�Ҳࣺ","��ࣺ","���Σ�","���Ҳࣺ","��ࣺ","���Ҳࣺ","��ࣺ"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>�š�����>>2��������>>��˱����","","ר�Ƽ��>>�š�����>>2��������>>������򢷴��>>�Ҳ�","ר�Ƽ��>>�š�����>>2��������>>������򢷴��>>���","","ר�Ƽ��>>�š�����>>2��������>>Hoffmann sign>>�Ҳ�","ר�Ƽ��>>�š�����>>2��������>>Hoffmann sign>>���"
				,"","ר�Ƽ��>>�š�����>>2��������>>Rossolimo sign>>�Ҳ�","ר�Ƽ��>>�š�����>>2��������>>Rossolimo sign>>���","","ר�Ƽ��>>�š�����>>2��������>>Babinski sign>>�Ҳ�","ר�Ƽ��>>�š�����>>2��������>>Babinski sign>>���","","ר�Ƽ��>>�š�����>>2��������>>Chaddock sign>>�Ҳ�","ר�Ƽ��>>�š�����>>2��������>>Chaddock sign>>���"
				,"","ר�Ƽ��>>�š�����>>2��������>>Oppenheim sign>>�Ҳ�","ר�Ƽ��>>�š�����>>2��������>>Oppenheim sign>>���","","ר�Ƽ��>>�š�����>>2��������>>Gordon sign>>�Ҳ�","ר�Ƽ��>>�š�����>>2��������>>Gordon sign>>���"}
				,new string[]{"\n  2�������䣺","��˱���䣺","������򢷴�䣺","�Ҳࣺ","��ࣺ","Hoffmann sign��","�Ҳࣺ","��ࣺ","Rossolimo sign��","�Ҳࣺ","��ࣺ","Babinski sign��","�Ҳࣺ","��ࣺ","Chaddock sign��","�Ҳࣺ","��ࣺ","Oppenheim sign��","�Ҳࣺ","��ࣺ","Gordon sign��","�Ҳࣺ","��ࣺ"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>ʮ��ֲ���񾭹���>>�쳣����","ר�Ƽ��>>ʮ��ֲ���񾭹���>>��㹦��","ר�Ƽ��>>ʮ��ֲ���񾭹���>>С�㹦��"},new string[]{"\nʮ��ֲ���񾭹���","\n  �쳣������","��㹦�ܣ�","С�㹦��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>ʮһ����Ĥ�̼���>>���ֿ�","ר�Ƽ��>>ʮһ����Ĥ�̼���>>�����ع�","ר�Ƽ��>>ʮһ����Ĥ�̼���>>��ָ","ר�Ƽ��>>ʮһ����Ĥ�̼���>>Kering sign","ר�Ƽ��>>ʮһ����Ĥ�̼���>>Brudzinskin sign"}
				,new string[]{"\nʮһ����Ĥ�̼���","\n  ���ֿ���","�����عǣ�","��ָ��","Kering sign��","Brudzinskin sign��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","ר�Ƽ��>>ʮ��������״̬>>�����˶�>>����֫","ר�Ƽ��>>ʮ��������״̬>>�����˶�>>����֫","ר�Ƽ��>>ʮ��������״̬>>�����˶�>>����֫","ר�Ƽ��>>ʮ��������״̬>>�����˶�>>����֫"},new string[]{"\nʮ��������״̬","\n  �����˶���","����֫��","����֫��","����֫��","����֫��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>ʮ��������״̬>>��ͬ�˶�>>����֫","ר�Ƽ��>>ʮ��������״̬>>��ͬ�˶�>>����֫","ר�Ƽ��>>ʮ��������״̬>>��ͬ�˶�>>����֫","ר�Ƽ��>>ʮ��������״̬>>��ͬ�˶�>>����֫"},new string[]{"\n  ��ͬ�˶���","����֫��","����֫��","����֫��","����֫��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>ʮ��������״̬>>����>>����෭","ר�Ƽ��>>ʮ��������״̬>>����>>���Ҳ෭"},new string[]{"\n  ����","����෭��","���Ҳ෭��"});
			
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>ʮ��������״̬>>����ת��>>�� �� ��","ר�Ƽ��>>ʮ��������״̬>>����ת��>>�� �� ��","ר�Ƽ��>>ʮ��������״̬>>��λƽ��"},new string[]{"\n  ����ת�ƣ�","�� �� ����","�� �� �ԣ�","��λƽ�⣺"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>ʮ��������״̬>>��վת��>>�� �� վ","ר�Ƽ��>>ʮ��������״̬>>��վת��>>վ �� ��","ר�Ƽ��>>ʮ��������״̬>>վλƽ��"},new string[]{"\n  ��վת�ƣ�","�� �� վ��","վ �� ����","վλƽ�⣺"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>ʮ��������״̬>>����>>���β���","ר�Ƽ��>>ʮ��������״̬>>����>>ƽ������","ר�Ƽ��>>ʮ��������״̬>>����>>����¥��","ר�Ƽ��>>ʮ��������״̬>>����>>��������","ר�Ƽ��>>ʮ��������״̬>>��̬"}
				,new string[]{"\n  ���У�","���β�����","ƽ�����ߣ�","����¥�ݣ�","�������ߣ�","��̬��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"ר�Ƽ��>>ʮ��������״̬>>�ؽڻ��","ר�Ƽ��>>ʮ��������״̬>>�ؽ���ʹ"},new string[]{"\n  �ؽڻ�ȣ�","�ؽ���ʹ��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","ר�Ƽ��>>ʮ��������״̬>>�ճ�����>>��ʳ","ר�Ƽ��>>ʮ��������״̬>>�ճ�����>>��ͷ","ר�Ƽ��>>ʮ��������״̬>>�ճ�����>>ϴ��","ר�Ƽ��>>ʮ��������״̬>>�ճ�����>>ˢ��","ר�Ƽ��>>ʮ��������״̬>>�ճ�����>>����","ר�Ƽ��>>ʮ��������״̬>>�ճ�����>>������","ר�Ƽ��>>ʮ��������״̬>>�ճ�����>>������"
				,"ר�Ƽ��>>ʮ��������״̬>>�ճ�����>>��Ь","ר�Ƽ��>>ʮ��������״̬>>�ճ�����>>���ת��","ר�Ƽ��>>ʮ��������״̬>>�ճ�����>>������","ר�Ƽ��>>ʮ��������״̬>>�ճ�����>>ϴ��"}
				,new string[]{"\n  �ճ����","��ʳ��","��ͷ��","ϴ����","ˢ����","���ڣ�","�����£�","�����ӣ�","��Ь��","���ת�ƣ�","�����ࣺ","ϴ�裺"});
			#endregion

			#region ������� & �������Ƽƻ�
			m_objPrintMultiItemArr[2].m_mthSetSpecialTitleValue("�� �� �� ��");
			m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[]{"�������>>1��ʵ���Ҽ��","�������>>2��CT��MR���","�������>>3��X�߼��","�������>>4���������"},new string[]{"\n  ʵ���Ҽ�飺","\n  CT��MR��飺","\n  X�߼�飺","\n  ������飺"});

			m_objPrintMultiItemArr[3].m_mthSetSpecialTitleValue("�������Ƽƻ�");
			m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[]{"","�������Ƽƻ�>>һ����������>>1����֪�������ϰ�","�������Ƽƻ�>>һ����������>>2���˶��ϰ�","�������Ƽƻ�>>һ����������>>3���о��ϰ�","�������Ƽƻ�>>һ����������>>4���ؽ���ʹ��ROM�쳣","�������Ƽƻ�>>һ����������>>5��ADL�ϰ�","�������Ƽƻ�>>һ����������>>6����С���ϰ�","�������Ƽƻ�>>һ����������>>7������"}
				,new string[]{"\nһ���������⣺","\n  1����֪�������ϰ���","\n  2���˶��ϰ���","\n  3���о��ϰ���","\n  4���ؽ���ʹ��ROM�쳣��","\n  5��ADL �ϰ���","\n  6����С���ϰ���","\n  7��������"});
			m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[]{"","�������Ƽƻ�>>�������Ƽƻ�"},new string[]{"\n�������Ƽƻ���\n    ",""});
			#endregion

			#region ǩ��������
			m_objPrintSignArr[0].m_mthSetPrintSignValue(new string[]{"סԺҽʦ","����ҽʦ","����"},new string[]{"סԺҽʦ��","����ҽʦ��","���ڣ�"});
			m_objPrintSignArr[1].m_mthSetPrintSignValue(new string[]{"�������Ƽƻ�>>ҽʦ","�������Ƽƻ�>>ʱ��"},new string[]{"ҽʦ��","���ڣ�"});
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
							p_intPosY += 20;
							p_objGrp.DrawString(m_strSpecialTitle,clsIMR_HerbalismPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+300,p_intPosY);
							p_intPosY += 40;
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
						m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);
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

	}
}
