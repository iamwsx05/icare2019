using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.ValueObject;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.gui.DataExchangeSystem
{
    public class clsCtl_DataExchangeMain
    {
        #region 变量
        /// <summary>
        /// 窗体
        /// </summary>
        public frmDataExchangeMain m_objViewer;
        /// <summary>
        /// 领域层对象

        /// </summary>
        private clsDcl_DataExchangeMain objDomain;

        /// <summary>
        /// 填报日期
        /// </summary>
        private DateTime dtmTBRQ;

        #endregion

        public clsCtl_DataExchangeMain()
        {
            objDomain = new clsDcl_DataExchangeMain();
        }

        /// <summary>
        /// 自动上传数据
        /// </summary>
        public void m_mthUploadExchangeData()
        {
            this.m_objViewer.rtb_showLog.Focus();
            if (isToUpload("采购入库"))
            {
                m_mthInStorage();
            }
            if (isToUpload("出库"))
            {
                m_mthOutStorage();
            }
            if (isToUpload("住院收入"))
            {
                m_mthInHospital();
            }
            if (isToUpload("门诊收入"))
            {
                m_mthOutpatient();
            }

        }

        /// <summary>
        /// 是否上传表数据
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public bool isToUpload(string strTableName)
        {
            bool isToUp = false;
            for (int i = 0; i < this.m_objViewer.dataGridViewUpdata.RowCount; i++)
            {
                if (this.m_objViewer.dataGridViewUpdata.Rows[i].Cells[0].Value.ToString().Trim() == strTableName)
                {
                    if ((bool)this.m_objViewer.dataGridViewUpdata.Rows[i].Cells[1].EditedFormattedValue == false)
                    {
                        isToUp = false;
                    }
                    else
                    {
                        isToUp = true;
                    }
                }
            }
            return isToUp;
        }

        #region 药库入库数据
        /// <summary>
        /// 药库入库数据上传
        /// </summary>
        public void m_mthInStorage()
        {
            this.m_objViewer.tsp_showProgress.Maximum = 100;
            this.m_objViewer.tsp_showProgress.Minimum = 0;

            this.m_objViewer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tslStat.Text = "准备下载药库入库数据上传,请稍后...";
            this.m_objViewer.Update();

            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tsp_showProgress.Value = 10;
            this.m_objViewer.tslStat.Text = "正在下载药库入库数据,请稍后...";
            this.m_objViewer.tsp_showProgress.Value = 20;
            this.m_objViewer.Update();

            List<clsInStorageData_VO> lisInStorageData = new List<clsInStorageData_VO>();
            clsInStorageData_VO[] ArrInStorageData = null;
            try
            {
                long lngRes = objDomain.m_lngGetInStorageData(Convert.ToDateTime(this.m_objViewer.dtmBegin.Value.ToString("yyyy-MM-dd 00:00:00")), Convert.ToDateTime(this.m_objViewer.dtmEnd.Value.ToString("yyyy-MM-dd 23:59:59")), out ArrInStorageData);
                if (ArrInStorageData != null && ArrInStorageData.Length > 0)
                {
                    lisInStorageData.AddRange(ArrInStorageData);
                }
                if (lngRes < 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "下载药库入库数据失败,请核查!";
                    this.m_objViewer.Update();
                }
                if (lisInStorageData.Count > 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 40;
                    this.m_objViewer.tslStat.Text = "准备上传药库入库数据,请稍后...";
                    this.m_objViewer.Update();

                    for (int i = 0; i < lisInStorageData.Count; i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 正在上传药库入库数据共" + lisInStorageData.Count.ToString() + "条,正在上传第" + (i + 1).ToString() + "条");
                        this.m_objViewer.tslStat.Text = "正在上传第" + (i + 1).ToString() + "条数据";
                        this.m_objViewer.Update();
                        lngRes = objDomain.m_lngUploadInStorageData(lisInStorageData[i]);
                        if (lngRes < 0)
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 上传药库入库数据第" + (i + 1).ToString() + "条数据失败,请核查");
                        }
                        else
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 上传药库入库数据第" + (i + 1).ToString() + "条数据成功");
                        }

                        this.m_objViewer.tsp_showProgress.Value = i % 100;

                        this.m_objViewer.Update();
                    }
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tslStat.Text = "上传完成,请查看日志确认是否全部成功";
                    this.m_objViewer.rtb_showLog.AppendText("\n 药库入库数据上传完成,请查看日志确认是否全部成功");
                    com.digitalwave.Utility.clsLogText objLogErr = new clsLogText();
                    objLogErr.LogError("[" + DateTime.Now.ToString("yyyy-MM-dd hh24:mi:ss") + "]: " + "药库入库数据上传成功");
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.Update();

                }
                else
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "此时间段内没有入库数据,请核查!";
                    this.m_objViewer.Update();
                }
            }
            catch
            {

            }
            finally
            {
                this.m_objViewer.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

        #region 药库出库数据
        /// <summary>
        /// 药库出库数据
        /// </summary>
        public void m_mthOutStorage()
        {
            this.m_objViewer.tsp_showProgress.Maximum = 100;
            this.m_objViewer.tsp_showProgress.Minimum = 0;

            this.m_objViewer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tslStat.Text = "准备下载药库出库数据上传,请稍后...";
            this.m_objViewer.Update();

            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tsp_showProgress.Value = 10;
            this.m_objViewer.tslStat.Text = "正在下载药库出库数据,请稍后...";
            this.m_objViewer.tsp_showProgress.Value = 20;
            this.m_objViewer.Update();

            List<clsOutStorageData_VO> lisOutStorageData = new List<clsOutStorageData_VO>();
            clsOutStorageData_VO[] ArrOutStorageData = null;
            try
            {
                long lngRes = objDomain.m_lngGetOutStorageData(Convert.ToDateTime(this.m_objViewer.dtmBegin.Value.ToString("yyyy-MM-dd 00:00:00")), Convert.ToDateTime(this.m_objViewer.dtmEnd.Value.ToString("yyyy-MM-dd 23:59:59")), out ArrOutStorageData);
                if (ArrOutStorageData != null && ArrOutStorageData.Length > 0)
                {
                    lisOutStorageData.AddRange(ArrOutStorageData);
                }
                if (lngRes < 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "下载药库出库数据失败,请核查!";
                    this.m_objViewer.Update();
                }
                if (lisOutStorageData.Count > 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 40;
                    this.m_objViewer.tslStat.Text = "准备上传药库出库数据,请稍后...";
                    this.m_objViewer.Update();

                    for (int i = 0; i < lisOutStorageData.Count; i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 正在上传药库出库数据共" + lisOutStorageData.Count.ToString() + "条,正在上传第" + (i + 1).ToString() + "条");
                        this.m_objViewer.tslStat.Text = "正在上传第" + (i + 1).ToString() + "条数据";
                        this.m_objViewer.Update();
                        lngRes = objDomain.m_lngUploadOutStorageData(lisOutStorageData[i]);
                        if (lngRes < 0)
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 上传药库出库数据第" + (i + 1).ToString() + "条数据失败,请核查");
                        }
                        else
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 上传药库出库数据第" + (i + 1).ToString() + "条数据成功");
                        }

                        this.m_objViewer.tsp_showProgress.Value = i % 100;

                        this.m_objViewer.Update();
                    }
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tslStat.Text = "上传完成,请查看日志确认是否全部成功";
                    this.m_objViewer.rtb_showLog.AppendText("\n 药库出库数据上传完成,请查看日志确认是否全部成功");
                    com.digitalwave.Utility.clsLogText objLogErr = new clsLogText();
                    objLogErr.LogError("[" + DateTime.Now.ToString("yyyy-MM-dd hh24:mi:ss") + "]: " + "药库出库数据上传成功");
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.Update();

                }
                else
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "此时间段内没有出库数据,请核查!";
                    this.m_objViewer.Update();
                }
            }
            catch
            {

            }
            finally
            {
                this.m_objViewer.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

        #region 住院收入数据
        /// <summary>
        /// 住院收入数据
        /// </summary>
        public void m_mthInHospital()
        {
            this.m_objViewer.tsp_showProgress.Maximum = 100;
            this.m_objViewer.tsp_showProgress.Minimum = 0;

            this.m_objViewer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tslStat.Text = "准备下载住院收入数据上传,请稍后...";
            this.m_objViewer.Update();

            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tsp_showProgress.Value = 10;
            this.m_objViewer.tslStat.Text = "正在下载住院收入数据,请稍后...";
            this.m_objViewer.tsp_showProgress.Value = 20;
            this.m_objViewer.Update();

            List<clsInHospital_VO> lisInHospital = new List<clsInHospital_VO>();
            clsInHospital_VO[] ArrInHospital = null;
            try
            {
                long lngRes = objDomain.m_lngGetInHospital(Convert.ToDateTime(this.m_objViewer.dtmBegin.Value.ToString("yyyy-MM-dd 00:00:00")), Convert.ToDateTime(this.m_objViewer.dtmEnd.Value.ToString("yyyy-MM-dd 23:59:59")), out ArrInHospital);
                if (ArrInHospital != null && ArrInHospital.Length > 0)
                {
                    lisInHospital.AddRange(ArrInHospital);
                }
                if (lngRes < 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "下载住院收入数据失败,请核查!";
                    this.m_objViewer.Update();
                }
                if (lisInHospital.Count > 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 40;
                    this.m_objViewer.tslStat.Text = "准备上传住院收入数据,请稍后...";
                    this.m_objViewer.Update();

                    for (int i = 0; i < lisInHospital.Count; i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 正在上传住院收入数据共" + lisInHospital.Count.ToString() + "条,正在上传第" + (i + 1).ToString() + "条");
                        this.m_objViewer.tslStat.Text = "正在上传第" + (i + 1).ToString() + "条数据";
                        this.m_objViewer.Update();
                        lngRes = objDomain.m_lngUploadInHospital(lisInHospital[i]);
                        if (lngRes < 0)
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 上传住院收入数据第" + (i + 1).ToString() + "条数据失败,请核查");
                        }
                        else
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 上传住院收入数据第" + (i + 1).ToString() + "条数据成功");
                        }

                        this.m_objViewer.tsp_showProgress.Value = i % 100;

                        this.m_objViewer.Update();
                    }
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tslStat.Text = "上传完成,请查看日志确认是否全部成功";
                    this.m_objViewer.rtb_showLog.AppendText("\n 住院收入数据上传完成,请查看日志确认是否全部成功");
                    com.digitalwave.Utility.clsLogText objLogErr = new clsLogText();
                    objLogErr.LogError("["+DateTime.Now.ToString("yyyy-MM-dd hh24:mi:ss") + "]: " + "住院收入数据上传成功");
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.Update();

                }
                else
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "此时间段内没有住院收入数据,请核查!";
                    this.m_objViewer.Update();
                }
            }
            catch
            {

            }
            finally
            {
                this.m_objViewer.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

        #region 门诊收入数据
        /// <summary>
        /// 门诊收入数据
        /// </summary>
        public void m_mthOutpatient()
        {
            this.m_objViewer.tsp_showProgress.Maximum = 100;
            this.m_objViewer.tsp_showProgress.Minimum = 0;

            this.m_objViewer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tslStat.Text = "准备下载门诊收入数据上传,请稍后...";
            this.m_objViewer.Update();

            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tsp_showProgress.Value = 10;
            this.m_objViewer.tslStat.Text = "正在下载门诊收入数据,请稍后...";
            this.m_objViewer.tsp_showProgress.Value = 20;
            this.m_objViewer.Update();

            List<clsOutpatient_VO> lisOutpatient = new List<clsOutpatient_VO>();
            clsOutpatient_VO[] ArrOutpatient = null;
            try
            {
                long lngRes = objDomain.m_lngGetOutpatient(Convert.ToDateTime(this.m_objViewer.dtmBegin.Value.ToString("yyyy-MM-dd 00:00:00")), Convert.ToDateTime(this.m_objViewer.dtmEnd.Value.ToString("yyyy-MM-dd 23:59:59")), out ArrOutpatient);
                if (ArrOutpatient != null && ArrOutpatient.Length > 0)
                {
                    lisOutpatient.AddRange(ArrOutpatient);
                }
                if (lngRes < 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "下载门诊收入数据失败,请核查!";
                    this.m_objViewer.Update();
                }
                if (lisOutpatient.Count > 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 40;
                    this.m_objViewer.tslStat.Text = "准备上传门诊收入数据,请稍后...";
                    this.m_objViewer.Update();

                    for (int i = 0; i < lisOutpatient.Count; i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 正在上传门诊收入数据共" + lisOutpatient.Count.ToString() + "条,正在上传第" + (i + 1).ToString() + "条");
                        this.m_objViewer.tslStat.Text = "正在上传第" + (i + 1).ToString() + "条数据";
                        this.m_objViewer.Update();
                        lngRes = objDomain.m_lngUploadOutpatient(lisOutpatient[i]);
                        if (lngRes < 0)
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 上传门诊收入数据第" + (i + 1).ToString() + "条数据失败,请核查");
                            //this.m_objViewer.rtb_showLog.AppendText("\n [" +lisOutpatient[i].BMBH+lisOutpatient[i]. "");

                        }
                        else
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] 上传门诊收入数据第" + (i + 1).ToString() + "条数据成功");
                        }

                        this.m_objViewer.tsp_showProgress.Value = i % 100;

                        this.m_objViewer.Update();
                    }
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tslStat.Text = "上传完成,请查看日志确认是否全部成功";
                    this.m_objViewer.rtb_showLog.AppendText("\n 门诊收入数据上传完成,请查看日志确认是否全部成功");
                    com.digitalwave.Utility.clsLogText objLogErr = new clsLogText();
                    objLogErr.LogError("[" + DateTime.Now.ToString("yyyy-MM-dd hh24:mi:ss") + "]: " + "门诊收入数据上传成功");
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.Update();

                }
                else
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "此时间段内没有门诊收入数据,请核查!";
                    this.m_objViewer.Update();
                }
            }
            catch
            {

            }
            finally
            {
                this.m_objViewer.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

    }
}
