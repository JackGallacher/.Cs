using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

public class card// card class
{
    private string name;// string value for the card name
    private int firstvalue;// int value for the first card value
    private int secondvalue;// int value for the second card value
    private string thirdvalue;// string value for the third card value

    public card(string name, int firstvalue, int secondvalue, string thirdvalue)
    {
        this.name = name;
        this.firstvalue = firstvalue;
        this.secondvalue = secondvalue;
        this.thirdvalue = thirdvalue;
    }

    public string Name// get : set for the card name
    {
        get { return name; }
        set { name = value; }
    }

    public int Firstvalue// get : set for value 1
    {
        get { return firstvalue; }
        set { firstvalue = value; }
    }

    public int Secondvalue// get : set for value 2
    {
        get { return secondvalue; }
        set { secondvalue = value; }
    }

    public string Thirdvalue// get : set for value 3
    {
        get { return thirdvalue; }
        set { thirdvalue = value; }
    }
}
public static class shuffle// shuffle class
{

    private static Random _rnd = new Random();// creates a new random value

    public static void Shuffle<T>(this List<T> cardlist, int shuffleamount = 5)
    {
        List<T> newList = new List<T>();// creates a new temp list 

        for (int i = 0; i < shuffleamount; i++)// loop that shuffles the objects
        {
            while (cardlist.Count > 0)
            {
                int index = _rnd.Next(cardlist.Count);

                newList.Add(cardlist[index]);

                cardlist.RemoveAt(index);
            }

            cardlist.AddRange(newList);

            newList.Clear();// clears the card temp list

        }
    }
}
public class die// die class
{
    public int player_roll;// int that holds the players die roll

    public die()
    {
        Random roll = new Random();// creates new roll object
        player_roll = roll.Next(6) + 1;// rolls number from 1 to 6
    }
}
public class chance
{
    public int[] chance_deck = new int[16] { 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5 };// array of chance deck
    public int player_pick;// int that hold the players pick

