using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ���ô�����ϢUI��
    /// </summary>
    public partial class frmDoctReUseInfo : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// �ж��Ƿ�����ѡ��ȱҩ true ����, false ������
        /// </summary>
        internal bool LackMedicine = true;
        /// <summary>
        /// ��������¼VO
        /// </summary>
        private clsRecipeInfo_VO RecipeInfo_VO;
        /// <summary>
        /// ������ϸ������
        /// </summary>
        internal Hashtable hasItem = new Hashtable();
        /// <summary>
        /// ������Ŀ-��ϸ������
        /// </summary>
        internal Hashtable hasEntry = new Hashtable();
        /// <summary>
        /// ��Ŀ���뷽ʽ 0 �շ���ϸ 1 ������Ŀ
        /// </summary>
        internal int ItemInputMode = 0;
        /// <summary>
        /// ����
        /// </summary>
        public frmDoctReUseInfo(clsRecipeInfo_VO objRecinfo)
        {
            InitializeComponent();

            RecipeInfo_VO = objRecinfo;
        }

        internal bool IsChildPrice { get; set; }

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

        #region ��ʾ��ʷ����
        /// <summary>
        /// ��ʾ��ʷ����
        /// </summary>
        public void m_mthShow()
        {
            string RecID = this.RecipeInfo_VO.m_strOUTPATRECIPEID_CHR;

            int row = 0;
            long ret = 0;
            string ItemID = "";
            DataTable dt;
            DataTable EntryDt = new DataTable();

            decimal Totalmny = 0;
            clsDcl_DoctorWorkstation objDoct = new clsDcl_DoctorWorkstation();

            #region isChildPrice

            bool isChildPrice = false;
            if (new clsDcl_YB().IsUseChildPrice())
                isChildPrice = (new clsBrithdayToAge()).IsChild(objDoct.GetBirthdayByRecipeId(RecID));

            #endregion

            //1����ҩ
            ret = objDoct.m_mthFindRecipeDetail1(RecID, out dt, false, this.IsChildPrice);
            if (ret > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    //0 ״̬ 1 ���� 2 ��Ŀ���� 3 ԭ��� 4 �¹�� 5 Ƶ�� 6 �÷� 7 ���� 8 ԭ���� 9 �µ��� 10 ԭ�ϼƽ�� 11 ��Ŀ���� 12 �����ܽ�� 13 �������� 14 ������λ���� 
                    string[] s = new string[15];

                    string NewUnit = "";
                    string NewDosageUnit = "";

                    ItemID = dr["itemid_chr"].ToString();

                    s[0] = "T";

                    if (dr["rowno_chr"].ToString().Trim() == "0")
                    {
                        s[1] = "";
                    }
                    else
                    {
                        s[1] = dr["rowno_chr"].ToString().Trim();
                    }

                    s[2] = dr["itemname_vchr"].ToString();
                    s[3] = dr["itemspec_vchr"].ToString();

                    if (dr["itemspec_vchr"].ToString().Trim() != dr["spec"].ToString().Trim())
                    {
                        s[4] = dr["spec"].ToString().Trim();
                    }
                    else
                    {
                        s[4] = "";
                    }


                    s[5] = dr["freqname_chr"].ToString();
                    s[6] = dr["usagename_vchr"].ToString();
                    s[7] = dr["tolqty_dec"].ToString();
                    s[8] = dr["unitprice_mny"].ToString();
                    s[9] = "";
                    s[10] = dr["tolprice_mny"].ToString();
                    s[11] = "1";
                    s[12] = dr["toldiffprice_mny"].ToString();
                    s[13] = dr["tradeprice_mny"].ToString();
                    s[14] = dr["subtrademoney"].ToString();
                    //�жϴ�С��λ
                    if (dr["opchargeflg_int"].ToString().Trim() == "0")
                    {
                        if (this.ConvertObjToDecimal(dr["unitprice_mny"]) != this.ConvertObjToDecimal(dr["itemprice_mny"]))
                        {
                            s[9] = dr["itemprice_mny"].ToString();
                        }

                        NewUnit = dr["itemopunit_chr"].ToString();
                    }
                    else
                    {
                        if (this.ConvertObjToDecimal(dr["unitprice_mny"]) != this.ConvertObjToDecimal(dr["submoney"]))
                        {
                            s[9] = dr["submoney"].ToString();
                        }

                        NewUnit = dr["itemipunit_chr"].ToString();
                    }

                    NewDosageUnit = dr["dosageunit"].ToString().Trim();

                    Totalmny += ConvertObjToDecimal(dr["tolprice_mny"]);
                    row = this.dtgItem.Rows.Add(s);

                    //if (dr["noqtyflag_int"].ToString() == "1")
                    //{
                    //    this.dtgItem.Rows[row].Cells[0].Value = "F";
                    //    this.dtgItem.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                    //    if (!LackMedicine)
                    //    {
                    //        this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                    //    }
                    //}

                    if ((dr["dosageunit_chr"].ToString().Trim() != NewDosageUnit) || (dr["unitid_chr"].ToString().Trim() != NewUnit) || (s[4].ToString().Trim() != ""))
                    {
                        this.dtgItem.Rows[row].Cells[0].Value = "F";
                        this.dtgItem.Rows[row].DefaultCellStyle.ForeColor = Color.Green;
                        this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                    }

                    if (dr["ifstop_int"].ToString() == "1")
                    {
                        this.dtgItem.Rows[row].Cells[0].Value = "F";
                        this.dtgItem.Rows[row].DefaultCellStyle.ForeColor = Color.DimGray;
                        this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                    }

                    this.dtgItem.Rows[row].Tag = dr;
                }
            }

            //2����ҩ
            ret = objDoct.m_mthFindRecipeDetail2(RecID, out dt, false, this.IsChildPrice);
            if (ret > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    //0 ״̬ 1 ���� 2 ��Ŀ���� 3 ԭ��� 4 �¹�� 5 Ƶ�� 6 �÷� 7 ���� 8 ԭ���� 9 �µ��� 10 ԭ�ϼƽ�� 11 ��Ŀ���� 12 �����ܽ�� 13 �������� 14 ������λ���� 
                    string[] s = new string[15];

                    string NewUnit = "";

                    ItemID = dr["itemid"].ToString();

                    s[0] = "T";
                    s[1] = "";
                    s[2] = dr["itemname"].ToString();
                    s[3] = dr["dec"].ToString();
                    s[4] = "";
                    s[5] = "";
                    s[6] = dr["usagename_vchr"].ToString();
                    s[7] = Convert.ToString(ConvertObjToDecimal(dr["times"]) * ConvertObjToDecimal(dr["min_qty_dec"]));
                    s[8] = dr["price"].ToString();
                    s[9] = "";
                    s[10] = dr["summoney"].ToString();
                    s[11] = "2";
                    s[12] = dr["toldiffprice_mny"].ToString();
                    s[13] = dr["tradeprice_mny"].ToString();
                    s[14] = dr["subtrademoney"].ToString();
                    if (dr["dec"].ToString().Trim() != dr["spec"].ToString().Trim())
                    {
                        s[4] = dr["spec"].ToString();
                    }

                    if (this.ConvertObjToDecimal(dr["price"]) != this.ConvertObjToDecimal(dr["submoney"]))
                    {
                        s[9] = dr["submoney"].ToString();
                    }

                    NewUnit = dr["dosageunit"].ToString().Trim();

                    Totalmny += ConvertObjToDecimal(dr["summoney"]);
                    row = this.dtgItem.Rows.Add(s);

                    //if (dr["noqtyflag_int"].ToString() == "1")
                    //{
                    //    this.dtgItem.Rows[row].Cells[0].Value = "F";
                    //    this.dtgItem.Rows[row].DefaultCellStyle.BackColor = Color.Red;
                    //    if (!LackMedicine)
                    //    {
                    //        this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                    //    }
                    //}

                    if ((dr["unit"].ToString().Trim() != NewUnit) || (s[4].ToString().Trim() != ""))
                    {
                        this.dtgItem.Rows[row].Cells[0].Value = "F";
                        this.dtgItem.Rows[row].DefaultCellStyle.ForeColor = Color.Green;
                        this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                    }

                    if (dr["ifstop_int"].ToString() == "1")
                    {
                        this.dtgItem.Rows[row].Cells[0].Value = "F";
                        this.dtgItem.Rows[row].DefaultCellStyle.BackColor = Color.DarkGray;
                        this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                    }

                    this.dtgItem.Rows[row].Tag = dr;
                }
            }

            if (ItemInputMode == 0)
            {
                //3������
                ret = objDoct.m_mthFindRecipeDetail3(RecID, out dt, false, this.IsChildPrice);
                if (ret > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        //0 ״̬ 1 ���� 2 ��Ŀ���� 3 ԭ��� 4 �¹�� 5 Ƶ�� 6 �÷� 7 ���� 8 ԭ���� 9 �µ��� 10 ԭ�ϼƽ�� 11 ��Ŀ����
                        string[] s = new string[12];

                        string NewUnit = "";

                        ItemID = dr["itemid"].ToString();

                        s[0] = "T";
                        s[1] = "";
                        s[2] = dr["itemname"].ToString();
                        s[3] = dr["dec"].ToString();
                        s[4] = "";
                        s[5] = "";
                        s[6] = "";
                        s[7] = dr["quantity"].ToString();
                        s[8] = dr["price"].ToString();
                        s[9] = "";
                        s[10] = dr["summoney"].ToString();
                        s[11] = "3";

                        if (dr["dec"].ToString().Trim() != dr["spec"].ToString().Trim())
                        {
                            s[4] = dr["spec"].ToString();
                        }

                        if (this.ConvertObjToDecimal(dr["price"]) != this.ConvertObjToDecimal(dr["itemprice_mny"]))
                        {
                            s[9] = dr["itemprice_mny"].ToString();
                        }

                        NewUnit = dr["itemunit_chr"].ToString().Trim();

                        Totalmny += ConvertObjToDecimal(dr["summoney"]);
                        row = this.dtgItem.Rows.Add(s);

                        if ((dr["unit"].ToString().Trim() != NewUnit) || (s[4].ToString().Trim() != ""))
                        {
                            this.dtgItem.Rows[row].Cells[0].Value = "F";
                            this.dtgItem.Rows[row].DefaultCellStyle.ForeColor = Color.Green;
                            this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                        }

                        if (dr["ifstop_int"].ToString() == "1")
                        {
                            this.dtgItem.Rows[row].Cells[0].Value = "F";
                            this.dtgItem.Rows[row].DefaultCellStyle.BackColor = Color.DarkGray;
                            this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                        }

                        this.dtgItem.Rows[row].Tag = dr;
                    }
                }

                //4�����
                ret = objDoct.m_mthFindRecipeDetail4(RecID, out dt, false, isChildPrice);
                if (ret > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        //0 ״̬ 1 ���� 2 ��Ŀ���� 3 ԭ��� 4 �¹�� 5 Ƶ�� 6 �÷� 7 ���� 8 ԭ���� 9 �µ��� 10 ԭ�ϼƽ�� 11 ��Ŀ����
                        string[] s = new string[12];

                        string NewUnit = "";

                        ItemID = dr["itemid"].ToString();

                        s[0] = "T";
                        s[1] = "";
                        s[2] = dr["itemname"].ToString();
                        s[3] = dr["dec"].ToString();
                        s[4] = "";
                        s[5] = "";
                        s[6] = "";
                        s[7] = dr["quantity"].ToString();
                        s[8] = dr["price"].ToString();
                        s[9] = "";
                        s[10] = dr["summoney"].ToString();
                        s[11] = "4";

                        if (dr["dec"].ToString().Trim() != dr["spec"].ToString().Trim())
                        {
                            s[4] = dr["spec"].ToString();
                        }

                        if (this.ConvertObjToDecimal(dr["price"]) != this.ConvertObjToDecimal(dr["itemprice_mny"]))
                        {
                            s[9] = dr["itemprice_mny"].ToString();
                        }
                        NewUnit = dr["itemunit_chr"].ToString().Trim();

                        Totalmny += ConvertObjToDecimal(dr["summoney"]);
                        row = this.dtgItem.Rows.Add(s);

                        if ((dr["unit"].ToString().Trim() != NewUnit) || (s[4].ToString().Trim() != ""))
                        {
                            this.dtgItem.Rows[row].Cells[0].Value = "F";
                            this.dtgItem.Rows[row].DefaultCellStyle.ForeColor = Color.Green;
                            this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                        }

                        if (dr["ifstop_int"].ToString() == "1")
                        {
                            this.dtgItem.Rows[row].Cells[0].Value = "F";
                            this.dtgItem.Rows[row].DefaultCellStyle.BackColor = Color.DarkGray;
                            this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                        }

                        this.dtgItem.Rows[row].Tag = dr;
                    }
                }

                //5����������
                ret = objDoct.m_mthFindRecipeDetail5(RecID, out dt, false, this.IsChildPrice);
                if (ret > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        //0 ״̬ 1 ���� 2 ��Ŀ���� 3 ԭ��� 4 �¹�� 5 Ƶ�� 6 �÷� 7 ���� 8 ԭ���� 9 �µ��� 10 ԭ�ϼƽ�� 11 ��Ŀ����
                        string[] s = new string[12];

                        string NewUnit = "";

                        ItemID = dr["itemid"].ToString();

                        s[0] = "T";
                        s[1] = "";
                        s[2] = dr["itemname"].ToString();
                        s[3] = dr["dec"].ToString();
                        s[4] = "";
                        s[5] = "";
                        s[6] = "";
                        s[7] = dr["quantity"].ToString();
                        s[8] = dr["price"].ToString();
                        s[9] = "";
                        s[10] = dr["summoney"].ToString();
                        s[11] = "5";

                        if (dr["dec"].ToString().Trim() != dr["spec"].ToString().Trim())
                        {
                            s[4] = dr["spec"].ToString();
                        }

                        if (this.ConvertObjToDecimal(dr["price"]) != this.ConvertObjToDecimal(dr["itemprice_mny"]))
                        {
                            s[9] = dr["itemprice_mny"].ToString();
                        }
                        NewUnit = dr["itemunit_chr"].ToString().Trim();

                        Totalmny += ConvertObjToDecimal(dr["summoney"]);
                        row = this.dtgItem.Rows.Add(s);

                        if ((dr["unit"].ToString().Trim() != NewUnit) || (s[4].ToString().Trim() != ""))
                        {
                            this.dtgItem.Rows[row].Cells[0].Value = "F";
                            this.dtgItem.Rows[row].DefaultCellStyle.ForeColor = Color.Green;
                            this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                        }

                        if (dr["ifstop_int"].ToString() == "1")
                        {
                            this.dtgItem.Rows[row].Cells[0].Value = "F";
                            this.dtgItem.Rows[row].DefaultCellStyle.BackColor = Color.DarkGray;
                            this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                        }

                        this.dtgItem.Rows[row].Tag = dr;
                    }
                }
            }
            else if (ItemInputMode == 1)    //����ģʽ
            {
                //������Ŀ
                ret = objDoct.m_mthFindRecipeDetailOrder(RecID, out dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    //0 ״̬ 1 ���� 2 ��Ŀ���� 3 ԭ��� 4 �¹�� 5 Ƶ�� 6 �÷� 7 ���� 8 ԭ���� 9 �µ��� 10 ԭ�ϼƽ�� 11 ��Ŀ����
                    string[] s = new string[12];

                    s[0] = "T";
                    s[1] = "";
                    s[2] = dr["ORDERDICNAME_VCHR"].ToString();
                    s[3] = dr["spec_vchr"].ToString();
                    s[4] = "";
                    s[5] = "";
                    s[6] = "";
                    s[7] = dr["qty_dec"].ToString();
                    s[8] = dr["pricemny_dec"].ToString();
                    s[9] = "";
                    s[10] = dr["totalmny_dec"].ToString();

                    string ordertype = dr["tableindex_int"].ToString();
                    if (ordertype == "3")
                    {
                        s[11] = "lis";
                        ret = objDoct.m_mthFindRecipeDetail3ByOrder(dr["outpatrecipeid_chr"].ToString(), dr["attachorderid_vchr"].ToString(), out EntryDt, false, this.IsChildPrice);
                    }
                    else if (ordertype == "4")
                    {
                        s[11] = "test";
                        ret = objDoct.m_mthFindRecipeDetail4ByOrder(dr["outpatrecipeid_chr"].ToString(), dr["attachorderid_vchr"].ToString(), out EntryDt, false, this.IsChildPrice);
                    }
                    else if (ordertype == "5")
                    {
                        s[11] = "ops";
                        ret = objDoct.m_mthFindRecipeDetail5ByOrder(dr["outpatrecipeid_chr"].ToString(), dr["attachorderid_vchr"].ToString(), out EntryDt, false, this.IsChildPrice);
                    }
                    else
                    {
                        continue;
                    }

                    if (ret > 0)
                    {
                        Totalmny += ConvertObjToDecimal(dr["totalmny_dec"]);
                        row = this.dtgItem.Rows.Add(s);
                        this.dtgItem.Rows[row].Tag = dr;
                        this.dtgItem.Rows[row].Cells[0].Tag = EntryDt;

                        if (dr["status_int"].ToString() == "0")
                        {
                            this.dtgItem.Rows[row].Cells[0].Value = "F";
                            this.dtgItem.Rows[row].DefaultCellStyle.BackColor = Color.Red;
                            this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < EntryDt.Rows.Count; j++)
                            {
                                if ((EntryDt.Rows[j]["dec"].ToString().Trim() != EntryDt.Rows[j]["spec"].ToString().Trim()) ||
                                    (EntryDt.Rows[j]["unit"].ToString().Trim() != EntryDt.Rows[j]["itemunit_chr"].ToString().Trim()))
                                {
                                    this.dtgItem.Rows[row].Cells[0].Value = "F";
                                    this.dtgItem.Rows[row].DefaultCellStyle.ForeColor = Color.Green;
                                    this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                                    break;
                                }

                                if (EntryDt.Rows[j]["ifstop_int"].ToString() == "1")
                                {
                                    this.dtgItem.Rows[row].Cells[0].Value = "F";
                                    this.dtgItem.Rows[row].DefaultCellStyle.BackColor = Color.DarkGray;
                                    this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //6������
            ret = objDoct.m_mthFindRecipeDetail6(RecID, out dt, false, this.IsChildPrice);
            if (ret > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    //0 ״̬ 1 ���� 2 ��Ŀ���� 3 ԭ��� 4 �¹�� 5 Ƶ�� 6 �÷� 7 ���� 8 ԭ���� 9 �µ��� 10 ԭ�ϼƽ�� 11 ��Ŀ����
                    string[] s = new string[12];

                    string NewUnit = "";

                    ItemID = dr["itemid"].ToString();

                    s[0] = "T";
                    s[1] = "";
                    s[2] = dr["itemname"].ToString();
                    s[3] = dr["dec"].ToString();
                    s[4] = "";
                    s[5] = "";
                    s[6] = "";
                    s[7] = dr["quantity"].ToString();
                    s[8] = dr["price"].ToString();
                    s[9] = "";
                    s[10] = dr["summoney"].ToString();
                    s[11] = "6";

                    if (dr["dec"].ToString().Trim() != dr["spec"].ToString().Trim())
                    {
                        s[4] = dr["spec"].ToString();
                    }

                    //if (this.ConvertObjToDecimal(dr["price"]) != this.ConvertObjToDecimal(dr["itemprice_mny"]))
                    //{
                    //    s[9] = dr["itemprice_mny"].ToString();
                    //}
                    //NewUnit = dr["itemunit_chr"].ToString().Trim();

                    //�жϴ�С��λ
                    if (dr["opchargeflg_int"].ToString().Trim() == "0")
                    {
                        if (this.ConvertObjToDecimal(dr["price"]) != this.ConvertObjToDecimal(dr["itemprice_mny"]))
                        {
                            s[9] = dr["itemprice_mny"].ToString();
                        }
                        else
                        {
                            s[9] = dr["price"].ToString();
                        }
                        NewUnit = dr["itemopunit_chr"].ToString();
                    }
                    else
                    {
                        if (this.ConvertObjToDecimal(dr["price"]) != this.ConvertObjToDecimal(dr["submoney"]))
                        {
                            s[9] = dr["submoney"].ToString();
                        }
                        else
                        {
                            s[9] = dr["price"].ToString();
                        }
                        NewUnit = dr["itemipunit_chr"].ToString();
                    }

                    Totalmny += ConvertObjToDecimal(dr["summoney"]);
                    row = this.dtgItem.Rows.Add(s);

                    if ((dr["unit"].ToString().Trim() != NewUnit) || (s[4].ToString().Trim() != ""))
                    {
                        this.dtgItem.Rows[row].Cells[0].Value = "F";
                        this.dtgItem.Rows[row].DefaultCellStyle.ForeColor = Color.Green;
                        this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                    }

                    if (dr["ifstop_int"].ToString() == "1")
                    {
                        this.dtgItem.Rows[row].Cells[0].Value = "F";
                        this.dtgItem.Rows[row].DefaultCellStyle.BackColor = Color.DarkGray;
                        this.dtgItem.Rows[row].Cells[0].ReadOnly = true;
                    }

                    this.dtgItem.Rows[row].Tag = dr;
                }
            }

            this.lblTotal.Text = Totalmny.ToString("0.00");
            this.Text = "������ - " + RecID;
        }
        #endregion

        #region ȷ��ѡ��
        /// <summary>
        /// ȷ��ѡ��
        /// </summary>
        public void m_mthSelect(int Flag)
        {
            if (this.dtgItem.Rows.Count == 0)
            {
                return;
            }

            if (hasEntry.Count > 0)
            {
                hasEntry.Clear();
            }
            if (hasItem.Count > 0)
            {
                hasItem.Clear();
            }

            ArrayList SubArr1 = new ArrayList();
            ArrayList SubArr2 = new ArrayList();
            ArrayList SubArr3 = new ArrayList();
            ArrayList SubArr4 = new ArrayList();
            ArrayList SubArr5 = new ArrayList();
            ArrayList SubArr6 = new ArrayList();
            ArrayList SubArrLis = new ArrayList();
            ArrayList SubArrTest = new ArrayList();
            ArrayList SubArrOps = new ArrayList();

            for (int i = 0; i < dtgItem.Rows.Count; i++)
            {
                if (Flag == 1 && this.dtgItem.Rows[i].Cells[0].Value.ToString() != "T")
                {
                    continue;
                }

                DataRow dr = this.dtgItem.Rows[i].Tag as DataRow;

                switch (this.dtgItem.Rows[i].Cells["colItemAttribute"].Value.ToString())
                {
                    case "1":
                        SubArr1.Add(dr);
                        break;
                    case "2":
                        SubArr2.Add(dr);
                        break;
                    case "3":
                        SubArr3.Add(dr);
                        break;
                    case "4":
                        SubArr4.Add(dr);
                        break;
                    case "5":
                        SubArr5.Add(dr);
                        break;
                    case "6":
                        SubArr6.Add(dr);
                        break;
                    case "lis":
                        SubArrLis.Add(dr);
                        hasEntry.Add(dr["attachorderid_vchr"].ToString().Trim(), this.dtgItem.Rows[i].Cells[0].Tag);
                        break;
                    case "test":
                        SubArrTest.Add(dr);
                        hasEntry.Add(dr["attachorderid_vchr"].ToString().Trim(), this.dtgItem.Rows[i].Cells[0].Tag);
                        break;
                    case "ops":
                        SubArrOps.Add(dr);
                        hasEntry.Add(dr["attachorderid_vchr"].ToString().Trim(), this.dtgItem.Rows[i].Cells[0].Tag);
                        break;
                }
            }

            if (SubArr1.Count > 0)
            {
                hasItem.Add("1", SubArr1);
            }
            if (SubArr2.Count > 0)
            {
                hasItem.Add("2", SubArr2);
            }
            if (SubArr3.Count > 0)
            {
                hasItem.Add("3", SubArr3);
            }
            if (SubArr4.Count > 0)
            {
                hasItem.Add("4", SubArr4);
            }
            if (SubArr5.Count > 0)
            {
                hasItem.Add("5", SubArr5);
            }
            if (SubArr6.Count > 0)
            {
                hasItem.Add("6", SubArr6);
            }
            if (SubArrLis.Count > 0)
            {
                hasItem.Add("lis", SubArrLis);
            }
            if (SubArrTest.Count > 0)
            {
                hasItem.Add("test", SubArrTest);
            }
            if (SubArrOps.Count > 0)
            {
                hasItem.Add("ops", SubArrOps);
            }
        }
        #endregion

        #region ɾ��(����)
        /// <summary>
        /// ɾ��(����)
        /// </summary>
        /// <param name="Flag">1 ɾ��(����) 2 ɾ��(����)����</param>
        private void m_mthDelRecipe(int Flag)
        {
            clsDcl_DoctorWorkstation objDoct = new clsDcl_DoctorWorkstation();
            DataTable dtTmp = new DataTable();
            objDoct.m_lngGetRecipeMainInfo(RecipeInfo_VO.m_strOUTPATRECIPEID_CHR, out dtTmp);
            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                if (dtTmp.Rows[0]["diagdr_chr"].ToString().Trim() != this.LoginInfo.m_strEmpID)
                {
                    MessageBox.Show("�Ǳ��˴��������ܲ�����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            string info = "";
            int status = RecipeInfo_VO.m_intPSTATUS_INT;

            if (status == 0 || status == 1)
            {
                if (Flag == 1)
                {
                    info = "ɾ��";
                }
                else if (Flag == 2)
                {
                    info = "ɾ������";
                }
            }
            else if (status == 4 || status == 5)
            {
                if (Flag == 1)
                {
                    info = "����";
                }
                else if (Flag == 2)
                {
                    info = "���Ϻ���";
                }
            }
            else
            {
                return;
            }

            //αɾ�������ϴ���
            if (MessageBox.Show("ȷ���Ƿ�" + info + "��ǰ������", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            if (Flag == 2)
            {
                this.m_mthSelect(1);

                if (hasItem.Count == 0)
                {
                    MessageBox.Show("��ѡ����Ŀ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            long l = objDoct.m_mthDelRecipe(RecipeInfo_VO.m_strOUTPATRECIPEID_CHR, "-1");

            /*  
             *  һ�����������̣�
             *     2 �ѽ���ģ���������  -4 ����ʧ��
             *  ��������Ϊ������������ƺ����ģ���ӵ��ж�
             *     -3 ��ʾ�Ѿ���ҩƷ����ҩ����ҩ���������Ѿ���ҩ��ҩ��
             *     -5 ��ʾ�Ѿ��м��鿪ʼ��  -7 ��ʾ�Ѿ��м�鿪ʼ��
             * **/
            if ((l > 0 && l != 2) || l == -1)
            {
                MessageBox.Show(info + "�����ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (Flag == 1)
                {
                    this.DialogResult = DialogResult.Abort;
                }
                else if (Flag == 2)
                {
                    this.DialogResult = DialogResult.Yes;
                }
            }
            else if (l == -3)
            {
                MessageBox.Show(info + "����ʧ�ܡ�ҩƷ������ϣ�������ѷ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (l == -5)
            {
                MessageBox.Show(info + "����ʧ�ܡ������������顣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (l == -7)
            {
                MessageBox.Show(info + "����ʧ�ܡ�����������顣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(info + "����ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void frmDoctReUseInfo_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.m_mthShow();
            this.Top = this.Top - 30;
            this.checkBox1.Checked = true;
            this.Cursor = Cursors.Default;

            //��Դ��t_opr_outpatientrecipe.pstauts_int
            int status = RecipeInfo_VO.m_intPSTATUS_INT;
            if (status == 0 || status == 1)
            {
                this.btnDelOrCancel.Text = "ɾ��(&D)";
                this.btnDelOrCancelReUse.Text = "ɾ������";
            }
            else if (status == 4 || status == 5)
            {
                this.btnDelOrCancel.Text = "����(&A)";
                this.btnDelOrCancelReUse.Text = "���Ϻ���";
            }
            else
            {
                this.btnDelOrCancel.Text = "��";
                this.btnDelOrCancel.Enabled = false;
                this.btnDelOrCancelReUse.Text = "��";
                this.btnDelOrCancelReUse.Enabled = false;
            }
        }

        private void btnReUse_Click(object sender, EventArgs e)
        {
            this.m_mthSelect(1);

            if (hasItem.Count == 0)
            {
                MessageBox.Show("��ѡ����Ŀ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.m_mthSelect(2);
            this.DialogResult = DialogResult.Retry;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                for (int i = 0; i < this.dtgItem.Rows.Count; i++)
                {
                    if (this.dtgItem.Rows[i].Cells[0].ReadOnly == false)
                    {
                        this.dtgItem.Rows[i].Cells[0].Value = "T";
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.dtgItem.Rows.Count; i++)
                {
                    this.dtgItem.Rows[i].Cells[0].Value = "F";
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDoctReUseInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnDelOrCancel_Click(object sender, EventArgs e)
        {
            this.m_mthDelRecipe(1);
        }

        private void btnDelOrCancelReUse_Click(object sender, EventArgs e)
        {
            this.m_mthDelRecipe(2);
        }

    }
}