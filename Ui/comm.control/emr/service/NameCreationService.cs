using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 控件命名服务,用于生成控件名称
    /// </summary>
    public class NameCreationService : INameCreationService
    {
        private IDesignerHost host;
        public NameCreationService(IDesignerHost designerhost)
        {
            host = designerhost;
        }

        /// <summary>
        /// 生成控件名：当前名称最大值+1
        /// </summary>
        /// <param name="container"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public string CreateName(System.ComponentModel.IContainer container, System.Type dataType)
        {
            int i = 0;
            string name = FormTool.CtrlName(dataType);

            while (true)
            {
                i++;
                if (container.Components[name + i.ToString()] == null)
                    break;
            }

            name = name + i.ToString();
            return name;
        }

        public void ValidateName(string name)
        {
            if (!IsValidName(name))
            {
               DialogBox.Msg("无效的控件名称", MessageBoxIcon.Error);
            }
        }

        public bool IsValidName(string name)
        {
            if (name == null || name.Length == 0)
                return false;

            if (!Char.IsLetter(name, 0))
                return false;

            if (name.StartsWith("_"))
                return false;

            for (int i = 0; i < name.Length; i++)
            {
                if (!Char.IsLetterOrDigit(name, i))
                    return false;
            }
            return true;
        }

    }
}
