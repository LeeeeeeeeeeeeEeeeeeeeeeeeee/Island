using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgm_player;
    public AudioSource sfx_player;

    public AudioClip[] bgm_clips;
    public AudioClip[] audio_clips;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        int i = Random.Range(0, bgm_clips.Length);
        bgm_player.clip = bgm_clips[i];
        bgm_player.Play();
    }

    public void PlaySound(string type)
    {
        int index = 0;

        switch (type)
        {
            case "UI": index = Random.Range(0,3); break;
            case "Buy": index = 3; break;
            case "Touch": index = Random.Range(4, 6); break;
            case "Mill": index = 6; break;
            case "Shop": index = 7; break;
            case "Cook": index = 8; break;
            case "Snack": index = Random.Range(9, 11); break;
            case "Inter": index = Random.Range(11, 17); break;
            case "Pat": index = Random.Range(17, 19); break;
            case "Clap": index = Random.Range(19, 27); break;
            case "Boom": index = Random.Range(27, 30); break;
            case "Food": index = Random.Range(30, 33); break;
            case "Milk": index = Random.Range(33, 36); break;
            case "Sugar": index = Random.Range(36, 40); break;

        }


        GameObject NewSound = Instantiate(sfx_player.gameObject, gameObject.transform);
        NewSound.GetComponent<AudioSource>().clip = audio_clips[index];
        NewSound.GetComponent<AudioSource>().Play();
    }
}
