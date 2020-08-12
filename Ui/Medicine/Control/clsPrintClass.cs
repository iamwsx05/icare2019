using System;
using System.Drawing;
using System.Data;
using weCare.Core.Entity;
using System.Collections;
using System.Windows.Forms;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsPrintClass ��ժҪ˵����
	/// </summary>
	public class clsPrintClass  :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		public clsPrintClass()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ����
		/// <summary>
		/// ��������
		/// </summary>
		Font objFontTitle=new Font("����_GB2312",14,System.Drawing.FontStyle.Bold);
		/// <summary>
		/// ��������
		/// </summary>
		Font objFontNormal=new Font("SimSun",10);
		/// <summary>
		/// �Ӵ�����
		/// </summary>
		Font objFontBold=new Font("SimSun",11,System.Drawing.FontStyle.Bold);
		/// <summary>
		/// ��ϸ����
		/// </summary>
		Font objFont=new Font("SimSun",9);
		/// <summary>
		/// ��߾�
		/// </summary>
		const float fltLeftIndentProp=20f;
		/// <summary>
		/// �ұ߾�
		/// </summary>
		const float fltRightIndentProp=20f;
		/// <summary>
		/// ���±߾�
		/// </summary>
		const float fltStarHight=20f;
		/// <summary>
		/// �м��
		/// </summary>
		const   float  fltRowHeight=25F;
		/// <summary>
		/// ������
		/// </summary>
		private float  Y=fltStarHight;
		/// <summary>
		/// ������
		/// </summary>
		private float  X=fltLeftIndentProp;
		/// <summary>
		/// ��ӡ�Ŀ��
		/// </summary>
		private float  PageWith;
		/// <summary>
		/// ��ӡ�ĳ���
		/// </summary>
		private float  PageHigh;
		/// <summary>
		/// ����
		/// </summary>
		Pen PenLine=new Pen(Brushes.Black,1);
		/// <summary>
		/// ��������λ�õ�X��
		/// </summary>
		float temX=0;
		/// <summary>
		/// ��������λ�õ�Y��
		/// </summary>
		float temY=0;
		float temHight=fltRowHeight;
		int currRow=0;
		int currPage=1;
		#endregion
		public  void m_mthPrint(clsMedStoreApplPrint_VO MedStoreAppl,DataSet dtSet,System.Drawing.Printing.PrintPageEventArgs e)
		{
			if(temHight!=0)
			{
				PageWith=e.PageBounds.Width;
				PageHigh=e.PageBounds.Height;
				m_mthPrintTit(MedStoreAppl,e);
			}
			e.Graphics.DrawString("��"+currPage.ToString()+"ҳ",objFontNormal,Brushes.Black,(PageWith-fltLeftIndentProp-fltRightIndentProp-e.Graphics.MeasureString("��"+currPage.ToString()+"ҳ",objFontNormal).Width)/2,PageHigh-30);
			for(int i1=currRow;i1<dtSet.Tables.Count;i1++)
			{	
				if(Y+(dtSet.Tables[i1].Rows.Count+1)*fltRowHeight>PageHigh-fltStarHight)
				{
					Y=fltStarHight;
					X=fltLeftIndentProp;
					currRow=i1;
					temHight=0;
					currPage++;
					e.HasMorePages=true;
					return;
				}
				else
				{
					m_mthPrintBady(dtSet.Tables[i1],temHight,e);
					temHight=fltRowHeight;
					currRow=i1;
				}
			}
			if(Y+3*fltRowHeight>PageHigh-fltStarHight)
			{
				Y=fltStarHight;
				X=fltLeftIndentProp;
				temHight=0;
				currRow=dtSet.Tables.Count;
				currPage++;
				e.HasMorePages=true;
				return;
			}
			else
			{
				m_mthPrintEnd(MedStoreAppl,e,temHight);
			}
		}
		private void m_mthPrintTit(clsMedStoreApplPrint_VO MedStoreAppl,System.Drawing.Printing.PrintPageEventArgs e)
		{
			e.Graphics.DrawString(MedStoreAppl.m_strPrintName_CHR,objFontTitle,Brushes.Black,(PageWith-fltLeftIndentProp-fltRightIndentProp-e.Graphics.MeasureString(MedStoreAppl.m_strPrintName_CHR,objFontTitle).Width)/2,Y);
			Y+=fltRowHeight;
			e.Graphics.DrawString("���뵥��:",objFontNormal,Brushes.Black,X,Y);
			X+=e.Graphics.MeasureString("���뵥��:",objFontNormal).Width;
			e.Graphics.DrawString(MedStoreAppl.m_strMEDAPPLNO_CHR,objFontNormal,Brushes.Black,X,Y);
			X=PageWith-200;
			e.Graphics.DrawString("��������:",objFontNormal,Brushes.Black,X,Y);
			X+=e.Graphics.MeasureString("��������:",objFontNormal).Width;
			e.Graphics.DrawString(MedStoreAppl.m_strAPPLDATE_DAT,objFontNormal,Brushes.Black,X,Y);
			Y+=fltRowHeight;
			X=fltLeftIndentProp;
			temY=Y;
			e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
			X+=5;
			e.Graphics.DrawString("��ע",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("��ע��ע��",objFontNormal).Width;
			e.Graphics.DrawLine(PenLine,PageWith-fltRightIndentProp,Y,PageWith-fltRightIndentProp,Y+fltRowHeight);
			
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
			X+=5;
			e.Graphics.DrawString(MedStoreAppl.m_strMEMO_VCHR,objFontNormal,Brushes.Black,X,Y+5);
		}
		private void m_mthPrintBady(DataTable dt ,float hight,System.Drawing.Printing.PrintPageEventArgs e)
		{
				double fltmomey=0;
					X=fltLeftIndentProp;
					Y+=fltRowHeight;
					e.Graphics.DrawLine(PenLine,X,Y-hight,X,Y+fltRowHeight*2);
					//ҩƷ����
					e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
					X+=5;
					e.Graphics.DrawString("ҩƷ����",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("��ע��ע��",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//ҩƷ����
					X+=5;
					e.Graphics.DrawString("ҩƷ����",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("ҩƷ���Ʊ�",objFontNormal).Width*2;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//���
					X+=5;
					e.Graphics.DrawString("���",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("���",objFontNormal).Width*5;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//��������
					X+=5;
					e.Graphics.DrawString("��������",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("���",objFontNormal).Width*5;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);;

					//����
					X+=5;
					e.Graphics.DrawString("����",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("����",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

					//��λ
					X+=5;
					e.Graphics.DrawString("��λ",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("��λ",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

					//����
					X+=5;
					e.Graphics.DrawString("����",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("����.",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

					//�ܽ��
					X+=5;
					e.Graphics.DrawString("�ܽ��",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("�ܽ���",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,PageWith-fltRightIndentProp,Y-hight,PageWith-fltRightIndentProp,Y+fltRowHeight);
					for(int f2=0;f2<dt.Rows.Count;f2++)
					{
						X=fltLeftIndentProp;
						Y+=fltRowHeight;
						e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						//ҩƷ����
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["ASSISTCODE_CHR"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("��ע��ע��",objFontNormal).Width;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						//ҩƷ����
						X+=5;
						if(e.Graphics.MeasureString(dt.Rows[f2]["MEDICINENAME_VCHR"].ToString(),objFontNormal).Width>160)
						{
							e.Graphics.DrawString(dt.Rows[f2]["MEDICINENAME_VCHR"].ToString().Substring(0,12),objFont,Brushes.Black,X,Y);
							e.Graphics.DrawString(dt.Rows[f2]["MEDICINENAME_VCHR"].ToString().Substring(12),objFont,Brushes.Black,X,Y+13);
						}
						else
						{
							e.Graphics.DrawString(dt.Rows[f2]["MEDICINENAME_VCHR"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						}
						X+=e.Graphics.MeasureString("ҩƷ���Ʊ�",objFontNormal).Width*2;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						//���
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["MEDSPEC_VCHR"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("���",objFontNormal).Width*5;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						//��������
						X+=5;
						if(e.Graphics.MeasureString(dt.Rows[f2]["PRODCUTORID_CHR"].ToString(),objFontNormal).Width>176.256)
						{
							e.Graphics.DrawString(dt.Rows[f2]["PRODCUTORID_CHR"].ToString().Substring(0,12),objFont,Brushes.Black,X,Y);
							e.Graphics.DrawString(dt.Rows[f2]["PRODCUTORID_CHR"].ToString().Substring(12),objFont,Brushes.Black,X,Y+13);
						}
						else
						{
							e.Graphics.DrawString(dt.Rows[f2]["PRODCUTORID_CHR"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						}
						
						X+=e.Graphics.MeasureString("���",objFontNormal).Width*5;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

						//����
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["QTY_DEC"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("����",objFontNormal).Width;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						temX=X;
						//��λ
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["UNITID_CHR"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("��λ",objFontNormal).Width;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

						//����
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["UNITPRICE_MNY"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("����.",objFontNormal).Width;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						

						//�ܽ��
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["TOLMNY_MNY"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("�ܽ���",objFontNormal).Width;
						fltmomey+=double.Parse(dt.Rows[f2]["TOLMNY_MNY"].ToString());
						e.Graphics.DrawLine(PenLine,PageWith-fltRightIndentProp,Y,PageWith-fltRightIndentProp,Y+fltRowHeight);
					}
					X=fltLeftIndentProp;
					Y+=fltRowHeight;
					e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//��Ӧ��
					X+=5;
					e.Graphics.DrawString("��Ӧ��",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("��ע��ע��",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//��Ӧ������
					X+=5;
					if(dt.TableName=="Table1")
						e.Graphics.DrawString("",objFontNormal,Brushes.Black,X,Y+5);
					else
						e.Graphics.DrawString(dt.TableName,objFontNormal,Brushes.Black,X,Y+5);
					X=temX;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//С�ƽ��
					X+=5;
					e.Graphics.DrawString("С�ƽ��",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("ҩƷ���Ʊ�",objFontNormal).Width+3;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

					//С�ƽ������
					X+=5;
					e.Graphics.DrawString(fltmomey.ToString(),objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("ҩƷ���Ʊ�",objFontNormal).Width;
					Y+=fltRowHeight;
					X=fltLeftIndentProp;
					e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
					e.Graphics.DrawLine(PenLine,PageWith-fltRightIndentProp,Y-fltRowHeight,PageWith-fltRightIndentProp,Y);

	    }
		private void m_mthPrintEnd(clsMedStoreApplPrint_VO MedStoreAppl,System.Drawing.Printing.PrintPageEventArgs e,float hight)
		{
			Y+=fltRowHeight;
			X=fltLeftIndentProp;
			e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
			e.Graphics.DrawLine(PenLine,X,Y-hight,X,Y+fltRowHeight*2);
			X=temX;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
			//�ܽ��
			X+=5;
			e.Graphics.DrawString("�� �� ��",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("ҩƷ���Ʊ�",objFontNormal).Width+3;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

			//�ܽ��
			X+=5;
			e.Graphics.DrawString(MedStoreAppl.m_strTOTMONEY_CHR,objFontNormal,Brushes.Black,X,Y+5);

			X=fltLeftIndentProp;
			e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
			X=fltLeftIndentProp;
			Y+=fltRowHeight;
			e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
			X+=5;
			
			//�Ƶ���
			e.Graphics.DrawString("�Ƶ���",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("��ע��ע��",objFontNormal).Width;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
			//�Ƶ���
			X+=5;
			e.Graphics.DrawString(MedStoreAppl.m_strCREATORNAME_CHR,objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("ҩƷ���Ʊ�",objFontNormal).Width*2;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
			//������
			X+=5;
			e.Graphics.DrawString("������",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("���",objFontNormal).Width*2;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
			//������
			X+=5;
			e.Graphics.DrawString(" ",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("���",objFontNormal).Width*3;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);;

			//����
			X+=5;
			e.Graphics.DrawString("�����",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("����",objFontNormal).Width;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

			//�����
			X+=5;
			e.Graphics.DrawString("",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("��λ..",objFontNormal).Width*2;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

			//���ʱ��
			X+=5;
			e.Graphics.DrawString("���ʱ��",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("���ʱ��",objFontNormal).Width;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

			//�ܽ��
			X+=5;
			e.Graphics.DrawString(MedStoreAppl.m_strEMPDATE_CHR,objFontNormal,Brushes.Black,X,Y+5);
			X=fltLeftIndentProp;
			Y+=fltRowHeight;
			e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
			e.Graphics.DrawLine(PenLine,PageWith-fltRightIndentProp,Y-fltRowHeight*2-hight,PageWith-fltRightIndentProp,Y);

		}

	}
	public class clsStorageOrd : com.digitalwave.GUI_Base.clsController_Base
	{
		public clsStorageOrd()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ����
		/// <summary>
		/// ��������
		/// </summary>
		Font objFontTitle=new Font("����_GB2312",14,System.Drawing.FontStyle.Bold);
		/// <summary>
		/// ��������
		/// </summary>
		Font objFontNormal=new Font("SimSun",10);
		/// <summary>
		/// ��ϸ����
		/// </summary>
		Font objFont=new Font("SimSun",9);
		/// <summary>
		/// �Ӵ�����
		/// </summary>
		Font objFontBold=new Font("SimSun",11,System.Drawing.FontStyle.Bold);
		/// <summary>
		/// ��߾�
		/// </summary>
		const float fltLeftIndentProp=10f;
		/// <summary>
		/// �ұ߾�
		/// </summary>
	    float fltRightIndentProp=40f;
		/// <summary>
		/// ���±߾�
		/// </summary>
		public const float fltStarHight=0f;
		/// <summary>
		/// �м��
		/// </summary>
		const   float  fltRowHeight=25F;
		/// <summary>
		/// �����м��
		/// </summary>
		const   float  fltRowHeightTit=43F;
		/// <summary>
		/// ������
		/// </summary>
		public float  Y=fltStarHight;
		/// <summary>
		/// ������
		/// </summary>
		private float  X=fltLeftIndentProp;
		/// <summary>
		/// ��ӡ�Ŀ��
		/// </summary>
		private float  PageWith;
		/// <summary>
		/// ��ӡ�ĳ���
		/// </summary>
		private float  PageHigh;
		/// <summary>
		/// ����
		/// </summary>
		Pen PenLine=new Pen(Brushes.Black,1);
		/// <summary>
		/// ��������λ�õ�Y��
		/// </summary>
		float temY=0;
		float fltrithgwith=2;
		/// <summary>
		/// ��ǰ��ӡ�ı�
		/// </summary>
		public int currdt=0;
		/// <summary>
		/// ��ǰ��ӡ����
		/// </summary>
		public int currRow=0;
		/// <summary>
		/// ��¼���м���ҳ
		/// </summary>
		public int pagenuber=0;
		/// <summary>
		/// ����
		/// </summary>
		string titleName;
        /// <summary>
        /// ��ʶ�Ƿ��ӡ
        /// </summary>
        public bool isPrint;
		#endregion

		public  void m_mthPrint(string datestart,string dateEnd,DataSet dtsetAll,string strMan,string strSTANDARD,string strSTORAGETYPEID,string strSIGN,string strIn,System.Drawing.Printing.PrintPageEventArgs e)
		{
			PageWith=e.PageBounds.Width;
			PageHigh=e.PageBounds.Height;
			if(strSIGN=="1")
			{
                fltRightIndentProp = 45;
                X = fltLeftIndentProp;
                Y = fltStarHight;
                #region 
                switch (strSTORAGETYPEID)
                {
                    case "1":
                        if (strIn == "1")
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ�б��˿���ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ���б��˿���ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ�˿���ϸ����";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ�б������ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ���б������ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ�����ϸ����";
                            }
                        }
                        break;
                    case "2":
                        if (strIn == "1")
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ�б��˿���ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ���б��˿���ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ�˿���ϸ����";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ�б������ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ���б������ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ�����ϸ����";
                            }
                        }
                        break;
                    case "3":
                        if (strIn == "1")
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ�б��˿���ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ���б��˿���ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ�˿���ϸ����";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ�б������ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ���б������ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ�����ϸ����";
                            }
                        }
                        break;
                }
                #endregion
            }
			else
			{
                X = fltLeftIndentProp;
                Y = fltStarHight;
                #region
                switch (strSTORAGETYPEID)
                {
                    case "1":
                        if (strIn == "1")
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ�б������ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ���б������ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ������ϸ����";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ�б��˻���ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ���б��˻���ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "��ҩ�˻���ϸ����";
                            }
                        }

                        break;
                    case "2":
                        if (strIn == "1")
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ�б������ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ���б������ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ������ϸ����";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ�б��˻���ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ���б��˻���ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�в�ҩ�˻���ϸ����";
                            }
                        }
                        break;
                    case "3":
                        if (strIn == "1")
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ�б������ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ���б������ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ������ϸ����";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ�б��˻���ϸ����";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ���б��˻���ϸ����";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "�г�ҩ�˻���ϸ����";
                            }
                        }
                        break;
                }
                #endregion
            }
			if(dtsetAll.Tables.Count>0)
			{
                int showPage = pagenuber + 1;
                e.Graphics.DrawString("��" + showPage.ToString() + "ҳ", objFontNormal, Brushes.Black, (PageWith - fltLeftIndentProp - fltRightIndentProp - e.Graphics.MeasureString("��" + showPage.ToString() + "ҳ", objFontNormal).Width) / 2, PageHigh - 50);
				e.Graphics.DrawString("�Ʊ�:"+strMan,objFontNormal,Brushes.Black,PageWith-fltRightIndentProp-150,PageHigh-50);
				if(pagenuber==0)
				{
					Y+=50;
					e.Graphics.DrawString(titleName,objFontTitle,Brushes.Black,(PageWith-fltLeftIndentProp-fltRightIndentProp-e.Graphics.MeasureString(this.m_objComInfo.m_strGetHospitalTitle()+"��ҩ�б������ϸ����",objFontTitle).Width)/2,Y);
				}
				for(int i1=currdt;i1<dtsetAll.Tables.Count;i1++)
				{
					if(Y+fltRowHeight*5>PageHigh-60)
					{
						currdt=i1;
						currRow=0;
                        //pagenuber++;
						e.HasMorePages=true;
						return;
					}
					if(currRow==0)
						m_mthPrintTit(datestart,dateEnd,strSTANDARD,strSTORAGETYPEID,strSIGN,dtsetAll.Tables[i1].TableName,e);

					for(int f2=currRow;f2<dtsetAll.Tables[i1].Rows.Count;f2++)
					{
						if(i1==1)
						{
							temY=0;
						}
						if(f2>0)
						{
							Y+=fltRowHeight;
						}
						if(Y+fltRowHeight*2>PageHigh-60)
						{
							currdt=i1;
							currRow=f2;
                            //pagenuber++;
							e.HasMorePages=true;
							return;
						}
                        m_mthPrintBady(dtsetAll.Tables[i1].Rows[f2], strSTANDARD,strSIGN,e);
						if(f2==dtsetAll.Tables[i1].Rows.Count-1)
						{
							currRow=0;
						}
					}
                    if (dtsetAll.Tables[i1].Rows.Count > 0)
                    {
                        m_mthPrintEnd(dtsetAll.Tables[i1], strMan, strSTANDARD, strSIGN, e);
                    }
				}
			}

            Y = 0;
            currdt = 0;
            currRow = 0;
            pagenuber = 1;
            
		}

		private void m_mthPrintTit(string datestart,string dateEnd,string strSTANDARD,string strSTORAGETYPEID,string strSIGN,string strVendor,System.Drawing.Printing.PrintPageEventArgs e)
		{
			X=fltLeftIndentProp;
			Y+=fltRowHeight*2;
            //if (strSIGN == "1")
            //{
                e.Graphics.DrawString("��λ:", objFontNormal, Brushes.Black, X, Y);
                X += e.Graphics.MeasureString("��λ:", objFontNormal).Width;
                e.Graphics.DrawString(strVendor, objFontNormal, Brushes.Black, X, Y);
                X = PageWith - 250;
                e.Graphics.DrawString("�������:", objFontNormal, Brushes.Black, X, Y);
                X += e.Graphics.MeasureString("�������:", objFontNormal).Width;
                e.Graphics.DrawString(datestart + "��" + dateEnd, objFontNormal, Brushes.Black, X, Y);
                Y += fltRowHeight;
                X = fltLeftIndentProp;
                temY = Y;
                e.Graphics.DrawLine(PenLine, X, Y, PageWith - fltRightIndentProp, Y);
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);
                X += fltrithgwith;
                float tempHight = 18;
                e.Graphics.DrawString("�� ��", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("�� ��", objFontNormal).Width;
                e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);

                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);
                X += fltrithgwith;
                e.Graphics.DrawString("ҩƷ���Ƽ����", objFontNormal, Brushes.Black, X + 50, Y + tempHight);
                X += e.Graphics.MeasureString("ҩƷ���Ƽ����", objFontNormal).Width + 150;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith + 10;
                e.Graphics.DrawString("��������", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("��������", objFontNormal).Width;
                X += 10;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("��", objFontNormal, Brushes.Black, X, Y + 8);
                e.Graphics.DrawString("λ", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("λ", objFontNormal).Width;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                e.Graphics.DrawString("�����", objFontNormal, Brushes.Black, X + 180, Y + 5);
                e.Graphics.DrawLine(PenLine, X, Y + 20, X + 448, Y + 20);
                X += fltrithgwith;
                e.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("���۽��", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("���۽��", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);
                e.Graphics.DrawString("�� ��", objFont, Brushes.Black, X, Y + 20);
                e.Graphics.DrawString("�� ��", objFont, Brushes.Black, X, Y + 31);
                X += e.Graphics.MeasureString("�� ��", objFont).Width;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("�б���", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("�б���", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);
                e.Graphics.DrawString("�� ��", objFont, Brushes.Black, X, Y + 20);
                e.Graphics.DrawString("�� ��", objFont, Brushes.Black, X, Y + 31);
                X += e.Graphics.MeasureString("�� ��", objFont).Width;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("�޼۽��", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("�޼۽��", objFontNormal).Width;
                X += fltrithgwith;

                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);
                e.Graphics.DrawString("�� ��", objFont, Brushes.Black, X, Y + 20);
                e.Graphics.DrawString("�� ��", objFont, Brushes.Black, X, Y + 31);
                X += e.Graphics.MeasureString("�� ��", objFont).Width;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("���۽��", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("���۽��", objFontNormal).Width;
                X += fltrithgwith;

                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("��׼�ĺ�", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("��׼�ĺ�", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);


                X += fltrithgwith;
                e.Graphics.DrawString("��Ч��", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("��Ч��", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeightTit);
                Y += fltRowHeightTit;
                e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y, PageWith - fltRightIndentProp, Y);
            //}
            //else
            //{
            //    e.Graphics.DrawString("��λ:", objFontNormal, Brushes.Black, X, Y);
            //    X += e.Graphics.MeasureString("��λ:", objFontNormal).Width;
            //    e.Graphics.DrawString(strVendor, objFontNormal, Brushes.Black, X, Y);
            //    X = PageWith - 250;

            //    e.Graphics.DrawString("��������:", objFontNormal, Brushes.Black, X, Y);
            //    X += e.Graphics.MeasureString("�������:", objFontNormal).Width;
            //    e.Graphics.DrawString(datestart + "��" + dateEnd, objFontNormal, Brushes.Black, X, Y);
            //    Y += fltRowHeight;
            //    X = fltLeftIndentProp;
            //    temY = Y;
            //    e.Graphics.DrawLine(PenLine, X, Y, PageWith - fltRightIndentProp, Y);
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);
            //    X += fltrithgwith;
            //    e.Graphics.DrawString("�� ��", objFontNormal, Brushes.Black, X, Y + 15);

            //    X += e.Graphics.MeasureString("�� ��", objFontNormal).Width;
            //    e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);

            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);
            //    X += fltrithgwith;
            //    e.Graphics.DrawString("ҩƷ���Ƽ����", objFontNormal, Brushes.Black, X + 50, Y + 15);
            //    X += e.Graphics.MeasureString("ҩƷ���Ƽ����", objFontNormal).Width + 158;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    X += fltrithgwith + 10;
            //    e.Graphics.DrawString("��������", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("��������", objFontNormal).Width;
            //    X += 10;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("��", objFontNormal, Brushes.Black, X, Y + 5);
            //    e.Graphics.DrawString("λ", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("λ", objFontNormal).Width;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);
            //    e.Graphics.DrawString("������", objFontNormal, Brushes.Black, X + 180, Y + 5);
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X + 396, Y + 20);
            //    X += fltrithgwith;
            //    e.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;

            //    e.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;

            //    e.Graphics.DrawString("������", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("���۽��", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

            //    //			X+=fltrithgwith;
            //    e.Graphics.DrawString("�б����", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("�б����", objFontNormal).Width;
            //    //			X+=fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("�б���", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("�б���", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);


            //    //			X+=fltrithgwith;
            //    e.Graphics.DrawString("�����޼�", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("�����޼�", objFontNormal).Width;
            //    //			X+=fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("�޼۽��", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("�޼۽��", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("��׼�ĺ�", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("��׼�ĺ�", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);


            //    X += fltrithgwith;
            //    e.Graphics.DrawString("��Ч��", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("��Ч��", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    //			X+=fltrithgwith;
            //    //			e.Graphics.DrawString("����",objFontNormal,Brushes.Black,X,Y+15);
            //    //			X+=e.Graphics.MeasureString("����",objFontNormal).Width;
            //    //			X+=fltrithgwith;
            //    //			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeightTit);
            //    Y += fltRowHeightTit;
            //    e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y, PageWith - fltRightIndentProp, Y);
            //}
		}
		private void m_mthPrintBady(DataRow dtRow,string strSTANDARD,string strSIGN,System.Drawing.Printing.PrintPageEventArgs e)
		{
            //if (strSIGN != "1")
            //{
            //    X = fltLeftIndentProp;
            //    e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y, PageWith - fltRightIndentProp, Y);
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //���
            //    if (dtRow["���"].ToString().Length > 6)
            //    {
            //        e.Graphics.DrawString(dtRow["���"].ToString().Substring(0, 6), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["���"].ToString().Substring(6), objFont, Brushes.Black, X, Y + 10);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["���"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }

            //    X += e.Graphics.MeasureString("�� ��", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //ҩƷ����
            //    if (e.Graphics.MeasureString(dtRow["ҩƷ����"].ToString().Trim(), objFontNormal).Width > 190.944)
            //    {
            //        e.Graphics.DrawString(dtRow["ҩƷ����"].ToString().Substring(0, 13), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["ҩƷ����"].ToString().Substring(13), objFont, Brushes.Black, X, Y + 13);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["ҩƷ����"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }

            //    X += 160;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

            //    //ҩƷ���
            //    if (e.Graphics.MeasureString(dtRow["���"].ToString().Trim(), objFontNormal).Width > 120)
            //    {
            //        e.Graphics.DrawString(dtRow["���"].ToString().Substring(0, 12), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["���"].ToString().Substring(12), objFont, Brushes.Black, X, Y + 13);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["���"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }

            //    X += e.Graphics.MeasureString("ҩƷ���Ƽ����", objFontNormal).Width;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //����
            //    e.Graphics.DrawString(dtRow["����"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //��������
            //    if (e.Graphics.MeasureString(dtRow["��������"].ToString().Trim(), objFontNormal).Width > 90 && dtRow["��������"].ToString().Length > 7)
            //    {
            //        e.Graphics.DrawString(dtRow["��������"].ToString().Substring(0, 7), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["��������"].ToString().Substring(7), objFont, Brushes.Black, X, Y + 13);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["��������"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }
            //    X += e.Graphics.MeasureString("��������", objFontNormal).Width;
            //    X += fltrithgwith + 20;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //��λ
            //    X += fltrithgwith;
            //    e.Graphics.DrawString(dtRow["��λ"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("λ", objFontNormal).Width;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //����
            //    e.Graphics.DrawString(dtRow["����"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //����
            //    e.Graphics.DrawString(dtRow["����"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //���۽��
            //    e.Graphics.DrawString(dtRow["���۽��"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("���۽��", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //�б����
            //    //if (strSTANDARD == "1")
            //        e.Graphics.DrawString(dtRow["�б����"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    //else
            //    //    e.Graphics.DrawString("0", objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("�б����", objFontNormal).Width;
            //    //			X+=fltrithgwith*2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //�б���
            //    //if (strSTANDARD == "1")
            //        e.Graphics.DrawString(dtRow["�б���"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    //else
            //    //    e.Graphics.DrawString("0", objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("�б���", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //�����޼�
            //    e.Graphics.DrawString(dtRow["�����޼�"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("�����޼�", objFontNormal).Width;
            //    //			X+=fltrithgwith*2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //�޼۽��
            //    e.Graphics.DrawString(dtRow["�޼۽��"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("�޼۽��", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //����
            //    if (dtRow["����"].ToString().Trim().Length > 5)
            //    {
            //        e.Graphics.DrawString(dtRow["����"].ToString().Substring(0, 5), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["����"].ToString().Substring(5), objFont, Brushes.Black, X, Y + 10);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["����"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //��׼�ĺ�
            //    if (e.Graphics.MeasureString(dtRow["��׼�ĺ�"].ToString().Trim(), objFontNormal).Width > 117.504)
            //    {
            //        e.Graphics.DrawString(dtRow["��׼�ĺ�"].ToString().Substring(0, 8), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["��׼�ĺ�"].ToString().Substring(8), objFont, Brushes.Black, X, Y + 10);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["��׼�ĺ�"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }
            //    X += e.Graphics.MeasureString("��׼�ĺ�", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

            //    //��Ч
            //    if (dtRow["ʧЧ��"] != null && dtRow["ʧЧ��"].ToString() != "")
            //    {
            //        e.Graphics.DrawString(DateTime.Parse(dtRow["ʧЧ��"].ToString()).ToString("yy/MM/dd"), objFont, Brushes.Black, X, Y + 5);
            //    }
            //    X += e.Graphics.MeasureString("ʧЧ��", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

            //    //			//����
            //    //			e.Graphics.DrawString("�ϸ�",objFontNormal,Brushes.Black,X+fltrithgwith,Y+5);
            //    //			X+=e.Graphics.MeasureString("����",objFontNormal).Width;
            //    //			X+=fltrithgwith*2;
            //    //			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

            //    //����
            //    if (dtRow["����"] != null && dtRow["����"].ToString() != "")
            //    {
            //        e.Graphics.DrawString(DateTime.Parse(dtRow["����"].ToString()).ToString("yy/MM/dd"), objFont, Brushes.Black, X, Y + 5);
            //    }
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);
            //    e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y + fltRowHeight, PageWith - fltRightIndentProp, Y + fltRowHeight);

            //}
            //else
            //{
                X = fltLeftIndentProp;
                e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y, PageWith - fltRightIndentProp, Y);
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //���
                if (dtRow["���"].ToString().Length > 6)
                {
                    e.Graphics.DrawString(dtRow["���"].ToString().Substring(0, 6), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["���"].ToString().Substring(6), objFont, Brushes.Black, X, Y + 10);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["���"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }

                X += e.Graphics.MeasureString("�� ��", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //ҩƷ����
                if (e.Graphics.MeasureString(dtRow["ҩƷ����"].ToString().Trim(), objFontNormal).Width > 190.944)
                {
                    e.Graphics.DrawString(dtRow["ҩƷ����"].ToString().Substring(0, 13), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["ҩƷ����"].ToString().Substring(13), objFont, Brushes.Black, X, Y + 13);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["ҩƷ����"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }

                X += 160;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                //ҩƷ���
                if (e.Graphics.MeasureString(dtRow["���"].ToString().Trim(), objFontNormal).Width > 120)
                {
                    e.Graphics.DrawString(dtRow["���"].ToString().Substring(0, 12), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["���"].ToString().Substring(12), objFont, Brushes.Black, X, Y + 13);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["���"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }
                float tempwith = 8;
                X += e.Graphics.MeasureString("ҩƷ���Ƽ����", objFontNormal).Width - tempwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //����
                e.Graphics.DrawString(dtRow["����"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //��������
                if (e.Graphics.MeasureString(dtRow["��������"].ToString().Trim(), objFontNormal).Width > 90 && dtRow["��������"].ToString().Length > 7)
                {
                    e.Graphics.DrawString(dtRow["��������"].ToString().Substring(0, 7), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["��������"].ToString().Substring(7), objFont, Brushes.Black, X, Y + 13);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["��������"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }
                X += e.Graphics.MeasureString("��������", objFontNormal).Width;
                X += fltrithgwith + 20;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //��λ
                X += fltrithgwith;
                e.Graphics.DrawString(dtRow["��λ"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("λ", objFontNormal).Width;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //����
                float tempwidth = 0;
                //float tempwidth = e.Graphics.MeasureString("����", objFontNormal).Width - e.Graphics.MeasureString(dtRow["����"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["����"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //����
                //tempwidth = e.Graphics.MeasureString("����", objFontNormal).Width - e.Graphics.MeasureString(dtRow["����"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["����"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //���۽��
                tempwidth = e.Graphics.MeasureString("���۽��", objFontNormal).Width - e.Graphics.MeasureString(dtRow["���۽��"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["���۽��"].ToString(), objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                X += e.Graphics.MeasureString("���۽��", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //�б����
                //if (strSTANDARD == "1")
                //{
                //    //tempwidth = e.Graphics.MeasureString("�� ��", objFontNormal).Width - e.Graphics.MeasureString(dtRow["�б����"].ToString(), objFontNormal).Width;
                    e.Graphics.DrawString(dtRow["�б����"].ToString(), objFontNormal, Brushes.Black, X , Y + 5);
                //}
                //else
                //{
                //    tempwidth = e.Graphics.MeasureString("0", objFontNormal).Width - e.Graphics.MeasureString(dtRow["�б����"].ToString(), objFontNormal).Width;
                //    e.Graphics.DrawString("0", objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                //}
                X += e.Graphics.MeasureString("�� ��", objFont).Width;

                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //�б���
                //if (strSTANDARD == "1")
                //{
                    tempwidth = e.Graphics.MeasureString("�б���", objFontNormal).Width - e.Graphics.MeasureString(dtRow["�б���"].ToString(), objFontNormal).Width;
                    e.Graphics.DrawString(dtRow["�б���"].ToString(), objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                //}
                //else
                //{
                //    tempwidth = e.Graphics.MeasureString("0", objFontNormal).Width - e.Graphics.MeasureString(dtRow["�б���"].ToString(), objFontNormal).Width;
                //    e.Graphics.DrawString("0", objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                //}
                X += e.Graphics.MeasureString("�б���", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //�����޼�
                //tempwidth = e.Graphics.MeasureString("�� ��", objFontNormal).Width - e.Graphics.MeasureString(dtRow["�����޼�"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["�����޼�"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("�� ��", objFont).Width;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //�޼۽��
                tempwidth = e.Graphics.MeasureString("�޼۽��", objFontNormal).Width - e.Graphics.MeasureString(dtRow["�޼۽��"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["�޼۽��"].ToString(), objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                X += e.Graphics.MeasureString("�޼۽��", objFontNormal).Width;
                X += fltrithgwith * 2 + 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                //���۵���
                //tempwidth = e.Graphics.MeasureString("�� ��", objFontNormal).Width - e.Graphics.MeasureString(dtRow["���۵���"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["���۵���"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("�� ��", objFont).Width;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //���۽��
                 tempwidth = e.Graphics.MeasureString("���۽��", objFontNormal).Width-e.Graphics.MeasureString(dtRow["���۽��"].ToString(), objFontNormal).Width;

                e.Graphics.DrawString(dtRow["���۽��"].ToString(), objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                X += e.Graphics.MeasureString("���۽��", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                //����
                if (dtRow["����"].ToString().Trim().Length > 5)
                {
                    e.Graphics.DrawString(dtRow["����"].ToString().Substring(0, 5), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["����"].ToString().Substring(5), objFont, Brushes.Black, X, Y + 10);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["����"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //��׼�ĺ�
                if (e.Graphics.MeasureString(dtRow["��׼�ĺ�"].ToString().Trim(), objFontNormal).Width > 117.504)
                {
                    e.Graphics.DrawString(dtRow["��׼�ĺ�"].ToString().Substring(0, 8), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["��׼�ĺ�"].ToString().Substring(8), objFont, Brushes.Black, X, Y + 10);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["��׼�ĺ�"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }
                X += e.Graphics.MeasureString("��׼�ĺ�", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                //��Ч
                if (dtRow["ʧЧ��"] != null && dtRow["ʧЧ��"].ToString() != "")
                {
                    e.Graphics.DrawString(DateTime.Parse(dtRow["ʧЧ��"].ToString()).ToString("yy/MM/dd"), objFont, Brushes.Black, X, Y + 5);
                }
                X += e.Graphics.MeasureString("ʧЧ��", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                //			//����
                //			e.Graphics.DrawString("�ϸ�",objFontNormal,Brushes.Black,X+fltrithgwith,Y+5);
                //			X+=e.Graphics.MeasureString("����",objFontNormal).Width;
                //			X+=fltrithgwith*2;
                //			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

                //����
                if (dtRow["����"] != null && dtRow["����"].ToString() != "")
                {
                    e.Graphics.DrawString(DateTime.Parse(dtRow["����"].ToString()).ToString("yy/MM/dd"), objFont, Brushes.Black, X, Y + 5);
                }
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);
                e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y + fltRowHeight, PageWith - fltRightIndentProp, Y + fltRowHeight);
            //}
			
		}
        private void m_mthPrintEnd(DataTable dt, string strMan, string strSTANDARD, string strSIGN, System.Drawing.Printing.PrintPageEventArgs e)
		{
			Double AIMUNITPRICEMONEY=0;
			Double LIMITUNITPRICEMONEY=0;
			Double TOTAILMONEY=0;
            Double SALTOTAILMONEY = 0;
			for(int i1=0;i1<dt.Rows.Count;i1++)
			{

                    if (strSTANDARD == "1" && dt.Rows[i1]["�б���"] != null && dt.Rows[i1]["�б���"].ToString()!="")
					AIMUNITPRICEMONEY+=Double.Parse(dt.Rows[i1]["�б���"].ToString());
                    if (dt.Rows[i1]["�޼۽��"] != null && dt.Rows[i1]["�޼۽��"].ToString() != "")
					LIMITUNITPRICEMONEY+=Double.Parse(dt.Rows[i1]["�޼۽��"].ToString());
                if (dt.Rows[i1]["���۽��"] != null && dt.Rows[i1]["���۽��"].ToString() != "")
					TOTAILMONEY+=Double.Parse(dt.Rows[i1]["���۽��"].ToString());
                if (dt.Rows[i1]["���۽��"] != null && dt.Rows[i1]["���۽��"].ToString() != "")
                    SALTOTAILMONEY += Double.Parse(dt.Rows[i1]["���۽��"].ToString());
			}
            //if (strSIGN == "1")
            //{
                X = fltLeftIndentProp;
                Y += fltRowHeight;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                e.Graphics.DrawString("��  ��", objFontTitle, Brushes.Black, X + 50, Y + 5);
                X += e.Graphics.MeasureString("�� ��", objFontNormal).Width;
                X += fltrithgwith + 160;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                X += (e.Graphics.MeasureString("ҩƷ���Ƽ����", objFontNormal).Width - 5);
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += e.Graphics.MeasureString("��������", objFontNormal).Width;
                X += fltrithgwith * 2;
                X += e.Graphics.MeasureString("λ", objFontNormal).Width;
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += fltrithgwith * 3 + 17;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                e.Graphics.DrawString(TOTAILMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
                X += e.Graphics.MeasureString("����", objFontNormal).Width;
                X += fltrithgwith * 2;
                X += e.Graphics.MeasureString("���۽��", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y + fltRowHeight, PageWith - fltRightIndentProp, Y + fltRowHeight);
                e.Graphics.DrawString(AIMUNITPRICEMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
                X += e.Graphics.MeasureString("�� ��", objFont).Width;
                X += e.Graphics.MeasureString("�б���", objFontNormal).Width;
                X += fltrithgwith * 4 - 4;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                e.Graphics.DrawString(LIMITUNITPRICEMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
                X += e.Graphics.MeasureString("�� ��", objFontNormal).Width;
                X += e.Graphics.MeasureString("�޼۽��", objFontNormal).Width;
                X += fltrithgwith * 4 - 6;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                e.Graphics.DrawString(SALTOTAILMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
                X += e.Graphics.MeasureString("�� ��", objFontNormal).Width;
                X += e.Graphics.MeasureString("���۽��", objFontNormal).Width;
                X += fltrithgwith * 4 - 8;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);
            //}
            //else
            //{
            //    X = fltLeftIndentProp;
            //    Y += fltRowHeight;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    e.Graphics.DrawString("��  ��", objFontTitle, Brushes.Black, X + 50, Y + 5);
            //    X += e.Graphics.MeasureString("�� ��", objFontNormal).Width;
            //    X += fltrithgwith + 165;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    X += (e.Graphics.MeasureString("ҩƷ���Ƽ����", objFontNormal).Width - 5);
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += e.Graphics.MeasureString("��������", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    X += e.Graphics.MeasureString("λ", objFontNormal).Width;
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += fltrithgwith * 3 + 20;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    e.Graphics.DrawString(TOTAILMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
            //    X += e.Graphics.MeasureString("����", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    X += e.Graphics.MeasureString("���۽��", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y + fltRowHeight, PageWith - fltRightIndentProp, Y + fltRowHeight);
            //    e.Graphics.DrawString(AIMUNITPRICEMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
            //    X += e.Graphics.MeasureString("�б���", objFontNormal).Width * 2;
            //    X += fltrithgwith * 4-4 ;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    e.Graphics.DrawString(LIMITUNITPRICEMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
            //    X += e.Graphics.MeasureString("�б���", objFontNormal).Width * 2;
            //    X += fltrithgwith * 4-4;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);

            //}

		}

	}
	}
