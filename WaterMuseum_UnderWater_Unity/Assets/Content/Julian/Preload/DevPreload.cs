using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DevPreload : MonoBehaviour
{
    [SerializeField] private string appGameObjectName = "AppPreload";

    private void Awake()
    {
        GameObject check = GameObject.Find(appGameObjectName);
        if (check == null) UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        {
            Destroy(gameObject);
        }
    }
}
