    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoListApi.Model.Enum;

namespace ToDoListApi.Model;

public class Tarefa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(250)]
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public TarefaStatusEnum Status { get; set; } = TarefaStatusEnum.Pendente;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataConclusao { get; set; }
    //public int UsuarioId { get; set; }

}
