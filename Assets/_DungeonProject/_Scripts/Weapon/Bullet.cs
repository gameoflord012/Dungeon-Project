using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Damager))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public UnityEvent OnBulletHitTarget;
    public UnityEvent OnBulletHitObstacle;
    public UnityEvent OnBulletHit;

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

    public void Init(Quaternion spawnAngle, LayerMask attackLayer)
    {
        Direction = spawnAngle * Vector3.right;
        gameObject.layer = attackLayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            OnBulletHitObstacle?.Invoke();
            OnBulletHit?.Invoke();
            Destroy(gameObject);
        }
        else if (LayerMaskManager.IsAttackable(collision.gameObject.layer))
        {
            if (collision.TryGetComponent(out Health damageTarget))
            {
                damageTarget.TakeDamage(damager);

                OnBulletHit?.Invoke();                
                OnBulletHitTarget?.Invoke();

                Destroy(gameObject);
            }
        }
    }
}