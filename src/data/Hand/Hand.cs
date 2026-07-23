using Godot;
using System;

public partial class Hand : Control
{
	private int _Round = 0;
	private int _Turns = 0;
	private int _cardId = 0;
	private int _CardsInHand = 0;
	
	private void DrawCard(int numberDrawn = 1) 
	{
		PackedScene cardScene = GD.Load<PackedScene>("res://src/data/card/card.tscn");
		for (int i = 0; i < numberDrawn; i++)
		{
			Card card = cardScene.Instantiate() as Card;

			card.Name = _cardId.ToString();
			_cardId++;

			card.Pressed += () => PlayCard(card.Name);

			GetNode<HBoxContainer>("PlayerHandScroller/PlayerHand").AddChild(card);
			_CardsInHand++;
		}
	}
	
	private void PlayCard(string CardButtonId)
	{

		Card playedCard = GetNode<Card>("PlayerHandScroller/PlayerHand/" + CardButtonId);
		
		// check if the move was valid. If not reduce the counter.
		if (!CheckRules(playedCard)) { 
			_Turns--;
			GetNode<Label>("CountDownCounter").Text = _Turns.ToString();
			return; 
		}
		
		Card currentDiscard = GetNode<Card>("DiscardPile");
		currentDiscard.Attributes = playedCard.Attributes;
		playedCard.QueueFree();
		_CardsInHand--;

	}
	
	private bool CheckRules(Card card)
	{
		Card DiscardPile = GetNode<Card>("DiscardPile");
		// Round 0 Has no rules, all cards are allowed.
		
		// Round 1, Match Number or Suit.
		if (_Round >= 1 &&
			card.Attributes.Suit != DiscardPile.Attributes.Suit &&
			card.Attributes.Rank != DiscardPile.Attributes.Rank
		) { return false; }

		// Round 2, Evens on Odds
		if ( _Round >= 2 &&
			card.Attributes.Suit == DiscardPile.Attributes.Suit &&
			DiscardPile.Attributes.Rank % 2 == card.Attributes.Rank % 2
		) { return false; }

		// The card fails all rules.
		return true;
	}

	private void SetBoard(int round)
	{
		foreach (Node child in GetNode<HBoxContainer>("PlayerHandScroller/PlayerHand").GetChildren())
		{
			child.QueueFree();
		}
		_CardsInHand = 0;
		_Round = round;
		_Turns = _Round * 2 + 3;
		DrawCard(5);
		GetNode<Label>("RoundLabel").Text = "Round " + _Round.ToString() +  (_Round > 0 ? "\nNew Rules Added." : "");
		GetNode<Label>("CountDownCounter").Text = _Turns.ToString();
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetBoard(0);
		GetNode<Button>("DrawButton").Pressed += () => DrawCard(1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Check if the game is over
		// Win, move to next round
		if (_CardsInHand == 0)
		{
			SetBoard(_Round+1);
		}
		// Lose, Start over
		else if (_Turns == 0) { SetBoard(0); }
	}
}
