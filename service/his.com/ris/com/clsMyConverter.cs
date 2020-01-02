using System;

namespace iCare.RIS.Utility
{
	/// <summary>
	/// clsMyConverter 的摘要说明。
	/// </summary>
    public class clsMyConverter	{
		public static float ToSingle(object objValue)
		{
			try
			{
				return Convert.ToSingle(objValue);
			}
			catch(Exception)
			{
				return 0;
			}
		}
		
		public static int ToInt(object objValue)
		{
			return ToInt32(objValue);
		}

		public  static int ToInt32(object objValue)
		{
			try
			{
				return Convert.ToInt32(objValue);
			}
			catch(Exception)
			{
				return 0;
			}
		}
		public  static DateTime ToDateTime(object objValue)
		{
			try
			{
				return Convert.ToDateTime(objValue);
			}
			catch(Exception)
			{
				return DateTime.Parse("1900-1-1");
			}
		}

		public  static decimal ToDecimal(object objValue)
		{
			try
			{
				return Convert.ToDecimal(objValue);
			}
			catch(Exception)
			{
				return 0;
			}
		}

		public static  bool ToBoolean(object objValue)
		{
			try
			{
				return Convert.ToBoolean(objValue);
			}
			catch(Exception)
			{
				return false;
			}
		}

		public static byte ToByte(object objValue)
		{
			try
			{
				return Convert.ToByte(objValue);
			}
			catch(Exception)
			{
				return 0;
			}
		}

        public static string ToString(object objValue)
        {
            if (objValue == null )
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(objValue);
            }            
        }

	}
}
