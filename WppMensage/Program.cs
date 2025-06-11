using WppMensage.Entity;
using OfficeOpenXml;

class Program
{
    static async Task Main(string[] args)
    {
        
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization");
        bool itWorked = false;

        using (var client = new HttpClient())
        {
            byte[] excelData = await client.GetByteArrayAsync("https://docs.google.com/spreadsheets/d/1m199_EDPHCn_rET8IQVVf-7sZbockIjZ6ahIjJtdzgU/export?format=xlsx");

            using (var stream = new MemoryStream(excelData))
            using (var pack = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets[0];

                int colCount = ws.Dimension.End.Column;
                int rowCount = ws.Dimension.End.Row;

                for (int row = 1; row <= rowCount; row++)
                {
                    try
                    {
                        var pessoa = new Person(ws.Cells[row, 10].Value.ToString());

                        if (pessoa.status.Equals("Mandar Mensagem"))
                        {
                            pessoa.name = ws.Cells[row, 6].Value.ToString() ?? "";
                            pessoa.client = ws.Cells[row, 7].Value.ToString() ?? "";

                            Console.WriteLine("\n\n======================================================================================\n\n");

                            Console.WriteLine(GetMensage(GetNome(pessoa.name).Trim(), GetEmpresa(pessoa.client.Replace("-", "_").Replace("/", "_"), 0)));
                            itWorked = true;
                        }
                    }
                    catch (Exception) { continue; }
                }
            }
        }

        if (!itWorked)
        {
            Console.Write("Não Encontrou nenhuma Situação (Mandar Mensagem)\n");
        }
        
        
        Console.ReadLine();
    }
    public static string GetEmpresa(string nomeEmpresa, int errosOcorridos)
    {
        try
        {
            var empresaContratante = (EmpresaContratante)Enum.Parse(typeof(EmpresaContratante), nomeEmpresa);
            return EmpresaExtensions.ToCustomString(empresaContratante.GetEmpresa());
        }
        catch (Exception)
        {
            errosOcorridos++;
        }
        return "(Empresa Não encontrada)";
    }

    public static string GetMensage(string nome, string empresa)
    {
        return $"""
                Olá, *{nome}*! 👋

                Somos do Departamento de T.I da {empresa} e informamos que seu acesso ao registro de ponto via tablet foi liberado ✅.

                Para registrar o ponto corretamente, siga estas etapas:

                1️⃣  Posicione seu rosto corretamente em frente ao tablet.
                2️⃣  O sistema solicitará uma senha 🔒 → digite os quatro primeiros dígitos do seu CPF.
                3️⃣  Clique em "OK" para confirmar.

                📌 Lembre-se de registrar o ponto nos seguintes momentos:
                ✔️  Entrada no expediente
                ✔️  Saída para intervalo
                ✔️  Retorno do intervalo
                ✔️  Saída ao final do expediente
                """;
    }

    public static string GetNome(String nomeOriginal)
    {
        string nome = "";

        try
        {
            if ((nomeOriginal[19] == ' ') && (nomeOriginal[21] == ' '))
            {
                nome = nomeOriginal.Substring(21);
            }
            else
            {
                nome = nomeOriginal.Substring(19);
            }

            if (nome[3] == ' ')
            {
                nome = nome.Substring(3);
            }
            if (nome[2] == ' ')
            {
                nome = nome.Substring(2);
            }
        }
        catch (Exception)
        {
            throw new FieldAccessException();
        }
        return nome;
    }
    
    public static string[] GetNomeAndEmpresaCSV(String linhaOriginal)
    {
        string[] nomes = new string[2];
        var splitLine = linhaOriginal.Split(",");
        var name = splitLine[5];
        nomes[1] = splitLine[6];

        // Tem "," na planilha e isso confunde o csv: var status = splitLine[10];

        try
        {
            if ((name[19] == ' ') && (name[21] == ' '))
            {
                nomes[0] = name.Substring(21);
            }
            else
            {
                nomes[0] = name.Substring(19);
            }

            if (nomes[0][3] == ' ')
            {
                nomes[0] = nomes[0].Substring(3);
            }
        }
        catch (Exception)
        {
            throw new FieldAccessException();
        }
        return nomes;
    }
}