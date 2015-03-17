using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
	p1_start,
	p1_waiting,
	p2_start,
	p2_waiting,
	gameover
}

public class Controller : MonoBehaviour
{
	public GameState state = GameState.p1_start;

	public GameState nextState = GameState.p1_waiting;

	public GameObject p1hand;
	public GameObject p2hand;

	public BattlePlayer player1;
	public BattlePlayer player2;

	public GameObject cardPrefab;

	public void Start()
	{
		player1 = new BattlePlayer(new Player(), new Deck(), new Hand(p1hand));
		player2 = new BattlePlayer(new Player(), new Deck(), new Hand(p2hand));
	}

	public void Update()
	{
		switch (state)
		{
			case GameState.p1_start:
				player1.Update();
				nextState = GameState.p1_waiting;
				break;
			case GameState.p2_start:
				player2.Update();
				nextState = GameState.p2_waiting;
				break;
		}

		state = nextState;
	}

	public void CardClicked(Object cardObject)
	{
		var card = cardObject as GameObject;

		Debug.Log("Clicked card: " + card.GetComponent<Card>().cardName);
	}
}