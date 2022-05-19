using System;
using UnityEngine;

namespace VantasticKick.Core.Input
{
    public interface IGameInput
    {
        Action<Vector3> OnTarget { get; set; }
        Action OnStartTargeting { get; set; }
        Action OnTargetRelease { get; set; }
    }
}
