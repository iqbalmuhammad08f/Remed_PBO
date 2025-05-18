using System.Threading.Channels;

public abstract class Bank
{
    public string no_rekening { get; set; }
    public string nama_pemilik { get; set; }
    public int saldo { get; set; }
    public string password { get; set; }

}
public interface IMethodBank
{
    public void penarikanSaldo(string no_rekenign, int tarik_saldo);
    public void setorTunai(string no_rekenign, int setor_tunai);
    public void transferSaldo(string no_rekening, string rekening_tujuan, int nominal_transfer);
}
public class BankPelita : Bank
{

    public BankPelita(string no_rekening, string nama_pemilik, int saldo, string password)
    {
        this.no_rekening = no_rekening;
        this.nama_pemilik = nama_pemilik;
        this.saldo = saldo;
        this.password = password;
    }
    
}
public class ServiceBankPelita : IMethodBank
{
    public List<BankPelita> banks = new List<BankPelita>
    {
        new BankPelita("2120","denis",100000,"123"),

    };
    public void Register(string no_rekening, string nama_pemilik, int saldo, string password)
    {

        banks.Add(new BankPelita(no_rekening, nama_pemilik, saldo, password));
        Console.Write("Registrasi berhasil [Enter untuk melanjutkan]");
        Console.ReadLine();
    }
    
