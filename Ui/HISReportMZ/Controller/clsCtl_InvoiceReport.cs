using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
//using CrystalDecisions.CrystalReports.Engine;
using Sybase.DataWindow;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    public class clsCtl_InvoiceReport: com.digitalwave.GUI_Base.clsController_Base
    { 
        public clsCtl_InvoiceReport()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            // 
        }
        /// <summary>
        /// �������
        /// </summary>

        frmRptNOCheckOutInvoice m_objViewerCollect;

        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewerCollect = (frmRptNOCheckOutInvoice)frmMDI_Child_Base_in;
        }

        /// <summary>
        /// ����δ�ս���ܱ���
        /// </summary>
        public void m_intGetNOCheckOutInvoice()
        {
            DataTable p_dtResult = new DataTable();
            string startDate = this.m_objViewerCollect.dtm_start.Value.ToShortDateString() + " 00:00:00";
            string endDate = this.m_objViewerCollect.dtm_end.Value.ToShortDateString() + " 23:59:59";
            this.m_objViewerCollect.dataWindowControl1.Modify("t_start.text='" + this.m_objViewerCollect.dtm_start.Value.ToShortDateString() + "'");
            this.m_objViewerCollect.dataWindowControl1.Modify("t_end.text='" + this.m_objViewerCollect.dtm_end.Value.ToShortDateString() + "'");
            try
            {
                clsPublic.PlayAvi("findFILE.avi", "���ڲ�ѯ���ݣ����Ժ�...");
                long lngRes = (new weCare.Proxy.ProxyReport()).Service.m_lngGetNOCheckOutInvoice(startDate,endDate,out p_dtResult);
                if (lngRes > 0)
                {
                    if (p_dtResult.Rows.Count > 0)
                    {
                        m_objViewerCollect.dataWindowControl1.SetRedrawOff();
                        m_objViewerCollect.dataWindowControl1.Retrieve(p_dtResult);
                        m_objViewerCollect.dataWindowControl1.Sort();
                        m_objViewerCollect.dataWindowControl1.CalculateGroups();
                        m_objViewerCollect.dataWindowControl1.SetRedrawOn();
                        m_objViewerCollect.dataWindowControl1.Refresh();
                    }

                    if (p_dtResult.Rows.Count <= 0)
                    {
                        MessageBox.Show("��Ǹ��û���ҵ���Ӧ����Ŀ��¼!", "��ʾ ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            finally
            {
                clsPublic.CloseAvi();
            }
        }
    }
}