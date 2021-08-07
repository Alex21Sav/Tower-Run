using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;
    //[SerializeField] private Rigidbody _rigidbody;

    public Transform FixationPoint => _fixationPoint;

    //public void Bounce(float force, Vector3 centre, float radius)
    //{
    //    if (TryGetComponent(out Rigidbody rigidbody))
    //    {
    //        rigidbody.isKinematic = false;
    //        rigidbody.AddExplosionForce(force, centre, radius);
    //    }
    //}

}
