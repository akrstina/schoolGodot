using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export] public PackedScene FallingObjectScene;
	[Export] public float SpawnInterval = 1.0f; // seconds
	[Export] public float SpawnXMin = -400f;
	[Export] public float SpawnXMax = 400f;
	[Export] public float SpawnY = -50f;

	private Timer _spawnTimer;
	private Node2D _objectsParent;

	public override void _Ready()
	{
		_spawnTimer = new Timer();
		_spawnTimer.WaitTime = SpawnInterval;
		_spawnTimer.Autostart = true;
		_spawnTimer.OneShot = false;
		AddChild(_spawnTimer);
		_spawnTimer.Timeout += OnSpawnTimerTimeout;

		_objectsParent = GetNode<Node2D>("../Objects"); // parent in Main
	}

	private void OnSpawnTimerTimeout()
	{
		if (FallingObjectScene == null) return;

		var instance = FallingObjectScene.Instantiate() as Node2D;
		if (instance == null) return;

		float x = (float)GD.RandRange(SpawnXMin, SpawnXMax);
		instance.Position = new Vector2(x, SpawnY);
		_objectsParent.AddChild(instance);
	}

	// Optional: a method to change spawn rate to increase difficulty
	public void SetSpawnInterval(float t)
	{
		SpawnInterval = t;
		_spawnTimer.WaitTime = t;
	}
}
