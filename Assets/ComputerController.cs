using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    private GameObject _ball;
    private float _objectHeight;


    private Vector2 _previousFrameBallPosition;
    
    [SerializeField] private float _speed = 10f;
    void Start()
    {
        _ball = GameObject.Find("Ball");
        _objectHeight = gameObject.GetComponent<Renderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        
        Vector2 movingTowards;

        //If the ball is moving away from the computer then move to the middle.
        if (_ball.transform.position.x <= _previousFrameBallPosition.x) //Ball is moving left;
        {
            movingTowards = Vector2.MoveTowards(position,new Vector2(position.x, 0), _speed * Time.deltaTime);
        }
        else
        {
            movingTowards = Vector2.MoveTowards(position, _ball.transform.position, _speed * Time.deltaTime);
        }
        float clampedY = Mathf.Clamp(movingTowards.y, -Camera.main.orthographicSize + (_objectHeight / 2), Camera.main.orthographicSize - (_objectHeight)/2);

        position= new Vector2(position.x, clampedY);
       
        transform.position = position;
        _previousFrameBallPosition = _ball.transform.position;
    }
}
