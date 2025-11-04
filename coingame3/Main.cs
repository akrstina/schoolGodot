using Godot;
using System;

public partial class Main : Node2D
{
	public int Score = 0;
	public int CoinsLeft = 0;
	private Area2D? exitArea;

	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		exitArea = GetNodeOrNull<Area2D>("Exit");
		if (exitArea != null)
		{
			exitArea.Visible = true; // keep visible if you want
			exitArea.BodyEntered += OnExitBodyEntered;
		}
		UpdateHUD();
	}

	public void RegisterCoin()
	{
		CoinsLeft++;
		UpdateHUD();
	}

	public void CoinCollected()
	{
		Score += 1;
		CoinsLeft = Math.Max(0, CoinsLeft - 1);
		UpdateHUD();

		if (CoinsLeft == 0)
		{
			GD.Print("All coins collected!");
			ShowLevelCompleteReady();
		}
	}

	private void ShowLevelCompleteReady()
	{
		var completeLabel = GetNodeOrNull<Label>("HUD/Control/CompleteLabel");
		if (completeLabel != null)
		{
			completeLabel.Visible = true;
			completeLabel.Text = "All coins collected! Go to the exit.";
		}
	}

	private void OnExitBodyEntered(Node body)
	{
		if (body is Player && CoinsLeft == 0)
		{
			GD.Print("Level complete!");
			var completeLabel = GetNodeOrNull<Label>("HUD/Control/CompleteLabel");
			if (completeLabel != null)
			{
				completeLabel.Visible = true;
				completeLabel.Text = "Level complete! Press R to restart.";
			}

			GetTree().Paused = true;
		}
	}

	private void UpdateHUD()
	{
		var scoreLabel = GetNode<Label>("HUD/Control/ScoreLabel");
		var coinsLabel = GetNode<Label>("HUD/Control/CoinsLeftLabel");
		if (scoreLabel != null) scoreLabel.Text = $"Score: {Score}";
		if (coinsLabel != null) coinsLabel.Text = $"Coins left: {CoinsLeft}";
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("restart"))
		{
			GetTree().Paused = false; // ensure unpaused before reload
			GetTree().ReloadCurrentScene();
		}
	}
}
