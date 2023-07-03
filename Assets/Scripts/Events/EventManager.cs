using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<int> HealthUpdated;
    public static event UnityAction<int> ObstacleOpenedWithID;
    public static event UnityAction<int> ObstacleClosedWithID;
    public static event UnityAction PlayerHit;
    public static event UnityAction PlayerHeal;
    public static event UnityAction GameOver;

    public static void OnHealthUpdated(int health) => HealthUpdated?.Invoke(health);
    public static void OnObstacleOpenedWithID(int id) => ObstacleOpenedWithID?.Invoke(id);
    public static void OnObstacleClosedWithID(int id) => ObstacleClosedWithID?.Invoke(id);
    public static void OnPlayerHit() => PlayerHit?.Invoke();
    public static void OnPlayerHeal() => PlayerHeal?.Invoke();
    public static void OnGameOver() => GameOver?.Invoke();
}
