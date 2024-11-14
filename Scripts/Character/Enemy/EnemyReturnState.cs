using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();
        destination = GetPointGlobalPosition(0);
    }
    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);

        characterNode.Agent2DNode.TargetPosition = destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.Agent2DNode.IsNavigationFinished())
        {
            characterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
            return;
        }

        Move();
    }
}
