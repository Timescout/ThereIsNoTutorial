using Godot;
using System;

public partial class Card : Control
{
	//private static _Graphics = ;

	Card()
	{
		// Randomly Create a card
		_attributes = new CardAttributes(GD.RandRange(1, 10), (Suit)GD.RandRange(0, 3));
	}

	private CardAttributes _attributes;

	[Export]
	public CardAttributes Attributes 
	{ 
		get => _attributes;
		set
		{
			Attributes.Rank = value.Rank;
			Attributes.Suit = value.Suit;
			UpdateGraphics();
		}
	}
	

	public void UpdateGraphics()
	{
		// Set Graphics
		GetNode<RichTextLabel>("Number").Text = "[color=black]" + Attributes.Rank.ToString() + "[/color]";
		switch (Attributes.Suit) {
			case Suit.Square:
				GetNode<TextureRect>("Suit").Texture = GD.Load<Texture2D>("res://src/data/card/assets/Square.png");
				break;
			case Suit.Circle:
				GetNode<TextureRect>("Suit").Texture = GD.Load<Texture2D>("res://src/data/card/assets/Circle.png");
				break;
			case Suit.Triangle:
				GetNode<TextureRect>("Suit").Texture = GD.Load<Texture2D>("res://src/data/card/assets/Triangle.png");
				break;
			case Suit.Cross:
				GetNode<TextureRect>("Suit").Texture = GD.Load<Texture2D>("res://src/data/card/assets/Cross.png");
				break;
			default:
				break;
		}
	}
	
	
	private const int _MAXCARDNUMBER = 10;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateGraphics();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
