namespace RelicCondition
{
    using System.Collections.Generic;
    using UnityEngine;
    using BepInEx;
    using HarmonyLib;
    using System;

    [BepInPlugin(GUID, NAME, VERSION)]
    public class RelicCondition : BaseUnityPlugin
    {
        public const string GUID = "com.ehaugw.reliccondition";
        public const string VERSION = "1.0.1";
        public const string NAME = "RelicCondition";

        internal void Awake()
        {
        }
    }
}