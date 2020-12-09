using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleDial : MonoBehaviour
{
    
    public CallTowerManager ctm;
    private AudioSource aS;
    private bool isBusy = false;
    private bool isIncomingCall = false;

    public AudioClip ringtone;
    private AudioClip incomingCall;
    private int incomingCallerFrequency = 0;

    public AudioClip callEndTone;
    private bool prevPlaying = false;
    private bool callEnded = true;

    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    public void QuickDial(int freq)
    {
        if (!isBusy) // You need to hangup to make a call.
        {
            isBusy = true;
            aS.clip = ctm.CallCrewmate(freq);
            string text;
            if (freq == ctm.GetEmergencyFrequency())
            {
                text = "Calling emergency line";
            } else
            {
                Transform crewmate = ctm.crewmates[Array.IndexOf(ctm.crewmateFrequencies, freq)];
                text = "Calling " + crewmate.name;
            }
            callEnded = false;
            ctm.audioManager.SayText(text, aS);
        }
    }

    public bool IncomingCall(AudioClip speaker, int freq)
    {
        if (!isBusy && !isIncomingCall)
        {
            string crewmateName = ctm.crewmates[Array.IndexOf(ctm.crewmateFrequencies, freq)].name;
            ctm.audioManager.SayText("Incoming call from " + crewmateName);
            incomingCall = speaker;

            incomingCallerFrequency = freq;
            isIncomingCall = true;
            isBusy = true;

            aS.clip = ringtone;
            aS.loop = true;
            aS.Play();
        }

        return !isBusy;

    }

    public void AnswerCall()
    {
        if (isIncomingCall)
        {
            aS.Stop();
            aS.loop = false;
            aS.clip = incomingCall;
            aS.Play();
            isIncomingCall = false;
            callEnded = false;
        }
    }

    public void HangUp()
    {
        if (isBusy)
        {
            aS.Stop();
            isBusy = false;
            isIncomingCall = false;

            // TODO add dial tone here
        }
    }

    public void Update()
    {
        // Play call ended tone until hang up
        Debug.Log((!ctm.audioManager.IsBusy()).ToString() +  prevPlaying.ToString() + (!callEnded).ToString());
        if (!isIncomingCall && !ctm.audioManager.IsBusy() && !callEnded && !aS.isPlaying)
        {
            callEnded = true;
            aS.loop = true;
            aS.clip = callEndTone;
            aS.Play();
            prevPlaying = false;
        } 
        
    }
}
