using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    private GameObject _ball;
    private float _objectHeight;
    
    [SerializeField] private float _speed = 20f;
    void Start()
    {
        _ball = GameObject.Find("Ball");
        _objectHeight = gameObject.GetComponent<Renderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        var movingPosition = Vector2.MoveTowards(position, _ball.transform.position, _speed * Time.deltaTime);
        float clampedY = Mathf.Clamp(movingPosition.y, -Camera.main.orthographicSize + (_objectHeight / 2), Camera.main.orthographicSize - (_objectHeight)/2);

        position= new Vector2(position.x, movingPosition.y);
       
        transform.position = position;
    }
}
