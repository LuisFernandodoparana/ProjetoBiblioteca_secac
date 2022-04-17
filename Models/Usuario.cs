namespace Biblioteca.Models
{
    public class Usuario
    {
        public static int Admin = 0;
        public static int Padrao = 1;

        // as variaveis estaticas não serão inseridas no banco de dados
        public int Id{ get; set;}
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public int Tipo {get; set;}

    }
}