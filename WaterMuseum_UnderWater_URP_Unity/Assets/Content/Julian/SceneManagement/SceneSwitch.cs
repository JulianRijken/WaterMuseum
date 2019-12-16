using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneSwitch : MonoBehaviour
{
    private static SceneSwitch m_instance;
    private Animator m_animator;
    private bool m_closingAnimationDone = false;
    private bool m_swtiching = false;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            m_animator = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void LoadScene(int sceneIndex)
    {
        SceneSwitch instance = m_instance;

        if (!instance.m_swtiching)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            instance.StartCoroutine(instance.LoadScene(operation));
        }
    }

    private IEnumerator LoadScene(AsyncOperation operation)
    {
        operation.allowSceneActivation = false;
        m_closingAnimationDone = false;
        m_swtiching = true;

        m_animator.SetTrigger("LoadingScene");

        yield return new WaitUntil(() => m_closingAnimationDone == true);
        operation.allowSceneActivation = true;
        yield return new WaitUntil(() => operation.isDone == true);

        m_animator.SetTrigger("SceneLoaded");
    }

    private void ClosingAnimationDone()
    {
        m_closingAnimationDone = true;
    }

    private void OpeningAnimationDone()
    {
        m_swtiching = false;
    }

}
