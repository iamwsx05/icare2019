using System;
using System.Collections;
using System.Reflection;

namespace com.digitalwave.iCare.ValueObject
{
	/// <summary>
	/// Summary description for clsICARE_BaseVO.
	/// </summary>
	//��������iCare��Ŀ��ValueObject����
	[Serializable]
	public class clsICARE_BaseVO 
	{
		public clsICARE_BaseVO()
		{

		}
		/// <summary>
        /// ֻ����ֵ��(����string),����������;(��������,˽�м�������Ա)
        /// �쳣:1.������Ϊ��ʱ���� ArgumentNullException ;
        ///      2.��Ŀ�����Ͳ�ͬ��Դ����ʱ(��ȫ��ͬ)���� ArgumentException("Type unmatched.").
		/// </summary>
		/// <param name="objTarget"></param>
        public virtual void m_mthCopyTo(clsICARE_BaseVO objTarget)
        {
            if (objTarget == null)
                throw new System.ArgumentNullException();

            if (objTarget.GetType() != this.GetType())
                throw new System.ArgumentException("Type unmatched.");

            System.Reflection.FieldInfo[] objFields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (System.Reflection.FieldInfo fd in objFields)
            {
                if (fd.FieldType.IsValueType || fd.FieldType == typeof(string))
                {
                    object obj = fd.GetValue(this);
                    fd.SetValue(objTarget, obj, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null);
                }
            }
        }
        /// <summary>
        /// ǳ����,����ֵ��(����string)��ֵ,ͬʱ���������������;(��������,˽�м�������Ա)
        /// �쳣:1.������Ϊ��ʱ���� ArgumentNullException ;
        ///      2.��Ŀ�����Ͳ�ͬ��Դ����ʱ(��ȫ��ͬ)���� ArgumentException("Type unmatched.").
        /// </summary>
        /// <param name="objTarget"></param>
        public virtual void m_mthClone(clsICARE_BaseVO objTarget)
        { 
            if (objTarget == null)
                throw new System.ArgumentNullException();

            if (objTarget.GetType() != this.GetType())
                throw new System.ArgumentException("Type unmatched.");

            System.Reflection.FieldInfo[] objFields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (System.Reflection.FieldInfo fd in objFields)
            {
                object obj = fd.GetValue(this);
                fd.SetValue(objTarget, obj, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null);
            }
        }
		/// <summary>
		/// ��ȫ����������������������ֵ��(��������,˽�м�������Ա)
        /// �쳣:1.������Ϊ��ʱ���� ArgumentNullException ;
        ///      2.��Ŀ�����Ͳ�ͬ��Դ����ʱ(��ȫ��ͬ)���� ArgumentException("Type unmatched.").
		/// </summary>
		/// <param name="objTarget"></param>
		public void m_mthTreeCopyTo(object objTarget)
		{
            if (objTarget == null)
                throw new System.ArgumentNullException();
            if (objTarget.GetType() != this.GetType())
                throw new System.ArgumentException("Type unmatched.");

			this.m_mthTreeCopy(this,objTarget);
		}
        private void m_mthTreeCopy(object objs, object objt)
        {
            Type typeS = objs.GetType();
            Type typeD = objt.GetType();

            System.Reflection.FieldInfo[] objFields = typeS.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (System.Reflection.FieldInfo fd in objFields)
            {
                object obj = fd.GetValue(objs);
                if (fd.FieldType.IsValueType || fd.FieldType == typeof(string))
                {
                    fd.SetValue(objt, obj, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null);
                }
                else if (fd.FieldType.IsClass)
                {
                    if (obj == null)
                        fd.SetValue(objt, obj, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null);
                    else
                    {
                        object vb = fd.GetValue(objt);
                        if (vb == null)
                        {
                            vb = System.Activator.CreateInstance(fd.FieldType, new object[] { });
                            fd.SetValue(objt, vb, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null);
                        }
                        m_mthTreeCopy(obj, vb);
                    }
                }
            }
        }
		public static void Copyto(object objs,object objt)
		{
			IEnumerator enumer = null;
			IEnumerator enumerT = null;
			if(objs is IEnumerator)
			{
				enumer = (IEnumerator)objs;
				enumerT = (IEnumerator)objt;
			} 
			else if (objs is IEnumerable)
			{
				enumer = ((IEnumerable)objs).GetEnumerator();
				enumerT = ((IEnumerable)objt).GetEnumerator();
			}
			if(enumer != null)
			{
				enumer.Reset();
				enumerT.Reset();
				while(enumer.MoveNext())
				{
					enumerT.MoveNext();

					Copyto(enumer.Current,enumerT.Current);
				}
			}
			else
			{
				Type typeS = objs.GetType();
				Type typeD = objt.GetType();
				if(typeS == typeD)
				{
					System.Reflection.FieldInfo[] objFields = typeS.GetFields();
					foreach(System.Reflection.FieldInfo fd in objFields)
					{
						object obj = fd.GetValue(objs);
						if(fd.FieldType.IsValueType)
						{
							fd.SetValue(objt,obj);
						}					
						else
						{
							Copyto(fd.GetValue(objs),fd.GetValue(objt));
						}					
					}
				}
			}
		}

