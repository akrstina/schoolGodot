using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 200f;

	public override void _Ready()
	{
		var cam = GetNode<Camera2D>("Camera2D");
	}

	public override void _PhysicsProcess(double delta)
{
	Vector2 direction = Vector2.Zero;

	if (Input.IsActionPressed("ui_right"))
		direction.X += 1;
	if (Input.IsActionPressed("ui_left"))
		direction.X -= 1;
	if (Input.IsActionPressed("ui_down"))
		direction.Y += 1;
	if (Input.IsActionPressed("ui_up"))
		direction.Y -= 1;

	if (direction != Vector2.Zero)
		Velocity = direction.Normalized() * Speed;
	else
		Velocity = Vector2.Zero;

	MoveAndSlide();
}

}
