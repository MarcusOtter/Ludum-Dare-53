using System.Linq;
using UnityEngine;

public class KillWall : MonoBehaviour
{
	[SerializeField] private Transform[] exemptObjects;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (exemptObjects.Any(x => other.transform == x))
		{
			return;
		}
		
		Destroy(other.gameObject);
	}
}
