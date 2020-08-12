using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlStorageSetup 的摘要说明。
	/// </summary>
	public class clsControlMetStorageSetup:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMetStorageSetup()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		#region 设置窗体对象

		private clsDomainConrol_Medicne m_objDoMain=new clsDomainConrol_Medicne();
		/// <summary>
		/// 保存数据表
		/// </summary>
		private System.Data.DataTable objResultArr;
		/// <summary>
		/// 保存查询结果数据表
		/// </summary>
		private DataTable FidTable=null;
		/// <summary>
		/// 保存药品类型
		/// </summary>
		private DataTable medTypebt=new DataTable();
		/// <summary>
		/// 判断当前Datagrid显示的数据查找得到的还是从数据库得到的
		/// </summary>
		bool blCommand =true;
		/// <summary>
		/// 保存查找到数据的总和
		/// </summary>
//		int  intfind =0;

		com.digitalwave.iCare.gui.HIS.frmMetStorageSetup m_objViewer;
		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMetStorageSetup)frmMDI_Child_Base_in;
		}
		#endregion

		#region 焦点改变发生的事件
		public void m_lngCellChang()
		{
            if (this.m_objViewer.isReadOnly == false)
            {
                if (blCommand)
                {
                    if (objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15].ToString() == "√")
                    {
                        this.m_objViewer.m_tbOK.Enabled = true;
                        this.m_objViewer.m_tbNot.Enabled = false;
                    }
                    else
                    {
                        this.m_objViewer.m_tbNot.Enabled = true;
                        this.m_objViewer.m_tbOK.Enabled = false;

                    }
                }
                else
                {
                    if (FidTable.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15].ToString() == "√")
                    {
                        this.m_objViewer.m_tbOK.Enabled = true;
                        this.m_objViewer.m_tbNot.Enabled = false;
                    }
                    else
                    {
                        this.m_objViewer.m_tbNot.Enabled = true;
                        this.m_objViewer.m_tbOK.Enabled = false;
                    }
                }
            }

		}
		#endregion

		#region 窗口初始化
		/// <summary>
		/// 窗口初始化
		/// </summary>
		string[]  MedTypeList;
		public void m_mthFromLoad(string m_strMedType)
		{
			string[] typeName;
			string str1="*";
			char[] delimiter=str1.ToCharArray();
			MedTypeList=m_strMedType.Split(delimiter);
			m_objDoMain.m_lngGetMedTypeArr(out typeName,MedTypeList);
			string fromName="";
			this.m_objViewer.cobSelectType.Item.Add("全部","");
			for(int i1=0;i1<MedTypeList.Length;i1++)
			{
				this.m_objViewer.cobSelectType.Item.Add(typeName[i1],MedTypeList[i1]);
				fromName+="{"+typeName[i1]+"}";
			}
			this.m_objViewer.cobSelectType.SelectedIndex=0;
			this.m_objViewer.Name="药房"+fromName+"缺药设定";
			this.m_objViewer.Text="药房"+fromName+"缺药设定";
		}
		#endregion

		#region 返回数据填充Datagrid
		/// <summary>
		/// 返回数据填充Datagrid
		/// </summary>
		public void m_lngFillDataGrid(string medType)
		{
//			m_cobSelect(medType);
		}
		#endregion	

		#region 药品类型选择事件
		public void m_mthCboSele()
		{
			string[] MedTypeListsele=new string[1];
			if(this.m_objViewer.cobSelectType.SelectItemText!="全部")
			{
				MedTypeListsele[0]=this.m_objViewer.cobSelectType.SelectItemValue;
				m_objDoMain.m_lngGetMetList(MedTypeListsele
					,0,out objResultArr);
			}
			else
			{
				m_objDoMain.m_lngGetMetList(MedTypeList
					,0,out objResultArr);
			}
			this.m_objViewer.DgdMed.m_mthSetDataTable(objResultArr);
			this.m_objViewer.DgdMed.BeginUpdate();
			for(int i1=0;i1<objResultArr.Rows.Count;i1++)
			{
				if(objResultArr.Rows[i1]["缺药"].ToString()=="√")
				{
					for(int f2=0;f2<22;f2++)
					{
						this.m_objViewer.DgdMed.m_mthFormatCell(i1,f2,m_objViewer.DgdMed.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
					}

				}
			}
			this.m_objViewer.DgdMed.EndUpdate();

		}
		#endregion

		#region 点击按钮事件
		public void m_btCheck(int p_ing)
		{
			if(blCommand)
			{
				int RowNuber=this.m_objViewer.DgdMed.CurrentCell.RowNumber;
				string MetID=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber]["MEDICINEID_CHR"].ToString();
				long lngRes=m_objDoMain.m_lngSetStorage(MetID,p_ing);
				if(lngRes>0)
				{
					if(p_ing==1)
					{
					
						objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15]="√";
						objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15]="√";
						for(int i1=0;i1<22;i1++)
						{
							this.m_objViewer.DgdMed.m_mthFormatCell(this.m_objViewer.DgdMed.CurrentCell.RowNumber,i1,m_objViewer.DgdMed.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
						}
					}
					else
					{
	
						objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15]="×";

						for(int i1=0;i1<19;i1++)
						{
							this.m_objViewer.DgdMed.m_mthFormatReset(this.m_objViewer.DgdMed.CurrentCell.RowNumber,i1);
						}
					}

				}
			}
			else
			{
				int RowNuber=this.m_objViewer.DgdMed.CurrentCell.RowNumber;
				string MetID=FidTable.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber]["MEDICINEID_CHR"].ToString();
				long lngRes=m_objDoMain.m_lngSetStorage(MetID,p_ing);
				if(lngRes>0)
				{
					if(p_ing==1)
					{
						FidTable.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15]="√";

							for(int i1=0;i1<objResultArr.Rows.Count;i1++)
							{
								if(objResultArr.Rows[i1]["MEDICINEID_CHR"]==FidTable.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber]["MEDICINEID_CHR"])
								{
									objResultArr.Rows[i1][15]="√";
									break;
								}
							}
			
						for(int i1=0;i1<22;i1++)
						{
							this.m_objViewer.DgdMed.m_mthFormatCell(this.m_objViewer.DgdMed.CurrentCell.RowNumber,i1,m_objViewer.DgdMed.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
						}
					}
					else
					{
						FidTable.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15]="×";
						for(int i1=0;i1<objResultArr.Rows.Count;i1++)
						{
							if(objResultArr.Rows[i1]["MEDICINEID_CHR"]==FidTable.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber]["MEDICINEID_CHR"])
							{
								objResultArr.Rows[i1][15]="×";
								break;
							}
						}
						for(int i1=0;i1<19;i1++)
						{
							this.m_objViewer.DgdMed.m_mthFormatReset(this.m_objViewer.DgdMed.CurrentCell.RowNumber,i1);
						}
					}

				}

			}

		}
		#endregion

		#region 返回按钮事件
		public void m_lngReture()
		{
			FidTable=null;
			if(!blCommand)
			{
//				this.m_objViewer.DgdMed.m_mthFormatReset();
				this.m_objViewer.DgdMed.m_mthSetDataTable(objResultArr);
				blCommand=true;

				this.m_objViewer.DgdMed.BeginUpdate();
				for(int i1=0;i1<objResultArr.Rows.Count;i1++)
				{
					if(objResultArr.Rows[i1][15].ToString()=="√")
					{
						for(int f2=0;f2<22;f2++)
						{
							this.m_objViewer.DgdMed.m_mthFormatCell(i1,f2,m_objViewer.DgdMed.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
						}

					}
				}
				this.m_objViewer.DgdMed.EndUpdate();
				this.m_objViewer.DgdMed.Refresh();
			}
			blCommand=true;

		}
		#endregion

		#region 查找事件
		/// <summary>
		/// 查找事件
		/// </summary>
		public void m_lngFind()
		{
			if(m_CheckValues())
			{
				if(FidTable == null)
				{
					FidTable = objResultArr.Clone();
				}
				FidTable.Rows.Clear();
				string strFind=this.m_objViewer.m_txtVal.Text;
				DataRow[] seleRowArr=null;
				switch(this.m_objViewer.m_CobSele.Text)
				{
					case "药品助记码":
						seleRowArr=objResultArr.Select("助记码 like '"+strFind+"%'");
						break;
					case "药品名称":
						seleRowArr=objResultArr.Select("药品名称 like '"+strFind+"%'");
						break;
					case "药品通用名":
						seleRowArr=objResultArr.Select("药品通用名 like '"+strFind+"%'");
						break;
					case "英文名称":
						seleRowArr=objResultArr.Select("英文名 like '"+strFind+"%'");
						break;
					case "拼音码":
						strFind=strFind.ToUpper();
						seleRowArr=objResultArr.Select("拼音码 like '"+strFind+"%'");
						break;
					case "五笔码":
						strFind=strFind.ToUpper();
						seleRowArr=objResultArr.Select("五笔码 like '"+strFind+"%'");
						break;

				}
				if(seleRowArr!=null&&seleRowArr.Length>0)
				{
					for(int i=0;i<seleRowArr.Length;i++)
					{
						m_mthContructDataRow(seleRowArr[i],ref FidTable);
					}
				}
				this.m_objViewer.DgdMed.m_mthSetDataTable(FidTable);
				this.m_objViewer.DgdMed.m_mthFormatReset();
				this.m_objViewer.DgdMed.BeginUpdate();
				for(int i1=0;i1<FidTable.Rows.Count;i1++)
				{

					if(FidTable.Rows[i1]["缺药"].ToString()=="√")
					{
						for(int f2=0;f2<22;f2++)
						{
							this.m_objViewer.DgdMed.m_mthFormatCell(i1,f2,m_objViewer.DgdMed.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
						}

					}
				}
				this.m_objViewer.DgdMed.EndUpdate();
				blCommand=false;
			}
		}
		#endregion

		#region 判断用户输入
		private bool m_CheckValues()
		{
			if(this.m_objViewer.m_CobSele.Text!=""||this.m_objViewer.m_txtVal.Text!="")
			{
				return true;
			}
			else
			{
				return false;
			}

		}
		#endregion

		#region 双击事件
		public void DoubleCleck()
		{
			frmMetInfo MetInfo=new frmMetInfo();
			clsMedicines_VO objArr=new clsMedicines_VO();
			for(int i1=0;i1<19;i1++)
			{
				objArr.m_strASSISTCODE_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][0].ToString();
				objArr.m_strMEDICINENAME_VCHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][1].ToString();
				objArr.m_strMEDICINEENGNAME_VCHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][2].ToString();
				objArr.m_strMEDICINETYPENAME_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][3].ToString();
				objArr.m_strMEDICINEPREPTYPENAME_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][4].ToString();
				objArr.m_strMEDSPEC_VCHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][5].ToString();
				objArr.m_strPRODUCTORNAME_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][6].ToString();
				if(objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][7].ToString()!="")
				{
					objArr.m_dblDOSAGE_DEC=Convert.ToDouble(objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][7].ToString());
				}
				objArr.m_strDOSAGEUNITNAME_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][8].ToString();
				objArr.m_strOPUNITNAME_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][9].ToString();
				objArr.m_strIPUNITNAME_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][10].ToString();
