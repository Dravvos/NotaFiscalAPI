using CRUD_nota_fiscal.Data;
using CRUD_nota_fiscal.Interfaces;
using CRUD_nota_fiscal.Models;
using System.Data.Entity;

namespace CRUD_nota_fiscal.Repository
{
    public class NotaFiscalRepository : INotaFiscalRepositoy
    {
        private readonly DataContext _context;
        public NotaFiscalRepository(DataContext context)
        {
            _context = context;
        }
        #region Métodos GET
        public ICollection<NotaFiscal> GetNotasFiscais()
        {
            return _context.NotaFiscal.OrderBy(nf => nf.NumeroNf).ToList();
        }

        public ICollection<NotaFiscal> GetNotasFiscaisPorData(DateTime data)
        {
            return _context.NotaFiscal.Where(nf => nf.DataNf.Date == data.Date).OrderBy(nf=>nf.NumeroNf).ToList();
        }

        public ICollection<NotaFiscal> GetNotasFiscaisPorDestinatario(string cnpj)
        {
            return _context.NotaFiscal.Where(nf => nf.CnpjDestinatarioNf.Contains(cnpj)).OrderBy(nf => nf.NumeroNf).ToList();
        }

        public ICollection<NotaFiscal> GetNotasFiscaisPorEmissor(string cnpj)
        {
            return _context.NotaFiscal.Where(nf => nf.CnpjEmissorNf.Contains(cnpj)).OrderBy(nf => nf.NumeroNf).ToList();
        }
        public NotaFiscal GetNotaFiscalPorNumero(int numero)
        {
            return _context.NotaFiscal.Where(nf => nf.NumeroNf == numero).FirstOrDefault();
        }

        #endregion

        #region Métodos verifica se existe
        public bool NotaFiscalExiste(int numero)
        {
            return _context.NotaFiscal.Any(nf => nf.NumeroNf == numero);
        }

        public bool CnpjEmissorExiste(string cnpj)
        {
            return _context.NotaFiscal.Any(nf => nf.CnpjEmissorNf.Contains(cnpj));
        }

        public bool NotasFiscaisExistem(DateTime data)
        {
            return _context.NotaFiscal.Any(nf => nf.DataNf.Date == data.Date);
        }

        public bool CnpjDestinatarioExiste(string cnpj)
        {
            return _context.NotaFiscal.Any(nf => nf.CnpjDestinatarioNf.Contains(cnpj));
        }
        #endregion


        public bool CreateNotaFiscal(NotaFiscal notaFiscal)
        {
            //context.add serve tanto para adicionar quanto atualizar
            _context.Add(notaFiscal);
            return Save();
        }

        public bool UpdateNotaFiscal(NotaFiscal notaFiscal)
        {
            //context.add serve tanto para adicionar quanto atualizar
            _context.Update(notaFiscal);
            return Save();
        }

        public bool DeleteNotaFiscal(int numero)
        {
            var notaFiscal = GetNotaFiscalPorNumero(numero);
            _context.Remove(notaFiscal);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        
    }
}
