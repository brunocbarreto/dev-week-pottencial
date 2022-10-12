using System.Collections.Generic;

namespace src.Models;

public class Person
{
  public Person()
  {
    this.Name = "template";
    this.Age = 0;
    this.Activated = true;
    this.contracts = new List<Contract>();
  }

  public Person(string Name, int Age, string Cpf)
  {
    this.Name = Name;
    this.Age = Age;
    this.Cpf = Cpf;
    this.Activated = true;
    this.contracts = new List<Contract>();
  }

  //nome, idade, cpf, ativa
  public int Id { get; set; }
  public string Name { get; set; }
  public int Age { get; set; }
  public string? Cpf { get; set; }
  public bool Activated { get; set; }
  public List<Contract> contracts { get; set; }
}
