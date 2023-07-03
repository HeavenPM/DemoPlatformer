using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private int _obstacleID;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() ||
            collision.gameObject.GetComponent<Enemy>())
            EventManager.OnObstacleOpenedWithID(_obstacleID);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() ||
            collision.gameObject.GetComponent<Enemy>())
            EventManager.OnObstacleClosedWithID(_obstacleID);
    }
}