    public chance()
    {
        Random pick = new Random();// creates new pick object
        player_pick = chance_deck[pick.Next(0, chance_deck.Length)];// picks a number from the chance_deck array
    }
}
class Program// program class
{
    static void Main(string[] args)
    {
        bool runprogram = true;// loop that allows program to run multiple times

        while (runprogram == true)// sets value to true
        {
            int processed = 0;// counter to check how many times the card loop has iterated
            int player_value = 0;// the value the player has picked from thir card
            int computer_value = 0;// the corresponding value to that of the player's
            decimal player_score = 0;// holds the players score
            decimal computer_score = 0;//holds the computers score                             
            string player_start;// holds input to start the game
            string player_choice;// holds players catagory choice

            List<card> cardlist = new List<card>();// list of all the cards in the deck
            cardlist.Add(new card("Orc Mage", 6, 10, "Changeable"));
            cardlist.Add(new card("Gnome Warrior", 11, 5, "Changeable"));
            cardlist.Add(new card("Worgan Druid", 9, 9, "Changeable"));
            cardlist.Add(new card("Goblin Rogue", 7, 4, "Changeable"));
            cardlist.Add(new card("Human Hunter", 10, 5, "Changeable"));
            cardlist.Add(new card("Panderan Monk", 8, 4, "Changeable"));
            cardlist.Add(new card("Undead Warlock", 3, 11, "Changeable"));
            cardlist.Add(new card("Night Elf Druid", 6, 6, "Changeable"));
            cardlist.Add(new card("Human Warrior", 13, 2, "Changeable"));
            cardlist.Add(new card("Troll Hunter", 6, 7, "Changeable"));
            cardlist.Shuffle();

            //players hand
            var p1 = cardlist.Take(1);
            var p2 = cardlist.Take(2);
            var p3 = cardlist.Take(3);
            var p4 = cardlist.Take(4);
            var p5 = cardlist.Take(5);
            //computers hand
            var c1 = cardlist.Take(6);
            var c2 = cardlist.Take(7);
            var c3 = cardlist.Take(8);
            var c4 = cardlist.Take(9);
            var c5 = cardlist.Take(10);

            Console.WriteLine("Top Trumps");
            Console.WriteLine("Press 1 to play");
            player_start = Convert.ToString(Console.ReadLine());

            switch (player_start)// switch statment holding player inputs
            {

                case "1":

                    Console.Clear();
                    Console.WriteLine("Press 1, 2, or 3 to select your catagory\n");
                    foreach (var card in p1)
                    {
                        ++processed;
                        Console.WriteLine("Name: {0}\nStrength: {1}\nMana: {2}\nAgility: {3}", card.Name, card.Firstvalue, card.Secondvalue, card.Thirdvalue);// displays current card
                        if (processed == 1) break;
                    }

                    player_choice = Console.ReadLine();
                    if (player_choice != "1" || player_choice != "2" || player_choice != "3")// shows error for wrong input
                    {
                        Console.WriteLine("\nWrong key pressed, Round discarded!");
                    }
                    if (player_choice == "1")
                    {
                        foreach (var card in c1)// displays computers vlaue
                        {
                            ++processed;
                            Console.Clear();
                            Console.WriteLine("Computers Value: {0}", card.Firstvalue);// displays card value
                            computer_value = card.Firstvalue;
                            if (processed == 1) break;
                        }
                        foreach (var card in p1)// displays players value
                        {
                            ++processed;
                            Console.WriteLine("Your cards value: {0}\n", card.Firstvalue);// displays card value
                            player_value = card.Firstvalue;
                            if (processed == 1) break;
                        }

                        if (player_value > computer_value)// compares values
                        {
                            player_score++;
                            Console.WriteLine("Player Wins!\n");// displays player win
                        }
                        else
                        {
                            computer_score++;
                            Console.WriteLine("Computer Wins!\n");// displays computer win
                        }

                        Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);// displays score so far
                    }

                    if (player_choice == "2")
                    {
                        foreach (var card in c1)
                        {
                            ++processed;
                            Console.Clear();
                            Console.WriteLine("Computers Value: {0}", card.Secondvalue);// displays card value
                            computer_value = card.Secondvalue;
                            if (processed == 1) break;
                        }
                        foreach (var card in p1)
                        {
                            ++processed;
                            Console.WriteLine("Your cards value: {0}\n", card.Secondvalue);// shows card value
                            player_value = card.Secondvalue;
                            if (processed == 1) break;
                        }

                        if (player_value > computer_value)// compares score
                        {
                            player_score++;
                            Console.WriteLine("Player Wins!\n");// displays player win
                        }
                        else
                        {
                            computer_score++;
                            Console.WriteLine("Computer Wins!\n");// compares score
                        }

                        Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);// shows current score

                    }

                    if (player_choice == "3")
                    {
                        Console.Clear();// clears console
                        Console.WriteLine("Press 1 to roll a dice and 2 to choose an chance card");// shows players options
                        player_choice = Console.ReadLine();

                        if (player_choice != "1" || player_choice != "2" || player_choice != "3")// wromg input error
                        {
                            Console.WriteLine("\nWrong key pressed, Round discarded!");
                        }

                        if (player_choice == "1")
                        {
                            Console.Clear();// clear console
                            Console.WriteLine("You have chosen to roll a die\n");// shows player choice

                            die proll = new die();
                            chance croll = new chance();
                            Console.Clear();
                            Console.WriteLine("You rolled: {0}", proll.player_roll);// shows player choice
                            Console.WriteLine("The computer chose: {0}\n", croll.player_pick);// shows player choice

                            player_value = proll.player_roll;
                            computer_value = croll.player_pick;

                            if (player_value > computer_value)// compates values
                            {
                                player_score++;
                                Console.WriteLine("Player Wins!N");// player wins
                            }
                            if (player_value < computer_value)// compares values
                            {
                                computer_score++;
                                Console.WriteLine("Computer Wins!\n");// computer wins
                            }
                            if (player_value == computer_value)// compares vlaues
                            {
                                player_score = player_score + 0.5m;
                                computer_score = computer_score + 0.5m;
                                Console.WriteLine("Tie! both players get half a point\n");
                            }

                            Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);// displays score so far

                        }

                        if (player_choice == "2")
                        {
                            Console.Clear();
                            Console.WriteLine("You have chosen to choose a chance card\n");

                            die croll = new die();
                            chance proll = new chance();
                            Console.Clear();
                            Console.WriteLine("You chose: {0}", proll.player_pick);
                            Console.WriteLine("The computer rolled: {0}\n", croll.player_roll);

                            player_value = proll.player_pick;
                            computer_value = croll.player_roll;

                            if (player_value > computer_value)
                            {
                                player_score++;
                                Console.WriteLine("Player Wins!");
                            }
                            if (player_value < computer_value)
                            {
                                computer_score++;
                                Console.WriteLine("Computer Wins!");
                            }
                            if (player_value == computer_value)
                            {
                                player_score = player_score + 0.5m;
                                computer_score = computer_score + 0.5m;
                                Console.WriteLine("Tie! both players get half a point\n");
                            }

                            Console.WriteLine("Score so far:\nPlayer: {0}\nComputer: {1}", player_score, computer_score);

                        }
                    }

