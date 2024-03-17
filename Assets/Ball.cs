using System;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public enum BallState
{
    Idle,
    Moving
}

public class Ball : MonoBehaviour
{
    private BallState _ballState = BallState.Idle;
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _speed = 5f;

    [SerializeField] private TextMeshProUGUI _leftScore;
    [SerializeField] private TextMeshProUGUI _rightScore;

    private float? _previousWinner = null;
    
    

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();

        _leftScore.text = "0";
        _rightScore.text = "0";
        
        _previousWinner = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    void Update()
    {
        if (_ballState == BallState.Idle && Input.GetKey(KeyCode.Space))
        {
            _speed = 5f;
            _rigidbody.WakeUp();
            _ballState = BallState.Moving;
            _rigidbody.velocity = new Vector2(-_previousWinner.Value, 0) * _speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnPaddleColliison(other);

        if (other.gameObject.CompareTag("Finish"))
        {
            if (transform.position.x > 0)
            {
                _leftScore.text = (Convert.ToInt32(_leftScore.text) + 1).ToString();
                _previousWinner = -1;
            }
            else
            {
                _rightScore.text = (Convert.ToInt32(_rightScore.text) + 1).ToString();
                _previousWinner = 1;
            }

            _ballState = BallState.Idle;
            _rigidbody.Sleep();
            transform.position = new Vector3(0, 0, 0);
        }
        
    }

    private void OnPaddleColliison(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            if (transform.position.y > other.transform.position.y)
            {
                _rigidbody.velocity = new Vector2(Mathf.Clamp(-transform.position.x, -1, 1), 1) * _speed;
            }
            else if(_rigidbody.position.y < other.transform.position.y)
            {
                _rigidbody.velocity = new Vector2(Mathf.Clamp(-transform.position.x, -1, 1), -1) * _speed;
            }
            else
            {
                _rigidbody.velocity = new Vector2(Mathf.Clamp(-transform.position.x, -1, 1),0) * _speed;
                
            }
            _speed += .25f;

        }
    }

}
