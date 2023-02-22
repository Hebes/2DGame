using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class TextureSetting : AssetPostprocessor
{
    private void OnPreprocessTexture()
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.mipmapEnabled = false;
        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.filterMode = FilterMode.Point;
        //设置图片的压缩格式为无损压缩
        var setting = new TextureImporterPlatformSettings();
        setting.textureCompression = TextureImporterCompression.Uncompressed;
        textureImporter.SetPlatformTextureSettings(setting);
        Debug.Log(assetPath);
    }
}
