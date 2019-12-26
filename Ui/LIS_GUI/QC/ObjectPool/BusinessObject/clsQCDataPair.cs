using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// �ʿ�����,ͬһ�ʿ�ʱ�䣬ͬһ�ʿ���Ŀ����ͬ�ʿ�Ũ�ȵ�����
    /// </summary>
    public class clsQCDataPair
    {
        private DateTime m_dtQcdate;
        public DateTime QCDate
        {
            get { return m_dtQcdate; }
            set { m_dtQcdate = value; }
        }
        private int m_intSeq;

        public int Seq
        {
            get { return m_intSeq; }
            set { m_intSeq = value; }
        }

        private List<clsLisQCDataVO> m_arrData = new List<clsLisQCDataVO>();

        public clsLisQCDataVO this[int contrationId]
        {
            get
            {
                for (int i = 0; i < m_arrData.Count; i++)
                {
                    if (m_arrData[i].m_intConcentrationSeq == contrationId)
                    {
                        return m_arrData[i];
                    }
                }
                return null;
            }
        }

        public bool Add(clsLisQCDataVO p_objDataVO)
        {
            bool isExist = IsExistSameConcentration(p_objDataVO);
            if (isExist)
            {
                return false;
            }
            m_arrData.Add(p_objDataVO);
            return true;
        }

        private bool IsExistSameConcentration(clsLisQCDataVO p_objDataVO)
        {
            foreach (clsLisQCDataVO vo in m_arrData)
            {
                if (p_objDataVO.m_intConcentrationSeq == vo.m_intConcentrationSeq)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<clsQCDataPair> GetQCDataPairList(List<clsLisQCDataVO> p_dataList)
        {
            List<clsQCDataPair> lstDataPair = new List<clsQCDataPair>();
            p_dataList.Sort(CompareQCDataVO);

            bool isAdd = false;
            foreach (clsLisQCDataVO vo in p_dataList)
            {
                foreach (clsQCDataPair pair in lstDataPair)
                {
                    isAdd = false;
                    if (vo.m_datQCDate == pair.m_dtQcdate)
                    {
                        if (pair.Add(vo))
                        {
                            isAdd = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                //��������ͬʱ���
                if (!isAdd) 
                {
                    clsQCDataPair newPair2 = new clsQCDataPair();
                    newPair2.Add(vo);
                    newPair2.m_dtQcdate = vo.m_datQCDate;
                    lstDataPair.Add(newPair2);
                }
                
            }
            return lstDataPair;
        }

        private static int CompareQCDataVO(clsLisQCDataVO x, clsLisQCDataVO y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                    return 1;
                else
                {
                    if (x.m_datQCDate > y.m_datQCDate)
                    {
                        return 1;
                    }
                    if (x.m_datQCDate == y.m_datQCDate)
                    {
                        if (x.m_intSeq > y.m_intSeq)
                        {
                            return 1;
                        }
                        else
                        {
                            if (x.m_intSeq == y.m_intSeq)
                            {
                                return 0;
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }
                    else 
                    {
                        return -1;
                    }

                }

            }
        }
    }

    /// <summary>
    /// �ʿ�����,ͬһ�ʿ�ʱ�䣬��ͬ�ʿ���Ŀ����һ�ʿ�Ũ�ȵ�����
    /// </summary>
    public class clsQCDataPairItem
    {
        /// <summary>
        /// �ʿ�ʱ��
        /// </summary>
        private DateTime m_dtQcdate;
        /// <summary>
        /// �ʿ�ʱ��
        /// </summary>
        public DateTime QCDate
        {
            get { return m_dtQcdate; }
            set { m_dtQcdate = value; }
        }


        private List<clsLisQCDataVO> m_arrData = new List<clsLisQCDataVO>();
        /// <summary>
        /// ��ȡָ���ʿ�����ŵĵ�һ���ʿ�����
        /// </summary>
        /// <param name="contrationId"></param>
        /// <returns></returns>
        public clsLisQCDataVO this[int p_intBatchSeq]
        {
            get
            {
                for (int i = 0; i < m_arrData.Count; i++)
                {
                    if (m_arrData[i].m_intQCBatchSeq == p_intBatchSeq)
                    {
                        return m_arrData[i];
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// �����ʿ��������������true�����������ͬ���ʿ�����������Ӳ�����false
        /// </summary>
        /// <param name="p_objDataVO"></param>
        /// <returns></returns>
        public bool Add(clsLisQCDataVO p_objDataVO)
        {
            bool isExist = IsExistSameBatch(p_objDataVO);
            if (isExist)
            {
                return false;
            }
            m_arrData.Add(p_objDataVO);
            return true;
        }
        /// <summary>
        /// �ж��Ƿ������ָ�����������ͬ��Ũ�����,������ڷ���true�����򷵻�false
        /// </summary>
        /// <param name="p_objDataVO"></param>
        /// <returns></returns>
        private bool IsExistSameBatch(clsLisQCDataVO p_objDataVO)
        {
            foreach (clsLisQCDataVO vo in m_arrData)
            {
                if (p_objDataVO.m_intQCBatchSeq == vo.m_intQCBatchSeq)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// �����ʿ����ݶ��б�
        /// </summary>
        /// <param name="p_dataList"></param>
        /// <returns></returns>
        public static List<clsQCDataPairItem> GetQCDataPairItemList(List<clsLisQCDataVO> p_dataList)
        {
            List<clsQCDataPairItem> lstDataPair = new List<clsQCDataPairItem>();
            p_dataList.Sort(CompareQCDataVO);

            bool isAdd = false;
            foreach (clsLisQCDataVO vo in p_dataList)
            {
                foreach (clsQCDataPairItem pair in lstDataPair)
                {
                    isAdd = false;
                    if (vo.m_datQCDate == pair.m_dtQcdate)
                    {
                        if (pair.Add(vo))
                        {
                            isAdd = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                //��������ͬʱ���
                if (!isAdd)
                {
                    clsQCDataPairItem newPair2 = new clsQCDataPairItem();
                    newPair2.Add(vo);
                    newPair2.m_dtQcdate = vo.m_datQCDate;
                    lstDataPair.Add(newPair2);
                }

            }
            return lstDataPair;
        }
        /// <summary>
        /// �ʿ������Ľ���Ƚϣ������ã����ʿ����� Asc, ���ʽ����� Asc
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int CompareQCDataVO(clsLisQCDataVO x, clsLisQCDataVO y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                    return 1;
                else
                {
                    if (x.m_datQCDate > y.m_datQCDate)
                    {
                        return 1;
                    }
                    if (x.m_datQCDate == y.m_datQCDate)
                    {
                        if (x.m_intSeq > y.m_intSeq)
                        {
                            return 1;
                        }
                        else
                        {
                            if (x.m_intSeq == y.m_intSeq)
                            {
                                return 0;
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }
                    else
                    {
                        return -1;
                    }

                }

            }
        }
    }
}
