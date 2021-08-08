using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damager))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 20;

    private Damager damager;

    private void Awake()
    {
        damager = GetComponent<Damager>();
    }

    private Vector2 direction;
    public Vector2 Direction
    {
        get => direction;
        set
        {
            direction = value.normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }
        else if(LayerMaskManager.IsAttackable(collision.gameObject.layer))
        {            
            if(collision.TryGetComponent(out Health damageTarget))
            {
                damageTarget.CurrentHealth -= damager.Damage;
                Destroy(gameObject);
            }
        }
    }
}
