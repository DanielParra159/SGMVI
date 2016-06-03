using UnityEngine;

namespace Common.Utils
{

    public class AutoPauseParticles : MonoBehaviour
    {

        protected Pausable m_pausable;

        void Awake()
        {
            m_pausable = new Pausable(OnPause, OnResume);
        }

        void Update()
        {
            if (m_pausable.Check()) return;
        }

        public void OnPause()
        {
            ParticleSystem[] particles = gameObject.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < particles.Length; ++i)
            {
                particles[i].Pause();
            }
            ParticleSystem[] particles2 = gameObject.GetComponents<ParticleSystem>();
            for (int i = 0; i < particles2.Length; ++i)
            {
                particles2[i].Pause();
            }
        }
        public void OnResume()
        {
            ParticleSystem[] particles = gameObject.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < particles.Length; ++i)
            {
                particles[i].Play();
            }
            ParticleSystem[] particles2 = gameObject.GetComponents<ParticleSystem>();
            for (int i = 0; i < particles2.Length; ++i)
            {
                particles2[i].Play();
            }
        }
    }
}