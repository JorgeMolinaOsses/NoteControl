﻿using NoteControl.Source.BusinessLogic;
using NoteControl.Source.MVVM.Model;
using NoteControl.Source.MVVM.ViewModel.Commands;
using NoteControl.Source.MVVM.ViewModel.DataGridRowModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteControl.Source.MVVM.ViewModel
{

    public class MantProfesoresViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public BLProfesores _blProfesores = new BLProfesores();
        private Profesor _profesorEncontrado = null;
        public Command ButtonSaveClick { get; set; }
        public Command ButtonDeleteClick { get; set; }
        public Command ButtonUpdateClick { get; set; }
        private string _textBoxRut;
        public string TextBoxRut
        {
            get
            {
                return _textBoxRut;
            }
            set
            {
                _textBoxRut = value;
                NotifyPropertyChanged("TextBoxRut");
                //consulta si el profesor ya existe
                if (!ProfeExist(_textBoxRut))
                {
                    ChangeEnableButton();
                    ButtonDeleteClick.methodToDetectCanExecute = () => false;
                    ButtonUpdateClick.methodToDetectCanExecute = () => false;
                    _profesorEncontrado = null;
                }
                else
                {
                    //si ya existe desabilita el boton save y activa el update y delete
                    ButtonSaveClick.methodToDetectCanExecute = () => false;
                    ButtonDeleteClick.methodToDetectCanExecute = () => true;
                    ButtonUpdateClick.methodToDetectCanExecute = () => true;
                    //carga los datos del usuario en el formulario
                    CargarDatoProfesor();
                }
            }
        }
        private string _textBoxNombreProfe;
        public string TextBoxNombreProfe
        {
            get
            {
                return _textBoxNombreProfe;
            }
            set
            {
                _textBoxNombreProfe = value;
                NotifyPropertyChanged("TextBoxNombreProfe");
            }
        }
        private string _textBoxApellido;
        public string TextBoxApellido
        {
            get
            {
                return _textBoxApellido;
            }
            set
            {
                _textBoxApellido = value;
                NotifyPropertyChanged("TextBoxApellido");
            }
        }
        private List<ProfeRowModel> _dataGridColumnProfe = new List<ProfeRowModel>();
        public List<ProfeRowModel> DataGridColumnProfe
        {
            get
            {
                return _dataGridColumnProfe;
            }
            set
            {
                _dataGridColumnProfe = value;
                NotifyPropertyChanged("DataGridColumnProfe");
            }
        }
        public MantProfesoresViewModel()
        {
            //constructor
            CargarDataGrid();
            //inicializa los buttons como disabled
            ButtonSaveClick = new Command(SaveClick, () => false);
            ButtonDeleteClick = new Command(DeleteClick, () => false);
            ButtonUpdateClick = new Command(UpdateClick, () => false);
        }

        private void UpdateClick()
        {
            Profesor profesor = new Profesor()
            {
                Nombre = _textBoxNombreProfe,
                Apellido = _textBoxApellido
            };
            _blProfesores.ModificarProfesor(profesor, _textBoxRut);
            CargarDataGrid();
            NotifyPropertyChanged("DataGridColumnProfe");
        }

        private void DeleteClick()
        {
            _blProfesores.EliminarProfesor(_textBoxRut);
            CargarDataGrid();
            NotifyPropertyChanged("DataGridColumnProfe");
            TextBoxRut = "";
            TextBoxApellido = "";
            TextBoxNombreProfe = "";
        }

        private void SaveClick()
        {

            Profesor profe = new Profesor()
            {
                Rut = int.Parse(_textBoxRut),
                Nombre = _textBoxNombreProfe,
                Apellido = _textBoxApellido
            };
            _blProfesores.CrearProfesor(profe);
            CargarDataGrid();
            NotifyPropertyChanged("DataGridColumnProfe");
        }
        private bool ProfeExist(string text)
        {
            foreach (Profesor p in _blProfesores.ListarProfesores())
            {
                if (p.Rut.ToString() == text)
                {
                    _profesorEncontrado = p;
                    return true;
                }
            }
            return false;
        }
        private void CargarDatoProfesor()
        {
            TextBoxNombreProfe = _profesorEncontrado.Nombre;
            TextBoxApellido = _profesorEncontrado.Apellido;
        }
        private void CargarDataGrid()
        {
            DataGridColumnProfe.Clear();
            foreach (Profesor p in _blProfesores.ListarProfesores())
            {
                DataGridColumnProfe.Add(new ProfeRowModel()
                {
                    Rut = p.Rut,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido
                });
            }
        }
        private void ChangeEnableButton()
        {
            if (_textBoxRut.Length > 0)
            {
                ButtonSaveClick.methodToDetectCanExecute = () => true;
            }
            else
            {
                ButtonSaveClick.methodToDetectCanExecute = () => false;
            }
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