                    Console.WriteLine("\npress 2");
                    string next_round = Console.ReadLine();

                    Console.Clear();
                    if (next_round != "2")
                    {
                        Console.WriteLine("\nerror loading game... Press enter to continue...");
                    }
                    if (next_round == "2")
                    {
                        foreach (var card in p2)
                        {
                            ++processed;
                            Console.Clear();
                            Console.WriteLine("Press 1, 2, or 3 to select your catagory\n");
                            Console.WriteLine("Name: {0}\nStrength: {1}\nMana: {2}\nAgility: {3}", card.Name, card.Firstvalue, card.Secondvalue, card.Thirdvalue);
                            if (processed == 1) break;
                        }

                        player_choice = Console.ReadLine();
                        if (player_choice != "1" || player_choice != "2" || player_choice != "3")
                        {
                            Console.WriteLine("\nWrong key pressed, Round discarded!");
                        }
                        if (player_choice == "1")
                        {
                            foreach (var card in c2)
                            {
                                ++processed;
                                Console.Clear();
                                Console.WriteLine("Computers Value: {0}", card.Firstvalue);
                                computer_value = card.Firstvalue;
                                if (processed == 1) break;
                            }
                            foreach (var card in p2)
                            {
                                ++processed;
                                Console.Write("\rYour cards value: {0}", card.Firstvalue);
                                player_value = card.Firstvalue;
                                if (processed == 1) break;
                            }

                            if (player_value > computer_value)
                            {
                                player_score++;
                                Console.WriteLine("\n\nPlayer Wins!\n");


                            }
                            else
                            {
                                computer_score++;
                                Console.WriteLine("\n\nComputer Wins!\n");

                            }

                            Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);

                        }

