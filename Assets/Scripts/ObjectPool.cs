using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int poolSize = 10;

    protected Queue<GameObject> objectPool;

    public Transform spawnedObjectsParent;

    public bool alwaysDestroy = false;

    private void Awake()
    {
        objectPool = new Queue<GameObject>();
    }

    public void Initialize(GameObject objectToPool, int poolSize = 10)
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolSize;
    }

    public GameObject CreateObject()
    {
        CreateObjectParentIfNeeded();
        GameObject spawnedObject = null;

        if(objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(objectToPool, transform.position, Quaternion.identity); 
            spawnedObject.name = transform.root.name + "_" + objectToPool.name + "_" + objectPool.Count;
            spawnedObject.transform.SetParent(spawnedObjectsParent);

            spawnedObject.AddComponent<DestroyIfDisable>();
        }
        else
        {
            // lấy phần tử đầu tiên và loại bỏ ra khỏi hàng đợi
            spawnedObject = objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }

        // thêm phần tử vào cuối hàng đợi
        objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    private void CreateObjectParentIfNeeded()
    {
        if(spawnedObjectsParent == null)
        {
            string name = "ObjectPool_" + objectToPool.name;
            var parentObject = GameObject.Find(name);
            if(parentObject != null)
            {
                spawnedObjectsParent = parentObject.transform;
            }
            else
            {
                spawnedObjectsParent = new GameObject(name).transform;
            }
        }
    }

    // xoá viên đạn lưu trữ khi tank bị bắn hạ
    private void OnDestroy()
    {
        foreach(var item in objectPool)
        {
            if(item == null) continue;
            else if(item.activeSelf == false || alwaysDestroy)
            {
                Destroy(item);
            }
            else
            {
                // Bật tính năng huỷ (viên đạn sẽ bay 1 lúc đến khi tắt thì bị xoá)
                item.GetComponent<DestroyIfDisable>().SelfDestructionEnabled = true;
            }
        }
    }
}
