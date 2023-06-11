using Godot;
using System;

public class Actor : KinematicBody2D
{

	protected enum DIRECTION {
		Left,
		Right,
		Up,
		Down
	}
	
	public static Vector2 FLOOR_NORMAL = new Vector2(0.0f, -1.0f);
	protected const float gravity = 4000.0f;

	protected Vector2 speed = new Vector2(500.0f, 1500.0f);
	protected Vector2 velocity = new Vector2(0.0f, 0.0f);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		
	}
}
