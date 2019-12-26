using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;  
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll

namespace com.digitalwave.iCare.BIHOrder
{
	/// <summary>
	/// ҽ�������ж�	�߼����Ʋ�
	/// ���ߣ� 
    /// ����ʱ�䣺 2006-4-6
	/// </summary>
    class clsDcl_OrderSaveCheck : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ��ȡҽ��-ȫ��
        public long ConfirmMaxValue(string EMPNO_CHR, string PSW_CHR, double maxvalue, ref DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.ConfirmMaxValue(EMPNO_CHR, PSW_CHR, maxvalue, out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ȷ��Ա��/����
        public long ConfirmPassWord(string EMPNO_CHR, string PSW_CHR, ref DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.ConfirmPassWord(EMPNO_CHR, PSW_CHR, out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
    }
}
