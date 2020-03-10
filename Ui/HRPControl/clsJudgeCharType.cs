using System;

namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// Summary description for clsJudgeCharType.
	/// </summary>
	public class clsJudgeCharType
	{
		/// <summary>
		/// �ж��ַ�������
		/// </summary>
		/// <param name="p_chr"></param>
		/// <returns></returns>
		public static enmCharType s_enmGetCharType(char p_chr)
		{			
			if(char.IsPunctuation(p_chr))
				return enmCharType.Punctuation;
			else if(char.GetUnicodeCategory(p_chr).Equals(System.Globalization.UnicodeCategory.LowercaseLetter) || char.GetUnicodeCategory(p_chr).Equals(System.Globalization.UnicodeCategory.UppercaseLetter))
				return enmCharType.Letter;
			else if(char.IsNumber(p_chr))
				return enmCharType.Number;
			else
				return enmCharType.Normal;
			
		}

		#region old
//		public static void s_mthMoveLastLetters(string p_strMain,ref int p_intEndIndex)
//		{
//			if(s_enmGetCharType(p_strMain[p_intEndIndex]).Equals(enmCharType.Letter))
//			{
//				p_intEndIndex--;
//				s_mthMoveLetters(p_strMain,ref p_intEndIndex);
//			}
//			else
//				return;
//		}
		#endregion

		/// <summary>
		/// ȥ���ַ��������Ӣ����ĸ
		/// </summary>
		/// <param name="p_strMain"></param>
		public static void s_mthMoveLastLetters(ref string p_strMain)
		{
			if(p_strMain.Length == 0)
				return;
			if(s_enmGetCharType(p_strMain[p_strMain.Length - 1]).Equals(enmCharType.Letter))
			{
				try
				{
					p_strMain = p_strMain.Substring(0,p_strMain.Length - 1);
				}
				catch{return;}
				s_mthMoveLastLetters(ref p_strMain);
			}
		}

		/// <summary>
		/// ȥ���ַ����������ֻ�'.'
		/// </summary>
		/// <param name="p_strMain"></param>
		public static void s_mthMoveLastNumbersOrDot(ref string p_strMain)
		{
			if(s_enmGetCharType(p_strMain[p_strMain.Length - 1]).Equals(enmCharType.Number) || p_strMain[p_strMain.Length - 1]=='.')
			{
				p_strMain = p_strMain.Substring(0,p_strMain.Length - 1);
				s_mthMoveLastNumbersOrDot(ref p_strMain);
			}
		}

	}

	public enum enmCharType
	{
		/// <summary>
		/// һ��
		/// </summary>
		Normal,
		/// <summary>
		/// ������
		/// </summary>
		Punctuation,
		/// <summary>
		/// ��ĸ
		/// </summary>
		Letter,
		/// <summary>
		/// ��ֵ
		/// </summary>
		Number,
	}
}
