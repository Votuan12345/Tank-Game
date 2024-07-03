using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfDisable : MonoBehaviour
{
    public bool SelfDestructionEnabled { get; set; } = false;

    // viên đạn sẽ bay đến khi bị tắt
    private void OnDisable()
    {
        // nếu tính năng huỷ đã bật => xoá 
        if (SelfDestructionEnabled)
        {
            Destroy(gameObject);
        }
    }
}
