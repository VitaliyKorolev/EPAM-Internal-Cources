namespace AbstractFabricLib
{
    public class KitchenFurnitureFactory : FurnitureFactory
    {
        public override Chair CreateChair()
        {
            return new KitchenChair();
        }
        public override Table CreateTable()
        {
            return new KitchenTable();
        }
    }
}
