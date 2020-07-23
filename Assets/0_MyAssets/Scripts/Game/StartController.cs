using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartController : MonoBehaviour
{
    [SerializeField] Transform cubesTfm;
    [SerializeField] Transform movePosTfm;
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
        if (Variables.gameState != GameState.FloorMove) return;
        if (Input.GetMouseButtonDown(0))
        {

            if (IsTap(out Vector3 tapPos))
            {

                Variables.gameState = GameState.BallFall;
                cubesTfm.DOMove(movePosTfm.position, 0.5f);
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
