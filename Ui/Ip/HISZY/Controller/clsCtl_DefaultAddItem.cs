using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ����Ĭ�ϴ����շ���Ŀά������
    /// </summary>
    public class clsCtl_DefaultAddItem : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsCtl_DefaultAddItem()
        {
            objSvc = new clsDcl_AidDictionary();
            objSvcCharge = new clsDcl_Charge();
        }
        #endregion

        #region ����
        /// <summary>
        /// GUI����
        /// </summary>
        private com.digitalwave.iCare.gui.HIS.frmDefaultAddItem m_objViewer;
        /// <summary>
        /// Domain��
        /// </summary>
        private clsDcl_AidDictionary objSvc;
        /// <summary>
        /// Domain��
        /// </summary>
        private clsDcl_Charge objSvcCharge;
        /// <summary>
        /// ְ��DATATABLE
        /// </summary>
        private DataTable dtDuty;
        /// <summary>
        /// ְ��HashTable
        /// </summary>
        private Hashtable hasDuty = new Hashtable();
        /// <summary>
        /// �շ���Ŀ��Ʊ����
        /// </summary>
        private DataTable dtChargeItemCat;
        /// <summary>
        /// �Ƿ��޸ı�־
        /// </summary>
        internal bool IsModify = false;
        /// <summary>
        /// ��¼ID
        /// </summary>
        internal string RecordID = "";
        /// <summary>
        /// �����б�
        /// </summary>
        private DataTable dtbDepts;
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmDefaultAddItem)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ȡ�շ���Ŀ��Ʊ����
        /// <summary>
        /// ��ȡ�շ���Ŀ��Ʊ����
        /// </summary>
        public void m_mthGetCat()
        {
            long l = this.objSvcCharge.m_lngGetChargeItemCat(4, out dtChargeItemCat);
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

        #region ���ڳ�ʼ��
        /// <summary>
        /// ���ڳ�ʼ��
        /// </summary>
        public void m_mthInit()
        {
            #region ��ȡ���(�ѱ�)�б�ְ���б�
            DataTable dt;
            long l = this.objSvcCharge.m_lngGetPayTypeInfo(out dt);
            if (l > 0)
            {
                int row = 0;
                DataRow dr;

                string[] sarr = new string[4];
                sarr[0] = "0000";
                sarr[1] = "����";
                sarr[2] = "����";
                sarr[3] = "0000";
                this.m_objViewer.dtgPayType.Rows.Add(sarr);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    
                    sarr[0] = dr["paytypeno_vchr"].ToString().Trim();
                    sarr[1] = dr["paytypename_vchr"].ToString().Trim();
                    if (dr["isusing_num"].ToString() == "1")
                    {
                        sarr[2] = "����";
                    }
                    else
                    {
                        sarr[2] = "ͣ��";
                    }
                    sarr[3] = dr["paytypeid_chr"].ToString().Trim();

                    row = this.m_objViewer.dtgPayType.Rows.Add(sarr);                    

                    if (sarr[2] == "ͣ��")
                    {
                        this.m_objViewer.dtgPayType.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                    }

                    if (Math.IEEERemainder(Convert.ToDouble(i), 2) == 0)
                    {
                        this.m_objViewer.dtgPayType.Rows[row].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                    }
                }

                l = this.objSvc.m_lngGetDoctorDuty(out dtDuty);
                if (l > 0)
                {
                    this.m_objViewer.cboDuty.Items.Add("ȫ��");                        
                    
                    for (int i = 0; i < dtDuty.Rows.Count; i++)
                    {                        
                        this.m_objViewer.cboDuty.Items.Add(dtDuty.Rows[i]["dictname_vchr"].ToString());                       
                    }
                }
            }
            #endregion 

            this.m_objViewer.cboRegStatus.SelectedIndex = 0;
            this.m_objViewer.cboRecipeType.SelectedIndex = 0;
            this.m_objViewer.cboDuty.SelectedIndex = 0;

            this.m_objViewer.mskBeginTime.Text = "00:00";
            this.m_objViewer.mskEndTime.Text = "23:59";
            this.m_objViewer.panelItem.Height = 0;

            this.m_objViewer.btnSave.Tag = "";

            this.m_mthGetCat();
            //���ؿ���
            objSvcCharge.m_lngGetDepts(out dtbDepts);
        }
        #endregion

        #region �½�
        /// <summary>
        /// �½�
        /// </summary>
        public void m_mthNew()
        {            
            if (this.m_objViewer.CurrRow >= 0)
            {
                this.m_objViewer.dtgPayType.Rows[this.m_objViewer.CurrRow].Selected = false;
            }

            this.m_objViewer.btnSave.Tag = "";

            this.m_mthClear();
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
            this.m_objViewer.mskAmount.Text = "";

            this.m_objViewer.cboRegStatus.SelectedIndex = 0;
            this.m_objViewer.cboRecipeType.SelectedIndex = 0;
            this.m_objViewer.cboDuty.SelectedIndex = 0;

            this.m_objViewer.mskBeginTime.Text = "00:00";
            this.m_objViewer.mskEndTime.Text = "23:59";
                        
            this.m_objViewer.lblPrice.Text = "";
            this.m_objViewer.lblStandard.Text = "";
            this.m_objViewer.lblUnit.Text = "";

            this.m_objViewer.panelItem.Height = 0;

            this.m_objViewer.txtItemName.Focus();           
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

            string PayType = this.m_objViewer.PayTypeID.Trim();
            if (PayType == "")
            {
                return;
            }

            if (PayType == "0000")
            {
                PayType = "0001";
            }

            this.m_objViewer.lsvItem.BeginUpdate();
            this.m_objViewer.lsvItem.Items.Clear();

            DataTable dt;

            long l = this.objSvcCharge.m_lngFindChargeItem(FindStr, PayType, out dt, false);
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

            if (this.m_objViewer.lsvItem.Items.Count > 0)
            {
                this.m_objViewer.panelItem.Height = 200;
                this.m_objViewer.lsvItem.Items[0].Selected = true;
                this.m_objViewer.lsvItem.Focus();
            }

            this.m_objViewer.lsvItem.EndUpdate();
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

            //�������Ŀ
            this.m_objViewer.txtItemName.Text = ItemName;
            this.m_objViewer.lblStandard.Text = ItemSpe;
            this.m_objViewer.lblUnit.Text = ItemUnit;
            this.m_objViewer.lblPrice.Text = ItemPrice;           

            #endregion

            this.m_objViewer.txtItemName.Tag = dr;

            this.m_objViewer.mskAmount.Focus();
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

            if (this.m_objViewer.mskAmount.Text.Trim() == "" || this.m_objViewer.mskAmount.Text.Trim() == "0")
            {
                MessageBox.Show("������������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.mskAmount.Focus();
                return;
            }

            string BeginTime = this.m_objViewer.mskBeginTime.Text.Trim();
            string EndTime = this.m_objViewer.mskEndTime.Text.Trim();

            if (BeginTime == "" || BeginTime.Length != 5)
            {
                MessageBox.Show("��ʼʱ�䲻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.mskBeginTime.Focus();
                return;
            }

            if (EndTime == "" || EndTime.Length != 5)
            {
                MessageBox.Show("����ʱ�䲻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.mskEndTime.Focus();
                return;
            }

            DataRow dr = this.m_objViewer.txtItemName.Tag as DataRow;

            string[] sarr = new string[13];

            sarr[0] = Convert.ToString(this.m_objViewer.dtgItem.Rows.Count + 1);
            sarr[1] = dr["itemname_vchr"].ToString().Trim();
            sarr[2] = dr["itemspec_vchr"].ToString().Trim();

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
            sarr[3] = ItemUnit;
            sarr[4] = this.m_objViewer.mskAmount.Text.Trim();
            sarr[5] = ItemPrice;
            decimal d = clsPublic.ConvertObjToDecimal(ItemPrice) * clsPublic.ConvertObjToDecimal(this.m_objViewer.mskAmount.Text);
            sarr[6] = d.ToString("0.00");

            sarr[7] = this.m_objViewer.cboRegStatus.Text;
            sarr[8] = this.m_objViewer.cboRecipeType.Text;
            sarr[9] = this.m_objViewer.cboDuty.Text;
            sarr[10] = this.m_objViewer.mskBeginTime.Text.Trim() + "~" + this.m_objViewer.mskEndTime.Text.Trim();
            if (this.m_objViewer.txtDept.Text != "" && this.m_objViewer.txtDept.Tag != null)
            {
                sarr[11] = this.m_objViewer.txtDept.Text.Trim();
                sarr[12] = this.m_objViewer.txtDept.Tag.ToString();
            }
            else
            {
                sarr[11] = "��ָ��";
                sarr[12] = "0";
            }
            int row = this.m_objViewer.dtgItem.Rows.Add(sarr);
            this.m_objViewer.dtgItem.Rows[row].Tag = dr;            
            this.m_objViewer.CurrItemRow = row;

            if (Math.IEEERemainder(Convert.ToDouble(row), 2) == 0)
            {
                this.m_objViewer.dtgItem.Rows[row].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
            }

            this.m_mthClear();
            IsModify = true;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public bool m_blnSave()
        {
            bool ret = false;

            if (this.m_objViewer.dtgItem.Rows.Count == 0)
            {
                return ret;
            }

            if (this.m_objViewer.PayTypeID.Trim() == "")
            {
                return ret;
            }

            RecordID = this.m_objViewer.btnSave.Tag.ToString().Trim();

            if (RecordID == "" && this.m_objViewer.dtgItem.Rows.Count == 0)
            {
                return ret;
            }

            List<clsOutPatientDefaultAddItem_VO> RecordsArr = new List<clsOutPatientDefaultAddItem_VO>();
            int SaveFlag = 0;

            if (RecordID != "" && this.m_objViewer.dtgItem.Rows.Count == 0)
            {
                SaveFlag = -1;
            }
            else
            {
                string RegFlag = "";
                string RecFlag = "";
                string DutyName = "";
                string TimeScpe = "";

                DataRow dr;
                for (int i = 0; i < this.m_objViewer.dtgItem.Rows.Count; i++)
                {
                    dr = this.m_objViewer.dtgItem.Rows[i].Tag as DataRow;
                    clsOutPatientDefaultAddItem_VO DefaultAddItem_VO = new clsOutPatientDefaultAddItem_VO();

                    DefaultAddItem_VO.Sid = RecordID;
                    DefaultAddItem_VO.PayTypeID = this.m_objViewer.PayTypeID;

                    DefaultAddItem_VO.ItemID = dr["itemid_chr"].ToString();
                    DefaultAddItem_VO.Qty = clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgItem.Rows[i].Cells["sl"].Value);

                    RegFlag = this.m_objViewer.dtgItem.Rows[i].Cells["ghzt"].Value.ToString().Trim();
                    RecFlag = this.m_objViewer.dtgItem.Rows[i].Cells["cflx"].Value.ToString().Trim();
                    DutyName = this.m_objViewer.dtgItem.Rows[i].Cells["yszc"].Value.ToString().Trim();
                    TimeScpe = this.m_objViewer.dtgItem.Rows[i].Cells["sjfw"].Value.ToString().Trim();

                    if (RegFlag == "ȫ��")
                    {
                        RegFlag = "0";
                    }
                    else if (RegFlag == "�ѹҺ�")
                    {
                        RegFlag = "1";
                    }
                    else if (RegFlag == "δ�Һ�")
                    {
                        RegFlag = "2";
                    }
                    DefaultAddItem_VO.RegFlag = int.Parse(RegFlag);

                    if (RecFlag == "ȫ��")
                    {
                        RecFlag = "0";
                    }
                    else if (RecFlag == "����")
                    {
                        RecFlag = "1";
                    }
                    else if (RecFlag == "����")
                    {
                        RecFlag = "2";
                    }
                    DefaultAddItem_VO.RecFlag = int.Parse(RecFlag);
                                      
                    if (DutyName.Trim() == "")
                    {
                        MessageBox.Show("ְ�Ʋ���Ϊ�ա�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    DefaultAddItem_VO.DutyID = DutyName;

                    DefaultAddItem_VO.BeginTime = TimeScpe.Substring(0, 5);
                    DefaultAddItem_VO.EndTime = TimeScpe.Substring(6, 5);
                    DefaultAddItem_VO.DeptID = this.m_objViewer.dtgItem.Rows[i].Cells["ksid"].Value.ToString().Trim();
                    RecordsArr.Add(DefaultAddItem_VO);
                }
            }

            long l = this.objSvc.m_lngSaveOutPatientDefaultAddItem(RecordsArr, SaveFlag, RecordID);
            if (l > 0)
            {
                MessageBox.Show("���ݱ���ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.m_objViewer.btnSave.Tag = this.m_objViewer.PayTypeID;
                ret = true;
            }
            else
            {
                MessageBox.Show("���ݱ���ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }       
        #endregion

        #region ��ȡ��ʷ��¼
        /// <summary>
        /// ��ȡ��ʷ��¼
        /// </summary>
        public void m_mthGetHistoryItem()
        {
            this.m_objViewer.btnSave.Tag = ""; 

            if (this.m_objViewer.PayTypeID.Trim() == "")
            {                                
                return;
            }

            this.m_objViewer.dtgItem.Rows.Clear();

            DataTable dt;
            long l = this.objSvc.m_lngGetOutPatientDefaultAddItem(out dt, this.m_objViewer.PayTypeID );
            if (l > 0 && dt.Rows.Count > 0)
            {
                DataRow dr;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];

                    string[] sarr = new string[13];

                    sarr[0] = Convert.ToString(i + 1);
                    sarr[1] = dr["itemname_vchr"].ToString().Trim();
                    sarr[2] = dr["itemspec_vchr"].ToString().Trim();

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
                    sarr[3] = ItemUnit;
                    sarr[4] = dr["qty_dec"].ToString().Trim();
                    sarr[5] = ItemPrice;
                    decimal d = clsPublic.ConvertObjToDecimal(ItemPrice) * clsPublic.ConvertObjToDecimal(dr["qty_dec"]);
                    sarr[6] = d.ToString("0.00");

                    if (dr["regflag_int"].ToString() == "0")
                    {
                        sarr[7] = "ȫ��";
                    }
                    else if (dr["regflag_int"].ToString() == "1")
                    {
                        sarr[7] = "�ѹҺ�";
                    }
                    else if (dr["regflag_int"].ToString() == "2")
                    {
                        sarr[7] = "δ�Һ�";
                    }                    

                    if (dr["recflag_int"].ToString() == "0")
                    {
                        sarr[8] = "ȫ��";
                    }
                    else if (dr["recflag_int"].ToString() == "1")
                    {
                        sarr[8] = "����";
                    }
                    else if (dr["recflag_int"].ToString() == "2")
                    {
                        sarr[8] = "����";
                    }

                    sarr[9] = dr["dutyname_vchr"].ToString();
                    sarr[10] = dr["begintime_chr"].ToString() + "~" + dr["endtime_chr"].ToString();
                    if (dr["deptid_chr"].ToString().Trim() != "0")
                    {
                        DataRow[] tempdr = dtbDepts.Select("deptid_chr='" + dr["deptid_chr"].ToString() + "'");
                        sarr[11] = tempdr[0]["deptname_vchr"].ToString();
                        sarr[12] = dr["deptid_chr"].ToString();
                    }
                    else
                    {
                        sarr[11] = "��ָ��";
                        sarr[12] = "0";
                    }

                    int row = this.m_objViewer.dtgItem.Rows.Add(sarr);
                    this.m_objViewer.dtgItem.Rows[row].Tag = dr;
                    this.m_objViewer.CurrItemRow = row;

                    if (Math.IEEERemainder(Convert.ToDouble(row), 2) == 0)
                    {
                        this.m_objViewer.dtgItem.Rows[row].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                    }                    
                }

                this.m_mthClear();
                this.m_objViewer.btnSave.Tag = this.m_objViewer.PayTypeID;
            }
        }
        #endregion

        #region �༭
        /// <summary>
        /// �༭
        /// </summary>
        /// <param name="row"></param>
        public void m_mthModify(int row)
        {
            clsParmItem_VO Item_VO = new clsParmItem_VO();                     
                        
            Item_VO.ItemName = this.m_objViewer.dtgItem.Rows[row].Cells["xmmc"].Value.ToString();
            Item_VO.ItemAmout = clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgItem.Rows[row].Cells["sl"].Value);

            Item_VO.RegFlag = this.m_objViewer.dtgItem.Rows[row].Cells["ghzt"].Value.ToString();
            Item_VO.RecFlag = this.m_objViewer.dtgItem.Rows[row].Cells["cflx"].Value.ToString();
            Item_VO.DutyName = this.m_objViewer.dtgItem.Rows[row].Cells["yszc"].Value.ToString();

            string TimeScope = this.m_objViewer.dtgItem.Rows[row].Cells["sjfw"].Value.ToString();
            Item_VO.BeginTime = TimeScope.Substring(0, 5);
            Item_VO.EndTime = TimeScope.Substring(6, 5);
            Item_VO.DeptID = this.m_objViewer.dtgItem.Rows[row].Cells["ksid"].Value.ToString();
            Item_VO.DeptName = this.m_objViewer.dtgItem.Rows[row].Cells["ks"].Value.ToString();

            frmAidEditItem_OutPatientDefault fEdit = new frmAidEditItem_OutPatientDefault(ref Item_VO, dtDuty);
            fEdit.dtbDeptsSource = dtbDepts;
            if (fEdit.ShowDialog() == DialogResult.OK)
            {
                decimal d = Item_VO.ItemAmout;
                this.m_objViewer.dtgItem.Rows[row].Cells["sl"].Value = d.ToString();
                d = d * clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgItem.Rows[row].Cells["dj"].Value);
                this.m_objViewer.dtgItem.Rows[row].Cells["hj"].Value = d.ToString("0.00");

                this.m_objViewer.dtgItem.Rows[row].Cells["ghzt"].Value = Item_VO.RegFlag;
                this.m_objViewer.dtgItem.Rows[row].Cells["cflx"].Value = Item_VO.RecFlag;
                this.m_objViewer.dtgItem.Rows[row].Cells["yszc"].Value = Item_VO.DutyName;

                this.m_objViewer.dtgItem.Rows[row].Cells["sjfw"].Value = Item_VO.BeginTime + "~" + Item_VO.EndTime;
                this.m_objViewer.dtgItem.Rows[row].Cells["ks"].Value = Item_VO.DeptName;
                this.m_objViewer.dtgItem.Rows[row].Cells["ksid"].Value = Item_VO.DeptID;
            }
        }
        #endregion

        #region ���ݹؼ��ֲ��ҿ��ң����ŵ�datagridview�У���������ƴ���룬����룩
        public void m_mthSearchDept(string key)
        {
            DataView dv = dtbDepts.DefaultView;
            dv.RowFilter = "deptname_vchr like '%" + key + "%' or wbcode_chr like '%" + key + "%' or pycode_chr like '%" + key + "%' or usercode_vchr like '%" + key + "%'";
            DataTable dt = dv.ToTable();
            this.m_objViewer.dgvDept.DataSource = dt;
        }
        #endregion
    }
}
