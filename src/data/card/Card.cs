using Godot;
using System;

public partial class Card : Control
{
	[Export]
	public CardAttributes Attributes = new CardAttributes();
	
	private const int _MAXCARDNUMBER = 10;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Randomly Create a card
		Attributes.Number = GD.RandRange(1, 10);
		Attributes.CardSuit = (Suit)GD.RandRange(0, 3);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Set Graphics
		GetNode<RichTextLabel>("Number").Text = "[color=black]" + Attributes.Number.ToString() + "[/color]";
		switch (Attributes.CardSuit) {
			case Suit.Square:
				GetNode<TextureRect>("Suit").Texture = GD.Load<Texture2D>("res://src/data/card/Square.png");
				break;
			case Suit.Circle:
				GetNode<TextureRect>("Suit").Texture = GD.Load<Texture2D>("res://src/data/card/Circle.png");
				break;
			case Suit.Triangle:
				GetNode<TextureRect>("Suit").Texture = GD.Load<Texture2D>("res://src/data/card/Triangle.png");
				break;
			case Suit.Cross:
				GetNode<TextureRect>("Suit").Texture = GD.Load<Texture2D>("res://src/data/card/Cross.png");
				break;
			default:
				break;
		}
	}
}
