using Godot;
using System;

public class GameInterface : Control
{

	SceneTree sceneTree = null;
	TextureRect pauseOverlay = null;
	bool paused = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneTree = GetTree();
		pauseOverlay = (TextureRect)GetNode("PauseOverlay");
		setPause(false);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		
	}

	public void setPause(bool state) {
		paused = state;
		sceneTree.Paused = state;
		pauseOverlay.Visible = state;
	}

	public void onContinueButtonUp()
	{
		setPause(false);
	}

	private void onQuitButtonUp()
	{
		setPause(false);
		GetTree().ChangeScene("res://src/Screens/MainScreen.tscn");
	}

	public override void _UnhandledInput(InputEvent @event) {
		if(@event.IsActionPressed("pause")) {
			if (paused)
			{
				// Hide the pause menu if it's already visible
				setPause(false);
			}
			else
			{
				// Show the pause menu if it's hidden
				setPause(true);
			}
		}
	}
}
