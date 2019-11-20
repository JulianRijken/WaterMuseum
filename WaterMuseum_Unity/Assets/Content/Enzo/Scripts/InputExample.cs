using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputExample : MonoBehaviour
{
    private Controls m_controls;
    private void Start()
    {
        m_controls = new Controls();
       // m_controls.Player.Press += HandlePress;
    }


    private void OnEnable()
    {
        m_controls.Enable();
    }

    private void OnDisable()
    {
        m_controls.Disable();
    }

    public void HandlePress(InputAction.CallbackContext context)
    {
        Debug.Log("Press");
    }
}
