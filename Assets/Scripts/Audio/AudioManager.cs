using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> Sources;

    public AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Play(AudioClip clip)
    {
        foreach(AudioSource source in Sources)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(clip);
                return;
            }
        }

        AudioSource newSource = AddNewSource();
        newSource.PlayOneShot(clip);
    }

    private AudioSource AddNewSource()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        Sources.Add(source);
        return source;
    }

    // Start is called before the first frame update
    void Start()
    {
        Sources.Add(gameObject.AddComponent<AudioSource>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
