using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public AudioSource _handAudioSource, _footAudioSource;
    public AudioClip _swingAudio, _stepAudio;

	// Use this for initialization
	void Start () {
		
	}
	
	public void Swing()
    {
        _handAudioSource.clip = _swingAudio;
        _handAudioSource.Play();
    }

    public void Step()
    {
        //_footAudioSource.clip = _stepAudio;
        if(_footAudioSource != null)
        {
            _footAudioSource.PlayOneShot(_stepAudio);
        }
       
    }

    public void AudioStop()
    {
        _footAudioSource.Stop();
    }
}
