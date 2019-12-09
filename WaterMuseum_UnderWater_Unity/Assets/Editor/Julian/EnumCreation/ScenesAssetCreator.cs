using System.IO;
using UnityEditor;
using UnityEngine;

public class ScenesAssetCreator : MonoBehaviour
{

    private const string m_sceneFileName = "Scenes";


    [MenuItem("EnumCreation/CreateScenesFile")]
    public static void CreateScenesEnumFile()
    {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];

        Debug.Log("-----Scene Enum Start-----");
        Debug.Log("");
        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            Debug.Log("Scene: " + scenes[i] + " Added.");
        }
        Debug.Log("");
        Debug.Log("-----Scene Enum Eind-----");


        EnumWriter.WriteToEnum(m_sceneFileName, scenes);
    }
}