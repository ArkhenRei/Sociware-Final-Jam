using System;
using System.Collections;
using System.Collections.Generic;
using ArkhenRei;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Controller))]
public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;
    [SerializeField, Range(0.05f, 0.5f)] private float _wallStickTime = 0.25f;

    private Vector2 _direction;
    private Vector2 _desiredVelocity;
    private Vector2 _velocity;
    private Rigidbody2D _body;
    private CollisionDataRetriever _collisionDataRetriever;
    private WallInteractor _wallInteractor;
    private Controller _controller;

    private float _maxSpeedChange, _acceleration, _wallStickCounter;
    private bool _onGround;

    public static bool fight;
    public Animator anim;
   
    
    // Start is called before the first frame update
    void Awake()
    {
        fight = false;
        _body = GetComponent<Rigidbody2D>();
        _collisionDataRetriever = GetComponent<CollisionDataRetriever>();
        _controller = GetComponent<Controller>();
        _wallInteractor = GetComponent<WallInteractor>();
        anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _direction.x = input.RetrieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _collisionDataRetriever.GetFriction(), 0f);
        if (Input.GetKeyDown(KeyCode.Space)&& !fight)
        {
            anim.SetTrigger("isJumping");
        }
    }

    private void FixedUpdate()
    {
        _onGround = _collisionDataRetriever.GetOnGround();
        _velocity = _body.velocity;

        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        #region Wall Stick

        if (_collisionDataRetriever.OnWall && !_collisionDataRetriever.OnGround && !_wallInteractor.WallJumping)
        {
            if (_wallStickCounter > 0)
            {
                _velocity.x = 0;

                if (_controller.input.RetrieveMoveInput() == _collisionDataRetriever.ContactNormal.x)
                {
                    _wallStickCounter -= Time.deltaTime;
                }
                else
                {
                    _wallStickCounter = _wallStickTime;
                }
            }
            else
            {
                _wallStickCounter = _wallStickTime;
            }
        }

        #endregion

        _body.velocity = _velocity;
        if (_body.velocity.x != 0 && !fight)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if (_body.velocity.x < 0 && !fight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_body.velocity.x > 0 && !fight )
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


    }
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            GameObject.FindWithTag("SceneCT").GetComponent<SceneCt>().FinishLevel();
        }
        else if(collision.CompareTag("Enemy"))
        {
            fight = true;
               
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }




}
