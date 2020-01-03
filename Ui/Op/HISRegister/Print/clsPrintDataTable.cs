using System;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsPrintDataTable ��ժҪ˵����
	/// </summary>
	public class clsPrintDataTable
	{
		#region ȫ�ֱ���
		/// <summary>
		/// ��������
		/// </summary>
		public string strReportName ="";
		/// <summary>
		/// ҽԺ����
		/// </summary>
		public string strHospitalName ="";
		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		public string strBeginTime ="";
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string strEndTime="";
		/// <summary>
		/// ��ӡ��
		/// </summary>
		public string strPrinter="";
		/// <summary>
		/// ��ǰҳ��
		/// </summary>
		private int intPageLocation=1;
		/// <summary>
		/// ��������1
		/// </summary>
		Font objFontTitle1;
		/// <summary>
		/// ��������2
		/// </summary>
		Font objFontTitle2;
		/// <summary>
		/// ��������
		/// </summary>
		Font objFontNormal;
		/// <summary>
		/// ��߾�
		/// </summary>
		float fltLeftIndentProp=0.07f;
		/// <summary>
		/// �ұ߾�
		/// </summary>
		float fltRightIndentProp=0.07f;
		/// <summary>
		/// �м��
		/// </summary>
		private  float  fltRowHeight=0;
		/// <summary>
		///�п�
		/// </summary>
		private  float  fltRowWidth=0.055f;
		/// <summary>
		/// ������
		/// </summary>
		private float  Y;
		/// <summary>
		/// ������
		/// </summary>
		private float  X;
		/// <summary>
		/// ��¼����Y����
		/// </summary>
		private float Y2=0;
		/// <summary>
		/// ��ǰ���е����к�
		/// </summary>
		private int row=0;
		/// <summary>
		/// ��ǰ��ӡ����
		/// </summary>
		private int Col =0;
		/// <summary>
		/// ���ݱ�
		/// </summary>
		private DataTable dt;
		/// <summary>
		/// ����Դ
		/// </summary>
		public DataTable SetDataSource
		{
			set
			{
			dt =value;
			}
		}
		
		#endregion
		public clsPrintDataTable()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			objFontTitle1=new Font("SimSun",12,FontStyle.Bold);
			objFontTitle2=new Font("SimSun",16,FontStyle.Bold);
			objFontNormal=new Font("SimSun",10);
		}
		#region ��ȡ�ܵ�ҳ����ÿҳ������	
		/// <summary>
		/// ÿҳ������
		/// </summary>
		/// <param name="e"></param>
		private int intRowCount =0;
		/// <summary>
		/// �ܵ�ҳ��
		/// </summary>
		/// <param name="e"></param>
		private int intPageCount =0;
		/// <summary>
		/// ÿҳ���Դ�ӡ������
		/// </summary>
		private int intPageTemp =0;
		private void m_mthGetPageCount(System.Drawing.Printing.PrintPageEventArgs e)
		{
			if(intRowCount!=0)
			{
				return;
			}
			//ÿ���ܴ�ӡ������fltLeftIndentProp*2��Ϊ�˵�һ�мӿ�
			double d1 =(1-this.fltLeftIndentProp-this.fltRightIndentProp)/this.fltRowWidth;
			double intPageTemp1=Math.Floor(d1);
			intPageTemp =(int)intPageTemp1;
//			 intPageTemp=(int)Math.Floor((1-this.fltLeftIndentProp-this.fltRightIndentProp)/this.fltRowWidth);
			//��ȡ�ܵ�����Ҫ����ҳ��
			double d2 =this.dt.Columns.Count/intPageTemp1;
			double intPageCountTemp =Math.Ceiling(d2);
			//�ܵĸ߶�
			float fltPageHeight =e.PageBounds.Height;
			//��ȡÿҳ�ܴ������.
			double d3 =(fltPageHeight -Y-this.fltRowHeight)/this.fltRowHeight;
			double intRowCount1 =Math.Floor(d3);
			intRowCount=(int)intRowCount1;
			//��ȡ�ܵ�����Ҫ���ҳ��
			double d4 =this.dt.Rows.Count/intRowCount1;
			double intRowCountTemp =Math.Ceiling(d4);
			for(int i=1;i<intRowCountTemp;i++)
			{
			Object[] oTemp =dt.Rows[0].ItemArray;
			DataRow drTemp =dt.NewRow();
			drTemp.ItemArray =oTemp;
			this.dt.Rows.InsertAt(drTemp,i*intRowCount);
			}
			double d5 =this.dt.Rows.Count/intRowCount1;
			 intRowCountTemp =Math.Ceiling(d5);
//			intRowCountTemp =Math.Ceiling(this.dt.Rows.Count/intRowCount);
			//�ܵ�ҳ�������ܵ��з�ҳ���ܵ��з�ҳ��
			intPageCount =(int)(intRowCountTemp*intPageCountTemp);
		

		}
		#endregion
		#region ��ӡ

		public void m_mthPrintMultWorkLoad(System.Drawing.Printing.PrintPageEventArgs e)
		{
			if(dt==null)
			{
			return;
			}
			Pen objPen =new Pen(Color.Black,1);
			this.m_mthPrintTitle(e);
//			this.m_mthGetPageCount(e);//��ȡҳ��
			this.m_mthPrintColumn(e);
//			//			    X+=e.PageBounds.Width*this.fltRowWidth;
////			if(this.m_objViewer.radioButton4.Checked)
////			{
//				X+=30;
////			}
//			Y=Y2;
//			if(this.intPageLocation==0)
//			{
//				X+=e.PageBounds.Width*this.fltRowWidth;
//				this.m_mthPrintColumn(1,e);
//			}
//			for(int i=row;i<dt.Columns.Count;i++)
//			{
//				if(X>e.PageBounds.Width*(1-this.fltRowWidth-this.fltRightIndentProp))
//				{
//					e.HasMorePages =true;
//					intPageLocation+=1;
//					float temp =X+e.PageBounds.Width*this.fltRowWidth;
//					e.Graphics.DrawLine(objPen,temp,Y2,temp,Y2+fltRowHeight*dt.Rows.Count);
//					break;
//				}
//				X+=e.PageBounds.Width*this.fltRowWidth;//�����긴λ
//				Y=Y2;
//				this.m_mthPrintColumn(i,e);
//				row =i;//��¼��ǰ������һ��
//			}
//			X+=e.PageBounds.Width*this.fltRowWidth;//��λ�����һ������
//			e.Graphics.DrawLine(objPen,X,Y2,X,Y2+fltRowHeight*dt.Rows.Count);//�����һ����
//			objPen.Dispose();
//			objPen=null;
		}
		#endregion
		#region ��ӡ����
		private void m_mthPrintTitle(System.Drawing.Printing.PrintPageEventArgs objDraw)
		{
			//����
			//ҽԺ����
			SizeF objFontSize =objDraw.Graphics.MeasureString(this.strHospitalName+this.strReportName,this.objFontTitle1);
			X=(objDraw.PageBounds.Width-objFontSize.Width)/2;
			Y=objDraw.PageBounds.Height*0.047f-(objFontSize.Height/2);
			objDraw.Graphics.DrawString(this.strHospitalName+this.strReportName,objFontTitle1,Brushes.Black,X,Y);
			Y+=objFontSize.Height+15;
			//����
			X=objDraw.PageBounds.Width*fltLeftIndentProp;
			objFontSize =objDraw.Graphics.MeasureString("ͳ��ʱ��:"+this.strBeginTime+" ~ "+this.strEndTime,this.objFontNormal);
			fltRowHeight =objFontSize.Height+2;	//��ȡ�и�
			objDraw.Graphics.DrawString("ͳ��ʱ��:"+this.strBeginTime+" ~ "+this.strEndTime,objFontNormal,Brushes.Black,X,Y);

			X+=objFontSize.Width+20;
			//��ӡʱ��
			objFontSize =objDraw.Graphics.MeasureString("��ӡʱ��:"+DateTime.Now.ToString("yyyy-MM-dd hh:mm"),this.objFontNormal);
			objDraw.Graphics.DrawString("��ӡʱ��:"+DateTime.Now.ToString("yyyy-MM-dd hh:mm"),objFontNormal,Brushes.Black,X,Y);
			//ҳ��
			objFontSize =objDraw.Graphics.MeasureString("��1 ҳ�� 10 ҳ",this.objFontNormal);
			X =objDraw.PageBounds.Width*(1-this.fltRightIndentProp)-objFontSize.Width;
			this.m_mthGetPageCount(objDraw);//��ȡҳ��
			objDraw.Graphics.DrawString("��"+this.intPageLocation.ToString()+" ҳ�� "+intPageCount.ToString()+" ҳ",objFontNormal,Brushes.Black,X,Y);
			Y+=objFontSize.Height+2;
			Y2=Y;
			fltRowHeight =objFontSize.Height+2;	

		}
		#endregion
		#region ��ӡָ������
		private void m_mthPrintColumn(System.Drawing.Printing.PrintPageEventArgs objDraw)
		{
			X=objDraw.PageBounds.Width*fltLeftIndentProp;
			Pen objPen =new Pen(Color.Black,1);
//			float RX=X+objDraw.PageBounds.Width*this.fltRowWidth;
//			if(columnNo==0)
//			{
//				RX +=30;
//			}
			//Ҫѭ���Ĵ���
			int iniLimint=0;
			if(this.row+this.intRowCount>=this.dt.Rows.Count)
			{
				iniLimint  =this.dt.Rows.Count -this.row;
			}
			else
			{
				iniLimint =this.intRowCount;
			}
			int intColmnsLimint =0;
			//��ѭ����
			if(this.Col+this.intPageTemp>=this.dt.Columns.Count)
			{
				intColmnsLimint  =this.dt.Columns.Count -this.Col;
			}
			else
			{
				intColmnsLimint =this.intPageTemp;
			}
		    float RX=X+this.fltRowWidth*intColmnsLimint*objDraw.PageBounds.Width;
			if(Col==0)
			{
				RX+=30;
			}
			X=objDraw.PageBounds.Width*fltLeftIndentProp;
//			objDraw.Graphics.DrawLine(objPen,X,Y,X,Y+fltRowHeight*iniLimint);//��������
			//��¼
			int intColTemp =Col;
			for(int i =0;i<iniLimint;i++)
			{
				X=objDraw.PageBounds.Width*fltLeftIndentProp;
				objDraw.Graphics.DrawLine(objPen,X,Y,RX,Y);//������
				Col =intColTemp;
				for(int i2=0;i2<intColmnsLimint;i2++)
				{
					if(i==0)
					{
						objDraw.Graphics.DrawLine(objPen,X,Y,X,Y+fltRowHeight*iniLimint);//��������
					}
					objDraw.Graphics.DrawString(dt.Rows[row][Col].ToString(),objFontNormal,Brushes.Black,X+1,Y+3);
					X+=this.fltRowWidth*objDraw.PageBounds.Width;
					if(Col==0)
					{
						X+=30;
					}
					Col++;
				}
				if(i==0)
				{
					objDraw.Graphics.DrawLine(objPen,X,Y,X,Y+fltRowHeight*iniLimint);//��������
				}
				row++;
				
//				objDraw.Graphics.DrawLine(objPen,X,Y,RX,Y);
//				objDraw.Graphics.DrawString(dt.Rows[i][columnNo].ToString(),objFontNormal,Brushes.Black,X+1,Y+2);
				Y+=fltRowHeight;

			}
			X=objDraw.PageBounds.Width*fltLeftIndentProp;
			objDraw.Graphics.DrawLine(objPen,X,Y,RX,Y);//�����һ������
			objPen.Dispose();
			objPen =null;
			if(Col<this.dt.Columns.Count)
			{
				objDraw.HasMorePages =true;
				row =row2;
				this.intPageLocation++;
				return;
			}
			row2=row;
			if(row<this.dt.Rows.Count)
			{
				objDraw.HasMorePages =true;
				Col =0;
				this.intPageLocation++;
				return;
			}
		}
		private int row2 =0;
		#endregion
	
	}
}
