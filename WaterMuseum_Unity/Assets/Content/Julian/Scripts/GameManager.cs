using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] string m_treeName;
    private Tool m_tool;

    private void Awake()
    {
        SetTool(Tool.tree);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            switch (m_tool)
            {
                case Tool.tree:
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        PlaceTree(m_treeName, touch.position, Quaternion.identity);
                    }
                    break;
                case Tool.water:
                    break;
                case Tool.shovel:
                    break;
                default:
                    break;
            }
         
        }
    }

    public void SetTool(Tool tool)
    {
        m_tool = tool;
    }

    public Tool GetTool()
    {
        return m_tool;
    }

    private void PlaceTree(string treeName,Vector3 position,Quaternion rotation)
    {
        ObjectPooler.SpawnObject(treeName, position, rotation, true);

    }
}

public enum Tool
{
    tree,
    water,
    shovel
}
