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
		helpText += "Say Answer to answer calls. Say help to hear this again.";

		PopulateCommands();
	}

	private void PopulateCommands()
    {
		
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

    void Update()
    {
		bool changesMade = false;
		ArrayList crewNames = new ArrayList();
		Dictionary<string, CrewInfo> lookup = new Dictionary<string, CrewInfo>();
		ArrayList namesToDelete = new ArrayList();
		
		//add recongizable commands into our dictionary
		foreach (CrewInfo crew in towerManger.GetCrewmatesInformation())
		{
			crewNames.Add(crew.name);
			lookup.Add(crew.name, crew);
		}

		foreach (string key in actions.Keys)
        {
			if (!key.Contains("Navigate to"))
            {
				continue;
            }

			string name = key.Split(' ')[2];
			if (!crewNames.Contains(name))
            {
				namesToDelete.Add(name);
				changesMade = true;
            } else
            {
				crewNames.Remove(name);
            }
        }

		foreach (string name in namesToDelete)
        {
			actions.Remove("Navigate to " + name);
			actions.Remove("Call " + name);
		}

		foreach (string name in crewNames)
        {
			changesMade = true;
			CrewInfo crew = lookup[name];
			actions.Add("Navigate to " + crew.name, () => im.SetTracking(crew));
			actions.Add("Call " + crew.name, () => towerManger.playerTransmitter.QuickDial(crew.frequency));
		}

		if (changesMade && keywordRecognizer != null)
        {
			keywordRecognizer.Stop();
			keywordRecognizer.Dispose();

			keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
			keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
			keywordRecognizer.Start();
		}
	}

    private void OnDestroy()
    {
		keywordRecognizer.Stop();
		keywordRecognizer.Dispose();
	}

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
	{
		Debug.Log(speech.text);
		actions[speech.text].Invoke();
	}
	
}
