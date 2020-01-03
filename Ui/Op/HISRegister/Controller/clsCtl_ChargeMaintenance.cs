using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.middletier.HI;
using System.Collections;
using System.Drawing;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsCtl_ChargeMaintenance ��ժҪ˵����
	/// </summary>
	public class clsCtl_ChargeMaintenance: com.digitalwave.GUI_Base.clsController_Base
	{
		/// <summary>
		/// ���ݱ�,��¼��ϸ����
		/// </summary>
		private DataTable dt=null;
		/// <summary>
		/// ��¼������Ϣ�����ID����
		/// </summary>
		private ArrayList objArry=null;
        /// <summary>
        /// �����û�ѡ�����
        /// </summary>
        checkType CheckType;
        /// <summary>
        /// ����ÿһ�е�״̬
        /// </summary>
        ArrayList ColumnArr = new ArrayList();
		private clsDcl_ChargeMaintenance objSvc=null;
		private	System.Windows.Forms.DataGridTableStyle m_GridStyle;
		private DataSet ds;
		public clsCtl_ChargeMaintenance()
		{
			ds=new DataSet();
			dt=new DataTable();
			ds.Tables.Add(dt);
			dt.TableName="dt";
			objSvc=new clsDcl_ChargeMaintenance();
			objArry=new ArrayList();
			m_GridStyle=new DataGridTableStyle();
		
			m_GridStyle.MappingName="dt";
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmChargeMaintenance m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmChargeMaintenance)frmMDI_Child_Base_in;
		}
		#endregion
		#region �����ʼ������
		/// <summary>
		/// Ȩ�ޱ�־
		/// </summary>
		public bool IsCanAddItem =false;
		public void m_mthAddCat(string strCatID)
		{
			if(IsCanAddItem)
			{
			return;
			}
			string strCatName;
			switch(strCatID)
			{
				case "0001":
					strCatName ="��ҩ";
					break;
				case "0002":
					strCatName ="��ҩ";
					break;
				case "0003":
					strCatName ="����";
					break;
				case "0004":
					strCatName ="����";
					break;
				case "0006":
					strCatName ="����";
					break;
				case "0005":
					strCatName ="����";
					break;
				default:
					strCatName ="��ѡ�����";
					strCatID="";
					break;
			}
			this.m_objViewer.cmbCatType.Item.Add(strCatName,strCatID);
		}
		public void m_mthFormLoad()
		{
            
			this.m_objViewer.m_cmbFind.SelectedIndex=2;
			this.m_mthAddCat("��ѡ�����");
			this.m_mthAddCat("0001");
			this.m_mthAddCat("0002");
			this.m_mthAddCat("0003");
			this.m_mthAddCat("0004");
			this.m_mthAddCat("0005");
			this.m_mthAddCat("0006");
			

		this.m_objViewer.cmbCatType.SelectedIndex=0;
		DataTable m_dt=null;
		this.m_mthGetPatientCatInfo(out m_dt);
        this.m_mthCreatTableFrame(m_dt);
        m_mthGetCheck();
		}
		#endregion
		#region ��ȡ���˷��������Ϣ
		private void m_mthGetPatientCatInfo(out DataTable p_dt)
		{
			p_dt=null;
			objSvc.m_mthGetPatientCatInfo(out p_dt);

			
		}
		#endregion
		#region �������ݱ�Ľṹ
		private void m_mthCreatTableFrame(DataTable p_dt)
		{
			System.Data.DataColumn  dtcol=null;
			dtcol=new DataColumn();
			dtcol.ColumnName="ID";//��ĿID
			this.dt.Columns.Add(dtcol);
			dtcol=new DataColumn();
            dtcol.ColumnName = "��Ŀ���";//��Ŀ���
			this.dt.Columns.Add(dtcol);
			dtcol=new DataColumn();
            dtcol.ColumnName = "��Ŀ����";//��Ŀ����
			this.dt.Columns.Add(dtcol);
			dtcol=new DataColumn();
			dtcol.ColumnName="����";//��Ŀ����
			this.dt.Columns.Add(dtcol);
			for(int i=0;i<p_dt.Rows.Count;i++)
			{
			objArry.Add(p_dt.Rows[i]["COPAYID_CHR"].ToString().Trim());
			dtcol=new DataColumn();
			dtcol.DataType=typeof(System.Decimal);
			dtcol.DefaultValue=100;
			dtcol.ColumnName=p_dt.Rows[i]["COPAYNAME_CHR"].ToString().Trim();
            //�ô����Ա���������ͣ����������������������סԺ
            ColumnArr.Add(p_dt.Rows[i]["payflag_dec"].ToString());
			this.dt.Columns.Add(dtcol);
			}

		}
		#endregion

        #region ��ȡ�û���ѡ�����
        public void m_mthGetCheck()
        {
            CheckType = new checkType();
            if (this.m_objViewer.checkBox1.Checked)
            {
                CheckType.isCheckPubli = true;
                CheckType.PublicColor = this.m_objViewer.panel2.BackColor;
            }
            else
            {
                CheckType.isCheckPubli = false;
            }
            if (this.m_objViewer.checkBox2.Checked)
            {
                CheckType.isCheckOP = true;
                CheckType.OPColor = this.m_objViewer.panel3.BackColor;
            }
            else
            {
                CheckType.isCheckOP = false;
            }
            if (this.m_objViewer.checkBox3.Checked)
            {
                CheckType.isCheckIP = true;
                CheckType.IPColor = this.m_objViewer.panel4.BackColor;
            }
            else
            {
                CheckType.isCheckIP = false;
            }
            m_ShowColumn();
        }
        #endregion

        #region �����û�ѡ����ʾ��������
        /// <summary>
        /// �����û�ѡ����ʾ��������
        /// </summary>
        private void m_ShowColumn()
        {
            this.m_objViewer.dataGridView1.DataSource = this.dt;
            this.m_objViewer.dataGridView1.Columns[0].Visible = false;
            if (this.m_objViewer.dataGridView1.Columns.Count > 0 && ColumnArr.Count>0)
            {

                for (int i1 = 0; i1 < ColumnArr.Count; i1++)
                {
                    this.m_objViewer.dataGridView1.Columns[i1 + 4].Visible = false;
                    this.m_objViewer.dataGridView1.Columns[i1 + 4].ReadOnly = false;
                    this.m_objViewer.dataGridView1.Columns[i1 + 4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    string str =(string)ColumnArr[i1];
                    if (CheckType.isCheckPubli && str == "0")
                    {
                        this.m_objViewer.dataGridView1.Columns[i1 + 4].Visible = true;
                        this.m_objViewer.dataGridView1.Columns[i1 + 4].DefaultCellStyle.BackColor = CheckType.PublicColor;
                    }
                    if (CheckType.isCheckOP && str == "1")
                    {
                        this.m_objViewer.dataGridView1.Columns[i1 + 4].Visible = true;
                        this.m_objViewer.dataGridView1.Columns[i1 + 4].DefaultCellStyle.BackColor = CheckType.OPColor;
                    }

                    if (CheckType.isCheckIP && str == "2")
                    {
                        this.m_objViewer.dataGridView1.Columns[i1 + 4].Visible = true;
                        this.m_objViewer.dataGridView1.Columns[i1 + 4].DefaultCellStyle.BackColor = CheckType.IPColor;
                    }
                }
            }
        }

        #endregion

        #region ���ҷַ�ʽ�ı�
        public void m_cmbFind_SelectedIndexChanged()
		{
			switch(m_objViewer.m_cmbFind.SelectedIndex)
			{
				case 0://��ĿID
					m_objViewer.m_cmbFind.Tag="ITEMID_CHR";
					break;
				case 1://��Ŀ����
					m_objViewer.m_cmbFind.Tag="ITEMNAME_VCHR";
					break;
				case 2://��Ŀ����
					m_objViewer.m_cmbFind.Tag="ITEMCODE_VCHR";
					break;
				case 3://��Ŀƴ��
					m_objViewer.m_cmbFind.Tag="ITEMPYCODE_CHR";
					break;
				case 4://��Ŀ���
					m_objViewer.m_cmbFind.Tag="ITEMWBCODE_CHR";
					break;
			}
			m_objViewer.m_txtFind.Select();
		}
		#endregion
		#region �����շ���Ŀ
		public void m_mthFindChargeItem()
		{
			if(this.m_objViewer.m_txtFind.Text.Trim()=="")
			{
				MessageBox.Show("�������ѯ����!");
				this.m_objViewer.m_txtFind.Select();
				return;
			}
		DataTable m_dt;
		
			long strRet=objSvc.m_mthFindChargeItem(this.m_objViewer.m_cmbFind.Tag.ToString().Trim(),this.m_objViewer.m_txtFind.Text.Trim(),out m_dt,this.m_objViewer.cmbCatType.SelectItemValue);
			string strFirtID="";//��¼��һ�е�ID
			int location=-1;
			if(strRet>0&&m_dt.Rows.Count>0)
			{
				this.m_objViewer.m_btFind.Enabled=false;
              dwtProcessBar obj=new dwtProcessBar();
			obj.m_mthSetMaxValue(m_dt.Rows.Count);
			obj.Show();
			obj.Update();
			dt.Rows.Clear();
			while(m_dt.Rows.Count>0)
			{
					strFirtID=m_dt.Rows[m_dt.Rows.Count-1]["ITEMID_CHR"].ToString().Trim();
					DataRow dr=dt.NewRow();
					dr["ID"]=strFirtID;
                    dr["��Ŀ���"] = m_dt.Rows[m_dt.Rows.Count - 1]["ITEMCODE_VCHR"].ToString().Trim();
                    dr["����"] = m_dt.Rows[m_dt.Rows.Count - 1]["ITEMPRICE_MNY"].ToString().Trim();
                    dr["��Ŀ����"] = m_dt.Rows[m_dt.Rows.Count - 1]["ITEMNAME_VCHR"].ToString().Trim();
					location =this.m_mthGetIndex(m_dt.Rows[m_dt.Rows.Count-1]["COPAYID_CHR"].ToString().Trim());
					if(location>-1)
					{
						dr[location+4]=m_dt.Rows[m_dt.Rows.Count-1]["PRECENT_DEC"].ToString().Trim();
					}
					else
					{
					MessageBox.Show("���ݲ���ȷ!");
						return;
					}
					m_dt.Rows.RemoveAt(m_dt.Rows.Count-1);
					obj.m_mthAdd();
					for(int i=m_dt.Rows.Count-1;i>-1;i--)
					{
						if(strFirtID==m_dt.Rows[i]["ITEMID_CHR"].ToString().Trim())
						{
							location =this.m_mthGetIndex(m_dt.Rows[i]["COPAYID_CHR"].ToString().Trim());
							if(location>-1)
							{
								dr[location+4]=m_dt.Rows[i]["PRECENT_DEC"].ToString().Trim();
							}
							else
							{
								MessageBox.Show("���ݲ���ȷ!");
								return;
							}
							m_dt.Rows.RemoveAt(i);
							obj.m_mthAdd();
						}
					}
					dt.Rows.Add(dr);
				}
				if(obj!=null)
				{
					obj.Close();
				}
			}
			dt.AcceptChanges();
			this.m_objViewer.m_btFind.Enabled=true;
		}
		#endregion
		private int m_mthGetIndex(string str)
		{
		return objArry.IndexOf(str);
		}
		#region ȡ������
		public void btCancel_Click()
		{
		dt.RejectChanges();
		}
		#endregion
		#region ��������
		public void m_mthSave()
		{
			this.m_objViewer.Cursor=Cursors.WaitCursor;
			for(int i=0;i<dt.Rows.Count;i++)
			{
				if(dt.Rows[i].RowState==DataRowState.Modified)
				{
					for(int ii=0;ii<objArry.Count;ii++)
					{
					this.m_mthUpdateData(dt.Rows[i]["ID"].ToString().Trim(),objArry[ii].ToString().Trim(),dt.Rows[i][ii+4].ToString().Trim());
					}
				}
			}
			this.m_objViewer.Cursor=Cursors.Default;
			dt.AcceptChanges();
		}
		private void m_mthUpdateData(string strItemID,string strCopayID,string strValue)
		{
		objSvc.m_mthUpdateData(strItemID,strCopayID,strValue);
		}
		#endregion
		#region �رմ���
		public void m_mthCloseWindow()
		{
			if(ds.HasChanges())
			{
				if(	MessageBox.Show("�����Ѿ��ı�,�Ƿ��˳�?","ICare",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
				{
				return;
				}


			}
			
				this.m_objViewer.Close();
			
		}
		#endregion
	}
    /// <summary>
    /// ѡ������
    /// </summary>
    struct checkType
    {
        /// <summary>
        /// ѡ�й���
        /// </summary>
        public bool isCheckPubli;
        /// <summary>
        /// �������͵ı���ɫ
        /// </summary>
        public System.Drawing.Color PublicColor;
        /// <summary>
        /// ѡ������
        /// </summary>
        public bool isCheckOP;
        /// <summary>
        /// �������͵ı���ɫ
        /// </summary>
        public System.Drawing.Color OPColor;
        /// <summary>
        /// ѡ��סԺ
        /// </summary>
        public bool isCheckIP;
        /// <summary>
        /// סԺ���͵ı���ɫ
        /// </summary>
        public System.Drawing.Color IPColor;
    }
    /// <summary>
    /// ����ÿһ�е�����
    /// </summary>
    struct ColumnType
    {
        /// <summary>
        /// �ڼ���
        /// </summary>
        public int   columnMuber;
        /// <summary>
        /// ����0-�����������������סԺ
        /// </summary>
        public string strType;

    }
}
