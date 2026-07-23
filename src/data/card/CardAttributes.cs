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
}
