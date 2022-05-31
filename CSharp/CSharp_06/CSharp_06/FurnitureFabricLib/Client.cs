namespace AbstractFabricLib
{
    public class Client
    {
        private Chair chair;
        private Table table;
        public Client(FurnitureFactory furnitureFactory)
        {
            this.chair = furnitureFactory.CreateChair();
            this.table = furnitureFactory.CreateTable();
        }
    }
}
