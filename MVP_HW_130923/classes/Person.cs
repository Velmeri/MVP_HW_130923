using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_HW_130923.classes
{
	public class Person
	{
		public string Name { get; set; }
		public int Age { get; set; }

		public Person() {	}
		public Person(string name, int age) { Name = name; Age = age; }
	}
}
