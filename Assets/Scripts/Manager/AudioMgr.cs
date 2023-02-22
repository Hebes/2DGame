/****************************************************
    文件：AudioMgr.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/25 14:56:10
	功能：游戏音乐播放管理器
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LitJson;
using System.IO;

public class AudioMgr : MonoSingleTon<AudioMgr>, IInit
{
    private AudioSource _bgSource;
    private AudioSource _playerOnceSource;
    private List<AudioSource> _activeAudioSource = new List<AudioSource>();
    private List<AudioSource> _inactiveAudioSource = new List<AudioSource>();
    private Dictionary<string, AudioSource> _playingAudioSource = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioClip> _audioClipDic;
    private Dictionary<string, float> _audioClipVolumeDic;

    private float _globalVolumeScale = 1;
    private float _soundVolumeScale = 1;//普通音量大小
    private float _bgVolumeScale = 1;//背景音乐大小

    public float SoundVolumeScale
    {
        get => _soundVolumeScale;
        set
        {
            var scale = Mathf.Clamp01(value);
            _soundVolumeScale = scale;
            SetAllSoundAudioVolume(_soundVolumeScale * GlobalVolumeScale);
            GameDataMgr.Instance.AudioData.soundVolume = _soundVolumeScale;
            PlayUIMusic(E_UIMusic.SliderChange);
        }
    }
    public float BgVolumeScale
    {
        get => _bgVolumeScale;
        set
        {
            var scale = Mathf.Clamp01(value);
            _bgVolumeScale = scale;
            SetBgAudioVolume(_bgVolumeScale * GlobalVolumeScale);
            GameDataMgr.Instance.AudioData.bgVolume = _bgVolumeScale;
            PlayUIMusic(E_UIMusic.SliderChange);
        }
    }
    public float GlobalVolumeScale
    {
        get => _globalVolumeScale;
        set
        {
            var scale = Mathf.Clamp01(value);
            _globalVolumeScale = scale;
            SetAllSoundAudioVolume(_soundVolumeScale * GlobalVolumeScale);
            SetBgAudioVolume(_bgVolumeScale * GlobalVolumeScale);
            PlayUIMusic(E_UIMusic.SliderChange);
        }
    }
    public void Init()
    {
        InitAudioSource();
        InitAudioClipData(Paths.AUDIO_FOLIDER);
        InitAudioVolumeData(Paths.CFG_AUDIO);
        InitVolumeScale();
    }
    //重置音频大小
    public void ResetVolumeScale()
    {
        SoundVolumeScale = 0.7f;
        BgVolumeScale = 1f;
        GlobalVolumeScale = 1f;
    }
    public void SaveVolumeScale()
        => GameDataMgr.Instance.SaveAudioVolumeData();
    private void InitVolumeScale()
    {
        var audioData = GameDataMgr.Instance.AudioData;
        _bgVolumeScale = (float)audioData.bgVolume;
        _soundVolumeScale = (float)audioData.soundVolume;
    }
    private void InitAudioSource()
    {
        _bgSource = gameObject.AddComponent<AudioSource>();
        _bgSource.loop = true;
        _playerOnceSource = gameObject.AddComponent<AudioSource>();
    }
    //初始化AudioClip
    private void InitAudioClipData(string path)
    {
        _audioClipDic = new Dictionary<string, AudioClip>();
        var clips = ResMgr.Instance.LoadAllRes<AudioClip>(path);
        foreach (var clip in clips)
            _audioClipDic[clip.name] = clip;
    }
    //初始化音乐大小
    private void InitAudioVolumeData(string filePath)
    {
        _audioClipVolumeDic = new Dictionary<string, float>();
        JsonData jsonData = JsonMapper.ToObject(File.ReadAllText(filePath));
        //通过所用的方式获取json中的信息
        foreach (JsonData item in jsonData)
        {
            string name = item["name"].ToString();
            float volume = float.Parse(item["volume"].ToString());
            _audioClipVolumeDic[name] = volume;
        }
    }
    private void PlayBG(string clipName)
    {
        var clip = GetClip(clipName);
        if (_bgSource.clip != clip || _bgSource.clip == null)
            _bgSource.clip = clip;
        SetAudioVolume(clipName, _bgSource, true);
        _bgSource.Play();
    }
    public void PauseBg() => _bgSource.Pause();
    public void ContinueBg() => _bgSource.UnPause();
    public void PlayBgMusic(E_BgMusic bgMusic)
        => PlayBG(bgMusic.ToString());
    public void PlayBattleMusic(E_BattleMusic battleMusic)
        => PlayBG(battleMusic.ToString());
    public void Play(string clipName, bool isLoop = false)
    {
        //todo这里可能写的有一些问题
        if (GetPlayingSource(clipName) != null)
            return;
        var audio = GetAudioSource();
        var clip = GetClip(clipName);
        //添加进该列表用于暂停
        audio.loop = isLoop;
        audio.clip = clip;
        SetAudioVolume(clipName, audio);
        //如果不暂停 则通过协程去关闭
        audio.Play();
        _playingAudioSource[clipName] = audio;
        if (!isLoop)
            CoroutineMgr.Instance.ExcuteOne(Wait(clipName));
    }
    //判断音频是否在播放
    public bool IsPlaying(string clipName)
    {
        var audio = GetAudioSource();
        if (audio != null)
            return audio.isPlaying;
        return false;
    }
    public void Stop(string clipName)
    {
        var source = GetPlayingSource(clipName);
        if (source == null) return;
        source.Stop();
        source.clip = null;
        source.loop = false;
        _activeAudioSource.Remove(source);
        _inactiveAudioSource.Add(source);
        _playingAudioSource.Remove(clipName);
    }
    //立即暂停
    public void PauseNow(string clipName)
    {
        var source = GetPlayingSource(clipName);
        if (source == null) return;
        source.Pause();
    }
    //延迟暂停
    public void PauseDelay(string clipName)
    {
        var source = GetPlayingSource(clipName);
        if (source == null) return;
        source.loop = false;
    }
    //重新播放
    public void ContinuePlay(string clipName, bool isLoop = true)
    {
        var source = GetPlayingSource(clipName);
        if (source == null) return;
        source.loop = isLoop;
        source.UnPause();//暂停就再播放就是接着上次暂停的地方继续播放
    }
    public void Replay(string clipName, bool isLoop = true)
    {
        var source = GetPlayingSource(clipName);
        if (source == null) return;
        source.loop = isLoop;
        source.Play();//暂停就再播放就是接着上次暂停的地方继续播放
    }
    private IEnumerator Wait(string clipName)
    {
        float length = GetClip(clipName).length;
        yield return new WaitForSeconds(length);
        Stop(clipName);
    }
    public void PlayOnce(string clipName)
    {
        var clip = GetClip(clipName);
        var volume = GetVolume(clipName) * SoundVolumeScale * GlobalVolumeScale;
        _playerOnceSource.PlayOneShot(clip, volume);
    }
    private AudioClip GetClip(string clipName)
    {
        AudioClip clip = null;
        if (!_audioClipDic.TryGetValue(clipName, out clip))
            Debug.LogError($"未找到名为:{clipName}的AudioClip");
        return clip;
    }
    private float GetVolume(string clipName)
    {
        if (!_audioClipVolumeDic.TryGetValue(clipName, out float volume))
            Debug.LogError($"未找到名为:{clipName}的音频对应的音量大小");
        return volume;
    }
    private AudioSource GetAudioSource()
    {
        AudioSource audio = null;
        if (_inactiveAudioSource.Count > 0)
        {
            audio = _inactiveAudioSource[0];
            _inactiveAudioSource.RemoveAt(0);
        }
        else
            audio = gameObject.AddComponent<AudioSource>();
        _activeAudioSource.Add(audio);
        return audio;
    }
    //得到正在播放的音频组件
    private AudioSource GetPlayingSource(string clipName)
    {
        AudioSource source;
        _playingAudioSource.TryGetValue(clipName, out source);
        return source;
    }
    private void SetAudioVolume(string clipName, AudioSource source, bool isBgAudio = false)
    {
        float volume = GetVolume(clipName);
        source.volume = volume * (isBgAudio ? BgVolumeScale : SoundVolumeScale) * GlobalVolumeScale;
    }
    //设置所有音效音源组件的大小
    private void SetAllSoundAudioVolume(float soundVolumeScale)
    {
        foreach (KeyValuePair<string, AudioSource> kv in _playingAudioSource)
        {
            float volume = GetVolume(kv.Key);
            float setVolumeScale = volume * soundVolumeScale;
            if (kv.Value.volume != setVolumeScale)
                kv.Value.volume = setVolumeScale;
        }
    }
    //设置背景音乐音源组件的大小
    private void SetBgAudioVolume(float bgVolumeScale)
    {
        if (_bgSource.clip == null)
            return;
        float volume = GetVolume(_bgSource.clip.name);
        float setVolumeScale = volume * bgVolumeScale;
        if (_bgSource.volume != setVolumeScale)
            _bgSource.volume = setVolumeScale;
    }
    public void PlayUIMusic(E_UIMusic type)
    {
        switch (type)
        {
            case E_UIMusic.ChooseArchive:
                PlayOnce("ui_btn_ChooseArchive");
                break;
            case E_UIMusic.NormalClick:
                PlayOnce("ui_btn_NormalClick");
                break;
            case E_UIMusic.NotClick:
                PlayOnce("ui_btn_NotClick");
                break;
            case E_UIMusic.SliderChange:
                PlayOnce("ui_Slider_Change");
                break;
            case E_UIMusic.Enter:
                PlayOnce("ui_Enter");
                break;
            case E_UIMusic.ToggleClick:
                PlayOnce("ui_Toggle_Click");
                break;
            case E_UIMusic.None:
                break;
        }
    }
    public void PauseAllAudio()
    {
        foreach (var souce in _playingAudioSource)
            souce.Value.Pause();
        _bgSource.Pause();
        //_playerOnceSource.Pause();
    }
    public void ContinueAllAudio()
    {
        foreach (var souce in _playingAudioSource)
            souce.Value.UnPause();
        _bgSource.UnPause();
        //_playerOnceSource.UnPause();
    }
}