namespace LinearExpansivity.Service
{
    public interface ILinearExpansivity
    {
        decimal Change(double initial, double final);
        decimal SolvingForLinearExpansivity(double initialLengthInMeter = 0, double finalLengthInMeter = 0, double initialTempInKelvin = 0, double finalTempInKelvin = 0, double alpha = 0, string resultType = null);
        decimal LinearExpansivityOfTwoDifferentMaterialsAtSameTemprature(double linearExpansivityOfMaterial_A = 0, double linearExpansivityOfMaterial_B = 0, double lengthOfMaterial_A_In_Meter = 0, double lengthOfMaterial_B_In_Meter = 0);
    }
}
