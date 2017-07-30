using NoteControl.Source.BusinessLogic;
using NoteControl.Source.MVVM.Model;
using NoteControl.Source.MVVM.ViewModel.Commands;
using NoteControl.Source.MVVM.ViewModel.DataGridRowModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace NoteControl.Source.MVVM.ViewModel 
{
   public class IngNotasAsigViewModel : INotifyPropertyChanged
    {
        private BLProfesores _blProfesores = new BLProfesores();
        private BLAsignaturas _blAsignaturas = new BLAsignaturas();
        public Command ButtonBuscarAsig { get; set; }
        private List<ComboBoxItem> _comboBoxProfesores = new List<ComboBoxItem>();
        public List<ComboBoxItem> ComboBoxProfesores
        {
            get => _comboBoxProfesores; 
            set { _comboBoxProfesores = value;
                NotifyPropertyChanged("ComboBoxProfesores");
            }
        }
        private ComboBoxItem _selectedComboBoxProfesores;

        public ComboBoxItem SelectedComboBoxProfesores
        {
            get => _selectedComboBoxProfesores; 
            set { _selectedComboBoxProfesores = value;
                NotifyPropertyChanged("SelectedComboBoxProfesores"); }
        }


        private List<AsigCursoProfeModel> _dataGridAsigCursoProfe = new List<AsigCursoProfeModel>();
       public List<AsigCursoProfeModel> DataGridAsigCursoProfe
        {
            get => _dataGridAsigCursoProfe; 
            set { _dataGridAsigCursoProfe = value;
                NotifyPropertyChanged("DataGridAsigCursoProfe");
            }
        }
        public IngNotasAsigViewModel()
        {
            CargarComboBox();
            ButtonBuscarAsig = new Command( CargarDataGrid , () => true);
        }

        private void CargarComboBox()
        {
            foreach (Profesor p in _blProfesores.ListarProfesores()) {
             ComboBoxProfesores.Add(new ComboBoxItem() { Content = p.Nombre+" "+p.Apellido, Tag = p.Rut , ToolTip = "Rut: "+p.Rut });
            }
        }
            private void CargarDataGrid()
        {
            DataGridAsigCursoProfe.Clear();
            int rut = int.Parse(_selectedComboBoxProfesores.Tag.ToString());
            foreach (Asignatura asig in _blAsignaturas.ListarAsignaturasPorProfesor(rut)) {
                asig.CursoAsignaturas.ForEach(e => {
                    DataGridAsigCursoProfe.Add(new AsigCursoProfeModel()
                    {
                        CodeCurso = e.CursoCode,
                        NombreCurso = e.Cursos.Nombre,
                        CodeAsignatura = e.AsignaturaCode,
                        NombreAsignatura = e.Asignaturas.Nombre

                    });
                });
            }
            NotifyPropertyChanged("DataGridAsigCursoProfe");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
