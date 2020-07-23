using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] Collider col;
    Vector3 preTapPos;
    bool isDragging;
    Vector3 railVectorUp;
    void Start()
    {
        railVectorUp = GetRailTf(transform.position).up;
    }


    void Update()
    {
        if (Variables.gameState != GameState.FloorMove) return;
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
            Vector3 dragDiff = tapPos - preTapPos;
            dragDiff.z = 0;

            var moveDiff = Vector3.Project(dragDiff, railVectorUp);

            Transform railTfm = GetRailTf(transform.position + dragDiff);
            if (railTfm == null)
            {
                preTapPos = tapPos;
                return;
            }
            transform.Translate(moveDiff);
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

    Transform GetRailTf(Vector3 rayOrigin)
    {
        Ray ray = new Ray(rayOrigin, Vector3.forward);
        bool isHit = Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity);
        if (!isHit) return null;
        if (!hit.collider.CompareTag("Rail")) return null;
        return hit.collider.transform;
    }
}
