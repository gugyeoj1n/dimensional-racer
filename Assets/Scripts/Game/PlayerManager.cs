using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private AirplaneController airplaneController;
    private GameManager gameManager;
    public string playerId;

    public float maxFuel;
    public float fuel;
    public float acceleration;

    public ParticleSystem[] particles;
    
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        airplaneController = GetComponent<AirplaneController>();
        particles = GetComponentsInChildren<ParticleSystem>();

        fuel = maxFuel;
    }

    public void InitParticles()
    {
        foreach(ParticleSystem particle in particles)
            particle.Play();
    }

    void Update()
    {
        if (!photonView.IsMine || !gameManager.isStarted) return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Accelerate();
        }
        else
        {
            Decelerate();
        }
    }

    public void StartCamera()
    {
        CameraFollow cam = GetComponent<CameraFollow>();
        cam.StartFollowing();
    }

    private void Accelerate()
    {
        if (fuel <= 0 || airplaneController.speed >= airplaneController.maxSpeed)
            return;

        fuel -= acceleration * 0.5f * Time.deltaTime;
        airplaneController.speed += acceleration * 10f * Time.deltaTime;
        SetParticles(true);
    }

    public void SetParticles(bool up)
    {
        foreach (ParticleSystem particle in particles)
        {
            ParticleSystem.MainModule main = particle.main;
            main.startSpeed = new ParticleSystem.MinMaxCurve(up ? 40f : 20f);
        }
    }

    private void Decelerate()
    {
        if (airplaneController.speed <= airplaneController.minSpeed)
            return;

        airplaneController.speed -= 100f * Time.deltaTime;
        SetParticles(false);
    }
}
