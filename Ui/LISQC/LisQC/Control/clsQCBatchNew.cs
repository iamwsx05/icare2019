using System;
using System.Collections.Generic;
using weCare.Core.Entity;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsQCBatchNew
    {  
        internal DateTime m_datBegin;
        internal DateTime m_datEnd;
        internal List<clsLisQCBatchVO> m_objBatchSets;
        internal List<clsLisQCConcentrationVO> m_objConcentrations;
        internal List<clsLisQCDataVO> m_objDatas;
        //private clsDcl_QCDataBusiness m_objDomain;
        internal List<clsLisQCReportVO> m_objReports;
        internal string m_strBrokenRules;
        public EventHandler Reloaded;
        public EventHandler Reseted;
        public EventHandler SetChanged;

        // Events
        public event EventHandler BrokenRulesChanged;
        public event EventHandler ConcentrationChanged;
        public event EventHandler DataChanged;
        public event EventHandler Loaded;
        public event DataLoadFailedEventHandler LoadFailed; 

        // Methods
        public clsQCBatchNew()
        {
            this.m_objConcentrations = new List<clsLisQCConcentrationVO>();
            this.m_objDatas = new List<clsLisQCDataVO>();
            this.m_objReports = new List<clsLisQCReportVO>();
            this.m_strBrokenRules = string.Empty;
            this.m_datBegin = DateTime.MinValue;
            this.m_datEnd = DateTime.MaxValue;
            //this.m_objDomain = new clsDcl_QCDataBusiness(); 
        }

        public static int CompareQCBatchVO(clsLisQCBatchVO x, clsLisQCBatchVO y)
        {
            int result;
            if (x == null)
            {
                if (y == null)
                {
                    result = 0;
                }
                else
                {
                    result = -1;
                }
            }
            else
            {
                if (y == null)
                {
                    result = 1;
                }
                else
                {
                    int num = string.Compare(x.m_strDeviceId, y.m_strDeviceId);
                    if (num != 0)
                    {
                        result = num;
                    }
                    else
                    {
                        num = string.Compare(x.m_strSampleLotNo, y.m_strSampleLotNo);
                        if (num != 0)
                        {
                            result = num;
                        }
                        else
                        {
                            int num2 = 0;
                            int num3 = 0;
                            int.TryParse(x.m_strSortNum, out num2);
                            int.TryParse(y.m_strSortNum, out num3);
                            if (num2 > num3)
                            {
                                result = 1;
                            }
                            else
                            {
                                if (num2 < num3)
                                {
                                    result = -1;
                                }
                                else
                                {
                                    result = x.m_intSeq - y.m_intSeq;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public List<clsLisQCConcentrationVO> GetConcentrations()
        {
            List<clsLisQCConcentrationVO> concentratrionList = new List<clsLisQCConcentrationVO>();
            foreach (clsLisQCConcentrationVO var in m_objConcentrations)
            {
                clsLisQCConcentrationVO temp = new clsLisQCConcentrationVO();
                var.m_mthCopyTo(temp);
                concentratrionList.Add(temp);
            }
            return concentratrionList;
        }

        public List<clsLisQCConcentrationVO> GetConcentrations(int p_intBatchSeq)
        {
            List<clsLisQCConcentrationVO> concentratrionList = new List<clsLisQCConcentrationVO>();
            foreach (clsLisQCConcentrationVO var in m_objConcentrations)
            {
                if (var.m_intQCBatchSeq == p_intBatchSeq)
                {
                    clsLisQCConcentrationVO temp = new clsLisQCConcentrationVO();
                    var.m_mthCopyTo(temp);
                    concentratrionList.Add(temp);
                }
            }
            return concentratrionList;
        }

        public List<clsLisQCDataVO> GetDatas()
        {
            if (this.IsNull)
                return null;

            List<clsLisQCDataVO> dataList = new List<clsLisQCDataVO>();
            clsLisQCDataVO objTemp = null;
            foreach (clsLisQCDataVO data in m_objDatas)
            {
                objTemp = new clsLisQCDataVO();
                data.m_mthCopyTo(objTemp);
                dataList.Add(objTemp);
            }
            return dataList;
        }

        public List<clsLisQCDataVO> GetDatas(int p_intBatchSeq)
        {
            if (this.IsNull)
                return null;

            List<clsLisQCDataVO> dataList = new List<clsLisQCDataVO>();
            clsLisQCDataVO objTemp = null;
            foreach (clsLisQCDataVO data in m_objDatas)
            {
                if (data.m_intQCBatchSeq == p_intBatchSeq)
                {
                    objTemp = new clsLisQCDataVO();
                    data.m_mthCopyTo(objTemp);
                    dataList.Add(objTemp);
                }
            }
            return dataList;
        }

        public List<clsLisQCBatchVO> GetQCBatchSet()
        {
            List<clsLisQCBatchVO> result;
            if (this.IsNull)
            {
                result = null;
            }
            else
            {
                List<clsLisQCBatchVO> list = new List<clsLisQCBatchVO>();
                foreach (clsLisQCBatchVO current in this.m_objBatchSets)
                {
                    clsLisQCBatchVO clsLisQCBatchVO = new clsLisQCBatchVO();
                    current.m_mthCopyTo(clsLisQCBatchVO);
                    list.Add(clsLisQCBatchVO);
                }
                result = list;
            }
            return result;
        }

        public clsLisQCBatchVO GetQCBatchSet(int p_intBatchSeq)
        {
            if (this.IsNull)
                return null;

            clsLisQCBatchVO temp = null;
            foreach (clsLisQCBatchVO objBatch in m_objBatchSets)
            {
                if (objBatch.m_intSeq == p_intBatchSeq)
                {
                    temp = new clsLisQCBatchVO();
                    objBatch.m_mthCopyTo(temp);
                    return temp;
                }
            }
            return null;
        }

        public List<clsLisQCReportVO> GetReports()
        {
            return m_objReports;
        }

        public List<clsLisQCReportVO> GetReports(int p_intBatchSeq)
        {
            if (this.IsNull)
                return null;

            List<clsLisQCReportVO> reportList = new List<clsLisQCReportVO>();
            clsLisQCReportVO objTemp = null;
            foreach (clsLisQCReportVO report in m_objReports)
            {
                if (report.m_intQCBatchSeq == p_intBatchSeq)
                {
                    objTemp = new clsLisQCReportVO();
                    report.m_mthCopyTo(objTemp);
                    reportList.Add(objTemp);
                }
            }
            return reportList;
        }

        public void lngInsertBatchSet(DataTable dtbResult)
        {
            if( (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertBatchSet(dtbResult) > 0)
            {
                MessageBox.Show("保存成功!", "质控管理", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("保存失败！", "质控管理", MessageBoxButtons.OK);
            }
        }

        public void Load(int p_intQCBatchSeq, DateTime p_dtpDateStart, DateTime p_DateEnd)
        {
            clsLisQCBatchVO qcBactchVo = null;
            clsLisQCConcentrationVO[] qcConcentrationVo = null;
            clsLisQCDataVO[] qCDataVo = null;
            clsLisQCReportVO[] qCReportVo = null;
            long num = 0L;
            num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCBatch(p_intQCBatchSeq, true, out qcBactchVo);
            if (num > 0L && qcBactchVo != null)
            {
                num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCConcentration(p_intQCBatchSeq, out qcConcentrationVo);
            }
            if (num > 0L && qcBactchVo != null)
            {
                num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCData(out qCDataVo, p_intQCBatchSeq, p_dtpDateStart, p_DateEnd);
            }
            if (num > 0L && qcBactchVo != null)
            {
                num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCReport(p_intQCBatchSeq, p_dtpDateStart, p_DateEnd, enmQCStatus.Natrural, out qCReportVo);
            }
            if (num > 0L && qcBactchVo != null)
            {
                if (this.m_objBatchSets == null)
                {
                    this.m_objBatchSets = new List<clsLisQCBatchVO>();
                }
                else
                {
                    this.m_objBatchSets.Clear();
                }
                this.m_objConcentrations.Clear();
                this.m_objDatas.Clear();
                this.m_objReports.Clear();
                this.m_strBrokenRules = string.Empty;
                this.m_objBatchSets.Add(qcBactchVo);
                this.m_objBatchSets.Sort(new Comparison<clsLisQCBatchVO>(clsQCBatchNew.CompareQCBatchVO));
                if (qcConcentrationVo != null)
                {
                    this.m_objConcentrations.AddRange(qcConcentrationVo);
                }
                if (qCDataVo != null)
                {
                    this.m_objDatas.AddRange(qCDataVo);
                }
                if (qCReportVo != null)
                {
                    this.m_objReports.AddRange(qCReportVo);
                }
                this.m_datBegin = p_dtpDateStart;
                this.m_datEnd = p_DateEnd;
                if (this.Loaded != null)
                {
                    this.Loaded(this, null);
                }
            }
            else
            {
                if (num <= 0L)
                {
                    if (this.LoadFailed != null)
                    {
                        this.LoadFailed(this, new clsQCBatchNew.DataLoadFailedEventArgs("读取数据失败."));
                    }
                }
                else
                {
                    if (qcBactchVo == null)
                    {
                        if (this.LoadFailed != null)
                        {
                            this.LoadFailed(this, new clsQCBatchNew.DataLoadFailedEventArgs("指定的质控批不存在."));
                        }
                    }
                }
            }
        }

        public void Load(int[] p_intQCBatchSeqArr, DateTime p_dtpDateStart, DateTime p_dtpDateEnd)
        {
            clsLisQCBatchVO[] qCbatchVo = null;
            clsLisQCConcentrationVO[] qCconcentrationVo = null;
            clsLisQCDataVO[] qCdataVo = null;
            clsLisQCReportVO[] qCreportVo = null;
            long num = 0L;
            num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCBatch(p_intQCBatchSeqArr, true, out qCbatchVo);
            if (num > 0L && qCbatchVo != null)
            {
                num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCConcentration(p_intQCBatchSeqArr, out qCconcentrationVo);
            }
            if (num > 0L && qCbatchVo != null)
            {
                num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCData(out qCdataVo, p_intQCBatchSeqArr, p_dtpDateStart, p_dtpDateEnd);
            }
            if (num > 0L && qCbatchVo != null)
            {
                num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCReport(p_intQCBatchSeqArr, p_dtpDateStart, p_dtpDateEnd, enmQCStatus.Natrural, out qCreportVo);
            }
            if (num > 0L && qCbatchVo != null)
            {
                if (this.m_objBatchSets == null)
                {
                    this.m_objBatchSets = new List<clsLisQCBatchVO>();
                }
                else
                {
                    this.m_objBatchSets.Clear();
                }
                this.m_objConcentrations.Clear();
                this.m_objDatas.Clear();
                this.m_objReports.Clear();
                this.m_strBrokenRules = string.Empty;
                this.m_objBatchSets.AddRange(qCbatchVo);
                this.m_objBatchSets.Sort(new Comparison<clsLisQCBatchVO>(clsQCBatchNew.CompareQCBatchVO));
                if (qCconcentrationVo != null)
                {
                    this.m_objConcentrations.AddRange(qCconcentrationVo);
                }
                if (qCdataVo != null)
                {
                    this.m_objDatas.AddRange(qCdataVo);
                }
                if (qCreportVo != null)
                {
                    this.m_objReports.AddRange(qCreportVo);
                }
                this.m_datBegin = p_dtpDateStart;
                this.m_datEnd = p_dtpDateEnd;
                if (this.Loaded != null)
                {
                    this.Loaded(this, null);
                }
            }
            else
            {
                if (num <= 0L)
                {
                    if (this.LoadFailed != null)
                    {
                        this.LoadFailed(this, new clsQCBatchNew.DataLoadFailedEventArgs("读取数据失败."));
                    }
                }
                else
                {
                    if (qCbatchVo == null)
                    {
                        if (this.LoadFailed != null)
                        {
                            this.LoadFailed(this, new clsQCBatchNew.DataLoadFailedEventArgs("指定的质控批不存在."));
                        }
                    }
                }
            }
        }

        public long m_lngQueryDeviceSampleID(int p_intBatchSeq, out string p_strSampleId)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngQueryDeviceSampleID(p_intBatchSeq, out p_strSampleId);
        }

        public long m_mthDeleteItems(int intSeq)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngDeleteQCBatch(intSeq); 
        }

        public List<double> m_mthGetDatas(int p_intBatchSeq)
        {
            List<double> result;
            if (this.IsNull)
            {
                result = null;
            }
            else
            {
                List<double> list = new List<double>();
                foreach (clsLisQCDataVO current in this.m_objDatas)
                {
                    if (current.m_intQCBatchSeq == p_intBatchSeq)
                    {
                        list.Add(current.m_dlbResult);
                    }
                }
                result = list;
            }
            return result;
        }

        public void Reload(DateTime p_dtpDateStart, DateTime p_dtpDateEnd)
        {
            if (this.IsNull)
                return;

            clsLisQCDataVO[] objDatas = null;
            clsLisQCReportVO[] objReports = null;

            long lngRes = 0;

            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCData(out objDatas, this.SeqArr, p_dtpDateStart, p_dtpDateEnd);

            if (lngRes > 0)
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCReport(this.SeqArr, p_dtpDateStart, p_dtpDateEnd, enmQCStatus.Natrural, out objReports);
            }
            if (lngRes > 0)
            {
                this.m_objDatas.Clear();
                this.m_objReports.Clear();
                this.m_strBrokenRules = string.Empty;

                if (objDatas != null)
                {
                    m_objDatas.AddRange(objDatas);
                }
                if (objReports != null)
                {
                    this.m_objReports.AddRange(objReports);
                }
                this.m_datBegin = p_dtpDateStart;
                this.m_datEnd = p_dtpDateEnd;

                if (this.Reloaded != null)
                {
                    Reloaded(this, null);
                }
            }
            else
            {
                if (this.LoadFailed != null)
                {
                    LoadFailed(this, new DataLoadFailedEventArgs("读取数据失败."));
                }
            }
        }

        public void Reset()
        {
            if (this.IsNull)
            {
                return;
            }
            this.m_objBatchSets.Clear();
            this.m_objConcentrations.Clear();
            this.m_objDatas.Clear();
            this.m_objReports.Clear();
            this.m_strBrokenRules = string.Empty;
            m_datBegin = DateTime.MinValue;
            m_datEnd = DateTime.MaxValue;

            if (this.Reseted != null)
            {
                Reseted(this, null);
            }
        }

        public void UpdateConcentrations(List<clsLisQCConcentrationVO> p_objConcentrations)
        {
            if (this.IsNull)
            {
                return;
            }
            if (p_objConcentrations != null)
            {
                this.m_objConcentrations.Clear();
                this.m_strBrokenRules = string.Empty;

                foreach (clsLisQCConcentrationVO var in p_objConcentrations)
                {
                    clsLisQCConcentrationVO temp = new clsLisQCConcentrationVO();
                    var.m_mthCopyTo(temp);
                    this.m_objConcentrations.Add(temp);
                }

                if (this.ConcentrationChanged != null)
                {
                    ConcentrationChanged(this, null);
                }
            }
        }

        public void UpdateConcentrations(List<clsLisQCConcentrationVO> p_objConcentrations, int p_intQCBatchSeq)
        {
            if (!this.IsNull)
            {
                if (p_objConcentrations != null)
                {
                    for (int i = 0; i < this.m_objConcentrations.Count; i++)
                    {
                        if (this.m_objConcentrations[i].m_intQCBatchSeq == p_intQCBatchSeq)
                        {
                            this.m_objConcentrations.RemoveAt(i);
                            i--;
                        }
                    }
                    this.m_strBrokenRules = string.Empty;
                    foreach (clsLisQCConcentrationVO current in p_objConcentrations)
                    {
                        clsLisQCConcentrationVO clsLisQCConcentrationVO = new clsLisQCConcentrationVO();
                        current.m_mthCopyTo(clsLisQCConcentrationVO);
                        this.m_objConcentrations.Add(clsLisQCConcentrationVO);
                    }
                    if (this.ConcentrationChanged != null)
                    {
                        this.ConcentrationChanged(this, null);
                    }
                }
            }
        }

        public void UpdateDatas(List<clsLisQCDataVO> p_objDatas)
        {
            if (this.IsNull)
            {
                return;
            }
            if (p_objDatas != null)
            {
                this.m_objDatas.Clear();
                this.m_strBrokenRules = string.Empty;

                foreach (clsLisQCDataVO var in p_objDatas)
                {
                    clsLisQCDataVO temp = new clsLisQCDataVO();
                    var.m_mthCopyTo(temp);
                    this.m_objDatas.Add(temp);
                }
                if (this.DataChanged != null)
                {
                    DataChanged(this, null);
                }
            }
        }

        public void UpdateSet(clsLisQCBatchVO[] p_objSetArr)
        {
            if (this.IsNull)
            {
                return;
            }
            if (p_objSetArr != null)
            {
                bool blnIsChange = false;

                foreach (clsLisQCBatchVO objSet in p_objSetArr)
                {
                    foreach (clsLisQCBatchVO objBatch in m_objBatchSets)
                    {
                        if (objSet.m_intSeq != objBatch.m_intSeq)
                            continue;

                        if (!objSet.m_mthEquals(objBatch))
                        {
                            objSet.m_mthCopyTo(objBatch);
                            blnIsChange = true;
                            break;
                        }
                    }
                }
                if (blnIsChange)
                {
                    if (this.SetChanged != null)
                        SetChanged(this, null);
                }
            }
        }

        // Properties
        public string BrokenRules
        {
            set
            {
                if (this.IsNull)
                {
                    return;
                }
                if (value != null)
                {
                    if (this.m_strBrokenRules != value)
                    {
                        this.m_strBrokenRules = value;
                        if (this.BrokenRulesChanged != null)
                        {
                            BrokenRulesChanged(this, null);
                        }
                    }
                }
            }
            get
            {
                return this.m_strBrokenRules;
            }
        }

        public int Count
        {
            get
            {
                if (m_objBatchSets != null)
                    return m_objBatchSets.Count;
                else
                    return -1;
            }
        }

        public DateTime DateBegin
        {
            get
            {
                return this.m_datBegin;
            }
        }

        public DateTime DateEnd
        {
            get
            {
                return this.m_datEnd;
            }
        }

        public bool IsNull
        {
            get
            {
                if (this.m_objBatchSets == null || this.m_objBatchSets.Count <= 0)
                    return true;
                return false;
            }
        }

        public clsLisQCBatchVO this[int index]
        {
            get
            {
                if (Count <= 0)
                    return null;
                else
                {
                    if (index >= 0 && index < Count)
                    {
                        return m_objBatchSets[index];
                    }
                }
                return null;
            }
        }

        public int[] SeqArr
        {
            get
            {
                if (this.IsNull)
                    return null;
                List<int> lstSeq = new List<int>();
                foreach (clsLisQCBatchVO objBatch in m_objBatchSets)
                {
                    lstSeq.Add(objBatch.m_intSeq);
                }

                lstSeq.Sort();

                return lstSeq.ToArray();
            }
        }

        // Nested Types
        public class DataLoadFailedEventArgs : EventArgs
        {
            private string m_strFailedMessage;
            public string FailedMessage
            {
                get { return this.m_strFailedMessage; }
            }
            public DataLoadFailedEventArgs(string p_strFailedMessage)
            {
                this.m_strFailedMessage = p_strFailedMessage;
            }
        }

        public delegate void DataLoadFailedEventHandler(object sender, clsQCBatchNew.DataLoadFailedEventArgs e);
    }
}
