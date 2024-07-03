using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolPathBehaviour : AIBehaviour
{
    public PatrolPath patrolPath;
    [Range(0.1f, 1.0f)]
    public float arriveDistance = 1f;
    public float waitTime = 0.5f;
    [SerializeField]
    private bool isWaiting = false;

    // tương ứng với Position, Index của PathPoint
    [SerializeField]
    Vector2 currentPatrolTarget = Vector2.zero;
    private int currentIndex = -1;

    private bool isInitialized = false;


    private void Awake()
    {
        if (patrolPath == null)
        {
            patrolPath = GetComponentInChildren<PatrolPath>();
        }
    }

    public override void PerformAction(TankController tank, AIDerector detector)
    {
        if (!isWaiting)
        {
            if (patrolPath.Length < 2)
            {
                return;
            }

            if (!isInitialized)
            {
                var currentPathPoint = patrolPath.GetClosestPathPoint(tank.transform.position);
                this.currentIndex = currentPathPoint.Index;
                this.currentPatrolTarget = currentPathPoint.Position;
                isInitialized = true;
            }

            if (Vector2.Distance(tank.transform.position, currentPatrolTarget) < arriveDistance)
            {
                isWaiting = true;
                StartCoroutine(WaitCoroutine());
                return;
            }

            Vector2 directionToGo = currentPatrolTarget - (Vector2)tank.tankMover.transform.position;
            
            // tính tích vô hướng giữa 2 vector
            var dotProduct = Vector2.Dot(tank.tankMover.transform.up, directionToGo.normalized);

            // 2 vector không cùng hướng => rẽ
            if (dotProduct < 0.98f)
            {
                // tính tích có hướng => trả về 1 vector vuông góc với 2 vector
                var crossProduct = Vector3.Cross(tank.tankMover.transform.up, directionToGo.normalized);

                // xác định xem quay bên phải hay bên trái dựa vào crossProduct.z
                int rotationResult = crossProduct.z >= 0 ? -1 : 1;
                tank.HandleMoveBody(new Vector2(rotationResult, 1));
            }
            else
            {
                // 2 vector cùng hướng => di chuyển lên (0, 1)
                tank.HandleMoveBody(Vector2.up);
            }
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        var nextPathPoint = patrolPath.GetNextPathPoint(this.currentIndex);
        this.currentIndex = nextPathPoint.Index;
        this.currentPatrolTarget = nextPathPoint.Position;
        isWaiting = false;
    }
}
