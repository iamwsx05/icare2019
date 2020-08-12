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
	/// clsControlStorageSetup ��ժҪ˵����
	/// </summary>
	public class clsControlMetStorageSetup:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMetStorageSetup()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region ���ô������

		private clsDomainConrol_Medicne m_objDoMain=new clsDomainConrol_Medicne();
		/// <summary>
		/// �������ݱ�
		/// </summary>
		private System.Data.DataTable objResultArr;
		/// <summary>
		/// �����ѯ������ݱ�
		/// </summary>
		private DataTable FidTable=null;
		/// <summary>
		/// ����ҩƷ����
		/// </summary>
		private DataTable medTypebt=new DataTable();
		/// <summary>
		/// �жϵ�ǰDatagrid��ʾ�����ݲ��ҵõ��Ļ��Ǵ����ݿ�õ���
		/// </summary>
		bool blCommand =true;
		/// <summary>
		/// ������ҵ����ݵ��ܺ�
		/// </summary>
//		int  intfind =0;

		com.digitalwave.iCare.gui.HIS.frmMetStorageSetup m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMetStorageSetup)frmMDI_Child_Base_in;
		}
		#endregion

		#region ����ı䷢�����¼�
		public void m_lngCellChang()
		{
            if (this.m_objViewer.isReadOnly == false)
            {
                if (blCommand)
                {
                    if (objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15].ToString() == "��")
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
                    if (FidTable.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15].ToString() == "��")
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

		#region ���ڳ�ʼ��
		/// <summary>
		/// ���ڳ�ʼ��
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
			this.m_objViewer.cobSelectType.Item.Add("ȫ��","");
			for(int i1=0;i1<MedTypeList.Length;i1++)
			{
				this.m_objViewer.cobSelectType.Item.Add(typeName[i1],MedTypeList[i1]);
				fromName+="{"+typeName[i1]+"}";
			}
			this.m_objViewer.cobSelectType.SelectedIndex=0;
			this.m_objViewer.Name="ҩ��"+fromName+"ȱҩ�趨";
			this.m_objViewer.Text="ҩ��"+fromName+"ȱҩ�趨";
		}
		#endregion

		#region �����������Datagrid
		/// <summary>
		/// �����������Datagrid
		/// </summary>
		public void m_lngFillDataGrid(string medType)
		{
//			m_cobSelect(medType);
		}
		#endregion	

		#region ҩƷ����ѡ���¼�
		public void m_mthCboSele()
		{
			string[] MedTypeListsele=new string[1];
			if(this.m_objViewer.cobSelectType.SelectItemText!="ȫ��")
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
				if(objResultArr.Rows[i1]["ȱҩ"].ToString()=="��")
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

		#region �����ť�¼�
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
					
						objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15]="��";
						objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15]="��";
						for(int i1=0;i1<22;i1++)
						{
							this.m_objViewer.DgdMed.m_mthFormatCell(this.m_objViewer.DgdMed.CurrentCell.RowNumber,i1,m_objViewer.DgdMed.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
						}
					}
					else
					{
	
						objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15]="��";

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
						FidTable.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15]="��";

							for(int i1=0;i1<objResultArr.Rows.Count;i1++)
							{
								if(objResultArr.Rows[i1]["MEDICINEID_CHR"]==FidTable.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber]["MEDICINEID_CHR"])
								{
									objResultArr.Rows[i1][15]="��";
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
						FidTable.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][15]="��";
						for(int i1=0;i1<objResultArr.Rows.Count;i1++)
						{
							if(objResultArr.Rows[i1]["MEDICINEID_CHR"]==FidTable.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber]["MEDICINEID_CHR"])
							{
								objResultArr.Rows[i1][15]="��";
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

		#region ���ذ�ť�¼�
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
					if(objResultArr.Rows[i1][15].ToString()=="��")
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

		#region �����¼�
		/// <summary>
		/// �����¼�
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
					case "ҩƷ������":
						seleRowArr=objResultArr.Select("������ like '"+strFind+"%'");
						break;
					case "ҩƷ����":
						seleRowArr=objResultArr.Select("ҩƷ���� like '"+strFind+"%'");
						break;
					case "ҩƷͨ����":
						seleRowArr=objResultArr.Select("ҩƷͨ���� like '"+strFind+"%'");
						break;
					case "Ӣ������":
						seleRowArr=objResultArr.Select("Ӣ���� like '"+strFind+"%'");
						break;
					case "ƴ����":
						strFind=strFind.ToUpper();
						seleRowArr=objResultArr.Select("ƴ���� like '"+strFind+"%'");
						break;
					case "�����":
						strFind=strFind.ToUpper();
						seleRowArr=objResultArr.Select("����� like '"+strFind+"%'");
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

					if(FidTable.Rows[i1]["ȱҩ"].ToString()=="��")
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

		#region �ж��û�����
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

		#region ˫���¼�
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
				
				if(objResultArr.Rows[this.m_objViewer.DgdMed.CurrentCell.RowNumber][14].ToString()=="��")
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

		#region ����DataTable��������
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
