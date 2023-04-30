using UnityEngine;

public class TransformFollower : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private bool followY;
	[SerializeField] private bool followX;
	[SerializeField] private bool followRotation;

	private Vector3 _offset;
	private Quaternion _rotationOffset;
	private Transform _transform;

	private void Start()
	{
		_transform = transform;
		_offset = _transform.position - target.position;
		_rotationOffset = Quaternion.Inverse(target.rotation) * _transform.rotation;
	}
	
	private void Update()
	{
		var position = _transform.position;
		
		if (followRotation)
		{
			_transform.rotation = target.rotation * _rotationOffset;
		}
		
		if (followX)
		{
			_transform.position = new Vector3(target.position.x + _offset.x, position.y, position.z);
		}
		
		if (followY)
		{
			_transform.position = new Vector3(position.x, position.y + _offset.y, position.z);
		}
	}
}

