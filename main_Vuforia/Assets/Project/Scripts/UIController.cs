using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	[SerializeField]
	GameObject LostTargetField;
	[SerializeField]
	Text txt_targetLostScore;
	[SerializeField]
	Text txt_Countdown;
	[SerializeField]
	Text txt_PlayerScore;
	int playerScore;

	public void CountdownActive(bool activeState)
	{
		txt_Countdown.gameObject.SetActive (activeState);
	}

	public void SetCountdownTxt(string text)
	{
		txt_Countdown.text = text;
	}

	public void ActivatePlayerScore(bool activeState)
	{
		if (activeState) {
			playerScore = 0;
			txt_PlayerScore.text = "Score : " + playerScore.ToString ();
		}
		txt_PlayerScore.gameObject.SetActive (activeState);
	}

	public void AddScore()
	{
		playerScore++;
		txt_PlayerScore.text = "Score : " + playerScore.ToString ();
	}

	public void LostTargetGameOver(bool activeState)
	{
		LostTargetField.SetActive (activeState);
		if (activeState) {
			txt_targetLostScore.text = "Your score is " + playerScore.ToString () + "!" + "\n" + "Well done?"; 
		}
	}
}
