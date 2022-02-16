using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    public GameObject destinationAnchor;
    
    [SerializeField] private static float _angularSpeed = 230;
    [SerializeField] private float angle;
    public static float Speed = 1;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Vector3 _lastPosition;
    private bool _hasStarted;

    private bool IsWalking = false;
    private Vector3 _destination;
    private bool _isWalkingToDestination = false;
    private bool _isRotatingToFaceCamera = false;
    private bool _isRotating;
    private int _walkDirection;

    private AudioSource _audio;
    private bool _HasSpoken = false;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Test += StopSpeaking;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _lastPosition = transform.position;
        _navMeshAgent.updateRotation = false;
        _isRotating = false;

        _hasStarted = false;

        //InputManager.Test += StartWalking;
        
        transform.LookAt(Camera.main.transform);
        //Debug.Log("Camera at: " + Camera.main.transform.position);

        //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_HasSpoken)
        {
            _audio = GetComponent<AudioSource>();
            _audio.Play();
            
            _animator.SetTrigger("IsSpeaking");
            _HasSpoken = true;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            GameObject canvas = GameObject.Find("Canvas");
            GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
            PointerEventData ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(ped, results);
            //Debug.Log("Raycast hit Count: " + results.Count);
            if(results.Count <= 1)
            {
                _audio.Stop();

                Vector3 worldPosition = Vector3.zero;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitData;

                if (Physics.Raycast(ray, out hitData, 1000, LayerMask.NameToLayer("Ground")))
                {
                    worldPosition = hitData.point;
                }

                _destination = worldPosition;

                if (Vector3.Dot(transform.forward,
                    Quaternion.AngleAxis(90, Vector3.up) * (_destination - transform.position)) > 0)
                    _walkDirection = -1;
                else
                    _walkDirection = 1;

                StartWalking();

                _isRotating = true;
                _isWalkingToDestination = true;
            }
        }
        
        Vector3 destinationVector = _destination - transform.position;
        Vector3 lookVector = transform.forward;

        Vector3 destinationVectorY = new Vector3(destinationVector.x, 0, destinationVector.z);
        Vector3 lookVectorY = new Vector3(lookVector.x, 0, lookVector.z);
        if (Vector3.Angle(destinationVectorY, lookVectorY) <= 4)
        {
            _isRotating = false;
        }
        
        if (_isRotating)
            transform.Rotate(transform.up, _walkDirection * _angularSpeed * Time.deltaTime);


        if (IsWalking)
        {
            transform.position += transform.forward * Speed * Time.deltaTime;
        }

        Vector3 positionY = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 destinationY = new Vector3(_destination.x, 0, _destination.z);
        if ((positionY - destinationY).magnitude < 0.5  && _isWalkingToDestination)
        {
            _isWalkingToDestination = false;
            _isRotatingToFaceCamera = true;

            _destination = Camera.main.transform.position;
            
            if (Vector3.Dot(transform.forward,  Quaternion.AngleAxis(90, Vector3.up) * (_destination - transform.position)) > 0)
                _walkDirection = -1;
            else
                _walkDirection = 1;
            
            
            _isRotating = true;

            //_animator.SetBool("isMoving", false);
        }

        if (!_isRotating && _isRotatingToFaceCamera)
        {
            IsWalking = false;
            _isRotatingToFaceCamera = false;
            
            _animator.SetBool("isMoving", false);
        }
    }

    private void FixedUpdate()
    {
        // Vector3 currentPosition = transform.position;
        //
        // if ((_lastPosition - currentPosition).sqrMagnitude > 0.000005 && _hasStarted)
        // {
        //     //transform.rotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);
        // }
        // else
        //     _animator.SetBool("isMoving", false);
        //
        // _hasStarted = true;
        //
        // //Debug.Log((_lastPosition - currentPosition).sqrMagnitude);
        //
        // _lastPosition = currentPosition;
    }

    private void StartWalking()
    {
        IsWalking = true;
        
        _animator.SetBool("isMoving", true);
    }
    
    
    public void StartSpeaking()
    {
        _navMeshAgent.SetDestination(transform.position);

        _animator.SetTrigger("isSpeaking");
    }

    public void StopSpeaking()
    {
        _audio.Stop();
        
        _animator.SetTrigger("StopSpeaking");
    }
}