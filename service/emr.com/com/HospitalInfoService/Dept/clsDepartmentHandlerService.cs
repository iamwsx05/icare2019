using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.IO;

namespace com.digitalwave.DepartmentManagerService
{
	/// <summary>
	/// 部门（包括科室、病区（病房）、病床）信息的添加、删除、修改
	/// 以及病人,员工的相关操作.Jacky-2003-6-19
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsDepartmentHandlerService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		#region 定义
	

		#region 部门SQL定义
		// 从DeptBaseInfo中获取指定时间的表单。
//		private const string c_strCheckIDSQL="select DeptID from DeptBaseInfo where rtrim(DeptID) = ? ";
		// 从DeptBaseInfo中获取指定时间的表单。
//		private const string c_strCheckNameSQL=@"select b.DeptName from DeptBaseInfo a,Dept_Desc b where rtrim(b.DeptName) = ?  and b.DeptID=a.DeptID and a.Status=0 and
//						b.ModifyDate=(select Max(ModifyDate) from Dept_Desc where DeptID=a.DeptID)";


		// 从Dept_Desc获取指定表单的最后修改时间。
//		private const string c_strCheckLastModifyRecordSQL= @"select b.ModifyDate from DeptBaseInfo a,Dept_Desc b where rtrim(a.DeptID) = ?  and b.DeptID=a.DeptID and
//						b.ModifyDate=(select Max(ModifyDate) from Dept_Desc where DeptID=a.DeptID)";
		

		// 添加记录到DeptBaseInfo
//		private const string c_strAddNewRecordSQL= @"insert into  DeptBaseInfo(DeptID,CreateDate,Status) 
//				values(?,?,0)";

		// 添加记录到Dept_Desc
//		private const string c_strAddNewRecordContentSQL=@"insert into  Dept_Desc(DeptID,ModifyDate,DeptName,Category,InPatientOrOutPatient,Address,PYCode,ShortNO) 
//				values(?,?,?,?,?,?,?,?)";

		// 添加记录到DeptAndDept
//		private const string c_strAddNewRecordDeptAndDeptSQL= @"insert into  DeptAndDept(DeptID,ParentDeptID,ModifyDate,Levels) 
//				values(?,?,?,?)";
		
		// 修改记录到Dept_Desc(不需要修改DeptBaseInfo)
//		private const string c_strModifyRecordContentSQL=c_strAddNewRecordContentSQL;
//		private const string c_strModifyRecordContentSQL = @"UPDATE Dept_Desc
//SET ModifyDate = ?, DeptName = ?, Category = ?, InPatientOrOutPatient = ?, Address = ?, 
//      PYCode = ?, ShortNO = ?
//WHERE (rtrim(DeptID) = ?)";

		// 设置DeptBaseInfo中删除记录的信息
//		private const string c_strDeleteRecordSQL="Update DeptBaseInfo Set Status=1,DeActivedDate=?,OperatorID=? where rtrim(DeptID)=? and Status=0";		
		/// <summary>
		/// 删除角色
		/// </summary>
//		private const string c_strDeleteRole="Update Role_Definition Set Status=1,DeActivedDate=?,OperatorID=? where rtrim(Role_ID) like ? and Status=0";
		// 检查有没有与下级(病区)的关联.(部门没有直接上级关联)
//		private  string c_strCheckLowerRelatingExistSQL_Dept="select DepartmentID from InPatient_Area_Dept WHERE (End_Date_Dept = (select TO_DATE(null) from dual) OR End_Date_Dept ="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@") AND DepartmentID= ? ";
		
		#endregion 部门SQL定义

		#region 病区SQL定义
		// 从InPatient_Area中获取指定时间的表单。
//		private const string c_strCheckIDSQL_Area="select Area_ID from InPatient_Area where rtrim(Area_ID) = ? ";
		// 从InPatient_Area中获取指定时间的表单。
//		private const string c_strCheckNameSQL_Area=@"select b.Area_Name from InPatient_Area a,InPatient_Area_Desc b where b.Area_Name = ?  and b.Area_ID=a.Area_ID and a. End_Date_Area="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" and
//						b.Begin_Date_Area_Naming=(select Max(Begin_Date_Area_Naming) from InPatient_Area_Desc where Area_ID=a.Area_ID and End_Date_Area_Naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";
		

		// 从InPatient_Area_Desc获取指定表单的最后修改时间。
//		private const string c_strCheckLastModifyRecordSQL_Area= @"select b.Begin_Date_Area_Naming from InPatient_Area a,InPatient_Area_Desc b where rtrim(a.Area_ID) = ?  and b.Area_ID=a.Area_ID and
//						b.Begin_Date_Area_Naming=(select Max(Begin_Date_Area_Naming) from InPatient_Area_Desc where Area_ID=a.Area_ID)";
		

		// 添加记录到InPatient_Area
//		private const string c_strAddNewRecordSQL_Area= @"insert into  InPatient_Area(Area_ID,Begin_Date_Area,End_Date_Area) 
//				values(?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";

		// 添加记录到InPatient_Area_Dept
//		private const string c_strAddNewRecordSQL_Area_Dept= @"insert into  InPatient_Area_Dept(Area_ID,DepartmentID,Begin_Date_Area_Dept,End_Date_Dept) 
//				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";

		// 添加记录到InPatient_Room_Area
//		private const string c_strAddNewRecordSQL_Room_Area= @"insert into  InPatient_Room_Area(Room_ID,Area_ID,Begin_Date_Room_Area,End_Date_Room_Area) 
//				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";

		// 添加记录到InPatient_Bed_Room
//		private const string c_strAddNewRecordSQL_Bed_Room= @"insert into  InPatient_Bed_Room(Bed_ID,Room_ID,Begin_Date_Bed_Room,End_Date_Bed_Room) 
//				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";


		// 添加记录到InPatient_Area_Desc
//		private const string c_strAddNewRecordContentSQL_Area=@"insert into  InPatient_Area_Desc(Area_ID,Begin_Date_Area_Naming,Area_Name,End_Date_Area_Naming) 
//				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";
		
		// 修改记录到InPatient_Area_Desc(添加操作)
//		private const string c_strModifyRecordContentSQL_Area=c_strAddNewRecordContentSQL_Area;
		// 修改记录到InPatient_Area_Desc(删除操作)
//		private const string c_strModifyRecordContentSQL2_Area="Update InPatient_Area_Desc Set End_Date_Area_Naming=? where rtrim(Area_ID)=? and (End_Date_Area_Naming = (select TO_DATE(null) from dual) OR End_Date_Area_Naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+") ";

		// 设置InPatient_Area中删除记录的信息
//		private const string c_strDeleteRecordSQL_Area="Update InPatient_Area Set End_Date_Area=? where rtrim(Area_ID)=? ";
		// 检查有没有与下级(病房)的关联.
//		private const string c_strCheckLowerRelatingExistSQL_Area="select Area_ID from InPatient_Room_Area WHERE (End_Date_Room_Area = (select TO_DATE(null) from dual) OR End_Date_Room_Area ="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+") AND rtrim(Area_ID)= ? ";
		// 更新与上级(部门)的关联状态.
//		private const string c_strDeleteHigherRelatingSQL_Area="Update InPatient_Area_Dept Set End_Date_Dept=? where rtrim(Area_ID)=? ";
		
		#endregion 病区SQL定义

		#region 病房SQL定义
		// 从InPatient_Room中获取指定时间的表单。
//		private const string c_strCheckIDSQL_Room="select * from InPatient_Room where Room_ID = ? ";
		// 从InPatient_Room中获取指定时间的表单。
//		private const string c_strCheckNameSQL_Room=@"select b.Room_Name from InPatient_Room a,InPatient_Room_Desc b where b.Room_Name = ?  and b.Room_ID=a.Room_ID and a. End_Date_Room="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" and
//						b.Begin_Date_Room_Naming=(select Max(Begin_Date_Room_Naming) from InPatient_Room_Desc where Room_ID=a.Room_ID and End_Date_Room_Naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";
	

		// 从InPatient_Room_Desc获取指定表单的最后修改时间。
//		private const string c_strCheckLastModifyRecordSQL_Room= @"select b.Begin_Date_Room_Naming from InPatient_Room a,InPatient_Room_Desc b where a.Room_ID = ?  and b.Room_ID=a.Room_ID and
//						b.Begin_Date_Room_Naming=(select Max(Begin_Date_Room_Naming) from InPatient_Room_Desc where Room_ID=a.Room_ID)";
	
//		private const string c_strGetRoomIDArr_InAreaSQL = @"select Room_ID from InPatient_Room_Area	where Area_ID=?
//						and End_Date_Room_Area = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" ";

		// 添加记录到InPatient_Room
//		private const string c_strAddNewRecordSQL_Room= @"insert into  InPatient_Room(Room_ID,Begin_Date_Room,End_Date_Room) 
//				values(?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";

		// 添加记录到InPatient_Room_Desc
//		private const string c_strAddNewRecordContentSQL_Room=@"insert into  InPatient_Room_Desc(Room_ID,Begin_Date_Room_Naming,Room_Name,End_Date_Room_Naming) 
//				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";
		
		// 修改记录到InPatient_Room_Desc(添加操作)
//		private const string c_strModifyRecordContentSQL_Room=c_strAddNewRecordContentSQL_Room;
		// 修改记录到InPatient_Room_Desc(删除操作)
//		private const string c_strModifyRecordContentSQL2_Room="Update InPatient_Room_Desc Set End_Date_Room_Naming=? where rtrim(Room_ID)=? and (End_Date_Room_Naming = (select TO_DATE(null) from dual) OR End_Date_Room_Naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@") ";

		// 设置InPatient_Room中删除记录的信息
//		private const string c_strDeleteRecordSQL_Room="Update InPatient_Room Set End_Date_Room=? where rtrim(Room_ID)=? ";
		// 检查有没有与下级(病床)的关联.
//		private const string c_strCheckLowerRelatingExistSQL_Room="select Room_ID from InPatient_Bed_Room WHERE (End_Date_Bed_Room = (select TO_DATE(null) from dual) OR End_Date_Bed_Room ="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@") AND Room_ID= ? ";
		// 更新与上级(病区)的关联状态.
//		private const string c_strDeleteHigherRelatingSQL_Room="Update InPatient_Room_Area Set End_Date_Room_Area=? where rtrim(Room_ID)=? ";
		#endregion 病房SQL定义

		#region 病床SQL定义
		// 从InPatient_Bed中获取指定时间的表单。
//		private const string c_strCheckIDSQL_Bed="select * from InPatient_Bed where Bed_ID = ? ";
		// 从InPatient_Bed中获取指定时间的表单。
//		private const string c_strCheckNameSQL_Bed=@"select b.Bed_Name from InPatient_Bed a,InPatient_Bed_Desc b where b.Bed_Name = ?  and b.Bed_ID=a.Bed_ID and a. End_Date_Bed="+clsHRPTableService.s_strGetSQLInvalidDateForma()+@" and
//						b.Begin_Date_Bed_Naming=(select Max(Begin_Date_Bed_Naming) from InPatient_Bed_Desc where Bed_ID=a.Bed_ID and End_Date_Bed_Naming="+clsHRPTableService.s_strGetSQLInvalidDateForma()+@")";
	

		// 从InPatient_Bed_Desc获取指定表单的最后修改时间。
//		private const string c_strCheckLastModifyRecordSQL_Bed= @"select b.Begin_Date_Bed_Naming from InPatient_Bed a,InPatient_Bed_Desc b where a.Bed_ID = ?  and b.Bed_ID=a.Bed_ID and
//						b.Begin_Date_Bed_Naming=(select Max(Begin_Date_Bed_Naming) from InPatient_Bed_Desc where Bed_ID=a.Bed_ID)";
	
		// 添加记录到InPatient_Bed
//		private const string c_strAddNewRecordSQL_Bed= @"insert into  InPatient_Bed(Bed_ID,Begin_Date_Bed,End_Date_Bed) 
//				values(?,?,"+clsHRPTableService.s_strGetSQLInvalidDateForma()+@")";

		// 添加记录到InPatient_Bed_Desc
//		private const string c_strAddNewRecordContentSQL_Bed=@"insert into  InPatient_Bed_Desc(Bed_ID,Begin_Date_Bed_Naming,Bed_Name,End_Date_Bed_Naming) 
//				values(?,?,?,"+clsHRPTableService.s_strGetSQLInvalidDateForma()+@")";
		
		// 修改记录到InPatient_Bed_Desc(添加操作)
//		private const string c_strModifyRecordContentSQL_Bed=c_strAddNewRecordContentSQL_Bed;
		// 修改记录到InPatient_Bed_Desc(删除操作)
//		private const string c_strModifyRecordContentSQL2_Bed="Update InPatient_Bed_Desc Set End_Date_Bed_Naming=? where rtrim(Bed_ID)=? and (End_Date_Bed_Naming IS NULL OR End_Date_Bed_Naming="+clsHRPTableService.s_strGetSQLInvalidDateForma()+@") ";

		// 设置InPatient_Bed中删除记录的信息
//		private const string c_strDeleteRecordSQL_Bed="Update InPatient_Bed Set End_Date_Bed=? where rtrim(Bed_ID)=? ";
		// 更新与上级(病房)的关联状态.(没有直接下级关联)
//		private const string c_strDeleteHigherRelatingSQL_Bed="Update InPatient_Bed_Room Set End_Date_Bed_Room=? where rtrim(Bed_ID)=? ";
		#endregion 病床SQL定义

		#region 病人SQL定义
	
		// 添加记录到PatientBaseInfo
//		private const string c_strAddNewPatientBaseInfoSQL=@"Insert into PatientBaseInfo (InPatientID,PatientID, FirstName ,LastName , IDCard , Sex, Married, Birth,ChargeCategory,
//			PaymentPercent,Homeplace,Nationality,Nation,NativePlace,Occupation,OfficePhone,HomePhone,Mobile,OfficeAddress,HomeAddress,Job,
//			OfficePC,HomePC,EMail,LinkManFirstName,LinkManLastName,LinkManAddress,LinkManPhone,LinkManPC,PatientRelation,FirstDate,IsEmployee,Status)
//			values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,'0') ";
		// 添加记录到PatientBaseInfo(入院)
//		private const string c_strAddNewPatientBaseInfoSQL2=@"Insert into PatientBaseInfo (InPatientID,PatientID,hic_no,FirstName,Sex,Birth,Married,Occupation,vip_code,Homeplace,
//			Nation,Nationality,IDCard,Office_name,Office_district,Office_street,OfficePhone,OfficePC,LinkManFirstName,PatientRelation,
//			LinkMan_district,LinkMan_street,LinkManPhone,LinkManPC,home_district,home_street,HomePhone,HomePC,temp_district,temp_street,
//			temp_tel,temp_zipcode,insurance,admiss_status,visit_type,ChargeCategory,PaymentPercent,Times,Status,NATIVEPLACE)
//			values(?,?,?,?,?,?,?,?,?,?,
//					?,?,?,?,?,?,?,?,?,?,
//					?,?,?,?,?,?,?,?,?,?,
//					?,?,?,?,?,?,?,?,'0',?) ";

