using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 结算DOMIAN类
    /// </summary>
    public class clsDcl_Charge : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public clsDcl_Charge()
        {
        }

        weCare.Proxy.ProxyIP01 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyIP01();
            }
        }
        #endregion

        #region 根据员工ID获取隶属组信息
        /// <summary>
        /// 根据员工ID获取隶属组信息
        /// </summary>
        /// <param name="EmpID">员工ID</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetGroupEmp(string EmpID, out DataTable dt)
        {
            return (new weCare.Proxy.ProxyBase()).Service.m_lngGetGroupEmp(EmpID, out dt);
        }
        #endregion

        #region 根据分类范围获取费用分类(门诊核算、发票；住院核算、发票)定义信息
        /// <summary>
        /// 根据分类范围获取费用分类(门诊核算、发票；住院核算、发票)定义信息
        /// </summary>
        /// <param name="Scope">范围: 1 门诊核算 2 门诊发票 3 住院核算 4 住院发票</param>
        /// <param name="Status">% 全部 0 停用 1 启用</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetDefChargeCat(string Scope, string Status, out DataTable dt)
        {
            return proxy.Service.m_lngGetDefChargeCat(Scope, Status, out dt);
        }
        #endregion

        #region 获取身份(费别)信息
        /// <summary>
        /// 获取身份(费别)信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPayTypeInfo(out DataTable dt)
        {
            return proxy.Service.m_lngGetPayTypeInfo(out dt);
        }
        #endregion

        #region 获取员工代码表
        /// <summary>
        /// 获取员工代码表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetEmployee(out DataTable dt)
        {
            return proxy.Service.m_lngGetEmployee(out dt);
        }
        /// <summary>
        /// 获取员工代码表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="empTypeId"></param>
        /// <returns></returns>
        public long m_lngGetEmployee(out DataTable dt, int empTypeId)
        {
            return proxy.Service.m_lngGetEmployee(out dt, empTypeId);
        }
        #endregion

        #region 根据员工工号获取ID和姓名
        /// <summary>
        /// 根据员工工号获取ID和姓名
        /// </summary>
        /// <param name="EmpCode"></param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <returns></returns>
        public long m_lngGetEmployee(string EmpCode, out string EmpID, out string EmpName)
        {
            return proxy.Service.m_lngGetEmployee(EmpCode, out EmpID, out EmpName);
        }
        #endregion

        #region 根据用户ID获取所属角色列表
        /// <summary>
        /// 根据用户ID获取所属角色列表
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetEmpRole(string EmpID, out DataTable dt)
        {
            return proxy.Service.m_lngGetEmpRole(EmpID, out dt);
        }
        #endregion

        #region 获得病区信息
        /// <summary>
        /// 获得病区信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Flag">1 科室 2 病区</param>
        /// <returns></returns>
        public long m_lngGetDeptArea(out DataTable dt, int Flag)
        {
            return proxy.Service.m_lngGetDeptArea(out dt, Flag);
        }
        #endregion

        #region 根据病区ID和床号ID(或CODE)获取住院号
        /// <summary>
        /// 根据病区ID和床号ID(或CODE)获取住院号
        /// </summary>
        /// <param name="AreaID">病区ID</param>
        /// <param name="BedID">床号ID(或CODE)</param>          
        /// <returns></returns>        
        public string m_strGetZyhByAreaAndBedID(string AreaID, string BedID)
        {
            return proxy.Service.m_strGetZyhByAreaAndBedID(AreaID, BedID);
        }
        #endregion

        #region 根据住院号或诊疗卡号获取病人信息
        /// <summary>
        /// 根据住院号或诊疗卡号获取病人信息
        /// </summary>
        /// <param name="no"></param>
        /// <param name="dt"></param>
        /// <param name="flag">0 所有 1 在院 2 出院 3 呆帐</param>
        /// <param name="type">0 诊疗卡号或住院号 1 诊疗卡号  2 住院号 </param>
        /// <returns></returns>
        public long m_lngGetPatientinfoByNO(string no, out DataTable dt, int flag, int type)
        {
            return proxy.Service.m_lngGetPatientinfoByNO(no, out dt, flag, type);
        }
        #endregion

        #region  查找出院病人床号
        /// <summary>
        /// 查找出院病人床号
        /// </summary>
        /// <param name="no"></param>
        /// <param name="type"></param>
        /// <param name="p_strBedNo"></param>
        /// <returns></returns>
        public long m_lngGetDedNo(string no, ref string p_strBedNo)
        {
            return proxy.Service.m_lngGetDedNo(no, ref p_strBedNo);
        }
        #endregion

        #region 根据住院登记流水号查找病人所有期帐信息
        /// <summary>
        /// 根据住院登记流水号查找病人所有期帐信息
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetPatientDayaccountsByRegID(string RegID, out DataTable dt)
        {
            return proxy.Service.m_lngGetPatientDayaccountsByRegID(RegID, out dt);
        }
        #endregion

        #region 获取该期帐有效费用信息
        /// <summary>
        /// 获取该期帐有效费用信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type">1 根据入院登记ID 2 根据期帐ID </param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetChargeInfoByID(string ID, int type, out DataTable dt)
        {
            return proxy.Service.m_lngGetChargeInfoByID(ID, type, out dt);
        }
        #endregion

        #region 母婴合并结算费用一起查询ucpatien控件使用
        /// <summary>
        /// 母婴合并结算费用一起查询ucpatien控件使用
        /// </summary>
        /// <param name="p_strRegisterID">病人registerId</param>
        /// <param name="p_dtbCharge"></param>
        /// <returns></returns>
        public long m_lngGetChargeInfoByIDForBaby(string p_strRegisterID, out DataTable p_dtbCharge)
        {
            return proxy.Service.m_lngGetChargeInfoByIDForBaby(p_strRegisterID, out p_dtbCharge);
        }
        #endregion

        #region 获取项目分类信息(核算分类、发票分类)
        /// <summary>
        /// 获取项目分类信息(核算分类、发票分类)
        /// </summary>
        /// <param name="flag">分类类型：1 门诊核算 2 门诊发票 3 住院核算 4 住院发票 5 病案核算</param>
        /// <param name="dt"></param>
        /// <returns></returns>              
        public long m_lngGetChargeItemCat(int flag, out DataTable dt)
        {
            return proxy.Service.m_lngGetChargeItemCat(flag, out dt);
        }
        #endregion

        #region 根据住院登记流水号检查项目状态
        /// <summary>
        /// 根据住院登记流水号检查项目状态
        /// </summary>
        /// <param name="RegID">住院登记流水号</param>
        /// <param name="status">0=待确认;1=待结;2=待清;3=已清;4=直收</param>
        /// <returns></returns>
        public bool m_blnCheckChargeItemStatus(string RegID, int status)
        {
            return proxy.Service.m_blnCheckChargeItemStatus(RegID, status);
        }
        #endregion

        #region 根据住院登记流水号生成各状态项目总费用
        /// <summary>
        /// 根据住院登记流水号生成各状态项目总费用
        /// </summary>
        /// <param name="RegID">住院登记流水号</param>
        /// <param name="status">0 待确认 1 待结 2 待清 3 已清 4 直收 9 生成期帐</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetChargeItemFee(string RegID, int status, out DataTable dt)
        {
            return proxy.Service.m_lngGetChargeItemFee(RegID, status, out dt);
        }
        #endregion

        #region 检查发票号是否重复
        /// <summary>
        /// 检查发票号是否重复
        /// </summary>
        /// <param name="CurrNo"></param>
        /// <returns></returns>
        public bool m_blnCheckInvoiceNo(string CurrNo)
        {
            return proxy.Service.m_blnCheckInvoiceNo(CurrNo);
        }
        #endregion

        #region 根据入院登记流水ID获取最后诊金、床位费生成时间
        /// <summary>
        /// 根据入院登记流水ID获取最后诊金、床位费生成时间
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="FinallyDate"></param>
        /// <returns></returns>
        public long m_lngGetFinallyDiagFeeDateByRegID(string RegID, out string FinallyDate)
        {
            return proxy.Service.m_lngGetFinallyDiagFeeDateByRegID(RegID, out FinallyDate);
        }
        #endregion

        #region 根据入院登记流水号获取预出院时间
        /// <summary>
        /// 根据入院登记流水号获取预出院时间
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="PrepDate"></param>
        /// <returns></returns>
        public long m_lngGetPrepLHDateByRegID(string RegID, out string PrepDate)
        {
            return proxy.Service.m_lngGetPrepLHDateByRegID(RegID, out PrepDate);
        }
        #endregion

        #region 结帐
        /// <summary>
        /// 结帐
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="DayChrgType">期帐结算类型：1 期帐 2 明细</param>
        /// <param name="DayAccountsArr"></param>
        /// <param name="Charge_VO"></param>
        /// <param name="ChargeCatArr"></param>
        /// <param name="Invoice_VO"></param>
        /// <param name="InvoCatArr"></param>
        /// <param name="PaymentArr"></param>
        /// <param name="PrePayDeal">预交金处理： 0 不处理 1 退回 2 转下期</param> 
        /// <param name="PrePayIDArr"></param>
        /// <param name="ChrgType">结算类型：1 中途结算 2 出院结算 3 呆帐结算</param>
        /// <returns></returns>
        public long m_lngReckoning(DataTable dtSource, int DayChrgType, List<clsBihDayAccounts_VO> DayAccountsArr, clsBihCharge_VO Charge_VO, List<clsBihChargeCat_VO> ChargeCatArr, clsBihInvoice_VO Invoice_VO, List<clsBihInvoiceCat_VO> InvoCatArr, List<clsBihPayment_VO> PaymentArr, int PrePayDeal, List<string> PrePayIDArr, int ChargeType, clsBihConfirm_VO Confirm_VO, out string ChargeNo)
        {
            //return proxy.Service.m_lngReckoning(dtSource, DayChrgType, DayAccountsArr, Charge_VO, ChargeCatArr, Invoice_VO, InvoCatArr, PaymentArr, PrePayDeal, PrePayIDArr, ChargeType, Confirm_VO, out ChargeNo);
            return proxy.Service.m_lngReckoning(weCare.Core.Utils.Compression.Zip(dtSource), DayChrgType, DayAccountsArr, Charge_VO, ChargeCatArr, Invoice_VO, InvoCatArr, PaymentArr, PrePayDeal, PrePayIDArr, ChargeType, Confirm_VO, out ChargeNo);
        }
        #endregion

        #region 退款
        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="ChargeNo"></param>
        /// <param name="Invono"></param>
        /// <param name="EmpID"></param>
        /// <param name="ChargeType">结算类型：1 中途结算 2 出院结算 3 呆帐结算 4 直收 5 确认收费 6 呆帐补交款结算</param>
        /// <returns></returns>
        public long m_lngRefundment(string ChargeNo, string Invono, string EmpID, int ChargeType, int PayMode)
        {
            return proxy.Service.m_lngRefundment(ChargeNo, Invono, EmpID, ChargeType, PayMode);
        }
        #endregion

        #region 根据结算号获取发票明细信息
        /// <summary>
        /// 根据结算号获取发票明细信息
        /// </summary>
        /// <param name="ChargeNo"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPrepay"></param>
        /// <returns></returns>
        public long m_lngGetInvoiceByChargeNo(string ChargeNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPrepay, out DataTable dtPayMode, out DataTable dtItemDate)
        {
            return proxy.Service.m_lngGetInvoiceByChargeNo(ChargeNo, out dtMain, out dtDet, out dtPrepay, out dtPayMode, out dtItemDate);
        }
        #endregion

        #region 根据入院登记流水ID获取发票号信息
        /// <summary>
        /// 根据入院登记流水ID获取发票号信息
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="Type">发票类型范围：1 正常 2 正常+重打</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetInvoiceInfoByRegID(string RegID, int Type, out DataTable dt)
        {
            return proxy.Service.m_lngGetInvoiceByRegID(RegID, Type, out dt);
        }
        #endregion

        #region 查询收费项目
        /// <summary>
        /// 查询收费项目
        /// </summary>
        /// <param name="FindStr"></param>
        /// <param name="PatType">病人身份</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngFindChargeItem(string FindStr, string PatType, out DataTable dt, bool isChildPrice)
        {
            return proxy.Service.m_lngFindChargeItem(FindStr, PatType, out dt, isChildPrice);
        }

        /// <summary>
        /// 根据项目ID查找收费项目
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngFindChargeItem(string ItemID, out DataTable dt, bool isChildPrice)
        {
            return proxy.Service.m_lngFindChargeItem(ItemID, out dt, isChildPrice);
        }
        #endregion

        #region 直接收费生成费用明细
        /// <summary>
        /// 直接收费生成费用明细
        /// </summary>
        /// <param name="OrderDicArr">主诊疗项目数组</param>
        /// <param name="PatientChargeArr">费用明细数组</param>
        /// <param name="Type">8 直收 9 补记帐</param>
        /// <param name="OrderID">返回的费用ID号(是呀医嘱号字段)</param>
        /// <returns></returns>        
        public long m_lngGenPatientChargeByDir(List<clsBihOrderDic_VO> OrderDicArr, List<clsBihPatientCharge_VO> PatientChargeArr, int Type, ref string OrderID)
        {
            return proxy.Service.m_lngGenPatientChargeByDir(OrderDicArr, PatientChargeArr, Type, ref OrderID);
        }
        #endregion

        #region 根据住院登记流水号获取各生效类型(费用状态)费用信息
        /// <summary>
        /// 根据住院登记流水号获取各生效类型(费用状态)费用信息
        /// </summary>
        /// <param name="RegID">住院登记流水号</param>
        /// <param name="ActiveType">生效类型{1=其它;2=补记帐;3=确认记帐;4=确认收费;5=直接收费;888=费用状态分类;999=全部}</param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetFeeItemByActiveType(string RegID, int ActiveType, string Pstatus, string AreaID, string BeginDate, string EndDate, out DataTable dt)
        {
            long l = proxy.Service.m_lngGetFeeItemByActiveType(RegID, ActiveType, Pstatus, AreaID, BeginDate, EndDate, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                #region Lis
                DataRow[] drr = null;
                DataTable dtRpt = proxy.Service.GetFeeItemByActiveTypeLis(dt);
                if (dtRpt != null && dtRpt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRpt.Rows)
                    {
                        drr = dt.Select("orderid_chr = '" + dr["orderId"].ToString() + "'");
                        if (drr != null && drr.Length > 0)
                        {
                            foreach (DataRow dr1 in drr)
                            {
                                dr1["rptStatus"] = 1;
                            }
                        }
                    }
                    dt.AcceptChanges();
                }
                #endregion

                #region Pacs
                try
                {
                    List<string> lstOrderId = proxy.Service.GetFeeItemByActiveTypePacs(dt);
                    if (lstOrderId.Count > 0)
                    {
                        foreach (string orderId in lstOrderId)
                        {
                            drr = dt.Select("orderid_chr = '" + orderId + "'");
                            if (drr != null && drr.Length > 0)
                            {
                                foreach (DataRow dr in drr)
                                {
                                    dr["rptStatus"] = 1;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("连接影像数据库(PACS)失败，不能判断检查费用状态。" + Environment.NewLine + ex.Message);
                }
                #endregion
            }

            return l;
        }
        #endregion

        #region 根据医嘱ID(直收费用ID)获取费用明细
        /// <summary>
        /// 根据医嘱ID(直收费用ID)获取费用明细
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientChargeByID(string ID, out DataTable dt)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngGetPatientChargeByID(ID, out dt);
        }
        #endregion

        #region 获取收费项目默认执行地点
        /// <summary>
        /// 获取收费项目默认执行地点
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ApplyAreaID"></param>
        /// <returns></returns>        
        public string m_strGetChargeItemDefaultExecAreaID(string ItemID, string ApplyAreaID, out string ExecAreaName)
        {
            return proxy.Service.m_strGetChargeItemDefaultExecAreaID(ItemID, ApplyAreaID, out ExecAreaName);
        }
        #endregion

        #region 提交补记帐费用明细
        /// <summary>
        /// 提交补记帐费用明细
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>        
        public long m_lngCommitPatchCharge(string OrderID, string RegID, string OperID, int Type)
        {
            return proxy.Service.m_lngCommitPatchCharge(OrderID, RegID, OperID, Type);
        }
        #endregion

        #region 按日期滚费
        /// <summary>
        /// 按日期滚费
        /// </summary>
        /// <param name="ExecDate">滚费时间(格式：yyy-mm-dd hh:mm:ss</param>
        /// <param name="FeeDate">费用时间(格式：yyy-mm-dd hh:mm:ss</param>
        /// <param name="OperID">操作员ID</param>   
        /// <param name="RegID">个人滚费时：入院登记ID</param>  
        /// <param name="ExecType">1 正常夜间 2 出院补滚</param>
        public long AutoCharge(string ExecDate, string FeeDate, string OperID, string RegID, int ExecType)
        {
            //com.digitalwave.iCare.middletier.HIS.clsAutoCharge objSvc =
            //                                               (com.digitalwave.iCare.middletier.HIS.clsAutoCharge)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAutoCharge));            

            //long l = proxy.Service.AutoCharge(ExecDate, OperID);
            //proxy.Service.Dispose();

            //com.digitalwave.iCare.middletier.HIS.clsAutoCharge_CS objSvc =
            //                                               (com.digitalwave.iCare.middletier.HIS.clsAutoCharge_CS)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAutoCharge_CS));

            com.digitalwave.iCare.middletier.HIS.clsAutoCharge_CS objSvc = new com.digitalwave.iCare.middletier.HIS.clsAutoCharge_CS();
            long l = objSvc.AutoCharge(ExecDate, FeeDate, OperID, clsPublic.m_intGetSysParm("1013"), clsPublic.m_intGetSysParm("1014"), RegID, ExecType);
            objSvc = null;

            return l;
        }
        #endregion

        #region 出院收取连续性费用
        /// <summary>
        /// 出院收取连续性费用
        /// </summary>
        /// <param name="FeeDate">费用时间(格式：yyy-mm-dd hh:mm:ss)</param>
        /// <param name="OperID">操作员ID</param>
        /// <param name="RegID">入院登记ID</param>
        /// <returns></returns>
        public long AutoChargeContinueItem(string FeeDate, string OperID, string RegID)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.AutoChargeContinueItem(FeeDate, OperID, RegID);
        }
        #endregion

        #region 获取期帐日期信息
        /// <summary>
        /// 获取期帐日期信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long GetDayAccountsInfo(out DataTable dt)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.GetDayAccountsInfo(out dt);
        }
        #endregion

        #region 获取期帐最后生成时间
        /// <summary>
        /// 获取期帐最后生成时间
        /// </summary>
        /// <param name="RegID">入院登记ID</param>
        /// <returns></returns>        
        public string GetDayAccountsMaxDate(string RegID)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.GetDayAccountsMaxDate(RegID);
        }
        #endregion

        #region 生成期帐
        /// <summary>
        /// 生成期帐
        /// </summary>
        /// <param name="DayAccounts_VO">期帐VO</param>
        /// <param name="EmpID">操作员ID</param>       
        /// <param name="ChargeType">0 普通结帐 1 出院结帐 2 出院结算</param>     
        /// <returns></returns>        
        public long m_lngBuildDayAccounts(clsBihDayAccounts_VO DayAccounts_VO, string EmpID, int ChargeType)
        {
            return proxy.Service.m_lngBuildDayAccounts(DayAccounts_VO, EmpID, ChargeType);
        }
        #endregion

        #region 补期帐
        /// <summary>
        /// 补期帐
        /// </summary>
        /// <param name="PatientChargeArr">费用明细数组</param>        
        /// <param name="DayAccountID">期帐ID</param>                
        /// <returns></returns>        
        public long m_lngPatchDayAccount(List<clsBihPatientCharge_VO> PatientChargeArr, string DayAccountID)
        {
            return proxy.Service.m_lngPatchDayAccount(PatientChargeArr, DayAccountID);
        }
        #endregion

        #region 保存发票打印设置
        /// <summary>
        /// 保存发票打印设置
        /// </summary>
        /// <param name="ChargeItemCatArr">费用分类设置VO</param>
        /// <param name="Scope">范围: 1 门诊核算 2 门诊发票 3 住院核算 4 住院发票</param>
        /// <returns></returns>        
        public long m_lngSaveInvoiceSet(List<clsBihChargeItemCat_VO> ChargeItemCatArr, string Scope)
        {
            return proxy.Service.m_lngSaveInvoiceSet(ChargeItemCatArr, Scope);
        }
        #endregion

        #region 收款员日结(发票+按金)
        /// <summary>
        /// 收款员日结(发票+按金)
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="RecDate"></param>
        /// <param name="RemarkInfo"></param>
        /// <param name="RecType">0 全部 1 发票 2 按金</param>
        /// <returns></returns>        
        public long m_lngDayReckoningUnion(string EmpID, string RecDate, string RemarkInfo, int RecType)
        {
            return proxy.Service.m_lngDayReckoningUnion(EmpID, RecDate, RemarkInfo, RecType);
        }
        #endregion

        #region 收款员日结(发票)
        /// <summary>
        /// 收款员日结(发票)
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="RecDate"></param>
        /// <param name="RemarkInfo"></param>
        /// <returns></returns>        
        public long m_lngDayReckoning(string EmpID, string RecDate, string RemarkInfo)
        {
            return proxy.Service.m_lngDayReckoning(EmpID, RecDate, RemarkInfo);
        }
        #endregion

        #region 收款员日结(按金)
        /// <summary>
        /// 收款员日结(按金)
        /// </summary>
        /// <param name="EmpID">收款员ID</param>
        /// <param name="RecDate"></param>  
        /// <param name="RemarkInfo"></param>
        /// <returns></returns>        
        public long m_lngDayReckoningPre(string EmpID, string RecDate, string RemarkInfo)
        {
            return proxy.Service.m_lngDayReckoningPre(EmpID, RecDate, RemarkInfo);
        }
        #endregion

        #region 获取收款员日结时间列表
        /// <summary>
        /// 获取收款员日结时间列表
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetDayReckoningTime(string EmpID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngGetDayReckoningTime(EmpID, BeginDate, EndDate, out dt);
        }
        #endregion

        #region (医保)传送住院收费数据到医保前置机
        /// <summary>
        /// (医保)传送住院收费数据到医保前置机
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>
        public long m_lngSendybdata(string DSN, List<clsYB_VO> objYBArr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsZyYB objSvc =
            //                                                   (com.digitalwave.iCare.middletier.HIS.clsZyYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsZyYB));

            //clsZyYB objSvc = new clsZyYB();

            return (new weCare.Proxy.ProxyIP02()).Service.m_lngSendybdata(DSN, objYBArr);
        }

        /// <summary>
        /// (医保)传送住院收费数据到医保前置机
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>
        public long m_lngSendybdata(string DSN, DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsZyYB objSvc =
            //                                             (com.digitalwave.iCare.middletier.HIS.clsZyYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsZyYB));

            //clsZyYB objSvc = new clsZyYB();

            return (new weCare.Proxy.ProxyIP02()).Service.m_lngSendybdata(DSN, dt);
        }
        #endregion

        #region (医保)查询传送收费项目是否成功
        /// <summary>
        /// (医保)查询传送收费项目是否成功
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="Hospcode"></param>
        /// <param name="ZYNo"></param>
        /// <param name="ZYSno"></param>
        /// <returns></returns>
        public bool m_blnCheckSendRes(string DSN, string Hospcode, string ZYNo, string ZYSno)
        {
            //com.digitalwave.iCare.middletier.HIS.clsZyYB objSvc =
            //                                                   (com.digitalwave.iCare.middletier.HIS.clsZyYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsZyYB));

            //clsZyYB objSvc = new clsZyYB();

            return (new weCare.Proxy.ProxyIP02()).Service.m_blnCheckSendRes(DSN, Hospcode, ZYNo, ZYSno);
        }
        #endregion

        #region (医保)传送时HIS事务失败，手工删除传送数据
        /// <summary>
        /// (医保)传送时HIS事务失败，手工删除传送数据
        /// </summary>
        /// <param name="billno"></param>
        /// <returns></returns>
        public long m_lngDelybdata(string DSN, string ZYNo, string ZYSno)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngDelybdata(DSN, ZYNo, ZYSno);
        }
        #endregion

        #region (医保)获取医保结算明细
        /// <summary>
        /// (医保)获取医保结算明细
        /// </summary>
        /// <param name="DSN"></param>        
        /// <param name="Hospcode"></param>        
        /// <param name="ZYNo"></param>
        /// <param name="ZYSno"></param>
        /// <param name="YbType">1 普通 2 公务员</param>
        /// <returns></returns>      
        public long m_lngGetybjsmx(string DSN, string Hospcode, string ZYNo, string ZYSno, out DataTable dtRecord, out int YbType)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngGetybjsmx(DSN, Hospcode, ZYNo, ZYSno, out dtRecord, out YbType);
        }
        #endregion

        #region (医保试算)发送数据
        /// <summary>
        /// (医保试算)发送数据
        /// </summary>
        /// <param name="HospCode"></param>
        /// <param name="RegID"></param>
        /// <param name="Mode">1 模式一：全部未清项目 2 模式二：指定项目</param>
        /// <returns></returns>        
        public long m_lngBudgetSendData(string HospCode, string RegID, int Mode)
        {
            return proxy.Service.m_lngBudgetSendData(HospCode, RegID, Mode);
        }
        #endregion

        #region (医保试算)接收数据
        /// <summary>
        /// (医保试算)接收数据
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <returns></returns>        
        public long m_lngBudgetGetData(string RegID, out DataTable dtMain, out DataTable dtDet)
        {
            return proxy.Service.m_lngBudgetGetData(RegID, out dtMain, out dtDet);
        }
        #endregion

        #region (医保)下载医保前置机数据
        /// <summary>
        /// (医保)下载医保前置机数据
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="Hospcode"></param>
        /// <param name="ZYNo"></param>
        /// <param name="ZYSno"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngDownloadYBData(string DSN, string Hospcode, string ZYNo, string ZYSno, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsZyYB objSvc =
            //                                             (com.digitalwave.iCare.middletier.HIS.clsZyYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsZyYB));

            //clsZyYB objSvc = new clsZyYB();

            return (new weCare.Proxy.ProxyIP02()).Service.m_lngDownloadYBData(DSN, Hospcode, ZYNo, ZYSno, out dt);
        }
        #endregion

        #region (医保)下载医保前置机数据->生成到本地
        /// <summary>
        /// (医保)下载医保前置机数据->生成到本地
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngDownloadYBData(DataTable dt)
        {
            return proxy.Service.m_lngDownloadYBData(dt);
        }
        #endregion

        #region (医保)删除已下载医保前置机数据
        /// <summary>
        /// (医保)删除已下载医保前置机数据
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="Zycs"></param>
        /// <returns></returns>        
        public long m_lngDelDownloadYBData(string Zyh, int Zycs)
        {
            return proxy.Service.m_lngDelDownloadYBData(Zyh, Zycs);
        }
        #endregion

        #region (医保)获取已下载医保前置机数据
        /// <summary>
        /// (医保)获取已下载医保前置机数据
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="Zycs"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetDownloadYBData(string Zyh, int Zycs, out DataTable dt)
        {
            return proxy.Service.m_lngGetDownloadYBData(Zyh, Zycs, out dt);
        }
        #endregion

        #region 医保试算
        /// <summary>
        /// 医保试算
        /// </summary>
        /// <param name="HospCode"></param>
        /// <param name="RegID"></param>
        /// <param name="Zyh"></param>
        /// <param name="Zycs"></param>
        /// <param name="TotalMoney"></param>
        /// <param name="InsuredMoney"></param>
        /// <param name="OutErrMsg"></param>
        /// <returns></returns>        
        public long m_lngYBBudget(string HospCode, string RegID, string Zyh, int Zycs, out decimal TotalMoney, out decimal InsuredMoney, out string OutErrMsg)
        {
            long lngRes = proxy.Service.m_lngYBBudget(HospCode, RegID, Zyh, Zycs, out TotalMoney, out InsuredMoney, out OutErrMsg);
            return proxy.Service.m_lngZYSS(HospCode, Zyh, Zycs, out TotalMoney, out InsuredMoney, out OutErrMsg);
        }
        #endregion

        #region (茶山医保)生成DBF文件
        /// <summary>
        /// (茶山医保)生成DBF文件
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="DbfName"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>  
        public long m_lngCreateDBF(string DSN, string DbfName, ArrayList objYBArr)
        {
            com.digitalwave.iCare.middletier.HIS.clsChaShan objSvc = new middletier.HIS.clsChaShan();
            //(com.digitalwave.iCare.middletier.HIS.clsChaShan)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChaShan));

            return objSvc.m_lngCreateDbf(DSN, DbfName, objYBArr);
        }
        #endregion

        #region (医保)获取结果
        /// <summary>
        /// (医保)获取结果
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="DbfName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetResult(string DSN, string DbfName, out DataTable dtRecord)
        {
            com.digitalwave.iCare.middletier.HIS.clsChaShan objSvc = new middletier.HIS.clsChaShan();
            //(com.digitalwave.iCare.middletier.HIS.clsChaShan)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChaShan));

            return objSvc.m_lngGetResult(DSN, DbfName, out dtRecord);
        }
        #endregion

        #region 查找诊疗项目
        /// <summary>
        /// 查找诊疗项目
        /// </summary>
        /// <param name="ID"></param>        
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngFindOrderByID(string ID, out DataTable dt, bool isChildPrice)
        {
            return proxy.Service.m_lngFindOrderByID(ID, out dt, isChildPrice);
        }
        #endregion

        #region 根据诊疗项目获取收费项目
        /// <summary>
        /// 根据诊疗项目获取收费项目
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="PayTypeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetChargeItemByOrderID(string OrderID, string PayTypeID, out DataTable dt, bool isChildPrice)
        {
            return proxy.Service.m_lngGetChargeItemByOrderID(OrderID, PayTypeID, out dt, isChildPrice);
        }
        #endregion

        #region 判断诊疗项目(组合)是否允许打折
        /// <summary>
        /// 判断诊疗项目(组合)是否允许打折
        /// </summary>
        /// <param name="OrderID">诊疗项目ID</param>
        /// <param name="InvoCatArr">发票类型数组</param>
        /// <param name="SysType">系统 1 门诊 2 住院</param>
        /// <param name="ItemNums">项目个数</param>
        /// <returns></returns>        
        public bool m_blnCheckOrderDiscount(string OrderID, List<string> InvoCatArr, int SysType, int ItemNums)
        {
            return proxy.Service.m_blnCheckOrderDiscount(OrderID, InvoCatArr, SysType, ItemNums);
        }
        #endregion

        #region 获取补记帐、直收费用(诊疗项目)主表记录
        /// <summary>
        /// 获取补记帐、直收费用(诊疗项目)主表记录
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetOrderDic(string OrderID, out DataTable dt)
        {
            return proxy.Service.m_lngGetOrderDic(OrderID, out dt);
        }
        #endregion

        #region (医保)更新医保统筹费用
        /// <summary>
        /// (医保)更新医保统筹费用
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="InsuredSum"></param>
        /// <returns></returns>        
        public long m_lngUpdateInsuredSum(string RegID, decimal InsuredSum)
        {
            return proxy.Service.m_lngUpdateInsuredSum(RegID, InsuredSum);
        }
        #endregion

        #region 出院结帐则立即腾出床位
        /// <summary>
        /// 出院结帐则立即腾出床位
        /// </summary>
        /// <param name="RegID"></param>
        /// <returns></returns>        
        public long m_lngClearBed(string RegID)
        {
            return proxy.Service.m_lngClearBed(RegID);
        }
        #endregion

        #region 记录当前收费员使用之发票号
        /// <summary>
        /// 记录当前收费员使用之发票号
        /// </summary>
        /// <param name="OperID"></param>
        /// <param name="InvoNo"></param>
        /// <param name="Type">类型： 1 住院发票 2 押金单 3 门诊发票</param>
        /// <returns></returns>        
        public long m_lngRegOperInvoNO(string OperID, string InvoNo, int Type)
        {
            return proxy.Service.m_lngRegOperInvoNO(OperID, InvoNo, Type);
        }
        #endregion

        #region 获取当前收费员使用之发票号
        /// <summary>
        /// 获取当前收费员使用之发票号
        /// </summary>
        /// <param name="OperID"></param>
        /// <param name="Type">类型： 1 住院发票 2 押金单 3 门诊发票</param>
        /// <param name="InvoNo"></param>
        /// <returns></returns>        
        public long m_lngGetOperInvoNO(string OperID, int Type, out string InvoNo)
        {
            return proxy.Service.m_lngGetOperInvoNO(OperID, Type, out InvoNo);
        }
        #endregion

        #region 检查婴儿费
        /// <summary>
        /// 检查婴儿费
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngCheckBaby(string Zyh, out DataTable dt)
        {
            return proxy.Service.m_lngCheckBaby(Zyh, out dt);
        }
        #endregion

        #region 补记帐退费
        /// <summary>
        /// 补记帐退费
        /// </summary>
        /// <param name="ChargeIDArr"></param>
        /// <param name="EmpID"></param>
        /// <returns></returns>        
        public long m_lngPatchRefundment(List<clsBihRefCharge_VO> ChargeIDArr, string EmpID)
        {
            return proxy.Service.m_lngPatchRefundment(ChargeIDArr, EmpID);
        }
        #endregion

        #region 修改备注信息
        /// <summary>
        /// 修改备注信息
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="RecDate"></param>
        /// <param name="RemarkInfo"></param>
        /// <returns></returns>        
        public long m_lngUpdateDayRecRemark(string EmpID, string RecDate, string RemarkInfo)
        {
            return proxy.Service.m_lngUpdateDayRecRemark(EmpID, RecDate, RemarkInfo);
        }
        #endregion

        #region 获取不同费别的费用明细
        /// <summary>
        /// 获取不同费别的费用明细
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="PayTypeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientFeeDetByPayType(string RegID, string PayTypeID, out DataTable dt)
        {
            return proxy.Service.m_lngGetPatientFeeDetByPayType(RegID, PayTypeID, out dt);
        }
        #endregion

        #region 费用明细对应之诊疗项目
        /// <summary>
        /// 费用明细对应之诊疗项目
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="ActiveType">生效类型{1=其它;2=补记帐;3=确认记帐;4=确认收费;5=直接收费;888=费用状态分类;999=全部}</param>
        /// <param name="Pstatus"></param>
        /// <param name="AreaID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetFeeDiagItem(string RegID, int ActiveType, string Pstatus, string AreaID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngGetFeeDiagItem(RegID, ActiveType, Pstatus, AreaID, BeginDate, EndDate, out dt);
        }
        #endregion

        #region 根据费用明细ID查找费用信息
        /// <summary>
        /// 根据费用明细ID查找费用信息
        /// </summary>
        /// <param name="DiagArr"></param>
        /// <param name="dtNormal"></param>
        /// <param name="dtRefundment"></param>
        /// <returns></returns>        
        public long m_lngGetFeeItemByActiveType(List<clsParmDiagItem_VO> DiagArr, out DataTable dtNormal, out DataTable dtRefundment)
        {
            return proxy.Service.m_lngGetFeeItemByActiveType(DiagArr, out dtNormal, out dtRefundment);
        }
        #endregion

        #region 按科室类型获取病人费用分类
        /// <summary>
        /// 按科室类型获取病人费用分类
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="DeptClass">1 执行科室 2 开单科室 3 所在病区</param>
        /// <param name="Status">0 未呆帐结算 1 已呆帐结算</param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetFeeCatByDeptClass(string RegID, int DeptClass, int Status, out DataTable dt)
        {
            return proxy.Service.m_lngGetFeeCatByDeptClass(RegID, DeptClass, Status, out dt);
        }
        #endregion

        #region 按科室类型获取病人费用分类母婴合并结算使用 by yibing.zheng 09-07-04

        /// <summary>
        /// 按科室类型获取病人费用分类母婴合并结算使用
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="DeptClass">1 执行科室 2 开单科室 3 所在病区</param>
        /// <param name="Status">0 未呆帐结算 1 已呆帐结算</param>
        /// <param name="dt"></param>
        /// <returns></returns>

        public long m_lngGetFeeCatByDeptClassForMortherBaby(string RegID, int DeptClass, int Status, out DataTable dt)
        {
            return proxy.Service.m_lngGetFeeCatByDeptClassForMortherBaby(RegID, DeptClass, Status, out dt);
        }
        #endregion

        #region 呆帐结算
        /// <summary>
        /// 呆帐结算
        /// </summary>
        /// <param name="Charge_VO"></param>
        /// <param name="ChargeCatArr"></param>
        /// <param name="FactTotalMoney">实际未结、未清总金额</param>
        /// <param name="FactPreMoney">实际分摊总金额</param>
        /// <param name="DiffValDeptID">差值项科室ID</param>
        /// <param name="DiffValCatID">差值项核算分类ID</param>
        /// <param name="IsHavePrepayMoney">是否有预交金 (true 有 false 无)</param>
        /// <param name="ChargeNo"></param>
        /// <returns></returns>        
        public long m_lngBadCharge(clsBihCharge_VO Charge_VO, List<clsBihChargeCat_VO> ChargeCatArr, clsBihInvoice_VO Invoice_VO, List<clsBihInvoiceCat_VO> InvoCatArr, List<clsBihPayment_VO> PaymentArr, decimal FactTotalMoney, decimal FactPreMoney, string DiffValDeptID, string DiffValCatID, bool IsHavePrepayMoney, out string ChargeNo)
        {
            return proxy.Service.m_lngBadCharge(Charge_VO, ChargeCatArr, Invoice_VO, InvoCatArr, PaymentArr, FactTotalMoney, FactPreMoney, DiffValDeptID, DiffValCatID, IsHavePrepayMoney, out ChargeNo);
        }
        #endregion

        #region 获取呆帐病人未清费用
        /// <summary>
        /// 获取呆帐病人未清费用
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetBadChargeFeeInfo(string RegID, out DataTable dt)
        {
            return proxy.Service.m_lngGetBadChargeFeeInfo(RegID, out dt);
        }

        /// <summary>
        /// 获取呆帐病人未清费用(母婴合并结算合用)
        /// </summary>
        /// <param name="RegID">病人ID</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetBadChargeFeeInfoMotherBaby(string RegID, out DataTable dt)
        {
            return proxy.Service.m_lngGetBadChargeFeeInfoMotherBaby(RegID, out dt);
        }
        #endregion

        #region 结帐
        /// <summary>
        /// 结帐
        /// </summary>
        /// <param name="Charge_VO"></param>
        /// <param name="ChargeNo"></param>
        /// <returns></returns>        
        public long m_lngReckoning(clsBihCharge_VO Charge_VO, out string ChargeNo)
        {
            return proxy.Service.m_lngReckoning(Charge_VO, out ChargeNo);
        }
        #endregion

        #region 获取记帐所有处方ID
        /// <summary>
        /// 获取记帐所有处方ID
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngMzGetAcctRecipeID(string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngMzGetAcctRecipeID(BeginDate, EndDate, out dt);
        }
        #endregion

        #region 根据已收费处方ID获取核算分类
        /// <summary>
        /// 根据已收费处方ID获取核算分类
        /// </summary>
        /// <param name="RecipeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngMzGetRecipeCat(string RecipeID, out DataTable dt)
        {
            return proxy.Service.m_lngMzGetRecipeCat(RecipeID, out dt);
        }
        #endregion

        #region 根据SEQID获取核算分类
        /// <summary>
        /// 根据SEQID获取核算分类
        /// </summary>
        /// <param name="SeqID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngMzGetChargeCat(string SeqID, out DataTable dt)
        {
            return proxy.Service.m_lngMzGetChargeCat(SeqID, out dt);
        }
        #endregion

        #region 根据SEQID、CatID更新核算分类
        /// <summary>
        /// 根据SEQID、CatID更新核算分类
        /// </summary>
        /// <param name="SeqID"></param>
        /// <param name="CatIDArr"></param>
        /// <param name="CatSumArr"></param>
        /// <returns></returns>        
        public long m_lngMzUpdateChargeCat(string SeqID, List<string> CatIDArr, List<decimal> CatSumArr, string PStatus)
        {
            return proxy.Service.m_lngMzUpdateChargeCat(SeqID, CatIDArr, CatSumArr, PStatus);
        }
        #endregion

        #region 根据处方ID获取SEQID列表
        /// <summary>
        /// 根据处方ID获取SEQID列表
        /// </summary>
        /// <param name="RecipeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngMzGetSeqIDList(string RecipeID, out DataTable dt)
        {
            return proxy.Service.m_lngMzGetSeqIDList(RecipeID, out dt);
        }
        #endregion

        #region <门诊>发票信息
        /// <summary>
        /// (门诊)发票信息 
        /// </summary>
        /// <param name="ChargeNo"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPayMode"></param>
        /// <returns></returns>        
        public long m_lngGetOPInvoiceByChargeNo(string ChargeNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPayMode)
        {
            return proxy.Service.m_lngGetOPInvoiceByChargeNo(ChargeNo, out dtMain, out dtDet, out dtPayMode);
        }

        /// <summary>
        /// 门诊发票信息 
        /// </summary>
        /// <param name="InvoNo"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPayMode"></param>
        /// <returns></returns> 
        public long m_lngGetOPInvoiceByInvoNo(string InvoNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPayMode)
        {
            return proxy.Service.m_lngGetOPInvoiceByInvoNo(InvoNo, out dtMain, out dtDet, out dtPayMode);
        }

        /// <summary>
        /// 门诊发票信息(for 退票)
        /// </summary>
        /// <param name="mode">模式(保留标识符) 0-退票</param>
        /// <param name="InvoNo"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPayMode"></param>
        /// <returns></returns>
        public long m_lngGetOPInvoiceByInvoNo(int mode, string InvoNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPayMode)
        {
            return proxy.Service.m_lngGetOPInvoiceByInvoNo(mode, InvoNo, out dtMain, out dtDet, out dtPayMode);
        }
        #endregion

        #region 江门台山医保结算
        /// <summary>
        /// 插入费用明细
        /// </summary>
        /// <param name="p_strlsh0"></param>
        /// <param name="p_inpatientid"></param>
        /// <returns></returns>
        public long m_lngInsertRegisterCharge(string p_strlsh0, string p_inpatientid)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngInsertRegisterCharge(p_strlsh0, p_inpatientid);
        }
        /// <summary>
        /// 插入病人信息
        /// </summary>
        /// <param name="p_strlsh0"></param>
        /// <param name="p_inpatientid"></param>
        /// <returns></returns>
        public long m_lngInsertRegister(string p_strlsh0, string p_inpatientid)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngInsertRegister(p_strlsh0, p_inpatientid);
        }
        /// <summary>
        /// 获取医保支付的金额
        /// </summary>
        /// <param name="p_strlsh0"></param>
        /// <param name="p_strYBpay"></param>
        /// <returns></returns>
        public long m_lngGetYBpay(string p_strlsh0, out string p_strMedno, out string p_strYBpay)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngGetYBpay(p_strlsh0, out p_strMedno, out p_strYBpay);
        }
        /// <summary>
        /// 删除旧HIS上传信息
        /// </summary>
        /// <param name="p_registerid"></param>
        public long m_lngDelYBInfo(string p_strlsh0)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngDelYBInfo(p_strlsh0);
        }
        #endregion

        #region 更改病人费用核对状态
        /// <summary>
        /// 更改病人费用核对状态
        /// </summary>
        /// <param name="RegisterID"></param>
        /// <param name="CheckStatus"></param>
        public long m_lngUpdatePatientChargeCheckStatus(string RegisterID, string CheckStatus)
        {
            return proxy.Service.m_lngUpdatePatientChargeCheckStatus(RegisterID, CheckStatus);
        }
        #endregion

        #region 调出婴儿未结费用
        /// <summary>
        /// 调出婴儿未结费用
        /// </summary>
        /// <param name="p_strRegisterId">婴儿ID</param>
        /// <param name="p_dtbResult">返回结果</param>
        /// <returns></returns>
        public long m_lngCheckBabyNoPayCharge(string p_strRegisterId, out DataTable p_dtbResult)
        {
            return proxy.Service.m_lngCheckBabyNoPayCharge(p_strRegisterId, out p_dtbResult);
        }
        #endregion

        #region 根据母亲ID获取婴儿信息

        /// <summary>
        /// 根据母亲ID获取婴儿信息
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtbBabyInfo"></param>
        /// <returns></returns>
        public long m_lngGetBabyRegisterId(string p_strRegisterId, out DataTable p_dtbBabyInfo)
        {
            return proxy.Service.m_lngGetBabyRegisterId(p_strRegisterId, out p_dtbBabyInfo);
        }
        #endregion

        #region 获取科室列表
        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetDepts(out DataTable dtbResult)
        {
            return proxy.Service.m_lngGetDepts(out dtbResult);
        }
        #endregion

        #region 茶山医保上传 适应症
        public long m_lngCheckChangeSFLB(Dictionary<string, int> p_gdicItemIDs, out Dictionary<string, string> p_gdicItemIDResult)
        {
            return proxy.Service.m_lngCheckChangeSFLB(p_gdicItemIDs, out p_gdicItemIDResult);
        }


        public long m_lngGetSFLB_ForZjwsy(out DataTable dtResult)
        {
            return proxy.Service.m_lngGetSFLB_ForZjwsy(out dtResult);
        }

        public long m_lngGetPatientChargeSFLB(List<string> m_glstPChargeID,
                                              out Dictionary<string, string> p_gdicItemIDResult,
                                              out Dictionary<string, decimal> p_gdicPatchAmount,
                                              out Dictionary<string, List<string>> p_gdicPatchList)
        {
            return proxy.Service.m_lngGetPatientChargeSFLB(m_glstPChargeID, out p_gdicItemIDResult, out p_gdicPatchAmount, out p_gdicPatchList);
        }



        public long m_lngSetChargeSFLB_Zjwsy(List<clsSFLB_log> m_glstSFLB, string p_strEmpID, string p_strEmpName)
        {
            return proxy.Service.m_lngSetChargeSFLB_Zjwsy(m_glstSFLB, p_strEmpID, p_strEmpName);
        }


        public long m_lngGetPatientPayTypeSFLBBH(string p_strPayType, out string p_strPayNo)
        {
            return proxy.Service.m_lngGetPatientPayTypeSFLBBH(p_strPayType, out p_strPayNo);
        }

        #endregion

        #region 项目适应症
        ///// <summary>
        ///// 项目适应症
        ///// </summary>
        ///// <param name="strRegID"></param>
        ///// <param name="dtResult"></param>
        ///// <returns></returns>
        //public long m_lngGetItemShiying(string strRegID, out DataTable dtResult)
        //{
        //    com.digitalwave.iCare.middletier.HIS.clsCommonQuery objSvc =
        //                                                    (com.digitalwave.iCare.middletier.HIS.clsCommonQuery)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsCommonQuery));

        //    long l = proxy.Service.m_lngGetItemShiying(strRegID, out dtResult);
        //    proxy.Service.Dispose();

        //    return l;
        //}
        #endregion

        #region 通过流水号查询手术或麻醉的补充记账记录
        /// <summary>
        /// 通过流水号查询手术或麻醉的补充记账记录
        /// </summary>
        /// <param name="p_strIpno"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngQueryOpExtraChargeByRgno(string p_strIpno, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = null;

            #region 中间件操作 
            try
            {
                lngRes = proxy.Service.m_lngQueryOpExtraChargeByRgno(p_strIpno, out p_dtResult);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("连接中间件操作异常，" + exp.Message);
            }
            finally
            {
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region 通过流水号查询手术和麻醉信息
        /// <summary>
        /// 通过流水号查询手术和麻醉信息
        /// </summary>
        /// <param name="p_strIpno"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngQuerySMDetailByRgno(string p_strRgno, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = null;

            #region 中间件操作 
            try
            {
                lngRes = proxy.Service.m_lngQuerySMDetailByRgno(p_strRgno, out p_dtResult);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("连接中间件操作异常，" + exp.Message);
            }
            finally
            {
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region 更新手术申请单修改表
        /// <summary>
        /// 更新手术申请单修改表
        /// </summary>
        /// <param name="p_strRgno"></param>
        /// <param name="p_strOpreationName"></param>
        /// <param name="p_strANAName"></param>
        /// <param name="p_strANADate"></param>
        /// <param name="p_strEmployID"></param>
        /// <param name="p_strEmployName"></param>
        /// <returns></returns>
        public long m_lngUpdateRequisitionMR(string p_strRgno, string p_strOpreationName, string p_strANAName, string p_strANADate, string p_strEmployID, string p_strEmployName)
        {
            long lngRes = 0;

            #region 中间件操作 
            try
            {
                lngRes = proxy.Service.m_lngUpdateRequisitionMR(p_strRgno, p_strOpreationName, p_strANAName, p_strANADate, p_strEmployID, p_strEmployName);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("连接中间件操作异常，" + exp.Message);
            }
            finally
            {
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region 检测社保病人社保结算没有成功就不能做HIS结算
        /// <summary>
        /// 检测社保病人社保结算没有成功就不能做HIS结算
        /// </summary>
        /// <param name="p_registerID"></param>
        /// <returns></returns>
        public bool m_blnCheckYBChargeSuccessFull(string p_registerID)
        {
            bool blnSucc = false;

            #region 中间件操作 
            try
            {
                blnSucc = proxy.Service.m_blnCheckYBChargeSuccessFull(p_registerID);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("连接中间件操作异常，" + exp.Message);
            }
            finally
            {
            }
            #endregion
            return blnSucc;
        }
        #endregion

        #region 审核费用

        #region 获取患者费用审核信息
        /// <summary>
        /// 获取患者费用审核信息
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public DataTable GetPatientCheckFee(string registerId)
        {
            return proxy.Service.GetPatientCheckFee(registerId);
        }
        #endregion

        #region 保存患者费用审核信息
        /// <summary>
        /// 保存患者费用审核信息
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        public int SavePatientCheckFee(string registerId, string operId)
        {
            return proxy.Service.SavePatientCheckFee(registerId, operId);
        }
        #endregion

        #region 取消患者费用审核信息
        /// <summary>
        /// 取消患者费用审核信息
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        public int CancelPatientCheckFee(string registerId, string operId)
        {
            return proxy.Service.CancelPatientCheckFee(registerId, operId);
        }
        #endregion

        #endregion

        #region 已发(摆)口服药不允许退费
        /// <summary>
        /// 已发(摆)口服药不允许退费
        /// </summary>
        /// <param name="pchargeId"></param>
        /// <returns></returns>
        public bool IsCanRefundment(string pchargeId)
        {
            return proxy.Service.IsCanRefundment(pchargeId);
        }
        #endregion

        #region 病历角色设置

        #region GetEmpInfo
        /// <summary>
        /// GetEmpInfo
        /// </summary>
        /// <param name="empNo"></param>
        /// <returns></returns>
        public DataTable GetEmpInfo(string empNo)
        {
            return proxy.Service.GetEmpInfo(empNo);
        }
        #endregion

        #region AddCaseRole
        /// <summary>
        /// AddCaseRole
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public int AddCaseRole(EntityLogSetCaseRole vo)
        {
            return proxy.Service.AddCaseRole(vo);
        }
        #endregion

        #region DelCaseRole
        /// <summary>
        /// DelCaseRole
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public int DelCaseRole(EntityLogSetCaseRole vo)
        {
            return proxy.Service.DelCaseRole(vo);
        }
        #endregion

        #region QueryCaseRole
        /// <summary>
        /// QueryCaseRole
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="doctCode"></param>
        /// <returns></returns>
        public DataTable QueryCaseRole(string startDate, string endDate, string doctCode)
        {
            return proxy.Service.QueryCaseRole(startDate, endDate, doctCode);
        }
        #endregion

        #region 查询发票退款原因模板
        /// <summary>
        /// 查询发票退款原因模板
        /// </summary>
        /// <param name="flagId"></param>
        /// <returns></returns>
        public DataTable GetRefundReasonList(int flagId)
        {
            return proxy.Service.GetRefundReasonList(flagId);
        }
        #endregion

        #region 查询发票退款原因
        /// <summary>
        /// 查询发票退款原因
        /// </summary>
        /// <param name="flagId"></param>
        /// <param name="invoNo"></param>
        /// <returns></returns>
        public EntityInvoiceRefundReason GetInvoiceRefundReason(int flagId, string invoNo)
        {
            return proxy.Service.GetInvoiceRefundReason(flagId, invoNo);
        }
        #endregion

        #region 保存发票退款原因
        /// <summary>
        /// 保存发票退款原因
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public int SaveInvoiceRefundReason(EntityInvoiceRefundReason vo)
        {
            return proxy.Service.SaveInvoiceRefundReason(vo);
        }
        #endregion

        #endregion
    }
}
