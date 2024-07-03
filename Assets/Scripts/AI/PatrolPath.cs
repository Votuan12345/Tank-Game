using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();
    public int Length { get => patrolPoints.Count; }

    [Header("Gizmos parameters")]
    public Color pointsColor = Color.blue;
    public float pointSize = 0.3f;
    public Color lineColor = Color.magenta;

    public struct PathPoint
    {
        public int Index;
        public Vector2 Position;
    }

    // dùng để tìm điểm tuần tra gần nhất với vị trí hiện tại của xe tăng
    public PathPoint GetClosestPathPoint(Vector3 tankPosition)
    {
        var minDistance = float.MaxValue;
        var index = -1;
        for(int i = 0; i < patrolPoints.Count; i++)
        {
            var tempDistance = Vector2.Distance(tankPosition, patrolPoints[i].position);
            if(tempDistance < minDistance)
            {
                minDistance = tempDistance;
                index = i;
            }
        }

        return new PathPoint { Index = index, Position = patrolPoints[index].position };
    }

    // lấy điểm tuần tra tiếp theo trong danh sách patrolPoints dựa trên chỉ số hiện tại (index).
    public PathPoint GetNextPathPoint(int index)
    {
        var newIndex = (index + 1 >= patrolPoints.Count ? 0 : index + 1);
        return new PathPoint { Index = newIndex, Position = patrolPoints[newIndex].position };
    }

    private void OnDrawGizmos()
    {
        if(patrolPoints.Count == 0)
        {
            return;
        }
        for(int i = patrolPoints.Count - 1; i >= 0; i--)
        {
            if(patrolPoints[i] == null)
            {
                return;
            }

            // Vẽ hình cầu tại vị trí của điểm tuần tra
            Gizmos.color = pointsColor;
            Gizmos.DrawSphere(patrolPoints[i].position, pointSize);

            if (i == 0) return;

            // Vẽ đường nối giữa các điểm tuần tra (vd: 3-2, 2-1, 1-0)
            Gizmos.color = lineColor;
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i-1].position);

            // vẽ đường nối giữa điểm (vd: 3-0) => tạo thành 1 đường kín
            if (patrolPoints.Count > 2 && i == patrolPoints.Count - 1)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
            }
        }
    }
}
