using UnityEngine;
using VantasticKick.Config;

namespace VantasticKick.Core
{
    public static class BallDirector
    {
        public static Quaternion GetBallDirectionFromInput(Vector3 valueFromInput)
        {
            float xAngle = 5 + valueFromInput.y * 30;
            float yAngle = valueFromInput.x * 45;
            
            return Quaternion.Euler(-1*xAngle, yAngle,0);
        }

        public static void SetBallVelocity(Rigidbody ball, Vector3 direction, GameplayConfig config)
        {
            ball.rotation = Quaternion.identity;
            var velocity = direction * config.ballVelocity;
            var xScatter = Random.Range(-config.scatterFactor, config.scatterFactor);
            var yScatter = Random.Range(-config.scatterFactor, config.scatterFactor);
            velocity += new Vector3(xScatter, yScatter, 0);
            ball.velocity = velocity;
            ball.angularVelocity = 100 * Vector3.up * direction.y;
        }
    }
}
