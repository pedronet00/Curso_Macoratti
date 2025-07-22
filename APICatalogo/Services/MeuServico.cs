namespace APICatalogo.Services
{
    public class MeuServico : IMeuServico
    {
        public string Saudacao(string nome)
        {
            return $"Olá {nome}, seja bem vindo! \n\n Horário atual:{DateTime.UtcNow}";
        }
    }
}
