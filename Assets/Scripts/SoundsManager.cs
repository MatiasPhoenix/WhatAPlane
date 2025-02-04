using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [Header("AudioSources")]
    public AudioSource[] audioSources;


    [Header("Sounds")]
    public AudioClip[] soundsFX;
    public AudioClip[] soundsFlightFX;
    public AudioClip[] soundsShootingFX;
    public AudioClip[] hittedFX;


    [Header("Flight Sounds Managers")]
    public AudioSource flightSoundP1;
    public AudioSource flightSoundP2;

    //Private variables
    bool planeHitted = false;

    //Declare Singleton instance
    public static SoundsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSources = GetComponents<AudioSource>();
    }

    public void PlayExplosion(string planePlayer)
    {
        int numExplosion = Random.Range(0, soundsFX.Length - 1);
        audioSources[0].PlayOneShot(soundsFX[numExplosion]);
        
        StopSound(planePlayer);
    }

    public void PlayPlaneHitted(string planePlayer)
    {
        audioSources[0].PlayOneShot(soundsFX[2]);


        if (planeHitted) return;
        planeHitted = true;
        StopSound(planePlayer);
        if (planePlayer == "Player1" || planePlayer == "Player1(clone)")
        {
            audioSources[1].PlayOneShot(hittedFX[0]);
            // Debug.Log($"PlayPlaneHitted --> {hittedFX[0]}");

        }else if (planePlayer == "Player2" || planePlayer == "Player2(clone)")
        {
            audioSources[2].PlayOneShot(hittedFX[0]);
            // Debug.Log($"PlayPlaneHitted --> {hittedFX[0]}");
        }
    }

    public void PlayShoot()
    {
        int numShoot = Random.Range(0, soundsShootingFX.Length);
        audioSources[0].PlayOneShot(soundsShootingFX[numShoot]);
    }

    public void PlayFlightSound(string planePlayer)
    {
        // Debug.Log($"---Riproducendo suono {planePlayer}");

        int numFlight = Random.Range(0, soundsFlightFX.Length);
        if (planePlayer == "Player1" || planePlayer == "Player1(clone)")
        {
            audioSources[1].PlayOneShot(soundsFlightFX[numFlight]);
            // Debug.Log($"PlayFlightSound --> {soundsFlightFX[numFlight]}");

        }
        else if (planePlayer == "Player2" || planePlayer == "Player2(clone)")
        {
            audioSources[2].PlayOneShot(soundsFlightFX[numFlight]);
            // Debug.Log($"PlayFlightSound --> {soundsFlightFX[numFlight]}");

        }
    }



    public void StopSound(string planePlayer)
    {
        
        if (planePlayer == "Player1" || planePlayer == "Player1(clone)")
        {
            audioSources[1].Stop();
        }
        else if (planePlayer == "Player2" || planePlayer == "Player2(clone)")
        {
            audioSources[2].Stop();
        }
    }


}
