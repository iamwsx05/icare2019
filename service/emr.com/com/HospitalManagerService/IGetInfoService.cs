using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.emr.HospitalManagerService
{
    /// <summary>
    /// 通过此接口可以获取科室、病区、病房、病床以及病人的信息
    /// </summary>
    public interface IHospitalManagerService
    {
        /// 根据员工ID获取所属科室
        long m_lngGetDeptInfo(string p_strEmployeeID, out DataTable p_dtbValue);
        /// 根据科室ID获取病区
        long m_lngGetAreaInfo(string p_strDeptID, out DataTable p_dtbValue);
        /// 根据病区ID获取病房
        long m_lngGetRoomInfo(string pStrAreaID, out DataTable pDtbValue);
        /// 根据病区（病房）ID获取病床
        long m_lngGetBedInfo(string p_strID, bool p_blnIsRoom, out DataTable p_dtbValue);
        /// 根据病区（病房）ID获取病人列表
        long m_lngGetPatientInfo(string p_strID, bool p_blnIsRoom, out DataTable p_dtbValue);

    }
}
