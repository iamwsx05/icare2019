using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmAuto : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        com.digitalwave.iCare.gui.HIS.frmOPMedStoreWin ObjFrm = null;
        /// <summary>
        /// 1-���մ����ţ��������չ���
        /// </summary>
        string status = "";
        public frmAuto(com.digitalwave.iCare.gui.HIS.frmOPMedStoreWin  objFrm,string strstatus)
        {
            status = strstatus;
            ObjFrm = objFrm;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (status == "1")
            {
                if (e.KeyCode == Keys.Enter && textBox1.Text.Length == 18)
                {
                    if (((clsControlOPMedStore)ObjFrm.objController).m_mthFindRow(ObjFrm.m_lsvPatientDetial, textBox1.Text.Trim(), true))
                    {
                        status = "2";
                        textBox1.Text = "";
                        if (ObjFrm.statusWindows.statusTone == 1)
                        {
                            this.Text = "�Զ���ҩ�������빤�ţ�";
                        }
                        else
                        {
                            this.Text = "�Զ���ҩ�������빤�ţ�";
                        }
                        textBox1.Focus();
                    }
                    else
                    {
                        textBox1.SelectAll();
                        ((clsControlOPMedStore)ObjFrm.objController).publiClass.m_mthShowWarning(textBox1, "�˴��������б��в����ڣ�");
                        textBox1.Focus();
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter && textBox1.Text.Length >2)
                {
                    string strEmpID = ((clsControlOPMedStore)ObjFrm.objController).m_mthEmpNo(textBox1.Text.Trim());
                    if (strEmpID != "")
                    {
                        ((clsControlOPMedStore)ObjFrm.objController).SaveEmp[0].empID = strEmpID;
                        ((clsControlOPMedStore)ObjFrm.objController).m_mthClick();
                        textBox1.Text = "";
                        textBox1.Focus();
                        status = "1";
                        if (ObjFrm.statusWindows.statusTone == 1)
                        {
                            this.Text = "�Զ���ҩ�������봦���ţ�";
                        }
                        else
                        {
                            this.Text = "�Զ���ҩ�������봦���ţ�";
                        }
                    }
                    else
                    {
                        textBox1.SelectAll();
                        ((clsControlOPMedStore)ObjFrm.objController).publiClass.m_mthShowWarning(textBox1, "�˹��Ų����ڣ�");
                        textBox1.Focus();
                    }
                }
            }
        }

        private void frmAuto_Load(object sender, EventArgs e)
        {
            if (ObjFrm.statusWindows.statusTone == 1)
            {
                this.Text = "�Զ���ҩ�����봦���ţ�";
            }
            else
            {
                this.Text = "�Զ���ҩ�����봦���ţ�";
            }
        }
        
    }
}