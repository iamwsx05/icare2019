using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Drawing;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsCtl_RecipeConfirm : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsCtl_RecipeConfirm()
        {
            objSvc = new clsDcl_RecipeConfirm();
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmRecipeConfirm m_objViewer;
        clsDcl_RecipeConfirm objSvc = null;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmRecipeConfirm)frmMDI_Child_Base_in;
        }
        #endregion

        #region ���ݿ��Ų�ѯ������Ϣ
        /// <summary>
        /// ���ݿ��Ų�ѯ������Ϣ
        /// </summary>
        public void m_mthGetPatientInfo()
        {
            string strCardno = this.m_objViewer.txtCardno.Text.Trim().ToString();
            DataTable dtResult = new DataTable();
            clsRecipeInfo_VO[] objRI_VO = null;
            long lngRes = objSvc.m_lngGetPatientInfo(strCardno, out dtResult);
            if (lngRes > 0 && dtResult.Rows.Count > 0)
            {
                string strPatientID = dtResult.Rows[0]["PATIENTID_CHR"].ToString();
                this.m_objViewer.txtName.Text = dtResult.Rows[0]["lastname_vchr"].ToString();
                this.m_objViewer.txtSex.Text = dtResult.Rows[0]["sex_chr"].ToString();
                lngRes = objSvc.m_mthLoadRecipeNo(strCardno, out objRI_VO);
                if (lngRes > 0 && objRI_VO != null)
                {
                    this.m_mthFillTreeView(objRI_VO);
                }
            }
        }
        #endregion

        #region ����TreeView
        /// <summary>
        /// ����TreeView
        /// </summary>
        /// <param name="p_objRI_VO"></param>
        private void m_mthFillTreeView(clsRecipeInfo_VO[] p_objRI_VO)
        {
            this.m_objViewer.trvRecipe.Nodes.Clear();
            clsRecipeInfo_VO objRI_VO;
            string strdate;//����
            bool flag;
            for (int i = 0; i < p_objRI_VO.Length; i++)
            {
                //if (p_objRI_VO[i].m_strIsGreen != "1")
                //{
                //    continue;
                //}
                objRI_VO = p_objRI_VO[i];
                flag = true;
                if (i == 0)
                {
                    m_mthAddNewNode(objRI_VO);
                }
                else
                {
                    strdate = DateTime.Parse(objRI_VO.m_strCreatTime).ToString("yyyy��MM��dd��");
                    for (int i2 = 0; i2 < this.m_objViewer.trvRecipe.Nodes.Count; i2++)
                    {
                        if (strdate == this.m_objViewer.trvRecipe.Nodes[i2].Text.Trim())
                        {
                            TreeNode subtn = new TreeNode(objRI_VO.m_strOUTPATRECIPEID_CHR);
                            //							switch(objRI_VO.m_intPSTATUS_INT)
                            //							{
                            //								case 0:
                            //									subtn.ForeColor=Color.Black;
                            //									break;
                            //								case 1:
                            //									subtn.ForeColor=Color.Blue;
                            //									break;
                            //								default:
                            //									subtn.ForeColor=Color.Red;
                            //									break;
                            //							}
                            subtn.ForeColor = this.m_mthCreatColor(objRI_VO.m_intPSTATUS_INT);
                            subtn.Tag = objRI_VO;
                            this.m_objViewer.trvRecipe.Nodes[i2].Nodes.Add(subtn);
                            flag = false;
                            break;
                        }

                    }
                    if (flag)
                    {
                        m_mthAddNewNode(objRI_VO);
                    }
                }

            }
            this.m_mthShowTreeView();
        }
        #endregion

        #region ����һ���ڵ�
        private void m_mthAddNewNode(clsRecipeInfo_VO objRI_VO)
        {
            string strdate = DateTime.Parse(objRI_VO.m_strCreatTime).ToString("yyyy��MM��dd��");
            TreeNode tn = new TreeNode(strdate);
            TreeNode subtn = new TreeNode(objRI_VO.m_strOUTPATRECIPEID_CHR);
            //			switch(objRI_VO.m_intPSTATUS_INT)
            //			{
            //				case 1:
            //					subtn.ForeColor=Color.Brown;
            //					break;
            //				case 2:
            //					subtn.ForeColor=Color.Red;
            //					break;
            //			}
            subtn.ForeColor = this.m_mthCreatColor(objRI_VO.m_intPSTATUS_INT);
            subtn.Tag = objRI_VO;
            tn.Nodes.Add(subtn);
            this.m_objViewer.trvRecipe.Nodes.Add(tn);
        }

        #endregion

        #region
        private Color m_mthCreatColor(int p_flag)
        {
            Color ret = Color.Black;
            switch (p_flag)
            {
                case 1:
                    ret = Color.Green;
                    break;
                case -2:
                    ret = Color.Brown;
                    break;
                case 2:
                    ret = Color.Red;
                    break;
                case 3:
                    ret = Color.Red;
                    break;
                case 5:
                    ret = Color.Orange;
                    break;
            }
            return ret;
        }
        #endregion

        #region ��ʾtreeView
        private void m_mthShowTreeView()
        {
            this.m_objViewer.trvRecipe.Nodes[0].Expand();
            this.m_objViewer.trvRecipe.Nodes[0].TreeView.SelectedNode = this.m_objViewer.trvRecipe.Nodes[0].Nodes[0];
            this.m_objViewer.trvRecipe.Focus();
            this.m_objViewer.trvRecipe.Select();
        }
        #endregion

        #region ѡ��ĳ���󣬲�ѯ����
        /// <summary>
        /// ѡ��ĳ���󣬲�ѯ����
        /// </summary>
        public void m_mthGetRecipeInfo()
        {
            this.m_objViewer.objListApp.Clear();
            if (this.m_objViewer.trvRecipe.SelectedNode != null && this.m_objViewer.trvRecipe.SelectedNode.Tag != null)
            {
                try
                {
                    clsRecipeInfo_VO objRI_VO = (clsRecipeInfo_VO)this.m_objViewer.trvRecipe.SelectedNode.Tag;
                    DataTable dt = new DataTable();
                    this.m_objViewer.dgvRecipe.Rows.Clear();
                    this.m_objViewer.dgvDetails.Rows.Clear();
                    if (true)
                    {
                        DataTable dtbDiagnosisItemStatus = null;
                        long ret = 0;
                        //������Ŀ
                        //����
                        int intCheck = 1;
                        //���
                        int intTest = 2;
                        //����
                        int intOps = 3;
                        int intIsSendOth = 0;
                        int intRowIndex = 0;
                        decimal Totalmny = 0;
                        objSvc.m_mthRecipeDetail(objRI_VO.m_strOUTPATRECIPEID_CHR, out dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            intRowIndex = this.m_objViewer.dgvRecipe.Rows.Add();

                            DataGridViewRow dgvrTemp = this.m_objViewer.dgvRecipe.Rows[intRowIndex];
                            DataRow dr = dt.Rows[i];

                            //����
                            dgvrTemp.Cells["colRepNo"].Value = dr["id"].ToString();
                            //��Ŀ����
                            dgvrTemp.Cells["colItemName"].Value = dr["name"].ToString();
                            //������Ŀid
                            dgvrTemp.Cells["colItemID"].Value = dr["itemid_chr"].ToString();
                            dgvrTemp.Cells["colPatientID"].Value = dr["usageparentid_vchr"].ToString();
                            dgvrTemp.Cells["coldeid"].Value = dr["outpatrecipedeid_chr"].ToString();
                            dgvrTemp.Cells["colChecked"].Value = "F";
                            dgvrTemp.Cells["colstatus"].Value = "";
                            dgvrTemp.Cells["colType"].Value = dr["tableindex_int"].ToString();
                            if (dr["status"].ToString() == "1")
                            {
                                dgvrTemp.Cells["colstatus"].Value = "��ȷ��";
                                dgvrTemp.Cells["colChecked"].Value = "T";
                                dgvrTemp.Cells["colChecked"].ReadOnly = true;
                                dgvrTemp.ReadOnly = true;
                                dgvrTemp.DefaultCellStyle.BackColor = Color.Orange;
                            }
                            //���Ŀ�����뵥id
                            //dgvrTemp.Cells["colAttachid"].Value = dr["attachid_vchr"].ToString();

                            //string ordertype = dr["tableindex_int"].ToString();
                            //string strItemid = dr["itemid_chr"].ToString().Trim();
                            //if (dtbDiagnosisItemStatus != null)
                            //{
                            //    dtbDiagnosisItemStatus.Dispose();
                            //    dtbDiagnosisItemStatus = null;
                            //}
                            //if (ordertype == "3")
                            //{
                            //    dgvrTemp.Cells["colItemAttribute"].Value = "lis";
                            //    ret = objSvc.m_lngQueryDiagnosisItemStatus(p_strRecipeNo, dr["orderdicid_chr"].ToString().Trim(), intCheck, out dtbDiagnosisItemStatus);
                            //}
                            //else if (ordertype == "4")
                            //{
                            //    dgvrTemp.Cells["colItemAttribute"].Value = "test";
                            //    ret = objSvc.m_lngQueryDiagnosisItemStatus(p_strRecipeNo, dr["orderdicid_chr"].ToString().Trim(), intTest, out dtbDiagnosisItemStatus);
                            //}
                            //else if (ordertype == "5")
                            //{
                            //    dgvrTemp.Cells["colItemAttribute"].Value = "ops";
                            //    if (objSvc.m_blnChkopsitem(strItemid))
                            //    {
                            //        ret = objSvc.m_lngQueryDiagnosisItemStatus(p_strRecipeNo, dr["orderdicid_chr"].ToString().Trim(), intOps, out dtbDiagnosisItemStatus);
                            //    }
                            //    else
                            //    {
                            //        this.m_objViewer.dgvRecipe.Rows.RemoveAt(intRowIndex);
                            //        continue;
                            //    }
                            //}
                            //else
                            //{
                            //    continue;
                            //}

                            //if (ret > 0)
                            //{
                            //    Totalmny += ConvertObjToDecimal(dr["totalmny_dec"]);

                            //    if (dtbDiagnosisItemStatus != null && dtbDiagnosisItemStatus.Rows.Count > 0)
                            //    {
                            //        DataRow dtrTemp = dtbDiagnosisItemStatus.Rows[0];
                            //        int intStatus = 0;
                            //        int.TryParse(dtrTemp["isfinish"].ToString(), out intStatus);

                            //        if (intStatus == 1)
                            //        {
                            //            dgvrTemp.Cells["colChecked"].Value = "T";
                            //            dgvrTemp.Cells["colChecked"].ReadOnly = true;
                            //            dgvrTemp.Cells["colState"].Value = "����";
                            //            dgvrTemp.DefaultCellStyle.BackColor = Color.Red;
                            //        }
                            //        else
                            //        {
                            //            dgvrTemp.Cells["colChecked"].Value = "F";
                            //            dgvrTemp.Cells["colState"].Value = "δ��";
                            //        }
                            //    }

                            //    dgvrTemp.Tag = dr;
                            //}
                        }
                    }
                    else
                    {
                        this.m_objViewer.dgvRecipe.Columns["colchecked"].Visible = true;
                        string p_strRecipeNo = objRI_VO.m_strOUTPATRECIPEID_CHR;
                        //������Ŀ��״̬���Ƿ���ɸ�����
                        DataTable dtbDiagnosisItemStatus = null;
                        long ret = 0;
                        #region 3��������Ŀ
                        //������Ŀ
                        //����
                        int intCheck = 1;
                        //���
                        int intTest = 2;
                        //����
                        int intOps = 3;
                        int intIsSendOth = 0;
                        int intRowIndex = 0;
                        decimal Totalmny = 0;
                        ret = objSvc.m_mthFindRecipeDetailOrder(objRI_VO.m_strOUTPATRECIPEID_CHR, out dt);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            intRowIndex = this.m_objViewer.dgvRecipe.Rows.Add();

                            DataGridViewRow dgvrTemp = this.m_objViewer.dgvRecipe.Rows[intRowIndex];
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

                            //���Ŀ�����뵥id
                            dgvrTemp.Cells["colAttachid"].Value = dr["attachid_vchr"].ToString();

                            string ordertype = dr["tableindex_int"].ToString();
                            string strItemid = dr["itemid_chr"].ToString().Trim();
                            if (dtbDiagnosisItemStatus != null)
                            {
                                dtbDiagnosisItemStatus.Dispose();
                                dtbDiagnosisItemStatus = null;
                            }
                            if (ordertype == "3")
                            {
                                dgvrTemp.Cells["colItemAttribute"].Value = "lis";
                                ret = objSvc.m_lngQueryDiagnosisItemStatus(p_strRecipeNo, dr["orderdicid_chr"].ToString().Trim(), intCheck, out dtbDiagnosisItemStatus);
                            }
                            else if (ordertype == "4")
                            {
                                dgvrTemp.Cells["colItemAttribute"].Value = "test";
                                ret = objSvc.m_lngQueryDiagnosisItemStatus(p_strRecipeNo, dr["orderdicid_chr"].ToString().Trim(), intTest, out dtbDiagnosisItemStatus);
                            }
                            else if (ordertype == "5")
                            {
                                dgvrTemp.Cells["colItemAttribute"].Value = "ops";
                                if (objSvc.m_blnChkopsitem(strItemid))
                                {
                                    ret = objSvc.m_lngQueryDiagnosisItemStatus(p_strRecipeNo, dr["orderdicid_chr"].ToString().Trim(), intOps, out dtbDiagnosisItemStatus);
                                }
                                else
                                {
                                    this.m_objViewer.dgvRecipe.Rows.RemoveAt(intRowIndex);
                                    continue;
                                }
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
                                        dgvrTemp.Cells["colChecked"].Value = "T";
                                        dgvrTemp.Cells["colChecked"].ReadOnly = true;
                                        dgvrTemp.Cells["colState"].Value = "����";
                                        dgvrTemp.DefaultCellStyle.BackColor = Color.Red;
                                    }
                                    else
                                    {
                                        dgvrTemp.Cells["colChecked"].Value = "F";
                                        dgvrTemp.Cells["colState"].Value = "δ��";
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
                        ret = objSvc.m_mthFindRecipeDetail6(p_strRecipeNo, out dt, false);

                        if (ret > 0)
                        {
                            int intRwoCount = dt.Rows.Count;
                            for (int i1 = 0; i1 < intRwoCount; i1++)
                            {
                                DataRow dtrTemp = dt.Rows[i1];

                                if (string.IsNullOrEmpty(dtrTemp["usageparentid_vchr"].ToString().Trim()))
                                {
                                    intRowIndex = this.m_objViewer.dgvRecipe.Rows.Add();
                                    DataGridViewRow dgvrTemp = this.m_objViewer.dgvRecipe.Rows[intRowIndex];


                                    if (intIsSendOth == 1)
                                    {
                                        dgvrTemp.Cells["colChecked"].Value = "T";
                                        dgvrTemp.Cells["colChecked"].ReadOnly = true;
                                        dgvrTemp.Cells["colState"].Value = "�������";
                                        dgvrTemp.DefaultCellStyle.BackColor = Color.Red;
                                    }
                                    else
                                    {
                                        dgvrTemp.Cells["colChecked"].Value = "F";
                                        dgvrTemp.Cells["colState"].Value = "δ�����";
                                    }

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

                                    Totalmny += ConvertObjToDecimal(dtrTemp["summoney"]);

                                    dgvrTemp.Tag = dtrTemp;
                                }
                            }
                        }
                        #endregion

                        if (this.m_objViewer.dgvRecipe.Rows.Count == 0)
                        {
                            intRowIndex = this.m_objViewer.dgvRecipe.Rows.Add();
                            DataGridViewRow dgvrTemp = this.m_objViewer.dgvRecipe.Rows[intRowIndex];
                            this.m_objViewer.dgvRecipe.Columns["colchecked"].Visible = false;
                            this.m_objViewer.dgvRecipe.Columns["colItemName"].Width = 300;
                            dgvrTemp.Cells["colItemName"].Value = "û����Ҫȷ�ϵļ��顢��顢��������Ŀ";
                            dgvrTemp.Cells["colItemName"].ReadOnly = true;
                        }
                    }
                }
                catch (Exception objEX)
                {
                    com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                    objLog.LogError(objEX);
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

        #region ����ѡ���ȡ����Ŀ���Ժϵ�����Ŀ���й���ѡ��
        /// <summary>
        /// ����ѡ���ȡ����Ŀ���Ժϵ�����Ŀ���й���ѡ��
        /// </summary>
        public void m_mthSelectMergeItem(int p_intRowIndex)
        {
            //Ŀ�����뵥id
            string strArrachid = "";
            string strItemAttribute = this.m_objViewer.dgvRecipe.Rows[p_intRowIndex].Cells["colItemAttribute"].Value.ToString().ToLower();
            if (strItemAttribute == "lis" || strItemAttribute == "test")
            {
                strArrachid = this.m_objViewer.dgvRecipe.Rows[p_intRowIndex].Cells["colAttachid"].Value.ToString().ToLower();
            }

            if (string.IsNullOrEmpty(strArrachid))
            {
                return;
            }

            bool blnIsSelected = false;

            bool.TryParse(this.m_objViewer.dgvRecipe.Rows[p_intRowIndex].Cells["colChecked"].EditedFormattedValue.ToString(), out blnIsSelected);

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
            int intCount = m_objViewer.dgvRecipe.Rows.Count;
            for (int i1 = 0; i1 < intCount; i1++)
            {
                DataGridViewRow dgvrTmp = this.m_objViewer.dgvRecipe.Rows[i1];
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

        #region ��ѯ����Ŀ����ϸ
        /// <summary>
        /// ��ѯ����Ŀ����ϸ
        /// </summary>
        /// <param name="p_strRecipeNO"></param>
        /// <param name="p_strPatientID"></param>
        public void m_mthGetItemDetails(string p_strRecipeNO, string p_strItemID, string p_strPatientID, string p_strPaitentDeID, string p_strType)
        {
            if (string.IsNullOrEmpty(p_strRecipeNO))
            {
                return;
            }
            try
            {                
                DataTable dtDetails = new DataTable();
                long l = objSvc.m_lngGetItemDetails(p_strRecipeNO, p_strPatientID, p_strItemID, p_strType, out dtDetails);
                int intRowIndex = 0;
                if (l > 0 && dtDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDetails.Rows.Count; i++)
                    {
                        intRowIndex = this.m_objViewer.dgvDetails.Rows.Add();

                        DataGridViewRow dgvrTemp = this.m_objViewer.dgvDetails.Rows[intRowIndex];
                        DataRow dr = dtDetails.Rows[i];

                        //����
                        dgvrTemp.Cells["colRecipeno"].Value = dr["id"].ToString();
                        //��Ŀ����
                        dgvrTemp.Cells["colNAME"].Value = dr["name"].ToString();
                        //����
                        dgvrTemp.Cells["colCount"].Value = dr["count"].ToString();
                        //����
                        dgvrTemp.Cells["colPrice"].Value = dr["price"].ToString();
                        //���
                        dgvrTemp.Cells["colDec"].Value = dr["dec"].ToString();
                        //��ϸID
                        dgvrTemp.Cells["colrecipedeid"].Value = dr["outpatrecipedeid_chr"].ToString();
                        //����ϸID
                        dgvrTemp.Cells["colpatientdeid"].Value = p_strPaitentDeID;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                objLog.LogError(objEx);
            }
        }
        #endregion

        #region ȷ�Ϻ�������뵥״̬
        /// <summary>
        /// ȷ�Ϻ�������뵥״̬
        /// </summary>
        public void m_mthConfirmApp()
        {
            if (this.m_objViewer.dgvDetails.Rows.Count > 0)
            {
                int num = 0;
               
                for (int i = 0; i < this.m_objViewer.dgvRecipe.Rows.Count; i++)
                {
                    DataGridViewRow dr = this.m_objViewer.dgvRecipe.Rows[i];
                    if (dr.Cells["colChecked"].Value.ToString() == "T" && !dr.Cells["colstatus"].Value.ToString().Contains("��ȷ��"))
                    {
                        num++;
                    }
                }
                clsOutpatientRecipe_VO[] objRecipeVO = new clsOutpatientRecipe_VO[num];
                int numbers = -1;
                for (int i = 0; i < this.m_objViewer.dgvRecipe.Rows.Count; i++)
                {
                    DataGridViewRow dr = this.m_objViewer.dgvRecipe.Rows[i];
                    if (dr.Cells["colChecked"].Value.ToString() == "T" && !dr.Cells["colstatus"].Value.ToString().Contains("��ȷ��"))
                    {
                        numbers++;
                        objRecipeVO[numbers] = new clsOutpatientRecipe_VO();
                        objRecipeVO[numbers].m_strOutpatRecipeNo = dr.Cells["colRepNo"].Value.ToString();
                        objRecipeVO[numbers].m_strOutpatRecipeID = dr.Cells["coldeid"].Value.ToString();
                        objRecipeVO[numbers].strDIAG_VCHR = this.m_objViewer.LoginInfo.m_strEmpID.ToString();
                    }
                }
                objSvc.m_lngModiffyAppStatus(objRecipeVO);
                this.m_mthGetRecipeInfo();
            }
            else
            {
                MessageBox.Show("��ѡ����Ҫȷ�ϵ���Ŀ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        #endregion

        #region ����ѡĳ��ʱ��ȥ����ϸ�е���Ӧ��Ŀ
        /// <summary>
        /// ����ѡĳ��ʱ��ȥ����ϸ�е���Ӧ��Ŀ
        /// </summary>
        /// <param name="p_strPatientDeID"></param>
        public void m_mthDeleteItemDetails(string p_strPatientDeID)
        {
            if (this.m_objViewer.dgvDetails.Rows.Count > 0)
            {
                for (int i = 0; i < this.m_objViewer.dgvDetails.Rows.Count; i++)
                {
                    string strTemp = this.m_objViewer.dgvDetails.Rows[i].Cells["colpatientdeid"].Value.ToString();
                    if (p_strPatientDeID == strTemp)
                    {
                        this.m_objViewer.dgvDetails.Rows.RemoveAt(i);
                        i--;
                    }
                }
                //foreach (DataGridViewRow dr in this.m_objViewer.dgvDetails.Rows)
                //{
                //    string strTemp = dr.Cells["colpatientdeid"].Value.ToString();
                //    if (p_strPatientDeID == strTemp)
                //    {
                //        this.m_objViewer.dgvDetails.Rows.Remove(dr);
                //    }
                //}
            }
        }
        #endregion

        #region ȡ��ȷ��
        /// <summary>
        /// ȡ��ȷ��
        /// </summary>
        public void m_mthItemsCancel()
        {
            if (this.m_objViewer.dgvRecipe.SelectedRows.Count > 0)
            {
                try
                {
                    string strItemdid = this.m_objViewer.dgvRecipe.CurrentRow.Cells["coldeid"].Value.ToString();
                    string strEmpid = this.m_objViewer.LoginInfo.m_strEmpID;
                    long lngRes = objSvc.m_lngItemsCancel(strItemdid, strEmpid);
                    if(lngRes >0)
                    {
                        this.m_mthGetRecipeInfo();
                    }
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                    objLog.LogError(objEx);
                }               
            }
        }
        #endregion

        #region Ȩ���ж�
        /// <summary>
        /// Ȩ���ж�
        /// </summary>
        public void m_mthCompetence()
        {
            if (!objSvc.m_blnCompetence(this.m_objViewer.LoginInfo.m_strEmpID))
            {
               // this.m_objViewer.contextMenuStrip1.Visible = false;
                this.m_objViewer.dgvRecipe.ContextMenuStrip = null;
            }
        }
        #endregion
    }
}
