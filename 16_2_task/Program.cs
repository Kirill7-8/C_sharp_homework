//на основе данных входного файла составить инвентарную ведомость
//склада, включив следующие данные: вид продукции, стоимость, сорт, количество. Вывести в
//новый файл информацию о той продукции, количество которой более заданной величины,
//сгруппировав их по виду продукции.
class Program
{
    public struct Product
    {
        public string Name {get; set;}
        public double Price {get; set;}
        public int Variety {get; set;}
        public int Count {get; set;}
        public Product(string Name, double Price, int Variety, int Count)
        {
            this.Name = Name;
            this.Price = Price;
            this.Variety = Variety;
            this.Count = Count;
        }
        public Product(string[] array)
        {
            if (array.Length < 4)
                throw new ArgumentException("Массив должен содержать 4 элемента");
            Name = array[0];
            Price = double.Parse(array[1]);
            Variety = int.Parse(array[2]);
            Count = int.Parse(array[3]);
        }
    }
    public static List<Product> Input(string file_name)
    {
        using (StreamReader FileIn = new StreamReader(file_name))
        {
            List<Product> productsList = new List<Product>();
            string line = FileIn.ReadLine();
            while((line = FileIn.ReadLine())!= null)
            {
                productsList.Add(new Product(line.Split("; ")));
            }
            return productsList;
        }
    }


    public static void Main()
    {
        string inputFileName = "C:/Users/Kirill_WinLap/C#_projects/16_2_task/Input.txt";
        string outputFileName = "C:/Users/Kirill_WinLap/C#_projects/16_2_task/Output.txt";

        Console.WriteLine("Введите минимальное количество товара: ");
        int count = int.Parse(Console.ReadLine());
        
        List<Product> products = Input(inputFileName);
        var query = products.Where(n => n.Count > count).GroupBy(n => n.Name);

        using(StreamWriter fileOut = new StreamWriter(outputFileName))
            foreach(var items in query)
            {
                fileOut.WriteLine("{0}:", items.Key);
                foreach(var item in items)
                {
                    fileOut.WriteLine("    Цена: {0}; Сорт: {1}; Количество: {2}", item.Price, item.Variety, item.Count);
                }
            }
    }
}