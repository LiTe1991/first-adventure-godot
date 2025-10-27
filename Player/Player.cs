using Godot;

namespace Player;

public partial class Player : CharacterBody2D
{
	private const float MoveSpeed = 100;

	private Vector2 moveVector;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = SceneManager.Instance.PlayerSpawnPosition; 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		moveVector = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		Velocity = moveVector * MoveSpeed;

		PlayAnimationForDirection();

		MoveAndSlide();
	}
	
	private void PlayAnimationForDirection()
	{
		AnimatedSprite2D playNode = GetNode<AnimatedSprite2D>("MoveAnimation");

        if(Velocity.X > 0)
        {
			playNode.Play("move_right");
        } else if(Velocity.X < 0)
        {
            playNode.Play("move_left");
        } else if(Velocity.Y > 0)
        {
            playNode.Play("move_down");
        } else if(Velocity.Y < 0)
        {
            playNode.Play("move_up");
        } else
        {
			playNode.Stop();
        }
    }
}
