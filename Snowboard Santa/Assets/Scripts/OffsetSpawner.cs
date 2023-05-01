using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

// It is assumed that this script is always moving to the right
public class OffsetSpawner : MonoBehaviour
{
	[FormerlySerializedAs("minOffset")]
	[SerializeField] private float minHorizontalOffset;
	[FormerlySerializedAs("maxOffset")]
	[SerializeField] private float maxHorizontalOffset;
	[SerializeField] private float minRotation;
	[SerializeField] private float maxRotation;
	[SerializeField] private float minVerticalOffset;
	[SerializeField] private float maxVerticalOffset;
	[SerializeField] private int startAmount;
	[SerializeField] private Transform[] objects;
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
		
		var randomRotation = Quaternion.AngleAxis(Random.Range(minRotation, maxRotation), Vector3.forward);
		var randomVerticalOffset = Random.Range(minVerticalOffset, maxVerticalOffset);
		var randomHorizontalOffset = Random.Range(minHorizontalOffset, maxHorizontalOffset);
		
		var spawned = Instantiate(randomObject, transform.position.Add(randomVerticalOffset), randomRotation, parent);
		
		var sprites = spawned.GetComponentsInChildren<SpriteRenderer>();
		var maxWidth = sprites.OrderBy(spr => spr.Right()).Last().Right() - spawned.position.x;

		_nextSpawnPosition = transform.position.Add(x: randomHorizontalOffset + maxWidth);
		_previousIndex = randomIndex;
	}
}
