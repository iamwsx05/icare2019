namespace com.digitalwave.Utility
{
	public class clsRomanNumeric
	{
		private static readonly string [] c_strRomansArr = new string[]{"I","IV","V","IX","X","XL","L","XC","C","CD","D","CM","M"};
		private static readonly int [] c_intArabicsArr = new int[]{1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };

		public static string m_strDecToRoman(int p_intValue)
		{
			string strResult = "";

			for(int i=12;i>=0;i--)
			{
				while(p_intValue >= c_intArabicsArr[i])
				{
					p_intValue = p_intValue - c_intArabicsArr[i];
					strResult += c_strRomansArr[i];
				}
			}

			return strResult;
		}
	}
}