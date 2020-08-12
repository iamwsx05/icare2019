using System;
using System.Data;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;
using System.Windows.Forms; 
using System.Collections;
using System.Collections.Generic;
 
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsControlChangPrice 的摘要说明。
    /// </summary>
    public class clsControlChangPrice : com.digitalwave.GUI_Base.clsController_Base //GUI_Base.dll
    {
        public clsControlChangPrice()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        clsDomainConrolChangPrice objSVC = new clsDomainConrolChangPrice();

        #region 设置窗体对象
        /// <summary>
        /// 窗体对象
        /// </summary>
        frmChangPrice m_objViewer;
        public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmChangPrice)frmMDI_Child_Base_in;
        }
        #endregion

        #region 变量
        /// <summary>
        /// 保存药品基本信息
        /// </summary>
        DataTable dtbMedicine = null;
        /// <summary>
        /// 保存查找数据
        /// </summary>
        DataTable dtbFind = new DataTable();
        /// <summary>
        /// 保存所有符合查找条件的调价单数据
        /// </summary>
        clsPriceChgeAppl[] objFindChgeApplAll = new clsPriceChgeAppl[0];

        /// <summary>
        /// 保存财务期数据
        /// </summary>
        clsPeriod_VO[] objItems = new clsPeriod_VO[0];
        /// <summary>
        /// 标志当前的单据状态,true-新增,false-修改
        /// </summary>
        bool isNew = true;

        private System.Data.DataSet dsDelDate;
        /// <summary>
        /// 保存当前财务期的索引
        /// </summary>
        int intSelPeriod = -1;
        /// <summary>
        /// 明细VO
        /// </summary>
        clsPriceChgeApplDe objDe_vo;
        /// <summary>
        /// 公共方法
        /// </summary>
        clsPublicParm publicClass = new clsPublicParm();
        private ListView m_lv;
        /// <summary>
        /// 判断当前删除的是那个表的数据,1-调价单,2-调价明细
        /// </summary>
        int isdele_int = 1;
        /// <summary>
        /// 保存选定调价药品ID
        /// </summary>
        public ArrayList AddArr = new ArrayList();
        /// <summary>
        /// 修改明细还是单据,1-修改明细,2-修改单据
        /// </summary>
        int isModifyDe = 2;
        #endregion

        #region 初始化窗体
        public void m_lngFrmLoad()
        {
            System.Data.DataTable dt;
            this.objSVC.m_lngGetChangeType(out dt);
            if (dt != null)
            {
                this.m_objViewer.m_cmbType.DisplayMember = "TYPENAME_CHR";
                this.m_objViewer.m_cmbType.ValueMember = "TYPEID_CHR";
                this.m_objViewer.m_cmbType.DataSource = dt;
            }
            //com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
            this.m_objViewer.dateTime.Value = (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate();
            m_mthGetPeriodList();
        }
        #endregion

        #region 获得帐务期列表
        /// <summary>
        /// 获得帐务期列表
        /// </summary>
        private void m_mthGetPeriodList()
        {
            objItems = clsPublicParm.s_GetPeriodList();
            string nowdate = clsPublicParm.s_datGetServerDate().Date.ToString();
            if (objItems.Length > 0)
            {
                int intcommand = 0;
                for (int i1 = 0; i1 < objItems.Length; i1++)
                {
                    this.m_objViewer.comPriod.Items.Insert(i1, objItems[i1].m_strStartDate + " 至 " + objItems[i1].m_strEndDate);
                    if (Convert.ToDateTime(nowdate) >= Convert.ToDateTime(objItems[i1].m_strStartDate) && Convert.ToDateTime(nowdate) <= Convert.ToDateTime(objItems[i1].m_strEndDate))
                    {
                        intSelPeriod = i1;
                        this.m_objViewer.comPriod.Tag = objItems[i1].m_strPeriodID;
                    }
                    intcommand = i1;
                }
                this.m_objViewer.comPriod.Items.Insert(intcommand + 1, "所有财务期的数据");
                if (intSelPeriod != -1)
                {
                    m_objViewer.comPriod.SelectedIndex = intSelPeriod;
                }
                else
                {
                    MessageBox.Show("还没有初始化财务期,请先设置财务期!", "系统提示");
                }

            }
        }
        #endregion

        #region 财务期选择事件
        /// <summary>
        /// 财务期选择事件
        /// </summary>
        public void m_lngPriodchang()
        {
            this.m_objViewer.lsvPrice.Items.Clear();
            this.m_objViewer.livPriceOk.Items.Clear();
            this.m_objViewer.lisvChangPriceDe.Items.Clear();
            if (this.m_objViewer.comPriod.Text != "所有财务期的数据")
            {
                m_lngGetAndFill(objItems[this.m_objViewer.comPriod.SelectedIndex].m_strPeriodID);
                this.m_objViewer.comPriod.Tag = objItems[this.m_objViewer.comPriod.SelectedIndex].m_strPeriodID;
            }
            else
            {
                m_lngGetAndFill("");
                this.m_objViewer.comPriod.Tag = "";
            }
        }
        #endregion

        #region 获的所有的调价单并填充到未审核同己审核窗体
        /// <summary>
        /// 获的所有的调价单并填充到未审核同己审核窗体
        /// </summary>
        /// <param name="nowPriod">财务期</param>
        private void m_lngGetAndFill(string nowPriod)
        {
            long lngRes = 0;
            clsPriceChgeAppl[] objprice_vo = new clsPriceChgeAppl[0];
            lngRes = this.objSVC.m_lngGetAllChgAppl(out objprice_vo, nowPriod);
            if (lngRes > 0 && objprice_vo.Length > 0)
            {
                for (int i1 = 0; i1 < objprice_vo.Length; i1++)
                {
                    if (objprice_vo[i1].m_intPSTATUS_INT == 1)
                        m_lngNewAppl(objprice_vo[i1]);
                    else
                        m_lngFillLsvOkAppl(objprice_vo[i1]);
                }
            }

        }
        #endregion

        #region 计算差额
        public void m_lngCountNuber()
        {
            try
            {
                double tolNuber = (Convert.ToDouble(this.m_objViewer.TXTCHANGEPRICE.Text) - Convert.ToDouble(this.m_objViewer.m_txtPRICE.Text)) * Convert.ToDouble(this.m_objViewer.txtAllAmount.Text);
                this.m_objViewer.txtoddsDe.Text = tolNuber.ToString("0.0000");
            }
            catch
            {
                this.m_objViewer.txtoddsDe.Text = "0.0000";
            }
        }
        #endregion

        #region 增加按钮事件
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        public void m_lngAddNew()
        {
            objDe_vo = new clsPriceChgeApplDe();
            objDe_vo.m_strMEDICINEID_CHR = (string)this.m_objViewer.txtMEDICINE.Tag;
            objDe_vo.m_strMEDICINENAME_CHR = this.m_objViewer.txtMEDICINE.Text;
            objDe_vo.m_strMEDSPEC_VCHR = this.m_objViewer.txtMEDSPEC.Text;
            objDe_vo.m_strASSISTCODE_CHR = (string)this.m_objViewer.txtMEDSPEC.Tag;
            objDe_vo.m_strUNITID_CHR = this.m_objViewer.m_txtUNIT.Text.Trim();
            if (this.m_objViewer.m_txtPRICE.Text.Trim() != "")
                objDe_vo.m_dblCURPRICE_MNY = double.Parse(this.m_objViewer.m_txtPRICE.Text.Trim());
            else
                objDe_vo.m_dblCURPRICE_MNY = 0;
            if (this.m_objViewer.TXTCHANGEPRICE.Text.Trim() != "")
                objDe_vo.m_dblCHANGEPRICE_MNY = double.Parse(this.m_objViewer.TXTCHANGEPRICE.Text.Trim());
            else
                objDe_vo.m_dblCHANGEPRICE_MNY = 0;
            //if (this.m_objViewer.m_txtBuyNewPrice.Text.Trim() != "")
            //    objDe_vo.m_dblBUYCHANGEPRICE_MNY = double.Parse(this.m_objViewer.m_txtBuyNewPrice.Text.Trim());
            //else
            //    objDe_vo.m_dblBUYCHANGEPRICE_MNY = 0;
            //if (this.m_objViewer.m_labBuyOldPrice.Text.Trim() != "")
            //    objDe_vo.m_dblBUYCURPRICE_MNY = double.Parse(this.m_objViewer.m_labBuyOldPrice.Text.Trim());
            //else
            //    objDe_vo.m_dblBUYCURPRICE_MNY = 0;

            if (this.m_objViewer.txtAllAmount.Text.Trim() != "")
                objDe_vo.m_dblQTY_DEC = double.Parse(this.m_objViewer.txtAllAmount.Text.Trim());
            else
                objDe_vo.m_dblQTY_DEC = 0;
            if (this.m_objViewer.m_cmbType.Text != "")
                objDe_vo.m_strtypeid = this.m_objViewer.m_cmbType.SelectedValue.ToString();
            objDe_vo.m_strtypeName = this.m_objViewer.m_cmbType.Text.ToString().Trim();
            if (isNew == false)
            {
                string strID = (string)this.m_objViewer.panel4.Tag;
                long lngRes = this.objSVC.m_lngAddDe(strID, objDe_vo);
                if (lngRes < 1)
                {
                    publicClass.m_mthShowWarning(this.m_objViewer.panel4, "增加明细失败!");
                    return;
                }
                else
                {
                    publicClass.m_mthShowWarning(this.m_objViewer.panel4, "增加明细成功!");
                }
            }
            m_lngFillToLsv(objDe_vo);
        }
        #endregion

        #region 计算总差额
        /// <summary>
        /// 计算总差额
        /// </summary>
        /// <returns></returns>
        public double m_getTotalMoney()
        {
            double totalMoney = 0;
            if (this.m_objViewer.lisvChangPriceDe.Items.Count > 0)
            {
                for (int i1 = 0; i1 < this.m_objViewer.lisvChangPriceDe.Items.Count; i1++)
                {
                    try
                    {
                        totalMoney += double.Parse(this.m_objViewer.lisvChangPriceDe.Items[i1].SubItems[8].Text);
                    }
                    catch
                    {
                    }
                }
            }
            return totalMoney;
        }
        #endregion

        #region 把调价明细填充到Listview列表
        /// <summary>
        /// 把调价明细填充到Listview列表
        /// </summary>
        /// <param name="dr"></param>
        private void m_lngFillToLsv(clsPriceChgeApplDe objVO)
        {
            ListViewItem LisTemp = null;
            double dBalance = 0;
            dBalance = objVO.m_dblQTY_DEC * (objVO.m_dblCHANGEPRICE_MNY - objVO.m_dblCURPRICE_MNY);
            objVO.m_dblODDSDE_MNY = dBalance;
            LisTemp = new ListViewItem(objVO.m_strROWNO_CHR);
            LisTemp.SubItems.Add(objVO.m_strASSISTCODE_CHR);
            LisTemp.SubItems.Add(objVO.m_strMEDICINENAME_CHR);
            LisTemp.SubItems.Add(objVO.m_strMEDSPEC_VCHR);
            LisTemp.SubItems.Add(objVO.m_strUNITID_CHR);
            LisTemp.SubItems.Add(objVO.m_dblQTY_DEC.ToString());
            LisTemp.SubItems.Add(objVO.m_dblCURPRICE_MNY.ToString());
            LisTemp.SubItems.Add(objVO.m_dblCHANGEPRICE_MNY.ToString());
            LisTemp.SubItems.Add(objVO.m_dblODDSDE_MNY.ToString());
            LisTemp.SubItems.Add(objVO.m_strtypeName);
            LisTemp.Tag = objVO;
            this.m_objViewer.lisvChangPriceDe.Items.Add(LisTemp);
        }
        #endregion

        #region 选择明细事件
        /// <summary>
        /// 选择明细事件
        /// </summary>
        public void m_mthDClik()
        {
            isdele_int = 2;
            isModifyDe = 1;
            this.m_objViewer.btnAdd.Enabled = false;
            if (this.m_objViewer.lisvChangPriceDe.SelectedItems.Count > 0)
            {
                clsPriceChgeApplDe objDeVo = (clsPriceChgeApplDe)this.m_objViewer.lisvChangPriceDe.SelectedItems[0].Tag;
                this.m_objViewer.txtMEDICINE.Tag = objDeVo.m_strMEDICINEID_CHR;
                this.m_objViewer.txtMEDICINE.Text = objDeVo.m_strMEDICINENAME_CHR;
                this.m_objViewer.txtMEDSPEC.Text = objDeVo.m_strMEDSPEC_VCHR;
                this.m_objViewer.txtMEDSPEC.Tag = objDeVo.m_strASSISTCODE_CHR;
                this.m_objViewer.panel6.Tag = objDeVo.m_strMEDICINEPRICECHGAPPLDEID_CHR;
                this.m_objViewer.m_txtUNIT.Text = objDeVo.m_strUNITID_CHR;
                this.m_objViewer.m_txtPRICE.Text = objDeVo.m_dblCURPRICE_MNY.ToString();
                this.m_objViewer.TXTCHANGEPRICE.Text = objDeVo.m_dblCHANGEPRICE_MNY.ToString();
                this.m_objViewer.txtAllAmount.Text = objDeVo.m_dblQTY_DEC.ToString();
                this.m_objViewer.txtoddsDe.Text = objDeVo.m_dblODDSDE_MNY.ToString();
                this.m_objViewer.m_cmbType.SelectedIndex = this.m_objViewer.m_cmbType.FindString(objDeVo.m_strtypeName);
            }
        }
        #endregion 选择明细事件

        #region 选择单据
        /// <summary>
        /// 选择单据
        /// </summary>
        /// <param name="obj"></param>
        public void m_mthSelectedBill(object obj)
        {
            isdele_int = 1;
            isModifyDe = 2;
            isNew = false;
            ListView lv = (ListView)obj;
            clsPriceChgeAppl BillVO = ((clsPriceChgeAppl)lv.SelectedItems[0].Tag);
            m_fillToTextBox(BillVO);
            this.m_objViewer.lisvChangPriceDe.Items.Clear();
            clsPriceChgeApplDe[] objVO = new clsPriceChgeApplDe[0];
            this.objSVC.m_lngGetChgApplDe(BillVO.m_strMEDICINEPRICECHGAPPLID_CHR, out objVO, this.m_objViewer.tabChang.SelectedIndex == 0 ? 1 : 2);
            AddArr.Clear();
            for (int i = 0; i < objVO.Length; i++)
            {
                AddArr.Add(objVO[i].m_strMEDICINEID_CHR);
                m_lngFillToLsv(objVO[i]);
            }
        }
        #endregion 选择单据

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        public void m_mthDeleData()
        {
            string strID;
            long lngRes = 0;
            if (isdele_int == 1)
            {
                if (this.m_objViewer.lsvPrice.SelectedItems.Count <= 0)
                    return;
                strID = ((clsPriceChgeAppl)this.m_objViewer.lsvPrice.SelectedItems[0].Tag).m_strMEDICINEPRICECHGAPPLID_CHR;
                lngRes = this.objSVC.m_lngDeleAppl(strID);
                if (lngRes == 1)
                {
                    this.m_objViewer.lsvPrice.SelectedItems[0].Remove();
                    publicClass.m_mthShowWarning(this.m_objViewer.panel4, "删除成功!!");
                    m_mthGreatNew();
                }
                else
                {
                    publicClass.m_mthShowWarning(this.m_objViewer.panel4, "删除失败!!");
                }

            }
            else
            {
                if (this.m_objViewer.lisvChangPriceDe.SelectedItems.Count <= 0)
                    return;
                strID = ((clsPriceChgeApplDe)this.m_objViewer.lisvChangPriceDe.SelectedItems[0].Tag).m_strMEDICINEPRICECHGAPPLDEID_CHR;
                lngRes = this.objSVC.m_lngDeleteDeById(strID);
                if (lngRes == 1)
                {
                    this.m_objViewer.lisvChangPriceDe.SelectedItems[0].Remove();
                    publicClass.m_mthShowWarning(this.m_objViewer.panel4, "删除成功!!");
                    m_mthClearText();
                }
                else
                {
                    publicClass.m_mthShowWarning(this.m_objViewer.panel4, "删除失败!!");
                }

            }

        }

        #endregion

        #region 填充单据数据
        public void m_fillToTextBox(clsPriceChgeAppl BillVO)
        {
            this.m_objViewer.panel4.Tag = BillVO.m_strMEDICINEPRICECHGAPPLID_CHR;
            this.m_objViewer.m_txtBillNo.Text = BillVO.m_strMEDICINEPRICECHGAPPLNO_CHR.Substring(0, 8);
            this.m_objViewer.txtDocEnd.Text = BillVO.m_strMEDICINEPRICECHGAPPLNO_CHR.Substring(8);
            this.m_objViewer.dateTime.Value = DateTime.Parse(BillVO.m_strCREATEDATE_DAT);
            this.m_objViewer.m_txtMemo.Text = BillVO.m_strMEMO_VCHR;
            this.m_objViewer.panel4.Tag = BillVO.m_strMEDICINEPRICECHGAPPLID_CHR;
        }
        #endregion

        #region 把单据填充到Lisview列表(未审核）
        /// <summary>
        /// 把单据填充到Lisview列表(未审核）
        /// </summary>
        /// <param name="objvo"></param>
        private void m_lngNewAppl(clsPriceChgeAppl objvo)
        {
            ListViewItem LisTemp = new ListViewItem(objvo.m_strMEDICINEPRICECHGAPPLNO_CHR);
            LisTemp.SubItems.Add(objvo.m_strCREATORNAME_CHR);
            LisTemp.SubItems.Add(objvo.m_strCREATEDATE_DAT);
            LisTemp.Tag = objvo;
            LisTemp.Selected = true;
            this.m_objViewer.lsvPrice.Items.Add(LisTemp);
        }
        #endregion

        #region 把单据填充到Lisview列表(己审核）
        /// <summary>
        /// 把单据填充到Lisview列表(己审核）
        /// </summary>
        /// <param name="objvo"></param>
        private void m_lngFillLsvOkAppl(clsPriceChgeAppl objvo)
        {
            ListViewItem LisTemp = new ListViewItem(objvo.m_strMEDICINEPRICECHGAPPLNO_CHR);
            LisTemp.SubItems.Add(objvo.m_strADUITEMPNAME_CHR);
            LisTemp.SubItems.Add(objvo.m_strADUITDATE_DAT);
            LisTemp.Tag = objvo;
            this.m_objViewer.livPriceOk.Items.Add(LisTemp);
        }
        #endregion

        #region 删除数据事件
        /// <summary>
        /// 删除数据事件
        /// </summary>
        public void m_lngDelEvent()
        {
            //			if(dsDelDate == null)
            //			{
            //				dsDelDate = dsChangePrice.Clone();
            //			}
            //			if(this.m_objViewer.lsvPrice.Items.Count > 0)
            //			{
            //				if(this.m_objViewer.lsvPrice.SelectedItems.Count > 0)
            //				{
            //					try
            //					{
            //						DataRow dtRow=(DataRow)this.m_objViewer.lsvPrice.SelectedItems[0].Tag;
            //						string ChangPriceID=dtRow["MEDICINEPRICECHGAPPLID_CHR"].ToString();
            //						long lngRes=objSVC.m_mthDele(ChangPriceID);
            //						if(lngRes==1)
            //						{
            //							dsDelDate.Tables["t_opr_medicinepricechgappl"].ImportRow((DataRow)this.m_objViewer.lsvPrice.SelectedItems[0].Tag);
            //							((DataRow)this.m_objViewer.lsvPrice.SelectedItems[0].Tag).Delete();
            //							this.m_objViewer.lsvPrice.Items.RemoveAt(this.m_objViewer.lsvPrice.SelectedItems[0].Index);
            //						}
            //					}
            //					catch
            //					{
            //					}
            //				}
            //			}
        }


        #endregion

        #region 删除明细
        public void DelDetial()
        {
            //			if(dsDelDate == null)
            //			{
            //				dsDelDate = dsChangePrice.Clone();
            //			}
            //			if(this.m_objViewer.lisvChangPriceDe.Items.Count > 0)
            //			{
            //				if(this.m_objViewer.lisvChangPriceDe.SelectedItems.Count > 0)
            //				{
            //					dsDelDate.Tables["t_opr_medicinepricechgapplde"].ImportRow((DataRow)this.m_objViewer.lisvChangPriceDe.SelectedItems[0].Tag);
            //					((DataRow)this.m_objViewer.lisvChangPriceDe.SelectedItems[0].Tag).Delete();
            //					this.m_objViewer.lisvChangPriceDe.Items.RemoveAt(this.m_objViewer.lisvChangPriceDe.SelectedItems[0].Index);
            //					this.m_objViewer.lsvPrice.Items.Clear();
            //				}
            //			}
        }
        #endregion 删除明细

        #region 填充调价单VO
        /// <summary>
        /// 填充调价单VO
        /// </summary>
        /// <returns></returns>
        private clsPriceChgeAppl m_mthFillVo()
        {
            clsPriceChgeAppl chgeVo = new clsPriceChgeAppl();
            chgeVo.m_intPSTATUS_INT = 1;
            if (this.m_objViewer.panel4.Tag != null)
                chgeVo.m_strMEDICINEPRICECHGAPPLID_CHR = (string)this.m_objViewer.panel4.Tag;
            chgeVo.m_strCREATORID_CHR = this.m_objViewer.LoginInfo.m_strEmpID;
            chgeVo.m_strMEDICINEPRICECHGAPPLNO_CHR = this.m_objViewer.m_txtBillNo.Text + this.m_objViewer.txtDocEnd.Text;
            chgeVo.m_strCREATORNAME_CHR = this.m_objViewer.LoginInfo.m_strEmpName;
            chgeVo.m_strCREATEDATE_DAT = this.m_objViewer.dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            chgeVo.m_strMEMO_VCHR = this.m_objViewer.m_txtMemo.Text;
            chgeVo.m_strPERIODID_CHR = objItems[this.m_objViewer.comPriod.SelectedIndex].m_strPeriodID;
            chgeVo.m_dblODDS_MNY = double.Parse(this.m_objViewer.label8.Text);
            return chgeVo;
        }
        #endregion

        #region 获取所有的申请单明细
        /// <summary>
        /// 获取所有的申请单明细
        /// </summary>
        /// <returns></returns>
        private clsPriceChgeApplDe[] m_getChgeDeVO()
        {
            clsPriceChgeApplDe[] ApplDe = new clsPriceChgeApplDe[this.m_objViewer.lisvChangPriceDe.Items.Count];
            if (this.m_objViewer.lisvChangPriceDe.Items.Count > 0)
            {
                for (int i1 = 0; i1 < this.m_objViewer.lisvChangPriceDe.Items.Count; i1++)
                {
                    ApplDe[i1] = new clsPriceChgeApplDe();
                    ApplDe[i1] = (clsPriceChgeApplDe)this.m_objViewer.lisvChangPriceDe.Items[i1].Tag;
                }
            }
            return ApplDe;
        }
        #endregion


        #region 合并单据
        /// <summary>
        /// 合并单据
        /// </summary>
        public void m_mthUnite()
        {
            List<string> saveApplID = new List<string>();
            List<string> saveApplNO = new List<string>();
            string strPriod = objItems[this.m_objViewer.comPriod.SelectedIndex].m_strPeriodID;
            string strEmpId = this.m_objViewer.LoginInfo.m_strEmpID;
            string strBillNo = objSVC.m_mthGetMaxNo(this.m_objViewer.dateTime.Value.Year.ToString() + this.m_objViewer.dateTime.Value.Month.ToString("00") + this.m_objViewer.dateTime.Value.Day.ToString("00"));
            string NewNO = clsPublicParm.m_mthGetNewDocument(strBillNo, "3", 0);
            if (this.m_objViewer.lsvPrice.Items.Count > 0)
            {
                for (int i1 = 0; i1 < this.m_objViewer.lsvPrice.Items.Count; i1++)
                {
                    if (this.m_objViewer.lsvPrice.Items[i1].Checked == true)
                    {
                        clsPriceChgeAppl objAppl = (clsPriceChgeAppl)this.m_objViewer.lsvPrice.Items[i1].Tag;
                        saveApplID.Add(objAppl.m_strMEDICINEPRICECHGAPPLID_CHR);
                        saveApplNO.Add(objAppl.m_strMEDICINEPRICECHGAPPLNO_CHR);
                    }
                }
            }
            else
            {
                publicClass.m_mthShowWarning(this.m_objViewer.lsvPrice, "当前没有可以合并的数据!");
                return;
            }
            if (saveApplID.Count > 1)
            {
                clsPriceChgeAppl objApplOut;
                long lngRes = objSVC.m_lngUniteChangPriceData(saveApplID, saveApplNO, strPriod, strEmpId, NewNO, out objApplOut);
                if (lngRes == 1)
                {
                    publicClass.m_mthShowWarning(this.m_objViewer.lsvPrice, "合并单据成功!");
                    int intCount = this.m_objViewer.lsvPrice.Items.Count;
                    for (int i1 = 0; i1 < intCount; i1++)
                    {
                        if (this.m_objViewer.lsvPrice.Items[i1].Checked == true)
                        {
                            this.m_objViewer.lsvPrice.Items[i1].Remove();
                            i1--;
                            intCount--;
                        }
                    }
                    objApplOut.m_strCREATORNAME_CHR = this.m_objViewer.LoginInfo.m_strEmpName;
                    m_lngNewAppl(objApplOut);
                    m_mthGreatNew();
                }
            }
            else
            {
                publicClass.m_mthShowWarning(this.m_objViewer.lsvPrice, "请先选择你要合并的调价单!");
            }

        }
        #endregion

        #region 保存调价信息
        /// <summary>
        /// 保存调价信息
        /// </summary>
        public void m_lngSave()
        {
            clsPriceChgeAppl objChgeApplVO = m_mthFillVo();

            string strBillID = "";
            if (isNew == true)
            {
                clsPriceChgeApplDe[] objChgeApplDeVO = m_getChgeDeVO();
                if (objChgeApplDeVO.Length == 0)
                {
                    publicClass.m_mthShowWarning(this.m_objViewer.lisvChangPriceDe, "没有明细数据!");
                    return;
                }
                long returnValuse = objSVC.m_lngSaveChangPriceData(objChgeApplVO, objChgeApplDeVO, out strBillID);
                if (returnValuse < 1)
                {
                    if (returnValuse == -2)
                    {
                        publicClass.m_mthShowWarning(this.m_objViewer.panel6, "申请单据号重复！");
                        this.m_objViewer.txtDocEnd.Focus();
                        return;
                    }
                    publicClass.m_mthShowWarning(this.m_objViewer.panel6, "保存失败!");
                }
                else
                {
                    objChgeApplVO.m_strMEDICINEPRICECHGAPPLID_CHR = strBillID;
                    m_lngNewAppl(objChgeApplVO);
                    m_mthGreatNew();
                    publicClass.m_mthShowWarning(this.m_objViewer.panel6, "保存单据成功!");
                }
            }
            else
            {

                if (isModifyDe == 1)
                {
                    clsPriceChgeApplDe ApplDe = new clsPriceChgeApplDe();
                    ApplDe.m_strMEDICINEPRICECHGAPPLDEID_CHR = (string)this.m_objViewer.panel6.Tag;
                    ApplDe.m_strMEDICINEID_CHR = (string)this.m_objViewer.txtMEDICINE.Tag;
                    ApplDe.m_strMEDICINENAME_CHR = this.m_objViewer.txtMEDICINE.Text;
                    ApplDe.m_strMEDSPEC_VCHR = this.m_objViewer.txtMEDSPEC.Text;
                    ApplDe.m_strASSISTCODE_CHR = (string)this.m_objViewer.txtMEDSPEC.Tag;
                    ApplDe.m_strUNITID_CHR = this.m_objViewer.m_txtUNIT.Text.Trim();
                    if (this.m_objViewer.m_txtPRICE.Text.Trim() != "")
                        ApplDe.m_dblCURPRICE_MNY = double.Parse(this.m_objViewer.m_txtPRICE.Text.Trim());
                    else
                        ApplDe.m_dblCURPRICE_MNY = 0;
                    if (this.m_objViewer.TXTCHANGEPRICE.Text.Trim() != "")
                        ApplDe.m_dblCHANGEPRICE_MNY = double.Parse(this.m_objViewer.TXTCHANGEPRICE.Text.Trim());
                    else
                        ApplDe.m_dblCHANGEPRICE_MNY = 0;
                    if (this.m_objViewer.txtAllAmount.Text.Trim() != "")
                        ApplDe.m_dblQTY_DEC = double.Parse(this.m_objViewer.txtAllAmount.Text.Trim());
                    else
                        ApplDe.m_dblQTY_DEC = 0;
                    ApplDe.m_strtypeid = this.m_objViewer.m_cmbType.SelectedValue.ToString();
                    ApplDe.m_strtypeName = this.m_objViewer.m_cmbType.Text.ToString().Trim();
                    long lngRes = objSVC.m_lngMondifiy(ApplDe, objChgeApplVO);
                    if (lngRes == 1)
                    {
                        this.m_objViewer.lsvPrice.SelectedItems[0].SubItems[0].Text = objChgeApplVO.m_strMEDICINEPRICECHGAPPLNO_CHR;
                        this.m_objViewer.lsvPrice.SelectedItems[0].Tag = objChgeApplVO;
                        m_mthmodifyDe(ApplDe);
                        m_mthClearText();
                        publicClass.m_mthShowWarning(this.m_objViewer.panel6, "修改单据成功!");
                    }
                }
                else
                {
                    this.m_objViewer.lsvPrice.SelectedItems[0].SubItems[0].Text = objChgeApplVO.m_strMEDICINEPRICECHGAPPLNO_CHR;
                    this.m_objViewer.lsvPrice.SelectedItems[0].Tag = objChgeApplVO;
                    objSVC.m_lngMondifiy(null, objChgeApplVO);
                    m_mthClearText();
                    publicClass.m_mthShowWarning(this.m_objViewer.panel6, "修改单据成功!");
                }
            }
            this.m_objViewer.dateTime.Focus();
        }
        #endregion 保存调价信息
        #region 修改单据明细
        /// <summary>
        /// 修改单据明细
        /// </summary>
        /// <param name="objVO"></param>
        private void m_mthmodifyDe(clsPriceChgeApplDe objVO)
        {
            this.m_objViewer.lisvChangPriceDe.SelectedItems[0].SubItems[1].Text = objVO.m_strASSISTCODE_CHR;
            this.m_objViewer.lisvChangPriceDe.SelectedItems[0].SubItems[2].Text = objVO.m_strMEDICINENAME_CHR;
            this.m_objViewer.lisvChangPriceDe.SelectedItems[0].SubItems[3].Text = objVO.m_strMEDSPEC_VCHR;
            this.m_objViewer.lisvChangPriceDe.SelectedItems[0].SubItems[4].Text = objVO.m_strUNITID_CHR;
            this.m_objViewer.lisvChangPriceDe.SelectedItems[0].SubItems[5].Text = objVO.m_dblQTY_DEC.ToString();
            this.m_objViewer.lisvChangPriceDe.SelectedItems[0].SubItems[6].Text = objVO.m_dblCURPRICE_MNY.ToString();
            this.m_objViewer.lisvChangPriceDe.SelectedItems[0].SubItems[7].Text = objVO.m_dblCHANGEPRICE_MNY.ToString();
            this.m_objViewer.lisvChangPriceDe.SelectedItems[0].SubItems[8].Text = objVO.m_dblODDSDE_MNY.ToString();
            this.m_objViewer.lisvChangPriceDe.SelectedItems[0].SubItems[9].Text = objVO.m_strtypeName;
            this.m_objViewer.lisvChangPriceDe.SelectedItems[0].Tag = objVO;
        }

        #endregion

        #region 审核调价单
        /// <summary>
        /// 审核调价单
        /// </summary>
        public void m_lngConfirm()
        {
            if (this.m_objViewer.lsvPrice.SelectedItems.Count == 0)
            {
                publicClass.m_mthShowWarning(this.m_objViewer.panel6, "请先选择数据!");
                return;
            }
            clsPriceChgeAppl objChgePricelVO = (clsPriceChgeAppl)this.m_objViewer.lsvPrice.SelectedItems[0].Tag;
            objChgePricelVO.m_strADUITEMP_CHR = this.m_objViewer.LoginInfo.m_strEmpID;
            clsPriceChgeApplDe[] objChgePriceDeVO = m_getChgeDeVO();
            if (objSVC.m_lngTrialChang(objChgePricelVO, objChgePriceDeVO) < 0)
            {
                publicClass.m_mthShowWarning(this.m_objViewer.panel6, "审核失败!");
            }
            else
            {
                publicClass.m_mthShowWarning(this.m_objViewer.panel6, "审核成功!");
                objChgePricelVO.m_intPSTATUS_INT = 2;
                objChgePricelVO.m_strADUITEMPNAME_CHR = this.m_objViewer.LoginInfo.m_strEmpName;
                ;
                objChgePricelVO.m_strADUITDATE_DAT = DateTime.Now.ToString("yyyy-MM-dd HH24:mi:ss");
                this.m_objViewer.lsvPrice.SelectedItems[0].Remove();
                this.m_objViewer.lisvChangPriceDe.Items.Clear();
                m_lngFillLsvOkAppl(objChgePricelVO);
                m_mthGreatNew();
            }
            m_mthGreatNew();
        }
        #endregion

        #region 查找模块
        public void m_lngFindData()
        {
            //			string findGrearNAME=this.m_objViewer.GrearNAME.Text.Trim();
            //			string findAPPLDATE=this.m_objViewer.APPLDATE.Text.Trim();
            //			string findADUITDATE=this.m_objViewer.ADUITDATE.Text.Trim();
            //			string findADUITEMP=this.m_objViewer.ADUITEMP.Text.Trim();
            //		    string findTextID=this.m_objViewer.TextID.Text.Trim();
            //			int tolNuber=0;
            //			if(findTextID!=""||findGrearNAME!=""||findAPPLDATE!=""||findADUITDATE!=""||findADUITEMP!="")
            //			{
            //				this.m_objViewer.lsvPrice.Items.Clear();
            //				this.m_objViewer.livPriceOk.Items.Clear();
            //				this.m_objViewer.lisvChangPriceDe.Items.Clear();
            //				objFindChgeApplAll=new clsPriceChgeAppl[objPriceChgeApplAll.Length];
            //
            //				if(objPriceChgeApplAll.Length>0&&findTextID!="")
            //				{
            //					for(int i1=0;i1<objPriceChgeApplAll.Length;i1++)
            //					{
            //						if(objPriceChgeApplAll[i1].m_strMEDICINEPRICECHGAPPLID_CHR.IndexOf(findTextID,0)==0)
            //						{
            //							objFindChgeApplAll[tolNuber]=new clsPriceChgeAppl();
            //							objFindChgeApplAll[tolNuber]=objPriceChgeApplAll[i1];
            //							tolNuber++;
            //						}
            //					}
            //				}
            //				if(objPriceChgeApplAll.Length>0&&findAPPLDATE!="")
            //				{
            //					for(int i1=0;i1<objPriceChgeApplAll.Length;i1++)
            //					{
            //						if(objPriceChgeApplAll[i1].m_strAPPLDATE_DAT.IndexOf(findAPPLDATE,0)==0)
            //						{
            //							objFindChgeApplAll[tolNuber]=new clsPriceChgeAppl();
            //							objFindChgeApplAll[tolNuber]=objPriceChgeApplAll[i1];
            //							tolNuber++;
            //						}
            //					}
            //				}
            //				if(objPriceChgeApplAll.Length>0&&findGrearNAME!="")
            //				{
            //					for(int i1=0;i1<objPriceChgeApplAll.Length;i1++)
            //					{
            //						if(objPriceChgeApplAll[i1].m_strCREATORNAME_CHR.IndexOf(findGrearNAME,0)==0)
            //						{
            //							objFindChgeApplAll[tolNuber]=new clsPriceChgeAppl();
            //							objFindChgeApplAll[tolNuber]=objPriceChgeApplAll[i1];
            //							tolNuber++;
            //						}
            //					}
            //				}
            //
            //				if(objPriceChgeApplAll.Length>0&&findADUITEMP!="")
            //				{
            //					for(int i1=0;i1<objPriceChgeApplAll.Length;i1++)
            //					{
            //						if(objPriceChgeApplAll[i1].m_strADUITDATE_DAT.IndexOf(findADUITEMP,0)==0)
            //						{
            //							objFindChgeApplAll[tolNuber]=new clsPriceChgeAppl();
            //							objFindChgeApplAll[tolNuber]=objPriceChgeApplAll[i1];
            //							tolNuber++;
            //						}
            //					}
            //				}
            //				if(tolNuber!=0)
            //				{
            ////					for(int i1=0;i1<tolNuber;i1++)
            ////					{
            ////						if(objFindChgeApplAll[i1].m_intPSTATUS_INT==1)
            ////						//	m_lngFillLsvAppl(objFindChgeApplAll[i1]);
            ////						else
            ////							m_lngFillLsvOkAppl(objFindChgeApplAll[i1]);
            ////					}
            //				}
            //				m_lngClearFind();
            //	
            //			}
            //			else
            //			{
            //				MessageBox.Show("请输入查找数据","系统提示");
            //			}

        }
        #endregion

        #region 清空查找输入框
        private void m_lngClearFind()
        {
            //			this.m_objViewer.GrearNAME.Text="";
            //			this.m_objViewer.APPLDATE.Text="";
            //			this.m_objViewer.ADUITDATE.Text="";
            //			this.m_objViewer.ADUITEMP.Text="";
            //			this.m_objViewer.TextID.Text="";
        }
        #endregion

        #region 创建提示列表和选择操作
        public void CreatePharmList(System.Windows.Forms.Control obj)
        {
            if (dtbMedicine == null)
            {
                return;
            }
            if (m_lv == null)
            {
                m_lv = new ListView();
                m_lv.View = View.Details;
                m_lv.FullRowSelect = true;
                m_lv.MultiSelect = false;
                m_lv.HideSelection = false;
                m_lv.Columns.Add("助记码", 60, HorizontalAlignment.Left);
                m_lv.Columns.Add("药品名称", 160, HorizontalAlignment.Left);
                m_lv.Columns.Add("药品规格", 160, HorizontalAlignment.Left);
                m_lv.Columns.Add("单位", 40, HorizontalAlignment.Left);
                m_lv.Columns.Add("数量", 60, HorizontalAlignment.Left);
                m_lv.Columns.Add("单价", 40, HorizontalAlignment.Left);
                m_lv.Columns.Add("拼音", 50, HorizontalAlignment.Left);
                m_lv.Columns.Add("五笔", 50, HorizontalAlignment.Left);
                m_lv.Width = 600;
                m_lv.Height = 400;
                m_lv.GridLines = true;
                System.Windows.Forms.Control para = obj;
                int nLeft = obj.Left - 20;
                int nTop = obj.Top + obj.Height + 25;
                while (para.Parent != null && !para.Equals(obj.FindForm()))
                {
                    para = para.Parent;
                    nLeft += para.Left;
                    nTop += para.Top;
                }
                m_lv.Left = nLeft;
                m_lv.Top = nTop;
                para.Controls.Add(m_lv);
                para.Controls.SetChildIndex(m_lv, 0);
                m_lv.KeyDown += new KeyEventHandler(m_lv_KeyDown);
                m_lv.DoubleClick += new EventHandler(m_lv_DoubleClick);
                m_lv.Leave += new EventHandler(m_lv_Leave);
                obj.KeyDown += new KeyEventHandler(obj_KeyDown);
                obj.Leave += new EventHandler(obj_Leave);
            }
            if (obj.Text == "")
            {
                m_lv.Visible = false;
                return;
            }
            m_lv.Items.Clear();
            DataRow[] drs = null;

            for (int i = 0; i < drs.Length && i < 100; i++)
            {
                ListViewItem item = new ListViewItem(drs[i]["ASSISTCODE_CHR"].ToString().Trim());
                item.SubItems.Add(drs[i]["MEDICINENAME_VCHR"].ToString().Trim());
                item.SubItems.Add(drs[i]["MEDSPEC_VCHR"].ToString().Trim());
                item.SubItems.Add(drs[i]["OPUNIT_CHR"].ToString().Trim());
                item.SubItems.Add(drs[i]["amount_dec"].ToString().Trim());
                item.SubItems.Add(drs[i]["UNITPRICE_MNY"].ToString().Trim());
                item.SubItems.Add(drs[i]["PYCODE_CHR"].ToString().Trim());
                item.SubItems.Add(drs[i]["WBCODE_CHR"].ToString().Trim());
                item.Tag = drs[i];
                m_lv.Items.Add(item);
            }
            if (drs.Length > 0)
            {
                m_lv.Items[0].Selected = true;
            }
            m_lv.Visible = true;
        }
        private void m_lv_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_lv == null)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (m_lv.Items.Count > 0)
                {
                    this.m_objViewer.txtMEDICINE.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["MEDICINENAME_VCHR"].ToString().Trim();
                    this.m_objViewer.txtMEDICINE.Tag = ((DataRow)m_lv.SelectedItems[0].Tag)["MEDICINEID_CHR"].ToString().Trim();
                    this.m_objViewer.txtMEDSPEC.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["MEDSPEC_VCHR"].ToString().Trim();
                    this.m_objViewer.txtMEDSPEC.Tag = ((DataRow)m_lv.SelectedItems[0].Tag)["ASSISTCODE_CHR"].ToString().Trim();
                    this.m_objViewer.m_txtPRICE.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["UNITPRICE_MNY"].ToString().Trim();
                    this.m_objViewer.m_txtUNIT.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["OPUNIT_CHR"].ToString().Trim();
                    this.m_objViewer.txtAllAmount.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["amount_dec"].ToString().Trim();
                }
                this.m_objViewer.TXTCHANGEPRICE.Focus();
                this.m_objViewer.TXTCHANGEPRICE.SelectAll();
                m_lv.Visible = false;

            }
        }

        private void m_lv_DoubleClick(object sender, EventArgs e)
        {
            if (m_lv == null)
            {
                return;
            }
            if (m_lv.Items.Count > 0)
            {
                this.m_objViewer.txtMEDICINE.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["MEDICINENAME_VCHR"].ToString().Trim();
                this.m_objViewer.txtMEDICINE.Tag = ((DataRow)m_lv.SelectedItems[0].Tag)["MEDICINEID_CHR"].ToString().Trim();
                this.m_objViewer.txtMEDSPEC.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["MEDSPEC_VCHR"].ToString().Trim();
                this.m_objViewer.txtMEDSPEC.Tag = ((DataRow)m_lv.SelectedItems[0].Tag)["ASSISTCODE_CHR"].ToString().Trim();
                this.m_objViewer.m_txtPRICE.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["UNITPRICE_MNY"].ToString().Trim();
                this.m_objViewer.m_txtUNIT.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["OPUNIT_CHR"].ToString().Trim();
                this.m_objViewer.txtAllAmount.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["amount_dec"].ToString().Trim();
            }
            this.m_objViewer.TXTCHANGEPRICE.Focus();
            this.m_objViewer.TXTCHANGEPRICE.SelectAll();
            m_lv.Visible = false;
        }
        private void obj_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_lv == null)
            {
                return;
            }
            if (m_lv.Items.Count > 0)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        if (m_lv.SelectedItems[0].Index > 0)
                        {
                            m_lv.Items[m_lv.SelectedItems[0].Index - 1].Selected = true;
                        }
                        else
                        {
                            m_lv.Items[m_lv.Items.Count - 1].Selected = true;
                        }
                        break;
                    case Keys.Down:
                        if (m_lv.SelectedItems[0].Index < m_lv.Items.Count - 1)
                        {
                            m_lv.Items[m_lv.SelectedItems[0].Index + 1].Selected = true;
                        }
                        else
                        {
                            m_lv.Items[0].Selected = true;
                        }
                        break;
                    case Keys.Enter:
                        if (m_lv.Visible == false)
                        {
                            return;
                        }
                        if (m_lv.Items.Count > 0)
                        {
                            this.m_objViewer.txtMEDICINE.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["MEDICINENAME_VCHR"].ToString().Trim();
                            this.m_objViewer.txtMEDICINE.Tag = ((DataRow)m_lv.SelectedItems[0].Tag)["MEDICINEID_CHR"].ToString().Trim();
                            this.m_objViewer.txtMEDSPEC.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["MEDSPEC_VCHR"].ToString().Trim();
                            this.m_objViewer.txtMEDSPEC.Tag = ((DataRow)m_lv.SelectedItems[0].Tag)["ASSISTCODE_CHR"].ToString().Trim();
                            this.m_objViewer.m_txtPRICE.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["UNITPRICE_MNY"].ToString().Trim();
                            this.m_objViewer.m_txtUNIT.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["OPUNIT_CHR"].ToString().Trim();
                            this.m_objViewer.txtAllAmount.Text = ((DataRow)m_lv.SelectedItems[0].Tag)["amount_dec"].ToString().Trim();
                        }
                        this.m_objViewer.TXTCHANGEPRICE.Focus();
                        this.m_objViewer.TXTCHANGEPRICE.SelectAll();
                        m_lv.Visible = false;

                        break;
                }
            }
        }
        private void m_lv_Leave(object sender, EventArgs e)
        {
            if (this.m_objViewer.txtMEDICINE.Focused)
            {
                return;
            }
            m_lv.Visible = false;

        }

        private void obj_Leave(object sender, EventArgs e)
        {
            if (m_lv.FocusedItem != null)
            {
                return;
            }
            m_lv.Visible = false;
        }
        #endregion 创建提示列表


        private void clsControlChangPrice_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            this.m_objViewer.dntEmp.Enabled = false;
        }
        #region 打印
        public void m_mthPrintChangePrice()
        {
            //CrystalDecisions.Windows.Forms.CrystalReportViewer view = CreateViewer();
            //if (view == null)
            //{
            //    MessageBox.Show("没有什么可以预览");
            //    return;
            //}
            //System.Windows.Forms.Form frm = new Form();
            //frm.Height = 400;

            //view.Location = new System.Drawing.Point(0, 0);
            //frm.Width = 800;
            //frm.Height = 600;
            //view.Width = frm.Width;
            //view.Height = frm.Height;
            //view.DisplayGroupTree = false;

            //frm.Text = "打印预览";
            //view.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            //    | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
            //frm.Controls.Add(view);
            //frm.ShowDialog();
        }
        //private CrystalDecisions.Windows.Forms.CrystalReportViewer CreateViewer()
        //{
        //    if (this.m_objViewer.lisvChangPriceDe.Items.Count < 1)
        //    {
        //        return null;
        //    }
        //    DataTable dtDe = new DataTable();
        //    dtDe.Columns.Add("MEDICINEID_CHR");
        //    dtDe.Columns.Add("medicinepricechgappldeid_chr");
        //    dtDe.Columns.Add("ROWNO_CHR");
        //    dtDe.Columns.Add("medicineid_chr");
        //    dtDe.Columns.Add("unitid_chr");
        //    dtDe.Columns.Add("curprice_mny");
        //    dtDe.Columns.Add("changeprice_mny");
        //    dtDe.Columns.Add("medicinepricechgapplid_chr");
        //    dtDe.Columns.Add("assistcode_chr");
        //    dtDe.Columns.Add("typeid_chr");
        //    dtDe.Columns.Add("typename_chr");
        //    dtDe.Columns.Add("MEDICINENAME_VCHR");
        //    dtDe.Columns.Add("MEDSPEC_VCHR");
        //    dtDe.Columns.Add("AMOUNT_DEC");
        //    dtDe.Columns.Add("Balance", Type.GetType("System.Double"));

        //    for (int i1 = 0; i1 < this.m_objViewer.lisvChangPriceDe.Items.Count; i1++)
        //    {
        //        clsPriceChgeApplDe ArrDe = (clsPriceChgeApplDe)this.m_objViewer.lisvChangPriceDe.Items[i1].Tag;
        //        DataRow dtRow = dtDe.NewRow();
        //        dtRow["changeprice_mny"] = ArrDe.m_dblCHANGEPRICE_MNY;
        //        dtRow["curprice_mny"] = ArrDe.m_dblCURPRICE_MNY;
        //        dtRow["AMOUNT_DEC"] = ArrDe.m_dblQTY_DEC;
        //        dtRow["assistcode_chr"] = ArrDe.m_strASSISTCODE_CHR;
        //        dtRow["MEDICINENAME_VCHR"] = ArrDe.m_strMEDICINENAME_CHR;
        //        dtRow["MEDSPEC_VCHR"] = ArrDe.m_strMEDSPEC_VCHR;
        //        dtRow["typename_chr"] = ArrDe.m_strtypeName;
        //        dtRow["Balance"] = ArrDe.m_dblODDSDE_MNY;
        //        dtDe.Rows.Add(dtRow);
        //    }

        //    string BillNO = this.m_objViewer.m_txtBillNo.Text;
        //    string AppDate = DateTime.Now.ToString();
        //    com.digitalwave.iCare.gui.HIS.baotable.ChangePriceRpt rpt = new com.digitalwave.iCare.gui.HIS.baotable.ChangePriceRpt();
        //    ((TextObject)rpt.ReportDefinition.ReportObjects["TextAppDate"]).Text = AppDate;
        //    rpt.SetDataSource(dtDe);
        //    CrystalDecisions.Windows.Forms.CrystalReportViewer viewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
        //    rpt.Refresh();
        //    viewer.ReportSource = rpt;
        //    return viewer;
        //}
        #endregion 打印

        #region 生成单号
        /// <summary>
        /// 生成单号
        /// </summary>
        public void m_mthCreatBillNo()
        {
            if (isNew == true)
            {
                string strBillNo = objSVC.m_mthGetMaxNo(this.m_objViewer.dateTime.Value.ToString("yyyyMMdd"));
                string strDoc = clsPublicParm.m_mthGetNewDocument(strBillNo, "", 0, this.m_objViewer.dateTime.Value.ToString("yyyyMMdd"), "");
                this.m_objViewer.m_txtBillNo.Text = strDoc.Substring(0, 8);
                this.m_objViewer.txtDocEnd.Text = strDoc.Substring(8);
            }
        }
        #endregion

        #region 新建
        public void m_mthGreatNew()
        {
            isNew = true;
            AddArr.Clear();
            this.m_objViewer.panel4.Tag = null;
            //com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
            this.m_objViewer.dateTime.Value = (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate();
            this.m_objViewer.label8.Text = "0";
            this.m_objViewer.m_txtMemo.Text = "";
            m_mthClearText();
            this.m_objViewer.lisvChangPriceDe.Items.Clear();
            m_mthCreatBillNo();
            this.m_objViewer.dateTime.Focus();
            this.m_objViewer.btnSave.Text = "保存(&S)";
        }
        #endregion
        public void m_mthClearText()
        {
            this.m_objViewer.panel6.Tag = null;
            this.m_objViewer.txtMEDICINE.Clear();
            this.m_objViewer.txtMEDSPEC.Text = "";
            this.m_objViewer.TXTCHANGEPRICE.Text = "";
            this.m_objViewer.m_txtPRICE.Text = "";
            this.m_objViewer.txtAllAmount.Text = "";
            this.m_objViewer.m_txtUNIT.Text = "";
            this.m_objViewer.txtoddsDe.Text = "";
            this.m_objViewer.m_cmbType.Text = "";
            this.m_objViewer.btnAdd.Enabled = true;
        }
    }
}
