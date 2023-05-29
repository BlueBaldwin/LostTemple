using UnityEngine;

namespace Audio
{
    public class AudioAnalyzer : MonoBehaviour
    {
        public AudioSource audioSource; 
        float[] samples = new float[512];
        public float Amplitude { get; private set; }

        void Update()
        {
            if (audioSource == null) return;
            
            audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
            Amplitude = 0;
            for (int i = 0; i < samples.Length; i++)
            {
                Amplitude += samples[i] * samples[i];
            }
            Amplitude = Mathf.Sqrt(Amplitude / samples.Length);
        }
    }
}