    public void penarikanSaldo(string no_rekenign, int tarik_saldo)
    {
        BankPelita penarikan_saldo = banks.Find(a => a.no_rekening == no_rekenign);
        penarikan_saldo.saldo -= tarik_saldo;
        Console.Write("Saldo berhasil ditarik [Enter untuk kembali]");
        Console.ReadLine();
    }
    public void setorTunai(string no_rekenign, int setor_tunai)
    {
        BankPelita setor = banks.Find(a => a.no_rekening == no_rekenign);
        setor.saldo += setor_tunai;
        Console.Write("Nominal berhasil disetorkan [Enter untuk kembali]");
        Console.ReadLine();
    }
    public void transferSaldo(string no_rekening, string rekening_tujuan, int nominal_transfer)
    {
        BankPelita saldo_pengirim = banks.Find(a => a.no_rekening == no_rekening);
        saldo_pengirim.saldo -= nominal_transfer;
        BankPelita saldo_penerima = banks.Find(a => a.no_rekening == rekening_tujuan);
        saldo_penerima.saldo += nominal_transfer;
        Console.Write("Transfer Berhasil [Enter untuk kembali]");
        Console.ReadLine();
    }

}
class Program
{
    static ServiceBankPelita service = new ServiceBankPelita();
    static void Main()
    {
        while(true)
        {
            Console.Clear();
            Console.WriteLine("\n=== BANK PELITA ===");
            Console.WriteLine("\n1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("0. Exit");
            Console.Write("Masukkan Pilihan: ");
            string input_user = Console.ReadLine();
            if (input_user == "1")
            {
                bool run = true;
                Console.Clear();
                Console.WriteLine("\n=== LOGIN ===");
                Console.Write("Masukkan No Rekening: ");
                string no_rekening = Console.ReadLine();
                Console.Write("Masukkan Password: ");
                string password = Console.ReadLine();
                foreach(var data in service.banks)
                {
                    if(no_rekening == data.no_rekening && password == data.password)
                    {
                        menu(no_rekening);
                        run = false;
                        break;
                    }
                }
                if(run)
                {
                    Console.Write("Akun tidak ada atau password salah [Enter untuk kembali]");
                    Console.ReadLine();
                    break;
                }
            }
            else if (input_user == "2")
            {
                Console.Clear();
                Console.WriteLine("=== REGISTER ===");
                Console.Write("Masukkan No Rekening: ");
                string no_rekening = Console.ReadLine();
                Console.Write("Masukkan Nama Pemilik: ");
                string nama_pemilik = Console.ReadLine();
                Console.Write("Masukkan Saldo Awal: ");
                int saldo = int.Parse(Console.ReadLine());
                Console.Write("Masukkan Password: ");
                string password = Console.ReadLine();
                service.Register(no_rekening, nama_pemilik, saldo, password);
                continue;
                
            }
            else if(input_user == "0")
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.Write("Input Salah [Enter Untuk Mengulangi]");
                Console.ReadLine();
            }
            static void menu(string no_rekening)
            {
                while (true)
                {
                    Console.Clear();
                    BankPelita nasabah = service.banks.Find(a => a.no_rekening == no_rekening);
                    Console.WriteLine("\n=== MENU ===");
                    Console.WriteLine($"\nSaldo: {nasabah.saldo}");
                    Console.WriteLine("1. Tarik Saldo");
                    Console.WriteLine("2. Setor Tunai");
                    Console.WriteLine("3. Transfer");
                    Console.WriteLine("4. Info");
                    Console.WriteLine("0. Exit");
                    Console.Write("Masukkan Pilihan: ");
                    string user_input = Console.ReadLine();
                    if (user_input == "1")
                    {
                        Console.Clear();
                        Console.WriteLine("=== TARIK SALDO ===");
                        if (nasabah.saldo >= 50000)
                        {
                            Console.WriteLine($"\nSaldo: {nasabah.saldo}");
                            Console.Write("\nMasukkan nominal kelipatan 50.000: ");
                            int tarik_saldo = int.Parse(Console.ReadLine());
                            if (tarik_saldo % 50000 == 0)
                            {
                                service.penarikanSaldo(no_rekening, tarik_saldo);
                                continue;
                            }
                            else
                            {
                                Console.Write("Nominal tidak sesuai [Enter Untuk Mengulangi]");
                                Console.ReadLine();
                                continue;
                            }
                        }
                        else
                        {
                            Console.Write("Saldo tidak cukup, tidak dapat melaukan penarikan [Enter untuk kembali]");
                            Console.ReadLine();
                        }


                    }
                    else if(user_input == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("=== Setor Tunai ===");
                        Console.Write("\nMasukkan nominal kelipatan 50.000: ");
                        int setor_tunai = int.Parse(Console.ReadLine());
                        if (setor_tunai % 50000 == 0)
                        {
                            service.setorTunai(no_rekening, setor_tunai);
                            continue;
                        }
                        else
                        {
                            Console.Write("Nominal tidak sesuai [Enter Untuk Mengulangi]");
                            Console.ReadLine();
                            continue;
                        }

                    }
                    else if(user_input == "3")
                    {
                        Console.Clear();
                        Console.WriteLine("=== TRANSFER SALDO ===");
                        if (nasabah.saldo >= 50000)
                        {
                            Console.Write("\nNo Rekening tujuan: ");
                            string rekening_tujuan = Console.ReadLine();
                            if (service.banks.Find(a => a.no_rekening == rekening_tujuan) != null)
                            {
                                Console.Write("Masukkan Nominal: ");
                                int nominal_transfer = int.Parse(Console.ReadLine());
                                service.transferSaldo(no_rekening, rekening_tujuan, nominal_transfer);
                                continue;
                            }
                            else
                            {
                                Console.Write("Rekening tujuan tidak ditemukan [Enter untuk mengulangi]");
                                Console.ReadLine();
                                continue;
                            }
                        }
                        else
                        {
                            Console.Write("Nominal tidak sesuai [Enter Untuk Mengulangi]");
                            Console.ReadLine();
                            continue;
                        }
                    }
                    else if(user_input == "4")
                    {
                        Console.Clear();
                        Console.WriteLine("=== INFO ===");
                        Console.WriteLine($"\n1. Nama          : {nasabah.nama_pemilik}");
                        Console.WriteLine($"2. Nomer Rekening: {nasabah.no_rekening}");
                        Console.WriteLine($"3. Password      : {nasabah.password}");
                        Console.WriteLine($"4. Saldo         : {nasabah.saldo}");
                        Console.Write("Enter Untuk kembali ");
                        Console.ReadLine();
                    }
                    else if(user_input == "0")
                    {
                        return;
                    }
                    else
                    {
                        Console.Write("Input Salah [Enter Untuk Mengulangi]");
                        Console.ReadLine();
                        continue;
                    }

                }
            }
        }
    }
}