using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MyPlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private ContactFilter2D _platform;
    private bool _isOnPlatform => _rigidbody.IsTouching(_platform); // Проверка, находится ли персонаж на платформе
    private bool _jumpRequested; //Булевская переменная для отслеживания запроса на прыжок

    public UnityEvent Landed; // Событие при приземлении персонажа
    public UnityEvent Dead; // Событие при смерти персонажа


    // Метод запускается при старте сцены
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    //При нажатии на Space, персонаж - прыгает
    private void Update()
    {
        // Обработка нажатия Space
        if (Input.GetKeyDown(KeyCode.Space))
            _jumpRequested = true;
    }

    // Публичный метод для прыжка, вызывается кнопкой
    public void Jump()
    {
        _jumpRequested = true;
    }

    //Метод вызывается с фиксированным интервалом времени
    private void FixedUpdate()
    {
        TryJump();
    }

    // Метод запускается при попытке прыжка персонажем
    private void TryJump()
    {
        if (_jumpRequested && _isOnPlatform)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse); // Прыжок вверх
        }
        // Сброс запроса на прыжок
        _jumpRequested = false;
    }

    // Метод вызывается при столкновении с другим объектом
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject; // Получение объекта, с которым произошло столкновение

        if (collisionObject.transform.parent != null) { 
            if(collisionObject.transform.parent.TryGetComponent(out Platform platform)) // Проверка наличия компонента Platform у родительского объекта
                platform.StopMovement(); // Остановка движения платформы при столкновении
        }
        if(collisionObject.CompareTag("PlatformWall"))
            Dead?.Invoke();
        else if (collisionObject.CompareTag("Platform")) {
            collisionObject.tag = "Untagged"; // Снятие тега "Platform" после приземления
            Landed?.Invoke();
        }
    }

}
