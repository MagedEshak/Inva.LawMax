using AutoMapper;
using Inva.LawMax.Books;

namespace Inva.LawMax;

public class LawMaxApplicationAutoMapperProfile : Profile
{
    public LawMaxApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
