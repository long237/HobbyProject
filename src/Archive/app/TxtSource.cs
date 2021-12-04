
class TxtSource : IDataSource{
    private string fileName;
    public int ReadData(string a){
        try{
            Console.WriteLine("Access a text file");
            return 0;
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return -1;
        }
    }

    public int WriteData(string cmd){
        Console.WriteLine("Writing data to a text file");
        return 0;
    }

    public int DeleteData() {
        Console.WriteLine("Deleting data from a text file");
        return 0;
    }

    public int UpdateData() {
        Console.WriteLine("Update data to a text file");
        return 0;
    }
}