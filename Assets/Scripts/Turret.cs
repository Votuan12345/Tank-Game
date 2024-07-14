using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectPool))]
public class Turret : MonoBehaviour
{
    [SerializeField] List<Transform> turretBarrels;

    public TurretData turretData;
    [SerializeField] private GameObject bulletPrefab;

    private bool canShoot = true;
    private Collider2D[] tankColliders;
    private float currentDelay = 0;

    private ObjectPool bulletPool;
    [SerializeField] private int bulletPoolCount = 10;

    public UnityEvent OnShoot, OnCantShoot;
    public UnityEvent<float> OnReloading;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
        bulletPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        bulletPool.Initialize(bulletPrefab, bulletPoolCount);
        OnReloading?.Invoke(currentDelay / turretData.reloadDelay);
    }

    private void Update()
    {
        if(canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            OnReloading?.Invoke(currentDelay / turretData.reloadDelay);

            if (currentDelay <= 0 )
            {
                canShoot = true;
            }
        }
    }

    public void Shoot()
    {
        if(canShoot == true)
        {
            canShoot = false;
            currentDelay = turretData.reloadDelay;

            foreach(var barrel in turretBarrels)
            {
                var hit = Physics2D.Raycast(barrel.position, barrel.up);

                //GameObject bullet = Instantiate(bulletPrefab);
                GameObject bullet = bulletPool.CreateObject();

                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize(turretData.bulletData);

                // bỏ qua va chạm của đạn với xe tank
                foreach(var collider in tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider, true);   
                }
            }

            OnShoot?.Invoke();
            OnReloading?.Invoke(currentDelay / turretData.reloadDelay);
        }
        else
        {
            OnCantShoot?.Invoke();
        }
    }
}
