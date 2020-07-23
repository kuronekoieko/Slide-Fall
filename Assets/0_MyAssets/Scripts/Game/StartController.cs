using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    Collider col;
    void Awake()
    {
        col = GetComponent<Collider>();
    }

    void Start()
    {
        Variables.gameState = GameState.FloorMove;
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (IsTap(out Vector3 tapPos))
            {
                gameObject.SetActive(false);
                Variables.gameState = GameState.BallFall;
            }
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
}
