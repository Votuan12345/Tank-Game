using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyAI : MonoBehaviour
{
    [SerializeField]
    private AIBehaviour shootBehaviour, patrolBehaviour;

    [SerializeField]
    private TankController tank;
    [SerializeField]
    private AIDerector detector;

    private void Awake()
    {
        detector = GetComponentInChildren<AIDerector>();
        tank = GetComponentInChildren<TankController>();
    }

    private void Update()
    {
        if(detector.TargetVisible)
        {
            shootBehaviour.PerformAction(tank, detector);
        }
        else
        {
            patrolBehaviour.PerformAction(tank, detector);
        }
    }
}
