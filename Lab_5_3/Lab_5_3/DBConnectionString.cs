namespace Lab_5_3
{
    public static class DBConnectionString
    {
        private static string Server = "vm-dev-erp3";
        private static string DataBase = "develope";

        static DBConnectionString()
        {
            var adressUsing = (DBAdressUsing)UserInteraction.ChooseEnumValue("Choose type of connection parameters", typeof(DBAdressUsing));

            switch (adressUsing)
            {
                case DBAdressUsing.UseDefault:
                    return;

                case DBAdressUsing.EnterByUser:
                    Server = UserInteraction.GetString("Enter server name");
                    DataBase = UserInteraction.GetString("Enter database name");
                    break;
            }
        }
        public static string GetConnectionString()
        {
            return $"Data source = {Server};Initial catalog={DataBase};Integrated Security = true;MultipleActiveResultSets=true";
        }
    }
}
