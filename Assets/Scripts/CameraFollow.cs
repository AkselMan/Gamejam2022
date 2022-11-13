using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

    public void Start()
    {
		target = FindObjectOfType<PlayerMovement>().transform;
    }

    void FixedUpdate()
	{
		Vector3 desiredPosition = new Vector3(0, target.position.y, offset.z);
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
	}

}