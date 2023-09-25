using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Dependencies
    [SerializeField] private Transform _toFollow;
    private Transform _transform;

    // Values
    private float _smoothFactor = 0.25f;
    private float _boundX = 1f;
    private float _boundY = 1.8f;
    [SerializeField] private float _minClampX, _maxClampX;
    [SerializeField] private float _minClampY, _maxClampY;

    private void Start()
    {
        _transform = this.transform;
    }

    private void FixedUpdate()
    {
        if (_toFollow)
            SmoothFollow();
    }

    private void SmoothFollow()
    {
        Vector3 distance = _toFollow.position - _transform.position;
        Vector3 delta = Vector3.zero;

        // X Axis
        if (distance.x > _boundX || distance.x < -_boundX)
        {
            if (_transform.position.x < _toFollow.position.x)
                delta.x = distance.x - _boundX;
            else if (_transform.position.x > _toFollow.position.x)
                delta.x = distance.x + _boundX;
        }

        // Y Axis
        if (distance.y > _boundY || distance.y < -_boundY)
        {
            if (_transform.position.y < _toFollow.position.y)
                delta.y = distance.y - _boundY;
            else if (_transform.position.y > _toFollow.position.y)
                delta.y = distance.y + _boundY;
        }

        Vector3 desiredBoundsOffset = new Vector3(delta.x, delta.y, 0);
        Vector3 desiredPosition = Vector3.Lerp(_transform.position, _transform.position + desiredBoundsOffset, _smoothFactor);
        _transform.position = new Vector3
            (
                Mathf.Clamp(desiredPosition.x, _minClampX, _maxClampX),
                Mathf.Clamp(desiredPosition.y, _minClampY, _maxClampY),
                -10
            );
    }
}
