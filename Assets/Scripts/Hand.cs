using System.Linq;

public class Hand
{
	const int HAND_SIZE = 3;
	public Card[] hand = new Card[HAND_SIZE];

	public int Count
	{
		get
		{
			return hand.Count((x) => x != null);
		}
	}

	public bool IsFull
	{
		get
		{
			return Count >= HAND_SIZE;
		}
	}

	public bool IsEmpty
	{
		get
		{
			return Count <= 0;
		}
	}

	/// <summary>
	/// Discard the given card from the hand.
	/// </summary>
	/// <param name="card"></param>
	/// <returns>True if discarded, false if not found.</returns>
	public bool Discard(Card card)
	{
		for (int i = 0; i < hand.Length; i++) {
			if (hand[i] == card) {
				hand[i] = null;
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Puts a card in this hand, if able to.
	/// </summary>
	/// <param name="card"></param>
	/// <returns>True if successful, false if full.</returns>
	public bool Insert(Card card)
	{
		for (int i = 0; i < hand.Length; i++)
		{
			if (hand[i] == null)
			{
				hand[i] = card;
				return true;
			}
		}

		return false;
	}
}