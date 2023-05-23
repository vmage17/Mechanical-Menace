using Godot;
using System;

public class ChangeSceneButton : Button
{

	[Export(PropertyHint.File)]
	public string nextScenePath = "";

	private void onButtonUp()
	{
		GetTree().ChangeScene(nextScenePath);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		
	}
}



