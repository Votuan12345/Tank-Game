using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolStaticBehaviour : AIBehaviour
{
    public float patroDelay = 4;

    [SerializeField]
    private Vector2 randomDirection = Vector2.zero;
    [SerializeField]
    private float currentPatrolDelay;


    private void Awake()
    {
        // lấy ngẫu nhiên 1 điểm trong vòng tròn bán kính = 1
        randomDirection = Random.insideUnitCircle;
    }

    public override void PerformAction(TankController tank, AIDerector detector)
    {
        // xác định góc giữa 2 vector
        float angle = Vector2.Angle(tank.aimTurret.transform.right, randomDirection);
        if (currentPatrolDelay <= 0 && (angle < 2))
        {
            randomDirection = Random.insideUnitCircle;
            currentPatrolDelay = patroDelay;
        }
        else
        {
            if(currentPatrolDelay > 0)
            {
                currentPatrolDelay -= Time.deltaTime;
            }
            else
            {
                tank.HandleTurretMovement((Vector2)tank.aimTurret.transform.position + randomDirection);
            }
        }
    }
}
