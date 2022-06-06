namespace Furion.Application.Persons;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Person, PersonDto>()
            .Map(dest => dest.PhoneNumber, src => src.PersonDetail.PhoneNumber)
            .Map(dest => dest.QQ, src => src.PersonDetail.QQ);

        config.ForType<PersonInputDto, Person>()
            .Map(dest => dest.PersonDetail.PersonId, src => src.Id.Value)
            .Map(dest => dest.PersonDetail.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.PersonDetail.QQ, src => src.QQ)
            .Map(dest => dest.UpdatedTime, src => DateTimeOffset.Now);
    }
}