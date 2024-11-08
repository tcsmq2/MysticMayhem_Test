using Godot;
using System;

public partial class PlayerIdleState : Node
{
    private Player characterNode;
    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.velocity = characterNode.Velocity;
        //Resetting the character movement to 0 for the idle state
        characterNode.velocity.X = 0;

        // Add the gravity.
        if (!characterNode.IsOnFloor())
        {
            characterNode.velocity += characterNode.GetGravity() * (float)delta;
        }

        characterNode.direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_JUMP, "ui_down");
        if (characterNode.direction != Vector2.Zero && characterNode.direction.Y == 0)
        {
            characterNode.stateMachineNode.SwitchState<PlayerMoveState>();
        }

        //Switch to the Jump State
        if (Input.IsActionJustPressed(GameConstants.INPUT_JUMP) && characterNode.IsOnFloor())
        {
            characterNode.stateMachineNode.SwitchState<PlayerJumpState>();
        }

        characterNode.Velocity = characterNode.velocity;

        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == 5001)
        {
            characterNode.animationPlayerNode.Play(GameConstants.ANIM_IDLE);
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == 5002)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            //GD.Print("Player attack state - input detected");
            characterNode.stateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
}
