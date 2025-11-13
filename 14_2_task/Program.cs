using System.Globalization;
using System.Runtime.CompilerServices;

class Program
{
    struct Student : IComparable<Student>
    {
        public string Name;
        public int GroupNumber;
        public int[] Grades;

        public int CompareTo(Student other)
        {
            if (GroupNumber == other.GroupNumber)
            {
                return 0;
            }
            else
            {
                if (GroupNumber < other.GroupNumber)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        public Student(string Name, int GroupNumber, int[] Grades)
        {
            this.Name = Name;
            this.GroupNumber = GroupNumber;
            this.Grades = Grades;
        }
        public string Show()
        {
            return string.Format("{0}; {1}; {2}", Name, GroupNumber, string.Join(", ", Grades));
        }
        public bool isPassedSession()
        {
            foreach (int Grade in Grades)
            {
                if (Grade < 3) return false;
            }
            return true;
        }
    }
    static Student[] Input(string FileName)
    {
        using (StreamReader file = new StreamReader(FileName))
        {
            int n = int.Parse(file.ReadLine());
            Student[] Students_array = new Student[n];

            for (int i = 0; i < n; i++)
            {
                string[] array = file.ReadLine().Split(';');
                int[] intArray = new int[3];
                string[] strArray = array[2].Split(',', StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < 3; j++)
                {
                    intArray[j] = int.Parse(strArray[j]);
                }
                Students_array[i] = new Student(array[0], int.Parse(array[1]), intArray);
            }
            return Students_array;
        }
    }

    static void Main()
    {
        string InFileName = "C:/Users/Kirill_WinLap/C#_projects/14_2_task/input.txt";
        string OutFileName = "C:/Users/Kirill_WinLap/C#_projects/14_2_task/output.txt";

        Student[] Students_array = Input(InFileName);
        Array.Sort(Students_array);
        using (StreamWriter file = new StreamWriter(OutFileName))
        {
            foreach(Student student in Students_array)
            {
                if (student.isPassedSession())
                {
                    file.WriteLine(student.Show());
                }
            }
        }

    }
}