		// 修改记录到PatientBaseInfo
//		private const string c_strModifyPatientBaseInfoSQL=@"Update PatientBaseInfo Set FirstName=? ,LastName =? ,  IDCard=? , Sex=?, Married=?, Birth=?,ChargeCategory=?,
//			PaymentPercent=?,Homeplace=?,Nationality=?,Nation=?,NativePlace=?,Occupation=?,OfficePhone=?,HomePhone=?,Mobile=?,OfficeAddress=?,HomeAddress=?,Job=?,
//			OfficePC=?,HomePC=?,EMail=?,LinkManFirstName=?,LinkManLastName=?,LinkManAddress=?,LinkManPhone=?,LinkManPC=?,PatientRelation=?,FirstDate=?,IsEmployee=?,PatientID=? 
//			where InPatientID=? ";
		// 修改记录到PatientBaseInfo（护士方面）
//		private const string c_strModifyPatientBaseInfoSQL2 = @"Update PatientBaseInfo Set
//			Times=?,PatientID=?,hic_no=?,FirstName=? ,Sex=?,Birth=?,Married=?,Occupation=?,vip_code=?,
//			Homeplace=?,Nation=?,Nationality=?,IDCard=?,Office_name=?,Office_district=?,Office_street=?,OfficePhone=?,OfficePC=?,
//			LinkManFirstName=?,PatientRelation=?,LinkMan_district=?,LinkMan_street=?,LinkManPhone=?,LinkManPC=?,
//			home_district=?,home_street=?,HomePhone=?,HomePC=?,
//			temp_district=?,temp_street=?,temp_tel=?,temp_zipcode=?,
//			insurance=?,admiss_status=?,visit_type=?,ChargeCategory=?,PaymentPercent=?，NativePlace=?
//			where InPatientID=? ";
		// 记录病人基本资料的修改标记
//		private const string c_strAddPatientBase_FlagSQL=@"INSERT INTO PatientBaseInfo_ModifyFlag
//      (InPatientID, PatientID, FirstName, IDCard, Sex, Married, Birth, ChargeCategory, 
//      PaymentPercent, Homeplace, Nationality, Nation, NativePlace, Occupation, 
//      OfficePhone, HomePhone, Mobile, OfficeAddress, HomeAddress, OfficePC, HomePC, 
//      EMail, LinkManFirstName, LinkManAddress, LinkManPhone, LinkManPC, 
//      PatientRelation, FirstDate, IsEmployee)
//VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		// 添加记录到InPatientDateInfo
//		private const string c_strAddNewInPatientDateInfoSQL=@"insert into  InPatientDateInfo(InPatientID,InPatientDate,InPatientEndDate) 
//				values(?,?,?)";

		// 删除记录InPatientDateInfo
//		private const string c_strDeleteInPatientDateInfoSQL=@"Update InPatientDateInfo SET InPatientEndDate=?, EndReason=? where InPatientID=? and InPatientDate=? ";
		
		// 添加记录到InDeptInfo
//		private const string c_strAssignPatientToBedSQL=@"insert into  InDeptInfo(InPatientID,InPatientDate,InDeptID,Area_ID,Room_ID,Bed_ID,ModifyDate,InBedEndDate,Begin_Date_Area_Dept,Begin_Date_Room_Area,Begin_Date_Bed_Room) 
//				values(?,?,?,?,?,?,?,?,?,?,?)";

//		private const string c_strAssignPatientToBedSQL=@"
//			declare t1,t2,t3 as DateTime
//									select t1=
//			select Begin_Date_Area_Dept from InPatient_Area_Dept where DepartmentID=? and Area_ID=? and (End_Date_Dept="+clsHRPTableService.s_strGetSQLInvalidDateForma()+@" OR End_Date_Dept IS NULL) 
//			select t2=
//				select Begin_Date_Room_Area from InPatient_Room_Area where Area_ID=? and Room_ID=? and (End_Date_Room_Area="+clsHRPTableService.s_strGetSQLInvalidDateForma()+@" OR End_Date_Room_Area IS NULL)
//			select t3=
//				select Begin_Date_Bed_Room from InPatient_Bed_Room where Room_ID=? and Bed_ID=? and (End_Date_Bed_Room="+clsHRPTableService.s_strGetSQLInvalidDateForma()+@" OR End_Date_Bed_Room IS NULL)
//			insert into  InDeptInfo(InPatientID,InPatientDate,InDeptID,Area_ID,Room_ID,Bed_ID,ModifyDate,InBedEndDate,Begin_Date_Area_Dept,Begin_Date_Room_Area,Begin_Date_Bed_Room) 
//							values(?,?,?,?,?,?,?,"+clsHRPTableService.s_strGetSQLInvalidDateForma()+@",t1,t2,t3)"
		
		/// <summary>
		/// 删除转床前的信息
		/// </summary>
//		private const string c_strDeleteOldInfoWhenPatientTransferBedSQL="Update InDeptInfo Set InBedEndDate=? where InPatientID=? and InPatientDate=? and (InBedEndDate = (select TO_DATE(null) from dual) OR InBedEndDate="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";
		
		/// <summary>
		/// 转床(重新分配病床)
		/// </summary>
//		private const string c_strPatientTransferBedSQL=c_strAssignPatientToBedSQL;


		// 从PatientBaseInfo中获取指定的表单。
//		private const string c_strCheckIDSQL_Patient="select InPatientID from PatientBaseInfo Where InPatientID = ? ";
			#endregion

		#region 员工SQL定义
		// 从EmployeeBaseInfo中获取指定的表单。
//		private const string c_strCheckIDSQL_Employee="select * from EmployeeBaseInfo where rtrim(EmployeeID) = ? ";
		
		// 添加记录到PatientBaseInfo
//		private const string c_strAddNewEmployeeBaseInfoSQL=@"Insert into EmployeeBaseInfo (EmployeeID,BeginDate, FirstName ,LastName , IDCard ,PYCode, Sex, EducationalLevel,Married,TitleOfaTechnicalPost,LanguageAbility, Birth,
//			OfficePhone,HomePhone,Mobile,OfficeAddress,HomeAddress,
//			OfficePC,HomePC,EMail,FirstNameOfAnnouncer,LastNameOfAnnouncer,PhoneOfAnnouncer,Experience,Remark,Status)
//			values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) ";

		// 修改记录到PatientBaseInfo
//		private const string c_strModifyEmployeeBaseInfoSQL=@"Update EmployeeBaseInfo Set BeginDate=?,FirstName=? ,LastName =? ,  IDCard=? ,PYCode=?, Sex=?,EducationalLevel=?, Married=?, TitleOfaTechnicalPost=?,LanguageAbility=?,
//			Birth=?,OfficePhone=?,HomePhone=?,Mobile=?,OfficeAddress=?,HomeAddress=?,
//			OfficePC=?,HomePC=?,EMail=?,FirstNameOfAnnouncer=?,LastNameOfAnnouncer=?,PhoneOfAnnouncer=?,Experience=?,Remark=?,Status=?,DeActiveDate=?,OperatorID=? 
//			where rtrim(EmployeeID)=? ";

//		private const string c_strModifyEmployeeBaseInfoSQL2=@"Update EmployeeBaseInfo Set Status=?,DeActiveDate=?,OperatorID=? 
//			where rtrim(EmployeeID)=? ";


		// 分配员工负责的病区,(添加记录到InPatient_Area_Employee)
//		private const string c_strAssignArea_EmployeeSQL=@"insert into  InPatient_Area_Employee(Employee_ID,Area_ID,DepartmentID,Begin_Date_Area_Dept,Begin_Date_Employee_Area,End_Date_Employee_Area) 
//				values(?,?,?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";
		// 分配员工负责的科室,(添加记录到Dept_Employee)
//		private const string c_strAssignDept_EmployeeSQL=@"insert into  Dept_Employee(EmployeeID,DeptID,ModifyDate,EndDate) 
//				values(?,?,?,?)";
		
		/// <summary>
		/// 删除在修改员工负责的病区之前的信息
		/// </summary>
//		private const string c_strDeleteOldInfoOfArea_EmployeeSQL="Update InPatient_Area_Employee Set End_Date_Employee_Area=?,OperatorID=? where rtrim(Employee_ID)=? and rtrim(Area_ID)=? and (End_Date_Employee_Area = (select TO_DATE(null) from dual) OR End_Date_Employee_Area="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";
		/// <summary>
		/// 删除在修改员工负责的科室之前的信息
		/// </summary>
//		private const string c_strDeleteOldInfoOfDept_EmployeeSQL="Update Dept_Employee Set EndDate=? where rtrim(EmployeeID)=? and rtrim(DeptID)=? and (EndDate = (select TO_DATE(null) from dual) OR EndDate="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";
		
		/// <summary>
		/// 修改员工负责的病区
		/// </summary>
//		private const string c_strModifyArea_EmployeeSQL=c_strAssignArea_EmployeeSQL;
		/// <summary>
		/// 修改员工负责的科室
		/// </summary>
//		private const string c_strModifyDept_EmployeeSQL=c_strAssignDept_EmployeeSQL;

			
//		private const string m_strGetBegin_Date_Area_DeptSQL=@"select Begin_Date_Area_Dept from InPatient_Area_Dept where rtrim(DepartmentID)=? and rtrim(Area_ID)=? and (End_Date_Dept="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+" OR End_Date_Dept = (select TO_DATE(null) from dual)) ";
//		private const string m_strGetBegin_Date_Room_AreaSQL=@"select Begin_Date_Room_Area from InPatient_Room_Area where rtrim(Area_ID)=? and rtrim(Room_ID)=? and (End_Date_Room_Area="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+" OR End_Date_Room_Area = (select TO_DATE(null) from dual)) ";
//		private const string m_strGetBegin_Date_Bed_RoomSQL=@"select Begin_Date_Bed_Room from InPatient_Bed_Room where rtrim(Room_ID)=? and rtrim(Bed_ID)=? and (End_Date_Bed_Room="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+" OR End_Date_Bed_Room = (select TO_DATE(null) from dual)) ";
//		private const string m_strGetInPatientDateSQL=@"select InPatientDate from InPatientDateInfo where InPatientID=? and (InPatientEndDate="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+" OR InPatientEndDate = (select TO_DATE(null) from dual)) ";
		#endregion

		#endregion 定义

		#region 部门
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strCheckString"></param>
		/// <param name="p_strCheckSQL"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngCheckSame(string p_strCheckString,string p_strCheckSQL)
		{
			//检查参数
			if(p_strCheckString==null || p_strCheckString=="")
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strCheckString;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(p_strCheckSQL, ref dtbValue, objDPArr);

                //查看DataTable.Rows.Count
                //如果等于1，表示已经有相同的p_strCheckString。返回值使用Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    lngRes = (long)enmOperationResult.Record_Already_Exist;
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		}

		/// <summary>
		/// 查看是否有相同的部门ID
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckID(string p_strDeptID)
		{
			string c_strCheckIDSQL="select deptid from deptbaseinfo where  deptid  = ? ";
			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngCheckID");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;	

				lngRes= m_lngCheckSame(p_strDeptID,c_strCheckIDSQL);
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
			
		}
		[AutoComplete]
		public long m_lngCheckName(string p_strDeptName)
		{		
			string c_strCheckNameSQL=@"select b.DeptName from DeptBaseInfo a,Dept_Desc b where  b.DeptName  = ?  and b.DeptID=a.DeptID and a.Status=0 and
						b.ModifyDate=(select Max(ModifyDate) from Dept_Desc where DeptID=a.DeptID)";

			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngCheckName");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;	

				lngRes= m_lngCheckSame(p_strDeptName,c_strCheckNameSQL);
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
			
		}
		
