﻿using NoteControl.Source.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteControl.Source.DataAccess.Source
{
   public class DAPerfiles : IDisposable
    {
        private readonly NoteControlContext _db = new NoteControlContext();

        //metodo para agregar un nuevo perfil
        public void CrearPerfil(Perfil perfil) {
            _db.Perfiles.Add(perfil);
            _db.SaveChanges();
        }

        //metodo para listar todos los perfil
        public List<Perfil> ListarPerfiles()
        {
            List<Perfil> lista = _db.Perfiles.ToList();
            return lista;
        }
        public List<Privilegio> ListarPrivilegiosDelPerfil(Perfil perfilUsuario)
        {
            List<Privilegio> list = new List<Privilegio>();
            var query = from privilegios in _db.Privilegios
                        join perfilprivilegio in _db.PerfilesPrivilegios on privilegios.PrivilegioId
                        equals perfilprivilegio.PrivilegioId
                        join perfiles in _db.Perfiles on perfilprivilegio.PerfilId
                        equals perfiles.PerfilId
                        where perfiles.PerfilId == perfilUsuario.PerfilId
                        select privilegios;
            foreach (Privilegio privi in query)
            {
                list.Add(privi);
            }
            return list;
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
