// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision: 915 $</version>
// </file>

using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.IO;

namespace ICSharpCode.TextEditor.Document
{
	public class ResourceSyntaxModeProvider : ISyntaxModeFileProvider
	{
		ArrayList syntaxModes = null;
		
		public ArrayList SyntaxModes {
			get {
				return syntaxModes;
			}
		}
		
		public ResourceSyntaxModeProvider()
		{
            String projectName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
			Assembly assembly = typeof(SyntaxMode).Assembly;
            // SyntaxModes.xml 文件右键属性-》资源文件，否则找不到,一直为 null.
            Stream syntaxModeStream = assembly.GetManifestResourceStream("Common.Controls.custom.ICSharpCode.Resources.SyntaxModes.xml");
			if (syntaxModeStream != null) {
				syntaxModes = SyntaxMode.GetSyntaxModes(syntaxModeStream);
			} else {
				syntaxModes = new ArrayList();
			}
		}
		
		public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
		{
			Assembly assembly = typeof(SyntaxMode).Assembly;
            return new XmlTextReader(assembly.GetManifestResourceStream("Common.Controls.custom.ICSharpCode.Resources." + syntaxMode.FileName));
		}
		
		public void UpdateSyntaxModeList()
		{
			// resources don't change during runtime
		}
	}
}
