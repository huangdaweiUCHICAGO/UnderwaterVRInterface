﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Linq;


public class TTSManager : MonoBehaviour
{

    private string apiKey;
    private bool isBusy;

    private bool sayingHelpText;
    private AudioSource helpAudioSource;
    private AudioSource callAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        apiKey = new StreamReader("gcloud.key").ReadToEnd();
    }

    // Says the given text, calling Google Cloud's TTS service as needed
    // (when the phrase has not been said before)
    // If provided with an audio source, this will play the source immediately after 
    // the text is spoken
    public void SayText(string text, AudioSource nextSource=null)
    {
        if (nextSource != null)
        {
            isBusy = true;
        }

        Debug.Log("Saying: " + text);
        if (text.Contains("Reach your crewmates"))
        {
            if (helpAudioSource != null)
            {
                helpAudioSource.Stop();
            }
            sayingHelpText = true;
        }


        StartCoroutine(SayTextRoutine(text, nextSource));

    }

    public bool IsBusy()
    {
        return isBusy;
    }

    
    public IEnumerator SayTextRoutine(string text, AudioSource nextSource=null)
    {
        string textFilename = text.Replace(" ", "_");
        textFilename = textFilename.Replace(":", "_");
        textFilename = textFilename.Replace("?", "_");

        if (textFilename.Length > 200)
        {
            textFilename = textFilename.Substring(0, 200);
        }
        string filename = "Assets/Audio/TTS/" + textFilename + ".mp3";
        if (!File.Exists(filename))
        {
            yield return StartCoroutine(CreateMP3(text, filename));
        }

        
        
        string path = "file://" + Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")) + "/" + filename;
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG);
        yield return www.SendWebRequest();

        AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
        AudioSource audioSource = this.GetComponents<AudioSource>()[0];
      
        if (audioSource.isPlaying)
        {
            audioSource = this.GetComponents<AudioSource>()[1].isPlaying ? this.GetComponents<AudioSource>()[2] : this.GetComponents<AudioSource>()[1];
        }

        audioSource.clip = clip;
        audioSource.Play();
        if (sayingHelpText)
        {
            helpAudioSource = audioSource;
        }
        if (nextSource != null)
        {
            yield return new WaitForSeconds(clip.length);
            audioSource.clip = nextSource.clip;
            audioSource.Play();
            callAudioSource = audioSource;
            yield return new WaitForSeconds(nextSource.clip.length);
            if (callAudioSource != null)
            {
                isBusy = false;
            }
        }
        
        if (sayingHelpText)
        {
            yield return new WaitForSeconds(clip.length);
            sayingHelpText = false;
            helpAudioSource = null;
        } 
        
    }

    class Response
    {
        public string audioContent;
    }

    IEnumerator CreateMP3(string spokenText, string filename)
    {
        string url = "https://texttospeech.googleapis.com/v1/text:synthesize?key=" + apiKey;

        string text = "{\"text\": \"" + spokenText +"\"}";
        string voice = "{\"languageCode\":\"en-GB\",\"name\":\"en-GB-Wavenet-D\"}";
        string audioConfig = "{\"audioEncoding\": \"MP3\"}";
        string data = "{\"input\":" + text + ",\"voice\":" + voice + ",\"audioConfig\":" + audioConfig + "}";

        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            Response response = JsonUtility.FromJson<Response>(uwr.downloadHandler.text);
            File.WriteAllBytes(filename, Convert.FromBase64String(response.audioContent));
        }



    }

    public void StopCall()
    {
        if (isBusy && callAudioSource != null)
        {
            callAudioSource.Stop();
            callAudioSource = null;
            isBusy = false;
        }
    }

}
