using System.ComponentModel;

namespace Tockify.Domain.Enums
{
    public enum TaskItemStatus
    {
        [Description("A Fazer")]
        ToDo,

        [Description("Em Progresso")]
        InProgress,

        [Description("Concluída")]
        Completed,

        [Description("Pendente")]
        Pending,

        [Description("Cancelado")]
        Canceled,

    }
}
