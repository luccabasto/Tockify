using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tockify.Domain.Enums
{
    public enum ToDoStatus
    {
        [Description("A Fazer")]
        ToDo,

        [Description("Em Progresso")]
        InProgress,

        [Description("Concluído")]
        Concluded,

        [Description("Cancelado")]
        Canceled
    }
}
