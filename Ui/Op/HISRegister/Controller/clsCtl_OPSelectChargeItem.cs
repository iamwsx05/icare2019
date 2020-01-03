using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.gui.HIS.Viewer;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    class clsCtl_OPSelectChargeItem : com.digitalwave.GUI_Base.clsController_Base
    {
        private frmOPSelectChargeItem m_objViewer = null;

        private clsCtl_OPCharge objCtlOPCharge = null;
        private clsDcl_OPSelectChargeItem objDomain = null;
        /// <summary>
        /// �շ���Ŀ��ϸ
        /// </summary>
        private DataTable m_dtbChargeItem = null;

        /// <summary>
        /// ��ͯ�۸�
        /// </summary>
        internal bool isChildPrice { get; set; }

        #region ���캯��
        public clsCtl_OPSelectChargeItem()
        {
            objCtlOPCharge = new clsCtl_OPCharge();
            objDomain = new clsDcl_OPSelectChargeItem();
        }
        #endregion

        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmOPSelectChargeItem)frmMDI_Child_Base_in;
        }

        #region ��ʾ�շ���Ŀ
        /// <summary>
        /// ��ʾ�շ���Ŀ
        /// </summary>
        /// <param name="p_dtbChargeItem"></param>
        /// <param name="p_strRecipeNo">������</param>
        public void m_mthShow(DataTable p_dtbChargeItem, string p_strRecipeNo)
        {

            if (p_dtbChargeItem == null && p_dtbChargeItem.Rows.Count <= 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(p_strRecipeNo))
            {
                return;
            }

            m_dtbChargeItem = p_dtbChargeItem.Copy();

            string RecID = p_strRecipeNo;

            long ret = 0;
            DataTable dt = null;
            //������Ŀ��״̬���Ƿ���ɸ�����
            DataTable dtbDiagnosisItemStatus = null;
            //�Ƿ�ҩ
            DataTable dtbSendMed = null;
            //�Ƿ�����ҩ
            int intIsSendWM = 0;
            //�Ƿ�����ҩ
            int intIsSendCM = 0;
            //�Ƿ��������Ĳ���
            int intIsSendOth = 0;

            decimal Totalmny = 0;
            clsDcl_DoctorWorkstation objDoct = new clsDcl_DoctorWorkstation();

            ret = objDomain.m_lngQuerySendMed(RecID, out dtbSendMed);

            if (ret > 0)
            {
                if (dtbSendMed != null && dtbSendMed.Rows.Count > 0)
                {
                    int intRowCount = dtbSendMed.Rows.Count;
                    for (int i1 = 0; i1 < intRowCount; i1++)
                    {
                        DataRow dtrTemp = dtbSendMed.Rows[i1];
                        switch (dtrTemp["medtype"].ToString().Trim())
                        {
                            case "WM":
                                int.TryParse(dtrTemp["issendmed"].ToString().Trim(), out intIsSendWM);
                                break;
                            case "CM":
                                int.TryParse(dtrTemp["issendmed"].ToString().Trim(), out intIsSendCM);
                                break;
                            case "QTH":
                                int.TryParse(dtrTemp["issendmed"].ToString().Trim(), out intIsSendOth);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            #region 1����ҩ
            //colChecked,colRepNo,colItemName,colState,colOriSpecification,colNewSpecification,
            // colQuantity,colOriPrice,colNewPrice,colSum,colFrequency,colUsage,colItemAttribute
            int intRowIndex = 0;
            //1����ҩ
            ret = objDoct.m_mthFindRecipeDetail1(RecID, out dt, false, false);
            if (ret > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    intRowIndex = this.m_objViewer.dtgItem.Rows.Add();

                    DataGridViewRow dgvrTemp = this.m_objViewer.dtgItem.Rows[intRowIndex];
                    DataRow dr = dt.Rows[i];

                    dgvrTemp.Cells["colChecked"].Value = "T";
                    //�Ƿ�ѡ�У�Ҳ�����Ƿ���ҩ
                    if (intIsSendWM == 1)
                    {
                        dgvrTemp.Cells["colChecked"].ReadOnly = true;
                        dgvrTemp.Cells["colState"].Value = "����ҩ";
                        dgvrTemp.DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        //dgvrTemp.Cells["colChecked"].Value = "F";
                        dgvrTemp.Cells["colState"].Value = "δ��ҩ";
                    }
                    //����
                    if (dr["rowno_chr"].ToString().Trim() == "0")
                    {
                        dgvrTemp.Cells["colRepNo"].Value = "";
                    }
                    else
                    {
                        dgvrTemp.Cells["colRepNo"].Value = dr["rowno_chr"].ToString().Trim();
                    }
                    //��Ŀ����
                    dgvrTemp.Cells["colItemName"].Value = dr["itemname_vchr"].ToString();
                    //ԭ���
                    dgvrTemp.Cells["colOriSpecification"].Value = dr["itemspec_vchr"].ToString();
                    //�¹��
                    if (dr["rowno_chr"].ToString().Trim() == "0")
                    {
                        dgvrTemp.Cells["colNewSpecification"].Value = "";
                    }
                    else
                    {
                        dgvrTemp.Cells["colNewSpecification"].Value = dr["rowno_chr"].ToString().Trim();
                    }
                    //Ƶ��
                    dgvrTemp.Cells["colFrequency"].Value = dr["freqname_chr"].ToString();
                    //�÷�
                    dgvrTemp.Cells["colUsage"].Value = dr["usagename_vchr"].ToString();
                    //����
                    dgvrTemp.Cells["colQuantity"].Value = dr["tolqty_dec"].ToString();
                    //ԭ����
                    dgvrTemp.Cells["colOriPrice"].Value = dr["unitprice_mny"].ToString();
                    //�µ���
                    dgvrTemp.Cells["colNewPrice"].Value = "";
                    //ԭ�ϼƽ��
                    dgvrTemp.Cells["colSum"].Value = dr["tolprice_mny"].ToString();
                    //��Ŀ����
                    dgvrTemp.Cells["colItemAttribute"].Value = "WM";
                    //��Ŀid 
                    dgvrTemp.Cells["colItemID"].Value = dr["itemid_chr"].ToString();
                    //���Ŀ�����뵥id
                    dgvrTemp.Cells["colAttachid"].Value = "";


                    //�жϴ�С��λ
                    if (dr["opchargeflg_int"].ToString().Trim() == "0")
                    {
                        if (this.ConvertObjToDecimal(dr["unitprice_mny"]) != this.ConvertObjToDecimal(dr["itemprice_mny"]))
                        {
                            dgvrTemp.Cells["colNewPrice"].Value = dr["itemprice_mny"].ToString();
                        }
                    }
                    else
                    {
                        if (this.ConvertObjToDecimal(dr["unitprice_mny"]) != this.ConvertObjToDecimal(dr["submoney"]))
                        {
                            dgvrTemp.Cells["colNewPrice"].Value = dr["submoney"].ToString();
                        }
                    }


                    Totalmny += ConvertObjToDecimal(dr["tolprice_mny"]);
                    dgvrTemp.Tag = dr;
                }
            }
            #endregion

            #region 2����ҩ
            //2����ҩ
            ret = objDoct.m_mthFindRecipeDetail2(RecID, out dt, false, false);
            if (ret > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    intRowIndex = this.m_objViewer.dtgItem.Rows.Add();

                    DataGridViewRow dgvrTemp = this.m_objViewer.dtgItem.Rows[intRowIndex];
                    DataRow dr = dt.Rows[i];

                    //colChecked,colRepNo,colItemName,colState,colOriSpecification,colNewSpecification,
                    // cloQuantity,colOriPrice,colNewPrice,colSum,colFrequency,colUsage,colItemAttribute

                    dgvrTemp.Cells["colChecked"].Value = "T";
                    if (intIsSendCM == 1)
                    {
                        dgvrTemp.Cells["colChecked"].ReadOnly = true;
                        dgvrTemp.Cells["colState"].Value = "����ҩ";
                        dgvrTemp.DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        //dgvrTemp.Cells["colChecked"].Value = "F";
                        dgvrTemp.Cells["colState"].Value = "δ��ҩ";
                    }
                    dgvrTemp.Cells["colRepNo"].Value = "";
                    dgvrTemp.Cells["colItemName"].Value = dr["itemname"].ToString();
                    dgvrTemp.Cells["colOriSpecification"].Value = dr["dec"].ToString();
                    if (dr["dec"].ToString().Trim() != dr["spec"].ToString().Trim())
                    {
                        dgvrTemp.Cells["colNewSpecification"].Value = dr["spec"].ToString();
                    }
                    else
                    {
                        dgvrTemp.Cells["colNewSpecification"].Value = "";
                    }
                    dgvrTemp.Cells["colFrequency"].Value = "";
                    dgvrTemp.Cells["colUsage"].Value = dr["usagename_vchr"].ToString();
                    dgvrTemp.Cells["colQuantity"].Value = Convert.ToString(ConvertObjToDecimal(dr["times"]) * ConvertObjToDecimal(dr["min_qty_dec"]));
                    dgvrTemp.Cells["colOriPrice"].Value = dr["price"].ToString();
                    if (this.ConvertObjToDecimal(dr["price"]) != this.ConvertObjToDecimal(dr["submoney"]))
                    {
                        dgvrTemp.Cells["colNewPrice"].Value = dr["submoney"].ToString();
                    }
                    else
                    {
                        dgvrTemp.Cells["colNewPrice"].Value = "";
                    }
                    dgvrTemp.Cells["colSum"].Value = dr["summoney"].ToString();
                    dgvrTemp.Cells["colItemAttribute"].Value = "CM";
                    //��Ŀid 
                    dgvrTemp.Cells["colItemID"].Value = dr["itemid"].ToString();
                    //���Ŀ�����뵥id
                    dgvrTemp.Cells["colAttachid"].Value = "";

                    Totalmny += ConvertObjToDecimal(dr["summoney"]);

                    dgvrTemp.Tag = dr;
                }
            }
            #endregion

            #region 3��������Ŀ
            //������Ŀ
            //���顢���顢����
            int intCheck = 1;
            ret = objDoct.m_mthFindRecipeDetailOrder(RecID, out dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                intRowIndex = this.m_objViewer.dtgItem.Rows.Add();

                DataGridViewRow dgvrTemp = this.m_objViewer.dtgItem.Rows[intRowIndex];
                DataRow dr = dt.Rows[i];

                //����
                dgvrTemp.Cells["colRepNo"].Value = "";
                //��Ŀ����
                dgvrTemp.Cells["colItemName"].Value = dr["orderdicname_vchr"].ToString();
                //ԭ���
                dgvrTemp.Cells["colOriSpecification"].Value = dr["spec_vchr"].ToString();
                //�¹��
                dgvrTemp.Cells["colNewSpecification"].Value = "";
                //Ƶ��
                dgvrTemp.Cells["colFrequency"].Value = "";
                //�÷�
                dgvrTemp.Cells["colUsage"].Value = "";
                //����
                dgvrTemp.Cells["colQuantity"].Value = dr["qty_dec"].ToString();
                //ԭ����
                dgvrTemp.Cells["colOriPrice"].Value = dr["pricemny_dec"].ToString();
                //�µ���
                dgvrTemp.Cells["colNewPrice"].Value = "";
                // ԭ�ϼƽ��
                dgvrTemp.Cells["colSum"].Value = dr["totalmny_dec"].ToString();
                //������Ŀid
                dgvrTemp.Cells["colItemID"].Value = dr["orderdicid_chr"].ToString();

                dgvrTemp.Cells["colChecked"].Value = "T";
                dgvrTemp.Cells["colState"].Value = "δȷ��";
                //���Ŀ�����뵥id
                dgvrTemp.Cells["colAttachid"].Value = dr["attachid_vchr"].ToString();
                string strItemid = dr["itemid_chr"].ToString().Trim();
                string ordertype = dr["tableindex_int"].ToString();

                if (dtbDiagnosisItemStatus != null)
                {
                    dtbDiagnosisItemStatus.Dispose();
                    dtbDiagnosisItemStatus = null;
                }
                if (ordertype == "3")
                {
                    dgvrTemp.Cells["colItemAttribute"].Value = "lis";
                    ret = objDomain.m_lngQueryDiagnosisItemStatus(p_strRecipeNo, dr["orderdicid_chr"].ToString().Trim(), intCheck, out dtbDiagnosisItemStatus);
                }
                else if (ordertype == "4")
                {
                    dgvrTemp.Cells["colItemAttribute"].Value = "test";
                    ret = objDomain.m_lngQueryDiagnosisItemStatus(p_strRecipeNo, dr["orderdicid_chr"].ToString().Trim(), intCheck, out dtbDiagnosisItemStatus);
                }
                else if (ordertype == "5")
                {
                    dgvrTemp.Cells["colItemAttribute"].Value = "ops";
                    ret = objDomain.m_lngQueryDiagnosisItemStatus(p_strRecipeNo, dr["orderdicid_chr"].ToString().Trim(), intCheck, out dtbDiagnosisItemStatus);
                }
                else
                {
                    continue;
                }

                if (ret > 0)
                {
                    Totalmny += ConvertObjToDecimal(dr["totalmny_dec"]);

                    if (dtbDiagnosisItemStatus != null && dtbDiagnosisItemStatus.Rows.Count > 0)
                    {
                        DataRow dtrTemp = dtbDiagnosisItemStatus.Rows[0];
                        int intStatus = 0;
                        int.TryParse(dtrTemp["isfinish"].ToString(), out intStatus);

                        if (intStatus == 1)
                        {
                            dgvrTemp.Cells["colChecked"].ReadOnly = true;
                            dgvrTemp.Cells["colState"].Value = "��ȷ��";
                            dgvrTemp.DefaultCellStyle.BackColor = Color.Red;
                        }
                    }

                    dgvrTemp.Tag = dr;
                }
            }
            #endregion

            #region 4��������Ŀ,ֻ����ʾ�ֶ���ӵģ�����ʾ��Ŀ������
            /*
             * ����ͨ���鿴�ǲ��Ǵ�������Ŀ�������Ƿ���ʾ,�磬�����÷���������ĿidΪ��
             * **/
            //6������
            ret = objDoct.m_mthFindRecipeDetail6(RecID, out dt, false, false);

            if (ret > 0)
            {
                int intRwoCount = dt.Rows.Count;
                for (int i1 = 0; i1 < intRwoCount; i1++)
                {
                    DataRow dtrTemp = dt.Rows[i1];

                    if (string.IsNullOrEmpty(dtrTemp["usageparentid_vchr"].ToString().Trim()))
                    {
                        intRowIndex = this.m_objViewer.dtgItem.Rows.Add();
                        DataGridViewRow dgvrTemp = this.m_objViewer.dtgItem.Rows[intRowIndex];

                        dgvrTemp.Cells["colChecked"].Value = "T";
                        dgvrTemp.Cells["colState"].Value = "δȷ��";
                        //if (intIsSendOth == 1)
                        //{
                        //    dgvrTemp.Cells["colChecked"].ReadOnly = true;
                        //    dgvrTemp.Cells["colState"].Value = "��ȷ��";
                        //    dgvrTemp.DefaultCellStyle.BackColor = Color.Red;
                        //}
                        //else
                        //{
                        //    //dgvrTemp.Cells["colChecked"].Value = "F";
                        //    dgvrTemp.Cells["colState"].Value = "δȷ��";
                        //}

                        //����
                        dgvrTemp.Cells["colRepNo"].Value = "";
                        //��Ŀ����
                        dgvrTemp.Cells["colItemName"].Value = dtrTemp["itemname"].ToString();
                        //ԭ���
                        dgvrTemp.Cells["colOriSpecification"].Value = dtrTemp["dec"].ToString();
                        //�¹��
                        dgvrTemp.Cells["colNewSpecification"].Value = "";
                        //Ƶ��
                        dgvrTemp.Cells["colFrequency"].Value = "";
                        //�÷�
                        dgvrTemp.Cells["colUsage"].Value = "";
                        //����
                        dgvrTemp.Cells["colQuantity"].Value = dtrTemp["quantity"].ToString();
                        //ԭ����
                        dgvrTemp.Cells["colOriPrice"].Value = dtrTemp["price"].ToString();
                        //�µ���
                        dgvrTemp.Cells["colNewPrice"].Value = "";
                        // ԭ�ϼƽ��
                        dgvrTemp.Cells["colSum"].Value = dtrTemp["summoney"].ToString();
                        //��Ŀ����
                        dgvrTemp.Cells["colItemAttribute"].Value = "oth";
                        //��ĿId
                        dgvrTemp.Cells["colItemID"].Value = dtrTemp["itemid"].ToString();
                        //���Ŀ�����뵥id
                        dgvrTemp.Cells["colAttachid"].Value = "";

                        if (dtrTemp["dec"].ToString().Trim() != dtrTemp["spec"].ToString().Trim())
                        {
                            dgvrTemp.Cells["colNewSpecification"].Value = dtrTemp["spec"].ToString();
                        }

                        if (this.ConvertObjToDecimal(dtrTemp["price"]) != this.ConvertObjToDecimal(dtrTemp["itemprice_mny"]))
                        {
                            dgvrTemp.Cells["colNewPrice"].Value = dtrTemp["itemprice_mny"].ToString();
                        }

                        ret = objDomain.m_lngQueryDiagnosisItemStatus(p_strRecipeNo, dtrTemp["outpatrecipedeid_chr"].ToString().Trim(), 2, out dtbDiagnosisItemStatus);

                        if (ret > 0)
                        {

                            Totalmny += ConvertObjToDecimal(dtrTemp["summoney"]);

                            if (dtbDiagnosisItemStatus != null && dtbDiagnosisItemStatus.Rows.Count > 0)
                            {
                                DataRow dtrTempStatus = dtbDiagnosisItemStatus.Rows[0];
                                int intStatus = 0;
                                int.TryParse(dtrTempStatus["isfinish"].ToString(), out intStatus);

                                if (intStatus == 1)
                                {
                                    dgvrTemp.Cells["colChecked"].ReadOnly = true;
                                    dgvrTemp.Cells["colState"].Value = "��ȷ��";
                                    dgvrTemp.DefaultCellStyle.BackColor = Color.Red;
                                }

                            }
                        }

                        dgvrTemp.Tag = dtrTemp;
                    }
                }
            }
            #endregion

        }
        #endregion

        #region ȷ��ѡ�����Ŀ
        /// <summary>
        /// ȷ��ѡ�����Ŀ
        /// </summary>
        public DataTable m_dtbSelectTable()
        {
            if (!m_blnIsCheckAllDataGridView())
            {

                if (MessageBox.Show("��ȷ��ִ�в��ֽ��Ѳ�����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return m_dtbChargeItem;
                }
            }

            m_objViewer.m_lstOrderDicItemID = new List<string>();
            DataTable dtbResult = null;
            if (this.m_objViewer.dtgItem.Rows.Count == 0)
            {
                return dtbResult;
            }

            int intRowCount = this.m_objViewer.dtgItem.Rows.Count;
            List<string> lstAttachOrderId = new List<string>();//��������Ŀid
            List<string> lstMedUsageParentId = new List<string>();  //�÷���������Ŀid
            List<string> lstOtherItemId = new List<string>(); //�ֶ���ӵ�������Ŀ
            List<string> lstMedItemId = new List<string>();//��ҩͬ����ʱ������ĿidΪ�գ�����itemid����
            string strOrderDicItemId = string.Empty;
            for (int i1 = 0; i1 < intRowCount; i1++)
            {
                if (this.m_objViewer.dtgItem.Rows[i1].Cells[0].Value.ToString() == "T")
                {
                    DataRow dr = this.m_objViewer.dtgItem.Rows[i1].Tag as DataRow;


                    string strItemAttribute = this.m_objViewer.dtgItem.Rows[i1].Cells["colItemAttribute"].Value.ToString().ToLower();
                    /*
                     * �Ǹ��ݲ鿴ҩƷ����顢�����ֵĲ�ѯ������ҩƷ��ϸ�����ѯ���з����Ա�
                     * �ó��Ĺ�������
                     * **/
                    if (strItemAttribute == "lis" || strItemAttribute == "test" || strItemAttribute == "ops")
                    {
                        string strArrachOrderId = dr["attachorderid_vchr"].ToString().Trim();

                        //���ѡ���������Ŀ,�������µ��ֹ�����ʱ������t_opr_outpatient_orderdic
                        strOrderDicItemId = this.m_objViewer.dtgItem.Rows[i1].Cells["colItemID"].Value.ToString().ToLower();
                        m_objViewer.m_lstOrderDicItemID.Add(strOrderDicItemId);

                        if (!string.IsNullOrEmpty(strArrachOrderId))
                        {
                            strArrachOrderId = strArrachOrderId.Substring(4, strArrachOrderId.Length - 4);
                            if (!lstAttachOrderId.Contains(strArrachOrderId))
                            {
                                lstAttachOrderId.Add(strArrachOrderId);
                            }
                        }
                    }
                    else if (strItemAttribute == "wm" || strItemAttribute == "cm")
                    {
                        string strTemp = dr["usageparentid_vchr"].ToString();
                        if (!string.IsNullOrEmpty(strTemp))
                        {
                            if (!lstMedUsageParentId.Contains(strTemp))
                            {
                                lstMedUsageParentId.Add(strTemp);
                            }
                        }
                        else
                        {
                            string strTid = string.Empty;// dr["itemid"].ToString();
                            if (strItemAttribute == "wm")
                            {
                                strTid = dr["itemid_chr"].ToString();
                            }
                            else
                            {
                                strTid = dr["itemid"].ToString();
                            }
                            if (!lstMedItemId.Contains(strTid))
                            {
                                lstMedItemId.Add(strTid);
                            }
                        }
                    }
                    else if (strItemAttribute == "oth")
                    {
                        string strTemp = dr["itemid"].ToString();
                        if (!string.IsNullOrEmpty(strTemp))
                        {
                            if (!lstOtherItemId.Contains(strTemp))
                            {
                                lstOtherItemId.Add(strTemp);
                            }
                        }
                    }
                }
            }

            if ((lstAttachOrderId != null && lstAttachOrderId.Count > 0) || (lstMedUsageParentId != null && lstMedUsageParentId.Count > 0) || (lstMedItemId != null && lstMedItemId.Count > 0) || (lstOtherItemId != null && lstOtherItemId.Count > 0))
            {
                string strExpression = string.Empty;
                StringBuilder sbAttachOrder = new StringBuilder(128);
                StringBuilder sbUsageParentId = new StringBuilder(512);
                StringBuilder strMedItemid = new StringBuilder(512);
                StringBuilder sbOtherItemId = new StringBuilder(64);

                //���˼��顢���ͼ��丽��������Ŀ
                if (lstAttachOrderId.Count > 0)
                {
                    foreach (string strTemp in lstAttachOrderId)
                    {
                        if (sbAttachOrder.Length > 0)
                        {
                            sbAttachOrder.Append(",");
                        }
                        sbAttachOrder.Append("'").Append(strTemp).Append("'");
                    }

                    strExpression = " orderid  in ( " + sbAttachOrder.ToString() + " ) ";
                }

                //����ҩƷ��Ŀ�ͼ��丽��������Ŀ
                if (lstMedUsageParentId.Count > 0)
                {
                    foreach (string strTemp in lstMedUsageParentId)
                    {
                        if (sbUsageParentId.Length > 0)
                        {
                            sbUsageParentId.Append(",");
                        }

                        sbUsageParentId.Append("'").Append(strTemp).Append("'").Append(",").Append("'").Append(strTemp.Substring(4, strTemp.Length - 4)).Append("'");
                        //sbUsageParentId.Append("'").Append(strTemp).Append("'");
                    }

                    if (strExpression.Length > 0)
                    {

                        strExpression += " or usageparentid_vchr in ( " + sbUsageParentId.ToString() + " )";
                        //strExpression += " or itemid in ( " + sbUsageParentId.ToString() + " )";
                    }
                    else
                    {
                        strExpression = " usageparentid_vchr in ( " + sbUsageParentId.ToString() + " )";
                    }
                }

                //��ҩͬ����ʱ����itemid����
                if (lstMedItemId.Count > 0)
                {
                    foreach (string strTemp in lstMedItemId)
                    {
                        if (strMedItemid.Length > 0)
                        {
                            strMedItemid.Append(",");
                        }
                        strMedItemid.Append("'").Append(strTemp).Append("'");
                    }

                    if (strExpression.Length > 0)
                    {
                        strExpression += " or itemid in ( " + strMedItemid.ToString() + " )";
                    }
                    else
                    {
                        strExpression = " itemid in ( " + strMedItemid.ToString() + " )";
                    }
                }

                //�����ֶ�����������Ŀ
                if (lstOtherItemId.Count > 0)
                {
                    foreach (string strTemp in lstOtherItemId)
                    {
                        if (sbOtherItemId.Length > 0)
                        {
                            sbOtherItemId.Append(",");
                        }

                        sbOtherItemId.Append("'").Append(strTemp).Append("'");
                    }

                    if (strExpression.Length > 0)
                    {
                        strExpression += " or itemid in ( " + sbOtherItemId + " )";
                    }
                    else
                    {
                        strExpression = " itemid in ( " + sbOtherItemId + " )";
                    }
                }

                m_dtbChargeItem.DefaultView.RowFilter = strExpression;
                if (m_dtbChargeItem.DefaultView.Count > 0)
                {
                    dtbResult = m_dtbChargeItem.DefaultView.ToTable();
                }
            }
            return dtbResult;
        }
        #endregion

        #region  �ж�DataGridView�Ƿ�ȫѡ

        private bool m_blnIsCheckAllDataGridView()
        {
            int intCount = m_objViewer.dtgItem.Rows.Count;
            for (int i1 = 0; i1 < intCount; i1++)
            {
                if (this.m_objViewer.dtgItem.Rows[i1].Cells[0].Value.ToString() == "F")
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region ����ѡ���ȡ����Ŀ���Ժϵ�����Ŀ���й���ѡ��
        /// <summary>
        /// �ϵ���Ŀ�Ĺ���ѡ��
        /// </summary>
        /// <param name="p_intRowIndex"></param>
        public void m_mthSelectMergeItem(int p_intRowIndex)
        {
            //Ŀ�����뵥id
            string strArrachid = "";
            string strItemAttribute = this.m_objViewer.dtgItem.Rows[p_intRowIndex].Cells["colItemAttribute"].Value.ToString().ToLower();
            if (strItemAttribute == "lis" || strItemAttribute == "test")
            {
                strArrachid = this.m_objViewer.dtgItem.Rows[p_intRowIndex].Cells["colAttachid"].Value.ToString().ToLower();
            }

            if (string.IsNullOrEmpty(strArrachid))
            {
                return;
            }

            bool blnIsSelected = false;

            bool.TryParse(this.m_objViewer.dtgItem.Rows[p_intRowIndex].Cells["colChecked"].EditedFormattedValue.ToString(), out blnIsSelected);

            if (!blnIsSelected)
            {
                m_mthAddMergeItemStatus(false, strArrachid);
            }
            else
            {
                m_mthAddMergeItemStatus(true, strArrachid);
            }
        }

        private void m_mthAddMergeItemStatus(bool p_blnFlag, string strArrachid)
        {
            int intCount = m_objViewer.dtgItem.Rows.Count;
            for (int i1 = 0; i1 < intCount; i1++)
            {
                DataGridViewRow dgvrTmp = this.m_objViewer.dtgItem.Rows[i1];
                string strItemAttribute = dgvrTmp.Cells["colItemAttribute"].Value.ToString().ToLower();

                if (strItemAttribute == "lis" || strItemAttribute == "test")
                {
                    if (strArrachid == dgvrTmp.Cells["colAttachid"].Value.ToString().ToLower())
                    {
                        if (p_blnFlag)
                        {
                            dgvrTmp.Cells["colChecked"].Value = "T";
                        }
                        else
                        {
                            dgvrTmp.Cells["colChecked"].Value = "F";
                        }
                    }
                }
            }
        }
        #endregion

        #region ת��������
        /// <summary>
        /// ת��������
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public decimal ConvertObjToDecimal(object obj)
        {
            try
            {
                if (obj != null && obj.ToString() != "")
                {
                    return Convert.ToDecimal(obj.ToString());

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        #endregion
    }
}
