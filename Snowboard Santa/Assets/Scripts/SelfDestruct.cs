using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SelfDestruct : MonoBehaviour
{
	[SerializeField] private float time;
	[SerializeField] private bool destroyOnCollision;
	[SerializeField] private bool destroyOnTrigger;
	[SerializeField] private Transform destroyWhenPassed;
	[SerializeField] private UnityEvent onDestroy;
	[SerializeField] private Transform[] spawnOnDeath;

	private bool _destroyWhenPassed;
	
	private IEnumerator Start()
	{
		if (time > 0f)
		{
			yield return new WaitForSeconds(time);
			DestroyNow();
		}

		_destroyWhenPassed = destroyWhenPassed != null;
	}

	private void Update()
	{
		if (!_destroyWhenPassed) return;
		
		if (transform.position.x < destroyWhenPassed.position.x)
		{
			DestroyNow();
		}

	}

	private void DestroyNow()
	{
		foreach(var obj in spawnOnDeath)
		{
			Instantiate(obj, transform.position, Quaternion.identity);
		}
		onDestroy?.Invoke();
		Destroy(gameObject);
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (destroyOnCollision)
		{
			DestroyNow();
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (destroyOnTrigger)
		{
			DestroyNow();
		}
	}
}
