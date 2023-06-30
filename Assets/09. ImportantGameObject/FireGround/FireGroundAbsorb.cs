using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGroundAbsorb : MonoBehaviour
{
    public Transform target; // 目標座標
    public ParticleSystem _particleSystem; // 粒子系統
    public float convergenceSpeed = 5f; // 匯聚速度

    private ParticleSystem.Particle[] particles; // 粒子陣列

    private void Start()
    {
        particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
    }

    private void Update()
    {
        int particleCount = _particleSystem.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            // 使用 Lerp 函式讓粒子匯聚到目標座標
            particles[i].position = Vector3.Lerp(particles[i].position, target.position, convergenceSpeed * Time.deltaTime);
        }

        _particleSystem.SetParticles(particles, particleCount);
    }
}
