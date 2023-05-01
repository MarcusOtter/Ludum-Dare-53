using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
	[SerializeField] private float time;
	[SerializeField] private bool destroyOnCollision;
	[SerializeField] private bool destroyOnTrigger;

	private void Awake()
	{
		if (time > 0f)
		{
			Destroy(gameObject, time);
		}
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (destroyOnCollision)
		{
			Destroy(gameObject);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (destroyOnTrigger)
		{
			Destroy(gameObject);
		}
	}
}
