using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;


public class Controller2 : MonoBehaviour, IPlayerCardEventHandler {
	public Player p1;
	public Player p2;

	public BattleLogger log;

	public enum State
	{
		None,
		PlayersChoosing,
		PlayersLocked
	};

	public State state;

	void Start()
	{
		p1.deck = new Deck();
		p2.deck = new Deck();
		p1.state = Player.State.Deciding;
		p2.state = Player.State.Deciding;
		p1.drawTillFull();
		p2.drawTillFull();
	}

	void Update()
	{
		switch (state)
		{
			case State.None:
				break;
			case State.PlayersChoosing:
				break;
			case State.PlayersLocked:
				CardChecks(p1, p2, p1.CardChoice, p2.CardChoice);
				state = State.PlayersChoosing;
				break;
			default:
				break;
		}
	}


	/// <summary>
	/// Print a message to the battle log.
	/// </summary>
	/// <param name="message"></param>
	void BattleLog(string message)
	{
		log.Log(message);
	}

	#region Event Handlers

	public void CardDrawn(Player player, Card card)
	{
		BattleLog(player + " draws " + card.cardName);
	}

	public void TookDamage(Player player, int amount)
	{
		throw new NotImplementedException();
	}

	public void CardChosen(Player player, Card card)
	{
		if (player.CanPickCard)
		{
			// if player has not chosen a card yet, notify that they are ready now
			// TODO: replace with observer pattern
			if (!player.HasChosen) {
				BattleLog(player + " is ready.");
			}
			player.cardChoice = card;
		}
	}

	public void CardChosen(UICard card)
	{
		CardChosen(card.owner, card.card);
	}

	public void CardPlayed(Player player, Card card)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// React to a selected card.
	/// </summary>
	/// <param name="card"></param>
	public void CardClicked(GameObject card)
	{
		var uiCard = card.GetComponent<UICard>();
		Debug.Log(uiCard.card.cardName + " was clicked");
		CardChosen(uiCard);
	}
	#endregion

	public void CardChecks(Player p1, Player p2, Card p1Card, Card p2Card)
	{
		// Attack Checks

		BattleLog("Player " + p1.Name + " now has " + p1.Health + "HP");
		BattleLog("Player " + p2.Name + " now has " + p2.Health + "HP");
	}
}
