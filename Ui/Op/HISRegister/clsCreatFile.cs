using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Data;
using com.digitalwave.iCare.common;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsCreatFile ��ժҪ˵����
    /// </summary>
    public class clsCreatFile : IDisposable
    {
        private clsDcl_ShowReports objSvc;
        public clsCreatFile()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            objSvc = new clsDcl_ShowReports();
        }
        string strHospitalName = "";
        /// <summary>
        /// ����������
        /// </summary>
        private string[] objArr;
        public clsCreatFile(string[] p_objArr)
        {
            objArr = p_objArr;
            strHospitalName = new clsCommmonInfo().m_strGetHospitalTitle();
        }
        /// <summary>
        /// ���û��ȡ����������
        /// </summary>
        public string[] RecipeArray
        {
            set
            {
                objArr = value;
            }
            get
            {
                return objArr;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public void m_mthCreatFile()
        {
            if (objArr == null || objArr.Length == 0)
            {
                return;
            }
            SaveFileDialog objDlg = new SaveFileDialog();
            objDlg.Title = "��ѡ�񱣴�·��";
            objDlg.OverwritePrompt = true;
            objDlg.CheckPathExists = true;
            objDlg.AddExtension = true;
            objDlg.FileName = "�ļ���";
            objDlg.DefaultExt = "emr";
            objDlg.Filter = "�嫴����ļ� (*.emr)|*.emr|�����ļ� (*.*)|*.*";
            if (objDlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string[] strPath = objDlg.FileName.Split('\\');
            string strpath = "";
            for (int p = 0; p < strPath.Length - 1; p++)
            {
                strpath += strPath[p] + "\\";
            }
            dwtProcessBar objProcessBar = new dwtProcessBar("���������ļ�,���Ժ�...");
            objProcessBar.IsCanCancel = true;
            objProcessBar.m_mthSetMaxValue(objArr.Length);
            try
            {
                objProcessBar.Show();
                for (int i = 0; i < objArr.Length; i++)
                {
                    if (objProcessBar.IsCancel)
                    {
                        break;
                    }
                    clsOutpatientPrintRecipe_VO m_objSetValue = m_mthGetPrintVo(objArr[i].Trim());
                    string path = strpath;
                    path += m_objSetValue.m_strDiagDrName + m_objSetValue.m_strPatientType.Replace("/", "") + m_objSetValue.strInvoiceNO + ".emr";
                    IFormatter objForm = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    Stream objStream = new System.IO.FileStream(path, FileMode.Create);
                    objForm.Serialize(objStream, m_objSetValue);
                    objStream.Flush();
                    objStream.Close();
                    objProcessBar.m_mthAdd();
                    Application.DoEvents();
                }
                objProcessBar.Hide();
                MessageBox.Show("�ļ����ɳɹ�\n��������" + objProcessBar.GetValue.ToString() + "���ļ�");
                objProcessBar.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //				MessageBox.Show("�޷������ļ��������ļ����Ƿ���ֻ���ļ���������̿ռ䲻�㡣");
                objProcessBar.Close();
            }

        }
        #region ���ݴ����Ż�ȡ��ӡVO
        private clsOutpatientPrintRecipe_VO m_mthGetPrintVo(string strID)
        {
            clsOutpatientPrintRecipe_VO obj_VO = new clsOutpatientPrintRecipe_VO();
            objSvc = new clsDcl_ShowReports();
            DataTable dt;
            long ret = objSvc.m_mthGetRecipeInfo(strID, out dt);
            if (ret > 0 && dt.Rows.Count > 0)
            {
                obj_VO.m_strDiagDeptID = dt.Rows[0]["DEPTNAME_CHR"].ToString().Trim();
                obj_VO.m_strDiagDrName = dt.Rows[0]["DOCTORNAME_CHR"].ToString().Trim();
                obj_VO.m_strRegisterID = "";
                obj_VO.m_strSelfPay = dt.Rows[0]["SBSUM_MNY"].ToString().Trim();
                obj_VO.m_strChargeUp = dt.Rows[0]["ACCTSUM_MNY"].ToString().Trim();
                obj_VO.m_strRecipePrice = dt.Rows[0]["TOTALSUM_MNY"].ToString().Trim();
                obj_VO.m_strPrintDate = dt.Rows[0]["INVDATE_DAT"].ToString().Trim().Substring(0, 10);
                obj_VO.m_strAddress = dt.Rows[0]["HOMEADDRESS_VCHR"].ToString().Trim();
                obj_VO.m_strGOVCARD = dt.Rows[0]["GOVCARD_CHR"].ToString().Trim();
                obj_VO.m_strINSURANCEID = dt.Rows[0]["INSURANCEID_VCHR"].ToString().Trim();
                DateTime dteBirth = Convert.ToDateTime(dt.Rows[0]["BIRTH_DAT"].ToString());
                obj_VO.m_strAge = clsCreatFile.s_strCalAge(dteBirth);
                obj_VO.m_strCardID = dt.Rows[0]["PATIENTCARDID_CHR"].ToString().Trim();
                obj_VO.m_strHospitalName = strHospitalName;
                obj_VO.m_strPatientName = dt.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
                obj_VO.m_strSex = dt.Rows[0]["SEX_CHR"].ToString().Trim();
                obj_VO.m_strRecipeType = dt.Rows[0]["RECIPEFLAG_INT"].ToString().Trim();
                obj_VO.m_strHerbalmedicineUsage = "";
                obj_VO.strInvoiceNO = dt.Rows[0]["INVOICENO_VCHR"].ToString().Trim();
                obj_VO.m_strRecordEmpID = dt.Rows[0]["TYPENAME_VCHR"].ToString().Trim();
                obj_VO.m_strPatientType = dt.Rows[0]["PAYTYPENAME_VCHR"].ToString().Trim();
                obj_VO.m_strdiagnose = dt.Rows[0]["DIAG_VCHR"].ToString().Trim();
            }
            obj_VO.m_strTimes = "1";
            obj_VO.m_strRecipeID = strID;

            clsDcl_DoctorWorkstation objDKSvc = new clsDcl_DoctorWorkstation();
            dt = null;
            decimal decWMedicineCost = 0;
            decimal decZCMedicineCost = 0;
            decimal decCureCost = 0;
            System.Collections.Generic.List<clsOutpatientPrintRecipeDetail_VO> objPRDArr = new System.Collections.Generic.List<clsOutpatientPrintRecipeDetail_VO>();
            string[] IDarr = null;
            objSvc.m_mthGetRecipeGroup(strID, out IDarr);
            foreach (string TempID in IDarr)
            {
                ret = objDKSvc.m_mthFindRecipeDetail1(TempID, out dt, true, false);//��ҩ
                if (ret > 0 && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        clsOutpatientPrintRecipeDetail_VO objtemp = new clsOutpatientPrintRecipeDetail_VO();
                        objtemp.m_strChargeName = dt.Rows[i]["ITEMNAME_VCHR"].ToString().Trim();
                        objtemp.m_strCount = dt.Rows[i]["TOLQTY_DEC"].ToString().Trim() + dt.Rows[i]["UNITID_CHR"].ToString();
                        objtemp.m_strPrice = clsConvertToDecimal.m_mthConvertObjToDecimal(dt.Rows[i]["UNITPRICE_MNY"]).ToString("0.00");
                        objtemp.m_strSumPrice = dt.Rows[i]["TOLPRICE_MNY"].ToString().Trim();
                        objtemp.m_strUnit = dt.Rows[i]["DOSAGEUNIT_CHR"].ToString().Trim();
                        objtemp.m_strFrequency = dt.Rows[i]["FREQNAME_CHR"].ToString().Trim();
                        objtemp.m_strDosage = dt.Rows[i]["QTY_DEC"].ToString().Trim() + dt.Rows[i]["DOSAGEUNIT_CHR"].ToString().Trim();
                        objtemp.m_strDays = dt.Rows[i]["DAYS_INT"].ToString().Trim();
                        objtemp.m_strSpec = dt.Rows[i]["ITEMSPEC_VCHR"].ToString().Trim();
                        objtemp.m_strUsage = dt.Rows[i]["USAGENAME_VCHR"].ToString().Trim();
                        objtemp.m_strRowNo = dt.Rows[i]["ROWNO_CHR"].ToString().Trim();
                        objtemp.m_strInvoiceCat = dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        decWMedicineCost += clsConvertToDecimal.m_mthConvertObjToDecimal(dt.Rows[i]["TOLPRICE_MNY"]);
                        objPRDArr.Add(objtemp);
                    }

                }
            }

            System.Collections.Generic.List<clsOutpatientPrintRecipeDetail_VO> objPRDArr2 = new System.Collections.Generic.List<clsOutpatientPrintRecipeDetail_VO>();
            foreach (string TempID in IDarr)
            {
                ret = objDKSvc.m_mthFindRecipeDetail2(TempID, out dt, true, false);//��ҩ
                if (ret > 0 && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        clsOutpatientPrintRecipeDetail_VO objtemp = new clsOutpatientPrintRecipeDetail_VO();
                        objtemp.m_strChargeName = dt.Rows[i]["ITEMNAME"].ToString().Trim();
                        objtemp.m_strDosage = dt.Rows[i]["MIN_QTY_DEC"].ToString().Trim() + dt.Rows[i]["UNIT"].ToString();
                        objtemp.m_strPrice = dt.Rows[i]["price"].ToString().Trim();
                        objtemp.m_strSumPrice = dt.Rows[i]["SUMMONEY"].ToString().Trim();
                        objtemp.m_strUsage = dt.Rows[i]["USAGENAME_VCHR"].ToString();
                        objtemp.m_strRowNo = dt.Rows[i]["ROWNO_CHR"].ToString();
                        objtemp.m_strInvoiceCat = dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        decZCMedicineCost += clsConvertToDecimal.m_mthConvertObjToDecimal(dt.Rows[i]["SUMMONEY"]);
                        objPRDArr2.Add(objtemp);

                    }
                }
            }


            obj_VO.objinjectArr = new System.Collections.Generic.List<clsOutpatientPrintRecipeDetail_VO>();
            foreach (string TempID in IDarr)
            {
                ret = objDKSvc.m_mthFindRecipeDetail3(TempID, out dt, true, false);
                if (ret > 0 && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        clsOutpatientPrintRecipeDetail_VO objtemp = new clsOutpatientPrintRecipeDetail_VO();
                        objtemp.m_strChargeName = dt.Rows[i]["ITEMNAME"].ToString(); ;
                        objtemp.m_strCount = dt.Rows[i]["quantity"].ToString().Trim();
                        objtemp.m_strPrice = dt.Rows[i]["PRICE"].ToString();
                        objtemp.m_strSumPrice = dt.Rows[i]["SUMMONEY"].ToString().Trim();
                        objtemp.m_strUnit = dt.Rows[i]["UNIT"].ToString().Trim();
                        objtemp.m_strInvoiceCat = dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        objtemp.m_strFrequency = "";
                        objtemp.m_strDosage = "";
                        objtemp.m_strDays = "";
                        objtemp.m_strUsage = "";
                        objtemp.m_strRowNo = "";
                        decCureCost += clsConvertToDecimal.m_mthConvertObjToDecimal(dt.Rows[i]["SUMMONEY"]);
                        obj_VO.objinjectArr.Add(objtemp);
                    }

                }
                ret = objDKSvc.m_mthFindRecipeDetail4(TempID, out dt, true, false);
                if (ret > 0 && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        clsOutpatientPrintRecipeDetail_VO objtemp = new clsOutpatientPrintRecipeDetail_VO();
                        objtemp.m_strChargeName = dt.Rows[i]["ITEMNAME"].ToString(); ;
                        objtemp.m_strCount = dt.Rows[i]["quantity"].ToString().Trim();
                        objtemp.m_strPrice = dt.Rows[i]["PRICE"].ToString();
                        objtemp.m_strSumPrice = dt.Rows[i]["SUMMONEY"].ToString().Trim();
                        objtemp.m_strUnit = dt.Rows[i]["UNIT"].ToString().Trim();
                        objtemp.m_strInvoiceCat = dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        objtemp.m_strFrequency = "";
                        objtemp.m_strDosage = "";
                        objtemp.m_strDays = "";
                        objtemp.m_strUsage = "";
                        objtemp.m_strRowNo = "";
                        decCureCost += clsConvertToDecimal.m_mthConvertObjToDecimal(dt.Rows[i]["SUMMONEY"]);
                        obj_VO.objinjectArr.Add(objtemp);
                    }

                }
                ret = objDKSvc.m_mthFindRecipeDetail5(TempID, out dt, true, false);
                if (ret > 0 && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        clsOutpatientPrintRecipeDetail_VO objtemp = new clsOutpatientPrintRecipeDetail_VO();
                        objtemp.m_strChargeName = dt.Rows[i]["ITEMNAME"].ToString(); ;
                        objtemp.m_strCount = dt.Rows[i]["quantity"].ToString().Trim();
                        objtemp.m_strPrice = dt.Rows[i]["PRICE"].ToString();
                        objtemp.m_strSumPrice = dt.Rows[i]["SUMMONEY"].ToString().Trim();
                        objtemp.m_strUnit = dt.Rows[i]["UNIT"].ToString().Trim();
                        objtemp.m_strInvoiceCat = dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        objtemp.m_strFrequency = "";
                        objtemp.m_strDosage = "";
                        objtemp.m_strDays = "";
                        objtemp.m_strUsage = "";
                        objtemp.m_strRowNo = "";
                        decCureCost += clsConvertToDecimal.m_mthConvertObjToDecimal(dt.Rows[i]["SUMMONEY"]);
                        obj_VO.objinjectArr.Add(objtemp);
                    }

                }
                ret = objDKSvc.m_mthFindRecipeDetail6(TempID, out dt, true, false);
                if (ret > 0 && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        clsOutpatientPrintRecipeDetail_VO objtemp = new clsOutpatientPrintRecipeDetail_VO();
                        objtemp.m_strChargeName = dt.Rows[i]["ITEMNAME"].ToString(); ;
                        objtemp.m_strCount = dt.Rows[i]["quantity"].ToString().Trim();
                        objtemp.m_strPrice = dt.Rows[i]["PRICE"].ToString();
                        objtemp.m_strSumPrice = dt.Rows[i]["SUMMONEY"].ToString().Trim();
                        objtemp.m_strUnit = dt.Rows[i]["UNIT"].ToString().Trim();
                        objtemp.m_strInvoiceCat = dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        objtemp.m_strFrequency = "";
                        objtemp.m_strDosage = "";
                        objtemp.m_strDays = "";
                        objtemp.m_strUsage = "";
                        objtemp.m_strRowNo = "";
                        decCureCost += clsConvertToDecimal.m_mthConvertObjToDecimal(dt.Rows[i]["SUMMONEY"]);
                        obj_VO.objinjectArr.Add(objtemp);
                    }

                }
            }
            obj_VO.m_strWMedicineCost = decWMedicineCost.ToString("0.00");//����������������
            obj_VO.m_strZCMedicineCost = decZCMedicineCost.ToString("0.00");
            obj_VO.m_strCureCost = decCureCost.ToString("0.00");
            objPRDArr.Sort(0, objPRDArr.Count, null);
            objPRDArr2.Sort(0, objPRDArr2.Count, null);
            obj_VO.objPRDArr = objPRDArr;
            obj_VO.objPRDArr2 = objPRDArr2;
            return obj_VO;
        }
        #endregion
        #region ��������
        /// <summary>
        /// �������䣬���ݷ��ص�ֵ�õ����꣬�»���
        /// </summary>
        /// <param name="dteBirth">��������</param>
        /// <param name="intAge">����õ�������</param>
        /// <returns></returns>
        public static Age CalcAge(DateTime dteBirth, out int intAge)
        {
            Age age = Age.Year;
            intAge = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            DateTime dteNow = DateTime.Now;
            int intYear = dteBirth.Year;
            int intMonth = dteBirth.Month;
            int intDay = dteBirth.Day;

            if ((dteNow.Year - intYear) > 0)
            {
                intAge = dteNow.Year - intYear;
                age = Age.Year;
            }
            else if ((dteNow.Month - intMonth) > 0)
            {
                intAge = dteNow.Month - intMonth;
                age = Age.Month;
            }
            else
            {
                intAge = dteNow.Day - intDay;
                age = Age.Day;
            }
            return age;

        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="dteBirth">��������</param>
        /// <returns></returns>
        public static string CalcAge(DateTime dteBirth)
        {
            return s_strCalAge(dteBirth);
        }
        /// <summary>
        /// ����
        /// </summary>
        public enum Age
        {
            /// <summary>
            /// ��
            /// </summary>
            Year,
            /// <summary>
            /// ��
            /// </summary>
            Month,
            /// <summary>
            /// ��
            /// </summary>
            Day
        }
        #endregion

        #region ��ȷ��������㷽��
        /// <summary>
        /// ���ݳ������ڼ����ֹ����ǰ���ڵ�����
        /// </summary>
        /// <param name="p_strBirthDate"></param>
        /// <param name="p_intAge"></param>
        /// <param name="p_intMonth"></param>
        /// <param name="p_intDay"></param>
        /// <param name="p_intHour"></param>
        /// <param name="p_intMinute"></param>
        /// <returns></returns>
        public static string s_strCalAge(DateTime p_strBirthDate, out int p_intAge, out int p_intMonth, out int p_intDay, out int p_intHour, out int p_intMinute)
        {
            p_intAge = 0;
            p_intMonth = 0;
            p_intDay = 0;
            p_intHour = 0;
            p_intMinute = 0;

            string strAge = "δ֪";

            if (p_strBirthDate < DateTime.Now)
            {
                DateTime dtmTime = DateTime.Now;
                DateTime m_dtmBirth = p_strBirthDate;

                #region
                //������
                //1�����Ե�ǰʱ�䣭������Ϊ���䣻
                //2�������ڵ���15������䣬ֻ�㵽�꣬ȡ����ʣ�಻��һ����·ݶ���������21��XX�£�����ʾ����Ϊ21�꼴�ɣ�
                //3����1�����ڵİ�xx��xx���㣬ȡ��������ʵ����ʾ����Ϊ10��20�죩
                //4����1�����ڵİ�xx��xxСʱ�㣻����20��23Сʱ��
                //5)��1�����ڵİ�xxСʱxx�����㣻����23Сʱ59���ӣ�
                //6)��1Сʱ�ڵ��㵽���ӣ���59���ӣ�

                int intYear = -1;
                int intMonth = -1;
                int intDay = -1;
                int intHour = -1;
                int intMinute = -1;

                System.TimeSpan diffTS = dtmTime.Subtract(m_dtmBirth);
                if (diffTS.TotalMinutes < 60)
                {
                    intMinute = (int)diffTS.TotalMinutes;
                    if (intMinute > 0)
                    {
                        strAge = intMinute.ToString() + "����";
                        p_intMinute = intMinute;
                    }
                    else
                    {
                        strAge = "0����";
                        p_intMinute = 0;
                    }
                }
                else if (diffTS.TotalHours < 24)
                {
                    intHour = (int)diffTS.TotalHours;
                    intMinute = (int)((diffTS.TotalHours - intHour) * 60);
                    strAge = intHour.ToString() + "Сʱ";
                    p_intHour = intHour;
                    if (intMinute > 0)
                    {
                        strAge += intMinute.ToString() + "����";
                        p_intMinute = intMinute;
                    }
                }
                else
                {
                    intYear = dtmTime.Year;
                    intMonth = dtmTime.Month;
                    intDay = dtmTime.Day;

                    if (intDay >= m_dtmBirth.Day)
                        intDay -= m_dtmBirth.Day;
                    else
                    {
                        if (dtmTime.Month == 1)
                        {
                            intDay += DateTime.DaysInMonth(dtmTime.Year - 1, 12) - m_dtmBirth.Day;
                            intMonth = 12;
                            intYear--;
                        }
                        else
                        {
                            intDay += DateTime.DaysInMonth(dtmTime.Year, dtmTime.Month - 1) - m_dtmBirth.Day;
                            intMonth--;
                        }
                    }
                    if (intMonth >= m_dtmBirth.Month)
                        intMonth -= m_dtmBirth.Month;
                    else
                    {
                        intMonth += 12 - m_dtmBirth.Month;
                        intYear--;
                    }
                    if (intYear >= m_dtmBirth.Year)
                        intYear -= m_dtmBirth.Year;

                    if (intYear >= 0 && intYear < 1)
                    {
                        if (intMonth == 0)
                        {
                            if (dtmTime.Hour - m_dtmBirth.Hour > 0)
                            {
                                strAge = intDay.ToString() + "��" + (dtmTime.Hour - m_dtmBirth.Hour).ToString() + "Сʱ";
                                p_intHour = dtmTime.Hour - m_dtmBirth.Hour;
                            }
                            else
                            {
                                intDay--;
                                strAge = (intDay <= 0 ? "" : intDay.ToString() + "��") + (dtmTime.Hour + 24 - m_dtmBirth.Hour).ToString() + "Сʱ";
                                p_intHour = dtmTime.Hour + 24 - m_dtmBirth.Hour;
                            }
                        }
                        else
                        {
                            strAge = intMonth.ToString() + "��" + (intDay == 0 ? "" : intDay.ToString() + "��");
                        }
                    }
                    else if (intYear >= 1 && intYear < 15)
                    {
                        strAge = intYear.ToString() + "��" + intMonth.ToString() + "��";
                    }
                    else if (intYear >= 15)
                    {
                        strAge = intYear.ToString() + "��";
                    }
                    p_intAge = intYear;
                    p_intMonth = intMonth;
                    p_intDay = intDay;
                }
                #endregion
            }
            else
            {
                strAge = "�������ڴ��ڵ�ǰ����";
            }
            return strAge;
        }

        public static string s_strCalAge(DateTime p_strBirthDate)
        {
            int age, mouth, day, hour, minute;
            s_strCalAge(p_strBirthDate, out age, out mouth, out day, out hour, out minute);
            if (age != 0)
            {
                return age.ToString();
            }
            else if (mouth != 0)
            {
                return "0";
            }
            else
            {
                return "0";
            }
        }
        #endregion

    }
}
