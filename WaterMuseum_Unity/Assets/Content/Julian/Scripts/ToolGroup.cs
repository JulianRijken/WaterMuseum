using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolGroup : MonoBehaviour
{
    [SerializeField] private Toggle[] toggels;

    private void Awake()
    {
        NotificationCenter.OnToolSwitch += HandleToolSwitch;
    }

    private void OnDestroy()
    {
        NotificationCenter.OnToolSwitch -= HandleToolSwitch;
    }

    public void ChangeTool(int tool)
    {
        NotificationCenter.FireToolSwitch((Tool)tool);
    }

    public void HandleToolSwitch(Tool tool)
    {
        toggels[(int)tool].SetIsOnWithoutNotify(true);
    }
}
