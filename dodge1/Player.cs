using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 400f;
	private Vector2 _velocity = Vector2.Zero;

	public override void _Ready()
	{
		// If Camera2D is child and you want to enforce it:
		var cam = GetNodeOrNull<Camera2D>("Camera2D");
		cam?.MakeCurrent();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 dir = Vector2.Zero;
		if (Input.IsActionPressed("ui_left")) dir.X -= 1;
		if (Input.IsActionPressed("ui_right")) dir.X += 1;

		_velocity = dir == Vector2.Zero ? Vector2.Zero : dir.Normalized() * Speed;
		Velocity = _velocity;
		MoveAndSlide();
	}

	// Optional: helper for hit detection (if main checks player)
}
