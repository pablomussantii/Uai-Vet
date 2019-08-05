using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;

namespace Vet.Domain
{
    public partial class Turno : IEntity
    {
        public int Id { get; set; }
        public SharedKernel.TipoEspecialidad TipoEspecialidad { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPaciente { get; set; }
        public int IdAtencion { get; set; }
        public int Hora { get; set; }
        public Boolean Abonado { get; set; }
        public Paciente Paciente { get; set; }
        public Atencion Atencion { get; set; }
        public Boolean Cancelado { get; set; }



        public int validarhora(Turno turno)
        {

            if (turno.Hora >= 25 || turno.Hora <= -1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int verificarrepeticion(IRepository<Turno> turnos, Turno turno)
        {
            foreach (var item in turnos.List())
            {
                if (item.Fecha == turno.Fecha && item.Hora == turno.Hora && item.IdAtencion == turno.IdAtencion && item.Cancelado == false)
                {
                    return 1;
                }

            }
            return 0;

        }

        public int verificarcoordinacionconatencion(IRepository<Atencion> atenciones, Turno turno)
        {

            foreach (var item in atenciones.List())
            {
                if (item.Id == turno.IdAtencion)
                {
                    turno.Atencion = item;
                    if (item.HorarioTurno == Domain.SharedKernel.HorarioTurno.Mañana)
                    {
                        if (turno.Hora >= 6 && turno.Hora <= 12)
                        {
                            return 0;
                        }
                    }

                    if (item.HorarioTurno == Domain.SharedKernel.HorarioTurno.Tarde)
                    {
                        if (turno.Hora >= 13 && turno.Hora <= 19)
                        {
                            return 0;
                        }

                    }

                    if (item.HorarioTurno == Domain.SharedKernel.HorarioTurno.Noche)
                    {
                        if (turno.Hora >= 20 && turno.Hora <= 23)
                        {
                            return 0;
                        }
                        if (turno.Hora >= 0 && turno.Hora <= 5)
                        {
                            return 0;
                        }

                    }
                }


            }
            return 1;

        }
    }

    [MetadataType(typeof(TurnoMetadata))]

    public partial class Turno
    {
        public class TurnoMetadata
        {
            [Key]
            [Column(Order = 1)]
            public int Id { get; set; }
            [ForeignKey("Paciente")]
            [Column(Order = 2)]
            [Required]
            public int IdPaciente { get; set; }
            [ForeignKey("Atencion")]
            [Column(Order = 3)]
            [Required]
            public int IdAtencion { get; set; }
            [Required]
            public DateTime Fecha { get; set; }
            [Required]
            public int Hora { get; set; }
            [Required]
            public SharedKernel.TipoEspecialidad TipoEspecialidad { get; set; }
            [Required]
            public Boolean Abonado { get; set; }
            [Required]
            public Boolean Cancelado { get; set; }
        }
    }
}
