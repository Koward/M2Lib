using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;

namespace m2lib_csharp.m2
{
    /// <summary>
    ///     Static class providing friendly access to AnimationData.dbc
    /// </summary>
    public static class AnimationData
    {
        static AnimationData()
        {
            Fallback = new Dictionary<ushort, ushort>();
            Ids = new Dictionary<string, ushort>();
            PlayThenStop = new HashSet<ushort>();
            PlayBackwards = new HashSet<ushort>();
            var assembly = Assembly.GetExecutingAssembly();
            var embeddedStream = assembly.GetManifestResourceStream("m2lib_csharp.src.csv.AnimationData.csv");
            if (embeddedStream == null) throw new IOException("Could not open embedded ressource AnimationData");
            var csvParser = new TextFieldParser(embeddedStream) {CommentTokens = new[] {"#"}};
            csvParser.SetDelimiters(",");
            csvParser.HasFieldsEnclosedInQuotes = true;
            csvParser.ReadLine(); // Skip first line
            while (!csvParser.EndOfData)
            {
                var fields = csvParser.ReadFields();
                Debug.Assert(fields != null);
                var id = Convert.ToUInt16(fields[0]);
                var name = fields[1];
                var fallback = Convert.ToUInt16(fields[3]);
                Fallback[id] = fallback;
                Ids[name] = id;
            }
            csvParser.Close();
            ushort[] playThenStopValues =
            {
                Ids["Dead"],
                Ids["SitGround"],
                Ids["Sleep"],
                Ids["KneelLoop"],
                Ids["UseStandingLoop"],
                Ids["Drowned"],
                Ids["LootHold"]
            };
            foreach (var value in playThenStopValues) PlayThenStop.Add(value);
            ushort[] playBackwardsValues =
            {
                Ids["Walkbackwards"],
                Ids["SwimBackwards"],
                Ids["SleepUp"],
                Ids["LootUp"]
            };
            foreach (var value in playBackwardsValues) PlayBackwards.Add(value);
        }

        public static IDictionary<ushort, ushort> Fallback { get; }
        public static IDictionary<string, ushort> Ids { get; }
        public static ISet<ushort> PlayThenStop { get; }
        public static ISet<ushort> PlayBackwards { get; }
    }
}