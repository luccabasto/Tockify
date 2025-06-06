

using System.ComponentModel;

namespace Tockify.Domain.Enums
{
    public enum StatusTask
    {
        [Description("A Fazer")]
        ToDo = 1,
        [Description("Em Progresso")]
        InProgress = 2,
        [Description("Concluído")]
        Concluded = 3,
        [Description("Cancelado")]
        Canceled = 4,

    }
}
