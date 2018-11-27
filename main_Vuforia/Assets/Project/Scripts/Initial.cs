using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reqire components
public class Initial : MonoBehaviour {
	//Target Transform
	public Transform tf_SunTarg;
	public Transform tf_SomethingTarg;

	SphereCollider col_Sun;
	SphereCollider col_Something;

	[SerializeField]
	[Range(0.5f,1f)]
	float num_waitTime;


	// not the best way to controll target lose and game resta
	bool bl_CountdownStarted = false;
	bool isPlaying = false;
	UIController ui;
	mainGame_Spawner spawner;
	userControll control;

	void Start () {
		col_Sun = tf_SunTarg.GetComponent<SphereCollider> (); 
		col_Something = tf_SomethingTarg.GetComponent<SphereCollider> ();
		ui = GetComponent<UIController> ();
		spawner = GetComponent<mainGame_Spawner> ();
		control = GetComponent<userControll> ();
	}

	void Update () {
		if (CheckForObjects () && !bl_CountdownStarted) {
			StartCoroutine (GameCountdown ());
			bl_CountdownStarted = true;
		} else if (isPlaying && !CheckForObjects()) {
			//destroy all obj
			spawner.ClearObjectsList();
			spawner.enabled = false;
			//hide Score UI
			ui.ActivatePlayerScore(false);
			//information of Sun and Something target lost
			//ability to start new game
			ui.LostTargetGameOver(true);

		}
	}

	public void OKClicked()
	{
		ui.LostTargetGameOver(false);
		bl_CountdownStarted = false;
		isPlaying = false;
	}

	bool CheckForObjects()
	{
		if (col_Sun.enabled && col_Something.enabled)
			return true;
		else
			return false;
	}

	IEnumerator GameCountdown()
	{
		ui.CountdownActive (true);
		for (int i = 0; i <5; i++) {
			if (!CheckForObjects ()) {
				//yield return null;
				print ("Coroutine stopped execution");
				bl_CountdownStarted = false;
				break;
			}
			if (i < 3) {
				string countdownTXT = (i + 1).ToString ();
				ui.SetCountdownTxt (countdownTXT);	
				}
			else if (i == 3) {
				ui.SetCountdownTxt ("START");
				}
			else if (i == 4) {
				spawner.enabled = true;
				control.enabled = true;
				ui.ActivatePlayerScore (true);
				isPlaying = true;
			}
			yield return new WaitForSeconds (num_waitTime);
		}
		//bl_CountdownStarted = false;
		ui.CountdownActive (false);
	}
}
