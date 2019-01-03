using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class FixedBeatManager : MonoBehaviour
{
    public double bpm = 140.0F;
    public AudioClip[] clips = new AudioClip[2];
    public double timingWindow = 0.2f;
    public double timingWindowPerfect = 0.1f;

    // This shit should nOT be named "flip" dumbass its not flipping in the real game ughhhhh!!!11!
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[2];
    private bool running = false;
    private int beatCount = 0;
    private int upbeatCount = 0;
    private int lastUsedUpbeat = 0;
    private int correctTimesFlipped = 0;
    private bool pressed = false;
    private bool inTimingWindow = false;

    private double secPerBeat;
    private double nextBeatTime;
    private double nextUpbeatTime;

    private double lastPressTime;


    private SpriteRenderer sprite;
    private Rigidbody canvas;
    public Text displayText;

    void Start()
    {
        int i = 0;
        while (i < 2)
        {
            GameObject child = new GameObject("Player");
            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();
            i++;
        }
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Start")
        {
            running = true;
        }
        else
        {
            running = false;
        }
        sprite = gameObject.GetComponent<SpriteRenderer>();
        secPerBeat = 60.0f / bpm;
        nextBeatTime = AudioSettings.dspTime + 1.0f;
        nextUpbeatTime = AudioSettings.dspTime + secPerBeat / 2 + 1.0f;
        Debug.Log("SPB: " + secPerBeat + "| Timing Window: " + timingWindow);

        // Set text
        canvas = GetComponent<Rigidbody>();
        displayText.text = "";

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pressed && (lastUsedUpbeat != upbeatCount))
        {
            pressed = true;
            lastPressTime = AudioSettings.dspTime;
            lastUsedUpbeat = upbeatCount;
            double distToBeat = Math.Abs(lastPressTime - nextBeatTime);
            if (distToBeat > secPerBeat / 2)
            {
                distToBeat = secPerBeat - distToBeat;
            }
            Debug.Log("Time: " + lastPressTime + " Next Beat: " + nextBeatTime + " Diff: " + distToBeat);
            if (!((distToBeat > timingWindow) && (distToBeat < (secPerBeat - timingWindow))))
            {
                sprite.flipY = !sprite.flipY;
                correctTimesFlipped++;
            }
            

        }
        displayText.text = "Flips: " + beatCount.ToString() + " Correct flips: " + correctTimesFlipped.ToString()
                           + "\n" + "Ratio: " + (((double)correctTimesFlipped / (double)beatCount)).ToString();
    }


    void FixedUpdate()
    {
        if (!running)
        {
            return;
        }
        double time = AudioSettings.dspTime;
        if (time > nextBeatTime)
        {
            audioSources[flip].clip = clips[flip];
            audioSources[flip].PlayScheduled(nextBeatTime);
            //Debug.Log("Scheduled source " + flip + " to start at time " + nextBeatTime);
            nextBeatTime += secPerBeat;
            flip = 1 - flip;
            sprite.flipX = !sprite.flipX;
            beatCount++;
        }

        if (time > nextUpbeatTime)
        {
            nextUpbeatTime += secPerBeat;
            upbeatCount++;
        }

        if (time > nextBeatTime - timingWindow )
        {
            pressed = false;
        }


    }
}