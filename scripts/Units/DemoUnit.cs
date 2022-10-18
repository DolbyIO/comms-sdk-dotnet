using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.VisualScripting;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Unity
{
    [UnitTitle("Demo Conference")]
    [UnitCategory("DolbyIO")]
    public class DemoUnit : ConferenceUnit
    {
        [DoNotSerialize]
        public ValueInput SpatialAudio;

        protected override void Definition()
        {
            base.Definition();

            InputTrigger = ControlInputCoroutine(nameof(InputTrigger), Demo);
            OutputTrigger = ControlOutput(nameof(OutputTrigger));

            SpatialAudio = ValueInput<bool>(nameof(SpatialAudio), true);
        }

        private IEnumerator Demo(Flow flow)
        {
            _sdk.Conference.DemoAsync(flow.GetValue<bool>(SpatialAudio)).Wait();
           
            var scale = flow.GetValue<Vector3>(Scale);
            var forward = flow.GetValue<Vector3>(Forward);
            var up = flow.GetValue<Vector3>(Up);
            var right = flow.GetValue<Vector3>(Right);

            _sdk.Conference.SetSpatialEnvironmentAsync
            (
                new System.Numerics.Vector3(scale.x, scale.y, scale.z),
                new System.Numerics.Vector3(forward.x, forward.y, forward.z),
                new System.Numerics.Vector3(up.x, up.y, up.z),
                new System.Numerics.Vector3(right.x, right.y, right.z)
            );

            yield return OutputTrigger;
        }
    }
}