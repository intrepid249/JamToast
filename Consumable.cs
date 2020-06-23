using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast
{
    class Consumable : Spawnable, IHealthItem
    {
        protected List<string> DiscoveredFlareTexts = new List<string>();
        protected List<string> ConsumedFlareTexts = new List<string>();
        protected List<string> DeclinedFlareTexts = new List<string>();
        public int NutritionValue { get; protected set; }


        public Consumable(string name, int nutritionValue) : base(name)
        {
            NutritionValue = nutritionValue;
        }



        public void AddDiscoveredFlareText(string text)
        {
            DiscoveredFlareTexts.Add(text);
        }

        public string GetRandomDiscoveredFlare()
        {
            Random FlareIndex = new Random();
            return DiscoveredFlareTexts[FlareIndex.Next(0, DiscoveredFlareTexts.Count)];
        }

        public void AddConsumedFlareText(string text)
        {
            ConsumedFlareTexts.Add(text);
        }

        public string GetRandomConsumedFlare()
        {
            Random FlareIndex = new Random();
            return ConsumedFlareTexts[FlareIndex.Next(0, ConsumedFlareTexts.Count)];
        }

        public void AddDeclinedFlareText(string text)
        {
            DeclinedFlareTexts.Add(text);
        }

        public string GetRandomDeclinedFlare()
        {
            Random FlareIndex = new Random();
            return DeclinedFlareTexts[FlareIndex.Next(0, DeclinedFlareTexts.Count)];
        }

        public void heal(Player player)
        {
            Console.WriteLine("[You gain " + NutritionValue + "health.]");
            player.AddHealth(NutritionValue);
        }
    }
}
