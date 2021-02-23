using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ControlLibrary;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    public class clsCtl_GetSections : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmRptOutNoCharge m_objViewer;
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmRptOutNoCharge)frmMDI_Child_Base_in;
        }
        #endregion
        #region ��ȡ������Ϣ
        public void m_mthGetSections()
        {

            clsColumns_VO[] columArr = new clsColumns_VO[]{
                new clsColumns_VO("���","code_vchr",HorizontalAlignment.Left,50),
                new clsColumns_VO("ƴ����","pycode_chr",HorizontalAlignment.Left,60),
                new clsColumns_VO("��������","deptname_vchr",HorizontalAlignment.Left,130),
                new clsColumns_VO("","deptid_chr",HorizontalAlignment.Left,0)
            };
            this.m_objViewer.m_txtSections.m_strSQL = @"select '%' deptid_chr,
       'ȫԺ' deptname_vchr,
       'qy' pycode_chr,
       '00' code_vchr
  from t_bse_deptdesc
 where rownum = 1
union all
select a.deptid_chr, a.deptname_vchr, a.pycode_chr, a.code_vchr
  from t_bse_deptdesc a
 where a.attributeid = '0000003'
   and a.status_int = 1
   and a.category_int = 0
   and a.inpatientoroutpatient_int = 1
 order by code_vchr";

            this.m_objViewer.m_txtSections.m_mthInitListView(columArr);
        }
        #endregion
    }
}
