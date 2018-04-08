﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RocketLeagueReplayParser.NetworkStream;
using System.IO;

namespace RocketLeagueReplayParser.Tests
{
    [TestFixture]
    public class ActorStatePropertyTests
    {
        [TestCaseSource(typeof(ReplayFileSource), nameof(ReplayFileSource.ReplayFiles))]
        public void CameraSettingsTest(string filePath)
        {
            var r = Replay.Deserialize(filePath);

            var cameraSettingsProperties = r.Frames
                .Where(f => f.ActorStates != null)
                .SelectMany(x => x.ActorStates)
                .Where(s => s.Properties != null)
                .SelectMany(s => s.Properties.Values)
                .Where(p => p.PropertyName == "TAGame.PRI_TA:CameraSettings");

            foreach (var p in cameraSettingsProperties)
            {
                var cs = (CameraSettings)p.Data;

                // FOV: 60 - 110
                // Height: 40 - 200
                // Angle: -45 - 0
                // Distance: 100 - 400
                // Stiffness: 0 - 1
                // Swivel Speed: 1 - 10

                Assert.IsTrue(cs.FieldOfView >= 60 && cs.FieldOfView <= 110);
                Assert.IsTrue(cs.Height >= 40 && cs.Height <= 200);
                Assert.IsTrue(cs.Pitch >= -45 && cs.Pitch <= 0);
                Assert.IsTrue(cs.Distance >= 100 && cs.Distance <= 400);
                Assert.IsTrue(cs.Stiffness >= 0 && cs.Stiffness <= 1);
                Assert.IsTrue(cs.SwivelSpeed >= 1 && cs.SwivelSpeed <= 10);
            }
        }
    }
}
