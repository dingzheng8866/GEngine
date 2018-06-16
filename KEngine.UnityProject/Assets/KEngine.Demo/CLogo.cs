using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace GEngine.UI
{
    public class CLogo : MonoBehaviour
    {
        Transform logo = null;         Image logoImage = null; 	    // Use this for initialization 	    void Start () {             logo = transform.Find("logo");
            logoImage = logo.GetComponent<Image>();
            logo.gameObject.SetActive(false);
        }          public IEnumerator PlayLogoAnimation()
        {
            yield return new WaitForSeconds(0.5f);
            logo.gameObject.SetActive(true);
            logoImage.CrossFadeAlpha(0, 0f, true);
            logoImage.CrossFadeAlpha(1, 0.5f, true);
            yield return new WaitForSeconds(1f);
            logoImage.CrossFadeAlpha(0, 0.5f, true);
            logo.gameObject.SetActive(false);
        }
    }
}
