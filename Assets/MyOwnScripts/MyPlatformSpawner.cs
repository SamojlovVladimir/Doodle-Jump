using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlatformSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints; //массив точек спавна
    [SerializeField] private GameObject _platformPrefab; //Ссылка на префаб
    [SerializeField] private float _verticalOffset = 0.5f; //Задаём расстояние между платоформами

    private float? _lastPointPositionY = null;

    public void Start(){
        Spawn();
    }

    public void Spawn(){
        Transform rSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        float spawnPointPositionY = _lastPointPositionY == null ? rSpawnPoint.position.y : (float)_lastPointPositionY + _verticalOffset;
        rSpawnPoint.position = new Vector3(rSpawnPoint.position.x, spawnPointPositionY, rSpawnPoint.position.z);
        _lastPointPositionY = spawnPointPositionY;

        //
        Instantiate(_platformPrefab, rSpawnPoint.position, Quaternion.identity);
    }
}
