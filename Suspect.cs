using System.Collections.Generic;

namespace NOVEL_
{
    public class Suspect
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SuspicionLevel { get; set; }
        public List<string> EvidenceAgainst { get; set; }
        // изм 5 
        public Suspect(string name, string description)
        {
            Name = name;
            Description = description;
            SuspicionLevel = 0;
            EvidenceAgainst = new List<string>();
        }

        public void AddEvidence(string evidenceName, int value = 1)
        {
            if (!EvidenceAgainst.Contains(evidenceName))
            {
                EvidenceAgainst.Add(evidenceName);
                SuspicionLevel += value;
            }
        }

        public void ResetSuspicion()
        {
            SuspicionLevel = 0;
            EvidenceAgainst.Clear();
        }
    }
}