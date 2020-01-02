using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using weCare.Core.Entity;
using Microsoft.VisualBasic;
using ControlLibrary;
using com.digitalwave.iCare.BIHOrder;
using Sybase.DataWindow;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ����ֱ�տ�����
    /// </summary>
    public class clsCtl_PatchCharge : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// Domain��
        /// </summary>
        private clsDcl_Charge objSvc;
        /// <summary>
        /// GUI����
        /// </summary>
        com.digitalwave.iCare.gui.HIS.frmPatchCharge m_objViewer;
        /// <summary>
        /// ����Դ
        /// </summary>
        private DataTable dtSource;
        /// <summary>
        /// �շ���Ŀ��Ʊ����
        /// </summary>
        private DataTable dtChargeItemCat;
        /// <summary>
        /// ��ǰ���õ�ID
        /// </summary>
        internal string CurrOrderID = "";
        /// <summary>
        /// ��ǰ״̬ 0 - ��ȷ�� 1 - ����(�ύ)
        /// </summary>
        internal int CurrOrderStatus = 0;
        /// <summary>
        /// ��ǰ����Ա�Ƿ�������ҩ����־ false ���� true ��
        /// </summary>
        internal bool DrugFlag = false;
        /// <summary>
        /// �Ƿ����������ҩ������Ա�ڲ�����ʱ¼�������� flag ��ֹ true ����
        /// </summary>
        internal bool PatchFlag = false;
        /// <summary>
        /// �����б�
        /// </summary>
        private DataTable dtDeptArea;
        /// <summary>
        /// ¼��ģʽ 0 �շ���Ŀ 1 ������Ŀ
        /// </summary>
        internal int ItemInputMode = 0;
        /// <summary>
        /// �к�׺
        /// </summary>
        private string colSuffix = "";
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        private bool IsAllowDiscount = false;
        /// <summary>
        /// ���������Ŀ����
        /// </summary>
        private int DiscountItemNus = 8;
        /// <summary>
        /// ���۱���
        /// </summary>
        private decimal DiscountScale = 80;
        /// <summary>
        /// ��������Ӧ��Ʊ����(����)
        /// </summary>
        private ArrayList DiscountInvoCatArr = new ArrayList();
        /// <summary>
        /// �Ƿ��޸ı�־
        /// </summary>
        internal bool IsModify = false;
        /// <summary>
        /// Ƿ���Ƿ���������
        /// </summary>
        private bool IsAllowPatch = false;
        /// <summary>
        /// ����ԱĬ�Ͽ���(����)�б�
        /// </summary>
        private ArrayList objEmpDefaultDeptArr = new ArrayList();
        /// <summary>
        /// ����������Ŀ��Ӧ�����շ���Ŀ��һ�Զࣩ�Ƿ��ҩ false ����ҩ true ��ҩ
        /// </summary>
        private bool IsControlPutMed = false;
        /// <summary>
        /// ��ҩ��־
        /// </summary>
        private bool IsPutMedicineFlag = false;
        /// <summary>
        ///  �Ƿ������ڲ����ʴ����˷�
        /// </summary>
        private bool IsAllowPatchNegative = false;
        /// <summary>
        /// �Ƿ�����帺��(ҩƷ��)
        /// </summary>
        private bool IsAllowPatchNegativeMed = false;
        /// <summary>
        /// �Ƿ�����帺��(��ҩƷ��)
        /// </summary>
        private bool IsAllowPatchNegativeNoMed = false;
        /// <summary>
        /// �Ƿ�������ʾȱҩ��Ŀ
        /// </summary>
        private bool IsAllowNoQty = false;
        /// <summary>
        /// �Ƿ�������
        /// </summary>
        private static int intDiffCostOn = clsPublic.m_intGetSysParm("9002");
        /// <summary>
        /// �Ƿ�������
        /// </summary>
        private static string strDiffCostOn = clsPublic.m_strGetSysparm("9003");
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsCtl_PatchCharge()
        {
            objSvc = new clsDcl_Charge();
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
            m_objViewer = (frmPatchCharge)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void m_mthInit()
        {
            this.m_objViewer.ucPatientInfo.Status = 9;

            this.m_mthGetCat();

            this.m_mthGetDeptList();

            this.m_mthLoadHistory();

            this.m_objViewer.panelItem.Height = 0;

            //��ʼ������ҽ��
            //ҽ���б�
            clsColumns_VO[] columArr = new clsColumns_VO[]{ new clsColumns_VO("����","empno_chr",HorizontalAlignment.Left,50),
                                                            new clsColumns_VO("ƴ����","pycode_chr",HorizontalAlignment.Left,60),
                                                            new clsColumns_VO("����","doctorname",HorizontalAlignment.Left,80),
                                                            new clsColumns_VO("","empid_chr",HorizontalAlignment.Left,0)
                                                          };

            string SqlSource = @"select empid_chr, empno_chr, pycode_chr, lastname_vchr as doctorname
                                   from t_bse_employee 
                                  where status_int = 1                                     
                               order by empno_chr";
            //and hasprescriptionright_chr = '1' ���д���Ȩ��

            this.m_objViewer.txtChargeDoctor.m_strSQL = SqlSource;
            this.m_objViewer.txtChargeDoctor.m_mthInitListView(columArr);
            this.m_objViewer.txtChargeDoctor.m_listView.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            //��ʼ��ִ�еص�
            //�ص��б�
            columArr = new clsColumns_VO[]{ new clsColumns_VO("���","code_vchr",HorizontalAlignment.Left,50),
                                            new clsColumns_VO("ƴ����","pycode_chr",HorizontalAlignment.Left,60),
                                            new clsColumns_VO("�ص�����","deptname_vchr",HorizontalAlignment.Left,130),
                                            new clsColumns_VO("","deptid_chr",HorizontalAlignment.Left,0)
                                          };

            SqlSource = @"select deptid_chr, deptname_vchr, pycode_chr, code_vchr
                            from t_bse_deptdesc
                           where status_int = 1
                        order by code_vchr";

            this.m_objViewer.txtApplyArea.m_strSQL = SqlSource;
            this.m_objViewer.txtApplyArea.m_mthInitListView(columArr);
            this.m_objViewer.txtApplyArea.m_listView.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            #region �жϵ�ǰ����Ա�Ƿ�������ҩ��

            //����ҩ��(����)ID
            string DrugStoreID = clsPublic.m_strGetSysparm("0009");

            //����Ա��������ID����           
            clsDepartmentVO[] objArr;
            this.m_objComInfo.m_mthGetDepartmentByUserID(this.m_objViewer.LoginInfo.m_strEmpID, out objArr);
            if (objArr != null)
            {
                for (int i = 0; i < objArr.Length; i++)
                {
                    if (DrugStoreID == objArr[i].strDeptID)
                    {
                        DrugFlag = true;
                        break;
                    }
                }
            }
            #endregion

            //�������Ƿ���ʾȱҩ��Ŀ  flag ��ֹ true ����
            if (clsPublic.m_intGetSysParm("1052") == 1)
            {
                IsAllowNoQty = true;
            }

            //�Ƿ����������ҩ������Ա�ڲ�����ʱ¼�������� flag ��ֹ true ����
            if (clsPublic.m_intGetSysParm("1042") == 1)
            {
                PatchFlag = true;
            }

            //סԺǷ�Ѳ����Ƿ��������� 0 ��ֹ 1 ����
            if (clsPublic.m_intGetSysParm("1048") == 1)
            {
                IsAllowPatch = true;
            }

            //����������Ŀ��Ӧ�����շ���Ŀ��һ�Զࣩ�Ƿ��ҩ false ����ҩ 1 ��ҩ
            if (clsPublic.m_intGetSysParm("1049") == 1)
            {
                IsControlPutMed = true;
            }

            //¼��ģʽ 0 �շ���Ŀ 1 ������Ŀ
            ItemInputMode = clsPublic.m_intGetSysParm("9001");
            if (ItemInputMode == 0)
            {
                this.m_objViewer.plOrder.Width = 0;
                this.m_objViewer.dtgOrderItem.Visible = false;
                this.m_objViewer.dtgItem.BringToFront();
            }
            else if (ItemInputMode == 1)
            {
                this.m_objViewer.plOrder.Width = 197;
                this.m_objViewer.txtExecArea.Enabled = false;
                this.m_objViewer.plExecArea.Visible = false;
                this.m_objViewer.dtgItem.Visible = false;
                this.m_objViewer.dtgOrderItem.BringToFront();

                //�����Ժ���ɣ���ʱע�͡�
                //DiscountItemNus = clsPublic.m_intGetSysParm("4006");
                //DiscountScale = clsPublic.m_intGetSysParm("4007");
                //if (clsPublic.m_intGetSysParm("4008") == 1)
                //{
                //    IsAllowDiscount = true;
                //}
                //DiscountInvoCatArr = clsPublic.m_ArrGettoken(clsPublic.m_strGetSysparm("0013"), ";");
            }

            //��ȡ����ԱĬ�Ͽ����б�
            clsDepartmentVO[] objEmpDeptArr;
            this.m_objComInfo.m_mthGetDepartmentByUserID(this.m_objViewer.LoginInfo.m_strEmpID, out objEmpDeptArr);
            if (objEmpDeptArr != null)
            {
                for (int i = 0; i < objEmpDeptArr.Length; i++)
                {
                    objEmpDefaultDeptArr.Add(objEmpDeptArr[i].strDeptID);
                }

                this.m_objViewer.ucPatientInfo.DeptArr = objEmpDeptArr;

                this.m_objViewer.ucPatientInfo.Parm_AreaID = this.m_objViewer.LoginInfo.m_strInpatientAreaID;
                this.m_objViewer.ucPatientInfo.Parm_AreaName = this.m_objViewer.LoginInfo.m_strInpatientAreaName;
            }

            //��ȡ�帺��Ȩ��
            this.m_mthGetNegativePurview();

            //���ÿ����Ϣ
            clsPublic.SetShortCutInfo(this.m_objViewer.MdiParent, 4, " F8�� �� ֱ�Ӳ�ѯ����");

            this.m_objViewer.btnNew_Click(null, null);
        }
        #endregion

        #region ��ȡ�帺��Ȩ��
        /// <summary>
        /// ��ȡ�帺��Ȩ��
        /// </summary>
        public void m_mthGetNegativePurview()
        {
            //�Ƿ������ڲ����ʴ����˷�  
            if (clsPublic.m_intGetSysParm("1055") == 1)
            {
                IsAllowPatchNegative = true;
            }

            string EmpID = this.m_objViewer.LoginInfo.m_strEmpID;

            //��ҩƷ��ɫ
            System.Collections.Generic.List<string> RoleMedArr = clsPublic.m_ArrGettoken(clsPublic.m_strGetSysparm("0017"), ";");
            //���ҩƷ��ɫ
            System.Collections.Generic.List<string> RoleNoMedArr = clsPublic.m_ArrGettoken(clsPublic.m_strGetSysparm("0018"), ";");

            DataTable dt;
            long l = this.objSvc.m_lngGetEmpRole(EmpID, out dt);
            if (l > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string RoleID = dt.Rows[i]["roleid_chr"].ToString();

                    if (RoleMedArr.IndexOf(RoleID) >= 0)
                    {
                        IsAllowPatchNegativeMed = true;
                    }

                    if (RoleNoMedArr.IndexOf(RoleID) >= 0)
                    {
                        IsAllowPatchNegativeNoMed = true;
                    }
                }
            }
        }
        #endregion

        #region �Ƿ��ͯ.������ö�ͯ�۸�
        /// <summary>
        /// �Ƿ��ͯ.������ö�ͯ�۸�
        /// </summary>
        internal bool IsChildPrice
        {
            get
            {
                if (this.m_objViewer.ucPatientInfo.BihPatient_VO == null)
                {
                    return false;
                }
                else
                {
                    if (new clsDcl_YB().IsUseChildPrice())
                        return new clsBrithdayToAge().IsChild(this.m_objViewer.ucPatientInfo.BihPatient_VO.m_dtmBirthDay);
                    else
                        return false;
                }
            }
        }
        #endregion

        #region �帺���
        /// <summary>
        /// �帺���
        /// </summary>
        /// <param name="Dr"></param>
        /// <param name="Amount"></param> 
        /// <param name="ItemType">0 �շ���Ŀ 1 ������Ŀ</param> 
        /// <returns></returns>
        public bool m_blnCheckPatchNegative(DataRow Dr, decimal Amount, int ItemType)
        {
            bool ret = false;
            decimal AmountSource = 0;
            decimal Price = 0;
            string ItemID = "";

            DataTable dtOrg;
            long l = this.objSvc.m_lngGetFeeItemByActiveType(this.m_objViewer.ucPatientInfo.RegisterID, 999, null, "00", this.m_objViewer.ucPatientInfo.BihPatient_VO.InHospitalDate.Substring(0, 10), DateTime.Now.ToString("yyyy-MM-dd"), out dtOrg);
            if (l > 0)
            {
                DataView dv = new DataView(dtOrg);
                dv.RowFilter = "pstatus_int = 1 or pstatus_int = 2";

                if (ItemType == 0)
                {
                    ItemID = Dr["itemid_chr"].ToString();

                    if (Dr["ipchargeflg_int"].ToString().Trim() == "1")
                    {
                        Price = clsPublic.ConvertObjToDecimal(Dr["submoney"]);
                    }
                    else
                    {
                        Price = clsPublic.ConvertObjToDecimal(Dr["itemprice_mny"]);
                    }

                    for (int i = 0; i < dv.Count; i++)
                    {
                        DataRow dr = dv[i].Row;

                        if (dr["chargeitemid_chr"].ToString() == ItemID && clsPublic.ConvertObjToDecimal(dr["unitprice_dec"]) == Price)
                        {
                            AmountSource += clsPublic.ConvertObjToDecimal(dr["amount_dec"]);
                        }
                    }

                    if (AmountSource >= Math.Abs(Amount))
                    {
                        ret = true;
                    }
                }
                else if (ItemType == 1)
                {
                    string OrderID = Dr["orderdicid_chr"].ToString();

                    DataTable dt;
                    l = this.objSvc.m_lngGetChargeItemByOrderID(OrderID, this.m_objViewer.ucPatientInfo.BihPatient_VO.PayTypeID, out dt, this.IsChildPrice);
                    if (l > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr2 = dt.Rows[i];

                            ItemID = dr2["itemid_chr"].ToString();

                            if (dr2["ipchargeflg_int"].ToString().Trim() == "1")
                            {
                                Price = clsPublic.ConvertObjToDecimal(dr2["submoney"]);
                            }
                            else
                            {
                                Price = clsPublic.ConvertObjToDecimal(dr2["itemprice_mny"]);
                            }
                            decimal AmountOrder = Amount * clsPublic.ConvertObjToDecimal(dr2["totalqty_dec"]);

                            AmountSource = 0;
                            for (int j = 0; j < dv.Count; j++)
                            {
                                DataRow dr3 = dv[j].Row;

                                if (dr3["chargeitemid_chr"].ToString() == ItemID && clsPublic.ConvertObjToDecimal(dr3["unitprice_dec"]) == Price)
                                {
                                    AmountSource += clsPublic.ConvertObjToDecimal(dr3["amount_dec"]);
                                }
                            }

                            if (AmountSource < Math.Abs(AmountOrder))
                            {
                                return false;
                            }

                            if (i == (dt.Rows.Count - 1))
                            {
                                ret = true;
                            }
                        }
                    }
                }
            }

            return ret;
        }
        #endregion

        #region ���ݿ��Ҳ���ID��ȡ����
        /// <summary>
        /// ���ݿ��Ҳ���ID��ȡ����
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string m_strGetDeptName(string ID)
        {
            string DeptName = "";

            for (int i = 0; i < dtDeptArea.Rows.Count; i++)
            {
                if (dtDeptArea.Rows[i]["deptid_chr"].ToString().Trim() == ID.Trim())
                {
                    DeptName = dtDeptArea.Rows[i]["deptname_vchr"].ToString();
                    break;
                }
            }

            return DeptName;
        }
        #endregion

        #region ��̬����ִ�еص�
        /// <summary>
        /// ��̬����ִ�еص�
        /// </summary>
        /// <param name="Flag">1 ȫ������ 2 ��Ŀִ�з�������Ӧ�Ŀ����б�</param>
        /// <param name="ItemID"></param>
        public void m_mthLoadExecArea(int Flag, string ItemID)
        {
            string SqlSource = "";

            clsColumns_VO[] columArr = new clsColumns_VO[]{ new clsColumns_VO("���","code_vchr",HorizontalAlignment.Left,50),
                                                             new clsColumns_VO("ƴ����","pycode_chr",HorizontalAlignment.Left,60),
                                                             new clsColumns_VO("�ص�����","deptname_vchr",HorizontalAlignment.Left,130),
                                                             new clsColumns_VO("","deptid_chr",HorizontalAlignment.Left,0)
                                                           };

            if (Flag == 1)
            {
                SqlSource = @"select deptid_chr, deptname_vchr, pycode_chr, code_vchr
                                from t_bse_deptdesc
                               where status_int = 1
                            order by code_vchr";
            }
            else if (Flag == 2)
            {
                SqlSource = @"select a.deptid_chr, a.deptname_vchr, a.pycode_chr, a.code_vchr
                                from t_bse_deptdesc a,
                                     t_aid_bih_ocdeptlist b,
                                     t_bse_chargeitem c
                               where a.status_int = 1 
                                 and a.deptid_chr = b.clacarea_chr 
                                 and b.ordercateid_chr = c.ordercateid_chr
                                 and c.itemid_chr = '" + ItemID + @"' 
                            order by a.code_vchr";
            }

            this.m_objViewer.txtExecArea.m_strSQL = SqlSource;
            this.m_objViewer.txtExecArea.m_mthInitListView(columArr);
            this.m_objViewer.txtExecArea.m_listView.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }
        #endregion

        #region ��ձ༭��
        /// <summary>
        /// ��ձ༭��
        /// </summary>
        public void m_mthClear()
        {
            this.m_objViewer.txtItemName.Tag = null;
            this.m_objViewer.txtItemName.Text = "";
            this.m_objViewer.lblStandard.Text = "";
            this.m_objViewer.lblUnit.Text = "";
            this.m_objViewer.lblPrice.Text = "";
            this.m_objViewer.txtAmount.Text = "";
            this.m_objViewer.lblTotal.Text = "";
            this.m_objViewer.txtExecArea.Value = null;
            this.m_objViewer.txtExecArea.Text = "";

            this.m_objViewer.txtItemName.Focus();
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthLoadHistory()
        {
            string RegId = this.m_objViewer.ucPatientInfo.RegisterID;

            this.m_objViewer.tvHistory.Nodes.Clear();

            //���ڵ�id
            string rootId = "root";
            //���ڵ�Text
            string rootName = "��ʷ��¼";

            //�����ڵ�
            TreeNode tnRoot = new TreeNode(rootName);
            tnRoot.Tag = rootId;
            tnRoot.ImageIndex = 0;
            tnRoot.SelectedImageIndex = 0;
            this.m_objViewer.tvHistory.Nodes.Add(tnRoot);

            if (RegId == "")
            {
                return;
            }

            long l = this.objSvc.m_lngGetFeeItemByActiveType(RegId, 2, null, null, null, null, out dtSource);
            if (l > 0 && dtSource.Rows.Count > 0)
            {
                Hashtable has = new Hashtable();
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    string OrderID = dtSource.Rows[i]["orderid_chr"].ToString();
                    string Status = dtSource.Rows[i]["pstatus_int"].ToString();

                    //��������ֻ�ܿ�������������¼
                    if (this.m_objViewer.Scope == "1" && objEmpDefaultDeptArr.IndexOf(dtSource.Rows[i]["createarea_chr"].ToString().Trim()) == -1)
                    {
                        continue;
                    }
                    else if (OrderID.Trim() == "")
                    {
                        continue;
                    }

                    if (!has.ContainsKey(OrderID))
                    {
                        has.Add(OrderID, OrderID + ";" + Status);
                    }
                }

                //�ڵ�id
                string tnId = "";
                //�ڵ�Text
                string tnName = "";

                ArrayList OrderArr = new ArrayList();
                OrderArr.AddRange(has.Values);
                OrderArr.Sort();

                for (int i = 0; i < OrderArr.Count; i++)
                {
                    string s = OrderArr[i].ToString();
                    string OrderId = clsPublic.m_strGettoken(ref s, ";");
                    string Status = clsPublic.m_strGettoken(ref s, ";");
                    Color clr = Color.Black;

                    tnId = OrderId;
                    string tmpName = "[" + Convert.ToString(i + 1) + "-" + OrderId.Substring(0, 8) + "." + OrderId.Substring(8, 2) + "]  ";
                    if (Status == "0")
                    {
                        tnName = tmpName + "����";
                    }
                    else if (Status == "1")
                    {
                        tnName = tmpName + "�ύ";
                        clr = Color.Blue;
                    }
                    else if (Status == "2")
                    {
                        tnName = tmpName + "����";
                        clr = Color.Blue;
                    }
                    else if (Status == "3")
                    {
                        tnName = tmpName + "����";
                        clr = Color.Red;
                    }

                    TreeNode tn = new TreeNode(tnName);
                    tn.Tag = OrderId + ";" + Status;
                    tn.ImageIndex = 1;
                    tn.SelectedImageIndex = 1;
                    tn.ForeColor = clr;

                    tnRoot.Nodes.Add(tn);
                }

                this.m_objViewer.tvHistory.ExpandAll();

            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthFind()
        {
            frmCommonFind f = new frmCommonFind("������Ժ��������", this.m_objViewer.ucPatientInfo.Status);
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.m_objViewer.ucPatientInfo.m_mthFind(f.RegisterID, 3);
                if (this.m_objViewer.ucPatientInfo.IsChanged)
                {
                    this.m_mthLoad();
                }
            }
        }
        #endregion

        #region װ��
        /// <summary>
        /// װ��
        /// </summary>
        public void m_mthLoad()
        {
            bool bl = true;

            //סԺ-�Ƿ�����δ���Ŵ�λ�Ĳ��˲����� 0 ��ֹ 1 ����
            if (clsPublic.m_intGetSysParm("1041") == 0 && !(this.m_objViewer.ucPatientInfo.BihPatient_VO.Status == 1 || this.m_objViewer.ucPatientInfo.BihPatient_VO.Status == 2 || this.m_objViewer.ucPatientInfo.BihPatient_VO.Status == 4))
            {
                bl = false;
                MessageBox.Show("��ǰϵͳ����Ϊ����ֹδ���Ŵ�λ(��Ԥ��Ժ)�Ĳ��˲����ʡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //סԺǷ�Ѳ����Ƿ��������� 0 ��ֹ 1 ����
                if (!IsAllowPatch && this.m_objViewer.ucPatientInfo.BihPatient_VO.BalanceMoney < 0 && this.m_objViewer.ucPatientInfo.BihPatient_VO.SpecChargeCtrl == 0)
                {
                    bl = false;
                    MessageBox.Show("��ǰϵͳ����Ϊ����ֹǷ�ѵĲ��˲�����(��ע�������)��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (bl)
            {
                this.m_mthLoadHistory();
                this.m_objViewer.dtgItem.Rows.Clear();
                this.m_objViewer.dtgOrder.Rows.Clear();
                this.m_objViewer.dtgOrderItem.Rows.Clear();
                this.m_objViewer.lblAllTotal.Text = "";

                if (this.m_objViewer.Scope == "1")
                {
                    this.m_objViewer.txtApplyArea.m_mthFindAndSelect(this.m_objViewer.LoginInfo.m_strInpatientAreaID);
                    this.m_objViewer.txtChargeDoctor.m_mthFindAndSelect(this.m_objViewer.LoginInfo.m_strEmpID);
                }
                else if (this.m_objViewer.Scope == "2")
                {
                    this.m_objViewer.txtApplyArea.m_mthFindAndSelect(this.m_objViewer.ucPatientInfo.BihPatient_VO.AreaID);
                    this.m_objViewer.txtChargeDoctor.m_mthFindAndSelect(this.m_objViewer.ucPatientInfo.BihPatient_VO.DoctorID);
                }
            }

            this.m_objViewer.btnNew.Enabled = bl;
            this.m_objViewer.btnAdd.Enabled = bl;
            this.m_objViewer.btnDel.Enabled = bl;
            this.m_objViewer.btnClear.Enabled = bl;
            this.m_objViewer.btnSave.Enabled = bl;
            this.m_objViewer.btnCommit.Enabled = bl;
            this.m_objViewer.tableLayoutPanel1.Enabled = bl;

            this.CurrOrderID = "";
            this.m_mthClear();
        }
        #endregion

        #region �����շ���Ŀ
        /// <summary>
        /// �����շ���Ŀ
        /// </summary>
        /// <param name="FindStr"></param>
        public void m_mthFindChargeItem(string FindStr)
        {
            if (FindStr.Trim() == "")
            {
                return;
            }

            string PatType = this.m_objViewer.ucPatientInfo.BihPatient_VO.PayTypeID;
            if (PatType == "")
            {
                MessageBox.Show("����ȷ�ѱ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FindStr.Trim().Substring(0, 1) == @"\" && ItemInputMode == 1)
            {
                frmBIHOrderGroupInput fGroup = new frmBIHOrderGroupInput(FindStr.Substring(1), this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strInpatientAreaID, 4, 1);
                if (fGroup.ShowDialog() == DialogResult.OK)
                {
                    this.m_mthAddOrderItem(fGroup.m_arrOrderDic, fGroup.m_decMount);
                    fGroup = null;
                }

                //if (FindStr.Trim().Length == 1 && (FindStr.Trim() == "?" || FindStr.Trim() == @"\"))
                //{
                //    if (this.m_objViewer.txtApplyArea.Value == null || this.m_objViewer.txtApplyArea.Text.Trim() == "")
                //    {
                //        MessageBox.Show("��ѡ�񿪵��ص㡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        this.m_objViewer.txtApplyArea.Focus();
                //        return;
                //    }

                //    if (this.m_objViewer.txtChargeDoctor.Value == null || this.m_objViewer.txtChargeDoctor.Text.Trim() == "")
                //    {
                //        MessageBox.Show("��ѡ�񿪵�ҽ����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        this.m_objViewer.txtChargeDoctor.Focus();
                //        return;
                //    }

                //    if (FindStr.Trim() == "?")
                //    {
                //        frmUsageAddItem fItem = new frmUsageAddItem(this.m_objViewer.ucPatientInfo.BihPatient_VO.PayTypeID, this.m_objViewer.txtApplyArea.Value.ToString());
                //        if (fItem.ShowDialog() == DialogResult.OK)
                //        {
                //            this.m_mthAddItem(fItem.DrArr, fItem.Nums);
                //        }
                //    }
                //    else if (FindStr.Trim() == @"\")
                //    {
                //        frmItemGroup fItem = new frmItemGroup(this.m_objViewer.ucPatientInfo.BihPatient_VO.PayTypeID, this.m_objViewer.txtApplyArea.Value.ToString());
                //        if (fItem.ShowDialog() == DialogResult.OK)
                //        {
                //            this.m_mthAddItem(fItem.DrArr, fItem.Nums);
                //        }
                //    }
            }
            else
            {
                this.m_objViewer.lsvItem.BeginUpdate();
                this.m_objViewer.lsvItem.Items.Clear();

                DataTable dt;

                #region �շ���Ŀ
                if (ItemInputMode == 0)
                {
                    long l = this.objSvc.m_lngFindChargeItem(FindStr, PatType, out dt, this.IsChildPrice);
                    if (l > 0 && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string invocat = m_strConvertToChType(dt.Rows[i]["itemipinvtype_chr"].ToString().Trim());   //��Ʊ���� flag_int = 4
                            ListViewItem lv = new ListViewItem(FindStr);
                            lv.SubItems.Add(dt.Rows[i]["itemcode_vchr"].ToString().Trim());
                            lv.SubItems.Add(dt.Rows[i]["itemname_vchr"].ToString().Trim());
                            lv.SubItems.Add(dt.Rows[i]["itemcommname_vchr"].ToString().Trim());
                            lv.SubItems.Add(invocat);
                            lv.SubItems.Add(dt.Rows[i]["itemspec_vchr"].ToString().Trim());
                            //������õ�����С��λ,����С���ۺ���С��λ                      
                            if (dt.Rows[i]["ipchargeflg_int"].ToString().Trim() == "1")
                            {
                                lv.SubItems.Add(dt.Rows[i]["itemipunit_chr"].ToString().Trim());
                                lv.SubItems.Add(dt.Rows[i]["submoney"].ToString().Trim());
                            }
                            else
                            {
                                lv.SubItems.Add(dt.Rows[i]["itemunit_chr"].ToString().Trim());
                                lv.SubItems.Add(dt.Rows[i]["itemprice_mny"].ToString().Trim());
                            }

                            string PRECENT_DEC = "100";
                            if (dt.Rows[i]["precent_dec"].ToString().Trim() != "")
                            {
                                PRECENT_DEC = dt.Rows[i]["precent_dec"].ToString().Trim();
                            }
                            lv.SubItems.Add(PRECENT_DEC + "%"); //�շѱ���  
                            lv.SubItems.Add(dt.Rows[i]["ybtypename"].ToString().Trim());

                            if (invocat.IndexOf("��") >= 0 || invocat.IndexOf("��") >= 0)
                            {
                                if (dt.Rows[i]["ipnoqtyflag_int"].ToString().Trim() != "0")
                                {
                                    if (!IsAllowNoQty)
                                    {
                                        continue;
                                    }
                                    lv.SubItems.Add("ȱҩ");
                                    lv.ForeColor = Color.Red;
                                }
                                else
                                {
                                    lv.SubItems.Add("");
                                }
                            }
                            else
                            {
                                lv.SubItems.Add("");
                            }

                            lv.Tag = dt.Rows[i];
                            this.m_objViewer.lsvItem.Items.Add(lv);
                        }
                    }
                    else
                    {
                        MessageBox.Show("û�ҵ������������շ���Ŀ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                #endregion

                #region ������Ŀ
                if (ItemInputMode == 1)
                {
                    long l = this.objSvc.m_lngFindOrderByID(FindStr, out dt, this.IsChildPrice);
                    string strFilter = clsPublic.m_strGetSysparm("1009");
                    strFilter = clsPublic.m_strGettoken(ref strFilter, "*");
                    strFilter = "Isnull(DRUGSTOREID_CHR,'Null Column') = 'Null Column' or DRUGSTOREID_CHR in ('" + strFilter + "')";
                    dt.DefaultView.RowFilter = strFilter;
                    dt = dt.DefaultView.ToTable();
                    if (l > 0 && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string invocat = m_strConvertToChType(dt.Rows[i]["itemipinvtype_chr"].ToString().Trim());   //��Ʊ���� flag_int = 4
                            ListViewItem lv = new ListViewItem(FindStr);
                            lv.SubItems.Add(dt.Rows[i]["usercode_chr"].ToString().Trim());
                            lv.SubItems.Add(dt.Rows[i]["name_chr"].ToString().Trim());
                            lv.SubItems.Add(dt.Rows[i]["commname_vchr"].ToString().Trim());
                            lv.SubItems.Add(invocat);
                            lv.SubItems.Add(dt.Rows[i]["itemspec_vchr"].ToString().Trim());
                            lv.SubItems.Add(dt.Rows[i]["itemunit"].ToString().Trim());
                            lv.SubItems.Add(dt.Rows[i]["totalmny"].ToString().Trim());
                            lv.SubItems.Add(""); //�շѱ���  
                            lv.SubItems.Add(dt.Rows[i]["ybtypename"].ToString().Trim());

                            if (invocat.IndexOf("��") >= 0 || invocat.IndexOf("��") >= 0)
                            {
                                if (dt.Rows[i]["noqtyflag_int"].ToString().Trim() == "1")
                                {
                                    if (!IsAllowNoQty)
                                    {
                                        continue;
                                    }
                                    lv.SubItems.Add("ȱҩ");
                                    lv.ForeColor = Color.Red;
                                }
                                else
                                {
                                    lv.SubItems.Add("");
                                }
                            }
                            else
                            {
                                lv.SubItems.Add("");
                            }

                            lv.Tag = dt.Rows[i];
                            this.m_objViewer.lsvItem.Items.Add(lv);
                        }
                    }
                    else
                    {
                        MessageBox.Show("û�ҵ������������շ���Ŀ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                #endregion

                if (this.m_objViewer.lsvItem.Items.Count > 0)
                {
                    this.m_objViewer.panelItem.Height = 200;
                    this.m_objViewer.lsvItem.Items[0].Selected = true;
                    this.m_objViewer.lsvItem.Focus();
                }

                this.m_objViewer.lsvItem.EndUpdate();
            }
        }
        #endregion

        #region ��ȡ�շ���Ŀ��Ʊ����
        /// <summary>
        /// ��ȡ�շ���Ŀ��Ʊ����
        /// </summary>
        private void m_mthGetCat()
        {
            long l = this.objSvc.m_lngGetChargeItemCat(4, out dtChargeItemCat);
        }
        #endregion

        #region ��ȡ�����б�
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        private void m_mthGetDeptList()
        {
            long l = this.objSvc.m_lngGetDeptArea(out dtDeptArea, 1);
        }
        #endregion

        #region ����IDת�����������
        /// <summary>
        /// ����IDת�����������
        /// </summary>
        /// <param name="TypeNo"></param>
        /// <returns></returns>
        private string m_strConvertToChType(string TypeNo)
        {
            string Ret = "";

            for (int i = 0; i < dtChargeItemCat.Rows.Count; i++)
            {
                if (TypeNo == dtChargeItemCat.Rows[i]["typeid_chr"].ToString())
                {
                    Ret = dtChargeItemCat.Rows[i]["typename_vchr"].ToString();
                    break;
                }
            }

            return Ret;
        }
        #endregion

        #region ѡ���շ���Ŀ
        /// <summary>
        /// ѡ���շ���Ŀ
        /// </summary>        
        public void m_mthSelectItem()
        {
            if (this.m_objViewer.lsvItem.Items.Count == 0 || this.m_objViewer.lsvItem.SelectedItems.Count == 0)
            {
                return;
            }

            DataRow dr = this.m_objViewer.lsvItem.SelectedItems[0].Tag as DataRow;

            #region �շ���Ŀ
            if (ItemInputMode == 0)
            {
                string ItemName = dr["itemname_vchr"].ToString().Trim();
                string ItemSpe = dr["itemspec_vchr"].ToString().Trim();
                string ItemUnit, ItemPrice;
                if (dr["ipchargeflg_int"].ToString().Trim() == "1")
                {
                    ItemUnit = dr["itemipunit_chr"].ToString().Trim();
                    ItemPrice = dr["submoney"].ToString().Trim();
                }
                else
                {
                    ItemUnit = dr["itemunit_chr"].ToString().Trim();
                    ItemPrice = dr["itemprice_mny"].ToString().Trim();
                }

                string Precent = "100";
                if (dr["precent_dec"].ToString().Trim() != "")
                {
                    Precent = dr["precent_dec"].ToString().Trim();
                }

                //�������Ŀ
                this.m_objViewer.txtItemName.Text = ItemName;
                this.m_objViewer.lblStandard.Text = ItemSpe;
                this.m_objViewer.lblUnit.Text = ItemUnit;
                this.m_objViewer.lblPrice.Text = ItemPrice;

                //���Ĭ��ִ�еص�
                string ApplyAreaID = "";
                if (this.m_objViewer.txtApplyArea.Value != null)
                {
                    ApplyAreaID = this.m_objViewer.txtApplyArea.Value.ToString().Trim();
                }

                string ItemId = dr["itemid_chr"].ToString();
                string ExecAreaName = "";
                string ExecAreaID = this.objSvc.m_strGetChargeItemDefaultExecAreaID(ItemId, ApplyAreaID, out ExecAreaName);
                if (ExecAreaID == "")
                {
                    this.m_mthLoadExecArea(1, null);
                }
                else
                {
                    this.m_mthLoadExecArea(2, ItemId);
                }

                this.m_objViewer.txtExecArea.m_mthFindAndSelect(ExecAreaID);
            }
            #endregion

            #region ������Ŀ
            if (ItemInputMode == 1)
            {
                //�������Ŀ
                this.m_objViewer.txtItemName.Text = dr["name_chr"].ToString().Trim();
                this.m_objViewer.lblStandard.Text = dr["itemspec_vchr"].ToString().Trim();
                this.m_objViewer.lblUnit.Text = dr["itemunit"].ToString().Trim();
                this.m_objViewer.lblPrice.Text = dr["totalmny"].ToString().Trim();
            }
            #endregion

            this.m_objViewer.txtItemName.Tag = dr;

            this.m_objViewer.txtAmount.Focus();
        }
        #endregion

        #region �����Ŀ
        /// <summary>
        /// �����Ŀ
        /// </summary>
        public void m_mthAddItem()
        {
            if (this.m_objViewer.txtItemName.Tag == null)
            {
                return;
            }

            if (this.m_objViewer.txtAmount.Text.Trim() == "-")
            {
                this.m_objViewer.txtAmount.Text = "";
            }

            if (this.m_objViewer.txtAmount.Text.Trim() == "" || this.m_objViewer.txtAmount.Text.Trim() == "0")
            {
                MessageBox.Show("������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.txtAmount.Focus();
                return;
            }

            if (!Microsoft.VisualBasic.Information.IsNumeric(this.m_objViewer.txtAmount.Text.Trim()))
            {
                MessageBox.Show("��������������������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.txtAmount.SelectAll();
                this.m_objViewer.txtAmount.Focus();
                return;
            }

            if (this.m_objViewer.lblTotal.Text.Trim() == "")
            {
                MessageBox.Show("����Ϊ�ա�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.m_objViewer.txtApplyArea.Value == null || this.m_objViewer.txtApplyArea.Text.Trim() == "")
            {
                MessageBox.Show("��ѡ�񿪵��ص㡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.txtApplyArea.Focus();
                return;
            }

            if (this.m_objViewer.txtChargeDoctor.Value == null || this.m_objViewer.txtChargeDoctor.Text.Trim() == "")
            {
                MessageBox.Show("��ѡ�񿪵�ҽ����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.txtChargeDoctor.Focus();
                return;
            }

            if (ItemInputMode == 0)
            {
                if (this.m_objViewer.txtExecArea.Value == null || this.m_objViewer.txtExecArea.Text.Trim() == "")
                {
                    MessageBox.Show("��ѡ��ִ�еص㡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.m_objViewer.txtExecArea.Focus();
                    return;
                }
            }

            DataRow dr = this.m_objViewer.txtItemName.Tag as DataRow;

            string invocat = this.m_strConvertToChType(dr["itemipinvtype_chr"].ToString().Trim());
            decimal Amount = clsPublic.ConvertObjToDecimal(this.m_objViewer.txtAmount.Text);

            //if (!DrugFlag && !PatchFlag && invocat.IndexOf("ҩ��") >= 0)
            //{
            //    if (clsPublic.ConvertObjToDecimal(this.m_objViewer.txtAmount.Text) < 0)
            //    {
            //        MessageBox.Show("��ǰϵͳ����Ϊ����ֹ������ҩ������Ա�ڲ�����ʱ¼�������ʡ�\r\n\r\n����������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        this.m_objViewer.txtAmount.SelectAll();
            //        this.m_objViewer.txtAmount.Focus();
            //        return;
            //    }
            //}

            if (Amount < 0)
            {
                if (IsAllowPatchNegative)
                {
                    if (invocat.IndexOf("ҩ") >= 0)
                    {

                        if (IsAllowPatchNegativeMed)
                        {
                            if (!this.m_blnCheckPatchNegative(dr, Amount, ItemInputMode))
                            {
                                MessageBox.Show("�Բ�����Ŀ����������������������������\r\n\r\n����������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.m_objViewer.txtAmount.SelectAll();
                                this.m_objViewer.txtAmount.Focus();
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("�Բ�����û��Ȩ�޶�ҩƷ���и������ʡ�\r\n\r\n����������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.m_objViewer.txtAmount.SelectAll();
                            this.m_objViewer.txtAmount.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (IsAllowPatchNegativeNoMed)
                        {
                            if (!this.m_blnCheckPatchNegative(dr, Amount, ItemInputMode))
                            {
                                MessageBox.Show("�Բ�����Ŀ����������������������������\r\n\r\n����������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.m_objViewer.txtAmount.SelectAll();
                                this.m_objViewer.txtAmount.Focus();
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("�Բ�����û��Ȩ�޶��շ���Ŀ���и������ʡ�\r\n\r\n����������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.m_objViewer.txtAmount.SelectAll();
                            this.m_objViewer.txtAmount.Focus();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("�Բ��𣬵�ǰϵͳ����Ϊ�������ڲ����ʴ��˷�\r\n\r\n���ڡ������ѯ���������˷ѣ�лл��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.m_objViewer.txtAmount.SelectAll();
                    this.m_objViewer.txtAmount.Focus();
                    return;
                }
            }

            string ApplyAreaID = this.m_objViewer.txtApplyArea.Value.ToString().Trim();

            if (ItemInputMode == 0)
            {
                string[] sItem = new string[20];

                sItem[0] = Convert.ToString(this.m_objViewer.dtgItem.Rows.Count + 1);
                sItem[1] = this.m_objViewer.txtExecArea.Text.Trim();
                sItem[2] = dr["itemcode_vchr"].ToString().Trim();
                sItem[3] = dr["itemname_vchr"].ToString().Trim();
                sItem[4] = dr["itemspec_vchr"].ToString().Trim();

                string ItemUnit, ItemPrice;
                if (dr["ipchargeflg_int"].ToString().Trim() == "1")
                {
                    ItemUnit = dr["itemipunit_chr"].ToString().Trim();
                    ItemPrice = dr["submoney"].ToString().Trim();
                }
                else
                {
                    ItemUnit = dr["itemunit_chr"].ToString().Trim();
                    ItemPrice = dr["itemprice_mny"].ToString().Trim();
                }
                sItem[5] = ItemUnit;
                sItem[6] = Amount.ToString();
                sItem[7] = ItemPrice;
                sItem[8] = Convert.ToDecimal(this.m_objViewer.lblTotal.Text).ToString("0.00");

                string Precent = "100";
                if (dr["precent_dec"].ToString().Trim() != "")
                {
                    Precent = dr["precent_dec"].ToString().Trim();
                }
                sItem[9] = Precent;

                decimal d = clsPublic.Round(clsPublic.ConvertObjToDecimal(sItem[8]) * clsPublic.ConvertObjToDecimal(sItem[9]) / 100, 2);
                sItem[10] = d.ToString("0.00");
                sItem[11] = ApplyAreaID;
                sItem[12] = this.m_objViewer.txtChargeDoctor.Value;
                sItem[13] = this.m_objViewer.txtExecArea.Value;
                sItem[14] = "0";  //0 �½� 1 ��ʷ��¼
                sItem[15] = invocat;
                sItem[16] = "";
                sItem[17] = "0";
                sItem[18] = "0";
                //��ҩ�ж�
                if (dr["putmedtype_int"].ToString().Trim() == "1")
                {
                    sItem[18] = "1";
                }
                sItem[19] = this.m_objViewer.txtChargeDoctor.Text;

                int row = this.m_objViewer.dtgItem.Rows.Add(sItem);
                this.m_objViewer.dtgItem.Rows[row].Tag = dr;
                this.m_objViewer.dtgItem.Rows[row].Selected = true;
                this.m_objViewer.CurrRowNo = row;

                d += clsPublic.ConvertObjToDecimal(this.m_objViewer.lblAllTotal.Text);
                this.m_objViewer.lblAllTotal.Text = d.ToString("0.00");
            }
            else if (ItemInputMode == 1)
            {
                if (!this.m_blnAddItemByOrderID(dr, Amount))
                {
                    return;
                }
            }
            this.m_mthClear();
            IsModify = true;
        }
        #endregion

        #region �������������Ŀ+�շ���Ŀ
        /// <summary>
        /// �������������Ŀ+�շ���Ŀ
        /// </summary>
        /// <param name="objArr">������Ŀ����</param>
        /// <param name="Nums">����ֵ</param>
        public void m_mthAddOrderItem(ArrayList objArr, decimal Nums)
        {
            if (objArr.Count == 0)
            {
                return;
            }

            for (int i = 0; i < objArr.Count; i++)
            {
                string OrderID = objArr[i].ToString();
                DataTable dt;

                long l = this.objSvc.m_lngFindOrderByID(OrderID, out dt, this.IsChildPrice);
                if (l > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DataRow dr = dt.Rows[j];

                        string invocat = this.m_strConvertToChType(dr["itemipinvtype_chr"].ToString().Trim());

                        if (!DrugFlag && !PatchFlag && invocat.IndexOf("ҩ��") >= 0)
                        {
                            if (clsPublic.ConvertObjToDecimal(this.m_objViewer.txtAmount.Text) < 0)
                            {
                                MessageBox.Show("��ǰϵͳ����Ϊ����ֹ������ҩ������Ա�ڲ�����ʱ¼�������ʡ�\r\n\r\n����������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.m_objViewer.txtAmount.SelectAll();
                                this.m_objViewer.txtAmount.Focus();
                                return;
                            }
                        }

                        if (!this.m_blnAddItemByOrderID(dr, Nums))
                        {
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("��ȡ������Ŀ����ʧ��.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            this.m_objViewer.txtItemName.Text = "";
            this.m_objViewer.txtItemName.Focus();
            IsModify = true;
        }

        #region ����������Ŀ����շ���Ŀ
        /// <summary>
        /// ����������Ŀ����շ���Ŀ
        /// </summary>
        /// <param name="dr"></param>  
        /// <param name="BaseNums"></param>  
        /// <returns></returns>
        private bool m_blnAddItemByOrderID(DataRow dr, decimal BaseNums)
        {
            string ApplyAreaID = this.m_objViewer.txtApplyArea.Value.ToString().Trim();
            string OrderID = dr["orderdicid_chr"].ToString();

            DataTable dt;
            long l = this.objSvc.m_lngGetChargeItemByOrderID(OrderID, this.m_objViewer.ucPatientInfo.BihPatient_VO.PayTypeID, out dt, this.IsChildPrice);
            if (l > 0)
            {
                #region �������Ŀ
                int num = this.m_objViewer.dtgOrder.Rows.Count + 1;
                string[] sarr = new string[4];
                sarr[0] = num.ToString();
                sarr[1] = dr["name_chr"].ToString().Trim();
                sarr[2] = num.ToString() + "->" + OrderID;
                sarr[3] = "0";  //0 �½� 1 ��ʷ��¼

                int row = this.m_objViewer.dtgOrder.Rows.Add(sarr);
                this.m_objViewer.dtgOrder.Rows[row].Tag = dr;
                this.m_objViewer.dtgOrder.Rows[row].Selected = false;
                #endregion

                #region ��ӹ�����ϸ��Ŀ

                //������Ŀ�����շ���ϸ��Ŀ
                int orderentrycount = dt.Rows.Count;
                decimal ordertotal = 0;
                this.m_objViewer.dtgOrderItem.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr2 = dt.Rows[i];

                    string invocat = this.m_strConvertToChType(dr2["itemipinvtype_chr"].ToString().Trim());
                    string ItemId = dr2["itemid_chr"].ToString();
                    string ExecAreaName = "";
                    string ExecAreaID = this.objSvc.m_strGetChargeItemDefaultExecAreaID(ItemId, ApplyAreaID, out ExecAreaName);
                    if (ExecAreaID.Trim() == "")
                    {
                        ExecAreaID = this.m_objViewer.ucPatientInfo.BihPatient_VO.AreaID;
                        ExecAreaName = this.m_objViewer.ucPatientInfo.BihPatient_VO.AreaName;
                    }

                    string[] sItem = new string[21];

                    #region Item�ؼ�
                    sItem[0] = Convert.ToString(this.m_objViewer.dtgItem.Rows.Count + 1);
                    sItem[1] = ExecAreaName;
                    sItem[2] = dr2["itemcode_vchr"].ToString().Trim();
                    sItem[3] = dr2["itemname_vchr"].ToString().Trim();
                    sItem[4] = dr2["itemspec_vchr"].ToString().Trim();

                    string ItemUnit, ItemPrice;
                    if (dr2["ipchargeflg_int"].ToString().Trim() == "1")
                    {
                        ItemUnit = dr2["itemipunit_chr"].ToString().Trim();
                        ItemPrice = dr2["submoney"].ToString().Trim();
                    }
                    else
                    {
                        ItemUnit = dr2["itemunit_chr"].ToString().Trim();
                        ItemPrice = dr2["itemprice_mny"].ToString().Trim();
                    }
                    decimal d = BaseNums * clsPublic.ConvertObjToDecimal(dr2["totalqty_dec"]);
                    sItem[5] = ItemUnit;
                    sItem[6] = d.ToString("0.00");
                    sItem[7] = ItemPrice;

                    d = clsPublic.ConvertObjToDecimal(sItem[6]) * clsPublic.ConvertObjToDecimal(sItem[7]);
                    sItem[8] = d.ToString("0.00");

                    string Precent = "100";
                    if (dr2["precent_dec"].ToString().Trim() != "")
                    {
                        Precent = dr2["precent_dec"].ToString().Trim();
                    }
                    sItem[9] = Precent;

                    d = clsPublic.Round(clsPublic.ConvertObjToDecimal(sItem[8]) * clsPublic.ConvertObjToDecimal(sItem[9]) / 100, 2);
                    sItem[10] = d.ToString("0.00");
                    sItem[11] = ApplyAreaID;
                    sItem[12] = this.m_objViewer.txtChargeDoctor.Value;
                    sItem[13] = ExecAreaID;
                    sItem[14] = "0";  //0 �½� 1 ��ʷ��¼
                    sItem[15] = invocat;
                    sItem[16] = num.ToString() + "->" + OrderID;
                    sItem[17] = "0";
                    sItem[18] = "0";

                    //��ҩ�ж�
                    if (dr2["putmedtype_int"].ToString().Trim() == "1")
                    {
                        if (orderentrycount == 1)
                        {
                            sItem[18] = "1";
                        }
                        else
                        {
                            if (IsControlPutMed)
                            {
                                sItem[18] = "1";
                            }
                        }
                    }
                    sItem[19] = this.m_objViewer.txtChargeDoctor.Text;

                    if (intDiffCostOn == 1 && strDiffCostOn.IndexOf(dr2["medicinetypeid_chr"].ToString()) > -1)
                    {
                        sItem[20] = clsPublic.Round(clsPublic.ConvertObjToDecimal(dr2["diffprice_mny"]) * clsPublic.ConvertObjToDecimal(sItem[6]) * (-1), 2).ToString();
                    }
                    else
                    {
                        sItem[20] = "0.00";
                    }

                    row = this.m_objViewer.dtgItem.Rows.Add(sItem);
                    this.m_objViewer.dtgItem.Rows[row].Tag = dr2;
                    ordertotal += d;
                    #endregion

                    #region OrderItem�ؼ�
                    sItem[0] = Convert.ToString(i + 1);
                    row = this.m_objViewer.dtgOrderItem.Rows.Add(sItem);
                    this.m_objViewer.dtgOrderItem.Rows[row].Tag = dr2;
                    #endregion
                }
                this.m_objViewer.dtgOrderItem.Rows[0].Selected = false;

                ordertotal += clsPublic.ConvertObjToDecimal(this.m_objViewer.lblAllTotal.Text);
                this.m_objViewer.lblAllTotal.Text = ordertotal.ToString("0.00");

                this.m_objViewer.dtgOrderItem.Rows[0].Selected = true;
                this.m_objViewer.CurrRowNo = 0;
                #endregion
            }
            else
            {
                return false;
            }

            return true;
        }
        #endregion

        #endregion

        #region ���������Ŀ
        /// <summary>
        /// ���������Ŀ
        /// </summary>
        /// <param name="objArr"></param>
        /// <param name="Nums"></param>       
        public void m_mthAddItem(ArrayList objArr, int Nums)
        {
            for (int i = 0; i < objArr.Count; i++)
            {
                DataRow dr = objArr[i] as DataRow;

                string ExecAreaID = "";

                if (dr["areaid1"].ToString().Trim() == "")
                {
                    ExecAreaID = dr["areaid2"].ToString().Trim();
                }
                else
                {
                    ExecAreaID = dr["areaid1"].ToString().Trim();
                }

                string[] sItem = new string[16];

                sItem[0] = Convert.ToString(this.m_objViewer.dtgItem.Rows.Count + 1);
                sItem[1] = this.m_strGetDeptName(ExecAreaID);
                sItem[2] = dr["itemcode_vchr"].ToString().Trim();
                sItem[3] = dr["itemname_vchr"].ToString().Trim();
                sItem[4] = dr["itemspec_vchr"].ToString().Trim();

                string ItemUnit, ItemPrice;
                if (dr["ipchargeflg_int"].ToString().Trim() == "1")
                {
                    ItemUnit = dr["itemipunit_chr"].ToString().Trim();
                    ItemPrice = dr["submoney"].ToString().Trim();
                }
                else
                {
                    ItemUnit = dr["itemunit_chr"].ToString().Trim();
                    ItemPrice = dr["itemprice_mny"].ToString().Trim();
                }
                sItem[5] = ItemUnit;
                sItem[6] = Convert.ToString(clsPublic.ConvertObjToDecimal(dr["amount"]) * Nums);
                sItem[7] = ItemPrice;

                decimal d = clsPublic.ConvertObjToDecimal(sItem[6]) * clsPublic.ConvertObjToDecimal(sItem[7]);

                sItem[8] = d.ToString("0.00");

                string Precent = "100";
                if (dr["precent_dec"].ToString().Trim() != "")
                {
                    Precent = dr["precent_dec"].ToString().Trim();
                }
                sItem[9] = Precent;
                sItem[10] = Convert.ToDecimal(d * clsPublic.ConvertObjToDecimal(sItem[9]) / 100).ToString("0.00");
                sItem[11] = this.m_objViewer.txtApplyArea.Value;
                sItem[12] = this.m_objViewer.txtChargeDoctor.Value;
                sItem[13] = ExecAreaID;
                sItem[14] = "0";  //0 �½� 1 ��ʷ��¼
                sItem[15] = this.m_strConvertToChType(dr["itemipinvtype_chr"].ToString().Trim());

                int row = this.m_objViewer.dtgItem.Rows.Add(sItem);
                this.m_objViewer.dtgItem.Rows[row].Tag = dr;
                this.m_objViewer.dtgItem.Rows[row].Selected = true;
                this.m_objViewer.CurrRowNo = row;

                d += clsPublic.ConvertObjToDecimal(this.m_objViewer.lblAllTotal.Text);
                this.m_objViewer.lblAllTotal.Text = d.ToString("0.00");
            }

            this.m_mthClear();
        }
        #endregion

        #region ɾ����Ŀ
        /// <summary>
        /// ɾ����Ŀ
        /// </summary>
        /// <param name="Type">1 ɾ������ 2 ɾ����ϸ</param>
        public void m_mthDelItem(int Type)
        {
            if (this.IsCanContinuePatch() == false)
            {
                MessageBox.Show("�Բ��𣬷�������� �����ٲ����ˡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Type == 1)
            {
                bool b = false;
                for (int i = 0; i < this.m_objViewer.dtgOrder.Rows.Count; i++)
                {
                    if (this.m_objViewer.dtgOrder.Rows[i].Selected)
                    {
                        string attachorderid = "";
                        string ordername = "";
                        DataRow dr = this.m_objViewer.dtgOrder.Rows[i].Tag as DataRow;

                        if (this.m_objViewer.dtgOrder.Rows[i].Cells["orderflag"].Value.ToString() == "0")
                        {
                            ordername = dr["name_chr"].ToString();
                            attachorderid = this.m_objViewer.dtgOrder.Rows[i].Cells["attachorderid"].Value.ToString();
                        }
                        else
                        {
                            ordername = dr["orderdicname_vchr"].ToString();
                            attachorderid = dr["attachorderid_vchr"].ToString();
                        }

                        if (MessageBox.Show("�Ƿ�ɾ�����շ���Ŀ(������Ŀ)����" + ordername + "��,��ϸ��Ŀ��һ��ɾ��? \r\n\r\nɾ����Ŀ���밴[����]������", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            for (int j = this.m_objViewer.dtgItem.Rows.Count - 1; j >= 0; j--)
                            {
                                if (this.m_objViewer.dtgItem.Rows[j].Cells["orderid"].Value.ToString().Trim() == attachorderid)
                                {
                                    this.m_objViewer.dtgItem.Rows.RemoveAt(j);
                                }
                            }

                            this.m_objViewer.dtgOrder.Rows.RemoveAt(i);
                            this.m_objViewer.dtgOrderItem.Rows.Clear();

                            this.m_objViewer.CurrRowNo = -1;
                        }

                        b = true;
                        break;
                    }
                }

                if (!b)
                {
                    MessageBox.Show("��ѡ�����շ���Ŀ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (Type == 2)
            {
                int RowNo = this.m_objViewer.CurrRowNo;

                if (RowNo < 0)
                {
                    return;
                }

                DataGridView dgrid = null;

                if (ItemInputMode == 0)
                {
                    dgrid = this.m_objViewer.dtgItem;
                    colSuffix = "";
                }
                else if (ItemInputMode == 1)
                {
                    dgrid = this.m_objViewer.dtgOrderItem;
                    colSuffix = "1";
                    if (RowNo > dgrid.Rows.Count - 1)
                    {
                        RowNo = 0;
                    }
                }

                if (dgrid == null || dgrid.Rows.Count == 0)
                {
                    return;
                }

                DataRow dr = dgrid.Rows[RowNo].Tag as DataRow;
                string ItemName = "";
                if (dgrid.Rows[RowNo].Cells["flag" + colSuffix].Value.ToString() == "0")
                {
                    ItemName = dr["itemname_vchr"].ToString().Trim();
                }
                else
                {
                    ItemName = dr["chargeitemname_chr"].ToString().Trim();
                }

                if (MessageBox.Show("�Ƿ�ɾ���շ���Ŀ����" + ItemName + "��? \r\n\r\nɾ����Ŀ���밴<����>�����浱ǰ��¼", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (ItemInputMode == 1)
                    {
                        for (int i = 0; i < this.m_objViewer.dtgItem.Rows.Count; i++)
                        {
                            if (this.m_objViewer.dtgItem.Rows[i].Cells["itemcode"].Value.ToString().Trim() == dgrid.Rows[RowNo].Cells["itemcode" + colSuffix].Value.ToString().Trim() &&
                                this.m_objViewer.dtgItem.Rows[i].Cells["orderid"].Value.ToString().Trim() == dgrid.Rows[RowNo].Cells["orderid" + colSuffix].Value.ToString().Trim())
                            {
                                this.m_objViewer.dtgItem.Rows.RemoveAt(i);
                                break;
                            }
                        }
                    }

                    string attachorderid = dgrid.Rows[RowNo].Cells["orderid" + colSuffix].Value.ToString().Trim();
                    dgrid.Rows.RemoveAt(RowNo);

                    if (ItemInputMode == 1 && dgrid.Rows.Count == 0)
                    {
                        for (int i = 0; i < this.m_objViewer.dtgOrder.Rows.Count; i++)
                        {
                            if (this.m_objViewer.dtgOrder.Rows[i].Cells["attachorderid"].Value.ToString().Trim() == attachorderid)
                            {
                                this.m_objViewer.dtgOrder.Rows.RemoveAt(i);
                                break;
                            }
                        }
                    }

                    if (RowNo >= dgrid.Rows.Count)
                    {
                        this.m_objViewer.CurrRowNo = dgrid.Rows.Count - 1;
                    }
                }
                else
                {
                    return;
                }
            }

            decimal total = 0;
            for (int j = 0; j < this.m_objViewer.dtgItem.Rows.Count; j++)
            {
                total += clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgItem.Rows[j].Cells["total"].Value);
            }
            if (total > 0)
            {
                this.m_objViewer.lblAllTotal.Text = total.ToString("0.00");
            }
            else
            {
                this.m_objViewer.lblAllTotal.Text = "";
            }
        }
        #endregion

        #region �Ƿ��������������
        /// <summary>
        /// �Ƿ��������������
        /// </summary>
        /// <returns></returns>
        bool IsCanContinuePatch()
        {
            // Ԥ��Ժ
            DataTable dt = this.objSvc.GetPatientCheckFee(this.m_objViewer.ucPatientInfo.BihPatient_VO.RegisterID);
            if ((dt == null || dt.Rows.Count == 0) || (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ischeckfee"].ToString() == "0"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public bool m_blnSave()
        {
            if (this.IsCanContinuePatch() == false)
            {
                MessageBox.Show("�Բ��𣬷�������� �����ٲ����ˡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (this.m_objViewer.dtgItem.Rows.Count == 0 && CurrOrderID == "")
            {
                this.IsModify = false;
                return false;
            }

            if (CurrOrderID != "" && CurrOrderStatus == 1)
            {
                return false;
            }

            if (!IsAllowPatch && this.m_objViewer.ucPatientInfo.BihPatient_VO.SpecChargeCtrl == 0)
            {
                if (this.m_objViewer.ucPatientInfo.BihPatient_VO.BalanceMoney < clsPublic.ConvertObjToDecimal(this.m_objViewer.lblAllTotal.Text))
                {
                    MessageBox.Show("�Բ����㲹���ʽ����ڵ�ǰ���˵Ľ������ɾ�������շ���Ŀ������¼�롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            List<clsBihOrderDic_VO> OrderDicArr = new List<clsBihOrderDic_VO>();

            for (int i = 0; i < this.m_objViewer.dtgOrder.Rows.Count; i++)
            {
                clsBihOrderDic_VO OrderDic_VO = new clsBihOrderDic_VO();

                DataRow dr = this.m_objViewer.dtgOrder.Rows[i].Tag as DataRow;

                //0 �½� 1 ��ʷ��¼
                if (this.m_objViewer.dtgOrder.Rows[i].Cells["orderflag"].Value.ToString() == "0")
                {
                    OrderDic_VO.Type = 1;
                    OrderDic_VO.OrderQue = i + 1;
                    OrderDic_VO.OrderDicID = dr["orderdicid_chr"].ToString();
                    OrderDic_VO.OrderDicName = dr["name_chr"].ToString();
                    OrderDic_VO.Spec = dr["des_vchr"].ToString();
                    OrderDic_VO.Qty = 0;
                    OrderDic_VO.PriceMny = 0;
                    OrderDic_VO.TotalMny = 0;
                    OrderDic_VO.AttachOrderID = this.m_objViewer.dtgOrder.Rows[i].Cells["attachorderid"].Value.ToString();
                    OrderDic_VO.SbBaseMny = 0;
                }
                else
                {
                    OrderDic_VO.Type = 1;
                    OrderDic_VO.OrderQue = i + 1;
                    OrderDic_VO.OrderDicID = dr["orderdicid_chr"].ToString();
                    OrderDic_VO.OrderDicName = dr["orderdicname_vchr"].ToString();
                    OrderDic_VO.Spec = dr["spec_vchr"].ToString();
                    OrderDic_VO.Qty = 0;
                    OrderDic_VO.PriceMny = 0;
                    OrderDic_VO.TotalMny = 0;
                    OrderDic_VO.AttachOrderID = dr["attachorderid_vchr"].ToString();
                    OrderDic_VO.SbBaseMny = 0;
                }
                OrderDicArr.Add(OrderDic_VO);
            }

            List<clsBihPatientCharge_VO> PatientChargeArr = new List<clsBihPatientCharge_VO>();

            for (int i = 0; i < this.m_objViewer.dtgItem.Rows.Count; i++)
            {
                clsBihPatientCharge_VO PatientCharge_VO = new clsBihPatientCharge_VO();

                DataRow dr = this.m_objViewer.dtgItem.Rows[i].Tag as DataRow;

                //0 �½� 1 ��ʷ��¼
                if (this.m_objViewer.dtgItem.Rows[i].Cells["flag"].Value.ToString() == "0")
                {
                    PatientCharge_VO.PatientID = this.m_objViewer.ucPatientInfo.BihPatient_VO.PatientID;
                    PatientCharge_VO.RegisterID = this.m_objViewer.ucPatientInfo.RegisterID;
                    PatientCharge_VO.ClacArea = this.m_objViewer.dtgItem.Rows[i].Cells["execareaid"].Value.ToString();
                    PatientCharge_VO.CreateArea = this.m_objViewer.dtgItem.Rows[i].Cells["applyareaid"].Value.ToString();
                    PatientCharge_VO.CalcCateID = dr["itemipcalctype_chr"].ToString();
                    PatientCharge_VO.InvCateID = dr["itemipinvtype_chr"].ToString();
                    PatientCharge_VO.ChargeItemID = dr["itemid_chr"].ToString();
                    PatientCharge_VO.ChargeItemName = dr["itemname_vchr"].ToString();

                    if (dr["ipchargeflg_int"].ToString().Trim() == "1")
                    {
                        PatientCharge_VO.Unit = dr["itemipunit_chr"].ToString().Trim();
                        PatientCharge_VO.UnitPrice = clsPublic.ConvertObjToDecimal(dr["submoney"]);
                    }
                    else
                    {
                        PatientCharge_VO.Unit = dr["itemunit_chr"].ToString().Trim();
                        PatientCharge_VO.UnitPrice = clsPublic.ConvertObjToDecimal(dr["itemprice_mny"]);
                    }

                    PatientCharge_VO.Amount = clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgItem.Rows[i].Cells["amount"].Value);
                    PatientCharge_VO.Discount = clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgItem.Rows[i].Cells["scale"].Value);
                    PatientCharge_VO.Ismepay = 0;
                    PatientCharge_VO.Des = "";
                    PatientCharge_VO.CreateType = 4;
                    PatientCharge_VO.Creator = this.m_objViewer.LoginInfo.m_strEmpID;
                    PatientCharge_VO.Operator = this.m_objViewer.LoginInfo.m_strEmpID;
                    PatientCharge_VO.PStatus = 0;
                    PatientCharge_VO.Activator = this.m_objViewer.dtgItem.Rows[i].Cells["applydoctorid"].Value.ToString();
                    PatientCharge_VO.ActivateType = 2;
                    PatientCharge_VO.IsRich = int.Parse(dr["isrich_int"].ToString());
                    PatientCharge_VO.CurAreaID = this.m_objViewer.ucPatientInfo.BihPatient_VO.AreaID;
                    PatientCharge_VO.CurBedID = this.m_objViewer.ucPatientInfo.BihPatient_VO.BedID;
                    PatientCharge_VO.DoctorID = this.m_objViewer.ucPatientInfo.BihPatient_VO.DoctorID;
                    PatientCharge_VO.Doctor = this.m_objViewer.ucPatientInfo.BihPatient_VO.DoctorName;
                    PatientCharge_VO.DoctorGroupID = this.m_objViewer.ucPatientInfo.BihPatient_VO.DoctorGroupID;
                    PatientCharge_VO.NeedConfirm = 0;
                    PatientCharge_VO.ActiveDat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    PatientCharge_VO.TotalMoney_dec = clsPublic.Round(PatientCharge_VO.UnitPrice * PatientCharge_VO.Amount, 2);
                    PatientCharge_VO.AcctMoney_dec = PatientCharge_VO.TotalMoney_dec - clsPublic.Round(PatientCharge_VO.UnitPrice * PatientCharge_VO.Amount * PatientCharge_VO.Discount / 100, 2);
                    PatientCharge_VO.AttachOrderID = this.m_objViewer.dtgItem.Rows[i].Cells["orderid"].Value.ToString();
                    PatientCharge_VO.AttachOrderBaseNum = 0;
                    PatientCharge_VO.SPEC_VCHR = dr["itemspec_vchr"].ToString();
                    PatientCharge_VO.PutMedicineFlag = int.Parse(clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgItem.Rows[i].Cells["putmedicineflag"].Value).ToString());
                    PatientCharge_VO.CHARGEDOCTORID_CHR = this.m_objViewer.dtgItem.Rows[i].Cells["applydoctorid"].Value.ToString();
                    PatientCharge_VO.CHARGEDOCTOR_VCHR = this.m_objViewer.dtgItem.Rows[i].Cells["colChargeDoctorName"].Value.ToString();
                    PatientCharge_VO.CHARGEDOCTORGROUPID_CHR = this.m_objViewer.LoginInfo.m_strGroupID;
                    PatientCharge_VO.TotalDiffCostMoney_dec = clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgItem.Rows[i].Cells["rljels"].Value);
                    PatientCharge_VO.BuyPrice = dr["buyprice_dec"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["buyprice_dec"]);
                }
                else
                {
                    PatientCharge_VO.PatientID = dr["patientid_chr"].ToString();
                    PatientCharge_VO.RegisterID = dr["registerid_chr"].ToString();
                    PatientCharge_VO.ClacArea = this.m_objViewer.dtgItem.Rows[i].Cells["execareaid"].Value.ToString();
                    PatientCharge_VO.CreateArea = dr["createarea_chr"].ToString();
                    PatientCharge_VO.CreateDat = Convert.ToDateTime(dr["create_dat"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    PatientCharge_VO.CalcCateID = dr["calccateid_chr"].ToString();
                    PatientCharge_VO.InvCateID = dr["invcateid_chr"].ToString();
                    PatientCharge_VO.ChargeItemID = dr["chargeitemid_chr"].ToString();
                    PatientCharge_VO.ChargeItemName = dr["chargeitemname_chr"].ToString();
                    PatientCharge_VO.Unit = dr["unit_vchr"].ToString();
                    PatientCharge_VO.UnitPrice = clsPublic.ConvertObjToDecimal(dr["unitprice_dec"]);
                    PatientCharge_VO.Amount = clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgItem.Rows[i].Cells["amount"].Value);
                    PatientCharge_VO.Discount = clsPublic.ConvertObjToDecimal(dr["discount_dec"]);
                    PatientCharge_VO.Ismepay = 0;
                    PatientCharge_VO.Des = "";
                    PatientCharge_VO.CreateType = 4;
                    PatientCharge_VO.Creator = dr["creator_chr"].ToString();
                    PatientCharge_VO.Operator = dr["operator_chr"].ToString();
                    PatientCharge_VO.PStatus = 0;
                    PatientCharge_VO.Activator = dr["activator_chr"].ToString();
                    PatientCharge_VO.ActivateType = 2;
                    PatientCharge_VO.IsRich = int.Parse(dr["isrich_int"].ToString());
                    PatientCharge_VO.CurAreaID = dr["curareaid_chr"].ToString();
                    PatientCharge_VO.CurBedID = dr["curbedid_chr"].ToString();
                    PatientCharge_VO.DoctorID = dr["doctorid_chr"].ToString();
                    PatientCharge_VO.Doctor = dr["doctor_vchr"].ToString();
                    PatientCharge_VO.DoctorGroupID = dr["doctorgroupid_chr"].ToString();
                    PatientCharge_VO.NeedConfirm = 0;
                    PatientCharge_VO.ActiveDat = Convert.ToDateTime(dr["chargeactive_dat"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    PatientCharge_VO.TotalMoney_dec = clsPublic.Round(PatientCharge_VO.UnitPrice * PatientCharge_VO.Amount, 2);
                    PatientCharge_VO.AcctMoney_dec = PatientCharge_VO.TotalMoney_dec - clsPublic.Round(PatientCharge_VO.UnitPrice * PatientCharge_VO.Amount * PatientCharge_VO.Discount / 100, 2);
                    PatientCharge_VO.AttachOrderID = dr["attachorderid_vchr"].ToString();
                    PatientCharge_VO.AttachOrderBaseNum = clsPublic.ConvertObjToDecimal(dr["attachorderbasenum_dec"]);
                    PatientCharge_VO.SPEC_VCHR = dr["spec_vchr"].ToString();
                    PatientCharge_VO.PutMedicineFlag = int.Parse(dr["putmedicineflag_int"].ToString());
                    PatientCharge_VO.CHARGEDOCTORID_CHR = dr["chargedoctorid_chr"].ToString();
                    PatientCharge_VO.CHARGEDOCTOR_VCHR = dr["chargedoctor_vchr"].ToString();
                    PatientCharge_VO.CHARGEDOCTORGROUPID_CHR = dr["chargedoctorgroupid_chr"].ToString();
                    PatientCharge_VO.TotalDiffCostMoney_dec = clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgItem.Rows[i].Cells["rljels"].Value);
                    PatientCharge_VO.BuyPrice = dr["buyprice_dec"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["buyprice_dec"]);
                }

                // 20190826
                if (Math.Abs(PatientCharge_VO.TotalDiffCostMoney_dec) > Math.Abs(PatientCharge_VO.TotalMoney_dec))
                {
                    MessageBox.Show(PatientCharge_VO.ChargeItemName + ": ��������쳣(������������Ŀ�ܽ��)������(����ɾ��������¼��)��");
                    return false;
                }

                PatientChargeArr.Add(PatientCharge_VO);
            }

            long l = this.objSvc.m_lngGenPatientChargeByDir(OrderDicArr, PatientChargeArr, 9, ref CurrOrderID);
            if (l > 0)
            {
                this.m_mthLoadHistory();
                this.CurrOrderStatus = 0;
                this.m_objViewer.btnCommit.Enabled = true;
                IsModify = false;
            }
            else
            {
                MessageBox.Show("��������ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }
        #endregion


        #region �ύ
        /// <summary>
        /// �ύ
        /// </summary>
        public void m_mthCommit()
        {
            if (!this.m_objViewer.btnCommit.Enabled)
            {
                return;
            }

            if (!this.m_blnSave())
            {
                return;
            }

            if (CurrOrderID == "" || CurrOrderStatus == 1)
            {
                return;
            }

            if (MessageBox.Show("���ύ���󽫲����ٸ�����¼��Ŀ���Ƿ�����ύ������", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            long l = this.objSvc.m_lngCommitPatchCharge(CurrOrderID, this.m_objViewer.ucPatientInfo.RegisterID, this.m_objViewer.LoginInfo.m_strEmpID, int.Parse(this.m_objViewer.Scope));
            if (l > 0)
            {
                this.m_objViewer.ucPatientInfo.m_mthShortCurFind();
                this.m_mthLoadHistory();
                this.m_objViewer.btnNew_Click(null, null);

                /***��ҩ�ӿ�***/
                //ArrayList arrReg = new ArrayList();
                //arrReg.Add(this.m_objViewer.ucPatientInfo.RegisterID);
                //IPutMadicine iPut = PutMadicineFactory.GetInstanceForRecipeMed();
                //iPut.CreatePutMedDetailByDptType(arrReg, this.m_objViewer.LoginInfo.m_strEmpID, int.Parse(this.m_objViewer.Scope));
                /******/
            }
            else
            {
                MessageBox.Show("�ύ����ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region ��ʾ��ʷ��¼
        /// <summary>
        /// ��ʾ��ʷ��¼
        /// </summary>
        /// <param name="OrderID"></param>
        public void m_mthShowHistory(string OrderID)
        {
            if (dtSource == null || dtSource.Rows.Count == 0)
            {
                return;
            }

            this.m_objViewer.dtgItem.Rows.Clear();
            this.m_objViewer.dtgOrder.Rows.Clear();
            this.m_objViewer.dtgOrderItem.Rows.Clear();
            decimal total = 0;

            if (ItemInputMode == 1)
            {
                this.m_objViewer.dtgItem.Visible = false;

                DataTable dt;
                long l = this.objSvc.m_lngGetOrderDic(OrderID, out dt);
                if (l > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region �������Ŀ
                        DataRow dr = dt.Rows[i];
                        string[] sarr = new string[4];
                        sarr[0] = Convert.ToString(i + 1);
                        sarr[1] = dr["orderdicname_vchr"].ToString().Trim();
                        sarr[2] = dr["attachorderid_vchr"].ToString();
                        sarr[3] = "1";  //0 �½� 1 ��ʷ��¼

                        int row = this.m_objViewer.dtgOrder.Rows.Add(sarr);
                        this.m_objViewer.dtgOrder.Rows[row].Tag = dr;
                        #endregion
                    }

                    if (this.m_objViewer.dtgOrder.Rows.Count > 0)
                    {
                        this.m_objViewer.dtgOrder.Rows[0].Selected = false;
                    }
                    else
                    {
                        this.m_objViewer.dtgItem.Visible = true;
                        this.m_objViewer.dtgItem.BringToFront();
                    }
                }
            }

            DataRow[] drSelect;
            drSelect = dtSource.Select("orderid_chr = '" + OrderID + "'");
            for (int i = 0; i < drSelect.Length; i++)
            {
                DataRow dr = drSelect[i];

                string[] sItem = new string[21];

                sItem[0] = Convert.ToString(i + 1);
                sItem[1] = dr["execarea"].ToString().Trim();
                sItem[2] = dr["itemcode_vchr"].ToString().Trim();
                sItem[3] = dr["chargeitemname_chr"].ToString().Trim();
                sItem[4] = dr["itemspec_vchr"].ToString().Trim();
                sItem[5] = dr["unit_vchr"].ToString().Trim();
                sItem[6] = dr["amount_dec"].ToString().Trim();
                sItem[7] = dr["unitprice_dec"].ToString().Trim();
                sItem[8] = Convert.ToDecimal(dr["totalmoney_dec"]).ToString("0.00");
                sItem[9] = dr["precent_dec"].ToString();
                sItem[10] = Convert.ToDecimal(clsPublic.ConvertObjToDecimal(dr["totalmoney_dec"]) - clsPublic.ConvertObjToDecimal(dr["acctmoney_dec"])).ToString("0.00");
                sItem[11] = dr["createarea_chr"].ToString();
                sItem[12] = dr["activator_chr"].ToString();
                sItem[13] = dr["clacarea_chr"].ToString();
                sItem[14] = "1";  //0 �½� 1 ��ʷ��¼
                sItem[15] = this.m_strConvertToChType(dr["invcateid_chr"].ToString().Trim());
                sItem[16] = dr["attachorderid_vchr"].ToString();

                sItem[17] = "0";
                sItem[18] = dr["putmedicineflag_int"].ToString();
                sItem[19] = dr["chargedoctor_vchr"].ToString();
                if (intDiffCostOn == 1)
                {
                    if (dr["totaldiffcostmoney_dec"] != null && dr["totaldiffcostmoney_dec"].ToString() != string.Empty)
                    {
                        sItem[20] = dr["totaldiffcostmoney_dec"].ToString();
                    }
                    else
                    {
                        sItem[20] = "0.00";
                    }
                }
                else
                {
                    sItem[20] = "0.00";
                }
                int row = this.m_objViewer.dtgItem.Rows.Add(sItem);
                this.m_objViewer.dtgItem.Rows[row].Tag = dr;
                this.m_objViewer.dtgItem.Rows[row].Selected = true;
                this.m_objViewer.CurrRowNo = row;
                total += clsPublic.ConvertObjToDecimal(dr["totalmoney_dec"]);

                if (Math.IEEERemainder(Convert.ToDouble(i + 1), 2) == 0)
                {
                    this.m_objViewer.dtgItem.Rows[row].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                }
            }
            if (total != 0)
            {
                this.m_objViewer.lblAllTotal.Text = total.ToString("0.00");
            }

            if (this.m_objViewer.dtgOrder.Rows.Count > 0)
            {
                this.m_mthShowOrderEntry(this.m_objViewer.dtgOrder.Rows[0].Cells["attachorderid"].Value.ToString());
            }
        }
        #endregion

        #region �޸�
        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="row">��ǰ�к�</param>  
        public void m_mthModify(int row)
        {
            clsParmItem_VO Item_VO = new clsParmItem_VO();

            DataGridView dgrid = null;

            if (ItemInputMode == 0)
            {
                dgrid = this.m_objViewer.dtgItem;
                colSuffix = "";
            }
            else if (ItemInputMode == 1)
            {
                dgrid = this.m_objViewer.dtgOrderItem;
                colSuffix = "1";
            }

            Item_VO.ItemCode = dgrid.Rows[row].Cells["itemcode" + colSuffix].Value.ToString();
            Item_VO.ItemName = dgrid.Rows[row].Cells["itemname" + colSuffix].Value.ToString();
            Item_VO.ItemAmout = clsPublic.ConvertObjToDecimal(dgrid.Rows[row].Cells["amount" + colSuffix].Value);
            Item_VO.DeptID = dgrid.Rows[row].Cells["execareaid" + colSuffix].Value.ToString();
            Item_VO.DeptName = dgrid.Rows[row].Cells["execarea" + colSuffix].Value.ToString();

            string invocat = dgrid.Rows[row].Cells["invocat" + colSuffix].Value.ToString();
            //if (!this.DrugFlag && !this.PatchFlag && dgrid.Rows[row].Cells["invocat" + colSuffix].Value.ToString().IndexOf("ҩ��") >= 0)
            //{
            //    Item_VO.AllowNegative = false;
            //}
            //else
            //{
            //    Item_VO.AllowNegative = true;
            //}

            frmAidEditItem fEdit = new frmAidEditItem(ref Item_VO);
            if (fEdit.ShowDialog() == DialogResult.OK)
            {
                if (Item_VO.ItemAmout < 0)
                {
                    DataRow dr = dgrid.Rows[row].Tag as DataRow;

                    if (invocat.IndexOf("ҩ") >= 0)
                    {
                        if (IsAllowPatchNegativeMed)
                        {
                            if (!this.m_blnCheckPatchNegative(dr, Item_VO.ItemAmout, 0))
                            {
                                MessageBox.Show("�Բ��𣬳��������������������\r\n\r\n����������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("�Բ�����û��Ȩ�޶�ҩƷ���и������ʡ�\r\n\r\n����������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        if (IsAllowPatchNegativeNoMed)
                        {
                            if (!this.m_blnCheckPatchNegative(dr, Item_VO.ItemAmout, 0))
                            {
                                MessageBox.Show("�Բ��𣬳��������������������\r\n\r\n����������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("�Բ�����û��Ȩ�޶��շ���Ŀ���и������ʡ�\r\n\r\n����������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                dgrid.Rows[row].Cells["amount" + colSuffix].Value = Item_VO.ItemAmout;
                dgrid.Rows[row].Cells["execareaid" + colSuffix].Value = Item_VO.DeptID;
                dgrid.Rows[row].Cells["execarea" + colSuffix].Value = Item_VO.DeptName;

                decimal d = clsPublic.ConvertObjToDecimal(dgrid.Rows[row].Cells["price" + colSuffix].Value)
                            * clsPublic.ConvertObjToDecimal(dgrid.Rows[row].Cells["amount" + colSuffix].Value);

                dgrid.Rows[row].Cells["total" + colSuffix].Value = d.ToString("0.00");
                dgrid.Rows[row].Cells["sbtotal" + colSuffix].Value = Convert.ToDecimal(d * clsPublic.ConvertObjToDecimal(dgrid.Rows[row].Cells["scale" + colSuffix].Value) / 100).ToString("0.00");

                if (ItemInputMode == 1)
                {
                    for (int j = 0; j < this.m_objViewer.dtgItem.Rows.Count; j++)
                    {
                        if (this.m_objViewer.dtgItem.Rows[j].Cells["itemcode"].Value.ToString().Trim() == dgrid.Rows[row].Cells["itemcode" + colSuffix].Value.ToString().Trim() &&
                            this.m_objViewer.dtgItem.Rows[j].Cells["orderid"].Value.ToString().Trim() == dgrid.Rows[row].Cells["orderid" + colSuffix].Value.ToString().Trim())
                        {
                            this.m_objViewer.dtgItem.Rows[j].Cells["amount"].Value = dgrid.Rows[row].Cells["amount" + colSuffix].Value;
                            this.m_objViewer.dtgItem.Rows[j].Cells["execareaid"].Value = dgrid.Rows[row].Cells["execareaid" + colSuffix].Value;
                            this.m_objViewer.dtgItem.Rows[j].Cells["execarea"].Value = dgrid.Rows[row].Cells["execarea" + colSuffix].Value;
                            this.m_objViewer.dtgItem.Rows[j].Cells["total"].Value = dgrid.Rows[row].Cells["total" + colSuffix].Value;
                            this.m_objViewer.dtgItem.Rows[j].Cells["sbtotal"].Value = dgrid.Rows[row].Cells["sbtotal" + colSuffix].Value;
                            break;
                        }
                    }
                }
            }

            decimal total = 0;
            for (int i = 0; i < this.m_objViewer.dtgItem.Rows.Count; i++)
            {
                total += clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgItem.Rows[i].Cells["total"].Value);
            }
            this.m_objViewer.lblAllTotal.Text = total.ToString("0.00");
        }
        #endregion

        #region ��ʾ������Ŀ��ϸ
        /// <summary>
        /// ��ʾ������Ŀ��ϸ
        /// </summary>
        /// <param name="AttatchOrderID"></param>
        public void m_mthShowOrderEntry(string AttatchOrderID)
        {
            int rowno = 1;
            this.m_objViewer.dtgOrderItem.Rows.Clear();
            for (int i = 0; i < this.m_objViewer.dtgItem.Rows.Count; i++)
            {
                if (this.m_objViewer.dtgItem.Rows[i].Cells["orderid"].Value.ToString() == AttatchOrderID)
                {
                    string[] sItem = new string[21];

                    sItem[0] = Convert.ToString(rowno.ToString());
                    sItem[1] = this.m_objViewer.dtgItem.Rows[i].Cells["execarea"].Value.ToString();
                    sItem[2] = this.m_objViewer.dtgItem.Rows[i].Cells["itemcode"].Value.ToString();
                    sItem[3] = this.m_objViewer.dtgItem.Rows[i].Cells["itemname"].Value.ToString();
                    sItem[4] = this.m_objViewer.dtgItem.Rows[i].Cells["standard"].Value.ToString();
                    sItem[5] = this.m_objViewer.dtgItem.Rows[i].Cells["unit"].Value.ToString();
                    sItem[6] = this.m_objViewer.dtgItem.Rows[i].Cells["amount"].Value.ToString();
                    sItem[7] = this.m_objViewer.dtgItem.Rows[i].Cells["price"].Value.ToString();
                    sItem[8] = this.m_objViewer.dtgItem.Rows[i].Cells["total"].Value.ToString();
                    sItem[9] = this.m_objViewer.dtgItem.Rows[i].Cells["scale"].Value.ToString();
                    sItem[10] = this.m_objViewer.dtgItem.Rows[i].Cells["sbtotal"].Value.ToString();
                    sItem[11] = this.m_objViewer.dtgItem.Rows[i].Cells["applyareaid"].Value.ToString();
                    sItem[12] = this.m_objViewer.dtgItem.Rows[i].Cells["applydoctorid"].Value.ToString();
                    sItem[13] = this.m_objViewer.dtgItem.Rows[i].Cells["execareaid"].Value.ToString();
                    sItem[14] = this.m_objViewer.dtgItem.Rows[i].Cells["flag"].Value.ToString();  //0 �½� 1 ��ʷ��¼
                    sItem[15] = this.m_objViewer.dtgItem.Rows[i].Cells["invocat"].Value.ToString();
                    sItem[16] = this.m_objViewer.dtgItem.Rows[i].Cells["orderid"].Value.ToString();
                    sItem[17] = "0";
                    sItem[18] = "0";
                    sItem[19] = "0";
                    sItem[20] = this.m_objViewer.dtgItem.Rows[i].Cells["rljels"].Value.ToString(); ;
                    int row = this.m_objViewer.dtgOrderItem.Rows.Add(sItem);
                    this.m_objViewer.dtgOrderItem.Rows[row].Tag = this.m_objViewer.dtgItem.Rows[i].Tag;

                    //if (Math.IEEERemainder(Convert.ToDouble(rowno), 2) == 0)
                    if (Math.IEEERemainder(rowno, 2) == 0)
                    {
                        this.m_objViewer.dtgOrderItem.Rows[row].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                    }

                    rowno++;
                }
            }
        }
        #endregion

        #region ��ӡ����������������嵥
        /// <summary>
        /// ��ӡ����������������嵥
        /// </summary>
        public void m_mthPrintExtraCharge()
        {
            string strName = this.m_objViewer.ucPatientInfo.lblName.Text;
            string strSex = this.m_objViewer.ucPatientInfo.lblSex.Text;
            string strAge = this.m_objViewer.ucPatientInfo.lblAge.Text;
            string strDept = this.m_objViewer.ucPatientInfo.txtArea.Text;
            string strBedNo = this.m_objViewer.ucPatientInfo.txtBedno.Text;
            string strInPatientNo = this.m_objViewer.ucPatientInfo.txtZyh.Text;
            string strRgno = this.m_objViewer.ucPatientInfo.RegisterID;

            DataStore dsPrint = new DataStore();
            dsPrint.LibraryList = clsPublic.PBLPath;
            dsPrint.DataWindowObject = "d_bih_op_extracharge";

            DataTable dtResult = new DataTable();
            clsDcl_Charge objDcl = new clsDcl_Charge();
            objDcl.m_lngQueryOpExtraChargeByRgno(strRgno, out dtResult);
            DataView dvTemp = dtResult.DefaultView;
            dvTemp.Sort = "invcateid_chr";
            dtResult = dvTemp.ToTable();
            dtResult.Columns.Add("index");
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                dtResult.Rows[i]["index"] = i;
            }

            #region ��ȡ�嵥��ͷ��Ϣ��������ȷ�ϴ��ڽ���ж��Ƿ���±����¼��
            //���޳�û��ѡ�е��շѼ�¼
            DataTable dtANA = new DataTable();
            objDcl.m_lngQuerySMDetailByRgno(strRgno, out dtANA);
            string strOpreationName = "";
            string strANAName = "";
            string strANADate = "";
            if (dtANA != null && dtANA.Rows.Count > 0)
            {
                strOpreationName = dtANA.Rows[0]["operationname_chr"].ToString();
                strANAName = dtANA.Rows[0]["anamode_chr"].ToString();
                strANADate = dtANA.Rows[0]["anatime"].ToString();
            }

            frmPrintSMListConfirm frmDialog = new frmPrintSMListConfirm(strOpreationName, strANAName, strANADate, ref dtResult);
            frmDialog.ShowDialog();
            if (frmDialog.DialogResult == DialogResult.Cancel)
            {
                return;
            }
            if (frmDialog.DialogResult == DialogResult.OK)
            {
                strOpreationName = frmDialog.strOpreationName;
                strANAName = frmDialog.strANAName;
                strANADate = frmDialog.strANADate;
                string strEmployID = this.m_objViewer.LoginInfo.m_strEmpID;
                string strEmployName = this.m_objViewer.LoginInfo.m_strEmpName;
                objDcl.m_lngUpdateRequisitionMR(strRgno, strOpreationName, strANAName, strANADate, strEmployID, strEmployName);
            }

            #endregion

            dsPrint.Modify("t_name.text = '" + strName + "'");
            dsPrint.Modify("t_sex.text = '" + strSex + "'");
            dsPrint.Modify("t_age.text = '" + strAge + "'");
            dsPrint.Modify("t_area.text = '" + strDept + "'");
            dsPrint.Modify("t_bedno.text = '" + strBedNo + "'");
            dsPrint.Modify("t_ipno.text = '" + strInPatientNo + "'");
            if (strOpreationName.Length < 45)
            {
                dsPrint.Modify("t_ssmc.text = '" + strOpreationName + "'");
            }
            else
            {
                dsPrint.Modify("t_ssmc.text = '" + strOpreationName.Substring(0, 45) + "'");
                dsPrint.Modify("t_ssmc1.text = '" + strOpreationName.Substring(strOpreationName.Length - 45) + "'");
            }
            dsPrint.Modify("t_mzff.text = '" + strANAName + "'");
            dsPrint.Modify("t_mzsj.text = '" + strANADate + "'");

            dtResult = frmDialog.dtChargeItems;
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    int row = dsPrint.InsertRow(0);
                    dsPrint.SetItemString(row, "fylb", m_strConvertToChType(dtResult.Rows[i]["invcateid_chr"].ToString()));
                    dsPrint.SetItemString(row, "xm", dtResult.Rows[i]["chargeitemname_chr"].ToString());
                    dsPrint.SetItemString(row, "gg", dtResult.Rows[i]["itemspec_vchr"].ToString());
                    dsPrint.SetItemDecimal(row, "je", Convert.ToDecimal(dtResult.Rows[i]["totalmoney_dec"]));
                    dsPrint.SetItemString(row, "dw", dtResult.Rows[i]["unit_vchr"].ToString());
                    dsPrint.SetItemDecimal(row, "sl", Convert.ToDecimal(dtResult.Rows[i]["amount_dec"]));
                    dsPrint.SetItemString(row, "yf", dtResult.Rows[i]["usagename_vchr"].ToString());
                }
            }

            clsPublic.PrintDialog(dsPrint);
        }
        #endregion
    }
}
