using UnityEngine;
using UnityEngine.UI;

public class BattlePlayer
{
	public Player player;

	public Deck deck;

	public Hand hand;

	public BattlePlayer(Player player, Deck deck, Hand hand)
	{
		this.player = player;
		this.deck = deck;
		this.hand = hand;
	}

	public void Update()
	{
		DrawUntilFull();
	}

	public bool DrawCard()
	{
		if (deck.cardCount <= 0)
		{
			return false;
		}

		var card = deck.Draw();

		if (!card)
		{
			return false;
		}

		hand.AddCard(card);

		return true;
	}

	public void DrawUntilFull()
	{
		var cards = hand.cardLimit - hand.cardCount;

		if (cards >= 0)
		{
			for (int i = 0; i < cards; i++)
			{
				if (!DrawCard())
				{
					break;
				}
			}
		}
	}
}