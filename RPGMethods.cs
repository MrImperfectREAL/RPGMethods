using System;
using System.Collections.Generic;
using System.Linq;

public class Character
{
	//oppgaven ville at jeg skulle gjøre det sånn men tror ikke dette er egentlig den beste måten? X)
    public string UserName { get; set; }
    public string GameClass { get; set; }
    public string Race { get; set; }
    public int HP { get; set; }
    public int MP { get; set; }
    public int Level { get; set; }
    public Artifact[] Artifacts { get; set; }
    public string Weapon { get; set; }

    public Character(string userName, string gameClass, string race, int hp, int mp, int level, Artifact[] artifacts, string weapon)
    {
        UserName = userName;
        GameClass = gameClass;
        Race = race;
        HP = hp;
        MP = mp;
        Level = level;
        Artifacts = artifacts;
        Weapon = weapon;
    }
	
	//metode for å legge til en artifact, syntaxen på å faktisk bruke den er ganske spesifikk
    public void AddArtifact(Artifact artifact)
    {
		// ?? tingen får den til å funke selv om arrayen er null
        List<Artifact> updatedArtifacts = new List<Artifact>(Artifacts ?? new Artifact[0]);
        updatedArtifacts.Add(artifact);
        Artifacts = updatedArtifacts.ToArray();
    }
}

//definerer artifacts og deres detaljer
public class Artifact
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }

    public Artifact(string name, string type, string description)
    {
        Name = name;
        Type = type;
        Description = description;
    }
}

//subclass for artifacts spesifikt for healing artifacts, inhereter properties til artifact
public class HealingArtifact : Artifact
{
    public int HealingAmount { get; set; }

    public HealingArtifact(string name, string type, string description, int healingAmount)
        : base(name, type, description)
    {
        HealingAmount = healingAmount;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
		//printer ut karakteren Doodledoo23
        Character Doodledoo23 = new Character("Doodledoo23", "Bard", "Halfling", 100, 50, 5, null, "The Holy Lyre");
		Console.WriteLine("Character details:");
        Console.WriteLine("Username: " + Doodledoo23.UserName);
        Console.WriteLine("Class: " + Doodledoo23.GameClass);
        Console.WriteLine("Race: " + Doodledoo23.Race);
        Console.WriteLine("HP: " + Doodledoo23.HP);
        Console.WriteLine("MP: " + Doodledoo23.MP);
        Console.WriteLine("Level: " + Doodledoo23.Level);
        Console.WriteLine("Weapon: " + Doodledoo23.Weapon);
        Console.WriteLine();
		
		//hvis du hadde 3 artifacter så tapte du en og fikk 2 til kunne det vært brukelig å se hva du hadde før, for eksempel
        Console.WriteLine("Artifacts before adding: " + (Doodledoo23.Artifacts == null ? "None" : string.Join(", ", Doodledoo23.Artifacts.Select(a => a.Name))));

        Artifact artifact1 = new Artifact("Akashic Records", "Musical", "Ancient records said to transcribe the fate of the universe through it's notes.");
        Doodledoo23.AddArtifact(artifact1);

        HealingArtifact artifact2 = new HealingArtifact("Shinigami Flower", "Healing", "A rare red flower associated with death. \"Only through death can life spring forth.\"", 20);
        Doodledoo23.AddArtifact(artifact2);

        Console.WriteLine("Artifacts after adding: " + (Doodledoo23.Artifacts == null ? "None" : string.Join(", ", Doodledoo23.Artifacts.Select(a => a.Name))));

		// != null gjør sikker koden din ikke crasher vis karakteren har ingenting
        if (Doodledoo23.Artifacts != null)
        {
            Console.WriteLine("\nArtifacts details:");
			//foreach gjør sikker alle artifacts blir printed ut uansett hvor manga det er!
            foreach (Artifact artifact in Doodledoo23.Artifacts)
            {
                Console.WriteLine("Name: " + artifact.Name);
                Console.WriteLine("Type: " + artifact.Type);
                Console.WriteLine("Description: " + artifact.Description);
				
				//if er trengt fordi det er en subclass of artifacts og denne metoden skal funke for både healing artifacts og ikke healing artifacts
				if (artifact.GetType() == typeof(HealingArtifact))
				{
  					  HealingArtifact healingArtifact = (HealingArtifact)artifact;
  					  Console.WriteLine("Healing Power: " + healingArtifact.HealingAmount);
				}

				
                Console.WriteLine();
            }
        }
    }
}
