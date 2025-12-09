using Godot;
using System;

public partial class FallingObject : Area2D
{
	[Export] public float Speed = 200f;

	public override void _Ready()
	{
		BodyEntered += OnHitPlayer;  // connects signal
	}

	public override void _Process(double delta)
	{
		Position += new Vector2(0, Speed * (float)delta);

		// Respawn if below screen
		float screenHeight = GetViewportRect().Size.Y;

		if (Position.Y > screenHeight + 50)
		{
			Respawn();
		}
	}

	private void Respawn()
	{
		Random random = new Random();
		float screenWidth = GetViewportRect().Size.X;

		Position = new Vector2(
			random.Next(50, (int)screenWidth - 50),
			-50
		);
	}

	private void OnHitPlayer(Node body)  // <-- THIS must exist
	{
		if (body is Player)
		{
			var main = GetTree().CurrentScene as Main;
			main?.GameOver();
		}
	}
}
