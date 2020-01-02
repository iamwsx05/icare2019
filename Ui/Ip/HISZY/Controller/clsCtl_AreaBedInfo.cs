using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsCtl_AreaBedInfo : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// Domain��
        /// </summary>
        private clsDcl_CommonFind objSvc;
        /// <summary>
        /// GUI����
        /// </summary>
        com.digitalwave.iCare.gui.HIS.frmAreaBedInfo m_objViewer;       

        private DataTable dtRecord = new DataTable();
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsCtl_AreaBedInfo()
        {
            objSvc = new clsDcl_CommonFind();
        }
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            this.m_objViewer = (frmAreaBedInfo)frmMDI_Child_Base_in;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="dt"></param>
        public void m_mthLoadArea(DataTable dt)
        {
            //���ڵ�id
            string rootId = "root";
            //���ڵ�Text
            string rootName = "�����б�";

            //�����ڵ�
            TreeNode tnRoot = new TreeNode(rootName);
            tnRoot.Tag = rootId;
            tnRoot.ImageIndex = 0;
            tnRoot.SelectedImageIndex = 0;
            this.m_objViewer.tvArea.Nodes.Add(tnRoot);

            if (dt.Rows.Count == 0)
            {
                return;
            }
            else
            {
                objSvc.m_lngGetBedinfo("%", 0, out dtRecord);
            }

            //��Ĭ�Ͽ���(����)         
            ArrayList objDeptID = new ArrayList();
            clsDepartmentVO[] objEmpDeptArr;
            this.m_objComInfo.m_mthGetDepartmentByUserID(this.m_objViewer.LoginInfo.m_strEmpID, out objEmpDeptArr);
            if (objEmpDeptArr != null)
            {
                for (int i = 0; i < objEmpDeptArr.Length; i++)
                {
                    objDeptID.Add(((clsDepartmentVO)objEmpDeptArr[i]).strDeptID);
                }
            }

            //�ڵ�id
            string tnId = "";
            //�ڵ�Text
            string tnName = "";
            //��һ����
            string FirstDeptID = "";
            //��һ�ڵ�
            TreeNode FirstTn = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string deptid = dt.Rows[i]["deptid_chr"].ToString();

                if (this.m_objViewer.ShowScope == 1)
                {
                    if (objDeptID.IndexOf(deptid) == -1)
                    {
                        continue;
                    }
                }

                tnId = deptid;
                tnName = "[" + dt.Rows[i]["code_vchr"].ToString().Trim() + "] " + dt.Rows[i]["deptname_vchr"].ToString();
                TreeNode tn = new TreeNode(tnName);
                tn.Tag = tnId;
                tn.ImageIndex = 1;
                tn.SelectedImageIndex = 3;
                tnRoot.Nodes.Add(tn);

                if (i == 0)
                {                    
                    FirstDeptID = deptid;
                    FirstTn = tn;
                }
            }

            this.m_objViewer.tvArea.ExpandAll();

            if (FirstDeptID != "")
            {
                this.m_objViewer.tvArea.SelectedNode = FirstTn;
                this.m_mthShowBedInfo(FirstDeptID);
            }
        }
        #endregion

        #region ��ʾѡ�в���������Ϣ
        /// <summary>
        /// ��ʾѡ�в���������Ϣ
        /// </summary>
        /// <param name="Areaid"></param>
        public void m_mthShowBedInfo(string Areaid)
        {
            this.m_objViewer.dgBed.Rows.Clear();

            if (dtRecord.Rows.Count == 0)
            {
                this.m_objViewer.lblInfo.BringToFront();
                this.m_objViewer.lblInfo.Visible = true;                
                return;
            }

            DataView dv = new DataView(dtRecord);
            dv.RowFilter = "areaid_chr = '" + Areaid + "'";

            if (dv.Count == 0)
            {
                this.m_objViewer.CurrRow = -1;
                this.m_objViewer.lblInfo.BringToFront();
                this.m_objViewer.lblInfo.Visible = true;
                return;
            }
            else
            {                
                this.m_objViewer.lblInfo.Visible = false;

                int i = 1;
                foreach (DataRowView drv in dv)
                {
                    string[] s = new string[11];

                    s[0] = i.ToString();
                    s[1] = drv["bedid_chr"].ToString().Trim();
                    s[2] = drv["code_chr"].ToString().Trim();
                    s[3] = drv["inpatientid_chr"].ToString().Trim();
                    if (s[3] != "")
                    {
                        s[4] = drv["inpatientcount_int"].ToString().Trim();
                        s[5] = drv["lastname_vchr"].ToString().Trim();
                        s[6] = drv["sex_chr"].ToString().Trim();
                        s[7] = clsPublic.CalcAge(Convert.ToDateTime(drv["birth_dat"]));
                        s[8] = drv["rysj"].ToString();
                        s[9] = drv["registerid_chr"].ToString();
                        s[10] = drv["patientid_chr"].ToString();
                    }
                    else
                    {
                        s[3] = "*";
                        s[4] = "*";
                        s[5] = "*";
                        s[6] = "*";
                        s[7] = "*";
                        s[8] = "*";
                        s[9] = "*";
                        s[10] = "*";
                    }             
                    
                    int row = this.m_objViewer.dgBed.Rows.Add(s);
                    this.m_objViewer.dgBed.Rows[row].Tag = drv;

                    if (Math.IEEERemainder(Convert.ToDouble(i + 1), 2) == 0)
                    {
                        this.m_objViewer.dgBed.Rows[row].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                    }

                    i++;
                }
                this.m_objViewer.CurrRow = 0;
            }
        }
        #endregion

        #region ����סԺ��
        /// <summary>
        /// ����סԺ��
        /// </summary>
        /// <param name="CurrRow"></param>
        public void m_mthGetZyh(int row)
        {
            if (row < 0)
            {
                return;
            }

            DataRowView drv = this.m_objViewer.dgBed.Rows[row].Tag as DataRowView;

            string zyh = drv["inpatientid_chr"].ToString();
            if (zyh.Trim() == "")
            {
                return;
            }

            this.m_objViewer.Zyh = drv["inpatientid_chr"].ToString(); 
            this.m_objViewer.Zycs = int.Parse(drv["inpatientcount_int"].ToString());
            this.m_objViewer.patname = drv["lastname_vchr"].ToString(); 
            this.m_objViewer.regid = drv["registerid_chr"].ToString(); 
            this.m_objViewer.pid = drv["patientid_chr"].ToString();
            this.m_objViewer.CardNo = drv["patientcardid_chr"].ToString(); 

            this.m_objViewer.DialogResult = DialogResult.OK;
        }
        #endregion
    
    }
}
