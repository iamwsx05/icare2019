using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Reflection;
using System.Drawing;

namespace Common.Controls.Emr
{
    internal class ExportService
    {
        public static void Serialize(string formID, string filepath, List<clsCustomFormControlArrangement> listControlsData)
        {
            DataSet ds = new DataSet("自定义表单");
            DataTable dt = ChangeEntity2Table(listControlsData);
            dt.TableName = "表单内容";
            ds.Tables.Add(dt);
            ds.WriteXml(filepath, XmlWriteMode.WriteSchema);
        }

        public static List<clsCustomFormControlArrangement> Deserialize(string filepath)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(filepath, XmlReadMode.ReadSchema);

            List<clsCustomFormControlArrangement> list = new List<clsCustomFormControlArrangement>();

            ChangeTable2Entity(ds.Tables[0], ref list);

            return list;
        }

        public static DataTable ChangeEntity2Table<T>(List<T> list)
        {
            DataTable dt = new DataTable();
            Type type = typeof(T);
            PropertyInfo[] propertys = type.GetProperties();
            ColorConverter cc = new ColorConverter();

            for (int i = 0; i < propertys.Length; i++)
            {
                //取得属性
                PropertyInfo property = propertys[i];

                Type dataType = property.PropertyType;

                if (dataType.IsGenericType)
                {
                    if (dataType.GetGenericTypeDefinition() == typeof(System.Nullable<>))
                    {
                        dataType = dataType.GetGenericArguments()[0];
                    }
                }
                else
                {
                    if (dataType == typeof(Color))
                    {
                        dataType = typeof(string);
                    }
                }

                dt.Columns.Add(property.Name, dataType);
            }

            for (int i = 0; i < list.Count; i++)
            {
                DataRow dr = dt.NewRow();

                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    object obj = propertys[j].GetValue(list[i], null);

                    object objVal = obj;

                    if (obj != null && propertys[j].PropertyType.FullName == "System.Drawing.Color")
                    {
                        objVal = cc.ConvertToString(obj);
                    }
                    else
                    {
                        //if (obj != null)
                        //{
                        //    strVal = obj.ToString();
                        //}
                        objVal = obj;
                    }

                    try
                    {
                        dr[j] = objVal;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static void ChangeTable2Entity<T>(DataTable dt, ref List<T> list)
        {
            Type type = typeof(T);
            PropertyInfo[] propertys = type.GetProperties();
            ColorConverter cc = new ColorConverter();

            foreach (DataRow dr in dt.Rows)
            {
                T entity = (T)type.GetConstructor(new Type[0]).Invoke(new object[0]);
                for (int i = 0; i < propertys.Length; i++)
                {
                    //取得属性
                    PropertyInfo property = propertys[i];
                    object obj = dr[property.Name];


                    switch (property.PropertyType.FullName)
                    {
                        case "System.Drawing.Color":
                            {
                                if (obj != null && obj != DBNull.Value && obj.ToString() != string.Empty)
                                {
                                    obj = (Color)cc.ConvertFromString(obj.ToString());
                                }
                                else
                                {
                                    obj = Color.Transparent;
                                }
                                break;
                            }
                        default:
                            {
                                //obj = strVal;
                                break;
                            }
                    }
                    if (obj != null)
                    {
                        try
                        {
                            //当前字段是否为泛型
                            if (property.PropertyType.IsGenericType)
                            {
                                //当前字段为可空类型
                                if (property.PropertyType.GetGenericTypeDefinition() == typeof(System.Nullable<>))
                                {

                                    if (property.PropertyType.GetGenericArguments()[0] == typeof(decimal))
                                    {
                                        property.SetValue(entity, weCare.Core.Utils.Function.Dec(obj), null);
                                    }
                                    else
                                    {
                                        property.SetValue(entity, obj, null);
                                    }
                                }
                            }
                            else
                            {
                                if (property.PropertyType == typeof(decimal))
                                {
                                    property.SetValue(entity, weCare.Core.Utils.Function.Dec(obj), null);
                                }
                                else if (property.PropertyType == typeof(string))
                                {
                                    property.SetValue(entity, obj.ToString(), null);
                                }
                                else
                                {
                                    property.SetValue(entity, obj, null);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                }
                list.Add(entity);
            }
        }
    }
}
