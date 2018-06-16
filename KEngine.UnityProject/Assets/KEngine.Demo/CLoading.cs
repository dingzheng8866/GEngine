using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace GEngine.UI
{
    public class CLoading : MonoBehaviour
    {
        CLoadBar loadBar = null;
        void Start()
        {
            loadBar = transform.Find("loadbar").GetComponent<CLoadBar>();
        }

        public void SetProgress(float p)
        {
            loadBar.SetProgress(p);
        }

        /*
        void Update()
        {
            if (aboutToLoading)
            {
                aboutToLoading = false;
                sceneLoader = SceneLoader.Load("");
                loadBar.SetProgress(sceneLoader.Progress);
            }
            else if (sceneLoader != null)
            {
                loadBar.SetProgress(sceneLoader.Progress);
                if (sceneLoader.IsCompleted)
                {
                    loadBar.SetProgress(1);

                    sceneLoader = null;
                }
            }
        }
        */
    }
}
