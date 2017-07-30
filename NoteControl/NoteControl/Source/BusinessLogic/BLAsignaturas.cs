using NoteControl.Source.DataAccess.Source;
using NoteControl.Source.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteControl.Source.BusinessLogic
{
   public class BLAsignaturas
    {
        DAAsignaturas _da = new DAAsignaturas();
        //metodo para agregar una asignatura
        public void CrearAsignatura(Asignatura asig)
        {
            try
            {
                _da.CrearAsignatura(asig);
                MessageBox.Show("Asignatura Creada Exitosamente!");
            }
            catch 
            {
                throw;
            }

        }
        public List<Asignatura> ListarAsignaturasPorProfesor(int rut) {

            try
            {
               return _da.ListarAsignaturasPorProfesor(rut);
            }
            catch
            {
                throw;
            }
        }
        //metodo para listar todos las asignaturas
        public List<Asignatura> ListarAsignaturas()
        {
            try
            {
                return _da.ListarAsignaturas();
            }
            catch
            {
                throw;
            }
        }
    }
}
