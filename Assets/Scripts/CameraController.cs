using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float _camOffset = -10f;

    [SerializeField] private Transform _toFollow;
    [SerializeField] private float _followSpeed = 3f;
    [SerializeField] private float _toFollowOffset = 4f;

    private Vector3 _movePos = new Vector3();


    private void Start()
    {
        _movePos = transform.position;
    }

    private void LateUpdate()
    {
        _movePos.z = _camOffset;
        _movePos.x = Mathf.Lerp(transform.position.x, _toFollow.position.x + _toFollowOffset, Time.deltaTime * _followSpeed);
        transform.position = _movePos;
    }
}
