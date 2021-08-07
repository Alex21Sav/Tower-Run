using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human _startHuman;
    [SerializeField] private Transform _distanceChecker;
    [SerializeField] private float _fixationMaxDistance;
    [SerializeField] private BoxCollider _checkerCollider;

    private List<Human> _humans;

    public event UnityAction<int> HumanAdded;

    private void Start()
    {
        _humans = new List<Human>();
        Vector3 spawnPoint = transform.position;
        _humans.Add(Instantiate(_startHuman, spawnPoint, Quaternion.identity, transform));
        HumanAdded?.Invoke(_humans.Count);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Human human))
        {
            Tower collisionTower = human.GetComponentInParent<Tower>();
            if(collisionTower != null)
            {
                List<Human> collectedHuman = collisionTower.CollectHuman(_distanceChecker, _fixationMaxDistance);

                if (collectedHuman != null)
                {
                    for (int i = collectedHuman.Count - 1; i >= 0; i--)
                    {
                        Human insertHuman = collectedHuman[i];
                        InsertHuman(insertHuman);
                        DisplceCheckers(insertHuman);
                    }
                    HumanAdded?.Invoke(_humans.Count);
                }
                collisionTower.Break();
            }          
            
        }
    }

    private void InsertHuman(Human collectedHuman)
    {
        _humans.Insert(0, collectedHuman);
        SetHumanPosition(collectedHuman);
    }

    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
        human.transform.localRotation = Quaternion.identity;
    }

    private void DisplceCheckers(Human human)
    {
        float displceScale = 1.5f;
        Vector3 distanceCheckerNewPosition = _distanceChecker.position;
        distanceCheckerNewPosition.y -= human.transform.localScale.y * displceScale;
        _distanceChecker.position = distanceCheckerNewPosition;
        _checkerCollider.center = _distanceChecker.localPosition;
    }
}
