using UnityEngine;

public class DDOL : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("DDOL: " + gameObject.name);
        Destroy(this);
    }
}
