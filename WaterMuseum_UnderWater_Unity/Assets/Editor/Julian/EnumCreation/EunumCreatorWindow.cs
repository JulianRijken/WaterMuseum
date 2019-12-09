using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class EunumCreatorWindow : EditorWindow
{

    private string m_filePath = "Assets/";
    private string m_fileName = "TestMyEnum";
    private const string m_extension = ".cs";


    [SerializeField] private string[]  m_names;
    private SerializedObject so;
    private SerializedProperty stringsProp;


    [MenuItem("EnumCreation/EunumCreatorWindow")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        EunumCreatorWindow window = (EunumCreatorWindow)EditorWindow.GetWindow(typeof(EunumCreatorWindow));
        window.Show();
    }

    private void Awake()
    {
        so = new SerializedObject(this);
        stringsProp = so.FindProperty("m_names");
    }

    private void OnGUI()
    {

        if (stringsProp != null)
        {
            EditorGUILayout.PropertyField(stringsProp, true);
            so.ApplyModifiedProperties();
        }

        m_filePath = EditorGUILayout.TextField("Path", m_filePath);
        m_fileName = EditorGUILayout.TextField("Name", m_fileName);

        if (GUILayout.Button("Save"))
        {
            EnumWriter.WriteToEnum(m_fileName, m_names, m_filePath);
        }
    }
}