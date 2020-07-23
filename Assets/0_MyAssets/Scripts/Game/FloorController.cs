using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] Collider col;
    Vector3 preTapPos;
    bool isDragging;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsTap(out Vector3 tapPos))
            {
                preTapPos = tapPos;
                isDragging = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (!isDragging) return;
            Vector3 tapPos = WorldPosOnTap();
            Vector3 dir = tapPos - preTapPos;
            dir.x = 0;
            dir.z = 0;
            transform.Translate(dir);
            preTapPos = tapPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    bool IsTap(out Vector3 tapPos)
    {
        tapPos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity);
        if (!isHit) return false;
        if (hit.collider != col) return false;
        tapPos = hit.point;
        return true;
    }


    Vector3 WorldPosOnTap()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
