using System;
using System.Data;
using System.Drawing;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlIatricalReport ��ժҪ˵����
	/// </summary>
	public class clsControlIatricalReport: com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlIatricalReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmIatricalReport m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmIatricalReport)frmMDI_Child_Base_in;
		}
		#endregion

		#region ͳ������
		clsDomainControl_Register doMain=new clsDomainControl_Register();
		private DataTable dtEmp=new DataTable();
		private DataTable dtPayType=new DataTable();
		private string startDate;
		private string EndDate;
		/// <summary>
		/// ��־�շ�����0-�Էѣ�1-���ѣ�2-ҽ����3-����,4-�����ԷѲ���,5-ҽ����ˢ��
		/// </summary>
		private string strIsOur="0";
		/// <summary>
		/// -1,ͳ������������ݣ�0-ͳ���������ݣ�1-ͳ�ƺ�����ݡ�
		/// </summary>
		private string strIsMB="-1";
		public void m_getData()
		{
			DataTable dtCheckOut;
			DataTable dtCheckOutDe;
			startDate=this.m_objViewer.startDate.Value.ToShortDateString();
			EndDate=this.m_objViewer.endDate.Value.ToShortDateString();
			doMain.m_lngGetIatrical(startDate,EndDate,out dtPayType,out dtCheckOut,out dtCheckOutDe,out dtEmp,this.m_objViewer.m_CboSeleChargeMan.Item.sValue[this.m_objViewer.m_CboSeleChargeMan.SelectedIndex].ToString(),this.m_objViewer.m_getData);
			switch(this.m_objViewer.m_getData)
			{
				case "-1":
					strIsOur="2";
					strIsMB="-1";
					this.m_objViewer.Text="ҽ��ͳ�Ʊ���";
					break;
				case "0":
					strIsMB="0";
					strIsOur="2";
					this.m_objViewer.Text="ҽ��ͳ�Ʊ���(����)";
					break;
				case "1":
					strIsMB="1";
					strIsOur="2";
					this.m_objViewer.Text="ҽ��ͳ�Ʊ���(���)";
					break;

				case "2":
					strIsMB="-1";
					strIsOur="1";
					this.m_objViewer.Text="����ͳ�Ʊ���";
					break;
				case "3":
					strIsMB="0";
					strIsOur="1";
					this.m_objViewer.Text="����ͳ�Ʊ���(����)";
					break;
				case "4":
					strIsMB="1";
					strIsOur="1";
					this.m_objViewer.Text="����ͳ�Ʊ���(���)";
					break;

				case "5":
					strIsMB="-1";
					strIsOur="0";
					this.m_objViewer.Text="�Է�ͳ�Ʊ���";
					break;
				case "6":
					strIsMB="0";
					strIsOur="0";
					this.m_objViewer.Text="�Է�ͳ�Ʊ���(����)";
					break;
				case "7":
					strIsMB="1";
					strIsOur="0";
					this.m_objViewer.Text="�Է�ͳ�Ʊ���(���)";
					break;

				case "8":
					strIsMB="-1";
					strIsOur="3";
					this.m_objViewer.Text="����ͳ�Ʊ���";
					break;

				case "9":
					strIsMB="0";
					this.m_objViewer.Text="����ͳ�Ʊ���(����)";
					strIsOur="3";
					break;
				case "10":
					strIsMB="1";
					strIsOur="3";
					this.m_objViewer.Text="����ͳ�Ʊ���(���)";
					break;

				case "11":
					strIsMB="-1";
					strIsOur="4";
					this.m_objViewer.Text="����(�ԷѲ���)����";
					break;

				case "12":
					strIsMB="0";
					this.m_objViewer.Text="����(�ԷѲ���)����(����)";
					strIsOur="4";
					break;
				case "13":
					strIsMB="1";
					strIsOur="4";
					this.m_objViewer.Text="����(�ԷѲ���)����(���)";
					break;


				case "14":
					strIsMB="-1";
					strIsOur="5";
					this.m_objViewer.Text="ҽ�����ʼ�ˢ������";
					break;

				case "15":
					strIsMB="0";
					this.m_objViewer.Text="ҽ�����ʼ�ˢ������";
					strIsOur="5";
					break;
				case "16":
					strIsMB="1";
					strIsOur="5";
					this.m_objViewer.Text="ҽ�����ʼ�ˢ������";
					break;
			}
			#region ͳ�Ƹ����շ����͵Ľ��
			dtPayType.Columns.Add("tolMoney");
			double data3;
			double data4;
			if(dtCheckOutDe.Rows.Count>=0)
			{
				for(int i1=0;i1<dtPayType.Rows.Count;i1++)
				{
					Double tolMoney=0;
					for(int f2=0;f2<dtCheckOutDe.Rows.Count;f2++)
					{
						
						if(dtCheckOutDe.Rows[f2]["ITEMOPCALCTYPE_CHR"].ToString().Trim()==dtPayType.Rows[i1]["TYPEID_CHR"].ToString().Trim())
						{
							if(strIsOur=="1"||strIsOur=="2"||strIsOur=="3")
							{
								data3=Convert.ToDouble(dtCheckOutDe.Rows[f2]["TOLPRICE_MNY"].ToString().Trim());
								data4=Convert.ToDouble(dtCheckOutDe.Rows[f2]["TOLFEE_MNY"].ToString().Trim());
								tolMoney=tolMoney+data4-data3;
					
							}
							else if(strIsOur=="5")
							{
								if(dtCheckOutDe.Rows[f2]["PAYTYPE_INT"].ToString()=="1")
								{
									tolMoney+=Convert.ToDouble(dtCheckOutDe.Rows[f2]["TOLPRICE_MNY"].ToString().Trim());
								}
								if(dtCheckOutDe.Rows[f2]["INTERNALFLAG_INT"].ToString()=="2")
								{
									data3=Convert.ToDouble(dtCheckOutDe.Rows[f2]["TOLPRICE_MNY"].ToString().Trim());
									data4=Convert.ToDouble(dtCheckOutDe.Rows[f2]["TOLFEE_MNY"].ToString().Trim());
									tolMoney=tolMoney+data4-data3;
								}
								

							}
							else
							{
								if(strIsOur=="0")
								{
									if(dtCheckOutDe.Rows[f2]["PAYTYPE_INT"].ToString()!="1"&&dtCheckOutDe.Rows[f2]["PAYTYPE_INT"].ToString()!="3")
										tolMoney+=Convert.ToDouble(dtCheckOutDe.Rows[f2]["TOLPRICE_MNY"].ToString().Trim());
								}
								else
								{
									if(dtCheckOutDe.Rows[f2]["PAYTYPE_INT"].ToString()!="1"&&dtCheckOutDe.Rows[f2]["PAYTYPE_INT"].ToString()!="3")
										tolMoney+=Convert.ToDouble(dtCheckOutDe.Rows[f2]["TOLPRICE_MNY"].ToString().Trim());
								}
							}
						}
					}
					dtPayType.Rows[i1]["tolMoney"]=tolMoney.ToString("0.00");
				}
			}
			#endregion
			dtEmp.Columns.Add("ҽ�����ʺϼƽ��");
			dtEmp.Columns.Add("ҽ���˴���");
			decimal data1;
			decimal data2;
			for(int i1=0;i1<dtCheckOut.Rows.Count;i1++)
			{
					for(int f2=0;f2<dtEmp.Rows.Count;f2++)
					{
						if(dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"].ToString()=="")
						{
							dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"]=0;
						}
						if(dtEmp.Rows[f2]["ҽ���˴���"].ToString()=="")
						{
							dtEmp.Rows[f2]["ҽ���˴���"]=0;
						}
						if(dtCheckOut.Rows[i1]["BALANCEEMP_CHR"].ToString()==dtEmp.Rows[f2]["BALANCEEMP_CHR"].ToString())
						{
							if(strIsOur=="1"||strIsOur=="2"||strIsOur=="3")
							{
								data1=decimal.Parse(dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"].ToString());
								data2=decimal.Parse(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
								dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"]=data1+data2;
							}
							else if(strIsOur=="5")
							{
								if(dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString()=="1")
								{
									data1=decimal.Parse(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString().Trim());
									data2=decimal.Parse(dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"].ToString());
									dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"]=data1+data2;
								}
								if(dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString()=="2")
								{
									data1=decimal.Parse(dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"].ToString());
									data2=decimal.Parse(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
									dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"]=data1+data2;
								}
								

							}
							else
							{
								if(strIsOur=="0")
								{
									if(dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString()!="1"&&dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString()!="3")
									{
										data1=decimal.Parse(dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"].ToString());
										data2=decimal.Parse(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
										dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"]=data1+data2;
									}
								}
								else
								{
									if(dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString()!="1"&&dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString()!="3")
									{
										data1=decimal.Parse(dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"].ToString());
										data2=decimal.Parse(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
										dtEmp.Rows[f2]["ҽ�����ʺϼƽ��"]=data1+data2;
									}
								}
							}
							dtEmp.Rows[f2]["ҽ���˴���"]=Convert.ToInt32(dtEmp.Rows[f2]["ҽ���˴���"])+1;
						}
					}
			}

			#region ��Ӻϼ���
			int totalNumber=0;
			double tolalMoney=0;
			if(dtEmp.Rows.Count>0)
			{
				for(int i1=0;i1<dtEmp.Rows.Count;i1++)
				{
					if(dtEmp.Rows[i1]["ҽ�����ʺϼƽ��"].ToString()!="0")
					{
						tolalMoney+=Convert.ToDouble(dtEmp.Rows[i1]["ҽ�����ʺϼƽ��"]);
						totalNumber+=Convert.ToInt32(dtEmp.Rows[i1]["ҽ���˴���"]);
					}
				}
			}
			DataRow newRow=dtEmp.NewRow();
			newRow["BALANCEEMP_CHR"]="11111";
			newRow["LASTNAME_VCHR"]="ͳ����";
			newRow["ҽ�����ʺϼƽ��"]=tolalMoney;
			newRow["ҽ���˴���"]=totalNumber;
			dtEmp.Rows.Add(newRow);
			#endregion
		}
		#endregion


		public void printPageFS(System.Drawing.Printing.PrintPageEventArgs e)
		{
			#region ����
			float PageWidth=e.PageBounds.Width;//���ҳ��Ŀ��
			float PageHight=e.PageBounds.Height;//���ҳ��ĸ߶�
			float curRowY=0;//��ǰ�е�Y����
			float curRowX=0;//��ǰ�е�X����
			System.Drawing.Font m_fntTitle=new Font("����",15);//����ʹ�õ�����
			System.Drawing.Font TextFont=new Font("����",11);//����ʹ�õ�����
			System.Drawing.Font TextFontBold=new Font("����",11,System.Drawing.FontStyle.Bold);//����ʹ�õ�����(�Ӵ֣�
			const float RowHight=25F;//��ĸ߶�
			const float LeftWith=30F;//�����޽��ĳ���
			const float Uphight=15F;//�����޽��ĳ���
			const float fontHight=7;//���ڱ������ʾ��λ��
			float SaveStartHight=0;
			#endregion

			#region ͷ��
			Pen penLine=new Pen(Brushes.Black,1);
			curRowY=RowHight+Uphight+10;
			curRowX+=LeftWith;
			string strName=this.m_objComInfo.m_strGetHospitalTitle();
			string strTilName="";
			string strMoneyName="";
			string strCountMan="0";
			switch(strIsOur)
			{
				case "0":
					if(strIsMB=="-1")
						strTilName="�Է�ͳ�Ʊ���";
					if(strIsMB=="0")
						strTilName="�Է�ͳ�Ʊ���������";
					if(strIsMB=="1")
						strTilName="�Է�ͳ�Ʊ�����ᣩ";
					strMoneyName="�ԷѺϼƽ��";
					strCountMan="�Է��˴���";
					break;
				case "1":
					if(strIsMB=="-1")
						strTilName="����ͳ�Ʊ���";
					if(strIsMB=="0")
						strTilName="����ͳ�Ʊ���������";
					if(strIsMB=="1")
						strTilName="����ͳ�Ʊ�����ᣩ";
					strMoneyName="���ʺϼƽ��";
					strCountMan="�����˴���";
					break;
				case "2":
					if(strIsMB=="-1")
						strTilName="ҽ��ͳ�Ʊ���";
					if(strIsMB=="0")
						strTilName="ҽ��ͳ�Ʊ���������";
					if(strIsMB=="1")
						strTilName="ҽ��ͳ�Ʊ�����ᣩ";
					strMoneyName="���ʺϼƽ��";
					strCountMan="ҽ���˴���";
					break;
				case "3":
					if(strIsMB=="-1")
						strTilName="����ͳ�Ʊ���";
					if(strIsMB=="0")
						strTilName="����ͳ�Ʊ���������";
					if(strIsMB=="1")
						strTilName="����ͳ�Ʊ�����ᣩ";
					strMoneyName="���ʺϼƽ��";
					strCountMan="�����˴���";
					break;
				case "4":
					if(strIsMB=="-1")
						strTilName="����(�ԷѲ���)����";
					if(strIsMB=="0")
						strTilName="����(�ԷѲ���)����������";
					if(strIsMB=="1")
						strTilName="����(�ԷѲ���)������ᣩ";
					strMoneyName="�ԷѺϼƽ��";
					strCountMan="�Է��˴���";
					break;
				case "5":
					if(strIsMB=="-1")
						strTilName="ҽ�����ʼ�ˢ��ͳ�Ʊ���";
					if(strIsMB=="0")
						strTilName="ҽ�����ʼ�ˢ��ͳ�Ʊ���������";
					if(strIsMB=="1")
						strTilName="ҽ�����ʼ�ˢ��ͳ�Ʊ�����ᣩ";
					strMoneyName="�ϼƽ��";
					strCountMan="�˴���";
					break;
			}
			SizeF tilWith= e.Graphics.MeasureString(strName+strTilName,m_fntTitle);
			e.Graphics.DrawString(strName+strTilName,m_fntTitle,Brushes.Black,(PageWidth-tilWith.Width)/2,Uphight);


			e.Graphics.DrawString("�������ڣ�",TextFont,Brushes.Black,curRowX,curRowY);
			tilWith= e.Graphics.MeasureString("�������ڣ�",TextFont);
			curRowX+=tilWith.Width;
			e.Graphics.DrawString("��"+startDate+"��"+EndDate,TextFont,Brushes.Black,curRowX,curRowY);
			e.Graphics.DrawString("��ӡ���ڣ�",TextFont,Brushes.Black,PageWidth-250,curRowY);
			tilWith= e.Graphics.MeasureString("��ӡ���ڣ�",TextFont);
			string NowDate=DateTime.Now.ToShortDateString();
			e.Graphics.DrawString(NowDate,TextFont,Brushes.Black,PageWidth-250+tilWith.Width,curRowY);
			#endregion
			curRowX=LeftWith;
			curRowY+=18;
			SaveStartHight=curRowY;
			#region ����
			int totalnumber=0;
			for(int i1=0;i1<dtEmp.Rows.Count;i1++)
			{
				if(dtEmp.Rows[i1]["ҽ�����ʺϼƽ��"].ToString()!="0"||dtEmp.Rows[i1]["LASTNAME_VCHR"].ToString()=="ͳ����")
				{
					totalnumber++;
					curRowX=LeftWith;
					e.Graphics.DrawLine(penLine,curRowX,curRowY,PageWidth-LeftWith,curRowY);
					e.Graphics.DrawLine(penLine,curRowX,curRowY+RowHight,PageWidth-LeftWith,curRowY+RowHight);
					e.Graphics.DrawLine(penLine,curRowX,curRowY+RowHight*2,PageWidth-LeftWith,curRowY+RowHight*2);
					if(i1==dtEmp.Rows.Count-1)
					{
						int totail=totalnumber-1;
						e.Graphics.DrawString("�ɿ�����",TextFont,Brushes.Black,curRowX+5,curRowY+5);
						tilWith=e.Graphics.MeasureString("�ɿ��� ��",TextFont);
						curRowX+=tilWith.Width;
						e.Graphics.DrawLine(penLine,curRowX,curRowY,curRowX,curRowY+RowHight);
						curRowX+=5;
						e.Graphics.DrawString(totail.ToString()+" ��",TextFont,Brushes.Black,curRowX,curRowY+5);
					}
					else
					{
						e.Graphics.DrawString("�� �� ��",TextFont,Brushes.Black,curRowX+5,curRowY+5);
						tilWith=e.Graphics.MeasureString("�ɿ��� ��",TextFont);
						curRowX+=tilWith.Width;
						e.Graphics.DrawLine(penLine,curRowX,curRowY,curRowX,curRowY+RowHight);
						curRowX+=5;
						e.Graphics.DrawString(dtEmp.Rows[i1]["LASTNAME_VCHR"].ToString(),TextFont,Brushes.Black,curRowX,curRowY+5);
					}
					curRowX+=70;
					e.Graphics.DrawLine(penLine,curRowX,curRowY,curRowX,curRowY+RowHight);
					curRowX+=5;
					if(i1==dtEmp.Rows.Count-1)
					{
						e.Graphics.DrawString("ȫ���ϼƽ��",TextFont,Brushes.Black,curRowX,curRowY+5);
					}
					else
					{
						e.Graphics.DrawString(strMoneyName,TextFont,Brushes.Black,curRowX,curRowY+5);
					}
					tilWith=e.Graphics.MeasureString("���ʺϼƽ��",TextFont);
					curRowX+=tilWith.Width;
					e.Graphics.DrawLine(penLine,curRowX,curRowY,curRowX,curRowY+RowHight);
					curRowX+=5;
					float ChMoney=float.Parse(dtEmp.Rows[i1]["ҽ�����ʺϼƽ��"].ToString());
					string strChMoney=clsMain.CurrencyToString(Math.Abs(ChMoney));
					e.Graphics.DrawString(strChMoney,TextFont,Brushes.Black,curRowX,curRowY+5);
					curRowX+=255;
					e.Graphics.DrawLine(penLine,curRowX,curRowY,curRowX,curRowY+RowHight);
					e.Graphics.DrawString("��"+dtEmp.Rows[i1]["ҽ�����ʺϼƽ��"].ToString(),TextFont,Brushes.Black,curRowX,curRowY+5);
					curRowX+=100;
					e.Graphics.DrawLine(penLine,curRowX,curRowY,curRowX,curRowY+RowHight);
					curRowX+=5;
					if(i1==dtEmp.Rows.Count-1)
					{
						e.Graphics.DrawString("�ϼ��˴���",TextFont,Brushes.Black,curRowX,curRowY+5);
					}
					else
					{
						e.Graphics.DrawString(strCountMan,TextFont,Brushes.Black,curRowX,curRowY+5);
					}
					tilWith=e.Graphics.MeasureString("�ϼ��˴���",TextFont);
					curRowX+=tilWith.Width;
					e.Graphics.DrawLine(penLine,curRowX,curRowY,curRowX,curRowY+RowHight);
					e.Graphics.DrawString(dtEmp.Rows[i1]["ҽ���˴���"].ToString()+"��",TextFont,Brushes.Black,curRowX,curRowY+5);
					curRowY+=RowHight*2;
				}
			}
			#endregion

			#region ����շ���������
			if(dtPayType.Rows.Count>0)
			{
				curRowY+=7;
				float tmpWith=LeftWith+2;
				for(int f2=0;f2<dtPayType.Rows.Count;f2++)
				{
					if(f2%5==0&&f2!=0)
					{
						tmpWith=LeftWith+2;
						curRowY+=RowHight+5;
					}
					tilWith= e.Graphics.MeasureString("��ӡ����",TextFont);
					if(dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Length>4)
					{
						string star=dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(0,4);
						string end=dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim().Substring(4);
						e.Graphics.DrawString(star,TextFont,Brushes.Black,tmpWith,curRowY-5);
						e.Graphics.DrawString(end+"��",TextFont,Brushes.Black,tmpWith,curRowY+10);
						e.Graphics.DrawString(Convert.ToDouble(dtPayType.Rows[f2]["tolMoney"].ToString()).ToString("0.00")+"Ԫ",TextFont,Brushes.Black,tmpWith+tilWith.Width,curRowY);
					}
					else
					{
						e.Graphics.DrawString(dtPayType.Rows[f2]["TYPENAME_VCHR"].ToString().Trim()+"��",TextFont,Brushes.Black,tmpWith,curRowY);
						e.Graphics.DrawString(Convert.ToDouble(dtPayType.Rows[f2]["tolMoney"].ToString()).ToString("0.00")+"Ԫ",TextFont,Brushes.Black,tmpWith+tilWith.Width,curRowY);
					}
					tmpWith+=150;
				}
			}
			curRowY+=RowHight;
			curRowX=LeftWith;
			e.Graphics.DrawLine(penLine,curRowX,curRowY,PageWidth-LeftWith,curRowY);
			curRowX+=2;
			curRowY+=7;
			e.Graphics.DrawString("ͳ���ˣ�",TextFont,Brushes.Black,curRowX,curRowY);
			tilWith= e.Graphics.MeasureString("ͳ���ˣ� ",TextFont);
			curRowX+=tilWith.Width;
			e.Graphics.DrawString(this.m_objViewer.LoginInfo.m_strEmpName,TextFont,Brushes.Black,curRowX,curRowY);
			curRowX+=200;
			e.Graphics.DrawString("����ˣ�",TextFont,Brushes.Black,curRowX,curRowY);
			curRowX+=200;
			e.Graphics.DrawString("���ɣ�",TextFont,Brushes.Black,curRowX,curRowY);
			curRowY+=RowHight-7;
			e.Graphics.DrawLine(penLine,LeftWith,curRowY,PageWidth-LeftWith,curRowY);

			e.Graphics.DrawLine(penLine,LeftWith,SaveStartHight,LeftWith,curRowY);
			e.Graphics.DrawLine(penLine,PageWidth-LeftWith,SaveStartHight,PageWidth-LeftWith,curRowY);
			#endregion

		}

	}
}
