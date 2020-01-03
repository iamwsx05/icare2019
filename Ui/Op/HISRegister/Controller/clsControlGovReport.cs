using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Drawing.Printing;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlGovReport ��ժҪ˵����
	/// </summary>
	public class clsControlGovReport: com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlGovReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmGovReport m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmGovReport)frmMDI_Child_Base_in;
		}
		#endregion

		#region ����
		clsDomainControl_Register Domain=new clsDomainControl_Register();
		/// <summary>
		/// ͳ�Ʊ�
		/// </summary>
		private DataTable dtStat=new DataTable();
		/// <summary>
		/// ����ÿһ�������������ķ�Ʊ����
		/// </summary>
		private DataTable dtCheck=new DataTable();
//		/// <summary>
//		/// �������ID���Ը�����
//		/// </summary>
//		private string[,] arrPatType=new string[,] {{"0014","10"},{"0015","20"},{"0016","0"},{"0017","10"},{"0018","20"},{"0019","0"},{"0020","10"},{"0021","20"},{"0022","0"},{"0023","10"},{"0024","20"},{"0025","0"},{"0026","10"},{"0027","20"},{"0028","0"},{"0029","10"},{"0030","20"},{"0031","0"},{"0032","10"},{"0033","20"},{"0034","0"},{"0035","10"},{"0036","20"},{"0037","0"},{"0038","10"},{"0039","20"},{"0040","0"},{"0041","10"},{"0042","20"},{"0043","0"},{"0044","10"},{"0045","20"},{"0046","0"}};
		/// <summary>
		/// ���淢Ʊ�������Ĺ�ҽ��
		/// </summary>
		private string[,] arrType=new string[,] {{"30","��������ҽ��"},{"31","��������ҽ��"},{"32","��������ҽ��"},{"A0","��������ҽ��"},{"A1","��������ҽ��"},{"A2","��������ҽ��"},{"40","��������ҽ��"},{"41","��������ҽ��"},{"42","��������ҽ��"},{"B0","��������ҽ��"},{"B1","��������ҽ��"},{"B2","��������ҽ��"},{"50","��������ҽ��"},{"51","��������ҽ��"},{"52","��������ҽ��"},{"00","�й�ҽ��"},{"01","�й�ҽ��"},{"02","�й�ҽ��"},{"10","��ɽ����ҽ��"},{"11","��ɽ����ҽ��"},{"12","��ɽ����ҽ��"},{"20","Խ������ҽ��"},{"21","Խ������ҽ��"},{"22","Խ������ҽ��"},{"83","ʡ��ҽ��"},{"90","ʡ��ҽ��"}};
		/// <summary>
		/// ���汨���ֶ�ά�����е��ֶ���Ϣ
		/// </summary>
		private string[,] arrAllMoney=new string[,] {{"0001","��ҩ"},{"0002","�г�ҩ"},{"0003","��ҩ"},{"0004","���"},{"0005","����"},{"0006","���"}};
		/// <summary>
		/// ���汨���ֶ�ά�����еı���ID
		/// </summary>
		private string strReportID="0010";
		#endregion

		#region ͳ������
		public void m_getGovData(string StarDate,string EndDate)
		{
			DataTable dtGovData=new DataTable();
			Domain.m_lngGetPublicMoney(StarDate,EndDate,out dtGovData);
			dtStat.Clear();
			try
			{
				dtStat.Columns.Add("���Ѻ�");
				dtStat.Columns.Add("��ҩ");
				dtStat.Columns.Add("�г�ҩ");
				dtStat.Columns.Add("��ҩ");
				dtStat.Columns.Add("���");
				dtStat.Columns.Add("����");
				dtStat.Columns.Add("���");
				dtStat.Columns.Add("�Ը�����");
			}
			catch
			{
			}
			#region ͳ������
			if(dtGovData.Rows.Count>0)
			{
				Domain.m_lngGetGopRla(out dtCheck);
				string filter="";
				DataRow[] objRow;
//				string strPercent="";//��ǰ��Ʊ���Ը�����
				for(int i1=0;i1<dtGovData.Rows.Count;i1++)
				{
//					#region ��ȡ��ǰ��Ʊ���Ը�����
//					for(int L4=0;L4<arrPatType.Length/2;L4++)
//					{
//						if(arrPatType[L4,0]==dtGovData.Rows[i1]["PAYTYPEID_CHR"].ToString().Trim())
//						{
//							strPercent=arrPatType[L4,1];
//							break;
//						}
//					}
//					#endregion
					string strType=dtGovData.Rows[i1]["GOVCARD_CHR"].ToString().Substring(0,2).ToUpper();
					for(int t1=0;t1<arrAllMoney.Length/2;t1++)
					{
						filter="RPTID_CHR ='"+strReportID+"' and GROUPID_CHR ='"+arrAllMoney[t1,0]+"'";
						objRow=dtCheck.Select(filter);
						if(objRow.Length>0)
						{
							for(int f2=0;f2<objRow.Length;f2++)
							{
								if(dtGovData.Rows[i1]["ITEMCATID_CHR"].ToString().Trim()==objRow[f2]["TYPEID_CHR"].ToString().Trim())
								{
									if(dtStat.Rows.Count>0)
									{
										//�ж�ͳ�Ʊ����Ƿ񼺾�������ͬ���͵�����
										for(int k3=0;k3<dtStat.Rows.Count;k3++)
										{
											if(dtStat.Rows[k3]["���Ѻ�"].ToString()==strType&&dtStat.Rows[k3]["�Ը�����"].ToString()==dtGovData.Rows[i1]["CHARGEPERCENT_DEC"].ToString().Trim())
											{
												//��������޸�ԭ��������
												dtStat.Rows[k3][arrAllMoney[t1,1]]=Convert.ToDouble(dtStat.Rows[k3][arrAllMoney[t1,1]])+Convert.ToDouble(dtGovData.Rows[i1]["TOLFEE_MNY"]);
												break;
											}
											//��������ڲ���һ������
											if(k3==dtStat.Rows.Count-1)
											{
												DataRow newRow=dtStat.NewRow();
												newRow["��ҩ"]=0;
												newRow["�г�ҩ"]=0;
												newRow["��ҩ"]=0;
												newRow["���"]=0;
												newRow["����"]=0;
												newRow["���"]=0;
												newRow["���Ѻ�"]=strType;
												newRow["�Ը�����"]=dtGovData.Rows[i1]["CHARGEPERCENT_DEC"].ToString().Trim();
												newRow[arrAllMoney[t1,1]]=dtGovData.Rows[i1]["TOLFEE_MNY"];
												dtStat.Rows.Add(newRow);
												break;
											}
										}
									}
										//���ͳ�Ʊ���û���κε����ݲ���һ������
									else
									{
										DataRow newRow=dtStat.NewRow();
										newRow["��ҩ"]=0;
										newRow["�г�ҩ"]=0;
										newRow["��ҩ"]=0;
										newRow["���"]=0;
										newRow["����"]=0;
										newRow["���"]=0;
										newRow["���Ѻ�"]=strType;
										newRow["�Ը�����"]=dtGovData.Rows[i1]["CHARGEPERCENT_DEC"].ToString().Trim();
										newRow[arrAllMoney[t1,1]]=dtGovData.Rows[i1]["TOLFEE_MNY"];
										dtStat.Rows.Add(newRow);
									}

								}
							}
						}
					}
				}
			}

			#endregion
		}
		#endregion


		#region ��ӡ����
		public void m_printRePort(System.Drawing.Printing.PrintPageEventArgs e)
		{
			float X=30.0F;//�������ʼ������
			float Y=30.0F;//��������ʼ������
			System.Drawing.Font tilFont=new Font("����",16,System.Drawing.FontStyle.Bold);//���������
			System.Drawing.Font  textFont=new Font("����",10);//�ı�������
			Pen penLine=new Pen(Brushes.Black,1);//���廭��
			float pageWith=e.PageBounds.Width;//��ȡҳ��Ŀ��
			float RowHigth=23;
			float RowWith=62;
			#region ��ͷ
			SizeF tilwith=e.Graphics.MeasureString("����ͳ�Ʊ���",tilFont);
			e.Graphics.DrawString("����ͳ�Ʊ���",tilFont,Brushes.Black,pageWith/2-tilwith.Width/2,Y);
			Y+=RowHigth+5;
			e.Graphics.DrawLine(penLine,X,Y,pageWith-X,Y);
			X+=RowWith-20;
			e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);
			e.Graphics.DrawString("��ҩ",textFont,Brushes.Black,X+3,Y+7);
			X+=RowWith;
			e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);

			e.Graphics.DrawString("�г�ҩ",textFont,Brushes.Black,X+3,Y+7);
			X+=RowWith;
			e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);
			e.Graphics.DrawString(" ",textFont,Brushes.Black,X,Y+7);



			e.Graphics.DrawString("�ϼ�",textFont,Brushes.Black,X+3,Y+7);
			X+=RowWith;
			e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);


			e.Graphics.DrawString("��ҩ",textFont,Brushes.Black,X+3,Y+7);
			X+=RowWith;
			e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);


			e.Graphics.DrawString("���",textFont,Brushes.Black,X+3,Y+7);
			X+=RowWith;
			e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);


			e.Graphics.DrawString("����",textFont,Brushes.Black,X+3,Y+7);
			X+=RowWith;
			e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);


			e.Graphics.DrawString("���",textFont,Brushes.Black,X+3,Y+7);
			X+=RowWith;
			e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);
			

			e.Graphics.DrawString("�ϼ�",textFont,Brushes.Black,X+3,Y+7);
			X+=RowWith;
			e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);


			e.Graphics.DrawString("�Ը�����",textFont,Brushes.Black,X+3,Y+7);
			X+=RowWith;
			e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);

			e.Graphics.DrawString("ʵ��",textFont,Brushes.Black,X+3,Y+7);
			X+=RowWith+20;
			e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);


			e.Graphics.DrawString("�������",textFont,Brushes.Black,X+3,Y+7);
			X=30.0F;
			Y+=RowHigth;
			e.Graphics.DrawLine(penLine,X,Y,pageWith-X,Y);
			#endregion
			DataView myDataView= dtStat.DefaultView;
			myDataView.Sort="���Ѻ� ASC";
			#region �����
			if(myDataView.Count>0)
			{
				for(int i1=0;i1<myDataView.Count;i1++)
				{
//					if(i1+1==myDataView.Count)
//					{
						e.Graphics.DrawString(myDataView.Table.Rows[i1]["���Ѻ�"].ToString(),textFont,Brushes.Black,X+3,Y+7);
						X+=RowWith-20;
						e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);
						e.Graphics.DrawString(myDataView.Table.Rows[i1]["��ҩ"].ToString(),textFont,Brushes.Black,X+3,Y+7);
						X+=RowWith;
						e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);

						e.Graphics.DrawString(myDataView.Table.Rows[i1]["�г�ҩ"].ToString(),textFont,Brushes.Black,X+3,Y+7);
						X+=RowWith;
						e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);
						//��ҩ�Ѻϼ�
						double tolMoneyCh=Convert.ToDouble(myDataView.Table.Rows[i1]["�г�ҩ"].ToString())+Convert.ToDouble(myDataView.Table.Rows[i1]["��ҩ"].ToString());
						e.Graphics.DrawString(tolMoneyCh.ToString(),textFont,Brushes.Black,X+3,Y+7);
						X+=RowWith;
						e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);

						e.Graphics.DrawString(myDataView.Table.Rows[i1]["��ҩ"].ToString(),textFont,Brushes.Black,X+3,Y+7);
						X+=RowWith;
						e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);

						e.Graphics.DrawString(myDataView.Table.Rows[i1]["���"].ToString(),textFont,Brushes.Black,X+3,Y+7);
						X+=RowWith;
						e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);

						e.Graphics.DrawString(myDataView.Table.Rows[i1]["����"].ToString(),textFont,Brushes.Black,X+3,Y+7);
						X+=RowWith;
						e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);

						e.Graphics.DrawString(myDataView.Table.Rows[i1]["���"].ToString(),textFont,Brushes.Black,X+3,Y+7);
						X+=RowWith;
						e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);
						//ȫ�����ϼ�
						double tolAllMoney=tolMoneyCh+Convert.ToDouble(myDataView.Table.Rows[i1]["��ҩ"].ToString())+Convert.ToDouble(myDataView.Table.Rows[i1]["���"].ToString())+Convert.ToDouble(myDataView.Table.Rows[i1]["����"].ToString())+Convert.ToDouble(myDataView.Table.Rows[i1]["���"].ToString());
						e.Graphics.DrawString(tolAllMoney.ToString(),textFont,Brushes.Black,X+3,Y+7);
						X+=RowWith;
						e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);

						e.Graphics.DrawString(myDataView.Table.Rows[i1]["�Ը�����"].ToString()+"%",textFont,Brushes.Black,X+3,Y+7);
						X+=RowWith;
						e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);
						//ʵ�����

						double isTrueMoney=tolAllMoney*(1-Convert.ToDouble(myDataView.Table.Rows[i1]["�Ը�����"].ToString())/100);
						e.Graphics.DrawString(isTrueMoney.ToString(),textFont,Brushes.Black,X+3,Y+7);
						X+=RowWith+20;
						e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth);
						string strType="";
						for(int f2=0;f2<arrType.Length/2;f2++)
						{
							if(arrType[f2,0]==myDataView.Table.Rows[i1]["���Ѻ�"].ToString())
							{
								strType=arrType[f2,1];
								break;
							}

						}
					e.Graphics.DrawString(strType,textFont,Brushes.Black,X+3,Y+7);
					 X=30.0F;
					e.Graphics.DrawLine(penLine,X,Y+RowHigth,pageWith-X,Y+RowHigth);
					Y+=RowHigth;
						//										X=30.0F;

//					}
//					else
//					{
//
//					}


				}
				X=30.0F;
				Y=30.0F;
				Y+=RowHigth+5;
				e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth*(myDataView.Count+1));
				e.Graphics.DrawLine(penLine,pageWith-X,Y,pageWith-X,Y+RowHigth*(myDataView.Count+1));
				
			}
			else
			{
				X=30.0F;
				Y=30.0F;
				Y+=RowHigth+5;
				e.Graphics.DrawLine(penLine,X,Y,X,Y+RowHigth*2);
				e.Graphics.DrawLine(penLine,pageWith-X,Y,pageWith-X,Y+RowHigth*2);
				e.Graphics.DrawLine(penLine,X,Y+RowHigth*2,pageWith-X,Y+RowHigth*2);
			}


			#endregion


		}
		#endregion


	}
}
