using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 消息筛选器，用于设计面板的键盘事件
    /// </summary>
    public class KeystrokMessageFilter : System.Windows.Forms.IMessageFilter
    {

        private IDesignerHost host;

        private DesignSurface _DesignSurface;

        public bool AllowDeleteKey = true;

        public KeystrokMessageFilter()
        {

        }

        public void SetHostAndMDIForm(IDesignerHost host, DesignSurface DesSurface)
        {

            this.host = host;

            _DesignSurface = DesSurface;

        }

        #region IMessageFilter

        public bool PreFilterMessage(ref Message m)
        {

            if (_DesignSurface == null)

                return false;

            //WM_KEYDOWN
            if (m.Msg != 256)
            {
                return false;
            }

            if (((Control)_DesignSurface.View).Focused)
            {
                IMenuCommandService mcs = host.GetService(typeof(IMenuCommandService)) as IMenuCommandService;

                switch (((int)m.WParam) | ((int)Control.ModifierKeys))
                {

                    case (int)Keys.Up: mcs.GlobalInvoke(MenuCommands.KeyMoveUp);

                        break;

                    case (int)Keys.Down: mcs.GlobalInvoke(MenuCommands.KeyMoveDown);

                        break;

                    case (int)Keys.Right: mcs.GlobalInvoke(MenuCommands.KeyMoveRight);

                        break;

                    case (int)Keys.Left: mcs.GlobalInvoke(MenuCommands.KeyMoveLeft);

                        break;

                    case (int)(Keys.Control | Keys.Up): mcs.GlobalInvoke(MenuCommands.KeyNudgeUp);

                        break;

                    case (int)(Keys.Control | Keys.Down): mcs.GlobalInvoke(MenuCommands.KeyNudgeDown);

                        break;

                    case (int)(Keys.Control | Keys.Right): mcs.GlobalInvoke(MenuCommands.KeyNudgeRight);

                        break;

                    case (int)(Keys.Control | Keys.Left): mcs.GlobalInvoke(MenuCommands.KeyNudgeLeft);

                        break;

                    case (int)(Keys.Shift | Keys.Up): mcs.GlobalInvoke(MenuCommands.KeySizeHeightIncrease);

                        break;

                    case (int)(Keys.Shift | Keys.Down): mcs.GlobalInvoke(MenuCommands.KeySizeHeightDecrease);

                        break;

                    case (int)(Keys.Shift | Keys.Right): mcs.GlobalInvoke(MenuCommands.KeySizeWidthIncrease);

                        break;

                    case (int)(Keys.Shift | Keys.Left): mcs.GlobalInvoke(MenuCommands.KeySizeWidthDecrease);

                        break;

                    case (int)(Keys.Control | Keys.Shift | Keys.Up): mcs.GlobalInvoke(MenuCommands.KeyNudgeHeightIncrease);

                        break;

                    case (int)(Keys.Control | Keys.Shift | Keys.Down): mcs.GlobalInvoke(MenuCommands.KeyNudgeHeightDecrease);

                        break;

                    case (int)(Keys.Control | Keys.Shift | Keys.Right): mcs.GlobalInvoke(MenuCommands.KeyNudgeWidthIncrease);

                        break;

                    case (int)(Keys.ControlKey | Keys.Shift | Keys.Left): mcs.GlobalInvoke(MenuCommands.KeyNudgeWidthDecrease);

                        break;

                    case (int)(Keys.Escape): mcs.GlobalInvoke(MenuCommands.KeyCancel);

                        break;

                    case (int)(Keys.Shift | Keys.Escape): mcs.GlobalInvoke(MenuCommands.KeyReverseCancel);

                        break;

                    case (int)(Keys.Control | Keys.C): mcs.GlobalInvoke(StandardCommands.Copy);
                        break;

                    case (int)(Keys.Control | Keys.V): mcs.GlobalInvoke(StandardCommands.Paste);
                        break;

                    case (int)(Keys.Control | Keys.X): mcs.GlobalInvoke(StandardCommands.Cut);
                        break;

                    case (int)(Keys.Enter): mcs.GlobalInvoke(MenuCommands.KeyDefaultAction);

                        break;

                    case (int)(Keys.Delete):

                        if (AllowDeleteKey)

                            mcs.GlobalInvoke(MenuCommands.Delete);

                        break;

                    case (int)(Keys.Control | Keys.Z):
                        mcs.GlobalInvoke(StandardCommands.Undo);
                        break;
                }

            }

            return false;

        }

        #endregion


    }

}
