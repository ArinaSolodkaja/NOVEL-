using System;
using System.Collections.Generic;

namespace NOVEL_
{
    public class PlayerState
    {
        public string SceneId { get; private set; }
        public List<string> Inventory { get; } = new List<string>();
        public Dictionary<string, int> Stats { get; } = new Dictionary<string, int>();

        public int Attention => GetStat("attention");
        public int Logic => GetStat("logic");

        public PlayerState(string startScene)
        {
            SceneId = startScene;
            Stats["attention"] = 1;
            Stats["logic"] = 1;
        }

        public void ChangeStat(string stat, int value)
        {
            if (!Stats.ContainsKey(stat))
                Stats[stat] = 0;

            Stats[stat] += value;
            if (Stats[stat] > 5) Stats[stat] = 5;
            if (Stats[stat] < 0) Stats[stat] = 0;
        }

        public int GetStat(string stat)
        {
            return Stats.ContainsKey(stat) ? Stats[stat] : 0;
        }
        // изм 12 
        public void AddEvidence(string evidence)
        {
            if (!Inventory.Contains(evidence))
                Inventory.Add(evidence);
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Сцена: {SceneId}");
            Console.WriteLine($"Внимательность: {Attention}/5");
            Console.WriteLine($"Логика: {Logic}/5");
            Console.WriteLine($"Улики: {string.Join(", ", Inventory)}");
        }
    }
}