using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechToText : MonoBehaviour
{

	public InformationManager im;
	public CallTowerManager towerManger;


    private KeywordRecognizer keywordRecognizer;
	private Dictionary<string, Action> actions = new Dictionary<string, Action>();
	
	void Start()
	{

		StartCoroutine(PopulateCommands());
		
	}

	private IEnumerator PopulateCommands()
    {
		yield return new WaitForSeconds(1.0f);
		Debug.Log(towerManger.GetCrewmatesInformation());
		//add recongizable commands into our dictionary
		foreach (CrewInfo crew in towerManger.GetCrewmatesInformation())
		{
			actions.Add("Navigate to " + crew.name, () => im.SetTracking(crew));
			actions.Add("Call " + crew.name, () => towerManger.playerTransmitter.QuickDial(crew.frequency));
		}
		actions.Add("Call emergency line", () => towerManger.playerTransmitter.QuickDial(towerManger.GetEmergencyFrequency()));
		actions.Add("Hang up", () => towerManger.playerTransmitter.HangUp());
		actions.Add("Answer", () => towerManger.playerTransmitter.AnswerCall());
		actions.Add("Cancel navigation", () => im.ClearTracking(false));

		
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
	
}
