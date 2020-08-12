using System;
using System.Drawing;
using System.Data;
using weCare.Core.Entity;
using System.Collections;
using System.Windows.Forms;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsPrintClass 的摘要说明。
	/// </summary>
	public class clsPrintClass  :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		public clsPrintClass()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 变量
		/// <summary>
		/// 标题字体
		/// </summary>
		Font objFontTitle=new Font("楷体_GB2312",14,System.Drawing.FontStyle.Bold);
		/// <summary>
		/// 正常字体
		/// </summary>
		Font objFontNormal=new Font("SimSun",10);
		/// <summary>
		/// 加粗字体
		/// </summary>
		Font objFontBold=new Font("SimSun",11,System.Drawing.FontStyle.Bold);
		/// <summary>
		/// 特细字体
		/// </summary>
		Font objFont=new Font("SimSun",9);
		/// <summary>
		/// 左边距
		/// </summary>
		const float fltLeftIndentProp=20f;
		/// <summary>
		/// 右边距
		/// </summary>
		const float fltRightIndentProp=20f;
		/// <summary>
		/// 上下边距
		/// </summary>
		const float fltStarHight=20f;
		/// <summary>
		/// 行间隔
		/// </summary>
		const   float  fltRowHeight=25F;
		/// <summary>
		/// 纵坐标
		/// </summary>
		private float  Y=fltStarHight;
		/// <summary>
		/// 横坐标
		/// </summary>
		private float  X=fltLeftIndentProp;
		/// <summary>
		/// 打印的宽度
		/// </summary>
		private float  PageWith;
		/// <summary>
		/// 打印的长度
		/// </summary>
		private float  PageHigh;
		/// <summary>
		/// 画笔
		/// </summary>
		Pen PenLine=new Pen(Brushes.Black,1);
		/// <summary>
		/// 保存特殊位置的X轴
		/// </summary>
		float temX=0;
		/// <summary>
		/// 保存特殊位置的Y轴
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
			e.Graphics.DrawString("第"+currPage.ToString()+"页",objFontNormal,Brushes.Black,(PageWith-fltLeftIndentProp-fltRightIndentProp-e.Graphics.MeasureString("第"+currPage.ToString()+"页",objFontNormal).Width)/2,PageHigh-30);
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
			e.Graphics.DrawString("申请单号:",objFontNormal,Brushes.Black,X,Y);
			X+=e.Graphics.MeasureString("申请单号:",objFontNormal).Width;
			e.Graphics.DrawString(MedStoreAppl.m_strMEDAPPLNO_CHR,objFontNormal,Brushes.Black,X,Y);
			X=PageWith-200;
			e.Graphics.DrawString("申请日期:",objFontNormal,Brushes.Black,X,Y);
			X+=e.Graphics.MeasureString("申请日期:",objFontNormal).Width;
			e.Graphics.DrawString(MedStoreAppl.m_strAPPLDATE_DAT,objFontNormal,Brushes.Black,X,Y);
			Y+=fltRowHeight;
			X=fltLeftIndentProp;
			temY=Y;
			e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
			X+=5;
			e.Graphics.DrawString("备注",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("备注备注备",objFontNormal).Width;
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
					//药品编码
					e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
					X+=5;
					e.Graphics.DrawString("药品编码",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("备注备注备",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//药品名称
					X+=5;
					e.Graphics.DrawString("药品名称",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("药品名称备",objFontNormal).Width*2;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//规格
					X+=5;
					e.Graphics.DrawString("规格",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("规格",objFontNormal).Width*5;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//生产厂家
					X+=5;
					e.Graphics.DrawString("生产厂家",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("规格",objFontNormal).Width*5;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);;

					//数量
					X+=5;
					e.Graphics.DrawString("数量",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("规格格",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

					//单位
					X+=5;
					e.Graphics.DrawString("单位",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("单位",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

					//单价
					X+=5;
					e.Graphics.DrawString("单价",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("单价.",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

					//总金额
					X+=5;
					e.Graphics.DrawString("总金额",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("总金额额",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,PageWith-fltRightIndentProp,Y-hight,PageWith-fltRightIndentProp,Y+fltRowHeight);
					for(int f2=0;f2<dt.Rows.Count;f2++)
					{
						X=fltLeftIndentProp;
						Y+=fltRowHeight;
						e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						//药品编码
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["ASSISTCODE_CHR"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("备注备注备",objFontNormal).Width;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						//药品名称
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
						X+=e.Graphics.MeasureString("药品名称备",objFontNormal).Width*2;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						//规格
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["MEDSPEC_VCHR"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("规格",objFontNormal).Width*5;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						//生产厂家
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
						
						X+=e.Graphics.MeasureString("规格",objFontNormal).Width*5;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

						//数量
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["QTY_DEC"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("规格格",objFontNormal).Width;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						temX=X;
						//单位
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["UNITID_CHR"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("单位",objFontNormal).Width;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

						//单价
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["UNITPRICE_MNY"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("单价.",objFontNormal).Width;
						e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
						

						//总金额
						X+=5;
						e.Graphics.DrawString(dt.Rows[f2]["TOLMNY_MNY"].ToString(),objFontNormal,Brushes.Black,X,Y+5);
						X+=e.Graphics.MeasureString("总金额额",objFontNormal).Width;
						fltmomey+=double.Parse(dt.Rows[f2]["TOLMNY_MNY"].ToString());
						e.Graphics.DrawLine(PenLine,PageWith-fltRightIndentProp,Y,PageWith-fltRightIndentProp,Y+fltRowHeight);
					}
					X=fltLeftIndentProp;
					Y+=fltRowHeight;
					e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//供应商
					X+=5;
					e.Graphics.DrawString("供应商",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("备注备注备",objFontNormal).Width;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//供应商内容
					X+=5;
					if(dt.TableName=="Table1")
						e.Graphics.DrawString("",objFontNormal,Brushes.Black,X,Y+5);
					else
						e.Graphics.DrawString(dt.TableName,objFontNormal,Brushes.Black,X,Y+5);
					X=temX;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
					//小计金额
					X+=5;
					e.Graphics.DrawString("小计金额",objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("药品名称备",objFontNormal).Width+3;
					e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

					//小计金额内容
					X+=5;
					e.Graphics.DrawString(fltmomey.ToString(),objFontNormal,Brushes.Black,X,Y+5);
					X+=e.Graphics.MeasureString("药品名称备",objFontNormal).Width;
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
			//总金额
			X+=5;
			e.Graphics.DrawString("总 金 额",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("药品名称备",objFontNormal).Width+3;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

			//总金额
			X+=5;
			e.Graphics.DrawString(MedStoreAppl.m_strTOTMONEY_CHR,objFontNormal,Brushes.Black,X,Y+5);

			X=fltLeftIndentProp;
			e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
			X=fltLeftIndentProp;
			Y+=fltRowHeight;
			e.Graphics.DrawLine(PenLine,X,Y,PageWith-fltRightIndentProp,Y);
			X+=5;
			
			//制单人
			e.Graphics.DrawString("制单人",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("备注备注备",objFontNormal).Width;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
			//制单人
			X+=5;
			e.Graphics.DrawString(MedStoreAppl.m_strCREATORNAME_CHR,objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("药品名称备",objFontNormal).Width*2;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
			//复核人
			X+=5;
			e.Graphics.DrawString("复核人",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("规格",objFontNormal).Width*2;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);
			//复核人
			X+=5;
			e.Graphics.DrawString(" ",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("规格",objFontNormal).Width*3;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);;

			//数量
			X+=5;
			e.Graphics.DrawString("审核人",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("规格格",objFontNormal).Width;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

			//审核人
			X+=5;
			e.Graphics.DrawString("",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("单位..",objFontNormal).Width*2;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

			//审核时间
			X+=5;
			e.Graphics.DrawString("审核时间",objFontNormal,Brushes.Black,X,Y+5);
			X+=e.Graphics.MeasureString("审核时间",objFontNormal).Width;
			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

			//总金额
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
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 变量
		/// <summary>
		/// 标题字体
		/// </summary>
		Font objFontTitle=new Font("楷体_GB2312",14,System.Drawing.FontStyle.Bold);
		/// <summary>
		/// 正常字体
		/// </summary>
		Font objFontNormal=new Font("SimSun",10);
		/// <summary>
		/// 特细字体
		/// </summary>
		Font objFont=new Font("SimSun",9);
		/// <summary>
		/// 加粗字体
		/// </summary>
		Font objFontBold=new Font("SimSun",11,System.Drawing.FontStyle.Bold);
		/// <summary>
		/// 左边距
		/// </summary>
		const float fltLeftIndentProp=10f;
		/// <summary>
		/// 右边距
		/// </summary>
	    float fltRightIndentProp=40f;
		/// <summary>
		/// 上下边距
		/// </summary>
		public const float fltStarHight=0f;
		/// <summary>
		/// 行间隔
		/// </summary>
		const   float  fltRowHeight=25F;
		/// <summary>
		/// 标题行间隔
		/// </summary>
		const   float  fltRowHeightTit=43F;
		/// <summary>
		/// 纵坐标
		/// </summary>
		public float  Y=fltStarHight;
		/// <summary>
		/// 横坐标
		/// </summary>
		private float  X=fltLeftIndentProp;
		/// <summary>
		/// 打印的宽度
		/// </summary>
		private float  PageWith;
		/// <summary>
		/// 打印的长度
		/// </summary>
		private float  PageHigh;
		/// <summary>
		/// 画笔
		/// </summary>
		Pen PenLine=new Pen(Brushes.Black,1);
		/// <summary>
		/// 保存特殊位置的Y轴
		/// </summary>
		float temY=0;
		float fltrithgwith=2;
		/// <summary>
		/// 当前打印的表
		/// </summary>
		public int currdt=0;
		/// <summary>
		/// 当前打印的行
		/// </summary>
		public int currRow=0;
		/// <summary>
		/// 记录共有几多页
		/// </summary>
		public int pagenuber=0;
		/// <summary>
		/// 标题
		/// </summary>
		string titleName;
        /// <summary>
        /// 标识是否打印
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
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药中标退库明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药非中标退库明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药退库明细报表";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药中标入库明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药非中标入库明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药入库明细报表";
                            }
                        }
                        break;
                    case "2":
                        if (strIn == "1")
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药中标退库明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药非中标退库明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药退库明细报表";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药中标入库明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药非中标入库明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药入库明细报表";
                            }
                        }
                        break;
                    case "3":
                        if (strIn == "1")
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药中标退库明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药非中标退库明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药退库明细报表";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药中标入库明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药非中标入库明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药入库明细报表";
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
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药中标出库明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药非中标出库明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药出库明细报表";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药中标退货明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药非中标退货明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "西药退货明细报表";
                            }
                        }

                        break;
                    case "2":
                        if (strIn == "1")
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药中标出库明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药非中标出库明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药出库明细报表";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药中标退货明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药非中标退货明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中草药退货明细报表";
                            }
                        }
                        break;
                    case "3":
                        if (strIn == "1")
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药中标出库明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药非中标出库明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药出库明细报表";
                            }
                        }
                        else
                        {
                            if (strSTANDARD == "1")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药中标退货明细报表";
                            }
                            else if (strSTANDARD == "0")
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药非中标退货明细报表";
                            }
                            else
                            {
                                titleName = this.m_objComInfo.m_strGetHospitalTitle() + "中成药退货明细报表";
                            }
                        }
                        break;
                }
                #endregion
            }
			if(dtsetAll.Tables.Count>0)
			{
                int showPage = pagenuber + 1;
                e.Graphics.DrawString("第" + showPage.ToString() + "页", objFontNormal, Brushes.Black, (PageWith - fltLeftIndentProp - fltRightIndentProp - e.Graphics.MeasureString("第" + showPage.ToString() + "页", objFontNormal).Width) / 2, PageHigh - 50);
				e.Graphics.DrawString("制表:"+strMan,objFontNormal,Brushes.Black,PageWith-fltRightIndentProp-150,PageHigh-50);
				if(pagenuber==0)
				{
					Y+=50;
					e.Graphics.DrawString(titleName,objFontTitle,Brushes.Black,(PageWith-fltLeftIndentProp-fltRightIndentProp-e.Graphics.MeasureString(this.m_objComInfo.m_strGetHospitalTitle()+"西药中标入库明细报表",objFontTitle).Width)/2,Y);
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
                e.Graphics.DrawString("单位:", objFontNormal, Brushes.Black, X, Y);
                X += e.Graphics.MeasureString("单位:", objFontNormal).Width;
                e.Graphics.DrawString(strVendor, objFontNormal, Brushes.Black, X, Y);
                X = PageWith - 250;
                e.Graphics.DrawString("入库日期:", objFontNormal, Brushes.Black, X, Y);
                X += e.Graphics.MeasureString("入库日期:", objFontNormal).Width;
                e.Graphics.DrawString(datestart + "至" + dateEnd, objFontNormal, Brushes.Black, X, Y);
                Y += fltRowHeight;
                X = fltLeftIndentProp;
                temY = Y;
                e.Graphics.DrawLine(PenLine, X, Y, PageWith - fltRightIndentProp, Y);
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);
                X += fltrithgwith;
                float tempHight = 18;
                e.Graphics.DrawString("序 号", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("序 号", objFontNormal).Width;
                e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);

                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);
                X += fltrithgwith;
                e.Graphics.DrawString("药品名称及规格", objFontNormal, Brushes.Black, X + 50, Y + tempHight);
                X += e.Graphics.MeasureString("药品名称及规格", objFontNormal).Width + 150;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("剂型", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("剂型", objFontNormal).Width;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith + 10;
                e.Graphics.DrawString("生产厂家", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("生产厂家", objFontNormal).Width;
                X += 10;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("单", objFontNormal, Brushes.Black, X, Y + 8);
                e.Graphics.DrawString("位", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("位", objFontNormal).Width;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                e.Graphics.DrawString("入库数", objFontNormal, Brushes.Black, X + 180, Y + 5);
                e.Graphics.DrawLine(PenLine, X, Y + 20, X + 448, Y + 20);
                X += fltrithgwith;
                e.Graphics.DrawString("数量", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("数量", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("进价", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("出价", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("进价金额", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("进价金额", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);
                e.Graphics.DrawString("中 标", objFont, Brushes.Black, X, Y + 20);
                e.Graphics.DrawString("零 价", objFont, Brushes.Black, X, Y + 31);
                X += e.Graphics.MeasureString("中 标", objFont).Width;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("中标金额", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("中标金额", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);
                e.Graphics.DrawString("国 家", objFont, Brushes.Black, X, Y + 20);
                e.Graphics.DrawString("限 价", objFont, Brushes.Black, X, Y + 31);
                X += e.Graphics.MeasureString("限 价", objFont).Width;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("限价金额", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("限价金额", objFontNormal).Width;
                X += fltrithgwith;

                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);
                e.Graphics.DrawString("零 售", objFont, Brushes.Black, X, Y + 20);
                e.Graphics.DrawString("单 价", objFont, Brushes.Black, X, Y + 31);
                X += e.Graphics.MeasureString("零 售", objFont).Width;
                e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("零售金额", objFontNormal, Brushes.Black, X, Y + 26);
                X += e.Graphics.MeasureString("零售金额", objFontNormal).Width;
                X += fltrithgwith;

                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("批号", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("批号", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("批准文号", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("批准文号", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);


                X += fltrithgwith;
                e.Graphics.DrawString("有效期", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("有效期", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

                X += fltrithgwith;
                e.Graphics.DrawString("日期", objFontNormal, Brushes.Black, X, Y + tempHight);
                X += e.Graphics.MeasureString("日期", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeightTit);
                Y += fltRowHeightTit;
                e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y, PageWith - fltRightIndentProp, Y);
            //}
            //else
            //{
            //    e.Graphics.DrawString("单位:", objFontNormal, Brushes.Black, X, Y);
            //    X += e.Graphics.MeasureString("单位:", objFontNormal).Width;
            //    e.Graphics.DrawString(strVendor, objFontNormal, Brushes.Black, X, Y);
            //    X = PageWith - 250;

            //    e.Graphics.DrawString("出库日期:", objFontNormal, Brushes.Black, X, Y);
            //    X += e.Graphics.MeasureString("入库日期:", objFontNormal).Width;
            //    e.Graphics.DrawString(datestart + "至" + dateEnd, objFontNormal, Brushes.Black, X, Y);
            //    Y += fltRowHeight;
            //    X = fltLeftIndentProp;
            //    temY = Y;
            //    e.Graphics.DrawLine(PenLine, X, Y, PageWith - fltRightIndentProp, Y);
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);
            //    X += fltrithgwith;
            //    e.Graphics.DrawString("序 号", objFontNormal, Brushes.Black, X, Y + 15);

            //    X += e.Graphics.MeasureString("序 号", objFontNormal).Width;
            //    e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);

            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);
            //    X += fltrithgwith;
            //    e.Graphics.DrawString("药品名称及规格", objFontNormal, Brushes.Black, X + 50, Y + 15);
            //    X += e.Graphics.MeasureString("药品名称及规格", objFontNormal).Width + 158;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("剂型", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("剂型", objFontNormal).Width;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    X += fltrithgwith + 10;
            //    e.Graphics.DrawString("生产厂家", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("生产厂家", objFontNormal).Width;
            //    X += 10;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("单", objFontNormal, Brushes.Black, X, Y + 5);
            //    e.Graphics.DrawString("位", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("位", objFontNormal).Width;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);
            //    e.Graphics.DrawString("出库数", objFontNormal, Brushes.Black, X + 180, Y + 5);
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X + 396, Y + 20);
            //    X += fltrithgwith;
            //    e.Graphics.DrawString("数量", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("数量", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;

            //    e.Graphics.DrawString("卖价", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("出价", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;

            //    e.Graphics.DrawString("出库金额", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("进价金额", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

            //    //			X+=fltrithgwith;
            //    e.Graphics.DrawString("中标零价", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("中标零价", objFontNormal).Width;
            //    //			X+=fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("中标金额", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("中标金额", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);


            //    //			X+=fltrithgwith;
            //    e.Graphics.DrawString("国家限价", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("国家限价", objFontNormal).Width;
            //    //			X+=fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y + 20, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("限价金额", objFontNormal, Brushes.Black, X, Y + 23);
            //    X += e.Graphics.MeasureString("限价金额", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("批号", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("批号", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("批准文号", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("批准文号", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);


            //    X += fltrithgwith;
            //    e.Graphics.DrawString("有效期", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("有效期", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeightTit);

            //    //			X+=fltrithgwith;
            //    //			e.Graphics.DrawString("质量",objFontNormal,Brushes.Black,X,Y+15);
            //    //			X+=e.Graphics.MeasureString("质量",objFontNormal).Width;
            //    //			X+=fltrithgwith;
            //    //			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeightTit);

            //    X += fltrithgwith;
            //    e.Graphics.DrawString("日期", objFontNormal, Brushes.Black, X, Y + 15);
            //    X += e.Graphics.MeasureString("日期", objFontNormal).Width;
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
            //    //序号
            //    if (dtRow["序号"].ToString().Length > 6)
            //    {
            //        e.Graphics.DrawString(dtRow["序号"].ToString().Substring(0, 6), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["序号"].ToString().Substring(6), objFont, Brushes.Black, X, Y + 10);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["序号"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }

            //    X += e.Graphics.MeasureString("序 号", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //药品名称
            //    if (e.Graphics.MeasureString(dtRow["药品名称"].ToString().Trim(), objFontNormal).Width > 190.944)
            //    {
            //        e.Graphics.DrawString(dtRow["药品名称"].ToString().Substring(0, 13), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["药品名称"].ToString().Substring(13), objFont, Brushes.Black, X, Y + 13);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["药品名称"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }

            //    X += 160;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

            //    //药品规格
            //    if (e.Graphics.MeasureString(dtRow["规格"].ToString().Trim(), objFontNormal).Width > 120)
            //    {
            //        e.Graphics.DrawString(dtRow["规格"].ToString().Substring(0, 12), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["规格"].ToString().Substring(12), objFont, Brushes.Black, X, Y + 13);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["规格"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }

            //    X += e.Graphics.MeasureString("药品名称及规格", objFontNormal).Width;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //剂型
            //    e.Graphics.DrawString(dtRow["剂型"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("剂型", objFontNormal).Width;
            //    X += fltrithgwith;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //生产厂家
            //    if (e.Graphics.MeasureString(dtRow["生产厂家"].ToString().Trim(), objFontNormal).Width > 90 && dtRow["生产厂家"].ToString().Length > 7)
            //    {
            //        e.Graphics.DrawString(dtRow["生产厂家"].ToString().Substring(0, 7), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["生产厂家"].ToString().Substring(7), objFont, Brushes.Black, X, Y + 13);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["生产厂家"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }
            //    X += e.Graphics.MeasureString("生产厂家", objFontNormal).Width;
            //    X += fltrithgwith + 20;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //单位
            //    X += fltrithgwith;
            //    e.Graphics.DrawString(dtRow["单位"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("位", objFontNormal).Width;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //数量
            //    e.Graphics.DrawString(dtRow["数量"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("数量", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //进价
            //    e.Graphics.DrawString(dtRow["进价"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("进价", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //进价金额
            //    e.Graphics.DrawString(dtRow["进价金额"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("进价金额", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //中标零价
            //    //if (strSTANDARD == "1")
            //        e.Graphics.DrawString(dtRow["中标零价"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    //else
            //    //    e.Graphics.DrawString("0", objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("中标零价", objFontNormal).Width;
            //    //			X+=fltrithgwith*2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //中标金额
            //    //if (strSTANDARD == "1")
            //        e.Graphics.DrawString(dtRow["中标金额"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    //else
            //    //    e.Graphics.DrawString("0", objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("中标金额", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //国家限价
            //    e.Graphics.DrawString(dtRow["国家限价"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("国家限价", objFontNormal).Width;
            //    //			X+=fltrithgwith*2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //限价金额
            //    e.Graphics.DrawString(dtRow["限价金额"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    X += e.Graphics.MeasureString("限价金额", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //批号
            //    if (dtRow["批号"].ToString().Trim().Length > 5)
            //    {
            //        e.Graphics.DrawString(dtRow["批号"].ToString().Substring(0, 5), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["批号"].ToString().Substring(5), objFont, Brushes.Black, X, Y + 10);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["批号"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }
            //    X += e.Graphics.MeasureString("批号", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    //批准文号
            //    if (e.Graphics.MeasureString(dtRow["批准文号"].ToString().Trim(), objFontNormal).Width > 117.504)
            //    {
            //        e.Graphics.DrawString(dtRow["批准文号"].ToString().Substring(0, 8), objFont, Brushes.Black, X, Y);
            //        e.Graphics.DrawString(dtRow["批准文号"].ToString().Substring(8), objFont, Brushes.Black, X, Y + 10);
            //    }
            //    else
            //    {
            //        e.Graphics.DrawString(dtRow["批准文号"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
            //    }
            //    X += e.Graphics.MeasureString("批准文号", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

            //    //有效
            //    if (dtRow["失效期"] != null && dtRow["失效期"].ToString() != "")
            //    {
            //        e.Graphics.DrawString(DateTime.Parse(dtRow["失效期"].ToString()).ToString("yy/MM/dd"), objFont, Brushes.Black, X, Y + 5);
            //    }
            //    X += e.Graphics.MeasureString("失效期", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

            //    //			//质量
            //    //			e.Graphics.DrawString("合格",objFontNormal,Brushes.Black,X+fltrithgwith,Y+5);
            //    //			X+=e.Graphics.MeasureString("质量",objFontNormal).Width;
            //    //			X+=fltrithgwith*2;
            //    //			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

            //    //日期
            //    if (dtRow["日期"] != null && dtRow["日期"].ToString() != "")
            //    {
            //        e.Graphics.DrawString(DateTime.Parse(dtRow["日期"].ToString()).ToString("yy/MM/dd"), objFont, Brushes.Black, X, Y + 5);
            //    }
            //    X += e.Graphics.MeasureString("日期", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);
            //    e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y + fltRowHeight, PageWith - fltRightIndentProp, Y + fltRowHeight);

            //}
            //else
            //{
                X = fltLeftIndentProp;
                e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y, PageWith - fltRightIndentProp, Y);
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //序号
                if (dtRow["序号"].ToString().Length > 6)
                {
                    e.Graphics.DrawString(dtRow["序号"].ToString().Substring(0, 6), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["序号"].ToString().Substring(6), objFont, Brushes.Black, X, Y + 10);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["序号"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }

                X += e.Graphics.MeasureString("序 号", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //药品名称
                if (e.Graphics.MeasureString(dtRow["药品名称"].ToString().Trim(), objFontNormal).Width > 190.944)
                {
                    e.Graphics.DrawString(dtRow["药品名称"].ToString().Substring(0, 13), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["药品名称"].ToString().Substring(13), objFont, Brushes.Black, X, Y + 13);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["药品名称"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }

                X += 160;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                //药品规格
                if (e.Graphics.MeasureString(dtRow["规格"].ToString().Trim(), objFontNormal).Width > 120)
                {
                    e.Graphics.DrawString(dtRow["规格"].ToString().Substring(0, 12), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["规格"].ToString().Substring(12), objFont, Brushes.Black, X, Y + 13);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["规格"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }
                float tempwith = 8;
                X += e.Graphics.MeasureString("药品名称及规格", objFontNormal).Width - tempwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //剂型
                e.Graphics.DrawString(dtRow["剂型"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("剂型", objFontNormal).Width;
                X += fltrithgwith;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //生产厂家
                if (e.Graphics.MeasureString(dtRow["生产厂家"].ToString().Trim(), objFontNormal).Width > 90 && dtRow["生产厂家"].ToString().Length > 7)
                {
                    e.Graphics.DrawString(dtRow["生产厂家"].ToString().Substring(0, 7), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["生产厂家"].ToString().Substring(7), objFont, Brushes.Black, X, Y + 13);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["生产厂家"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }
                X += e.Graphics.MeasureString("生产厂家", objFontNormal).Width;
                X += fltrithgwith + 20;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //单位
                X += fltrithgwith;
                e.Graphics.DrawString(dtRow["单位"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("位", objFontNormal).Width;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //数量
                float tempwidth = 0;
                //float tempwidth = e.Graphics.MeasureString("数量", objFontNormal).Width - e.Graphics.MeasureString(dtRow["数量"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["数量"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("数量", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //进价
                //tempwidth = e.Graphics.MeasureString("进价", objFontNormal).Width - e.Graphics.MeasureString(dtRow["进价"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["进价"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("进价", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //进价金额
                tempwidth = e.Graphics.MeasureString("进价金额", objFontNormal).Width - e.Graphics.MeasureString(dtRow["进价金额"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["进价金额"].ToString(), objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                X += e.Graphics.MeasureString("进价金额", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //中标零价
                //if (strSTANDARD == "1")
                //{
                //    //tempwidth = e.Graphics.MeasureString("中 标", objFontNormal).Width - e.Graphics.MeasureString(dtRow["中标零价"].ToString(), objFontNormal).Width;
                    e.Graphics.DrawString(dtRow["中标零价"].ToString(), objFontNormal, Brushes.Black, X , Y + 5);
                //}
                //else
                //{
                //    tempwidth = e.Graphics.MeasureString("0", objFontNormal).Width - e.Graphics.MeasureString(dtRow["中标零价"].ToString(), objFontNormal).Width;
                //    e.Graphics.DrawString("0", objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                //}
                X += e.Graphics.MeasureString("中 标", objFont).Width;

                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //中标金额
                //if (strSTANDARD == "1")
                //{
                    tempwidth = e.Graphics.MeasureString("中标金额", objFontNormal).Width - e.Graphics.MeasureString(dtRow["中标金额"].ToString(), objFontNormal).Width;
                    e.Graphics.DrawString(dtRow["中标金额"].ToString(), objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                //}
                //else
                //{
                //    tempwidth = e.Graphics.MeasureString("0", objFontNormal).Width - e.Graphics.MeasureString(dtRow["中标金额"].ToString(), objFontNormal).Width;
                //    e.Graphics.DrawString("0", objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                //}
                X += e.Graphics.MeasureString("中标金额", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //国家限价
                //tempwidth = e.Graphics.MeasureString("限 价", objFontNormal).Width - e.Graphics.MeasureString(dtRow["国家限价"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["国家限价"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("限 价", objFont).Width;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //限价金额
                tempwidth = e.Graphics.MeasureString("限价金额", objFontNormal).Width - e.Graphics.MeasureString(dtRow["限价金额"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["限价金额"].ToString(), objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                X += e.Graphics.MeasureString("限价金额", objFontNormal).Width;
                X += fltrithgwith * 2 + 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                //零售单价
                //tempwidth = e.Graphics.MeasureString("零 售", objFontNormal).Width - e.Graphics.MeasureString(dtRow["零售单价"].ToString(), objFontNormal).Width;
                e.Graphics.DrawString(dtRow["零售单价"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                X += e.Graphics.MeasureString("零 售", objFont).Width;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //零售金额
                 tempwidth = e.Graphics.MeasureString("零售金额", objFontNormal).Width-e.Graphics.MeasureString(dtRow["零售金额"].ToString(), objFontNormal).Width;

                e.Graphics.DrawString(dtRow["零售金额"].ToString(), objFontNormal, Brushes.Black, X + tempwidth, Y + 5);
                X += e.Graphics.MeasureString("零售金额", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                //批号
                if (dtRow["批号"].ToString().Trim().Length > 5)
                {
                    e.Graphics.DrawString(dtRow["批号"].ToString().Substring(0, 5), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["批号"].ToString().Substring(5), objFont, Brushes.Black, X, Y + 10);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["批号"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }
                X += e.Graphics.MeasureString("批号", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                //批准文号
                if (e.Graphics.MeasureString(dtRow["批准文号"].ToString().Trim(), objFontNormal).Width > 117.504)
                {
                    e.Graphics.DrawString(dtRow["批准文号"].ToString().Substring(0, 8), objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtRow["批准文号"].ToString().Substring(8), objFont, Brushes.Black, X, Y + 10);
                }
                else
                {
                    e.Graphics.DrawString(dtRow["批准文号"].ToString(), objFontNormal, Brushes.Black, X, Y + 5);
                }
                X += e.Graphics.MeasureString("批准文号", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                //有效
                if (dtRow["失效期"] != null && dtRow["失效期"].ToString() != "")
                {
                    e.Graphics.DrawString(DateTime.Parse(dtRow["失效期"].ToString()).ToString("yy/MM/dd"), objFont, Brushes.Black, X, Y + 5);
                }
                X += e.Graphics.MeasureString("失效期", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                //			//质量
                //			e.Graphics.DrawString("合格",objFontNormal,Brushes.Black,X+fltrithgwith,Y+5);
                //			X+=e.Graphics.MeasureString("质量",objFontNormal).Width;
                //			X+=fltrithgwith*2;
                //			e.Graphics.DrawLine(PenLine,X,Y,X,Y+fltRowHeight);

                //日期
                if (dtRow["日期"] != null && dtRow["日期"].ToString() != "")
                {
                    e.Graphics.DrawString(DateTime.Parse(dtRow["日期"].ToString()).ToString("yy/MM/dd"), objFont, Brushes.Black, X, Y + 5);
                }
                X += e.Graphics.MeasureString("日期", objFontNormal).Width;
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

                    if (strSTANDARD == "1" && dt.Rows[i1]["中标金额"] != null && dt.Rows[i1]["中标金额"].ToString()!="")
					AIMUNITPRICEMONEY+=Double.Parse(dt.Rows[i1]["中标金额"].ToString());
                    if (dt.Rows[i1]["限价金额"] != null && dt.Rows[i1]["限价金额"].ToString() != "")
					LIMITUNITPRICEMONEY+=Double.Parse(dt.Rows[i1]["限价金额"].ToString());
                if (dt.Rows[i1]["进价金额"] != null && dt.Rows[i1]["进价金额"].ToString() != "")
					TOTAILMONEY+=Double.Parse(dt.Rows[i1]["进价金额"].ToString());
                if (dt.Rows[i1]["零售金额"] != null && dt.Rows[i1]["零售金额"].ToString() != "")
                    SALTOTAILMONEY += Double.Parse(dt.Rows[i1]["零售金额"].ToString());
			}
            //if (strSIGN == "1")
            //{
                X = fltLeftIndentProp;
                Y += fltRowHeight;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                e.Graphics.DrawString("合  计", objFontTitle, Brushes.Black, X + 50, Y + 5);
                X += e.Graphics.MeasureString("序 号", objFontNormal).Width;
                X += fltrithgwith + 160;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                X += (e.Graphics.MeasureString("药品名称及规格", objFontNormal).Width - 5);
                X += e.Graphics.MeasureString("剂型", objFontNormal).Width;
                X += e.Graphics.MeasureString("生产厂家", objFontNormal).Width;
                X += fltrithgwith * 2;
                X += e.Graphics.MeasureString("位", objFontNormal).Width;
                X += e.Graphics.MeasureString("数量", objFontNormal).Width;
                X += fltrithgwith * 3 + 17;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                e.Graphics.DrawString(TOTAILMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
                X += e.Graphics.MeasureString("进价", objFontNormal).Width;
                X += fltrithgwith * 2;
                X += e.Graphics.MeasureString("进价金额", objFontNormal).Width;
                X += fltrithgwith * 2;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y + fltRowHeight, PageWith - fltRightIndentProp, Y + fltRowHeight);
                e.Graphics.DrawString(AIMUNITPRICEMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
                X += e.Graphics.MeasureString("中 标", objFont).Width;
                X += e.Graphics.MeasureString("中标金额", objFontNormal).Width;
                X += fltrithgwith * 4 - 4;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);

                e.Graphics.DrawString(LIMITUNITPRICEMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
                X += e.Graphics.MeasureString("限 价", objFontNormal).Width;
                X += e.Graphics.MeasureString("限价金额", objFontNormal).Width;
                X += fltrithgwith * 4 - 6;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                e.Graphics.DrawString(SALTOTAILMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
                X += e.Graphics.MeasureString("零 售", objFontNormal).Width;
                X += e.Graphics.MeasureString("零售金额", objFontNormal).Width;
                X += fltrithgwith * 4 - 8;
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
                e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);
            //}
            //else
            //{
            //    X = fltLeftIndentProp;
            //    Y += fltRowHeight;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    e.Graphics.DrawString("合  计", objFontTitle, Brushes.Black, X + 50, Y + 5);
            //    X += e.Graphics.MeasureString("序 号", objFontNormal).Width;
            //    X += fltrithgwith + 165;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    X += (e.Graphics.MeasureString("药品名称及规格", objFontNormal).Width - 5);
            //    X += e.Graphics.MeasureString("剂型", objFontNormal).Width;
            //    X += e.Graphics.MeasureString("生产厂家", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    X += e.Graphics.MeasureString("位", objFontNormal).Width;
            //    X += e.Graphics.MeasureString("数量", objFontNormal).Width;
            //    X += fltrithgwith * 3 + 20;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    e.Graphics.DrawString(TOTAILMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
            //    X += e.Graphics.MeasureString("进价", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    X += e.Graphics.MeasureString("进价金额", objFontNormal).Width;
            //    X += fltrithgwith * 2;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y + fltRowHeight, PageWith - fltRightIndentProp, Y + fltRowHeight);
            //    e.Graphics.DrawString(AIMUNITPRICEMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
            //    X += e.Graphics.MeasureString("中标金额", objFontNormal).Width * 2;
            //    X += fltrithgwith * 4-4 ;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    e.Graphics.DrawString(LIMITUNITPRICEMONEY.ToString(), objFontTitle, Brushes.Black, X + 20, Y + 5);
            //    X += e.Graphics.MeasureString("中标金额", objFontNormal).Width * 2;
            //    X += fltrithgwith * 4-4;
            //    e.Graphics.DrawLine(PenLine, X, Y, X, Y + fltRowHeight);
            //    e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);

            //}

		}

	}
	}
