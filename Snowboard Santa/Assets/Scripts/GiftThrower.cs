using UnityEngine;

public class GiftThrower : MonoBehaviour
{
    [SerializeField] private Gift[] gifts;
    [SerializeField] private float initialForce = 10f;
    [SerializeField] private float stompForce = 10f;
    
    private SantaMovement _santa;
    private Gift _latestThrownGift;

    private void Awake()
    {
        _santa = FindObjectOfType<SantaMovement>();
    }

    private void OnEnable()
    {
        _santa.OnOverChimneyEnter += ThrowGift;
        _santa.OnChimneyJump += StompGift;
    }

    private void StompGift()
    {
        _latestThrownGift.Stomp(stompForce);
    }

    private void ThrowGift(Transform chimney)
    {
        var randomGift = gifts[Random.Range(0, gifts.Length)];
        var gift = Instantiate(randomGift, transform.position, Quaternion.identity);
        gift.Throw(chimney, initialForce);

        _latestThrownGift = gift;
    }
    
    private void OnDisable()
    {
        _santa.OnOverChimneyEnter -= ThrowGift;
        _santa.OnChimneyJump -= StompGift;
    }
}
