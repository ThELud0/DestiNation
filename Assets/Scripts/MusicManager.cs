using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource Timbales;
    [SerializeField] AudioSource Basse;
    [SerializeField] AudioSource LocoBase;
    void Start()
    {
        Timbales.Play();
        Basse.Play();
        LocoBase.Play();

        StartCoroutine(TestSounds());
    }

    // Update is called once per frame
    private IEnumerator TestSounds(){
        Timbales.volume = 0;
        Basse.volume = 0;

        yield return new WaitForSeconds(5);
        Basse.volume = 1;

        yield return new WaitForSeconds(5);
        Timbales.volume = 1;
        yield return null;
    }
}
