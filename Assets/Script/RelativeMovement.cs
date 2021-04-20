using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] //Окружающие строки показывают контекст размещения метода RequireComponent(). Провера присоединён ли к объекту данный компонент.
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target; //Сценарию нужна ссылка на объект, относительно которого происходит вращение.

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f; //Скорость прыжка.
    public float gravity = -9.8f; //Сила гравитации.
    public float terminalVelocity = -10.0f;   //Предельная скорость.
    public float minFall = -1.5f; //Минимальоне скорость падение.
    public float pushForce = 3.0f; //Величина прилагаемой силы.

    private float _vertSpeed;

    private CharacterController _characterController;
    private ControllerColliderHit _contact; //Пременная дл хранения данных о столкновении между функциями.
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _vertSpeed = minFall; //Инициализируем переменную вертикальной скорости, присваивая ей минимальную скорость падения в начале существующей функции.
        _characterController = GetComponent<CharacterController>(); //Шаблон, используемый для доступа к другим компонентам.
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool hitGround = false;

        RaycastHit hit;

        Vector3 movement = Vector3.zero; //Начинаем с вектора (0,0,0), постепеннно добавляем компоненты вижения.

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        if (horInput != 0 || vertInput != 0) //Движение обрабатывается только при нажатии клавиш со стрелками.
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed); //Ограничивает движение по диагонали той же скоростью, что и движение в доль оси.

            Quaternion tmp = target.rotation; //Сохраняем начальную ориентацию, что бы вернуться к ней после завершения работы с целеым объектом.
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement); //Преобразуем направление движения из локальных в глобальные координаты.
            target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_characterController.height + _characterController.radius) / 1.9f;  //Расстояние, с которым производится сравнение (слегка выходит за нижнюю часть капсулы).
            hitGround = hit.distance <= check;
        }

        if (hitGround) //Результат метода бросания лучей.
        {
            if (Input.GetButtonDown("Jump")) //Реакция на кнопку Jump при нахождении на поверхности.
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;
                _animator.SetBool("Jumping", false);
            }
        }
        else //Если персонаж не стоит на поверхности, применяем гравитацию, пока не будет достигнута предельная скорость.
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;

            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }

            if (_contact != null) 
            {
                _animator.SetBool("Jumping", true); //Не активируйте это значение в начале уровня.
            }

            if (_characterController.isGrounded)  //Метод бросания лучей не обнаруживает поверзности, но капсула с ней соприкасается.
            {
                if (Vector3.Dot(movement, _contact.normal) < 0) //Реакция меняется в зависимости от того, смотрит ли персонаж в сторону точки контакта.
                {
                    movement = _contact.normal * moveSpeed;
                }
                else
                {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }

        movement.y = _vertSpeed; 

        movement *= Time.deltaTime; //Перемещения всегда нужно умножать на deltaTime, что бы они были независимы от частоты кадров.
        _characterController.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) //При распозновании столкновения данные этого столкновения сохраняются в методе обратного вызова.
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic) //Проверяем, есть ли у учавствующего в столкновении объекта компонент Rigibody, обеспечивающий реакцию на приложенную силу.
        {
            body.velocity = hit.moveDirection * pushForce; //Назначаем физическому телу скорость.
        }
    }

}
