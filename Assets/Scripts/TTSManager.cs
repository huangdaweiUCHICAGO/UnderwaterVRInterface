using System;
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

    // Start is called before the first frame update
    void Start()
    {
        apiKey = new StreamReader("gcloud.key").ReadToEnd();
    }

    public void SayText(string text, bool async=true)
    {
        StartCoroutine(SayTextRoutine(text, async));
    }

    
    public IEnumerator SayTextRoutine(string text, bool async)
    {
        // Plays given text. If async is true, 
        string filename = "Assets/Audio/TTS/" + text.Replace(" ", "_") + ".mp3";
        if (!File.Exists(filename))
        {
            yield return StartCoroutine(CreateMP3(text, filename));
        }

        //AUDIO
        string path = "file://" + Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")) + "/" + filename;
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG);
        yield return www.SendWebRequest();

        AudioClip clip = DownloadHandlerAudioClip.GetContent(www);


        this.GetComponent<AudioSource>().clip = clip;
        this.GetComponent<AudioSource>().Play();

        if (!async)
        {
            yield return new WaitForSeconds(clip.length);
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

}
