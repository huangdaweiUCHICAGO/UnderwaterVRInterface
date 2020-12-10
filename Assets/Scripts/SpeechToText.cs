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
	private string helpText;


    private KeywordRecognizer keywordRecognizer;
	private Dictionary<string, Action> actions = new Dictionary<string, Action>();
	
	void Start()
	{
		helpText = "Reach your crewmates by saying Call or Navigate to, followed by their name. ";
		helpText += "You may also say Call emergency line, hang up, or cancel navigation. ";
		helpText += "Say Hello to answer calls. Say help to hear this again.";

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
		actions.Add("Hello", () => towerManger.playerTransmitter.AnswerCall());
		actions.Add("Cancel navigation", () => im.ClearTracking(false));
		actions.Add("Help", () => im.audioManager.SayText(helpText));


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
