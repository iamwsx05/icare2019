using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using com.digitalwave.iCare.common;
namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmOPInvoiceRpt : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        //�������� "0"�ձ��� "1"�±�
        internal string m_strShowType = "0";

        internal string m_strTitle;

        public frmOPInvoiceRpt()
        {
            InitializeComponent();
        }

        public void ShowWithParm(string p_parm)
        {
            this.m_strShowType = p_parm;
            Show();
        }

        /// <summary>
        /// ���ô��������
        /// </summary>
        public override void CreateController()
        {
            this.objController = new clsCtlOPInvoiceRpt();
            this.objController.Set_GUI_Apperance(this);
        }

        private void frmOPInvoiceRpt_Load(object sender, EventArgs e)
        {
            #region �շ�Ա�б�
            DataTable dt;
            clsDomainControl_Register domain = new clsDomainControl_Register();
            domain.m_lngGetCheckMan(out dt, "0");
            if (dt != null)
            {
                this.m_cboCheckMan.Items.Clear(); 
                this.m_cboCheckMan.m_mthClear();

                if (dt.Rows.Count > 0)
                {
                    this.m_cboCheckMan.Item.Add("ȫ��", "1000");
                    for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                    {
                        this.m_cboCheckMan.Item.Add(dt.Rows[i1]["LASTNAME_VCHR"].ToString(), dt.Rows[i1]["BALANCEEMP_CHR"].ToString());
                    }
                    this.m_cboCheckMan.SelectedIndex = 0;
                }

            }



            #endregion

            #region ��ȡ�շ�Ա���ڲ���
            DataTable dtdept = null;
            string strEmpId = this.LoginInfo.m_strEmpID.ToString().Trim(); 
            (new weCare.Proxy.ProxyReport()).Service.m_lngGetRegdept(out dtdept, strEmpId);
            if (dtdept != null)
            {
                this.m_cboDeptdesc.Items.Clear();
                this.m_cboDeptdesc.m_mthClear();

                if (dtdept.Rows.Count > 0)
                {
                    this.m_cboDeptdesc.Item.Add("ȫ��", "1000");
                    for (int i = 0; i < dtdept.Rows.Count; i++)
                    {
                        this.m_cboDeptdesc.Item.Add(dtdept.Rows[i]["deptname_vchr"].ToString(), dtdept.Rows[i]["deptid_chr"].ToString());
                    }
                    this.m_cboDeptdesc.SelectedIndex = 0;
                }
            }
            #endregion

            dwRpt.LibraryList = Application.StartupPath + "\\pb_op.pbl";
            dwRpt.DataWindowObject = "d_invoice_rpt";
            if (this.m_strShowType == "0")
            {
                this.labTo.Visible = false;
                this.m_endDate.Visible = false;
                this.m_cboDeptdesc.Visible = false;
                this.m_strTitle = this.objController.m_objComInfo.m_strGetHospitalTitle() + "�����շ�Ա�շ�Ʊ����";
                this.Text = this.m_strTitle;
            }
            else
            {
                this.labTo.Visible = true;
                this.m_endDate.Visible = true;
                m_beginDate.Value = Convert.ToDateTime(m_beginDate.Value.Year.ToString() + "-" + m_beginDate.Value.Month.ToString() + "-" + "01");
                this.m_strTitle = this.objController.m_objComInfo.m_strGetHospitalTitle() + "�����շ�Ա�·�Ʊ����";
                this.Text = this.m_strTitle;
            }
            this.dwRpt.Modify("t_title.text='" + this.m_strTitle + "'");
            this.dwRpt.Modify("datawindow.print.preview=yes datawindow.print.preview.rulers=yes");
          

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                clsPublic.PlayAvi("findFILE.avi", "���ڽ������ݻ��ܣ����Ժ�...");
                ((clsCtlOPInvoiceRpt)this.objController).GetRptData();
            }
            finally
            {
                clsPublic.CloseAvi();
            }
        }

        private void btExcel_Click(object sender, EventArgs e)
        {
            if (this.dwRpt.RowCount > 0)
            {
                clsPublic.ExportDataWindow(this.dwRpt);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            clsPublic.ChoosePrintDialog(this.dwRpt, true);
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// ����ѡ��Ŀ���ID���¼����շ�Ա
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cboDeptdesc_SelectedIndexChanged(object sender, EventArgs e)
        {
           string strdeptId= this.m_cboDeptdesc.SelectItemValue.ToString();
           DataTable dt; 
            (new weCare.Proxy.ProxyReport()).Service.m_lngGetCheckManByDeptId(out dt, strdeptId);
           if (dt != null)
           {
               this.m_cboCheckMan.Items.Clear();
               this.m_cboCheckMan.m_mthClear();

               if (dt.Rows.Count > 0)
               {
                   this.m_cboCheckMan.Item.Add("ȫ��", "1000");
                   for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                   {
                       this.m_cboCheckMan.Item.Add(dt.Rows[i1]["LASTNAME_VCHR"].ToString(), dt.Rows[i1]["BALANCEEMP_CHR"].ToString());
                   }
                   this.m_cboCheckMan.SelectedIndex = 0;
               }

           }
        }
    }
}