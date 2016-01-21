﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLeagueReplayParser.NetworkStream
{
    public class PrivateMatchSettings
    {
        public IEnumerable<string> Mutators { get; private set; }
        public Int32 Unknown1 { get; private set; }
        public Int32 Unknown2 { get; private set; }
        public string GameName { get; private set; }
        public string Password { get; private set; }
        public bool Unknown3 { get; private set; }

        public static PrivateMatchSettings Deserialize(BitReader br)
        {
            var pms = new PrivateMatchSettings();
            pms.Mutators = br.ReadString().Split(',').ToList();
            pms.Unknown1 = br.ReadInt32();
            pms.Unknown2 = br.ReadInt32();
            pms.GameName = br.ReadString();
            pms.Password = br.ReadString();
            pms.Unknown3 = br.ReadBit();

            return pms;
        }
        public override string ToString()
        {
 	         return string.Format("Mutators: [{0}], Unknown1 {1}, Unknown2 {2}, GameName {3}, Password {4}, Unknown3 {5}", string.Join(", ", Mutators), Unknown1, Unknown2, GameName, Password, Unknown3);
        }
    }
}