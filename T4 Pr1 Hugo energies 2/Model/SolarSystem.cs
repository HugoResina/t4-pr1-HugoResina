﻿namespace T4_Pr1_Hugo_energies_2.Model
{
    public class SolarSystem : ASystem
    {
        public DateTime Date { get; set; }
        public string SimulationType = "Solar";
        public double value { get; set; }
        public double ratio { get; set; }
        public double energyGenerated { get; set; }
        public double costPerKWh { get; set; }
        public double pricePerKWh { get; set; }
        public double totalCost { get; set; }
        public double totalPrice { get; set; }
        public override double CalculateEnergy()
        {
            if (SimulationType == "Solar")
            {
                energyGenerated = value * ratio;
                return energyGenerated;
            }
            else
                return 0.0;
        }

    }
}
