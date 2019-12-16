using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class God : MonoBehaviour
{
    [SerializeField] string m_rockName;
    [SerializeField] private int m_maxStones;
    [SerializeField] private float m_placeDelay;
    [SerializeField] private Vector3 m_placeOffset;
    [SerializeField] private LayerMask m_terrainLayer;
    [SerializeField] private LayerMask m_removeLayer;

    private Camera m_mainCamera;
    private Tool m_selectedTool;
    private bool m_placeAllowed;

    private void Awake()
    {
        m_mainCamera = Camera.main;
        m_placeAllowed = true;

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

    private IEnumerator DelayRock()
    {
        m_placeAllowed = false;
        yield return new WaitForSeconds(m_placeDelay);
        m_placeAllowed = true;
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
                        if (Stats.Sheet.m_rockCount < m_maxStones)
                        {
                            Ray ray = m_mainCamera.ScreenPointToRay(touch.position);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit, m_terrainLayer))
                            {
                                if (m_placeAllowed)
                                {
                                    StartCoroutine(DelayRock());
                                    Vector3 spawnPoint = hit.point + m_placeOffset;
                                    ObjectPooler.SpawnObject(m_rockName, spawnPoint + m_placeOffset, Quaternion.identity, true);
                                    Stats.Sheet.m_rockCount++;
                                }
                            }
                        }
                    }
                    else
                    {
                        Ray ray = m_mainCamera.ScreenPointToRay(touch.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, m_removeLayer))
                        {
                            IRemovable removable = hit.collider.GetComponent<IRemovable>();
                            if (removable != null)
                            {
                                removable.OnRemove();
                            }
                        }
                    }
                        
                }
            }
        }
    }


}