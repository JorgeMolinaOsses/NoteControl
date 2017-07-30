using NoteControl.Source.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteControl.Source.DataAccess.Source
{
    public class DAAsignaturas : IDisposable
    {
        private readonly NoteControlContext _db = new NoteControlContext();


        //metodo para agregar una asignatura
        public void CrearAsignatura(Asignatura asignatura)
        {
            _db.Asignaturas.Add(asignatura);
            _db.SaveChanges();
        }

        //metodo para listar todos las asignaturas
        public List<Asignatura> ListarAsignaturas()
        {
            List<Asignatura> lista = _db.Asignaturas.ToList();
            return lista;
        }
        public List<Asignatura> ListarAsignaturasPorProfesor(int rut)
        {
            List<Asignatura> list = new List<Asignatura>();
            var li = (from profe in _db.Profesores
                      join cpa in _db.CursoProfeAsignaturas
                         on profe.Rut equals cpa.Rut
                      join cur in _db.Cursos
 on cpa.CursoCode equals cur.CursoCode
                      join asig in _db.Asignaturas
 on cpa.AsignaturaCode equals asig.AsignaturaCode
                      where profe.Rut == rut
                      select asig).ToList();
       
            foreach (Asignatura a in li)
            {
              list.Add(a);
            }
            return list;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}