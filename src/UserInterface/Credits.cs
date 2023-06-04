using Godot;
using System;

public class Credits : TextureButton
{
	private void onButtonUp()
	{
		GetTree().ChangeScene("res://src/Screens/CreditsScreen.tscn");
	}
}
