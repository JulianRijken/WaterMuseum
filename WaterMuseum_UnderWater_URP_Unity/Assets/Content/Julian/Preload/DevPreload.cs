using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevPreload : MonoBehaviour
{
    [SerializeField] private string appGameObjectName = "AppPreload";

    public static int currentSceneIndex;

    private void Awake()
    {
        GameObject check = GameObject.Find(appGameObjectName);
        if (check == null)
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }

    }
}
