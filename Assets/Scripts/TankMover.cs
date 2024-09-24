using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TankMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementVector;

    public TankMovementData movementData;

    [SerializeField] private float currentSpeed = 0; 
    [SerializeField] private float currentForwardDirection = 1;

    public UnityEvent<float> OnSpeedChange = new UnityEvent<float>();

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        CalculateSpeed(movementVector);

        // nếu nhấn mũi tên => movementVector.magnitude > 0
        OnSpeedChange?.Invoke(this.movementVector.magnitude);

        if(movementVector.y > 0)
        {
            currentForwardDirection = 1;
        }
        else if(movementVector.y < 0)
        {
            currentForwardDirection = -1;
        }
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        // th nhấn nút di chuyển => tăng tốc
        if(Mathf.Abs(movementVector.y) > 0)
        {
            currentSpeed += movementData.acceleration * Time.deltaTime;
        }
        else // th không nhấn => giảm tốc
        {
            currentSpeed -= movementData.deacceleration * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, movementData.maxSpeed);
    }

    private void FixedUpdate()
    {
        rb.velocity = (Vector2)transform.up * currentSpeed * currentForwardDirection * Time.fixedDeltaTime;

        // xoay theo chiều kim đồng hồ thi nhấn mũi tên phải
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x
            * movementData.rotationSpeed * Time.fixedDeltaTime));
    }
}
