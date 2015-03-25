using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Player : MonoBehaviour
{
	public enum State
	{
		None,
		Deciding,
		Chosen,
		Dead
	}

	public const int MAX_HEALTH = 20;

	public int Health;
	public string Name;
	public UICard cardChoice = null;
	public Deck deck;
	public Hand hand;
	public State state;

	public GameObject playerCardEventHandler;

	void Start()
	{
		Health = MAX_HEALTH;
		for (int i = 0; i < 3; i++)
		{
			draw();
		}
	}

	void Update()
	{
	}

	public bool CanPickCard
	{
		get
		{
			return state == State.Deciding || state == State.Chosen;
		}
	}

	public UICard CardChoice
	{
		get { return cardChoice; }
		set
		{
			if (value != null && CanPickCard)
			{
				cardChoice = value;
				ExecuteEvents.Execute<IPlayerCardEventHandler>(playerCardEventHandler, null, (x, y) => x.CardChosen(this, value));
			}
		}
	}

	public void discard(Card card)
	{
		hand.Discard(card);
	}

	public bool draw()
	{
		if (hand.IsFull)
		{
			Debug.LogWarning(this + "'s hand is full, but someone tried to draw anyway.");
			return false;
		}

		var card = deck.Draw();

		if (card == null)
		{
			Debug.LogError("Deck was empty when " + this + " drew");
			return false;
		}

		var insertSuccess = hand.Insert(card);

		if (!insertSuccess)
		{
			Debug.LogError("Couldn't add the card " + card + " to " + this + "'s hand for some reason");
			return false;
		}

	}

	// meant to handle when the player is struck by enemy card. 
	public void TakeDamage(int i)
	{
		Health -= i;
		Log("Player " + Name + " Takes " + i + " damage!");
	}

	public Card playCard(int cardToPlay)
	{
		// Player will choose a card from their hand.
		// This will eventually be associated with a button press on the card. 

		switch (cardToPlay)
		{
			case 1:
				cardChoice = hand[0];
				break;
			case 2:
				cardChoice = hand[1];
				break;
			case 3:
				cardChoice = hand[2];
				break;
		}
		return cardChoice;
	}

	public override string ToString()
	{
		return "Player " + Name;
	}

}