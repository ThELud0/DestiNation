using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] AudioSource Timbales;
    [SerializeField] AudioSource Basse;
    [SerializeField] AudioSource LocoBase;

    private void Awake()
    {
        // Si une instance existe déjà et que ce n'est pas celle-ci, détruisez cet objet
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // Assigner l'instance et la marquer pour persister entre les scènes
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Timbales.Play();
        Basse.Play();
        LocoBase.Play();

        // StartCoroutine(TestSounds());
    }

    public void SetVolumeTimbale(float _volume){
        if (_volume > 1 || _volume < 0){
            Debug.Log("Volume must be between 0 and 1");
        }
        Timbales.volume = _volume;
    }
    public void SetVolumeBasse(float _volume){
        if (_volume > 1 || _volume < 0){
            Debug.Log("Volume must be between 0 and 1");
        }
        Basse.volume = _volume;
    }
    public void SetVolumeLocoBase(float _volume){
        if (_volume > 1 || _volume < 0){
            Debug.Log("Volume must be between 0 and 1");
        }
        LocoBase.volume = _volume;
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
