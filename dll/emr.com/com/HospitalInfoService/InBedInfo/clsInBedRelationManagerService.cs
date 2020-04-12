using System;
using System.EnterpriseServices;
using System.Data;

using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.InBedInfoManagerService
{
	/// <summary>
	/// 的中间件�?
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsInBedRelationManagerService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strResultXml">返回的结�?/param>
		/// <param name="p_intResultRows">记录的数�?/param>
		/// <returns>
		/// 操作结果�?
		/// 0：失败�?
		/// 1：成功�?
		/// </returns>
		[AutoComplete]
		public long m_lngGetDeptInBedRelationInfo( string p_strDeptID,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 

                string strSQL = @"select inpatient_area_dept.departmentid, inpatient_area_dept.area_id, 
										inpatient_area_dept.begin_date_area_dept, inpatient_room_area.room_id, 
										inpatient_room_area.begin_date_room_area, inpatient_bed_room.bed_id, 
										inpatient_bed_room.begin_date_bed_room
									from inpatient_bed_room right outer join
										inpatient_room_area on 
										inpatient_bed_room.room_id = inpatient_room_area.room_id right outer join
										inpatient_area_dept on 
										inpatient_room_area.area_id = inpatient_area_dept.area_id
									where (inpatient_area_dept.end_date_dept = convert(datetime, 
										'1900-01-01 00:00:00', 102)) and 
										(inpatient_room_area.end_date_room_area = convert(datetime, 
										'1900-01-01 00:00:00', 102) or
										inpatient_room_area.end_date_room_area is null) and 
										(inpatient_bed_room.end_date_bed_room = convert(datetime, 
										'1900-01-01 00:00:00', 102) or
										inpatient_bed_room.end_date_bed_room is null) and 
										(inpatient_area_dept.departmentid = ?)
									order by inpatient_area_dept.departmentid, inpatient_area_dept.area_id, 
										inpatient_room_area.room_id, inpatient_bed_room.bed_id";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID;

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			//返回
			return lngRes;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_strDepartmentID"></param>
		/// <param name="p_strBeginDateAreaDept"></param>
		/// <param name="p_strResultXml"></param>
		/// <param name="p_intResultRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngInitEmployeeInAreaDept( string p_strAreaID,string p_strDepartmentID,string p_strBeginDateAreaDept,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 

                string strSQL = @"select inpatient_area_employee.employee_id, employeebaseinfo.firstname
									from inpatient_area_employee inner join
										employeebaseinfo on 
										inpatient_area_employee.employee_id = employeebaseinfo.employeeid
									where (inpatient_area_employee.area_id = '" + p_strAreaID + @"') and 
										(inpatient_area_employee.departmentid = '" + p_strDepartmentID + @"') and 
										(inpatient_area_employee.begin_date_area_dept = convert(datetime, '" + p_strBeginDateAreaDept + @"', 
										102)) and 
										(inpatient_area_employee.end_date_employee_area = convert(datetime, 
										'1900-01-01 00:00:00', 102)) and (employeebaseinfo.status = 0)";

                lngRes = objTabService.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			//返回
			return lngRes;
		}
	}
}