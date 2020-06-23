using System;
using System.Collections.Generic;
using System.Text;

namespace JamToast.TrepiClasses
{
    class TrepiConsumable : TrepiSpawnable, ITrepiHealthItem
    {
        protected List<string> DiscoveredFlairTexts = new List<string>();
        protected List<string> ConsumedFlairTexts = new List<string>();
        protected List<string> DeclinedFlairTexts = new List<string>();
        public int NutritionValue { get; protected set; }

        public TrepiConsumable(string name, int nutritionValue) : base(name)
        {
            NutritionValue = nutritionValue;
        }

        public void Heal(TrepiPlayer player)
        {
            player.AddHealth(NutritionValue);
        }

        public void AddDiscoveredFlairText(string flair)
        {
            DiscoveredFlairTexts.Add(flair);
        }

        public string GetRandomDiscoveredFlair()
        {
            Random flairIndex = new Random();
            return DiscoveredFlairTexts[flairIndex.Next(0, DiscoveredFlairTexts.Count)];
        }

        public void AddConsumedFlairText(string flair)
        {
            ConsumedFlairTexts.Add(flair);
        }

        public string GetRandomConsumedFlair()
        {
            Random flairIndex = new Random();
            return ConsumedFlairTexts[flairIndex.Next(0, ConsumedFlairTexts.Count)];
        }

        public void AddDeclinedFlairText(string flair)
        {
            DeclinedFlairTexts.Add(flair);
        }

        public string GetRandomDeclinedFlair()
        {
            Random flairIndex = new Random();
            return DeclinedFlairTexts[flairIndex.Next(0, DeclinedFlairTexts.Count)];
        }
    }
}
