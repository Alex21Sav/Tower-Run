using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Vector2Int _humanInTowerRange;
    [SerializeField] private Human[] _humensTemplate;
    [SerializeField] private float _bounceForce;
    [SerializeField] private float _bounceRadius;

    private List<Human> _humanInTower;

    private void Start()
    {
        _humanInTower = new List<Human>();
        int humanInTowerCount = Random.Range(_humanInTowerRange.x, _humanInTowerRange.y);
        SpawnHumans(humanInTowerCount);
    }
    private void SpawnHumans(int humanCount)
    {
        Vector3 spawnPoint = transform.position;

        for (int i = 0; i < humanCount; i++)
        {
            Human spawnedHuman = _humensTemplate[Random.Range(0, _humensTemplate.Length)];

            _humanInTower.Add(Instantiate(spawnedHuman, spawnPoint, Quaternion.identity, transform));

            _humanInTower[i].transform.localPosition = new Vector3(0, _humanInTower[i].transform.localPosition.y, 0);

            spawnPoint = _humanInTower[i].FixationPoint.position;
        }
    }
    public List<Human> CollectHuman(Transform distanceChecker, float fixationMaxDistance)
    {
        for (int i = 0; i < _humanInTower.Count; i++)
        {
            float distanceBetweenPoint = CheckDistanceY(distanceChecker, _humanInTower[i].FixationPoint.transform);

            if(distanceBetweenPoint < fixationMaxDistance)
            {
                List<Human> CollectedHumans = _humanInTower.GetRange(0, i + 1);
                _humanInTower.RemoveRange(0, i + 1);
                return CollectedHumans;
            }
        }
        return null;
    }

    private float CheckDistanceY(Transform distanceCheckr, Transform humanFixationPoint)
    {
        Vector3 distanceCheckrY = new Vector3(0, distanceCheckr.position.y, 0);
        Vector3 humanFixationPointY = new Vector3(0, humanFixationPoint.position.y, 0);
        return Vector3.Distance(distanceCheckrY, humanFixationPointY);
    }

    public void Break()
    {
        //Human[] towerHumans = GetComponentsInChildren<Human>();

        //foreach (var human in towerHumans)
        //{
        //    human.Bounce(_bounceForce, transform.position, _bounceRadius);
        //}
        Destroy(gameObject);
    }
}
