using System.ComponentModel;

namespace Tockify.Domain.Enums
{
    public enum StatusTask
    {
        [Description("A Fazer")]
        ToDo,

        [Description("Em Progresso")]
        InProgress,

        [Description("Concluído")]
        Concluded,

        [Description("Cancelado")]
        Canceled,

    }
}
