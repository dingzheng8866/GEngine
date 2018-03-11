#if !(UNITY_5 || UNITY_2017)
using UnityEngine;
using System.Collections;

/// <summary>
/// 用于AssetBundle打包Shader
/// </summary>
public class ResourceDepShaders : MonoBehaviour
{
    public Shader[] Shaders;
}
#endif
