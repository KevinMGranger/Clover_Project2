using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hand
{
	public GameObject goHand;

	public static GameObject cardPrefab;

	public int cardLimit;

	public Hand(GameObject goHand, int cardLimit = 5)
	{
		this.goHand = goHand;
		this.cardLimit = cardLimit;
	}

	public int cardCount
	{
		get
		{
			return goHand.transform.childCount;
		}
	}

	public void AddCard(Card card)
	{
		var goCard = GameObject.Instantiate(cardPrefab) as GameObject;
		goCard.transform.parent = goHand.transform;
	}

}