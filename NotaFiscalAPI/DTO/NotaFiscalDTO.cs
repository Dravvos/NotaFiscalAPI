namespace CRUD_nota_fiscal.DTO
{
    public class NotaFiscalDTO
    {
        public int NumeroNf { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataNf { get; set; }
        public string CnpjEmissorNf { get; set; }
        public string CnpjDestinatarioNf { get; set; }

    }
}