        /// <summary>
        /// ����Ա��ֵ�Ƚ��Ƿ����
        /// </summary>
        /// <param name="objTarget"></param>
        /// <returns></returns>
        public bool m_mthEquals(object objTarget)
        {
            if (objTarget == null)
                return false ;

            if (objTarget.GetType() != this.GetType())
                return false ;

            System.Reflection.FieldInfo[] objFields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (System.Reflection.FieldInfo fd in objFields)
            {
                object vs = fd.GetValue(this);
                object vt = fd.GetValue(objTarget);
                if(vs != vt)
                    return false ;
            }
            return true;
        }
	}
    /// <summary>
    /// �����˿�ֵ��ת�����ת������ int.MinValue, double.NaN ...
    /// </summary>
    public static class DBAssist
    {
        #region ��ֵ����
        public const int NullInt = int.MinValue;
        public const long NullLong = long.MinValue;
        public const double NullDouble = double.MinValue;
        public const float NullFloat = float.MinValue;
        public const decimal NullDecimal = decimal.MinValue;
        public static readonly DateTime NullDateTime = DateTime.MinValue;        
        #endregion

        #region �ж��Ƿ��ǿ�ֵ
        public static bool IsNull(object var)
        {
            return false;
        }
        public static bool IsNull(int var)
        {
            return var == DBAssist.NullInt;
        }
        public static bool IsNull(long var)
        {
            return var == DBAssist.NullLong;
        }
        public static bool IsNull(double var)
        {
            return var == DBAssist.NullDouble;
        }
        public static bool IsNull(float var)
        {
            return var == DBAssist.NullFloat;
        }
        public static bool IsNull(decimal var)
        {
            return var == DBAssist.NullDecimal;
        }
        public static bool IsNull(DateTime var)
        {
            return var == DBAssist.NullDateTime;
        }
        #endregion

        #region ���ݿ������ֵת���� ValueObject ��ֵ

        public static int ToInt32(object var)
        {
            if (var != System.DBNull.Value)
            {
                try
                {
                    return int.Parse(var.ToString().Trim());
                }
                catch { }
            }
            return DBAssist.NullInt;
        }

        public static long ToLong(object var)
        {
            if (var != System.DBNull.Value)
            {
                try
                {
                    return long.Parse(var.ToString().Trim());
                }
                catch { }
            }
            return DBAssist.NullLong;
        }

        public static double ToDouble(object var)
        {
            if (var != System.DBNull.Value)
            {
                try
                {
                    return double.Parse(var.ToString().Trim());
                }
                catch { }
            }
            return DBAssist.NullDouble;
        }

        public static float ToFloat(object var)
        {
            if (var != System.DBNull.Value)
            {
                try
                {
                    return float.Parse(var.ToString().Trim());
                }
                catch { }
            }
            return DBAssist.NullFloat;
        }

        public static decimal ToDecimal(object var)
        {
            if (var != System.DBNull.Value)
            {
                try
                {
                    return decimal.Parse(var.ToString().Trim());
                }
                catch { }
            }
            return DBAssist.NullDecimal;
        }

        public static DateTime ToDateTime(object var)
        {
            if (var != System.DBNull.Value)
            {
                try
                {
                    return DateTime.Parse(var.ToString().Trim());
                }
                catch { }
            }
            return DBAssist.NullDateTime;
        }
        
        #endregion

        #region ValueObject ��ֵת�� string 
        #region int ToString
        public static string ToString(int var)
        {
            if (var == DBAssist.NullInt)
                return string.Empty;
            return var.ToString();
        }
        public static string ToString(int var, IFormatProvider provider)
        {
            if (var == DBAssist.NullInt)
                return string.Empty;
            return var.ToString(provider);
        }
        public static string ToString(int var, string format)
        {
            if (var == DBAssist.NullInt)
                return string.Empty;
            return var.ToString(format);
        }
        public static string ToString(int var, string format, IFormatProvider provider)
        {
            if (var == DBAssist.NullInt)
                return string.Empty;
            return var.ToString(format, provider);
        }
        #endregion

