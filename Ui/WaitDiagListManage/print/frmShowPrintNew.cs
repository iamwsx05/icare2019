using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms; 
using com.digitalwave.controls;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmShowPrintNew 的摘要说明。
	/// </summary>
	public class frmShowPrintNew : System.Windows.Forms.Form
	{
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// 分类 1-注射 2 - 治疗3-手术4-输血
		/// </summary>
		private int intType;
		/// <summary>
		/// 打印的数据
		/// </summary>
		private System.Data.DataTable dtPrint=null;
		/// <summary>
		/// 保存病人信息
		/// </summary>
		System.Data.DataRow patientRow=null;
		/// <summary>
		/// 医院名称
		/// </summary>
		string docterName="";
		private System.Windows.Forms.Button button1;
		//private ReportDocument m_objReport=new ReportDocument();
		public frmShowPrintNew(int intType,System.Data.DataTable dt,System.Data.DataRow dtRow,string docterName)
		{
			this.intType=intType;
			this.dtPrint=dt;
			this.patientRow=dtRow;
			this.docterName=docterName;
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
			 public string OUTPATRECIPEID_CHR = "";//处方id
		public string m_strDoctor = "";

		#region 根据不同的分类来组合成不同的打印表
		/// <summary>
		/// 根据不同的分类来组合成不同的打印表
		/// </summary>
		/// <returns></returns>
		private System.Data.DataTable m_mthGetTypeDt()
		{
			System.Data.DataTable tempDt=new System.Data.DataTable();
            string str = "";
			switch(intType)
			{
				case 1:
		
					tempDt.Columns.Add("RowNO");
					tempDt.Columns.Add("medName");
					tempDt.Columns.Add("Sinature1");
					tempDt.Columns.Add("Sinature2");
					tempDt.Columns.Add("Sinature3");
					tempDt.Columns.Add("REMARK1_VCHR");
					tempDt.Columns.Add("REMARK1_VCHR2");
					tempDt.Columns.Add("REMARK1_VCHR3");
					tempDt.Columns.Add("EXECTIME_DAT");
					tempDt.Columns.Add("EXECTIME_DAT2");
					tempDt.Columns.Add("EXECTIME_DAT3");
					string Name1 = "";
					string Name2 = "";
					string Name3 = "";
					string REMARK1_VCHR = "";
					string REMARK1_VCHR2 = "";
					string REMARK1_VCHR3 = "";
					string EXECTIME_DAT = "";
					string EXECTIME_DAT2 = "";
					string EXECTIME_DAT3 = "";
					for(int i1=0;i1<dtPrint.Rows.Count;i1++)
					{
						if(dtPrint.Rows[i1]["MEDICINEID_CHR"].ToString()!="")
						{
							System.Data.DataRow newRow=tempDt.NewRow();
							newRow["RowNO"]=dtPrint.Rows[i1]["ROWNO_CHR"].ToString();
							newRow["medName"]=dtPrint.Rows[i1]["itemname_vchr"].ToString()+","+dtPrint.Rows[i1]["DISCOUNT_DEC"].ToString()+dtPrint.Rows[i1]["DOSAGEUNIT_CHR"].ToString();
							#region 找签名
							clst_opr_nurseexecute cls = new clst_opr_nurseexecute();
							cls.m_intBUSINESS_INT  = 1;
							cls.m_strOUTPATRECIPEID_CHR  = OUTPATRECIPEID_CHR;
							string row = dtPrint.Rows[i1]["ROWNO_CHR"].ToString().Trim();
							cls.m_strROWNO_CHR  = row==""?"-1":row;
                            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc= new com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc();
                            (new weCare.Proxy.ProxyOP()).Service.m_lngQueryNameXUNSHI(cls,out Name1,out Name2,out Name3
								,out REMARK1_VCHR,out REMARK1_VCHR2,out REMARK1_VCHR3
								,out EXECTIME_DAT,out EXECTIME_DAT2,out EXECTIME_DAT3
																			);
							newRow["Sinature1"] = Name1;
							newRow["Sinature2"] = Name2;
							newRow["Sinature3"] = Name3;

							newRow["REMARK1_VCHR"] = REMARK1_VCHR;
							newRow["REMARK1_VCHR2"] = REMARK1_VCHR2;
							newRow["REMARK1_VCHR3"] = REMARK1_VCHR3;

							newRow["EXECTIME_DAT"] = EXECTIME_DAT;
							newRow["EXECTIME_DAT2"] = EXECTIME_DAT2;
							newRow["EXECTIME_DAT3"] = EXECTIME_DAT3;
							#endregion
							tempDt.Rows.Add(newRow);
						}
					}
					//m_objReport.Load("Report\\CryTransfusionReport.rpt");
					break;
				case 2:
					tempDt.Columns.Add("ItemName");
					tempDt.Columns.Add("ItemSpece");
					tempDt.Columns.Add("Itemcount");
					for(int i1=0;i1<dtPrint.Rows.Count;i1++)
					{
						if(dtPrint.Rows[i1]["MEDICINETYPEID_CHR"].ToString()!="5")
						{
                            str = dtPrint.Rows[i1]["ITEMUSAGEDETAIL_VCHR"].ToString().Trim();
                            if (str != "")
                            {
                                str = "\n【详细用法】:" + str;
                            }
							System.Data.DataRow newRow=tempDt.NewRow();
							newRow["ItemName"]=" "+dtPrint.Rows[i1]["itemname_vchr"].ToString()+str;
							newRow["ItemSpece"]=dtPrint.Rows[i1]["ITEMSPEC_VCHR"].ToString();
							newRow["Itemcount"]=dtPrint.Rows[i1]["QTY_DEC"].ToString()+dtPrint.Rows[i1]["UNITID_CHR"].ToString();
							tempDt.Rows.Add(newRow);
						}
					}

					//m_objReport.Load("Report\\CrystalRptCure.rpt");
					break;
				case 3:
					tempDt.Columns.Add("ItemName");
					tempDt.Columns.Add("Date");
					for(int i1=0;i1<dtPrint.Rows.Count;i1++)
					{
						System.Data.DataRow newRow=tempDt.NewRow();
                        str = dtPrint.Rows[i1]["ITEMUSAGEDETAIL_VCHR"].ToString().Trim();
                        if (str != "")
                        {
                            str = "\n【详细用法】:" + str;
                        }
                        newRow["ItemName"] = " "+dtPrint.Rows[i1]["itemname_vchr"].ToString() + "," + dtPrint.Rows[i1]["QTY_DEC"].ToString() + dtPrint.Rows[i1]["UNITID_CHR"].ToString()+str ;
						newRow["Date"]="     年   月   日     上午/下午     时";
						tempDt.Rows.Add(newRow);
					}
					//m_objReport.Load("Report\\CrystalRptOperate.rpt");
					break;
				case 4:
					tempDt.Columns.Add("ItemName");
					tempDt.Columns.Add("ItemSpecs");
					tempDt.Columns.Add("GroudNO");
					tempDt.Columns.Add("UseCount");
                    tempDt.Columns.Add("UseType");
                    tempDt.Columns.Add("ITEMUSAGEDETAIL_VCHR");
					for(int i1=0;i1<dtPrint.Rows.Count;i1++)
					{
						System.Data.DataRow newRow=tempDt.NewRow();
						newRow["ItemName"]=dtPrint.Rows[i1]["itemname_vchr"].ToString();
						newRow["ItemSpecs"]=dtPrint.Rows[i1]["ITEMSPEC_VCHR"].ToString();
						newRow["GroudNO"]=dtPrint.Rows[i1]["ROWNO_CHR"].ToString();
                        //double tolQty = double.Parse(dtPrint.Rows[i1]["QTY_DEC"].ToString()) * double.Parse(dtPrint.Rows[i1]["DOSAGE_DEC"].ToString());
                        double tolQty = double.Parse(dtPrint.Rows[i1]["QTY_DEC"].ToString());
                        newRow["UseCount"]=tolQty.ToString()+dtPrint.Rows[i1]["UNITID_CHR"].ToString();
                        newRow["UseType"] = dtPrint.Rows[i1]["USAGENAME_VCHR"].ToString();
                        if (dtPrint.Rows[i1]["ITEMUSAGEDETAIL_VCHR"].ToString().Trim() != "")
                            newRow["ITEMUSAGEDETAIL_VCHR"] = "【详细用法】:" + dtPrint.Rows[i1]["ITEMUSAGEDETAIL_VCHR"].ToString().Trim();
                        else
                            newRow["ITEMUSAGEDETAIL_VCHR"] = "";

						tempDt.Rows.Add(newRow);
					}
                    //m_objReport.Load("Report\\cryWaitDiagListBlood.rpt");
					break;

			}
			return tempDt;
		}
		#endregion

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            //this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            //this.crystalReportViewer1.ActiveViewIndex = -1;
            //this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.crystalReportViewer1.DisplayGroupTree = false;
            //this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            //this.crystalReportViewer1.Name = "crystalReportViewer1";
            //this.crystalReportViewer1.SelectionFormula = "";
            //this.crystalReportViewer1.ShowCloseButton = false;
            //this.crystalReportViewer1.ShowExportButton = false;
            //this.crystalReportViewer1.ShowGotoPageButton = false;
            //this.crystalReportViewer1.ShowGroupTreeButton = false;
            //this.crystalReportViewer1.ShowTextSearchButton = false;
            //this.crystalReportViewer1.ShowZoomButton = false;
            //this.crystalReportViewer1.Size = new System.Drawing.Size(644, 475);
            //this.crystalReportViewer1.TabIndex = 0;
            //this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(258, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "关闭";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmShowPrintNew
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(644, 475);
            this.Controls.Add(this.button1);
            //this.Controls.Add(this.crystalReportViewer1);
            this.Name = "frmShowPrintNew";
            this.Text = "打印";
            this.Load += new System.EventHandler(this.frmShowPrintNew_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void frmShowPrintNew_Load(object sender, System.EventArgs e)
		{	
			
//			m_objReport.SetDataSource(m_mthGetTypeDt());
//crystalReportViewer1.ReportSource=m_objReport;
//			((TextObject)m_objReport.ReportDefinition.ReportObjects["TitName"]).Text =docterName;
//			((TextObject)m_objReport.ReportDefinition.ReportObjects["Name"]).Text =patientRow["NAME_VCHR"].ToString();
//			((TextObject)m_objReport.ReportDefinition.ReportObjects["sex"]).Text =patientRow["SEX_CHR"].ToString();
//			((TextObject)m_objReport.ReportDefinition.ReportObjects["age"]).Text =clsArithmetic.CalcAge(DateTime.Parse(patientRow["BIRTH_DAT"].ToString()));
////			((TextObject)m_objReport.ReportDefinition.ReportObjects["ExaminaNo"]).Text =patientRow["ExaminaNo"].ToString();
//			if(this.intType==2)//治
//			{
//				#region 得签名值
//				((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDoc"]).Text =this.m_strDoctor;
					
//				DataTable dt = new DataTable();
//				clsDcl_injectInfo objSvcs = new clsDcl_injectInfo();
//				long lng = objSvcs.m_lngQueryNameBybusiness_intAndrecipeid(3,this.OUTPATRECIPEID_CHR,3,out dt);
//				if(lng>0)
//				{
//					#region get data 
//					string name1 = "";
//					string name2 = "";
//					string name3 = "";
//					string name4 = "";
//					string name5 = "";
//					string name6 = "";
//					string name7 = "";
//					string date1 = "";
//					string date2 = "";
//					string date3 = "";
//					string date4 = "";
//					string date5 = "";
//					string date6= "";
//					string date7 = "";
//					if(dt.Rows.Count<=7)
//					{
//						for(int i=0;i<dt.Rows.Count;i++)
//						{
//							string strdate =Convert.ToDateTime(dt.Rows[i]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
//							string strName =dt.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
//							switch(i+1)
//							{
//								case 1:
//									date1 = strdate;
//									name1 = strName;
//									break;
//								case 2:
//									date2 = strdate;
//									name2 = strName;
//									break;
//								case 3:
//									date3 = strdate;
//									name3 = strName;
//									break;
//								case 4:
//									date4 = strdate;
//									name4 = strName;
//									break;
//								case 5:
//									date5 = strdate;
//									name5 = strName;
//									break;
//								case 6:
//									date6 = strdate;
//									name6 = strName;
//									break;
//								case 7:
//									date7 = strdate;
//									name7 = strName;
//									break;
//							}
							
//						}
//					}
//					else if(dt.Rows.Count>7)
//					{
//							int count = dt.Rows.Count;
//							 date7 =Convert.ToDateTime(dt.Rows[count-1]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
//							 name7 =dt.Rows[count-1]["LASTNAME_VCHR"].ToString().Trim();
//						date6 =Convert.ToDateTime(dt.Rows[count-2]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
//						name6 =dt.Rows[count-2]["LASTNAME_VCHR"].ToString().Trim();
//						date5 =Convert.ToDateTime(dt.Rows[count-3]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
//						name5 =dt.Rows[count-3]["LASTNAME_VCHR"].ToString().Trim();
//						date4 =Convert.ToDateTime(dt.Rows[count-4]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
//						name4 =dt.Rows[count-4]["LASTNAME_VCHR"].ToString().Trim();
//						date3 =Convert.ToDateTime(dt.Rows[count-5]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
//						name3 =dt.Rows[count-5]["LASTNAME_VCHR"].ToString().Trim();
//						date2 =Convert.ToDateTime(dt.Rows[count-6]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
//						name2 =dt.Rows[count-6]["LASTNAME_VCHR"].ToString().Trim();
//						date1 =Convert.ToDateTime(dt.Rows[count-7]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
//						name1 =dt.Rows[count-7]["LASTNAME_VCHR"].ToString().Trim();
//					}
//					#endregion 
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu1"]).Text =name1;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu2"]).Text =name2;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu3"]).Text =name3;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu4"]).Text =name4;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu5"]).Text =name5;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu6"]).Text =name6;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu7"]).Text =name7;

