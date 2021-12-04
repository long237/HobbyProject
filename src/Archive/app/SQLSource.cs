
class SQLSource  : IDataSource{ 
    //private SQLConnection conn;

    // public SQLSource(){
    //     conn = new SQLConnection("usernamePasword");
    // }

    public int ReadData(string cmd){
        try{
            // conn.open();
            // conn.Execute();
            Console.WriteLine("Access a SQL database");
            Console.WriteLine("Select * from archive");

            // while (myReader)
                // Print to console
            // conn.close();
            return 0;
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return -1;
        }
    }

    public int WriteData(string cmd){
        Console.WriteLine("Writing data to a SQL database");
        return 0;
    }

    public int DeleteData() {
        Console.WriteLine("Deleting data from a SQL database");
        return 0;
    }

    public int UpdateData() {
        Console.WriteLine("Update data to a SQL database");
        return 0;
    }
}