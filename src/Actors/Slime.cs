using Godot;
using System;

public class Slime : Enemy
{

	private Sprite sprite;
	Texture textureLeft = ResourceLoader.Load("res://assets/slime_left.png") as Texture;
	Texture textureRigth = ResourceLoader.Load("res://assets/slime_right.png") as Texture;

	private void switchTexture() {
		if(sprite.Texture == textureLeft)
			sprite.Texture = textureRigth;
		else
			sprite.Texture = textureLeft;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = (Sprite)this.GetNode("Slime");
		sprite.Texture = textureLeft;
		
		// Creating energy instance
		energy = (Energy)GD.Load<PackedScene>("res://src/Objects/Energy.tscn").Instance();

		// Setting physics off until player comes in
		SetPhysicsProcess(false);

		// Setting vertical speed
		velocity.x = -speed.x * 0.5f;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		// Gravity
		velocity.y += gravity * delta;

		// Checkiing if slime needs to change direction after hitting a wall
		if (IsOnWall()) {
			velocity.x *= -1.0f;
			switchTexture();
		}
	
		velocity.y = MoveAndSlide(velocity, FLOOR_NORMAL).y;
	}
}

