using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.GUI_Base;	
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility;
using com.digitalwave.iCare.common;
using Sybase.DataWindow;
using System.IO;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 药品相关查询 by shaowei.zheng on 2010.06.22
    /// </summary>
    public class clsControlMedicineReport : com.digitalwave.GUI_Base.clsController_Base
    {
        clsDomainControlMedicineReport m_objDomain = null;
        public clsControlMedicineReport()
        {
            m_objDomain = new clsDomainControlMedicineReport();
        }

        public long m_lngGetMedicineList(out DataTable p_dtbResult)
        {
            return m_objDomain.m_lngGetMedicineList(out p_dtbResult);
        }

        public long m_lngImpMedItem(string p_strVal, out DataTable p_dtResult)
        {
            return m_objDomain.m_lngImpMedItem(p_strVal, out p_dtResult);
        }

        public void m_mthExportPtDwToExcel(DataWindowControl p_dwcView)
        {
            if ((p_dwcView != null) && (p_dwcView.RowCount >= 1))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Excel 文档|*.xls";
                dialog.Title = "导出Excel 文档";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.FileName.Trim();
                    if (!path.ToLower().EndsWith(".xls"))
                    {
                        path = path + ".xls";
                    }
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    p_dwcView.SaveAs(path, FileSaveAsType.HtmlTable, true, FileSaveAsEncoding.Utf8);
                }
                dialog.Dispose();
                dialog = null;
            }
        }

        public void m_mthExportPtDwToExcel(DataStore p_dsView)
        {
            if ((p_dsView != null) && (p_dsView.RowCount >= 1))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Excel 文档|*.xls";
                dialog.Title = "导出Excel 文档";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.FileName.Trim();
                    if (!path.ToLower().EndsWith(".xls"))
                    {
                        path = path + ".xls";
                    }
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    p_dsView.SaveAs(path, FileSaveAsType.HtmlTable, true, FileSaveAsEncoding.Utf8);
                }
                dialog.Dispose();
                dialog = null;
            }
        }

        #region 获取药品信息
        /// <summary>
        /// 获取药品信息
        /// </summary>
        /// <param name="p_strVal"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetMedItem(string p_strVal, out DataTable p_dtbResult)
        {
            return m_objDomain.m_lngGetMedItem(p_strVal,out p_dtbResult);
        }
        #endregion

        /// <summary>
        /// 医生用药
        /// </summary>
        /// <param name="time"></param>
        /// <param name="time2"></param>
        /// <param name="str"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngStatDoctUseMed(DateTime time, DateTime time2, string str, out DataTable p_dtbResult)
        {
            return m_objDomain.m_lngStatDoctUseMed(time,time2,str,out p_dtbResult);
        }

        /// <summary>
        /// 库存
        /// </summary>
        /// <param name="str"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngStatMedStock(string str, out DataTable p_dtbResult)
        {
            return m_objDomain.m_lngStatMedStock(str, out p_dtbResult);
        }

        /// <summary>
        /// 出入库
        /// </summary>
        /// <param name="time"></param>
        /// <param name="time2"></param>
        /// <param name="str"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngStatMedInOutStore(DateTime time, DateTime time2, string str, out DataTable p_dtbResult)
        {
            return m_objDomain.m_lngStatMedInOutStore(time, time2, str, out p_dtbResult);
        }

        public decimal ConvertObjToDecimal(object obj)
        {
            try
            {
                if ((obj != null) && (obj.ToString() != ""))
                {
                    return Convert.ToDecimal(obj.ToString());
                }
                return 0M;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return 0M;
            }
        }

 

 

    }
}
