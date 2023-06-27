using AutoMapper;
using CRUD_nota_fiscal.DTO;
using CRUD_nota_fiscal.Models;

namespace CRUD_nota_fiscal.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<NotaFiscal, NotaFiscalDTO>();
            CreateMap<NotaFiscalDTO, NotaFiscal>();
        }
    }
}
