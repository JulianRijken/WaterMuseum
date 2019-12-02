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

    public static void FireTimeChange(float time)
    {
        OnTimeChange?.Invoke(time);
    }
    public delegate void TimeChangeAction(float time);
    public static event TimeChangeAction OnTimeChange;
}
