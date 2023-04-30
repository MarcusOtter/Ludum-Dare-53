using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SantaParticles : MonoBehaviour
{
	[SerializeField] private bool playOnGrounded;
	[SerializeField] private bool playOnLand;
	[SerializeField] private bool playOnJump;

	private SantaMovement _santa;
	private ParticleSystem _particleSystem;
	
	private void Awake()
	{
		_particleSystem = GetComponent<ParticleSystem>();
		_santa = FindObjectOfType<SantaMovement>();

	}

	private void OnEnable()
	{
		if (!_santa) return;
		if (playOnLand)
		{
			_santa.OnLand += Play;
		}
		if (playOnJump)
		{
			_santa.OnJump += Play;
		}
	}

	private void Update()
	{
		if (!playOnGrounded) return;
		
		if (_santa.IsGrounded)
		{
			Play();
		}
		else
		{
			Stop();
		}
	}

	private void Play()
	{
		if (_particleSystem.main.loop && _particleSystem.isPlaying) return;
		_particleSystem.time = 0;
		_particleSystem.Play();
	}

	private void Stop()
	{
		_particleSystem.Stop();
	}

	private void OnDisable()
	{
		if (!_santa) return;
		
		_santa.OnLand -= Play;
		_santa.OnJump -= Play;
	}
}
