using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour {

	private Text text;
	public static int totalPoints = 0;

	public void Start() {
		text = GetComponent<Text>();
		text.text = totalPoints.ToString ();
	}

	public void Score(int points) {
		totalPoints += points;
		text.text = totalPoints.ToString();
	}

	public int GetScore() {
		return totalPoints;
	}

	public static void Reset() {
		Scorekeeper.totalPoints = 0;
	}

}
