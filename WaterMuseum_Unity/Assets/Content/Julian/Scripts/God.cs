using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class God : MonoBehaviour
{
    [SerializeField] string m_treeName;
    [SerializeField] private Vector2 m_treeHeightOffest;
    private Tool m_tool;
    private Camera m_mainCamera;

    private void Awake()
    {
        m_mainCamera = Camera.main;
        NotificationCenter.FireToolSwitch(Tool.tree);
        NotificationCenter.OnToolSwitch += HandleToolSwitch;
    }

    private void OnDestroy()
    {
        NotificationCenter.OnToolSwitch -= HandleToolSwitch;
    }

    private void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                switch (m_tool)
                {
                    case Tool.tree:

                        if (touch.phase == TouchPhase.Began)
                        {
                            Ray ray = m_mainCamera.ScreenPointToRay(touch.position);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit))
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
                }
        }
    }

    public void HandleToolSwitch(Tool tool)
    {
        m_tool = tool;
    }
}

public enum Tool
{
    tree = 0,
    water = 1,
    shovel = 2
}
