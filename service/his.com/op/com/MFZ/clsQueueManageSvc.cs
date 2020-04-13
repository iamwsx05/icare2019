using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.iCare.middletier.MFZ
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsQueueManageSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngDoctorRoomStationAll(int p_diagAreaID,int schemeID,out clsMFZDeptVO[] depts,out clsMFZRoomVO[] rooms,out clsMFZWorkStationVO[] stations,out clsMFZDoctorVO[] doctors)
        {
            depts = null;
            rooms = null;
            stations = null;
            doctors = null;

            long lngRes = 0;

            clsTmdDeptSvc deptSvc = new clsTmdDeptSvc();
            lngRes = deptSvc.m_lngFindByDiagnoseAreaID(p_diagAreaID,schemeID,out  depts);
            if (lngRes < 1)
                return 0;

            clsTmdRoomSvc roomSvc = new clsTmdRoomSvc();
            lngRes = roomSvc.m_lngFindByAreaID(p_diagAreaID,schemeID, out rooms);
            if (lngRes < 1)
                return 0;

            clsTmdDoctorSvc doctorSvc = new clsTmdDoctorSvc();
            lngRes = doctorSvc.m_lngFindDoctorsByAreaID(p_diagAreaID,schemeID, out doctors);
            if (lngRes < 1)
                return 0;
           

            clsTmdWorkStationSvc stationSvc = new clsTmdWorkStationSvc();
            lngRes = stationSvc.m_lngFindByAreaID(p_diagAreaID,out stations);
            if (lngRes < 1)
                return 0;
            return 1;
        }
    }
}