        #region long ToString
        public static string ToString(long var)
        {
            if (var == DBAssist.NullLong)
                return string.Empty;
            return var.ToString();
        }
        public static string ToString(long var, IFormatProvider provider)
        {
            if (var == DBAssist.NullLong)
                return string.Empty;
            return var.ToString(provider);
        }
        public static string ToString(long var, string format)
        {
            if (var == DBAssist.NullLong)
                return string.Empty;
            return var.ToString(format);
        }
        public static string ToString(long var, string format, IFormatProvider provider)
        {
            if (var == DBAssist.NullLong)
                return string.Empty;
            return var.ToString(format, provider);
        }
        #endregion

        #region double ToString
        public static string ToString(double var)
        {
            if (var == DBAssist.NullDouble)
                return string.Empty;
            return var.ToString();
        }
        public static string ToString(double var, IFormatProvider provider)
        {
            if (var == DBAssist.NullDouble)
                return string.Empty;
            return var.ToString(provider);
        }
        public static string ToString(double var, string format)
        {
            if (var == DBAssist.NullDouble)
                return string.Empty;
            return var.ToString(format);
        }
        public static string ToString(double var, string format, IFormatProvider provider)
        {
            if (var == DBAssist.NullDouble)
                return string.Empty;
            return var.ToString(format, provider);
        }
        #endregion

        #region float ToString
        public static string ToString(float var)
        {
            if (var == DBAssist.NullFloat)
                return string.Empty;
            return var.ToString();
        }
        public static string ToString(float var, IFormatProvider provider)
        {
            if (var == DBAssist.NullFloat)
                return string.Empty;
            return var.ToString(provider);
        }
        public static string ToString(float var, string format)
        {
            if (var == DBAssist.NullFloat)
                return string.Empty;
            return var.ToString(format);
        }
        public static string ToString(float var, string format, IFormatProvider provider)
        {
            if (var == DBAssist.NullFloat)
                return string.Empty;
            return var.ToString(format, provider);
        }
        #endregion

        #region decimal ToString
        public static string ToString(decimal var)
        {
            if (var == DBAssist.NullDecimal)
                return string.Empty;
            return var.ToString();
        }
        public static string ToString(decimal var, IFormatProvider provider)
        {
            if (var == DBAssist.NullDecimal)
                return string.Empty;
            return var.ToString(provider);
        }
        public static string ToString(decimal var, string format)
        {
            if (var == DBAssist.NullDecimal)
                return string.Empty;
            return var.ToString(format);
        }
        public static string ToString(decimal var, string format, IFormatProvider provider)
        {
            if (var == DBAssist.NullDecimal)
                return string.Empty;
            return var.ToString(format, provider);
        }
        #endregion

        #region dateTime ToString
        public static string ToString(DateTime var)
        {
            if (var == DBAssist.NullDateTime)
                return string.Empty;
            return var.ToString();
        }
        public static string ToString(DateTime var, IFormatProvider provider)
        {
            if (var == DBAssist.NullDateTime)
                return string.Empty;
            return var.ToString(provider);
        }
        public static string ToString(DateTime var, string format)
        {
            if (var == DBAssist.NullDateTime)
                return string.Empty;
            return var.ToString(format);
        }
        public static string ToString(DateTime var, string format, IFormatProvider provider)
        {
            if (var == DBAssist.NullDateTime)
                return string.Empty;
            return var.ToString(format, provider);
        }
        #endregion
        #endregion

        #region ValueObject ��ֵ���ʱת�ɶ�Ӧ�Ĳ���ֵ
        public static object ToObject(object var)
        {
            if (DBAssist.IsNull(var))
            {
                return null;
            }
            return var;
        }
        public static object ToObject(int var)
        {
            if (DBAssist.IsNull(var))
            {
                return null;
            }
            return var;
        }
        public static object ToObject(long var)
        {
            if (DBAssist.IsNull(var))
            {
                return null;
            }
            return var;
        }
        public static object ToObject(double var)
        {
            if (DBAssist.IsNull(var))
            {
                return null;
            }
            return var;
        }
        public static object ToObject(float var)
        {
            if (DBAssist.IsNull(var))
            {
                return null;
            }
            return var;
        }
        public static object ToObject(decimal var)
        {
            if (DBAssist.IsNull(var))
            {
                return null;
            }
            return var;
        }
        public static object ToObject(DateTime var)
        {
            if (DBAssist.IsNull(var))
            {
                return null;
            }
            return var;
        }
        #endregion
    }
}
