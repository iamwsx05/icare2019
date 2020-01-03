using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.iCare.gui.Security;
using System.Data;
using System.Collections;
using com.digitalwave.iCare.middletier.HI;
using weCare.Core.Utils;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsControlChargeCheck ��ժҪ˵����
    /// </summary>
    public class clsControlChargeCheck : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsControlChargeCheck()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ���ô������
        public com.digitalwave.iCare.gui.HIS.frmChargeCheck m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmChargeCheck)frmMDI_Child_Base_in;
        }
        #endregion

        #region ����
        clsDomainControl_Register Domain = new clsDomainControl_Register();
        /// <summary>
        /// ���淵�صķ�Ʊ��Ϣ
        /// </summary>
        internal DataTable dtChargeCheck = null;
        /// <summary>
        /// ������������ķ�Ʊ��Ϣ
        /// </summary>
        DataTable dtFindCharge = null;
        /// <summary>
        /// �Ƿ���������,1 ����,0 ������
        /// ��������
        /// </summary>
        internal int intDiffPriceOn = 0;

        #endregion

        #region ��ʼ������
        private System.Data.DataView m_dvRegister = new System.Data.DataView();
        public void m_frmLoad()
        {

            decimal totalACCTSUM = 0;
            decimal totalSBSUM = 0;
            decimal totalSUM = 0;
            string strDateStart = this.m_objViewer.m_datFirstdate.Value.ToShortDateString();
            string strDateEnd = this.m_objViewer.m_datLastdate.Value.ToShortDateString();
            if (this.m_objViewer.Scope == "0")
            {
                Domain.m_lngGetChargeByDate(strDateStart, strDateEnd, out dtChargeCheck, this.m_objViewer.blnOnlySelectVip, this.m_objViewer.chkWechatRePrt.Checked);
            }
            else if (this.m_objViewer.Scope == "1")
            {
                Domain.m_lngGetChargeByempid(strDateStart, strDateEnd, this.m_objViewer.LoginInfo.m_strEmpID, out dtChargeCheck, this.m_objViewer.blnOnlySelectVip, this.m_objViewer.chkWechatRePrt.Checked);
            }
            intDiffPriceOn = clsPublic.m_intGetSysParm("9002"); // �������ÿ���
            #region �ı�������
            int n = -1;
            dtChargeCheck.Columns[++n].ColumnName = "���ƿ���";
            dtChargeCheck.Columns[++n].ColumnName = "��Ʊ���";
            dtChargeCheck.Columns[++n].ColumnName = "�ش�Ʊ��";
            dtChargeCheck.Columns[++n].ColumnName = "��ˮ��";
            dtChargeCheck.Columns[++n].ColumnName = "�������";
            dtChargeCheck.Columns[++n].ColumnName = "��������";
            dtChargeCheck.Columns[++n].ColumnName = "�Ա�";
            dtChargeCheck.Columns[++n].ColumnName = "֧������";
            dtChargeCheck.Columns[++n].ColumnName = "��Ʊ����";
            dtChargeCheck.Columns[++n].ColumnName = "��Ʊ״̬";
            dtChargeCheck.Columns[++n].ColumnName = "��������";
            dtChargeCheck.Columns[++n].ColumnName = "ҽ������";
            dtChargeCheck.Columns[++n].ColumnName = "�ɷ�״̬";
            dtChargeCheck.Columns[++n].ColumnName = "��¼ʱ��";
            dtChargeCheck.Columns[++n].ColumnName = "�շ�Ա";
            dtChargeCheck.Columns[++n].ColumnName = "����Ա";
            dtChargeCheck.Columns[++n].ColumnName = "���ʽ��";
            dtChargeCheck.Columns[++n].ColumnName = "�Ը����";
            dtChargeCheck.Columns[++n].ColumnName = "�ϼƽ��";
            dtChargeCheck.Columns[++n].ColumnName = "������";
            ++n;
            dtChargeCheck.Columns[++n].ColumnName = "������λ";
            // ���������,ʵ�����
            if (intDiffPriceOn == 1)
            {
                dtChargeCheck.Columns[++n].ColumnName = "ҩƷ������";
                dtChargeCheck.Columns[++n].ColumnName = "ʵ�����";
            }

            #endregion
            if (dtChargeCheck.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtChargeCheck.Rows.Count; i1++)
                {
                    totalACCTSUM += Convert.ToDecimal(dtChargeCheck.Rows[i1]["���ʽ��"].ToString());
                    totalSBSUM += Convert.ToDecimal(dtChargeCheck.Rows[i1]["�Ը����"].ToString());
                    if (dtChargeCheck.Rows[i1]["�ϼƽ��"].ToString() != string.Empty)
                    {
                        totalSUM += Convert.ToDecimal(dtChargeCheck.Rows[i1]["�ϼƽ��"].ToString());
                    }
                    else
                    {
                        totalSUM += 0;
                    }
                }
                DataRow newRow = dtChargeCheck.NewRow();
                newRow["���ƿ���"] = "�ܷ�Ʊ��";
                newRow["��Ʊ���"] = dtChargeCheck.Rows.Count.ToString();
                newRow["����Ա"] = "�ܽ��";
                newRow["���ʽ��"] = totalACCTSUM;
                newRow["�Ը����"] = totalSBSUM;
                newRow["�ϼƽ��"] = totalSUM;
                dtChargeCheck.Rows.Add(newRow);
            }
            this.m_dvRegister = dtChargeCheck.DefaultView;
            this.m_objViewer.DgChargeCheck.SetDataBinding(m_dvRegister, null);
            this.m_objViewer.DgChargeCheck.Tag = "dtChargeCheck";
            this.m_objViewer.DgChargeCheck.RowHeaderWidth = 10;
            this.m_objViewer.DgChargeCheck.m_SetDgrStyle();
            this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["��Ʊ����"].Width += 6;
            this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["���ƿ���"].Width = 80;
            this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["��ˮ��"].Width = 100;
            this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["��¼ʱ��"].Width = 120;
            this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["�Ա�"].Width = 40;
            this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["��������"].Width = 120;
            this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["�շ�Ա"].Width = 60;
            this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["����Ա"].Width = 60;
            this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["��Ʊ���"].Width = 80;
            this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["������λ"].Width = 0;
            this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["�ش�Ʊ��"].Width = 80;
            if (intDiffPriceOn == 1)
            {
                this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["ҩƷ������"].Width = 90;
            }
            #region ���Combox
            if (this.m_objViewer.m_cboFildName.Items.Count == 0)
            {
                int j6 = 0;
                m_objViewer.m_cboSub1.Items.Add("");
                m_objViewer.m_cboSub2.Items.Add("");
                foreach (DataColumn dc in dtChargeCheck.Columns)
                {
                    j6++;
                    if (dc.ColumnName.IndexOf("ID") >= 0)
                    {
                        continue;
                    }
                    if (dc.ColumnName == "��ˮ��" || dc.ColumnName == "�Ա�" || dc.ColumnName == "���ʽ��" || dc.ColumnName == "�Ը����" || dc.ColumnName == "�ϼƽ��")
                        continue;
                    else if (j6 <= 20)
                    {
                        this.m_objViewer.m_cboFildName.Items.Add(dc.ColumnName);
                        this.m_objViewer.m_cboSub1.Items.Add(dc.ColumnName);
                        this.m_objViewer.m_cboSub2.Items.Add(dc.ColumnName);
                    }
                    if (this.m_objViewer.m_cboFildName.Items.Count > 0)
                    {
                        this.m_objViewer.m_cboFildName.SelectedIndex = 0;
                        this.m_objViewer.m_cboSub1.SelectedIndex = 0;
                        this.m_objViewer.m_cboSub2.SelectedIndex = 0;
                    }
                }
            }
            #endregion
        }
        #endregion
        #region �����ֶβ�ѯ����
        //public void m_mthGetChargeInfoByField(string[] m_strArr)
        //{
        //    decimal totalACCTSUM = 0;
        //    decimal totalSBSUM = 0;
        //    decimal totalSUM = 0;
        //    string strDateStart = this.m_objViewer.m_datFirstdate.Value.ToShortDateString();
        //    string strDateEnd = this.m_objViewer.m_datLastdate.Value.ToShortDateString();
        //    if (this.m_objViewer.Scope == "0")
        //    {
        //       // Domain.m_lngGetChargeByDate(strDateStart, strDateEnd, out dtChargeCheck);
        //        Domain.m_lngGetChargeByCondition(m_strArr, out dtChargeCheck);
        //    }
        //    else if (this.m_objViewer.Scope == "1")
        //    {
        //       // Domain.m_lngGetChargeByempid(strDateStart, strDateEnd, this.m_objViewer.LoginInfo.m_strEmpID, out dtChargeCheck);'
        //        Domain.m_lngGetChargeByEmpid(m_strArr, this.m_objViewer.LoginInfo.m_strEmpID, out dtChargeCheck);
        //    }
        //    #region �ı�������
        //    dtChargeCheck.Columns[0].ColumnName = "���ƿ���";
        //    dtChargeCheck.Columns[1].ColumnName = "��Ʊ���";
        //    dtChargeCheck.Columns[2].ColumnName = "��ˮ��";
        //    dtChargeCheck.Columns[3].ColumnName = "�������";
        //    dtChargeCheck.Columns[4].ColumnName = "��������";
        //    dtChargeCheck.Columns[5].ColumnName = "�Ա�";
        //    dtChargeCheck.Columns[6].ColumnName = "֧������";
        //    dtChargeCheck.Columns[7].ColumnName = "��Ʊ����";
        //    dtChargeCheck.Columns[8].ColumnName = "��Ʊ״̬";
        //    dtChargeCheck.Columns[9].ColumnName = "��������";
        //    dtChargeCheck.Columns[10].ColumnName = "ҽ������";
        //    dtChargeCheck.Columns[11].ColumnName = "�ɷ�״̬";
        //    dtChargeCheck.Columns[12].ColumnName = "��¼ʱ��";
        //    dtChargeCheck.Columns[13].ColumnName = "�շ�Ա";
        //    dtChargeCheck.Columns[14].ColumnName = "����Ա";
        //    dtChargeCheck.Columns[15].ColumnName = "���ʽ��";
        //    dtChargeCheck.Columns[16].ColumnName = "�Ը����";
        //    dtChargeCheck.Columns[17].ColumnName = "�ϼƽ��";
        //    dtChargeCheck.Columns[20].ColumnName = "������λ";

        //    #endregion
        //    if (dtChargeCheck.Rows.Count > 0)
        //    {
        //        for (int i1 = 0; i1 < dtChargeCheck.Rows.Count; i1++)
        //        {
        //            totalACCTSUM += Convert.ToDecimal(dtChargeCheck.Rows[i1]["���ʽ��"].ToString());
        //            totalSBSUM += Convert.ToDecimal(dtChargeCheck.Rows[i1]["�Ը����"].ToString());
        //            totalSUM += Convert.ToDecimal(dtChargeCheck.Rows[i1]["�ϼƽ��"].ToString());
        //        }
        //        DataRow newRow = dtChargeCheck.NewRow();
        //        newRow["���ƿ���"] = "�ܷ�Ʊ��";
        //        newRow["��Ʊ���"] = dtChargeCheck.Rows.Count.ToString();
        //        newRow["����Ա"] = "�ܽ��";
        //        newRow["���ʽ��"] = totalACCTSUM;
        //        newRow["�Ը����"] = totalSBSUM;
        //        newRow["�ϼƽ��"] = totalSUM;
        //        dtChargeCheck.Rows.Add(newRow);
        //    }
        //    this.m_dvRegister = dtChargeCheck.DefaultView;
        //    this.m_objViewer.DgChargeCheck.SetDataBinding(m_dvRegister, null);
        //    this.m_objViewer.DgChargeCheck.Tag = "dtChargeCheck";
        //    this.m_objViewer.DgChargeCheck.RowHeaderWidth = 10;
        //    this.m_objViewer.DgChargeCheck.m_SetDgrStyle();
        //    this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["���ƿ���"].Width = 80;
        //    this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["��ˮ��"].Width = 100;
        //    this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["��¼ʱ��"].Width = 120;
        //    this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["�Ա�"].Width = 40;
        //    this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["��������"].Width = 120;
        //    this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["�շ�Ա"].Width = 60;
        //    this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["����Ա"].Width = 60;
        //    this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["��Ʊ���"].Width = 80;
        //    this.m_objViewer.DgChargeCheck.TableStyles[0].GridColumnStyles["������λ"].Width = 0;
        //    #region ���Combox
        //    if (this.m_objViewer.m_cboFildName.Items.Count == 0)
        //    {
        //        int j6 = 0;
        //        foreach (DataColumn dc in dtChargeCheck.Columns)
        //        {
        //            j6++;
        //            if (dc.ColumnName.IndexOf("ID") >= 0)
        //            {
        //                continue;
        //            }
        //            if (dc.ColumnName == "��ˮ��" || dc.ColumnName == "�Ա�" || dc.ColumnName == "���ʽ��" || dc.ColumnName == "�Ը����" || dc.ColumnName == "�ϼƽ��")
        //                continue;
        //            else if (j6 <= 20)
        //                this.m_objViewer.m_cboFildName.Items.Add(dc.ColumnName);
        //            if (this.m_objViewer.m_cboFildName.Items.Count > 0)
        //                this.m_objViewer.m_cboFildName.SelectedIndex = 0;
        //        }
        //    }
        //    #endregion
        //}
        #endregion

        #region ��������
        DataView m_dvRegisterfind = new DataView();

        public void m_mthFindCharge()
        {
            if (this.m_objViewer.m_cboFildName.Text == "")
            {
                MessageBox.Show("��ѡ����Ҫ���ҵ��ֶΣ�", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.m_cboFildName.Focus();
                return;
            }
            if (this.m_objViewer.m_txtValuse.Text == "")
            {
                MessageBox.Show("��������Ҫ���ҵ����ݣ�", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.m_txtValuse.Focus();
                return;
            }
            try
            {
                dtFindCharge = dtChargeCheck.Clone();
            }
            catch
            {
            }
            dtFindCharge.Clear();
            for (int i1 = 0; i1 < dtChargeCheck.Rows.Count; i1++)
            {
                if (dtChargeCheck.Rows[i1][this.m_objViewer.m_cboFildName.Text].ToString().IndexOf(this.m_objViewer.m_txtValuse.Text.Trim(), 0) == 0
                    && (this.m_objViewer.m_cboSub1.SelectedIndex == 0 || dtChargeCheck.Rows[i1][this.m_objViewer.m_cboSub1.Text].ToString().IndexOf(this.m_objViewer.m_txtVal1.Text.Trim(), 0) == 0)
                    && (this.m_objViewer.m_cboSub2.SelectedIndex == 0 || dtChargeCheck.Rows[i1][this.m_objViewer.m_cboSub2.Text].ToString().IndexOf(this.m_objViewer.m_txtVal2.Text.Trim(), 0) == 0))
                {
                    DataRow AddRow = dtFindCharge.NewRow();
                    AddRow["���ƿ���"] = dtChargeCheck.Rows[i1]["���ƿ���"];
                    AddRow["��Ʊ���"] = dtChargeCheck.Rows[i1]["��Ʊ���"];
                    AddRow["��ˮ��"] = dtChargeCheck.Rows[i1]["��ˮ��"];
                    AddRow["�������"] = dtChargeCheck.Rows[i1]["�������"];
                    AddRow["��������"] = dtChargeCheck.Rows[i1]["��������"];
                    AddRow["�Ա�"] = dtChargeCheck.Rows[i1]["�Ա�"];
                    AddRow["֧������"] = dtChargeCheck.Rows[i1]["֧������"];
                    AddRow["��Ʊ����"] = dtChargeCheck.Rows[i1]["��Ʊ����"];
                    AddRow["��Ʊ״̬"] = dtChargeCheck.Rows[i1]["��Ʊ״̬"];
                    AddRow["��������"] = dtChargeCheck.Rows[i1]["��������"];
                    AddRow["ҽ������"] = dtChargeCheck.Rows[i1]["ҽ������"];
                    AddRow["�ɷ�״̬"] = dtChargeCheck.Rows[i1]["�ɷ�״̬"];
                    AddRow["��¼ʱ��"] = dtChargeCheck.Rows[i1]["��¼ʱ��"];
                    AddRow["�շ�Ա"] = dtChargeCheck.Rows[i1]["�շ�Ա"];
                    AddRow["����Ա"] = dtChargeCheck.Rows[i1]["����Ա"];
                    AddRow["���ʽ��"] = dtChargeCheck.Rows[i1]["���ʽ��"];
                    AddRow["�Ը����"] = dtChargeCheck.Rows[i1]["�Ը����"];
                    AddRow["�ϼƽ��"] = dtChargeCheck.Rows[i1]["�ϼƽ��"];
                    AddRow["������"] = dtChargeCheck.Rows[i1]["������"];
                    AddRow["�ش�Ʊ��"] = dtChargeCheck.Rows[i1]["�ش�Ʊ��"];

                    dtFindCharge.Rows.Add(AddRow);
                }
            }
            if (dtFindCharge.Rows.Count > 0)
            {
                double totalACCTSUM = 0.0000;
                double totalSBSUM = 0.0000;
                double totalSUM = 0.0000;
                for (int i1 = 0; i1 < dtFindCharge.Rows.Count; i1++)
                {
                    totalACCTSUM += Convert.ToDouble(dtFindCharge.Rows[i1]["���ʽ��"].ToString());
                    totalSBSUM += Convert.ToDouble(dtFindCharge.Rows[i1]["�Ը����"].ToString());
                    if (dtFindCharge.Rows[i1]["�ϼƽ��"].ToString() != string.Empty)
                    {
                        totalSUM += Convert.ToDouble(dtFindCharge.Rows[i1]["�ϼƽ��"].ToString());
                    }
                    else
                    {
                        totalSUM += 0;
                    }
                }
                DataRow newRow = dtFindCharge.NewRow();
                newRow["���ƿ���"] = "�ܷ�Ʊ��";
                newRow["��Ʊ���"] = dtFindCharge.Rows.Count.ToString();
                newRow["����Ա"] = "�ܽ��";
                newRow["���ʽ��"] = Double.Parse(totalACCTSUM.ToString("0.0000"));
                newRow["�Ը����"] = Double.Parse(totalSBSUM.ToString("0.0000"));
                newRow["�ϼƽ��"] = Double.Parse(totalSUM.ToString("0.0000"));
                dtFindCharge.Rows.Add(newRow);
                m_dvRegisterfind = dtFindCharge.DefaultView;
                this.m_objViewer.DgChargeCheck.SetDataBinding(m_dvRegisterfind, null);
                this.m_objViewer.DgChargeCheck.Tag = "dtFindCharge";
                //				this.m_objViewer.buttonXP1.Enabled=false;
            }
            else
            {
                this.m_objViewer.DgChargeCheck.SetDataBinding(null, null);
            }
        }
        #endregion

        #region �������еĴ������
        public string[] m_mthGetAll()
        {
            string[] strArry = null;
            if ((string)this.m_objViewer.DgChargeCheck.Tag == "dtFindCharge")
            {
                if (dtFindCharge.Rows.Count - 1 > 0)
                {
                    strArry = new string[dtFindCharge.Rows.Count - 1];
                    for (int i1 = 0; i1 < dtFindCharge.Rows.Count - 1; i1++)
                    {
                        strArry[i1] = dtFindCharge.Rows[i1]["������"].ToString();
                    }
                }
            }
            else
            {
                if (dtChargeCheck.Rows.Count - 1 > 0)
                {
                    strArry = new string[dtChargeCheck.Rows.Count - 1];
                    for (int i1 = 0; i1 < dtChargeCheck.Rows.Count - 1; i1++)
                    {
                        strArry[i1] = dtChargeCheck.Rows[i1]["������"].ToString();
                    }
                }
            }
            return strArry;
        }
        #endregion

        #region ��ʾ��Ʊ������ϸ����
        /// <summary>
        /// ��ʾ��Ʊ������ϸ����
        /// </summary>
        public void m_mthShowChargeDe()
        {
            if (dtChargeCheck.Rows.Count == 0 || this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber < 0)
            {
                return;
            }

            DataTable dtChargeDe = new DataTable();
            this.m_objViewer.LsvChargeDe.Items.Clear();

            string strINVOICENO = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 1].ToString();
            string strSEQID = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 3].ToString();

            if (strINVOICENO == "" || strSEQID == "")
                return;
            Domain.m_lngGetChargeDe(strINVOICENO, strSEQID, out dtChargeDe);
            double totalMoney = 0.00, decMoney = 0;

            if (dtChargeDe.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtChargeDe.Rows.Count; i1++)
                {
                    ListViewItem addItem = new ListViewItem(dtChargeDe.Rows[i1]["TYPENAME_VCHR"].ToString());
                    decMoney = Convert.ToDouble(dtChargeDe.Rows[i1]["TOLFEE_MNY"].ToString());
                    if (string.Compare("0022", dtChargeDe.Rows[i1]["typeid_chr"].ToString()) == 0)
                    {
                        decMoney = Math.Abs(decMoney) * -1;//ȡ��ֵ
                    }
                    totalMoney += decMoney;
                    addItem.SubItems.Add("��" + decMoney.ToString());
                    this.m_objViewer.LsvChargeDe.Items.Add(addItem);
                }
            }
            ListViewItem addItem1 = new ListViewItem("�ϼƽ��");
            addItem1.SubItems.Add("��" + totalMoney.ToString());
            this.m_objViewer.LsvChargeDe.Items.Add(addItem1);
            this.m_objViewer.LsvChargeDe.Visible = true;
            this.m_objViewer.listView1.Visible = false;
        }
        #endregion

        #region IsOver
        public bool m_blisOver()
        {
            bool Over = false;
            if (this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 14].ToString() == "�ѽ���")
            {
                Over = true;
            }
            return Over;
        }
        #endregion

        #region �޸ķ�Ʊ��֧������

        public void m_mthModifiyType()
        {
            if (dtChargeCheck.Rows.Count == 0 && this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber < 0)
                return;

            DataTable dtChargeDe = new DataTable();
            this.m_objViewer.LsvChargeDe.Items.Clear();
            bool IsOver = false;

            string strINVOICENO = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 1].ToString();
            string strSEQID = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 3].ToString();
            if (this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 12].ToString() == "�ѽ���")
            {
                IsOver = true;
            }

            if (strINVOICENO == "" || strSEQID == "" || IsOver)
                return;
            if (MessageBox.Show("�Ƿ�Ҫ�޸�֧�����ͣ�", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            long lngRes = Domain.m_lngModifiyType(this.m_objViewer.m_cobChang.SelectedIndex.ToString(), strINVOICENO, strSEQID, this.m_objViewer.LoginInfo.m_strEmpID);
            if ((string)this.m_objViewer.DgChargeCheck.Tag == "dtChargeCheck")
            {
                m_dvRegister[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["֧������"] = this.m_objViewer.m_cobChang.Text;
            }
            else
            {
                m_dvRegisterfind[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["֧������"] = this.m_objViewer.m_cobChang.Text; ;
            }
            this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 7] = this.m_objViewer.m_cobChang.Text; ;
        }
        #endregion

        #region ��ʾ������ϸ����

        public void m_mthShowRecipeDe()
        {
            if (dtChargeCheck.Rows.Count == 0 && this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber < 0)
                return;

            DataTable dtChargeDe = new DataTable();
            this.m_objViewer.LsvChargeDe.Items.Clear();

            string strSEQID = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 3].ToString();
            if (strSEQID == "")
                return;
            this.m_objViewer.listView1.Items.Clear();
            Domain.m_lngGetRecipeDate(strSEQID, out dtChargeDe);

            decimal MoneyAll = 0;
            string dblsbsum_mnyAll = "0";
            string dblACCTSUM_MNYAll = "0";
            if (dtChargeDe.Rows.Count > 0)
            {
                decimal totalMoney = 0;
                decimal buyPrice = 0;
                decimal price = 0;
                foreach (DataRow dr in dtChargeDe.Rows)
                {
                    totalMoney = 0;
                    ListViewItem addItem = new ListViewItem(dr["NAME"].ToString().Trim());
                    addItem.SubItems.Add(dr["DEC"].ToString().Trim());
                    addItem.SubItems.Add(dr["PDCAREA_VCHR"].ToString().Trim());
                    addItem.SubItems.Add(dr["UINT"].ToString().Trim());

                    buyPrice = Function.Dec(dr["buyprice"]);
                    if (buyPrice == 0)
                    {
                        price = Function.Dec(dr["PRICE"]);
                        addItem.SubItems.Add(price.ToString());
                    }
                    else
                    {
                        addItem.SubItems.Add(buyPrice.ToString());
                        price = buyPrice;
                    }

                    addItem.SubItems.Add(dr["COUNT"].ToString().Trim());
                    totalMoney = Function.Round(Function.Dec(dr["COUNT"]) * price, 2);

                    MoneyAll += totalMoney;
                    addItem.SubItems.Add(totalMoney.ToString().Trim());
                    addItem.SubItems.Add(dr["DOCTORNAME_CHR"].ToString().Trim());
                    this.m_objViewer.listView1.Items.Add(addItem);

                    dblsbsum_mnyAll = Function.Dec(dr["sbsum_mny"]).ToString("0.00");
                    dblACCTSUM_MNYAll = Function.Dec(dr["ACCTSUM_MNY"]).ToString("0.00");
                }
            }
            ListViewItem addItem1 = new ListViewItem("�Ը����");
            addItem1.SubItems.Add(dblsbsum_mnyAll);
            addItem1.SubItems.Add("���ʽ��");
            addItem1.SubItems.Add(dblACCTSUM_MNYAll);
            addItem1.SubItems.Add("�ϼ�:");
            addItem1.SubItems.Add("");

            addItem1.SubItems.Add(MoneyAll.ToString());
            this.m_objViewer.listView1.Items.Add(addItem1);
            this.m_objViewer.listView1.Items[this.m_objViewer.listView1.Items.Count - 1].ForeColor = System.Drawing.Color.Red;
            this.m_objViewer.listView1.Items[this.m_objViewer.listView1.Items.Count - 1].Font = new System.Drawing.Font("����", 12);
            this.m_objViewer.listView1.Visible = true;
            this.m_objViewer.LsvChargeDe.Visible = false;
        }
        #endregion

        #region ��ʼ��ӡ
        clsOutpatientPrintRecipe_VO obj_VO = new clsOutpatientPrintRecipe_VO();
        clsPrintRecipeDetail objPrint = null;
        bool isOtherPage = false;
        public void m_mthBegionPrint(System.Drawing.Printing.PrintEventArgs e)
        {
            if (dtChargeCheck.Rows.Count == 0 && this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber < 0)
            {
                e.Cancel = true;
                return;
            }
            string strSEQID = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 3].ToString();
            DataTable dtChargeDe = new DataTable();
            DataView dv;
            if ((string)this.m_objViewer.DgChargeCheck.Tag == "dtChargeCheck")
            {
                dv = m_dvRegister;
                //strSEQID=m_dvRegister[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["��ˮ��"].ToString();
            }
            else
            {
                dv = m_dvRegisterfind;
                //strSEQID=m_dvRegisterfind[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["��ˮ��"].ToString();

            }
            if (strSEQID == "")
            {
                e.Cancel = true;
                return;
            }
            Domain.m_lngGetRecipeDate(strSEQID, out dtChargeDe);//��ȡ��ϸ����

            obj_VO.m_strHospitalName = this.m_objComInfo.m_strGetHospitalTitle();
            obj_VO.m_strPrintDate = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 13].ToString(); //dv[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["��¼ʱ��"].ToString();//��ӡʱ��
            obj_VO.m_strCardID = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 0].ToString();  //dv[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["���ƿ���"].ToString();//��ӡʱ��
            obj_VO.strInvoiceNO = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 1].ToString(); //dv[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["��Ʊ���"].ToString();//��ӡʱ��
            obj_VO.m_strPatientName = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 5].ToString(); //dv[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["��������"].ToString();//��ӡʱ��
            obj_VO.m_strPatientType = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 4].ToString();  //dv[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["�������"].ToString();//��ӡʱ��
            //			obj_VO.m_strPrintDate =dv[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["��¼ʱ��"].ToString();//��ӡʱ��
            //			obj_VO.m_strPrintDate =dv[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["��¼ʱ��"].ToString();//��ӡʱ��
            obj_VO.m_strChargeUp = "";
            obj_VO.m_strSelfPay = "";
            obj_VO.m_strEmployer = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 20].ToString(); // dv[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["������λ"].ToString();
            obj_VO.m_strDiagDrName = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 11].ToString();  //dv[this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber]["ҽ������"].ToString();
            System.Collections.Generic.List<clsOutpatientPrintRecipeDetail_VO> objItemArr = new System.Collections.Generic.List<clsOutpatientPrintRecipeDetail_VO>();
            decimal MoneyAll = 0;
            if (dtChargeDe.Rows.Count > 0)
            {
                decimal price = 0;
                decimal buyPrice = 0;
                decimal totalMoney = 0;
                clsOutpatientPrintRecipeDetail_VO recVo = null;
                foreach (DataRow dr in dtChargeDe.Rows)
                {
                    recVo = new clsOutpatientPrintRecipeDetail_VO();
                    totalMoney = 0;
                    recVo.m_strChargeName = dr["NAME"].ToString().Trim();
                    recVo.m_strSpec = dr["DEC"].ToString().Trim();
                    recVo.m_strFrequency = dr["PDCAREA_VCHR"].ToString().Trim();//��������Ƶ�ʴ����������
                    recVo.m_strUnit = dr["UINT"].ToString().Trim();

                    price = Function.Dec(dr["PRICE"].ToString());
                    buyPrice = Function.Dec(dr["buyPrice"].ToString());
                    if (buyPrice != 0) price = buyPrice;

                    recVo.m_strPrice = price.ToString();
                    recVo.m_strCount = dr["COUNT"].ToString().Trim();
                    if (dr["NAME"].ToString().IndexOf("�Һŷ�") != -1)
                    {
                        obj_VO.m_strRegisterFee = (Function.Dec(dr["COUNT"]) * price).ToString("0.00"); // ((decimal)(clsConvertToDecimal.m_mthConvertObjToDecimal(dr["COUNT"]) * clsConvertToDecimal.m_mthConvertObjToDecimal(dr["PRICE"]))).ToString("0.00");
                    }
                    if (dr["NAME"].ToString().IndexOf("���") != -1)
                    {
                        obj_VO.m_strTreatFee = (Function.Dec(dr["COUNT"]) * price).ToString("0.00");   // ((decimal)(clsConvertToDecimal.m_mthConvertObjToDecimal(dr["COUNT"]) * clsConvertToDecimal.m_mthConvertObjToDecimal(dr["PRICE"]))).ToString("0.00");
                    }
                    totalMoney = Function.Round(Function.Dec(dr["COUNT"]) * price, 2);
                    recVo.m_strSumPrice = totalMoney.ToString("0.00");
                    MoneyAll += totalMoney;
                    recVo.m_decTolDiffPrice = clsConvertToDecimal.m_mthConvertObjToDecimal(dr["toldiffprice_mny"]);// ���������
                    objItemArr.Add(recVo);
                }
                if (dtChargeDe.Rows[0]["ACCTSUM_MNY"] != null && dtChargeDe.Rows[0]["ACCTSUM_MNY"].ToString().Trim() != string.Empty)
                {
                    obj_VO.m_strChargeUp = double.Parse(dtChargeDe.Rows[0]["ACCTSUM_MNY"].ToString()).ToString("0.00");
                }
                if (dtChargeDe.Rows[0]["sbsum_mny"] != null && dtChargeDe.Rows[0]["sbsum_mny"].ToString().Trim() != string.Empty)
                {
                    obj_VO.m_strSelfPay = double.Parse(dtChargeDe.Rows[0]["sbsum_mny"].ToString()).ToString("0.00");
                }
                obj_VO.strCheckOutName = dtChargeDe.Rows[0]["LASTNAME_VCHR"].ToString();
            }
            obj_VO.objPRDArr = objItemArr;
            obj_VO.m_strRecipePrice = Convert.ToString(Function.Dec(obj_VO.m_strChargeUp) + Function.Dec(obj_VO.m_strSelfPay));    //MoneyAll.ToString("0.00");
            obj_VO.m_strINSURANCEID = Domain.m_strGetpatientidentityno(obj_VO.strInvoiceNO);
            isOtherPage = false;
            currRows = 0;
        }
        #endregion

        #region ��ӡ
        int currRows = 0;
        public void m_mthPrint(System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (isOtherPage == false)
            {
                clsPrintRecipeDetail.decTotalDiffCost = 0;
            }
            objPrint = new clsPrintRecipeDetail(e, obj_VO);
            objPrint.m_mthBegionPrint(isOtherPage, ref currRows, this.intDiffPriceOn == 1);
            isOtherPage = true;
        }
        #endregion

        #region �ش�Ʊ
        /// <summary>
        /// �ش�Ʊ
        /// </summary>
        /// <param name="seqid"></param>
        /// <param name="invono"></param>
        public void m_mthReprintinvo()
        {
            if (dtChargeCheck.Rows.Count == 0 || this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber < 0)
            {
                return;
            }

            string seqid = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 3].ToString();

            if (seqid == "")
            {
                return;
            }

            this.m_objViewer.Cursor = Cursors.WaitCursor;
            clsCalcPatientCharge objCalPatientCharge = new clsCalcPatientCharge(this.m_objComInfo.m_strGetHospitalTitle());
            com.digitalwave.Utility.clsLogText objlog = new com.digitalwave.Utility.clsLogText();
            objlog.LogError(this.m_objComInfo.m_strGetHospitalTitle());
            objCalPatientCharge.m_mthReprintinvoice(seqid, this.m_objViewer.LoginInfo.m_strEmpID, 0);
            this.m_objViewer.Cursor = Cursors.Default;
        }
        #endregion

        #region ��ʾҽ�����ʵ���
        /// <summary>
        /// ��ʾҽ�����ʵ���
        /// </summary>
        public void m_mthShowBillNo()
        {
            if (dtChargeCheck.Rows.Count == 0 || this.m_objViewer.DgChargeCheck.CurrentCell.RowNumber < 0)
            {
                return;
            }

            //��Ʊ��
            string InvoNo = this.m_objViewer.DgChargeCheck[this.m_objViewer.DgChargeCheck.CurrentRowIndex, 1].ToString();
            string BillNo = Domain.m_strGetBillNoByInvoNo(InvoNo);
            if (BillNo.Trim() != "")
            {
                this.m_objViewer.lblBillNo.Text = "ҽ�����ʵ��ţ�" + BillNo;
            }
            else
            {
                this.m_objViewer.lblBillNo.Text = "";
            }

        }
        #endregion
    }
}