//				if(objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][11].ToString()!="")
//				{
//					objArr.m_dblPACKQTY_DEC=Convert.ToDouble(objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][11].ToString());
//				}
				
				if(objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][14].ToString()=="√")
				{
					objArr.m_intNOQTYFLAG_INT=1;
				}
				else
				{
					objArr.m_intNOQTYFLAG_INT=0;
				}

				objArr.m_strISANAESTHESIA_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15].ToString();
				objArr.m_strISCHLORPROMAZINE_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][16].ToString();
				objArr.m_strISCOSTLY_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][17].ToString();
				objArr.m_strISSELF_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][18].ToString();
				objArr.m_strISIMPORT_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][19].ToString();

				objArr.m_strISSELFPAY_CHR=objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][20].ToString();

			}
			MetInfo.ShowMe(objArr,0);
		}
		#endregion

		#region 构造DataTable的行数据
		public void m_mthContructDataRow(DataRow p_drSourcRow,ref DataTable p_dtb)
		{
			DataRow dr = p_dtb.NewRow();
			dr[0] = p_drSourcRow[0];
			dr[1] = p_drSourcRow[1];
			dr[2] = p_drSourcRow[2];
			dr[3] = p_drSourcRow[3];
			dr[4] = p_drSourcRow[4];
			dr[5] = p_drSourcRow[5];
			dr[6] = p_drSourcRow[6];
			dr[7] = p_drSourcRow[7];
			dr[8] = p_drSourcRow[8];
			dr[9] = p_drSourcRow[9];
			dr[10] = p_drSourcRow[10];
			dr[11] = p_drSourcRow[11];
			dr[12] = p_drSourcRow[12];
			dr[13] = p_drSourcRow[13];
			dr[14] = p_drSourcRow[14];
			dr[15] = p_drSourcRow[15];
			dr[16] = p_drSourcRow[16];
			dr[17] = p_drSourcRow[17];
			dr[18] = p_drSourcRow[18];
			dr[19] = p_drSourcRow[19];
			dr[20] = p_drSourcRow[20];
			dr[21] = p_drSourcRow[21];
			dr[22] = p_drSourcRow[22];
			dr[23] = p_drSourcRow[23];
			dr[24] = p_drSourcRow[24];
			dr[25] = p_drSourcRow[25];
			dr[26] = p_drSourcRow[26];
			dr[27] = p_drSourcRow[27];
			dr[28] = p_drSourcRow[28];
			p_dtb.Rows.Add(dr);
		}
		#endregion

	}

}
