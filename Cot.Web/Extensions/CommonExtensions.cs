﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Cot.Web.Extensions
{
    public static class CommonExtensions
    {
        public static string ToCleanString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd | hh:mm:ss tt");
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            var Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (var item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