//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate1"]).Text =date1;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate2"]).Text =date2;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate3"]).Text =date3;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate4"]).Text =date4;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate5"]).Text =date5;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate6"]).Text =date6;
//					((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate7"]).Text =date7;

//				}
//				#endregion
//			}
//			if(this.intType==4)//血
//			{
//				m_mthsetDatablood();
//				((TextObject)m_objReport.ReportDefinition.ReportObjects["doctorName"]).Text =this.m_strDoctor.Trim();
//			}
//			if(this.intType==3)//手术
//			{
//				 m_mthsetDataoperation();//手术
				
//			}
		}

		private void m_mthsetDatablood()//血
		{
			#region 得签名值
				
			DataTable dt = new DataTable();
			clsDcl_injectInfo objSvcs = new clsDcl_injectInfo();
			long lng = objSvcs.m_lngQueryNameBybusiness_intAndrecipeid(5,this.OUTPATRECIPEID_CHR,11,out dt);
			if(lng>0)
			{
				#region get data 
				string name1 = "";
				string name2 = "";
				string name3 = "";
				string name4 = "";
				string name5 = "";
				string name6 = "";
				string name7 = "";
				string date1 = "";
				string date2 = "";
				string date3 = "";
				string date4 = "";
				string date5 = "";
				string date6= "";
				string date7 = "";
				if(dt.Rows.Count<=7)
				{
					for(int i=0;i<dt.Rows.Count;i++)
					{
						string strdate =Convert.ToDateTime(dt.Rows[i]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
						string strName =dt.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
						switch(i+1)
						{
							case 1:
								date1 = strdate;
								name1 = strName;
								break;
							case 2:
								date2 = strdate;
								name2 = strName;
								break;
							case 3:
								date3 = strdate;
								name3 = strName;
								break;
							case 4:
								date4 = strdate;
								name4 = strName;
								break;
							case 5:
								date5 = strdate;
								name5 = strName;
								break;
							case 6:
								date6 = strdate;
								name6 = strName;
								break;
							case 7:
								date7 = strdate;
								name7 = strName;
								break;
						}
							
					}
				}
				else if(dt.Rows.Count>7)
				{
					int count = dt.Rows.Count;
					date7 =Convert.ToDateTime(dt.Rows[count-1]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name7 =dt.Rows[count-1]["LASTNAME_VCHR"].ToString().Trim();
					date6 =Convert.ToDateTime(dt.Rows[count-2]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name6 =dt.Rows[count-2]["LASTNAME_VCHR"].ToString().Trim();
					date5 =Convert.ToDateTime(dt.Rows[count-3]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name5 =dt.Rows[count-3]["LASTNAME_VCHR"].ToString().Trim();
					date4 =Convert.ToDateTime(dt.Rows[count-4]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name4 =dt.Rows[count-4]["LASTNAME_VCHR"].ToString().Trim();
					date3 =Convert.ToDateTime(dt.Rows[count-5]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name3 =dt.Rows[count-5]["LASTNAME_VCHR"].ToString().Trim();
					date2 =Convert.ToDateTime(dt.Rows[count-6]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name2 =dt.Rows[count-6]["LASTNAME_VCHR"].ToString().Trim();
					date1 =Convert.ToDateTime(dt.Rows[count-7]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name1 =dt.Rows[count-7]["LASTNAME_VCHR"].ToString().Trim();
				}
				#endregion 
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu1"]).Text =name1;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu2"]).Text =name2;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu3"]).Text =name3;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu4"]).Text =name4;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu5"]).Text =name5;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu6"]).Text =name6;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu7"]).Text =name7;

				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate1"]).Text =date1;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate2"]).Text =date2;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate3"]).Text =date3;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate4"]).Text =date4;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate5"]).Text =date5;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate6"]).Text =date6;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate7"]).Text =date7;
			
			}
			#endregion
		}

		private void m_mthsetDataoperation()//手术
		{
			#region 得签名值
				
			DataTable dt = new DataTable();
			clsDcl_injectInfo objSvcs = new clsDcl_injectInfo();
			long lng = objSvcs.m_lngQueryNameBybusiness_intAndrecipeid(4,this.OUTPATRECIPEID_CHR,3,out dt);
			if(lng>0)
			{
				#region get data 
				string name1 = "";
				string name2 = "";
				string name3 = "";
				string name4 = "";
				string name5 = "";
				string name6 = "";
				string name7 = "";
				string date1 = "";
				string date2 = "";
				string date3 = "";
				string date4 = "";
				string date5 = "";
				string date6= "";
				string date7 = "";
				if(dt.Rows.Count<=7)
				{
					for(int i=0;i<dt.Rows.Count;i++)
					{
						string strdate =Convert.ToDateTime(dt.Rows[i]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
						string strName =dt.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
						switch(i+1)
						{
							case 1:
								date1 = strdate;
								name1 = strName;
								break;
							case 2:
								date2 = strdate;
								name2 = strName;
								break;
							case 3:
								date3 = strdate;
								name3 = strName;
								break;
							case 4:
								date4 = strdate;
								name4 = strName;
								break;
							case 5:
								date5 = strdate;
								name5 = strName;
								break;
							case 6:
								date6 = strdate;
								name6 = strName;
								break;
							case 7:
								date7 = strdate;
								name7 = strName;
								break;
						}
							
					}
				}
				else if(dt.Rows.Count>7)
				{
					int count = dt.Rows.Count;
					date7 =Convert.ToDateTime(dt.Rows[count-1]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name7 =dt.Rows[count-1]["LASTNAME_VCHR"].ToString().Trim();
					date6 =Convert.ToDateTime(dt.Rows[count-2]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name6 =dt.Rows[count-2]["LASTNAME_VCHR"].ToString().Trim();
					date5 =Convert.ToDateTime(dt.Rows[count-3]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name5 =dt.Rows[count-3]["LASTNAME_VCHR"].ToString().Trim();
					date4 =Convert.ToDateTime(dt.Rows[count-4]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name4 =dt.Rows[count-4]["LASTNAME_VCHR"].ToString().Trim();
					date3 =Convert.ToDateTime(dt.Rows[count-5]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name3 =dt.Rows[count-5]["LASTNAME_VCHR"].ToString().Trim();
					date2 =Convert.ToDateTime(dt.Rows[count-6]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name2 =dt.Rows[count-6]["LASTNAME_VCHR"].ToString().Trim();
					date1 =Convert.ToDateTime(dt.Rows[count-7]["EXECTIME_DAT"]).ToString("yyyy-MM-dd HH:mm");
					name1 =dt.Rows[count-7]["LASTNAME_VCHR"].ToString().Trim();
				}
				#endregion 
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu1"]).Text =name1;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu2"]).Text =name2;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu3"]).Text =name3;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu4"]).Text =name4;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu5"]).Text =name5;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu6"]).Text =name6;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtHu7"]).Text =name7;

				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate1"]).Text =date1;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate2"]).Text =date2;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate3"]).Text =date3;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate4"]).Text =date4;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate5"]).Text =date5;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate6"]).Text =date6;
				//((TextObject)m_objReport.ReportDefinition.ReportObjects["txtDate7"]).Text =date7;
			
			}
			#endregion
		}
		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
