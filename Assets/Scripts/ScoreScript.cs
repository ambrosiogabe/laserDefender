using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		text.text = Scorekeeper.totalPoints.ToString();
		Scorekeeper.Reset ();
	}
}
