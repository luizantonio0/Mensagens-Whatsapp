using Microsoft.Extensions.Primitives;
using WppMensage.Entity;

namespace WppMensage.Entity
{
    public enum EmpresaContratante
    {
        CCTG,
        BB,
        ICMBio,
        CCBB,
        INEMA,
        TJ_GO,
        STF,
        PMRJ,
        SEMAD_GO,
        ANEEL,
        SEPLAN_AC,
        IPEM_PR,
        Minuta,
        TECPAR,
        INSS_DF,
        CCBB_BH,
        IFPR
    }

    public static class EmpresaContratanteExtensions
    {
        public static Empresa GetEmpresa(this EmpresaContratante contratante)
        {
            switch (contratante)
            {
                case EmpresaContratante.CCTG:
                case EmpresaContratante.ICMBio:
                case EmpresaContratante.CCBB:
                case EmpresaContratante.CCBB_BH:
                case EmpresaContratante.STF:
                case EmpresaContratante.PMRJ:
                case EmpresaContratante.SEMAD_GO:
                case EmpresaContratante.ANEEL:
                case EmpresaContratante.IPEM_PR:
                case EmpresaContratante.INSS_DF:
                case EmpresaContratante.Minuta:
                case EmpresaContratante.SEPLAN_AC:
                case EmpresaContratante.BB:
                    return Empresa.Minuta;

                case EmpresaContratante.INEMA:
                case EmpresaContratante.TJ_GO:
                    return Empresa.Up_Ideias;

                case EmpresaContratante.IFPR:
                    return Empresa.Aristocrata;
                case EmpresaContratante.TECPAR:
                    return Empresa.B2;
                default:
                    throw new ArgumentException("Invalid empresa value", nameof(contratante));
            }
        }
    }
}