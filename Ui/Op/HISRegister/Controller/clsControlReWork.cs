using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlReWork ��ժҪ˵����
	/// </summary>
	public class clsControlReWork:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlReWork()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���ô������
		clsDomainControl_Register clsDomain=new clsDomainControl_Register();
		com.digitalwave.iCare.gui.HIS.frmReWorkCard m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmReWorkCard)frmMDI_Child_Base_in;
		}
		#endregion
		#region ����
		/// <summary>
		/// ���濨������
		/// </summary>
		DataTable bt=new DataTable();
		/// <summary>
		/// �����������
		/// </summary>
		DataTable btFind=new DataTable();
		#endregion
		#region ��ʼ����
		public void m_lngfrmload()
		{

		}
		#endregion

		#region ��������
		public void m_findData(string strCard)
		{
            this.m_objViewer.m_txtOldCard.Clear();
            string startDate = null;
            string endDate = null;
            string strCardID = null;
            string strName = null;
            if (strCard == "")
            {
                if (this.m_objViewer.checkBox1.Checked == true)
                {
                    startDate = this.m_objViewer.DtStart.Value.ToShortDateString();
                    endDate = this.m_objViewer.dtend.Value.ToShortDateString();
                }
                if (this.m_objViewer.txt_CarID.Text != "")
                {
                    strCardID = this.m_objViewer.txt_CarID.Text;
                }
                if (this.m_objViewer.m_txtName.Text != "")
                {
                    strName = this.m_objViewer.m_txtName.Text;
                }
            }
            else
            {
                strCardID = strCard;
            }

			long lngRes=clsDomain.m_lngGetCarData(startDate,endDate,out bt,strCardID,strName);
			if(lngRes==1)
			{
				this.m_objViewer.ctlDgList.m_mthSetDataTable(bt);
				this.m_objViewer.ctlDgList.Tag="bt";
                if (bt.Rows.Count > 0)
                {
                    this.m_objViewer.ctlDgList.CurrentCell=new DataGridCell(0,0);
                    m_mthSeleCardID();
                    this.m_objViewer.m_txtNewCard.Focus();
                }
			}
		}

		#endregion
		#region �˿�����
		public void m_lngReturnCar()
		{
			if((string)this.m_objViewer.ctlDgList.Tag=="bt")
			{
				if(bt.Rows.Count>0)
				{
					string carID=bt.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["PATIENTCARDID_CHR"].ToString();
					string patNO=bt.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["PATIENTID_CHR"].ToString();
					long lngRes=clsDomain.m_lngReturnCar(carID,patNO);
					if(lngRes==1&&bt.Rows.Count>0)
					{
						bt.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["status"]="���˿�";;
					}
				}
			}
			else
			{
				if(btFind.Rows.Count>0)
				{
					string carID=btFind.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["PATIENTCARDID_CHR"].ToString();
					string patNO=btFind.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["PATIENTID_CHR"].ToString();
					long lngRes=clsDomain.m_lngReturnCar(carID,patNO);
					if(lngRes==1&&bt.Rows.Count>0)
					{
						btFind.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["status"]="���˿�";;
					}
				}
			}
		}
		#endregion
		#region �����¼�
		public void m_lngReturnClick()
		{
			this.m_objViewer.ctlDgList.m_mthSetDataTable(bt);
			this.m_objViewer.ctlDgList.Tag="bt";
		}
		#endregion
		#region ��������
		public void m_lngReturnCarFind()
		{
			if(this.m_objViewer.txt_CarID.Text==""&&this.m_objViewer.m_txtName.Text=="")
				return;
			try
			{
				btFind=bt.Clone();
			}
			catch
			{
			}
			btFind.Clear();
			for(int i1=0;i1<bt.Rows.Count;i1++)
			{
				int intComm=-1;
				if(this.m_objViewer.m_txtName.Text!="")
				{
					intComm=bt.Rows[i1]["LASTNAME_VCHR"].ToString().IndexOf(this.m_objViewer.m_txtName.Text.Trim(),0);
				}
				else
				{
					intComm=bt.Rows[i1]["PATIENTCARDID_CHR"].ToString().IndexOf(this.m_objViewer.txt_CarID.Text.Trim(),0);
				}
				if(intComm==0)
				{
					DataRow newRow=btFind.NewRow();
					newRow["PATIENTCARDID_CHR"]=bt.Rows[i1]["PATIENTCARDID_CHR"];
					newRow["PATIENTID_CHR"]=bt.Rows[i1]["PATIENTID_CHR"];
					newRow["status"]=bt.Rows[i1]["status"];
					newRow["LASTNAME_VCHR"]=bt.Rows[i1]["LASTNAME_VCHR"];
					btFind.Rows.Add(newRow);
				}
			}
			this.m_objViewer.ctlDgList.m_mthSetDataTable(btFind);
			if(btFind.Rows.Count>0)
			{
				this.m_objViewer.ctlDgList.CurrentCell=new DataGridCell(0,0);
				this.m_objViewer.ctlDgList.m_mthSelectARow(0);
			}
			this.m_objViewer.ctlDgList.Tag="btFind";
		}
		#endregion

		#region ѡ�񿨺�
		public void m_mthSeleCardID()
		{
			if((string)this.m_objViewer.ctlDgList.Tag=="bt")
			{
				if(bt.Rows.Count>0)
				{
					this.m_objViewer.m_txtOldCard.Text=bt.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["PATIENTCARDID_CHR"].ToString();
					this.m_objViewer.m_txtNewCard.Tag=bt.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["PATIENTID_CHR"].ToString();
					
				}
			}
			else
			{
				if(btFind.Rows.Count>0)
				{
					this.m_objViewer.m_txtOldCard.Text=btFind.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["PATIENTCARDID_CHR"].ToString();
					this.m_objViewer.m_txtNewCard.Tag=btFind.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["PATIENTID_CHR"].ToString();
				}
			}
		}
		#endregion
		#region �޸Ŀ���
		public long  m_mthUpdateCardID()
		{
			if(this.m_objViewer.m_txtNewCard.Text!=""&&this.m_objViewer.m_txtOldCard.Text!="")
			{
				if(clsDomain.m_lngCheckCarID(this.m_objViewer.m_txtNewCard.Text)!=3)
				{
					long lngRes=clsDomain.m_lngUpdateCar(this.m_objViewer.m_txtNewCard.Text,(string)this.m_objViewer.m_txtNewCard.Tag,this.m_objViewer.LoginInfo.m_strEmpID,this.m_objViewer.m_txtOldCard.Text.Trim());
					if(lngRes==1)
					{
						if((string)this.m_objViewer.ctlDgList.Tag=="bt")
						{
							bt.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["PATIENTCARDID_CHR"]=this.m_objViewer.m_txtNewCard.Text;
						}
						else
						{
							btFind.Rows[this.m_objViewer.ctlDgList.CurrentCell.RowNumber]["PATIENTCARDID_CHR"]=this.m_objViewer.m_txtNewCard.Text;
						}
						this.m_objViewer.m_txtNewCard.Clear();
						this.m_objViewer.m_txtOldCard.Clear();
                        return 1;
					}
				}
				else
				{
					MessageBox.Show("�����Ѿ�����һ������ռ�ã�","Icare",MessageBoxButtons.OK,MessageBoxIcon.Stop);
					this.m_objViewer.m_txtNewCard.Focus();
                    return 0;
				}
			}
            return 0;
		}
		#endregion


	}
}
