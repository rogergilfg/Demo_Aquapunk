using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] float smoothTime;
    private Vector3 camVelocity;
    
    private void Start()
    {

    }

    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref camVelocity, smoothTime);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
