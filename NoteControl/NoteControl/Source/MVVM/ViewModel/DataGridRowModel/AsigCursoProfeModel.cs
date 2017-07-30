using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteControl.Source.MVVM.ViewModel.DataGridRowModel
{
    public class AsigCursoProfeModel
    {
        private string _codeCurso;

        public string CodeCurso
        {
            get { return _codeCurso; }
            set { _codeCurso = value; }
        }
        private string _nombreCurso;

        public string NombreCurso
        {
            get { return _nombreCurso; }
            set { _nombreCurso = value; }
        }
        private string _codeAsignatura;

        public string CodeAsignatura
        {
            get { return _codeAsignatura; }
            set { _codeAsignatura = value; }
        }
        private string _nombreAsignatura;

        public string NombreAsignatura
        {
            get { return _nombreAsignatura; }
            set { _nombreAsignatura = value; }
        }



    }
}
