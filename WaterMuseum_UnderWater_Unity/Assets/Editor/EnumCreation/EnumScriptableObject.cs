using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new Enum", menuName = "new Enum", order = 1)]
public class EnumScriptableObject : ScriptableObject
{
    public string[] enumNames;
}
