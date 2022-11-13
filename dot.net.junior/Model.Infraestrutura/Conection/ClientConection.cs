using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using Model.Entidades;

namespace Model.Infraestrutura.Conection
{
    public class ClientConection
    {
        public static List<ClientComplet> ListClients()
        {
            var ret = new List<ClientComplet>();
            var client_mode = new ClientComplet();

            using (var connectioDB = new SqlConnection())
            {
                connectioDB.ConnectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
                connectioDB.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connectioDB;
                    command.CommandText = "select c.id as id_client, c.cpf_cnpj, c.name, c.type_client, c.address_id, a.road, a.number, a.district, a.complement, a.cep, a.type_address, a.city, p.id as phone_id, p.ddd, p.number as number_phone, p.type_phone from client as c INNER JOIN address as a ON (c.address_id = a.id) INNER JOIN phones as p ON (c.id = p.client_id);";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var client = new Client
                        {
                            id = (int)reader["id_client"],
                            name = (string)reader["name"],
                            cpf_cnpj = (string)reader["cpf_cnpj"],
                            type_client = (string)reader["type_client"],
                            address_id = (int)reader["address_id"]
                        };
                        var address = new Address
                        {
                            id = (int)reader["address_id"],
                            road = (string)reader["road"],
                            number = (int)reader["number"],
                            district = (string)reader["district"],
                            city = (string)reader["city"],
                            cep = (string)reader["cep"],
                            type_address = (string)reader["type_address"],
                            complement = (string)reader["complement"],
                        };
                        var phone = new Phone
                        {
                            id = (int)reader["phone_id"],
                            ddd = (string)reader["ddd"],
                            number = (string)reader["number_phone"],
                            type_phone = (string)reader["type_phone"],
                            client_id = (int)reader["id_client"]
                        };
                        ret.Add(new ClientComplet {client=client, address=address, phone=phone });
                    }
                }
            }

