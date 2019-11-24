using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotificationCenter
{
    public static void FireToolSwitch(Tool tool)
    {
        OnToolSwitch?.Invoke(tool);
    }
    public delegate void ToolSwitchAction(Tool tool);
    public static event ToolSwitchAction OnToolSwitch;
}
