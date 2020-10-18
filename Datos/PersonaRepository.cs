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
                command.CommandText = @"Insert Into Persona(Identificacion,Nombre,Apellido,Sexo,Edad,Departamento,Ciudad,valorApoyo,Modalidad,Fecha)values 
                 (@Identificacion,@Nombre,@Apellido,@Sexo,@Edad,@Departamento,@Ciudad,@valorApoyo,@Modalidad,@Fecha)";
                command.Parameters.AddWithValue("@Identificacion", persona.Identificacion);
                command.Parameters.AddWithValue("@Nombre", persona.Nombre);
                command.Parameters.AddWithValue("@Apellido", persona.Apellido);
                command.Parameters.AddWithValue("@Sexo", persona.Sexo);
                command.Parameters.AddWithValue("@Edad", persona.Edad);
                command.Parameters.AddWithValue("@Departamento", persona.Departamento);
                command.Parameters.AddWithValue("@Ciudad", persona.Ciudad);
                command.Parameters.AddWithValue("@valorApoyo", persona.valorApoyo);
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
                command.CommandText = "Select * from Persona ";
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

        private Persona DataReaderMapToPerson(SqlDataReader dataReader)
        {
            if(!dataReader.HasRows) return null;
            Persona persona = new Persona();
            persona.Identificacion = (string)dataReader["Identificacion"];
            persona.Nombre = (string)dataReader["Nombre"];
            persona.Apellido = (string)dataReader["Apellido"];
            persona.Sexo = (string)dataReader["Sexo"];
            persona.Edad = (int)dataReader["Edad"];
            persona.Departamento = (string)dataReader["Departamento"];
            persona.Ciudad = (string)dataReader["Ciudad"];
            persona.valorApoyo = (decimal)dataReader["valorApoyo"];
            persona.Modalidad = (string)dataReader["Modalidad"];
            persona.Fecha = (DateTime)dataReader["Fecha"];
            return persona;
        }
       
    }
}