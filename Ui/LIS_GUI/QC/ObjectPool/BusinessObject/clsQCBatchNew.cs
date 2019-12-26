using System;
using System.Collections.Generic;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// �ʿ����ݹ�����(��)--֧��ͬʱ��������ʿ�����
    /// �ʿ����ݽ����Ũ���޹أ�ֻ���ʿ�Ʒ�йء� 
    /// </summary>
    public class clsQCBatchNew : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// �ʿ������б�
        /// </summary>
        internal List<clsLisQCBatchVO> m_objBatchSets;
        /// <summary>
        /// Ũ���б�
        /// </summary>
        internal List<clsLisQCConcentrationVO> m_objConcentrations = new List<clsLisQCConcentrationVO>();
        /// <summary>
        /// �ʿؽ������
        /// </summary>
        internal List<clsLisQCDataVO> m_objDatas = new List<clsLisQCDataVO>();
        /// <summary>
        /// �ʿر����б�
        /// </summary>
        internal List<clsLisQCReportVO> m_objReports = new List<clsLisQCReportVO>();
        /// <summary>
        /// ʧ�ع���
        /// </summary>
        internal string m_strBrokenRules = string.Empty;
        /// <summary>
        /// �ʿ����ݿ�ʼʱ��
        /// </summary>
        internal DateTime m_datBegin = DateTime.MinValue;
        /// <summary>
        /// �ʿ����ݽ���ʱ��
        /// </summary>
        internal DateTime m_datEnd = DateTime.MaxValue;

        #endregion

        #region
        private clsTmdQCBatchSmp m_objManage = new clsTmdQCBatchSmp();
        #endregion

        #region �¼� (�¼�����Ƕ��)
        /// <summary>
        /// ��������ʧ���¼�
        /// </summary>
        public class DataLoadFailedEventArgs : System.EventArgs
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
        public delegate void DataLoadFailedEventHandler(object sender, DataLoadFailedEventArgs e);

        /// <summary>
        /// ģ������
        /// </summary>
        public event System.EventHandler Reseted;
        /// <summary>
        /// ģ�ͳ�ʼ������
        /// </summary>
        public event System.EventHandler Loaded;
        /// <summary>
        /// ���¼���ĳ��ʱ��ε����ݺͱ���
        /// </summary>
        public event System.EventHandler Reloaded;
        /// <summary>
        /// �����ݿ��ȡʱʧ��
        /// </summary>
        public event DataLoadFailedEventHandler LoadFailed;
        /// <summary>
        /// �ʿ����������øı�
        /// </summary>
        public event System.EventHandler SetChanged;
        /// <summary>
        /// Ũ�ȸı䣬�����¼���������
        /// </summary>
        public event System.EventHandler ConcentrationChanged;
        /// <summary>
        /// ���ݸı�
        /// </summary>
        public event System.EventHandler DataChanged;
        /// <summary>
        /// ����������
        /// </summary>
        public event System.EventHandler BrokenRulesChanged;
        //public event System.EventHandler ReportsChanged;
        #endregion

        #region ��ȡ���ݽӿ�
        /// <summary>
        /// ��ȡ�ʿ�ģ������
        /// </summary>
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
        /// <summary>
        /// �Ƿ��,true=�ʿ�ģ��Ϊ��,����=false
        /// </summary>
        public bool IsNull
        {
            get
            {
                if (this.m_objBatchSets == null || this.m_objBatchSets.Count <= 0)
                    return true;
                return false;
            }
        }
        /// <summary>
        /// ��ȡָ���������ʿ�����
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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

        /// <summary>
        /// �ʿ����������
        /// </summary>
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
        /// <summary>
        /// �ʿ���������
        /// </summary>
        /// <returns></returns>
        public List<clsLisQCBatchVO> GetQCBatchSet()
        {
            //if (this.IsNull)
            //    return null;
            //List<clsLisQCBatchVO> lstQCBatch = new List<clsLisQCBatchVO>();
            //clsLisQCBatchVO temp = null;
            //foreach (clsLisQCBatchVO objBatch in m_objBatchSets)
            //{
            //    temp = new clsLisQCBatchVO();
            //    objBatch.m_mthCopyTo(temp);
            //    lstQCBatch.Add(temp);
            //}
            //return lstQCBatch;
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
        /// <summary>
        /// �ʿ�����
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// �ʿ���Ũ�ȱ�
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// �ʿ���Ũ�ȱ�
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// �����ʿ������Ľ������
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// �ʿ������Ľ������
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// �ʿر���
        /// </summary>
        /// <returns></returns>
        public List<clsLisQCReportVO> GetReports()
        {
            return m_objReports;
        }
        /// <summary>
        /// �ʿر���
        /// </summary>
        /// <returns></returns>
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

        ///// <summary>
        ///// �ʿع���
        ///// </summary>
        ///// <param name="p_intBatchSeq"></param>
        ///// <returns></returns>
        //public string GetBrokenRules(int p_intBatchSeq)
        //{
        //    if (this.IsNull)
        //        return null;

        //    foreach (clsLisQCDataModule objModule in m_lstDataModule)
        //    {
        //        if (objModule.m_objBatchSet.m_intSeq == p_intBatchSeq)
        //        {
        //            return objModule.m_strBrokenRules;
        //        }
        //    }
        //    return null;
        //}



        /// <summary>
        /// �ʿع���
        /// </summary>
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
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime DateBegin
        {
            get
            {
                return this.m_datBegin;
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime DateEnd
        {
            get
            {
                return this.m_datEnd;
            }
        }
        #endregion

        #region �����ύ�ӿ� (�ڿ�ģ���Ͻ���ִ���κβ���,����յĲ���Ҳ����ִ���κβ���)
        /// <summary>
        /// ����ģ�� (�ڿ�ģ���Ͻ���ִ���κβ���,����յĲ���Ҳ����ִ���κβ���)
        /// </summary>
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
        /// <summary>
        /// �����ʿ�����������
        /// </summary>
        /// <param name="p_objSet"></param>
        public void UpdateSet(clsLisQCBatchVO[] p_objSetArr)
        {
            if (this.IsNull)
            {
                return;
            }
            if (p_objSetArr != null)
            {
                bool blnIsChange = false;

                foreach(clsLisQCBatchVO objSet in p_objSetArr)
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
        /// <summary>
        /// Ũ�ȸı�
        /// </summary>
        /// <param name="p_objConcentrations"></param>
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
        /// <summary>
        /// ���ݸı�
        /// </summary>
        /// <param name="p_objDatas"></param>
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
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_intQCBatchSeq"></param>
        /// <param name="p_dtpDateStart"></param>
        /// <param name="p_DateEnd"></param>

        public void Load(int p_intQCBatchSeq, DateTime p_dtpDateStart, DateTime p_DateEnd)
        {
            clsLisQCBatchVO qcBactchVo = null;
            clsLisQCConcentrationVO[] qcConcentrationVo = null;
            clsLisQCDataVO[] qCDataVo = null;
            clsLisQCReportVO[] qCReportVo = null;
            long num = 0L;
            num = this.m_objManage.m_lngFindQCBatch(p_intQCBatchSeq, true, out qcBactchVo);
            if (num > 0L && qcBactchVo != null)
            {
                num = this.m_objManage.m_lngFindQCConcentration(p_intQCBatchSeq, out qcConcentrationVo);
            }
            if (num > 0L && qcBactchVo != null)
            {
                num = this.m_objManage.m_lngFindQCData(out qCDataVo, p_intQCBatchSeq, p_dtpDateStart, p_DateEnd);
            }
            if (num > 0L && qcBactchVo != null)
            {
                num = this.m_objManage.m_lngFindQCReport(p_intQCBatchSeq, p_dtpDateStart, p_DateEnd, enmQCStatus.Natrural, out qCReportVo);
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
                        this.LoadFailed(this, new clsQCBatchNew.DataLoadFailedEventArgs("��ȡ����ʧ��."));
                    }
                }
                else
                {
                    if (qcBactchVo == null)
                    {
                        if (this.LoadFailed != null)
                        {
                            this.LoadFailed(this, new clsQCBatchNew.DataLoadFailedEventArgs("ָ�����ʿ���������."));
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_intQCBatchSeqArr"></param>
        /// <param name="p_dtpDateStart"></param>
        /// <param name="p_dtpDateEnd"></param>
        public void Load(int[] p_intQCBatchSeqArr, DateTime p_dtpDateStart, DateTime p_dtpDateEnd)
        {
            clsLisQCBatchVO[] qCbatchVo = null;
            clsLisQCConcentrationVO[] qCconcentrationVo = null;
            clsLisQCDataVO[] qCdataVo = null;
            clsLisQCReportVO[] qCreportVo = null;
            long num = 0L;
            num = this.m_objManage.m_lngFindQCBatch(p_intQCBatchSeqArr, true, out qCbatchVo);
            if (num > 0L && qCbatchVo != null)
            {
                num = this.m_objManage.m_lngFindQCConcentration(p_intQCBatchSeqArr, out qCconcentrationVo);
            }
            if (num > 0L && qCbatchVo != null)
            {
                num = this.m_objManage.m_lngFindQCData(out qCdataVo, p_intQCBatchSeqArr, p_dtpDateStart, p_dtpDateEnd);
            }
            if (num > 0L && qCbatchVo != null)
            {
                num = this.m_objManage.m_lngFindQCReport(p_intQCBatchSeqArr, p_dtpDateStart, p_dtpDateEnd, enmQCStatus.Natrural, out qCreportVo);
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
                        this.LoadFailed(this, new clsQCBatchNew.DataLoadFailedEventArgs("��ȡ����ʧ��."));
                    }
                }
                else
                {
                    if (qCbatchVo == null)
                    {
                        if (this.LoadFailed != null)
                        {
                            this.LoadFailed(this, new clsQCBatchNew.DataLoadFailedEventArgs("ָ�����ʿ���������."));
                        }
                    }
                }
            }
        }


        /// <summary>
        /// ���¼�������
        /// </summary>
        /// <param name="p_dtpDateStart"></param>
        /// <param name="p_DateEnd"></param>
        public void Reload(DateTime p_dtpDateStart, DateTime p_dtpDateEnd)
        {
            if (this.IsNull)
                return;

            clsLisQCDataVO[] objDatas = null;
            clsLisQCReportVO[] objReports = null;

            long lngRes = 0;

            lngRes = clsTmdQCDataSmp.s_object.m_lngFind(out objDatas, this.SeqArr, p_dtpDateStart, p_dtpDateEnd);

            if (lngRes > 0)
            {
                lngRes = clsTmdQCReportSmp.s_object.m_lngFind(this.SeqArr, p_dtpDateStart, p_dtpDateEnd, enmQCStatus.Natrural, out objReports);
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
                    LoadFailed(this, new DataLoadFailedEventArgs("��ȡ����ʧ��."));
                }
            }
        }
        #endregion

        #region ���ݲ���

        #region ��Ӳ���

        //public int m_intAdd(clsLisQCDataModule p_objQCModule)
        //{
        //    if (p_objQCModule == null)
        //        return -1;

        //    if (this.IsNull)
        //        m_lstDataModule = new List<clsLisQCDataModule>();

        //    m_lstDataModule.Add(p_objQCModule);
        //    return 1;
        //}

        //public int m_intAdd(clsLisQCConcentrationVO p_objQCcon)
        //{
        //    if (p_objQCcon == null)
        //        return -1;
        //    if (this.IsNull)
        //        return -1;
        //    foreach (clsLisQCDataModule objModule in m_lstDataModule)
        //    {
        //        if (objModule.m_objBatchSet.m_intSeq == p_objQCcon.m_intQCBatchSeq)
        //        {
        //            objModule.m_objConcentrations.Add(p_objQCcon);
        //            return 1;
        //        }
        //    }
        //    return 0;
        //}

        //public int m_intAdd(clsLisQCDataVO p_objQCData)
        //{
        //    if (p_objQCData == null)
        //        return -1;
        //    if (this.IsNull)
        //        return -1;
        //    foreach (clsLisQCDataModule objModule in m_lstDataModule)
        //    {
        //        if (objModule.m_objBatchSet.m_intSeq == p_objQCData.m_intQCBatchSeq)
        //        {
        //            objModule.m_objDatas.Add(p_objQCData);
        //            return 1;
        //        }
        //    }
        //    return 0;
        //}

        //public int m_intAdd(clsLisQCReportVO p_objQCReport)
        //{
        //    if (p_objQCReport == null)
        //        return -1;
        //    if (this.IsNull)
        //        return -1;
        //    foreach (clsLisQCDataModule objModule in m_lstDataModule)
        //    {
        //        if (objModule.m_objBatchSet.m_intSeq == p_objQCReport.m_intQCBatchSeq)
        //        {
        //            objModule.m_objReports.Add(p_objQCReport);
        //            return 1;
        //        }
        //    }
        //    return 0;
        //}

        //public int m_intAddRange(clsLisQCConcentrationVO[] p_objQCconArr)
        //{
        //    if (p_objQCconArr == null || p_objQCconArr.Length <= 0)
        //        return -1;

        //    if (this.IsNull)
        //        return -1;

        //    foreach (clsLisQCConcentrationVO objTemp in p_objQCconArr)
        //    {
        //        foreach (clsLisQCDataModule objModule in m_lstDataModule)
        //        {
        //            if (objModule.m_objBatchSet == null)
        //                continue;

        //            if (objModule.m_objBatchSet.m_intSeq == objTemp.m_intQCBatchSeq)
        //            {
        //                objModule.m_objConcentrations.Add(objTemp);
        //                break;
        //            }
        //        }
        //    }

        //    return 1;
        //}

        //public int m_intAddRange(clsLisQCDataVO[] p_objQCDataArr)
        //{
        //    if (p_objQCDataArr == null || p_objQCDataArr.Length <= 0)
        //        return -1;

        //    if (this.IsNull)
        //        return -1;

        //    foreach (clsLisQCDataVO objTemp in p_objQCDataArr)
        //    {
        //        foreach (clsLisQCDataModule objModule in m_lstDataModule)
        //        {
        //            if (objModule.m_objBatchSet == null)
        //                continue;

        //            if (objModule.m_objBatchSet.m_intSeq == objTemp.m_intQCBatchSeq)
        //            {
        //                objModule.m_objDatas.Add(objTemp);
        //                break;
        //            }
        //        }
        //    }

        //    return 1;
        //}

        //public int m_intAddRange(clsLisQCReportVO[] p_objQCReportArr)
        //{
        //    if (p_objQCReportArr == null || p_objQCReportArr.Length <= 0)
        //        return -1;

        //    if (this.IsNull)
        //        return -1;

        //    foreach (clsLisQCReportVO objTemp in p_objQCReportArr)
        //    {
        //        foreach (clsLisQCDataModule objModule in m_lstDataModule)
        //        {
        //            if (objModule.m_objBatchSet == null)
        //                continue;

        //            if (objModule.m_objBatchSet.m_intSeq == objTemp.m_intQCBatchSeq)
        //            {
        //                objModule.m_objReports.Add(objTemp);
        //                break;
        //            }
        //        }
        //    }

        //    return 1;
        //}

        #endregion

        #region �������

        //public int m_intClearDatas()
        //{
        //    if (this.IsNull)
        //        return 0;

        //    foreach (clsLisQCDataModule objModule in m_lstDataModule)
        //    {
        //        objModule.m_objDatas.Clear();
        //    }
        //    return 1;
        //}

        //public int m_intClearReports()
        //{
        //    if (this.IsNull)
        //        return 0;

        //    foreach (clsLisQCDataModule objModule in m_lstDataModule)
        //    {
        //        objModule.m_objReports.Clear();
        //    }
        //    return 1;
        //}

        //public int m_intClearDataAndReports()
        //{
        //    if (this.IsNull)
        //        return 0;

        //    foreach (clsLisQCDataModule objModule in m_lstDataModule)
        //    {
        //        objModule.m_objDatas.Clear();
        //        objModule.m_objReports.Clear();
        //    }
        //    return 1;

        //}
        #endregion

        #endregion


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



        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_intBatchSeq"></param>
        /// <param name="p_strSampleId"></param>
        /// <returns></returns>
        public long m_lngQueryDeviceSampleID(int intBatchSeq, out string strSampleId)
        {
             return m_objManage.m_lngQueryDeviceSampleID(intBatchSeq, out strSampleId);
        }
        #endregion


        public long m_lngReceiveDeviceQCDataBySampleID(string p_strSampleID, string p_strStartDat, string p_strEndDat, int[] p_intBatchSeqArr, out clsLisQCDataVO[] p_objQCDataArr)
        {
            return m_objManage.m_lngReceiveDeviceQCDataBySampleID(p_strSampleID, p_strStartDat, p_strEndDat, p_intBatchSeqArr, out p_objQCDataArr);
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


    }
}
