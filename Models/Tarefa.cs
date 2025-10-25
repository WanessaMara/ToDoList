
namespace ToDoList.Models
{
    public class Tarefa 
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty; //Inicializa com string vazia
        public string? Descricao { get; set; } //Pode ser nulo usando ?
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public bool Concluida { get; set; } = false; //Inicializa como false

    }
}