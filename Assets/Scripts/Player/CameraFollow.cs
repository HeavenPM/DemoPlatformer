using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position + _offset;
        targetPosition.y = Mathf.Clamp(targetPosition.y, _minY, _maxY);

        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
    }
}
