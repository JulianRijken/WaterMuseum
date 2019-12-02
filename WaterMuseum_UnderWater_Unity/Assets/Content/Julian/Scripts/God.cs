using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class God : MonoBehaviour
{
    [SerializeField] string m_rockName;
    [SerializeField] private Vector2 m_treeHeightOffest;
    [SerializeField] private Vector3 m_placeOffset;
    [SerializeField] private int m_maxStones;
    [SerializeField] private LayerMask m_terrainLayer;
    [SerializeField] private LayerMask m_removeLayer;

    private Camera m_mainCamera;
    private Tool m_selectedTool;

    private void Awake()
    {
        m_mainCamera = Camera.main;

        NotificationCenter.OnToolSwitch += HandleToolSwitch;
    }
    private void OnDestroy()
    {
        NotificationCenter.OnToolSwitch -= HandleToolSwitch;
    }

    private void HandleToolSwitch(Tool newTool)
    {
        m_selectedTool = newTool;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (m_selectedTool == Tool.place)
                    {
                        if (Stats.GetSheet().m_rockCount < m_maxStones)
                        {
                            Ray ray = m_mainCamera.ScreenPointToRay(touch.position);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit,m_terrainLayer))
                            {
                                Vector3 spawnPoint = hit.point;
                                spawnPoint.y += Random.Range(m_treeHeightOffest.x, m_treeHeightOffest.y);
                                ObjectPooler.SpawnObject(m_rockName, spawnPoint + m_placeOffset, Quaternion.identity, true);
                                Stats.GetSheet().m_rockCount++;
                            }
                        }
                    }
                    else
                    {
                        Ray ray = m_mainCamera.ScreenPointToRay(touch.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, m_removeLayer))
                        {
                            Rock rock = hit.collider.GetComponent<Rock>();
                            if (rock != null)
                            {
                                rock.gameObject.SetActive(false);
                                Stats.GetSheet().m_rockCount--;
                            }
                        }
                    }
                        
                }
            }
        }
    }


}