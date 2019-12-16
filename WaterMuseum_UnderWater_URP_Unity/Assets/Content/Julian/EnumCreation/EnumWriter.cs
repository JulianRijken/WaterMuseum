using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EnumWriter : MonoBehaviour
{
    private const string m_extension = ".cs";

    public static string WriteToEnum<T>(string name, ICollection<T> data, string path = "Assets/Enums/")
    {
        if (data != null && data.Count > 0)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (StreamWriter file = File.CreateText(path + name + m_extension))
            {
                file.WriteLine("public enum " + name + " \n{");

                int i = 0;
                foreach (var line in data)
                {
                    string lineRep = line.ToString().Replace(" ", string.Empty);
                    if (!string.IsNullOrEmpty(lineRep))
                    {
                        file.WriteLine(string.Format("\t{0} = {1},", lineRep, i));
                        i++;
                    }
                }

                file.WriteLine("\n}");
            }

            AssetDatabase.ImportAsset(path + name + m_extension);
        }
        else
        {
            Debug.LogWarning(name + " Has no names");
        }

        return path;
    }
}

