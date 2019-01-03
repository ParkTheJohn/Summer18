using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Image [] images;
    public AudioClip pressSound;

    private AudioSource audioSource;
    public List<KeyLogPress> keyLog = new List<KeyLogPress>();

	// Use this for initialization
	void Start ()
    {

        images[0].enabled = false;
        images[1].enabled = false;
        images[2].enabled = false;
        images[3].enabled = false;

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = pressSound;

        keyLog.Clear();
    }

	// Update is called once per frame
    void Update () {
   
        // good lord this is awful code

		if (Input.GetKey(KeyCode.Z))
        {
            images[0].enabled = true;
        }
        else
        {
            images[0].enabled = false;
        }

        if (Input.GetKey(KeyCode.X))
        {
            images[1].enabled = true;
        }
        else
        {
            images[1].enabled = false;
        }

        if (Input.GetKey(KeyCode.Comma))
        {
            images[2].enabled = true;
        }
        else
        {
            images[2].enabled = false;
        }

        if (Input.GetKey(KeyCode.Period))
        {
            images[3].enabled = true;
        }
        else
        {
            images[3].enabled = false;
        }

        if (Input.anyKey)
        {
            string keyInput = Input.inputString;
            if (keyInput == "z" || keyInput == "x" || keyInput == "," || keyInput == ".")
            {
                audioSource.Play();
                KeyLogPress newInput;
                newInput.pressTime = AudioSettings.dspTime;
                newInput.key = keyInput;
                keyLog.Add(newInput);
            }
        }
    }

    public struct KeyLogPress
    {
        public double pressTime;
        public string key;
    }

}