                        if (player_choice == "2")
                        {
                            foreach (var card in c2)
                            {
                                ++processed;
                                Console.Clear();
                                Console.WriteLine("Computers Value: {0}", card.Secondvalue);
                                computer_value = card.Secondvalue;
                                if (processed == 1) break;
                            }
                            foreach (var card in p2)
                            {
                                ++processed;
                                Console.Write("\rYour cards value: {0}", card.Secondvalue);
                                player_value = card.Secondvalue;
                                if (processed == 1) break;
                            }

                            if (player_value > computer_value)
                            {
                                player_score++;
                                Console.WriteLine("\n\nPlayer Wins!\n");
                            }
                            else
                            {
                                computer_score++;
                                Console.WriteLine("\n\nComputer Wins!\n");
                            }

                            Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);

                        }

                        if (player_choice == "3")
                        {
                            Console.Clear();
                            Console.WriteLine("Press 1 to roll a dice and 2 to choose an chance card");
                            player_choice = Console.ReadLine();
                            if (player_choice != "1" || player_choice != "2" || player_choice != "3")
                            {
                                Console.WriteLine("\nWrong key pressed, Round discarded!");
                            }
                            if (player_choice == "1")
                            {
                                Console.Clear();
                                Console.WriteLine("You have chosen to roll a die\n");

                                die proll = new die();
                                chance croll = new chance();
                                Console.Clear();
                                Console.WriteLine("You rolled: {0}", proll.player_roll);
                                Console.WriteLine("The computer chose: {0}\n", croll.player_pick);

                                player_value = proll.player_roll;
                                computer_value = croll.player_pick;

                                if (player_value > computer_value)
                                {
                                    player_score++;
                                    Console.WriteLine("Player Wins!\n");
                                }
                                if (player_value < computer_value)
                                {
                                    computer_score++;
                                    Console.WriteLine("Computer Wins!\n");
                                }
                                if (player_value == computer_value)
                                {
                                    player_score = player_score + 0.5m;
                                    computer_score = computer_score + 0.5m;
                                    Console.WriteLine("Tie! both players get half a point\n");
                                }

                                Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);

                            }

                            if (player_choice == "2")
                            {
                                Console.Clear();
                                Console.WriteLine("You have chosen to choose a chance card\n");

                                die croll = new die();
                                chance proll = new chance();
                                Console.Clear();
                                Console.WriteLine("You chose: {0}", proll.player_pick);
                                Console.WriteLine("The computer rolled: {0}\n", croll.player_roll);

                                player_value = proll.player_pick;
                                computer_value = croll.player_roll;

                                if (player_value > computer_value)
                                {
                                    player_score++;
                                    Console.WriteLine("Player Wins!\n");
                                }
                                if (player_value < computer_value)
                                {
                                    computer_score++;
                                    Console.WriteLine("Computer Wins!\n");
                                }
                                if (player_value == computer_value)
                                {
                                    player_score = player_score + 0.5m;
                                    computer_score = computer_score + 0.5m;
                                    Console.WriteLine("Tie! both players get half a point\n");
                                }

                                Console.WriteLine("Score so far:\nPlayer: {0}\nComputer: {1}\n", player_score, computer_score);

                            }
                        }
                        Console.WriteLine("\npress 3");
                        next_round = Console.ReadLine();

                        Console.Clear();
                        if (next_round != "3")
                        {
                            Console.WriteLine("\nerror loading game... Press enter to continue...");
                        }
                        if (next_round == "3")
                        {
                            foreach (var card in p3)
                            {
                                ++processed;
                                Console.Clear();
                                Console.WriteLine("Press 1, 2, or 3 to select your catagory\n");
                                Console.WriteLine("Name: {0}\nStrength: {1}\nMana: {2}\nAgility: {3}", card.Name, card.Firstvalue, card.Secondvalue, card.Thirdvalue);
                                if (processed == 1) break;
                            }

                            player_choice = Console.ReadLine();
                            if (player_choice != "1" || player_choice != "2" || player_choice != "3")
                            {
                                Console.WriteLine("\nWrong key pressed, Round discarded!");
                            }
                            if (player_choice == "1")
                            {
                                foreach (var card in c3)
                                {
                                    ++processed;
                                    Console.Clear();
                                    Console.WriteLine("Computers Value: {0}", card.Firstvalue);
                                    computer_value = card.Firstvalue;
                                    if (processed == 1) break;
                                }
                                foreach (var card in p3)
                                {
                                    ++processed;
                                    Console.Write("\rYour cards value: {0}", card.Firstvalue);
                                    player_value = card.Firstvalue;
                                    if (processed == 1) break;
                                }

                                if (player_value > computer_value)
                                {
                                    player_score++;
                                    Console.WriteLine("\n\nPlayer Wins!\n");


                                }
                                else
                                {
                                    computer_score++;
                                    Console.WriteLine("\n\nComputer Wins!\n");

                                }

                                Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);

                            }

                            if (player_choice == "2")
                            {
                                foreach (var card in c2)
                                {
                                    ++processed;
                                    Console.Clear();
                                    Console.WriteLine("Computers Value: {0}", card.Secondvalue);
                                    computer_value = card.Secondvalue;
                                    if (processed == 1) break;
                                }
                                foreach (var card in p2)
                                {
                                    ++processed;
                                    Console.Write("\rYour cards value: {0}", card.Secondvalue);
                                    player_value = card.Secondvalue;
                                    if (processed == 1) break;
                                }

                                if (player_value > computer_value)
                                {
                                    player_score++;
                                    Console.WriteLine("\n\nPlayer Wins!\n");
                                }
                                else
                                {
                                    computer_score++;
                                    Console.WriteLine("\n\nComputer Wins!\n");
                                }

                                Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);

                            }

                            if (player_choice == "3")
                            {
                                Console.Clear();
                                Console.WriteLine("Press 1 to roll a dice and 2 to choose an chance card");
                                if (player_choice != "1" || player_choice != "2" || player_choice != "3")
                                {
                                    Console.WriteLine("\nWrong key pressed, Round discarded!");
                                }
                                player_choice = Console.ReadLine();

                                if (player_choice == "1")
                                {
                                    Console.Clear();
                                    Console.WriteLine("You have chosen to roll a die\n");

                                    die proll = new die();
                                    chance croll = new chance();
                                    Console.Clear();
                                    Console.WriteLine("You rolled: {0}", proll.player_roll);
                                    Console.WriteLine("The computer chose: {0}\n", croll.player_pick);

                                    player_value = proll.player_roll;
                                    computer_value = croll.player_pick;

                                    if (player_value > computer_value)
                                    {
                                        player_score++;
                                        Console.WriteLine("Player Wins!\n");
                                    }
                                    if (player_value < computer_value)
                                    {
                                        computer_score++;
                                        Console.WriteLine("Computer Wins!\n");
                                    }
                                    if (player_value == computer_value)
                                    {
                                        player_score = player_score + 0.5m;
                                        computer_score = computer_score + 0.5m;
                                        Console.WriteLine("Tie! both players get half a point\n");
                                    }

                                    Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);

                                }

                                if (player_choice == "2")
                                {
                                    Console.Clear();
                                    Console.WriteLine("You have chosen to choose a chance card\n");

                                    die croll = new die();
                                    chance proll = new chance();
                                    Console.Clear();
                                    Console.WriteLine("You chose: {0}", proll.player_pick);
                                    Console.WriteLine("The computer rolled: {0}\n", croll.player_roll);

                                    player_value = proll.player_pick;
                                    computer_value = croll.player_roll;

                                    if (player_value > computer_value)
                                    {
                                        player_score++;
                                        Console.WriteLine("Player Wins!\n");
                                    }
                                    if (player_value < computer_value)
                                    {
                                        computer_score++;
                                        Console.WriteLine("Computer Wins!\n");
                                    }
                                    if (player_value == computer_value)
                                    {
                                        player_score = player_score + 0.5m;
                                        computer_score = computer_score + 0.5m;
                                        Console.WriteLine("Tie! both players get half a point\n");
                                    }

                                    Console.WriteLine("Score so far:\nPlayer: {0}\nComputer: {1}\n", player_score, computer_score);

                                }
                            }

                            Console.WriteLine("\npress 4");
                            next_round = Console.ReadLine();

                            Console.Clear();
                            if (next_round != "4")
                            {
                                Console.WriteLine("\nerror loading game... Press enter to continue...");
                            }
                            if (next_round == "4")
                            {
                                foreach (var card in p4)
                                {
                                    ++processed;
                                    Console.Clear();
                                    Console.WriteLine("Press 1, 2, or 3 to select your catagory\n");
                                    Console.WriteLine("Name: {0}\nStrength: {1}\nMana: {2}\nAgility: {3}", card.Name, card.Firstvalue, card.Secondvalue, card.Thirdvalue);
                                    if (processed == 1) break;
                                }

                                player_choice = Console.ReadLine();
                                if (player_choice != "1" || player_choice != "2" || player_choice != "3")
                                {
                                    Console.WriteLine("\nWrong key pressed, Round discarded!");
                                }
                                if (player_choice == "1")
                                {
                                    foreach (var card in c4)
                                    {
                                        ++processed;
                                        Console.Clear();
                                        Console.WriteLine("Computers Value: {0}", card.Firstvalue);
                                        computer_value = card.Firstvalue;
                                        if (processed == 1) break;
                                    }
                                    foreach (var card in p4)
                                    {
                                        ++processed;
                                        Console.Write("\rYour cards value: {0}", card.Firstvalue);
                                        player_value = card.Firstvalue;
                                        if (processed == 1) break;
                                    }

                                    if (player_value > computer_value)
                                    {
                                        player_score++;
                                        Console.WriteLine("\n\nPlayer Wins!\n");


                                    }
                                    else
                                    {
                                        computer_score++;
                                        Console.WriteLine("\n\nComputer Wins!\n");

                                    }

                                    Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);

                                }

                                if (player_choice == "2")
                                {
                                    foreach (var card in c2)
                                    {
                                        ++processed;
                                        Console.Clear();
                                        Console.WriteLine("Computers Value: {0}", card.Secondvalue);
                                        computer_value = card.Secondvalue;
                                        if (processed == 1) break;
                                    }
                                    foreach (var card in p2)
                                    {
                                        ++processed;
                                        Console.Write("\rYour cards value: {0}", card.Secondvalue);
                                        player_value = card.Secondvalue;
                                        if (processed == 1) break;
                                    }

                                    if (player_value > computer_value)
                                    {
                                        player_score++;
                                        Console.WriteLine("\n\nPlayer Wins!\n");
                                    }
                                    else
                                    {
                                        computer_score++;
                                        Console.WriteLine("\n\nComputer Wins!\n");
                                    }

                                    Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);

                                }

                                if (player_choice == "3")
                                {
                                    Console.Clear();
                                    Console.WriteLine("Press 1 to roll a dice and 2 to choose an chance card");
                                    if (player_choice != "1" || player_choice != "2" || player_choice != "3")
                                    {
                                        Console.WriteLine("\nWrong key pressed, Round discarded!");
                                    }
                                    player_choice = Console.ReadLine();

                                    if (player_choice == "1")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("You have chosen to roll a die\n");

                                        die proll = new die();
                                        chance croll = new chance();
                                        Console.Clear();
                                        Console.WriteLine("You rolled: {0}", proll.player_roll);
                                        Console.WriteLine("The computer chose: {0}\n", croll.player_pick);

                                        player_value = proll.player_roll;
                                        computer_value = croll.player_pick;

                                        if (player_value > computer_value)
                                        {
                                            player_score++;
                                            Console.WriteLine("Player Wins!\n");
                                        }
                                        if (player_value < computer_value)
                                        {
                                            computer_score++;
                                            Console.WriteLine("Computer Wins!\n");
                                        }
                                        if (player_value == computer_value)
                                        {
                                            player_score = player_score + 0.5m;
                                            computer_score = computer_score + 0.5m;
                                            Console.WriteLine("Tie! both players get half a point\n");
                                        }

                                        Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);

                                    }

                                    if (player_choice == "2")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("You have chosen to choose a chance card\n");

                                        die croll = new die();
                                        chance proll = new chance();
                                        Console.Clear();
                                        Console.WriteLine("You chose: {0}", proll.player_pick);
                                        Console.WriteLine("The computer rolled: {0}\n", croll.player_roll);

                                        player_value = proll.player_pick;
                                        computer_value = croll.player_roll;

                                        if (player_value > computer_value)
                                        {
                                            player_score++;
                                            Console.WriteLine("Player Wins!\n");
                                        }
                                        if (player_value < computer_value)
                                        {
                                            computer_score++;
                                            Console.WriteLine("Computer Wins!\n");
                                        }
                                        if (player_value == computer_value)
                                        {
                                            player_score = player_score + 0.5m;
                                            computer_score = computer_score + 0.5m;
                                            Console.WriteLine("Tie! both players get half a point\n");
                                        }

                                        Console.WriteLine("Score so far:\nPlayer: {0}\nComputer: {1}\n", player_score, computer_score);

                                    }
                                }

                                Console.WriteLine("\npress 5");
                                next_round = Console.ReadLine();

                                Console.Clear();//. clears console
                                if (next_round != "5")
                                {
                                    Console.WriteLine("\nerror loading game... Press enter to continue...");//wrong input error
                                }
                                if (next_round == "5")
                                {
                                    foreach (var card in p5)
                                    {
                                        ++processed;
                                        Console.Clear();// clears console
                                        Console.WriteLine("Press 1, 2, or 3 to select your catagory\n");// player options
                                        Console.WriteLine("Name: {0}\nStrength: {1}\nMana: {2}\nAgility: {3}", card.Name, card.Firstvalue, card.Secondvalue, card.Thirdvalue);// displays card
                                        if (processed == 1) break;
                                    }

                                    player_choice = Console.ReadLine();
                                    if (player_choice != "1" || player_choice != "2" || player_choice != "3")// shows error for wrong input
                                    {
                                        Console.WriteLine("\nWrong key pressed, Round discarded!");
                                    }
                                    if (player_choice == "1")
                                    {
                                        foreach (var card in c5)
                                        {
                                            ++processed;
                                            Console.Clear();
                                            Console.WriteLine("Computers Value: {0}", card.Firstvalue);// diplays computer value
                                            computer_value = card.Firstvalue;
                                            if (processed == 1) break;
                                        }
                                        foreach (var card in p5)
                                        {
                                            ++processed;
                                            Console.Write("\rYour cards value: {0}", card.Firstvalue);// displays players value
                                            player_value = card.Firstvalue;
                                            if (processed == 1) break;
                                        }

                                        if (player_value > computer_value)// compares values
                                        {
                                            player_score++;
                                            Console.WriteLine("\n\nPlayer Wins!\n");// displays player win


                                        }
                                        else
                                        {
                                            computer_score++;
                                            Console.WriteLine("\n\nComputer Wins!\n");// displays computer win

                                        }

                                        Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);// displays current score

                                    }

                                    if (player_choice == "2")// players choice
                                    {
                                        foreach (var card in c2)
                                        {
                                            ++processed;
                                            Console.Clear();
                                            Console.WriteLine("Computers Value: {0}", card.Secondvalue);// displays computers value
                                            computer_value = card.Secondvalue;
                                            if (processed == 1) break;
                                        }
                                        foreach (var card in p2)
                                        {
                                            ++processed;
                                            Console.Write("\rYour cards value: {0}", card.Secondvalue);// displays player value
                                            player_value = card.Secondvalue;
                                            if (processed == 1) break;
                                        }

                                        if (player_value > computer_value)// compares score
                                        {
                                            player_score++;
                                            Console.WriteLine("\n\nPlayer Wins!\n");// displays player win
                                        }
                                        else
                                        {
                                            computer_score++;
                                            Console.WriteLine("\n\nComputer Wins!\n");// display computer win
                                        }

                                        Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score); // displays current socre

                                    }

                                    if (player_choice == "3")//play choice if
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Press 1 to roll a dice and 2 to choose an chance card");// shows user options
                                        if (player_choice != "1" || player_choice != "2" || player_choice != "3")// shows error for wrong input
                                        {
                                            Console.WriteLine("\nWrong key pressed, Round discarded!");
                                        }
                                        player_choice = Console.ReadLine();// sets coice to readline

                                        if (player_choice == "1")// player choice 1
                                        {
                                            Console.Clear();// clears console
                                            Console.WriteLine("You have chosen to roll a die\n");// displays player choice

                                            die proll = new die();// creates new die
                                            chance croll = new chance();// creates new chace
                                            Console.Clear();// clears console
                                            Console.WriteLine("You rolled: {0}", proll.player_roll);// shows player value
                                            Console.WriteLine("The computer chose: {0}\n", croll.player_pick);// shows computer value

                                            player_value = proll.player_roll;// assigns player vlaue
                                            computer_value = croll.player_pick;// assigns computer value

                                            if (player_value > computer_value)// compares score
                                            {
                                                player_score++;
                                                Console.WriteLine("Player Wins!\n");
                                            }
                                            if (player_value < computer_value)// compares score
                                            {
                                                computer_score++;
                                                Console.WriteLine("Computer Wins!\n");
                                            }
                                            if (player_value == computer_value)// compares score
                                            {
                                                player_score = player_score + 0.5m;
                                                computer_score = computer_score + 0.5m;
                                                Console.WriteLine("Tie! both players get half a point\n");
                                            }

                                            Console.WriteLine("Score so far:\nPlayer:{0}\nComputer: {1}", player_score, computer_score);// shows score so far

                                        }

                                        if (player_choice == "2")// player choice 2
                                        {
                                            Console.Clear();// clears console
                                            Console.WriteLine("You have chosen to choose a chance card\n");// displays method chosen

                                            die croll = new die();// creates new die
                                            chance proll = new chance();// creats new chance
                                            Console.Clear();// clears console
                                            Console.WriteLine("You chose: {0}", proll.player_pick);// displays player value
                                            Console.WriteLine("The computer rolled: {0}\n", croll.player_roll);// displays computer value

                                            player_value = proll.player_pick;// assigns player value
                                            computer_value = croll.player_roll;// assigns computer vlaue

                                            if (player_value > computer_value)// compares score
                                            {
                                                player_score++;
                                                Console.WriteLine("Player Wins!\n");// displays player win
                                            }
                                            if (player_value < computer_value)// compares score
                                            {
                                                computer_score++;
                                                Console.WriteLine("Computer Wins!\n");// displays computer win
                                            }
                                            if (player_value == computer_value)// compares score
                                            {
                                                player_score = player_score + 0.5m;
                                                computer_score = computer_score + 0.5m;
                                                Console.WriteLine("Tie! both players get half a point\n");// diplays tie
                                            }

                                            Console.WriteLine("Score so far:\nPlayer: {0}\nComputer: {1}\n", player_score, computer_score);// shows score so far

                                        }
                                    }

                                    Console.WriteLine("\nGame over!");// shows that the game is over

                                    if (player_score < computer_score)// compares scores
                                    {
                                        Console.WriteLine("\nThe Computer Wins!");// shows if computer wins
                                    }

                                    if (player_score > computer_score)// compares scores
                                    {
                                        Console.WriteLine("\nYou Win!");// shows if player wins
                                    }

                                    if (player_score == computer_score)// compares score
                                    {
                                        Console.WriteLine("\nDraw!");// shows if draw
                                    }
                                }
                            }
                        }
                    }
                    break;
                default:// default case
                    Console.WriteLine("error loading game... Press enter to continue...");// displays when the game has an error
                    break;
            }
            Console.ReadLine();
        }
    }
}

