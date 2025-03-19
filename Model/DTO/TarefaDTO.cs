using System.ComponentModel.DataAnnotations;
using ToDoListApi.Model.Enum;

namespace ToDoListApi.Model.DTO;

public class TarefaDTO
{
    [Required]
    public string Nome { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public TarefaStatusEnum Status { get; set; } = TarefaStatusEnum.Pendente;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataConclusao { get; set; }
}
