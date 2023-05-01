using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class TrickDetector : MonoBehaviour
{
    private SantaMovement santaMovement;

    //Air time
    //Super air time
    //Front & back flip
    //Front & back wheelie
    [SerializeField] private float minAirborneScoreTime = 1.0f;
    private float lastAirbornePointTime;
    private float lastSuperAirbornePointTime;

    [SerializeField] private float airborneScoreInterval = 0.1f;
    [SerializeField] private float superAirborneScoreInterval = 0.1f;

    private float netAirRotation = 0f;

    [SerializeField] private int superAirbornePoints = 100;
    [SerializeField] private int airborneScore = 50;

    private bool airborne;
    private float airborneStartTime;

    private Camera camera;

    private void OnEnable()
    {
        santaMovement = GetComponent<SantaMovement>();
        camera = Camera.main;

        santaMovement.OnAirborne += StartAirborne;
        santaMovement.OnLand += StopAirborne;
    }

    private void OnDisable()
    {
        santaMovement.OnAirborne -= StartAirborne;
        santaMovement.OnLand -= StopAirborne;
    }

    private void StartAirborne()
    {
        airborneStartTime = Time.time;
        airborne = true;
        netAirRotation = 0f;

    }

    private void StopAirborne()
    {
        airborne = false;
        netAirRotation = 0f;
    }

    private void AirbornePoints()
    {
        if (!airborne) return;
        if (Time.time - airborneStartTime > minAirborneScoreTime && Time.time - lastAirbornePointTime > airborneScoreInterval)
        {
            lastAirbornePointTime = Time.time;
            ScoreManager.ScorePoints(airborneScore);
        }
    }

    bool SuperAirBorne => transform.position.y > camera.ViewportToWorldPoint(Vector3.one).y;
    private void SuperAirBornePoints()
    {
        if (SuperAirBorne && Time.time - lastSuperAirbornePointTime > superAirborneScoreInterval)
        {
            lastSuperAirbornePointTime = Time.time;
            ScoreManager.ScorePoints(superAirbornePoints);
        }
    }

    private void Flips()
    {
        netAirRotation += GetComponent<Rigidbody2D>().angularVelocity * Time.deltaTime;

        if(netAirRotation > 360f)
        {
            ScoreManager.ScorePoints(500);
            netAirRotation -= 360;
        }

        if (netAirRotation < -360f)
        {
            ScoreManager.ScorePoints(500);
            netAirRotation += 360;
        }
    }


    private void Update()
    {
        AirbornePoints();
        SuperAirBornePoints();
        Flips();
    }
}
