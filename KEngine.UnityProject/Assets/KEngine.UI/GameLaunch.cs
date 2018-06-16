using KEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GEngine.UI;

namespace GEngine
{
    public class GameLaunch : MonoBehaviour
    {

        // Use this for initialization
        void SetScreenSetting()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Debug.Log("SetScreenSetting");
        }

        void Awake()
        {
            SetScreenSetting();
            StartCoroutine(LoadUIAsset());
        }

        public IEnumerator LoadUIAsset()
        {
            string path = string.Format("UI/{0}.prefab", "WGameLaunch");

            var assetLoader = StaticAssetLoader.Load(path);

            while (!assetLoader.IsCompleted)
                yield return null;

            GameObject go = assetLoader.TheAsset as GameObject;
            GameObject win = GameObject.Instantiate(go);

            GameObject p = GameObject.Find("UIRootWindow/UIAutoSizer");
            win.transform.SetParent(p.transform, false);
            win.SetActive(true);
            WGameLaunch wg = win.GetComponent<WGameLaunch>();
            wg.StartCoroutine(wg.Launch());
        }
    }
}

