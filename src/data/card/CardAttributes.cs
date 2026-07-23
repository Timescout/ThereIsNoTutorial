using Godot;
using System;

public enum Suit {
	Square,
	Circle,
	Triangle,
	Cross
}

public partial class CardAttributes : Resource {
	[Export]
	public Suit Suit { get; set; }
	[Export]
	public int Rank { get; set; }

	public CardAttributes(int rank, Suit suit)
	{
		Rank = rank;
		Suit = suit;
	}

	public CardAttributes()
	{
		Rank = 1;
		Suit = Suit.Circle;
	}
}
