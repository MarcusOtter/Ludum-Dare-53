using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SantaParticles : MonoBehaviour
{
	[SerializeField] private bool playOnGrounded;
	[SerializeField] private bool playOnLand;
	[SerializeField] private bool playOnJump;
	[SerializeField] private bool playOnChimneyJump;
	[SerializeField] private bool playOn360;
	[SerializeField] private bool playOn720;
	[SerializeField] private bool stopOnLand;

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
		if (playOnChimneyJump)
		{
			_santa.OnChimneyJump += Play;
		}

		if (stopOnLand)
		{
			_santa.OnLand += Stop;
		}
		if (playOn360 || playOn720)
		{
			TrickDetector.OnFlip += PlayFlip;
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

	private void PlayFlip(int amount)
	{
		if (amount == 1 && playOn360)
		{
			Play();
		}
		
		if (amount >= 2 && playOn720)
		{
			Play();
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
		_santa.OnChimneyJump -= Play;
		_santa.OnLand -= Stop;
		TrickDetector.OnFlip -= PlayFlip;
	}
}
