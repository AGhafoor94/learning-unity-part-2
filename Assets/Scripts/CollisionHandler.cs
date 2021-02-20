using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip collisionAudio;
    [SerializeField] AudioClip successAudio;

    [SerializeField] ParticleSystem collisionParticle, successParticle;

    int currentScene;
    int nextScene;
    bool collisionDisable = false;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextScene = currentScene + 1;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (collisionDisable) return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Take off");
                break;
            case "Finish":
                audioSource.PlayOneShot(successAudio);
                successParticle.Play();
                LoadNextLevel();
                break;
            default:
                audioSource.PlayOneShot(collisionAudio);
                collisionParticle.Play();
                SceneManager.LoadScene(currentScene);
                break;
        }
    }
    void LoadNextLevel()
    {
        if (nextScene == SceneManager.sceneCountInBuildSettings)
            nextScene = 0;
        SceneManager.LoadScene(nextScene);
    }
}
