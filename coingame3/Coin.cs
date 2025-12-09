using Godot;
using System;

public partial class Coin : Area2D
{
	private GPUParticles2D sparkle;
	public override void _Ready()
	{
		// Register this coin with main so main knows the total number
		var main = GetTree().CurrentScene as Main;
		main?.RegisterCoin();
		
		sparkle = GetNodeOrNull<GPUParticles2D>("SparkleParticles");

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

			var audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
			audio?.Play();
			if (sparkle != null)
			{
				sparkle.Emitting = true;
			}
		// Wait briefly before removing
		GetTree().CreateTimer(0.2).Timeout += () => QueueFree();
		}
	}
}
