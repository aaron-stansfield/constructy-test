using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SphereCollider))]
public class NPCProximityAudio : MonoBehaviour
{
    //soo was initially gonna make a small box collider then one outwith that (a larger one) so 2 audio sources dont play at same time 
    //but instead ive handled it using a cooldown timer, random chance, and checking if the audiosource is already playing

    [SerializeField] private AudioClip[] clips;
    private string playerTag = "Player";
    private float coolDown = 20f;
    [Range(0f, 1f)] private float chanceOfPlay = 0.5f;

    private Vector2 Pitch = new Vector2(0.99f, 1.1f);
    private Vector2 Volume = new Vector2(0.8f, 1.0f);

    private AudioSource audioSource;
    private float nextAllowedTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        //we making sure sphere collider is a trigger
        var sphere = GetComponent<SphereCollider>();
        sphere.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        if (Time.time < nextAllowedTime) return;
        if (audioSource.isPlaying) return;
        if (clips == null || clips.Length == 0) return;
        if (Random.value > chanceOfPlay) return;

        audioSource.pitch = Random.Range(Pitch.x, Pitch.y);
        audioSource.volume = Random.Range(Volume.x, Volume.y);

        var chosen = clips[Random.Range(0, clips.Length)];
        audioSource.PlayOneShot(chosen);

        nextAllowedTime = Time.time + coolDown;
    }
}