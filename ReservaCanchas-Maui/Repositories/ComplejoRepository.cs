﻿using ReservaCanchas_Maui.Interfaces;
using ReservaCanchas_Maui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReservaCanchas_Maui.Repositories
{
    public class ComplejoRepository : IComplejoRepository
    {
        public string _dbPath;
        public string? StatusMessage { get; set; }

        private SQLiteConnection? conn;
        public ComplejoRepository(string dbpath)
        {
            _dbPath = dbpath;
        }

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Complejo>();
        }
        public Complejo ObtenerComplejo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void ActualizarComplejo(Complejo complejoActualizado)
        {
            if (File.Exists(_fileName))
            {
                // Leer el archivo JSON existente
                string contenidoJson = File.ReadAllText(_fileName);
                var complejos = JsonSerializer.Deserialize<List<Complejo>>(contenidoJson) ?? new List<Complejo>();

                // Encontrar y actualizar el usuario correspondiente
                var complejoExistente = complejos.FirstOrDefault(c => c.IdComplejo == complejoActualizado.IdComplejo);
                if (complejoExistente != null)
                {
                    complejoExistente.NombreComplejo = complejoActualizado.NombreComplejo;
                    complejoExistente.ImagenComplejo = complejoActualizado.ImagenComplejo;
                }

                // Guardar los cambios de nuevo en el archivo JSON
                string nuevoJson = JsonSerializer.Serialize(complejos, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_fileName, nuevoJson);
            }
        }

        public void CrearComplejo(Complejo complejo)
        {
            List<Complejo> complejos = new List<Complejo>();
            List<Complejo> listaComplejos = ObtenerTodosLosComplejos();

            if (File.Exists(_fileName))
            {
                var contenido = File.ReadAllText(_fileName);
                complejos = JsonSerializer.Deserialize<List<Complejo>>(contenido) ?? new List<Complejo>();
            }

            complejo.IdComplejo = listaComplejos.Count > 0 ? listaComplejos.Max(c => c.IdComplejo) + 1 : 1;

            complejos.Add(complejo);
            File.WriteAllText(_fileName, JsonSerializer.Serialize(complejos, new JsonSerializerOptions { WriteIndented = true }));
            
        }

        public void EliminarComplejo(int idComplejo)
        {
            throw new NotImplementedException();
        }

        public List<Complejo> ObtenerTodosLosComplejos()
        {
            List<Complejo> _complejos;
            if (File.Exists(_fileName))
            {
                string contenidoJson = File.ReadAllText(_fileName);
                _complejos = JsonSerializer.Deserialize<List<Complejo>>(contenidoJson) ?? new List<Complejo>();
                return _complejos;
            } else
            {
                return new List<Complejo>();
            }
            
        }
    }
}
