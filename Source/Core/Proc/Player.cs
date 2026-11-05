namespace PosmosVV.Proc
{
    public class Governor
    {
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int Level { get; set; }
        public float GrowthRate { get; set; }
        public int Experience { get; set; }
        public int ExperienceThreshold { get; set; }
    }

    public class Government
    {
        public string Name { get; set; } = string.Empty;
        public List<string> GovernmentSlots { get; set; } = new();
        public List<string> GovernmentSlotNames { get; set; } = new();
        public List<string> PoliticalSlots { get; set; } = new();
        public List<Governor> Officials { get; set; } = new();
        public string Type { get; set; } = string.Empty;
        public string Ethic { get; set; } = string.Empty;
        public List<string> Values { get; set; } = new();
    }

    public class Species
    {
        public string Name { get; set; }
        public string Composition { get; set; }
        public bool Heterotrophic { get; set; }
        public string Class { get; set; }
        public List<string> Traits { get; set; }
        public string PreferredPlanet { get; set; }

        public Species(
            string name,
            string composition,
            bool heterotrophic,
            string speciesClass,
            IEnumerable<string> traits,
            string preferredPlanet)
        {
            Name = name;
            Composition = composition;
            Heterotrophic = heterotrophic;
            Class = speciesClass;
            Traits = new List<string>(traits);
            PreferredPlanet = preferredPlanet;
        }
    }

    public class GameMap
    {
        public List<MapNode> Nodes { get; } = new();
    }

    public class MapNode
    {
        public List<StarSystem> Systems { get; } = new();
    }

    public class StarSystem
    {
        public List<Domain> Domains { get; } = new();
        public string? Owner { get; set; }

        public void AddDomain(Domain domain)
        {
            ArgumentNullException.ThrowIfNull(domain);
            domain.System = this;
            Domains.Add(domain);
        }
    }

    public class Domain
    {
        public string Type { get; set; } = string.Empty;
        public bool Owned { get; set; }
        public StarSystem? System { get; set; }
    }

    public class Player
    {
        private static readonly string[] GovernmentTypes =
        {
            "Monarchy", "Republic", "Empire", "Consulate"
        };

        private static readonly string[] Ethics =
        {
            "Democratic", "Autocratic", "Oligarchic", "Theocratic",
            "Dictatorial", "Mercantile", "Enlightened"
        };

        private static readonly string[] SpeciesClasses =
        {
            "Mammal-class", "Reptile-class", "Camarid-class", "Arthropod-class",
            "Crustacean-class", "Plant-class", "Myco-class"
        };

        public List<Domain> OwnedDomains { get; } = new();
        public string Name { get; set; }
        public Government Government { get; set; } = new();
        public Species NativeSpecies { get; set; }

        public Player(string name)
        {
            Name = name;
            NativeSpecies = new Species(
                "Dekron",
                "Carbon-based",
                true,
                "Reptile-class",
                new[] { "cold-blooded", "egg-laying" },
                "alpine planet");
        }

        public void SetUpGovernment(TextReader? input = null, TextWriter? output = null)
        {
            input ??= Console.In;
            output ??= Console.Out;

            WriteOptions(output, GovernmentTypes);
            int governmentOption = ReadChoice(input, output, "Government: ", GovernmentTypes.Length);
            output.Write("Name: ");
            string governmentName = ReadRequiredLine(input, "A government name is required.");

            Government government = new()
            {
                Name = governmentName,
                GovernmentSlots = new List<string> { "main" }
            };

            switch (governmentOption)
            {
                case 1:
                    government.Type = "Monarchy";
                    government.PoliticalSlots = new List<string>
                    {
                        "military", "military", "economic", "wildcard"
                    };
                    government.GovernmentSlotNames.Add("King");
                    break;
                case 2:
                    government.Type = "Republic";
                    government.PoliticalSlots = new List<string>
                    {
                        "economic", "economic", "civilian", "wildcard"
                    };
                    government.GovernmentSlots.AddRange(
                        new[] { "economic", "military", "civilian" });
                    government.GovernmentSlotNames.AddRange(new[]
                    {
                        "First minister", "minister of economy", "minister of war",
                        "minister of infrastructure"
                    });
                    break;
                case 3:
                    government.Type = "Empire";
                    government.PoliticalSlots = new List<string>
                    {
                        "military", "military", "economic", "wildcard"
                    };
                    government.GovernmentSlots.Add("military");
                    government.GovernmentSlotNames.AddRange(
                        new[] { "emperor", "prime senator" });
                    break;
                case 4:
                    government.Type = "Consulate";
                    government.GovernmentSlots.Add("civilian");
                    government.PoliticalSlots = new List<string>
                    {
                        "military", "economic", "civilian", "wildcard"
                    };
                    government.GovernmentSlotNames.AddRange(
                        new[] { "consule", "praetor" });
                    break;
            }

            WriteOptions(output, Ethics);
            int ethicOption = ReadChoice(input, output, "Ethic: ", Ethics.Length);
            government.Ethic = Ethics[ethicOption - 1];
            Government = government;
        }

        public void SetUpSpecies(
            IReadOnlyList<string> generalTraits,
            IReadOnlyList<string> classTraits,
            TextReader? input = null,
            TextWriter? output = null)
        {
            ArgumentNullException.ThrowIfNull(generalTraits);
            ArgumentNullException.ThrowIfNull(classTraits);
            if (generalTraits.Count == 0 || classTraits.Count == 0)
            {
                throw new ArgumentException("Both trait lists must contain at least one trait.");
            }

            input ??= Console.In;
            output ??= Console.Out;

            output.Write("Species name: ");
            string speciesName = ReadRequiredLine(input, "A species name is required.");
            WriteOptions(output, new[] { "Carbon-based", "Silicon-based" });
            int compositionOption = ReadChoice(input, output, "Composition: ", 2);
            string composition = compositionOption == 1 ? "Carbon-based" : "Silicon-based";

            WriteOptions(output, SpeciesClasses);
            int classOption = ReadChoice(input, output, "Species class: ", SpeciesClasses.Length);
            string speciesClass = SpeciesClasses[classOption - 1];

            output.WriteLine("General traits:");
            WriteOptions(output, generalTraits);
            List<string> selectedTraits = new();
            for (int i = 0; i < 2; i++)
            {
                int traitOption = ReadChoice(
                    input, output, "Trait: ", generalTraits.Count);
                selectedTraits.Add(generalTraits[traitOption - 1]);
            }

            output.WriteLine("Class traits:");
            WriteOptions(output, classTraits, generalTraits.Count);
            int classTraitOption = ReadChoice(
                input, output, "Trait: ", classTraits.Count, generalTraits.Count);
            selectedTraits.Add(classTraits[classTraitOption - generalTraits.Count - 1]);

            NativeSpecies = new Species(
                speciesName,
                composition,
                true,
                speciesClass,
                selectedTraits,
                "alpine planet");
        }

        public Domain? FindStartingSystem(GameMap map)
        {
            ArgumentNullException.ThrowIfNull(map);
            if (string.IsNullOrEmpty(NativeSpecies.PreferredPlanet))
            {
                NativeSpecies.PreferredPlanet = "alpine planet";
            }

            foreach (MapNode node in map.Nodes)
            {
                foreach (StarSystem system in node.Systems)
                {
                    foreach (Domain domain in system.Domains)
                    {
                        if (domain.Type == NativeSpecies.PreferredPlanet)
                        {
                            return domain;
                        }
                    }
                }
            }

            return null;
        }

        public void FoundStartingColony(Domain domain, TextWriter? error = null)
        {
            ArgumentNullException.ThrowIfNull(domain);
            if (domain.System is null)
            {
                (error ?? Console.Error).WriteLine(
                    "Cannot found a colony on a domain with no parent system.");
                return;
            }

            domain.System.Owner = Name;
            domain.Owned = true;
            if (!OwnedDomains.Contains(domain))
            {
                OwnedDomains.Add(domain);
            }
        }

        private static int ReadChoice(
            TextReader input,
            TextWriter output,
            string prompt,
            int choiceCount,
            int displayOffset = 0)
        {
            while (true)
            {
                output.Write(prompt);
                string? line = input.ReadLine();
                if (line is null)
                {
                    throw new InvalidOperationException("No more input is available.");
                }

                if (int.TryParse(line, out int choice) &&
                    choice > displayOffset && choice <= choiceCount + displayOffset)
                {
                    return choice;
                }

                output.WriteLine(
                    $"Enter a number from {displayOffset + 1} to {choiceCount + displayOffset}.");
            }
        }

        private static string ReadRequiredLine(TextReader input, string errorMessage)
        {
            string? line = input.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new InvalidOperationException(errorMessage);
            }

            return line.Trim();
        }

        private static void WriteOptions(TextWriter output, IReadOnlyList<string> options, int displayOffset = 0)
        {
            for (int i = 0; i < options.Count; i++)
            {
                output.WriteLine($"{i + displayOffset + 1}) {options[i]}");
            }
        }
    }
}
