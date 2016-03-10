using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

    public AudioSource audio;

    static bool AudioBegin = false;

    void Awake()
    {
        if (!AudioBegin) {
            audio.Play();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
        }
    }
}
