using Godot;
using System;

namespace SceneEntreced;

public partial class SceneEntreced : Area2D
{
	[Export]
	public string NextScene { private get; set; }
	[Export]
	public Vector2 PlaySpawnPosition { private get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _onBodyEntered(Node body)
	{
		if (body is not Player.Player) return;

		SceneManager.Instance.PlayerSpawnPosition = PlaySpawnPosition;
		Callable.From(() => GetTree().ChangeSceneToFile(NextScene)).CallDeferred();
	}
	
	public void _OnBodyExited(Node body)
	{
		GD.Print("The body exited");
	}
}
