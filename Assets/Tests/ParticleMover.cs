using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMover : MonoBehaviour {
	public ParticleSystem particleSystem;
	// Update is called once per frame
	ParticleSystem.Particle[] particles;
	public float particleSpeed;
	private void Start() {
		particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
	}
	private void LateUpdate() {
		int numParticles = particleSystem.GetParticles(particles);
		for(int i = 0; i < numParticles; i++)
		{
			particles[i].velocity = (Random.insideUnitSphere * 5 + transform.position - particles[i].position).normalized * particleSpeed;
		}
		particleSystem.SetParticles(particles, numParticles);
	}
}
