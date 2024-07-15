using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public int MaxHealth = 100;
    private int health = 0;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            OnHealthChange?.Invoke((float)Health / MaxHealth);

            if (gameObject.CompareTag("Player"))
            {
                UIManager.instance.ShowHealthBar((float)Health / MaxHealth);
            }
        }
    }

    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHit, OnHeal;

    private void Start()
    {
        if(health == 0)
        {
            Health = MaxHealth;
        }

    }

    // sự kiện chịu sát thương
    internal void Hit(int damagePoints)
    {
        Health -= damagePoints;
        if(Health < 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }
    }

    
    // kích hoạt event hồi máu
    public void Heal(int healthBoost)
    {
        Health += healthBoost;
        Health = Mathf.Clamp(Health, 0, MaxHealth);
        OnHeal?.Invoke();
    }

    // player
    public void Dead()
    {
        if (gameObject.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
            Camera camera = Camera.main;
            if(camera != null)
            {
                camera.AddComponent<AudioListener>();
            }
        }
    }
}
