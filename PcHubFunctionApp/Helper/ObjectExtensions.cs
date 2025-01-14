﻿using System.Collections.Generic;
using System.IO;

namespace PcHubFunctionApp.Helper;

public static class ObjectExtensions
{
    public static string Dump(this object obj)
    {
        var writer = new StringWriter();
        if (obj == null)
        {
            writer.WriteLine("Object is null");
            return writer.ToString();
        }

        writer.Write("Hash: ");
        writer.Write(obj.GetHashCode());
        writer.Write("\tType: ");
        writer.WriteLine(obj.GetType());

        var props = GetProperties(obj);

        if (props.Count > 0) writer.WriteLine("-------------------------");

        foreach (var prop in props)
        {
            writer.Write(prop.Key);
            writer.Write("=");
            writer.Write(prop.Value);
            writer.Write("\t");
        }

        return writer.ToString();
    }

    private static Dictionary<string, string> GetProperties(object obj)
    {
        var props = new Dictionary<string, string>();
        if (obj == null)
            return props;

        var type = obj.GetType();
        foreach (var prop in type.GetProperties())
        {
            var val = prop.GetValue(obj, new object[] { });
            var valStr = val == null ? "" : val.ToString();
            props.Add(prop.Name, valStr);
        }

        return props;
    }
}