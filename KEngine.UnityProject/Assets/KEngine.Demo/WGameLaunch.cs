using System.Collections; using System.Collections.Generic; using UnityEngine; using DG.Tweening; using DG.Tweening.Plugins.Options; using UnityEngine.UI; using KEngine; 
namespace GEngine.UI
{
    public class WGameLaunch : MonoBehaviour
    {         CLogo logo = null;         CLoadBar loadBar = null;          //private AsyncOperation operation;          SceneLoader sceneLoader;          void Awake() {             logo = transform.Find("logo").GetComponent<CLogo>();
            loadBar = transform.Find("loading/loadbar").GetComponent<CLoadBar>();
        }          void Update()
        {
            if(sceneLoader!=null)
            {
                loadBar.SetProgress(sceneLoader.Progress);
                if(sceneLoader.IsCompleted)
                {
                    loadBar.SetProgress(1);
                    sceneLoader = null;
                }
            }
        }          public IEnumerator Launch()
        {
            yield return logo.PlayLogoAnimation();

            //logoImage.CrossFadeAlpha(0, 0f, true);
            //logoImage.CrossFadeAlpha(1, 0.5f, true);
            //yield return new WaitForSeconds(1f);
            //logoImage.CrossFadeAlpha(0, 0.5f, true);
            //logo.gameObject.SetActive(false);

            transform.Find("loading").gameObject.SetActive(true);
            loadBar.enabled = true;

            //yield return new WaitForSeconds(1f);
            sceneLoader = SceneLoader.Load("UI/DemoHome.unity");
            loadBar.SetProgress(sceneLoader.Progress);
            //loadBar.SetProgress(0.5f);
            //yield return new WaitForSeconds(1.5f);
            //loadBar.SetProgress(1f);
        }

    }
}
 