using UnityEngine;
using System.Collections;

public class UICard : MonoBehaviour {

	public Card card;

	public Player owner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static implicit operator Card(UICard self)
	{
		return self.card;
	}
}
