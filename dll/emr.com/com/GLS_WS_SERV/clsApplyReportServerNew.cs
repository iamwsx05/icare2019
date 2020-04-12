using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
//using Oracle.DataAccess.Types;
using weCare.Core.Entity;

namespace com.digitalwave.GLS_WS_SERV
{
    /// <summary>
    /// 申请报告单中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsApplyReportServerNew : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 属性

        //private int m_intApplyType; //保存申请类型ID
        ///// <summary>
        ///// 申请类型ID
        ///// </summary>
        //public int WhereApplyTypeID
        //{
        //    get
        //    {
        //        return m_intApplyType;
        //    }
        //    set
        //    {
        //        m_intApplyType = value;
        //    }
        //}


        //private string m_strDiagnosePart = ""; //保存诊断部位
        ///// <summary>
        ///// 诊断部位
        ///// </summary>
        //public string WhereDiagnosePart
        //{
        //    get
        //    {
        //        return m_strDiagnosePart;
        //    }
        //    set
        //    {
        //        m_strDiagnosePart = value;
        //    }
        //}


        //private string m_strPatientName = "";//保存姓名
        ///// <summary>
        ///// 姓名
        ///// </summary>
        //public string WherePatientName
        //{
        //    get
        //    {
        //        return m_strPatientName;
        //    }
        //    set
        //    {
        //        m_strPatientName = value;
        //    }
        //}


        //private string m_strInPatientNo = ""; //保存住院号
        ///// <summary>
        ///// 住院号
        ///// </summary>
        //public string WhereInPatientNo
        //{
        //    get
        //    {
        //        return m_strInPatientNo;
        //    }
        //    set
        //    {
        //        m_strInPatientNo = value;
        //    }
        //}


        //private string m_strCardNo = "";//保存诊疗卡号
        ///// <summary>
        ///// 诊疗卡号
        ///// </summary>
        //public string WhereCardINo
        //{
        //    get
        //    {
        //        return m_strCardNo;
        //    }
        //    set
        //    {
        //        m_strCardNo = value;
        //    }
        //}


        //private string m_strArea = "";//保存病区
        ///// <summary>
        ///// 病区
        ///// </summary>
        //public string WhereArea
        //{
        //    get
        //    {
        //        return m_strArea;
        //    }
        //    set
        //    {
        //        m_strArea = value;
        //    }
        //}


        //private DateTime m_dtmApplytDate;//保存申请日期-statr
        ///// <summary>
        ///// 保存申请日期-statr
        ///// </summary>
        //public DateTime WhereStartApplytDate
        //{
        //    get
        //    {
        //        return m_dtmApplytDate;
        //    }
        //    set
        //    {
        //        m_dtmApplytDate = value;
        //    }
        //}


        //private DateTime m_strEndApplytDate;//保存申请日期-end
        ///// <summary>
        ///// 保存申请日期-end
        ///// </summary>
        //public DateTime WhereEndApplytDate
        //{
        //    get
        //    {
        //        return m_strEndApplytDate;
        //    }
        //    set
        //    {
        //        m_strEndApplytDate = value;
        //    }
        //}

        #endregion

        /// <summary>
        /// 获取申请单数据表
        /// </summary>
        /// <param name="strErrorMessage">返回查询时错误信息</param>
        /// <param name="dtbResult">返回查询结果</param>
        /// <returns>返回值为True表示查询成功，否则查询失败</returns>
        [AutoComplete]
        public bool m_blnGetApplyReportDataTable(ref string p_strErrorMessage,ref DataTable p_dtbResult, string WhereApplyTypeID, string WhereDiagnosePart, string WherePatientName,
                                                string WhereInPatientNo, string WhereCardINo, string WhereArea, DateTime WhereStartApplytDate, DateTime WhereEndApplytDate)
        {
            //申请单查询语句
            string strSelectText = @"SELECT DISTINCT t1.applyid,
                                                    t1.NAME,
                                                    t1.diagnosepart AS diagnose_part,
                                                    CASE
                                                      WHEN t1.status_int = 0 THEN
                                                       '已申请'
                                                      WHEN t1.status_int = 1 THEN
                                                       '已登记'
                                                      WHEN t1.status_int = 2 THEN
                                                       '已完成'
                                                    END AS apply_status,
                                                    t1.applydate,
                                                    CASE
                                                      WHEN t1.submitted = 0 THEN
                                                       '未提交'
                                                      WHEN t1.submitted = 1 THEN
                                                       '已提交'
                                                    END AS submitted,
                                                    t1.chargedetail,
                                                    CASE
                                                      WHEN t2.status_int = 0 THEN
                                                       '未收费'
                                                      WHEN t2.status_int = 1 THEN
                                                       '已收费'
                                                      WHEN t2.status_int = 2 THEN
                                                       '已退费'
                                                    END AS charge_status,
                                                    t1.finishdate
                                      FROM ar_common_apply t1
                                     INNER JOIN t_opr_attachrelation t2 ON t1.applyid = t2.attachid_vchr
                                     WHERE t1.status_int != 2
                                       AND t1.bihno LIKE ?
                                       AND t1.cardno LIKE ?
                                       AND t1.typeid = ?
                                       AND t1.area LIKE ?
                                       AND t1.diagnosepart LIKE ?
                                       AND t1.NAME LIKE ?
                                       AND t1.applydate BETWEEN ? AND ?";

            clsHRPTableService objHRPServer = new clsHRPTableService(); //创建远程对象
                
            //设置查询参数
            IDataParameter[] objParamterArr = null;
            objHRPServer.CreateDatabaseParameter(8, out objParamterArr);
            objParamterArr[0].Value = WhereInPatientNo.Trim() + "%";
            objParamterArr[1].Value = WhereCardINo.Trim() + "%";
            objParamterArr[2].Value = WhereApplyTypeID;
            objParamterArr[3].Value = WhereArea.Trim() + "%";
            objParamterArr[4].Value = WhereDiagnosePart.Trim() + "%";
            objParamterArr[5].Value = WherePatientName.Trim() + "%";
            objParamterArr[6].Value = WhereStartApplytDate;
            objParamterArr[6].DbType = DbType.Date;
            objParamterArr[7].Value = WhereEndApplytDate;
            objParamterArr[7].DbType = DbType.Date;

            try
            {
                if (objHRPServer.lngGetDataTableWithParameters(strSelectText, ref p_dtbResult, objParamterArr) < 0) return false; //获取申请单数据
            }
            catch (Exception ex)
            {
                p_strErrorMessage = ex.Message; //返回错误信息
                return false;
            }

            return true;
        }

