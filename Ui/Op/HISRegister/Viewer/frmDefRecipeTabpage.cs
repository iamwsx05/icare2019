using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmDefRecipeTabpage : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// ҽ������վDomainClass
        /// </summary>
        private clsDcl_DoctorWorkstation objDoct;
        /// <summary>
        /// ��ǰ��
        /// </summary>
        private int CurrentRow = -1;

        public frmDefRecipeTabpage()
        {
            InitializeComponent();

            objDoct = new clsDcl_DoctorWorkstation();
        }

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void m_mthInit()
        {
            DataTable dt;

            long l = this.objDoct.m_lngGetDoctorList(out dt);

            if (l > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] s = new string[4];

                    s[0] = "F";
                    s[1] = dt.Rows[i]["empno_chr"].ToString().Trim();
                    s[2] = dt.Rows[i]["lastname_vchr"].ToString().Trim();
                    s[3] = dt.Rows[i]["deptname"].ToString().Trim();

                    int row = this.dataGridView1.Rows.Add(s);
                    this.dataGridView1.Rows[row].Tag = dt.Rows[i];

                    if (i == 0)
                    {
                        this.m_mthPreviewPurview(dt.Rows[i]["empid_chr"].ToString());
                    }
                }
            }

            ListViewItem lv1 = new ListViewItem("��ҩȨ��");
            lv1.ImageIndex = 0;
            lv1.Tag = "1";
            this.lvTop.Items.Add(lv1);

            ListViewItem lv2 = new ListViewItem("��ҩȨ��");
            lv2.ImageIndex = 0;
            lv2.Tag = "2";
            this.lvTop.Items.Add(lv2);

            ListViewItem lv3 = new ListViewItem("����Ȩ��");
            lv3.ImageIndex = 0;
            lv3.Tag = "3";
            this.lvTop.Items.Add(lv3);

            ListViewItem lv4 = new ListViewItem("���Ȩ��");
            lv4.ImageIndex = 0;
            lv4.Tag = "4";
            this.lvTop.Items.Add(lv4);

            ListViewItem lv5 = new ListViewItem("����Ȩ��");
            lv5.ImageIndex = 0;
            lv5.Tag = "5";
            this.lvTop.Items.Add(lv5);
            
        }
        #endregion               

        #region ���Ȩ��
        /// <summary>
        /// ���Ȩ��
        /// </summary>
        private void m_mthAddPurview()
        {
            List<string> PurviewArr = new List<string>();

            if (this.lvTop.SelectedItems.Count == 0)
            {
                return;
            }
            else
            {
                for (int i = 0; i < this.lvTop.SelectedItems.Count; i++)
                {
                    string text = this.lvTop.SelectedItems[i].Text.ToString();                  
                    ListViewItem item = this.lvBottom.FindItemWithText(text);
                    if (item == null)
                    {
                        PurviewArr.Add(this.lvTop.SelectedItems[i].Tag.ToString());
                    }                      
                }                
            }

            if (PurviewArr.Count == 0)
            {
                return;
            }

            List<clsOutRecipePurview_VO> objArr = new List<clsOutRecipePurview_VO>();            

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[0].Value.ToString().ToUpper() == "T")
                {
                    DataRow dr = this.dataGridView1.Rows[i].Tag as DataRow;
                   
                     clsOutRecipePurview_VO RecipePurview_VO = new  clsOutRecipePurview_VO();
                    RecipePurview_VO.EmpID = dr["empid_chr"].ToString();
                    RecipePurview_VO.PurviewArr = PurviewArr;

                    objArr.Add(RecipePurview_VO);
                }
            }

            if (objArr.Count == 0)
            {
                return;
            }

            long l = this.objDoct.m_lngSaveDoctorRecipePurview(objArr, 1);
            if (l > 0)
            {
                this.m_mthAddItem(PurviewArr);
                //MessageBox.Show("�������ҽ������Ȩ�޳ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("�������ҽ������Ȩ��ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region ɾ��Ȩ��
        /// <summary>
        /// ɾ��Ȩ��
        /// </summary>
        private void m_mthDelPurview()
        {
            List<string> PurviewArr = new List<string>();

            if (this.lvBottom.SelectedItems.Count == 0)
            {
                return;
            }
            else
            {
                for (int i = 0; i < this.lvBottom.SelectedItems.Count; i++)
                {
                    PurviewArr.Add(this.lvBottom.SelectedItems[i].Tag.ToString());                    
                }
            }                                               

            if (CurrentRow == -1)
            {
                return;
            }

            List<clsOutRecipePurview_VO> objArr = new List<clsOutRecipePurview_VO>();

            if (this.dataGridView1.Rows[CurrentRow].Cells[0].Value.ToString().ToUpper() == "T")
            {
                DataRow dr = this.dataGridView1.Rows[CurrentRow].Tag as DataRow;

                if (MessageBox.Show("ȷ��ɾ����" + dr["lastname_vchr"].ToString() + "��ҽ���Ĵ���Ȩ����", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }              

                 clsOutRecipePurview_VO RecipePurview_VO = new  clsOutRecipePurview_VO();
                RecipePurview_VO.EmpID = dr["empid_chr"].ToString();
                RecipePurview_VO.PurviewArr = PurviewArr;

                objArr.Add(RecipePurview_VO);
            }
            else
            {
                return;
            }                      

            long l = this.objDoct.m_lngSaveDoctorRecipePurview(objArr, 2);
            if (l > 0)
            {
                this.m_mthDelItem(PurviewArr);
                //MessageBox.Show("ɾ������ҽ������Ȩ�޳ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("ɾ������ҽ������Ȩ��ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region ���Ȩ��
        /// <summary>
        /// ���Ȩ��
        /// </summary>
        private void m_mthPreviewPurview(string DoctID)
        {
            DataTable dt;

            long l = this.objDoct.m_lngGetDoctorRecipePurview(DoctID, out dt);
            if (l > 0)
            {
                List<string> PurviewArr = new List<string>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PurviewArr.Add(dt.Rows[i]["purview_chr"].ToString());
                }

                this.lvBottom.Items.Clear();

                if (PurviewArr.Count > 0)
                {
                    this.m_mthAddItem(PurviewArr);
                }
            }
        }
        #endregion

        #region ���LV��Ŀ
        /// <summary>
        /// ���LV��Ŀ
        /// </summary>
        /// <param name="objArr"></param>
        private void m_mthAddItem(List<string> objArr)
        {            
            for (int i = 0; i < objArr.Count; i++)
            {
                int index = int.Parse(objArr[i].ToString());                
                string[] s = new string[5] { "��ҩȨ��", "��ҩȨ��", "����Ȩ��", "���Ȩ��", "����Ȩ��" };

                ListViewItem lv = new ListViewItem(s[index - 1]);
                lv.ImageIndex = 0;
                lv.Tag = index.ToString();
                this.lvBottom.Items.Add(lv);                
            }
        }
        #endregion

        #region ɾ��LV��Ŀ
        /// <summary>
        /// ɾ��LV��Ŀ
        /// </summary>
        /// <param name="objArr"></param>
        private void m_mthDelItem(List<string> objArr)
        {
            for (int i = 0; i < objArr.Count; i++)
            {
                int index = int.Parse(objArr[i].ToString());
                string[] s = new string[5] { "��ҩȨ��", "��ҩȨ��", "����Ȩ��", "���Ȩ��", "����Ȩ��" };
                                
                ListViewItem item = this.lvBottom.FindItemWithText(s[index - 1]);
                if (item != null)
                {
                    this.lvBottom.Items.Remove(item);
                }
            }
        }
        #endregion

        private void frmDefRecipeTabpage_Load(object sender, EventArgs e)
        {
            this.m_mthInit();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string Status = "F";

            if (this.checkBox1.Checked)
            {
                Status = "T";
            }

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                this.dataGridView1.Rows[i].Cells[0].Value = Status;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Rows[e.RowIndex].Tag == null)
            {
                return;
            }

            CurrentRow = e.RowIndex;

            DataRow dr = this.dataGridView1.Rows[CurrentRow].Tag as DataRow;

            this.m_mthPreviewPurview(dr["empid_chr"].ToString());
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().ToUpper() == "T")
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = "F";
            }
            else
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = "T";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.m_mthAddPurview();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            this.m_mthDelPurview();
        }

        private void lvTop_DoubleClick(object sender, EventArgs e)
        {
            this.m_mthAddPurview();
        }

        private void lvBottom_DoubleClick(object sender, EventArgs e)
        {
            this.m_mthDelPurview();
        }           
    }
}