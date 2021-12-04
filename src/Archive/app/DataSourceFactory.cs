
class DataSourceFactory{
    public IDataSource getDataSource(string name){
        
        if (name == "SQL"){
            return new SQLSource();
        }
        else if (String.Equals(name, "TXT", StringComparison.OrdinalIgnoreCase)){
            return new TxtSource();
        }
        else if(name == "XML"){
            return null;
        }
        else {
            return null;
        }
    }
}