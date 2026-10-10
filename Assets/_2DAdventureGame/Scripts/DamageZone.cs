using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();

        if (controller != null)
        {
            Debug.Log("Зіткнення спрацювало!");
            controller.ChangeHealth(-1);
        }
    }
}