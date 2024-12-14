using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BGMUSIC : MonoBehaviour
{
    // Singleton instance of BGMUSIC
    public static BGMUSIC instance;

    // List of audio clips to be used in the game
    [SerializeField] private List<AudioClip> audioClips = new();

    // Dictionary for quick lookup of audio clips by name
    private Dictionary<string, AudioClip> audioClipDict;

    // AudioSource component for playing audio
    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;

            // Ensure this GameObject persists across scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy duplicate instances
            Destroy(gameObject);
            return;
        }

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Populate the dictionary with audio clips for quick lookup
        audioClipDict = audioClips.ToDictionary(clip => clip.name, clip => clip);
    }

    /// <summary>
    /// Plays a sound effect by its name.
    /// </summary>
    /// <param name="str">The name of the audio clip to play.</param>
    public void PlaySfx(string str)
    {
        // Check if the clip exists in the dictionary
        if (audioClipDict.TryGetValue(str, out AudioClip audioClip))
        {
            // Play the audio clip without interrupting other sounds
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            // Log a warning if the audio clip is not found
            Debug.LogWarning($"AudioClip with name '{str}' not found.");
        }
    }
}
