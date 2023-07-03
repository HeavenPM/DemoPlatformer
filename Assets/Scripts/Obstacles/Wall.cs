using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private int _obstacleID;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _closedWallPosition;
    [SerializeField] private Transform _openedWallPosition;
    [SerializeField] private bool _isСlosed;

    private Transform _target;

    private void Start()
    {
        _target = _openedWallPosition;
    }

    private void Update()
    {
        _target = _isСlosed ? _openedWallPosition : _closedWallPosition;
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = _target.position;
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, _moveSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    private void OnEnable()
    {
        EventManager.ObstacleOpenedWithID += EventOpened;
        EventManager.ObstacleClosedWithID += EventClosed;
    }

    private void OnDisable()
    {
        EventManager.ObstacleOpenedWithID -= EventOpened;
        EventManager.ObstacleClosedWithID -= EventClosed;
    }

    private void EventOpened(int id)
    {
        if (_obstacleID == id) _isСlosed = false;
    }

    private void EventClosed(int id)
    {
        if (_obstacleID == id) _isСlosed = true;
    }
}
