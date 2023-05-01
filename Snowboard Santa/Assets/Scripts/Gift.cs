using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Gift : MonoBehaviour
{
	[SerializeField] private AudioClip clip;
	[SerializeField] private SpriteRenderer wholeGift;
	[SerializeField] private SpriteRenderer brokenGift;
	
	private Rigidbody2D _rigidbody;
	
	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	public void Throw(Transform chimney, float force)
	{
		var direction = (chimney.position - transform.position).normalized;
		_rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
	}
	
	public void Stomp(float force)
	{
		// _rigidbody.AddForce(Vector2.down * force, ForceMode2D.Impulse);
		wholeGift.gameObject.SetActive(false);
		brokenGift.gameObject.SetActive(true);
	}
}
