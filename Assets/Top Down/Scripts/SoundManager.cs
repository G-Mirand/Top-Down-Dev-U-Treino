using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.GraphicsBuffer;

public enum SoundType
{
    //Os tipo de son no jogo
    MUSICA,
    ARCO,
    PASSOS,

}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
        
        [SerializeField] private SoundsSO SO;
        private static SoundManager instance = null;
        private AudioSource audioSource;

        private void Awake()
        {
            if (!instance)
            {
                instance = this;
                audioSource = GetComponent<AudioSource>();
            }
        }

        public static void PlaySound(SoundType sound, AudioSource source = null, float volume = 1)
        {
            SoundList soundList = instance.SO.sounds[(int)sound];
            AudioClip[] clips = soundList.sounds;
            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

            if (source)
            {
                source.outputAudioMixerGroup = soundList.mixer;
                source.clip = randomClip;
                source.volume = volume * soundList.volume;
                source.Play();
            }
            else
            {
                instance.audioSource.outputAudioMixerGroup = soundList.mixer;
                instance.audioSource.PlayOneShot(randomClip, volume * soundList.volume);
            }
        }
    }

#if UNITY_EDITOR
[CustomEditor(typeof(SoundsSO))]
public class SoundsSOEditor : Editor
{
    private void OnEnable()
    {
        ref SoundList[] soundList = ref ((SoundsSO)target).sounds;

        if (soundList == null)
            return;

        string[] names = Enum.GetNames(typeof(SoundType));
        bool differentSize = names.Length != soundList.Length;

        Dictionary<string, SoundList> sounds = new();

        if (differentSize)
        {
            for (int i = 0; i < soundList.Length; ++i)
            {
                sounds.Add(soundList[i].name, soundList[i]);
            }
        }

        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            string currentName = names[i];
            soundList[i].name = currentName;
            if (soundList[i].volume == 0) soundList[i].volume = 1;

            if (differentSize)
            {
                if (sounds.ContainsKey(currentName))
                {
                    SoundList current = sounds[currentName];
                    UpdateElement(ref soundList[i], current.volume, current.sounds, current.mixer);
                }
                else
                    UpdateElement(ref soundList[i], 1, new AudioClip[0], null);

                static void UpdateElement(ref SoundList element, float volume, AudioClip[] sounds, AudioMixerGroup mixer)
                {
                    element.volume = volume;
                    element.sounds = sounds;
                    element.mixer = mixer;
                }
            }
        }
    }
}
#endif

    [Serializable]
    public struct SoundList
    {
        [HideInInspector] public string name;
        [Range(0, 1)] public float volume;
        public AudioMixerGroup mixer;
        public AudioClip[] sounds;
    }

