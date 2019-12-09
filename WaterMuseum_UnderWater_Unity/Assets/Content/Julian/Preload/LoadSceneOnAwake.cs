using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnAwake : MonoBehaviour
{

    [SerializeField] private int m_sceneIndex = 0;

    private void Awake()
    {
        SceneManager.LoadScene(m_sceneIndex);
        Destroy(this);
    }
}
