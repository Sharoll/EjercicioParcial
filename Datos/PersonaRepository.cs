using Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Datos
{
    public class PersonaRepository
    {
        private readonly SqlConnection _connection;
        private readonly List<Persona> _personas = new List<Persona>();
        public PersonaRepository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }
        public void Guardar(Persona persona)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into Persona (Identificacion,Nombre,Apellido,Sexo,Edad,Departamento,Ciudad,ValorApoyo,Modalidad,Fecha) values 
                (@Identificacion,@Nombre,@Edad,@Sexo,@Pulsacion)";
                command.Parameters.AddWithValue("@Identificacion", persona.Identificacion);
                command.Parameters.AddWithValue("@Nombre", persona.Nombre);
                command.Parameters.AddWithValue("@Apellido", persona.Apellido);
                command.Parameters.AddWithValue("@Sexo", persona.Sexo);
                command.Parameters.AddWithValue("@Edad", persona.Edad);
                command.Parameters.AddWithValue("@Departamento", persona.Departamento);
                command.Parameters.AddWithValue("@Ciudad", persona.Ciudad);
                command.Parameters.AddWithValue("@ValorApoyo", persona.valorApoyo);
                command.Parameters.AddWithValue("@Modalidad", persona.Modalidad);
                command.Parameters.AddWithValue("@Fecha", persona.Fecha);
                var filas = command.ExecuteNonQuery();
            }
        }
        
       public List<Persona> ConsultarTodos()
        {
            SqlDataReader dataReader;
            List<Persona> personas = new List<Persona>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Select * from persona ";
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                       Persona persona = DataReaderMapToPerson(dataReader);
                        personas.Add(persona);
                    }
                }
            }
            return personas;
        }
       
    }
}