using System;

namespace Eventua.API.DTOs
{
    public class LoteDTO
    {
         public int Id { get; set; }
         public string Nome { get; set; }
         public decimal Preco { get; set; }
         public DateTime? DataInicio { get; set; }
         public DateTime? DataFim { get; set; }
         public int Quantidade { get; set; }
    }
}