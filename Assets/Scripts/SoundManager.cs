using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSrc;

    private void Awake() => audioSrc = GetComponent<AudioSource>();
    


    public void PlaySound(int index, float volume = 1f, float pitchMin = 1f, float pitchMax = 1f, bool stopCurrent = false, bool playIfNotAlreadyPlaying = false)
    {
        if (index < 0 || index >= sounds.Length)
        {
            Debug.LogWarning("Sound index is out of range.");
            return;
        }

        AudioClip clip = sounds[index];
        if (stopCurrent)
        {
            audioSrc.Stop();
            if (playIfNotAlreadyPlaying)
                return;
        }

        if (playIfNotAlreadyPlaying && audioSrc.isPlaying)
            return;

        audioSrc.volume = volume;
        audioSrc.pitch = Random.Range(pitchMin, pitchMax);
        audioSrc.PlayOneShot(clip, volume);
    }
}

[System.Serializable]
public class SoundArrays
{
    public AudioClip[] soundArray;
}