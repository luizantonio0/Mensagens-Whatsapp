using System.ComponentModel;
using WppMensage.Entity;

namespace WppMensage.Entity
{
    public enum Empresa
    {
        Minuta,
        Up_Ideias,
        Aristocrata,
        B2
    }
}

public static class EmpresaExtensions
{
    public static string ToCustomString(this Empresa empresa)
    {
        return empresa.ToString().Replace("_", " ");
    }
}