using Godot;
using System;

public partial class Main : Node2D
{
	private int _score = 0;
	private bool _gameOver = false;
	private Label _scoreLabel;
	private Label _messageLabel;
	private Spawner _spawner;

	public override void _Ready()
	{
		_scoreLabel = GetNode<Label>("HUD/Control/ScoreLabel");
		_messageLabel = GetNode<Label>("HUD/Control/MessageLabel");
		_spawner = GetNode<Spawner>("Spawner");

		// Configure spawner packed scene in inspector OR do it here:
		// _spawner.FallingObjectScene = (PackedScene)ResourceLoader.Load("res://FallingObject.tscn");
		// Optionally set spawn X bounds relative to screen width:
		float screenW = GetViewportRect().Size.X / 2f;
		_spawner.SpawnXMin = -screenW + 32;
		_spawner.SpawnXMax = screenW - 32;
	}

	public override void _Process(double delta)
	{
		if (_gameOver && Input.IsActionJustPressed("restart"))
			GetTree().ReloadCurrentScene();
	}
	
	public void OnFallingObjectHit(Node body)
{
	if (body is Player)
	{
		GD.Print("GAME OVER!");
		GetTree().ReloadCurrentScene();
	}
}

	// Call this when player collides with an object
	public void HitByObject()
	{
		if (_gameOver) return;
		_gameOver = true;
		_messageLabel.Visible = true;
		_messageLabel.Text = "Game Over! Press R to restart";
		// stop spawning
		_spawner.SetSpawnInterval(9999f);
		// optionally pause the game physics
		// GetTree().Paused = true;
	}

	public void AddScore(int amount)
	{
		_score += amount;
		_scoreLabel.Text = $"Score: {_score}";
	}
	public void GameOver()
{
	GD.Print("GAME OVER!");
	GetTree().ReloadCurrentScene();
}
}
