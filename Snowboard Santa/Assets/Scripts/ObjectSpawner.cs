using UnityEngine;

// This may not be used, if so remove it
public class ObjectSpawner : MonoBehaviour
{
	[SerializeField] private float spawnInterval;
	[SerializeField] private Transform parent;
	[SerializeField] private Transform[] objects;

	private float _spawnTimer;

	private void Update()
	{
		if (spawnInterval <= 0) return; 
		_spawnTimer -= Time.deltaTime;

		if (_spawnTimer > 0) return;
		_spawnTimer = spawnInterval;
		Spawn();
	}

	public void Spawn()
	{
		var randomObject = objects[Random.Range(0, objects.Length)];
		 Instantiate(randomObject, parent);
	}
}
