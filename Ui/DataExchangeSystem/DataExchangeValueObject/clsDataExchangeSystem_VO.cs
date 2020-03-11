using System;
using System.Collections.Generic;
using System.Text;

namespace com.digitalwave.iCare.ValueObject
{
    /// <summary>
    /// 茶山入库数据VO
    /// </summary>
    [Serializable]
    public class clsInStorageData_VO : com.digitalwave.iCare.ValueObject.clsICARE_BaseVO
    {
        /// <summary>
        /// [业务类别] [nvarchar] (20)  
        /// </summary>
        public string YWLB;
        /// <summary>
        /// [单据编号] [nvarchar] (20)   NOT NULL 
        /// </summary>
        public string DJBH;
        /// <summary>
        ///  [日期] [smalldatetime] NOT NULL 
        /// </summary>
        public DateTime RQ;
        /// <summary>
        /// [药库号] [nvarchar] (20)  
        /// </summary>
        public string YKH;
        /// <summary>
        /// [药库名称] [nvarchar] (100)  
        /// </summary>
        public string YKMC;
        /// <summary>
        /// [单位编号] [nvarchar] (20)   NOT NULL 
        /// </summary>
        public string DWBH;
        /// <summary>
        /// [单位名称] [nvarchar] (100)  
        /// </summary>
        public string DWMC;
        /// <summary>
        /// [项目编号] [nvarchar] (20)  
        /// </summary>
        public string XMBH;
        /// <summary>
        /// [项目名称] [nvarchar] (100) 
        /// </summary>
        public string XMMC;
        /// <summary>
        /// [买入金额] [float] NOT NULL 
        /// </summary>
        public float MRJE;
        /// <summary>
        /// [零售金额] [float] NOT NULL 
        /// </summary>
        public float LSJE;
        /// <summary>
        /// [进零差价] [float] NOT NULL 
        /// </summary>
        public float JLCJ;
        /// <summary>
        /// [标识] [nvarchar] (20) 
        /// </summary>
        public string BZ;
        /// <summary>
        /// [凭证类别] [nvarchar] (50)  
        /// </summary>
        public string PZLB;
        /// <summary>
        /// [凭证日期] [smalldatetime]  
        /// </summary>
        public string PZRQ;
        /// <summary>
        /// [凭证编号] [nvarchar] (20)  
        /// </summary>
        public string PZBH;
        /// <summary>
        /// [记帐] [nvarchar] (10) 
        /// </summary>
        public string JZ;
    }

    /// <summary>
    /// 茶山出库数据VO
    /// </summary>
    [Serializable]
    public class clsOutStorageData_VO : com.digitalwave.iCare.ValueObject.clsICARE_BaseVO
    {
        /// <summary>
        /// [业务类别] [nvarchar] (20)  
        /// </summary>
        public string YWLB;
        /// <summary>
        /// [单据编号] [nvarchar] (20)  NOT NULL  
        /// </summary>
        public string DJBH;
        /// <summary>
        /// [日期] [smalldatetime] NOT NULL 
        /// </summary>
        public DateTime RQ;
        /// <summary>
        /// [药库号] [nvarchar] (20)  
        /// </summary>
        public string YKH;
        /// <summary>
        /// [药库名称] [nvarchar] (100)  
        /// </summary>
        public string YKMC;
        /// <summary>
        /// [单位编号] [nvarchar] (20)  NOT NULL  
        /// </summary>
        public string DWBH;
        /// <summary>
        /// [单位名称] [nvarchar] (100)  
        /// </summary>
        public string DWMC;
        /// <summary>
        /// [项目编号] [nvarchar] (20)  
        /// </summary>
        public string XMBH;
        /// <summary>
        /// [项目名称] [nvarchar] (100) 
        /// </summary>
        public string XMMC;
        /// <summary>
        /// [买入金额] [float] NOT NULL  
        /// </summary>
        public float MRJE;
        /// <summary>
        /// [零售金额] [float] NOT NULL 
        /// </summary>
        public float LSJE;
        /// <summary>
        /// [进零差价] [float] NOT NULL  
        /// </summary>
        public float JLCJ;
        /// <summary>
        /// [标识] [nvarchar] (20) 
        /// </summary>
        public string BZ;
        /// <summary>
        /// [凭证类别] [nvarchar] (50)  
        /// </summary>
        public string PZLB;
        /// <summary>
        /// [凭证日期] [smalldatetime]  
        /// </summary>
        public DateTime PZRQ;
        /// <summary>
        /// [凭证编号] [nvarchar] (20) 
        /// </summary>
        public string PZBH;
        /// <summary>
        /// [记帐] [nvarchar] (10) 
        /// </summary>
        public string JZ;
    }

