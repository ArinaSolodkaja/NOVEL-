using System;
using System.Collections.Generic;

namespace NOVEL_
{
    public class Choice
    {
        public string ChoiceId { get; }
        public string Text { get; set; }
        public string NextSceneId { get; }
        public string ParentSceneId { get; }
        public bool IsSecret { get; set; }
        public Dictionary<string, int> Requirements { get; } = new Dictionary<string, int>();

        public Choice(string id, string choiceText, string nextScene, string parentScene)
        {
            ChoiceId = id;
            Text = choiceText;
            NextSceneId = nextScene;
            ParentSceneId = parentScene;
            IsSecret = false;
        }
        // изм 11 
        public bool IsAvailable(PlayerState player)
        {
            if (player.SceneId != ParentSceneId) return false;

            foreach (var req in Requirements)
            {
                if (req.Key == "attention" && player.Attention < req.Value) return false;
                if (req.Key == "logic" && player.Logic < req.Value) return false;
            }
            
        }

        public void AddRequirement(string stat, int minValue)
        {
            Requirements[stat] = minValue;
        }


        //привет
    }
}
