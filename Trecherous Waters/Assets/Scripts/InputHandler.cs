using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    ISetWaypoint iSetWaypoint;

    public LayerMask navigableTerrain;

    void Start()
    {
        iSetWaypoint = GetComponent<ISetWaypoint>();
    }

    private void OnSetWaypoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, navigableTerrain))
        {
            iSetWaypoint?.SetWaypoint(hit.point);
        }
    }
}
