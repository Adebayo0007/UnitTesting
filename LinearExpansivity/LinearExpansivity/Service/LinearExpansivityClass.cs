namespace LinearExpansivity.Service
{
    public class LinearExpansivityClass : ILinearExpansivity
    {
        public decimal Change(double initial, double final)
        {
            if (initial.Equals(0) && final.Equals(0)) throw new ArgumentException();
            if (initial < 0 || final < 0) throw new ArgumentException();

            var result = final - initial;
            return (decimal)result;
        }

        public decimal LinearExpansivityOfTwoDifferentMaterialsAtSameTemprature(double linearExpansivityOfMaterial_A = 0, double linearExpansivityOfMaterial_B = 0, double lengthOfMaterial_A_In_Meter = 0, double lengthOfMaterial_B_In_Meter = 0)
        {
            if (lengthOfMaterial_B_In_Meter.Equals(0d) && lengthOfMaterial_A_In_Meter.Equals(0d) &&
               linearExpansivityOfMaterial_B.Equals(0d) && linearExpansivityOfMaterial_A.Equals(0d))
            {
                throw new ArgumentException();
            }
            decimal result = 0m;
            if (linearExpansivityOfMaterial_A.Equals(0d))
            {
                if (lengthOfMaterial_B_In_Meter.Equals(0d) || lengthOfMaterial_A_In_Meter.Equals(0d) || linearExpansivityOfMaterial_B.Equals(0d))
                {
                    throw new ArgumentException();
                }
                result = (decimal)((linearExpansivityOfMaterial_B * lengthOfMaterial_B_In_Meter) / lengthOfMaterial_A_In_Meter);  //Linear Expancivity of Material A is unknown
                return result;
            }
            if (linearExpansivityOfMaterial_B.Equals(0d))
            {
                if (lengthOfMaterial_B_In_Meter.Equals(0d) || lengthOfMaterial_A_In_Meter.Equals(0d) || linearExpansivityOfMaterial_A.Equals(0d))
                {
                    throw new ArgumentException();
                }
                result = (decimal)((linearExpansivityOfMaterial_A * lengthOfMaterial_A_In_Meter) / lengthOfMaterial_B_In_Meter);  //Linear Expancivity of Material B is unknown
                return result;
            }
            if (lengthOfMaterial_A_In_Meter.Equals(0d))
            {
                if (lengthOfMaterial_B_In_Meter.Equals(0d) || linearExpansivityOfMaterial_A.Equals(0d) || linearExpansivityOfMaterial_B.Equals(0d))
                {
                    throw new ArgumentException();
                }
                result = (decimal)((linearExpansivityOfMaterial_B * lengthOfMaterial_B_In_Meter) / linearExpansivityOfMaterial_A);  //Length of Material A is unknown
                return result;
            }
            if (lengthOfMaterial_B_In_Meter.Equals(0d))
            {
                if (lengthOfMaterial_A_In_Meter.Equals(0d) || linearExpansivityOfMaterial_A.Equals(0d) || linearExpansivityOfMaterial_B.Equals(0d))
                {
                    throw new ArgumentException();
                }
                result = (decimal)((linearExpansivityOfMaterial_A * lengthOfMaterial_A_In_Meter) / linearExpansivityOfMaterial_B); //Length of Material B is unknown
                return result;
            }
            throw new ArgumentException();
        }

        public decimal SolvingForLinearExpansivity(double initialLengthInMeter = 0, double finalLengthInMeter = 0, double initialTempInKelvin = 0, double finalTempInKelvin = 0, double alpha = 0, string resultType = null)
        {
            //Checking if the user need the answer in per Celcius,else it will solve the answer in kelvin
            if (resultType == "Celcius")
            {
                initialTempInKelvin -= 374;
                finalTempInKelvin -= 374;
            }
            var changeInLength = Change(initialLengthInMeter, finalLengthInMeter);  //BitaLength
            var changeInTemperature = Change(initialTempInKelvin, finalTempInKelvin); //BitaTita
            //Checking if the parameters are in good condition
            if (initialLengthInMeter.Equals(0d) && finalLengthInMeter.Equals(0d) && initialTempInKelvin.Equals(0d) && finalTempInKelvin.Equals(0d)) throw new ArgumentException();
            if (initialLengthInMeter < 0 ||
                finalLengthInMeter < 0) throw new ArgumentException();
            if (initialLengthInMeter > finalLengthInMeter && !finalLengthInMeter.Equals(0))
            {
                throw new ArgumentException();
            }
            if (initialTempInKelvin > finalTempInKelvin && !finalTempInKelvin.Equals(0))
            {
                throw new ArgumentException();
            }
            if (finalLengthInMeter.Equals(0d))  //Final length is unknown
            {
                if (initialLengthInMeter.Equals(0d) || alpha.Equals(0d) || changeInTemperature.Equals(0m))
                {
                    throw new ArgumentException();
                }
                decimal l2 = (decimal)initialLengthInMeter * (((decimal)alpha * changeInTemperature) + 1);
                return l2; //Returning final length,when final length is unknown
            }
            if (initialLengthInMeter.Equals(0d))  //initial length is unknown
            {
                if (finalLengthInMeter.Equals(0d) || alpha.Equals(0d) || changeInTemperature.Equals(0m))
                {
                    throw new ArgumentException();
                }
                var l1 = (decimal)finalLengthInMeter / (((decimal)alpha * changeInTemperature) + 1);
                return l1;  //Returning inital length,when initial length is unknown
            }
            if (finalTempInKelvin.Equals(0d))  //final Temperature is unknown
            {
                if (initialTempInKelvin.Equals(0d) || alpha.Equals(0d) || changeInLength.Equals(0m))
                {
                    throw new ArgumentException();
                }
                var t2 = (changeInLength / (decimal)(alpha * initialLengthInMeter)) + (decimal)initialTempInKelvin;
                return t2;  //Returning final temperature,when final temperature is unknown
            }
            if (initialTempInKelvin.Equals(0d))  //inital Temperature is unknown
            {
                if (finalTempInKelvin.Equals(0d) || alpha.Equals(0d) || changeInLength.Equals(0m) || initialLengthInMeter.Equals(0m))
                {
                    throw new ArgumentException();
                }
                var t1 = ((changeInLength / (decimal)(alpha * initialLengthInMeter)) - (decimal)finalTempInKelvin)/-1   ;
                return t1;  //Returning inital temperature,when initial tempearature is unknown
            }
            if (alpha.Equals(0d))  //linear Expansivity is unknown 
            {
                if (initialLengthInMeter.Equals(0d) || changeInLength.Equals(0m) || changeInTemperature.Equals(0m))
                {
                    throw new ArgumentException();
                }
                var linearExpansivity = changeInLength / ((decimal)initialLengthInMeter * changeInTemperature);  //Alpha(Linear Expansivity)
                return linearExpansivity; // Returning Linear Expansivity,when linear expansivity is unknown
            }
            throw new ArgumentException();
        }
    }
}
