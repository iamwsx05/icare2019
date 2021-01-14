using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using weCare.Core.Entity;
using weCare.Core.Itf;
using weCare.Core.Utils;
using System.Data;

namespace Com.svc
{
    [ServiceContract]
    public interface PeInterface : IWcf, IDisposable
    {
        /// <summary>
        /// 广东省.健康证服务.服务契约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract(Name = "CallFunc")]
        string CallFunc(string request);


        [OperationContract(Name = "ExecSql")]
        int ExecSql(string file,string sql);

        [OperationContract(Name = "GetDataTable")]
        DataTable GetDataTable(string file,string sql);

    }
}