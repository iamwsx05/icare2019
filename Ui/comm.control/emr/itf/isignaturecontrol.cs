using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Entity;

namespace Common.Controls.Emr
{
    public interface ISignatureControl
    {
        void ClearText();
        void AddSignEmp(EntitySignature dcSignature);
        List<EntitySignature> GetSignEmp();
        string Caption { get; set; }
        /// <summary>
        /// 是否自动签当前登陆者的名字
        /// </summary>
        int IsAutoSignature { get; set; }
        /// <summary>
        /// 是否允许不签名
        /// </summary>
        int IsAllowSignNull { get; set; }
    }
}
