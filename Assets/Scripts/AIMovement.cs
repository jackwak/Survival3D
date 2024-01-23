using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float _moveSpeed = 0.2f;
    private float _walkTime;
    private float _walkCounter;
    private float _waitTime;
    private float _waitCounter;

    private int _walkDirection;
    private bool _isWalking;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _walkTime = Random.Range(3, 6);
        _waitTime = Random.Range(5, 7);


        _waitCounter = _waitTime;
        _walkCounter = _walkTime;

        ChooseDirection();
    }

    private void Update()
    {
        if (_isWalking)
        {
            animator.SetBool("isRunning", true);

            _walkCounter -= Time.deltaTime;

            switch (_walkDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += transform.forward * _moveSpeed * Time.deltaTime;
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                    transform.position += transform.forward * _moveSpeed * Time.deltaTime;
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                    transform.position += transform.forward * _moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0f, 270f, 0f);
                    transform.position += transform.forward * _moveSpeed * Time.deltaTime;
                    break;
            }

            if (_walkCounter <= 0)
            {
                _isWalking = false;
                animator.SetBool("isRunning", false);
                _waitCounter = _waitTime;
            }
        }
        else
        {
            _waitCounter -= Time.deltaTime;

            if (_waitCounter <= 0)
            {
                ChooseDirection();
                _walkCounter = _walkTime;
                _isWalking = true;
            }
        }
    }

    private void ChooseDirection()
    {
        _walkDirection = Random.Range(0, 4);
    }
}
