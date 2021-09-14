using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace FileInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileinfos = FileInfo(args[0]);

            foreach (var fileinfo in fileinfos)
            {
                //out .csv
                Console.WriteLine("{0}, {1}", fileinfo.Item1, fileinfo.Item2);
            }

            IEnumerable<(string, object)> FileInfo(string fileName)
            {
                var VersionInfo = FileVersionInfo.GetVersionInfo(fileName);

                var PropertyInfos = GetProperties(VersionInfo.GetType());

                return PropertyInfos
                    .Select(p => (p.Name, GetValue(VersionInfo, p)));

                PropertyInfo[] GetProperties(Type type)
                {
                    return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
                }

                object GetValue(object obj, PropertyInfo info)
                {
                    return info.GetValue(obj, null);
                }
            }
        }
    }
}
