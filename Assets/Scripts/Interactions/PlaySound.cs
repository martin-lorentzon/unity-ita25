using UnityEngine;
public class PlaySound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.Play();
        //audioSource.loop = true;
    }
}
