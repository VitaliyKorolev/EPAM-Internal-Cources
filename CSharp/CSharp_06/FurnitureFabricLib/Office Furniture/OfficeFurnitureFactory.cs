namespace AbstractFabricLib
{
    public class OfficeFurnitureFactory:FurnitureFactory
    {
        public override Chair CreateChair()
        {
            return new OfficeChair();
        }
        public override Table CreateTable()
        {
            return new OfficeTable();
        }
    }
}
