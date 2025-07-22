using System.Collections.Generic;

namespace Permabuffs
{
    public static class DB
    {
        public static void Load()
        {
            // Load buff data from storage if needed
        }

        public static void Add(int playerID, int buffID)
        {
            // Add a permanent buff to the database
        }

        public static List<int> Get(int playerID)
        {
            // Retrieve the list of permanent buffs for a player
            return new List<int>();
        }
    }
}