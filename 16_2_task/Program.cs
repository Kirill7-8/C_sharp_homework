//На основе данных входного файла составить инвентарную ведомость
//игрушек, включив следующие данные: название игрушки, ее стоимость (в руб.), возрастные
//границы детей, для которых предназначена игрушка. Вывести в новый файл информацию о
//тех игрушках, которые предназначены для детей старше N лет, отсортировав их по стоимости.
class Program
{
    public struct Product
    {
        public string Name {get; set;}
        public double Price {get; set;}
        public int MinAge {get; set;}
        
        public Product(string[] array)
        {
            if (array.Length < 3)
                throw new ArgumentException("Массив должен содержать 4 элемента");
            Name = array[0];
            Price = double.Parse(array[1]);
            MinAge = int.Parse(array[2]);
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

        Console.WriteLine("Игрушки для детей старше: ");
        int count = int.Parse(Console.ReadLine());
        
        List<Product> products = Input(inputFileName);
        var query = products.Where(n => n.MinAge > count).OrderByDescending(n => n.Price);

        using(StreamWriter fileOut = new StreamWriter(outputFileName)){
            foreach(var item in query)
            {
               fileOut.WriteLine("Название: {0} Цена: {1}; Детям от: {2}", item.Name, item.Price, item.MinAge);
            }
        }
    }
}
