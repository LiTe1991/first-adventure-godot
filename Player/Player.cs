using Godot;

namespace Player;

public partial class Player : CharacterBody2D
{
	[Export]
	private float MoveSpeed = 70;
	[Export]
	private float PushStreng = 5;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = SceneManager.Instance.PlayerSpawnPosition; 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 moveVector = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		Velocity = moveVector * MoveSpeed;

		PlayAnimationForDirection();
		CheckCollision();

		MoveAndSlide();
	}

	private void PlayAnimationForDirection()
	{
		AnimatedSprite2D playNode = GetNode<AnimatedSprite2D>("MoveAnimation");

		if (Velocity.X > 0)
		{
			playNode.Play("move_right");
		}
		else if (Velocity.X < 0)
		{
			playNode.Play("move_left");
		}
		else if (Velocity.Y > 0)
		{
			playNode.Play("move_down");
		}
		else if (Velocity.Y < 0)
		{
			playNode.Play("move_up");
		}
		else
		{
			playNode.Stop();
		}
	}
	
	private void CheckCollision()
    {
		KinematicCollision2D collision2D = GetLastSlideCollision();
		if (collision2D == null) return;

		RigidBody2D collider = collision2D.GetCollider() as RigidBody2D;
		if (collider == null) return;

		Vector2 collisionVector = collision2D.GetNormal();
		collider.ApplyCentralForce(collisionVector * PushStreng * -1);
    }
}
