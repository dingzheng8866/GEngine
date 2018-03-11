using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTest : MonoBehaviour {

    public string tag = "";

    private Button Button1;

    // Use this for initialization
    void Start () {
        Debug.Log(tag+"-->" + Screen.width +":"+Screen.height);
        Button1 = GetComponent<Button>();
        //Button1.onClick.AddListener(() => Debug.Log("Click hidden close Button!"+tag));

        Button1.onClick.AddListener(delegate () {
            this.OnClick();
        });
    }

    public void OnClick()
    {
        Debug.Log("Click hidden close Button!" + tag);
        if(tag == "rootwin")
        {
            Transform tf = transform.Find("wincontainer");
            if(tf!=null)
            {
                if (tf.gameObject.activeSelf)
                {
                    tf.gameObject.SetActive(false);
                }
                else
                {
                    tf.gameObject.SetActive(true);
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetMouseButton(0))
        //{
            //Debug.Log(tag+"-------" + Input.mousePosition);
        //}
	}
}
