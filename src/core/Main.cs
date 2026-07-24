using Godot;
using System;

public partial class Main : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddMainMenu();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void AddMainMenu()
	{
		// when the game starts, open the main menu
		MainMenu menu = GD.Load<PackedScene>("res://src/ui/mainMenu/main_menu.tscn").Instantiate() as MainMenu;
		menu.Name = "MainMenu";

		menu.GetNode<Button>("MenuButtonContainer/QuitButton").Pressed += () => 
		{ 
			GetTree().Quit();
		};

		menu.GetNode<Button>("MenuButtonContainer/StartButton").Pressed += () =>
		{
			AddHand();
			GetNode<MainMenu>("MainMenu").QueueFree();
		};

		AddChild(menu);
	}

	private void AddHand()
	{
		Hand hand = GD.Load<PackedScene>("res://src/data/Hand/hand.tscn").Instantiate() as Hand;
		hand.Name = "Hand";
		AddChild(hand);
	}
}
