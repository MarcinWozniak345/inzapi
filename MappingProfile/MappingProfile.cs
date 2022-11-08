using Inzynierka_API.Entities;
using Inzynierka_API.Models;
using AutoMapper;

namespace Inzynierka_API.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Opinia, OpiniaDto>()
            //    .ForMember(o => o.KtoOdwiedzil, c => c.MapFrom(s => s.Odwiedzony.OdwiedzonePrzez.Login))
            //    .ForMember(o => o.Miejsce, c => c.MapFrom(s => s.Odwiedzony.OdwiedzonyUrbex.Nazwa))
            //    .ForMember(o => o.Ocena, c => c.MapFrom(s => s.Ocena))
            //    .ForMember(o => o.Tekst, c => c.MapFrom(s => s.Tekst));

            CreateMap<Medicament, GetMedicamentDto>()
                .ForMember(o => o.Login, c => c.MapFrom(s => s.User.Login))
                .ForMember(o => o.Name, c => c.MapFrom(s => s.Name))
                .ForMember(o => o.NumberOfTablets, c => c.MapFrom(s => s.NumberOfTablets))
                .ForMember(o => o.Dose, c => c.MapFrom(s => s.Dose))
                .ForMember(o => o.DailyDosage, c => c.MapFrom(s => s.DailyDosage));


            CreateMap<Friend, ShowInvitationsDto>()
               .ForMember(o => o.login, c => c.MapFrom(s => s.User1.Login));

            CreateMap<TimeTable, GetTimeTableDto>()
                .ForMember(o => o.Medicament, c => c.MapFrom(s => s.Medicament.Name))
                .ForMember(o => o.When, c => c.MapFrom(s => s.When));
        }
    }
}
