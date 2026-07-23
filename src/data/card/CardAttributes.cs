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
	public Suit CardSuit { get; set; }
	[Export]
	public int Number { get; set; }
}
