using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Entity;

namespace Common.Controls.Emr
{
    public interface IRtfEditor
    {
        /// <summary>
        /// 设置XML
        /// </summary>
        /// <param name="p_bytRtfArr"></param>
        /// <param name="p_strXML"></param>
        /// <param name="p_blnLoadCaseFlag">调用历史病历标志 true 是 false 否</param>
        void SetXmlText(byte[] p_bytRtfArr, string p_strXML, bool p_blnLoadCaseFlag);

        /// <summary>
        /// 单纯的不含修改信息的文本
        /// </summary>
        /// <returns></returns>
        string GetRightText();

        /// <summary>
        /// 带格式的xml
        /// </summary>
        /// <returns></returns>
        string GetXmlText();

        /// <summary>
        /// 带格式的xml
        /// </summary>
        /// <returns></returns>
        string GetXmlText(DateTime? p_dtmCaseWriteDate);

        /// <summary>
        /// 整个rtf信息
        /// </summary>
        /// <returns></returns>
        byte[] GetAllRtf();

        /// <summary>
        /// 打印用的rtf信息
        /// </summary>
        /// <returns></returns>
        byte[] GetPrintRtf();

        /// <summary>
        /// 获取图像信息
        /// </summary>
        /// <returns></returns>
        List<EntityCaseRtbImage> GetRtbImage();

        /// <summary>
        /// 设置图像
        /// </summary>
        /// <param name="p_lstRtbImage"></param>
        void SetRtbImage(List<EntityCaseRtbImage> p_lstRtbImage);

        /// <summary>
        /// 清空内容
        /// </summary>
        void ClearText();

        /// <summary>
        /// 右键菜单
        /// </summary>
        System.Windows.Forms.ContextMenuStrip ContextMenuStrip { get; set; }

        /// <summary>
        /// 是否允许右键菜单
        /// </summary>
        bool EnableContextMenuStrip { get; set; }

        /// <summary>
        /// 图片标志 true 是；false 是。
        /// </summary>
        //bool ImageFlag { get; set; }
        /// <summary>
        /// 多行编辑标识
        /// </summary>
        bool Multiline { get; set; }

        /// <summary>
        /// 固定高度
        /// </summary>
        bool FixedHeight { get; set; }

        string Text { get; set; }

        event EventHandler Enter;

        /// <summary>
        /// 设置首行标题
        /// </summary>
        void SetFirstlineCaption();

        /// <summary>
        /// 绑定页(主要针对表格式病历)
        /// </summary>
        bool BandingPage { get; set; }

        /// <summary>
        /// 设置系统登录人
        /// </summary>
        /// <param name="p_strLoginUserID">ID</param>
        /// <param name="p_strLoginUserName">名称</param>
        void SetLoginUser(string p_strLoginUserID, string p_strLoginUserName);

        /// <summary>
        /// 默认行数
        /// </summary>
        int DefaultRows { get; set; }

        /// <summary>
        /// 行缩进字符个数
        /// </summary>
        int RowShrinkdigit { get; set; }

        /// <summary>
        /// 首行标题字符
        /// </summary>
        string FirstlineCaption { get; set; }
    }

    /// <summary>
    /// RichText样式接口
    /// </summary>
    public interface IRichTextStyle
    {
        /// <summary>
        /// 显示元素红点
        /// </summary>
        bool blnShowElementRedPoint { get; set; }
    }
}
