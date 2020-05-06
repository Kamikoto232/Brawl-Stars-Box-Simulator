using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenURL : MonoBehaviour {
    public string URL;

	void Start () {
        GetComponent<Button>().onClick.AddListener(delegate { Click(); });
    }

    private void Click()
    {
        if(!string.IsNullOrEmpty(URL))
        Application.OpenURL(URL);
    }
}