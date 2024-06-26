using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DFSSolver : MonoBehaviour
{
    private GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        var currentState = new GameState(
            123456789,
            123456789,
            1234567,
            123456789,
            0,
            0,
            0,
            0,
            89,
            0,
            0,
            0,
            0,
            0,
            0,
            0);
        currentState = GameState.NewStartState();
        print(currentState);
        var nextMoves = currentState.nextPossibleMoves();
        var path = currentState.getPathToSolution();
        print("Path length: " + path.Length);
        foreach (var move in path)
        {
            print(move + "\n");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

class GameState : IEquatable<GameState>
{
    private int depthLimit;

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
    private int pile1;
    private int pile2;
    private int pile3;
    private int pile4;

    private static readonly System.Random rnd = new System.Random();

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
                     int deck8,
                     int pile1,
                     int pile2,
                     int pile3,
                     int pile4)
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
        this.pile1 = pile1;
        this.pile2 = pile2;
        this.pile3 = pile3;
        this.pile4 = pile4;
    }

    public GameState[] getPathToSolution()
    {
        depthLimit = 50;
        GameState[] path = Array.Empty<GameState>();
        for (int i = 0; i < 4; i++)
        {
            path = getPathToSolutionPriv();

            if (path.Length > 0)
            {
                return path;
            }

            depthLimit += 50;
        }

        return path;
    }
    private GameState[] getPathToSolutionPriv()
    {
        int depth = 1;

        List<GameState> path = new List<GameState>();

        if (this.IsVictoryState())
        {
            return path.ToArray();
        }

        foreach (var nextState in this.nextPossibleMoves())
        {
            if (nextState.IsVictoryState())
            {
                path.Add(nextState);
                return path.ToArray();
            }

            var states = nextState.getPathToSolutionRecursive(depth + 1);
            if (states.Length > 0)
            {
                path.AddRange(states);
                break;
            }
        }

        path.Reverse();
        return path.ToArray();
    }

    private GameState[] getPathToSolutionRecursive(int depth)
    {
        List<GameState> path = new List<GameState>();
        if (depth > depthLimit)
        {
            return path.ToArray();
        }

        foreach (var nextState in this.nextPossibleMoves())
        {
            if (nextState.IsVictoryState())
            {
                path.Add(nextState);
                path.Add(this);
                return path.ToArray();
            }

            var states = nextState.getPathToSolutionRecursive(depth + 1);
            if (states.Length > 0)
            {
                path.AddRange(states);
                path.Add(this);
                break;
            }
        }

        return path.ToArray();
    }

    private bool IsVictoryState()
    {
        return this.deck1 == 0 &&
               this.deck2 == 0 &&
               this.deck3 == 0 &&
               this.deck4 == 0 &&
               this.deck5 == 0 &&
               this.deck6 == 0 &&
               this.deck7 == 0 &&
               this.deck8 == 0 &&
               this.stack1 == 123456789 &&
               this.stack2 == 123456789 &&
               this.stack3 == 123456789 &&
               this.stack4 == 123456789 &&
               this.pile1 == 0 &&
               this.pile2 == 0 &&
               this.pile3 == 0 &&
               this.pile4 == 0;
    }

    public GameState[] nextPossibleMoves()
    {
        List<GameState> states = new List<GameState>();

        int expectedCardStack1 = (stack1 % 10) + 1;
        int expectedCardStack2 = (stack2 % 10) + 1;
        int expectedCardStack3 = (stack3 % 10) + 1;
        int expectedCardStack4 = (stack4 % 10) + 1;

        /* Deck 1 */
        int topCard = deck1 % 10;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.deck1 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.deck1 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.deck1 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.deck1 /= 10;
                states.Add(newState);
            }

            if (pile1 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile1 += topCard;
                newState.deck1 /= 10;
                states.Add(newState);
            }

            if (pile2 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile2 += topCard;
                newState.deck1 /= 10;
                states.Add(newState);
            }

            if (pile3 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile3 += topCard;
                newState.deck1 /= 10;
                states.Add(newState);
            }

            if (pile4 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile4 += topCard;
                newState.deck1 /= 10;
                states.Add(newState);
            }
        }

        /* Deck 2 */
        topCard = deck2 % 10;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.deck2 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.deck2 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.deck2 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.deck2 /= 10;
                states.Add(newState);
            }

            if (pile1 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile1 += topCard;
                newState.deck2 /= 10;
                states.Add(newState);
            }

            if (pile2 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile2 += topCard;
                newState.deck2 /= 10;
                states.Add(newState);
            }

            if (pile3 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile3 += topCard;
                newState.deck2 /= 10;
                states.Add(newState);
            }

            if (pile4 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile4 += topCard;
                newState.deck2 /= 10;
                states.Add(newState);
            }
        }

        /* Deck 3 */
        topCard = deck3 % 10;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.deck3 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.deck3 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.deck3 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.deck3 /= 10;
                states.Add(newState);
            }

            if (pile1 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile1 += topCard;
                newState.deck3 /= 10;
                states.Add(newState);
            }

            if (pile2 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile2 += topCard;
                newState.deck3 /= 10;
                states.Add(newState);
            }

            if (pile3 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile3 += topCard;
                newState.deck3 /= 10;
                states.Add(newState);
            }

            if (pile4 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile4 += topCard;
                newState.deck3 /= 10;
                states.Add(newState);
            }
        }

        /* Deck 4 */
        topCard = deck4 % 10;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.deck4 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.deck4 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.deck4 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.deck4 /= 10;
                states.Add(newState);
            }

            if (pile1 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile1 += topCard;
                newState.deck4 /= 10;
                states.Add(newState);
            }

            if (pile2 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile2 += topCard;
                newState.deck4 /= 10;
                states.Add(newState);
            }

            if (pile3 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile3 += topCard;
                newState.deck4 /= 10;
                states.Add(newState);
            }

            if (pile4 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile4 += topCard;
                newState.deck4 /= 10;
                states.Add(newState);
            }
        }

        /* Deck 5 */
        topCard = deck5 % 10;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.deck5 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.deck5 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.deck5 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.deck5 /= 10;
                states.Add(newState);
            }

            if (pile1 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile1 += topCard;
                newState.deck5 /= 10;
                states.Add(newState);
            }

            if (pile2 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile2 += topCard;
                newState.deck5 /= 10;
                states.Add(newState);
            }

            if (pile3 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile3 += topCard;
                newState.deck5 /= 10;
                states.Add(newState);
            }

            if (pile4 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile4 += topCard;
                newState.deck5 /= 10;
                states.Add(newState);
            }
        }

        /* Deck 6 */
        topCard = deck6 % 10;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.deck6 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.deck6 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.deck6 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.deck6 /= 10;
                states.Add(newState);
            }

            if (pile1 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile1 += topCard;
                newState.deck6 /= 10;
                states.Add(newState);
            }

            if (pile2 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile2 += topCard;
                newState.deck6 /= 10;
                states.Add(newState);
            }

            if (pile3 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile3 += topCard;
                newState.deck6 /= 10;
                states.Add(newState);
            }

            if (pile4 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile4 += topCard;
                newState.deck6 /= 10;
                states.Add(newState);
            }
        }

        /* Deck 7 */
        topCard = deck7 % 10;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.deck7 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.deck7 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.deck7 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.deck7 /= 10;
                states.Add(newState);
            }

            if (pile1 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile1 += topCard;
                newState.deck7 /= 10;
                states.Add(newState);
            }

            if (pile2 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile2 += topCard;
                newState.deck7 /= 10;
                states.Add(newState);
            }

            if (pile3 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile3 += topCard;
                newState.deck7 /= 10;
                states.Add(newState);
            }

            if (pile4 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile4 += topCard;
                newState.deck7 /= 10;
                states.Add(newState);
            }
        }

        /* Deck 8 */
        topCard = deck8 % 10;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.deck8 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.deck8 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.deck8 /= 10;
                states.Add(newState);
            }

            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.deck8 /= 10;
                states.Add(newState);
            }

            if (pile1 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile1 += topCard;
                newState.deck8 /= 10;
                states.Add(newState);
            }

            if (pile2 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile2 += topCard;
                newState.deck8 /= 10;
                states.Add(newState);
            }

            if (pile3 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile3 += topCard;
                newState.deck8 /= 10;
                states.Add(newState);
            }

            if (pile4 == 0)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.pile4 += topCard;
                newState.deck8 /= 10;
                states.Add(newState);
            }
        }

        /* Pile 1 */
        topCard = pile1;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck1 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck1 *= 10;
                newState.deck1 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck2 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck2 *= 10;
                newState.deck2 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck3 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck3 *= 10;
                newState.deck3 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck4 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck4 *= 10;
                newState.deck4 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck5 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck5 *= 10;
                newState.deck5 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck6 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck6 *= 10;
                newState.deck6 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck7 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck7 *= 10;
                newState.deck7 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck8 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck8 *= 10;
                newState.deck8 += topCard;
                newState.pile1 -= topCard;
                states.Add(newState);
            }
        }
        /* Pile 2 */
        topCard = pile2;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck1 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck1 *= 10;
                newState.deck1 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck2 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck2 *= 10;
                newState.deck2 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck3 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck3 *= 10;
                newState.deck3 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck4 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck4 *= 10;
                newState.deck4 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck5 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck5 *= 10;
                newState.deck5 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck6 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck6 *= 10;
                newState.deck6 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck7 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck7 *= 10;
                newState.deck7 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck8 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck8 *= 10;
                newState.deck8 += topCard;
                newState.pile2 -= topCard;
                states.Add(newState);
            }
        }
        /* Pile 3 */
        topCard = pile3;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck1 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck1 *= 10;
                newState.deck1 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck2 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck2 *= 10;
                newState.deck2 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck3 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck3 *= 10;
                newState.deck3 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck4 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck4 *= 10;
                newState.deck4 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck5 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck5 *= 10;
                newState.deck5 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck6 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck6 *= 10;
                newState.deck6 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck7 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck7 *= 10;
                newState.deck7 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck8 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck8 *= 10;
                newState.deck8 += topCard;
                newState.pile3 -= topCard;
                states.Add(newState);
            }
        }
        /* Pile 4 */
        topCard = pile4;
        if (topCard != 0)
        {
            if (topCard == expectedCardStack1)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack1 *= 10;
                newState.stack1 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack2)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack2 *= 10;
                newState.stack2 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack3)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack3 *= 10;
                newState.stack3 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
            if (topCard == expectedCardStack4)
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.stack4 *= 10;
                newState.stack4 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck1 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck1 *= 10;
                newState.deck1 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck2 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck2 *= 10;
                newState.deck2 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck3 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck3 *= 10;
                newState.deck3 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck4 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck4 *= 10;
                newState.deck4 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck5 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck5 *= 10;
                newState.deck5 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck6 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck6 *= 10;
                newState.deck6 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck7 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck7 *= 10;
                newState.deck7 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
            if (topCard == (deck8 + 1))
            {
                var newState = this.MemberwiseClone() as GameState;
                newState.deck8 *= 10;
                newState.deck8 += topCard;
                newState.pile4 -= topCard;
                states.Add(newState);
            }
        }

        return states.ToArray();
    }

    public static GameState NewStartState()
    {
        List<int> cards = new List<int>() {
            1, 1, 1, 1,
            2, 2, 2, 2,
            3, 3, 3, 3,
            4, 4, 4, 4,
            5, 5, 5, 5,
            6, 6, 6, 6,
            7, 7, 7, 7,
            8, 8, 8, 8,
            9, 9, 9, 9
        };
        List<int> shuffled = cards.OrderBy(item => rnd.Next()).ToList();

        GameState gameState = new GameState(
            0, 0, 0, 0,
            Int32.Parse(string.Join("", shuffled.GetRange(0, 5))),
            Int32.Parse(string.Join("", shuffled.GetRange(5, 5))),
            Int32.Parse(string.Join("", shuffled.GetRange(10, 5))),
            Int32.Parse(string.Join("", shuffled.GetRange(15, 5))),
            Int32.Parse(string.Join("", shuffled.GetRange(20, 4))),
            Int32.Parse(string.Join("", shuffled.GetRange(24, 4))),
            Int32.Parse(string.Join("", shuffled.GetRange(28, 4))),
            Int32.Parse(string.Join("", shuffled.GetRange(32, 4))),
            0, 0, 0, 0
            );

        return gameState;
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
               deck8.Equals(other.deck8) &&
               pile1.Equals(other.pile1) &&
               pile2.Equals(other.pile2) &&
               pile3.Equals(other.pile3) &&
               pile4.Equals(other.pile4);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as GameState);
    }

    public override string ToString()
    {
        return $"Stacks: {stack1}|{stack2}|{stack3}|{stack4}\nDecks: {deck1}|{deck2}|{deck3}|{deck4}|{deck5}|{deck6}|{deck7}|{deck8}\nPiles: {pile1}|{pile2}|{pile3}|{pile4}";
    }
}
