using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LavelCreator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Tower _towerTemplate;
    [SerializeField] private int _humanTowerCount;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        float roadSize = _pathCreator.path.length;
        float distanceBetweenTower = roadSize / _humanTowerCount;

        float distanceTravellet = 0;
        Vector3 spawnPoint;

        for (int i = 0; i < _humanTowerCount; i++)
        {
            distanceTravellet += distanceBetweenTower;
            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravellet, EndOfPathInstruction.Stop);

            Instantiate(_towerTemplate, spawnPoint, Quaternion.identity);
        }
    }


}
