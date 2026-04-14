using AppCore.Dto;
using AppCore.Models;

namespace AppCore.Module;

// TODO: Automapper (Lab4)
// public class ContactsMappingProfile()
// {
//     CreateMap<Person, PersonDto>()
//         .ForMember(
//             dest => dest.FullName,
//             opt  => opt.MapFrom(
//                 src => $"{src.FirstName} {src.LastName}"))
//         .ForMember(
//             dest => dest.EmployerName,
//             opt  => opt.MapFrom(
//                 src => src.Employer != null ? src.Employer.Name : null));
//     CreateMap<Address, AddressDto>().ReverseMap();
// }