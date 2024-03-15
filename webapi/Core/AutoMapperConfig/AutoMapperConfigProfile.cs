using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using webapi.Core.Dtos.Client;
using webapi.Core.Dtos.Project;
using webapi.Core.Dtos.Proposal;
using webapi.Core.Entities;

namespace webapi.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            CreateMap<CreateClientDto, Client>();
            CreateMap<Client, GetClientDto>();            
            CreateMap<UpdateClientDto, Client>(); 

            CreateMap<CreateProjectDto, Project>();
            CreateMap<Project, GetProjectDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
            CreateMap<UpdateProjectDto, Project>();

            CreateMap<CreateProposalDto, Proposal>();
            CreateMap<Proposal, GetProposalDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));            
            CreateMap<UpdateProposalDto, Proposal>(); 
        }
    }
}