        /// <summary>
        /// 更新申请单状态
        /// </summary>
        /// <returns>返回True表示更新成功，否则更新失败</returns>
        [AutoComplete]
        public bool m_blnUpdateApplyStatus(string p_strApplyStatus, ref DateTime p_dtmFinishDate, string p_strApplyID)
        {
            p_dtmFinishDate = DateTime.Now;
            string strUpdateApplyStatus = @"UPDATE ar_common_apply SET status_int = ?,finishdate =? WHERE applyid = ?";

            clsHRPTableService objHRPServer = new clsHRPTableService(); //创建远程对象
            
            long lngRowAffected = 0;            //影响记录数
            IDataParameter[] objParamterArr = null; 

            objHRPServer.CreateDatabaseParameter(3, out objParamterArr);
            objParamterArr[0].Value = p_strApplyStatus.Trim();
            objParamterArr[1].Value = p_dtmFinishDate;
            objParamterArr[2].Value = p_strApplyID.Trim();

            if (objHRPServer.lngExecuteParameterSQL(strUpdateApplyStatus, ref lngRowAffected, objParamterArr) < 0) //更新申请单状态SQL语句
                return false;
            
            return true;
        }

        /// <summary>
        /// 获取申请单诊断部位列表数据
        /// </summary>
        /// <param name="p_strErrorMessage">返回查询时错误信息</param>
        /// <param name="p_dtbResult">返回查询结果</param>
        /// <returns>返回值为True表示查询成功，否则查询失败</returns>
        [AutoComplete]
        public bool m_blnGetApplyPartList(ref string p_strErrorMessage, ref DataTable p_dtbResult)
        {
            string strQuertText = @"SELECT DISTINCT partname,typeid FROM ar_apply_partlist WHERE deleted=0 ORDER BY typeid,partname";

            clsHRPTableService objHRPServer = new clsHRPTableService();

            try
            {
                if (objHRPServer.DoGetDataTable(strQuertText, ref p_dtbResult) < 0) return false;
            }
            catch (Exception ex)
            {
                p_strErrorMessage = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取申请单病区列表数据
        /// </summary>
        /// <param name="p_strErrorMessage">返回查询时错误信息</param>
        /// <param name="p_dtbResult">返回查询结果</param>
        /// <returns>返回值为p_dtbResult表示查询成功，否则查询失败</returns>
        [AutoComplete]
        public bool m_blnGetPatientAreaList(ref string p_strErrorMessage, ref DataTable p_dtbResult)
        {
            string strQuertText = @"SELECT DISTINCT ctl_content as patient_area FROM ar_content WHERE controlid = 'm_txtArea' ORDER BY ctl_content";

            clsHRPTableService objHRPServer = new clsHRPTableService();

            try
            {
                if (objHRPServer.DoGetDataTable(strQuertText, ref p_dtbResult) < 0) return false;
            }
            catch (Exception ex)
            {
                p_strErrorMessage = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取申请单类型列表数据
        /// </summary>
        /// <param name="p_strErrorMessage">返回查询时错误信息</param>
        /// <param name="p_dtbResult">返回查询结果</param>
        /// <param name="p_strApplyTypeArr"></param>
        /// <returns>返回值为True表示查询成功，否则查询失败</returns>
        [AutoComplete]
        public bool m_blnGetApplyTypeListDataTable(ref string p_strErrorMessage, ref DataTable p_dtbResult, string[] p_strApplyTypeArr )
        {
            string strSelectText = @"SELECT DISTINCT typeid, typetext
                                           FROM ar_apply_typelist ";
            
            string strWhereText = "";
            if (p_strApplyTypeArr.Length <= 0)
            {
                strWhereText = " WHERE null = null ";
            }
            else
            {
                try
                {
                    //组合查询条件
                    foreach (string strApplyTypeID in p_strApplyTypeArr)
                    {
                        strWhereText += (strApplyTypeID.Trim() + ",");
                    }
                    strWhereText = strWhereText.Substring(0, strWhereText.Length - 1);
                    strWhereText = " WHERE typeid IN (" + strWhereText + ")";
                }
                catch (Exception ex)
                {
                    p_strErrorMessage = ex.Message;
                    return false;
                }
            }

            string strOrderByText = @" ORDER BY typeid";

            string strQueryText = strSelectText + strWhereText + strOrderByText;    //连接查询语句

            clsHRPTableService objHRPServer = new clsHRPTableService();             //创建远程对象

            try
            {
                if (objHRPServer.DoGetDataTable(strQueryText, ref p_dtbResult) < 0) return false; //获取申请类型列表数据
            }
            catch (Exception ex)
            {
                p_strErrorMessage = ex.Message; //返回错误信息
                return false;
            }
            return true;
        }
    }
}