		/// <summary>
		/// 保存记录到数据库。添加主表,添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAddNewRecord2DB(clsDept_Desc p_objRecordContent)
		{
			string c_strAddNewRecordSQL= @"insert into  deptbaseinfo(deptid,createdate,status) 
				values(?,?,0)";
			string c_strAddNewRecordContentSQL=@"insert into  dept_desc(deptid,modifydate,deptname,category,inpatientoroutpatient,address,pycode,shortno) 
				values(?,?,?,?,?,?,?,?)";
			string c_strAddNewRecordDeptAndDeptSQL= @"insert into  deptanddept(deptid,parentdeptid,modifydate,levels) 
				values(?,?,?,?)";


			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strDeptID=="" || p_objRecordContent.m_dtmCreateDate==DateTime.MinValue|| p_objRecordContent.m_dtmModifyDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;			
			long lngRes =1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngAddNewRecord2DB");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strDeptID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(8, out objDPArr2);
                objDPArr2[0].Value = p_objRecordContent.m_strDeptID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = p_objRecordContent.m_dtmModifyDate;
                objDPArr2[2].Value = p_objRecordContent.m_strDeptName;
                objDPArr2[3].Value = p_objRecordContent.m_strCategory;
                objDPArr2[4].Value = p_objRecordContent.m_strInPatientOrOutPatient;
                objDPArr2[5].Value = p_objRecordContent.m_strAddress;
                objDPArr2[6].Value = p_objRecordContent.m_strPYCode;
                objDPArr2[7].Value = p_objRecordContent.m_strShortNO;
                for (int i = 0; i < objDPArr2.Length; i++)
                {
                    if (objDPArr2[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strDeptID;
                objDPArr[1].Value = "0000000";
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmModifyDate;
                objDPArr[3].Value = "1";

                //执行SQL
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordDeptAndDeptSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                #region 为新建科室添加角色
                #region sql
                string strAddNewRecordSQL_Role_Definition = @"INSERT INTO Role_Definition (Role_ID, CreateDate, Status) 
					VALUES (?,?,'0')";
                string strAddNewRecordSQL_Role_Info = @"INSERT INTO Role_Info (Role_ID, ModifyDate, Role_Name, Description, Category) 
					VALUES (?,?,?,?,?)";
                string strAddNewRecordSQL_Role_Employee = @"INSERT INTO Role_Employee (Role_ID, EmployeeID, OpenDate, Status)      
					SELECT ?, EmployeeID, ?,'0' FROM Dept_Employee  WHERE DeptID=?";
                string strAddNewRecordSQL_Dept_SF_Operation_Role = @"INSERT INTO Dept_SF_Operation_Role (BaseID, SF_ID, Operation_ID, Role_ID, OpenDate, Status)      
					SELECT ?,a.SF_ID,b.Operation_ID,?,?,'0' FROM SF_Definition a,Operation_Definition b";
                string c_strAddNewRecordSQL_MidtierPrivilegeInfo = @"INSERT INTO MidtierPrivilegeInfo (Role_ID, ClassName, MethodName)  
					SELECT ?,ClassName,MethodName FROM MidTierClassMethod";
                #endregion
                string[] strTmp ={ "001", "002", "003", "004", "005" };
                string[] strCatArr ={ "住院医师", "主治医师", "主任医师", "护士", "护士长" };
                string[] strDesArr ={"用户可以在本部门下做全部工作，不能审核","用户可以在本部门下做全部工作，拥有一级审核","用户可以在本部门下做全部工作，拥有二级审核",
									"用户可以在本部门下做全部工作，不能审核", "用户可以在本部门下做全部工作，拥有一级审核"};
                for (int i = 0; i < strTmp.Length; i++)
                {
                    IDataParameter[] objDPArr3 = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr3);
                    objDPArr3[0].Value = p_objRecordContent.m_strDeptID + strTmp[i];
                    objDPArr3[1].DbType = DbType.DateTime;
                    objDPArr3[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strAddNewRecordSQL_Role_Definition, ref lngEff, objDPArr3);
                    if (lngRes <= 0) return lngRes;

                    IDataParameter[] objDPArr4 = null;
                    objHRPServ.CreateDatabaseParameter(5, out objDPArr4);
                    objDPArr4[0].Value = p_objRecordContent.m_strDeptID + strTmp[i];
                    objDPArr4[1].DbType = DbType.DateTime;
                    objDPArr4[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr4[2].Value = strCatArr[i];
                    objDPArr4[3].Value = strDesArr[i];
                    objDPArr4[4].Value = p_objRecordContent.m_strDeptName;
                    lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strAddNewRecordSQL_Role_Info, ref lngEff, objDPArr4);
                    if (lngRes <= 0) return lngRes;

                    IDataParameter[] objDPArr5 = null;
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr5);
                    objDPArr5[0].Value = p_objRecordContent.m_strDeptID + strTmp[i];
                    objDPArr5[1].DbType = DbType.DateTime;
                    objDPArr5[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr5[2].Value = p_objRecordContent.m_strDeptID;
                    lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strAddNewRecordSQL_Role_Employee, ref lngEff, objDPArr5);
                    if (lngRes <= 0) return lngRes;

                    //				IDataParameter[] objDPArr6;
                    //				objHRPServ.CreateDatabaseParameter(3,out objDPArr6);
                    //				objDPArr6[0].Value=p_objRecordContent.m_strDeptID;
                    //				objDPArr6[1].Value=p_objRecordContent.m_strDeptID+strTmp[i];
                    //				objDPArr6[2].Value=DateTime.Now;
                    //				lngEff=0;
                    //				lngRes = objHRPServ.lngExecuteParameterSQL(strAddNewRecordSQL_Dept_SF_Operation_Role,ref lngEff,objDPArr6);
                    //				if(lngRes<=0)return lngRes;
                    string strSql = @"insert into dept_sf_operation_role (baseid, sf_id, operation_id, role_id, opendate, status)      
					select '" + p_objRecordContent.m_strDeptID + "',a.sf_id,b.operation_id,'" + p_objRecordContent.m_strDeptID + strTmp[i] + "'," + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(DateTime.Now) + ",'0' from sf_definition a,operation_definition b";
                    lngRes = objHRPServ.DoExcute(strSql);
                    if (lngRes <= 0) return lngRes;

                    IDataParameter[] objDPArr7 = null;
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr7);
                    objDPArr7[0].Value = p_objRecordContent.m_strDeptID + strTmp[i];
                    lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL_MidtierPrivilegeInfo, ref lngEff, objDPArr7);
                    if (lngRes <= 0) return lngRes;
                }
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		}

		/// <summary>
		/// 查看当前记录是否最新的记录。
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngCheckLastModifyRecord(clsDept_Desc p_objRecordContent)
		{		
			string c_strCheckLastModifyRecordSQL= @"select b.modifydate from deptbaseinfo a,dept_desc b where  a.deptid  = ?  and b.deptid=a.deptid and
						b.modifydate=(select max(modifydate) from dept_desc where deptid=a.deptid)";

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strDeptID==null|| p_objRecordContent.m_dtmModifyDate==DateTime.MinValue )
				return (long)enmOperationResult.Parameter_Error;
           long lngRes = 0;
           clsHRPTableService objHRPServ = new clsHRPTableService();
           try
           {
               //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngCheckLastModifyRecord");
               //if (lngCheckRes <= 0)
                   //return lngCheckRes;

               //获取IDataParameter数组			
               IDataParameter[] objDPArr;
               objHRPServ.CreateDatabaseParameter(1, out objDPArr);
               objDPArr[0].Value = p_objRecordContent.m_strDeptID;

               //生成DataTable
               DataTable dtbValue = new DataTable();
               //执行查询，填充结果到DataTable
               lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


               //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
               if (lngRes > 0 && dtbValue.Rows.Count == 0)
               {
                   return (long)enmOperationResult.Record_Already_Delete;
               }
               //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
               else if (lngRes > 0 && dtbValue.Rows.Count > 0)
               {
                   //如果相同，返回DB_Succees
                   if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                       return (long)enmOperationResult.DB_Succeed;

                   //否则，返回Record_Already_Modify
                   return (long)enmOperationResult.Record_Already_Modify;
               }
           }
           catch (Exception objEx)
           {

               com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
               bool blnRes = objLogger.LogError(objEx);
           }
           finally
           {
               //objHRPServ.Dispose();

           }
			//返回
			return lngRes;
				
		}

		/// <summary>
		/// 把新修改的内容保存到数据库。更新主表,添加子表.(不需要修改DeptBaseInfo)
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete] 
		public  long m_lngModifyRecord2DB(clsDept_Desc p_objRecordContent,string p_strOldDeptName)
		{
			 string c_strModifyRecordContentSQL = @"update dept_desc
						set modifydate = ?, deptname = ?, category = ?, inpatientoroutpatient = ?, address = ?, 
							pycode = ?, shortno = ?
						where ( deptid  = ?)";
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strDeptID==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngModifyRecord2DB");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(8, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = p_objRecordContent.m_dtmModifyDate;
                objDPArr2[1].Value = p_objRecordContent.m_strDeptName;
                objDPArr2[2].Value = p_objRecordContent.m_strCategory;
                objDPArr2[3].Value = p_objRecordContent.m_strInPatientOrOutPatient;
                objDPArr2[4].Value = p_objRecordContent.m_strAddress;
                objDPArr2[5].Value = p_objRecordContent.m_strPYCode;
                objDPArr2[6].Value = p_objRecordContent.m_strShortNO;
                objDPArr2[7].Value = p_objRecordContent.m_strDeptID;
                for (int i = 0; i < objDPArr2.Length; i++)
                {
                    if (objDPArr2[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }
                //执行SQL	
                long lngEff = 0;
                long lngRes2 = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes2 <= 0)
                    return lngRes2;

                //更新科室名称时,同时更新角色分类
                string strUpdateRoleInfo = @"update role_info set category = '" + p_objRecordContent.m_strDeptName + "' where (category = '" + p_strOldDeptName + "')";
                lngRes= objHRPServ.DoExcute(strUpdateRoleInfo);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;	
		}

		
		/// <summary>
		/// 把记录从数据中“删除”。
		/// 1.首先检查有没有与下级(病区)的关联.
		/// 2.若没有关联,更新部门状态.
		/// 3.若有关联,返回不能删除信息.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容,需要提供m_strDeptID,m_dtmDeActivedDate,m_strDeActivedOperatorID</param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngDeleteRecord2DB(clsDept_Desc p_objRecordContent)
		{
			string c_strCheckLowerRelatingExistSQL_Dept="select departmentid from inpatient_area_dept where (end_date_dept is null or end_date_dept ="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@") and departmentid= ? ";
		    string c_strDeleteRecordSQL="update deptbaseinfo set status=1,deactiveddate=?,operatorid=? where  deptid =? and status=0";		
		    string c_strDeleteRole="update role_definition set status=1,deactiveddate=?,operatorid=? where  role_id  like ? and status=0";

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strDeptID==null|| p_objRecordContent.m_dtmDeActivedDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngDeleteRecord2DB");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //获取IDataParameter数组			
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strDeptID;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLowerRelatingExistSQL_Dept, ref dtbValue, objDPArr);

                if (lngRes <= 0) return lngRes;
                //如果DataTable.Rows.Count不等于0，代表没有权限删除，返回Not_permission                                 
                if (lngRes > 0 && dtbValue.Rows.Count != 0)
                {
                    return (long)enmOperationResult.Not_permission;
                }

                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strDeptID;
                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                //删除角色
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strDeptID + "%";

                lngRes= objHRPServ.lngExecuteParameterSQL(c_strDeleteRole, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
					
		}	
		#endregion 部门

		#region 病区	

		/// <summary>
		/// 查看是否有相同的ID
		/// </summary>
		/// <param name="p_strAreaID"></param>
		/// <returns></returns>
		[AutoComplete] 
		public long m_lngCheckID_Area(string p_strAreaID)
		{
			string c_strCheckIDSQL_Area="select area_id from inpatient_area where  area_id  = ? ";

			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngCheckID_Area");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;	

				lngRes =m_lngCheckSame(p_strAreaID,c_strCheckIDSQL_Area);
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
			
		}
		/// <summary>
		/// 查看是否有相同的Name
		/// </summary>
		/// <param name="p_strAreaName"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckName_Area(string p_strAreaName)
		{
			string c_strCheckNameSQL_Area=@"select b.area_name from inpatient_area a,inpatient_area_desc b where b.area_name = ?  and b.area_id=a.area_id and a. end_date_area="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" and
						b.begin_date_area_naming=(select max(begin_date_area_naming) from inpatient_area_desc where area_id=a.area_id and end_date_area_naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";

			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngCheckName_Area");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;	

				lngRes =m_lngCheckSame(p_strAreaName,c_strCheckNameSQL_Area);

			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
		}		
		
		/// <summary>
		/// 保存记录到数据库。添加主表,添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAddNewRecord2DB_Area(clsArea_Desc p_objRecordContent)
		{
			string c_strAddNewRecordSQL_Area= @"insert into  inpatient_area(area_id,begin_date_area,end_date_area) 
				values(?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";
			string c_strAddNewRecordContentSQL_Area=@"insert into  inpatient_area_desc(area_id,begin_date_area_naming,area_name,end_date_area_naming) 
				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";
			string c_strAddNewRecordSQL_Area_Dept= @"insert into  inpatient_area_dept(area_id,departmentid,begin_date_area_dept,end_date_dept) 
				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";



			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strArea_ID==""|| p_objRecordContent.m_dtmBegin_Date_Area_Naming==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngAddNewRecord2DB_Area");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strArea_ID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmBegin_Date_Area_Naming;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL_Area, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
                objDPArr2[0].Value = p_objRecordContent.m_strArea_ID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = p_objRecordContent.m_dtmBegin_Date_Area_Naming;
                objDPArr2[2].Value = p_objRecordContent.m_strArea_Name;
                for (int i = 0; i < objDPArr2.Length; i++)
                {
                    if (objDPArr2[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }
                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL_Area, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                //获取IDataParameter数组
                IDataParameter[] objDPArr3 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr3);
                objDPArr3[0].Value = p_objRecordContent.m_strArea_ID;
                objDPArr3[1].Value = p_objRecordContent.m_strParentDeptID;
                objDPArr3[0].DbType = DbType.DateTime;
                objDPArr3[2].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL_Area_Dept, ref lngEff, objDPArr3);
                if (lngRes <= 0) return lngRes;

                #region 默认地添加一个新病房
                string strNewRoomID = "";
                lngRes = m_lngGetNewID_Room(out strNewRoomID);
                if (lngRes <= 0) return lngRes;

                clsRoom_Desc objRoom_Desc = new clsRoom_Desc();
                objRoom_Desc.m_strRoom_ID = strNewRoomID;
                objRoom_Desc.m_strRoom_Name = strNewRoomID;
                objRoom_Desc.m_strParentAreaID = p_objRecordContent.m_strArea_ID;
                objRoom_Desc.m_dtmBegin_Date_Room_Naming = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                lngRes = m_lngAddNewRecord2DB_Room( objRoom_Desc);
                #endregion 默认地添加一个新病房

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		}

		/// <summary>
		/// 查看当前记录是否最新的记录。
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete] 
		public  long m_lngCheckLastModifyRecord_Area(clsArea_Desc p_objRecordContent)
		{	
			string c_strCheckLastModifyRecordSQL_Area= @"select b.begin_date_area_naming from inpatient_area a,inpatient_area_desc b where  a.area_id  = ?  and b.area_id=a.area_id and
						b.begin_date_area_naming=(select max(begin_date_area_naming) from inpatient_area_desc where area_id=a.area_id)";

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strArea_ID==null || p_objRecordContent.m_dtmBegin_Date_Area_Naming==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngCheckLastModifyRecord_Area");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //获取IDataParameter数组			
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strArea_ID;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL_Area, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    //如果相同，返回DB_Succees
                    if (p_objRecordContent.m_dtmBegin_Date_Area_Naming == DateTime.Parse(dtbValue.Rows[0]["BEGIN_DATE_AREA_NAMING"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    return (long)enmOperationResult.Record_Already_Modify;
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
				
		}

		/// <summary>
		/// 把新修改的内容保存到数据库。子表删除旧记录,添加一新记录.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngModifyRecord2DB_Area(clsArea_Desc p_objRecordContent)
		{
			string c_strModifyRecordContentSQL2_Area="update inpatient_area_desc set end_date_area_naming=? where  area_id =? and (end_date_area_naming is null or end_date_area_naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+") ";
			string c_strModifyRecordContentSQL_Area=@"insert into  inpatient_area_desc(area_id,begin_date_area_naming,area_name,end_date_area_naming) 
				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strArea_ID==null|| p_objRecordContent.m_dtmBegin_Date_Area_Naming==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngModifyRecord2DB_Area");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr2;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = p_objRecordContent.m_dtmBegin_Date_Area_Naming;//删除名称时删除时间与上面新建时间相同
                objDPArr2[1].Value = p_objRecordContent.m_strArea_ID;

                //执行SQL	
                long lngEff = 0;
                long lngRes2 = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL2_Area, ref lngEff, objDPArr2);
                if (lngRes2 <= 0) return lngRes2;


                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strArea_ID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmBegin_Date_Area_Naming;
                objDPArr[2].Value = p_objRecordContent.m_strArea_Name;
                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }
                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL_Area, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			

		}

		/// <summary>
		/// 把记录从数据中“删除”。
		/// 1.首先检查有没有与下级(病房)的关联.
		/// 2.若没有关联,更新病区状态,并更新与上级(部门)的关联状态.
		/// 3.若有关联,返回不能删除信息.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容,需要提供m_strArea_ID,m_dtmEnd_Date_Area</param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngDeleteRecord2DB_Area(clsArea_Desc p_objRecordContent)
		{
			string c_strCheckLowerRelatingExistSQL_Area="select area_id from inpatient_room_area where (end_date_room_area is null or end_date_room_area ="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+") and  area_id = ? ";
		    string c_strDeleteRecordSQL_Area="update inpatient_area set end_date_area=? where  area_id =? ";
			string c_strDeleteHigherRelatingSQL_Area="update inpatient_area_dept set end_date_dept=? where  area_id =? ";

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strArea_ID==null || p_objRecordContent.m_dtmEnd_Date_Area==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngDeleteRecord2DB_Area");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //获取IDataParameter数组			
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strArea_ID;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLowerRelatingExistSQL_Area, ref dtbValue, objDPArr);

                if (lngRes <= 0) return lngRes;
                //如果DataTable.Rows.Count不等于0，代表没有权限删除，返回Not_permission                                 
                if (lngRes > 0 && dtbValue.Rows.Count != 0)
                {
                    return (long)enmOperationResult.Not_permission;
                }

                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmEnd_Date_Area;
                objDPArr[1].Value = p_objRecordContent.m_strArea_ID;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL_Area, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmEnd_Date_Area;
                objDPArr[1].Value = p_objRecordContent.m_strArea_ID;

                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteHigherRelatingSQL_Area, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		
		}		
		#endregion 病区

		#region 病房	

		/// <summary>
		/// 产生最新的病房编号
		/// </summary>
		/// <param name="p_strDeptID"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngGetNewID_Room(out string p_strRoomID)
		{
			long lngRes = 0;
			p_strRoomID=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.lngGenerateID(3, "Room_ID", "InPatient_Room", out p_strRoomID);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		}
		
		
		/// <summary>
		/// 查看是否有相同的ID
		/// </summary>
		/// <param name="p_strDeptID"></param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngCheckID_Room(string p_strRoomID)
		{
			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngCheckID_Room");
                //if (lngCheckRes <= 0)
					//return lngCheckRes;	
			string c_strCheckIDSQL_Room = @"select room_id, begin_date_room, end_date_room
  from inpatient_room
 where room_id = ? ";

				lngRes= m_lngCheckSame(p_strRoomID,c_strCheckIDSQL_Room);
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strRoomName"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckName_Room(string p_strRoomName)
		{
			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngCheckName_Room");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;	
				string c_strCheckNameSQL_Room=@"select b.room_name from inpatient_room a,inpatient_room_desc b where b.room_name = ?  and b.room_id=a.room_id and a. end_date_room="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" and
						b.begin_date_room_naming=(select max(begin_date_room_naming) from inpatient_room_desc where room_id=a.room_id and end_date_room_naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";

				lngRes= m_lngCheckSame(p_strRoomName,c_strCheckNameSQL_Room);

			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
		}		
		
		/// <summary>
		/// 保存记录到数据库。添加主表,添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAddNewRecord2DB_Room(clsRoom_Desc p_objRecordContent)
		{
				string c_strAddNewRecordSQL_Room= @"insert into  inpatient_room(room_id,begin_date_room,end_date_room) 
				values(?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";
				string c_strAddNewRecordContentSQL_Room=@"insert into  inpatient_room_desc(room_id,begin_date_room_naming,room_name,end_date_room_naming) 
				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";
				string c_strAddNewRecordSQL_Room_Area= @"insert into  inpatient_room_area(room_id,area_id,begin_date_room_area,end_date_room_area) 
				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";

			
			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strRoom_ID=="" || p_objRecordContent.m_dtmBegin_Date_Room_Naming==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngAddNewRecord2DB_Room");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRoom_ID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmBegin_Date_Room_Naming;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL_Room, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
                objDPArr2[0].Value = p_objRecordContent.m_strRoom_ID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = p_objRecordContent.m_dtmBegin_Date_Room_Naming;
                objDPArr2[2].Value = p_objRecordContent.m_strRoom_Name;
                for (int i = 0; i < objDPArr2.Length; i++)
                {
                    if (objDPArr2[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }
                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL_Room, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                //获取IDataParameter数组
                IDataParameter[] objDPArr3;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr3);
                objDPArr3[0].Value = p_objRecordContent.m_strRoom_ID;
                objDPArr3[1].Value = p_objRecordContent.m_strParentAreaID;
                objDPArr3[0].DbType = DbType.DateTime;
                objDPArr3[2].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL_Room_Area, ref lngEff, objDPArr3);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		}

		/// <summary>
		/// 查看当前记录是否最新的记录。
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngCheckLastModifyRecord_Room(clsRoom_Desc p_objRecordContent)
		{	
			string c_strCheckLastModifyRecordSQL_Room= @"select b.begin_date_room_naming from inpatient_room a,inpatient_room_desc b where a.room_id = ?  and b.room_id=a.room_id and
						b.begin_date_room_naming=(select max(begin_date_room_naming) from inpatient_room_desc where room_id=a.room_id)";

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strRoom_ID==null || p_objRecordContent.m_dtmBegin_Date_Room_Naming==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngCheckLastModifyRecord_Room");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //获取IDataParameter数组			
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRoom_ID;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL_Room, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    //如果相同，返回DB_Succees
                    if (p_objRecordContent.m_dtmBegin_Date_Room_Naming == DateTime.Parse(dtbValue.Rows[0]["BEGIN_DATE_ROOM_NAMING"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    return (long)enmOperationResult.Record_Already_Modify;
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		}

		/// <summary>
		/// 把新修改的内容保存到数据库。子表删除旧记录,添加一新记录.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngModifyRecord2DB_Room(clsRoom_Desc p_objRecordContent)
		{
			string c_strModifyRecordContentSQL2_Room="update inpatient_room_desc set end_date_room_naming=? where  room_id =? and (end_date_room_naming is null or end_date_room_naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@") ";

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strRoom_ID==null || p_objRecordContent.m_dtmBegin_Date_Room_Naming==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngModifyRecord2DB_Room");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = p_objRecordContent.m_dtmBegin_Date_Room_Naming;//删除名称时删除时间与上面新建时间相同
                objDPArr2[1].Value = p_objRecordContent.m_strRoom_ID;

                //执行SQL
                long lngEff = 0;
                long lngRes2 = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL2_Room, ref lngEff, objDPArr2);
                if (lngRes2 <= 0) return lngRes2;


                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRoom_ID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmBegin_Date_Room_Naming;
                objDPArr[2].Value = p_objRecordContent.m_strRoom_Name;
                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }
                //执行SQL
                string c_strModifyRecordContentSQL_Room = @"insert into  inpatient_room_desc(room_id,begin_date_room_naming,room_name,end_date_room_naming) 
				values(?,?,?," + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")";


                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL_Room, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
			

		}

		/// <summary>
		/// 把记录从数据中“删除”。
		/// 1.首先检查有没有与下级(病床)的关联.
		/// 2.若没有关联,更新病房状态,并更新与上级(病区)的关联状态.
		/// 3.若有关联,返回不能删除信息.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容,需要提供m_strRoom_ID,m_dtmEnd_Date_Room</param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
        [AutoComplete]
		public  long m_lngDeleteRecord2DB_Room(clsRoom_Desc p_objRecordContent)
		{
			string c_strCheckLowerRelatingExistSQL_Room="select room_id from inpatient_bed_room where (end_date_bed_room is null or end_date_bed_room ="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@") and room_id= ? ";
			string c_strDeleteRecordSQL_Room="update inpatient_room set end_date_room=? where  room_id =? ";
			string c_strDeleteHigherRelatingSQL_Room="update inpatient_room_area set end_date_room_area=? where  room_id =? ";

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strRoom_ID==null || p_objRecordContent.m_dtmEnd_Date_Room==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngDeleteRecord2DB_Room");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //获取IDataParameter数组			
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRoom_ID;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLowerRelatingExistSQL_Room, ref dtbValue, objDPArr);

                if (lngRes <= 0) return lngRes;
                //如果DataTable.Rows.Count不等于0，代表没有权限删除，返回Not_permission                                 
                if (lngRes > 0 && dtbValue.Rows.Count != 0)
                {
                    return (long)enmOperationResult.Not_permission;
                }

                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmEnd_Date_Room;
                objDPArr[1].Value = p_objRecordContent.m_strRoom_ID;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL_Room, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmEnd_Date_Room;
                objDPArr[1].Value = p_objRecordContent.m_strRoom_ID;

                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteHigherRelatingSQL_Room, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

			
		}		
		#endregion 病房

		#region 病床
		/// <summary>
		/// 查看是否有相同的ID
		/// </summary>
		/// <param name="p_strBedID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckID_Bed(string p_strBedID)
		{
			string c_strCheckIDSQL_Bed = @"select bed_id, begin_date_bed, end_date_bed
  from inpatient_bed
 where bed_id = ?";

			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngCheckID_Bed");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;	

				lngRes= m_lngCheckSame(p_strBedID,c_strCheckIDSQL_Bed);
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
			
		}
		[AutoComplete]
		public long m_lngCheckName_Bed(string p_strBedName)
		{
			string c_strCheckNameSQL_Bed=@"select b.bed_name from inpatient_bed a,inpatient_bed_desc b where b.bed_name = ?  and b.bed_id=a.bed_id and a. end_date_bed="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" and
						b.begin_date_bed_naming=(select max(begin_date_bed_naming) from inpatient_bed_desc where bed_id=a.bed_id and end_date_bed_naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";

			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngCheckName_Bed");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;	

				lngRes= m_lngCheckSame(p_strBedName,c_strCheckNameSQL_Bed);
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
			
		}	
		
		/// <summary>
		/// 保存记录到数据库。添加主表,添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAddNewRecord2DB_Bed(
			clsBed_Desc p_objRecordContent,out string p_strBedID)
		{
				string c_strAddNewRecordSQL_Bed= @"insert into  inpatient_bed(bed_id,begin_date_bed,end_date_bed) 
				values(?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";
		string c_strAddNewRecordContentSQL_Bed=@"insert into  inpatient_bed_desc(bed_id,begin_date_bed_naming,bed_name,end_date_bed_naming) 
				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";
				string c_strAddNewRecordSQL_Bed_Room= @"insert into  inpatient_bed_room(bed_id,room_id,begin_date_bed_room,end_date_bed_room) 
				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+")";

			p_strBedID = "";

			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strBed_ID==""|| p_objRecordContent.m_dtmBegin_Date_Bed_Naming==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngAddNewRecord2DB_Bed");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                p_strBedID = m_strGetBedID();
                if (p_strBedID == "")
                    return -1;

                //获取IDataParameter数组
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strBedID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmBegin_Date_Bed_Naming;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL_Bed, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;


                IDataParameter[] objDPArr2;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
                objDPArr2[0].Value = p_strBedID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = p_objRecordContent.m_dtmBegin_Date_Bed_Naming;
                objDPArr2[2].Value = p_objRecordContent.m_strBed_Name;
                for (int i = 0; i < objDPArr2.Length; i++)
                {
                    if (objDPArr2[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }
                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL_Bed, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;


                //获取IDataParameter数组
                IDataParameter[] objDPArr3;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr3);
                objDPArr3[0].Value = p_strBedID;

                string[] strRoomIDArr = null;
                m_lngGetRoomIDArr_InArea(p_objRecordContent.m_strParentAreaID_OfParentRoom, out strRoomIDArr);
                if (strRoomIDArr != null && strRoomIDArr.Length > 0)
                    p_objRecordContent.m_strParentRoomID = strRoomIDArr[0];
                else
                    return (long)enmOperationResult.DB_Fail;
                objDPArr3[1].Value = p_objRecordContent.m_strParentRoomID;
                objDPArr3[0].DbType = DbType.DateTime;
                objDPArr3[2].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL_Bed_Room, ref lngEff, objDPArr3);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
		}
		[AutoComplete]
		private long m_lngGetRoomIDArr_InArea(string p_strAreaID,out string[] p_strRoomIDArr)
		{		
			string c_strGetRoomIDArr_InAreaSQL = @"select room_id from inpatient_room_area	where area_id='"+  p_strAreaID+
						"' and end_date_room_area =  "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat();

			p_strRoomIDArr=null;
			if(p_strAreaID==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //执行SQL	
                DataTable dtbResult = null;
                lngRes = objHRPServ.DoGetDataTable(c_strGetRoomIDArr_InAreaSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_strRoomIDArr = new String[dtbResult.Rows.Count];
                    for (int j = 0; j < dtbResult.Rows.Count; j++)
                        p_strRoomIDArr[j] = dtbResult.Rows[j]["ROOM_ID"].ToString();
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
			;
		}


		/// <summary>
		/// 查看当前记录是否最新的记录。
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngCheckLastModifyRecord_Bed(clsBed_Desc p_objRecordContent)
		{	
			string c_strCheckLastModifyRecordSQL_Bed= @"select b.begin_date_bed_naming from inpatient_bed a,inpatient_bed_desc b where a.bed_id = ?  and b.bed_id=a.bed_id and
						b.begin_date_bed_naming=(select max(begin_date_bed_naming) from inpatient_bed_desc where bed_id=a.bed_id)";

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strBed_ID==null || p_objRecordContent.m_dtmBegin_Date_Bed_Naming==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngCheckLastModifyRecord_Bed");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strBed_ID;

                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL_Bed, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    //如果相同，返回DB_Succees
                    if (p_objRecordContent.m_dtmBegin_Date_Bed_Naming == DateTime.Parse(dtbValue.Rows[0]["BEGIN_DATE_BED_NAMING"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    return (long)enmOperationResult.Record_Already_Modify;
                }
                return lngRes;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
		}

		/// <summary>
		/// 把新修改的内容保存到数据库。子表删除旧记录,添加一新记录.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngModifyRecord2DB_Bed(clsBed_Desc p_objRecordContent)
		{
			string c_strModifyRecordContentSQL2_Bed="update inpatient_bed_desc set end_date_bed_naming=? where r bed_id =? and (end_date_bed_naming is null or end_date_bed_naming="+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@") ";

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strBed_ID==null || p_objRecordContent.m_dtmBegin_Date_Bed_Naming==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngModifyRecord2DB_Bed");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr2;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = p_objRecordContent.m_dtmBegin_Date_Bed_Naming;//删除名称时删除时间与上面新建时间相同
                objDPArr2[1].Value = p_objRecordContent.m_strBed_ID;

                //执行SQL	
                long lngEff = 0;
                long lngRes2 = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL2_Bed, ref lngEff, objDPArr2);
                if (lngRes2 <= 0) return lngRes2;

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strBed_ID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmBegin_Date_Bed_Naming;
                objDPArr[2].Value = p_objRecordContent.m_strBed_Name;
                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }
                //执行SQL
                string c_strModifyRecordContentSQL_Bed = @"insert into  inpatient_bed_desc(bed_id,begin_date_bed_naming,bed_name,end_date_bed_naming) 
				values(?,?,?," + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL_Bed, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
			

		}

		/// <summary>
		/// 把记录从数据中“删除”。
		/// 更新病床状态,并更新与上级(病房)的关联状态.
		/// 
		/// </summary>
		/// <param name="p_objRecordContent">记录内容,需要提供m_strBed_ID,m_dtmEnd_Date_Bed</param>
		/// <param name="m_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngDeleteRecord2DB_Bed(clsBed_Desc p_objRecordContent)
		{
			string c_strDeleteRecordSQL_Bed="update inpatient_bed set end_date_bed=? where  bed_id =? ";
			string c_strDeleteHigherRelatingSQL_Bed="update inpatient_bed_room set end_date_bed_room=? where  bed_id =? ";

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strBed_ID==null || p_objRecordContent.m_dtmEnd_Date_Bed==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngDeleteRecord2DB_Bed");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //获取IDataParameter数组
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmEnd_Date_Bed;
                objDPArr[1].Value = p_objRecordContent.m_strBed_ID;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL_Bed, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmEnd_Date_Bed;
                objDPArr[1].Value = p_objRecordContent.m_strBed_ID;

                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteHigherRelatingSQL_Bed, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;	
			
		}
		[AutoComplete]
		private string m_strGetBedID()
		{
			string strSql = @"select max(bed_id) + 1 as bed_id
				from inpatient_bed";
			string strRes ="";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtResult = new DataTable();
                long lngRes = objHRPServ.DoGetDataTable(strSql, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count == 1)
                {
                    string strBedID = dtResult.Rows[0][0].ToString().Trim();
                    if (strBedID == "")
                        return "0001";
                    else
                        return strBedID.PadLeft(4, '0');
                }
                return "";
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			
            //返回
			return strRes;
			
		}
		#endregion 病床

		#region 病人

		/// <summary>
		/// 查看是否有相同的InPatientID
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckID_Patient(string p_strInPatientID)
		{
			string c_strCheckIDSQL_Patient="select inpatientid from patientbaseinfo where  inpatientid  = ? ";
			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngCheckID_Patient");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;	

				lngRes= m_lngCheckSame(p_strInPatientID,c_strCheckIDSQL_Patient);
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
			
		}

		#region 添加病人基本信息
		/*操作表PatientBaseInfo ,添加该表中各个字段 (除Deactivated_Date, De_EmployeeID之外) 的内容.
		 *  输入参数PatientBaseInfo类对象
		 */
		/// <summary>
		/// 添加病人基本信息
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAddNewPatientBaseInfo(clsPatientBaseInfo p_objRecordContent)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objRecordContent.m_objPeopleInfo==null || p_objRecordContent.m_objPeopleInfo.m_DtmBirth==DateTime.MinValue || p_objRecordContent.m_objPeopleInfo.m_DtmFirstDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngAddNewPatientBaseInfo");
                //			if(lngCheckRes <= 0)
                //				//return lngCheckRes;	

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(32, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].Value = p_objRecordContent.m_strPatientID;
                objDPArr[2].Value = p_objRecordContent.m_objPeopleInfo.m_StrFirstName;
                objDPArr[3].Value = p_objRecordContent.m_objPeopleInfo.m_StrLastName;
                objDPArr[4].Value = p_objRecordContent.m_objPeopleInfo.m_StrIDCard;
                objDPArr[5].Value = p_objRecordContent.m_objPeopleInfo.m_StrSex;
                objDPArr[6].Value = p_objRecordContent.m_objPeopleInfo.m_StrMarried;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = p_objRecordContent.m_objPeopleInfo.m_DtmBirth;
                objDPArr[8].Value = p_objRecordContent.m_objPeopleInfo.m_StrChargeCategory;
                objDPArr[9].Value = p_objRecordContent.m_objPeopleInfo.m_StrPaymentPercent;
                objDPArr[10].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomeplace;
                objDPArr[11].Value = p_objRecordContent.m_objPeopleInfo.m_StrNationality;
                objDPArr[12].Value = p_objRecordContent.m_objPeopleInfo.m_StrNation;
                objDPArr[13].Value = p_objRecordContent.m_objPeopleInfo.m_StrNativePlace;
                objDPArr[14].Value = p_objRecordContent.m_objPeopleInfo.m_StrOccupation;
                objDPArr[15].Value = p_objRecordContent.m_objPeopleInfo.m_StrOfficePhone;
                objDPArr[16].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomePhone;
                objDPArr[17].Value = p_objRecordContent.m_objPeopleInfo.m_StrMobile;
                objDPArr[18].Value = p_objRecordContent.m_objPeopleInfo.m_StrOfficeAddress;
                objDPArr[19].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomeAddress;
                objDPArr[20].Value = p_objRecordContent.m_objPeopleInfo.m_StrJob;
                objDPArr[21].Value = p_objRecordContent.m_objPeopleInfo.m_StrOfficePC;
                objDPArr[22].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomePC;
                objDPArr[23].Value = p_objRecordContent.m_objPeopleInfo.m_StrEMail;
                objDPArr[24].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManFirstName;
                objDPArr[25].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManLastName;
                objDPArr[26].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManAddress;
                objDPArr[27].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManPhone;
                objDPArr[28].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManPC;
                objDPArr[29].Value = p_objRecordContent.m_objPeopleInfo.m_StrPatientRelation;
                objDPArr[30].DbType = DbType.DateTime;
                objDPArr[30].Value = p_objRecordContent.m_objPeopleInfo.m_DtmFirstDate;
                objDPArr[31].Value = p_objRecordContent.m_objPeopleInfo.m_BlnIsEmployee == true ? "1" : "0";

                //			for(int i=0;i<objDPArr.Length;i++)
                //			{
                //				if(objDPArr[i].Value==null)
                //					return (long)enmOperationResult.Parameter_Error;
                //			}
                //执行SQL	
                long lngEff = 0;
                string c_strAddNewPatientBaseInfoSQL = @"insert into patientbaseinfo (inpatientid,patientid, firstname ,lastname , idcard , sex, married, birth,chargecategory,
				paymentpercent,homeplace,nationality,nation,nativeplace,occupation,officephone,homephone,mobile,officeaddress,homeaddress,job,
				officepc,homepc,email,linkmanfirstname,linkmanlastname,linkmanaddress,linkmanphone,linkmanpc,patientrelation,firstdate,isemployee,status)
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,'0') ";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewPatientBaseInfoSQL, ref lngEff, objDPArr);
                return lngRes;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}

		/// <summary>
		/// 添加病人基本信息 ( 2004-07-12增加，病人入院时用 －－HB）
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAddNewPatientBaseInfo2(clsPatientBaseInfo p_objRecordContent)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objRecordContent.m_objPeopleInfo==null || p_objRecordContent.m_objPeopleInfo.m_DtmBirth==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //	//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngAddNewPatientBaseInfo2");
                //			if(lngCheckRes <= 0)
                //				//return lngCheckRes;	

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(39, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;//如果住院号放在最后一个会出现乱码！估计是Oracle驱动的问题
                objDPArr[1].Value = p_objRecordContent.m_strPatientID;
                objDPArr[2].Value = p_objRecordContent.m_objPeopleInfo.m_Strhic_no;
                objDPArr[3].Value = p_objRecordContent.m_objPeopleInfo.m_StrFirstName;
                objDPArr[4].Value = p_objRecordContent.m_objPeopleInfo.m_StrSex;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objRecordContent.m_objPeopleInfo.m_DtmBirth;
                objDPArr[6].Value = p_objRecordContent.m_objPeopleInfo.m_StrMarried;
                objDPArr[7].Value = p_objRecordContent.m_objPeopleInfo.m_StrOccupation;
                objDPArr[8].Value = p_objRecordContent.m_objPeopleInfo.m_Strvip_code;
                objDPArr[9].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomeplace;
                objDPArr[10].Value = p_objRecordContent.m_objPeopleInfo.m_StrNation;
                objDPArr[11].Value = p_objRecordContent.m_objPeopleInfo.m_StrNationality;
                objDPArr[12].Value = p_objRecordContent.m_objPeopleInfo.m_StrIDCard;
                objDPArr[13].Value = p_objRecordContent.m_objPeopleInfo.m_StrOffice_name;
                objDPArr[14].Value = p_objRecordContent.m_objPeopleInfo.m_StrOffice_district;
                objDPArr[15].Value = p_objRecordContent.m_objPeopleInfo.m_StrOffice_street;
                objDPArr[16].Value = p_objRecordContent.m_objPeopleInfo.m_StrOfficePhone;
                objDPArr[17].Value = p_objRecordContent.m_objPeopleInfo.m_StrOfficePC;
                objDPArr[18].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManFirstName;
                objDPArr[19].Value = p_objRecordContent.m_objPeopleInfo.m_StrPatientRelation;
                objDPArr[20].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkMan_district;
                objDPArr[21].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkMan_street;
                objDPArr[22].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManPhone;
                objDPArr[23].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManPC;
                objDPArr[24].Value = p_objRecordContent.m_objPeopleInfo.m_Strhome_district;
                objDPArr[25].Value = p_objRecordContent.m_objPeopleInfo.m_Strhome_street;
                objDPArr[26].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomePhone;
                objDPArr[27].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomePC;
                objDPArr[28].Value = p_objRecordContent.m_objPeopleInfo.m_Strtemp_district;
                objDPArr[29].Value = p_objRecordContent.m_objPeopleInfo.m_Strtemp_street;
                objDPArr[30].Value = p_objRecordContent.m_objPeopleInfo.m_Strtemp_tel;
                objDPArr[31].Value = p_objRecordContent.m_objPeopleInfo.m_Strtemp_zipcode;
                objDPArr[32].Value = p_objRecordContent.m_objPeopleInfo.m_Strinsurance;
                objDPArr[33].Value = p_objRecordContent.m_objPeopleInfo.m_Stradmiss_status;
                objDPArr[34].Value = p_objRecordContent.m_objPeopleInfo.m_Strvisit_type;
                objDPArr[35].Value = p_objRecordContent.m_objPeopleInfo.m_StrChargeCategory;
                objDPArr[36].Value = p_objRecordContent.m_objPeopleInfo.m_StrPaymentPercent;
                objDPArr[37].Value = p_objRecordContent.m_objPeopleInfo.m_IntTimes;
                objDPArr[38].Value = p_objRecordContent.m_objPeopleInfo.m_StrNativePlace;


                //			for(int i=0;i<objDPArr.Length;i++)
                //			{
                //				if(objDPArr[i].Value==null)
                //					return (long)enmOperationResult.Parameter_Error;
                //			}
                //执行SQL	
                long lngEff = 0;
                string c_strAddNewPatientBaseInfoSQL2 = @"insert into patientbaseinfo (inpatientid,patientid,hic_no,firstname,sex,birth,married,occupation,vip_code,homeplace,
				nation,nationality,idcard,office_name,office_district,office_street,officephone,officepc,linkmanfirstname,patientrelation,
				linkman_district,linkman_street,linkmanphone,linkmanpc,home_district,home_street,homephone,homepc,temp_district,temp_street,
				temp_tel,temp_zipcode,insurance,admiss_status,visit_type,chargecategory,paymentpercent,times,status,nativeplace)
				values(?,?,?,?,?,?,?,?,?,?,
						?,?,?,?,?,?,?,?,?,?,
						?,?,?,?,?,?,?,?,?,?,
						?,?,?,?,?,?,?,?,'0',?) ";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewPatientBaseInfoSQL2, ref lngEff, objDPArr);
                return lngRes;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;


		}
		#endregion 添加病人基本信息

		#region 修改病人基本信息
		/*操作表PatientBaseInfo ,更新该表中各个字段 (除InPatientID, Status, Deactivated_Date, De_EmployeeID之外) 的内容.
		 *  输入参数PatientBaseInfo类对象
		 */
		/// <summary>
		/// 修改病人基本信息
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyPatientBaseInfo(clsPatientBaseInfo p_objRecordContent)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objRecordContent.m_objPeopleInfo==null || p_objRecordContent.m_objPeopleInfo.m_DtmBirth==DateTime.MinValue || p_objRecordContent.m_objPeopleInfo.m_DtmFirstDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngModifyPatientBaseInfo");
                //			if(lngCheckRes <= 0)
                //				//return lngCheckRes;	

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(47, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_objPeopleInfo.m_StrFirstName;
                objDPArr[1].Value = p_objRecordContent.m_objPeopleInfo.m_StrLastName;
                objDPArr[2].Value = p_objRecordContent.m_objPeopleInfo.m_StrIDCard;
                objDPArr[3].Value = p_objRecordContent.m_objPeopleInfo.m_StrSex;
                objDPArr[4].Value = p_objRecordContent.m_objPeopleInfo.m_StrMarried;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objRecordContent.m_objPeopleInfo.m_DtmBirth;
                objDPArr[6].Value = p_objRecordContent.m_objPeopleInfo.m_StrChargeCategory;
                objDPArr[7].Value = p_objRecordContent.m_objPeopleInfo.m_StrPaymentPercent;
                objDPArr[8].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomeplace;
                objDPArr[9].Value = p_objRecordContent.m_objPeopleInfo.m_StrNationality;
                objDPArr[10].Value = p_objRecordContent.m_objPeopleInfo.m_StrNation;
                objDPArr[11].Value = p_objRecordContent.m_objPeopleInfo.m_StrNativePlace;
                objDPArr[12].Value = p_objRecordContent.m_objPeopleInfo.m_StrOccupation;
                objDPArr[13].Value = p_objRecordContent.m_objPeopleInfo.m_StrOfficePhone;
                objDPArr[14].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomePhone;
                objDPArr[15].Value = p_objRecordContent.m_objPeopleInfo.m_StrMobile;
                //办公地址
                objDPArr[16].Value = p_objRecordContent.m_objPeopleInfo.m_StrOffice_district.Trim() + p_objRecordContent.m_objPeopleInfo.m_StrOffice_street.Trim();
                //家庭地址
                objDPArr[17].Value = p_objRecordContent.m_objPeopleInfo.m_Strhome_district.Trim() + p_objRecordContent.m_objPeopleInfo.m_Strhome_street.Trim();
                //工作单位
                objDPArr[18].Value = p_objRecordContent.m_objPeopleInfo.m_StrOffice_name;
                objDPArr[19].Value = p_objRecordContent.m_objPeopleInfo.m_StrOfficePC;
                objDPArr[20].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomePC;
                objDPArr[21].Value = p_objRecordContent.m_objPeopleInfo.m_StrEMail;
                objDPArr[22].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManFirstName;
                objDPArr[23].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManLastName;
                //联系人地址
                objDPArr[24].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkMan_district.Trim() + p_objRecordContent.m_objPeopleInfo.m_StrLinkMan_street.Trim();
                objDPArr[25].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManPhone;
                objDPArr[26].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManPC;
                objDPArr[27].Value = p_objRecordContent.m_objPeopleInfo.m_StrPatientRelation;
                objDPArr[28].DbType = DbType.DateTime;
                objDPArr[28].Value = p_objRecordContent.m_objPeopleInfo.m_DtmFirstDate;
                objDPArr[29].Value = p_objRecordContent.m_objPeopleInfo.m_BlnIsEmployee == true ? "1" : "0";
                objDPArr[30].Value = p_objRecordContent.m_strPatientID;
                //办公省市
                objDPArr[31].Value = p_objRecordContent.m_objPeopleInfo.m_StrOffice_district;
                //办公街道
                objDPArr[32].Value = p_objRecordContent.m_objPeopleInfo.m_StrOffice_street;
                //家庭省市
                objDPArr[33].Value = p_objRecordContent.m_objPeopleInfo.m_Strhome_district;
                //家庭街道
                objDPArr[34].Value = p_objRecordContent.m_objPeopleInfo.m_Strhome_street;
                //联系人省市
                objDPArr[35].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkMan_district;
                //联系人街道
                objDPArr[36].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkMan_street;
                //临时省市
                objDPArr[37].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkMan_district;
                //临时街道
                objDPArr[38].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkMan_street;
                //临时电话
                objDPArr[39].Value = p_objRecordContent.m_objPeopleInfo.m_Strtemp_tel;
                //临时邮编
                objDPArr[40].Value = p_objRecordContent.m_objPeopleInfo.m_Strtemp_zipcode;
                //保险公司
                objDPArr[41].Value = p_objRecordContent.m_objPeopleInfo.m_Strinsurance;
                //保险状态
                objDPArr[42].Value = p_objRecordContent.m_objPeopleInfo.m_Stradmiss_status;
                //访问类型
                objDPArr[43].Value = p_objRecordContent.m_objPeopleInfo.m_Strvisit_type;
                //司局级别
                objDPArr[44].Value = p_objRecordContent.m_objPeopleInfo.m_Strvip_code;
                //医疗证
                objDPArr[45].Value = p_objRecordContent.m_objPeopleInfo.m_Strhic_no;
                //住院号
                objDPArr[46].Value = p_objRecordContent.m_strInPatientID;






                //			for(int i=0;i<objDPArr.Length;i++)
                //			{
                //				if(objDPArr[i].Value==null)
                //					return (long)enmOperationResult.Parameter_Error;
                //			}
                //执行SQL	
                long lngEff = 0;
                string c_strModifyPatientBaseInfoSQL = @"update patientbaseinfo set firstname=? ,lastname =? ,  idcard=? , sex=?, married=?, birth=?,chargecategory=?,
				paymentpercent=?,homeplace=?,nationality=?,nation=?,nativeplace=?,occupation=?,officephone=?,homephone=?,mobile=?,officeaddress=?,homeaddress=?,office_name=?,
				officepc=?,homepc=?,email=?,linkmanfirstname=?,linkmanlastname=?,linkmanaddress=?,linkmanphone=?,linkmanpc=?,patientrelation=?,firstdate=?,isemployee=?,patientid=?,
				office_district=?,office_street=?, home_district=?,home_street=?, linkman_district=?,linkman_street=?, temp_district=?,temp_street=?,temp_tel=?,temp_zipcode=?,insurance=?,admiss_status=?,visit_type=?,vip_code=?,hic_no=? 
				where  inpatientid =? ";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyPatientBaseInfoSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}


		//病人基本资料维护（护士方面）
		[AutoComplete]
		public long m_lngModifyPatientBaseInfo2(clsPatientBaseInfo p_objRecordContent)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objRecordContent.m_objPeopleInfo==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(39, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_objPeopleInfo.m_IntTimes;
                objDPArr[1].Value = p_objRecordContent.m_strPatientID;
                objDPArr[2].Value = p_objRecordContent.m_objPeopleInfo.m_Strhic_no;
                objDPArr[3].Value = p_objRecordContent.m_objPeopleInfo.m_StrFirstName;
                objDPArr[4].Value = p_objRecordContent.m_objPeopleInfo.m_StrSex;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objRecordContent.m_objPeopleInfo.m_DtmBirth;
                objDPArr[6].Value = p_objRecordContent.m_objPeopleInfo.m_StrMarried;
                objDPArr[7].Value = p_objRecordContent.m_objPeopleInfo.m_StrOccupation;
                objDPArr[8].Value = p_objRecordContent.m_objPeopleInfo.m_Strvip_code;
                objDPArr[9].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomeplace;
                objDPArr[10].Value = p_objRecordContent.m_objPeopleInfo.m_StrNation;
                objDPArr[11].Value = p_objRecordContent.m_objPeopleInfo.m_StrNationality;
                objDPArr[12].Value = p_objRecordContent.m_objPeopleInfo.m_StrIDCard;
                objDPArr[13].Value = p_objRecordContent.m_objPeopleInfo.m_StrOffice_name;
                objDPArr[14].Value = p_objRecordContent.m_objPeopleInfo.m_StrOffice_district;
                objDPArr[15].Value = p_objRecordContent.m_objPeopleInfo.m_StrOffice_street;
                objDPArr[16].Value = p_objRecordContent.m_objPeopleInfo.m_StrOfficePhone;
                objDPArr[17].Value = p_objRecordContent.m_objPeopleInfo.m_StrOfficePC;
                objDPArr[18].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManFirstName;
                objDPArr[19].Value = p_objRecordContent.m_objPeopleInfo.m_StrPatientRelation;
                objDPArr[20].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkMan_district;
                objDPArr[21].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkMan_street;
                objDPArr[22].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManPhone;
                objDPArr[23].Value = p_objRecordContent.m_objPeopleInfo.m_StrLinkManPC;
                objDPArr[24].Value = p_objRecordContent.m_objPeopleInfo.m_Strhome_district;
                objDPArr[25].Value = p_objRecordContent.m_objPeopleInfo.m_Strhome_street;
                objDPArr[26].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomePhone;
                objDPArr[27].Value = p_objRecordContent.m_objPeopleInfo.m_StrHomePC;
                objDPArr[28].Value = p_objRecordContent.m_objPeopleInfo.m_Strtemp_district;
                objDPArr[29].Value = p_objRecordContent.m_objPeopleInfo.m_Strtemp_street;
                objDPArr[30].Value = p_objRecordContent.m_objPeopleInfo.m_Strtemp_tel;
                objDPArr[31].Value = p_objRecordContent.m_objPeopleInfo.m_Strtemp_zipcode;
                objDPArr[32].Value = p_objRecordContent.m_objPeopleInfo.m_Strinsurance;
                objDPArr[33].Value = p_objRecordContent.m_objPeopleInfo.m_Stradmiss_status;
                objDPArr[34].Value = p_objRecordContent.m_objPeopleInfo.m_Strvisit_type;
                objDPArr[35].Value = p_objRecordContent.m_objPeopleInfo.m_StrChargeCategory;
                objDPArr[36].Value = p_objRecordContent.m_objPeopleInfo.m_StrPaymentPercent;
                objDPArr[37].Value = p_objRecordContent.m_objPeopleInfo.m_StrNativePlace;
                objDPArr[38].Value = p_objRecordContent.m_strInPatientID;

                //			for(int i=0;i<objDPArr.Length;i++)
                //			{
                //				if(objDPArr[i].Value==null)
                //					return (long)enmOperationResult.Parameter_Error;
                //			}

                long lngEff = 0;
                string c_strModifyPatientBaseInfoSQL2 = @"update patientbaseinfo set
				times=?,patientid=?,hic_no=?,firstname=? ,sex=?,birth=?,married=?,occupation=?,vip_code=?,
				homeplace=?,nation=?,nationality=?,idcard=?,office_name=?,office_district=?,office_street=?,officephone=?,officepc=?,
				linkmanfirstname=?,patientrelation=?,linkman_district=?,linkman_street=?,linkmanphone=?,linkmanpc=?,
				home_district=?,home_street=?,homephone=?,homepc=?,
				temp_district=?,temp_street=?,temp_tel=?,temp_zipcode=?,
				insurance=?,admiss_status=?,visit_type=?,chargecategory=?,paymentpercent=?，nativeplace=?
				where  inpatientid =? ";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyPatientBaseInfoSQL2, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
		#endregion 修改病人基本信息

		#region 记录病人基本资料的修改标记
		/// <summary>
		/// 记录病人基本资料的修改标记
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddPatientBaseInfo_Flag(
			clsPatientBaseInfo_ModifyFlag p_objRecordContent)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null || p_objRecordContent.m_strInPatientID=="")
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngModifyPatientBaseInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr0;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr0);
                objDPArr0[0].Value = p_objRecordContent.m_strInPatientID;

                string strSQL = @"select inpatientid,
       occupation,
       homeplace,
       idcard,
       office_name,
       officephone,
       officepc,
       homeaddress,
       homepc,
       linkmanfirstname,
       patientrelation,
       linkmanaddress,
       linkmanphone
  from patientbaseinfo_modifyflag
 where inpatientid = ?";

                DataTable dtResult = new DataTable();
                //执行SQL	
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr0);
                if (lngRes > 0 && dtResult.Rows.Count > 0) return 1;

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(29, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].Value = p_objRecordContent.m_intPatientID;
                objDPArr[2].Value = p_objRecordContent.m_intFirstName;
                objDPArr[3].Value = p_objRecordContent.m_intIDCard;
                objDPArr[4].Value = p_objRecordContent.m_intSex;
                objDPArr[5].Value = p_objRecordContent.m_intMarried;
                objDPArr[6].Value = p_objRecordContent.m_intBirth;
                objDPArr[7].Value = p_objRecordContent.m_intChargeCategory;
                objDPArr[8].Value = p_objRecordContent.m_intPaymentPercent;
                objDPArr[9].Value = p_objRecordContent.m_intHomeplace;
                objDPArr[10].Value = p_objRecordContent.m_intNationality;
                objDPArr[11].Value = p_objRecordContent.m_intNation;
                objDPArr[12].Value = p_objRecordContent.m_intNativePlace;
                objDPArr[13].Value = p_objRecordContent.m_intOccupation;
                objDPArr[14].Value = p_objRecordContent.m_intOfficePhone;
                objDPArr[15].Value = p_objRecordContent.m_intHomePhone;
                objDPArr[16].Value = p_objRecordContent.m_intMobile;
                objDPArr[17].Value = p_objRecordContent.m_intOfficeAddress;
                objDPArr[18].Value = p_objRecordContent.m_intHomeAddress;
                objDPArr[19].Value = p_objRecordContent.m_intOfficePC;
                objDPArr[20].Value = p_objRecordContent.m_intHomePC;
                objDPArr[21].Value = p_objRecordContent.m_intEMail;
                objDPArr[22].Value = p_objRecordContent.m_intLinkManFirstName;
                objDPArr[23].Value = p_objRecordContent.m_intLinkManAddress;
                objDPArr[24].Value = p_objRecordContent.m_intLinkManPhone;
                objDPArr[25].Value = p_objRecordContent.m_intLinkManPC;
                objDPArr[26].Value = p_objRecordContent.m_intPatientRelation;
                objDPArr[27].Value = p_objRecordContent.m_intFirstDate;
                objDPArr[28].Value = p_objRecordContent.m_intIsEmployee;
                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        objDPArr[i].Value = 0;
                }
                //执行SQL	
                long lngEff = 0;
                string c_strAddPatientBase_FlagSQL = @"insert into patientbaseinfo_modifyflag
		(inpatientid, patientid, firstname, idcard, sex, married, birth, chargecategory, 
		paymentpercent, homeplace, nationality, nation, nativeplace, occupation, 
		officephone, homephone, mobile, officeaddress, homeaddress, officepc, homepc, 
		email, linkmanfirstname, linkmanaddress, linkmanphone, linkmanpc, 
		patientrelation, firstdate, isemployee)
			values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddPatientBase_FlagSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
		#endregion
		[AutoComplete]
		public long m_lngGetPatientModifyFlag(
			string p_strInPatientID,out clsPatientBaseInfo_ModifyFlag p_objRecordContent)
		{
			p_objRecordContent = null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select inpatientid,
       occupation,
       homeplace,
       idcard,
       office_name,
       officephone,
       officepc,
       homeaddress,
       homepc,
       linkmanfirstname,
       patientrelation,
       linkmanaddress,
       linkmanphone
  from patientbaseinfo_modifyflag where  inpatientid  = '" + p_strInPatientID + "'";

                DataTable dtResult = new DataTable();
                //执行SQL	
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtResult);
                if (lngRes <= 0) return lngRes;

                if (dtResult.Rows.Count == 1)
                {
                    p_objRecordContent = new clsPatientBaseInfo_ModifyFlag();
                    p_objRecordContent.m_intOccupation = (dtResult.Rows[0]["OCCUPATION"].ToString() == "True") ? 1 : 0;
                    p_objRecordContent.m_intHomeplace = (dtResult.Rows[0]["HOMEPLACE"].ToString() == "True") ? 1 : 0;
                    p_objRecordContent.m_intIDCard = (dtResult.Rows[0]["IDCARD"].ToString() == "True") ? 1 : 0;
                    p_objRecordContent.m_intOfficeAddress = (dtResult.Rows[0]["OFFICE_NAME"].ToString() == "True") ? 1 : 0;
                    p_objRecordContent.m_intOfficePhone = (dtResult.Rows[0]["OFFICEPHONE"].ToString() == "True") ? 1 : 0;
                    p_objRecordContent.m_intOfficePC = (dtResult.Rows[0]["OFFICEPC"].ToString() == "True") ? 1 : 0;
                    p_objRecordContent.m_intHomeAddress = (dtResult.Rows[0]["HOMEADDRESS"].ToString() == "True") ? 1 : 0;
                    p_objRecordContent.m_intHomePC = (dtResult.Rows[0]["HOMEPC"].ToString() == "True") ? 1 : 0;
                    p_objRecordContent.m_intLinkManFirstName = (dtResult.Rows[0]["LINKMANFIRSTNAME"].ToString() == "True") ? 1 : 0;
                    p_objRecordContent.m_intPatientRelation = (dtResult.Rows[0]["PATIENTRELATION"].ToString() == "True") ? 1 : 0;
                    p_objRecordContent.m_intLinkManAddress = (dtResult.Rows[0]["LINKMANADDRESS"].ToString() == "True") ? 1 : 0;
                    p_objRecordContent.m_intLinkManPhone = (dtResult.Rows[0]["LINKMANPHONE"].ToString() == "True") ? 1 : 0;
                }


            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
		}

		#region 入院
		/*操作表 InPatientDateInfo , 添加InPatientID及对应的InPatientDate,
		 *  输入参数InPatientID, InPatientDate. 
		 */
		/// <summary>
		/// 入院
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_dtmInPatientDate"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAddNewInPatientDateInfo(string p_strInPatientID,DateTime p_dtmInPatientDate)
		{
			//检查参数
			if(p_strInPatientID==null||p_dtmInPatientDate==DateTime.MinValue )
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngAddNewInPatientDateInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = p_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse("1900-1-1");

                //执行SQL	
                long lngEff = 0;
                string c_strAddNewInPatientDateInfoSQL = @"insert into  inpatientdateinfo(inpatientid,inpatientdate,inpatientenddate) 
					values(?,?,?)";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewInPatientDateInfoSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			
            //返回
			return lngRes;
		
		}
		#endregion 入院
		
		#region 分配床位
		/*操作表 InDeptInfo , 添加表中(除InBedEndDate之外)各个字段. 
		 * 输入参数InDeptInfo类对象. 
		 */		

		/// <summary>
		/// 分配床位
		/// </summary>
		/// <param name="p_objRecordContent">病床(住院)信息.(其中的InBedEndDate,Begin_Date_Area_Dept,Begin_Date_Room_Area,Begin_Date_Bed_Room不用赋值)</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAssignPatientToBed(clsInDeptInfo p_objRecordContent)
		{
			//检查参数
			if( p_objRecordContent==null|| p_objRecordContent.m_strInPatientID==null || p_objRecordContent.m_dtmModifyDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngAssignPatientToBed");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //提取m_dtmBegin_Date_Area_Dept;
                long lngRes0 = m_lngGetBegin_Date_Area_Dept(p_objRecordContent.m_strInDeptID, p_objRecordContent.m_strArea_ID, out p_objRecordContent.m_dtmBegin_Date_Area_Dept);
                if (lngRes0 > 0 && p_objRecordContent.m_dtmBegin_Date_Area_Dept == DateTime.MinValue)
                    return (long)enmOperationResult.Parameter_Error;
                else if (lngRes0 <= 0) return lngRes0;

                //提取m_dtmBegin_Date_Room_Area;
                lngRes0 = m_lngGetBegin_Date_Room_Area(p_objRecordContent.m_strArea_ID, p_objRecordContent.m_strRoom_ID, out p_objRecordContent.m_dtmBegin_Date_Room_Area);
                if (lngRes0 > 0 && p_objRecordContent.m_dtmBegin_Date_Room_Area == DateTime.MinValue)
                    return (long)enmOperationResult.Parameter_Error;
                else if (lngRes0 <= 0) return lngRes0;

                //提取m_dtmBegin_Date_Bed_Room;
                lngRes0 = m_lngGetBegin_Date_Bed_Room(p_objRecordContent.m_strRoom_ID, p_objRecordContent.m_strBed_ID, out p_objRecordContent.m_dtmBegin_Date_Bed_Room);
                if (lngRes0 > 0 && p_objRecordContent.m_dtmBegin_Date_Bed_Room == DateTime.MinValue)
                    return (long)enmOperationResult.Parameter_Error;
                else if (lngRes0 <= 0) return lngRes0;

                //提取m_dtmInPatientDate;
                lngRes0 = m_lngGetInPatientDate(p_objRecordContent.m_strInPatientID, out p_objRecordContent.m_dtmInPatientDate);
                if (lngRes0 > 0 && p_objRecordContent.m_dtmInPatientDate == DateTime.MinValue)
                    return (long)enmOperationResult.Parameter_Error;
                else if (lngRes0 <= 0) return lngRes0;


                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(11, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].Value = p_objRecordContent.m_strInDeptID;
                objDPArr[3].Value = p_objRecordContent.m_strArea_ID;
                objDPArr[4].Value = p_objRecordContent.m_strRoom_ID;
                objDPArr[5].Value = p_objRecordContent.m_strBed_ID;
                objDPArr[6].DbType = DbType.DateTime;
                objDPArr[6].Value = p_objRecordContent.m_dtmModifyDate;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = DateTime.Parse("1900-1-1");
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = p_objRecordContent.m_dtmBegin_Date_Area_Dept;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = p_objRecordContent.m_dtmBegin_Date_Room_Area;
                objDPArr[10].DbType = DbType.DateTime;
                objDPArr[10].Value = p_objRecordContent.m_dtmBegin_Date_Bed_Room;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL	
                long lngEff = 0;
                string c_strAssignPatientToBedSQL = @"insert into  indeptinfo(inpatientid,inpatientdate,indeptid,area_id,room_id,bed_id,modifydate,inbedenddate,begin_date_area_dept,begin_date_room_area,begin_date_bed_room) 
					values(?,?,?,?,?,?,?,?,?,?,?)";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAssignPatientToBedSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}

		/// <summary>
		/// 提取m_dtmBegin_Date_Area_Dept
		/// </summary>
		/// <param name="p_strDepartmentID"></param>
		/// <param name="p_strArea_ID"></param>
		/// <param name="p_dtmBegin_Date_Area_Dept"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngGetBegin_Date_Area_Dept(string p_strDepartmentID,string p_strArea_ID,out DateTime p_dtmBegin_Date_Area_Dept)
		{
			p_dtmBegin_Date_Area_Dept=DateTime.MinValue;			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strDepartmentID;
                objDPArr[1].Value = p_strArea_ID;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL	
                DataTable dtbResult = null;
                string m_strGetBegin_Date_Area_DeptSQL = @"select begin_date_area_dept from inpatient_area_dept where  departmentid =? and  area_id =? and (end_date_dept=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + " or end_date_dept is null) ";

                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetBegin_Date_Area_DeptSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_dtmBegin_Date_Area_Dept = DateTime.Parse(dtbResult.Rows[0]["BEGIN_DATE_AREA_DEPT"].ToString());
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			
            //返回
			return lngRes;
		}

		/// <summary>
		/// 提取m_dtmBegin_Date_Room_Area
		/// </summary>
		/// <param name="p_strArea_ID"></param>
		/// <param name="p_strRoom_ID"></param>
		/// <param name="p_dtmBegin_Date_Room_Area"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngGetBegin_Date_Room_Area(string p_strArea_ID,string p_strRoom_ID,out DateTime p_dtmBegin_Date_Room_Area)
		{
			p_dtmBegin_Date_Room_Area=DateTime.MinValue;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strArea_ID;
                objDPArr[1].Value = p_strRoom_ID;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL	
                DataTable dtbResult = null;
                string m_strGetBegin_Date_Room_AreaSQL = @"select begin_date_room_area from inpatient_room_area where  area_id =? and  room_id =? and (end_date_room_area=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + " or end_date_room_area is null) ";

                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetBegin_Date_Room_AreaSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_dtmBegin_Date_Room_Area = DateTime.Parse(dtbResult.Rows[0]["BEGIN_DATE_ROOM_AREA"].ToString());
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		}

		/// <summary>
		/// 提取m_dtmBegin_Date_Bed_Room
		/// </summary>
		/// <param name="p_strRoom_ID"></param>
		/// <param name="p_strBed_ID"></param>
		/// <param name="p_dtmBegin_Date_Bed_Room"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngGetBegin_Date_Bed_Room(string p_strRoom_ID,string p_strBed_ID,out DateTime p_dtmBegin_Date_Bed_Room)
		{
			p_dtmBegin_Date_Bed_Room=DateTime.MinValue;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRoom_ID;
                objDPArr[1].Value = p_strBed_ID;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL	
                DataTable dtbResult = null;
                string m_strGetBegin_Date_Bed_RoomSQL = @"select begin_date_bed_room from inpatient_bed_room where  room_id =? and  bed_id =? and (end_date_bed_room=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + " or end_date_bed_room is null) ";

                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetBegin_Date_Bed_RoomSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_dtmBegin_Date_Bed_Room = DateTime.Parse(dtbResult.Rows[0]["BEGIN_DATE_BED_ROOM"].ToString());
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		}
		[AutoComplete]
		private long m_lngGetInPatientDate(string p_strInPatientID,out DateTime p_dtmInPatientDate)
		{
			p_dtmInPatientDate=DateTime.MinValue;
			if(p_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;

                //执行SQL	
                DataTable dtbResult = null;
                string m_strGetInPatientDateSQL = @"select inpatientdate from inpatientdateinfo where  inpatientid =? and (inpatientenddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + " or inpatientenddate is null) ";

                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetInPatientDateSQL, ref dtbResult, objDPArr);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_dtmInPatientDate = DateTime.Parse(dtbResult.Rows[0]["INPATIENTDATE"].ToString());
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
		}
		#endregion 分配床位

		#region 转床（包括转科）
		/*操作表 InDeptInfo , 更新表中对应记录的InBedEndDate(删除记录),添加一条新记录. 
		 * 输入参数转床后的InDeptInfo类对象
		 */
		/// <summary> 
		/// 转床（包括转科）
		/// 1.更新表中对应记录的InBedEndDate(删除记录)
		/// 2.添加一条新记录.
		/// </summary>
		/// <param name="p_objRecordContent">要转到的病床(住院)信息.(其中的InBedEndDate,Begin_Date_Area_Dept,Begin_Date_Room_Area,Begin_Date_Bed_Room不用赋值)</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngPatientTransferBed(clsInDeptInfo p_objRecordContent)
		{			
			//检查参数
			if( p_objRecordContent==null|| p_objRecordContent.m_strInPatientID==null  || p_objRecordContent.m_dtmModifyDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngPatientTransferBed");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //提取m_dtmBegin_Date_Area_Dept;
                long lngRes0 = m_lngGetBegin_Date_Area_Dept(p_objRecordContent.m_strInDeptID, p_objRecordContent.m_strArea_ID, out p_objRecordContent.m_dtmBegin_Date_Area_Dept);
                if (lngRes0 > 0 && p_objRecordContent.m_dtmBegin_Date_Area_Dept == DateTime.MinValue)
                    return (long)enmOperationResult.Parameter_Error;
                else if (lngRes0 <= 0) return lngRes0;

                //提取m_dtmBegin_Date_Room_Area;
                lngRes0 = m_lngGetBegin_Date_Room_Area(p_objRecordContent.m_strArea_ID, p_objRecordContent.m_strRoom_ID, out p_objRecordContent.m_dtmBegin_Date_Room_Area);
                if (lngRes0 > 0 && p_objRecordContent.m_dtmBegin_Date_Room_Area == DateTime.MinValue)
                    return (long)enmOperationResult.Parameter_Error;
                else if (lngRes0 <= 0) return lngRes0;

                //提取m_dtmBegin_Date_Bed_Room;
                lngRes0 = m_lngGetBegin_Date_Bed_Room(p_objRecordContent.m_strRoom_ID, p_objRecordContent.m_strBed_ID, out p_objRecordContent.m_dtmBegin_Date_Bed_Room);
                if (lngRes0 > 0 && p_objRecordContent.m_dtmBegin_Date_Bed_Room == DateTime.MinValue)
                    return (long)enmOperationResult.Parameter_Error;
                else if (lngRes0 <= 0) return lngRes0;

                //提取m_dtmInPatientDate;
                lngRes0 = m_lngGetInPatientDate(p_objRecordContent.m_strInPatientID, out p_objRecordContent.m_dtmInPatientDate);
                if (lngRes0 > 0 && p_objRecordContent.m_dtmInPatientDate == DateTime.MinValue)
                    return (long)enmOperationResult.Parameter_Error;
                else if (lngRes0 <= 0) return lngRes0;


                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmModifyDate;//InBedEndDate;//新转床时间即为下床时间
                objDPArr[1].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmInPatientDate;

                //执行SQL	
                long lngEff = 0;
                string c_strDeleteOldInfoWhenPatientTransferBedSQL = "update indeptinfo set inbedenddate=? where  inpatientid =? and inpatientdate=? and (inbedenddate is null or inbedenddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteOldInfoWhenPatientTransferBedSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                objHRPServ.CreateDatabaseParameter(11, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].Value = p_objRecordContent.m_strInDeptID;
                objDPArr[3].Value = p_objRecordContent.m_strArea_ID;
                objDPArr[4].Value = p_objRecordContent.m_strRoom_ID;
                objDPArr[5].Value = p_objRecordContent.m_strBed_ID;
                objDPArr[6].DbType = DbType.DateTime;
                objDPArr[6].Value = p_objRecordContent.m_dtmModifyDate;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = DateTime.Parse("1900-1-1");
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = p_objRecordContent.m_dtmBegin_Date_Area_Dept;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = p_objRecordContent.m_dtmBegin_Date_Room_Area;
                objDPArr[10].DbType = DbType.DateTime;
                objDPArr[10].Value = p_objRecordContent.m_dtmBegin_Date_Bed_Room;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL
                string c_strPatientTransferBedSQL = @"insert into  indeptinfo(inpatientid,inpatientdate,indeptid,area_id,room_id,bed_id,modifydate,inbedenddate,begin_date_area_dept,begin_date_room_area,begin_date_bed_room) 
					values(?,?,?,?,?,?,?,?,?,?,?)";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strPatientTransferBedSQL, ref lngEff, objDPArr);
                return lngRes;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }		//返回
			return lngRes;

		}
		#endregion 转床（包括转科）

		#region 出院（包括死亡）
		/*操作表 InPatientDateInfo , 更新表中InPatientEndDate,
		 *  输入参数InPatientID, InPatientDate, InPatientEndDate
		 */ 
		/// <summary>
		/// 出院（包括死亡）
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_dtmInPatientDate"></param>
		/// <param name="p_dtmInPatientEndDate"></param>
		/// <param name="p_intEndReason"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngDeleteInPatientDateInfo(string p_strInPatientID,DateTime p_dtmInPatientDate,DateTime p_dtmInPatientEndDate,int p_intEndReason)
		{
			//检查参数
			if(p_strInPatientID==null||p_dtmInPatientDate==DateTime.MinValue || p_dtmInPatientEndDate==DateTime.MinValue || p_intEndReason<0 || p_intEndReason>9)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngDeleteInPatientDateInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmInPatientEndDate;
                objDPArr[1].Value = p_intEndReason;
                objDPArr[2].Value = p_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_dtmInPatientDate;

                //执行SQL	
                long lngEff = 0;
                string c_strDeleteInPatientDateInfoSQL = @"update inpatientdateinfo set inpatientenddate=?, endreason=? where  inpatientid =? and inpatientdate=? ";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteInPatientDateInfoSQL, ref lngEff, objDPArr);

                string strSQL = @"insert into inpatientarchiving (inpatientid,inpatientdate,opendate,ifarchived) 
						values(?,?,?,?)";
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[3].Value = "0";
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                return lngRes;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
		#endregion 出院（包括死亡）

		#region 下床
		/*操作表 InDeptInfo , 更新表中对应记录的InBedEndDate(删除记录) ,
		 *  输入参数InPatientID,InPatientDate,InBedEndDate
		 */
		/// <summary>
		/// 下床
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_dtmInPatientDate"></param>
		/// <param name="p_dtmInBedEndDate"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngPatientLeaveBed(string p_strInPatientID,DateTime p_dtmInPatientDate,DateTime p_dtmInBedEndDate)
		{
			//检查参数
			if(p_strInPatientID==null||p_dtmInPatientDate==DateTime.MinValue || p_dtmInBedEndDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngPatientLeaveBed");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmInBedEndDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmInPatientDate;

                //执行SQL	
                long lngEff = 0;
                string c_strDeleteOldInfoWhenPatientTransferBedSQL = "update indeptinfo set inbedenddate=? where  inpatientid =? and inpatientdate=? and (inbedenddate is null or inbedenddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteOldInfoWhenPatientTransferBedSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
		#endregion 下床

		#endregion 病人

		#region 员工
		/// <summary>
		/// 查看是否有相同的员工ID
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckID_Employee(string p_strEmployeeID)
		{
			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDepartmentHandlerService","m_lngCheckID_Employee");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;	
				string c_strCheckIDSQL_Employee = @"select employeeid,
       begindate,
       firstname,
       lastname,
       idcard,
       pycode,
       sex,
       educationallevel,
       married,
       titleofatechnicalpost,
       languageability,
       birth,
       officephone,
       homephone,
       mobile,
       officeaddress,
       homeaddress,
       officepc,
       homepc,
       email,
       firstnameofannouncer,
       lastnameofannouncer,
       phoneofannouncer,
       experience,
       remark,
       status,
       deactivedate,
       operatorid,
       shortname
  from employeebaseinfo
 where employeeid = ?";

				lngRes= m_lngCheckSame(p_strEmployeeID,c_strCheckIDSQL_Employee);

			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
		}

		#region 添加员工基本信息
		/*操作表EmployeeBaseInfo ,添加该表中各个字段 (除DeActiveDate, OperatorID之外) 的内容.
		 *  输入参数clsEmployee_BaseInfo类对象
		 */
		/// <summary>
		/// 添加病人基本信息
		/// </summary>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAddNewEmployeeBaseInfo(clsEmployee_BaseInfo p_objRecordContent)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strEmployeeID==null|| p_objRecordContent.m_dtmBeginDate==DateTime.MinValue || p_objRecordContent.m_dtmBirth==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngAddNewEmployeeBaseInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(26, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strEmployeeID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmBeginDate;
                objDPArr[2].Value = p_objRecordContent.m_strFirstName;
                objDPArr[3].Value = p_objRecordContent.m_strLastName;
                objDPArr[4].Value = p_objRecordContent.m_strIDCard;
                objDPArr[5].Value = p_objRecordContent.m_strPYCode;
                objDPArr[6].Value = p_objRecordContent.m_strSex;
                objDPArr[7].Value = p_objRecordContent.m_strEducationalLevel;
                objDPArr[8].Value = p_objRecordContent.m_strMarried;
                objDPArr[9].Value = p_objRecordContent.m_strTitleOfaTechnicalPost;
                objDPArr[10].Value = p_objRecordContent.m_strLanguageAbility;
                objDPArr[11].DbType = DbType.DateTime;
                objDPArr[11].Value = p_objRecordContent.m_dtmBirth;
                objDPArr[12].Value = p_objRecordContent.m_strOfficePhone;
                objDPArr[13].Value = p_objRecordContent.m_strHomePhone;
                objDPArr[14].Value = p_objRecordContent.m_strMobile;
                objDPArr[15].Value = p_objRecordContent.m_strOfficeAddress;
                objDPArr[16].Value = p_objRecordContent.m_strHomeAddress;
                objDPArr[17].Value = p_objRecordContent.m_strOfficePC;
                objDPArr[18].Value = p_objRecordContent.m_strHomePC;
                objDPArr[19].Value = p_objRecordContent.m_strEMail;
                objDPArr[20].Value = p_objRecordContent.m_strFirstNameOfAnnouncer;
                objDPArr[21].Value = p_objRecordContent.m_strLastNameOfAnnouncer;
                objDPArr[22].Value = p_objRecordContent.m_strPhoneOfAnnouncer;
                objDPArr[23].Value = p_objRecordContent.m_strExperience;
                objDPArr[24].Value = p_objRecordContent.m_strRemark;
                objDPArr[25].Value = "0";

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                IDataParameter[] objDPArr_ = new IDataParameter[26];
                //按顺序给IDataParameter赋值
                for (int i = 0; i < objDPArr.Length; i++)
                {
                    //				objDPArr_[i]=new IDataParameter();
                    objDPArr_[i] = objDPArr[i];
                }

                //执行SQL	
                long lngEff = 0;
                string c_strAddNewEmployeeBaseInfoSQL = @"insert into employeebaseinfo (employeeid,begindate, firstname ,lastname , idcard ,pycode, sex, educationallevel,married,titleofatechnicalpost,languageability, birth,
				officephone,homephone,mobile,officeaddress,homeaddress,
				officepc,homepc,email,firstnameofannouncer,lastnameofannouncer,phoneofannouncer,experience,remark,status)
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) ";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewEmployeeBaseInfoSQL, ref lngEff, objDPArr_);

                #region 添加员工密码
                string strSql = @"insert into employeepsw (employeeid,opendate,loginname,psw,status) values ('" + p_objRecordContent.m_strEmployeeID + "'," + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(DateTime.Now) + ",'" + p_objRecordContent.m_strEmployeeID + "','','0')";
                objHRPServ.DoExcute(strSql);
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
		#endregion 添加员工基本信息

		#region 修改员工基本信息(包括状态字段,即删除功能)
		/*操作表EmployeeBaseInfo ,更新该表中各个字段 (除EmployeeID之外) 的内容.
		 *  输入参数clsEmployee_BaseInfo类对象
		 */
		/// <summary>
		/// 修改病人基本信息
		/// </summary>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngModifyEmployeeBaseInfo(clsEmployee_BaseInfo p_objRecordContent)
		{			
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strEmployeeID==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngModifyEmployeeBaseInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr = null;
                string strSQL = "";
                if (p_objRecordContent.m_strStatus == "0" || p_objRecordContent.m_strStatus == null)
                {
                    //检查参数
                    if (p_objRecordContent.m_dtmBeginDate == DateTime.MinValue || p_objRecordContent.m_dtmBirth == DateTime.MinValue)
                        return (long)enmOperationResult.Parameter_Error;

                    objHRPServ.CreateDatabaseParameter(28, out objDPArr);

                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = p_objRecordContent.m_dtmBeginDate;
                    objDPArr[1].Value = p_objRecordContent.m_strFirstName;
                    objDPArr[2].Value = p_objRecordContent.m_strLastName;
                    objDPArr[3].Value = p_objRecordContent.m_strIDCard;
                    objDPArr[4].Value = p_objRecordContent.m_strPYCode;
                    objDPArr[5].Value = p_objRecordContent.m_strSex;
                    objDPArr[6].Value = p_objRecordContent.m_strEducationalLevel;
                    objDPArr[7].Value = p_objRecordContent.m_strMarried;
                    objDPArr[8].Value = p_objRecordContent.m_strTitleOfaTechnicalPost;
                    objDPArr[9].Value = p_objRecordContent.m_strLanguageAbility;
                    objDPArr[10].DbType = DbType.DateTime;
                    objDPArr[10].Value = p_objRecordContent.m_dtmBirth;
                    objDPArr[11].Value = p_objRecordContent.m_strOfficePhone;
                    objDPArr[12].Value = p_objRecordContent.m_strHomePhone;
                    objDPArr[13].Value = p_objRecordContent.m_strMobile;
                    objDPArr[14].Value = p_objRecordContent.m_strOfficeAddress;
                    objDPArr[15].Value = p_objRecordContent.m_strHomeAddress;
                    objDPArr[16].Value = p_objRecordContent.m_strOfficePC;
                    objDPArr[17].Value = p_objRecordContent.m_strHomePC;
                    objDPArr[18].Value = p_objRecordContent.m_strEMail;
                    objDPArr[19].Value = p_objRecordContent.m_strFirstNameOfAnnouncer;
                    objDPArr[20].Value = p_objRecordContent.m_strLastNameOfAnnouncer;
                    objDPArr[21].Value = p_objRecordContent.m_strPhoneOfAnnouncer;
                    objDPArr[22].Value = p_objRecordContent.m_strExperience;
                    objDPArr[23].Value = p_objRecordContent.m_strRemark;

                    objDPArr[24].Value = "0";
                    objDPArr[25].Value = DBNull.Value;
                    objDPArr[26].Value = DBNull.Value;

                    objDPArr[27].Value = p_objRecordContent.m_strEmployeeID;
                    strSQL = @"update employeebaseinfo set begindate=?,firstname=? ,lastname =? ,  idcard=? ,pycode=?, sex=?,educationallevel=?, married=?, titleofatechnicalpost=?,languageability=?,
			birth=?,officephone=?,homephone=?,mobile=?,officeaddress=?,homeaddress=?,
			officepc=?,homepc=?,email=?,firstnameofannouncer=?,lastnameofannouncer=?,phoneofannouncer=?,experience=?,remark=?,status=?,deactivedate=?,operatorid=? 
			where  employeeid =? ";

                }
                else
                {
                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strStatus;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmDeActiveDate;
                    objDPArr[2].Value = p_objRecordContent.m_strDeActivedOperatorID;
                    objDPArr[3].Value = p_objRecordContent.m_strEmployeeID;
                    strSQL = @"update employeebaseinfo set status=?,deactivedate=?,operatorid=? 
			where  employeeid =? "; ;

                }

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }
                //执行SQL	
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                if ((p_objRecordContent.m_strStatus == "0" || p_objRecordContent.m_strStatus == null) && lngRes > 0)
                {
                    strSQL = @"update employeepsw  set status = '0',deactiveddate = null where employeeid = '" + p_objRecordContent.m_strEmployeeID + @"'
         and opendate = (select max(opendate) from employeepsw where employeeid = '" + p_objRecordContent.m_strEmployeeID + "')";
                    objHRPServ.DoExcute(strSQL);
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
		#endregion 修改员工基本信息

		#region 分配或修改员工负责的病区
		/*操作表InPatient_Area_Employee,分配时,添加表InPatient_Area_Employee中(除OperatorID之外,End_Date_Employee_Area为"+clsHRPTableService.s_strGetSQLInvalidDateForma()+@")各个字段
		 * 修改时,先删除旧记录(更新End_Date_Employee_Area时间及OperatorID),再添加一条新记录.
		 * 传入参数:新的clsInPatient_Area_Employee对象(其中m_dtmBegin_Date_Area_Dept不用赋值.修改时,m_strOperatorID要赋值当前登陆医师ID).
		 */
		
		/// <summary>
		/// 分配员工负责的病区
		/// </summary>
		/// <param name="p_objRecordContent">clsInPatient_Area_Employee对象(其中Begin_Date_Area_Dept,OperatorID,End_Date_Employee_Area不用赋值).</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAssignArea_Employee(clsInPatient_Area_Employee p_objRecordContent)
		{			
			//检查参数
			if( p_objRecordContent==null
				|| p_objRecordContent.m_dtmBegin_Date_Employee_Area==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngAssignArea_Employee");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //提取m_dtmBegin_Date_Area_Dept;
                long lngRes0 = m_lngGetBegin_Date_Area_Dept(p_objRecordContent.m_strDepartmentID, p_objRecordContent.m_strArea_ID, out p_objRecordContent.m_dtmBegin_Date_Area_Dept);
                if (lngRes0 > 0 && p_objRecordContent.m_dtmBegin_Date_Area_Dept == DateTime.MinValue)
                    return (long)enmOperationResult.Parameter_Error;
                else if (lngRes0 <= 0) return lngRes0;

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strEmployee_ID;
                objDPArr[1].Value = p_objRecordContent.m_strArea_ID;
                objDPArr[2].Value = p_objRecordContent.m_strDepartmentID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmBegin_Date_Area_Dept;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmBegin_Date_Employee_Area;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL	
                long lngEff = 0;
                string c_strAssignArea_EmployeeSQL = @"insert into  inpatient_area_employee(employee_id,area_id,departmentid,begin_date_area_dept,begin_date_employee_area,end_date_employee_area) 
					values(?,?,?,?,?," + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAssignArea_EmployeeSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}		

		/// <summary>
		/// 修改员工负责的病区
		/// 1.删除旧记录(更新End_Date_Employee_Area时间及OperatorID)
		/// 2.添加一条新记录.
		/// </summary>
		/// <param name="p_objRecordContent">新的clsInPatient_Area_Employee对象(其中Begin_Date_Area_Dept,End_Date_Employee_Area不用赋值,OperatorID要赋值当前登陆医师ID).</param>
		/// <returns></returns>
		[AutoComplete] 
		public  long m_lngModifyArea_Employee(clsInPatient_Area_Employee p_objRecordContent)
		{
			//检查参数
			if( p_objRecordContent==null
				|| p_objRecordContent.m_dtmBegin_Date_Employee_Area==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngModifyArea_Employee");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //提取m_dtmBegin_Date_Area_Dept;
                long lngRes0 = m_lngGetBegin_Date_Area_Dept(p_objRecordContent.m_strDepartmentID, p_objRecordContent.m_strArea_ID, out p_objRecordContent.m_dtmBegin_Date_Area_Dept);
                if (lngRes0 > 0 && p_objRecordContent.m_dtmBegin_Date_Area_Dept == DateTime.MinValue)
                    return (long)enmOperationResult.Parameter_Error;
                else if (lngRes0 <= 0) return lngRes0;


                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmBegin_Date_Employee_Area;//修改时,删除时间等于新添时间
                objDPArr[1].Value = p_objRecordContent.m_strOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strEmployee_ID;
                objDPArr[3].Value = p_objRecordContent.m_strArea_ID;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL	
                long lngEff = 0;
                string c_strDeleteOldInfoOfArea_EmployeeSQL = "update inpatient_area_employee set end_date_employee_area=?,operatorid=? where  employee_id =? and  area_id =? and (end_date_employee_area is null or end_date_employee_area=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteOldInfoOfArea_EmployeeSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strEmployee_ID;
                objDPArr[1].Value = p_objRecordContent.m_strArea_ID;
                objDPArr[2].Value = p_objRecordContent.m_strDepartmentID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmBegin_Date_Area_Dept;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmBegin_Date_Employee_Area;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL	
                string c_strModifyArea_EmployeeSQL = @"insert into  inpatient_area_employee(employee_id,area_id,departmentid,begin_date_area_dept,begin_date_employee_area,end_date_employee_area) 
					values(?,?,?,?,?," + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyArea_EmployeeSQL, ref lngEff, objDPArr);
                return lngRes;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
			

		}

		#endregion 分配或修改员工负责的病区

		#region 分配或修改员工负责的科室
		/*操作表Dept_Employee,分配时,添加表Dept_Employee中各个字段
		 * 修改时,先删除旧记录(更新EndDate时间),再添加一条新记录.
		 * 传入参数:新的clsDept_Employee对象.
		 */
		/// <summary>
		/// 分配员工负责的科室
		/// </summary>
		/// <param name="p_objRecordContent">clsDept_Employee对象(其中EndDate不用赋值).</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAssignDept_Employee(clsDept_Employee p_objRecordContent)
		{			
			//检查参数
			if( p_objRecordContent==null
				|| p_objRecordContent.m_dtmModifyDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngAssignDept_Employee");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strEmployeeID;
                objDPArr[1].Value = p_objRecordContent.m_strDeptID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmModifyDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse("1900-1-1");


                //执行SQL	
                long lngEff = 0;
                string c_strAssignDept_EmployeeSQL = @"insert into  dept_employee(employeeid,deptid,modifydate,enddate) 
					values(?,?,?,?)";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAssignDept_EmployeeSQL, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                #region 员工权限，默认为住院医师
                string strAddRole_Employee = "insert into role_employee (role_id, employeeid, opendate, status) select role_id,'" +
                    p_objRecordContent.m_strEmployeeID + "'," + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(DateTime.Now) +
                    ",'0' from role_info a inner join dept_desc b on a.category=b.deptname where deptid='" +
                    p_objRecordContent.m_strDeptID + "' and role_name = '住院医师'";
                objHRPServ.DoExcute(strAddRole_Employee);
                return (long)enmOperationResult.DB_Succeed;
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
		}		


		/// <summary>
		/// 分配员工负责的科室,指定权限
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_strRoleName"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAssignDept_Employee2(clsDept_Employee p_objRecordContent,string p_strRoleName)
		{			
			//检查参数
			if( p_objRecordContent==null
				|| p_objRecordContent.m_dtmModifyDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strEmployeeID;
                objDPArr[1].Value = p_objRecordContent.m_strDeptID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmModifyDate;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL	
                long lngEff = 0;
                string c_strAssignDept_EmployeeSQL = @"insert into  dept_employee(employeeid,deptid,modifydate,enddate) 
					values(?,?,?,?)";
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAssignDept_EmployeeSQL, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;
                #region 员工权限
                string strAddRole_Employee = "insert into role_employee (role_id, employeeid, opendate, status) select role_id,'" +
                    p_objRecordContent.m_strEmployeeID + "'," + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(DateTime.Now) +
                    ",'0' from role_info a inner join dept_desc b on a.category=b.deptname where deptid='" +
                    p_objRecordContent.m_strDeptID + "' and role_name = '" + p_strRoleName + "'";
                objHRPServ.DoExcute(strAddRole_Employee);
                return (long)enmOperationResult.DB_Succeed;
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		
		}		


		/// <summary>
		/// 修改员工负责的科室
		/// 1.删除旧记录(更新EndDate)
		/// 2.添加一条新记录.
		/// </summary>
		/// <param name="p_objRecordContent">新的clsDept_Employee对象(其中EndDate不用赋值).</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngModifyDept_Employee(clsDept_Employee p_objRecordContent)
		{
			//检查参数
			if( p_objRecordContent==null
				|| p_objRecordContent.m_dtmModifyDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
clsHRPTableService objHRPServ = new clsHRPTableService();

long lngRes = 0;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngModifyDept_Employee");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmModifyDate;//修改时,删除时间等于新添时间			
                objDPArr[1].Value = p_objRecordContent.m_strEmployeeID;
                objDPArr[2].Value = p_objRecordContent.m_strDeptID;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL	
                long lngEff = 0;
                string c_strDeleteOldInfoOfDept_EmployeeSQL = "update dept_employee set enddate=? where  employeeid =? and  deptid =? and (enddate is null or enddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + ")";

                 lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteOldInfoOfDept_EmployeeSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strEmployeeID;
                objDPArr[1].Value = p_objRecordContent.m_strDeptID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmModifyDate;
                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL
                string c_strModifyDept_EmployeeSQL = @"insert into  dept_employee(employeeid,deptid,modifydate,enddate) 
					values(?,?,?,?)";

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyDept_EmployeeSQL, ref lngEff, objDPArr);

            }
	    finally
	    {
	      //objHRPServ.Dispose();

	    }
            return lngRes;	

		}

		/// <summary>
		/// 删除员工负责的科室
		/// </summary>
		/// <param name="p_objRecordContent">clsDept_Employee对象(其中ModifyDate不用赋值).</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngDeleteDept_Employee(clsDept_Employee p_objRecordContent)
		{
			//检查参数
			if( p_objRecordContent==null
				|| p_objRecordContent.m_dtmEndDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDepartmentHandlerService", "m_lngDeleteDept_Employee");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmEndDate;
                objDPArr[1].Value = p_objRecordContent.m_strEmployeeID;
                objDPArr[2].Value = p_objRecordContent.m_strDeptID;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                        return (long)enmOperationResult.Parameter_Error;
                }

                //执行SQL	
                long lngEff = 0;
                string c_strDeleteOldInfoOfDept_EmployeeSQL = "update dept_employee set enddate=? where  employeeid =? and  deptid =? and (enddate is null or enddate=" + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + ")";
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteOldInfoOfDept_EmployeeSQL, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                string strDeleteRole_Employee = @"update role_employee set role_employee.status=1 
												from role_employee,
												(select role_id from role_info,dept_desc
												where role_info.category=dept_desc.deptname
												and dept_desc.deptid='" + p_objRecordContent.m_strDeptID +
                                                "') v1 where role_employee.role_id=v1.role_id and role_employee.employeeid='" +
                                                p_objRecordContent.m_strEmployeeID + "'";

                objHRPServ.DoExcute(strDeleteRole_Employee);
                return (long)enmOperationResult.DB_Succeed;


            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		}

		#endregion 分配或修改员工负责的科室
		#endregion 员工

		#region  获取医院信息
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strName"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetHospitalName(out string p_strName)
		{
			p_strName="";
			string strSQL = "select hospital_name_chr from t_bse_hospitalinfo where hospital_no_chr='00001'";
			long lngRes =0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_strName = dtbResult.Rows[0][0].ToString();
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			return lngRes;	
		}

		/// <summary>
		/// 获取当前医院的名称及编码
		/// </summary>
		/// <param name="p_strHospitalName">医院的名称</param>
		/// <param name="p_strShortNO">医院编码(行政区划代码<7位>+序号)</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetHospitalInfo(out string p_strHospitalName, out string p_strShortNO)
		{
			p_strHospitalName = "";
			p_strShortNO = "";
			string strSQL = "select hospitalname_vchr,shortno_chr from t_aid_hospitals where usageflag_int=1";
			long lngRes =0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count == 1)
                {
                    p_strHospitalName = dtbResult.Rows[0]["HOSPITALNAME_VCHR"].ToString();
                    p_strShortNO = dtbResult.Rows[0]["SHORTNO_CHR"].ToString();
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			return lngRes;	
		}
		#endregion

	}

}