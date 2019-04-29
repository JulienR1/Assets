using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaLifter : MonoBehaviour
{
    public float moveSpeed;
    public Transform lowPoint;
    public Transform highPoint;
    private bool movingUp = true;

    private void Start()
    {
        movingUp = true;
        transform.position = lowPoint.position;
    }

    private void Update()
    {
        if (transform.position.y >= highPoint.position.y)
            movingUp = false;
        else if (transform.position.y <= lowPoint.position.y)
            movingUp = true;

        if (movingUp)
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        else
            transform.position -= Vector3.up * moveSpeed * Time.deltaTime;

        transform.eulerAngles += Vector3.up * moveSpeed * 25 * Time.deltaTime;
    }
}