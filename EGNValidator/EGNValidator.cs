using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidatorForEGN;

namespace ValidatorForEGN
{
    public class EGNValidator : IEGNValidator
    {
        private int[] weights =
        {
            2, 4, 8, 5, 10, 9, 7, 3, 6
        };

        private Dictionary<string, (int, int)> ranges = new()
        {
            { "Blagoevgrad", (0, 43) },
            { "Burgas", (44, 93) },
            { "Varna", (94, 139) },
            { "Veliko Tarnovo", (140, 169) },
            { "Vidin", (170, 183) },
            { "Vratsa", (184, 217) },
            { "Gabrovo", (218, 233) },
            { "Kurdzhali", (234, 281) },
            { "Kiustendil", (282, 301) },
            { "Lovech", (302, 319) },
            { "Montana", (320, 341) },
            { "Pazardzhik", (342, 377) },
            { "Pernik", (378, 395) },
            { "Pleven", (396, 435) },
            { "Plovdiv", (436, 501) },
            { "Razgrad", (502, 527) },
            { "Ruse", (528, 555) },
            { "Silistra", (556, 575) },
            { "Sliven", (576, 601) },
            { "Smolqn", (602, 623) },
            { "Sofia-grad", (624, 721) },
            { "Sofia", (624, 721) },
            { "Sofia-okrug", (722, 751) },
            { "Stara Zagora", (752, 789) },
            { "Dobrich", (790, 821) },
            { "Tolbuhin", (790, 821) },
            { "Targovishte", (822, 843) },
            { "Haskovo", (844, 871) },
            { "Shumen", (872, 903) },
            { "Yambol", (904, 925) },
            { "Other", (926, 999) },
            { "Unknown", (926, 999) },
        };

        public string[] Generate(DateTime birthDate, string city, bool isMale)
        {
            List<string> possible = new();

            int year0 = (birthDate.Year % 100) / 10;
            int year1 = (birthDate.Year % 100) % 10;

            int month0 = birthDate.Month / 10;
            int month1 = birthDate.Month % 10;

            int day0 = birthDate.Day / 10;
            int day1 = birthDate.Day % 10;

            if (birthDate.Year < 1900) month0 += 2;
            else if (birthDate.Year >= 2000) month0 += 4;

            if (!ranges.ContainsKey(city))
                city = "Unknown";

            int lowerBound = ranges[city].Item1;
            int upperBound = ranges[city].Item2;

            for (int i = lowerBound; i <= upperBound; i ++)
            {
                if ((isMale && i % 2 != 0) || (!isMale && i % 2 == 0)) continue;

                StringBuilder egn = new();

                int sum = 0;

                egn.Append(year0);
                egn.Append(year1);

                sum += year0 * weights[0] + year1 * weights[1];

                egn.Append(month0);
                egn.Append(month1   );

                sum += month0 * weights[2] + month1 * weights[3];

                egn.Append(day0);
                egn.Append(day1);

                sum += day0 * weights[4] + day1 * weights[5];

                if (i < 10)
                {
                    egn.Append('0');
                    egn.Append('0');
                    egn.Append(i);

                    sum += i * weights[8];
                }
                else if (i < 100)
                {
                    egn.Append('0');
                    egn.Append(i);

                    sum += (i / 10) * weights[7];
                    sum += (i % 10) * weights[8];
                }
                else
                {
                    egn.Append(i);

                    sum += (i / 100) * weights[6];
                    sum += ((i % 100) / 10) * weights[7];
                    sum += ((i % 100) % 10) * weights[8];
                }

                int finalDigit = (sum % 11 < 10) ? (sum % 11) : 0;

                egn.Append(finalDigit);

                possible.Add(egn.ToString());
            }

            return possible.ToArray();
        }

        public bool Validate(string egn)
        {
            if (egn.Length != 10) return false;

            int[] digits = egn.ToCharArray().Select(c => c - '0').ToArray();

            int year = digits[0] * 10 + digits[1];
            int month = digits[2] * 10 + digits[3];
            int day = digits[4] * 10 + digits[5];

            if (month >= 41)
            {
                month -= 40;
                year += 2000;
            }
            else if (month >= 21)
            {
                month -= 20;
                year += 1800;
            }
            else
            {
                year += 1900;
            }

            try
            {
                DateTime time = new DateTime(year, month, day);
            }
            catch (Exception e) 
            {
                return false;
            }

            int sum = 0;

            for (int i = 0; i < 9; i ++)
                sum += digits[i] * weights[i];

            int finalDigit = (sum % 11 < 10) ? (sum % 11) : 0;

            if (finalDigit == digits[9]) return true;
            else return false;
        }
    }
}
