using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    ISetWaypoint iSetWaypoint;
    IFire iFire;

    public LayerMask navigableTerrain;

    void Start()
    {
        iSetWaypoint = GetComponent<ISetWaypoint>();
        iFire = GetComponent<IFire>();
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

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            iFire?.Fire();
        }
    }
}
