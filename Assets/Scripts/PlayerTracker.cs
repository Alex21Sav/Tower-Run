using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private PlayerTower _playerTower;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector3 _OffsetPosition;
    [SerializeField] private Vector3 _OffsetRotation;

    private Vector3 _targetPosition;


    private void OnEnable()
    {
        _playerTower.HumanAdded += OnHumanAdded;
    }

    private void OnDisable()
    {
        _playerTower.HumanAdded -= OnHumanAdded;
    }
    private void Update()
    {        
        UpdatePosition();
        _OffsetPosition = Vector3.MoveTowards(_OffsetPosition, _targetPosition, _moveSpeed * Time.deltaTime);
    }
    private void UpdatePosition()
    {
        transform.position = _playerTower.transform.position;
        transform.localPosition = _OffsetPosition;
        Vector3 LookAtPoint = _playerTower.transform.position + _OffsetRotation;
        transform.LookAt(LookAtPoint);
    }

    private void OnHumanAdded(int count)
    {
        _targetPosition += _OffsetPosition + (Vector3.up + Vector3.back) * count;
        UpdatePosition();
    }


}
