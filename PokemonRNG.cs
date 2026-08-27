using System;
using System.Collections.Generic;

namespace PokemonRNGGame
{
    class Program
    {
        static Random random = new Random();

        // Dictionary to hold Pokémon by generation with rarity assigned
        static Dictionary<string, string> pokemonList = new Dictionary<string, string>()
        {
            // Generation 1 (sample)
            { "Bulbasaur", "Common" },
            { "Charmander", "Common" },
            { "Squirtle", "Common" },
            { "Pikachu", "Common" },
            { "Snorlax", "Rare" },
            { "Lapras", "Rare" },
            { "Articuno", "Legendary" },
            { "Zapdos", "Legendary" },
            { "Mewtwo", "Legendary" },
            { "Mew", "Legendary" },

            // Generation 2 (sample)
            { "Chikorita", "Common" },
            { "Cyndaquil", "Common" },
            { "Totodile", "Common" },
            { "Togepi", "Common" },
            { "Ampharos", "Rare" },
            { "Scizor", "Rare" },
            { "Lugia", "Legendary" },
            { "Ho-Oh", "Legendary" },
            { "Celebi", "Legendary" },

            // Generation 3 (sample)
            { "Treecko", "Common" },
            { "Torchic", "Common" },
            { "Mudkip", "Common" },
            { "Gardevoir", "Rare" },
            { "Salamence", "Rare" },
            { "Groudon", "Legendary" },
            { "Kyogre", "Legendary" },
            { "Rayquaza", "Legendary" },

            // Generation 4 (sample)
            { "Turtwig", "Common" },
            { "Chimchar", "Common" },
            { "Piplup", "Common" },
            { "Lucario", "Rare" },
            { "Garchomp", "Rare" },
            { "Dialga", "Legendary" },
            { "Palkia", "Legendary" },
            { "Giratina", "Legendary" },

            // Generation 5 (sample)
            { "Snivy", "Common" },
            { "Tepig", "Common" },
            { "Oshawott", "Common" },
            { "Zoroark", "Rare" },
            { "Haxorus", "Rare" },
            { "Reshiram", "Legendary" },
            { "Zekrom", "Legendary" },
            { "Kyurem", "Legendary" },

            // Generation 6 (sample)
            { "Chespin", "Common" },
            { "Fennekin", "Common" },
            { "Froakie", "Common" },
            { "Talonflame", "Rare" },
            { "Goodra", "Rare" },
            { "Xerneas", "Legendary" },
            { "Yveltal", "Legendary" },
            { "Zygarde", "Legendary" },

            // Generation 7 (sample)
            { "Rowlet", "Common" },
            { "Litten", "Common" },
            { "Popplio", "Common" },
            { "Decidueye", "Rare" },
            { "Lycanroc", "Rare" },
            { "Solgaleo", "Legendary" },
            { "Lunala", "Legendary" },
            { "Necrozma", "Legendary" },

            // Generation 8 (sample)
            { "Grookey", "Common" },
            { "Scorbunny", "Common" },
            { "Sobble", "Common" },
            { "Corviknight", "Rare" },
            { "Dragapult", "Rare" },
            { "Zacian", "Legendary" },
            { "Zamazenta", "Legendary" },
            { "Eternatus", "Legendary" },

            // Generation 9 (sample)
            { "Sprigatito", "Common" },
            { "Fuecoco", "Common" },
            { "Quaxly", "Common" },
            { "Armarouge", "Rare" },
            { "Ceruledge", "Rare" },
            { "Koraidon", "Legendary" },
            { "Miraidon", "Legendary" }

        };

        static Dictionary<string, int> playerCollection = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Pokémon Catching Game!");
            Console.WriteLine("Try to catch as many Pokémon as you can!\n");

            bool isPlaying = true;

            while (isPlaying)
            {
                Console.WriteLine("\nChoose an action:", Console.ForegroundColor = ConsoleColor.Yellow);
                Console.WriteLine("1. Attempt to Catch a Pokémon", Console.ForegroundColor = ConsoleColor.Yellow);
                Console.WriteLine("2. View Collection", Console.ForegroundColor = ConsoleColor.Yellow);
                Console.WriteLine("3. Exit Game", Console.ForegroundColor = ConsoleColor.Yellow);
               
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AttemptCatch();
                        break;
                    case "2":
                        Console.ForegroundColor = ConsoleColor.White;
                        ViewCollection();
                        
                        break;
                    case "3":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Thank you for playing!");
                       
                        isPlaying = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.", Console.ForegroundColor = ConsoleColor.Red);
                       
                        break;
                }
            }
        }

        static void AttemptCatch()
        {
            string encounteredPokemon = GetRandomPokemon();
            string rarity = pokemonList[encounteredPokemon];

            Console.WriteLine($"\nA wild {encounteredPokemon} appeared! (Rarity: {rarity})", Console.ForegroundColor = ConsoleColor.White);

            // Simple catch probability based on rarity
            bool caught = false;
            switch (rarity)
            {
                case "Common":
                    caught = random.Next(0, 100) < 70; // 70% chance
                    Console.ForegroundColor = ConsoleColor.White;

                    break;
                case "Rare":
                    caught = random.Next(0, 100) < 30; // 30% chance
                    Console.ForegroundColor = ConsoleColor.Blue;

                    break;
                case "Legendary":
                    caught = random.Next(0, 100) < 10; // 10% chance
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
            }

            if (caught)
            {
                Console.WriteLine($"You caught {encounteredPokemon}!", Console.ForegroundColor = ConsoleColor.Green);
                
                // Add to player collection
                if (playerCollection.ContainsKey(encounteredPokemon))
                {
                    playerCollection[encounteredPokemon]++;
                }
                else
                {
                    playerCollection[encounteredPokemon] = 1;
                }
            }
            else
            {
                Console.WriteLine($"{encounteredPokemon} escaped!", Console.ForegroundColor = ConsoleColor.Red);
                
            }
        }



        static string GetRandomPokemon()
        {
            int index = random.Next(pokemonList.Count);
            string selectedPokemon = new List<string>(pokemonList.Keys)[index];
            return selectedPokemon;
        }




        static void ViewCollection()
        {
            Console.WriteLine("\n--- Your Pokémon Collection ---");

            if (playerCollection.Count == 0)
            {
                Console.WriteLine("You have not caught any Pokémon yet.");
            }
            else
            {
                foreach (var pokemon in playerCollection)
                {
                    Console.WriteLine($"{pokemon.Key} - Caught: {pokemon.Value}");
                }
            }
        }

    }


}