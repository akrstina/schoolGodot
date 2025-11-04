using Godot;
using System;

public partial class Coin : Area2D
{
	public override void _Ready()
	{
		// Register this coin with main so main knows the total number
		var main = GetTree().CurrentScene as Main;
		main?.RegisterCoin();

		// Connect body entered event
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node body)
	{
		// If the player touches the coin
		if (body is Player)
		{
			// Tell main that a coin was collected
			var main = GetTree().CurrentScene as Main;
			main?.CoinCollected();

			// Play sound here (optional) and then remove coin
			QueueFree();
		}
	}
}
