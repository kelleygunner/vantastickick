using VantasticKick.Core;
using VantasticKick.UI.UiFramework;

namespace VantasticKick.UI
{
    public class FinishScreenModel : UiModel
    {
        public FinishScreenModel(GameRoundModel gameRoundModel)
        {
            Score = gameRoundModel.Score;
        }
        public int Score { get; private set; }
    }
}
