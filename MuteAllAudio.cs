using UnityEngine;

public class MuteAllAudio : MonoBehaviour
{
    public bool isMuted = false;

    public void MuteAll()
    {
        AudioListener listener = FindObjectOfType<AudioListener>();

        if (listener != null)
        {
            if (!isMuted)
            {
                AudioListener.volume = 0f;
                isMuted = true;
            }
            else
            {
                AudioListener.volume = 1f;
                isMuted = false;
            }
        }
    }
}