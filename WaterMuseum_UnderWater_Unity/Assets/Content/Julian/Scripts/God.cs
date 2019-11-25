using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class God : MonoBehaviour
{
    [SerializeField] string m_treeName;
    [SerializeField] private Vector2 m_treeHeightOffest;

    private Camera m_mainCamera;

    private void Awake()
    {
        m_mainCamera = Camera.main;
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
                    Ray ray = m_mainCamera.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Vector3 spawnPoint = hit.point;
                        spawnPoint.y += Random.Range(m_treeHeightOffest.x, m_treeHeightOffest.y);
                        ObjectPooler.SpawnObject(m_treeName, spawnPoint, Quaternion.identity, true);
                    }
                }
            }
        }
    }


}