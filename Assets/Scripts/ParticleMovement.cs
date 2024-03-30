using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovement : MonoBehaviour
{
    // Parameters
    public float windForce = 10.0f;
    public float viscosity = 0.1f;
    public float density = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Get all active particles
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[GetComponent<ParticleSystem>().particleCount];
        int particleCount = GetComponent<ParticleSystem>().GetParticles(particles);

        // Get current time
        float currentTime = Time.time;

        // Update particle velocities based on Navier-Stokes equation
        for (int i = 0; i < particleCount; i++)
        {
            Vector3 position = particles[i].position;
            float x = position.x;
            float y = position.y;
            float z = position.z;

            // Compute wind velocity components
            float u = windForce / density * (x - currentTime) * viscosity;
            float v = windForce / density * (y - currentTime) * viscosity;
            float w = windForce / density * (z - currentTime) * viscosity;

            particles[i].velocity = new Vector3(u, v, w);
        }

        // Apply updated velocities to particles
        GetComponent<ParticleSystem>().SetParticles(particles, particleCount);
    }
}
