using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using T4_Pr1_Hugo_energies_2.Model;

namespace T4_Pr1_Hugo_energies_2.Pages
{
	public class SimulationsModel : PageModel
	{
		private const string FilePath = "ModelData\\simulacions_energia.csv";
		public List<ASystem> EnergySystems { get; set; } = new List<ASystem>();

		public void OnGet()
		{
			if (System.IO.File.Exists(FilePath))
			{
				var lines = System.IO.File.ReadAllLines(FilePath);
				foreach (var line in lines)
				{
					var values = line.Split('|');
					

					string type = values[1];
					DateTime date = DateTime.ParseExact(values[0],"yyyy-mm-dd", CultureInfo.InvariantCulture);
					double parameter = double.Parse(values[2], CultureInfo.InvariantCulture);
					double ratio = double.Parse(values[3], CultureInfo.InvariantCulture);
					double energyGenerated = double.Parse(values[4], CultureInfo.InvariantCulture);
					/*double costPerKWh = double.Parse(values[5], CultureInfo.InvariantCulture);
					double pricePerKWh = double.Parse(values[6], CultureInfo.InvariantCulture);
					double totalCost = double.Parse(values[7], CultureInfo.InvariantCulture);
					double totalPrice = double.Parse(values[8], CultureInfo.InvariantCulture);*/

					ASystem system = type switch
					{
						"Solar" => new SolarSystem { Date = date, value = parameter, ratio = ratio, energyGenerated = energyGenerated },
						"Eolic" => new EolicSystem { Date = date, value = parameter, ratio = ratio, energyGenerated = energyGenerated },
						"Hydroelectric" => new HydroelectricSystem { Date = date, value = parameter, ratio = ratio, energyGenerated = energyGenerated },
						_ => null
					};

					/*if (system != null)
					{
						system.costPerKWh = costPerKWh;
						system.pricePerKWh = pricePerKWh;
						system.totalCost = totalCost;
						system.totalPrice = totalPrice;
						EnergySystems.Add(system);
					}*/
				}
			}
		}
	}
}