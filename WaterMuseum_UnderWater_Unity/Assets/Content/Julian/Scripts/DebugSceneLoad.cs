using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneLoad : MonoBehaviour
{
    private void Awake()
    {
        if (DevPreload.currentSceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(DevPreload.currentSceneIndex);
        }
        Destroy(this);
    }
}
