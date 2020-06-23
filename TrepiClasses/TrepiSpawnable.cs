namespace JamToast.TrepiClasses
{
    class TrepiSpawnable
    {
        public string Name { get; protected set; }                        //name of entity

        public TrepiSpawnable(string name)
        {
            Name = name;
        }
    }
}
