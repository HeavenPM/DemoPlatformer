using UnityEngine;

public class LivesViewer : MonoBehaviour
{
    [SerializeField] private GameObject[] _livesObjects;

    private readonly int _livesCount = 3;

    private void Start()
    {
        UpdateLivesView(_livesCount);
    }

    private void OnEnable()
    {
        EventManager.HealthUpdated += EventHealthUpdated;
    }

    private void OnDisable()
    {
        EventManager.HealthUpdated -= EventHealthUpdated;
    }

    private void EventHealthUpdated(int health)
    {
        UpdateLivesView(health);   
    }

    private void UpdateLivesView(int lives)
    {
        if (lives >= 0)
        {
            ActivateAllSprites();
            for (int i = _livesCount; i > lives; i--)
            {
                _livesObjects[i - 1].SetActive(false);
            }
        }
    }

    private void ActivateAllSprites()
    {
        for (int i = 0; i < _livesObjects.Length; i++)
        {
            _livesObjects[i].SetActive(true);
        }
    }
}
