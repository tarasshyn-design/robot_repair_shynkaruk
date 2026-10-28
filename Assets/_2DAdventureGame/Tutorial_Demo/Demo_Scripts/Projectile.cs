using UnityEngine;

namespace Beginner2D
{
    public class Projectile : MonoBehaviour
    {
        Rigidbody2D rigidbody2d;

        void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Helpers.RecursiveLayerSet(transform, Helpers.ProjectileLayer);
        }

        void Update()
        {
            if (transform.position.magnitude > 1000.0f)
            {
                Destroy(gameObject);
            }
        }

        public void Launch(Vector2 direction, float force)
        {
            rigidbody2d.AddForce(direction * force);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Fix();
            }
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}