    /// <summary>
    /// 茶山住院收入数据
    /// </summary>
    [Serializable]
    public class clsInHospital_VO : com.digitalwave.iCare.ValueObject.clsICARE_BaseVO
    {
        /// <summary>
        /// [单据编号] [nvarchar] (20)  NOT NULL 
        /// </summary>
        public string DJBH;
        /// <summary>
        /// [标识] [nvarchar] (20)  
        /// </summary>
        public string BZ;
        /// <summary>
        /// [日期] [smalldatetime] NOT NULL 
        /// </summary>
        public DateTime RQ;
        /// <summary>
        /// [部门编号] [nvarchar] (20)   NOT NULL 
        /// </summary>
        public string BMBH;
        /// <summary>
        /// [部门名称] [nvarchar] (100)  
        /// </summary>
        public string BMMC;
        /// <summary>
        /// [医生编号] [nvarchar] (20) 
        /// </summary>
        public string YSBH;
        /// <summary>
        /// [医生名称] [nvarchar] (100) 
        /// </summary>
        public string YSMC;
        /// <summary>
        /// [项目编号] [nvarchar] (20)  NOT NULL 
        /// </summary>
        public string XMBH;
        /// <summary>
        /// [项目名称] [nvarchar] (100)  
        /// </summary>
        public string XMMC;
        /// <summary>
        /// [项目金额] [float] NOT NULL  
        /// </summary>
        public float XMJE;
        /// <summary>
        /// [凭证类别] [nvarchar] (20)  
        /// </summary>
        public string PZLB;
        /// <summary>
        /// [凭证日期] [smalldatetime] 
        /// </summary>
        public DateTime PZRQ;
        /// <summary>
        /// [凭证编号] [nvarchar] (20) 
        /// </summary>
        public string PZBH;
        /// <summary>
        /// [记帐] [nvarchar] (10)  
        /// </summary>
        public string JZ;
    }

    /// <summary>
    /// 茶山门诊收入数据
    /// </summary>
    [Serializable]
    public class clsOutpatient_VO : com.digitalwave.iCare.ValueObject.clsICARE_BaseVO
    {
        /// <summary>
        /// [单据编号] [nvarchar] (20)  NOT NULL 
        /// </summary>
        public string DJBH;
        /// <summary>
        /// [标识] [nvarchar] (20)  
        /// </summary>
        public string BZ;
        /// <summary>
        /// [日期] [smalldatetime] NOT NULL  
        /// </summary>
        public DateTime RQ;
        /// <summary>
        /// [部门编号] [nvarchar] (20)  NOT NULL 
        /// </summary>
        public string BMBH;
        /// <summary>
        /// [部门名称] [nvarchar] (100)  
        /// </summary>
        public string BMMC;
        /// <summary>
        /// [医生编号] [nvarchar] (20)  
        /// </summary>
        public string YSBH;
        /// <summary>
        /// [医生名称] [nvarchar] (100)  
        /// </summary>
        public string YSMC;
        /// <summary>
        /// [项目编号] [nvarchar] (20)   NOT NULL  
        /// </summary>
        public string XMBH;
        /// <summary>
        /// [项目名称] [nvarchar] (100) 
        /// </summary>
        public string XMMC;
        /// <summary>
        ///  [项目金额] [float] NOT NULL  
        /// </summary>
        public float XMJE;
        /// <summary>
        /// [凭证类别] [nvarchar] (20) 
        /// </summary>
        public string PZLB;
        /// <summary>
        /// [凭证日期] [smalldatetime]  
        /// </summary>
        public DateTime PZRQ;
        /// <summary>
        /// [凭证编号] [nvarchar] (20) 
        /// </summary>
        public string PZBH;
        /// <summary>
        /// [记帐] [nvarchar] (10) 
        /// </summary>
        public string JZ;
        /// <summary>
        /// [收费处标识] [nvarchar] (50) 
        /// </summary>
        public string SFCBZ;
    }
}
