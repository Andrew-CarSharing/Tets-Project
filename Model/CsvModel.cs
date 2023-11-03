namespace WebApplication5.Model;

public class CsvModel
{
    public DateTime tpep_pickup_datetime { get; set; }

    public DateTime tpep_dropoff_datetime { get; set; }

    public byte passenger_count { get; set; }
    
    public float trip_distance { get; set; }
    
    public string store_and_fwd_flag { get; set; }
    
    public short PULocationID { get; set; }

    public short DOLocationID { get; set; }

    public float fare_amount { get; set; }

    public float tip_amount { get; set; }
}