using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDerector : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField] 
    private float viewRadius = 11f;
    [SerializeField]
    private float dectionCheckDelay = 0.1f;
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private LayerMask playerLayerMask;
    [SerializeField]
    private LayerMask visibilityLayer;

    [field: SerializeField]
    public bool TargetVisible { get; private set; }
    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        if (Target != null)
        {
            TargetVisible = CheckTargetVisible();
        }
    }

    private bool CheckTargetVisible()
    {
        var result = Physics2D.Raycast(transform.position, Target.position - transform.position, 
            viewRadius, visibilityLayer);

        if(result.collider != null)
        {
            // 00001000 
            //kiểm tra bitwise để xem liệu lớp của collider mà raycast chạm phải
            //có nằm trong playerLayerMask hay không.
            return (playerLayerMask & (1 << result.collider.gameObject.layer)) != 0;
        }

        return false;
    }

    private void DectectTarget()
    {
        if(Target == null)
        {
            CheckIfPlayerInRange();
        }
        else if(Target != null)
        {
            DectectIfOutOfRange();
        }
    }

    private void DectectIfOutOfRange()
    {
        if(Target == null || Target.gameObject.activeSelf == false || Vector2.Distance(
            transform.position, Target.position) > viewRadius + 1)
        {
            Target = null;
        }
    }

    private void CheckIfPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask);
        if(collision != null)
        {
            Target = collision.transform;
        }
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(dectionCheckDelay);
        DectectTarget();

        StartCoroutine(DetectionCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
