using CRUD_nota_fiscal.Models;

namespace CRUD_nota_fiscal.Interfaces
{
    public interface INotaFiscalRepositoy
    {
        ICollection<NotaFiscal> GetNotasFiscais();
        ICollection<NotaFiscal> GetNotasFiscaisPorEmissor(string cnpj);
        ICollection<NotaFiscal> GetNotasFiscaisPorDestinatario(string cnpj);
        ICollection<NotaFiscal> GetNotasFiscaisPorData(DateTime data);
        NotaFiscal GetNotaFiscalPorNumero(int numero);
        bool NotaFiscalExiste(int numero);
        bool CnpjEmissorExiste(string cnpj);
        bool CnpjDestinatarioExiste(string cnpj);
        bool NotasFiscaisExistem(DateTime data);
        bool CreateNotaFiscal(NotaFiscal notaFiscal);
        bool UpdateNotaFiscal(NotaFiscal notaFiscal);
        bool DeleteNotaFiscal(int numero);
        bool Save();
    }
}
