using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Entity;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    public delegate void HandleItemMouseClick(object sender, EntityFormCtrl Efc);

    public delegate void HandleItemClick(object sender, EntityFormCtrl Efc);

    /// <summary>
    /// PatientInfoHelper
    /// </summary>
    public class PatientInfoHelper
    {
        public static string GetTypePatientInfo(EnumPatientInfoType patientInfoType)
        {
            return GetTypePatientInfo(patientInfoType, GlobalPatient.currPatient);
        }

        /// <summary>
        /// 获取转科转床信息
        /// </summary>
        /// <param name="p_lstTransInfo"></param>
        /// <param name="p_intDeptID"></param>
        /// <param name="p_intType">1 科室 2 病区 3 床号</param>
        /// <returns></returns>
        public static string GetTransInfo(List<EntityPatientTransDept> p_lstTransInfo, string p_intDeptID, int p_intType)
        {
            string strInfo = string.Empty;
            List<string> lstCheck = new List<string>();

            var info = from transinfo in p_lstTransInfo
                       where (transinfo.DeptID == p_intDeptID)
                       select transinfo;
            if (info.ToArray().Length > 0)
            {
                if (p_intType == 1)
                {
                    strInfo = (info.ToArray())[0].DeptName;
                    //for (int i = 0; i < p_lstTransInfo.Count; i++)
                    //{
                    //    if (lstCheck.IndexOf(p_lstTransInfo[i].strDeptName) < 0)
                    //    {
                    //        strInfo += p_lstTransInfo[i].strDeptName + "->";
                    //        lstCheck.Add(p_lstTransInfo[i].strDeptName);
                    //    }
                    //}
                    //if (strInfo.Length > 2)
                    //{
                    //    strInfo = strInfo.Substring(0, strInfo.Length - 2);
                    //}
                }
                else if (p_intType == 2)
                {
                    strInfo = (info.ToArray())[0].AreaName;
                }
                else if (p_intType == 3)
                {
                    strInfo = (info.ToArray())[0].BedNO;
                    //for (int i = 0; i < p_lstTransInfo.Count; i++)
                    //{
                    //    if (lstCheck.IndexOf(p_lstTransInfo[i].strBedNO) < 0)
                    //    {
                    //        strInfo += p_lstTransInfo[i].strBedNO + "->";
                    //        lstCheck.Add(p_lstTransInfo[i].strBedNO);
                    //    }
                    //}
                    //if (strInfo.Length > 2)
                    //{
                    //    strInfo = strInfo.Substring(0, strInfo.Length - 2);
                    //}
                }
            }
            return strInfo;
        }

        /// <summary>
        /// GetTypePatientInfo
        /// </summary>
        /// <param name="patientInfoType"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public static string GetTypePatientInfo(EnumPatientInfoType patientInfoType, EntityPatient patientInfo)
        {
            string strReturn = string.Empty;

            if (patientInfo != null)
            {
                List<EntityPatientTransDept> lstTransInfo = null;
                int intPatType = patientInfo.PatType;
                string deptCode = string.Empty;
                if (intPatType == 1 && patientInfoType != EnumPatientInfoType.医院名称)
                {
                    lstTransInfo = patientInfo.PatientTransDeptInfo;
                    deptCode = GlobalLogin.objLogin.DeptCode;
                }
                switch (patientInfoType)
                {
                    case EnumPatientInfoType.病床号:
                        {
                            if (lstTransInfo != null)
                            {
                                strReturn = GetTransInfo(lstTransInfo, deptCode, 3);
                            }
                            else
                            {
                                strReturn = patientInfo.BedNo;
                            }
                            break;
                        }
                    case EnumPatientInfoType.病区:
                        {
                            if (lstTransInfo != null)
                            {
                                strReturn = GetTransInfo(lstTransInfo, deptCode, 2);
                            }
                            else
                            {
                                strReturn = patientInfo.AreaName;
                            }
                            break;
                        }
                    case EnumPatientInfoType.出生地:
                        {
                            strReturn = patientInfo.BirthPlace;
                            break;
                        }
                    case EnumPatientInfoType.出生日期:
                        {
                            if (GlobalHospital.HospitalCode == "0002")
                            {
                                if (patientInfo.Birthday.Hour == 0 && patientInfo.Birthday.Minute == 0)
                                {
                                    strReturn = patientInfo.Birthday.ToString("yyyy年MM月dd日");
                                }
                                else
                                {
                                    strReturn = patientInfo.Birthday.ToString("yyyy年MM月dd日 HH时mm分");
                                }
                            }
                            else
                            {
                                strReturn = patientInfo.Birthday.ToString("yyyy年MM月dd日");
                            }
                            if (strReturn.Contains("0001")) strReturn = string.Empty;
                            break;
                        }
                    case EnumPatientInfoType.费别:
                        {
                            //strReturn = patientInfo.PayType;
                            break;
                        }
                    case EnumPatientInfoType.工作单位:
                        {
                            strReturn = patientInfo.WorkUnit;
                            break;
                        }
                    case EnumPatientInfoType.国籍:
                        {
                            strReturn = patientInfo.CitizenShip;
                            break;
                        }
                    case EnumPatientInfoType.婚姻:
                        {
                            strReturn = patientInfo.Marriage;
                            break;
                        }
                    case EnumPatientInfoType.籍贯:
                        {
                            strReturn = patientInfo.NativePlace;
                            break;
                        }
                    case EnumPatientInfoType.家庭地址:
                        {
                            strReturn = patientInfo.HomeAddr;
                            break;
                        }
                    case EnumPatientInfoType.科室:
                        {
                            if (lstTransInfo != null)
                            {
                                strReturn = GetTransInfo(lstTransInfo, deptCode, 1);
                            }
                            else
                            {
                                strReturn = patientInfo.DeptName;
                            }
                            break;
                        }
                    case EnumPatientInfoType.联系电话:
                        {
                            strReturn = patientInfo.HomeTel;
                            break;
                        }
                    case EnumPatientInfoType.联系人:
                        {
                            strReturn = patientInfo.ContactName;
                            break;
                        }
                    case EnumPatientInfoType.联系人电话:
                        {
                            strReturn = patientInfo.ContactTel;
                            break;
                        }
                    case EnumPatientInfoType.民族:
                        {
                            strReturn = patientInfo.Nationality;
                            break;
                        }
                    case EnumPatientInfoType.年龄:
                        {
                            strReturn = patientInfo.Age;
                            break;
                        }
                    case EnumPatientInfoType.入院日期:
                        {
                            //if (GlobalCase.objCaseInfo != null)
                            //{
                            //    if (GlobalSysParameter.dicSysParameter[21].Split(';').ToList().IndexOf(GlobalCase.objCaseInfo.strCaseCode) >= 0)
                            //        strReturn = patientInfo.dtmRegisterDate.ToString("yyyy-MM-dd HH:mm");
                            //    else
                            //        strReturn = patientInfo.dtmRegisterDate.ToString("yyyy-MM-dd");
                            //}
                            //else
                            //{
                            strReturn = patientInfo.RegisterDate.ToString("yyyy-MM-dd");
                            //}
                            if (strReturn == "0001-01-01") strReturn = string.Empty;
                            break;
                        }
                    case EnumPatientInfoType.出院日期:
                        if (patientInfo.OutDate == null)
                            return string.Empty;
                        else
                            strReturn = patientInfo.OutDate.Value.ToString("yyyy-MM-dd");
                        if (strReturn == "0001-01-01") strReturn = string.Empty;
                        break;
                    case EnumPatientInfoType.住院天数:
                        {
                            TimeSpan ts = new TimeSpan();
                            if (GlobalPatient.currPatient.OutDate == null)
                                ts = Convert.ToDateTime(DateTime.Now.ToShortDateString()) - Convert.ToDateTime(patientInfo.RegisterDate.ToShortDateString());
                            else
                                ts = Convert.ToDateTime(GlobalPatient.currPatient.OutDate.Value.ToShortDateString()) - Convert.ToDateTime(patientInfo.RegisterDate.ToShortDateString());
                            if (ts.Days <= 0)
                                strReturn = ts.Hours.ToString() + "小时";
                            else
                                strReturn = ts.Days.ToString() + "天";
                            break;
                        }
                    case EnumPatientInfoType.身份证号:
                        {
                            strReturn = patientInfo.IdCard;
                            break;
                        }
                    case EnumPatientInfoType.身高:
                        {
                            strReturn = patientInfo.Stature;
                            break;
                        }
                    case EnumPatientInfoType.体重:
                        {
                            strReturn = patientInfo.Weight;
                            break;
                        }
                    case EnumPatientInfoType.姓名:
                        {
                            strReturn = patientInfo.PatientName;
                            break;
                        }
                    case EnumPatientInfoType.性别:
                        {
                            strReturn = patientInfo.SexCH;
                            break;
                        }
                    case EnumPatientInfoType.血型:
                        {
                            strReturn = patientInfo.Blood;
                            break;
                        }
                    case EnumPatientInfoType.医院名称:
                        {
                            strReturn = patientInfo.HospitalName;
                            break;
                        }
                    case EnumPatientInfoType.职业:
                        {
                            strReturn = patientInfo.Occupation;
                            break;
                        }
                    case EnumPatientInfoType.住院号:
                        {
                            strReturn = patientInfo.PatientIpNo;
                            break;
                        }
                    case EnumPatientInfoType.住院次数:
                        {
                            if (patientInfo.IpTimes <= 0)
                                strReturn = string.Empty;
                            else
                                strReturn = patientInfo.IpTimes.ToString();
                            break;
                        }
                    case EnumPatientInfoType.门诊号:
                        strReturn = patientInfo.PatientOpNo;
                        break;
                    case EnumPatientInfoType.文化程度:
                        strReturn = patientInfo.Education;
                        break;
                    case EnumPatientInfoType.门诊住院号:
                        if (!string.IsNullOrEmpty(patientInfo.PatientIpNo))
                            strReturn = patientInfo.PatientIpNo;
                        else
                            strReturn = patientInfo.CardNo; //patientInfo.PatientOpNo;
                        break;
                    case EnumPatientInfoType.户口地址:
                        strReturn = patientInfo.patHkadr;
                        break;
                    case EnumPatientInfoType.与联系人关系:
                        strReturn = patientInfo.patContactRelation;
                        break;
                    case EnumPatientInfoType.体温:
                        strReturn = patientInfo.patTemperature;
                        break;
                    case EnumPatientInfoType.脉搏:
                        strReturn = patientInfo.patPulse;
                        break;
                    case EnumPatientInfoType.呼吸:
                        strReturn = patientInfo.patBreath;
                        break;
                    case EnumPatientInfoType.血压:
                        strReturn = patientInfo.patBloodPressure;
                        break;
                    default: break;
                }
            }
            else
            {
                return null;
            }
            if (GlobalHospital.objHospital != null)
            {
                switch (patientInfoType)
                {
                    case EnumPatientInfoType.医院名称:
                        {
                            strReturn = GlobalHospital.objHospital.Hospitalname;
                            break;
                        }
                    default: break;
                }
            }
            return strReturn;
        }
    }
}
