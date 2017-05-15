using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour {

    public Sprite greenSprite;
    public Sprite redSprite;

    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setLight(string color)
    {
        if (color == "red")
        {
            sr.sprite = redSprite;
        } else if (color == "green")
        {
            sr.sprite = greenSprite;
        }
    }
}
