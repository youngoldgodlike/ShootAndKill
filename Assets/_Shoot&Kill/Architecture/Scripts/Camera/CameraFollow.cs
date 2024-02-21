using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        transform.position = _target.position + _offset;
        transform.LookAt(_target);
    }
}
