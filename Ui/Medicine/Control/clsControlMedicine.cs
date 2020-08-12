using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using com.digitalwave.Utility;
using com.digitalwave.iCare.common;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsControlMedicine:药品信息列表控制类 Create by Sam 2004-5-24
    /// </summary>
    public class clsControlMedicine : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsControlMedicine()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            m_objDoMain = new clsDomainConrol_Medicne();
        }
        clsDomainConrol_Medicne m_objDoMain = null;
        private bool IsNewOrUp = true;//默认为新增


        #region 设置窗体对象
        /// <summary>
        /// 保存药品的信息
        /// </summary>
        public System.Data.DataTable objResultArr = null;
        /// <summary>
        /// 保存当前被编辑的行号
        /// </summary>
        int SelectRow;

        /// <summary>
        /// 保存查询结果数据表
        /// </summary>
        public DataTable FidTable = new DataTable();
        /// <summary>
        /// 判断当前Datagrid显示的数据查找得到的还是从数据库得到的
        /// </summary>
        bool blCommand = true;
        /// <summary>
        /// 保存查找到数据的总和
        /// </summary>
        int intfind = 0;
        /// <summary>
        /// 中标年份
        /// </summary>
        string m_strStandarddate = string.Empty;

        com.digitalwave.iCare.gui.HIS.frmMedicine m_objViewer;
        /// <summary>
        /// 设置窗体对象
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmMedicine)frmMDI_Child_Base_in;
        }
        #endregion

        #region 变量
        /// <summary>
        /// 保存药品类型
        /// </summary>
        private DataTable medTypebt = new DataTable();
        /// <summary>
        /// 保存药品类型信息
        /// </summary>
        private DataTable dtmedtype = new DataTable();
        /// <summary>
        /// 保存药品剂型信息
        /// </summary>
        private DataTable dtMeicinePrep = new DataTable();
        /// <summary>
        /// 保存药品用法信息
        /// </summary>
        private DataTable dtuse = new DataTable();
        /// <summary>
        /// 保存门诊项目核算类型
        /// </summary>
        private DataTable dtItemextype = new DataTable();
        /// <summary>
        /// 保存门诊项目发票类型
        /// </summary>
        private DataTable dtItemextype1 = new DataTable();
        /// <summary>
        /// 病案核算分类
        /// </summary>
        private DataTable dtItemextype5 = new DataTable();

        /// <summary>
        /// 保存住院项目核算类型
        /// </summary>
        private DataTable dtItemextype3 = new DataTable();
        /// <summary>
        /// 保存住院项目发票类型
        /// </summary>
        private DataTable dtItemextype4 = new DataTable();
        /// <summary>
        /// 1新增，0修改
        /// </summary>
        internal int isAddNew = 1;

        /// <summary>
        /// 零价计算方式
        /// </summary>
        int m_intRet = 0;
        #endregion

        #region 显示药品库存信息
        public void m_showStorage()
        {
            if (this.m_objViewer.m_txtNo.Tag == null)
            {
                return;
            }
            DataTable dtStorage = new DataTable();
            m_objDoMain.m_lngGetMedStorage((string)this.m_objViewer.m_txtNo.Tag, out dtStorage);
            this.m_objViewer.listView1.Items.Clear();
            if (dtStorage.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtStorage.Rows.Count; i1++)
                {
                    ListViewItem newItem = new ListViewItem(dtStorage.Rows[i1]["storageName"].ToString().Trim());
                    newItem.SubItems.Add(dtStorage.Rows[i1]["AMOUNT_DEC"].ToString().Trim());
                    newItem.SubItems.Add(dtStorage.Rows[i1]["UNITID_CHR"].ToString().Trim());
                    if (dtStorage.Rows[i1]["FLAG_INT"].ToString() == "0")
                        newItem.SubItems.Add("药库");
                    else if (dtStorage.Rows[i1]["FLAG_INT"].ToString() == "1")
                        newItem.SubItems.Add("药房");
                    else
                        newItem.SubItems.Add("不清楚");
                    this.m_objViewer.listView1.Items.Add(newItem);
                }
                this.m_objViewer.panel4.Visible = true;
            }
        }

        #endregion

        #region 返回药品列表并填充到DataGrid
        /// <summary>
        /// 返回药品列表并填充到DataGrid
        /// </summary>
        string[] strReturn;
        public void m_lngLoad(string medTypeID)
        {
            long lngRes;
            m_objDoMain.m_lngGetMedType(out medTypebt);
            string str1 = "*";
            char[] delimiter = str1.ToCharArray();
            strReturn = medTypeID.Split(delimiter);
            this.m_objViewer.Text = "";
            string titleName = "";
            this.m_objViewer.cobSelectType.Item.Add("全部", "");
            this.m_objViewer.m_cboMedType.Item.Add("", "");
            if (strReturn.Length > 0 && medTypebt.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < strReturn.Length; i1++)
                {
                    for (int f2 = 0; f2 < medTypebt.Rows.Count; f2++)
                    {
                        if (strReturn[i1].Trim() == medTypebt.Rows[f2]["MEDICINETYPEID_CHR"].ToString().Trim())
                        {
                            this.m_objViewer.cobSelectType.Item.Add(medTypebt.Rows[f2]["MEDICINETYPENAME_VCHR"].ToString(), strReturn[i1].Trim());
                            this.m_objViewer.m_cboMedType.Item.Add(medTypebt.Rows[f2]["MEDICINETYPENAME_VCHR"].ToString(), strReturn[i1].Trim());
                            titleName += "{" + medTypebt.Rows[f2]["MEDICINETYPENAME_VCHR"].ToString() + "}";
                            break;
                        }
                    }
                }

            }
            #region 摆药分类
            this.m_objViewer.m_cboPUTMEDTYPE.Item.Add("", "");
            this.m_objViewer.m_cboPUTMEDTYPE.Item.Add("非摆药类", "0");
            this.m_objViewer.m_cboPUTMEDTYPE.Item.Add("摆药类", "1");
            //this.m_objViewer.m_cboPUTMEDTYPE.Item.Add("口服类", "2");
            #endregion
            DataTable dt = new DataTable();
            m_objDoMain.m_lngGetAllBihCate(out dt);
            if (dt.Rows.Count > 0)
            {
                this.m_objViewer.m_cobCat.Item.Add("", "");
                for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                {
                    this.m_objViewer.m_cobCat.Item.Add(dt.Rows[i1]["ORDERCATENAME_VCHR"].ToString().Trim(), dt.Rows[i1]["ORDERCATEID_CHR"].ToString().Trim());
                }
            }
            this.m_objViewer.cobSelectType.SelectedIndex = 0;
            this.m_objViewer.Text = titleName;
            this.m_objViewer.Text += "数据维护";
            #region 填充基本信息
            DataTable dtUnit = new DataTable();
            DataTable dtvendor = new DataTable();
            DataTable dtMEDICARETYPE = new DataTable();
            DataTable dtPharMatype = new DataTable();
            DataTable dtFreq = new DataTable();
            DataTable dtCATEID1 = new DataTable();
            bool isUse = false;
            lngRes = m_objDoMain.m_lngFindAllBase(out dtUnit, out dtMeicinePrep, out dtuse, out dtFreq, out dtvendor, out dtmedtype, out dtItemextype, out dtItemextype1, out dtItemextype3, out dtItemextype4, out dtMEDICARETYPE, out dtItemextype5, out dtPharMatype, out isUse, out dtCATEID1);
            if (isUse == false)
            {
                this.m_objViewer.m_txtTRADEPRICE.Enabled = false;
                this.m_objViewer.m_txtUNITPRICE.Enabled = false;
            }
            if (lngRes == 1)
            {
                if (dtCATEID1.Rows.Count > 0)
                {
                    this.m_objViewer.m_cboCATE1.Item.Add("", "");
                    for (int i1 = 0; i1 < dtCATEID1.Rows.Count; i1++)
                    {
                        this.m_objViewer.m_cboCATE1.Item.Add(dtCATEID1.Rows[i1][1].ToString().Trim(), dtCATEID1.Rows[i1][0].ToString());
                    }
                }
                if (dtPharMatype.Rows.Count > 0)
                {
                    dtPharMatype.Columns[0].ColumnName = "助记码";
                    dtPharMatype.Columns[1].ColumnName = "药理名称";
                    dtPharMatype.Columns[2].ColumnName = "拼音码";
                    dtPharMatype.Columns[3].ColumnName = "五笔码";
                    dtPharMatype.Columns[4].ColumnName = "ID";
                    this.m_objViewer.m_txtPharMatype.m_strParentName = "PARENTID_CHR";
                    this.m_objViewer.m_txtPharMatype.m_GetDataTable = dtPharMatype;
                }
                if (dtMEDICARETYPE.Rows.Count > 0)
                {
                    this.m_objViewer.ctlCARETYPE.Item.Add("", "");
                    this.m_objViewer.exComboBox2.Item.Add("", "");
                    for (int i1 = 0; i1 < dtMEDICARETYPE.Rows.Count; i1++)
                    {
                        this.m_objViewer.exComboBox2.Item.Add(dtMEDICARETYPE.Rows[i1]["TYPENAME_VCHR"].ToString(), dtMEDICARETYPE.Rows[i1]["TYPEID_CHR"].ToString());
                        this.m_objViewer.ctlCARETYPE.Item.Add(dtMEDICARETYPE.Rows[i1]["TYPENAME_VCHR"].ToString(), dtMEDICARETYPE.Rows[i1]["TYPEID_CHR"].ToString());
                    }

                }
                if (dtItemextype5.Rows.Count > 0)
                {

                    dtItemextype5.Columns[0].ColumnName = "助记码";
                    dtItemextype5.Columns[1].ColumnName = "病案核算名称";
                    dtItemextype5.Columns[2].ColumnName = "I   D";
                    this.m_objViewer.ctlTextBoxFind1.m_GetDataTable = dtItemextype5;
                }
                if (dtvendor.Rows.Count > 0)
                {
                    dtvendor.Columns[0].ColumnName = "助记码";
                    dtvendor.Columns[1].ColumnName = "生   产   厂   家";
                    dtvendor.Columns[2].ColumnName = "ID";
                    dtvendor.Columns[3].ColumnName = "拼音码";
                    dtvendor.Columns[4].ColumnName = "五笔码";

                }
                if (dtItemextype.Rows.Count > 0)
                {
                    dtItemextype.Columns[0].ColumnName = "助记码";
                    dtItemextype.Columns[1].ColumnName = "门诊核算名称";
                    dtItemextype.Columns[2].ColumnName = "I   D";
                    this.m_objViewer.m_txtITEMOPCALCTYPE.m_GetDataTable = dtItemextype;
                }

                if (dtItemextype1.Rows.Count > 0)
                {
                    dtItemextype1.Columns[0].ColumnName = "助记码";
                    dtItemextype1.Columns[1].ColumnName = "门诊发票名称";
                    dtItemextype1.Columns[2].ColumnName = "I   D";
                    this.m_objViewer.m_txtITEMOPINVTYPE.m_GetDataTable = dtItemextype1;

                }

                if (dtItemextype3.Rows.Count > 0)
                {
                    dtItemextype3.Columns[0].ColumnName = "助记码";
                    dtItemextype3.Columns[1].ColumnName = "住院核算名称";
                    dtItemextype3.Columns[2].ColumnName = "I   D";
                    this.m_objViewer.m_txtITEMIPCALCTYPE.m_GetDataTable = dtItemextype3;
                }

                if (dtItemextype4.Rows.Count > 0)
                {
                    dtItemextype4.Columns[0].ColumnName = "助记码";
                    dtItemextype4.Columns[1].ColumnName = "住院发票名称";
                    dtItemextype4.Columns[2].ColumnName = "I   D";
                    this.m_objViewer.m_txtITEMIPINVTYPE.m_GetDataTable = dtItemextype4;
                }
                if (dtMeicinePrep.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtMeicinePrep.Rows.Count; i1++)
                    {
                        this.m_objViewer.m_cboPreType.Items.Add(dtMeicinePrep.Rows[i1]["MEDICINEPREPTYPENAME_VCHR"].ToString().Trim());
                    }
                }
                if (dtUnit.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtUnit.Rows.Count; i1++)
                    {
                        this.m_objViewer.m_CobDosageUnit.Items.Add(dtUnit.Rows[i1]["UNITNAME_CHR"].ToString().Trim());
                        this.m_objViewer.m_CobUnit.Items.Add(dtUnit.Rows[i1]["UNITNAME_CHR"].ToString().Trim());
                        this.m_objViewer.m_CobIpUnit.Items.Add(dtUnit.Rows[i1]["UNITNAME_CHR"].ToString().Trim());
                        this.m_objViewer.m_CobRequestUnit.Items.Add(dtUnit.Rows[i1]["UNITNAME_CHR"].ToString().Trim());
                        this.m_objViewer.cboMedBagUnit.Items.Add(dtUnit.Rows[i1]["UNITNAME_CHR"].ToString().Trim());
                    }
                }
                if (dtuse.Rows.Count > 0)
                {
                    dtuse.Columns[0].ColumnName = "助记码";
                    dtuse.Columns[1].ColumnName = "用 法 名 称";
                    dtuse.Columns[2].ColumnName = "I  D";
                    this.m_objViewer.m_txtUse.m_GetDataTable = dtuse;

                }
                if (dtFreq.Rows.Count > 0)
                {
                    dtFreq.Columns[0].ColumnName = "助记码";
                    dtFreq.Columns[1].ColumnName = "频 率 名 称";
                    dtFreq.Columns[2].ColumnName = "I  D";
                    this.m_objViewer.textboxFreq.m_GetDataTable = dtFreq;
                }
            }

            #endregion

            m_objDoMain.m_lngGetRetailMethod(out m_intRet);

        }
        #endregion

        #region 药品类型选择事件
        public void m_CobMedTypeSele()
        {
            long lngRes = 0;
            if (this.m_objViewer.cobSelectType.SelectedIndex == 0)
            {
                lngRes = m_objDoMain.m_lngGetMetDgList(strReturn, out objResultArr, this.m_objViewer.checkBox2.Checked ? true : false);
            }
            else
            {
                string[] strSelect = new string[1];
                strSelect[0] = this.m_objViewer.cobSelectType.SelectItemValue;
                lngRes = m_objDoMain.m_lngGetMetDgList(strSelect, out objResultArr, this.m_objViewer.checkBox2.Checked ? true : false);
            }
            if (lngRes <= 0)
            {
                MessageBox.Show("获取药品数据出错！", "icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.m_objViewer.m_dgList.m_mthDeleteAllRow();
                return;
            }
            else
            {
                this.m_objViewer.m_dgList.m_mthSetDataTable(objResultArr);
                this.m_objViewer.m_dgList.Tag = "objResultArr";
            }
        }
        #endregion

        #region 是否显示停用药品
        public void m_mthShowMed()
        {
            long lngRes = m_objDoMain.m_lngGetMetDgList(strReturn, out objResultArr, this.m_objViewer.checkBox2.Checked ? true : false);
            if (lngRes <= 0)
            {
                MessageBox.Show("获取药品数据出错！", "icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.m_objViewer.m_dgList.m_mthDeleteAllRow();
                return;
            }
            else
            {
                this.m_objViewer.m_dgList.m_mthSetDataTable(objResultArr);
                this.m_objViewer.m_dgList.Tag = "objResultArr";
                m_lngClearfrm();
            }
        }

        #endregion

        #region 生成五笔码/拼音码
        public void m_lngGetpywb()
        {
            try
            {
                string strAny = this.m_objViewer.m_txtName.Text.Trim() + this.m_objViewer.txtMEDNORMALNAME.Text.Trim();
                clsCreateChinaCode getChinaCode = new clsCreateChinaCode();
                this.m_objViewer.m_txtPY.Text = getChinaCode.m_strCreateChinaCode(strAny, ChinaCode.PY).Trim();
                this.m_objViewer.m_txtWB.Text = getChinaCode.m_strCreateChinaCode(strAny, ChinaCode.WB).Trim();
            }
            catch
            {
                MessageBox.Show("生成生成五笔码/拼音码出错，请不要用英文字母", "icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region 删除对应的药典
        public void m_mthDeleMedByMedId()
        {
            if (isAddNew == 1)//新增
            {
                this.m_objViewer.LableMed.Tag = null;
                this.m_objViewer.LableMed.Text = "";
            }
            else
            {
                long lngRes = m_objDoMain.m_lngDeleteMedByID((string)this.m_objViewer.m_txtNo.Tag);
                if (lngRes == 1)
                {
                    this.m_objViewer.LableMed.Tag = null;
                    this.m_objViewer.LableMed.Text = "";
                }

            }

        }
        #endregion

        #region 保存数据
        public void m_lngSaveClick()
        {
            clsPublicParm publicClass = new clsPublicParm();
            if (this.m_objViewer.m_txtNo.Text == "")
            {
                publicClass.m_mthShowWarning(this.m_objViewer.m_txtNo, "助记码是必填项!");
                this.m_objViewer.m_txtNo.Focus();
                return;
            }
            if (this.m_objViewer.m_txtName.Text == "")
            {
                publicClass.m_mthShowWarning(this.m_objViewer.m_txtName, "药品名称是必填项!");
                this.m_objViewer.m_txtName.Focus();
                return;
            }
            if (this.m_objViewer.m_txtPackQty.Text == "" || this.m_objViewer.m_txtPackQty.Text == "0")
            {
                publicClass.m_mthShowWarning(this.m_objViewer.m_txtPackQty, "药品包装量是必填项!");
                this.m_objViewer.m_txtPackQty.Focus();
                return;
            }
            if (this.m_objViewer.m_cboMedType.Text == "")
            {
                publicClass.m_mthShowWarning(this.m_objViewer.m_cboMedType, "药品类型是必选项!");
                this.m_objViewer.m_cboMedType.Focus();
                return;
            }
            if (this.m_objViewer.m_cboPreType.Text == "")
            {
                publicClass.m_mthShowWarning(this.m_objViewer.m_cboPreType, "药品剂型是必选项!");
                this.m_objViewer.m_cboPreType.Focus();
                return;
            }

            #region check
            if (this.m_objViewer.m_txtITEMOPCALCTYPE.txtValuse == "")
            {
                publicClass.m_mthShowWarning(this.m_objViewer.m_txtITEMOPCALCTYPE, "门诊核算是必选项!");
                this.m_objViewer.m_txtITEMOPCALCTYPE.Focus();
                return;
            }
            if (this.m_objViewer.m_txtITEMOPINVTYPE.txtValuse == "")
            {
                publicClass.m_mthShowWarning(this.m_objViewer.m_txtITEMOPINVTYPE, "门诊发票是必选项!");
                this.m_objViewer.m_txtITEMOPINVTYPE.Focus();
                return;
            }
            if (this.m_objComInfo.m_mthGetHospitalNo() == "00001")
            {
                if (this.m_objViewer.m_txtITEMIPCALCTYPE.txtValuse == "")
                {
                    publicClass.m_mthShowWarning(this.m_objViewer.m_txtITEMIPCALCTYPE, "住院核算是必选项!");
                    this.m_objViewer.m_txtITEMIPCALCTYPE.Focus();
                    return;
                }
                if (this.m_objViewer.m_txtITEMIPINVTYPE.txtValuse == "")
                {
                    publicClass.m_mthShowWarning(this.m_objViewer.m_txtITEMIPINVTYPE, "住院发票是必选项!");
                    this.m_objViewer.m_txtITEMIPINVTYPE.Focus();
                    return;
                }
            }
            #endregion
            DataRow newRow = m_lngFillDataRow();
            clsAlias_VO objAliasVo = this.m_mthFillAlias();
            if (isAddNew == 1)//新增
            {
                //DataTable dt=new DataTable();
                //long lngRes=m_objDoMain.m_lngCheckIsUse(this.m_objViewer.m_txtNo.Text.Trim(),out dt);
                //if(lngRes==1&&dt.Rows.Count>0)
                //{
                //    publicClass.m_mthShowWarning(this.m_objViewer.m_txtNo,"系统检测到'"+this.m_objViewer.m_txtNo.Text.Trim()+"'助记码，已经被'"+dt.Rows[0]["MEDICINENAME_VCHR"].ToString()+"'药品使用，\n不可以再使用该助记码！");
                //    this.m_objViewer.m_txtNo.Focus();
                //    return;
                //}
                int isInsertItem = 1;
                bool IsAuto = false;
                if (m_objViewer.m_strType == "0")
                {
                    if (MessageBox.Show("是否需要自动产生调价单据？", "icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        IsAuto = true;
                }
                string strStorageID = "";
                frmShowSelect showFrm = new frmShowSelect(m_objViewer.m_strType);
                if (showFrm.ShowDialog() == DialogResult.OK)
                {
                    strStorageID = showFrm.StorageID;
                }

                string newID = "";
                DataTable dtbMedicine = objResultArr.Clone();
                dtbMedicine.BeginLoadData();
                dtbMedicine.LoadDataRow(newRow.ItemArray, true);
                dtbMedicine.EndLoadData();
                dtbMedicine.TableName = "Medicine";
                long lngRes = m_objDoMain.m_lngSaveed(m_objViewer.m_strType, dtbMedicine, out newID, isInsertItem, this.m_objViewer.LoginInfo.m_strEmpID, IsAuto, strStorageID, m_objViewer.m_dtbChargeItem);
                if (lngRes == 1)
                {
                    newRow["MEDICINEID_CHR"] = newID;
                    if ((string)this.m_objViewer.m_dgList.Tag == "objResultArr")
                    {
                        objResultArr.Rows.Add(newRow);
                        lngRes = m_objDoMain.m_lngGetItemID(newID, out objAliasVo.m_strMedicineId);
                        if (objAliasVo.m_strAliasName.Trim() != string.Empty)
                            m_objDoMain.m_mthSaveAlias(3, objAliasVo);//保存别名表
                        this.m_objViewer.label2.Tag = objAliasVo.m_strAliasName;
                    }
                    else
                    {
                        try
                        {
                            lngRes = m_objDoMain.m_lngGetItemID(newID, out objAliasVo.m_strMedicineId);
                            if (objAliasVo.m_strAliasName.Trim() != string.Empty)
                                m_objDoMain.m_mthSaveAlias(3, objAliasVo);//保存别名表
                            FidTable.Rows.Add(newRow);
                            this.m_objViewer.label2.Tag = objAliasVo.m_strAliasName;
                        }
                        catch
                        {
                            m_objDoMain.m_lngGetMetDgList(strReturn, out objResultArr, this.m_objViewer.checkBox2.Checked ? true : false);
                            this.m_objViewer.m_dgList.m_mthSetDataTable(objResultArr);
                            this.m_objViewer.m_dgList.Tag = "objResultArr";
                        }
                    }
                    m_lngClearfrm();
                }
            }
            else//修改
            {
                //DataTable dt=new DataTable();
                //long lngRes=m_objDoMain.m_lngCheckIsUse(this.m_objViewer.m_txtNo.Text.Trim(),out dt);
                //if(lngRes==1&&dt.Rows.Count>0)
                //{
                //    if(dt.Rows[0]["MEDICINEID_CHR"].ToString().Trim()!=newRow["MEDICINEID_CHR"].ToString().Trim())
                //    {
                //        publicClass.m_mthShowWarning(this.m_objViewer.m_txtNo,"系统检测到'"+this.m_objViewer.m_txtNo.Text.Trim()+"'助记码，已经被'"+dt.Rows[0]["MEDICINENAME_VCHR"].ToString()+"'药品使用，\n不可以再使用该助记码！");
                //        this.m_objViewer.m_txtNo.Focus();
                //        return;
                //    }
                //}
                int IsModify = 1;
                //					if(MessageBox.Show("是否需要同时修改收费项目？","icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                //						IsModify=1;
                DataTable dtbModify = objResultArr.Clone();
                dtbModify.BeginLoadData();
                dtbModify.LoadDataRow(newRow.ItemArray, true);
                dtbModify.EndLoadData();
                dtbModify.TableName = "Modify";
                long lngRes = m_objDoMain.m_lngModify(dtbModify, IsModify, this.m_objViewer.LoginInfo.m_strEmpID);
                if (objAliasVo.m_strAliasName.Trim() != string.Empty)
                    m_objDoMain.m_mthSaveAlias(3, objAliasVo);//保存别名表
                if (lngRes == 1)
                {
                    this.m_objViewer.label2.Tag = objAliasVo.m_strAliasName;
                    if ((string)this.m_objViewer.m_dgList.Tag == "objResultArr")
                    {
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["pharmaid_chr"] = newRow["pharmaid_chr"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["pharmaname_vchr"] = newRow["pharmaname_vchr"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINESTDID_CHR"] = newRow["MEDICINESTDID_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PRODUCTORID_CHR"] = newRow["PRODUCTORID_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINESTDESC_VCHR"] = newRow["MEDICINESTDESC_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["HYPE_INT"] = newRow["HYPE_INT"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["INSURANCETYPE_VCHR"] = newRow["INSURANCETYPE_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["INPINSURANCETYPE_VCHR"] = newRow["INPINSURANCETYPE_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["TYPENAME_VCHR"] = newRow["TYPENAME_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["TYPENAME_VCHR1"] = newRow["TYPENAME_VCHR1"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDNORMALNAME_VCHR"] = newRow["MEDNORMALNAME_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PERMITNO_VCHR"] = newRow["PERMITNO_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ADULTDOSAGE_DEC"] = newRow["ADULTDOSAGE_DEC"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["CHILDDOSAGE_DEC"] = newRow["CHILDDOSAGE_DEC"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINENAME_VCHR"] = newRow["MEDICINENAME_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDSPEC_VCHR"] = newRow["MEDSPEC_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PYCODE_CHR"] = newRow["PYCODE_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["WBCODE_CHR"] = newRow["WBCODE_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["NMLDOSAGE_DEC"] = newRow["NMLDOSAGE_DEC"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MINDOSAGE_DEC"] = newRow["MINDOSAGE_DEC"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MAXDOSAGE_DEC"] = newRow["MAXDOSAGE_DEC"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["LIMITUNITPRICE_MNY"] = newRow["LIMITUNITPRICE_MNY"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["INSURANCEID_VCHR"] = newRow["INSURANCEID_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ASSISTCODE_CHR"] = newRow["ASSISTCODE_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINEENGNAME_VCHR"] = newRow["MEDICINEENGNAME_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINETYPENAME_VCHR"] = newRow["MEDICINETYPENAME_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINEPREPTYPENAME_VCHR"] = newRow["MEDICINEPREPTYPENAME_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDSPEC_VCHR"] = newRow["MEDSPEC_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["TRADEPRICE_MNY"] = newRow["TRADEPRICE_MNY"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["UNITPRICE_MNY"] = newRow["UNITPRICE_MNY"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PRODUCTORID_CHR"] = newRow["PRODUCTORID_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["DOSAGE_DEC"] = newRow["DOSAGE_DEC"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["DOSAGEUNIT_CHR"] = newRow["DOSAGEUNIT_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["OPUNIT_CHR"] = newRow["OPUNIT_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["IPUNIT_CHR"] = newRow["IPUNIT_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PACKQTY_DEC"] = newRow["PACKQTY_DEC"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["IPUNITPRICE_MNY"] = newRow["IPUNITPRICE_MNY"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISANAESTHESIA"] = newRow["ISANAESTHESIA"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ischlorpromazine2"] = newRow["ischlorpromazine2"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ispoison"] = newRow["ispoison"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISCHLORPROMAZIN"] = newRow["ISCHLORPROMAZIN"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISCOSTLY"] = newRow["ISCOSTLY"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISSELF"] = newRow["ISSELF"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISIMPORT"] = newRow["ISIMPORT"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISSELFPAY"] = newRow["ISSELFPAY"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["POFLAG"] = newRow["POFLAG"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["USAGENAME_VCHR"] = newRow["USAGENAME_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["OPCHARGEFLG"] = newRow["OPCHARGEFLG"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["IPCHARGEFLG"] = newRow["IPCHARGEFLG"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["IFSTOP_INT"] = newRow["IFSTOP_INT"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["isSTANDARD"] = newRow["isSTANDARD"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["INSURANCEID_VCHR"] = newRow["INSURANCEID_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ITEMOPCALCTYPE_CHR"] = newRow["ITEMOPCALCTYPE_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ITEMIPCALCTYPE_CHR"] = newRow["ITEMIPCALCTYPE_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ITEMOPINVTYPE_CHR"] = newRow["ITEMOPINVTYPE_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ITEMIPINVTYPE_CHR"] = newRow["ITEMIPINVTYPE_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICNETYPE_INT"] = newRow["MEDICNETYPE_INT"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["itembihctype_chr"] = newRow["itembihctype_chr"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["DEPTPREP_INT"] = newRow["DEPTPREP_INT"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["DEPTPREP"] = newRow["DEPTPREP"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ORDERCATEID_CHR"] = newRow["ORDERCATEID_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ORDERCATENAME_VCHR"] = newRow["ORDERCATENAME_VCHR"];

                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["FREQID_CHR"] = newRow["FREQID_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["FREQNAME_CHR"] = newRow["FREQNAME_CHR"];


                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PUTMEDTYPENAME"] = newRow["PUTMEDTYPENAME"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PUTMEDTYPE_INT"] = newRow["PUTMEDTYPE_INT"];

                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ORDERCATEID1_CHR"] = newRow["ORDERCATEID1_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["NAME_CHR"] = newRow["NAME_CHR"];

                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["INPINSURANCETYPE_VCHR"] = newRow["INPINSURANCETYPE_VCHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["typename_vchr1"] = newRow["typename_vchr1"];

                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ORDERCATEID_CHR"] = newRow["ORDERCATEID_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ordercatename_vchr"] = newRow["ordercatename_vchr"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["STANDARDDATE"] = newRow["STANDARDDATE"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["REQUESTUNIT_CHR"] = newRow["REQUESTUNIT_CHR"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["REQUESTPACKQTY_DEC"] = newRow["REQUESTPACKQTY_DEC"];

                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["EXPENSELIMIT_MNY"] = newRow["EXPENSELIMIT_MNY"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["DIFFPRICE_MNY"] = newRow["DIFFPRICE_MNY"];//Added by: 吴汉明 2014-12-9
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["medbagunit"] = newRow["medbagunit"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["highriskflag"] = newRow["highriskflag"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["isproducedrugs"] = newRow["isproducedrugs"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["transno"] = newRow["transno"];
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["varietycode"] = newRow["varietycode"];
                    }
                    else
                    {

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["pharmaid_chr"] = newRow["pharmaid_chr"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["pharmaname_vchr"] = newRow["pharmaname_vchr"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINESTDID_CHR"] = newRow["MEDICINESTDID_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINESTDESC_VCHR"] = newRow["MEDICINESTDESC_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PRODUCTORID_CHR"] = newRow["PRODUCTORID_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PERMITNO_VCHR"] = newRow["PERMITNO_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["HYPE_INT"] = newRow["HYPE_INT"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["INSURANCETYPE_VCHR"] = newRow["INSURANCETYPE_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["TYPENAME_VCHR"] = newRow["TYPENAME_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDNORMALNAME_VCHR"] = newRow["MEDNORMALNAME_VCHR"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ADULTDOSAGE_DEC"] = newRow["ADULTDOSAGE_DEC"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["CHILDDOSAGE_DEC"] = newRow["CHILDDOSAGE_DEC"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINENAME_VCHR"] = newRow["MEDICINENAME_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDSPEC_VCHR"] = newRow["MEDSPEC_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PYCODE_CHR"] = newRow["PYCODE_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["WBCODE_CHR"] = newRow["WBCODE_CHR"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MINDOSAGE_DEC"] = newRow["MINDOSAGE_DEC"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MAXDOSAGE_DEC"] = newRow["MAXDOSAGE_DEC"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["NMLDOSAGE_DEC"] = newRow["NMLDOSAGE_DEC"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["INSURANCEID_VCHR"] = newRow["INSURANCEID_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ASSISTCODE_CHR"] = newRow["ASSISTCODE_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINEENGNAME_VCHR"] = newRow["MEDICINEENGNAME_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINETYPENAME_VCHR"] = newRow["MEDICINETYPENAME_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINEPREPTYPENAME_VCHR"] = newRow["MEDICINEPREPTYPENAME_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDSPEC_VCHR"] = newRow["MEDSPEC_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["TRADEPRICE_MNY"] = newRow["TRADEPRICE_MNY"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["UNITPRICE_MNY"] = newRow["UNITPRICE_MNY"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PRODUCTORID_CHR"] = newRow["PRODUCTORID_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["DOSAGE_DEC"] = newRow["DOSAGE_DEC"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["DOSAGEUNIT_CHR"] = newRow["DOSAGEUNIT_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["OPUNIT_CHR"] = newRow["OPUNIT_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["IPUNIT_CHR"] = newRow["IPUNIT_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PACKQTY_DEC"] = newRow["PACKQTY_DEC"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["IPUNITPRICE_MNY"] = newRow["IPUNITPRICE_MNY"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISANAESTHESIA"] = newRow["ISANAESTHESIA"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ischlorpromazine2"] = newRow["ischlorpromazine2"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ispoison"] = newRow["ispoison"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISCHLORPROMAZIN"] = newRow["ISCHLORPROMAZIN"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISCOSTLY"] = newRow["ISCOSTLY"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISSELF"] = newRow["ISSELF"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISIMPORT"] = newRow["ISIMPORT"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ISSELFPAY"] = newRow["ISSELFPAY"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["POFLAG"] = newRow["POFLAG"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["USAGENAME_VCHR"] = newRow["USAGENAME_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["OPCHARGEFLG"] = newRow["OPCHARGEFLG"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["IPCHARGEFLG"] = newRow["IPCHARGEFLG"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["IFSTOP"] = newRow["IFSTOP"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["isSTANDARD"] = newRow["isSTANDARD"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["INSURANCEID_VCHR"] = newRow["INSURANCEID_VCHR"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ITEMOPCALCTYPE_CHR"] = newRow["ITEMOPCALCTYPE_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ITEMIPCALCTYPE_CHR"] = newRow["ITEMIPCALCTYPE_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ITEMOPINVTYPE_CHR"] = newRow["ITEMOPINVTYPE_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ITEMIPINVTYPE_CHR"] = newRow["ITEMIPINVTYPE_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["itembihctype_chr"] = newRow["itembihctype_chr"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICNETYPE_INT"] = newRow["MEDICNETYPE_INT"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["DEPTPREP_INT"] = newRow["DEPTPREP_INT"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["DEPTPREP"] = newRow["DEPTPREP"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ORDERCATEID_CHR"] = newRow["ORDERCATEID_CHR"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ORDERCATENAME_VCHR"] = newRow["ORDERCATENAME_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["FREQID_CHR"] = newRow["FREQID_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["FREQNAME_CHR"] = newRow["FREQNAME_CHR"];


                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PUTMEDTYPENAME"] = newRow["PUTMEDTYPENAME"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["PUTMEDTYPE_INT"] = newRow["PUTMEDTYPE_INT"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ORDERCATEID1_CHR"] = newRow["ORDERCATEID1_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["NAME_CHR"] = newRow["NAME_CHR"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["INPINSURANCETYPE_VCHR"] = newRow["INPINSURANCETYPE_VCHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["typename_vchr1"] = newRow["typename_vchr1"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ORDERCATEID_CHR"] = newRow["ORDERCATEID_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["ordercatename_vchr"] = newRow["ordercatename_vchr"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["IFSTOP_INT"] = newRow["IFSTOP_INT"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["LIMITUNITPRICE_MNY"] = newRow["LIMITUNITPRICE_MNY"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["STANDARDDATE"] = newRow["STANDARDDATE"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["REQUESTUNIT_CHR"] = newRow["REQUESTUNIT_CHR"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["REQUESTPACKQTY_DEC"] = newRow["REQUESTPACKQTY_DEC"];

                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["EXPENSELIMIT_MNY"] = newRow["EXPENSELIMIT_MNY"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["DIFFPRICE_MNY"] = newRow["DIFFPRICE_MNY"];//Added by: 吴汉明 2014-12-9
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["medbagunit"] = newRow["medbagunit"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["highriskflag"] = newRow["highriskflag"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["isproducedrugs"] = newRow["isproducedrugs"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["transno"] = newRow["transno"];
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["varietycode"] = newRow["varietycode"];

                        DataRow[] drResult = objResultArr.Select("MEDICINEID_CHR = '" + newRow["MEDICINEID_CHR"].ToString().Trim() + "'");
                        if (drResult != null && drResult.Length == 1)
                        {
                            drResult[0]["MEDICINESTDID_CHR"] = newRow["MEDICINESTDID_CHR"];
                            drResult[0]["PRODUCTORID_CHR"] = newRow["PRODUCTORID_CHR"];
                            drResult[0]["MEDICINESTDESC_VCHR"] = newRow["MEDICINESTDESC_VCHR"];
                            drResult[0]["HYPE_INT"] = newRow["HYPE_INT"];
                            drResult[0]["INSURANCETYPE_VCHR"] = newRow["INSURANCETYPE_VCHR"];
                            drResult[0]["TYPENAME_VCHR"] = newRow["TYPENAME_VCHR"];
                            drResult[0]["ITEMOPCALCTYPE_CHR"] = newRow["ITEMOPCALCTYPE_CHR"];
                            drResult[0]["ITEMIPCALCTYPE_CHR"] = newRow["ITEMIPCALCTYPE_CHR"];
                            drResult[0]["ITEMOPINVTYPE_CHR"] = newRow["ITEMOPINVTYPE_CHR"];
                            drResult[0]["ITEMIPINVTYPE_CHR"] = newRow["ITEMIPINVTYPE_CHR"];
                            drResult[0]["itembihctype_chr"] = newRow["itembihctype_chr"];
                            drResult[0]["MEDNORMALNAME_VCHR"] = newRow["MEDNORMALNAME_VCHR"];
                            drResult[0]["MEDICINENAME_VCHR"] = newRow["MEDICINENAME_VCHR"];
                            drResult[0]["MEDSPEC_VCHR"] = newRow["MEDSPEC_VCHR"];
                            drResult[0]["PYCODE_CHR"] = newRow["PYCODE_CHR"];
                            drResult[0]["WBCODE_CHR"] = newRow["WBCODE_CHR"];
                            drResult[0]["NMLDOSAGE_DEC"] = newRow["NMLDOSAGE_DEC"];
                            drResult[0]["INSURANCEID_VCHR"] = newRow["INSURANCEID_VCHR"];
                            drResult[0]["MINDOSAGE_DEC"] = newRow["MINDOSAGE_DEC"];
                            drResult[0]["MAXDOSAGE_DEC"] = newRow["MAXDOSAGE_DEC"];
                            drResult[0]["ASSISTCODE_CHR"] = newRow["ASSISTCODE_CHR"];
                            drResult[0]["MEDICINEENGNAME_VCHR"] = newRow["MEDICINEENGNAME_VCHR"];
                            drResult[0]["MEDICINETYPENAME_VCHR"] = newRow["MEDICINETYPENAME_VCHR"];
                            drResult[0]["MEDICINEPREPTYPENAME_VCHR"] = newRow["MEDICINEPREPTYPENAME_VCHR"];
                            drResult[0]["MEDSPEC_VCHR"] = newRow["MEDSPEC_VCHR"];
                            drResult[0]["TRADEPRICE_MNY"] = newRow["TRADEPRICE_MNY"];
                            drResult[0]["UNITPRICE_MNY"] = newRow["UNITPRICE_MNY"];
                            drResult[0]["PRODUCTORID_CHR"] = newRow["PRODUCTORID_CHR"];
                            drResult[0]["DOSAGE_DEC"] = newRow["DOSAGE_DEC"];
                            drResult[0]["DOSAGEUNIT_CHR"] = newRow["DOSAGEUNIT_CHR"];
                            drResult[0]["OPUNIT_CHR"] = newRow["OPUNIT_CHR"];
                            drResult[0]["IPUNIT_CHR"] = newRow["IPUNIT_CHR"];
                            drResult[0]["PACKQTY_DEC"] = newRow["PACKQTY_DEC"];
                            drResult[0]["IPUNITPRICE_MNY"] = newRow["IPUNITPRICE_MNY"];
                            drResult[0]["ISANAESTHESIA"] = newRow["ISANAESTHESIA"];
                            drResult[0]["ischlorpromazine2"] = newRow["ischlorpromazine2"];
                            drResult[0]["ispoison"] = newRow["ispoison"];
                            drResult[0]["ISCHLORPROMAZIN"] = newRow["ISCHLORPROMAZIN"];
                            drResult[0]["ISCOSTLY"] = newRow["ISCOSTLY"];
                            drResult[0]["ISSELF"] = newRow["ISSELF"];
                            drResult[0]["ISIMPORT"] = newRow["ISIMPORT"];
                            drResult[0]["ISSELFPAY"] = newRow["ISSELFPAY"];
                            drResult[0]["POFLAG"] = newRow["POFLAG"];
                            drResult[0]["USAGENAME_VCHR"] = newRow["USAGENAME_VCHR"];
                            drResult[0]["OPCHARGEFLG"] = newRow["OPCHARGEFLG"];
                            drResult[0]["IPCHARGEFLG"] = newRow["IPCHARGEFLG"];
                            drResult[0]["IFSTOP"] = newRow["IFSTOP"];
                            drResult[0]["isSTANDARD"] = newRow["isSTANDARD"];
                            drResult[0]["INSURANCEID_VCHR"] = newRow["INSURANCEID_VCHR"];
                            drResult[0]["MEDICNETYPE_INT"] = newRow["MEDICNETYPE_INT"];
                            drResult[0]["DEPTPREP_INT"] = newRow["DEPTPREP_INT"];
                            drResult[0]["DEPTPREP"] = newRow["DEPTPREP"];
                            drResult[0]["ORDERCATEID_CHR"] = newRow["ORDERCATEID_CHR"];
                            drResult[0]["ORDERCATENAME_VCHR"] = newRow["ORDERCATENAME_VCHR"];
                            drResult[0]["FREQID_CHR"] = newRow["FREQID_CHR"];
                            drResult[0]["FREQNAME_CHR"] = newRow["FREQNAME_CHR"];
                            drResult[0]["PUTMEDTYPENAME"] = newRow["PUTMEDTYPENAME"];
                            drResult[0]["PUTMEDTYPE_INT"] = newRow["PUTMEDTYPE_INT"];

                            drResult[0]["ORDERCATEID1_CHR"] = newRow["ORDERCATEID1_CHR"];
                            drResult[0]["NAME_CHR"] = newRow["NAME_CHR"];

                            drResult[0]["INPINSURANCETYPE_VCHR"] = newRow["INPINSURANCETYPE_VCHR"];
                            drResult[0]["typename_vchr1"] = newRow["typename_vchr1"];

                            drResult[0]["ORDERCATEID_CHR"] = newRow["ORDERCATEID_CHR"];
                            drResult[0]["ordercatename_vchr"] = newRow["ordercatename_vchr"];
                            drResult[0]["IFSTOP_INT"] = newRow["IFSTOP_INT"];
                            drResult[0]["LIMITUNITPRICE_MNY"] = newRow["LIMITUNITPRICE_MNY"];
                            drResult[0]["STANDARDDATE"] = newRow["STANDARDDATE"];
                            drResult[0]["REQUESTUNIT_CHR"] = newRow["REQUESTUNIT_CHR"];
                            drResult[0]["REQUESTPACKQTY_DEC"] = newRow["REQUESTPACKQTY_DEC"];

                            drResult[0]["EXPENSELIMIT_MNY"] = newRow["EXPENSELIMIT_MNY"];

                            drResult[0]["DIFFPRICE_MNY"] = newRow["DIFFPRICE_MNY"];//Added by: 吴汉明 2014-12-9
                            drResult[0]["medbagunit"] = newRow["medbagunit"];
                            drResult[0]["highriskflag"] = newRow["highriskflag"];
                            drResult[0]["isproducedrugs"] = newRow["isproducedrugs"];
                            drResult[0]["transno"] = newRow["transno"];
                            drResult[0]["varietycode"] = newRow["varietycode"];
                        }
                        //for(int i1=0;i1<objResultArr.Rows.Count;i1++)
                        //{
                        //    if(objResultArr.Rows[i1]["MEDICINEID_CHR"].ToString().Trim()==newRow["MEDICINEID_CHR"].ToString().Trim())
                        //    {
                        //        objResultArr.Rows[i1]["MEDICINESTDID_CHR"]=newRow["MEDICINESTDID_CHR"];
                        //        objResultArr.Rows[i1]["PRODUCTORID_CHR"]=newRow["PRODUCTORID_CHR"];
                        //        objResultArr.Rows[i1]["MEDICINESTDESC_VCHR"]=newRow["MEDICINESTDESC_VCHR"];
                        //        objResultArr.Rows[i1]["HYPE_INT"]=newRow["HYPE_INT"];
                        //        objResultArr.Rows[i1]["INSURANCETYPE_VCHR"]=newRow["INSURANCETYPE_VCHR"];
                        //        objResultArr.Rows[i1]["TYPENAME_VCHR"]=newRow["TYPENAME_VCHR"];
                        //        objResultArr.Rows[i1]["ITEMOPCALCTYPE_CHR"]=newRow["ITEMOPCALCTYPE_CHR"];
                        //        objResultArr.Rows[i1]["ITEMIPCALCTYPE_CHR"]=newRow["ITEMIPCALCTYPE_CHR"];
                        //        objResultArr.Rows[i1]["ITEMOPINVTYPE_CHR"]=newRow["ITEMOPINVTYPE_CHR"];
                        //        objResultArr.Rows[i1]["ITEMIPINVTYPE_CHR"]=newRow["ITEMIPINVTYPE_CHR"];
                        //        objResultArr.Rows[i1]["itembihctype_chr"] = newRow["itembihctype_chr"];
                        //        objResultArr.Rows[i1]["MEDNORMALNAME_VCHR"]=newRow["MEDNORMALNAME_VCHR"];
                        //        objResultArr.Rows[i1]["MEDICINENAME_VCHR"]=newRow["MEDICINENAME_VCHR"];
                        //        objResultArr.Rows[i1]["MEDSPEC_VCHR"]=newRow["MEDSPEC_VCHR"];
                        //        objResultArr.Rows[i1]["PYCODE_CHR"]=newRow["PYCODE_CHR"];
                        //        objResultArr.Rows[i1]["WBCODE_CHR"]=newRow["WBCODE_CHR"];
                        //        objResultArr.Rows[i1]["NMLDOSAGE_DEC"]=newRow["NMLDOSAGE_DEC"];
                        //        objResultArr.Rows[i1]["INSURANCEID_VCHR"]=newRow["INSURANCEID_VCHR"];
                        //        objResultArr.Rows[i1]["MINDOSAGE_DEC"]=newRow["MINDOSAGE_DEC"];
                        //        objResultArr.Rows[i1]["MAXDOSAGE_DEC"]=newRow["MAXDOSAGE_DEC"];
                        //        objResultArr.Rows[i1]["ASSISTCODE_CHR"]=newRow["ASSISTCODE_CHR"];
                        //        objResultArr.Rows[i1]["MEDICINEENGNAME_VCHR"]=newRow["MEDICINEENGNAME_VCHR"];
                        //        objResultArr.Rows[i1]["MEDICINETYPENAME_VCHR"]=newRow["MEDICINETYPENAME_VCHR"];
                        //        objResultArr.Rows[i1]["MEDICINEPREPTYPENAME_VCHR"]=newRow["MEDICINEPREPTYPENAME_VCHR"];
                        //        objResultArr.Rows[i1]["MEDSPEC_VCHR"]=newRow["MEDSPEC_VCHR"];
                        //        objResultArr.Rows[i1]["TRADEPRICE_MNY"]=newRow["TRADEPRICE_MNY"];
                        //        objResultArr.Rows[i1]["UNITPRICE_MNY"]=newRow["UNITPRICE_MNY"];
                        //        objResultArr.Rows[i1]["PRODUCTORID_CHR"]=newRow["PRODUCTORID_CHR"];
                        //        objResultArr.Rows[i1]["DOSAGE_DEC"]=newRow["DOSAGE_DEC"];
                        //        objResultArr.Rows[i1]["DOSAGEUNIT_CHR"]=newRow["DOSAGEUNIT_CHR"];
                        //        objResultArr.Rows[i1]["OPUNIT_CHR"]=newRow["OPUNIT_CHR"];
                        //        objResultArr.Rows[i1]["IPUNIT_CHR"]=newRow["IPUNIT_CHR"];
                        //        objResultArr.Rows[i1]["PACKQTY_DEC"] = newRow["PACKQTY_DEC"];
                        //        objResultArr.Rows[i1]["IPUNITPRICE_MNY"] = newRow["IPUNITPRICE_MNY"];
                        //        objResultArr.Rows[i1]["ISANAESTHESIA"]=newRow["ISANAESTHESIA"];
                        //        objResultArr.Rows[i1]["ischlorpromazine2"] = newRow["ischlorpromazine2"];
                        //        objResultArr.Rows[i1]["ispoison"] = newRow["ispoison"];
                        //        objResultArr.Rows[i1]["ISCHLORPROMAZIN"]=newRow["ISCHLORPROMAZIN"];
                        //        objResultArr.Rows[i1]["ISCOSTLY"]=newRow["ISCOSTLY"];
                        //        objResultArr.Rows[i1]["ISSELF"]=newRow["ISSELF"];
                        //        objResultArr.Rows[i1]["ISIMPORT"]=newRow["ISIMPORT"];
                        //        objResultArr.Rows[i1]["ISSELFPAY"]=newRow["ISSELFPAY"];
                        //        objResultArr.Rows[i1]["POFLAG"]=newRow["POFLAG"];
                        //        objResultArr.Rows[i1]["USAGENAME_VCHR"]=newRow["USAGENAME_VCHR"];
                        //        objResultArr.Rows[i1]["OPCHARGEFLG"]=newRow["OPCHARGEFLG"];
                        //        objResultArr.Rows[i1]["IPCHARGEFLG"]=newRow["IPCHARGEFLG"];
                        //        objResultArr.Rows[i1]["IFSTOP"]=newRow["IFSTOP"];
                        //        objResultArr.Rows[i1]["isSTANDARD"]=newRow["isSTANDARD"];
                        //        objResultArr.Rows[i1]["INSURANCEID_VCHR"]=newRow["INSURANCEID_VCHR"];
                        //        objResultArr.Rows[i1]["MEDICNETYPE_INT"] = newRow["MEDICNETYPE_INT"];
                        //        objResultArr.Rows[i1]["DEPTPREP_INT"] = newRow["DEPTPREP_INT"];
                        //        objResultArr.Rows[i1]["DEPTPREP"] = newRow["DEPTPREP"];
                        //        objResultArr.Rows[i1]["ORDERCATEID_CHR"] = newRow["ORDERCATEID_CHR"];
                        //        objResultArr.Rows[i1]["ORDERCATENAME_VCHR"] = newRow["ORDERCATENAME_VCHR"];
                        //        objResultArr.Rows[i1]["FREQID_CHR"] = newRow["FREQID_CHR"];
                        //        objResultArr.Rows[i1]["FREQNAME_CHR"] = newRow["FREQNAME_CHR"];
                        //        objResultArr.Rows[i1]["PUTMEDTYPENAME"] = newRow["PUTMEDTYPENAME"];
                        //        objResultArr.Rows[i1]["PUTMEDTYPE_INT"] = newRow["PUTMEDTYPE_INT"];

                        //        objResultArr.Rows[i1]["ORDERCATEID1_CHR"] = newRow["ORDERCATEID1_CHR"];
                        //        objResultArr.Rows[i1]["NAME_CHR"] = newRow["NAME_CHR"];

                        //        objResultArr.Rows[i1]["INPINSURANCETYPE_VCHR"] = newRow["INPINSURANCETYPE_VCHR"];
                        //        objResultArr.Rows[i1]["typename_vchr1"] = newRow["typename_vchr1"];

                        //        objResultArr.Rows[i1]["ORDERCATEID_CHR"] = newRow["ORDERCATEID_CHR"];
                        //        objResultArr.Rows[i1]["ordercatename_vchr"] = newRow["ordercatename_vchr"];
                        //        objResultArr.Rows[i1]["IFSTOP_INT"] = newRow["IFSTOP_INT"];
                        //        objResultArr.Rows[i1]["LIMITUNITPRICE_MNY"] = newRow["LIMITUNITPRICE_MNY"];
                        //        break;
                        //    }

                        //}
                    }
                    objResultArr.AcceptChanges();
                    FidTable.AcceptChanges();
                    m_lngClearfrm();
                }
            }
        }

        #endregion

        #region 把用户输入数据绑定到DataRow
        private DataRow m_lngFillDataRow()
        {
            DataRow SaveRow = objResultArr.NewRow();
            SaveRow["ITEMOPCALCTYPE_CHR"] = (string)this.m_objViewer.m_txtITEMOPCALCTYPE.Tag;
            SaveRow["ITEMIPCALCTYPE_CHR"] = (string)this.m_objViewer.m_txtITEMIPCALCTYPE.Tag;
            SaveRow["ITEMOPINVTYPE_CHR"] = (string)this.m_objViewer.m_txtITEMOPINVTYPE.Tag;
            SaveRow["ITEMIPINVTYPE_CHR"] = (string)this.m_objViewer.m_txtITEMIPINVTYPE.Tag;
            SaveRow["ITEMBIHCTYPE_CHR"] = (string)this.m_objViewer.ctlTextBoxFind1.Tag;
            SaveRow["PERMITNO_VCHR"] = this.m_objViewer.textBox1.Text;
            SaveRow["MEDICINESTDESC_VCHR"] = this.m_objViewer.LableMed.Text;
            SaveRow["MEDICINESTDID_CHR"] = (string)this.m_objViewer.LableMed.Tag;
            SaveRow["INSURANCETYPE_VCHR"] = this.m_objViewer.ctlCARETYPE.SelectItemValue;
            SaveRow["INPINSURANCETYPE_VCHR"] = this.m_objViewer.exComboBox2.SelectItemValue;
            SaveRow["TYPENAME_VCHR"] = this.m_objViewer.ctlCARETYPE.SelectItemText;
            SaveRow["TYPENAME_VCHR1"] = this.m_objViewer.exComboBox2.SelectItemText;
            if (this.m_objViewer.checkBox1.Checked == true)
                SaveRow["HYPE_INT"] = 1;
            else
                SaveRow["HYPE_INT"] = 0;
            SaveRow["MEDICINEID_CHR"] = (string)this.m_objViewer.m_txtNo.Tag;
            SaveRow["ASSISTCODE_CHR"] = this.m_objViewer.m_txtNo.Text.Trim();
            SaveRow["MEDICINENAME_VCHR"] = this.m_objViewer.m_txtName.Text.Trim();
            SaveRow["NMLDOSAGE_DEC"] = this.m_objViewer.txt_NMLDOSAGE.Text.Trim();
            SaveRow["MEDICINEENGNAME_VCHR"] = this.m_objViewer.m_txtEnName.Text.Trim();
            SaveRow["MEDSPEC_VCHR"] = this.m_objViewer.m_txtSpec.Text.Trim();
            SaveRow["PRODUCTORID_CHR"] = this.m_objViewer.ctlvendor.Text;


            SaveRow["ORDERCATEID1_CHR"] = this.m_objViewer.m_cboCATE1.SelectItemValue;
            SaveRow["NAME_CHR"] = this.m_objViewer.m_cboCATE1.SelectItemText;

            try
            {
                SaveRow["PUTMEDTYPE_INT"] = int.Parse(this.m_objViewer.m_cboPUTMEDTYPE.SelectItemValue);
            }
            catch
            {
            }
            SaveRow["PUTMEDTYPEName"] = this.m_objViewer.m_cboPUTMEDTYPE.SelectItemText;

            if (this.m_objViewer.m_txtUse.txtValuse.Trim() == "")
            {
                SaveRow["usageid_chr"] = "";
                SaveRow["usagename_vchr"] = "";
            }
            else
            {
                SaveRow["usageid_chr"] = (string)this.m_objViewer.m_txtUse.Tag;
                SaveRow["usagename_vchr"] = this.m_objViewer.m_txtUse.txtValuse;
            }
            if (this.m_objViewer.textboxFreq.txtValuse.Trim() == "")
            {
                SaveRow["FREQID_CHR"] = "";
                SaveRow["FREQNAME_CHR"] = "";
            }
            else
            {
                SaveRow["FREQID_CHR"] = (string)this.m_objViewer.textboxFreq.Tag;
                SaveRow["FREQNAME_CHR"] = this.m_objViewer.textboxFreq.txtValuse;
            }
            if (this.m_objViewer.textBoxTypedNumeric1.Text == "")
            {
                SaveRow["LIMITUNITPRICE_MNY"] = "0";
            }
            else
            {
                SaveRow["LIMITUNITPRICE_MNY"] = this.m_objViewer.textBoxTypedNumeric1.Text;
            }
            SaveRow["PHARMAID_CHR"] = (string)this.m_objViewer.m_txtPharMatype.Tag;
            SaveRow["PHARMANAME_VCHR"] = this.m_objViewer.m_txtPharMatype.txtValuse;

            SaveRow["WBCODE_CHR"] = this.m_objViewer.m_txtWB.Text.Trim();
            SaveRow["PYCODE_CHR"] = this.m_objViewer.m_txtPY.Text.Trim();
            SaveRow["MEDNORMALNAME_VCHR"] = this.m_objViewer.txtMEDNORMALNAME.Text.Trim();
            SaveRow["MEDICINETYPEID_CHR"] = this.m_objViewer.m_cboMedType.SelectItemValue;
            SaveRow["MEDICINETYPENAME_VCHR"] = this.m_objViewer.m_cboMedType.Text;
            SaveRow["MEDICNETYPE_INT"] = this.m_objViewer.exComboBox1.SelectedIndex;
            if (dtMeicinePrep.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtMeicinePrep.Rows.Count; i1++)
                {
                    if (dtMeicinePrep.Rows[i1]["MEDICINEPREPTYPENAME_VCHR"].ToString().Trim().Trim() == this.m_objViewer.m_cboPreType.Text.Trim())
                        SaveRow["MEDICINEPREPTYPE_CHR"] = dtMeicinePrep.Rows[i1]["MEDICINEPREPTYPE_CHR"].ToString().Trim();
                }
            }
            SaveRow["MEDICINEPREPTYPENAME_VCHR"] = this.m_objViewer.m_cboPreType.Text;
            if (this.m_objViewer.m_txtDosage.Text.Trim() != "")
                SaveRow["DOSAGE_DEC"] = this.m_objViewer.m_txtDosage.Text.Trim();
            else
                SaveRow["DOSAGE_DEC"] = 0;
            if (this.m_objViewer.m_txtChild.Text.Trim() != "")
                SaveRow["CHILDDOSAGE_DEC"] = this.m_objViewer.m_txtChild.Text.Trim();
            else
                SaveRow["CHILDDOSAGE_DEC"] = 0;

            if (this.m_objViewer.m_txtAdul.Text.Trim() != "")
                SaveRow["ADULTDOSAGE_DEC"] = this.m_objViewer.m_txtAdul.Text.Trim();
            else
                SaveRow["ADULTDOSAGE_DEC"] = 0;

            SaveRow["DOSAGEUNIT_CHR"] = this.m_objViewer.m_CobDosageUnit.Text.Trim();
            if (this.m_objViewer.m_txtTRADEPRICE.Text.Trim() != "")
                SaveRow["TRADEPRICE_MNY"] = this.m_objViewer.m_txtTRADEPRICE.Text.Trim();
            else
                SaveRow["TRADEPRICE_MNY"] = 0;
            if (this.m_objViewer.m_txtUNITPRICE.Text.Trim() != "")
                SaveRow["UNITPRICE_MNY"] = this.m_objViewer.m_txtUNITPRICE.Text.Trim();
            else
                SaveRow["UNITPRICE_MNY"] = 0;
            /*
             * Added by: 吴汉明 2014-12-9
             * Summury:药品让利
             * AddStart:
             */
            if (!string.IsNullOrEmpty(this.m_objViewer.m_txtDiffPrice.Text.Trim()))
                SaveRow["DIFFPRICE_MNY"] = this.m_objViewer.m_txtDiffPrice.Text.Trim();
            else
                SaveRow["DIFFPRICE_MNY"] = 0;
            /*
             * Add End
             */
            SaveRow["OPUNIT_CHR"] = this.m_objViewer.m_CobUnit.Text.Trim();
            SaveRow["IPUNIT_CHR"] = this.m_objViewer.m_CobIpUnit.Text.Trim();
            if (this.m_objViewer.m_txtPackQty.Text.Trim() != "")
                SaveRow["PACKQTY_DEC"] = this.m_objViewer.m_txtPackQty.Text;
            else
                SaveRow["PACKQTY_DEC"] = 0;
            if (!string.IsNullOrEmpty(this.m_objViewer.m_txtIpUnitPrice.Text.Trim()))
            {
                SaveRow["IPUNITPRICE_MNY"] = this.m_objViewer.m_txtIpUnitPrice.Text;
            }
            else
            {
                SaveRow["IPUNITPRICE_MNY"] = 0;
            }
            if (this.m_objViewer.mixuser.Text.Trim() != "")
                SaveRow["MINDOSAGE_DEC"] = this.m_objViewer.mixuser.Text.Trim();
            else
                SaveRow["MINDOSAGE_DEC"] = 0;
            if (this.m_objViewer.maxuser.Text.Trim() != "")
                SaveRow["MAXDOSAGE_DEC"] = this.m_objViewer.maxuser.Text.Trim();
            else
                SaveRow["MAXDOSAGE_DEC"] = 0;
            SaveRow["INSURANCEID_VCHR"] = this.m_objViewer.m_txtInsuranceID.Text.Trim();
            SaveRow["ORDERCATEID_CHR"] = this.m_objViewer.m_cobCat.SelectItemValue;
            SaveRow["ORDERCATENAME_VCHR"] = this.m_objViewer.m_cobCat.SelectItemText;

            if (this.m_objViewer.m_cmbIsInDocAdv.Text == "是")
            {
                SaveRow["POFLAG_INT"] = 1;
                SaveRow["POFLAG"] = "是";
            }
            else
            {
                SaveRow["POFLAG_INT"] = 0;
                SaveRow["POFLAG"] = "否";
            }
            if (this.m_objViewer.isStop.Checked)
            {
                SaveRow["IFSTOP_INT"] = 1;
                SaveRow["IFSTOP"] = "是(停用)";
            }
            else
            {
                SaveRow["IFSTOP_INT"] = 0;
                SaveRow["IFSTOP"] = "否(正常)";
            }
            if (this.m_objViewer.cboOPCHARGEFLG.Text == "最小单位")
            {
                SaveRow["OPCHARGEFLG_INT"] = 1;
                SaveRow["OPCHARGEFLG"] = "最小单位";
            }
            else
            {
                SaveRow["OPCHARGEFLG_INT"] = 0;
                SaveRow["OPCHARGEFLG"] = "基本单位";
            }
            if (this.m_objViewer.cboIPCHARGEFLG.Text == "最小单位")
            {
                SaveRow["IPCHARGEFLG_INT"] = 1;
                SaveRow["IPCHARGEFLG"] = "最小单位";
            }
            else
            {
                SaveRow["IPCHARGEFLG_INT"] = 0;
                SaveRow["IPCHARGEFLG"] = "基本单位";
            }
            if (this.m_objViewer.m_chkIsAnaesthesia.Checked == true)
            {
                SaveRow["ISANAESTHESIA_CHR"] = "T";
                SaveRow["ISANAESTHESIA"] = "√";
            }
            else
            {
                SaveRow["ISANAESTHESIA_CHR"] = "F";
                SaveRow["ISANAESTHESIA"] = "×";
            }
            if (this.m_objViewer.m_chkIsChlorpromazine.Checked == true)
            {
                SaveRow["ISCHLORPROMAZINE_CHR"] = "T";
                SaveRow["ISCHLORPROMAZIN"] = "√";
            }
            else
            {
                SaveRow["ISCHLORPROMAZINE_CHR"] = "F";
                SaveRow["ISCHLORPROMAZIN"] = "×";
            }
            if (this.m_objViewer.m_chkIsCostly.Checked == true)
            {
                SaveRow["ISCOSTLY_CHR"] = "T";
                SaveRow["ISCOSTLY"] = "√";
            }
            else
            {
                SaveRow["ISCOSTLY_CHR"] = "F";
                SaveRow["ISCOSTLY"] = "×";
            }
            if (this.m_objViewer.m_chkIsSelf.Checked == true)
            {
                SaveRow["ISSELF_CHR"] = "T";
                SaveRow["ISSELF"] = "√";
            }
            else
            {
                SaveRow["ISSELF_CHR"] = "F";
                SaveRow["ISSELF"] = "×";
            }
            //if(this.m_objViewer.m_chkIsImport.Checked==true)
            //{
            //    SaveRow["ISIMPORT_CHR"]="T";
            //    SaveRow["ISIMPORT"]="√";
            //}
            //else
            //{
            //    SaveRow["ISIMPORT_CHR"]="F";
            //    SaveRow["ISIMPORT"]="×";
            //}
            if (this.m_objViewer.m_rbxF.Checked)
            {
                SaveRow["ISIMPORT_CHR"] = "F";
                SaveRow["ISIMPORT"] = "国产";
            }
            else if (this.m_objViewer.m_rbxT.Checked)
            {
                SaveRow["ISIMPORT_CHR"] = "T";
                SaveRow["ISIMPORT"] = "进口";
            }
            else if (this.m_objViewer.m_rbxH.Checked)
            {
                SaveRow["ISIMPORT_CHR"] = "H";
                SaveRow["ISIMPORT"] = "合资";
            }
            if (this.m_objViewer.m_chkIsSelfPay.Checked == true)
            {
                SaveRow["ISSELFPAY_CHR"] = "T";
                SaveRow["ISSELFPAY"] = "√";
            }
            else
            {
                SaveRow["ISSELFPAY_CHR"] = "F";
                SaveRow["ISSELFPAY"] = "×";
            }
            if (this.m_objViewer.isSTANDARD.Checked == true)
            {
                SaveRow["STANDARD_INT"] = 1;
                SaveRow["isSTANDARD"] = "√";
            }
            else
            {
                SaveRow["STANDARD_INT"] = 0;
                SaveRow["isSTANDARD"] = "×";
            }

            SaveRow["STANDARDDATE"] = m_objViewer.m_strStandarddateReturn;

            if (this.m_objViewer.checkBox4.Checked == true)
            {
                SaveRow["ISPOISON_CHR"] = "T";
                SaveRow["ISPOISON"] = "√";
            }
            else
            {
                SaveRow["ISPOISON_CHR"] = "F";
                SaveRow["ISPOISON"] = "×";
            }
            if (this.m_objViewer.checkBox3.Checked == true)
            {
                SaveRow["ISCHLORPROMAZINE2_CHR"] = "T";
                SaveRow["ISCHLORPROMAZINE2"] = "√";
            }
            else
            {
                SaveRow["ISCHLORPROMAZINE2_CHR"] = "F";
                SaveRow["ISCHLORPROMAZINE2"] = "×";
            }
            if (this.m_objViewer.checkBox5.Checked)
            {
                SaveRow["DEPTPREP_INT"] = 1;
                SaveRow["DEPTPREP"] = "是";
            }
            else
            {
                SaveRow["DEPTPREP_INT"] = 0;
                SaveRow["DEPTPREP"] = "否";
            }

            SaveRow["REQUESTUNIT_CHR"] = this.m_objViewer.m_CobRequestUnit.Text.Trim();
            if (this.m_objViewer.m_txtRequestPackQty.Text.Trim() != "")
                SaveRow["REQUESTPACKQTY_DEC"] = this.m_objViewer.m_txtRequestPackQty.Text;
            else
                SaveRow["REQUESTPACKQTY_DEC"] = 0;

            if (this.m_objViewer.m_txtExpenseLimit.Text.Trim() != "")
            {
                SaveRow["EXPENSELIMIT_MNY"] = this.m_objViewer.m_txtExpenseLimit.Text.Trim();
            }
            else
            {
                SaveRow["EXPENSELIMIT_MNY"] = 0;
            }
            SaveRow["medbagunit"] = this.m_objViewer.cboMedBagUnit.Text.Trim();
            SaveRow["highriskflag"] = this.m_objViewer.cboHighRisk.SelectedIndex;
            SaveRow["isproducedrugs"] = this.m_objViewer.chkIsProduceDrugs.Checked ? 1 : 0;
            SaveRow["transno"] = this.m_objViewer.txtTransNo.Text.Trim();
            SaveRow["varietycode"] = this.m_objViewer.txtVarietyCode.Text.Trim();
            return SaveRow;
        }

        private clsAlias_VO m_mthFillAlias()
        {
            clsAlias_VO p_objAlias = new clsAlias_VO();
            if (this.m_objViewer.btnNewAdd.Tag != null)
            { p_objAlias.m_strMedicineId = this.m_objViewer.btnNewAdd.Tag.ToString(); }
            p_objAlias.m_strAliasCode = this.m_objViewer.m_txtNo.Text.Trim();
            p_objAlias.m_strAliasName = this.m_objViewer.txtMEDNORMALNAME.Text.Trim();

            if (this.m_objViewer.label2.Tag != null)
            { p_objAlias.m_strOldAliasName = this.m_objViewer.label2.Tag.ToString(); }
            else
            { p_objAlias.m_strOldAliasName = this.m_objViewer.txtMEDNORMALNAME.Text.Trim(); }

            p_objAlias.m_strPyCode = this.m_objViewer.m_txtPY.Text.Trim();
            p_objAlias.m_strWbCode = this.m_objViewer.m_txtWB.Text.Trim();
            p_objAlias.m_strUserCode = "";
            p_objAlias.m_strOpCode = this.m_objViewer.m_txtOPChargeCode.Text;
            return p_objAlias;
        }
        #endregion

        #region 选择事件
        public void m_lngSeleType()
        {
            //			if(this.m_objViewer.ComType.Text=="所有药品")
            //				m_objDoMain.m_lngGetMetDgList("ALL",out objResultArr,false);
            //			else
            //				m_objDoMain.m_lngGetMetDgList(medTypebt.Rows[this.m_objViewer.ComType.SelectedIndex]["MEDICINETYPEID_CHR"].ToString().Trim(),
            //					out objResultArr);
            //
            //			if(objResultArr.Rows.Count==0)
            //			{
            //				this.m_objViewer.m_dgList.m_mthDeleteAllRow();
            //				return;
            //			}
            //			this.m_objViewer.m_dgList.m_mthSetDataTable(objResultArr);
            //			this.m_objViewer.m_dgList.Tag="objResultArr";

        }
        #endregion

        #region 添加药品到列表
        /// <summary>
        /// 添加药品到列表
        /// </summary>
        /// <param name="objResult"></param>
        public void AddMedicineList(clsMedicine_VO objResult)
        {

            if (objResult != null)
            {
                if (this.IsNewOrUp == true)//是新增
                {
                    System.Data.DataRow objResultRow = objResultArr.NewRow();
                    objResultRow["助记码"] = objResult.m_strASSISTCODE_CHR;
                    objResultRow["药品名称"] = objResult.m_strMedicineName;
                    objResultRow["英文名"] = objResult.m_strMedicineEngName;
                    objResultRow["类别"] = objResult.m_objMedicineType.m_strMedicineTypeName;
                    objResultRow["制剂"] = objResult.m_objMedicinePrepType.m_strMedicinePrepTypeName;
                    objResultRow["规格"] = objResult.m_strMedSpec;
                    objResultRow["批发价"] = objResult.m_dblTRADEPRICE_MNY;
                    objResultRow["单价"] = objResult.m_dblUNITPRICE_MNY;
                    objResultRow["生产厂家"] = objResult.m_objProduct.m_strVendorName;
                    objResultRow["剂量"] = objResult.m_dblDOSAGE_DEC;
                    objResultRow["剂量单位"] = objResult.m_strDOSAGEUNIT_CHR;
                    objResultRow["门诊单位"] = objResult.m_strOPUNIT_CHR;
                    objResultRow["住院单位"] = objResult.m_strIPUNIT_CHR;
                    objResultRow["包装量"] = objResult.m_dblPACKQTY_DEC;
                    objResultRow["麻醉药品"] = objResult.m_strIsAnaesthesia;
                    objResultRow["精神药品"] = objResult.m_strIsChlorpromzine;
                    objResultRow["贵重药品"] = objResult.m_strIsCostly;
                    objResultRow["院内制剂"] = objResult.m_strIsSelf;
                    objResultRow["进口药品"] = objResult.m_strIsImport;
                    objResultRow["自费药品"] = objResult.m_strIsSelfPay;
                    //毒麻药品
                    if (objResult.m_strIsAnaesthesia == "T")
                    {
                        objResultRow["麻醉药品"] = "√";
                    }
                    else
                    {
                        objResultRow["麻醉药品"] = "×";
                    }

                    //精神药品
                    if (objResult.m_strIsChlorpromzine == "T")
                    {
                        objResultRow["精神药品"] = "√";
                    }
                    else
                    {
                        objResultRow["精神药品"] = "×";
                    }

                    //贵重
                    if (objResult.m_strIsCostly == "T")
                    {
                        objResultRow["贵重药品"] = "√";
                    }
                    else
                    {
                        objResultRow["贵重药品"] = "×";
                    }

                    //院内制剂
                    if (objResult.m_strIsSelf == "T")
                    {
                        objResultRow["院内制剂"] = "√";
                    }
                    else
                    {
                        objResultRow["院内制剂"] = "×";
                    }

                    //进口药品
                    if (objResult.m_strIsImport == "T")
                    {
                        objResultRow["进口药品"] = "进口";
                    }
                    else if (objResult.m_strIsImport == "F")
                    {
                        objResultRow["进口药品"] = "国产";
                    }
                    else if (objResult.m_strIsImport == "H")
                    {
                        objResultRow["进口药品"] = "合资";
                    }

                    //自费药品
                    if (objResult.m_strIsSelfPay == "T")
                    {
                        objResultRow["自费药品"] = "√";
                    }
                    else
                    {
                        objResultRow["自费药品"] = "×";
                    }
                    objResultArr.Rows.Add(objResultRow);
                }
                else
                {
                    if (blCommand)
                    {
                        objResultArr.Rows[SelectRow]["助记码"] = objResult.m_strASSISTCODE_CHR;
                        objResultArr.Rows[SelectRow]["药品名称"] = objResult.m_strMedicineName;
                        objResultArr.Rows[SelectRow]["英文名"] = objResult.m_strMedicineEngName;
                        objResultArr.Rows[SelectRow]["类别"] = objResult.m_objMedicineType.m_strMedicineTypeName;
                        objResultArr.Rows[SelectRow]["制剂"] = objResult.m_objMedicinePrepType.m_strMedicinePrepTypeName;
                        objResultArr.Rows[SelectRow]["规格"] = objResult.m_strMedSpec;
                        objResultArr.Rows[SelectRow]["批发价"] = objResult.m_dblTRADEPRICE_MNY;
                        objResultArr.Rows[SelectRow]["单价"] = objResult.m_dblUNITPRICE_MNY;
                        objResultArr.Rows[SelectRow]["生产厂家"] = objResult.m_objProduct.m_strVendorName;
                        objResultArr.Rows[SelectRow]["剂量"] = objResult.m_dblDOSAGE_DEC;
                        objResultArr.Rows[SelectRow]["剂量单位"] = objResult.m_strDOSAGEUNIT_CHR;
                        objResultArr.Rows[SelectRow]["门诊单位"] = objResult.m_strOPUNIT_CHR;
                        objResultArr.Rows[SelectRow]["住院单位"] = objResult.m_strIPUNIT_CHR;
                        objResultArr.Rows[SelectRow]["包装量"] = objResult.m_dblPACKQTY_DEC;
                        objResultArr.Rows[SelectRow]["麻醉药品"] = objResult.m_strIsAnaesthesia;
                        objResultArr.Rows[SelectRow]["精神药品"] = objResult.m_strIsChlorpromzine;
                        objResultArr.Rows[SelectRow]["贵重药品"] = objResult.m_strIsCostly;
                        objResultArr.Rows[SelectRow]["院内制剂"] = objResult.m_strIsSelf;
                        objResultArr.Rows[SelectRow]["进口药品"] = objResult.m_strIsImport;
                        objResultArr.Rows[SelectRow]["自费药品"] = objResult.m_strIsSelfPay;
                        //毒麻药品
                        if (objResult.m_strIsAnaesthesia == "T")
                        {
                            objResultArr.Rows[SelectRow]["麻醉药品"] = "√";
                        }
                        else
                        {
                            objResultArr.Rows[SelectRow]["麻醉药品"] = "×";
                        }

                        //精神药品
                        if (objResult.m_strIsChlorpromzine == "T")
                        {
                            objResultArr.Rows[SelectRow]["精神药品"] = "√";
                        }
                        else
                        {
                            objResultArr.Rows[SelectRow]["精神药品"] = "×";
                        }

                        //贵重
                        if (objResult.m_strIsCostly == "T")
                        {
                            objResultArr.Rows[SelectRow]["贵重药品"] = "√";
                        }
                        else
                        {
                            objResultArr.Rows[SelectRow]["贵重药品"] = "×";
                        }

                        //院内制剂
                        if (objResult.m_strIsSelf == "T")
                        {
                            objResultArr.Rows[SelectRow]["院内制剂"] = "√";
                        }
                        else
                        {
                            objResultArr.Rows[SelectRow]["院内制剂"] = "×";
                        }

                        //进口药品
                        if (objResult.m_strIsImport == "F")
                        {
                            objResultArr.Rows[SelectRow]["进口药品"] = "国产";
                        }
                        else if (objResult.m_strIsImport == "T")
                        {
                            objResultArr.Rows[SelectRow]["进口药品"] = "进口";
                        }
                        if (objResult.m_strIsImport == "H")
                        {
                            objResultArr.Rows[SelectRow]["进口药品"] = "合资";
                        }

                        //自费药品
                        if (objResult.m_strIsSelfPay == "T")
                        {
                            objResultArr.Rows[SelectRow]["自费药品"] = "√";
                        }
                        else
                        {
                            objResultArr.Rows[SelectRow]["自费药品"] = "×";
                        }
                    }
                    else
                    {
                        FidTable.Rows[SelectRow]["助记码"] = objResult.m_strASSISTCODE_CHR;
                        FidTable.Rows[SelectRow]["药品名称"] = objResult.m_strMedicineName;
                        FidTable.Rows[SelectRow]["英文名"] = objResult.m_strMedicineEngName;
                        FidTable.Rows[SelectRow]["类别"] = objResult.m_objMedicineType.m_strMedicineTypeName;
                        FidTable.Rows[SelectRow]["制剂"] = objResult.m_objMedicinePrepType.m_strMedicinePrepTypeName;
                        FidTable.Rows[SelectRow]["规格"] = objResult.m_strMedSpec;
                        FidTable.Rows[SelectRow]["批发价"] = objResult.m_dblTRADEPRICE_MNY;
                        FidTable.Rows[SelectRow]["单价"] = objResult.m_dblUNITPRICE_MNY;
                        FidTable.Rows[SelectRow]["生产厂家"] = objResult.m_objProduct.m_strVendorName;
                        FidTable.Rows[SelectRow]["剂量"] = objResult.m_dblDOSAGE_DEC;
                        FidTable.Rows[SelectRow]["剂量单位"] = objResult.m_strDOSAGEUNIT_CHR;
                        FidTable.Rows[SelectRow]["门诊单位"] = objResult.m_strOPUNIT_CHR;
                        FidTable.Rows[SelectRow]["住院单位"] = objResult.m_strIPUNIT_CHR;
                        FidTable.Rows[SelectRow]["包装量"] = objResult.m_dblPACKQTY_DEC;
                        FidTable.Rows[SelectRow]["麻醉药品"] = objResult.m_strIsAnaesthesia;
                        FidTable.Rows[SelectRow]["精神药品"] = objResult.m_strIsChlorpromzine;
                        FidTable.Rows[SelectRow]["贵重药品"] = objResult.m_strIsCostly;
                        FidTable.Rows[SelectRow]["院内制剂"] = objResult.m_strIsSelf;
                        FidTable.Rows[SelectRow]["进口药品"] = objResult.m_strIsImport;
                        FidTable.Rows[SelectRow]["自费药品"] = objResult.m_strIsSelfPay;
                        //毒麻药品
                        if (objResult.m_strIsAnaesthesia == "T")
                        {
                            FidTable.Rows[SelectRow]["麻醉药品"] = "√";
                        }
                        else
                        {
                            FidTable.Rows[SelectRow]["麻醉药品"] = "×";
                        }

                        //精神药品
                        if (objResult.m_strIsChlorpromzine == "T")
                        {
                            FidTable.Rows[SelectRow]["精神药品"] = "√";
                        }
                        else
                        {
                            FidTable.Rows[SelectRow]["精神药品"] = "×";
                        }

                        //贵重
                        if (objResult.m_strIsCostly == "T")
                        {
                            FidTable.Rows[SelectRow]["贵重药品"] = "√";
                        }
                        else
                        {
                            FidTable.Rows[SelectRow]["贵重药品"] = "×";
                        }

                        //院内制剂
                        if (objResult.m_strIsSelf == "T")
                        {
                            FidTable.Rows[SelectRow]["院内制剂"] = "√";
                        }
                        else
                        {
                            FidTable.Rows[SelectRow]["院内制剂"] = "×";
                        }

                        ////进口药品
                        //if(objResult.m_strIsImport == "T")
                        //{
                        //    FidTable.Rows[SelectRow]["进口药品"]="√";
                        //}
                        //else
                        //{
                        //    FidTable.Rows[SelectRow]["进口药品"]="×";
                        //}
                        //进口药品
                        if (objResult.m_strIsImport == "F")
                        {
                            FidTable.Rows[SelectRow]["进口药品"] = "国产";
                        }
                        else if (objResult.m_strIsImport == "T")
                        {
                            FidTable.Rows[SelectRow]["进口药品"] = "进口";
                        }
                        if (objResult.m_strIsImport == "H")
                        {
                            FidTable.Rows[SelectRow]["进口药品"] = "合资";
                        }

                        //自费药品
                        if (objResult.m_strIsSelfPay == "T")
                        {
                            FidTable.Rows[SelectRow]["自费药品"] = "√";
                        }
                        else
                        {
                            FidTable.Rows[SelectRow]["自费药品"] = "×";
                        }

                    }
                }
            }
        }
        #endregion

        #region 设置选中的项（弹出修改窗体）
        /// <summary>
        /// 设置选中的项（弹出修改窗体）
        /// </summary>
        /// <param name="IsNew"></param>
        public void m_SetItem(bool IsNew)
        {
            frmMedicineInfo objInfo = new frmMedicineInfo();
            clsMedicine_VO objSelectedItem = new clsMedicine_VO();
            objSelectedItem = null;
            this.IsNewOrUp = IsNew;//新增
            if (IsNew == true)
            {
                objInfo.ShowMe(objSelectedItem, this);
                return;
            }
            if (this.m_objViewer.m_dgList.RowCount > 0)
            {
                SelectRow = this.m_objViewer.m_dgList.CurrentCell.RowNumber;
                m_lngFillToVo(SelectRow, out objSelectedItem);
                objInfo.ShowMe(objSelectedItem, this);
            }
        }
        #endregion

        #region 把DataGrid的某一项绑定到VO
        /// <summary>
        /// 把DataGrid的某一项绑定到VO
        /// </summary>
        /// <param name="AnyRow"></param>
        /// <param name="SelectedItemArr"></param>
        private void m_lngFillToVo(int AnyRow, out clsMedicine_VO SelectedItemArr)
        {
            if (blCommand)
            {
                SelectedItemArr = new clsMedicine_VO();
                SelectedItemArr.m_strASSISTCODE_CHR = objResultArr.Rows[AnyRow]["助记码"].ToString().Trim();
                SelectedItemArr.m_strMedicineName = objResultArr.Rows[AnyRow]["药品名称"].ToString().Trim();
                SelectedItemArr.m_strMedicineEngName = objResultArr.Rows[AnyRow]["英文名"].ToString().Trim();
                SelectedItemArr.m_objMedicineType = new clsMedicineType_VO();
                SelectedItemArr.m_objMedicineType.m_strMedicineTypeName = objResultArr.Rows[AnyRow]["类别"].ToString().Trim();
                SelectedItemArr.m_objMedicineType.m_strMedicineTypeID = objResultArr.Rows[AnyRow]["药品类型ID"].ToString().Trim();
                SelectedItemArr.m_objMedicinePrepType = new clsMedicinePrepType_VO();
                SelectedItemArr.m_objMedicinePrepType.m_strMedicinePrepTypeName = objResultArr.Rows[AnyRow]["制剂"].ToString().Trim();
                SelectedItemArr.m_objMedicinePrepType.m_strMedicinePrepTypeID = objResultArr.Rows[AnyRow]["制剂类型ID"].ToString().Trim();
                SelectedItemArr.m_strMedSpec = objResultArr.Rows[AnyRow]["规格"].ToString().Trim();
                if (objResultArr.Rows[AnyRow]["批发价"].ToString().Trim() != "")
                {
                    SelectedItemArr.m_dblTRADEPRICE_MNY = Convert.ToDouble(objResultArr.Rows[AnyRow]["批发价"].ToString().Trim());
                }
                if (objResultArr.Rows[AnyRow]["单价"].ToString().Trim() != "")
                {
                    SelectedItemArr.m_dblUNITPRICE_MNY = Convert.ToDouble(objResultArr.Rows[AnyRow]["单价"].ToString().Trim());
                }
                SelectedItemArr.m_objProduct = new clsVendor_VO();
                SelectedItemArr.m_objProduct.m_strVendorName = objResultArr.Rows[AnyRow]["生产厂家"].ToString().Trim();
                if (objResultArr.Rows[AnyRow]["剂量"].ToString().Trim() != "")
                {
                    SelectedItemArr.m_dblDOSAGE_DEC = Convert.ToDouble(objResultArr.Rows[AnyRow]["剂量"].ToString().Trim());
                }
                SelectedItemArr.m_strDOSAGEUNIT_CHR = objResultArr.Rows[AnyRow]["剂量单位"].ToString().Trim();
                SelectedItemArr.m_strOPUNIT_CHR = objResultArr.Rows[AnyRow]["门诊单位"].ToString().Trim();
                SelectedItemArr.m_strIPUNIT_CHR = objResultArr.Rows[AnyRow]["住院单位"].ToString().Trim();
                if (objResultArr.Rows[AnyRow]["包装量"].ToString().Trim() != "")
                {
                    SelectedItemArr.m_dblPACKQTY_DEC = Convert.ToDouble(objResultArr.Rows[AnyRow]["包装量"].ToString().Trim());
                }
                SelectedItemArr.m_strIsAnaesthesia = objResultArr.Rows[AnyRow]["麻醉药品"].ToString().Trim();
                SelectedItemArr.m_strIsChlorpromzine = objResultArr.Rows[AnyRow]["精神药品"].ToString().Trim();
                SelectedItemArr.m_strIsSelf = objResultArr.Rows[AnyRow]["院内制剂"].ToString().Trim();
                SelectedItemArr.m_strIsCostly = objResultArr.Rows[AnyRow]["贵重药品"].ToString().Trim();
                SelectedItemArr.m_strIsImport = objResultArr.Rows[AnyRow]["进口药品"].ToString().Trim();
                SelectedItemArr.m_strIsSelfPay = objResultArr.Rows[AnyRow]["自费药品"].ToString().Trim();
                SelectedItemArr.m_strMedicineID = objResultArr.Rows[AnyRow]["药品代码"].ToString().Trim();
                SelectedItemArr.m_strWBCode = objResultArr.Rows[AnyRow]["五笔码"].ToString().Trim();
                SelectedItemArr.m_strPYCode = objResultArr.Rows[AnyRow]["拼音码"].ToString().Trim();
            }
            else
            {
                SelectedItemArr = new clsMedicine_VO();
                SelectedItemArr.m_strASSISTCODE_CHR = FidTable.Rows[AnyRow]["助记码"].ToString().Trim();
                SelectedItemArr.m_strMedicineName = FidTable.Rows[AnyRow]["药品名称"].ToString().Trim();
                SelectedItemArr.m_strMedicineEngName = FidTable.Rows[AnyRow]["英文名"].ToString().Trim();
                SelectedItemArr.m_objMedicineType = new clsMedicineType_VO();
                SelectedItemArr.m_objMedicineType.m_strMedicineTypeName = FidTable.Rows[AnyRow]["类别"].ToString().Trim();
                SelectedItemArr.m_objMedicineType.m_strMedicineTypeID = FidTable.Rows[AnyRow]["药品类型ID"].ToString().Trim();
                SelectedItemArr.m_objMedicinePrepType = new clsMedicinePrepType_VO();
                SelectedItemArr.m_objMedicinePrepType.m_strMedicinePrepTypeName = FidTable.Rows[AnyRow]["制剂"].ToString().Trim();
                SelectedItemArr.m_objMedicinePrepType.m_strMedicinePrepTypeID = FidTable.Rows[AnyRow]["制剂类型ID"].ToString().Trim();
                SelectedItemArr.m_strMedSpec = FidTable.Rows[AnyRow]["规格"].ToString().Trim();
                if (FidTable.Rows[AnyRow]["批发价"].ToString().Trim() != "")
                {
                    SelectedItemArr.m_dblTRADEPRICE_MNY = Convert.ToDouble(FidTable.Rows[AnyRow]["批发价"].ToString().Trim());
                }
                if (FidTable.Rows[AnyRow]["单价"].ToString().Trim() != "")
                {
                    SelectedItemArr.m_dblUNITPRICE_MNY = Convert.ToDouble(FidTable.Rows[AnyRow]["单价"].ToString().Trim());
                }
                SelectedItemArr.m_objProduct = new clsVendor_VO();
                SelectedItemArr.m_objProduct.m_strVendorName = FidTable.Rows[AnyRow]["生产厂家"].ToString().Trim();
                if (FidTable.Rows[AnyRow]["剂量"].ToString().Trim() != "")
                {
                    SelectedItemArr.m_dblDOSAGE_DEC = Convert.ToDouble(FidTable.Rows[AnyRow]["剂量"].ToString().Trim());
                }
                SelectedItemArr.m_strDOSAGEUNIT_CHR = FidTable.Rows[AnyRow]["剂量单位"].ToString().Trim();
                SelectedItemArr.m_strOPUNIT_CHR = FidTable.Rows[AnyRow]["门诊单位"].ToString().Trim();
                SelectedItemArr.m_strIPUNIT_CHR = FidTable.Rows[AnyRow]["住院单位"].ToString().Trim();
                if (FidTable.Rows[AnyRow]["包装量"].ToString().Trim() != "")
                {
                    SelectedItemArr.m_dblPACKQTY_DEC = Convert.ToDouble(FidTable.Rows[AnyRow]["包装量"].ToString().Trim());
                }
                SelectedItemArr.m_strIsAnaesthesia = FidTable.Rows[AnyRow]["麻醉药品"].ToString().Trim();
                SelectedItemArr.m_strIsChlorpromzine = FidTable.Rows[AnyRow]["精神药品"].ToString().Trim();
                SelectedItemArr.m_strIsSelf = FidTable.Rows[AnyRow]["院内制剂"].ToString().Trim();
                SelectedItemArr.m_strIsCostly = FidTable.Rows[AnyRow]["贵重药品"].ToString().Trim();
                SelectedItemArr.m_strIsImport = FidTable.Rows[AnyRow]["进口药品"].ToString().Trim();
                SelectedItemArr.m_strIsSelfPay = FidTable.Rows[AnyRow]["自费药品"].ToString().Trim();
                SelectedItemArr.m_strMedicineID = FidTable.Rows[AnyRow]["药品代码"].ToString().Trim();
                SelectedItemArr.m_strWBCode = FidTable.Rows[AnyRow]["五笔码"].ToString().Trim();
                SelectedItemArr.m_strPYCode = FidTable.Rows[AnyRow]["拼音码"].ToString().Trim();
            }

        }
        #endregion

        #region 删除药品信息
        /// <summary>
        /// 删除药品信息
        /// </summary>
        public void m_lngDelMedInfo()
        {
            if (m_objViewer.m_dgList.CurrentCell.RowNumber >= 0)
            {
                bool isDeleItem = false;
                if (MessageBox.Show("是否要同时删除收费项目数据？", "icare", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    isDeleItem = true;
                }
                long lngRes = 0;
                string strID;
                if ((string)this.m_objViewer.m_dgList.Tag == "objResultArr")
                    strID = objResultArr.Rows[m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINEID_CHR"].ToString().Trim();
                else
                    strID = FidTable.Rows[m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINEID_CHR"].ToString().Trim();
                lngRes = m_objDoMain.m_lngDeleteMedicineByID(strID, isDeleItem, this.m_objViewer.LoginInfo.m_strEmpID);
                if (lngRes > 0)
                {
                    MessageBox.Show("删除成功", "icare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_lngClearfrm();
                    if ((string)this.m_objViewer.m_dgList.Tag == "objResultArr")
                    {
                        objResultArr.Rows[m_objViewer.m_dgList.CurrentCell.RowNumber].Delete();
                        objResultArr.AcceptChanges();
                    }
                    else
                    {
                        for (int i1 = 0; i1 < objResultArr.Rows.Count; i1++)
                        {
                            if (objResultArr.Rows[i1]["MEDICINEID_CHR"].ToString().Trim() == FidTable.Rows[m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINEID_CHR"].ToString().Trim())
                            {
                                objResultArr.Rows[i1].Delete();
                                FidTable.Rows[m_objViewer.m_dgList.CurrentCell.RowNumber].Delete();
                                FidTable.AcceptChanges();
                                objResultArr.AcceptChanges();
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的药品", "icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }
        #endregion

        #region 把用户选择的数据填充到编辑界面
        public void m_lngFillEditFrm()
        {
            isAddNew = 0;
            DataRow EditRow = objResultArr.NewRow();
            if ((string)this.m_objViewer.m_dgList.Tag == "objResultArr")
                EditRow = objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber];
            else
                EditRow = FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber];
            if (EditRow["ITEMOPCALCTYPE_CHR"].ToString().Trim() != "" && dtItemextype.Rows.Count > 0)
            {
                this.m_objViewer.m_txtITEMOPCALCTYPE.Tag = EditRow["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                for (int i1 = 0; i1 < dtItemextype.Rows.Count; i1++)
                {
                    if (dtItemextype.Rows[i1]["I   D"].ToString().Trim() == EditRow["ITEMOPCALCTYPE_CHR"].ToString().Trim())
                    {
                        this.m_objViewer.m_txtITEMOPCALCTYPE.txtValuse = dtItemextype.Rows[i1]["门诊核算名称"].ToString().Trim();
                        break;
                    }
                }
            }
            else
            {
                this.m_objViewer.m_txtITEMOPCALCTYPE.Tag = null;
                this.m_objViewer.m_txtITEMOPCALCTYPE.txtValuse = "";
            }


            if (EditRow["ITEMOPINVTYPE_CHR"].ToString().Trim() != "" && dtItemextype1.Rows.Count > 0)
            {
                this.m_objViewer.m_txtITEMOPINVTYPE.Tag = EditRow["ITEMOPINVTYPE_CHR"].ToString().Trim();
                for (int i1 = 0; i1 < dtItemextype1.Rows.Count; i1++)
                {
                    if (dtItemextype1.Rows[i1]["I   D"].ToString().Trim() == EditRow["ITEMOPINVTYPE_CHR"].ToString().Trim())
                    {
                        this.m_objViewer.m_txtITEMOPINVTYPE.txtValuse = dtItemextype1.Rows[i1]["门诊发票名称"].ToString().Trim();
                        break;
                    }
                }
            }
            else
            {
                this.m_objViewer.m_txtITEMOPINVTYPE.Tag = null;
                this.m_objViewer.m_txtITEMOPINVTYPE.txtValuse = "";
            }

            if (EditRow["ITEMIPCALCTYPE_CHR"].ToString().Trim() != "" && dtItemextype3.Rows.Count > 0)
            {

                this.m_objViewer.m_txtITEMIPCALCTYPE.Tag = EditRow["ITEMIPCALCTYPE_CHR"].ToString().Trim();
                for (int i1 = 0; i1 < dtItemextype3.Rows.Count; i1++)
                {
                    if (dtItemextype3.Rows[i1]["I   D"].ToString().Trim() == EditRow["ITEMIPCALCTYPE_CHR"].ToString().Trim())
                    {
                        this.m_objViewer.m_txtITEMIPCALCTYPE.txtValuse = dtItemextype3.Rows[i1]["住院核算名称"].ToString().Trim();
                        break;
                    }
                }
            }
            else
            {
                this.m_objViewer.m_txtITEMIPCALCTYPE.Tag = null;
                this.m_objViewer.m_txtITEMIPCALCTYPE.txtValuse = "";
            }


            if (EditRow["ITEMIPINVTYPE_CHR"].ToString().Trim() != "" && dtItemextype4.Rows.Count > 0)
            {
                this.m_objViewer.m_txtITEMIPINVTYPE.Tag = EditRow["ITEMIPINVTYPE_CHR"].ToString().Trim();
                for (int i1 = 0; i1 < dtItemextype4.Rows.Count; i1++)
                {
                    if (dtItemextype4.Rows[i1]["I   D"].ToString().Trim() == EditRow["ITEMIPINVTYPE_CHR"].ToString().Trim())
                    {
                        this.m_objViewer.m_txtITEMIPINVTYPE.txtValuse = dtItemextype4.Rows[i1]["住院发票名称"].ToString().Trim();
                        break;
                    }
                }
            }
            else
            {
                this.m_objViewer.m_txtITEMIPINVTYPE.Tag = null;
                this.m_objViewer.m_txtITEMIPINVTYPE.txtValuse = "";
            }
            if (EditRow["ITEMBIHCTYPE_CHR"].ToString().Trim() != "" && dtItemextype5.Rows.Count > 0)
            {
                this.m_objViewer.ctlTextBoxFind1.Tag = EditRow["ITEMBIHCTYPE_CHR"].ToString().Trim();
                for (int i1 = 0; i1 < dtItemextype5.Rows.Count; i1++)
                {
                    if (dtItemextype5.Rows[i1]["I   D"].ToString().Trim() == EditRow["ITEMBIHCTYPE_CHR"].ToString().Trim())
                    {
                        this.m_objViewer.ctlTextBoxFind1.txtValuse = dtItemextype5.Rows[i1]["病案核算名称"].ToString().Trim();
                        break;
                    }
                }
            }
            else
            {
                this.m_objViewer.ctlTextBoxFind1.Tag = null;
                this.m_objViewer.ctlTextBoxFind1.txtValuse = "";
            }
            string strItemID = null;
            m_objDoMain.m_lngGetItemID(EditRow["MEDICINEID_CHR"].ToString(), out strItemID);
            this.m_objViewer.btnNewAdd.Tag = strItemID;
            this.m_objViewer.textBox1.Text = EditRow["PERMITNO_VCHR"].ToString().Trim();
            this.m_objViewer.m_txtPharMatype.Tag = EditRow["PHARMAID_CHR"].ToString().Trim();
            this.m_objViewer.m_txtPharMatype.txtValuse = EditRow["PHARMANAME_VCHR"].ToString().Trim();
            this.m_objViewer.textBoxTypedNumeric1.Text = EditRow["LIMITUNITPRICE_MNY"].ToString().Trim();
            this.m_objViewer.m_txtAdul.Text = EditRow["ADULTDOSAGE_DEC"].ToString().Trim();
            this.m_objViewer.m_txtChild.Text = EditRow["CHILDDOSAGE_DEC"].ToString().Trim();
            this.m_objViewer.txtMEDNORMALNAME.Text = EditRow["MEDNORMALNAME_VCHR"].ToString().Trim();
            this.m_objViewer.label2.Tag = EditRow["MEDNORMALNAME_VCHR"].ToString().Trim();
            this.m_objViewer.m_txtNo.Tag = EditRow["MEDICINEID_CHR"].ToString().Trim().Trim();
            this.m_objViewer.m_txtNo.Text = EditRow["ASSISTCODE_CHR"].ToString().Trim().Trim();
            this.m_objViewer.m_txtName.Text = EditRow["MEDICINENAME_VCHR"].ToString().Trim().Trim();
            this.m_objViewer.txt_NMLDOSAGE.Text = EditRow["NMLDOSAGE_DEC"].ToString().Trim().Trim();
            this.m_objViewer.m_txtEnName.Text = EditRow["MEDICINEENGNAME_VCHR"].ToString().Trim().Trim();
            this.m_objViewer.m_txtSpec.Text = EditRow["MEDSPEC_VCHR"].ToString().Trim().Trim();
            this.m_objViewer.ctlvendor.Text = EditRow["PRODUCTORID_CHR"].ToString().Trim();
            this.m_objViewer.m_txtWB.Text = EditRow["WBCODE_CHR"].ToString().Trim().Trim();
            this.m_objViewer.m_txtPY.Text = EditRow["PYCODE_CHR"].ToString().Trim().Trim();
            this.m_objViewer.m_cboMedType.Tag = EditRow["MEDICINETYPEID_CHR"].ToString().Trim().Trim();
            this.m_objViewer.m_cboMedType.Text = EditRow["MEDICINETYPENAME_VCHR"].ToString().Trim().Trim();
            this.m_objViewer.m_cboPreType.Tag = EditRow["MEDICINEPREPTYPE_CHR"].ToString().Trim().Trim();
            this.m_objViewer.m_cboPreType.Text = EditRow["MEDICINEPREPTYPENAME_VCHR"].ToString().Trim().Trim();
            this.m_objViewer.m_txtDosage.Text = EditRow["DOSAGE_DEC"].ToString().Trim().Trim();
            this.m_objViewer.m_txtDosage.Text = EditRow["DOSAGE_DEC"].ToString().Trim();
            this.m_objViewer.m_CobDosageUnit.Text = EditRow["DOSAGEUNIT_CHR"].ToString().Trim();
            this.m_objViewer.m_txtTRADEPRICE.Text = EditRow["TRADEPRICE_MNY"].ToString().Trim();
            this.m_objViewer.m_txtUNITPRICE.Text = EditRow["UNITPRICE_MNY"].ToString().Trim();
            this.m_objViewer.m_CobUnit.Text = EditRow["OPUNIT_CHR"].ToString().Trim();
            this.m_objViewer.m_CobIpUnit.Text = EditRow["IPUNIT_CHR"].ToString().Trim();
            this.m_objViewer.m_txtPackQty.Text = EditRow["PACKQTY_DEC"].ToString().Trim();
            if (EditRow["IPUNITPRICE_MNY"] != DBNull.Value)
            {
                this.m_objViewer.m_txtIpUnitPrice.Text = EditRow["IPUNITPRICE_MNY"].ToString();
            }

            this.m_objViewer.mixuser.Text = EditRow["MINDOSAGE_DEC"].ToString().Trim();
            this.m_objViewer.maxuser.Text = EditRow["MAXDOSAGE_DEC"].ToString().Trim();
            this.m_objViewer.m_txtInsuranceID.Text = EditRow["INSURANCEID_VCHR"].ToString().Trim();
            this.m_objViewer.m_cmbIsInDocAdv.Text = EditRow["POFLAG"].ToString().Trim();
            this.m_objViewer.m_txtUse.Tag = EditRow["USAGEID_CHR"].ToString().Trim();
            this.m_objViewer.m_txtUse.txtValuse = EditRow["USAGENAME_VCHR"].ToString().Trim();
            this.m_objViewer.textboxFreq.Tag = EditRow["FREQID_CHR"].ToString().Trim();
            this.m_objViewer.textboxFreq.txtValuse = EditRow["FREQNAME_CHR"].ToString().Trim();
            this.m_objViewer.m_txtOPChargeCode.Text = EditRow["itemopcode_chr"].ToString().Trim();
            if (EditRow["IFSTOP_INT"].ToString().Trim() == "1")
                this.m_objViewer.isStop.Checked = true;
            else
                this.m_objViewer.isStop.Checked = false;
            this.m_objViewer.cboOPCHARGEFLG.Text = EditRow["OPCHARGEFLG"].ToString().Trim();
            this.m_objViewer.cboIPCHARGEFLG.Text = EditRow["IPCHARGEFLG"].ToString().Trim();

            this.m_objViewer.LableMed.Text = EditRow["MEDICINESTDESC_VCHR"].ToString().Trim();
            this.m_objViewer.LableMed.Tag = EditRow["MEDICINESTDID_CHR"].ToString().Trim();

            this.m_objViewer.ctlCARETYPE.Text = EditRow["TYPENAME_VCHR"].ToString().Trim();

            this.m_objViewer.exComboBox2.Text = EditRow["TYPENAME_VCHR1"].ToString().Trim();
            this.m_objViewer.m_cobCat.Text = EditRow["ORDERCATENAME_VCHR"].ToString().Trim();

            this.m_objViewer.m_cboCATE1.Text = EditRow["NAME_CHR"].ToString().Trim();
            this.m_objViewer.m_cboPUTMEDTYPE.Text = EditRow["PUTMEDTYPEName"].ToString().Trim();
            try
            {
                this.m_objViewer.exComboBox1.SelectedIndex = int.Parse(EditRow["MEDICNETYPE_INT"].ToString());
            }
            catch
            {
            }
            if (EditRow["HYPE_INT"].ToString().Trim() == "1")
                this.m_objViewer.checkBox1.Checked = true;
            else
                this.m_objViewer.checkBox1.Checked = false;
            if (EditRow["DEPTPREP_INT"].ToString().Trim() == "1")
                this.m_objViewer.checkBox5.Checked = true;
            else
                this.m_objViewer.checkBox5.Checked = false;

            if (EditRow["ISANAESTHESIA"].ToString().Trim() == "√")
            {
                this.m_objViewer.m_chkIsAnaesthesia.Checked = true;
            }
            else
                this.m_objViewer.m_chkIsAnaesthesia.Checked = false;
            if (EditRow["ISCHLORPROMAZIN"].ToString().Trim() == "√")
            {
                this.m_objViewer.m_chkIsChlorpromazine.Checked = true;
            }
            else
                this.m_objViewer.m_chkIsChlorpromazine.Checked = false;
            if (EditRow["ISCOSTLY"].ToString().Trim() == "√")
            {
                this.m_objViewer.m_chkIsCostly.Checked = true;
            }
            else
                this.m_objViewer.m_chkIsCostly.Checked = false;
            if (EditRow["ISSELF"].ToString().Trim() == "√")
            {
                this.m_objViewer.m_chkIsSelf.Checked = true;
            }
            else
                this.m_objViewer.m_chkIsSelf.Checked = false;
            //if(EditRow["ISIMPORT"].ToString().Trim()=="√")
            //{
            //    this.m_objViewer.m_chkIsImport.Checked=true;
            //}
            //else
            //    this.m_objViewer.m_chkIsImport.Checked=false;
            if (EditRow["ISIMPORT"].ToString().Trim() == "国产")
            {
                this.m_objViewer.m_rbxF.Checked = true;
            }
            else if (EditRow["ISIMPORT"].ToString().Trim() == "进口")
            {
                this.m_objViewer.m_rbxT.Checked = true;
            }
            else if (EditRow["ISIMPORT"].ToString().Trim() == "合资")
            {
                this.m_objViewer.m_rbxH.Checked = true;
            }
            if (EditRow["ISSELFPAY"].ToString().Trim() == "√")
            {
                this.m_objViewer.m_chkIsSelfPay.Checked = true;
            }
            else
                this.m_objViewer.m_chkIsSelfPay.Checked = false;
            if (EditRow["isSTANDARD"].ToString().Trim() == "√")
            {
                this.m_objViewer.isSTANDARD.Checked = true;
            }
            else
                this.m_objViewer.isSTANDARD.Checked = false;
            m_objViewer.m_strStandarddateReturn = Convert.ToString(EditRow["STANDARDDATE"]);

            if (EditRow["ispoison"].ToString().Trim() == "√")
            {
                this.m_objViewer.checkBox4.Checked = true;
            }
            else
                this.m_objViewer.checkBox4.Checked = false;
            if (EditRow["ischlorpromazine2"].ToString().Trim() == "√")
            {
                this.m_objViewer.checkBox3.Checked = true;
            }
            else
                this.m_objViewer.checkBox3.Checked = false;

            this.m_objViewer.m_CobRequestUnit.Text = EditRow["REQUESTUNIT_CHR"].ToString().Trim();
            this.m_objViewer.m_txtRequestPackQty.Text = EditRow["REQUESTPACKQTY_DEC"].ToString().Trim();

            //杨镇伟添加
            if (this.m_objViewer.Readonly == "1")
            {
                this.m_mthControllEnabled();
            }

            this.m_objViewer.m_txtExpenseLimit.Text = EditRow["EXPENSELIMIT_MNY"].ToString().Trim();
            this.m_objViewer.cboMedBagUnit.Text = EditRow["medbagunit"].ToString().Trim();
            this.m_objViewer.cboHighRisk.SelectedIndex = EditRow["highriskflag"] == DBNull.Value ? 0 : Convert.ToInt32(EditRow["highriskflag"]);
            this.m_objViewer.chkIsProduceDrugs.Checked = (EditRow["isproducedrugs"] != DBNull.Value && Convert.ToInt32(EditRow["isproducedrugs"]) == 1) ? true : false;
            this.m_objViewer.txtTransNo.Text = EditRow["transno"].ToString().Trim();
            this.m_objViewer.txtVarietyCode.Text = EditRow["varietycode"].ToString().Trim();
        }
        #endregion

        #region 返回按钮事件
        public void m_lngReture()
        {
            if (!blCommand)
            {
                this.m_objViewer.m_dgList.m_mthFormatReset();
                this.m_objViewer.m_dgList.m_mthSetDataTable(objResultArr);
                this.m_objViewer.m_dgList.Tag = "objResultArr";
                m_frmClear();
                blCommand = true;
                this.m_objViewer.m_dgList.Refresh();
            }

        }
        #endregion

        #region 查找事件
        /// <summary>
        /// 查找事件
        /// </summary>
        public void m_lngFind()
        {
            if (m_CheckValues())
            {
                intfind = 0;
                FidTable.Clear();
                try
                {
                    FidTable = objResultArr.Clone();
                }
                catch
                {
                }
                int Number = 0;
                bool IsFind = false;
                if (this.m_objViewer.m_cboFindContent.Text.Trim().IndexOf("%") == 0)
                {
                    IsFind = Number != 0;
                }
                else
                {
                    IsFind = Number == 0;
                }
                string fidCode = "";
                string fidName = "";
                string fidEnName = "";
                string fidPYCode = "";
                string fidWDCode = "";
                string strSele = "";
                string fidZJName = "";
                if (this.m_objViewer.m_CboSelect.SelectedIndex == 0)
                {
                    fidCode = this.m_objViewer.m_cboFindContent.Text.Trim();
                    strSele = "ASSISTCODE_CHR like '" + fidCode + "%'";
                }
                if (this.m_objViewer.m_CboSelect.SelectedIndex == 1)
                {
                    fidName = this.m_objViewer.m_cboFindContent.Text.Trim();
                    strSele = "MEDICINENAME_VCHR like '" + fidName + "%'";
                }
                if (this.m_objViewer.m_CboSelect.SelectedIndex == 2)
                {
                    fidEnName = this.m_objViewer.m_cboFindContent.Text.Trim();
                    strSele = "MEDICINEENGNAME_VCHR like '" + fidEnName + "%'";
                }
                if (this.m_objViewer.m_CboSelect.SelectedIndex == 3)
                {
                    fidPYCode = this.m_objViewer.m_cboFindContent.Text.Trim().ToUpper();
                    strSele = "PYCODE_CHR like '" + fidPYCode + "%'";
                }
                if (this.m_objViewer.m_CboSelect.SelectedIndex == 4)
                {
                    fidWDCode = this.m_objViewer.m_cboFindContent.Text.Trim().ToUpper();
                    strSele = "WBCODE_CHR like '" + fidWDCode + "%'";
                }
                if (this.m_objViewer.m_CboSelect.SelectedIndex == 5)
                {
                    fidZJName = this.m_objViewer.m_cboFindContent.Text.Trim().ToUpper();
                    if (fidZJName == "")
                    {
                        strSele = " MEDICINEPREPTYPENAME_VCHR is null ";
                    }
                    else
                    {
                        strSele = " MEDICINEPREPTYPENAME_VCHR like '" + fidZJName + "%'";
                    }
                }

                DataRow[] seleRow = objResultArr.Select(strSele);
                intfind = seleRow.Length;
                if (intfind > 0)
                {
                    for (int i1 = 0; i1 < seleRow.Length; i1++)
                    {
                        m_AddRow(seleRow[i1], ref FidTable);
                    }
                }
                this.m_objViewer.m_dgList.m_mthSetDataTable(FidTable);
                this.m_objViewer.m_dgList.Tag = "FidTable";
                this.m_objViewer.m_dgList.m_mthFormatReset();
                blCommand = false;
            }
            else
            {
                MessageBox.Show("请输入查找条件", "icare", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
        }
        #endregion

        #region 构造DataTable的行数据
        /// <summary>
        /// 构造DataTable的行数据
        /// </summary>
        /// <param name="ResultArrRow"></param>
        /// <param name="FidTable"></param>
        private void m_AddRow(DataRow ResultArrRow, ref DataTable FidTable)
        {
            DataRow objRow = FidTable.NewRow();
            objRow["USAGEID_CHR"] = ResultArrRow["USAGEID_CHR"];
            objRow["ADULTDOSAGE_DEC"] = ResultArrRow["ADULTDOSAGE_DEC"];
            objRow["PRODUCTORID_CHR"] = ResultArrRow["PRODUCTORID_CHR"];
            objRow["MEDNORMALNAME_VCHR"] = ResultArrRow["MEDNORMALNAME_VCHR"];
            objRow["CHILDDOSAGE_DEC"] = ResultArrRow["CHILDDOSAGE_DEC"];
            objRow["MEDICINEID_CHR"] = ResultArrRow["MEDICINEID_CHR"];
            objRow["MEDICINENAME_VCHR"] = ResultArrRow["MEDICINENAME_VCHR"];
            objRow["MEDSPEC_VCHR"] = ResultArrRow["MEDSPEC_VCHR"];
            objRow["PYCODE_CHR"] = ResultArrRow["PYCODE_CHR"];
            objRow["NMLDOSAGE_DEC"] = ResultArrRow["NMLDOSAGE_DEC"];
            objRow["WBCODE_CHR"] = ResultArrRow["WBCODE_CHR"];
            objRow["INSURANCEID_VCHR"] = ResultArrRow["INSURANCEID_VCHR"];
            objRow["ASSISTCODE_CHR"] = ResultArrRow["ASSISTCODE_CHR"];
            objRow["MEDICINEENGNAME_VCHR"] = ResultArrRow["MEDICINEENGNAME_VCHR"];
            objRow["MEDICINETYPENAME_VCHR"] = ResultArrRow["MEDICINETYPENAME_VCHR"];
            objRow["MEDICINEPREPTYPENAME_VCHR"] = ResultArrRow["MEDICINEPREPTYPENAME_VCHR"];
            objRow["MEDSPEC_VCHR"] = ResultArrRow["MEDSPEC_VCHR"];
            objRow["TRADEPRICE_MNY"] = ResultArrRow["TRADEPRICE_MNY"];
            objRow["UNITPRICE_MNY"] = ResultArrRow["UNITPRICE_MNY"];
            objRow["PRODUCTORID_CHR"] = ResultArrRow["PRODUCTORID_CHR"];
            objRow["DOSAGE_DEC"] = ResultArrRow["DOSAGE_DEC"];
            objRow["DOSAGEUNIT_CHR"] = ResultArrRow["DOSAGEUNIT_CHR"];
            objRow["OPUNIT_CHR"] = ResultArrRow["OPUNIT_CHR"];
            objRow["PACKQTY_DEC"] = ResultArrRow["PACKQTY_DEC"];
            objRow["IPUNIT_CHR"] = ResultArrRow["IPUNIT_CHR"];
            objRow["ISANAESTHESIA"] = ResultArrRow["ISANAESTHESIA"];
            objRow["ISCHLORPROMAZIN"] = ResultArrRow["ISCHLORPROMAZIN"];
            objRow["ISCOSTLY"] = ResultArrRow["ISCOSTLY"];
            objRow["ISSELF"] = ResultArrRow["ISSELF"];
            objRow["ISIMPORT"] = ResultArrRow["ISIMPORT"];
            objRow["ISSELFPAY"] = ResultArrRow["ISSELFPAY"];
            objRow["POFLAG"] = ResultArrRow["POFLAG"];
            objRow["USAGENAME_VCHR"] = ResultArrRow["USAGENAME_VCHR"];
            objRow["OPCHARGEFLG"] = ResultArrRow["OPCHARGEFLG"];
            objRow["IPCHARGEFLG"] = ResultArrRow["IPCHARGEFLG"];
            objRow["IFSTOP"] = ResultArrRow["IFSTOP"];
            objRow["ifstop_int"] = ResultArrRow["ifstop_int"];
            objRow["ISCOSTLY"] = ResultArrRow["ISCOSTLY"];
            objRow["INSURANCEID_VCHR"] = ResultArrRow["INSURANCEID_VCHR"];
            objRow["MINDOSAGE_DEC"] = ResultArrRow["MINDOSAGE_DEC"];
            objRow["MAXDOSAGE_DEC"] = ResultArrRow["MAXDOSAGE_DEC"];
            objRow["NMLDOSAGE_DEC"] = ResultArrRow["NMLDOSAGE_DEC"];
            objRow["ITEMOPCALCTYPE_CHR"] = ResultArrRow["ITEMOPCALCTYPE_CHR"];
            objRow["ITEMIPCALCTYPE_CHR"] = ResultArrRow["ITEMIPCALCTYPE_CHR"];
            objRow["ITEMOPINVTYPE_CHR"] = ResultArrRow["ITEMOPINVTYPE_CHR"];
            objRow["ITEMIPINVTYPE_CHR"] = ResultArrRow["ITEMIPINVTYPE_CHR"];
            objRow["MEDICINESTDID_CHR"] = ResultArrRow["MEDICINESTDID_CHR"];
            objRow["MEDICINESTDESC_VCHR"] = ResultArrRow["MEDICINESTDESC_VCHR"];
            objRow["MEDICNETYPE_INT"] = ResultArrRow["MEDICNETYPE_INT"];
            objRow["pharmaname_vchr"] = ResultArrRow["pharmaname_vchr"];
            objRow["pharmaid_chr"] = ResultArrRow["pharmaid_chr"];
            objRow["HYPE_INT"] = ResultArrRow["HYPE_INT"];
            objRow["INSURANCETYPE_VCHR"] = ResultArrRow["INSURANCETYPE_VCHR"];
            objRow["TYPENAME_VCHR"] = ResultArrRow["TYPENAME_VCHR"];
            objRow["ITEMBIHCTYPE_CHR"] = ResultArrRow["ITEMBIHCTYPE_CHR"];
            objRow["isSTANDARD"] = ResultArrRow["isSTANDARD"];
            objRow["DEPTPREP_INT"] = ResultArrRow["DEPTPREP_INT"];
            objRow["DEPTPREP"] = ResultArrRow["DEPTPREP"];
            objRow["PUTMEDTYPENAME"] = ResultArrRow["PUTMEDTYPENAME"];
            objRow["PUTMEDTYPE_INT"] = ResultArrRow["PUTMEDTYPE_INT"];

            objRow["ORDERCATEID1_CHR"] = ResultArrRow["ORDERCATEID1_CHR"];
            objRow["NAME_CHR"] = ResultArrRow["NAME_CHR"];

            objRow["INPINSURANCETYPE_VCHR"] = ResultArrRow["INPINSURANCETYPE_VCHR"];
            objRow["typename_vchr1"] = ResultArrRow["typename_vchr1"];

            objRow["ORDERCATEID_CHR"] = ResultArrRow["ORDERCATEID_CHR"];
            objRow["ordercatename_vchr"] = ResultArrRow["ordercatename_vchr"];
            objRow["FREQNAME_CHR"] = ResultArrRow["FREQNAME_CHR"];
            objRow["FREQID_CHR"] = ResultArrRow["FREQID_CHR"];
            objRow["LIMITUNITPRICE_MNY"] = ResultArrRow["LIMITUNITPRICE_MNY"];
            objRow["IPUNITPRICE_MNY"] = ResultArrRow["IPUNITPRICE_MNY"];
            objRow["PERMITNO_VCHR"] = ResultArrRow["PERMITNO_VCHR"];
            objRow["STANDARDDATE"] = ResultArrRow["STANDARDDATE"];
            objRow["REQUESTUNIT_CHR"] = ResultArrRow["REQUESTUNIT_CHR"];
            objRow["REQUESTPACKQTY_DEC"] = ResultArrRow["REQUESTPACKQTY_DEC"];
            objRow["EXPENSELIMIT_MNY"] = ResultArrRow["EXPENSELIMIT_MNY"];
            objRow["medbagunit"] = ResultArrRow["medbagunit"];
            objRow["highriskflag"] = ResultArrRow["highriskflag"];
            objRow["isproducedrugs"] = ResultArrRow["isproducedrugs"];
            objRow["transno"] = ResultArrRow["transno"];
            objRow["varietycode"] = ResultArrRow["varietycode"];
            FidTable.Rows.Add(objRow);
        }


        #endregion

        #region 判断用户输入
        /// <summary>
        /// 判断用户输入
        /// </summary>
        /// <returns></returns>
        private bool m_CheckValues()
        {
            if (this.m_objViewer.m_CboSelect.Text != "" || this.m_objViewer.m_cboFindContent.Text.Trim() != "")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 清空输入数据
        public void m_lngClearfrm()
        {
            this.m_objViewer.m_txtOPChargeCode.Text = string.Empty;
            this.m_objViewer.m_cboMedType.Text = "";
            this.m_objViewer.textBox1.Text = "";
            this.m_objViewer.txtMEDNORMALNAME.Text = "";
            this.m_objViewer.LableMed.Tag = null;
            this.m_objViewer.LableMed.Text = "";
            this.m_objViewer.btnNewAdd.Tag = null;
            this.m_objViewer.ctlCARETYPE.Text = "";
            this.m_objViewer.exComboBox2.Text = "";
            this.m_objViewer.exComboBox2.Tag = null;
            this.m_objViewer.ctlCARETYPE.Tag = null;

            this.m_objViewer.m_cboCATE1.Text = "";
            this.m_objViewer.m_cboCATE1.Tag = null;
            this.m_objViewer.m_cboPUTMEDTYPE.Text = "";
            this.m_objViewer.m_cboPUTMEDTYPE.Tag = null;

            this.m_objViewer.checkBox1.Checked = false;
            this.m_objViewer.m_txtPharMatype.txtValuse = "";
            this.m_objViewer.m_txtPharMatype.Tag = null;
            this.m_objViewer.m_txtITEMOPCALCTYPE.Tag = null;
            this.m_objViewer.m_txtITEMOPCALCTYPE.txtValuse = "";
            this.m_objViewer.m_txtITEMOPINVTYPE.Tag = null;
            this.m_objViewer.m_txtITEMOPINVTYPE.txtValuse = "";

            this.m_objViewer.m_txtITEMIPCALCTYPE.Tag = null;
            this.m_objViewer.m_txtITEMIPCALCTYPE.txtValuse = "";
            this.m_objViewer.ctlTextBoxFind1.txtValuse = "";
            this.m_objViewer.m_txtITEMIPINVTYPE.Tag = null;
            this.m_objViewer.m_txtITEMIPINVTYPE.txtValuse = "";
            this.m_objViewer.ctlvendor.Text = "";
            this.m_objViewer.m_cobCat.Text = "";
            this.m_objViewer.m_txtUse.Tag = null;
            this.m_objViewer.m_txtUse.txtValuse = "";
            this.m_objViewer.textboxFreq.Tag = null;
            this.m_objViewer.textboxFreq.txtValuse = "";
            this.m_objViewer.textBoxTypedNumeric1.Text = "";

            this.m_objViewer.m_txtNo.Clear();
            this.m_objViewer.m_txtNo.Tag = null;
            this.m_objViewer.m_txtName.Clear();
            this.m_objViewer.m_txtEnName.Clear();
            this.m_objViewer.txt_NMLDOSAGE.Clear();
            this.m_objViewer.m_txtSpec.Clear();
            this.m_objViewer.ctlvendor.Text = "";
            this.m_objViewer.m_txtWB.Clear();
            this.m_objViewer.m_txtPY.Clear();
            this.m_objViewer.m_cboPreType.Text = "";
            this.m_objViewer.m_txtDosage.Clear();
            this.m_objViewer.m_CobDosageUnit.Text = "";
            this.m_objViewer.m_txtTRADEPRICE.Clear();
            this.m_objViewer.m_txtUNITPRICE.Clear();
            this.m_objViewer.m_CobUnit.Text = "";
            this.m_objViewer.m_CobIpUnit.Text = "";
            this.m_objViewer.m_txtPackQty.Clear();
            this.m_objViewer.mixuser.Clear();
            this.m_objViewer.maxuser.Clear();
            this.m_objViewer.m_txtInsuranceID.Clear();
            this.m_objViewer.m_cmbIsInDocAdv.Text = "";
            this.m_objViewer.isStop.Checked = false;
            this.m_objViewer.cboOPCHARGEFLG.Text = "";
            this.m_objViewer.cboIPCHARGEFLG.Text = "";
            this.m_objViewer.m_chkIsAnaesthesia.Checked = false;
            this.m_objViewer.m_chkIsChlorpromazine.Checked = false;
            this.m_objViewer.m_chkIsCostly.Checked = false;
            this.m_objViewer.m_chkIsSelf.Checked = false;
            //this.m_objViewer.m_chkIsImport.Checked=false;
            this.m_objViewer.m_rbxF.Checked = true;
            this.m_objViewer.m_chkIsSelfPay.Checked = false;
            this.m_objViewer.isSTANDARD.Checked = false;

            this.m_objViewer.checkBox3.Checked = false;
            this.m_objViewer.checkBox4.Checked = false;
            this.m_objViewer.m_txtNo.Focus();

            this.m_objViewer.m_txtAdul.Text = "";
            this.m_objViewer.m_txtChild.Text = "";
            this.m_objViewer.exComboBox1.SelectedIndex = 0;
            this.m_objViewer.checkBox5.Checked = false;
            this.m_objViewer.errorProvider1.SetError(this.m_objViewer.m_txtNo, "");
            this.m_objViewer.errorProvider1.SetError(this.m_objViewer.m_txtName, "");
            m_objViewer.m_strStandarddateReturn = "";
            m_objViewer.m_dtbChargeItem = null;
            this.m_objViewer.m_CobRequestUnit.Text = "";
            this.m_objViewer.m_txtRequestPackQty.Clear();
            isAddNew = 1;
            this.m_objViewer.m_txtExpenseLimit.Text = "";

            this.m_objViewer.m_txtSpec.Enabled = true;
            this.m_objViewer.m_cboCATE1.Enabled = true;
            this.m_objViewer.m_cboPUTMEDTYPE.Enabled = true;
            this.m_objViewer.cboIPCHARGEFLG.Enabled = true;
            this.m_objViewer.cboOPCHARGEFLG.Enabled = true;
            this.m_objViewer.m_txtPackQty.Enabled = true;
            this.m_objViewer.m_txtUNITPRICE.Enabled = true;

            this.m_objViewer.m_txtDiffPrice.Clear();//Added by: 吴汉明 2014-12-9
            this.m_objViewer.cboMedBagUnit.Text = "";
            this.m_objViewer.cboHighRisk.SelectedIndex = 0;
            this.m_objViewer.chkIsProduceDrugs.Checked = false;
        }
        #endregion

        #region 清空输入
        /// <summary>
        /// 清空输入
        /// </summary>
        private void m_frmClear()
        {
            this.m_objViewer.m_CboSelect.Text = "";

            this.m_objViewer.m_cboFindContent.Text = "";


        }
        #endregion

        #region 修改药品对应的药典
        public void m_modify(string CurSTDID, string CurSTDName)
        {
            if (isAddNew == 0 && (string)this.m_objViewer.m_txtNo.Tag != "")
            {
                long lngRes = m_objDoMain.m_lngModifyMEDICINESTDID((string)this.m_objViewer.m_txtNo.Tag, CurSTDID, CurSTDName);
                if (lngRes == 1)
                {
                    if ((string)this.m_objViewer.m_dgList.Tag == "objResultArr")
                    {
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINESTDID_CHR"] = CurSTDID;
                        objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINESTDESC_VCHR"] = CurSTDName;

                    }
                    else
                    {
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINESTDID_CHR"] = CurSTDID;
                        FidTable.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINESTDESC_VCHR"] = CurSTDName;
                        for (int i1 = 0; i1 < objResultArr.Rows.Count; i1++)
                        {
                            if (objResultArr.Rows[i1]["MEDICINEID_CHR"].ToString().Trim() == (string)this.m_objViewer.m_txtNo.Tag)
                            {
                                objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINESTDID_CHR"] = CurSTDID;
                                objResultArr.Rows[this.m_objViewer.m_dgList.CurrentCell.RowNumber]["MEDICINESTDESC_VCHR"] = CurSTDName;
                                break;

                            }
                        }
                    }

                }
            }
        }


        #endregion

        int intCount = 0;
        public void m_mthOutExcel(DataTable p_dt)
        {
            intCount++;
            DataTable dttemp = new DataTable("Table" + intCount.ToString());
            string str = "";
            for (int i = 0; i < p_dt.Columns.Count; i++)
            {
                str = p_dt.Columns[i].ColumnName.Replace("(", "");
                str = str.Replace(")", "");
                dttemp.Columns.Add(str, p_dt.Columns[i].DataType);
            }
            DataRow dr = null;
            for (int i = 0; i < p_dt.Rows.Count; i++)
            {
                dr = dttemp.NewRow();
                for (int i2 = 0; i2 < p_dt.Columns.Count; i2++)
                {
                    dr[i2] = p_dt.Rows[i][i2];
                }
                dttemp.Rows.Add(dr);
            }
            DataSet ds = new DataSet();
            ds.Tables.Clear();
            ds.Tables.Add(dttemp);
            ExcelExporter excel = new ExcelExporter(ds);
            bool b = excel.m_mthExport();
            if (b)
            {
                MessageBox.Show("导出数据成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("导出数据失败。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            ds.Tables.Clear();
            dttemp = null;
            ds = null;
        }

        #region 计算零售价
        /// <summary>
        /// 计算零售价
        /// </summary>
        /// <returns></returns>
        public long m_mthGetGrossprofitrate()
        {
            if (m_objViewer.m_cboMedType.SelectItemValue == null)
            {
                return -1;
            }
            if (m_objViewer.m_txtTRADEPRICE.Text.Trim() == "")
            {
                return -1;
            }
            if (!m_objViewer.m_txtUNITPRICE.Enabled)
            {
                return -1;
            }


            long lngRes = 0;

            //零价＝进价*（1+毛利率）
            if (m_intRet == 1)
            {
                double douGrossprofitrate;
                double douUnitprice;
                lngRes = m_objDoMain.m_lngGetGrossprofitrate(m_objViewer.m_cboMedType.SelectItemValue, out douGrossprofitrate);
                douUnitprice = Convert.ToDouble(m_objViewer.m_txtTRADEPRICE.Text) * (1 + douGrossprofitrate / 100);
                //  douUnitprice = m_mthMathPayment(m_strsto
                douUnitprice = m_mthMathPayment(this.m_objViewer.m_cboMedType.SelectItemValue, Convert.ToDouble(m_objViewer.m_txtTRADEPRICE.Text), douGrossprofitrate);
                m_objViewer.m_txtUNITPRICE.Text = douUnitprice.ToString("0.0000");
                m_objViewer.m_txtDiffPrice.Text = (douUnitprice - Convert.ToDouble(m_objViewer.m_txtTRADEPRICE.Text)).ToString("0.0000");//Added by: 吴汉明 2014-12-9
            }
            return lngRes;
        }
        #endregion

        #region 由购入价计算零售价
        /// <summary>
        /// 由购入价计算零售价
        /// </summary>
        /// <param name="m_strStorageID">仓库ID</param>
        /// <param name="p_dcmBuyIn">购入价</param>
        /// <param name="dblRate">利率</param>
        /// <returns></returns>
        public static double m_mthMathPayment(string m_strStorageID, double p_dcmBuyIn, double dblRate)
        {
            // 药品  按照最新的计算公式来计算零售价
            //（1）X<=10  Y=X*1.25    （2）10<X<=40  Y=X*1.15+1
            //（3）40<X<=200 Y=X*1.1+3 （4）200<X<=600 Y=X*1.08+5
            //（5）600<X<=2000 Y=X*1.06+15 （6）X>2000 Y=X+135
            // 材料
            //关于卫生材料
            //（1）<=1000元  加收10%  即：零售价=1000+购入单价*10%
            //（2）大于1000元   实行累积差率  即：零售价=1000+1000*10%+（购入单价-1000）*8%
            //（3）单次一次性医用消耗材料加收部分，最高不得超过800元
            double dblRetailMoney = 0d;
            // 草药:1 一类疫苗:6  二类疫苗:7 西药:2  综合材料:3  低值材料:4  其他材料:5
            if (m_strStorageID == "1" || m_strStorageID == "2" || m_strStorageID == "6" || m_strStorageID == "7")
            {
                if (p_dcmBuyIn <= 10)
                {
                    dblRetailMoney = p_dcmBuyIn * 1.25;
                }
                else if (p_dcmBuyIn > 10 && p_dcmBuyIn <= 40)
                {
                    dblRetailMoney = p_dcmBuyIn * 1.15 + 1;
                }
                else if (p_dcmBuyIn > 40 && p_dcmBuyIn <= 200)
                {
                    dblRetailMoney = p_dcmBuyIn * 1.1 + 3;
                }
                else if (p_dcmBuyIn > 200 && p_dcmBuyIn <= 600)
                {
                    dblRetailMoney = p_dcmBuyIn * 1.08 + 5;
                }
                else if (p_dcmBuyIn > 600 && p_dcmBuyIn <= 2000)
                {
                    dblRetailMoney = p_dcmBuyIn * 1.06 + 15;
                }
                else
                {
                    dblRetailMoney = p_dcmBuyIn + 135;
                }
            }
            else if (m_strStorageID == "3" || m_strStorageID == "4" || m_strStorageID == "5")
            {
                if (p_dcmBuyIn <= 1000)
                {
                    dblRetailMoney = p_dcmBuyIn * 0.1 + p_dcmBuyIn;
                }
                else if (p_dcmBuyIn > 1000)
                {
                    dblRetailMoney = 1000 + 1000 * 0.1 + (p_dcmBuyIn - 1000) * 1.08;
                    if (m_mthGreaterThan800(dblRetailMoney, p_dcmBuyIn))
                    {
                        dblRetailMoney = p_dcmBuyIn + 800;
                    }
                }
            }
            else
            {
                dblRetailMoney = (double)p_dcmBuyIn * (1 + dblRate / 100);
            }
            return dblRetailMoney;
        }
        /// <summary>
        /// 零售价减去购入价是否大于800
        /// </summary>
        /// <param name="dblRetailMoney"></param>
        /// <param name="p_dcmBuyIn"></param>
        /// <returns></returns>
        private static bool m_mthGreaterThan800(double dblRetailMoney, double p_dcmBuyIn)
        {
            if (dblRetailMoney - p_dcmBuyIn > 800)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 取得中标年份数，默认前后10年
        /// </summary>
        internal int GetStandardDate()
        {
            int iYear;
            m_objDoMain.m_lngGetStandardDate(out iYear);
            if (iYear == 0)
                iYear = 10;
            return iYear;
        }

        /// <summary>
        /// 保存中标年份
        /// </summary>
        /// <param name="p_MedicineID">药品ID</param>
        /// <param name="strReturn">中标年份</param>
        internal long m_lngSaveStandardYear(string p_MedicineID, string p_strYear)
        {
            return m_objDoMain.m_lngSaveStandardYear(p_MedicineID, p_strYear);
        }

        internal long m_lngFillChargeItem(string p_MedicineID, out DataTable p_dtbChargeItem)
        {
            p_dtbChargeItem = new DataTable();
            return m_objDoMain.m_lngFillChargeItem(p_MedicineID, p_dtbChargeItem);
        }

        #region 控制控件是否显示
        /// <summary>
        /// 杨镇伟添加:控制控件是否显示
        /// </summary>
        public void m_mthControllEnabled()
        {
            if (this.m_objViewer.m_strEblControll == "1")
            {
                this.m_objViewer.m_txtSpec.Enabled = true;
                this.m_objViewer.m_cboCATE1.Enabled = true;
                this.m_objViewer.m_cboPUTMEDTYPE.Enabled = true;
                this.m_objViewer.cboIPCHARGEFLG.Enabled = true;
                this.m_objViewer.cboOPCHARGEFLG.Enabled = true;
                this.m_objViewer.m_txtPackQty.Enabled = true;
                this.m_objViewer.m_txtUNITPRICE.Enabled = true;
            }
            else
            {
                this.m_objViewer.m_txtSpec.Enabled = false;
                this.m_objViewer.m_cboCATE1.Enabled = false;
                this.m_objViewer.m_cboPUTMEDTYPE.Enabled = false;
                this.m_objViewer.cboIPCHARGEFLG.Enabled = false;
                this.m_objViewer.cboOPCHARGEFLG.Enabled = false;
                this.m_objViewer.m_txtPackQty.Enabled = false;
                this.m_objViewer.m_txtUNITPRICE.Enabled = false;
            }
        }
        #endregion
    }
}
