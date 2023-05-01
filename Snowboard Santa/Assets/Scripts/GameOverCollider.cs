using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameOverCollider : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    
    [FormerlySerializedAs("playerLayer")]
    [SerializeField] private LayerMask gameOverLayer;
    
    private void CollisionCheck(Collider2D other)
    {
        if (gameOverLayer == (gameOverLayer | (1 << other.gameObject.layer)))
        {
            OnPlayerDeath?.Invoke();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        CollisionCheck(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CollisionCheck(other.collider);
    }
}
