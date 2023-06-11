using Godot;
using System;

public class ChangeSceneButton : TextureButton
{
	[Export(PropertyHint.File)]
	public string nextScenePath = "";

	private void onButtonUp()
	{
		GetTree().ChangeScene(nextScenePath);
	}
}
