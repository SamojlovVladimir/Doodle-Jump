using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlatform : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private GameObject _player;
    private int _moveDirection; //направление движения платформы
    private bool _hasToMove = true;

    //метод, который вызывается автоматически при старте сцены
    private void Awake() { 
        _player = GameObject.FindWithTag("Player"); //ссылка на игрока
        _moveDirection = transform.position.x < _player.transform.position.x ? 1 : -1; // определение направления движения платформы, 1 - вправо, -1 - влево
    }

    //метод, который вызывается автоматически каждый кадр
    private void Update()
    {
        if (_hasToMove == true)
            transform.position += Vector3.right * _moveDirection * _moveSpeed * Time.deltaTime;
    }

    //метод для остановки движения платформы
    public void StopMovement() => _hasToMove = false;

    private void OnBecameInvisible()
    {
        Destroy(gameObject); //Оптимизация - удаление платформы, когда она выходит из видимости камеры
    }
}
