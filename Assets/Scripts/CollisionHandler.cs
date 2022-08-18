using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem Deathvfx;
    [SerializeField] int loaddelay = 1;
    public AudioSource source;
    public AudioClip clip;
    [SerializeField] MeshRenderer[] parts;
    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        Deathvfx.Play();
        if (source.isPlaying) { return; }
        source.PlayOneShot(clip);
        foreach (MeshRenderer part in parts)
        {
            part.enabled = false;
        }
        // GetComponent<Rigidbody>().AddForce(0,500000000,0);
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", loaddelay);

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
