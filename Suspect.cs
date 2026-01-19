using System.Collections.Generic;

namespace NOVEL_
{
    public class Suspect
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SuspicionLevel { get; set; }
        public List<string> EvidenceAgainst { get; set; }
        public string Alibi { get; set; }
        public string Motive { get; set; }
        public Dictionary<string, int> Stats { get; set; }

        public Suspect(string name, string description)
        {
            Name = name;
            Description = description;
            SuspicionLevel = 0;
            EvidenceAgainst = new List<string>();
            Alibi = "Не установлено";
            Motive = "Не установлено";
            Stats = new Dictionary<string, int>
            {
                { "opportunity", 0 },
                { "motive", 0 },
                { "means", 0 }
            };
        }

        // Методы работы с уликами
        public void AddEvidence(string evidenceName, int value = 1)
        {
            if (!EvidenceAgainst.Contains(evidenceName))
            {
                EvidenceAgainst.Add(evidenceName);
                SuspicionLevel += value;

                // Ограничиваем максимальный уровень подозрительности
                if (SuspicionLevel > 10) SuspicionLevel = 10;
            }
        }

        public void ResetSuspicion()
        {
            SuspicionLevel = 0;
            EvidenceAgainst.Clear();
        }
    }
}