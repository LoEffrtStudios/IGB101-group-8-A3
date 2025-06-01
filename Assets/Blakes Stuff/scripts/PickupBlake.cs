using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float rotationSpeed = 50f;  // Rotation speed (degrees per second)
    public float bobSpeed = 2f;        // Speed of the bobbing motion
    public float bobHeight = 0.5f;     // Maximum height of bobbing

    private Vector3 startPos;          // Initial position for bobbing
    private GameManager gameManager;   // Reference to GameManager
    private AudioSource audioSource;

    public AudioClip pickupSound;  // Assign "Chime" in Inspector

    void Start()
    {
        startPos = transform.position; // Store initial position

        // Find GameManager
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        if (gm != null)
        {
            gameManager = gm.GetComponent<GameManager>();
        }
        else
        {
            Debug.LogError("GameManager NOT FOUND in scene! Ensure it's tagged correctly.");
        }

        // Get AudioSource from pickup object
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource missing from pickup object! Add one in Inspector.");
        }

        // Ensure the correct sound is assigned
        if (pickupSound == null)
        {
            Debug.LogError("Pickup sound not assigned! Assign 'Chime' in Inspector.");
        }
    }

    void Update()
    {
        // Rotate the pickup smoothly
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Bob up and down using a sine wave
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Pickup triggered by: {other.gameObject.name}");

        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is NULL!");
            return;
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player picked up item!");
            gameManager.currentPickups++;
            gameManager.UpdateGUI();

            if (audioSource != null && pickupSound != null)
            {
                Debug.Log("Playing pickup sound: Chime");
                audioSource.PlayOneShot(pickupSound);
            }
            else
            {
                Debug.LogError("AudioSource or pickupSound missing—sound cannot play.");
            }

            Destroy(gameObject, 0.5f); // Slight delay to let sound play before removing
        }
    }
}
