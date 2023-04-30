using UnityEngine;

public class Snowboard : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private SantaMovement santa;

	[Header("Settings")]
	[SerializeField] private float speed = 500f;
	
	private Vector3 _axis;
	
	private void OnEnable()
	{
		santa.OnJump += HandleJump;
	}

	private void HandleJump()
	{
		var axes = new[] { Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.back, Vector3.forward };
		var randomAxis = axes[Random.Range(0, axes.Length)];
		_axis = randomAxis;
	}

	private void Update()
	{
		if (santa.IsGrounded)
		{
			transform.localRotation = Quaternion.identity;
		}
		else
		{
			transform.Rotate(_axis, speed * Time.deltaTime);
		}
	}

	private void OnDisable()
	{
		santa.OnJump -= HandleJump;
	}
}
