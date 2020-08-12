using System;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlCenterMetStorageSetup 的摘要说明。
	/// </summary>
	public class clsControlCenterMetStorageSetup:com.digitalwave.GUI_Base.clsController_Base
	{
		#region parameters

		com.digitalwave.iCare.gui.HIS.clsDomainConrol_Medicne m_objDomainController = new clsDomainConrol_Medicne();

		/// <summary>
		/// 保存结果数据
		/// </summary>
		private DataTable m_dtbResult = new DataTable();

		/// <summary>
		/// 保存查询结果数据表
		/// </summary>
		private DataTable m_dtbSearchResult = null;

		/// <summary>
		/// 保存药品类型
		/// </summary>
		private DataTable m_dtbMedType = new DataTable();

		#endregion

		#region Constructor
		public clsControlCenterMetStorageSetup()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#endregion

		#region 设置窗体对象
		com.digitalwave.iCare.gui.HIS.frmCenterMetStorageSetup m_objViewer;
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			m_objViewer = (frmCenterMetStorageSetup)frmMDI_Child_Base_in;
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
			m_objDomainController.m_lngGetMedTypeArr(out typeName,MedTypeList);
			string fromName="";
			this.m_objViewer.cobSelectType.Item.Add("全部","");
			for(int i1=0;i1<MedTypeList.Length;i1++)
			{
				this.m_objViewer.cobSelectType.Item.Add(typeName[i1],MedTypeList[i1]);
				fromName+="{"+typeName[i1]+"}";
			}
            if (MedTypeList.Length > 0)
            {
                this.m_objViewer.cobSelectType.SelectedIndex = MedTypeList.Length;
            }
            else
            {
                this.m_objViewer.cobSelectType.SelectedIndex = 0;
            }
			this.m_objViewer.Name="药房"+fromName+"缺药设定";
			this.m_objViewer.Text="药房"+fromName+"缺药设定";
		}
		#endregion

		#region 药品类型选择事件
		public void m_mthCboSele()
		{
			string[] MedTypeListsele=new string[1];
			if(this.m_objViewer.cobSelectType.SelectItemText!="全部")
			{
				MedTypeListsele[0]=this.m_objViewer.cobSelectType.SelectItemValue;
				m_objDomainController.m_lngGetMetList(MedTypeListsele
					,1,out m_dtbResult);
			}
			else
			{
				m_objDomainController.m_lngGetMetList(MedTypeList
					,1,out m_dtbResult);
			}
			this.m_objViewer.m_dtgMedicineList.m_mthSetDataTable(m_dtbResult);
			this.m_objViewer.m_dtgMedicineList.BeginUpdate();
			for(int i1=0;i1<m_dtbResult.Rows.Count;i1++)
			{
				if(m_dtbResult.Rows[i1]["缺药"].ToString()=="√")
				{
					for(int f2=0;f2<22;f2++)
					{
						this.m_objViewer.m_dtgMedicineList.m_mthFormatCell(i1,f2,m_objViewer.m_dtgMedicineList.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
					}

				}
			}
			this.m_objViewer.m_dtgMedicineList.EndUpdate();

		}
		#endregion

		#region 绑定DataGrid
		public void m_mthBindDataGrid(DataTable p_dtbResult)
		{
			if(p_dtbResult.Rows.Count==0)
			{
				this.m_objViewer.m_dtgMedicineList.m_mthDeleteAllRow();
				return;
			}
			this.m_objViewer.m_dtgMedicineList.m_mthSetDataTable(p_dtbResult);
			this.m_objViewer.m_dtgMedicineList.BeginUpdate();
			for(int i1=0;i1<p_dtbResult.Rows.Count;i1++)
			{
				if(p_dtbResult.Rows[i1]["缺药"].ToString()=="√")
				{
					for(int f2=0;f2<23;f2++)
					{
						this.m_objViewer.m_dtgMedicineList.m_mthFormatCell
							(i1,f2,m_objViewer.m_dtgMedicineList.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
					}

				}
			}
			this.m_objViewer.m_dtgMedicineList.EndUpdate();
		}
		#endregion
		#region 设置缺药标志
		public void m_mthSetLackMedicineFlag(int p_intFlag)
		{
			int intRowNuber=this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber;
			string strMedID = m_objViewer.m_dtgMedicineList.m_objGetRow(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber)["MEDICINEID_CHR"].ToString();;
			long lngRes=m_objDomainController.m_lngSetCenterStorageFlag(strMedID,p_intFlag);
			if(blCommand)
			{
				if(lngRes>0)
				{
					if(p_intFlag == 1)
					{
						m_objViewer.m_dtgMedicineList.m_objGetRow(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber)[15]="√";
						m_dtbResult.Rows[this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber]["缺药"]="√";
						for(int i1=0;i1<21;i1++)
						{
							this.m_objViewer.m_dtgMedicineList.m_mthFormatCell(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber,i1,
								m_objViewer.m_dtgMedicineList.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
						}
					}
					else
					{
						m_objViewer.m_dtgMedicineList.m_objGetRow(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber)[15]="×";

						for(int i1=0;i1<21;i1++)
						{
							this.m_objViewer.m_dtgMedicineList.m_mthFormatReset(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber,i1);
						}
					}
					m_mthTooBarButtonEnableSetUp();
				}
			}
			else
			{
				
				if(lngRes>0)
				{
					if(p_intFlag == 1)
					{
						m_objViewer.m_dtgMedicineList.m_objGetRow(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber)[15]="√";
						FidTable.Rows[this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber]["缺药"]="√";
						for(int i1=0;i1<m_dtbResult.Rows.Count;i1++)
						{
							if(m_dtbResult.Rows[i1]["MEDICINEID_CHR"]==FidTable.Rows[this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber]["MEDICINEID_CHR"])
							{
								m_dtbResult.Rows[i1][15]="√";
								break;
							}
						}
						for(int i1=0;i1<19;i1++)
						{
							this.m_objViewer.m_dtgMedicineList.m_mthFormatCell(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber,i1,
								m_objViewer.m_dtgMedicineList.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
						}
					}
					else
					{
						m_objViewer.m_dtgMedicineList.m_objGetRow(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber)[15]="×";
						FidTable.Rows[this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber]["缺药"]="×";
						for(int i1=0;i1<m_dtbResult.Rows.Count;i1++)
						{
							if(m_dtbResult.Rows[i1]["MEDICINEID_CHR"]==FidTable.Rows[this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber]["MEDICINEID_CHR"])
							{
								m_dtbResult.Rows[i1][15]="×";
								break;
							}
						}
						for(int i1=0;i1<21;i1++)
						{
							this.m_objViewer.m_dtgMedicineList.m_mthFormatReset(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber,i1);
						}
					}
					m_mthTooBarButtonEnableSetUp();
				}
			}
		}
		#endregion

		#region 工具栏按钮有效性设定
		public void m_mthTooBarButtonEnableSetUp()
		{
			if(m_objViewer.m_dtgMedicineList.m_objGetRow(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber)[15].ToString()=="√")
			{
				this.m_objViewer.m_tbBtnOK.Enabled=true;
				this.m_objViewer.m_tbBtnNot.Enabled=false;
			}
			else
			{
				this.m_objViewer.m_tbBtnNot.Enabled=true;
				this.m_objViewer.m_tbBtnOK.Enabled=false;
			}
		}
		#endregion

		#region 显示药品信息
		public void m_mthShowMedicineInfo()
		{
			frmMetInfo objMedInfo=new frmMetInfo();
			clsMedicines_VO objMedicineVO = new clsMedicines_VO();
			DataRow dr = this.m_objViewer.m_dtgMedicineList.m_objGetRow(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber);
			for(int i1=0;i1<19;i1++)
			{
				objMedicineVO.m_strASSISTCODE_CHR = dr[0].ToString();
				objMedicineVO.m_strMEDICINENAME_VCHR = dr[1].ToString();
				objMedicineVO.m_strMEDICINEENGNAME_VCHR = dr[2].ToString();
				objMedicineVO.m_strMEDICINETYPENAME_CHR = dr[3].ToString();
				objMedicineVO.m_strMEDICINEPREPTYPENAME_CHR = dr[4].ToString();
				objMedicineVO.m_strMEDSPEC_VCHR = dr[5].ToString();
				objMedicineVO.m_strPRODUCTORNAME_CHR = dr[6].ToString();
				if(dr[7].ToString() != "")
				{
					objMedicineVO.m_dblDOSAGE_DEC = Convert.ToDouble(dr[7].ToString());
				}
				objMedicineVO.m_strDOSAGEUNITNAME_CHR = dr[8].ToString();
				objMedicineVO.m_strOPUNITNAME_CHR = dr[9].ToString();
				objMedicineVO.m_strIPUNITNAME_CHR = dr[10].ToString();
				
				if(dr[15].ToString() == "√")
				{
					objMedicineVO.m_intIPNOQTYFLAG_INT = 1;
				}
				else
				{
					objMedicineVO.m_intIPNOQTYFLAG_INT = 0;
				}

				objMedicineVO.m_strISANAESTHESIA_CHR = dr[15].ToString();
				objMedicineVO.m_strISCHLORPROMAZINE_CHR = dr[16].ToString();
				objMedicineVO.m_strISCOSTLY_CHR = dr[17].ToString();
				objMedicineVO.m_strISSELF_CHR = dr[18].ToString();
				objMedicineVO.m_strISIMPORT_CHR = dr[19].ToString();

				objMedicineVO.m_strISSELFPAY_CHR = dr[20].ToString();

			}
			objMedInfo.ShowMe(objMedicineVO,1);
		}
		#endregion
		#region 判断用户输入
		private bool m_CheckValues()
		{
			if(this.m_objViewer.m_CobSele.Text!=""&&this.m_objViewer.m_txtVal.Text!="")
			{
				return true;
			}
			else
			{
				return false;
			}

		}
		#endregion
		#region 查找事件
		/// <summary>
		/// 查找事件
		/// </summary>
		/// <summary>
		/// 查找事件
		/// </summary>
		DataTable FidTable=null;
		/// <summary>
		///flase 查找，true
		/// </summary>
		bool blCommand=true;
		public void m_lngFind()
		{
			if(m_CheckValues())
			{
				if(FidTable == null)
				{
					FidTable = m_dtbResult.Clone();
				}
				FidTable.Rows.Clear();
				string strFind=this.m_objViewer.m_txtVal.Text;
				DataRow[] seleRowArr=null;
				switch(this.m_objViewer.m_CobSele.Text)
				{
					case "药品助记码":
						seleRowArr=m_dtbResult.Select("助记码 like '"+strFind+"%'");
						break;
					case "药品名称":
						seleRowArr=m_dtbResult.Select("药品名称 like '"+strFind+"%'");
						break;
					case "药品通用名":
						seleRowArr=m_dtbResult.Select("药品通用名 like '"+strFind+"%'");
						break;
					case "英文名称":
						seleRowArr=m_dtbResult.Select("英文名 like '"+strFind+"%'");
						break;
					case "拼音码":
						strFind=strFind.ToUpper();
						seleRowArr=m_dtbResult.Select("拼音码 like '"+strFind+"%'");
						break;
					case "五笔码":
						strFind=strFind.ToUpper();
						seleRowArr=m_dtbResult.Select("五笔码 like '"+strFind+"%'");
						break;

				}
				if(seleRowArr!=null&&seleRowArr.Length>0)
				{
					for(int i=0;i<seleRowArr.Length;i++)
					{
						m_mthContructDataRow(seleRowArr[i],ref FidTable);
					}
				}

				this.m_objViewer.m_dtgMedicineList.m_mthSetDataTable(FidTable);
				this.m_objViewer.m_dtgMedicineList.m_mthFormatReset();
				this.m_objViewer.m_dtgMedicineList.BeginUpdate();
				for(int i1=0;i1<FidTable.Rows.Count;i1++)
				{

					if(FidTable.Rows[i1]["缺药"].ToString()=="√")
					{
						for(int f2=0;f2<23;f2++)
						{
							this.m_objViewer.m_dtgMedicineList.m_mthFormatCell(i1,f2,m_objViewer.m_dtgMedicineList.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
						}

					}
				}
				this.m_objViewer.m_dtgMedicineList.EndUpdate();
				blCommand=false;
	
			}
		}
		#endregion

		#region 构造DataTable的行数据
		public void m_mthContructDataRow(DataRow p_drSourcRow,ref DataTable p_dtb)
		{
			DataRow dr = p_dtb.NewRow();
            for (int i1 = 0; i1 < p_dtb.Columns.Count; i1++)
            {
                dr[i1] = p_drSourcRow[i1];
            }
			p_dtb.Rows.Add(dr);
		}
		#endregion
		#region 返回按钮事件
		public void m_mthReturn()
		{
			FidTable=null;
			if(!blCommand)
			{
				this.m_objViewer.m_dtgMedicineList.m_mthSetDataTable(m_dtbResult);
				blCommand=true;

				this.m_objViewer.m_dtgMedicineList.BeginUpdate();
				for(int i1=0;i1<m_dtbResult.Rows.Count;i1++)
				{
					if(m_dtbResult.Rows[i1][15].ToString()=="√")
					{
						for(int f2=0;f2<21;f2++)
						{
							this.m_objViewer.m_dtgMedicineList.m_mthFormatCell(i1,f2,m_objViewer.m_dtgMedicineList.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
						}

					}
				}
				this.m_objViewer.m_dtgMedicineList.EndUpdate();
				this.m_objViewer.m_dtgMedicineList.Refresh();
			}
			blCommand=true;
			m_objViewer.m_txtVal.Text="";
			m_objViewer.gbFind.Visible = false;

		}
		#endregion

	}
}
