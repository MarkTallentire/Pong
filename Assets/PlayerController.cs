using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private float objectHeight;

    void Start()
    {
        objectHeight = gameObject.GetComponent<Renderer>().bounds.size.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            var newPosition = transform.position + Vector3.down * (moveSpeed * Time.deltaTime);
            ClampMovement(newPosition);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            var newPosition = transform.position + Vector3.up * (moveSpeed * Time.deltaTime);
            ClampMovement(newPosition);
        }
        
    }

    void ClampMovement(Vector3 newPosition)
    {
        float clampedY = Mathf.Clamp(newPosition.y, -Camera.main.orthographicSize + (objectHeight / 2), Camera.main.orthographicSize - (objectHeight)/2);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
}
