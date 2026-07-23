using Godot;
using System;

public partial class Hand : Control
{
	private int _Round = 1;
	private int _cardId = 0;
	
	private void DrawCard(int numberDrawn = 1) 
	{
		PackedScene cardScene = GD.Load<PackedScene>("res://src/data/card/card.tscn");
		for (int i = 0; i < numberDrawn; i++)
		{
			Card card = cardScene.Instantiate() as Card;

			card.Name = _cardId.ToString();
			_cardId++;

			card.Pressed += () => PlayCard(card.Name);

			GetNode<HBoxContainer>("PlayerHand").AddChild(card);
			
		}
	}
	
	private void PlayCard(string CardButtonId)
	{
		GD.Print(CardButtonId);
		Card playedCard = GetNode<Card>("PlayerHand/" + CardButtonId);
		
		// check if the move was valid.
		if (!CheckRules(playedCard)) { return; }
		
		Card currentDiscard = GetNode<Card>("DiscardPile");
		currentDiscard.Attributes = playedCard.Attributes;
		playedCard.QueueFree();
	}
	
	private bool CheckRules(Card card)
	{
		// Round 0 Has no rules, all cards are allowed.
		
		// Round 1, Match Number or Suit.
		Card DiscardPile = GetNode<Card>("DiscardPile");
		if (_Round >= 1 &&
			card.Attributes.Suit != DiscardPile.Attributes.Suit && 
			card.Attributes.Rank != DiscardPile.Attributes.Rank
		) 
			{
				return false; 
			}
		return true;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DrawCard(5);
		GetNode<Button>("DrawButton").Pressed += () => DrawCard(1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
