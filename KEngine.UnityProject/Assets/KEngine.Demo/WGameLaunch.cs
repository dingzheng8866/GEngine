using System.Collections; using System.Collections.Generic; using UnityEngine; using DG.Tweening; using DG.Tweening.Plugins.Options; using UnityEngine.UI;  public class WGameLaunch : MonoBehaviour {      Transform logo = null;     Image logoImage = null; 	// Use this for initialization 	void Start () {         logo = transform.Find("logo");
        logoImage = logo.GetComponent<Image>();
        logo.gameObject.SetActive(false);
        //img.CrossFadeAlpha(0, 0f, true);
        //img.CrossFadeAlpha(1, 0.2f, true);
        //DOTween.To(() => logo.localScale, x => logo.localScale = x, new Vector3(0.9f, 0.9f, 0.9f), 0.8f);
        //StartCoroutine(PlayLogoAnimation());
    }       public IEnumerator PlayLogoAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        logo.gameObject.SetActive(true);
        logoImage.CrossFadeAlpha(0, 0f, true);
        logoImage.CrossFadeAlpha(1, 0.5f, true);
        yield return new WaitForSeconds(1f);
        logoImage.CrossFadeAlpha(0, 0.5f, true);
        logo.gameObject.SetActive(false);
    } } 