using UnityEngine;

public class ExtraLive : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth player))
        {
            int playerHealth = player.Health;
            if (playerHealth < 3)
            {
                EventManager.OnPlayerHeal();
                Destroy(gameObject);
            }
        }
    }
}
