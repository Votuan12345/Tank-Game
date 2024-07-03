using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootBehaviour : AIBehaviour
{
    // (field of vision - FOV): tầm nhìn

    public float fieldOfVisionForShooting = 60; // góc nhìn khi bắn

    public override void PerformAction(TankController tank, AIDerector detector)
    {
        if(TargetInFOV(tank, detector))
        {
            tank.HandleMoveBody(Vector2.zero);
            tank.HandleShoot();
        }
        tank.HandleTurretMovement(detector.Target.position);
    }

    // xác định xem mục tiêu có nằm trong phạm vi bắn của họng súng
    private bool TargetInFOV(TankController tank, AIDerector detector)
    {
        var direction = detector.Target.position - tank.aimTurret.transform.position;

        if(Vector2.Angle(tank.aimTurret.transform.right, direction) < fieldOfVisionForShooting / 2)
        {
            return true;
        }
        return false;
    }
}
