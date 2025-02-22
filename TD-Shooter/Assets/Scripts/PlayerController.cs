using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables

    public float walkSpeed = 6f;
    public float stoppingForce = 10f;
    private Vector3 moveDirection = Vector3.zero;

    public GameObject playerObject;

    private WeaponControllSystem weaponControlSystem;
    private Rigidbody playerRigidBody;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        weaponControlSystem = GetComponent<WeaponControllSystem>();
    }


    void FixedUpdate()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        // mouse
        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        Vector3 movementVector = GetMovementVector();

        // Если есть движение, применяем силу
        if (movementVector.magnitude > 0)
        {
            playerRigidBody.MovePosition(playerRigidBody.position + movementVector * walkSpeed * Time.deltaTime);
        }
        else
        {
            // Применяем ускоренное обнуление скорости для быстрой остановки
            playerRigidBody.velocity = Vector3.Lerp(playerRigidBody.velocity, Vector3.zero, stoppingForce * Time.deltaTime);
        }
    }

    private Vector3 GetMovementVector()
    {
        // Получаем значения осей (горизонтальная и вертикальная оси)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Возвращаем нормализованный вектор для движения по оси X и Z
        return new Vector3(horizontal, 0.0f, vertical).normalized;
    }
}
