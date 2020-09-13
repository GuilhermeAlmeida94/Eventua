using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventua.API.DTOs
{
    public class EventoDTO
    {
        public int Id { get; set; }
        
        [Required (ErrorMessage="O campo {0} é obrigatório.")]
        public string Local { get; set; }

        [Required (ErrorMessage="O campo {0} é obrigatório.")]
        public string DataEvento { get; set; }

        [Required (ErrorMessage="O campo {0} é obrigatório.")]
        [StringLength (50, MinimumLength=4, ErrorMessage="O campo {0} deve ter entre 4 e 50 caracteres.")]
        public string Tema { get; set; }

        [Required (ErrorMessage="O campo {0} é obrigatório.")]
        [Range (0, 12000, ErrorMessage="O campo {0} tem o valor máximo de 12000.")]
        public int QtdPessoas { get; set; }

        [Required (ErrorMessage="O campo {0} é obrigatório.")]
        public string ImagemURL { get; set; }

        [Required (ErrorMessage="O campo {0} é obrigatório.")]
        public string Telefone { get; set; }

        [Required (ErrorMessage="O campo {0} é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        public List<LoteDTO> Lotes { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; }
        public List<PalestranteDTO> Palestrantes { get; set; }
    }
}