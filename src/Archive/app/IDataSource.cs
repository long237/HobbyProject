
interface IDataSource{

    // Method for reading data, return 0 for sucessful operation
    int ReadData(string cmd);

    // Method for Writing data to a data source
    int WriteData(string cmd);

    //Method for deleteing data from a data source, 0 for successful
    int DeleteData();

    // Method for updating data from a data source, 0 for sucessful
    int UpdateData();
}