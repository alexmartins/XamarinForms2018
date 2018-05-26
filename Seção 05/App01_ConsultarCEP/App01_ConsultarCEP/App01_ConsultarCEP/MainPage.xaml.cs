using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico;
using App01_ConsultarCEP.Servico.Modelo;

namespace App01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
        int restrictCount = 8; //Enter your number of character restriction
        public MainPage()
		{
			InitializeComponent();
            CEP.TextChanged += OnTextChanged;
            //CEP.Text = "00000000";
            BOTAO.Clicked += BuscarCEP;
		}

        private void OnTextChanged(object sender, EventArgs e)
        {
            Entry entry = sender as Entry;
            String val = entry.Text; //Get Current Text

            if (val.Length > restrictCount) //If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                entry.Text = val; //Set the Old value
            }
        }

        private void BuscarCEP(Object sender, EventArgs args)
        { 
            string cep = CEP.Text.Trim();
            if(isValidCEP(cep)){
                try{
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if(end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço:\r\n\r\n{0}, {1} \r\n{2} - {3}", end.logradouro, end.bairro, end.localidade, end.uf);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi localizado para o CEP: " + cep, "OK");
                    }
                    CEP.Focus();
                }
                catch (Exception e){
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }

        private bool isValidCEP(String cep){
            bool valido = true;
            if(cep.Length != 8){
                DisplayAlert("ERRO", "CEP inválido! O CEP deve contar 8 caracteres.", "OK");
                valido = false;
            }
            int novoCEP = 0;
            if(!int.TryParse(cep, out novoCEP)){
                DisplayAlert("ERRO", "CEP inválido! O CEP deve contar 8 caracteres.", "OK");
                valido = false;
            }        
            return valido;
        }
	}
}