using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 
using com.digitalwave.iCare.BIHOrder.Control;
using System.Collections;
using System.Collections.Generic;
using com.digitalwave.iCare.gui.HIS;

namespace com.digitalwave.iCare.BIHOrder
{
    class clsCtl_BIHConfirmCharge : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ��������
        clsDcl_ExecuteOrder m_objManage = null;
        DataTable m_dtChargeList = new DataTable();
        DataTable m_dtOrderExecute = new DataTable();
        /// <summary>
        /// ���úϼ��б�
        /// </summary>
        Hashtable m_HtSumList = new Hashtable();
        /// <summary>
        /// ��ǰ���˵Ǽ���ˮ��
        /// </summary>
        public string m_strCurrentRegisterID = "";
        #endregion

        #region ���캯��
        public clsCtl_BIHConfirmCharge()
        {
            m_objManage = new clsDcl_ExecuteOrder();
        }
        #endregion

        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmBIHConfirmCharge m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmBIHConfirmCharge)frmMDI_Child_Base_in;

        }
        #endregion

        internal void IniTheForm()
        {
            this.m_objViewer.m_dtvChangeList.Rows.Clear();

            if (this.m_objViewer.m_strView.Equals("2"))
            {
                long lngRes = m_objManage.m_lngGetComfirmThChargeData(m_strCurrentRegisterID, out m_dtOrderExecute, out m_dtChargeList);

            }
            else
            {
                long lngRes = m_objManage.m_lngGetComfirmChargeData(m_strCurrentRegisterID, out m_dtOrderExecute, out m_dtChargeList);
            }

            TheChargeSumInit();
            BindTheOrderList();
            TheCheckedChargeSum();
            ControlTheButton();

        }

        /// <summary>
        /// ��ǰѡ�е���Ŀ�����ܺ�
        /// </summary>
        private void TheSelectChargeSum()
        {

        }

        /// <summary>
        /// ���úϼƷ���ϼ�
        /// </summary>
        private void TheChargeSumInit()
        {
            m_HtSumList.Clear();
            decimal allSum = 0;
            for (int i = 0; i < m_dtChargeList.Rows.Count; i++)
            {
                if (m_dtChargeList.Rows[i]["STATUS_INT"].ToString().Trim().Equals("0"))//�Ѿ����ѵ��շ���Ŀ���Ʒ�
                {
                    continue;
                }
                if (this.m_objViewer.m_strView.Equals("2"))//ֱ�ӵĲ��ϷѲ���ʾ
                {
                    if (m_dtChargeList.Rows[i]["flag_int"].ToString().Trim().Equals("2"))
                    {
                        continue;
                    }
                }
                string m_strORDEREXECID = clsConverter.ToString(m_dtChargeList.Rows[i]["ORDEREXECID_CHR"].ToString().Trim());
                if (!m_HtSumList.Contains(m_strORDEREXECID))
                {
                    decimal k = 0;
                    m_HtSumList.Add(m_strORDEREXECID, k);
                }
                allSum = (decimal)m_HtSumList[m_strORDEREXECID];
                allSum += clsConverter.ToDecimal(m_dtChargeList.Rows[i]["AMOUNT_DEC"].ToString().Trim()) * clsConverter.ToDecimal(m_dtChargeList.Rows[i]["UNITPRICE_DEC"].ToString().Trim());
                m_HtSumList[m_strORDEREXECID] = allSum;
            }
        }

        private void BindTheChargeList(string ORDEREXECID_CHR)
        {
            this.m_objViewer.m_dtvChangeList.Rows.Clear();
            DataView myDataView = new DataView(m_dtChargeList);
            myDataView.RowFilter = "ORDEREXECID_CHR='" + ORDEREXECID_CHR + "'";
            myDataView.Sort = "CREATE_DAT";
            if (myDataView.Count <= 0)
            {
                return;
            }
            decimal allSum = 0;//����ϼ�
            for (int i = 0; i < myDataView.Count; i++)
            {
                if (this.m_objViewer.m_strView.Equals("2"))//ֱ�ӵĲ��ϷѲ���ʾ
                {
                    if (myDataView[i]["flag_int"].ToString().Trim().Equals("2"))
                    {
                        continue;
                    }
                }
                this.m_objViewer.m_dtvChangeList.Rows.Add();
                DataGridViewRow row = this.m_objViewer.m_dtvChangeList.Rows[this.m_objViewer.m_dtvChangeList.RowCount - 1];
                row.Cells["c_no"].Value = clsConverter.ToString(i + 1);
                row.Cells["c_CHARGEITEMNAME"].Value = clsConverter.ToString(myDataView[i]["CHARGEITEMNAME_CHR"].ToString().Trim());
                row.Cells["c_UNITPRICE"].Value = clsConverter.ToString(myDataView[i]["UNITPRICE_DEC"].ToString().Trim()) + clsConverter.ToString(myDataView[i]["UNIT_VCHR"].ToString().Trim());
                row.Cells["c_AMOUNT"].Value = clsConverter.ToString(myDataView[i]["AMOUNT_DEC"].ToString().Trim());
                string PSTATUS_CHR = "";
                switch (clsConverter.ToString(myDataView[i]["PSTATUS_INT"].ToString().Trim()))
                {
                    case "0":
                        PSTATUS_CHR = "��ȷ��";
                        break;
                    case "1":
                        PSTATUS_CHR = "����";
                        break;
                    case "2":
                        PSTATUS_CHR = "����";
                        break;
                    case "3":
                        PSTATUS_CHR = "����";
                        break;
                    case "4":
                        PSTATUS_CHR = "ֱ��";
                        break;

                }
                row.Cells["c_PSTATUS_INT"].Value = PSTATUS_CHR;
                if (myDataView[i]["STATUS_INT"].ToString().Trim().Equals("0"))//��ʾΪ����
                {
                    row.Cells["c_PSTATUS_INT"].Value = "����";

                }
                if (clsConverter.ToString(myDataView[i]["ISRICH_INT"].ToString().Trim()).Equals("0"))
                {
                    row.Cells["c_ISRICH"].Value = "";
                }
                else
                {
                    row.Cells["c_ISRICH"].Value = "��";
                }
                if (clsConverter.ToString(myDataView[i]["NEEDCONFIRM_INT"].ToString().Trim()).Equals("0"))
                {
                    row.Cells["c_NEEDCONFIRM"].Value = "0";
                }
                else
                {
                    row.Cells["c_NEEDCONFIRM"].Value = "1";
                }
                if (clsConverter.ToString(myDataView[i]["ISMEPAY_INT"].ToString().Trim()).Equals("0"))
                {
                    row.Cells["c_ISMEPAY_INT"].Value = "";
                }
                else
                {
                    row.Cells["c_ISMEPAY_INT"].Value = "��";
                }
                row.Cells["c_CONFIRMER_VCHR"].Value = clsConverter.ToString(myDataView[i]["CONFIRMER_VCHR"].ToString().Trim());
                row.Cells["c_CONFIRM_DAT"].Value = clsConverter.ToString(myDataView[i]["CONFIRM_DAT"].ToString().Trim());
                row.Cells["c_Sum"].Value = decimal.Round(clsConverter.ToDecimal(myDataView[i]["AMOUNT_DEC"].ToString().Trim()) * clsConverter.ToDecimal(myDataView[i]["UNITPRICE_DEC"].ToString().Trim()), 2).ToString("0.00");
                row.Cells["c_INSURACEDESC_VCHR"].Value = clsConverter.ToString(myDataView[i]["INSURACEDESC_VCHR"].ToString().Trim());
                row.Cells["c_SPEC_VCHR"].Value = clsConverter.ToString(myDataView[i]["SPEC_VCHR"].ToString().Trim());

                allSum += decimal.Round(clsConverter.ToDecimal(myDataView[i]["AMOUNT_DEC"].ToString().Trim()) * clsConverter.ToDecimal(myDataView[i]["UNITPRICE_DEC"].ToString().Trim()), 2);

            }

            /*
            CLACAREA_NAME ���㲡��id{=����.id}
            CREATEAREA_NAME �����ص�id{=����.id}

            CHARGEITEMNAME_CHR �շ���Ŀ����{=�շ���Ŀ.����}
            UNITPRICE_DEC סԺ����{=�շ���Ŀ.סԺ����}
            UNIT_VCHR  סԺ��λ{=�շ���Ŀ.סԺ��λ}
            AMOUNT_DEC ����
            PSTATUS_INT ����״̬{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            ISRICH_INT �Ƿ����{1/0}
            NEEDCONFIRM_INT �Ƿ���Ҫ������� 0-�� 1-��
            CONFIRMER_VCHR ���������
            CONFIRM_DAT ���ʱ��


            c_no
            c_CHARGEITEMNAME
            c_UNITPRICE
            c_AMOUNT
            c_PSTATUS_INT
            c_ISRICH
            c_NEEDCONFIRM
            c_CONFIRMER_VCHR
            c_CONFIRM_DAT
             */
        }

        private void BindTheOrderList()
        {
            this.m_objViewer.m_dtvOrderList.Rows.Clear();
            int j = 0;

            for (int i = 0; i < m_dtOrderExecute.Rows.Count; i++)
            {
                if (this.m_objViewer.m_rdoYET.Checked)
                {
                    if (m_dtOrderExecute.Rows[i]["CONFIRM_DAT"].ToString().Trim().Equals(""))
                    {
                        continue;
                    }
                }
                else if (this.m_objViewer.m_rdoNOT.Checked)
                {
                    if (!m_dtOrderExecute.Rows[i]["CONFIRM_DAT"].ToString().Trim().Equals(""))
                    {
                        continue;
                    }

                }
                this.m_objViewer.m_dtvOrderList.Rows.Add();
                DataGridViewRow row = this.m_objViewer.m_dtvOrderList.Rows[this.m_objViewer.m_dtvOrderList.RowCount - 1];
                if (this.m_objViewer.m_chkSelectAll.Checked)
                {
                    row.Cells["m_clmselectCheck"].Value = "1";
                }
                else
                {
                    row.Cells["m_clmselectCheck"].Value = "0";
                }
                j++;
                row.Cells["m_clmNO"].Value = Convert.ToString(j);
                row.Cells["m_clmrecipeno_int"].Value = m_dtOrderExecute.Rows[i]["recipeno_int"].ToString().Trim();
                row.Cells["m_clmname_vchr"].Value = m_dtOrderExecute.Rows[i]["name_vchr"].ToString().Trim();
                row.Cells["m_clmDOCTOR_VCHR"].Value = m_dtOrderExecute.Rows[i]["DOCTOR_VCHR"].ToString().Trim();
                row.Cells["m_clmCREATOR_CHR"].Value = m_dtOrderExecute.Rows[i]["CREATOR_CHR"].ToString().Trim();
                row.Cells["m_clmCREATEDATE_DAT"].Value = m_dtOrderExecute.Rows[i]["CREATEDATE_DAT"].ToString().Trim();
                row.Cells["m_clmCONFIRMER_VCHR"].Value = m_dtOrderExecute.Rows[i]["CONFIRMER_VCHR"].ToString().Trim();
                row.Cells["m_clmCONFIRM_DAT"].Value = m_dtOrderExecute.Rows[i]["CONFIRM_DAT"].ToString().Trim();
                row.Cells["m_clmORDEREXECID_CHR"].Value = m_dtOrderExecute.Rows[i]["ORDEREXECID_CHR"].ToString().Trim();
                row.Cells["m_clmSTATUS_INT"].Value = m_dtOrderExecute.Rows[i]["STATUS_INT"].ToString().Trim();
                /*
                m_clmselectCheck
                m_clmNO
                m_clmrecipeno_int
                m_clmname_vchr
                m_clmDOCTOR_VCHR
                m_clmCREATOR_CHR
                m_clmCREATEDATE_DAT
                m_clmCONFIRMER_VCHR
                m_clmCONFIRM_DAT
                m_clmORDEREXECID_CHR
                 */
                if (j == 1)
                {
                    this.m_objViewer.m_dtvOrderList.CurrentCell = this.m_objViewer.m_dtvOrderList.Rows[0].Cells[0];
                }
            }
            if (this.m_objViewer.m_dtvOrderList.RowCount > 0)
            {
                this.m_objViewer.m_dtvOrderList.CurrentCell = this.m_objViewer.m_dtvOrderList.Rows[0].Cells[1];
            }
        }

        internal void binTheChargeList()
        {
            if (this.m_objViewer.m_dtvOrderList.CurrentRow != null)
            {
                int i = this.m_objViewer.m_dtvOrderList.CurrentRow.Index;
                if (this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmORDEREXECID_CHR"].Value != null)
                {
                    decimal oneSum = 0;
                    BindTheChargeList(this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmORDEREXECID_CHR"].Value.ToString().Trim());
                    string ORDEREXECID_CHR = this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmORDEREXECID_CHR"].Value.ToString().Trim();
                    if (m_HtSumList.Contains(ORDEREXECID_CHR))
                    {
                        oneSum = decimal.Round((decimal)m_HtSumList[ORDEREXECID_CHR], 2);

                    }
                    this.m_objViewer.m_txtOneSum.Text = "��ǰ�ϼƣ�" + oneSum.ToString("0.00");
                }
            }
        }

        #region ѡ����¼�����
        /// <summary>
        /// ѡ����¼�����
        /// </summary>
        internal void SelectAll()
        {
            if (m_objViewer.m_chkSelectAll.Checked == true)
            {
                for (int i = 0; i < m_objViewer.m_dtvOrderList.Rows.Count; i++)
                {
                    m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmselectCheck"].Value = "1";
                }
            }
            else
            {
                for (int i = 0; i < m_objViewer.m_dtvOrderList.Rows.Count; i++)
                {
                    m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmselectCheck"].Value = "0";
                }
            }
        }
        #endregion



        internal void ChangeTheSelectState(int rowNum, int colNum)
        {
            if (this.m_objViewer.m_dtvOrderList.Rows.Count > 0 && rowNum >= 0 && colNum == 0)
            {
                if (this.m_objViewer.m_dtvOrderList.Rows[rowNum].Cells["m_clmselectCheck"].Value.ToString().Trim().Equals("0"))
                {
                    this.m_objViewer.m_dtvOrderList.Rows[rowNum].Cells["m_clmselectCheck"].Value = "1";
                }
                else if (this.m_objViewer.m_dtvOrderList.Rows[rowNum].Cells["m_clmselectCheck"].Value.ToString().Trim().Equals("1"))
                {
                    this.m_objViewer.m_dtvOrderList.Rows[rowNum].Cells["m_clmselectCheck"].Value = "0";
                }
                TheCheckedChargeSum();
                ControlTheButton();
            }

        }

        private void TheChargeSum(int rowNum)
        {
            string ORDEREXECID_CHR = m_objViewer.m_dtvOrderList.Rows[rowNum].Cells["m_clmORDEREXECID_CHR"].Value.ToString().Trim();
            if (m_HtSumList.Contains(ORDEREXECID_CHR))
            {
                this.m_objViewer.m_txtOneSum.Text = "��ǰ�ϼƣ�" + ((decimal)m_HtSumList[ORDEREXECID_CHR]).ToString("0.00");
            }

        }

        /// <summary>
        /// ��ǰѡ�з��úϼ�
        /// </summary>
        public void TheCheckedChargeSum()
        {
            decimal allSum = 0;
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                if (this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmselectCheck"].Value.ToString().Trim().Equals("1"))
                {
                    if (this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmSTATUS_INT"].Value.ToString().Trim().Equals("0"))//�Ѿ����ѵ��շ���Ŀ���Ʒ�
                    {
                        continue;
                    }
                    string ORDEREXECID_CHR = m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmORDEREXECID_CHR"].Value.ToString().Trim();
                    if (m_HtSumList.Contains(ORDEREXECID_CHR))
                    {
                        allSum += (decimal)m_HtSumList[ORDEREXECID_CHR];
                    }
                }
            }
            this.m_objViewer.m_txtAllSum.Text = "ѡ�кϼƣ�" + allSum.ToString("0.00");

        }

        private void ControlTheButton()
        {
            int m_intStatus = 0;
            this.m_objViewer.m_cmdComfirm.Enabled = true;
            this.m_objViewer.m_btnDable.Enabled = true;
            if (this.m_objViewer.m_dtvOrderList.Rows.Count > 0)
            {
                for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
                {
                    if (this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmselectCheck"].Value == null)
                    {
                        return;
                    }
                    if (this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmselectCheck"].Value.ToString().Trim().Equals("1"))
                    {
                        if (!this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmCONFIRM_DAT"].Value.ToString().Trim().Equals(""))
                        {
                            this.m_objViewer.m_cmdComfirm.Enabled = false;
                            this.m_objViewer.m_btnDable.Enabled = false;
                            return;
                        }
                    }
                }

            }

        }

        internal void UpdateBihOrderConfirmer()
        {

            string m_strOrderExecuteID_Arr = "";
            m_strOrderExecuteID_Arr = GetTheSelectItem();
            if (m_strOrderExecuteID_Arr.Trim().Equals(""))
            {
                MessageBox.Show("����ѡ���ȷ�ϵ�ִ�е�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            //DotorComfirmBox comfirmBox1=new DotorComfirmBox();
            //if (comfirmBox1.ShowDialog() == DialogResult.OK)
            if (MessageBox.Show("ȷ�Ͻ��д˲���?", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                long lngRes = 0;
                if (this.m_objViewer.m_strView.Equals("2"))
                {
                    // lngRes = m_objManage.m_lngBihOrderExecuteThChargeConfirmer(m_strOrderExecuteID_Arr, comfirmBox1.empid_chr, comfirmBox1.lastname_vchr);
                    frmReckoning frec = new frmReckoning(this.m_objViewer.InvoNo);

                    //frec.txtInvono.Text = clsPublic.m_strGetCurrInvoiceNo();
                    frec.ChargeType = 5;
                    DataView myDataView = new DataView(m_dtChargeList);
                    string ORDEREXECID_Arr = "";//�ǲ���
                    string ORDEREXECID_Arr2 = "";//����
                    for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
                    {
                        if (this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmselectCheck"].Value.ToString().Trim().Equals("0"))
                        {
                            continue;
                        }
                        ORDEREXECID_Arr += " ORDEREXECID_CHR='" + this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmORDEREXECID_CHR"].Value.ToString().TrimEnd() + "' ";
                        if (i < this.m_dtOrderExecute.Rows.Count - 1)
                        {
                            ORDEREXECID_Arr += "OR";
                        }
                    }
                    ORDEREXECID_Arr = ORDEREXECID_Arr.TrimEnd("OR".ToCharArray());
                    if (!ORDEREXECID_Arr.Trim().Equals(""))
                    {
                        ORDEREXECID_Arr = " (flag_int<>2) and (" + ORDEREXECID_Arr + ")";
                        ORDEREXECID_Arr2 = " (flag_int=2) and (" + ORDEREXECID_Arr + ")";
                    }
                    myDataView.RowFilter = ORDEREXECID_Arr;
                    DataTable m_dtChargeList2 = m_dtChargeList.Clone();
                    for (int i = 0; i < myDataView.Count; i++)
                    {
                        m_dtChargeList2.Rows.Add(myDataView[i].Row.ItemArray);
                        //string a1= myDataView[i]["UNITPRICE_DEC"].ToString().Trim();
                        //string a2 = myDataView[i]["AMOUNT_DEC"].ToString().Trim();
                        //string a3 = myDataView[i]["precent_dec"].ToString().Trim();
                    }

                    frec.ChargeDetail = m_dtChargeList2;
                    frec.objPatient = this.m_objViewer.ucPatientInfo1;
                    frec.ConfirmID = this.m_objViewer.LoginInfo.m_strEmpID; //comfirmBox1.empid_chr ;
                    frec.ConfirmName = this.m_objViewer.LoginInfo.m_strEmpName; //comfirmBox1.lastname_vchr;
                    frec.DayChrgType = 2;
                    frec.DayAccountsArr = null;
                    if (frec.ShowDialog() == DialogResult.OK)
                    {
                        myDataView.RowFilter = ORDEREXECID_Arr;
                        List<string> m_arrPCHARGEID_CHR = new List<string>();
                        for (int i = 0; i < myDataView.Count; i++)
                        {
                            m_arrPCHARGEID_CHR.Add(myDataView[i]["PCHARGEID_CHR"].ToString().Trim());
                        }
                        if (m_arrPCHARGEID_CHR.Count > 0)
                        {
                            lngRes = m_objManage.m_lngBihOrderExecuteChargeConfirmerTh(m_arrPCHARGEID_CHR, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);
                        }
                        ArrayList arr = new ArrayList();
                        arr.Add(this.m_objViewer.ucPatientInfo1.RegisterID);

                        IPutMadicine put = PutMadicineFactory.GetInstanceForRecipeMed();
                        //put.CreatePutMedDetail(arr, comfirmBox1.empid_chr);
                        put.CreatePutMedDetail(arr, this.m_objViewer.LoginInfo.m_strEmpID);
                        this.m_objViewer.ucPatientInfo1.m_mthFind(this.m_objViewer.ucPatientInfo1.BihPatient_VO.Zyh, 2);
                        this.m_objViewer.ucPatientInfo1_ZyhChanged();
                    }

                }
                else
                {
                    //  lngRes = m_objManage.m_lngBihOrderExecuteChargeConfirmer(m_strOrderExecuteID_Arr, comfirmBox1.empid_chr, comfirmBox1.lastname_vchr);
                    lngRes = m_objManage.m_lngBihOrderExecuteChargeConfirmer(m_strOrderExecuteID_Arr, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);
                    if (lngRes > 0)
                    {
                        MessageBox.Show("��˳ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //LoadTheDate();
                        sendTheBill();
                        this.m_objViewer.ucPatientInfo1.m_mthFind(this.m_objViewer.ucPatientInfo1.BihPatient_VO.Zyh, 2);
                        this.m_objViewer.ucPatientInfo1_ZyhChanged();
                    }
                }



            }
            // comfirmBox1.Close();


        }

        internal void UpdateBihOrderDenableConfirmer()
        {

            string m_strOrderExecuteID_Arr = "";
            m_strOrderExecuteID_Arr = GetTheSelectItem();
            if (m_strOrderExecuteID_Arr.Trim().Equals(""))
            {
                MessageBox.Show("����ѡ���ȷ�ϵ�ִ�е�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            //DotorComfirmBox comfirmBox1 = new DotorComfirmBox();
            //if (comfirmBox1.ShowDialog() == DialogResult.OK)
            if (MessageBox.Show("ȷ���������ϲ���?", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                long lngRes = 0;
                lngRes = m_objManage.m_lngBihOrderExecuteDenableConfirmer(m_strOrderExecuteID_Arr, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);
                if (lngRes > 0)
                {
                    MessageBox.Show("���ϳɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //LoadTheDate();
                    //sendTheBill();
                    this.m_objViewer.ucPatientInfo1.m_mthFind(this.m_objViewer.ucPatientInfo1.BihPatient_VO.Zyh, 2);
                    this.m_objViewer.ucPatientInfo1_ZyhChanged();
                }
            }
            //comfirmBox1.Close();

            //IniTheForm();

        }

        private string GetTheSelectItem()
        {
            string orderArr = "";
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                if (m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmselectCheck"].Value.ToString().Equals("1"))
                {
                    orderArr += "'" + m_objViewer.m_dtvOrderList.Rows[i].Cells["m_clmORDEREXECID_CHR"].Value + "',";

                }
            }
            orderArr = orderArr.TrimEnd(',');
            return orderArr;
        }


        internal void sendTheBill()
        {
            bool m_blhave = false;
            //��鵱ǰ�Ƿ��л�û����ȷ���Ƿ���Ҫ������� 0-�� 1-�ǵĵ�����˵���û�з��͵�ҽ�����뵥
            long lngRes = m_objManage.m_lngCheckTheExecuteBill(m_strCurrentRegisterID, out m_blhave);
            if (m_blhave)
            {
                IPutMadicine madicine;
                ArrayList m_arrRegisterid = new ArrayList();
                m_arrRegisterid.Add(m_strCurrentRegisterID);
                madicine = PutMadicineFactory.GetInstance();
                long ret = madicine.CreatePutMedDetail(m_arrRegisterid, this.m_objViewer.LoginInfo.m_strEmpID);
                if (ret > 0)
                {

                    MessageBox.Show("�ѳɹ�������ϣ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //else
            //{
            //    MessageBox.Show("��ǰû������˵���û�з��͵�ҽ�����뵥��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;   
            //}

        }
    }
}
