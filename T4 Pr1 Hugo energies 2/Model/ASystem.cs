namespace T4_Pr1_Hugo_energies_2.Model
{
	public abstract class ASystem
	{
		/*data i hora en què s’ha generat (del sistema)
		tipus de simulació
		 hores de sol disponibles / velocitat del vent / cabal de l’aigua
		valor indicat per calcular la fòrmula (rati)
		energia generada
		cost i preu per kWh.
		cost total
		preu total*/
		public DateTime Date { get; set; }
		public string SimulationType { get; set; }
		public double value { get; set; }
		public double ratio { get; set; }
        public double energyGenerated { get; set; }
        public double costPerKWh { get; set; }
        public double pricePerKWh { get; set; }
        public double totalCost { get; set; }
		public double totalPrice { get; set; }

		public abstract double CalculateEnergy();


    }
}
