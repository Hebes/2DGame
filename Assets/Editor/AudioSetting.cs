/****************************************************
    文件：AudioSetting.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/25 15:30:24
	功能：导入音频设置
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class AudioSetting : AssetPostprocessor
{
    private void OnPostprocessAudio(AudioClip clip)
    {
        var audioImporter =(AudioImporter)assetImporter;
        AudioImporterSampleSettings setting = new AudioImporterSampleSettings();
        //如果大于1s
        if(clip.length>1)
            setting.loadType = AudioClipLoadType.Streaming;
        else
            setting.loadType = AudioClipLoadType.DecompressOnLoad;
        audioImporter.preloadAudioData = false;
        audioImporter.defaultSampleSettings = setting;
    }
}