using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Common.Audio;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Action_PlayRandom_SFX : Instruction
{
    public List<AudioClip> soundClips;
    [SerializeField] private bool m_WaitToComplete;
    [SerializeField] private AudioConfigSoundEffect m_Config = new AudioConfigSoundEffect();

    protected override async Task Run(Args args)
    {
        if (soundClips == null || soundClips.Count == 0)
        {
            Debug.LogWarning("No sound clips assigned");
            return;
        }

        var audioClip = soundClips[Random.Range(0, soundClips.Count)];
 
        
        if (audioClip == null) return;
        if (this.m_WaitToComplete)
        {
            await AudioManager.Instance.SoundEffect.Play(audioClip, this.m_Config, args);
        }
        else
        {
            _ = AudioManager.Instance.SoundEffect.Play(audioClip, this.m_Config, args);
        }
    }
}