using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechToText : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
	private Dictionary<string, Action> actions = new Dictionary<string, Action>();
	
	void Start()
	{
		//add recongizable commands into our dictionary
		actions.Add("cat", Forward);
		actions.Add("up", Up);
		actions.Add("down", Down);
		actions.Add("back", Back);
		
		//begin speech recognition
		keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
		keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
		keywordRecognizer.Start();
	}
	
	private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
	{
		Debug.Log(speech.text);
		actions[speech.text].Invoke();
	}
	
	//actions that you want to complete for each command
	private void Forward()
	{	
		// transform.Translate(1,0,0);
	}
	private void Back()
	{	
		// transform.Translate(-1,0,0);
	}
	private void Up()
	{	
		// transform.Translate(0,1,0);
	}
	private void Down()
	{	
		// transform.Translate(0,-1,0);
	}
}
