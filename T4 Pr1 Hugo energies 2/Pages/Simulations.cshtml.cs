using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using T4_Pr1_Hugo_energies_2.Model;

namespace T4_Pr1_Hugo_energies_2.Pages
{
    public class SimulacionsModel : PageModel
    {
        private const string FilePath = @"ModelData\simulacions_energia.csv";
        public List<ASystem> EnergySystems { get; set; } = new List<ASystem>();

        public void OnGet()
        {
            if (System.IO.File.Exists(FilePath))
            {
                var lines = System.IO.File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    var values = line.Split('|');
                    if (values.Length < 9) continue; // Evitar errores en líneas inválidas

                    string type = values[1];
                    DateTime date = DateTime.ParseExact(values[0], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                    double parameter = double.Parse(values[2]);
                    double ratio = double.Parse(values[3]);
                    double energyGenerated = double.Parse(values[4]);
                    double costPerKWh = double.Parse(values[5]);
                    double pricePerKWh = double.Parse(values[6]);
                    double totalCost = double.Parse(values[7]);
                    double totalPrice = double.Parse(values[8]);

                    ASystem system = type switch
                    {
                        "Solar" => new SolarSystem { Date = date, value = parameter, ratio = ratio, energyGenerated = energyGenerated },
                        "Eolic" => new EolicSystem { Date = date, value = parameter, ratio = ratio, energyGenerated = energyGenerated },
                        "Hydro" => new HydroelectricSystem { Date = date, value = parameter, ratio = ratio, energyGenerated = energyGenerated },
                        _ => null
                    };

                    if (system != null)
                    {
                        system.costPerKWh = costPerKWh;
                        system.pricePerKWh = pricePerKWh;
                        system.totalCost = totalCost;
                        system.totalPrice = totalPrice;
                        EnergySystems.Add(system);
                    }
                }
            }
        }
    }
}
