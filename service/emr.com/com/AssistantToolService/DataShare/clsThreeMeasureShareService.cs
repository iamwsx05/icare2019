using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.DataShareService
{
	/// <summary>
	/// Summary description for clsThreeMeasureShareService.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsThreeMeasureShareService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		[AutoComplete]
		public long m_lngGetFirstValue(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			
			string strFirstOracleAnd = " ";
			string strFirstOracleWhere = " ";
			string strFirstSQL2000 = " ";
			string strSQL=string.Empty;
			if(clsHRPTableService.bytDatabase_Selector == 2)
			{
                #region oracle
                strFirstOracleAnd = " and rownum = 1 ";
                strFirstOracleWhere = " where rownum = 1 ";
                strFirstSQL2000 = "";
                strSQL = @"select dv.diastolicvalue, sv.systolicvalue, d2v.diastolicvalue2, s2v.systolicvalue2, pv.pulsevalue, tv.temperaturevalue, bv.breathvalue
								from
								(select  diastolicvalue
									from 
									(---------------------------------
									select   diastolicvalue
									from threemeasurerecord a,threemeasurerecordcontent b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurerecordcontent
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate ) base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and diastolicvalue is not null  and diastolicvalue='1' and rownum = 1
									---------------------------------
									) 
									
								) dv ,
								(select   systolicvalue
									from
									(-----------------------------
									select   systolicvalue
									from threemeasurerecord a,threemeasurerecordcontent b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurerecordcontent
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate ) base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and systolicvalue is not null  and systolicvalue='1' and rownum = 1
									----------------------------
									) 
									
								) sv,
								(select  diastolicvalue2
									from 
									(---------------------
									select   diastolicvalue2
									from threemeasurerecord a,threemeasurerecordcontent b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurerecordcontent
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate ) base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and diastolicvalue2 is not null and diastolicvalue2='1' and rownum = 1
									------------------------
									)
									 
								) d2v,
								(select   systolicvalue2
									from 
									(-----------------
									select   systolicvalue2
									from threemeasurerecord a,threemeasurerecordcontent b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurerecordcontent
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate )base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and systolicvalue2 is not null and systolicvalue2='1' and rownum = 1
									-------------------
									)
									
								)s2v,
								(select   pulsevalue
									from 
									(---------------
									select  pulsevalue
									from threemeasurerecord a,threemeasurereccontaccess b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurereccontaccess
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate )base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and pulsevalue is not null and pulsevalue='1' and rownum = 1
									--------------
									) 
									
								)pv,
								(select   temperaturevalue
									from 
									(----------------
									select  temperaturevalue
									from threemeasurerecord a,threemeasurereccontaccess b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurereccontaccess
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate )base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and temperaturevalue is not null and temperaturevalue='1' and rownum = 1
									------------------
									)
								) tv,
								(select " + strFirstSQL2000 + @" breathvalue
									from 
									(-----------------
									select  breathvalue
									from threemeasurerecord a,threemeasurereccontaccess b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurereccontaccess
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate )base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and breathvalue is not null and breathvalue='1' and rownum = 1
									-----------------------
									)
								
								)bv"; 
                #endregion

			}
            else if (clsHRPTableService.bytDatabase_Selector == 0)
			{
                #region sqlserver
                strFirstOracleAnd = "";
                strFirstOracleWhere = "";
                strFirstSQL2000 = " top 1 ";
                strSQL = @"select dv.diastolicvalue, sv.systolicvalue, d2v.diastolicvalue2, s2v.systolicvalue2, pv.pulsevalue, tv.temperaturevalue,  bv.breathvalue
								from
								(select " + strFirstSQL2000 + @" diastolicvalue
								from(select '1' as dd) dumm full outer join
								(---------------------------------
								select " + strFirstSQL2000 + @" diastolicvalue
								from threemeasurerecord a,threemeasurerecordcontent b,
								(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
								from threemeasurerecordcontent
                                where inpatientid = ?
								and inpatientdate = ?
								group by inpatientid,inpatientdate,opendate ) base
								where b.inpatientid = a.inpatientid
								and b.inpatientdate = a.inpatientdate
								and b.opendate = a.opendate
								and b.inpatientid = base.inpatientid
								and b.inpatientdate = base.inpatientdate
								and b.opendate = base.opendate
								and b.modifydate = base.modifydate
								and a.status = 0
								and diastolicvalue is not null " + strFirstOracleAnd + @"
								---------------------------------
								) dv
								on dumm.dd = dv.diastolicvalue " + strFirstOracleWhere + @"
								order by diastolicvalue desc
								) dv ,
								(select " + strFirstSQL2000 + @" systolicvalue
								from(select '1' as dd
								) dumm full outer join
								(-----------------------------
								select " + strFirstSQL2000 + @" systolicvalue
								from threemeasurerecord a,threemeasurerecordcontent b,
								(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
								from threemeasurerecordcontent
                                where inpatientid = ?
								and inpatientdate = ?
								group by inpatientid,inpatientdate,opendate ) base
								where b.inpatientid = a.inpatientid
								and b.inpatientdate = a.inpatientdate
								and b.opendate = a.opendate
								and b.inpatientid = base.inpatientid
								and b.inpatientdate = base.inpatientdate
								and b.opendate = base.opendate
								and b.modifydate = base.modifydate
								and a.status = 0
								and systolicvalue is not null " + strFirstOracleAnd + @"
								----------------------------
								) sv
								on dumm.dd = sv.systolicvalue " + strFirstOracleWhere + @"
								order by systolicvalue desc
								) sv,
								(select " + strFirstSQL2000 + @" diastolicvalue2
								from(select '1' as dd
								) dumm full outer join
								(---------------------
								select " + strFirstSQL2000 + @" diastolicvalue2
								from threemeasurerecord a,threemeasurerecordcontent b,
								(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
								from threemeasurerecordcontent
                                where inpatientid = ?
								and inpatientdate = ?
								group by inpatientid,inpatientdate,opendate ) base
								where b.inpatientid = a.inpatientid
								and b.inpatientdate = a.inpatientdate
								and b.opendate = a.opendate
								and b.inpatientid = base.inpatientid
								and b.inpatientdate = base.inpatientdate
								and b.opendate = base.opendate
								and b.modifydate = base.modifydate
								and a.status = 0
								and diastolicvalue2 is not null " + strFirstOracleWhere + @"
								------------------------
								) d2v
								on dumm.dd = d2v.diastolicvalue2 " + strFirstOracleWhere + @"
								order by diastolicvalue2 desc
								) d2v,
								(select " + strFirstSQL2000 + @" systolicvalue2
								from(select '1' as dd
								) dumm full outer join
								(-----------------
								select " + strFirstSQL2000 + @" systolicvalue2
								from threemeasurerecord a,threemeasurerecordcontent b,
								(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
								from threemeasurerecordcontent
                                where inpatientid = ?
								and inpatientdate = ?
								group by inpatientid,inpatientdate,opendate )base
								where b.inpatientid = a.inpatientid
								and b.inpatientdate = a.inpatientdate
								and b.opendate = a.opendate
								and b.inpatientid = base.inpatientid
								and b.inpatientdate = base.inpatientdate
								and b.opendate = base.opendate
								and b.modifydate = base.modifydate
								and a.status = 0
								and systolicvalue2 is not null " + strFirstOracleWhere + @"
								-------------------
								)s2v
								on dumm.dd = s2v.systolicvalue2 " + strFirstOracleWhere + @"
								order by systolicvalue2 desc
								)s2v,
								(select " + strFirstSQL2000 + @" pulsevalue
								from(select '1' as dd
								) dumm full outer join
								(---------------
								select " + strFirstSQL2000 + @" pulsevalue
								from threemeasurerecord a,threemeasurereccontaccess b,
								(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
								from threemeasurereccontaccess
                                where inpatientid = ?
								and inpatientdate = ?
								group by inpatientid,inpatientdate,opendate )base
								where b.inpatientid = a.inpatientid
								and b.inpatientdate = a.inpatientdate
								and b.opendate = a.opendate
								and b.inpatientid = base.inpatientid
								and b.inpatientdate = base.inpatientdate
								and b.opendate = base.opendate
								and b.modifydate = base.modifydate
								and a.status = 0
								and pulsevalue is not null " + strFirstOracleWhere + @"
								--------------
								)pv
								on dumm.dd = pv.pulsevalue " + strFirstOracleWhere + @"
								order by pulsevalue desc
								)pv,
								(select " + strFirstSQL2000 + @" temperaturevalue
								from(select '1' as dd
								)dumm full outer join
								(----------------
								select " + strFirstSQL2000 + @" temperaturevalue
								from threemeasurerecord a,threemeasurereccontaccess b,
								(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
								from threemeasurereccontaccess
                                where inpatientid = ?
								and inpatientdate = ?
								group by inpatientid,inpatientdate,opendate )base
								where b.inpatientid = a.inpatientid
								and b.inpatientdate = a.inpatientdate
								and b.opendate = a.opendate
								and b.inpatientid = base.inpatientid
								and b.inpatientdate = base.inpatientdate
								and b.opendate = base.opendate
								and b.modifydate = base.modifydate
								and a.status = 0
								and temperaturevalue is not null " + strFirstOracleWhere + @"
								------------------
								)tv
								on dumm.dd = tv.temperaturevalue " + strFirstOracleWhere + @"
								order by temperaturevalue desc
								) tv,
								(select " + strFirstSQL2000 + @" breathvalue
								from(select '1' as dd
								) dumm full outer join
								(-----------------
								select " + strFirstSQL2000 + @" breathvalue
								from threemeasurerecord a,threemeasurereccontaccess b,
								(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
								from threemeasurereccontaccess
                                where inpatientid = ?
								and inpatientdate = ?
								group by inpatientid,inpatientdate,opendate )base
								where b.inpatientid = a.inpatientid
								and b.inpatientdate = a.inpatientdate
								and b.opendate = a.opendate
								and b.inpatientid = base.inpatientid
								and b.inpatientdate = base.inpatientdate
								and b.opendate = base.opendate
								and b.modifydate = base.modifydate
								and a.status = 0
								and breathvalue is not null " + strFirstOracleWhere + @"
								-----------------------
								)bv
								on dumm.dd = bv.breathvalue " + strFirstOracleWhere + @"
								order by breathvalue desc
								)bv"; 
                #endregion

			}
            else if (clsHRPTableService.bytDatabase_Selector == 4)
            {
                #region db2
                strFirstOracleAnd = " and rownum = 1 ";
                strFirstOracleWhere = " where rownum = 1 ";
                strFirstSQL2000 = "";
                strSQL = @"select dv.diastolicvalue, sv.systolicvalue, d2v.diastolicvalue2, s2v.systolicvalue2, pv.pulsevalue, tv.temperaturevalue, bv.breathvalue
								from
								(select  diastolicvalue
									from 
									(---------------------------------
									select   diastolicvalue
									from threemeasurerecord a,threemeasurerecordcontent b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurerecordcontent
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate ) base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and diastolicvalue is not null  and diastolicvalue='1' fetch first 1 row only
									---------------------------------
									) 
									
								) dv ,
								(select   systolicvalue
									from
									(-----------------------------
									select   systolicvalue
									from threemeasurerecord a,threemeasurerecordcontent b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurerecordcontent
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate ) base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and systolicvalue is not null  and systolicvalue='1' fetch first 1 row only
									----------------------------
									) 
									
								) sv,
								(select  diastolicvalue2
									from 
									(---------------------
									select   diastolicvalue2
									from threemeasurerecord a,threemeasurerecordcontent b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurerecordcontent
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate ) base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and diastolicvalue2 is not null and diastolicvalue2='1' fetch first 1 row only
									------------------------
									)
									 
								) d2v,
								(select   systolicvalue2
									from 
									(-----------------
									select   systolicvalue2
									from threemeasurerecord a,threemeasurerecordcontent b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurerecordcontent
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate )base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and systolicvalue2 is not null and systolicvalue2='1' fetch first 1 row only
									-------------------
									)
									
								)s2v,
								(select   pulsevalue
									from 
									(---------------
									select  pulsevalue
									from threemeasurerecord a,threemeasurereccontaccess b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurereccontaccess
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate )base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and pulsevalue is not null and pulsevalue='1' fetch first 1 row only
									--------------
									) 
									
								)pv,
								(select   temperaturevalue
									from 
									(----------------
									select  temperaturevalue
									from threemeasurerecord a,threemeasurereccontaccess b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurereccontaccess
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate )base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and temperaturevalue is not null and temperaturevalue='1' fetch first 1 row only
									------------------
									)
								) tv,
								(select " + strFirstSQL2000 + @" breathvalue
									from 
									(-----------------
									select  breathvalue
									from threemeasurerecord a,threemeasurereccontaccess b,
										(select inpatientid,inpatientdate,opendate,max(modifydate) as modifydate
										from threemeasurereccontaccess
                                        where inpatientid = ?
										and inpatientdate = ?
										group by inpatientid,inpatientdate,opendate )base
									where b.inpatientid = a.inpatientid
									and b.inpatientdate = a.inpatientdate
									and b.opendate = a.opendate
									and b.inpatientid = base.inpatientid
									and b.inpatientdate = base.inpatientdate
									and b.opendate = base.opendate
									and b.modifydate = base.modifydate
									and a.status = 0
									and breathvalue is not null and breathvalue='1' fetch first 1 row only
									-----------------------
									)
								
								)bv";
                #endregion
            }

			#endregion SQL

			p_dtbResult = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(14, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strInPaitentID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].Value = p_strInPaitentID;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[6].Value = p_strInPaitentID;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[8].Value = p_strInPaitentID;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[10].Value = p_strInPaitentID;
                objDPArr[11].DbType = DbType.DateTime;
                objDPArr[11].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[12].Value = p_strInPaitentID;
                objDPArr[13].DbType = DbType.DateTime;
                objDPArr[13].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;	
		
		}

		/// <summary>
		/// 获取最邻近时间的数据
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtmCompare"></param>
		/// <param name="p_strResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetNearestValue(string p_strInPaitentID,string p_strInPatientDate,DateTime p_dtmCompare, out string[] p_strResult)
		{
			p_strResult = new string[4];
            
			for(int i = 0; i < p_strResult.Length; i++)
			{
				string strSql = "";
				switch(i)
				{
					case 0:
						#region 体温
						strSql = @"select b.createtime,b.temperaturevalue
from threemeasurerecord  a 
inner join threemeasurereccontaccess  b
on a.inpatientid = b.inpatientid
and a.inpatientdate = b.inpatientdate
and a.opendate = b.opendate
where a.inpatientid = ?
and a.inpatientdate = ?
and a.status =0
and b.temperaturevalue is not null
and b.modifydate in 
(select max(modifydate) as modifydate 
from threemeasurerecordcontent
where inpatientid = ?
and inpatientdate = ?
group by inpatientid,inpatientdate,opendate )";
			#endregion
						break;
					case 1:
						#region 脉搏
						strSql = @"select b.createtime,b.pulsevalue
from threemeasurerecord  a 
inner join threemeasurereccontaccess  b
on a.inpatientid = b.inpatientid
and a.inpatientdate = b.inpatientdate
and a.opendate = b.opendate
where a.inpatientid = ?
and a.inpatientdate = ?
and a.status =0
and b.pulsevalue is not null
and b.modifydate in 
(select max(modifydate) as modifydate 
from threemeasurerecordcontent
where inpatientid = ?
and inpatientdate = ?
group by inpatientid,inpatientdate,opendate )";
			#endregion
						break;
					case 2:
						#region 呼吸
						strSql = @"select b.createtime,b.breathvalue
from threemeasurerecord  a 
inner join threemeasurereccontaccess  b
on a.inpatientid = b.inpatientid
and a.inpatientdate = b.inpatientdate
and a.opendate = b.opendate
where a.inpatientid = ?
and a.inpatientdate = ?
and a.status =0
and b.breathvalue is not null
and b.modifydate in 
(select max(modifydate) as modifydate 
from threemeasurerecordcontent
where inpatientid = ?
and inpatientdate = ?
group by inpatientid,inpatientdate,opendate )";
			#endregion
						break;
					case 3:
						#region 血压
						strSql = @"select a.createdate as createtime,(b.systolicvalue) + '/' + b.diastolicvalue) as pressurevalue
from threemeasurerecord  a 
inner join threemeasurerecordcontent  b
on a.inpatientid = b.inpatientid
and a.inpatientdate = b.inpatientdate
and a.opendate = b.opendate
where a.inpatientid = ?
and a.inpatientdate =?
and a.status =0
and b.diastolicvalue is not null
and b.systolicvalue is not null
and b.modifydate in 
(select max(modifydate) as modifydate 
from threemeasurerecordcontent
where inpatientid = ?
and inpatientdate = ?
group by inpatientid,inpatientdate,opendate )";
						#endregion
						break;
				}
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strInPaitentID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);

                p_strResult[i] = m_strGetNearestValue(p_dtmCompare, strSql, objDPArr);
			}
	
			return 1;
		}
		
		[AutoComplete]
        private string m_strGetNearestValue(DateTime p_dtmCompare, string p_strSql, IDataParameter[] p_objDPArr)
		{
			DataTable dtResult = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.lngGetDataTableWithParameters(p_strSql, ref dtResult, p_objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }

			if(lngRes <=0 || dtResult.Rows.Count == 0)
				return "";

			TimeSpan tsPre =  p_dtmCompare - DateTime.Parse(dtResult.Rows[0]["CREATETIME"].ToString()).AddHours(2);//4pm -> 14:00:00
			int intNearestIndex = 0;

			for(int i = 1; i < dtResult.Rows.Count; i++)
			{
				TimeSpan tsNow = p_dtmCompare - DateTime.Parse(dtResult.Rows[i][0].ToString()).AddHours(2);

				if(tsNow.Duration() < tsPre.Duration())
				{
					intNearestIndex = i;
					tsPre = tsNow;
				}
			}

			return dtResult.Rows[intNearestIndex][1].ToString();
		}

		/// <summary>
		/// 获取最新的三测表的记录
		/// </summary>
		/// <param name="p_strInpatientID">住院号</param>
		/// <param name="p_strInPatientDate">住院日期</param>
		/// <param name="p_dtbResult">结果:
		/// 列名               说明
		/// DIASTOLICVALUE-----舒张压,
		/// SYSTOLICVALUE------收缩压,
		/// DIASTOLICVALUE2----舒张压2,
		/// SYSTOLICVALUE2-----收缩压2,
		/// PULSEVALUE---------脉搏,
		/// TEMPERATUREVALUE---体温,
		/// BREATHVALUE--------呼吸,
		/// WEIGHTVALUE--------体重
		/// </param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLastValue(string p_strInPaitentID,string p_strInPatientDate,out DataTable p_dtbResult)
		{
			p_dtbResult = new DataTable();
			try
			{
				#region SQL
			
				string strSQL=string.Empty;
				if(clsHRPTableService.bytDatabase_Selector == 2)
				{
                    #region oracle
                    strSQL = @"select
---------------------------------
 (select diastolicvalue
    from (select nvl(diastolicvalue2,diastolicvalue) diastolicvalue
            from threemeasurerecord a,
                 threemeasurerecordcontent b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurerecordcontent
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and diastolicvalue is not null
           order by a.createdate desc)
   where rownum = 1) as diastolicvalue,
 ---------------------------------
 (select systolicvalue
    from (select nvl(systolicvalue2,systolicvalue) systolicvalue
            from threemeasurerecord a,
                 threemeasurerecordcontent b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurerecordcontent
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and systolicvalue is not null
           order by a.createdate desc)
   where rownum = 1) as systolicvalue,
 ---------------------
 (select pulsevalue
    from (select pulsevalue
            from threemeasurerecord a,
                 threemeasurereccontaccess b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and pulsevalue is not null
           order by a.createdate desc,b.createtime desc)
   where rownum = 1) as pulsevalue,
 ----------------
 (select temperaturevalue
    from (select temperaturevalue
            from threemeasurerecord a,
                 threemeasurereccontaccess b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and temperaturevalue is not null
           order by a.createdate desc,b.createtime desc)
   where rownum = 1) as temperaturevalue,
 ------------------
 (select breathvalue
    from (select breathvalue
            from threemeasurerecord a,
                 threemeasurereccontaccess b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and breathvalue is not null
           order by a.createdate desc,b.createtime desc)
   where rownum = 1) as breathvalue,
 -----------------------
 (select weightvalue
    from (select weightvalue
            from threemeasurerecord a,
                 threemeasurerecordcontent b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and weightvalue is not null
           order by a.createdate desc)
   where rownum = 1) as weightvalue
-----------------------
  from dual";

                } 
                    #endregion
                else if (clsHRPTableService.bytDatabase_Selector == 0)
				{
                    #region Sqlserver
                    strSQL = @"select
---------------------------------
 (select top 1 isnull(diastolicvalue2,diastolicvalue)
            from threemeasurerecord a,
                 threemeasurerecordcontent b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurerecordcontent
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and diastolicvalue is not null
           order by a.createdate desc) as diastolicvalue,
 ---------------------------------
 (select  top 1  isnull(systolicvalue2,systolicvalue)
            from threemeasurerecord a,
                 threemeasurerecordcontent b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurerecordcontent
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and systolicvalue is not null
           order by a.createdate desc) as systolicvalue,
 ---------------------
 (select  top 1  pulsevalue
            from threemeasurerecord a,
                 threemeasurereccontaccess b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and pulsevalue is not null
           order by a.createdate desc,b.createtime desc) as pulsevalue,
 ----------------
 (select  top 1  temperaturevalue
            from threemeasurerecord a,
                 threemeasurereccontaccess b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and temperaturevalue is not null
           order by a.createdate desc,b.createtime desc) as temperaturevalue,
 ------------------
 (select  top 1  breathvalue
            from threemeasurerecord a,
                 threemeasurereccontaccess b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and breathvalue is not null
           order by a.createdate desc,b.createtime desc) as breathvalue,
 -----------------------
 (select  top 1  weightvalue
            from threemeasurerecord a,
                 threemeasurerecordcontent b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and weightvalue is not null
           order by a.createdate desc) as weightvalue
-----------------------
"; 
                    #endregion

				}
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    #region db2
                    strSQL = @"select
---------------------------------
  (select isnull(diastolicvalue2,diastolicvalue)
            from threemeasurerecord a,
                 threemeasurerecordcontent b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurerecordcontent
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and diastolicvalue is not null
           order by a.createdate desc fetch first 1 row only
   ) as diastolicvalue,
 ---------------------------------
  (select isnull(systolicvalue2,systolicvalue)
            from threemeasurerecord a,
                 threemeasurerecordcontent b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurerecordcontent
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and systolicvalue is not null
           order by a.createdate desc fetch first 1 row only
  ) as systolicvalue,
 ---------------------
 ( select pulsevalue
            from threemeasurerecord a,
                 threemeasurereccontaccess b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and pulsevalue is not null
           order by a.createdate desc,b.createtime desc fetch first 1 row only
    ) as pulsevalue,
 ----------------
 ( select temperaturevalue
            from threemeasurerecord a,
                 threemeasurereccontaccess b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and temperaturevalue is not null
           order by a.createdate desc,b.createtime desc 
    ) as temperaturevalue,
 ------------------
 ( select breathvalue
            from threemeasurerecord a,
                 threemeasurereccontaccess b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and breathvalue is not null
           order by a.createdate desc,b.createtime desc  fetch first 1 row only) as breathvalue,
 -----------------------
 (select weightvalue
            from threemeasurerecord a,
                 threemeasurerecordcontent b,
                 (select inpatientid,
                         inpatientdate,
                         opendate,
                         max(modifydate) as modifydate
                    from threemeasurereccontaccess
                    where inpatientid = ? and inpatientdate = ?
                   group by inpatientid, inpatientdate, opendate ) base
           where b.inpatientid = a.inpatientid
             and b.inpatientdate = a.inpatientdate
             and b.opendate = a.opendate
             and b.inpatientid = base.inpatientid
             and b.inpatientdate = base.inpatientdate
             and b.opendate = base.opendate
             and b.modifydate = base.modifydate
             and a.status = 0
             and weightvalue is not null
           order by a.createdate desc  fetch first 1 row only) as weightvalue
-----------------------
  from dual";

                    #endregion                
            }
				
				#endregion SQL


				
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                DateTime dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(12, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = dtmInPatientDate;
                objDPArr[2].Value = p_strInPaitentID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = dtmInPatientDate;
                objDPArr[4].Value = p_strInPaitentID;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = dtmInPatientDate;
                objDPArr[6].Value = p_strInPaitentID;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = dtmInPatientDate;
                objDPArr[8].Value = p_strInPaitentID;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = dtmInPatientDate;
                objDPArr[10].Value = p_strInPaitentID;
                objDPArr[11].DbType = DbType.DateTime;
                objDPArr[11].Value = dtmInPatientDate;

				lngRes=objHRPServ.lngGetDataTableWithParameters (strSQL, ref p_dtbResult,objDPArr); 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;

			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return 0;
		}



	}
}
