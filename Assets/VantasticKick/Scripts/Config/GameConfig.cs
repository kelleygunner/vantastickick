using Newtonsoft.Json;
using UnityEngine;

namespace VantasticKick.Config
{
    public class GameConfig
    {
        public GameplayConfig gameplay { get; set; }
        public GameRoundConfig gameround { get; set; }
        
        public static GameConfig LoadAt(string path)
        {
            var jsonFile = Resources.Load<TextAsset>(path);
            if (jsonFile == null)
            {
                Debug.LogError("No config file found");
                return null;
            }
            var json = jsonFile.text;
            var config = JsonConvert.DeserializeObject<GameConfig>(json);
            return config;
        }
    }

    public class GameplayConfig
    {
        public float ballVelocity = 1f;
        public float scatterFactor = 1f;
    }

    public class GameRoundConfig
    {
        public int attempts = 10;
        public int basicPoints = 100;
        public int[] comboBonusPoints = {0, 50, 100, 200, 500};
    }
}
