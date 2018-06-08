using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioAdd {
    [Tooltip("要播放的各种音乐片段")]
    public AudioClip clip;
    [Tooltip("音乐音量")]
    [Range(0, 1)] public float volume;

}
