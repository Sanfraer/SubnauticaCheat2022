using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sub2
{
    [HarmonyPatch(typeof(Oxygen), "RemoveOxygen", MethodType.Normal)]
    class Oxygen_RemoveOxygen
    {
        public static bool Prefix()
        {
            return !CheatMain.setOxygen;
        }
    }

    [HarmonyPatch(typeof(EnergyMixin), "RemoveEnergy", MethodType.Normal)]
    class ConsumeEnergy_remove
    {
        public static void Prefix(ref float amount)
        {
            amount = 0f;
        }
    }

}
