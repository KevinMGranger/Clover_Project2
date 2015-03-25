using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;


public class Controller2 : MonoBehaviour, IPlayerCardEventHandler {
	private Player p1;
	private Player p2;

	public BattleLogger log;

	public GameObject cardPrototype;


	void Start()
	{
		p1 = new Player ("One", new Hand(), new Deck(), this);
		p2 = new Player ("Two", new Hand(), new Deck(), this);
	}
	
	void Update () {
		ProcessInput();
	}

	void ProcessInput()
	{
		if (!Input.anyKeyDown) return;

		if (Input.GetKeyDown(KeyCode.Q)) p1.playCard(0);
		else if (Input.GetKeyDown(KeyCode.A)) p1.playCard(1);
		else if (Input.GetKeyDown(KeyCode.Z)) p1.playCard(2);
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

	/// <summary>
	/// React to a selected card.
	/// </summary>
	/// <param name="card"></param>
	public void CardClicked(GameObject card)
	{
		var uiCard = card.GetComponent<UICard>();
		Debug.Log(uiCard.card.cardName + " was clicked");

		var owner = uiCard.owner;

		if (owner.CanPickCard) {
			owner.cardChoice = uiCard;


		if ((state & GameState.Player2Deciding) == GameState.Player2Deciding)
			if (owner == p2)
			{
				c2 = uiCard.card;
				go2 = card;
				BattleLog("Player " + p2.Name + " picked " + uiCard.card.ToString());
				state = state | GameState.Player2Chosen ^ GameState.Player2Deciding;
			}

		if ((state & GameState.Player1Chosen) == GameState.Player1Chosen)
			if ((state & GameState.Player2Chosen) == GameState.Player2Chosen)
			{
				CardChecks(p1, p2, c1, c2);
				Destroy(go1);
				Destroy(go2);
				p1.discard(c1);
				p2.discard(c2);
				p1.draw();
				p2.draw();
				state = GameState.Player1Deciding | GameState.Player2Deciding;
			}
	}
	#endregion

	public void CardChecks(Player p1, Player p2, Card p1Card, Card p2Card)
	{
		// Defense Checks
		if (p1Card.type == CardType.defense) {
			p1.Defend(p1Card.def);
		}
		if (p2Card.type == CardType.defense) {
			p2.Defend(p2Card.def);
		}

		// Attack Checks
		if (p1Card.type == CardType.attack) {
			if(p1Card.effect1 == EffectType.pierce)
			{
				p2.TakeDamage(p1Card.atk, p1Card.effectStrength1);
			}
			else if(p1Card.effect2 == EffectType.pierce)
			{
				p2.TakeDamage(p1Card.atk, p1Card.effectStrength2);
			}
			else if(p1Card.effect1 == EffectType.counter)
			{
				p1.TakeDamage(p2Card.atk,0);
				p2.TakeDamage(p1Card.atk,0);
			}
			else if(p1Card.effect2 == EffectType.counter)
			{
				p1.TakeDamage(p2Card.atk,0);
				p2.TakeDamage(p1Card.atk,0);
			}
			else{
				p2.TakeDamage(p1Card.atk, 0);
			}
		}
		if (p2Card.type == CardType.attack) {
			if(p2Card.effect1 == EffectType.pierce)
			{
				p1.TakeDamage(p2Card.atk, p2Card.effectStrength1);
			}
			else if(p2Card.effect2 == EffectType.pierce)
			{
				p1.TakeDamage(p2Card.atk, p2Card.effectStrength2);
			}
			else if(p2Card.effect1 == EffectType.counter)
			{
				p1.TakeDamage(p2Card.atk,0);
				p2.TakeDamage(p1Card.atk,0);
			}
			else if(p2Card.effect2 == EffectType.counter)
			{
				p1.TakeDamage(p2Card.atk,0);
				p2.TakeDamage(p1Card.atk,0);
			}
			else{
				p1.TakeDamage(p2Card.atk, 0);
			}
		}

		// Effect Checks
		if (p1Card.type == CardType.utility) {
			if(p1Card.effect1 == EffectType.heal || p1Card.effect2 == EffectType.heal || p1Card.effect1 == EffectType.cure || p1Card.effect2 == EffectType.cure)
			{
				p1.AddEffect(p1Card);
			}
			else{
				p2.AddEffect(p1Card);
			}
		}
		if(p2Card.type == CardType.utility){
			if(p2Card.effect1 == EffectType.heal || p2Card.effect2 == EffectType.heal || p2Card.effect1 == EffectType.cure || p2Card.effect2 == EffectType.cure)
			{
				p2.AddEffect(p2Card);
			}
			else{
				p1.AddEffect(p2Card);
			}
		}

		BattleLog("Player " + p1.Name + " now has " + p1.Health + "HP");
		BattleLog("Player " + p2.Name + " now has " + p2.Health + "HP");
	}

}
