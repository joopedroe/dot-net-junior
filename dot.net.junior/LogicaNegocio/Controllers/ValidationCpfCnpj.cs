using System;
using System.Collections.Generic;
using Model.Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Model.Infraestrutura.Conection;

namespace LogicaNegocio.Controllers
{
	public static class ValidationCpfCnpj
    {
		private static bool IsCnpj(string cnpj)
		{
			int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int soma;
			int resto;
			string digito;
			string tempCnpj;
			cnpj = cnpj.Trim();
			cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
			if (cnpj.Length != 14)
				return false;
			tempCnpj = cnpj.Substring(0, 12);
			soma = 0;
			for (int i = 0; i < 12; i++)
				soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
			resto = (soma % 11);
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = resto.ToString();
			tempCnpj = tempCnpj + digito;
			soma = 0;
			for (int i = 0; i < 13; i++)
				soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
			resto = (soma % 11);
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = digito + resto.ToString();
			return cnpj.EndsWith(digito);
		}

			private static bool IsCpf(string cpf)
			{
				int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
				int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
				string tempCpf;
				string digito;
				int soma;
				int resto;
				cpf = cpf.Trim();
				cpf = cpf.Replace(".", "").Replace("-", "");
				if (cpf.Length != 11)
					return false;
				tempCpf = cpf.Substring(0, 9);
				soma = 0;

				for (int i = 0; i < 9; i++)
					soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
				resto = soma % 11;
				if (resto < 2)
					resto = 0;
				else
					resto = 11 - resto;
				digito = resto.ToString();
				tempCpf = tempCpf + digito;
				soma = 0;
				for (int i = 0; i < 10; i++)
					soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
				resto = soma % 11;
				if (resto < 2)
					resto = 0;
				else
					resto = 11 - resto;
				digito = digito + resto.ToString();
				return cpf.EndsWith(digito);
			}

		private static ModelResponse IsTypeClient(string cpf_cnpj, string type_client, string type_address)
        {
			var status = true;
			string status_response = "Sucesso", message = "Valido";
			if (cpf_cnpj.Length == 11)
            {
				if(type_client != "Pessoa Física")
                {
					status_response = "Erro";
					status = false;
					message = "Pessoa Júridica não aceita CPF";
				}
				else if (!IsCpf(cpf_cnpj))
				{
					status_response = "Erro";
					status = false;
					message = "CPF inválido";
				}

			}
			else if (cpf_cnpj.Length == 14)
			{
				if(type_client != "Pessoa Júridica")
                {
					status_response = "Erro";
					status = false;
					message = "Pessoa Física não aceita CNPJ";
				}

				else if (!IsCnpj(cpf_cnpj))
				{
					status_response = "Erro";
					status = false;
					message = "CNPJ inválido";
				}
				else if (type_address != "Comercial")
                {
					status_response = "Erro";
					status = false;
					message = "Para cliente Pessoa Júridica deve ser selecionado endereço comercial!";
				}
			}
            else
            {
				status_response = "Erro";
				status = false;
				message = "CPF inválido";
			}
			return (new ModelResponse { status = status, status_response = status_response, message = message });
		}

		public static ModelResponse Validation(string cpf_cnpj, int id, string type_client, string type_address)
        {
			string status_response = "Sucesso", message = "Valido";
			var model = ClientConection.GetClient(id);
			bool status = true;
			var validation_init = IsTypeClient(cpf_cnpj, type_client, type_address);
			if (!validation_init.status)
            {
				status = validation_init.status;
				status_response = validation_init.status_response;
				message = validation_init.message;
			}
			
            if (status && model == null)
            {
				var isExist = ClientConection.GetClientCpfCnpj(cpf_cnpj);
				if (isExist != null)
				{
					status = false;
					status_response = "Erro";
					message = "Erro, CPF ou CNPJ ja cadastrado!";
				}
			}

			return (new ModelResponse { status= status, status_response = status_response, message= message });
		}
	}
}
