using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musiques;
    List<int> musiquesPlayed= new List<int>();
    AudioSource audSo;

    private void Awake()
    {
        audSo = GetComponent<AudioSource>();
        StartCoroutine(PlaySongs());
    }

    IEnumerator PlaySongs()
    {
        if (musiquesPlayed.Count == 0)
        {
            for (int i = 0; i < musiques.Length; i++)
            {
                musiquesPlayed.Add(i);
            }
        }
            int potato = Random.Range(0, musiquesPlayed.Count);
            audSo.clip = musiques[musiquesPlayed[potato]];
            musiquesPlayed.RemoveAt(potato);
            audSo.Play();

        yield return new WaitForSeconds(audSo.clip.length);

        StartCoroutine(PlaySongs());

    }

}
