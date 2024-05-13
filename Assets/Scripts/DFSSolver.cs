using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSSolver : MonoBehaviour
{
    private GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        print("Test");
        gameState = new GameState(0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0);
        var gameState2 = new GameState(1, 2, 3, 4,
            5, 6, 7, 8, 9, 10, 11, 12);
        print(gameState);
        print(gameState2);
        print(gameState.Equals(gameState));
        print(gameState.Equals(gameState2));
    }

    // Update is called once per frame
    void Update()
    {

    }
}

class GameState : IEquatable<GameState>
{
    private int stack1;
    private int stack2;
    private int stack3;
    private int stack4;
    private int deck1;
    private int deck2;
    private int deck3;
    private int deck4;
    private int deck5;
    private int deck6;
    private int deck7;
    private int deck8;

    public GameState(int stack1,
                     int stack2,
                     int stack3,
                     int stack4,
                     int deck1,
                     int deck2,
                     int deck3,
                     int deck4,
                     int deck5,
                     int deck6,
                     int deck7,
                     int deck8)
    {
        this.stack1 = stack1;
        this.stack2 = stack2;
        this.stack3 = stack3;
        this.stack4 = stack4;
        this.deck1 = deck1;
        this.deck2 = deck2;
        this.deck3 = deck3;
        this.deck4 = deck4;
        this.deck5 = deck5;
        this.deck6 = deck6;
        this.deck7 = deck7;
        this.deck8 = deck8;
    }

    public static bool operator ==(GameState state1, GameState state2)
    {
        if (ReferenceEquals(state1, state2))
            return true;
        if (ReferenceEquals(state1, null))
            return false;
        if (ReferenceEquals(state2, null))
            return false;
        return state1.Equals(state2);
    }
    public static bool operator !=(GameState state1, GameState state2) => !(state1 == state2);

    public bool Equals(GameState other)
    {
        if (ReferenceEquals(other, null))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return stack1.Equals(other.stack1) &&
               stack2.Equals(other.stack2) &&
               stack3.Equals(other.stack3) &&
               stack4.Equals(other.stack4) &&
               deck1.Equals(other.deck1) &&
               deck2.Equals(other.deck2) &&
               deck3.Equals(other.deck3) &&
               deck4.Equals(other.deck4) &&
               deck5.Equals(other.deck5) &&
               deck6.Equals(other.deck6) &&
               deck7.Equals(other.deck7) &&
               deck8.Equals(other.deck8);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as GameState);
    }

    public override string ToString()
    {
        return $"Stacks: {stack1}|{stack2}|{stack3}|{stack4}\nDecks: {deck1}|{deck2}|{deck3}|{deck4}|{deck5}|{deck6}|{deck7}|{deck8}";
    }
}
