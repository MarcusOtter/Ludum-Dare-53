using System;
using UnityEngine;

public class TrickDetector : MonoBehaviour
{
    public static event Action<int> OnFlip;
    
    //Air time
    //Super air time
    //Front & back flip
    //Front & back wheelie

    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private float minAirborneScoreTime = 1.0f;
    [SerializeField] private float airborneScoreInterval = 0.1f;
    [SerializeField] private int airborneScore = 50;
    
    private float _lastAirbornePointTime;
    private float _netAirRotation;
    
    private bool _airborne;
    private float _airborneStartTime;
    private int _amountOfFlips;

    private SantaMovement _santaMovement;
    private ScoreManager _scoreManager;

    private void OnEnable()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
        _santaMovement = FindObjectOfType<SantaMovement>();

        _santaMovement.OnAirborne += StartAirborne;
        _santaMovement.OnLand += StopAirborne;
    }

    private void OnDisable()
    {
        _santaMovement.OnAirborne -= StartAirborne;
        _santaMovement.OnLand -= StopAirborne;
    }

    private void StartAirborne()
    {
        _airborneStartTime = Time.time;
        _airborne = true;
        _netAirRotation = 0f;

    }

    private void StopAirborne()
    {
        _airborne = false;
        _netAirRotation = 0f;
        _amountOfFlips = 0;
    }

    private void AirbornePoints()
    {
        if (!_airborne) return;
        if (Time.time - _airborneStartTime > minAirborneScoreTime && Time.time - _lastAirbornePointTime > airborneScoreInterval)
        {
            _lastAirbornePointTime = Time.time;
            _scoreManager.ScorePoints(airborneScore);
        }
    }
    
    private void Flips()
    {
        _netAirRotation += playerRigidbody.angularVelocity * Time.deltaTime;

        if(_netAirRotation > 360f)
        {
            _scoreManager.ScorePoints(500);
            _netAirRotation -= 360;
            _amountOfFlips++;
            OnFlip?.Invoke(_amountOfFlips);
        }

        if (_netAirRotation < -360f)
        {
            _scoreManager.ScorePoints(500);
            _netAirRotation += 360;
            _amountOfFlips++;
            OnFlip?.Invoke(_amountOfFlips);
        }
    }
    
    private void Update()
    {
        AirbornePoints();
        Flips();
    }
}
