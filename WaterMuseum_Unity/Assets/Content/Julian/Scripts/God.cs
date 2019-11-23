using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    [SerializeField] string m_treeName;
    [SerializeField] private Vector2 m_treeHeightOffest;
    private Tool m_tool;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
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
                        Ray ray = mainCamera.ScreenPointToRay(touch.position);
                        RaycastHit hit;
                        if(Physics.Raycast(ray,out hit))
                        {
                            Vector3 spawnPoint = hit.point;
                            spawnPoint.y += Random.Range(m_treeHeightOffest.x, m_treeHeightOffest.y);
                            ObjectPooler.SpawnObject(m_treeName, spawnPoint, Quaternion.identity, true);
                        }

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
}

public enum Tool
{
    tree,
    water,
    shovel
}
