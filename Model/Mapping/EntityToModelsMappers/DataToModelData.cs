using AutoMapper;
using WebApplication5.Context;

namespace WebApplication5.Model.Mapping.EntityToModelsMappers;

public class DataToModelData: Profile
{
    public DataToModelData()
    {
        CreateMap<Data, DataModel>()
            .ForMember(dst => dst.id, opt => opt.MapFrom(src => src.id))
            .ForMember(dst => dst.tpep_pickup_datetime, opt => opt.MapFrom(src => src.tpep_pickup_datetime))
            .ForMember(dst => dst.tpep_dropoff_datetime, opt => opt.MapFrom(src => src.tpep_dropoff_datetime))
            .ForMember(dst => dst.passenger_count, opt => opt.MapFrom(src => src.passenger_count))
            .ForMember(dst => dst.trip_distance, opt => opt.MapFrom(src => src.trip_distance))
            .ForMember(dst => dst.store_and_fwd_flag, opt => opt.MapFrom(src => src.store_and_fwd_flag))
            .ForMember(dst => dst.PULocationID, opt => opt.MapFrom(src => src.PULocationID))
            .ForMember(dst => dst.DOLocationID, opt => opt.MapFrom(src => src.DOLocationID))
            .ForMember(dst => dst.fare_amount, opt => opt.MapFrom(src => src.fare_amount))
            .ForMember(dst => dst.tip_amount, opt => opt.MapFrom(src => src.tip_amount));
    }
}