            return ret;
        }

        public static Client GetClient(int id)
        {
            Client ret = null;

            using (var connectioDB = new SqlConnection())
            {
                connectioDB.ConnectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
                connectioDB.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connectioDB;
                    command.CommandText = string.Format("select * from client where (id = {0})", id);
                    var reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        ret = new Client
                        {
                            id = (int)reader["id"],
                            name = (string)reader["name"],
                            cpf_cnpj = (string)reader["cpf_cnpj"],
                            type_client = (string)reader["type_client"]
                        };
                    }
                }
            }

            return ret;
        }

        public static ClientComplet GetClientComplet(int id)
        {
            ClientComplet ret = new ClientComplet();
            var address_id = 0;
            var client_id = 0;

            using (var connectioDB = new SqlConnection())
            {
                connectioDB.ConnectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
                connectioDB.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connectioDB;
                    command.CommandText = string.Format("select * from client where (id = {0})", id);
                    var reader = command.ExecuteReader();
                    command.Dispose();
                    if (reader.Read())
                    {
                        address_id = (int)reader["address_id"];
                        client_id = (int)reader["id"];
                        ret.client = new Client
                        {
                            id = client_id,
                            name = (string)reader["name"],
                            cpf_cnpj = (string)reader["cpf_cnpj"],
                            type_client = (string)reader["type_client"],
                            address_id = address_id
                        };

                        
                    }
                }
            }
            using (var connectioDB2= new SqlConnection())
            {
                connectioDB2.ConnectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
                connectioDB2.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connectioDB2;
                    command.CommandText = string.Format("select * from address where (id = {0})", address_id);
                        var reader_address = command.ExecuteReader();
                        if (reader_address.Read())
                        {
                            ret.address = new Address
                            {
                                id = (int)reader_address["id"],
                                road = (string)reader_address["road"],
                                number = (int)reader_address["number"],
                                district = (string)reader_address["district"],
                                city = (string)reader_address["city"],
                                cep = (string)reader_address["cep"],
                                type_address = (string)reader_address["type_address"],
                                complement = (string)reader_address["complement"],
                            };
                        }
                }
            }

            using (var connectioDB3 = new SqlConnection())
            {
                connectioDB3.ConnectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
                connectioDB3.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connectioDB3;
                    command.CommandText = string.Format("select * from phones where (client_id = {0})", client_id);
                    var reader_phone = command.ExecuteReader();
                    command.Dispose();
                    if (reader_phone.Read())
                    {
                        ret.phone = new Phone
                        {
                            id = (int)reader_phone["id"],
                            ddd = (string)reader_phone["ddd"],
                            number = (string)reader_phone["number"],
                            type_phone = (string)reader_phone["type_phone"],
                            client_id = (int)reader_phone["client_id"]
                        };
                    }
                }
            }

            return ret;
        }

        public static Client GetClientCpfCnpj(string cpf_cnpj)
        {
            Client ret = null;

            using (var connectioDB = new SqlConnection())
            {
                connectioDB.ConnectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
                connectioDB.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connectioDB;
                    command.CommandText = string.Format("select * from client where (cpf_cnpj = '{0}')", cpf_cnpj);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new Client
                        {
                            id = (int)reader["id"],
                            name = (string)reader["name"],
                            cpf_cnpj = (string)reader["cpf_cnpj"],
                            type_client = (string)reader["type_client"]
                        };
                    }
                }
            }

            return ret;
        }
        public static bool DeleteClient(int id)
        {
            var ret = false;

            using (var connectioDB = new SqlConnection())
            {
                connectioDB.ConnectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
                connectioDB.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connectioDB;
                    command.CommandText = string.Format("delete from phones where (client_id = {0})", id);
                    command.ExecuteNonQuery();
                    command.CommandText = string.Format("delete from client where (id = {0})", id);
                    ret = (command.ExecuteNonQuery() > 0);
                }
            }

            return ret;
        }

        public static Client SaveClient(ClientComplet model)
        {
            Client ret = null;
            var id_response = 0;
            var id_address = 0;
            var isclient = GetClient(model.client.id);
            using (var connectioDB = new SqlConnection())
            {
                connectioDB.ConnectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
                connectioDB.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connectioDB;
                    if (isclient == null)
                    {
                        command.CommandText = string.Format("insert into address (road, number, district, city, cep, type_address, complement) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}'); select convert(int,scope_identity())", model.address.road, model.address.number, model.address.district, model.address.city, model.address.cep, model.address.type_address, model.address.complement);
                        id_address = ((int)command.ExecuteScalar());

                        command.CommandText = string.Format("insert into client (name, cpf_cnpj, type_client, address_id) values ('{0}','{1}','{2}', {3}); select convert(int,scope_identity())", model.client.name, model.client.cpf_cnpj, model.client.type_client, id_address);
                        id_response = ((int)command.ExecuteScalar());

                        command.CommandText = string.Format("insert into phones (ddd, number, type_phone, client_id, identification) values ('{0}','{1}','{2}', {3},'{4}'); select convert(int,scope_identity())", model.phone.ddd, model.phone.number, model.phone.type_phone, id_response, "celular");
                        command.ExecuteScalar();

                    }
                    else
                    {
                        command.CommandText = string.Format("update client  set name='{0}', cpf_cnpj='{1}', type_client='{2}' where id = {3};", model.client.name, model.client.cpf_cnpj, model.client.type_client, model.client.id);
                        if (command.ExecuteNonQuery() > 0)
                        {
                            id_response = model.client.id;
                        }
                        command.CommandText = string.Format("update address  set road='{0}', number={1}, district='{2}', city='{3}', cep='{4}', type_address='{5}', complement='{6}'   where id = {7};", model.address.road, model.address.number, model.address.district, model.address.city, model.address.cep, model.address.type_address, model.address.complement, model.address.id);
                        command.ExecuteNonQuery();
                        command.CommandText = string.Format("update phones  set ddd='{0}', number='{1}', type_phone='{2}', identification='{3}' where id = {4};", model.phone.ddd, model.phone.number, model.phone.type_phone, "celular", model.phone.id);
                        command.ExecuteNonQuery();
                    }
                    if (id_response>0)
                    {
                        command.CommandText = string.Format("select * from client where (id = {0})", id_response);
                        var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            ret = new Client
                            {
                                id = (int)reader["id"],
                                name = (string)reader["name"],
                                cpf_cnpj = (string)reader["cpf_cnpj"],
                                type_client = (string)reader["type_client"]
                            };
                        }
                    }
                    
                }
                    
            }


            return ret;
        }
    }
}
