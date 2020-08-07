using Godot;
using System;

public class FinalScore : Label
{
    public override void _Ready()
    {
        Text = GetNode<GlobalValues>("root/GlobalValues").score;
    }
}
