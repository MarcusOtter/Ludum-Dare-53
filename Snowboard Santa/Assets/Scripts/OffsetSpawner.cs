using UnityEngine;
using System.Linq;
// It is assumed that this script is always moving to the right
public class OffsetSpawner : MonoBehaviour
{
	[SerializeField] private float minOffset;
	[SerializeField] private float maxOffset;
	[SerializeField] private int startAmount;
	[SerializeField] private SpriteRenderer[] objects;
	[SerializeField] private Transform parent;

	private Vector3 _nextSpawnPosition;
	private int _previousIndex = -1;

	private void Awake()
	{
		for(var i = 0; i < startAmount; i++)
		{
			Spawn();
			transform.position = _nextSpawnPosition;
		}
	}

	private void Update()
	{
		if (transform.position.x < _nextSpawnPosition.x) return;

		Spawn();
	}
	
	private void Spawn()
	{
		var randomIndex = _previousIndex;
		while (objects.Length > 1 && randomIndex == _previousIndex)
		{
			randomIndex = Random.Range(0, objects.Length);
		}
		var randomObject = objects[randomIndex];
		var spawned = Instantiate(randomObject, transform.position, Quaternion.identity, parent);
		
		var offset = Random.Range(minOffset, maxOffset);

		var sprites = spawned.GetComponentsInChildren<SpriteRenderer>();
		float maxWidth = sprites.OrderBy(spr => spr.Right()).Last().Right() - spawned.transform.position.x;

		_nextSpawnPosition = transform.position.Add(x: offset + maxWidth);
		_previousIndex = randomIndex;
	}
}
