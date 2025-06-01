using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentPickups = 0;   // Tracks collected pickups
    public int maxPickups = 5;       // Total pickups in the scene
    public Text pickupCounter;       // UI element showing pickup progress
    public AudioSource[] audioSources; // Array of all sound sources in scene
    public GameObject player;        // Reference to player
    public float audioProximity = 5f; // Distance within which sounds should play

    public bool levelComplete = false; // Tracks level completion

    void Start()
    {
        UpdateGUI();
    }

    void Update()
    {
        PlayAudioSamples();
    }

    public void UpdateGUI()
    {
        pickupCounter.text = $"Pickups: {currentPickups}/{maxPickups}";
    }

    void PlayAudioSamples()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source != null && source.gameObject != null)
            {
                if (Vector3.Distance(player.transform.position, source.transform.position) <= audioProximity)
                {
                    if (!source.isPlaying)
                    {
                        source.Play();
                    }
                }
            }
        }
    }

    public void LevelComplete()
    {
        Debug.Log("Level completed!");
        levelComplete = true;
    }
}