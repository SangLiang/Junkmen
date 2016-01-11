using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public AudioSource efxSourse;
    public AudioSource musicSourse;

    public static SoundManager instance = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;


    void Awake() {
        if (instance == null)
            instance = this;
        if (instance != this) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
    }


    public void PlaySingle(AudioClip clip) {
        efxSourse.clip = clip;
        efxSourse.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);
        float randmPitch = Random.Range(lowPitchRange,highPitchRange);
        efxSourse.pitch = randmPitch;
        efxSourse.clip = clips[randomIndex];
        efxSourse.Play();

    
    }

	
}
