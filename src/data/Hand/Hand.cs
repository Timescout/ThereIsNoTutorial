using Godot;
using System;

public partial class Hand : Control
{
	private int _Round = 1;
	private int _cardId = 0;
	
	private void DrawOne() 
	{
		DrawCard(1);
	}
	private void DrawCard(int numberDrawn = 1) 
	{
		PackedScene card = GD.Load<PackedScene>("res://src/data/card/card.tscn");
		for (int i = 0; i < numberDrawn; i++)
		{
			Button button = new Button();
			button.Name = _cardId.ToString();
			_cardId++;
			button.AddChild(card.Instantiate());
			button.CustomMinimumSize = new Vector2(100, 200);
			button.Size = new Vector2(100, 140);
			button.Pressed += () => PlayCard(button.Name);
			button.ZIndex = 1;
			GetNode<HBoxContainer>("PlayerHand").AddChild(button);
		}
	}
	
	private void PlayCard(string CardButtonId) 
	{
		Button CardButton = GetNode<Button>("PlayerHand/" + CardButtonId);
		Card playedCard = CardButton.GetNode<Card>("Card");
		
		// check if the move was valid.
		if (!CheckRules(playedCard)) { return; }
		
		Card currentDiscard = GetNode<Card>("DiscardPile");
		currentDiscard.Attributes.CardSuit = playedCard.Attributes.CardSuit;
		currentDiscard.Attributes.Number = playedCard.Attributes.Number;
		CardButton.QueueFree();
	}
	
	private bool CheckRules(Card card)
	{
		// Round 0 Has no rules, all cards are allowed.
		
		// Round 1, Match Number or Suit.
		Card DiscardPile = GetNode<Card>("DiscardPile");
		if (_Round >= 1 &&
			card.Attributes.CardSuit != DiscardPile.Attributes.CardSuit && 
			card.Attributes.Number != DiscardPile.Attributes.Number) 
			{
				return false; 
			}
		return true;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DrawCard(5);
		GetNode<Button>("DrawButton").Pressed += DrawOne;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
