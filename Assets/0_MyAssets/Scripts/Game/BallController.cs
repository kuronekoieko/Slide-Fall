using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Transform deadLine;

    void Start()
    {
        deadLine = GameObject.FindGameObjectWithTag("DeadLine").transform;
    }

    void Update()
    {
        if (transform.position.y < deadLine.position.y)
        {
            if (Variables.screenState != ScreenState.Game) return;
            Variables.screenState = ScreenState.Failed;
            Debug.Log(Variables.screenState);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Goal")) return;
        if (Variables.screenState != ScreenState.Game) return;
        Variables.screenState = ScreenState.Clear;
        Debug.Log(Variables.screenState);
        CameraController.i.PlayConfetti();
    }
}
